using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Common;
using errorLog;
using System.IO;
using DBLibrary;
using Obout.Grid;

public partial class Inventory_ItemMaster : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();



    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Session["urLoginId"] != null)
            {
                this.txtItemCode.Text = Request[this.txtItemCode.UniqueID];

                if (!IsPostBack)
                {
                    BlanksControls();
                }
            }
            else
            {
                Response.Redirect("~/default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page Loading.\r\nSee error log for detail."));
        }
    }
    private void getDepartment()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            ddlDepartment.DataSource = dt;
            ddlDepartment.DataValueField = "DEPT_CODE";
            ddlDepartment.DataTextField = "DEPT_NAME";
            ddlDepartment.DataBind();
            //ddlDepartment.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Department.\r\nSee error log for detail."));
        }
    }
    private void Insertdata()
    {
        try
        {
            if (txtItemCode.Text != "")
            {
                if (txtHSNCODE.Text != "")
                {
                    if (ddlUOM.SelectedIndex >= 0)
                    {
                        int iRecordFound = 0;

                        SaitexDM.Common.DataModel.ItemMaster oItemMaster = new SaitexDM.Common.DataModel.ItemMaster();
                        oItemMaster.ITEM_CODE = CommonFuction.funFixQuotes(txtItemCode.Text.ToUpper().Trim());
                        oItemMaster.HSN_CODE = txtHSNCODE.Text.Trim();
                        oItemMaster.CAT_CODE = CommonFuction.funFixQuotes(ddlItemCategory.SelectedValue.Trim().Replace("&amp;", "&"));
                        oItemMaster.ITEM_TYPE = CommonFuction.funFixQuotes(ddlItemType.SelectedValue.Trim().Replace("&amp;", "&"));
                        oItemMaster.ITEM_DESC = CommonFuction.funFixQuotes(txtItemDesc.Text.ToUpper().Trim());
                        oItemMaster.REMARKS = CommonFuction.funFixQuotes(txtremarks.Text.Trim());
                        oItemMaster.ITEM_MAKE = CommonFuction.funFixQuotes(ddlItemMake.SelectedValue.Trim());
                        oItemMaster.UOM = CommonFuction.funFixQuotes(ddlUOM.SelectedValue.Trim());
                        oItemMaster.ASOC_ITEM_CODE = CommonFuction.funFixQuotes(txtAsocItemCode.SelectedValue.Trim());
                        oItemMaster.DEPARTMENT = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());
                        oItemMaster.LOCATION = oUserLoginDetail.VC_BRANCHNAME;
                        oItemMaster.STORE = CommonFuction.funFixQuotes(ddlDepartment.SelectedItem.ToString().Trim());


                        oItemMaster.DEPT_CODE = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());
                        oItemMaster.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                        oItemMaster.RACK_CODE = CommonFuction.funFixQuotes(txtrackCode.Text.Trim());

                        int opStock = 0;
                        int.TryParse(CommonFuction.funFixQuotes(txtOpBalStock.Text.Trim()), out opStock);
                        oItemMaster.OP_BAL_STOCK = opStock.ToString().Trim();

                        double opRate = 0;
                        double.TryParse(CommonFuction.funFixQuotes(txtOpRate.Text.Trim()), out opRate);
                        oItemMaster.OP_RATE = opRate.ToString();

                        double Minlvl = 0;
                        double.TryParse(CommonFuction.funFixQuotes(txtMinStockLevel.Text.Trim()), out Minlvl);
                        oItemMaster.MIN_STOCK_LVL = Minlvl;

                        int rQty = 0;
                        int.TryParse(CommonFuction.funFixQuotes(txtReorderQt.Text.Trim()), out rQty);
                        oItemMaster.REORDER_QTY = rQty;

                        int rLvl = 0;
                        int.TryParse(CommonFuction.funFixQuotes(txtReorderLevel.Text.Trim()), out rLvl);
                        oItemMaster.REORDER_LVL = rLvl;

                        int iProcure = 0;
                        int.TryParse(CommonFuction.funFixQuotes(txtMinProcDay.Text.Trim()), out iProcure);
                        oItemMaster.MIN_PROCURE_DAYS = iProcure;

                        int iExpirydays = 0;
                        int.TryParse(CommonFuction.funFixQuotes(txtExpDay.Text.Trim()), out iExpirydays);
                        oItemMaster.EXPIRY_DAYS = iExpirydays;

                        oItemMaster.TUSER = oUserLoginDetail.UserCode;

                        oItemMaster.TDATE = System.DateTime.Now;
                        if (ddlItemStatus.SelectedValue.Trim() == "Open")
                            oItemMaster.ITEM_STATUS = true;
                        else
                            oItemMaster.ITEM_STATUS = false;

                        if (rad_qc_req.SelectedValue.Trim() == "yes")
                            oItemMaster.QC_REQUIRED = true;
                        else
                            oItemMaster.QC_REQUIRED = false;

                        double Maxlvl = 0;
                        double.TryParse(CommonFuction.funFixQuotes(txtMaxStockLevel0.Text.Trim()), out Maxlvl);

                        oItemMaster.MAX_STK_LVL = Maxlvl;
                        if (ddlConsumble.SelectedItem.Text == "Consumable")
                        {
                            oItemMaster.CONSUME = true;
                        }
                        else
                        {
                            oItemMaster.CONSUME = false;
                        }

                        if (rdAuto.Checked)
                            oItemMaster.CODE_TYPE = rdAuto.Text;
                        else
                            oItemMaster.CODE_TYPE = rdManual.Text;



                        Int64 _tariff_heading = 0;
                        Int64 _sales_itchs_code = 0;
                        Int64 _custom_itchs_code = 0;
                        Int64.TryParse(txtTariffHeading.Text, out _tariff_heading);
                        Int64.TryParse(txtSales_ITCHS.Text, out _sales_itchs_code);
                        Int64.TryParse(txtCustom_ITCHS.Text, out _custom_itchs_code);
                        oItemMaster.IS_EXCISABLE = rdIsExciable.SelectedValue;
                        oItemMaster.TARIFF_HEADING = _tariff_heading;
                        oItemMaster.SALES_ITCHS_CODE = _sales_itchs_code;
                        oItemMaster.CUSTOM_ITCHS_CODE = _custom_itchs_code;
                        oItemMaster.ITEM_SIZE = txtItemSize.Text;
                        oItemMaster.WEIGHT = txtWeight.Text;
                        oItemMaster.IS_MOVABLE = ddlMovable.SelectedValue;
                        oItemMaster.OTHER_NO = txtother.Text.Trim();
                        oItemMaster.ITEM_DROWING = txtdrowing.Text.Trim();
                        oItemMaster.CATALOUGE = txtcatalog.Text.Trim();
                        oItemMaster.PART_NO = txtpartno.Text.Trim();
                        oItemMaster.SERIAL_NO = txtserial.Text.Trim();
                        oItemMaster.PARTY_CODE = txtPartyCode.SelectedValue.Trim();
                        oItemMaster.PARTY_NAME = txtPartyName.Text.Trim();
                        oItemMaster.ITEM_SUB_CAT = ddlsubcategory.SelectedValue.Trim();
                        oItemMaster.BRANCH_UNIT = ddlbranchUnit.SelectedValue.Trim();
                        oItemMaster.RACK_CODE1 = CommonFuction.funFixQuotes(txtrackCode2.Text.Trim());
                        oItemMaster.RACK_CODE2 = CommonFuction.funFixQuotes(txtrackCode3.Text.Trim());
                        oItemMaster.IMG_PATH = lblPath.Text.ToString();
                        bool bResult = SaitexBL.Interface.Method.ItemMaster.InsertItemMaster(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oItemMaster, out iRecordFound);

                        if (bResult)
                        {
                            BlanksControls();
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Item Saved Successfully');", true);

                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('This Item already saved');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Select UOM.');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Provide HSN Code');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Provide Item Code');", true);

            }
        }
        catch
        { throw; }
    }

    private void BlanksControls()
    {
        try
        {
            txtItemCode.Visible = true;
            txtItemCode.Text = "";
            txtItemDesc.Text = "";
            ddlItemCode.SelectedIndex = -1;
            ddlItemCode.SelectedValue = "";
            ddlItemCode.SelectedText = "";
            ddlItemCode.Items.Clear();
            ddlItemCode.Visible = false;
            ddlItemCategory.SelectedIndex = -1;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            lblMode.Text = "Save";

            bindItemCategory("");
            bindItemType("");
            bindUOM("");
            bindMake("");
            bindItemSubCategory("");
            ddlItemType.SelectedText = "";
            ddlUOM.SelectedText = "";
            // ddlItemCategory.SelectedText = "";
            // ddlItemCategory.Items.Clear();
            //ddlItemCategory.SelectedIndex = -1;
            ddlItemType.Items.Clear();
            ddlUOM.Items.Clear();
            //ddlItemCategory.DataSource = null;
            //ddlItemCategory.DataBind();
            //ddlItemType.DataSource = null;
            //ddlItemType.DataBind();
            //ddlUOM.DataSource = null;
            //ddlUOM.DataBind();
            ddlUOM.SelectedIndex = -1;
            ddlItemType.SelectedIndex = -1;
            //ddlItemCategory.EmptyText = "Select Item Category";
            //ddlItemType.EmptyText = "Select Item Type";
            //ddlUOM.EmptyText = "Select UOM";

            txtremarks.Text = "";
            ddlItemMake.SelectedIndex = -1;
            txtAsocItemCode.SelectedText = "";
            txtOpRate.Text = "";
            txtOpBalStock.Text = "0";
            txtrackCode.Text = "";
            txtMinStockLevel.Text = "";
            txtReorderQt.Text = "";
            txtReorderLevel.Text = "";
            txtMinProcDay.Text = "";
            txtExpDay.Text = "";
            txtMaxStockLevel0.Text = "";

            getDepartment();
            ddlDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
            bindItemCategory("");
            ddlItemCategory.SelectedValue = "ADMIN";
            var cat = ddlItemCategory.SelectedValue;
            txtItemCode.Text = string.Empty;


            txtTariffHeading.Text = string.Empty;
            txtSales_ITCHS.Text = string.Empty;
            txtCustom_ITCHS.Text = string.Empty;
            txtHSNCODE.Text = string.Empty;
            txtItemSize.Text = string.Empty;
            txtWeight.Text = string.Empty;
            ddlMovable.SelectedIndex = 0;
            rdIsExciable.SelectedValue = "1";
            txtother.Text = string.Empty;
            txtdrowing.Text = string.Empty;
            txtcatalog.Text = string.Empty;
            txtpartno.Text = string.Empty;
            txtserial.Text = string.Empty;
            txtPartyCode.SelectedIndex = -1;
            txtPartyName.Text = string.Empty;
            ddlsubcategory.SelectedIndex = -1;
            ddlbranchUnit.SelectedIndex = 0;
            txtrackCode2.Text = string.Empty;
            txtrackCode3.Text = string.Empty;
            lblPath.Text = string.Empty;
            if (ItemImage.ImageUrl != null)
            {
                ItemImage.ImageUrl = "~/APP_IMAGES/No_Image.jpg";
            }
        }
        catch
        {
            throw;
        }

    }

    private string GetNewItemCode()
    {

        string sNewItemCode = string.Empty;
        try
        {
            string sItemCategory = ddlItemCategory.SelectedText.Trim();
            string PrefixString = string.Empty;
            if (sItemCategory.Length > 2)
            {
                PrefixString = sItemCategory.Substring(0, 3);

                DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetNewId(PrefixString.ToUpper(), rdAuto.Text);

                double NewItmCode = 0;

                if (dt != null && dt.Rows.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dt.Rows.Count; iLoop++)
                    {
                        double sTempCode = Convert.ToDouble(dt.Rows[iLoop]["TItem_Code"].ToString());
                        if (sTempCode > NewItmCode)
                            NewItmCode = sTempCode;
                    }
                }

                double iNewItmCode = NewItmCode + 1;
                if (Convert.ToInt64(iNewItmCode) < 10)
                {
                    sNewItemCode = PrefixString + "0100" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 100)
                {
                    sNewItemCode = PrefixString + "010" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 1000)
                {
                    sNewItemCode = PrefixString + "01" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 10000)
                {
                    sNewItemCode = PrefixString + "0" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 100000)
                {
                    sNewItemCode = PrefixString + "" + iNewItmCode.ToString();
                }
            }
            return sNewItemCode;
        }
        catch
        {
            return sNewItemCode;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["ITEMCODE"] != null)
            {
                txtItemCode.Text = ViewState["ITEMCODE"].ToString();
            }
            if (txtItemCode.Text.Trim() != "")
            {
                if (txtHSNCODE.Text != "")
                {
                    UpdateData();
                    ViewState["ITEMCODE"] = null;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Enter HSN Code to update');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('First Enter ItemCode to update');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in updation.\r\nSee error log for detail."));
        }
    }

    private void UpdateData()
    {
        try
        {
            if (CheckForDuplicacy(txtItemCode.Text.Trim()))
            {
                if (ddlUOM.SelectedIndex >= 0 || ddlUOM.SelectedValue.Trim() != "")
                {
                    int iRecordFound = 0;

                    SaitexDM.Common.DataModel.ItemMaster oItemMaster = new SaitexDM.Common.DataModel.ItemMaster();

                    oItemMaster.ITEM_CODE = CommonFuction.funFixQuotes(txtItemCode.Text.Trim());
                    oItemMaster.HSN_CODE = txtHSNCODE.Text.Trim();
                    oItemMaster.CAT_CODE = CommonFuction.funFixQuotes(ddlItemCategory.SelectedValue.Trim());
                    oItemMaster.ITEM_TYPE = CommonFuction.funFixQuotes(ddlItemType.SelectedValue.Trim());
                    oItemMaster.ITEM_DESC = CommonFuction.funFixQuotes(txtItemDesc.Text.ToUpper().Trim());
                    oItemMaster.REMARKS = CommonFuction.funFixQuotes(txtremarks.Text.Trim());
                    oItemMaster.ITEM_MAKE = CommonFuction.funFixQuotes(ddlItemMake.SelectedValue.Trim());
                    oItemMaster.UOM = CommonFuction.funFixQuotes(ddlUOM.SelectedValue.Trim());
                    oItemMaster.ASOC_ITEM_CODE = CommonFuction.funFixQuotes(txtAsocItemCode.SelectedValue.Trim());
                    oItemMaster.DEPARTMENT = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());
                    oItemMaster.LOCATION = oUserLoginDetail.VC_BRANCHNAME;
                    oItemMaster.STORE = CommonFuction.funFixQuotes(ddlDepartment.SelectedItem.ToString().Trim());
                    oItemMaster.DEPT_CODE = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());
                    oItemMaster.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oItemMaster.RACK_CODE = CommonFuction.funFixQuotes(txtrackCode.Text.Trim());


                    int opStock = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtOpBalStock.Text.Trim()), out opStock);
                    oItemMaster.OP_BAL_STOCK = opStock.ToString().Trim();

                    double opRate = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtOpRate.Text.Trim()), out opRate);
                    oItemMaster.OP_RATE = opRate.ToString();

                    double Minlvl = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtMinStockLevel.Text.Trim()), out Minlvl);
                    oItemMaster.MIN_STOCK_LVL = Minlvl;

                    int rQty = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtReorderQt.Text.Trim()), out rQty);
                    oItemMaster.REORDER_QTY = rQty;

                    int rLvl = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtReorderLevel.Text.Trim()), out rLvl);
                    oItemMaster.REORDER_LVL = rLvl;

                    int iProcure = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtMinProcDay.Text.Trim()), out iProcure);
                    oItemMaster.MIN_PROCURE_DAYS = iProcure;

                    int iExpirydays = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtExpDay.Text.Trim()), out iExpirydays);
                    oItemMaster.EXPIRY_DAYS = iExpirydays;

                    oItemMaster.TUSER = oUserLoginDetail.UserCode;
                    oItemMaster.TDATE = System.DateTime.Now;

                    if (ddlItemStatus.SelectedValue.Trim() == "Close")
                        oItemMaster.ITEM_STATUS = true;
                    else
                        oItemMaster.ITEM_STATUS = false;

                    if (rad_qc_req.SelectedValue.Trim() == "yes")
                        oItemMaster.QC_REQUIRED = true;
                    else
                        oItemMaster.QC_REQUIRED = false;

                    double Maxlvl = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtMaxStockLevel0.Text.Trim()), out Maxlvl);

                    oItemMaster.MAX_STK_LVL = Maxlvl;
                    if (ddlConsumble.SelectedItem.Text == "Consumable")
                    {
                        oItemMaster.CONSUME = true;
                    }
                    else
                    {
                        oItemMaster.CONSUME = false;
                    }
                    Int64 _tariff_heading = 0;
                    Int64 _sales_itchs_code = 0;
                    Int64 _custom_itchs_code = 0;
                    Int64.TryParse(txtTariffHeading.Text, out _tariff_heading);
                    Int64.TryParse(txtSales_ITCHS.Text, out _sales_itchs_code);
                    Int64.TryParse(txtCustom_ITCHS.Text, out _custom_itchs_code);
                    oItemMaster.IS_EXCISABLE = rdIsExciable.SelectedValue;
                    oItemMaster.TARIFF_HEADING = _tariff_heading;
                    oItemMaster.SALES_ITCHS_CODE = _sales_itchs_code;
                    oItemMaster.CUSTOM_ITCHS_CODE = _custom_itchs_code;
                    oItemMaster.ITEM_SIZE = txtItemSize.Text;
                    oItemMaster.WEIGHT = txtWeight.Text;
                    oItemMaster.IS_MOVABLE = ddlMovable.SelectedValue;
                    oItemMaster.ITEM_SUB_CAT = CommonFuction.funFixQuotes(ddlsubcategory.SelectedValue.Trim());
                    oItemMaster.PARTY_CODE = CommonFuction.funFixQuotes(txtPartyCode.SelectedValue.Trim());
                    oItemMaster.BRANCH_UNIT = CommonFuction.funFixQuotes(ddlbranchUnit.SelectedValue.Trim());
                    oItemMaster.OTHER_NO = CommonFuction.funFixQuotes(txtother.Text.Trim());
                    oItemMaster.ITEM_DROWING = CommonFuction.funFixQuotes(txtdrowing.Text.Trim());
                    oItemMaster.CATALOUGE = CommonFuction.funFixQuotes(txtcatalog.Text.Trim());
                    oItemMaster.PART_NO = CommonFuction.funFixQuotes(txtpartno.Text.Trim());
                    oItemMaster.SERIAL_NO = CommonFuction.funFixQuotes(txtserial.Text.Trim());
                    oItemMaster.PARTY_NAME = CommonFuction.funFixQuotes(txtPartyName.Text.Trim());
                    oItemMaster.RACK_CODE1 = CommonFuction.funFixQuotes(txtrackCode2.Text.Trim());
                    oItemMaster.RACK_CODE2 = CommonFuction.funFixQuotes(txtrackCode3.Text.Trim());
                    oItemMaster.IMG_PATH = lblPath.Text.ToString();


                    bool bResult = SaitexBL.Interface.Method.ItemMaster.UpdateItemMaster(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oItemMaster, out iRecordFound);

                    if (bResult)
                    {
                        BlanksControls();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('Item Updated successfully');", true);
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('This record is already saved Please save anather Record');", true);

                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('Please select UOM');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('No such record exits.! Pls enter valid Item Code');", true);
            }
        }
        catch
        { throw; }

    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtItemCode.Text != "")
            {
                //   DeleteData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('First search Record to delete');", true);

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
        }
    }

    private void DeleteData()
    {
        try
        {
            if (CheckForDuplicacy(txtItemCode.Text.Trim()))
            {
                string CatCode = CommonFuction.funFixQuotes(txtItemCode.Text.Trim());
                SaitexDM.Common.DataModel.ItemMaster oItemMaster = new SaitexDM.Common.DataModel.ItemMaster();
                oItemMaster.ITEM_CODE = CatCode;
                bool bResult = SaitexBL.Interface.Method.ItemMaster.DeleteItemMaster(oItemMaster);

                if (bResult)
                {
                    BlanksControls();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('Item Deleted Successfully');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('No such record exits.! Pls enter valid Category Code');", true);
            }
        }
        catch
        { throw; }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Item_MST_OPT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=100,scrollbars=1,resizable=1,width=800,height=300');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        HelpData();
    }

    private void HelpData()
    {

    }

    private bool CheckForDuplicacy(string ItemCode)
    {
        bool Flag = false;
        try
        {
            DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetItemMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "ITEM_CODE='" + ItemCode + "'";
                if (dv.Count > 0)
                {
                    Flag = true;
                }
            }
            return Flag;
        }
        catch
        { return Flag; }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        BlanksControls();
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        tdDelete.Visible = true;
        lblMode.Text = "Update";
        ddlItemCode.Visible = true;
        txtItemCode.Visible = false;
        //grdSuggestion.Visible = false;
        //grdSuggestion.DataSource = null;
        //grdSuggestion.DataBind();
    }

    protected void lnkItemCode_Click(object sender, EventArgs e)
    {

    }

    protected void gvItemmaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //string customerName = Request.Form[txtItemDesc.UniqueID];
            //string customerId = Request.Form[hfDescriptionId.UniqueID];
            string Message = "";
            if (Validate(out Message))
                Insertdata();
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('" + Message + "');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving.\r\nSee error log for detail."));
        }

    }

    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            SaitexDM.Common.DataModel.ItemMaster oItemMaster = new SaitexDM.Common.DataModel.ItemMaster();
            oItemMaster.ITEM_CODE = CommonFuction.funFixQuotes(txtItemCode.Text.Trim());
            DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetItemMasterByItemCode(oItemMaster);

            if (dt != null && dt.Rows.Count > 0)
            {
                txtItemCode.Text = dt.Rows[0]["ITEM_CODE"].ToString();
                ddlItemCategory.SelectedValue = dt.Rows[0]["CAT_CODE"].ToString();
                txtItemDesc.Text = dt.Rows[0]["ITEM_DESC"].ToString();
                txtremarks.Text = dt.Rows[0]["REMARKS"].ToString();
                ddlItemMake.SelectedIndex = ddlItemMake.Items.IndexOf(ddlItemMake.Items.FindByValue(dt.Rows[0]["ITEM_MAKE"].ToString()));
                ddlUOM.SelectedValue = dt.Rows[0]["UOM"].ToString();
                txtAsocItemCode.SelectedValue = dt.Rows[0]["ASOC_ITEM_CODE"].ToString();
                txtOpRate.Text = dt.Rows[0]["OP_RATE"].ToString();
                txtOpBalStock.Text = dt.Rows[0]["OP_BAL_STOCK"].ToString();
                txtrackCode.Text = dt.Rows[0]["RACK_CODE"].ToString();
                txtMinStockLevel.Text = dt.Rows[0]["MIN_STOCK_LVL"].ToString();
                txtReorderQt.Text = dt.Rows[0]["REORDER_QTY"].ToString();
                txtReorderLevel.Text = dt.Rows[0]["REORDER_LVL"].ToString();
                txtMinProcDay.Text = dt.Rows[0]["MIN_PROCURE_DAYS"].ToString();
                txtExpDay.Text = dt.Rows[0]["EXPIRY_DAYS"].ToString();
                ddlItemType.SelectedValue = dt.Rows[0]["ITEM_TYPE"].ToString();


                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Find";

            }
            else
            {
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                lblMode.Text = "Save";

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting item for updation.\r\nSee error log for detail."));
        }
    }

    private void bindItemCategory(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_CATG");
            ddlItemCategory.Items.Clear();
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataTextField = "MST_CODE";
            ddlItemCategory.DataValueField = "MST_DESC";
            ddlItemCategory.DataBind();
            ddlItemCategory.SelectedValue = "ADMIN";
            var cat1 = ddlItemCategory.SelectedText;
            var cat2 = ddlItemCategory.SelectedValue;
        }
        catch
        {
            throw;
        }
    }

    private void bindItemType(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_TYPE");
            ddlItemType.Items.Clear();
            ddlItemType.DataSource = dt;
            ddlItemType.DataTextField = "MST_CODE";
            ddlItemType.DataValueField = "MST_CODE";
            ddlItemType.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void bindMake(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "MAKE");
            ddlItemMake.Items.Clear();
            ddlItemMake.DataSource = dt;
            ddlItemMake.DataTextField = "MST_CODE";
            ddlItemMake.DataValueField = "MST_CODE";
            ddlItemMake.DataBind();
            ddlItemMake.Items.Insert(0, new ListItem("-Select-", ""));
        }
        catch
        {
            throw;
        }
    }

    protected void ddlItemCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtItemCode.Text = GetNewItemCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in item category selection.\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "window.confirm('Are you sure to save the record ?')");
        imgbtnUpdate.Attributes.Add("OnClick", "window.confirm('Are you sure to update the record ?')");
        imgbtnDelete.Attributes.Add("OnClick", "window.confirm('Are you sure to delete the record ?')");
        imgbtnPrint.Attributes.Add("OnClick", "window.confirm('Are you sure to print the record ?')");
        imgbtnClear.Attributes.Add("OnClick", "window.confirm('Are you sure to clear the record ?')");
        imgbtnExit.Attributes.Add("OnClick", "window.confirm('Are you sure to exit the record ?')");
    }

    protected void txtAsocItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select ITEM_CODE,ITEM_DESC,ITEM_TYPE from ( select * from TX_ITEM_MST where Del_Status=0 and rownum <=10) asd";
            string WhereClause = "  where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery or ITEM_TYPE like :SearchQuery";
            string SortExpression = " order by ITEM_CODE asc";
            string SearchQuery = e.Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            txtAsocItemCode.Items.Clear();

            txtAsocItemCode.DataSource = data;
            txtAsocItemCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting associated Items.\r\nSee error log for detail."));
        }
    }

    protected void ddlUOM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindUOM(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting UOM.\r\nSee error log for detail."));
        }
    }

    private void bindUOM(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "UOM");
            ddlUOM.Items.Clear();
            ddlUOM.DataSource = dt;
            ddlUOM.DataTextField = "MST_CODE";
            ddlUOM.DataValueField = "MST_CODE";
            ddlUOM.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE,MST_DESC from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and (UPPER(MST_CODE) like :SearchQuery OR UPPER(MST_DESC) like :SearchQuery)";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = "%" + Text.ToUpper() + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlItemCategory_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            bindItemCategory(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Category.\r\nSee error log for detail."));
        }
    }

    protected void ddlItemType_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindItemType(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in item type.\r\nSee error log for detail."));
        }
    }

    protected void ddlItemCategory_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (lblMode.Text == "Save")
                txtItemCode.Text = GetNewItemCode();
            //  GetGrid();
            ddlUOM.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in item category selection.\r\nSee error log for detail."));
        }
    }

    private bool Validate(out string Message)
    {
        Message = "";
        bool IsValidated = false;
        if (ddlItemCategory.SelectedText == "")
        {
            Message += "<br />Enter Item Category";
        }
        if (txtOpBalStock.Text == "")
        {
            Message += "<br />Enter Opening Stock.";
        }
        if (Message == "")
        {
            IsValidated = true;
        }
        return IsValidated;
    }

    protected void ddlItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            bindItemCategory("");
            bindItemType("");
            bindUOM("");
            bindItemSubCategory("");

            SaitexDM.Common.DataModel.ItemMaster oItemMaster = new SaitexDM.Common.DataModel.ItemMaster();
            oItemMaster.ITEM_CODE = CommonFuction.funFixQuotes(ddlItemCode.SelectedValue.Trim());
            DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetItemMasterByItemCode(oItemMaster);

            if (dt != null && dt.Rows.Count > 0)
            {

                if (dt.Rows[0]["QC_REQUIRED"].ToString() == "1")
                { rad_qc_req.SelectedValue = "yes"; }
                else
                {
                    rad_qc_req.SelectedValue = "No";
                }
                txtItemCode.Text = dt.Rows[0]["ITEM_CODE"].ToString();
                ViewState["ITEMCODE"] = dt.Rows[0]["ITEM_CODE"].ToString();
                txtHSNCODE.Text = dt.Rows[0]["HSN_CODE"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DEPT_CODE"].ToString();
                ddlItemCategory.SelectedValue = dt.Rows[0]["CAT_CODE"].ToString();
                txtItemDesc.Text = dt.Rows[0]["ITEM_DESC"].ToString();
                txtremarks.Text = dt.Rows[0]["ITEM_REMARKS"].ToString();
                ddlItemMake.SelectedIndex = ddlItemMake.Items.IndexOf(ddlItemMake.Items.FindByValue(dt.Rows[0]["ITEM_MAKE"].ToString()));
                ddlUOM.SelectedValue = dt.Rows[0]["UOM"].ToString();
                txtAsocItemCode.SelectedValue = dt.Rows[0]["ASOC_ITEM_CODE"].ToString();
                txtOpRate.Text = dt.Rows[0]["OP_RATE"].ToString();
                txtOpBalStock.Text = dt.Rows[0]["OP_BAL_STOCK"].ToString();
                txtrackCode.Text = dt.Rows[0]["RACK_CODE"].ToString();
                txtMinStockLevel.Text = dt.Rows[0]["MIN_STOCK_LVL"].ToString();
                txtReorderQt.Text = dt.Rows[0]["REODR_QTY"].ToString();
                txtReorderLevel.Text = dt.Rows[0]["REODR_LVL"].ToString();
                txtMinProcDay.Text = dt.Rows[0]["MIN_PROCURE_DAYS"].ToString();
                txtExpDay.Text = dt.Rows[0]["EXPIRY_DAYS"].ToString();
                ddlItemType.SelectedValue = dt.Rows[0]["ITEM_TYPE"].ToString();
                txtMaxStockLevel0.Text = dt.Rows[0]["MAX_STK_LVL"].ToString();

                rdIsExciable.SelectedIndex = rdIsExciable.Items.IndexOf(rdIsExciable.Items.FindByValue(dt.Rows[0]["IS_EXCISABLE"].ToString()));
                txtTariffHeading.Text = dt.Rows[0]["TARIFF_HEADING"].ToString();
                txtSales_ITCHS.Text = dt.Rows[0]["SALES_ITCHS_CODE"].ToString();
                txtCustom_ITCHS.Text = dt.Rows[0]["CUSTOM_ITCHS_CODE"].ToString();
                txtItemSize.Text = dt.Rows[0]["ITEM_SIZE"].ToString();
                txtWeight.Text = dt.Rows[0]["WEIGHT"].ToString();
                ddlMovable.SelectedIndex = ddlMovable.Items.IndexOf(ddlMovable.Items.FindByValue(dt.Rows[0]["IS_MOVABLE"].ToString()));
                txtother.Text = dt.Rows[0]["OTHER_NO"].ToString();
                txtdrowing.Text = dt.Rows[0]["ITEM_DROWING"].ToString();
                txtcatalog.Text = dt.Rows[0]["CATALOUGE"].ToString();
                txtpartno.Text = dt.Rows[0]["PART_NO"].ToString();
                txtserial.Text = dt.Rows[0]["SERIAL_NO"].ToString();
                txtPartyCode.SelectedValue = dt.Rows[0]["PARTY_CODE"].ToString();
                txtPartyName.Text = dt.Rows[0]["PARTY_NAME"].ToString();
                ddlsubcategory.SelectedValue = dt.Rows[0]["ITEM_SUB_CAT"].ToString();
                ddlbranchUnit.SelectedIndex = ddlbranchUnit.Items.IndexOf(ddlbranchUnit.Items.FindByValue(dt.Rows[0]["BRANCH_UNIT"].ToString()));
                txtrackCode2.Text = dt.Rows[0]["RACK_CODE1"].ToString();
                txtrackCode3.Text = dt.Rows[0]["RACK_CODE2"].ToString();
                lblPath.Text = dt.Rows[0]["IMG_PATH"].ToString();
                ItemImage.ImageUrl = dt.Rows[0]["IMG_PATH"].ToString();
                popupImg.HRef = dt.Rows[0]["IMG_PATH"].ToString();




                if (dt.Rows[0]["CONSUME"].ToString() == "1")
                {
                    ddlConsumble.SelectedValue = "Consumable";
                }
                else
                {
                    ddlConsumble.SelectedValue = "Non Consumable";
                }
                if (dt.Rows[0]["CODE_TYPE"].ToString().Equals("Auto"))
                {
                    rdAuto.Checked = true;
                    rdManual.Checked = false;
                }
                else
                {
                    rdAuto.Checked = false;
                    rdManual.Checked = true;
                }
                txtItemDesc.Focus();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Invalid Item Selection');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    protected void ddlItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.DataSource = data;
                ddlItemCode.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }

    protected void txtItemDesc_TextChanged(object sender, EventArgs e)
    {
        GetGrid();
    }

    private void GetGrid()
    {
        try
        {
            //Grid grdSuggestion = new Grid();
            //grdSuggestion.ID = "grdSuggestion";
            //grdSuggestion.AllowAddingRecords = false;
            //grdSuggestion.AutoGenerateColumns = false;
            //grdSuggestion.AllowFiltering = false;
            //grdSuggestion.FolderStyle = "~/StyleSheet/black_glass";
            //grdSuggestion.Rebind += new Grid.DefaultEventHandler(grdSuggestion_Rebind);

            //Column oCol1 = new Column();
            //oCol1.DataField = "ITEM_CODE";
            //oCol1.ReadOnly = true;
            //oCol1.HeaderText = "ITEM CODE";
            //oCol1.Width = "120";

            //Column oCol2 = new Column();
            //oCol2.DataField = "ITEM_DESC";
            //oCol2.HeaderText = "DESCRIPTION";
            //oCol2.Width = "180";

            //Column oCol3 = new Column();
            //oCol3.DataField = "CAT_CODE";
            //oCol3.HeaderText = "CATEGORY";
            //oCol3.Width = "120";
            //grdSuggestion.Columns.Add(oCol1);
            //grdSuggestion.Columns.Add(oCol2);
            //grdSuggestion.Columns.Add(oCol3);

            //if (phGrid1.HasControls())
            //{
            //    foreach (Control c in phGrid1.Controls)
            //    {
            //        if (c.ID == "grdSuggestion")
            //        {
            //            phGrid1.Controls.Remove(c);
            //        }
            //    }
            //}
            //phGrid1.Controls.Add(grdSuggestion);

            //CreateGrid(grdSuggestion);
        }
        catch
        {
            throw;
        }
    }

    private void CreateGrid(Grid grdSuggestion)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.ItemMaster.GetDataForSoundEX(ddlItemCategory.SelectedValue, CommonFuction.funFixQuotes(txtItemDesc.Text.ToUpper().Trim()));
            if (dt != null && dt.Rows.Count > 0)
            {
                grdSuggestion.DataSource = dt;
                grdSuggestion.DataBind();
                grdSuggestion.Visible = true;
            }

        }
        catch
        {
            throw;
        }
    }

    protected void grdSuggestion_Rebind(object sender, EventArgs e)
    {
        try
        {
            if (phGrid1.HasControls())
            {
                foreach (Control c in phGrid1.Controls)
                {
                    if (c.ID == "grdSuggestion")
                    {
                        CreateGrid((Grid)c);
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM  as Combined FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_ITEM_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ITEM_CODE desc";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, ITEM_TYPE FROM   TX_ITEM_MST WHERE      ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    protected void rdAuto_CheckedChanged(object sender, EventArgs e)
    {
        txtItemCode.ReadOnly = false;
        txtItemCode.Text = GetNewItemCode();
        txtItemCode.ReadOnly = true;
        rdManual.Checked = false;

    }
    protected void rdManual_CheckedChanged(object sender, EventArgs e)
    {
        txtItemCode.ReadOnly = false;
        rdAuto.Checked = false;
    }
    protected void imgBtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Queries/ITEM_MATER_QUERY.aspx");
    }
    protected void rdIsExciable_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtTariffHeading.Text = string.Empty;
        if (rdIsExciable.SelectedValue == "1")
        {
            txtTariffHeading.Enabled = true;
            txtTariffHeadingValidator.Enabled = true;
        }
        else
        {
            txtTariffHeading.Enabled = false;
            txtTariffHeadingValidator.Enabled = false;
        }

    }

    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME ||' '|| PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST WHERE (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE       :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)<>upper('TRANSPORTER & LOGISTICS') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE,VENDOR_CAT_CODE FROM TX_VENDOR_MST  WHERE (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE       :SearchQuery) ORDER BY PRTY_CODE ASC) asd WHERE upper(VENDOR_CAT_CODE)<> upper('TRANSPORTER & LOGISTICS') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {
        try
        {

            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('TRANSPORTER & LOGISTICS') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
            //lblPartyCode.Text = txtPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Changing.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
        }
    }
    protected void ddlsubcategory_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            bindItemSubCategory(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item SubCategory.\r\nSee error log for detail."));
        }
    }
    private void bindItemSubCategory(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "ITEM_SUB_CAT");
            ddlsubcategory.Items.Clear();
            ddlsubcategory.DataSource = dt;
            ddlsubcategory.DataTextField = "MST_CODE";
            ddlsubcategory.DataValueField = "MST_DESC";
            ddlsubcategory.DataBind();
            ddlsubcategory.SelectedValue = "ADMIN";
            var cat1 = ddlsubcategory.SelectedText;
            var cat2 = ddlsubcategory.SelectedValue;
        }
        catch
        {
            throw;
        }
    }
    protected void ddlsubcategory_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            string str = FileUpload1.FileName;
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/APP_IMAGES/" + str));
            string Image1 = "~/APP_IMAGES/" + str.ToString();
            lblPath.Text = Image1;
            ItemImage.ImageUrl = Image1;
            popupImg.HRef = Image1;

        }
        else
        {
            lblPath.Text = "Please Upload a File";

        }
    }
}