using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OracleClient;
using System.Text;
using DBLibrary;
using Common;
using errorLog;
using AmountToWords;

public partial class Module_HRMS_Pages_UpdateSalarySlip : System.Web.UI.Page
{
    csSaitex obj = null;
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader dr = null;
    DataSet ds = null;
    OracleDataReader dr1 = null;

    public static string leaveName_cl = "Casual Leave", leaveName_pl = "Paternity Leave", leaveName_ml = "Maternity Leave", leaveName_sl = "Sick Leave", leaveName_el = "Earn Leave", leaveName_lwp = "Without Pay", leaveName_cm = "Compensatory Leave", radFirstValue = "Full", radLastValue = "Full";
    public static decimal cl = 0, pl = 0, ml = 0, sl = 0, el = 0, cm = 0, withoutpay = 0;
    public static decimal l_cl = 0, l_pl = 0, l_ml = 0, l_sl = 0, l_el = 0, l_cm = 0, l_withoutpay = 0;
    public static decimal l_cl1 = 0, l_pl1 = 0, l_ml1 = 0, l_sl1 = 0, l_el1 = 0, l_cm1 = 0, l_withoutpay1 = 0;
    public static decimal Attendance1 = 0;
    public static Int32 DayInmonth = 0;
    public static decimal PayDay = 0;
    public static decimal Hoilday = 0;
    public static string lblEmployeeId = "";
    public static Int32 lblMonth = 0;
    public static Int32 lblYear = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session["usrName"] != null)
        //{
        tdTotalAdditional.Visible = false;
        if (!IsPostBack)
        {
            if (Request.QueryString["SalaryId"] != null)
            {
                if (CheckingLockingStatus(Request.QueryString["SalaryId"].ToString().Trim()) == 0)
                {
                    createDataTable_BasicCalculation();
                    createDataTable_BasicCalculation_Deduction();
                    createDataTable_BasicCalculation_Deduction_Loans();
                    createDataTable_StoreLeaveAvilableinMonth();
                    createDataTable_LeaveTaken();
                    getrecordfromSalaryslip();
                    getSubHeadDetail();
                    fillAllDataTable();
                    fillAmountinDataTable();
                    bindGridView();
                    getrecordfromSalaryslip1();
                    getSalaryPreparedBy();
                }
                else
                {
                    Response.Redirect("./SalarySlipView.aspx?SalaryId=" + Request.QueryString["SalaryId"].ToString().Trim(), false);

                }

            }
        }

        //else
        //{
        //    Response.Redirect("/Saitex/Default.aspx", false);
        //}

    }

    private int CheckingLockingStatus(string TableId)
    {
        int result = 0;
        try
        {
            string strLockSataus = "SELECT SAL_SLIP_MST_ID,LOCK_LV FROM hr_sal_slip_mst where ltrim(rtrim(SAL_SLIP_MST_ID))='" + TableId.Trim() + "' and LOCK_LV='Lock'";
            obj = new csSaitex();
            dr = obj.getDataReader(strLockSataus, CommandType.Text);
            if (dr.Read() == true)
            {
                result = 1;
            }
            dr.Close();
            dr.Dispose();
            dr = null;
        }
        catch (OracleException ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }

        }
        return result;
    }
    private void getrecordfromSalaryslip1()
    {
        try
        {
            string strGetSalary = "SELECT * FROM HR_SAL_SLIP_MST where ltrim(rtrim(SAL_SLIP_MST_ID))='" + Request.QueryString["SalaryId"].ToString().Trim() + "'";
            obj = new csSaitex();

            dr1 = obj.getDataReader(strGetSalary, CommandType.Text);
            if (dr1.Read() == true)
            {
                //lblEmployerEpf.Text = dr1["EPF"].ToString().Trim();
                lblLoadTotal.Text = dr1["LOAN_AMT"].ToString().Trim();
                lblOtherDeduction.Text = dr1["DEDCT_AMT"].ToString().Trim();
                lblTotaladditionSalay.Text = dr1["ERN_AMT"].ToString().Trim();
                lblActualTotal.Text = dr1["NET_SAL"].ToString().Trim();
                lblNetSalary.Text = dr1["NET_SAL"].ToString().Trim();
                RupeesToWord o1 = new RupeesToWord();
                lblFiguretoWord.Text = o1.changeNumericToWords(Convert.ToDouble(lblActualTotal.Text)) + " only";

                l_cl = Convert.ToDecimal(dr1["CASUAL_LV"].ToString().Trim());
                l_pl = Convert.ToDecimal(dr1["PATERNITY_LV"].ToString().Trim());
                l_ml = Convert.ToDecimal(dr1["MATERNITY_LV"].ToString().Trim());
                l_sl = Convert.ToDecimal(dr1["SICK_LV"].ToString().Trim());
                l_el = Convert.ToDecimal(dr1["EARN_LV"].ToString().Trim());
                l_cm = Convert.ToDecimal(dr1["COMPENSATORY_LV"].ToString().Trim());
                l_withoutpay = Convert.ToDecimal(dr1["LV_WITHOUT_PAY"].ToString().Trim());

                cl = Convert.ToDecimal(dr1["OPENING_CASUAL_LV"].ToString().Trim());
                pl = Convert.ToDecimal(dr1["OPENING_PATERNITY_LV"].ToString().Trim());
                ml = Convert.ToDecimal(dr1["OPENING_MATERNITY_LV"].ToString().Trim());
                sl = Convert.ToDecimal(dr1["OPENING_SICK_LV"].ToString().Trim());
                el = Convert.ToDecimal(dr1["OPENING_EARN_LV"].ToString().Trim());
                cm = Convert.ToDecimal(dr1["OPENING_COMPENSATORY_LV"].ToString().Trim());
                withoutpay = Convert.ToDecimal(dr1["OPENING_LV_WITHOUT_PAY"].ToString().Trim());

                l_cl1 = Convert.ToDecimal(dr1["AVIAL_CASUAL_LV"].ToString().Trim());
                l_pl1 = Convert.ToDecimal(dr1["AVIAL_PATERNITY_LV"].ToString().Trim());
                l_ml1 = Convert.ToDecimal(dr1["AVIAL_MATERNITY_LV"].ToString().Trim());
                l_sl1 = Convert.ToDecimal(dr1["AVIAL_SICK_LV"].ToString().Trim());
                l_el1 = Convert.ToDecimal(dr1["AVIAL_EARN_LV"].ToString().Trim());
                l_cm1 = Convert.ToDecimal(dr1["AVIAL_COMPENSATORY_LV"].ToString().Trim());
                l_withoutpay1 = Convert.ToDecimal(dr1["AVIAL_LV_WITHOUT_PAY"].ToString().Trim());



                Hoilday = Convert.ToDecimal(dr1["HLD"].ToString().Trim());
                PayDay = Convert.ToDecimal(dr1["PAID_DAY"].ToString().Trim());
                Attendance1 = Convert.ToDecimal(dr1["SAL_WORKING_DAY"].ToString().Trim());
                Fill_Data_InForm(dr1["EMP_CODE"].ToString().Trim());
                string strMonth = "";
                if (dr1["SAL_MONTH"].ToString().Trim() == "1")
                {
                    strMonth = "JAN";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "2")
                {
                    strMonth = "FEB";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "3")
                {
                    strMonth = "MAR";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "4")
                {
                    strMonth = "APR";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "5")
                {
                    strMonth = "MAY";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "6")
                {
                    strMonth = "JUNE";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "7")
                {
                    strMonth = "JULY";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "8")
                {
                    strMonth = "AUG";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "9")
                {
                    strMonth = "SEP";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "10")
                {
                    strMonth = "OCT";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "11")
                {
                    strMonth = "NOV";
                }

                else if (dr1["SAL_MONTH"].ToString().Trim() == "12")
                {
                    strMonth = "DEC";
                }

                lblPaySlipMonth_Year.Text = "Salary Slip For the M/O " + " <b>&nbsp;&nbsp;:&nbsp;&nbsp;" + strMonth + "</b>" + ",&nbsp;" + "<b>" + dr1["SAL_YEAR"].ToString().Trim() + "</b>";

                //lblPaySlipMonth_Year.Text = "Detailed Payslip For " + dr1["MonthName"].ToString().Trim() + "," + dr1["vc_Year"].ToString().Trim();
                lblEmployeeId = dr1["EMP_CODE"].ToString().Trim();
                lblMonth = Convert.ToInt32(dr1["SAL_MONTH"].ToString().Trim());
                lblYear = Convert.ToInt32(dr1["SAL_YEAR"].ToString().Trim());
                CurrentMonthCalculation();
                CalaulationOfNormalLeave(Convert.ToInt32(dr1["SAL_MONTH"].ToString().Trim()), Convert.ToInt32(dr1["SAL_YEAR"].ToString().Trim()), dr1["EMP_CODE"].ToString().Trim());
                CalculateEarnLeave(Convert.ToInt32(dr1["SAL_YEAR"].ToString().Trim()), dr1["EMP_CODE"].ToString().Trim());
                calulationLeaveTakenByEmployeeNormal(Convert.ToInt32(dr1["SAL_MONTH"].ToString().Trim()), Convert.ToInt32(dr1["SAL_YEAR"].ToString().Trim()), dr1["EMP_CODE"].ToString().Trim());
                calulationLeaveTakenByEmployeeEarn(Convert.ToInt32(dr1["SAL_YEAR"].ToString().Trim()), dr1["EMP_CODE"].ToString().Trim());
                TryTOcheck();
                getTotalLeaveDetail();

            }
            dr1.Close();
            dr1.Dispose();
            dr1 = null;
        }
        catch (OracleException ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }
    }

    private void Fill_Data_InForm(string EMP_CODE)
    {
        try
        {
            StringBuilder Esql = new StringBuilder();
            Esql.Append("SELECT A.EMP_CODE,A.CARD_NO,A.F_NAME ||' '||M_NAME||' '||L_NAME AS EMPLOYEENAME,A.GENDER,A.F_H_NAME,A.PAY_MODE,A.COMP_CODE,C.COMP_NAME AS COMPANY,A.JOIN_DT,");
            Esql.Append(" BR.BRANCH_CODE,BR.BRANCH_NAME,BA.BANK_NAME,DE.DESIG_NAME as Designation,DP.DEPT_NAME as Department,");
            Esql.Append(" CO.AC_NO as BankAC,CO.PF_AC_NO AS PFNO,MD.ESI AS ESINO,(SELECT AMT  FROM HR_EMP_SAL_MST SAL WHERE SAL.SUBH_ID=1 AND SAL.EMP_CODE=A.EMP_CODE)AS BASIC");
            Esql.Append(" FROM HR_EMP_MST A");
            Esql.Append(" LEFT JOIN CM_BRANCH_MST BR ON BR.BRANCH_CODE=A.BRANCH_CODE");
            Esql.Append(" LEFT JOIN CM_COMPANY_MST C ON C.COMP_CODE=A.COMP_CODE");
            Esql.Append(" LEFT JOIN CM_DESIG_MST DE ON DE.DESIG_CODE=A.DESIG_CODE");
            Esql.Append(" LEFT JOIN CM_DEPT_MST DP ON DP.DEPT_CODE=A.DEPT_CODE");
            Esql.Append(" LEFT JOIN HR_EMP_COMP_INFO CO ON CO.EMP_CODE=A.EMP_CODE");
            Esql.Append(" LEFT JOIN HR_EMP_MED_DTL MD ON MD.EMP_CODE=A.EMP_CODE");
            Esql.Append(" LEFT OUTER JOIN HR_BANK_MST BA ON ltrim(rtrim(BA.BANK_CODE))=ltrim(rtrim(CO.BANK_CODE))");
            Esql.Append(" WHERE ltrim(rtrim(A.EMP_CODE))='" + EMP_CODE.Trim() + "'");
            //strEmployeeRecord = "SELECT Em.EMP_CODE EMP_CODE,CARD_NO, (F_NAME||' '||M_NAME||' '|| L_NAME) EmployeeName,GENDER, Em.COMP_CODE,Em.BRANCH_CODE,Bm.BRANCH_NAME,F_H_NAME,JOIN_DT, Em.DEPT_CODE,Em.DESIG_CODE,Em.COMP_CODE, Bm.BRANCH_CODE,PAY_MODE,POSTED_LEN,COMP_NAME,DESIG_NAME, DEPT_NAME, PF_AC_NO,AC_NO ,BANK_NAME FROM HR_EMP_MST Em,CM_COMPANY_MST Cm,CM_BRANCH_MST Bm, CM_DESIG_MST Pd ,CM_DEPT_MST dm,HR_EMP_COMP_INFO eci,HR_BANK_MST bn where Em.COMP_CODE=Cm.COMP_CODE and Em.BRANCH_CODE=Bm.BRANCH_CODE  and em.EMP_CODE=eci.EMP_CODE and eci.BANK_CODE=bn.BANK_CODE and Em.DESIG_CODE=Pd.DESIG_CODE and Em.DEPT_CODE=Dm.DEPT_CODE and ltrim(rtrim(em.EMP_CODE))='" + EMP_CODE.Trim() + "' ";
            obj = new csSaitex();
            dr = obj.getDataReader(Esql.ToString(), CommandType.Text);
            if (dr.Read() == true)
            {
                lblEmployeeName.Text = dr["EMPLOYEENAME"].ToString().Trim();
                lblEnrolNumber.Text =  dr["CARD_NO"].ToString().Trim();
                lblDepartment.Text = dr["Department"].ToString().Trim();
                lblDesignation.Text = dr["Designation"].ToString().Trim();
                lblComapanyName.Text = dr["COMPANY"].ToString().Trim();
                lblFatherName.Text = dr["F_H_NAME"].ToString().Trim();
                string strJoiningDate = "";
                strJoiningDate = dr["JOIN_DT"].ToString().Trim();
                strJoiningDate = strJoiningDate.Substring(0, 9);
                lblJoining.Text = strJoiningDate;
                lblPayMode.Text = dr["PAY_MODE"].ToString().Trim();
                lblPF.Text = dr["PFNO"].ToString().Trim();
                lblAccountNumber.Text = dr["BankAC"].ToString().Trim();
                lblBankName.Text = dr["BANK_NAME"].ToString().Trim();
                lblBranchName.Text = dr["BRANCH_NAME"].ToString().Trim();                
                if (dr["GENDER"].ToString().Trim() == "Male")
                {
                    leaveName_ml = "";
                }
                else
                {
                    leaveName_pl = "";
                }

                dr.Close();
                dr.Dispose();
                dr = null;
                


            }
        }
        catch (OracleException ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
    private void CurrentMonthCalculation()
    {
        try
        {
            lblCurrentMonthCalculation.Text = " <table border='0' width='100%' class='tContentArial'>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <tr>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + "   <td>Working Day :" + String.Format("{0:0.00}", Attendance1) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>Holiday :" + String.Format("{0:0.00}", Hoilday) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_cl.Trim() + ":" + String.Format("{0:0.00}", l_cl) + "</td>";
            //lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_el.Trim() + ":" + String.Format("{0:0.00}", l_el) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_sl.Trim() + ":" + String.Format("{0:0.00}", l_sl) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " </tr>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <tr>";
            if (leaveName_ml.Trim() != "")
            {
                //lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_ml.Trim() + ":" + String.Format("{0:0.00}", l_ml) + "</td>";
            }
            else
            {
                //lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_pl.Trim() + ":" + String.Format("{0:0.00}", l_pl) + "</td>";
            }
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_lwp.Trim() + ":" + String.Format("{0:0.00}", l_withoutpay) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>Paid Days :" + String.Format("{0:0.00}", PayDay) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " </tr>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " </table>";
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void getrecordfromSalaryslip()
    {
        try
        {
            string strGetSalary="SELECT SAL_SLIP_MST_ID, EMP_CODE,SAL_YEAR, SAL_MONTH,SAL_WORKING_DAY, HLD, PAID_DAY, NET_SAL, EPF, ERN_AMT, LOAN_AMT, DEDCT_AMT, CASUAL_LV, SICK_LV, EARN_LV, MATERNITY_LV, PATERNITY_LV, COMPENSATORY_LV, LV_WITHOUT_PAY, SUBH1, SUBH2, SUBH3, SUBH4, SUBH5, SUBH6, SUBH7, SUBH8, SUBH9, SUBH10, SUBH11, SUBH12, SUBH13, SUBH14, SUBH15, SUBH16, SUBH17, SUBH18, SUBH19, SUBH20, SUBH21, SUBH22, SUBH23, SUBH24, SUBH25, SUBH26, SUBH27, SUBH28, SUBH29, SUBH30, LOCK_LV, a.TDATE,USER_NAME  FROM hr_sal_slip_mst a, CM_USER_MST b  where ltrim(rtrim(a.TUSER))=ltrim(rtrim(b.USER_CODE)) and ltrim(rtrim(SAL_SLIP_MST_ID))='" + Request.QueryString["SalaryId"].ToString().Trim() + "'";
            
            obj = new csSaitex();
            ds = obj.getDataSet(strGetSalary, CommandType.Text);
            ViewState["GetSalaryDetail"] = ds;
        }
        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
    }
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
            dt_colounm.ColumnName = "FieldName";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_BasicCalculation.Columns.Add(dt_colounm);

            ViewState["BasicCalculation"] = dt_BasicCalculation;
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
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
            dt_colounm.ColumnName = "FieldName";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_BasicCalculation_Deduction.Columns.Add(dt_colounm);

            ViewState["BasicCalculation_Deduction"] = dt_BasicCalculation_Deduction;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
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
            dt_colounm.ColumnName = "FieldName";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_BasicCalculation_Deduction_Loans.Columns.Add(dt_colounm);

            ViewState["BasicCalculation_Deduction_Loans"] = dt_BasicCalculation_Deduction_Loans;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void getSubHeadDetail()
    {
        try
        {
            string strGetSubSalaryName = "SELECT SUBH_ID,HEAD_ID,SUBH_NAME,DEL_STATUS,STATUS,TDATE,SUBH_CAT,SUBH_SAL_TYPE,SUBH_TYPE,SUBH_SLIP_FLD_NAME FROM HR_SUBH_MST where ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            ds = obj.getDataSet(strGetSubSalaryName, CommandType.Text);
            ViewState["GetSubHead"] = ds;
        }
        catch (OracleException ex)
        {
            Response.Write(ex.Message);
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }
    }
    private void fillAllDataTable()
    {
        try
        {
            DataSet ds = (DataSet)ViewState["GetSubHead"];
            DataTable datatable_BasicCalculation = (DataTable)ViewState["BasicCalculation"];
            DataTable datatable_BasicCalculation_Deduction = (DataTable)ViewState["BasicCalculation_Deduction"];
            DataTable datatable_BasicCalculation_Deduction_Loans = (DataTable)ViewState["BasicCalculation_Deduction_Loans"];
            DataRow rw1;
            foreach (DataRow rw in ds.Tables[0].Rows)
            {
                if (rw["SUBH_CAT"].ToString().Trim() == "S" || rw["SUBH_CAT"].ToString().Trim() == "A" || rw["SUBH_CAT"].ToString().Trim() == "P")
                {

                    rw1 = datatable_BasicCalculation.NewRow();
                    rw1["SalaryName"] = rw["SUBH_NAME"].ToString().Trim();
                    rw1["SalaryAmount"] = "0";
                    rw1["SalaryId"] = rw["SUBH_ID"].ToString().Trim();
                    rw1["FieldName"] = rw["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                    datatable_BasicCalculation.Rows.Add(rw1);
                }
                else if (rw["SUBH_CAT"].ToString().Trim() == "D" && rw["SUBH_CAT"].ToString().Trim() != "L")
                {
                    rw1 = datatable_BasicCalculation_Deduction.NewRow();
                    rw1["SalaryName"] = rw["SUBH_NAME"].ToString().Trim();
                    rw1["SalaryAmount"] = "0";
                    rw1["SalaryId"] = rw["SUBH_ID"].ToString().Trim();
                    rw1["FieldName"] = rw["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                    datatable_BasicCalculation_Deduction.Rows.Add(rw1);
                }
                else if (rw["SUBH_CAT"].ToString().Trim() == "D" && rw["SUBH_CAT"].ToString().Trim() == "L")
                {
                    rw1 = datatable_BasicCalculation_Deduction_Loans.NewRow();
                    rw1["SalaryName"] = rw["SUBH_NAME"].ToString().Trim();
                    rw1["SalaryAmount"] = "0";
                    rw1["SalaryId"] = rw["SUBH_ID"].ToString().Trim();
                    rw1["FieldName"] = rw["SUBH_SLIP_FLD_NAME"].ToString().Trim();
                    datatable_BasicCalculation_Deduction_Loans.Rows.Add(rw1);
                }
            }
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void fillAmountinDataTable()
    {

        try
        {
            DataSet ds = (DataSet)ViewState["GetSalaryDetail"];
            DataTable datatable_BasicCalculation = (DataTable)ViewState["BasicCalculation"];
            DataTable datatable_BasicCalculation_Deduction = (DataTable)ViewState["BasicCalculation_Deduction"];
            DataTable datatable_BasicCalculation_Deduction_Loans = (DataTable)ViewState["BasicCalculation_Deduction_Loans"];


            foreach (DataRow rw in datatable_BasicCalculation.Rows)
            {
                rw["SalaryAmount"] = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["" + rw["FieldName"].ToString().Trim() + ""].ToString().Trim()));
                rw.AcceptChanges();
            }

            foreach (DataRow rw2 in datatable_BasicCalculation_Deduction.Rows)
            {
                rw2["SalaryAmount"] = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["" + rw2["FieldName"].ToString().Trim() + ""].ToString().Trim()));
                rw2.AcceptChanges();
            }

            foreach (DataRow rw3 in datatable_BasicCalculation_Deduction_Loans.Rows)
            {
                rw3["SalaryAmount"] = String.Format("{0:0.00}", Convert.ToDecimal(ds.Tables[0].Rows[0]["" + rw3["FieldName"].ToString().Trim() + ""].ToString().Trim()));
                rw3.AcceptChanges();
            }
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void bindGridView()
    {
        try
        {
            DataTable datatable_BasicCalculation = (DataTable)ViewState["BasicCalculation"];
            DataTable datatable_BasicCalculation_Deduction = (DataTable)ViewState["BasicCalculation_Deduction"];
            DataTable datatable_BasicCalculation_Deduction_Loans = (DataTable)ViewState["BasicCalculation_Deduction_Loans"];

            gvBasicCalculation.DataSource = datatable_BasicCalculation;
            gvBasicCalculation.DataBind();

            gvDeductionCalculation.DataSource = datatable_BasicCalculation_Deduction;
            gvDeductionCalculation.DataBind();


            gvDeductionCalculation_Loans.DataSource = datatable_BasicCalculation_Deduction_Loans;
            gvDeductionCalculation_Loans.DataBind();

            getTotalDeduction();
            getTotalSubHeadTotal();
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void chkActiveGrid_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (chkActiveGrid.Checked == true)
            {
                gvBasicCalculation.Enabled = true;
                gvDeductionCalculation.Enabled = true;
                gvDeductionCalculation_Loans.Enabled = true;
                // lblEmployerEpf.Enabled = true;
                btnUpdate.Visible = true;
            }
            else
            {
                Response.Redirect("./UpdateSalarySlip.aspx?SalaryId=" + Request.QueryString["SalaryId"].ToString().Trim(), false);
            }
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    ////////////////////////////////////////////////////////////////////////////////////////////////////
    private void createDataTable_StoreLeaveAvilableinMonth()
    {
        try
        {
            DataTable dt_LeaveAvialable = new DataTable();
            DataColumn dt_colounm;

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "ch_EmployeeMasterId";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "Month";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "Year";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "in_LeaveMasterId";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "ch_LeaveType";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "vc_Total_LeaveDay_InMonth";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            ViewState["AvailableLeave_Detail"] = dt_LeaveAvialable;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

    }
    private void createDataTable_LeaveTaken()
    {
        try
        {
            DataTable dt_LeaveAvialable = new DataTable();
            DataColumn dt_colounm;

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "ch_EmployeeMasterId";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "Month";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "Year";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "in_LeaveMasterId";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "ch_LeaveType";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            dt_colounm = new DataColumn();
            dt_colounm.ColumnName = "vc_Total_LeaveDay_InMonth";
            dt_colounm.DataType = Type.GetType("System.String");
            dt_LeaveAvialable.Columns.Add(dt_colounm);

            ViewState["TakenLeave_Detail"] = dt_LeaveAvialable;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

    }
    private void CalaulationOfNormalLeave(int month, int Year, string EMP_CODE)
    {

        try
        {
            string strAssignedLeave = "";

            //strAssignedLeave = " SELECT ch_LeaveAssignedMasterId,ch_Year,ch_Month,ch_EmployeeMasterId,in_LeaveMasterId,ch_LeaveType,vc_Total_LeaveDay_InMonth,dt_Created,dt_Updated FROM tblLeaveAssignedMaster where ltrim(rtrim(ch_Month))='" + month + "' and ltrim(rtrim(ch_Year))='" + Year + "' and ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(ch_LeaveType))='CL'";
            strAssignedLeave = "SELECT LV_ASS_ID,LV_YEAR,LV_MONTH,EMP_CODE,LV_ID,LV_TYPE, LV_DAYS_IN_MONTH,TDATE,LV_DAYS_IN_MONTH FROM HR_LV_ASS_MST where ltrim(rtrim(LV_YEAR))='" + Year + "' and ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(LV_TYPE))='1' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            ds = obj.getDataSet(strAssignedLeave, CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                decimal cl = 0;
                DataTable dt = (DataTable)ViewState["AvailableLeave_Detail"];
                DataRow rw;
                rw = dt.NewRow();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {

                    cl = cl + Convert.ToDecimal(dr["LV_DAYS_IN_MONTH"].ToString().Trim());

                    rw["ch_EmployeeMasterId"] = dr["EMP_CODE"].ToString().Trim();
                    rw["Month"] = dr["LV_MONTH"].ToString().Trim();
                    rw["Year"] = dr["LV_YEAR"].ToString().Trim();
                    rw["in_LeaveMasterId"] = dr["LV_ID"].ToString().Trim();
                    //rw["ch_LeaveType"] = dr["ch_LeaveType"].ToString().Trim();
                    rw["vc_Total_LeaveDay_InMonth"] = cl;

                }
                dt.Rows.Add(rw);
            }
        }

        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
    }
    private void CalculateEarnLeave(int Year, string EMP_CODE)
    {
        try
        {
            string strAssignedLeave = "SELECT LV_ASS_ID,LV_YEAR,LV_MONTH,EMP_CODE,LV_ID,LV_TYPE, LV_DAYS_IN_MONTH,TDATE FROM HR_LV_ASS_MST where ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(LV_YEAR))<='" + Year + "' and ltrim(rtrim(LV_TYPE))<>'1' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            ds = obj.getDataSet(strAssignedLeave, CommandType.Text);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataTable dt = (DataTable)ViewState["AvailableLeave_Detail"];
                DataRow rw;
                rw = dt.NewRow();
                rw["ch_EmployeeMasterId"] = dr["EMP_CODE"].ToString().Trim();
                rw["Month"] = dr["LV_MONTH"].ToString().Trim();
                rw["Year"] = dr["LV_YEAR"].ToString().Trim();
                rw["in_LeaveMasterId"] = dr["LV_ID"].ToString().Trim();
                rw["ch_LeaveType"] = dr["LV_TYPE"].ToString().Trim();
                rw["vc_Total_LeaveDay_InMonth"] = dr["LV_DAYS_IN_MONTH"].ToString().Trim();
                dt.Rows.Add(rw);
            }

        }

        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
    
    }
    private void calulationLeaveTakenByEmployeeNormal(int month, int Year, string EMP_CODE)
    {
        try
        {
            //  string strAssignedLeave = " SELECT ch_LeaveApplicationDetailId,ch_LeaveApplicationFormDetailId,ch_EmployeeMasterId,vc_SelectedDate,month(vc_SelectedDate) ch_Month,year(vc_SelectedDate) ch_Year,ch_LeaveType,ch_LeaveDayDetail,ch_DaysOfLeave,ch_Status,dt_Created,dt_Updated FROM tblLeaveApplicationDetail where ltrim(rtrim(month(vc_SelectedDate)))='" + month + "' and ltrim(rtrim(year(vc_SelectedDate)))='" + Year + "' and ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(ch_LeaveType))='CL' and ltrim(rtrim(ch_Status))='1'";
            string strAssignedLeave = "SELECT LV_APP_DTL_ID,LV_APP_FORM_ID,EMP_CODE,LV_APP_DATE,DAYS_LV, to_char(LV_APP_DATE,'mm') lm,to_char(LV_APP_DATE,'yyyy') ly,LV_TYPE,LV_DAY_DTL, DAYS_LV,STATUS,TDATE FROM HR_LV_APP_DTL where to_char(LV_APP_DATE,'yyyyy')='" + Year + "' and ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(LV_TYPE))='1' and ltrim(rtrim(STATUS))='1' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            ds = obj.getDataSet(strAssignedLeave, CommandType.Text);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataTable dt = (DataTable)ViewState["TakenLeave_Detail"];
                DataRow rw;
                rw = dt.NewRow();
                rw["ch_EmployeeMasterId"] = dr["EMP_CODE"].ToString().Trim();
                rw["Month"] = dr["lm"].ToString().Trim();
                rw["Year"] = dr["ly"].ToString().Trim();
                rw["in_LeaveMasterId"] = "";
                rw["ch_LeaveType"] = dr["LV_TYPE"].ToString().Trim();
                rw["vc_Total_LeaveDay_InMonth"] = dr["DAYS_LV"].ToString().Trim();
                dt.Rows.Add(rw);
            }
        }
        catch (OracleException ex)
        {
            //lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }
    }
    private void calulationLeaveTakenByEmployeeEarn(int Year, string EMP_CODE)
    {
        try
        {
            //string strAssignedLeave = "SELECT ch_LeaveApplicationDetailId,ch_LeaveApplicationFormDetailId,ch_EmployeeMasterId,vc_SelectedDate,month(vc_SelectedDate) ch_Month,year(vc_SelectedDate) ch_Year,ch_LeaveType,ch_LeaveDayDetail,ch_DaysOfLeave,ch_Status,dt_Created,dt_Updated FROM tblLeaveApplicationDetail where ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(year(vc_SelectedDate)))<='" + Year + "'  and ltrim(rtrim(ch_LeaveType))!='CL' and ltrim(rtrim(ch_Status))='1'";
            string strAssignedLeave = "SELECT LV_APP_DTL_ID,LV_APP_FORM_ID,EMP_CODE,LV_APP_DATE, to_char(LV_APP_DATE,'mm') lm,to_char(LV_APP_DATE,'yyyy') ly,LV_TYPE,LV_DAY_DTL,DAYS_LV, STATUS,TDATE FROM HR_LV_APP_DTL where ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and to_char(LV_APP_DATE,'yyyy')<='" + Year + "' and ltrim(rtrim(LV_TYPE))<>'1' and ltrim(rtrim(STATUS))='1' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            ds = obj.getDataSet(strAssignedLeave, CommandType.Text);
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                DataTable dt = (DataTable)ViewState["TakenLeave_Detail"];
                DataRow rw;
                rw = dt.NewRow();
                rw["ch_EmployeeMasterId"] = dr["EMP_CODE"].ToString().Trim();
                rw["Month"] = dr["lm"].ToString().Trim();
                rw["Year"] = dr["ly"].ToString().Trim();
                rw["in_LeaveMasterId"] = "";
                rw["ch_LeaveType"] = dr["LV_TYPE"].ToString().Trim();
                rw["vc_Total_LeaveDay_InMonth"] = dr["DAYS_LV"].ToString().Trim();
                dt.Rows.Add(rw);
            }
        }
        catch (OracleException ex)
        {
            //lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }
        }
    }
    private void TryTOcheck()
    {       

        try
        {
            lblLeaveDetail.Text = "<table width='100%' border='0' cellspacing='0' cellpadding='2'>";
            lblLeaveDetail.Text = lblLeaveDetail.Text + "<tr><td width='40%' align='left'></td><td width='20%' align='left'><b>Opening</b></td><td width='20%' align='right'><b>Availed</b></td><td width='20%' align='right'><b>Balance</b></td></tr>";

            if (leaveName_cl.Trim() != "")
            {
                // Suman code chang dure balance leave in minus value
                lblLeaveDetail.Text = lblLeaveDetail.Text + "<tr><td width='40%' align='left'>" + leaveName_cl + "</td><td width='20%' align='right'>" + String.Format("{0,00:#,##0.00}", cl) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_cl1) + "</td><td width='20%' align='right'>" + (String.Format("{0,0:#,##0.00}", cl - l_cl1)) + "</td></tr>";

            }
            if (leaveName_sl.Trim() != "")
            {
                lblLeaveDetail.Text = lblLeaveDetail.Text + "<tr><td width='40%' align='left'>" + leaveName_sl + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", sl) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_sl1) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", (sl - l_sl1)) + "</td></tr>";
            }
            if (leaveName_pl.Trim() != "")
            {
               // lblLeaveDetail.Text = lblLeaveDetail.Text + "<tr><td width='40%' align='left'>" + leaveName_pl + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", pl) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_pl1) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", (pl - l_pl1)) + "</td></tr>";
            }
            if (leaveName_ml.Trim() != "")
            {
                //lblLeaveDetail.Text = lblLeaveDetail.Text + "<tr><td width='40%' align='left'>" + leaveName_ml + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", ml) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_ml1) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", (ml - l_ml1)) + "</td></tr>";
            }
            if (leaveName_el.Trim() != "")
            {
                //lblLeaveDetail.Text = lblLeaveDetail.Text + "<tr><td width='40%' align='left'>" + leaveName_el + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", el) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_el1) + "</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", (el - l_el1)) + "</td></tr>";
            }

            lblLeaveDetail.Text = lblLeaveDetail.Text + "</table>";
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void getTotalLeaveDetail()
    {
        try
        {
            //lblTotalLeaveDetail.Text = "";

            //lblTotalLeaveDetail.Text = "<table width='100%' border='0' cellspacing='0' cellpadding='2'>";
            // lblTotalLeaveDetail.Text = lblTotalLeaveDetail.Text + "<tr><td width='40%' align='left'></td><td width='20%' align='left'><b>Opening</b></td><td width='20%' align='right'><b>Availed</b></td><td width='20%' align='right'><b>Balance</b></td></tr>";
            //lblTotalLeaveDetail.Text = lblTotalLeaveDetail.Text + "<tr><td width='40%' align='left'>Total Leave Without Pay</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_withoutpay1) + "</td></tr>";
            //lblTotalLeaveDetail.Text = lblTotalLeaveDetail.Text + "<tr><td width='40%' align='left'>Total Compensatory Leave</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", l_cm1) + "</td></tr>";
            //lblTotalLeaveDetail.Text = lblTotalLeaveDetail.Text + "<tr><td width='40%' align='left'>Total Other Leave</td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", (l_pl1 + l_ml1 + l_el1 + l_cl1 + l_sl1)) + "</td></tr>";
            //lblTotalLeaveDetail.Text = lblTotalLeaveDetail.Text + "<tr><td width='40%' align='left'>Total Leave Avail </td><td width='20%' align='right'>" + String.Format("{0,0:#,##0.00}", (l_pl1 + l_ml1 + l_el1 + l_cl1 + l_sl1 + l_withoutpay1 + l_cm1)) + "</td></tr>";
            //lblTotalLeaveDetail.Text = lblTotalLeaveDetail.Text + "</table>";
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    //////////////////////////////////////////////////////////////////////////////////////////////////
    protected void txtSalaryAmount_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblTotaladditionSalay.Text = "0";
            lblOtherDeduction.Text = "0";
            decimal ft_TotalDeduction = 0;
            string TotalDeduction = "0.00";
            foreach (GridViewRow rw3 in gvBasicCalculation.Rows)
            {
                Label SubHeadName = (Label)rw3.FindControl("lblSalaryName");
                Label lblSalaryId = (Label)rw3.FindControl("lblSalaryId");
                TextBox SubHeadText = (TextBox)rw3.FindControl("txtSalaryAmount");


                SubHeadText.Text = String.Format("{0,0:#0.00}", Convert.ToInt64(Convert.ToDecimal(SubHeadText.Text)));
                //suman//lblTotaladditionSalay.Text = String.Format("{0,0:#0.00}", Convert.ToInt64(Convert.ToDecimal(lblTotaladditionSalay.Text) + Convert.ToDecimal(SubHeadText.Text)));
                TotalDeduction = SubHeadText.Text.Trim();
                //suman//lblOtherDeduction.Text = Convert.ToString(String.Format("{0:0.00} ", Convert.ToInt64(Convert.ToDecimal(lblOtherDeduction.Text) + Convert.ToDecimal(SubHeadText.Text))));
                ft_TotalDeduction = ft_TotalDeduction + Convert.ToDecimal(TotalDeduction);
            }
            if (ft_TotalDeduction == 0)
            {
                lblOtherDeduction.Text = "0.00";
            }
            else
            {
                lblOtherDeduction.Text = ft_TotalDeduction.ToString().Trim();
            }
            
            GetActualSalary();
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtSalaryAmount_Loans_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblLoadTotal.Text = "0";
            foreach (GridViewRow rwl in gvDeductionCalculation_Loans.Rows)
            {
                Label SubHeadName = (Label)rwl.FindControl("lblSalaryName");
                TextBox SubHeadText = (TextBox)rwl.FindControl("txtSalaryAmount");

                SubHeadText.Text = String.Format("{0,0:#0.00}", Convert.ToInt64(Convert.ToDecimal(SubHeadText.Text)));
                lblLoadTotal.Text = Convert.ToString(String.Format("{0:0.00} ", Convert.ToInt64(Convert.ToDecimal(lblLoadTotal.Text) + Convert.ToDecimal(SubHeadText.Text))));

            }
            GetActualSalary();
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtSalaryAmount_Deduction_TextChanged(object sender, EventArgs e)
    {
        try
        {
            lblOtherDeduction.Text = "0";
            decimal ft_TotalDeduction=0;
            string TotalDeduction = "0.00";
            foreach (GridViewRow rwl in gvDeductionCalculation.Rows)
            {
                
                Label SubHeadName = (Label)rwl.FindControl("lblSalaryName");
                TextBox SubHeadText = (TextBox)rwl.FindControl("txtSalaryAmount");
                Label lblSalaryId = (Label)rwl.FindControl("lblSalaryId");
                TotalDeduction = SubHeadText.Text.Trim();
                //suman//lblOtherDeduction.Text = Convert.ToString(String.Format("{0:0.00} ", Convert.ToInt64(Convert.ToDecimal(lblOtherDeduction.Text) + Convert.ToDecimal(SubHeadText.Text))));
                ft_TotalDeduction = ft_TotalDeduction + Convert.ToDecimal(TotalDeduction);
            }
            if (ft_TotalDeduction == 0)
            {
                lblOtherDeduction.Text = "0.00";
            }
            else
            {
                lblOtherDeduction.Text = ft_TotalDeduction.ToString().Trim();
            }
            
            GetActualSalary();
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void GetActualSalary()
    {
        try
        {
            lblActualTotal.Text = Convert.ToString(String.Format("{0:0.00}", Convert.ToDecimal(lblTotaladditionSalay.Text.Trim()) - (Convert.ToDecimal(lblLoadTotal.Text.Trim()) + Convert.ToDecimal(lblOtherDeduction.Text.Trim()))));
            RupeesToWord o1 = new RupeesToWord();
            lblNetSalary.Text = lblActualTotal.Text;
            lblFiguretoWord.Text = o1.changeNumericToWords(Convert.ToDouble(lblActualTotal.Text)) + " only";
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////

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

            ViewState["SavingSubHeadTable"] = dt_SavingSubHeadTable;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void AddFieldinSavingSubHeadTable()
    {
        try
        {
            DataTable dt = (DataTable)ViewState["SavingSubHeadTable"];
            DataRow rw;
            for (int i = 1; i < 31; i++)
            {
                rw = dt.NewRow();
                rw["SubHeadName"] = "SUBH" + Convert.ToString(i);
                rw["SalaryAmount"] = "0";
                dt.Rows.Add(rw);
            }
            ViewState["SavingSubHeadTable"] = dt;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void fillingDataTable_SavingSubHeadTable()
    {
        try
        {
            lblTotaladditionSalay.Text = "0";
            lblOtherDeduction.Text = "0";
            lblLoadTotal.Text = "0";
            DataTable SavingSubHeadTable = (DataTable)ViewState["SavingSubHeadTable"];

            foreach (GridViewRow rw2 in gvBasicCalculation.Rows)
            {
                Label lblSubheadName = (Label)rw2.FindControl("lblSalaryFieldName");
                TextBox txtAmount = (TextBox)rw2.FindControl("txtSalaryAmount");
                foreach (DataRow rw1 in SavingSubHeadTable.Rows)
                {
                    if (rw1["SubHeadName"].ToString().Trim() == lblSubheadName.Text.ToString().Trim())
                    {
                        rw1["SalaryAmount"] = txtAmount.Text.ToString().Trim();
                        rw1.AcceptChanges();
                        lblTotaladditionSalay.Text = String.Format("{0,0:#0.00}", Convert.ToInt64(Convert.ToDecimal(lblTotaladditionSalay.Text) + Convert.ToDecimal(rw1["SalaryAmount"].ToString().Trim())));

                    }

                }
            }


            foreach (GridViewRow rw4 in gvDeductionCalculation.Rows)
            {
                Label lblSubheadName = (Label)rw4.FindControl("lblSalaryFieldName");
                TextBox txtAmount = (TextBox)rw4.FindControl("txtSalaryAmount");
                foreach (DataRow rw3 in SavingSubHeadTable.Rows)
                {
                    if (rw3["SubHeadName"].ToString().Trim() == lblSubheadName.Text.ToString().Trim())
                    {
                        rw3["SalaryAmount"] = txtAmount.Text.ToString().Trim();
                        rw3.AcceptChanges();
                        lblOtherDeduction.Text = String.Format("{0,0:#0.00}", Convert.ToInt64(Convert.ToDecimal(lblOtherDeduction.Text) + Convert.ToDecimal(rw3["SalaryAmount"].ToString().Trim())));

                    }

                }
            }

            foreach (GridViewRow rw6 in gvDeductionCalculation_Loans.Rows)
            {
                Label lblSubheadName = (Label)rw6.FindControl("lblSalaryFieldName");
                TextBox txtAmount = (TextBox)rw6.FindControl("txtSalaryAmount");
                foreach (DataRow rw5 in SavingSubHeadTable.Rows)
                {
                    if (rw5["SubHeadName"].ToString().Trim() == lblSubheadName.Text.ToString().Trim())
                    {
                        rw5["SalaryAmount"] = txtAmount.Text.ToString().Trim();
                        rw5.AcceptChanges();
                        lblLoadTotal.Text = String.Format("{0,0:#0.00}", Convert.ToInt64(Convert.ToDecimal(lblLoadTotal.Text) + Convert.ToDecimal(rw5["SalaryAmount"].ToString().Trim())));

                    }

                }
            }


            ViewState["SavingSubHeadTable"] = SavingSubHeadTable;
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void getSalaryPreparedBy()
    {
        try
        {
            string strSQL = "";
            strSQL = "SELECT SAL_SLIP_MST_ID,a.TDATE,USER_NAME FROM hr_sal_slip_mst a, CM_USER_MST b where ltrim(rtrim(a.TUSER))=ltrim(rtrim(b.USER_CODE)) and ltrim(rtrim(SAL_SLIP_MST_ID))='" + Convert.ToInt32(Request.QueryString["SalaryId"].ToString().Trim()) + "'";
            obj = new csSaitex();
            dr = obj.getDataReader(strSQL,CommandType.Text);

            if (dr.Read())
            {
                
                lblPreparedBy.Text = dr["USER_NAME"].ToString().Trim();
                string strPreparedDate = dr["TDATE"].ToString().Trim();
                strPreparedDate = strPreparedDate.Substring(0, 10);
                lblPreparedDate.Text = strPreparedDate.Trim();
                
            }

            dr.Close();
            dr.Dispose();
            dr = null;
            obj = null;
        }

        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
        }
    
    
    
    }

    private void getTotalDeduction()
    {
        try
        {
            lblOtherDeduction.Text = "0";
            decimal ft_TotalDeduction = 0;
            string TotalDeduction = "0.00";
            foreach (GridViewRow rwl in gvDeductionCalculation.Rows)
            {

                //Label SubHeadName = (Label)rwl.FindControl("lblSalaryName");
                TextBox SubHeadText = (TextBox)rwl.FindControl("txtSalaryAmount");
                //Label lblSalaryId = (Label)rwl.FindControl("lblSalaryId");
                TotalDeduction = SubHeadText.Text.Trim();
                //suman//lblOtherDeduction.Text = Convert.ToString(String.Format("{0:0.00} ", Convert.ToInt64(Convert.ToDecimal(lblOtherDeduction.Text) + Convert.ToDecimal(SubHeadText.Text))));
                ft_TotalDeduction = ft_TotalDeduction + Convert.ToDecimal(TotalDeduction);
            }
            if (ft_TotalDeduction == 0)
            {
                //lblOtherDeduction.Text = "0.00";
            }
            else
            {
                lblOtherDeduction.Text = ft_TotalDeduction.ToString().Trim();
            }

           
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }


    private void getTotalSubHeadTotal()
    {
        try
        {
            lblOtherDeduction.Text = "0";
            decimal ft_TotalDeduction = 0;
            string TotalDeduction = "0.00";
            foreach (GridViewRow rwl in gvBasicCalculation.Rows)
            {

                //Label SubHeadName = (Label)rwl.FindControl("lblSalaryName");
                TextBox SubHeadText = (TextBox)rwl.FindControl("txtSalaryAmount");
                //Label lblSalaryId = (Label)rwl.FindControl("lblSalaryId");
                TotalDeduction = SubHeadText.Text.Trim();
                //suman//lblOtherDeduction.Text = Convert.ToString(String.Format("{0:0.00} ", Convert.ToInt64(Convert.ToDecimal(lblOtherDeduction.Text) + Convert.ToDecimal(SubHeadText.Text))));
                ft_TotalDeduction = ft_TotalDeduction + Convert.ToDecimal(TotalDeduction);
            }
            if (ft_TotalDeduction == 0)
            {
                //lblTotalSubHeadAmount.Text = "0.00";
            }
            else
            {
                lblTotalSubHeadAmount.Text = ft_TotalDeduction.ToString().Trim();
            }


        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        int iRecordEffected = 0;

        try
        {
            int iRecordFound = 0;
            ///////////////////////////////////////  Code to insert the data /////////////////////////////////
            DataTable dt = (DataTable)ViewState["HoildayList"];
            createSavingSubHeadTable();
            AddFieldinSavingSubHeadTable();
            fillingDataTable_SavingSubHeadTable();
            GetActualSalary();
            DataTable SavingSubHeadTable = (DataTable)ViewState["SavingSubHeadTable"];
            if (iRecordFound == 0)
            {
                con = new OracleConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                con.Open();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "P_HR_SAL_SLIP_MST_UPDATE";
                cmd.CommandType = CommandType.StoredProcedure;

                param = new OracleParameter("P_SAL_SLIP_MST_ID", OracleType.Int32);
                param.Value = CommonFuction.funFixQuotes(Request.QueryString["SalaryId"].ToString().Trim());
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
                
                param = new OracleParameter("P_NET_SAL", OracleType.VarChar, 10);
                param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Convert.ToDecimal(lblNetSalary.Text.ToString().Trim())));
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_ERN_AMT", OracleType.VarChar, 10);
                param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Convert.ToDecimal(lblTotaladditionSalay.Text.ToString().Trim())));
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_LOAN_AMT", OracleType.VarChar, 10);
                param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Convert.ToDecimal(lblLoadTotal.Text.ToString().Trim())));
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_DEDCT_AMT", OracleType.VarChar, 10);
                param.Value = CommonFuction.funFixQuotes(String.Format("{0:0.00}", Convert.ToDecimal(lblOtherDeduction.Text.ToString().Trim())));
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                foreach (DataRow rw in SavingSubHeadTable.Rows)
                {
                    string FieldName = "P_" + rw["SubHeadName"].ToString().Trim();
                    //string FieldName = rw["SubHeadName"].ToString().Trim();
                    param = new OracleParameter(FieldName, OracleType.VarChar, 10);
                    param.Value = CommonFuction.funFixQuotes(rw["SalaryAmount"].ToString().Trim());
                    param.Direction = ParameterDirection.Input;
                    cmd.Parameters.Add(param);
                }

                param = new OracleParameter("P_LOCK_LV", OracleType.Char, 7);
                param.Value = "UnLock";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_SAVE_STATUS", OracleType.Char, 2);
                param.Value = "M";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_TUSER", OracleType.VarChar, 10);
                //SaitexBL.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexBL.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                //param.Value = oUserLoginDetail.UserCode;
                param.Value = "SA0001";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
                
                param = new OracleParameter("P_TDATE", OracleType.DateTime);
                param.Value = System.DateTime.Now.ToShortDateString();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
                
                
                iRecordEffected = cmd.ExecuteNonQuery();
                if (iRecordEffected == 1)
                {
                    Response.Redirect("./GridDispayofSalary.aspx?SaveStaus=1", false);
                    Session["saveStatus"] = 1;
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script language='javascript'  type='text/javascript'> alert('Salary Slip Not Successfully !');  </script>");
                }

            }
        }

        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
        }

        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
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
