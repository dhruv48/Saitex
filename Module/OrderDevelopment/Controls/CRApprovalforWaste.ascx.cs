using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Common;
using errorLog;
using System.IO;
using System.Globalization;
using WCFMain;
using System.Data;

public partial class Module_OrderDevelopment_Controls_CRApprovalforWaste : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string PRODUCT_TYPE = string.Empty;
    private static double NoOfUnit = 0f;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                PRODUCT_TYPE = "WASTE";
                bindCustomerRequestApproval();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    public void bindCustomerRequestApproval()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetUnApprovedCustomerRequestSewingOnly(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_BY"))
                {
                    dt.Columns.Add("CONF_BY", typeof(string));
                }
                if (!dt.Columns.Contains("CONF_DATE"))
                {
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                }


                foreach (DataRow dr in dt.Rows)
                {
                    string CONF_BY = dr["CONF_BY"].ToString();
                    if (CONF_BY == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();



                }
                gvCustomerApproval.DataSource = dt;
                gvCustomerApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Customer Request for approval";
                gvCustomerApproval.DataSource = null;
                gvCustomerApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Customer Request for approval");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void lblNoOfUnit_TextChanged(object sender, EventArgs e)
    {
        TextBox tb1 = ((TextBox)(sender));
        GridViewRow gv1 = ((GridViewRow)(tb1.NamingContainer));
        Label lblWeighofUnit = gv1.FindControl("lblWeighofUnit") as Label;
        TextBox lblNoOfUnit = gv1.FindControl("lblNoOfUnit") as TextBox;
        Label lblQuantity = gv1.FindControl("lblQuantity") as Label;
        Label lblRequestedNoOfUnit = gv1.FindControl("lblRequestedNoOfUnit") as Label;

        double RequestedNoUnit = 0;
        Double.TryParse(lblRequestedNoOfUnit.Text, out RequestedNoUnit);
        // NoOfUnit = 0;
        double.TryParse(lblNoOfUnit.Text, out NoOfUnit);
        if (RequestedNoUnit >= NoOfUnit)
        {
            double WeighofUnit = 0f;
            double.TryParse(lblWeighofUnit.Text, out   WeighofUnit);
            double Quantity = 0f;
            double.TryParse(lblQuantity.Text, out   Quantity);
            lblQuantity.Text = (WeighofUnit * NoOfUnit).ToString();

        }
        else
        {
            Common.CommonFuction.ShowMessage("Dear !! Qty Cannot Be Greater Than Requested Qty");
            lblNoOfUnit.Text = lblRequestedNoOfUnit.Text;
            double WeighofUnit = 0f;
            double.TryParse(lblWeighofUnit.Text, out   WeighofUnit);
            double NoOfUnit1 = 0;
            double.TryParse(lblNoOfUnit.Text, out NoOfUnit1);
            lblQuantity.Text = (WeighofUnit * NoOfUnit1).ToString();
        }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../../../../Module/OrderDevelopment/CustomerRequest/Reports/CustomerRequestapprovalReport.aspx";
        URL += "?PRODUCT_TYPE=WASTE";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
}

    protected void chkApproved_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chk = ((CheckBox)sender);
        if (chk.Checked)
        {
            GridViewRow gv1 = ((GridViewRow)chk.NamingContainer);
            DataTable dtPODetail = CreateDataTable();
            CheckBox chkconfirm = gv1.FindControl("chkApproved") as CheckBox;
            CheckBox chkclosed = gv1.FindControl("ChkClosed") as CheckBox;
            custapp ocustapp = new custapp();
            Label lblCompCode = (Label)gv1.FindControl("lblCompCode");
            Label lblBRANCH_CODE = (Label)gv1.FindControl("lblBRANCH_CODE");
            Label lblYEAR = (Label)gv1.FindControl("lblYEAR");
            Label lblORDER_TYPE = (Label)gv1.FindControl("lblORDER_TYPE");
            Label lblORDER_CAT = (Label)gv1.FindControl("lblORDER_CAT");
            Label lblPRODUCT_TYPE = (Label)gv1.FindControl("lblPRODUCT_TYPE");
            Label lblBUSINESS_TYPE = (Label)gv1.FindControl("lblBUSINESS_TYPE");
            Label lblORDER_NO = (Label)gv1.FindControl("lblORDER_NO");
            Label lblARTICLE_NO = (Label)gv1.FindControl("lblARTICLE_NO");
            Label lblSUBSTRATE = (Label)gv1.FindControl("lblSUBSTRATE");
            Label lblCOUNT = (Label)gv1.FindControl("lblCOUNT");
            Label lblSHADE_FAMILY_CODE = (Label)gv1.FindControl("lblSHADE_FAMILY_CODE");
            Label lblSHADE_CODE = (Label)gv1.FindControl("lblSHADE_CODE");
            TextBox lblNoOfUnit = (TextBox)gv1.FindControl("lblNoOfUnit");
            Label lblWeighofUnit = (Label)gv1.FindControl("lblWeighofUnit");
            Label lblQuantity = (Label)gv1.FindControl("lblQuantity");
            TextBox txtConfirmDate = (TextBox)gv1.FindControl("txtConfirmDate");
            TextBox txtConfirmBy = (TextBox)gv1.FindControl("txtConfirmBy");
            TextBox lblTRANS_PRICE = (TextBox)gv1.FindControl("lblTRANS_PRICE");
            TextBox lblSALE_PRICE = (TextBox)gv1.FindControl("lblSALE_PRICE");
            double NooUnit = 0;
            double.TryParse(((TextBox)gv1.FindControl("lblNoOfUnit")).Text, out NooUnit);     
            if (CheckTranSalepriceforZero(lblTRANS_PRICE.Text, lblSALE_PRICE.Text))
            {
                if ((NooUnit == 0) || lblNoOfUnit.Text == string.Empty)
                {
                    Common.CommonFuction.ShowMessage("App Cr. Unit Should not be Null or greater then Qty.!!");
                    chk.Checked = false;

                }
                else
                {

                    DataRow dr = dtPODetail.NewRow();
                    dr["YEAR"] = lblYEAR.Text;
                    dr["COMP_CODE"] = lblCompCode.Text;
                    dr["BRANCH_CODE"] = lblBRANCH_CODE.Text;
                    dr["ORDER_TYPE"] = lblORDER_TYPE.Text;
                    dr["ORDER_CAT"] = lblORDER_CAT.Text;
                    dr["BUSINESS_TYPE"] = lblBUSINESS_TYPE.Text;
                    dr["PRODUCT_TYPE"] = lblPRODUCT_TYPE.Text;
                    dr["ORDER_NO"] = lblORDER_NO.Text;
                    dr["ARTICLE_NO"] = lblARTICLE_NO.Text;
                    dr["SUBSTRATE"] = lblSUBSTRATE.Text;
                    dr["COUNT"] = lblCOUNT.Text;
                    dr["SHADE_FAMILY_CODE"] = lblSHADE_FAMILY_CODE.Text;
                    dr["SHADE_CODE"] = lblSHADE_CODE.Text;
                    double weightofunit = 0;
                    double noofunit = 0;
                    double quanity = 0;
                    double.TryParse(lblWeighofUnit.Text, out weightofunit);
                    double.TryParse(lblNoOfUnit.Text, out noofunit);
                    double.TryParse(lblQuantity.Text, out quanity);



                    dr["APP_WEIGHT_OF_UNIT"] = weightofunit;
                    dr["APP_NO_OF_UNIT"] = noofunit;
                    dr["QTY_APPROVED"] = quanity;

                    dr["CONF_BY"] = oUserLoginDetail.UserCode;
                    dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                    dr["CONF_FLAG"] = "1";
                    dr["TDATE"] = DateTime.Now.Date.ToString();
                    dr["TUSER"] = oUserLoginDetail.UserCode;
                    dr["REMARKS"] = string.Empty;
                    double Tranprice;
                    double.TryParse(lblTRANS_PRICE.Text, out Tranprice);
                    dr["TRANS_PRICE"] = Tranprice;
                    double SaleRate;
                    double.TryParse(lblSALE_PRICE.Text, out SaleRate);
                    dr["SALE_RATE"] = SaleRate;
                    dtPODetail.Rows.Add(dr);
                    Label lblRequestedNoOfUnit = gv1.FindControl("lblRequestedNoOfUnit") as Label;
                    double RequestedNoUnit = 0;
                    Double.TryParse(lblRequestedNoOfUnit.Text, out RequestedNoUnit);
                    // NoOfUnit = 0;
                    double.TryParse(lblNoOfUnit.Text, out NoOfUnit);
                    if (RequestedNoUnit >= NoOfUnit)
                    {
                        bool result = ocustapp.confirmCustomerRequest(dtPODetail);
                        if (result)
                        {
                            bindCustomerRequestApproval();
                        }
                        else
                        {
                            CommonFuction.ShowMessage(" Approval Failed ");
                            chk.Checked = false;
                        }
                    }
                    else
                    {
                        chk.Checked = false;
                    }
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Tran Price and Sale Price Cannot Be Zero!!");
                chk.Checked = false;

            }

        }
    }

    private bool CheckTranSalepriceforZero(string lblTRANS_PRICE, string lblSALE_PRICE)
    {
        double Tranprice = 0;
        double SalePrice = 0;
        double.TryParse(lblTRANS_PRICE.ToString(), out Tranprice);
        double.TryParse(lblSALE_PRICE.ToString(), out SalePrice);

        bool result = false;
        if (Tranprice.Equals(0) && SalePrice.Equals(0))
        {
            result = false;

        }
        else
        {
            result = true;
        }



        return result;
    }

    protected void ChkClosed_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = ((CheckBox)(sender));
            GridViewRow gv1 = ((GridViewRow)(chk.NamingContainer));


            DataTable dtPODetail = CreateDataTable();
            CheckBox chkconfirm = gv1.FindControl("chkApproved") as CheckBox;
            CheckBox chkclosed = gv1.FindControl("ChkClosed") as CheckBox;
            custapp ocustapp = new custapp();
            Label lblCompCode = (Label)gv1.FindControl("lblCompCode");
            Label lblBRANCH_CODE = (Label)gv1.FindControl("lblBRANCH_CODE");
            Label lblYEAR = (Label)gv1.FindControl("lblYEAR");
            Label lblORDER_TYPE = (Label)gv1.FindControl("lblORDER_TYPE");
            Label lblORDER_CAT = (Label)gv1.FindControl("lblORDER_CAT");
            Label lblPRODUCT_TYPE = (Label)gv1.FindControl("lblPRODUCT_TYPE");
            Label lblBUSINESS_TYPE = (Label)gv1.FindControl("lblBUSINESS_TYPE");
            Label lblORDER_NO = (Label)gv1.FindControl("lblORDER_NO");
            LinkButton lblARTICLE_NO = (LinkButton)gv1.FindControl("lblARTICLE_NO");
            Label lblSUBSTRATE = (Label)gv1.FindControl("lblSUBSTRATE");
            Label lblCOUNT = (Label)gv1.FindControl("lblCOUNT");
            Label lblSHADE_FAMILY_CODE = (Label)gv1.FindControl("lblSHADE_FAMILY_CODE");
            Label lblSHADE_CODE = (Label)gv1.FindControl("lblSHADE_CODE");
            //END KEY
            TextBox lblNoOfUnit = (TextBox)gv1.FindControl("lblNoOfUnit");
            Label lblWeighofUnit = (Label)gv1.FindControl("lblWeighofUnit");
            Label lblQuantity = (Label)gv1.FindControl("lblQuantity");
            TextBox txtConfirmDate = (TextBox)gv1.FindControl("txtConfirmDate");
            TextBox txtConfirmBy = (TextBox)gv1.FindControl("txtConfirmBy");
            TextBox lblSALE_PRICE = (TextBox)gv1.FindControl("lblSALE_PRICE");

            DataRow dr = dtPODetail.NewRow();

            dr["YEAR"] = lblYEAR.Text;
            dr["COMP_CODE"] = lblCompCode.Text;
            dr["BRANCH_CODE"] = lblBRANCH_CODE.Text;
            dr["ORDER_TYPE"] = lblORDER_TYPE.Text;
            dr["ORDER_CAT"] = lblORDER_CAT.Text;
            dr["BUSINESS_TYPE"] = lblBUSINESS_TYPE.Text;
            dr["PRODUCT_TYPE"] = lblPRODUCT_TYPE.Text;

            dr["ORDER_NO"] = lblORDER_NO.Text;
            dr["ARTICLE_NO"] = lblARTICLE_NO.Text;
            dr["SUBSTRATE"] = lblSUBSTRATE.Text;
            dr["COUNT"] = lblCOUNT.Text;
            dr["SHADE_FAMILY_CODE"] = lblSHADE_FAMILY_CODE.Text;
            dr["SHADE_CODE"] = lblSHADE_CODE.Text;

            dr["APP_WEIGHT_OF_UNIT"] = double.Parse(lblWeighofUnit.Text);
            dr["APP_NO_OF_UNIT"] = double.Parse(lblNoOfUnit.Text);
            dr["QTY_APPROVED"] = double.Parse(lblQuantity.Text);
            dr["CONF_BY"] = oUserLoginDetail.UserCode;
            dr["CONF_DATE"] = DateTime.Now.Date.ToString();
            dr["CONF_FLAG"] = "2";
            dr["TDATE"] = DateTime.Now.Date.ToString();
            dr["TUSER"] = oUserLoginDetail.UserCode;
            double SaleRate;
            double.TryParse(lblSALE_PRICE.Text, out SaleRate);
            dr["SALE_RATE"] = SaleRate;
            dtPODetail.Rows.Add(dr);

            Session["dtPODetailClosed"] = dtPODetail;
            AutoComplete o = new AutoComplete();
            bool result = o.ClosedCustomerRequest();
            SearchbyKeywords();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Closing.\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        DataTable dtPODetail = new DataTable();
        dtPODetail.Columns.Add("YEAR", typeof(int));
        dtPODetail.Columns.Add("COMP_CODE", typeof(string));
        dtPODetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtPODetail.Columns.Add("ORDER_TYPE", typeof(string));
        dtPODetail.Columns.Add("ORDER_CAT", typeof(string));
        dtPODetail.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtPODetail.Columns.Add("BUSINESS_TYPE", typeof(string));
        dtPODetail.Columns.Add("ORDER_NO", typeof(string));
        dtPODetail.Columns.Add("ARTICLE_NO", typeof(string));
        dtPODetail.Columns.Add("SUBSTRATE", typeof(string));
        dtPODetail.Columns.Add("COUNT", typeof(string));
        dtPODetail.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
        dtPODetail.Columns.Add("SHADE_CODE", typeof(string));

        dtPODetail.Columns.Add("APP_WEIGHT_OF_UNIT", typeof(double));
        dtPODetail.Columns.Add("APP_NO_OF_UNIT", typeof(double));
        dtPODetail.Columns.Add("QTY_APPROVED", typeof(double));

        dtPODetail.Columns.Add("CONF_BY", typeof(string));
        dtPODetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtPODetail.Columns.Add("CONF_FLAG", typeof(string));
        dtPODetail.Columns.Add("TDATE", typeof(string));
        dtPODetail.Columns.Add("TUSER", typeof(string));
        dtPODetail.Columns.Add("REMARKS", typeof(string));
        dtPODetail.Columns.Add("TRANS_PRICE", typeof(double));
        dtPODetail.Columns.Add("SALE_RATE", typeof(double));

        return dtPODetail;
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/OrderDevelopment/CustomerRequest/Pages/CRApprovalForYarnSpinning.aspx", false);
    }

    protected void FilterGrid_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
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

    public void SearchbyKeywords()
    {

        try
        {

            string BranchName = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthBranchName")).Text;
            string CRDate = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthCRDate")).Text;
            string CustNo = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthCustNo")).Text;
            string Party = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthPartyName")).Text;
            string GrayYarn = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthGrayYarnDtl")).Text;
            string ShadeFamily = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthShadeFamily")).Text;
            string ShadeCode = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthShadeCode")).Text;

            string transPrice = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthTransPrice")).Text;
            string SalePrice = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthSalePrice")).Text;
            string UOM = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthUOM")).Text;

            string CRQty = ((TextBox)gvCustomerApproval.HeaderRow.FindControl("txthCRQty")).Text;

            custapp ocustapp = new custapp();
            DataTable dt = ocustapp.GetCRBySearchFilterUnApprovedOnly(oUserLoginDetail.COMP_CODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE, BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty,"");

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
                gvCustomerApproval.DataSource = dt;
                gvCustomerApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                DataTable dtblank = CreatedtForGrid();
                DataRow dr = dtblank.NewRow();

                dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;

                dtblank.Rows.Add(dr);

                gvCustomerApproval.DataSource = dtblank;
                gvCustomerApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No Customer Request for approval");
                AutofillSearchContent(BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty);

            }
            AutofillSearchContent(BranchName, CRDate, CustNo, Party, GrayYarn, ShadeFamily, ShadeCode, transPrice, SalePrice, UOM, CRQty);


        }
        catch
        {
            throw;
        }
    }

    private void AutofillSearchContent(string BranchName, string CRDate, string CustNo, string Party, string GrayYarn, string ShadeFamily, string ShadeCode, string transPrice, string SalePrice, string UOM, string CRQty)
    {
        try
        {
            TextBox txthBranchName = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthBranchName");
            TextBox txthCRDate = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthCRDate");
            TextBox txthCustNo = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthCustNo");
            TextBox txthPartyName = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthPartyName");
            TextBox txthGrayYarnDtl = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthGrayYarnDtl");
            TextBox txthShadeFamily = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthShadeFamily");
            TextBox txthShadeCode = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthShadeCode");

            TextBox txthTransPrice = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthTransPrice");
            TextBox txthSalePrice = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthSalePrice");
            TextBox txthUOM = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthUOM");
            TextBox txthCRQty = (TextBox)gvCustomerApproval.HeaderRow.FindControl("txthCRQty");

            txthBranchName.Text = BranchName;
            txthCRDate.Text = CRDate;
            txthCustNo.Text = CustNo;
            txthPartyName.Text = Party;
            txthGrayYarnDtl.Text = GrayYarn;
            txthShadeFamily.Text = ShadeFamily;
            txthShadeCode.Text = ShadeCode;
            txthTransPrice.Text = transPrice;

            txthSalePrice.Text = SalePrice;
            txthUOM.Text = UOM;
            txthCRQty.Text = CRQty;

        }
        catch
        {
            throw;
        }

    }

    private DataTable CreatedtForGrid()
    {
        try
        {
            DataTable dtblank = new DataTable();

            dtblank.Columns.Add("ORDER_DATE", typeof(DateTime));
            dtblank.Columns.Add("COMP_CODE", typeof(string));
            dtblank.Columns.Add("BRANCH_CODE", typeof(string));
            dtblank.Columns.Add("YEAR", typeof(int));
            dtblank.Columns.Add("ORDER_TYPE", typeof(string));
            dtblank.Columns.Add("ORDER_CAT", typeof(string));
            dtblank.Columns.Add("PRODUCT_TYPE", typeof(string));
            dtblank.Columns.Add("CUSTNO", typeof(string));
            dtblank.Columns.Add("BUSINESS_TYPE", typeof(string));
            dtblank.Columns.Add("ARTICLE_NO", typeof(string));
            dtblank.Columns.Add("MAKE", typeof(string));
            dtblank.Columns.Add("TKT_NO", typeof(string));
            dtblank.Columns.Add("SHADE_FAMILY_CODE", typeof(string));
            dtblank.Columns.Add("MATCHING_REFF", typeof(string));
            dtblank.Columns.Add("QUANTITY", typeof(double));
            dtblank.Columns.Add("NO_OF_CASE_BOX", typeof(string));
            dtblank.Columns.Add("END_USE", typeof(string));
            dtblank.Columns.Add("SUBSTRATE", typeof(string));
            dtblank.Columns.Add("COUNT", typeof(string));
            dtblank.Columns.Add("SHADE_CODE", typeof(string));
            dtblank.Columns.Add("LIGHT_SOURCE", typeof(string));
            dtblank.Columns.Add("UOM", typeof(string));
            dtblank.Columns.Add("CR_UNIT", typeof(double));
            dtblank.Columns.Add("REMQTY", typeof(double));
            dtblank.Columns.Add("ADJUST_QTY", typeof(double));
            dtblank.Columns.Add("PRTY_CODE", typeof(string));
            dtblank.Columns.Add("PRTY_NAME", typeof(string));
            dtblank.Columns.Add("QTY_APPROVED", typeof(double));
            dtblank.Columns.Add("APP_NO_OF_UNIT", typeof(double));
            dtblank.Columns.Add("INVOICE_QTY", typeof(double));
            dtblank.Columns.Add("INVOICE_NO_OF_UNIT", typeof(double));
            dtblank.Columns.Add("REMUNIT", typeof(double));
            dtblank.Columns.Add("NO_OF_UNIT", typeof(double));
            dtblank.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtblank.Columns.Add("CONF_FLAG", typeof(string));
            dtblank.Columns.Add("YARN_DESC", typeof(string));
            dtblank.Columns.Add("SHADE_GROUP", typeof(string));
            dtblank.Columns.Add("TRANS_PRICE", typeof(double));
            dtblank.Columns.Add("SALE_PRICE", typeof(double));
            dtblank.Columns.Add("OP_RATE", typeof(double));
            dtblank.Columns.Add("BRANCH_NAME", typeof(string));
            dtblank.Columns.Add("BAL_INVOICE_QTY", typeof(double));
            dtblank.Columns.Add("BAL_INVOICE_NO_OF_UNIT", typeof(double));
            dtblank.Columns.Add("CONF_TYPE", typeof(string));
            dtblank.Columns.Add("CONF_DATE", typeof(DateTime));
            dtblank.Columns.Add("CONF_BY", typeof(string));

            return dtblank;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtPODetail = CreateDataTable();
            int totalRows = gvCustomerApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvCustomerApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {


                    Label lblCompCode = (Label)thisGridViewRow.FindControl("lblCompCode");
                    Label lblBRANCH_CODE = (Label)thisGridViewRow.FindControl("lblBRANCH_CODE");
                    Label lblYEAR = (Label)thisGridViewRow.FindControl("lblYEAR");
                    Label lblORDER_TYPE = (Label)thisGridViewRow.FindControl("lblORDER_TYPE");
                    Label lblORDER_CAT = (Label)thisGridViewRow.FindControl("lblORDER_CAT");
                    Label lblPRODUCT_TYPE = (Label)thisGridViewRow.FindControl("lblPRODUCT_TYPE");
                    Label lblBUSINESS_TYPE = (Label)thisGridViewRow.FindControl("lblBUSINESS_TYPE");
                    Label lblORDER_NO = (Label)thisGridViewRow.FindControl("lblORDER_NO");
                    Label lblARTICLE_NO = (Label)thisGridViewRow.FindControl("lblARTICLE_NO");
                    Label lblSUBSTRATE = (Label)thisGridViewRow.FindControl("lblSUBSTRATE");
                    Label lblCOUNT = (Label)thisGridViewRow.FindControl("lblCOUNT");
                    Label lblSHADE_FAMILY_CODE = (Label)thisGridViewRow.FindControl("lblSHADE_FAMILY_CODE");
                    Label lblSHADE_CODE = (Label)thisGridViewRow.FindControl("lblSHADE_CODE");

                    TextBox lblNoOfUnit = (TextBox)thisGridViewRow.FindControl("lblNoOfUnit");
                    Label lblWeighofUnit = (Label)thisGridViewRow.FindControl("lblWeighofUnit");
                    Label lblQuantity = (Label)thisGridViewRow.FindControl("lblQuantity");

                    TextBox txtConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox txtConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    TextBox lblSALE_PRICE = (TextBox)thisGridViewRow.FindControl("lblSALE_PRICE");
                    CheckBox chkApproved = (CheckBox)thisGridViewRow.FindControl("chkApproved");

                    if (chkApproved.Checked == true)
                    {
                        DataRow dr = dtPODetail.NewRow();

                        dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["ORDER_TYPE"] = lblORDER_TYPE.Text;
                        dr["ORDER_CAT"] = lblORDER_CAT.Text;
                        dr["BUSINESS_TYPE"] = lblBUSINESS_TYPE.Text;
                        dr["PRODUCT_TYPE"] = lblPRODUCT_TYPE.Text;

                        dr["ORDER_NO"] = lblORDER_NO.Text;
                        dr["ARTICLE_NO"] = lblARTICLE_NO.Text;
                        dr["SUBSTRATE"] = lblSUBSTRATE.Text;
                        dr["COUNT"] = lblCOUNT.Text;
                        dr["SHADE_FAMILY_CODE"] = lblSHADE_FAMILY_CODE.Text;
                        dr["SHADE_CODE"] = lblSHADE_CODE.Text;

                        dr["APP_WEIGHT_OF_UNIT"] = double.Parse(lblWeighofUnit.Text);
                        dr["APP_NO_OF_UNIT"] = double.Parse(lblNoOfUnit.Text);
                        dr["QTY_APPROVED"] = double.Parse(lblQuantity.Text);


                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();

                        dr["CONF_FLAG"] = "1";

                        dr["TDATE"] = DateTime.Now.Date.ToString();
                        dr["TUSER"] = oUserLoginDetail.UserCode;
                        double SaleRate;
                        double.TryParse(lblSALE_PRICE.Text, out SaleRate);
                        dr["SALE_RATE"] = SaleRate;
                        dtPODetail.Rows.Add(dr);
                        chkApproved.Checked = false;
                    }
                }
            }
            if (msg != string.Empty)
            {
                CommonFuction.ShowMessage(msg);
            }

            int iResult = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Customer Request approved Successfully.");
                bindCustomerRequestApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request Confirm.\r\nSee error log for detail."));
        }
    }

    protected void gvCustomerApproval_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        gvCustomerApproval.PageIndex = e.NewPageIndex;
        bindCustomerRequestApproval();
    }

    protected void gvCustomerApproval_PreRender1(object sender, EventArgs e)
    {
        gvCustomerApproval.UseAccessibleHeader = true;
        //gvCustomerApproval.HeaderRow.TableSection = TableRowSection.TableHeader;
    }
}
