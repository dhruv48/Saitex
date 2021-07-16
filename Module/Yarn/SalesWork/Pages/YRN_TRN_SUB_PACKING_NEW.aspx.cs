using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Globalization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;


public partial class Module_Yarn_SalesWork_Pages_YRN_TRN_SUB_PACKING_NEW : System.Web.UI.Page
{
    private DataTable dtTRN_SUB_PACKING = null;
    private string PI_TYPE = string.Empty;
    private string YARN_CODE = string.Empty;
    private double lblMaxQTY = 0;
    private int PO_NUMB = 999998;
    private string PO_TYPE = "PCK";
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
    private int MRN_NO = 0;
    private int PO_YEAR = 0;
    private string TRN_TYPE = string.Empty;
    //private string CWT = string.Empty;
    //private string PWT = string.Empty;
    private string FINISH_TYPE = string.Empty;
    private string BATCH_NO = string.Empty;
    private string PACK_DATE = DateTime.Now.Date.ToString("dd");
    private string PACK_MONTH = DateTime.Now.Date.ToString("MM");
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.YRN_IR_MST oYRN_IR_MST;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Request.QueryString["LOT_NO"] != null)
            {
                LOT_NO = Request.QueryString["LOT_NO"].Trim();
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
            if (Request.QueryString["txtShadeCode"] != null)
            {
                txtShadeCode.Text = Request.QueryString["txtShadeCode"].Trim();
            }
            if (Request.QueryString["txtShadeFamily"] != null)
            {
                txtShadeFamily.Text = Request.QueryString["txtShadeFamily"].Trim();
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
            if (Request.QueryString["PO_YEAR"] != null)
            {
                PO_YEAR = Convert.ToInt32(Request.QueryString["PO_YEAR"].Trim());
            }
            if (Request.QueryString["TRN_TYPE"] != null)
            {
                TRN_TYPE = Request.QueryString["TRN_TYPE"].Trim();
            }

            if (Request.QueryString["FINISH"] != null)
            {
                FINISH_TYPE = Request.QueryString["FINISH"].Trim();
            }
            if (Request.QueryString["BATCH_NO"] != null)
            {
                BATCH_NO = Request.QueryString["BATCH_NO"].Trim();
            }

            if (Request.QueryString["MRN_NO"] != null)
            {
                MRN_NO = int.Parse(Request.QueryString["MRN_NO"].ToString());
            }

            if (!IsPostBack)
            {
                BindIntial();
                // BindCortoonNo();
                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
                lblRemaining.Text = lblMaxQTY.ToString();
                txtQty.Text = lblMaxQTY.ToString();
                if (Request.QueryString["CWT"] != null)
                {
                    txtCWT.Text = Request.QueryString["CWT"].Trim();//CWT
                }
                if (Request.QueryString["PWT"] != null)
                {
                    txtPWT.Text = Request.QueryString["PWT"].Trim();//PWT
                }
                if (Session["dtTRN_SUB_PACKING"] != null)
                {
                    dtTRN_SUB_PACKING = (DataTable)Session["dtTRN_SUB_PACKING"];
                }
                else
                {
                    grdsub_trn.DataSource = null;
                    grdsub_trn.DataBind();
                    if (dtTRN_SUB_PACKING != null && dtTRN_SUB_PACKING.Rows.Count > 0)
                    {
                        dtTRN_SUB_PACKING.Clear();
                    }
                }
                bindUOM("UOM");
                BindBOMGrid();
                SelectCartonFromMRN();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }
    //************************* Cortoon No Auto Generate **********Arun Sharma *******************//
    private void BindCortoonNo()
    {
        try
        {

            string PREFIX = string.Empty;
            oYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
            oYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IR_MST.PACK_DATE = PACK_DATE;
            oYRN_IR_MST.PACK_MONTH = PACK_MONTH;
            oYRN_IR_MST.TRN_TYPE = TRN_TYPE;
            string CortoonNo = SaitexBL.Interface.Method.YRN_IR_MST.BindCortoonNo(oYRN_IR_MST);
            txtCartornNo.Text = CortoonNo;

        }
        catch
        {
            throw;
        }
    }
    //************************* Cortoon No Auto Generate **********Arun Sharma *******************//

    public void BindIntial()
    {

        try
        {

            //txtGrade.Text = "NA";
            txtCartornNo.Text = "NA";
            txtDyedBatchNo.Text = BATCH_NO.ToString();
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
        }
        catch (Exception ex)
        {
            throw ex;
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
                ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByValue("KGS"));
                //ddlUOM.SelectedValue = "KG";
                //ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    public void SelectCartonFromMRN()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectCartonFromMRN(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), MRN_NO);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlOldCartonNo.Items.Clear();
                ddlOldCartonNo.DataSource = dt;
                ddlOldCartonNo.DataTextField = "CARTON_NO";
                ddlOldCartonNo.DataValueField = "abc";
                ddlOldCartonNo.DataBind();
                ddlOldCartonNo.Items.Insert(0, "");

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


            dtTRN_SUB_PACKING = new DataTable();
            dtTRN_SUB_PACKING.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB_PACKING.Columns.Add("YARN_CODE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB_PACKING.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("GRADE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("LOT_NO", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("DYED_BATCH", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("DATE_OF_MFG", typeof(DateTime));
            dtTRN_SUB_PACKING.Columns.Add("PO_NUMB", typeof(int));
            dtTRN_SUB_PACKING.Columns.Add("PO_TYPE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("PO_COMP_CODE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("PO_BRANCH", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("PO_YEAR", typeof(int));
            dtTRN_SUB_PACKING.Columns.Add("NO_OF_UNIT", typeof(double));
            dtTRN_SUB_PACKING.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtTRN_SUB_PACKING.Columns.Add("PI_NO", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("SHADE_FAMILY", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("CARTON_NO", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("GROSS_WT", typeof(double));
            dtTRN_SUB_PACKING.Columns.Add("TARE_WT", typeof(double));
            dtTRN_SUB_PACKING.Columns.Add("BARCODE_NO", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("FINISH_TYPE", typeof(string));
            //dtTRN_SUB_PACKING.Columns.Add("OLD_CARTON_NO", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("OLD_TRN_NUMB", typeof(int));
            dtTRN_SUB_PACKING.Columns.Add("OLD_TRN_TYPE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("OLD_COMP_CODE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("OLD_BRANCH_CODE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("OLD_PO_TYPE", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("OLD_PO_NUMB", typeof(string));
            dtTRN_SUB_PACKING.Columns.Add("OLD_PO_YEAR", typeof(string));
            return dtTRN_SUB_PACKING;

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

            if (Session["dtTRN_SUB_PACKING"] != null)
            {

                dtTRN_SUB_PACKING = (DataTable)Session["dtTRN_SUB_PACKING"];

            }
            else
            {
                dtTRN_SUB_PACKING = CreateSUBTRNTable();
            }


            DataView dv = new DataView(dtTRN_SUB_PACKING);
            dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "' and SHADE_FAMILY='" + txtShadeFamily.Text.Trim() + "' AND PO_NUMB='" + PO_NUMB + "'    AND LOT_NO='" + LOT_NO + "'  AND GRADE='" + GRADE + "' AND FINISH_TYPE='" + FINISH_TYPE + "'";//AND PI_NO='" + PI_NO + "'
            if (dv.Count > 0)
            {
                clearInitial();
                txtQty.Text = string.Empty;
                dv.Sort = "CARTON_NO DESC";
                double total = 0.0;
                for (int i = 0; i < dv.Count; i++)
                {
                    total += double.Parse(dv[i]["TRN_QTY"].ToString());

                }
                if ((lblMaxQTY - total) >= 0)
                {
                    grdsub_trn.DataSource = dv;
                    grdsub_trn.DataBind();
                    CalculateAllData();
                }
                else
                {

                    Common.CommonFuction.ShowMessage(" Remaining Quantity is 0");
                }

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
    private bool CheckPackingCartonExistence(string COMP_CODE, string BRANCH_CODE, int YEAR, string TRN_TYPE, string CARTON_NO)
    {
        bool flag = false;
        try
        {
            if (ViewState["UNIQUE_ID"] != null)
            {
                flag = true;
            }
            else
            {
                flag = SaitexBL.Interface.Method.YRN_IR_MST.CheckPackingCartonExistence(COMP_CODE, BRANCH_CODE, YEAR, TRN_TYPE, CARTON_NO);
            }
            return flag;
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
            if (lblOldTrn.Text != string.Empty)
            {
                bool result = CheckPackingCartonExistence(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, TRN_TYPE, txtCartornNo.Text.Trim());
                if (result)
                {
                    if (Session["dtTRN_SUB_PACKING"] != null)
                    {
                        dtTRN_SUB_PACKING = (DataTable)Session["dtTRN_SUB_PACKING"];
                    }
                    else
                    {
                        dtTRN_SUB_PACKING = CreateSUBTRNTable();
                    }
                    txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();

                    if (dtTRN_SUB_PACKING.Rows.Count < 1000)
                    {
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
                                    DataView dv = new DataView(dtTRN_SUB_PACKING);
                                    dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "' and SHADE_FAMILY='" + txtShadeFamily.Text.Trim() + "' and UNIQUE_ID=" + UNIQUE_ID;
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
                                        dv[0]["PO_YEAR"] = PO_YEAR;
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
                                        dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                        dv[0]["GROSS_WT"] = GROSSWT;
                                        dv[0]["TARE_WT"] = TEARWT;
                                        dv[0]["BARCODE_NO"] = txtBarCode.Text.Trim();
                                        dv[0]["FINISH_TYPE"] = FINISH_TYPE;
                                        //dv[0]["OLD_CARTON_NO"] = ddlOldCartonNo.SelectedItem.Text.ToString();
                                        dv[0]["OLD_TRN_NUMB"] = int.Parse(lblOldTrn.Text.ToString());
                                        dv[0]["OLD_TRN_TYPE"] = lblOldTrnType.Text.ToString();
                                        dv[0]["OLD_COMP_CODE"] = lblOldPoCompCode.Text.ToString();
                                        dv[0]["OLD_BRANCH_CODE"] = lblOldPoBranchCode.Text.ToString();
                                        dv[0]["OLD_PO_TYPE"] = lblPOType.Text.ToString();
                                        dv[0]["OLD_PO_NUMB"] = lblPoNumb.Text.ToString();
                                        dv[0]["OLD_PO_YEAR"] = lblPoYear.Text.ToString();
                                        dtTRN_SUB_PACKING.AcceptChanges();
                                    }
                                }
                                else
                                {


                                    DataRow dr = dtTRN_SUB_PACKING.NewRow();
                                    dr["UNIQUE_ID"] = dtTRN_SUB_PACKING.Rows.Count + 1;
                                    dr["YARN_CODE"] = YARN_CODE;

                                    double QTY = 0f;
                                    double.TryParse(txtQty.Text.Trim(), out QTY);
                                    double TEARWT = 0f;
                                    double.TryParse(txtTareWt.Text.Trim(), out TEARWT);
                                    double GROSSWT = 0f;
                                    double.TryParse(txtGrossWt.Text.Trim(), out GROSSWT);
                                    dr["TRN_QTY"] = QTY;
                                    dr["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                                    dr["PO_YEAR"] = PO_YEAR;
                                    dr["PO_TYPE"] = PO_TYPE;
                                    dr["PO_COMP_CODE"] = PO_COMP_CODE;
                                    dr["PO_BRANCH"] = PO_BRANCH;
                                    dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                                    dr["GRADE"] = GRADE;
                                    dr["LOT_NO"] = LOT_NO;
                                    dr["CARTON_NO"] = txtCartornNo.Text.Trim();
                                    dr["DYED_BATCH"] = txtDyedBatchNo.Text.Trim();
                                    dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                                    dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                                    dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                    dr["WEIGHT_OF_UNIT"] = Math.Round(double.Parse(txtWeightofUnit.Text.Trim()), 3);
                                    dr["PI_NO"] = PI_NO;
                                    dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                                    dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                                    dr["GROSS_WT"] = GROSSWT;
                                    dr["TARE_WT"] = TEARWT;
                                    dr["BARCODE_NO"] = txtBarCode.Text.Trim();
                                    dr["FINISH_TYPE"] = FINISH_TYPE;

                                    //dr["OLD_CARTON_NO"] = ddlOldCartonNo.SelectedItem.Text.ToString();
                                    dr["OLD_TRN_NUMB"] = int.Parse(lblOldTrn.Text.ToString());
                                    dr["OLD_TRN_TYPE"] = lblOldTrnType.Text.ToString();
                                    dr["OLD_COMP_CODE"] = lblOldPoCompCode.Text.ToString();
                                    dr["OLD_BRANCH_CODE"] = lblOldPoBranchCode.Text.ToString();
                                    dr["OLD_PO_TYPE"] = lblPOType.Text.ToString();
                                    dr["OLD_PO_NUMB"] = lblPoNumb.Text.ToString();
                                    dr["OLD_PO_YEAR"] = lblPoYear.Text.ToString();
                                    dtTRN_SUB_PACKING.Rows.Add(dr);

                                }
                                Session["dtTRN_SUB_PACKING"] = dtTRN_SUB_PACKING;
                                RefresBOMRow();

                                txtCWT.Focus();
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
                    }

                    else
                    {
                        Common.CommonFuction.ShowMessage("You have reached the limit of Sub Transaction. Only 15 Sub Transaction Allowed.");
                    }
                }
                else
                {
                    CommonFuction.ShowMessage("Carton No " + txtCartornNo.Text + " already exist.!!");
                }
            }
            else 
            {

                Common.CommonFuction.ShowMessage("Please select the Old Carton !");
            
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

            ddlMaterialStatus.SelectedIndex = -1;
            txtDofMfd.Text = string.Empty;
            txtBarCode.Text = string.Empty;
            txtGrossWt.Text = string.Empty;
            txtTareWt.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
            if (Request.QueryString["CWT"] != null)
            {
                txtCWT.Text = Request.QueryString["CWT"].Trim();//CWT
            }
            if (Request.QueryString["PWT"] != null)
            {
                txtPWT.Text = Request.QueryString["PWT"].Trim();//PWT
            }

        }
        catch
        {
            throw;
        }
    }

    protected void BtnBOMCancel_Click(object sender, EventArgs e)
    {
        RefresBOMRow();
        txtNoofUnit.Focus();
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

            if (Session["dtTRN_SUB_PACKING"] != null)
            {

                dtTRN_SUB_PACKING = (DataTable)Session["dtTRN_SUB_PACKING"];

            }
            else
            {
                dtTRN_SUB_PACKING = CreateSUBTRNTable();
            }
            DataView dv = new DataView(dtTRN_SUB_PACKING);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                txtQty.Text = dv[0]["TRN_QTY"].ToString();
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtCartornNo.Text = dv[0]["CARTON_NO"].ToString();
                txtDyedBatchNo.Text = dv[0]["DYED_BATCH"].ToString();
                txtGrossWt.Text = dv[0]["GROSS_WT"].ToString();
                txtTareWt.Text = dv[0]["TARE_WT"].ToString();
                txtBarCode.Text = dv[0]["BARCODE_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                ddlUOM.SelectedValue = dv[0]["UOM_OF_UNIT"].ToString();
                //ddlOldCartonNo.SelectedIndex = ddlOldCartonNo.Items.IndexOf(ddlOldCartonNo.Items.FindByText(dv[0]["OLD_CARTON_NO"].ToString()));


                lblOldTrn.Text= dv[0]["OLD_TRN_NUMB"].ToString();
                lblOldTrnType.Text = dv[0]["OLD_TRN_TYPE"].ToString();
                    lblOldPoCompCode.Text=dv[0]["OLD_COMP_CODE"].ToString();
                        lblOldPoBranchCode.Text=dv[0]["OLD_BRANCH_CODE"].ToString();
                        lblPOType.Text=dv[0]["OLD_PO_TYPE"].ToString();
                            lblPoNumb.Text=dv[0]["OLD_PO_NUMB"].ToString();
                            lblPoYear.Text = dv[0]["OLD_PO_YEAR"].ToString();
                double _TAREWT = 0;
                double.TryParse(txtTareWt.Text, out _TAREWT);
                double _COPS = 0;
                double.TryParse(txtNoofUnit.Text, out _COPS);
                double _PWT = 0;
                double.TryParse(txtPWT.Text, out _PWT);
                txtCWT.Text = (_TAREWT - (_COPS * _PWT)).ToString();
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

            if (Session["dtTRN_SUB_PACKING"] != null)
            {

                dtTRN_SUB_PACKING = (DataTable)Session["dtTRN_SUB_PACKING"];

            }

            if (grdsub_trn.Rows.Count == 1)
            {
                dtTRN_SUB_PACKING.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtTRN_SUB_PACKING.Rows)
                {
                    int iUNIQUE_ID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUE_ID == UNIQUE_ID)
                    {
                        dtTRN_SUB_PACKING.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtTRN_SUB_PACKING.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            Session["dtTRN_SUB_PACKING"] = dtTRN_SUB_PACKING;
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
        catch (Exception ex)
        {
            throw ex;
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

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                totalQty += double.Parse(lblQTY.Text);

                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                NoOfUNIT += double.Parse(lblNoUnit.Text);
                Label lblWeightofUnit = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
                TotalWeightofUnit += double.Parse(lblWeightofUnit.Text);
                Label lblGWt = grdsub_trn.Rows[i].FindControl("lblGrossWt") as Label;
                totalGrossWt = totalGrossWt + double.Parse(lblGWt.Text);
                Label lblTWt = grdsub_trn.Rows[i].FindControl("lblTareWt") as Label;
                totalTareWt = totalTareWt + double.Parse(lblTWt.Text);
            }
            WeightofUnit = Math.Round(totalQty / NoOfUNIT, 3);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY('" + totalQty + "','" + NoOfUNIT + "','" + WeightofUnit + "','" + totalGrossWt + "','" + totalTareWt + "','" + grdsub_trn.Rows.Count + "','" + txtboxClientid + "','" + txtNoOfUnit + "','" + txtWeightOfUnit + "','" + GROSS_WT + "','" + TARE_WT + "','" + CARTONS + "')", true);
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
            txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)), 3).ToString();
        }
        checkTareWeight();
        getNetWt();
        txtGrossWt.Focus();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtQty.Text) && !string.IsNullOrEmpty(txtNoofUnit.Text))
        {
            txtWeightofUnit.Text = Math.Round((double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)), 3).ToString();
        }
        BtnBOMSave.Focus();

    }

    protected void CalculateAllData()
    {
        if (grdsub_trn.Rows.Count > 0)
        {
            double totalCops = 0;
            double totalGrossWt = 0;
            double totalTareWt = 0;
            double totalNetWt = 0;
            Int64 _cartonNo = 0;
            string _carton_prefix = string.Empty;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                double Cops = 0;
                double GrossWt = 0;
                double TareWt = 0;
                double NetWt = 0;
                Int64 cartonNO = 0;


                Label lblNUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                Label lblGWt = grdsub_trn.Rows[i].FindControl("lblGrossWt") as Label;
                Label lblTWt = grdsub_trn.Rows[i].FindControl("lblTareWt") as Label;
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                Label lbtcartonno = grdsub_trn.Rows[i].FindControl("lbtcartonno") as Label;

                double.TryParse(lblNUnit.Text, out Cops);
                double.TryParse(lblGWt.Text, out GrossWt);
                double.TryParse(lblTWt.Text, out TareWt);
                double.TryParse(lblQTY.Text, out NetWt);
                if (lbtcartonno.Text.Length > 4)
                {
                    Int64.TryParse(lbtcartonno.Text.Substring(lbtcartonno.Text.Length - 4, 4), out cartonNO);
                    _carton_prefix = lbtcartonno.Text.Substring(0, lbtcartonno.Text.Length - 4);
                }
                else
                {
                    Int64.TryParse(lbtcartonno.Text, out cartonNO);
                }
                totalNetWt = totalNetWt + NetWt;
                totalCops = totalCops + Cops;
                totalGrossWt = totalGrossWt + GrossWt;
                totalTareWt = totalTareWt + TareWt;

                if (_cartonNo < cartonNO)
                {
                    _cartonNo = cartonNO;
                }
            }
            Label lblDyedBatch = grdsub_trn.Rows[grdsub_trn.Rows.Count - 1].FindControl("lblDyedBatch") as Label;
            ((Label)grdsub_trn.FooterRow.FindControl("flblQTY")).Text = totalNetWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblGrossWt")).Text = totalGrossWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblTareWt")).Text = totalTareWt.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flblNoUnit")).Text = totalCops.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flbtcartonno")).Text = grdsub_trn.Rows.Count.ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("flbtDyedBatch")).Text = grdsub_trn.Rows.Count.ToString();
            int Dyedbatchno = 0;
            int.TryParse(lblDyedBatch.Text, out Dyedbatchno);
            txtDyedBatchNo.Text = (Dyedbatchno).ToString();

            if (_cartonNo < 10)
            {
                txtCartornNo.Text = _carton_prefix + "000" + (_cartonNo + 1).ToString();
            }
            else if (_cartonNo > 10 && _cartonNo < 100)
            {
                txtCartornNo.Text = _carton_prefix + "00" + (_cartonNo + 1).ToString();
            }
            else if (_cartonNo > 100 && _cartonNo < 1000)
            {
                txtCartornNo.Text = _carton_prefix + "0" + (_cartonNo + 1).ToString();
            }
            else
            {
                txtCartornNo.Text = _carton_prefix + (_cartonNo + 1).ToString();
            }
            txtBarCode.Text = "SA" + txtCartornNo.Text;

            if ((lblMaxQTY - totalNetWt) >= 0)
            { lblRemaining.Text = (lblMaxQTY - totalNetWt).ToString(); }
            else
            {
                Common.CommonFuction.ShowMessage("Remaining Quantity is 0 ");
            }

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
        BtnBOMSave.Focus();

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
        txtGrade.Focus();
        //txtNoofUnit.Focus();
        //txtCWT.Focus();
    }


    protected void chkCartonWt_CheckedChanged(object sender, EventArgs e)
    {
        checkTareWeight();
    }

    protected void checkTareWeight()
    {
        double NoOfTube = 0;
        double CartonWeight = 0;
        double PaperTubeWeight = 0;
        if (chkCartonWt.Checked)
        {
            double.TryParse(txtCWT.Text, out CartonWeight);
        }
        if (chkPaperTubeWt.Checked)
        {
            double.TryParse(txtPWT.Text, out PaperTubeWeight);
        }
        double.TryParse(txtNoofUnit.Text, out NoOfTube);
        txtTareWt.Text = ((NoOfTube * PaperTubeWeight) + CartonWeight).ToString();
    }
    protected void chkPaperTubeWt_CheckedChanged(object sender, EventArgs e)
    {
        checkTareWeight();
    }

    protected void txtCWT_TextChanged(object sender, EventArgs e)
    {
        checkTareWeight();
        txtNoofUnit.Focus();

    }
    protected void ddlOldCartonNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = ddlOldCartonNo.SelectedValue.ToString();
        string[] str1 = str.Split('@');
        lblOldTrn.Text = str1[0].ToString();
        lblOldTrnType.Text = str1[1].ToString();
        lblOldPoCompCode.Text = str1[2].ToString();
        lblOldPoBranchCode.Text = str1[3].ToString();
        lblPOType.Text = str1[4].ToString();
        lblPoNumb.Text = str1[5].ToString();
        lblPoYear.Text = str1[6].ToString();
    }
}