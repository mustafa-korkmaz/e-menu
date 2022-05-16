using Application.Constants;
using Application.Dto;
using Application.Dto.Menu;
using Application.Dto.Product;
using Application.Exceptions;
using Application.Services.Tenant;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Menu;
using Domain.Aggregates.Product;
using Infrastructure.Services;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Menu
{
    public class MenuService : ServiceBase<IMenuRepository, Domain.Aggregates.Menu.Menu, MenuDto>, IMenuService
    {
        private readonly ITenantContext _tenantContextService;
        private readonly IProductRepository _productRepository;

        public MenuService(IUnitOfWork uow, ITenantContext tenantContextService, ILogger<MenuService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _tenantContextService = tenantContextService;
            _productRepository = Uow.GetRepository<IProductRepository, Domain.Aggregates.Product.Product>();
        }

        public override async Task AddAsync(MenuDto dto)
        {
            dto.CreatedBy = _tenantContextService.UserId!;

            var existingUrlSlug = await Repository.GetByUrlSlugAsync(dto.UrlSlug);

            if (existingUrlSlug != null)
            {
                throw new ValidationException(ErrorCode.UrlSlugAlreadyExists);
            }

            await base.AddAsync(dto);
        }

        public override async Task UpdateAsync(MenuDto dto)
        {
            var document = await Repository.GetByIdAsync(dto.Id);

            await ValidateUpdateAsync(document, dto);

            document!.SetName(dto.Name);
            document.SetUrlSlug(dto.UrlSlug);
            document.SetImageUrls(dto.ImageUrl, dto.LogoUrl);
            document.SetSocialMedia(dto.Twitter, dto.Facebook, dto.Instagram);
            document.SetAddress(dto.Address);
            document.SetIsPublished(dto.IsPublished);

            await Repository.ReplaceOneAsync(document);
        }

        public override async Task DeleteByIdAsync(string id)
        {
            var menu = await Repository.GetByIdAsync(id);

            ValidateMenuOwnership(menu);

            await base.DeleteByIdAsync(id);
        }

        public async Task AddCategoryAsync(string menuId, CategoryDto dto)
        {
            var menu = await Repository.GetByIdAsync(menuId);

            ValidateAddCategory(menu, dto.Name);

            menu!.AddCategory(dto.Id, dto.Name, dto.ImageUrl, dto.DisplayOrder);

            await Repository.ReplaceOneAsync(menu);
        }

        public async Task DeleteCategoryAsync(string menuId, string categoryId)
        {
            var menu = await Repository.GetByIdAsync(menuId);

            ValidateMenuOwnership(menu);

            menu!.RemoveCategory(categoryId);

            await Repository.ReplaceOneAsync(menu);
        }

        public async Task<ListDtoResponse<MenuDto>> ListAsync(ListDtoRequest request)
        {
            var req = new ListDocumentRequest<string>
            {
                Offset = request.Offset,
                Limit = request.Limit,
                SearchCriteria = _tenantContextService.UserId!
            };

            var response = await Repository.ListAsync(req);

            return Mapper.Map<ListDtoResponse<MenuDto>>(response);
        }

        public async Task<IReadOnlyCollection<dynamic>> ListContentByUrlSlugAsync(string urlSlug)
        {
            var menu = await Repository.GetByUrlSlugAsync(urlSlug.GetNormalized());

            if (menu == null)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }

            if (menu.HasCategories)
            {
                //return categories
                var categories = menu.Categories
                    .OrderBy(c => c.DisplayOrder)
                    .ToList();

                return Mapper.Map<IReadOnlyCollection<CategoryDto>>(categories);
            }

            // return products
            var products = await _productRepository.ListByMenuIdAsync(menu.Id);

            return Mapper.Map<IReadOnlyCollection<ProductDto>>(products);
        }

        private void ValidateMenuOwnership(Domain.Aggregates.Menu.Menu? menu)
        {
            if (menu == null || menu.CreatedBy != _tenantContextService.UserId!)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }
        }

        private async Task ValidateUpdateAsync(Domain.Aggregates.Menu.Menu? menu, MenuDto menuDto)
        {
            ValidateMenuOwnership(menu);

            var existingMenuWithDesiredUrlSlug = await Repository.GetByUrlSlugAsync(menuDto.UrlSlug);

            if (existingMenuWithDesiredUrlSlug != null && existingMenuWithDesiredUrlSlug.Id != menu!.Id)
            {
                throw new ValidationException(ErrorCode.UrlSlugAlreadyExists);
            }
        }

        private void ValidateAddCategory(Domain.Aggregates.Menu.Menu? menu, string categoryName)
        {
            ValidateMenuOwnership(menu);

            var categoryExists = menu!.HasCategoryByName(categoryName);

            if (categoryExists)
            {
                throw new ValidationException(ErrorCode.CategoryAlreadyExists);
            }
        }
    }
}