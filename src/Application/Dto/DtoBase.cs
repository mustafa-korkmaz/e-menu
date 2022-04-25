
namespace Application.Dto
{
    public abstract class DtoBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    public class ListDtoResponse<TDto> where TDto : DtoBase
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TDto> Items { get; set; }

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }
}
