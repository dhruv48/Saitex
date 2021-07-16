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
using System.Drawing;
using Obout.ComboBox;


public partial class Module_Yarn_SalesWork_Pages_YarnMaster : System.Web.UI.Page
{
    DataTable dtColorDetail;
    DataTable dtTRN_SUB;
    DataTable dtAssociatedItemDetail;
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
    SaitexDM.Common.DataModel.YRN_MST oYRNMST = new SaitexDM.Common.DataModel.YRN_MST();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
           
            Page.MaintainScrollPositionOnPostBack = true;
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                RefreshControls();
                Initialize();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));

        }
    }

    private void Initialize()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);           
            PackageTR.Visible = false;
            Package1TR.Visible = false;          
            YarnComp.Visible = false;           
            ConRate.Visible = false;
            DivValues.Visible = false;          
            Singleplymsgtr.Visible = false;            
            EnableFieldinUpdateforYarncode();
            this.CreateBaseArticleDetailTable();
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            txtYarnCode.Text = string.Empty; //(int.Parse(SaitexBL.Interface.Method.YRN_MST.GenrateShortCodeDuplicasyCheck().ToString()) + 1).ToString();
            lblMode.Text = "Save";
            bindCategory("YARN_CAT");
            bindClassification("YARN_CLASSIFICATION");
            BindFancyEffect("FIBER_FANCY_EFFECT");
            bindCount("YARN_COUNT");          
            bindYarnBland("YARN_BLAND");
            bindYarnUOM("UOM");
            bindDEJ("PO_NATURE");
            BindMultiTPM("MULTI_TPM");
            bindYarnPLY("YARN_PLY");
            bindYarnCatType("YARN_CAT_TYPE");           
            bindBaseUOM("UOM");
            bindYarnTWIST("YARN_TWIST");
            bindYarnCOATING("YARN_COATING");
            bindYarnENDUSE("END_USE");
            bindYarnSHADE("YARN_SHADE");
            bindYarnPROCESS("YARN_PROCESS");
            bindBlendingProcess("BLENDING_PROCESS");
            bindClassification("YARN_CLASSIFICATION");
            bindBaseDirection("ITCHS_CUSTOM");
            bindSHADE_CODE("YARN_SHADE");
            bindMILANGE_CODE("MILANGE_CODE");
            //BindITCHSCode("ITCHS_SALES", ddlSales_ITCHS);
            //BindITCHSCode("ITCHS_CUSTOM", ddlCustom_ITCHS);
           // BindITCHSCode("TARIFF_HEADING", ddlTariffHeading);
            //bindColor("COLOR");
            CreateBlandDetailTable();
           // bindYarnSupplier();
            txtYarnCode.Visible = true;
            bindYarnBAseProductType();         
            BindBaseBASIS();
            //lblArticalCode.Text = string.Empty;
            bindYarnType("YARN_TYPE", ddlCatType.SelectedValue.ToString());
            BindYarnCode();
            BindArticleCode();
        }
        catch
        {
            throw; 
        }
    }


    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (CheckValidation("Save"))
            {
                return;
            }
            int total_per = 0;

            foreach (GridViewRow grow  in grdBaseArticleDetail.Rows)
            {
             }
            if (total_per != 100)
            {
                CommonFuction.ShowMessage("Total Yarn Composition(Blend%) should be 100.");
                return;
            }
            string yarn_code = SaitexBL.Interface.Method.YRN_MST.Get_YarnMasterCodeDescDuplicate(txtYarnDescription.Text.Trim().ToUpper());
            if (!string.IsNullOrEmpty(yarn_code))
            {
                CommonFuction.ShowMessage("Yarn Description is already exist. Yarn Code is : " + yarn_code);
                return;
            }
            if (Page.IsValid)
            {
                if (ViewState["dtBaseArticleDetail"] != null)
                    dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];

                if (dtBaseArticleDetail.Rows.Count > 0)
                {
               
                    if (ViewState["dtBlandDetail"] != null)
                    dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];

                    int iRecordFound = 0;
                    oYRNMST.YARN_CAT = ddlYarnCat.SelectedItem.ToString();
                    oYRNMST.CLASSIFICATION  = ddlClassification.SelectedValue;
                    oYRNMST.YARN_TYPE = ddlYarnType.SelectedValue.ToString();
                    oYRNMST.YARN_DESC = Common.CommonFuction.funFixQuotes(txtYarnDescription.Text.Trim().ToUpper());
                    oYRNMST.Y_COUNT = double.Parse(Common.CommonFuction.funFixQuotes(ddlCount.SelectedValue.ToString()));
                    oYRNMST.COUNT_VALUE = Common.CommonFuction.funFixQuotes(txtCountValue.Text.Trim());
                    oYRNMST.UOM = ddlUOM.SelectedItem.ToString();
                    oYRNMST.COLOUR = string.Empty;// Common.CommonFuction.funFixQuotes(ddlColor.Text.Trim());
                    if (ddlPly.SelectedIndex < 0)
                        oYRNMST.PLY = string.Empty;
                    else
                    oYRNMST.PLY = Common.CommonFuction.funFixQuotes(ddlPly.Text.Trim());
                    oYRNMST.YBIN_CODE ="";
                    double CSP = 0;
                    oYRNMST.CSP = CSP;
                    oYRNMST.STATUS = "";

                    double Uster = 0;
                    oYRNMST.USTER = Uster;

                    double Hairness = 0;
                    oYRNMST.HAIRINESS = Hairness;

                    double Classimate = 0;
                    oYRNMST.CLASSIMATE = Classimate;

                    oYRNMST.YARN_SUPPLIER = ""; 
                    oYRNMST.BRAND_NAME = "";
                    oYRNMST.MANUFACTURER = "";
                    oYRNMST.REMARKS = "";
                    oYRNMST.SORT_NAME = "";
                     int VARIANCE_STRENGTH = 0;
                    oYRNMST.VARIANCE_STRENGTH = VARIANCE_STRENGTH;
                  
                       oYRNMST.IS_EXCISEABLE = false;
                       double countCV = 0;
                       double.TryParse(txtCountCV.Text, out countCV);
                       oYRNMST.COUNT_CV = countCV;
                      double minstock = 0;
                      dd = Common.CommonFuction.ToDouble(txtMimimumStock.Text.Trim(), "MimimumStock", 10, 2, out msg, out minstock);

                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";

                    }
                    else
                    {
                        oYRNMST.MIN_STOCK = minstock;
                    }
                    double minporcuredays = 0;
                    dd = Common.CommonFuction.ToDouble(txtMinimumProcureDays.Text.Trim(), "MinimumProcureDays", 12, 2, out msg, out minporcuredays);
                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";
                    }
                    else
                    {
                        oYRNMST.MIN_PROCURE_DAYS = minporcuredays;
                    }
                    Int64 findebcode = 0;
                    Int64.TryParse(txtFindDepCode.Text, out findebcode);
                    oYRNMST.FIN_DEB_CODE = findebcode; //Convert.ToInt64(Common.CommonFuction.funFixQuotes(txtFindDepCode.Text.Trim()));
                    double opblancestock = 0;
                    dd = Common.CommonFuction.ToDouble(txtOpeningBalanceStock.Text.Trim(), "OpeningBalanceStock", 10, 3, out msg, out opblancestock);
                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";
                    }
                    else
                    {
                        oYRNMST.OPENING_BALANCE_STOCK = opblancestock;
                    }
                    double openingrate = 0;
                    dd = Common.CommonFuction.ToDouble(txtOpeningRate.Text.Trim(), "OpeningRate", 16, 4, out msg, out openingrate);
                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";
                    }
                    else
                    {
                        oYRNMST.OPENING_RATE = openingrate;
                    }
                    oYRNMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oYRNMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oYRNMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oYRNMST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                    oYRNMST.TUSER = oUserLoginDetail.UserCode;
                    long TotalImp = 0;
                    long.TryParse(Common.CommonFuction.funFixQuotes(txtTotalImp.Text.Trim()), out TotalImp);
                    oYRNMST.TOTAL_IMP = TotalImp;
                    double RecorderLevel = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtRecorderLevel.Text.Trim()), out RecorderLevel);
                    oYRNMST.REORDER_LEVEL = RecorderLevel;
                    double ReorderQuantity = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtRecorderQuantity.Text.Trim()), out ReorderQuantity);
                    oYRNMST.REORDER_QUANTITY = ReorderQuantity;
                    double PACKAGE_SIZE = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtpackageSize.Text.Trim()), out PACKAGE_SIZE);
                    oYRNMST.PACKAGE_SIZE = PACKAGE_SIZE;
                    oYRNMST.YARN_QUALITY = ddlQuality.SelectedValue.ToString();
                    oYRNMST.YARN_DEJ = ddlDej.SelectedValue.ToString();
                    double MAX_STOCK = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtMaximumStock.Text.Trim()), out MAX_STOCK);

                    oYRNMST.MAX_STOCK = MAX_STOCK;

                    oYRNMST.TPM1 = Common.CommonFuction.funFixQuotes(txtTpm1.Text.Trim());
                    oYRNMST.TPM2 = Common.CommonFuction.funFixQuotes(txtTpm2.Text.Trim());

                    oYRNMST.DIRECTION1 = Common.CommonFuction.funFixQuotes(txtDirection1.Text.Trim());
                    oYRNMST.DIRECTION2 = Common.CommonFuction.funFixQuotes(txtDirection2.Text.Trim());

                    oYRNMST.REMARKS1 = Common.CommonFuction.funFixQuotes(txtRemarks1.Text.Trim());
                    oYRNMST.REMARKS2 = Common.CommonFuction.funFixQuotes(txtRemarks2.Text.Trim());

                    oYRNMST.TRAFIF_SUB_HEADING = Common.CommonFuction.funFixQuotes(txttrarifSubheading.Text.Trim());
                    
                     /***************************** ADDED BY Arun Sharma **********************************/
                    oYRNMST.TWIST_DIRECTION = ddlTwistDirection.SelectedValue.Trim();
                    oYRNMST.FANCY_EFFECT = ddlFancyEffect.SelectedValue.ToString();
                    oYRNMST.COATING = ddlCoating.SelectedValue.Trim();
                    oYRNMST.YARN_SHADE =ddlYarnShade.SelectedValue.Trim();
                    oYRNMST.FILAMENT = ddlCatType.SelectedValue.Trim();
                    oYRNMST.ENDUSE = ddlEndUse.SelectedValue.Trim();
                    oYRNMST.BLENDING_PROCESS = ddlbBlendingProcess.SelectedValue.Trim();
                    oYRNMST.LOCATION = oUserLoginDetail.VC_BRANCHNAME;
                    oYRNMST.STORE = oUserLoginDetail.VC_DEPARTMENTNAME;
                    /***************************** ADDED BY Arun Sharma**********************************/
                    Int64 _tariff_heading = 0;
                    Int64.TryParse(ddlTariffHeading.SelectedValue, out _tariff_heading);
                    if (ddlSales_ITCHS.SelectedIndex < 0)
                        oYRNMST.SALES_ITCHS_CODE = string.Empty;
                    else
                    oYRNMST.SALES_ITCHS_CODE = ddlSales_ITCHS.SelectedValue.Trim();
                    oYRNMST.IS_EXCISABLE = rdIsExciable.SelectedValue;
                    oYRNMST.TARIFF_HEADING = _tariff_heading;
                   
                    oYRNMST.CUSTOM_ITCHS_CODE = ddlCustom_ITCHS.SelectedValue.Trim().ToString();
                    oYRNMST.CONVERSION_RATE = txtConversionRate.Text;
                    oYRNMST.VALUE1 = txtValue1.Text;
                    oYRNMST.VALUE2 = txtValue2.Text;
                    oYRNMST.HSN_CODE = txtHSNCODE.Text.Trim();

                    if (ViewState["dtBaseArticleDetail"] != null)
                        dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];

                    if (dtBaseArticleDetail.Rows.Count > 0)
                    {
                        oYRNMST.YARN_CODE = Common.CommonFuction.funFixQuotes(txtYarnCode.Text);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("No Composition is there");
                    }
                    if (ViewState["dtColorDetail"] != null)
                        dtColorDetail = (DataTable)ViewState["dtColorDetail"];

                    if (Session["dtTRN_SUB"] != null)
                        dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                    if (ViewState["dtAssociatedItemDetail"] != null)
                        dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
                    if (rad_qc_req.SelectedValue.Trim() == "yes")
                        oYRNMST.QC_REQUIRED = true;
                    else
                        oYRNMST.QC_REQUIRED = false;

                    if (Errormsg == string.Empty)
                    {
                        bool resutl = SaitexBL.Interface.Method.YRN_MST.InsertYarnMaster(oYRNMST, out iRecordFound, dtBlandDetail, dtBaseArticleDetail, dtColorDetail, dtTRN_SUB, dtAssociatedItemDetail);
                        if (resutl)
                        {
                            string Resultmsg = " Yarn Master Saved Successfully " + "\\r\\n";
                            Resultmsg += " Article Code is: " + oYRNMST.YARN_CODE;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                            RefreshControls();
                            Initialize();
                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Already Exists');", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Master Saving Failed!!');", true);
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(Errormsg);
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Atleast One Bland/Substrate!!");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving Yarn.\r\nSee error log for detail."));
        }
    }

    private void bindMILANGE_CODE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlSales_ITCHS.Items.Clear();
                ddlSales_ITCHS.DataSource = dt;
                ddlSales_ITCHS.DataTextField = "MST_CODE";
                ddlSales_ITCHS.DataValueField = "MST_CODE";
                ddlSales_ITCHS.DataBind();
                ddlSales_ITCHS.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void bindBaseDirection(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCustom_ITCHS.Items.Clear();
                ddlCustom_ITCHS.DataSource = dt;
                ddlCustom_ITCHS.DataTextField = "MST_CODE";
                ddlCustom_ITCHS.DataValueField = "MST_CODE";
                ddlCustom_ITCHS.DataBind();
                ddlCustom_ITCHS.Items.Insert(0, new ListItem("------Select------", "Select"));
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
                ddlFancyEffect.Items.Insert(0, new ListItem("------Select------", "Select"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindMultiTPM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtDirection2.Items.Clear();
                txtDirection2.DataSource = dt;
                txtDirection2.DataTextField = "MST_CODE";
                txtDirection2.DataValueField = "MST_CODE";
                txtDirection2.DataBind();
                txtDirection2.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void bindYarnTWIST(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTwistDirection.Items.Clear();
                ddlTwistDirection.DataSource = dt;
                ddlTwistDirection.DataTextField = "MST_CODE";
                ddlTwistDirection.DataValueField = "MST_CODE";
                ddlTwistDirection.DataBind();
                ddlTwistDirection.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindBlendingProcess(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlbBlendingProcess.Items.Clear();
                ddlbBlendingProcess.DataSource = dt;
                ddlbBlendingProcess.DataTextField = "MST_CODE";
                ddlbBlendingProcess.DataValueField = "MST_CODE";
                ddlbBlendingProcess.DataBind();
                ddlbBlendingProcess.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindYarnCOATING(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlCoating.Items.Clear();
                ddlCoating.DataSource = dt;
                ddlCoating.DataTextField = "MST_CODE";
                ddlCoating.DataValueField = "MST_CODE";
                ddlCoating.DataBind();
                ddlCoating.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindYarnENDUSE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlEndUse.Items.Clear();
                ddlEndUse.DataSource = dt;
                ddlEndUse.DataTextField = "MST_CODE";
                ddlEndUse.DataValueField = "MST_CODE";
                ddlEndUse.DataBind();
                ddlEndUse.Items.Insert(0, new ListItem("------Select------", ""));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindYarnSHADE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlYarnShade.Items.Clear();
                ddlYarnShade.DataSource = dt;
                ddlYarnShade.DataTextField = "MST_CODE";
                ddlYarnShade.DataValueField = "MST_CODE";
                ddlYarnShade.DataBind();                
                ddlYarnShade.SelectedIndex =ddlYarnShade.Items.IndexOf(ddlYarnShade.Items.FindByValue("GREY"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindSHADE_CODE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlBaseShadeCode.Items.Clear();
                ddlBaseShadeCode.DataSource = dt;
                ddlBaseShadeCode.DataTextField = "MST_CODE";
                ddlBaseShadeCode.DataValueField = "MST_CODE";
                ddlBaseShadeCode.DataBind();
                ddlBaseShadeCode.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlBaseShadeCode.Items.Insert(1, new ListItem("NA", "NA"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void bindYarnPROCESS(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlQuality.Items.Clear();
                ddlQuality.DataSource = dt;
                ddlQuality.DataTextField = "MST_CODE";
                ddlQuality.DataValueField = "MST_CODE";
                ddlQuality.DataBind();
                ddlQuality.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindYarnType(string MST_NAME, string TYPE)
    {
        try
        {
            //if (!string.IsNullOrEmpty(TYPE))
            //{
                DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    //dv.RowFilter = "MST_DESC='" + TYPE + "'";
                    if (dv != null && dv.Count > 0)
                    {
                        ddlYarnType.Items.Clear();
                        ddlYarnType.DataSource = dv;
                        ddlYarnType.DataTextField = "MST_CODE";
                        ddlYarnType.DataValueField = "MST_CODE";
                        ddlYarnType.DataBind();
                        ddlYarnType.Items.Insert(0, new ListItem("------Select------", ""));
                    }
                    else
                    {
                        ddlYarnType.Items.Clear();
                        ddlYarnType.Items.Insert(0, new ListItem("------NoItems------", ""));

                    }

                }
            //}
        }
        catch
        {
            throw;
        }


    }

    public void bindYarnQuality(string MST_NAME, string TYPE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlQuality.Items.Clear();
                    ddlQuality.DataSource = dv;
                    ddlQuality.DataTextField = "MST_CODE";
                    ddlQuality.DataValueField = "MST_CODE";
                    ddlQuality.DataBind();
                    ddlQuality.Items.Insert(0, new ListItem("------Select------", "0"));
                }
                else
                {
                    ddlQuality.Items.Clear();
                    ddlQuality.Items.Insert(0, new ListItem("------NoItems------", "0"));

                }
            }
        }
        catch
        {
            throw;
        }


    }

    public void FindSpunFilamentType(string MST_NAME, string YARNTYPE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                //dv.RowFilter = "MST_CODE='" + YARNTYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlCatType.SelectedValue = dv[0]["MST_DESC"].ToString();
                }

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindYarnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlYarnType.Items.Clear();
                ddlYarnType.DataSource = dt;
                ddlYarnType.DataTextField = "MST_DESC";
                ddlYarnType.DataValueField = "MST_DESC";
                ddlYarnType.DataBind();
                ddlYarnType.Items.Insert(0, new ListItem("------Select------", ""));

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindCount(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME_For_Count(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlCount.Items.Clear();
                ddlCount.DataSource = dt;
                ddlCount.DataTextField = "MST_DESC";
                ddlCount.DataValueField = "MST_DESC";
                ddlCount.DataBind();
                ddlCount.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }


    }

    private void BindYarnCode()
    {
        try
        {
            txtYarnCode.Enabled = true;
            string yType = string.Empty;
            string msg = string.Empty;
            yType = ddlYarnCat.SelectedValue;
            string yDesc=ddlCatType.SelectedValue;
            string PREFIX = string.Empty;
            if (yType !="0" )
            {
                 PREFIX = SaitexBL.Interface.Method.YRN_MST.GetPrefixCode(yType, yDesc);
            }
            string YarnCode = SaitexBL.Interface.Method.YRN_MST.GetFabricCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, yType.Trim(), PREFIX.ToUpper());
            txtYarnCode.Text = YarnCode;
            txtYarnCode.Enabled = false;

        }
        catch
        {
            throw;
        }
    }

    public void bindCategory(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlYarnCat.Items.Clear();
                ddlYarnCat.DataSource = dt;
                ddlYarnCat.DataTextField = "MST_DESC";
                ddlYarnCat.DataValueField = "MST_DESC";
                ddlYarnCat.DataBind();
                ddlYarnCat.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }


    }
    public void bindClassification(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlClassification.Items.Clear();
                ddlClassification.DataSource = dt;
                ddlClassification.DataTextField = "MST_DESC";
                ddlClassification.DataValueField = "MST_DESC";
                ddlClassification.DataBind();
                ddlClassification.Items.Insert(0, new ListItem("------Select------", ""));

            }
        }
        catch
        {
            throw;
        }


    }


    public void bindYarnPLY(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlPly.Items.Clear();
                ddlPly.DataSource = dt;
                ddlPly.DataTextField = "MST_CODE";
                ddlPly.DataValueField = "MST_CODE";
                ddlPly.DataBind();
                ddlPly.Items.Insert(0, new ListItem("------Select------", ""));

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindDEJ(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlDej.Items.Clear();
                ddlDej.DataSource = dt;
                ddlDej.DataTextField = "MST_DESC";
                ddlDej.DataValueField = "MST_DESC";
                ddlDej.DataBind();
                ddlDej.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindQuality(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlQuality.Items.Clear();
                ddlQuality.DataSource = dt;
                ddlQuality.DataTextField = "MST_DESC";
                ddlQuality.DataValueField = "MST_DESC";
                ddlQuality.DataBind();
                ddlQuality.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }


    }

  

    public void bindYarnCatType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlCatType.Items.Clear();
                ddlCatType.DataSource = dt;
                ddlCatType.DataTextField = "MST_DESC";
                ddlCatType.DataValueField = "MST_DESC";
                ddlCatType.DataBind();
                ddlCatType.Items.Insert(0, new ListItem("------Select------", ""));

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindYarnBland(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {



                ddlBland2.Items.Clear();
                ddlBland2.DataSource = dt;
                ddlBland2.DataTextField = "MST_DESC";
                ddlBland2.DataValueField = "MST_DESC";
                ddlBland2.DataBind();
                ddlBland2.Items.Insert(0, new ListItem("------Select------", "0"));



            }

        }
        catch
        {
            throw;
        }


    }

    public void bindYarnUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlUOM.Enabled = true;
                ddlUOM.Items.Clear();
                ddlUOM.DataSource = dt;
                ddlUOM.DataTextField = "MST_CODE";
                ddlUOM.DataValueField = "MST_CODE";
                ddlUOM.DataBind();
                ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlUOM.SelectedValue = "KGS";
               // ddlUOM.Enabled = false;
            }

        }
        catch
        {
            throw;
        }


    }

    //public void bindYarnSupplier()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
    //        if (dt != null && dt.Rows.Count > 0)
    //        {

    //            ddlYarnSupplier.Items.Clear();
    //            ddlYarnSupplier.DataSource = dt;
    //            ddlYarnSupplier.DataTextField = "PRTY_NAME";
    //            ddlYarnSupplier.DataValueField = "PRTY_CODE";
    //            ddlYarnSupplier.DataBind();
    //            ddlYarnSupplier.Items.Insert(0, new ListItem("---------------Select---------------", "0"));
    //        }

    //    }
    //    catch
    //    {
    //        throw;
    //    }


    //}

    public void bindYarnBAseProductType()
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
                ddlProductType.Items.Insert(1, new ListItem("NA", "NA"));
                //ddlProductType.SelectedIndex =2;
                if(dt.Rows.Count>0)
                {
                    ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue("Poy")); 
                }
                //ddlProductType.Enabled = false;
                
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
                ddlBaseUOM.SelectedValue = "KGS";
                ddlBaseUOM.Enabled = false;
            }

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

    private void RefreshControls()
    {
        try
        {
            txtYarnDescription.Text = string.Empty; 
            //ddlPly.SelectedIndex = -1;
            ddlFancyEffect.SelectedIndex = -1;
            //txtTPI.Text = string.Empty;


            txtCountCV.Text = string.Empty;
            txtMimimumStock.Text ="0";
            txtOpeningBalanceStock.Text = "0";
            txtMinimumProcureDays.Text = "0";
            txtOpeningRate.Text = "0";
            txtFindDepCode.Text = "0";
            txtRecorderLevel.Text = "0";
            txtRecorderQuantity.Text = "0";
            txtMaximumStock.Text = "0";

            txtCountCV.Text = ""; 
            txtTotalImp.Text = "";
            
            grdBlandTran.DataSource = null;
            grdBlandTran.DataBind();
            ddlyarncode.SelectedIndex = -1;
            txtCountValue.Text = "";
            txtpackageSize.Text = "";
            grdBlandTran.DataSource = null;
            grdBlandTran.DataBind();
            
            grdBaseArticleDetail.DataSource = null;
            grdBaseArticleDetail.DataBind();
            //ddlYarnType.SelectedIndex = -1;            
            txttrarifSubheading.Text = string.Empty;
            ViewState["dtColorDetail"]=null;
            Session["dtTRN_SUB"] = null;
            ViewState["dtBlandDetail"]=null;
           ViewState["dtBaseArticleDetail"]=null;
           ViewState["dtAssociatedItemDetail"] = null;
            grdColorDetail.DataSource = null;
            grdColorDetail.DataBind();

            grdAssociatedYarn.DataSource = null;
            grdAssociatedYarn.DataBind();
            
            txtHSNCODE.Text = "";
           // ddlSales_ITCHS.SelectedIndex = -1;
            ddlCustom_ITCHS.SelectedIndex = -1;
            ddlTariffHeading.SelectedIndex = -1;
            rdIsExciable.SelectedValue = "1";
            txtConversionRate.Text = string.Empty;
            txtValue2.Text = string.Empty;
            txtValue1.Text = string.Empty;

            txtTpm1.Text = string.Empty;
            txtDirection1.Text = string.Empty;
            txtRemarks1.Text = string.Empty;
            //****************** Commented By Nishant Rai at 29-7-13***********************//
            //txtBinCode.Text = string.Empty;
            //txtCsP.Text = string.Empty;
            //txtUster.Text = "";
            //txtHairness.Text = "";
            //txtClassimate.Text = "";
            //ddlYarnSupplier.SelectedIndex = -1;
            //txtBrandName.Text = "";
            //txtManufaturer.Text = "";
            //txtRemarks.Text = "";
            //txtSortName.Text = "";
            //****************** Commented By Nishant Rai at 29-7-13***********************//

            txtAssocatedItemCode.Text = string.Empty;
            txtAssocatedItemDesc.Text = string.Empty;
        }
        catch
        {
            throw;
        }

    }

    //public string GenrateShortCode()
    //{
    //    //string ShortCode = string.Empty;
    //    //ShortCode = DateTime.Now.ToString();
    //    //ShortCode = ImageFileName.Replace(":", "");
    //    //ShortCode = ImageFileName.Replace("/", "");
    //    //ShortCode = ImageFileName.Replace(" ", "");
    //    //ShortCode = ImageFileName.Replace("AM", "01");
    //    //ShortCode = ImageFileName.Replace("PM", "02");

    //    //string YARN_CODE = "0000000001";
    //    string yarnno = SaitexBL.Interface.Method.YRN_MST.GenrateShortCodeDuplicasyCheck();
    //    if (yarnno != string.Empty)
    //    {
    //        long  shortcode = Convert.ToInt64 (yarnno);
    //        shortcode++;
    //        if (shortcode < 10)
    //        {
    //            prefix = "000000000";
    //            newShortcode = prefix + shortcode.ToString();

    //        }
    //        else if (shortcode < 100)
    //        {

    //            prefix = "00000000";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 1000)
    //        {

    //            prefix = "0000000";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 10000)
    //        {

    //            prefix = "000000";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 100000)
    //        {

    //            prefix = "00000";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 1000000)
    //        {

    //            prefix = "0000";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 10000000)
    //        {

    //            prefix = "000";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 100000000)
    //        {

    //            prefix = "00";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode < 100000000)
    //        {

    //            prefix = "0";
    //            newShortcode = prefix + shortcode.ToString();
    //        }
    //        else if (shortcode == 100000000)
    //        {

    //            prefix = "0000000001";
    //            newShortcode = prefix + shortcode.ToString();
    //        }



    //    }
    //    else
    //    {

    //        newShortcode = "0000000001";
    //    }
    //    return newShortcode;

    //}

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Save";

            Response.Redirect("~/Module/yarn/SalesWork/Pages/yarnMaster.aspx", false);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clearing Form Data.\r\nSee error log for detail."));

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

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (CheckValidation("Update"))
            {
                return;
            }
            //Comment code by surendra sir
            int total_per = 0;


            foreach (GridViewRow grow in grdBaseArticleDetail.Rows)
            {
                total_per = total_per + int.Parse(((Label)grow.Cells[2].FindControl("lblValueQty")).Text);
            }

            if (total_per != 100)
            {
                CommonFuction.ShowMessage("Total Yarn Composition(Blend%) should be 100.");
                return;
            }


            //string yarn_desc = generatedescription();
            //string yarn_code = SaitexBL.Interface.Method.YRN_MST.Get_YarnMasterCodeDescDuplicate(yarn_desc);
            string yarn_code = SaitexBL.Interface.Method.YRN_MST.Get_YarnMasterCodeDescDuplicate(txtYarnDescription.Text.Trim().ToUpper());

            if (!string.IsNullOrEmpty(yarn_code) && txtYarnCode.Text.Trim() != yarn_code)
            {
                CommonFuction.ShowMessage("Yarn Description is already exist. Yarn Code is : " + yarn_code);
                return;
            }
            //txtYarnDescription.Text = yarn_desc.ToUpper().Trim();
            if (Page.IsValid)
            {
                if (ViewState["dtBaseArticleDetail"] != null)
                    dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];

                if (dtBaseArticleDetail.Rows.Count > 0)
                {
                    if (ViewState["dtBlandDetail"] != null)
                        dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];

                    int iRecordFound = 0;

                    oYRNMST.YARN_CODE = txtYarnCode.Text.Trim();
                    oYRNMST.YARN_CAT = ddlYarnCat.SelectedItem.ToString();
                    oYRNMST.CLASSIFICATION = ddlClassification.SelectedValue;
                    oYRNMST.YARN_TYPE = ddlYarnType.SelectedValue.ToString();
                    oYRNMST.YARN_DESC = Common.CommonFuction.funFixQuotes(txtYarnDescription.Text.Trim().ToUpper());

                    oYRNMST.Y_COUNT = double.Parse(Common.CommonFuction.funFixQuotes(ddlCount.SelectedValue.ToString()));

                    oYRNMST.COUNT_VALUE = Common.CommonFuction.funFixQuotes(txtCountValue.Text.Trim());



                    oYRNMST.UOM = ddlUOM.SelectedItem.ToString();
                    oYRNMST.COLOUR = string.Empty;// Common.CommonFuction.funFixQuotes(ddlColor.Text.Trim());

                    //long lPLY = 0;
                    //long.TryParse(Common.CommonFuction.funFixQuotes(ddlPly.Text.Trim()), out lPLY);
                    oYRNMST.PLY = Common.CommonFuction.funFixQuotes(ddlPly.Text.Trim());


                    //**************************** Commented By Nishant Rai at *****************************//
                    oYRNMST.YBIN_CODE = "";
                    //Common.CommonFuction.funFixQuotes(txtBinCode.Text.Trim());

                   double CSP = 0;
                    //double.TryParse(Common.CommonFuction.funFixQuotes(txtCsP.Text.Trim()), out CSP);
                    oYRNMST.CSP = CSP;

                    oYRNMST.STATUS = "";
                    //ddlStatus.SelectedItem.ToString();

                    double Uster = 0;
                    //double.TryParse(Common.CommonFuction.funFixQuotes(txtUster.Text.Trim()), out Uster);
                    oYRNMST.USTER = Uster;
                    double Hairness = 0;
                    //double.TryParse(Common.CommonFuction.funFixQuotes(txtHairness.Text.Trim()), out Hairness);
                    oYRNMST.HAIRINESS = Hairness;

                    double Classimate = 0;
                    //double.TryParse(Common.CommonFuction.funFixQuotes(txtClassimate.Text.Trim()), out Classimate);
                    oYRNMST.CLASSIMATE = Classimate;

                    oYRNMST.YARN_SUPPLIER = "";
                    //ddlYarnSupplier.SelectedItem.ToString();
                    oYRNMST.BRAND_NAME = "";
                    //Common.CommonFuction.funFixQuotes(txtBrandName.Text.Trim());
                    oYRNMST.MANUFACTURER = "";
                    //Common.CommonFuction.funFixQuotes(txtManufaturer.Text.Trim());
                    oYRNMST.REMARKS = "";
                    //Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());

                    int VARIANCE_STRENGTH = 0;
                    //int.TryParse(Common.CommonFuction.funFixQuotes(txtVarience.Text), out VARIANCE_STRENGTH);
                    oYRNMST.VARIANCE_STRENGTH = VARIANCE_STRENGTH;

                    oYRNMST.SORT_NAME = "";
                    //Common.CommonFuction.funFixQuotes(txtSortName.Text.Trim());
                    //if (IsExciseable.Checked)
                    //{
                    //    oYRNMST.IS_EXCISEABLE = true;
                    //}
                    //else
                    //{
                        oYRNMST.IS_EXCISEABLE = false;
                    //}


                        double countcv = 0;
                        double.TryParse(txtCountCV.Text, out countcv);
                        oYRNMST.COUNT_CV = countcv;
                        //Convert.ToDouble(Common.CommonFuction.funFixQuotes(txtCountCV.Text.Trim()));
                  

                    double minstock = 0;

                    dd = Common.CommonFuction.ToDouble(txtMimimumStock.Text.Trim(), "MimimumStock", 10, 2, out msg, out minstock);

                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";

                    }
                    else
                    {
                        oYRNMST.MIN_STOCK = minstock;
                    }


                    double minporcuredays = 0;

                    dd = Common.CommonFuction.ToDouble(txtMinimumProcureDays.Text.Trim(), "MinimumProcureDays", 12, 2, out msg, out minporcuredays);

                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";

                    }
                    else
                    {
                        oYRNMST.MIN_PROCURE_DAYS = minporcuredays;
                    }

                    oYRNMST.FIN_DEB_CODE = Convert.ToInt64(Common.CommonFuction.funFixQuotes(txtFindDepCode.Text.Trim()));

                    double opblancestock = 0;

                    dd = Common.CommonFuction.ToDouble(txtOpeningBalanceStock.Text.Trim(), "OpeningBalanceStock", 10, 3, out msg, out opblancestock);

                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";

                    }
                    else
                    {
                        oYRNMST.OPENING_BALANCE_STOCK = opblancestock;
                    }


                    double openingrate = 0;

                    dd = Common.CommonFuction.ToDouble(txtOpeningRate.Text.Trim(), "OpeningRate", 16, 4, out msg, out openingrate);

                    if (!dd)
                    {
                        Errormsg += msg + "\\r\\n";

                    }
                    else
                    {
                        oYRNMST.OPENING_RATE = openingrate;
                    }
                    
                    oYRNMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oYRNMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oYRNMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oYRNMST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                    oYRNMST.TUSER = oUserLoginDetail.UserCode;
                    ///Added by Sandeep 10March11
                    long NO_OF_FILAMENT = 0;
                    //long.TryParse(Common.CommonFuction.funFixQuotes(txtNoOfFilament.Text.Trim()), out NO_OF_FILAMENT);
                    oYRNMST.NO_OF_FILAMENT = NO_OF_FILAMENT;

                  

                    long TotalImp = 0;
                    long.TryParse(Common.CommonFuction.funFixQuotes(txtTotalImp.Text.Trim()), out TotalImp);
                    oYRNMST.TOTAL_IMP = TotalImp; 

                    double RecorderLevel = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtRecorderLevel.Text.Trim()), out RecorderLevel);
                    oYRNMST.REORDER_LEVEL = RecorderLevel;

                    double ReorderQuantity = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtRecorderQuantity.Text.Trim()), out ReorderQuantity);
                    oYRNMST.REORDER_QUANTITY = ReorderQuantity;

                   
                    double PACKAGE_SIZE = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtpackageSize.Text.Trim()), out PACKAGE_SIZE);
                    oYRNMST.PACKAGE_SIZE = PACKAGE_SIZE;

                    //double TPM1 = 0;
                    //double.TryParse(Common.CommonFuction.funFixQuotes(txtTpm1.Text.Trim()), out TPM1);
                    oYRNMST.TPM1 = Common.CommonFuction.funFixQuotes(txtTpm1.Text.Trim());

                    //double TPM2 = 0;
                    //double.TryParse(Common.CommonFuction.funFixQuotes(txtTpm2.Text.Trim()), out TPM2);
                    oYRNMST.TPM2 = Common.CommonFuction.funFixQuotes(txtTpm1.Text.Trim());

                    oYRNMST.DIRECTION1 = Common.CommonFuction.funFixQuotes(txtDirection1.Text.Trim());
                    oYRNMST.DIRECTION2 = Common.CommonFuction.funFixQuotes(txtDirection2.Text.Trim());

                    oYRNMST.REMARKS1 = Common.CommonFuction.funFixQuotes(txtRemarks1.Text.Trim());
                    oYRNMST.REMARKS2 = Common.CommonFuction.funFixQuotes(txtRemarks2.Text.Trim());


                    if (ViewState["dtBaseArticleDetail "] != null)
                        dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail "];
                //------------------------------------------------------code chage by -surendra sir
                    if (dtBaseArticleDetail.Rows.Count > 0)
                    {
                        oYRNMST.ARTICLE_CODE = txtYarnCode.Text.Trim();
                            //GenrateArticleCode();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("No Composition is there");

                    }
                //-----------------------------------------------------------surendra sir
                    oYRNMST.YARN_QUALITY = ddlQuality.SelectedValue.ToString();
                    oYRNMST.YARN_DEJ = ddlDej.SelectedValue.ToString();
                   
                    double MAX_STOCK = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtMaximumStock.Text.Trim()), out MAX_STOCK);

                    oYRNMST.MAX_STOCK = MAX_STOCK;
                    oYRNMST.TRAFIF_SUB_HEADING = Common.CommonFuction.funFixQuotes(txttrarifSubheading.Text.Trim());
                   

                    /***************************** ADDED BY ARUN SHARMA**********************************/
                    oYRNMST.TWIST_DIRECTION = Common.CommonFuction.funFixQuotes(ddlTwistDirection.SelectedValue.Trim());
                    oYRNMST.FANCY_EFFECT = Common.CommonFuction.funFixQuotes(ddlFancyEffect.SelectedValue.Trim());
                    oYRNMST.COATING = Common.CommonFuction.funFixQuotes(ddlCoating.SelectedValue.Trim());
                    oYRNMST.YARN_SHADE = Common.CommonFuction.funFixQuotes(ddlYarnShade.SelectedValue.Trim());
                    oYRNMST.FILAMENT = Common.CommonFuction.funFixQuotes(ddlCatType.SelectedValue.Trim());
                    oYRNMST.ENDUSE = Common.CommonFuction.funFixQuotes(ddlEndUse.SelectedValue.Trim());
                    oYRNMST.BLENDING_PROCESS = Common.CommonFuction.funFixQuotes(ddlbBlendingProcess.SelectedValue.Trim());
                    oYRNMST.LOCATION = oUserLoginDetail.VC_BRANCHNAME;
                    oYRNMST.STORE = oUserLoginDetail.VC_DEPARTMENTNAME;
                    /***************************** ADDED BY ARUN SHARMA**********************************/

                    Int64 _tariff_heading = 0;
                   // Int64 _custom_itchs_code = 0;
                    Int64.TryParse(ddlTariffHeading.SelectedValue, out _tariff_heading);
                    oYRNMST.SALES_ITCHS_CODE = ddlSales_ITCHS.SelectedValue.Trim();
                    oYRNMST.IS_EXCISABLE = rdIsExciable.SelectedValue;
                    oYRNMST.TARIFF_HEADING = _tariff_heading;
                    
                    oYRNMST.CUSTOM_ITCHS_CODE = ddlCustom_ITCHS.SelectedValue.Trim(); ;
                    oYRNMST.CONVERSION_RATE = txtConversionRate.Text;
                    oYRNMST.VALUE1 = txtValue1.Text;
                    oYRNMST.VALUE2 = txtValue2.Text;
                    oYRNMST.HSN_CODE = txtHSNCODE.Text.Trim();

                    if (ViewState["dtColorDetail"] != null)
                        dtColorDetail = (DataTable)ViewState["dtColorDetail"];
                    if (Session["dtTRN_SUB"] != null)
                        dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                    if (ViewState["dtAssociatedItemDetail"] != null)
                        dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
                    if (rad_qc_req.SelectedValue.Trim() == "yes")
                        oYRNMST.QC_REQUIRED = true;
                    else
                        oYRNMST.QC_REQUIRED = false;

                    if (Errormsg == string.Empty)
                    {
                        bool resutl = SaitexBL.Interface.Method.YRN_MST.UpdateYarnMaster(oYRNMST, out iRecordFound, dtBlandDetail, dtBaseArticleDetail, dtColorDetail, dtTRN_SUB, dtAssociatedItemDetail);
                        if (resutl)
                        {
                            string Resultmsg = "Yarn Master Updated Successfully" + "\\r\\n";
                            Resultmsg += "Article Code is:" + oYRNMST.YARN_CODE;
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                            RefreshControls();   
                            Initialize();
                                                     

                        }
                        else if (iRecordFound > 0)
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Already Exists');", true);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Master Updation Failed!!');", true);

                           
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(Errormsg);

                    }

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Atleast One Bland/Substrate!!");

                }
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating Yarn Details.\r\nSee error log for detail."));
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
                        DataRow dr = dtBlandDetail.NewRow();
                        dr["UniqueId"] = dtBlandDetail.Rows.Count + 1;
                        dr["BlendArticle"] = drTemp["BLEND"];
                        dr["Percentage"] = drTemp["BLEND_PER"];
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
    private void MapAssociatedDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtAssociatedItemDetail"] != null)
                dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
            else 
                CreateAssociatedYarnDetailTable();
            dtAssociatedItemDetail.Clear();
           
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {

                    DataRow dr = dtAssociatedItemDetail.NewRow();
                    dr["AssUniqueId"] = dtAssociatedItemDetail.Rows.Count + 1;
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["ASS_YARN_CODE"] = drTemp["ASS_YARN_CODE"];
                    dr["ASS_YARN_DESC"] = drTemp["ASS_YARN_DESC"];
                    dtAssociatedItemDetail.Rows.Add(dr);
                    
                }
                dtTemp = null;
                ViewState["dtAssociatedItemDetail"] = dtAssociatedItemDetail;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlyarncode.Visible = true;
            tdSave.Visible = false;
            //ddlyarncode.Enabled = true;
            
            
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            txtYarnCode.Text = "";
            txtYarnCode.Visible = false;
            DisableFieldinUpdateforYarncode();
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));

        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Yarn_Mst_Opt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing.\r\nSee error log for detail."));

        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

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
                //  FillDataTableByGrid();
                DeleteBlandDetailRow(UniqueId);
                BindBlandDetailGrid();
                ViewState["UniqueId"] = null;
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
            DataView dv = new DataView(dtBlandDetail);

            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                bindYarnBland("YARN_BLAND");
                //ddlBland2.SelectedItem.Text = dv[0]["BlendArticle"].ToString();
                ddlBland2.SelectedValue = dv[0]["BlendArticle"].ToString();
                txtbland1percentage0.Text = dv[0]["Percentage"].ToString();
                txtBlandRemarks.Text = dv[0]["Remarks"].ToString();
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
    private bool SearchItemCodeInGrid(string BLEND,  int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdAssociatedYarn.Rows)
            {
                Label txtblendArtilce = (Label)grdRow.FindControl("txtblendArtilce");
                
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtblendArtilce.Text.Trim() == BLEND && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchItemCodeInGridComposition(string BLEND, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdBlandTran.Rows)
            {
                Label txtblendArtilce = (Label)grdRow.FindControl("txtblendArtilce");

                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtblendArtilce.Text.Trim() == BLEND && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void btmSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlBland2.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select type of blending.");
                return;
            }

            int percent = 0;
            int.TryParse(txtbland1percentage0.Text, out percent);

            if(percent > 100 || percent < 1 )
            {
                CommonFuction.ShowMessage("Percent should not be greater then 100 and less then 1.");
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
                        bool bb = SearchItemCodeInGridComposition(ddlBland2.SelectedItem.ToString().Trim(), UniqueId);
                        if (!bb)
                        {


                            if (UniqueId > 0)
                            {
                                DataView dv = new DataView(dtBlandDetail);
                                dv.RowFilter = "UniqueId=" + UniqueId;
                                if (dv.Count > 0)
                                {
                                    dv[0]["BlendArticle"] = ddlBland2.SelectedItem.ToString().Trim();
                                    dv[0]["Percentage"] = txtbland1percentage0.Text.Trim();
                                    dv[0]["Remarks"] = txtBlandRemarks.Text.Trim();

                                    dtBlandDetail.AcceptChanges();
                                }
                            }
                            else
                            {

                                DataRow dr = dtBlandDetail.NewRow();
                                dr["UniqueId"] = dtBlandDetail.Rows.Count + 1;
                                dr["BlendArticle"] = ddlBland2.SelectedItem.ToString().Trim();
                                dr["Percentage"] = txtbland1percentage0.Text.Trim();
                                dr["Remarks"] = txtBlandRemarks.Text.Trim();

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

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }

    private void CreateBlandDetailTable()
    {
        try
        {
            dtBlandDetail = new DataTable();
            dtBlandDetail.Columns.Add("UniqueId", typeof(int));
            dtBlandDetail.Columns.Add("BlendArticle", typeof(string));
            //dtBlandDetail.Columns.Add("BlendArticleID", typeof(string));
            dtBlandDetail.Columns.Add("Percentage", typeof(double));
            dtBlandDetail.Columns.Add("Remarks", typeof(string));
            ViewState["dtBlandDetail"] = dtBlandDetail;
        }
        catch
        {
            throw;
        }

    }
    private void CreateAssociatedYarnDetailTable()
    {
        try
        {
            dtAssociatedItemDetail = new DataTable();
            dtAssociatedItemDetail.Columns.Add("AssUniqueId", typeof(int));
            dtAssociatedItemDetail.Columns.Add("YARN_CODE", typeof(string));
            dtAssociatedItemDetail.Columns.Add("ASS_YARN_CODE", typeof(string));
            dtAssociatedItemDetail.Columns.Add("ASS_YARN_DESC", typeof(string));            
            ViewState["dtAssociatedItemDetail"] = dtAssociatedItemDetail;
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
            txtbland1percentage0.Text = "";
            txtBlandRemarks.Text = "";
            ViewState["UniqueId"] = null;
          }
        catch
        {
            throw;
        }
    }

    private void RefreshAssociatedYarnRows()
    {
        try
        {
            txtAssocatedItemCode.Text = string.Empty;
            txtAssocatedItemDesc.Text = string.Empty;
            txtAssocatedItemRemarks.Text = string.Empty;
            ViewState["AssUniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }
    //code change by Arun Sharma  11/12/2017........................
    protected void ddlCatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            txtAssocatedItemDesc.Text = txtYarnDescription.Text;
            txtAssocatedItemCode.Text = getAssociatedYarnCode(); 

            //bindYarnType("YARN_TYPE", ddlCatType.SelectedValue.ToString());


            //bindYarnQuality("YARN_QUALITY", ddlCatType.SelectedItem.ToString());
           // DisableFormFieldsByCategoryType();
            if (ddlCatType.SelectedValue == "SINGLE TWIST")
            {
                //lblCount.Text = "Denier";
                //lblCountValue.Text = "Denier Value";
                txtCountValue.Visible = true;
                lblNamrmalPly.Text = "Twist PLY";
                SingleTR.Visible = false;
                MultiDetails.Visible = false;
                PlyTr.Visible = false;
                lblCountValue.Visible = true;
                NormalDetails.Visible = true;
                NormalTPM.Visible = true;
                PLY1.Visible = true;
                ddlPly.Visible = true;
                lblNamrmalPly.Visible = true;
            }
            else if (ddlCatType.SelectedValue == "DOUBLE TWIST")
                {
                    lblNamrmalPly.Text = "Twist PLY";
                    lblNamrmalPly.Visible = true;
                    PLY1.Visible = true;
                    ddlPly.Visible = true;
                    lblmultiply.Text = "Multi PLY";
                    lblmultiply.Visible = false;
                    ddlEndUse.Visible = false;
                    SingleTR.Visible = true;
                    MultiDetails.Visible = false;
                    PlyTr.Visible = false;
                    NormalTPM.Visible =true;
                    NormalDetails.Visible = true;
                   
                }
            else if (ddlCatType.SelectedValue == "NON TWIST")
            {
                txtCountValue.Visible = true;
                ddlPly.Visible = false;
                lblNamrmalPly.Visible = false;
                SingleTR.Visible = false;
                MultiDetails.Visible = false;
                PlyTr.Visible = false;
                lblCountValue.Visible = true;
                //multiply.Visible = false;
                NormalDetails.Visible = false;
                NormalTPM.Visible = false;
            }

            else if (ddlCatType.SelectedValue == "MULTI FOLD")
            {
                txtCountValue.Visible = true;
                ddlPly.Visible = true;
                lblNamrmalPly.Text = "Twist PLY";
                lblNamrmalPly.Visible = true;
                SingleTR.Visible = true;
                MultiDetails.Visible = true;
                lblCountValue.Visible = true;
                ddlEndUse.Visible = true;
                ddlPly.Visible = true;
                PlyTr.Visible = true;
                NormalDetails.Visible = true;
                NormalTPM.Visible = true;
                //multiply.Visible = true;
                lblmultiply.Visible = true;
            }
            else
            {
                //lblCount.Text = "English Count";
                lblCountValue.Text = "Count Value";
                lblCountValue.Visible = false;
                txtCountValue.Visible = false;
            }

        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Spun /Filament  Selection.\r\nSee error log for detail."));

        }
    }

    protected void DisableFormFieldsByCategoryType()
    {

        try
        {
            if (ddlCatType.SelectedItem.ToString() == "SPUN")
            {
                //NoOfFilament.Visible = false;
                //NoOfFilament1.Visible = false;
                //HairinessIndex.Visible = true;
                //HairinessIndex1.Visible = true;
                //Classimate.Visible = true;
                //Classimate1.Visible = true;
                //Uster.Visible = true;
                //Uster1.Visible = true;
                // PLY.Visible = true;
                // PLY1.Visible = true;
                //Variencelbl.Visible = false;
                //Variencetxt.Visible = false;
                lblCountValue.Text = "Count Value";
                //lblCount.Text = "English Count";
            }
            else if (ddlCatType.SelectedItem.ToString() == "OPEN END")
            {
                //HairinessIndex.Visible = false;
                //HairinessIndex1.Visible = false;
                //Classimate.Visible = false;
                //Classimate1.Visible = false;
                //Uster.Visible = false;
                //Uster1.Visible = false;
                // PLY.Visible = false;
                //  PLY1.Visible = false;
                // NoOfFilament.Visible = true;
                // NoOfFilament1.Visible = true;
                //Variencelbl.Visible = true;
                //Variencetxt.Visible = true;
                lblCountValue.Text = "Denier Value";
                lblCount.Text = "Denier";
            }

        }
        catch
        {

            throw;
        }



    }

    protected string GenrateArticleCode()
    {

        try
        {
            string Substrate = string.Empty;
            for (int i = 0; i < grdBlandTran.Rows.Count; i++)
            {
                Label lblSubtrate = grdBlandTran.Rows[i].FindControl("txtblendArtilce") as Label;
                Substrate += lblSubtrate.Text.Substring(0, 1);
            }

            Substrate += ddlPly.SelectedValue.ToString();

            Substrate += ddlCount.SelectedValue.ToString();
            Substrate += ddlQuality.SelectedValue.ToString();
            Substrate += ddlDej.SelectedValue.ToString();
            for (int i = 0; i < grdBlandTran.Rows.Count; i++)
            {
                Label txtbland1percentage = grdBlandTran.Rows[i].FindControl("txtItemDesc") as Label;
                Substrate += txtbland1percentage.Text;
            }
            return Substrate;
        }
        catch
        {
            throw;
        }
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
                    Label txtbland1percentage = grdBlandTran.Rows[i].FindControl("txtItemDesc") as Label;
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

    protected void txtTPI_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    double tpi = 0;

        //    if (double.TryParse(txtTPI.Text, out tpi))
        //    {
        //        double tpm = tpi * 39.5;

        //        txtTPM.Text = tpm.ToString();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TPI.\r\nSee error log for detail."));
        //}
    }

    protected void txtTPM_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
        //    double tpm = 0;

        //    if (double.TryParse(txtTPM.Text, out tpm))
        //    {
        //        double tpi = tpm / 39.5;

        //        txtTPI.Text = tpi.ToString();
        //    }
        //}
        //catch (Exception ex)
        //{
        //    CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TPM.\r\nSee error log for detail."));
        //}
    }

    protected void btnGenerateArticalCode_Click(object sender, EventArgs e)
    {
        string GenratedArticlecode = string.Empty;
        //lblArticalCode.Text = string.Empty;
        try
        {
            if (ViewState["dtBlandDetail"] != null)
                dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
            if (dtBlandDetail.Rows.Count > 0)
            {

                GenratedArticlecode = GenrateArticleCode();
                if (GetArticleAvailability(string.Empty, GenratedArticlecode))
                {
                    //lblArticalCode.Text = string.Empty;
                    //lblArticalCode.Font.Bold = true;
                    txtYarnCode.Text = GenratedArticlecode;
                    //lblArticalCode.Text = GenratedArticlecode + "   This Article Code is Avaliable !!";
                    //lblArticalCode.ForeColor = System.Drawing.Color.Green;

                }
                else
                {

                    //lblArticalCode.Text = GenratedArticlecode + "  This Article Code is  Already Exist In Database.Please Try Another !!";
                    txtYarnCode.Text = string.Empty;
                    //lblArticalCode.ForeColor = System.Drawing.Color.Red;

                }
            }
            else
            {
                CommonFuction.ShowMessage("Please Create Atleast One Bland/Substrate To Genrate Aritlce Code");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in articale code generation.\r\nSee error log for detail."));
        }
    }

    protected bool GetArticleAvailability(string text, string ArticalCode)
    {
        try
        {
            bool Result = false;
            string whereClause = " where YARN_CODE like :searchQuery ";
            string sortExpression = " order by YARN_CODE asc";
            string commandText = "SELECT   *  FROM   (SELECT   YARN_CODE FROM   YRN_MST  WHERE   YARN_CODE = '" + ArticalCode.Trim() + "')  ASD";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    Result = false;

                }
                else
                {
                    Result = true;
                }
            }
            else
            {
                Result = true;
            }
            return Result;
        }

        catch
        {
            throw;
        }
    }

    #region For Base Article Detail Datatable

    protected void BtnBaseSave_Click(object sender, EventArgs e)
    {
        try

              {

                  int percent = 0;
                  int.TryParse(txtValueQty.Text, out percent);

                  if (percent > 100 || percent < 1)
                  {
                      CommonFuction.ShowMessage("Percent should not be greater then 100 and less then 1.");
                      return;
                  }
                  if (txtBaseArticleCode.SelectedIndex < 1)
                  {
                      CommonFuction.ShowMessage("Please select article code.");
                      return;
                  }
            if (ViewState["dtBaseArticleDetail"] != null)
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            if (dtBaseArticleDetail.Rows.Count < 15)
            {
                if (txtValueQty.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                    {
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    }
                    bool bb = SearchParameterInBaseArticle(ddlProductType.SelectedItem.ToString().Trim(), UniqueId, txtBaseArticleCode.SelectedItem.ToString());
                    if (!bb)
                    {


                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(dtBaseArticleDetail);
                            dv.RowFilter = "UniqueId=" + UniqueId;
                            if (dv.Count > 0)
                            {

                                dv[0]["ProductType"] = ddlProductType.SelectedItem.ToString().Trim();
                                dv[0]["ArticleCode"] = txtBaseArticleCode.SelectedValue.Trim();
                                dv[0]["ArticleDesc"] = txtBaseArticleCode.SelectedItem.ToString();
                                dv[0]["YARN_SHADE"] = ddlBaseShadeCode.SelectedValue.ToString();
                                dv[0]["UOM"] = ddlBaseUOM.SelectedItem.ToString();
                                dv[0]["Basis"] = ddlBaseBasis.SelectedItem.ToString();
                                dv[0]["ValueQty"] = txtValueQty.Text;
                                dtBaseArticleDetail.AcceptChanges();
                            }
                        }
                        else
                        {

                            DataRow dr = dtBaseArticleDetail.NewRow();
                            dr["UniqueId"] = dtBaseArticleDetail.Rows.Count + 1;
                            dr["ProductType"] = ddlProductType.SelectedItem.ToString().Trim();
                            dr["ArticleCode"] = txtBaseArticleCode.SelectedValue.Trim();
                            dr["ArticleDesc"] = txtBaseArticleCode.SelectedItem.ToString();
                            dr["YARN_SHADE"] = ddlBaseShadeCode.SelectedValue.ToString();
                            dr["UOM"] = ddlBaseUOM.SelectedItem.ToString();
                            dr["Basis"] = ddlBaseBasis.SelectedItem.ToString();
                            dr["ValueQty"] = txtValueQty.Text;
                            dtBaseArticleDetail.Rows.Add(dr);
                        }


                        ViewState["dtBaseArticleDetail"] = dtBaseArticleDetail;
                        RefresBaseArticleRow();
                    }


                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Atrilce Type  Already Added.Please Select Another');", true);
                    }
                    ////}
                    ////else if (txtItemCode.SelectedText == "")
                    ////{
                    ////    CommonFuction.ShowMessage("Yarn Code Required");
                    ////}
                    ////else if (txtRequestQty.Text == "")
                    ////{
                    ////    CommonFuction.ShowMessage("Quantity can not be zero");
                    ////}
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

    protected void BtnBaseCancel_Click(object sender, EventArgs e)
    {
        RefresBaseArticleRow();
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
                ViewState["UniqueId"] = null;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Base Grid RowCommand Selection.\r\nSee error log for detail."));
        }
    }

    private void CreateBaseArticleDetailTable()
    {
        dtBaseArticleDetail = new DataTable();
        dtBaseArticleDetail.Columns.Add("UniqueId", typeof(int));
        dtBaseArticleDetail.Columns.Add("ProductType", typeof(string));
        dtBaseArticleDetail.Columns.Add("ArticleCode", typeof(string));
        dtBaseArticleDetail.Columns.Add("ArticleDesc", typeof(string));
        dtBaseArticleDetail.Columns.Add("YARN_SHADE", typeof(string));
        dtBaseArticleDetail.Columns.Add("UOM", typeof(string));
        dtBaseArticleDetail.Columns.Add("Basis", typeof(string));
        dtBaseArticleDetail.Columns.Add("ValueQty", typeof(double));


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
                BindArticleCode();
                txtBaseArticleCode.Items.Insert(0, new ListItem("----Select---", ""));

                txtBaseArticleCode.SelectedIndex = txtBaseArticleCode.Items.IndexOf(txtBaseArticleCode.Items.FindByValue(dv[0]["ArticleCode"].ToString()));
                ddlBaseShadeCode.SelectedValue = dv[0]["YARN_SHADE"].ToString();
                ddlBaseUOM.SelectedValue = dv[0]["UOM"].ToString();
                ddlBaseBasis.SelectedValue = dv[0]["Basis"].ToString();
                txtValueQty.Text = dv[0]["ValueQty"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
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
                ViewState["UniqueId"] = null;
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

    private void RefresBaseArticleRow()
    {
        ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue("Fiber")); 
        
        txtBaseArticleCode.SelectedIndex = -1;
        ddlBaseShadeCode.SelectedIndex = -1;
        //ddlBaseUOM.SelectedIndex = -1;
        ddlBaseUOM.SelectedValue = "KGS";
        ddlBaseBasis.SelectedIndex = -1;
        txtValueQty.Text = string.Empty;
        ViewState["UniqueId"] = null;
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
                    //if (currentyear == int.Parse(drTemp["YEAR"].ToString()))
                    //{
                        DataRow dr = dtBaseArticleDetail.NewRow();
                        dr["UniqueId"] = dtBaseArticleDetail.Rows.Count + 1;
                        dr["ProductType"] = drTemp["PRODUCT_TYPE"];
                        dr["ArticleCode"] = drTemp["ARTICLE_CODE"];
                        dr["ArticleDesc"] = drTemp["ARTICLE_DESC"];
                        dr["YARN_SHADE"] = drTemp["YARN_SHADE"];
                        dr["UOM"] = drTemp["UOM"];
                        dr["Basis"] = drTemp["BASIS"];
                        dr["ValueQty"] = drTemp["VALUE_QTY"];
                        dtBaseArticleDetail.Rows.Add(dr);
                    //}
                }
                dtTemp = null;

            }
        }
        catch
        {
            throw;
        }
    }
    #endregion

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindArticleCode();

    }

    protected void BindArticleCode()
    {
        try
        {
            DataTable dtallbasedetail = SaitexBL.Interface.Method.YRN_MST.GetALLBaseArticleDetail();
            DataView dvBaseArticle = new DataView(dtallbasedetail);
            dvBaseArticle.RowFilter = "ARTICLE_TYPE='" + ddlProductType.SelectedItem.ToString() + "'";
            if (dvBaseArticle != null && dvBaseArticle.Count > 0)
            {
                txtBaseArticleCode.Items.Clear();
                txtBaseArticleCode.DataSource = dvBaseArticle;
                txtBaseArticleCode.DataValueField = "CODE";
                txtBaseArticleCode.DataTextField = "YARNDESC";
                txtBaseArticleCode.DataBind();
                txtBaseArticleCode.Items.Insert(0, new ListItem("----Select---", ""));
                txtBaseArticleCode.Items.Insert(1, new ListItem("NA", "NA"));

            }
            else
            {


                
                txtBaseArticleCode.Items.Clear();
                txtBaseArticleCode.Items.Insert(0, new ListItem("----Select----", ""));
                txtBaseArticleCode.Items.Insert(1, new ListItem("NA", "NA"));
            }
        }
        catch
        {
            throw;
        }

    }

    protected void ddlPly_SelectedIndexChanged(object sender, EventArgs e)
    {
        //HideShowByPlySelection();
    }

    private void DisableFieldinUpdateforYarncode()
    {

        try
        {
            
            
            //ddlPly.Enabled = false;
            //ddlCount.Enabled = false;
            //ddlQuality.Enabled = false;
            //ddlDej.Enabled = false;
            //ddlBland2.Enabled = false;
            //txtbland1percentage0.Enabled = false;
            //txtBlandRemarks.Enabled = false;
           // btmSave.Enabled = false;
            //btnCancel.Enabled = false;
           // grdBlandTran.Enabled = false;
            ddlYarnCat.Enabled = false;
        }
        catch
        {
            throw;
        }
    }

    private void EnableFieldinUpdateforYarncode()
    {

        try
        {
            ddlPly.Enabled = true;
            ddlCount.Enabled = true;
            ddlQuality.Enabled = true;
            ddlDej.Enabled = true;
            ddlBland2.Enabled = true;
            txtbland1percentage0.Enabled = true;
            txtBlandRemarks.Enabled = true;
            btmSave.Enabled = true;
            btnCancel.Enabled = true;
            grdBlandTran.Enabled = true;
            ddlYarnType.Enabled=true;
            ddlYarnCat.Enabled = true;

        }
        catch
        {
            throw;
        }
    }

    private void HideShowByPlySelection()
    {
        
        try
        {
            if (ddlPly.SelectedIndex > -1)
            {
                if (ddlPly.SelectedItem.Text == "0")
                {
                    SingleTR.Visible = false;
                    PlyTr.Visible = false;
                    Singleplymsgtr.Visible = false;
                }
                else if (ddlPly.SelectedItem.Text == "1")
                {
                    SingleTR.Visible = true;
                    PlyTr.Visible = false;
                    Singleplymsgtr.Visible = true;
                }
                else if (ddlPly.SelectedItem.Text == "2")
                {
                    Singleplymsgtr.Visible = true;
                    SingleTR.Visible = true;
                    PlyTr.Visible = true;

                }
                else
                {
                    Singleplymsgtr.Visible = true;
                    SingleTR.Visible = true;
                    PlyTr.Visible = true;
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select any Ply");

            }
        }
        catch
        {
            throw;
        }
    }

 
    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataTextField = "YARN_DESC";
            ddlyarncode.DataValueField = "YARN_CODE";
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
       
    }


    private DataTable GetYarnData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "select * from (select YARN_CODE,YARN_CAT,YARN_DESC  from YRN_MST Where   (UPPER(YARN_CODE) like :SearchQuery  or  UPPER(YARN_DESC) like :SearchQuery) order by YARN_CODE )  WHERE  ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( select YARN_CODE from (select YARN_CODE  from YRN_MST Where   (UPPER(YARN_CODE) like :SearchQuery  or  UPPER(YARN_DESC) like :SearchQuery) order by YARN_CODE )    WHERE  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by YARN_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        string CommandText = " select YARN_CODE from YRN_MST Where   (UPPER(YARN_CODE) like :SearchQuery  or  UPPER(YARN_DESC) like :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }






    protected void ddlyarncode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            ddlyarncode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetYarnMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                bindCategory("YARN_CAT");
                BindFancyEffect("FIBER_FANCY_EFFECT");
                BindMultiTPM("MULTI_TPM");
                bindCount("YARN_COUNT"); 
                bindDEJ("PO_NATURE");
                bindYarnPLY("YARN_PLY");
                bindYarnCatType("YARN_CAT_TYPE");             
                bindYarnTWIST("YARN_TWIST");
                bindYarnCOATING("YARN_COATING");
                bindYarnENDUSE("END_USE");
                bindYarnSHADE("YARN_SHADE");
                bindSHADE_CODE("YARN_SHADE");
                bindYarnPROCESS("YARN_PROCESS");
                bindBlendingProcess("BLENDING_PROCESS");
                bindClassification("YARN_CLASSIFICATION");
                bindBaseDirection("ITCHS_CUSTOM");
                bindMILANGE_CODE("MILANGE_CODE");
                string text = ddlyarncode.SelectedValue.ToString();
                DataView dv = new DataView(dt);
                dv.RowFilter = "YARN_CODE='" + text + "'";
                if (dv != null && dv.Count > 0)
                {

                    txtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                  
                    if (dv[0]["YARN_TYPE"].ToString() != "")
                    {
                        ddlCatType.SelectedIndex = ddlCatType.Items.IndexOf(ddlCatType.Items.FindByValue( dv[0]["FILAMENT"].ToString())); 
                        bindYarnType("YARN_TYPE", ddlCatType.SelectedItem.Text);
                        ddlYarnType.SelectedValue= dv[0]["YARN_TYPE"].ToString();
                    }

                    txtYarnDescription.Text = dv[0]["YARN_DESC"].ToString().Trim().ToUpper();
                    txtHSNCODE.Text = dv[0]["HSN_CODE"].ToString().Trim();
                    if (dv[0]["QC_REQUIRED"].ToString() == "1")
                    {
                        rad_qc_req.SelectedValue = "yes";
                    }
                    else 
                    {
                        rad_qc_req.SelectedValue = "No";
                    }
                    ddlCount.SelectedIndex = ddlCount.Items.IndexOf(ddlCount.Items.FindByText(dv[0]["Y_COUNT"].ToString()));
                    ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByText(dv[0]["UOM"].ToString()));
               
                    if (ddlYarnType.SelectedValue == "MELANGE")
                    {
                        Td22.Visible = true;
                        Td23.Visible = true;

                    }
                    else
                    {
                        Td22.Visible = false;
                        Td23.Visible = false;
                    }

                    if (ddlCatType.SelectedValue == "SINGLE TWIST")
                    {
                        //lblCount.Text = "Denier";
                        //lblCountValue.Text = "Denier Value";
                        txtCountValue.Visible = true;
                        lblNamrmalPly.Text = "Twist PLY";
                        SingleTR.Visible = false;
                        MultiDetails.Visible = false;
                        PlyTr.Visible = false;
                        lblCountValue.Visible = true;
                        NormalDetails.Visible = true;
                        NormalTPM.Visible = true;
                        PLY1.Visible = true;
                        ddlPly.Visible = true;
                        lblNamrmalPly.Visible = true;
                    }
                    else if (ddlCatType.SelectedValue == "DOUBLE TWIST")
                    {
                        lblNamrmalPly.Text = "Twist PLY";
                        lblNamrmalPly.Visible = true;
                        PLY1.Visible = true;
                        ddlPly.Visible = true;
                        lblmultiply.Text = "Multi PLY";
                        lblmultiply.Visible = false;
                        ddlEndUse.Visible = false;
                        SingleTR.Visible = true;
                        MultiDetails.Visible = false;
                        PlyTr.Visible = false;
                        NormalTPM.Visible = true;
                        NormalDetails.Visible = true;

                    }
                    else if (ddlCatType.SelectedValue == "NON TWIST")
                    {
                        txtCountValue.Visible = true;
                        ddlPly.Visible = false;
                        lblNamrmalPly.Visible = false;
                        SingleTR.Visible = false;
                        MultiDetails.Visible = false;
                        PlyTr.Visible = false;
                        lblCountValue.Visible = true;
                        //multiply.Visible = false;
                        NormalDetails.Visible = false;
                        NormalTPM.Visible = false;
                    }

                    else if (ddlCatType.SelectedValue == "MULTI FOLD")
                    {
                        txtCountValue.Visible = true;
                        ddlPly.Visible = true;
                        lblNamrmalPly.Text = "Twist PLY";
                        lblNamrmalPly.Visible = true;
                        SingleTR.Visible = true;
                        MultiDetails.Visible = true;
                        lblCountValue.Visible = true;
                        ddlEndUse.Visible = true;
                        ddlPly.Visible = true;
                        PlyTr.Visible = true;
                        NormalDetails.Visible = true;
                        NormalTPM.Visible = true;
                        //multiply.Visible = true;
                        lblmultiply.Visible = true;
                    }
                    else
                    {
                        //lblCount.Text = "English Count";
                        lblCountValue.Text = "Count Value";
                        lblCountValue.Visible = false;
                        txtCountValue.Visible = false;
                    }

                    //ddlPly.SelectedValue = dv[0]["PLY"].ToString();
                    ddlPly.SelectedIndex = ddlPly.Items.IndexOf(ddlPly.Items.FindByValue(dv[0]["PLY"].ToString())); 
                 
                    txtCountCV.Text = dv[0]["COUNT_CV"].ToString();
                  
                    txtTotalImp.Text = dv[0]["TOTAL_IMP"].ToString();
                  
                    txtRecorderLevel.Text = dv[0]["REORDER_LEVEL"].ToString();
                    txtRecorderQuantity.Text = dv[0]["REORDER_QUANTITY"].ToString();
                    txtCountValue.Text = dv[0]["COUNT_VALUE"].ToString();
                    txtMimimumStock.Text = dv[0]["MIN_STOCK"].ToString();
                    txtMinimumProcureDays.Text = dv[0]["MIN_PROCURE_DAYS"].ToString();
                    txtFindDepCode.Text = dv[0]["FIN_DEB_CODE"].ToString();
                    txtOpeningBalanceStock.Text = dv[0]["OP_BAL_STOCK"].ToString();
                    txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                    txtpackageSize.Text = dv[0]["PACKAGE_SIZE"].ToString();
                    ddlQuality.SelectedValue = dv[0]["YARN_QUALITY"].ToString();
                    ddlDej.SelectedIndex = ddlDej.Items.IndexOf(ddlDej.Items.FindByText(dv[0]["YARN_DEJ"].ToString()));
                    txtMaximumStock.Text = dv[0]["MAX_STOCK"].ToString();
                    ddlYarnCat.SelectedIndex = ddlYarnCat.Items.IndexOf(ddlYarnCat.Items.FindByText(dv[0]["YARN_CAT"].ToString()));
                    ddlClassification.SelectedIndex = ddlClassification.Items.IndexOf(ddlClassification.Items.FindByText(dv[0]["CLASSIFICATION"].ToString()));
                    txtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                    txtTpm1.Text = dv[0]["TPM1"].ToString();
                    txtTpm2.Text = dv[0]["TPM2"].ToString();
                    txtDirection1.Text = dv[0]["DIRECTION1"].ToString();
                    txtDirection2.Text = dv[0]["DIRECTION2"].ToString().Trim();
                    txtRemarks1.Text = dv[0]["REMARKS1"].ToString();
                    txtRemarks2.Text = dv[0]["REMARKS2"].ToString();
                    txttrarifSubheading.Text = dv[0]["TRAFIF_SUB_HEADING"].ToString();
                    ddlFancyEffect.SelectedValue =  dv[0]["FANCY_EFFECT"].ToString();
                    ddlEndUse.SelectedIndex = ddlEndUse.Items.IndexOf(ddlEndUse.Items.FindByValue(dv[0]["ENDUSE"].ToString()));
                    ddlCoating.SelectedValue =  dv[0]["COATING"].ToString();
                    ddlTwistDirection.SelectedValue =  dv[0]["TWIST"].ToString();
                    ddlYarnShade.SelectedValue = dv[0]["YARN_SHADE"].ToString();
                    ddlCustom_ITCHS.SelectedValue = dv[0]["CUSTOM_ITCHS_CODE"].ToString();

                    ddlTariffHeading.SelectedIndex = ddlTariffHeading.Items.IndexOf(ddlTariffHeading.Items.FindByValue(dv[0]["TARIFF_HEADING"].ToString()));
                    ddlSales_ITCHS.SelectedIndex = ddlSales_ITCHS.Items.IndexOf(ddlSales_ITCHS.Items.FindByValue(dv[0]["SALES_ITCHS_CODE"].ToString()));

                    rdIsExciable.SelectedIndex = rdIsExciable.Items.IndexOf(rdIsExciable.Items.FindByValue(dv[0]["IS_EXCISABLE"].ToString()));
                    txtConversionRate.Text = dv[0]["CONVERSION_RATE"].ToString();
                    txtValue1.Text = dv[0]["VALUE1"].ToString();
                    txtValue2.Text = dv[0]["VALUE2"].ToString();

                    ddlbBlendingProcess.SelectedIndex = ddlbBlendingProcess.Items.IndexOf(ddlbBlendingProcess.Items.FindByValue(dv[0]["BLENDING_PROCESS"].ToString()));
                    
                    DataTable dtBland = SaitexBL.Interface.Method.YRN_MST.GetYarnBlandDetailByYarnCode(text);
                    if (dtBland != null && dtBland.Rows.Count > 0)
                    {
                        MapDataTable(dtBland);
                        if (ViewState["dtBlandDetail"] != null)
                            dtBlandDetail = (DataTable)ViewState["dtBlandDetail"];
                        grdBlandTran.DataSource = dtBlandDetail;
                        grdBlandTran.DataBind();                      

                    }
                    DataTable dtAssociatedItemDetails = SaitexBL.Interface.Method.YRN_MST.GetYarnAssociatedItemDetailByYarnCode(text);
                    if (dtAssociatedItemDetails != null && dtAssociatedItemDetails.Rows.Count > 0)
                    {
                        MapAssociatedDataTable(dtAssociatedItemDetails);
                        if (ViewState["dtAssociatedItemDetail"] != null)
                            dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
                        grdAssociatedYarn.DataSource = dtAssociatedItemDetail;
                        grdAssociatedYarn.DataBind();
                       
                    }
                    txtAssocatedItemCode.Text = getAssociatedYarnCode();

                    DataTable dtBaseArtilce = SaitexBL.Interface.Method.YRN_MST.GetYarnBaseArticleByYarnCode(text);
                    if (dtBaseArtilce != null && dtBaseArtilce.Rows.Count > 0)
                    {
                        if (ViewState["dtBaseArticleDetail"] != null)
                            dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
                        MapBaseArticleRowDataTable(dtBaseArtilce);
                        grdBaseArticleDetail.DataSource = dtBaseArticleDetail;
                        grdBaseArticleDetail.DataBind();
                    }

                    DataTable dtTRN_SUB = SaitexBL.Interface.Method.YRN_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtYarnCode.Text, "OPB01");
                    ViewState["dtTRN_Sub"] = dtTRN_SUB;
                    MapTrnDataTable();

                    DataTable dtTemp = SaitexBL.Interface.Method.YRN_MST.Select_Yarn_Color_LIST(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, txtYarnCode.Text, "OPB01");
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        MapColorDataTable(dtTemp);
                    }
                    BindColorDetailGrid();

                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

    }
    private void MapTrnDataTable()
    {
        try
        {
            DataTable dtTRN_SUB = null;
            if (ViewState["dtTRN_Sub"] != null)
                dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];          
            if (!dtTRN_SUB.Columns.Contains("UNIQUE_ID"))
                dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            for (int iLoop = 0; iLoop < dtTRN_SUB.Rows.Count; iLoop++)
            {
                dtTRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
            }
            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }

    private void MapColorDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            if (dtColorDetail == null || dtColorDetail.Rows.Count == 0)
                CreateColorDetailTable();
            dtColorDetail.Rows.Clear();
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtColorDetail.NewRow();
                    dr["UniqueId"] = dtColorDetail.Rows.Count + 1;
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["SHADE_FAMILY"] = drTemp["SHADE_FAMILY"];                   
                    dr["SHADE"] = drTemp["SHADE"];
                    dr["RGB"] = drTemp["RGB"];
                    dr["STORE"] = drTemp["STORE"];
                    dr["LOCATION"] = drTemp["LOCATION"];
                    dr["OP_BAL_STOCK"] = drTemp["OP_BAL_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["MIN_STOCK"] = drTemp["MIN_STOCK"];
                    dr["MAX_STOCK"] = drTemp["MAX_STOCK"];
                    dr["OLD_STORE"] = drTemp["OLD_STORE"];
                    dr["OLD_LOCATION"] = drTemp["OLD_LOCATION"];

                    dr["LOT_NO"] = drTemp["LOT_NO"];
                    dr["GRADE"] = drTemp["GRADE"];
                    dr["GROSS_WT"] = drTemp["GROSS_WT"];
                    dr["TARE_WT"] = drTemp["TARE_WT"];
                    dr["CARTONS"] = drTemp["CARTONS"];
                    dr["TRN_NUMB"] = drTemp["TRN_NUMB"];
                    dr["PRTY_CODE"] = drTemp["PRTY_CODE"];
                    dr["PRTY_NAME"] = drTemp["PRTY_NAME"]; 
                    dr["ROW_STATE"] = "NO STATE";
                    dtColorDetail.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtColorDetail"] = dtColorDetail;
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlYarnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlYarnType.SelectedValue == "MELANGE")
            {
                Td22.Visible = true;
                Td23.Visible = true;

            }
            else
            {
                Td22.Visible = false;
                Td23.Visible = false;
            }
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Spun /Filament  Selection.\r\nSee error log for detail."));

        }
    }

    //Change code By Arun Sharma 12/12/2017 according to Director shiv Sir .............
    protected bool CheckValidation(string task)
    {        
        bool result = false;
        if (ddlCatType.Text == "SINGLE TWIST")
        {
            if (ddlYarnCat.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select yarn category.");
                result = true;
            }
            if (ddlClassification.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Luster.");
                result = true;
            }
            if (ddlQuality.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Process.");
                result = true;
            }
            if (ddlCoating.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Filament.");
                result = true;
            }
            if (ddlCount.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select english count/Denier.");
                result = true;
            }
            if (ddlUOM.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select UOM.");
                result = true;
            }
            if (ddlFancyEffect.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Base ply.");
                result = true;
            }
            if (ddlCustom_ITCHS.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("PLease select Master Direction");
                result = true;
            }
            if (ddlYarnType.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Dyed / Non Dyed");
                result = true;
            }
            if (ddlTwistDirection.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Narmal Twist Direction.");
                result = true;
            }
            if (txtTpm1.Text.Trim() == "")
            {
                CommonFuction.ShowMessage("Fill Primary TPM.");
                result = true;
            }
           
        }
        else if (ddlCatType.Text == "NON TWIST")
        {
            if (ddlYarnCat.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select yarn category.");
                result = true;
            }
            if (ddlClassification.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Luster.");
                result = true;
            }
            if (ddlQuality.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Process.");
                result = true;
            }
            if (ddlCoating.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Filament.");
                result = true;
            }
            if (ddlCount.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select english count/Denier.");
                result = true;
            }
            if (ddlUOM.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select UOM.");
                result = true;
            }
            if (ddlFancyEffect.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Master ply.");
                result = true;
            }
            if (ddlCustom_ITCHS.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("PLease select Master Direction");
                result = true;
            }
            if (ddlYarnType.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Dyed / Non Dyed");
                result = true;
            }
        }
        else if (ddlCatType.Text == "DOUBLE TWIST")
        {
            if (ddlYarnCat.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select yarn category.");
                result = true;
            }
            if (ddlClassification.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Luster.");
                result = true;
            }
            if (ddlQuality.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Process.");
                result = true;
            }
            if (ddlCoating.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Filament.");
                result = true;
            }
            if (ddlCount.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select english count/Denier.");
                result = true;
            }
            if (ddlUOM.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select UOM.");
                result = true;
            }
            if (ddlFancyEffect.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Master ply.");
                result = true;
            }
            if (ddlCustom_ITCHS.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("PLease select Master Direction");
                result = true;
            }
            if (ddlYarnType.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Dyed / Non Dyed");
                result = true;
            }
            if (ddlTwistDirection.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Narmal Twist Direction.");
                result = true;
            }
            if (ddlbBlendingProcess.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Multi Twist Direction.");
                result = true;
            }
            if (txtTpm1.Text.Trim() == "")
            {
                CommonFuction.ShowMessage("Fill Primary TPM.");
                result = true;
            }
            if (txtDirection1.Text.Trim() == "")
            {
                CommonFuction.ShowMessage("Fill Secondary TPM.");
                result = true;
            }
        }

        else if (ddlCatType.Text == "MULTI FOLD")
        {
            if (ddlYarnCat.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select yarn category.");
                result = true;
            }
            if (ddlClassification.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Luster.");
                result = true;
            }
            if (ddlQuality.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Process.");
                result = true;
            }
            if (ddlCoating.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Filament.");
                result = true;
            }
            if (ddlCount.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select english count/Denier.");
                result = true;
            }
            if (ddlUOM.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select UOM.");
                result = true;
            }
            if (ddlFancyEffect.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Master ply.");
                result = true;
            }
            if (ddlCustom_ITCHS.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("PLease select Master Direction");
                result = true;
            }
            if (ddlYarnType.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Dyed / Non Dyed");
                result = true;
            }
            if (ddlTwistDirection.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Narmal Twist Direction.");
                result = true;
            }
            if (ddlbBlendingProcess.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Multi Twist Direction.");
                result = true;
            }
            if (ddlbBlendingProcess.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Multi Twist Direction.");
                result = true;
            }
            if (ddlEndUse.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Multi Ply.");
                result = true;
            }
            if (txtTpm1.Text.Trim() == "")
            {
                CommonFuction.ShowMessage("Fill Primary TPM.");
                result = true;
            }
            if (txtDirection1.Text.Trim() == "")
            {
                CommonFuction.ShowMessage("Fill Secondary TPM.");
                result = true;
            }
            if (txtDirection2.SelectedIndex < 1)
            {
                CommonFuction.ShowMessage("Please select Multi Twist Direction.");
                result = true;
            }
            if (txtTpm2.Text.Trim() == "")
            {
                CommonFuction.ShowMessage("Fill Multi TPM.");
                result = true;
            }
        }
       
        if (task != "GenerateDesc")
        {
            //if (txtFindDepCode.Text.Trim() == "")
            //{
            //    CommonFuction.ShowMessage("Fin Code is not entered.");
            //    result = true;
            //}
            //if (txtOpeningBalanceStock.Text.Trim() == "")
            //{
            //    CommonFuction.ShowMessage("Opening Stock is not entered.");
            //    result = true;
            //}
            //if (txtMimimumStock.Text.Trim() == "")
            //{
            //    CommonFuction.ShowMessage("Minimum Stock is not entered.");
            //    result = true;
            //}
            
        }        
        return result;
    
    }



    protected void grdBaseArticleDetail_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdBaseArticleDetail_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void BtnYanDescGenerate_Click(object sender, EventArgs e)
    {
        
        try
        {
            if (CheckValidation("GenerateDesc"))
            {
                return;
            }
            
            string yarn_desc = generatedescription();
            string yarn_code = SaitexBL.Interface.Method.YRN_MST.Get_YarnMasterCodeDescDuplicate(yarn_desc);

            if (!string.IsNullOrEmpty(yarn_code))
            {
                CommonFuction.ShowMessage("Yarn Description is already exist. Yarn Code is : " + yarn_code);
                return;
            }

            txtYarnDescription.Text = yarn_desc.Trim().ToUpper();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Generating Yarn Details.\r\nSee error log for detail."));
        }
    }

    private string generatedescription()
    {
        string final_str;
        //**************************Code Change by Arun Sharma auto Desc Generate select cat wise according to client and Shiv Sir ...
        //***********************************12/12/2017**********************************************
        final_str = ddlCount.Text + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + ddlCustom_ITCHS.Text +
            " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " +
            ddlEndUse.Text + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + ddlYarnCat.Text +
            " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + "" + "/" + "" + ddlYarnType.Text + " ";
       
           if (ddlCatType.Text == "SINGLE TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "1")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + txtTpm1.Text 
                + " " + "/" + " " + ddlTwistDirection.Text  + " " + "/" + " " +ddlClassification.Text
                + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }
         if (ddlCatType.Text == "SINGLE TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "NA")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + txtTpm1.Text
                + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + ddlClassification.Text
                + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }

         if (ddlCatType.Text == "SINGLE TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "1")
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + txtTpm1.Text
                 + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + ddlClassification.Text
                 + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
         }



       else if (ddlCatType.Text == "SINGLE TWIST" && ddlFancyEffect.Text == "1")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlCustom_ITCHS.Text
                + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " "
                + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }
        else if (ddlCatType.Text == "SINGLE TWIST" && ddlCoating.Text == "NA")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + ddlCustom_ITCHS.Text + " " + "/" + " " +
                        txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " +
                        ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }
         else if (ddlCatType.Text == "SINGLE TWIST")
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + ddlCustom_ITCHS.Text
                  + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " +
                  ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
         }
         if (ddlCatType.Text == "NON TWIST" )
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " +
                 ddlCustom_ITCHS.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " +
                 ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
         }
         if (ddlCatType.Text == "NON TWIST" && ddlFancyEffect.Text == "NA" && ddlCustom_ITCHS.Text == "NA")
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text 
                  + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " +
                 ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
         }
        
        if (ddlCatType.Text == "NON TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text =="1")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlClassification.Text
                + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }
       else if (ddlCatType.Text == "NON TWIST"  && ddlFancyEffect.Text == "1")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlCustom_ITCHS.Text
                + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }
         if (ddlCatType.Text == "NON TWIST" && ddlCoating.Text == "NA")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " +
                ddlCustom_ITCHS.Text  + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " +
                ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";
        }
       
         if (ddlCatType.Text == "DOUBLE TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "1")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + txtDirection1.Text
                + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " 
                + ddlPly.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text + "";

        }

         else if (ddlCatType.Text == "DOUBLE TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text!="NA")
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + txtDirection1.Text
                 + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " "
                 + ddlPly.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";

         }
         else if (ddlCatType.Text == "DOUBLE TWIST" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "NA")
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " "  + "/" + " " + txtDirection1.Text
                 + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " "
                 + ddlPly.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";

         }

         else if (ddlCatType.Text == "DOUBLE TWIST" && ddlFancyEffect.Text == "1")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text +  " " + "/" + " " + ddlCustom_ITCHS.Text + " " + "/" + " " + txtDirection1.Text
                + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " "
                + ddlPly.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text + "";

        }
         else if (ddlCatType.Text == "DOUBLE TWIST" && ddlFancyEffect.Text == "NA" && ddlCustom_ITCHS.Text == "NA" && ddlCoating.Text == "NA")
         {
             final_str = ddlCount.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + 
                 txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text  + " " + "/" + " " + ddlPly.Text
                       + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " "
                       + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";

         }
         else if (ddlCatType.Text == "DOUBLE TWIST" && ddlCoating.Text == "NA")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + ddlCustom_ITCHS.Text + " " + "/" + " " +
                          txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " +
                          ddlbBlendingProcess.Text + " " + "/" + " " +        ddlPly.Text + " " + "/" + " " +
                       ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";

        }
         

         else if (ddlCatType.Text == "DOUBLE TWIST" && ddlFancyEffect.Text == "NA" && ddlCustom_ITCHS.Text == "NA")
          {
              final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " +
                   txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " +
                  txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + ddlPly.Text
                        + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " "
                        + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";

          }
        else if (ddlCatType.Text == "DOUBLE TWIST"  )
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + ddlCustom_ITCHS.Text
                         + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " 
                        + ddlTwistDirection.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + 
                        ddlQuality.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlYarnType.Text + "";

        }
        else if (ddlCatType.Text == "MULTI FOLD" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "1" && ddlYarnType.Text == "MELANGE")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text  + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + txtDirection1.Text
                + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " "  + ddlTwistDirection.Text
                + " " + "/" + " " + ddlEndUse.Text + " " + "/" + " " + txtTpm2.Text + " " + "/" + " " + txtDirection2.Text +  " " + "/" + " " +
                ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text
                + " " + "/" + " " + ddlSales_ITCHS.Text + " ";

        }
        else if (ddlCatType.Text == "MULTI FOLD"  && ddlFancyEffect.Text == "1" && ddlYarnType.Text == "MELANGE")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlCustom_ITCHS.Text + " " + "/" + " " + ddlPly.Text 
                + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" +
                " " + ddlTwistDirection.Text + " " + "/" + " " + ddlEndUse.Text + " " + "/" + " " + txtTpm2.Text + " " + "/" + " " + txtDirection2.Text
                + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " +
                ddlYarnType.Text + " " + "/" + " " + ddlSales_ITCHS.Text + " ";

        }
        else if (ddlCatType.Text == "MULTI FOLD" && ddlCustom_ITCHS.Text == "NA" && ddlFancyEffect.Text == "1" )
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + txtDirection1.Text
                + " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text
                + " " + "/" + " " + ddlEndUse.Text + " " + "/" + " " + txtTpm2.Text + " " + "/" + " " + txtDirection2.Text + " " + "/" + " " +
                ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text + " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text
                 + " ";

        }
        else if (ddlCatType.Text == "MULTI FOLD" && ddlYarnType.Text == "MELANGE")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " + 
                ddlCustom_ITCHS.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text
                + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlEndUse.Text + " " + "/" + " " 
                + txtTpm2.Text + " " + "/" + " " + txtDirection2.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text +
                " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text + " " + "/" + " " + ddlSales_ITCHS.Text + " ";

        }
         if (ddlCatType.Text == "MULTI FOLD" && ddlCoating.Text == "NA")
        {
            final_str = ddlCount.Text + " " + "/" + " "  + ddlFancyEffect.Text + " " + "/" + " " +  ddlCustom_ITCHS.Text + " " + "/" + " "
                + ddlPly.Text + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text + txtTpm2.Text
                + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlEndUse.Text + " " + "/" + " "
                 + " " + "/" + " " + txtDirection2.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text +
                " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text  + " ";

        }
         else if (ddlCatType.Text == "MULTI FOLD" && ddlFancyEffect.Text == "NA" && ddlCustom_ITCHS.Text == "NA")
        {
            final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " +  ddlPly.Text + " " + "/" + " " + txtDirection1.Text + 
                " " + "/" + " " + ddlbBlendingProcess.Text + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text +
                " " + "/" + " " + ddlEndUse.Text + " " + "/" + " "  + txtTpm2.Text + " " + "/" + " " + txtDirection2.Text + " " + "/" + " " +
                ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text +  " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + 
                ddlYarnType.Text + " ";


        }
         else if (ddlCatType.Text == "MULTI FOLD")
         {
             final_str = ddlCount.Text + " " + "/" + " " + ddlCoating.Text + " " + "/" + " " + ddlFancyEffect.Text + " " + "/" + " " +
                 ddlCustom_ITCHS.Text + " " + "/" + " " + ddlPly.Text + " " + "/" + " " + txtDirection1.Text + " " + "/" + " " + ddlbBlendingProcess.Text
                 + " " + "/" + " " + txtTpm1.Text + " " + "/" + " " + ddlTwistDirection.Text + " " + "/" + " " + ddlEndUse.Text + " " + "/" + " "
                 + txtTpm2.Text + " " + "/" + " " + txtDirection2.Text + " " + "/" + " " + ddlYarnCat.Text + " " + "/" + " " + ddlClassification.Text +
                 " " + "/" + " " + ddlQuality.Text + " " + "/" + " " + ddlYarnType.Text + " ";

         }
        if (grdBlandTran.Rows.Count > 1)
        {
            foreach (GridViewRow grow in grdBlandTran.Rows)
            {
                final_str = final_str + "" + ((Label)grow.Cells[2].FindControl("txtItemDesc")).Text + "/";
            }
        }
        //final_str = final_str.Substring(0, final_str.Length - 1) +" "+( (ddlYarnCat.SelectedValue == "SLUB") ? "SLUB" : "") + " " + ddlQuality.Text + " YARN " + ddlbBlendingProcess.Text;



        return final_str;
    }

    /************************************* For opening balance color wise *****************************************/

    protected void btnLotDetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool Result = false;
            string Shade_Family = txtShadeFamily.Text.Trim();
            string Shade = txtShade.Text.Trim();
           
            if (ViewState["UniqueId"] == null)
            {
                foreach (GridViewRow grdRow in grdColorDetail.Rows)
                {
                    Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                    Label lblShadeFamily = (Label)grdRow.FindControl("txtShadeFamily");
                    Label lblShade = (Label)grdRow.FindControl("txtShade");
                    Label lblStore = (Label)grdRow.FindControl("txtstore");
                    Label lblLocation = (Label)grdRow.FindControl("txtlocation");
                    ////if (Shade_Family == lblShadeFamily.Text.Trim() && Shade == lblShade.Text.Trim())
                    ////{
                    ////    Result = true;
                    ////}
                }
            }


            //if (!string.IsNullOrEmpty(txtShade.Text) && Result == false)
            //{
                txtOpeningBal.ReadOnly = false;              
                string URL = "YARN_OP_BAL_LOT_DETAILS.aspx";
                URL = URL + "?YARN_CODE=" + txtYarnCode.Text;
                URL = URL + "&SHADE_FAMILY=" + HttpUtility.UrlEncode(txtShadeFamily.Text.Trim());
                URL = URL + "&SHADE=" + HttpUtility.UrlEncode(txtShade.Text.Trim());
                URL = URL + "&RGB=" + txtRGB.Text.Trim();
                URL = URL + "&OP_BAL=" + txtOpeningBal.Text;
                URL = URL + "&UOM=" + ddlUOM.SelectedValue;
                URL = URL + "&STORE=" + ddlStore.SelectedValue;
                URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
                //URL = URL + "&txtOpBal=" + txtOpeningBal.ClientID;
                URL = URL + "&txtQTY=" + txtOpeningBal.ClientID;
                URL = URL + "&LOT_NO=" + txtLotNo.Text.Trim();                
                URL = URL + "&GRADE=" + txtGrade.Text;               
                URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
                URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
                URL = URL + "&CARTONS=" + txtCartoons.ClientID;
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
                URL = URL + "&PI_NO=NA";
                URL = URL + "&PRTY_CODE=" + txtPartyCode.SelectedText;
              
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


            //}
            //else
            //{
            //    Common.CommonFuction.ShowMessage("Please select another color, this color is already exist.");
            //}
        }
        catch
        {
        }
    }   

    private void CreateColorDetailTable()
    {
        dtColorDetail = new DataTable();
        dtColorDetail.Columns.Add("UniqueId", typeof(int));
        dtColorDetail.Columns.Add("YARN_CODE", typeof(string));
        dtColorDetail.Columns.Add("SHADE_FAMILY", typeof(string));
        dtColorDetail.Columns.Add("SHADE", typeof(string));
        dtColorDetail.Columns.Add("RGB", typeof(string));
        dtColorDetail.Columns.Add("OP_BAL_STOCK", typeof(double));
        dtColorDetail.Columns.Add("OP_RATE", typeof(string));
        dtColorDetail.Columns.Add("MIN_STOCK", typeof(double));
        dtColorDetail.Columns.Add("MAX_STOCK", typeof(double));
        dtColorDetail.Columns.Add("LOCATION", typeof(string));
        dtColorDetail.Columns.Add("STORE", typeof(string));
        dtColorDetail.Columns.Add("OLD_LOCATION", typeof(string));
        dtColorDetail.Columns.Add("OLD_STORE", typeof(string));
        dtColorDetail.Columns.Add("ROW_STATE", typeof(string));

        dtColorDetail.Columns.Add("LOT_NO", typeof(string));
        dtColorDetail.Columns.Add("GRADE", typeof(string));
        dtColorDetail.Columns.Add("GROSS_WT", typeof(double));
        dtColorDetail.Columns.Add("TARE_WT", typeof(double));
        dtColorDetail.Columns.Add("CARTONS", typeof(double));
        dtColorDetail.Columns.Add("NO_OF_UNIT", typeof(double));
        dtColorDetail.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dtColorDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtColorDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtColorDetail.Columns.Add("PRTY_CODE", typeof(string));
        dtColorDetail.Columns.Add("PRTY_NAME", typeof(string));

    }

    protected void lbtnsavedetailColor_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            if (dtColorDetail == null || dtColorDetail.Rows.Count == 0)

                CreateColorDetailTable();

            if (!string.IsNullOrEmpty(txtPartyName.Value) && !string.IsNullOrEmpty(txtShade.Text.Trim() ))
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchShadeCodeInGrid(UniqueId);
                if (!bb)
                {
                    double rate = 0;
                    double.TryParse(txtOpenRate.Text.Trim(), out rate);
                    double opBal = 0;
                    double.TryParse(txtOpeningBal.Text.Trim(), out opBal);
                    double minStock = 0;
                    double.TryParse(txtMinStock.Text.Trim(), out minStock);
                    double maxStock = 0;
                    double.TryParse(txtMaxStock.Text.Trim(), out maxStock);

                    double grossWt = 0;
                    double tareWt = 0;
                    double cartons = 0;
                    double no_of_unit = 0;
                    double wt_of_unit = 0;
                    double.TryParse(txtGrossWt.Value.Trim(), out grossWt);
                    double.TryParse(txtTareWt.Value.Trim(), out tareWt);
                    double.TryParse(txtCartoons.Value.Trim(), out cartons);
                    double.TryParse(txtNoOfUnit.Value, out no_of_unit);
                    double.TryParse(txtWeightOfUnit.Value, out wt_of_unit);

                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtColorDetail);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["YARN_CODE"] = txtYarnCode.Text.Trim();
                            dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                            dv[0]["SHADE"] = txtShade.Text.Trim();
                            dv[0]["RGB"] = txtRGB.Text.Trim();
                            dv[0]["OP_BAL_STOCK"] = opBal;
                            dv[0]["OP_RATE"] = rate;
                            dv[0]["MIN_STOCK"] = minStock ;
                            dv[0]["MAX_STOCK"] = maxStock;
                            dv[0]["LOCATION"] = ddlLocation.SelectedValue.ToString();
                            dv[0]["STORE"] = ddlStore.SelectedValue.ToString();
                            dv[0]["ROW_STATE"] = "UPDATE";

                            dv[0]["CARTONS"] = cartons ;
                            dv[0]["TARE_WT"] = tareWt;
                            dv[0]["GROSS_WT"] = grossWt;
                            dv[0]["NO_OF_UNIT"] = no_of_unit;
                            dv[0]["WEIGHT_OF_UNIT"] = wt_of_unit;
                            dv[0]["LOT_NO"] = txtLotNo.Text ;
                            dv[0]["GRADE"] = txtGrade.Text;
                            dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                            dv[0]["PRTY_NAME"] = txtPartyName.Value; 
                            dtColorDetail.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtColorDetail.NewRow();
                        dr["UniqueId"] = dtColorDetail.Rows.Count + 1;
                        dr["YARN_CODE"] = txtYarnCode.Text.Trim();
                        dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                        dr["SHADE"] = txtShade.Text.Trim();
                        dr["RGB"] = txtRGB.Text.Trim();
                        dr["OP_BAL_STOCK"] = opBal;
                        dr["OP_RATE"] = rate;
                        dr["MIN_STOCK"] = minStock;
                        dr["MAX_STOCK"] = maxStock;
                        dr["LOCATION"] = ddlLocation.SelectedValue.ToString();
                        dr["STORE"] = ddlStore.SelectedValue.ToString();
                        dr["ROW_STATE"] = "INSERT";
                        dr["CARTONS"] = cartons;
                        dr["TARE_WT"] = tareWt;
                        dr["GROSS_WT"] = grossWt;
                        dr["NO_OF_UNIT"] = no_of_unit;
                        dr["WEIGHT_OF_UNIT"] = wt_of_unit;
                        dr["LOT_NO"] = txtLotNo.Text;
                        dr["GRADE"] = txtGrade.Text;
                        dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                        dr["PRTY_NAME"] = txtPartyName.Value; 
                        dtColorDetail.Rows.Add(dr);
                    }
                    RefreshDetailRowColor();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Shade Code Should Be Diffrent');", true);
                }
            }
            else if (string.IsNullOrEmpty(txtShade.Text.Trim()))
            {
                CommonFuction.ShowMessage("Party/Color Required.");
            }

            ViewState["dtColorDetail"] = dtColorDetail;
            BindColorDetailGrid();

        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        RefreshDetailRowColor();
    }

    private void BindColorDetailGrid()
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
            {
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
                DataView dv = new DataView(dtColorDetail);
                dv.RowFilter = "ROW_STATE <> 'DELETE'";
                grdColorDetail.DataSource = dv;
                grdColorDetail.DataBind();
            }
            else
            {
                grdColorDetail.DataSource = null;
                grdColorDetail.DataBind();
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SearchShadeCodeInGrid(int UniqueId)
    {
        bool Result = false;
        try
        {
            string Shade_Family = txtShadeFamily.Text.Trim();
            string Shade = txtShade.Text.Trim();
            string Store = ddlStore.SelectedValue;
            string PARTY = txtPartyCode.SelectedText;
            foreach (GridViewRow grdRow in grdColorDetail.Rows)
            {
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                 Label lblShadeFamily = (Label)grdRow.FindControl("txtShadeFamily");
                    Label lblShade = (Label)grdRow.FindControl("txtShade");
                    Label lblStore = (Label)grdRow.FindControl("txtstore");
                    Label lblParty = (Label)grdRow.FindControl("txtParty");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (UniqueId != iUniqueId && Shade_Family == lblShadeFamily.Text.Trim() && Shade == lblShade.Text.Trim() && Store == lblStore.Text.Trim() && lblParty.ToolTip == PARTY)
                {
                    Result = true;
                }
            }            
         


            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void RefreshDetailRowColor()
    {       
        txtOpenRate.Text = string.Empty;
        txtOpeningBal.Text = string.Empty;
        txtMaxStock.Text = "";
        txtMinStock.Text = "";
        cmbShade.SelectedIndex = -1;
        txtShade.Text = string.Empty;
        txtShadeFamily.Text = string.Empty;
        ViewState["UniqueId"] = null;
        txtRGB.Text = string.Empty;
        txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
        txtCartoons.Value = string.Empty;
        txtGrossWt.Value = string.Empty;
        txtLotNo.Text = string.Empty;
        txtGrade.Text = string.Empty;
        txtTareWt.Value = string.Empty;
        txtNoOfUnit.Value = string.Empty;
        txtWeightOfUnit.Value = string.Empty;
        txtPartyCode.SelectedIndex = -1;
        txtPartyName.Value = string.Empty;
    }

    protected void grdColorDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "colorEdit")
            {

                FillDetailByGridColor(UniqueId);
            }
            else if (e.CommandName == "colorDelete")
            {
                DeleteColorDetailRow(UniqueId);
                BindColorDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void grdColorDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DateTime TDATE = DateTime.Now;
                if (ViewState["TDATE"] != null)
                    TDATE = (DateTime)ViewState["TDATE"];
                Button delButton = ((Button)e.Row.FindControl("lnkDelete"));
                Button lnkEdit = ((Button)e.Row.FindControl("lnkEdit"));

                Label txtRGB = ((Label)e.Row.FindControl("txtRGB"));
                TextBox txtRGBColor = ((TextBox)e.Row.FindControl("txtRGBColor"));
                if (!string.IsNullOrEmpty(txtRGB.Text))
                {
                    txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
                }


                if (!oUserLoginDetail.UserType.Equals("SA"))
                {
                    if (TDATE.Date.Equals(DateTime.Now.Date))
                    {
                        delButton.Visible = true;
                        lnkEdit.Visible = true;
                        imgbtnUpdate.Enabled = true;
                    }
                    else
                    {
                        delButton.Visible = false;
                        lnkEdit.Visible = false;
                        imgbtnUpdate.Enabled = false;
                    }

                }
                else
                {

                    delButton.Visible = true;
                    lnkEdit.Visible = true;
                    imgbtnUpdate.Enabled = true;
                }

                LinkButton lnkunige = (LinkButton)e.Row.FindControl("lnkunige");
                int UNIQUE_ID = int.Parse(lnkunige.CommandArgument);
                if (dtColorDetail != null && dtColorDetail.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtColorDetail);

                    dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string YARN_CODE = dv[0]["YARN_CODE"].ToString();
                        string SHADE_FAMILY = dv[0]["SHADE_FAMILY"].ToString();
                        string SHADE = dv[0]["SHADE"].ToString();
                        string STORE = dv[0]["STORE"].ToString();
                        string LOCATION = dv[0]["LOCATION"].ToString();
                        string PRTY_CODE = dv[0]["PRTY_CODE"].ToString();
                        if (Session["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];
                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + YARN_CODE + "' AND SHADE='" + SHADE + "' AND SHADE_FAMILY='" + SHADE_FAMILY + "' AND STORE='" + STORE + "'  AND LOCATION='" + LOCATION + "' AND PRTY_CODE='"+PRTY_CODE+"'";
                            if (dvYRNDRecieve_trn.Count > 0)
                            {
                                GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNDRecieve_trn;
                                grdBOM.DataBind();
                            }

                        }

                    }

                }



            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Databound..\r\nSee error log for detail."));

        }
    }
   

    private void DeleteColorDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            if (grdColorDetail.Rows.Count == 1)
            {
                //dtColorDetail.Rows.Clear();
                dtColorDetail.Rows[0].SetField("ROW_STATE", "DELETE");
            }
            else
            {
                foreach (DataRow dr in dtColorDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        //dtColorDetail.Rows.Remove(dr);
                        dr.SetField("ROW_STATE", "DELETE");
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtColorDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
            ViewState["dtColorDetail"] = dtColorDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void FillDetailByGridColor(int UniqueId)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            DataView dv = new DataView(dtColorDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                setShadeFamilyCombo(dv[0]["SHADE_FAMILY"].ToString().Trim(), dv[0]["SHADE"].ToString());
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtShade.Text = dv[0]["SHADE"].ToString();
                txtRGB.Text = dv[0]["RGB"].ToString();
                txtOpenRate.Text = dv[0]["OP_RATE"].ToString();
                txtOpeningBal.Text = dv[0]["OP_BAL_STOCK"].ToString();
                txtMinStock.Text = dv[0]["MIN_STOCK"].ToString();
                txtMaxStock.Text = dv[0]["MAX_STOCK"].ToString();
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dv[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dv[0]["STORE"].ToString()));
                ViewState["UniqueId"] = UniqueId;
                txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
                txtCartoons.Value = dv[0]["CARTONS"].ToString();
                txtGrossWt.Value = dv[0]["GROSS_WT"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtTareWt.Value = dv[0]["TARE_WT"].ToString();
                txtNoOfUnit.Value = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightOfUnit.Value = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtPartyName.Value = dv[0]["PRTY_NAME"].ToString();
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
                    if (item.Text == dv[0]["PRTY_CODE"].ToString().Trim())
                    {
                        txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        break;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  



   protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
   {
       try
       {
           DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
           // Looping through the items and adding them to the "Items" collection of the ComboBox
           if (data != null && data.Rows.Count > 0)
           {
               cmbShade.Items.Clear();
               cmbShade.DataSource = data;
               cmbShade.DataBind();
           }
           // Calculating the numbr of items loaded so far in the ComboBox
           e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
           // Getting the total number of items that start with the typed text
           e.ItemsCount = GetShadeItemsCount(e.Text);
       }
       catch (Exception ex)
       {
           Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
           lblMode.Text = ex.ToString();
       }
   }

   protected void cmbShade_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
   {
       try
       {
           if (cmbShade.SelectedIndex > -1)
           {
               //string[] arrString = cmbShade.SelectedValue.Split('@');
               //txtShadeFamily.Text = arrString[0].ToString();               
               //txtShade.Text = arrString[1].ToString();  
               txtShadeFamily.Text = cmbShade.SelectedText;               
               txtShade.Text = cmbShade.SelectedValue;            
               
           }
           else
           {
               Common.CommonFuction.ShowMessage("Please select Shade");
           }
       }
       catch (Exception ex)
       {
           Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Selection.\r\nSee error log for detail."));
           lblMode.Text = ex.ToString();
       }
   }

   protected DataTable GetShadeItems(string text, int startOffset)
   {
       try
       {
           string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
           string whereClause = string.Empty;

           if (startOffset != 0)
           {
               whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
           }
           string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
           string SearchQuery = text.ToUpper() + "%";
           DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
           return data;
       }
       catch
       {
           throw;
       }
   }

   protected int GetShadeItemsCount(string text)
   {
       try
       {
           string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
           string WhereClause = " ";
           string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
           string SearchQuery = text.ToUpper() + "%";
           return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;           
       }
       catch
       {
           throw;
       }
   }






   protected void txtRGB_TextChanged(object sender, EventArgs e)
   {
       txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
   }

   public Color getRGBColor(string argb)
   {
       int r = 0;
       int g = 0;
       int b = 0;
       Color RGB = Color.White;
       try
       {

           if (!string.IsNullOrEmpty(argb))
           {
               string[] argbstring = argb.Split(',');
               if (argbstring.Length > 2)
               {
                   int.TryParse(argbstring[0].ToString(), out r);
                   int.TryParse(argbstring[1].ToString(), out g);
                   int.TryParse(argbstring[2].ToString(), out b);

                   if (r > 255 || g > 255 || b > 255 || r < 0 || g < 0 || b < 0)
                   {
                       Common.CommonFuction.ShowMessage("R G B values are being less then 0 or greater then 255.");
                   }
                   else
                   {
                       RGB = Color.FromArgb(r, g, b);
                   }
               }
               else
               {
                   Common.CommonFuction.ShowMessage("make space between R G B values.");
                   RGB = Color.FromArgb(255, 255, 255);
               }

           }
       }
       catch (Exception ex)
       {
           Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is getting Shade color.\r\nSee error log for detail."));
       }
       return RGB;
      
   }
   private void BindDropDown(DropDownList ddl)
   {
       DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
       if (dt != null && dt.Rows.Count > 0)
       {
           ddl.Items.Clear();
           ddl.DataSource = dt;
           ddl.DataTextField = "MST_DESC";
           ddl.DataValueField = "MST_DESC";
           ddl.DataBind();
       }
       else
       {
           ddl.DataSource = null;
           ddl.DataBind();
           ddl.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

       }
       ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));

   }

   private void BindDepartment(DropDownList ddl)
   {
       try
       {
           ddl.Items.Clear();
           DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
           if (dt != null && dt.Rows.Count > 0)
           {
               ddl.DataSource = dt;
               ddl.DataTextField = "MST_DESC";
               ddl.DataValueField = "MST_DESC";
               ddl.DataBind();
               ddl.Items.Insert(0, new ListItem("--Select--", ""));
           }
           else
           {
               DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
               if (dtDepartment != null && dtDepartment.Rows.Count > 0)
               {
                   ddl.DataSource = dtDepartment;
                   ddl.DataTextField = "DEPT_NAME";
                   ddl.DataValueField = "DEPT_NAME";
                   ddl.DataBind();
               }
           }
           ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
       }
       catch
       {
           throw;
       }
   }

   public void setShadeFamilyCombo(string shade_family,string  shade)
   {

       DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery)", "", "", "", "%", "");
       cmbShade.DataSource = data;
       cmbShade.DataTextField = "SHADE_FAMILY_NAME";
       cmbShade.DataValueField = "SHADE_NAME";
       cmbShade.DataBind();
       foreach (ComboBoxItem dl in cmbShade.Items)
       {
           if (dl.Text == shade_family && dl.Value == shade)
           {
               cmbShade.SelectedIndex = cmbShade.Items.IndexOf(dl);
               break;
           }
       }
   }
   protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
   {
       Response.Redirect("~/Module/Yarn/SalesWork/Queries/YarnMasterQuery.aspx", false);
   }

   protected void rdIsExciable_SelectedIndexChanged(object sender, EventArgs e)
   {
       ddlTariffHeading.SelectedIndex = -1;
       if (rdIsExciable.SelectedValue == "1")
       {
           ddlTariffHeading.Enabled = true;
           //txtTariffHeadingValidator.Enabled = true;
       }
       else
       {
           ddlTariffHeading.Enabled = false;
          // txtTariffHeadingValidator.Enabled = false;
       }
   }

  
    protected void btnAssSave_Click(object sender, EventArgs e)
   {
       
        try
       {
          
           if (ViewState["dtAssociatedItemDetail"] != null)
               dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
           else
               CreateAssociatedYarnDetailTable();

           if (!string.IsNullOrEmpty(txtYarnCode.Text) && !string.IsNullOrEmpty(txtYarnDescription.Text) && !string.IsNullOrEmpty(txtAssocatedItemCode.Text) && !string.IsNullOrEmpty(txtAssocatedItemDesc.Text))
           {



                       int AssUniqueId = 0;
                       if (ViewState["AssUniqueId"] != null)
                       {
                           AssUniqueId = int.Parse(ViewState["AssUniqueId"].ToString());
                       }
                       bool bb = SearchAssociatedItemCodeInGrid(txtAssocatedItemCode.Text.Trim(), txtAssocatedItemDesc.Text.Trim(), AssUniqueId);
                       if (!bb)
                       {


                           if (AssUniqueId > 0)
                           {
                               DataView dv = new DataView(dtAssociatedItemDetail);
                               dv.RowFilter = "AssUniqueId=" + AssUniqueId;
                               if (dv.Count > 0)
                               {                                 
                                   dv[0]["YARN_CODE"] = txtYarnCode.Text;
                                   dv[0]["ASS_YARN_CODE"] = txtAssocatedItemCode.Text;
                                   dv[0]["ASS_YARN_DESC"] = txtAssocatedItemDesc.Text;
                                   dtAssociatedItemDetail.AcceptChanges();
                               }
                           }
                           else
                           {

                               DataRow dr = dtAssociatedItemDetail.NewRow();
                               dr["AssUniqueId"] = dtAssociatedItemDetail.Rows.Count + 1;
                               dr["YARN_CODE"] = txtYarnCode.Text;
                               dr["ASS_YARN_CODE"] = txtAssocatedItemCode.Text;
                               dr["ASS_YARN_DESC"] = txtAssocatedItemDesc.Text;
                               dtAssociatedItemDetail.Rows.Add(dr);
                           }
                           //RefreshAssociatedYarnRows();
                       }


                       else
                       {
                           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Yarn Associated Item, Already Added.');", true);
                       }
                       ViewState["dtAssociatedItemDetail"] = dtAssociatedItemDetail;
                       BindAssociatedDetailGrid();
                       txtAssocatedItemCode.Text = getAssociatedYarnCode();
                       ViewState["AssUniqueId"] = null;
              

           }

           else
           {
               CommonFuction.ShowMessage("Yarn Code/Yarn Desc/Associated Item/Associated Desc is not entered.");
           }

       }
       catch
       {
           throw;
       }
   }
  
    protected void btnAssCancel_Click(object sender, EventArgs e)
   {
       RefreshAssociatedYarnRows();
   }
  
    private bool SearchAssociatedItemCodeInGrid(string ASSOCIATED_YARN_CODE,string ASSOCIATED_YARN_DESC, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdAssociatedYarn.Rows)
            {
                Label lblAssocatedItemCode = (Label)grdRow.FindControl("lblAssocatedItemCode");
                Label lblAssocatedItemDesc = (Label)grdRow.FindControl("lblAssociatedItemDesc");
                Button lnkDelete = (Button)grdRow.FindControl("lnkAssDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if ((lblAssocatedItemCode.Text.Trim() == ASSOCIATED_YARN_CODE || lblAssocatedItemDesc.Text.Trim() == ASSOCIATED_YARN_DESC) && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }


   
    protected void grdAssociatedYarn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {

                FillAssociatedDetailByGrid(UniqueId);

            }
            else if (e.CommandName == "indentDelete")
            {                
                DeleteAssociatedDetailRow(UniqueId);
                BindAssociatedDetailGrid();
                ViewState["AssUniqueId"] = null;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Bland Grid Row Command.\r\nSee error log for detail."));
        }
    }

    private void FillAssociatedDetailByGrid(int UniqueId)
    {

        try
        {
            if (ViewState["dtAssociatedItemDetail"] != null)
                dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
            DataView dv = new DataView(dtAssociatedItemDetail);

            dv.RowFilter = "AssUniqueId=" + UniqueId;
            if (dv.Count > 0)
            {

                txtAssocatedItemCode.Text = dv[0]["ASS_YARN_CODE"].ToString();
                txtAssocatedItemDesc.Text = dv[0]["ASS_YARN_DESC"].ToString();                
                ViewState["AssUniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteAssociatedDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtAssociatedItemDetail"] != null)
                dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
            if (grdAssociatedYarn.Rows.Count == 1)
            {
                dtAssociatedItemDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtAssociatedItemDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["AssUniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtAssociatedItemDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtAssociatedItemDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["AssUniqueId"] = iCount;
                }
                ViewState["dtAssociatedItemDetail"] = dtAssociatedItemDetail;
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindAssociatedDetailGrid()
    {
        try
        {
            if (ViewState["dtAssociatedItemDetail"] != null)
                dtAssociatedItemDetail = (DataTable)ViewState["dtAssociatedItemDetail"];
            grdAssociatedYarn.DataSource = dtAssociatedItemDetail;
            grdAssociatedYarn.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void txtYarnDescription_TextChanged(object sender, EventArgs e)
    {
        txtAssocatedItemDesc.Text = txtYarnDescription.Text;
        txtAssocatedItemCode.Text = getAssociatedYarnCode(); 
        
    }


    protected string getAssociatedYarnCode()
    {
        return  txtYarnCode.Text +"-"+(grdAssociatedYarn.Rows.Count + 1).ToString();    
    }


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

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            //string CommandText = "SELECT   PRTY_CODE,PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM   (  SELECT   PRTY_CODE,       PRTY_NAME,PRTY_ADD1,  PRTY_GRP_CODE,   VENDOR_CAT_CODE     FROM   TX_VENDOR_MST               WHERE   NVL (CONF_FLAG, 0) = 1  AND PRTY_GRP_CODE IN ('48')     AND  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE       :SearchQuery)     ORDER BY   PRTY_CODE ASC) asd   WHERE   UPPER (VENDOR_CAT_CODE) NOT IN    ( UPPER('Transporter'), UPPER('Spinner'), UPPER('Broker'))     AND ROWNUM <= 15  ";
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                //whereClause += " AND PRTY_CODE NOT IN (  SELECT   PRTY_CODE,PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM   (  SELECT   PRTY_CODE,       PRTY_NAME,PRTY_ADD1,  PRTY_GRP_CODE,   VENDOR_CAT_CODE     FROM   TX_VENDOR_MST               WHERE   NVL (CONF_FLAG, 0) = 1       AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE       :SearchQuery)     ORDER BY   PRTY_CODE ASC) asd   WHERE   UPPER (VENDOR_CAT_CODE) NOT IN    ( UPPER('Transporter'), UPPER('Spinner'), UPPER('Broker'))    ROWNUM <= " + startOffset + ")";
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

    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyName.Value = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }



    protected void ddlYarnCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        

        BindYarnCode();
        if (ddlYarnCat.SelectedValue == "TEXTURISED" || ddlYarnCat.SelectedValue == "TWISTED")
        {
            lblCount.Text = "Filament";
            lblCountValue.Text = "Filament Value";
            txtCountValue.Visible = true;
            lblCountValue.Visible = true;
        }
        else
        {
            //lblCount.Text = "Count";
            lblCount.Text = "Denier";
            lblCountValue.Text = "Count Value";
            lblCountValue.Visible = false;
            txtCountValue.Visible = false;
        }
    }


    private void BindITCHSCode(string MST_NAME, DropDownList ddl)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddl.Items.Clear();
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_CODE";
                ddl.DataValueField = "MST_CODE";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

}

