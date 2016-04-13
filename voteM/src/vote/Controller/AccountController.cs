using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Authorization;
using vote.Models;

namespace vote.Controller
{
    public class AccountController : BaseController
    {
        #region 管理员登录
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var result = await SignInManager.PasswordSignInAsync(username, password, false, false);
            if (result.Succeeded)
            {
                return Content("success");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        #endregion
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await SignInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        [Authorize(Roles ="超级管理员")]
        public IActionResult CreateAdmin()
        {
            return View();
        }
        //[Authorize(Roles ="超级管理员")]
        //[HttpPost]
        //public async Task<IActionResult> CreateAdmin(User user)
        //{
        //    return View();
        //}

    }
}
