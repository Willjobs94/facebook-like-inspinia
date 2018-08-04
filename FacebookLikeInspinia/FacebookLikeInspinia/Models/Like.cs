namespace FacebookLikeInspinia.Models
{
    public class Like : BaseEntity
    {
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}