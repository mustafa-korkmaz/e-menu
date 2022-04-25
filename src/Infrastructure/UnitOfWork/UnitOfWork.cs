
using Domain.Aggregates;
using Infrastructure.Persistence.MongoDb;
using System.Reflection;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IMongoContext _context;

        private Dictionary<string, object> _repositories;

        public UnitOfWork(IMongoContext context)
        {
            _context = context;
            _repositories = new Dictionary<string, object>();
        }
    
        public TRepository GetRepository<TRepository, TDocument>()
            where TRepository : IRepository<TDocument>
            where TDocument : IDocument
        {
            var type = typeof(TDocument).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryInterfaceType = typeof(TRepository);

                var assignedTypesToRepositoryInterface = Assembly.GetExecutingAssembly().GetTypes().Where(t => repositoryInterfaceType.IsAssignableFrom(t)); //all types of your plugin

                var repositoryType = assignedTypesToRepositoryInterface.First(p => p.Name[0] != 'I'); //filter interfaces, select only first implemented class

                var repositoryInstance = Activator.CreateInstance(repositoryType, _context);

                if (repositoryInstance == null)
                {
                    throw new ArgumentNullException("repositoryInstance");
                }

                _repositories.Add(type, repositoryInstance);
            }
            return (TRepository)_repositories[type];
        }

        public Task CreateTransactionAsync(Func<Task> transactionBody)
        {
           return _context.SaveTransactionalChangesAsync(transactionBody);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

    }
}
