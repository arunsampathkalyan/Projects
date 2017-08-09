using Oracle.ManagedDataAccess.Client;
using STC.Projects.ClassLibrary.Common;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Data;

namespace STC.Projects.ClassLibrary.DAL
{
    public class CommunicationDAL
    {
        string oradb;
        public CommunicationDAL(string connectionString)
        {
            oradb = connectionString;
        }

        public bool SendSMS(SMSDTO message)
        {
            OracleConnection conn = new OracleConnection(oradb);

            string language = string.Empty;
            string languageLetter = string.Empty;

            if (message.Language == SMSLanguage.Arabic)
            {
                language = "SMS_MESSAGE_A";
                languageLetter = "A";
            }
            else
            {
                language = "SMS_MESSAGE_E";
                languageLetter = "E";
            }

            conn.Open();
            OracleCommand cmd = new OracleCommand();
            cmd.Connection = conn;
            cmd.CommandText = "INSERT INTO SMS_INTERFACE (MOBILE_NO," + language + ", SENDERID,SMS_LANG) VALUES(" + message.RecipientNumber + ",N'" + message.MessageBody + "','2259','" + languageLetter + "')";
            cmd.CommandType = CommandType.Text;
            OracleDataReader dr = cmd.ExecuteReader();
            dr.Read();
            conn.Close();

            return doCommit();
        }

        private bool doCommit()
        {
            try
            {
                OracleConnection conn = new OracleConnection(oradb);

                conn.Open();
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = "commit";
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();

                conn.Close();

                return true;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
        }
    }
}
