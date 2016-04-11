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
        #region 图片显示页面

        [HttpGet]
        public IActionResult Show(int id)
        {
            var photo = DB.Photos.Include(x=>x.Author).Where(x => x.Id == id).SingleOrDefault();
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
        #region 书法
        [HttpGet]
        public IActionResult Handwriting()
        {
            var writing = DB.Photos
                .Include(x => x.Author)
                .Where(x=>x.Category == Models.Category.书法)
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return PagedView(writing);
        }
        #endregion

        #region 摄影
        public IActionResult Photograph()
        {
            var p = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Models.Category.摄影)
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return PagedView(p);
        }
        #endregion

        #region 素描
        public IActionResult Sketch()
        {
            var sketch = DB.Photos
                .Include(x => x.Author)
                .Where(x => x.Category == Models.Category.素描)
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return PagedView(sketch);
        }
        #endregion
    }
}
