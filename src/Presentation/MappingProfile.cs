using Application.Dto;
using Application.Dto.Menu;
using Application.Dto.Product;
using AutoMapper;
using Presentation.ViewModels;
using Presentation.ViewModels.Menu;
using Presentation.ViewModels.Product;

namespace Presentation
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddEditProductViewModel, ProductDto>();
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditMenuViewModel, MenuDto>();
            CreateMap<MenuDto, MenuViewModel>();
        }
    }
}

