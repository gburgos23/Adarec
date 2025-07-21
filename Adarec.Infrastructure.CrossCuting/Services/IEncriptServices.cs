namespace Adarec.Infrastructure.CrossCuting.Services
{
    public interface IEncriptServices
    {
        string Decrypt(string srtDecrypt);
        string Encrypt(string srtEncrypt);
    }
}