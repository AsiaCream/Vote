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
            ViewBag.Top10=DB.Photos
            .Include(x=>x.Author)
            .OrderByDescending(x=>x.VoteNumber)
            .Take(5)
            .ToList();
            var bigTitle1 = DB.WebTitle
                .Where(x => x.Id == 1)
                .SingleOrDefault();
            ViewBag.bigTitle1 = bigTitle1;
            
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

            return PagedView(ret,8);
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
            var photosCount = DB.Photos
                .OrderBy(x => x.DateTime)
                .Count();
            var authorCount = DB.Author
                .OrderBy(x => x.Id)
                .Count();
            ViewBag.PhotosCount = photosCount;
            ViewBag.AuthorCount = authorCount;
            return View();
        }
        [HttpGet]
        public IActionResult Error()
        {
            return View();
        }
        public IActionResult Voted(int id)
        {
            //var computername = Environment.MachineName;
            var photo = DB.Photos
                .Where(x => x.Id == id)
                .SingleOrDefault();
            if (photo == null)
            {
                return RedirectToAction("Error", "Home");
            }
            else
            {
                photo.VoteNumber = photo.VoteNumber + 1;
                DB.SaveChanges();
                return Content("success");
            }
            /*else
            {
                //判断电脑有没有投过票
                var ipaddress = DB.IPAddress
                    .Where(x => x.IP == computername)
                    .SingleOrDefault();
                //如果之前没有投过票直接进行投票
                if (ipaddress == null)
                {
                    photo.VoteNumber++;
                    var address = new ComputerIP { IP = computername, First = DateTime.Now };
                    DB.Photos.Add(photo);
                    DB.IPAddress.Add(address);
                    DB.SaveChanges();
                    return Content("success");
                }
                //如果已经投过票，再判断一次
                else
                {
                    //时间在差值外可以进行投票
                    double First = ipaddress.First.Minute;
                    double Second = ipaddress.Second.Minute;
                    if (Second - First > 1)
                    {
                        photo.VoteNumber++;
                        DB.SaveChanges();
                        return Content("success");
                    }
                    //时间在差值外不能投票
                    else
                    {
                        return Content("failure");
                    }
                }
            }*/

        }
        
    }
}
