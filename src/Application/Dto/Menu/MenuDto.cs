
namespace Application.Dto.Menu
{
    public class MenuDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public string? LogoUrl { get; set; }

        public string UrlSlug { get; set; } = string.Empty;

        public string? Twitter { get; set; }

        public string? Facebook { get; set; }

        public string? Instagram { get; set; }

        public string? Address { get; set; }

        public ICollection<CategoryDto> Categories = new List<CategoryDto>();

        public bool IsPublished { get; set; }

        public bool HasCategories { get; set; }
    }
}
