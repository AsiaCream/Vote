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

                db.WebTitle.Add(new WebTitle
                {
                    Title = "齐齐哈尔市民投票网",
                    location = "这是网站首页的总标题"

                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "Vote",
                    location = "Home 标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "摄影",
                    location = "按照nav顺序依次往下排的第二个标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "书法",
                    location = "按照nav顺序依次往下排的第三个标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "素描",
                    location = "按照nav顺序依次往下排的第四个标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "随笔",
                    location = "按照nav顺序依次往下排的第五个标题"
                });
                db.SaveChanges();
            }
        }
    }
}
