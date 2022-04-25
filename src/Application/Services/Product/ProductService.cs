
using Application.Dto.Product;
using AutoMapper;
using Domain.Aggregates.Product;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Logging;

namespace Application.Services.Product
{
    public class ProductService : ServiceBase<IProductRepository, Domain.Aggregates.Product.Product, ProductDto>, IProductService
    {
        public ProductService(IUnitOfWork uow, ILogger<ProductService> logger, IMapper mapper)
        : base(uow, logger, mapper)
        {

        }
    }
}
