using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using vote.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace vote.Tests
{
    public class Program
    {
        public void Main(string[] args)
        {
        }
        [Fact]
        public async Task init_database_test()
        {
            var services = new ServiceCollection();
            services.AddEntityFramework()
                .AddInMemoryDatabase()
                .AddDbContext<VoteContext>(x => x.UseInMemoryDatabase());
            var provider = services.BuildServiceProvider();
            var db = provider.GetRequiredService<VoteContext>();
            await SampleData.InitDB(provider);
            Assert.NotNull(db.WebTitle);

        }
    }
}
