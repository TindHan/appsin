using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class flow_template
    {
        public flow_template()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.flow_template model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into flow_template(");
            strSql.Append("templatePK,templateName,templateDesc,str1Title,str2Title,str3Title,int1Title,int2Title,int3Title,date1Title,date2Title,date3Title,createUser,createTime,isReady,displayOrder,templateStatus)");
            strSql.Append(" values (");
            strSql.Append("@templatePK,@templateName,@templateDesc,@str1Title,@str2Title,@str3Title,@int1Title,@int2Title,@int3Title,@date1Title,@date2Title,@date3Title,@createUser,@createTime,@isReady,@displayOrder,@templateStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@templatePK", SqlDbType.VarChar,50),
                    new SqlParameter("@templateName", SqlDbType.VarChar,50),
                    new SqlParameter("@templateDesc", SqlDbType.VarChar,500),
                    new SqlParameter("@str1Title", SqlDbType.VarChar,50),
                    new SqlParameter("@str2Title", SqlDbType.VarChar,50),
                    new SqlParameter("@str3Title", SqlDbType.VarChar,50),
                    new SqlParameter("@int1Title", SqlDbType.VarChar,50),
                    new SqlParameter("@int2Title", SqlDbType.VarChar,50),
                    new SqlParameter("@int3Title", SqlDbType.VarChar,50),
                    new SqlParameter("@date1Title", SqlDbType.VarChar,50),
                    new SqlParameter("@date2Title", SqlDbType.VarChar,50),
                    new SqlParameter("@date3Title", SqlDbType.VarChar,50),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@isReady", SqlDbType.Int,4),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@templateStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.templatePK;
            parameters[1].Value = model.templateName;
            parameters[2].Value = model.templateDesc;
            parameters[3].Value = model.str1Title;
            parameters[4].Value = model.str2Title;
            parameters[5].Value = model.str3Title;
            parameters[6].Value = model.int1Title;
            parameters[7].Value = model.int2Title;
            parameters[8].Value = model.int3Title;
            parameters[9].Value = model.date1Title;
            parameters[10].Value = model.date2Title;
            parameters[11].Value = model.date3Title;
            parameters[12].Value = model.createUser;
            parameters[13].Value = model.createTime;
            parameters[14].Value = model.isReady;
            parameters[15].Value = model.displayOrder;
            parameters[16].Value = model.templateStatus;

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
        public bool Update(appsin.Bizcs.Model.flow_template model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update flow_template set ");
            strSql.Append("templatePK=@templatePK,");
            strSql.Append("templateName=@templateName,");
            strSql.Append("templateDesc=@templateDesc,");
            strSql.Append("str1Title=@str1Title,");
            strSql.Append("str2Title=@str2Title,");
            strSql.Append("str3Title=@str3Title,");
            strSql.Append("int1Title=@int1Title,");
            strSql.Append("int2Title=@int2Title,");
            strSql.Append("int3Title=@int3Title,");
            strSql.Append("date1Title=@date1Title,");
            strSql.Append("date2Title=@date2Title,");
            strSql.Append("date3Title=@date3Title,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("isReady=@isReady,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("templateStatus=@templateStatus");
            strSql.Append(" where templateID=@templateID");
            SqlParameter[] parameters = {
                    new SqlParameter("@templatePK", SqlDbType.VarChar,50),
                    new SqlParameter("@templateName", SqlDbType.VarChar,50),
                    new SqlParameter("@templateDesc", SqlDbType.VarChar,500),
                    new SqlParameter("@str1Title", SqlDbType.VarChar,50),
                    new SqlParameter("@str2Title", SqlDbType.VarChar,50),
                    new SqlParameter("@str3Title", SqlDbType.VarChar,50),
                    new SqlParameter("@int1Title", SqlDbType.VarChar,50),
                    new SqlParameter("@int2Title", SqlDbType.VarChar,50),
                    new SqlParameter("@int3Title", SqlDbType.VarChar,50),
                    new SqlParameter("@date1Title", SqlDbType.VarChar,50),
                    new SqlParameter("@date2Title", SqlDbType.VarChar,50),
                    new SqlParameter("@date3Title", SqlDbType.VarChar,50),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@isReady", SqlDbType.Int,4),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@templateStatus", SqlDbType.Int,4),
                    new SqlParameter("@templateID", SqlDbType.Int,4)};
            parameters[0].Value = model.templatePK;
            parameters[1].Value = model.templateName;
            parameters[2].Value = model.templateDesc;
            parameters[3].Value = model.str1Title;
            parameters[4].Value = model.str2Title;
            parameters[5].Value = model.str3Title;
            parameters[6].Value = model.int1Title;
            parameters[7].Value = model.int2Title;
            parameters[8].Value = model.int3Title;
            parameters[9].Value = model.date1Title;
            parameters[10].Value = model.date2Title;
            parameters[11].Value = model.date3Title;
            parameters[12].Value = model.createUser;
            parameters[13].Value = model.createTime;
            parameters[14].Value = model.isReady;
            parameters[15].Value = model.displayOrder;
            parameters[16].Value = model.templateStatus;
            parameters[17].Value = model.templateID;

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
        public bool Delete(int templateID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from flow_template ");
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

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public appsin.Bizcs.Model.flow_template GetModel(int templateID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 templateID,templatePK,templateName,templateDesc,str1Title,str2Title,str3Title,int1Title,int2Title,int3Title,date1Title,date2Title,date3Title,createUser,createTime,isReady,displayOrder,templateStatus from flow_template ");
            strSql.Append(" where templateID=@templateID");
            SqlParameter[] parameters = {
                    new SqlParameter("@templateID", SqlDbType.Int,4)
            };
            parameters[0].Value = templateID;

            appsin.Bizcs.Model.flow_template model = new appsin.Bizcs.Model.flow_template();
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
        public appsin.Bizcs.Model.flow_template DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.flow_template model = new appsin.Bizcs.Model.flow_template();
            if (row != null)
            {
                if (row["templateID"] != null && row["templateID"].ToString() != "")
                {
                    model.templateID = int.Parse(row["templateID"].ToString());
                }
                if (row["templatePK"] != null)
                {
                    model.templatePK = row["templatePK"].ToString();
                }
                if (row["templateName"] != null)
                {
                    model.templateName = row["templateName"].ToString();
                }
                if (row["templateDesc"] != null)
                {
                    model.templateDesc = row["templateDesc"].ToString();
                }
                if (row["str1Title"] != null)
                {
                    model.str1Title = row["str1Title"].ToString();
                }
                if (row["str2Title"] != null)
                {
                    model.str2Title = row["str2Title"].ToString();
                }
                if (row["str3Title"] != null)
                {
                    model.str3Title = row["str3Title"].ToString();
                }
                if (row["int1Title"] != null)
                {
                    model.int1Title = row["int1Title"].ToString();
                }
                if (row["int2Title"] != null)
                {
                    model.int2Title = row["int2Title"].ToString();
                }
                if (row["int3Title"] != null)
                {
                    model.int3Title = row["int3Title"].ToString();
                }
                if (row["date1Title"] != null)
                {
                    model.date1Title = row["date1Title"].ToString();
                }
                if (row["date2Title"] != null)
                {
                    model.date2Title = row["date2Title"].ToString();
                }
                if (row["date3Title"] != null)
                {
                    model.date3Title = row["date3Title"].ToString();
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["isReady"] != null && row["isReady"].ToString() != "")
                {
                    model.isReady = int.Parse(row["isReady"].ToString());
                }
                if (row["displayOrder"] != null && row["displayOrder"].ToString() != "")
                {
                    model.displayOrder = int.Parse(row["displayOrder"].ToString());
                }
                if (row["templateStatus"] != null && row["templateStatus"].ToString() != "")
                {
                    model.templateStatus = int.Parse(row["templateStatus"].ToString());
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
            strSql.Append("select templateID,templatePK,templateName,templateDesc,str1Title,str2Title,str3Title,int1Title,int2Title,int3Title,date1Title,date2Title,date3Title,createUser,createTime,isReady,displayOrder,templateStatus ");
            strSql.Append(" FROM flow_template ");
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
                strSql.Append("order by T.templateID desc");
            }
            strSql.Append(")AS Row, T.*  from flow_template T ");
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
        public appsin.Bizcs.Model.flow_template GetModelByPK(string templatePK)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 templateID,templatePK,templateName,templateDesc,str1Title,str2Title,str3Title,int1Title,int2Title,int3Title,date1Title,date2Title,date3Title,createUser,createTime,isReady,displayOrder,templateStatus from flow_template ");
            strSql.Append(" where templatePK=@templatePK");
            SqlParameter[] parameters = {
                    new SqlParameter("@templatePK", SqlDbType.VarChar,50)
            };
            parameters[0].Value = templatePK;

            appsin.Bizcs.Model.flow_template model = new appsin.Bizcs.Model.flow_template();
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
        /// 获得数据列表
        /// </summary>
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select templateID,templatePK,templateName,templateDesc,str1Title,str2Title,str3Title,int1Title,int2Title,int3Title,date1Title,date2Title,date3Title,createUser,createTime,isReady,displayOrder,templateStatus ");
            strSql.Append(",(select psnName from psn_psnMain where psnID=createUser) as psnName ");
            strSql.Append(" FROM flow_template ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT *,(select psnName from psn_psnMain where psnID=createUser) as psnName FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.templateID desc");
            }
            strSql.Append(")AS Row, T.*  from flow_template T ");
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
