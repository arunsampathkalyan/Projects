using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace STC.Projects.WCF.ServiceLayer
{
    public static class Utility
    {
        public static string GetUTSUserId()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["UTSUserId"].ToString() == null)
                return "";

            return System.Configuration.ConfigurationSettings.AppSettings["UTSUserId"].ToString();
        }

        public static string GetUTSUserName()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["UTSUsername"] == null)
                return "";

            return System.Configuration.ConfigurationSettings.AppSettings["UTSUsername"].ToString();
        }

        public static string GetTemplatePath()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["TemplatesPath"] == null)
                return "";

            return System.Configuration.ConfigurationSettings.AppSettings["TemplatesPath"].ToString();
        }

        public static string GetUTSPassword()
        {
            if (System.Configuration.ConfigurationSettings.AppSettings["UTSPassword"] == null)
                return "";

            return System.Configuration.ConfigurationSettings.AppSettings["UTSPassword"].ToString();
        }

        public static int GetUTSSystemCode()
        {
            int x = 0;

            if (System.Configuration.ConfigurationSettings.AppSettings["UTSSystemCode"] != null)
            {
                int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["UTSSystemCode"], out x);
            }

            return x;
        }

        public static int GetNotificationPendingPeriod()
        {
            int oldPeriod = 24;
            int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["OldNotificationInHours"].ToString(), out oldPeriod);
            return oldPeriod;
        }
        public static string GetExecutionPath()
        {
            return  Path.GetDirectoryName(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath);
        }
        public static void WriteLog(string txt)
        {
            var mode = System.Configuration.ConfigurationSettings.AppSettings["Mode"];

            if (mode != null && mode == "Release")
            {
                return;
            }

            string m_exePath = Path.GetDirectoryName(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath);

            using (StreamWriter txtWriter = File.AppendText(m_exePath + "\\" + "log.txt"))
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine(txt);
                txtWriter.WriteLine("-------------------------------");
            }
        }

        public static void WriteErrorLog(Exception ex)
        {
            if (ex == null)
                return;

            string m_exePath = Path.GetDirectoryName(System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath);
            WriteErrorLog(ex, m_exePath);
        }

        public static void WriteErrorLog(Exception ex, string path)
        {
            try
            {
                using (StreamWriter txtWriter = File.AppendText(path + "\\" + "Errorlog.txt"))
                {
                    txtWriter.Write("\r\nLog Entry : ");
                    txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                    txtWriter.WriteLine("  :{0}", ex.Message);
                    txtWriter.WriteLine(ex.StackTrace);
                    txtWriter.WriteLine("-------------------------------");
                    if (ex.InnerException != null)
                    {
                        txtWriter.WriteLine(" Inner Exception:");
                        txtWriter.WriteLine(ex.InnerException.Message);
                        txtWriter.WriteLine(ex.InnerException.StackTrace);
                        txtWriter.WriteLine("-------------------------------");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}