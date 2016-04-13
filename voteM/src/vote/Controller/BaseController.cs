using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Filters;
using vote.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace vote.Controller
{
    public class BaseController : BaseController<VoteContext,User,string>
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
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
        }
        
    }
}
