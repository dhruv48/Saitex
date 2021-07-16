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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_FA_Controls_WebUserControl : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                bindChequeDetail();
            }
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }

    private void bindChequeDetail()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUE_DTL.SelectChequeDetail();

            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("AMT_ISSUED"))
                    dt.Columns.Add("AMT_ISSUED", typeof(int));
                if (!dt.Columns.Contains("AMT_RECIEVED"))
                    dt.Columns.Add("AMT_RECIEVED", typeof(double));

                foreach (DataRow dr in dt.Rows)
                {
                    int Issued = Convert.ToInt32(dr["IS_ISSUED"]);
                    double AMOUNT = 0;

                    double.TryParse(dr["AMOUNT"].ToString(), out AMOUNT);

                    if (Issued == 1)
                    {
                        dr["AMT_ISSUED"] = AMOUNT;
                    }
                    else
                    {
                        dr["AMT_RECIEVED"] = AMOUNT;
                    }
                }

                gvBankReconc.DataSource = dt;
                gvBankReconc.DataBind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('All Cheques are Cleared');", true);
            }
        }
        catch
        {
            throw;
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bool bResult = false;
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.FA_CHEQUE_DTL oFA_CHEQUE_DTL = new SaitexDM.Common.DataModel.FA_CHEQUE_DTL();

            foreach (GridViewRow thisGridViewRow in gvBankReconc.Rows)
            {
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkCleared = (CheckBox)thisGridViewRow.FindControl("chkCleared");
                    if (chkCleared.Checked == true)
                    {
                        Label lblCompCode = (Label)thisGridViewRow.FindControl("lblCompCode");
                        Label LblBranchCode = (Label)thisGridViewRow.FindControl("LblBranchCode");
                        Label LblYear = (Label)thisGridViewRow.FindControl("LblYear");
                        Label LblVoucherNo = (Label)thisGridViewRow.FindControl("LblVoucherNo");

                        oFA_CHEQUE_DTL.COMP_CODE = lblCompCode.Text;
                        oFA_CHEQUE_DTL.BRANCH_CODE = LblBranchCode.Text;
                        oFA_CHEQUE_DTL.YEAR = Convert.ToInt32(LblYear.Text);
                        oFA_CHEQUE_DTL.VCHR_NO = LblVoucherNo.Text;
                        oFA_CHEQUE_DTL.ISCLEARED = chkCleared.Checked;
                        oFA_CHEQUE_DTL.TDATE = System.DateTime.Now; ;
                        oFA_CHEQUE_DTL.TUSER = oUserLoginDetail.UserCode;
                        oFA_CHEQUE_DTL.CLEAR_BY = oUserLoginDetail.UserCode;

                        bResult = SaitexBL.Interface.Method.FA_CHEQUE_DTL.UpdateChequeDetail(oFA_CHEQUE_DTL, out iRecordFound);
                    }
                }
            }
            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./BankReconcillation.aspx?cId=U", false);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/BankReconcillation.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data..\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }

    protected void gvBankReconc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            gvBankReconc.PageIndex = e.NewPageIndex;
            bindChequeDetail();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }
}