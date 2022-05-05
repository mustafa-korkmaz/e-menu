using Application.Dto;
using Application.Dto.Menu;
using Application.Dto.Product;
using Application.Dto.User;
using Application.Enums;
using AutoMapper;
using Infrastructure.Services;
using MongoDB.Bson;
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
                    opt.MapFrom(source => source.Email))
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(source => ObjectId.GenerateNewId().ToString()));

            CreateMap<UserDto, UserViewModel>()
                .ForMember(dest => dest.Subscription, opt =>
                opt.MapFrom(source => source.Subscription.ResolveEnum()));

            CreateMap<UserDto, TokenViewModel>()
                .ForMember(dest => dest.Subscription, opt =>
                    opt.MapFrom(source => source.Subscription.ResolveEnum()));

            CreateMap<AddEditProductViewModel, ProductDto>()
                .ForMember(dest => dest.IsActive, opt =>
                    opt.MapFrom(source => true))
                .ForMember(dest => dest.Currency, opt =>
                    opt.MapFrom(source => source.Currency!.ToEnum<Currency>()))
                .ForMember(dest => dest.Id, opt =>
                    opt.MapFrom(source => ObjectId.GenerateNewId().ToString()));

            CreateMap<ProductDto, ProductViewModel>()
                .ForMember(dest => dest.Currency, opt =>
                    opt.MapFrom(source => source.Currency.ResolveEnum()));

            CreateMap(typeof(ListDtoResponse<>), typeof(ListViewModelResponse<>));

            CreateMap<ListViewModelRequest, ListDtoRequest>();
            CreateMap(typeof(ListViewModelRequest<>), typeof(ListDtoRequest<>));

            CreateMap<AddEditMenuViewModel, MenuDto>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(source => ObjectId.GenerateNewId().ToString()));

            CreateMap<MenuDto, MenuViewModel>();
            CreateMap<CategoryDto, CategoryViewModel>();

            CreateMap<AddCategoryViewModel, CategoryDto>()
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(source => ObjectId.GenerateNewId().ToString()));
        }
    }
}

