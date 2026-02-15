using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_menuBind
    {
        public sys_menuBind()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_menuBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_menuBind(");
            strSql.Append("roleID,menuID,createTime,createUser,bindStatus)");
            strSql.Append(" values (");
            strSql.Append("@roleID,@menuID,@createTime,@createUser,@bindStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleID", SqlDbType.Int,4),
                    new SqlParameter("@menuID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@bindStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.roleID;
            parameters[1].Value = model.menuID;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.createUser;
            parameters[4].Value = model.bindStatus;

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
        public bool Update(Bizcs.Model.sys_menuBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_menuBind set ");
            strSql.Append("roleID=@roleID,");
            strSql.Append("menuID=@menuID,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("bindStatus=@bindStatus");
            strSql.Append(" where bindID=@bindID");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleID", SqlDbType.Int,4),
                    new SqlParameter("@menuID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@bindStatus", SqlDbType.Int,4),
                    new SqlParameter("@bindID", SqlDbType.Int,4)};
            parameters[0].Value = model.roleID;
            parameters[1].Value = model.menuID;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.createUser;
            parameters[4].Value = model.bindStatus;
            parameters[5].Value = model.bindID;

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
        public bool Delete(int bindID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_menuBind ");
            strSql.Append(" where bindID=@bindID");
            SqlParameter[] parameters = {
                    new SqlParameter("@bindID", SqlDbType.Int,4)
            };
            parameters[0].Value = bindID;

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
        public Bizcs.Model.sys_menuBind GetModel(int bindID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 bindID,roleID,menuID,createTime,createUser,bindStatus from sys_menuBind ");
            strSql.Append(" where bindID=@bindID");
            SqlParameter[] parameters = {
                    new SqlParameter("@bindID", SqlDbType.Int,4)
            };
            parameters[0].Value = bindID;

            Bizcs.Model.sys_menuBind model = new Bizcs.Model.sys_menuBind();
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
        public Bizcs.Model.sys_menuBind DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_menuBind model = new Bizcs.Model.sys_menuBind();
            if (row != null)
            {
                if (row["bindID"] != null && row["bindID"].ToString() != "")
                {
                    model.bindID = int.Parse(row["bindID"].ToString());
                }
                if (row["roleID"] != null && row["roleID"].ToString() != "")
                {
                    model.roleID = int.Parse(row["roleID"].ToString());
                }
                if (row["menuID"] != null && row["menuID"].ToString() != "")
                {
                    model.menuID = int.Parse(row["menuID"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["bindStatus"] != null && row["bindStatus"].ToString() != "")
                {
                    model.bindStatus = int.Parse(row["bindStatus"].ToString());
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
            strSql.Append("select bindID,roleID,menuID,createTime,createUser,bindStatus ");
            strSql.Append(" FROM sys_menuBind ");
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
                strSql.Append("order by T.bindID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_menuBind T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        #endregion  BasicMethod
        #region  ExtensionMethod
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select bindID, ");
            strSql.Append("(select menuName from sys_menu m where m.menuID = b.menuID) as menuName ,");
            strSql.Append("(select menuName from sys_menu m where m.menuID = (select parentID from sys_menu p where p.menuID = b.menuID)) as moduleName ,");
            strSql.Append("(select appName from app_appMain a where a.appID = (select menuAppID from sys_menu p where p.menuID = b.menuID)) as appName ,");
            strSql.Append("(select psnName from psn_psnMain psn where psn.psnID = b.createUser) as createUserName,");
            strSql.Append("createTime,bindStatus");
            strSql.Append(" FROM sys_menuBind b");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by appName,moduleName");
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        #endregion  ExtensionMethod
    }
}
