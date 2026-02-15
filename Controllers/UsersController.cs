using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Bizcs.Model;
using appsin.Common;
using appsin.Models;
using System.Text;
using Microsoft.Data.SqlClient;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpPost(Name = "gotoApp")]
        public ResponseSet<string> gotoApp([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();

            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int menuID = int.Parse(reqModel.reqData[0]);
                Bizcs.Model.sys_menu menuModel = new Bizcs.BLL.sys_menu().GetModel(menuID);
                string goStr = Guid.NewGuid().ToString().ToUpper();
                Bizcs.Model.app_goToLog gotoModel = new Bizcs.Model.app_goToLog();
                gotoModel.menuID = menuID;
                gotoModel.psnID = VerifyHelper.getPsnID(reqModel.uToken);
                gotoModel.appID = menuModel.menuAppID;
                gotoModel.goStr = goStr;
                gotoModel.goTime = DateTime.Now;
                new Bizcs.BLL.app_goToLog().Add(gotoModel);

                Bizcs.Model.app_appMain appModel = new Bizcs.BLL.app_appMain().GetModel(Convert.ToInt32(menuModel.menuAppID));

                res.status = 1;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Good job!";
                res.resData = [appModel.appDomain1, appModel.appDomain2, menuModel.menuLink, goStr];
            }


            return res;
        }

        [HttpPost(Name = "pwdUpdate")]
        public ResponseSet<string> pwdUpdate([FromBody] RequestSet<commonItem> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.Model.psn_psnMain psnModel = new Bizcs.Model.psn_psnMain();

                Bizcs.BLL.sys_RSAKey rsaBll = new Bizcs.BLL.sys_RSAKey();
                string psnID = reqModel.reqData[0].id;
                string pubKey = reqModel.reqData[0].order;
                string cryptedPwd = reqModel.reqData[0].type;

                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" wkey=@wkey");
                List<SqlParameter> parms = new List<SqlParameter>
                {
                    new SqlParameter("@wkdy",pubKey)
                };

                DataSet dsrsa = rsaBll.GetList(strWhere.ToString(), parms.ToArray());
                if (dsrsa.Tables[0] != null && dsrsa.Tables[0].Rows.Count > 0)
                {
                    string privateKey = dsrsa.Tables[0].Rows[0]["nkey"].ToString();
                    string userPwd = CryptRSA.RsaHelper.Decrypt(privateKey, cryptedPwd);
                    string md5Pwd = CryptAES.Md5Encrypt(userPwd);
                    psnModel = psnBll.GetModel(int.Parse(psnID));
                    psnModel.psnPassword = md5Pwd;
                    bool iss = psnBll.Update(psnModel);
                    if (iss)
                    {
                        LogHelper.logRecord(int.Parse(psnID), "update pwd", "true", cryptedPwd, "", "");

                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "ok!";
                        res.resData = null;
                    }
                    else
                    {
                        LogHelper.logRecord(int.Parse(psnID), "update pwd", "false", cryptedPwd, "", "");

                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
                        res.resData = null;
                    }

                }
                else
                {
                    LogHelper.logRecord(int.Parse(psnID), "update pwd", "false", cryptedPwd, "", "");

                    res.status = -1;
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

        [HttpPost(Name = "getApp")]
        public ResponseSet<app_appMain> getApp([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<app_appMain> res = new ResponseSet<app_appMain>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken != null && psnID > 0)
            {
                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                DataSet dsApp = appBll.GetList(" appStatus=1");
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<app_appMain> appList = Common.listHelper.ConvertDtToList<app_appMain>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = appList;
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

        [HttpPost(Name = "getMenu")]
        public ResponseSet<iMenuList> getMenu([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iMenuList> res = new ResponseSet<iMenuList>();

            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();

                if (!string.IsNullOrEmpty(reqModel.reqData[0]))
                {
                    strWhere.Append(" parentID=@parentID and ");
                    parms.Add(new SqlParameter("@parentID", reqModel.reqData[0]));
                }
                strWhere.Append(" menuStatus=1 order by menuLevel,displayOrder ");

                DataSet dsMenu = menuBll.GetSimpleList(strWhere.ToString(), parms.ToArray());
                if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                {
                    List<iMenuList> menuList = Common.listHelper.ConvertDtToList<iMenuList>(dsMenu.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = menuList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "getOsrzApp")]
        public ResponseSet<app_appMain> getOsrzApp([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<app_appMain> res = new ResponseSet<app_appMain>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken != null && psnID > 0)
            {
                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                DataSet dsApp = appBll.GetOsrzApp(psnID);
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<app_appMain> appList = Common.listHelper.ConvertDtToList<app_appMain>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = appList;
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

        [HttpPost(Name = "getOsrzMenu")]
        public ResponseSet<iMenuList> getOsrzMenu([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iMenuList> res = new ResponseSet<iMenuList>();

            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                DataSet dsMenu = menuBll.GetOsrzMenu(VerifyHelper.getPsnID(reqModel.uToken));
                if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                {
                    List<iMenuList> menuList = Common.listHelper.ConvertDtToList<iMenuList>(dsMenu.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = menuList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "getPsnRole")]
        public ResponseSet<iPsnRole> getPsnRole([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iPsnRole> res = new ResponseSet<iPsnRole>();

            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                DataSet dsMenu = menuBll.GetPsnRole(int.Parse(reqModel.reqData[0]));
                if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                {
                    List<iPsnRole> roleList = Common.listHelper.ConvertDtToList<iPsnRole>(dsMenu.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = roleList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "getMenuDetail")]
        public ResponseSet<iMenuList> getMenuDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iMenuList> res = new ResponseSet<iMenuList>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();

                StringBuilder strWhere = new StringBuilder();
                strWhere.Append(" menuID=@menuID ");
                List<SqlParameter> parms = new List<SqlParameter>
                    {
                        new SqlParameter("@menuID",reqModel.reqData[0])
                    };

                DataSet dsMenu = menuBll.GetSimpleList(strWhere.ToString(), parms.ToArray());
                if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                {
                    List<iMenuList> menuList = Common.listHelper.ConvertDtToList<iMenuList>(dsMenu.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = menuList;
                }
                else
                {
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success,but no data!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "getAppMenu")]
        public ResponseSet<iMenuList> getAppMenu([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iMenuList> res = new ResponseSet<iMenuList>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                if (VerifyHelper.isSafe(reqModel.reqData[0].oid) && VerifyHelper.isSafe(reqModel.reqData[0].ons) && VerifyHelper.isSafe(reqModel.reqData[0].ty))
                {
                    StringBuilder strWhere = new StringBuilder();
                    List<SqlParameter> parms = new List<SqlParameter>();
                    strWhere.Append(" menuID not in (10011,10016,10019) ");
                    if (reqModel.reqData[0].ty == "1")
                    {
                        strWhere.Append(" and menuLevel=1 and menuAppID=@menuAppID ");
                        parms.Add(new SqlParameter("@menuAppID", reqModel.reqData[0].oid));
                    }
                    else
                    {
                        strWhere.Append("  and menuLevel=2 and parentID=@parentID ");
                        parms.Add(new SqlParameter("@parentID", reqModel.reqData[0].oid));
                    }
                    strWhere.Append(" and menuStatus=@menuStatus order by menuLevel,displayOrder");
                    parms.Add(new SqlParameter("@menuStatus", reqModel.reqData[0].ons));

                    DataSet dsMenu = menuBll.GetSimpleList(strWhere.ToString(), parms.ToArray());
                    if (dsMenu.Tables[0] != null && dsMenu.Tables[0].Rows.Count > 0)
                    {
                        List<iMenuList> menuList = Common.listHelper.ConvertDtToList<iMenuList>(dsMenu.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Query Success!";
                        res.resData = menuList;
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
                    res.status = 110;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "High risk characters!";
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

        [HttpPost(Name = "loginCheck")]
        public ResponseSet<string> loginCheck([FromBody] reqLogin reqLoginModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();

            if (VerifyHelper.isSafe(reqLoginModel.wkey) && VerifyHelper.isSafe(reqLoginModel.userName) && VerifyHelper.isSafe(reqLoginModel.vCode))
            {
                Bizcs.BLL.sys_RSAKey rsaBll = new Bizcs.BLL.sys_RSAKey();
                Bizcs.Model.sys_RSAKey rsaModel = rsaBll.GetModelByPubkey(reqLoginModel.wkey);
                if (rsaModel != null)//if RSA key is exist
                {
                    string privateKey = rsaModel.nkey;

                    Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                    Bizcs.Model.psn_psnMain psnModel = psnBll.GetModelByPsnUserName(reqLoginModel.userName);
                    if (psnModel != null && privateKey != null)//if the user is exist
                    {
                        string userPwd1 = psnModel.psnPassword;
                        string userPwd2 = CryptRSA.RsaHelper.Decrypt(privateKey, reqLoginModel.userPwd);
                        //if password meet the safe requirement.
                        if (VerifyHelper.checkPwd(userPwd2))
                        {
                            string userPwd3 = CryptAES.Md5Encrypt(userPwd2);
                            int psnID = psnModel.psnID;
                            Bizcs.Model.psn_captcha capModel = new Bizcs.BLL.psn_captcha().GetIsCaptcha(psnID);
                            //no need to input captcha and password is correct
                            if (capModel == null && userPwd1.Equals(userPwd3))
                            {
                                string uToken = VerifyHelper.genTokenStr(psnID);
                                LogHelper.logRecord(psnID, "login", "success", reqLoginModel.userName, "", "");
                                res.status = 1;
                                res.uToken = uToken;
                                res.message = "Login success！";
                            }
                            //need to input captcha and captcha、password  are all correct
                            else if (capModel != null && capModel.captchaStr.ToUpper().Equals(reqLoginModel.vCode.ToUpper()) && userPwd1.Equals(userPwd3))
                            {
                                string uToken = VerifyHelper.genTokenStr(psnID);
                                LogHelper.logRecord(psnID, "login", "success", reqLoginModel.userName, "", "");

                                res.status = 1;
                                res.uToken = uToken;
                                res.message = "Login success！";
                            }
                            //no need to input captcha and password is incorrect
                            else if (capModel == null && !userPwd1.Equals(userPwd3))
                            {
                                LogHelper.logRecord(0, "login", "false", reqLoginModel.userName, "Wrong password", "");
                                string imgStr = CaptchaHelper.genPsnCaptcha(psnID);

                                res.status = -1;
                                res.uToken = "";
                                res.message = "Incorrect password,please change your password and retry！";
                                res.resData = [imgStr];
                            }
                            //need to input captcha and captcha is correct,but password is incorrect
                            else if (capModel != null && capModel.captchaStr.ToUpper().Equals(reqLoginModel.vCode.ToUpper()) && !userPwd1.Equals(userPwd3))
                            {
                                LogHelper.logRecord(0, "login", "false", reqLoginModel.userName, "Wrong password", "");
                                string imgStr = CaptchaHelper.genPsnCaptcha(psnID);

                                res.status = -1;
                                res.uToken = "";
                                res.message = "Incorrect password,please change your password and retry！！";
                                res.resData = [imgStr];
                            }
                            //need to input captcha and captcha is incorrect
                            else
                            {
                                LogHelper.logRecord(0, "login", "false", reqLoginModel.userName, "Wrong captcha", "");
                                string imgStr = CaptchaHelper.genPsnCaptcha(psnID);

                                res.status = -1;
                                res.uToken = "";
                                res.message = "Incorrect captcha,please reinput captcha and retry！";
                                res.resData = [imgStr];
                            }
                        }
                        else
                        {
                            res.status = -2;
                            res.uToken = "";
                            res.message = "Your password complexity is too low,please change your password in order to match the requirement！";
                        }

                    }
                    else
                    {
                        res.status = 0;
                        res.uToken = "";
                        res.message = "Can't find user,please check username！";
                    }
                }
            }
            else
            {
                res.status = -11;
                res.uToken = "";
                res.message = "High risk characters！";
            }

            return res;
        }

        [HttpPost(Name = "alterCaptcha")]
        public ResponseSet<string> alterCaptcha([FromBody] reqSimple reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            string userName = reqModel.args;
            if (VerifyHelper.isSafe(userName))
            {
                Bizcs.BLL.psn_psnMain psnBll = new Bizcs.BLL.psn_psnMain();
                Bizcs.Model.psn_psnMain psnModel = psnBll.GetModelByPsnUserName(userName);
                if (psnModel != null)
                {
                    int psnID = psnModel.psnID;
                    LogHelper.logRecord(0, "alter captcha", "success", userName, "", "");
                    string imgStr = CaptchaHelper.genPsnCaptcha(psnID);

                    res.status = 1;
                    res.uToken = "";
                    res.message = "Get captcha success！";
                    res.resData = [imgStr];
                }
                else
                {
                    res.status = 0;
                    res.uToken = "";
                    res.message = "User name is wrong ,please check and retry！";
                }
            }
            else
            {
                res.status = -1;
                res.uToken = "";
                res.message = "The user name has high risk charactors！";
            }
            return res; ;
        }

        [HttpPost(Name = "getwkey")]
        public ResponseSet<wkey> getwkey([FromBody] wkey pubkey)
        {

            Common.CryptRSA.RsaHelper.GenerateKeys(out var privateKey, out var publicKey);

            sys_RSAKey rsakeyModel = new sys_RSAKey();
            rsakeyModel.wkey = publicKey;
            rsakeyModel.nkey = privateKey;
            rsakeyModel.createUser = 10000;
            rsakeyModel.createTime = DateTime.Now;
            rsakeyModel.createFor = "login";
            Bizcs.BLL.sys_RSAKey rsakeyBll = new Bizcs.BLL.sys_RSAKey();
            rsakeyBll.Add(rsakeyModel);

            ResponseSet<wkey> rsaKey = new ResponseSet<wkey>();

            wkey returnkey = new wkey();
            returnkey.wkeyStr = publicKey.ToString();
            returnkey.wkeyFor = "";
            returnkey.wkeyStamp = "";
            wkey[] keyList = { returnkey };

            rsaKey.status = 1;
            rsaKey.message = "OK";
            rsaKey.resData = keyList.ToList();
            return rsaKey;
        }

        [HttpPost(Name = "sessionCheck")]
        public ResponseSet<string> sessionCheck([FromBody] RequestSet<string> reqModel)
        {

            ResponseSet<string> res = new ResponseSet<string>();

            if (!reqModel.uToken.Trim().Equals(""))
            {
                int psnID = Common.VerifyHelper.getPsnID(reqModel.uToken);
                if (psnID > 0)
                {
                    res.status = 1;
                    res.message = "OK";
                    res.resData = null;
                }
                else if (psnID == 0)
                {
                    res.status = 0;
                    res.message = "OK";
                    res.resData = null;
                }
                else
                {
                    res.status = -1;
                    res.message = "OK";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "logout")]
        public ResponseSet<string> logout([FromBody] RequestSet<string> reqModel)
        {

            ResponseSet<string> res = new ResponseSet<string>();

            if (!reqModel.uToken.Trim().Equals(""))
            {
                int psnID = VerifyHelper.getPsnID(reqModel.uToken);
                int iss = Common.VerifyHelper.cancelToken(reqModel.uToken);
                if (iss > 0)
                {
                    LogHelper.logRecord(psnID, "logout", "success", "", "", "");

                    res.status = 1;
                    res.message = "OK";
                    res.resData = null;
                }
                else if (iss == 0)
                {
                    LogHelper.logRecord(psnID, "logout", "fail", "", "", "");

                    res.status = 0;
                    res.message = "uToken is invalid!";
                    res.resData = null;
                }
                else
                {
                    LogHelper.logRecord(psnID, "logout", "fail", "", "", "");

                    res.status = -1;
                    res.message = "uToken is correct,but something wrong occured!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "getActLog")]
        public ResponseSet<iActLog> getActLog([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iActLog> res = new ResponseSet<iActLog>();

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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();
                strWhere.Append(" 1=1 ");
                string[] timeScope = reqModel.reqData[0].ons.Split(';');
                if (timeScope.Length == 2 && DateTime.TryParse(timeScope[0], out var st) && DateTime.TryParse(timeScope[1], out var et))
                {
                    DateTime sTime = DateTime.Parse(timeScope[0] + " 00:00:00");
                    DateTime eTime = DateTime.Parse(timeScope[1] + " 23:59:59");

                    strWhere.Append(" and logTime>=@logTime1 and logTime<=@logTime2");
                    parms.Add(new SqlParameter("@logTime1", sTime));
                    parms.Add(new SqlParameter("@logTime2", eTime));
                }
                if (!string.IsNullOrEmpty(reqModel.reqData[0].kw))
                {
                    strWhere.Append(" and psnID=(select psnID from psn_psnMain where psnName like @psnName )");
                    parms.Add(new SqlParameter("@psnName", $"%{reqModel.reqData[0].kw.Trim()}%"));
                }

                Bizcs.BLL.psn_actLog logBll = new Bizcs.BLL.psn_actLog();
                DataSet dsLog = logBll.GetSimpleListByPage(strWhere.ToString(), " logID desc", sIndex, eIndex, parms.ToArray());
                DataSet dsLogCount = logBll.GetList(strWhere.ToString(), parms.ToArray());

                if (dsLog.Tables[0] != null && dsLog.Tables[0].Rows.Count > 0)
                {
                    List<iActLog> logList = listHelper.ConvertDtToList<iActLog>(dsLog.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsLogCount.Tables[0].Rows.Count;
                    res.resData = logList;
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "exportActLog")]
        public ResponseSet<string> exportActLog([FromBody] RequestSet<iReqConditon> reqModel)
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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;


                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();
                strWhere.Append(" 1=1 ");
                string[] timeScope = reqModel.reqData[0].ons.Split(';');
                if (timeScope.Length == 2 && DateTime.TryParse(timeScope[0], out var st) && DateTime.TryParse(timeScope[1], out var et))
                {
                    DateTime sTime = DateTime.Parse(timeScope[0] + " 00:00:00");
                    DateTime eTime = DateTime.Parse(timeScope[1] + " 23:59:59");

                    strWhere.Append(" and logTime>=@logTime1 and logTime<=@logTime2");
                    parms.Add(new SqlParameter("@logTime1", sTime));
                    parms.Add(new SqlParameter("@logTime2", eTime));
                }
                if (!string.IsNullOrEmpty(reqModel.reqData[0].kw))
                {
                    strWhere.Append(" and psnID=(select psnID from psn_psnMain where psnName like @psnName )");
                    parms.Add(new SqlParameter("@psnName", $"%{reqModel.reqData[0].kw.Trim()}%"));
                }

                Bizcs.BLL.psn_actLog logBll = new Bizcs.BLL.psn_actLog();
                DataSet dsLog = logBll.GetSimpleListByPage(strWhere.ToString(), " logID desc", sIndex, eIndex, parms.ToArray());
                DataSet dsLogCount = logBll.GetList(strWhere.ToString(), parms.ToArray());

                if (dsLog.Tables[0] != null && dsLog.Tables[0].Rows.Count > 0)
                {
                    string fileName = "EXLOG" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                    string filePath = ExcelHelper.DataToExcel(dsLog.Tables[0], fileName, "LOG LIST");
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsLogCount.Tables[0].Rows.Count;
                    res.resData = [fileName, filePath];
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "getGotoLog")]
        public ResponseSet<iGotoLog> getGotoLog([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iGotoLog> res = new ResponseSet<iGotoLog>();

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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();
                strWhere.Append(" 1=1 ");
                string[] timeScope = reqModel.reqData[0].ons.Split(';');
                if (timeScope.Length == 2 && DateTime.TryParse(timeScope[0], out var st) && DateTime.TryParse(timeScope[1], out var et))
                {
                    DateTime sTime = DateTime.Parse(timeScope[0] + " 00:00:00");
                    DateTime eTime = DateTime.Parse(timeScope[1] + " 23:59:59");

                    strWhere.Append(" and goTime>=@goTime1 and goTime<=@goTime2 ");
                    parms.Add(new SqlParameter("@goTime1", sTime));
                    parms.Add(new SqlParameter("@goTime2", eTime));
                }
                if (!string.IsNullOrEmpty(reqModel.reqData[0].kw))
                {
                    strWhere.Append(" and psnID=(select psnID from psn_psnMain where psnName like @psnName )");
                    parms.Add(new SqlParameter("@psnName", $"%{reqModel.reqData[0].kw.Trim()}%"));
                }

                Bizcs.BLL.app_goToLog logBll = new Bizcs.BLL.app_goToLog();
                DataSet dsLog = logBll.GetSimpleListByPage(strWhere.ToString(), " logID desc", sIndex, eIndex, parms.ToArray());
                DataSet dsLogCount = logBll.GetList(strWhere.ToString(), parms.ToArray());
                if (dsLog.Tables[0] != null && dsLog.Tables[0].Rows.Count > 0)
                {
                    List<iGotoLog> logList = listHelper.ConvertDtToList<iGotoLog>(dsLog.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsLogCount.Tables[0].Rows.Count;
                    res.resData = logList;
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }

            return res;
        }

        [HttpPost(Name = "exportGotoLog")]
        public ResponseSet<string> exportGotoLog([FromBody] RequestSet<iReqConditon> reqModel)
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
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = -11;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                int sIndex = (pageIndex - 1) * pageListNum + 1;
                int eIndex = pageIndex * pageListNum;

                StringBuilder strWhere = new StringBuilder();
                List<SqlParameter> parms = new List<SqlParameter>();
                strWhere.Append(" 1=1 ");
                string[] timeScope = reqModel.reqData[0].ons.Split(';');
                if (timeScope.Length == 2 && DateTime.TryParse(timeScope[0], out var st) && DateTime.TryParse(timeScope[1], out var et))
                {
                    DateTime sTime = DateTime.Parse(timeScope[0] + " 00:00:00");
                    DateTime eTime = DateTime.Parse(timeScope[1] + " 23:59:59");

                    strWhere.Append(" and goTime>=@goTime1 and goTime<=@goTime2 ");
                    parms.Add(new SqlParameter("@goTime1", sTime));
                    parms.Add(new SqlParameter("@goTime2", eTime));
                }
                if (!string.IsNullOrEmpty(reqModel.reqData[0].kw))
                {
                    strWhere.Append(" and psnID=(select psnID from psn_psnMain where psnName like @psnName )");
                    parms.Add(new SqlParameter("@psnName", $"%{reqModel.reqData[0].kw.Trim()}%"));
                }

                Bizcs.BLL.app_goToLog logBll = new Bizcs.BLL.app_goToLog();
                DataSet dsLog = logBll.GetSimpleListByPage(strWhere.ToString(), " logID desc", sIndex, eIndex, parms.ToArray());
                DataSet dsLogCount = logBll.GetList(strWhere.ToString(), parms.ToArray());

                if (dsLog.Tables[0] != null && dsLog.Tables[0].Rows.Count > 0)
                {
                    string fileName = "EXLOG" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                    string filePath = ExcelHelper.DataToExcel(dsLog.Tables[0], fileName, "LOG LIST");
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsLogCount.Tables[0].Rows.Count;
                    res.resData = [fileName, filePath];
                }
                else
                {
                    res.status = -11;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is no data,please retry or contact administrator!";
                    res.resData = null;
                }
            }

            return res;
        }
    }
}
