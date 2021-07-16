using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Common;
using System.Collections;
using System.Configuration;
using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SaitexDM.Common.DataModel;

using Obout.ComboBox;
public partial class Module_Fiber_Controls_FIBER_MASTER_NEW : System.Web.UI.UserControl
 {   
    DataTable dtOP_TRN_SUB = null;
    DataTable dtOP_TRN = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialControls();
                BindControls();
                BindFibreCode();
                BindFiberSubCat();

                if (Session["dtOP_TRN_SUB"] != null)
                {
                    DataTable dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                    if (dtOP_TRN_SUB.Rows.Count > 0)
                    {
                        grdsub_trn.DataSource = dtOP_TRN_SUB;
                        grdsub_trn.DataBind();
                    }

                }
                else
                {
                    grdsub_trn.DataSource = null;
                    grdsub_trn.DataBind();
                }
            }


           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {
        imgbtnSave.Visible = true;
        imgbtnUpdate.Visible = false;
        imgbtnDelete.Visible = true;       
        txtFiberCode.Visible = true;
        DDLFiberCode.SelectedIndex = -1;
        DDLFiberCode.Visible = false;
        ddlfibercat.SelectedIndex = -1;
        ddlsubfiber_cat.SelectedIndex = -1;
        ddluom1.SelectedIndex = -1;
        ddluom2.SelectedIndex = -1;
        Txtuomperbail.Text = string.Empty;
        ddlLengthType.SelectedIndex = -1;
        //txtlengthvalue.Text = string.Empty;
        ddllengthvalue.SelectedIndex = -1; 
        //*************Commented By Nishant Rai at 26-07-2013*****************/

        //txtfinness.Text = string.Empty;
        //txtmosture.Text = string.Empty;
        //txtenduse.Text = string.Empty;
        //txtfiberappearance.Text = string.Empty;
        //txtbio_property.Text = string.Empty;
        //txtproperties.Text = string.Empty;
        txtdescription.Text = string.Empty;

        //*************Commented By Nishant Rai at 26-07-2013*****************/

        txtOpeningBalanceStock.Text = string.Empty;
        txtMimimumStock.Text = string.Empty;
        txtMinimumProcureDays.Text = string.Empty;
        txtOpeningRate.Text = string.Empty;
        txtRecorderLevel.Text = string.Empty;
        txtRecorderQuantity.Text = string.Empty;
        txtMaximumStock.Text = string.Empty;


        //*************ADDED By Nishant Rai at 26-07-2013*****************/
        ddlfiberlusture.SelectedIndex = -1;
        ddlFancyEffect.SelectedIndex = -1;
        txtFiberDenier.Text = string.Empty;
        txtPartyCodecmb.SelectedIndex = -1;
        
        txtTariffHeading.Text = string.Empty;
        txtSales_ITCHS.Text = string.Empty;
        ddlSales_ITCHS.SelectedIndex = -1;
        ddlCustom_ITCHS.SelectedIndex = -1;
        ddlTariffHeading.SelectedIndex = -1;
        txtCustom_ITCHS.Text = string.Empty;       
        rdIsExciable.SelectedValue = "1";

        txtTanacity.Text = string.Empty;
        Txtremark.Text = string.Empty;

        Session["dtOP_TRN_SUB"] = null;
        ViewState["dtDetailTBL"] = null;
        grdsub_trn.DataSource = null;
        grdsub_trn.DataBind();
        txtPartyCode.SelectedIndex = -1;
        txtPartyName.Text = string.Empty;
        
         BindIntial();
    }

    private void InsertData()
    {
        int iRecordFound = 0;
        try
        {
            if (Page.IsValid)
            {
                Fiber_Master_new oFiber_Master_new = new Fiber_Master_new();
                BindFibreCode();
                oFiber_Master_new.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFiber_Master_new.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFiber_Master_new.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFiber_Master_new.FIBER_CODE = CommonFuction.funFixQuotes(txtFiberCode.Text.Trim().ToUpper());
                oFiber_Master_new.FIBER_CAT = CommonFuction.funFixQuotes(ddlfibercat.SelectedValue.Trim());
                oFiber_Master_new.SUB_FIBER_CAT = CommonFuction.funFixQuotes(ddlsubfiber_cat.SelectedValue.Trim());
                oFiber_Master_new.UOM = CommonFuction.funFixQuotes(ddluom1.SelectedValue.Trim());

                oFiber_Master_new.UOM1 = CommonFuction.funFixQuotes(ddluom2.SelectedValue.Trim());
                oFiber_Master_new.UOM_BAIL = CommonFuction.funFixQuotes(Txtuomperbail.Text.Trim());
                oFiber_Master_new.LENGTH_TYPE = CommonFuction.funFixQuotes(ddlLengthType.SelectedValue.Trim());
                oFiber_Master_new.LENGTH_VALUE = CommonFuction.funFixQuotes(ddllengthvalue.SelectedValue);// txtlengthvalue.Text.Trim()

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //oFiber_Master_new.FINEE_FIBER = CommonFuction.funFixQuotes(txtfinness.Text.Trim());
                //oFiber_Master_new.MOISTURE = CommonFuction.funFixQuotes(txtmosture.Text.Trim());
                //oFiber_Master_new.END_USE = CommonFuction.funFixQuotes(txtenduse.Text.Trim());
                //oFiber_Master_new.FIBER_APPEARINCE = CommonFuction.funFixQuotes(txtfiberappearance.Text.Trim());
                //oFiber_Master_new.BIO_LOGIC_PROPERTY = CommonFuction.funFixQuotes(txtbio_property.Text.Trim());
                //oFiber_Master_new.FIBER_PROPERTY = CommonFuction.funFixQuotes(txtproperties.Text.Trim());
                oFiber_Master_new.FIBER_DESC = CommonFuction.funFixQuotes(txtdescription.Text.Trim()).ToUpper();

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiber_Master_new.FIBER_LUSTURE = CommonFuction.funFixQuotes(ddlfiberlusture.SelectedValue.Trim());
                oFiber_Master_new.FIBER_DENIER  = CommonFuction.funFixQuotes(txtFiberDenier.Text.Trim());
                oFiber_Master_new.FANCY_EFFECT = CommonFuction.funFixQuotes(ddlFancyEffect.SelectedValue.Trim());
                oFiber_Master_new.FIBER_SUPPLIER = CommonFuction.funFixQuotes(txtPartyCodecmb.SelectedValue.Trim());
                oFiber_Master_new.FIBER_TENACITY  = CommonFuction.funFixQuotes(txtTanacity.Text.Trim());
                oFiber_Master_new.REMARK = CommonFuction.funFixQuotes(Txtremark.Text.Trim());

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiber_Master_new.OPEN_STOCK = 0;//double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiber_Master_new.MIN_STOCK = double.Parse(txtMimimumStock.Text.Trim());
                oFiber_Master_new.PROCURE_DAYS = int.Parse(txtMinimumProcureDays.Text.Trim());
                oFiber_Master_new.OPEN_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiber_Master_new.REORDER_LEVEL = txtRecorderLevel.Text.Trim();
                oFiber_Master_new.REORDER_QTY = double.Parse(txtRecorderQuantity.Text.Trim());
                oFiber_Master_new.MAXIMUM_STOCK = double.Parse(txtMaximumStock.Text.Trim());
                oFiber_Master_new.TUSER = oUserLoginDetail.UserCode;
                oFiber_Master_new.TDATE = System.DateTime.Now;
                oFiber_Master_new.STATUS = "1";

                oFiber_Master_new.PRTY_CODE = "SELF";
                oFiber_Master_new.OP_BAL_STOCK = 0;// double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiber_Master_new.OP_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiber_Master_new.OP_BAL_PRTY_STOK = double.Parse("0");
                oFiber_Master_new.OP_QTY_ADJ = double.Parse("0");
                oFiber_Master_new.FIB_ISS = "0";
                oFiber_Master_new.FIB_RCPT = "0";
                oFiber_Master_new.CUR_RATE = double.Parse("0");
                oFiber_Master_new.WT_AVRG_RATE = double.Parse("0");
                oFiber_Master_new.LAST_PO_RATE = double.Parse("0");

                Int64 _tariff_heading = 0;
                Int64 _sales_itchs_code = 0;
                Int64 _custom_itchs_code = 0;
                Int64.TryParse(ddlTariffHeading.SelectedValue, out _tariff_heading);
                Int64.TryParse(ddlSales_ITCHS.SelectedValue, out _sales_itchs_code);
                Int64.TryParse(ddlCustom_ITCHS.SelectedValue, out _custom_itchs_code);
                oFiber_Master_new.IS_EXCISABLE = rdIsExciable.SelectedValue;
                oFiber_Master_new.TARIFF_HEADING = _tariff_heading;
                oFiber_Master_new.SALES_ITCHS_CODE = _sales_itchs_code;
                oFiber_Master_new.CUSTOM_ITCHS_CODE = _custom_itchs_code;                

                bool bResult = SaitexBL.Interface.Method.Fiber_Master_new.InsertFiberMstData(oFiber_Master_new, out iRecordFound);
               // bool trnResult= insertTrnData();
                      
                
                if (bResult)
                {

                    Common.CommonFuction.ShowMessage("Record Saved Successfully...");
                    InitialControls();
                    BindControls();
                    BindFibreCode();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Error While Save Record...");
                    InitialControls();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void UpdateData()
    {
        int iRecordFound = 0;
        try
        {
            if (Page.IsValid)
            {
                SaitexDM.Common.DataModel.Fiber_Master_new oFiber_Master_new = new SaitexDM.Common.DataModel.Fiber_Master_new();

                oFiber_Master_new.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFiber_Master_new.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFiber_Master_new.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFiber_Master_new.FIBER_CODE = CommonFuction.funFixQuotes(txtFiberCode.Text.Trim().ToUpper());
                oFiber_Master_new.FIBER_CAT = CommonFuction.funFixQuotes(ddlfibercat.SelectedItem.Text.Trim());
                oFiber_Master_new.SUB_FIBER_CAT = CommonFuction.funFixQuotes(ddlsubfiber_cat.SelectedValue.Trim());
                oFiber_Master_new.UOM = CommonFuction.funFixQuotes(ddluom1.SelectedItem.Text.Trim());
                oFiber_Master_new.UOM1 = CommonFuction.funFixQuotes(ddluom2.SelectedValue.Trim());
                oFiber_Master_new.UOM_BAIL = CommonFuction.funFixQuotes(Txtuomperbail.Text.Trim());
                oFiber_Master_new.LENGTH_TYPE = CommonFuction.funFixQuotes(ddlLengthType.SelectedItem.Text.Trim());
                oFiber_Master_new.LENGTH_VALUE = CommonFuction.funFixQuotes(ddllengthvalue.SelectedValue);//txtlengthvalue.Text.Trim()

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //oFiber_Master_new.FINEE_FIBER = CommonFuction.funFixQuotes(txtfinness.Text.Trim());
                //oFiber_Master_new.MOISTURE = CommonFuction.funFixQuotes(txtmosture.Text.Trim());
                //oFiber_Master_new.END_USE = CommonFuction.funFixQuotes(txtenduse.Text.Trim());
                //oFiber_Master_new.FIBER_APPEARINCE = CommonFuction.funFixQuotes(txtfiberappearance.Text.Trim());
                //oFiber_Master_new.BIO_LOGIC_PROPERTY = CommonFuction.funFixQuotes(txtbio_property.Text.Trim());
                //oFiber_Master_new.FIBER_PROPERTY = CommonFuction.funFixQuotes(txtproperties.Text.Trim());
                oFiber_Master_new.FIBER_DESC = CommonFuction.funFixQuotes(txtdescription.Text.Trim()).ToUpper();

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiber_Master_new.FIBER_LUSTURE = CommonFuction.funFixQuotes(ddlfiberlusture.SelectedValue.Trim());
                oFiber_Master_new.FIBER_DENIER = CommonFuction.funFixQuotes(txtFiberDenier.Text.Trim());
                oFiber_Master_new.FANCY_EFFECT = CommonFuction.funFixQuotes(ddlFancyEffect.SelectedValue.Trim());
                oFiber_Master_new.FIBER_SUPPLIER = CommonFuction.funFixQuotes(txtPartyCodecmb.SelectedValue.Trim());
                oFiber_Master_new.FIBER_TENACITY = CommonFuction.funFixQuotes(txtTanacity.Text.Trim());
                oFiber_Master_new.REMARK = CommonFuction.funFixQuotes(Txtremark.Text.Trim());


                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//


                oFiber_Master_new.OPEN_STOCK = 0;// double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiber_Master_new.MIN_STOCK = double.Parse(txtMimimumStock.Text.Trim());
                oFiber_Master_new.PROCURE_DAYS = int.Parse(txtMinimumProcureDays.Text.Trim());
                oFiber_Master_new.OPEN_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiber_Master_new.REORDER_LEVEL = (txtRecorderLevel.Text.Trim());
                oFiber_Master_new.REORDER_QTY = double.Parse(txtRecorderQuantity.Text.Trim());
                oFiber_Master_new.MAXIMUM_STOCK = double.Parse(txtMaximumStock.Text.Trim());
                oFiber_Master_new.TUSER = oUserLoginDetail.UserCode;
                oFiber_Master_new.TDATE = System.DateTime.Now;
                oFiber_Master_new.STATUS = "1";

                oFiber_Master_new.PRTY_CODE = "SELF";
                oFiber_Master_new.OP_BAL_STOCK = 0;//double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiber_Master_new.OP_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiber_Master_new.OP_BAL_PRTY_STOK = double.Parse("0");
                oFiber_Master_new.OP_QTY_ADJ = double.Parse("0");
                oFiber_Master_new.FIB_ISS = "0";
                oFiber_Master_new.FIB_RCPT = "0";
                oFiber_Master_new.CUR_RATE = double.Parse("0");
                oFiber_Master_new.WT_AVRG_RATE = double.Parse("0");
                oFiber_Master_new.LAST_PO_RATE = double.Parse("0");
                Int64 _tariff_heading = 0;
                Int64 _sales_itchs_code = 0;
                Int64 _custom_itchs_code = 0;
                Int64.TryParse(ddlTariffHeading.SelectedValue, out _tariff_heading);
                Int64.TryParse(ddlSales_ITCHS.SelectedValue, out _sales_itchs_code);
                Int64.TryParse(ddlCustom_ITCHS.SelectedValue, out _custom_itchs_code);
                oFiber_Master_new.IS_EXCISABLE = rdIsExciable.SelectedValue;
                oFiber_Master_new.TARIFF_HEADING = _tariff_heading;
                oFiber_Master_new.SALES_ITCHS_CODE = _sales_itchs_code;
                oFiber_Master_new.CUSTOM_ITCHS_CODE = _custom_itchs_code;


                bool bResult = SaitexBL.Interface.Method.Fiber_Master_new.UpdateFiberMstData(oFiber_Master_new, out iRecordFound);
               // bool trnResult = updateTrnData();
                if (bResult)
                {

                    Common.CommonFuction.ShowMessage("Record Update Successfully...");
                    InitialControls();
                    BindControls();
                    BindFibreCode();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Record already exist...");
                    InitialControls();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindControls()
    {
        BindUom("UOM");
        BindUom1("UOM");
        BindFiberCat("FIBER_MASTER");
        BindLengthType("LENGTH_TYPE", ddlLengthType);
        BindLengthType("FILAMENT", ddllengthvalue);

        BindLengthType("ITCHS_SALES", ddlSales_ITCHS);
        BindLengthType("ITCHS_CUSTOM", ddlCustom_ITCHS);
        BindLengthType("TARIFF_HEADING", ddlTariffHeading);

        //*************ADDED By Nishant Rai at 26-07-2013*****************/

        BindFiberLusture("FIBER_LUSTURE");
        BindFancyEffect("FIBER_FANCY_EFFECT");

    }

    //private void BindFiberCode()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.BindFiberCode();
    //        DDLFiberCode.Items.Clear();
    //        DDLFiberCode.DataSource = dt;
    //        DDLFiberCode.DataValueField = "FIBER_CODE";
    //        DDLFiberCode.DataTextField = "FIBER_CODE";
    //        DDLFiberCode.DataBind();
    //        DDLFiberCode.Items.Insert(0, new ListItem("----Select---"));

    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}
    private void BindFiberCodeinFindMode(string text, int offset)
    {

        try
        {

            string CommandText = "SELECT DISTINCT LTRIM (RTRIM (FIBER_CODE)) AS FIBER_CODE , FIBER_DESC FROM TX_FIBER_MASTER ";
            string WhereClause = "  where upper(FIBER_CODE) like :SearchQuery  or upper(FIBER_DESC)  like :SearchQuery OR upper(FIBER_CAT)  like  :SearchQuery  OR  upper(SUB_FIBER_CAT)  LIKE  :SearchQuery";
            string SortExpression = "  order by FIBER_CODE  asc";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    DDLFiberCode.Items.Clear();
                    DDLFiberCode.DataTextField = "FIBER_DESC";
                    DDLFiberCode.DataValueField = "FIBER_CODE";
                    DDLFiberCode.DataSource = data;
                    DDLFiberCode.DataBind();
                    // ddlyarncode.Items.Insert(0, new ListItem("------Select------", "0"));
                }
            }


        }
        catch
        {

        }
    }

    protected void DDLFiberCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        BindFiberCodeinFindMode(e.Text, e.ItemsOffset);
    }

    private void BindFiberSubCat()
    {
        string fiber_cat = ddlfibercat.SelectedItem.Text;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.BindFiberSubCat(fiber_cat);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlsubfiber_cat.Items.Clear();
                ddlsubfiber_cat.DataSource = dt;
                ddlsubfiber_cat.DataTextField = "FIBR_SUBCAT";
                ddlsubfiber_cat.DataValueField = "FIBR_SUBCAT";
                ddlsubfiber_cat.DataBind();
                ddlsubfiber_cat.Items.Insert(0, new ListItem("----Select---",""));
            }
            else
            {
                ddlsubfiber_cat.Items.Clear();
                ddlsubfiber_cat.DataSource = null;               
                ddlsubfiber_cat.DataBind();
                ddlsubfiber_cat.Items.Insert(0, new ListItem("----Select---",""));
                //Common.CommonFuction.ShowMessage("Sub. Category is not Available by selected cat... Contact To Admin");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindLengthType(string MST_NAME, DropDownList ddlLengthType)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlLengthType.Items.Clear();
                ddlLengthType.DataSource = dt;
                ddlLengthType.DataTextField = "MST_CODE";
                ddlLengthType.DataValueField = "MST_CODE";
                ddlLengthType.DataBind();
                ddlLengthType.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFiberCat(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlfibercat.Items.Clear();
                ddlfibercat.DataSource = dt;
                ddlfibercat.DataTextField = "MST_CODE";
                ddlfibercat.DataValueField = "MST_CODE";
                ddlfibercat.DataBind();
               // ddlfibercat.Items.Insert(0, new ListItem("------Select------", "0"));
                //ddlfibercat.SelectedIndex = 1;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindUom(string MST_NAME)
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddluom1.Enabled = true;
                ddluom1.Items.Clear();
                ddluom1.DataSource = dt;
                ddluom1.DataTextField = "MST_CODE";
                ddluom1.DataValueField = "MST_CODE";
                ddluom1.DataBind();
                ddluom1.Items.Insert(0, new ListItem("------Select------", "0"));
                ddluom1.SelectedValue = "KG";
                ddluom1.Enabled = false;
            }

        }
        catch
        {
            throw;
        }
    }
    private void BindUom1(string MST_NAME)
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddluom2.Enabled = true;
                ddluom2.Items.Clear();
                ddluom2.DataSource = dt;
                ddluom2.DataTextField = "MST_CODE";
                ddluom2.DataValueField = "MST_CODE";
                ddluom2.DataBind();
                ddluom2.Items.Insert(0, new ListItem("------Select------", "0"));
                ddluom2.SelectedIndex = ddluom2.Items.IndexOf(ddluom2.Items.FindByValue("BAIL"));    
                ddluom2.Enabled = false;
            }

        }
        catch
        {
            throw;
        }
    }
    private void BindFiberLusture(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlfiberlusture.Items.Clear();
                ddlfiberlusture.DataSource = dt;
                ddlfiberlusture.DataTextField = "MST_CODE";
                ddlfiberlusture.DataValueField = "MST_CODE";
                ddlfiberlusture.DataBind();
                ddlfiberlusture.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFancyEffect(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFancyEffect.Items.Clear();
                ddlFancyEffect.DataSource = dt;
                ddlFancyEffect.DataTextField = "MST_CODE";
                ddlFancyEffect.DataValueField = "MST_CODE";
                ddlFancyEffect.DataBind();
                ddlFancyEffect.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindFibreCode()
    {
        try
        {
            txtFiberCode.Enabled = true;          
            string FType = string.Empty;           
            string msg = string.Empty;
            FType = ddlfibercat.SelectedItem.ToString();
            string PREFIX = SaitexBL.Interface.Method.Fiber_Master_new.GetPrefixCode(FType);
            string FibleCode = SaitexBL.Interface.Method.Fiber_Master_new.GetFabricCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,FType.Trim(),PREFIX.ToUpper());
            txtFiberCode.Text = FibleCode;
            txtFiberCode.Enabled = false;
           
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        InsertData();
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpdateData();
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        imgbtnSave.Visible = false;
        imgbtnUpdate.Visible = true;
        txtFiberCode.Visible = false;
        DDLFiberCode.Visible = true;

        BindFiberCodeinFindMode("",0);

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialControls();
        BindControls();
        BindFibreCode();
        BindFiberSubCat();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message));
        }
    }

    protected void DDLFiberCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetDataByFiberCode(DDLFiberCode.SelectedValue.ToString());
        ViewState["iFIBER_CODE"] = DDLFiberCode.SelectedValue.ToString();
    }

    private void GetDataByFiberCode(string iFIBER_CODE)
    {
        try
        {
            bindPartyCode1("", 0);
            
            DataTable dt = SaitexBL.Interface.Method.Fiber_Master_new.GetDataByFiberCode(iFIBER_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFiberCode.Text = dt.Rows[0]["FIBER_CODE"].ToString().Trim();
                ddlfibercat.SelectedItem.Text = dt.Rows[0]["FIBER_CAT"].ToString().Trim();
                BindFiberSubCat();
                ddlsubfiber_cat.SelectedIndex = ddlsubfiber_cat.Items.IndexOf(ddlsubfiber_cat.Items.FindByValue(dt.Rows[0]["SUB_FIBER_CAT"].ToString().Trim()));
               
                ddluom1.SelectedItem.Text = dt.Rows[0]["UOM"].ToString().Trim();
                ddluom2.SelectedItem.Text = dt.Rows[0]["UOM1"].ToString().Trim();
                ddlLengthType.SelectedItem.Text = dt.Rows[0]["LENGTH_TYPE"].ToString().Trim();
                ddllengthvalue.SelectedIndex = ddllengthvalue.Items.IndexOf(ddllengthvalue.Items.FindByValue(dt.Rows[0]["LENGTH_VALUE"].ToString()));  //txtlengthvalue.Text 
                Txtuomperbail.Text = dt.Rows[0]["UOM_BAIL"].ToString().Trim();
                //************************* COMMENTED By Nishant Rai at 26-07-2013 ****************************//
                //txtfinness.Text = dt.Rows[0]["FINEE_FIBER"].ToString().Trim();
                //txtmosture.Text = dt.Rows[0]["MOISTURE"].ToString().Trim();
                //txtenduse.Text = dt.Rows[0]["END_USE"].ToString().Trim();
                //txtfiberappearance.Text = dt.Rows[0]["FIBER_APPEARINCE"].ToString().Trim();
                //txtbio_property.Text = dt.Rows[0]["BIO_LOGIC_PROPERTY"].ToString().Trim();
                //txtproperties.Text = dt.Rows[0]["FIBER_PROPERTY"].ToString().Trim();
                txtdescription.Text = dt.Rows[0]["FIBER_DESC"].ToString().Trim();
                //************************* COMMENTED By Nishant Rai at 26-07-2013 ****************************//

                txtOpeningBalanceStock.Text = dt.Rows[0]["OPEN_STOCK"].ToString().Trim();
                txtMimimumStock.Text = dt.Rows[0]["MIN_STOCK"].ToString().Trim();
                txtMinimumProcureDays.Text = dt.Rows[0]["PROCURE_DAYS"].ToString().Trim();
                txtOpeningRate.Text = dt.Rows[0]["OPEN_RATE"].ToString().Trim();
                txtRecorderLevel.Text = dt.Rows[0]["REORDER_LEVEL"].ToString().Trim();
                txtRecorderQuantity.Text = dt.Rows[0]["REORDER_QTY"].ToString().Trim();
                txtMaximumStock.Text = dt.Rows[0]["MAXIMUM_STOCK"].ToString().Trim();

                //************************* Added By Nishant Rai at 27-07-2013 ****************************//

                ddlfiberlusture.SelectedValue= dt.Rows[0]["LUSTURE"].ToString().Trim();
                txtFiberDenier.Text = dt.Rows[0]["DENIER"].ToString().Trim();
                ddlFancyEffect.SelectedValue= dt.Rows[0]["FANCY_EFFECT"].ToString().Trim();              
                txtTanacity.Text = dt.Rows[0]["TENACITY"].ToString().Trim();
                Txtremark.Text = dt.Rows[0]["REMARK"].ToString().Trim();

                if (!string.IsNullOrEmpty(dt.Rows[0]["FIBER_SUPPLIER"].ToString()))
                {
                    txtPartyCodecmb.SelectedValue = dt.Rows[0]["FIBER_SUPPLIER"].ToString().Trim(); 
                }

                ddlTariffHeading.SelectedIndex =ddlTariffHeading.Items.IndexOf(ddlTariffHeading.Items.FindByValue(dt.Rows[0]["TARIFF_HEADING"].ToString()));
                ddlSales_ITCHS.SelectedIndex = ddlSales_ITCHS.Items.IndexOf(ddlSales_ITCHS.Items.FindByValue(dt.Rows[0]["SALES_ITCHS_CODE"].ToString()));
                ddlCustom_ITCHS.SelectedIndex = ddlCustom_ITCHS.Items.IndexOf(ddlCustom_ITCHS.Items.FindByValue(dt.Rows[0]["CUSTOM_ITCHS_CODE"].ToString()));
                rdIsExciable.SelectedIndex = rdIsExciable.Items.IndexOf(rdIsExciable.Items.FindByValue(dt.Rows[0]["IS_EXCISABLE"].ToString()));


                //SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
                //oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                //oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                //oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                //oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                //oTX_FIBER_IR_MST.TRN_TYPE = "OPB01";
                // Session["dtOP_TRN_SUB"] = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetSUBTRN_DataByFiberCode(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtFiberCode.Text, "OPB01");
                //ViewState["dtDetailTBL"] = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetTRN_DataByFiberCode(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtFiberCode.Text, "OPB01");
               
                //  MapTrnDataTable();
                //  MapDataTable();
                //  BindBOMGrid();





               
                //************************* Added By Nishant Rai at 27-07-2013 ****************************//
            

            }
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
  private void MapTrnDataTable()
        {
            try
            {
                DataTable dtOP_TRN_SUB = null;
                if (Session["dtOP_TRN_SUB"] != null)
                {
                    dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                }
                if (!dtOP_TRN_SUB.Columns.Contains("UNIQUE_ID"))
                    dtOP_TRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
                if (!dtOP_TRN_SUB.Columns.Contains("PI_NO"))
                    dtOP_TRN_SUB.Columns.Add("PI_NO", typeof(string));

                for (int iLoop = 0; iLoop < dtOP_TRN_SUB.Rows.Count; iLoop++)
                {
                    dtOP_TRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                    dtOP_TRN_SUB.Rows[iLoop]["PI_NO"] = "NA";
                }
                dtOP_TRN_SUB.AcceptChanges();
                Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;
            }
            catch
            {
                throw;
            }
        }

  private void MapDataTable()
  {
      try
      {
          DataTable dtDetailTBL = null;
          if (ViewState["dtDetailTBL"] != null)
          {
              dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
          }
          if (!dtDetailTBL.Columns.Contains("UNIQUE_ID"))
              dtDetailTBL.Columns.Add("UNIQUE_ID", typeof(int));
          if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
              dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
          if (!dtDetailTBL.Columns.Contains("PI_NO"))
              dtDetailTBL.Columns.Add("PI_NO", typeof(string));

          for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
          {
              dtDetailTBL.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
              dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;     
          }
          dtDetailTBL.AcceptChanges();
          ViewState["dtDetailTBL"] = dtDetailTBL;
      }
      catch
      {
          throw;
      }
  }
    
    protected void ddlfibercat_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindFibreCode();
        BindFiberSubCat();
    }
   
    protected void txtFiberCode_TextChanged(object sender, EventArgs e)
    {
        string fib_code = txtFiberCode.Text.Trim().ToUpper();
        string comp_code = oUserLoginDetail.COMP_CODE;
        string branch_code = oUserLoginDetail.CH_BRANCHCODE;
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.Fiber_Master_new.CheckDuplicateFiberCode(fib_code, comp_code, branch_code);
            if (dt != null && dt.Rows.Count > 0)
            {
                Common.CommonFuction.ShowMessage("Please Select Abother Fiber Code");
                InitialControls();
                BindControls();
            }
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }







    //************************* Added By Nishant Rai at 26-07-2013 ****************************//



    private void bindPartyCode1(string SearchQuery,int OFFSET)
    {
        try
        {
           // if (OFFSET == 0)
           //// {
             //   OFFSET = 1000;
           // }
            //if (string.IsNullOrEmpty(SearchQuery))
            //{
               // SearchQuery = "%";
            //
        
                   DataTable dt = GetPartyData(SearchQuery, OFFSET);
            txtPartyCodecmb.Items.Clear();
            txtPartyCodecmb.DataSource = dt;
            txtPartyCodecmb.DataTextField = "PRTY_CODE";
            txtPartyCodecmb.DataValueField = "PRTY_CODE";
            txtPartyCodecmb.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void txtPartyCodecmb_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCodecmb.Items.Clear();
            txtPartyCodecmb.DataSource = data;
            txtPartyCodecmb.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
           string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }


    //************************* Added By Nishant Rai at 26-07-2013 ****************************//
    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtOpeningBalanceStock_TextChanged(object sender, EventArgs e)
    {

    }
    protected void txtFiberDenier_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txtPartyCodecmb_TextChanged(object sender, EventArgs e)
    {
        //txtpartycode.Enabled = true;
        //txtpartycode.Text = txtPartyCodecmb.SelectedValue;
        //txtpartycode.Enabled = false;
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        DataTable myDataTable = new DataTable();
        DataColumn myDataColumn;

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "FIBER_CAT";
        myDataTable.Columns.Add(myDataColumn);

        myDataColumn = new DataColumn();
        myDataColumn.DataType = Type.GetType("System.String");
        myDataColumn.ColumnName = "BRANCH_CODE";
        myDataTable.Columns.Add(myDataColumn);

        DataRow row;
        row = myDataTable.NewRow();
        row["FIBER_CAT"] = string.Empty;
        row["BRANCH_CODE"] =string.Empty;
        myDataTable.Rows.Add(row);
        Session["fiberreportdt"] = myDataTable;

        string URL = "../Reports/FiberMasterReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);
    
    }
    


    public bool insertTrnData()
    {

        Hashtable htReceive = new Hashtable();
        SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
        oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
        oTX_FIBER_IR_MST.FORM_NUMB = "";
        oTX_FIBER_IR_MST.FORM_TYPE = "";

        DateTime dt = System.DateTime.Now.Date;
        bool Is_Gate_Entry = false;
        Is_Gate_Entry = DateTime.TryParse(DateTime.MinValue.ToString().Trim(), out dt);
        htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FIBER_IR_MST.GATE_DATE = dt;
        oTX_FIBER_IR_MST.GATE_NUMB = "";
        oTX_FIBER_IR_MST.GATE_OUT_NUMB = "";
        oTX_FIBER_IR_MST.GATE_PASS_TYPE = "";
        oTX_FIBER_IR_MST.LORY_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        Is_LR = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("LR", Is_LR);
        oTX_FIBER_IR_MST.LR_DATE = dt;

        oTX_FIBER_IR_MST.LR_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FIBER_IR_MST.PRTY_CH_DATE = dt;


        oTX_FIBER_IR_MST.PRTY_CH_NUMB ="";
        oTX_FIBER_IR_MST.PRTY_CODE = "";
        oTX_FIBER_IR_MST.PRTY_NAME = "";
        oTX_FIBER_IR_MST.RCOMMENT ="";
        oTX_FIBER_IR_MST.REPROCESS ="";
        oTX_FIBER_IR_MST.SHIFT = "";


        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(DateTime.Now.ToString() , out dt);
        htReceive.Add("MRN", Is_MRN);
        
        
        oTX_FIBER_IR_MST.TRSP_CODE = "";

        oTX_FIBER_IR_MST.TUSER = oUserLoginDetail.UserCode;
        int TRN_NUMB = 0;

        oTX_FIBER_IR_MST.BILL_NUMB ="";


        dt = System.DateTime.Now.Date;
        bool Is_Bill_Date = false;
        Is_Bill_Date = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("BILL_DATE", Is_Bill_Date);
        oTX_FIBER_IR_MST.BILL_DATE = dt;


        oTX_FIBER_IR_MST.BILL_TYPE = "FSP";
        oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


        
        oTX_FIBER_IR_MST.EXCISE_TYPE = string.Empty ;
        oTX_FIBER_IR_MST.SPINNERS = string.Empty;
        oTX_FIBER_IR_MST.TRN_DATE = dt;
        oTX_FIBER_IR_MST.TRN_TYPE = "OPB01";
        DataTable dtOP_TRN_SUB = null;
        if (Session["dtOP_TRN_SUB"] != null)
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
        }
        //creteTrnData();
        DataTable dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        bool _result = false;
        for (int i = 0; i< dtDetailTBL.Rows.Count; i++)
        {
            oTX_FIBER_IR_MST.PRTY_CODE = dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() ;
            oTX_FIBER_IR_MST.PRTY_NAME = dtDetailTBL.Rows[i]["PRTY_NAME"].ToString();
            double totalAmt = 0;
            double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out totalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out finalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = finalAmt;
            oTX_FIBER_IR_MST.FINAL_AMOUNT = finalAmt;
            oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewMRNNumber(oTX_FIBER_IR_MST).ToString());
            DataView dvDetailTBL = new DataView(dtDetailTBL);
            dvDetailTBL.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";
            DataView dvOP_TRN_SUB = new DataView(dtOP_TRN_SUB);
            dvOP_TRN_SUB.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";

            _result = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Insert(oTX_FIBER_IR_MST, out TRN_NUMB, dvDetailTBL.ToTable(), htReceive, dvOP_TRN_SUB.ToTable(), new DataTable(), new DataTable());
        }
        return _result;
    }

    public bool updateTrnData()
    {

        Hashtable htReceive = new Hashtable();
        SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
        oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FIBER_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
        oTX_FIBER_IR_MST.FORM_NUMB = "";
        oTX_FIBER_IR_MST.FORM_TYPE = "";

        DateTime dt = System.DateTime.Now.Date;
        bool Is_Gate_Entry = false;
        Is_Gate_Entry = DateTime.TryParse(DateTime.MinValue.ToString().Trim(), out dt);
        htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FIBER_IR_MST.GATE_DATE = dt;
        oTX_FIBER_IR_MST.GATE_NUMB = "";
        oTX_FIBER_IR_MST.GATE_OUT_NUMB = "";
        oTX_FIBER_IR_MST.GATE_PASS_TYPE = "";
        oTX_FIBER_IR_MST.LORY_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        Is_LR = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("LR", Is_LR);
        oTX_FIBER_IR_MST.LR_DATE = dt;

        oTX_FIBER_IR_MST.LR_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FIBER_IR_MST.PRTY_CH_DATE = dt;


        oTX_FIBER_IR_MST.PRTY_CH_NUMB = "";
        oTX_FIBER_IR_MST.PRTY_CODE = "";
        oTX_FIBER_IR_MST.PRTY_NAME = "";
        oTX_FIBER_IR_MST.RCOMMENT = "";
        oTX_FIBER_IR_MST.REPROCESS = "";
        oTX_FIBER_IR_MST.SHIFT = "";


        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(DateTime.Now.ToString(), out dt);
        htReceive.Add("MRN", Is_MRN);
        oTX_FIBER_IR_MST.TRN_DATE = dt;
        oTX_FIBER_IR_MST.TRN_TYPE = "OPB01";          
        oTX_FIBER_IR_MST.TRSP_CODE = "";

        oTX_FIBER_IR_MST.TUSER = oUserLoginDetail.UserCode;
        int TRN_NUMB = 0;

        oTX_FIBER_IR_MST.BILL_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Bill_Date = false;
        Is_Bill_Date = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("BILL_DATE", Is_Bill_Date);
        oTX_FIBER_IR_MST.BILL_DATE = dt;


        oTX_FIBER_IR_MST.BILL_TYPE = "FSP";
        oTX_FIBER_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;

        oTX_FIBER_IR_MST.EXCISE_TYPE = string.Empty;
        oTX_FIBER_IR_MST.SPINNERS = string.Empty;
       

        DataTable dtOP_TRN_SUB = null;
        if (Session["dtOP_TRN_SUB"] != null)
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
        }
        //creteTrnData();
        DataTable dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

        bool _result = false;
        for (int i = 0; i < dtDetailTBL.Rows.Count; i++)
        {
            oTX_FIBER_IR_MST.PRTY_CODE = dtDetailTBL.Rows[i]["PRTY_CODE"].ToString();
            oTX_FIBER_IR_MST.PRTY_NAME = dtDetailTBL.Rows[i]["PRTY_NAME"].ToString();
            double totalAmt = 0;
            double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out totalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = totalAmt;

            double finalAmt = 0;
            double.TryParse(dtDetailTBL.Rows[i]["AMOUNT"].ToString(), out finalAmt);
            oTX_FIBER_IR_MST.TOTAL_AMOUNT = finalAmt;
            oTX_FIBER_IR_MST.FINAL_AMOUNT = finalAmt;
            if (!string.IsNullOrEmpty(dtDetailTBL.Rows[i]["TRN_NUMB"].ToString()))
            {
                oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(dtDetailTBL.Rows[i]["TRN_NUMB"].ToString());
            }
            else
            {
                oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetNewMRNNumber(oTX_FIBER_IR_MST).ToString());
            }
            DataView dvDetailTBL = new DataView(dtDetailTBL);
            dvDetailTBL.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";
            DataView dvOP_TRN_SUB = new DataView(dtOP_TRN_SUB);
            dvOP_TRN_SUB.RowFilter = " FIBER_CODE='" + dtDetailTBL.Rows[i]["FIBER_CODE"].ToString().ToUpper() + "'  and LOT_NO='" + dtDetailTBL.Rows[i]["LOT_NO"].ToString().ToUpper() + "' AND  PALLET_CODE='" + dtDetailTBL.Rows[i]["PALLET_CODE"].ToString().ToUpper() + "' AND PRTY_CODE='" + dtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "'";
            _result= SaitexBL.Interface.Method.TX_FIBER_IR_MST.Update(oTX_FIBER_IR_MST, dvDetailTBL.ToTable(), htReceive, dvOP_TRN_SUB.ToTable(), new DataTable(), new DataTable());

        }
        return _result;
    }



    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("FIBER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FIBER_DESC", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
            dtDetailTBL.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtDetailTBL.Columns.Add("TRN_QTY", typeof(double));
            dtDetailTBL.Columns.Add("NO_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtDetailTBL.Columns.Add("UOM1", typeof(string));
            dtDetailTBL.Columns.Add("UOM_BAIL", typeof(string));
            dtDetailTBL.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("AMOUNT", typeof(double));
            dtDetailTBL.Columns.Add("COST_CENTER_CODE", typeof(string));
            dtDetailTBL.Columns.Add("MAC_CODE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("QCFLAG", typeof(string));
            dtDetailTBL.Columns.Add("PO_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PO_COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PO_BRANCH", typeof(string));
            dtDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtDetailTBL.Columns.Add("PALLET_CODE", typeof(string));
            dtDetailTBL.Columns.Add("LOT_NO", typeof(string));
            dtDetailTBL.Columns.Add("GRADE", typeof(string));
            dtDetailTBL.Columns.Add("TRN_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("TRN_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("PRTY_CODE", typeof(string));
            dtDetailTBL.Columns.Add("PRTY_NAME", typeof(string));
            return dtDetailTBL;
        }
        catch
        {

            throw;
        }
    }


    public void creteTrnData()
    {
        try
        {
            double RATE=0;
            double QTY=0;
            DataTable dtDetailTBL=null;
            if(ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            else
            {
                dtDetailTBL = CreateDataTable(); 
            }
            if (Session["dtOP_TRN_SUB"] != null)
            {
                dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                QTY = calculatiallTrnData(dtOP_TRN_SUB, out   RATE);
            }
            
            if (txtFiberCode.Text != "" && txtRate.Text != "" && txtPalletCode.Text != "" )
            {
              DataView dv = new DataView(dtDetailTBL);
              dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim().ToUpper() + "'  and LOT_NO='" + txtLotNo.Text.Trim().ToUpper() + "' AND  PALLET_CODE='" + txtPalletCode.Text.Trim().ToUpper() + "' AND PRTY_CODE='"+txtPartyCode.SelectedText+"'";
              if (dv.Count > 0)
              {                  
                  dv[0]["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                  dv[0]["PO_NUMB"] = 99999;
                  dv[0]["PO_TYPE"] = "OPB";
                  dv[0]["PO_COMP_CODE"] = "C99999";
                  dv[0]["PO_BRANCH"] = "B99999";
                  dv[0]["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();
                  dv[0]["FIBER_DESC"] = txtdescription.Text.Trim();
                  dv[0]["TRN_QTY"] = 0;//Math.Round(double.Parse(txtOpeningBalanceStock.Text.Trim()), 3);
                  dv[0]["UOM"] = ddluom1.SelectedValue;
                  dv[0]["UOM1"] = ddluom1.SelectedValue;
                  dv[0]["UOM_BAIL"] = ddluom2.SelectedValue;
                  dv[0]["BASIC_RATE"] =Math.Round(RATE, 3);
                  dv[0]["FINAL_RATE"] =  Math.Round(RATE, 3);
                  dv[0]["AMOUNT"] = Math.Round(RATE*QTY, 3);
                  dv[0]["COST_CENTER_CODE"] = "";
                  dv[0]["MAC_CODE"] = string.Empty;
                  dv[0]["REMARKS"] = "";
                  dv[0]["QCFLAG"] = "No";
                  DateTime dd = System.DateTime.Now;
                  DateTime.TryParse(DateTime.MinValue.ToString(), out dd);
                  dv[0]["DATE_OF_MFG"] = dd;
                  dv[0]["NO_OF_UNIT"] = 0;// calculateNoOfUnit();
                  dv[0]["WEIGHT_OF_UNIT"] = 0;// calculateWeightofunit();
                  dv[0]["UOM_OF_UNIT"] = ddluom1.SelectedValue.Trim();
                  dv[0]["PI_NO"] = "NA";
                  dv[0]["PALLET_CODE"] = txtPalletCode.Text.ToUpper().Trim();
                  dv[0]["LOT_NO"] = txtLotNo.Text.ToUpper().Trim();
                  dv[0]["GRADE"] = txtGrade.Text.ToUpper().Trim();
                  dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                  dv[0]["PRTY_NAME"] = txtPartyName.Text;
                  dtDetailTBL.AcceptChanges();
              }
              else
              {
                  DataRow dr = dtDetailTBL.NewRow();
                  dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                  dr["PO_NUMB"] = 99999;
                  dr["PO_TYPE"] = "OPB";
                  dr["PO_COMP_CODE"] = "C99999";
                  dr["PO_BRANCH"] = "B99999";
                  dr["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();
                  dr["FIBER_DESC"] = txtdescription.Text.Trim();
                  dr["TRN_QTY"] = 0;
                  dr["UOM"] = ddluom1.SelectedValue;
                  dr["UOM1"] = ddluom1.SelectedValue;
                  dr["UOM_BAIL"] = ddluom2.SelectedValue;
                  dr["BASIC_RATE"] = Math.Round(RATE, 3);
                  dr["FINAL_RATE"] = Math.Round(RATE, 3);
                  dr["AMOUNT"] = Math.Round(RATE * QTY, 3);
                  dr["COST_CENTER_CODE"] = "";
                  dr["MAC_CODE"] = string.Empty;
                  dr["REMARKS"] = "";
                  dr["QCFLAG"] = "No";
                  DateTime dd = System.DateTime.Now;
                  DateTime.TryParse(DateTime.MinValue.ToString(), out dd);
                  dr["DATE_OF_MFG"] = dd;
                  dr["NO_OF_UNIT"] = 0;// calculateNoOfUnit();
                  dr["WEIGHT_OF_UNIT"] = 0;// calculateWeightofunit();
                  dr["UOM_OF_UNIT"] = ddluom1.SelectedValue.Trim();
                  dr["PI_NO"] = "NA";
                  dr["PALLET_CODE"] = txtPalletCode.Text.ToUpper().Trim();
                  dr["LOT_NO"] = txtLotNo.Text.ToUpper().Trim();
                  dr["GRADE"] = txtGrade.Text.ToUpper().Trim();
                  dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                  dr["PRTY_NAME"] = txtPartyName.Text;
                  dtDetailTBL.Rows.Add(dr);
              
              }
                       

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                }

            
            ViewState["dtDetailTBL"] = dtDetailTBL;
            

        }


        catch (Exception)
        {

            throw;
        }
    }



    //**********************OPENING BAL TRN DATA BY NISHANT RAI*************************//

   public void BindIntial()
    {

        try
        {

            txtGrade.Text = "NA";
            txtLotNo.Text = "NA";
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
            txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
            bindUOM("UOM");
        }
        catch
        {

        }
    }


   public double  calculatiallTrnData()
   {

       double totalQty = 0;       
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
           totalQty += double.Parse(txtQTY.Text);  

       }
       txtOpeningBalanceStock.Text = totalQty.ToString();
       return totalQty;
   }
   public double calculatiallTrnData(DataTable dtDetailTBL,out double   RATE)
   {     
       RATE = 0;
       DataView dv = new DataView(dtDetailTBL);
       dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim().ToUpper() + "'  and LOT_NO='" + txtLotNo.Text.Trim().ToUpper() + "' AND  PALLET_CODE='" + txtPalletCode.Text.Trim().ToUpper() + "' AND PRTY_CODE='" + txtPartyCode.SelectedText + "'  AND ROW_STATE <> 'DELETE'";
             
       double totalQty = 0;
       double totalRate = 0;
       for (int i = 0; i < dv.Count; i++)
       {
         
           totalQty += double.Parse(dv[i]["TRN_QTY"].ToString());  
           totalRate += double.Parse(dv[i]["FINAL_RATE"].ToString())/dv.Count; 

       }
       RATE = totalRate;
      
       return totalQty;
   }
            

   public double calculateWeightofunit()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
           totalQty += double.Parse(txtQTY.Text);

       }
       return totalQty / grdsub_trn.Rows.Count;
   }

   public double calculateNoOfUnit()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
           totalQty += double.Parse(txtQTY.Text);

       }
       return totalQty;
   }

   public double calculateTotalPallet()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblCPNo") as Label;
           totalQty += 1;

       }
       return totalQty;
   }



    public void bindUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlUOM.Items.Clear();
                ddlUOM.DataSource = dt;
                ddlUOM.DataTextField = "MST_CODE";
                ddlUOM.DataValueField = "MST_CODE";
                ddlUOM.DataBind();
                ddlUOM.SelectedValue = "KG";
                //ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    private DataTable  CreateSUBTRNTable()
    {
        try
        {
            dtOP_TRN_SUB = new DataTable();
            dtOP_TRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtOP_TRN_SUB.Columns.Add("TRN_NUMB", typeof(int));
            dtOP_TRN_SUB.Columns.Add("TRN_TYPE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("FIBER_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtOP_TRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtOP_TRN_SUB.Columns.Add("GRADE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtOP_TRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtOP_TRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtOP_TRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtOP_TRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtOP_TRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtOP_TRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtOP_TRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PALLET_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PALLET_NO", typeof(string));
            dtOP_TRN_SUB.Columns.Add("ROW_STATE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("ISS_QTY", typeof(double));
            dtOP_TRN_SUB.Columns.Add("PRTY_CODE", typeof(string));
            dtOP_TRN_SUB.Columns.Add("PRTY_NAME", typeof(string));
            dtOP_TRN_SUB.Columns.Add("FINAL_RATE", typeof(double));   
            
            return dtOP_TRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void BindBOMGrid()
    {
        try
        {
            
            if (Session["dtOP_TRN_SUB"] == null)
                dtOP_TRN_SUB = CreateSUBTRNTable();
            else
                dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
            

            DataView dv = new DataView(dtOP_TRN_SUB);
            dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim() + "' AND ROW_STATE <> 'DELETE'";
            if (dv.Count > 0)
            {
                //BindIntial();
                txtQty.Text = string.Empty;
                grdsub_trn.DataSource = dv;
                grdsub_trn.DataBind();
            }
            else
            {
                grdsub_trn.DataSource = null;
                grdsub_trn.DataBind();
            
            }
            calculatiallTrnData();
        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMSave_Click(object sender, EventArgs e)
    {
        try
        {
            double QTY = 0;
            double ISS_QTY = 0;
            double.TryParse(txtQty.Text.Trim(), out QTY);         
            double.TryParse(lblIssueQty.Text.Trim(), out ISS_QTY);
            double finalRate = 0;
            double.TryParse(txtRate.Text, out finalRate);

            if (QTY > 0 && QTY>ISS_QTY)
            {

                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
                if (Session["dtOP_TRN_SUB"] == null)
                    dtOP_TRN_SUB = CreateSUBTRNTable();
                else
                    dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];

                if (dtOP_TRN_SUB.Rows.Count < 500)
                {

                    int UNIQUE_ID = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                    {
                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    }
                    bool bb = SearchInBOMgrid(txtLotNo.Text,txtPalletNo.Text,txtPartyCode.SelectedText , UNIQUE_ID);
                    if (!bb)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtOP_TRN_SUB);
                            dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim().ToUpper() + "'  and UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {

                                dv[0]["TRN_QTY"] = QTY;
                                dv[0]["PO_NUMB"] = 99999;
                                dv[0]["PO_TYPE"] = "OPB";
                                dv[0]["PO_COMP_CODE"] = "C99999";
                                dv[0]["PO_BRANCH"] = "B99999";
                                dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                                dv[0]["GRADE"] = txtGrade.Text.Trim().ToUpper();
                                dv[0]["LOT_NO"] = txtLotNo.Text.ToUpper();
                                dv[0]["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                                dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                                dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                                dv[0]["PI_NO"] = "NA";
                                dv[0]["PALLET_CODE"] = txtPalletCode.Text.ToUpper().Trim();
                                dv[0]["PALLET_NO"] = txtPalletNo.Text.ToUpper().Trim();
                                dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                                dv[0]["PRTY_NAME"] = txtPartyName.Text;
                                
                                dv[0]["FINAL_RATE"] = finalRate;
                                   
                                dtOP_TRN_SUB.AcceptChanges();
                            }
                        }
                        else
                        {


                            DataRow dr = dtOP_TRN_SUB.NewRow();
                            dr["UNIQUE_ID"] = dtOP_TRN_SUB.Rows.Count + 1;
                            dr["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();    
                            dr["TRN_QTY"] = QTY;
                            dr["PO_NUMB"] = 99999;
                            dr["PO_TYPE"] = "OPB";
                            dr["PO_COMP_CODE"] = "C99999";
                            dr["PO_BRANCH"] = "B99999";
                            dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dr["GRADE"] = txtGrade.Text.Trim().ToUpper();
                            dr["LOT_NO"] = txtLotNo.Text.Trim().ToUpper();
                            dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                            dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                            dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dr["PI_NO"] = "NA";
                            dr["PALLET_CODE"] = txtPalletCode.Text.ToUpper().Trim();
                            dr["PALLET_NO"] = txtPalletNo.Text.ToUpper().Trim();
                            dr["ROW_STATE"] = "INSERT";
                            dr["ISS_QTY"] = 0;
                            dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                            dr["PRTY_NAME"] = txtPartyName.Text;
                            dr["FINAL_RATE"] = finalRate;
                            dtOP_TRN_SUB.Rows.Add(dr);
                            Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;
                            creteTrnData();
                           
                        }
                        RefresBOMRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please Enter Another Lot No.This Already Added ');", true);
                    }
                    BindBOMGrid();


                }

                else
                {
                    Common.CommonFuction.ShowMessage("You have reached the limit of Sub Transaction. Only 15 Sub Transaction Allowed.");
                }
            }
            else 
            {
                Common.CommonFuction.ShowMessage("Lot qty must be greater then 0 OR qty could not be less then already issue qty.");
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Sub Transaction Detail Row.\r\nSee error log for detail."));

        }
    }

    private void RefresBOMRow()
    {
        try
        {

            txtQty.Text = string.Empty;            
            ddlMaterialStatus.SelectedIndex = -1;
            //txtGrade.Text = "NA";
            //txtLotNo.Text = "NA";
            txtDofMfd.Text = DateTime.Now.Date.ToString();
            ViewState["UNIQUE_ID"] = null;
            //txtPalletCode.Text = string.Empty;
            txtPalletNo.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }

    private void ClearAllBOMRow()
    {
        try
        {

            txtQty.Text = string.Empty;
            ddlMaterialStatus.SelectedIndex = -1;
            txtGrade.Text = "NA";
            txtLotNo.Text = "NA";
            txtDofMfd.Text = DateTime.Now.Date.ToString();
            ViewState["UNIQUE_ID"] = null;
            txtPalletCode.Text = string.Empty;
            txtPalletNo.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }
    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        ClearAllBOMRow();
    }

    protected void grdSub_trnArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUE_ID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BOMEdit")
            {
                FillBOMByGrid(UNIQUE_ID);
            }
            else if (e.CommandName == "BOMDelete")
            {
                DeleteBOMRow(UNIQUE_ID);
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sub Tran Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void FillBOMByGrid(int UNIQUE_ID)
    {
        try
        {
            if (Session["dtOP_TRN_SUB"] != null)
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
            DataView dv = new DataView(dtOP_TRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                //ddlW_Side.SelectedValue = dv[0]["W_SIDE"].ToString();
                txtQty.Text = dv[0]["TRN_QTY"].ToString();                
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtPalletNo.Text=dv[0]["PALLET_NO"].ToString();
                txtPalletCode.Text = dv[0]["PALLET_CODE"].ToString();
                lblIssueQty.Text = dv[0]["ISS_QTY"].ToString();
                txtRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtPartyName.Text = dv[0]["PRTY_NAME"].ToString();
                string CommandText1 = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME || PRTY_ADD1) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker')) ";
                string WhereClause1 = "  and  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                string SortExpression1 = " order by PRTY_CODE asc";
                string SearchQuery1 = "%";
                DataTable data1 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText1, WhereClause1, SortExpression1, "", SearchQuery1, "");
                txtPartyCode.DataSource = data1;
                txtPartyCode.DataTextField = "PRTY_CODE";
                txtPartyCode.DataValueField = "Address";
                txtPartyCode.DataBind();
                foreach (ComboBoxItem item in txtPartyCode.Items)
                {
                    if (item.Text ==dv[0]["PRTY_CODE"].ToString())
                    {
                        txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        break;
                    }
                }
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteBOMRow(int UNIQUE_ID)
    {
        try
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
            if (grdsub_trn.Rows.Count == 1)
            {
                //dtOP_TRN_SUB.Rows.Clear();
                dtOP_TRN_SUB.Rows[0].SetField("ROW_STATE","DELETE");
            }
            else
            {
                foreach (DataRow dr in dtOP_TRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        //dtOP_TRN_SUB.Rows.Remove(dr);  
                        dr.SetField("ROW_STATE", "DELETE");
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtOP_TRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            dtOP_TRN_SUB.AcceptChanges();
            Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchInBOMgrid(string LotNo,string palletno,string PRTY ,int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdsub_trn.Rows)
            {
                Label txtLotNo = (Label)grdRow.FindControl("lbtlotno");
                Label txtPalletNo = (Label)grdRow.FindControl("lblCPNo");
                Label txtParty = (Label)grdRow.FindControl("lblParty");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtLotNo.Text.ToUpper() == LotNo.ToUpper() && txtPalletNo.Text.ToUpper() == palletno.ToUpper() && txtParty.ToolTip.ToUpper() == PRTY.ToUpper() && UNIQUE_ID != iUNIQUE_ID)
                {
                    Result = true;
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }   
   
    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)),3).ToString();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (txtNoofUnit.Text != "")
        {
            txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)),3).ToString();
        }

    }
    protected void ddlLengthType_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtFiberDenier.Text = ddlLengthType.SelectedItem.ToString();
    } 


 


   // **********************************************************//


    protected void grdsub_trn_RowDataBound(object sender, GridViewRowEventArgs e)
    {  
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label issQty = (Label)e.Row.FindControl("txtIssueQty");
            Button btnDelete = (Button)e.Row.FindControl("lnkBOMDelete");
            double issueQty = 0;
            double.TryParse(issQty.Text, out issueQty);
            if (issueQty > 0)
            {
                btnDelete.Enabled = false;
            }
            else 
            {
                btnDelete.Enabled = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Literal totalQty = (Literal)e.Row.FindControl("totalQty");
            Literal totalPalletNo = (Literal)e.Row.FindControl("totalPalletNo");
            Literal totalNoOfUnit = (Literal)e.Row.FindControl("totalNoOfUnit");
            totalQty.Text = calculatiallTrnData().ToString();
            totalNoOfUnit.Text = calculateNoOfUnit().ToString();
            totalPalletNo.Text = calculateTotalPallet().ToString();
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Pages/FiberMasterQuery.aspx", false);
    }
    protected void rdIsExciable_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTariffHeading.Text = string.Empty;
        if (rdIsExciable.SelectedValue == "1")
        {
            txtTariffHeading.Enabled = true;
            txtTariffHeadingValidator.Enabled = true;
            ddlTariffHeading.Enabled = true;
            ddlTariffHeadingValidator.Enabled = true;
        }
        else
        {
            txtTariffHeading.Enabled = false;
            txtTariffHeadingValidator.Enabled = false;

            ddlTariffHeading.Enabled = false;
            ddlTariffHeadingValidator.Enabled = false;
        }

    }
}
