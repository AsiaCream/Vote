using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            if(db.Database != null && db.Database.EnsureCreated())
            {
                await roleManager.CreateAsync(new IdentityRole { Name = "超级管理员" });
                await roleManager.CreateAsync(new IdentityRole { Name = "系统管理员" });

                var superAdmin = new User { UserName = "Admin", Email = "627148026@qq.com" };
                await userManager.CreateAsync(superAdmin, "Cream2015!@#");
                await userManager.AddToRoleAsync(superAdmin, "超级管理员");

                var admin = new User { UserName = "Cream2015", Email = "343224963@qq.com" };
                await userManager.CreateAsync(admin, "Cream2015!@#");
                await userManager.AddToRoleAsync(admin, "系统管理员");


                db.WebTitle.Add(new WebTitle
                {
                    Title = "齐齐哈尔市民投票网",
                    type = "大标题1",
                    location = "这是网站首页的总标题（也是对应home的第一个标题）"
                    
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "书法类投票区",
                    type = "大标题2",
                    location = "依次对应小标题的第二个大标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "摄影类投票区",
                    type = "大标题3",
                    location = "依次对应小标题的第三个大标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "素描类投票区",
                    type = "大标题4",
                    location = "依次对应小标题的第四个大标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "随笔类投票区",
                    type = "大标题5",
                    location = "依次对应小标题的第五个大标题"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "Vote",
                    type="小标题1",
                    location = "Home 标题(nav中的第一个标题)"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "书法",
                    location = "按照nav顺序依次往下排的第二个标题",
                    type = "小标题2"

                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "摄影",
                    location = "按照nav顺序依次往下排的第三个标题",
                    type = "小标题3"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "素描",
                    location = "按照nav顺序依次往下排的第四个标题",
                    type = "小标题4"
                });
                db.WebTitle.Add(new WebTitle
                {
                    Title = "随笔",
                    location = "按照nav顺序依次往下排的第五个标题",
                    type="小标题5"

                });
                db.SaveChanges();
            }
        }
    }
}
