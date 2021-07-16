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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OracleClient;

public partial class Module_HRMS_Pages_AttendanceReport : System.Web.UI.Page
{
    int Month = 0;
    int Year = System.DateTime.Now.Year;
    string CompCode = string.Empty;
    string BranchCode = string.Empty;
    string od = string.Empty;
    string nd = string.Empty;
    string emp_code = string.Empty;
    string name = string.Empty;
    String url = string.Empty;
   

    string[,] strAdays = new string[1, 31];
    DataTable dt1 = new DataTable();
   
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        Create_Data_Table();
        CompCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
        BranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
        if (Request.QueryString["Month"] != null)
        {
            Month = Convert.ToInt32(Request.QueryString["Month"]);
        }
        else
        {
            Month = Convert.ToInt32(System.DateTime.Now.Month);
        }
        if (Request.QueryString["year"] != null)
        {
            Year = Convert.ToInt32(Request.QueryString["year"]);
        }
        else
        {
            Year = Convert.ToInt32(System.DateTime.Now.Year);
        }
        LoadEmpData();
        getReport(dt1);

    }
    private void LoadEmpData()
    {
        try
        {
            DataTable dt2 = SaitexBL.Interface.Method.HR_ATTN_TRN.GetEmpData(CompCode,BranchCode);
            foreach (DataRow dr in dt2.Rows)
            {
                emp_code = dr["EMP_CODE"].ToString().Trim();
                name = dr["NAME"].ToString().Trim();
                LoadData(Month.ToString(), Year.ToString(), CompCode, BranchCode, emp_code);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    private void LoadData(String mth, String yr,string Comp_Code,string Branch_Code,string Emp_Code)
    {

        int rnum = 0;
        Decimal p = 0;
        Decimal a = 0;
        Decimal w = 0;
        Decimal l = 0;
        Decimal n = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_ATTN_TRN.Load_Attendance_Record (mth, yr,Comp_Code,Branch_Code,emp_code);
            ArrayList Days = new ArrayList();
            DataRow rw;
            rw = dt1.NewRow();
            rw["ecode"] = emp_code.ToString().Trim();
            rnum++;
            rw["name"] = name.ToString().Trim();
            rnum++;
            foreach (DataRow dr in dt.Rows)
            {               
                if (string.Compare(dr["ADATE"].ToString().Trim(), nd.ToString().Trim()) == 0)
                {
                    rw[rnum - 1] = rw[rnum - 1] + "/" + dr["ATTNTYPE"].ToString().Trim();
                }
                else
                {
                    nd = dr["ADATE"].ToString().Trim();
                    rw[rnum] = dr["ATTNTYPE"].ToString().Trim();
                    rnum++;
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "P") == 0)
                {
                    p += Convert.ToDecimal(dr["ATTNVAL"]);
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "A") == 0)
                {
                    a++;
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "WO") == 0)
                {
                    w += Convert.ToDecimal(dr["ATTNVAL"]);
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "CL") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTNVAL"]);
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "SL") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTNVAL"]);
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "EL") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTNVAL"]);
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "CO") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTNVAL"]);
                }
                if (string.Compare(dr["ATTNTYPE"].ToString().Trim(), "NH") == 0)
                {
                    n += Convert.ToDecimal(dr["ATTNVAL"]);
                }
            }
            rw["PR"] = p.ToString().Trim();
            rw["AB"] = a.ToString().Trim();
            rw["WO"] = w.ToString().Trim();
            rw["LV"] = l.ToString().Trim();
            rw["NH"] = n.ToString().Trim();
            dt1.Rows.Add(rw);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    private void getReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load((Server.MapPath(@"\Saitex\Module\HRMS\Reports\MonthlyAttendance.rpt")));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }
    private void Create_Data_Table()
    {
        try
        {
            dt1.Columns.Add("ecode", typeof(string));
            dt1.Columns.Add("name", typeof(string));
            dt1.Columns.Add("d1", typeof(string));
            dt1.Columns.Add("d2", typeof(string));
            dt1.Columns.Add("d3", typeof(string));
            dt1.Columns.Add("d4", typeof(string));
            dt1.Columns.Add("d5", typeof(string));
            dt1.Columns.Add("d6", typeof(string));
            dt1.Columns.Add("d7", typeof(string));
            dt1.Columns.Add("d8", typeof(string));
            dt1.Columns.Add("d9", typeof(string));
            dt1.Columns.Add("d10", typeof(string));
            dt1.Columns.Add("d11", typeof(string));
            dt1.Columns.Add("d12", typeof(string));
            dt1.Columns.Add("d13", typeof(string));
            dt1.Columns.Add("d14", typeof(string));
            dt1.Columns.Add("d15", typeof(string));
            dt1.Columns.Add("d16", typeof(string));
            dt1.Columns.Add("d17", typeof(string));
            dt1.Columns.Add("d18", typeof(string));
            dt1.Columns.Add("d19", typeof(string));
            dt1.Columns.Add("d20", typeof(string));
            dt1.Columns.Add("d21", typeof(string));
            dt1.Columns.Add("d22", typeof(string));
            dt1.Columns.Add("d23", typeof(string));
            dt1.Columns.Add("d24", typeof(string));
            dt1.Columns.Add("d25", typeof(string));
            dt1.Columns.Add("d26", typeof(string));
            dt1.Columns.Add("d27", typeof(string));
            dt1.Columns.Add("d28", typeof(string));
            dt1.Columns.Add("d29", typeof(string));
            dt1.Columns.Add("d30", typeof(string));
            dt1.Columns.Add("d31", typeof(string));
            dt1.Columns.Add("PR", typeof(string));
            dt1.Columns.Add("AB", typeof(string));
            dt1.Columns.Add("WO", typeof(string));
            dt1.Columns.Add("LV", typeof(string));
            dt1.Columns.Add("NH", typeof(string));
        }
        catch
        {
            throw;
        }
    }
    //private void Gettotal(DataTable dt)
    //{
    //    int count = 0;
    //    int total = 0;

    //    for (int i = 0; i < dt.Rows.Count; i++)
    //    {
    //        count = 0;
    //        string s1 = dt.Rows[i]["1"].ToString();
    //        string s2 = dt.Rows[i]["2"].ToString();
    //        string s3 = dt.Rows[i]["3"].ToString();
    //        string s4 = dt.Rows[i]["4"].ToString();
    //        string s5 = dt.Rows[i]["5"].ToString();
    //        string s6 = dt.Rows[i]["6"].ToString();
    //        string s7 = dt.Rows[i]["7"].ToString();
    //        string s8 = dt.Rows[i]["8"].ToString();
    //        string s9 = dt.Rows[i]["9"].ToString();
    //        string s10 = dt.Rows[i]["10"].ToString();
    //        string s11 = dt.Rows[i]["11"].ToString();
    //        string s12 = dt.Rows[i]["12"].ToString();
    //        string s13 = dt.Rows[i]["13"].ToString();
    //        string s14 = dt.Rows[i]["14"].ToString();
    //        string s15 = dt.Rows[i]["15"].ToString();
    //        string s16 = dt.Rows[i]["16"].ToString();
    //        string s17 = dt.Rows[i]["17"].ToString();
    //        string s18 = dt.Rows[i]["18"].ToString();
    //        string s19 = dt.Rows[i]["19"].ToString();
    //        string s20 = dt.Rows[i]["20"].ToString();
    //        string s21 = dt.Rows[i]["21"].ToString();
    //        string s22 = dt.Rows[i]["22"].ToString();
    //        string s23 = dt.Rows[i]["23"].ToString();
    //        string s24 = dt.Rows[i]["24"].ToString();
    //        string s25 = dt.Rows[i]["25"].ToString();
    //        string s26 = dt.Rows[i]["26"].ToString();
    //        string s27 = dt.Rows[i]["27"].ToString();
    //        string s28 = dt.Rows[i]["28"].ToString();
    //        string s29 = dt.Rows[i]["29"].ToString();
    //        string s30 = dt.Rows[i]["30"].ToString();
    //        string s31 = dt.Rows[i]["31"].ToString();

    //        if (Convert.ToInt32(s1.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s2.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s3.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s4.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s5.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s6.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s7.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s8.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s9.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s10.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s11.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s12.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s13.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s14.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s15.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s16.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s17.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s18.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s19.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s20.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s21.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s22.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s23.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s24.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s25.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s26.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s27.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s28.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s29.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s30.ToString()) == 1)
    //            count++;
    //        if (Convert.ToInt32(s31.ToString()) == 1)
    //            count++;


    //        dt.Rows[i]["TotalP"] = count;
    //    }
    //}
    //private DataTable GetData(int Month, int yr, string CompCode, string BranchCode)
    //{

    //    int total = 0;
    //    try
    //    {

    //        SaitexDM.Common.DataModel.HR_ATTN_TRN oHR_ATTN_TRN = new SaitexDM.Common.DataModel.HR_ATTN_TRN();
    //        DataTable dt = SaitexBL.Interface.Method.HR_ATTN_TRN.GetReport(Month, yr,CompCode,BranchCode );

    //        dt.Columns.Add("TotalP", typeof(int));
    //        foreach (DataRow dr in dt.Rows)
    //        {
    //            dr["TotalP"] = total;
    //        }
    //        Gettotal(dt);

    //        return dt;

    //    }
    //    catch (OracleException ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        return null;
    //    }

    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //        return null;
    //    }
    //}


    //protected void btnView_Click(object sender, EventArgs e)
    //{
    //    url = "../Reports/MonthlyAttendance.aspx";
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    //}
}
