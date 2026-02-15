using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class api_apiOsrz
    {
        public api_apiOsrz()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.api_apiOsrz model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into api_apiOsrz(");
            strSql.Append("appID,apiID,validStartTime,validEndTime,osrzDescription,osrzMemo1,osrzMemo2,osrzMemo3,osrzMemo4,osrzMemo5,displayOrder,createUser,createTime,osrzStatus)");
            strSql.Append(" values (");
            strSql.Append("@appID,@apiID,@validStartTime,@validEndTime,@osrzDescription,@osrzMemo1,@osrzMemo2,@osrzMemo3,@osrzMemo4,@osrzMemo5,@displayOrder,@createUser,@createTime,@osrzStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@apiID", SqlDbType.Int,4),
                    new SqlParameter("@validStartTime", SqlDbType.DateTime),
                    new SqlParameter("@validEndTime", SqlDbType.DateTime),
                    new SqlParameter("@osrzDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@osrzStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.apiID;
            parameters[2].Value = model.validStartTime;
            parameters[3].Value = model.validEndTime;
            parameters[4].Value = model.osrzDescription;
            parameters[5].Value = model.osrzMemo1;
            parameters[6].Value = model.osrzMemo2;
            parameters[7].Value = model.osrzMemo3;
            parameters[8].Value = model.osrzMemo4;
            parameters[9].Value = model.osrzMemo5;
            parameters[10].Value = model.displayOrder;
            parameters[11].Value = model.createUser;
            parameters[12].Value = model.createTime;
            parameters[13].Value = model.osrzStatus;

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
        public bool Update(Bizcs.Model.api_apiOsrz model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update api_apiOsrz set ");
            strSql.Append("appID=@appID,");
            strSql.Append("apiID=@apiID,");
            strSql.Append("validStartTime=@validStartTime,");
            strSql.Append("validEndTime=@validEndTime,");
            strSql.Append("osrzDescription=@osrzDescription,");
            strSql.Append("osrzMemo1=@osrzMemo1,");
            strSql.Append("osrzMemo2=@osrzMemo2,");
            strSql.Append("osrzMemo3=@osrzMemo3,");
            strSql.Append("osrzMemo4=@osrzMemo4,");
            strSql.Append("osrzMemo5=@osrzMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("osrzStatus=@osrzStatus");
            strSql.Append(" where osrzID=@osrzID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@apiID", SqlDbType.Int,4),
                    new SqlParameter("@validStartTime", SqlDbType.DateTime),
                    new SqlParameter("@validEndTime", SqlDbType.DateTime),
                    new SqlParameter("@osrzDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@osrzMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@osrzStatus", SqlDbType.Int,4),
                    new SqlParameter("@osrzID", SqlDbType.Int,4)};
            parameters[0].Value = model.appID;
            parameters[1].Value = model.apiID;
            parameters[2].Value = model.validStartTime;
            parameters[3].Value = model.validEndTime;
            parameters[4].Value = model.osrzDescription;
            parameters[5].Value = model.osrzMemo1;
            parameters[6].Value = model.osrzMemo2;
            parameters[7].Value = model.osrzMemo3;
            parameters[8].Value = model.osrzMemo4;
            parameters[9].Value = model.osrzMemo5;
            parameters[10].Value = model.displayOrder;
            parameters[11].Value = model.createUser;
            parameters[12].Value = model.createTime;
            parameters[13].Value = model.osrzStatus;
            parameters[14].Value = model.osrzID;

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
        public bool Delete(int osrzID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from api_apiOsrz ");
            strSql.Append(" where osrzID=@osrzID");
            SqlParameter[] parameters = {
                    new SqlParameter("@osrzID", SqlDbType.Int,4)
            };
            parameters[0].Value = osrzID;

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
        public Bizcs.Model.api_apiOsrz GetModel(int osrzID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 osrzID,appID,apiID,validStartTime,validEndTime,osrzDescription,osrzMemo1,osrzMemo2,osrzMemo3,osrzMemo4,osrzMemo5,displayOrder,createUser,createTime,osrzStatus from api_apiOsrz ");
            strSql.Append(" where osrzID=@osrzID");
            SqlParameter[] parameters = {
                    new SqlParameter("@osrzID", SqlDbType.Int,4)
            };
            parameters[0].Value = osrzID;

            Bizcs.Model.api_apiOsrz model = new Bizcs.Model.api_apiOsrz();
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
        public Bizcs.Model.api_apiOsrz DataRowToModel(DataRow row)
        {
            Bizcs.Model.api_apiOsrz model = new Bizcs.Model.api_apiOsrz();
            if (row != null)
            {
                if (row["osrzID"] != null && row["osrzID"].ToString() != "")
                {
                    model.osrzID = int.Parse(row["osrzID"].ToString());
                }
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["apiID"] != null && row["apiID"].ToString() != "")
                {
                    model.apiID = int.Parse(row["apiID"].ToString());
                }
                if (row["validStartTime"] != null && row["validStartTime"].ToString() != "")
                {
                    model.validStartTime = DateTime.Parse(row["validStartTime"].ToString());
                }
                if (row["validEndTime"] != null && row["validEndTime"].ToString() != "")
                {
                    model.validEndTime = DateTime.Parse(row["validEndTime"].ToString());
                }
                if (row["osrzDescription"] != null)
                {
                    model.osrzDescription = row["osrzDescription"].ToString();
                }
                if (row["osrzMemo1"] != null)
                {
                    model.osrzMemo1 = row["osrzMemo1"].ToString();
                }
                if (row["osrzMemo2"] != null)
                {
                    model.osrzMemo2 = row["osrzMemo2"].ToString();
                }
                if (row["osrzMemo3"] != null)
                {
                    model.osrzMemo3 = row["osrzMemo3"].ToString();
                }
                if (row["osrzMemo4"] != null)
                {
                    model.osrzMemo4 = row["osrzMemo4"].ToString();
                }
                if (row["osrzMemo5"] != null)
                {
                    model.osrzMemo5 = row["osrzMemo5"].ToString();
                }
                if (row["displayOrder"] != null && row["displayOrder"].ToString() != "")
                {
                    model.displayOrder = int.Parse(row["displayOrder"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["osrzStatus"] != null && row["osrzStatus"].ToString() != "")
                {
                    model.osrzStatus = int.Parse(row["osrzStatus"].ToString());
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
            strSql.Append("select osrzID,appID,apiID,validStartTime,validEndTime,osrzDescription,osrzMemo1,osrzMemo2,osrzMemo3,osrzMemo4,osrzMemo5,displayOrder,createUser,createTime,osrzStatus ");
            strSql.Append(" FROM api_apiOsrz ");
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
                strSql.Append("order by T.osrzID desc");
            }
            strSql.Append(")AS Row, T.*  from api_apiOsrz T ");
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

        public Bizcs.Model.api_apiOsrz GetModelByID(int appID,int apiID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 osrzID,appID,apiID,validStartTime,validEndTime,osrzDescription,osrzMemo1,osrzMemo2,osrzMemo3,osrzMemo4,osrzMemo5,displayOrder,createUser,createTime,osrzStatus from api_apiOsrz ");
            strSql.Append(" where appID=@appID and apiID=@apiID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4),
                    new SqlParameter("@apiID", SqlDbType.Int,4)
            };
            parameters[0].Value = appID;
            parameters[1].Value = apiID;

            Bizcs.Model.api_apiOsrz model = new Bizcs.Model.api_apiOsrz();
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
        public Bizcs.Model.api_apiOsrz GetModelByCode(string apiCode,string appSID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 osrzID,appID,apiID,validStartTime,validEndTime,osrzDescription,osrzMemo1,osrzMemo2,osrzMemo3,osrzMemo4,osrzMemo5,displayOrder,createUser,createTime,osrzStatus from api_apiOsrz ");
            strSql.Append(" where apiID=(select top 1 apiID from api_apiMain where apiCode=@apiCode and apiStatus=1) ");
            strSql.Append(" and appID = (select top 1 appID from app_appMain where appSID=@appSID and appStatus=1) and osrzStatus=1");

            SqlParameter[] parameters = {
                    new SqlParameter("@apiCode", SqlDbType.VarChar,10),
                    new SqlParameter("@appSID", SqlDbType.VarChar,10)
            };
            parameters[0].Value = apiCode;
            parameters[1].Value = appSID;

            Bizcs.Model.api_apiOsrz model = new Bizcs.Model.api_apiOsrz();
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

        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select osrzID,appID,apiID,");
            strSql.Append("(select appName from app_appMain m where m.appID=TT.appID) as appName, ");
            strSql.Append("(select apiName from api_apiMain a where a.apiID=TT.apiID) as apiName, ");
            strSql.Append("(select apiCode from api_apiMain a where a.apiID=TT.apiID) as apiCode, ");
            strSql.Append(" validStartTime,validEndTime,osrzDescription,displayOrder,(select psnName from psn_psnMain where psnID=createUser) as createUser,createTime,osrzStatus ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.osrzID desc");
            }
            strSql.Append(")AS Row, T.*  from api_apiOsrz T ");
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
