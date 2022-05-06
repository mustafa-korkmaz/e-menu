

namespace Domain.Aggregates
{
    public class Document : IDocument
    {
        public string Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public void SetCreatedAt(DateTime createdAt)
        {
            CreatedAt = createdAt;
        }

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
        string Id { get; }

        DateTime CreatedAt { get; }
        
        void SetCreatedAt(DateTime createdAt);
    }

    public class ListDocumentRequest
    {
        public int Offset { get; set; }

        public int Limit { get; set; }
    }

    /// <summary>
    /// for specific searches which requires extra filters
    /// </summary>
    /// <typeparam name="TSearchCriteria"></typeparam>
    public class ListDocumentRequest<TSearchCriteria> : ListDocumentRequest
    {
        public TSearchCriteria SearchCriteria { get; set; } = default!;
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