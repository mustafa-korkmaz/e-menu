using System.ComponentModel.DataAnnotations;
using Application.Constants;
using Application.Enums;
using Infrastructure.Services;
using Presentation.Middlewares.Validations;

namespace Presentation.ViewModels.Home
{
    public class UrlSlugViewModel
    {
        [Required(ErrorMessage = ValidationErrorCode.RequiredField)]
        [StringLength(50, ErrorMessage = ValidationErrorCode.MaxLength)]
        [RegularExpression("^[a-zA-Z0-9_-]*$", ErrorMessage = ValidationErrorCode.AlphaNumericCharsAllowed)]
        [Display(Name = "URL_SLUG")]
        public string? urlSlug { get; set; }
    }

    public class CategoryIdViewModel
    {
        [ObjectIdValidation]
        [Display(Name = "CATEGORY_ID")]
        public string? categoryId { get; set; }
    }

    public class HomeResponseViewModel
    {
        public string Type { get; private set; }

        public IEnumerable<object> Items { get; private set; }

        public HomeResponseViewModel(ResponseContentType type, IEnumerable<object> items)
        {
            Type = type.ResolveEnum();
            Items = items;
        }
    }
}
