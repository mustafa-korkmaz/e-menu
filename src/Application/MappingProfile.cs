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
                .ConvertUsing(src => new User(ObjectId.GenerateNewId().ToString(), src.Username.GetNormalized(), src.Email.GetNormalized(), src.IsEmailConfirmed, src.PasswordHash, (byte)src.Subscription, src.SubscriptionExpiresAt));


            CreateMap(typeof(ListDocumentResponse<>), typeof(ListDtoResponse<>));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
              .ConvertUsing(src => new Product(ObjectId.GenerateNewId().ToString(), src.CategoryId, src.MenuId, src.Name, src.ImageUrl, src.Price, (byte)src.Currency, src.CreatedBy));

            CreateMap<Menu, MenuDto>();
            CreateMap<MenuDto, Menu>()
                .ConvertUsing((src) => new Menu(ObjectId.GenerateNewId().ToString(), src.UserId, src.Name, src.ImageUrl, src.UrlSlug, src.HasCategories));
        }
    }
}
