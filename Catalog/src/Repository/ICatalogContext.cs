using Catalog.Api.Domain;
using MongoDB.Driver;

namespace Catalog.Api.Repository
{
    public interface ICatalogContext
    {
        IMongoCollection<Product> Products { get; }
    }
}