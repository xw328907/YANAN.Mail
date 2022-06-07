namespace YANAN.Mail.IServices
{
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;
    using System.Collections.Generic;

    public interface IMailService
    {
        #region 邮件
        /// <summary>
        /// 获取邮件列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="searchDto"></param>
        /// <returns></returns>
        EntityList<MailMain> GetListMailMain(LoginedUserInfo loginInfo, MailSearchDto searchDto);
        /// <summary>
        /// 获取邮件信息(含正文、附件信息)
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        MailMain GetMailMain(string mailId);

        /// <summary>
        /// 邮件删除
        /// </summary>
        /// <param name="mailIds"></param>
        /// <returns></returns>
        ResponseResult RemoveMail(LoginedUserInfo loginInfo, string[] mailIds);

        /// <summary>
        /// 设置邮件为已读/未读状态
        /// </summary>
        /// <param name="mailIds">邮件ID编号数组</param>
        /// <param name="read">阅读状态,true =设为已读（默认值）；false=设为未读</param>
        /// <returns></returns>
        ResponseResult SetMailReadStatus(string[] mailIds, bool read = true);

        /// <summary>
        /// 设置邮箱文件夹全部邮件已读
        /// </summary>
        /// <param name="mailFolderId">邮件文件夹ID编号</param>
        /// <returns></returns>
        ResponseResult SetMailFolderRead(LoginedUserInfo userInfo, string mailFolderId);

        /// <summary>
        /// 邮件发送(常规发送/转发/回复)
        /// </summary>
        /// <param name="mailDto"></param>
        /// <returns></returns>
        ResponseResult MailSend(LoginedUserInfo loginInfo, MailMain mailDto);

        /// <summary>
        /// 邮件保存,存草稿
        /// </summary>
        /// <param name="mailDto"></param>
        /// <returns></returns>
        ResponseResult MailSave(LoginedUserInfo loginInfo, MailMain mailDto);

        /// <summary>
        /// 移动邮件至文件夹,逻辑如下：
        /// 1、参数不能为空/null
        /// 2、收件不能移入发件箱/草稿箱
        /// 3、发件/草稿不能移入收件箱
        /// </summary>
        /// <param name="mailId">邮件ID</param>
        /// <param name="toMailFolderId">移入的文件夹ID</param>
        /// <returns></returns>
        ResponseResult MoveMailToFolder(string mailId, string toMailFolderId);
        /// <summary>
        /// 设置邮件置顶/取消置顶
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="isTop"></param>
        ResponseResult SetMailTopStatus(bool isTop = true, params string[] mailIds);

        /// <summary>
        /// 邮件以附件发送(生成邮件附件)
        /// </summary>
        /// <param name="mailId">要作为附件发送的邮件编号ID</param>
        /// <returns></returns>
        MailMain ComposeMailAsAttach(LoginedUserInfo loginInfo, string mailId);

        #endregion 邮件

        #region 邮箱联系人

        /// <summary>
        /// 获取当前用户的邮箱联系人列表
        /// </summary>
        /// <param name="query">查询条件,可不传</param>
        /// <returns></returns>
        EntityList<MailContact> GetListSelfMailContact(LoginedUserInfo loginInfo, QueryParameter parameter);
        /// <summary>
        /// 获取邮件联系人
        /// </summary>
        /// <param name="contactId">邮件联系人ID</param>
        /// <returns></returns>
        MailContact GetMailContact(int contactId);
        /// <summary>
        /// 保存邮箱联系人
        /// </summary>
        /// <param name="contactDto">联系人数据传输对象</param>
        /// <returns></returns>
        ResponseResult SaveMailContact(LoginedUserInfo loginInfo, MailContact contactDto);
        /// <summary>
        /// 删除邮件联系人，控制逻辑如下：
        /// 1、只能删除自己的
        /// </summary>
        /// <param name="ids">联系人编号ID数组</param>
        /// <returns></returns>
        ResponseResult RemoveMailContact(LoginedUserInfo loginInfo, int[] ids);

        /// <summary>
        /// 写邮件收件人(抄送密送)搜索下拉数据源
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <param name="keyword">输入的关键词</param>
        /// <returns></returns>
         List<ListItem> GetComposeMailContactsList(LoginedUserInfo loginInfo, string keyword);

        #endregion 邮箱联系人

        #region 客户联系人

        /// <summary>
        /// 获取客户联系人列表(当前企业当前用户下所有)
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        void SyncCustomerContacts(LoginedUserInfo loginInfo);
        /// <summary>
        /// 获取当前用户的客户联系人列表
        /// </summary>
        /// <param name="loginInfo"></param>
        /// <returns></returns>
        List<CustomerContacts> GetCustomerContactsList(LoginedUserInfo loginInfo);

        /// <summary>
        /// 客户邮件归档
        /// </summary>
        /// <param name="loginInfo">登录用户信息</param>
        /// <param name="customerContactsList">需为同一客户的联系人信息</param>
        /// <returns></returns>
        ResponseResult ArchiveCustomerMail(LoginedUserInfo loginInfo, List<CustomerContacts> customerContactsList);

        #endregion 客户联系人

        #region 邮箱签名/模板

        /// <summary>
        /// 获取当前用户的邮箱签名列表，max = 999
        /// </summary>
        /// <param name="signType">类型：1=邮箱签名,默认值；2=模板</param>
        /// <returns></returns>
        IList<MailSignature> GetListSelfMailSign(LoginedUserInfo loginInfo, string mailBoxId = "", int signType = 1);
        /// <summary>
        /// 保存邮箱签名/模板，注意：参数MailSignature.SignType必须赋值,否则保存后无法取值
        /// </summary>
        /// <param name="signDto"></param>
        /// <returns></returns>
        ResponseResult SaveMailSign(LoginedUserInfo loginInfo, MailSignature signDto);
        /// <summary>
        /// 邮件签名删除
        /// </summary>
        /// <param name="signId"></param>
        /// <returns></returns>
        ResponseResult RemoveMailSign(LoginedUserInfo loginInfo, int signId);
        #endregion 邮箱签名/模板

        #region 邮箱拒收/黑名单

        /// <summary>
        /// 获取当前用户的邮箱拒收/黑名单列表
        /// </summary>
        /// <param name="query">查询条件,可不传</param>
        /// <returns></returns>
        EntityList<MailJudge> GetListSelfMailJudge(LoginedUserInfo loginInfo, QueryParameter parameter);
        /// <summary>
        /// 保存邮箱拒收/黑名单，逻辑如下：
        /// 1、只能修改自己的
        /// </summary>
        /// <param name="modelDto">数据传输对象</param>
        /// <returns></returns>
        ResponseResult SaveMailJudge(LoginedUserInfo loginInfo, MailJudge modelDto);
        /// <summary>
        /// 删除邮件黑名单/拒收，控制逻辑如下：
        /// 1、只能删除自己的
        /// </summary>
        /// <param name="ids">黑名单编号ID数组</param>
        /// <returns></returns>
        ResponseResult RemoveMailJudge(LoginedUserInfo loginInfo, int[] ids);

        #endregion 邮箱拒收/黑名单

        #region 邮件标签

        /// <summary>
        /// 获取当前用户的邮件标签列表
        /// </summary>
        /// <param name="query">查询条件,可不传</param>
        /// <returns></returns>
        EntityList<MailLabel> GetListSelfMailLabel(LoginedUserInfo loginInfo, QueryParameter parameter);
        /// <summary>
        /// 保存邮件标签，逻辑如下：
        /// 1、不能保存相同邮件名称和颜色的标签
        /// 2、只能修改自己的邮件标签
        /// </summary>
        /// <param name="contactDto">邮件标签数据传输对象</param>
        /// <returns></returns>
        ResponseResult SaveMailLabel(LoginedUserInfo loginInfo, MailLabel contactDto);
        /// <summary>
        /// 删除邮件标签，控制逻辑如下：
        /// 1、判断是否已被过滤器使用,如已使用不可删除
        /// 2、只能删除自己的
        /// </summary>
        /// <param name="ids">邮件标签编号ID数组</param>
        /// <returns></returns>
        ResponseResult RemoveMailLabel(LoginedUserInfo loginInfo, string[] ids);
        /// <summary>
        /// 邮件打标签
        /// </summary>
        /// <param name="mailIds">邮件ID数组</param>
        /// <param name="labelId">标签ID</param>
        /// <returns></returns>
        ResponseResult MailAddLabel(LoginedUserInfo loginInfo, string[] mailIds, string labelId);
        /// <summary>
        /// 删除邮件标签
        /// </summary>
        /// <param name="mailIds">邮件ID数组</param>
        /// <param name="labelId">标签ID</param>
        /// <returns></returns>
        ResponseResult MailRemoveLabel(LoginedUserInfo loginInfo, string[] mailIds, string labelId);
        #endregion 邮件标签
    }
}
