using Presentation.Middlewares.Validations;
using System.ComponentModel.DataAnnotations;
using Application.Constants;

namespace Presentation.ViewModels
{
    public class ViewModelBase
    {
        public string Id { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }

    public class ListViewModelRequest
    {
        public int Offset { get; set; }

        [Range(1, 1000, ErrorMessage = ValidationErrorCode.BetweenRange)]
        [Display(Name = "LIMIT")]
        public int Limit { get; set; }
    }

    public class ListViewModelResponse<TViewModel> where TViewModel : class
    {
        /// <summary>
        /// Paged list items
        /// </summary>
        public IReadOnlyCollection<TViewModel> Items { get; set; } = new List<TViewModel>();

        /// <summary>
        /// Total count of items stored in repository
        /// </summary>
        public long TotalCount { get; set; }
    }

    public class ObjectIdViewModel
    {
        [ObjectIdValidation]
        public string? id { get; set; }
    }
}
