using Adarec.Infrastructure.CrossCuting.Services;
using System.Security.Cryptography;
using System.Text;

namespace Adarec.Infrastructure.CrossCuting.ServicesImpl
{
    public class EncriptServicesImpl : IEncriptServices
    {
        private static readonly string Key = Environment.GetEnvironmentVariable("encryptKey")!;

        public string Encrypt(string srtEncrypt)
        {
            try
            {
                byte[] keyArray;
                byte[] Arreglo_a_Cifrar = Encoding.UTF8.GetBytes(srtEncrypt);
                keyArray = MD5.HashData(Encoding.UTF8.GetBytes(Key));

                byte[] ArrayResultado;

                using (TripleDES tdes = TripleDES.Create())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tdes.CreateEncryptor();
                    ArrayResultado = cTransform.TransformFinalBlock(Arreglo_a_Cifrar, 0, Arreglo_a_Cifrar.Length);
                }

                return Convert.ToBase64String(ArrayResultado, 0, ArrayResultado.Length);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante el cifrado: {ex.Message}");
            }
        }

        public string Decrypt(string srtDecrypt)
        {
            try
            {
                byte[] keyArray;
                byte[] Array_a_Descifrar = Convert.FromBase64String(srtDecrypt);
                keyArray = MD5.HashData(Encoding.UTF8.GetBytes(Key));

                byte[] resultArray;


                using (TripleDES tdes = TripleDES.Create())
                {
                    tdes.Key = keyArray;
                    tdes.Mode = CipherMode.ECB;
                    tdes.Padding = PaddingMode.PKCS7;
                    ICryptoTransform cTransform = tdes.CreateDecryptor();
                    resultArray = cTransform.TransformFinalBlock(Array_a_Descifrar, 0, Array_a_Descifrar.Length);
                }
                return Encoding.UTF8.GetString(resultArray);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error durante el descifrado: {ex.Message}");
            }
        }
    }
}
