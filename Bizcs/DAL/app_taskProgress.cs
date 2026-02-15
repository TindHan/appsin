using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_taskProgress
    {
        public app_taskProgress()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.app_taskProgress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_taskProgress(");
            strSql.Append("taskID,psnID,progressValue,progressContent,createTime,createUser,progressStatus)");
            strSql.Append(" values (");
            strSql.Append("@taskID,@psnID,@progressValue,@progressContent,@createTime,@createUser,@progressStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@taskID", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@progressValue", SqlDbType.Int,4),
                    new SqlParameter("@progressContent", SqlDbType.VarChar,500),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@progressStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.taskID;
            parameters[1].Value = model.psnID;
            parameters[2].Value = model.progressValue;
            parameters[3].Value = model.progressContent;
            parameters[4].Value = model.createTime;
            parameters[5].Value = model.createUser;
            parameters[6].Value = model.progressStatus;

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
        public bool Update(appsin.Bizcs.Model.app_taskProgress model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_taskProgress set ");
            strSql.Append("taskID=@taskID,");
            strSql.Append("psnID=@psnID,");
            strSql.Append("progressValue=@progressValue,");
            strSql.Append("progressContent=@progressContent,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("progressStatus=@progressStatus");
            strSql.Append(" where progressID=@progressID");
            SqlParameter[] parameters = {
                    new SqlParameter("@taskID", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@progressValue", SqlDbType.Int,4),
                    new SqlParameter("@progressContent", SqlDbType.VarChar,500),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@progressStatus", SqlDbType.Int,4),
                    new SqlParameter("@progressID", SqlDbType.Int,4)};
            parameters[0].Value = model.taskID;
            parameters[1].Value = model.psnID;
            parameters[2].Value = model.progressValue;
            parameters[3].Value = model.progressContent;
            parameters[4].Value = model.createTime;
            parameters[5].Value = model.createUser;
            parameters[6].Value = model.progressStatus;
            parameters[7].Value = model.progressID;

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
        public bool Delete(int progressID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_taskProgress ");
            strSql.Append(" where progressID=@progressID");
            SqlParameter[] parameters = {
                    new SqlParameter("@progressID", SqlDbType.Int,4)
            };
            parameters[0].Value = progressID;

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
        public appsin.Bizcs.Model.app_taskProgress GetModel(int progressID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 progressID,taskID,psnID,progressValue,progressContent,createTime,createUser,progressStatus from app_taskProgress ");
            strSql.Append(" where progressID=@progressID");
            SqlParameter[] parameters = {
                    new SqlParameter("@progressID", SqlDbType.Int,4)
            };
            parameters[0].Value = progressID;

            appsin.Bizcs.Model.app_taskProgress model = new appsin.Bizcs.Model.app_taskProgress();
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
        public appsin.Bizcs.Model.app_taskProgress DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.app_taskProgress model = new appsin.Bizcs.Model.app_taskProgress();
            if (row != null)
            {
                if (row["progressID"] != null && row["progressID"].ToString() != "")
                {
                    model.progressID = int.Parse(row["progressID"].ToString());
                }
                if (row["taskID"] != null && row["taskID"].ToString() != "")
                {
                    model.taskID = int.Parse(row["taskID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["progressValue"] != null && row["progressValue"].ToString() != "")
                {
                    model.progressValue = int.Parse(row["progressValue"].ToString());
                }
                if (row["progressContent"] != null)
                {
                    model.progressContent = row["progressContent"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["progressStatus"] != null && row["progressStatus"].ToString() != "")
                {
                    model.progressStatus = int.Parse(row["progressStatus"].ToString());
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
            strSql.Append("select progressID,taskID,psnID,progressValue,progressContent,createTime,createUser,progressStatus ");
            strSql.Append(" FROM app_taskProgress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top " + Top + " progressID,taskID,psnID,progressValue,progressContent,createTime,createUser,progressStatus ");
            strSql.Append(" FROM app_taskProgress ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            if (filedOrder.Trim() != "")
            {
                strSql.Append(" order by " + filedOrder);
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
                strSql.Append("order by T.progressID desc");
            }
            strSql.Append(")AS Row, T.*  from app_taskProgress T ");
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

        #endregion  ExtensionMethod
    }
}
