using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using STC.Projects.WPFControlLibrary.SOPBox.SmartTowerServiceReference;

namespace STC.Projects.WPFControlLibrary.SOPBox.Helper
{
    public class SmartTowerDAL
    {
        //private SmartTowerEntities dataContext = new SmartTowerEntities();

        //private string connectionString =@"data source=RMG-Alliance-SE\SQLSYMON;initial catalog=AllianceDB;persist security info=True;user id=rmg;password=ATS2015;";

        private string connectionString = @"data source=stc-connect.cloudapp.net;initial catalog=AD-SmartTowers;persist security info=True;user id=sa;password=P@ssw0rd32;";

        public List<TowerPredefinedMessageDTO> GetAllTowerStaticMessages(string TowerId)
        {
            var con = new SqlConnection(connectionString);

            try {
                
                con.Open();

                string strSql = "select * from traffic_static where location='" + TowerId + "'";

                SqlDataAdapter dadapter = new SqlDataAdapter {
                    SelectCommand = new SqlCommand(strSql, con)
                };

                DataSet dset = new DataSet();
                dadapter.Fill(dset);
                


                var table = dset.Tables[0];

                return (from DataRow dataRow in table.Rows
                        select new TowerPredefinedMessageDTO {
                            Location = dataRow["location"].ToString(),
                            MessageDescription = dataRow["MessageDescription"].ToString(),
                            MessageId = dataRow["MessageID"].ToString(),
                            MyLastUpdate = DateTime.Now,
                            NotificationEnabled = true
                        }).ToList();
            }
            catch (Exception e) {

                return null;
            }
            finally {
                con.Close();
            }
        }

        public bool UpdateTowerCurrentMessage(TowerMessageDTO TowerMessage)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try {

                connection.Open();

                SqlCommand myCommand1 = new SqlCommand("UPDATE traffic_static SET NotificationEnabled = '0' " + "WHERE location = @location", connection);

                myCommand1.Parameters.AddWithValue("@location", TowerMessage.TowerId);

                var count1 = myCommand1.ExecuteNonQuery();


                SqlCommand myCommand2 = new SqlCommand("UPDATE traffic_static SET NotificationEnabled = '1' " + "WHERE MessageID =@MessageID And location = @location", connection);

                myCommand2.Parameters.AddWithValue("@location", TowerMessage.TowerId);
                myCommand2.Parameters.AddWithValue("@MessageID", TowerMessage.MessageId);


                var count2 = myCommand2.ExecuteNonQuery();

                return count2 > 0;
            }
            catch (Exception) {
                return false;
            }
            finally {
                connection.Close();
            }
        }

        public TowerMessageDTO GetTowerCurrentMessage(string TowerId)
        {

            var con = new SqlConnection(connectionString);
            try
            {

                con.Open();

                string strSql = "select * from traffic_static where location='" + TowerId + "' And NotificationEnabled = '1'";

                SqlDataAdapter dadapter = new SqlDataAdapter {
                    SelectCommand = new SqlCommand(strSql, con)
                };

                DataSet dset = new DataSet();
                dadapter.Fill(dset);
                con.Close();


                var table = dset.Tables[0];

                TowerMessageDTO returnMsg = new TowerMessageDTO();

                foreach (DataRow dataRow in table.Rows)
                {
                    returnMsg = new TowerMessageDTO
                    {
                        TowerId = dataRow["location"].ToString(),
                        ArabicMessage = dataRow["MessageDescription"].ToString(),
                        MixedMessage = dataRow["MessageDescription"].ToString(),
                        EnglishMessage = dataRow["MessageDescription"].ToString(),
                        IncidentImage = "",
                        MessageId = dataRow["MessageID"].ToString(),
                        MessageType = 1,
                        MyLastUpdate = DateTime.Now,
                        EnableNotification = true
                    };

                }

                return returnMsg;
            }
            catch (Exception e)
            {

                return null;
            }
            finally
            {
                con.Close();
            }

        }

        public SensorReadingDTO GetTowerSensorCurrentReading(string SensorId)
        {

            var con = new SqlConnection(connectionString);
            try
            {

                con.Open();

                string strSql = "select TOP 1 CONVERT(DATETIME, CONVERT(NCHAR(10), [Date], 112) + ' ' + CONVERT(NCHAR(10), [Time], 108)) as ReadingDate , * from SWS200Table where SensorID='" + SensorId + "' ORDER BY ReadingDate DESC";

                SqlDataAdapter dadapter = new SqlDataAdapter
                {
                    SelectCommand = new SqlCommand(strSql, con)
                };

                DataSet dset = new DataSet();
                dadapter.Fill(dset);
                con.Close();


                var table = dset.Tables[0];

                SensorReadingDTO returnMsg = new SensorReadingDTO();

                foreach (DataRow dataRow in table.Rows)
                {
                    returnMsg = new SensorReadingDTO
                    {
                        Code = dataRow["Code"].ToString(),
                        Date = dataRow["Date"].ToString(),
                        Header = dataRow["Header"].ToString(),
                        InstantMOR = dataRow["InstantMOR"].ToString(),
                        MOR = dataRow["MOR"].ToString(),
                        Period = dataRow["Period"].ToString(),
                        Precipitation = dataRow["Precipitation"].ToString(),
                        SelfTest = dataRow["SelfTest"].ToString(),
                        SensorID = dataRow["SensorID"].ToString(),
                        Temperature = dataRow["Temperature"].ToString(),
                        Time = dataRow["Time"].ToString()
                    };

                }

                return returnMsg;
            }
            catch (Exception e)
            {

                return null;
            }
            finally
            {
                con.Close();
            }

        }
    }
}
