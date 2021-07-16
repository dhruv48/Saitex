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


public partial class Module_HRMS_Pages_EmpLoanRequest : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_MST HR_EMP_MST = new SaitexDM.Common.DataModel.HR_EMP_MST();
    string User_Code = string.Empty;
    private static string POSITION = string.Empty;
    private static DataTable DTable;
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
                Load_Loan_for_approved();
            }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Pages/Default.aspx", false);
        }
    }
    private void Load_Loan_for_approved()
    {
        try
        {
            DTable = SaitexBL.Interface.Method.HR_EMP_LOAN.Load_Loan_Detail(User_Code, POSITION ,"" , "1");
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
    protected void gvReportDisplayGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ChkAll_CheckedChanged(object sender, System.EventArgs e)
    {
        CheckBox Ctl = (CheckBox)gvReportDisplayGrid.HeaderRow.FindControl("ChkAll");
        bool chkFlag = false;
        if (Ctl.Checked)
        {
            chkFlag = true;
        }
        foreach (GridViewRow dr in gvReportDisplayGrid.Rows)
        {
            CheckBox Chk = (CheckBox)dr.FindControl("ChkSelect");
            Chk.Checked = chkFlag;
        }
    }
    private string Read_DocIDs()
    {
        string DocIDs = "";
        foreach (GridViewRow dr in gvReportDisplayGrid.Rows)
        {
            CheckBox Chk = (CheckBox)dr.FindControl("ChkSelect");
            if (Chk.Checked)
            {
                if (DocIDs.Trim() != string.Empty)
                {
                    DocIDs = DocIDs + "','" + gvReportDisplayGrid.DataKeys[dr.RowIndex].Value;
                }
                else
                {
                    DocIDs = gvReportDisplayGrid.DataKeys[dr.RowIndex].Value.ToString();
                }
            }
        }
        return DocIDs;
    }
    protected void CmdSave_Click(object sender, EventArgs e)
    {
        string lOAN_iD = Read_DocIDs();
        if (DDLStatus.SelectedValue != "0" && lOAN_iD != string.Empty)
        {
            bool Result = SaitexBL.Interface.Method.HR_EMP_LOAN.Change_Loan_Status(lOAN_iD, Convert.ToChar(DDLStatus.SelectedValue.ToString()));
            if (Result)
            {
                Common.CommonFuction.ShowMessage("Records update successfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("unable to update");
            }
            Load_Loan_for_approved();
        }
        else
        {
            Common.CommonFuction.ShowMessage("Please Select Status or Leave");
        }

    }
}
