using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_WorkOrder_Pages_Work_order_Entry_report : System.Web.UI.Page
{
    //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    //SaitexDM.Common.DataModel.OD_WO_MST oOD_WO_MST;
    private static string WOTYPE = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Session["LoginDetail"] != null)
            {
                if (!IsPostBack)
                {
                    if (Request.QueryString["Type"] != null)
                    {
                        WOTYPE = Request.QueryString["Type"].ToString();
                    }
                    setfromTo();
                }
            }
        }
        catch
        {

        }
    }

    public void setfromTo()
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        SaitexDM.Common.DataModel.OD_WO_MST oAPP_JOB_WORK_MST = new SaitexDM.Common.DataModel.OD_WO_MST();
        oAPP_JOB_WORK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;

        oAPP_JOB_WORK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

        ddlWOType.SelectedIndex = ddlWOType.Items.IndexOf(ddlWOType.Items.FindByValue(WOTYPE));
        oAPP_JOB_WORK_MST.WO_TYPE = ddlWOType.SelectedValue;
        oAPP_JOB_WORK_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        string WO_NUMB = (int.Parse(SaitexBL.Interface.Method.OD_WO_MST.GetNewWONoReport(oAPP_JOB_WORK_MST))).ToString();
        txtFrom.Text = WO_NUMB;
        txtTo.Text = WO_NUMB;
    
    }
    protected void ddlWOType_SelectedIndexChanged(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        SaitexDM.Common.DataModel.OD_WO_MST oAPP_JOB_WORK_MST = new SaitexDM.Common.DataModel.OD_WO_MST();
        oAPP_JOB_WORK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;

        oAPP_JOB_WORK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oAPP_JOB_WORK_MST.WO_TYPE = ddlWOType.SelectedValue;
        string WO_NUMB = (int.Parse(SaitexBL.Interface.Method.OD_WO_MST.GetNewWONoReport(oAPP_JOB_WORK_MST))).ToString();
        txtFrom.Text = WO_NUMB;
        txtTo.Text = WO_NUMB;
     
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        int count = 0;
        try
        {
            if (Page.IsValid)
            {

                string msg = "";
                int From = 0;
                int To = 0;
                if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtFrom.Text.Trim()), out From))
                    msg += "Invalid Starting Number.</ br>";
                if (!int.TryParse(Common.CommonFuction.funFixQuotes(txtTo.Text.Trim()), out To))
                    msg += "Invalid Ending Number.</ br>";



                if (msg == "")
                {
                    string QueryString = "";
                    QueryString += "?FromNo=" + From;
                    QueryString += "&ToNo=" + To;
                    QueryString += "&WO_TYPE=" + ddlWOType.SelectedValue;

                    string URL = "../../../Module/WorkOrder/Reports/job_EntryRpt.aspx" + QueryString;
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
}

