using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Text;
using appsin.Common;

namespace appsin.Controllers
{
    [Route("upload/[controller]")]
    [ApiController]
    public class FileSaveController : ControllerBase
    {

        [HttpPost(Name = "FileSave")]
        public async Task<IActionResult> FileSave()
        {
            var files = Request.Form.Files;
            var uToken = Request.Headers["uToken"].ToString();

            object res = null;
            int psnID = VerifyHelper.getPsnID(uToken);

            if (uToken != "" && psnID > 0)
            {
                long size = files.Sum(f => f.Length);
                string webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "files\\UpLoad\\");
                List<string> filelist = new List<string>();
                List<string> namelist = new List<string>();
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        string fileExt = Path.GetExtension(formFile.FileName); //文件扩展名，含“.”
                        if (fileExt == ".xlsx" || fileExt == ".xls" ||  fileExt == ".doc" ||fileExt == ".docx" ||
                            fileExt == ".pptx" || fileExt == ".ppt" ||  fileExt == ".png" ||fileExt == ".jpg" ||
                            fileExt == ".pdf"  || fileExt == ".zip")
                        {
                            long fileSize = formFile.Length; //get file size，以字节为单位
                            if (fileSize <= 100 * 1024 * 1024) //smaller than 100M
                            {
                                string newFileName = DateTime.Now.ToString("yyyyMMdd") + Guid.NewGuid().ToString().ToUpper() + fileExt; //随机生成新的文件名
                                namelist.Add(newFileName);
                                var filePath = webRootPath + newFileName;
                                filelist.Add(filePath);
                                using (var stream = new FileStream(filePath, FileMode.Create))
                                {
                                    await formFile.CopyToAsync(stream);
                                }
                                LogHelper.logRecord(psnID, "upload file", "success", formFile.FileName, newFileName, "");
                                res = new { status = 1, message = "Upload success！", count = files.Count, size, namelist };
                            }
                            else
                            {
                                LogHelper.logRecord(psnID, "upload file", "fail", formFile.FileName, "file too large", "");
                                res = new { status = -1, message = "File size limitation is 100M,too large to upload!" };
                            }
                        }
                        else
                        {
                            LogHelper.logRecord(psnID, "upload file", "fail", formFile.FileName, "File Type is not allowed", "");
                            res = new { status = -1, message = "File Type is not allowed!" };
                        }
                    }
                    else
                    {
                        res = new { status = 0, message = "No Valid File!" };
                    }
                }
            }
            else
            {
                res = new { status = -1, message = "uToken is invalid!" };
            }

            return Ok(res);
        
        }
    }
}
