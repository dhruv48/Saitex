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

using errorLog;
using Common;
using DBLibrary;
using System.IO;

public partial class Module_OrderDevelopment_Fiber_Lap_Dip_Pages_Fiber_DyePopUp : System.Web.UI.Page
{
 
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string TextBoxId = string.Empty;
    private static string TextBoxId1 = string.Empty;
    private DataTable dtDye = null;
    private static string LRNo = string.Empty;
    private static string Option = string.Empty;
    private static string GREY_LOT_NO = string.Empty;
    private static string CAT_CODE = "DYES";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //cmbItemCode.AutoPostBack = true;
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
                if (Request.QueryString["LRNo"] != null && Request.QueryString["LRNo"].ToString() != "" && Request.QueryString["Option"] != null && Request.QueryString["Option"].ToString() != "" && Request.QueryString["TextBoxId"] != null && Request.QueryString["TextBoxId"].ToString() != "")
                {
                    LRNo = Request.QueryString["LRNo"].ToString();
                    Option = Request.QueryString["Option"].ToString();
                    GREY_LOT_NO = Request.QueryString["GREY_LOT_NO"].ToString();
                    TextBoxId = Request.QueryString["TextBoxId"].ToString();
                    TextBoxId1 = Request.QueryString["TextBoxId1"].ToString();
                    txtLRNo.Text = LRNo;
                    txtOption.Text = Option;
                    txtGeryLot.Text = GREY_LOT_NO;
                    BindGridWithLRNoAndOption(LRNo, Option, GREY_LOT_NO);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            txtLRNo.Text = string.Empty;
            txtOption.Text = string.Empty;
            txtGeryLot.Text = string.Empty;
            ddlItemCode.SelectedIndex = -1;
            cmbItemCode.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtDose.Text = string.Empty;
            txtCost.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    private void BindGridWithLRNoAndOption(string LRNo, string Option, string GREY_LOT_NO)
    {
        try
        {
            double dblDose = 0, dblCost = 0, dblTempDose = 0, dblTempCost = 0;
            //double dblTot = 0, dblTemp = 0;

            if (Session["dtDye"] != null)
            {
                dtDye = (DataTable)Session["dtDye"];
                if (dtDye != null && dtDye.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtDye);
                    dv.RowFilter = "LAB_DIP_NO = '" + LRNo + "' AND LR_OPTION = '" + Option + "'";
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            dblDose = Convert.ToDouble(dv[iLoop]["DOSE"].ToString());
                            dblCost = Convert.ToDouble(dv[iLoop]["RECIPE_COST"].ToString());
                            dblTempDose = dblTempDose + dblDose;
                            dblTempCost = dblTempCost + dblCost;

                            grdDyeName.DataSource = dv;
                            grdDyeName.DataBind();

                            if (grdDyeName.Rows.Count > 0)
                            {
                                Label txtTotalAmt = (Label)grdDyeName.FooterRow.FindControl("txtTotalAmt");
                                Label lblCostFooter = (Label)grdDyeName.FooterRow.FindControl("lblCostFooter");
                                txtTotalAmt.Text = dblTempDose.ToString("00.000000");
                                lblCostFooter.Text = dblTempCost.ToString("00.000000");
                            }
                        }
                    }
                }
                else
                {
                    grdDyeName.DataSource = dtDye;
                    Session["dtDye"] = dtDye;
                    grdDyeName.DataBind();
                }
            }
            else
            {
                grdDyeName.DataSource = dtDye;
                Session["dtDye"] = dtDye;
                grdDyeName.DataBind();

                if (grdDyeName.Rows.Count > 0)
                {
                    Label txtTotalAmt = (Label)grdDyeName.FooterRow.FindControl("txtTotalAmt");
                    Label lblCostFooter = (Label)grdDyeName.FooterRow.FindControl("lblCostFooter");
                    txtTotalAmt.Text = GetTotalDose().ToString();
                    lblCostFooter.Text = GetTotalCost().ToString();
                    //dblTot = double.Parse(txtTotalAmt.Text.Trim());
                    //dblTemp = dblTot + double.Parse(txtDose.Text.Trim());
                }
            }

            if (ViewState["UNIQUE_ID"] != null)
                ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            double dblTot = 0, dblTemp = 0;
            int UNIQUE_ID = 0;

            if (ViewState["UNIQUE_ID"] != null)
                UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

            if (Session["dtDye"] != null)
                dtDye = (DataTable)Session["dtDye"];

            if (dtDye == null)
                CreateDataTable();

            if (UNIQUE_ID > 0)
            {
                if (grdDyeName.Rows.Count > 0)
                {
                    Label txtTotalAmt = (Label)grdDyeName.FooterRow.FindControl("txtTotalAmt");
                    txtTotalAmt.Text = GetTotalDoseForEdit(UNIQUE_ID).ToString();
                    dblTot = double.Parse(txtTotalAmt.Text.Trim());
                    dblTemp = dblTot + double.Parse(txtDose.Text.Trim());
                }

                if (dblTemp > 100000)
                {
                    //Common.CommonFuction.ShowMessage("Sum Of Dose Percent should not be greater than 100..");
                }
                else
                {
                    DataView dvEdit = new DataView(dtDye);
                    dvEdit.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                    if (dvEdit.Count > 0)
                    {
                        dvEdit[0]["DYE_NAME"] = cmbItemCode.Text.Trim();
                        dvEdit[0]["ITEM_DESC"] = ddlItemCode.SelectedText.ToString().Trim();
                        dvEdit[0]["RATE"] = double.Parse(txtRate.Text.Trim());
                        dvEdit[0]["DOSE"] = double.Parse(txtDose.Text.Trim());
                        dvEdit[0]["RECIPE_COST"] = double.Parse(txtCost.Text.Trim());
                        dtDye.AcceptChanges();
                    }
                }
            }
            else
            {
                if (grdDyeName.Rows.Count > 0)
                {
                    Label txtTotalAmt = (Label)grdDyeName.FooterRow.FindControl("txtTotalAmt");
                    txtTotalAmt.Text = GetTotalDose().ToString();
                    dblTot = double.Parse(txtTotalAmt.Text.Trim());
                    dblTemp = dblTot + double.Parse(txtDose.Text.Trim());
                }

                if (dblTemp > 100000)
                {
                    Common.CommonFuction.ShowMessage("Sum Of Dose Percent should not be greater than 100..");
                }
                else
                {
                    DataRow dr = dtDye.NewRow();
                    dr["UNIQUE_ID"] = dtDye.Rows.Count + 1;
                    dr["LAB_DIP_NO"] = txtLRNo.Text.Trim();
                    dr["LR_OPTION"] = txtOption.Text.Trim();
                    dr["GREY_LOT_NO"] = txtGeryLot.Text.Trim();
                    dr["DYE_NAME"] = cmbItemCode.Text.Trim();
                    dr["ITEM_DESC"] = ddlItemCode.SelectedText.ToString().Trim();
                    dr["RATE"] = double.Parse(txtRate.Text.Trim());
                    dr["DOSE"] = double.Parse(txtDose.Text.Trim());
                    if (txtCost.Text.Trim() != "")
                        dr["RECIPE_COST"] = double.Parse(txtCost.Text.Trim());
                    else
                        dr["RECIPE_COST"] = 0;

                    dtDye.Rows.Add(dr);
                }
            }
            BindGridWithLRNoAndOption(txtLRNo.Text.Trim(), txtOption.Text.Trim(), txtGeryLot.Text.Trim());
            ddlItemCode.SelectedIndex = -1;
            cmbItemCode.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtDose.Text = string.Empty;
            txtCost.Text = string.Empty;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    private double GetTotalDose()
    {
        try
        {
            int totalRows = grdDyeName.Rows.Count;
            double dblDose = 0, dblTemp = 0, dblTemp1 = 0, dblCost = 0;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdDyeName.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblDose = (Label)thisGridViewRow.FindControl("lblDose");
                    Label lblCost = (Label)thisGridViewRow.FindControl("lblCost");
                    double.TryParse(lblDose.Text, out dblDose);
                    dblTemp = dblTemp + dblDose;
                    double.TryParse(lblCost.Text, out dblCost);
                    dblTemp1 = dblTemp1 + dblCost;
                }
            }
            return dblTemp;
        }
        catch
        {
            throw;
        }
    }

    private double GetTotalCost()
    {
        try
        {
            int totalRows = grdDyeName.Rows.Count;
            double dblTemp1 = 0, dblCost = 0;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdDyeName.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblCost = (Label)thisGridViewRow.FindControl("lblCost");
                    double.TryParse(lblCost.Text, out dblCost);
                    dblTemp1 = dblTemp1 + dblCost;
                }
            }
            return dblTemp1;
        }
        catch
        {
            throw;
        }
    }

    private double GetTotalDoseForEdit(int UNIQUE_ID)
    {
        try
        {
            int totalRows = grdDyeName.Rows.Count;
            double dblDose = 0, dblTemp = 0;
            int Unique = 0;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdDyeName.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblDose = (Label)thisGridViewRow.FindControl("lblDose");
                    Label lblUnique = (Label)thisGridViewRow.FindControl("lblUnique");
                    int.TryParse(lblUnique.Text, out Unique);
                    if (Unique == UNIQUE_ID)
                    {

                    }
                    else
                    {
                        double.TryParse(lblDose.Text, out dblDose);
                        dblTemp = dblTemp + dblDose;
                    }
                }
            }
            return dblTemp;
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtDye = new DataTable();
            dtDye.Columns.Add("UNIQUE_ID", typeof(int));
            dtDye.Columns.Add("LAB_DIP_NO", typeof(string));
            dtDye.Columns.Add("LR_OPTION", typeof(string));
            dtDye.Columns.Add("GREY_LOT_NO", typeof(string));
            dtDye.Columns.Add("DYE_NAME", typeof(string));
            dtDye.Columns.Add("ITEM_DESC", typeof(string));
            dtDye.Columns.Add("RATE", typeof(double));
            dtDye.Columns.Add("DOSE", typeof(double));
            dtDye.Columns.Add("RECIPE_COST", typeof(double));
            dtDye.Columns.Add("UOM", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ddlItemCode.SelectedIndex = -1;
            cmbItemCode.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtDose.Text = string.Empty;
            txtCost.Text = string.Empty;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (Session["dtDye"] != null)
                dtDye = (DataTable)Session["dtDye"];

            if (grdDyeName.Rows.Count > 0)
            {
                double Total = 0;
                double dblTemp = 0;
                double Total1 = 0;
                double dblTemp1 = 0;
                Label lblCostFooter = (Label)grdDyeName.FooterRow.FindControl("lblCostFooter");
                Label lblDoseFooter = (Label)grdDyeName.FooterRow.FindControl("txtTotalAmt");
                lblCostFooter.Text = GetTotalCost().ToString();
                lblDoseFooter.Text = GetTotalDose().ToString();
                dblTemp = GetTotalCost();
                dblTemp1 = GetTotalDose();
                Total = dblTemp;
                Total1 = dblTemp1;
              //  Session["Total1"] = Total1;
                Session["dtDye"] = dtDye;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "closewinjs", "javascript:GetRowValue('" + Total + "', '" + TextBoxId + "', '" + Total1 + "', '" + TextBoxId1 +"')", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Close.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected void grdDyeName_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Unique_id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditTRN")
            {
                EditTRNRow(Unique_id);
            }
            else if (e.CommandName == "DeleteTRN")
            {
                DeleteTRNRow(Unique_id);
                BindGridWithLRNoAndOption(txtLRNo.Text.Trim(), txtOption.Text.Trim(), txtGeryLot.Text.Trim());
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    private void EditTRNRow(int UNIQUEID)
    {
        try
        {
            if (Session["dtDye"] != null)
                dtDye = (DataTable)Session["dtDye"];

            DataView dv = new DataView(dtDye);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                txtLRNo.Text = dv[0]["LAB_DIP_NO"].ToString();
                txtOption.Text = dv[0]["LR_OPTION"].ToString();
                txtGeryLot.Text = dv[0]["GREY_LOT_NO"].ToString();
                cmbItemCode.Text = dv[0]["DYE_NAME"].ToString();
                //cmbItemCode.SetIndexByValue(dv[0]["DYE_NAME"].ToString());
                //cmbItemCode.SelectedIndex = cmbItemCode.Items.IndexOf(cmbItemCode.Items.f(dv[0]["LAB_DIP_NO"].ToString()));
                txtRate.Text = dv[0]["RATE"].ToString();
                txtDose.Text = dv[0]["DOSE"].ToString();
                txtCost.Text = dv[0]["RECIPE_COST"].ToString();
                ViewState["UNIQUE_ID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteTRNRow(int UNIQUEID)
    {
        try
        {
            if (Session["dtDye"] != null)
                dtDye = (DataTable)Session["dtDye"];

            if (dtDye.Rows.Count == 1)
            {
                dtDye.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDye.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtDye.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDye.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }

            Session["dtDye"] = dtDye;
        }
        catch
        {
            throw;
        }
    }

    protected void txtDose_TextChanged(object sender, EventArgs e)
    {
        try
        {
            double dblRate = 0, dblDose = 0, dblCost = 0, dblTemp = 0;
            txtCost.Text = string.Empty;
            if (cmbItemCode.Text != string.Empty)
            {
                if (txtRate.Text != string.Empty)
                {
                    if (txtDose.Text != string.Empty)
                    {
                        dblRate = double.Parse(txtRate.Text.Trim());
                        if (dblRate == 0)
                        {
                            Common.CommonFuction.ShowMessage("Rate should be greater than Zero..");
                            txtDose.Text = string.Empty;
                            txtRate.Text = string.Empty;
                            txtCost.Text = string.Empty;
                        }
                        else
                        {
                            double.TryParse(txtRate.Text.Trim(), out dblRate);
                            double.TryParse(txtDose.Text.Trim(), out dblDose);
                            dblTemp = (dblRate / 1000);
                            dblCost = (dblTemp * dblDose * 10);
                            //dblCost = (dblTemp * dblDose );
                            //dblTemp = (dblRate);
                            //dblCost = (dblTemp * dblDose);
                           
                            txtCost.Text = dblCost.ToString("00.000000");


                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Please enter Dose %..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please select different Item, Because Last PO Rate for this Item is not in Database..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Item first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Dose Text Changed Event.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected void ddlItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            if (data != null && data.Rows.Count > 0)
            {
                ddlItemCode.Items.Clear();
                ddlItemCode.DataSource = data;
                ddlItemCode.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;
            CommandText = " SELECT * FROM (SELECT * FROM ( SELECT ITEM_CODE, ITEM_TYPE, ITEM_DESC, UOM, CAT_CODE FROM TX_ITEM_MST WHERE CAT_CODE = '" + CAT_CODE + "' ORDER BY ITEM_CODE) asd WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) bd WHERE ROWNUM <= 15 ";
            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_CODE NOT IN( SELECT ITEM_CODE FROM (SELECT * FROM ( SELECT ITEM_CODE, ITEM_TYPE, ITEM_DESC, UOM, CAT_CODE FROM TX_ITEM_MST WHERE CAT_CODE = '" + CAT_CODE + "' ORDER BY ITEM_CODE) asd WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) bd WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOVWithOutComp(CommandText, whereClause, SortExpression, "", SearchQuery);
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = string.Empty;
            CommandText = " SELECT * FROM (SELECT * FROM ( SELECT ITEM_CODE, ITEM_TYPE, ITEM_DESC, UOM, CAT_CODE FROM TX_ITEM_MST WHERE CAT_CODE = '" + CAT_CODE + "' ORDER BY ITEM_CODE) asd WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) bd WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ITEM_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOVWithOutComp(CommandText, WhereClause, SortExpression, "", SearchQuery);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

   protected void ddlItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
       try
        {
            cmbItemCode.Text = ddlItemCode.SelectedValue.ToString().Trim();

            if (Session["dtDye"] != null)
                dtDye = (DataTable)Session["dtDye"];

            if (dtDye != null && dtDye.Rows.Count > 0)
            {
                DataView dv = new DataView(dtDye);
                dv.RowFilter = "LAB_DIP_NO='" + LRNo + "' and LR_OPTION='" + Option + "' and DYE_NAME='" + cmbItemCode.Text.Trim() + "'";
                if (dv.Count > 0)
                {
                    Common.CommonFuction.ShowMessage("This Dye Name is already exists in the Grid, Please Choose different Dye Name..");
                    ddlItemCode.SelectedIndex = -1;
                    cmbItemCode.Text = string.Empty;
                }
                else
                {
                    txtRate.Text = string.Empty;
                    DataTable dtRate = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLastRateFromItemOpeningByItem(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlItemCode.SelectedValue.ToString().Trim(), "SELF");
                    if (dtRate != null && dtRate.Rows.Count > 0)
                    {
                        txtRate.Text = dtRate.Rows[0]["LAST_PO_RATE"].ToString().Trim();
                    }
                    else
                    {
                        txtRate.Text = "0";
                    }
                }
            }
            else
            {
                txtRate.Text = string.Empty;
                DataTable dtRate = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLastRateFromItemOpeningByItem(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, cmbItemCode.Text.Trim(), "SELF");
                if (dtRate != null && dtRate.Rows.Count > 0)
                {
                    txtRate.Text = dtRate.Rows[0]["LAST_PO_RATE"].ToString().Trim();
                }
                else
                {
                    txtRate.Text = "0";
                }
            }
            txtUOM.Text = "Kg.";
            txtDose.Focus();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Order Number..\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    

    
}
