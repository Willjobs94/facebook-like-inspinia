using FacebookLikeInspinia.ViewModels.Comment;
using System;
using System.Collections.Generic;

namespace FacebookLikeInspinia.ViewModels.Post
{
    public class PostDetailViewModel
    {
        public int PostId { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public string Content { get; set; }
        public IEnumerable<CommentItemViewModel> Comments { get; set; }
        public string JsonParsedCreatedAt { get; set; }
        public int LikesCount { get; set; }
        public string Base64ProfileImage { get; set; }
        public string UserId { get; set; }
    }
}