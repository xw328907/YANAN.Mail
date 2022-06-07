using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace YANAN.Mail.Utilities
{
    public class UtilityHelper
    {
        /// <summary>
        /// 邮件附件存储的根目录,最后不带\
        /// </summary>
        public static string MailAttachBaseDirectory
        {
            get
            {
                return AssemblyHelper.GetBaseDirectory() + "Storage/Attach";
            }
        }
        /// <summary>
        /// 生成32位无符号GUID
        /// </summary>
        /// <returns></returns>
        public static string GetGuid()
        {
            return Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// 生成36位标准GUID 
        /// </summary>
        /// <returns></returns>
        public static string GetGuid36()
        {
            return Guid.NewGuid().ToString();
        }
        /// <summary>  
        /// 根据GUID获取16位的唯一字符串  
        /// </summary>  
        /// <param name=\"guid\"></param>  
        /// <returns></returns>  
        public static string GuidTo16String()
        {
            long i = 1;
            foreach (byte b in Guid.NewGuid().ToByteArray())
                i *= ((int)b + 1);
            return string.Format("{0:x}", i - DateTime.Now.Ticks);
        }
        /// <summary>  
        /// 根据GUID获取19位的唯一数字序列
        /// </summary>  
        /// <returns></returns>
        public static long GuidToLongID()
        {
            byte[] buffer = Guid.NewGuid().ToByteArray();
            return BitConverter.ToInt64(buffer, 0);
        }
        /// <summary>
        /// 唯一单据号生成(20位) yyyyMMddHHmmssfff+3位随机数
        /// </summary>
        /// <returns></returns>
        public static string GenerateOrderNumber()
        {
            string strDateTimeNumber = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string strRandomResult = NextRandom(3).ToString();
            return strDateTimeNumber + strRandomResult;
        }
        /// <summary>
        /// 生成随机数
        /// </summary>
        /// <param name="len">随机数位数,不能超过10位</param>
        /// <returns></returns>
        public static int NextRandom(int len)
        {
            return NextRandomByGuid(len);
        }

        /// <summary>
        /// 根据Guid生成随机数
        /// </summary>
        /// <param name="len">随机数位数,不能超过10位</param>
        /// <returns></returns>
        public static int NextRandomByGuid(int len)
        {
            int min = len < 10 ? int.Parse("1".PadRight(len, '0')) : 1000000000;
            int max = len < 10 ? int.Parse("9".PadRight(len, '9')) : 2147483647;
            return new Random(GetRandomSeedbyGuid()).Next(min, max);
        }
        /// <summary>
        /// 使用Guid生成种子
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeedbyGuid()
        {
            return Guid.NewGuid().GetHashCode();
        }
        /// <summary>
        /// 根据RNGCryptoServiceProvider生成随机数
        /// </summary>
        /// <param name="len">随机数位数,不能超过10位</param>
        /// <returns></returns>
        public static int NextRandomByRNG(int len)
        {
            int min = len < 10 ? int.Parse("1".PadRight(len, '0')) : 1000000000;
            int max = len < 10 ? int.Parse("9".PadRight(len, '9')) : 2147483647;
            return new Random(GetRandomSeedByRNG()).Next(min, max);
        }
        /// <summary>
        /// 使用RNGCryptoServiceProvider生成种子
        /// </summary>
        /// <returns></returns>
        public static int GetRandomSeedByRNG()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rngCrypto = new RNGCryptoServiceProvider();
            rngCrypto.GetBytes(randomBytes);
            return BitConverter.ToInt32(randomBytes, 0);
        }

        /// <summary>
        /// 是否图片格式
        /// </summary>
        /// <param name="fileType">后缀名类型</param>
        /// <returns></returns>
        public static bool IsImageType(string fileType)
        {
            const string strFilter = ".jpeg|.gif|.jpg|.png|.bmp|.pic|.tiff|.ico|.iff|.lbm|.mag|.mac|.mpt|.opt|";
            return strFilter.IndexOf("." + fileType.ToLower(), StringComparison.Ordinal) != -1;
        }

        #region 文字替换
        static Dictionary<char, char> charMaping = new Dictionary<char, char>()
                                                {
                                                    {'A','龘'},
                                                    {'B','杩'},
                                                    {'C','锝'},
                                                    {'D','铴'},
                                                    {'E','糨'},
                                                    {'F','澉'},
                                                    {'G','鼽'},
                                                    {'H','肟'},
                                                    {'I','锿'},
                                                    {'J','旎'},
                                                    {'K','亟'},
                                                    {'L','欹'},
                                                    {'M','皛'},
                                                    {'N','螬'},
                                                    {'O','麤'},
                                                    {'P','灏'},
                                                    {'Q','薅'},
                                                    {'R','龉'},
                                                    {'S','鹨'},
                                                    {'T','禚'},
                                                    {'U','擢'},
                                                    {'V','阏'},
                                                    {'W','罨'},
                                                    {'X','酽'},
                                                    {'Y','魇'},
                                                    {'Z','鼹'},

                                                    {'a','龘'},
                                                    {'b','杩'},
                                                    {'c','锝'},
                                                    {'d','铴'},
                                                    {'e','糨'},
                                                    {'f','澉'},
                                                    {'g','鼽'},
                                                    {'h','肟'},
                                                    {'i','锿'},
                                                    {'j','旎'},
                                                    {'k','亟'},
                                                    {'l','欹'},
                                                    {'m','皛'},
                                                    {'n','螬'},
                                                    {'o','麤'},
                                                    {'p','灏'},
                                                    {'q','薅'},
                                                    {'r','龉'},
                                                    {'s','鹨'},
                                                    {'t','禚'},
                                                    {'u','擢'},
                                                    {'v','阏'},
                                                    {'w','罨'},
                                                    {'x','酽'},
                                                    {'y','魇'},
                                                    {'z','鼹'},

                                                    {'0','雩'},
                                                    {'1','罴'},
                                                    {'2','淠'},
                                                    {'3','鼙'},
                                                    {'4','赕'},
                                                    {'5','疸'},
                                                    {'6','锛'},
                                                    {'7','疋'},
                                                    {'8','懋'},
                                                    {'9','鼐'},

                                                    {'~','獿'},
                                                    {'!','犫'},
                                                    {'@','鬻'},
                                                    {'#','瀣'},
                                                    {'$','翥'},
                                                    {'%','圝'},
                                                    {'^','橐'},
                                                    {'&','蔺'},
                                                    {'*','爨'},
                                                    {'(','硇'},
                                                    {')','耪'},
                                                    {'_','鲞'},
                                                    {'-','隰'},
                                                    {'=','鼍'},
                                                    {'+','薹'},
                                                    {'[','鼋'},
                                                    {']','簪'},
                                                    {'{','髭'},
                                                    {'}','罅'},
                                                    {'"','衤'},
                                                    {'\'','厶'},
                                                    {';','彐'},
                                                    {':','彡'},
                                                    {'<','凵'},
                                                    {'>','壐'},
                                                    {',','壜'},
                                                    {'.','壣'},
                                                    {'?','嚹'},
                                                    {'/','亸'},
                                                    {'\\','嗭'},

                                                    {'￣','嚂'},
                                                    {'！','爣'},
                                                    {'＠','牗'},
                                                    {'＃','玂'},
                                                    {'￥','爩'},
                                                    {'％','獩'},
                                                    {'…','嬺'},
                                                    {'＆','牅'},
                                                    {'×','牋'},
                                                    {'（','牍'},
                                                    {'）','牎'},
                                                    {'—','灟'},
                                                    {'－','牖'},
                                                    {'＝','犡'},
                                                    {'＋','犤'},
                                                    {'【','孴'},
                                                    {'】','孻'},
                                                    {'｛','幰'},
                                                    {'｝','戃'},
                                                    {'”','灪'},
                                                    {'’','漐'},
                                                    {'；','濷'},
                                                    {'：','槬'},
                                                    {'《','欎'},
                                                    {'》','曧'},
                                                    {'，','攕'},
                                                    {'。','攡'},
                                                    {'？','犥'},
                                                    {'、','攚'},
                                                    {' ','哶'},
                                                    {'　','哶'}
                                                };

        public static string ConvertFullIndexString(string source)
        {
            return ConvertFullIndexString(new StringBuilder(source));
        }

        public static string ConvertFullIndexString(StringBuilder source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (charMaping.ContainsKey(source[i])) source[i] = charMaping[source[i]];
            }
            return source.ToString();
        }
        #endregion 文字替换

        public static string StripHtml(string html)
        {
            html = Regex.Replace(html, "(\\<script(.+?)\\</script\\>)|(\\<style(.+?)\\</style\\>)", "",
                                 RegexOptions.Singleline | RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @">[^>]+\[mailto:([^\""\]]*)\]", ">$1"); //替换类似[mailto:xx@xx.com]
            Regex reg = new Regex("<[^>]+>", RegexOptions.IgnoreCase);
            html = System.Web.HttpUtility.HtmlDecode(reg.Replace(html + "", " "));
            html = Regex.Replace(html, @"[\s　\n]+", " "); //合并空格(全角和半角)
            return html;
        }

        /// <summary>
        /// 转换成纯文本
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Nohtml(string str)
        {
            str = Regex.Replace(str, "<[^>]+>|</[^>]+>", "", RegexOptions.IgnoreCase);
            str = Regex.Replace(str, "\r|\n|\\s", "", RegexOptions.IgnoreCase);
            return str;
        }

        /// <summary>
        /// 根据长度转换成文件大小
        /// </summary>
        /// <param name="size">文件大小 单位：byte</param>
        /// <returns></returns>
        public static string ConvertFileSize(long? size)
        {
            if (size.HasValue == false) return "0";
            double leng = Convert.ToDouble(size);
            string[] sizes = { "Byte", "KB", "MB", "GB", "TB" };
            int order = 0;
            while (leng >= 1024 && order + 1 < sizes.Length)
            {
                order++;
                leng = leng / 1024;
            }
            return string.Format("{0:0.##} {1}", leng, sizes[order]);
        }
        /// <summary>
        /// 获取收件人/抄送发件人的名称
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        public static string GetMailReceiveCcName(string address)
        {
            string val = string.Empty;
            if (string.IsNullOrWhiteSpace(address)) return val;
            var splits = address.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string split in splits)
            {
                var e = Regex.Match(split, "<.*?>");//匹配abc<abc@qq.com>中的<abc@qq.com>
                string name = string.Empty; string email = string.Empty;
                if (e.Success)
                {
                    name = split.Replace(e.Value, "");
                    email = e.Value.Trim().Trim('<').Trim('>');
                }
                else
                {
                    email = split;
                }
                if (IsEmail(email))
                {
                    if (string.IsNullOrWhiteSpace(name)) name = email.Split('@')[0];
                    val += name + ",";
                }
            }
            return val.Trim(',');
        }
        /// <summary>
        /// 格式化输出收件人/抄送 列表Dictionary(邮箱，显示名)
        /// </summary>
        /// <param name="receiver"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetMailReceiverGroup(string receiver)
        {
            Dictionary<string, string> dicReceivers = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(receiver)) return dicReceivers;
            var splits = receiver.Split(new string[] { ";", "," }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string split in splits)
            {
                var e = Regex.Match(split, "<.*?>");//匹配abc<abc@qq.com>中的<abc@qq.com>
                string name = string.Empty; string email = string.Empty;
                if (e.Success)
                {
                    name = split.Replace(e.Value, "");
                    email = e.Value.Trim().Trim('<').Trim('>');
                }
                //if (split.IndexOf("<") > -1 && split.IndexOf(">") > -1)
                //{
                //    var s = split.Split('<');
                //    name = s[0];
                //    email = s[1].Replace(">", "");
                //}
                else
                {
                    email = split;
                }
                if (IsEmail(email))
                {
                    if (string.IsNullOrWhiteSpace(name))
                        name = email.Split('@')[0];
                    dicReceivers.Add(email, name);
                }
            }
            return dicReceivers;
        }
        /// <summary>
        /// 获取格式化的邮箱地址文本，如: 邮箱管理员&lt;admin@qq.com&gt; 
        /// Ps: &lt; &gt;为尖括号
        /// </summary>
        /// <param name="email">邮箱地址</param>
        /// <param name="displayName">显示名，为空则为邮箱地址</param>
        /// <returns></returns>
        public static string GetMailAddressText(string email, string displayName = "")
        {
            if (string.IsNullOrWhiteSpace(displayName)) displayName = email;
            return displayName + "<" + email + ">";
        }
        /// <summary>
        /// 是否为有效的邮箱地址
        /// </summary>
        /// <param name="email">需验证的邮箱</param>
        /// <returns>true  有效邮箱地址</returns>
        public static bool IsEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;
            return Regex.IsMatch(email, @"^\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$");
        }
        /// <summary>
        /// 参数无效
        /// </summary>
        /// <returns></returns>
        public static ResponseResult ReturnParameterNull()
        {
            return new ResponseResult
            {
                Code = Enums.ResponseCodeEnum.ERROR.ToString(),
                ErrorCode = Enums.ResponseErrorCodeEnum.ERROR_PARAMETER.ToString(),
                Message = "参数无效"
            };
        }
    }
}
