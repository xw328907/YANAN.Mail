using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using Titan;
using Titan.MySql;
using Titan.SQLite;
using Titan.SqlServer;
using Titan.Oracle;

namespace YANAN.Mail.Entity
{
    
    public class EntityList<T> 
    { 
        private List<T> _items =   new List<T>();

        public long TotalCount{ get; set; }

        public List<T> Items {
            get { return _items;}
            set { _items = value;}
        }
          
    }
/*
<system.runtime.serialization>
    <dataContractSerializer>
      <declaredTypes>
 
      </declaredTypes>
    </dataContractSerializer>
  </system.runtime.serialization>

*/
    #region enums

    #endregion




    #region MailAttach
    /// <summary>
    /// 邮件系统 - 邮件附件,邮件附件
    /// </summary>
    [DisplayName("邮件系统 - 邮件附件")]
    [Table]
    public partial class MailAttach 
    {
        
        public MailAttach()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮件附件主键ID,邮件附件主键ID,自增
        /// </summary>
        [DisplayName("邮件附件主键ID")]
        [Column(IsPrimaryKey = true)]
        [SQLiteColumn(IsIdentity = true)]
        public int MailAttachId {  get; set; }


        /// <summary>
        /// 邮件ID,邮件主键ID
        /// </summary>
        [DisplayName("邮件ID")]
        [Column(Size = 50)] 
        public string MailMainId {  get; set; }


        /// <summary>
        /// 附件名称,文件名
        /// </summary>
        [DisplayName("附件名称")]
        [Column(Size = 225)] 
        public string FilesName {  get; set; }


        /// <summary>
        /// 附件文件大小,附件文件大小，已转换为文本如:1.24 MB
        /// </summary>
        [DisplayName("附件文件大小")]
        [Column(Size = 32)] 
        public string FilesSize {  get; set; }


        /// <summary>
        /// 附件实际大小,实际大小
        /// </summary>
        [DisplayName("附件实际大小")]
        [Column()] 
        public int? ActualSize {  get; set; }


        /// <summary>
        /// 附件路径,文件路径
        /// </summary>
        [DisplayName("附件路径")]
        [Column(Size = 400)] 
        public string FilesPath {  get; set; }


        /// <summary>
        /// 附件类型,文件类型
        /// </summary>
        [DisplayName("附件类型")]
        [Column(Size = 10)] 
        public string FilesType {  get; set; }


        /// <summary>
        /// 内嵌内容ID,是否为内嵌图片,如果是,则为图片编号Id
        /// </summary>
        [DisplayName("内嵌内容ID")]
        [Column(Size = 200)] 
        public string ContentID {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮件附件表.邮件表,邮件附件表外键关联邮件表
        /// </summary>
        [Relation("this.MailMainId=out.MailMainId")]
        public MailMain MailMain { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailAttachProperties
    public static partial class MailAttach_
    {
    
        private static MailAttachDescriptor instance = new MailAttachDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailAttach";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件附件主键ID,邮件附件主键ID,自增
        /// </summary>
        public static PropertyExpression MailAttachId { get{return instance.MailAttachId;}} 
        /// <summary>
        /// 邮件ID,邮件主键ID
        /// </summary>
        public static PropertyExpression MailMainId { get{return instance.MailMainId;}} 
        /// <summary>
        /// 附件名称,文件名
        /// </summary>
        public static PropertyExpression FilesName { get{return instance.FilesName;}} 
        /// <summary>
        /// 附件文件大小,附件文件大小，已转换为文本如:1.24 MB
        /// </summary>
        public static PropertyExpression FilesSize { get{return instance.FilesSize;}} 
        /// <summary>
        /// 附件实际大小,实际大小
        /// </summary>
        public static PropertyExpression ActualSize { get{return instance.ActualSize;}} 
        /// <summary>
        /// 附件路径,文件路径
        /// </summary>
        public static PropertyExpression FilesPath { get{return instance.FilesPath;}} 
        /// <summary>
        /// 附件类型,文件类型
        /// </summary>
        public static PropertyExpression FilesType { get{return instance.FilesType;}} 
        /// <summary>
        /// 内嵌内容ID,是否为内嵌图片,如果是,则为图片编号Id
        /// </summary>
        public static PropertyExpression ContentID { get{return instance.ContentID;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}


 
        /// <summary>
        /// 邮件附件表.邮件表,邮件附件表外键关联邮件表
        /// </summary>
        public static MailMainDescriptor MailMain { get{return instance.MailMain;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailAttachDescriptor
    public partial class MailAttachDescriptor:ObjectDescriptorBase
    {
     
        public MailAttachDescriptor(string prefix):base(prefix)
        {  
    
            this._MailAttachId = new PropertyExpression(prefix + "MailAttachId");
            this._MailMainId = new PropertyExpression(prefix + "MailMainId");
            this._FilesName = new PropertyExpression(prefix + "FilesName");
            this._FilesSize = new PropertyExpression(prefix + "FilesSize");
            this._ActualSize = new PropertyExpression(prefix + "ActualSize");
            this._FilesPath = new PropertyExpression(prefix + "FilesPath");
            this._FilesType = new PropertyExpression(prefix + "FilesType");
            this._ContentID = new PropertyExpression(prefix + "ContentID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailAttachId,this._MailMainId,this._FilesName,this._FilesSize,this._ActualSize,this._FilesPath,this._FilesType,this._ContentID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailAttachId;
        /// <summary>
        /// 邮件附件主键ID,邮件附件主键ID,自增
        /// </summary>
        public PropertyExpression MailAttachId { get{return _MailAttachId;}}
        private PropertyExpression _MailMainId;
        /// <summary>
        /// 邮件ID,邮件主键ID
        /// </summary>
        public PropertyExpression MailMainId { get{return _MailMainId;}}
        private PropertyExpression _FilesName;
        /// <summary>
        /// 附件名称,文件名
        /// </summary>
        public PropertyExpression FilesName { get{return _FilesName;}}
        private PropertyExpression _FilesSize;
        /// <summary>
        /// 附件文件大小,附件文件大小，已转换为文本如:1.24 MB
        /// </summary>
        public PropertyExpression FilesSize { get{return _FilesSize;}}
        private PropertyExpression _ActualSize;
        /// <summary>
        /// 附件实际大小,实际大小
        /// </summary>
        public PropertyExpression ActualSize { get{return _ActualSize;}}
        private PropertyExpression _FilesPath;
        /// <summary>
        /// 附件路径,文件路径
        /// </summary>
        public PropertyExpression FilesPath { get{return _FilesPath;}}
        private PropertyExpression _FilesType;
        /// <summary>
        /// 附件类型,文件类型
        /// </summary>
        public PropertyExpression FilesType { get{return _FilesType;}}
        private PropertyExpression _ContentID;
        /// <summary>
        /// 内嵌内容ID,是否为内嵌图片,如果是,则为图片编号Id
        /// </summary>
        public PropertyExpression ContentID { get{return _ContentID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



        private MailMainDescriptor _MailMain;
        public MailMainDescriptor MailMain 
        { 
            get
            {
                if(_MailMain==null) _MailMain=new MailMainDescriptor(base.Prefix+"MailMain.");
                return _MailMain;
            }
        }
    }
     #endregion


    #region MailAttachs
    /// <summary>
    /// 邮件系统 - 邮件附件,邮件附件
    /// </summary>
    [DisplayName("邮件系统 - 邮件附件")]
    [Table]
    public partial class MailAttachs:EntityList<MailAttach> 
    {
        
    }
    #endregion


    #region MailBody
    /// <summary>
    /// 邮件系统 - 邮件正文,邮件正文表
    /// </summary>
    [DisplayName("邮件系统 - 邮件正文")]
    [Table]
    public partial class MailBody 
    {
        
        public MailBody()
        {

        
        }
        #region propertys
        
        /// <summary>
        /// 邮件ID,邮件主键
        /// </summary>
        [DisplayName("邮件ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailMainId {  get; set; }


        /// <summary>
        /// 邮件正文内容,
        /// </summary>
        [DisplayName("邮件正文内容")]
        [Column()] 
        public string BodyHtml {  get; set; }


        /// <summary>
        /// 邮件纯文本内容,
        /// </summary>
        [DisplayName("邮件纯文本内容")]
        [Column()] 
        public string BodyText {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮件正文表.邮件表,邮件正文表.邮件表
        /// </summary>
        [Relation("this.MailMainId=out.MailMainId")]
        public MailMain MailMain { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailBodyProperties
    public static partial class MailBody_
    {
    
        private static MailBodyDescriptor instance = new MailBodyDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailBody";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件ID,邮件主键
        /// </summary>
        public static PropertyExpression MailMainId { get{return instance.MailMainId;}} 
        /// <summary>
        /// 邮件正文内容,
        /// </summary>
        public static PropertyExpression BodyHtml { get{return instance.BodyHtml;}} 
        /// <summary>
        /// 邮件纯文本内容,
        /// </summary>
        public static PropertyExpression BodyText { get{return instance.BodyText;}}


 
        /// <summary>
        /// 邮件正文表.邮件表,邮件正文表.邮件表
        /// </summary>
        public static MailMainDescriptor MailMain { get{return instance.MailMain;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailBodyDescriptor
    public partial class MailBodyDescriptor:ObjectDescriptorBase
    {
     
        public MailBodyDescriptor(string prefix):base(prefix)
        {  
    
            this._MailMainId = new PropertyExpression(prefix + "MailMainId");
            this._BodyHtml = new PropertyExpression(prefix + "BodyHtml");
            this._BodyText = new PropertyExpression(prefix + "BodyText");
            ALL = new PropertyExpression[] {this._MailMainId,this._BodyHtml,this._BodyText};
        }
         

        private PropertyExpression _MailMainId;
        /// <summary>
        /// 邮件ID,邮件主键
        /// </summary>
        public PropertyExpression MailMainId { get{return _MailMainId;}}
        private PropertyExpression _BodyHtml;
        /// <summary>
        /// 邮件正文内容,
        /// </summary>
        public PropertyExpression BodyHtml { get{return _BodyHtml;}}
        private PropertyExpression _BodyText;
        /// <summary>
        /// 邮件纯文本内容,
        /// </summary>
        public PropertyExpression BodyText { get{return _BodyText;}}



        private MailMainDescriptor _MailMain;
        public MailMainDescriptor MailMain 
        { 
            get
            {
                if(_MailMain==null) _MailMain=new MailMainDescriptor(base.Prefix+"MailMain.");
                return _MailMain;
            }
        }
    }
     #endregion


    #region MailBodys
    /// <summary>
    /// 邮件系统 - 邮件正文,邮件正文表
    /// </summary>
    [DisplayName("邮件系统 - 邮件正文")]
    [Table]
    public partial class MailBodys:EntityList<MailBody> 
    {
        
    }
    #endregion


    #region MailBox
    /// <summary>
    /// 邮件系统 - 邮箱,邮件系统--邮箱表
    /// </summary>
    [DisplayName("邮件系统 - 邮箱")]
    [Table]
    public partial class MailBox 
    {
        
        public MailBox()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮箱主键ID,
        /// </summary>
        [DisplayName("邮箱主键ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 排序号,
        /// </summary>
        [DisplayName("排序号")]
        [Column()] 
        public int Sorting {  get; set; }


        /// <summary>
        /// 邮箱显示名称,
        /// </summary>
        [DisplayName("邮箱显示名称")]
        [Column(Size = 128)] 
        public string ShowName {  get; set; }


        /// <summary>
        /// 邮箱别名,邮箱别名,发件时显示的邮箱名称
        /// </summary>
        [DisplayName("邮箱别名")]
        [Column(Size = 125)] 
        public string NickName {  get; set; }


        /// <summary>
        /// 邮箱地址,
        /// </summary>
        [DisplayName("邮箱地址")]
        [Column(Size = 125)] 
        public string MailAddress {  get; set; }


        /// <summary>
        /// 邮箱密码,邮箱密码(加密,可解密)
        /// </summary>
        [DisplayName("邮箱密码")]
        [Column(Size = 64)] 
        public string MailPassword {  get; set; }


        /// <summary>
        /// 协议类型编号,协议类型1=pop3,2=imap
        /// </summary>
        [DisplayName("协议类型编号")]
        [Column()] 
        public int ProtocolTypeId {  get; set; }


        /// <summary>
        /// 收件服务地址,收件服务地址(IMAP/POP3)，如ProtocolTypeId=2则为imap地址
        /// </summary>
        [DisplayName("收件服务地址")]
        [Column(Size = 100)] 
        public string PopServer {  get; set; }


        /// <summary>
        /// 收件服务端口号,收件服务端口号；如ProtocolTypeId=2则为imap端口
        /// </summary>
        [DisplayName("收件服务端口号")]
        [Column()] 
        public int PopPort {  get; set; }


        /// <summary>
        /// 发件服务地址,发件地址SMTP
        /// </summary>
        [DisplayName("发件服务地址")]
        [Column(Size = 100)] 
        public string SmtpServer {  get; set; }


        /// <summary>
        /// 发件服务端口号,发件端口号
        /// </summary>
        [DisplayName("发件服务端口号")]
        [Column()] 
        public int SmtpPort {  get; set; }


        /// <summary>
        /// 邮件总数,邮箱邮件总数
        /// </summary>
        [DisplayName("邮件总数")]
        [Column()] 
        public int MailCount {  get; set; }


        /// <summary>
        /// 邮箱邮件大小,邮箱邮件大小,单位byte
        /// </summary>
        [DisplayName("邮箱邮件大小")]
        [Column()] 
        public long? MailSize {  get; set; }


        /// <summary>
        /// 是否默认邮箱,是否为默认的发件箱
        /// 
        /// </summary>
        [DisplayName("是否默认邮箱")]
        [Column()] 
        public bool IsDefault {  get; set; }


        /// <summary>
        /// 发送轮询时间,发送轮询时间
        /// 
        /// </summary>
        [DisplayName("发送轮询时间")]
        [Column()] 
        public int SendTimer {  get; set; }


        /// <summary>
        /// 接收轮询时间,接收轮询时间
        /// 
        /// </summary>
        [DisplayName("接收轮询时间")]
        [Column()] 
        public int ReceiveTimer {  get; set; }


        /// <summary>
        /// 服务端保留天数,服务端保留天数
        /// 
        /// </summary>
        [DisplayName("服务端保留天数")]
        [Column()] 
        public int KeepDays {  get; set; }


        /// <summary>
        /// 默认收取邮件开始日期,默认收取邮件开始日期，用于指定收取某一日期之后邮件
        /// </summary>
        [DisplayName("默认收取邮件开始日期")]
        [Column()] 
        public DateTime? ReceiveBeginTime {  get; set; }


        /// <summary>
        /// 默认密送,默认密送,多个以分号分割
        /// </summary>
        [DisplayName("默认密送")]
        [Column(Size = 4000)] 
        public string Bcc {  get; set; }


        /// <summary>
        /// 默认抄送,默认抄送,多个以分号分割
        /// </summary>
        [DisplayName("默认抄送")]
        [Column(Size = 4000)] 
        public string Cc {  get; set; }


        /// <summary>
        /// 是否同步,是否同步;是否上传云端(我司平台),true 已上传; false 未上传
        /// </summary>
        [DisplayName("是否同步")]
        [Column()] 
        public bool IsSync {  get; set; }


        /// <summary>
        /// 备注,
        /// </summary>
        [DisplayName("备注")]
        [Column(Size = 128)] 
        public string Memo {  get; set; }


        /// <summary>
        /// 邮箱拥有人编号,
        /// </summary>
        [DisplayName("邮箱拥有人编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 是否删除,是否已删除，true 删除；false 未删除(默认值)
        /// </summary>
        [DisplayName("是否删除")]
        [Column()] 
        public bool Deleted {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        #endregion

    }
    #endregion
    #region MailBoxProperties
    public static partial class MailBox_
    {
    
        private static MailBoxDescriptor instance = new MailBoxDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailBox";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮箱主键ID,
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 排序号,
        /// </summary>
        public static PropertyExpression Sorting { get{return instance.Sorting;}} 
        /// <summary>
        /// 邮箱显示名称,
        /// </summary>
        public static PropertyExpression ShowName { get{return instance.ShowName;}} 
        /// <summary>
        /// 邮箱别名,邮箱别名,发件时显示的邮箱名称
        /// </summary>
        public static PropertyExpression NickName { get{return instance.NickName;}} 
        /// <summary>
        /// 邮箱地址,
        /// </summary>
        public static PropertyExpression MailAddress { get{return instance.MailAddress;}} 
        /// <summary>
        /// 邮箱密码,邮箱密码(加密,可解密)
        /// </summary>
        public static PropertyExpression MailPassword { get{return instance.MailPassword;}} 
        /// <summary>
        /// 协议类型编号,协议类型1=pop3,2=imap
        /// </summary>
        public static PropertyExpression ProtocolTypeId { get{return instance.ProtocolTypeId;}} 
        /// <summary>
        /// 收件服务地址,收件服务地址(IMAP/POP3)，如ProtocolTypeId=2则为imap地址
        /// </summary>
        public static PropertyExpression PopServer { get{return instance.PopServer;}} 
        /// <summary>
        /// 收件服务端口号,收件服务端口号；如ProtocolTypeId=2则为imap端口
        /// </summary>
        public static PropertyExpression PopPort { get{return instance.PopPort;}} 
        /// <summary>
        /// 发件服务地址,发件地址SMTP
        /// </summary>
        public static PropertyExpression SmtpServer { get{return instance.SmtpServer;}} 
        /// <summary>
        /// 发件服务端口号,发件端口号
        /// </summary>
        public static PropertyExpression SmtpPort { get{return instance.SmtpPort;}} 
        /// <summary>
        /// 邮件总数,邮箱邮件总数
        /// </summary>
        public static PropertyExpression MailCount { get{return instance.MailCount;}} 
        /// <summary>
        /// 邮箱邮件大小,邮箱邮件大小,单位byte
        /// </summary>
        public static PropertyExpression MailSize { get{return instance.MailSize;}} 
        /// <summary>
        /// 是否默认邮箱,是否为默认的发件箱
        /// 
        /// </summary>
        public static PropertyExpression IsDefault { get{return instance.IsDefault;}} 
        /// <summary>
        /// 发送轮询时间,发送轮询时间
        /// 
        /// </summary>
        public static PropertyExpression SendTimer { get{return instance.SendTimer;}} 
        /// <summary>
        /// 接收轮询时间,接收轮询时间
        /// 
        /// </summary>
        public static PropertyExpression ReceiveTimer { get{return instance.ReceiveTimer;}} 
        /// <summary>
        /// 服务端保留天数,服务端保留天数
        /// 
        /// </summary>
        public static PropertyExpression KeepDays { get{return instance.KeepDays;}} 
        /// <summary>
        /// 默认收取邮件开始日期,默认收取邮件开始日期，用于指定收取某一日期之后邮件
        /// </summary>
        public static PropertyExpression ReceiveBeginTime { get{return instance.ReceiveBeginTime;}} 
        /// <summary>
        /// 默认密送,默认密送,多个以分号分割
        /// </summary>
        public static PropertyExpression Bcc { get{return instance.Bcc;}} 
        /// <summary>
        /// 默认抄送,默认抄送,多个以分号分割
        /// </summary>
        public static PropertyExpression Cc { get{return instance.Cc;}} 
        /// <summary>
        /// 是否同步,是否同步;是否上传云端(我司平台),true 已上传; false 未上传
        /// </summary>
        public static PropertyExpression IsSync { get{return instance.IsSync;}} 
        /// <summary>
        /// 备注,
        /// </summary>
        public static PropertyExpression Memo { get{return instance.Memo;}} 
        /// <summary>
        /// 邮箱拥有人编号,
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 是否删除,是否已删除，true 删除；false 未删除(默认值)
        /// </summary>
        public static PropertyExpression Deleted { get{return instance.Deleted;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailBoxDescriptor
    public partial class MailBoxDescriptor:ObjectDescriptorBase
    {
     
        public MailBoxDescriptor(string prefix):base(prefix)
        {  
    
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._Sorting = new PropertyExpression(prefix + "Sorting");
            this._ShowName = new PropertyExpression(prefix + "ShowName");
            this._NickName = new PropertyExpression(prefix + "NickName");
            this._MailAddress = new PropertyExpression(prefix + "MailAddress");
            this._MailPassword = new PropertyExpression(prefix + "MailPassword");
            this._ProtocolTypeId = new PropertyExpression(prefix + "ProtocolTypeId");
            this._PopServer = new PropertyExpression(prefix + "PopServer");
            this._PopPort = new PropertyExpression(prefix + "PopPort");
            this._SmtpServer = new PropertyExpression(prefix + "SmtpServer");
            this._SmtpPort = new PropertyExpression(prefix + "SmtpPort");
            this._MailCount = new PropertyExpression(prefix + "MailCount");
            this._MailSize = new PropertyExpression(prefix + "MailSize");
            this._IsDefault = new PropertyExpression(prefix + "IsDefault");
            this._SendTimer = new PropertyExpression(prefix + "SendTimer");
            this._ReceiveTimer = new PropertyExpression(prefix + "ReceiveTimer");
            this._KeepDays = new PropertyExpression(prefix + "KeepDays");
            this._ReceiveBeginTime = new PropertyExpression(prefix + "ReceiveBeginTime");
            this._Bcc = new PropertyExpression(prefix + "Bcc");
            this._Cc = new PropertyExpression(prefix + "Cc");
            this._IsSync = new PropertyExpression(prefix + "IsSync");
            this._Memo = new PropertyExpression(prefix + "Memo");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._Deleted = new PropertyExpression(prefix + "Deleted");
            this._OCode = new PropertyExpression(prefix + "OCode");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            ALL = new PropertyExpression[] {this._MailBoxId,this._Sorting,this._ShowName,this._NickName,this._MailAddress,this._MailPassword,this._ProtocolTypeId,this._PopServer,this._PopPort,this._SmtpServer,this._SmtpPort,this._MailCount,this._MailSize,this._IsDefault,this._SendTimer,this._ReceiveTimer,this._KeepDays,this._ReceiveBeginTime,this._Bcc,this._Cc,this._IsSync,this._Memo,this._OwnerUID,this._Deleted,this._OCode,this._CreateUID,this._CreateTime};
        }
         

        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 邮箱主键ID,
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _Sorting;
        /// <summary>
        /// 排序号,
        /// </summary>
        public PropertyExpression Sorting { get{return _Sorting;}}
        private PropertyExpression _ShowName;
        /// <summary>
        /// 邮箱显示名称,
        /// </summary>
        public PropertyExpression ShowName { get{return _ShowName;}}
        private PropertyExpression _NickName;
        /// <summary>
        /// 邮箱别名,邮箱别名,发件时显示的邮箱名称
        /// </summary>
        public PropertyExpression NickName { get{return _NickName;}}
        private PropertyExpression _MailAddress;
        /// <summary>
        /// 邮箱地址,
        /// </summary>
        public PropertyExpression MailAddress { get{return _MailAddress;}}
        private PropertyExpression _MailPassword;
        /// <summary>
        /// 邮箱密码,邮箱密码(加密,可解密)
        /// </summary>
        public PropertyExpression MailPassword { get{return _MailPassword;}}
        private PropertyExpression _ProtocolTypeId;
        /// <summary>
        /// 协议类型编号,协议类型1=pop3,2=imap
        /// </summary>
        public PropertyExpression ProtocolTypeId { get{return _ProtocolTypeId;}}
        private PropertyExpression _PopServer;
        /// <summary>
        /// 收件服务地址,收件服务地址(IMAP/POP3)，如ProtocolTypeId=2则为imap地址
        /// </summary>
        public PropertyExpression PopServer { get{return _PopServer;}}
        private PropertyExpression _PopPort;
        /// <summary>
        /// 收件服务端口号,收件服务端口号；如ProtocolTypeId=2则为imap端口
        /// </summary>
        public PropertyExpression PopPort { get{return _PopPort;}}
        private PropertyExpression _SmtpServer;
        /// <summary>
        /// 发件服务地址,发件地址SMTP
        /// </summary>
        public PropertyExpression SmtpServer { get{return _SmtpServer;}}
        private PropertyExpression _SmtpPort;
        /// <summary>
        /// 发件服务端口号,发件端口号
        /// </summary>
        public PropertyExpression SmtpPort { get{return _SmtpPort;}}
        private PropertyExpression _MailCount;
        /// <summary>
        /// 邮件总数,邮箱邮件总数
        /// </summary>
        public PropertyExpression MailCount { get{return _MailCount;}}
        private PropertyExpression _MailSize;
        /// <summary>
        /// 邮箱邮件大小,邮箱邮件大小,单位byte
        /// </summary>
        public PropertyExpression MailSize { get{return _MailSize;}}
        private PropertyExpression _IsDefault;
        /// <summary>
        /// 是否默认邮箱,是否为默认的发件箱
        /// 
        /// </summary>
        public PropertyExpression IsDefault { get{return _IsDefault;}}
        private PropertyExpression _SendTimer;
        /// <summary>
        /// 发送轮询时间,发送轮询时间
        /// 
        /// </summary>
        public PropertyExpression SendTimer { get{return _SendTimer;}}
        private PropertyExpression _ReceiveTimer;
        /// <summary>
        /// 接收轮询时间,接收轮询时间
        /// 
        /// </summary>
        public PropertyExpression ReceiveTimer { get{return _ReceiveTimer;}}
        private PropertyExpression _KeepDays;
        /// <summary>
        /// 服务端保留天数,服务端保留天数
        /// 
        /// </summary>
        public PropertyExpression KeepDays { get{return _KeepDays;}}
        private PropertyExpression _ReceiveBeginTime;
        /// <summary>
        /// 默认收取邮件开始日期,默认收取邮件开始日期，用于指定收取某一日期之后邮件
        /// </summary>
        public PropertyExpression ReceiveBeginTime { get{return _ReceiveBeginTime;}}
        private PropertyExpression _Bcc;
        /// <summary>
        /// 默认密送,默认密送,多个以分号分割
        /// </summary>
        public PropertyExpression Bcc { get{return _Bcc;}}
        private PropertyExpression _Cc;
        /// <summary>
        /// 默认抄送,默认抄送,多个以分号分割
        /// </summary>
        public PropertyExpression Cc { get{return _Cc;}}
        private PropertyExpression _IsSync;
        /// <summary>
        /// 是否同步,是否同步;是否上传云端(我司平台),true 已上传; false 未上传
        /// </summary>
        public PropertyExpression IsSync { get{return _IsSync;}}
        private PropertyExpression _Memo;
        /// <summary>
        /// 备注,
        /// </summary>
        public PropertyExpression Memo { get{return _Memo;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 邮箱拥有人编号,
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _Deleted;
        /// <summary>
        /// 是否删除,是否已删除，true 删除；false 未删除(默认值)
        /// </summary>
        public PropertyExpression Deleted { get{return _Deleted;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}



    }
     #endregion


    #region MailBoxs
    /// <summary>
    /// 邮件系统 - 邮箱,邮件系统--邮箱表
    /// </summary>
    [DisplayName("邮件系统 - 邮箱")]
    [Table]
    public partial class MailBoxs:EntityList<MailBox> 
    {
        
    }
    #endregion


    #region MailBoxMessage
    /// <summary>
    /// 邮件系统 - 邮件ID表,存储每个邮箱邮件的messageId
    /// </summary>
    [DisplayName("邮件系统 - 邮件ID表")]
    [Table]
    public partial class MailBoxMessage 
    {
        
        public MailBoxMessage()
        {

            InsertTime=DateTime.Now;
            DeleteTime=DateTime.Now;
            MailTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮箱ID,邮箱主键ID，联合主键
        /// </summary>
        [DisplayName("邮箱ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 邮件ID,邮件邮局唯一UID，如为IMAP协议则该值为文件夹ID与邮件UID组合值，联合主键
        /// </summary>
        [DisplayName("邮件ID")]
        [Column(IsPrimaryKey = true,Size = 100)] 
        public string MessageId {  get; set; }


        /// <summary>
        /// 收取时间,
        /// </summary>
        [DisplayName("收取时间")]
        [Column()] 
        public DateTime InsertTime {  get; set; }


        /// <summary>
        /// 收取状态,收取状态；1= 正常，2=错误,3=拒收(黑名单)
        /// </summary>
        [DisplayName("收取状态")]
        [Column()] 
        public int InsertType {  get; set; }


        /// <summary>
        /// 收取错误次数,收取错误次数，InsertType=2时有效,主用于判断超多少次之后停止收取
        /// </summary>
        [DisplayName("收取错误次数")]
        [Column()] 
        public int? InsertErrorNum {  get; set; }


        /// <summary>
        /// 删除时间,
        /// </summary>
        [DisplayName("删除时间")]
        [Column()] 
        public DateTime DeleteTime {  get; set; }


        /// <summary>
        /// 删除状态,删除状态；1= 正常，2=错误
        /// </summary>
        [DisplayName("删除状态")]
        [Column()] 
        public int DeleteType {  get; set; }


        /// <summary>
        /// 删除错误次数,删除错误次数，InsertType=2时有效,主用于判断超多少次之后停止删除
        /// </summary>
        [DisplayName("删除错误次数")]
        [Column()] 
        public int? DeleteErrorNum {  get; set; }


        /// <summary>
        /// 邮件时间,
        /// </summary>
        [DisplayName("邮件时间")]
        [Column()] 
        public DateTime MailTime {  get; set; }


        #endregion

    }
    #endregion
    #region MailBoxMessageProperties
    public static partial class MailBoxMessage_
    {
    
        private static MailBoxMessageDescriptor instance = new MailBoxMessageDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailBoxMessage";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮箱ID,邮箱主键ID，联合主键
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 邮件ID,邮件邮局唯一UID，如为IMAP协议则该值为文件夹ID与邮件UID组合值，联合主键
        /// </summary>
        public static PropertyExpression MessageId { get{return instance.MessageId;}} 
        /// <summary>
        /// 收取时间,
        /// </summary>
        public static PropertyExpression InsertTime { get{return instance.InsertTime;}} 
        /// <summary>
        /// 收取状态,收取状态；1= 正常，2=错误,3=拒收(黑名单)
        /// </summary>
        public static PropertyExpression InsertType { get{return instance.InsertType;}} 
        /// <summary>
        /// 收取错误次数,收取错误次数，InsertType=2时有效,主用于判断超多少次之后停止收取
        /// </summary>
        public static PropertyExpression InsertErrorNum { get{return instance.InsertErrorNum;}} 
        /// <summary>
        /// 删除时间,
        /// </summary>
        public static PropertyExpression DeleteTime { get{return instance.DeleteTime;}} 
        /// <summary>
        /// 删除状态,删除状态；1= 正常，2=错误
        /// </summary>
        public static PropertyExpression DeleteType { get{return instance.DeleteType;}} 
        /// <summary>
        /// 删除错误次数,删除错误次数，InsertType=2时有效,主用于判断超多少次之后停止删除
        /// </summary>
        public static PropertyExpression DeleteErrorNum { get{return instance.DeleteErrorNum;}} 
        /// <summary>
        /// 邮件时间,
        /// </summary>
        public static PropertyExpression MailTime { get{return instance.MailTime;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailBoxMessageDescriptor
    public partial class MailBoxMessageDescriptor:ObjectDescriptorBase
    {
     
        public MailBoxMessageDescriptor(string prefix):base(prefix)
        {  
    
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._MessageId = new PropertyExpression(prefix + "MessageId");
            this._InsertTime = new PropertyExpression(prefix + "InsertTime");
            this._InsertType = new PropertyExpression(prefix + "InsertType");
            this._InsertErrorNum = new PropertyExpression(prefix + "InsertErrorNum");
            this._DeleteTime = new PropertyExpression(prefix + "DeleteTime");
            this._DeleteType = new PropertyExpression(prefix + "DeleteType");
            this._DeleteErrorNum = new PropertyExpression(prefix + "DeleteErrorNum");
            this._MailTime = new PropertyExpression(prefix + "MailTime");
            ALL = new PropertyExpression[] {this._MailBoxId,this._MessageId,this._InsertTime,this._InsertType,this._InsertErrorNum,this._DeleteTime,this._DeleteType,this._DeleteErrorNum,this._MailTime};
        }
         

        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 邮箱ID,邮箱主键ID，联合主键
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _MessageId;
        /// <summary>
        /// 邮件ID,邮件邮局唯一UID，如为IMAP协议则该值为文件夹ID与邮件UID组合值，联合主键
        /// </summary>
        public PropertyExpression MessageId { get{return _MessageId;}}
        private PropertyExpression _InsertTime;
        /// <summary>
        /// 收取时间,
        /// </summary>
        public PropertyExpression InsertTime { get{return _InsertTime;}}
        private PropertyExpression _InsertType;
        /// <summary>
        /// 收取状态,收取状态；1= 正常，2=错误,3=拒收(黑名单)
        /// </summary>
        public PropertyExpression InsertType { get{return _InsertType;}}
        private PropertyExpression _InsertErrorNum;
        /// <summary>
        /// 收取错误次数,收取错误次数，InsertType=2时有效,主用于判断超多少次之后停止收取
        /// </summary>
        public PropertyExpression InsertErrorNum { get{return _InsertErrorNum;}}
        private PropertyExpression _DeleteTime;
        /// <summary>
        /// 删除时间,
        /// </summary>
        public PropertyExpression DeleteTime { get{return _DeleteTime;}}
        private PropertyExpression _DeleteType;
        /// <summary>
        /// 删除状态,删除状态；1= 正常，2=错误
        /// </summary>
        public PropertyExpression DeleteType { get{return _DeleteType;}}
        private PropertyExpression _DeleteErrorNum;
        /// <summary>
        /// 删除错误次数,删除错误次数，InsertType=2时有效,主用于判断超多少次之后停止删除
        /// </summary>
        public PropertyExpression DeleteErrorNum { get{return _DeleteErrorNum;}}
        private PropertyExpression _MailTime;
        /// <summary>
        /// 邮件时间,
        /// </summary>
        public PropertyExpression MailTime { get{return _MailTime;}}



    }
     #endregion


    #region MailBoxMessages
    /// <summary>
    /// 邮件系统 - 邮件ID表,存储每个邮箱邮件的messageId
    /// </summary>
    [DisplayName("邮件系统 - 邮件ID表")]
    [Table]
    public partial class MailBoxMessages:EntityList<MailBoxMessage> 
    {
        
    }
    #endregion


    #region MailContact
    /// <summary>
    /// 邮件系统 - 邮箱联系人,邮件系统 - 邮箱联系人表
    /// </summary>
    [DisplayName("邮件系统 - 邮箱联系人")]
    [Table]
    public partial class MailContact 
    {
        
        public MailContact()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮件联系人主键ID,
        /// </summary>
        [DisplayName("邮件联系人主键ID")]
        [Column(IsPrimaryKey = true)]
        [SQLiteColumn(IsIdentity = true)]
        public int MailContactId {  get; set; }


        /// <summary>
        /// 邮箱地址,
        /// </summary>
        [DisplayName("邮箱地址")]
        [Column(Size = 128)] 
        public string EMail {  get; set; }


        /// <summary>
        /// 联系人,
        /// </summary>
        [DisplayName("联系人")]
        [Column(Size = 64)] 
        public string ContactName {  get; set; }


        /// <summary>
        /// 联系人拼音,
        /// </summary>
        [DisplayName("联系人拼音")]
        [Column(Size = 64)] 
        public string ContactPinyin {  get; set; }


        /// <summary>
        /// 联系地址,
        /// </summary>
        [DisplayName("联系地址")]
        [Column(Size = 512)] 
        public string Address {  get; set; }


        /// <summary>
        /// 电话,
        /// </summary>
        [DisplayName("电话")]
        [Column(Size = 128)] 
        public string Tel {  get; set; }


        /// <summary>
        /// 传真,传真号
        /// </summary>
        [DisplayName("传真")]
        [Column(Size = 128)] 
        public string Fax {  get; set; }


        /// <summary>
        /// 手机,
        /// </summary>
        [DisplayName("手机")]
        [Column(Size = 256)] 
        public string Mobile {  get; set; }


        /// <summary>
        /// 邮编,
        /// </summary>
        [DisplayName("邮编")]
        [Column(Size = 40)] 
        public string Postalcode {  get; set; }


        /// <summary>
        /// QQ,
        /// </summary>
        [DisplayName("QQ")]
        [Column(Size = 50)] 
        public string QQ {  get; set; }


        /// <summary>
        /// 微信,
        /// </summary>
        [DisplayName("微信")]
        [Column(Size = 50)] 
        public string WeChat {  get; set; }


        /// <summary>
        /// Facebook,
        /// </summary>
        [DisplayName("Facebook")]
        [Column(Size = 50)] 
        public string Facebook {  get; set; }


        /// <summary>
        /// Twitter,
        /// </summary>
        [DisplayName("Twitter")]
        [Column(Size = 50)] 
        public string Twitter {  get; set; }


        /// <summary>
        /// Skype号,
        /// </summary>
        [DisplayName("Skype号")]
        [Column(Size = 128)] 
        public string Skype {  get; set; }


        /// <summary>
        /// 公司名称,
        /// </summary>
        [DisplayName("公司名称")]
        [Column(Size = 128)] 
        public string CompanyName {  get; set; }


        /// <summary>
        /// 公司职位,
        /// </summary>
        [DisplayName("公司职位")]
        [Column(Size = 128)] 
        public string Post {  get; set; }


        /// <summary>
        /// 区域,
        /// </summary>
        [DisplayName("区域")]
        [Column(Size = 128)] 
        public string Area {  get; set; }


        /// <summary>
        /// 国别编号,
        /// </summary>
        [DisplayName("国别编号")]
        [Column(Size = 50)] 
        public string CountryId {  get; set; }


        /// <summary>
        /// 备注,
        /// </summary>
        [DisplayName("备注")]
        [Column(Size = 256)] 
        public string Memo {  get; set; }


        /// <summary>
        /// 最后联系时间,最后一次联系时间
        /// </summary>
        [DisplayName("最后联系时间")]
        [Column()] 
        public DateTime? LastContactTime {  get; set; }


        /// <summary>
        /// 拥有人编号,联系人拥有人，默认与建立人一致
        /// </summary>
        [DisplayName("拥有人编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 建立人编号,
        /// </summary>
        [DisplayName("建立人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

    }
    #endregion
    #region MailContactProperties
    public static partial class MailContact_
    {
    
        private static MailContactDescriptor instance = new MailContactDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailContact";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件联系人主键ID,
        /// </summary>
        public static PropertyExpression MailContactId { get{return instance.MailContactId;}} 
        /// <summary>
        /// 邮箱地址,
        /// </summary>
        public static PropertyExpression EMail { get{return instance.EMail;}} 
        /// <summary>
        /// 联系人,
        /// </summary>
        public static PropertyExpression ContactName { get{return instance.ContactName;}} 
        /// <summary>
        /// 联系人拼音,
        /// </summary>
        public static PropertyExpression ContactPinyin { get{return instance.ContactPinyin;}} 
        /// <summary>
        /// 联系地址,
        /// </summary>
        public static PropertyExpression Address { get{return instance.Address;}} 
        /// <summary>
        /// 电话,
        /// </summary>
        public static PropertyExpression Tel { get{return instance.Tel;}} 
        /// <summary>
        /// 传真,传真号
        /// </summary>
        public static PropertyExpression Fax { get{return instance.Fax;}} 
        /// <summary>
        /// 手机,
        /// </summary>
        public static PropertyExpression Mobile { get{return instance.Mobile;}} 
        /// <summary>
        /// 邮编,
        /// </summary>
        public static PropertyExpression Postalcode { get{return instance.Postalcode;}} 
        /// <summary>
        /// QQ,
        /// </summary>
        public static PropertyExpression QQ { get{return instance.QQ;}} 
        /// <summary>
        /// 微信,
        /// </summary>
        public static PropertyExpression WeChat { get{return instance.WeChat;}} 
        /// <summary>
        /// Facebook,
        /// </summary>
        public static PropertyExpression Facebook { get{return instance.Facebook;}} 
        /// <summary>
        /// Twitter,
        /// </summary>
        public static PropertyExpression Twitter { get{return instance.Twitter;}} 
        /// <summary>
        /// Skype号,
        /// </summary>
        public static PropertyExpression Skype { get{return instance.Skype;}} 
        /// <summary>
        /// 公司名称,
        /// </summary>
        public static PropertyExpression CompanyName { get{return instance.CompanyName;}} 
        /// <summary>
        /// 公司职位,
        /// </summary>
        public static PropertyExpression Post { get{return instance.Post;}} 
        /// <summary>
        /// 区域,
        /// </summary>
        public static PropertyExpression Area { get{return instance.Area;}} 
        /// <summary>
        /// 国别编号,
        /// </summary>
        public static PropertyExpression CountryId { get{return instance.CountryId;}} 
        /// <summary>
        /// 备注,
        /// </summary>
        public static PropertyExpression Memo { get{return instance.Memo;}} 
        /// <summary>
        /// 最后联系时间,最后一次联系时间
        /// </summary>
        public static PropertyExpression LastContactTime { get{return instance.LastContactTime;}} 
        /// <summary>
        /// 拥有人编号,联系人拥有人，默认与建立人一致
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 建立人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailContactDescriptor
    public partial class MailContactDescriptor:ObjectDescriptorBase
    {
     
        public MailContactDescriptor(string prefix):base(prefix)
        {  
    
            this._MailContactId = new PropertyExpression(prefix + "MailContactId");
            this._EMail = new PropertyExpression(prefix + "EMail");
            this._ContactName = new PropertyExpression(prefix + "ContactName");
            this._ContactPinyin = new PropertyExpression(prefix + "ContactPinyin");
            this._Address = new PropertyExpression(prefix + "Address");
            this._Tel = new PropertyExpression(prefix + "Tel");
            this._Fax = new PropertyExpression(prefix + "Fax");
            this._Mobile = new PropertyExpression(prefix + "Mobile");
            this._Postalcode = new PropertyExpression(prefix + "Postalcode");
            this._QQ = new PropertyExpression(prefix + "QQ");
            this._WeChat = new PropertyExpression(prefix + "WeChat");
            this._Facebook = new PropertyExpression(prefix + "Facebook");
            this._Twitter = new PropertyExpression(prefix + "Twitter");
            this._Skype = new PropertyExpression(prefix + "Skype");
            this._CompanyName = new PropertyExpression(prefix + "CompanyName");
            this._Post = new PropertyExpression(prefix + "Post");
            this._Area = new PropertyExpression(prefix + "Area");
            this._CountryId = new PropertyExpression(prefix + "CountryId");
            this._Memo = new PropertyExpression(prefix + "Memo");
            this._LastContactTime = new PropertyExpression(prefix + "LastContactTime");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailContactId,this._EMail,this._ContactName,this._ContactPinyin,this._Address,this._Tel,this._Fax,this._Mobile,this._Postalcode,this._QQ,this._WeChat,this._Facebook,this._Twitter,this._Skype,this._CompanyName,this._Post,this._Area,this._CountryId,this._Memo,this._LastContactTime,this._OwnerUID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailContactId;
        /// <summary>
        /// 邮件联系人主键ID,
        /// </summary>
        public PropertyExpression MailContactId { get{return _MailContactId;}}
        private PropertyExpression _EMail;
        /// <summary>
        /// 邮箱地址,
        /// </summary>
        public PropertyExpression EMail { get{return _EMail;}}
        private PropertyExpression _ContactName;
        /// <summary>
        /// 联系人,
        /// </summary>
        public PropertyExpression ContactName { get{return _ContactName;}}
        private PropertyExpression _ContactPinyin;
        /// <summary>
        /// 联系人拼音,
        /// </summary>
        public PropertyExpression ContactPinyin { get{return _ContactPinyin;}}
        private PropertyExpression _Address;
        /// <summary>
        /// 联系地址,
        /// </summary>
        public PropertyExpression Address { get{return _Address;}}
        private PropertyExpression _Tel;
        /// <summary>
        /// 电话,
        /// </summary>
        public PropertyExpression Tel { get{return _Tel;}}
        private PropertyExpression _Fax;
        /// <summary>
        /// 传真,传真号
        /// </summary>
        public PropertyExpression Fax { get{return _Fax;}}
        private PropertyExpression _Mobile;
        /// <summary>
        /// 手机,
        /// </summary>
        public PropertyExpression Mobile { get{return _Mobile;}}
        private PropertyExpression _Postalcode;
        /// <summary>
        /// 邮编,
        /// </summary>
        public PropertyExpression Postalcode { get{return _Postalcode;}}
        private PropertyExpression _QQ;
        /// <summary>
        /// QQ,
        /// </summary>
        public PropertyExpression QQ { get{return _QQ;}}
        private PropertyExpression _WeChat;
        /// <summary>
        /// 微信,
        /// </summary>
        public PropertyExpression WeChat { get{return _WeChat;}}
        private PropertyExpression _Facebook;
        /// <summary>
        /// Facebook,
        /// </summary>
        public PropertyExpression Facebook { get{return _Facebook;}}
        private PropertyExpression _Twitter;
        /// <summary>
        /// Twitter,
        /// </summary>
        public PropertyExpression Twitter { get{return _Twitter;}}
        private PropertyExpression _Skype;
        /// <summary>
        /// Skype号,
        /// </summary>
        public PropertyExpression Skype { get{return _Skype;}}
        private PropertyExpression _CompanyName;
        /// <summary>
        /// 公司名称,
        /// </summary>
        public PropertyExpression CompanyName { get{return _CompanyName;}}
        private PropertyExpression _Post;
        /// <summary>
        /// 公司职位,
        /// </summary>
        public PropertyExpression Post { get{return _Post;}}
        private PropertyExpression _Area;
        /// <summary>
        /// 区域,
        /// </summary>
        public PropertyExpression Area { get{return _Area;}}
        private PropertyExpression _CountryId;
        /// <summary>
        /// 国别编号,
        /// </summary>
        public PropertyExpression CountryId { get{return _CountryId;}}
        private PropertyExpression _Memo;
        /// <summary>
        /// 备注,
        /// </summary>
        public PropertyExpression Memo { get{return _Memo;}}
        private PropertyExpression _LastContactTime;
        /// <summary>
        /// 最后联系时间,最后一次联系时间
        /// </summary>
        public PropertyExpression LastContactTime { get{return _LastContactTime;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 拥有人编号,联系人拥有人，默认与建立人一致
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 建立人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



    }
     #endregion


    #region MailContacts
    /// <summary>
    /// 邮件系统 - 邮箱联系人,邮件系统 - 邮箱联系人表
    /// </summary>
    [DisplayName("邮件系统 - 邮箱联系人")]
    [Table]
    public partial class MailContacts:EntityList<MailContact> 
    {
        
    }
    #endregion


    #region MailFilterCondition
    /// <summary>
    /// 邮件系统 - 邮箱邮件过滤器,邮件过滤器条件
    /// </summary>
    [DisplayName("邮件系统 - 邮箱邮件过滤器")]
    [Table]
    public partial class MailFilterCondition 
    {
        
        public MailFilterCondition()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 过滤器条件ID,
        /// </summary>
        [DisplayName("过滤器条件ID")]
        [Column(IsPrimaryKey = true)]
        [SQLiteColumn(IsIdentity = true)]
        public int FilterConditionId {  get; set; }


        /// <summary>
        /// 过滤规则名称,
        /// </summary>
        [DisplayName("过滤规则名称")]
        [Column(Size = 100)] 
        public string FilterName {  get; set; }


        /// <summary>
        /// 顺序号,顺序号,顺序
        /// </summary>
        [DisplayName("顺序号")]
        [Column()] 
        public int SortNumber {  get; set; }


        /// <summary>
        /// 邮箱编号ID,
        /// </summary>
        [DisplayName("邮箱编号ID")]
        [Column(Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 过滤执行时机,过滤执行时机,收件时执行还是发送执行等, 0=收取时执行(默认值)、1=发件时执行、2=收取、发件都执行
        /// </summary>
        [DisplayName("过滤执行时机")]
        [Column()] 
        public int FilterDoTime {  get; set; }


        /// <summary>
        /// 过滤执行条件,过滤执行条件，0=满足所有条件、1=满足任一条件
        /// </summary>
        [DisplayName("过滤执行条件")]
        [Column()] 
        public int ConditionOpertation {  get; set; }


        /// <summary>
        /// 过滤条件规则,过滤条件,条件对象（json 字符串）
        /// </summary>
        [DisplayName("过滤条件规则")]
        [Column(Size = 8000)] 
        public string FilterConditions {  get; set; }


        /// <summary>
        /// 执行动作,执行动作,条件对象（json 字符串）
        /// </summary>
        [DisplayName("执行动作")]
        [Column(Size = 8000)] 
        public string FilterEvents {  get; set; }


        /// <summary>
        /// 停止处理其他规则,
        /// </summary>
        [DisplayName("停止处理其他规则")]
        [Column()] 
        public bool IsnoreOther {  get; set; }


        /// <summary>
        /// 是否启用,是否启用过滤，true启用；false 未启用
        /// </summary>
        [DisplayName("是否启用")]
        [Column()] 
        public bool IsFilter {  get; set; }


        /// <summary>
        /// 拥有者编号,拥有者编号ID
        /// </summary>
        [DisplayName("拥有者编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

    }
    #endregion
    #region MailFilterConditionProperties
    public static partial class MailFilterCondition_
    {
    
        private static MailFilterConditionDescriptor instance = new MailFilterConditionDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailFilterCondition";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 过滤器条件ID,
        /// </summary>
        public static PropertyExpression FilterConditionId { get{return instance.FilterConditionId;}} 
        /// <summary>
        /// 过滤规则名称,
        /// </summary>
        public static PropertyExpression FilterName { get{return instance.FilterName;}} 
        /// <summary>
        /// 顺序号,顺序号,顺序
        /// </summary>
        public static PropertyExpression SortNumber { get{return instance.SortNumber;}} 
        /// <summary>
        /// 邮箱编号ID,
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 过滤执行时机,过滤执行时机,收件时执行还是发送执行等, 0=收取时执行(默认值)、1=发件时执行、2=收取、发件都执行
        /// </summary>
        public static PropertyExpression FilterDoTime { get{return instance.FilterDoTime;}} 
        /// <summary>
        /// 过滤执行条件,过滤执行条件，0=满足所有条件、1=满足任一条件
        /// </summary>
        public static PropertyExpression ConditionOpertation { get{return instance.ConditionOpertation;}} 
        /// <summary>
        /// 过滤条件规则,过滤条件,条件对象（json 字符串）
        /// </summary>
        public static PropertyExpression FilterConditions { get{return instance.FilterConditions;}} 
        /// <summary>
        /// 执行动作,执行动作,条件对象（json 字符串）
        /// </summary>
        public static PropertyExpression FilterEvents { get{return instance.FilterEvents;}} 
        /// <summary>
        /// 停止处理其他规则,
        /// </summary>
        public static PropertyExpression IsnoreOther { get{return instance.IsnoreOther;}} 
        /// <summary>
        /// 是否启用,是否启用过滤，true启用；false 未启用
        /// </summary>
        public static PropertyExpression IsFilter { get{return instance.IsFilter;}} 
        /// <summary>
        /// 拥有者编号,拥有者编号ID
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailFilterConditionDescriptor
    public partial class MailFilterConditionDescriptor:ObjectDescriptorBase
    {
     
        public MailFilterConditionDescriptor(string prefix):base(prefix)
        {  
    
            this._FilterConditionId = new PropertyExpression(prefix + "FilterConditionId");
            this._FilterName = new PropertyExpression(prefix + "FilterName");
            this._SortNumber = new PropertyExpression(prefix + "SortNumber");
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._FilterDoTime = new PropertyExpression(prefix + "FilterDoTime");
            this._ConditionOpertation = new PropertyExpression(prefix + "ConditionOpertation");
            this._FilterConditions = new PropertyExpression(prefix + "FilterConditions");
            this._FilterEvents = new PropertyExpression(prefix + "FilterEvents");
            this._IsnoreOther = new PropertyExpression(prefix + "IsnoreOther");
            this._IsFilter = new PropertyExpression(prefix + "IsFilter");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._FilterConditionId,this._FilterName,this._SortNumber,this._MailBoxId,this._FilterDoTime,this._ConditionOpertation,this._FilterConditions,this._FilterEvents,this._IsnoreOther,this._IsFilter,this._OwnerUID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _FilterConditionId;
        /// <summary>
        /// 过滤器条件ID,
        /// </summary>
        public PropertyExpression FilterConditionId { get{return _FilterConditionId;}}
        private PropertyExpression _FilterName;
        /// <summary>
        /// 过滤规则名称,
        /// </summary>
        public PropertyExpression FilterName { get{return _FilterName;}}
        private PropertyExpression _SortNumber;
        /// <summary>
        /// 顺序号,顺序号,顺序
        /// </summary>
        public PropertyExpression SortNumber { get{return _SortNumber;}}
        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 邮箱编号ID,
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _FilterDoTime;
        /// <summary>
        /// 过滤执行时机,过滤执行时机,收件时执行还是发送执行等, 0=收取时执行(默认值)、1=发件时执行、2=收取、发件都执行
        /// </summary>
        public PropertyExpression FilterDoTime { get{return _FilterDoTime;}}
        private PropertyExpression _ConditionOpertation;
        /// <summary>
        /// 过滤执行条件,过滤执行条件，0=满足所有条件、1=满足任一条件
        /// </summary>
        public PropertyExpression ConditionOpertation { get{return _ConditionOpertation;}}
        private PropertyExpression _FilterConditions;
        /// <summary>
        /// 过滤条件规则,过滤条件,条件对象（json 字符串）
        /// </summary>
        public PropertyExpression FilterConditions { get{return _FilterConditions;}}
        private PropertyExpression _FilterEvents;
        /// <summary>
        /// 执行动作,执行动作,条件对象（json 字符串）
        /// </summary>
        public PropertyExpression FilterEvents { get{return _FilterEvents;}}
        private PropertyExpression _IsnoreOther;
        /// <summary>
        /// 停止处理其他规则,
        /// </summary>
        public PropertyExpression IsnoreOther { get{return _IsnoreOther;}}
        private PropertyExpression _IsFilter;
        /// <summary>
        /// 是否启用,是否启用过滤，true启用；false 未启用
        /// </summary>
        public PropertyExpression IsFilter { get{return _IsFilter;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 拥有者编号,拥有者编号ID
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



    }
     #endregion


    #region MailFilterConditions
    /// <summary>
    /// 邮件系统 - 邮箱邮件过滤器,邮件过滤器条件
    /// </summary>
    [DisplayName("邮件系统 - 邮箱邮件过滤器")]
    [Table]
    public partial class MailFilterConditions:EntityList<MailFilterCondition> 
    {
        
    }
    #endregion


    #region MailFolder
    /// <summary>
    /// 邮件系统 - 邮箱文件夹,邮件文件夹
    /// </summary>
    [DisplayName("邮件系统 - 邮箱文件夹")]
    [Table]
    public partial class MailFolder 
    {
        
        public MailFolder()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 、,邮箱文件夹主键ID
        /// </summary>
        [DisplayName("、")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailFolderId {  get; set; }


        /// <summary>
        /// 邮箱主键ID,
        /// </summary>
        [DisplayName("邮箱主键ID")]
        [Column(Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 文件夹名称,
        /// </summary>
        [DisplayName("文件夹名称")]
        [Column(Size = 128)] 
        public string FolderName {  get; set; }


        /// <summary>
        /// 排序号,顺序
        /// </summary>
        [DisplayName("排序号")]
        [Column()] 
        public int Sorting {  get; set; }


        /// <summary>
        /// 上级文件夹ID,
        /// </summary>
        [DisplayName("上级文件夹ID")]
        [Column(Size = 50)] 
        public string ParentId {  get; set; }


        /// <summary>
        /// 文件夹类型,99收件,97=发件,95= 草稿,93=垃圾箱,91=已删除; 默认是0（表示自定义文件夹）
        /// </summary>
        [DisplayName("文件夹类型")]
        [Column()] 
        public int MailType {  get; set; }


        /// <summary>
        /// 邮件数,邮件总数,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        [DisplayName("邮件数")]
        [Column()] 
        public int MailCount {  get; set; }


        /// <summary>
        /// 未读数量,未读数量,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        [DisplayName("未读数量")]
        [Column()] 
        public int UnreadCount {  get; set; }


        /// <summary>
        /// 深度,深度，默认1
        /// </summary>
        [DisplayName("深度")]
        [Column()] 
        public int Depth {  get; set; }


        /// <summary>
        /// 来源表,来源(绑定)表，用于区分当前文件夹属于那个表及类型，如属于邮箱自带的内置收发件箱，还是客户分组/邮件标签等归档文件夹
        /// </summary>
        [DisplayName("来源表")]
        [Column(Size = 100)] 
        public string SourceTable {  get; set; }


        /// <summary>
        /// 来源表ID,
        /// </summary>
        [DisplayName("来源表ID")]
        [Column(Size = 50)] 
        public string SourceId {  get; set; }


        /// <summary>
        /// 邮局文件夹名称,邮局文件夹名称,文件全名(如果存在父文件夹,则包括父文件夹名称),IMAP协议;邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        [DisplayName("邮局文件夹名称")]
        [Column(Size = 1000)] 
        public string ServerFullFolderName {  get; set; }


        /// <summary>
        /// 最新邮件时间,最新邮件时间,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        [DisplayName("最新邮件时间")]
        [Column()] 
        public DateTime? LastMailTime {  get; set; }


        /// <summary>
        /// 拥有人编号,
        /// </summary>
        [DisplayName("拥有人编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮箱文件夹.邮箱表,邮箱文件夹.邮箱表
        /// </summary>
        [Relation("this.MailBoxId=out.MailBoxId")]
        public MailBox MailBox { get;  set;  } 



        /// <summary>
        /// 邮箱文件夹.上级邮箱文件夹,邮箱文件夹.上级邮箱文件夹
        /// </summary>
        [Relation("this.ParentId=out.MailFolderId")]
        public MailFolder ParentMailFolder { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailFolderProperties
    public static partial class MailFolder_
    {
    
        private static MailFolderDescriptor instance = new MailFolderDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailFolder";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 、,邮箱文件夹主键ID
        /// </summary>
        public static PropertyExpression MailFolderId { get{return instance.MailFolderId;}} 
        /// <summary>
        /// 邮箱主键ID,
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 文件夹名称,
        /// </summary>
        public static PropertyExpression FolderName { get{return instance.FolderName;}} 
        /// <summary>
        /// 排序号,顺序
        /// </summary>
        public static PropertyExpression Sorting { get{return instance.Sorting;}} 
        /// <summary>
        /// 上级文件夹ID,
        /// </summary>
        public static PropertyExpression ParentId { get{return instance.ParentId;}} 
        /// <summary>
        /// 文件夹类型,99收件,97=发件,95= 草稿,93=垃圾箱,91=已删除; 默认是0（表示自定义文件夹）
        /// </summary>
        public static PropertyExpression MailType { get{return instance.MailType;}} 
        /// <summary>
        /// 邮件数,邮件总数,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public static PropertyExpression MailCount { get{return instance.MailCount;}} 
        /// <summary>
        /// 未读数量,未读数量,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public static PropertyExpression UnreadCount { get{return instance.UnreadCount;}} 
        /// <summary>
        /// 深度,深度，默认1
        /// </summary>
        public static PropertyExpression Depth { get{return instance.Depth;}} 
        /// <summary>
        /// 来源表,来源(绑定)表，用于区分当前文件夹属于那个表及类型，如属于邮箱自带的内置收发件箱，还是客户分组/邮件标签等归档文件夹
        /// </summary>
        public static PropertyExpression SourceTable { get{return instance.SourceTable;}} 
        /// <summary>
        /// 来源表ID,
        /// </summary>
        public static PropertyExpression SourceId { get{return instance.SourceId;}} 
        /// <summary>
        /// 邮局文件夹名称,邮局文件夹名称,文件全名(如果存在父文件夹,则包括父文件夹名称),IMAP协议;邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public static PropertyExpression ServerFullFolderName { get{return instance.ServerFullFolderName;}} 
        /// <summary>
        /// 最新邮件时间,最新邮件时间,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public static PropertyExpression LastMailTime { get{return instance.LastMailTime;}} 
        /// <summary>
        /// 拥有人编号,
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}


 
        /// <summary>
        /// 邮箱文件夹.邮箱表,邮箱文件夹.邮箱表
        /// </summary>
        public static MailBoxDescriptor MailBox { get{return instance.MailBox;}} 
        /// <summary>
        /// 邮箱文件夹.上级邮箱文件夹,邮箱文件夹.上级邮箱文件夹
        /// </summary>
        public static MailFolderDescriptor ParentMailFolder { get{return instance.ParentMailFolder;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailFolderDescriptor
    public partial class MailFolderDescriptor:ObjectDescriptorBase
    {
     
        public MailFolderDescriptor(string prefix):base(prefix)
        {  
    
            this._MailFolderId = new PropertyExpression(prefix + "MailFolderId");
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._FolderName = new PropertyExpression(prefix + "FolderName");
            this._Sorting = new PropertyExpression(prefix + "Sorting");
            this._ParentId = new PropertyExpression(prefix + "ParentId");
            this._MailType = new PropertyExpression(prefix + "MailType");
            this._MailCount = new PropertyExpression(prefix + "MailCount");
            this._UnreadCount = new PropertyExpression(prefix + "UnreadCount");
            this._Depth = new PropertyExpression(prefix + "Depth");
            this._SourceTable = new PropertyExpression(prefix + "SourceTable");
            this._SourceId = new PropertyExpression(prefix + "SourceId");
            this._ServerFullFolderName = new PropertyExpression(prefix + "ServerFullFolderName");
            this._LastMailTime = new PropertyExpression(prefix + "LastMailTime");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailFolderId,this._MailBoxId,this._FolderName,this._Sorting,this._ParentId,this._MailType,this._MailCount,this._UnreadCount,this._Depth,this._SourceTable,this._SourceId,this._ServerFullFolderName,this._LastMailTime,this._OwnerUID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailFolderId;
        /// <summary>
        /// 、,邮箱文件夹主键ID
        /// </summary>
        public PropertyExpression MailFolderId { get{return _MailFolderId;}}
        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 邮箱主键ID,
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _FolderName;
        /// <summary>
        /// 文件夹名称,
        /// </summary>
        public PropertyExpression FolderName { get{return _FolderName;}}
        private PropertyExpression _Sorting;
        /// <summary>
        /// 排序号,顺序
        /// </summary>
        public PropertyExpression Sorting { get{return _Sorting;}}
        private PropertyExpression _ParentId;
        /// <summary>
        /// 上级文件夹ID,
        /// </summary>
        public PropertyExpression ParentId { get{return _ParentId;}}
        private PropertyExpression _MailType;
        /// <summary>
        /// 文件夹类型,99收件,97=发件,95= 草稿,93=垃圾箱,91=已删除; 默认是0（表示自定义文件夹）
        /// </summary>
        public PropertyExpression MailType { get{return _MailType;}}
        private PropertyExpression _MailCount;
        /// <summary>
        /// 邮件数,邮件总数,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public PropertyExpression MailCount { get{return _MailCount;}}
        private PropertyExpression _UnreadCount;
        /// <summary>
        /// 未读数量,未读数量,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public PropertyExpression UnreadCount { get{return _UnreadCount;}}
        private PropertyExpression _Depth;
        /// <summary>
        /// 深度,深度，默认1
        /// </summary>
        public PropertyExpression Depth { get{return _Depth;}}
        private PropertyExpression _SourceTable;
        /// <summary>
        /// 来源表,来源(绑定)表，用于区分当前文件夹属于那个表及类型，如属于邮箱自带的内置收发件箱，还是客户分组/邮件标签等归档文件夹
        /// </summary>
        public PropertyExpression SourceTable { get{return _SourceTable;}}
        private PropertyExpression _SourceId;
        /// <summary>
        /// 来源表ID,
        /// </summary>
        public PropertyExpression SourceId { get{return _SourceId;}}
        private PropertyExpression _ServerFullFolderName;
        /// <summary>
        /// 邮局文件夹名称,邮局文件夹名称,文件全名(如果存在父文件夹,则包括父文件夹名称),IMAP协议;邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public PropertyExpression ServerFullFolderName { get{return _ServerFullFolderName;}}
        private PropertyExpression _LastMailTime;
        /// <summary>
        /// 最新邮件时间,最新邮件时间,邮箱文件夹类别(SourceTable)时值有效
        /// </summary>
        public PropertyExpression LastMailTime { get{return _LastMailTime;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 拥有人编号,
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



        private MailBoxDescriptor _MailBox;
        public MailBoxDescriptor MailBox 
        { 
            get
            {
                if(_MailBox==null) _MailBox=new MailBoxDescriptor(base.Prefix+"MailBox.");
                return _MailBox;
            }
        }
        private MailFolderDescriptor _ParentMailFolder;
        public MailFolderDescriptor ParentMailFolder 
        { 
            get
            {
                if(_ParentMailFolder==null) _ParentMailFolder=new MailFolderDescriptor(base.Prefix+"ParentMailFolder.");
                return _ParentMailFolder;
            }
        }
    }
     #endregion


    #region MailFolders
    /// <summary>
    /// 邮件系统 - 邮箱文件夹,邮件文件夹
    /// </summary>
    [DisplayName("邮件系统 - 邮箱文件夹")]
    [Table]
    public partial class MailFolders:EntityList<MailFolder> 
    {
        
    }
    #endregion


    #region MailGroup
    /// <summary>
    /// 邮件系统 - 邮件收件人,邮件系统 - 邮件收件人列表(收件人、抄送、密送人)
    /// </summary>
    [DisplayName("邮件系统 - 邮件收件人")]
    [Table]
    public partial class MailGroup 
    {
        
        public MailGroup()
        {

        
        }
        #region propertys
        
        /// <summary>
        /// 邮件群发主键ID,邮件群发主键ID,自增
        /// </summary>
        [DisplayName("邮件群发主键ID")]
        [Column(IsPrimaryKey = true)]
        [SQLiteColumn(IsIdentity = true)]
        public int MailGroupId {  get; set; }


        /// <summary>
        /// 邮件ID,
        /// </summary>
        [DisplayName("邮件ID")]
        [Column(Size = 50)] 
        public string MailMainId {  get; set; }


        /// <summary>
        /// 排序号,
        /// </summary>
        [DisplayName("排序号")]
        [Column()] 
        public int Sorting {  get; set; }


        /// <summary>
        /// 收件人类型,收件人类型,收件人=1、抄送人=2、密送=3
        /// </summary>
        [DisplayName("收件人类型")]
        [Column()] 
        public int ReceiveTypeCode {  get; set; }


        /// <summary>
        /// 收件人名称,
        /// </summary>
        [DisplayName("收件人名称")]
        [Column(Size = 128)] 
        public string ReceiveName {  get; set; }


        /// <summary>
        /// 收件地址,
        /// </summary>
        [DisplayName("收件地址")]
        [Column(Size = 128)] 
        public string ReceiveAddress {  get; set; }


        /// <summary>
        /// 是否已发送,是否已经发送
        /// </summary>
        [DisplayName("是否已发送")]
        [Column()] 
        public bool? IsSend {  get; set; }


        /// <summary>
        /// 是否已收到,是否已经收到
        /// </summary>
        [DisplayName("是否已收到")]
        [Column()] 
        public bool? IsReceive {  get; set; }


        /// <summary>
        /// 发送时间,
        /// </summary>
        [DisplayName("发送时间")]
        [Column()] 
        public DateTime? SendTime {  get; set; }


        #endregion

    }
    #endregion
    #region MailGroupProperties
    public static partial class MailGroup_
    {
    
        private static MailGroupDescriptor instance = new MailGroupDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailGroup";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件群发主键ID,邮件群发主键ID,自增
        /// </summary>
        public static PropertyExpression MailGroupId { get{return instance.MailGroupId;}} 
        /// <summary>
        /// 邮件ID,
        /// </summary>
        public static PropertyExpression MailMainId { get{return instance.MailMainId;}} 
        /// <summary>
        /// 排序号,
        /// </summary>
        public static PropertyExpression Sorting { get{return instance.Sorting;}} 
        /// <summary>
        /// 收件人类型,收件人类型,收件人=1、抄送人=2、密送=3
        /// </summary>
        public static PropertyExpression ReceiveTypeCode { get{return instance.ReceiveTypeCode;}} 
        /// <summary>
        /// 收件人名称,
        /// </summary>
        public static PropertyExpression ReceiveName { get{return instance.ReceiveName;}} 
        /// <summary>
        /// 收件地址,
        /// </summary>
        public static PropertyExpression ReceiveAddress { get{return instance.ReceiveAddress;}} 
        /// <summary>
        /// 是否已发送,是否已经发送
        /// </summary>
        public static PropertyExpression IsSend { get{return instance.IsSend;}} 
        /// <summary>
        /// 是否已收到,是否已经收到
        /// </summary>
        public static PropertyExpression IsReceive { get{return instance.IsReceive;}} 
        /// <summary>
        /// 发送时间,
        /// </summary>
        public static PropertyExpression SendTime { get{return instance.SendTime;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailGroupDescriptor
    public partial class MailGroupDescriptor:ObjectDescriptorBase
    {
     
        public MailGroupDescriptor(string prefix):base(prefix)
        {  
    
            this._MailGroupId = new PropertyExpression(prefix + "MailGroupId");
            this._MailMainId = new PropertyExpression(prefix + "MailMainId");
            this._Sorting = new PropertyExpression(prefix + "Sorting");
            this._ReceiveTypeCode = new PropertyExpression(prefix + "ReceiveTypeCode");
            this._ReceiveName = new PropertyExpression(prefix + "ReceiveName");
            this._ReceiveAddress = new PropertyExpression(prefix + "ReceiveAddress");
            this._IsSend = new PropertyExpression(prefix + "IsSend");
            this._IsReceive = new PropertyExpression(prefix + "IsReceive");
            this._SendTime = new PropertyExpression(prefix + "SendTime");
            ALL = new PropertyExpression[] {this._MailGroupId,this._MailMainId,this._Sorting,this._ReceiveTypeCode,this._ReceiveName,this._ReceiveAddress,this._IsSend,this._IsReceive,this._SendTime};
        }
         

        private PropertyExpression _MailGroupId;
        /// <summary>
        /// 邮件群发主键ID,邮件群发主键ID,自增
        /// </summary>
        public PropertyExpression MailGroupId { get{return _MailGroupId;}}
        private PropertyExpression _MailMainId;
        /// <summary>
        /// 邮件ID,
        /// </summary>
        public PropertyExpression MailMainId { get{return _MailMainId;}}
        private PropertyExpression _Sorting;
        /// <summary>
        /// 排序号,
        /// </summary>
        public PropertyExpression Sorting { get{return _Sorting;}}
        private PropertyExpression _ReceiveTypeCode;
        /// <summary>
        /// 收件人类型,收件人类型,收件人=1、抄送人=2、密送=3
        /// </summary>
        public PropertyExpression ReceiveTypeCode { get{return _ReceiveTypeCode;}}
        private PropertyExpression _ReceiveName;
        /// <summary>
        /// 收件人名称,
        /// </summary>
        public PropertyExpression ReceiveName { get{return _ReceiveName;}}
        private PropertyExpression _ReceiveAddress;
        /// <summary>
        /// 收件地址,
        /// </summary>
        public PropertyExpression ReceiveAddress { get{return _ReceiveAddress;}}
        private PropertyExpression _IsSend;
        /// <summary>
        /// 是否已发送,是否已经发送
        /// </summary>
        public PropertyExpression IsSend { get{return _IsSend;}}
        private PropertyExpression _IsReceive;
        /// <summary>
        /// 是否已收到,是否已经收到
        /// </summary>
        public PropertyExpression IsReceive { get{return _IsReceive;}}
        private PropertyExpression _SendTime;
        /// <summary>
        /// 发送时间,
        /// </summary>
        public PropertyExpression SendTime { get{return _SendTime;}}



    }
     #endregion


    #region MailGroups
    /// <summary>
    /// 邮件系统 - 邮件收件人,邮件系统 - 邮件收件人列表(收件人、抄送、密送人)
    /// </summary>
    [DisplayName("邮件系统 - 邮件收件人")]
    [Table]
    public partial class MailGroups:EntityList<MailGroup> 
    {
        
    }
    #endregion


    #region MailJudge
    /// <summary>
    /// 邮件系统 - 邮箱黑名单,邮件 - 邮件拒收(黑名单)表
    /// </summary>
    [DisplayName("邮件系统 - 邮箱黑名单")]
    [Table]
    public partial class MailJudge 
    {
        
        public MailJudge()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮件拒收主键ID,邮件拒收主键id，自动增长
        /// </summary>
        [DisplayName("邮件拒收主键ID")]
        [Column(IsPrimaryKey = true)]
        [SQLiteColumn(IsIdentity = true)]
        public int MailJudgeId {  get; set; }


        /// <summary>
        /// 邮件唯一标识,
        /// </summary>
        [DisplayName("邮件唯一标识")]
        [Column(Size = 100)] 
        public string MessageID {  get; set; }


        /// <summary>
        /// 操作类型,操作类型；1=删除,2=拒收
        /// </summary>
        [DisplayName("操作类型")]
        [Column()] 
        public int OperateType {  get; set; }


        /// <summary>
        /// 邮箱地址,邮箱地址,要拒收/删除的邮箱地址
        /// </summary>
        [DisplayName("邮箱地址")]
        [Column(Size = 50)] 
        public string MailAddress {  get; set; }


        /// <summary>
        /// 邮箱ID,邮箱ID，来源于表MailBox
        /// </summary>
        [DisplayName("邮箱ID")]
        [Column(Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 备注,
        /// </summary>
        [DisplayName("备注")]
        [Column(Size = 256)] 
        public string Memo {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮件拒收表.邮箱表,邮件拒收表.邮箱表
        /// </summary>
        [Relation("this.MailBoxId=out.MailBoxId")]
        public MailBox MailBox { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailJudgeProperties
    public static partial class MailJudge_
    {
    
        private static MailJudgeDescriptor instance = new MailJudgeDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailJudge";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件拒收主键ID,邮件拒收主键id，自动增长
        /// </summary>
        public static PropertyExpression MailJudgeId { get{return instance.MailJudgeId;}} 
        /// <summary>
        /// 邮件唯一标识,
        /// </summary>
        public static PropertyExpression MessageID { get{return instance.MessageID;}} 
        /// <summary>
        /// 操作类型,操作类型；1=删除,2=拒收
        /// </summary>
        public static PropertyExpression OperateType { get{return instance.OperateType;}} 
        /// <summary>
        /// 邮箱地址,邮箱地址,要拒收/删除的邮箱地址
        /// </summary>
        public static PropertyExpression MailAddress { get{return instance.MailAddress;}} 
        /// <summary>
        /// 邮箱ID,邮箱ID，来源于表MailBox
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 备注,
        /// </summary>
        public static PropertyExpression Memo { get{return instance.Memo;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}


 
        /// <summary>
        /// 邮件拒收表.邮箱表,邮件拒收表.邮箱表
        /// </summary>
        public static MailBoxDescriptor MailBox { get{return instance.MailBox;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailJudgeDescriptor
    public partial class MailJudgeDescriptor:ObjectDescriptorBase
    {
     
        public MailJudgeDescriptor(string prefix):base(prefix)
        {  
    
            this._MailJudgeId = new PropertyExpression(prefix + "MailJudgeId");
            this._MessageID = new PropertyExpression(prefix + "MessageID");
            this._OperateType = new PropertyExpression(prefix + "OperateType");
            this._MailAddress = new PropertyExpression(prefix + "MailAddress");
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._Memo = new PropertyExpression(prefix + "Memo");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailJudgeId,this._MessageID,this._OperateType,this._MailAddress,this._MailBoxId,this._Memo,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailJudgeId;
        /// <summary>
        /// 邮件拒收主键ID,邮件拒收主键id，自动增长
        /// </summary>
        public PropertyExpression MailJudgeId { get{return _MailJudgeId;}}
        private PropertyExpression _MessageID;
        /// <summary>
        /// 邮件唯一标识,
        /// </summary>
        public PropertyExpression MessageID { get{return _MessageID;}}
        private PropertyExpression _OperateType;
        /// <summary>
        /// 操作类型,操作类型；1=删除,2=拒收
        /// </summary>
        public PropertyExpression OperateType { get{return _OperateType;}}
        private PropertyExpression _MailAddress;
        /// <summary>
        /// 邮箱地址,邮箱地址,要拒收/删除的邮箱地址
        /// </summary>
        public PropertyExpression MailAddress { get{return _MailAddress;}}
        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 邮箱ID,邮箱ID，来源于表MailBox
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _Memo;
        /// <summary>
        /// 备注,
        /// </summary>
        public PropertyExpression Memo { get{return _Memo;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



        private MailBoxDescriptor _MailBox;
        public MailBoxDescriptor MailBox 
        { 
            get
            {
                if(_MailBox==null) _MailBox=new MailBoxDescriptor(base.Prefix+"MailBox.");
                return _MailBox;
            }
        }
    }
     #endregion


    #region MailJudges
    /// <summary>
    /// 邮件系统 - 邮箱黑名单,邮件 - 邮件拒收(黑名单)表
    /// </summary>
    [DisplayName("邮件系统 - 邮箱黑名单")]
    [Table]
    public partial class MailJudges:EntityList<MailJudge> 
    {
        
    }
    #endregion


    #region MailMain
    /// <summary>
    /// 邮件系统 - 邮件,邮件系统----邮件主表 
    /// </summary>
    [DisplayName("邮件系统 - 邮件")]
    [Table]
    public partial class MailMain 
    {
        
        public MailMain()
        {

            MailTime=DateTime.Now;
            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮件主键ID,
        /// </summary>
        [DisplayName("邮件主键ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailMainId {  get; set; }


        /// <summary>
        /// 邮箱ID,
        /// </summary>
        [DisplayName("邮箱ID")]
        [Column(Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 邮件唯一标志码,邮件邮局唯一UID
        /// </summary>
        [DisplayName("邮件唯一标志码")]
        [Column(Size = 100)] 
        public string MessageId {  get; set; }


        /// <summary>
        /// 邮件类型编号,1=收件，2=发件
        /// </summary>
        [DisplayName("邮件类型编号")]
        [Column()] 
        public int MailType {  get; set; }


        /// <summary>
        /// 邮件主题,
        /// </summary>
        [DisplayName("邮件主题")]
        [Column(Size = 512)] 
        public string Subject {  get; set; }


        /// <summary>
        /// 发件人,发件人,格式如:wzh<wzh@qq.com>
        /// </summary>
        [DisplayName("发件人")]
        [Column(Size = 256)] 
        public string Sender {  get; set; }


        /// <summary>
        /// 收件人,收件人,格式如:wzh<wzh@qq.com>;devin<devin@qq.com>
        /// </summary>
        [DisplayName("收件人")]
        [Column(Size = 4000)] 
        public string Receiver {  get; set; }


        /// <summary>
        /// 抄送,抄送,格式如收件人
        /// </summary>
        [DisplayName("抄送")]
        [Column(Size = 4000)] 
        public string Cc {  get; set; }


        /// <summary>
        /// 密送地址,密送地址,格式如收件人
        /// </summary>
        [DisplayName("密送地址")]
        [Column(Size = 4000)] 
        public string Bcc {  get; set; }


        /// <summary>
        /// 已读回执,
        /// </summary>
        [DisplayName("已读回执")]
        [Column()] 
        public bool IsReadReply {  get; set; }


        /// <summary>
        /// 回复地址,回复地址,对方收件发送已读回执地址
        /// </summary>
        [DisplayName("回复地址")]
        [Column(Size = 4000)] 
        public string Reply {  get; set; }


        /// <summary>
        /// 发送等级,重要程度,邮件优先级; 0=未定义、1=极高、2=高、3=正常、4=低、5=极低
        /// 
        /// </summary>
        [DisplayName("发送等级")]
        [Column()] 
        public int? Importance {  get; set; }


        /// <summary>
        /// 邮件时间,邮件时间,收件时间\发件时间
        /// </summary>
        [DisplayName("邮件时间")]
        [Column()] 
        public DateTime MailTime {  get; set; }


        /// <summary>
        /// 附件数量,附件数量,如不存在附件则为0
        /// </summary>
        [DisplayName("附件数量")]
        [Column()] 
        public int AttachCount {  get; set; }


        /// <summary>
        /// 回复次数,回复次数,FromId邮件
        /// </summary>
        [DisplayName("回复次数")]
        [Column()] 
        public int ReplyCount {  get; set; }


        /// <summary>
        /// 转发次数,转发次数,FromId邮件
        /// </summary>
        [DisplayName("转发次数")]
        [Column()] 
        public int ForwardCount {  get; set; }


        /// <summary>
        /// 来源邮件ID,回复/转发的邮件ID
        /// </summary>
        [DisplayName("来源邮件ID")]
        [Column(Size = 50)] 
        public string FromId {  get; set; }


        /// <summary>
        /// 来源类型编号,1=回复，2=转发
        /// 
        /// </summary>
        [DisplayName("来源类型编号")]
        [Column()] 
        public int? FromTypeId {  get; set; }


        /// <summary>
        /// 邮件大小,邮件大小,单位byte
        /// </summary>
        [DisplayName("邮件大小")]
        [Column()] 
        public int? MailSize {  get; set; }


        /// <summary>
        /// 是否已读,是否已读，收件时有效,发件默认设置为已读状态标识
        /// </summary>
        [DisplayName("是否已读")]
        [Column()] 
        public bool Viewed {  get; set; }


        /// <summary>
        /// 分别发送,是否群发,分别发送
        /// </summary>
        [DisplayName("分别发送")]
        [Column()] 
        public bool IsGroup {  get; set; }


        /// <summary>
        /// 是否收发完成,是否收发完成,主要用于分别发送
        /// </summary>
        [DisplayName("是否收发完成")]
        [Column()] 
        public bool IsComplete {  get; set; }


        /// <summary>
        /// 是否备注,是否打了备注
        /// </summary>
        [DisplayName("是否备注")]
        [Column()] 
        public bool IsMemo {  get; set; }


        /// <summary>
        /// 邮件备注,此字段用于员工对该邮件进行备注说明(此次沟通说明)
        /// </summary>
        [DisplayName("邮件备注")]
        [Column(Size = 256)] 
        public string Memo {  get; set; }


        /// <summary>
        /// 置顶邮件,置顶(星标)邮件,true 置顶
        /// </summary>
        [DisplayName("置顶邮件")]
        [Column()] 
        public bool? IsTop {  get; set; }


        /// <summary>
        /// 是否定时发送,
        /// </summary>
        [DisplayName("是否定时发送")]
        [Column()] 
        public bool? IsTimer {  get; set; }


        /// <summary>
        /// 定时发送时间,定时发送时间,IsTimer=true时值有意义
        /// </summary>
        [DisplayName("定时发送时间")]
        [Column()] 
        public DateTime? TimerSendTime {  get; set; }


        /// <summary>
        /// 邮件状态,邮件状态: SEND_SUCCESS = 发送成功；WAIT_SEND=待发送；SENDING=发送中；SEND_FAIL=发送失败待重新发送；SEND_FAIL_END=发送失败不再发送
        /// 
        /// </summary>
        [DisplayName("邮件状态")]
        [Column(Size = 30)] 
        public string MailState {  get; set; }


        /// <summary>
        /// 邮件标签,邮件标签信息,邮件已打标签信息对象
        /// </summary>
        [DisplayName("邮件标签")]
        [Column(Size = 8000)] 
        public string LabelInfo {  get; set; }


        /// <summary>
        /// 是否已归档,是否已归档(客户邮件归档)
        /// </summary>
        [DisplayName("是否已归档")]
        [Column()] 
        public bool IsArchived {  get; set; }


        /// <summary>
        /// 是否跟踪,
        /// </summary>
        [DisplayName("是否跟踪")]
        [Column()] 
        public bool? IsTracking {  get; set; }


        /// <summary>
        /// 是否同步,是否同步;是否上传云端(我司平台),true 已上传; false 未上传
        /// </summary>
        [DisplayName("是否同步")]
        [Column()] 
        public bool IsSync {  get; set; }


        /// <summary>
        /// 是否删除,是否已删除，true 删除；false 未删除(默认值)
        /// </summary>
        [DisplayName("是否删除")]
        [Column()] 
        public bool Deleted {  get; set; }


        /// <summary>
        /// 拥有人编号,
        /// </summary>
        [DisplayName("拥有人编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮件表.邮件正文表,邮件表.邮件正文表
        /// </summary>
        [Relation("this.MailMainId=out.MailMainId")]
        public MailBody MailBody { get;  set;  } 



        /// <summary>
        /// 邮件表.邮箱表,邮件表.邮箱表
        /// </summary>
        [Relation("this.MailBoxId=out.MailBoxId")]
        public MailBox MailBox { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailMainProperties
    public static partial class MailMain_
    {
    
        private static MailMainDescriptor instance = new MailMainDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailMain";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件主键ID,
        /// </summary>
        public static PropertyExpression MailMainId { get{return instance.MailMainId;}} 
        /// <summary>
        /// 邮箱ID,
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 邮件唯一标志码,邮件邮局唯一UID
        /// </summary>
        public static PropertyExpression MessageId { get{return instance.MessageId;}} 
        /// <summary>
        /// 邮件类型编号,1=收件，2=发件
        /// </summary>
        public static PropertyExpression MailType { get{return instance.MailType;}} 
        /// <summary>
        /// 邮件主题,
        /// </summary>
        public static PropertyExpression Subject { get{return instance.Subject;}} 
        /// <summary>
        /// 发件人,发件人,格式如:wzh<wzh@qq.com>
        /// </summary>
        public static PropertyExpression Sender { get{return instance.Sender;}} 
        /// <summary>
        /// 收件人,收件人,格式如:wzh<wzh@qq.com>;devin<devin@qq.com>
        /// </summary>
        public static PropertyExpression Receiver { get{return instance.Receiver;}} 
        /// <summary>
        /// 抄送,抄送,格式如收件人
        /// </summary>
        public static PropertyExpression Cc { get{return instance.Cc;}} 
        /// <summary>
        /// 密送地址,密送地址,格式如收件人
        /// </summary>
        public static PropertyExpression Bcc { get{return instance.Bcc;}} 
        /// <summary>
        /// 已读回执,
        /// </summary>
        public static PropertyExpression IsReadReply { get{return instance.IsReadReply;}} 
        /// <summary>
        /// 回复地址,回复地址,对方收件发送已读回执地址
        /// </summary>
        public static PropertyExpression Reply { get{return instance.Reply;}} 
        /// <summary>
        /// 发送等级,重要程度,邮件优先级; 0=未定义、1=极高、2=高、3=正常、4=低、5=极低
        /// 
        /// </summary>
        public static PropertyExpression Importance { get{return instance.Importance;}} 
        /// <summary>
        /// 邮件时间,邮件时间,收件时间\发件时间
        /// </summary>
        public static PropertyExpression MailTime { get{return instance.MailTime;}} 
        /// <summary>
        /// 附件数量,附件数量,如不存在附件则为0
        /// </summary>
        public static PropertyExpression AttachCount { get{return instance.AttachCount;}} 
        /// <summary>
        /// 回复次数,回复次数,FromId邮件
        /// </summary>
        public static PropertyExpression ReplyCount { get{return instance.ReplyCount;}} 
        /// <summary>
        /// 转发次数,转发次数,FromId邮件
        /// </summary>
        public static PropertyExpression ForwardCount { get{return instance.ForwardCount;}} 
        /// <summary>
        /// 来源邮件ID,回复/转发的邮件ID
        /// </summary>
        public static PropertyExpression FromId { get{return instance.FromId;}} 
        /// <summary>
        /// 来源类型编号,1=回复，2=转发
        /// 
        /// </summary>
        public static PropertyExpression FromTypeId { get{return instance.FromTypeId;}} 
        /// <summary>
        /// 邮件大小,邮件大小,单位byte
        /// </summary>
        public static PropertyExpression MailSize { get{return instance.MailSize;}} 
        /// <summary>
        /// 是否已读,是否已读，收件时有效,发件默认设置为已读状态标识
        /// </summary>
        public static PropertyExpression Viewed { get{return instance.Viewed;}} 
        /// <summary>
        /// 分别发送,是否群发,分别发送
        /// </summary>
        public static PropertyExpression IsGroup { get{return instance.IsGroup;}} 
        /// <summary>
        /// 是否收发完成,是否收发完成,主要用于分别发送
        /// </summary>
        public static PropertyExpression IsComplete { get{return instance.IsComplete;}} 
        /// <summary>
        /// 是否备注,是否打了备注
        /// </summary>
        public static PropertyExpression IsMemo { get{return instance.IsMemo;}} 
        /// <summary>
        /// 邮件备注,此字段用于员工对该邮件进行备注说明(此次沟通说明)
        /// </summary>
        public static PropertyExpression Memo { get{return instance.Memo;}} 
        /// <summary>
        /// 置顶邮件,置顶(星标)邮件,true 置顶
        /// </summary>
        public static PropertyExpression IsTop { get{return instance.IsTop;}} 
        /// <summary>
        /// 是否定时发送,
        /// </summary>
        public static PropertyExpression IsTimer { get{return instance.IsTimer;}} 
        /// <summary>
        /// 定时发送时间,定时发送时间,IsTimer=true时值有意义
        /// </summary>
        public static PropertyExpression TimerSendTime { get{return instance.TimerSendTime;}} 
        /// <summary>
        /// 邮件状态,邮件状态: SEND_SUCCESS = 发送成功；WAIT_SEND=待发送；SENDING=发送中；SEND_FAIL=发送失败待重新发送；SEND_FAIL_END=发送失败不再发送
        /// 
        /// </summary>
        public static PropertyExpression MailState { get{return instance.MailState;}} 
        /// <summary>
        /// 邮件标签,邮件标签信息,邮件已打标签信息对象
        /// </summary>
        public static PropertyExpression LabelInfo { get{return instance.LabelInfo;}} 
        /// <summary>
        /// 是否已归档,是否已归档(客户邮件归档)
        /// </summary>
        public static PropertyExpression IsArchived { get{return instance.IsArchived;}} 
        /// <summary>
        /// 是否跟踪,
        /// </summary>
        public static PropertyExpression IsTracking { get{return instance.IsTracking;}} 
        /// <summary>
        /// 是否同步,是否同步;是否上传云端(我司平台),true 已上传; false 未上传
        /// </summary>
        public static PropertyExpression IsSync { get{return instance.IsSync;}} 
        /// <summary>
        /// 是否删除,是否已删除，true 删除；false 未删除(默认值)
        /// </summary>
        public static PropertyExpression Deleted { get{return instance.Deleted;}} 
        /// <summary>
        /// 拥有人编号,
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}


 
        /// <summary>
        /// 邮件表.邮件正文表,邮件表.邮件正文表
        /// </summary>
        public static MailBodyDescriptor MailBody { get{return instance.MailBody;}} 
        /// <summary>
        /// 邮件表.邮箱表,邮件表.邮箱表
        /// </summary>
        public static MailBoxDescriptor MailBox { get{return instance.MailBox;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailMainDescriptor
    public partial class MailMainDescriptor:ObjectDescriptorBase
    {
     
        public MailMainDescriptor(string prefix):base(prefix)
        {  
    
            this._MailMainId = new PropertyExpression(prefix + "MailMainId");
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._MessageId = new PropertyExpression(prefix + "MessageId");
            this._MailType = new PropertyExpression(prefix + "MailType");
            this._Subject = new PropertyExpression(prefix + "Subject");
            this._Sender = new PropertyExpression(prefix + "Sender");
            this._Receiver = new PropertyExpression(prefix + "Receiver");
            this._Cc = new PropertyExpression(prefix + "Cc");
            this._Bcc = new PropertyExpression(prefix + "Bcc");
            this._IsReadReply = new PropertyExpression(prefix + "IsReadReply");
            this._Reply = new PropertyExpression(prefix + "Reply");
            this._Importance = new PropertyExpression(prefix + "Importance");
            this._MailTime = new PropertyExpression(prefix + "MailTime");
            this._AttachCount = new PropertyExpression(prefix + "AttachCount");
            this._ReplyCount = new PropertyExpression(prefix + "ReplyCount");
            this._ForwardCount = new PropertyExpression(prefix + "ForwardCount");
            this._FromId = new PropertyExpression(prefix + "FromId");
            this._FromTypeId = new PropertyExpression(prefix + "FromTypeId");
            this._MailSize = new PropertyExpression(prefix + "MailSize");
            this._Viewed = new PropertyExpression(prefix + "Viewed");
            this._IsGroup = new PropertyExpression(prefix + "IsGroup");
            this._IsComplete = new PropertyExpression(prefix + "IsComplete");
            this._IsMemo = new PropertyExpression(prefix + "IsMemo");
            this._Memo = new PropertyExpression(prefix + "Memo");
            this._IsTop = new PropertyExpression(prefix + "IsTop");
            this._IsTimer = new PropertyExpression(prefix + "IsTimer");
            this._TimerSendTime = new PropertyExpression(prefix + "TimerSendTime");
            this._MailState = new PropertyExpression(prefix + "MailState");
            this._LabelInfo = new PropertyExpression(prefix + "LabelInfo");
            this._IsArchived = new PropertyExpression(prefix + "IsArchived");
            this._IsTracking = new PropertyExpression(prefix + "IsTracking");
            this._IsSync = new PropertyExpression(prefix + "IsSync");
            this._Deleted = new PropertyExpression(prefix + "Deleted");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailMainId,this._MailBoxId,this._MessageId,this._MailType,this._Subject,this._Sender,this._Receiver,this._Cc,this._Bcc,this._IsReadReply,this._Reply,this._Importance,this._MailTime,this._AttachCount,this._ReplyCount,this._ForwardCount,this._FromId,this._FromTypeId,this._MailSize,this._Viewed,this._IsGroup,this._IsComplete,this._IsMemo,this._Memo,this._IsTop,this._IsTimer,this._TimerSendTime,this._MailState,this._LabelInfo,this._IsArchived,this._IsTracking,this._IsSync,this._Deleted,this._OwnerUID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailMainId;
        /// <summary>
        /// 邮件主键ID,
        /// </summary>
        public PropertyExpression MailMainId { get{return _MailMainId;}}
        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 邮箱ID,
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _MessageId;
        /// <summary>
        /// 邮件唯一标志码,邮件邮局唯一UID
        /// </summary>
        public PropertyExpression MessageId { get{return _MessageId;}}
        private PropertyExpression _MailType;
        /// <summary>
        /// 邮件类型编号,1=收件，2=发件
        /// </summary>
        public PropertyExpression MailType { get{return _MailType;}}
        private PropertyExpression _Subject;
        /// <summary>
        /// 邮件主题,
        /// </summary>
        public PropertyExpression Subject { get{return _Subject;}}
        private PropertyExpression _Sender;
        /// <summary>
        /// 发件人,发件人,格式如:wzh<wzh@qq.com>
        /// </summary>
        public PropertyExpression Sender { get{return _Sender;}}
        private PropertyExpression _Receiver;
        /// <summary>
        /// 收件人,收件人,格式如:wzh<wzh@qq.com>;devin<devin@qq.com>
        /// </summary>
        public PropertyExpression Receiver { get{return _Receiver;}}
        private PropertyExpression _Cc;
        /// <summary>
        /// 抄送,抄送,格式如收件人
        /// </summary>
        public PropertyExpression Cc { get{return _Cc;}}
        private PropertyExpression _Bcc;
        /// <summary>
        /// 密送地址,密送地址,格式如收件人
        /// </summary>
        public PropertyExpression Bcc { get{return _Bcc;}}
        private PropertyExpression _IsReadReply;
        /// <summary>
        /// 已读回执,
        /// </summary>
        public PropertyExpression IsReadReply { get{return _IsReadReply;}}
        private PropertyExpression _Reply;
        /// <summary>
        /// 回复地址,回复地址,对方收件发送已读回执地址
        /// </summary>
        public PropertyExpression Reply { get{return _Reply;}}
        private PropertyExpression _Importance;
        /// <summary>
        /// 发送等级,重要程度,邮件优先级; 0=未定义、1=极高、2=高、3=正常、4=低、5=极低
        /// 
        /// </summary>
        public PropertyExpression Importance { get{return _Importance;}}
        private PropertyExpression _MailTime;
        /// <summary>
        /// 邮件时间,邮件时间,收件时间\发件时间
        /// </summary>
        public PropertyExpression MailTime { get{return _MailTime;}}
        private PropertyExpression _AttachCount;
        /// <summary>
        /// 附件数量,附件数量,如不存在附件则为0
        /// </summary>
        public PropertyExpression AttachCount { get{return _AttachCount;}}
        private PropertyExpression _ReplyCount;
        /// <summary>
        /// 回复次数,回复次数,FromId邮件
        /// </summary>
        public PropertyExpression ReplyCount { get{return _ReplyCount;}}
        private PropertyExpression _ForwardCount;
        /// <summary>
        /// 转发次数,转发次数,FromId邮件
        /// </summary>
        public PropertyExpression ForwardCount { get{return _ForwardCount;}}
        private PropertyExpression _FromId;
        /// <summary>
        /// 来源邮件ID,回复/转发的邮件ID
        /// </summary>
        public PropertyExpression FromId { get{return _FromId;}}
        private PropertyExpression _FromTypeId;
        /// <summary>
        /// 来源类型编号,1=回复，2=转发
        /// 
        /// </summary>
        public PropertyExpression FromTypeId { get{return _FromTypeId;}}
        private PropertyExpression _MailSize;
        /// <summary>
        /// 邮件大小,邮件大小,单位byte
        /// </summary>
        public PropertyExpression MailSize { get{return _MailSize;}}
        private PropertyExpression _Viewed;
        /// <summary>
        /// 是否已读,是否已读，收件时有效,发件默认设置为已读状态标识
        /// </summary>
        public PropertyExpression Viewed { get{return _Viewed;}}
        private PropertyExpression _IsGroup;
        /// <summary>
        /// 分别发送,是否群发,分别发送
        /// </summary>
        public PropertyExpression IsGroup { get{return _IsGroup;}}
        private PropertyExpression _IsComplete;
        /// <summary>
        /// 是否收发完成,是否收发完成,主要用于分别发送
        /// </summary>
        public PropertyExpression IsComplete { get{return _IsComplete;}}
        private PropertyExpression _IsMemo;
        /// <summary>
        /// 是否备注,是否打了备注
        /// </summary>
        public PropertyExpression IsMemo { get{return _IsMemo;}}
        private PropertyExpression _Memo;
        /// <summary>
        /// 邮件备注,此字段用于员工对该邮件进行备注说明(此次沟通说明)
        /// </summary>
        public PropertyExpression Memo { get{return _Memo;}}
        private PropertyExpression _IsTop;
        /// <summary>
        /// 置顶邮件,置顶(星标)邮件,true 置顶
        /// </summary>
        public PropertyExpression IsTop { get{return _IsTop;}}
        private PropertyExpression _IsTimer;
        /// <summary>
        /// 是否定时发送,
        /// </summary>
        public PropertyExpression IsTimer { get{return _IsTimer;}}
        private PropertyExpression _TimerSendTime;
        /// <summary>
        /// 定时发送时间,定时发送时间,IsTimer=true时值有意义
        /// </summary>
        public PropertyExpression TimerSendTime { get{return _TimerSendTime;}}
        private PropertyExpression _MailState;
        /// <summary>
        /// 邮件状态,邮件状态: SEND_SUCCESS = 发送成功；WAIT_SEND=待发送；SENDING=发送中；SEND_FAIL=发送失败待重新发送；SEND_FAIL_END=发送失败不再发送
        /// 
        /// </summary>
        public PropertyExpression MailState { get{return _MailState;}}
        private PropertyExpression _LabelInfo;
        /// <summary>
        /// 邮件标签,邮件标签信息,邮件已打标签信息对象
        /// </summary>
        public PropertyExpression LabelInfo { get{return _LabelInfo;}}
        private PropertyExpression _IsArchived;
        /// <summary>
        /// 是否已归档,是否已归档(客户邮件归档)
        /// </summary>
        public PropertyExpression IsArchived { get{return _IsArchived;}}
        private PropertyExpression _IsTracking;
        /// <summary>
        /// 是否跟踪,
        /// </summary>
        public PropertyExpression IsTracking { get{return _IsTracking;}}
        private PropertyExpression _IsSync;
        /// <summary>
        /// 是否同步,是否同步;是否上传云端(我司平台),true 已上传; false 未上传
        /// </summary>
        public PropertyExpression IsSync { get{return _IsSync;}}
        private PropertyExpression _Deleted;
        /// <summary>
        /// 是否删除,是否已删除，true 删除；false 未删除(默认值)
        /// </summary>
        public PropertyExpression Deleted { get{return _Deleted;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 拥有人编号,
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



        private MailBodyDescriptor _MailBody;
        public MailBodyDescriptor MailBody 
        { 
            get
            {
                if(_MailBody==null) _MailBody=new MailBodyDescriptor(base.Prefix+"MailBody.");
                return _MailBody;
            }
        }
        private MailBoxDescriptor _MailBox;
        public MailBoxDescriptor MailBox 
        { 
            get
            {
                if(_MailBox==null) _MailBox=new MailBoxDescriptor(base.Prefix+"MailBox.");
                return _MailBox;
            }
        }
    }
     #endregion


    #region MailMains
    /// <summary>
    /// 邮件系统 - 邮件,邮件系统----邮件主表 
    /// </summary>
    [DisplayName("邮件系统 - 邮件")]
    [Table]
    public partial class MailMains:EntityList<MailMain> 
    {
        
    }
    #endregion


    #region MailMainFolder
    /// <summary>
    /// 邮件系统 - 邮件文件夹,邮件系统 - 邮件文件夹
    /// </summary>
    [DisplayName("邮件系统 - 邮件文件夹")]
    [Table]
    public partial class MailMainFolder 
    {
        
        public MailMainFolder()
        {

        
        }
        #region propertys
        
        /// <summary>
        /// 邮件ID,
        /// </summary>
        [DisplayName("邮件ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailMainId {  get; set; }


        /// <summary>
        /// 邮箱文件夹ID,
        /// </summary>
        [DisplayName("邮箱文件夹ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailFolderId {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮件文件夹.邮件表,外键关系：邮件文件夹.邮件表
        /// </summary>
        [Relation("this.MailMainId=out.MailMainId")]
        public MailMain MailMain { get;  set;  } 



        /// <summary>
        /// 邮件文件夹.邮箱文件夹,外键关系：邮件文件夹.邮箱文件夹
        /// </summary>
        [Relation("this.MailFolderId=out.MailFolderId")]
        public MailFolder MailFolder { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailMainFolderProperties
    public static partial class MailMainFolder_
    {
    
        private static MailMainFolderDescriptor instance = new MailMainFolderDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailMainFolder";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件ID,
        /// </summary>
        public static PropertyExpression MailMainId { get{return instance.MailMainId;}} 
        /// <summary>
        /// 邮箱文件夹ID,
        /// </summary>
        public static PropertyExpression MailFolderId { get{return instance.MailFolderId;}}


 
        /// <summary>
        /// 邮件文件夹.邮件表,外键关系：邮件文件夹.邮件表
        /// </summary>
        public static MailMainDescriptor MailMain { get{return instance.MailMain;}} 
        /// <summary>
        /// 邮件文件夹.邮箱文件夹,外键关系：邮件文件夹.邮箱文件夹
        /// </summary>
        public static MailFolderDescriptor MailFolder { get{return instance.MailFolder;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailMainFolderDescriptor
    public partial class MailMainFolderDescriptor:ObjectDescriptorBase
    {
     
        public MailMainFolderDescriptor(string prefix):base(prefix)
        {  
    
            this._MailMainId = new PropertyExpression(prefix + "MailMainId");
            this._MailFolderId = new PropertyExpression(prefix + "MailFolderId");
            ALL = new PropertyExpression[] {this._MailMainId,this._MailFolderId};
        }
         

        private PropertyExpression _MailMainId;
        /// <summary>
        /// 邮件ID,
        /// </summary>
        public PropertyExpression MailMainId { get{return _MailMainId;}}
        private PropertyExpression _MailFolderId;
        /// <summary>
        /// 邮箱文件夹ID,
        /// </summary>
        public PropertyExpression MailFolderId { get{return _MailFolderId;}}



        private MailMainDescriptor _MailMain;
        public MailMainDescriptor MailMain 
        { 
            get
            {
                if(_MailMain==null) _MailMain=new MailMainDescriptor(base.Prefix+"MailMain.");
                return _MailMain;
            }
        }
        private MailFolderDescriptor _MailFolder;
        public MailFolderDescriptor MailFolder 
        { 
            get
            {
                if(_MailFolder==null) _MailFolder=new MailFolderDescriptor(base.Prefix+"MailFolder.");
                return _MailFolder;
            }
        }
    }
     #endregion


    #region MailMainFolders
    /// <summary>
    /// 邮件系统 - 邮件文件夹,邮件系统 - 邮件文件夹
    /// </summary>
    [DisplayName("邮件系统 - 邮件文件夹")]
    [Table]
    public partial class MailMainFolders:EntityList<MailMainFolder> 
    {
        
    }
    #endregion


    #region MailLabel
    /// <summary>
    /// 邮件系统 - 邮件标签,邮件系统--邮件标签表
    /// </summary>
    [DisplayName("邮件系统 - 邮件标签")]
    [Table]
    public partial class MailLabel 
    {
        
        public MailLabel()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮件标签ID,邮件标签主键ID,guid
        /// </summary>
        [DisplayName("邮件标签ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string MailLabelId {  get; set; }


        /// <summary>
        /// 标签名称,
        /// </summary>
        [DisplayName("标签名称")]
        [Column(Size = 50)] 
        public string MailLabelName {  get; set; }


        /// <summary>
        /// 标签颜色,
        /// </summary>
        [DisplayName("标签颜色")]
        [Column(Size = 50)] 
        public string Color {  get; set; }


        /// <summary>
        /// 备注,
        /// </summary>
        [DisplayName("备注")]
        [Column(Size = 256)] 
        public string Memo {  get; set; }


        /// <summary>
        /// 建立人编号,建立人编号
        /// 
        /// </summary>
        [DisplayName("建立人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

    }
    #endregion
    #region MailLabelProperties
    public static partial class MailLabel_
    {
    
        private static MailLabelDescriptor instance = new MailLabelDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailLabel";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮件标签ID,邮件标签主键ID,guid
        /// </summary>
        public static PropertyExpression MailLabelId { get{return instance.MailLabelId;}} 
        /// <summary>
        /// 标签名称,
        /// </summary>
        public static PropertyExpression MailLabelName { get{return instance.MailLabelName;}} 
        /// <summary>
        /// 标签颜色,
        /// </summary>
        public static PropertyExpression Color { get{return instance.Color;}} 
        /// <summary>
        /// 备注,
        /// </summary>
        public static PropertyExpression Memo { get{return instance.Memo;}} 
        /// <summary>
        /// 建立人编号,建立人编号
        /// 
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailLabelDescriptor
    public partial class MailLabelDescriptor:ObjectDescriptorBase
    {
     
        public MailLabelDescriptor(string prefix):base(prefix)
        {  
    
            this._MailLabelId = new PropertyExpression(prefix + "MailLabelId");
            this._MailLabelName = new PropertyExpression(prefix + "MailLabelName");
            this._Color = new PropertyExpression(prefix + "Color");
            this._Memo = new PropertyExpression(prefix + "Memo");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailLabelId,this._MailLabelName,this._Color,this._Memo,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailLabelId;
        /// <summary>
        /// 邮件标签ID,邮件标签主键ID,guid
        /// </summary>
        public PropertyExpression MailLabelId { get{return _MailLabelId;}}
        private PropertyExpression _MailLabelName;
        /// <summary>
        /// 标签名称,
        /// </summary>
        public PropertyExpression MailLabelName { get{return _MailLabelName;}}
        private PropertyExpression _Color;
        /// <summary>
        /// 标签颜色,
        /// </summary>
        public PropertyExpression Color { get{return _Color;}}
        private PropertyExpression _Memo;
        /// <summary>
        /// 备注,
        /// </summary>
        public PropertyExpression Memo { get{return _Memo;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 建立人编号,建立人编号
        /// 
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



    }
     #endregion


    #region MailLabels
    /// <summary>
    /// 邮件系统 - 邮件标签,邮件系统--邮件标签表
    /// </summary>
    [DisplayName("邮件系统 - 邮件标签")]
    [Table]
    public partial class MailLabels:EntityList<MailLabel> 
    {
        
    }
    #endregion


    #region MailSignature
    /// <summary>
    /// 邮件系统 - 邮箱签名,邮件签名
    /// </summary>
    [DisplayName("邮件系统 - 邮箱签名")]
    [Table]
    public partial class MailSignature 
    {
        
        public MailSignature()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 邮箱签名主键ID,主键自动增长
        /// </summary>
        [DisplayName("邮箱签名主键ID")]
        [Column(IsPrimaryKey = true)]
        [SQLiteColumn(IsIdentity = true)]
        public int MailSignatureId {  get; set; }


        /// <summary>
        /// 所属邮箱ID,所属邮箱ID,如果邮箱ID为空则未用户名下所有邮箱都可以使用的签名
        /// 
        /// </summary>
        [DisplayName("所属邮箱ID")]
        [Column(Size = 50)] 
        public string MailBoxId {  get; set; }


        /// <summary>
        /// 签名名称,
        /// </summary>
        [DisplayName("签名名称")]
        [Column(Size = 128)] 
        public string SignatureName {  get; set; }


        /// <summary>
        /// 签名内容,
        /// </summary>
        [DisplayName("签名内容")]
        [Column(Size = 8000)] 
        public string SignatureContent {  get; set; }


        /// <summary>
        /// 是否默认签名,是否为默认
        /// </summary>
        [DisplayName("是否默认签名")]
        [Column()] 
        public bool? IsDefault {  get; set; }


        /// <summary>
        /// 类型编号,1=签名,2=模板
        /// </summary>
        [DisplayName("类型编号")]
        [Column()] 
        public int? SignType {  get; set; }


        /// <summary>
        /// 拥有人编号,拥有人编号，默认与建立人一致，增加此字段主要是考虑一个签名允许在同一人名下多个邮箱中选择使用
        /// </summary>
        [DisplayName("拥有人编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 建立人编号,
        /// </summary>
        [DisplayName("建立人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

        #region link objects
        
        /// <summary>
        /// 邮箱签名表.邮箱表,邮箱签名表.邮箱表
        /// </summary>
        [Relation("this.MailBoxId=out.MailBoxId")]
        public MailBox MailBox { get;  set;  } 



        #endregion
    }
    #endregion
    #region MailSignatureProperties
    public static partial class MailSignature_
    {
    
        private static MailSignatureDescriptor instance = new MailSignatureDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "MailSignature";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 邮箱签名主键ID,主键自动增长
        /// </summary>
        public static PropertyExpression MailSignatureId { get{return instance.MailSignatureId;}} 
        /// <summary>
        /// 所属邮箱ID,所属邮箱ID,如果邮箱ID为空则未用户名下所有邮箱都可以使用的签名
        /// 
        /// </summary>
        public static PropertyExpression MailBoxId { get{return instance.MailBoxId;}} 
        /// <summary>
        /// 签名名称,
        /// </summary>
        public static PropertyExpression SignatureName { get{return instance.SignatureName;}} 
        /// <summary>
        /// 签名内容,
        /// </summary>
        public static PropertyExpression SignatureContent { get{return instance.SignatureContent;}} 
        /// <summary>
        /// 是否默认签名,是否为默认
        /// </summary>
        public static PropertyExpression IsDefault { get{return instance.IsDefault;}} 
        /// <summary>
        /// 类型编号,1=签名,2=模板
        /// </summary>
        public static PropertyExpression SignType { get{return instance.SignType;}} 
        /// <summary>
        /// 拥有人编号,拥有人编号，默认与建立人一致，增加此字段主要是考虑一个签名允许在同一人名下多个邮箱中选择使用
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 建立人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}


 
        /// <summary>
        /// 邮箱签名表.邮箱表,邮箱签名表.邮箱表
        /// </summary>
        public static MailBoxDescriptor MailBox { get{return instance.MailBox;}}

        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region MailSignatureDescriptor
    public partial class MailSignatureDescriptor:ObjectDescriptorBase
    {
     
        public MailSignatureDescriptor(string prefix):base(prefix)
        {  
    
            this._MailSignatureId = new PropertyExpression(prefix + "MailSignatureId");
            this._MailBoxId = new PropertyExpression(prefix + "MailBoxId");
            this._SignatureName = new PropertyExpression(prefix + "SignatureName");
            this._SignatureContent = new PropertyExpression(prefix + "SignatureContent");
            this._IsDefault = new PropertyExpression(prefix + "IsDefault");
            this._SignType = new PropertyExpression(prefix + "SignType");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._MailSignatureId,this._MailBoxId,this._SignatureName,this._SignatureContent,this._IsDefault,this._SignType,this._OwnerUID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _MailSignatureId;
        /// <summary>
        /// 邮箱签名主键ID,主键自动增长
        /// </summary>
        public PropertyExpression MailSignatureId { get{return _MailSignatureId;}}
        private PropertyExpression _MailBoxId;
        /// <summary>
        /// 所属邮箱ID,所属邮箱ID,如果邮箱ID为空则未用户名下所有邮箱都可以使用的签名
        /// 
        /// </summary>
        public PropertyExpression MailBoxId { get{return _MailBoxId;}}
        private PropertyExpression _SignatureName;
        /// <summary>
        /// 签名名称,
        /// </summary>
        public PropertyExpression SignatureName { get{return _SignatureName;}}
        private PropertyExpression _SignatureContent;
        /// <summary>
        /// 签名内容,
        /// </summary>
        public PropertyExpression SignatureContent { get{return _SignatureContent;}}
        private PropertyExpression _IsDefault;
        /// <summary>
        /// 是否默认签名,是否为默认
        /// </summary>
        public PropertyExpression IsDefault { get{return _IsDefault;}}
        private PropertyExpression _SignType;
        /// <summary>
        /// 类型编号,1=签名,2=模板
        /// </summary>
        public PropertyExpression SignType { get{return _SignType;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 拥有人编号,拥有人编号，默认与建立人一致，增加此字段主要是考虑一个签名允许在同一人名下多个邮箱中选择使用
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 建立人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



        private MailBoxDescriptor _MailBox;
        public MailBoxDescriptor MailBox 
        { 
            get
            {
                if(_MailBox==null) _MailBox=new MailBoxDescriptor(base.Prefix+"MailBox.");
                return _MailBox;
            }
        }
    }
     #endregion


    #region MailSignatures
    /// <summary>
    /// 邮件系统 - 邮箱签名,邮件签名
    /// </summary>
    [DisplayName("邮件系统 - 邮箱签名")]
    [Table]
    public partial class MailSignatures:EntityList<MailSignature> 
    {
        
    }
    #endregion


    #region CustomerContacts
    /// <summary>
    /// 邮件系统 - 客户联系人表,邮件系统 - 客户联系人表,用于客户归档,保持与业务系统的客户数据一致
    /// </summary>
    [DisplayName("邮件系统 - 客户联系人表")]
    [Table]
    public partial class CustomerContacts 
    {
        
        public CustomerContacts()
        {

            CreateTime=DateTime.Now;
        
        }
        #region propertys
        
        /// <summary>
        /// 客户联系人ID,
        /// </summary>
        [DisplayName("客户联系人ID")]
        [Column(IsPrimaryKey = true,Size = 50)] 
        public string ContactsId {  get; set; }


        /// <summary>
        /// 客户ID,客户主键ID，GUID
        /// </summary>
        [DisplayName("客户ID")]
        [Column(Size = 50)] 
        public string ClientId {  get; set; }


        /// <summary>
        /// 客户代码,
        /// </summary>
        [DisplayName("客户代码")]
        [Column(Size = 30)] 
        public string ClientNo {  get; set; }


        /// <summary>
        /// 客户名称,
        /// </summary>
        [DisplayName("客户名称")]
        [Column(Size = 128)] 
        public string ClientName {  get; set; }


        /// <summary>
        /// 联系人,
        /// </summary>
        [DisplayName("联系人")]
        [Column(Size = 128)] 
        public string Linkman {  get; set; }


        /// <summary>
        /// 联系人邮箱,
        /// </summary>
        [DisplayName("联系人邮箱")]
        [Column(Size = 128)] 
        public string Email {  get; set; }


        /// <summary>
        /// 拥有人编号,
        /// </summary>
        [DisplayName("拥有人编号")]
        [Column(Size = 50)] 
        public string OwnerUID {  get; set; }


        /// <summary>
        /// 创建人编号,
        /// </summary>
        [DisplayName("创建人编号")]
        [Column(Size = 50)] 
        public string CreateUID {  get; set; }


        /// <summary>
        /// 创建时间,
        /// </summary>
        [DisplayName("创建时间")]
        [Column()] 
        public DateTime CreateTime {  get; set; }


        /// <summary>
        /// 所属公司编号,
        /// </summary>
        [DisplayName("所属公司编号")]
        [Column(Size = 50)] 
        public string OCode {  get; set; }


        #endregion

    }
    #endregion
    #region CustomerContactsProperties
    public static partial class CustomerContacts_
    {
    
        private static CustomerContactsDescriptor instance = new CustomerContactsDescriptor(""); 
        
        /// <summary>
        /// 默认表名
        /// </summary>
        public static string TABLE_NAME { get{return "CustomerContacts";}}
        /// <summary>
        /// 全部字段
        /// </summary>
        public static PropertyExpression[] ALL { get{return instance.ALL;}}

 
        /// <summary>
        /// 客户联系人ID,
        /// </summary>
        public static PropertyExpression ContactsId { get{return instance.ContactsId;}} 
        /// <summary>
        /// 客户ID,客户主键ID，GUID
        /// </summary>
        public static PropertyExpression ClientId { get{return instance.ClientId;}} 
        /// <summary>
        /// 客户代码,
        /// </summary>
        public static PropertyExpression ClientNo { get{return instance.ClientNo;}} 
        /// <summary>
        /// 客户名称,
        /// </summary>
        public static PropertyExpression ClientName { get{return instance.ClientName;}} 
        /// <summary>
        /// 联系人,
        /// </summary>
        public static PropertyExpression Linkman { get{return instance.Linkman;}} 
        /// <summary>
        /// 联系人邮箱,
        /// </summary>
        public static PropertyExpression Email { get{return instance.Email;}} 
        /// <summary>
        /// 拥有人编号,
        /// </summary>
        public static PropertyExpression OwnerUID { get{return instance.OwnerUID;}} 
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public static PropertyExpression CreateUID { get{return instance.CreateUID;}} 
        /// <summary>
        /// 创建时间,
        /// </summary>
        public static PropertyExpression CreateTime { get{return instance.CreateTime;}} 
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public static PropertyExpression OCode { get{return instance.OCode;}}




        public static IEnumerable<PropertyExpression> Exclude(params PropertyExpression[] properties)
        {
            return instance.Exclude(properties);
        }

    }
     #endregion
    #region CustomerContactsDescriptor
    public partial class CustomerContactsDescriptor:ObjectDescriptorBase
    {
     
        public CustomerContactsDescriptor(string prefix):base(prefix)
        {  
    
            this._ContactsId = new PropertyExpression(prefix + "ContactsId");
            this._ClientId = new PropertyExpression(prefix + "ClientId");
            this._ClientNo = new PropertyExpression(prefix + "ClientNo");
            this._ClientName = new PropertyExpression(prefix + "ClientName");
            this._Linkman = new PropertyExpression(prefix + "Linkman");
            this._Email = new PropertyExpression(prefix + "Email");
            this._OwnerUID = new PropertyExpression(prefix + "OwnerUID");
            this._CreateUID = new PropertyExpression(prefix + "CreateUID");
            this._CreateTime = new PropertyExpression(prefix + "CreateTime");
            this._OCode = new PropertyExpression(prefix + "OCode");
            ALL = new PropertyExpression[] {this._ContactsId,this._ClientId,this._ClientNo,this._ClientName,this._Linkman,this._Email,this._OwnerUID,this._CreateUID,this._CreateTime,this._OCode};
        }
         

        private PropertyExpression _ContactsId;
        /// <summary>
        /// 客户联系人ID,
        /// </summary>
        public PropertyExpression ContactsId { get{return _ContactsId;}}
        private PropertyExpression _ClientId;
        /// <summary>
        /// 客户ID,客户主键ID，GUID
        /// </summary>
        public PropertyExpression ClientId { get{return _ClientId;}}
        private PropertyExpression _ClientNo;
        /// <summary>
        /// 客户代码,
        /// </summary>
        public PropertyExpression ClientNo { get{return _ClientNo;}}
        private PropertyExpression _ClientName;
        /// <summary>
        /// 客户名称,
        /// </summary>
        public PropertyExpression ClientName { get{return _ClientName;}}
        private PropertyExpression _Linkman;
        /// <summary>
        /// 联系人,
        /// </summary>
        public PropertyExpression Linkman { get{return _Linkman;}}
        private PropertyExpression _Email;
        /// <summary>
        /// 联系人邮箱,
        /// </summary>
        public PropertyExpression Email { get{return _Email;}}
        private PropertyExpression _OwnerUID;
        /// <summary>
        /// 拥有人编号,
        /// </summary>
        public PropertyExpression OwnerUID { get{return _OwnerUID;}}
        private PropertyExpression _CreateUID;
        /// <summary>
        /// 创建人编号,
        /// </summary>
        public PropertyExpression CreateUID { get{return _CreateUID;}}
        private PropertyExpression _CreateTime;
        /// <summary>
        /// 创建时间,
        /// </summary>
        public PropertyExpression CreateTime { get{return _CreateTime;}}
        private PropertyExpression _OCode;
        /// <summary>
        /// 所属公司编号,
        /// </summary>
        public PropertyExpression OCode { get{return _OCode;}}



    }
     #endregion


    #region CustomerContactss
    /// <summary>
    /// 邮件系统 - 客户联系人表,邮件系统 - 客户联系人表,用于客户归档,保持与业务系统的客户数据一致
    /// </summary>
    [DisplayName("邮件系统 - 客户联系人表")]
    [Table]
    public partial class CustomerContactss:EntityList<CustomerContacts> 
    {
        
    }
    #endregion
}
