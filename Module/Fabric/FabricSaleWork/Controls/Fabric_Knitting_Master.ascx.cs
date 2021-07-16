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
using errorLog;


public partial class Module_Fabric_FabricSaleWork_Controls_Fabric_Knitting_Master : System.Web.UI.UserControl
{
    /***************************** CREATED BY NISHANT RAI AT 28_01_2014**********************************/  
                  
    int countflag = 0;
    bool dd = false;
    string msg = string.Empty;
    string Errormsg = string.Empty;
    string ArticleCode = string.Empty;
    string prefix = string.Empty;
    private DataTable dtBlandDetail = null; 
    private DataTable dtBaseArticleDetail = null;
    string newShortcode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_FABRIC_MST oFABRICMST = new SaitexDM.Common.DataModel.TX_FABRIC_MST();

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
            Initialize();
        }
    }

    private void Initialize()
    {
        try
        {
            lblMode.Text = "Save";
            imgbtnDelete.Visible = false;
            imgbtnUpdate.Visible = false;
            imgbtnPrint.Visible = false;
            imgbtnSave.Visible = true;
            tdSave.Visible = true;
            txtFabricCode.Visible = true;
            DisableOrInableFieldinUpdateforFabriccode(true);
            ddlFabricCode.Visible = false;                
            txtFabricCode.Text = string.Empty;
            ddlFabricType.SelectedIndex = -1;
            ddlFabricCategory.SelectedIndex = -1;
            ddlFabricSubCategory.SelectedIndex = -1;
            ddlFilament.SelectedIndex = -1;
            ddlUOM.SelectedIndex = -1;
            this.CreateBaseArticleDetailTable();
            txtGauge.Text = string.Empty;
            txtDiameter.Text = string.Empty;
            txtStitchLenght.Text = string.Empty;
            txtGSM.Text = string.Empty;
            txtFabricRemarks.Text = string.Empty;
            txtOpeningBalanceStock.Text = string.Empty;
            txtMimimumStock.Text = string.Empty;
            txtMinimumProcureDays.Text = string.Empty;
            txtOpeningRate.Text = string.Empty;
            txtRecorderLevel.Text = string.Empty;
            txtRecorderQuantity.Text = string.Empty;
            txtMaximumStock.Text = string.Empty;
            RefreshDetailRow();
           
            //ddlProductType.SelectedIndex = 4;
            BindBaseBASIS();
            ddlBland21.SelectedValue = "Yarn";
            bindFabricType("FABRIC_TYPE");
            bindFabricCategory("FABRIC_CATEGORY");
            bindFabricSubCategory("FABRIC_SUB_CATEGORY", ddlFabricCategory.SelectedItem.ToString());
            bindFabricFilament("FABRIC_FILAMENT");
            bindFabricUOM("UOM");                    
            CreateBlandDetailTable(); 
            //BindFabricCode();
            bindFabricBland("YARN_BLAND");
            bindBaseUOM("UOM");           
            bindFiberBAseProductType();
            bindFabricBland();
        }
        catch
        {
            throw;
        }
    }

    public void bindFabricType(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFabricType.Items.Clear();
                ddlFabricType.DataSource = dt;
                ddlFabricType.DataTextField = "MST_DESC";
                ddlFabricType.DataValueField = "MST_DESC";
                ddlFabricType.DataBind();
                ddlFabricType.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch
        {
            throw;
        }
    }

    public void bindFabricCategory(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFabricCategory.Items.Clear();
                ddlFabricCategory.DataSource = dt;
                ddlFabricCategory.DataTextField = "MST_DESC";
                ddlFabricCategory.DataValueField = "MST_DESC";
                ddlFabricCategory.DataBind();
                ddlFabricCategory.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch
        {
            throw;
        }


    }

    public void bindFabricSubCategory(string MST_NAME, string TYPE)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                var dv = new DataView(dt);
                dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlFabricSubCategory.Items.Clear();
                    ddlFabricSubCategory.DataSource = dv;
                    ddlFabricSubCategory.DataTextField = "MST_CODE";
                    ddlFabricSubCategory.DataValueField = "MST_CODE";
                    ddlFabricSubCategory.DataBind();
                    ddlFabricSubCategory.Items.Insert(0, new ListItem("------Select------",""));
                }
                else
                {
                    ddlFabricSubCategory.Items.Clear();
                    ddlFabricSubCategory.Items.Insert(0, new ListItem("------NoItems------", ""));

                }
            }
        }
        catch
        {
            throw;
        }
    }

    public void bindFabricFilament(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFilament.Items.Clear();
                ddlFilament.DataSource = dt;
                ddlFilament.DataTextField = "MST_DESC";
                ddlFilament.DataValueField = "MST_DESC";
                ddlFilament.DataBind();
                ddlFilament.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch
        {
            throw;
        }
    }

    public void bindFabricUOM(string MST_NAME)
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlUOM.Enabled = true;
                ddlUOM.Items.Clear();
                ddlUOM.DataSource = dt;
                ddlUOM.DataTextField = "MST_CODE";
                ddlUOM.DataValueField = "MST_CODE";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, new ListItem("------Select------", ""));
                ddlUOM.SelectedValue = "KG";
                //ddlUOM.Enabled = false;
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindFabricCode()
    {
        try
        {
            txtFabricCode.Enabled = true;
            string msg = string.Empty;
            var PREFIX = SaitexBL.Interface.Method.TX_FABRIC_MST.GetPrefixCode(ddlFabricCategory.SelectedValue);
            var YarnCode = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ddlFabricCategory.SelectedValue, PREFIX.ToUpper());
            txtFabricCode.Text = YarnCode;
            txtFabricCode.Enabled = false;
        }
        catch
        {
            throw;
        }
    }

    private void BindBaseBASIS()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_BASIS", oUserLoginDetail.COMP_CODE);
            ddlBaseBasis.DataSource = dt;
            ddlBaseBasis.DataValueField = "MST_CODE";
            ddlBaseBasis.DataTextField = "MST_DESC";
            ddlBaseBasis.DataBind();
            //ddlBaseBasis.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;

        }

    }

    public void bindFabricBland(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBland2.Items.Clear();
                ddlBland2.DataSource = dt;
                ddlBland2.DataTextField = "MST_DESC";
                ddlBland2.DataValueField = "MST_CODE";
                ddlBland2.DataBind();
                //ddlBland2.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    protected void ddlfabricCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFabricSubCategory("FABRIC_SUB_CATEGORY", ddlFabricCategory.SelectedItem.ToString());
        BindFabricCode();
    }

    protected void ddlBland2_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            bindFabricBland("YARN_BLAND");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Category.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (CheckValidation())
            {
                return;
            }
            if (Page.IsValid)
            {
                if (ViewState["dtBlandDetail"] != null)
                    dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
                if (dtBlandDetail.Rows.Count > 0)
                {
                    if (ViewState["dtBaseArticleDetail"] != null)
                        dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
                    int iRecordFound = 0;
                    oFABRICMST.FABR_CODE = txtFabricCode.Text;
                    oFABRICMST.FABR_DESC = Common.CommonFuction.funFixQuotes(txtFabricRemarks.Text);
                    oFABRICMST.FABR_TYPE = ddlFabricType.SelectedValue ;
                    oFABRICMST.FABRIC_CATEGORY = ddlFabricCategory.SelectedValue;
                    oFABRICMST.FABRIC_SUB_CATEGORY = ddlFabricSubCategory.SelectedValue;

                    double _gauge = 0;
                    double.TryParse(txtGauge.Text,out _gauge);
                    oFABRICMST.GAUGE = _gauge;

                    double _diameter = 0;
                    double.TryParse(txtDiameter.Text, out _diameter);
                    oFABRICMST.DIAMETER = _diameter;

                    double _stitchlength = 0;
                    double.TryParse(txtStitchLenght.Text, out _stitchlength);
                    oFABRICMST.STITCH_LENGTH = _stitchlength;

                    //double _gsm = 0;
                    //double.TryParse(txtGSM.Text, out _gsm);
                    oFABRICMST.GSM = txtGSM.Text;

                    oFABRICMST.UOM = ddlUOM.SelectedValue;
                    oFABRICMST.FABRIC_FILAMENT = ddlFilament.SelectedValue;                                   
                    oFABRICMST.TUSER = oUserLoginDetail.UserCode;
                    oFABRICMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year.ToString();
                    oFABRICMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFABRICMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFABRICMST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                    oFABRICMST.TUSER = oUserLoginDetail.UserCode;
                    double _minstock = 0;
                    double _minprocureday = 0;
                    double _openingbal = 0;
                    double _openingrate = 0;
                    double _reorderlevel = 0;
                    double _reorderqty = 0;
                    double _maxstock = 0;
                    double.TryParse(txtOpeningBalanceStock.Text, out _openingbal);
                    double.TryParse(txtMinimumProcureDays.Text, out _minprocureday);
                    double.TryParse(txtMimimumStock.Text, out _minstock);
                    double.TryParse(txtOpeningRate.Text, out _openingrate);
                    double.TryParse(txtRecorderLevel.Text, out _reorderlevel);
                    double.TryParse(txtRecorderQuantity.Text, out _reorderqty);
                    double.TryParse(txtMaximumStock.Text, out _maxstock);
                    oFABRICMST.MIN_STOCK = _minstock;
                    oFABRICMST.MIN_PROCURE_DAYS = _minprocureday;
                    oFABRICMST.OPENING_BALANCE_STOCK = _openingbal;
                    oFABRICMST.OPENING_RATE = _openingrate;
                    oFABRICMST.REORDER_LEVEL = _reorderlevel;
                    oFABRICMST.REORDER_QUANTITY = _reorderqty;
                    oFABRICMST.MAX_STOCK = _maxstock;

                    if (Errormsg == string.Empty)
                    {
                        bool resutl = SaitexBL.Interface.Method.TX_FABRIC_MST.InsertFabricKnittingMaster(oFABRICMST, out iRecordFound, dtBlandDetail, dtBaseArticleDetail);
                        if (resutl)
                        {
                            string Resultmsg = "Fabric Knitting Master Saved Successfully" + "\\r\\n";
                            Resultmsg += "Fabric Code is:" + oFABRICMST.FABR_CODE;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                            RefreshAllControls();
                            dtBlandDetail.Dispose();
                            dtBaseArticleDetail.Dispose();
                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Already Exists');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Master Saving Failed!!');", true);


                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(Errormsg);

                    }
                }

                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Atleast One Bland!");

                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving Fabric Master.\r\nSee error log for detail."));

        }

    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (CheckValidation())
            {
                return;
            }
            if (Page.IsValid)
            {
                if (ViewState["dtBlandDetail"] != null)
                    dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];

                if (dtBlandDetail.Rows.Count > 0)
                {
                    if (ViewState["dtBaseArticleDetail"] != null)
                        dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
                    int iRecordFound = 0;
                    oFABRICMST.FABR_CODE = txtFabricCode.Text;
                    oFABRICMST.FABR_DESC = Common.CommonFuction.funFixQuotes(txtFabricRemarks.Text);
                    oFABRICMST.FABR_TYPE = ddlFabricType.SelectedValue;
                    oFABRICMST.FABRIC_CATEGORY = ddlFabricCategory.SelectedValue;
                    oFABRICMST.FABRIC_SUB_CATEGORY = ddlFabricSubCategory.SelectedValue;

                    double _gauge = 0;
                    double.TryParse(txtGauge.Text, out _gauge);
                    oFABRICMST.GAUGE = _gauge;

                    double _diameter = 0;
                    double.TryParse(txtDiameter.Text, out _diameter);
                    oFABRICMST.DIAMETER = _diameter;

                    double _stitchlength = 0;
                    double.TryParse(txtStitchLenght.Text, out _stitchlength);
                    oFABRICMST.STITCH_LENGTH = _stitchlength;

                    //double _gsm = 0;
                    //double.TryParse(txtGSM.Text, out _gsm);
                    oFABRICMST.GSM = txtGSM.Text;

                    oFABRICMST.UOM = ddlUOM.SelectedValue;
                    oFABRICMST.FABRIC_FILAMENT = ddlFilament.SelectedValue;
                    oFABRICMST.TUSER = oUserLoginDetail.UserCode;
                    oFABRICMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year.ToString();
                    oFABRICMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFABRICMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFABRICMST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                    oFABRICMST.TUSER = oUserLoginDetail.UserCode;
                    double _minstock = 0;
                    double _minprocureday = 0;
                    double _openingbal = 0;
                    double _openingrate = 0;
                    double _reorderlevel = 0;
                    double _reorderqty = 0;
                    double _maxstock = 0;
                    double.TryParse(txtOpeningBalanceStock.Text, out _openingbal);
                    double.TryParse(txtMinimumProcureDays.Text, out _minprocureday);
                    double.TryParse(txtMimimumStock.Text, out _minstock);
                    double.TryParse(txtOpeningRate.Text, out _openingrate);
                    double.TryParse(txtRecorderLevel.Text, out _reorderlevel);
                    double.TryParse(txtRecorderQuantity.Text, out _reorderqty);
                    double.TryParse(txtMaximumStock.Text, out _maxstock);
                    oFABRICMST.MIN_STOCK = _minstock;
                    oFABRICMST.MIN_PROCURE_DAYS = _minprocureday;
                    oFABRICMST.OPENING_BALANCE_STOCK = _openingbal;
                    oFABRICMST.OPENING_RATE = _openingrate;
                    oFABRICMST.REORDER_LEVEL = _reorderlevel;
                    oFABRICMST.REORDER_QUANTITY = _reorderqty;
                    oFABRICMST.MAX_STOCK = _maxstock;

                    if (Errormsg == string.Empty)
                    {
                        bool resutl = SaitexBL.Interface.Method.TX_FABRIC_MST.UpdateFabricKnittingMaster(oFABRICMST, out iRecordFound, dtBlandDetail, dtBaseArticleDetail);
                        if (resutl)
                        {
                            string Resultmsg = "Fabric Knitting Master Updated Successfully" + "\\r\\n";
                            Resultmsg += "Fabric Code is:" + oFABRICMST.FABR_CODE;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                            RefreshAllControls();
                            dtBlandDetail.Dispose();
                            dtBaseArticleDetail.Dispose();
                            
                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Already Exists');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Master Saving Failed!!');", true);


                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(Errormsg);

                    }
                }

                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Atleast One Bland!");

                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving Fabric Master.\r\nSee error log for detail."));

        }

    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            ddlFabricCode.Visible = true;
            imgbtnUpdate.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            txtFabricCode.Text = "";
            txtFabricCode.Visible = false;
            DisableOrInableFieldinUpdateforFabriccode(false);
            BindFabricCodeinFindMode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));

        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Save";
            RefreshAllControls();
           // Response.Redirect("~/Module/Fabric/FabricSaleWork/Pages/Fabric_Knitting_Master.aspx",false);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clearing Form Data.\r\nSee error log for detail."));

        }

    }

    protected void grdBaseArticleDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "BaseEdit")
            {
                FillBaseArticleByGrid(UniqueId);
            }
            else if (e.CommandName == "BaseDelete")
            {
                DeleteBaseArticleRow(UniqueId);
                BindBaseArticleGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Base Grid RowCommand Selection.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }    
   
    private void CreateBlandDetailTable()
    {
        try
        {
            dtBlandDetail = new DataTable();
            dtBlandDetail.Columns.Add("UniqueId", typeof(int));
            dtBlandDetail.Columns.Add("BlendArticle", typeof(string));
            dtBlandDetail.Columns.Add("BlendArticleDesc", typeof(string));
            dtBlandDetail.Columns.Add("Percentage", typeof(double));
            dtBlandDetail.Columns.Add("Remarks", typeof(string));
           // dtBlandDetail.Columns.Add("Count", typeof(string));
            ViewState["dtBlandDetail"] = dtBlandDetail;
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }

    protected void btmSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBland2.SelectedIndex < 0)
            {
                CommonFuction.ShowMessage("Please select type of blending.");
                return;
            }

            int percent = 0;
            int.TryParse(txtbland1percentage0.Text, out percent);
            if (percent > 100 || percent < 1)
            {
                CommonFuction.ShowMessage("Percent should not be greater then 100 and less then 1");
                return;
            }

            if (ViewState["dtBlandDetail"] != null)
                dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
            if (dtBlandDetail == null)
                CreateBlandDetailTable();

            if (dtBlandDetail.Rows.Count < 15)
            {

                if (txtbland1percentage0.Text != "")
                {
                    if (Checkblandtotal())
                    {
                        int UniqueId = 0;
                        if (ViewState["UniqueId"] != null)
                        {
                            UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                        }
                        bool bb = SearchItemCodeInGrid(ddlBland2.SelectedValue.Trim(), UniqueId);
                        if (!bb)
                        {


                            if (UniqueId > 0)
                            {
                                var dv = new DataView(dtBlandDetail);
                                dv.RowFilter = "UniqueId=" + UniqueId;
                                if (dv.Count > 0)
                                {
                                    dv[0]["BlendArticle"] = ddlBland2.SelectedValue.Trim();
                                    dv[0]["BlendArticleDesc"] = ddlBland2.SelectedText.Trim();
                                    dv[0]["Percentage"] = txtbland1percentage0.Text.Trim();
                                    dv[0]["Remarks"] = txtBlandRemarks.Text.Trim();
                                    //dv[0]["Count"] = txtCount.Text.Trim();
                                    dtBlandDetail.AcceptChanges();
                                }
                            }
                            else
                            {
                                var dr = dtBlandDetail.NewRow();
                                dr["UniqueId"] = dtBlandDetail.Rows.Count + 1;
                                dr["BlendArticle"] = ddlBland2.SelectedValue.Trim();
                                dr["BlendArticleDesc"] = ddlBland2.SelectedText.Trim();
                                dr["Percentage"] = txtbland1percentage0.Text.Trim();
                                dr["Remarks"] = txtBlandRemarks.Text.Trim();
                                //dr["Count"] = txtCount.Text.Trim();
                                dtBlandDetail.Rows.Add(dr);
                            }
                            RefreshDetailRow();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Bland Already Added.Please Select Another');", true);
                        }
                        ViewState["dtBlandDetail"] = dtBlandDetail;
                        BindBlandDetailGrid();
                    }
                    else
                    {
                        CommonFuction.ShowMessage("Total Substrate/Bland Should not Excceed 100%");
                    }
                }


                else
                {
                    CommonFuction.ShowMessage("Please Enter Bland Percentage");
                }

            }

            else
            {
                CommonFuction.ShowMessage("You have reached the limit of Bland. Only 15 Bland allowed in one Yarn Master.");
            }

        }
        catch
        {
            throw;
        }
    }

    private void RefreshDetailRow()
    {
        try
        {
            ddlBland2.SelectedIndex = -1;
            txtbland1percentage0.Text = string.Empty;
            txtBlandRemarks.Text = string.Empty;
            //txtCount.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void BindBlandDetailGrid()
    {
        try
        {
            if (ViewState["dtBlandDetail"] != null)
                dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];

            grdBlandTran.DataSource = dtBlandDetail;
            grdBlandTran.DataBind();

            double FinalTotal = 0;
            foreach (GridViewRow row in grdBlandTran.Rows)
            {
                Label txtItemDesc = (Label)row.FindControl("txtItemDesc");
                FinalTotal = FinalTotal + double.Parse(txtItemDesc.Text.Trim());
            }
            if (grdBlandTran.Rows.Count > 0)
            {
                Label lblBlandTotal = (Label)grdBlandTran.FooterRow.FindControl("lblBlandTotal");
                lblBlandTotal.Text = FinalTotal.ToString();
            }


        }
        catch
        {
            throw;
        }
    }

    private bool SearchItemCodeInGrid(string BlendArticle, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdBlandTran.Rows)
            {
                var lblbland = (Label)grdRow.FindControl("txtblendArtilce");
                var lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (lblbland.Text.Trim() == BlendArticle && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void BtnBaseCancel_Click(object sender, EventArgs e)
    {
        RefresBaseArticleRow();
    }

    private void RefresBaseArticleRow()
    {
        ddlProductType.SelectedValue = "Yarn";        
        ddlBland21.SelectedIndex = -1;
        lblYarnDesc.Text = string.Empty;        
        txtCount.Text = string.Empty;
        ddlBaseUOM.SelectedValue = "KG";
        ddlBaseBasis.SelectedIndex = -1;
        txtValueQty.Text = string.Empty;
        TxtWastage.Text = string.Empty;
        ViewState["UniqueId"] = null;
    }

    protected bool Checkblandtotal()
    {
        try
        {
            double alltotal = 0f;
            double totalpercentage = 100;
            double currentpage = 0;

            if (grdBlandTran.Rows.Count > 0)
            {
                for (int i = 0; i < grdBlandTran.Rows.Count; i++)
                {
                    var txtbland1percentage = grdBlandTran.Rows[i].FindControl("txtItemDesc") as Label;
                    currentpage += double.Parse(txtbland1percentage.Text);
                }
                if (ViewState["countflag"] != null)
                {
                    countflag = (int)ViewState["countflag"];
                }
                if (countflag == 0)
                {
                    alltotal = currentpage + double.Parse(txtbland1percentage0.Text);
                }
                else
                {
                    alltotal = double.Parse(txtbland1percentage0.Text);
                    countflag = 0;
                    ViewState["countflag"] = countflag;

                }
                if (alltotal > totalpercentage)
                {
                    return false;

                }
                else
                {
                    return true;
                }

            }
            else if (double.Parse(txtbland1percentage0.Text) > 100)
            {
                return false;
            }
            else
            {
                return true;

            }

        }
        catch
        {
            throw;

        }


    }

    private void BindBaseArticleGrid()
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            grdBaseArticleDetail.DataSource = dtBaseArticleDetail;
            grdBaseArticleDetail.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void MapBaseArticleRowDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            if (dtBaseArticleDetail == null || dtBaseArticleDetail.Rows.Count == 0)
            {
                CreateBaseArticleDetailTable();
            }
            dtBaseArticleDetail.Rows.Clear();

            int currentyear = oUserLoginDetail.DT_STARTDATE.Year;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    if (currentyear == int.Parse(drTemp["YEAR"].ToString()))
                    {
                        DataRow dr = dtBaseArticleDetail.NewRow();
                        dr["UniqueId"] = dtBaseArticleDetail.Rows.Count + 1;
                        dr["ProductType"] = drTemp["PRODUCT_TYPE"];
                        dr["ArticleCode"] = drTemp["ARTICLE_CODE"];
                        dr["ArticleDesc"] = drTemp["YARN_DESC"];
                        dr["Count"] = drTemp["COUNT"];
                        dr["UOM"] = drTemp["UOM"];
                        dr["Basis"] = drTemp["BASIS"];
                        dr["ValueQty"] = drTemp["VALUE_QTY"];
                        dr["WASTAGE"] = drTemp["WASTEG"];
                        dtBaseArticleDetail.Rows.Add(dr);
                    }
                }
                dtTemp = null;

            }
        }
        catch
        {
            throw;
        }
    }

    private void CreateBaseArticleDetailTable()
    {
        dtBaseArticleDetail = new DataTable();
        dtBaseArticleDetail.Columns.Add("UniqueId", typeof(int));
        dtBaseArticleDetail.Columns.Add("ProductType", typeof(string));
        dtBaseArticleDetail.Columns.Add("ArticleCode", typeof(string));
        dtBaseArticleDetail.Columns.Add("ArticleDesc", typeof(string));
        dtBaseArticleDetail.Columns.Add("Count", typeof(string));

        dtBaseArticleDetail.Columns.Add("UOM", typeof(string));
        dtBaseArticleDetail.Columns.Add("Basis", typeof(string));
        dtBaseArticleDetail.Columns.Add("ValueQty", typeof(double));
        dtBaseArticleDetail.Columns.Add("Wastage",typeof(double));


        ViewState["dtBaseArticleDetail"] = dtBaseArticleDetail;

    }

    private void FillBaseArticleByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            DataView dv = new DataView(dtBaseArticleDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {

                ddlProductType.SelectedValue = dv[0]["ProductType"].ToString();
                bindFabricBland();
              
                ddlBland21.SelectedValue = dv[0]["ArticleCode"].ToString();
                lblYarnDesc.Text = dv[0]["ArticleDesc"].ToString();
                txtCount.Text = dv[0] ["Count"].ToString();
                ddlBaseUOM.SelectedValue = dv[0]["UOM"].ToString();
                ddlBaseBasis.SelectedValue = dv[0]["Basis"].ToString();
                txtValueQty.Text = dv[0]["ValueQty"].ToString();
                TxtWastage.Text = dv[0] ["Wastage"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchParameterInBaseArticle(string Parameter, int UniqueId, string ArticleType)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdBaseArticleDetail.Rows)
            {
                Label txtProductType = (Label)grdRow.FindControl("txtProductType");
                Label txtBaseArticleCode = (Label)grdRow.FindControl("txtArticleCode");
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete0");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtProductType.Text.Trim() == Parameter && UniqueId != iUniqueId && txtBaseArticleCode.Text.Trim() == ArticleType)
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

    protected void BtnBaseSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBland21.SelectedIndex < 0)
            {
                CommonFuction.ShowMessage("Please select article code.");
                return;
            }
            if (ViewState["dtBaseArticleDetail"] != null)
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            if (dtBaseArticleDetail == null)
                CreateBaseArticleDetailTable();

            if (dtBaseArticleDetail.Rows.Count < 15)
            {
                if (txtValueQty.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                    {
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    }
                    bool bb = SearchParameterInBaseArticle(ddlProductType.SelectedItem.ToString().Trim(), UniqueId, ddlBland21.SelectedValue.ToString());
                    if (!bb)
                    {

                        double Wastage = 0;
                        double.TryParse(TxtWastage.Text.Trim(), out Wastage);
                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(dtBaseArticleDetail);
                            dv.RowFilter = "UniqueId=" + UniqueId;
                            if (dv.Count > 0)
                            {

                                dv[0]["ProductType"] = ddlProductType.SelectedItem.ToString().Trim();
                                dv[0]["ArticleCode"] = ddlBland21.SelectedValue.Trim();
                                dv[0]["ArticleDesc"] = lblYarnDesc.Text;
                                dv[0]["Count"] = txtCount.Text;
                                dv[0]["UOM"] = ddlBaseUOM.SelectedItem.ToString();
                                dv[0]["Basis"] = ddlBaseBasis.SelectedItem.ToString();
                                dv[0]["ValueQty"] = txtValueQty.Text;
                                dv[0]["Wastage"] = Wastage;
                                dtBaseArticleDetail.AcceptChanges();
                            }
                        }
                        else
                        {
                           

                            DataRow dr = dtBaseArticleDetail.NewRow();
                            dr["UniqueId"] = dtBaseArticleDetail.Rows.Count + 1;
                            dr["ProductType"] = ddlProductType.SelectedItem.ToString().Trim();
                            dr["ArticleCode"] = ddlBland21.SelectedValue.Trim();
                            dr["ArticleDesc"] = lblYarnDesc.Text;
                            dr["Count"] = txtCount.Text;
                            dr["UOM"] = ddlBaseUOM.SelectedItem.ToString();
                            dr["Basis"] = ddlBaseBasis.SelectedItem.ToString();
                            dr["ValueQty"] = txtValueQty.Text;
                            dr["Wastage"] = Wastage;
                            dtBaseArticleDetail.Rows.Add(dr);
                        }
                        ViewState["dtBaseArticleDetail"] = dtBaseArticleDetail;
                        RefresBaseArticleRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Atrilce Type  Already Added.Please Select Another');", true);
                    }
                    
                    BindBaseArticleGrid();
                }

                else
                {
                    CommonFuction.ShowMessage("Please Enter  Value Quantity");
                }

            }

            else
            {
                CommonFuction.ShowMessage("You have reached the limit of Base Article. Only 15 Standard allowed in one Machine Process Master.");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving Base Detail Row.\r\nSee error log for detail."));

        }
    }

    private void DeleteBaseArticleRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            if (grdBaseArticleDetail.Rows.Count == 1)
            {
                dtBaseArticleDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtBaseArticleDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtBaseArticleDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtBaseArticleDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtBaseArticleDetail"] = dtBaseArticleDetail;
            }
        }
        catch
        {
            throw;
        }
    }
  
    public void bindBaseUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBaseUOM.Enabled = true;
                ddlBaseUOM.Items.Clear();
                ddlBaseUOM.DataSource = dt;
                ddlBaseUOM.DataTextField = "MST_CODE";
                ddlBaseUOM.DataValueField = "MST_CODE";
                ddlBaseUOM.DataBind();
                ddlBaseUOM.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlBaseUOM.SelectedValue = "KG";
               
            }

        }
        catch
        {
            throw;
        }


    }

    public void bindFiberBAseProductType()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetBaseArticleType();
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlProductType.Items.Clear();
                ddlProductType.DataSource = dt;
                ddlProductType.DataTextField = "ARTICLE_TYPE";
                ddlProductType.DataValueField = "ARTICLE_TYPE";
                ddlProductType.DataBind();
                ddlProductType.Items.Insert(0, new ListItem("--------Select-------", "0"));
                ddlProductType.SelectedValue = "Yarn";                
                ddlProductType.Enabled = false;

            }

        }
        catch
        {
            throw;
        }


    }

    protected void ddlBland21_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            bindFabricBland();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Category.\r\nSee error log for detail."));
        }
    }

    public void bindFabricBland()
    {
        try
        {
            var dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetYarnDetails();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBland21.Items.Clear();
                ddlBland21.DataSource = dt;
                ddlBland21.DataTextField = "YARN_DESC";
                ddlBland21.DataValueField = "YARN_CODE";
                ddlBland21.DataBind();
                ddlBland21.Items.Insert(0,new Obout.ComboBox.ComboBoxItem("----Select---", ""));                
                //ddlBland21.Items.Insert(0, new ListItem("----Select---", ""));
              
            }
        }
        catch
        {
            throw;
        }


    }

    //protected void BindArticleCode()
    //{
    //    try
    //    {
    //        DataTable dtallbasedetail = SaitexBL.Interface.Method.TX_FABRIC_MST.GetYarnDetails();
    //        DataView dvBaseArticle = new DataView(dtallbasedetail);
    //        //dvBaseArticle.RowFilter = "ARTICLE_TYPE='" + ddlProductType.SelectedItem.ToString() + "'";
    //        if (dvBaseArticle != null && dvBaseArticle.Count > 0)
    //        {

    //            txtBaseArticleCode.Items.Clear();
    //            txtBaseArticleCode.DataSource = dvBaseArticle;
    //            txtBaseArticleCode.DataValueField = "YARN_CODE";
    //            txtBaseArticleCode.DataTextField = "YARN_CODE";
    //            txtBaseArticleCode.DataBind();
    //            txtBaseArticleCode.Items.Insert(0, new ListItem("----Select---", ""));

    //        }
    //        else
    //        {
    //           txtBaseArticleCode.Items.Clear();
    //           txtBaseArticleCode.Items.Insert(0, new ListItem("----Select----", ""));
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }

    //}

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindFabricBland();

    }

    protected void grdBlandTran_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
                countflag = 1;
                ViewState["countflag"] = countflag;
                FillDetailByGrid(UniqueId);

            }
            else if (e.CommandName == "indentDelete")
            {               
                DeleteBlandDetailRow(UniqueId);
                BindBlandDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Bland Grid Row Command.\r\nSee error log for detail."));
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {

        try
        {
            if (ViewState["dtBlandDetail"] != null)
                dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
            var dv = new DataView(dtBlandDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                bindFabricBland("YARN_BLAND");
                ddlBland2.SelectedValue = dv[0]["BlendArticle"].ToString();
                txtbland1percentage0.Text = dv[0]["Percentage"].ToString();
                txtBlandRemarks.Text = dv[0]["Remarks"].ToString();
               // txtCount.Text = dv[0]["Count"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteBlandDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtBlandDetail"] != null)
                dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
            if (grdBlandTran.Rows.Count == 1)
            {
                dtBlandDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtBlandDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtBlandDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtBlandDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtBlandDetail"] = dtBlandDetail;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlBland21_SelectedIndexChanged(object sender, EventArgs e)
    {
        var DT = SaitexBL.Interface.Method.TX_FABRIC_MST.GetYarnCount(ddlBland21.SelectedValue);
        if (DT.Rows.Count > 0 && DT != null)
        {
            txtCount.ReadOnly = false;
            txtCount.Text = DT.Rows[0]["Y_COUNT"].ToString();
            lblYarnDesc.Text = DT.Rows[0]["YARN_DESC"].ToString();
            txtCount.ReadOnly = true;
        }
    }

    protected bool CheckValidation()
    {
        bool result = false;
        if (string.IsNullOrEmpty(txtFabricCode.Text))
        {
            CommonFuction.ShowMessage("Please enter fabric code.");
            result = true;
        }
        if (ddlFabricType.SelectedIndex < 1)
        {
            CommonFuction.ShowMessage("Please select fabric type.");
            result = true;
        }
        if (ddlFabricCategory.SelectedIndex < 1)
        {
            CommonFuction.ShowMessage("Please select fabric category.");
            result = true;
        }
        
        return result;

    }

    protected void ddlFabricCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
         BindFabricCodeinFindMode();
    }

    protected void ddlfabriccode_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)   
    { try
        {
            tdSave.Visible = false;
            ddlFabricCode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            var dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFiberMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                bindFabricType("FABRIC_TYPE");
                bindFabricCategory("FABRIC_CATEGORY");                
                bindFabricFilament("FABRIC_FILAMENT");
                bindFabricUOM("UOM");                         

                string text = ddlFabricCode.SelectedValue.ToString();
                var dv = new DataView(dt);
                dv.RowFilter = "FABR_CODE='" + text + "'";
                if (dv != null && dv.Count > 0)
                {
                    txtFabricCode.Text = dv[0]["FABR_CODE"].ToString();
                    txtStitchLenght.Text = dv[0]["STITCH_LENGTH"].ToString();
                    txtGauge.Text = dv[0]["GAUGE"].ToString();
                    txtDiameter.Text = dv[0]["DIAMETER"].ToString();                   
                    txtGSM.Text = dv[0]["GSM"].ToString();
                    txtFabricRemarks.Text = dv[0]["FABR_DESC"].ToString();
                    txtOpeningBalanceStock.Text = dv[0]["OP_BAL_STOCK"].ToString();
                    txtMimimumStock.Text = dv[0]["MIN_STOCK"].ToString();
                    txtMinimumProcureDays.Text = dv[0]["MIN_PROCURE_DAYS"].ToString();
                    txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                    txtRecorderLevel.Text = dv[0]["REORDER_LEVEL"].ToString();
                    txtRecorderQuantity.Text = dv[0]["REORDER_QUANTITY"].ToString();
                    txtMaximumStock.Text = dv[0]["MAX_STOCK"].ToString();


                    ddlFabricType.SelectedIndex = ddlFabricType.Items.IndexOf(ddlFabricType.Items.FindByText(dv[0]["FABR_TYPE"].ToString())); ;
                    ddlFabricCategory.SelectedIndex = ddlFabricCategory.Items.IndexOf(ddlFabricCategory.Items.FindByText(dv[0]["FABR_CATEGORY"].ToString())); ;
                    ddlFilament.SelectedIndex = ddlFilament.Items.IndexOf(ddlFilament.Items.FindByText(dv[0]["FILAMENT"].ToString())); ;
                    if (!string.IsNullOrEmpty(dv[0]["FABR_SUB_CATEGORY"].ToString()))
                    {                
                        bindFabricSubCategory("FABRIC_SUB_CATEGORY", ddlFabricCategory.SelectedItem.ToString());
                        ddlFabricSubCategory.SelectedValue= dv[0]["FABR_SUB_CATEGORY"].ToString();
                    } 
                    ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByText(dv[0]["UOM"].ToString()));
                    var dtBland = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricBlandDetailByFabricCode(text);
                    if (dtBland != null && dtBland.Rows.Count > 0)
                    {
                        MapDataTable(dtBland);
                        if (ViewState["dtBlandDetail"] != null)
                            dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
                        grdBlandTran.DataSource = dtBlandDetail;
                        grdBlandTran.DataBind();

                        DataTable dtBaseArtilce = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabricCodeBaseArticleByFabricCode(text);
                        if (dtBaseArtilce != null && dtBaseArtilce.Rows.Count > 0)
                        {
                            if (ViewState["dtBaseArticleDetail"] != null)
                                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
                            MapBaseArticleRowDataTable(dtBaseArtilce);
                            grdBaseArticleDetail.DataSource = dtBaseArticleDetail;
                            grdBaseArticleDetail.DataBind();
                        }
                    }
                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }
         
    }

    private void BindFabricCodeinFindMode()
    {

        try
        {

            string CommandText = "select * from (select FABR_CODE,FABR_TYPE,FABR_DESC, FABR_CATEGORY,FABR_DESC||'---'||  FABR_TYPE ||'---'|| FABR_CATEGORY as Combined   from TX_FABRIC_MST )a";
            string WhereClause = "  where a.FABR_CODE like :SearchQuery  or a.FABR_DESC like :SearchQuery";
            string SortExpression = "  order by a.FABR_CATEGORY  asc";
            string SearchQuery = "%";
            var data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            if (data != null)
            {
                if (data.Rows.Count > 0)
                {
                    ddlFabricCode.Items.Clear();
                    ddlFabricCode.DataTextField = "Combined";
                    ddlFabricCode.DataValueField = "FABR_CODE";
                    ddlFabricCode.DataSource = data;
                    ddlFabricCode.DataBind();
                    // ddlyarncode.Items.Insert(0, new ListItem("------Select------", "0"));
                }
            }


        }
        catch
        {

        }
    }

    private void DisableOrInableFieldinUpdateforFabriccode(bool value)
    {

        try
        {
            ddlBland2.Enabled = value;
            txtbland1percentage0.Enabled = value;
            txtBlandRemarks.Enabled = value;
           // txtCount.Enabled = value;
            btmSave.Enabled = value;
            btnCancel.Enabled = value;
            grdBlandTran.Enabled = value;
            ddlFabricType.Enabled = value;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtBlandDetail"] != null)
                dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
            if (dtBlandDetail == null || dtBlandDetail.Rows.Count == 0)
                CreateBlandDetailTable();
            dtBlandDetail.Rows.Clear();
            int currentyear = oUserLoginDetail.DT_STARTDATE.Year;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    if (currentyear == int.Parse(drTemp["YEAR"].ToString()))
                    {
                        var dr = dtBlandDetail.NewRow();
                        dr["UniqueId"] = dtBlandDetail.Rows.Count + 1;
                        dr["BlendArticle"] = drTemp["BLEND"];
                        dr["BlendArticleDesc"] = drTemp["BLEND_DESC"];
                        dr["Percentage"] = drTemp["BLEND_PER"];
                       // dr["Count"] = drTemp["COUNT"];
                        dr["Remarks"] = drTemp["REMARKS"];
                        dtBlandDetail.Rows.Add(dr);
                    }
                }
                dtTemp = null;
                ViewState["dtBlandDetail"] = dtBlandDetail;
            }
        }
        catch
        {
            throw;
        }
    }

    private void RefreshAllControls()
    {
        Initialize();
        RefreshDetailRow();
        RefresBaseArticleRow();
        grdBlandTran.DataSource = null;
        grdBlandTran.DataBind();
        grdBaseArticleDetail.DataSource = null;
        grdBaseArticleDetail.DataBind();
        dtBlandDetail.Dispose();
        ViewState["dtBlandDetail"] = null;
    }


    /***************************** CREATED BY NISHANT RAI AT 28_01_2014**********************************/
}
