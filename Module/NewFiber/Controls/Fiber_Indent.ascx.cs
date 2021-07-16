using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;
using System.Data;
using System.Data.OracleClient;
using DBLibrary;
public partial class Module_Fiber_Fiber_Indent1 : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string CompanyCode;
    private string BranchCode;
    private string DepartmentCode;
    private DataTable dtIndentDetail;
    private double FinalTotal;
    private string UserCode;
    private string PRODUCT_TYPE = "FIBER";

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DateTime StartDate = oUserLoginDetail.DT_STARTDATE;
            DateTime EndDate = CommonFuction.GetYearEndDate(StartDate);

            if (!IsPostBack)
            {
                bindcmbIndentNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN");
                InitialisePage();

            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    private void bindcmbIndentNo(string COMP_CODE, string BRANCH_CODE, int Year, string IND_TYPE)
    {
        try
        {
            ddlIndentNumber.Items.Clear();
            DataTable dt = new DataTable();

            dt = SaitexBL.Interface.Method.FIBER_NEW_IND_MST.GetIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN");
            ddlIndentNumber.DataSource = dt;
            ddlIndentNumber.DataValueField = "IND_NUMB";
            ddlIndentNumber.DataTextField = "IND_NUMB";
            ddlIndentNumber.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }

    private void InitialisePage()
    {
        try
        {
            ActivateSaveMode();
            lblMode.Text = "Save";
            txtIndentNumber.Focus();
            FinalTotal = 0;
            txtComment.Text = "";
            txtDepartment.Text = "";
            txtIndentDate.Text = "";

            txtIndentNumber.Text = "";
            txtPreparedBy.Text = "";
            txtRequiredBefore.Text = "";

            txtIndentNumber.Text = SaitexBL.Interface.Method.FIBER_NEW_IND_MST.GetNewIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN"); //FindMaxIndentNumber(); 
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            BindInitialData();
            BindIndentDetailGrid();
            DataTable data = new DataTable();
            data = GetItems("", 0, 10);
            cmbFabricCode.DataSource = data;
            cmbFabricCode.DataTextField = "FIBER_CODE";
            cmbFabricCode.DataValueField = "FIBER_CODE";
            cmbFabricCode.DataBind();
            //BindShade();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            txtIndentNumber.ReadOnly = true;
            txtIndentNumber.AutoPostBack = false;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);

        }
    }


    //private void BindShadeByFabrCode(string FIBER_CODE)
    //{
    //    try
    //    {
    //        cmbShade.Items.Clear();
    //        List<string> dtShade = SaitexDL.Interface.Method.TX_FABRIC_MST.GetShadeDataByFabrCode(FIBER_CODE);
    //        {
    //            cmbShade.DataSource = dtShade;

    //            cmbShade.DataBind();
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    //private void BindShade()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlShadeCode.Items.Clear();
    //            ddlShadeCode.DataSource = dt;
    //            ddlShadeCode.DataTextField = "SHADE_CODE";
    //            ddlShadeCode.DataValueField = "SHADE_CODE";
    //            ddlShadeCode.DataBind();
    //            //ddlShadeCode.Items.Insert(0, new ListItem("SELECT", "0"));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void BindInitialData()
    {
        try
        {
            txtIndentDate.Text = DateTime.Now.Date.ToShortDateString();
            txtRequiredBefore.Text = DateTime.Now.Date.ToShortDateString();

            if (Session["LoginDetail"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
                txtPreparedBy.Text = oUserLoginDetail.Username;
                txtIndentDate.Text = DateTime.Now.ToShortDateString();
                txtRequiredBefore.Text = DateTime.Now.ToShortDateString();
                CompanyCode = oUserLoginDetail.COMP_CODE;
                BranchCode = oUserLoginDetail.CH_BRANCHCODE;
                DepartmentCode = oUserLoginDetail.VC_DEPARTMENTCODE;
                UserCode = oUserLoginDetail.UserCode;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindIndentDetailGrid()
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            grdIndentDetail.DataSource = dtIndentDetail;
            grdIndentDetail.DataBind();
            FinalTotal = 0;
            foreach (GridViewRow row in grdIndentDetail.Rows)
            {
                Label txtAmount = (Label)row.FindControl("txtAmount");
                FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
            }
            if (grdIndentDetail.Rows.Count > 0)
            {
                Label txtFooterAmount = (Label)grdIndentDetail.FooterRow.FindControl("txtFooterAmount");
                txtFooterAmount.Text = FinalTotal.ToString();
            }
            getReqDate();
            AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
            lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreateIndentDetailTable()
    {
        dtIndentDetail = new DataTable();
        dtIndentDetail.Columns.Add("UniqueId", typeof(int));
        dtIndentDetail.Columns.Add("IndentDetailNumber", typeof(int));
        dtIndentDetail.Columns.Add("IndentNumber", typeof(string));
        dtIndentDetail.Columns.Add("FIBER_CODE", typeof(string));
        dtIndentDetail.Columns.Add("FIBER_DESC", typeof(string));
        dtIndentDetail.Columns.Add("OP_BAL_STOCK", typeof(double));
        dtIndentDetail.Columns.Add("OP_BAL_PRTY_STOK", typeof(double));
        dtIndentDetail.Columns.Add("OP_RATE", typeof(double));
        dtIndentDetail.Columns.Add("Min_Procure_days", typeof(double));
        dtIndentDetail.Columns.Add("QTY", typeof(double));
        dtIndentDetail.Columns.Add("UOM", typeof(string));
        dtIndentDetail.Columns.Add("UOM1", typeof(string));
        dtIndentDetail.Columns.Add("UOM_BAIL", typeof(string));
        dtIndentDetail.Columns.Add("Amount", typeof(double));
        dtIndentDetail.Columns.Add("REMARK", typeof(string));
        //dtIndentDetail.Columns.Add("SHADE_CODE", typeof(string));
    }

    protected void grdIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
                //cmbFabricCode.Enabled = false;
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "indentDelete")
            {
                //  FillDataTableByGrid();
                DeleteIndentDetailRow(UniqueId);
                BindIndentDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            DataView dv = new DataView(dtIndentDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                //cmbShade.Items.Clear();
                cmbFabricCode.SelectedValue = dv[0]["FIBER_CODE"].ToString();
                txtFabricDesc.Text = dv[0]["FIBER_DESC"].ToString();
                //BindShadeByFabrCode(dv[0]["FIBER_CODE"].ToString());
                txtOpeningStock.Text = dv[0]["OP_BAL_STOCK"].ToString();
                txtOpeningPartyStock.Text = dv[0]["OP_BAL_PRTY_STOK"].ToString();
                txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                txtRequestQty.Text = dv[0]["QTY"].ToString();
                txtUnitName.Text = dv[0]["UOM"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtRemark.Text = dv[0]["REMARK"].ToString();
                Txtkg_bail.Text = dv[0]["UOM_BAIL"].ToString();
                Txtuom2.Text = dv[0]["UOM1"].ToString();
                //cmbShade.SelectedIndex = cmbShade.Items.IndexOf(cmbShade.Items.FindByText(dv[0]["SHADE_CODE"].ToString()));

                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void DeleteIndentDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (grdIndentDetail.Rows.Count == 1)
            {
                dtIndentDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtIndentDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }

            ViewState["dtIndentDetail"] = dtIndentDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool FillDataTableByGrid()
    {
        try
        {
            bool result = true;
            if (grdIndentDetail.Rows.Count > 0)
            {
                dtIndentDetail.Rows.Clear();
                FinalTotal = 0;
                foreach (GridViewRow grdRow in grdIndentDetail.Rows)
                {
                    //  Obout.ComboBox.ComboBox Item_LOV = (Obout.ComboBox.ComboBox)grdRow.FindControl("Item_LOV");
                    ComboBox cmbFabricCode = (ComboBox)grdRow.FindControl("cmbFabricCode");
                    TextBox txtRequestQty = (TextBox)grdRow.FindControl("txtRequestQty");
                    Label txtOpeningRate = (Label)grdRow.FindControl("txtOpeningRate");
                    Label txtAmount = (Label)grdRow.FindControl("txtAmount");
                    Label txtFabricDesc = (Label)grdRow.FindControl("txtFabricDesc");
                    Label txtOpeningStock = (Label)grdRow.FindControl("txtOpeningStock");
                    Label txtOpeningPartyStock = (Label)grdRow.FindControl("txtOpeningPartyStock");
                    Label txtUnitName = (Label)grdRow.FindControl("txtUnitName");
                    TextBox txtRemark = (TextBox)grdRow.FindControl("txtRemark");
                    TextBox txtIndentDetailNumber = (TextBox)grdRow.FindControl("txtIndentDetailNumber");
                    if (cmbFabricCode.SelectedText != "" && txtRequestQty.Text != "" && txtOpeningRate.Text != "" && txtAmount.Text != "")
                    {
                        double Qty = 0;
                        double.TryParse(txtRequestQty.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            DataRow dr = dtIndentDetail.NewRow();
                            dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                            dr["IndentDetailNumber"] = int.Parse(txtIndentDetailNumber.Text.Trim());
                            dr["IndentNumber"] = txtIndentNumber.Text;
                            dr["FIBER_CODE"] = cmbFabricCode.SelectedValue.Trim();
                            dr["FIBER_DESC"] = txtFabricDesc.Text.Trim();
                            dr["OP_BAL_STOCK"] = double.Parse(txtOpeningStock.Text.Trim());
                            dr["OP_BAL_PRTY_STOK"] = double.Parse(txtOpeningPartyStock.Text.Trim());
                            dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                            dr["QTY"] = double.Parse(txtRequestQty.Text.Trim());
                            dr["UOM"] = txtUnitName.Text.Trim();
                            dr["Amount"] = double.Parse(txtAmount.Text.Trim());
                            dr["REMARK"] = txtRemark.Text.Trim();
                            FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
                            dtIndentDetail.Rows.Add(dr);
                        }
                        else
                        {
                            result = false;
                        }
                    }
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SearchFabricCodeInGrid(string FabricCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            //string Shade = cmbShade.Text.Trim();
            foreach (GridViewRow grdRow in grdIndentDetail.Rows)
            {
                Label txtFabricCode = (Label)grdRow.FindControl("txtFabricCode");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                //Label txtSHADE = (Label)grdRow.FindControl("txtShadeCode");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                //if (txtFabricCode.Text.Trim() == FabricCode && UniqueId != iUniqueId && Shade == txtSHADE.Text.Trim())
                if (txtFabricCode.Text.Trim() == FabricCode && UniqueId != iUniqueId)
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

    protected void txtRequestQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtRequestQty;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {
                    //GridViewRow grdRow = ((GridViewRow)(thisTextBox.NamingContainer));
                    //Label txtOpeningRate = (Label)grdRow.FindControl("txtOpeningRate");
                    //Label txtAmount = (Label)grdRow.FindControl("txtAmount");
                    double OpeningRate = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtOpeningRate.Text.Trim()), out OpeningRate))
                    {
                        double Total = (OpeningRate) * (double.Parse(RequestQTY.ToString()));
                        txtAmount.Text = Total.ToString();
                        FinalTotal = FinalTotal + Total;
                        txtRemark.Focus();
                        AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                        lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal.ToString());

                    }
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    private int GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out string UOM1, out string KG_BAIL, out double OpeningStock, out double OpeningPartyStock, out double OpeningRate, out double Min_Procure_days)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "";
        UOM1 = "";
        KG_BAIL = "";
        OpeningStock = 0;
        OpeningPartyStock = 0;
        OpeningRate = 0;
        Min_Procure_days = 0;

        try
        {


            DataTable dts = SaitexBL.Interface.Method.FIBER_NEW_IND_MST.GetItemDetailByItemCode(ItemCode, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["FIBER_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UOM"].ToString().Trim();
                UOM1 = dts.Rows[0]["UOM1"].ToString().Trim();
                KG_BAIL = dts.Rows[0]["UOM_BAIL"].ToString().Trim();

                double _OP_BAL_STOCK = 0;
                double _OP_BAL_PRTY_STOK = 0;
                double _OP_RATE = 0;
                double.TryParse(dts.Rows[0]["OP_BAL_PRTY_STOK"].ToString().Trim(), out _OP_BAL_PRTY_STOK);
                double.TryParse(dts.Rows[0]["OP_BAL_STOCK"].ToString().Trim(), out _OP_BAL_STOCK);
                double.TryParse(dts.Rows[0]["OP_RATE"].ToString().Trim(), out _OP_RATE);

                OpeningStock = _OP_BAL_STOCK;
                OpeningPartyStock = _OP_BAL_PRTY_STOK;
                OpeningRate = _OP_RATE;
                double.TryParse(dts.Rows[0]["PROCURE_DAYS"].ToString().Trim(), out Min_Procure_days);
                iRecordFound = dts.Rows.Count;
            }
            return iRecordFound;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int GetdataByIndentNumber(string IndentNumber)
    {
        int iRecordFound = 0;
        try
        {

            DataTable dts = SaitexBL.Interface.Method.FIBER_NEW_IND_MST.SelectByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN", int.Parse(IndentNumber));
            if (dts != null && dts.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtComment.Text = dts.Rows[0]["CONF_COMMENT"].ToString().Trim();
                txtIndentDate.Text = DateTime.Parse(dts.Rows[0]["IND_DATE"].ToString().Trim()).ToShortDateString();
                txtPreparedBy.Text = dts.Rows[0]["PREP_BY"].ToString().Trim();
                txtRequiredBefore.Text = DateTime.Parse(dts.Rows[0]["REQD_DATE"].ToString().Trim()).ToShortDateString();
                txtDepartment.Text = dts.Rows[0]["DEPT_NAME"].ToString().Trim();
            }

            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetItemIndentTrasaction(IndentNumber.Trim());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    txtIndentNumber.Text = IndentNumber;
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Indent already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtIndentNumber.Text = "";
                    ddlIndentNumber.Focus();

                    lblMode.Text = "Update";

                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                    tdDelete.Visible = false;

                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }
        catch (OracleException ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }

        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtIndentDetail.NewRow();
                    dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                    dr["IndentDetailNumber"] = 0;
                    dr["IndentNumber"] = drTemp["IND_NUMB"];
                    dr["FIBER_CODE"] = drTemp["FIBER_CODE"];
                    //dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["FIBER_DESC"] = drTemp["FIBER_DESC"];
                    dr["OP_BAL_STOCK"] = drTemp["OP_BAL_STOCK"];
                    dr["OP_BAL_PRTY_STOK"] = drTemp["OP_BAL_PRTY_STOK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["QTY"] = drTemp["RQST_QTY"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["UOM1"] = drTemp["UOM1"];
                    dr["UOM_BAIL"] = drTemp["UOM_BAIL"];
                    dr["Amount"] = drTemp["iValue"];
                    dr["REMARK"] = drTemp["DPT_REMARK"];
                    FinalTotal = FinalTotal + double.Parse(drTemp["iValue"].ToString());
                    dtIndentDetail.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable GetItemIndentTrasaction(string strIndentNum)
    {
        try
        {
            return SaitexBL.Interface.Method.FIBER_NEW_IND_MST.Select_TransactionByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, int.Parse(strIndentNum), oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, "GEN");

        }

        catch (OracleException ex)
        {

            throw ex;
        }

        catch (Exception ex)
        {

            throw ex;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "Fiber_Indent_Report.aspx";
        //////ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
        Response.Redirect(URL);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
        RefreshDetailRow();
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));

        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Help Is Not Available Yet');", true);
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    /// <summary>
    /// code for save fabric indent data
    /// code tested by abhishek 9-4-2012 monday
    /// </summary>
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtRequiredBefore.Text == "")
            {
                txtRequiredBefore.Text = DateTime.Now.ToShortDateString();
            }
            if (grdIndentDetail.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
            }
            else
            {
                if (ViewState["dtIndentDetail"] != null)
                    dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

                if (Page.IsValid)
                {
                    if (dtIndentDetail.Rows.Count > 0)
                    {
                        SaveIndentData();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }


    /// <summary>
    /// code for update fabric indent data
    /// code tested by abhishek 9-4-2012 monday
    /// </summary>    
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["dtIndentDetail"] != null)
            dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

        if (dtIndentDetail.Rows.Count > 0)
        {
            UpdateIndentData();
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
        }



    }

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (txtIndentNumber.Text != null)
            {

            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        //txtIndentNumber.Focus();
        //txtIndentNumber.ReadOnly = false;
        //txtIndentNumber.AutoPostBack = true;
        txtIndentNumber.Text = string.Empty;
        lblMode.Text = "Update";
        // ddlIndentNumber.Focus();
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        tdDelete.Visible = false;
        RefreshDetailRow();
        ddlIndentNumber.Visible = true;
        txtIndentNumber.Visible = false;
        ActivateUpdateMode();
    }

    protected void SaveIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.FIBER_NEW_IND_MST OFIBER_NEW_IND_MST = new SaitexDM.Common.DataModel.FIBER_NEW_IND_MST();

            OFIBER_NEW_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            OFIBER_NEW_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            OFIBER_NEW_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            OFIBER_NEW_IND_MST.IND_TYPE = "GEN";
            OFIBER_NEW_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            OFIBER_NEW_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            OFIBER_NEW_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            OFIBER_NEW_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            OFIBER_NEW_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtComment.Text.Trim());
            OFIBER_NEW_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            OFIBER_NEW_IND_MST.STATUS = true;
            OFIBER_NEW_IND_MST.TUSER = oUserLoginDetail.UserCode;
            int Ind_numb = 0;

            bool Result = SaitexBL.Interface.Method.FIBER_NEW_IND_MST.Insert(OFIBER_NEW_IND_MST, dtIndentDetail, out Ind_numb);
            if (Result)
            {
                InitialisePage();
                cmbFabricCode.SelectedIndex = -1;
                string msg = "Indent Number " + Ind_numb + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent saving Failed');", true);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);
        }
    }

    protected void UpdateIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.FIBER_NEW_IND_MST OFIBER_NEW_IND_MST = new SaitexDM.Common.DataModel.FIBER_NEW_IND_MST();


            OFIBER_NEW_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            OFIBER_NEW_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            OFIBER_NEW_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            OFIBER_NEW_IND_MST.IND_TYPE = "GEN";
            OFIBER_NEW_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            OFIBER_NEW_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            OFIBER_NEW_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            OFIBER_NEW_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            OFIBER_NEW_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtComment.Text.Trim());
            OFIBER_NEW_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            OFIBER_NEW_IND_MST.STATUS = true;
            OFIBER_NEW_IND_MST.TUSER = oUserLoginDetail.UserCode;
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];


            bool Result = SaitexBL.Interface.Method.FIBER_NEW_IND_MST.Update(OFIBER_NEW_IND_MST, dtIndentDetail);
            if (Result)
            {
                InitialisePage();
                cmbFabricCode.SelectedIndex = -1;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updation Failed');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + ex.Message + "');", true);
        }
    }

    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetItems(e.Text, e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage("Problem in getting Item for Indent. see error log for detail.");
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        string whereClause = " WHERE upper(FIBER_CODE) like :SearchQuery or upper(FIBER_DESC) like :SearchQuery OR upper(FIBER_CAT) like :SearchQuery OR upper(SUB_FIBER_CAT) LIKE  :SearchQuery";
        string sortExpression = " ORDER BY FIBER_CODE";
        string commandText = "SELECT * FROM TX_FIBER_NEW_MASTER";
        string sPO = "";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", '%' + text.ToUpper() + '%', sPO);

    }

    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT COUNT(*) FROM TX_FIBER_NEW_MASTER WHERE upper(FIBER_CODE) like :SearchQuery or upper(FIBER_DESC) like :SearchQuery OR upper(FIBER_CAT) like :SearchQuery OR upper(SUB_FIBER_CAT) LIKE  :SearchQuery";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetCountForLOV(CommandText, '%' + text.ToUpper() + '%', "");
    }


    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private void RefreshDetailRow()
    {
        cmbFabricCode.SelectedIndex = -1;
        //cmbShade.Items.Clear();
        //cmbShade.SelectedIndex = -1;

        txtFabricDesc.Text = "";
        txtOpeningPartyStock.Text = "";
        txtOpeningRate.Text = "";
        txtRemark.Text = "";
        txtRequestQty.Text = "";
        txtAmount.Text = string.Empty;
        txtOpeningStock.Text = "";
        txtUnitName.Text = "";
        ViewState["UniqueId"] = null;
        txtRequestQty.Text = string.Empty;
        Txtuom2.Text = string.Empty;
        Txtkg_bail.Text = string.Empty;

    }

    private void getReqDate()
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

            if (dtIndentDetail != null && dtIndentDetail.Rows.Count > 0)
            {
                DateTime Temp = System.DateTime.Now.Date;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    double Min_Procure_Days = 0;
                    double.TryParse(dr["Min_Procure_days"].ToString(), out Min_Procure_Days);
                    // double Min_Procure_Days = double.Parse(dr["Min_Procure_days"].ToString());
                    DateTime IndentDate = DateTime.Parse(txtIndentDate.Text.Trim());
                    DateTime NewDate = IndentDate.AddDays(Min_Procure_Days);
                    int Val = Temp.CompareTo(NewDate);
                    if (Val == -1)
                        Temp = NewDate;
                }
                txtRequiredBefore.Text = Temp.ToShortDateString();
            }
            else
                txtRequiredBefore.Text = DateTime.Now.ToShortDateString();
        }
        catch
        {
            throw;
        }
    }

    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();

            if (txtRequestQty.Text != "" && txtOpeningRate.Text != "")
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchFabricCodeInGrid(cmbFabricCode.SelectedValue.Trim(), UniqueId);
                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtRequestQty.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(dtIndentDetail);
                            dv.RowFilter = "UniqueId=" + UniqueId;
                            if (dv.Count > 0)
                            {
                                dv[0]["IndentNumber"] = txtIndentNumber.Text;
                                dv[0]["FIBER_CODE"] = cmbFabricCode.SelectedValue.Trim();
                                dv[0]["FIBER_DESC"] = txtFabricDesc.Text.Trim();
                                dv[0]["OP_BAL_STOCK"] = double.Parse(txtOpeningStock.Text.Trim());
                                dv[0]["OP_BAL_PRTY_STOK"] = double.Parse(txtOpeningPartyStock.Text.Trim());
                                dv[0]["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                dv[0]["QTY"] = double.Parse(txtRequestQty.Text.Trim());

                                dv[0]["UOM"] = txtUnitName.Text.Trim();
                                dv[0]["UOM1"] = Txtuom2.Text.Trim();
                                dv[0]["UOM_BAIL"] = Txtkg_bail.Text.Trim();
                                dv[0]["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dv[0]["REMARK"] = txtRemark.Text.Trim();
                                //dv[0]["SHADE_CODE"] = cmbShade.Text.Trim();
                                FinalTotal = FinalTotal + double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dtIndentDetail.AcceptChanges();
                            }
                        }
                        else
                        {

                            DataRow dr = dtIndentDetail.NewRow();
                            dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                            dr["IndentDetailNumber"] = 0;
                            dr["IndentNumber"] = txtIndentNumber.Text;
                            dr["FIBER_CODE"] = cmbFabricCode.SelectedValue.Trim();
                            dr["FIBER_DESC"] = txtFabricDesc.Text.Trim();
                            dr["OP_BAL_STOCK"] = double.Parse(txtOpeningStock.Text.Trim());
                            dr["OP_BAL_PRTY_STOK"] = double.Parse(txtOpeningPartyStock.Text.Trim());
                            dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                            //dr["Min_Procure_days"] = double.Parse(lblMin_Procure_days.Text.Trim());
                            dr["QTY"] = double.Parse(txtRequestQty.Text.Trim());
                            dr["Min_Procure_days"] = double.Parse(lblMin_Procure_days.Text.Trim());

                            dr["UOM"] = txtUnitName.Text.Trim();
                            dr["UOM1"] = Txtuom2.Text.Trim();
                            dr["UOM_BAIL"] = Txtkg_bail.Text.Trim();
                            dr["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                            dr["REMARK"] = txtRemark.Text.Trim();
                            //dr["SHADE_CODE"] = cmbShade.Text.Trim();
                            FinalTotal = FinalTotal + double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                            dtIndentDetail.Rows.Add(dr);
                            lblMin_Procure_days.Text = "";
                        }
                        RefreshDetailRow();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Fiber Code can not be blank');", true);
                }
            }
            else if (cmbFabricCode.SelectedText == "")
            {
                CommonFuction.ShowMessage("Fiber Code Required");
            }
            else if (txtRequestQty.Text == "")
            {
                CommonFuction.ShowMessage("Quantity can not be zero");
            }
            ViewState["dtIndentDetail"] = dtIndentDetail;
            BindIndentDetailGrid();
            getReqDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }

    protected void ddlIndentNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetIndents(e.Text.ToUpper(), e.ItemsOffset, 10);

            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in loading Indent for updation. see error log for detail");
        }
    }

    protected DataTable GetIndents(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = "  AND IND_NUMB like :SearchQuery order by IND_NUMB desc ";
            string sortExpression = " ";
            string commandText = "Select IND_NUMB, TO_CHAR(IND_DATE,'mm/dd/yyyy')as IND_DATE from TX_FIBER_NEW_IND_MST WHERE BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND YEAR='" + oUserLoginDetail.DT_STARTDATE.Year + "'  ";
            string sPO = "";
            //DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN", commandText, whereClause, sortExpression, "", text + "%");
            return SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
                                                                                                          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    /// <summary>
    /// Fiber Indent dropdown select index change code to get all indent's of fiber
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlIndentNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            string IndentNo = ddlIndentNumber.SelectedValue.Trim();
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByIndentNumber(CommonFuction.funFixQuotes(IndentNo));
            BindIndentDetailGrid();
            if (iRecordFound == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Indent Number');", true);
                //InitialisePage();
                txtIndentNumber.Focus();
                txtIndentNumber.Text = "";
                ActivateUpdateMode();
            }
            else
            {

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage("Problem in loading Indent data for updation. see error log for detail");
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            ddlIndentNumber.Visible = true;
            ddlIndentNumber.SelectedValue = string.Empty;
            ddlIndentNumber.SelectedText = string.Empty;
            ddlIndentNumber.SelectedIndex = -1;
            ddlIndentNumber.Items.Clear();
            txtIndentNumber.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            ddlIndentNumber.Visible = false;
            ddlIndentNumber.SelectedValue = string.Empty;
            ddlIndentNumber.SelectedText = string.Empty;
            ddlIndentNumber.SelectedIndex = -1;
            ddlIndentNumber.Items.Clear();
            txtIndentNumber.Visible = true;
        }
        catch
        {
            throw;
        }
    }


    //protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
    //        // Looping through the items and adding them to the "Items" collection of the ComboBox
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            cmbShade.Items.Clear();
    //            cmbShade.DataSource = data;
    //            cmbShade.DataBind();
    //        }
    //        // Calculating the numbr of items loaded so far in the ComboBox
    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
    //        // Getting the total number of items that start with the typed text
    //        e.ItemsCount = GetShadeItemsCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected void cmbShade_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        if (cmbShade.SelectedIndex > -1)
    //        {
    //            string[] arrString = cmbShade.SelectedValue.Split('@');
    //            cmbShade.Text = arrString[0].ToString();
    //            cmbShade.Text = arrString[1].ToString();
    //            cmbShade.Text = arrString[2].ToString();
    //            cmbShade.Text = arrString[3].ToString();

    //        }
    //        else
    //        {
    //            Common.CommonFuction.ShowMessage("Please select Shade");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Selection.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected DataTable GetShadeItems(string text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
    //        string whereClause = string.Empty;

    //        if (startOffset != 0)
    //        {
    //            whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
    //        }
    //        string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected int GetShadeItemsCount(string text)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
    //        string WhereClause = " ";
    //        string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
    //        return data.Rows.Count;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void cmbFabricCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            ComboBox thisTextBox = (ComboBox)cmbFabricCode;
            string Description = "";
            string UOM = "";
            string UOM1 = "";
            string KG_BAIL = "";
            double OpeningStock = 0;
            double OpeningPartyStock = 0;
            double OpeningRate = 0;
            double Min_Procure_days = 0;
            txtAmount.Text = string.Empty;
            txtRequestQty.Text = string.Empty;
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchFabricCodeInGrid(thisTextBox.SelectedValue.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.SelectedValue.Trim()), out Description, out UOM, out UOM1, out KG_BAIL, out OpeningStock, out OpeningPartyStock, out OpeningRate, out Min_Procure_days);
                if (iRecordFound > 0)
                {
                    txtOpeningRate.Text = OpeningRate.ToString();
                    txtFabricDesc.Text = Description;
                    txtOpeningStock.Text = OpeningStock.ToString();
                    txtOpeningPartyStock.Text = OpeningPartyStock.ToString();
                    txtUnitName.Text = UOM;
                    Txtuom2.Text = UOM1;
                    Txtkg_bail.Text = KG_BAIL;
                    txtRequestQty.Focus();
                    //BindShadeByFabrCode(thisTextBox.SelectedValue.Trim());
                    lblMin_Procure_days.Text = Min_Procure_days.ToString();
                }
                else
                {
                    RefreshDetailRow();
                    thisTextBox.Focus();
                }
            }
            else
            {
                CommonFuction.ShowMessage("This Item already included");
                thisTextBox.SelectedText = "";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage("Problem in Item selection. see error log for detail");
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtRequestQty_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtRequestQty;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {
                    //GridViewRow grdRow = ((GridViewRow)(thisTextBox.NamingContainer));
                    //Label txtOpeningRate = (Label)grdRow.FindControl("txtOpeningRate");
                    //Label txtAmount = (Label)grdRow.FindControl("txtAmount");
                    double OpeningRate = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtOpeningRate.Text.Trim()), out OpeningRate))
                    {
                        double Total = (OpeningRate) * (double.Parse(RequestQTY.ToString()));
                        txtAmount.Text = Total.ToString();
                        FinalTotal = FinalTotal + Total;
                        txtRemark.Focus();
                        AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
                        lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal.ToString());

                    }
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtIndentNumber_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByIndentNumber(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            BindIndentDetailGrid();
            if (iRecordFound == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Indent Number');", true);
                //InitialisePage();
                txtIndentNumber.Focus();
                txtIndentNumber.Text = "";

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data indent updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

}