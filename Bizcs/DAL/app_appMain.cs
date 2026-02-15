using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class app_appMain
    {
        public app_appMain()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.app_appMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into app_appMain(");
            strSql.Append("appPK,appName,appSID,appSecret,appSkey,appType,appDomain1,appDomain2,appDomain3,appDescription,appMemo1,appMemo2,appMemo3,appMemo4,appMemo5,validStartTime,validEndTime,displayOrder,createUser,createTime,appStatus)");
            strSql.Append(" values (");
            strSql.Append("@appPK,@appName,@appSID,@appSecret,@appSkey,@appType,@appDomain1,@appDomain2,@appDomain3,@appDescription,@appMemo1,@appMemo2,@appMemo3,@appMemo4,@appMemo5,@validStartTime,@validEndTime,@displayOrder,@createUser,@createTime,@appStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@appPK", SqlDbType.VarChar,100),
                    new SqlParameter("@appName", SqlDbType.VarChar,100),
                    new SqlParameter("@appSID", SqlDbType.VarChar,100),
                    new SqlParameter("@appSecret", SqlDbType.VarChar,100),
                    new SqlParameter("@appSkey", SqlDbType.VarChar,100),
                    new SqlParameter("@appType", SqlDbType.VarChar,100),
                    new SqlParameter("@appDomain1", SqlDbType.VarChar,100),
                    new SqlParameter("@appDomain2", SqlDbType.VarChar,100),
                    new SqlParameter("@appDomain3", SqlDbType.VarChar,100),
                    new SqlParameter("@appDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@validStartTime", SqlDbType.DateTime),
                    new SqlParameter("@validEndTime", SqlDbType.DateTime),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@appStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.appPK;
            parameters[1].Value = model.appName;
            parameters[2].Value = model.appSID;
            parameters[3].Value = model.appSecret;
            parameters[4].Value = model.appSkey;
            parameters[5].Value = model.appType;
            parameters[6].Value = model.appDomain1;
            parameters[7].Value = model.appDomain2;
            parameters[8].Value = model.appDomain3;
            parameters[9].Value = model.appDescription;
            parameters[10].Value = model.appMemo1;
            parameters[11].Value = model.appMemo2;
            parameters[12].Value = model.appMemo3;
            parameters[13].Value = model.appMemo4;
            parameters[14].Value = model.appMemo5;
            parameters[15].Value = model.validStartTime;
            parameters[16].Value = model.validEndTime;
            parameters[17].Value = model.displayOrder;
            parameters[18].Value = model.createUser;
            parameters[19].Value = model.createTime;
            parameters[20].Value = model.appStatus;

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
        public bool Update(Bizcs.Model.app_appMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update app_appMain set ");
            strSql.Append("appPK=@appPK,");
            strSql.Append("appName=@appName,");
            strSql.Append("appSID=@appSID,");
            strSql.Append("appSecret=@appSecret,");
            strSql.Append("appSkey=@appSkey,");
            strSql.Append("appType=@appType,");
            strSql.Append("appDomain1=@appDomain1,");
            strSql.Append("appDomain2=@appDomain2,");
            strSql.Append("appDomain3=@appDomain3,");
            strSql.Append("appDescription=@appDescription,");
            strSql.Append("appMemo1=@appMemo1,");
            strSql.Append("appMemo2=@appMemo2,");
            strSql.Append("appMemo3=@appMemo3,");
            strSql.Append("appMemo4=@appMemo4,");
            strSql.Append("appMemo5=@appMemo5,");
            strSql.Append("validStartTime=@validStartTime,");
            strSql.Append("validEndTime=@validEndTime,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("appStatus=@appStatus");
            strSql.Append(" where appID=@appID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appPK", SqlDbType.VarChar,100),
                    new SqlParameter("@appName", SqlDbType.VarChar,100),
                    new SqlParameter("@appSID", SqlDbType.VarChar,100),
                    new SqlParameter("@appSecret", SqlDbType.VarChar,100),
                    new SqlParameter("@appSkey", SqlDbType.VarChar,100),
                    new SqlParameter("@appType", SqlDbType.VarChar,100),
                    new SqlParameter("@appDomain1", SqlDbType.VarChar,100),
                    new SqlParameter("@appDomain2", SqlDbType.VarChar,100),
                    new SqlParameter("@appDomain3", SqlDbType.VarChar,100),
                    new SqlParameter("@appDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@appMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@validStartTime", SqlDbType.DateTime),
                    new SqlParameter("@validEndTime", SqlDbType.DateTime),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@appStatus", SqlDbType.Int,4),
                    new SqlParameter("@appID", SqlDbType.Int,4)};
            parameters[0].Value = model.appPK;
            parameters[1].Value = model.appName;
            parameters[2].Value = model.appSID;
            parameters[3].Value = model.appSecret;
            parameters[4].Value = model.appSkey;
            parameters[5].Value = model.appType;
            parameters[6].Value = model.appDomain1;
            parameters[7].Value = model.appDomain2;
            parameters[8].Value = model.appDomain3;
            parameters[9].Value = model.appDescription;
            parameters[10].Value = model.appMemo1;
            parameters[11].Value = model.appMemo2;
            parameters[12].Value = model.appMemo3;
            parameters[13].Value = model.appMemo4;
            parameters[14].Value = model.appMemo5;
            parameters[15].Value = model.validStartTime;
            parameters[16].Value = model.validEndTime;
            parameters[17].Value = model.displayOrder;
            parameters[18].Value = model.createUser;
            parameters[19].Value = model.createTime;
            parameters[20].Value = model.appStatus;
            parameters[21].Value = model.appID;

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
        public bool Delete(int appID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from app_appMain ");
            strSql.Append(" where appID=@appID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4)
            };
            parameters[0].Value = appID;

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
        public Bizcs.Model.app_appMain GetModel(int appID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 appID,appPK,appName,appSID,appSecret,appSkey,appType,appDomain1,appDomain2,appDomain3,appDescription,appMemo1,appMemo2,appMemo3,appMemo4,appMemo5,validStartTime,validEndTime,displayOrder,createUser,createTime,appStatus from app_appMain ");
            strSql.Append(" where appID=@appID");
            SqlParameter[] parameters = {
                    new SqlParameter("@appID", SqlDbType.Int,4)
            };
            parameters[0].Value = appID;

            Bizcs.Model.app_appMain model = new Bizcs.Model.app_appMain();
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
        public Bizcs.Model.app_appMain DataRowToModel(DataRow row)
        {
            Bizcs.Model.app_appMain model = new Bizcs.Model.app_appMain();
            if (row != null)
            {
                if (row["appID"] != null && row["appID"].ToString() != "")
                {
                    model.appID = int.Parse(row["appID"].ToString());
                }
                if (row["appPK"] != null)
                {
                    model.appPK = row["appPK"].ToString();
                }
                if (row["appName"] != null)
                {
                    model.appName = row["appName"].ToString();
                }
                if (row["appSID"] != null)
                {
                    model.appSID = row["appSID"].ToString();
                }
                if (row["appSecret"] != null)
                {
                    model.appSecret = row["appSecret"].ToString();
                }
                if (row["appSkey"] != null)
                {
                    model.appSkey = row["appSkey"].ToString();
                }
                if (row["appType"] != null)
                {
                    model.appType = row["appType"].ToString();
                }
                if (row["appDomain1"] != null)
                {
                    model.appDomain1 = row["appDomain1"].ToString();
                }
                if (row["appDomain2"] != null)
                {
                    model.appDomain2 = row["appDomain2"].ToString();
                }
                if (row["appDomain3"] != null)
                {
                    model.appDomain3 = row["appDomain3"].ToString();
                }
                if (row["appDescription"] != null)
                {
                    model.appDescription = row["appDescription"].ToString();
                }
                if (row["appMemo1"] != null)
                {
                    model.appMemo1 = row["appMemo1"].ToString();
                }
                if (row["appMemo2"] != null)
                {
                    model.appMemo2 = row["appMemo2"].ToString();
                }
                if (row["appMemo3"] != null)
                {
                    model.appMemo3 = row["appMemo3"].ToString();
                }
                if (row["appMemo4"] != null)
                {
                    model.appMemo4 = row["appMemo4"].ToString();
                }
                if (row["appMemo5"] != null)
                {
                    model.appMemo5 = row["appMemo5"].ToString();
                }
                if (row["validStartTime"] != null && row["validStartTime"].ToString() != "")
                {
                    model.validStartTime = DateTime.Parse(row["validStartTime"].ToString());
                }
                if (row["validEndTime"] != null && row["validEndTime"].ToString() != "")
                {
                    model.validEndTime = DateTime.Parse(row["validEndTime"].ToString());
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
                if (row["appStatus"] != null && row["appStatus"].ToString() != "")
                {
                    model.appStatus = int.Parse(row["appStatus"].ToString());
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
            strSql.Append("select appID,appPK,appName,appSID,appSecret,appSkey,appType,appDomain1,appDomain2,appDomain3,appDescription,appMemo1,appMemo2,appMemo3,appMemo4,appMemo5,validStartTime,validEndTime,displayOrder,createUser,createTime,appStatus ");
            strSql.Append(" FROM app_appMain ");
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
                strSql.Append("order by T.appID desc");
            }
            strSql.Append(")AS Row, T.*  from app_appMain T ");
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

        public DataSet GetOsrzApp(int psnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select appID,appPK,appName,appSID,appSecret,appSkey,appType,appDomain1,appDomain2,appDomain3,appDescription,validStartTime,validEndTime,
                            createUser,createTime,appStatus 
                            from app_appMain where appStatus=1 and (appID=10001 or appID in
                            (SELECT menuAppID FROM sys_menu WHERE menuStatus=1 and menuID in 
                            (select menuID from sys_menuBind where roleID in
                            ( select osrzRoleID from sys_menuOsrz where 
                            (osrzObjType='psn' and osrzObjID=@psn1)
                            or (osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psn2))
                            or (osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psn3))
                            or (osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psn4))
                            )))) order by displayOrder");

            SqlParameter[] parameters = {
                    new SqlParameter("@psn1", SqlDbType.Int,4),
                    new SqlParameter("@psn2", SqlDbType.Int,4),
                    new SqlParameter("@psn3", SqlDbType.Int,4),
                    new SqlParameter("@psn4", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;
            parameters[1].Value = psnID;
            parameters[2].Value = psnID;
            parameters[3].Value = psnID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds;
        }

        public Bizcs.Model.app_appMain GetModelBySID(string appSID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 appID,appPK,appName,appSID,appSecret,appSkey,appType,appDomain1,appDomain2,appDomain3,appDescription,appMemo1,appMemo2,appMemo3,appMemo4,appMemo5,validStartTime,validEndTime,displayOrder,createUser,createTime,appStatus from app_appMain ");
            strSql.Append(" where appSID=@appSID AND appstatus=1");
            SqlParameter[] parameters = {
                    new SqlParameter("@appSID", SqlDbType.VarChar,50)
            };
            parameters[0].Value = appSID;

            Bizcs.Model.app_appMain model = new Bizcs.Model.app_appMain();
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
            strSql.Append("select appID,appPK,appName,appSID,appSecret,appSkey,appType,appDomain1,appDomain2,appDomain3,appDescription,validStartTime,validEndTime,(select psnName from psn_psnMain where psnID=createUser) as createUser,createTime,appStatus ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.appID desc");
            }
            strSql.Append(")AS Row, T.*  from app_appMain T ");
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
