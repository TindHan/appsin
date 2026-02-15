using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Cryptography.Xml;
using appsin.Common;
using appsin.Models;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class OsrzsController : ControllerBase
    {
        [HttpPost(Name = "osrzDelete")]
        public ResponseSet<string> osrzDelete([FromBody] RequestSet<osrzDisable> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0 && VerifyHelper.IsConvertToInt(reqModel.reqData[0].id))
                {
                    if (reqModel.reqData[0].type == "func")
                    {
                        Bizcs.BLL.sys_menuOsrz funcBll = new Bizcs.BLL.sys_menuOsrz();
                        bool iss = funcBll.Delete(int.Parse(reqModel.reqData[0].id));
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
                    else
                    {
                        Bizcs.BLL.sys_dataOsrz dataBll = new Bizcs.BLL.sys_dataOsrz();
                        bool iss = dataBll.Delete(int.Parse(reqModel.reqData[0].id));
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

        [HttpPost(Name = "osrzDisable")]
        public ResponseSet<string> osrzDisable([FromBody] RequestSet<osrzDisable> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (reqModel.reqData[0].type == "func")
                    {
                        Bizcs.BLL.sys_menuOsrz funcBll = new Bizcs.BLL.sys_menuOsrz();
                        Bizcs.Model.sys_menuOsrz funcModel = funcBll.GetModel(int.Parse(reqModel.reqData[0].id));
                        funcModel.osrzStatus = 0;
                        bool iss = funcBll.Update(funcModel);
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
                    else
                    {
                        Bizcs.BLL.sys_dataOsrz dataBll = new Bizcs.BLL.sys_dataOsrz();
                        Bizcs.Model.sys_dataOsrz dataModel = dataBll.GetModel(int.Parse(reqModel.reqData[0].id));
                        dataModel.osrzStatus = 0;
                        bool iss = dataBll.Update(dataModel);
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

        [HttpPost(Name = "getAllBind")]
        public ResponseSet<objRoleBindList> getAllBind([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<objRoleBindList> res = new ResponseSet<objRoleBindList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                    int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                    int sIndex = (pageIndex - 1) * pageListNum + 1;
                    int eIndex = pageIndex * pageListNum;

                    string strwhere1 = "";
                    if (reqModel.reqData[0].kw != "0")
                    {
                        string[] kwList = reqModel.reqData[0].kw.Split(":");
                        strwhere1 = "osrzObjType = '" + kwList[0] + "'" + " and  osrzObjID=" + kwList[1] + " ";
                    }
                    string strwhere2 = reqModel.reqData[0].ty == "all" ? "" : (strwhere1 == "" ? (" osrzWay='" + reqModel.reqData[0].ty + "' ") : (" and osrzWay='" + reqModel.reqData[0].ty + "' "));

                    Bizcs.BLL.sys_dataOsrz bindBll = new Bizcs.BLL.sys_dataOsrz();
                    DataSet dsBind = bindBll.GetAllBindByPage(reqModel.reqData[0].ons, "", strwhere1 + strwhere2, sIndex, eIndex);
                    DataSet dsBindNumber = bindBll.GetAllBindByPage(reqModel.reqData[0].ons, "", strwhere1 + strwhere2, 1, 999999);
                    if (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0)
                    {
                        List<objRoleBindList> bindList = listHelper.ConvertDtToList<objRoleBindList>(dsBind.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.number = dsBindNumber.Tables[0].Rows.Count;
                        res.resData = bindList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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

        [HttpPost(Name = "addFuncOsrz")]
        public ResponseSet<string> addFuncOsrz([FromBody] RequestSet<objRoleBindEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuOsrz osrzBll = new Bizcs.BLL.sys_menuOsrz();
                    Bizcs.Model.sys_menuOsrz osrzModel = new Bizcs.Model.sys_menuOsrz();

                    DataSet ds = osrzBll.GetList(" osrzStatus=1 and osrzObjType='" + reqModel.reqData[0].objType + "' and osrzObjID=" + int.Parse(reqModel.reqData[0].objID) + " and osrzRoleID=" + int.Parse(reqModel.reqData[0].roleID));
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is a same bind already exist,please check bind infomation!";
                        res.resData = null;
                    }
                    else
                    {
                        osrzModel.osrzRoleID = int.Parse(reqModel.reqData[0].roleID);
                        osrzModel.osrzObjType = reqModel.reqData[0].objType;
                        osrzModel.osrzObjID = int.Parse(reqModel.reqData[0].objID);
                        osrzModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        osrzModel.createTime = DateTime.Now;
                        osrzModel.osrzStatus = 1;

                        int osrzID = osrzBll.Add(osrzModel);
                        if (osrzID > 0)
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

        [HttpPost(Name = "addDataOsrz")]
        public ResponseSet<string> addDataOsrz([FromBody] RequestSet<objRoleBindEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataOsrz osrzBll = new Bizcs.BLL.sys_dataOsrz();
                    Bizcs.Model.sys_dataOsrz osrzModel = new Bizcs.Model.sys_dataOsrz();

                    DataSet ds = osrzBll.GetList(" osrzStatus=1 and osrzObjType='" + reqModel.reqData[0].objType + "' and osrzObjID=" + int.Parse(reqModel.reqData[0].objID) + " and osrzRoleID=" + int.Parse(reqModel.reqData[0].roleID));
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is a same bind already exist,please check bind infomation!";
                        res.resData = null;
                    }
                    else
                    {
                        osrzModel.osrzRoleID = int.Parse(reqModel.reqData[0].roleID);
                        osrzModel.osrzObjType = reqModel.reqData[0].objType;
                        osrzModel.osrzObjID = int.Parse(reqModel.reqData[0].objID);
                        osrzModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        osrzModel.createTime = DateTime.Now;
                        osrzModel.osrzStatus = 1;

                        int osrzID = osrzBll.Add(osrzModel);
                        if (osrzID > 0)
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

        [HttpPost(Name = "getObjList")]
        public ResponseSet<objList> getObjList([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<objList> res = new ResponseSet<objList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataOsrz bindBll = new Bizcs.BLL.sys_dataOsrz();
                    DataSet dsBind = bindBll.GetObjecList();
                    if (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0)
                    {
                        List<objList> bindList = listHelper.ConvertDtToList<objList>(dsBind.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = bindList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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


        [HttpPost(Name = "dataBindDel")]
        public ResponseSet<string> dataBindDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataBind bindBll = new Bizcs.BLL.sys_dataBind();
                    int bindID = int.Parse(reqModel.reqData[0]);

                    bool iss = bindBll.Delete(bindID);
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

        [HttpPost(Name = "dataBindAdd")]
        public ResponseSet<string> dataBindAdd([FromBody] RequestSet<dataItemEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataBind bindBll = new Bizcs.BLL.sys_dataBind();
                    Bizcs.Model.sys_dataBind bindModel = new Bizcs.Model.sys_dataBind();
                    bindModel.bindType = reqModel.reqData[0].bindType;
                    bindModel.dynamicOrg = reqModel.reqData[0].dynamicOrg;
                    bindModel.subOrgIn = int.Parse(reqModel.reqData[0].subOrgIn);
                    bindModel.staticOrgID = int.Parse(reqModel.reqData[0].staticOrgID);
                    bindModel.roleID = int.Parse(reqModel.reqData[0].roleID);
                    bindModel.createTime = DateTime.Now;
                    bindModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                    bindModel.bindStatus = 1;

                    int bindID = bindBll.Add(bindModel);
                    if (bindID > 0)
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


        [HttpPost(Name = "getDataRoleBind")]
        public ResponseSet<dataItemList> getDataRoleBind([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<dataItemList> res = new ResponseSet<dataItemList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataBind bindBll = new Bizcs.BLL.sys_dataBind();
                    DataSet dsBind = bindBll.GetSimpleList(" roleID=" + reqModel.reqData[0]);
                    if (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0)
                    {
                        List<dataItemList> bindList = listHelper.ConvertDtToList<dataItemList>(dsBind.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = bindList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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

        [HttpPost(Name = "dataRoleAdd")]
        public ResponseSet<string> dataRoleAdd([FromBody] RequestSet<osrzRoleEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataRole roleBll = new Bizcs.BLL.sys_dataRole();
                    Bizcs.Model.sys_dataRole roleModel = new Bizcs.Model.sys_dataRole();

                    DataSet ds = roleBll.GetList(" roleStatus=1 and roleName = " + reqModel.reqData[0].roleName);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is the same name role exist,please change the role name!";
                        res.resData = null;
                    }
                    else
                    {
                        roleModel.rolePK = Guid.NewGuid().ToString().ToUpper();
                        roleModel.roleName = reqModel.reqData[0].roleName;
                        roleModel.roleMemo1 = reqModel.reqData[0].roleMemo;
                        roleModel.createTime = DateTime.Now;
                        roleModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        roleModel.roleStatus = (reqModel.reqData[0].roleStatus == "on" ? 1 : 0);
                        int roleID = roleBll.Add(roleModel);
                        if (roleID > 0)
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


        [HttpPost(Name = "dataRoleEdit")]
        public ResponseSet<string> dataRoleEdit([FromBody] RequestSet<osrzRoleEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataRole roleBll = new Bizcs.BLL.sys_dataRole();
                    Bizcs.Model.sys_dataRole roleModel = roleBll.GetModel(int.Parse(reqModel.reqData[0].roleID));
                    roleModel.roleName = reqModel.reqData[0].roleName;
                    roleModel.roleMemo1 = reqModel.reqData[0].roleMemo;
                    roleModel.roleStatus = (reqModel.reqData[0].roleStatus == "on" ? 1 : 0);
                    bool iss = roleBll.Update(roleModel);
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


        [HttpPost(Name = "getDataRoleDetail")]
        public ResponseSet<osrzRoleList> getDataRoleDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<osrzRoleList> res = new ResponseSet<osrzRoleList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataRole roleBll = new Bizcs.BLL.sys_dataRole();
                    DataSet dsRole = roleBll.GetSimpleListByPage(" roleID=" + reqModel.reqData[0], "", 1, 1);
                    if (dsRole.Tables[0] != null && dsRole.Tables[0].Rows.Count > 0)
                    {
                        List<osrzRoleList> roleModel = listHelper.ConvertDtToList<osrzRoleList>(dsRole.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = roleModel.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
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


        [HttpPost(Name = "getDataRoleList")]
        public ResponseSet<osrzRoleList> getDataRoleList([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<osrzRoleList> res = new ResponseSet<osrzRoleList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                    int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                    int sIndex = (pageIndex - 1) * pageListNum + 1;
                    int eIndex = pageIndex * pageListNum;

                    string kwWhere = reqModel.reqData[0].kw.Trim() == "" ? "" : " and roleName like '%" + reqModel.reqData[0].kw.Trim() + "%' ";
                    Bizcs.BLL.sys_dataRole roleBll = new Bizcs.BLL.sys_dataRole();
                    DataSet dsRole = roleBll.GetSimpleListByPage(" roleStatus=" + reqModel.reqData[0].ons + kwWhere, "", sIndex, eIndex);
                    DataSet dsRoleCount = roleBll.GetList(" roleStatus=" + reqModel.reqData[0].ons + kwWhere);
                    if (dsRole.Tables[0] != null && dsRole.Tables[0].Rows.Count > 0)
                    {
                        List<osrzRoleList> roleList = listHelper.ConvertDtToList<osrzRoleList>(dsRole.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.number = dsRoleCount.Tables[0].Rows.Count;
                        res.resData = roleList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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

        [HttpPost(Name = "dataRoleDel")]
        public ResponseSet<string> dataRoleDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_dataRole roleBll = new Bizcs.BLL.sys_dataRole();
                    Bizcs.BLL.sys_dataOsrz osrzBll = new Bizcs.BLL.sys_dataOsrz();
                    Bizcs.BLL.sys_dataBind bindBll = new Bizcs.BLL.sys_dataBind();
                    int roleID = int.Parse(reqModel.reqData[0]);
                    DataSet dsOsrz = osrzBll.GetList(" osrzRoleID=" + roleID);
                    DataSet dsBind = bindBll.GetList(" roleID=" + roleID);
                    if ((dsOsrz.Tables[0] != null && dsOsrz.Tables[0].Rows.Count > 0) || (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0))
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Someone already bind this role or this role already have authorized organization scope,can't be deleted!";
                        res.resData = null;
                    }
                    else
                    {
                        bool iss = roleBll.Delete(roleID);
                        if (iss)
                        {
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.resData = null;
                        }
                        else
                        {
                            res.status = -22;
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

        [HttpPost(Name = "funcBindDel")]
        public ResponseSet<string> funcBindDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuBind bindBll = new Bizcs.BLL.sys_menuBind();
                    int bindID = int.Parse(reqModel.reqData[0]);

                    bool iss = bindBll.Delete(bindID);
                    if (iss)
                    {
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = null;
                    }
                    else
                    {
                        res.status = -22;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
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

        [HttpPost(Name = "funcBindAdd")]
        public ResponseSet<string> funcBindAdd([FromBody] RequestSet<osrzItemEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuBind bindBll = new Bizcs.BLL.sys_menuBind();
                    Bizcs.Model.sys_menuBind bindModel = new Bizcs.Model.sys_menuBind();

                    string menuList = reqModel.reqData[0].menuID;
                    int count = 0;
                    if (menuList != "" && menuList != "0" && menuList.Substring(menuList.Length - 1, 1) == ",")//if menu list is not null and correct format
                    {
                        menuList = menuList.Substring(0, menuList.Length - 1);
                        var marr = menuList.Split(',');
                        string bindedMenu = "";

                        DataSet dsBind = bindBll.GetSimpleList(" roleID=" + reqModel.reqData[0].roleID + " and menuID in (" + menuList + ")");
                        if (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0)//if menu bind already exist
                        {
                            for (int j = 0; j < dsBind.Tables[0].Rows.Count; j++)
                            {
                                bindedMenu += dsBind.Tables[0].Rows[j]["menuName"].ToString() + " / ";
                            }
                            res.status = -11;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Some menu bind already exist! The list on blew:<br />" + bindedMenu.Substring(0, bindedMenu.Length - 2);
                            res.resData = [bindedMenu];
                        }
                        else
                        {
                            for (int i = 0; i < marr.Length; i++)
                            {
                                bindModel.menuID = int.Parse(marr[i].ToString());
                                bindModel.roleID = int.Parse(reqModel.reqData[0].roleID);
                                bindModel.createTime = DateTime.Now;
                                bindModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                                bindModel.bindStatus = 1;
                                bindBll.Add(bindModel);
                                count++;
                            }

                            if (count > 0)
                            {
                                res.status = 1;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "Exec success!";
                                res.resData = null;
                            }
                            else
                            {
                                res.status = -22;
                                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                                res.message = "There is something wrong occured,please retry or contact administrator!";
                                res.resData = null;
                            }
                        }



                    }
                    else
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Please choose at least one menu!";
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


        [HttpPost(Name = "getFuncRoleBind")]
        public ResponseSet<osrzItemList> getFuncRoleBind([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<osrzItemList> res = new ResponseSet<osrzItemList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuBind bindBll = new Bizcs.BLL.sys_menuBind();
                    DataSet dsBind = bindBll.GetSimpleList(" roleID=" + reqModel.reqData[0]);
                    if (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0)
                    {
                        List<osrzItemList> bindList = listHelper.ConvertDtToList<osrzItemList>(dsBind.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = bindList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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

        [HttpPost(Name = "funcRoleAdd")]
        public ResponseSet<string> funcRoleAdd([FromBody] RequestSet<osrzRoleEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuRole roleBll = new Bizcs.BLL.sys_menuRole();
                    Bizcs.Model.sys_menuRole roleModel = new Bizcs.Model.sys_menuRole();

                    DataSet ds = roleBll.GetList(" roleStatus=1 and roleName = " + reqModel.reqData[0].roleName);
                    if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is the same name role exist,please change the role name!";
                        res.resData = null;
                    }
                    else
                    {
                        roleModel.rolePK = Guid.NewGuid().ToString().ToUpper();
                        roleModel.roleName = reqModel.reqData[0].roleName;
                        roleModel.roleMemo1 = reqModel.reqData[0].roleMemo;
                        roleModel.createTime = DateTime.Now;
                        roleModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        roleModel.roleStatus = (reqModel.reqData[0].roleStatus == "on" ? 1 : 0);
                        int roleID = roleBll.Add(roleModel);
                        if (roleID > 0)
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


        [HttpPost(Name = "funcRoleEdit")]
        public ResponseSet<string> funcRoleEdit([FromBody] RequestSet<osrzRoleEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuRole roleBll = new Bizcs.BLL.sys_menuRole();
                    Bizcs.Model.sys_menuRole roleModel = roleBll.GetModel(int.Parse(reqModel.reqData[0].roleID));
                    roleModel.roleName = reqModel.reqData[0].roleName;
                    roleModel.roleMemo1 = reqModel.reqData[0].roleMemo;
                    roleModel.roleStatus = (reqModel.reqData[0].roleStatus == "on" ? 1 : 0);
                    bool iss = roleBll.Update(roleModel);
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


        [HttpPost(Name = "getFuncRoleDetail")]
        public ResponseSet<osrzRoleList> getFuncRoleDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<osrzRoleList> res = new ResponseSet<osrzRoleList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuRole roleBll = new Bizcs.BLL.sys_menuRole();
                    DataSet dsRole = roleBll.GetSimpleListByPage(" roleID=" + reqModel.reqData[0], "", 1, 1);
                    if (dsRole.Tables[0] != null && dsRole.Tables[0].Rows.Count > 0)
                    {
                        List<osrzRoleList> roleModel = listHelper.ConvertDtToList<osrzRoleList>(dsRole.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = roleModel.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is something wrong occured,please retry or contact administrator!";
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


        [HttpPost(Name = "getFuncRoleList")]
        public ResponseSet<osrzRoleList> getFuncRoleList([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<osrzRoleList> res = new ResponseSet<osrzRoleList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                    int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                    int sIndex = (pageIndex - 1) * pageListNum + 1;
                    int eIndex = pageIndex * pageListNum;

                    string kwWhere = reqModel.reqData[0].kw.Trim() == "" ? "" : " and roleName like '%" + reqModel.reqData[0].kw.Trim() + "%' ";
                    Bizcs.BLL.sys_menuRole roleBll = new Bizcs.BLL.sys_menuRole();
                    DataSet dsRole = roleBll.GetSimpleListByPage(" roleStatus=" + reqModel.reqData[0].ons + kwWhere, "", sIndex, eIndex);
                    DataSet dsRoleCount = roleBll.GetList(" roleStatus=" + reqModel.reqData[0].ons + kwWhere);
                    if (dsRole.Tables[0] != null && dsRole.Tables[0].Rows.Count > 0)
                    {
                        List<osrzRoleList> roleList = listHelper.ConvertDtToList<osrzRoleList>(dsRole.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.number = dsRoleCount.Tables[0].Rows.Count;
                        res.resData = roleList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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

        [HttpPost(Name = "funcRoleDel")]
        public ResponseSet<string> funcRoleDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    Bizcs.BLL.sys_menuRole roleBll = new Bizcs.BLL.sys_menuRole();
                    Bizcs.BLL.sys_menuOsrz osrzBll = new Bizcs.BLL.sys_menuOsrz();
                    Bizcs.BLL.sys_menuBind bindBll = new Bizcs.BLL.sys_menuBind();
                    int roleID = int.Parse(reqModel.reqData[0]);
                    DataSet dsOsrz = osrzBll.GetList(" osrzRoleID=" + roleID);
                    DataSet dsBind = bindBll.GetList(" roleID=" + roleID);
                    if ((dsOsrz.Tables[0] != null && dsOsrz.Tables[0].Rows.Count > 0) || (dsBind.Tables[0] != null && dsBind.Tables[0].Rows.Count > 0))
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Someone already bind this role or this role already have authorized functions,can't be deleted!";
                        res.resData = null;
                    }
                    else
                    {
                        bool iss = roleBll.Delete(roleID);
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

        [HttpPost(Name = "getAllRole")]
        public ResponseSet<objList> getAllRole([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<objList> res = new ResponseSet<objList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {

                    Bizcs.BLL.sys_menuRole roleBll = new Bizcs.BLL.sys_menuRole();
                    DataSet dsRole = roleBll.GetAllRole();
                    if (dsRole.Tables[0] != null && dsRole.Tables[0].Rows.Count > 0)
                    {
                        List<objList> roleList = listHelper.ConvertDtToList<objList>(dsRole.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Exec success!";
                        res.resData = roleList.ToList();
                    }
                    else
                    {
                        res.status = -11;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is no data,please retry or contact administrator!";
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
    }
}
