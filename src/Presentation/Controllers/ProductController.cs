using Application.Dto.Product;
using Application.Services.Product;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels;
using Presentation.ViewModels.Product;
using System.Net;
using Application.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Presentation.Controllers
{
    [ApiController, Authorize(AppConstants.DefaultAuthorizationPolicy)]
    [Route("products")]
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
        public async Task<IActionResult> Search([FromQuery] ListViewModelRequest model)
        {
            var resp = await ListAsync(model);

            return Ok(resp);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get([FromRoute] ObjectIdViewModel model)
        {
            var o = await _productService.GetByIdAsync(model.id!);

            if (o == null)
            {
                return NotFound();
            }

            var viewMmodel = _mapper.Map<ProductViewModel>(o);

            return Ok(viewMmodel);
        }

        [ModelStateValidation]
        [HttpPost]
        [ProducesResponseType(typeof(ProductViewModel), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post([FromBody] AddEditProductViewModel model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            await _productService.AddAsync(productDto);

            var product = _mapper.Map<ProductViewModel>(productDto);

            return Created($"products/{product.Id}", product);
        }

        [ModelStateValidation]
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put([FromRoute] ObjectIdViewModel idModel, [FromBody] AddEditProductViewModel model)
        {
            var productDto = _mapper.Map<ProductDto>(model);

            productDto.Id = idModel.id!;

            await _productService.UpdateAsync(productDto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete([FromRoute] ObjectIdViewModel idModel)
        {
            await _productService.DeleteByIdAsync(idModel.id!);

            return NoContent();
        }

        private async Task<ListViewModelResponse<ProductViewModel>> ListAsync(ListViewModelRequest model)
        {
           // var resp = await _productService.ListAsync(model.Offset, model.Limit);
           return null;
           // return _mapper.Map<ListViewModelResponse<ProductViewModel>>(resp);
        }
    }
}