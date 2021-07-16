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

public partial class Module_SewingThread_Page_SW_TRN_SUB_MISC : System.Web.UI.Page
{
    private static DataTable dtTRN_SUB = null;
    private static string YARN_CODE = string.Empty;
    private static string SHADE_CODE = string.Empty;

    private static int PO_NUMB = 999996;
    private static string PO_TYPE = "MII";
    private static string PO_COMP_CODE = "C99996";
    private static string PO_BRANCH = "B99996";
    private static string txtboxClientid = string.Empty;

    private static string txtNoOfUnit = string.Empty;

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                txtboxClientid = string.Empty;
                txtNoOfUnit = string.Empty;
                SHADE_CODE = string.Empty;
                YARN_CODE = string.Empty;

                txtDofMfd.Text = System.DateTime.Now.Date.ToShortDateString();
                if (Request.QueryString["YARN_CODE"] != null)
                {
                    YARN_CODE = Request.QueryString["YARN_CODE"].Trim();
                    lblArticleCode.Text = YARN_CODE;


                }
                if (Request.QueryString["SHADE_CODE"] != null)
                {
                    SHADE_CODE = Request.QueryString["SHADE_CODE"].Trim();

                    lblShadeCode.Text = SHADE_CODE;
                }

                if (Request.QueryString["txtQTY"] != null)
                {
                    txtboxClientid = Request.QueryString["txtQTY"].Trim();
                }
                if (Request.QueryString["txtNoOfUnit"] != null)
                {
                    txtNoOfUnit = Request.QueryString["txtNoOfUnit"].Trim();
                }
                if (Request.QueryString["BASE_UOM"] != null)
                {
                    txtBaseUOM.Text = Request.QueryString["BASE_UOM"].Trim();
                }
                if (Request.QueryString["WEIGHT_OF_UNIT"] != null)
                {
                    txtWeightofUnit.Text = Request.QueryString["WEIGHT_OF_UNIT"].Trim();
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
                BindBOMGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nsee error log for detail."));
        }
    }

    private void CreateSUBTRNTable()
    {
        try
        {
            dtTRN_SUB = new DataTable();
            dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            dtTRN_SUB.Columns.Add("YARN_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("SHADE_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("TRN_QTY", typeof(double));
            dtTRN_SUB.Columns.Add("MATERIAL_STATUS", typeof(string));
            dtTRN_SUB.Columns.Add("GRADE", typeof(string));
            dtTRN_SUB.Columns.Add("LOT_NO", typeof(string));
            dtTRN_SUB.Columns.Add("DATE_OF_MFG", typeof(string));
            dtTRN_SUB.Columns.Add("PO_NUMB", typeof(int));
            dtTRN_SUB.Columns.Add("PO_TYPE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_COMP_CODE", typeof(string));
            dtTRN_SUB.Columns.Add("PO_BRANCH", typeof(string));
            dtTRN_SUB.Columns.Add("NO_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("UOM_OF_UNIT", typeof(string));
            dtTRN_SUB.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
            dtTRN_SUB.Columns.Add("PI_NO", typeof(string));

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

            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' AND SHADE_CODE='" + SHADE_CODE + "'";
            if (dv.Count > 0)
            {
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


            int UNIQUE_ID = 0;
            if (ViewState["UNIQUE_ID"] != null)
            {
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
            }
            bool bb = SearchInBOMgrid(txtLotNo.Text, UNIQUE_ID);
            if (!bb)
            {
                DateTime DATE_OF_MFG = System.DateTime.Now.Date;
                if (UNIQUE_ID > 0)
                {

                    DataView dv = new DataView(dtTRN_SUB);
                    dv.RowFilter = "YARN_CODE='" + YARN_CODE + "' and SHADE_CODE='" + SHADE_CODE + "' and UNIQUE_ID=" + UNIQUE_ID;
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
                        DATE_OF_MFG = DateTime.Parse(txtDofMfd.Text.Trim());
                        dv[0]["DATE_OF_MFG"] = DATE_OF_MFG.ToShortDateString();
                        dv[0]["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                        dv[0]["UOM_OF_UNIT"] = txtBaseUOM.Text.ToString();
                        dv[0]["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                        dv[0]["PI_NO"] = "NA";
                        dtTRN_SUB.AcceptChanges();
                    }
                }
                else
                {


                    DataRow dr = dtTRN_SUB.NewRow();
                    dr["UNIQUE_ID"] = dtTRN_SUB.Rows.Count + 1;
                    dr["YARN_CODE"] = YARN_CODE;
                    dr["SHADE_CODE"] = SHADE_CODE;
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
                    DATE_OF_MFG = DateTime.Parse(txtDofMfd.Text.Trim());
                    dr["DATE_OF_MFG"] = DATE_OF_MFG.ToShortDateString();
                    dr["NO_OF_UNIT"] = double.Parse(txtNoofUnit.Text.Trim());
                    dr["UOM_OF_UNIT"] = txtBaseUOM.Text.ToString();
                    dr["WEIGHT_OF_UNIT"] = double.Parse(txtWeightofUnit.Text.Trim());
                    dr["PI_NO"] = "NA";
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
            txtNoofUnit.Text = string.Empty;
            ddlMaterialStatus.SelectedIndex = -1;
            txtGrade.Text = string.Empty;
            txtLotNo.Text = string.Empty;
            txtDofMfd.Text = string.Empty;
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
            DataView dv = new DataView(dtTRN_SUB);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                txtQty.Text = dv[0]["TRN_QTY"].ToString();
                ddlMaterialStatus.SelectedValue = dv[0]["MATERIAL_STATUS"].ToString();
                txtGrade.Text = dv[0]["GRADE"].ToString();
                txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                txtDofMfd.Text = dv[0]["DATE_OF_MFG"].ToString();
                txtNoofUnit.Text = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightofUnit.Text = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtBaseUOM.Text = dv[0]["UOM_OF_UNIT"].ToString();
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
            double WeightofUnit = 0;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {
                Label txtQTY = grdsub_trn.Rows[i].FindControl("txtQTY") as Label;
                totalQty += double.Parse(txtQTY.Text);

                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                NoOfUNIT += double.Parse(lblNoUnit.Text);

            }
            double.TryParse(txtWeightofUnit.Text, out WeightofUnit);

            totalQty = WeightofUnit * NoOfUNIT;
            Session["dtTRN_SUB"] = dtTRN_SUB;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:BindYRNQTY('" + totalQty + "','" + NoOfUNIT + "','" + txtBaseUOM.Text.ToString() + "','" + WeightofUnit + "','" + txtboxClientid + "','" + txtNoOfUnit + "','" + txtWeightofUnit.Text + "','" + txtBaseUOM.Text + "')", true);

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in submitting delivery data.\r\nsee error log for detail."));
        }
    }

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        txtQty.Text = (double.Parse(txtWeightofUnit.Text) * double.Parse(txtNoofUnit.Text)).ToString();
    }

}
