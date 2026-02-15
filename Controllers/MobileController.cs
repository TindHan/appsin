using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using appsin.Bizcs.Model;
using appsin.Common;
using appsin.Models;

namespace appsin.Controllers
{
    [Route("core/api/[controller]/[action]")]
    [ApiController]
    public class MobileController : ControllerBase
    {
        [HttpPost(Name = "getMobOsrzMenu")]
        public ResponseSet<iMenuList> getMobOsrzMenu([FromBody] RequestSet<string> reqModel)
        {
            ResponseSet<iMenuList> res = new ResponseSet<iMenuList>();
            int psnID = VerifyHelper.getPsnID(reqModel.uToken);
            if (reqModel.uToken == null || psnID <= 0)
            {
                res.status = -1;
                res.uToken = "";
                res.message = "uToken is invalid!";
                res.resData = null;
            }
            if (!VerifyHelper.isSafe(reqModel.reqData[0]))
            {
                res.status = 110;
                res.uToken = VerifyHelper.updateTokenStr(reqModel.uToken);
                res.message = "High risk characters!";
                res.resData = null;
            }
            else
            {
                Bizcs.BLL.sys_menu menuBll = new Bizcs.BLL.sys_menu();
                DataSet dsMenu = menuBll.GetOsrzMobMenu(psnID);
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
    }
}
