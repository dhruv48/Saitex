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
using System.Text;
using errorLog;

public partial class Module_HRMS_Controls_AttendanceOPT : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Load_Control();
        }
    }
    private void Load_Control()
    {
        try
        {
            DataSet  DSet = SaitexBL.Interface.Method.HR_ATTN_TRN.Get_Attn_Month();
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
       if (DDLMonth.SelectedIndex != 0 && DDLYear.SelectedIndex!=0)
        {
          Response.Redirect("../Reports/AttendanceMonthlyReport.aspx?Year=" + DDLYear.SelectedValue.ToString() + "&MONTH=" + DDLMonth.SelectedValue.ToString());
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
}
