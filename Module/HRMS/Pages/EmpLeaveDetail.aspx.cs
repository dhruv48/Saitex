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


public partial class Module_HRMS_Pages_EmpLeaveDetail : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    string User_Code = string.Empty;
    private static string POSITION = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        string str = Session["EmpCode"].ToString();
        if (Session["EmpCode"] != null)
        {
            HR_EMP_MST.EMP_CODE = Session["EmpCode"].ToString();
            HR_EMP_MST.POSITION = Session["POSITION"].ToString();
            User_Code = HR_EMP_MST.EMP_CODE;
            POSITION = HR_EMP_MST.POSITION;
            if (!Page.IsPostBack)
            {
                Load_Leave();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
        }
    }
    private void Load_Leave()
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_LV_APP.Load_HR_LV_APP_Form_Record(User_Code, POSITION, "2");
            gvReportDisplayGrid.DataSource = DTable;
            gvReportDisplayGrid.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }

    }
    protected void gvReportDisplayGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportDisplayGrid.PageIndex = e.NewPageIndex;
        Load_Leave();
    }
    protected void gvReportDisplayGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

}
