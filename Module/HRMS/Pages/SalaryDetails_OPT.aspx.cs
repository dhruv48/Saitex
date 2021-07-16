using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class HRMS_SalaryDetails_OPT : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            bindGradeMaster();
        }
    }
    private void bindGradeMaster()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetGradeMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlGrade.DataSource = dt;
                ddlGrade.DataTextField = "MST_DESC";
                ddlGrade.DataValueField = "MST_CODE";
                ddlGrade.DataBind();
            }
            //ddlGrade.Items.Insert(0, new ListItem("------------Select-----------", ""));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        string QueryString = "";
        bool flag = false;
        if (ddlGrade.SelectedValue.Trim() != "")
        {
            if (flag)
                QueryString = QueryString + "";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "GRADE_ID=" + ddlGrade.SelectedValue.Trim();
            flag = true;
        }

        string URL = "SalaryDetails_RPT.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=1000');", true);
    }
}
