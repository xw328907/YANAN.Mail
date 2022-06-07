using System.Collections.Generic;

namespace YANAN.Mail.IServices
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;

    public interface IMailBoxService
    {
        #region MailBox

        /// <summary>
        /// 添加邮箱绑定；保存前如下逻辑判断控制：
        /// 1、检查邮箱配置是否正确，能否链接邮局正常收发邮件
        /// 2、判断用户邮箱是否重复(本应控制整个系统是否重复,目前仅判断单一用户名下是否重复)
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        ResponseResult AddMailBox(LoginedUserInfo loginInfo, MailBox mailBox);
        /// <summary>
        /// 更新邮箱
        /// </summary>
        /// <param name="mailBox"></param>
        /// <returns></returns>
        ResponseResult UpdateMailBox(LoginedUserInfo loginInfo, MailBox mailBox);
        /// <summary>
        /// 获取当前用户的邮箱列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        IList<MailBox> GetListMailBoxCurrentUser(LoginedUserInfo loginInfo);
        /// <summary>
        /// 删除邮箱，逻辑如下：
        /// 1、判断邮箱是否存在
        /// 2、只有邮箱所有者才可删除
        /// 3、执行删除
        /// 4、如当前删除邮箱为默认邮箱则再设置一个默认邮箱
        /// </summary>
        /// <param name="mailBoxId">邮箱id</param>
        /// <returns></returns>
        ResponseResult RemoveMailBox(LoginedUserInfo loginInfo, string mailBoxId);

        #endregion MailBox

        #region MailFolder

        /// <summary>
        /// 添加邮箱文件夹/修改文件夹名
        /// </summary>
        /// <param name="mailFolder">需添加的邮箱文件夹对象</param>
        /// <returns></returns>
        ResponseResult AddMailFolder(LoginedUserInfo loginInfo, MailFolder mailFolder);
        /// <summary>
        /// 删除邮箱文件夹，逻辑如下：
        /// 1、只能删除归属于自己的文件夹
        /// 2、只能删除自定义类型文件夹 MailType=0
        /// 3、存在子文件夹的不能删除
        /// 4、文件夹存在邮件的不能删除
        /// 5、删除文件夹
        /// </summary>
        /// <param name="folderId"></param>
        /// <returns></returns>
        ResponseResult RemoveMailFolder(LoginedUserInfo loginInfo, string folderId);
        /// <summary>
        /// 获取邮箱下所有文件夹
        /// </summary>
        /// <param name="mailboxIds">邮箱ID编号数组</param>
        /// <returns></returns>
        IList<MailFolder> GetListMailFolderByMailBoxIds(string[] mailboxIds);
        /// <summary>
        /// 设置邮箱文件夹全部邮件已读
        /// </summary>
        /// <param name="mailFolderId">邮件文件夹ID编号</param>
        /// <returns></returns>
        ResponseResult SetMailFolderRead(LoginedUserInfo loginInfo, string mailFolderId);

        /// <summary>
        /// 获取当前登录用户的客户列表(存在往来邮件的客户)
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        IList<MailFolder> GetListMailCustomer(LoginedUserInfo loginInfo);

        #endregion MailFolder
    }
}
