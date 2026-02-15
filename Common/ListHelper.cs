using System.Data;
using System.Reflection;

namespace appsin.Common
{
    public static class listHelper
    {
        public static List<T> ConvertDtToList<T>(DataTable dt) where T : new()//泛型约束，用于可以实例化T对象
        {
            List<T> list = new List<T>();
            Type type = typeof(T);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                T t = new T();
                PropertyInfo[] properties = type.GetProperties();
                foreach (PropertyInfo item in properties)
                {

                    if (item.CanWrite && dt.Columns.Contains(item.Name))
                    {
                        object value = dt.Rows[i][item.Name];
                        if (value != DBNull.Value) // 检查是否为空值
                        {
                            item.SetValue(t, value);
                        }
                    }
                }
                list.Add(t);
            }
            return list;
        }

        public static string[] GetColsByDt(DataTable dt)
        {
            string[] strColumns = null;


            if (dt.Columns.Count > 0)
            {
                int columnNum = 0;
                columnNum = dt.Columns.Count;
                strColumns = new string[columnNum];
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    strColumns[i] = dt.Columns[i].ColumnName;
                }
            }


            return strColumns;
        }
    }
}
