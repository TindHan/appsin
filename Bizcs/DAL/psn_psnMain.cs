using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class psn_psnMain
    {
        public psn_psnMain()
        { }
        #region  BasicMethod

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(appsin.Bizcs.Model.psn_psnMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_psnMain(");
            strSql.Append("psnPK,unitID,deptID,postID,psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,psnSex,psnNational,psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName,loginStatus,psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,displayOrder,createTime,psnStatus)");
            strSql.Append(" values (");
            strSql.Append("@psnPK,@unitID,@deptID,@postID,@psnName,@aliaName,@psnCode,@IDType,@IDNo,@onStatus,@onType,@psnSex,@psnNational,@psnPicture,@psnEmail,@psnCellPhone,@psnIM,@psnBirthday,@psnJoinday,@psnJobday,@psnPassword,@psnUserName,@loginStatus,@psnMemo1,@psnMemo2,@psnMemo3,@psnMemo4,@psnMemo5,@displayOrder,@createTime,@psnStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnPK", SqlDbType.VarChar,100),
                    new SqlParameter("@unitID", SqlDbType.Int,4),
                    new SqlParameter("@deptID", SqlDbType.Int,4),
                    new SqlParameter("@postID", SqlDbType.Int,4),
                    new SqlParameter("@psnName", SqlDbType.VarChar,100),
                    new SqlParameter("@aliaName", SqlDbType.VarChar,100),
                    new SqlParameter("@psnCode", SqlDbType.VarChar,100),
                    new SqlParameter("@IDType", SqlDbType.VarChar,100),
                    new SqlParameter("@IDNo", SqlDbType.VarChar,100),
                    new SqlParameter("@onStatus", SqlDbType.Int,4),
                    new SqlParameter("@onType", SqlDbType.Int,4),
                    new SqlParameter("@psnSex", SqlDbType.VarChar,100),
                    new SqlParameter("@psnNational", SqlDbType.VarChar,100),
                    new SqlParameter("@psnPicture", SqlDbType.VarChar,-1),
                    new SqlParameter("@psnEmail", SqlDbType.VarChar,100),
                    new SqlParameter("@psnCellPhone", SqlDbType.VarChar,100),
                    new SqlParameter("@psnIM", SqlDbType.VarChar,100),
                    new SqlParameter("@psnBirthday", SqlDbType.Date,3),
                    new SqlParameter("@psnJoinday", SqlDbType.Date,3),
                    new SqlParameter("@psnJobday", SqlDbType.Date,3),
                    new SqlParameter("@psnPassword", SqlDbType.VarChar,1000),
                    new SqlParameter("@psnUserName", SqlDbType.VarChar,100),
                    new SqlParameter("@loginStatus", SqlDbType.Int,4),
                    new SqlParameter("@psnMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@psnStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.psnPK;
            parameters[1].Value = model.unitID;
            parameters[2].Value = model.deptID;
            parameters[3].Value = model.postID;
            parameters[4].Value = model.psnName;
            parameters[5].Value = model.aliaName;
            parameters[6].Value = model.psnCode;
            parameters[7].Value = model.IDType;
            parameters[8].Value = model.IDNo;
            parameters[9].Value = model.onStatus;
            parameters[10].Value = model.onType;
            parameters[11].Value = model.psnSex;
            parameters[12].Value = model.psnNational;
            parameters[13].Value = model.psnPicture;
            parameters[14].Value = model.psnEmail;
            parameters[15].Value = model.psnCellPhone;
            parameters[16].Value = model.psnIM;
            parameters[17].Value = model.psnBirthday;
            parameters[18].Value = model.psnJoinday;
            parameters[19].Value = model.psnJobday;
            parameters[20].Value = model.psnPassword;
            parameters[21].Value = model.psnUserName;
            parameters[22].Value = model.loginStatus;
            parameters[23].Value = model.psnMemo1;
            parameters[24].Value = model.psnMemo2;
            parameters[25].Value = model.psnMemo3;
            parameters[26].Value = model.psnMemo4;
            parameters[27].Value = model.psnMemo5;
            parameters[28].Value = model.displayOrder;
            parameters[29].Value = model.createTime;
            parameters[30].Value = model.psnStatus;

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
        public bool Update(appsin.Bizcs.Model.psn_psnMain model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_psnMain set ");
            strSql.Append("psnPK=@psnPK,");
            strSql.Append("unitID=@unitID,");
            strSql.Append("deptID=@deptID,");
            strSql.Append("postID=@postID,");
            strSql.Append("psnName=@psnName,");
            strSql.Append("aliaName=@aliaName,");
            strSql.Append("psnCode=@psnCode,");
            strSql.Append("IDType=@IDType,");
            strSql.Append("IDNo=@IDNo,");
            strSql.Append("onStatus=@onStatus,");
            strSql.Append("onType=@onType,");
            strSql.Append("psnSex=@psnSex,");
            strSql.Append("psnNational=@psnNational,");
            strSql.Append("psnPicture=@psnPicture,");
            strSql.Append("psnEmail=@psnEmail,");
            strSql.Append("psnCellPhone=@psnCellPhone,");
            strSql.Append("psnIM=@psnIM,");
            strSql.Append("psnBirthday=@psnBirthday,");
            strSql.Append("psnJoinday=@psnJoinday,");
            strSql.Append("psnJobday=@psnJobday,");
            strSql.Append("psnPassword=@psnPassword,");
            strSql.Append("psnUserName=@psnUserName,");
            strSql.Append("loginStatus=@loginStatus,");
            strSql.Append("psnMemo1=@psnMemo1,");
            strSql.Append("psnMemo2=@psnMemo2,");
            strSql.Append("psnMemo3=@psnMemo3,");
            strSql.Append("psnMemo4=@psnMemo4,");
            strSql.Append("psnMemo5=@psnMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("psnStatus=@psnStatus");
            strSql.Append(" where psnID=@psnID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnPK", SqlDbType.VarChar,100),
                    new SqlParameter("@unitID", SqlDbType.Int,4),
                    new SqlParameter("@deptID", SqlDbType.Int,4),
                    new SqlParameter("@postID", SqlDbType.Int,4),
                    new SqlParameter("@psnName", SqlDbType.VarChar,100),
                    new SqlParameter("@aliaName", SqlDbType.VarChar,100),
                    new SqlParameter("@psnCode", SqlDbType.VarChar,100),
                    new SqlParameter("@IDType", SqlDbType.VarChar,100),
                    new SqlParameter("@IDNo", SqlDbType.VarChar,100),
                    new SqlParameter("@onStatus", SqlDbType.Int,4),
                    new SqlParameter("@onType", SqlDbType.Int,4),
                    new SqlParameter("@psnSex", SqlDbType.VarChar,100),
                    new SqlParameter("@psnNational", SqlDbType.VarChar,100),
                    new SqlParameter("@psnPicture", SqlDbType.VarChar,-1),
                    new SqlParameter("@psnEmail", SqlDbType.VarChar,100),
                    new SqlParameter("@psnCellPhone", SqlDbType.VarChar,100),
                    new SqlParameter("@psnIM", SqlDbType.VarChar,100),
                    new SqlParameter("@psnBirthday", SqlDbType.Date,3),
                    new SqlParameter("@psnJoinday", SqlDbType.Date,3),
                    new SqlParameter("@psnJobday", SqlDbType.Date,3),
                    new SqlParameter("@psnPassword", SqlDbType.VarChar,1000),
                    new SqlParameter("@psnUserName", SqlDbType.VarChar,100),
                    new SqlParameter("@loginStatus", SqlDbType.Int,4),
                    new SqlParameter("@psnMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@psnMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@psnStatus", SqlDbType.Int,4),
                    new SqlParameter("@psnID", SqlDbType.Int,4)};
            parameters[0].Value = model.psnPK;
            parameters[1].Value = model.unitID;
            parameters[2].Value = model.deptID;
            parameters[3].Value = model.postID;
            parameters[4].Value = model.psnName;
            parameters[5].Value = model.aliaName;
            parameters[6].Value = model.psnCode;
            parameters[7].Value = model.IDType;
            parameters[8].Value = model.IDNo;
            parameters[9].Value = model.onStatus;
            parameters[10].Value = model.onType;
            parameters[11].Value = model.psnSex;
            parameters[12].Value = model.psnNational;
            parameters[13].Value = model.psnPicture;
            parameters[14].Value = model.psnEmail;
            parameters[15].Value = model.psnCellPhone;
            parameters[16].Value = model.psnIM;
            parameters[17].Value = model.psnBirthday;
            parameters[18].Value = model.psnJoinday;
            parameters[19].Value = model.psnJobday;
            parameters[20].Value = model.psnPassword;
            parameters[21].Value = model.psnUserName;
            parameters[22].Value = model.loginStatus;
            parameters[23].Value = model.psnMemo1;
            parameters[24].Value = model.psnMemo2;
            parameters[25].Value = model.psnMemo3;
            parameters[26].Value = model.psnMemo4;
            parameters[27].Value = model.psnMemo5;
            parameters[28].Value = model.displayOrder;
            parameters[29].Value = model.createTime;
            parameters[30].Value = model.psnStatus;
            parameters[31].Value = model.psnID;

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
        public bool Delete(int psnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from psn_psnMain ");
            strSql.Append(" where psnID=@psnID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;

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
        public appsin.Bizcs.Model.psn_psnMain GetModel(int psnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 psnID,psnPK,unitID,deptID,postID,psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,psnSex,psnNational,psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName,loginStatus,psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,displayOrder,createTime,psnStatus from psn_psnMain ");
            strSql.Append(" where psnID=@psnID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;

            appsin.Bizcs.Model.psn_psnMain model = new appsin.Bizcs.Model.psn_psnMain();
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
        public appsin.Bizcs.Model.psn_psnMain DataRowToModel(DataRow row)
        {
            appsin.Bizcs.Model.psn_psnMain model = new appsin.Bizcs.Model.psn_psnMain();
            if (row != null)
            {
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["psnPK"] != null)
                {
                    model.psnPK = row["psnPK"].ToString();
                }
                if (row["unitID"] != null && row["unitID"].ToString() != "")
                {
                    model.unitID = int.Parse(row["unitID"].ToString());
                }
                if (row["deptID"] != null && row["deptID"].ToString() != "")
                {
                    model.deptID = int.Parse(row["deptID"].ToString());
                }
                if (row["postID"] != null && row["postID"].ToString() != "")
                {
                    model.postID = int.Parse(row["postID"].ToString());
                }
                if (row["psnName"] != null)
                {
                    model.psnName = row["psnName"].ToString();
                }
                if (row["aliaName"] != null)
                {
                    model.aliaName = row["aliaName"].ToString();
                }
                if (row["psnCode"] != null)
                {
                    model.psnCode = row["psnCode"].ToString();
                }
                if (row["IDType"] != null)
                {
                    model.IDType = row["IDType"].ToString();
                }
                if (row["IDNo"] != null)
                {
                    model.IDNo = row["IDNo"].ToString();
                }
                if (row["onStatus"] != null && row["onStatus"].ToString() != "")
                {
                    model.onStatus = int.Parse(row["onStatus"].ToString());
                }
                if (row["onType"] != null && row["onType"].ToString() != "")
                {
                    model.onType = int.Parse(row["onType"].ToString());
                }
                if (row["psnSex"] != null)
                {
                    model.psnSex = row["psnSex"].ToString();
                }
                if (row["psnNational"] != null)
                {
                    model.psnNational = row["psnNational"].ToString();
                }
                if (row["psnPicture"] != null)
                {
                    model.psnPicture = row["psnPicture"].ToString();
                }
                if (row["psnEmail"] != null)
                {
                    model.psnEmail = row["psnEmail"].ToString();
                }
                if (row["psnCellPhone"] != null)
                {
                    model.psnCellPhone = row["psnCellPhone"].ToString();
                }
                if (row["psnIM"] != null)
                {
                    model.psnIM = row["psnIM"].ToString();
                }
                if (row["psnBirthday"] != null && row["psnBirthday"].ToString() != "")
                {
                    model.psnBirthday = DateTime.Parse(row["psnBirthday"].ToString());
                }
                if (row["psnJoinday"] != null && row["psnJoinday"].ToString() != "")
                {
                    model.psnJoinday = DateTime.Parse(row["psnJoinday"].ToString());
                }
                if (row["psnJobday"] != null && row["psnJobday"].ToString() != "")
                {
                    model.psnJobday = DateTime.Parse(row["psnJobday"].ToString());
                }
                if (row["psnPassword"] != null)
                {
                    model.psnPassword = row["psnPassword"].ToString();
                }
                if (row["psnUserName"] != null)
                {
                    model.psnUserName = row["psnUserName"].ToString();
                }
                if (row["loginStatus"] != null && row["loginStatus"].ToString() != "")
                {
                    model.loginStatus = int.Parse(row["loginStatus"].ToString());
                }
                if (row["psnMemo1"] != null)
                {
                    model.psnMemo1 = row["psnMemo1"].ToString();
                }
                if (row["psnMemo2"] != null)
                {
                    model.psnMemo2 = row["psnMemo2"].ToString();
                }
                if (row["psnMemo3"] != null)
                {
                    model.psnMemo3 = row["psnMemo3"].ToString();
                }
                if (row["psnMemo4"] != null)
                {
                    model.psnMemo4 = row["psnMemo4"].ToString();
                }
                if (row["psnMemo5"] != null)
                {
                    model.psnMemo5 = row["psnMemo5"].ToString();
                }
                if (row["displayOrder"] != null && row["displayOrder"].ToString() != "")
                {
                    model.displayOrder = int.Parse(row["displayOrder"].ToString());
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["psnStatus"] != null && row["psnStatus"].ToString() != "")
                {
                    model.psnStatus = int.Parse(row["psnStatus"].ToString());
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
            strSql.Append("select psnID,psnPK,unitID,deptID,postID,psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,psnSex,psnNational,psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName,loginStatus,psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,displayOrder,createTime,psnStatus ");
            strSql.Append(" FROM psn_psnMain ");
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
                strSql.Append("order by T.psnID desc");
            }
            strSql.Append(")AS Row, T.*  from psn_psnMain T ");
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
        public Bizcs.Model.psn_psnMain GetModelByPsnUserName(string psnUserName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 psnID,psnPK,unitID,deptID,postID,psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,psnSex,psnNational,psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName,loginStatus,psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,displayOrder,createTime,psnStatus from psn_psnMain ");
            strSql.Append(" where psnUserName=@psnUserName and psnStatus=1 ");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnUserName", SqlDbType.VarChar,100)
            };
            parameters[0].Value = psnUserName;

            Bizcs.Model.psn_psnMain model = new Bizcs.Model.psn_psnMain();
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
        public Bizcs.Model.psn_psnMain GetModelByPsnPK(string psnPK)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 psnID,psnPK,unitID,deptID,postID,psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,psnSex,psnNational,psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName,loginStatus,psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,displayOrder,createTime,psnStatus from psn_psnMain ");
            strSql.Append(" where psnPK=@psnPK");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnPK", SqlDbType.VarChar,100)
            };
            parameters[0].Value = psnPK;

            Bizcs.Model.psn_psnMain model = new Bizcs.Model.psn_psnMain();
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

        public DataSet GetListByPsnPK(string psnPK)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select top 1 psnID,psnPK,unitID,deptID,postID," +
                "(select orgName from org_orgMain org where org.orgID=psn.unitID) as unitName," +
                "(select orgName from org_orgMain org where org.orgID=psn.deptID) as deptName," +
                "(select orgName from org_orgMain org where org.orgID=psn.postID) as postName," +
                "psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,loginStatus,psnSex,psnNational," +
                "(select itemName from sys_fielditem item where setID=10000 and item.itemID=psn.onStatus) as onStatusName," +
                "(select itemName from sys_fielditem item where setID=10001 and item.itemID=psn.onType) as onTypeName," +
                "(select itemName from sys_fielditem item where setID=10002 and item.itemID=psn.IDType) as IDTypeName," +
                "psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName," +
                "psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,createTime ");
            strSql.Append(" FROM psn_psnMain where psnPK=@psnPk");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnPk", SqlDbType.VarChar,100)
            };
            parameters[0].Value = psnPK;
            return DbHelperSQL.Query(strSql.ToString(), parameters);
        }


        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetSimpleList(string strWhere,params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psnID,psnPK,unitID,deptID,postID," +
                "(select orgName from org_orgMain org where org.orgID=psn.unitID) as unitName," +
                "(select orgName from org_orgMain org where org.orgID=psn.deptID) as deptName," +
                "(select orgName from org_orgMain org where org.orgID=psn.postID) as postName," +
                "(select orgPK from org_orgMain org where org.orgID=psn.unitID) as unitPK," +
                "(select orgPK from org_orgMain org where org.orgID=psn.deptID) as deptPK," +
                "(select orgPK from org_orgMain org where org.orgID=psn.postID) as postPK," +
                "psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,loginStatus,psnSex,psnNational," +
                "(select itemName from sys_fielditem item where setID=10000 and item.itemID=psn.onStatus) as onStatusName," +
                "(select itemName from sys_fielditem item where setID=10001 and item.itemID=psn.onType) as onTypeName," +
                "(select itemName from sys_fielditem item where setID=10002 and item.itemID=psn.IDType) as IDTypeName," +
                "psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName," +
                "psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,createTime ");
            strSql.Append(" FROM psn_psnMain psn ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" where " + strWhere);
            }
            return DbHelperSQL.Query(strSql.ToString(),parms);
        }

        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select psnID,psnPK,unitID,deptID,postID," +
             "(select orgName from org_orgMain org where org.orgID=TT.unitID) as unitName," +
             "(select orgName from org_orgMain org where org.orgID=TT.deptID) as deptName," +
             "(select orgName from org_orgMain org where org.orgID=TT.postID) as postName," +
             "psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,loginStatus,psnSex,psnNational," +
             "(select itemName from sys_fielditem item where setID=10000 and item.itemID=TT.onStatus) as onStatusName," +
             "(select itemName from sys_fielditem item where setID=10001 and item.itemID=TT.onType) as onTypeName," +
             "(select itemName from sys_fielditem item where setID=10002 and item.itemID=TT.IDType) as IDTypeName," +
             "psnPicture,psnEmail,psnCellPhone,psnIM,CONVERT(date,psnBirthday) as psnBirthday,CONVERT(date,psnJoinday) as psnJoinday,CONVERT(date,psnJobday) as psnJobday,psnPassword,psnUserName," +
             "psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,createTime ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.psnID desc");
            }
            strSql.Append(")AS Row, T.*  from psn_psnMain T ");
            if (!string.IsNullOrEmpty(strWhere.Trim()))
            {
                strSql.Append(" WHERE " + strWhere);
            }
            strSql.Append(" ) TT");
            strSql.AppendFormat(" WHERE TT.Row between {0} and {1}", startIndex, endIndex,parms);
            return DbHelperSQL.Query(strSql.ToString());
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public Bizcs.Model.psn_psnMain GetSimpleModel(int psnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 psnID,psnPK,unitID,deptID,postID," +
                "(select orgName from org_orgMain org where org.orgID=psn.unitID) as unitName," +
                "(select orgName from org_orgMain org where org.orgID=psn.deptID) as deptName," +
                "(select orgName from org_orgMain org where org.orgID=psn.postID) as postName," +
                "psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,loginStatus,psnSex,psnNational," +
                "(select itemName from sys_fielditem item where setID=10000 and item.itemID=psn.onStatus) as onStatusName," +
                "(select itemName from sys_fielditem item where setID=10001 and item.itemID=psn.onType) as onTypeName," +
                "(select itemName from sys_fielditem item where setID=10002 and item.itemID=psn.IDType) as IDTypeName," +
                "psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName," +
                "psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,createTime from psn_psnMain psn");
            strSql.Append(" where psnID=@psnID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;

            Bizcs.Model.psn_psnMain model = new Bizcs.Model.psn_psnMain();
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

        public DataSet GetServList(int psnID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 psnPK," +
                "(select orgName from org_orgMain org where org.orgID=psn.unitID) as unitName," +
                "(select orgName from org_orgMain org where org.orgID=psn.deptID) as deptName," +
                "(select orgName from org_orgMain org where org.orgID=psn.postID) as postName," +
                "(select orgPK from org_orgMain org where org.orgID=psn.unitID) as unitPK," +
                "(select orgPK from org_orgMain org where org.orgID=psn.deptID) as deptPK," +
                "(select orgPK from org_orgMain org where org.orgID=psn.postID) as postPK," +
                "psnName,aliaName,psnCode,IDType,IDNo,onStatus,onType,loginStatus,psnSex,psnNational," +
                "(select itemName from sys_fielditem item where setID=10000 and item.itemID=psn.onStatus) as onStatusName," +
                "(select itemName from sys_fielditem item where setID=10001 and item.itemID=psn.onType) as onTypeName," +
                "(select itemName from sys_fielditem item where setID=10002 and item.itemID=psn.IDType) as IDTypeName," +
                "psnPicture,psnEmail,psnCellPhone,psnIM,psnBirthday,psnJoinday,psnJobday,psnPassword,psnUserName," +
                "psnMemo1,psnMemo2,psnMemo3,psnMemo4,psnMemo5,createTime from psn_psnMain psn");
            strSql.Append(" where psnID=@psnID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4)
            };
            parameters[0].Value = psnID;

            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return ds;
            }
            else
            {
                return null;
            }
        }
        #endregion  ExtensionMethod
    }
}
