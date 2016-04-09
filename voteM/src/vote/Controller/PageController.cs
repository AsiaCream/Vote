using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;

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
            var photo = DB.Photos.Where(x => x.Id == id).SingleOrDefault();
            if (photo == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                return View(photo);
            }
            
        }
        #endregion
    }
}
