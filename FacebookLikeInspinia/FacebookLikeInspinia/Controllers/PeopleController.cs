using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
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
            
            var viewModel = _dbContext.Users
                .Include(nameof(ApplicationUser.Followers))
                .Include(nameof(ApplicationUser.Following))
                .Where(u => u.Id != currentUserId)
                .ToList().Select(x => new PersonBasicInfoViewModel
                {
                    FollowerCount = x.Followers.Count,
                    FollowingCount = x.Following.Count,
                    PostCount = _dbContext.Posts.Count(p => p.UserOwnerId == x.Id),
                    IsFollowedByCurrentUser = x.Followers.Any(f => f.Id == currentUserId),
                    FullName = $"{x.FirstName} {x.LastName}",
                    UserId = x.Id
                });

            return View(viewModel);
        }

        public ActionResult Detail(string userId)
        {
            var user = _dbContext.Users
                .Include(nameof(ApplicationUser.Followers))
                .Include(nameof(ApplicationUser.Following))
                .FirstOrDefault(x => x.Id == userId);

            if (user == null) return HttpNotFound();
            var profileViewModel = new ProfileViewModel
            {
                About = user.About,
                FullName = $"{user.FirstName} {user.LastName}",
                FollowerCount = user.Followers.Count,
                FollowingCount = user.Following.Count
            };
            return View(profileViewModel);
        }

        [HttpPost]
        public ActionResult UploadProfilePhoto(UploadProfilePhotoViewModel uploadProfilePhotoViewModel)
        {

            if (!ModelState.IsValid) return RedirectToAction(nameof(Detail), new {userId = User.Identity.GetUserId()});

            var user = _dbContext.Users.FirstOrDefault(x => x.Id == uploadProfilePhotoViewModel.UserId);

            if(user == null) return RedirectToAction(nameof(Detail), new { userId = User.Identity.GetUserId() });

            var model = uploadProfilePhotoViewModel.ProfilePhoto;
            byte[] data;
            using (var inputStream = model.InputStream)
            {
                if (!(inputStream is MemoryStream memoryStream))
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }

            user.ProfilePhoto = data;

            _dbContext.Entry(user).State = EntityState.Modified;

            _dbContext.SaveChanges();

            return RedirectToAction(nameof(Detail), new { userId = User.Identity.GetUserId() });
        }

        /*
         * [HttpPost]
           public ActionResult Index(HttpPostedFileBase file) {
           
           if (file.ContentLength > 0) {
           var fileName = Path.GetFileName(file.FileName);
           var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), fileName);
           file.SaveAs(path);
           }
           
           return RedirectToAction("Index");
           }
         */
    }
}