using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_dataBind
    {
        public sys_dataBind()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_dataBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_dataBind(");
            strSql.Append("roleID,bindType,dynamicOrg,staticOrgID,subOrgIn,createTime,createUser,bindStatus)");
            strSql.Append(" values (");
            strSql.Append("@roleID,@bindType,@dynamicOrg,@staticOrgID,@subOrgIn,@createTime,@createUser,@bindStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleID", SqlDbType.Int,4),
                    new SqlParameter("@bindType", SqlDbType.VarChar,100),
                    new SqlParameter("@dynamicOrg", SqlDbType.VarChar,100),
                    new SqlParameter("@staticOrgID", SqlDbType.Int,4),
                    new SqlParameter("@subOrgIn", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@bindStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.roleID;
            parameters[1].Value = model.bindType;
            parameters[2].Value = model.dynamicOrg;
            parameters[3].Value = model.staticOrgID;
            parameters[4].Value = model.subOrgIn;
            parameters[5].Value = model.createTime;
            parameters[6].Value = model.createUser;
            parameters[7].Value = model.bindStatus;

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
        public bool Update(Bizcs.Model.sys_dataBind model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_dataBind set ");
            strSql.Append("roleID=@roleID,");
            strSql.Append("bindType=@bindType,");
            strSql.Append("dynamicOrg=@dynamicOrg,");
            strSql.Append("staticOrgID=@staticOrgID,");
            strSql.Append("subOrgIn=@subOrgIn,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("bindStatus=@bindStatus");
            strSql.Append(" where bindID=@bindID");
            SqlParameter[] parameters = {
                    new SqlParameter("@roleID", SqlDbType.Int,4),
                    new SqlParameter("@bindType", SqlDbType.VarChar,100),
                    new SqlParameter("@dynamicOrg", SqlDbType.VarChar,100),
                    new SqlParameter("@staticOrgID", SqlDbType.Int,4),
                    new SqlParameter("@subOrgIn", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@bindStatus", SqlDbType.Int,4),
                    new SqlParameter("@bindID", SqlDbType.Int,4)};
            parameters[0].Value = model.roleID;
            parameters[1].Value = model.bindType;
            parameters[2].Value = model.dynamicOrg;
            parameters[3].Value = model.staticOrgID;
            parameters[4].Value = model.subOrgIn;
            parameters[5].Value = model.createTime;
            parameters[6].Value = model.createUser;
            parameters[7].Value = model.bindStatus;
            parameters[8].Value = model.bindID;

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
        public bool Delete(int bindID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_dataBind ");
            strSql.Append(" where bindID=@bindID");
            SqlParameter[] parameters = {
                    new SqlParameter("@bindID", SqlDbType.Int,4)
            };
            parameters[0].Value = bindID;

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
        public Bizcs.Model.sys_dataBind GetModel(int bindID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 bindID,roleID,bindType,dynamicOrg,staticOrgID,subOrgIn,createTime,createUser,bindStatus from sys_dataBind ");
            strSql.Append(" where bindID=@bindID");
            SqlParameter[] parameters = {
                    new SqlParameter("@bindID", SqlDbType.Int,4)
            };
            parameters[0].Value = bindID;

            Bizcs.Model.sys_dataBind model = new Bizcs.Model.sys_dataBind();
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
        public Bizcs.Model.sys_dataBind DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_dataBind model = new Bizcs.Model.sys_dataBind();
            if (row != null)
            {
                if (row["bindID"] != null && row["bindID"].ToString() != "")
                {
                    model.bindID = int.Parse(row["bindID"].ToString());
                }
                if (row["roleID"] != null && row["roleID"].ToString() != "")
                {
                    model.roleID = int.Parse(row["roleID"].ToString());
                }
                if (row["bindType"] != null)
                {
                    model.bindType = row["bindType"].ToString();
                }
                if (row["dynamicOrg"] != null)
                {
                    model.dynamicOrg = row["dynamicOrg"].ToString();
                }
                if (row["staticOrgID"] != null && row["staticOrgID"].ToString() != "")
                {
                    model.staticOrgID = int.Parse(row["staticOrgID"].ToString());
                }
                if (row["subOrgIn"] != null && row["subOrgIn"].ToString() != "")
                {
                    model.subOrgIn = int.Parse(row["subOrgIn"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["bindStatus"] != null && row["bindStatus"].ToString() != "")
                {
                    model.bindStatus = int.Parse(row["bindStatus"].ToString());
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
            strSql.Append("select bindID,roleID,bindType,dynamicOrg,staticOrgID,subOrgIn,createTime,createUser,bindStatus ");
            strSql.Append(" FROM sys_dataBind ");
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
                strSql.Append("order by T.bindID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_dataBind T ");
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
        public DataSet GetSimpleList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select bindID,roleID,bindType,dynamicOrg,staticOrgID,createTime");
            strSql.Append(" ,(select orgName from org_orgMain org where org.orgID = db.staticOrgID) as staticOrgName");
            strSql.Append(" ,(case subOrgIn when 1 then 'Include' else 'Exclude' end) as subOrgIn");
            strSql.Append(" ,(select psnName from psn_psnMain psn where psn.psnID = db.createUser) as createUserName");
            strSql.Append(" FROM sys_dataBind db");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            strSql.Append(" order by createTime desc");
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        public DataSet GetOsrzOrg(int psnID)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select bindID,roleID,bindType,dynamicOrg,staticOrgID,subOrgIn,createTime,createUser,bindStatus,
                            (select orgLevel from org_orgMain where orgID=staticOrgID) as orgLevel 
                            from sys_dataBind where bindStatus=1 and roleID in 
                            (select osrzRoleID from sys_dataOsrz where (osrzObjType='psn' and osrzObjID=@psnID1)
                            or (osrzObjType='unit' and osrzObjID=(select unitID from psn_psnMain where psnID=@psnID2))
                            or (osrzObjType='dept' and osrzObjID=(select deptID from psn_psnMain where psnID=@psnID3))
                            or (osrzObjType='post' and osrzObjID=(select postID from psn_psnMain where psnID=@psnID4)))");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID1", SqlDbType.Int,4),
                    new SqlParameter("@psnID2", SqlDbType.Int,4),
                    new SqlParameter("@psnID3", SqlDbType.Int,4),
                    new SqlParameter("@psnID4", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;
            parameters[1].Value = psnID;
            parameters[2].Value = psnID;
            parameters[3].Value = psnID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            return ds;
        }
        #endregion  ExtensionMethod
    }
}
