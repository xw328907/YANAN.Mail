using YANAN.Mail.Utilities;
using System;
using System.IO;
using Titan;

namespace YANAN.Mail.Services
{
    /// <summary>
    /// 数据库脚本
    /// </summary>
    public class DatabaseVersionScript
    {
        #region 初始化数据库脚本

        /// <summary>
        /// 初始化数据库脚本
        /// </summary>
        public const string InitSql = @"

create table MailBody (
MailMainId           VARCHAR(50)                    not null,
BodyHtml             text,
BodyText             text,
primary key (MailMainId)
);

create table MailBox (
MailBoxId            VARCHAR(50)                    not null,
Sorting              INT                        not null,
ShowName             VARCHAR(128),
NickName             VARCHAR(125)                   not null,
MailAddress          VARCHAR(125)                   not null,
MailPassword         VARCHAR(64)                    not null,
ProtocolTypeId       INT                        not null,
PopServer            VARCHAR(100)                   not null,
PopPort              INT                        not null,
SmtpServer           VARCHAR(100)                   not null,
SmtpPort             INT                        not null,
MailCount            INT                        not null default 0,
MailSize             BIGINT,
IsDefault            BIT                            not null,
SendTimer            INT                        not null default 1,
ReceiveTimer         INT                        not null default 15,
KeepDays             INT                        not null,
ReceiveBeginTime     DATE,
Bcc                  VARCHAR(4000),
Cc                   VARCHAR(4000),
IsSync               BIT                            not null default 0,
Memo                 VARCHAR(128),
OwnerUID             VARCHAR(50),
Deleted              BIT                            not null default 0,
OCode                VARCHAR(50),
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
primary key (MailBoxId)
);

create table MailMain (
MailMainId           VARCHAR(50)                    not null,
MailBoxId            VARCHAR(50)                    not null,
MessageId            VARCHAR(100)                   not null,
MailType             INT                        not null,
Subject              VARCHAR(512)                   not null,
Sender               VARCHAR(256)                   not null,
Receiver             VARCHAR(4000),
Cc                   VARCHAR(4000),
Bcc                  VARCHAR(4000),
IsReadReply          BIT                            not null default 0,
Reply                VARCHAR(4000),
Importance           INT,
MailTime             DATE                           not null,
AttachCount          INT                        not null default 0,
ReplyCount           INT                        not null default 0,
ForwardCount         INT                        not null default 0,
FromId               VARCHAR(50),
FromTypeId           INT,
MailSize             INT,
Viewed               BIT                            not null default 0,
IsGroup              BIT                            not null default 0,
IsComplete           BIT                            not null default 0,
IsMemo               BIT                            not null default 0,
Memo                 VARCHAR(256),
IsTop                BIT                             default 0,
IsTimer              BIT                             default 0,
TimerSendTime        DATE,
MailState            VARCHAR(30)                    not null,
LabelInfo            VARCHAR(8000),
IsArchived           BIT                            not null default 0,
IsTracking           BIT                             default 0,
IsSync               BIT                            not null default 0,
Deleted              BIT                            not null default 0,
OwnerUID             VARCHAR(50)                    not null,
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null default 'getdate()',
OCode                VARCHAR(50)                    not null,
primary key (MailMainId)
);

create table MailAttach (
MailAttachId       INTEGER  primary key  autoincrement                     not null,
MailMainId           VARCHAR(50),
FilesName            VARCHAR(225)                   not null,
FilesSize            VARCHAR(32)                    not null,
ActualSize           INT,
FilesPath            VARCHAR(400)                   not null,
FilesType            VARCHAR(10)                    not null,
ContentID            VARCHAR(200)                   not null,
CreateUID            VARCHAR(50),
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null
);

create table MailBoxMessage (
MailBoxId            VARCHAR(50)                    not null,
MessageId            VARCHAR(100)                   not null,
InsertTime           DATE                           not null,
InsertType           INT                        not null,
InsertErrorNum       INT                         default 1,
DeleteTime           DATE                           not null,
DeleteType           INT                        not null,
DeleteErrorNum       INT                         default 1,
MailTime             DATE                           not null,
primary key (MailBoxId, MessageId)
);

create table MailContact (
MailContactId    INTEGER  primary key  autoincrement                     not null,
EMail                VARCHAR(128)                   not null,
ContactName          VARCHAR(64)                    not null,
ContactPinyin        VARCHAR(64),
Address              VARCHAR(512),
Tel                  VARCHAR(128),
Fax                  VARCHAR(128),
Mobile               VARCHAR(256),
Postalcode           VARCHAR(40),
QQ                   VARCHAR(50),
WeChat               VARCHAR(50),
Facebook             VARCHAR(50),
Twitter              VARCHAR(50),
Skype                VARCHAR(128),
CompanyName          VARCHAR(128),
Post                 VARCHAR(128),
Area                 VARCHAR(128),
CountryId            VARCHAR(50),
Memo                 VARCHAR(256),
LastContactTime      DATE,
OwnerUID             VARCHAR(50)                    not null,
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null
);

create table CustomerContacts (
ContactsId           VARCHAR(50)                    not null,
ClientId             VARCHAR(50)                    not null,
ClientNo             VARCHAR(30)                    not null,
ClientName           VARCHAR(128),
Linkman              VARCHAR(128),
Email                VARCHAR(128),
OwnerUID             VARCHAR(50),
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null,
primary key (ContactsId)
);

create table MailFilterCondition (
FilterConditionId   INTEGER  primary key  autoincrement                     not null,
FilterName           VARCHAR(100)                   not null,
SortNumber           INT                        not null default 0,
MailBoxId            VARCHAR(50)                    not null,
FilterDoTime         INT                        not null default 0,
ConditionOpertation  INT                        not null default 0,
FilterConditions     VARCHAR(8000)                 not null,
FilterEvents         VARCHAR(8000)                 not null,
IsnoreOther          BIT                            not null default 0,
IsFilter             BIT                            not null default 1,
OwnerUID             VARCHAR(50)                    not null,
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null
);

create table MailFolder (
MailFolderId         VARCHAR(50)                    not null,
MailBoxId            VARCHAR(50),
FolderName           VARCHAR(128)                   not null,
Sorting              INT                        not null default 1,
ParentId             VARCHAR(50)                    not null,
MailType             INT                        not null,
MailCount            INT                        not null default 0,
UnreadCount          INT                        not null default 0,
Depth                INT                        not null default 1,
SourceTable          VARCHAR(100)                   not null,
SourceId             VARCHAR(50)                    not null,
ServerFullFolderName VARCHAR(1000),
LastMailTime         DATE,
OwnerUID             VARCHAR(50)                    not null,
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null,
primary key (MailFolderId)
);

create table MailGroup (
MailGroupId       INTEGER  primary key  autoincrement                     not null,
MailMainId           VARCHAR(50)                    not null,
Sorting              INT                        not null default 1,
ReceiveTypeCode      INT                        not null default 1,
ReceiveName          VARCHAR(128)                   not null,
ReceiveAddress       VARCHAR(128)                   not null,
IsSend               BIT                             default 0,
IsReceive            BIT                             default 0,
SendTime             DATE
);

create table MailJudge (
MailJudgeId       INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,
MessageID            VARCHAR(100)                   not null,
OperateType          INT                        not null default 2,
MailAddress          VARCHAR(50)                    not null,
MailBoxId            VARCHAR(50),
Memo                 VARCHAR(256),
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null
);

create table MailLabel (
MailLabelId          VARCHAR(50)                    not null,
MailLabelName        VARCHAR(50)                    not null,
Color                VARCHAR(50)                    not null,
Memo                 VARCHAR(256),
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null,
primary key (MailLabelId)
);

create  index In_MailTime on MailMain (
MailTime ASC
);

create table MailMainFolder (
MailMainId           VARCHAR(50)                    not null,
MailFolderId         VARCHAR(50)                    not null,
primary key (MailMainId, MailFolderId)
);

create table MailSignature (
MailSignatureId    INTEGER   PRIMARY KEY AUTOINCREMENT NOT NULL,
MailBoxId            VARCHAR(50)                    not null,
SignatureName        VARCHAR(128)                   not null,
SignatureContent     VARCHAR(8000)                  not null,
IsDefault            BIT,
SignType             INT,
OwnerUID             VARCHAR(50)                    not null,
CreateUID            VARCHAR(50)                    not null,
CreateTime           DATE                           not null,
OCode                VARCHAR(50)                    not null
);
";

        #endregion 初始化数据库脚本

        /// <summary>
        /// 邮件数据库初始化，如果不存在的话
        /// </summary>
        /// <returns></returns>
        public bool InitDatabase()
        {
            string filePath = AssemblyHelper.GetBaseDirectory() + "Storage\\yanan.mail";
            if (string.IsNullOrWhiteSpace(filePath) || !File.Exists(filePath))
            {
                try
                {
                    if (!Directory.Exists(AssemblyHelper.GetBaseDirectory() + "Storage"))
                    {
                        Directory.CreateDirectory(AssemblyHelper.GetBaseDirectory() + "Storage");
                    }
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    fileStream.Close(); fileStream.Dispose();
                    InitDatabaseTable();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return true;
        }
        /// <summary>
        /// 初始化数据库表结构
        /// </summary>
        private void InitDatabaseTable()
        {
            string sql = DatabaseVersionScript.InitSql;

            using (IDbSession db = DbSessionHelper.OpenSessionSQLite())
            {
                try
                {
                    db.BeginTransaction();//数据库需每个表独立创建，批量无法执行
                    var tables = sql.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string table in tables)
                    {
                        db.ExecuteNonQuery(table);
                    }
                    db.Commit();
                }
                catch (Exception ex)
                {
                    db.Rollback();
                    throw ex;
                }
            }
        }
    }

}
