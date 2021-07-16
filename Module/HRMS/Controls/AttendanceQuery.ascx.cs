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
using errorLog;
public partial class Module_HRMS_Controls_AttendanceQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string CompCode = string.Empty;
    string BranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            CompCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            BranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
            if (!IsPostBack)
            {

                Load_Control();
                Grid1.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }
    private void Load_Control()
    {
        try
        {
            DataSet DSet = SaitexBL.Interface.Method.HR_ATTN_TRN.Get_Attn_Month();
            DDLMonth.DataSource = DSet.Tables["TMonth"];
            DDLMonth.DataValueField = "MNO";
            DDLMonth.DataTextField = "MONTH";
            DDLMonth.DataBind();
            DDLYear.DataSource = DSet.Tables["TYear"];
            DDLYear.DataValueField = "YEAR";
            DDLYear.DataTextField = "YEAR";
            DDLYear.DataBind();
            DDLYear.Items.Insert(0, "------Select-------");
            DDLMonth.Items.Insert(0, "------Select-------");
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message.ToString());
        }
    }
    private void bindGvAttendance(DataTable dt)
    {
        try
        {
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch
        {
            throw;
        }
    }
    private void Gettotal(DataTable dt)
    {
        try
        {
            int count = 0;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                count = 0;
                string s1 = dt.Rows[i]["1"].ToString();
                string s2 = dt.Rows[i]["2"].ToString();
                string s3 = dt.Rows[i]["3"].ToString();
                string s4 = dt.Rows[i]["4"].ToString();
                string s5 = dt.Rows[i]["5"].ToString();
                string s6 = dt.Rows[i]["6"].ToString();
                string s7 = dt.Rows[i]["7"].ToString();
                string s8 = dt.Rows[i]["8"].ToString();
                string s9 = dt.Rows[i]["9"].ToString();
                string s10 = dt.Rows[i]["10"].ToString();
                string s11 = dt.Rows[i]["11"].ToString();
                string s12 = dt.Rows[i]["12"].ToString();
                string s13 = dt.Rows[i]["13"].ToString();
                string s14 = dt.Rows[i]["14"].ToString();
                string s15 = dt.Rows[i]["15"].ToString();
                string s16 = dt.Rows[i]["16"].ToString();
                string s17 = dt.Rows[i]["17"].ToString();
                string s18 = dt.Rows[i]["18"].ToString();
                string s19 = dt.Rows[i]["19"].ToString();
                string s20 = dt.Rows[i]["20"].ToString();
                string s21 = dt.Rows[i]["21"].ToString();
                string s22 = dt.Rows[i]["22"].ToString();
                string s23 = dt.Rows[i]["23"].ToString();
                string s24 = dt.Rows[i]["24"].ToString();
                string s25 = dt.Rows[i]["25"].ToString();
                string s26 = dt.Rows[i]["26"].ToString();
                string s27 = dt.Rows[i]["27"].ToString();
                string s28 = dt.Rows[i]["28"].ToString();
                string s29 = dt.Rows[i]["29"].ToString();
                string s30 = dt.Rows[i]["30"].ToString();
                string s31 = dt.Rows[i]["31"].ToString();

                if (Convert.ToInt32(s1.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s2.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s3.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s4.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s5.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s6.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s7.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s8.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s9.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s10.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s11.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s12.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s13.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s14.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s15.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s16.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s17.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s18.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s19.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s20.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s21.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s22.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s23.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s24.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s25.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s26.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s27.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s28.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s29.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s30.ToString()) == 1)
                    count++;
                if (Convert.ToInt32(s31.ToString()) == 1)
                    count++;


                dt.Rows[i]["TotalP"] = count;
            }
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetData(int Month, int yr)
    {

        int total = 0;
        try
        {            
            DataTable dt = SaitexBL.Interface.Method.HR_ATTN_TRN.GetReport(Month,yr,CompCode,BranchCode);
            dt.Columns.Add("TotalP", typeof(int));
            foreach (DataRow dr in dt.Rows)
            {
                dr["TotalP"] = total;
            }
            Gettotal(dt);
            return dt;
        }
        catch (OracleException ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }


    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                Response.Redirect(Session["RedirectURL"].ToString(), false);
                Session["RedirectURL"] = null;
            }
            else
            {
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void DDLMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Grid1.Visible = true;
            int mon = 0; int yr = 0;
            if (DDLMonth.SelectedValue != "" && DDLMonth.SelectedValue != null)
            {
                mon = Convert.ToInt32(DDLMonth.SelectedValue.Trim());
            }
            if (DDLYear.SelectedValue != "" && DDLYear.SelectedValue != null)
            {
                yr = Convert.ToInt32(DDLYear.SelectedValue.Trim());
            }
            DataTable dt = GetData(mon, yr);
            bindGvAttendance(dt);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading.\r\nplease see error log"));
        }
    }
}
