
namespace Presentation.ViewModels.Home
{
    public class ProductItemViewModel
    {
        public decimal Price { get; set; }
        public string Currency { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }
}
