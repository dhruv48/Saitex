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
using System.Xml;
using System.Net;
using Obout.ComboBox;
using System.Web.UI.HtmlControls;


public partial class Module_OrderDevelopment_CustomerRequest_Controls_CRApprovalYarnDyeing : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string PRODUCT_TYPE = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            PRODUCT_TYPE = "YARN DYEING";
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
              
                bindCustomerRequestApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

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

        dtPODetail.Columns.Add("EMAIL", typeof(string));
        dtPODetail.Columns.Add("PHONE", typeof(string));
        dtPODetail.Columns.Add("Body", typeof(string));
        dtPODetail.Columns.Add("Body1", typeof(string));
        dtPODetail.Columns.Add("CONF_BY", typeof(string));
        dtPODetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtPODetail.Columns.Add("CONF_FLAG", typeof(string));
        dtPODetail.Columns.Add("TDATE", typeof(string));
        dtPODetail.Columns.Add("TUSER", typeof(string));
        dtPODetail.Columns.Add("REMARKS", typeof(string));
        dtPODetail.Columns.Add("TRANS_PRICE", typeof(double));
        dtPODetail.Columns.Add("FINAL_AMOUNT", typeof(double));
        
        dtPODetail.Columns.Add("SALE_RATE", typeof(double));

        return dtPODetail;
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtPODetail = CreateDataTable();
            int totalRows = gvCustomerApproval.Rows.Count;
            string Order_Id = "", strmsg = "";
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvCustomerApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {

                    //Primary key
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
                    //END KEY

                    Label lblEMAIL = (Label)thisGridViewRow.FindControl("lblEMAIL");
                    Label lblPHONE = (Label)thisGridViewRow.FindControl("lblPHONE");

                    Label lblPRTY_Name = (Label)thisGridViewRow.FindControl("lblPRTY_Name");
                    Label lblORDER_DATE = (Label)thisGridViewRow.FindControl("lblORDER_DATE");
                    Label lblFINAL_AMOUNTS = (Label)thisGridViewRow.FindControl("lblFINAL_AMOUNTS");
                    Label lblqualitydesc = (Label)thisGridViewRow.FindControl("lblqualitydesc");
                    Label lblCUSTNO = (Label)thisGridViewRow.FindControl("lblCUSTNO");
                    Label lblUOM = (Label)thisGridViewRow.FindControl("lblUOM");
                    Label lblGRADE = (Label)thisGridViewRow.FindControl("lblGRADE");
                    Label lblQUANTITY_QTY = (Label)thisGridViewRow.FindControl("lblQUANTITY_QTY");
                    Label lblREQ_DATE = (Label)thisGridViewRow.FindControl("lblREQ_DATE");
                    Label lblPAYMENT_MODE = (Label)thisGridViewRow.FindControl("lblPAYMENT_MODE");
                    Label lblPAYMENT_TERMS = (Label)thisGridViewRow.FindControl("lblPAYMENT_TERMS");
                    Label lblFINAL_RATE = (Label)thisGridViewRow.FindControl("lblFINAL_RATE");
                    Label lblPARTY_ARTICLE_DESC = (Label)thisGridViewRow.FindControl("lblPARTY_ARTICLE_DESC");
                    
                    TextBox lblNoOfUnit = (TextBox)thisGridViewRow.FindControl("lblNoOfUnit");
                    Label lblWeighofUnit = (Label)thisGridViewRow.FindControl("lblWeighofUnit");
                    Label lblQuantity = (Label)thisGridViewRow.FindControl("lblQuantity");
                    TextBox txtConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    TextBox txtConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");
                    CheckBox chkApproved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    Label lblTRANS_PRICE = (Label)thisGridViewRow.FindControl("lblTRANS_PRICE");
                    Label lblSALE_PRICE = (Label)thisGridViewRow.FindControl("lblSALE_PRICE");
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
                        dr["ARTICLE_NO"] = lblARTICLE_NO.ToolTip;
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
                        dr["TRANS_PRICE"] = double.Parse(lblTRANS_PRICE.Text);
                        dr["SALE_RATE"] = double.Parse(lblSALE_PRICE.Text);
                       
                        dr["EMAIL"] = lblEMAIL.Text;
                        dr["PHONE"] = lblPHONE.Text;
                        dr["Body"] = OrderTable(lblORDER_NO.Text, lblPRTY_Name.Text, lblPAYMENT_MODE.Text, lblPAYMENT_TERMS.Text, lblORDER_DATE.Text, lblSHADE_CODE.Text, lblARTICLE_NO.Text, lblGRADE.Text, lblqualitydesc.Text, lblPARTY_ARTICLE_DESC.Text, lblFINAL_AMOUNTS.Text, lblCUSTNO.Text, lblQUANTITY_QTY.Text, lblSALE_PRICE.Text, lblFINAL_RATE.Text, lblUOM.Text, lblREQ_DATE.Text);

                        DataView dv = new DataView(dtPODetail);
                        dv.RowFilter = "ORDER_NO='" + lblORDER_NO.Text + "'";
                        if (dv.Count > 0)
                        {
                            dv[0]["Body1"] = strmsg;
                            dtPODetail.AcceptChanges();
                        }
                        dr["Body1"] = strmsg;

                        dtPODetail.Rows.Add(dr);
                        chkApproved.Checked = false;
                    }
                }
            }
            if (msg != string.Empty)
            CommonFuction.ShowMessage(msg);
            if (ViewState["dtPODetail"] != null)
            {
                dtPODetail = (DataTable)ViewState["dtPODetail"];
            }
            if (dtPODetail.Rows.Count > 0 && dtPODetail.Rows.Count > 0)
            {

                int iResult = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateCustomerRequest(oUserLoginDetail.UserCode, dtPODetail);
                if (iResult > 0)
                {
                    string Order_Id1 = "";
                  
                    foreach (DataRow dr in dtPODetail.Rows)
                    {
                        bool found = false;
                        string Body = "";
                        Body = dr["Body"].ToString() + dr["Body1"].ToString();
                        if (Order_Id1 == "")
                        {
                            Order_Id1 = dr["ORDER_NO"].ToString();

                            found = true;
                        }
                        else if (Order_Id1 != dr["ORDER_NO"].ToString())
                        {

                            Order_Id1 = dr["ORDER_NO"].ToString();
                            found = true;
                        }
                        if (found)
                        {
                            string[] CCMail = new string[10];
                            //CCMail[0] = "arun@jingleinfo.com";
                            //CCMail[1] = "arun@jingleinfo.com";
                            //CCMail[3] = "new@sanimoindia.com";
                        //    CCMail[0] = "arun@jingleinfo.com";
                           // CCMail[1] = "chandram@jingleinfo.com";
                           // CCMail[2] = "it@sanimoindia.com";
                          //  CCMail[3] = "ranjeet@jingleinfo.com";
                           // CCMail[4] = "shiv@jingleinfo.com";
                            //CCMail[4] = "suresh@sanimoindia.com";
                            string Subject = "Sanimo Polymers Order Placed Confirmation (For Test)";

                            try
                            {
                                  //bool bMail = SaitexBL.Interface.Method.OD_CAPTURE_MST.SendMail(dr["EMAIL"].ToString(), Body, Subject, "planning@sanimoindia.com", "Planning@123", CCMail);
                                bool bMail = SaitexBL.Interface.Method.OD_CAPTURE_MST.SendMail("bhavesh@sanimoindia.com", Body, Subject, "planning@sanimoindia.com", "Planning@123", CCMail);
                                if (bMail)
                                {

                                }

                            }
                            catch (Exception ex) { errorLog.ErrHandler.WriteError(ex.ToString()); }
                            try
                            {
                                string msg1 = " 	Hi, Your order " + Order_Id1.Trim() + " has been placed with I Sanimo Polymers Pvt Ltd. Queries? Contact 011 41087447 or info@sanimoindia.com";
                                bool bSMS = SaitexBL.Interface.Method.OD_CAPTURE_MST.SendSMS("8826968131,9452449410", msg1);
                                //bool bSMS = SaitexBL.Interface.Method.OD_CAPTURE_MST.SendSMS(dr["MOBILE"].ToString(), msg1);

                                if (bSMS)
                                {

                                }
                            }
                            catch (Exception ex) { errorLog.ErrHandler.WriteError(ex.ToString()); }
                        }
                    }
                    CommonFuction.ShowMessage("Customer Request approved Successfully.");
                    bindCustomerRequestApproval();
                }
            }
            else
            {
                CommonFuction.ShowMessage("Confirm atleast one item.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Customer Request Confirm.\r\nSee error log for detail."));
        }
    }


    private string OrderTable(string OrderNO, string PRTY_NAME, string Payment_Mode , string Payment_terms, string Order_Date, string Shade_code, string Quality_code, string GRADE, string Quality_Desc, string Party_Quality_Desc, string FinalAmount, string cust_no, string Qty, string SalePrice, string Final_rate, string uom, string DeliveryDate)
    {

        string strMsg = "";
        strMsg = "<html><head><title></title></head><body><div width='100%'><div><img src='http://192.168.1.200/texams/CommonImages/logo/SanimoIndia.JPG' /></div>";
        //strMsg += "<div><h3><u>Note : As per new guidelines issued by Govt. of India, we will not be accepting notes of Rs 500 & Rs 1000, starting 9.11.2016 </u></h3></div>";
        strMsg += "<div><h3>Hi " + "" + PRTY_NAME + "" + "</h3></div>";
        strMsg += "<div>Thank you for your order from Sanimo Polymers Pvt. ltd.  If you have any questions about your order please contact us at <b style='color:#1e7ec8;'><u>tejas.shah@sanimoindia.com</u></b> or call us at 09974195795 Monday - Sunday, 9am - 6pm IST. We want to give you a heads up on your order amount which is Rs&nbsp;&nbsp;" + FinalAmount + ". Your order confirmation is below. Thank you again for your business.</div>";
        strMsg += "<div>&nbsp;</div>";
        strMsg += "<div>Your Order #" + "" + OrderNO + "" + " (placed on " + "" + Order_Date + "" + "&nbsp;&nbsp;IST)</div>";
        strMsg += "<div>&nbsp;</div>";
        //strMsg += "<div><table  width='100%' ><tr><th style='background-color:#eaeaea;' cellpadding='5' align='left'>Sold to :</th><th style='background-color:#eaeaea;' align='left'>Ship to :</th></tr>";

        //strMsg += "<tr><td style='border:1px solid  #eaeaea;' align='left'>" + " " + BillingAddress + " " + "</td><td style='border:1px solid  #eaeaea;'>" + " " + ShippingAddress + " " + "</td></tr>";


        strMsg += "<div>&nbsp;</div>";
        strMsg += "<tr><th style='background-color:#eaeaea;' align='left'>Payment Mode : </th><th style='background-color:#eaeaea;' align='left'>";
        strMsg += "<tr><td style='border:1px solid  #eaeaea;' align='left'>" + "" + Payment_Mode + "" + "</td><td style='border:1px solid  #eaeaea;' align='left'><b>Delivery by Date:-&nbsp;&nbsp;</b>" + "" + DeliveryDate + " , " + Payment_terms + "" + "</td></tr></table></div>";
        strMsg += "<div>&nbsp;</div>";
        strMsg += "<div><table  width='100%' cellpadding='5'><tr><th style='background-color:#eaeaea;' align='left'>Quality Desc</th><th style='background-color:#eaeaea;' align='left'>SHADE No</th><th style='background-color:#eaeaea;' align='left'>GRADE</th><th style='background-color:#eaeaea;' align='left'>Sale Rate</th><th style='background-color:#eaeaea;' align='left'>Final Rate</th><th style='background-color:#eaeaea;' align='left'>ORDER QTY</th><th style='background-color:#eaeaea;' align='left'>UOM</th><th style='background-color:#eaeaea;' align='left'>Subtotal</th></tr>";
        strMsg += "<tr><td style='border:1px solid  #eaeaea;'>" + "" + Party_Quality_Desc + "" + "</td><td style='border:1px solid  #eaeaea;'>" + "" + Shade_code + "" + "</td><td style='border:1px solid  #eaeaea;'>" + "" + GRADE + "" + "</td><td style='border:1px solid  #eaeaea;'>" + "" + SalePrice + "" + "</td><td style='border:1px solid  #eaeaea;'>" + "" + Final_rate + "" + "</td><td style='border:1px solid  #eaeaea;'>" + "" + Qty + "" + "</td><td style='border:1px solid  #eaeaea;'>" + "" + uom + "</td><td style='border:1px solid  #eaeaea;'>" + "" + FinalAmount + "" + "</td></tr>";
        strMsg += "<tr><th align='right' style='border:1px solid  #eaeaea;border-right:2px solid  #eaeaea;width:53%' colspan='4'>Grand Total : </th><td style='background-color:#eaeaea;border:1px solid  #eaeaea;'  colspan='4' align='right'>" + "Rs." + FinalAmount + "" + "</td></tr></table></div>";

        return strMsg;

    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
            string URL = "/Saitex/Module/OrderDevelopment/CustomerRequest/Reports/CRApproval4Print.aspx";
            URL += "?PRODUCT_TYPE=" + PRODUCT_TYPE;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/OrderDevelopment/CustomerRequest/Pages/CRApprovalYarnDyeing.aspx", false);
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void bindCustomerRequestApproval()
    {
        try
        {
            var oOD_CUSTOMER_REQUEST_MST = new SaitexDM.Common.DataModel.OD_CUSTOMER_RQST_MST();

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetUnApprovedCustomerRequestSewingOnly(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, PRODUCT_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));
                dt.Columns.Add("DISCOUNT", typeof(string));
                dt.Columns.Add("FREIGHT", typeof(string));
                dt.Columns.Add("SGST", typeof(string));
                dt.Columns.Add("CGST", typeof(string));
                dt.Columns.Add("IGST", typeof(string));
                dt.Columns.Add("NET_RATE", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    DataTable dtMst;
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();

                    oOD_CUSTOMER_REQUEST_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oOD_CUSTOMER_REQUEST_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oOD_CUSTOMER_REQUEST_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oOD_CUSTOMER_REQUEST_MST.PO_NUMB = dr["CUSTNO"].ToString();
                    oOD_CUSTOMER_REQUEST_MST.ORDER_TYPE = dr["ORDER_TYPE"].ToString();
                    oOD_CUSTOMER_REQUEST_MST.BUSINESS_TYPE = dr["BUSINESS_TYPE"].ToString();
                    oOD_CUSTOMER_REQUEST_MST.PRODUCT_TYPE = dr["PRODUCT_TYPE"].ToString();
                    oOD_CUSTOMER_REQUEST_MST.ORDER_CAT = dr["ORDER_CAT"].ToString();
                    dtMst = SaitexBL.Interface.Method.OD_CUSTOMER_REQUEST_MST.GetTaxByPOOnly(oOD_CUSTOMER_REQUEST_MST);


                    if (dr["DISCOUNT"].ToString() == null || dr["DISCOUNT"].ToString() == "")
                    {
                        dr["DISCOUNT"] = "0.00";
                    }
                    if (dr["FREIGHT"].ToString() == null || dr["FREIGHT"].ToString() == "")
                    {
                        dr["FREIGHT"] = "0.00";
                    }
                    if (dr["IGST"].ToString() == null || dr["IGST"].ToString() == "")
                    {
                        dr["IGST"] = "0.00";
                    }
                    if (dr["SGST"].ToString() == null || dr["SGST"].ToString() == "")
                    {
                        dr["SGST"] = "0.00";
                    }

                    if (dr["CGST"].ToString() == null || dr["CGST"].ToString() == "")
                    {
                        dr["CGST"] = "0.00";
                    }

                    for(int i=0;i<dtMst.Rows.Count;i++)
                    {


                        if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "DISCOUNT" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Basic Rate")
                        {

                            dr["DISCOUNT"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * double.Parse(dr["SALE_PRICE"].ToString())) / 100).ToString();

                        }
                        else if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "DISCOUNT" && dr["DISCOUNT"] != "0.00" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() != "Basic Rate" )
                        {

                            Common.CommonFuction.ShowMessage("Discount should be on Basic Rate");
                        }




                        if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "FREIGHT" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Flat Amount")
                        {

                            dr["FREIGHT"] = dtMst.Rows[i]["RATE"].ToString();

                        }

                        else if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "FREIGHT" && dr["FREIGHT"] != "0.00" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() != "Flat Amount")
                        {

                            Common.CommonFuction.ShowMessage("Discount should be on Flat Amount");
                        }
                        
                       


                        if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "SGST" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Final Rate")
                        {

                            dr["SGST"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * (double.Parse(dr["SALE_PRICE"].ToString()) - double.Parse(dr["DISCOUNT"].ToString()) + double.Parse(dr["FREIGHT"].ToString()))) / 100);

                        }
                        else if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "SGST" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Basic Rate")
                        {

                            dr["SGST"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * double.Parse(dr["SALE_PRICE"].ToString())) / 100).ToString();

                        }

                        else if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "SGST" && dr["SGST"] != "0.00" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Flat Amount")
                        {

                            Common.CommonFuction.ShowMessage("Discount should not be on Flat Amount");
                        }
                       
                       


                        if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "CGST" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Final Rate")
                        {

                            dr["CGST"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * (double.Parse(dr["SALE_PRICE"].ToString()) - double.Parse(dr["DISCOUNT"].ToString()) + double.Parse(dr["FREIGHT"].ToString()))) / 100);

                        }
                      else  if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "CGST" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Basic Rate")
                        {

                            dr["CGST"] = dr["CGST"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * double.Parse(dr["SALE_PRICE"].ToString())) / 100).ToString();

                        }

                        else if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "CGST" && dr["CGST"] != "0.00" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Flat Amount")
                        {

                            Common.CommonFuction.ShowMessage("Discount should not be on Flat Amount");
                        }

                       
                       
                        

                        if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "IGST" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Final Rate")
                        {




                            dr["IGST"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * (double.Parse(dr["SALE_PRICE"].ToString()) - double.Parse(dr["DISCOUNT"].ToString()) + double.Parse(dr["FREIGHT"].ToString()))) / 100).ToString();

                       
                        
                        }

                     else   if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "IGST" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Basic Rate")
                        {

                            dr["IGST"] = ((double.Parse(dtMst.Rows[i]["RATE"].ToString()) * double.Parse(dr["SALE_PRICE"].ToString())) / 100).ToString();

                        }

                        else if (dtMst.Rows[i]["COMPO_CODE"].ToString() == "IGST" && dr["IGST"] != "0.00" && dtMst.Rows[i]["BASE_COMPO_CODE"].ToString() == "Flat Amount")
                        {

                            Common.CommonFuction.ShowMessage("Discount should not be on Flat Amount");
                        }
                       
                       
                       
                        
                        
                    }



                    dr["NET_RATE"] = double.Parse(dr["SALE_PRICE"].ToString()) - double.Parse(dr["DISCOUNT"].ToString()) + double.Parse(dr["FREIGHT"].ToString());


                   

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
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void gvCustomerApproval_RowDataBound1(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                GridViewRow grdRow = e.Row;
                Label CONF_FLAG = ((Label)e.Row.FindControl("lblConfFlag"));
                Label REMUNIT = ((Label)e.Row.FindControl("lblREMUNIT"));
                Label INVOICE_NO_OF_UNIT = ((Label)e.Row.FindControl("INVOICE_NO_OF_UNIT"));
                TextBox lblNoOfUnit = ((TextBox)e.Row.FindControl("lblNoOfUnit"));
                Label lblAPP_NO_OF_UNIT = ((Label)e.Row.FindControl("lblAPP_NO_OF_UNIT"));

                CheckBox chkApproved = ((CheckBox)e.Row.FindControl("chkApproved"));
                CheckBox ChkClosed = ((CheckBox)e.Row.FindControl("ChkClosed"));

                if (CONF_FLAG.Text == "1")
                {
                    lblNoOfUnit.Enabled = false;
                    ChkClosed.Enabled = true;
                    e.Row.BackColor = System.Drawing.Color.LightBlue;
                    //chkApproved.BackColor = System.Drawing.Color.Green;
                    chkApproved.Enabled = false;

                    //ChkClosed.ToolTip = "Remaining Unit" + REMUNIT.Text + "||" + " Invoice Unit" + INVOICE_NO_OF_UNIT.Text;
                }
                else if (CONF_FLAG.Text == "2")
                {
                    //ChkClosed.BackColor  = System.Drawing.Color.Red;
                    ChkClosed.Enabled = false;
                }
                else if (CONF_FLAG.Text == "0")
                {
                    // ChkClosed.BackColor = System.Drawing.Color.Red;
                    ChkClosed.Enabled = false;
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
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

        double RequestedNoOfUnit = 0;
        double.TryParse(lblRequestedNoOfUnit.Text, out RequestedNoOfUnit);
        double NoOfUnit = 0;
        double.TryParse(lblNoOfUnit.Text, out NoOfUnit);
        if (RequestedNoOfUnit >= NoOfUnit)
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

    protected void gvCustomerApproval_SelectedIndexChanged(object sender, EventArgs e)
    {


    }

    protected void gvCustomerApproval_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void gvCustomerApproval_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }

    protected void chkApproved_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = ((CheckBox)(sender));
            //if (chk.Checked)
            //{
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
                DataRow dr = dtPODetail.NewRow();

                double appweightofUnit = 0;
                double noofUnit = 0;
                double approvedQty = 0;
                double.TryParse(lblWeighofUnit.Text,out appweightofUnit );
                double.TryParse(lblNoOfUnit.Text, out noofUnit);
                double.TryParse(lblQuantity.Text, out approvedQty);

                if (approvedQty > 0)
                {

                    dr["YEAR"] = lblYEAR.Text;
                    dr["COMP_CODE"] = lblCompCode.Text;
                    dr["BRANCH_CODE"] = lblBRANCH_CODE.Text;
                    dr["ORDER_TYPE"] = lblORDER_TYPE.Text;
                    dr["ORDER_CAT"] = lblORDER_CAT.Text;
                    dr["BUSINESS_TYPE"] = lblBUSINESS_TYPE.Text;
                    dr["PRODUCT_TYPE"] = lblPRODUCT_TYPE.Text;

                    dr["ORDER_NO"] = lblORDER_NO.Text;
                    dr["ARTICLE_NO"] = lblARTICLE_NO.ToolTip;
                    dr["SUBSTRATE"] = lblSUBSTRATE.Text;
                    dr["COUNT"] = lblCOUNT.Text;
                    dr["SHADE_FAMILY_CODE"] = lblSHADE_FAMILY_CODE.Text;
                    dr["SHADE_CODE"] = lblSHADE_CODE.Text;

                    dr["APP_WEIGHT_OF_UNIT"] = appweightofUnit;
                    dr["APP_NO_OF_UNIT"] = noofUnit;
                    dr["QTY_APPROVED"] = approvedQty;
                    dr["CONF_BY"] = oUserLoginDetail.UserCode;
                    dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                    dr["CONF_FLAG"] = "1";
                    dr["TDATE"] = DateTime.Now.Date.ToString();
                    dr["TUSER"] = oUserLoginDetail.UserCode;
                    dr["REMARKS"] = string.Empty;
                    dtPODetail.Rows.Add(dr);
                    bool result = ocustapp.confirmCustomerRequest(dtPODetail);
                }
                else 
                {
                    Common.CommonFuction.ShowMessage("Approved qty cannot be zero.");
                }
               // bindCustomerRequestApproval();
            //}
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Confirming.\r\nSee error log for detail."));
        }
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
            Label lblARTICLE_NO = (Label)gv1.FindControl("lblARTICLE_NO");
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
            dtPODetail.Rows.Add(dr);

            Session["dtPODetailClosed"] = dtPODetail;

            bool result = ocustapp.ClosedCustomerRequest();
            bindCustomerRequestApproval();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Closing.\r\nSee error log for detail."));
        }
    }

    protected void FilterGrid_Click(object sender, EventArgs e)
    {
        SearchbyKeywords();
    }

    private DataTable CreatedtForGrid()
    {
        try
        {
            DataTable dtblank = new DataTable();

            dtblank.Columns.Add("ORDER_DATE", typeof(DateTime));
            dtblank.Columns.Add("REQ_DATE", typeof(DateTime));
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
            dtblank.Columns.Add("FINAL_AMOUNT", typeof(double));
            dtblank.Columns.Add("GRADE", typeof(string));
            dtblank.Columns.Add("EMAIL", typeof(string));
            dtblank.Columns.Add("PHONE", typeof(string));
            dtblank.Columns.Add("FINAL_RATE", typeof(double));
            dtblank.Columns.Add("PAYMENT_MODE", typeof(string));
            dtblank.Columns.Add("PAYMENT_TERMS", typeof(string));
            dtblank.Columns.Add("PARTY_ARTICLE_DESC", typeof(string));

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
            dtblank.Columns.Add("DISCOUNT", typeof(string));
            dtblank.Columns.Add("FREIGHT", typeof(string));
            dtblank.Columns.Add("SGST", typeof(string));
            dtblank.Columns.Add("CGST", typeof(string));
            dtblank.Columns.Add("IGST", typeof(string));
            dtblank.Columns.Add("NET_RATE", typeof(string));
            dtblank.Columns.Add("REMARKS", typeof(string));

            return dtblank;
        }
        catch
        {
            throw;
        }
    }

    private void SearchbyKeywords()
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

}