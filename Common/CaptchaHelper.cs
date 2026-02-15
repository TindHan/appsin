using static System.Net.Mime.MediaTypeNames;
using System.Drawing;
using System.Drawing.Imaging;

namespace appsin.Common
{
    public class CaptchaHelper
    {
        private static readonly Random _random = new Random();
        private const string CaptchaChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        // generate captcha image base65 string
        private static string GenCaptchaImage(int width, int height, string codeStr)
        {
            using (Bitmap bmp = new Bitmap(width, height))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                // set backcolor
                g.Clear(Color.White);

                // 画干扰线条
                for (int i = 0; i < 20; i++)
                {
                    int x1 = _random.Next(width);
                    int y1 = _random.Next(height);
                    int x2 = _random.Next(width);
                    int y2 = _random.Next(height);
                    g.DrawLine(Pens.Orange, x1, y1, x2, y2);
                }

                // 画验证码
                int fontSize = height / 2;
                int x = 6;
                for (int i = 0; i < codeStr.Length; i++)
                {
                    System.Drawing.Font font = new System.Drawing.Font(new FontFamily("Arial"), fontSize, FontStyle.Bold | FontStyle.Italic);
                    Brush brush = new SolidBrush(Color.FromArgb(_random.Next(0, 180), _random.Next(0, 180), _random.Next(0, 180)));
                    g.DrawString(codeStr.Substring(i,1), font, brush, x, 6);
                    x += fontSize;
                }

                // 画干扰点
                for (int i = 0; i < 120; i++)
                {
                    int xx = _random.Next(width);
                    int yy = _random.Next(height);
                    Color color = Color.FromArgb(_random.Next(0, 255), _random.Next(0, 255), _random.Next(0, 255));
                    bmp.SetPixel(xx, yy, color);
                }

                string base64String = "";
                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Jpeg);
                    byte[] imageBytes = ms.ToArray();

                    base64String = Convert.ToBase64String(imageBytes);
                }

                return base64String;
            }
        }

        private static string GenRandomCaptcha(int length)
        {
            return new string(Enumerable.Repeat(CaptchaChars, length)
              .Select(s => s[_random.Next(s.Length)]).ToArray());
        }

        public static string genPsnCaptcha(int psnID)
        {
            string codeStr = CaptchaHelper.GenRandomCaptcha(6);
            string imgStr = CaptchaHelper.GenCaptchaImage(150, 45, codeStr);

            Bizcs.Model.psn_captcha newCapModel = new Bizcs.Model.psn_captcha();
            newCapModel.psnID = psnID;
            newCapModel.captchaStr = codeStr;
            newCapModel.createTime = DateTime.Now;
            new Bizcs.BLL.psn_captcha().Add(newCapModel);

            return imgStr;
        }

        public static string genAdminCaptcha(int adminID)
        {
            string codeStr = CaptchaHelper.GenRandomCaptcha(6);
            string imgStr = CaptchaHelper.GenCaptchaImage(150, 45, codeStr);

            Bizcs.Model.sys_captcha newCapModel = new Bizcs.Model.sys_captcha();
            newCapModel.adminID = adminID;
            newCapModel.captchaStr = codeStr;
            newCapModel.createTime = DateTime.Now;
            new Bizcs.BLL.sys_captcha().Add(newCapModel);

            return imgStr;
        }
    }
}
