using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_dataRole
    {
        public sys_dataRole()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_dataRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_dataRole(");
            strSql.Append("rolePK,roleName,roleType,roleMemo1,roleMemo2,roleMemo3,roleMemo4,roleMemo5,displayOrder,createTime,createUser,roleStatus)");
            strSql.Append(" values (");
            strSql.Append("@rolePK,@roleName,@roleType,@roleMemo1,@roleMemo2,@roleMemo3,@roleMemo4,@roleMemo5,@displayOrder,@createTime,@createUser,@roleStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@rolePK", SqlDbType.VarChar,100),
                    new SqlParameter("@roleName", SqlDbType.VarChar,100),
                    new SqlParameter("@roleType", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@roleStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.rolePK;
            parameters[1].Value = model.roleName;
            parameters[2].Value = model.roleType;
            parameters[3].Value = model.roleMemo1;
            parameters[4].Value = model.roleMemo2;
            parameters[5].Value = model.roleMemo3;
            parameters[6].Value = model.roleMemo4;
            parameters[7].Value = model.roleMemo5;
            parameters[8].Value = model.displayOrder;
            parameters[9].Value = model.createTime;
            parameters[10].Value = model.createUser;
            parameters[11].Value = model.roleStatus;

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
        public bool Update(Bizcs.Model.sys_dataRole model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_dataRole set ");
            strSql.Append("rolePK=@rolePK,");
            strSql.Append("roleName=@roleName,");
            strSql.Append("roleType=@roleType,");
            strSql.Append("roleMemo1=@roleMemo1,");
            strSql.Append("roleMemo2=@roleMemo2,");
            strSql.Append("roleMemo3=@roleMemo3,");
            strSql.Append("roleMemo4=@roleMemo4,");
            strSql.Append("roleMemo5=@roleMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("roleStatus=@roleStatus");
            strSql.Append(" where roleID=@roleID");
            SqlParameter[] parameters = {
                    new SqlParameter("@rolePK", SqlDbType.VarChar,100),
                    new SqlParameter("@roleName", SqlDbType.VarChar,100),
                    new SqlParameter("@roleType", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@roleMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@roleStatus", SqlDbType.Int,4),
                    new SqlParameter("@roleID", SqlDbType.Int,4)};
            parameters[0].Value = model.rolePK;
            parameters[1].Value = model.roleName;
            parameters[2].Value = model.roleType;
            parameters[3].Value = model.roleMemo1;
            parameters[4].Value = model.roleMemo2;
            parameters[5].Value = model.roleMemo3;
            parameters[6].Value = model.roleMemo4;
            parameters[7].Value = model.roleMemo5;
            parameters[8].Value = model.displayOrder;
            parameters[9].Value = model.createTime;
            parameters[10].Value = model.createUser;
            parameters[11].Value = model.roleStatus;
            parameters[12].Value = model.roleID;

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
        public bool Delete(int roleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_dataRole ");
            strSql.Append(" where roleID=@roleID");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleID", SqlDbType.Int,4)
            };
            parameters[0].Value = roleID;

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
        public Bizcs.Model.sys_dataRole GetModel(int roleID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 roleID,rolePK,roleName,roleType,roleMemo1,roleMemo2,roleMemo3,roleMemo4,roleMemo5,displayOrder,createTime,createUser,roleStatus from sys_dataRole ");
            strSql.Append(" where roleID=@roleID");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleID", SqlDbType.Int,4)
            };
            parameters[0].Value = roleID;

            Bizcs.Model.sys_dataRole model = new Bizcs.Model.sys_dataRole();
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
        public Bizcs.Model.sys_dataRole DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_dataRole model = new Bizcs.Model.sys_dataRole();
            if (row != null)
            {
                if (row["roleID"] != null && row["roleID"].ToString() != "")
                {
                    model.roleID = int.Parse(row["roleID"].ToString());
                }
                if (row["rolePK"] != null)
                {
                    model.rolePK = row["rolePK"].ToString();
                }
                if (row["roleName"] != null)
                {
                    model.roleName = row["roleName"].ToString();
                }
                if (row["roleType"] != null)
                {
                    model.roleType = row["roleType"].ToString();
                }
                if (row["roleMemo1"] != null)
                {
                    model.roleMemo1 = row["roleMemo1"].ToString();
                }
                if (row["roleMemo2"] != null)
                {
                    model.roleMemo2 = row["roleMemo2"].ToString();
                }
                if (row["roleMemo3"] != null)
                {
                    model.roleMemo3 = row["roleMemo3"].ToString();
                }
                if (row["roleMemo4"] != null)
                {
                    model.roleMemo4 = row["roleMemo4"].ToString();
                }
                if (row["roleMemo5"] != null)
                {
                    model.roleMemo5 = row["roleMemo5"].ToString();
                }
                if (row["displayOrder"] != null && row["displayOrder"].ToString() != "")
                {
                    model.displayOrder = int.Parse(row["displayOrder"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["roleStatus"] != null && row["roleStatus"].ToString() != "")
                {
                    model.roleStatus = int.Parse(row["roleStatus"].ToString());
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
            strSql.Append("select roleID,rolePK,roleName,roleType,roleMemo1,roleMemo2,roleMemo3,roleMemo4,roleMemo5,displayOrder,createTime,createUser,roleStatus ");
            strSql.Append(" FROM sys_dataRole ");
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
                strSql.Append("order by T.roleID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_dataRole T ");
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



        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select roleID,rolePK,roleName,roleType,roleMemo1 as roleMemo,displayOrder,createTime,(select psnName from psn_psnMain psn where psn.psnID=TT.createUser) as createUserName,roleStatus ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.roleID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_dataRole T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex);
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }
        #endregion  ExtensionMethod
    }
}
