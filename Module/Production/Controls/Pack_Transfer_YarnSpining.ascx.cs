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
using Common;
using errorLog;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

public partial class Module_Production_Controls_Pack_Transfer_YarnSpining : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_YARN_PACK_MST obj;
    private static string PRODUCT_TYPE = string.Empty;
    private static GridViewRow row;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                PRODUCT_TYPE = "YARN SPINING";
                bindCustomerRequestApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request Confirm.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //    string URL = "PrintItemPO.aspx";
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Production/Pages/Pack_transfer.aspx", false);
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
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

    private void bindCustomerRequestApproval()
    {
        try
        {
            List<SaitexDM.Common.DataModel.TX_YARN_PACK_MST> dt = SaitexBL.Interface.Method.TX_YARN_PACK_MST.GetPackDTLForTrans1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, PRODUCT_TYPE);
            if (dt.Count > 0)
            {
                lblTotalRecord.Text = dt.Count.ToString();
                grdFGTrans.DataSource = dt;
                grdFGTrans.DataBind();
            }
            else
            {
                lblTotalRecord.Text = "No packing data for Transfer";
                grdFGTrans.DataSource = null;
                grdFGTrans.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No packing data for Transfer");
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

    protected void grdFGTrans_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grdFGTrans_PreRender(object sender, EventArgs e)
    {
        grdFGTrans.UseAccessibleHeader = true;
        grdFGTrans.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void grdFGTrans_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdFGTrans.PageIndex = e.NewPageIndex;
        bindCustomerRequestApproval();
    }

    protected void chkTransfer_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)(sender));
        row = ((GridViewRow)(chk.NamingContainer));
        Label lblPI = (Label)row.FindControl("lblPI_NO");
        Label lblYarnCode = (Label)row.FindControl("lblYarn");

        row.Visible = false;

        obj = new SaitexDM.Common.DataModel.TX_YARN_PACK_MST();
        obj.COMP_CODE = oUserLoginDetail.COMP_CODE;
        obj.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        obj.PACKING_ID = int.Parse(chk.Text);
        obj.TRANSFER_DATE = DateTime.Now.Date;
        obj.TUSER = oUserLoginDetail.UserCode;
        obj.TDATE = DateTime.Now.Date;
        obj.PI_NO = lblPI.Text;
        obj.YARN_CODE = lblYarnCode.ToolTip;

        bool bresult = SaitexBL.Interface.Method.TX_YARN_PACK_MST.UpdatePackDtlTranToFG(obj);
        if (bresult)
        {
            row.Visible = false;
            lblTotalRecord.Text = (int.Parse(lblTotalRecord.Text) - 1).ToString();
            obj = null;
            row = null;
        }
        else
        {
            row.Visible = true;
        }
    }
}
