namespace YANAN.Mail.Entity
{
    /// <summary>
    /// 下拉控件数据源项
    /// </summary>
    public class ListItem : object
    {
        /// <summary>
        /// 显示文本值
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public string Value { get; set; }
        /// <summary>
        /// 数据
        /// </summary>
        public object Tag { get; set; }

        public ListItem()
        {

        }
        public ListItem(string text, string value)
        {
            Text = text;
            Value = value;
        }

        public override string ToString()
        {
            return Text;
        }
    }
}
