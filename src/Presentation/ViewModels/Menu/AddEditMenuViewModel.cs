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

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "HAS_CATEGORIES")]
        public bool? HasCategories { get; set; }
    }
}
