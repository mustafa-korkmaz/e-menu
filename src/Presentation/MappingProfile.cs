using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Presentation.ViewModels;
using Presentation.ViewModels.Order;
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

            CreateMap<AddEditOrderViewModel, OrderDto>();
            CreateMap<AddEditOrderItemViewModel, OrderItemDto>();
            CreateMap<OrderDto, OrderViewModel>();
            CreateMap<OrderItemDto, OrderItemViewModel>();
        }
    }
}

