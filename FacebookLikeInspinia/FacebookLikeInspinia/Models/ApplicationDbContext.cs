using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FacebookLikeInspinia.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false) { }


    }
}