
using Application.Enums;

namespace Application.Dto.Product
{
    public class ProductDto : DtoBase
    {
        public string? CategoryId { get; set; }

        public string MenuId { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public decimal Price { get; set; }

        public Currency Currency { get; set; }

        public bool IsActive { get; set; }

        public short DisplayOrder { get; set; }
    }
}
