
namespace YANAN.Mail.Utilities
{
    using Newtonsoft.Json;

    /// <summary>
    /// JSON序列化和反序列化
    /// </summary>
    public class JsonSerializationHelper
    {
        /// <summary>
        /// 将C#数据实体转化为JSON数据 Newtonsoft.Json(JSON.NET)；
        /// 默认规则：排除Null属性
        /// </summary>
        /// <param name="obj">要转化的数据实体</param>
        /// <returns>JSON格式字符串</returns>
        public static string JsonSerialize(object obj)
        {
            return JsonSerialize(obj, null);
        }

        /// <summary>
        /// 将C#数据实体转化为JSON数据 Newtonsoft.Json(JSON.NET)
        /// </summary>
        /// <param name="obj">要转化的数据实体</param>
        /// <param name="setting"></param>
        /// <returns>JSON格式字符串</returns>
        public static string JsonSerialize(object obj, JsonSerializerSettings setting)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            if (setting == null) setting = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
            return JsonConvert.SerializeObject(obj, setting);
        }
        /// <summary>
        /// 将C#数据实体转化为JSON数据 Newtonsoft.Json(JSON.NET)
        /// </summary>
        /// <param name="obj">要转化的数据实体</param>
        /// <param name="setting"></param>
        /// <returns>JSON格式字符串</returns>
        public static string JsonSerialize(object obj, Formatting setting)
        {
            if (obj == null)
            {
                return null;

            }
            return JsonConvert.SerializeObject(obj, setting);
        }

        /// <summary>
        /// 将JSON数据转化为C#数据实体 Newtonsoft.Json(JSON.NET)
        /// </summary>
        /// <param name="json">符合JSON格式的字符串</param>
        /// <returns>T类型的对象</returns>
        public static T JsonDeserialize<T>(string json)
        {
            return JsonDeserialize<T>(json, null);
        }
        /// <summary>
        /// 将JSON数据转化为C#数据实体 Newtonsoft.Json(JSON.NET)
        /// </summary>
        /// <param name="json">符合JSON格式的字符串</param>
        /// <param name="setting"></param>
        /// <returns>T类型的对象</returns>
        public static T JsonDeserialize<T>(string json, JsonSerializerSettings setting)
        {
            if (string.IsNullOrWhiteSpace(json)) return default(T);
            if (setting == null)
                setting = new JsonSerializerSettings
                {
                    Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
                    {
                        //errors.Add(e.ErrorContext.Error.Message);
                        e.ErrorContext.Handled = true;
                    }
                };
            else
            {
                setting.Error = delegate (object sender, Newtonsoft.Json.Serialization.ErrorEventArgs e)
                {
                    e.ErrorContext.Handled = true;
                };
            }
            return JsonConvert.DeserializeObject<T>(json, setting);
        }
    }
}
