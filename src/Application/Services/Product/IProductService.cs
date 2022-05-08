
using Application.Dto;
using Application.Dto.Product;

namespace Application.Services.Product
{
    public interface IProductService : IService<ProductDto>
    {
        public Task DeleteAsync(ProductDto productDto);

        Task<ListDtoResponse<ProductDto>> ListAsync(ListDtoRequest<string> request);

        Task<IReadOnlyCollection<ProductDto>> ListByCategoryIdAsync(string categoryId);
    }
}
