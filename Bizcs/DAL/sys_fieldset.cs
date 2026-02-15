using DBUtility;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace appsin.Bizcs.DAL
{
    public class sys_fieldset
    {
        public sys_fieldset()
        { }
        #region  BasicMethod


        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(Bizcs.Model.sys_fieldset model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("insert into sys_fieldset(");
            strSql.Append("setPK,setName,setDescription,setType,setCode,setLevel,setMemo1,setMemo2,setMemo3,setMemo4,setMemo5,displayOrder,createTime,createUser,setStatus)");
            strSql.Append(" values (");
            strSql.Append("@setPK,@setName,@setDescription,@setType,@setCode,@setLevel,@setMemo1,@setMemo2,@setMemo3,@setMemo4,@setMemo5,@displayOrder,@createTime,@createUser,@setStatus)");
            strSql.Append(";select @@IDENTITY");
            SqlParameter[] parameters = {
                    new SqlParameter("@setPK", SqlDbType.VarChar,100),
                    new SqlParameter("@setName", SqlDbType.VarChar,100),
                    new SqlParameter("@setDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@setType", SqlDbType.VarChar,100),
                    new SqlParameter("@setCode", SqlDbType.VarChar,100),
                    new SqlParameter("@setLevel", SqlDbType.Int,4),
                    new SqlParameter("@setMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@setStatus", SqlDbType.Int,4)};
            parameters[0].Value = model.setPK;
            parameters[1].Value = model.setName;
            parameters[2].Value = model.setDescription;
            parameters[3].Value = model.setType;
            parameters[4].Value = model.setCode;
            parameters[5].Value = model.setLevel;
            parameters[6].Value = model.setMemo1;
            parameters[7].Value = model.setMemo2;
            parameters[8].Value = model.setMemo3;
            parameters[9].Value = model.setMemo4;
            parameters[10].Value = model.setMemo5;
            parameters[11].Value = model.displayOrder;
            parameters[12].Value = model.createTime;
            parameters[13].Value = model.createUser;
            parameters[14].Value = model.setStatus;

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
        public bool Update(Bizcs.Model.sys_fieldset model)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("update sys_fieldset set ");
            strSql.Append("setPK=@setPK,");
            strSql.Append("setName=@setName,");
            strSql.Append("setDescription=@setDescription,");
            strSql.Append("setType=@setType,");
            strSql.Append("setCode=@setCode,");
            strSql.Append("setLevel=@setLevel,");
            strSql.Append("setMemo1=@setMemo1,");
            strSql.Append("setMemo2=@setMemo2,");
            strSql.Append("setMemo3=@setMemo3,");
            strSql.Append("setMemo4=@setMemo4,");
            strSql.Append("setMemo5=@setMemo5,");
            strSql.Append("displayOrder=@displayOrder,");
            strSql.Append("createTime=@createTime,");
            strSql.Append("createUser=@createUser,");
            strSql.Append("setStatus=@setStatus");
            strSql.Append(" where setID=@setID");
            SqlParameter[] parameters = {
                    new SqlParameter("@setPK", SqlDbType.VarChar,100),
                    new SqlParameter("@setName", SqlDbType.VarChar,100),
                    new SqlParameter("@setDescription", SqlDbType.VarChar,100),
                    new SqlParameter("@setType", SqlDbType.VarChar,100),
                    new SqlParameter("@setCode", SqlDbType.VarChar,100),
                    new SqlParameter("@setLevel", SqlDbType.Int,4),
                    new SqlParameter("@setMemo1", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo2", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo3", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo4", SqlDbType.VarChar,100),
                    new SqlParameter("@setMemo5", SqlDbType.VarChar,100),
                    new SqlParameter("@displayOrder", SqlDbType.Int,4),
                    new SqlParameter("@createTime", SqlDbType.DateTime),
                    new SqlParameter("@createUser", SqlDbType.Int,4),
                    new SqlParameter("@setStatus", SqlDbType.Int,4),
                    new SqlParameter("@setID", SqlDbType.Int,4)};
            parameters[0].Value = model.setPK;
            parameters[1].Value = model.setName;
            parameters[2].Value = model.setDescription;
            parameters[3].Value = model.setType;
            parameters[4].Value = model.setCode;
            parameters[5].Value = model.setLevel;
            parameters[6].Value = model.setMemo1;
            parameters[7].Value = model.setMemo2;
            parameters[8].Value = model.setMemo3;
            parameters[9].Value = model.setMemo4;
            parameters[10].Value = model.setMemo5;
            parameters[11].Value = model.displayOrder;
            parameters[12].Value = model.createTime;
            parameters[13].Value = model.createUser;
            parameters[14].Value = model.setStatus;
            parameters[15].Value = model.setID;

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
        public bool Delete(int setID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("delete from sys_fieldset ");
            strSql.Append(" where setID=@setID");
            SqlParameter[] parameters = {
                    new SqlParameter("@setID", SqlDbType.Int,4)
            };
            parameters[0].Value = setID;

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
        public Bizcs.Model.sys_fieldset GetModel(int setID)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 setID,setPK,setName,setDescription,setType,setCode,setLevel,setMemo1,setMemo2,setMemo3,setMemo4,setMemo5,displayOrder,createTime,createUser,setStatus from sys_fieldset ");
            strSql.Append(" where setID=@setID");
            SqlParameter[] parameters = {
                    new SqlParameter("@setID", SqlDbType.Int,4)
            };
            parameters[0].Value = setID;

            Bizcs.Model.sys_fieldset model = new Bizcs.Model.sys_fieldset();
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
        public Bizcs.Model.sys_fieldset DataRowToModel(DataRow row)
        {
            Bizcs.Model.sys_fieldset model = new Bizcs.Model.sys_fieldset();
            if (row != null)
            {
                if (row["setID"] != null && row["setID"].ToString() != "")
                {
                    model.setID = int.Parse(row["setID"].ToString());
                }
                if (row["setPK"] != null)
                {
                    model.setPK = row["setPK"].ToString();
                }
                if (row["setName"] != null)
                {
                    model.setName = row["setName"].ToString();
                }
                if (row["setDescription"] != null)
                {
                    model.setDescription = row["setDescription"].ToString();
                }
                if (row["setType"] != null)
                {
                    model.setType = row["setType"].ToString();
                }
                if (row["setCode"] != null)
                {
                    model.setCode = row["setCode"].ToString();
                }
                if (row["setLevel"] != null && row["setLevel"].ToString() != "")
                {
                    model.setLevel = int.Parse(row["setLevel"].ToString());
                }
                if (row["setMemo1"] != null)
                {
                    model.setMemo1 = row["setMemo1"].ToString();
                }
                if (row["setMemo2"] != null)
                {
                    model.setMemo2 = row["setMemo2"].ToString();
                }
                if (row["setMemo3"] != null)
                {
                    model.setMemo3 = row["setMemo3"].ToString();
                }
                if (row["setMemo4"] != null)
                {
                    model.setMemo4 = row["setMemo4"].ToString();
                }
                if (row["setMemo5"] != null)
                {
                    model.setMemo5 = row["setMemo5"].ToString();
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
                if (row["setStatus"] != null && row["setStatus"].ToString() != "")
                {
                    model.setStatus = int.Parse(row["setStatus"].ToString());
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
            strSql.Append("select setID,setPK,setName,setDescription,setType,setCode,setLevel,setMemo1,setMemo2,setMemo3,setMemo4,setMemo5,displayOrder,createTime,createUser,setStatus ");
            strSql.Append(" FROM sys_fieldset ");
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
                strSql.Append("order by T.setID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_fieldset T ");
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

        public DataSet GetSimpleList()
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select setPK,setName,setDescription,setType,setCode,setLevel,setMemo1,setMemo2,setMemo3,setMemo4,setMemo5 ");
            strSql.Append(" FROM sys_fieldset where setStatus=1");
            return DbHelperSQL.Query(strSql.ToString());
        }

        public DataSet GetSimpleListByPage(string strWhere, string orderby, int startIndex, int endIndex, params SqlParameter[] parms)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select setID,setPK,setName,setDescription,setType,setCode,setLevel,displayOrder,createTime,(select psnName from psn_psnMain where psnID=createUser) as createUser,setStatus ");
            strSql.Append(" FROM ( ");
            strSql.Append(" SELECT ROW_NUMBER() OVER (");
            if (!string.IsNullOrEmpty(orderby.Trim()))
            {
                strSql.Append("order by T." + orderby);
            }
            else
            {
                strSql.Append("order by T.setID desc");
            }
            strSql.Append(")AS Row, T.*  from sys_fieldset T ");
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
