using Microsoft.Extensions.Options;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Synevyr.Infrastructure;

public class MongoDbRepository<TModel> : IRepository<TModel> where TModel : Entity
{
    private string _collectionName;
    private string _databaseName;
    private MongoClient _mongo;
    private static readonly object _lock = new object();
    public IMongoCollection<TModel> Collection => Database.GetCollection<TModel>(_collectionName);
    private IMongoDatabase Database => _mongo.GetDatabase(_databaseName);
    
    public MongoDbRepository(string connectionString)
    {
        _mongo = new MongoClient(connectionString);
        _databaseName = MongoUrl.Create(connectionString).DatabaseName;
        _collectionName = typeof(TModel).Name;
        if (!BsonClassMap.IsClassMapRegistered(typeof(TModel)))
        {
            BsonClassMap.RegisterClassMap<TModel>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
        }
    }
    
    public MongoDbRepository(IOptions<MongodbSettings> settings)
    {
        _mongo = new MongoClient(settings.Value.ConnectionString);
        _databaseName = MongoUrl.Create(settings.Value.ConnectionString).DatabaseName;
        _collectionName = typeof(TModel).Name;
        //lock classes registration: without it we had kind of concurency access error: "An item with the same key has already been added"
        //TODO: lock is not shared among close constructed types. Needs to be reconsidered
        lock (_lock)
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(TModel)))
            {
                BsonClassMap.RegisterClassMap<TModel>(cm =>
                {
                    cm.AutoMap();
                    cm.SetIgnoreExtraElements(true);
                });
            }
        }
    }
    
    public IQueryable<TModel> AsQuaryable()
    {
        return Collection.AsQueryable();
    }
    
    public void Save(TModel model)
    {
        if (model.Id == Guid.Empty)
        {
            model.Id = Guid.NewGuid();
            model.Created = DateTime.UtcNow;
        }

        model.Updated = DateTime.UtcNow;
        var filter = Builders<TModel>.Filter.Eq(x => x.Id, model.Id);
        Collection.ReplaceOne(filter, model, new ReplaceOptions { IsUpsert = true });
    }
    
    public void Remove(TModel spec)
    {
        if (spec == null)
            return;
        var builder = Builders<TModel>.Filter;
        var filter = builder.Eq(x => x.Id, spec.Id);

        Collection.DeleteOne(filter);
    }
}