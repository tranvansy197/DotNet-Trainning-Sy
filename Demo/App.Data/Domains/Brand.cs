using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace App.Api.Domains;

public class Brand
{
    [BsonId]
    [BsonRepresentation(BsonType.Int64)]
    public long Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; } = null!;
}