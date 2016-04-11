using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using System.Net;
using Microsoft.Data.Entity;
using vote.Models;

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

        [HttpGet]
        public IActionResult Active()
        {
            var active = DB.Activity
                .OrderByDescending(x => x.DateTime)
                .ToList();
            return PagedView(active);
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
        [HttpPost]
        public IActionResult Vote(int id)
        {
            var computername = Environment.MachineName;
            var photo = DB.Photos
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (photo == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                //判断电脑有没有投过票
                var ipaddress = DB.IPAddress
                    .Where(x => x.IP == computername)
                    .SingleOrDefault();
                //如果之前没有投过票进行投票
                if (ipaddress == null)
                {
                    photo.VoteNumber++;
                    var address = new ComputerIP { IP = computername, First = DateTime.Now };
                    return Content("success");
                }
                //如果投票，再判断一次
                else
                {
                    //时间在差值外可以进行投票
                    double First = ipaddress.First.Minute;
                    double Second = ipaddress.Second.Minute;
                    if (Second - First > 1)
                    {
                        photo.VoteNumber++;
                        return Content("success");
                    }
                    //时间在差值外不能投票
                    else
                    {
                        return Content("failure");
                    }
                }
            }

        }
        
    }
}
