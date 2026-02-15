using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using appsin.Common;
using appsin.Models;
using appsin.ApiModels;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class ConsoleController : ControllerBase
    {

        [HttpPost(Name = "getConsoleNum")]
        public ResponseSet<consoleData> getConsoleNum([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<consoleData> res = new ResponseSet<consoleData>();
            int adminID = VerifyHelper.getAdminID(reqModel.uToken);
            if (reqModel.uToken != null && adminID > 0)
            {
                Bizcs.BLL.app_common commonBll = new Bizcs.BLL.app_common();
                DataSet ds1 = commonBll.getConsoleLoginCount();
                DataSet ds2 = commonBll.getConsoleLoginUser();
                DataSet ds3 = commonBll.getConsoleAppUse();
                DataSet ds4 = commonBll.getConsoleApiUse();
                if (ds1 != null && ds2 != null && ds3 != null && ds4 != null)
                {
                    List<consoleNum> list1 = Common.listHelper.ConvertDtToList<consoleNum>(ds1.Tables[0]);
                    List<consoleNum> list2 = Common.listHelper.ConvertDtToList<consoleNum>(ds2.Tables[0]);
                    List<consoleNum> list3 = Common.listHelper.ConvertDtToList<consoleNum>(ds3.Tables[0]);
                    List<consoleNum> list4 = Common.listHelper.ConvertDtToList<consoleNum>(ds4.Tables[0]);

                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Query Success!";
                    res.resData = new List<consoleData> { new consoleData { data1 = list1, data2 = list2, data3 = list3, data4 = list4 } };
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
                res.message = "Query Success!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "adminLogin")]
        public ResponseSet<string> adminLogin([FromBody] reqLogin reqLoginModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (VerifyHelper.isSafe(reqLoginModel.wkey) && VerifyHelper.isSafe(reqLoginModel.userName) && VerifyHelper.isSafe(reqLoginModel.vCode))
            {
                Bizcs.BLL.sys_RSAKey rsaBll = new Bizcs.BLL.sys_RSAKey();
                Bizcs.Model.sys_RSAKey rsaModel = rsaBll.GetModelByPubkey(reqLoginModel.wkey);

                Bizcs.Model.sys_admin adminModel = new Bizcs.BLL.sys_admin().GetModelByName(reqLoginModel.userName);

                Bizcs.Model.sys_captcha captchaModel = new Bizcs.BLL.sys_captcha().GetValidModel(reqLoginModel.vCode.ToUpper());

                if (rsaModel == null)//if RSA key is exist
                {
                    LogHelper.logRecord(0, "adminlogin", "fail", reqLoginModel.userName, "wrong pubkey", reqLoginModel.wkey);
                    res.status = 1;
                    res.uToken = "";
                    res.message = "Wrong pubkey！";
                }
                else if (adminModel == null)
                {
                    res.status = -1;
                    res.uToken = "";
                    res.message = "Cannot find this administrator！Please check the administrator name";
                }
                else if (captchaModel == null)
                {
                    res.status = -2;
                    res.uToken = "";
                    res.message = "Wrong captcha！Please check the captcha string";
                }
                else
                {
                    string adminPwd1 = CryptRSA.RsaHelper.Decrypt(rsaModel.nkey, reqLoginModel.userPwd);
                    string adminPwd2 = CryptAES.Md5Encrypt(adminPwd1);
                    string adminPwd3 = adminModel.adminPwd;
                    if (adminPwd2 == adminPwd3)
                    {
                        string uToken = VerifyHelper.genTokenStr(adminModel.adminID);
                        LogHelper.logRecord(adminModel.adminID, "adminlogin", "success", adminModel.adminName, "", reqLoginModel.bfp);
                        res.status = 1;
                        res.uToken = uToken;
                        res.message = "Admin login success！";
                    }
                    else
                    {
                        LogHelper.logRecord(adminModel.adminID, "adminlogin", "fail", adminModel.adminName, "wrong pwd", adminPwd2);
                        res.status = 1;
                        res.uToken = "";
                        res.message = "Wrong Password！";
                    }
                }
            }

            return res;
        }

        [HttpPost(Name = "genCaptcha")]
        public ResponseSet<string> genCaptcha([FromBody] reqSimple reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            string adminName = reqModel.args;
            if (!VerifyHelper.isSafe(adminName))
            {
                res.status = -1;
                res.uToken = "";
                res.message = "The user name has high risk charactors！";
            }
            else
            {
                Bizcs.Model.sys_admin adminModel = new Bizcs.BLL.sys_admin().GetModelByName(adminName);
                int adminID = 0;
                if (adminModel != null)
                {
                    adminID = adminModel.adminID;
                }

                LogHelper.logRecord(0, "generate admin captcha", "success", adminName, "", "");
                string imgStr = CaptchaHelper.genAdminCaptcha(adminID);

                res.status = 1;
                res.uToken = "";
                res.message = "Generate captcha success！";
                res.resData = [imgStr];
            }
            return res; ;
        }

        [HttpPost(Name = "getAdminMenu")]
        public ResponseSet<iMenuList> getAdminMenu([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iMenuList> res = new ResponseSet<iMenuList>();
            int adminID = VerifyHelper.getAdminID(reqModel.uToken);
            if (reqModel.uToken != null && adminID > 0)
            {
                if (VerifyHelper.isSafe(reqModel.reqData[0]))
                {
                    Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                    DataSet dsMenu = menuBll.GetAdminMenu();
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

    }
}
