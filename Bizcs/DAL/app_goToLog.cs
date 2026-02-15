using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_goToLog
    {
        public app_goToLog()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.app_goToLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_goToLog(");
            strSql.Append("psnID,appID,menuID,goStr,goTime,callBackTime,callBackRes)");
            strSql.Append(" values (");
            strSql.Append("@psnID,@appID,@menuID,@goStr,@goTime,@callBackTime,@callBackRes)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@menuID", SqlDbType.Int,4),
                    new SqlParameter("@goStr", SqlDbType.VarChar,50),
                    new SqlParameter("@goTime", SqlDbType.DateTime),
                    new SqlParameter("@callBackTime", SqlDbType.DateTime),
                    new SqlParameter("@callBackRes", SqlDbType.VarChar,50)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.menuID;
            parameters[3].Value = model.goStr;
            parameters[4].Value = model.goTime;
            parameters[5].Value = model.callBackTime;
            parameters[6].Value = model.callBackRes;

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
        public bool Update(Bizcs.Model.app_goToLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_goToLog set ");
            strSql.Append("psnID=@psnID,");
            strSql.Append("appID=@appID,");
            strSql.Append("menuID=@menuID,");
            strSql.Append("goStr=@goStr,");
            strSql.Append("goTime=@goTime,");
            strSql.Append("callBackTime=@callBackTime,");
            strSql.Append("callBackRes=@callBackRes");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@menuID", SqlDbType.Int,4),
                    new SqlParameter("@goStr", SqlDbType.VarChar,50),
                    new SqlParameter("@goTime", SqlDbType.DateTime),
                    new SqlParameter("@callBackTime", SqlDbType.DateTime),
                    new SqlParameter("@callBackRes", SqlDbType.VarChar,50),
                    new SqlParameter("@logID", SqlDbType.Int,4)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.menuID;
            parameters[3].Value = model.goStr;
            parameters[4].Value = model.goTime;
            parameters[5].Value = model.callBackTime;
            parameters[6].Value = model.callBackRes;
            parameters[7].Value = model.logID;

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
        public bool Delete(int logID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_goToLog ");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@logID", SqlDbType.Int,4)
            };
            parameters[0].Value = logID;

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
        public Bizcs.Model.app_goToLog GetModel(int logID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 logID,psnID,appID,menuID,goStr,goTime,callBackTime,callBackRes from app_goToLog ");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@logID", SqlDbType.Int,4)
            };
            parameters[0].Value = logID;

            Bizcs.Model.app_goToLog model = new Bizcs.Model.app_goToLog();
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
        public Bizcs.Model.app_goToLog DataRowToModel(DataRow row)
        {
            Bizcs.Model.app_goToLog model = new Bizcs.Model.app_goToLog();
            if (row != null)
            {
                if (row["logID"] != null && row["logID"].ToString() != "")
                {
                    model.logID = int.Parse(row["logID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["menuID"] != null && row["menuID"].ToString() != "")
                {
                    model.menuID = int.Parse(row["menuID"].ToString());
                }
                if (row["goStr"] != null)
                {
                    model.goStr = row["goStr"].ToString();
                }
                if (row["goTime"] != null && row["goTime"].ToString() != "")
                {
                    model.goTime = DateTime.Parse(row["goTime"].ToString());
                }
                if (row["callBackTime"] != null && row["callBackTime"].ToString() != "")
                {
                    model.callBackTime = DateTime.Parse(row["callBackTime"].ToString());
                }
                if (row["callBackRes"] != null)
                {
                    model.callBackRes = row["callBackRes"].ToString();
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
            strSql.Append("select logID,psnID,appID,menuID,goStr,goTime,callBackTime,callBackRes ");
            strSql.Append(" FROM app_goToLog ");
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
                strSql.Append("order by T.logID desc");
            }
            strSql.Append(")AS Row, T.*  from app_goToLog T ");
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
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select logID,psnID,(select psnName from psn_psnMain p where p.psnID=TT.psnID) as psnName,
                            appID,(select appName from app_appMain app where app.appID=TT.appID) as appName,
                            menuID,(select menuName from sys_menu mn where mn.menuID=TT.menuID) as menuName,
                            goStr,goTime,callBackTime,callBackRes ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.logID desc");
            }
            strSql.Append(")AS Row, T.*  from app_goToLog T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        public Bizcs.Model.app_goToLog GetListByGostr(string goStr,string appSID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 logID,psnID,appID,menuID,goStr,goTime,callBackTime,callBackRes ");
            strSql.Append(" FROM app_goToLog where goStr=@goStr and appID in (select appID from app_appMain where appSID=@appSID) order by logID desc");
            SqlParameter[] parameters = {
                    new SqlParameter("@goStr", SqlDbType.VarChar,50),
                    new SqlParameter("@appSID", SqlDbType.VarChar,50)
            };
            parameters[0].Value = goStr;
            parameters[1].Value = appSID;

            Bizcs.Model.app_goToLog model = new Bizcs.Model.app_goToLog();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(),parameters);

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
