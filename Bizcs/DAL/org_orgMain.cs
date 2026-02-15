using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class org_orgMain
    {
        public org_orgMain()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.org_orgMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into org_orgMain(");
            strSql.Append("orgPK,orgCode,parentID,parentPK,orgLevel,orgType,orgName,chargeUser,chargePost,validStartDate,validEndDate,certPic,displayOrder,orgMemo1,orgMemo2,orgMemo3,orgMemo4,orgMemo5,createTime,orgStatus)");
            strSql.Append(" values (");
            strSql.Append("@orgPK,@orgCode,@parentID,@parentPK,@orgLevel,@orgType,@orgName,@chargeUser,@chargePost,@validStartDate,@validEndDate,@certPic,@displayOrder,@orgMemo1,@orgMemo2,@orgMemo3,@orgMemo4,@orgMemo5,@createTime,@orgStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgPK", SqlDbType.VarChar,100),
                    new SqlParameter("@orgCode", SqlDbType.VarChar,100),
                    new SqlParameter("@parentID", SqlDbType.Int,4),
                    new SqlParameter("@parentPK", SqlDbType.VarChar,100),
                    new SqlParameter("@orgLevel", SqlDbType.VarChar,100),
                    new SqlParameter("@orgType", SqlDbType.VarChar,100),
                    new SqlParameter("@orgName", SqlDbType.VarChar,100),
                    new SqlParameter("@chargeUser", SqlDbType.Int,4),
                    new SqlParameter("@chargePost", SqlDbType.Int,4),
                    new SqlParameter("@validStartDate", SqlDbType.DateTime),
                    new SqlParameter("@validEndDate", SqlDbType.DateTime),
                    new SqlParameter("@certPic", SqlDbType.VarChar,-1),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@orgMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@orgStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.orgPK;
            parameters[1].Value = model.orgCode;
            parameters[2].Value = model.parentID;
            parameters[3].Value = model.parentPK;
            parameters[4].Value = model.orgLevel;
            parameters[5].Value = model.orgType;
            parameters[6].Value = model.orgName;
            parameters[7].Value = model.chargeUser;
            parameters[8].Value = model.chargePost;
            parameters[9].Value = model.validStartDate;
            parameters[10].Value = model.validEndDate;
            parameters[11].Value = model.certPic;
            parameters[12].Value = model.displayOrder;
            parameters[13].Value = model.orgMemo1;
            parameters[14].Value = model.orgMemo2;
            parameters[15].Value = model.orgMemo3;
            parameters[16].Value = model.orgMemo4;
            parameters[17].Value = model.orgMemo5;
            parameters[18].Value = model.createTime;
            parameters[19].Value = model.orgStatus;

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
        public bool Update(Bizcs.Model.org_orgMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update org_orgMain set ");
            strSql.Append("orgPK=@orgPK,");
            strSql.Append("orgCode=@orgCode,");
            strSql.Append("parentID=@parentID,");
            strSql.Append("parentPK=@parentPK,");
            strSql.Append("orgLevel=@orgLevel,");
            strSql.Append("orgType=@orgType,");
            strSql.Append("orgName=@orgName,");
            strSql.Append("chargeUser=@chargeUser,");
            strSql.Append("chargePost=@chargePost,");
            strSql.Append("validStartDate=@validStartDate,");
            strSql.Append("validEndDate=@validEndDate,");
            strSql.Append("certPic=@certPic,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("orgMemo1=@orgMemo1,");
            strSql.Append("orgMemo2=@orgMemo2,");
            strSql.Append("orgMemo3=@orgMemo3,");
            strSql.Append("orgMemo4=@orgMemo4,");
            strSql.Append("orgMemo5=@orgMemo5,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("orgStatus=@orgStatus");
            strSql.Append(" where orgID=@orgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgPK", SqlDbType.VarChar,100),
                    new SqlParameter("@orgCode", SqlDbType.VarChar,100),
                    new SqlParameter("@parentID", SqlDbType.Int,4),
                    new SqlParameter("@parentPK", SqlDbType.VarChar,100),
                    new SqlParameter("@orgLevel", SqlDbType.VarChar,100),
                    new SqlParameter("@orgType", SqlDbType.VarChar,100),
                    new SqlParameter("@orgName", SqlDbType.VarChar,100),
                    new SqlParameter("@chargeUser", SqlDbType.Int,4),
                    new SqlParameter("@chargePost", SqlDbType.Int,4),
                    new SqlParameter("@validStartDate", SqlDbType.DateTime),
                    new SqlParameter("@validEndDate", SqlDbType.DateTime),
                    new SqlParameter("@certPic", SqlDbType.VarChar,-1),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@orgMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@orgMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@orgStatus", SqlDbType.Int,4),
                    new SqlParameter("@orgID", SqlDbType.Int,4)};
            parameters[0].Value = model.orgPK;
            parameters[1].Value = model.orgCode;
            parameters[2].Value = model.parentID;
            parameters[3].Value = model.parentPK;
            parameters[4].Value = model.orgLevel;
            parameters[5].Value = model.orgType;
            parameters[6].Value = model.orgName;
            parameters[7].Value = model.chargeUser;
            parameters[8].Value = model.chargePost;
            parameters[9].Value = model.validStartDate;
            parameters[10].Value = model.validEndDate;
            parameters[11].Value = model.certPic;
            parameters[12].Value = model.displayOrder;
            parameters[13].Value = model.orgMemo1;
            parameters[14].Value = model.orgMemo2;
            parameters[15].Value = model.orgMemo3;
            parameters[16].Value = model.orgMemo4;
            parameters[17].Value = model.orgMemo5;
            parameters[18].Value = model.createTime;
            parameters[19].Value = model.orgStatus;
            parameters[20].Value = model.orgID;

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
        public bool Delete(int orgID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from org_orgMain ");
            strSql.Append(" where orgID=@orgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgID", SqlDbType.Int,4)
            };
            parameters[0].Value = orgID;

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
        public Bizcs.Model.org_orgMain GetModel(int orgID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 orgID,orgPK,orgCode,parentID,parentPK,orgLevel,orgType,orgName,chargeUser,chargePost,validStartDate,validEndDate,certPic,displayOrder,orgMemo1,orgMemo2,orgMemo3,orgMemo4,orgMemo5,createTime,orgStatus from org_orgMain ");
            strSql.Append(" where orgID=@orgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgID", SqlDbType.Int,4)
            };
            parameters[0].Value = orgID;

            Bizcs.Model.org_orgMain model = new Bizcs.Model.org_orgMain();
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
        public Bizcs.Model.org_orgMain DataRowToModel(DataRow row)
        {
            Bizcs.Model.org_orgMain model = new Bizcs.Model.org_orgMain();
            if (row != null)
            {
                if (row["orgID"] != null && row["orgID"].ToString() != "")
                {
                    model.orgID = int.Parse(row["orgID"].ToString());
                }
                if (row["orgPK"] != null)
                {
                    model.orgPK = row["orgPK"].ToString();
                }
                if (row["orgCode"] != null)
                {
                    model.orgCode = row["orgCode"].ToString();
                }
                if (row["parentID"] != null && row["parentID"].ToString() != "")
                {
                    model.parentID = int.Parse(row["parentID"].ToString());
                }
                if (row["parentPK"] != null)
                {
                    model.parentPK = row["parentPK"].ToString();
                }
                if (row["orgLevel"] != null)
                {
                    model.orgLevel = row["orgLevel"].ToString();
                }
                if (row["orgType"] != null)
                {
                    model.orgType = row["orgType"].ToString();
                }
                if (row["orgName"] != null)
                {
                    model.orgName = row["orgName"].ToString();
                }
                if (row["chargeUser"] != null && row["chargeUser"].ToString() != "")
                {
                    model.chargeUser = int.Parse(row["chargeUser"].ToString());
                }
                if (row["chargePost"] != null && row["chargePost"].ToString() != "")
                {
                    model.chargePost = int.Parse(row["chargePost"].ToString());
                }
                if (row["validStartDate"] != null && row["validStartDate"].ToString() != "")
                {
                    model.validStartDate = DateTime.Parse(row["validStartDate"].ToString());
                }
                if (row["validEndDate"] != null && row["validEndDate"].ToString() != "")
                {
                    model.validEndDate = DateTime.Parse(row["validEndDate"].ToString());
                }
                if (row["certPic"] != null)
                {
                    model.certPic = row["certPic"].ToString();
                }
                if (row["displayOrder"] != null && row["displayOrder"].ToString() != "")
                {
                    model.displayOrder = int.Parse(row["displayOrder"].ToString());
                }
                if (row["orgMemo1"] != null)
                {
                    model.orgMemo1 = row["orgMemo1"].ToString();
                }
                if (row["orgMemo2"] != null)
                {
                    model.orgMemo2 = row["orgMemo2"].ToString();
                }
                if (row["orgMemo3"] != null)
                {
                    model.orgMemo3 = row["orgMemo3"].ToString();
                }
                if (row["orgMemo4"] != null)
                {
                    model.orgMemo4 = row["orgMemo4"].ToString();
                }
                if (row["orgMemo5"] != null)
                {
                    model.orgMemo5 = row["orgMemo5"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["orgStatus"] != null && row["orgStatus"].ToString() != "")
                {
                    model.orgStatus = int.Parse(row["orgStatus"].ToString());
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
            strSql.Append("select orgID,orgPK,orgCode,parentID,parentPK,orgLevel,orgType,orgName,chargeUser,chargePost,validStartDate,validEndDate,certPic,displayOrder,orgMemo1,orgMemo2,orgMemo3,orgMemo4,orgMemo5,createTime,orgStatus ");
            strSql.Append(" FROM org_orgMain ");
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
                strSql.Append("order by T.orgID desc");
            }
            strSql.Append(")AS Row, T.*  from org_orgMain T ");
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

        public int GetOrgIDByStr(string orgStr, string orgType)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 orgID");
            strSql.Append(" FROM org_orgMain ");
            strSql.Append(" where (orgName=@orgName or orgCode=@orgCode or orgPK=@orgPK or orgID=@orgID) and orgType=@orgType and orgStatus=1 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgName", SqlDbType.VarChar,100),
                    new SqlParameter("@orgCode", SqlDbType.VarChar,100),
                    new SqlParameter("@orgPK", SqlDbType.VarChar,100),
                    new SqlParameter("@orgID", SqlDbType.Int,4),
                    new SqlParameter("@orgType", SqlDbType.VarChar,100)
            };
            parameters[0].Value = orgStr;
            parameters[1].Value = orgStr;
            parameters[2].Value = orgStr;
            parameters[3].Value = (int.TryParse(orgStr, out int result) ? int.Parse(orgStr) : 0);
            parameters[4].Value = orgType;
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    return int.Parse(ds.Tables[0].Rows[0][0].ToString());
                }
                else
                {
                    return 0;
                }
            }
        }

        public DataSet GetOrgList(string orgPK, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(@"select orgID,orgPK,orgCode,parentID,parentPK,orgLevel,orgType,orgName,
                            (select psnPK from psn_psnMain where psnID = chargeUser) as chargePsn,
                            (select orgPK from org_orgMain where orgID= chargePost) as chargePost,
                            (select orgName from org_orgMain s where o.parentID=s.orgID) as parentName,
                            validStartDate,validEndDate,certPic,displayOrder,orgMemo1,orgMemo2,orgMemo3,
                            orgMemo4,orgMemo5,createTime,orgStatus ");
            strSql.Append(" FROM org_orgMain where orgStatus=1");
            if (orgPK.Trim() != "")
            {
                strSql.Append(" and orgPK='" + orgPK + "'");
            }
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetTreeList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select orgID as id ,parentID as pId,orgType as type," +
                "(case orgStatus when 1 then orgName else orgName+'(invalid)' end) as name ," +
                "(select orgName from org_orgMain s where s.orgID=o.parentID) as pName ," +
                "(case orgType when 'unit' then '/img/unit.png' when 'dept' then '/img/dept.png' when 'post' then '/img/post.png' else '' end) as icon," +
                "'true' as [open] ");
            strSql.Append(" FROM org_orgMain o");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }
        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetOrgsList(string strWhere, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select orgID,orgCode,parentID,(select orgName from org_orgMain s where o.parentID=s.orgID) as parentName,orgLevel,orgType,orgName ");
            strSql.Append(" FROM org_orgMain o");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(), parms);
        }

        public int cancelOrg(string orgLevel)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append(" update org_orgMain set ");
            strSql.Append(" orgStatus=-1 ");
            strSql.Append(" where orglevel like @orgLevel ");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgLevel", SqlDbType.VarChar,150)
            };
            parameters[0].Value = $"{orgLevel}%";
            int rows = DbHelperSQL.ExecuteSql(strSql.ToString());
            return rows;
        }

        public Bizcs.Model.org_orgMain GetFullModel(int orgID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 orgID,orgPK,orgCode,parentID,parentPK,orgLevel,orgType,orgName,chargeUser,chargePost,validStartDate,validEndDate,certPic,displayOrder," +
                "orgMemo1,orgMemo2," +
                "(select psnName from psn_psnMain p where p.psnID=o.chargeUser) as orgMemo3," +
                "(select orgName from org_orgMain n where n.orgID=o.chargePost) as orgMemo4," +
                "(select orgName from org_orgMain n where n.orgID=o.parentID) as orgMemo5," +
                "createTime,orgStatus from org_orgMain o");
            strSql.Append(" where orgID=@orgID");
            SqlParameter[] parameters = {
                    new SqlParameter("@orgID", SqlDbType.Int,4)
            };
            parameters[0].Value = orgID;

            Bizcs.Model.org_orgMain model = new Bizcs.Model.org_orgMain();
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
        #endregion  ExtensionMethod
    }
}
