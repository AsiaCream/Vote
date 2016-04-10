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
        #region
        //图片显示页面
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
    }
}
