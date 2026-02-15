using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace appsin.Bizcs.DAL
{
    public class app_tasks
    {
        public app_tasks()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.app_tasks model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_tasks(");
            strSql.Append("parentTaskID,psnID,assignPsnID,taskTitle,taskContent,taskDeadline,taskProgress,taskAssessResult,taskAssessTime,taskSupervisor,taskCreateTime,taskStatus)");
            strSql.Append(" values (");
            strSql.Append("@parentTaskID,@psnID,@assignPsnID,@taskTitle,@taskContent,@taskDeadline,@taskProgress,@taskAssessResult,@taskAssessTime,@taskSupervisor,@taskCreateTime,@taskStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@parentTaskID", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@assignPsnID", SqlDbType.Int,4),
                    new SqlParameter("@taskTitle", SqlDbType.VarChar,150),
                    new SqlParameter("@taskContent", SqlDbType.VarChar,5000),
                    new SqlParameter("@taskDeadline", SqlDbType.DateTime),
                    new SqlParameter("@taskProgress", SqlDbType.Int,4),
                    new SqlParameter("@taskAssessResult", SqlDbType.Int,4),
                    new SqlParameter("@taskAssessTime", SqlDbType.DateTime),
                    new SqlParameter("@taskSupervisor", SqlDbType.Int,4),
                    new SqlParameter("@taskCreateTime", SqlDbType.DateTime),
                    new SqlParameter("@taskStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.parentTaskID;
            parameters[1].Value = model.psnID;
            parameters[2].Value = model.assignPsnID;
            parameters[3].Value = model.taskTitle;
            parameters[4].Value = model.taskContent;
            parameters[5].Value = model.taskDeadline;
            parameters[6].Value = model.taskProgress;
            parameters[7].Value = model.taskAssessResult;
            parameters[8].Value = model.taskAssessTime;
            parameters[9].Value = model.taskSupervisor;
            parameters[10].Value = model.taskCreateTime;
            parameters[11].Value = model.taskStatus;

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
        public bool Update(appsin.Bizcs.Model.app_tasks model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_tasks set ");
            strSql.Append("parentTaskID=@parentTaskID,");
            strSql.Append("psnID=@psnID,");
            strSql.Append("assignPsnID=@assignPsnID,");
            strSql.Append("taskTitle=@taskTitle,");
            strSql.Append("taskContent=@taskContent,");
            strSql.Append("taskDeadline=@taskDeadline,");
            strSql.Append("taskProgress=@taskProgress,");
            strSql.Append("taskAssessResult=@taskAssessResult,");
            strSql.Append("taskAssessTime=@taskAssessTime,");
            strSql.Append("taskSupervisor=@taskSupervisor,");
            strSql.Append("taskCreateTime=@taskCreateTime,");
            strSql.Append("taskStatus=@taskStatus");
            strSql.Append(" where taskID=@taskID");
            SqlParameter[] parameters = {
                    new SqlParameter("@parentTaskID", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@assignPsnID", SqlDbType.Int,4),
                    new SqlParameter("@taskTitle", SqlDbType.VarChar,150),
                    new SqlParameter("@taskContent", SqlDbType.VarChar,5000),
                    new SqlParameter("@taskDeadline", SqlDbType.DateTime),
                    new SqlParameter("@taskProgress", SqlDbType.Int,4),
                    new SqlParameter("@taskAssessResult", SqlDbType.Int,4),
                    new SqlParameter("@taskAssessTime", SqlDbType.DateTime),
                    new SqlParameter("@taskSupervisor", SqlDbType.Int,4),
                    new SqlParameter("@taskCreateTime", SqlDbType.DateTime),
                    new SqlParameter("@taskStatus", SqlDbType.Int,4),
                    new SqlParameter("@taskID", SqlDbType.Int,4)};
            parameters[0].Value = model.parentTaskID;
            parameters[1].Value = model.psnID;
            parameters[2].Value = model.assignPsnID;
            parameters[3].Value = model.taskTitle;
            parameters[4].Value = model.taskContent;
            parameters[5].Value = model.taskDeadline;
            parameters[6].Value = model.taskProgress;
            parameters[7].Value = model.taskAssessResult;
            parameters[8].Value = model.taskAssessTime;
            parameters[9].Value = model.taskSupervisor;
            parameters[10].Value = model.taskCreateTime;
            parameters[11].Value = model.taskStatus;
            parameters[12].Value = model.taskID;

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
        public bool Delete(int taskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_tasks ");
            strSql.Append(" where taskID=@taskID");
            SqlParameter[] parameters = {
                    new SqlParameter("@taskID", SqlDbType.Int,4)
            };
            parameters[0].Value = taskID;

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
        public appsin.Bizcs.Model.app_tasks GetModel(int taskID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 taskID,parentTaskID,psnID,assignPsnID,taskTitle,taskContent,taskDeadline,taskProgress,taskAssessResult,taskAssessTime,taskSupervisor,taskCreateTime,taskStatus from app_tasks ");
            strSql.Append(" where taskID=@taskID");
            SqlParameter[] parameters = {
                    new SqlParameter("@taskID", SqlDbType.Int,4)
            };
            parameters[0].Value = taskID;

            appsin.Bizcs.Model.app_tasks model = new appsin.Bizcs.Model.app_tasks();
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
        public appsin.Bizcs.Model.app_tasks DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.app_tasks model = new appsin.Bizcs.Model.app_tasks();
            if (row != null)
            {
                if (row["taskID"] != null && row["taskID"].ToString() != "")
                {
                    model.taskID = int.Parse(row["taskID"].ToString());
                }
                if (row["parentTaskID"] != null && row["parentTaskID"].ToString() != "")
                {
                    model.parentTaskID = int.Parse(row["parentTaskID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["assignPsnID"] != null && row["assignPsnID"].ToString() != "")
                {
                    model.assignPsnID = int.Parse(row["assignPsnID"].ToString());
                }
                if (row["taskTitle"] != null)
                {
                    model.taskTitle = row["taskTitle"].ToString();
                }
                if (row["taskContent"] != null)
                {
                    model.taskContent = row["taskContent"].ToString();
                }
                if (row["taskDeadline"] != null && row["taskDeadline"].ToString() != "")
                {
                    model.taskDeadline = DateTime.Parse(row["taskDeadline"].ToString());
                }
                if (row["taskProgress"] != null && row["taskProgress"].ToString() != "")
                {
                    model.taskProgress = int.Parse(row["taskProgress"].ToString());
                }
                if (row["taskAssessResult"] != null && row["taskAssessResult"].ToString() != "")
                {
                    model.taskAssessResult = int.Parse(row["taskAssessResult"].ToString());
                }
                if (row["taskAssessTime"] != null && row["taskAssessTime"].ToString() != "")
                {
                    model.taskAssessTime = DateTime.Parse(row["taskAssessTime"].ToString());
                }
                if (row["taskSupervisor"] != null && row["taskSupervisor"].ToString() != "")
                {
                    model.taskSupervisor = int.Parse(row["taskSupervisor"].ToString());
                }
                if (row["taskCreateTime"] != null && row["taskCreateTime"].ToString() != "")
                {
                    model.taskCreateTime = DateTime.Parse(row["taskCreateTime"].ToString());
                }
                if (row["taskStatus"] != null && row["taskStatus"].ToString() != "")
                {
                    model.taskStatus = int.Parse(row["taskStatus"].ToString());
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
            strSql.Append("select taskID,parentTaskID,psnID,assignPsnID,taskTitle,taskContent,taskDeadline,taskProgress,taskAssessResult,taskAssessTime,taskSupervisor,taskCreateTime,taskStatus ");
            strSql.Append(" FROM app_tasks ");
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
            strSql.Append("select top " + Top + " taskID,parentTaskID,psnID,assignPsnID,taskTitle,taskContent,taskDeadline,taskProgress,taskAssessResult,taskAssessTime,taskSupervisor,taskCreateTime,taskStatus ");
            strSql.Append(" FROM app_tasks ");
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
                strSql.Append("order by T.taskID desc");
            }
            strSql.Append(")AS Row, T.*  from app_tasks T ");
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
        public DataSet GetSimpleListByPage(string strWhere, int sIndex, int eIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();

            strSql.Append(" WITH ");
            strSql.Append(" todo AS ( ");
            strSql.Append("     SELECT  rn = ROW_NUMBER() OVER (ORDER BY taskDeadline), ");
            strSql.Append("             * ");
            strSql.Append("     FROM    app_tasks ");
            strSql.Append("     WHERE   taskProgress < 100 ");
            strSql.Append(" ), ");
            strSql.Append(" done AS ( ");
            strSql.Append("     SELECT  rn = ROW_NUMBER() OVER (ORDER BY taskDeadline), ");
            strSql.Append("             * ");
            strSql.Append("     FROM    app_tasks ");
            strSql.Append("     WHERE   taskProgress = 100 ");
            strSql.Append(" ), ");
            strSql.Append(" all_list AS ( ");
            strSql.Append("     SELECT  *, 1 AS grp    ");
            strSql.Append("     FROM    todo ");
            strSql.Append("     UNION ALL ");
            strSql.Append("     SELECT  *, 2 AS grp    ");
            strSql.Append("     FROM    done ");
            strSql.Append(" ), ");
            strSql.Append(" numbered AS ( ");
            strSql.Append("     SELECT  ROW_NUMBER() OVER (ORDER BY grp, rn) AS new_rn, ");
            strSql.Append("             * ");
            strSql.Append("     FROM    all_list ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) ");
            strSql.Append(" SELECT  * ");
            strSql.Append(" FROM    numbered ");
            strSql.AppendFormat(" WHERE   new_rn BETWEEN {0} AND {1} ", sIndex, eIndex);
            strSql.Append(" ORDER BY new_rn; ");

            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        #endregion  ExtensionMethod
    }
}