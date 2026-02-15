using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class psn_actLog
    {
        public psn_actLog()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.psn_actLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_actLog(");
            strSql.Append("psnID,logTime,logAction,isSuccess,actDvsIP,actDvsCode,actPara,actResult,actMemo)");
            strSql.Append(" values (");
            strSql.Append("@psnID,@logTime,@logAction,@isSuccess,@actDvsIP,@actDvsCode,@actPara,@actResult,@actMemo)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@logTime", SqlDbType.DateTime),
                    new SqlParameter("@logAction", SqlDbType.VarChar,100),
                    new SqlParameter("@isSuccess", SqlDbType.VarChar,100),
                    new SqlParameter("@actDvsIP", SqlDbType.VarChar,100),
                    new SqlParameter("@actDvsCode", SqlDbType.VarChar,1000),
                    new SqlParameter("@actPara", SqlDbType.VarChar,1000),
                    new SqlParameter("@actResult", SqlDbType.VarChar,1000),
                    new SqlParameter("@actMemo", SqlDbType.VarChar,8000)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.logTime;
            parameters[2].Value = model.logAction;
            parameters[3].Value = model.isSuccess;
            parameters[4].Value = model.actDvsIP;
            parameters[5].Value = model.actDvsCode;
            parameters[6].Value = model.actPara;
            parameters[7].Value = model.actResult;
            parameters[8].Value = model.actMemo;

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
        public bool Update(Bizcs.Model.psn_actLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_actLog set ");
            strSql.Append("psnID=@psnID,");
            strSql.Append("logTime=@logTime,");
            strSql.Append("logAction=@logAction,");
            strSql.Append("isSuccess=@isSuccess,");
            strSql.Append("actDvsIP=@actDvsIP,");
            strSql.Append("actDvsCode=@actDvsCode,");
            strSql.Append("actPara=@actPara,");
            strSql.Append("actResult=@actResult,");
            strSql.Append("actMemo=@actMemo");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@logTime", SqlDbType.DateTime),
                    new SqlParameter("@logAction", SqlDbType.VarChar,100),
                    new SqlParameter("@isSuccess", SqlDbType.VarChar,100),
                    new SqlParameter("@actDvsIP", SqlDbType.VarChar,100),
                    new SqlParameter("@actDvsCode", SqlDbType.VarChar,1000),
                    new SqlParameter("@actPara", SqlDbType.VarChar,1000),
                    new SqlParameter("@actResult", SqlDbType.VarChar,1000),
                    new SqlParameter("@actMemo", SqlDbType.VarChar,8000),
                    new SqlParameter("@logID", SqlDbType.Int,4)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.logTime;
            parameters[2].Value = model.logAction;
            parameters[3].Value = model.isSuccess;
            parameters[4].Value = model.actDvsIP;
            parameters[5].Value = model.actDvsCode;
            parameters[6].Value = model.actPara;
            parameters[7].Value = model.actResult;
            parameters[8].Value = model.actMemo;
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
            strSql.Append("delete from psn_actLog ");
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
        public Bizcs.Model.psn_actLog GetModel(int logID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 logID,psnID,logTime,logAction,isSuccess,actDvsIP,actDvsCode,actPara,actResult,actMemo from psn_actLog ");
            strSql.Append(" where logID=@logID");
            SqlParameter[] parameters = {
                    new SqlParameter("@logID", SqlDbType.Int,4)
            };
            parameters[0].Value = logID;

            Bizcs.Model.psn_actLog model = new Bizcs.Model.psn_actLog();
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
        public Bizcs.Model.psn_actLog DataRowToModel(DataRow row)
        {
            Bizcs.Model.psn_actLog model = new Bizcs.Model.psn_actLog();
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
                if (row["logTime"] != null && row["logTime"].ToString() != "")
                {
                    model.logTime = DateTime.Parse(row["logTime"].ToString());
                }
                if (row["logAction"] != null)
                {
                    model.logAction = row["logAction"].ToString();
                }
                if (row["isSuccess"] != null)
                {
                    model.isSuccess = row["isSuccess"].ToString();
                }
                if (row["actDvsIP"] != null)
                {
                    model.actDvsIP = row["actDvsIP"].ToString();
                }
                if (row["actDvsCode"] != null)
                {
                    model.actDvsCode = row["actDvsCode"].ToString();
                }
                if (row["actPara"] != null)
                {
                    model.actPara = row["actPara"].ToString();
                }
                if (row["actResult"] != null)
                {
                    model.actResult = row["actResult"].ToString();
                }
                if (row["actMemo"] != null)
                {
                    model.actMemo = row["actMemo"].ToString();
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
            strSql.Append("select logID,psnID,logTime,logAction,isSuccess,actDvsIP,actDvsCode,actPara,actResult,actMemo ");
            strSql.Append(" FROM psn_actLog ");
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
            strSql.Append(")AS Row, T.*  from psn_actLog T ");
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
            strSql.Append("SELECT logID,psnID,");
            strSql.Append("(case when len(psnID)=3 then (select adminName from sys_admin where sys_admin.adminID=TT.psnID) else (select psnName from psn_psnMain where psn_psnMain.psnID=TT.psnID) END) AS psnName,");
            strSql.Append("logTime,logAction,isSuccess,actDvsIP,actDvsCode,actPara,actResult,actMemo FROM ( ");

            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.logID desc");
            }
            strSql.Append(")AS Row, T.*  from psn_actLog T ");
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
