using System.Web.Mvc;
using FacebookLikeInspinia.Models;
using FacebookLikeInspinia.ViewModels;

namespace FacebookLikeInspinia.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private FacebookLikeInspiniaDbContext _dbContext;

        public PostController(FacebookLikeInspiniaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public JsonResult Create(CreatePostViewModel model)
        {
            var post = new Post
            {
                UserOwnerId = model.UserId,
                BodyContent = model.BodyContent
            };

            _dbContext.Posts.Add(post);
            _dbContext.SaveChanges();

            return Json(new { });
        }
        
    }
}