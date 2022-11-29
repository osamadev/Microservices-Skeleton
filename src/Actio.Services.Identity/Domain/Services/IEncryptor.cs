namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncryptor
    {
         string GetSalt();
         (string, string) GetHash(string password);

         string GetHash(string password, string saltString);
    }
}