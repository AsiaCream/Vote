using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using vote.Models;
using System.IO;
using Microsoft.AspNet.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace vote.Controller
{
    [Authorize]
    public class AdminController : BaseController
    {
        #region

        //详情

        [HttpGet]
        public IActionResult DetailsPhotos()
        {
            return View();
        }
        //添加投稿（图片）
        [HttpGet]
        public IActionResult CreatePhotos()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePhotos(IFormFile file, Photos photo)
        {
            if (!Directory.Exists(".\\wwwroot\\upload"))
            {
                Directory.CreateDirectory(".\\wwwroot\\upload");
            }
            file.SaveAs(".\\wwwroot\\upload\\" + DateTime.Now.ToString("yyMMddhhmmss") + ".jpg");
            photo.Path = DateTime.Now.ToString("yyMMddhhmmss") + ".jpg";
            DB.Photos.Add(photo);
            photo.DateTime = DateTime.Now;
            DB.SaveChanges();
            return RedirectToAction("DetailsPhotos", "Admin");
        }
        #endregion
    }
}
