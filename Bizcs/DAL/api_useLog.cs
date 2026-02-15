using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class api_useLog
    {
        public api_useLog()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.api_useLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into api_useLog(");
            strSql.Append("appID,apiID,osrzID,createTime,appDomain,isS,logMemo,inPara,outPara)");
            strSql.Append(" values (");
            strSql.Append("@appID,@apiID,@osrzID,@createTime,@appDomain,@isS,@logMemo,@inPara,@outPara)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@apiID", SqlDbType.Int,4),
                    new SqlParameter("@osrzID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@appDomain", SqlDbType.VarChar,1000),
                    new SqlParameter("@isS", SqlDbType.VarChar,50),
                    new SqlParameter("@logMemo", SqlDbType.VarChar,1000),
                    new SqlParameter("@inPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@outPara", SqlDbType.VarChar,5000)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.apiID;
            parameters[2].Value = model.osrzID;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.appDomain;
            parameters[5].Value = model.isS;
            parameters[6].Value = model.logMemo;
            parameters[7].Value = model.inPara;
            parameters[8].Value = model.outPara;

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
        public bool Update(Bizcs.Model.api_useLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update api_useLog set ");
            strSql.Append("appID=@appID,");
            strSql.Append("apiID=@apiID,");
            strSql.Append("osrzID=@osrzID,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("appDomain=@appDomain,");
            strSql.Append("isS=@isS,");
            strSql.Append("logMemo=@logMemo,");
            strSql.Append("inPara=@inPara,");
            strSql.Append("outPara=@outPara");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@apiID", SqlDbType.Int,4),
                    new SqlParameter("@osrzID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@appDomain", SqlDbType.VarChar,1000),
                    new SqlParameter("@isS", SqlDbType.VarChar,50),
                    new SqlParameter("@logMemo", SqlDbType.VarChar,1000),
                    new SqlParameter("@inPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@outPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@logID", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.apiID;
            parameters[2].Value = model.osrzID;
            parameters[3].Value = model.createTime;
            parameters[4].Value = model.appDomain;
            parameters[5].Value = model.isS;
            parameters[6].Value = model.logMemo;
            parameters[7].Value = model.inPara;
            parameters[8].Value = model.outPara;
            parameters[9].Value = model.logID;

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
            strSql.Append("delete from api_useLog ");
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
        public Bizcs.Model.api_useLog GetModel(int logID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 logID,appID,apiID,osrzID,createTime,appDomain,isS,logMemo,inPara,outPara from api_useLog ");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@logID", SqlDbType.Int,4)
            };
            parameters[0].Value = logID;

            Bizcs.Model.api_useLog model = new Bizcs.Model.api_useLog();
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
        public Bizcs.Model.api_useLog DataRowToModel(DataRow row)
        {
            Bizcs.Model.api_useLog model = new Bizcs.Model.api_useLog();
            if (row != null)
            {
                if (row["logID"] != null && row["logID"].ToString() != "")
                {
                    model.logID = int.Parse(row["logID"].ToString());
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["apiID"] != null && row["apiID"].ToString() != "")
                {
                    model.apiID = int.Parse(row["apiID"].ToString());
                }
                if (row["osrzID"] != null && row["osrzID"].ToString() != "")
                {
                    model.osrzID = int.Parse(row["osrzID"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["appDomain"] != null)
                {
                    model.appDomain = row["appDomain"].ToString();
                }
                if (row["isS"] != null && row["isS"].ToString() != "")
                {
                    model.isS = row["isS"].ToString();
                }
                if (row["logMemo"] != null)
                {
                    model.logMemo = row["logMemo"].ToString();
                }
                if (row["inPara"] != null)
                {
                    model.inPara = row["inPara"].ToString();
                }
                if (row["outPara"] != null)
                {
                    model.outPara = row["outPara"].ToString();
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
            strSql.Append("select logID,appID,apiID,osrzID,createTime,appDomain,isS,logMemo,inPara,outPara ");
            strSql.Append(" FROM api_useLog ");
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
                strSql.Append("order by T.logID desc");
            }
            strSql.Append(")AS Row, T.*  from api_useLog T ");
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

        public DataSet GetSimpleList(int logID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select logID,appID,apiID,osrzID,createTime,appDomain,isS,logMemo,inPara,outPara, ");
            strSql.Append("(select appName from app_appMain app where app.appID=log.appID) as appName, ");
            strSql.Append("(select apiName from api_apiMain api where api.apiID=log.apiID) as apiName ");
            strSql.Append(" FROM api_useLog log where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@logID", SqlDbType.Int,4)
            };
            parameters[0].Value = logID;
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }
        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *, ");
            strSql.Append("(select appName from app_appMain app where app.appID=TT.appID) as appName, ");
            strSql.Append("(select apiName from api_apiMain api where api.apiID=TT.apiID) as apiName ");
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
            strSql.Append(")AS Row, T.*  from api_useLog T ");
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
