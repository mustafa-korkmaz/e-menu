

namespace Domain.Aggregates
{
    public class Document : IDocument
    {
        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public string? CreatedBy { get; protected set; }


        public Document(string id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
            CreatedBy = null;
        }
    }

    public interface IDocument
    {
        public string Id { get; }

        DateTime CreatedAt { get; }
    }

    public class ListDocumentResponse<TDocument> where TDocument : IDocument
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDocument> Items { get; set; } = new List<TDocument>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}