using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Synevyr.Infrastructure;

public class Entity
{
    protected Entity() { }
    protected Entity(Guid id)
    {
        this.Id = id;
    }

    [BsonId(IdGenerator = typeof(CombGuidGenerator))]
    public Guid Id { get; set; }
    [BsonElement]
    public DateTime Created { get; set; }
    [BsonElement]
    public DateTime Updated { get; set; }

    public bool Equals(Entity other)
    {
        if (ReferenceEquals(null, other)) return false;
        return ReferenceEquals(this, other) || other.Id.Equals(Id);
    }


}