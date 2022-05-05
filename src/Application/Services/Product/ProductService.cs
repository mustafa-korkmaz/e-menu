
using Application.Constants;
using Application.Dto.Product;
using Application.Exceptions;
using Application.Services.Tenant;
using AutoMapper;
using Domain.Aggregates.Menu;
using Domain.Aggregates.Product;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Product
{
    public class ProductService : ServiceBase<IProductRepository, Domain.Aggregates.Product.Product, ProductDto>, IProductService
    {
        private readonly ITenantContextService _tenantContextService;
        private readonly IMenuRepository _menuRepository;

        public ProductService(IUnitOfWork uow, ILogger<ProductService> logger, IMapper mapper, ITenantContextService tenantContextService)
        : base(uow, logger, mapper)
        {
            _tenantContextService = tenantContextService;
            _menuRepository = uow.GetRepository<IMenuRepository, Domain.Aggregates.Menu.Menu>();
        }

        public override async Task AddAsync(ProductDto dto)
        {
            dto.CreatedBy = _tenantContextService.TenantContext.UserId!;

            await ValidateAddAsync(dto);

            await base.AddAsync(dto);
        }

        private async Task ValidateAddAsync(ProductDto productDto)
        {
            var menu = await _menuRepository.GetByIdAsync(productDto.MenuId);

            ValidateMenuOwnership(menu);

            if (productDto.CategoryId != null)
            {
                if (!menu!.HasCategoryById(productDto.CategoryId))
                {
                    throw new ValidationException(ErrorCode.CategoryNotFound);
                }
            }
            else
            {
                if (menu!.HasCategories)
                {
                    throw new ValidationException(ErrorCode.CategoryIsRequired);
                }
            }
        }

        private void ValidateMenuOwnership(Domain.Aggregates.Menu.Menu? menu)
        {
            if (menu == null || menu.CreatedBy != _tenantContextService.TenantContext.UserId!)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }
        }
    }
}
