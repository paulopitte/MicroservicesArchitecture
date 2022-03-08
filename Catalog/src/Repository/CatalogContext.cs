using Catalog.Api.Domain;
using MongoDB.Driver;

namespace Catalog.Api.Repository
{
    public class CatalogContext : ICatalogContext
    {

      private  const string DATABASESETTINGS = "DatabaseSettings";

        public CatalogContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<String>
                ($"{ DATABASESETTINGS }:ConnectionString"));

            var database = client.GetDatabase(configuration.GetValue<string>($"{DATABASESETTINGS}:DatabaseName"));

            Products = database.GetCollection<Product>(configuration.GetValue<string>($"{DATABASESETTINGS}:CollectionName"));

            CatalogContextSeed.SeedData(Products);

        }
        public IMongoCollection<Product> Products { get; }
    }
}
