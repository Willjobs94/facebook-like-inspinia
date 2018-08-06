using System.ComponentModel.DataAnnotations;
using System.Web;

namespace FacebookLikeInspinia.ViewModels.People
{
    public class UploadProfilePhotoViewModel
    {
        [Required]
        public HttpPostedFileBase ProfilePhoto { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}