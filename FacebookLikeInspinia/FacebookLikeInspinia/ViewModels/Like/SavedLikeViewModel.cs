namespace FacebookLikeInspinia.ViewModels.Like
{
    public class SavedLikeViewModel
    {
        public int PostId { get; set; }
        public int CommentId { get; set; }
        public int LikeCount { get; set; }
        public string LikedByUserId { get; set; }
    }
}