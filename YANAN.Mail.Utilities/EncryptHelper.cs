using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace YANAN.Mail.Utilities
{
    /// <summary>
    /// 加解密帮助类
    /// </summary>
    public class EncryptHelper
    {
        /// <summary>
        /// MD5 hash加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string MD5hash(string s)
        {
            var md5 = new MD5CryptoServiceProvider();
            var result = BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(s.Trim())));
            return result;
        }
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string MD5(string input)
        {
            return MD5(input, new UTF8Encoding());
        }

        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string MD5(string input, Encoding encode)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] t = md5.ComputeHash(encode.GetBytes(input));
            StringBuilder sb = new StringBuilder(32);
            foreach (byte t1 in t)
                sb.Append(t1.ToString("x2"));
            return sb.ToString();

        }
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <returns>返回40位UTF8 大写</returns>  
        public static string SHA1(string content)
        {
            return SHA1(content, Encoding.UTF8);
        }
        /// <summary>  
        /// SHA1 加密，返回大写字符串  
        /// </summary>  
        /// <param name="content">需要加密字符串</param>  
        /// <param name="encode">指定加密编码</param>  
        /// <returns>返回40位大写字符串</returns>  
        public static string SHA1(string content, Encoding encode)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(content)) return string.Empty;
                SHA1 sha1 = new SHA1CryptoServiceProvider();
                byte[] bytes_in = encode.GetBytes(content);
                byte[] bytes_out = sha1.ComputeHash(bytes_in);
                sha1.Dispose();
                string result = BitConverter.ToString(bytes_out);
                result = result.Replace("-", "");
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("SHA1加密出错：" + ex.Message);
            }
        }

        /// <summary>
        /// 双重加密(DES加密+MD5) 暂用于密码加密
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static string EncryptPassword(string val)
        {
            return MD5(EncodeDES(val));
        }

        #region DES加密解密

        private const string _encryptKey = "Wzh123$%";
        /// <summary>
        /// DES加密字符串
        /// </summary>
        /// <param name="encryptString">待加密的字符串</param>
        /// <param name="encryptKey">加密密钥,要求为8位</param>
        /// <returns>加密成功返回加密后的字符串,失败返回源串</returns>
        public static string EncodeDES(string encryptString, string encryptKey = _encryptKey)
        {
            if (string.IsNullOrWhiteSpace(encryptString)) return string.Empty;
            encryptKey = encryptKey.Substring(0, 8);
            encryptKey = encryptKey.PadRight(8, ' ');
            byte[] inputByteArray = Encoding.Default.GetBytes(encryptString);
            //建立加密对象的密钥和偏移量   
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.ASCII.GetBytes(encryptKey),
                IV = Encoding.ASCII.GetBytes(encryptKey)
            };
            var ms = new MemoryStream();
            var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            var ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();

        }

        /// <summary>
        /// DES解密字符串
        /// </summary>
        /// <param name="decryptString">待解密的字符串</param>
        /// <param name="decryptKey">解密密钥,要求为8位,和加密密钥相同</param>
        /// <returns>解密成功返回解密后的字符串,失败返源串</returns>
        public static string DecodeDES(string decryptString, string decryptKey = _encryptKey)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(decryptString)) return string.Empty;
                decryptKey = decryptKey.Substring(0, 8);
                decryptKey = decryptKey.PadRight(8, ' ');
                var des = new DESCryptoServiceProvider();
                var inputByteArray = new byte[decryptString.Length / 2];
                for (var x = 0; x < decryptString.Length / 2; x++)
                {
                    var i = (Convert.ToInt32(decryptString.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                //建立加密对象的密钥和偏移量 
                des.Key = Encoding.ASCII.GetBytes(decryptKey);
                des.IV = Encoding.ASCII.GetBytes(decryptKey);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return string.Empty;
            }

        }
        #endregion

        #region Base64加解码
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encode">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string EncodeBase64(Encoding encode, string source)
        {
            if (string.IsNullOrWhiteSpace(source)) return string.Empty;
            string code;
            byte[] bytes = encode.GetBytes(source);
            try
            {
                code = Convert.ToBase64String(bytes);
            }
            catch
            {
                code = source;
            }
            return code;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64(string source)
        {
            return EncodeBase64(Encoding.UTF8, source);
        }
        /// <summary>
        /// Base64加密，采用GBK编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string EncodeBase64GBK(string source)
        {
            return EncodeBase64(Encoding.GetEncoding("GBK"), source);
        }


        /// <summary>
        /// Base64解密，采用utf8编码方式解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(string result)
        {
            return DecodeBase64(Encoding.UTF8, result);
        }
        /// <summary>
        /// Base64解密 GBK编码格式
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64GBK(string result)
        {
            return DecodeBase64(Encoding.GetEncoding("GBK"), result);
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="encode">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string DecodeBase64(Encoding encode, string result)
        {
            if (string.IsNullOrWhiteSpace(result)) return string.Empty;
            string decode = "";
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encode.GetString(bytes);
            }
            catch
            {
                decode = result;
            }
            return decode;
        }



        #endregion Base64加解码
    }
}
