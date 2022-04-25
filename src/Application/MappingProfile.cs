using Application.Dto;
using Application.Dto.Order;
using Application.Dto.Product;
using AutoMapper;
using Domain.Aggregates;
using Domain.Aggregates.Order;
using Domain.Aggregates.Product;
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

            CreateMap(typeof(ListDocumentResponse<>), typeof(ListDtoResponse<>));

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>()
              .ConvertUsing(src => new Product(ObjectId.GenerateNewId().ToString(), src.CategoryId, src.MenuId, src.Name, src.ImageUrl, src.Price, (byte)src.Currency));

            CreateMap<OrderItem, OrderItemDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<OrderDto, Order>()
                .ConvertUsing((src, dest) =>
                {
                    var order = new Order(ObjectId.GenerateNewId().ToString(), src.Username);

                    foreach (var item in src.Items)
                    {
                        order.AddItem(item.ProductId, item.UnitPrice, item.Quantity);
                    }

                    return order;
                });
        }
    }
}
