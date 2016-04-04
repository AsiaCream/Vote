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
        #region 图片

        //详情
        
        [HttpGet]
        public IActionResult DetailsPhotos()
        {
            var photos = DB.Photos.ToList();
            return PagedView(photos,10);
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
            return Content("success");
        }

        //删除
        public IActionResult DeletePhotos(int id)
        {
            var photo = DB.Photos
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (photo == null)
            {
                return Content("error");
            }
            else
            {
                DB.Photos.Remove(photo);
                DB.SaveChanges();
                return Content("success");
            }
            
        }
        //修改
        [HttpGet]
        public IActionResult EditPhotos(int id)
        {
            var photo = DB.Photos
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(photo == null)
            {
                return Content("没有此记录");
            }
            else
            {
                return View(photo);
            }
        }
        [HttpPost]
        public IActionResult EditPhotos(Photos photo,int id)
        {
            var photoEdit = DB.Photos
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(photoEdit == null)
            {
                return Content("没有该记录");
            }
            photoEdit.Author = photo.Author;
            photoEdit.Discription = photo.Discription;
            photoEdit.PhotoName = photo.PhotoName;
            photoEdit.DateTime = photo.DateTime;
            photoEdit.Path = photo.Path;
            DB.SaveChanges();
            return RedirectToAction("DetailsPhotos", "Admin");
        }

        //查看详细
        public IActionResult Photo(int id)
        {
            var photo = DB.Photos
                .Where(x => x.Id == id)
                .SingleOrDefault();

            return View(photo);
        }
        #endregion
    }
}
