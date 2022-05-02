
using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels.Menu
{
    public class AddCategoryViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "NAME")]
        public string? Name { get; set; }

        [StringLength(1000, ErrorMessage = ValidationErrorCode.MaxLength)]
        [Display(Name = "IMAGE_URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "DISPLAY_ORDER")]
        public short? DisplayOrder { get; set; }
    }
}