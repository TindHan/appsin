using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class flow_tempNode
    {
        public flow_tempNode()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.flow_tempNode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into flow_tempNode(");
            strSql.Append("templateID,nodePk,nodeName,prevNodePK,isEnd,approve,condition,[left],[top],type,createUser,createTime,nodeStatus)");
            strSql.Append(" values (");
            strSql.Append("@templateID,@nodePk,@nodeName,@prevNodePK,@isEnd,@approve,@condition,@left,@top,@type,@createUser,@createTime,@nodeStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@templateID", SqlDbType.Int,4),
                    new SqlParameter("@nodePk", SqlDbType.VarChar,50),
                    new SqlParameter("@nodeName", SqlDbType.VarChar,50),
                    new SqlParameter("@prevNodePK", SqlDbType.VarChar,50),
                    new SqlParameter("@isEnd", SqlDbType.Int,4),
                    new SqlParameter("@approve", SqlDbType.VarChar,50),
                    new SqlParameter("@condition", SqlDbType.VarChar,500),
                    new SqlParameter("@left", SqlDbType.Decimal,9),
                    new SqlParameter("@top", SqlDbType.Decimal,9),
                    new SqlParameter("@type", SqlDbType.VarChar,50),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@nodeStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.templateID;
            parameters[1].Value = model.nodePk;
            parameters[2].Value = model.nodeName;
            parameters[3].Value = model.prevNodePK;
            parameters[4].Value = model.isEnd;
            parameters[5].Value = model.approve;
            parameters[6].Value = model.condition;
            parameters[7].Value = model.left;
            parameters[8].Value = model.top;
            parameters[9].Value = model.type;
            parameters[10].Value = model.createUser;
            parameters[11].Value = model.createTime;
            parameters[12].Value = model.nodeStatus;

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
        public bool Update(appsin.Bizcs.Model.flow_tempNode model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flow_tempNode set ");
            strSql.Append("templateID=@templateID,");
            strSql.Append("nodePk=@nodePk,");
            strSql.Append("nodeName=@nodeName,");
            strSql.Append("prevNodePK=@prevNodePK,");
            strSql.Append("isEnd=@isEnd,");
            strSql.Append("approve=@approve,");
            strSql.Append("condition=@condition,");
            strSql.Append("[left]=@left,");
            strSql.Append("[top]=@top,");
            strSql.Append("type=@type,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("nodeStatus=@nodeStatus");
            strSql.Append(" where nodeID=@nodeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@templateID", SqlDbType.Int,4),
                    new SqlParameter("@nodePk", SqlDbType.VarChar,50),
                    new SqlParameter("@nodeName", SqlDbType.VarChar,50),
                    new SqlParameter("@prevNodePK", SqlDbType.VarChar,50),
                    new SqlParameter("@isEnd", SqlDbType.Int,4),
                    new SqlParameter("@approve", SqlDbType.VarChar,50),
                    new SqlParameter("@condition", SqlDbType.VarChar,500),
                    new SqlParameter("@left", SqlDbType.Decimal,9),
                    new SqlParameter("@top", SqlDbType.Decimal,9),
                    new SqlParameter("@type", SqlDbType.VarChar,50),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@nodeStatus", SqlDbType.Int,4),
                    new SqlParameter("@nodeID", SqlDbType.Int,4)};
            parameters[0].Value = model.templateID;
            parameters[1].Value = model.nodePk;
            parameters[2].Value = model.nodeName;
            parameters[3].Value = model.prevNodePK;
            parameters[4].Value = model.isEnd;
            parameters[5].Value = model.approve;
            parameters[6].Value = model.condition;
            parameters[7].Value = model.left;
            parameters[8].Value = model.top;
            parameters[9].Value = model.type;
            parameters[10].Value = model.createUser;
            parameters[11].Value = model.createTime;
            parameters[12].Value = model.nodeStatus;
            parameters[13].Value = model.nodeID;

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
        public bool Delete(int nodeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from flow_tempNode ");
            strSql.Append(" where nodeID=@nodeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@nodeID", SqlDbType.Int,4)
            };
            parameters[0].Value = nodeID;

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
        public appsin.Bizcs.Model.flow_tempNode GetModel(int nodeID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 nodeID,templateID,nodePk,nodeName,prevNodePK,isEnd,approve,condition,[left],[top],type,createUser,createTime,nodeStatus from flow_tempNode ");
            strSql.Append(" where nodeID=@nodeID");
            SqlParameter[] parameters = {
                    new SqlParameter("@nodeID", SqlDbType.Int,4)
            };
            parameters[0].Value = nodeID;

            appsin.Bizcs.Model.flow_tempNode model = new appsin.Bizcs.Model.flow_tempNode();
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
        public appsin.Bizcs.Model.flow_tempNode DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.flow_tempNode model = new appsin.Bizcs.Model.flow_tempNode();
            if (row != null)
            {
                if (row["nodeID"] != null && row["nodeID"].ToString() != "")
                {
                    model.nodeID = int.Parse(row["nodeID"].ToString());
                }
                if (row["templateID"] != null && row["templateID"].ToString() != "")
                {
                    model.templateID = int.Parse(row["templateID"].ToString());
                }
                if (row["nodePk"] != null)
                {
                    model.nodePk = row["nodePk"].ToString();
                }
                if (row["nodeName"] != null)
                {
                    model.nodeName = row["nodeName"].ToString();
                }
                if (row["prevNodePK"] != null)
                {
                    model.prevNodePK = row["prevNodePK"].ToString();
                }
                if (row["isEnd"] != null && row["isEnd"].ToString() != "")
                {
                    model.isEnd = int.Parse(row["isEnd"].ToString());
                }
                if (row["approve"] != null)
                {
                    model.approve = row["approve"].ToString();
                }
                if (row["condition"] != null)
                {
                    model.condition = row["condition"].ToString();
                }
                if (row["left"] != null && row["left"].ToString() != "")
                {
                    model.left = decimal.Parse(row["left"].ToString());
                }
                if (row["top"] != null && row["top"].ToString() != "")
                {
                    model.top = decimal.Parse(row["top"].ToString());
                }
                if (row["type"] != null)
                {
                    model.type = row["type"].ToString();
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["nodeStatus"] != null && row["nodeStatus"].ToString() != "")
                {
                    model.nodeStatus = int.Parse(row["nodeStatus"].ToString());
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
            strSql.Append("select nodeID,templateID,nodePk,nodeName,prevNodePK,isEnd,approve,condition,[left],[top],type,createUser,createTime,nodeStatus ");
            strSql.Append(" FROM flow_tempNode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }


        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex,params SqlParameter[] parms)
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
                strSql.Append("order by T.nodeID desc");
            }
            strSql.Append(")AS Row, T.*  from flow_tempNode T ");
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
        public DataSet GetNodeList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select templateID, nodePk as nodePK,nodeName as name,prevNodePK as prevID,isEnd,approve,condition,[left],[top],type ");
            strSql.Append(" FROM flow_tempNode ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        public bool DeleteBeforeSave(int templateID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from flow_tempNode ");
            strSql.Append(" where templateID=@templateID");
            SqlParameter[] parameters = {
                    new SqlParameter("@templateID", SqlDbType.Int,4)
            };
            parameters[0].Value = templateID;

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
        #endregion  ExtensionMethod
    }
}
