namespace FacebookLikeInspinia.ViewModels.People
{
    public class PersonBasicInfoViewModel
    {
        public string FullName { get; set; }
        public string About { get; set; }
        public bool IsFollowedByCurrentUser { get; set; }
        public string UserId { get; set; }
    }
}