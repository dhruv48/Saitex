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

public partial class Module_FA_Controls_BankMasterQueryForm : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            BindGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                grdBank.DataSource = dt;
                grdBank.DataBind();
                Session["bankreport"] = dt;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string URL = "~/Module//Reports/FA_Bank_Mst_Rpt.aspx";
            Response.Redirect("~/Module/FA/Reports/FA_Bank_Mst_Rpt.aspx", false);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/BankMasterHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping..\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
        }
    }
    protected void grdBank_PageIndexing(object sender, GridViewPageEventArgs e)
    {
        grdBank.PageIndex = e.NewPageIndex;
        BindGrid();

    }
    protected void grdBank_RowBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Visibility, "hidden");


                e.Row.Attributes["onclick"] = "__doPostBack('" + grdBank.UniqueID + "','Select$" + e.Row.RowIndex + "');";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ex.Message);
        }

    }
    protected void grdBank_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = grdBank.Rows[index];
           Label lblBankCode=(Label) row.FindControl("lblBankCode");
           String i = lblBankCode.Text;
           Response.Redirect("~/Module/FA/Queries/ChequeBookmstQueryForm.aspx?id=" + i); 
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select..\r\nSee error log for detail."));
        }
    }

    //protected void grdBank_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    //{
    //    try
    //    {
    //        ArrayList ar = grdBank.SelectedRecords;
    //        string i;
    //        Hashtable ht = (Hashtable)ar[0];
    //        i = ht["LGR_BANK_CODE"].ToString();
    //        //string URL = "../Reports/AppointmentLetterReport.aspx?ref1=" + txtRef.Text.Trim() + "&dt1=" + txtDate.Text.Trim() + "&empcode=" + EmpCode + "&gradename=" + GradeName;
    //        //string URL = "~/Module/FA/Queries/ChequeBookmstQueryForm.aspx?id = " + i ; 
    //        Response.Redirect("~/Module/FA/Queries/ChequeBookmstQueryForm.aspx?id=" + i);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Select..\r\nSee error log for detail."));
    //    }
    //}

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}