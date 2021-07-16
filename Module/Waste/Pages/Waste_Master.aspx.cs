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

public partial class Module_Waste_Pages_Waste_Master : System.Web.UI.Page
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
                if (ddlUOM.SelectedIndex >= 0)
                {
                    int iRecordFound = 0;

                    SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();
                    oTX_WASTE_MASTER.ITEM_CODE = CommonFuction.funFixQuotes(txtItemCode.Text.ToUpper().Trim());
                    oTX_WASTE_MASTER.CAT_CODE = CommonFuction.funFixQuotes(ddlItemCategory.SelectedValue.Trim());
                    oTX_WASTE_MASTER.ITEM_TYPE = CommonFuction.funFixQuotes(ddlItemType.SelectedValue.Trim());
                    oTX_WASTE_MASTER.ITEM_DESC = CommonFuction.funFixQuotes(txtItemDesc.Text.ToUpper().Trim());
                    oTX_WASTE_MASTER.REMARKS = CommonFuction.funFixQuotes(txtremarks.Text.Trim());
                    oTX_WASTE_MASTER.ITEM_MAKE = CommonFuction.funFixQuotes(txtItemMake.Text.Trim());
                    oTX_WASTE_MASTER.UOM = CommonFuction.funFixQuotes(ddlUOM.SelectedValue.Trim());
                    oTX_WASTE_MASTER.ASOC_ITEM_CODE = CommonFuction.funFixQuotes(txtAsocItemCode.SelectedValue.Trim());
                    //oTX_WASTE_MASTER.DEPARTMENT = CommonFuction.funFixQuotes(txtDept.Text.Trim());
                    //oTX_WASTE_MASTER.CH_BRANCHCODE = CommonFuction.funFixQuotes(txtBranchCode.Text.Trim());

                    oTX_WASTE_MASTER.DEPT_CODE = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());
                    oTX_WASTE_MASTER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                    oTX_WASTE_MASTER.RACK_CODE = CommonFuction.funFixQuotes(txtrackCode.Text.Trim());

                    int opStock = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtOpBalStock.Text.Trim()), out opStock);
                    oTX_WASTE_MASTER.OP_BAL_STOCK = opStock.ToString().Trim();

                    double opRate = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtOpRate.Text.Trim()), out opRate);
                    oTX_WASTE_MASTER.OP_RATE = opRate.ToString();

                    double Minlvl = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtMinStockLevel.Text.Trim()), out Minlvl);
                    oTX_WASTE_MASTER.MIN_STOCK_LVL = Minlvl;

                    int rQty = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtReorderQt.Text.Trim()), out rQty);
                    oTX_WASTE_MASTER.REORDER_QTY = rQty;

                    int rLvl = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtReorderLevel.Text.Trim()), out rLvl);
                    oTX_WASTE_MASTER.REORDER_LVL = rLvl;

                    int iProcure = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtMinProcDay.Text.Trim()), out iProcure);
                    oTX_WASTE_MASTER.MIN_PROCURE_DAYS = iProcure;

                    int iExpirydays = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtExpDay.Text.Trim()), out iExpirydays);
                    oTX_WASTE_MASTER.EXPIRY_DAYS = iExpirydays;

                    oTX_WASTE_MASTER.TUSER = oUserLoginDetail.UserCode;

                    oTX_WASTE_MASTER.TDATE = System.DateTime.Now;
                    if (ddlItemStatus.SelectedValue.Trim() == "Open")
                        oTX_WASTE_MASTER.ITEM_STATUS = true;
                    else
                        oTX_WASTE_MASTER.ITEM_STATUS = false;

                    if (rad_qc_req.SelectedValue.Trim() == "yes")
                        oTX_WASTE_MASTER.QC_REQUIRED = true;
                    else
                        oTX_WASTE_MASTER.QC_REQUIRED = false;

                    double Maxlvl = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtMaxStockLevel0.Text.Trim()), out Maxlvl);

                    oTX_WASTE_MASTER.MAX_STK_LVL = Maxlvl;
                    if (ddlConsumble.SelectedItem.Text == "Consumable")
                    {
                        oTX_WASTE_MASTER.CONSUME = true;
                    }
                    else
                    {
                        oTX_WASTE_MASTER.CONSUME = false;
                    }

                    bool bResult = SaitexBL.Interface.Method.TX_WASTE_MASTER.InsertTX_WASTE_MASTER(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oTX_WASTE_MASTER, out iRecordFound);

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
            ddlItemCode.SelectedIndex = -1;
            ddlItemCode.SelectedValue = "";
            ddlItemCode.SelectedText = "";
            ddlItemCode.Items.Clear();
            ddlItemCode.Visible = false;

            txtItemCode.Visible = true;

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            lblMode.Text = "Save";

            txtItemCode.Visible = true;

            bindItemCategory("");
            bindItemType("");
            bindUOM("");


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

            txtItemCode.Text = "";
            txtItemDesc.Text = "";
            txtremarks.Text = "";
            txtItemMake.Text = "";
            txtAsocItemCode.SelectedText = "";
            txtOpRate.Text = "";
            txtOpBalStock.Text = "";
            txtrackCode.Text = "";
            txtMinStockLevel.Text = "";
            txtReorderQt.Text = "";
            txtReorderLevel.Text = "";
            txtMinProcDay.Text = "";
            txtExpDay.Text = "";
            txtMaxStockLevel0.Text = "";

            //getDepartment();
            //ddlDepartment.SelectedValue = oUserLoginDetail.VC_DEPARTMENTCODE;
            bindItemCategory("");
            ddlItemCategory.SelectedValue = "Waste";
            var cat = ddlItemCategory.SelectedValue;
            txtItemCode.Text = GetNewItemCode();
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
            string sItemCategory = ddlItemCategory.SelectedValue.Trim();
            string PrefixString = string.Empty;
            if (sItemCategory.Length > 1)
            {
                PrefixString = sItemCategory.Substring(0, 2);

                DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetNewId(PrefixString.ToUpper());

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
                    sNewItemCode = PrefixString + "00000" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 100)
                {
                    sNewItemCode = PrefixString + "0000" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 1000)
                {
                    sNewItemCode = PrefixString + "000" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 10000)
                {
                    sNewItemCode = PrefixString + "00" + iNewItmCode.ToString();
                }
                else if (Convert.ToInt64(iNewItmCode) < 100000)
                {
                    sNewItemCode = PrefixString + "0" + iNewItmCode.ToString();
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
                UpdateData();
                ViewState["ITEMCODE"] = null;
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

                    SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();

                    oTX_WASTE_MASTER.ITEM_CODE = CommonFuction.funFixQuotes(txtItemCode.Text.Trim());
                    oTX_WASTE_MASTER.CAT_CODE = CommonFuction.funFixQuotes(ddlItemCategory.SelectedValue.Trim());
                    oTX_WASTE_MASTER.ITEM_TYPE = CommonFuction.funFixQuotes(ddlItemType.SelectedValue.Trim());
                    oTX_WASTE_MASTER.ITEM_DESC = CommonFuction.funFixQuotes(txtItemDesc.Text.ToUpper().Trim());
                    oTX_WASTE_MASTER.REMARKS = CommonFuction.funFixQuotes(txtremarks.Text.Trim());
                    oTX_WASTE_MASTER.ITEM_MAKE = CommonFuction.funFixQuotes(txtItemMake.Text.Trim());
                    oTX_WASTE_MASTER.UOM = CommonFuction.funFixQuotes(ddlUOM.SelectedValue.Trim());
                    oTX_WASTE_MASTER.ASOC_ITEM_CODE = CommonFuction.funFixQuotes(txtAsocItemCode.SelectedValue.Trim());

                    oTX_WASTE_MASTER.DEPT_CODE = CommonFuction.funFixQuotes(ddlDepartment.SelectedValue.Trim());
                    oTX_WASTE_MASTER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                    oTX_WASTE_MASTER.RACK_CODE = CommonFuction.funFixQuotes(txtrackCode.Text.Trim());

                    int opStock = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtOpBalStock.Text.Trim()), out opStock);
                    oTX_WASTE_MASTER.OP_BAL_STOCK = opStock.ToString().Trim();

                    double opRate = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtOpRate.Text.Trim()), out opRate);
                    oTX_WASTE_MASTER.OP_RATE = opRate.ToString();

                    double Minlvl = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtMinStockLevel.Text.Trim()), out Minlvl);
                    oTX_WASTE_MASTER.MIN_STOCK_LVL = Minlvl;

                    int rQty = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtReorderQt.Text.Trim()), out rQty);
                    oTX_WASTE_MASTER.REORDER_QTY = rQty;

                    int rLvl = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtReorderLevel.Text.Trim()), out rLvl);
                    oTX_WASTE_MASTER.REORDER_LVL = rLvl;

                    int iProcure = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtMinProcDay.Text.Trim()), out iProcure);
                    oTX_WASTE_MASTER.MIN_PROCURE_DAYS = iProcure;

                    int iExpirydays = 0;
                    int.TryParse(CommonFuction.funFixQuotes(txtExpDay.Text.Trim()), out iExpirydays);
                    oTX_WASTE_MASTER.EXPIRY_DAYS = iExpirydays;

                    oTX_WASTE_MASTER.TUSER = oUserLoginDetail.UserCode;
                    oTX_WASTE_MASTER.TDATE = System.DateTime.Now;

                    if (ddlItemStatus.SelectedValue.Trim() == "Close")
                        oTX_WASTE_MASTER.ITEM_STATUS = true;
                    else
                        oTX_WASTE_MASTER.ITEM_STATUS = false;

                    if (rad_qc_req.SelectedValue.Trim() == "yes")
                        oTX_WASTE_MASTER.QC_REQUIRED = true;
                    else
                        oTX_WASTE_MASTER.QC_REQUIRED = false;

                    double Maxlvl = 0;
                    double.TryParse(CommonFuction.funFixQuotes(txtMaxStockLevel0.Text.Trim()), out Maxlvl);

                    oTX_WASTE_MASTER.MAX_STK_LVL = Maxlvl;
                    if (ddlConsumble.SelectedItem.Text == "Consumable")
                    {
                        oTX_WASTE_MASTER.CONSUME = true;
                    }
                    else
                    {
                        oTX_WASTE_MASTER.CONSUME = false;
                    }

                    bool bResult = SaitexBL.Interface.Method.TX_WASTE_MASTER.UpdateTX_WASTE_MASTER(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oTX_WASTE_MASTER, out iRecordFound);

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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "dd", "window.alert('No such record exits.! Pls enter valid Waste Code');", true);
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
                SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();
                oTX_WASTE_MASTER.ITEM_CODE = CatCode;
                bool bResult = SaitexBL.Interface.Method.TX_WASTE_MASTER.DeleteTX_WASTE_MASTER(oTX_WASTE_MASTER);

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
            string URL = "../Report/WASTE_MST_OPT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=500,height=200');", true);
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
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetTX_WASTE_MASTER();

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
            SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();
            oTX_WASTE_MASTER.ITEM_CODE = CommonFuction.funFixQuotes(txtItemCode.Text.Trim());
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetTX_WASTE_MASTERByItemCode(oTX_WASTE_MASTER);

            if (dt != null && dt.Rows.Count > 0)
            {
                txtItemCode.Text = dt.Rows[0]["ITEM_CODE"].ToString();
                ddlItemCategory.SelectedValue = dt.Rows[0]["CAT_CODE"].ToString();
                txtItemDesc.Text = dt.Rows[0]["ITEM_DESC"].ToString();
                txtremarks.Text = dt.Rows[0]["REMARKS"].ToString();
                txtItemMake.Text = dt.Rows[0]["ITEM_MAKE"].ToString();
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
            DataTable dt = GET_MOM_DATA(SearchQuery, "WASTE_CATEGORY");
            ddlItemCategory.Items.Clear();
            ddlItemCategory.DataSource = dt;
            ddlItemCategory.DataTextField = "MST_CODE";
            ddlItemCategory.DataValueField = "MST_CODE";
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
            string CommandText = "select ITEM_CODE,ITEM_DESC,ITEM_TYPE from ( select * from TX_WASTE_MST where Del_Status=0 and rownum <=10) asd";
            string WhereClause = "  where ITEM_CODE like :SearchQuery or ITEM_DESC like :SearchQuery or ITEM_TYPE like :SearchQuery";
            string SortExpression = " order by ITEM_CODE asc";
            string SearchQuery = e.Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_WASTE_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

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
            string CommandText = "select MST_CODE from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and MST_CODE like :SearchQuery";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = Text + "%";
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

            SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();
            oTX_WASTE_MASTER.ITEM_CODE = CommonFuction.funFixQuotes(ddlItemCode.SelectedValue.Trim());
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetTX_WASTE_MASTERByItemCode(oTX_WASTE_MASTER);

            if (dt != null && dt.Rows.Count > 0)
            {

                txtItemCode.Text = dt.Rows[0]["ITEM_CODE"].ToString();
                ViewState["ITEMCODE"] = dt.Rows[0]["ITEM_CODE"].ToString();
                ddlDepartment.SelectedValue = dt.Rows[0]["DEPT_CODE"].ToString();
                ddlItemCategory.SelectedValue = dt.Rows[0]["CAT_CODE"].ToString();
                txtItemDesc.Text = dt.Rows[0]["ITEM_DESC"].ToString();
                txtremarks.Text = dt.Rows[0]["ITEM_REMARKS"].ToString();
                txtItemMake.Text = dt.Rows[0]["ITEM_MAKE"].ToString();
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
                if (dt.Rows[0]["CONSUME"].ToString() == "1")
                {
                    ddlConsumble.SelectedValue = "Consumable";

                }
                else
                {
                    ddlConsumble.SelectedValue = "Non Consumable";
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
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetDataForSoundEX(ddlItemCategory.SelectedText, CommonFuction.funFixQuotes(txtItemDesc.Text.ToUpper().Trim()));
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
            string CommandText = " SELECT * FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_TYPE, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM  as Combined FROM TX_WASTE_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_code NOT IN (SELECT ITEM_CODE FROM (SELECT ITEM_CODE, ITEM_DESC, ITEM_CODE||'@'|| ITEM_DESC||'@'||UOM as Combined   FROM TX_WASTE_MST WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE) asd WHERE ROWNUM <= " + startOffset + ")  ";
            }

            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_WASTE_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE, ITEM_DESC, ITEM_TYPE FROM   TX_WASTE_MST WHERE      ITEM_CODE LIKE :SearchQuery OR ITEM_TYPE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY ITEM_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_WASTE_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
}
