using MongoDB.Driver;

namespace Synevyr.Infrastructure;

public interface IRepository<TModel> where  TModel : Entity
{
    public IQueryable<TModel> AsQuaryable();
    public IMongoCollection<TModel> Collection { get;}
    public void Save(TModel model);
    public void Remove(TModel spec);
}