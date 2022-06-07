using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace YANAN.Mail.Services
{
    using Titan;
    using Titan.Core;
    using Titan.SqlServer;
    using Titan.SqlTracer;
    using Titan.SQLite;
    using YANAN.Mail.Utilities;
    /// <summary>
    /// 数据库连接池
    /// </summary>
    public class DbSessionHelper
    {

        /// <summary>
        /// 打开MSSQL数据库连接
        /// </summary>
        /// <param name="connString">数据库连接串</param>
        /// <returns></returns>
        public static IDbSession OpenSessionMSSQL(string connString = "")
        {
            return OpenSession(connString);
        }
        /// <summary>
        /// 打开SQLite数据库连接
        /// </summary>
        /// <param name="connString">数据库连接串</param>
        /// <returns></returns>
        public static IDbSession OpenSessionSQLite(string connString = "")
        {
            if (string.IsNullOrWhiteSpace(connString))
            {
                connString = "Data Source=" + AssemblyHelper.GetBaseDirectory() + "Storage\\yanan.mail";
            }
            return OpenSession(connString, new SQLiteSqlProvider());
        }
        /// <summary>
        /// 打开MSSQL数据库连接
        /// </summary>
        /// <param name="connString">数据库连接串</param>
        /// <returns></returns>
        public static IDbSession OpenSession(string connString, ISqlProvider sqlProvider = null)
        {
            if (string.IsNullOrWhiteSpace(connString))
                throw new Exception("无效数据库连接");
            if (sqlProvider == null)
                sqlProvider = new SqlServerSqlProvider();
            ISqlTracer[] sqlTracers = null;
            //bool isTrace = AppConfigHelper.GetAppSetting("IsSqlTrace").ToLower() == "true";
            //if (isTrace)
            //{
            //    sqlTracers = new ISqlTracer[] { new FileSqlTracer { FileName = AppConfigHelper.GetAppSetting("SqlTraceFile") } };
            //}
            IDbSession session = new DbSession(sqlProvider, connString, sqlTracers);
            session.Open();
            return session;
        }

    }


}
