using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.Data.Entity;

namespace vote.Controller
{
    public class HomeController : BaseController
    {
        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var ret = DB.Photos
                .Include(x => x.Author)
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return PagedView(ret,16);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Manage()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
        
    }
}
