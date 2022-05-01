namespace Domain.Aggregates.Product
{
    public class Product : Document
    {
        public string? CategoryId { get; private set; }

        public string MenuId { get; private set; }

        public string Name { get; private set; }

        public string? ImageUrl { get; private set; }

        public decimal Price { get; private set; }

        public byte Currency { get; private set; }

        public bool IsActive { get; private set; }

        public Product(string id, string? categoryId, string menuId, string name, string? imageUrl, decimal price, byte currency, string createdBy) : base(id)
        {
            CategoryId = categoryId;
            MenuId = menuId;
            Name = name;
            ImageUrl = imageUrl;
            Price = price;
            Currency = currency;
            IsActive = true;
            CreatedBy = createdBy;
        }
    }
}