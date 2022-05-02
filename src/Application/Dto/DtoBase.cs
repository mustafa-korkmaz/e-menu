
namespace Application.Dto
{
    public abstract class DtoBase
    {
        public string Id { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
    }

    public class ListDtoRequest
    {
        public int Offset { get; set; }

        public int Limit { get; set; }
    }

    /// <summary>
    /// for specific searches which requires extra filters
    /// </summary>
    /// <typeparam name="TSearchCriteria"></typeparam>
    public class ListDtoRequest<TSearchCriteria> : ListDtoRequest
    {
        public TSearchCriteria SearchCriteria { get; set; } = default!;
    }

    public class ListDtoResponse<TDto> where TDto : DtoBase
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDto> Items { get; set; } = new List<TDto>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}
