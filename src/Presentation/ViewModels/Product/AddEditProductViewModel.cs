using System.ComponentModel.DataAnnotations;
using Application.Constants;
using Application.Enums;
using Presentation.Middlewares.Validations;

namespace Presentation.ViewModels.Product
{
    public class AddEditProductViewModel
    {
        [ObjectIdValidation]
        [Display(Name = "MENU_ID")]
        public string? MenuId { get; set; }

        [ObjectIdValidation(AllowNull = true)]
        [Display(Name = "CATEGORY_ID")]
        public string? CategoryId { get; set; }

        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [Display(Name = "PRICE")]
        public decimal Price { get; set; }

        [EnumValidation(Type = typeof(Currency))]
        [Display(Name = "CURRENCY")]
        public string? Currency { get; set; }

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
