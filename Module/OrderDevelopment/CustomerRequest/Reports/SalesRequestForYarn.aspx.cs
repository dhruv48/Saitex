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
using System.Data.OracleClient;
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;
public partial class Module_OrderDevelopment_CustomerRequest_Reports_SalesRequestForYarn : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string yr = string.Empty;
    string branch = string.Empty;
    string url = string.Empty;
    private  DateTime Sdate;
    private  DateTime Edate;
    string sdate1 = string.Empty;
    string edate1 = string.Empty;
    string PRODUCT_TYPE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        try
        {
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            if (Request.QueryString["PRODUCT_TYPE"] != null)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            }
            if (!IsPostBack)
            {
                InitialControls();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page Loading"));
        }
    }

    private void InitialControls()
    {

        

        getBranchName();
        ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
        bindyear();
        ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
        bindFromToDate();
        bindBusinessType();

        GETMAX_INVOICE();
        BindInvoiceType();
    }

    private void bindyear()
    {
        try
        {

            string branch = ddlBranch.SelectedValue.ToString();

            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);

            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();

                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();

                ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));

                dt.Dispose();
                dt = null;

            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();

                CommonFuction.ShowMessage(brnch + " No have financial year & data .");

                getBranchName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;

                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));

            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    private void bindFromToDate()
    {
        try
        {
            string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtDate1.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                        txtDate2.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    private void bindBusinessType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("BUSINESS_TYPE", oUserLoginDetail.COMP_CODE);
            ddlBusinessType.Items.Clear();
            ddlBusinessType.DataSource = dt;
            ddlBusinessType.DataValueField = "MST_CODE";
            ddlBusinessType.DataTextField = "MST_DESC";
            ddlBusinessType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBusinessType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GETMAX_INVOICE();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bisiness Type Selection.\r\nSee error log for detail."));
           // lblMode.Text = ex.ToString();
        }
    }


    private void getBranchName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();

            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();

            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);

            DataView Dv = new DataView(dt);

            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();

            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }

    protected void txtDate1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(txtDate1.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate1.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Start Date TextChanged.\r\nSee error log for detail."));
        }
    }

    protected void txtDate2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty && txtDate2.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(txtDate2.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate2.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in End Date TextChanged.\r\nSee error log for detail."));
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        int count = 0;
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
                        Invoice_copy += "$" + item.Text.Trim() + "$";
                        bFirst = false;
                    }
                    else
                    {
                        Invoice_copy += ", $" + item.Text.Trim() + "$";
                    }
                }
            }
            string StDate = string.Empty;
            string EnDate = string.Empty;
            string YEAR = string.Empty;

            try
            {

                var myDataTable = new DataTable();
                myDataTable.Columns.Add("BRANCH_CODE", typeof(string));
                myDataTable.Columns.Add("StDate", typeof(string));
                myDataTable.Columns.Add("EnDate", typeof(string));
                myDataTable.Columns.Add("YEAR", typeof(string));
                myDataTable.Columns.Add("ORDER_NO", typeof(string));
                myDataTable.Columns.Add("ORDER_FROM", typeof(string));
                myDataTable.Columns.Add("PRINT_INVOICE", typeof(string));
                myDataTable.Columns.Add("PRODUCT_TYPE", typeof(string));
                
                DataRow row;
                row = myDataTable.NewRow();
                if (ddlBranch.SelectedValue.ToString() != null && ddlBranch.SelectedValue.ToString() != string.Empty)
                {
                    row["BRANCH_CODE"] = ddlBranch.SelectedItem.Text.ToString();
                }
                else
                {
                    row["BRANCH_CODE"] = string.Empty;
                }
                if (txtDate1.Text.Trim() != string.Empty && txtDate2.Text.Trim().ToString() != string.Empty)
                {
                    row["StDate"] = DateTime.Parse(txtDate1.Text.Trim().ToString());
                    row["EnDate"] = DateTime.Parse(txtDate2.Text.Trim().ToString());
                }
                else
                {
                    row["StDate"] = Sdate;
                    row["EnDate"] = Edate;
                }
                if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
                {
                    row["YEAR"] = ddlYear.SelectedItem.Text.ToString();
                }
                else
                {
                    row["YEAR"] = string.Empty;
                }
                if (!string.IsNullOrEmpty(txtInvoiceTo.Text) && !string.IsNullOrEmpty(txtInvoiceFrom.Text))
                {
                    row["ORDER_NO"] = txtInvoiceTo.Text;
                    row["ORDER_FROM"] = txtInvoiceFrom.Text;
                }
                else
                {
                    row["ORDER_NO"] = string.Empty;
                    row["ORDER_FROM"] = string.Empty;
                }
                if (!string.IsNullOrEmpty(Invoice_copy))
                {
                    row["PRINT_INVOICE"] = Invoice_copy;

                }
                else
                {
                    row["PRINT_INVOICE"] = string.Empty;

                }

                if (Request.QueryString["PRODUCT_TYPE"] != null)
                {
                    row["PRODUCT_TYPE"] = Request.QueryString["PRODUCT_TYPE"].ToString();
                }
                else
                {
                    row["PRODUCT_TYPE"] = string.Empty;
                }
                

                  string  REPORT_TYPE = ddlReportType.SelectedValue.ToString();
                
                
                myDataTable.Rows.Add(row);
                Session["SALE_CONTRACT"] = myDataTable;
                string URL = "SalesReport.aspx?REPORT_TYPE="+REPORT_TYPE;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            }

            catch (Exception ex)
            {
                CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
            }

        }
        else
        {
            CommonFuction.ShowMessage("Please Select Atleast One Invoice Copy Type..");
        }


    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControls();

            Response.Redirect("SalesReport.aspx", false);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        int count = 0;
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
                        Invoice_copy += "$" + item.Text.Trim() + "$";
                        bFirst = false;
                    }
                    else
                    {
                        Invoice_copy += ", $" + item.Text.Trim() + "$";
                    }
                }
            }
            string StDate = string.Empty;
            string EnDate = string.Empty;
            string YEAR = string.Empty;

            try
            {

                var myDataTable = new DataTable();
                myDataTable.Columns.Add("BRANCH_CODE", typeof(string));
                myDataTable.Columns.Add("StDate", typeof(string));
                myDataTable.Columns.Add("EnDate", typeof(string));
                myDataTable.Columns.Add("YEAR", typeof(string));
                myDataTable.Columns.Add("ORDER_NO", typeof(string));
                myDataTable.Columns.Add("ORDER_FROM", typeof(string));
                myDataTable.Columns.Add("PRINT_INVOICE", typeof(string));
                myDataTable.Columns.Add("PRODUCT_TYPE", typeof(string));

                DataRow row;
                row = myDataTable.NewRow();
                if (txtDate1.Text.Trim() != string.Empty && txtDate2.Text.Trim().ToString() != string.Empty)
                {
                    row["StDate"] = DateTime.Parse(txtDate1.Text.Trim().ToString());
                    row["EnDate"] = DateTime.Parse(txtDate2.Text.Trim().ToString());
                }
                else
                {
                    row["StDate"] = Sdate;
                    row["EnDate"] = Edate;
                }
                if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
                {
                    row["YEAR"] = ddlYear.SelectedItem.Text.ToString();
                }
                else
                {
                    row["YEAR"] = string.Empty;
                }
                if (!string.IsNullOrEmpty(txtInvoiceTo.Text) && !string.IsNullOrEmpty(txtInvoiceFrom.Text))
                {
                    row["ORDER_NO"] = txtInvoiceTo.Text;
                    row["ORDER_FROM"] = txtInvoiceFrom.Text;
                }
                else
                {
                    row["ORDER_NO"] = string.Empty;
                    row["ORDER_FROM"] = string.Empty;
                }
                if (!string.IsNullOrEmpty(Invoice_copy))
                {
                    row["PRINT_INVOICE"] = Invoice_copy;

                }
                else
                {
                    row["PRINT_INVOICE"] = string.Empty;

                }

                if (Request.QueryString["PRODUCT_TYPE"] != null)
                {
                    row["PRODUCT_TYPE"] = Request.QueryString["PRODUCT_TYPE"].ToString();
                }
                else
                {
                    row["PRODUCT_TYPE"] = string.Empty;
                }
                myDataTable.Rows.Add(row);
                Session["SALE_CONTRACT"] = myDataTable;
                string URL = "SalesReport.aspx";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
            }

            catch (Exception ex)
            {
                CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
            }

        }
        else
        {
            CommonFuction.ShowMessage("Please Select Atleast One Invoice Copy Type..");
        }


        ///////////////////////////////////
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindyear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString())); ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            GETMAX_INVOICE();

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
            GETMAX_INVOICE();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Year Selection.\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    public void GETMAX_INVOICE()
    {
        var MAX_INVOICE_DT = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GET_MAX_ORDER_NO1(ddlBranch.SelectedValue.ToString(), ddlYear.SelectedItem.ToString(), PRODUCT_TYPE ,ddlBusinessType.SelectedValue.Trim());
        if (MAX_INVOICE_DT.Rows[0]["ORDER_NO"].ToString()!="")
        {
            var MAX_INVOICE = MAX_INVOICE_DT.Rows[0]["ORDER_NO"].ToString();
            txtInvoiceTo.Text = MAX_INVOICE;
            txtInvoiceFrom.Text = MAX_INVOICE;
        }
        else 
        {
            txtInvoiceTo.Text = "";
            txtInvoiceFrom.Text = "";
            Common.CommonFuction.ShowMessage("Records Not Found");
            
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

