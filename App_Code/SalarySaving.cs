using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Text;
using DBLibrary;
using Common;
using errorLog;
using AmountToWords;

/// <summary>
/// DESIGN BY VIRESH RAJPUT
/// Summary description for SalarySaving
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class SalarySaving : System.Web.Services.WebService
{
    csSaitex obj = null;
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
   
    
    private static decimal TLoanTotal = 0;
    private static decimal TOtherDeduction = 0;
    private static decimal TAdditionSalay = 0;

    private static string  EmployeEPF = string.Empty ;
    private static decimal ActualTotal = 0;
    private static decimal NetSalary = 0;
    private static string FiguretoWord = string.Empty;
    private static string EmployeeId = string.Empty;
    private static string SMonth = string.Empty;
    private static string STYear = string.Empty;
    //Change on 04-02-2011
    private static DataTable TakenLeave_Detail = new DataTable();
    private static DataTable BasicCalculation = new DataTable();
    private static DataTable BasicCalculation_Deduction = new DataTable();
    private static DataTable BasicCalculation_Deduction_Loans = new DataTable();
    private static DataTable SavingSubHeadTable = new DataTable();
    private static DataTable AvailableLeave_Detail = new DataTable();
    private static DataTable HoildayList = new DataTable();
    private static DataTable AbsentDate = new DataTable();
    private static DataTable CalaculationDT = new DataTable();

    private static decimal  BasicAmount = 0;
    private static string BasicID = string.Empty;
    private static int Flag = 0;
    
    public static string leaveName_cl = "", leaveName_pl = "", leaveName_ml = "", leaveName_sl = "", leaveName_el = "", leaveName_lwp = "", leaveName_cm = "", radFirstValue = "Full", radLastValue = "Full";
    public static decimal cl = 0, pl = 0, ml = 0, sl = 0, el = 0, CO = 0, withoutpay = 0;
    public static decimal Rcl = 0, Rpl = 0, Rml = 0, Rsl = 0, Rel = 0, RCO = 0, Rwithoutpay = 0;
    public static decimal Tcl = 0, Tpl = 0, Tml = 0, Tsl = 0, Tel = 0, TCO = 0, Twithoutpay = 0;
    ArrayList arr_Attendance = null;
    public static Int32 DayInmonth = 0;
    public static decimal PayDay = 0;
    private static DateTime  Emp_JoinDate;
    public static string  ToDate;
    public static string  FromDate;
    private static  DateTime SDate;   
    public static int FromYear;

    public static decimal MaxPF=6500;
    public static decimal MinPF=6200;
      

    [WebMethod(EnableSession = true)]
    public bool  SaveAlltheRecord(string SearchQuery, int Month, int Year)
    {

        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        bool Result=false ;
        try
        {
            
         
            SMonth = Convert.ToString(Month);
            STYear = Convert.ToString(Year);
            DataTable DTEmp = new DataTable();
            StringBuilder Lsql = new StringBuilder();
            Lsql.Append("SELECT EMP_CODE,TO_CHAR(JOIN_DT,'dd/MM/yyyy') AS JOININGDATE FROM HR_EMP_MST");
            Lsql.Append(" WHERE" + SearchQuery );
            OracleDataAdapter OCD = new OracleDataAdapter(Lsql.ToString(), con);
            OCD.Fill(DTEmp);
            foreach (DataRow DRow in DTEmp.Rows )
            {
                EmployeeId = DRow["EMP_CODE"].ToString().Trim();
                Emp_JoinDate = DateTime.Parse(DRow["JOININGDATE"].ToString().Trim());               
                    bool RES = Can_Genrate(EmployeeId, SMonth, STYear);
                    if (RES)
                    {
                        Result = Genrate_Salary();
                    }                
            }
            return Result;
        }        
        catch (System.Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
            throw ex;
        }       
    }   
    public bool Can_Genrate(string EMP_CODE, string Month, string Year)
    {
        bool REsult = false;
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        try
        {
            OracleCommand OCD = new OracleCommand();
            OCD.CommandText = "SELECT COUNT(*) FROM HR_SAL_SLIP_MST WHERE ltrim(rtrim(EMP_CODE))=:EMP_CODE AND ltrim(rtrim(SAL_MONTH))=:Month AND ltrim(rtrim(SAL_YEAR))=:Year";
            OCD.Parameters.AddWithValue(":EMP_CODE", EMP_CODE);
            OCD.Parameters.AddWithValue(":Month", Month);
            OCD.Parameters.AddWithValue(":Year", Year);
            OCD.CommandType = CommandType.Text;
            OCD.Connection = con;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            int res = int.Parse(OCD.ExecuteOracleScalar().ToString());
            if (res > 0)
            {
                REsult = false;
            }
            else
            {
                REsult = true;
            }
            return REsult;
        }
        catch (Exception oex)
        {
            throw oex;
        }
        finally
        {
            if (con != null) { con.Close(); con.Dispose(); con = null; }
        }
    }

    private void Initial_Control()
    {
        try
        {
           cl = 0; pl = 0; ml = 0; sl = 0; el = 0; CO = 0; withoutpay = 0;
            Rcl = 0; Rpl = 0; Rml = 0; Rsl = 0; Rel = 0; RCO = 0; Rwithoutpay = 0;
            Tcl = 0; Tpl = 0; Tml = 0; Tsl = 0; Tel = 0; TCO = 0; Twithoutpay = 0;
            PayDay = 0; Flag = 0; BasicAmount = 0; NetSalary = 0; ActualTotal = 0;
            TLoanTotal = 0; TOtherDeduction = 0; TAdditionSalay = 0;
        }
        catch
        {
            throw;
        }
    }
    private bool Genrate_Salary()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            FromDate = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oUserLoginDetail.SALARY_FROMDATE));
            ToDate = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oUserLoginDetail.SALARY_TODATE));
            if (oUserLoginDetail.SALARY_FROMDATE != null && oUserLoginDetail.SALARY_FROMDATE != "" && oUserLoginDetail.SALARY_TODATE != "" && oUserLoginDetail.SALARY_TODATE != null)
            {
                TimeSpan tDAYS = DateTime.Parse(oUserLoginDetail.SALARY_TODATE).Subtract(DateTime.Parse(oUserLoginDetail.SALARY_FROMDATE));
                DayInmonth = tDAYS.Days + 1;
            }
            else
            { DayInmonth = System.DateTime.DaysInMonth(System.DateTime.Now.Year, System.DateTime.Now.Month); }
                    
            Int32 Month = Convert.ToInt32(SMonth.Trim());
            Int32 Year = Convert.ToInt32(STYear.Trim());
            Create_Virtual_Table();
            Initial_Control();            
            Leave_Calc(Year, EmployeeId);           
            absent_date(FromDate, ToDate, EmployeeId);
            Taken_Leave_Dates(Month, Year, EmployeeId);
            GetNumberOfSundays(EmployeeId.Trim(), Month, Year);
            getOptionalHoildayForEmployee(EmployeeId, Month, Year);
            getLeaveDetail(Month, Year, EmployeeId);
            getAttandanceDetail(Month, Year, EmployeeId);            

            Count_Paid_Day();           
            GetBasicSalaryCalcuilation(EmployeeId);  
            getDecductionCalculation();
            getDecduction_LoansCalculation();
            GetActualSalary();           
            int result = Insert_SalaryRecord();
            bool res = false;
            if (result == 1)
            {
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
        catch
        {
            throw;
        }
    }
    private void Create_Virtual_Table()
    {
        try
        {
            BasicCalculation_Deduction_Loans = null;
            createDataTable_HoildayList();
            createDataTable_BasicCalculation();
            createDataTable_BasicCalculation_Deduction();
            createDataTable_BasicCalculation_Deduction_Loans();
        }
        catch
        {
            throw;
        }
    }        
    #region create Leave data table           
            private void createDataTable_BasicCalculation()
            {
                try
                {
                    DataTable dt_BasicCalculation = new DataTable();
                    DataColumn dt_colounm;

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryName";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryAmount";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryId";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryHeader";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation.Columns.Add(dt_colounm);

                    BasicCalculation = dt_BasicCalculation;
                }
                catch (Exception ex)
                {

                    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
                }
            }
            private void createDataTable_BasicCalculation_Deduction()
            {
                try
                {
                    DataTable dt_BasicCalculation_Deduction = new DataTable();
                    DataColumn dt_colounm;

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryName";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryAmount";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryId";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryHeader";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction.Columns.Add(dt_colounm);

                    BasicCalculation_Deduction = dt_BasicCalculation_Deduction;
                }
                catch (Exception ex)
                {

                    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
                }
            }
            private void createDataTable_BasicCalculation_Deduction_Loans()
            {
                try
                {
                    DataTable dt_BasicCalculation_Deduction_Loans = new DataTable();
                    DataColumn dt_colounm;

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryName";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction_Loans.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryAmount";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction_Loans.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryId";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction_Loans.Columns.Add(dt_colounm);

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "SalaryHeader";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_BasicCalculation_Deduction_Loans.Columns.Add(dt_colounm);

                    BasicCalculation_Deduction_Loans = dt_BasicCalculation_Deduction_Loans;
                }
                catch (Exception ex)
                {
                    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
                }
            }
            private void createDataTable_HoildayList()
            {
                try
                {
                    DataTable dt_HoildayList = new DataTable();
                    DataColumn dt_colounm;

                    dt_colounm = new DataColumn();
                    dt_colounm.ColumnName = "HolidayDay";
                    dt_colounm.DataType = Type.GetType("System.String");
                    dt_HoildayList.Columns.Add(dt_colounm);

                    HoildayList = dt_HoildayList;
                }
                catch (Exception ex)
                {
                    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
                }
            }           
    # endregion
    #region Fill Leave data table
    private void Leave_Calc(int Year, string EMP_CODE)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
            DataTable DT = new DataTable();
            StringBuilder Lsql = new StringBuilder();
            Lsql.Append("SELECT EMP_CODE,CUR_YEAR,TCASUAL,TkCASUAL,BCASUAL,TC,TEL,TkEL,BEL,TE,TSICK,TkSICK,BSICK,TS,TML,TkML,BML,TM,TOLW,TkLW,BLW,TLW,TCO,TKCO,BCO,TCOF");
            Lsql.Append(" FROM (SELECT EMP_CODE,CUR_YEAR,SUM (CASE WHEN LV_ID = 1 THEN LV_DAYS ELSE '0' END)TCASUAL,");
            Lsql.Append(" SUM (CASE WHEN LV_ID = 1 THEN TAKEN_LV_DAYS ELSE '0' END)TkCASUAL,SUM (CASE WHEN LV_ID = 1 THEN REMAIN_LV_DAYS ELSE '0' END)BCASUAL,");

            Lsql.Append(" (SELECT  NVL (SUM (L.DAYS_LV), 0) FROM   HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID WHERE TO_CHAR (L.LV_APP_DATE, 'yyyy') = :YEAR AND L.LV_APP_DATE");
            Lsql.Append(" BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM (RTRIM (L.EMP_CODE)) = :EMP_CODE  AND LTRIM (RTRIM (L.LV_TYPE)) = 1 AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' AND LTRIM (RTRIM (L.DEL_STATUS)) = '0') TC,");
            
            Lsql.Append(" SUM (CASE WHEN LV_ID = 2 THEN LV_DAYS ELSE '0' END) TEL,SUM (CASE WHEN LV_ID = 2 THEN TAKEN_LV_DAYS ELSE '0' END)TkEL,SUM (CASE WHEN LV_ID = 2 THEN REMAIN_LV_DAYS ELSE '0' END)BEL,");
            Lsql.Append(" (SELECT  NVL (SUM (L.DAYS_LV), 0) FROM   HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID WHERE TO_CHAR (L.LV_APP_DATE, 'yyyy') = :YEAR AND L.LV_APP_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy')");

            Lsql.Append(" AND ltrim(rtrim(L.EMP_CODE))=:EMP_CODE and ltrim(rtrim(L.LV_TYPE))='2' AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' AND LTRIM (RTRIM (L.DEL_STATUS)) = '0') TE,SUM (CASE WHEN LV_ID = 3 THEN LV_DAYS ELSE '0' END) TSICK,SUM (CASE WHEN LV_ID = 3 THEN TAKEN_LV_DAYS ELSE '0' END)TkSICK,");
            Lsql.Append(" SUM (CASE WHEN LV_ID = 3 THEN REMAIN_LV_DAYS ELSE '0' END)BSICK,(SELECT  NVL (SUM (L.DAYS_LV), 0) FROM   HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID WHERE TO_CHAR (L.LV_APP_DATE, 'yyyy') = :YEAR AND L.LV_APP_DATE");

            Lsql.Append(" BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') and ltrim(rtrim(L.EMP_CODE))=:EMP_CODE and ltrim(rtrim(L.LV_TYPE))='3'AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' AND LTRIM (RTRIM (L.DEL_STATUS)) = '0') TS,");
            Lsql.Append(" SUM (CASE WHEN LV_ID = 4 THEN LV_DAYS ELSE '0' END) TML,SUM (CASE WHEN LV_ID = 4 THEN TAKEN_LV_DAYS ELSE '0' END)TkML,SUM (CASE WHEN LV_ID = 4 THEN REMAIN_LV_DAYS ELSE '0' END)BML,");

            Lsql.Append(" (SELECT  NVL (SUM (L.DAYS_LV), 0) FROM   HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID WHERE TO_CHAR (L.LV_APP_DATE, 'yyyy') = :YEAR AND L.LV_APP_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') and ltrim(rtrim(L.EMP_CODE))=:EMP_CODE and ltrim(rtrim(L.LV_TYPE))='4'AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' AND LTRIM (RTRIM (L.DEL_STATUS)) = '0') TM,");
            Lsql.Append(" SUM (CASE WHEN LV_ID = 7 THEN LV_DAYS ELSE '0' END) TOLW,SUM (CASE WHEN LV_ID = 7 THEN TAKEN_LV_DAYS ELSE '0' END)TkLW,SUM (CASE WHEN LV_ID = 7 THEN REMAIN_LV_DAYS ELSE '0' END)BLW,");

            Lsql.Append(" (SELECT  NVL (SUM (L.DAYS_LV), 0) FROM   HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID WHERE TO_CHAR (L.LV_APP_DATE, 'yyyy') = :YEAR AND L.LV_APP_DATE");
            Lsql.Append(" BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') and ltrim(rtrim(L.EMP_CODE))=:EMP_CODE and ltrim(rtrim(L.LV_TYPE))='7'AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' AND LTRIM (RTRIM (L.DEL_STATUS)) = '0') TLW,");
            Lsql.Append(" SUM (CASE WHEN LV_ID = 9 THEN LV_DAYS ELSE '0' END) TCO,SUM (CASE WHEN LV_ID = 9 THEN TAKEN_LV_DAYS ELSE '0' END)TKCO,SUM (CASE WHEN LV_ID = 9 THEN REMAIN_LV_DAYS ELSE '0' END)BCO,");

            Lsql.Append(" (SELECT  NVL (SUM (L.DAYS_LV), 0) FROM   HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID WHERE TO_CHAR (L.LV_APP_DATE, 'yyyy') = :YEAR AND L.LV_APP_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy')");
            Lsql.Append(" and ltrim(rtrim(L.EMP_CODE))=:EMP_CODE and ltrim(rtrim(L.LV_TYPE))='9'AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' AND LTRIM (RTRIM (L.DEL_STATUS)) = '0') TCOF FROM   HR_EMP_LV WHERE EMP_CODE=:EMP_CODE GROUP BY   EMP_CODE, CUR_YEAR, DEL_STATUS) WHERE CUR_YEAR=:YEAR");
            
            OracleCommand NewCom = new OracleCommand(Lsql.ToString(), con);
            NewCom.CommandType = CommandType.Text;
            NewCom.Parameters.AddWithValue(":EMP_CODE",EMP_CODE );
            NewCom.Parameters.AddWithValue(":YEAR", Year );
            NewCom.Parameters.AddWithValue(":FROMDATE", FromDate);
            NewCom.Parameters.AddWithValue(":TODATE", ToDate);

            OracleDataAdapter ODA = new OracleDataAdapter(NewCom);
            ODA.Fill(DT);
            foreach (DataRow Drow in DT.Rows) 
            {
                 //Opening Leave
                cl = decimal.Parse(Drow["TCASUAL"].ToString());
                ml = decimal.Parse(Drow["TML"].ToString());
                sl = decimal.Parse(Drow["TSICK"].ToString());
                el = decimal.Parse(Drow["TEL"].ToString());
                CO = decimal.Parse(Drow["TCO"].ToString());
                withoutpay = decimal.Parse(Drow["TOLW"].ToString());

                //Remaining Leave
                Rcl = decimal.Parse(Drow["BCASUAL"].ToString());
                Rml = decimal.Parse(Drow["BML"].ToString());
                Rsl = decimal.Parse(Drow["BSICK"].ToString());
                Rel = decimal.Parse(Drow["BEL"].ToString());
                RCO = decimal.Parse(Drow["BCO"].ToString());
                Rwithoutpay = decimal.Parse(Drow["BLW"].ToString());

                //Taken Leave
                Tcl = decimal.Parse(Drow["TC"].ToString());
                Tml = decimal.Parse(Drow["TM"].ToString());
                Tsl = decimal.Parse(Drow["TS"].ToString());
                Tel = decimal.Parse(Drow["TE"].ToString());
                TCO = decimal.Parse(Drow["TCOF"].ToString());
                Twithoutpay = decimal.Parse(Drow["TLW"].ToString());               
            }
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }        
    }
    private void Taken_Leave_Dates(int month, int Year, string EMP_CODE)
    {
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        try
        {
            TakenLeave_Detail.Rows.Clear();
            StringBuilder LSql = new StringBuilder();
            LSql.Append("SELECT  L.EMP_CODE,L.LV_APP_DATE,L.LV_TYPE FROM HR_LV_APP_DTL L");
            LSql.Append(" LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID");
            LSql.Append(" WHERE to_char(L.LV_APP_DATE,'yyyy')='" + Year + "' AND L.LV_APP_DATE BETWEEN To_Date('" + FromDate + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') and");
            LSql.Append(" ltrim(rtrim(L.EMP_CODE))='" + EMP_CODE + "' and ltrim(rtrim(L.STATUS))='1'");
            LSql.Append("  AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' and ltrim(rtrim(L.DEL_STATUS))='0'");
            LSql.Append(" ORDER BY LV_APP_DATE");
            OracleDataAdapter OCD = new OracleDataAdapter(LSql.ToString(), con);
            OCD.Fill(TakenLeave_Detail);
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }        
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }       
    }
    # endregion
    # region Calculation Salary
    private void GetNumberOfSundays(string EMP_CODE, int Month, int Year)
    {
        int dubRecord = 0;
        DataTable dt1 = HoildayList;
        DataTable DTStart = new DataTable();
        DataRow rw;
        try
        {
            DateTime Startdate = DateTime.Parse(FromDate.ToString());
            string Start = string.Format("{0:dd/MM/yyyy}", Startdate);
            if (Emp_JoinDate > Startdate)
            {
                Start = string.Format("{0:dd/MM/yyyy}", Emp_JoinDate);
            }
            StringBuilder Lsql = new StringBuilder();
            Lsql.Append("SELECT * FROM(SELECT to_char(ATTN_DATE,'dd/MM/yyyy') StartDate FROM HR_ATTN_TRN");
            Lsql.Append(" WHERE ltrim(rtrim(EMP_CODE))='" + EMP_CODE + "' AND ATTN_DATE BETWEEN To_Date ('" + Start + "', 'dd/MM/yyyy')");
            Lsql.Append(" AND To_Date ('" + ToDate + "','dd/MM/yyyy') AND to_char(ATTN_DATE,'yyyy')='" + Year + "' AND ltrim(rtrim(DEL_STATUS))='0'");
            Lsql.Append(" ORDER BY ATTN_DATE asc)");
            Lsql.Append(" WHERE rownum <=1");
            OracleDataAdapter OCD = new OracleDataAdapter(Lsql.ToString(), con);
            OCD.Fill(DTStart);
            if (DTStart.Rows.Count > 0)
            {
                SDate = DateTime.Parse(DTStart.Rows[0]["StartDate"].ToString().Trim());
            }
            else
            {
                SDate = DateTime.Parse(FromDate.ToString());
            }
            ArrayList arr_Sunday = new ArrayList();
            if (SDate > Emp_JoinDate)
            {
                SDate = DateTime.Parse(FromDate.ToString());
            }
            DateTime dayt = SDate;
            DateTime TDate = DateTime.Parse(ToDate.ToString());
            while (dayt <= TDate)
            {

                if (dayt.DayOfWeek == DayOfWeek.Sunday)
                {
                    dubRecord = 0;
                    string SundayDate = String.Format("{0:dd/MM/yyyy}", dayt);
                    string PreDate = String.Format("{0:dd/MM/yyyy}", dayt.AddDays(-1));
                    string PostDate = String.Format("{0:dd/MM/yyyy}", dayt.AddDays(1));
                    if (ChkLeave(PreDate))
                    {
                        if (ChkLeave(PostDate))
                        {
                            dubRecord = 1;
                        }
                        else { Flag = 0; }
                    }
                    else
                    { Flag = 0; }
                    if (dubRecord != 1)
                    {
                        rw = dt1.NewRow();
                        rw["HolidayDay"] = Convert.ToString(dayt);
                        dt1.Rows.Add(rw);
                    }

                }
                dayt = dayt.AddDays(1);
            }
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }

    }
    private bool ChkLeave(string ChkDate)
    {
        bool Res = false;
        Flag = 0;
        foreach (DataRow DTRow in AbsentDate.Rows)
        {
            string Absent = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(DTRow["ABSENT"].ToString()));
            if (ChkDate == Absent)
            {
                Flag = 1;
                break;                
            }
            else
            {
                Flag = 0;
            }
        }
        if (Flag == 1)
        {
            Res = true;
            return Res;
        }
        foreach (DataRow DRow in TakenLeave_Detail.Rows)
        {
            string LeaveDate = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(DRow["LV_APP_DATE"].ToString()));
            if (ChkDate == LeaveDate)
            {
                Flag = 1;
                break;
            }
            else
            {
                Flag = 0;
            }
        }        
        if (Flag == 1)
        {
            Res = true;
        }
        else
        {
            Res = false;
        }
        return Res;
    }
    private void absent_date(string FromDate, string EDATE, string EMP_CODE)
    {
        try
        {
            AbsentDate = SaitexBL.Interface.Method.HR_ATTN_TRN.ABSENT_IN_MONTH(FromDate, ToDate, EMP_CODE);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
    }
    
    private void getOptionalHoildayForEmployee(string EMP_CODE, int Month, int Year)
    {

        try
        {
            ArrayList arrOptionalLeave = new ArrayList();
            DataTable dt = HoildayList;
            DataRow rw;
            DateTime Startdate = DateTime.Parse(FromDate.ToString());
            string Start = string.Format("{0:dd/MM/yyyy}", Startdate);
            if (Emp_JoinDate > Startdate)
            {
                Start = string.Format("{0:dd/MM/yyyy}", Emp_JoinDate);
            }
            int Dubresult = 0;

            string strOptionalLeave = "";
            strOptionalLeave = "SELECT EMP_OPT_LV_DTL_ID,eop.EMP_CODE,eop.HLD_ID, EMP_OPT_REMARKS,HLD_DATE FROM HR_EMP_OPT_LV_DTL eop,HR_HLD_MST hm where hm.STATUS='0' and ltrim(rtrim(eop.DEL_STATUS))='0' and ltrim(rtrim(hm.DEL_STATUS))='0' and eop.HLD_ID=hm.HLD_ID and ltrim(rtrim(EMP_CODE))='" + EMP_CODE + "' AND HLD_DATE BETWEEN To_Date ('" + Start + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') AND  substr(HLD_DATE,7,4)='" + Convert.ToString(Year).Trim() + "' and eop.EMP_CODE in (select EMP_CODE from HR_LV_APP_DTL where  ltrim(rtrim(EMP_CODE))='" + EMP_CODE + "' and ltrim(rtrim(DEL_STATUS))='0' AND LV_APP_DATE BETWEEN To_Date ('" + Start + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') and  to_char(LV_APP_DATE,'yyyy')='" + Convert.ToString(Year).Trim() + "' and LV_ADMINLOCKING='1' and SUP_ADMIN_LOCK='1' and ltrim(rtrim(DEL_STATUS))='0' ) and HLD_DATE not in (select LV_APP_DATE from HR_LV_APP_DTL where  ltrim(rtrim(EMP_CODE))='" + EMP_CODE + "' AND LV_APP_DATE BETWEEN To_Date ('" + Start + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') and  to_char(LV_APP_DATE,'yyyy')='" + Convert.ToString(Year).Trim() + "' and LV_ADMINLOCKING='1' and SUP_ADMIN_LOCK='1' and ltrim(rtrim(DEL_STATUS))='0')";
            OracleDataAdapter ocd = new OracleDataAdapter(strOptionalLeave, con);
            DataTable DTHOLI = new DataTable();
            ocd.Fill(DTHOLI);
            foreach (DataRow rw1 in DTHOLI.Rows)
            {
                Dubresult = 0;
                foreach (DataRow dt1 in dt.Rows)
                {
                    if (Convert.ToString(rw1["LV_APP_DATE"]) == Convert.ToString(dt1["HolidayDay"]))
                    { Dubresult = 1; }

                }
                if (Dubresult != 1)
                {
                    arrOptionalLeave.Add(Convert.ToString(rw1["LV_APP_DATE"]));
                }
            }
            DTHOLI.Dispose();
            for (int i = 0; i < arrOptionalLeave.Count; i++)
            {
                rw = dt.NewRow();
                rw["HolidayDay"] = Convert.ToString(arrOptionalLeave[i]);
                dt.Rows.Add(rw);
            }
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
    }
    private void getLeaveDetail(int month, int year, string EMP_CODE)
    {
        DataTable dt = HoildayList;
        DataRow rw;
        ArrayList arr_Hoilday = new ArrayList();
        string  HldDate=string.Empty ;
        DateTime Hld_Date;
        try
        {
            DateTime Startdate = DateTime.Parse(FromDate.ToString());
            string Start = string.Format("{0:dd/MM/yyyy}", Startdate);
            if (Emp_JoinDate > Startdate)
            {
                Start = string.Format("{0:dd/MM/yyyy}", Emp_JoinDate);
            }
            string strHoliday = "SELECT HLD_ID,YEAR,OPT_LV,HLD_NAME,HLD_DATE FROM HR_HLD_MST where ltrim(rtrim(OPT_LV))='N' AND HLD_DATE BETWEEN To_Date ('" + Start + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') AND to_char(HLD_DATE,'yyyy')='" + year + "' and ltrim(rtrim(DEL_STATUS))='0'";
            OracleDataAdapter OCD = new OracleDataAdapter(strHoliday, con);
            DataTable DTable = new DataTable();
            OCD.Fill(DTable);
            foreach (DataRow dr in DTable.Rows)
            {
                int result = 0;
                Hld_Date = DateTime.Parse(dr["HLD_DATE"].ToString().Trim());
                HldDate = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(dr["HLD_DATE"].ToString().Trim()));
                foreach (DataRow rw1 in dt.Rows)
                {
                    if (HldDate == String.Format("{0:dd/MM/yyyy}", DateTime.Parse(rw1["HolidayDay"].ToString().Trim())))
                    {
                        result = 1;
                    }
                }
                if (result != 1)
                {
                    string PreDate = String.Format("{0:dd/MM/yyyy}", Hld_Date.AddDays(-1));
                    string PostDate = String.Format("{0:dd/MM/yyyy}", Hld_Date.AddDays(1));
                    if (ChkLeave(PreDate))
                    {
                        if (ChkLeave(PostDate))
                        {
                            result = 1;
                        }
                        else { result = 0; }
                    }
                    else
                    { result = 0; }
                }                
                if (result == 0)
                {
                    rw = dt.NewRow();
                    rw["HolidayDay"] = Hld_Date ;
                    dt.Rows.Add(rw);                    
                }
            }           
            
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }       
    }  
    private void getAttandanceDetail(int month, int year, string EMP_CODE)
    {
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        try
        {
            arr_Attendance = new ArrayList();
            DataTable DTAttend = new DataTable();
            StringBuilder lSQL = new StringBuilder();
            lSQL.Append("WITH TAB AS(SELECT ATTN_DATE FROM HR_ATTN_TRN WHERE TO_CHAR(ATTN_DATE,'Dy') != 'Sun' AND ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "'");
            lSQL.Append(" AND ATTN_DATE BETWEEN To_Date ('" + FromDate + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') AND to_char(ATTN_DATE,'yyyy')='" + year + "'");
            lSQL.Append(" and ltrim(rtrim(DEL_STATUS))='0' ORDER BY ATTN_DATE )");
            lSQL.Append(" SELECT ATTN_DATE FROM TAB MINUS");
            lSQL.Append(" SELECT HLD_DATE FROM HR_HLD_MST where ltrim(rtrim(OPT_LV))='N' AND HLD_DATE BETWEEN To_Date ('" + FromDate + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') ");
            lSQL.Append(" AND to_char(HLD_DATE,'yyyy')='" + year + "' and ltrim(rtrim(DEL_STATUS))='0'");
            lSQL.Append(" MINUS SELECT  L.LV_APP_DATE AS ATTN_DATE FROM HR_LV_APP_DTL L LEFT JOIN HR_LV_APP_FORM_DTL AP ON AP.LV_APP_ID=L.LV_APP_FORM_ID");
            lSQL.Append(" WHERE to_char(L.LV_APP_DATE,'yyyy')='" + year + "' AND L.LV_APP_DATE BETWEEN To_Date('" + FromDate + "', 'dd/MM/yyyy') AND To_Date ('" + ToDate + "','dd/MM/yyyy') and");
            lSQL.Append(" ltrim(rtrim(L.EMP_CODE))='" + EMP_CODE + "' and ltrim(rtrim(L.STATUS))='1'");
            lSQL.Append(" AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' and ltrim(rtrim(L.DEL_STATUS))='0'");
            //lSQL.Append(" AND LTRIM (RTRIM (AP.LV_STATUS)) = 'A' and ltrim(rtrim(L.DEL_STATUS))='0' AND LTRIM(RTRIM(L.DAYS_LV))<>'0.5'");
            OracleDataAdapter OCD = new OracleDataAdapter(lSQL.ToString(), con);
            OCD.Fill(DTAttend);
            foreach (DataRow DR in DTAttend.Rows)
            {
                arr_Attendance.Add(Convert.ToDateTime(DR["ATTN_DATE"].ToString().Trim()).ToShortDateString());
            }
            DTAttend.Dispose();
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
    }
    private void Count_Paid_Day()
    {
        try
        {
           
            DataTable dt = HoildayList;
            PayDay = 0;
            PayDay = (decimal.Parse(arr_Attendance.Count.ToString()) + decimal.Parse(dt.Rows.Count.ToString()) + Tcl + Tel + Tsl + Tml + Tpl + TCO);
            if (PayDay >= 28)
            {
                decimal p = 0;
                if (PayDay > DayInmonth)
                {
                    p = DayInmonth - Twithoutpay;
                }
                else
                {
                    p = PayDay - Twithoutpay;
                }
                PayDay = Math.Ceiling(p);               
            }            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
    }
    private void GetBasicSalaryCalcuilation(string EMP_CODE)
    {
        try
        {
            decimal basic = 0;
            DataTable DTable = new DataTable();
            string Lsql = "";
            Lsql = "SELECT a.SAL_GRADE_ID,a.GRADE_ID,a.HEAD_ID,b.SUBH_SLIP_FLD_NAME,a.SUBH_ID, a.AMT,a.TDATE,a.EMP_CODE, b.SUBH_NAME,b.SUBH_CAT,b.SUBH_TYPE,b.SUBH_SAL_TYPE FROM HR_EMP_SAL_MST a, HR_SUBH_MST b where a.SUBH_ID=b.SUBH_ID and ltrim(rtrim(EMP_CODE))='" + EMP_CODE + "' and ltrim(rtrim(a.DEL_STATUS))='0' and ltrim(rtrim(b.DEL_STATUS))='0' order by b.SUBH_CAT desc";
            OracleDataAdapter OCD = new OracleDataAdapter(Lsql, con);
            OCD.Fill(DTable);
            CalaculationDT = DTable;        
            foreach (DataRow dr in DTable.Rows)
                {
                    decimal Amount = 0;
                    if (dr["SUBH_CAT"].ToString().Trim() == "S" || dr["SUBH_CAT"].ToString().Trim() == "A" || dr["SUBH_CAT"].ToString().Trim() == "P")
                    {
                        if (dr["SUBH_CAT"].ToString().Trim() == "S")
                        {
                            if (dr["SUBH_SAL_TYPE"].ToString().Trim() == "B")
                            {
                                basic = Convert.ToDecimal(dr["AMT"].ToString().Trim());
                                basic = ((Convert.ToDecimal(basic) / Convert.ToDecimal(DayInmonth)) * (PayDay));
                                BasicAmount = basic;
                                BasicID = dr["SUBH_ID"].ToString().Trim();
                                Amount = basic;
                            }
                        }
                        else if (dr["SUBH_CAT"].ToString().Trim() == "A")
                        {
                            if (dr["SUBH_TYPE"].ToString().Trim() == "P")
                            {
                                Amount = (basic * decimal.Parse(dr["AMT"].ToString().Trim())) / 100;
                            }
                            else if (dr["SUBH_TYPE"].ToString().Trim() == "R")
                            {
                                Amount = Convert.ToDecimal ((decimal.Parse(dr["AMT"].ToString().Trim()) / Convert.ToDecimal(DayInmonth)) * (PayDay));
                            }
                        }
                        else if (dr["SUBH_CAT"].ToString().Trim() == "P")
                        {
                            if (dr["SUBH_TYPE"].ToString().Trim() == "R")
                            {
                                Amount = decimal.Parse(dr["AMT"].ToString().Trim());
                            }
                            else if (dr["SUBH_TYPE"].ToString().Trim() == "P")
                            {
                                Amount = (basic * decimal.Parse(dr["AMT"].ToString().Trim())) / 100;
                            }
                        }
                        DataTable dt = BasicCalculation;
                        DataRow rw;
                        rw = dt.NewRow();
                        rw["SalaryName"] = dr["SUBH_NAME"].ToString().Trim();
                        rw["SalaryAmount"] = Amount;
                        rw["SalaryId"] = dr["SUBH_ID"].ToString().Trim();
                        rw["SalaryHeader"] = dr["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                        dt.Rows.Add(rw);
                    }

                }
        }
        catch (OracleException ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
    }
    private void getDecductionCalculation()
    {
        try
        {
            decimal basic = 0;
            DataTable datatable_dt = HoildayList;
            DataTable DTAB = CalaculationDT;           
            basic = BasicAmount;
            foreach (DataRow dr in DTAB.Rows)
                {
                    decimal Amount = 0;
                    if (dr["AMT"].ToString().Trim() != "0")
                    {
                        if (dr["SUBH_CAT"].ToString().Trim() == "D")
                        {
                            if (dr["SUBH_SAL_TYPE"].ToString().Trim() == "P" || dr["SUBH_SAL_TYPE"].ToString().Trim() == "O")
                            {
                                if (dr["SUBH_TYPE"].ToString().Trim() == "P")
                                {
                                    Amount = (basic * decimal.Parse(dr["AMT"].ToString().Trim())) / 100;
                                }
                                else if (dr["SUBH_TYPE"].ToString().Trim() == "R")
                                {
                                    Amount = Convert.ToDecimal(dr["AMT"].ToString().Trim());
                                }
                               if (dr["SUBH_TYPE"].ToString().Trim() == "P" && dr["SUBH_SAL_TYPE"].ToString().Trim() == "P")
                                    {
                                        if (MaxPF < basic)
                                        {
                                            Amount = Convert.ToDecimal(((MaxPF / Convert.ToDecimal(DayInmonth)) * (PayDay)) * 12 / 100);
                                        }
                                        else
                                        {
                                            Amount = Convert.ToDecimal(((MinPF / Convert.ToDecimal(DayInmonth)) * (PayDay)) * 12 / 100);
                                        }

                                    }
                            }

                            DataTable dt = BasicCalculation_Deduction;
                            DataRow rw;
                            rw = dt.NewRow();
                            rw["SalaryName"] = dr["SUBH_NAME"].ToString().Trim();
                            rw["SalaryAmount"] = Amount;
                            rw["SalaryId"] = dr["SUBH_ID"].ToString().Trim();
                            rw["SalaryHeader"] = dr["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                            dt.Rows.Add(rw);
                        }
                    }
                    else
                    {
                        Amount = 0;                        
                        DataTable dt = BasicCalculation_Deduction;
                        DataRow rw;
                        rw = dt.NewRow();
                        rw["SalaryName"] = dr["SUBH_NAME"].ToString().Trim();
                        rw["SalaryAmount"] = Amount;
                        rw["SalaryId"] = dr["SUBH_ID"].ToString().Trim();
                        rw["SalaryHeader"] = dr["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                        dt.Rows.Add(rw);
                    }
                }            

        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }        
    }
    private void getDecduction_LoansCalculation()
    {
        try
        {
            decimal basic = 0;
            DataTable DT = CalaculationDT;            
           basic = BasicAmount;            
            foreach (DataRow dr in DT.Rows)
                {

                    decimal Amount = 0;

                    if (dr["SUBH_CAT"].ToString().Trim() == "D")
                    {
                        if (dr["SUBH_SAL_TYPE"].ToString().Trim() == "L")
                        {
                            if (dr["SUBH_TYPE"].ToString().Trim() == "P")
                            {
                                Amount = (basic * decimal.Parse(dr["AMT"].ToString().Trim())) / 100;
                            }
                            else
                            {
                                Amount = decimal.Parse(dr["AMT"].ToString().Trim());
                            }
                            DataTable dt = BasicCalculation_Deduction_Loans;
                            DataRow rw;
                            rw = dt.NewRow();
                            rw["SalaryName"] = dr["SUBH_NAME"].ToString().Trim();
                            rw["SalaryAmount"] = Amount;
                            rw["SalaryId"] = dr["SUBH_ID"].ToString().Trim();
                            rw["SalaryHeader"] = dr["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                            dt.Rows.Add(rw);
                        }
                    }                
            }
        }
        catch (OracleException ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }        
    }   
    private void GetActualSalary()
    {
        try
        {
            ActualTotal = TAdditionSalay - (TLoanTotal + TOtherDeduction);
            RupeesToWord o1 = new RupeesToWord();
            NetSalary = ActualTotal;
            FiguretoWord = o1.changeNumericToWords(ActualTotal.ToString()) + " only";
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }        
    }  
    /* -----------------  Create Data Table  -----------------*/
    private void createSavingSubHeadTable()
    {
        try
        {
            DataTable dt_SavingSubHeadTable = new DataTable();
            DataColumn dt_colounm;

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "SubHeadName";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_SavingSubHeadTable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "SalaryAmount";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_SavingSubHeadTable.Columns.Add(dt_colounm);

            SavingSubHeadTable = dt_SavingSubHeadTable;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
    }
    private void AddFieldinSavingSubHeadTable()
    {
        try
        {
            DataTable dt = SavingSubHeadTable;
            DataRow rw;
            for (int i = 1; i < 31; i++)
            {
                rw = dt.NewRow();
                rw["SubHeadName"] = "SUBH" + Convert.ToString(i);
                rw["SalaryAmount"] = "0";
                dt.Rows.Add(rw);
            }
            SavingSubHeadTable = dt;
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        
    }
    private void fillingDataTable_SavingSubHeadTable()
    {
        try
        {
            TAdditionSalay = 0;
            TOtherDeduction = 0;
            TLoanTotal = 0;

            foreach (DataRow rw1 in SavingSubHeadTable.Rows)
            {
                foreach (DataRow rw2 in BasicCalculation.Rows)
                {
                    if (rw1["SubHeadName"].ToString().Trim() == rw2["SalaryHeader"].ToString().Trim())
                    {
                        rw1["SalaryAmount"] = rw2["SalaryAmount"].ToString().Trim();
                        rw1.AcceptChanges();
                        TAdditionSalay = TAdditionSalay + decimal.Parse(rw2["SalaryAmount"].ToString().Trim());

                    }
                }

            }

            foreach (DataRow rw3 in SavingSubHeadTable.Rows)
            {
                foreach (DataRow rw4 in BasicCalculation_Deduction.Rows)
                {
                    if (rw3["SubHeadName"].ToString().Trim() == rw4["SalaryHeader"].ToString().Trim())
                    {
                        rw3["SalaryAmount"] = rw4["SalaryAmount"].ToString().Trim();
                        rw3.AcceptChanges();
                        TOtherDeduction = TOtherDeduction + decimal.Parse(rw4["SalaryAmount"].ToString().Trim());

                    }
                }

            }

            foreach (DataRow rw5 in SavingSubHeadTable.Rows)
            {
                foreach (DataRow rw6 in BasicCalculation_Deduction_Loans.Rows)
                {
                    if (rw5["SubHeadName"].ToString().Trim() == rw6["SalaryHeader"].ToString().Trim())
                    {
                        rw5["SalaryAmount"] = rw6["SalaryAmount"].ToString().Trim();
                        rw5.AcceptChanges();
                        TLoanTotal = TLoanTotal + decimal.Parse(rw6["SalaryAmount"].ToString().Trim());

                    }
                }

            }
        }
        catch (OracleException ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }
        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
        }

    }    
    # endregion
    protected int Insert_SalaryRecord()
    {
        int iRecordEffected = 0;

        try
        {
            int iRecordFound = 0;
            ///////////////////////////////////////  Code to insert the data /////////////////////////////////
            DataTable dt = HoildayList;
            createSavingSubHeadTable();
            AddFieldinSavingSubHeadTable();
            fillingDataTable_SavingSubHeadTable();
            GetActualSalary();           
            if (iRecordFound == 0)
            {

                if (con.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                   
                    cmd = new OracleCommand("P_HR_SAL_SLIP_MST_INSERT",con );                   
                    cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("P_EMP_CODE", CommonFuction.funFixQuotes(EmployeeId));
                cmd.Parameters.AddWithValue("P_SAL_MONTH", CommonFuction.funFixQuotes(SMonth));
                cmd.Parameters.AddWithValue("P_SAL_YEAR", CommonFuction.funFixQuotes(STYear.Trim()));
                cmd.Parameters.AddWithValue("P_SAL_WORKING_DAY", CommonFuction.funFixQuotes(String.Format("{0:0.00}", arr_Attendance.Count)));
                cmd.Parameters.AddWithValue("P_HLD", CommonFuction.funFixQuotes(String.Format("{0:0.00}", dt.Rows.Count)));
                cmd.Parameters.AddWithValue("P_PAID_DAY",CommonFuction.funFixQuotes(String.Format("{0:0.00}", PayDay)));
                 cmd.Parameters.AddWithValue("P_NET_SAL",Math.Round (NetSalary,0));
                 cmd.Parameters.AddWithValue("P_EPF","0");
                 cmd.Parameters.AddWithValue("P_ERN_AMT",String.Format("{0:0.00}",Math.Round (TAdditionSalay,2)));
                 cmd.Parameters.AddWithValue("P_LOAN_AMT",String.Format("{0:0.00}",Math.Round (TLoanTotal,2)));

                  cmd.Parameters.AddWithValue("P_DEDCT_AMT",String.Format("{0:0.00}",Math.Round (TOtherDeduction,2)));
                  cmd.Parameters.AddWithValue("P_CASUAL_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rcl)));
                    cmd.Parameters.AddWithValue("P_SICK_LV",CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rsl)));
                    cmd.Parameters.AddWithValue("P_EARN_LV",CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rel)));

                    param = new OracleParameter("P_MATERNITY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rml));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_PATERNITY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rpl));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_COMPENSATORY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", RCO));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_LV_WITHOUT_PAY", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rwithoutpay));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_CASUAL_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", cl));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_SICK_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", sl));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_EARN_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", el));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_MATERNITY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", ml));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_PATERNITY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", pl));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_COMPENSATORY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", CO));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_OPENING_LV_WITHOUT_PAY", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", withoutpay));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_AVIAL_CASUAL_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tcl ));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);


                    param = new OracleParameter("P_AVIAL_SICK_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tsl));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_AVIAL_EARN_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tel));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_AVIAL_MATERNITY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tml));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_AVIAL_PATERNITY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tpl));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_AVIAL_COMPENSATORY_LV", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", TCO));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_AVIAL_LV_WITHOUT_PAY", OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Twithoutpay));
                    
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);


                    foreach (DataRow rw in SavingSubHeadTable.Rows)
                    {
                        string FieldName = "P_" + rw["SubHeadName"].ToString().Trim();
                        param = new OracleParameter(FieldName, OracleType.VarChar, 10);
                        param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Math.Round(decimal.Parse(rw["SalaryAmount"].ToString().Trim()),2)));
                        param.Direction = ParameterDirection.Input;
                        cmd.Parameters.Add(param);
                    }

                    param = new OracleParameter("P_LOCK_LV", OracleType.Char, 7);
                    param.Value = "UnLock";
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_SAVE_STATUS", OracleType.Char, 2);
                    param.Value = "A";
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_DEL_STATUS", OracleType.Char, 1);
                    param.Value = "0";
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_TDATE", OracleType.DateTime);
                    param.Value = System.DateTime.Now.ToShortDateString();
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter("P_TUSER", OracleType.VarChar, 10);
                    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                    param.Value = oUserLoginDetail.UserCode;
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);

                    if (arr_Attendance.Count > 0 )
                    {
                        iRecordEffected = cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        iRecordEffected = 0;
                    }              
            }
            return iRecordEffected;
        }

        catch (OracleException ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
            return 0;
        }

        catch (Exception ex)
        {

            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Salary Saving.\r\nSee error log for detail."));
            return 0;
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            if (param != null)
            {
                param = null;
            }

        }
       
    }
}

