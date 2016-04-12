using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using vote.Models;

namespace vote.Controller
{
    public class BaseController : BaseController<VoteContext,User,string>
    {
        
    }
}
