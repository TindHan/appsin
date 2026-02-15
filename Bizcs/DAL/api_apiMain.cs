using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class api_apiMain
    {
        public api_apiMain()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.api_apiMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into api_apiMain(");
            strSql.Append("apiName,apiDescription,apiCode,apiType,apiAddress,apiReqPara,apiResPara,apiKeyNote,apiExample,isIdentify,apiMemo1,apiMemo2,apiMemo3,apiMemo4,apiMemo5,displayOrder,createUser,createTime,apiStatus)");
            strSql.Append(" values (");
            strSql.Append("@apiName,@apiDescription,@apiCode,@apiType,@apiAddress,@apiReqPara,@apiResPara,@apiKeyNote,@apiExample,@isIdentify,@apiMemo1,@apiMemo2,@apiMemo3,@apiMemo4,@apiMemo5,@displayOrder,@createUser,@createTime,@apiStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@apiName", SqlDbType.VarChar,100),
                    new SqlParameter("@apiDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@apiCode", SqlDbType.VarChar,100),
                    new SqlParameter("@apiType", SqlDbType.VarChar,100),
                    new SqlParameter("@apiAddress", SqlDbType.VarChar,1000),
                    new SqlParameter("@apiReqPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@apiResPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@apiKeyNote", SqlDbType.VarChar,5000),
                    new SqlParameter("@apiExample", SqlDbType.VarChar,5000),
                    new SqlParameter("@isIdentify", SqlDbType.Int,4),
                    new SqlParameter("@apiMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@apiStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.apiName;
            parameters[1].Value = model.apiDescription;
            parameters[2].Value = model.apiCode;
            parameters[3].Value = model.apiType;
            parameters[4].Value = model.apiAddress;
            parameters[5].Value = model.apiReqPara;
            parameters[6].Value = model.apiResPara;
            parameters[7].Value = model.apiKeyNote;
            parameters[8].Value = model.apiExample;
            parameters[9].Value = model.isIdentify;
            parameters[10].Value = model.apiMemo1;
            parameters[11].Value = model.apiMemo2;
            parameters[12].Value = model.apiMemo3;
            parameters[13].Value = model.apiMemo4;
            parameters[14].Value = model.apiMemo5;
            parameters[15].Value = model.displayOrder;
            parameters[16].Value = model.createUser;
            parameters[17].Value = model.createTime;
            parameters[18].Value = model.apiStatus;

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
        public bool Update(Bizcs.Model.api_apiMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update api_apiMain set ");
            strSql.Append("apiName=@apiName,");
            strSql.Append("apiDescription=@apiDescription,");
            strSql.Append("apiCode=@apiCode,");
            strSql.Append("apiType=@apiType,");
            strSql.Append("apiAddress=@apiAddress,");
            strSql.Append("apiReqPara=@apiReqPara,");
            strSql.Append("apiResPara=@apiResPara,");
            strSql.Append("apiKeyNote=@apiKeyNote,");
            strSql.Append("apiExample=@apiExample,");
            strSql.Append("isIdentify=@isIdentify,");
            strSql.Append("apiMemo1=@apiMemo1,");
            strSql.Append("apiMemo2=@apiMemo2,");
            strSql.Append("apiMemo3=@apiMemo3,");
            strSql.Append("apiMemo4=@apiMemo4,");
            strSql.Append("apiMemo5=@apiMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("apiStatus=@apiStatus");
            strSql.Append(" where apiID=@apiID");
            SqlParameter[] parameters = {
                    new SqlParameter("@apiName", SqlDbType.VarChar,100),
                    new SqlParameter("@apiDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@apiCode", SqlDbType.VarChar,100),
                    new SqlParameter("@apiType", SqlDbType.VarChar,100),
                    new SqlParameter("@apiAddress", SqlDbType.VarChar,1000),
                    new SqlParameter("@apiReqPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@apiResPara", SqlDbType.VarChar,5000),
                    new SqlParameter("@apiKeyNote", SqlDbType.VarChar,5000),
                    new SqlParameter("@apiExample", SqlDbType.VarChar,5000),
                    new SqlParameter("@isIdentify", SqlDbType.Int,4),
                    new SqlParameter("@apiMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@apiMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@apiStatus", SqlDbType.Int,4),
                    new SqlParameter("@apiID", SqlDbType.Int,4)};
            parameters[0].Value = model.apiName;
            parameters[1].Value = model.apiDescription;
            parameters[2].Value = model.apiCode;
            parameters[3].Value = model.apiType;
            parameters[4].Value = model.apiAddress;
            parameters[5].Value = model.apiReqPara;
            parameters[6].Value = model.apiResPara;
            parameters[7].Value = model.apiKeyNote;
            parameters[8].Value = model.apiExample;
            parameters[9].Value = model.isIdentify;
            parameters[10].Value = model.apiMemo1;
            parameters[11].Value = model.apiMemo2;
            parameters[12].Value = model.apiMemo3;
            parameters[13].Value = model.apiMemo4;
            parameters[14].Value = model.apiMemo5;
            parameters[15].Value = model.displayOrder;
            parameters[16].Value = model.createUser;
            parameters[17].Value = model.createTime;
            parameters[18].Value = model.apiStatus;
            parameters[19].Value = model.apiID;

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
        public bool Delete(int apiID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from api_apiMain ");
            strSql.Append(" where apiID=@apiID");
            SqlParameter[] parameters = {
                    new SqlParameter("@apiID", SqlDbType.Int,4)
            };
            parameters[0].Value = apiID;

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
        public Bizcs.Model.api_apiMain GetModel(int apiID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 apiID,apiName,apiDescription,apiCode,apiType,apiAddress,apiReqPara,apiResPara,apiKeyNote,apiExample,isIdentify,apiMemo1,apiMemo2,apiMemo3,apiMemo4,apiMemo5,displayOrder,createUser,createTime,apiStatus from api_apiMain ");
            strSql.Append(" where apiID=@apiID");
            SqlParameter[] parameters = {
                    new SqlParameter("@apiID", SqlDbType.Int,4)
            };
            parameters[0].Value = apiID;

            Bizcs.Model.api_apiMain model = new Bizcs.Model.api_apiMain();
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
        public Bizcs.Model.api_apiMain DataRowToModel(DataRow row)
        {
            Bizcs.Model.api_apiMain model = new Bizcs.Model.api_apiMain();
            if (row != null)
            {
                if (row["apiID"] != null && row["apiID"].ToString() != "")
                {
                    model.apiID = int.Parse(row["apiID"].ToString());
                }
                if (row["apiName"] != null)
                {
                    model.apiName = row["apiName"].ToString();
                }
                if (row["apiDescription"] != null)
                {
                    model.apiDescription = row["apiDescription"].ToString();
                }
                if (row["apiCode"] != null)
                {
                    model.apiCode = row["apiCode"].ToString();
                }
                if (row["apiType"] != null)
                {
                    model.apiType = row["apiType"].ToString();
                }
                if (row["apiAddress"] != null)
                {
                    model.apiAddress = row["apiAddress"].ToString();
                }
                if (row["apiReqPara"] != null)
                {
                    model.apiReqPara = row["apiReqPara"].ToString();
                }
                if (row["apiResPara"] != null)
                {
                    model.apiResPara = row["apiResPara"].ToString();
                }
                if (row["apiKeyNote"] != null)
                {
                    model.apiKeyNote = row["apiKeyNote"].ToString();
                }
                if (row["apiExample"] != null)
                {
                    model.apiExample = row["apiExample"].ToString();
                }
                if (row["isIdentify"] != null && row["isIdentify"].ToString() != "")
                {
                    model.isIdentify = int.Parse(row["isIdentify"].ToString());
                }
                if (row["apiMemo1"] != null)
                {
                    model.apiMemo1 = row["apiMemo1"].ToString();
                }
                if (row["apiMemo2"] != null)
                {
                    model.apiMemo2 = row["apiMemo2"].ToString();
                }
                if (row["apiMemo3"] != null)
                {
                    model.apiMemo3 = row["apiMemo3"].ToString();
                }
                if (row["apiMemo4"] != null)
                {
                    model.apiMemo4 = row["apiMemo4"].ToString();
                }
                if (row["apiMemo5"] != null)
                {
                    model.apiMemo5 = row["apiMemo5"].ToString();
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
                if (row["apiStatus"] != null && row["apiStatus"].ToString() != "")
                {
                    model.apiStatus = int.Parse(row["apiStatus"].ToString());
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
            strSql.Append("select apiID,apiName,apiDescription,apiCode,apiType,apiAddress,apiReqPara,apiResPara,apiKeyNote,apiExample,isIdentify,apiMemo1,apiMemo2,apiMemo3,apiMemo4,apiMemo5,displayOrder,createUser,createTime,apiStatus ");
            strSql.Append(" FROM api_apiMain ");
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
                strSql.Append("order by T.apiID desc");
            }
            strSql.Append(")AS Row, T.*  from api_apiMain T ");
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

        public Bizcs.Model.api_apiMain GetModelByApiCode(string apiCode)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 apiID,apiName,apiDescription,apiCode,apiType,apiAddress,apiReqPara,apiResPara,apiKeyNote,apiExample,isIdentify,apiMemo1,apiMemo2,apiMemo3,apiMemo4,apiMemo5,displayOrder,createUser,createTime,apiStatus from api_apiMain ");
            strSql.Append(" where apiCode=@apiCode");
            SqlParameter[] parameters = {
                    new SqlParameter("@apiCode", SqlDbType.VarChar,50)
            };
            parameters[0].Value = apiCode;

            Bizcs.Model.api_apiMain model = new Bizcs.Model.api_apiMain();
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
            strSql.Append("select apiID,apiName,apiDescription,apiCode,apiType,apiAddress,apiReqPara,apiResPara,apiKeyNote,apiExample, isIdentify,(select psnName from psn_psnMain where psnID=createUser) as createUser,createTime,displayOrder,apiStatus ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.apiID desc");
            }
            strSql.Append(")AS Row, T.*  from api_apiMain T ");
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
