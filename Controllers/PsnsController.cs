using appsin.Common;
using appsin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Primitives;
using System.Data;
using System.Text;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class PsnsController : ControllerBase
    {
        [HttpPost(Name = "transEdit")]
        public ResponseSet<string> transEdit([FromBody] RequestSet<iTransEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                    Bizcs.Model.psn_psnMain psnModel = psnBll.GetModel(int.Parse(reqModel.reqData[0].psnID));

                    Bizcs.BLL.psn_transfer transBll = new Bizcs.BLL.psn_transfer();
                    Bizcs.Model.psn_transfer transModel = new Bizcs.Model.psn_transfer();

                    Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                    Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();

                    switch (reqModel.reqData[0].transType)
                    {
                        case "1":
                            psnModel.unitID = int.Parse(reqModel.reqData[0].unitID);
                            psnModel.deptID = int.Parse(reqModel.reqData[0].deptID);
                            psnModel.postID = int.Parse(reqModel.reqData[0].postID);
                            bool iss = psnBll.Update(psnModel);
                            if (iss)
                            {
                                transModel.psnID = int.Parse(reqModel.reqData[0].psnID);
                                transModel.transferType = "1";
                                transModel.transferTime = reqModel.reqData[0].transDate;
                                transModel.preUnitName = orgBll.GetModel(Convert.ToInt32(psnModel.unitID)).orgName;
                                transModel.nextUnitName = orgBll.GetModel(int.Parse(reqModel.reqData[0].unitID)).orgName;
                                transModel.preDeptName = orgBll.GetModel(Convert.ToInt32(psnModel.deptID)).orgName;
                                transModel.nextDeptName = orgBll.GetModel(int.Parse(reqModel.reqData[0].deptID)).orgName;
                                transModel.prePostName = psnModel.postID == 0 ? "" : orgBll.GetModel(Convert.ToInt32(psnModel.postID)).orgName;
                                transModel.nextPostName = reqModel.reqData[0].postID == "0" ? "" : orgBll.GetModel(int.Parse(reqModel.reqData[0].postID)).orgName;
                                transModel.preOnType = transModel.nextOnType = itemBll.GetModel(Convert.ToInt32(psnModel.onType)).itemName;
                                transModel.preOnStatus = transModel.nextOnStatus = itemBll.GetModel(Convert.ToInt32(psnModel.onStatus)).itemName;
                                transModel.createTime = DateTime.Now;
                                transModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                                transModel.transferStatus = 1;
                                int transID = transBll.Add(transModel);
                                if (transID > 0)
                                {
                                    res.status = 1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "ok!";
                                    res.resData = null;
                                }
                                else
                                {
                                    res.status = -11;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "Transfer already exec,but log write failed!";
                                    res.resData = null;
                                }
                            }
                            else
                            {
                                res.status = -12;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "Transfer failed,please retry or contact administrator";
                                res.resData = null;
                            }
                            break;
                        case "2":
                            psnModel.onType = int.Parse(reqModel.reqData[0].onType);
                            iss = psnBll.Update(psnModel);
                            if (iss)
                            {
                                transModel.psnID = int.Parse(reqModel.reqData[0].psnID);
                                transModel.transferType = "2";
                                transModel.transferTime = reqModel.reqData[0].transDate;
                                transModel.transferTime = reqModel.reqData[0].transDate;
                                transModel.preUnitName = transModel.nextUnitName = orgBll.GetModel(Convert.ToInt32(psnModel.unitID)).orgName;
                                transModel.preDeptName = transModel.nextDeptName = orgBll.GetModel(Convert.ToInt32(psnModel.deptID)).orgName;
                                transModel.prePostName = transModel.nextPostName = psnModel.postID == 0 ? "" : orgBll.GetModel(Convert.ToInt32(psnModel.postID)).orgName;
                                transModel.preOnType = itemBll.GetModel(Convert.ToInt32(psnModel.onType)).itemName;
                                transModel.nextOnType = itemBll.GetModel(Convert.ToInt32(reqModel.reqData[0].onType)).itemName;
                                transModel.preOnStatus = transModel.nextOnStatus = itemBll.GetModel(Convert.ToInt32(psnModel.onStatus)).itemName;
                                transModel.createTime = DateTime.Now;
                                transModel.transferStatus = 1;
                                transModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                                int transID = transBll.Add(transModel);
                                if (transID > 0)
                                {
                                    res.status = 1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "ok!";
                                    res.resData = null;
                                }
                                else
                                {
                                    res.status = -11;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "Transfer already exec,but log write failed!";
                                    res.resData = null;
                                }
                            }
                            else
                            {
                                res.status = -12;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "Transfer failed,please retry or contact administrator";
                                res.resData = null;
                            }
                            break;
                        case "3":
                            psnModel.onStatus = int.Parse(reqModel.reqData[0].onStatus);
                            iss = psnBll.Update(psnModel);
                            if (iss)
                            {
                                transModel.psnID = int.Parse(reqModel.reqData[0].psnID);
                                transModel.transferType = "3";
                                transModel.transferTime = reqModel.reqData[0].transDate;
                                transModel.preUnitName = transModel.nextUnitName = orgBll.GetModel(Convert.ToInt32(psnModel.unitID)).orgName;
                                transModel.preDeptName = transModel.nextDeptName = orgBll.GetModel(Convert.ToInt32(psnModel.deptID)).orgName;
                                transModel.prePostName = transModel.nextPostName = psnModel.postID == 0 ? "" : orgBll.GetModel(Convert.ToInt32(psnModel.postID)).orgName;
                                transModel.preOnType = transModel.nextOnType = itemBll.GetModel(Convert.ToInt32(psnModel.onType)).itemName;
                                transModel.preOnStatus = itemBll.GetModel(Convert.ToInt32(psnModel.onStatus)).itemName;
                                transModel.nextOnStatus = itemBll.GetModel(Convert.ToInt32(reqModel.reqData[0].onStatus)).itemName;
                                transModel.createTime = DateTime.Now;
                                transModel.transferStatus = 1;
                                transModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                                int transID = transBll.Add(transModel);
                                if (transID > 0)
                                {
                                    res.status = 1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "ok!";
                                    res.resData = null;
                                }
                                else
                                {
                                    res.status = -11;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "Transfer already exec,but log write failed!";
                                    res.resData = null;
                                }
                            }
                            else
                            {
                                res.status = -12;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "Transfer failed,please retry or contact administrator";
                                res.resData = null;
                            }
                            break;
                    }

                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query failed,no valid arguments!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;

        }

        [HttpPost(Name = "psnImport")]
        public ResponseSet<string> psnImport([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken == null || reqModel.uToken == "" || psnID <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count == 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Query failed,no valid arguments!";
                res.resData = null;
            }
            else
            {
                string fileName = reqModel.reqData[0];
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files\\UpLoad\\", fileName);
                if (System.IO.File.Exists(fullPath))
                {
                    DataSet ds = ExcelHelper.ReadExcelToDs(fullPath);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        string[] cols = listHelper.GetColsByDt(ds.Tables[0]);
                        if (cols.Length > 0 && cols.Contains("Name") && cols.Contains("Nickname") && cols.Contains("Organization")
                            && cols.Contains("Department") && cols.Contains("Postion") && cols.Contains("Code") && cols.Contains("ID Type")
                            && cols.Contains("ID No#") && cols.Contains("Sex") && cols.Contains("Nationality") && cols.Contains("Type")
                            && cols.Contains("Username") && cols.Contains("Password") && cols.Contains("Cellphone") && cols.Contains("Email")
                            && cols.Contains("Birthday") && cols.Contains("Start Work Date") && cols.Contains("Hire Date")
                            && cols.Contains("Memo1") && cols.Contains("Memo2") && cols.Contains("Memo3") && cols.Contains("Memo4") && cols.Contains("Memo5"))
                        {
                            string codeList = "";
                            string IDNoList = "";
                            string userList = "";
                            string noOrgList = "";
                            string errPwdList = "";
                            int mustNull = 0;
                            int riskCount = 0;
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (!VerifyHelper.isSafe(dr["Name"].ToString()) || !VerifyHelper.isSafe(dr["Nickname"].ToString()) || !VerifyHelper.isSafe(dr["Organization"].ToString())
                                    || !VerifyHelper.isSafe(dr["Department"].ToString()) || !VerifyHelper.isSafe(dr["Postion"].ToString()) || !VerifyHelper.isSafe(dr["Code"].ToString())
                                    || !VerifyHelper.isSafe(dr["ID Type"].ToString()) || !VerifyHelper.isSafe(dr["ID No#"].ToString()) || !VerifyHelper.isSafe(dr["Sex"].ToString())
                                    || !VerifyHelper.isSafe(dr["Nationality"].ToString()) || !VerifyHelper.isSafe(dr["Type"].ToString()) || !VerifyHelper.isSafe(dr["Username"].ToString())
                                    || !VerifyHelper.isSafe(dr["Password"].ToString()) || !VerifyHelper.isSafe(dr["Cellphone"].ToString()) || !VerifyHelper.isSafe(dr["Email"].ToString())
                                    || !VerifyHelper.isSafe(dr["Memo1"].ToString()) || !VerifyHelper.isSafe(dr["Memo2"].ToString()) || !VerifyHelper.isSafe(dr["Memo3"].ToString())
                                    || !VerifyHelper.isSafe(dr["Memo4"].ToString()) || !VerifyHelper.isSafe(dr["Memo5"].ToString()))
                                {
                                    riskCount++;
                                }
                                if (string.IsNullOrEmpty(dr["Organization"].ToString()) || string.IsNullOrEmpty(dr["Name"].ToString()) || string.IsNullOrEmpty(dr["Username"].ToString()))
                                {
                                    mustNull++;
                                }
                                if (new Bizcs.BLL.org_orgMain().GetOrgIDByStr(dr["Organization"].ToString(), "unit") <= 0)
                                {
                                    noOrgList += dr["ID No#"].ToString() + ",";
                                }
                                if (!VerifyHelper.checkPwd(dr["Password"].ToString()))
                                {
                                    errPwdList += dr["ID No#"].ToString() + ",";
                                }
                                codeList += "'" + dr["Code"].ToString() + "',";
                                IDNoList += "'" + dr["ID No#"].ToString() + "',";
                                userList += "'" + dr["Username"].ToString() + "',";
                            }
                            if (mustNull > 0)
                            {
                                res.status = -1;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "There are someone's organization or name or username are null, please fill in all necessary information!";
                                res.resData = null;
                            }
                            else if (errPwdList.Length > 0)
                            {
                                res.status = -1;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "There are someone's password cannot match the complexity requirement, the ID list as follows:<br />" + errPwdList.Substring(0, noOrgList.Length - 1);
                                res.resData = null;
                            }
                            else if (noOrgList.Length > 0)
                            {
                                res.status = -1;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "There are someone's organization string cannot match the organization in system, the ID list as follows:<br />" + noOrgList.Substring(0, noOrgList.Length - 1);
                                res.resData = null;
                            }
                            else if (riskCount > 0)
                            {
                                res.status = -1;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "High Risk Characters In Your Excel Data";
                                res.resData = null;
                            }
                            else
                            {
                                DataSet dsMulCode = new Bizcs.BLL.psn_psnMain().GetList(" psnCode in (" + codeList.Substring(0, codeList.Length - 1) + ")");
                                DataSet dsMulIDNo = new Bizcs.BLL.psn_psnMain().GetList(" IDNo in (" + IDNoList.Substring(0, IDNoList.Length - 1) + ")");
                                DataSet dsMulUser = new Bizcs.BLL.psn_psnMain().GetList(" psnUserName in (" + userList.Substring(0, userList.Length - 1) + ")");

                                if (dsMulCode.Tables[0] != null && dsMulCode.Tables[0].Rows.Count > 0)
                                {
                                    string mulCodeList = "";
                                    foreach (DataRow dr in dsMulCode.Tables[0].Rows)
                                    {
                                        mulCodeList += dr["code"] + ",";
                                    }
                                    res.status = -1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "There are repeatitive code, the ID as follows:<br />" + mulCodeList.Substring(0, mulCodeList.Length - 1);
                                    res.resData = null;
                                }
                                else if (dsMulIDNo.Tables[0] != null && dsMulIDNo.Tables[0].Rows.Count > 0)
                                {
                                    string mulIDNoList = "";
                                    foreach (DataRow dr in dsMulIDNo.Tables[0].Rows)
                                    {
                                        mulIDNoList += dr["IDNo"] + ",";
                                    }
                                    res.status = -1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "There are repetitive ID No., the ID  as follows:<br />" + mulIDNoList.Substring(0, mulIDNoList.Length - 1);
                                    res.resData = null;
                                }
                                else if (dsMulUser.Tables[0] != null && dsMulUser.Tables[0].Rows.Count > 0)
                                {
                                    string mulIDNoList = "";
                                    foreach (DataRow dr in dsMulUser.Tables[0].Rows)
                                    {
                                        mulIDNoList += dr["IDNo"] + ",";
                                    }
                                    res.status = -1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "There are repetitive username, the ID list as follows:<br />" + mulIDNoList.Substring(0, mulIDNoList.Length - 1);
                                    res.resData = null;
                                }
                                else
                                {
                                    Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                                    Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
                                    Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                                    foreach (DataRow dr in ds.Tables[0].Rows)
                                    {
                                        Bizcs.Model.psn_psnMain psnModel = new Bizcs.Model.psn_psnMain(); ;
                                        psnModel.psnPK = Guid.NewGuid().ToString().ToUpper();
                                        psnModel.psnName = dr["Name"].ToString();
                                        psnModel.aliaName = dr["Nickname"].ToString();
                                        psnModel.IDNo = dr["ID No#"].ToString();
                                        psnModel.psnSex = dr["Sex"].ToString();
                                        psnModel.psnNational = dr["Nationality"].ToString();
                                        psnModel.psnUserName = dr["Username"].ToString();
                                        psnModel.psnPassword = CryptAES.Md5Encrypt(dr["Password"].ToString());
                                        psnModel.psnCellPhone = dr["Cellphone"].ToString();
                                        psnModel.psnBirthday = DateTime.TryParse(dr["Birthday"].ToString(), out var bt) ? DateTime.Parse(dr["Birthday"].ToString()) : null;
                                        psnModel.psnJoinday = DateTime.TryParse(dr["Hire Date"].ToString(), out var jnt) ? DateTime.Parse(dr["Hire Date"].ToString()) : null;
                                        psnModel.psnJobday = DateTime.TryParse(dr["Start Work Date"].ToString(), out var jbt) ? DateTime.Parse(dr["Start Work Date"].ToString()) : null;
                                        psnModel.loginStatus = 1;
                                        psnModel.onStatus = 10000;
                                        psnModel.psnStatus = 1;
                                        psnModel.psnEmail = dr["Email"].ToString();
                                        psnModel.psnMemo1 = dr["memo1"].ToString();
                                        psnModel.psnMemo2 = dr["memo2"].ToString();
                                        psnModel.psnMemo3 = dr["memo3"].ToString();
                                        psnModel.psnMemo4 = dr["memo4"].ToString();
                                        psnModel.psnMemo5 = dr["memo5"].ToString();
                                        psnModel.unitID = orgBll.GetOrgIDByStr(dr["Organization"].ToString(), "unit");
                                        psnModel.deptID = orgBll.GetOrgIDByStr(dr["Department"].ToString(), "dept");
                                        psnModel.postID = orgBll.GetOrgIDByStr(dr["Postion"].ToString(), "post");
                                        psnModel.IDType = itemBll.GetItemIDByStr(dr["ID Type"].ToString()).ToString();
                                        psnModel.onType = itemBll.GetTypeIDByStr(dr["Type"].ToString());
                                        psnBll.Add(psnModel);
                                    }

                                    res.status = 1;
                                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                    res.message = "Person Import success!";
                                    res.resData = null;
                                }
                            }

                        }
                        else
                        {
                            res.status = 0;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "The columes of excel does not meet the requirement!";
                            res.resData = null;
                        }

                    }
                    else
                    {
                        res.status = 0;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Query failed,no valid arguments!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query failed,no valid arguments!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "psnDel")]
        public ResponseSet<string> psnDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Query failed,no valid arguments!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.psn_transfer transBll = new Bizcs.BLL.psn_transfer();
                Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
                Bizcs.BLL.sys_dataOsrz dataBll = new Bizcs.BLL.sys_dataOsrz();
                Bizcs.BLL.sys_menuOsrz roleBll = new Bizcs.BLL.sys_menuOsrz();

                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@psnID", reqModel.reqData[0]));

                DataSet dsTrans = transBll.GetList(" psnID=@psnID", parms.ToArray());
                DataSet dsToken = tokenBll.GetList(" psnID=@psnID", parms.ToArray());
                DataSet dsData = dataBll.GetList(" osrzObjType='psn' and osrzObjID=@psnID", parms.ToArray());
                DataSet dsRole = roleBll.GetList(" osrzObjType='psn' and osrzObjID=@psnID", parms.ToArray());
                string msg = "";
                if (dsTrans.Tables[0] != null && dsTrans.Tables[0].Rows.Count > 0) { msg += "There is some transfer record already exist;"; }
                if (dsToken.Tables[0] != null && dsToken.Tables[0].Rows.Count > 0) { msg += "There is some login record already exist;"; }
                if (dsData.Tables[0] != null && dsData.Tables[0].Rows.Count > 0) { msg += "There is some data authorize record already exist;"; }
                if (dsRole.Tables[0] != null && dsRole.Tables[0].Rows.Count > 0) { msg += "There is some function authorize record already exist;"; }
                if (msg.Length > 0)
                {
                    msg += "can't be deleted!";
                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = msg;
                    res.resData = null;
                }
                else
                {
                    Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                    bool iss = psnBll.Delete(int.Parse(reqModel.reqData[0]));
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = msg;
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
                        res.resData = null;
                    }
                }

            }
            return res;
        }

        [HttpPost(Name = "psnUpdate")]
        public ResponseSet<string> psnUpdate([FromBody] RequestSet<iPsnEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "No valid arguments!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].psnUsername) || !VerifyHelper.isSafe(reqModel.reqData[0].psnName) || !VerifyHelper.isSafe(reqModel.reqData[0].aliaName) || !VerifyHelper.isSafe(reqModel.reqData[0].psnPicture)
                || !VerifyHelper.isSafe(reqModel.reqData[0].onType) || !VerifyHelper.isSafe(reqModel.reqData[0].psnCode) || !VerifyHelper.isSafe(reqModel.reqData[0].idType) || !VerifyHelper.isSafe(reqModel.reqData[0].idNo)
                || !VerifyHelper.isSafe(reqModel.reqData[0].psnCellphone) || !VerifyHelper.isSafe(reqModel.reqData[0].psnEmail) || !VerifyHelper.isSafe(reqModel.reqData[0].psnIM) || !VerifyHelper.isSafe(reqModel.reqData[0].psnNational)
                || !VerifyHelper.isSafe(reqModel.reqData[0].unitID) || !VerifyHelper.isSafe(reqModel.reqData[0].deptID) || !VerifyHelper.isSafe(reqModel.reqData[0].postID) || !VerifyHelper.isSafe(reqModel.reqData[0].psnID)
                || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo1) || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo2) || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo3) || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo4)
                || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo5) || !VerifyHelper.isSafe(reqModel.reqData[0].loginStatus)
                )
            {
                res.status = -1;
                res.uToken = "";
                res.message = "Arguments contain high risk characters!";
                res.resData = null;
            }
            else
            {

                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();

                string exist = "";
                if (!string.IsNullOrEmpty(reqModel.reqData[0].psnUsername))
                {
                    strWhere.Clear(); parms.Clear();
                    strWhere.Append(" psnUserName=@psnUserName and psnID<>@psnID");
                    parms.Add(new SqlParameter("@psnUserName", reqModel.reqData[0].psnUsername));
                    parms.Add(new SqlParameter("@psnID", reqModel.reqData[0].psnID));
                    DataSet ds = psnBll.GetList(strWhere.ToString(), parms.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    { exist = " Username "; }
                }

                if (!string.IsNullOrEmpty(reqModel.reqData[0].idNo))
                {
                    strWhere.Clear(); parms.Clear();
                    strWhere.Append(" idNo=@idNo  and psnID<>@psnID");
                    parms.Add(new SqlParameter("@idNo", reqModel.reqData[0].idNo));
                    parms.Add(new SqlParameter("@psnID", reqModel.reqData[0].psnID));
                    DataSet ds = psnBll.GetList(strWhere.ToString(), parms.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    { exist = " ID Number "; }
                }
                if (!string.IsNullOrEmpty(reqModel.reqData[0].psnCode))
                {
                    strWhere.Clear(); parms.Clear();
                    strWhere.Append(" psnCode=@psnCode  and psnID<>@psnID");
                    parms.Add(new SqlParameter("@psnCode", reqModel.reqData[0].psnCode));
                    parms.Add(new SqlParameter("@psnID", reqModel.reqData[0].psnID));
                    DataSet ds = psnBll.GetList(strWhere.ToString(), parms.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    { exist = " Code "; }
                }
                if (exist != "")
                {
                    res.status = -2;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "The " + exist + " is alreadey exist,plese check again!";
                    res.resData = null;
                }
                else
                {
                    iPsnEdit psnEdit = reqModel.reqData[0];
                    Bizcs.Model.psn_psnMain psnModel = psnBll.GetModel(int.Parse(psnEdit.psnID));

                    psnModel.psnName = psnEdit.psnName;
                    psnModel.aliaName = psnEdit.aliaName;
                    psnModel.psnCode = psnEdit.psnCode;
                    psnModel.IDType = psnEdit.idType;
                    psnModel.IDNo = psnEdit.idNo;
                    psnModel.psnSex = psnEdit.psnSex;
                    psnModel.loginStatus = int.Parse(psnEdit.loginStatus);
                    psnModel.psnPicture = psnEdit.psnPicture;
                    psnModel.psnEmail = psnEdit.psnEmail;
                    psnModel.psnCellPhone = psnEdit.psnCellphone;
                    psnModel.psnIM = psnEdit.psnIM;
                    psnModel.psnBirthday = DateTime.TryParse(psnEdit.psnBirthday, out var bt) ? DateTime.Parse(psnEdit.psnBirthday) : null;
                    psnModel.psnJoinday = DateTime.TryParse(psnEdit.psnJoinday, out var jnt) ? DateTime.Parse(psnEdit.psnJoinday) : null;
                    psnModel.psnJobday = DateTime.TryParse(psnEdit.psnJobday, out var jbt) ? DateTime.Parse(psnEdit.psnJobday) : null;
                    psnModel.psnNational = psnEdit.psnNational;
                    psnModel.psnUserName = psnEdit.psnUsername;
                    psnModel.psnPassword = CryptAES.Md5Encrypt(psnEdit.psnPassword);
                    psnModel.psnMemo1 = psnEdit.psnMemo1;
                    psnModel.psnMemo2 = psnEdit.psnMemo2;
                    psnModel.psnMemo3 = psnEdit.psnMemo3;
                    psnModel.psnMemo4 = psnEdit.psnMemo4;
                    psnModel.psnMemo5 = psnEdit.psnMemo5;

                    bool iss = psnBll.Update(psnModel);
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Update person information success!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -4;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Update peron information failed,please retry or contact administrator!";
                        res.resData = null;
                    }
                }

            }
            return res;
        }
        [HttpPost(Name = "psnAdd")]
        public ResponseSet<string> psnAdd([FromBody] RequestSet<iPsnEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "No valid arguments!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].psnUsername) || !VerifyHelper.isSafe(reqModel.reqData[0].psnName) || !VerifyHelper.isSafe(reqModel.reqData[0].aliaName) || !VerifyHelper.isSafe(reqModel.reqData[0].psnPicture)
                || !VerifyHelper.isSafe(reqModel.reqData[0].onType) || !VerifyHelper.isSafe(reqModel.reqData[0].psnCode) || !VerifyHelper.isSafe(reqModel.reqData[0].idType) || !VerifyHelper.isSafe(reqModel.reqData[0].idNo)
                || !VerifyHelper.isSafe(reqModel.reqData[0].psnCellphone) || !VerifyHelper.isSafe(reqModel.reqData[0].psnEmail) || !VerifyHelper.isSafe(reqModel.reqData[0].psnIM) || !VerifyHelper.isSafe(reqModel.reqData[0].psnNational)
                || !VerifyHelper.isSafe(reqModel.reqData[0].unitID) || !VerifyHelper.isSafe(reqModel.reqData[0].deptID) || !VerifyHelper.isSafe(reqModel.reqData[0].postID) || !VerifyHelper.isSafe(reqModel.reqData[0].psnID)
                || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo1) || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo2) || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo3) || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo4)
                || !VerifyHelper.isSafe(reqModel.reqData[0].psnMemo5) || !VerifyHelper.isSafe(reqModel.reqData[0].loginStatus)
                )
            {
                res.status = -1;
                res.uToken = "";
                res.message = "Arguments contain high risk characters!";
                res.resData = null;
            }
            else
            {

                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();

                string exist = "";
                if (!string.IsNullOrEmpty(reqModel.reqData[0].psnUsername))
                {
                    strWhere.Clear(); parms.Clear();
                    strWhere.Append(" psnUserName=@psnUserName ");
                    parms.Add(new SqlParameter("@psnUserName", reqModel.reqData[0].psnUsername));
                    DataSet ds = psnBll.GetList(strWhere.ToString(), parms.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    { exist = " Username "; }
                }

                if (!string.IsNullOrEmpty(reqModel.reqData[0].idNo))
                {
                    strWhere.Clear(); parms.Clear();
                    strWhere.Append(" idNo=@idNo ");
                    parms.Add(new SqlParameter("@idNo", reqModel.reqData[0].idNo));
                    DataSet ds = psnBll.GetList(strWhere.ToString(), parms.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    { exist = " ID Number "; }
                }
                if (!string.IsNullOrEmpty(reqModel.reqData[0].psnCode))
                {
                    strWhere.Append(" or psnCode=@psnCode ");
                    parms.Add(new SqlParameter("@psnCode", reqModel.reqData[0].psnCode));
                    DataSet ds = psnBll.GetList(strWhere.ToString(), parms.ToArray());
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    { exist = " Code "; }
                }
                if (exist != "")
                {
                    res.status = -2;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "The " + exist + " is alreadey exist,plese check again!";
                    res.resData = null;
                }
                else
                {
                    Bizcs.Model.psn_psnMain psnModel = new Bizcs.Model.psn_psnMain();
                    iPsnEdit psnEdit = reqModel.reqData[0];
                    psnModel.psnPK = Guid.NewGuid().ToString().ToUpper();
                    psnModel.psnName = psnEdit.psnName;
                    psnModel.aliaName = psnEdit.aliaName;
                    psnModel.unitID = int.Parse(psnEdit.unitID);
                    psnModel.deptID = int.Parse(psnEdit.deptID);
                    psnModel.postID = int.Parse(psnEdit.postID);
                    psnModel.psnCode = psnEdit.psnCode;
                    psnModel.IDType = psnEdit.idType;
                    psnModel.IDNo = psnEdit.idNo;
                    psnModel.psnSex = psnEdit.psnSex;
                    psnModel.onStatus = 10000;
                    psnModel.onType = int.Parse(psnEdit.onType);
                    psnModel.loginStatus = int.Parse(psnEdit.loginStatus);
                    psnModel.psnPicture = psnEdit.psnPicture;
                    psnModel.psnEmail = psnEdit.psnEmail;
                    psnModel.psnCellPhone = psnEdit.psnCellphone;
                    psnModel.psnIM = psnEdit.psnIM;
                    psnModel.psnBirthday = DateTime.TryParse(psnEdit.psnBirthday, out var bt) ? DateTime.Parse(psnEdit.psnBirthday) : null;
                    psnModel.psnJoinday = DateTime.TryParse(psnEdit.psnJoinday, out var jnt) ? DateTime.Parse(psnEdit.psnJoinday) : null;
                    psnModel.psnJobday = DateTime.TryParse(psnEdit.psnJobday, out var jbt) ? DateTime.Parse(psnEdit.psnJobday) : null;
                    psnModel.psnNational = psnEdit.psnNational;
                    psnModel.psnUserName = psnEdit.psnUsername;
                    psnModel.psnPassword = CryptAES.Md5Encrypt(psnEdit.psnPassword);
                    psnModel.psnMemo1 = psnEdit.psnMemo1;
                    psnModel.psnMemo2 = psnEdit.psnMemo2;
                    psnModel.psnMemo3 = psnEdit.psnMemo3;
                    psnModel.psnMemo4 = psnEdit.psnMemo4;
                    psnModel.psnMemo5 = psnEdit.psnMemo5;
                    psnModel.createTime = DateTime.Now;
                    psnModel.psnStatus = 1;
                    int psnID = psnBll.Add(psnModel);
                    if (psnID > 0)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Add person success!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -4;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Add peron failed,please retry or contact administrator!";
                        res.resData = null;
                    }
                }
            }
            return res;

        }

        [HttpPost(Name = "getField")]
        public ResponseSet<iFieldItem> getField([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iFieldItem> res = new ResponseSet<iFieldItem>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData[0] != "")
                {
                    string fieldsetID = "";
                    switch (reqModel.reqData[0])
                    {
                        case "status":
                            fieldsetID = "10000";
                            break;
                        case "type":
                            fieldsetID = "10001";
                            break;
                        case "id":
                            fieldsetID = "10002";
                            break;
                    }

                    Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                    List<SqlParameter> parms = new List<SqlParameter>()
                    {
                        new SqlParameter("@setID",fieldsetID)
                    };
                    DataSet dsItem = itemBll.GetSimpleList(" itemStatus=1 and setID=@setID order by itemLevel,displayOrder", parms.ToArray());
                    if (dsItem.Tables[0] != null && dsItem.Tables[0].Rows.Count > 0)
                    {
                        List<iFieldItem> itemList = listHelper.ConvertDtToList<iFieldItem>(dsItem.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Query Success!";
                        res.resData = itemList;
                    }
                    else
                    {
                        res.status = 0;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Query Success,but no data!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query failed,no valid arguments!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getPsnDetail")]
        public ResponseSet<iPsnInfo> getPsnDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iPsnInfo> res = new ResponseSet<iPsnInfo>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();

                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@psnID", reqModel.reqData[0]));

                DataSet dsPsn = psnBll.GetSimpleList(" psnID=@psnID ", parms.ToArray());
                if (dsPsn.Tables[0] != null && dsPsn.Tables[0].Rows.Count > 0)
                {
                    List<iPsnInfo> psnList = listHelper.ConvertDtToList<iPsnInfo>(dsPsn.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = psnList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getMyDetail")]
        public ResponseSet<iPsnInfo> getMyDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iPsnInfo> res = new ResponseSet<iPsnInfo>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken != null && psnID > 0)
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();

                List<SqlParameter> parms = new List<SqlParameter>();
                parms.Add(new SqlParameter("@psnID", psnID));

                DataSet dsPsn = psnBll.GetSimpleList(" psnID=@psnID ", parms.ToArray());
                if (dsPsn.Tables[0] != null && dsPsn.Tables[0].Rows.Count > 0)
                {
                    List<iPsnInfo> psnList = listHelper.ConvertDtToList<iPsnInfo>(dsPsn.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = psnList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getPsnList")]
        public ResponseSet<iPsnInfo> getPsnList([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iPsnInfo> res = new ResponseSet<iPsnInfo>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken != null && psnID > 0)
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();


                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                List<SqlParameter> parms = new List<SqlParameter>();

                Bizcs.Model.org_orgMain orgModel = orgBll.GetModel(int.Parse(reqModel.reqData[0].oid));
                parms.Add(new SqlParameter("@orgLevel", $"{orgModel.orgLevel}%"));
                parms.Add(new SqlParameter("@orgType", orgModel.orgType));

                DataSet dsOrg = orgBll.GetList(" orgLevel like @orgLevel and orgType=@orgType ", parms.ToArray());

                string strOrg = "";
                for (int i = 0; i < dsOrg.Tables[0].Rows.Count; i++)
                {
                    strOrg += dsOrg.Tables[0].Rows[i]["orgID"] + ",";
                }
                strOrg = strOrg.Substring(0, strOrg.Length - 1);

                //analyse conditions
                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(orgModel.orgType + "ID in (" + strOrg + ")");
                strWhere.Append(reqModel.reqData[0].kw != "" ? " and (psnName like '%" + reqModel.reqData[0].kw + "%' or psnCode like '%" + reqModel.reqData[0].kw + "%') " : "");
                strWhere.Append(" and onStatus=" + reqModel.reqData[0].ons);
                strWhere.Append(reqModel.reqData[0].ty == "all" ? " and onType<>'' " : " and onType=" + reqModel.reqData[0].ty);

                //get user's organization management scope
                List<string> orglist; string unitlist; string deptlist; string postlist;
                OsrzHelper.getOsrzOrgID(psnID, out orglist, out unitlist, out deptlist, out postlist);
                string strWhere1 = "";
                strWhere1 += (unitlist.Length > 0 ? "unitID in (" + unitlist + ")" : "");
                strWhere1 += (strWhere1.Length > 0 && deptlist.Length > 0 ? " or " : "") + (deptlist.Length > 0 ? " deptID in (" + deptlist + ")" : "");
                strWhere1 += (strWhere1.Length > 0 && postlist.Length > 0 ? " or " : "") + (postlist.Length > 0 ? " postID in (" + postlist + ")" : "");
                strWhere1 = strWhere1.Length > 0 ? (" and (" + strWhere1 + ") ") : "";

                DataSet dsPsn = psnBll.GetSimpleListByPage(strWhere.ToString() + strWhere1, " unitID,deptID,postID", sIndex, eIndex);

                if (dsPsn.Tables[0] != null && dsPsn.Tables[0].Rows.Count > 0)
                {
                    int psnCount = psnBll.GetSimpleList(strWhere.ToString()).Tables[0].Rows.Count;
                    List<iPsnInfo> psnList = listHelper.ConvertDtToList<iPsnInfo>(dsPsn.Tables[0]);
                    res.status = 1;
                    res.number = psnCount;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = psnList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "getPsn")]
        public ResponseSet<iPsnInfo> getPsn([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iPsnInfo> res = new ResponseSet<iPsnInfo>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken != null && psnID > 0)
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                string strWhere = "";
                switch (reqModel.reqData[0])
                {
                    case "all":
                        strWhere = " 1=1";
                        break;
                    case "on":
                        strWhere = "onStatus=10000";
                        break;
                    case "off":
                        strWhere = "onStatus=10001";
                        break;
                    case "noLogin":
                        strWhere = "loginStatus=-1";
                        break;
                    case "onLogin":
                        strWhere = "loginStatus=1";
                        break;
                    case "working":
                        strWhere = "onStatus=10000 and loginStatus=1 and psnStatus=1 ";
                        break;
                }
                DataSet dsPsn = psnBll.GetSimpleList(strWhere);
                if (dsPsn.Tables[0] != null && dsPsn.Tables[0].Rows.Count > 0)
                {
                    List<iPsnInfo> psnList = listHelper.ConvertDtToList<iPsnInfo>(dsPsn.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = psnList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "myPwdUpdate")]
        public ResponseSet<string> myPwdUpdate([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken != null && psnID > 0)
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.Model.psn_psnMain psnModel = psnBll.GetModel(psnID);

                Bizcs.BLL.sys_RSAKey rsaBll = new Bizcs.BLL.sys_RSAKey();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@wkey", reqModel.reqData[0]));
                string cryptedPrePwd = reqModel.reqData[1];
                string cryptedNextPwd = reqModel.reqData[2];

                DataSet dsrsa = rsaBll.GetList(" wkey=@wkey", param.ToArray());
                if (dsrsa.Tables[0] != null && dsrsa.Tables[0].Rows.Count > 0)//if RAS is exist
                {
                    string privateKey = dsrsa.Tables[0].Rows[0]["nkey"].ToString();

                    string prePwd = CryptRSA.RsaHelper.Decrypt(privateKey, cryptedPrePwd);
                    string nextPwd = CryptRSA.RsaHelper.Decrypt(privateKey, cryptedNextPwd);

                    string md5PrePwd = CryptAES.Md5Encrypt(prePwd);
                    if (md5PrePwd == psnModel.psnPassword)//if pre password is correct
                    {
                        string md5NextPwd = CryptAES.Md5Encrypt(nextPwd);
                        psnModel.psnPassword = md5NextPwd;
                        bool iss = psnBll.Update(psnModel);
                        if (iss)
                        {
                            LogHelper.logRecord(psnID, "update pwd", "success", md5NextPwd, "", "");

                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "ok!";
                            res.resData = null;
                        }
                        else
                        {
                            LogHelper.logRecord(psnID, "update pwd", "false", md5NextPwd, "", "");

                            res.status = -22;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "There is something wrong occured,please retry or contact administrator!";
                            res.resData = null;
                        }
                    }
                    else
                    {
                        LogHelper.logRecord(psnID, "update pwd", "false", cryptedNextPwd, "", "");

                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "The pre password is incorrect!";
                        res.resData = null;

                    }
                }
                else
                {
                    LogHelper.logRecord(psnID, "update pwd", "false", cryptedNextPwd, "", "");

                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "No RSA key!";
                    res.resData = null;
                }

            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "adminPwdUpdate")]
        public ResponseSet<string> adminPwdUpdate([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            int adminID = VerifyHelper.getAdminID(reqModel.uToken);
            if (reqModel.uToken != null && adminID > 0)
            {

                Bizcs.BLL.sys_admin adminBll = new Bizcs.BLL.sys_admin();
                Bizcs.Model.sys_admin adminModel = adminBll.GetModel(adminID);

                Bizcs.BLL.sys_RSAKey rsaBll = new Bizcs.BLL.sys_RSAKey();
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter("@wkey", reqModel.reqData[0]));
                string cryptedPrePwd = reqModel.reqData[1];
                string cryptedNextPwd = reqModel.reqData[2];

                DataSet dsrsa = rsaBll.GetList(" wkey=@wkey", param.ToArray());
                if (dsrsa.Tables[0] != null && dsrsa.Tables[0].Rows.Count > 0)//if RAS is exist
                {
                    string privateKey = dsrsa.Tables[0].Rows[0]["nkey"].ToString();

                    string prePwd = CryptRSA.RsaHelper.Decrypt(privateKey, cryptedPrePwd);
                    string nextPwd = CryptRSA.RsaHelper.Decrypt(privateKey, cryptedNextPwd);

                    string md5PrePwd = CryptAES.Md5Encrypt(prePwd);
                    if (md5PrePwd == adminModel.adminPwd)//if pre password is correct
                    {
                        string md5NextPwd = CryptAES.Md5Encrypt(nextPwd);
                        adminModel.adminPwd = md5NextPwd;
                        bool iss = adminBll.Update(adminModel);
                        if (iss)
                        {
                            LogHelper.logRecord(adminID, "update admin pwd", "success", md5NextPwd, "", "");
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "ok!";
                            res.resData = null;
                        }
                        else
                        {
                            LogHelper.logRecord(adminID, "update admin pwd", "false", md5NextPwd, "", "");
                            res.status = -22;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "There is something wrong occured,please retry or contact administrator!";
                            res.resData = null;
                        }
                    }
                    else
                    {
                        LogHelper.logRecord(adminID, "update admin pwd", "false", cryptedNextPwd, "", "");
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "The pre password is incorrect!";
                        res.resData = null;
                    }
                }
                else
                {
                    LogHelper.logRecord(adminID, "update admin pwd", "false", cryptedNextPwd, "", "");

                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "No RSA key!";
                    res.resData = null;
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            return res;
        }
    }
}
