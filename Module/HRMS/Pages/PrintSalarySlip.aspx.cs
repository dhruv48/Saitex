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
using DBLibrary;
using Common;
using errorLog;
using AmountToWords;
public partial class Module_HRMS_Pages_PrintSalarySlip : System.Web.UI.Page
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
       
        if (!IsPostBack)
        {
            //if (Request.QueryString["SalaryId"] != null)
            //{
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
                //}
            }
     }

    private int CheckingLockingStatus(string TableId)
    {
        int result = 0;
        try
        {
            string strLockSataus = "SELECT SAL_SLIP_MST_ID,LOCK_LV FROM hr_sal_slip_mst where ltrim(rtrim(SAL_SLIP_MST_ID))='" + TableId.Trim() + "' and LOCK_LV='Lock' and ltrim(rtrim(DEL_STATUS))='0'";
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
            //string strGetSalary = "SELECT ltrim(rtrim(vc_SalarySlipMasterId)) vc_SalarySlipMasterId, ch_EmployeeMasterId,vc_Year,vc_Month, DATENAME(month, (vc_Year+'-'+vc_Month+'-01')) AS MonthName,vc_WorkingDay, vc_Hoilday, vc_PaidDay, vc_NetSalary, vc_Epf, vc_EarningAmount, vc_LoadAmount, vc_DeductionAmount, vc_CasualLeave, vc_SickLeave, vc_EarnLeave, vc_MaternityLeave, vc_PaternityLeave, vc_CompensatoryLeave, vc_LeaveWithoutPay,vc_OpeningCasualLeave,vc_OpeningSickLeave,vc_OpeningEarnLeave,vc_OpeningMaternityLeave,vc_OpeningPaternityLeave,vc_OpeningCompensatoryLeave,vc_OpeningLeaveWithoutPay,vc_AvialCasualLeave,vc_AvialSickLeave,vc_AvialEarnLeave,vc_AvialMaternityLeave,vc_AvialPaternityLeave,vc_AvialCompensatoryLeave,vc_AvialLeaveWithoutPay, SubHead1, SubHead2, SubHead3, SubHead4, SubHead5, SubHead6, SubHead7, SubHead8, SubHead9, SubHead10, SubHead11, SubHead12, SubHead13, SubHead14, SubHead15, SubHead16, SubHead17, SubHead18, SubHead19, SubHead20, SubHead21, SubHead22, SubHead23, SubHead24, SubHead25, SubHead26, SubHead27, SubHead28, SubHead29, SubHead30, ch_LockLeave, dt_Created, dt_Updated FROM tblSalarySlipMaster where ltrim(rtrim(vc_SalarySlipMasterId))='32'";
            //string strGetSalary = "SELECT ltrim(rtrim(SAL_SLIP_MST_ID)) SAL_SLIP_MST_ID, EMP_CODE,SAL_YEAR,SAL_MONTH, SAL_WORKING_DAY, HLD, PAID_DAY, NET_SAL, EPF, ERN_AMT, LOAN_AMT, DEDCT_AMT, CASUAL_LV, EARN_LV, MATERNITY_LV, PATERNITY_LV, COMPENSATORY_LV, LV_WITHOUT_PAY,OPENING_CASUAL_LV,OPENING_SICK_LV,OPENING_EARN_LV,OPENING_MATERNITY_LV,OPENING_PATERNITY_LV,OPENING_COMPENSATORY_LV,OPENING_LV_WITHOUT_PAY,AVIAL_CASUAL_LV,AVIAL_SICK_LV,AVIAL_EARN_LV,AVIAL_MATERNITY_LV,AVIAL_MATERNITY_LV,AVIAL_COMPENSATORY_LV,AVIAL_LV_WITHOUT_PAY, SUBH1, SUBH2, SUBH3, SUBH4, SUBH5, SUBH6, SUBH7, SUBH8, SUBH9, SUBH10, SUBH11, SUBH12, SUBH13, SUBH14, SUBH15, SUBH16, SUBH17, SUBH18, SUBH19, SUBH20, SUBH21, SUBH22, SUBH23, SUBH24, SUBH25, SUBH26, SUBH27, SUBH28, SUBH29, SUBH30, LOCK_LV, TDATE FROM HR_SAL_SLIP_MST where ltrim(rtrim(SAL_SLIP_MST_ID))='32'";
            string strGetSalary = "SELECT * FROM HR_SAL_SLIP_MST where ltrim(rtrim(SAL_SLIP_MST_ID))='32'";
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
            string strEmployeeRecord = "";
            strEmployeeRecord = "SELECT Em.EMP_CODE EMP_CODE,CARD_NO, (F_NAME||' '||M_NAME||' '|| L_NAME) EmployeeName,GENDER, Em.COMP_CODE,Em.BRANCH_CODE,Bm.BRANCH_NAME,F_H_NAME,JOIN_DT, Em.DEPT_CODE,Em.DESIG_CODE,Em.COMP_CODE, Bm.BRANCH_CODE,PAY_MODE,POSTED_LEN,COMP_NAME,DESIG_NAME, DEPT_NAME, PF_AC_NO,AC_NO ,BANK_NAME FROM HR_EMP_MST Em,CM_COMPANY_MST Cm,CM_BRANCH_MST Bm, CM_DESIG_MST Pd ,CM_DEPT_MST dm,HR_EMP_COMP_INFO eci,HR_BANK_MST bn where Em.COMP_CODE=Cm.COMP_CODE and Em.BRANCH_CODE=Bm.BRANCH_CODE  and em.EMP_CODE=eci.EMP_CODE and eci.BANK_CODE=bn.BANK_CODE and Em.DESIG_CODE=Pd.DESIG_CODE and Em.DEPT_CODE=Dm.DEPT_CODE and ltrim(rtrim(em.EMP_CODE))='" + EMP_CODE.Trim() + "' ";
            obj = new csSaitex();
            dr = obj.getDataReader(strEmployeeRecord, CommandType.Text);
            if (dr.Read() == true)
            {
                lblEmployeeName.Text = dr["EmployeeName"].ToString().Trim();
                lblEnrolNumber.Text = dr["CARD_NO"].ToString().Trim();
                lblDepartment.Text = dr["DEPT_NAME"].ToString().Trim();
                lblDesignation.Text = dr["DESIG_NAME"].ToString().Trim();
                lblComapanyName.Text = dr["COMP_NAME"].ToString().Trim();
                lblFatherName.Text = dr["F_H_NAME"].ToString().Trim();
                string strJoiningDate = "";
                strJoiningDate = dr["JOIN_DT"].ToString().Trim();
                strJoiningDate = strJoiningDate.Substring(0, 9);
                lblJoining.Text = strJoiningDate;
                lblPayMode.Text = dr["PAY_MODE"].ToString().Trim();
                lblPF.Text = dr["PF_AC_NO"].ToString().Trim();
                lblAccountNumber.Text = dr["AC_NO"].ToString().Trim();
                lblBankName.Text = dr["BANK_NAME"].ToString().Trim();
                lblBranchName.Text = dr["BRANCH_NAME"].ToString().Trim();
                //ViewState["vc_MaximumEmployeePF"] = dr["vc_MaximumEmployeePF"].ToString().Trim();
                //ViewState["vc_EmployeesEPS"] = dr["vc_EmployeesEPS"].ToString().Trim();
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
            //lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>" + leaveName_cm.Trim() + ":" + String.Format("{0:0.00}", l_cm) + "</td>";
            //lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td></td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " <td>Paid Days :" + String.Format("{0:0.00}", PayDay) + "</td>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " </tr>";
            lblCurrentMonthCalculation.Text = lblCurrentMonthCalculation.Text + " </table>";
        }
        catch (Exception ex)
        {
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void getrecordfromSalaryslip()
    {
        try
        {
            //string strGetSalary = "SELECT SAL_SLIP_MST_ID, EMP_CODE,SAL_YEAR, SAL_MONTH,SAL_WORKING_DAY, HLD, PAID_DAY, NET_SAL, EPF, ERN_AMT, LOAN_AMT, DEDCT_AMT, CASUAL_LV, SICK_LV, EARN_LV, MATERNITY_LV, PATERNITY_LV, COMPENSATORY_LV, LV_WITHOUT_PAY, SUBH1, SUBH2, SUBH3, SUBH4, SUBH5, SUBH6, SUBH7, SUBH8, SUBH9, SUBH10, SUBH11, SUBH12, SUBH13, SUBH14, SUBH15, SUBH16, SUBH17, SUBH18, SUBH19, SUBH20, SUBH21, SUBH22, SUBH23, SUBH24, SUBH25, SUBH26, SUBH27, SUBH28, SUBH29, SUBH30, LOCK_LV, TDATE  FROM hr_sal_slip_mst where ltrim(rtrim(SAL_SLIP_MST_ID))='32'";
            string strGetSalary = "SELECT SAL_SLIP_MST_ID, EMP_CODE,SAL_YEAR, SAL_MONTH,SAL_WORKING_DAY, HLD, PAID_DAY, NET_SAL, EPF, ERN_AMT, LOAN_AMT, DEDCT_AMT, CASUAL_LV, SICK_LV, EARN_LV, MATERNITY_LV, PATERNITY_LV, COMPENSATORY_LV, LV_WITHOUT_PAY, SUBH1, SUBH2, SUBH3, SUBH4, SUBH5, SUBH6, SUBH7, SUBH8, SUBH9, SUBH10, SUBH11, SUBH12, SUBH13, SUBH14, SUBH15, SUBH16, SUBH17, SUBH18, SUBH19, SUBH20, SUBH21, SUBH22, SUBH23, SUBH24, SUBH25, SUBH26, SUBH27, SUBH28, SUBH29, SUBH30, LOCK_LV, a.TDATE,USER_NAME  FROM hr_sal_slip_mst a, CM_USER_MST b  where ltrim(rtrim(a.TUSER))=ltrim(rtrim(b.USER_CODE)) and ltrim(rtrim(SAL_SLIP_MST_ID))='32'";

            obj = new csSaitex();
            ds = obj.getDataSet(strGetSalary, CommandType.Text);
            ViewState["GetSalaryDetail"] = ds;
        }
        catch (OracleException ex)
        {
            // lblErrorMessage.Text = ex.Message;
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
    private void createDataTable_BasicCalculation()
    {
        try
        {
            DataTable dt_BasicCalculation = new DataTable();
            DataColumn dt_colounm;

            //dt_colounm = new DataColumn();
            //dt_colounm.ColumnName = "ch_EmployeeMasterId";
            //dt_colounm.DataType = Type.GetType("System.String");
            //dt_BasicCalculation.Columns.Add(dt_colounm);

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
            // lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void createDataTable_BasicCalculation_Deduction()
    {
        try
        {
            DataTable dt_BasicCalculation_Deduction = new DataTable();
            DataColumn dt_colounm;

            //dt_colounm = new DataColumn();
            //dt_colounm.ColumnName = "ch_EmployeeMasterId";
            //dt_colounm.DataType = Type.GetType("System.String");
            //dt_BasicCalculation.Columns.Add(dt_colounm);

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
        //DataTable dt = (DataTable)ViewState["AvailableLeave_Detail"];

        //foreach (DataRow dr in dt.Rows)
        //{
        //    if (dr["ch_LeaveType"].ToString().Trim() == "1")
        //    {
        //        leaveName_cl = "Casual Leave";
        //        cl = cl + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //    if (dr["ch_LeaveType"].ToString().Trim() == "2")
        //    {
        //        leaveName_el = "Earn Leave";
        //        el = el + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }
        //    if (dr["ch_LeaveType"].ToString().Trim() == "3")
        //    {
        //        leaveName_sl = "Sick Leave";
        //        sl = sl + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }
        //    if (dr["ch_LeaveType"].ToString().Trim() == "4")
        //    {
        //        leaveName_pl = "Paternity Leave";
        //        pl = pl + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }
        //    if (dr["ch_LeaveType"].ToString().Trim() == "5")
        //    {
        //        leaveName_ml = "Maternity Leave";
        //        ml = ml + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //    if (dr["ch_LeaveType"].ToString().Trim() == "6")
        //    {
        //        leaveName_cm = "Compensatory Leave";
        //        cm = cm + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //    if (dr["ch_LeaveType"].ToString().Trim() == "7")
        //    {
        //        leaveName_lwp = "Without Pay";
        //        withoutpay = withoutpay + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //}

        //dt = (DataTable)ViewState["TakenLeave_Detail"];

        //foreach (DataRow dr in dt.Rows)
        //{
        //    if (dr["ch_LeaveType"].ToString().Trim() == "1")
        //    {
        //        leaveName_cl = "Casual Leave";
        //        l_cl1 = l_cl1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //    if (dr["ch_LeaveType"].ToString().Trim() == "2")
        //    {
        //        leaveName_el = "Earn Leave";
        //        l_el1 = l_el1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }
        //    if (dr["ch_LeaveType"].ToString().Trim() == "3")
        //    {
        //        leaveName_sl = "Sick Leave";
        //        l_sl1 = l_sl1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }
        //    if (dr["ch_LeaveType"].ToString().Trim() == "4")
        //    {
        //        leaveName_pl = "Paternity Leave";
        //        l_pl1 = l_pl1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }
        //    if (dr["ch_LeaveType"].ToString().Trim() == "5")
        //    {
        //        leaveName_ml = "Maternity Leave";
        //        l_ml1 = l_ml1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //    if (dr["ch_LeaveType"].ToString().Trim() == "6")
        //    {
        //        leaveName_cm = "Compensatory Leave";
        //        l_cm1 = l_cm1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //    if (dr["ch_LeaveType"].ToString().Trim() == "7")
        //    {
        //        leaveName_lwp = "Without Pay";
        //        l_withoutpay1 = l_withoutpay1 + Convert.ToDecimal(dr["DAYS_LV"].ToString().Trim());
        //    }

        //}

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
            decimal ft_TotalDeduction = 0;
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
            dr = obj.getDataReader(strSQL, CommandType.Text);

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
                lblOtherDeduction.Text = "0.00";
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
                lblTotalSubHeadAmount.Text = "0.00";
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
}
