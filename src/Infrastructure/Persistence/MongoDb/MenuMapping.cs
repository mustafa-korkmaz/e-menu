using Domain.Aggregates.Menu;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public class MenuMapping
    {
        public static async Task ConfigureAsync(IMongoContext mongoContext)
        {
            BsonClassMap.RegisterClassMap<Menu>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x=>x.ImageUrl).SetIgnoreIfNull(true);
                map.MapMember(x => x.LogoUrl).SetIgnoreIfNull(true);
                map.MapMember(x => x.Address).SetIgnoreIfNull(true);
                map.MapMember(x => x.Twitter).SetIgnoreIfNull(true);
                map.MapMember(x => x.Facebook).SetIgnoreIfNull(true);
                map.MapMember(x => x.Instagram).SetIgnoreIfNull(true);
                map.MapMember(x => x.UrlSlug).SetIsRequired(true);
                map.MapMember(x => x.Name).SetIsRequired(true);
            });

            BsonClassMap.RegisterClassMap<Category>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Id).SetIsRequired(true)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));
            });

            var options = new CreateIndexOptions
            {
                Unique = true
            };

            var builder = Builders<Menu>.IndexKeys;
            var urlSlugIndex = builder.Ascending(p => p.UrlSlug);

            await mongoContext.GetCollection<Menu>().Indexes.CreateOneAsync(new CreateIndexModel<Menu>(urlSlugIndex, options));
        }
    }
}
