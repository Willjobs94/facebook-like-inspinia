using System.Collections.Generic;

namespace FacebookLikeInspinia.Models
{
    public class Post : BaseEntity
    {
        public int UserOwnerId { get; set; }
        public virtual ApplicationUser UserOwner { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
        public string BodyContent { get; set; }
    }
}