using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_menuOsrz
    {
        public sys_menuOsrz()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_menuOsrz model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_menuOsrz(");
            strSql.Append("osrzObjType,osrzObjID,osrzRoleID,createTime,createUser,osrzStatus)");
            strSql.Append(" values (");
            strSql.Append("@osrzObjType,@osrzObjID,@osrzRoleID,@createTime,@createUser,@osrzStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@osrzObjType", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzObjID", SqlDbType.Int,4),
                    new SqlParameter("@osrzRoleID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@osrzStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.osrzObjType;
            parameters[1].Value = model.osrzObjID;
            parameters[2].Value = model.osrzRoleID;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.createUser;
            parameters[5].Value = model.osrzStatus;

            object obj = DbHelperSQL.GetSingle(strSql.ToString(), parameters);
            if (obj == null)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(obj);
            }
        }
        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(Bizcs.Model.sys_menuOsrz model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_menuOsrz set ");
            strSql.Append("osrzObjType=@osrzObjType,");
            strSql.Append("osrzObjID=@osrzObjID,");
            strSql.Append("osrzRoleID=@osrzRoleID,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("osrzStatus=@osrzStatus");
            strSql.Append(" where osrzID=@osrzID");
            SqlParameter[] parameters = {
                    new SqlParameter("@osrzObjType", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzObjID", SqlDbType.Int,4),
                    new SqlParameter("@osrzRoleID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@osrzStatus", SqlDbType.Int,4),
                    new SqlParameter("@osrzID", SqlDbType.Int,4)};
            parameters[0].Value = model.osrzObjType;
            parameters[1].Value = model.osrzObjID;
            parameters[2].Value = model.osrzRoleID;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.createUser;
            parameters[5].Value = model.osrzStatus;
            parameters[6].Value = model.osrzID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(int osrzID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_menuOsrz ");
            strSql.Append(" where osrzID=@osrzID");
            SqlParameter[] parameters = {
                    new SqlParameter("@osrzID", SqlDbType.Int,4)
            };
            parameters[0].Value = osrzID;

            int rows = DbHelperSQL.ExecuteSql(strSql.ToString(), parameters);
            if (rows > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.sys_menuOsrz GetModel(int osrzID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 osrzID,osrzObjType,osrzObjID,osrzRoleID,createTime,createUser,osrzStatus from sys_menuOsrz ");
            strSql.Append(" where osrzID=@osrzID");
            SqlParameter[] parameters = {
                    new SqlParameter("@osrzID", SqlDbType.Int,4)
            };
            parameters[0].Value = osrzID;

            Bizcs.Model.sys_menuOsrz model = new Bizcs.Model.sys_menuOsrz();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.sys_menuOsrz DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_menuOsrz model = new Bizcs.Model.sys_menuOsrz();
            if (row != null)
            {
                if (row["osrzID"] != null && row["osrzID"].ToString() != "")
                {
                    model.osrzID = int.Parse(row["osrzID"].ToString());
                }
                if (row["osrzObjType"] != null)
                {
                    model.osrzObjType = row["osrzObjType"].ToString();
                }
                if (row["osrzObjID"] != null && row["osrzObjID"].ToString() != "")
                {
                    model.osrzObjID = int.Parse(row["osrzObjID"].ToString());
                }
                if (row["osrzRoleID"] != null && row["osrzRoleID"].ToString() != "")
                {
                    model.osrzRoleID = int.Parse(row["osrzRoleID"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["osrzStatus"] != null && row["osrzStatus"].ToString() != "")
                {
                    model.osrzStatus = int.Parse(row["osrzStatus"].ToString());
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select osrzID,osrzObjType,osrzObjID,osrzRoleID,createTime,createUser,osrzStatus ");
            strSql.Append(" FROM sys_menuOsrz ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.osrzID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_menuOsrz T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod

        #endregion  ExtensionMethod
    }
}
