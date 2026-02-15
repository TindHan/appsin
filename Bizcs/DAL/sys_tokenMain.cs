using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_tokenMain
    {
        public sys_tokenMain()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_tokenMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_tokenMain(");
            strSql.Append("psnID,appID,createTime,expireTime,tokenStr,tokenStatus,cancelTime)");
            strSql.Append(" values (");
            strSql.Append("@psnID,@appID,@createTime,@expireTime,@tokenStr,@tokenStatus,@cancelTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@expireTime", SqlDbType.DateTime),
                    new SqlParameter("@tokenStr", SqlDbType.VarChar,1000),
                    new SqlParameter("@tokenStatus", SqlDbType.Int,4),
                    new SqlParameter("@cancelTime", SqlDbType.DateTime)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.expireTime;
            parameters[4].Value = model.tokenStr;
            parameters[5].Value = model.tokenStatus;
            parameters[6].Value = model.cancelTime;

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
        public bool Update(Bizcs.Model.sys_tokenMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_tokenMain set ");
            strSql.Append("psnID=@psnID,");
            strSql.Append("appID=@appID,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("expireTime=@expireTime,");
            strSql.Append("tokenStr=@tokenStr,");
            strSql.Append("tokenStatus=@tokenStatus,");
            strSql.Append("cancelTime=@cancelTime");
            strSql.Append(" where tokenID=@tokenID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@expireTime", SqlDbType.DateTime),
                    new SqlParameter("@tokenStr", SqlDbType.VarChar,1000),
                    new SqlParameter("@tokenStatus", SqlDbType.Int,4),
                    new SqlParameter("@cancelTime", SqlDbType.DateTime),
                    new SqlParameter("@tokenID", SqlDbType.Int,4)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.createTime;
            parameters[3].Value = model.expireTime;
            parameters[4].Value = model.tokenStr;
            parameters[5].Value = model.tokenStatus;
            parameters[6].Value = model.cancelTime;
            parameters[7].Value = model.tokenID;

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
        public bool Delete(int tokenID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_tokenMain ");
            strSql.Append(" where tokenID=@tokenID");
            SqlParameter[] parameters = {
                    new SqlParameter("@tokenID", SqlDbType.Int,4)
            };
            parameters[0].Value = tokenID;

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
        public Bizcs.Model.sys_tokenMain GetModel(int tokenID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 tokenID,psnID,appID,createTime,expireTime,tokenStr,tokenStatus,cancelTime from sys_tokenMain ");
            strSql.Append(" where tokenStatus=1 and tokenID=@tokenID");
            SqlParameter[] parameters = {
                    new SqlParameter("@tokenID", SqlDbType.Int,4)
            };
            parameters[0].Value = tokenID;

            Bizcs.Model.sys_tokenMain model = new Bizcs.Model.sys_tokenMain();
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
        public Bizcs.Model.sys_tokenMain DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_tokenMain model = new Bizcs.Model.sys_tokenMain();
            if (row != null)
            {
                if (row["tokenID"] != null && row["tokenID"].ToString() != "")
                {
                    model.tokenID = int.Parse(row["tokenID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["expireTime"] != null && row["expireTime"].ToString() != "")
                {
                    model.expireTime = DateTime.Parse(row["expireTime"].ToString());
                }
                if (row["tokenStr"] != null)
                {
                    model.tokenStr = row["tokenStr"].ToString();
                }
                if (row["tokenStatus"] != null && row["tokenStatus"].ToString() != "")
                {
                    model.tokenStatus = int.Parse(row["tokenStatus"].ToString());
                }
                if (row["cancelTime"] != null && row["cancelTime"].ToString() != "")
                {
                    model.cancelTime = DateTime.Parse(row["cancelTime"].ToString());
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
            strSql.Append("select tokenID,psnID,appID,createTime,expireTime,tokenStr,tokenStatus,cancelTime ");
            strSql.Append(" FROM sys_tokenMain ");
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
                strSql.Append("order by T.tokenID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_tokenMain T ");
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
        public Bizcs.Model.sys_tokenMain GetModelLatest(int psnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 tokenID,psnID,appID,createTime,expireTime,tokenStr,tokenStatus,cancelTime from sys_tokenMain ");
            strSql.Append(" where psnID=@psnID order by expireTime desc");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;

            Bizcs.Model.sys_tokenMain model = new Bizcs.Model.sys_tokenMain();
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
