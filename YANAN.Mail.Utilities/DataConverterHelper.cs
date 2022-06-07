using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Globalization;
using System.Reflection;

namespace YANAN.Mail.Utilities
{
    /// <summary>
    /// 数据转换帮助类
    /// </summary>
    public class DataConverterHelper
    {
        /// <summary>
        /// List对象集合转DataSet
        /// </summary>
        /// <param name="p_List"></param>
        /// <returns></returns>
        public static DataSet ToDataSet(IList p_List)
        {
            DataSet result = new DataSet();
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    _DataTable.Columns.Add(pi.Name, pi.PropertyType);
                }

                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        object obj = pi.GetValue(p_List[i], null);
                        tempList.Add(obj);
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            result.Tables.Add(_DataTable);
            return result;
        }
        /// <summary>
        /// List对象集合转DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(IList<T> list)
        {
            return ToDataSet(list, null);
        }
        /// <summary>
        /// List对象集合转DataSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_List"></param>
        /// <param name="p_PropertyName"></param>
        /// <returns></returns>
        public static DataSet ToDataSet<T>(IList<T> p_List, params string[] p_PropertyName)
        {
            DataSet result = new DataSet();
            result.Tables.Add(ToDataTable(p_List, p_PropertyName));
            return result;
        }
        /// <summary>
        /// List对象集合转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> list)
        {
            return ToDataTable(list, null);
        }
        /// <summary>
        /// List对象集合转DataTable
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="p_List">数据集合</param>
        /// <param name="p_PropertyName">需要转换的字段/属性 数组</param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(IList<T> p_List, params string[] p_PropertyName)
        {
            List<string> propertyNameList = new List<string>();
            if (p_PropertyName != null)
                propertyNameList.AddRange(p_PropertyName);
            DataTable _DataTable = new DataTable();
            if (p_List.Count > 0)
            {
                PropertyInfo[] propertys = p_List[0].GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    Type colType = pi.PropertyType;
                    if ((colType.IsGenericType) && (colType.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        colType = colType.GetGenericArguments()[0];
                    }
                    if (propertyNameList.Count == 0)
                    {
                        // 没有指定属性的情况下全部属性都要转换 
                        _DataTable.Columns.Add(pi.Name, colType);
                    }
                    else
                    {
                        if (propertyNameList.Contains(pi.Name))
                            _DataTable.Columns.Add(pi.Name, colType);
                    }
                }

                for (int i = 0; i < p_List.Count; i++)
                {
                    ArrayList tempList = new ArrayList();
                    foreach (PropertyInfo pi in propertys)
                    {
                        if (propertyNameList.Count == 0)
                        {
                            object obj = pi.GetValue(p_List[i], null);
                            tempList.Add(obj);
                        }
                        else
                        {
                            if (propertyNameList.Contains(pi.Name))
                            {
                                object obj = pi.GetValue(p_List[i], null);
                                tempList.Add(obj);
                            }
                        }
                    }
                    object[] array = tempList.ToArray();
                    _DataTable.LoadDataRow(array, true);
                }
            }
            return _DataTable;
        }
        /// <summary>
        /// DataSet转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet"></param>
        /// <param name="p_TableIndex"></param>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, int p_TableIndex = 0)
        {
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (p_TableIndex > p_DataSet.Tables.Count - 1)
                return null;
            if (p_TableIndex < 0)
                p_TableIndex = 0;
            DataTable p_Data = p_DataSet.Tables[p_TableIndex];
            // 返回值初始化 
            IList<T> result = DataTableToIList<T>(p_Data);
            return result;
        }
        /// <summary>
        /// DataSet转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_DataSet"></param>
        /// <param name="p_TableName"></param>
        /// <returns></returns>
        public static IList<T> DataSetToIList<T>(DataSet p_DataSet, string p_TableName)
        {
            int _TableIndex = 0;
            if (p_DataSet == null || p_DataSet.Tables.Count < 0)
                return null;
            if (string.IsNullOrEmpty(p_TableName))
                return null;
            for (int i = 0; i < p_DataSet.Tables.Count; i++)
            {
                // 获取Table名称在Tables集合中的索引值 
                if (p_DataSet.Tables[i].TableName.Equals(p_TableName))
                {
                    _TableIndex = i;
                    break;
                }
            }
            return DataSetToIList<T>(p_DataSet, _TableIndex);
        }
        /// <summary>
        /// DataTable转对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="p_Data"></param>
        /// <returns></returns>
        public static IList<T> DataTableToIList<T>(DataTable p_Data)
        {
            if (p_Data == null || p_Data.Columns.Count < 0)
                return null;
            // 返回值初始化 
            IList<T> result = new List<T>();

            for (int j = 0; j < p_Data.Rows.Count; j++)
            {
                T _t = (T)Activator.CreateInstance(typeof(T));
                PropertyInfo[] propertys = _t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    var c = (ColumnAttribute)Attribute.GetCustomAttribute(pi, typeof(ColumnAttribute));
                    for (int i = 0; i < p_Data.Columns.Count; i++)
                    {
                        // 属性与字段名称一致的进行赋值 
                        if ((c != null && c.Name == p_Data.Columns[i].ColumnName) || pi.Name.Equals(p_Data.Columns[i].ColumnName))
                        {
                            // 数据库NULL值单独处理 
                            if (p_Data.Rows[j][i] != DBNull.Value)
                                pi.SetValue(_t, p_Data.Rows[j][i], null);
                            else
                                pi.SetValue(_t, null, null);
                            break;
                        }
                    }
                }
                result.Add(_t);
            }
            return result;
        }
        /// <summary>
        /// DataRow转对象
        /// </summary>
        /// <param name="adaptedRow"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static object ToEntity(DataRow adaptedRow, Type entityType)
        {
            if (entityType == null || adaptedRow == null)
            {
                return null;
            }

            object entity = Activator.CreateInstance(entityType);
            CopyToEntity(entity, adaptedRow);

            return entity;
        }

        public static T ToEntity<T>(DataRow adaptedRow, T value) where T : new()
        {
            T item = new T();
            if (value == null || adaptedRow == null)
            {
                return item;
            }

            item = Activator.CreateInstance<T>();
            CopyToEntity(item, adaptedRow);

            return item;
        }

        public static void CopyToEntity(object entity, DataRow adaptedRow)
        {
            if (entity == null || adaptedRow == null)
            {
                return;
            }
            PropertyInfo[] propertyInfos = entity.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                if (!CanSetPropertyValue(propertyInfo, adaptedRow))
                {
                    continue;
                }

                try
                {
                    if (adaptedRow[propertyInfo.Name] is DBNull)
                    {
                        propertyInfo.SetValue(entity, null, null);
                        continue;
                    }
                    SetPropertyValue(entity, adaptedRow, propertyInfo);
                }
                finally
                {

                }
            }
        }

        private static bool CanSetPropertyValue(PropertyInfo propertyInfo, DataRow adaptedRow)
        {
            if (!propertyInfo.CanWrite)
            {
                return false;
            }

            if (!adaptedRow.Table.Columns.Contains(propertyInfo.Name))
            {
                return false;
            }

            return true;
        }

        private static void SetPropertyValue(object entity, DataRow adaptedRow, PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType == typeof(DateTime?) ||
                propertyInfo.PropertyType == typeof(DateTime))
            {
                DateTime date = DateTime.MaxValue;
                DateTime.TryParse(adaptedRow[propertyInfo.Name].ToString(),
                    CultureInfo.CurrentCulture, DateTimeStyles.None, out date);

                propertyInfo.SetValue(entity, date, null);
            }
            else
            {
                propertyInfo.SetValue(entity, adaptedRow[propertyInfo.Name], null);
            }
        }
    }
}
