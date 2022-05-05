
namespace Presentation.ViewModels.Menu
{
    public class CategoryViewModel
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public short? DisplayOrder { get; set; }
    }
}