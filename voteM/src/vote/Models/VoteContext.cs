using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class VoteContext:IdentityDbContext<User>
    {

    }
}
