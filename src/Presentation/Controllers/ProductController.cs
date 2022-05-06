using Application.Dto.Product;
using Application.Services.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels;
using Presentation.ViewModels.Product;
using System.Net;
using Application.Constants;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController, Authorize(AppConstants.DefaultAuthorizationPolicy)]
    [Route("menus/{menuId}/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [ModelStateValidation]
        [HttpGet]
        [ProducesResponseType(typeof(ListViewModelResponse<ProductViewModel>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Search([FromRoute] MenuIdViewModel menuIdModel, [FromQuery] ListViewModelRequest model)
        {
            var searchReq = new ListDtoRequest<string>
            {
                Limit = model.Limit,
                Offset = model.Offset,
                SearchCriteria = menuIdModel.menuId!
            };

            var resp = await _productService.ListAsync(searchReq);

            var listViewModel = _mapper.Map<ListViewModelResponse<ProductViewModel>>(resp);

            return Ok(listViewModel);
        }

        [ModelStateValidation]
        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromRoute] MenuIdViewModel menuIdModel, [FromBody] AddEditProductViewModel model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            productDto.MenuId = menuIdModel.menuId!;

            await _productService.AddAsync(productDto);

            var product = _mapper.Map<ProductViewModel>(productDto);

            return Created($"products/{product.Id}", product);
        }

        [ModelStateValidation]
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromRoute] MenuIdViewModel menuIdModel, [FromRoute] ObjectIdViewModel idModel, [FromBody] AddEditProductViewModel model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            productDto.MenuId = menuIdModel.menuId!;

            productDto.Id = idModel.id!;

            await _productService.UpdateAsync(productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] MenuIdViewModel menuIdModel, [FromRoute] ObjectIdViewModel idModel)
        {
            await _productService.DeleteAsync(new ProductDto { Id = idModel.id!, MenuId = menuIdModel.menuId! });

            return NoContent();
        }
    }
}