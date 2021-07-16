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

public partial class Module_HRMS_Pages_SalarySlipView : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Load_Month();
        }
    }
    private void Load_Month()
    {
        try
        {
           DataSet  DS= SaitexBL.Interface.Method.HR_EMP_SAL_MST.Get_Salary_Month(Session["EmpCode"].ToString());
           DDLMonth.DataSource = DS.Tables["MONTH"];
           DDLMonth.DataValueField = "SAL_MONTH";
           DDLMonth.DataTextField = "MONTH";
           DDLMonth.DataBind();
           DDLYear.DataSource = DS.Tables["YEAR"];
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
    protected void CmdDisplay_Click(object sender, EventArgs e)
    {
        if (DDLMonth.SelectedIndex != 0 && DDLYear.SelectedIndex!=0)
        {
            Response.Redirect("./PrintSalrySlip.aspx?EmpCode=" + Session["EmpCode"].ToString() + "&Year=" + DDLYear.SelectedValue.ToString() + "&MONTH=" + DDLMonth.SelectedValue.ToString());
        }
    }
}
