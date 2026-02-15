using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Common;
using appsin.Models;
using System.Diagnostics.Eventing.Reader;
using Microsoft.Data.SqlClient;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class AppsController : ControllerBase
    {
        [HttpPost(Name = "menuUpdate")]
        public ResponseSet<string> menuUpdate(RequestSet<iMenuEdit> reqModel)
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }

            else if (!VerifyHelper.isSafe(reqModel.reqData[0].menuName) || !VerifyHelper.isSafe(reqModel.reqData[0].menuDesc) || !VerifyHelper.isSafe(reqModel.reqData[0].menuIcon) || !VerifyHelper.isSafe(reqModel.reqData[0].menuLink) || !VerifyHelper.isSafe(reqModel.reqData[0].displayOrder) || !VerifyHelper.isSafe(reqModel.reqData[0].menuStatus) || !VerifyHelper.isSafe(reqModel.reqData[0].menuID) || !VerifyHelper.isSafe(reqModel.reqData[0].appName))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                Bizcs.Model.sys_menu menuModel = menuBll.GetModel(int.Parse(reqModel.reqData[0].menuID));

                menuModel.menuName = reqModel.reqData[0].menuName;
                menuModel.menuType = reqModel.reqData[0].menuType;
                menuModel.menuDescription = reqModel.reqData[0].menuDesc;
                menuModel.menuIcon = reqModel.reqData[0].menuIcon;
                menuModel.menuLink = reqModel.reqData[0].menuLink;
                menuModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                menuModel.menuStatus = reqModel.reqData[0].menuStatus == "on" ? 1 : 0;
                bool iss = menuBll.Update(menuModel);
                if (iss)
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
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "menuDel")]
        public ResponseSet<string> menuDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count > 0 && VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                int menuID = int.Parse(reqModel.reqData[0]);
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                Bizcs.Model.sys_menu menuModel = menuBll.GetModel(menuID);

                DataSet dsOsrz = new Bizcs.BLL.sys_menuBind().GetList(" menuID=" + menuID);

                if (menuModel.menuAppID == 10001)
                {
                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Core apps are basis components of system ,any menu cannot be deleted!";
                    res.resData = null;
                }
                else if (dsOsrz != null && dsOsrz.Tables[0].Rows.Count > 0)
                {
                    res.status = -10;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "This menu has been authorized by some role, please remove all the authorization first!";
                    res.resData = null;
                }
                else
                {
                    bool iss = menuBll.Delete(menuID);
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
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
            else
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Query failed,no valid arguments!";
                res.resData = null;
            }
            return res;
        }

        [HttpPost(Name = "menuAdd")]
        public ResponseSet<string> menuAdd(RequestSet<iMenuEdit> reqModel)
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].appName) || !VerifyHelper.isSafe(reqModel.reqData[0].menuName) || !VerifyHelper.isSafe(reqModel.reqData[0].menuDesc) || !VerifyHelper.isSafe(reqModel.reqData[0].menuLink) || !VerifyHelper.isSafe(reqModel.reqData[0].menuIcon))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                Bizcs.Model.sys_menu menuModel = new Bizcs.Model.sys_menu();
                Bizcs.Model.sys_menu pmenuModel = menuBll.GetModel(int.Parse(reqModel.reqData[0].menuID));

                menuModel.parentPK = pmenuModel.menuPK;
                menuModel.parentID = pmenuModel.menuID;
                menuModel.menuPK = Guid.NewGuid().ToString().ToUpper();
                menuModel.menuName = reqModel.reqData[0].menuName;
                menuModel.menuDescription = reqModel.reqData[0].menuDesc;
                menuModel.menuIcon = reqModel.reqData[0].menuIcon;
                menuModel.menuLink = reqModel.reqData[0].menuLink;
                menuModel.menuLevel = 2;
                menuModel.menuType = reqModel.reqData[0].menuType;
                menuModel.menuAppID = pmenuModel.menuAppID;
                menuModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                menuModel.createTime = DateTime.Now;
                menuModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                menuModel.menuStatus = reqModel.reqData[0].menuStatus == "on" ? 1 : 0;
                int menuID = menuBll.Add(menuModel);
                if (menuID > 0)
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
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;
        }

        [HttpPost(Name = "moduleUpdate")]
        public ResponseSet<string> moduleUpdate(RequestSet<iMenuEdit> reqModel)
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].appName) || !VerifyHelper.isSafe(reqModel.reqData[0].menuName) || VerifyHelper.isSafe(reqModel.reqData[0].menuDesc) || !VerifyHelper.isSafe(reqModel.reqData[0].menuLink) || !VerifyHelper.isSafe(reqModel.reqData[0].menuIcon))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                Bizcs.Model.sys_menu menuModel = menuBll.GetModel(int.Parse(reqModel.reqData[0].menuID));
                menuModel.menuName = reqModel.reqData[0].menuName;
                menuModel.menuDescription = reqModel.reqData[0].menuDesc;
                menuModel.menuIcon = reqModel.reqData[0].menuIcon;
                menuModel.menuLink = reqModel.reqData[0].menuLink;
                menuModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                menuModel.menuStatus = reqModel.reqData[0].menuStatus == "on" ? 1 : 0;
                bool iss = menuBll.Update(menuModel);
                if (iss)
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
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;

        }
        [HttpPost(Name = "moduleAdd")]
        public ResponseSet<string> moduleAdd(RequestSet<iMenuEdit> reqModel)
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].appName) || !VerifyHelper.isSafe(reqModel.reqData[0].menuName) || !VerifyHelper.isSafe(reqModel.reqData[0].menuDesc) || !VerifyHelper.isSafe(reqModel.reqData[0].menuLink) || !VerifyHelper.isSafe(reqModel.reqData[0].menuIcon))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                Bizcs.Model.sys_menu menuModel = new Bizcs.Model.sys_menu();
                menuModel.menuPK = Guid.NewGuid().ToString().ToUpper();
                menuModel.menuName = reqModel.reqData[0].menuName;
                menuModel.menuDescription = reqModel.reqData[0].menuDesc;
                menuModel.menuIcon = reqModel.reqData[0].menuIcon;
                menuModel.menuLink = reqModel.reqData[0].menuLink;
                menuModel.menuLevel = 1;
                menuModel.menuType = "group";
                menuModel.menuAppID = int.Parse(reqModel.reqData[0].parentID);
                menuModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                menuModel.createTime = DateTime.Now;
                menuModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                menuModel.menuStatus = reqModel.reqData[0].menuStatus == "on" ? 1 : 0;
                int appID = menuBll.Add(menuModel);
                if (appID > 0)
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
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;

        }

        [HttpPost(Name = "getAllApp")]
        public ResponseSet<iAppsList> getAllApp(RequestSet<string> reqModel)
        {
            ResponseSet<iAppsList> res = new ResponseSet<iAppsList>();
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
                res.message = "Request Success,but no data input!";
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
                string strwhere1 = "appID<>10001 and appStatus=1";
                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                DataSet dsApp = appBll.GetSimpleListByPage(strwhere1, "", 1, 999999);
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<iAppsList> appList = listHelper.ConvertDtToList<iAppsList>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsApp.Tables[0].Rows.Count;
                    res.resData = appList.ToList();
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

        [HttpPost(Name = "getAppList")]
        public ResponseSet<iAppsList> getAppList(RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iAppsList> res = new ResponseSet<iAppsList>();
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].kw) || !VerifyHelper.isSafe(reqModel.reqData[0].oid) || !VerifyHelper.isSafe(reqModel.reqData[0].ty) || !VerifyHelper.isSafe(reqModel.reqData[0].ons))
            {
                res.status = 110;
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

                string strwhere1 = "appID<>10001 and appStatus=" + reqModel.reqData[0].ons;
                string strwhere2 = reqModel.reqData[0].kw == "" ? "" : "and appName like '%" + reqModel.reqData[0].kw + "%'";
                string strwhere3 = reqModel.reqData[0].ty == "all" ? "" : " and appType='" + reqModel.reqData[0].ty + "' ";

                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                DataSet dsApp = appBll.GetSimpleListByPage(strwhere1 + strwhere2 + strwhere3, "", sIndex, eIndex);
                DataSet dsAppNumber = appBll.GetList(strwhere1 + strwhere2 + strwhere3);
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<iAppsList> appList = listHelper.ConvertDtToList<iAppsList>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.number = dsAppNumber.Tables[0].Rows.Count;
                    res.resData = appList.ToList();
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

        [HttpPost(Name = "getAppDetail")]
        public ResponseSet<iAppsList> getAppDetail(RequestSet<string> reqModel)
        {
            ResponseSet<iAppsList> res = new ResponseSet<iAppsList>();

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
                res.message = "Request Success,but no data input!";
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
                List<SqlParameter> param= new List<SqlParameter>();
                param.Add(new SqlParameter("@appID", reqModel.reqData[0]));
                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                DataSet dsApp = appBll.GetSimpleListByPage("appID=@appID", "",1, 1,param.ToArray());
                if (dsApp.Tables[0] != null && dsApp.Tables[0].Rows.Count > 0)
                {
                    List<iAppsList> listApp = listHelper.ConvertDtToList<iAppsList>(dsApp.Tables[0]);
                    res.status = 1;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Exec success!";
                    res.resData = listApp.ToList();
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

        [HttpPost(Name = "appAdd")]
        public ResponseSet<string> appAdd(RequestSet<iAppsEdit> reqModel)
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].appID) || !VerifyHelper.isSafe(reqModel.reqData[0].appName) || !VerifyHelper.isSafe(reqModel.reqData[0].appSID) || !VerifyHelper.isSafe(reqModel.reqData[0].appSecret) || !VerifyHelper.isSafe(reqModel.reqData[0].appDesc) || !VerifyHelper.isSafe(reqModel.reqData[0].Domain1) || !VerifyHelper.isSafe(reqModel.reqData[0].Domain2) || !VerifyHelper.isSafe(reqModel.reqData[0].Domain3))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Common.CryptRSA.RsaHelper.GenerateKeys(out var privateKey, out var publicKey);

                Bizcs.Model.sys_RSAKey rsakeyModel = new Bizcs.Model.sys_RSAKey();
                rsakeyModel.wkey = publicKey;
                rsakeyModel.nkey = privateKey;
                rsakeyModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                rsakeyModel.createTime = DateTime.Now;
                rsakeyModel.createFor = "appVerify";
                Bizcs.BLL.sys_RSAKey rsakeyBll = new Bizcs.BLL.sys_RSAKey();
                rsakeyBll.Add(rsakeyModel);

                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                Bizcs.Model.app_appMain appModel = new Bizcs.Model.app_appMain();
                appModel.appPK = Guid.NewGuid().ToString().ToUpper();
                appModel.appName = reqModel.reqData[0].appName;
                appModel.appType = reqModel.reqData[0].appType;
                appModel.appSID = reqModel.reqData[0].appSID;
                appModel.appSecret = reqModel.reqData[0].appSecret;
                appModel.appSkey = publicKey;
                appModel.appDescription = reqModel.reqData[0].appDesc;
                appModel.validStartTime = DateTime.Parse(reqModel.reqData[0].startDate);
                appModel.validEndTime = DateTime.Parse(reqModel.reqData[0].endDate);
                appModel.appDomain1 = reqModel.reqData[0].Domain1;
                appModel.appDomain2 = reqModel.reqData[0].Domain2;
                appModel.appDomain3 = reqModel.reqData[0].Domain3;
                appModel.createTime = DateTime.Now;
                appModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                appModel.appStatus = reqModel.reqData[0].appStatus == "on" ? 1 : 0;
                int appID = appBll.Add(appModel);
                if (appID > 0)
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
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }

            }

            return res;

        }

        [HttpPost(Name = "appUpdate")]
        public ResponseSet<string> appUpdate(RequestSet<iAppsEdit> reqModel)
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
                res.message = "Request Success,but no data input!";
                res.resData = null;
            }
            else if (!VerifyHelper.isSafe(reqModel.reqData[0].appID) || !VerifyHelper.isSafe(reqModel.reqData[0].appName) || !VerifyHelper.isSafe(reqModel.reqData[0].appSID) || !VerifyHelper.isSafe(reqModel.reqData[0].appSecret) || !VerifyHelper.isSafe(reqModel.reqData[0].appDesc) || !VerifyHelper.isSafe(reqModel.reqData[0].Domain1) || !VerifyHelper.isSafe(reqModel.reqData[0].Domain2) || !VerifyHelper.isSafe(reqModel.reqData[0].Domain3))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                Bizcs.Model.app_appMain appModel = appBll.GetModel(int.Parse(reqModel.reqData[0].appID));
                appModel.appID = int.Parse(reqModel.reqData[0].appID);
                appModel.appName = reqModel.reqData[0].appName;
                appModel.appType = reqModel.reqData[0].appType;
                appModel.appSID = reqModel.reqData[0].appSID;
                appModel.appSecret = reqModel.reqData[0].appSecret;
                appModel.appDescription = reqModel.reqData[0].appDesc;
                appModel.validStartTime = DateTime.Parse(reqModel.reqData[0].startDate);
                appModel.validEndTime = DateTime.Parse(reqModel.reqData[0].endDate);
                appModel.appDomain1 = reqModel.reqData[0].Domain1;
                appModel.appDomain2 = reqModel.reqData[0].Domain2;
                appModel.appDomain3 = reqModel.reqData[0].Domain3;
                appModel.appStatus = reqModel.reqData[0].appStatus == "off" ? 0 : 1;
                bool iss = appBll.Update(appModel);
                if (iss)
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
                    res.message = "There is something wrong occured,please retry or contact administrator!";
                    res.resData = null;
                }
            }
            return res;

        }

        [HttpPost(Name = "appDel")]
        public ResponseSet<string> appDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken == null || VerifyHelper.getPsnID(reqModel.uToken) <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            else if (reqModel.reqData.Count > 0 && VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
            {
                int appID = int.Parse(reqModel.reqData[0]);
                DataSet dsMenu = new Bizcs.BLL.sys_menu().GetList("menuAppID=" + appID);
                DataSet dsApi = new Bizcs.BLL.api_apiOsrz().GetList(" appID=" + appID);
                if (dsMenu != null || dsMenu.Tables[0].Rows.Count == 0)
                {
                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is some menu belong to this app is exist,can't be deleted, please remove them first!";
                    res.resData = null;
                }
                else if (dsApi != null || dsApi.Tables[0].Rows.Count == 0)
                {
                    res.status = -12;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "There is api authorized to this app,can't be deleted, please remove them first!";
                    res.resData = null;
                }
                else
                {
                    Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                    bool iss = appBll.Delete(appID);
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
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
            else
            {
                res.status = 0;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "Query failed,no valid arguments!";
                res.resData = null;
            }
            return res;
        }
    }
}
