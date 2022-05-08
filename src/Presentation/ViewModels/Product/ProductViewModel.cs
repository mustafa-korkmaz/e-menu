using System.ComponentModel.DataAnnotations;
using Presentation.Middlewares.Validations;

namespace Presentation.ViewModels.Product
{
    public class ProductViewModel : ViewModelBase
    {
        public string MenuId { get; set; } = string.Empty;

        public string? CategoryId { get; set; }

        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public short DisplayOrder { get; set; }
    }

    public class MenuIdViewModel
    {
        [ObjectIdValidation]
        [Display(Name = "MENU_ID")]
        public string? menuId { get; set; }
    }
}
