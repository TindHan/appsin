using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_messages
    {
        public app_messages()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.app_messages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_messages(");
            strSql.Append("appID,objType,objID,bizType,bizID,msgType,msgTitle,msgContent,msgUrl,createTime,expireTime,msgDesc,msgStatus)");
            strSql.Append(" values (");
            strSql.Append("@appID,@objType,@objID,@bizType,@bizID,@msgType,@msgTitle,@msgContent,@msgUrl,@createTime,@expireTime,@msgDesc,@msgStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@objType", SqlDbType.VarChar,50),
                    new SqlParameter("@objID", SqlDbType.Int,4),
                    new SqlParameter("@bizType", SqlDbType.VarChar,50),
                    new SqlParameter("@bizID", SqlDbType.Int,4),
                    new SqlParameter("@msgType", SqlDbType.VarChar,50),
                    new SqlParameter("@msgTitle", SqlDbType.VarChar,500),
                    new SqlParameter("@msgContent", SqlDbType.VarChar,5000),
                    new SqlParameter("@msgUrl", SqlDbType.VarChar,5000),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@expireTime", SqlDbType.DateTime),
                    new SqlParameter("@msgDesc", SqlDbType.VarChar,5000),
                    new SqlParameter("@msgStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.objType;
            parameters[2].Value = model.objID;
            parameters[3].Value = model.bizType;
            parameters[4].Value = model.bizID;
            parameters[5].Value = model.msgType;
            parameters[6].Value = model.msgTitle;
            parameters[7].Value = model.msgContent;
            parameters[8].Value = model.msgUrl;
            parameters[9].Value = model.createTime;
            parameters[10].Value = model.expireTime;
            parameters[11].Value = model.msgDesc;
            parameters[12].Value = model.msgStatus;

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
        public bool Update(appsin.Bizcs.Model.app_messages model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_messages set ");
            strSql.Append("appID=@appID,");
            strSql.Append("objType=@objType,");
            strSql.Append("objID=@objID,");
            strSql.Append("bizType=@bizType,");
            strSql.Append("bizID=@bizID,");
            strSql.Append("msgType=@msgType,");
            strSql.Append("msgTitle=@msgTitle,");
            strSql.Append("msgContent=@msgContent,");
            strSql.Append("msgUrl=@msgUrl,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("expireTime=@expireTime,");
            strSql.Append("msgDesc=@msgDesc,");
            strSql.Append("msgStatus=@msgStatus");
            strSql.Append(" where msgID=@msgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@objType", SqlDbType.VarChar,50),
                    new SqlParameter("@objID", SqlDbType.Int,4),
                    new SqlParameter("@bizType", SqlDbType.VarChar,50),
                    new SqlParameter("@bizID", SqlDbType.Int,4),
                    new SqlParameter("@msgType", SqlDbType.VarChar,50),
                    new SqlParameter("@msgTitle", SqlDbType.VarChar,500),
                    new SqlParameter("@msgContent", SqlDbType.VarChar,5000),
                    new SqlParameter("@msgUrl", SqlDbType.VarChar,5000),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@expireTime", SqlDbType.DateTime),
                    new SqlParameter("@msgDesc", SqlDbType.VarChar,5000),
                    new SqlParameter("@msgStatus", SqlDbType.Int,4),
                    new SqlParameter("@msgID", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.objType;
            parameters[2].Value = model.objID;
            parameters[3].Value = model.bizType;
            parameters[4].Value = model.bizID;
            parameters[5].Value = model.msgType;
            parameters[6].Value = model.msgTitle;
            parameters[7].Value = model.msgContent;
            parameters[8].Value = model.msgUrl;
            parameters[9].Value = model.createTime;
            parameters[10].Value = model.expireTime;
            parameters[11].Value = model.msgDesc;
            parameters[12].Value = model.msgStatus;
            parameters[13].Value = model.msgID;

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
        public bool Delete(int msgID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_messages ");
            strSql.Append(" where msgID=@msgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@msgID", SqlDbType.Int,4)
            };
            parameters[0].Value = msgID;

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
        public appsin.Bizcs.Model.app_messages GetModel(int msgID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 msgID,appID,objType,objID,bizType,bizID,msgType,msgTitle,msgContent,msgUrl,createTime,expireTime,msgDesc,msgStatus from app_messages ");
            strSql.Append(" where msgID=@msgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@msgID", SqlDbType.Int,4)
            };
            parameters[0].Value = msgID;

            appsin.Bizcs.Model.app_messages model = new appsin.Bizcs.Model.app_messages();
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
        public appsin.Bizcs.Model.app_messages DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.app_messages model = new appsin.Bizcs.Model.app_messages();
            if (row != null)
            {
                if (row["msgID"] != null && row["msgID"].ToString() != "")
                {
                    model.msgID = int.Parse(row["msgID"].ToString());
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
                if (row["bizType"] != null)
                {
                    model.bizType = row["bizType"].ToString();
                }
                if (row["bizID"] != null && row["bizID"].ToString() != "")
                {
                    model.bizID = int.Parse(row["bizID"].ToString());
                }
                if (row["msgType"] != null)
                {
                    model.msgType = row["msgType"].ToString();
                }
                if (row["msgTitle"] != null)
                {
                    model.msgTitle = row["msgTitle"].ToString();
                }
                if (row["msgContent"] != null)
                {
                    model.msgContent = row["msgContent"].ToString();
                }
                if (row["msgUrl"] != null)
                {
                    model.msgUrl = row["msgUrl"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["expireTime"] != null && row["expireTime"].ToString() != "")
                {
                    model.expireTime = DateTime.Parse(row["expireTime"].ToString());
                }
                if (row["msgDesc"] != null)
                {
                    model.msgDesc = row["msgDesc"].ToString();
                }
                if (row["msgStatus"] != null && row["msgStatus"].ToString() != "")
                {
                    model.msgStatus = int.Parse(row["msgStatus"].ToString());
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
            strSql.Append("select msgID,appID,objType,objID,bizType,bizID,msgType,msgTitle,msgContent,msgUrl,createTime,expireTime,msgDesc,msgStatus ");
            strSql.Append(" FROM app_messages ");
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
                strSql.Append("order by T.msgID desc");
            }
            strSql.Append(")AS Row, T.*  from app_messages T ");
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
            strSql.Append("select msgID,appID,objType,objID,bizType,bizID,msgType,msgTitle,msgContent,msgUrl,createTime,expireTime,msgDesc,msgStatus, ");
            strSql.Append("(select appName from app_appMain app where app.appID=app_messages.appID) as appName, ");
            strSql.Append("(select count(1) from app_messageRead mrd where mrd.msgID=app_messages.msgID and mrd.psnID=" + psnID + ") as readCount ");
            strSql.Append(" FROM app_messages ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        public DataSet GetSimpleListByPage(string strWhere, int psnID, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
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
                strSql.Append("order by T.msgID desc");
            }
            strSql.Append(")AS Row, T.* , ");
            strSql.Append("(select appName from app_appMain app where app.appID=T.appID) as appName, ");
            strSql.Append("(select count(1) from app_messageRead mrd where mrd.msgID=T.msgID and mrd.psnID=" + psnID + ") as readCount ");
            strSql.Append(" from app_messages T ");
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
