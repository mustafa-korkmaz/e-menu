using Domain.Aggregates.Menu;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Persistence.MongoDb
{
    public class MenuMapping
    {
        public static async Task Configure()
        {
            BsonClassMap.RegisterClassMap<Menu>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
            });
        }
    }
}
