using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using vote.Models;
using System.IO;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;

namespace vote.Controller
{
    [Authorize]
    public class AdminController : BaseController
    {
        #region 活动

        public IActionResult DetailsActivity()
        {
            var Activity = DB.Activity.ToList();
            return PagedView(Activity,10);
        }

        [HttpGet]
        public IActionResult CreateActivity()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateActivity(Activity activity)
        {
            DB.Activity.Add(activity);
            DB.SaveChanges();
            return RedirectToAction("DetailsActivity", "Admin");
        }

        [HttpGet]
        public IActionResult EditActivity(int id)
        {
            var activity = DB.Activity
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(activity == null)
            {
                return Content("不存在该记录！");
            }
            else
            {
                return View(activity);
            }
        }

        [HttpPost]
        public IActionResult EditActivity(Activity activity,int id)
        {
            var active = DB.Activity
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if(active == null)
            {
                return Content("该记录不存在");
            }
            else
            {
                active.ActivityTitle = activity.ActivityTitle;
                active.Originator = activity.Originator;
                active.DateTime = activity.DateTime;
                active.Content = activity.Content;
                DB.SaveChanges();
                return RedirectToAction("DetailsActivity", "Admin");
            }
      
        }

        public IActionResult DeleteActivity(int id)
        {
            var activity = DB.Activity
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (activity == null)
            {
                return Content("删除不成功，没有该记录");
            }
            else
            {
                DB.Activity.Remove(activity);
                DB.SaveChanges();
                return Content("success");
            }

        }

        public IActionResult Activity(int id)
        {
            var activity = DB.Activity
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (activity == null)
            {
                return Content("没有该记录");
            }
            else
            {
                return View(activity);
            }
        }

        #endregion

        #region 搜索
        [ValidateAntiForgeryToken]
        public IActionResult Searching(string key)
        {
            //找作者
            var Authors = DB.Author
                .Where(x => x.Id.ToString() == key || x.AuthorName.Contains(key))
                .ToList();
            //相关作者数量
            var AuthorsCount = DB.Author
                .Where(x => x.Id.ToString() == key || x.AuthorName.Contains(key)).Count();
            //找图片
            var Photos = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Id.ToString() == key || x.Title.Contains(key))
                .ToList();
            //相关图片数量
            var PhotosCount = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Id.ToString() == key || x.Title.Contains(key))
                .Count();

            ViewBag.Authors = Authors;
            ViewBag.AuthorsCount = AuthorsCount;
            ViewBag.Photos = Photos;
            ViewBag.PhotosCount = PhotosCount;

            return View();
        }
        [HttpGet]
        public IActionResult SearchResult()
        {
            return View();
        }


        #endregion

        #region 图片

        //详情

        [HttpGet]
        public IActionResult DetailsPhotos()
        {
            var photos = DB.Photos.Include(x => x.Author).ToList();
            return PagedView(photos, 10);
        }
        //添加投稿（图片）
        [HttpGet]
        public IActionResult CreatePhotos(int id)
        {
            var Author = DB.Author.Where(x => x.Id == id).SingleOrDefault();
            ViewBag.Author = Author;
            return View();
        }
        [HttpPost]
        public IActionResult CreatePhotos(int AuthorId, string Title, Category Category, int Priority, string Describe, IFormFile file)
        {
            var author = DB.Author.Where(x => x.Id == AuthorId).SingleOrDefault();
            Photos photo = new Photos();
            photo.AuthorId = AuthorId;
            photo.Title = Title;
            photo.Category = Category;
            photo.Describe = Describe;
            photo.Priority = Priority;
            photo.VoteNumber = 0;
            if (author == null)
            {
                return RedirectToAction("CreatePhotos", "Admin");
            }
            else
            {
                file.SaveAs(".\\wwwroot\\upload\\" + author.Id + "-" + author.AuthorName + "\\" + author.Id + "_" + photo.Title + ".jpg");
                photo.Path = author.Id + "-" + author.AuthorName + "\\" + author.Id + "_" + photo.Title + ".jpg";
                photo.DateTime = DateTime.Now;
           
                DB.Photos.Add(photo);
                DB.SaveChanges();
                return RedirectToAction("DetailsPhotos", "Admin");
            }

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
            var photo = DB.Photos.Include(x => x.Author)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (photo == null)
            {
                return Content("没有此记录");
            }
            else
            {
                return View(photo);
            }
        }
        [HttpPost]
        public IActionResult EditPhotos(Photos photo, int id)
        {
            var photoEdit = DB.Photos.Include(x => x.Author)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (photoEdit == null)
            {
                return Content("没有该记录");
            }
            //photoEdit.Author.AuthorName = Author;
            photoEdit.Describe = photo.Describe;
            photoEdit.Title = photo.Title;
            photoEdit.Category = photo.Category;
            photoEdit.DateTime = photo.DateTime;
            //photoEdit.Path = photo.Path;
            photoEdit.Priority = photo.Priority;
            DB.SaveChanges();
            return RedirectToAction("DetailsPhotos", "Admin");
        }

        //查看详细
        public IActionResult Photo(int id)
        {
            var photo = DB.Photos.Include(x => x.Author)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            return View(photo);
        }
        #endregion

        #region 摄影后台管理
        public IActionResult DetailsPhotograph()
        {
            var photograph = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Category.摄影)
                .ToList();
            return PagedView(photograph);
        }
        #endregion

        #region 素描后台管理
        public IActionResult DetailsSketch()
        {
            var sketch = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Category.素描)
                .ToList();
            return PagedView(sketch);
        }
        #endregion

        #region 书法后台管理
        public IActionResult DetailsHandwriting()
        {
            var handwriting = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Category.书法)
                .ToList();
            return PagedView(handwriting);
        }
        #endregion


        #region 作者
        [HttpGet]
        public IActionResult DetailsAuthor()
        {
            var authors = DB.Author.OrderByDescending(x => x.Id).ToList();
            return PagedView(authors, 10);
        }
        [HttpGet]
        public IActionResult CreateAuthor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateAuthor(Author Author)
        {
            DB.Author.Add(Author);
            DB.SaveChanges();

            if (!Directory.Exists(".\\wwwroot\\upload"))
            {
                Directory.CreateDirectory(".\\wwwroot\\upload");
            }
            if (!Directory.Exists(".\\wwwroot\\upload\\" + Author.Id + "-" + Author.AuthorName))
            {
                Directory.CreateDirectory(".\\wwwroot\\upload\\" + Author.Id + "-" + Author.AuthorName);
            }
            return RedirectToAction("DetailsAuthor", "Admin");
        }

        [HttpGet]
        public IActionResult EditAuthor(int id)
        {
            var editAuthor = DB.Author
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (editAuthor == null)
            {
                return Content("没有该记录");
            }
            else
            {
                return View(editAuthor);
            }

        }
        [HttpPost]
        public IActionResult EditAuthor(Author author, int id)
        {
            var editAuthor = DB.Author
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (editAuthor == null)
            {
                return Content("null");
            }
            editAuthor.AuthorName = author.AuthorName;
            editAuthor.Email = author.Email;
            editAuthor.PhoneNumber = author.PhoneNumber;
            DB.SaveChanges();
            return RedirectToAction("DetailsAuthor", "Admin");
        }

        public IActionResult DeleteAuthor(int id)
        {
            var author = DB.Author
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (author == null)
            {
                return Content("error");
            }
            else
            {
                if (Directory.Exists(".\\wwwroot\\upload\\" + author.Id + "-" + author.AuthorName))
                {
                    Directory.Delete(".\\wwwroot\\upload\\" + author.Id + "-" + author.AuthorName);
                }
                DB.Author.Remove(author);
                DB.SaveChanges();
                return Content("success");
            }
        }
        #endregion
    }
}
