using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Common;
using appsin.Models;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class FieldcodeController : ControllerBase
    {
        [HttpPost(Name = "itemDetail")]
        public ResponseSet<iFielditemList> itemDetail([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iFielditemList> res = new ResponseSet<iFielditemList>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (VerifyHelper.isSafe(reqModel.reqData[0]))
                {
                    Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                    DataSet dsItem = itemBll.GetSimpleList(" itemID=" + reqModel.reqData[0]);
                    if (dsItem.Tables[0] != null && dsItem.Tables[0].Rows.Count > 0)
                    {
                        List<iFielditemList> itemList = Common.listHelper.ConvertDtToList<iFielditemList>(dsItem.Tables[0]);
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

        [HttpPost(Name = "itemList")]
        public ResponseSet<iFielditemList> itemList([FromBody] RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iFielditemList> res = new ResponseSet<iFielditemList>();

            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                if (VerifyHelper.isSafe(reqModel.reqData[0].oid) && VerifyHelper.isSafe(reqModel.reqData[0].ons) && VerifyHelper.isSafe(reqModel.reqData[0].ty))
                {
                    string strWhere = reqModel.reqData[0].ty == "1" ? " itemLevel=1 and setID=" + reqModel.reqData[0].oid : " itemLevel=2 and parentID=" + reqModel.reqData[0].oid;
                    DataSet dsItem = itemBll.GetSimpleList(strWhere + " and itemStatus=" + reqModel.reqData[0].ons + " order by itemLevel,displayOrder");
                    if (dsItem.Tables[0] != null && dsItem.Tables[0].Rows.Count > 0)
                    {
                        List<iFielditemList> itemList = Common.listHelper.ConvertDtToList<iFielditemList>(dsItem.Tables[0]);
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

        [HttpPost(Name = "itemUpdate")]
        public ResponseSet<string> itemUpdate(RequestSet<iFielditemEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].itemID) && VerifyHelper.isSafe(reqModel.reqData[0].itemName) && VerifyHelper.isSafe(reqModel.reqData[0].itemDesc)  && VerifyHelper.isSafe(reqModel.reqData[0].itemStatus))
                    {

                        Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                        Bizcs.Model.sys_fieldItem itemModel = itemBll.GetModel(int.Parse(reqModel.reqData[0].itemID));

                        itemModel.itemName = reqModel.reqData[0].itemName;
                        itemModel.itemDescription = reqModel.reqData[0].itemDesc;
                        itemModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                        itemModel.itemStatus = reqModel.reqData[0].itemStatus == "off" ? 0 : 1;
                        bool iss = itemBll.Update(itemModel);
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

        [HttpPost(Name = "itemDel")]
        public ResponseSet<string> itemDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int itemID = int.Parse(reqModel.reqData[0]);
                    Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                    Bizcs.Model.sys_fieldItem itemModel = itemBll.GetModel(itemID);
                    if (DateTime.Now< Convert.ToDateTime(itemModel.createTime).AddMinutes(5))
                    {
                        bool iss = itemBll.Delete(itemID);
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
                        res.message = "The field item only deleted in 5 minites after create,but you can disable it!";
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

        [HttpPost(Name = "item2Add")]
        public ResponseSet<string> item2Add(RequestSet<iFielditemEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].itemID) && VerifyHelper.isSafe(reqModel.reqData[0].itemName) && VerifyHelper.isSafe(reqModel.reqData[0].itemDesc) &&  VerifyHelper.isSafe(reqModel.reqData[0].itemStatus))
                    {

                        Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                        Bizcs.Model.sys_fieldItem itemModel = new Bizcs.Model.sys_fieldItem();
                        Bizcs.Model.sys_fieldItem pitemModel = itemBll.GetModel(int.Parse(reqModel.reqData[0].parentID));

                        itemModel.parentPK = pitemModel.itemPK;
                        itemModel.parentID = pitemModel.itemID;
                        itemModel.setID = int.Parse(reqModel.reqData[0].setID);
                        itemModel.itemPK = Guid.NewGuid().ToString().ToUpper();
                        itemModel.itemName = reqModel.reqData[0].itemName;
                        itemModel.itemDescription = reqModel.reqData[0].itemDesc;
                        itemModel.itemLevel = 2;
                        itemModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                        itemModel.createTime = DateTime.Now;
                        itemModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        itemModel.itemStatus = reqModel.reqData[0].itemStatus == "off" ? 0 : 1;
                        int itemID = itemBll.Add(itemModel);
                        if (itemID > 0)
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

        [HttpPost(Name = "item1Add")]
        public ResponseSet<string> item1Add(RequestSet<iFielditemEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].itemID) && VerifyHelper.isSafe(reqModel.reqData[0].itemName) && VerifyHelper.isSafe(reqModel.reqData[0].itemDesc) && VerifyHelper.isSafe(reqModel.reqData[0].itemStatus))
                    {

                        Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                        Bizcs.Model.sys_fieldItem itemModel = new Bizcs.Model.sys_fieldItem();

                        itemModel.itemPK = Guid.NewGuid().ToString().ToUpper();
                        itemModel.setID = int.Parse(reqModel.reqData[0].setID);
                        itemModel.itemName = reqModel.reqData[0].itemName;
                        itemModel.itemDescription = reqModel.reqData[0].itemDesc;
                        itemModel.itemLevel = 1;
                        itemModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                        itemModel.createTime = DateTime.Now;
                        itemModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        itemModel.itemStatus = reqModel.reqData[0].itemStatus == "off" ? 0 : 1;
                        int itemID = itemBll.Add(itemModel);
                        if (itemID > 0)
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

        [HttpPost(Name = "setAll")]
        public ResponseSet<iFieldsetList> setAll(RequestSet<string> reqModel)
        {
            ResponseSet<iFieldsetList> res = new ResponseSet<iFieldsetList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0]))
                    {
                        string strwhere1 = "setStatus=1";
                        Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                        DataSet dsSet = setBll.GetSimpleListByPage(strwhere1, "", 1, 999999);
                        if (dsSet.Tables[0] != null && dsSet.Tables[0].Rows.Count > 0)
                        {
                            List<iFieldsetList> apiList = listHelper.ConvertDtToList<iFieldsetList>(dsSet.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.number = dsSet.Tables[0].Rows.Count;
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

        [HttpPost(Name = "setList")]
        public ResponseSet<iFieldsetList> setList(RequestSet<iReqConditon> reqModel)
        {
            ResponseSet<iFieldsetList> res = new ResponseSet<iFieldsetList>();
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

                        string strwhere1 = "setStatus=" + reqModel.reqData[0].ons;
                        string strwhere2 = reqModel.reqData[0].kw == "" ? "" : "and setName like '%" + reqModel.reqData[0].kw + "%'";
                        string strwhere3 = reqModel.reqData[0].ty == "all" ? "" : " and setType='" + reqModel.reqData[0].ty + "' ";

                        Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                        DataSet dsSet = setBll.GetSimpleListByPage(strwhere1 + strwhere2 + strwhere3, " displayOrder desc", sIndex, eIndex);
                        DataSet dsSetNumber = setBll.GetList(strwhere1 + strwhere2 + strwhere3);
                        if (dsSet.Tables[0] != null && dsSet.Tables[0].Rows.Count > 0)
                        {
                            List<iFieldsetList> setList = listHelper.ConvertDtToList<iFieldsetList>(dsSet.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.number = dsSetNumber.Tables[0].Rows.Count;
                            res.resData = setList.ToList();
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

        [HttpPost(Name = "setDetail")]
        public ResponseSet<iFieldsetList> setDetail(RequestSet<string> reqModel)
        {
            ResponseSet<iFieldsetList> res = new ResponseSet<iFieldsetList>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0]))
                    {
                        Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                        DataSet dsSet = setBll.GetSimpleListByPage("setID=" + reqModel.reqData[0], "", 1, 1);
                        if (dsSet.Tables[0] != null && dsSet.Tables[0].Rows.Count > 0)
                        {
                            List<iFieldsetList> listSet = listHelper.ConvertDtToList<iFieldsetList>(dsSet.Tables[0]);
                            res.status = 1;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "Exec success!";
                            res.resData = listSet.ToList();
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


        [HttpPost(Name = "setAdd")]
        public ResponseSet<string> setAdd(RequestSet<iFieldsetEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].setID) && VerifyHelper.isSafe(reqModel.reqData[0].setName) && VerifyHelper.isSafe(reqModel.reqData[0].setDesc) && VerifyHelper.isSafe(reqModel.reqData[0].setType) && VerifyHelper.isSafe(reqModel.reqData[0].setCode) && VerifyHelper.isSafe(reqModel.reqData[0].setStatus) && VerifyHelper.isSafe(reqModel.reqData[0].displayOrder))
                    {

                        Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                        Bizcs.Model.sys_fieldset setModel = new Bizcs.Model.sys_fieldset();
                        setModel.setPK = Guid.NewGuid().ToString().ToUpper();
                        setModel.setName = reqModel.reqData[0].setName;
                        setModel.setDescription = reqModel.reqData[0].setDesc;
                        setModel.setType = reqModel.reqData[0].setType;
                        setModel.setCode = reqModel.reqData[0].setCode;
                        setModel.setLevel = int.Parse(reqModel.reqData[0].setLevel==""?"0": reqModel.reqData[0].setLevel);
                        setModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                        setModel.createTime = DateTime.Now;
                        setModel.createUser = VerifyHelper.getPsnID(reqModel.uToken);
                        setModel.setStatus = reqModel.reqData[0].setStatus == "on" ? 1 : 0;
                        int setID = setBll.Add(setModel);
                        if (setID > 0)
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

        [HttpPost(Name = "setUpdate")]
        public ResponseSet<string> setUpdate(RequestSet<iFieldsetEdit> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    if (VerifyHelper.isSafe(reqModel.reqData[0].setID) && VerifyHelper.isSafe(reqModel.reqData[0].setName) && VerifyHelper.isSafe(reqModel.reqData[0].setDesc) && VerifyHelper.isSafe(reqModel.reqData[0].setType) && VerifyHelper.isSafe(reqModel.reqData[0].setCode) && VerifyHelper.isSafe(reqModel.reqData[0].setStatus) && VerifyHelper.isSafe(reqModel.reqData[0].displayOrder))
                    {

                        Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                        Bizcs.Model.sys_fieldset setModel = setBll.GetModel(int.Parse(reqModel.reqData[0].setID));
                        if (setModel.setType != "system")
                        {
                            setModel.setID = int.Parse(reqModel.reqData[0].setID);
                            setModel.setName = reqModel.reqData[0].setName;
                            setModel.setDescription = reqModel.reqData[0].setDesc;
                            setModel.setType = reqModel.reqData[0].setType;
                            setModel.setCode = reqModel.reqData[0].setCode;
                            setModel.setLevel = int.Parse(reqModel.reqData[0].setLevel == "" ? "0" : reqModel.reqData[0].setLevel);
                            setModel.displayOrder = int.Parse(reqModel.reqData[0].displayOrder == "" ? "0" : reqModel.reqData[0].displayOrder);
                            setModel.setStatus = reqModel.reqData[0].setStatus == "off" ? 0 : 1;
                            bool iss = setBll.Update(setModel);
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
                            res.status = -12;
                            res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                            res.message = "This field set is defined by system,can't be altered or deleted!";
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

        [HttpPost(Name = "setDel")]
        public ResponseSet<string> setDel([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<string> res = new ResponseSet<string>();
            if (reqModel.uToken != null && VerifyHelper.getPsnID(reqModel.uToken) > 0)
            {
                if (reqModel.reqData.Count > 0)
                {
                    int setID = int.Parse(reqModel.reqData[0]);
                    Bizcs.BLL.sys_fieldset setBll = new Bizcs.BLL.sys_fieldset();
                    Bizcs.Model.sys_fieldset setModel = setBll.GetModel(int.Parse(reqModel.reqData[0]));
                    if (setModel.setType != "system")
                    {
                        Bizcs.BLL.sys_fieldItem itemBll = new Bizcs.BLL.sys_fieldItem();
                        DataSet dsItem = itemBll.GetList("setID=" + setID);
                        if (dsItem.Tables[0] == null || dsItem.Tables[0].Rows.Count == 0)
                        {
                            Bizcs.BLL.api_apiMain apiBll = new Bizcs.BLL.api_apiMain();
                            bool iss = apiBll.Delete(setID);
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
                            res.message = "There is some items belong to this field set is exist,can't be deleted!";
                            res.resData = null;
                        }
                    }
                    else
                    {
                        res.status = -12;
                        res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                        res.message = "This field set is defined by system,can't be altered or deleted!";
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
