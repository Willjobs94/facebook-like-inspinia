using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using FacebookLikeInspinia.Models;
using FacebookLikeInspinia.ViewModels.People;
using Microsoft.AspNet.Identity;

namespace FacebookLikeInspinia.Controllers
{
    [Authorize]
    public class PeopleController : Controller
    {
        private readonly FacebookLikeInspiniaDbContext _dbContext = new FacebookLikeInspiniaDbContext();

        // GET: People

        public ActionResult Index()
        {
            var currentUserId = User.Identity.GetUserId();
            var viewModel = _dbContext.Users.Include(nameof(ApplicationUser.Followers))
                .Include(nameof(ApplicationUser.Following))
                .Where(u => u.Id != currentUserId)
                .AsEnumerable().Select(x => new PersonBasicInfoViewModel
                {
                    About = x.About,
                    IsFollowedByCurrentUser = x.Followers.Any(f => f.Id == currentUserId),
                    FullName = $"{x.FirstName} {x.LastName}",
                    UserId = x.Id
                });

            return View(viewModel);
        }
    }
}