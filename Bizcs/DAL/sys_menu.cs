using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_menu
    {
        public sys_menu()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_menu(");
            strSql.Append("menuPK,parentID,parentPK,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,menuMemo1,menuMemo2,menuMemo3,menuMemo4,menuMemo5,displayOrder,createUser,createTime,menuStatus)");
            strSql.Append(" values (");
            strSql.Append("@menuPK,@parentID,@parentPK,@menuName,@menuAppID,@menuLevel,@menuType,@menuLink,@menuIcon,@menuDescription,@menuMemo1,@menuMemo2,@menuMemo3,@menuMemo4,@menuMemo5,@displayOrder,@createUser,@createTime,@menuStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@menuPK", SqlDbType.VarChar,100),
                    new SqlParameter("@parentID", SqlDbType.Int,4),
                    new SqlParameter("@parentPK", SqlDbType.VarChar,100),
                    new SqlParameter("@menuName", SqlDbType.VarChar,100),
                    new SqlParameter("@menuAppID", SqlDbType.Int,4),
                    new SqlParameter("@menuLevel", SqlDbType.Int,4),
                    new SqlParameter("@menuType", SqlDbType.VarChar,100),
                    new SqlParameter("@menuLink", SqlDbType.VarChar,4000),
                    new SqlParameter("@menuIcon", SqlDbType.VarChar,100),
                    new SqlParameter("@menuDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@menuStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.menuPK;
            parameters[1].Value = model.parentID;
            parameters[2].Value = model.parentPK;
            parameters[3].Value = model.menuName;
            parameters[4].Value = model.menuAppID;
            parameters[5].Value = model.menuLevel;
            parameters[6].Value = model.menuType;
            parameters[7].Value = model.menuLink;
            parameters[8].Value = model.menuIcon;
            parameters[9].Value = model.menuDescription;
            parameters[10].Value = model.menuMemo1;
            parameters[11].Value = model.menuMemo2;
            parameters[12].Value = model.menuMemo3;
            parameters[13].Value = model.menuMemo4;
            parameters[14].Value = model.menuMemo5;
            parameters[15].Value = model.displayOrder;
            parameters[16].Value = model.createUser;
            parameters[17].Value = model.createTime;
            parameters[18].Value = model.menuStatus;

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
        public bool Update(Bizcs.Model.sys_menu model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_menu set ");
            strSql.Append("menuPK=@menuPK,");
            strSql.Append("parentID=@parentID,");
            strSql.Append("parentPK=@parentPK,");
            strSql.Append("menuName=@menuName,");
            strSql.Append("menuAppID=@menuAppID,");
            strSql.Append("menuLevel=@menuLevel,");
            strSql.Append("menuType=@menuType,");
            strSql.Append("menuLink=@menuLink,");
            strSql.Append("menuIcon=@menuIcon,");
            strSql.Append("menuDescription=@menuDescription,");
            strSql.Append("menuMemo1=@menuMemo1,");
            strSql.Append("menuMemo2=@menuMemo2,");
            strSql.Append("menuMemo3=@menuMemo3,");
            strSql.Append("menuMemo4=@menuMemo4,");
            strSql.Append("menuMemo5=@menuMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("menuStatus=@menuStatus");
            strSql.Append(" where menuID=@menuID");
            SqlParameter[] parameters = {
                    new SqlParameter("@menuPK", SqlDbType.VarChar,100),
                    new SqlParameter("@parentID", SqlDbType.Int,4),
                    new SqlParameter("@parentPK", SqlDbType.VarChar,100),
                    new SqlParameter("@menuName", SqlDbType.VarChar,100),
                    new SqlParameter("@menuAppID", SqlDbType.Int,4),
                    new SqlParameter("@menuLevel", SqlDbType.Int,4),
                    new SqlParameter("@menuType", SqlDbType.VarChar,100),
                    new SqlParameter("@menuLink", SqlDbType.VarChar,4000),
                    new SqlParameter("@menuIcon", SqlDbType.VarChar,100),
                    new SqlParameter("@menuDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@menuMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@menuStatus", SqlDbType.Int,4),
                    new SqlParameter("@menuID", SqlDbType.Int,4)};
            parameters[0].Value = model.menuPK;
            parameters[1].Value = model.parentID;
            parameters[2].Value = model.parentPK;
            parameters[3].Value = model.menuName;
            parameters[4].Value = model.menuAppID;
            parameters[5].Value = model.menuLevel;
            parameters[6].Value = model.menuType;
            parameters[7].Value = model.menuLink;
            parameters[8].Value = model.menuIcon;
            parameters[9].Value = model.menuDescription;
            parameters[10].Value = model.menuMemo1;
            parameters[11].Value = model.menuMemo2;
            parameters[12].Value = model.menuMemo3;
            parameters[13].Value = model.menuMemo4;
            parameters[14].Value = model.menuMemo5;
            parameters[15].Value = model.displayOrder;
            parameters[16].Value = model.createUser;
            parameters[17].Value = model.createTime;
            parameters[18].Value = model.menuStatus;
            parameters[19].Value = model.menuID;

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
        public bool Delete(int menuID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_menu ");
            strSql.Append(" where menuID=@menuID");
            SqlParameter[] parameters = {
                    new SqlParameter("@menuID", SqlDbType.Int,4)
            };
            parameters[0].Value = menuID;

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
        public Bizcs.Model.sys_menu GetModel(int menuID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 menuID,menuPK,parentID,parentPK,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,menuMemo1,menuMemo2,menuMemo3,menuMemo4,menuMemo5,displayOrder,createUser,createTime,menuStatus from sys_menu ");
            strSql.Append(" where menuID=@menuID");
            SqlParameter[] parameters = {
                    new SqlParameter("@menuID", SqlDbType.Int,4)
            };
            parameters[0].Value = menuID;

            Bizcs.Model.sys_menu model = new Bizcs.Model.sys_menu();
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
        public Bizcs.Model.sys_menu DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_menu model = new Bizcs.Model.sys_menu();
            if (row != null)
            {
                if (row["menuID"] != null && row["menuID"].ToString() != "")
                {
                    model.menuID = int.Parse(row["menuID"].ToString());
                }
                if (row["menuPK"] != null)
                {
                    model.menuPK = row["menuPK"].ToString();
                }
                if (row["parentID"] != null && row["parentID"].ToString() != "")
                {
                    model.parentID = int.Parse(row["parentID"].ToString());
                }
                if (row["parentPK"] != null)
                {
                    model.parentPK = row["parentPK"].ToString();
                }
                if (row["menuName"] != null)
                {
                    model.menuName = row["menuName"].ToString();
                }
                if (row["menuAppID"] != null && row["menuAppID"].ToString() != "")
                {
                    model.menuAppID = int.Parse(row["menuAppID"].ToString());
                }
                if (row["menuLevel"] != null && row["menuLevel"].ToString() != "")
                {
                    model.menuLevel = int.Parse(row["menuLevel"].ToString());
                }
                if (row["menuType"] != null)
                {
                    model.menuType = row["menuType"].ToString();
                }
                if (row["menuLink"] != null)
                {
                    model.menuLink = row["menuLink"].ToString();
                }
                if (row["menuIcon"] != null)
                {
                    model.menuIcon = row["menuIcon"].ToString();
                }
                if (row["menuDescription"] != null)
                {
                    model.menuDescription = row["menuDescription"].ToString();
                }
                if (row["menuMemo1"] != null)
                {
                    model.menuMemo1 = row["menuMemo1"].ToString();
                }
                if (row["menuMemo2"] != null)
                {
                    model.menuMemo2 = row["menuMemo2"].ToString();
                }
                if (row["menuMemo3"] != null)
                {
                    model.menuMemo3 = row["menuMemo3"].ToString();
                }
                if (row["menuMemo4"] != null)
                {
                    model.menuMemo4 = row["menuMemo4"].ToString();
                }
                if (row["menuMemo5"] != null)
                {
                    model.menuMemo5 = row["menuMemo5"].ToString();
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
                if (row["menuStatus"] != null && row["menuStatus"].ToString() != "")
                {
                    model.menuStatus = int.Parse(row["menuStatus"].ToString());
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
            strSql.Append("select menuID,menuPK,parentID,parentPK,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,menuMemo1,menuMemo2,menuMemo3,menuMemo4,menuMemo5,displayOrder,createUser,createTime,menuStatus ");
            strSql.Append(" FROM sys_menu ");
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
                strSql.Append("order by T.menuID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_menu T ");
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

        public DataSet GetPsnRole(int psnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select roleID ,roleName ,roleMemo1, createTime ,'psn' as roleObj, (select psnName from psn_psnMain where psnID=createUser) as createUser from sys_menuRole where roleID in
                            (select osrzRoleID from sys_menuOsrz where (osrzObjType='psn' and osrzObjID=@psn1))
                            UNION 
                            select roleID ,roleName ,roleMemo1, createTime ,'unit' as roleObj, (select psnName from psn_psnMain where psnID=createUser) as createUser from sys_menuRole where roleID in
                            (select osrzRoleID from sys_menuOsrz where osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psn2))
                            UNION 
                            select roleID ,roleName ,roleMemo1, createTime ,'dept' as roleObj,(select psnName from psn_psnMain where psnID=createUser) as createUser from sys_menuRole where roleID in
                            (select osrzRoleID from sys_menuOsrz where osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psn3))
                            UNION 
                            select roleID ,roleName ,roleMemo1, createTime ,'post' as roleObj,(select psnName from psn_psnMain where psnID=createUser) as createUser from sys_menuRole where roleID in
                            (select osrzRoleID from sys_menuOsrz where osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psn4))");

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

        public DataSet GetAdminMenu()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT menuID,parentID,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,displayOrder,
                            (select psnName from psn_psnMain p where p.psnID=createUser) as createUser, createTime,menuStatus 
                            from sys_menu where menuStatus=1 and (menuID=10011 or menuID=10016 or menuID=10019 or parentID=10011 or parentID=10016 or parentID= 10019)
                            order by menuLevel,displayOrder");

            DataSet ds = DbHelperSQL.Query(strSql.ToString());
            return ds;
        }

        public DataSet GetOsrzMenu(int psnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT menuID,parentID,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,displayOrder,
                            (select psnName from psn_psnMain p where p.psnID=createUser) as createUser, createTime,menuStatus 
                            from sys_menu where menuID not in (10011,10016,10019) and (menuID in (10000,10001,10002,10003,10004) or menuID in
                            (SELECT menuID FROM sys_menu WHERE menuType<>'moblink' and menuStatus=1 and menuID in 
                            (select menuID from sys_menuBind where roleID in
                            ( select osrzRoleID from sys_menuOsrz where (osrzObjType='psn' and osrzObjID=@psn1)
                            or (osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psn2))
                            or (osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psn3))
                            or (osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psn4))
                            ))	
                            union
                            SELECT parentID as menuID FROM sys_menu WHERE menuType<>'moblink' and menuStatus=1 and menuID in 
                            (select menuID from sys_menuBind where roleID in
                            ( select osrzRoleID from sys_menuOsrz where (osrzObjType='psn' and osrzObjID=@psn5)
                            or (osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psn6))
                            or (osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psn7))
                            or (osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psn8))
                            ))))
                            order by menuLevel,displayOrder");

            SqlParameter[] parameters = {
                    new SqlParameter("@psn1", SqlDbType.Int,4),
                    new SqlParameter("@psn2", SqlDbType.Int,4),
                    new SqlParameter("@psn3", SqlDbType.Int,4),
                    new SqlParameter("@psn4", SqlDbType.Int,4),
                    new SqlParameter("@psn5", SqlDbType.Int,4),
                    new SqlParameter("@psn6", SqlDbType.Int,4),
                    new SqlParameter("@psn7", SqlDbType.Int,4),
                    new SqlParameter("@psn8", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;
            parameters[1].Value = psnID;
            parameters[2].Value = psnID;
            parameters[3].Value = psnID;
            parameters[4].Value = psnID;
            parameters[5].Value = psnID;
            parameters[6].Value = psnID;
            parameters[7].Value = psnID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds;
        }

        public DataSet GetOsrzMobMenu(int psnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"SELECT menuID,parentID,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,displayOrder,
                            (select psnName from psn_psnMain p where p.psnID=createUser) as createUser, createTime,menuStatus 
                            from sys_menu where menuType='moblink' and menuID in
                            (SELECT menuID FROM sys_menu WHERE menuStatus=1 and menuID in 
                            (select menuID from sys_menuBind where roleID in
                            ( select osrzRoleID from sys_menuOsrz where (osrzObjType='psn' and osrzObjID=@psn1)
                            or (osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psn2))
                            or (osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psn3))
                            or (osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psn4))
                            ))	
                            union
                            SELECT parentID as menuID FROM sys_menu WHERE menuType='moblink' and menuStatus=1 and menuID in 
                            (select menuID from sys_menuBind where roleID in
                            ( select osrzRoleID from sys_menuOsrz where (osrzObjType='psn' and osrzObjID=@psn5)
                            or (osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psn6))
                            or (osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psn7))
                            or (osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psn8))
                            )))
                            order by menuLevel,displayOrder");

            SqlParameter[] parameters = {
                    new SqlParameter("@psn1", SqlDbType.Int,4),
                    new SqlParameter("@psn2", SqlDbType.Int,4),
                    new SqlParameter("@psn3", SqlDbType.Int,4),
                    new SqlParameter("@psn4", SqlDbType.Int,4),
                    new SqlParameter("@psn5", SqlDbType.Int,4),
                    new SqlParameter("@psn6", SqlDbType.Int,4),
                    new SqlParameter("@psn7", SqlDbType.Int,4),
                    new SqlParameter("@psn8", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;
            parameters[1].Value = psnID;
            parameters[2].Value = psnID;
            parameters[3].Value = psnID;
            parameters[4].Value = psnID;
            parameters[5].Value = psnID;
            parameters[6].Value = psnID;
            parameters[7].Value = psnID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds;
        }

        /// </summary>
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select menuID,parentID,menuName,menuAppID,menuLevel,menuType,menuLink,menuIcon,menuDescription,displayOrder, (select psnName from psn_psnMain p where p.psnID=createUser) as createUser, createTime,menuStatus ");
            strSql.Append(" FROM sys_menu as m");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        public DataSet GetSearchList(string kw)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select menuPK,menuName,menuDescription, ");
            strSql.Append("(select menuName from sys_menu menu2 where menu2.menuID=menu1.parentID and menuLevel=1) as moduleName, ");
            strSql.Append("(select appName from app_appMain app where app.appID=menuAppID) as appName ");
            strSql.Append(" FROM sys_menu menu1 where parentID not in (10011,10016,10019) and menuStatus=1 and menuLevel=2 and ( menuName like @kw1 or menuDescription like @kw2 )");
            SqlParameter[] parameters = {
                    new SqlParameter("@kw1", SqlDbType.VarChar,150),
                    new SqlParameter("@kw2", SqlDbType.VarChar,150)
            };
            parameters[0].Value = $"%{kw}%";
            parameters[1].Value = $"%{kw}%";
            return DbHelperSQL.Query(strSql.ToString(),parameters);
        }

        public DataSet GetSearchAdmin(string kw)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select menuPK,menuName,menuDescription, ");
            strSql.Append("(select menuName from sys_menu menu2 where menu2.menuID=menu1.parentID and menuLevel=1) as moduleName, ");
            strSql.Append("(select appName from app_appMain app where app.appID=menuAppID) as appName ");
            strSql.Append(" FROM sys_menu menu1 where parentID in (10011,10016,10019) and menuStatus=1 and menuLevel=2 and ( menuName like @kw1 or menuDescription like @kw2 )");
            SqlParameter[] parameters = {
                    new SqlParameter("@kw1", SqlDbType.VarChar,150),
                    new SqlParameter("@kw2", SqlDbType.VarChar,150)
            }; 
            parameters[0].Value = $"%{kw}%";
            parameters[1].Value = $"%{kw}%";
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }
        #endregion  ExtensionMethod
    }
}
