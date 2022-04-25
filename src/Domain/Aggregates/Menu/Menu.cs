namespace Domain.Aggregates.Menu
{
    public class Menu : Document
    {
        public string UserId { get; private set; }

        public string Name { get; private set; }

        public string ImageUrl { get; private set; }

        public string UrlSlug { get; private set; }

        public bool HasCategories { get; private set; }

        public Menu(string id, string userId, string name, string imageUrl, string urlSlug, bool hasCategories) : base(id)
        {
            Name = name;
            UserId = userId;
            ImageUrl = imageUrl;
            UrlSlug = urlSlug;
            HasCategories = hasCategories;
        }
    }
}