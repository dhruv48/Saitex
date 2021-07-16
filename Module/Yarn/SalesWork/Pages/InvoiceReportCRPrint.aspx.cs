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


public partial class Module_Yarn_SalesWork_Pages_InvoiceReportCRPrint : System.Web.UI.Page
{
    string INVOICE_TYPE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["INVOICE_NUMB"] != null)
            {
                int InvoiceNumber = 0;
                int.TryParse(Request.QueryString["INVOICE_NUMB"].ToString(), out InvoiceNumber);
                txtFrom.Text = InvoiceNumber.ToString();
                txtTo.Text = InvoiceNumber.ToString();
                BindInvoiceType();
            }
            else
            {
                GetLastInvoiceNo();
                BindInvoiceType();
            }
        }
    }

    private void GetLastInvoiceNo()
    {
       
        SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        int NewInvoiceNo = int.Parse(SaitexBL.Interface.Method.YRN_INT_MST.GetNewInvoiceNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlInvoiceType.SelectedValue , oUserLoginDetail.DT_STARTDATE.Year));
        txtFrom.Text = (NewInvoiceNo - 1).ToString();
        txtTo.Text = (NewInvoiceNo - 1).ToString();
    }

    private void BindInvoiceType()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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


    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {
            string QueryString = "";
            bool flag = false;
            if (txtFrom.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "From=" + txtFrom.Text;
                flag = true;
            }
            if (txtTo.Text != "")
            {
                if (flag)
                    QueryString = QueryString + "&";
                else
                    QueryString = QueryString + "?";

                QueryString = QueryString + "To=" + txtTo.Text;
                flag = true;
            }

            if (ddlInvoiceType.SelectedValue != "" && ddlInvoiceType.SelectedValue != null)
            {

                QueryString = QueryString + "&INVOICE_TYPE=" + ddlInvoiceType.SelectedValue.ToString().Trim();

            }
            int count = 0;
            bool bFirst = true;
            string Invoice_copy = string.Empty;
            foreach (ListItem item in chkLstInvoiceType.Items)
            {
                if (item.Selected)
                {
                    count++;
                    if (bFirst)
                    {
                        Invoice_copy += "$" + item.Text.Trim() + "$";
                        bFirst = false;
                    }
                    else
                    {
                        Invoice_copy += ", $" + item.Text.Trim() + "$";
                    }
                }
            }
            if (Invoice_copy != "" && Invoice_copy != null)
            {
                QueryString += "&PRINT_INVOICE=" + Invoice_copy;
                string URL = "../Reports/InvoiceReportCR.aspx" + QueryString;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.close();", true);
            }
            else
            {

                Common.CommonFuction.ShowMessage("Please select Invoice Report Copy type");
            }


            // QueryString += "&TRN_TYPE=" + TRN_TYPE;

            
        
        
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void txtFrom_TextChanged(object sender, EventArgs e)
    {
        txtTo.Text = txtFrom.Text.ToString();
    }
    protected void ddlInvoiceType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetLastInvoiceNo();
    }
}
