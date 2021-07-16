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


public partial class Module_HRMS_Pages_LoanDetail : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    string User_Code = string.Empty;
    private static string POSITION = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            HR_EMP_MST.EMP_CODE = Session["EmpCode"].ToString();
            HR_EMP_MST.POSITION = Session["POSITION"].ToString();
            User_Code = HR_EMP_MST.EMP_CODE;
            POSITION = HR_EMP_MST.POSITION;
            if (!Page.IsPostBack)
            {
                Load_lOAN_Detail();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
        }
    }
    private void Load_lOAN_Detail()
    {
        try
        {
           DataTable  DTable = SaitexBL.Interface.Method.HR_EMP_LOAN.Load_Loan_Detail(User_Code, POSITION ,"", "2");
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
    }  

}
