using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
public partial class Module_HRMS_Controls_MonthlyLeaveQuery : System.Web.UI.UserControl
{
     
    //GridView grid1 = new Grid();
    protected void Page_Load(object sender, EventArgs e)
     {
       
        if (!IsPostBack)
        {
             tdShowGrid.Visible = false;
        }
    }
    private void bindGvMonthlyLeave()
    {
        try
        {
           // SaitexDM.Common.DataModel. oHR_LV_MST = new SaitexDM.Common.DataModel.HR_LV_MST();
            DataTable dt = new DataTable();
            string FromDate = txtFromDate.Text.Trim();
            string ToDate = txtToDate.Text.Trim();
            dt = SaitexBL.Interface.Method.HR_LV_MST.SelectMonthlyLeave(FromDate, ToDate);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
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
    

    protected void btndisplay_Click(object sender, EventArgs e)
    {
        try
        {
            tdShowGrid.Visible = true;
            bindGvMonthlyLeave();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

