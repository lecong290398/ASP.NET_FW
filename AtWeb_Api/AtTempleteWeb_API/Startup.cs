using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AtTempleteWeb_API.AtLogic;
using AtTempleteWeb_API.Context;
using AtTempleteWeb_API.Controllers;
using AtTempleteWeb_API.Data;
using AtTempleteWeb_API.Models;
using AtTempleteWeb_API.Validators;
using AtTempleteWeb_API.Validators.AccountObject;
using AtTempleteWeb_API.Validators.MenuFunction;
using AtTempleteWeb_API.Validators.Role;
using AtTempleteWeb_API.Validators.Setting;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.SwaggerGen;
using static AtDomain.AccountObjectDm;
using static AtDomain.AtMenuFuntionDm;
using static AtDomain.AtRoleDm;
using static AtDomain.AtSettingDm;

namespace AtTempleteWeb_API
{
    public class Startup
    {
        public const string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IMemoryCache Cache { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    // Return JSON responses in LowerCase?
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                    // Resolve Looping navigation properties
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });


            services.AddDbContext<AtTempleteWebContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));


            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Template WEBSITE",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Shayne Boyer",
                        Email = string.Empty,
                        Url = new Uri("https://twitter.com/spboyer"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/license"),
                    }
                });




                // JWT-token authentication by password
                c.OperationFilter<AuthResponsesOperationFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme.",

                    Scheme = "bearer",
                    BearerFormat = "JWT",

                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });


                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });




            services.AddControllers();

            services.AddAuthentication()
                .AddJwtBearer(cfg =>
                {
                    //cfg.RequireHttpsMetadata = false;
                    //cfg.SaveToken = true;
                    cfg.TokenValidationParameters = TokensController.GetTokenValidationParameters(Configuration, true);
                    cfg.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = async context =>
                        {
                            var jwtToken = (JwtSecurityToken)context.SecurityToken;
                            var userId = TokensController.GetClaimsUserId(jwtToken.Claims);
                            var device = TokensController.GetClaimsDevice(jwtToken.Claims);

                            var accessToken = TokensController.GetCacheAccessTokenAsync(Cache, userId, device);

                            if (context.SecurityToken.Id != accessToken)
                            {
                                var refreshToken = TokensController.GetCacheRefreshTokenAsync(Cache, userId, device);

                                if (string.IsNullOrWhiteSpace(refreshToken))
                                {
                                    context.Response.Headers.Add("Token-Revoked", "Access-Refresh");
                                    context.Fail("Token-Revoked-Access-Refresh");
                                }
                                else
                                {
                                    context.Response.Headers.Add("Token-Revoked", "Access");
                                    context.Fail("Token-Revoked-Access");
                                }
                            }

                            return;
                        },
                        OnAuthenticationFailed = async context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {

                                if (context.Principal != null)
                                {
                                    context.Response.Headers.Add("Token-Expired", "Access");
                                    var userId = TokensController.GetClaimsUserId(context.Principal.Claims);
                                    var device = TokensController.GetClaimsDevice(context.Principal.Claims);


                                    var refreshToken = TokensController.GetCacheRefreshTokenAsync(Cache, userId, device);

                                    if (string.IsNullOrWhiteSpace(refreshToken))
                                    {
                                        context.Response.Headers.Add("Token-Revoked", "Refresh");
                                    }
                                }

                            }
                        }
                    };
                });
            ;



            services.AddMemoryCache();


            // Đăng ký logic

            services.AddScoped<AtLoginLogic>();
            services.AddScoped<AtBaseLogic>();
            services.AddScoped<AtInformationUserLogic>();
            services.AddScoped<AtAccountObjectLogic>();
            services.AddScoped<AtPermissionMenuFunctionLogic>();
            services.AddScoped<AtRoleLogic>();
            services.AddScoped<AtDepartmentLogic>();
            services.AddScoped<AtMenuFuntionLogic>();
            services.AddScoped<AtMenuFunctionSubGroupLogic>();
            services.AddScoped<AtSettingLogic>();

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder => builder
                           .AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod()

                );
            });


            // Validators Controller AcccountObject
            services.AddSingleton<IValidator<AccountObjectDmInput_Create>, AccountObjectCreateVd>();
            services.AddSingleton<IValidator<AccountObjectDmInput_Edit>, AccountObjectEditVd>();
            services.AddSingleton<IValidator<AccountObjectDm_ResetPassword>, ResetPasswordVd>();
            services.AddSingleton<IValidator<AccountObjectDm_UpdatePass>, ModifyPassword>();
            services.AddSingleton<IValidator<AccountObjectDm_UpdateAccount>, AccountObjectUpdateInfromation>();
            services.AddSingleton<IValidator<AccountObjectDm_Delete>, AccountObjectDeleteVd>();

            //Validators Controller Role
            services.AddSingleton<IValidator<AtRoleDmInputCreate>, RoleCreateVd>();
            services.AddSingleton<IValidator<AtRoleDmInputEdit>, AtRoleEditVd>();
            services.AddSingleton<IValidator<AtRoleDmInputDelete>, AtRoleDeleteVd>();

            //Validation Controller Setting
            services.AddSingleton<IValidator<AtSettingDmCreateInput>, AtSettingCreateVd>();
            services.AddSingleton<IValidator<AtSettingDmEditInput>, AtSettingEditVd>();
            services.AddSingleton<IValidator<AtSettingDmInputDelete>, AtSettingDeleteVd>();

            //Validator Controller MenuFunction
            services.AddSingleton<IValidator<MenuFunctionDmCreatetInputOrEdit>, MenuFunctionCreatetOrEditVd>();





            // mvc + validating
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0).AddFluentValidation();

            // override modelstate
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (context) =>
                {
                    var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                    var result = new
                    {
                        Code = "400",
                        Message = "Validation errors",
                        Errors = errors
                    };
                    return new BadRequestObjectResult(result);
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IMemoryCache cache, ILogger<Startup> logger)
        {
            Cache = cache;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });


            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }


    /// <summary>
    /// 
    /// </summary>
    public class AuthResponsesOperationFilter : IOperationFilter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="context"></param>
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var authAttributes = context.MethodInfo.DeclaringType.GetCustomAttributes(true)
                .Union(context.MethodInfo.GetCustomAttributes(true))
                .OfType<AuthorizeAttribute>();

            if (authAttributes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

                operation.Security.Add(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });
            }

        }
    }
}
