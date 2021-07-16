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
/// maa durga
/// 26-july-2011 viresh rajput new salary 
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]

public class ReGenerateSalary : System.Web.Services.WebService
{
    #region MyClass definition
    OracleConnection OracleCon;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
            private static string SalaryStartDate = string.Empty,SalaryEndDate = string.Empty,SMonth = string.Empty,STYear = string.Empty;       

            private static string EMP_CODE = string.Empty;
            private static string EMP_CADER_CODE = string.Empty;
            private static string EMP_TYPE = string.Empty;
            private static decimal  DayInmonth = 0,PAID_DAYS=0,ABSENT_DAYS=0,PRESENT_DAY=0,NH=0,WO,AttendanceAllownces=0;

            private static decimal cl = 0, pl = 0, ml = 0, sl = 0, el = 0, CO = 0, withoutpay = 0;
            private static decimal Rcl = 0, Rpl = 0, Rml = 0, Rsl = 0, Rel = 0, RCO = 0, Rwithoutpay = 0;
            private static decimal Tcl = 0, Tpl = 0, Tml = 0, Tsl = 0, Tel = 0, TCO = 0, Twithoutpay = 0;
            private static decimal TLoanTotal = 0, BasicAmount=0,TOtherDeduction = 0, TAdditionSalay = 0, ActualTotal = 0, NetSalary = 0;
            private static string EmployeEPF = string.Empty,FiguretoWord = string.Empty;

            private  DataTable Temp_Salary_Table=new DataTable();
            private  DataTable Temp_Deduction_Table = new DataTable();
            private  DataTable Temp_Deduction_Loans_Table = new DataTable();
            private  DataTable SavingSubHeadTable = new DataTable();
            private static DataTable CalaculationDT = new DataTable();

            public static decimal MaxPF = 6500;

           

    #endregion
    [WebMethod(EnableSession = true)]
     public bool Employee_Salary(string SearchQuery, Int32 SalaryMonth, Int32 SalaryYear)
      {
         try
           {
             SMonth=SalaryMonth.ToString();
             STYear=SalaryYear.ToString();
             oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
             SalaryStartDate = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oUserLoginDetail.SALARY_FROMDATE));
             SalaryEndDate = String.Format("{0:dd/MM/yyyy}", DateTime.Parse(oUserLoginDetail.SALARY_TODATE));
             Initial_Records();
             if (Can_Genrate())
             {
                 if (Genrate_EMP_Salary(SearchQuery, SalaryMonth, SalaryYear))
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
           catch(Exception ex)
           {
               errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
               throw ex;
           }
      }
    private bool Can_Genrate()
    {
        bool rESULT = false;
        OracleCon = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        try
        {
            DateTime From_Date = DateTime.Parse(oUserLoginDetail.SALARY_FROMDATE).Date;
            DateTime To_Date = DateTime.Parse(oUserLoginDetail.SALARY_TODATE).Date;

            OracleCommand Ocd = new OracleCommand("PRE_REQUEST_FOR_SALARY", OracleCon);
            Ocd.Parameters.AddWithValue("P_S_Date", From_Date);
            Ocd.Parameters.AddWithValue("P_E_Date", To_Date);
            Ocd.CommandType = CommandType.StoredProcedure;
            if (OracleCon.State == ConnectionState.Closed)
            {
                OracleCon.Open();
            }
            int rES = Ocd.ExecuteNonQuery();
            if (rES > 0)
            { rESULT = true; }else 
            { rESULT = false ; }
            return rESULT;
        }
        catch(Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Initial_Records()
    {
        try
        {                    
            Create_Temp_Table(Temp_Salary_Table);
            Create_Temp_Table(Temp_Deduction_Table);
            Create_Temp_Table(Temp_Deduction_Loans_Table);
            Initial_Control();  

        }
        catch(Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    #region temp table creating
        private void Create_Temp_Table(DataTable DT)
        {
            try
            {                                      
                DT.Columns.Add("SalaryId", typeof(string));
                DT.Columns.Add("SalaryName", typeof(string)); 
                DT.Columns.Add("SalaryAmount", typeof(decimal));
                DT.Columns.Add("SalaryHeader", typeof(string));                     
            }
            catch(Exception ex)
            {
                errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
                throw;
            }
        }
    #endregion
   
    private bool Genrate_EMP_Salary(string SearchQuery, int SalaryMonth, int SalaryYear)
    {
        OracleCon = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        bool Result = false;
        DataTable DTEmp = new DataTable();
        StringBuilder Lsql = new StringBuilder();
        try
        {
                        
            Lsql.Append("SELECT EMP_CODE,E.CADDER_CODE,E.EMP_TYPE,TO_CHAR(JOIN_DT,'dd/MM/yyyy') AS JOININGDATE FROM HR_EMP_MST E");
            Lsql.Append(" WHERE" + SearchQuery);
            OracleDataAdapter OCD = new OracleDataAdapter(Lsql.ToString(), OracleCon);
            OCD.Fill(DTEmp);
            foreach (DataRow DRow in DTEmp.Rows)
            {
                Initial_Control();
                EMP_CODE = DRow["EMP_CODE"].ToString().Trim();
                EMP_TYPE = DRow["EMP_TYPE"].ToString().Trim();
                EMP_CADER_CODE = DRow["CADDER_CODE"].ToString().Trim();               
                Result = Genrate_Salary(EMP_CODE, EMP_CADER_CODE, EMP_TYPE,DateTime.Parse(SalaryStartDate),DateTime.Parse(SalaryEndDate));
            }
            OCD.Dispose();
            return Result;
        }        
        catch(Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }          
    }
    private bool Genrate_Salary(string EMPLOYEE, string EMPCadder, string EMPType, DateTime FromDate, DateTime Todate)
    {
        
        try
        {
            Calculate_Attendance(EMPLOYEE, FromDate.Date.ToShortDateString(), Todate.Date.ToShortDateString());
            Leave_Calc(STYear, EMPLOYEE, FromDate.ToShortDateString(), Todate.Date.ToShortDateString());
            GetBasicSalaryCalcuilation(EMPLOYEE);
            getDecductionCalculation();
            getDecduction_LoansCalculation();
            createSavingSubHeadTable();
            AddFieldinSavingSubHeadTable();
            fillingDataTable_SavingSubHeadTable();
            GetActualSalary();

            Insert_SalaryRecord();
            return true;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Leave_Calc(string  Year, string EMP_CODE, string FromDate, string Todate)
    {
        try
        {
            OracleCon = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
            OracleCon.Open();
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

            OracleCommand NewCom = new OracleCommand(Lsql.ToString(), OracleCon);
            NewCom.CommandType = CommandType.Text;
            NewCom.Parameters.AddWithValue(":EMP_CODE", EMP_CODE);
            NewCom.Parameters.AddWithValue(":YEAR", Year);
            NewCom.Parameters.AddWithValue(":FROMDATE", FromDate);
            NewCom.Parameters.AddWithValue(":TODATE", Todate);

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
            if (OracleCon.State == ConnectionState.Open)
            {
                OracleCon.Close();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Calculate_Attendance(string EMPLOYEE, string  FromDate, string  Todate)
    {
        DataTable DTEmployee = new DataTable();
        StringBuilder Lsql=new StringBuilder();
        OracleCon = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        try
        {
            DTEmployee.Rows.Clear();
            Lsql.Append("WITH TAB1 AS (SELECT DISTINCT EMP_CODE,(SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='P')  AS PRE,");
            Lsql.Append(" (SELECT NVL(COUNT(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy')AND LTRIM(RTRIM(S.ATTNTYPE))='A')  AS ABSENT,");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='NH')  AS NH, ");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='WO')  AS WO,");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='CL')  AS CL,");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='SL')  AS SL, ");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='EL')  AS EL, ");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy')AND LTRIM(RTRIM(S.ATTNTYPE))='ML')  AS ML,");
            Lsql.Append(" (SELECT NVL(SUM(S.ATTNVAL),0)  FROM HR_ATTN_SUMMARY S  WHERE S.EMP_CODE=:EMP_CODE AND S.ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy') AND LTRIM(RTRIM(S.ATTNTYPE))='C0')  AS CO");
            Lsql.Append(" FROM HR_ATTN_SUMMARY WHERE  EMP_CODE=:EMP_CODE AND ATTN_DATE BETWEEN To_Date(:FROMDATE, 'dd/MM/yyyy') AND To_Date (:TODATE,'dd/MM/yyyy'))");
            Lsql.Append(" SELECT T.* ,(SELECT SUM( PRE+NH+WO+CL+SL+EL+ML+CO ) FROM TAB1) AS TPAID,NVL(To_Date (:TODATE,'dd/MM/yyyy')-To_Date (:FROMDATE,'dd/MM/yyyy'),0)+1 AS TMONTHDAYS FROM TAB1 T");
            OracleCommand OCmd = new OracleCommand(Lsql.ToString());
            OCmd.Parameters.AddWithValue(":EMP_CODE", EMPLOYEE.Trim().ToString());
            OCmd.Parameters.AddWithValue(":FROMDATE", FromDate);
            OCmd.Parameters.AddWithValue(":TODATE", Todate);
            OCmd.Connection = OracleCon;
            if (OracleCon.State == ConnectionState.Closed)
            {
                OracleCon.Open();
            }
            OracleDataAdapter OCD = new OracleDataAdapter(OCmd);
            OCD.Fill(DTEmployee);
            foreach (DataRow DRow in DTEmployee.Rows)
            {
                Tcl = decimal.Parse(DRow["CL"].ToString());
                Tsl = decimal.Parse(DRow["SL"].ToString());
                Tel = decimal.Parse(DRow["EL"].ToString());
                Tml = decimal.Parse(DRow["ML"].ToString());
                TCO = decimal.Parse(DRow["CO"].ToString());
                NH = decimal.Parse(DRow["NH"].ToString());
                WO = decimal.Parse(DRow["WO"].ToString());
                PRESENT_DAY = decimal.Parse(DRow["PRE"].ToString());
                DayInmonth = decimal.Parse(DRow["TMONTHDAYS"].ToString());
                PAID_DAYS = decimal.Parse(DRow["TPAID"].ToString());
                ABSENT_DAYS = decimal.Parse(DRow["ABSENT"].ToString());
                if (EMP_CADER_CODE == "WORKMEN")
                {
                    PAID_DAYS = PAID_DAYS - WO;
                }
                if (DayInmonth == 31)
                {
                    if (PRESENT_DAY >= 26)
                    {
                        AttendanceAllownces = 150;
                    }
                }
                else if (DayInmonth == 30)
                {
                    if (PRESENT_DAY >= 25)
                    {
                        AttendanceAllownces = 150;
                    }
                }
                else if (DayInmonth == 28 || DayInmonth == 29)
                {
                    if (PRESENT_DAY >= 24)
                    {
                        AttendanceAllownces = 150;
                    }
                }
            }
            OCmd.Dispose();
            OCD.Dispose();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
        finally
        {
            if (OracleCon.State == ConnectionState.Open)
            {
                OracleCon.Close();
            }
        }
    }
    private void Initial_Control()
    {
        try
        {
            cl = 0; pl = 0; ml = 0; sl = 0; el = 0; CO = 0; withoutpay = 0;
            Rcl = 0; Rpl = 0; Rml = 0; Rsl = 0; Rel = 0; RCO = 0; Rwithoutpay = 0;
            Tcl = 0; Tpl = 0; Tml = 0; Tsl = 0; Tel = 0; TCO = 0; Twithoutpay = 0;
            ABSENT_DAYS = 0; PRESENT_DAY = 0; NH = 0; WO=0;
            PAID_DAYS = 0;  BasicAmount = 0; NetSalary = 0; ActualTotal = 0;
            TLoanTotal = 0; TOtherDeduction = 0; TAdditionSalay = 0;
            Temp_Salary_Table.Clear(); Temp_Deduction_Table.Clear(); Temp_Deduction_Loans_Table.Clear();

        }
        catch
        {
            throw;
        }
    }
    private void GetBasicSalaryCalcuilation(string EMP_CODE)
    {
        OracleCon = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        try
        {
            decimal basic = 0;
            DataTable DTable = new DataTable();
            string Lsql = "";
            Lsql = "SELECT a.SAL_GRADE_ID,a.GRADE_ID,a.HEAD_ID,b.SUBH_SLIP_FLD_NAME,a.SUBH_ID,NVL(a.AMT,0) AS AMT,a.TDATE,a.EMP_CODE, b.SUBH_NAME,b.SUBH_CAT,b.SUBH_TYPE,b.SUBH_SAL_TYPE FROM HR_EMP_SAL_MST a, HR_SUBH_MST b where a.SUBH_ID=b.SUBH_ID and ltrim(rtrim(EMP_CODE))='" + EMP_CODE + "' and ltrim(rtrim(a.DEL_STATUS))='0' and ltrim(rtrim(b.DEL_STATUS))='0' order by b.SUBH_CAT desc";
            OracleDataAdapter OCD = new OracleDataAdapter(Lsql, OracleCon);
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
                            basic = ((Convert.ToDecimal(basic) / Convert.ToDecimal(DayInmonth)) * (PAID_DAYS));
                            BasicAmount = basic;                           
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
                            Amount = Convert.ToDecimal((decimal.Parse(dr["AMT"].ToString().Trim()) / Convert.ToDecimal(DayInmonth)) * (PAID_DAYS));
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
                    DataTable dt = Temp_Salary_Table;
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
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void getDecductionCalculation()
    {
        try
        {
            decimal basic = 0;
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
                                    Amount = (MaxPF) * 12 / 100;
                                    //Amount = Convert.ToDecimal(((MaxPF / Convert.ToDecimal(DayInmonth)) * (PAID_DAYS)) * 12 / 100);
                                }
                                else
                                {
                                    Amount = Convert.ToDecimal(((basic / Convert.ToDecimal(DayInmonth)) * (PAID_DAYS)) * 12 / 100);
                                }

                            }
                        }

                        DataTable dt = Temp_Deduction_Table;
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
                    DataTable dt = Temp_Deduction_Table;
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
        catch(Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in Deduction Calculation.\r\nSee error log for detail.");
            throw;
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
                        DataTable dt = Temp_Deduction_Loans_Table;
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
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
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
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void createSavingSubHeadTable()
    {
        try
        {
            SavingSubHeadTable = new DataTable();
            DataColumn[] columns = new DataColumn[] 
                  { 
                    new DataColumn("SubHeadName", typeof(string)),
                    new DataColumn("SalaryAmount", typeof(string)),
                  };
            SavingSubHeadTable.Columns.AddRange(columns);            
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
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
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
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
                foreach (DataRow rw2 in Temp_Salary_Table.Rows)
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
                foreach (DataRow rw4 in Temp_Deduction_Table.Rows)
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
                foreach (DataRow rw6 in Temp_Deduction_Loans_Table.Rows)
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
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

    protected int Insert_SalaryRecord()
    {
        OracleCon = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        try
        {       OracleCommand cmd = new OracleCommand("P_HR_SAL_SLIP_MST_INSERT", OracleCon);
                cmd.CommandType = CommandType.StoredProcedure;
                if (OracleCon.State == ConnectionState.Closed)
                {
                    OracleCon.Open();
                }
                cmd.Parameters.AddWithValue("P_EMP_CODE", CommonFuction.funFixQuotes(EMP_CODE));
                cmd.Parameters.AddWithValue("P_SAL_MONTH", CommonFuction.funFixQuotes(SMonth));
                cmd.Parameters.AddWithValue("P_SAL_YEAR", CommonFuction.funFixQuotes(STYear));
                cmd.Parameters.AddWithValue("P_SAL_WORKING_DAY", CommonFuction.funFixQuotes(String.Format("{0:0.00}", PRESENT_DAY)));
                cmd.Parameters.AddWithValue("P_HLD", CommonFuction.funFixQuotes(String.Format("{0:0.00}", NH+WO)));
                cmd.Parameters.AddWithValue("P_PAID_DAY", CommonFuction.funFixQuotes(String.Format("{0:0.00}", PAID_DAYS)));
                cmd.Parameters.AddWithValue("P_NET_SAL", Math.Round(NetSalary, 0));
                cmd.Parameters.AddWithValue("P_EPF", "0");
                cmd.Parameters.AddWithValue("P_ERN_AMT", String.Format("{0:0.00}", Math.Round(TAdditionSalay, 2)));
                cmd.Parameters.AddWithValue("P_LOAN_AMT", String.Format("{0:0.00}", Math.Round(TLoanTotal, 2)));

                cmd.Parameters.AddWithValue("P_DEDCT_AMT", String.Format("{0:0.00}", Math.Round(TOtherDeduction, 2)));
                cmd.Parameters.AddWithValue("P_CASUAL_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rcl)));
                cmd.Parameters.AddWithValue("P_SICK_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rsl)));
                cmd.Parameters.AddWithValue("P_EARN_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rel)));
                cmd.Parameters.AddWithValue("P_MATERNITY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rml)));
                cmd.Parameters.AddWithValue("P_PATERNITY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rpl)));
                cmd.Parameters.AddWithValue("P_COMPENSATORY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", RCO)));
                cmd.Parameters.AddWithValue("P_LV_WITHOUT_PAY", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Rwithoutpay)));

                cmd.Parameters.AddWithValue("P_OPENING_CASUAL_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", cl)));
                cmd.Parameters.AddWithValue("P_OPENING_SICK_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", sl)));
                cmd.Parameters.AddWithValue("P_OPENING_EARN_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", el)));
                cmd.Parameters.AddWithValue("P_OPENING_MATERNITY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", ml)));
                cmd.Parameters.AddWithValue("P_OPENING_PATERNITY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", pl)));
                cmd.Parameters.AddWithValue("P_OPENING_COMPENSATORY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", CO)));
                cmd.Parameters.AddWithValue("P_OPENING_LV_WITHOUT_PAY", CommonFuction.funFixQuotes(String.Format("{0:0.00}", withoutpay)));

                cmd.Parameters.AddWithValue("P_AVIAL_CASUAL_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tcl)));
                cmd.Parameters.AddWithValue("P_AVIAL_SICK_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tsl)));
                cmd.Parameters.AddWithValue("P_AVIAL_EARN_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tel)));
                cmd.Parameters.AddWithValue("P_AVIAL_MATERNITY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tml)));
                cmd.Parameters.AddWithValue("P_AVIAL_PATERNITY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Tpl)));
                cmd.Parameters.AddWithValue("P_AVIAL_COMPENSATORY_LV", CommonFuction.funFixQuotes(String.Format("{0:0.00}", TCO)));
                cmd.Parameters.AddWithValue("P_AVIAL_LV_WITHOUT_PAY", CommonFuction.funFixQuotes(String.Format("{0:0.00}", Twithoutpay)));
            
                foreach (DataRow rw in SavingSubHeadTable.Rows)
                {
                    string FieldName = "P_" + rw["SubHeadName"].ToString().Trim();                    
                    cmd.Parameters.AddWithValue(FieldName, CommonFuction.funFixQuotes(String.Format("{0:0.00}", Math.Round(decimal.Parse(rw["SalaryAmount"].ToString().Trim()), 2))));
                }

               cmd.Parameters.AddWithValue("P_TUSER", oUserLoginDetail.UserCode.Trim().ToString());
               cmd.Parameters.AddWithValue("P_LOCK_LV", "UnLock");
               cmd.Parameters.AddWithValue("P_SAVE_STATUS","A");
               cmd.Parameters.AddWithValue("P_DEL_STATUS", "0");
               cmd.Parameters.AddWithValue("P_TDATE", System.DateTime.Now);                            
               int rES = cmd.ExecuteNonQuery();
               if (cmd != null)
               {
                   cmd.Dispose();
                   cmd = null;
               }    
               return rES;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
        finally
        {            
            if (OracleCon  != null)
            {
                OracleCon.Close();
                OracleCon = null;                
            } 
        }

    }
}

