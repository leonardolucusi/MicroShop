using MicroShop.Web.Application;
using MicroShop.Web.Domain.DTOs.UserDTOs;
using MicroShop.Web.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MicroShop.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View(registerDto);
            }
            var result = await _userService.RegisterUserAsync(registerDto);
            if (result == null)
            {
                ModelState.AddModelError("", "Failed to register user.");
                return View(registerDto);
            }
            return RedirectToAction("LoginPage");
        }

        public IActionResult LoginPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            if (!ModelState.IsValid) return View(loginDto);

            var token = await _userService.AuthenticateAsync(loginDto);

            if (token == null)
            {
                ModelState.AddModelError("", "Invalid username or password.");
                return View(loginDto);
            }

            Response.Cookies.Append("jwt", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
            });

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<User>> UserEditPage()
        {
            return View(await _userService.GetUserById(GetUserIdFromToken(Request.Cookies["jwt"])));
        }
        private int GetUserIdFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            handler.InboundClaimTypeMap.Clear();
            handler.InboundClaimTypeMap.Add(ClaimTypes.NameIdentifier, "nameid");
            var jwtToken = handler.ReadJwtToken(token);
            var userIdString = jwtToken.Claims.First(claim => claim.Type == "nameid").Value;
            if (int.TryParse(userIdString, out int userId))
            {
                return userId;
            }
            return userId;
        }
    }
}
