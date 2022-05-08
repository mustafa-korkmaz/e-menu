
using Application.Constants;
using Application.Dto;
using Application.Dto.Product;
using Application.Exceptions;
using Application.Services.Tenant;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Menu;
using Domain.Aggregates.Product;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Product
{
    public class ProductService : ServiceBase<IProductRepository, Domain.Aggregates.Product.Product, ProductDto>, IProductService
    {
        private readonly ITenantContext _tenantContext;
        private readonly IMenuRepository _menuRepository;

        public ProductService(IUnitOfWork uow, ILogger<ProductService> logger, IMapper mapper, ITenantContext tenantContext)
        : base(uow, logger, mapper)
        {
            _tenantContext = tenantContext;
            _menuRepository = uow.GetRepository<IMenuRepository, Domain.Aggregates.Menu.Menu>();
        }

        public override async Task AddAsync(ProductDto dto)
        {
            dto.CreatedBy = _tenantContext.UserId!;

            await ValidateAddAsync(dto);

            await base.AddAsync(dto);
        }

        public override async Task UpdateAsync(ProductDto dto)
        {
            var document = await Repository.GetByIdAsync(dto.Id);

            await ValidateUpdateAsync(document, dto);

            dto.CreatedBy = document!.CreatedBy;
            var createdAt = document.CreatedAt;

            var updatedDocument = Mapper.Map<Domain.Aggregates.Product.Product>(dto);

            updatedDocument.SetCreatedAt(createdAt);

            await Repository.ReplaceOneAsync(updatedDocument);
        }

        public async Task DeleteAsync(ProductDto productDto)
        {
            var document = await Repository.GetByIdAsync(productDto.Id);

            ValidateOwnership(document);

            if (document!.MenuId != productDto.MenuId)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }

            await base.DeleteByIdAsync(productDto.Id);
        }

        public async Task<ListDtoResponse<ProductDto>> ListAsync(ListDtoRequest<string> request)
        {
            var searchReq = Mapper.Map<ListDocumentRequest<string>>(request);

            var response = await Repository.ListAsync(searchReq, _tenantContext.UserId!);

            return Mapper.Map<ListDtoResponse<ProductDto>>(response);
        }

        public async Task<IReadOnlyCollection<ProductDto>> ListByCategoryIdAsync(string categoryId)
        {
            var products = await Repository.ListByCategoryIdAsync(categoryId);

            return Mapper.Map<IReadOnlyCollection<ProductDto>>(products);
        }

        private async Task ValidateAddAsync(ProductDto productDto)
        {
            var menu = await _menuRepository.GetByIdAsync(productDto.MenuId);

            ValidateMenuOwnership(menu);

            ValidateCategory(menu!, productDto);
        }

        private async Task ValidateUpdateAsync(Domain.Aggregates.Product.Product? product, ProductDto productDto)
        {
            if (product == null || product.CreatedBy != _tenantContext.UserId!)
            {
                throw new ValidationException(ErrorCode.ProductNotFound);
            }

            if (product.MenuId != productDto.MenuId)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }

            var menu = await _menuRepository.GetByIdAsync(productDto.MenuId);

            ValidateCategory(menu!, productDto);
        }

        private void ValidateMenuOwnership(Domain.Aggregates.Menu.Menu? menu)
        {
            if (menu == null || menu.CreatedBy != _tenantContext.UserId!)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }
        }

        private void ValidateOwnership(Domain.Aggregates.Product.Product? product)
        {
            if (product == null || product.CreatedBy != _tenantContext.UserId!)
            {
                throw new ValidationException(ErrorCode.ProductNotFound);
            }
        }

        private void ValidateCategory(Domain.Aggregates.Menu.Menu menu, ProductDto productDto)
        {
            if (productDto.CategoryId != null)
            {
                if (!menu.HasCategoryById(productDto.CategoryId))
                {
                    throw new ValidationException(ErrorCode.CategoryNotFound);
                }
            }
            else
            {
                if (menu.HasCategories)
                {
                    throw new ValidationException(ErrorCode.CategoryIsRequired);
                }
            }
        }
    }
}
