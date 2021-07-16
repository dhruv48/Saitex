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

public partial class Module_Inventory_Controls_Vat_Retail_Price : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST;
    private static string MaxTrnNum = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                Clear_Record();
                BindInvoiceType();
                BindLastInvoiceNo();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void Clear_Record()
    {
        try
        {
            TxtInvoiceTo.Text = string.Empty;
            TxtInvoiceFrom.Text = string.Empty;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }

    private void BindInvoiceType()
    {
        try
        {
            chkLstInvoiceType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("SW_PLANT_INV_CP", oUserLoginDetail.COMP_CODE);
            chkLstInvoiceType.DataSource = dt;
            chkLstInvoiceType.DataTextField = "MST_CODE";
            chkLstInvoiceType.DataValueField = "MST_CODE";
            chkLstInvoiceType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void BindLastInvoiceNo()
    {
        try
        {
            oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.TRN_TYPE = "IYS28";
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            MaxTrnNum = SaitexBL.Interface.Method.YRN_IR_MST.GetMaxTRNNumber(oYRN_IR_MST);
            TxtInvoiceFrom.Text = MaxTrnNum;
            TxtInvoiceTo.Text = MaxTrnNum;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Record();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string SearchQuery = string.Empty;
        int count = 0;
        try
        {
            if (!ValidationForInvoiceType())
            {
                bool bFirst = true;
                string Invoice_copy = string.Empty;
                foreach (ListItem item in chkLstInvoiceType.Items)
                {
                    if (item.Selected)
                    {
                        count++;
                        if (bFirst)
                        {
                            Invoice_copy += "'" + item.Text.Trim() + "'";
                            bFirst = false;
                        }
                        else
                        {
                            Invoice_copy += ", '" + item.Text.Trim() + "'";
                        }
                    }
                }

                SearchQuery = " WHERE LTRIM(RTRIM(BRANCH_CODE))='" + oUserLoginDetail.CH_BRANCHCODE + "' AND LTRIM(RTRIM(COMP_CODE))='" + oUserLoginDetail.COMP_CODE + "' AND LTRIM(RTRIM(PRINT_INVOICE)) IN (" + Invoice_copy + ")";

                if (TxtInvoiceFrom.Text != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND INVOICE_NO between '" + TxtInvoiceFrom.Text.Trim().ToString() + "'";
                }

                if (TxtInvoiceTo.Text != string.Empty)
                {
                    SearchQuery = SearchQuery + " AND '" + TxtInvoiceTo.Text.Trim().ToString() + "'";
                }

                SearchQuery = SearchQuery.Replace("'", "$");
                string URL = "../Reports/Vat_Retail_Price.aspx?Search=" + SearchQuery;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
            }
            else
            {
                CommonFuction.ShowMessage("Please Select Atleast One Invoice Copy Type..");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));

        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);
            imgbtnClear.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to clear this record')");
            imgbtnExit.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to exit this record')");
            imgbtnPrint.Attributes.Add("OnClick", "javascript:return window.confirm('Are you sure you want to print this record')");
        }
        catch
        {
            throw;
        }
    }

    public bool ValidationForInvoiceType()
    {
        try
        {
            bool result = false;
            int count = 0;
            foreach (ListItem item in chkLstInvoiceType.Items)
            {
                if (item.Selected)
                {
                    count++;
                }

            }
            if (count > 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }
            return result;
        }
        catch
        {
            throw;
        }
    }
}
