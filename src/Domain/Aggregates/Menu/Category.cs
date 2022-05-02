namespace Domain.Aggregates.Menu
{
    public class Category
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public string? ImageUrl { get; private set; }

        public short DisplayOrder { get; private set; }

        public Category(string id, string name, string? imageUrl, short displayOrder)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            DisplayOrder = displayOrder;
        }
    }
}