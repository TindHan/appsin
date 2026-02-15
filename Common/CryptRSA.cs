using System.Security.Cryptography;
using System.Text;

namespace appsin.Common
{
    public class CryptRSA
    {
        /// <summary>
        /// 在C#中，RSACryptoServiceProvider的KeySize属性可以设置为任意偶数位的值，
        /// 建议使用2048位或更长的密钥长度以提供足够的安全性，典型情况下，RSA密钥的长度为2048位。
        /// 尽管在理论上可以使用较小的密钥长度来加速RSA操作，但是使用较小的密钥可能会使RSA易受到攻击，
        /// 因此不推荐使用少于2048位的密钥长度。为了确保数据的安全，应该使用更长的密钥(根据实际需求选择)，并定期更换密钥。
        /// ----------------------------
        /// RSA验证签名的作用是确保接收到的数据没有经过篡改，并且确实是由发送方发送的。
        /// 在数字通信中，攻击者可能会截取通信并修改消息内容，然后将其发送给接收方，导致接收方无法正确解释消息或采取错误的行动。
        /// 通过使用RSA签名，发送方可以对消息进行数字签名，这个数字签名是通过使用发送方的私钥生成的。
        /// 接收方可以使用发送方的公钥来验证数字签名，如果验证成功，就意味着消息没有被篡改，并且确实是由发送方发送的。
        /// 如果验证失败，接收方就可以确定消息已被篡改或不是由发送方发送的。
        /// 该过程确保了消息的完整性和真实性，防止了中间人攻击。因此，在数字通信中，RSA验证签名是一种非常重要的安全机制。
        /// ----------------------------
        /// 【注意】：为了与jsencrypt.min.js互换数据，需要将生成的 publicKey 和 privateKey 中的 = 去除
        /// </summary>
        public class RsaHelper
        {
            /// <summary>
            /// 生成公钥和私钥
            /// </summary>
            /// <param name="keySize">密钥大小</param>
            /// <param name="privateKey">输出私钥</param>
            /// <param name="publicKey">输出公钥</param>
            public static void GenerateKeys( out string privateKey, out string publicKey)
            {
                using (var rsa = new RSACryptoServiceProvider(512))
                {
                    privateKey = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
                    publicKey = Convert.ToBase64String(rsa.ExportRSAPublicKey());
                }
            }

            /// <summary>
            /// 加密
            /// </summary>
            /// <param name="publicKey">公钥</param>
            /// <param name="data">要加密的数据</param>
            /// <returns>加密后的数据</returns>
            public static string Encrypt(string publicKey, string data)
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
                    var bytes = Encoding.UTF8.GetBytes(data);
                    var encryptedBytes = rsa.Encrypt(bytes, RSAEncryptionPadding.Pkcs1);
                    return Convert.ToBase64String(encryptedBytes);
                }
            }

            /// <summary>
            /// 解密
            /// </summary>
            /// <param name="privateKey">私钥</param>
            /// <param name="data">要解密的数据</param>
            /// <returns>解密后的数据</returns>
            public static string Decrypt(string privateKey, string data)
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
                    var encryptedBytes = Convert.FromBase64String(data);
                    var decryptedBytes = rsa.Decrypt(encryptedBytes, RSAEncryptionPadding.Pkcs1);
                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }

            /// <summary>
            /// RSA私钥签名
            /// </summary>
            /// <param name="privateKey">私钥</param>
            /// <param name="data">要签名的数据</param>
            /// <returns>签名数据</returns>
            public static string SignData(string privateKey, string data)
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportRSAPrivateKey(Convert.FromBase64String(privateKey), out _);
                    var bytes = Encoding.UTF8.GetBytes(data);
                    var signatureBytes = rsa.SignData(bytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                    return Convert.ToBase64String(signatureBytes);
                }
            }

            /// <summary>
            /// 验证RSA签名
            /// </summary>
            /// <param name="publicKey">公钥</param>
            /// <param name="data">原始数据</param>
            /// <param name="signatureData">签名数据</param>
            /// <returns></returns>
            public static bool VerifyData(string publicKey, string data, string signatureData)
            {
                using (var rsa = new RSACryptoServiceProvider())
                {
                    rsa.ImportRSAPublicKey(Convert.FromBase64String(publicKey), out _);
                    var bytes = Encoding.UTF8.GetBytes(data);
                    var signatureBytes = Convert.FromBase64String(signatureData);
                    return rsa.VerifyData(bytes, signatureBytes, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                }
            }
        }
    }
}
