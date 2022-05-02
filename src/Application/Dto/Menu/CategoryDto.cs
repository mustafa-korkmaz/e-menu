namespace Application.Dto.Menu
{
    public class CategoryDto
    {
        public string Id { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

        public short DisplayOrder { get; set; }
    }
}