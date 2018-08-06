namespace FacebookLikeInspinia.Models
{
    public class Like : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
    }
}