using System.ComponentModel.DataAnnotations;

namespace Presentation.ViewModels.Product
{
    public class AddEditProductViewModel
    {
        [Required]
        public string MenuId { get; set; }

        public string? CategoryId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Name { get; set; }

        public string? ImageUrl { get; set; }
    }
}
