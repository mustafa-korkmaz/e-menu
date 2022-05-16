using System.Data;

namespace Domain.Aggregates.Menu
{
    public class Menu : Document
    {
        public string Name { get; private set; }

        public string? LogoUrl { get; private set; }

        public string? ImageUrl { get; private set; }

        public string UrlSlug { get; private set; }

        public string? Twitter { get; private set; }

        public string? Facebook { get; private set; }

        public string? Instagram { get; private set; }

        public string? Address { get; private set; }

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

        public bool IsPublished { get; private set; }

        public bool HasCategories => _categories.Any();

        public Menu(string id, string userId, string name, string? imageUrl,
            string? logoUrl, string urlSlug, string? twitter, string? facebook,
            string? instagram, string? address, bool isPublished) : base(id)
        {
            Name = name;
            CreatedBy = userId;
            ImageUrl = imageUrl;
            LogoUrl = logoUrl;
            UrlSlug = urlSlug;
            _categories = new List<Category>();
            Twitter = twitter;
            Facebook = facebook;
            Instagram = instagram;
            Address = address;
            IsPublished = isPublished;
        }

        public bool HasCategoryByName(string name)
        {
            return _categories.Any(c => string.Equals(c.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public bool HasCategoryById(string id)
        {
            return _categories.Any(c => c.Id == id);
        }

        public void AddCategory(string id, string name, string? imageUrl, short displayOrder)
        {
            if (HasCategoryByName(name))
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

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetSocialMedia(string? twitter, string? facebook, string? instagram)
        {
            Twitter = twitter;
            Facebook = facebook;
            Instagram = instagram;
        }

        public void SetUrlSlug(string urlSlug)
        {
            UrlSlug = urlSlug;
        }

        public void SetImageUrls(string? imageUrl, string? logoUrl)
        {
            ImageUrl = imageUrl;
            LogoUrl = logoUrl;
        }

        public void SetAddress(string? address)
        {
            Address = address;
        }

        public void SetIsPublished(bool isPublished)
        {
            IsPublished = isPublished;
        }
    }
}