using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DBLibrary;
using Common;
using errorLog;

public partial class Module_Inventory_Controls_Item_QC_Checking_Approval : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!IsPostBack)
                {
                    lblMode.Text = "Find";
                    bindMaterialReceiptApproval();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_YEAR", typeof(double));
        dtReceiptDetail.Columns.Add("YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("TRN_NUMB", typeof(double));
        dtReceiptDetail.Columns.Add("QC_NUMB", typeof(int));
        dtReceiptDetail.Columns.Add("ITEM_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("QC_CONF_FLAG", typeof(string));
        dtReceiptDetail.Columns.Add("QC_CONF_BY", typeof(string));
        dtReceiptDetail.Columns.Add("QC_CONF_RESULT", typeof(string));
        dtReceiptDetail.Columns.Add("QC_CONF_REMARKS", typeof(string));
        dtReceiptDetail.Columns.Add("TUSER", typeof(string));
        return dtReceiptDetail;
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {


            DataTable dtReceiptDetail = CreateDataTable();
            int totalRows = gvMaterialReceiptApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialReceiptApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                   
                    Label lblTRN_TYPE = (Label)thisGridViewRow.FindControl("lblTRN_TYPE");
                    Label lblITEM_CODE = (Label)thisGridViewRow.FindControl("lblITEM_CODE");
                    Label lblTRN_NUMB = (Label)thisGridViewRow.FindControl("lblTRN_NUMB");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    RadioButton chkQCPass = (RadioButton)thisGridViewRow.FindControl("chkQCPass");
                    RadioButton chkQCFail = (RadioButton)thisGridViewRow.FindControl("chkQCFail");
                    TextBox txtRemarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");

                    Label lblQC_NUMB = (Label)thisGridViewRow.FindControl("lblQC_NUMB");
                    Label lblTRN_YEAR = (Label)thisGridViewRow.FindControl("lblTRN_YEAR");
                    Label lblQC_Year = (Label)thisGridViewRow.FindControl("lblQC_Year");

                    Label lblQC_Result = (Label)thisGridViewRow.FindControl("lblQC_Result");

                    int QC_NUMB = 0, QC_Year = 0, TRN_YEAR = 0;
                    double TRN_NUMB = 0;
                    double.TryParse(lblTRN_NUMB.Text, out TRN_NUMB);
                    int.TryParse(lblQC_NUMB.Text, out QC_NUMB);
                    int.TryParse(lblTRN_YEAR.Text, out TRN_YEAR);
                    int.TryParse(lblQC_Year.Text, out QC_Year);
                    string QC_CHANGE_RESULT = "";
                    if (chkQCPass.Checked)
                    {
                        QC_CHANGE_RESULT = "1";
                    }
                    else if (chkQCFail.Checked)
                    {
                        QC_CHANGE_RESULT = "0";
                    }
                    else if (chkQCFail.Checked == false && chkQCPass.Checked == false)
                    {
                        if (lblQC_Result.Text.ToLower() == "pass")
                        {
                            QC_CHANGE_RESULT = "1";
                        }
                        else if (lblQC_Result.Text.ToLower() == "fail")
                        {
                            QC_CHANGE_RESULT = "0";
                        }
                       
                    }

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtReceiptDetail.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["TRN_NUMB"] = TRN_NUMB;
                        dr["TRN_TYPE"] = lblTRN_TYPE.Text.Trim();
                        dr["YEAR"] = QC_Year;
                        dr["QC_NUMB"] = QC_NUMB;
                        dr["TRN_YEAR"] = TRN_YEAR;
                      
                        dr["ITEM_CODE"] = lblITEM_CODE.Text.Trim();
                        dr["QC_CONF_FLAG"] = "1";
                        dr["QC_CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["QC_CONF_RESULT"] = QC_CHANGE_RESULT;
                        dr["QC_CONF_REMARKS"] = txtRemarks.Text;
                        dr["TUSER"] = oUserLoginDetail.UserCode;
                        dtReceiptDetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }

                }
            }

            if (dtReceiptDetail != null && dtReceiptDetail.Rows.Count > 0)
            {
                bool iResult = SaitexBL.Interface.Method.TX_ITEM_IR_MST.Update_ReceiptForQCApproval(dtReceiptDetail);
                if (iResult)
                {
                    lblMode.Text = "Find";
                    CommonFuction.ShowMessage("QC Approved Successfully.");
                    bindMaterialReceiptApproval();
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please confirm atleast one MRN for QC Approval !!!");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in QC Checking .\r\nSee error log for detail."));
        }
    }


    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Inventory/Reports/Item_QC_CheckingReport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        bindMaterialReceiptApproval();
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
    private void bindMaterialReceiptApproval()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetReceiptDataForQCApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No MRN for QC approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No MRN for QC approval");
            }
        }
        catch
        {
            throw;
        }
    }


    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQC_Result = (Label)e.Row.FindControl("lblQC_Result");

            if (lblQC_Result.Text.ToLower() == "pass")
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
            }
           
        }
    }
   
}