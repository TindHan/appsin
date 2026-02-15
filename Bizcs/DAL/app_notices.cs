using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_notices
    {
        public app_notices()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.app_notices model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_notices(");
            strSql.Append("appID,objType,objID,noticeTitle,noticeContent,noticeFile,storeFile,createTime,startTime,endTime,createUser,noticeStatus)");
            strSql.Append(" values (");
            strSql.Append("@appID,@objType,@objID,@noticeTitle,@noticeContent,@noticeFile,@storeFile,@createTime,@startTime,@endTime,@createUser,@noticeStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@objType", SqlDbType.VarChar,50),
                    new SqlParameter("@objID", SqlDbType.Int,4),
                    new SqlParameter("@noticeTitle", SqlDbType.VarChar,500),
                    new SqlParameter("@noticeContent", SqlDbType.VarChar,-1),
                    new SqlParameter("@noticeFile", SqlDbType.VarChar,500),
                    new SqlParameter("@storeFile", SqlDbType.VarChar,500),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@startTime", SqlDbType.DateTime),
                    new SqlParameter("@endTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@noticeStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.objType;
            parameters[2].Value = model.objID;
            parameters[3].Value = model.noticeTitle;
            parameters[4].Value = model.noticeContent;
            parameters[5].Value = model.noticeFile;
            parameters[6].Value = model.storeFile;
            parameters[7].Value = model.createTime;
            parameters[8].Value = model.startTime;
            parameters[9].Value = model.endTime;
            parameters[10].Value = model.createUser;
            parameters[11].Value = model.noticeStatus;

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
        public bool Update(appsin.Bizcs.Model.app_notices model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_notices set ");
            strSql.Append("appID=@appID,");
            strSql.Append("objType=@objType,");
            strSql.Append("objID=@objID,");
            strSql.Append("noticeTitle=@noticeTitle,");
            strSql.Append("noticeContent=@noticeContent,");
            strSql.Append("noticeFile=@noticeFile,");
            strSql.Append("storeFile=@storeFile,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("startTime=@startTime,");
            strSql.Append("endTime=@endTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("noticeStatus=@noticeStatus");
            strSql.Append(" where noticeID=@noticeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@objType", SqlDbType.VarChar,50),
                    new SqlParameter("@objID", SqlDbType.Int,4),
                    new SqlParameter("@noticeTitle", SqlDbType.VarChar,500),
                    new SqlParameter("@noticeContent", SqlDbType.VarChar,-1),
                    new SqlParameter("@noticeFile", SqlDbType.VarChar,500),
                    new SqlParameter("@storeFile", SqlDbType.VarChar,500),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@startTime", SqlDbType.DateTime),
                    new SqlParameter("@endTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@noticeStatus", SqlDbType.Int,4),
                    new SqlParameter("@noticeID", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.objType;
            parameters[2].Value = model.objID;
            parameters[3].Value = model.noticeTitle;
            parameters[4].Value = model.noticeContent;
            parameters[5].Value = model.noticeFile;
            parameters[6].Value = model.storeFile;
            parameters[7].Value = model.createTime;
            parameters[8].Value = model.startTime;
            parameters[9].Value = model.endTime;
            parameters[10].Value = model.createUser;
            parameters[11].Value = model.noticeStatus;
            parameters[12].Value = model.noticeID;

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
        public bool Delete(int noticeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_notices ");
            strSql.Append(" where noticeID=@noticeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@noticeID", SqlDbType.Int,4)
            };
            parameters[0].Value = noticeID;

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
        public appsin.Bizcs.Model.app_notices GetModel(int noticeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 noticeID,appID,objType,objID,noticeTitle,noticeContent,noticeFile,storeFile,createTime,startTime,endTime,createUser,noticeStatus from app_notices ");
            strSql.Append(" where noticeID=@noticeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@noticeID", SqlDbType.Int,4)
            };
            parameters[0].Value = noticeID;

            appsin.Bizcs.Model.app_notices model = new appsin.Bizcs.Model.app_notices();
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
        public appsin.Bizcs.Model.app_notices DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.app_notices model = new appsin.Bizcs.Model.app_notices();
            if (row != null)
            {
                if (row["noticeID"] != null && row["noticeID"].ToString() != "")
                {
                    model.noticeID = int.Parse(row["noticeID"].ToString());
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["objType"] != null)
                {
                    model.objType = row["objType"].ToString();
                }
                if (row["objID"] != null && row["objID"].ToString() != "")
                {
                    model.objID = int.Parse(row["objID"].ToString());
                }
                if (row["noticeTitle"] != null)
                {
                    model.noticeTitle = row["noticeTitle"].ToString();
                }
                if (row["noticeContent"] != null)
                {
                    model.noticeContent = row["noticeContent"].ToString();
                }
                if (row["noticeFile"] != null)
                {
                    model.noticeFile = row["noticeFile"].ToString();
                }
                if (row["storeFile"] != null)
                {
                    model.storeFile = row["storeFile"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["startTime"] != null && row["startTime"].ToString() != "")
                {
                    model.startTime = DateTime.Parse(row["startTime"].ToString());
                }
                if (row["endTime"] != null && row["endTime"].ToString() != "")
                {
                    model.endTime = DateTime.Parse(row["endTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["noticeStatus"] != null && row["noticeStatus"].ToString() != "")
                {
                    model.noticeStatus = int.Parse(row["noticeStatus"].ToString());
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
            strSql.Append("select noticeID,appID,objType,objID,noticeTitle,noticeContent,noticeFile,storeFile,createTime,startTime,endTime,createUser,noticeStatus ");
            strSql.Append(" FROM app_notices ");
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
                strSql.Append("order by T.noticeID desc");
            }
            strSql.Append(")AS Row, T.*  from app_notices T ");
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

        public DataSet GetSimpleList(string strWhere, int psnID, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select noticeID,appID,objType,objID,noticeTitle,noticeContent,noticeFile,storeFile,createTime,startTime,endTime,createUser,noticeStatus, ");
            strSql.Append("(select appName from app_appMain app where app.appID=app_notices.appID) as appName, ");
            strSql.Append("(select count(1) from app_noticeRead nrd where nrd.noticeID=app_notices.noticeID and nrd.psnID=" + psnID + ") as readCount, ");
            strSql.Append("(select psnName from psn_psnmain psn where psn.psnID=app_notices.createUser) as createUserName ");
            strSql.Append(" FROM app_notices ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        public DataSet GetSimpleListByPage(string strWhere, string orderby, int psnID, int startIndex, int endIndex, params SqlParameter[] parms)
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
                strSql.Append("order by T.noticeID desc");
            }
            strSql.Append(")AS Row, T.*, ");
            strSql.Append("(select appName from app_appMain app where app.appID=T.appID) as appName, ");
            strSql.Append("(select count(1) from app_noticeRead nrd where nrd.noticeID=T.noticeID and nrd.psnID=" + psnID + ") as readCount, ");
            strSql.Append("(select psnName from psn_psnmain psn where psn.psnID=T.createUser) as createUserName ");
            strSql.Append(" from app_notices T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }

        public DataSet GetAllListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
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
                strSql.Append("order by T.noticeID desc");
            }
            strSql.Append(")AS Row, T.*, ");
            strSql.Append("(select appName from app_appMain app where app.appID=T.appID) as appName, ");
            strSql.Append("(select count(DISTINCT psnID) from app_noticeRead nrd where nrd.noticeID=T.noticeID GROUP BY noticeID) as readCount, ");
            strSql.Append("(select psnName from psn_psnmain psn where psn.psnID=T.createUser) as createUserName ");
            strSql.Append(" from app_notices T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        #endregion  ExtensionMethod
    }
}
