
namespace Presentation.ViewModels.Menu
{
    public class MenuViewModel : ViewModelBase
    {
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string UrlSlug { get; set; } = string.Empty;

        public bool HasCategories { get; set; }
    }
}