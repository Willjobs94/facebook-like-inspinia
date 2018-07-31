namespace FacebookLikeInspinia.Models
{
    public class Comment : BaseEntity
    {
        public int CommentOwnerUserId { get; set; }
        public virtual ApplicationUser CommentOwnerUser { get; set; }
        public string Body { get; set; }
        public int PostId { get; set; }
        public virtual Post Post { get; set; }
    }
}