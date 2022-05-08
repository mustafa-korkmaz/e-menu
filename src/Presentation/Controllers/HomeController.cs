using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.Dto.Product;
using Application.Enums;
using Application.Services.Menu;
using Application.Services.Product;
using Presentation.Middlewares.Validations;
using Presentation.ViewModels.Home;
using Presentation.ViewModels.Menu;
using Presentation.ViewModels.Product;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("")]
    public class HomeController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IProductService _productService;

        private readonly IMapper _mapper;

        public HomeController(IMenuService menuService, IMapper mapper, IProductService productService)
        {
            _menuService = menuService;
            _mapper = mapper;
            _productService = productService;
        }

        /// <summary>
        /// returns either <see cref="ProductViewModel"/> or <see cref="CategoryViewModel"/>
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [ModelStateValidation]
        [HttpGet("{urlSlug}")]
        [ProducesResponseType(typeof(HomeResponseViewModel), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> Get([FromRoute] UrlSlugViewModel model)
        {
            var resp = await _menuService.ListContentByUrlSlugAsync(model.urlSlug!);

            if (resp is IReadOnlyCollection<ProductDto>)
            {
                var productsViewModelResp = _mapper.Map<IEnumerable<ProductItemViewModel>>(resp);

                return Ok(new HomeResponseViewModel(ResponseContentType.Product, productsViewModelResp));
            }

            var categoriesViewModelResp = _mapper.Map<IEnumerable<CategoryItemViewModel>>(resp);

            return Ok(new HomeResponseViewModel(ResponseContentType.Category, categoriesViewModelResp));
        }

        [ModelStateValidation]
        [HttpGet("{urlSlug}/category/{categoryId}")]
        [ProducesResponseType(typeof(IEnumerable<ProductItemViewModel>), (int)HttpStatusCode.OK)]

        public async Task<IActionResult> GetByCategory([FromRoute] UrlSlugViewModel model, [FromRoute] CategoryIdViewModel categoryIdModel)
        {
            var resp = await _productService.ListByCategoryIdAsync(categoryIdModel.categoryId!);

            var viewModelResp = _mapper.Map<IEnumerable<ProductItemViewModel>>(resp);

            return Ok(viewModelResp);
        }
    }
}