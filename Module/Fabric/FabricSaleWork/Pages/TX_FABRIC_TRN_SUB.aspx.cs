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
public partial class Module_Fabric_FabricSaleWork_Pages_TX_FABRIC_TRN_SUB : System.Web.UI.Page
{
    private static DataTable dtTRN_SUB = null;
    private static string PI_TYPE = string.Empty;
    private static string FABR_CODE = string.Empty;

    private static double lblMaxQTY = 0;
    private static int PO_NUMB = 0;
    private static string PO_TYPE = string.Empty;
    private static string PO_COMP_CODE = string.Empty;
    private static string PO_BRANCH = string.Empty;
    private static string txtboxClientid = string.Empty;

    private static string txtNoOfUnit = string.Empty;
    private static string txtUOm = string.Empty;
    private static string txtWeightOfUnit = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                BindIntial();
                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
                if (Request.QueryString["FABR_CODE"] != null)
                {
                    FABR_CODE = Request.QueryString["FABR_CODE"].Trim();
                    lblYarnCode.Text = FABR_CODE;
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

                if (Request.QueryString["lblMaxQTY"] != null)
                {
                    lblMaxQTY = double.Parse(Request.QueryString["lblMaxQTY"].Trim());
                    lblRemaining.Text = lblMaxQTY.ToString();
                    txtQty.Text = lblMaxQTY.ToString();
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
                if (Session["dtTRN_SUB"] != null)
                {
                    if (dtTRN_SUB == null)
                        CreateSUBTRNTable();
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

            txtGrade.Text = "NA";
            txtLotNo.Text = "NA";
            txtNoofUnit.Text = "1";
            txtWeightofUnit.Text = "1";
        }
        catch
        {

        }

    }

    public void clearInitial()
    {
        txtGrade.Text = string.Empty;
        txtLotNo.Text = string.Empty;
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
                //ddlUOM.Items.Insert(0, new ListItem("------Select------", "0"));
            }

        }
        catch
        {
            throw;
        }


    }

    private void CreateSUBTRNTable()
    {
        try
        {
            dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("FABR_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB.Columns.Add("GRADE", typeof(string));
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
            if (dtTRN_SUB == null)
            {
                CreateSUBTRNTable();
            }
            var dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "FABR_CODE='" + FABR_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "' AND PO_NUMB='" + PO_NUMB + "'";
            if (dv.Count > 0)
            {
                clearInitial();
                txtQty.Text = string.Empty;
                grdsub_trn.DataSource = dv;
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
            txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
            if (dtTRN_SUB == null)
            {
                CreateSUBTRNTable();
            } 

            if (dtTRN_SUB.Rows.Count < 15)
            {
                if (CheckQTYtotal())
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
                            DataView dv = new DataView(dtTRN_SUB);
                            dv.RowFilter = "FABR_CODE='" + FABR_CODE + "' and SHADE_CODE='" + txtShadeCode.Text.Trim() + "'  and UNIQUE_ID=" + UNIQUE_ID;
                            if (dv.Count > 0)
                            {
                                double QTY = 0f;
                                double.TryParse(txtQty.Text.Trim(), out QTY);
                                dv[0]["TRN_QTY"] = QTY;
                                dv[0]["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                                dv[0]["PO_TYPE"] = PO_TYPE;
                                dv[0]["PO_COMP_CODE"] = PO_COMP_CODE;
                                dv[0]["PO_BRANCH"] = PO_BRANCH;
                                dv[0]["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                                dv[0]["GRADE"] = txtGrade.Text.Trim();
                                dv[0]["LOT_NO"] = txtLotNo.Text.Trim();
                                dv[0]["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                                dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                                dv[0]["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                                dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                                dv[0]["PI_NO"] = "NA";
                                if (txtShadeCode.Text == string.Empty || txtShadeCode.Text == "" || txtShadeCode.Text == null)
                                {
                                    dv[0]["SHADE_CODE"] = "NA";
                                }
                                else
                                {
                                    dv[0]["SHADE_CODE"] = txtShadeCode.Text.Trim();
                                }
                               
                                dtTRN_SUB.AcceptChanges();
                            }
                        }
                        else
                        {


                            DataRow dr = dtTRN_SUB.NewRow();
                            dr["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                            dr["FABR_CODE"] = FABR_CODE;

                            double QTY = 0f;
                            double.TryParse(txtQty.Text.Trim(), out QTY);

                            dr["TRN_QTY"] = QTY;
                            dr["PO_NUMB"] = Convert.ToInt32(PO_NUMB);
                            dr["PO_TYPE"] = PO_TYPE;
                            dr["PO_COMP_CODE"] = PO_COMP_CODE;
                            dr["PO_BRANCH"] = PO_BRANCH;
                            dr["MATERIAL_STATUS"] = ddlMaterialStatus.SelectedItem.ToString().Trim();
                            dr["GRADE"] = txtGrade.Text.Trim();
                            dr["LOT_NO"] = txtLotNo.Text.Trim();
                            dr["DATE_OF_MFG"] = txtDofMfd.Text.Trim();
                            dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                            dr["UOM_OF_UNIT"] = ddlUOM.SelectedItem.ToString();
                            dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                            dr["PI_NO"] = "NA";
                            if (txtShadeCode.Text.Trim() == string.Empty || txtShadeCode.Text.Trim() == "" || txtShadeCode.Text.Trim() == null)
                            {
                                dr["SHADE_CODE"] = "NA";
                            }
                            else
                            {
                                dr["SHADE_CODE"] = txtShadeCode.Text.Trim();

                            }
                            dtTRN_SUB.Rows.Add(dr);

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
                    Common.CommonFuction.ShowMessage("You Cannot Recieve Exceeds Quantity !! Limit is =: " + lblMaxQTY);
                }
            }

            else
            {
                Common.CommonFuction.ShowMessage("You have reached the limit of Sub Transaction. Only 15 Sub Transaction Allowed.");
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
            //txtQtyIssue.Text = string.Empty;
            ddlMaterialStatus.SelectedIndex = -1;
            txtGrade.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtDofMfd.Text = string.Empty;
            ViewState["UNIQUE_ID"] = null;
            txtNoofUnit.Text = string.Empty;
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
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {

                //ddlW_Side.SelectedValue = dv[0]["W_SIDE"].ToString();
                txtQty.Text = dv[0]["TRN_QTY"].ToString();
                //txtQtyIssue.Text = dv[0]["QTY_ISSUE"].ToString();
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
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

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            double totalQty = 0;
            double NoOfUNIT = 0;
            double TotalWeightofUnit = 0;
            double WeightofUnit = 0;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                totalQty += double.Parse(txtQTY.Text);

                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                NoOfUNIT += double.Parse(lblNoUnit.Text);

                Label lblWeightofUnit = grdsub_trn.Rows[i].FindControl("lblWeightofUnit") as Label;
                TotalWeightofUnit += double.Parse(lblWeightofUnit.Text);

            }
            WeightofUnit = totalQty / NoOfUNIT;
            
            Session["dtTRN_SUB"] = dtTRN_SUB;


            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY('" + totalQty + "','" + NoOfUNIT + "','" + ddlUOM.SelectedItem.ToString() + "','" + WeightofUnit + "','" + txtboxClientid + "','" + txtNoOfUnit + "','" + txtWeightOfUnit + "','" + txtUOm + "')", true);

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
                        Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                        currentpage += double.Parse(txtQTY.Text);
                    }
                }
                double alltotal = currentpage + double.Parse(txtQty.Text);
                if (alltotal > lblMaxQTY)
                {
                    return false;

                }
                else
                {
                    return true;
                }

            }
            else if (double.Parse(txtQty.Text) > lblMaxQTY)
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

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        txtWeightofUnit.Text = (double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)).ToString();
    }

    protected void txtQty_TextChanged(object sender, EventArgs e)
    {
        if (txtNoofUnit.Text != "")
        {
            txtWeightofUnit.Text = (double.Parse(txtQty.Text) / double.Parse(txtNoofUnit.Text)).ToString();
        }

    }
}
