
using Domain.Aggregates;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure.Persistence.MongoDb
{
    public static class MongoDbPersistence
    {
        public static async Task ConfigureAsync(IMongoContext mongoContext)
        {
            BsonClassMap.RegisterClassMap<Document>(x =>
               {
                   x.AutoMap();
                   x.MapIdMember(document => document.Id)
                   .SetSerializer(new StringSerializer(BsonType.ObjectId));
                   x.MapMember(document => document.CreatedBy).SetIgnoreIfNull(true);
               });

            await ProductMapping.ConfigureAsync(mongoContext);
            await UserMapping.ConfigureAsync(mongoContext);
            await MenuMapping.ConfigureAsync(mongoContext);
        }
    }
}
