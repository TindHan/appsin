using DBUtility;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class flow_instance
    {
        public flow_instance()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.flow_instance model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into flow_instance(");
            strSql.Append("instancePK,appID,templateID,templatePK,psnID,psnPK,createPsnID,createPsnPK,instanceName,instanceDesc,createTime,str1Value,str2Value,str3Value,int1Value,int2Value,int3Value,date1Value,date2Value,date3Value,contentUrl,doneNodePK,isEnd,isPass,isError,errorDesc,instanceStatus)");
            strSql.Append(" values (");
            strSql.Append("@instancePK,@appID,@templateID,@templatePK,@psnID,@psnPK,@createPsnID,@createPsnPK,@instanceName,@instanceDesc,@createTime,@str1Value,@str2Value,@str3Value,@int1Value,@int2Value,@int3Value,@date1Value,@date2Value,@date3Value,@contentUrl,@doneNodePK,@isEnd,@isPass,@isError,@errorDesc,@instanceStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@instancePK", SqlDbType.VarChar,50),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@templateID", SqlDbType.Int,4),
                    new SqlParameter("@templatePK", SqlDbType.VarChar,50),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@psnPK", SqlDbType.VarChar,50),
                    new SqlParameter("@createPsnID", SqlDbType.Int,4),
                    new SqlParameter("@createPsnPK", SqlDbType.VarChar,50),
                    new SqlParameter("@instanceName", SqlDbType.VarChar,50),
                    new SqlParameter("@instanceDesc", SqlDbType.VarChar,500),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@str1Value", SqlDbType.VarChar,500),
                    new SqlParameter("@str2Value", SqlDbType.VarChar,500),
                    new SqlParameter("@str3Value", SqlDbType.VarChar,500),
                    new SqlParameter("@int1Value", SqlDbType.Int,4),
                    new SqlParameter("@int2Value", SqlDbType.Int,4),
                    new SqlParameter("@int3Value", SqlDbType.Int,4),
                    new SqlParameter("@date1Value", SqlDbType.DateTime),
                    new SqlParameter("@date2Value", SqlDbType.DateTime),
                    new SqlParameter("@date3Value", SqlDbType.DateTime),
                    new SqlParameter("@contentUrl", SqlDbType.VarChar,1500),
                    new SqlParameter("@doneNodePK", SqlDbType.VarChar,50),
                    new SqlParameter("@isEnd", SqlDbType.Int,4),
                    new SqlParameter("@isPass", SqlDbType.Int,4),
                    new SqlParameter("@isError", SqlDbType.Int,4),
                    new SqlParameter("@errorDesc", SqlDbType.VarChar,1500),
                    new SqlParameter("@instanceStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.instancePK;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.templateID;
            parameters[3].Value = model.templatePK;
            parameters[4].Value = model.psnID;
            parameters[5].Value = model.psnPK;
            parameters[6].Value = model.createPsnID;
            parameters[7].Value = model.createPsnPK;
            parameters[8].Value = model.instanceName;
            parameters[9].Value = model.instanceDesc;
            parameters[10].Value = model.createTime;
            parameters[11].Value = model.str1Value;
            parameters[12].Value = model.str2Value;
            parameters[13].Value = model.str3Value;
            parameters[14].Value = model.int1Value;
            parameters[15].Value = model.int2Value;
            parameters[16].Value = model.int3Value;
            parameters[17].Value = model.date1Value;
            parameters[18].Value = model.date2Value;
            parameters[19].Value = model.date3Value;
            parameters[20].Value = model.contentUrl;
            parameters[21].Value = model.doneNodePK;
            parameters[22].Value = model.isEnd;
            parameters[23].Value = model.isPass;
            parameters[24].Value = model.isError;
            parameters[25].Value = model.errorDesc;
            parameters[26].Value = model.instanceStatus;

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
        public bool Update(appsin.Bizcs.Model.flow_instance model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flow_instance set ");
            strSql.Append("instancePK=@instancePK,");
            strSql.Append("appID=@appID,");
            strSql.Append("templateID=@templateID,");
            strSql.Append("templatePK=@templatePK,");
            strSql.Append("psnID=@psnID,");
            strSql.Append("psnPK=@psnPK,");
            strSql.Append("createPsnID=@createPsnID,");
            strSql.Append("createPsnPK=@createPsnPK,");
            strSql.Append("instanceName=@instanceName,");
            strSql.Append("instanceDesc=@instanceDesc,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("str1Value=@str1Value,");
            strSql.Append("str2Value=@str2Value,");
            strSql.Append("str3Value=@str3Value,");
            strSql.Append("int1Value=@int1Value,");
            strSql.Append("int2Value=@int2Value,");
            strSql.Append("int3Value=@int3Value,");
            strSql.Append("date1Value=@date1Value,");
            strSql.Append("date2Value=@date2Value,");
            strSql.Append("date3Value=@date3Value,");
            strSql.Append("contentUrl=@contentUrl,");
            strSql.Append("doneNodePK=@doneNodePK,");
            strSql.Append("isEnd=@isEnd,");
            strSql.Append("isPass=@isPass,");
            strSql.Append("isError=@isError,");
            strSql.Append("errorDesc=@errorDesc,");
            strSql.Append("instanceStatus=@instanceStatus");
            strSql.Append(" where instanceID=@instanceID");
            SqlParameter[] parameters = {
                    new SqlParameter("@instancePK", SqlDbType.VarChar,50),
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@templateID", SqlDbType.Int,4),
                    new SqlParameter("@templatePK", SqlDbType.VarChar,50),
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@psnPK", SqlDbType.VarChar,50),
                    new SqlParameter("@createPsnID", SqlDbType.Int,4),
                    new SqlParameter("@createPsnPK", SqlDbType.VarChar,50),
                    new SqlParameter("@instanceName", SqlDbType.VarChar,50),
                    new SqlParameter("@instanceDesc", SqlDbType.VarChar,500),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@str1Value", SqlDbType.VarChar,500),
                    new SqlParameter("@str2Value", SqlDbType.VarChar,500),
                    new SqlParameter("@str3Value", SqlDbType.VarChar,500),
                    new SqlParameter("@int1Value", SqlDbType.Int,4),
                    new SqlParameter("@int2Value", SqlDbType.Int,4),
                    new SqlParameter("@int3Value", SqlDbType.Int,4),
                    new SqlParameter("@date1Value", SqlDbType.DateTime),
                    new SqlParameter("@date2Value", SqlDbType.DateTime),
                    new SqlParameter("@date3Value", SqlDbType.DateTime),
                    new SqlParameter("@contentUrl", SqlDbType.VarChar,1500),
                    new SqlParameter("@doneNodePK", SqlDbType.VarChar,50),
                    new SqlParameter("@isEnd", SqlDbType.Int,4),
                    new SqlParameter("@isPass", SqlDbType.Int,4),
                    new SqlParameter("@isError", SqlDbType.Int,4),
                    new SqlParameter("@errorDesc", SqlDbType.VarChar,1500),
                    new SqlParameter("@instanceStatus", SqlDbType.Int,4),
                    new SqlParameter("@instanceID", SqlDbType.Int,4)};
            parameters[0].Value = model.instancePK;
            parameters[1].Value = model.appID;
            parameters[2].Value = model.templateID;
            parameters[3].Value = model.templatePK;
            parameters[4].Value = model.psnID;
            parameters[5].Value = model.psnPK;
            parameters[6].Value = model.createPsnID;
            parameters[7].Value = model.createPsnPK;
            parameters[8].Value = model.instanceName;
            parameters[9].Value = model.instanceDesc;
            parameters[10].Value = model.createTime;
            parameters[11].Value = model.str1Value;
            parameters[12].Value = model.str2Value;
            parameters[13].Value = model.str3Value;
            parameters[14].Value = model.int1Value;
            parameters[15].Value = model.int2Value;
            parameters[16].Value = model.int3Value;
            parameters[17].Value = model.date1Value;
            parameters[18].Value = model.date2Value;
            parameters[19].Value = model.date3Value;
            parameters[20].Value = model.contentUrl;
            parameters[21].Value = model.doneNodePK;
            parameters[22].Value = model.isEnd;
            parameters[23].Value = model.isPass;
            parameters[24].Value = model.isError;
            parameters[25].Value = model.errorDesc;
            parameters[26].Value = model.instanceStatus;
            parameters[27].Value = model.instanceID;

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
        public bool Delete(int instanceID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from flow_instance ");
            strSql.Append(" where instanceID=@instanceID");
            SqlParameter[] parameters = {
                    new SqlParameter("@instanceID", SqlDbType.Int,4)
            };
            parameters[0].Value = instanceID;

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
        public appsin.Bizcs.Model.flow_instance GetModel(int instanceID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 instanceID,instancePK,appID,templateID,templatePK,psnID,psnPK,createPsnID,createPsnPK,instanceName,instanceDesc,createTime,str1Value,str2Value,str3Value,int1Value,int2Value,int3Value,date1Value,date2Value,date3Value,contentUrl,doneNodePK,isEnd,isPass,isError,errorDesc,instanceStatus from flow_instance ");
            strSql.Append(" where instanceID=@instanceID");
            SqlParameter[] parameters = {
                    new SqlParameter("@instanceID", SqlDbType.Int,4)
            };
            parameters[0].Value = instanceID;

            appsin.Bizcs.Model.flow_instance model = new appsin.Bizcs.Model.flow_instance();
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
        public appsin.Bizcs.Model.flow_instance DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.flow_instance model = new appsin.Bizcs.Model.flow_instance();
            if (row != null)
            {
                if (row["instanceID"] != null && row["instanceID"].ToString() != "")
                {
                    model.instanceID = int.Parse(row["instanceID"].ToString());
                }
                if (row["instancePK"] != null)
                {
                    model.instancePK = row["instancePK"].ToString();
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["templateID"] != null && row["templateID"].ToString() != "")
                {
                    model.templateID = int.Parse(row["templateID"].ToString());
                }
                if (row["templatePK"] != null)
                {
                    model.templatePK = row["templatePK"].ToString();
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["psnPK"] != null)
                {
                    model.psnPK = row["psnPK"].ToString();
                }
                if (row["createPsnID"] != null && row["createPsnID"].ToString() != "")
                {
                    model.createPsnID = int.Parse(row["createPsnID"].ToString());
                }
                if (row["createPsnPK"] != null)
                {
                    model.createPsnPK = row["createPsnPK"].ToString();
                }
                if (row["instanceName"] != null)
                {
                    model.instanceName = row["instanceName"].ToString();
                }
                if (row["instanceDesc"] != null)
                {
                    model.instanceDesc = row["instanceDesc"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["str1Value"] != null)
                {
                    model.str1Value = row["str1Value"].ToString();
                }
                if (row["str2Value"] != null)
                {
                    model.str2Value = row["str2Value"].ToString();
                }
                if (row["str3Value"] != null)
                {
                    model.str3Value = row["str3Value"].ToString();
                }
                if (row["int1Value"] != null && row["int1Value"].ToString() != "")
                {
                    model.int1Value = int.Parse(row["int1Value"].ToString());
                }
                if (row["int2Value"] != null && row["int2Value"].ToString() != "")
                {
                    model.int2Value = int.Parse(row["int2Value"].ToString());
                }
                if (row["int3Value"] != null && row["int3Value"].ToString() != "")
                {
                    model.int3Value = int.Parse(row["int3Value"].ToString());
                }
                if (row["date1Value"] != null && row["date1Value"].ToString() != "")
                {
                    model.date1Value = DateTime.Parse(row["date1Value"].ToString());
                }
                if (row["date2Value"] != null && row["date2Value"].ToString() != "")
                {
                    model.date2Value = DateTime.Parse(row["date2Value"].ToString());
                }
                if (row["date3Value"] != null && row["date3Value"].ToString() != "")
                {
                    model.date3Value = DateTime.Parse(row["date3Value"].ToString());
                }
                if (row["contentUrl"] != null)
                {
                    model.contentUrl = row["contentUrl"].ToString();
                }
                if (row["doneNodePK"] != null)
                {
                    model.doneNodePK = row["doneNodePK"].ToString();
                }
                if (row["isEnd"] != null && row["isEnd"].ToString() != "")
                {
                    model.isEnd = int.Parse(row["isEnd"].ToString());
                }
                if (row["isPass"] != null && row["isPass"].ToString() != "")
                {
                    model.isPass = int.Parse(row["isPass"].ToString());
                }
                if (row["isError"] != null && row["isError"].ToString() != "")
                {
                    model.isError = int.Parse(row["isError"].ToString());
                }
                if (row["errorDesc"] != null)
                {
                    model.errorDesc = row["errorDesc"].ToString();
                }
                if (row["instanceStatus"] != null && row["instanceStatus"].ToString() != "")
                {
                    model.instanceStatus = int.Parse(row["instanceStatus"].ToString());
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
            strSql.Append("select instanceID,instancePK,appID,templateID,templatePK,psnID,psnPK,createPsnID,createPsnPK,instanceName,instanceDesc,createTime,str1Value,str2Value,str3Value,int1Value,int2Value,int3Value,date1Value,date2Value,date3Value,contentUrl,doneNodePK,isEnd,isPass,isError,errorDesc,instanceStatus ");
            strSql.Append(" FROM flow_instance ");
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
                strSql.Append("order by T.instanceID desc");
            }
            strSql.Append(")AS Row, T.*  from flow_instance T ");
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


        public DataSet getNextApprover(int instanceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select msgID, msgTitle,msgContent,bizType,bizID,objType,objID,createTime,");
            strSql.Append("(select psnName from psn_psnMain where psnID = objID) as psnName, ");
            strSql.Append("(select orgName from org_orgMain where orgID = (select postID from psn_psnMain where psnID = objID)) as postName, ");
            strSql.Append("(select orgName from org_orgMain where orgID = (select deptID from psn_psnMain where psnID = objID)) as deptName, ");
            strSql.Append("(select orgName from org_orgMain where orgID = (select unitID from psn_psnMain where psnID = objID)) as unitName, ");
            strSql.Append("(select max(readTime) from app_messageRead rd where rd.MsgID = msg.msgID) as readTime, ");
            strSql.Append("(select count(1) from app_messageRead rd where rd.MsgID = msg.msgID) as readCount ");
            strSql.Append("from app_messages msg where objType = 'psn' and bizType = 'FlowNode' and bizID in ");
            strSql.Append("(select nodeID from flow_instanceNode where instanceID = @inst1 and prevNodePK = ");
            strSql.Append("(select top 1 doneNodePK from flow_instance where instanceID = @inst2)) ");
            SqlParameter[] parameters = {
                    new SqlParameter("@inst1", SqlDbType.Int,4),
                    new SqlParameter("@inst2", SqlDbType.Int,4)
            };
            parameters[0].Value = instanceID;
            parameters[1].Value = instanceID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select instanceID,instancePK,appID,templateID,templatePK,psnID,psnPK,createPsnID,createPsnPK,instanceName,instanceDesc,createTime,str1Value,str2Value,str3Value,int1Value,int2Value,int3Value,date1Value,date2Value,date3Value,contentUrl,doneNodePK,isEnd,isPass,isError,errorDesc,instanceStatus ");
            strSql.Append(",(select psnName from psn_psnMain psn where psnID=createPsnID) as createPsnName ");
            strSql.Append(",(select psnName from psn_psnMain psn where psn.psnID=flow_instance.psnID) as psnName ");
            strSql.Append(",(select appName from app_appMain app where app.appID=flow_instance.appID) as appName ");
            strSql.Append(",(select templateName from flow_template temp where temp.templateID=flow_instance.templateID) as templateName ");
            strSql.Append(",(select nodeName from flow_instanceNode node where node.nodePK=flow_instance.doneNodePK and node.instanceID=flow_instance.instanceID) as doneNodeName ");
            strSql.Append(",(select appDomain1+appDomain2 from app_appMain app where app.appID=flow_instance.appID) as domain ");
            strSql.Append(" FROM flow_instance ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        public DataSet GetShowList(int instanceID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select instanceID,instancePK,appID,templateID,templatePK,psnID,psnPK,instanceName,instanceDesc,createTime,str1Value,str2Value,str3Value,int1Value,int2Value,int3Value,date1Value,date2Value,date3Value,contentUrl,doneNodePK,createPsnID,createPsnPK,isEnd,isPass,isError,errorDesc,instanceStatus, ");
            strSql.Append("(select appDomain1+appDomain2 from app_appMain app where app.appID=flow_instance.appID) as domain, ");
            strSql.Append("(select psnName from psn_psnMain where psnID=flow_instance.createPsnID) as createPsn ");
            strSql.Append(" FROM flow_instance where instanceID=@instanceID");
            SqlParameter[] parameters = {
                    new SqlParameter("@instanceID", SqlDbType.Int,4)
            };
            parameters[0].Value = instanceID;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }

        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT * ");
            strSql.Append(",(select psnName from psn_psnMain psn where psnID=createPsnID) as createPsnName ");
            strSql.Append(",(select psnName from psn_psnMain psn where psn.psnID=TT.psnID) as psnName ");
            strSql.Append(",(select appName from app_appMain app where app.appID=TT.appID) as appName ");
            strSql.Append(",(select templateName from flow_template temp where temp.templateID=TT.templateID) as templateName ");
            strSql.Append(",(select nodeName from flow_instanceNode node where node.nodePK=TT.doneNodePK and node.instanceID=TT.instanceID) as doneNodeName ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.instanceID desc");
            }
            strSql.Append(")AS Row, T.*  from flow_instance T ");
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

