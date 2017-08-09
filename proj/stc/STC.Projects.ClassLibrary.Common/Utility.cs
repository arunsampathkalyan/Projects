using STC.Projects.ClassLibrary.Common.Enums;
using STC.Projects.ClassLibrary.Common.Interfaces;
using STC.Projects.ClassLibrary.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace STC.Projects.ClassLibrary.Common
{
    public static class Utility
    {
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //:::                                                                         :::
        //:::  This routine calculates the distance between two points (given the     :::
        //:::  latitude/longitude of those points). It is being used to calculate     :::
        //:::  the distance between two locations using GeoDataSource(TM) products    :::
        //:::                                                                         :::
        //:::  Definitions:                                                           :::
        //:::    South latitudes are negative, east longitudes are positive           :::
        //:::                                                                         :::
        //:::  Passed to function:                                                    :::
        //:::    lat1, lon1 = Latitude and Longitude of point 1 (in decimal degrees)  :::
        //:::    lat2, lon2 = Latitude and Longitude of point 2 (in decimal degrees)  :::
        //:::    unit = the unit you desire for results                               :::
        //:::           where: 'M' is statute miles (default)                         :::
        //:::                  'K' is kilometers                                      :::
        //:::                  'N' is nautical miles                                  :::
        public static double CalculateDistance(double lat1, double lon1, double lat2, double lon2, char unit)
        {
            double theta = lon1 - lon2;
            double dist = Math.Sin(deg2rad(lat1)) * Math.Sin(deg2rad(lat2)) + Math.Cos(deg2rad(lat1)) * Math.Cos(deg2rad(lat2)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts decimal degrees to radians             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        //::  This function converts radians to decimal degrees             :::
        //:::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::::
        public static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        public static string ConvertXMLUsingXSLT(string xml, string xslT)
        {
            XslCompiledTransform myXslTransform;
            myXslTransform = new XslCompiledTransform();
            var xmlReader = new StringReader(xml);
            var xsltReader = new StringReader(xslT);
            var xslTLoader = XmlReader.Create(xsltReader);
            var xmlLoader = XmlReader.Create(xmlReader);
            StringBuilder outputXml = new StringBuilder();
            var xmlWriter = XmlWriter.Create(outputXml);
            myXslTransform.Load(xslTLoader);
            myXslTransform.Transform(xmlLoader, xmlWriter);
            if (outputXml != null)
                return outputXml.ToString();
            return "";
        }
        static public string SerializeXML(object details)
        {
            var type = details.GetType();
            if (type != null)
            {
                XmlSerializer serializer = new XmlSerializer(type);
                var stringwriter = new System.IO.StringWriter();
                serializer.Serialize(stringwriter, details);
                return stringwriter.ToString();
            }
            return "";
        }
        public static void RegisterEvent(object obj, object dest, string eventName, string methodName)
        {
            EventInfo reqEvent = obj.GetType().GetEvent(eventName);
            if (reqEvent == null)
                return;
            Type tDelegate = reqEvent.EventHandlerType;
            MethodInfo miHandler =
dest.GetType().GetMethod(methodName,
BindingFlags.NonPublic | BindingFlags.Instance);
            if (reqEvent != null && miHandler != null)
            {
                try
                {
                    Delegate d = Delegate.CreateDelegate(tDelegate, dest, miHandler);
                    reqEvent.AddEventHandler(obj, d);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void UnRegisterEvent(object obj, object dest, string eventName, string methodName)
        {
            EventInfo reqEvent = obj.GetType().GetEvent(eventName);
            if (reqEvent == null)
                return;
            Type tDelegate = reqEvent.EventHandlerType;
            MethodInfo miHandler =
dest.GetType().GetMethod(methodName,
BindingFlags.NonPublic | BindingFlags.Instance);
            if (reqEvent != null && miHandler != null)
            {
                try
                {
                    Delegate d = Delegate.CreateDelegate(tDelegate, dest, miHandler);
                    reqEvent.RemoveEventHandler(obj, d);
                }
                catch (Exception ex)
                {

                }
            }
        }

        public static void CallMethodUsingReflector(object obj, string messageName, object messageParameters)
        {
            try
            {
                var method = obj.GetType().GetMethod(messageName, new Type[] { messageParameters.GetType() });

                var func = method.Invoke(obj, new object[] { messageParameters });

            }
            catch (Exception ex)
            {

            }

        }
        public static object MapObjectsUsingXSLT(object oldObject, string xslt, string newObjectType, Assembly asm)
        {
            var xmlOld = SerializeXML(oldObject);
            if (xmlOld != "")
            {
                var newXML = ConvertXMLUsingXSLT(xmlOld, xslt);
                if (newXML != "")
                {
                    var newObject = DesrializeXML(newXML, newObjectType, asm, false);
                    return newObject;
                }
            }
            return null;
        }
        public static void RegisterMe(this UserControl Control, List<Type> Types)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                window.InlistSubscriber(Control, Types);
        }

        public static void Publish(this UserControl Control, Object Obj)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                window.Publish(Control, Obj);
        }

        public static void NavigateToPage(this UserControl Control, SystemPages page)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                window.NavigateToPage(page);
        }

        public static string GetCurrentUsername(this UserControl Control)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                return window.GetCurrentUsername();
            else
                return "";
        }
        public static Dictionary<string, int> GetCurrentUserFeatuers(this UserControl Control)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                return window.GetUserFeatures();
            else
                return new Dictionary<string, int>();
        }

        public static int GetCurrentUserId(this UserControl Control)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                return window.GetCurrentUserId();
            else
                return 0;
        }

        public static object GetLastUnHandledNotification(this UserControl Control)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                return window.GetLastUnHandledNotification();
            else
                return null;
        }
        public static void SetLastUnHandledNotification(this UserControl Control, object obj)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                window.SetLastUnHandledNotification(obj);
        }

        public static bool IsHomePage(this UserControl Control)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                return window.GetPageId() == (int)SystemPages.OperationTest;
            return false;
        }

        public static void RedirectToHome(this UserControl Control)
        {
            var window = (IParent)Window.GetWindow(Control);
            if (window != null)
                window.NavigateToPage(SystemPages.OperationTest);
        }

        public static string GetSignalRUrl()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["SignalRUrl"];
        }
        public static string GetLang()
        {
            return System.Configuration.ConfigurationSettings.AppSettings["Lang"];
        }

        public static void SetLang(string lang)
        {
            System.Configuration.ConfigurationSettings.AppSettings["Lang"] = lang;
        }
        public static object DesrializeXML(string XML, string MessageType, Assembly asm, bool needNameSpace = true)
        {
            var messagesNameSpace = "STC.Projects.ClassLibrary.ControlMessages";

            var type = GetTypeFromString(MessageType, asm, messagesNameSpace, needNameSpace);
            XmlSerializer serializer = new XmlSerializer(type);
            var stringwriter = new System.IO.StringReader(XML);
            var obj = serializer.Deserialize(stringwriter);
            return obj;
        }

        public static Type GetTypeFromString(string typeName, Assembly asm, string messagesNameSpace, bool needNameSpace = true)
        {
            var namespaceQualifiedTypeName = typeName;
            if (needNameSpace)
                namespaceQualifiedTypeName = string.Format("{0}.{1}", messagesNameSpace, typeName);
            Type type = asm.GetType(namespaceQualifiedTypeName);
            return type;
        }

        public static childItem FindVisualChild<childItem>(DependencyObject obj)
    where childItem : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        public static void WriteLog(string txt)
        {
            var mode = System.Configuration.ConfigurationSettings.AppSettings["Mode"];


            if (mode != null && mode == "Release")
            {
                return;
            }

            string m_exePath = System.Configuration.ConfigurationSettings.AppSettings["LogDefaultPath"];

            using (StreamWriter txtWriter = File.AppendText(m_exePath + "\\" + "log.txt"))
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine(txt);
                txtWriter.WriteLine("-------------------------------");
            }
        }
        public static void WriteLog(Exception ex)
        {
            if (ex == null)
                return;

            string m_exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            WriteLog(ex, m_exePath);
        }

        public static List<AccidentStandardDTO> ParseAccidentExcelToDTO(string excelFilePath, FileSourceTypes fileType)
        {
            try
            {
                try
                {
                    File.Copy(excelFilePath, @"D:\STC Data\" + "Copy - " + Path.GetFileName(excelFilePath));
                }
                catch (Exception ex)
                {

                }

                List<AccidentStandardDTO> lst = new List<AccidentStandardDTO>();

                OleDbDataAdapter adapter;

                OleDbConnection conn = new OleDbConnection(String.Format(@"Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" + excelFilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\""));
                conn.Open();
                var dt2 = conn.GetSchema("TABLES");

                if (dt2 != null && dt2.Rows.Count > 0 && dt2.Columns.Contains("TABLE_NAME"))
                {
                    adapter = new OleDbDataAdapter("SELECT * FROM [" + dt2.Rows[0].ItemArray[dt2.Columns.IndexOf("TABLE_NAME")] + "]", conn);

                    new OleDbCommandBuilder(adapter);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt != null & dt.Columns.Count > 0 && dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            AccidentStandardDTO incident = null;

                            if (fileType == FileSourceTypes.Accident)
                                incident = ParseAccidentRow(dt.Rows[i]);
                            else if (fileType == FileSourceTypes.ArabicFileAccident)
                                incident = ParseAccidentArabicRow(dt.Rows[i]);
                            else if (fileType == FileSourceTypes.PDOAccident)
                                incident = ParseAccidentPDORow(dt.Rows[i]);

                            if (incident != null)
                                lst.Add(incident);
                        }
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public static List<ManualViolationDTO> ParseViolationExcelToDTO(string excelFilePath, FileSourceTypes fileType)
        {
            try
            {
                List<ManualViolationDTO> lst = new List<ManualViolationDTO>();

                OleDbDataAdapter adapter;

                OleDbConnection conn = new OleDbConnection(String.Format(@"Provider=Microsoft.ACE.OLEDB.15.0;Data Source=" + excelFilePath + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES\""));
                conn.Open();
                var dt2 = conn.GetSchema("TABLES");

                if (dt2 != null && dt2.Rows.Count > 0 && dt2.Columns.Contains("TABLE_NAME"))
                {
                    adapter = new OleDbDataAdapter("SELECT * FROM [" + dt2.Rows[0].ItemArray[dt2.Columns.IndexOf("TABLE_NAME")] + "]", conn);

                    new OleDbCommandBuilder(adapter);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dt != null & dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            ManualViolationDTO violation = null;

                            if (fileType == FileSourceTypes.AbsenseViolation)
                                violation = ParseAbscentViolationRow(dt.Rows[i]);
                            else if (fileType == FileSourceTypes.PresentViolation)
                                violation = ParsePresentViolationRow(dt.Rows[i]);

                            if (violation != null)
                                lst.Add(violation);
                        }
                    }
                }

                return lst;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public static int GetIndexForExcelColumn(string colName, DataTable mapList)
        {
            try
            {
                if (mapList != null && colName != null)
                {
                    return mapList.Columns[colName].Ordinal;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public static AccidentStandardDTO ParseAccidentRow(DataRow row)
        {
            try
            {
                AccidentStandardDTO dto = new AccidentStandardDTO
                {
                    CreatedTime = MergeDateAndTime(row["Date"], row["Iac_Rep_Time"]),//
                    IncidentNo = GetString(row["Crash_Report"]),//
                    EmirateName = GetString(row["Emirate"]),//
                    StateName = GetString(row["Emirate"]),//
                    CityName = GetString(row["City"]),//
                    AreaName = GetString(row["Area"]),//
                    StatusName = "Inactive",
                    PoliceStationName = GetString(row["Police_Station"]),//
                    IncidentTypeName = GetString(row["Report_Type"]),//
                    CrashTypeName = GetString(row["Crash_Type"]),//
                    WeatherName = GetString(row["Weather"]),//
                    PConditionName = GetString(row["Pavement_Condition"]),//
                    RoadSpeed = GetDouble(row["Speed"]),//
                    LanesCount = GetInt(row["Lanes"]),//
                    AddressComment = GetString(row["Street"]),//
                    Longitude = GetDouble(row["Longitude"]),//
                    Latitude = GetDouble(row["Latitude"]),//
                    ServiceCallStep = ServiceCallEnum.New,//
                    CauseName = GetString(row["Causes"], 49),//
                    CrashSeverityName = GetString(row["Crash_Severity"]),//
                    LocationName = GetString(row["Location"]),//
                    LocationDescription = GetString(row["Location_Description"]),//
                    CrashDescription = GetString(row["Crash_Description1"]),//
                    LocationTypeName = GetString(row["Location_Type"]),//
                    IntersectionName = GetString(row["Intersection"]),//
                    LightingName = GetString(row["Lighting"]),//
                    SlightInjuriesCount = GetInt(row["Slight_injuries_no"]),//
                    MediumInjuriesCount = GetInt(row["Medium_injuries_no"]),//
                    SevereInjuriesCount = GetInt(row["Severe_injuries_no"]),//
                    FatalitiesCount = GetInt(row["Fatalities_no"]),//
                    TotalInjuriesFatalities = GetInt(row["Total_injuries_fatalities"]),//
                    ClassName = GetString(row["Time"]),//
                    Address = GetString(row["Street"]),
                    RoadTypeName = GetString(row["Road_Type"]),

                };

                return dto;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public static string PrepareDoublePoints(object expression)
        {
            try
            {
                string output = "0";

                if (expression == null)
                    return "0";

                if (expression.ToString().Length > 0)
                {
                    output = expression.ToString().Insert(2, ".");
                }

                return output;
            }
            catch
            {
                return "0";
            }
        }

        public static AccidentStandardDTO ParseAccidentArabicRow(DataRow row)
        {
            try
            {
                AccidentStandardDTO dto = new AccidentStandardDTO
                {
                    CreatedTime = GetDateTime(row["التاريخ"]),//
                    IncidentNo = GetString(row["رقم التقرير"]),//
                    EmirateName = "غير معروف",//
                    StateName = "غير معروف",//
                    CityName = GetString(row["المدينة#"]),//
                    AreaName = GetString(row["المنطقة"]),//
                    StatusName = "Inactive",
                    PoliceStationName = GetString(row["المراكز"]),//
                    IncidentTypeName = GetString(row["نوع التقرير"]),//
                    CrashTypeName = GetString(row["نوع الحادث"]),//
                    WeatherName = GetString(row["حالة الطقس"]),//
                    PConditionName = GetString(row["سطح الطريق"]),//
                    RoadSpeed = GetDouble(row["سرعة الطريق"]),//
                    LanesCount = GetInt(row["عدد المسارات"]),//
                    AddressComment = GetString(row["الشارع"]),//
                    Longitude = GetDouble(PrepareDoublePoints(row["Y-احداثيات"])),//
                    Latitude = GetDouble(PrepareDoublePoints(row["X-احداثيات"])),//
                    ServiceCallStep = ServiceCallEnum.New,//
                    CauseName = GetString(row["الاسباب"], 49),//
                    CrashSeverityName = "غير معروف",//
                    LocationName = GetString(row["المكان"]),//
                    LocationDescription = GetString(row["وصف الموقع"]),//
                    CrashDescription = "غير معروف",//
                    LocationTypeName = "غير معروف",//
                    IntersectionName = GetString(row["التقاطع"]),//
                    LightingName = GetString(row["الاضاءة"]),//
                    SlightInjuriesCount = GetInt(row["بسيطة"]),//
                    MediumInjuriesCount = GetInt(row["متوسطة"]),//
                    SevereInjuriesCount = GetInt(row["بليغة"]),//
                    FatalitiesCount = GetInt(row["وفاة"]),//
                    TotalInjuriesFatalities = GetInt(row["بسيطة"]) + GetInt(row["متوسطة"]) + GetInt(row["بليغة"]) + GetInt(row["وفاة"]),//
                    ClassName = GetString(row["الوقت"]),//
                    Address = GetString(row["الشارع"]),
                    RoadTypeName = "غير معروف",

                };

                return dto;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public static AccidentStandardDTO ParseAccidentPDORow(DataRow row)
        {
            try
            {
                AccidentStandardDTO dto = new AccidentStandardDTO
                {
                    CreatedTime = MergeDateAndTime(row["التاريخ"], row["Iac Rep Time"]),//
                    IncidentNo = GetString(row["رقم التقرير"]),//
                    EmirateName = GetString(row["الامارة"]),//
                    StateName = "غير معروف",//
                    CityName = "غير معروف",//
                    AreaName = "غير معروف",//
                    StatusName = "Inactive",
                    PoliceStationName = GetString(row["المراكز"]),//
                    IncidentTypeName = GetString(row["نوع التقرير"]),//
                    CrashTypeName = GetString(row["نوع الحادث"]),//
                    WeatherName = GetString(row["حالة الطقس"]),//
                    PConditionName = GetString(row["سطح الطريق"]),//
                    RoadSpeed = GetDouble(row["سرعة الطريق"]),//
                    LanesCount = GetInt(row["عدد المسارات"]),//
                    AddressComment = GetString(row["الشوارع"]),//
                    Longitude = 0,//
                    Latitude = 0,//
                    ServiceCallStep = ServiceCallEnum.New,//
                    CauseName = GetString(row["الاسباب"], 49),//
                    CrashSeverityName = "غير معروف",//
                    LocationName = GetString(row["المكان"]),//
                    LocationDescription = GetString(row["وصف الموقع"]),//
                    CrashDescription = GetString(row["وصف الحادث"]),//
                    LocationTypeName = "غير معروف",//
                    IntersectionName = GetString(row["التقاطع"]),//
                    LightingName = GetString(row["الاضاءة"]),//
                    SlightInjuriesCount = 0,//
                    MediumInjuriesCount = 0,//
                    SevereInjuriesCount = 0,//
                    FatalitiesCount = 0,//
                    TotalInjuriesFatalities = 0,//
                    ClassName = "غير معروف",//
                    Address = GetString(row["الشوارع"]),
                    RoadTypeName = "غير معروف",

                };

                return dto;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return null;
            }
        }

        public static ManualViolationDTO ParsePresentViolationRow(DataRow row)
        {
            ManualViolationDTO dto = new ManualViolationDTO
            {
                Age = GetInt(row["العمر"]),
                AgeClass = GetString(row["الفئة العمرية"]),
                City = GetString(row["االمدينة"]),
                Emirate = GetString(row["الإمارة"]),
                Gender = GetString(row["الجنس"]),
                Lat = GetDouble(row["احداثيات X"]),
                Lon = GetDouble(row["احداثيات Y"]),
                LevelOfEducation = GetString(row["المستوى التعليمي"]),
                LicenseIssueDate = GetDateTime(row["تارخ إصدار الرخصة"]),
                LicenseSource = GetString(row["مصدر الرخصة #"]),
                MaritalStatus = GetString(row["الحالة الاجتماعية"]),
                OwnerName = GetString(row["اسم السائق"]),
                OwnerTCFNumber = GetString(row["الرمز المروري للسائق"]),
                PlateKind = GetString(row["نوع اللوحة"]),
                PlateSource = GetString(row["مصدر اللوحة"]),
                RadarClass = GetString(row["فئة الردار"]),
                RadarType = GetString(row["نوع الرادار"]),
                Reason = GetString(row["مادة المخالفة"]),
                ReasonCode = GetInt(row["رمز المادة"]),
                RoadSpeed = GetInt(row["سرعة الطريق"]),
                StreetName = GetString(row["اسم الشارع"]),
                TCFNumber = GetString(row["الرمز المروري للمالك"]),
                Type = GetString(row["حالة المخالفة"]),
                VehicleType = GetString(row["صنف المركبة"]),
                ViolationDate = MergeDateAndTime(row["تاريخ المخالفة"], row["وقت المخالفة"]),
                ViolationNumber = GetString(row["رقم المخالفة"]),
                ViolationTime = MergeDateAndTime(row["تاريخ المخالفة"], row["وقت المخالفة"])
            };

            return dto;
        }

        public static ManualViolationDTO ParseAbscentViolationRow(DataRow row)
        {
            ManualViolationDTO dto = new ManualViolationDTO
            {
                Age = GetInt(row["العمر"]),
                AgeClass = GetString(row["الفئة العمرية"]),
                City = GetString(row["االمدينة"]),
                Emirate = GetString(row["الإمارة"]),
                Gender = GetString(row["الجنس"]),
                Lat = GetDouble(row["احداثيات X"]),
                Lon = GetDouble(row["احداثيات Y"]),
                LevelOfEducation = GetString(row["المستوى التعليمي"]),
                LicenseIssueDate = GetDateTime(row["تارخ إصدار الرخصة"]),
                LicenseSource = GetString(row["مصدر الرخصة #"]),
                MaritalStatus = GetString(row["الحالة الاجتماعية"]),
                OwnerName = GetString(row["اسم المالك"]),
                OwnerTCFNumber = GetString(row["الرمز المروري للمالك"]),
                PlateKind = GetString(row["نوع اللوحة"]),
                PlateSource = GetString(row["مصدر اللوحة"]),
                RadarClass = GetString(row["فئة الردار"]),
                RadarType = GetString(row["نوع الرادار"]),
                Reason = GetString(row["مادة المخالفة"]),
                ReasonCode = GetInt(row["رمز المادة"]),
                RoadSpeed = GetInt(row["سرعة الطريق"]),
                StreetName = GetString(row["اسم الشارع"]),
                TCFNumber = GetString(row["الرمز المروري للسائق"]),
                Type = GetString(row["حالة المخالفة"]),
                VehicleType = GetString(row["صنف المركبة"]),
                ViolationDate = MergeDateAndTime(row["تاريخ المخالفة"], row["وقت المخالفة"]),
                ViolationNumber = GetString(row["رقم المخالفة"]),
                ViolationTime = MergeDateAndTime(row["تاريخ المخالفة"], row["وقت المخالفة"])
            };

            return dto;
        }

        public static void WriteLog(Exception ex, string path)
        {
            try
            {
                using (StreamWriter txtWriter = File.AppendText(path + "\\" + "log.txt"))
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
            }
        }

        public static short GetShort(object expression)
        {
            short output = 0;

            if (expression != null)
            {
                short.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static int GetInt(object expression)
        {
            int output = 0;

            if (expression != null)
            {
                int.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static long GetLong(object expression)
        {
            long output = 0;

            if (expression != null)
            {
                long.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static decimal GetDecimal(object expression)
        {
            decimal output = 0;

            if (expression != null)
            {
                decimal.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static float GetFloat(object expression)
        {
            float output = 0;

            if (expression != null)
            {
                float.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static double GetDouble(object expression)
        {
            double output = 0;

            if (expression != null)
            {
                double.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static bool GetBoolean(object expression)
        {
            bool output = false;

            if (expression != null)
            {
                bool.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static DateTime GetDateTime(object expression)
        {
            DateTime output = new DateTime(1990, 1, 1, 1, 1, 1);

            if (expression != null && expression.ToString().Trim().Length > 0)
            {
                DateTime.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        public static DateTime MergeDateAndTime(object date, object time)
        {
            DateTime output = new DateTime(2000, 1, 1, 1, 1, 1);

            try
            {
                if (date == null || time == null)
                    return output;

                DateTime dateOutput;

                if (!DateTime.TryParse(date.ToString(), out dateOutput))
                    return output;

                var times = (time as System.DateTime?).Value.TimeOfDay;

                output = new DateTime(dateOutput.Year, dateOutput.Month, dateOutput.Day, times.Hours, times.Minutes, times.Seconds);

                return output;
            }
            catch (Exception ex)
            {
                return output;
            }
        }

        public static string GetString(object expression, int maxLength = 255)
        {
            string output = string.Empty;

            if (expression != null)
                output = expression.ToString();

            if (maxLength > 0 && output.Trim().Length > maxLength)
                output = output.Substring(0, maxLength);

            return output;
        }
        public static void LogOut()
        {
            var lstWindows = Application.Current.Windows;

            foreach (Window window in lstWindows)
            {
                if (window is IParent)
                {
                    (window as IParent).Logout();
                    break;
                }
            }
        }

        public static string GetErrorMessage()
        {
            string msg = string.Empty;
            var lang = Utility.GetLang();

            if (lang == "ar")
                msg = System.Configuration.ConfigurationSettings.AppSettings["ErrorMsgAr"];
            else
                msg = System.Configuration.ConfigurationSettings.AppSettings["ErrorMsgEn"];

            return msg;
        }

        public static List<int> GetRecentYearsList(int numOfYear)
        {
            List<int> recentYearList = new List<int>();

            for (int i = 0; i < numOfYear; i++)
            {
                recentYearList.Add(DateTime.Now.Year - i);
            }

            return recentYearList;
        }

        public static List<DateTime> GetNumberOfSpecificDaysInMonth(int year, int month, DayOfWeek dayName)
        {
            List<DateTime> lstOfSpecificDays = new List<DateTime>();
            int NumOfDaysThisMonth = DateTime.DaysInMonth(year, month);

            for (int i = 1; i <= NumOfDaysThisMonth; i++)
            {
                if (new DateTime(year, month, i).DayOfWeek == dayName)
                {
                    lstOfSpecificDays.Add(new DateTime(year, month, i));
                }
            }
            return lstOfSpecificDays;
        }

        public static List<DateTime> GetNumberOfDaysInMonth(int year, int month)
        {
            List<DateTime> lstOfDays = new List<DateTime>();
            int NumOfDaysThisMonth = DateTime.DaysInMonth(year, month);


            for (int i = 1; i <= NumOfDaysThisMonth; i++)
            {
                lstOfDays.Add(new DateTime(year, month, i));

            }
            return lstOfDays;
        }

        public static Dictionary<string, List<DateTime>> GetNumberOfWeeksAndDays(int year, int month, DayOfWeek dayName)
        {

            Dictionary<string, List<DateTime>> lstOfSpecificWeeksAndDays = new Dictionary<string, List<DateTime>>();
            int NumOfDaysThisMonth = DateTime.DaysInMonth(year, month);
            string weekStartDate = string.Empty;
            for (int i = 1; i <= NumOfDaysThisMonth; i++)
            {
                DateTime currDate = new DateTime(year, month, i);
                if (currDate.DayOfWeek == dayName)
                {
                    weekStartDate = currDate.DayOfWeek + " - " + currDate.Date.ToString("dd MMM");
                    lstOfSpecificWeeksAndDays.Add(weekStartDate, new List<DateTime>() { new DateTime(year, month, i) });

                }
                else if (!string.IsNullOrEmpty(weekStartDate))
                {
                    List<DateTime> lstDateTemp = new List<DateTime>();
                    lstOfSpecificWeeksAndDays.TryGetValue(weekStartDate, out lstDateTemp);
                    lstDateTemp.Add(currDate);
                    lstOfSpecificWeeksAndDays[weekStartDate] = lstDateTemp;
                }
            }
            return lstOfSpecificWeeksAndDays;

        }

    }
}
