using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_OrderDevelopment_Pages_BatchIssueReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Initial_Control();
        }
    }

    private void Initial_Control()
    {
        try
        {
            getMachines();
            imgbtnClear.Visible = true;
            imgbtnExit.Visible = true;
            imgbtnHelp.Visible = true;

            ddlmachine.SelectedIndex = -1;

            TxtFdate.Text = string.Empty;
            TxtTdate.Text = string.Empty;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    public void getMachines()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetMachineCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlmachine.Items.Clear();
                ddlmachine.DataSource = dt;
                ddlmachine.DataTextField = "MACHINE_CODE";
                ddlmachine.DataValueField = "MACHINE_CODE";
                ddlmachine.DataBind();
                ddlmachine.Items.Insert(0, new ListItem("------SELECT----", ""));
            }
        }
        catch
        {
            throw;
        }
    }

  
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Initial_Control();

    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = "";


                //if (TxtFdate.Text.Trim() == "")
                //    msg += "Enter the From Date.</ br>";
                //if (TxtTdate.Text.Trim() == "")
                //    msg += "Enter the To Date</ br>";
                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FDate=" + TxtFdate.Text.Trim();
                    QueryString += "&TDate=" + TxtTdate.Text.Trim();
                    QueryString += "&Machine=" + ddlmachine.SelectedValue.ToString();
                    string URL = "../Reports/BatchIssueReport.aspx" + QueryString;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                }
                else
                    Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {

        }
    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnview_Click(object sender, EventArgs e)
    {

    }
}