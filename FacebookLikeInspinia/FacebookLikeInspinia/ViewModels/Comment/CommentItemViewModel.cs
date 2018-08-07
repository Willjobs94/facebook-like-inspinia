using System;

namespace FacebookLikeInspinia.ViewModels.Comment
{
    public class CommentItemViewModel
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public int LikesCount { get; set; }
        public string Content { get; set; }
        public string CommentedByFullName { get; set; }
        public bool IsLikedByCurrentUser { get; set; }
        public DateTime CreatedAt { get; set; }
        public string JsonParsedCreatedAt { get => CreatedAt.ToString("dd.MM.yyyy"); }
        public string Base64ProfileImage { get; set; }
        public string UserId { get; set; }
    }
}