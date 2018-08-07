using FacebookLikeInspinia.Models;
using FacebookLikeInspinia.ViewModels.Comment;
using FacebookLikeInspinia.ViewModels.Home;
using FacebookLikeInspinia.ViewModels.Post;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;

namespace FacebookLikeInspinia.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly FacebookLikeInspiniaDbContext _dbContext = new FacebookLikeInspiniaDbContext();


        public ActionResult Index()
        {
            var posts = _dbContext.Posts
                .Include(nameof(Post.UserOwner))
                .Include(nameof(Post.Comments))
                .Include(nameof(Post.Likes)).AsEnumerable()
                .Where(x => x.IsActive && !x.IsDeleted)
                .OrderByDescending(x => x.CreatedAt).Select(x => new PostDetailViewModel
                {
                    UserId = x.UserOwnerId,
                    Base64ProfileImage = $"data:image/png;base64,{ Convert.ToBase64String(x.UserOwner.ProfilePhoto)}",
                    PostId = x.Id,
                    LikesCount = x.Likes.Count,
                    IsLikedByCurrentUser = x.Likes.Any(y => y.UserId == User.Identity.GetUserId()),
                    Content = x.BodyContent,
                    CreatedAt = x.CreatedAt,
                    UserFullName = x.UserOwner.FirstName + " " + x.UserOwner.LastName,
                    Comments = x.Comments.Select(c => new CommentItemViewModel
                    {
                        UserId = c.CommentOwnerUserId,
                        Base64ProfileImage = $"data:image/png;base64,{ Convert.ToBase64String(c.CommentOwnerUser.ProfilePhoto)}",
                        CommentedByFullName = c.CommentOwnerUser.FirstName + " " + c.CommentOwnerUser.LastName,
                        LikesCount = c.Likes.Count,
                        CommentId = c.Id,
                        Content = c.Body,
                        CreatedAt = c.CreatedAt,
                        IsLikedByCurrentUser = c.Likes.Any(l => l.UserId == User.Identity.GetUserId())
                    })
                });

            var currentUserId = User.Identity.GetUserId();

            var currentUser = _dbContext.Users.FirstOrDefault(x => x.Id == currentUserId);
            var homeIndexViewModel = new HomeIndexViewModel
            {
                Posts = posts,
                Base64ProfilePhoto = $"data:image/png;base64,{ Convert.ToBase64String(currentUser.ProfilePhoto)}",

            };
            return View(homeIndexViewModel);
        }

        public ActionResult Minor()
        {
            ViewData["SubTitle"] = "Simple example of second view";
            ViewData["Message"] = "Data are passing to view by ViewData from controller";

            return View();
        }
    }
}