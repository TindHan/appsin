using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.Http.Headers;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class flow_approveLog
    {
        public flow_approveLog()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.flow_approveLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into flow_approveLog(");
            strSql.Append("approvePK,instanceID,instancePK,nodeID,nodePK,approverID,approverPK,approverUnit,approverDept,approverPost,isAgree,isNote,isTimes,approveTime)");
            strSql.Append(" values (");
            strSql.Append("@approvePK,@instanceID,@instancePK,@nodeID,@nodePK,@approverID,@approverPK,@approverUnit,@approverDept,@approverPost,@isAgree,@isNote,@isTimes,@approveTime)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@approvePK", SqlDbType.VarChar,50),
                    new SqlParameter("@instanceID", SqlDbType.Int,4),
                    new SqlParameter("@instancePK", SqlDbType.VarChar,50),
                    new SqlParameter("@nodeID", SqlDbType.Int,4),
                    new SqlParameter("@nodePK", SqlDbType.VarChar,50),
                    new SqlParameter("@approverID", SqlDbType.Int,4),
                    new SqlParameter("@approverPK", SqlDbType.VarChar,50),
                    new SqlParameter("@approverUnit", SqlDbType.VarChar,150),
                    new SqlParameter("@approverDept", SqlDbType.VarChar,150),
                    new SqlParameter("@approverPost", SqlDbType.VarChar,150),
                    new SqlParameter("@isAgree", SqlDbType.Int,4),
                    new SqlParameter("@isNote", SqlDbType.VarChar,500),
                    new SqlParameter("@isTimes", SqlDbType.Int,4),
                    new SqlParameter("@approveTime", SqlDbType.DateTime)};
            parameters[0].Value = model.approvePK;
            parameters[1].Value = model.instanceID;
            parameters[2].Value = model.instancePK;
            parameters[3].Value = model.nodeID;
            parameters[4].Value = model.nodePK;
            parameters[5].Value = model.approverID;
            parameters[6].Value = model.approverPK;
            parameters[7].Value = model.approverUnit;
            parameters[8].Value = model.approverDept;
            parameters[9].Value = model.approverPost;
            parameters[10].Value = model.isAgree;
            parameters[11].Value = model.isNote;
            parameters[12].Value = model.isTimes;
            parameters[13].Value = model.approveTime;

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
        public bool Update(appsin.Bizcs.Model.flow_approveLog model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flow_approveLog set ");
            strSql.Append("approvePK=@approvePK,");
            strSql.Append("instanceID=@instanceID,");
            strSql.Append("instancePK=@instancePK,");
            strSql.Append("nodeID=@nodeID,");
            strSql.Append("nodePK=@nodePK,");
            strSql.Append("approverID=@approverID,");
            strSql.Append("approverPK=@approverPK,");
            strSql.Append("approverUnit=@approverUnit,");
            strSql.Append("approverDept=@approverDept,");
            strSql.Append("approverPost=@approverPost,");
            strSql.Append("isAgree=@isAgree,");
            strSql.Append("isNote=@isNote,");
            strSql.Append("isTimes=@isTimes,");
            strSql.Append("approveTime=@approveTime");
            strSql.Append(" where approveID=@approveID");
            SqlParameter[] parameters = {
                    new SqlParameter("@approvePK", SqlDbType.VarChar,50),
                    new SqlParameter("@instanceID", SqlDbType.Int,4),
                    new SqlParameter("@instancePK", SqlDbType.VarChar,50),
                    new SqlParameter("@nodeID", SqlDbType.Int,4),
                    new SqlParameter("@nodePK", SqlDbType.VarChar,50),
                    new SqlParameter("@approverID", SqlDbType.Int,4),
                    new SqlParameter("@approverPK", SqlDbType.VarChar,50),
                    new SqlParameter("@approverUnit", SqlDbType.VarChar,150),
                    new SqlParameter("@approverDept", SqlDbType.VarChar,150),
                    new SqlParameter("@approverPost", SqlDbType.VarChar,150),
                    new SqlParameter("@isAgree", SqlDbType.Int,4),
                    new SqlParameter("@isNote", SqlDbType.VarChar,500),
                    new SqlParameter("@isTimes", SqlDbType.Int,4),
                    new SqlParameter("@approveTime", SqlDbType.DateTime),
                    new SqlParameter("@approveID", SqlDbType.Int,4)};
            parameters[0].Value = model.approvePK;
            parameters[1].Value = model.instanceID;
            parameters[2].Value = model.instancePK;
            parameters[3].Value = model.nodeID;
            parameters[4].Value = model.nodePK;
            parameters[5].Value = model.approverID;
            parameters[6].Value = model.approverPK;
            parameters[7].Value = model.approverUnit;
            parameters[8].Value = model.approverDept;
            parameters[9].Value = model.approverPost;
            parameters[10].Value = model.isAgree;
            parameters[11].Value = model.isNote;
            parameters[12].Value = model.isTimes;
            parameters[13].Value = model.approveTime;
            parameters[14].Value = model.approveID;

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
        public bool Delete(int approveID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from flow_approveLog ");
            strSql.Append(" where approveID=@approveID");
            SqlParameter[] parameters = {
                    new SqlParameter("@approveID", SqlDbType.Int,4)
            };
            parameters[0].Value = approveID;

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
        public appsin.Bizcs.Model.flow_approveLog GetModel(int approveID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 approveID,approvePK,instanceID,instancePK,nodeID,nodePK,approverID,approverPK,approverUnit,approverDept,approverPost,isAgree,isNote,isTimes,approveTime from flow_approveLog ");
            strSql.Append(" where approveID=@approveID");
            SqlParameter[] parameters = {
                    new SqlParameter("@approveID", SqlDbType.Int,4)
            };
            parameters[0].Value = approveID;

            appsin.Bizcs.Model.flow_approveLog model = new appsin.Bizcs.Model.flow_approveLog();
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
        public appsin.Bizcs.Model.flow_approveLog DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.flow_approveLog model = new appsin.Bizcs.Model.flow_approveLog();
            if (row != null)
            {
                if (row["approveID"] != null && row["approveID"].ToString() != "")
                {
                    model.approveID = int.Parse(row["approveID"].ToString());
                }
                if (row["approvePK"] != null)
                {
                    model.approvePK = row["approvePK"].ToString();
                }
                if (row["instanceID"] != null && row["instanceID"].ToString() != "")
                {
                    model.instanceID = int.Parse(row["instanceID"].ToString());
                }
                if (row["instancePK"] != null)
                {
                    model.instancePK = row["instancePK"].ToString();
                }
                if (row["nodeID"] != null && row["nodeID"].ToString() != "")
                {
                    model.nodeID = int.Parse(row["nodeID"].ToString());
                }
                if (row["nodePK"] != null)
                {
                    model.nodePK = row["nodePK"].ToString();
                }
                if (row["approverID"] != null && row["approverID"].ToString() != "")
                {
                    model.approverID = int.Parse(row["approverID"].ToString());
                }
                if (row["approverPK"] != null)
                {
                    model.approverPK = row["approverPK"].ToString();
                }
                if (row["approverUnit"] != null)
                {
                    model.approverUnit = row["approverUnit"].ToString();
                }
                if (row["approverDept"] != null)
                {
                    model.approverDept = row["approverDept"].ToString();
                }
                if (row["approverPost"] != null)
                {
                    model.approverPost = row["approverPost"].ToString();
                }
                if (row["isAgree"] != null && row["isAgree"].ToString() != "")
                {
                    model.isAgree = int.Parse(row["isAgree"].ToString());
                }
                if (row["isNote"] != null)
                {
                    model.isNote = row["isNote"].ToString();
                }
                if (row["isTimes"] != null && row["isTimes"].ToString() != "")
                {
                    model.isTimes = int.Parse(row["isTimes"].ToString());
                }
                if (row["approveTime"] != null && row["approveTime"].ToString() != "")
                {
                    model.approveTime = DateTime.Parse(row["approveTime"].ToString());
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
            strSql.Append("select approveID,approvePK,instanceID,instancePK,nodeID,nodePK,approverID,approverPK,approverUnit,approverDept,approverPost,isAgree,isNote,isTimes,approveTime ");
            strSql.Append(" FROM flow_approveLog ");
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
                strSql.Append("order by T.approveID desc");
            }
            strSql.Append(")AS Row, T.*  from flow_approveLog T ");
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
        public DataSet GetLogList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select approveID,approvePK,instanceID,instancePK,nodeID,nodePK,approverID,approverPK,approverUnit,approverDept,approverPost,isAgree,isNote,isTimes,approveTime, ");
            strSql.Append("(select psnName from psn_psnMain where psnID= approverID) as approverName ");
            strSql.Append(" FROM flow_approveLog ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }
        #endregion  ExtensionMethod
    }
}
