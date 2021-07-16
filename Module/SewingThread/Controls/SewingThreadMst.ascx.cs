using System;
using System.Text;
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
using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_Sewing_Thread_Controls_SewingThreadMst : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.YRN_MST oYRN_MST;
    private DataTable dtBaseArticleDetail = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Test Changes MAde";
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            CreateBaseArticleDetailTable();

            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;

            txtArticleNo.Visible = true;
            txtArticleNo.ReadOnly = false;
            cmbArticleNo.Visible = false;
            BindUOM();
            bindMAKE("ST_MAKE");
            bindYarnPLY("YARN_PLY");
            bindCount("YARN_COUNT");
            bindEndUse("END_USE");
            BindBaseBASIS();
            bindYarnBAseProductType();
        }
        catch
        {
            throw;
        }
    }

    private void BlankControls()
    {
        try
        {
            txtArticleNo.Text = string.Empty;
            txtTKTNo.Text = string.Empty;
            ddlUOMUnitWeight.SelectedIndex = 0;
            //txtCount.Text = string.Empty;
            txtTexSize.Text = string.Empty;
            txtTPI.Text = string.Empty;
            txtTwist.Text = string.Empty;
            txtQuality.Text = string.Empty;
            txtBrand.Text = string.Empty;
            txtLengthMtr.Text = string.Empty;
            txtColourOfUnit.Text = string.Empty;
            txtUnitWt.Text = string.Empty;
            txtUnitSize.Text = string.Empty;
            txtBoxPkg.Text = string.Empty;
            txtCartonPkg.Text = string.Empty;
            txtTotalNoOfUnits.Text = string.Empty;
            txtNetCarton.Text = string.Empty;
            txtNetBoxWt.Text = string.Empty;
            txtOpeningStock.Text = string.Empty;
            txtOpeningRate.Text = string.Empty;
            ddlenduse.SelectedIndex = -1;
            ddlMake.SelectedIndex = -1;
            ddlPLy.SelectedIndex = -1;
            ddlCount.SelectedIndex = -1;
            ddlUOMUnitWeight.SelectedIndex = -1;
            // Removed By Rajesh 01 Dec 2011 (Guided By Akhilesh Sir)
            //ddlUOMEmptyCatronWeight.SelectedIndex = -1; 
            //ddlUOMNetBox.SelectedIndex = -1;
            //ddlUomNetCarton.SelectedIndex = -1;
            txtEmptyCarton.Text = string.Empty;
            grdBaseArticleDetail.DataSource = null;
            grdBaseArticleDetail.DataBind();
            txtArticleDescription.Text = string.Empty;
            txtTarifSubheading.Text = string.Empty;

            if (ViewState["dtBaseArticleDetail"] != null)
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
                if (dtBaseArticleDetail.Rows.Count > 0)
                {
                    dtBaseArticleDetail.Rows.Clear();
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindUOM()
    {
        try
        {
            ddlUOMUnitWeight.Items.Clear();
            //ddlUomNetCarton.Items.Clear();
            //ddlUOMEmptyCatronWeight.Items.Clear();
            //ddlUOMNetBox.Items.Clear();
            ddlBaseUOM.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlUOMUnitWeight.DataSource = dt;
                ddlUOMUnitWeight.DataTextField = "MST_DESC";
                ddlUOMUnitWeight.DataValueField = "MST_CODE";
                ddlUOMUnitWeight.DataBind();


                //ddlUomNetCarton.DataSource = dt;
                //ddlUomNetCarton.DataTextField = "MST_DESC";
                //ddlUomNetCarton.DataValueField = "MST_CODE";
                //ddlUomNetCarton.DataBind();


                //ddlUOMEmptyCatronWeight.DataSource = dt;
                //ddlUOMEmptyCatronWeight.DataTextField = "MST_DESC";
                //ddlUOMEmptyCatronWeight.DataValueField = "MST_CODE";
                //ddlUOMEmptyCatronWeight.DataBind();


                //ddlUOMNetBox.DataSource = dt;
                //ddlUOMNetBox.DataTextField = "MST_DESC";
                //ddlUOMNetBox.DataValueField = "MST_CODE";
                //ddlUOMNetBox.DataBind();


                ddlBaseUOM.DataSource = dt;
                ddlBaseUOM.DataTextField = "MST_DESC";
                ddlBaseUOM.DataValueField = "MST_CODE";
                ddlBaseUOM.DataBind();



            }
            ddlUOMUnitWeight.Items.Insert(0, new ListItem("-------- Select Unit -------", "0"));
            //ddlUomNetCarton.Items.Insert(0, new ListItem("-------- Select Unit -------", "0"));
            //ddlUOMEmptyCatronWeight.Items.Insert(0, new ListItem("-------- Select Unit -------", "0"));
            //ddlUOMNetBox.Items.Insert(0, new ListItem("-------- Select Unit -------", "0"));
            ddlBaseUOM.Items.Insert(0, new ListItem("-------- Select Unit -------", "0"));
        }
        catch
        {
            throw;
        }
    }

    #region MOMS
    public void bindMAKE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlMake.Items.Clear();
                ddlMake.DataSource = dt;
                ddlMake.DataTextField = "MST_DESC";
                ddlMake.DataValueField = "MST_CODE";
                ddlMake.DataBind();
                ddlMake.Items.Insert(0, new ListItem("------Select------", "0"));

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

                ddlPLy.Items.Clear();
                ddlPLy.DataSource = dt;
                ddlPLy.DataTextField = "MST_DESC";
                ddlPLy.DataValueField = "MST_CODE";
                ddlPLy.DataBind();
                ddlPLy.Items.Insert(0, new ListItem("------Select------", "Select"));

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
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlCount.Items.Clear();
                ddlCount.DataSource = dt;
                ddlCount.DataTextField = "MST_DESC";
                ddlCount.DataValueField = "MST_CODE";
                ddlCount.DataBind();
                ddlCount.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindEndUse(string MST_NAME)
    {
        try
        {
            //////////////////// Bind EndUse By Abhishek 09-10-2011 ////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlenduse.Items.Clear();
                ddlenduse.DataSource = dt;
                ddlenduse.DataTextField = "MST_DESC";
                ddlenduse.DataValueField = "MST_CODE";
                ddlenduse.DataBind();
                ddlenduse.Items.Insert(0, new ListItem("------Select------", "0"));
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

            //DataTable dt = new DataTable();
            //dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_BASIS", oUserLoginDetail.COMP_CODE);
            //ddlBaseBasis.DataSource = dt;
            //ddlBaseBasis.DataValueField = "MST_CODE";
            //ddlBaseBasis.DataTextField = "MST_DESC";
            //ddlBaseBasis.DataBind();
            ////ddlBaseBasis.Items.Insert(0, new ListItem("-------Select--------", ""));
            //dt.Dispose();
            //dt = null;
        }
        catch
        {
            throw;

        }

    }
    #endregion

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }
            if (dtBaseArticleDetail.Rows.Count > 0)
            {
                InsertData();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Create One Base Detail Alteast!!");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {

            if (txtArticleNo.Text != "")
            {
                // CalculateAllFormCalculation();
                oYRN_MST = new SaitexDM.Common.DataModel.YRN_MST();

                oYRN_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRN_MST.ARTICLE_CODE = txtArticleNo.Text.ToUpper().Trim();
                oYRN_MST.YARN_CODE = txtArticleNo.Text.ToUpper().Trim();
                oYRN_MST.YARN_DESC = ddlPLy.SelectedItem.ToString() + "/" +
                    ddlCount.SelectedItem.ToString() + "  " +
                    txtQuality.Text.Trim() + "-" +
                    txtLengthMtr.Text.Trim() + " MTRS -" +
                    ddlMake.SelectedItem.ToString();

                oYRN_MST.YARN_CAT = "SEWING THREAD";            // Fixed For Sewing Thread Master..
                oYRN_MST.PRTY_CODE = "SELF";
                oYRN_MST.UOM_OF_UNIT_WEIGHT = ddlUOMUnitWeight.SelectedValue.ToString().ToUpper().Trim();
                oYRN_MST.TKTNO = txtTKTNo.Text.ToUpper().Trim();

                oYRN_MST.MAKE = ddlMake.SelectedItem.ToString();
                oYRN_MST.PLY = ddlPLy.SelectedItem.ToString();
                oYRN_MST.Y_COUNT = double.Parse(ddlCount.SelectedItem.ToString());

                double dblSizeLong = 0;
                 double.TryParse(txtTexSize.Text.Trim(), out dblSizeLong);
                oYRN_MST.SIZ_LONG = dblSizeLong;

                double dblTPI = 0;
                double.TryParse(txtTPI.Text.Trim(), out dblTPI);
                oYRN_MST.TPI = dblTPI;

                oYRN_MST.TWIST = txtTwist.Text.Trim();
                oYRN_MST.YARN_QUALITY = txtQuality.Text.Trim();
                oYRN_MST.BRAND_NAME = txtBrand.Text.Trim();
                oYRN_MST.ENDUSE = ddlenduse.SelectedItem.ToString().ToUpper().Trim();
                oYRN_MST.LENMTR = txtLengthMtr.Text.Trim();
                oYRN_MST.COLOUR = txtColourOfUnit.Text.Trim();
                oYRN_MST.UOM = ddlUOM.SelectedValue.Trim();

                double dblUnitWt = 0;
                double.TryParse(txtUnitWt.Text, out dblUnitWt);
                oYRN_MST.UNITWT = dblUnitWt;

                double dblUnitSize = 0;
                double.TryParse(txtUnitSize.Text.Trim(), out dblUnitSize);
                oYRN_MST.UNITSIZE = dblUnitSize;

                double dblBoxPkg = 0;
                double.TryParse(txtBoxPkg.Text.Trim(), out dblBoxPkg);
                oYRN_MST.BOXPKG_PERBOX = dblBoxPkg;

                double dblCartonPkg = 0;
                double.TryParse(txtCartonPkg.Text.Trim(), out dblCartonPkg);
                oYRN_MST.CARTPKG_NO_OF_BOX = dblCartonPkg;

                double dblTotNoUnit = 0;
                double.TryParse(txtTotalNoOfUnits.Text.Trim(), out dblTotNoUnit);
                oYRN_MST.TOT_NO_OF_UNIT = dblTotNoUnit;

                double dblNetCartonWt = 0;
                double.TryParse(txtNetCarton.Text.Trim(), out dblNetCartonWt);
                oYRN_MST.NET_CART_WT = dblNetCartonWt;

                double dblNetBoxWt = 0;
                double.TryParse(txtNetBoxWt.Text.Trim(), out dblNetBoxWt);
                oYRN_MST.NET_BOX_WT = dblNetBoxWt;
                oYRN_MST.TUSER = oUserLoginDetail.UserCode;

                double dblOpenStock = 0;
                double.TryParse(txtOpeningStock.Text.Trim(), out dblOpenStock);
                oYRN_MST.OP_BAL_STOCK = dblOpenStock;

                double dblOpenRate = 0;
                double.TryParse(txtOpeningRate.Text.Trim(), out dblOpenRate);
                oYRN_MST.OP_RATE = dblOpenRate;

                oYRN_MST.UOM_OF_UNIT_WEIGHT = ddlUOMUnitWeight.SelectedItem.ToString();
                oYRN_MST.UOM_OF_NET_CARTON_WT = ddlUOMUnitWeight.SelectedItem.ToString();
                oYRN_MST.UOM_OF_EMPTY_CARTON_WEIGHT = ddlUOMUnitWeight.SelectedItem.ToString();
                oYRN_MST.UOM_OF_NET_BOX_WT = ddlUOMUnitWeight.SelectedItem.ToString();
                oYRN_MST.TRAFIF_SUB_HEADING = txtTarifSubheading.Text.Trim();

                double TransferPrice = 0;
                oYRN_MST.TRANSFER_PRICE = TransferPrice;

                double SalePrice = 0;
                oYRN_MST.SALE_PRICE = SalePrice;

                double EMPTY_CARTON_WEIGHT = 0;
                double.TryParse(txtEmptyCarton.Text.Trim(), out EMPTY_CARTON_WEIGHT);
                oYRN_MST.EMPTY_CARTON_WEIGHT = EMPTY_CARTON_WEIGHT;

                if (IsExciseable.Checked)
                {
                    oYRN_MST.IS_EXCISEABLE = true;
                }
                else
                {
                    oYRN_MST.IS_EXCISEABLE = false;
                }

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.YRN_MST.InsertSewingThreadMst(oYRN_MST, out iRecordFound, dtBaseArticleDetail);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Sewing Thread Master entry saved successfully!");
                    BlankControls();
                }
                else if (iRecordFound == 1)
                {
                    Common.CommonFuction.ShowMessage("This Sewing Thread Master is already saved.. Please try another..");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Problem in Saving Sewing Thread Master..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter Article Number..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            txtArticleNo.ReadOnly = true;
            txtArticleNo.Visible = false;
            cmbArticleNo.Visible = true;
            cmbArticleNo.SelectedIndex = -1;
            BlankControls();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }
            if (dtBaseArticleDetail.Rows.Count > 0)
            {
                UpdateData();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Create One Base Detail Alteast!!");

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
        }
    }

    private void UpdateData()
    {
        try
        {

            if (cmbArticleNo.SelectedIndex != -1)
            {
                //CalculateAllFormCalculation();
                oYRN_MST = new SaitexDM.Common.DataModel.YRN_MST();

                oYRN_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRN_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRN_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRN_MST.ARTICLE_CODE = cmbArticleNo.SelectedValue.ToString().Trim();
                oYRN_MST.YARN_DESC = ddlPLy.SelectedItem.ToString() + "/" +
                    ddlCount.SelectedItem.ToString() + "  " +
                    txtQuality.Text.Trim() + "-" +
                    txtLengthMtr.Text.Trim() + " MTRS -" +
                    ddlMake.SelectedItem.ToString();
                oYRN_MST.YARN_CAT = "SEWING THREAD"; // Fixed For Sewing Thread Master..
                oYRN_MST.PRTY_CODE = "SELF";
                oYRN_MST.UOM_OF_UNIT_WEIGHT = ddlUOMUnitWeight.SelectedValue.ToString().ToUpper().Trim();
                oYRN_MST.TKTNO = txtTKTNo.Text.ToUpper().Trim();

                oYRN_MST.MAKE = ddlMake.SelectedItem.ToString();
                oYRN_MST.PLY = ddlPLy.SelectedItem.ToString();
                oYRN_MST.Y_COUNT = double.Parse(ddlCount.SelectedItem.ToString());
                oYRN_MST.UOM = ddlUOM.SelectedValue.Trim();

                double dblSizeLong = 0;
                double.TryParse(txtTexSize.Text.Trim(), out dblSizeLong);
                oYRN_MST.SIZ_LONG = dblSizeLong;

                double dblTPI = 0;
                double.TryParse(txtTPI.Text.Trim(), out dblTPI);
                oYRN_MST.TPI = dblTPI;

                oYRN_MST.TWIST = txtTwist.Text.Trim();
                oYRN_MST.YARN_QUALITY = txtQuality.Text.Trim();
                oYRN_MST.BRAND_NAME = txtBrand.Text.Trim();
                oYRN_MST.ENDUSE = ddlenduse.SelectedItem.ToString().ToUpper().Trim();
                oYRN_MST.LENMTR = txtLengthMtr.Text.Trim();
                oYRN_MST.COLOUR = txtColourOfUnit.Text.Trim();

                double dblUnitWt = 0;
                double.TryParse(txtUnitWt.Text, out dblUnitWt);
                oYRN_MST.UNITWT = dblUnitWt;

                double dblUnitSize = 0;
                double.TryParse(txtUnitSize.Text.Trim(), out dblUnitSize);
                oYRN_MST.UNITSIZE = dblUnitSize;

                double dblBoxPkg = 0;
                double.TryParse(txtBoxPkg.Text.Trim(), out dblBoxPkg);
                oYRN_MST.BOXPKG_PERBOX = dblBoxPkg;

                double dblCartonPkg = 0;
                double.TryParse(txtCartonPkg.Text.Trim(), out dblCartonPkg);
                oYRN_MST.CARTPKG_NO_OF_BOX = dblCartonPkg;

                double dblTotNoUnit = 0;
                double.TryParse(txtTotalNoOfUnits.Text.Trim(), out dblTotNoUnit);
                oYRN_MST.TOT_NO_OF_UNIT = dblTotNoUnit;

                double dblNetCartonWt = 0;
                double.TryParse(txtNetCarton.Text.Trim(), out dblNetCartonWt);
                oYRN_MST.NET_CART_WT = dblNetCartonWt;

                double dblNetBoxWt = 0;
                double.TryParse(txtNetBoxWt.Text.Trim(), out dblNetBoxWt);
                oYRN_MST.NET_BOX_WT = dblNetBoxWt;
                oYRN_MST.TUSER = oUserLoginDetail.UserCode;

                double dblOpenStock = 0;
                double.TryParse(txtOpeningStock.Text.Trim(), out dblOpenStock);
                oYRN_MST.OP_BAL_STOCK = dblOpenStock;

                double dblOpenRate = 0;
                double.TryParse(txtOpeningRate.Text.Trim(), out dblOpenRate);
                oYRN_MST.OP_RATE = dblOpenRate;


                oYRN_MST.UOM_OF_UNIT_WEIGHT = ddlUOMUnitWeight.SelectedItem.ToString();
                oYRN_MST.UOM_OF_NET_CARTON_WT = ddlUOMUnitWeight.SelectedItem.ToString(); // Added By Rajesh Guided By Akhilesh Sir
                oYRN_MST.UOM_OF_EMPTY_CARTON_WEIGHT = ddlUOMUnitWeight.SelectedItem.ToString();
                oYRN_MST.UOM_OF_NET_BOX_WT = ddlUOMUnitWeight.SelectedItem.ToString();

                double EMPTY_CARTON_WEIGHT = 0;
                double.TryParse(txtEmptyCarton.Text.Trim(), out EMPTY_CARTON_WEIGHT);
                oYRN_MST.EMPTY_CARTON_WEIGHT = EMPTY_CARTON_WEIGHT;
                oYRN_MST.YARN_DESC = txtArticleDescription.Text.Trim();
                oYRN_MST.TRAFIF_SUB_HEADING = txtTarifSubheading.Text.Trim();

                double TransferPrice = 0;
                oYRN_MST.TRANSFER_PRICE = TransferPrice;

                double SalePrice = 0;
                oYRN_MST.SALE_PRICE = SalePrice;

                if (IsExciseable.Checked)
                {
                    oYRN_MST.IS_EXCISEABLE = true;
                }
                else
                {
                    oYRN_MST.IS_EXCISEABLE = false;
                }

                int iRecordFound = 0;
                bool bResult = SaitexBL.Interface.Method.YRN_MST.UpdateSewingThreadMst(oYRN_MST, out iRecordFound, dtBaseArticleDetail);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Sewing Thread Master entry updated successfully!");
                    BlankControls();
                    InitialisePage();
                }
                else if (iRecordFound == 1)
                {
                    Common.CommonFuction.ShowMessage("This Sewing Thread Master is already saved.. Please try another..");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Problem in Updating Sewing Thread Master..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Enter Article Number..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Common.CommonFuction.ShowMessage("Sorry Dear ! No Deletion allowed..");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./SewingThread.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    private void FillDataForEdit(string YARN_CODE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetYarnMasterWithYarnCategoryAndYarnCode(YARN_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                bindMAKE("ST_MAKE");
                bindYarnPLY("YARN_PLY");
                bindCount("YARN_COUNT");
                txtArticleNo.Text = dt.Rows[0]["YARN_CODE"].ToString();
                txtTKTNo.Text = dt.Rows[0]["TKTNO"].ToString();
                ddlUOMUnitWeight.SelectedIndex = ddlUOMUnitWeight.Items.IndexOf(ddlUOMUnitWeight.Items.FindByText(dt.Rows[0]["UOM_OF_UNIT_WEIGHT"].ToString()));
                //ddlUomNetCarton.SelectedIndex = ddlUomNetCarton.Items.IndexOf(ddlUomNetCarton.Items.FindByText(dt.Rows[0]["UOM_OF_NET_CARTON_WT"].ToString()));
                //ddlUOMEmptyCatronWeight.SelectedIndex = ddlUOMEmptyCatronWeight.Items.IndexOf(ddlUOMEmptyCatronWeight.Items.FindByText(dt.Rows[0]["UOM_OF_EMPTY_CARTON_WEIGHT"].ToString()));
                //ddlUOMNetBox.SelectedIndex = ddlUOMNetBox.Items.IndexOf(ddlUOMNetBox.Items.FindByText(dt.Rows[0]["UOM_OF_NET_BOX_WT"].ToString()));
                txtEmptyCarton.Text = dt.Rows[0]["EMPTY_CARTON_WEIGHT"].ToString();
                ddlenduse.SelectedIndex = ddlenduse.Items.IndexOf(ddlenduse.Items.FindByText(dt.Rows[0]["ENDUSE"].ToString()));
                ddlMake.SelectedIndex = ddlMake.Items.IndexOf(ddlMake.Items.FindByText(dt.Rows[0]["MAKE"].ToString()));
                ddlPLy.SelectedIndex = ddlPLy.Items.IndexOf(ddlPLy.Items.FindByText(dt.Rows[0]["PLY"].ToString()));
                ddlCount.SelectedIndex = ddlCount.Items.IndexOf(ddlCount.Items.FindByText(dt.Rows[0]["Y_COUNT"].ToString()));
                ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByText(dt.Rows[0]["UOM"].ToString()));

                txtTexSize.Text = dt.Rows[0]["SIZ_LONG"].ToString();
                txtTPI.Text = dt.Rows[0]["TPI"].ToString();
                txtTwist.Text = dt.Rows[0]["TWIST"].ToString();
                txtQuality.Text = dt.Rows[0]["YARN_QUALITY"].ToString();
                txtBrand.Text = dt.Rows[0]["BRAND_NAME"].ToString();
                txtLengthMtr.Text = dt.Rows[0]["LENMTR"].ToString();
                txtColourOfUnit.Text = dt.Rows[0]["COLOUR"].ToString();
                txtUnitWt.Text = dt.Rows[0]["UNITWT"].ToString();
                txtUnitSize.Text = dt.Rows[0]["UNITSIZE"].ToString();
                txtBoxPkg.Text = dt.Rows[0]["BOXPKG_PERBOX"].ToString();
                txtCartonPkg.Text = dt.Rows[0]["CARTPKG_NO_OF_BOX"].ToString();
                txtTotalNoOfUnits.Text = dt.Rows[0]["TOT_NO_OF_UNIT"].ToString();
                txtNetCarton.Text = dt.Rows[0]["NET_CART_WT"].ToString();
                txtNetBoxWt.Text = dt.Rows[0]["NET_BOX_WT"].ToString();
                txtOpeningStock.Text = dt.Rows[0]["OP_BAL_STOCK"].ToString();
                txtOpeningRate.Text = dt.Rows[0]["OP_RATE"].ToString();
                txtArticleDescription.Text = dt.Rows[0]["YARN_DESC"].ToString();
                txtTarifSubheading.Text = dt.Rows[0]["TRAFIF_SUB_HEADING"].ToString();

                int IsExcise = 0;
                int.TryParse(dt.Rows[0]["IS_EXCISEABLE"].ToString(), out IsExcise);
                if (IsExcise == 1)
                {
                    IsExciseable.Checked = true;
                }
                else
                {
                    IsExciseable.Checked = false;
                }

                DataTable dtBaseArtilce = SaitexBL.Interface.Method.YRN_MST.GetYarnBaseArticleByYarnCode(dt.Rows[0]["YARN_CODE"].ToString());
                if (dtBaseArtilce != null)
                {
                    if (dtBaseArtilce != null && dtBaseArtilce.Rows.Count > 0)
                    {
                        MapBaseArticleRowDataTable(dtBaseArtilce);
                        grdBaseArticleDetail.DataSource = dtBaseArticleDetail;
                        grdBaseArticleDetail.DataBind();
                    }
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Sorry dear ! There is no record found related to this Article Code.");
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindArticleCode();
    }

    #region For Base Article Detail Datatable

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
                ddlProductType.Items.Remove("Fabric");
                ddlProductType.Items.Remove("Item");


            }

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
            if (ViewState["dtBaseArticleDetail"] != null)
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }
            if (dtBaseArticleDetail == null)
            {
                CreateBaseArticleDetailTable();
            }

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
                                dv[0]["ArticleCode"] = txtBaseArticleCode.SelectedItem.ToString();
                                dv[0]["UOM"] = ddlBaseUOM.SelectedItem.ToString();
                                dv[0]["Basis"] = string.Empty;// ddlBaseBasis.SelectedItem.ToString();
                                dv[0]["ValueQty"] = txtValueQty.Text;
                                dtBaseArticleDetail.AcceptChanges();
                            }
                        }
                        else
                        {

                            DataRow dr = dtBaseArticleDetail.NewRow();
                            dr["UniqueId"] = dtBaseArticleDetail.Rows.Count + 1;
                            dr["ProductType"] = ddlProductType.SelectedItem.ToString().Trim();
                            dr["ArticleCode"] = txtBaseArticleCode.SelectedItem.ToString();
                            dr["UOM"] = ddlBaseUOM.SelectedItem.ToString();
                            dr["Basis"] = string.Empty;// ddlBaseBasis.SelectedItem.ToString();
                            dr["ValueQty"] = txtValueQty.Text;
                            dtBaseArticleDetail.Rows.Add(dr);
                        }
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
                    ViewState["dtBaseArticleDetail"] = dtBaseArticleDetail;
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
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }

            DataView dv = new DataView(dtBaseArticleDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {

                ddlProductType.SelectedValue = dv[0]["ProductType"].ToString();
                BindArticleCode();
                txtBaseArticleCode.Items.Insert(0, new ListItem("----Select---", ""));
                txtBaseArticleCode.SelectedIndex = txtBaseArticleCode.Items.IndexOf(txtBaseArticleCode.Items.FindByValue(dv[0]["ArticleCode"].ToString()));

                ddlBaseUOM.SelectedValue = dv[0]["UOM"].ToString();
                // ddlBaseBasis.SelectedValue = dv[0]["Basis"].ToString();
                txtValueQty.Text = dv[0]["ValueQty"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
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
                txtBaseArticleCode.DataTextField = "ARTICLE_CODE";
                txtBaseArticleCode.DataBind();
                txtBaseArticleCode.Items.Insert(0, new ListItem("----Select---", ""));

            }
            else
            {
                txtBaseArticleCode.Items.Clear();
                txtBaseArticleCode.Items.Insert(0, new ListItem("----Select----", ""));
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
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }

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
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }
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
        ddlProductType.SelectedIndex = -1;
        txtBaseArticleCode.SelectedIndex = -1;
        ddlBaseUOM.SelectedIndex = -1;
        // ddlBaseBasis.SelectedIndex = -1;
        txtValueQty.Text = string.Empty;
        ViewState["UniqueId"] = null;
    }

    private void MapBaseArticleRowDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtBaseArticleDetail"] != null)
            {
                dtBaseArticleDetail = (DataTable)ViewState["dtBaseArticleDetail"];
            }
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
                        dr["UOM"] = drTemp["UOM"];
                        dr["Basis"] = drTemp["BASIS"];
                        dr["ValueQty"] = drTemp["VALUE_QTY"];
                        dtBaseArticleDetail.Rows.Add(dr);
                    }
                }
                dtTemp = null;
                ViewState["dtBaseArticleDetail"] = dtBaseArticleDetail;
            }
        }
        catch
        {
            throw;
        }
    }
    #endregion

    protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddlCount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void cmbArticleNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetArticleData(e.Text.ToUpper(), e.ItemsOffset);

            cmbArticleNo.Items.Clear();

            cmbArticleNo.DataSource = data;
            cmbArticleNo.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetArticleCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetArticleData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT YARN_CAT, YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) asd WHERE UPPER (YARN_CAT) = UPPER ('SEWING THREAD') AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and YARN_CODE not in ( SELECT YARN_CODE FROM (SELECT YARN_CAT, YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) asd WHERE UPPER (YARN_CAT) = UPPER ('SEWING THREAD') AND ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by YARN_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetArticleCount(string text)
    {
        string CommandText = " SELECT * FROM (SELECT YARN_CAT, YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE ASC) asd WHERE UPPER (YARN_CAT) = UPPER ('SEWING THREAD')  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            BlankControls();
            FillDataForEdit(cmbArticleNo.SelectedValue.ToString().Trim());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting Article Code..\r\nSee error log for detail."));
        }
    }
}