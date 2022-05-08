
using Application.Dto;
using Application.Dto.Menu;

namespace Application.Services.Menu
{
    public interface IMenuService : IService<MenuDto>
    {
        public Task AddCategoryAsync(string menuId, CategoryDto dto);

        public Task DeleteCategoryAsync(string menuId, string categoryId);

        public Task<ListDtoResponse<MenuDto>> ListAsync(ListDtoRequest request);

        /// <summary>
        /// if menu has categories, then return type is <see cref="CategoryDto"/> otherwise <see cref="Dto.Product.ProductDto"/>
        /// </summary>
        /// <param name="urlSlug"></param>
        /// <returns></returns>
        public Task<IReadOnlyCollection<dynamic>> ListContentByUrlSlugAsync(string urlSlug);
    }
}
