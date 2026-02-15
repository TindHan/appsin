using System.Security.Cryptography;
using System.Text;

namespace appsin.Common
{
    public class CryptAES
    {
        /// <summary>
        /// AES算法的keysize有一定限制。
        /// 具体来说，AES算法支持的keysize为 128 bits、192 bits 和 256 bits，而且只能以16 bits（即2个字节）为步长递增。
        /// 也就是说，支持的有效的 keysize 可以是：128、160、192、224 或 256。
        /// 需要注意的是，AES算法的 keysize 越大，加密强度越高，但同时也会增加加密运算所需的时间和计算资源。
        /// 因此，在实际应用中，需要根据实际需求和环境对 keysize 进行合理的选择。
        /// </summary>
        private static readonly int KeySize = 256;

        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Encrypt(string plainText)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.Mode = CipherMode.ECB; // ECB 模式无需 IV 向量
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String("TindRick123456789012345678901234");

                ICryptoTransform encryptor = aes.CreateEncryptor();

                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] cipherBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(cipherBytes);
            }
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            using (var aes = Aes.Create())
            {
                aes.KeySize = KeySize;
                aes.Mode = CipherMode.ECB; // ECB 模式无需 IV 向量
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Convert.FromBase64String("TindRick123456789012345678901234");

                ICryptoTransform decryptor = aes.CreateDecryptor();

                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                byte[] plainBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                return Encoding.UTF8.GetString(plainBytes);
            }
        }

        /// <summary>
        /// 对字符串进MD5加密
        /// </summary>
        /// <param name="sourceStr">源类型</param>
        /// <returns>加密后字符串</returns>
        public static string Md5Encrypt(string sourceStr)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            //将要加密的字符串转换成字节数组
            byte[] palindata = Encoding.Default.GetBytes(sourceStr);
            //通过字节数组进行加密
            byte[] encryptdata = md5.ComputeHash(palindata);
            //将加密后的字节数组转换成字符串
            string returnData = Convert.ToBase64String(encryptdata);
            return returnData;
        }
    }
}
