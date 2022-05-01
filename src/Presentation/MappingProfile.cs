using Application.Dto;
using Application.Dto.Menu;
using Application.Dto.Product;
using Application.Dto.User;
using AutoMapper;
using Infrastructure.Services;
using Presentation.ViewModels;
using Presentation.ViewModels.Menu;
using Presentation.ViewModels.Product;
using Presentation.ViewModels.User;

namespace Presentation
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AddUserViewModel, UserDto>()
                .ForMember(dest => dest.Username, opt =>
                    opt.MapFrom(source => source.Email));

            CreateMap<UserDto, UserViewModel>()
                .ForMember(dest => dest.Subscription, opt =>
                opt.MapFrom(source => source.Subscription.ResolveEnum()));

            CreateMap<UserDto, TokenViewModel>()
                .ForMember(dest => dest.Subscription, opt =>
                    opt.MapFrom(source => source.Subscription.ResolveEnum()));

            CreateMap<AddEditProductViewModel, ProductDto>();
            CreateMap<ProductDto, ProductViewModel>();
            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<AddEditMenuViewModel, MenuDto>();
            CreateMap<MenuDto, MenuViewModel>();
        }
    }
}

