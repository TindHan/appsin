using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class psn_transfer
    {
        public psn_transfer()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.psn_transfer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into psn_transfer(");
            strSql.Append("psnID,transferType,preUnitName,preDeptName,prePostName,preOnType,preOnStatus,nextUnitName,nextDeptName,nextPostName,nextOnType,nextOnStatus,transferTime,transferMemo1,transferMemo2,transferMemo3,transferMemo4,transferMemo5,createTime,createUser,transferStatus)");
            strSql.Append(" values (");
            strSql.Append("@psnID,@transferType,@preUnitName,@preDeptName,@prePostName,@preOnType,@preOnStatus,@nextUnitName,@nextDeptName,@nextPostName,@nextOnType,@nextOnStatus,@transferTime,@transferMemo1,@transferMemo2,@transferMemo3,@transferMemo4,@transferMemo5,@createTime,@createUser,@transferStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@transferType", SqlDbType.VarChar,100),
                    new SqlParameter("@preUnitName", SqlDbType.VarChar,100),
                    new SqlParameter("@preDeptName", SqlDbType.VarChar,100),
                    new SqlParameter("@prePostName", SqlDbType.VarChar,100),
                    new SqlParameter("@preOnType", SqlDbType.VarChar,100),
                    new SqlParameter("@preOnStatus", SqlDbType.VarChar,100),
                    new SqlParameter("@nextUnitName", SqlDbType.VarChar,100),
                    new SqlParameter("@nextDeptName", SqlDbType.VarChar,100),
                    new SqlParameter("@nextPostName", SqlDbType.VarChar,100),
                    new SqlParameter("@nextOnType", SqlDbType.VarChar,100),
                    new SqlParameter("@nextOnStatus", SqlDbType.VarChar,100),
                    new SqlParameter("@transferTime", SqlDbType.DateTime),
                    new SqlParameter("@transferMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@transferStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.transferType;
            parameters[2].Value = model.preUnitName;
            parameters[3].Value = model.preDeptName;
            parameters[4].Value = model.prePostName;
            parameters[5].Value = model.preOnType;
            parameters[6].Value = model.preOnStatus;
            parameters[7].Value = model.nextUnitName;
            parameters[8].Value = model.nextDeptName;
            parameters[9].Value = model.nextPostName;
            parameters[10].Value = model.nextOnType;
            parameters[11].Value = model.nextOnStatus;
            parameters[12].Value = model.transferTime;
            parameters[13].Value = model.transferMemo1;
            parameters[14].Value = model.transferMemo2;
            parameters[15].Value = model.transferMemo3;
            parameters[16].Value = model.transferMemo4;
            parameters[17].Value = model.transferMemo5;
            parameters[18].Value = model.createTime;
            parameters[19].Value = model.createUser;
            parameters[20].Value = model.transferStatus;

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
        public bool Update(Bizcs.Model.psn_transfer model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update psn_transfer set ");
            strSql.Append("psnID=@psnID,");
            strSql.Append("transferType=@transferType,");
            strSql.Append("preUnitName=@preUnitName,");
            strSql.Append("preDeptName=@preDeptName,");
            strSql.Append("prePostName=@prePostName,");
            strSql.Append("preOnType=@preOnType,");
            strSql.Append("preOnStatus=@preOnStatus,");
            strSql.Append("nextUnitName=@nextUnitName,");
            strSql.Append("nextDeptName=@nextDeptName,");
            strSql.Append("nextPostName=@nextPostName,");
            strSql.Append("nextOnType=@nextOnType,");
            strSql.Append("nextOnStatus=@nextOnStatus,");
            strSql.Append("transferTime=@transferTime,");
            strSql.Append("transferMemo1=@transferMemo1,");
            strSql.Append("transferMemo2=@transferMemo2,");
            strSql.Append("transferMemo3=@transferMemo3,");
            strSql.Append("transferMemo4=@transferMemo4,");
            strSql.Append("transferMemo5=@transferMemo5,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("transferStatus=@transferStatus");
            strSql.Append(" where transferID=@transferID");
            SqlParameter[] parameters = {
                    new SqlParameter("@psnID", SqlDbType.Int,4),
                    new SqlParameter("@transferType", SqlDbType.VarChar,100),
                    new SqlParameter("@preUnitName", SqlDbType.VarChar,100),
                    new SqlParameter("@preDeptName", SqlDbType.VarChar,100),
                    new SqlParameter("@prePostName", SqlDbType.VarChar,100),
                    new SqlParameter("@preOnType", SqlDbType.VarChar,100),
                    new SqlParameter("@preOnStatus", SqlDbType.VarChar,100),
                    new SqlParameter("@nextUnitName", SqlDbType.VarChar,100),
                    new SqlParameter("@nextDeptName", SqlDbType.VarChar,100),
                    new SqlParameter("@nextPostName", SqlDbType.VarChar,100),
                    new SqlParameter("@nextOnType", SqlDbType.VarChar,100),
                    new SqlParameter("@nextOnStatus", SqlDbType.VarChar,100),
                    new SqlParameter("@transferTime", SqlDbType.DateTime),
                    new SqlParameter("@transferMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@transferMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@transferStatus", SqlDbType.Int,4),
                    new SqlParameter("@transferID", SqlDbType.Int,4)};
            parameters[0].Value = model.psnID;
            parameters[1].Value = model.transferType;
            parameters[2].Value = model.preUnitName;
            parameters[3].Value = model.preDeptName;
            parameters[4].Value = model.prePostName;
            parameters[5].Value = model.preOnType;
            parameters[6].Value = model.preOnStatus;
            parameters[7].Value = model.nextUnitName;
            parameters[8].Value = model.nextDeptName;
            parameters[9].Value = model.nextPostName;
            parameters[10].Value = model.nextOnType;
            parameters[11].Value = model.nextOnStatus;
            parameters[12].Value = model.transferTime;
            parameters[13].Value = model.transferMemo1;
            parameters[14].Value = model.transferMemo2;
            parameters[15].Value = model.transferMemo3;
            parameters[16].Value = model.transferMemo4;
            parameters[17].Value = model.transferMemo5;
            parameters[18].Value = model.createTime;
            parameters[19].Value = model.createUser;
            parameters[20].Value = model.transferStatus;
            parameters[21].Value = model.transferID;

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
        public bool Delete(int transferID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from psn_transfer ");
            strSql.Append(" where transferID=@transferID");
            SqlParameter[] parameters = {
                    new SqlParameter("@transferID", SqlDbType.Int,4)
            };
            parameters[0].Value = transferID;

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
        public Bizcs.Model.psn_transfer GetModel(int transferID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 transferID,psnID,transferType,preUnitName,preDeptName,prePostName,preOnType,preOnStatus,nextUnitName,nextDeptName,nextPostName,nextOnType,nextOnStatus,transferTime,transferMemo1,transferMemo2,transferMemo3,transferMemo4,transferMemo5,createTime,createUser,transferStatus from psn_transfer ");
            strSql.Append(" where transferID=@transferID");
            SqlParameter[] parameters = {
                    new SqlParameter("@transferID", SqlDbType.Int,4)
            };
            parameters[0].Value = transferID;

            Bizcs.Model.psn_transfer model = new Bizcs.Model.psn_transfer();
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
        public Bizcs.Model.psn_transfer DataRowToModel(DataRow row)
        {
            Bizcs.Model.psn_transfer model = new Bizcs.Model.psn_transfer();
            if (row != null)
            {
                if (row["transferID"] != null && row["transferID"].ToString() != "")
                {
                    model.transferID = int.Parse(row["transferID"].ToString());
                }
                if (row["psnID"] != null && row["psnID"].ToString() != "")
                {
                    model.psnID = int.Parse(row["psnID"].ToString());
                }
                if (row["transferType"] != null)
                {
                    model.transferType = row["transferType"].ToString();
                }
                if (row["preUnitName"] != null)
                {
                    model.preUnitName = row["preUnitName"].ToString();
                }
                if (row["preDeptName"] != null)
                {
                    model.preDeptName = row["preDeptName"].ToString();
                }
                if (row["prePostName"] != null)
                {
                    model.prePostName = row["prePostName"].ToString();
                }
                if (row["preOnType"] != null)
                {
                    model.preOnType = row["preOnType"].ToString();
                }
                if (row["preOnStatus"] != null)
                {
                    model.preOnStatus = row["preOnStatus"].ToString();
                }
                if (row["nextUnitName"] != null)
                {
                    model.nextUnitName = row["nextUnitName"].ToString();
                }
                if (row["nextDeptName"] != null)
                {
                    model.nextDeptName = row["nextDeptName"].ToString();
                }
                if (row["nextPostName"] != null)
                {
                    model.nextPostName = row["nextPostName"].ToString();
                }
                if (row["nextOnType"] != null)
                {
                    model.nextOnType = row["nextOnType"].ToString();
                }
                if (row["nextOnStatus"] != null)
                {
                    model.nextOnStatus = row["nextOnStatus"].ToString();
                }
                if (row["transferTime"] != null && row["transferTime"].ToString() != "")
                {
                    model.transferTime = DateTime.Parse(row["transferTime"].ToString());
                }
                if (row["transferMemo1"] != null)
                {
                    model.transferMemo1 = row["transferMemo1"].ToString();
                }
                if (row["transferMemo2"] != null)
                {
                    model.transferMemo2 = row["transferMemo2"].ToString();
                }
                if (row["transferMemo3"] != null)
                {
                    model.transferMemo3 = row["transferMemo3"].ToString();
                }
                if (row["transferMemo4"] != null)
                {
                    model.transferMemo4 = row["transferMemo4"].ToString();
                }
                if (row["transferMemo5"] != null)
                {
                    model.transferMemo5 = row["transferMemo5"].ToString();
                }
                if (row["createTime"] != null && row["createTime"].ToString() != "")
                {
                    model.createTime = DateTime.Parse(row["createTime"].ToString());
                }
                if (row["createUser"] != null && row["createUser"].ToString() != "")
                {
                    model.createUser = int.Parse(row["createUser"].ToString());
                }
                if (row["transferStatus"] != null && row["transferStatus"].ToString() != "")
                {
                    model.transferStatus = int.Parse(row["transferStatus"].ToString());
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
            strSql.Append("select transferID,psnID,transferType,preUnitName,preDeptName,prePostName,preOnType,preOnStatus,nextUnitName,nextDeptName,nextPostName,nextOnType,nextOnStatus,transferTime,transferMemo1,transferMemo2,transferMemo3,transferMemo4,transferMemo5,createTime,createUser,transferStatus ");
            strSql.Append(" FROM psn_transfer ");
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
                strSql.Append("order by T.transferID desc");
            }
            strSql.Append(")AS Row, T.*  from psn_transfer T ");
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

        #endregion  ExtensionMethod
    }
}
