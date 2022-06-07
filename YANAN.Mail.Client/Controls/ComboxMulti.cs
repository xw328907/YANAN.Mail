using YANAN.Mail.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace YANAN.Mail.Client.Controls
{
    /// <summary>
    /// Combox多选
    /// </summary>
    public class ComboBoxMulti : ComboBox
    {
        TreeView lst = new TreeView();

        public ComboBoxMulti()
        {
            this.DrawMode = DrawMode.OwnerDrawFixed;//只有设置这个属性为OwnerDrawFixed才可能让重画起作用
            lst.KeyUp += new KeyEventHandler(lst_KeyUp);
            lst.MouseUp += new MouseEventHandler(lst_MouseUp);
            // lst.KeyDown += new KeyEventHandler(lst_KeyDown);
            lst.Leave += new EventHandler(lst_Leave);
            lst.CheckBoxes = true;
            lst.ShowLines = false;
            lst.ShowPlusMinus = false;
            lst.ShowRootLines = false;
            this.DropDownHeight = 1;
        }

        void lst_Leave(object sender, EventArgs e)
        {
            lst.Hide();
        }

        #region Property

        [Description("选定项的值"), Category("Data")]
        public List<TreeNode> SelectedItems
        {
            get
            {
                List<TreeNode> lsttn = new List<TreeNode>();
                foreach (TreeNode tn in lst.Nodes)
                {
                    if (tn.Checked)
                    {
                        lsttn.Add(tn);
                    }
                }
                return lsttn;
            }
        }

        /// <summary>
        /// 数据源
        /// </summary>
        [Description("数据源"), Category("Data")]
        public object DataSource
        {
            get;
            set;
        }
        /// <summary>
        /// 显示字段
        /// </summary>
        [Description("显示字段"), Category("Data")]
        public string DisplayFiled
        {
            get;
            set;
        }
        /// <summary>
        /// 值字段
        /// </summary>
        [Description("值字段"), Category("Data")]
        public string ValueFiled
        {
            get;
            set;
        }
        #endregion

        public void DataBind()
        {
            this.BeginUpdate();
            if (DataSource != null)
            {
                if (DataSource is IDataReader)
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(DataSource as IDataReader);

                    DataBindToDataTable(dataTable);
                }
                else if (DataSource is DataView || DataSource is DataSet || DataSource is DataTable)
                {
                    DataTable dataTable = null;

                    if (DataSource is DataView)
                    {
                        dataTable = ((DataView)DataSource).ToTable();
                    }
                    else if (DataSource is DataSet)
                    {
                        dataTable = ((DataSet)DataSource).Tables[0];
                    }
                    else
                    {
                        dataTable = ((DataTable)DataSource);
                    }

                    DataBindToDataTable(dataTable);
                }
                else if (DataSource is IEnumerable)
                {
                    DataBindToEnumerable((IEnumerable)DataSource);
                }
                else
                {
                    throw new Exception("DataSource doesn't support data type: " + DataSource.GetType().ToString());
                }
            }
            else
            {
                lst.Nodes.Clear();
            }

            lst.ItemHeight = this.ItemHeight;
            lst.BorderStyle = BorderStyle.FixedSingle;
            lst.Size = new Size(this.DropDownWidth, this.ItemHeight * (this.MaxDropDownItems - 1) - (int)this.ItemHeight / 2);
            lst.Location = new Point(this.Left, this.Top + this.ItemHeight + 6);
            this.Parent.Controls.Add(lst);
            lst.Hide();
            this.EndUpdate();
        }

        private void DataBindToDataTable(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tn = new TreeNode();
                if (!string.IsNullOrEmpty(DisplayFiled) && !string.IsNullOrEmpty(ValueFiled))
                {
                    tn.Text = dr[DisplayFiled].ToString();
                    tn.Tag = dr[ValueFiled].ToString();
                }
                else if (string.IsNullOrEmpty(ValueFiled))
                {
                    tn.Text = dr[DisplayFiled].ToString();
                    tn.Tag = dr[DisplayFiled].ToString();
                }
                else if (string.IsNullOrEmpty(DisplayFiled))
                {
                    tn.Text = dr[ValueFiled].ToString();
                    tn.Tag = dr[ValueFiled].ToString();
                }
                else
                {
                    throw new Exception("ValueFiled和DisplayFiled至少保证有一项有值");
                }

                tn.Checked = false;
                lst.Nodes.Add(tn);
            }
        }

        /// <summary>
        /// 绑定到可枚举类型
        /// </summary>
        /// <param name="enumerable">可枚举类型</param>
        private void DataBindToEnumerable(IEnumerable enumerable)
        {
            IEnumerator enumerator = enumerable.GetEnumerator();
            while (enumerator.MoveNext())
            {
                object currentObject = enumerator.Current;
                lst.Nodes.Add(CreateListItem(currentObject));
            }
        }

        private TreeNode CreateListItem(object obj)
        {
            TreeNode item = new TreeNode();

            if (obj is string)
            {
                item.Text = obj.ToString();
                item.Tag = obj.ToString();
            }
            else
            {
                if (DisplayFiled != "")
                {
                    item.Text = GetPropertyValue(obj, DisplayFiled);
                }
                else
                {
                    item.Text = obj.ToString();
                }

                if (ValueFiled != "")
                {
                    item.Tag = GetPropertyValue(obj, ValueFiled);
                }
                else
                {
                    item.Tag = obj.ToString();
                }
            }
            return item;
        }

        private string GetPropertyValue(object obj, string propertyName)
        {
            object result = null;

            result = ObjectUtil.GetPropertyValue(obj, propertyName);
            return result == null ? String.Empty : result.ToString();
        }

        #region override

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            bool Pressed = (e.Control && ((e.KeyData & Keys.A) == Keys.A));
            if (Pressed)
            {
                this.Text = "";
                for (int i = 0; i < lst.Nodes.Count; i++)
                {
                    lst.Nodes[i].Checked = true;
                    if (this.Text != "")
                    {
                        this.Text += ",";
                    }
                    this.Text += lst.Nodes[i].Tag;
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.DroppedDown = false;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.DroppedDown = false;
            lst.Focus();
        }

        protected override void OnDropDown(EventArgs e)
        {
            string strValue = this.Text;
            if (!string.IsNullOrEmpty(strValue))
            {
                List<string> lstvalues = strValue.Split(',').ToList<string>();
                foreach (TreeNode tn in lst.Nodes)
                {
                    if (tn.Checked && !lstvalues.Contains(tn.Tag.ToString()) && !string.IsNullOrEmpty(tn.Tag.ToString().Trim()))
                    {
                        tn.Checked = false;
                    }
                    else if (!tn.Checked && lstvalues.Contains(tn.Tag.ToString()) && !string.IsNullOrEmpty(tn.Tag.ToString().Trim()))
                    {
                        tn.Checked = true;
                    }
                }
            }

            lst.Show();

        }

        #endregion override

        private void lst_KeyUp(object sender, KeyEventArgs e)
        {
            this.OnKeyUp(e);
        }

        private void lst_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                this.Text = "";
                for (int i = 0; i < lst.Nodes.Count; i++)
                {
                    if (lst.Nodes[i].Checked)
                    {
                        if (this.Text != "")
                        {
                            this.Text += ",";
                        }
                        this.Text += lst.Nodes[i].Tag;
                    }
                }
            }
            catch
            {
                this.Text = "";
            }
            bool isControlPressed = (Control.ModifierKeys == Keys.Control);
            bool isShiftPressed = (Control.ModifierKeys == Keys.Shift);
            if (isControlPressed || isShiftPressed)
                lst.Show();
            else
                lst.Hide();
        }

    }

}
