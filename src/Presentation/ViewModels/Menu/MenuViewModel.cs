
namespace Presentation.ViewModels.Menu
{
    public class MenuViewModel : ViewModelBase
    {
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string UrlSlug { get; set; } = string.Empty;

        public string? LogoUrl { get; set; }

        public string? Twitter { get; set; }

        public string? Facebook { get; set; }

        public string? Instagram { get; set; }

        public string? Address { get; set; }

        public IReadOnlyCollection<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();
    }
}