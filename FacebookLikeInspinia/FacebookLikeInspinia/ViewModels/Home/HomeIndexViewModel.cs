using FacebookLikeInspinia.ViewModels.Post;
using System.Collections.Generic;

namespace FacebookLikeInspinia.ViewModels.Home
{
    public class HomeIndexViewModel
    {
        public IEnumerable<PostDetailViewModel> Posts { get; set; }
        public string Base64ProfilePhoto { get; set; }
    }
}