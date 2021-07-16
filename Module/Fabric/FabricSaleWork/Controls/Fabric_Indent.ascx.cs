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
using System.Data.OracleClient;
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;
using Obout.Interface;
using System.Collections.Generic;
public partial class Module_Fabric_FabricSaleWork_Controls_Fabric_Indent : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static string CompanyCode;
    private static string BranchCode;
    private static string DepartmentCode;
    private static DataTable dtIndentDetail;
    private static double FinalTotal;
    private static string UserCode;
    private static string PRODUCT_TYPE = "FABRIC";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];        
            var EndDate = CommonFuction.GetYearEndDate(oUserLoginDetail.DT_STARTDATE);
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
            var dt = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.GetIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN");
            ddlIndentNumber.DataSource = dt;
            ddlIndentNumber.DataValueField = "IND_NUMB";
            ddlIndentNumber.DataTextField = "IND_NUMB";
            ddlIndentNumber.DataBind();
            dt.Dispose();

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
            txtIndentNumber.Text = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.GetNewIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN"); //FindMaxIndentNumber();
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            BindInitialData();
            BindIndentDetailGrid();           
            var data = GetItems("", 0, 10);
            if (data != null && data.Rows.Count > 0)
            {
                cmbFabricCode.Items.Clear();
                cmbFabricCode.DataSource = data;
                cmbFabricCode.DataTextField = "FABR_CODE";
                cmbFabricCode.DataValueField = "FABR_CODE";
                cmbFabricCode.DataBind();
            }         
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            txtIndentNumber.ReadOnly = true;
            txtIndentNumber.AutoPostBack = false;
            //BindShadeCode();
          
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);

        }
    }

    private void BindShadeByFabrCode(string FABR_CODE)
    {
        try
        {
            cmbShade.Items.Clear();
            var dtShade = SaitexDL.Interface.Method.TX_FABRIC_MST.GetShadeDataByFabrCode(FABR_CODE);
            if (dtShade != null && dtShade.Count > 0)
            {
                cmbShade.DataSource = dtShade;               
                cmbShade.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindFabricTypeByFabrCode(string FABR_CODE)
    {
        try
        {
           // BindShadeCode();           
            var dtFabricType = SaitexDL.Interface.Method.TX_FABRIC_MST.BindFabricTypeByFabrCode(FABR_CODE);
            foreach (string element in dtFabricType){
                cmbShade.SelectedIndex = cmbShade.Items.IndexOf(cmbShade.Items.FindByValue(element));
               // cmbShade.SelectedValue = element
                    
                    ; }           
        }
        catch
        {
            throw;
        }
    }

    private void BindInitialData()
    {
        try
        {
            txtIndentDate.Text = DateTime.Now.Date.ToShortDateString();
            txtRequiredBefore.Text = DateTime.Now.Date.ToShortDateString();

            if (Session["LoginDetail"] != null)
            {
                var oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
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
                var txtAmount = (Label)row.FindControl("txtAmount");
                double _amount = 0;
                double.TryParse(txtAmount.Text, out _amount);
                FinalTotal = FinalTotal + _amount;
            }
            if (grdIndentDetail.Rows.Count > 0)
            {
                var txtFooterAmount = (Label)grdIndentDetail.FooterRow.FindControl("txtFooterAmount");
                txtFooterAmount.Text = FinalTotal.ToString();
            }
            getReqDate();
            var oRupeesToWord = new AmountToWords.RupeesToWord();
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
        dtIndentDetail.Columns.Add("FABR_CODE", typeof(string));
        dtIndentDetail.Columns.Add("FABR_DESC", typeof(string));
        dtIndentDetail.Columns.Add("OP_BAL_STOCK", typeof(double));
        dtIndentDetail.Columns.Add("OPBAL_PRTY_STOK", typeof(double));
        dtIndentDetail.Columns.Add("OP_RATE", typeof(double));
        dtIndentDetail.Columns.Add("QTY", typeof(double));
        dtIndentDetail.Columns.Add("UOM", typeof(string));
        dtIndentDetail.Columns.Add("Amount", typeof(double));
        dtIndentDetail.Columns.Add("REMARK", typeof(string));
        dtIndentDetail.Columns.Add("SHADE_CODE", typeof(string));
        dtIndentDetail.Columns.Add("Min_Procure_days", typeof(double));
    }

    protected void grdIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
                cmbFabricCode.Enabled = false;
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "indentDelete")
            {              
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
            var dv = new DataView(dtIndentDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbFabricCode.SelectedValue = dv[0]["FABR_CODE"].ToString();
                txtFabricDesc.Text = dv[0]["FABR_DESC"].ToString();
                BindShadeByFabrCode(dv[0]["FABR_CODE"].ToString());
                // BindFabricTypeByFabrCode(dv[0]["FABR_CODE"].ToString());
                txtOpeningStock.Text = dv[0]["OP_BAL_STOCK"].ToString();
                txtOpeningPartyStock.Text = dv[0]["OPBAL_PRTY_STOK"].ToString();
                txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                txtRequestQty.Text = dv[0]["QTY"].ToString();
                txtUnitName.Text = dv[0]["UOM"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtRemark.Text = dv[0]["REMARK"].ToString();
                cmbShade.SelectedIndex = cmbShade.Items.IndexOf(cmbShade.Items.FindByText(dv[0]["SHADE_CODE"].ToString()));
                
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
                    var cmbFabricCode = (ComboBox)grdRow.FindControl("cmbFabricCode");
                    var txtRequestQty = (TextBox)grdRow.FindControl("txtRequestQty");
                    var txtOpeningRate = (Label)grdRow.FindControl("txtOpeningRate");
                    var txtAmount = (Label)grdRow.FindControl("txtAmount");
                    var txtFabricDesc = (Label)grdRow.FindControl("txtFabricDesc");
                    var txtOpeningStock = (Label)grdRow.FindControl("txtOpeningStock");
                    var txtOpeningPartyStock = (Label)grdRow.FindControl("txtOpeningPartyStock");
                    var txtUnitName = (Label)grdRow.FindControl("txtUnitName");
                    var txtRemark = (TextBox)grdRow.FindControl("txtRemark");
                    var txtIndentDetailNumber = (TextBox)grdRow.FindControl("txtIndentDetailNumber");
                    if (cmbFabricCode.SelectedText != "" && txtRequestQty.Text != "" && txtOpeningRate.Text != "" && txtAmount.Text != "")
                    {
                        double Qty = 0;
                        double.TryParse(txtRequestQty.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            var dr = dtIndentDetail.NewRow();
                            dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                            dr["IndentDetailNumber"] = int.Parse(txtIndentDetailNumber.Text.Trim());
                            dr["IndentNumber"] = txtIndentNumber.Text;
                            dr["FABR_CODE"] = cmbFabricCode.SelectedValue.Trim();
                            dr["FABR_DESC"] = txtFabricDesc.Text.Trim();
                            dr["OP_BAL_STOCK"] = double.Parse(txtOpeningStock.Text.Trim());
                            dr["OPBAL_PRTY_STOK"] = double.Parse(txtOpeningPartyStock.Text.Trim());
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
        var Result = false;
        try
        {
            var Shade = cmbShade.Text.Trim();
            foreach (GridViewRow grdRow in grdIndentDetail.Rows)
            {
                var txtFabricCode = (Label)grdRow.FindControl("txtFabricCode");
               var lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                var txtSHADE = (Label)grdRow.FindControl("txtShadeCode");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtFabricCode.Text.Trim() == FabricCode && UniqueId != iUniqueId && Shade == txtSHADE.Text.Trim())
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
            var thisTextBox = (TextBox)txtRequestQty;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {                 
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

    private int GetItemDetailByItemCode(string FabricCode, out string Description, out string UOM, out int OpeningStock, out int OpeningPartyStock, out double OpeningRate, out double Min_Procure_days)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "";
        OpeningStock = 0;
        OpeningPartyStock = 0;
        OpeningRate = 0;
        Min_Procure_days = 0;
        try
        {
            var dts = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.SelectFabricDetailsbyFabricCode(FabricCode);
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["FABR_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UOM"].ToString().Trim();
                OpeningStock = int.Parse(dts.Rows[0]["OP_BAL_STOCK"].ToString().Trim());
                OpeningPartyStock = int.Parse(dts.Rows[0]["OPBAL_PRTY_STOK"].ToString().Trim());
                OpeningRate = double.Parse(dts.Rows[0]["OP_RATE"].ToString().Trim());

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
            var dts = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.SelectByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN", int.Parse(IndentNumber));
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
                var dtTemp = GetItemIndentTrasaction(IndentNumber.Trim());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    txtIndentNumber.Text = IndentNumber;
                }
                else
                {
                    var msg = "Dear " + oUserLoginDetail.Username + " !! Indent already approved. Modification not allowed.";
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
                    double _ivalue = 0;
                    var dr = dtIndentDetail.NewRow();
                    dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                    dr["IndentDetailNumber"] = 0;
                    dr["IndentNumber"] = drTemp["IND_NUMB"];
                    dr["FABR_CODE"] = drTemp["FABR_CODE"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["FABR_DESC"] = drTemp["FABR_DESC"];
                    dr["OP_BAL_STOCK"] = drTemp["OP_BAL_STOCK"];
                    dr["OPBAL_PRTY_STOK"] = drTemp["OPBAL_PRTY_STOK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["QTY"] = drTemp["RQST_QTY"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["Amount"] = drTemp["iValue"];
                    dr["REMARK"] = drTemp["DPT_REMARK"];                   
                    double.TryParse(drTemp["iValue"].ToString(), out _ivalue);
                    FinalTotal = FinalTotal + _ivalue;
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
            return SaitexBL.Interface.Method.TX_FABRIC_IND_MST.Select_TransactionByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, "GEN", int.Parse(strIndentNum));
                     
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
        string URL = "../Reports/FabricIndent_OPT_R.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
       
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
       
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
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
            else
            {
                CommonFuction.ShowMessage("Indent Number not found");
            }
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (ViewState["dtIndentDetail"] != null)
            dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
        if (txtIndentNumber.Text != "")
        {
            if (dtIndentDetail.Rows.Count > 0)
            {
                UpdateIndentData();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
            }
        }
        else
        {
            CommonFuction.ShowMessage("Indent Number not found");
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
       
        txtIndentNumber.Text = string.Empty;
        lblMode.Text = "Update";      
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        RefreshDetailRow();
        ddlIndentNumber.Visible = true;
        tdDelete.Visible = false;
        txtIndentNumber.Visible = false;       
        ActivateUpdateMode();
    }

    protected void SaveIndentData()
    {
        try
        {

            var oFABRIC_IND_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IND_MST();
            oFABRIC_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFABRIC_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFABRIC_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFABRIC_IND_MST.IND_TYPE = "GEN";
            oFABRIC_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oFABRIC_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oFABRIC_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oFABRIC_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oFABRIC_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtComment.Text.Trim());
            oFABRIC_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oFABRIC_IND_MST.STATUS = true;
            oFABRIC_IND_MST.TUSER = oUserLoginDetail.UserCode;
            int Ind_numb = 0;          
            var Result = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.Insert(oFABRIC_IND_MST, dtIndentDetail, out Ind_numb);
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
            SaitexDM.Common.DataModel.TX_FABRIC_IND_MST oFABRIC_IND_MST = new SaitexDM.Common.DataModel.TX_FABRIC_IND_MST();
            oFABRIC_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFABRIC_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFABRIC_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFABRIC_IND_MST.IND_TYPE = "GEN";
            oFABRIC_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oFABRIC_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oFABRIC_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oFABRIC_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oFABRIC_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtComment.Text.Trim());
            oFABRIC_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oFABRIC_IND_MST.STATUS = true;
            oFABRIC_IND_MST.TUSER = oUserLoginDetail.UserCode;
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            bool Result = SaitexBL.Interface.Method.TX_FABRIC_IND_MST.Update(oFABRIC_IND_MST, dtIndentDetail);
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

    protected void Item_LOV_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            var thisTextBox = (ComboBox)cmbFabricCode;
            string Description = "";
            string UOM = "";
            int OpeningStock = 0;
            int OpeningPartyStock = 0;
            double OpeningRate = 0;
            double Min_Procure_days = 0;
            txtAmount.Text = string.Empty;
            txtRequestQty.Text = string.Empty;
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchFabricCodeInGrid(thisTextBox.SelectedValue.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.SelectedValue.Trim()), out Description, out UOM, out OpeningStock, out OpeningPartyStock, out OpeningRate, out Min_Procure_days);
                if (iRecordFound > 0)
                {
                    txtOpeningRate.Text = OpeningRate.ToString();
                    txtFabricDesc.Text = Description;
                    txtOpeningStock.Text = OpeningStock.ToString();
                    txtOpeningPartyStock.Text = OpeningPartyStock.ToString();
                    txtUnitName.Text = UOM;
                    txtRequestQty.Focus();
                    BindShadeByFabrCode(thisTextBox.SelectedValue.Trim());
                    //BindFabricTypeByFabrCode(thisTextBox.SelectedValue.Trim());
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

    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
           var thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();
            thisTextBox.DataSource = GetItems(e.Text, e.ItemsOffset, 10);
            thisTextBox.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + thisTextBox.Items.Count;
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
        var whereClause = " WHERE FABR_CODE like :SearchQuery or FABR_DESC like :SearchQuery";
        var sortExpression = " ORDER BY FABR_CODE";     
        var commandText = "SELECT * FROM TX_FABRIC_MST";
        var sPO = "";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

        
    }

    protected int GetItemsCount(string text)
    {
        var CommandText = "SELECT COUNT(*) FROM TX_FABRIC_MST WHERE FABR_CODE like :SearchQuery or FABR_TYPE like :SearchQuery or FABR_CODE like :SearchQuery";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetCountForLOV(CommandText, text + '%', "");
    }

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private void RefreshDetailRow()
    {
        cmbFabricCode.SelectedIndex = -1;
        cmbShade.Items.Clear();   
        cmbShade.SelectedIndex = -1;
        txtFabricDesc.Text = "";
        txtOpeningPartyStock.Text = "";
        txtOpeningRate.Text = "";
        txtRemark.Text = "";
        txtRequestQty.Text = "";
        txtAmount.Text = "";
        txtOpeningStock.Text = "";
        txtUnitName.Text = "";
        ViewState["UniqueId"] = null;
        txtRequestQty.Text = string.Empty;
        cmbFabricCode.Enabled = true;
        //BindShadeCode();
      
    }

    private void getReqDate()
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            var Temp = System.DateTime.Now.Date;
            if (dtIndentDetail != null && dtIndentDetail.Rows.Count > 0)
            {
               
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    double Min_Procure_Days = 0;
                    double.TryParse(dr["Min_Procure_days"].ToString(), out Min_Procure_Days);
                    var IndentDate = DateTime.Parse(txtIndentDate.Text.Trim());
                    var NewDate = IndentDate.AddDays(Min_Procure_Days);
                    int Val = Temp.CompareTo(NewDate);
                    if (Val == -1)
                        Temp = NewDate;
                }
                txtRequiredBefore.Text = Temp.ToShortDateString();
            }
            else
                txtRequiredBefore.Text = Temp.ToShortDateString();
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
                    var bb = SearchFabricCodeInGrid(cmbFabricCode.SelectedValue.Trim(), UniqueId);
                    if (!bb)
                    {
                        double Qty = 0;
                        double.TryParse(txtRequestQty.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            if (UniqueId > 0)
                            {
                                var dv = new DataView(dtIndentDetail);
                                dv.RowFilter = "UniqueId=" + UniqueId;
                                if (dv.Count > 0)
                                {
                                    dv[0]["IndentNumber"] = txtIndentNumber.Text;
                                    dv[0]["FABR_CODE"] = cmbFabricCode.SelectedValue.Trim();
                                    dv[0]["FABR_DESC"] = txtFabricDesc.Text.Trim();
                                    dv[0]["OP_BAL_STOCK"] = double.Parse(txtOpeningStock.Text.Trim());
                                    dv[0]["OPBAL_PRTY_STOK"] = double.Parse(txtOpeningPartyStock.Text.Trim());
                                    dv[0]["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                    dv[0]["QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["UOM"] = txtUnitName.Text.Trim();
                                    dv[0]["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["REMARK"] = txtRemark.Text.Trim();
                                    dv[0]["SHADE_CODE"] = cmbShade.Text.Trim();
                                    FinalTotal = FinalTotal + double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                    dtIndentDetail.AcceptChanges();
                                }
                            }
                            else
                            {

                                var dr = dtIndentDetail.NewRow();
                                dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                                dr["IndentDetailNumber"] = 0;
                                dr["IndentNumber"] = txtIndentNumber.Text;
                                dr["FABR_CODE"] = cmbFabricCode.SelectedValue.Trim();
                                dr["FABR_DESC"] = txtFabricDesc.Text.Trim();
                                dr["OP_BAL_STOCK"] = double.Parse(txtOpeningStock.Text.Trim());
                                dr["OPBAL_PRTY_STOK"] = double.Parse(txtOpeningPartyStock.Text.Trim());
                                dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                dr["QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                dr["Min_Procure_days"] = double.Parse(lblMin_Procure_days.Text.Trim());
                                dr["UOM"] = txtUnitName.Text.Trim();
                                dr["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dr["REMARK"] = txtRemark.Text.Trim();
                                dr["SHADE_CODE"] = cmbShade.Text.Trim();
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Fabric Code and Shade Code Should Be Diffrent');", true);
                    }
                }
                else if (cmbFabricCode.SelectedText == "")
                {
                    CommonFuction.ShowMessage("Fabric Code Required");
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
          

            var thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();
            thisTextBox.DataSource = GetIndents(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataTextField = "IND_NUMB";
            thisTextBox.DataValueField = "IND_NUMB";
            thisTextBox.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + thisTextBox.Items.Count;
            e.ItemsCount = thisTextBox.Items.Count;

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
            var whereClause = "  AND IND_NUMB like :SearchQuery order by IND_NUMB desc ";
            var sortExpression = " ";
            var commandText = "Select IND_NUMB, TO_CHAR(IND_DATE,'mm/dd/yyyy')as IND_DATE from TX_FABRIC_IND_MST WHERE BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'   ";
            var sPO = "";           
            return SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
     
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
               // txtIndentNumber.Text = IndentNo;
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

    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            //var data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);     
            var data = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FABRIC_TYPE", oUserLoginDetail.COMP_CODE);
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }           
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;          
           // e.ItemsCount = GetShadeItemsCount(e.Text);
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void BindShadeCode()
    {
        try
        {    
            var data = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FABRIC_TYPE", oUserLoginDetail.COMP_CODE);
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataValueField = "MST_DESC";
                cmbShade.DataTextField = "MST_DESC";
                cmbShade.DataBind();
                cmbShade.SelectedIndex = cmbShade.Items.IndexOf(cmbShade.Items.FindByValue("GREIGE"));
                //cmbShade.SelectedValue = "GREIGE";
            }           
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
  
    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
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
             var CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = '" + PRODUCT_TYPE + "' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            var WhereClause = " ";
            var SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            var SearchQuery = text.ToUpper() + "%";
            var data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void grdIndentDetail_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
                cmbFabricCode.Enabled = false;
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "indentDelete")
            {                
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

    protected void txtRequestQty_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            var thisTextBox = (TextBox)txtRequestQty;
            if (thisTextBox.Text != "")
            {
                double RequestQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {                   
                    double OpeningRate = 0f;                  
                    if (double.TryParse(CommonFuction.funFixQuotes(txtOpeningRate.Text.Trim()), out OpeningRate))
                    {
                      
                        var Total = (OpeningRate) * (RequestQTY);
                        txtAmount.Text = Total.ToString();
                        FinalTotal = FinalTotal + Total;
                        txtRemark.Focus();
                        var oRupeesToWord = new AmountToWords.RupeesToWord();
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
}




