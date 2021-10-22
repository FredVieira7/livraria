using Livraria.Domain.Entidades;
using Livraria.Infra.Settings;
using MongoDB.Driver;

namespace Livraria.Infra.Data.DataContexts
{
    public class DataContext
    {
        public IMongoCollection<Livro> LivrosDb { get; set; }

        public DataContext(AppSettings appSettings)
        {
            var client = new MongoClient(appSettings.ConnectionString);
            var database = client.GetDatabase(appSettings.DatabaseName);
            
            LivrosDb = database.GetCollection<Livro>(appSettings.CollectionName);
            
        }
    }
}