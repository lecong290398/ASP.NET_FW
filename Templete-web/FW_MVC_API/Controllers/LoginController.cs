using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain;
using FW_MVC_API.Context;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FW_MVC_API.Controllers
{

    public class LoginController : AtBaseController
    {
        private readonly DataFrameworkWebContext _context;


        public LoginController(DataFrameworkWebContext context)
        {
            _context = context;
        }

        // GET: SYS001
        [HttpGet("dang-nhap")]
        public IActionResult Login(string returnUrl)
        {
            return View(new Login_Input { ReturnUrl = returnUrl });
        }

        [HttpPost("dang-nhap")]
        public async Task<IActionResult> Login([FromForm]Login_Input model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var dbAccount = await _context.AccountObject.AsNoTracking()
                .FirstOrDefaultAsync(h =>
                    h.UserName == model.Username
                    && h.PassWord == model.Password
                    && h.AtRowStatus == (int)AtRowStatus.Normal
                ).ConfigureAwait(false);

            if (dbAccount == null)
            {
                ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng !");
                ViewBag.ShowErros = "Tên đăng nhập hoặc mật khẩu không đúng !";
                return View(model);
            }

            var roles = await _context.Role_AccountObject
                .Where(h =>
                    h.FK_AccountObject == dbAccount.Id
                    && h.AtRowStatus == (int)AtRowStatus.Normal
                )
                .Select(h => h.FK_RoleNavigation.Id)
                .ToListAsync().ConfigureAwait(false);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, dbAccount.Id),
                new Claim("AtUserToken", dbAccount.Id),
                new Claim("AtTen", dbAccount.AccountObjectName),
                new Claim("AtMa", dbAccount.AccountCode),

            };

            foreach (var item in roles)
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

            if (!string.IsNullOrWhiteSpace(model.ReturnUrl))
            {
                return LocalRedirect(model.ReturnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public async Task<IActionResult> Logout()
        {
            var userId = User.Identity.Name;

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (!string.IsNullOrWhiteSpace(userId))
            {
                try
                {
                    var listFirebase = await _context.FirebaseUser
                        .Where(h => h.UserId == userId)
                        .ToListAsync().ConfigureAwait(false);

                    foreach (var item in listFirebase)
                    {
                        _context.Remove(item);
                    }

                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                catch (Exception)
                {

                }
            }


            return RedirectToAction("Index", "Home");
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

