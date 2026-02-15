using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_admin
    {
        public sys_admin()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.sys_admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_admin(");
            strSql.Append("adminName,adminPwd,createUser,createTime,pwdSetTime)");
            strSql.Append(" values (");
            strSql.Append("@adminName,@adminPwd,@createUser,@createTime,@pwdSetTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminName", SqlDbType.VarChar,50),
                    new SqlParameter("@adminPwd", SqlDbType.VarChar,150),
                    new SqlParameter("@createUser", SqlDbType.VarChar,150),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@pwdSetTime", SqlDbType.DateTime)};
            parameters[0].Value = model.adminName;
            parameters[1].Value = model.adminPwd;
            parameters[2].Value = model.createUser;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.pwdSetTime;

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
        public bool Update(appsin.Bizcs.Model.sys_admin model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_admin set ");
            strSql.Append("adminName=@adminName,");
            strSql.Append("adminPwd=@adminPwd,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("pwdSetTime=@pwdSetTime");
            strSql.Append(" where adminID=@adminID");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminName", SqlDbType.VarChar,50),
                    new SqlParameter("@adminPwd", SqlDbType.VarChar,150),
                    new SqlParameter("@createUser", SqlDbType.VarChar,150),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@pwdSetTime", SqlDbType.DateTime),
                    new SqlParameter("@adminID", SqlDbType.Int,4)};
            parameters[0].Value = model.adminName;
            parameters[1].Value = model.adminPwd;
            parameters[2].Value = model.createUser;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.pwdSetTime;
            parameters[5].Value = model.adminID;

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
        public bool Delete(int adminID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_admin ");
            strSql.Append(" where adminID=@adminID");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminID", SqlDbType.Int,4)
            };
            parameters[0].Value = adminID;

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
        public appsin.Bizcs.Model.sys_admin GetModel(int adminID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 adminID,adminName,adminPwd,createUser,createTime,pwdSetTime from sys_admin ");
            strSql.Append(" where adminID=@adminID");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminID", SqlDbType.Int,4)
            };
            parameters[0].Value = adminID;

            appsin.Bizcs.Model.sys_admin model = new appsin.Bizcs.Model.sys_admin();
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
        public appsin.Bizcs.Model.sys_admin DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.sys_admin model = new appsin.Bizcs.Model.sys_admin();
            if (row != null)
            {
                if (row["adminID"] != null && row["adminID"].ToString() != "")
                {
                    model.adminID = int.Parse(row["adminID"].ToString());
                }
                if (row["adminName"] != null)
                {
                    model.adminName = row["adminName"].ToString();
                }
                if (row["adminPwd"] != null)
                {
                    model.adminPwd = row["adminPwd"].ToString();
                }
                if (row["createUser"] != null)
                {
                    model.createUser = row["createUser"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["pwdSetTime"] != null && row["pwdSetTime"].ToString() != "")
                {
                    model.pwdSetTime = DateTime.Parse(row["pwdSetTime"].ToString());
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
            strSql.Append("select adminID,adminName,adminPwd,createUser,createTime,pwdSetTime ");
            strSql.Append(" FROM sys_admin ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
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
                strSql.Append("order by T.adminID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_admin T ");
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
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public appsin.Bizcs.Model.sys_admin GetModelByName(string adminName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 adminID,adminName,adminPwd,createUser,createTime,pwdSetTime from sys_admin ");
            strSql.Append(" where adminName=@adminName");
            SqlParameter[] parameters = {
                    new SqlParameter("@adminName", SqlDbType.VarChar,50)
            };
            parameters[0].Value = adminName;

            appsin.Bizcs.Model.sys_admin model = new appsin.Bizcs.Model.sys_admin();
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
        #endregion  ExtensionMethod
    }
}
