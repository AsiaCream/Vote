using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace vote.Controller
{
    public class PageController : BaseController
    {
        #region 作者详情
        public IActionResult AuthorDetail(int id)
        {
            var photo = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Author.Id == id)
                .ToList();

            var author = DB.Author
                .Where(x => x.Id == id)
                .SingleOrDefault();
            ViewBag.author = author;

            var top = DB.Photos
                .Include(x => x.Author)
                .OrderBy(x => x.VoteNumber)
                .ToList();
            ViewBag.top = top;

            if(photo == null)
            {
                return Content("该作者没有作品展示");
            }
            else
            {
                return PagedView(photo);
            }
        }
        #endregion 

        #region 图片显示页面

        [HttpGet]
        public IActionResult Show(int id)
        {
            var photo = DB.Photos
                .Include(x=>x.Author)
                .Where(x => x.Id == id)
                .SingleOrDefault();
            

            var bigTitle2 = DB.WebTitle
                .Where(x => x.Id == 2)
                .SingleOrDefault();
            ViewBag.bigTitle2 = bigTitle2;

            if (photo == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                //推荐作品
                var Recommended = DB.Photos.Include(x => x.Author) 
                    .OrderByDescending(x => x.Priority)
                    .ToList();
                ViewBag.Recommended = Recommended;

                return View(photo);
            }
            
        }
        #endregion

        #region 书法(nav 第二个标题)
        [HttpGet]
        public IActionResult Handwriting()
        {
            var writing = DB.Photos
                .Include(x => x.Author)
                .Where(x=>x.Category == Models.Category.书法)
                .OrderByDescending(x => x.DateTime)
                .ToList();
            //标题
            var bigTitle1 = DB.WebTitle
                .Where(x => x.Id == 1)
                .SingleOrDefault();
            ViewBag.bigTitle1 = bigTitle1;
            var bigTitle2 = DB.WebTitle
                .Where(x => x.Id == 2)
                .SingleOrDefault();
            ViewBag.bigTitle2 = bigTitle2;

            var smallTitle1 = DB.WebTitle
                .Where(x => x.Id == 6)
                .SingleOrDefault();
            ViewBag.smallTitle1 = smallTitle1;

            var smallTitle2 = DB.WebTitle
                .Where(x => x.Id == 7)
                .SingleOrDefault();
            ViewBag.smallTitle2 = smallTitle2;

            var smallTitle3 = DB.WebTitle
               .Where(x => x.Id == 8)
               .SingleOrDefault();
            ViewBag.smallTitle3 = smallTitle3;

            var smallTitle4 = DB.WebTitle
                .Where(x => x.Id == 9)
                .SingleOrDefault();
            ViewBag.smallTitle4 = smallTitle4;

            if (writing == null)
            {
                return Content("该栏目暂无内容");
            }
            else
            {
                var top = DB.Photos
                    .Include(x => x.Author)
                    .Where(x => x.Category == Models.Category.书法)
                    .OrderBy(x => x.VoteNumber)
                    .ToList();
                ViewBag.top = top;
                return PagedView(writing);
            }
            
        }
        #endregion

        #region 摄影( nav的第三个标题)
        public IActionResult Photograph()
        {
            var p = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Models.Category.摄影)
                .OrderByDescending(x => x.DateTime)
                .ToList();

            var bigTitle1 = DB.WebTitle
                .Where(x => x.Id == 1)
                .SingleOrDefault();
            ViewBag.bigTitle1 = bigTitle1;

            var bigTitle3 = DB.WebTitle
                .Where(x => x.Id == 3)
                .SingleOrDefault();
            ViewBag.bigTitle3 = bigTitle3;

            var smallTitle1 = DB.WebTitle
                .Where(x => x.Id == 6)
                .SingleOrDefault();
            ViewBag.smallTitle1 = smallTitle1;

            var smallTitle2 = DB.WebTitle
                .Where(x => x.Id == 7)
                .SingleOrDefault();
            ViewBag.smallTitle2 = smallTitle2;

            var smallTitle3 = DB.WebTitle
               .Where(x => x.Id == 8)
               .SingleOrDefault();
            ViewBag.smallTitle3 = smallTitle3;

            var smallTitle4 = DB.WebTitle
                .Where(x => x.Id == 9)
                .SingleOrDefault();
            ViewBag.smallTitle4 = smallTitle4;
            if (p == null)
            {
                return Content("该项暂时不存在内容，请访问其他页面");
            }
            else
            {
                var top = DB.Photos
                    .Include(x => x.Author)
                    .Where(x => x.Category == Models.Category.摄影)
                    .OrderBy(x => x.VoteNumber)
                    .ToList();
                ViewBag.top = top;
                return PagedView(p);
            }
            
        }
        #endregion

        #region 素描(nav 的第四个标题)
        public IActionResult Sketch()
        {
            var sketch = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Models.Category.素描)
                .OrderByDescending(x => x.DateTime)
                .ToList();
            var bigTitle1 = DB.WebTitle
                .Where(x => x.Id == 1)
                .SingleOrDefault();
            ViewBag.bigTitle1 = bigTitle1;

            var bigTitle4 = DB.WebTitle
                .Where(x => x.Id == 4)
                .SingleOrDefault();
            ViewBag.bigTitle4 = bigTitle4;

            var smallTitle1 = DB.WebTitle
                .Where(x => x.Id == 6)
                .SingleOrDefault();
            ViewBag.smallTitle1 = smallTitle1;

            var smallTitle2 = DB.WebTitle
                .Where(x => x.Id == 7)
                .SingleOrDefault();
            ViewBag.smallTitle2 = smallTitle2;

            var smallTitle3 = DB.WebTitle
               .Where(x => x.Id == 8)
               .SingleOrDefault();
            ViewBag.smallTitle3 = smallTitle3;

            var smallTitle4 = DB.WebTitle
                .Where(x => x.Id == 9)
                .SingleOrDefault();
            ViewBag.smallTitle4 = smallTitle4;
            if(sketch == null)
            {
                return Content("该项暂时不存在内容");

            }
            else
            {
                var top = DB.Photos
                    .Include(x => x.Author)
                    .Where(x => x.Category == Models.Category.素描)
                    .OrderBy(x => x.VoteNumber)
                    .ToList();
                ViewBag.top = top;
                return PagedView(sketch);
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
        
        #endregion
    }
}
