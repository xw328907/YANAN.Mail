using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    public static class ControlHelper
    {
        /// <summary>
        /// 关闭窗体
        /// </summary>
        /// <param name="form"></param>
        public static void CloseForm(this Form form)
        {
            form.Close();
            //if (!form.IsDisposed) form.Dispose();//不能释放,如果是ShowDialog则可能导致父窗体不是最前
        }
        /// <summary>
        /// 关闭窗体并退出系统
        /// </summary>
        /// <param name="form"></param>
        public static void CloseAndExit(this Form form)
        {
            form.Close();
            if (!form.IsDisposed) form.Dispose();
            ExitSystem();
        }
        /// <summary>
        /// 退出系统
        /// </summary>
        public static void ExitSystem(Form form = null)
        {
            if (form != null && !form.IsDisposed) form.Dispose();
            Application.ExitThread();
            Application.Exit();
            Environment.Exit(0);
        }
        /// <summary>
        /// 为ComboBox绑定数据源并提供下拉提示
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="combox">ComboBox</param>
        /// <param name="list">数据源</param>
        /// <param name="displayMember">显示字段</param>
        /// <param name="valueMember">隐式字段</param>
        /// <param name="displayText">下拉提示文字</param>
        public static void Bind<T>(this ComboBox combox, IList<T> list, string displayMember = "Text", string valueMember = "Value", string displayText = null)
        {
            AddItem(list, displayMember, displayText);
            combox.DataSource = list;
            combox.DisplayMember = displayMember;
            if (!string.IsNullOrEmpty(valueMember))
                combox.ValueMember = valueMember;
        }
        private static void AddItem<T>(IList<T> list, string displayMember, string displayText)
        {
            if (displayText == null)
                return;
            Object _obj = Activator.CreateInstance<T>();
            Type _type = _obj.GetType();
            if (!string.IsNullOrEmpty(displayMember))
            {
                PropertyInfo _displayProperty = _type.GetProperty(displayMember);
                _displayProperty.SetValue(_obj, displayText, null);
            }
            list.Insert(0, (T)_obj);
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class TreeNodeTag
    {
        public string NodeType { get; set; }
        public object Data { get; set; }
    }
}
