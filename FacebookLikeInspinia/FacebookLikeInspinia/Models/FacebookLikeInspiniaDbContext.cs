using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.VisualBasic.ApplicationServices;

namespace FacebookLikeInspinia.Models
{
    
    public class FacebookLikeInspiniaDbContext : IdentityDbContext<ApplicationUser>
    {

        //public FacebookLikeInspiniaDbContext()

        //    {
        //        Database.SetInitializer<FacebookLikeInspiniaDbContext>(null);// Remove default initializer
        //        Configuration.ProxyCreationEnabled = false;
        //        Configuration.LazyLoadingEnabled = false;
        //    }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Like> Likes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();


            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Followers).WithMany(x => x.Following)
                .Map(x => x.ToTable("Followers")
                    .MapLeftKey("ApplicationUserId")
                    .MapRightKey("FollowerId"));
        }

        public  static FacebookLikeInspiniaDbContext Create()
        {
            return new FacebookLikeInspiniaDbContext();
        }

    }
}