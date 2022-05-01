using Application.Dto.Menu;
using AutoMapper;
using Domain.Aggregates.Menu;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Menu
{
    public class MenuService : ServiceBase<IMenuRepository, Domain.Aggregates.Menu.Menu, MenuDto>, IMenuService
    {
        public MenuService(IUnitOfWork uow, ILogger<MenuService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {
        }
       
    }
}