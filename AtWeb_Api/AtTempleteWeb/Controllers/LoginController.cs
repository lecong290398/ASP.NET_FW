using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AtDomain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestSharp;
using static AtDomain.AccountObjectDm;
using static AtDomain.AtTokensDm;

namespace AtTempleteWeb.Controllers
{
    public class LoginController : AtBaseController
    {
        public LoginController(IConfiguration config) : base(config)
        {
        }

        [HttpGet("login")]
        public IActionResult Login(string returnUrl)
        {
            return View(new Login_Input { ReturnUrl = returnUrl });
        }

        [HttpPost("dang-nhap")]
        public async Task<AtResult<TokenDto>> Login([FromBody] Login_Input model)
        {

            try
            {
                var input = new LoginDto
                {
                    Username = model.Username,
                    Password = model.Password
                };
                var client = new RestClient(_strConfig);

                var request = new RestRequest("api/Tokens/Login");
                // Them hang loat query string bang object

                request.AddJsonBody(input);

                var response = await client.ExecutePostAsync<AtResult<LoginDtoOutput>>(request).ConfigureAwait(true);

                // Nhan duoc du lieu tu API
                if (response.IsSuccessful)
                {
                    if (response.Data.IsOk == false && response.Data.Error == AtNotify.LoginFail || response.Data.PayLoad == null)
                    {

                        return new AtResult<TokenDto>(AtNotify.LoginFail);

                    }

                    // Api logic thuc hien dung

                    var output = response.Data.PayLoad;



                    var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name, output.AccountObjOutput.AccountObjectName),
                            new Claim("userId", output.AccountObjOutput.Id),
                            new Claim("AtUserToken",output.Tokens.accessToken),
                            new Claim("AtUserTokenReset",output.Tokens.refreshToken),
                            };



                    foreach (var item in output.ListIdRole)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, item));
                    }

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.

                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        //IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);





                    return new AtResult<TokenDto>(output.Tokens);

                }
                else
                {
                    return new AtResult<TokenDto>(AtNotify.Conectimeout);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost("dang-xuat")]
        public async Task<AtResult<bool>> Logout()
        {
            var userId = User.Identity.Name;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            if (!string.IsNullOrWhiteSpace(userId))
            {
                try
                {
                    var claimsIdentity = User.Claims.ToList();
                    var token = claimsIdentity?.FirstOrDefault(x => x.Type.Equals("AtUserToken", StringComparison.OrdinalIgnoreCase))?.Value;

                    var client = new RestClient(_strConfig);

                    var request = new RestRequest("api/Tokens/Logout");
                    // Them hang loat query string bang object
                    request.AddHeader("Authorization", "Bearer " + token);

                    var response = await client.ExecutePostAsync(request).ConfigureAwait(true);

                    // Nhan duoc du lieu tu API
                    if (response.IsSuccessful)
                    {
                        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                        return new AtResult<bool>(true);
                    }
                    else
                    {
                        return new AtResult<bool>(false);
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return new AtResult<bool>(false);
        }


        [HttpPost("RefreshToken")]
        public async Task<AtResult<TokenDto>> RefreshToken()
        {
            var claimsIdentity =  User.Claims.ToList();
            var token = claimsIdentity?.FirstOrDefault(x => x.Type.Equals("AtUserToken", StringComparison.OrdinalIgnoreCase))?.Value;
            var atUserTokenReset = claimsIdentity?.FirstOrDefault(x => x.Type.Equals("AtUserTokenReset", StringComparison.OrdinalIgnoreCase))?.Value;
            var userName = claimsIdentity?.FirstOrDefault(x => x.Type.Equals(ClaimTypes.Name, StringComparison.OrdinalIgnoreCase))?.Value;
            var userID = claimsIdentity?.FirstOrDefault(x => x.Type.Equals("userId", StringComparison.OrdinalIgnoreCase))?.Value;

            try
            {
                var input = new RefreshDto
                {
                    AccessToken = token,
                    RefreshToken = atUserTokenReset
                };

                var client = new RestClient(_strConfig);

                var request = new RestRequest("api/Tokens/RefreshToken");
                // Them hang loat query string bang object

                request.AddJsonBody(input);

                var response = await client.ExecutePostAsync<AtResult<TokenDto>>(request).ConfigureAwait(true);

                // Nhan duoc du lieu tu API
                if (response.IsSuccessful)
                {
                    if (response.Data.IsOk == false && response.Data.Error == AtNotify.LoginFail || response.Data.PayLoad == null)
                    {
                        return new AtResult<TokenDto>(AtNotify.ResetTokenFail);
                    }

                    // Api logic thuc hien dung

                    var output = response.Data.PayLoad;

                    var claims = new List<Claim>
                            {
                            new Claim(ClaimTypes.Name, userName),
                            new Claim("userId", userID),
                            new Claim("AtUserToken",output.accessToken),
                            new Claim("AtUserTokenReset",output.refreshToken),
                            };


                    var claimsIdentitys = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        //AllowRefresh = <bool>,
                        // Refreshing the authentication session should be allowed.

                        //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                        // The time at which the authentication ticket expires. A 
                        // value set here overrides the ExpireTimeSpan option of 
                        // CookieAuthenticationOptions set with AddCookie.

                        //IsPersistent = true,
                        // Whether the authentication session is persisted across 
                        // multiple requests. When used with cookies, controls
                        // whether the cookie's lifetime is absolute (matching the
                        // lifetime of the authentication ticket) or session-based.

                        //IssuedUtc = <DateTimeOffset>,
                        // The time at which the authentication ticket was issued.

                        //RedirectUri = <string>
                        // The full path or absolute URI to be used as an http 
                        // redirect response value.
                    };

                    await HttpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentitys),
                        authProperties);


                    return new AtResult<TokenDto>(output);
                }
                else
                {
                    return new AtResult<TokenDto>(AtNotify.Conectimeout);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }

    public class Login_Input
    {
        [Required(ErrorMessage = "Chưa nhập tên đăng nhập")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(50, ErrorMessage = "Tên đăng nhập vượt quá 50 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Chưa nhập mật khẩu")]
        [Display(Name = "Mật khẩu")]
        [MaxLength(50, ErrorMessage = "Mật khẩu vượt quá 50 ký tự")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }
}