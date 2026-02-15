using System.Data;

namespace appsin.Common
{
    public class OsrzHelper
    {
        public static void getOsrzOrgID(int psnID, out List<string> orglist, out string unitlist, out string deptlist, out string postlist)
        {
            List<string> result = new List<string>();
            Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
            Bizcs.Model.psn_psnMain psnModel = new Bizcs.BLL.psn_psnMain().GetModel(psnID);
            int psnUnitID = Convert.ToInt32(psnModel.unitID);
            int psnDeptID = Convert.ToInt32(psnModel.deptID);

            string bindType = ""; string isInSub = ""; string dynmOrg = "";

            DataSet dsOsrz = new Bizcs.BLL.sys_dataBind().GetOsrzOrg(psnID);
            for (int i = 0; i < dsOsrz.Tables[0].Rows.Count; i++)
            {
                bindType = dsOsrz.Tables[0].Rows[i]["bindType"].ToString();
                isInSub = dsOsrz.Tables[0].Rows[i]["subOrgIn"].ToString();
                dynmOrg = dsOsrz.Tables[0].Rows[i]["dynamicOrg"].ToString();
                if (bindType == "static" && isInSub == "1")
                {
                    DataSet dsLevel = orgBll.GetList(" orgLevel like '" + dsOsrz.Tables[0].Rows[i]["orgLevel"].ToString() + "%'");
                    for (int j = 0; j < dsLevel.Tables[0].Rows.Count; j++)
                    {
                        result.Add(dsLevel.Tables[0].Rows[j]["orgID"].ToString());
                    }
                }
                else if (bindType == "static" && isInSub == "-1")
                {
                    result.Add(dsOsrz.Tables[0].Rows[i]["staticOrgID"].ToString());
                }
                else if (bindType == "dynamic" && dynmOrg == "unit" && isInSub == "1")
                {
                    DataSet dsLevel = orgBll.GetList(" orgLevel like '" + orgBll.GetModel(psnUnitID).orgLevel + "%'");
                    for (int k = 0; k < dsLevel.Tables[0].Rows.Count; k++)
                    {
                        result.Add(dsLevel.Tables[0].Rows[k]["orgID"].ToString());
                    }
                }
                else if (bindType == "dynamic" && dynmOrg == "unit" && isInSub == "-1")
                {
                    result.Add(new Bizcs.BLL.psn_psnMain().GetModel(psnID).unitID.ToString());
                }
                else if (bindType == "dynamic" && dynmOrg == "dept" && isInSub == "1")
                {
                    DataSet dsLevel = orgBll.GetList(" orgLevel like '" + orgBll.GetModel(psnDeptID).orgLevel + "%'");
                    for (int x = 0; x < dsLevel.Tables[0].Rows.Count; x++)
                    {
                        result.Add(dsLevel.Tables[0].Rows[x]["orgID"].ToString());
                    }
                }
                else if (bindType == "dynamic" && dynmOrg == "dept" && isInSub == "-1")
                {
                    result.Add(new Bizcs.BLL.psn_psnMain().GetModel(psnID).unitID.ToString());
                }
                else
                { }
            }
            //split org by orgType
            unitlist = ""; deptlist = ""; postlist = ""; orglist = result.Distinct().ToList();
            for (int y = 0; y < result.Count; y++)
            {
                if (orgBll.GetModel(int.Parse(result[y])).orgType == "unit")
                {
                    unitlist += result[y] + ",";
                }
                if (orgBll.GetModel(int.Parse(result[y])).orgType == "dept")
                {
                    deptlist += result[y] + ",";
                }
                if (orgBll.GetModel(int.Parse(result[y])).orgType == "post")
                {
                    postlist += result[y] + ",";
                }
            }

            //remove the last comma
            unitlist = unitlist.Length > 0 ? unitlist.Substring(0, unitlist.Length - 1) : unitlist;
            deptlist = deptlist.Length > 0 ? deptlist.Substring(0, deptlist.Length - 1) : deptlist;
            postlist = postlist.Length > 0 ? postlist.Substring(0, postlist.Length - 1) : postlist;

        }

        public static List<string> getOsrzOrglist(List<string> orgIDList)
        {
            Bizcs.BLL.org_orgMain orgBll = new Bizcs.BLL.org_orgMain();
            Bizcs.Model.org_orgMain orgModel = new Bizcs.Model.org_orgMain();
            List<string> result = new List<string>();
            for (int i = 0; i < orgIDList.Count; i++)
            {
                orgModel = orgBll.GetModel(int.Parse(orgIDList[i]));
                int level = orgModel.orgLevel.Length / 3;
                for (int j = 0; j < level + 1; j++)
                {
                    result.Add(orgModel.orgLevel.Substring(0, (1 + 3 * j)));
                }
            }
            return result.Distinct().ToList();
        }
    }
}
