using Microsoft.AspNet.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class SampleData
    {
        public async static Task InitDB(IServiceProvider service)
        {
            var db = service.GetRequiredService<VoteContext>();
            var userManager = service.GetRequiredService<UserManager<User>>();
            if(db.Database != null && db.Database.EnsureCreated())
            {
                await userManager.CreateAsync(new User { UserName = "admin" }, "Cream2015!@#");
            }
        }
    }
}
