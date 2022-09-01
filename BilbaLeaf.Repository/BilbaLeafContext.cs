using BilbaLeaf.Entities;
using BilbaLeaf.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace BilbaLeaf.Repository
{
    public class BilbaLeafContext:IdentityDbContext<AppUser>
    {
        public BilbaLeafContext(DbContextOptions<BilbaLeafContext> options) : base(options)
        {

        }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ArticleImage> ArticleImages { get; set; }
        public DbSet<ArticleKeyword> ArticleKeywords { get; set; }
        public DbSet<ArticleReference> ArticleReferences { get; set; }
        public DbSet<Keyword> Keywords { get; set; }
        public DbSet<Synonym> Synonyms { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Biz> Biz { get; set; }
        public DbSet<BizAddress> BizAddress { get; set; }
        public DbSet<UserAddress> UserAddress { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }


        public virtual async Task Commit()
        {
            var i = await base.SaveChangesAsync();
        }

    }
}
