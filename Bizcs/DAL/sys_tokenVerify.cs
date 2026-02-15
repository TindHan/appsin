using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_tokenVerify
    {
        public sys_tokenVerify()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_tokenVerify model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_tokenVerify(");
            strSql.Append("tokenID,appID,verifyTime,verifyResult)");
            strSql.Append(" values (");
            strSql.Append("@tokenID,@appID,@verifyTime,@verifyResult)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@tokenID", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@verifyTime", SqlDbType.DateTime),
                    new SqlParameter("@verifyResult", SqlDbType.VarChar,100)};
            parameters[0].Value = model.tokenID;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.verifyTime;
            parameters[3].Value = model.verifyResult;

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
        public bool Update(Bizcs.Model.sys_tokenVerify model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_tokenVerify set ");
            strSql.Append("tokenID=@tokenID,");
            strSql.Append("appID=@appID,");
            strSql.Append("verifyTime=@verifyTime,");
            strSql.Append("verifyResult=@verifyResult");
            strSql.Append(" where verifyID=@verifyID");
            SqlParameter[] parameters = {
                    new SqlParameter("@tokenID", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@verifyTime", SqlDbType.DateTime),
                    new SqlParameter("@verifyResult", SqlDbType.VarChar,100),
                    new SqlParameter("@verifyID", SqlDbType.Int,4)};
            parameters[0].Value = model.tokenID;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.verifyTime;
            parameters[3].Value = model.verifyResult;
            parameters[4].Value = model.verifyID;

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
        public bool Delete(int verifyID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_tokenVerify ");
            strSql.Append(" where verifyID=@verifyID");
            SqlParameter[] parameters = {
                    new SqlParameter("@verifyID", SqlDbType.Int,4)
            };
            parameters[0].Value = verifyID;

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
        public Bizcs.Model.sys_tokenVerify GetModel(int verifyID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 verifyID,tokenID,appID,verifyTime,verifyResult from sys_tokenVerify ");
            strSql.Append(" where verifyID=@verifyID");
            SqlParameter[] parameters = {
                    new SqlParameter("@verifyID", SqlDbType.Int,4)
            };
            parameters[0].Value = verifyID;

            Bizcs.Model.sys_tokenVerify model = new Bizcs.Model.sys_tokenVerify();
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
        public Bizcs.Model.sys_tokenVerify DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_tokenVerify model = new Bizcs.Model.sys_tokenVerify();
            if (row != null)
            {
                if (row["verifyID"] != null && row["verifyID"].ToString() != "")
                {
                    model.verifyID = int.Parse(row["verifyID"].ToString());
                }
                if (row["tokenID"] != null && row["tokenID"].ToString() != "")
                {
                    model.tokenID = int.Parse(row["tokenID"].ToString());
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["verifyTime"] != null && row["verifyTime"].ToString() != "")
                {
                    model.verifyTime = DateTime.Parse(row["verifyTime"].ToString());
                }
                if (row["verifyResult"] != null)
                {
                    model.verifyResult = row["verifyResult"].ToString();
                }
            }
            return model;
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere,params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select verifyID,tokenID,appID,verifyTime,verifyResult ");
            strSql.Append(" FROM sys_tokenVerify ");
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
                strSql.Append("order by T.verifyID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_tokenVerify T ");
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
