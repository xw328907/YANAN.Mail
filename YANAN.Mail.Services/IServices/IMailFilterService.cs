using System.Collections.Generic;

namespace YANAN.Mail.IServices
{
    using Titan;
    using YANAN.Mail.Entity;
    using YANAN.Mail.Utilities;

    public partial interface IMailFilterService
    {
        /// <summary>
        /// 获取用户某一邮箱的过滤条件
        /// </summary>
        /// <param name="mailBoxId">邮箱ID</param>
        /// <returns></returns>
        List<MailFilterCondition> GetListMailFilterCondition(LoginedUserInfo loginInfo, string mailBoxId);
        /// <summary>
        /// 邮件过滤器保存/新增、修改
        /// </summary>
        /// <param name="filterConditionDto">过滤器数据对象dto</param>
        /// <returns></returns>
        ResponseResult SaveMailFilterCondition(LoginedUserInfo loginInfo, MailFilterCondition filterConditionDto);
        /// <summary>
        /// 过滤器删除
        /// </summary>
        /// <param name="ids">过滤器ID数组</param>
        /// <returns></returns>
        ResponseResult RemoveMailFilterCondition(LoginedUserInfo loginInfo, int[] ids);
        /// <summary>
        /// 检查邮件标签是否在过滤器中使用
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tagId"></param>
        /// <returns></returns>
        bool CheckMailLabelUsedInFilter(LoginedUserInfo loginInfo, IDbSession db, string[] tagId);
        /// <summary>
        /// 更新过滤器中的邮件标签信息
        /// </summary>
        /// <param name="db"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        bool UpdateTagToFilter(LoginedUserInfo loginInfo, IDbSession db, MailLabel sign);
    }
}
