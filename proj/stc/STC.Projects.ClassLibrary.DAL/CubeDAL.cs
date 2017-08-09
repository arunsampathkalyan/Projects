using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using STC.Projects.ClassLibrary.DTO;
using STC.Projects.ClassLibrary.DAL.Utilities;
using SimpleImpersonation;
using System.Collections.ObjectModel;
using System.Windows.Media;
using STC.Projects.ClassLibrary.Common;
using System.Data.SqlClient;

namespace STC.Projects.ClassLibrary.DAL
{
    public class CubeDAL
    {
        #region Output Preparation

        private string _AnalysisConnectionString = string.Empty;

        public CubeDAL(string AnalysisConnectionString)
        {
            _AnalysisConnectionString = AnalysisConnectionString;
        }

        public string FormatText(string text)
        {
            switch (text.Trim())
            {
                case "Day 1":
                    text = "Sun";
                    break;
                case "Day 2":
                    text = "Mon";
                    break;
                case "Day 3":
                    text = "Tue";
                    break;
                case "Day 4":
                    text = "Wed";
                    break;
                case "Day 5":
                    text = "Thu";
                    break;
                case "Day 6":
                    text = "Fri";
                    break;
                case "Day 7":
                    text = "Sat";
                    break;

                case "Month 1":
                    text = "Jan";
                    break;
                case "Month 2":
                    text = "Feb";
                    break;
                case "Month 3":
                    text = "Mar";
                    break;
                case "Month 4":
                    text = "Apr";
                    break;
                case "Month 5":
                    text = "May";
                    break;
                case "Month 6":
                    text = "Jun";
                    break;
                case "Month 7":
                    text = "Jul";
                    break;
                case "Month 8":
                    text = "Aug";
                    break;
                case "Month 9":
                    text = "Sep";
                    break;
                case "Month 10":
                    text = "Oct";
                    break;
                case "Month 11":
                    text = "Nov";
                    break;
                case "Month 12":
                    text = "Dec";
                    break;
                default:
                    break;
            }
            text = text.Replace("All", "Total");
            text = text.Replace("Week", "");
            text = text.Replace("Calendar", "");

            return text;
        }

        public int GetAutomaticReportCounts(string queryTxt)
        {
            int res = 0;
            try
            {
                var cnn = new SqlConnection(_AnalysisConnectionString);
                cnn.Open();
                var cmd = new SqlCommand(queryTxt, cnn);
                res = Convert.ToInt32(cmd.ExecuteScalar());
                cmd.Dispose();
                cnn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return res;
        }
        public int GetAutomaticReportCounts(DateTime startDate, DateTime endDate,string fieldName,string cubeName,string diminisionName, out string mdx)
        {
            int res = 0;
            try
            {
                //mdx = @" SELECT  { [Measures]." + fieldName + "} ON COLUMNS FROM " + cubeName + " where { " + diminisionName + ".[date].&[" + startDate.Date.ToString("yyyy-MM-dd") + "T" + startDate.ToString("hh:mm:ss") + "] : " + diminisionName + ".[date].&[" + endDate.Date.ToString("yyyy-MM-dd") + "T00:00:00" + "] }";
                mdx = @"SELECT NON EMPTY { [Measures]." + fieldName + "} ON COLUMNS FROM ( SELECT ( " + diminisionName + ".[Date].&[" + startDate.Date.ToString("yyyy-MM-dd") + "T" + "00:00:00" + "] : " + diminisionName + ".[Date].&[" + endDate.Date.ToString("yyyy-MM-dd") + "T" + endDate.ToString("hh:mm:ss") + "] ) ON COLUMNS FROM " + cubeName + ")";

                var cst = GetCellset(mdx);
               
                if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
                {
                    res = (int)GetDouble(cst.Cells[0].FormattedValue);
                }
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return res;
        }

        private object GetScalar(string mdx)
        {
            {
                //Lets store the connection string and MDX query to local variables
                string strMDX = mdx;

                //create and open adomd connection with connection string
                AdomdConnection conn = new AdomdConnection(_AnalysisConnectionString);
                conn.Open();

                //create adomd command using connection and MDX query
                AdomdCommand cmd = new AdomdCommand(strMDX, conn);

                //The ExecuteCellSet method of adomd command will 
                //execute the MDX query and return  object
                object cst = cmd.ExecuteScalar();

                //close connection
                conn.Close();


                return cst;
            }
        }
        private CellSet GetCellset(string mdx)
        {
            //using (Impersonation.LogonUser("STC-Connect.cloudapp.net", "softecadmin", "P@ssw0rd32", LogonType.Network))
            {
                //Lets store the connection string and MDX query to local variables
                string strMDX = mdx;

                //create and open adomd connection with connection string
                AdomdConnection conn = new AdomdConnection(_AnalysisConnectionString);
                conn.Open();

                //create adomd command using connection and MDX query
                AdomdCommand cmd = new AdomdCommand(strMDX, conn);

                //The ExecuteCellSet method of adomd command will 
                //execute the MDX query and return CellSet object
                CellSet cst = cmd.ExecuteCellSet();

                //close connection
                conn.Close();

                //return cellset
                return cst;
            }
        }

        private double GetDouble(object expression)
        {
            double output = 0;

            if (expression != null)
            {
                double.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        private int GetInt(object expression)
        {
            int output = 0;

            if (expression != null)
            {
                int.TryParse(expression.ToString(), out output);
            }

            return output;
        }

        private int GetCountFromCellSet(CellSet cst)
        {
            int output = 0;

            if (cst != null && cst.Cells != null && cst.Cells.Count > 0)
            {
                output = GetInt(cst.Cells[0].FormattedValue);
            }

            return output;
        }

        private List<CubeDTO> PrepareCubeOutput(CellSet cst, int column = 0)
        {
            List<CubeDTO> list = new List<CubeDTO>();

            CubeDTO dto = null;

            if (cst != null)
            {
                TupleCollection colTuples = cst.Axes[0].Set.Tuples;
                TupleCollection rowTuples = cst.Axes[1].Set.Tuples;

                for (int col = 0; col < colTuples.Count; col++)
                {
                    dto = new CubeDTO();
                    dto.LegendName = colTuples[col].Members[column].Caption;

                    dto.LegendName = FormatText(dto.LegendName);

                    dto.Details = new ObservableCollection<CubeDetailsDTO>();
                    for (int row = 0; row < rowTuples.Count; row++)
                    {
                        try
                        {
                            CubeDetailsDTO detail = new CubeDetailsDTO();

                            detail.Key = rowTuples[row].Members[0].Caption;
                            detail.Value = GetDouble(cst.Cells[col, row].FormattedValue);

                            detail.Key = FormatText(detail.Key);

                            dto.Details.Add(detail);
                        }
                        catch
                        {

                        }
                    }

                    list.Add(dto);
                }
            }

            return list;
        }

        private void CalculatePercentage(ObservableCollection<CubeDetailsDTO> details)
        {
            if (details != null && details.Count > 0)
            {
                double total = details.Sum(x => x.Value);

                foreach (var item in details)
                {
                    if (item.Value > 0)
                    {
                        item.Percentage = ((item.Value / total) * (double)100).ToString();
                    }
                }
            }
        }

        private List<int> GetIntListFromCube(CellSet cst)
        {
            List<int> list = new List<int>();

            if (cst != null)
            {
                TupleCollection rowTuples = cst.Axes[1].Set.Tuples;

                for (int row = 0; row < rowTuples.Count; row++)
                {
                    try
                    {
                        string detail;

                        detail = rowTuples[row].Members[0].Caption;
                        detail = FormatText(detail);

                        int output = GetInt(detail);

                        if (output > 1)
                            list.Add(output);
                    }
                    catch
                    {

                    }
                }
            }

            return list;
        }

        #endregion

        #region Trend Analysis

        public List<CubeDTO> GetTruckViolationsTrendDaysOfWeek(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             {[Time].[Day Of Week].[Day Of Week]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetTruckViolationsTrendWeekOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Week Of Year].[Week Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetTruckViolationsTrendMonthOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Month Of Year].[Month Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetTruckViolationsTrendQuarterOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Quarter Of Year].[Quarter Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetViolationTypeTrendDaysOfWeek(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].ALLMEMBERS - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Day Of Week].[Day Of Week]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [MicrosoftJPSCDSADP])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetViolationTypeTrendWeekOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].ALLMEMBERS - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Week Of Year].[Week Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [MicrosoftJPSCDSADP])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetViolationTypeTrendMonthOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] *([License Plate Violation Type DIM].[Description].ALLMEMBERS - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Month Of Year].[Month Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [MicrosoftJPSCDSADP])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetViolationTypeTrendQuarterOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].ALLMEMBERS - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Quarter Of Year].[Quarter Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [MicrosoftJPSCDSADP])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetIncidentTrendDaysOfWeek(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].ALLMEMBERS - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Day Of Week].[Day Of Week]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetIncidentTrendWeekOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].ALLMEMBERS - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Week Of Year].[Week Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetIncidentTrendMonthOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].ALLMEMBERS - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Month Of Year].[Month Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetIncidentTrendQuarterOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].ALLMEMBERS - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             {[Time].[Quarter Of Year].[Quarter Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetDangerousViolationTrendDaysOfWeek(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT NON EMPTY
                            {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].ALLMEMBERS - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             NON EMPTY {[Time].[Day Of Week].[Day Of Week]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP-CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetDangerousViolationTrendWeekOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT NON EMPTY
                            {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].ALLMEMBERS - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                            NON EMPTY {[Time].[Week Of Year].[Week Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP-CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetDangerousViolationTrendMonthOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT NON EMPTY
                            {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].ALLMEMBERS - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             NON EMPTY {[Time].[Month Of Year].[Month Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP-CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetDangerousViolationTrendQuarterOfYear(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT NON EMPTY
                            {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].ALLMEMBERS - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             NON EMPTY {[Time].[Quarter Of Year].[Quarter Of Year]} ON Rows 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear.ToString() + "-01-01T00:00:00] : [Time].[Year].&[" + endYear.ToString() + "-01-01T00:00:00] ) ON COLUMNS FROM [ADP-CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        #endregion

        #region Region Wise Normal

        public List<CubeDTO> GetTruckViolationRegionWiseYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name]- [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                             { [Time].[Year].[Year].ALLMEMBERS } ON ROWS 
                            FROM [ADP CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT { [Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                            { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } ON ROWS 
                            FROM ( SELECT ( [Time].[Date].&[" + WeekStartDate.Year + "-" + WeekStartDate.Month.ToString("00") + "-" + WeekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + WeekEndDate.Year + "-" + WeekEndDate.Month.ToString("00") + "-" + WeekEndDate.Day.ToString("00") + "T00:00:00]) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationRegionWiseMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  { [Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                             { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationRegionWiseWeekly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT { [Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                            { [Time].[Week Of Year].[Week Of Year].ALLMEMBERS }  ON ROWS 
                        FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                              { [Time].[Year].[Year].ALLMEMBERS } 
                            DIMENSION PROPERTIES MEMBER_CAPTION, MEMBER_UNIQUE_NAME ON ROWS FROM [ADP-CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                             { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } 
                            DIMENSION PROPERTIES MEMBER_CAPTION, MEMBER_UNIQUE_NAME ON ROWS 
                            FROM ( SELECT ( [Time].[Date].&[" + WeekStartDate.Year + "-" + WeekStartDate.Month.ToString("00") + "-" + WeekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + WeekEndDate.Year + "-" + WeekEndDate.Month.ToString("00") + "-" + WeekEndDate.Day.ToString("00") + "T00:00:00]) ON COLUMNS FROM [ADP-CDS])";
            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  { [Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                              { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP-CDS]) WHERE ( [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationRegionWiseWeekly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  { [Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                             { [Time].[Week Of Year].[Week Of Year].ALLMEMBERS } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP-CDS]) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionWiseYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                             { [Time].[Year].[Year].ALLMEMBERS } ON ROWS FROM [ADP CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT { [Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                            { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } ON ROWS 
                            FROM ( SELECT ( [Time].[Date].&[" + WeekStartDate.Year + "-" + WeekStartDate.Month.ToString("00") + "-" + WeekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + WeekEndDate.Year + "-" + WeekEndDate.Month.ToString("00") + "-" + WeekEndDate.Day.ToString("00") + "T00:00:00]) ON COLUMNS FROM [ADP CDS])";
            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionWiseMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  { [Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                             { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionWiseWeekly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT { [Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)  } ON COLUMNS, 
                            { [Time].[Week Of Year].[Week Of Year].ALLMEMBERS } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionWiseYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                             { [Time].[Year].[Year].ALLMEMBERS } ON ROWS FROM [MicrosoftJPSCDSADP]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionWiseDaily(DateTime WeekStartDate, DateTime WeekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT { [Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                            { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } ON ROWS 
                            FROM ( SELECT ( [Time].[Date].&[" + WeekStartDate.Year + "-" + WeekStartDate.Month.ToString("00") + "-" + WeekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + WeekEndDate.Year + "-" + WeekEndDate.Month.ToString("00") + "-" + WeekEndDate.Day.ToString("00") + "T00:00:00]) ON COLUMNS FROM [MicrosoftJPSCDSADP])";
            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionWiseMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  { [Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                             { [Time].[Month Of Year].[Month Of Year] }  ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] } ) ON COLUMNS FROM [MicrosoftJPSCDSADP]) WHERE ( [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionWiseWeekly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT { [Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                            { [Time].[Week Of Year].[Week Of Year].ALLMEMBERS } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [MicrosoftJPSCDSADP]) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public double GetFatalityCountPerYear(int year)
        {
            double res = 0.00;
            string mdx = @" SELECT  { [Measures].[Fatalities Count] } ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] )";
            var cst = GetCellset(mdx);
            if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
            {
                res = GetDouble(cst.Cells[0].FormattedValue);
            }
            return res;
        }

        public double GetFatalInjuriesCountPerYear(int year)
        {
            double res = 0.00;
            string mdx = @" SELECT  { [Measures].[Total Injuries Fatalities] } ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] )";
            var cst = GetCellset(mdx);
            if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
            {
                res = GetDouble(cst.Cells[0].FormattedValue);
            }
            return res;
        }
        public double GetAccidentCountPerYearAndAccidentReason(int year, string accidentReason)
        {
            try
            {
                double res = 0.00;

                string mdx = @" SELECT  { [Measures].[Service Call Cube View Count] } ON COLUMNS FROM ( SELECT ( { [Activity Reason DIM].[Description].&[" + accidentReason + "] } ) ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00], [Activity Reason DIM].[Description].&[" + accidentReason + "] )";
                var cst = GetCellset(mdx);
                if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
                {
                    res = GetDouble(cst.Cells[0].FormattedValue);
                }
                return res;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }

        public double GetFatalityCountPerYearAndRegion(int year, string region)
        {
            double res = 0.00;

            string mdx = @" SELECT  { [Measures].[Fatalities Count] } ON COLUMNS FROM ( SELECT ( { [Geo State DIM].[State Name].&[" + region + "] } ) ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00], [Geo State DIM].[State Name].&[" + region + "] )";
            var cst = GetCellset(mdx);
            if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
            {
                res = GetDouble(cst.Cells[0].FormattedValue);
            }
            return res;
        }

        public double GetFatalityCountPerYearAndReason(int year, string reason)
        {
            try
            {
                double res = 0.00;

                string mdx = @" SELECT  { [Measures].[Fatalities Count] } ON COLUMNS FROM ( SELECT ( { [Activity Reason DIM].[Description].&[" + reason + "] } ) ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00], [Activity Reason DIM].[Description].&[" + reason + "] )";
                var cst = GetCellset(mdx);
                if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
                {
                    res = GetDouble(cst.Cells[0].FormattedValue);
                }
                return res;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        public double GetAccidentCountPerYearAndAccidentType(int year, string accidentType)
        {
            double res = 0.00;
            string mdx = @"SELECT  { [Measures].[Service Call Cube View Count] } ON COLUMNS FROM ( SELECT ( { [Service Call Priority DIM].[Description].&[" + accidentType + "] } ) ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00], [Service Call Priority DIM].[Description].&[" + accidentType + "] )";
            var cst = GetCellset(mdx);
            if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
            {
                res = GetDouble(cst.Cells[0].FormattedValue);
            }
            return res;
        }

        public double GetViolationCountPerYearAndVehicleType(int year, string vehicleType)
        {
            double res = 0.00;
            string mdx = @" SELECT  { [Measures].[Violation View Count] } ON COLUMNS FROM ( SELECT ( { [Item Category DIM].[Description].&[" + vehicleType + "] } ) ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [MicrosoftJPSCDSADP])) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00], [Item Category DIM].[Description].&[" + vehicleType + "] )";
            var cst = GetCellset(mdx);
            if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
            {
                res = GetDouble(cst.Cells[0].FormattedValue);
            }
            return res;
        }

        public double GetViolationCountPerYearPerType(int year, string type)
        {
            double res = 0.00;
            string mdx = @"SELECT  { [Measures].[Violation View Count] } ON COLUMNS FROM ( SELECT ( { [License Plate Violation Type DIM].[Description].&[" + type + "] } ) ON COLUMNS FROM ( SELECT ( { [Time].[Year].&[" + year + "-01-01T00:00:00] } ) ON COLUMNS FROM [MicrosoftJPSCDSADP])) WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00], [License Plate Violation Type DIM].[Description].&[" + type + "] )";
            var cst = GetCellset(mdx);
            if (cst != null && cst.Cells != null && cst.Cells.Count > 0 && cst.Cells[0] != null)
            {
                res = GetDouble(cst.Cells[0].FormattedValue);
            }
            return res;
        }

        #endregion

        #region Region Wise Comparison

        public List<CubeDTO> GetTruckViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name].ALLMEMBERS - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Year].[Year] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {[Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [ADP CDS])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Truck Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS  }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)  } ON COLUMNS, 
                              { [Time].[Year].[Year] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [MicrosoftJPSCDSADP]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {[Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [MicrosoftJPSCDSADP])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [MicrosoftJPSCDSADP]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Violation View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [MicrosoftJPSCDSADP]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Year].[Year] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {[Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [ADP CDS])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS,  
                              { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Service Call Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Year].[Year] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP-CDS]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {[Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                                { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [ADP-CDS])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [ADP-CDS]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsRegionComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Dangerous Violation Cube View Count] * ([Geo State DIM].[State Name].[State Name] - [Geo State DIM].[State Name].[All].UNKNOWNMEMBER)} ON COLUMNS,
                               { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [ADP-CDS]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        #endregion

        #region Target Analysis

        public List<CubeDTO> GetViolationsTarget()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Violation View Count] } ON COLUMNS,
                             { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [MicrosoftJPSCDSADP]) WHERE ( [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst);
            return list;
        }

        public List<CubeDTO> GetIncidentsTarget()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Service Call Cube View Count] } ON COLUMNS,
                             { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationTarget()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  NON EMPTY { [Measures].[Dangerous Violation Cube View Count] } ON COLUMNS,
                             NON EMPTY { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP-CDS]) WHERE ( [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst);
            return list;
        }

        public List<CubeDTO> GetTruckViolationTarget()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Measures].[Truck Violation View Count] } ON COLUMNS, 
                             { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                            FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS]) WHERE ( [Time].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00] )";


            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst);
            return list;
        }

        #endregion

        #region Statistical Normal

        public List<CubeDTO> GetTruckViolationsStatisticalYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                            {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].ALLMEMBERS - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             NON EMPTY { [Time].[Year].[Year] } ON Rows 
                            FROM [ADP CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsStaticsticalMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                        {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                         { [Time].[Month Of Year].[Month Of Year] } ON Rows 
                        FROM [ADP CDS]
                        WHERE [Time].[Year].&[" + year + "-01-01T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                             {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             { [Time].[Week].[Week] } ON Rows 
                            FROM ( SELECT ( { [Time].[Month Of Year].&[" + ((int)month) + @"] } ) ON COLUMNS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                            FROM [ADP CDS])) 
                            WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] ) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsStatisticalDaily(DateTime weekStartDate, DateTime weekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT   {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                                      { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } ON Rows 
                                    FROM [ADP CDS]
                                    WHERE [Time].[Date].&[" + weekStartDate.Year + "-" + weekStartDate.Month.ToString("00") + "-" + weekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + weekEndDate.Year + "-" + weekEndDate.Month.ToString("00") + "-" + weekEndDate.Day.ToString("00") + "T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsStatisticalYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             { [Time].[Year].[Year] } ON Rows 
                            FROM [MicrosoftJPSCDSADP]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsStaticsticalMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                        {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                         { [Time].[Month Of Year].[Month Of Year] } ON Rows 
                        FROM [MicrosoftJPSCDSADP]
                        WHERE [Time].[Year].&[" + year + "-01-01T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                             {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             { [Time].[Week].[Week] } ON Rows 
                            FROM ( SELECT ( { [Time].[Month Of Year].&[" + ((int)month) + @"] } ) ON COLUMNS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                            FROM [MicrosoftJPSCDSADP])) 
                            WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] ) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsStatisticalDaily(DateTime weekStartDate, DateTime weekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT   {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                                      { [Time].[Day Of Week].[Day Of Week] } ON Rows 
                                    FROM [MicrosoftJPSCDSADP]
                                    WHERE [Time].[Date].&[" + weekStartDate.Year + "-" + weekStartDate.Month.ToString("00") + "-" + weekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + weekEndDate.Year + "-" + weekEndDate.Month.ToString("00") + "-" + weekEndDate.Day.ToString("00") + "T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsStatisticalYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                            {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             NON EMPTY { [Time].[Year].[Year] } ON Rows 
                            FROM [ADP CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsStaticsticalMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                        {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                         { [Time].[Month Of Year].[Month Of Year] } ON Rows 
                        FROM [ADP CDS]
                        WHERE [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsStaticsticalWeekly(int year, MonthOfYear month)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                             {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                             { [Time].[Week].[Week] } ON Rows 
                            FROM ( SELECT ( { [Time].[Month Of Year].&[" + ((int)month) + @"] } ) ON COLUMNS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                            FROM [ADP CDS])) 
                            WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] ) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsStatisticalDaily(DateTime weekStartDate, DateTime weekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT   {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                                      { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } ON Rows 
                                    FROM [ADP CDS]
                                    WHERE [Time].[Date].&[" + weekStartDate.Year + "-" + weekStartDate.Month.ToString("00") + "-" + weekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + weekEndDate.Year + "-" + weekEndDate.Month.ToString("00") + "-" + weekEndDate.Day.ToString("00") + "T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsStatisticalYearly()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT NON EMPTY
                            {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                            NON EMPTY { [Time].[Year].[Year] } ON Rows 
                            FROM [ADP-CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsStaticsticalMonthly(int year)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT NON EMPTY
                        {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                        NON EMPTY { [Time].[Month Of Year].[Month Of Year] } ON Rows 
                        FROM [ADP-CDS]
                        WHERE [Time].[Year].&[" + year.ToString() + "-01-01T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsStaticsticalWeekly(int year, MonthOfYear month)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT NON EMPTY
                             {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                             NON EMPTY { [Time].[Week].[Week] } ON Rows 
                            FROM ( SELECT ( { [Time].[Month Of Year].&[" + ((int)month) + @"] } ) ON COLUMNS 
                            FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                            FROM [ADP-CDS])) 
                            WHERE ( [Time].[Year].&[" + year + "-01-01T00:00:00] ) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsStatisticalDaily(DateTime weekStartDate, DateTime weekEndDate)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  NON EMPTY {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER) } ON Columns,
                                      NON EMPTY { [Time].[Day Of Week].[Day Of Week].ALLMEMBERS } ON Rows 
                                    FROM [ADP-CDS]
                                    WHERE [Time].[Date].&[" + weekStartDate.Year + "-" + weekStartDate.Month.ToString("00") + "-" + weekStartDate.Day.ToString("00") + "T00:00:00] : [Time].[Date].&[" + weekEndDate.Year + "-" + weekEndDate.Month.ToString("00") + "-" + weekEndDate.Day.ToString("00") + "T00:00:00]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        #endregion

        #region Statistical Comparison

        public List<CubeDTO> GetTruckViolationsComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Year].[Year] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [ADP CDS])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetTruckViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Truck Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Violation View Count] * [Time].[Year].[Year] } ON COLUMNS, 
                              {  ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [MicrosoftJPSCDSADP]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {([Measures].[Violation View Count] *  ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) )} ON COLUMNS, 
                               { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [MicrosoftJPSCDSADP])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Violation View Count] *  ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)  } ON COLUMNS, 
                              { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [MicrosoftJPSCDSADP]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Violation View Count] *  ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER) } ON COLUMNS, 
                              { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [MicrosoftJPSCDSADP]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                              {[Measures].[Service Call Cube View Count] * [Time].[Year].[Year]} ON COLUMNS, 
                              { ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER) }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [ADP CDS])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS,  
                              { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT  {[Measures].[Service Call Cube View Count] * ([Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [ADP CDS]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsComparisonYearly(int startYear, int endYear)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT NON EMPTY
                              {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                              NON EMPTY { [Time].[Year].[Year] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP-CDS]) ";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsComparisonMonthly(int year, MonthOfYear startMonth, MonthOfYear endMonth)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT NON EMPTY {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                               NON EMPTY { [Time].[Month Of Year].[Month Of Year] } ON ROWS 
                              FROM ( SELECT ( [Time].[Month Of Year].&[" + ((int)endMonth).ToString("00") + @"] : [Time].[Month Of Year].&[" + ((int)startMonth).ToString("00") + @"] ) ON COLUMNS 
                              FROM ( SELECT ( { [Time].[Year].&[" + year + @"-01-01T00:00:00] } ) ON COLUMNS 
                              FROM [ADP-CDS])) 
                              WHERE ( [Time].[Year].&[" + year + @"-01-01T00:00:00] )";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsComparisonWeekly(DateTime firstWeek, DateTime secondWeek)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT NON EMPTY {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS, 
                             NON EMPTY { [Time].[Week Of Year].[Week Of Year] }  ON ROWS 
                             FROM [ADP-CDS]
                             WHERE UNION(([Time].[Week].&[" + firstWeek.Year + "-" + firstWeek.Month.ToString("00") + "-" + firstWeek.Day.ToString("00") + "T00:00:00]),([Time].[Week].&[" + secondWeek.Year + "-" + secondWeek.Month.ToString("00") + "-" + secondWeek.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetDangerousViolationsComparisonDaily(DateTime firstDay, DateTime secondDay)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"  SELECT NON EMPTY {[Measures].[Dangerous Violation Cube View Count] * ([Activity Reason DIM].[Description].[Description] - [Activity Reason DIM].[Description].[All].UNKNOWNMEMBER)} ON COLUMNS,
                              NON EMPTY { [Time].[Day Of Week].[Day Of Week]  }  ON ROWS 
                             FROM [ADP-CDS]
                             WHERE UNION(([Time].[Date].&[" + firstDay.Year + "-" + firstDay.Month.ToString("00") + "-" + firstDay.Day.ToString("00") + "T00:00:00] ),([Time].[Date].&[" + secondDay.Year + "-" + secondDay.Month.ToString("00") + "-" + secondDay.Day.ToString("00") + "T00:00:00]))";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        #endregion

        #region Main Dashboard

        public List<CubeDTO> GetTotalInjuriesComparisonForDashboard(int startYear, int endYear)
        {
            try
            {
                List<CubeDTO> list = new List<CubeDTO>();
                string mdx = @" WITH MEMBER [Measures].[Total]
                            AS [Measures].[Slight Injuries Count] + [Measures].[Medium Injuries Count] + [Measures].[Severe Injuries Count]
                            SELECT 
                            {([Measures].[Total] * [Time].[Year].[Year])}  ON COLUMNS, 
                            { [Crash Severity DIM].[Description].[All] } ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

                CellSet cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 1);

                if (list != null && list.Count > 0)
                {
                    double _previousValue = 0;
                    double _currentValue = 0;

                    var lastYear = list.First();
                    if (lastYear.Details != null && lastYear.Details.Count > 0)
                        _previousValue = lastYear.Details.First().Value;

                    if (list.Count > 1)
                    {
                        var currentYear = list[1];

                        if (currentYear.Details != null && currentYear.Details.Count > 0)
                        {
                            _currentValue = currentYear.Details.First().Value;
                        }
                    }
                    KpiDTO target = new KpiDTO
                    {
                        TargetName = "TARGET_TOTALINJURIES",
                        CurrentValue = _currentValue,
                        PreviousValue = _previousValue,
                        LabelValueArabic = " إجمالي عدد الإصابات",
                        LabelValueEnglish = "Total Injuries",
                        Percentage = GetTargetPercentage("TARGET_TOTALINJURIES")
                    };


                    list.Add(new CubeDTO
                    {
                        LegendName = "(" + DateTime.Now.Year.ToString() + ")Target",
                        Details = new ObservableCollection<CubeDetailsDTO>
                    {
                        new CubeDetailsDTO
                        {
                            Key = "Total",
                            Value = target.TargetValue
                        }
                    }
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return new List<CubeDTO>();
            }
        }

        public List<CubeDTO> GetSevereAccidentsComparisonForDashboard(int startYear, int endYear)
        {
            try
            {
                List<CubeDTO> list = new List<CubeDTO>();
                string mdx = @" SELECT 
                            {[Measures].[Severe Injuries Count] * [Time].[Year].[Year]} ON COLUMNS, 
                            { [Crash Severity DIM].[Description].[All] } ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

                CellSet cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 1);

                if (list != null && list.Count > 0)
                {
                    double _previousValue = 0;
                    double _currentValue = 0;

                    var lastYear = list.First();
                    if (lastYear.Details != null && lastYear.Details.Count > 0)
                        _previousValue = lastYear.Details.First().Value;

                    if (list.Count > 1)
                    {
                        var currentYear = list[1];

                        if (currentYear.Details != null && currentYear.Details.Count > 0)
                        {
                            _currentValue = currentYear.Details.First().Value;
                        }
                    }
                    KpiDTO target = new KpiDTO
                    {
                        TargetName = "TARGET_ACCIDENTS_FATALITYINJURIES",
                        CurrentValue = _currentValue,
                        PreviousValue = _previousValue,
                        LabelValueArabic = " نوع حوادث الوفيات",
                        LabelValueEnglish = "Fatal accidents",
                        Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYINJURIES")
                    };


                    list.Add(new CubeDTO
                    {
                        LegendName = "(" + DateTime.Now.Year.ToString() + ")Target",
                        Details = new ObservableCollection<CubeDetailsDTO>
                    {
                        new CubeDetailsDTO
                        {
                            Key = "Total",
                            Value = target.TargetValue
                        }
                    }
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return new List<CubeDTO>();
            }
        }

        public List<CubeDTO> GetFatalitiesComparisonForDashboard(int startYear, int endYear)
        {
            try
            {
                List<CubeDTO> list = new List<CubeDTO>();
                string mdx = @" SELECT 
                            {[Measures].[Fatalities Count] * [Time].[Year].[Year]} ON COLUMNS, 
                            { [Crash Severity DIM].[Description].[All] } ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

                CellSet cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 1);

                if (list != null && list.Count > 0)
                {
                    double _previousValue = 0;
                    double _currentValue = 0;

                    var lastYear = list.First();
                    if (lastYear.Details != null && lastYear.Details.Count > 0)
                        _previousValue = lastYear.Details.First().Value;

                    if (list.Count > 1)
                    {
                        var currentYear = list[1];

                        if (currentYear.Details != null && currentYear.Details.Count > 0)
                        {
                            _currentValue = currentYear.Details.First().Value;
                        }
                    }
                    KpiDTO target = new KpiDTO
                    {
                        TargetName = "TARGET_ACCIDENTS_FATALITYACCIDENTS",
                        CurrentValue = _currentValue,
                        PreviousValue = _previousValue,
                        LabelValueArabic = " حوادث الوفيات",
                        LabelValueEnglish = "Fatal accidents",
                        Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYACCIDENTS")
                    };

                    list.Add(new CubeDTO
                    {
                        LegendName = "(" + DateTime.Now.Year.ToString() + ")Target",
                        Details = new ObservableCollection<CubeDetailsDTO>
                    {
                        new CubeDetailsDTO
                        {
                            Key = "Total",
                            Value = target.TargetValue
                        }
                    }
                    });
                }

                return list;

            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return new List<CubeDTO>();
            }
        }

        public List<CubeDTO> GetDangerousViolatorsComparisonForDashboard(int startYear, int endYear)
        {
            try
            {
                List<CubeDTO> list = new List<CubeDTO>();
                string mdx = @" SELECT 
                            {[Measures].[Dangerous Violation Cube View Count] * [Time].[Year].[Year]} ON COLUMNS, 
                            { [Activity Reason DIM].[Description].[All] }  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP-CDS]) ";

                CellSet cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 1);

                if (list != null && list.Count > 0)
                {
                    double _previousValue = 0;
                    double _currentValue = 0;

                    var lastYear = list.First();
                    if (lastYear.Details != null && lastYear.Details.Count > 0)
                        _previousValue = lastYear.Details.First().Value;

                    if (list.Count > 1)
                    {
                        var currentYear = list[1];

                        if (currentYear.Details != null && currentYear.Details.Count > 0)
                        {
                            _currentValue = currentYear.Details.First().Value;
                        }
                    }
                    KpiDTO target = new KpiDTO
                    {
                        TargetName = "TARGET_DANGEROUSVIOLATOR",
                        CurrentValue = _currentValue,
                        PreviousValue = _previousValue,
                        LabelValueArabic = " المخالفين الخطرين",
                        LabelValueEnglish = "Dangerous Violators",
                        Percentage = GetTargetPercentage("TARGET_DANGEROUSVIOLATOR")
                    };

                    list.Add(new CubeDTO
                    {
                        LegendName = "(" + DateTime.Now.Year.ToString() + ")Target",
                        Details = new ObservableCollection<CubeDetailsDTO>
                    {
                        new CubeDetailsDTO
                        {
                            Key = "Total",
                            Value = target.TargetValue
                        }
                    }
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return new List<CubeDTO>();
            }
        }

        public List<CubeDTO> GetAccidentsWithFatalitiesCountComparisonForDashboard(int startYear, int endYear)
        {
            try
            {

                List<CubeDTO> list = new List<CubeDTO>();
                string mdx = @" SELECT 
                            {([Measures].[Service Call Cube View Count] * [Time].[Year].[Year])}  ON COLUMNS, 
                            { [Crash Severity DIM].[Description].[All] } HAVING [Measures].[Fatalities Count] > 0  ON ROWS 
                             FROM ( SELECT ( [Time].[Year].&[" + startYear + @"-01-01T00:00:00] : [Time].[Year].&[" + endYear + @"-01-01T00:00:00] ) ON COLUMNS 
                             FROM [ADP CDS]) ";

                CellSet cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 1);

                if (list != null && list.Count > 0)
                {
                    double _previousValue = 0;
                    double _currentValue = 0;

                    var lastYear = list.First();
                    if (lastYear.Details != null && lastYear.Details.Count > 0)
                        _previousValue = lastYear.Details.First().Value;

                    if (list.Count > 1)
                    {
                        var currentYear = list[1];

                        if (currentYear.Details != null && currentYear.Details.Count > 0)
                        {
                            _currentValue = currentYear.Details.First().Value;
                        }
                    }
                    KpiDTO target = new KpiDTO
                    {
                        TargetName = "TARGET_ACCIDENTSPERTYPE_FATAL",
                        CurrentValue = _currentValue,
                        PreviousValue = _previousValue,
                        LabelValueArabic = " نوع حوادث الوفيات",
                        LabelValueEnglish = "Fatal accidents",
                        Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_FATAL")
                    };

                    list.Add(new CubeDTO
                    {
                        LegendName = "(" + DateTime.Now.Year.ToString() + ")Target",
                        Details = new ObservableCollection<CubeDetailsDTO>
                    {
                        new CubeDetailsDTO
                        {
                            Key = "Total",
                            Value = target.TargetValue
                        }
                    }
                    });
                }

                return list;
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return new List<CubeDTO>();
            }
        }

        public CubeDetailsDTO GetTotalViolationsForMainDashboard(string language)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDetailsDTO currentYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO lastYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO output = new CubeDetailsDTO();

            output.Key = language == "ar" ? "مخالفات مرورية" : "Traffic Violations";
            output.Value = 0;
            output.Percentage = "100%";
            output.Orientation = "down";

            string mdx = @"    SELECT  { [Measures].[Violation View Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [MicrosoftJPSCDSADP])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 0);

            if (list != null && list.Count > 0)
            {
                currentYearViolations = list.First().Details.FirstOrDefault();

                mdx = @"    SELECT  { [Measures].[Violation View Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-01-01T00:00:00] : [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-" + DateTime.Now.AddYears(-1).Month.ToString("00") + "-" + DateTime.Now.AddYears(-1).Day.ToString("00") + "T00:00:00] ) ON COLUMNS FROM [MicrosoftJPSCDSADP])";

                cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 0);

                if (list != null && list.Count > 0)
                {
                    lastYearViolations = list.First().Details.FirstOrDefault();
                }

                if (currentYearViolations != null)
                {
                    output.Value = currentYearViolations.Value;

                    if (lastYearViolations != null && lastYearViolations.Value > 0)
                    {
                        output.Percentage = ((currentYearViolations.Value / lastYearViolations.Value) * 100).ToString();
                        output.Orientation = currentYearViolations.Value > lastYearViolations.Value ? "up" : "down";
                    }
                    else
                    {
                        output.Percentage = "100%";
                        output.Orientation = "up";
                    }
                }
            }

            return output;
        }

        public CubeDetailsDTO GetTotalFatalitiesForMainDashboard(string language)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDetailsDTO currentYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO lastYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO output = new CubeDetailsDTO();

            output.Key = language == "ar" ? "حالات الوفاة" : "Fatalities";
            output.Value = 0;
            output.Percentage = "100%";
            output.Orientation = "down";

            string mdx = @"    SELECT  { [Measures].[Fatalities Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 0);

            if (list != null && list.Count > 0)
            {
                currentYearViolations = list.First().Details.FirstOrDefault();

                mdx = @"    SELECT  { [Measures].[Fatalities Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-01-01T00:00:00] : [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-" + DateTime.Now.AddYears(-1).Month.ToString("00") + "-" + DateTime.Now.AddYears(-1).Day.ToString("00") + "T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

                cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 0);

                if (list != null && list.Count > 0)
                {
                    lastYearViolations = list.First().Details.FirstOrDefault();
                }

                if (currentYearViolations != null)
                {
                    output.Value = currentYearViolations.Value;

                    if (lastYearViolations != null && lastYearViolations.Value > 0)
                    {
                        output.Percentage = ((currentYearViolations.Value / lastYearViolations.Value) * 100).ToString();
                        output.Orientation = currentYearViolations.Value > lastYearViolations.Value ? "up" : "down";
                    }
                    else
                    {
                        output.Percentage = "100%";
                        output.Orientation = "up";
                    }
                }
            }

            return output;
        }

        public CubeDetailsDTO GetTotalSlightInjuriesForMainDashboard(string language)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDetailsDTO currentYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO lastYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO output = new CubeDetailsDTO();

            output.Key = language == "ar" ? "إصابات بسيطة" : "Slight Injuries";
            output.Value = 0;
            output.Percentage = "100%";
            output.Orientation = "down";

            string mdx = @"    SELECT  { [Measures].[Slight Injuries Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 0);

            if (list != null && list.Count > 0)
            {
                currentYearViolations = list.First().Details.FirstOrDefault();

                mdx = @"    SELECT  { [Measures].[Slight Injuries Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-01-01T00:00:00] : [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-" + DateTime.Now.AddYears(-1).Month.ToString("00") + "-" + DateTime.Now.AddYears(-1).Day.ToString("00") + "T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

                cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 0);

                if (list != null && list.Count > 0)
                {
                    lastYearViolations = list.First().Details.FirstOrDefault();
                }

                if (currentYearViolations != null)
                {
                    output.Value = currentYearViolations.Value;

                    if (lastYearViolations != null && lastYearViolations.Value > 0)
                    {
                        output.Percentage = ((currentYearViolations.Value / lastYearViolations.Value) * 100).ToString();
                        output.Orientation = currentYearViolations.Value > lastYearViolations.Value ? "up" : "down";
                    }
                    else
                    {
                        output.Percentage = "100%";
                        output.Orientation = "up";
                    }
                }
            }

            return output;
        }

        public CubeDetailsDTO GetTotalSevereInjuriesForMainDashboard(string language)
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDetailsDTO currentYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO lastYearViolations = new CubeDetailsDTO();
            CubeDetailsDTO output = new CubeDetailsDTO();

            output.Key = language == "ar" ? "إصابات بليغة" : "Severe Injuries";
            output.Value = 0;
            output.Percentage = "100%";
            output.Orientation = "down";

            string mdx = @"    SELECT  { [Measures].[Severe Injuries Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Year + "-01-01T00:00:00] } ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 0);

            if (list != null && list.Count > 0)
            {
                currentYearViolations = list.First().Details.FirstOrDefault();

                mdx = @"    SELECT  { [Measures].[Severe Injuries Count] } ON COLUMNS, 
                               { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                              FROM ( SELECT ( [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-01-01T00:00:00] : [Time].[Date].&[" + DateTime.Now.AddYears(-1).Year + "-" + DateTime.Now.AddYears(-1).Month.ToString("00") + "-" + DateTime.Now.AddYears(-1).Day.ToString("00") + "T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

                cst = GetCellset(mdx);
                list = PrepareCubeOutput(cst, 0);

                if (list != null && list.Count > 0)
                {
                    lastYearViolations = list.First().Details.FirstOrDefault();
                }

                if (currentYearViolations != null)
                {
                    output.Value = currentYearViolations.Value;

                    if (lastYearViolations != null && lastYearViolations.Value > 0)
                    {
                        output.Percentage = ((currentYearViolations.Value / lastYearViolations.Value) * 100).ToString();
                        output.Orientation = currentYearViolations.Value > lastYearViolations.Value ? "up" : "down";
                    }
                    else
                    {
                        output.Percentage = "100%";
                        output.Orientation = "up";
                    }
                }
            }

            return output;
        }

        #endregion

        #region Fatality Target 2030

        public List<CubeDTO> GetFatalitiyTarget()
        {
            List<CubeDTO> list = new List<CubeDTO>();

            string mdx = @" SELECT  { [Measures].[Fatalities Count] } ON COLUMNS, 
                            { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS  
                            FROM ( SELECT ( [Time].[Year].&[2009-01-01T00:00:00] : [Time].[Year].&[2030-01-01T00:00:00] ) ON COLUMNS FROM [ADP CDS])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 0);

            foreach (CubeDTO item in list)
            {
                item.LineDetails = new ObservableCollection<CubeDetailsDTO>();

                if (item.Details != null && item.Details.Count > 0)
                {
                    item.LineDetails.Add(item.Details.First());
                    item.LineDetails.Add(item.Details.Last());

                    foreach (CubeDetailsDTO detail in item.Details)
                    {
                        try
                        {
                            if (detail.Key == DateTime.Now.Year.ToString())
                            {
                                var fatalities = GetFatalitiesComparisonForDashboard(DateTime.Now.AddYears(-1).Year, DateTime.Now.Year);

                                if (fatalities != null && fatalities.Count > 2)
                                {
                                    var target = fatalities.Last();

                                    if (target.Details != null && target.Details.Count > 0)
                                    {
                                        detail.TargetValue = target.Details.First().Value;
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Utility.WriteLog(ex);
                        }

                        switch (detail.Key.Trim())
                        {
                            case "2009":
                                detail.Value = 409;
                                break;
                            case "2010":
                                detail.Value = 376;
                                break;
                            case "2011":
                                detail.Value = 334;
                                break;
                            case "2012":
                                detail.Value = 271;
                                break;
                            case "2013":
                                detail.Value = 289;
                                break;
                            case "2014":
                                detail.Value = 267;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            return list;
        }

        #endregion

        #region On Map Counts

        public List<CubeDTO> GetViolationsCountForMap(PeriodType type)
        {
            string axe = "";

            switch (type)
            {
                case PeriodType.Monthly:
                    axe = "[Time].[Month].[Month]";
                    break;
                case PeriodType.Weekly:
                    axe = "[Time].[Week].[Week]";
                    break;
                case PeriodType.Daily:
                    axe = "[Time].[Date].[Date]";
                    break;
                default:
                    break;
            }

            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * " + axe + " } ON Columns, { [License Plate Camera DIM].[Code].[Code] } ON Rows FROM [MicrosoftJPSCDSADP]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        public List<CubeDTO> GetIncidentsCountForMap(PeriodType type)
        {
            string axe = "";

            switch (type)
            {
                case PeriodType.Monthly:
                    axe = "[Time].[Month].[Month]";
                    break;
                case PeriodType.Weekly:
                    axe = "[Time].[Week].[Week]";
                    break;
                case PeriodType.Daily:
                    axe = "[Time].[Date].[Date]";
                    break;
                default:
                    break;
            }

            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT 
                            {[Measures].[Service Call Cube View Count] * " + axe + " } ON Columns, { [License Plate Camera DIM].[Code].[Code] } ON Rows FROM [MicrosoftJPSCDSADP]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        #endregion

        #region Mesc

        public List<CubeDTO> GetViolationCountMonthAndCity()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * [Item Registration Authority DIM].[Description].[Description]} ON Columns,
                             {[Time].[Month Of Year].[Month Of Year]} ON Rows 
                            FROM [MicrosoftJPSCDSADP]
                             WHERE ([Time].[Year].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;

        }

        public List<CubeDTO> GetViolationPerMonthAndViolationType()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @"SELECT   
                                    {[Measures].[Violation View Count] * [License Plate Violation Type DIM].[Description].[Description]} ON Columns,
                                     {[Time].[Month Of Year].[Month Of Year]} ON Rows 
                                    FROM [MicrosoftJPSCDSADP]
                        WHERE ([Time].[Year].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetViolationPerVehicleTypeAndDayOfWeek()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT   
                            {[Measures].[Violation View Count] * [Item Registration Plate Kind DIM].[Description].[Description]} ON Columns,
                             {[Time].[Day Of Week].[Day Of Week]} ON Rows 
                            FROM [MicrosoftJPSCDSADP]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public List<CubeDTO> GetViolationPerSpeedAndTime()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            CubeDTO dto = null;

            string mdx = @" SELECT 
                            {[Measures].[Violation View Count] * [Speed Dim].[description].[description]} ON Columns,
                             {[Time Period Dim].[description].[description]} ON Rows 
                            FROM [MicrosoftJPSCDSADP]
                        WHERE ([Time].[Year].[Year].&[" + DateTime.Now.Date.Year + "-01-01T00:00:00])";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);

            return list;
        }

        public int GetViolationYearCountForAsset(string assetCode)
        {
            int output = 0;

            string mdx = @" SELECT NON EMPTY { [Measures].[Violation View Count] } ON COLUMNS 
                             FROM ( SELECT ( { [License Plate Camera DIM].[Code].&[" + assetCode + @"] } ) ON COLUMNS 
                             FROM ( SELECT ( { [Time].[Year].&[" + DateTime.Now.Year.ToString() + @"-01-01T00:00:00] } ) ON COLUMNS 
                             FROM [MicrosoftJPSCDSADP])) 
                             WHERE ( [Time].[Year].&[" + DateTime.Now.Year.ToString() + "-01-01T00:00:00], [License Plate Camera DIM].[Code].&[" + assetCode + "] )";

            CellSet cst = GetCellset(mdx);

            output = GetCountFromCellSet(cst);

            return output;
        }

        public int GetViolationMonthCountForAsset(string assetCode)
        {
            int output = 0;

            string mdx = @"   SELECT NON EMPTY { [Measures].[Violation View Count] } ON COLUMNS 
                                FROM ( SELECT ( { [License Plate Camera DIM].[Code].&[" + assetCode + @"] } ) ON COLUMNS 
                                FROM ( SELECT ( { [Time].[Month].&[" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + @"-01T00:00:00] } ) ON COLUMNS 
                                FROM [MicrosoftJPSCDSADP])) WHERE ( [Time].[Month].&[" + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString("00") + "-01T00:00:00], [License Plate Camera DIM].[Code].&[" + assetCode + "])";

            CellSet cst = GetCellset(mdx);

            output = GetCountFromCellSet(cst);

            return output;
        }

        public int GetViolationWeekCountForAsset(string assetCode)
        {
            DateTime startofweek = DateTime.Now.StartOfWeek();
            int output = 0;

            string mdx = @"  SELECT NON EMPTY { [Measures].[Violation View Count] } ON COLUMNS 
                             FROM ( SELECT ( { [License Plate Camera DIM].[Code].&[" + assetCode + @"] } ) ON COLUMNS 
                             FROM (SELECT( { [Time].[Week].&[" + startofweek.Year.ToString("00") + "-" + startofweek.Month.ToString("00") + "-" + startofweek.Day.ToString("00") + @"T00:00:00]} ) ON COLUMNS
                             FROM [MicrosoftJPSCDSADP])) 
                             WHERE([Time].[Week].&[" + startofweek.Year.ToString("00") + "-" + startofweek.Month.ToString("00") + "-" + startofweek.Day.ToString("00") + "T00:00:00], [License Plate Camera DIM].[Code].&[" + assetCode + "] )";

            CellSet cst = GetCellset(mdx);

            output = GetCountFromCellSet(cst);

            return output;
        }

        #endregion

        #region Available Years

        public List<int> GetViolationsAvailableYears()
        {
            string mdx = @" SELECT {  } ON COLUMNS, 
                            { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                            FROM [MicrosoftJPSCDSADP] ";

            CellSet cst = GetCellset(mdx);

            return GetIntListFromCube(cst);
        }

        public List<int> GetIncidentsAvailableYears()
        {
            string mdx = @" SELECT {  } ON COLUMNS, 
                            { ([Time].[Year].[Year].ALLMEMBERS ) } ON ROWS 
                            FROM [ADP CDS] ";

            CellSet cst = GetCellset(mdx);

            return GetIntListFromCube(cst);
        }

        #endregion

        #region 3D Charts

        public List<CubeDTO> GetViolations3DTypeAndRegion()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @"SELECT 
                        {[Measures].[Violation View Count] * ([License Plate Violation Type DIM].[Description].[Description] - [License Plate Violation Type DIM].[Description].[All].UNKNOWNMEMBER)} ON Columns,
                        { [Geo State DIM].[State Name].[State Name] } ON Rows 
                        FROM [MicrosoftJPSCDSADP]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst, 1);
            return list;
        }

        #endregion

        #region ChartsCityWise

        public List<CubeDTO> GetAccidentSeverityCountforCity()
        {
            List<CubeDTO> list = new List<CubeDTO>();
            string mdx = @" SELECT  { [Geo City DIM].[City Name].[City Name] - [Geo City DIM].[City Name].[All].UNKNOWNMEMBER } ON COLUMNS, 
                            { [Crash Severity DIM].[Description].[Description] - [Crash Severity DIM].[Description].[All].UNKNOWNMEMBER} ON ROWS 
                            FROM [ADP CDS]";

            CellSet cst = GetCellset(mdx);
            list = PrepareCubeOutput(cst);

            foreach (var item in list)
            {
                CalculatePercentage(item.Details);
            }

            return list;
        }

        #endregion

        #region KPI Targets

        public KpiDTO GetFatalAccidentKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTSPERTYPE_FATAL",
                CurrentValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year, "FatAcc"),
                PreviousValue = GetAccidentCountPerYearAndAccidentType(DateTime.Now.Year - 1, "FatAcc"),
                LabelValueArabic = " نوع حوادث الوفيات",
                LabelValueEnglish = "Fatal accidents",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTSPERTYPE_FATAL")
            };
            return res;

        }

        public KpiDTO GetFatalInjuriesKPI()
        {
            var res = new KpiDTO
            {
                TargetName = "TARGET_ACCIDENTS_FATALITYINJURIES",
                CurrentValue = GetFatalInjuriesCountPerYear(DateTime.Now.Year),
                PreviousValue = GetFatalInjuriesCountPerYear(DateTime.Now.Year - 1),
                LabelValueArabic = "الاصابات الجسيمة",
                LabelValueEnglish = "Fatal Injuries",
                Percentage = GetTargetPercentage("TARGET_ACCIDENTS_FATALITYINJURIES")
            };
            return res;
        }

        private double GetTargetPercentage(string keyname)
        {
            double res = 0.00;
            var allTarget = new KPITargetsDAL().GetAllTargets();
            if (allTarget != null)
            {
                var target = allTarget.FirstOrDefault(x => x.TargetName == keyname);
                if (target != null)
                {
                    res = target.TargetValue;
                }
            }
            return res;
        }

        #endregion
    }
}
