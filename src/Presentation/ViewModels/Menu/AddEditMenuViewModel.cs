using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels.Menu
{
    public class AddEditMenuViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "NAME")]
        public string? Name { get; set; }

        [StringLength(1000, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "IMAGE_URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [RegularExpression("^[a-zA-Z0-9_-]*$", ErrorMessage = ValidationErrorCode.AlphaNumericCharsAllowed)]
        [Display(Name = "URL_SLUG")]
        public string? UrlSlug { get; set; }

        [StringLength(1000, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "LOGO_URL")]
        public string? LogoUrl { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "TWITTER")]
        public string? Twitter { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "FACEBOOK")]
        public string? Facebook { get; set; }

        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "INSTAGRAM")]
        public string? Instagram { get; set; }

        [StringLength(1000, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "ADDRESS")]
        public string? Address { get; set; }
    }
}
