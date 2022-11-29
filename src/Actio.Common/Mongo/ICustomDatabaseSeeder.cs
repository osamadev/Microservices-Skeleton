using System.Threading.Tasks;

namespace Actio.Common.Mongo
{
    public interface ICustomDatabaseSeeder
    {
       Task SeedDatabase<TDocument>();
    }
}