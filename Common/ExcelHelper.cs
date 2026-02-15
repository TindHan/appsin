
using System.Data;
using System.Data.OleDb;
using System.Text;
namespace appsin.Common
{
    public class ExcelHelper
    {
        public static string DataToExcel(DataTable m_DataTable, string s_FileName, string titleName)
        {
            string fileUrl = ("\\files\\Download\\") + s_FileName;
            string FileName = Directory.GetCurrentDirectory() + "\\wwwroot" + fileUrl;  //file absolutely path

            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
            FileStream objFileStream;
            StreamWriter objStreamWriter;
            string strLine = "<table cellpadding=\"5\" style=\"font-family:宋体\">";
            objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
            objStreamWriter = new StreamWriter(objFileStream, Encoding.Unicode);

            strLine += "<tr><td colspan='" + (m_DataTable.Columns.Count + 1).ToString() + "' style='text-align:center;font-size:large'>" + titleName + "</td></tr>";

            strLine += "<tr><td>序号</td>";
            for (int i = 0; i < m_DataTable.Columns.Count; i++)
            {
                strLine = strLine + "<td style='text-align:center;'>" + m_DataTable.Columns[i].Caption.ToString() + "</td>";      //colume caption
            }
            strLine += "</tr>";

            for (int i = 0; i < m_DataTable.Rows.Count; i++)
            {
                strLine += "<tr>";
                strLine += "<td>" + (i + 1).ToString() + "</td>";
                for (int j = 0; j < m_DataTable.Columns.Count; j++)
                {
                    if (m_DataTable.Rows[i].ItemArray[j] == null)
                    {
                        strLine = strLine + "<td></td> ";
                    }
                    else
                    {
                        string rowstr = "";
                        rowstr = m_DataTable.Rows[i].ItemArray[j].ToString();
                        if (m_DataTable.Columns[j].Caption.ToString().Contains("ID") || m_DataTable.Columns[j].Caption.ToString().Contains("code") || m_DataTable.Columns[j].Caption.ToString().Contains("Passport") || m_DataTable.Columns[j].Caption.Contains("身份证") || m_DataTable.Columns[j].Caption.Contains("号码") || m_DataTable.Columns[j].Caption.Contains("电话") || m_DataTable.Columns[j].Caption.Contains("账号") || m_DataTable.Columns[j].Caption.ToString().Contains("编码"))
                        {
                            strLine = strLine + "<td style='vnd.ms-excel.numberformat:@'>" + rowstr + "</td>";
                        }
                        else
                        {
                            strLine = strLine + "<td>" + rowstr + "</td>";
                        }
                    }
                }
                strLine += "</tr>";
            }
            strLine += "</table>";
            objStreamWriter.WriteLine(strLine);
            strLine = "";
            objStreamWriter.Close();
            objFileStream.Close();
            return fileUrl;
        }

        public static DataSet ReadExcelToDs(string filepath)
        {
            try
            {
                string strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filepath + ";Extended Properties='Excel 12.0;HDR=Yes;IMEX=1';";
                using (OleDbConnection ole_conn = new OleDbConnection(strConn))
                {
                    string sql = "select * from [Sheet1$]";
                    OleDbDataAdapter adapter = new OleDbDataAdapter(sql, ole_conn);
                    DataSet ds = new DataSet();
                    adapter.Fill(ds, "Sheet1");
                    ole_conn.Close();
                    return ds;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}
