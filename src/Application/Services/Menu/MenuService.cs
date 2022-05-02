using Application.Constants;
using Application.Dto.Menu;
using Application.Exceptions;
using Application.Services.Tenant;
using AutoMapper;
using Domain.Aggregates.Menu;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Menu
{
    public class MenuService : ServiceBase<IMenuRepository, Domain.Aggregates.Menu.Menu, MenuDto>, IMenuService
    {
        private readonly ITenantContextService _tenantContextService;

        public MenuService(IUnitOfWork uow, ITenantContextService tenantContextService, ILogger<MenuService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
            _tenantContextService = tenantContextService;
        }

        public override async Task AddAsync(MenuDto dto)
        {
            dto.UserId = _tenantContextService.TenantContext.UserId!;

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

            dto.UserId = document!.UserId;

            var menu = Mapper.Map<Domain.Aggregates.Menu.Menu>(dto);

            await Repository.ReplaceOneAsync(menu);
        }

        private async Task ValidateUpdateAsync(Domain.Aggregates.Menu.Menu? menu, MenuDto menuDto)
        {
            if (menu == null || menu.UserId != _tenantContextService.TenantContext.UserId!)
            {
                throw new ValidationException(ErrorCode.MenuNotFound);
            }

            var existingUrlSlug = await Repository.GetByUrlSlugAsync(menuDto.UrlSlug);

            if (existingUrlSlug != null && existingUrlSlug.Id != menu.Id)
            {
                throw new ValidationException(ErrorCode.UrlSlugAlreadyExists);
            }
        }
    }
}