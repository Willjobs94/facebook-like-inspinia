using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualBasic.ApplicationServices;

namespace FacebookLikeInspinia.Models
{

    public class FacebookLikeInspiniaDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Followers).WithMany(x => x.Following)
                .Map(x => x.ToTable("Followers")
                    .MapLeftKey("ApplicationUserId")
                    .MapRightKey("FollowerUserId"));

        }

        public static FacebookLikeInspiniaDbContext Create()
        {
            return new FacebookLikeInspiniaDbContext();
        }
    }
}