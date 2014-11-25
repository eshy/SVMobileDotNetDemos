using System.ComponentModel.DataAnnotations;

namespace PicShare.Api.Models
{
    public class UserModel
    {
        [Required]
        [Display(Name = "Nickname")]
        public string NickName { get; set; }

        [Required]
        [Display(Name = "Password")]
//        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
