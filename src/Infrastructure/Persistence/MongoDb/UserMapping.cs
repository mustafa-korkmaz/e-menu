using Domain.Aggregates.User;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Infrastructure.Persistence.MongoDb
{
    public class UserMapping
    {
        public static async Task ConfigureAsync(IMongoContext mongoContext)
        {
            BsonClassMap.RegisterClassMap<User>(map =>
            {
                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapMember(x => x.Email).SetIsRequired(true);
                map.MapMember(x => x.Username).SetIsRequired(true);
                map.MapMember(x => x.PasswordHash).SetIsRequired(true);
            });

            var options = new CreateIndexOptions
            {
                Unique = true
            };
       
            var builder = Builders<User>.IndexKeys;
            var emailIndex = builder.Ascending(p => p.Email);

            await mongoContext.GetCollection<User>().Indexes.CreateOneAsync(new CreateIndexModel<User>(emailIndex, options));
        }
    }
}
