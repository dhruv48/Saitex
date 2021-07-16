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

public partial class Module_Production_Controls_Dyeing_Prod_approval : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindMaterialIndentApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail"));
        }
    }

    private void BlanksConrols()
    {
        try
        {

            int totalRows = gvMaterialIndentApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialIndentApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label IndentId = (Label)thisGridViewRow.FindControl("lblIndentTrnId");
                    TextBox ApprovedQty = (TextBox)thisGridViewRow.FindControl("txtApprovedQty");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    ApprovedQty.Text = "";
                    Approved.Checked = false;
                    ConfirmDate.Text = "";
                    ConfirmBy.Text = "";
                    Remarks.Text = "";
                }

            }

        }



        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }

    private DataTable CreateDataTable()
    {
        DataTable dtDyedDetail = new DataTable();
        dtDyedDetail.Columns.Add("YEAR", typeof(int));
        dtDyedDetail.Columns.Add("COMP_CODE", typeof(string));
        dtDyedDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtDyedDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtDyedDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtDyedDetail.Columns.Add("ARTICAL_CODE", typeof(string));
        dtDyedDetail.Columns.Add("SHADE_CODE", typeof(string));
        dtDyedDetail.Columns.Add("TRN_QTY", typeof(double));
        dtDyedDetail.Columns.Add("CONF_FLAG", typeof(string));
        dtDyedDetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtDyedDetail.Columns.Add("CONF_BY", typeof(string));
        dtDyedDetail.Columns.Add("REMARK", typeof(string));
        return dtDyedDetail;
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtDyedDetail = CreateDataTable();
            int totalRows = gvMaterialIndentApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialIndentApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblInd_NUMB = (Label)thisGridViewRow.FindControl("lblInd_NUMB");
                    Label lblItemCode = (Label)thisGridViewRow.FindControl("lblItemCode");
                    Label lblShade = (Label)thisGridViewRow.FindControl("lblShade");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    TextBox Remarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    if (Approved.Checked == true)
                    {
                            string strRemarks = Remarks.Text.Trim();
                            DataRow dr = dtDyedDetail.NewRow();
                            dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                            dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                            dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                            dr["TRN_TYPE"] = lblItemCode.ToolTip.Trim();
                            dr["TRN_NUMB"] = int.Parse(lblInd_NUMB.Text.Trim());
                            dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                            dr["CONF_BY"] = oUserLoginDetail.UserCode;
                            dr["CONF_FLAG"] = "1";
                            dr["REMARK"] = strRemarks;
                            dtDyedDetail.Rows.Add(dr);
                          
                            Approved.Checked = false;
                          
                            Remarks.Text = "";
                        }
                }
            }
            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }
            int iResult = SaitexBL.Interface.Method.YRN_PROD_MST.Update_TransactionForApproval(oUserLoginDetail.UserCode, dtDyedDetail);
            if (iResult > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('Dyed Prod Approved Successfully');", true);

                lblMode.Text = "Find";
                bindMaterialIndentApproval();
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("../Reports/YarnPrintIndent1.aspx");
        //string URL = "../Reports/YarnPrintIndent1.aspx";
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=300');", true);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/yarn/SalesWork/Pages/Yarn_Indent_Approval.aspx", false);
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void bindMaterialIndentApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.YRN_PROD_MST.GetDyedProdDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                gvMaterialIndentApproval.DataSource = dt;
                gvMaterialIndentApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Dyeing Prod for approval";
                gvMaterialIndentApproval.DataSource = null;
                gvMaterialIndentApproval.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        }
    }

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        //imgbtnUpdate.Attributes.Add("Onclick", "return confirm('Are you sure to update the record?');");
        //imgbtnDelete.Attributes.Add("Onclick", "return confirm('Are you sure to delete the record ?');");
        //imgbtnPrint.Attributes.Add("Onclick", "return confirm('Are you sure to print the record?');");
        //imgbtnClear.Attributes.Add("Onclick", "return confirm('Are you sure to clear the record ?');");
        //imgbtnExit.Attributes.Add("Onclick", "return confirm('Are you sure to exit the record?');");
        //imgbtnHelp.Attributes.Add("Onclick", "return confirm('Are you sure to see help?');");      

    }
}
