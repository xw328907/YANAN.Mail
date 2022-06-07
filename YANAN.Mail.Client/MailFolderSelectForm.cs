using YANAN.Mail.Entity;
using YANAN.Mail.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace YANAN.Mail.Client
{
    public partial class MailFolderSelectForm : BaseDialogForm
    {
        /// <summary>
        /// 选中的邮箱文件夹ID
        /// </summary>
        public string SelectedFolderId { get; set; }
        public MailFolderSelectForm(string mailBoxId = "")
        {
            InitializeComponent();
            toolBtn_ok.Click += new EventHandler(toolBtn_ok_Click);
            toolBtn_cancel.Click += new EventHandler(toolBtn_cancel_Click);
            LoadMailFolder(mailBoxId);
        }
        private void toolBtn_ok_Click(object sender, EventArgs e)
        {
            var node = tree_mailfolder.SelectedNode;
            if (node == null)
            {
                MessageBox.Show("请先选择文件夹", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                return;
            }
            SelectedFolderId = node.Name;
            DialogResult = DialogResult.OK;
        }
        private void toolBtn_cancel_Click(object sender, EventArgs e)
        {
            this.CloseForm();
        }
        private void LoadMailFolder(string mailBoxId)
        {
            tree_mailfolder.LabelEdit = false;
            tree_mailfolder.Nodes.Clear();
            if (!string.IsNullOrWhiteSpace(mailBoxId))
            {
                var folders = mailBoxService.GetListMailFolderByMailBoxIds(new string[] { mailBoxId });
                if (folders.Count > 0)
                {
                    Action<IList<MailFolder>, TreeNode> loadMailFolderTreeNodeAction = null;
                    List<TreeNode> nodes = new List<TreeNode>();
                    //邮箱文件夹加递归
                    loadMailFolderTreeNodeAction = (items, parentNode) =>
                    {
                        if (items == null || items.Count < 1) return;
                        List<MailFolder> childrens = new List<MailFolder>();
                        if (parentNode == null)
                        {
                            childrens = items.Where(x => string.IsNullOrWhiteSpace(x.ParentId)).ToList();
                        }
                        else
                        {
                            childrens = items.Where(x => x.ParentId == parentNode.Name).ToList();
                        }
                        if (childrens != null && childrens.Count() > 0)
                        {
                            foreach (var item in childrens)
                            {
                                TreeNode node_child = new TreeNode
                                {
                                    Name = item.MailFolderId,
                                    Tag = new TreeNodeTag { NodeType = MailBoxFolderTreeNodeTypeEnum.MailFolder.ToString(), Data = item },
                                    Text = item.FolderName
                                };
                                if (parentNode == null)
                                    nodes.Add(node_child);
                                else
                                    parentNode.Nodes.Add(node_child);
                                loadMailFolderTreeNodeAction(items, node_child);
                                SetMailFolderTreeNodeImageIcon(node_child, item.MailType);
                            }
                        }
                    };
                    loadMailFolderTreeNodeAction(folders, null);
                    tree_mailfolder.Nodes.AddRange(nodes.ToArray());
                }
            }
        }
        /// <summary>
        /// 设置树节点图标
        /// </summary>
        /// <param name="node"></param>
        /// <param name="mailType"></param>
        private void SetMailFolderTreeNodeImageIcon(TreeNode node, int mailType)
        {
            if (node.Nodes.Count > 0)
            {
                node.ImageKey = "collapse.png";
                if (node.IsExpanded)
                { node.ImageKey = "expand.png"; }
            }
            else
            {
                if (node.IsExpanded) node.Collapse();
                switch (mailType)
                {
                    case (int)MailFolderEnum.InBox:
                        node.ImageKey = "mail_receive_box.png";
                        break;
                    case (int)MailFolderEnum.OutBox:
                        node.ImageKey = "mail_send.png";
                        break;
                    case (int)MailFolderEnum.DraftBox:
                        node.ImageKey = "mail_draft.png";
                        break;
                    case (int)MailFolderEnum.TrashBox:
                        node.ImageKey = "mail_rubbish.png";
                        break;
                    case (int)MailFolderEnum.Deleted:
                        node.ImageKey = "mail_deleted.png";
                        break;
                    default:
                        node.ImageKey = "mail_unread.png";
                        break;
                }
            }
            node.SelectedImageKey = node.ImageKey;
        }
    }
}
