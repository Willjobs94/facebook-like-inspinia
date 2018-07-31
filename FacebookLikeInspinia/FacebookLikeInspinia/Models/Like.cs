namespace FacebookLikeInspinia.Models
{
    public class Like : BaseEntity
    {
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}