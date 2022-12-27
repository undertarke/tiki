using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Linq;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;

namespace SoloDevApp.Service.Utilities
{
    public class FuncUtilities
    {
        public static DateTime GetDateCurrent()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            DateTime d = new DateTime();
            if (date != "")
            {
                d = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                d = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            return d;
        }
        public static DateTime GetDateTimeCurrent()
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            DateTime d = new DateTime();
            if (date != "")
            {
                d = DateTime.ParseExact(date, "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                d = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), "dd/MM/yyyy hh:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            return d;
        }
        public static DateTime ConvertStringToDateTime(string date = "")
        {
            DateTime d = new DateTime();
            if (date.Split('-').Count() > 1)
            {
                if (!string.IsNullOrEmpty(date))
                {
                    d = DateTime.ParseExact(date, "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    d = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss"), "dd-MM-yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                }
                return d;
            }
            if (!string.IsNullOrEmpty(date))
            {
                d = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                d = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            }
            return d;
        }
        public static DateTime ConvertStringToDate(string date = "")
        {
            DateTime d = new DateTime();
            if (date.Split('-').Count() > 1)
            {
                if (!string.IsNullOrEmpty(date))
                {
                    d = DateTime.ParseExact(date, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                else
                {
                    d = DateTime.ParseExact(DateTime.Now.ToString("dd-MM-yyyy"), "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);
                }
                return d;
            }
            if (!string.IsNullOrEmpty(date))
            {
                d = DateTime.ParseExact(date, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            else
            {
                d = DateTime.ParseExact(DateTime.Now.ToString("dd/MM/yyyy"), "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
            }
            return d;
        }
        public static string ConvertDateToString(DateTime date)
        {
            string dateString = "";
            if (date != null)
                dateString = date.ToString();
            return dateString;
        }
        public static DateTime ConvertToTimeStamp(int unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static string BestLower(string input = "",bool checkVideo=false)
        {
            if (input == null)
            {
                return "";
            }
            input = input.Trim();
            if (checkVideo)
            {
                for (int i = 0x20; i < 0x30; i++)
                {
                    input = input.Replace(((char)i).ToString(), " ");
                }
            }
              
            input = input.Replace(" ", "-");
            input = input.Replace(",", "-");
            input = input.Replace(";", "-");
            input = input.Replace(":", "-");
            input = input.Replace("  ", "-");
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");
            string str = input.Normalize(NormalizationForm.FormD);
            string str2 = regex.Replace(str, string.Empty).Replace('đ', 'd').Replace('Đ', 'D');
            if (checkVideo)
            {
                str2=str2.Replace("-mp4", ".mp4");
            }
            while (str2.IndexOf("?") >= 0)
            {
                str2 = str2.Remove(str2.IndexOf("?"), 1);
            }
            while (str2.Contains("--"))
            {
                str2 = str2.Replace("--", "-").ToLower();
            }

            return str2.ToLower();
        }

        public static bool ReCaptchaPassed(string secret, string gRecaptchaResponse)
        {
            HttpClient httpClient = new HttpClient();
            var res = httpClient.GetAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={gRecaptchaResponse}").Result;
            if (res.StatusCode != HttpStatusCode.OK)
                return false;

            string JSONres = res.Content.ReadAsStringAsync().Result;
            dynamic JSONdata = JObject.Parse(JSONres);
            if (JSONdata.success != "true")
                return false;

            return true;
        }

        public static int TinhKhoangCachNgay(DateTime date)
        {
            DateTime dateNow = GetDateCurrent();
            TimeSpan ts = date - dateNow;
            return ts.Days;
        }

        public static string RandomString(int size, bool lowerCase = false)
        {
            Random _random = new Random();
            var builder = new StringBuilder(size);

            // Unicode/ASCII Letters are divided into two blocks
            // (Letters 65–90 / 97–122):   
            // The first group containing the uppercase letters and
            // the second group containing the lowercase.  

            // char is a single Unicode character  
            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26; // A...Z or a..z: length = 26  

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }

        public static string CheckToken (string accessToken,bool checkQuyen)
        {

            try
            {
                JwtSecurityToken token = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);
                string hetHanTime = token.Claims.FirstOrDefault(c => c.Type == "exp").Value;
                string sId = token.Claims.FirstOrDefault(c => c.Type == "id").Value;

                DateTime dNow = DateTime.Now;
                DateTime dSince = new DateTime(1970, 01, 01);
                dSince = dNow.Date.AddMilliseconds(double.Parse(hetHanTime + "000"));
                if (dSince <= dNow)
                {
                    return "0";
                }

                if (checkQuyen)
                {
                    string sRole = token.Claims.FirstOrDefault(c => c.Type == "role").Value;
                    return sRole;
                }

                return sId;
            }
            catch (Exception ex)
            {
                return "0";
            }
        }

        public static string TokenMessage(string value, bool checkQuyen)
        {

            if (value == "0")
                return "token user hết hạn hoặc không đúng";
            if (checkQuyen)
            {
                if (value != "ADMIN" && value != "SPADMIN")
                    return "User không phải quyền admin";
            }
           
            return "";

        }
    }
}