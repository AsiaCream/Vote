using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vote.Models
{
    public class VoteContext : IdentityDbContext<User>
    {
        public DbSet<Photos> Photos { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Activity> Activity { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Photos>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<Author>(e =>
            {
                e.HasIndex(x => x.Id);
            });
            builder.Entity<Activity>(e =>
            {
                e.HasIndex(x => x.Id);
            });
        }
    }
}
