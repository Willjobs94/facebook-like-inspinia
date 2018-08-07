namespace FacebookLikeInspinia.ViewModels.People
{
    public class PersonBasicInfoViewModel
    {
        public string FullName { get; set; }
        public int PostCount { get; set; }
        public int FollowingCount { get; set; }
        public int FollowerCount { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }
        public string UserId { get; set; }
        public string Base64ProfileImage { get; set; }
    }
}