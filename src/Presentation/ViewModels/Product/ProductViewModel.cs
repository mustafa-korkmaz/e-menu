namespace Presentation.ViewModels.Product
{
    public class ProductViewModel : ViewModelBase
    {
        public string MenuId { get; set; } = string.Empty;

        public string? CategoryId { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public bool IsActive { get; set; }

        public short DisplayOrder { get; set; }

        public string Currency { get; set; } = string.Empty;
    }
}
