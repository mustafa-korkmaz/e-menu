using Domain.Aggregates.Product;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public class ProductMapping
    {
        public static async Task ConfigureAsync(IMongoContext mongoContext)
        {
            BsonClassMap.RegisterClassMap<Product>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.ImageUrl).SetIgnoreIfNull(true);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.MenuId).SetIsRequired(true)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
                map.MapMember(x => x.CategoryId).SetIgnoreIfNull(true)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            var builder = Builders<Product>.IndexKeys;
            var menuIndex = builder.Ascending(p => p.MenuId);
            var categoryIndex = builder.Ascending(p => p.CategoryId);

            await mongoContext.GetCollection<Product>().Indexes.CreateOneAsync(new CreateIndexModel<Product>(menuIndex));
            await mongoContext.GetCollection<Product>().Indexes.CreateOneAsync(new CreateIndexModel<Product>(categoryIndex));
        }
    }
}
