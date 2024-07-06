using MicroShop.Web.Application;
using MicroShop.Web.Domain.DTOs.UserDTOs;
using MicroShop.Web.Domain.Entities;
using MicroShop.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace MicroShop.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        [AllowAnonymous]
        public IActionResult RegisterPage()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterPage", registerDto);
            }

            var result = await _userService.RegisterUserAsync(registerDto);

            if (result == "Username already exists.")
            {
                ModelState.AddModelError("", result);
                ViewData["ErrorMessage"] = result; 
                return View("RegisterPage", registerDto);
            }

            if (result != "User created successfully.")
            {
                ModelState.AddModelError("", "An error occurred while registering the user.");
                ViewData["ErrorMessage"] = "An error occurred while registering the user."; 
                return View("RegisterPage", registerDto);
            }

            return RedirectToAction("LoginPage");
        }
        [AllowAnonymous]
        public IActionResult LoginPage()
        {
            var loginDto = new UserLoginDTO();
            return View(loginDto);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO loginDto)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginPage", loginDto);
            }
            try
            {
                var token = await _userService.AuthenticateAsync(loginDto);

                if (token == null)
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                    ViewData["ErrorMessage"] = "Invalid username or password.";
                    return View("LoginPage", loginDto);
                }
                Response.Cookies.Append("jwt", token, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                });
            }
            catch (Exception)
            {

                throw;
            }
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
        public async Task<ActionResult<UserRegisterDTO>> UserEditPage()
        {
            try
            {
                return View(await _userService.GetUserById(TokenManipulator.GetUserIdFromToken(Request.Cookies["jwt"])));
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UserEdit(UserRegisterDTO userDto)
        {
            try
            {
                var updatedUser = await _userService.UpdateUserAsync(userDto);
                if (updatedUser != null)
                {
                    return RedirectToAction("UserEditPage", "Users");
                }
                return View(updatedUser);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
