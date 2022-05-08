using Application.Dto;
using Application.Dto.Menu;
using Application.Dto.Product;
using Application.Dto.User;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Menu;
using Domain.Aggregates.Product;
using Domain.Aggregates.User;
using Infrastructure.Services;
using MongoDB.Bson;

namespace Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Document, DtoBase>();
            CreateMap<DtoBase, Document>()
                .ConvertUsing(src => new Document(src.Id));

            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>()
                .ConvertUsing(src => new User(src.Id, src.Username.GetNormalized(), src.Email.GetNormalized(), src.IsEmailConfirmed, src.PasswordHash, (byte)src.Subscription, src.SubscriptionExpiresAt));

            CreateMap(typeof(ListDocumentResponse<>), typeof(ListDtoResponse<>));

            CreateMap<ListDtoRequest, ListDocumentRequest>();
            CreateMap(typeof(ListDtoRequest<>), typeof(ListDocumentRequest<>));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
              .ConvertUsing(src => new Product(src.Id, src.CategoryId, src.MenuId, src.Name, src.Description, src.ImageUrl, src.Price, (byte)src.Currency, src.DisplayOrder, src.CreatedBy));

            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>()
                .ConvertUsing((src) => new Category(src.Id, src.Name, src.ImageUrl, src.DisplayOrder));

            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>()
                .ConvertUsing((src) => new Menu(src.Id, src.CreatedBy, src.Name, src.ImageUrl, src.UrlSlug, src.IsPublished));
        }
    }
}
