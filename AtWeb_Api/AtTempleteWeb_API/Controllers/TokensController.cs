using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AtDomain;
using AtTempleteWeb_API.AtLogic;
using AtTempleteWeb_API.Entires;
using AtTempleteWeb_API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static AtDomain.AccountObjectDm;
using static AtDomain.AtTokensDm;

namespace AtTempleteWeb_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensController : AtBaseApiController
    {

        private readonly IConfiguration _configuration;
        private readonly IMemoryCache _cache;
        private readonly AtLoginLogic _logicLogin;
        private readonly AtBaseLogic _logicBase;

        public TokensController(
           IConfiguration configuration,
           IMemoryCache cache,
           AtLoginLogic logicLogin,
           AtBaseLogic logicBase
           )
        {
            _configuration = configuration;
            _cache = cache;
            _logicLogin = logicLogin;
            _logicBase = logicBase;
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<AtResult<LoginDtoOutput>>> Login([FromBody] LoginDto model)
        {
            var result = await _logicLogin.Login(model.Username, model.Password).ConfigureAwait(false);

            if (result != null)
            {
                //var appUser = await _userManager.FindByNameAsync(model.Username).ConfigureAwait(false);
                //var listRole = await _userManager.GetRolesAsync(appUser).ConfigureAwait(false);
                var token = await GenerateTokenAsync(model.Username, model.Device, result.Item1).ConfigureAwait(false);


                // Khởi tạo dữ liệu trả về 
                var AccountObjectOutput = new AccountObjectDmOutput
                {
                    Id = result.Item1.Id,
                    AccountCode = result.Item1.AccountCode,
                    AccountObjectName = result.Item1.AccountObjectName,
                    UserName = result.Item1.UserName
                };
                var modelLoginOutput = new LoginDtoOutput
                {
                    Tokens = token,
                    AccountObjOutput = AccountObjectOutput,
                    ListIdRole = result.Item2
                };

                return new AtResult<LoginDtoOutput>(modelLoginOutput);
            }

            return new AtResult<LoginDtoOutput>(AtNotify.LoginFail);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost(nameof(Logout))]
        public async Task<IActionResult> Logout()
        {
            var userId = GetClaimsUserId(User.Claims);
            var device = GetClaimsDevice(User.Claims);

            var keyAccessToken = CachingHelpers.GetKeyAccessToken(userId, device);
            _cache.Remove(keyAccessToken);

            var keyRefressToken = CachingHelpers.GetKeyRefreshToken(userId, device);
            _cache.Remove(keyRefressToken);

            return Ok();
        }

        [HttpPost(nameof(RefreshToken))]
        public async Task<ActionResult<AtResult<TokenDto>>> RefreshToken([FromBody]RefreshDto model)
        {
            var principal = GetPrincipalFromExpiredToken(model.AccessToken);
            if (principal == null)
            {
                Response.Headers.Add("Token-Invalid", "Access");
                return BadRequest("Token-Invalid-Access");
            }

            var userId = GetClaimsUserId(principal.Claims);
            var device = GetClaimsDevice(principal.Claims);

            var valueRefreshToken = GetCacheRefreshTokenAsync(_cache, userId, device);
            if (valueRefreshToken != model.RefreshToken)
            {
                Response.Headers.Add("Token-Invalid", "Refresh");
                return BadRequest("Token-Invalid-Refresh");
            }

            var token = await GenerateTokenAsync(userId, device, principal.Claims).ConfigureAwait(false);

            return new AtResult<TokenDto>(token);
        }

        private (string tokenId, string token, DateTime expires) GenerateJwtToken(in string username, in string device, AccountObject user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username), // Cho jwt token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name, username), // cho cookie
                new Claim(ATClaimTypes.Id, user.Id),
                new Claim(ATClaimTypes.Device, $"{device}")
            };


            return GenerateJwtToken(claims);
        }
        private (string tokenId, string token, DateTime expires) GenerateJwtToken(IEnumerable<Claim> listClaim)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var time = _configuration.GetValue<int>("JwtExpireMinutes");

            var expires = _logicBase.GetDateTimeFromServer().AddMinutes(_configuration.GetValue<int>("JwtExpireMinutes"));

            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                listClaim,
                expires: expires,
                signingCredentials: creds
            );

            return (token.Id, new JwtSecurityTokenHandler().WriteToken(token), token.ValidTo);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = GetTokenValidationParameters(_configuration, false);

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken)
                || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                //throw new SecurityTokenException("Invalid token");
                return null;
            }

            return principal;
        }

        [NonAction]
        public static string GetCacheRefreshTokenAsync(IMemoryCache cache, in string userId, in string device)
        {
            var key = CachingHelpers.GetKeyRefreshToken(userId, device);
            var asd = cache.Get<string>(key);
            return cache.Get<string>(key);
        }
        [NonAction]
        public static string GetCacheAccessTokenAsync(IMemoryCache cache, in string userId, in string device)
        {
            var key = CachingHelpers.GetKeyAccessToken(userId, device);
            return cache.Get<string>(key);
        }

        private Task<TokenDto> GenerateTokenAsync(string username, string device, AccountObject user)
        {
            var (accessTokenId, accessToken, expiresAccessToken) = GenerateJwtToken(username, device, user);

            return GenerateTokenAsync(
               accessTokenId, accessToken, expiresAccessToken,
               user.Id, device);
        }
        private Task<TokenDto> GenerateTokenAsync(string userId, string device, IEnumerable<Claim> listClaims)
        {
            var (accessTokenId, accessToken, expiresAccessToken) = GenerateJwtToken(listClaims);

            return GenerateTokenAsync(
                accessTokenId, accessToken, expiresAccessToken,
                userId, device);
        }
        private async Task<TokenDto> GenerateTokenAsync(
            string accessTokenId, string accessToken, DateTime expiresAccessToken,
            string userId, string device)
        {
            try
            {
                var keyAccess = CachingHelpers.GetKeyAccessToken(userId, device);

                _cache.Set(keyAccess, accessTokenId, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expiresAccessToken

                });

                var randomNumber = new byte[32];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(randomNumber);
                var refreshToken = Convert.ToBase64String(randomNumber);

                var expiresRefreshToken = _logicBase.GetDateTimeFromServer().AddDays(_configuration.GetValue<int>("JwtRefreshExpireDays"));
                var keyRefresh = CachingHelpers.GetKeyRefreshToken(userId, device);

                _cache.Set(keyRefresh, refreshToken, new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = expiresRefreshToken

                });

                return new TokenDto { accessToken = accessToken, refreshToken = refreshToken, expiresRefreshToken = expiresRefreshToken };
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        [NonAction]
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration config, bool validateLifetime)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = config["JwtIssuer"],

                ValidateAudience = true,
                ValidAudience = config["JwtIssuer"],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtKey"])),

                ValidateLifetime = validateLifetime
            };

            return tokenValidationParameters;
        }

        [NonAction]
        public static string GetClaimsUserId(IEnumerable<Claim> listClaims)
        {
            UserId = listClaims.FirstOrDefault(h => h.Type == ATClaimTypes.Id)?.Value;
            return UserId;
        }
        [NonAction]
        public static string GetClaimsDevice(IEnumerable<Claim> listClaims)
        {
            return listClaims.FirstOrDefault(h => h.Type == ATClaimTypes.Device)?.Value;
        }
    }
}