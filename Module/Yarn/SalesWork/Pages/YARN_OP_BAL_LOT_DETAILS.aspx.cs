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

public partial class Module_Appaerals_Pages_YARN_OP_BAL_LOT_DETAILS : System.Web.UI.Page
{
    private DataTable dtTRN_SUB = null;
    private string PI_TYPE = string.Empty;
    private string YARN_CODE = string.Empty;
    private double lblMaxQTY = 0;
    private int PO_NUMB = 999998;
    private string PO_TYPE = "OPI";
    private string PO_COMP_CODE = "C99999";
    private string PO_BRANCH = "B99999";
    private string txtboxClientid = string.Empty;
    private string txtNoOfUnit = string.Empty;
    private string txtUOm = string.Empty;
    private string txtWeightOfUnit = string.Empty;
    private string LOT_NO = string.Empty;
    private string GRADE = string.Empty;
    private string PI_NO = string.Empty;
    private string GROSS_WT = string.Empty;
    private string TARE_WT = string.Empty;
    private string CARTONS = string.Empty;
    private string DYED_BATCH = string.Empty;
    private string SHADE_FAMILY = string.Empty;
    private string SHADE = string.Empty;
    private string RGB = string.Empty;
    private double OP_BAL = 0;
    private string UOM = string.Empty;
    private string STORE = string.Empty;
    private string LOCATION = string.Empty;
    private string PRTY_CODE = string.Empty;
    private string txtNoOfPallet = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["LOT_NO"] != null)
            {
                lblShowLotNO.Text = LOT_NO = Request.QueryString["LOT_NO"].Trim();

            }
            if (Request.QueryString["GRADE"] != null)
            {
                txtGrade.Text = GRADE = Request.QueryString["GRADE"].Trim();
            }

            if (Request.QueryString["PI_NO"] != null)
            {
                PI_NO = Request.QueryString["PI_NO"].Trim();
            }
            if (Request.QueryString["PO_NUMB"] != null)
            {
                PO_NUMB = Convert.ToInt32(Request.QueryString["PO_NUMB"].Trim().ToString());
            }
            if (Request.QueryString["PO_TYPE"] != null)
            {
                PO_TYPE = Request.QueryString["PO_TYPE"].Trim();
            }
            if (Request.QueryString["PO_COMP_CODE"] != null)
            {
                PO_COMP_CODE = Request.QueryString["PO_COMP_CODE"].Trim();
            }
            if (Request.QueryString["PO_BRANCH"] != null)
            {
                PO_BRANCH = Request.QueryString["PO_BRANCH"].Trim();
            }

            if (Request.QueryString["YARN_CODE"] != null)
            {
                YARN_CODE = Request.QueryString["YARN_CODE"].Trim();
                lblYarnCode.Text = YARN_CODE;
            }
            if (Request.QueryString["lblMaxQTY"] != null)
            {
                lblMaxQTY = double.Parse(Request.QueryString["lblMaxQTY"].Trim());

            }
            if (Request.QueryString["txtQTY"] != null)
            {
                txtboxClientid = Request.QueryString["txtQTY"].Trim();
            }

            if (Request.QueryString["txtNoOfUnit"] != null)
            {
                txtNoOfUnit = Request.QueryString["txtNoOfUnit"].Trim();
            }
            if (Request.QueryString["txtUOm"] != null)
            {
                txtUOm = Request.QueryString["txtUOm"].Trim();
            }
            if (Request.QueryString["txtWeightOfUnit"] != null)
            {
                txtWeightOfUnit = Request.QueryString["txtWeightOfUnit"].Trim();
            }
            if (Request.QueryString["SHADE"] != null)
            {
                txtShadeCode.Text = Request.QueryString["SHADE"].Trim();
            }
            if (Request.QueryString["SHADE_FAMILY"] != null)
            {
                txtShadeFamily.Text = Request.QueryString["SHADE_FAMILY"].Trim();
            }
            if (Request.QueryString["RGB"] != null)
            {
                RGB = Request.QueryString["RGB"].Trim();
            }
            if (Request.QueryString["GROSS_WT"] != null)
            {
                GROSS_WT = Request.QueryString["GROSS_WT"].Trim();
            }
            if (Request.QueryString["TARE_WT"] != null)
            {
                TARE_WT = Request.QueryString["TARE_WT"].Trim();
            }
            if (Request.QueryString["CARTONS"] != null)
            {
                CARTONS = Request.QueryString["CARTONS"].Trim();
            }
            if (Request.QueryString["DYED_BATCH"] != null)
            {
                DYED_BATCH = Request.QueryString["DYED_BATCH"].Trim();
            }
            if (Request.QueryString["GRADE"] != null)
            {
                GRADE = txtGrade.Text = Request.QueryString["GRADE"].Trim();
            }
            if (Request.QueryString["STORE"] != null)
            {
                STORE = Request.QueryString["STORE"].Trim();

            }
            if (Request.QueryString["LOCATION"] != null)
            {
                LOCATION = Request.QueryString["LOCATION"].Trim();

            }
            if (Request.QueryString["PRTY_CODE"] != null)
            {
                PRTY_CODE = Request.QueryString["PRTY_CODE"].Trim();

            }
            if (Request.QueryString["txtNoOfPallet"] != null)
            {
                txtNoOfPallet = Request.QueryString["txtNoOfPallet"].Trim();
            }

            if (!IsPostBack)
            {
                BindIntial();
                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();

                lblRemaining.Text = lblMaxQTY.ToString();
                txtQty.Text = lblMaxQTY.ToString();


                if (Session["dtTRN_SUB"] != null)
                {

                    dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

                }
                else
                {
                    grdsub_trn.DataSource = null;
                    grdsub_trn.DataBind();
                    if (dtTRN_SUB != null && dtTRN_SUB.Rows.Count > 0)
                    {
                        dtTRN_SUB.Clear();
                    }
                }
                bindUOM("UOM");
                BindBOMGrid();

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    public void BindIntial()
    {

        try
        {

            //txtGrade.Text = "NA";
            txtCartornNo.Text = "NA";
            txtDyedBatchNo.Text = "NA";
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
        }
        catch
        {

        }

    }
    public void clearInitial()
    {
        //txtGrade.Text = string.Empty;
        txtCartornNo.Text = string.Empty;
        txtDyedBatchNo.Text = string.Empty;
        txtNoofUnit.Text = string.Empty;
        txtWeightofUnit.Text = string.Empty;
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
                ddlUOM.SelectedValue = "KGS";
                //ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    private DataTable CreateSUBTRNTable()
    {
        try
        {


            dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("YARN_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtTRN_SUB.Columns.Add("DYED_BATCH", typeof(string));
            dtTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("PI_NO", typeof(string));
            dtTRN_SUB.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("SHADE", typeof(string));
            dtTRN_SUB.Columns.Add("SHADE_FAMILY", typeof(string));
            dtTRN_SUB.Columns.Add("CARTON_NO", typeof(string));
            dtTRN_SUB.Columns.Add("GROSS_WT", typeof(double));
            dtTRN_SUB.Columns.Add("TARE_WT", typeof(double));
            dtTRN_SUB.Columns.Add("BARCODE_NO", typeof(string));
            dtTRN_SUB.Columns.Add("STORE", typeof(string));
            dtTRN_SUB.Columns.Add("LOCATION", typeof(string));
            dtTRN_SUB.Columns.Add("RGB", typeof(string));
            dtTRN_SUB.Columns.Add("PRTY_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("IS_PALLET", typeof(string));
            return dtTRN_SUB;

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

            if (Session["dtTRN_SUB"] != null)
            {

                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

            }
            else
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }


            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "' and SHADE_FAMILY='" + txtShadeFamily.Text.Trim() + "' AND PO_NUMB='" + PO_NUMB + "'  AND PI_NO='" + PI_NO + "'  AND LOT_NO='" + LOT_NO + "'  AND GRADE='" + GRADE + "'  AND PRTY_CODE='" + PRTY_CODE + "'";
            if (dv.Count > 0)
            {
                clearInitial();
                txtQty.Text = string.Empty;
                grdsub_trn.DataSource = dv;
                grdsub_trn.DataBind();
                CalculateAllData();
            }
            else
            {
                grdsub_trn.DataSource = null;
                grdsub_trn.DataBind();
            }
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

            if (Session["dtTRN_SUB"] != null)
            {

                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

            }
            else
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }
            txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();

            // if (dtTRN_SUB.Rows.Count < 15)
            //{
            if (CheckQTYtotal())
            {
                int UNIQUE_ID = 0;
                if (ViewState["UNIQUE_ID"] != null)
                {
                    UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                }
                bool bb = SearchInBOMgrid(txtCartornNo.Text, UNIQUE_ID);
                if (!bb)
                {
                    if (UNIQUE_ID > 0)
                    {
                        DataView dv = new DataView(dtTRN_SUB);
                        dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "' and SHADE_FAMILY='" + txtShadeFamily.Text.Trim() + "' and SHADE_FAMILY='" + txtShadeFamily.Text.Trim() + "' and UNIQUE_ID=" + UNIQUE_ID;
                        if (dv.Count > 0)
                        {
                            double QTY = 0f;
                            double.TryParse(txtQty.Text.Trim(), out QTY);
                            double TEARWT = 0f;
                            double.TryParse(txtTareWt.Text.Trim(), out TEARWT);
                            double GROSSWT = 0f;
                            double.TryParse(txtGrossWt.Text.Trim(), out GROSSWT);
                            dv[0]["TRN_QTY"] = QTY;
                            dv[0]["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                            dv[0]["PO_TYPE"] = PO_TYPE;
                            dv[0]["PO_COMP_CODE"] = PO_COMP_CODE;
                            dv[0]["PO_BRANCH"] = PO_BRANCH;
                            dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dv[0]["GRADE"] = GRADE;
                            dv[0]["LOT_NO"] = LOT_NO;
                            dv[0]["CARTON_NO"] = txtCartornNo.Text.Trim();
                            dv[0]["DYED_BATCH"] = txtDyedBatchNo.Text.Trim();
                            dv[0]["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                            dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                            dv[0]["WEIGHT_OF_UNIT"] = Math.Round(double.Parse(txtWeightofUnit.Text.Trim()), 3);
                            dv[0]["PI_NO"] = PI_NO;
                            dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dv[0]["SHADE"] = txtShadeCode.Text.Trim();
                            dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                            dv[0]["GROSS_WT"] = GROSSWT;
                            dv[0]["TARE_WT"] = TEARWT;
                            dv[0]["BARCODE_NO"] = txtBarCode.Text.Trim();
                            dv[0]["RGB"] = RGB;
                            dv[0]["PRTY_CODE"] = PRTY_CODE;
                            dv[0]["IS_PALLET"] = (isPallet.Checked == true) ? "1" : "0";
                            dtTRN_SUB.AcceptChanges();
                        }
                    }
                    else
                    {


                        DataRow dr = dtTRN_SUB.NewRow();
                        dr["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                        dr["YARN_CODE"] = YARN_CODE;

                        double QTY = 0f;
                        double.TryParse(txtQty.Text.Trim(), out QTY);
                        double TEARWT = 0f;
                        double.TryParse(txtTareWt.Text.Trim(), out TEARWT);
                        double GROSSWT = 0f;
                        double.TryParse(txtGrossWt.Text.Trim(), out GROSSWT);
                        dr["TRN_QTY"] = QTY;
                        dr["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                        dr["PO_TYPE"] = PO_TYPE;
                        dr["PO_COMP_CODE"] = PO_COMP_CODE;
                        dr["PO_BRANCH"] = PO_BRANCH;
                        dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                        dr["GRADE"] = GRADE;
                        dr["LOT_NO"] = LOT_NO.Trim();
                        dr["CARTON_NO"] = txtCartornNo.Text.Trim();
                        dr["DYED_BATCH"] = txtDyedBatchNo.Text.Trim();
                        dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                        dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                        dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                        dr["WEIGHT_OF_UNIT"] = Math.Round(double.Parse(txtWeightofUnit.Text.Trim()), 3);
                        dr["PI_NO"] = PI_NO;
                        dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                        dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                        dr["SHADE"] = txtShadeCode.Text.Trim();
                        dr["GROSS_WT"] = GROSSWT;
                        dr["TARE_WT"] = TEARWT;
                        dr["BARCODE_NO"] = txtBarCode.Text.Trim();

                        dr["STORE"] = STORE;


                        dr["LOCATION"] = LOCATION;

                        dr["RGB"] = RGB;
                        dr["PRTY_CODE"] = PRTY_CODE;
                        dr["IS_PALLET"] = (isPallet.Checked == true) ? "1" : "0";
                        dtTRN_SUB.Rows.Add(dr);

                    }
                    Session["dtTRN_SUB"] = dtTRN_SUB;
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
                Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity !! Limit is =: " + lblMaxQTY);

            }
            //}

            //else
            //{
            //    Common.CommonFuction.ShowMessage("You have reached the limit of Sub Transaction. Only 15 Sub Transaction Allowed.");
            //}

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
            //txtQtyIssue.Text = string.Empty;
            ddlMaterialStatus.SelectedIndex = -1;
            //txtGrade.Text = string.Empty;
            txtCartornNo.Text = string.Empty;
            txtDofMfd.Text = string.Empty;
            txtBarCode.Text = string.Empty;
            txtGrossWt.Text = string.Empty;
            txtTareWt.Text = string.Empty;
            isPallet.Checked = false;
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

            if (Session["dtTRN_SUB"] != null)
            {

                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

            }
            else
            {
                dtTRN_SUB = CreateSUBTRNTable();
            }
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                //ddlW_Side.SelectedValue = dv[0]["W_SIDE"].ToString();
                txtQty.Text = dv[0]["TRN_QTY"].ToString();
                //txtQtyIssue.Text = dv[0]["QTY_ISSUE"].ToString();
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                //txtLOTNo.Text = dv[0]["LOT_NO"].ToString();
                txtCartornNo.Text = dv[0]["CARTON_NO"].ToString();
                txtDyedBatchNo.Text = dv[0]["DYED_BATCH"].ToString();
                txtGrossWt.Text = dv[0]["GROSS_WT"].ToString();
                txtTareWt.Text = dv[0]["TARE_WT"].ToString();
                txtBarCode.Text = dv[0]["BARCODE_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                ddlUOM.SelectedValue = dv[0]["UOM_OF_UNIT"].ToString();
                isPallet.Checked = (dv[0]["IS_PALLET"].ToString() == "1") ? true : false;
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

            if (Session["dtTRN_SUB"] != null)
            {

                dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];

            }

            if (grdsub_trn.Rows.Count == 1)
            {
                dtTRN_SUB.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_SUB.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_SUB.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            Session["dtTRN_SUB"] = dtTRN_SUB;
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
                Label lbtcartonno = (Label)grdRow.FindControl("lbtcartonno");
                Button lnkDelete = (Button)grdRow.FindControl("lnkBOMDelete");
                int iUNIQUE_ID = int.Parse(lnkDelete.CommandArgument.Trim());
                if (lbtcartonno.Text == LotNo && UNIQUE_ID != iUNIQUE_ID)
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            double totalQty = 0;
            double NoOfUNIT = 0;
            double TotalWeightofUnit = 0;
            double WeightofUnit = 0;
            double totalGrossWt = 0;
            double totalTareWt = 0;
            int no_of_pallet = 0;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                totalQty += double.Parse((lblQTY.Text).Trim());

                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                NoOfUNIT += double.Parse((lblNoUnit.Text).Trim());

                Label lblWeightofUnit = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
                TotalWeightofUnit += double.Parse((lblWeightofUnit.Text).Trim());

                Label lblGWt = grdsub_trn.Rows[i].FindControl("lblGrossWt") as Label;
                totalGrossWt = totalGrossWt + double.Parse((lblGWt.Text).Trim());

                Label lblTWt = grdsub_trn.Rows[i].FindControl("lblTareWt") as Label;
                totalTareWt = totalTareWt + double.Parse((lblTWt.Text).Trim());

                Label lblPallet = grdsub_trn.Rows[i].FindControl("lblPallet") as Label;
                if (lblPallet.Text == "1")
                {
                    no_of_pallet = no_of_pallet + 1;
                }
            }
            WeightofUnit = totalQty / NoOfUNIT;



            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY('" + totalQty + "','" + NoOfUNIT + "','" + WeightofUnit + "','" + totalGrossWt + "','" + totalTareWt + "','" + grdsub_trn.Rows.Count + "','" + no_of_pallet + "','" + txtboxClientid + "','" + txtNoOfUnit + "','" + txtWeightOfUnit + "','" + GROSS_WT + "','" + TARE_WT + "','" + DYED_BATCH + "','" + CARTONS + "','" + txtNoOfPallet + "')", true);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

    protected bool CheckQTYtotal()
    {
        try
        {

            double currentpage = 0;
            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
            {
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
            }
            if (grdsub_trn.Rows.Count > 0)
            {
                for (int i = 0; i < grdsub_trn.Rows.Count; i++)
                {

                    Label txtSubTrnUNIQUE_ID = (Label)grdsub_trn.Rows[i].FindControl("txtSubTrnUNIQUE_ID");
                    int iUNIQUEID = int.Parse(txtSubTrnUNIQUE_ID.Text);
                    if (UNIQUE_ID != iUNIQUEID)
                    {
                        Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                        currentpage += double.Parse(lblQTY.Text);
                    }
                }
                double alltotal = currentpage + double.Parse(txtQty.Text);
                if (alltotal > lblMaxQTY)
                {
                    //return false;
                    return true;

                }
                else
                {
                    return true;
                }

            }
            else if (double.Parse(txtQty.Text) > lblMaxQTY)
            {
                return true;
                //return false;
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

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtNoofUnit.Text))
        {
            txtWeightofUnit.Text = (double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)).ToString();
        }
        txtGrossWt.Focus();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtNoofUnit.Text))
        {
            txtWeightofUnit.Text = (double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)).ToString();
        }
        isPallet.Focus();

    }

    protected void CalculateAllData()
    {
        if (grdsub_trn.Rows.Count > 0)
        {
            double totalCops = 0;
            double totalGrossWt = 0;
            double totalTareWt = 0;
            double totalNetWt = 0;
            int no_of_pallet = 0;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                double Cops = 0;
                double GrossWt = 0;
                double TareWt = 0;
                double NetWt = 0;


                Label lblNUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                Label lblGWt = grdsub_trn.Rows[i].FindControl("lblGrossWt") as Label;
                Label lblTWt = grdsub_trn.Rows[i].FindControl("lblTareWt") as Label;
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                Label lblPallet = grdsub_trn.Rows[i].FindControl("lblPallet") as Label;

                double.TryParse(lblNUnit.Text, out Cops);
                double.TryParse(lblGWt.Text, out GrossWt);
                double.TryParse(lblTWt.Text, out TareWt);
                double.TryParse(lblQTY.Text, out NetWt);

                totalNetWt = totalNetWt + NetWt;
                totalCops = totalCops + Cops;
                totalGrossWt = totalGrossWt + GrossWt;
                totalTareWt = totalTareWt + TareWt;
                if (lblPallet.Text == "1")
                {
                    no_of_pallet = no_of_pallet + 1;
                }


            }
            Label lbtcartonno = grdsub_trn.Rows[grdsub_trn.Rows.Count - 1].FindControl("lbtcartonno") as Label;
            Label lblDyedBatch = grdsub_trn.Rows[grdsub_trn.Rows.Count - 1].FindControl("lblDyedBatch") as Label;
            ((Label)grdsub_trn.FooterRow.FindControl("flblQTY")).Text = totalNetWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblGrossWt")).Text = totalGrossWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblTareWt")).Text = totalTareWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblNoUnit")).Text = totalCops.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flbtcartonno")).Text = grdsub_trn.Rows.Count.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblPallet")).Text = no_of_pallet.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flbtdyedbatch")).Text = grdsub_trn.Rows.Count.ToString();
            int cartonNO = 0;
            int.TryParse(lbtcartonno.Text, out cartonNO);
            int Dyedbatchno = 0;
            int.TryParse(lblDyedBatch.Text, out Dyedbatchno);
            txtCartornNo.Text = (cartonNO + 1).ToString();
            txtDyedBatchNo.Text = (Dyedbatchno ).ToString();
            txtBarCode.Text = "SA" + txtCartornNo.Text;

        }
    }


    protected void txtGrossWt_TextChanged(object sender, EventArgs e)
    {
        getNetWt();
        txtTareWt.Focus();

    }
    protected void txtTareWt_TextChanged(object sender, EventArgs e)
    {
        getNetWt();
        isPallet.Focus();


    }

    protected void getNetWt()
    {
        if (!string.IsNullOrEmpty(txtGrossWt.Text) && !string.IsNullOrEmpty(txtTareWt.Text))
        {
            txtQty.Text = (double.Parse(txtGrossWt.Text) - double.Parse(txtTareWt.Text)).ToString();
            if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtNoofUnit.Text))
            {
                txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)), 3).ToString();
            }
        }

    }

    protected void txtCartornNo_TextChanged(object sender, EventArgs e)
    {
        txtBarCode.Text = "SA" + txtCartornNo.Text;
        txtNoofUnit.Focus();
    }
}
