using System.Data;
using System.Text.RegularExpressions;
using appsin.Bizcs;
using appsin.ApiModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace appsin.Common
{
    public class VerifyHelper
    {
        public static string genTokenStr(int psnID)
        {
            DateTime expireTime = DateTime.Now.AddMinutes(60);//过期时间
            string expireStr = expireTime.ToFileTime().ToString();

            Random rd = new Random();

            Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
            Bizcs.Model.sys_tokenMain tokenModel = new Bizcs.Model.sys_tokenMain();
            tokenModel.appID = 10000;
            tokenModel.psnID = psnID;
            tokenModel.expireTime = expireTime;
            tokenModel.createTime = DateTime.Now;
            tokenModel.tokenStatus = 1;
            int tokenID = tokenBll.Add(tokenModel);

            string tokenRealStr = "core@" + tokenID + rd.Next(11, 99) + "@" + psnID.ToString() + rd.Next(11, 99) + "@" + expireStr;

            string userToken = CryptAES.Encrypt(tokenRealStr);

            tokenModel.tokenStr = userToken;
            tokenModel.tokenID = tokenID;

            tokenBll.Update(tokenModel);

            return userToken;
        }

        public static string updateTokenStr(string userToken)
        {
            if (userToken == "" || userToken == "undefined")
            {
                return "";
            }
            else
            {
                string tokenRealStr = CryptAES.Decrypt(userToken);

                string[] tokenList = tokenRealStr.Split('@');

                if (tokenList.Length == 4)
                {
                    int tokenID = int.Parse(tokenList[1].Substring(0, tokenList[1].Length - 2));

                    Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
                    Bizcs.Model.sys_tokenMain tokenModel = tokenBll.GetModel(tokenID);
                    int psnID = int.Parse(tokenList[2].Substring(0, tokenList[2].Length - 2));

                    if (tokenModel.tokenStatus == 1)
                    {
                        if (psnID == tokenModel.psnID)
                        {
                            Random rd = new Random();
                            DateTime expireTime = DateTime.Now.AddMinutes(60);//过期时间
                            string expireStr = expireTime.ToFileTime().ToString();
                            string newTokenRealStr = "core@" + tokenID + rd.Next(11, 99) + "@" + psnID.ToString() + rd.Next(11, 99) + "@" + expireStr;

                            string newUserToken = CryptAES.Encrypt(tokenRealStr);

                            tokenModel.expireTime = DateTime.Now.AddMinutes(60);
                            tokenModel.tokenStr = newUserToken;

                            bool iss = tokenBll.Update(tokenModel);


                            return newUserToken;

                        }
                        else
                        {
                            return "";
                        }
                    }
                    else
                    {
                        return "";
                    }

                }
                else
                {
                    return "";
                }
            }

        }

        public static int getPsnID(string userToken)
        {
            if (userToken == "" || userToken == "undefined")
            {
                return 0;
            }
            else
            {
                string tokenRealStr = CryptAES.Decrypt(userToken);

                string[] tokenList = tokenRealStr.Split('@');

                if (tokenList.Length == 4)
                {
                    int tokenID = int.Parse(tokenList[1].Substring(0, tokenList[1].Length - 2));

                    Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
                    Bizcs.Model.sys_tokenMain tokenModel = tokenBll.GetModel(tokenID);
                    int psnID = int.Parse(tokenList[2].Substring(0, tokenList[2].Length - 2));

                    if (tokenModel != null && tokenModel.tokenStatus == 1)
                    {
                        if (psnID == tokenModel.psnID)
                        {
                            DateTime expireTime = Convert.ToDateTime(tokenModel.expireTime);

                            if (expireTime >= DateTime.Now)
                            {
                                return psnID;
                            }
                            else
                            {
                                tokenModel.tokenStr = userToken;
                                tokenModel.tokenStatus = -1;
                                tokenModel.cancelTime = DateTime.Now;
                                bool iss = tokenBll.Update(tokenModel);
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public static int cancelToken(string userToken)
        {
            if (userToken == "")
            {
                return 0;
            }
            else if (userToken == "undefined")
            {
                return 0;
            }
            else
            {
                string tokenRealStr = CryptAES.Decrypt(userToken);

                string[] tokenList = tokenRealStr.Split('@');

                if (tokenList.Length == 4)
                {
                    int tokenID = int.Parse(tokenList[1].Substring(0, tokenList[1].Length - 2));

                    Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
                    Bizcs.Model.sys_tokenMain tokenModel = tokenBll.GetModel(tokenID);
                    int psnID = int.Parse(tokenList[2].Substring(0, tokenList[2].Length - 2));

                    if (psnID == tokenModel.psnID)
                    {
                        tokenModel.tokenStr = userToken;
                        tokenModel.tokenStatus = -1;
                        tokenModel.cancelTime = DateTime.Now;
                        bool iss = tokenBll.Update(tokenModel);

                        if (iss)
                        {
                            return 1;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public static int getAdminID(string adminToken)
        {
            if (adminToken == "" || adminToken == "undefined")
            {
                return 0;
            }
            else
            {
                string tokenRealStr = CryptAES.Decrypt(adminToken);

                string[] tokenList = tokenRealStr.Split('@');

                if (tokenList.Length == 4)
                {
                    int tokenID = int.Parse(tokenList[1].Substring(0, tokenList[1].Length - 2));

                    Bizcs.BLL.sys_tokenMain tokenBll = new Bizcs.BLL.sys_tokenMain();
                    Bizcs.Model.sys_tokenMain tokenModel = tokenBll.GetModel(tokenID);
                    int psnID = int.Parse(tokenList[2].Substring(0, tokenList[2].Length - 2));

                    if (tokenModel != null && tokenModel.tokenStatus == 1)
                    {
                        if (psnID == tokenModel.psnID)
                        {
                            DateTime expireTime = Convert.ToDateTime(tokenModel.expireTime);

                            if (expireTime >= DateTime.Now)
                            {
                                return psnID;
                            }
                            else
                            {
                                tokenModel.tokenStr = adminToken;
                                tokenModel.tokenStatus = -1;
                                tokenModel.cancelTime = DateTime.Now;
                                bool iss = tokenBll.Update(tokenModel);
                                return -1;
                            }
                        }
                        else
                        {
                            return -2;
                        }
                    }
                    else
                    {
                        return -3;
                    }
                }
                else
                {
                    return 0;
                }
            }
        }

        public static bool checkPwd(string pwd)
        {
            var reg = @"(?=.*[0-9])(?=.*[A-Z])(?=.[a-z])(?=.*[^a-zA-Z0-9]).{8,30}";
            if (Regex.IsMatch(pwd, reg))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool checkPhone(string phone)
        {
            var reg = @"(^[0-9]{3,4}\-[0-9]{5,8}$)|(^[0-9]{5,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(0?(13|14|15|16|17|18|19)[0-9]{9})";
            if (Regex.IsMatch(phone, reg))
            {
                return false;
            }
            return true;
        }

        private static char[] constant ={ '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
        public static string genRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        public static bool checkAuKey(string auKey)
        {
            if (auKey.Length > 8)
            {
                string appSID = auKey.Substring(0, 8);
                string encryedKey = auKey.Substring(8, auKey.Length - 8);
                Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
                Bizcs.Model.app_appMain appModel = appBll.GetModelBySID(appSID);
                if (appModel != null)
                {
                    string pubkey = appModel.appSkey;
                    Bizcs.BLL.sys_RSAKey rsaBll = new Bizcs.BLL.sys_RSAKey();
                    Bizcs.Model.sys_RSAKey rsaModel = rsaBll.GetModelByPubkey(pubkey);
                    if (rsaModel != null)
                    {
                        string prikey = rsaModel.nkey;
                        string keystr = CryptRSA.RsaHelper.Decrypt(prikey, encryedKey);
                        string[] args = keystr.Split(';');
                        DateTime args2 = ConvertSecondsToDateTime(args[2]);
                        if (appSID == args[0] && appModel.appSecret == args[1] 
                            && (DateTime.Now.ToString("yyyyMMddhh") == args2.ToString("yyyyMMddhh") 
                            || DateTime.Now.AddHours(-1).ToString("yyyyMMddhh") == args2.ToString("yyyyMMddhh")))//The hours need to consider cross hours request ,so the previous hour is also fine.
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else { return false; }

        }

        public static bool checkApiAccess(string apiCode, servReqArgs reqArgs)
        {
            Bizcs.Model.api_useLog useModel = new Bizcs.Model.api_useLog();

            string appSID = reqArgs.aukey.Substring(0, 8);
            Bizcs.BLL.app_appMain appBll = new Bizcs.BLL.app_appMain();
            Bizcs.Model.app_appMain appModel = appBll.GetModelBySID(appSID);
            if (appModel != null)
            {
                Bizcs.BLL.api_apiOsrz osrzBll = new Bizcs.BLL.api_apiOsrz();
                Bizcs.Model.api_apiOsrz osrzModel = osrzBll.GetModelByCode(apiCode, appSID);

                useModel.appID = appModel.appID;
                useModel.createTime = DateTime.Now;
                useModel.inPara = reqArgs.args;

                if (osrzModel != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                useModel.isS = "0";
                new Bizcs.BLL.api_useLog().Add(useModel);
                return false;
            }
        }

        public static int getApiID(string apiCode)
        {
            Bizcs.Model.api_apiMain apiModel = new Bizcs.BLL.api_apiMain().GetModelByApiCode(apiCode);
            if (apiModel != null)
            { return apiModel.apiID; }
            else { return 0; }
        }

        public static int getAppID(string auKey)
        {
            if (VerifyHelper.IsConvertToInt((auKey.Substring(0, 8))))
            {
                Bizcs.Model.app_appMain appModel = new Bizcs.BLL.app_appMain().GetModelBySID(auKey.Substring(0, 8));
                if (appModel != null)
                { return appModel.appID; }
                else { return 0; }
            }
            else
            { return -1; }
        }

        public static int getOsrzID(int apiID, int appID)
        {
            Bizcs.Model.api_apiOsrz osrzModel = new Bizcs.BLL.api_apiOsrz().GetModelByID(appID, apiID);
            if (osrzModel != null)
            { return osrzModel.osrzID; }
            else { return 0; }
        }

        public static bool IsConvertToInt(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;
            string trimmedInput = input.Trim();
            return int.TryParse(trimmedInput, out _);
        }

        public static bool isUrl(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            // 用 UriKind.Absolute 强制必须是绝对路径
            if (!Uri.TryCreate(text, UriKind.Absolute, out var uri))
                return false;

            // 只认 http/https
            return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
        }

        public static DateTime ConvertSecondsToDateTime(string timestampSecondsStr)
        {
            if (string.IsNullOrWhiteSpace(timestampSecondsStr))
            {
                throw new ArgumentException("Argument is null or empty!", nameof(timestampSecondsStr));
            }
            if (!long.TryParse(timestampSecondsStr, out long timestampSeconds))
            {
                throw new ArgumentException("Wrong time format!", nameof(timestampSecondsStr));
            }
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            DateTime utcTime = startTime.AddSeconds(timestampSeconds);
            return utcTime.ToLocalTime();
        }

        #region safty detection
        // Regular expression patterns for SQL injection detection
        /* 1. Union-based injection */
        private static readonly Regex Union = new Regex(@"\bunion\s+(all\s+)?select\b", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /* 2. DDL whole-word match */
        private static readonly Regex DDL = new Regex(
            @"\b(drop|alter|truncate|create)\s+(table|database|view|index|proc|procedure|function|trigger|schema)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /* 3. DML matches only “keyword + space + table name (non-adjective)” */
        private static readonly Regex DML = new Regex(
            @"\b(delete|insert|update|grant|revoke)\s+[a-z][a-z0-9_]*\b(?!['""])",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /* 4. Dangerous extended stored procedure names */
        private static readonly Regex Xp = new Regex(
            @"\bxp_(cmdshell|regread|regwrite|loginconfig|enumgroups|fileexist|fixeddrives|dirtree|enumerrorlogs|getfiledetails|makecab|readerrorlog|sprintf|sqlmaint|sscanf|regdeletekey|regdeletevalue|regenumkeys|regenumvalues|servicecontrol|terminate_process)\b",
            RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static bool IsDangerous(string input) =>
            !string.IsNullOrWhiteSpace(input) &&
            (Union.IsMatch(input) || DDL.IsMatch(input) || DML.IsMatch(input) || Xp.IsMatch(input));

        // Regular expression pattern for XSS attack detection
        private static readonly Regex XssPattern = new Regex(
            @"<script\b[^>]*>.*?</script\s*>|" +
            @"<\s*(iframe|object|embed|form|input|button|svg|math)\b[^>]*>.*?</\s*\1\s*>|" +
            @"<\s*[^>]*?\b(" +
            @"javascript:|on\w+\s*=|data:\s*text/html|style\s*=\s*[^'""]*expression\(" +
            @")\b[^>]*>",
            RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.Singleline
        );

        /// <summary>
        /// Checks whether the input string contains dangerous characters indicative of SQL injection
        /// </summary>
        /// <param name="input">The string to be inspected</param>
        /// <returns>true if dangerous characters are detected; otherwise, false</returns>
        public static bool ContainsSqlInjectionRisk(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return IsDangerous(input);
        }

        /// <summary>
        /// Checks whether the input string contains XSS attack scripts
        /// </summary>
        /// <param name="input">The string to be inspected</param>
        /// <returns>true if attack scripts are detected; otherwise, false</returns>
        public static bool ContainsXssRisk(string input)
        {
            if (string.IsNullOrEmpty(input))
                return false;

            return XssPattern.IsMatch(input);
        }

        /// <summary>
        /// Determines whether the input string is free of both SQL-injection and XSS risks
        /// </summary>
        /// <param name="input">The string to be inspected</param>
        /// <returns>true if the input is safe; false if any risk is found</returns>
        public static bool isSafe(string input)
        {
            return !(ContainsSqlInjectionRisk(input) || ContainsXssRisk(input));
        }

        /// <summary>
        /// Inspects the input string and returns the specific type of security risk detected
        /// </summary>
        /// <param name="input">The string to be inspected</param>
        /// <returns>A description of the risk type, or an empty string if no risk is found</returns>
        public static string GetSecurityRiskType(string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            bool hasSqlRisk = ContainsSqlInjectionRisk(input);
            bool hasXssRisk = ContainsXssRisk(input);

            if (hasSqlRisk && hasXssRisk)
                return "Contains both SQL-injection and XSS risks";
            if (hasSqlRisk)
                return "Contains SQL-injection risk";
            if (hasXssRisk)
                return "Contains XSS risk";

            return string.Empty;
        }

        #endregion
    }
}
