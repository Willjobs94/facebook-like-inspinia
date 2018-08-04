using System.Collections.Generic;

namespace FacebookLikeInspinia.Models
{
    public class Comment : BaseEntity
    {
        public string CommentOwnerUserId { get; set; }
        public virtual ApplicationUser CommentOwnerUser { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}