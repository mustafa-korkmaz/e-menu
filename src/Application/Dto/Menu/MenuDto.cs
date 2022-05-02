
namespace Application.Dto.Menu
{
    public class MenuDto : DtoBase
    {
        public string UserId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string UrlSlug { get; set; } = string.Empty;

        public ICollection<CategoryDto> Categories = new List<CategoryDto>();

        public bool HasCategories { get; set; }
    }
}
