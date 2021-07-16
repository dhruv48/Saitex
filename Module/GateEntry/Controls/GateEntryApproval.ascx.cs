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
using Common;
using errorLog;

public partial class Module_GateEntry_Controls_GateEntryApproval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
     {
        if (Session["urLoginId"] != null)
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                getItemType();
                bindGateForApproval(ddlItemType.SelectedValue);
            }
        }
        else
        {
            Response.Redirect("~/Default.aspx", false);
        }

    }

    private void bindGateForApproval(string gatetype)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            var dt = SaitexBL.Interface.Method.TX_Gate_MST.GetGateEntryApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,gatetype);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("APPROVED_DATE"))
                    dt.Columns.Add("APPROVED_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("APPROVED_BY"))
                    dt.Columns.Add("APPROVED_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["APPROVED_BY"].ToString();
                    if (ConfBy == "")
                        dr["APPROVED_BY"] = oUserLoginDetail.Username;
                        dr["APPROVED_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvGateEntryApproval.DataSource = dt;
                gvGateEntryApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Gate for approval";
                gvGateEntryApproval.DataSource = null;
                gvGateEntryApproval.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void gvGateEntryApproval_Paging(object sender, GridViewPageEventArgs e)
    {
        gvGateEntryApproval.PageIndex = e.NewPageIndex;
        bindGateForApproval(ddlItemType.SelectedValue); 
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string msg = string.Empty;
            DataTable dtIndentDetail = CreateDataTable();
            int totalRows = gvGateEntryApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvGateEntryApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblGATE_NUMB = (Label)thisGridViewRow.FindControl("lblGATE_NUMB");
                    Label lblGATE_TYPE = (Label)thisGridViewRow.FindControl("lblGATE_NUMB");
                    Label lblITEM_TYPE = (Label)thisGridViewRow.FindControl("lblITEM_TYPE");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");


                    if (Approved.Checked == true)
                    {                        
                            
                            
                                
                                DateTime dConfirmDate = Convert.ToDateTime(ConfirmDate.Text.Trim());
                                string strConfirmBy = ConfirmBy.Text.Trim();
                                string strRemarks = Remarks.Text.Trim();                               
                                DataRow dr = dtIndentDetail.NewRow();
                                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dr["GATE_TYPE"] = lblGATE_TYPE.ToolTip;
                                dr["GATE_NUMB"] = int.Parse(lblGATE_NUMB.Text);
                                dr["ITEM_TYPE"] = lblITEM_TYPE.Text;
                                dr["IS_APPROVED"] = "1";
                                dr["APPROVED_DATE"] = DateTime.Parse(dConfirmDate.ToShortTimeString());
                                dr["APPROVED_BY"] = strConfirmBy;
                                dr["APPROVED_REMARK"] = strRemarks;
                                dtIndentDetail.Rows.Add(dr);
                             
                                Approved.Checked = false;
                                ConfirmDate.Text = "";
                                ConfirmBy.Text = "";
                                Remarks.Text = "";
                           
                       
                    }
                }
            }
            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }
            int iResult = SaitexBL.Interface.Method.TX_Gate_MST.Update_TransactionForApproval(oUserLoginDetail.UserCode, dtIndentDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Gate Approved Successfully');", true);
                bindGateForApproval(ddlItemType.SelectedValue);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

    }
  

    private DataTable CreateDataTable()
    {
        DataTable dtIndentDetail = new DataTable();
        dtIndentDetail.Columns.Add("COMP_CODE", typeof(string));
        dtIndentDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtIndentDetail.Columns.Add("YEAR", typeof(string));
        dtIndentDetail.Columns.Add("GATE_TYPE", typeof(string));
        dtIndentDetail.Columns.Add("GATE_NUMB", typeof(int));
        dtIndentDetail.Columns.Add("ITEM_TYPE", typeof(string));
        dtIndentDetail.Columns.Add("IS_APPROVED", typeof(string));
        dtIndentDetail.Columns.Add("APPROVED_DATE", typeof(DateTime));
        dtIndentDetail.Columns.Add("APPROVED_BY", typeof(string));
        dtIndentDetail.Columns.Add("APPROVED_REMARK", typeof(string));
        return dtIndentDetail;



       
        
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void getItemType()
    {
        try
        {

            ddlItemType.DataSource = SaitexBL.Interface.Method.TX_Gate_MST.getType("ITEM_TYPE"); ;
            ddlItemType.DataValueField = "ITEM_TYPE";
            ddlItemType.DataTextField = "ITEM_TYPE";
            ddlItemType.DataBind();
            ddlItemType.Items.Insert(0, new ListItem("---------All---------", ""));

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlItemType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGateForApproval(ddlItemType.SelectedValue);

    }
}
