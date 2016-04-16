using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class User:IdentityUser
    {
        public DateTime RegisterTime { get; set; }
        public string Phone { get; set; }
    }
}
