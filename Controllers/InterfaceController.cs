using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Common;
using appsin.Models;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class InterfaceController : ControllerBase
    {
        [HttpPost(Name = "osrzDetail")]
        public ResponseSet<iApiOsrzList> osrzDetail(RequestSet<string> reqModel)
        {
            ResponseSet<iApiOsrzList> res = new ResponseSet<iApiOsrzList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0]))
                    {
                        string strwhere1 = "osrzID=" + reqModel.reqData[0];
                        Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                        DataSet dsOsrz = osrzBll.GetSimpleListByPage(strwhere1, "", 1, 1);
                        if (dsOsrz.Tables[0] != null && dsOsrz.Tables[0].Rows.Count > 0)
                        {
                            List<iApiOsrzList> osrzList = listHelper.ConvertDtToList<iApiOsrzList>(dsOsrz.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.resData = osrzList.ToList();
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
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "osrzList")]
        public ResponseSet<iApiOsrzList> osrzList(RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iApiOsrzList> res = new ResponseSet<iApiOsrzList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].kw) && VerifyHelper.isSafe(reqModel.reqData[0].oid) && VerifyHelper.isSafe(reqModel.reqData[0].ty) && VerifyHelper.isSafe(reqModel.reqData[0].ons))
                    {
                        int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                        int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                        int sIndex = (pageIndex - 1) * pageListNum + 1;
                        int eIndex = pageIndex * pageListNum;

                        string strwhere1 = reqModel.reqData[0].oid == "all" ? ("osrzStatus=" + reqModel.reqData[0].ons) : ("osrzStatus=" + reqModel.reqData[0].ons + " and appID=" + reqModel.reqData[0].oid);
                        Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                        DataSet dsOsrz = osrzBll.GetSimpleListByPage(strwhere1, "", sIndex, eIndex);
                        DataSet dsInfNumber = osrzBll.GetList(strwhere1);
                        if (dsOsrz.Tables[0] != null && dsOsrz.Tables[0].Rows.Count > 0)
                        {
                            List<iApiOsrzList> osrzList = listHelper.ConvertDtToList<iApiOsrzList>(dsOsrz.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.number = dsInfNumber.Tables[0].Rows.Count;
                            res.resData = osrzList.ToList();
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
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "osrzAdd")]
        public ResponseSet<string> osrzAdd(RequestSet<iApiOsrzEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].apiID) && VerifyHelper.isSafe(reqModel.reqData[0].appID) && VerifyHelper.isSafe(reqModel.reqData[0].osrzDesc) && VerifyHelper.isSafe(reqModel.reqData[0].validStartTime.ToString()) && VerifyHelper.isSafe(reqModel.reqData[0].validEndTime.ToString()) && VerifyHelper.isSafe(reqModel.reqData[0].osrzStatus))
                    {

                        Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                        Bizcs.Model.api_apiOsrz osrzModel = new Bizcs.Model.api_apiOsrz();
                        osrzModel.appID = int.Parse(reqModel.reqData[0].appID);
                        osrzModel.apiID = int.Parse(reqModel.reqData[0].apiID);
                        osrzModel.osrzDescription = reqModel.reqData[0].osrzDesc;
                        osrzModel.validStartTime = reqModel.reqData[0].validStartTime;
                        osrzModel.validEndTime = reqModel.reqData[0].validEndTime;
                        osrzModel.createTime = DateTime.Now;
                        osrzModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        osrzModel.osrzStatus = reqModel.reqData[0].osrzStatus == "off" ? 0 : 1;
                        int apiID = osrzBll.Add(osrzModel);
                        if (apiID > 0)
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
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "osrzDel")]
        public ResponseSet<string> osrzDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int osrzID = int.Parse(reqModel.reqData[0]);
                    Bizcs.BLL.api_useLog logBll = new Bizcs.BLL.api_useLog();
                    DataSet dsLog = logBll.GetList("osrzID=" + osrzID);
                    if (dsLog.Tables[0] == null || dsLog.Tables[0].Rows.Count == 0)
                    {
                        Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                        bool iss = osrzBll.Delete(osrzID);
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
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is some invoke log belong to this authorization is exist,can't be deleted!";
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

        [HttpPost(Name = "osrzUpdate")]
        public ResponseSet<string> osrzUpdate(RequestSet<iApiOsrzEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].apiID) && VerifyHelper.isSafe(reqModel.reqData[0].appID) && VerifyHelper.isSafe(reqModel.reqData[0].apiID) && VerifyHelper.isSafe(reqModel.reqData[0].osrzDesc) && VerifyHelper.isSafe(reqModel.reqData[0].displayOrder) && VerifyHelper.isSafe(reqModel.reqData[0].osrzStatus))
                    {

                        Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                        Bizcs.Model.api_apiOsrz osrzModel = osrzBll.GetModel(int.Parse(reqModel.reqData[0].osrzID));
                        osrzModel.osrzDescription = reqModel.reqData[0].osrzDesc;
                        osrzModel.validStartTime = reqModel.reqData[0].validStartTime;
                        osrzModel.validEndTime = reqModel.reqData[0].validEndTime;
                        osrzModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder);
                        osrzModel.osrzStatus = reqModel.reqData[0].osrzStatus == "off" ? 0 : 1;
                        bool iss = osrzBll.Update(osrzModel);
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
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "getAllApi")]
        public ResponseSet<iApiList> getAllApi(RequestSet<string> reqModel)
        {
            ResponseSet<iApiList> res = new ResponseSet<iApiList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0]))
                    {
                        string strwhere1 = "apiStatus=1";
                        Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                        DataSet dsApi = apiBll.GetSimpleListByPage(strwhere1, "apiCode", 1, 999999);
                        if (dsApi.Tables[0] != null && dsApi.Tables[0].Rows.Count > 0)
                        {
                            List<iApiList> apiList = listHelper.ConvertDtToList<iApiList>(dsApi.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.number = dsApi.Tables[0].Rows.Count;
                            res.resData = apiList.ToList();
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
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "getApiList")]
        public ResponseSet<iApiList> getApiList(RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iApiList> res = new ResponseSet<iApiList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].kw) && VerifyHelper.isSafe(reqModel.reqData[0].oid) && VerifyHelper.isSafe(reqModel.reqData[0].ty) && VerifyHelper.isSafe(reqModel.reqData[0].ons))
                    {
                        int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                        int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                        int sIndex = (pageIndex - 1) * pageListNum + 1;
                        int eIndex = pageIndex * pageListNum;

                        string strwhere1 = "apiStatus=" + reqModel.reqData[0].ons;
                        string strwhere2 = reqModel.reqData[0].kw == "" ? "" : "and apiName like '%" + reqModel.reqData[0].kw + "%'";
                        string strwhere3 = reqModel.reqData[0].ty == "all" ? "" : " and apiType='" + reqModel.reqData[0].ty + "' ";

                        Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                        DataSet dsApi = apiBll.GetSimpleListByPage(strwhere1 + strwhere2 + strwhere3, " apiCode ", sIndex, eIndex);
                        DataSet dsApiNumber = apiBll.GetList(strwhere1 + strwhere2 + strwhere3);
                        if (dsApi.Tables[0] != null && dsApi.Tables[0].Rows.Count > 0)
                        {
                            List<iApiList> apiList = listHelper.ConvertDtToList<iApiList>(dsApi.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.number = dsApiNumber.Tables[0].Rows.Count;
                            res.resData = apiList.ToList();
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
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "getApiDetail")]
        public ResponseSet<iApiList> getApiDetail(RequestSet<string> reqModel)
        {
            ResponseSet<iApiList> res = new ResponseSet<iApiList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0]))
                    {
                        Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                        DataSet dsApi = apiBll.GetSimpleListByPage("apiID=" + reqModel.reqData[0], "", 1, 1);
                        if (dsApi.Tables[0] != null && dsApi.Tables[0].Rows.Count > 0)
                        {
                            List<iApiList> listInf = listHelper.ConvertDtToList<iApiList>(dsApi.Tables[0]);

                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.resData = listInf.ToList();
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
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "apiAdd")]
        public ResponseSet<string> apiAdd(RequestSet<iApiEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].apiID) && VerifyHelper.isSafe(reqModel.reqData[0].apiName) && VerifyHelper.isSafe(reqModel.reqData[0].apiDesc) && VerifyHelper.isSafe(reqModel.reqData[0].apiAddress) && VerifyHelper.isSafe(reqModel.reqData[0].apiKeyNote) && VerifyHelper.isSafe(reqModel.reqData[0].apiResPara) && VerifyHelper.isSafe(reqModel.reqData[0].apiReqPara) && VerifyHelper.isSafe(reqModel.reqData[0].apiType))
                    {

                        Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                        Bizcs.Model.api_apiMain apiModel = new Bizcs.Model.api_apiMain();
                        apiModel.apiName = reqModel.reqData[0].apiName;
                        apiModel.apiDescription = reqModel.reqData[0].apiDesc;
                        apiModel.apiCode = reqModel.reqData[0].apiCode;
                        apiModel.apiReqPara = reqModel.reqData[0].apiReqPara;
                        apiModel.apiResPara = reqModel.reqData[0].apiResPara;
                        apiModel.apiKeyNote = reqModel.reqData[0].apiKeyNote;
                        apiModel.apiType = reqModel.reqData[0].apiType;
                        apiModel.apiAddress = reqModel.reqData[0].apiAddress;
                        apiModel.apiDescription = reqModel.reqData[0].apiDesc;
                        apiModel.isIdentify = 1;
                        apiModel.createTime = DateTime.Now;
                        apiModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        apiModel.apiStatus = reqModel.reqData[0].apiStatus == "on" ? 1 : 0;
                        int infID = apiBll.Add(apiModel);
                        if (infID > 0)
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
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "apiUpdate")]
        public ResponseSet<string> apiUpdate(RequestSet<iApiEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].apiID) && VerifyHelper.isSafe(reqModel.reqData[0].apiName) && VerifyHelper.isSafe(reqModel.reqData[0].apiDesc) && VerifyHelper.isSafe(reqModel.reqData[0].apiAddress) && VerifyHelper.isSafe(reqModel.reqData[0].apiKeyNote) && VerifyHelper.isSafe(reqModel.reqData[0].apiResPara) && VerifyHelper.isSafe(reqModel.reqData[0].apiReqPara) && VerifyHelper.isSafe(reqModel.reqData[0].apiType))
                    {

                        Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                        Bizcs.Model.api_apiMain apiModel = apiBll.GetModel(int.Parse(reqModel.reqData[0].apiID));
                        apiModel.apiID = int.Parse(reqModel.reqData[0].apiID);
                        apiModel.apiName = reqModel.reqData[0].apiName;
                        apiModel.apiDescription = reqModel.reqData[0].apiDesc;
                        apiModel.apiCode = reqModel.reqData[0].apiCode;
                        apiModel.apiReqPara = reqModel.reqData[0].apiReqPara;
                        apiModel.apiResPara = reqModel.reqData[0].apiResPara;
                        apiModel.apiKeyNote = reqModel.reqData[0].apiKeyNote;
                        apiModel.apiType = reqModel.reqData[0].apiType;
                        apiModel.apiAddress = reqModel.reqData[0].apiAddress;
                        apiModel.apiDescription = reqModel.reqData[0].apiDesc;
                        apiModel.apiExample = reqModel.reqData[0].apiExample;
                        apiModel.isIdentify = 1;
                        apiModel.createTime = DateTime.Now;
                        apiModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        apiModel.apiStatus = reqModel.reqData[0].apiStatus == "off" ? 0 : 1;
                        bool iss = apiBll.Update(apiModel);
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
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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

        [HttpPost(Name = "apiDel")]
        public ResponseSet<string> apiDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int apiID = int.Parse(reqModel.reqData[0]);
                    Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                    DataSet dsOsrz = osrzBll.GetList("apiID=" + apiID);
                    if (dsOsrz.Tables[0] == null || dsOsrz.Tables[0].Rows.Count == 0)
                    {
                        Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                        bool iss = apiBll.Delete(apiID);
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
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "There is some menu belong to this app is exist,can't be deleted!";
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

        [HttpPost(Name = "getApiUseLog")]
        public ResponseSet<iApiUseLogList> getApiUseLog(RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iApiUseLogList> res = new ResponseSet<iApiUseLogList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].oid) && VerifyHelper.isSafe(reqModel.reqData[0].ty))
                    {
                        int pageIndex = Math.Max(reqModel.reqData[0].pageIndex, 1);
                        int pageListNum = Math.Max(reqModel.reqData[0].pageListNum, 10);
                        int sIndex = (pageIndex - 1) * pageListNum + 1;
                        int eIndex = pageIndex * pageListNum;

                        string strwhere1 = " 1=1 ";
                        string strwhere2 = reqModel.reqData[0].oid == "all" ? "" : ("and appID=" + reqModel.reqData[0].oid);
                        string strwhere3 = reqModel.reqData[0].ty == "all" ? "" : ("and apiID=" + reqModel.reqData[0].ty);

                        Bizcs.BLL.api_useLog logBll = new Bizcs.BLL.api_useLog();
                        DataSet dsLog = logBll.GetSimpleListByPage(strwhere1 + strwhere2 + strwhere3, "createTime", sIndex, eIndex);
                        DataSet dsLogNumber = logBll.GetList(strwhere1 + strwhere2 + strwhere3);
                        if (dsLog.Tables[0] != null && dsLog.Tables[0].Rows.Count > 0)
                        {
                            List<iApiUseLogList> logList = listHelper.ConvertDtToList<iApiUseLogList>(dsLog.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.number = dsLogNumber.Tables[0].Rows.Count;
                            res.resData = logList.ToList();
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
                        res.status = 110;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "High risk characters!";
                        res.resData = null;
                    }
                }
                else
                {
                    res.status = 0;
                    res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                    res.message = "Request Success,but no data input!";
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
        
        [HttpPost(Name = "getApiUseLogDetail")]
        public ResponseSet<iApiUseLogList> getApiUseLogDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iApiUseLogList> res = new ResponseSet<iApiUseLogList>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (VerifyHelper.isSafe(reqModel.reqData[0]) && VerifyHelper.IsConvertToInt(reqModel.reqData[0]))
                {
                    Bizcs.BLL.api_useLog logBll = new Bizcs.BLL.api_useLog();
                    DataSet dslog = logBll.GetSimpleList(int.Parse(reqModel.reqData[0]));
                    if (dslog.Tables[0] != null && dslog.Tables[0].Rows.Count > 0)
                    {
                        List<iApiUseLogList> logList = Common.listHelper.ConvertDtToList<iApiUseLogList>(dslog.Tables[0]);
                        res.status = 1;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "Query Success!";
                        res.resData = logList;
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
