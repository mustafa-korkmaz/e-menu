namespace Domain.Aggregates.Product
{
    public class Product : Document
    {
        public string? CategoryId { get; private set; }

        public string MenuId { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public string? ImageUrl { get; private set; }

        public decimal Price { get; private set; }

        public byte Currency { get; private set; }

        public bool IsActive { get; private set; }

        public short DisplayOrder { get; private set; }

        public Product(string id, string? categoryId, string menuId, string name, string? description, string? imageUrl, decimal price, byte currency, short displayOrder, bool isActive, string createdBy) : base(id)
        {
            CategoryId = categoryId;
            MenuId = menuId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Price = price;
            Currency = currency;
            IsActive = true;
            DisplayOrder = displayOrder;
            IsActive = isActive;
            CreatedBy = createdBy;
        }
    }
}