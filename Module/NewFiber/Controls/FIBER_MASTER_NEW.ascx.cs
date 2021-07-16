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
{    DataTable dtOP_TRN_SUB = null;
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
        imgbtnDelete.Visible = false;       
        txtFiberCode.Visible = true;
        DDLFiberCode.SelectedIndex = -1;
        DDLFiberCode.Visible = false;
        ddlfibercat.SelectedIndex = -1;
        ddlsubfiber_cat.SelectedIndex = -1;
        ddluom1.SelectedIndex = -1;
        ddluom2.SelectedIndex = -1;
        Txtuomperbail.Text = string.Empty;
        txtpolyster.Text = string.Empty;
        txtviscose.Text = string.Empty;
        ddlLengthType.SelectedIndex = -1;
        txtlengthvalue.Text = string.Empty;

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

        txtTanacity.Text = string.Empty;
        Txtremark.Text = string.Empty;

        Session["dtOP_TRN_SUB"] = null;
        grdsub_trn.DataSource = null;
        grdsub_trn.DataBind();
        
         BindIntial();
    }

    private void InsertData()
    {
        int iRecordFound = 0;
        try
        {
            if (Page.IsValid)
            {
                FiberNew_Master oFiberNew_Master = new FiberNew_Master();
                BindFibreCode();
                oFiberNew_Master.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFiberNew_Master.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFiberNew_Master.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFiberNew_Master.FIBER_CODE = CommonFuction.funFixQuotes(txtFiberCode.Text.Trim().ToUpper());
                oFiberNew_Master.FIBER_CAT = CommonFuction.funFixQuotes(ddlfibercat.SelectedValue.Trim());
                oFiberNew_Master.SUB_FIBER_CAT = CommonFuction.funFixQuotes(ddlsubfiber_cat.SelectedValue.Trim());
                oFiberNew_Master.UOM = CommonFuction.funFixQuotes(ddluom1.SelectedValue.Trim());

                oFiberNew_Master.UOM1 = CommonFuction.funFixQuotes(ddluom2.SelectedValue.Trim());
                oFiberNew_Master.UOM_BAIL = CommonFuction.funFixQuotes(Txtuomperbail.Text.Trim());
                oFiberNew_Master.POLYSTER = CommonFuction.funFixQuotes(txtpolyster.Text.Trim());
                oFiberNew_Master.VISCOSE = CommonFuction.funFixQuotes(txtviscose.Text.Trim());
                oFiberNew_Master.LENGTH_TYPE = CommonFuction.funFixQuotes(ddlLengthType.SelectedValue.Trim());
                oFiberNew_Master.LENGTH_VALUE = CommonFuction.funFixQuotes(txtlengthvalue.Text.Trim());

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //oFiberNew_Master.FINEE_FIBER = CommonFuction.funFixQuotes(txtfinness.Text.Trim());
                //oFiberNew_Master.MOISTURE = CommonFuction.funFixQuotes(txtmosture.Text.Trim());
                //oFiberNew_Master.END_USE = CommonFuction.funFixQuotes(txtenduse.Text.Trim());
                //oFiberNew_Master.FIBER_APPEARINCE = CommonFuction.funFixQuotes(txtfiberappearance.Text.Trim());
                //oFiberNew_Master.BIO_LOGIC_PROPERTY = CommonFuction.funFixQuotes(txtbio_property.Text.Trim());
                //oFiberNew_Master.FIBER_PROPERTY = CommonFuction.funFixQuotes(txtproperties.Text.Trim());
                oFiberNew_Master.FIBER_DESC = CommonFuction.funFixQuotes(txtdescription.Text.Trim());

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiberNew_Master.FIBER_LUSTURE = CommonFuction.funFixQuotes(ddlfiberlusture.SelectedValue.Trim());
                oFiberNew_Master.FIBER_DENIER  = CommonFuction.funFixQuotes(txtFiberDenier.Text.Trim());
                oFiberNew_Master.FANCY_EFFECT = CommonFuction.funFixQuotes(ddlFancyEffect.SelectedValue.Trim());
                oFiberNew_Master.FIBER_SUPPLIER = CommonFuction.funFixQuotes(txtPartyCodecmb.SelectedValue.Trim());
                oFiberNew_Master.FIBER_TENACITY  = CommonFuction.funFixQuotes(txtTanacity.Text.Trim());
                oFiberNew_Master.REMARK = CommonFuction.funFixQuotes(Txtremark.Text.Trim());

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiberNew_Master.OPEN_STOCK = double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiberNew_Master.MIN_STOCK = double.Parse(txtMimimumStock.Text.Trim());
                oFiberNew_Master.PROCURE_DAYS = int.Parse(txtMinimumProcureDays.Text.Trim());
                oFiberNew_Master.OPEN_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiberNew_Master.REORDER_LEVEL = txtRecorderLevel.Text.Trim();
                oFiberNew_Master.REORDER_QTY = double.Parse(txtRecorderQuantity.Text.Trim());
                oFiberNew_Master.MAXIMUM_STOCK = double.Parse(txtMaximumStock.Text.Trim());
                oFiberNew_Master.TUSER = oUserLoginDetail.UserCode;
                oFiberNew_Master.TDATE = System.DateTime.Now;
                oFiberNew_Master.STATUS = "1";

                oFiberNew_Master.PRTY_CODE = "SELF";
                oFiberNew_Master.OP_BAL_STOCK = double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiberNew_Master.OP_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiberNew_Master.OP_BAL_PRTY_STOK = double.Parse("0");
                oFiberNew_Master.OP_QTY_ADJ = double.Parse("0");
                oFiberNew_Master.FIB_ISS = "0";
                oFiberNew_Master.FIB_RCPT = "0";
                oFiberNew_Master.CUR_RATE = double.Parse("0");
                oFiberNew_Master.WT_AVRG_RATE = double.Parse("0");
                oFiberNew_Master.LAST_PO_RATE = double.Parse("0");
                oFiberNew_Master.IS_EXCISABLE = "0";
                if (rad_qc_req.SelectedValue.Trim() == "yes")
                    oFiberNew_Master.QC_REQUIRED = true;
                else
                    oFiberNew_Master.QC_REQUIRED = false;

                bool bResult = SaitexBL.Interface.Method.FiberNew_Master.InsertFiberMstData(oFiberNew_Master, out iRecordFound);
               bool trnResult= insertTrnData();
                      
                
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
                FiberNew_Master oFiberNew_Master = new FiberNew_Master();
                BindFibreCode();
                oFiberNew_Master.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oFiberNew_Master.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFiberNew_Master.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFiberNew_Master.FIBER_CODE = CommonFuction.funFixQuotes(txtFiberCode.Text.Trim().ToUpper());
                oFiberNew_Master.FIBER_CAT = CommonFuction.funFixQuotes(ddlfibercat.SelectedValue.Trim());
                oFiberNew_Master.SUB_FIBER_CAT = CommonFuction.funFixQuotes(ddlsubfiber_cat.SelectedValue.Trim());
                oFiberNew_Master.UOM = CommonFuction.funFixQuotes(ddluom1.SelectedValue.Trim());

                oFiberNew_Master.UOM1 = CommonFuction.funFixQuotes(ddluom2.SelectedValue.Trim());
                oFiberNew_Master.UOM_BAIL = CommonFuction.funFixQuotes(Txtuomperbail.Text.Trim());
                oFiberNew_Master.POLYSTER = CommonFuction.funFixQuotes(txtpolyster.Text.Trim());
                oFiberNew_Master.VISCOSE = CommonFuction.funFixQuotes(txtviscose.Text.Trim());
                oFiberNew_Master.LENGTH_TYPE = CommonFuction.funFixQuotes(ddlLengthType.SelectedValue.Trim());
                oFiberNew_Master.LENGTH_VALUE = CommonFuction.funFixQuotes(txtlengthvalue.Text.Trim());

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //oFiberNew_Master.FINEE_FIBER = CommonFuction.funFixQuotes(txtfinness.Text.Trim());
                //oFiberNew_Master.MOISTURE = CommonFuction.funFixQuotes(txtmosture.Text.Trim());
                //oFiberNew_Master.END_USE = CommonFuction.funFixQuotes(txtenduse.Text.Trim());
                //oFiberNew_Master.FIBER_APPEARINCE = CommonFuction.funFixQuotes(txtfiberappearance.Text.Trim());
                //oFiberNew_Master.BIO_LOGIC_PROPERTY = CommonFuction.funFixQuotes(txtbio_property.Text.Trim());
                //oFiberNew_Master.FIBER_PROPERTY = CommonFuction.funFixQuotes(txtproperties.Text.Trim());
                oFiberNew_Master.FIBER_DESC = CommonFuction.funFixQuotes(txtdescription.Text.Trim());

                //*************Commented By Nishant Rai at 26-07-2013*****************/

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiberNew_Master.FIBER_LUSTURE = CommonFuction.funFixQuotes(ddlfiberlusture.SelectedValue.Trim());
                oFiberNew_Master.FIBER_DENIER = CommonFuction.funFixQuotes(txtFiberDenier.Text.Trim());
                oFiberNew_Master.FANCY_EFFECT = CommonFuction.funFixQuotes(ddlFancyEffect.SelectedValue.Trim());
                oFiberNew_Master.FIBER_SUPPLIER = CommonFuction.funFixQuotes(txtPartyCodecmb.SelectedValue.Trim());
                oFiberNew_Master.FIBER_TENACITY = CommonFuction.funFixQuotes(txtTanacity.Text.Trim());
                oFiberNew_Master.REMARK = CommonFuction.funFixQuotes(Txtremark.Text.Trim());

                //****************** ADDED BY NISHANT RAI AT 27-07-2013*******************//

                oFiberNew_Master.OPEN_STOCK = double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiberNew_Master.MIN_STOCK = double.Parse(txtMimimumStock.Text.Trim());
                oFiberNew_Master.PROCURE_DAYS = int.Parse(txtMinimumProcureDays.Text.Trim());
                oFiberNew_Master.OPEN_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiberNew_Master.REORDER_LEVEL = txtRecorderLevel.Text.Trim();
                oFiberNew_Master.REORDER_QTY = double.Parse(txtRecorderQuantity.Text.Trim());
                oFiberNew_Master.MAXIMUM_STOCK = double.Parse(txtMaximumStock.Text.Trim());
                oFiberNew_Master.TUSER = oUserLoginDetail.UserCode;
                oFiberNew_Master.TDATE = System.DateTime.Now;
                oFiberNew_Master.STATUS = "1";

                oFiberNew_Master.PRTY_CODE = "SELF";
                oFiberNew_Master.OP_BAL_STOCK = double.Parse(txtOpeningBalanceStock.Text.Trim());
                oFiberNew_Master.OP_RATE = double.Parse(txtOpeningRate.Text.Trim());
                oFiberNew_Master.OP_BAL_PRTY_STOK = double.Parse("0");
                oFiberNew_Master.OP_QTY_ADJ = double.Parse("0");
                oFiberNew_Master.FIB_ISS = "0";
                oFiberNew_Master.FIB_RCPT = "0";
                oFiberNew_Master.CUR_RATE = double.Parse("0");
                oFiberNew_Master.WT_AVRG_RATE = double.Parse("0");
                oFiberNew_Master.LAST_PO_RATE = double.Parse("0");
                oFiberNew_Master.IS_EXCISABLE = "0";
                if (rad_qc_req.SelectedValue.Trim() == "yes")
                    oFiberNew_Master.QC_REQUIRED = true;
                else
                    oFiberNew_Master.QC_REQUIRED = false;
                bool bResult = SaitexBL.Interface.Method.FiberNew_Master.UpdateFiberMstData(oFiberNew_Master, out iRecordFound);
                bool trnResult = updateTrnData();
                if (bResult)
                {

                    Common.CommonFuction.ShowMessage("Record Update Successfully...");
                    InitialControls();
                    BindControls();
                    BindFibreCode();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Error While Update Record...");
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
        BindLengthType("LENGTH_TYPE");

        //*************ADDED By Nishant Rai at 26-07-2013*****************/

        BindFiberLusture("FIBER_LUSTURE");
        BindFancyEffect("FIBER_FANCY_EFFECT");

    }

    //private void BindFiberCode()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.FiberNew_Master.BindFiberCode();
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

            string CommandText = "SELECT DISTINCT LTRIM (RTRIM (FIBER_CODE)) AS FIBER_CODE , FIBER_DESC FROM TX_FIBER_NEW_MASTER ";
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
            DataTable dt = SaitexBL.Interface.Method.FiberNew_Master.BindFiberSubCat(fiber_cat);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlsubfiber_cat.Items.Clear();
                ddlsubfiber_cat.DataSource = dt;
                ddlsubfiber_cat.DataTextField = "FIBR_SUBCAT";
                ddlsubfiber_cat.DataValueField = "FIBR_SUBCAT";
                ddlsubfiber_cat.DataBind();
                ddlsubfiber_cat.Items.Insert(0, new ListItem("----Select---"));
            }
            else
            {
                ddlsubfiber_cat.Items.Clear();
                ddlsubfiber_cat.DataSource = null;               
                ddlsubfiber_cat.DataBind();
                ddlsubfiber_cat.Items.Insert(0, new ListItem("----Select---"));
                Common.CommonFuction.ShowMessage("Sub. Category is not Available by selected cat... Contact To Admin");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }   

    private void BindLengthType(string MST_NAME)
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
                ddluom2.SelectedValue = "BAIL";
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
            string PREFIX = SaitexBL.Interface.Method.FiberNew_Master.GetPrefixCode(FType);
            string FibleCode = SaitexBL.Interface.Method.FiberNew_Master.GetFabricCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE,FType.Trim(),PREFIX.ToUpper());
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
            
            DataTable dt = SaitexBL.Interface.Method.FiberNew_Master.GetDataByFiberCode(iFIBER_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFiberCode.Text = dt.Rows[0]["FIBER_CODE"].ToString().Trim();
                ddlfibercat.SelectedItem.Text = dt.Rows[0]["FIBER_CAT"].ToString().Trim();
                BindFiberSubCat();
                ddlsubfiber_cat.SelectedItem.Text = dt.Rows[0]["SUB_FIBER_CAT"].ToString().Trim();
                ddluom1.SelectedItem.Text = dt.Rows[0]["UOM"].ToString().Trim();
                ddluom2.SelectedItem.Text = dt.Rows[0]["UOM1"].ToString().Trim();
                ddlLengthType.SelectedItem.Text = dt.Rows[0]["LENGTH_TYPE"].ToString().Trim();
                txtlengthvalue.Text = dt.Rows[0]["LENGTH_VALUE"].ToString().Trim();
                Txtuomperbail.Text = dt.Rows[0]["UOM_BAIL"].ToString().Trim();
                txtpolyster.Text = dt.Rows[0]["POLYSTER"].ToString().Trim();
                txtviscose.Text = dt.Rows[0]["VISCOSE"].ToString().Trim();
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

                SaitexDM.Common.DataModel.TX_FIBER_NEW_IR_MST oTX_FIBER_NEW_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_NEW_IR_MST();
                oTX_FIBER_NEW_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_FIBER_NEW_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FIBER_NEW_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FIBER_NEW_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oTX_FIBER_NEW_IR_MST.TRN_TYPE = "OPB01";
                int TRNNUMBer = 0;
                string trnnumb = SaitexDL.Interface.Method.TX_FIBER_NEW_IR_MST.GetMRNNumber(oTX_FIBER_NEW_IR_MST, txtFiberCode.Text).ToString();
                if (!string.IsNullOrEmpty(trnnumb))
                {
                     TRNNUMBer = int.Parse(trnnumb);
                }
                else 
                {
                    TRNNUMBer = int.Parse(SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetNewMRNNumber(oTX_FIBER_NEW_IR_MST).ToString());
                }


                Session["dtOP_TRN_SUB"] = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRNNUMBer, "OPB01");
               
                MapTrnDataTable();
                  if (Session["dtOP_TRN_SUB"] != null)
                   {
                     DataTable dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
                     if (dtOP_TRN_SUB.Rows.Count > 0)
                     {
                     grdsub_trn.DataSource = dtOP_TRN_SUB;
                     grdsub_trn.DataBind();
                     }

                   }





               
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
            dt = SaitexBL.Interface.Method.FiberNew_Master.CheckDuplicateFiberCode(fib_code, comp_code, branch_code);
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

            //txtPartyCodecmb.DataSource = data;
            //txtPartyCodecmb.DataBind();
            bindPartyCode1(e.Text.ToUpper(), e.ItemsOffset);
   

           // e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
          //  e.ItemsCount = GetPartyCount(e.Text);
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
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }


    //************************* Added By Nishant Rai at 26-07-2013 ****************************//

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
        SaitexDM.Common.DataModel.TX_FIBER_NEW_IR_MST oTX_FIBER_NEW_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_NEW_IR_MST();
        oTX_FIBER_NEW_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FIBER_NEW_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FIBER_NEW_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FIBER_NEW_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
        oTX_FIBER_NEW_IR_MST.FORM_NUMB = "";
        oTX_FIBER_NEW_IR_MST.FORM_TYPE = "";

        DateTime dt = System.DateTime.Now.Date;
        bool Is_Gate_Entry = false;
        Is_Gate_Entry = DateTime.TryParse(DateTime.MinValue.ToString().Trim(), out dt);
        htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FIBER_NEW_IR_MST.GATE_DATE = dt;
        oTX_FIBER_NEW_IR_MST.GATE_NUMB = "";
        oTX_FIBER_NEW_IR_MST.GATE_OUT_NUMB = "";
        oTX_FIBER_NEW_IR_MST.GATE_PASS_TYPE = "";
        oTX_FIBER_NEW_IR_MST.LORY_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        Is_LR = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("LR", Is_LR);
        oTX_FIBER_NEW_IR_MST.LR_DATE = dt;

        oTX_FIBER_NEW_IR_MST.LR_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FIBER_NEW_IR_MST.PRTY_CH_DATE = dt;


        oTX_FIBER_NEW_IR_MST.PRTY_CH_NUMB ="";
        oTX_FIBER_NEW_IR_MST.PRTY_CODE = "";
        oTX_FIBER_NEW_IR_MST.PRTY_NAME = "";
        oTX_FIBER_NEW_IR_MST.RCOMMENT ="";
        oTX_FIBER_NEW_IR_MST.REPROCESS ="";
        oTX_FIBER_NEW_IR_MST.SHIFT = "";


        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(DateTime.Now.ToString() , out dt);
        htReceive.Add("MRN", Is_MRN);
        oTX_FIBER_NEW_IR_MST.TRN_DATE = dt;
        oTX_FIBER_NEW_IR_MST.TRN_TYPE = "OPB01";
        oTX_FIBER_NEW_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetNewMRNNumber(oTX_FIBER_NEW_IR_MST).ToString());
        
        oTX_FIBER_NEW_IR_MST.TRSP_CODE = "";

        oTX_FIBER_NEW_IR_MST.TUSER = oUserLoginDetail.UserCode;
        int TRN_NUMB = 0;

        oTX_FIBER_NEW_IR_MST.BILL_NUMB ="";


        dt = System.DateTime.Now.Date;
        bool Is_Bill_Date = false;
        Is_Bill_Date = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("BILL_DATE", Is_Bill_Date);
        oTX_FIBER_NEW_IR_MST.BILL_DATE = dt;


        oTX_FIBER_NEW_IR_MST.BILL_TYPE = "FSP";
        oTX_FIBER_NEW_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


        double totalAmt = 0;
        double.TryParse(txtOpeningRate.Text, out totalAmt);
        oTX_FIBER_NEW_IR_MST.TOTAL_AMOUNT = totalAmt;

        double finalAmt = 0;
        double.TryParse(txtOpeningRate.Text, out finalAmt);
        oTX_FIBER_NEW_IR_MST.TOTAL_AMOUNT = finalAmt;

        DataTable dtOP_TRN_SUB=null;
        if (Session["dtOP_TRN_SUB"] != null)
        {
             dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
        }
        creteTrnData();
        DataTable dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        bool Result_TRN = SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.Insert(oTX_FIBER_NEW_IR_MST, out TRN_NUMB, dtDetailTBL, htReceive, dtOP_TRN_SUB, new DataTable(), new DataTable());
         return Result_TRN;     

    }

    public bool updateTrnData()
    {

        Hashtable htReceive = new Hashtable();
        SaitexDM.Common.DataModel.TX_FIBER_NEW_IR_MST oTX_FIBER_NEW_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_NEW_IR_MST();
        oTX_FIBER_NEW_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
        oTX_FIBER_NEW_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        oTX_FIBER_NEW_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
        oTX_FIBER_NEW_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
        oTX_FIBER_NEW_IR_MST.FORM_NUMB = "";
        oTX_FIBER_NEW_IR_MST.FORM_TYPE = "";

        DateTime dt = System.DateTime.Now.Date;
        bool Is_Gate_Entry = false;
        Is_Gate_Entry = DateTime.TryParse(DateTime.MinValue.ToString().Trim(), out dt);
        htReceive.Add("GATE_ENTRY", Is_Gate_Entry);
        oTX_FIBER_NEW_IR_MST.GATE_DATE = dt;
        oTX_FIBER_NEW_IR_MST.GATE_NUMB = "";
        oTX_FIBER_NEW_IR_MST.GATE_OUT_NUMB = "";
        oTX_FIBER_NEW_IR_MST.GATE_PASS_TYPE = "";
        oTX_FIBER_NEW_IR_MST.LORY_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_LR = false;
        Is_LR = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("LR", Is_LR);
        oTX_FIBER_NEW_IR_MST.LR_DATE = dt;

        oTX_FIBER_NEW_IR_MST.LR_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Party_challan = false;
        Is_Party_challan = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("PARTY_CHALLAN", Is_Party_challan);
        oTX_FIBER_NEW_IR_MST.PRTY_CH_DATE = dt;


        oTX_FIBER_NEW_IR_MST.PRTY_CH_NUMB = "";
        oTX_FIBER_NEW_IR_MST.PRTY_CODE = "";
        oTX_FIBER_NEW_IR_MST.PRTY_NAME = "";
        oTX_FIBER_NEW_IR_MST.RCOMMENT = "";
        oTX_FIBER_NEW_IR_MST.REPROCESS = "";
        oTX_FIBER_NEW_IR_MST.SHIFT = "";


        dt = System.DateTime.Now.Date;
        bool Is_MRN = false;
        Is_MRN = DateTime.TryParse(DateTime.Now.ToString(), out dt);
        htReceive.Add("MRN", Is_MRN);
        oTX_FIBER_NEW_IR_MST.TRN_DATE = dt;
        oTX_FIBER_NEW_IR_MST.TRN_TYPE = "OPB01";

        string trnnumb = SaitexDL.Interface.Method.TX_FIBER_NEW_IR_MST.GetMRNNumber(oTX_FIBER_NEW_IR_MST, txtFiberCode.Text).ToString();
        if (!string.IsNullOrEmpty(trnnumb))
        {
            oTX_FIBER_NEW_IR_MST.TRN_NUMB = int.Parse(trnnumb);
        }
        else
        {
            oTX_FIBER_NEW_IR_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.GetNewMRNNumber(oTX_FIBER_NEW_IR_MST).ToString());
        }
        oTX_FIBER_NEW_IR_MST.TRSP_CODE = "";

        oTX_FIBER_NEW_IR_MST.TUSER = oUserLoginDetail.UserCode;
       
        oTX_FIBER_NEW_IR_MST.BILL_NUMB = "";


        dt = System.DateTime.Now.Date;
        bool Is_Bill_Date = false;
        Is_Bill_Date = DateTime.TryParse(DateTime.MinValue.ToString(), out dt);
        htReceive.Add("BILL_DATE", Is_Bill_Date);
        oTX_FIBER_NEW_IR_MST.BILL_DATE = dt;


        oTX_FIBER_NEW_IR_MST.BILL_TYPE = "FSP";
        oTX_FIBER_NEW_IR_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;


        double totalAmt = 0;
        double.TryParse(txtOpeningRate.Text, out totalAmt);
        oTX_FIBER_NEW_IR_MST.TOTAL_AMOUNT = totalAmt;

        double finalAmt = 0;
        double.TryParse(txtOpeningRate.Text, out finalAmt);
        oTX_FIBER_NEW_IR_MST.TOTAL_AMOUNT = finalAmt;

        DataTable dtOP_TRN_SUB = null;
        if (Session["dtOP_TRN_SUB"] != null)
        {
            dtOP_TRN_SUB = (DataTable)Session["dtOP_TRN_SUB"];
        }
        creteTrnData();
        DataTable dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
        return SaitexBL.Interface.Method.TX_FIBER_NEW_IR_MST.Update(oTX_FIBER_NEW_IR_MST, dtDetailTBL, htReceive, dtOP_TRN_SUB, new DataTable(), new DataTable());
              

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
            ViewState["dtDetailTBL"] = null;
           DataTable dtDetailTBL = CreateDataTable();            
            if (txtFiberCode.Text != "" && txtOpeningRate.Text != "" && txtOpeningBalanceStock.Text != "" )
            {
              
               
              
                            DataRow dr = dtDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                            dr["PO_NUMB"] = 99999;
                            dr["PO_TYPE"] = "OPB";
                            dr["PO_COMP_CODE"] = "C99999";
                            dr["PO_BRANCH"] = "B99999";
                            dr["FIBER_CODE"] = txtFiberCode.Text.Trim().ToUpper();
                            dr["FIBER_DESC"] = txtdescription.Text.Trim();
                            dr["TRN_QTY"] = Math.Round(double.Parse(txtOpeningBalanceStock.Text.Trim()), 3);
                            dr["UOM"] = ddluom1.SelectedValue ;
                            dr["UOM1"] = ddluom1.SelectedValue;
                            dr["UOM_BAIL"] = ddluom2.SelectedValue;
                            dr["BASIC_RATE"] = Math.Round(double.Parse(txtOpeningRate.Text.Trim()), 3);
                            dr["FINAL_RATE"] = Math.Round(double.Parse(txtOpeningRate.Text.Trim()), 3);
                            dr["AMOUNT"] = Math.Round(double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtOpeningBalanceStock.Text.Trim()), 3);
                            dr["COST_CENTER_CODE"] = "";
                            dr["MAC_CODE"] = string.Empty;
                            dr["REMARKS"] = "";
                            dr["QCFLAG"] = "No";
                            DateTime dd = System.DateTime.Now;
                            DateTime.TryParse(DateTime.MinValue.ToString(), out dd);
                            dr["DATE_OF_MFG"] = dd;
                            dr["NO_OF_UNIT"] =calculateNoOfUnit();
                            dr["WEIGHT_OF_UNIT"] = calculateWeightofunit();
                            dr["UOM_OF_UNIT"] = ddluom1.SelectedValue.Trim();
                            dr["PI_NO"] = "NA";
                            dtDetailTBL.Rows.Add(dr);
                       

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


   public void calculatiallTrnData()
   {

       double totalQty = 0;       
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
           totalQty += double.Parse(txtQTY.Text);  

       }
       txtOpeningBalanceStock.Text = totalQty.ToString();
   }

   public double  calculateWeightofunit()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
           totalQty += double.Parse(txtQTY.Text);

       }
       return totalQty / grdsub_trn.Rows.Count;
   }

   public double  calculateNoOfUnit()
   {

       double totalQty = 0;
       for (int i = 0; i < grdsub_trn.Rows.Count; i++)
       {
           Label txtQTY = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
           totalQty += double.Parse(txtQTY.Text);

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
            dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim() + "'";
            if (dv.Count > 0)
            {
                BindIntial();
                txtQty.Text = string.Empty;
                grdsub_trn.DataSource = dv;
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
            double QTY = 0f;
            double.TryParse(txtQty.Text.Trim(), out QTY);

            if (QTY > 0)
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
                    bool bb = SearchInBOMgrid(txtLotNo.Text, UNIQUE_ID);
                    if (!bb)
                    {
                        if (UNIQUE_ID > 0)
                        {
                            DataView dv = new DataView(dtOP_TRN_SUB);
                            dv.RowFilter = "FIBER_CODE='" + txtFiberCode.Text.Trim() + "'  and UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {

                                dv[0]["TRN_QTY"] = QTY;
                                dv[0]["PO_NUMB"] = 99999;
                                dv[0]["PO_TYPE"] = "OPB";
                                dv[0]["PO_COMP_CODE"] = "C99999";
                                dv[0]["PO_BRANCH"] = "B99999";
                                dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                                dv[0]["GRADE"] = txtGrade.Text.Trim();
                                dv[0]["LOT_NO"] = txtLotNo.Text.Trim();
                                dv[0]["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                                dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                                dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                                dv[0]["PI_NO"] = "NA";

                                dtOP_TRN_SUB.AcceptChanges();
                            }
                        }
                        else
                        {


                            DataRow dr = dtOP_TRN_SUB.NewRow();
                            dr["UNIQUE_ID"] = dtOP_TRN_SUB.Rows.Count + 1;
                            dr["FIBER_CODE"] = txtFiberCode.Text.Trim();    
                            dr["TRN_QTY"] = QTY;
                            dr["PO_NUMB"] = 99999;
                            dr["PO_TYPE"] = "OPB";
                            dr["PO_COMP_CODE"] = "C99999";
                            dr["PO_BRANCH"] = "B99999";
                            dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dr["GRADE"] = txtGrade.Text.Trim();
                            dr["LOT_NO"] = txtLotNo.Text.Trim();
                            dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                            dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                            dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dr["PI_NO"] = "NA";
                            dtOP_TRN_SUB.Rows.Add(dr);
                            Session["dtOP_TRN_SUB"] = dtOP_TRN_SUB;

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
                Common.CommonFuction.ShowMessage("Lot qty must be greater then 0.");
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
            txtGrade.Text = "NA";
            txtLotNo.Text = "NA";
            txtDofMfd.Text = DateTime.Now.Date.ToString();
            ViewState["UNIQUE_ID"] = null;

        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        RefresBOMRow();
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
                dtOP_TRN_SUB.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtOP_TRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtOP_TRN_SUB.Rows.Remove(dr);
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

    private bool SearchInBOMgrid(string LotNo, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdsub_trn.Rows)
            {
                Label txtLotNo = (Label)grdRow.FindControl("lbtlotno");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtLotNo.Text == LotNo && UNIQUE_ID != iUNIQUE_ID)
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


    protected void imgbtnList_Click(object sender, EventArgs e)
    {
        try {
            Response.Redirect("FiberMasterQuery.aspx",false);

        }
        catch (Exception ex)
        { throw ex;
        }
      
    }















   // **********************************************************//

}
