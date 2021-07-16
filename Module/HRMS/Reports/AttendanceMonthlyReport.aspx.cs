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
    string  Month = string.Empty ;
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
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            Create_Data_Table();
            CompCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            BranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
            if (Request.QueryString["Month"] != null)
            {
                Month = Request.QueryString["Month"].ToString();
            }
            else
            {
                Month = System.DateTime.Now.Month.ToString();
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
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in report uploading"));
        }
    

    }
    private void LoadEmpData()
    {
        try
        {
            DataTable dt2 = SaitexBL.Interface.Method.HR_ATTN_TRN.GetEmpData(CompCode, BranchCode);
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
    private void LoadData(String mth, String yr, string Comp_Code, string Branch_Code, string Emp_Code)
    {

        int rnum = 0;
        Decimal p = 0;
        Decimal a = 0;
        Decimal w = 0;
        Decimal l = 0;
        Decimal n = 0;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_ATTN_TRN.Load_Attendance_Record(mth, yr, Comp_Code, Branch_Code, emp_code);
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
                    rw[rnum - 1] = rw[rnum - 1] + "/" + dr["ATTN_TYPE"].ToString().Trim();
                }
                else
                {
                    nd = dr["ADATE"].ToString().Trim();
                    rw[rnum] = dr["ATTN_TYPE"].ToString().Trim();
                    rnum++;
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "P") == 0)
                {
                    p += Convert.ToDecimal(dr["ATTN_DAY"]);
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "A") == 0)
                {
                    a++;
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "WO") == 0)
                {
                    w += Convert.ToDecimal(dr["ATTN_DAY"]);
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "CL") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTN_DAY"]);
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "SL") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTN_DAY"]);
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "EL") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTN_DAY"]);
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "CO") == 0)
                {
                    l += Convert.ToDecimal(dr["ATTN_DAY"]);
                }
                if (string.Compare(dr["ATTN_TYPE"].ToString().Trim(), "NH") == 0)
                {
                    n += Convert.ToDecimal(dr["ATTN_DAY"]);
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
}
