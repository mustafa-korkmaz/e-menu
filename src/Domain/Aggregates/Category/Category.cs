namespace Domain.Aggregates.Category
{
    public class Category : Document
    {
        public string MenuId { get; private set; }

        public string Name { get; private set; }

        public string ImageUrl { get; private set; }

        public short DisplayOrder { get; private set; }

        public Category(string id, string menuId, string name, string imageUrl, short displayOrder) : base(id)
        {
            MenuId = menuId;
            Name = name;
            ImageUrl = imageUrl;
            DisplayOrder = displayOrder;
        }
    }
}