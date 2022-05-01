using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels;
using System.Net;
using Application.Dto.Menu;
using Application.Services.Menu;
using Presentation.ViewModels.Menu;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("menus")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IMapper _mapper;

        public MenuController(IMenuService menuService, IMapper mapper)
        {
            _menuService = menuService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpGet]
        [ProducesResponseType(typeof(ListViewModelResponse<MenuViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromQuery] ListViewModelRequest model)
        {
            var resp = await ListAsync(model);

            return Ok(resp);
        }

        [ModelStateValidation]
        [HttpPost]
        [ProducesResponseType(typeof(MenuViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AddEditMenuViewModel model)
        {
            var orderDto = _mapper.Map<MenuDto>(model);

            await _menuService.AddAsync(orderDto);

            var order = _mapper.Map<MenuViewModel>(orderDto);

            return Created($"orders/{order.Id}", order);
        }

        [ModelStateValidation]
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] AddEditMenuViewModel model)
        {
            var productDto = _mapper.Map<MenuDto>(model);

            productDto.Id = id;

            await _menuService.UpdateAsync(productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            await _menuService.DeleteByIdAsync(id);

            return NoContent();
        }

        private async Task<ListViewModelResponse<MenuViewModel>> ListAsync(ListViewModelRequest model)
        {
            return null;
            //var resp = await _menuService.ListAsync(model.Offset, model.Limit);

            //return _mapper.Map<ListViewModelResponse<OrderViewModel>>(resp);
        }
    }
}