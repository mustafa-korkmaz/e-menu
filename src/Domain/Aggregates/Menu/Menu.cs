using System.Data;

namespace Domain.Aggregates.Menu
{
    public class Menu : Document
    {
        public string UserId { get; private set; }

        public string Name { get; private set; }

        public string ImageUrl { get; private set; }

        public string UrlSlug { get; private set; }

        private ICollection<Category> _categories;
        public IReadOnlyCollection<Category> Categories
        {
            get => _categories
                .OrderBy(c => c.DisplayOrder)
                .ThenBy(c => c.Name)
                .ToList();
            private set =>
                // mongo db serialization will use this part
                _categories = value.ToList();
        }

        public bool HasCategories => _categories.Any();

        public Menu(string id, string userId, string name, string imageUrl, string urlSlug) : base(id)
        {
            Name = name;
            UserId = userId;
            ImageUrl = imageUrl;
            UrlSlug = urlSlug;
            _categories = new List<Category>();
        }

        public bool HasCategory(string name)
        {
            return _categories.Any(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void AddCategory(string id, string name, string? imageUrl, short displayOrder)
        {
            if (HasCategory(name))
            {
                throw new DuplicateNameException(nameof(name));
            }

            _categories.Add(new Category(id, name, imageUrl, displayOrder));
        }

        public void RemoveCategory(string id)
        {
            var c = _categories.SingleOrDefault(c => c.Id == id);

            if (c != null)
                _categories.Remove(c);
        }
    }
}