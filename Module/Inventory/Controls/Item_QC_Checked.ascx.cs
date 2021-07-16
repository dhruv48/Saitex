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

public partial class Module_Inventory_Controls_Item_QC_Checked : System.Web.UI.UserControl
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
                    bindMaterialReceiptApproval();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
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

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_Checking.aspx?NQC=true");
    }

    private void bindMaterialReceiptApproval()
    {
        try
        {
            lblMode.Text = "Find";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetReceiptDataForQC_Checked(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No MRN for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No MRN for approval");
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
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }



    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblQC_Result = (Label)e.Row.FindControl("lblQC_Result");
            Label lblQC_Approved_Result = (Label)e.Row.FindControl("lblQC_Approved_Result");
            if (lblQC_Result.Text.ToLower() == "pass")
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                e.Row.Cells[14].BackColor = System.Drawing.Color.Red;
            }

            if (lblQC_Approved_Result.Text.ToLower() == "pass")
            {
                e.Row.Cells[17].BackColor = System.Drawing.Color.Green;
            }
            else if (lblQC_Approved_Result.Text.ToLower() == "fail")
            {
                e.Row.Cells[17].BackColor = System.Drawing.Color.Red;
            }
        }
    }

    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_CheckingList.aspx");
    }



   

   

}