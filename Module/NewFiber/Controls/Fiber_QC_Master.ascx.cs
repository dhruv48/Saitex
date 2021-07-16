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
using Common;
using errorLog;
using Obout.ComboBox;
using System.Collections.Generic;

public partial class Module_NewFiber_Controls_Fiber_QC_Master : System.Web.UI.UserControl
{
  
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    private DataTable dtDetailTBL = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (Session["urLoginId"] != null)
                {
                    if (!IsPostBack)
                    {
                        Initialisepage();
                    }
                }

            }
            else
            {
                Response.Redirect("~/default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page Loading.\r\nSee error log for detail."));
        }
    }

    private void Initialisepage()
    {
        BlanksControls();
        bindINWARDTYPE("");
        txtYarnCode.Text = string.Empty;
        ddlyarncode.Enabled = true;
        ddlYarnCat.Enabled = true;
        ddlInwardType.Enabled = true;
        txtTRNNUMBer.Text = string.Empty;
        ddlYarnCat.SelectedIndex = -1;
        ddlyarncode.SelectedIndex = -1;
        ddlTRNNumber.SelectedIndex = -1;
        txtNominalCount.Text = string.Empty;
        ddlInwardType.SelectedIndex = 0;
        bindCategory("FIBER_CAT");
        BindNewMRNNum();
        tdSave.Visible = true;
        tdUpdate.Visible = false;
        tdFind.Visible = true;
        lblMode.Text = "Save";
        ViewState["dtDetailTBL"] = null;
        ddlTRNNumber.Visible = false;
        txtTRNNUMBer.Visible = true;
        grdMaterialItemReceipt.DataSource = null;
        grdMaterialItemReceipt.DataBind();
    }

    private void BindNewMRNNum()
    {
        try
        {
            string FIBER_CATEGORY = "", FIBER_CODE = "", InwardType = "";
            if (ddlYarnCat.SelectedIndex > -1)
            {
                FIBER_CATEGORY = ddlYarnCat.SelectedValue;
            }
            if (ddlyarncode.SelectedIndex > -1)
            {
                FIBER_CODE = ddlyarncode.SelectedText.Trim();
            }
            if (ddlInwardType.SelectedIndex > 0)
            {
                InwardType = ddlInwardType.SelectedItem.Text.Trim();
            }
            SaitexDM.Common.DataModel.FIBERQC_PARAMETER oFIBERQC_PARAMETER = new SaitexDM.Common.DataModel.FIBERQC_PARAMETER();
            oFIBERQC_PARAMETER.FIBER_CATEGORY = FIBER_CATEGORY;
            oFIBERQC_PARAMETER.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFIBERQC_PARAMETER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFIBERQC_PARAMETER.FIBER_CODE = FIBER_CODE;
            oFIBERQC_PARAMETER.INWARD_TYPE = InwardType;
            txtTRNNUMBer.Text = SaitexBL.Interface.Method.FIBERQC_PARAMETER.GetNewTRNNumber(oFIBERQC_PARAMETER).ToString();
        }
        catch
        {
            throw;
        }
    }

    private void BlanksControls()
    {
        try
        {
            tdUom.Visible = false;
            tdheadUom.Visible = false;
            txtRemarks.Text = string.Empty;
            txtMaxValue.Text = "0";
            txtMinValue.Text = "0";
            txtTolerance.Text = string.Empty;
            bindSTD_Type("");
            bindTolerance_Range("");
            bindTolerance_type("");
            ddlStdType.SelectedIndex = -1;
            ddltolerancerange.SelectedIndex = -1;
            ddltoleranceType.SelectedIndex = -1;
            ddlUOM.SelectedIndex = -1;
            ViewState["UNIQUEID"] = null;
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                double Nominal_Count = 0, Total_Imperfection = 0;
                double TRN_NUMB = 0;
                txtTRNNUMBer.Text = ddlTRNNumber.SelectedText;
                double.TryParse(txtTRNNUMBer.Text, out TRN_NUMB);
                double.TryParse(txtNominalCount.Text, out Nominal_Count);
                double.TryParse(txtTotalImperfection.Text, out Total_Imperfection);
                SaitexDM.Common.DataModel.FIBERQC_PARAMETER oFIBERQC_PARAMETER = new SaitexDM.Common.DataModel.FIBERQC_PARAMETER();
                oFIBERQC_PARAMETER.FIBER_CATEGORY = ddlYarnCat.SelectedValue.Trim();
                oFIBERQC_PARAMETER.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFIBERQC_PARAMETER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFIBERQC_PARAMETER.FIBER_CODE = txtYarnCode.Text.Trim();
                oFIBERQC_PARAMETER.INWARD_TYPE = ddlInwardType.SelectedItem.Text;
                oFIBERQC_PARAMETER.NOMINAL_COUNT = Nominal_Count;
                oFIBERQC_PARAMETER.TOTAL_IMPERFECTION = Total_Imperfection;
                oFIBERQC_PARAMETER.TUSER = oUserLoginDetail.UserCode;
                oFIBERQC_PARAMETER.TDATE = DateTime.Now;
                oFIBERQC_PARAMETER.TRN_NUMB = TRN_NUMB;
                bool bResult = SaitexBL.Interface.Method.FIBERQC_PARAMETER.UpdateSTANDARD_PARAMETER(oFIBERQC_PARAMETER, dtDetailTBL);
                if (bResult)
                {
                    Initialisepage();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fiber QC Standard Updated Successfully');", true);

                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fill Fiber QC Standard detail');", true);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving.\r\nSee error log for detail."));
        }
    }


    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Yarn_QC_Report.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=100,scrollbars=1,resizable=1,width=800,height=500');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/NewFiber/Pages/Fiber_Qc_Standard_Parameter.aspx");
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }


    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Initialisepage();

    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        lblMode.Text = "Update";
        ddlTRNNumber.Visible = true;
        txtTRNNUMBer.Visible = false;

    }



    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string iRecordFound = "";
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                double Nominal_Count = 0, Total_Imperfection = 0;
                double TRN_NUMB = 0;
                double.TryParse(txtTRNNUMBer.Text, out TRN_NUMB);
                double.TryParse(txtNominalCount.Text, out Nominal_Count);
                double.TryParse(txtTotalImperfection.Text, out Total_Imperfection);
                SaitexDM.Common.DataModel.FIBERQC_PARAMETER oFIBERQC_PARAMETER = new SaitexDM.Common.DataModel.FIBERQC_PARAMETER();
                oFIBERQC_PARAMETER.FIBER_CATEGORY = ddlYarnCat.SelectedValue.Trim();
                oFIBERQC_PARAMETER.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFIBERQC_PARAMETER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFIBERQC_PARAMETER.FIBER_CODE = txtYarnCode.Text.Trim();
                oFIBERQC_PARAMETER.INWARD_TYPE = ddlInwardType.SelectedItem.Text;
                oFIBERQC_PARAMETER.NOMINAL_COUNT = Nominal_Count;
                oFIBERQC_PARAMETER.TOTAL_IMPERFECTION = Total_Imperfection;
                oFIBERQC_PARAMETER.TUSER = oUserLoginDetail.UserCode;
                oFIBERQC_PARAMETER.TDATE = DateTime.Now;
                oFIBERQC_PARAMETER.TRN_NUMB = TRN_NUMB;
                bool bResult = SaitexBL.Interface.Method.FIBERQC_PARAMETER.InsertSTANDARD_PARAMETER(oFIBERQC_PARAMETER, dtDetailTBL, out  iRecordFound);
                if (bResult)
                {
                    Initialisepage();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fiber QC Standard Saved Successfully');", true);

                }
                else if (!string.IsNullOrEmpty(iRecordFound))
                {
                    iRecordFound += "Qc Standard of this yarn " + iRecordFound + " Already exist";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + iRecordFound + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fill Yarn QC Standard detail');", true);
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in saving.\r\nSee error log for detail."));
        }

    }



    private void bindINWARDTYPE(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "INWARDTYPE");
            ddlInwardType.Items.Clear();
            ddlInwardType.DataSource = dt;
            ddlInwardType.DataTextField = "MST_CODE";
            ddlInwardType.DataValueField = "MST_CODE";
            ddlInwardType.DataBind();
            ddlInwardType.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }

    private void bindSTD_Type(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "F_STD_TYPE");
            ddlStdType.Items.Clear();
            ddlStdType.DataSource = dt;
            ddlStdType.DataTextField = "MST_CODE";
            ddlStdType.DataValueField = "MST_CODE";
            ddlStdType.DataBind();
            ddlStdType.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }
    private void bindTolerance_type(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "TOLR_TYPE");
            ddltoleranceType.Items.Clear();
            ddltoleranceType.DataSource = dt;
            ddltoleranceType.DataTextField = "MST_CODE";
            ddltoleranceType.DataValueField = "MST_CODE";
            ddltoleranceType.DataBind();
            ddltoleranceType.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }

    private void bindTolerance_Range(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "TOLR_RANGE");
            ddltolerancerange.Items.Clear();
            ddltolerancerange.DataSource = dt;
            ddltolerancerange.DataTextField = "MST_CODE";
            ddltolerancerange.DataValueField = "MST_CODE";
            ddltolerancerange.DataBind();
            ddltolerancerange.Items.Insert(0, new ListItem("-Select-", "-1"));
        }
        catch
        {
            throw;
        }
    }


    private DataTable GET_MOM_DATA(string Text, string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select MST_CODE,MST_DESC from tx_Master_TRN where Del_Status=0 and MST_NAME=:MST_NAME and comp_code='" + oUserLoginDetail.COMP_CODE + "' ";
            string WhereClause = " and (UPPER(MST_CODE) like :SearchQuery OR UPPER(MST_DESC) like :SearchQuery)";
            string SortExpression = " order by MST_CODE asc";
            string SearchQuery = "%" + Text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_MASTER_TRN.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, MST_NAME, oUserLoginDetail.COMP_CODE);

        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "window.confirm('Are you sure to save the record ?')");
        imgbtnUpdate.Attributes.Add("OnClick", "window.confirm('Are you sure to update the record ?')");
        imgbtnPrint.Attributes.Add("OnClick", "window.confirm('Are you sure to print the record ?')");
        imgbtnClear.Attributes.Add("OnClick", "window.confirm('Are you sure to clear the record ?')");
        imgbtnExit.Attributes.Add("OnClick", "window.confirm('Are you sure to exit the record ?')");
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {

            bool ReturnResult = false;

            int count = 0;
            int iCountAll = 0;
            msg = string.Empty;
            double Tolerance = 0, Max_Value = 0, Min_Value = 0;
            iCountAll += 1;
            if (ddlStdType.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Std Type.\r\n";
            }
            iCountAll += 1;
            if (txtTolerance.Text != "" && double.TryParse(txtTolerance.Text, out Tolerance))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Invalid Tolerance.\r\n";
            }
            iCountAll += 1;
            if (ddltoleranceType.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Tolerance type.\r\n";
            }
            iCountAll += 1;
            if (ddltolerancerange.SelectedIndex > -1)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Select Tolerance Range.\r\n";
            }

            iCountAll += 1;
            if (txtMaxValue.Text != "" && double.TryParse(txtMaxValue.Text, out Max_Value))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Enter Valid Maximum Value .\r\n";
            }

            iCountAll += 1;
            if (txtMinValue.Text != "" && double.TryParse(txtMinValue.Text, out Min_Value))
            {
                count += 1;
            }
            else
            {
                msg += @"#. Enter Valid Minimum Value .\r\n";
            }



            if (count == iCountAll)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            bool Istrue = false;
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                Istrue = true;
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();


            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                int.TryParse(ViewState["UNIQUEID"].ToString(), out UNIQUEID);

            double Tolerance = 0, Max_Value = 0, Min_Value = 0;

            double.TryParse(txtMaxValue.Text.Trim(), out Max_Value);
            double.TryParse(txtMinValue.Text.Trim(), out Min_Value);
            double.TryParse(txtTolerance.Text.Trim(), out Tolerance);
            if (Istrue)
            {
                bool bb = SearchItemCodeInGrid(ddlStdType.SelectedItem.Text.Trim(), UNIQUEID);
                if (!bb)
                {

                    if (UNIQUEID > 0)
                    {
                        DataView dv = new DataView(dtDetailTBL);
                        dv.RowFilter = "SR_NO=" + UNIQUEID;
                        if (dv.Count > 0)
                        {
                            dv[0]["STD_TYPE"] = ddlStdType.SelectedItem.Text.Trim();
                            dv[0]["TOLERANCE"] = Tolerance;
                            dv[0]["TOLERANCE_TYPE"] = ddltoleranceType.SelectedItem.Text.Trim();
                            dv[0]["TOLERANCE_RANGE"] = ddltolerancerange.SelectedItem.Text.Trim();
                            dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                            dv[0]["MAX_VALUE"] = Max_Value;
                            dv[0]["MIN_VALUE"] = Min_Value;
                            dv[0]["UOM"] = ddlUOM.SelectedIndex > -1 ? ddlUOM.SelectedText.Trim() : "NA";
                            dtDetailTBL.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtDetailTBL.NewRow();
                        dr["SR_NO"] = dtDetailTBL.Rows.Count + 1;
                        dr["STD_TYPE"] = ddlStdType.SelectedItem.Text.Trim();
                        dr["TOLERANCE"] = Tolerance;
                        dr["TOLERANCE_TYPE"] = ddltoleranceType.SelectedItem.Text.Trim();
                        dr["TOLERANCE_RANGE"] = ddltolerancerange.SelectedItem.Text.Trim();
                        dr["REMARKS"] = txtRemarks.Text.Trim();
                        dr["MAX_VALUE"] = Max_Value;
                        dr["MIN_VALUE"] = Min_Value;
                        dr["UOM"] = ddlUOM.SelectedIndex > -1 ? ddlUOM.SelectedText.Trim() : "NA";
                        dtDetailTBL.Rows.Add(dr);
                    }
                    BlanksControls();



                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('This Fiber Code Already Added!!!');", true);
                }
            }

            ViewState["dtDetailTBL"] = dtDetailTBL;
            BindGridFromDataTable();
            CalculateTotal_Imperfection();
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding detail.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }
    private void CalculateTotal_Imperfection()
    {
        double Total_Imperfection = 0;
        foreach (GridViewRow row in grdMaterialItemReceipt.Rows)
        {
            double Max_Value = 0, Min_Value = 0;
            Label lblSTD_TYPE = (Label)row.FindControl("lblSTD_TYPE");
            Label lblMaxValue = (Label)row.FindControl("lblMaxValue");
            Label lblMinValue = (Label)row.FindControl("lblMinValue");
            Label lblTOLERANCE_RANGE = (Label)row.FindControl("lblTOLERANCE_RANGE");

            double.TryParse(lblMaxValue.Text, out Max_Value);
            double.TryParse(lblMinValue.Text, out Min_Value);
            if (lblTOLERANCE_RANGE.Text == "MAXIMUM")
            {
                if (lblSTD_TYPE.Text == "THIN")
                {
                    Total_Imperfection += Max_Value;
                }
                else if (lblSTD_TYPE.Text == "THICK")
                {
                    Total_Imperfection += Max_Value;
                }
                else if (lblSTD_TYPE.Text == "NEPS")
                {
                    Total_Imperfection += Max_Value;
                }
            }
            else if (lblTOLERANCE_RANGE.Text == "MINIMUM")
            {
                if (lblSTD_TYPE.Text == "THIN")
                {
                    Total_Imperfection += Min_Value;
                }
                else if (lblSTD_TYPE.Text == "THICK")
                {
                    Total_Imperfection += Min_Value;
                }
                else if (lblSTD_TYPE.Text == "NEPS")
                {
                    Total_Imperfection += Min_Value;
                }
            }
        }
        txtTotalImperfection.Text = Total_Imperfection.ToString();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        BlanksControls();
    }

    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("SR_NO", typeof(int));
            dtDetailTBL.Columns.Add("STD_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("TOLERANCE", typeof(double));
            dtDetailTBL.Columns.Add("TOLERANCE_TYPE", typeof(string));
            dtDetailTBL.Columns.Add("TOLERANCE_RANGE", typeof(string));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("MAX_VALUE", typeof(double));
            dtDetailTBL.Columns.Add("MIN_VALUE", typeof(double));
            dtDetailTBL.Columns.Add("UOM", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL == null)
                CreateDataTable();

            grdMaterialItemReceipt.DataSource = dtDetailTBL;
            grdMaterialItemReceipt.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void grdMaterialItemReceipt_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUEID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UNIQUEID);
                GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                row.BackColor = System.Drawing.Color.Green;
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemReceiptRow(UNIQUEID);
                BindGridFromDataTable();
                CalculateTotal_Imperfection();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchItemCodeInGrid(string STD_TYPE, int UNIQUEID)
    {
        bool Result = false;
        try
        {
            if (grdMaterialItemReceipt.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdMaterialItemReceipt.Rows)
                {
                    Label lblSTD_TYPE = (Label)grdRow.FindControl("lblSTD_TYPE");
                    LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());

                    if (lblSTD_TYPE.Text.Trim() == STD_TYPE && UNIQUEID != iUNIQUEID)
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

    private void EditItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "SR_NO=" + UNIQUEID;
            if (dv.Count > 0)
            {
                bindSTD_Type("");
                ddlStdType.SelectedIndex = ddlStdType.Items.IndexOf(ddlStdType.Items.FindByText(dv[0]["STD_TYPE"].ToString()));
                bindTolerance_Range("");
                ddltolerancerange.SelectedIndex = ddltolerancerange.Items.IndexOf(ddltolerancerange.Items.FindByText(dv[0]["TOLERANCE_RANGE"].ToString()));
                bindTolerance_type("");
                ddltoleranceType.SelectedIndex = ddltoleranceType.Items.IndexOf(ddltoleranceType.Items.FindByText(dv[0]["TOLERANCE_TYPE"].ToString()));
                txtTolerance.Text = dv[0]["TOLERANCE"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                txtMaxValue.Text = dv[0]["MAX_VALUE"].ToString();
                txtMinValue.Text = dv[0]["MIN_VALUE"].ToString();

                bindUOM("");

                foreach (ComboBoxItem item in ddlUOM.Items)
                {
                    if (item.Text == dv[0]["UOM"].ToString())
                    {
                        ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(item);
                        break;
                    }
                }

                ViewState["UNIQUEID"] = UNIQUEID;

            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemReceiptRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["SR_NO"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["SR_NO"] = iCount;
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }


    protected void ddlInwardType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindNewMRNNum();
    }

    protected void ddlYarnCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable data = GetYarnData("", 0, ddlYarnCat.SelectedValue);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fiber Category selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset, ddlYarnCat.SelectedValue);
            ddlyarncode.Items.Clear();
            ddlyarncode.DataSource = data;
            ddlyarncode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text, ddlYarnCat.SelectedValue);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fiber Code selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }


    //private DataTable GetYarnData(string Text, int startOffset, string Fiber_Category)
    //{
    //    try
    //    {
    //        DataTable dt = null;
    //        if (tdSave.Visible)
    //        {
    //            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = '" + Fiber_Category + "' ) WHERE   FIBER_CODE  not IN (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15 ";//AND NVL (QC_REQUIRED, 0) = '1'
    //            string whereClause = string.Empty;
    //            if (startOffset != 0)
    //            {
    //                whereClause += " AND FIBER_CODE NOT IN ( select FIBER_CODE from ( SELECT   FIBER_CODE FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = '" + Fiber_Category + "' ) WHERE   FIBER_CODE not IN  (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)    WHERE  ROWNUM <= " + startOffset + " )";//AND NVL (QC_REQUIRED, 0) = '1'
    //            }
    //            string SortExpression = " order by FIBER_CODE";
    //            string SearchQuery = "%" + Text + "%";
    //            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
    //        }
    //        else if (tdUpdate.Visible)
    //        {
    //            string CommandText = " SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = '" + Fiber_Category + "' ) WHERE   FIBER_CODE IN (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15 ";//AND NVL (QC_REQUIRED, 0) = '1'
    //            string whereClause = string.Empty;

    //            if (startOffset != 0)
    //            {
    //                whereClause += " AND FIBER_CODE NOT IN ( select FIBER_CODE from ( SELECT   FIBER_CODE FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = '" + Fiber_Category + "' ) WHERE   FIBER_CODE  IN  (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)    WHERE  ROWNUM <= " + startOffset + " ) ";//AND NVL (QC_REQUIRED, 0) = '1'
    //            }

    //            string SortExpression = " ORDER BY FIBER_CODE";
    //            string SearchQuery = "%" + Text.ToUpper() + "%";
    //            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        }
    //        return dt;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    private DataTable GetYarnData(string Text, int startOffset, string Fiber_Category)
    {
        try
        {
            DataTable dt = null;
            if (tdSave.Visible)
            {
                string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = FIBER_CAT ) WHERE   FIBER_CODE  not IN (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15 ";//AND NVL (QC_REQUIRED, 0) = '1'
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND FIBER_CODE NOT IN ( select FIBER_CODE from ( SELECT   FIBER_CODE FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = FIBER_CAT ) WHERE   FIBER_CODE not IN  (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)    WHERE  ROWNUM <= " + startOffset + " )";//AND NVL (QC_REQUIRED, 0) = '1'
                }
                string SortExpression = " order by FIBER_CODE";
                string SearchQuery = "%" + Text + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            }
            else if (tdUpdate.Visible)
            {
                string CommandText = " SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = FIBER_CAT ) WHERE   FIBER_CODE IN (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15 ";//AND NVL (QC_REQUIRED, 0) = '1'
                string whereClause = string.Empty;

                if (startOffset != 0)
                {
                    whereClause += " AND FIBER_CODE NOT IN ( select FIBER_CODE from ( SELECT   FIBER_CODE FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT =  FIBER_CAT ) WHERE   FIBER_CODE  IN  (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE)    WHERE  ROWNUM <= " + startOffset + " ) ";//AND NVL (QC_REQUIRED, 0) = '1'
                }

                string SortExpression = " ORDER BY FIBER_CODE";
                string SearchQuery = "%" + Text.ToUpper() + "%";
                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            }
            return dt;
        }
        catch
        {
            throw;
        }
    }
    protected int GetYarnCount(string text, string Fiber_Category)
    {
        DataTable dt = null;
        if (tdSave.Visible)
        {
            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC, Y_COUNT FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = '" + Fiber_Category + "' AND NVL (QC_REQUIRED, 0) = '1') WHERE   FIBER_CODE  not IN (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        else if (tdUpdate.Visible)
        {
            string CommandText = " SELECT   * FROM   (  SELECT   * FROM   (SELECT   * FROM   (SELECT   FIBER_CODE, FIBER_CAT, FIBER_DESC, Y_COUNT FROM   TX_FIBER_NEW_MASTER WHERE   FIBER_CAT = '" + Fiber_Category + "' AND NVL (QC_REQUIRED, 0) = '1') WHERE   FIBER_CODE IN (SELECT   FIBER_CODE FROM   FIBER_STANDARD_PARAMETER_MST m)) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR UPPER (FIBER_DESC) LIKE :SearchQuery) ORDER BY   FIBER_CODE) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = "%" + text.ToUpper() + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        }
        return dt.Rows.Count;
    }


    protected void ddlyarncode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            txtYarnCode.Text = ddlyarncode.SelectedText.ToString();
            txtNominalCount.Text = ddlyarncode.SelectedValue.ToString();
            BindNewMRNNum();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Fiber Code Selection.\r\nSee error log for detail."));

        }

    }

    protected void ddlTRNNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetReceiving(e.Text.ToUpper(), e.ItemsOffset, 10);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetReceivingCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Standard No selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetReceiving(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT  distinct a.TRN_NUMB, a.FIBER_CATEGORY, a.FIBER_CODE,(a.TRN_NUMB||'@'|| a.FIBER_CATEGORY||'@'|| a.FIBER_CODE||'@'||A.INWARD_TYPE||'@'||a.NOMINAL_COUNT) as combined FROM   FI_STANDARD_PARAMETER_MST a, FIBER_STANDARD_PARAMETER_TRN b WHERE A.BRANCH_CODE = B.BRANCH_CODE  AND A.COMP_CODE = B.COMP_CODE AND A.TRN_NUMB = B.TRN_NUMB AND A.FIBER_CATEGORY = B.FIBER_CATEGORY AND A.FIBER_CODE = B.FIBER_CODE AND A.INWARD_TYPE = B.INWARD_TYPE AND NVL (B.CONF_FLAG, 0) = '0' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery) OR UPPER (FIBER_CATEGORY) LIKE :SearchQuery  ORDER BY   FIBER_CODE) WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " and TRN_NUMB not in (SELECT TRN_NUMB from (SELECT  TRN_NUMB FROM   (  SELECT   * FROM   (SELECT  distinct a.TRN_NUMB, a.FIBER_CATEGORY, a.FIBER_CODE,(a.TRN_NUMB||'@'|| a.FIBER_CATEGORY||'@'|| a.FIBER_CODE||'@'||A.INWARD_TYPE||'@'||a.NOMINAL_COUNT) as combined FROM   FI_STANDARD_PARAMETER_MST a, FIBER_STANDARD_PARAMETER_TRN b WHERE A.BRANCH_CODE = B.BRANCH_CODE  AND A.COMP_CODE = B.COMP_CODE AND A.TRN_NUMB = B.TRN_NUMB AND A.FIBER_CATEGORY = B.FIBER_CATEGORY AND A.FIBER_CODE = B.FIBER_CODE AND A.INWARD_TYPE = B.INWARD_TYPE AND NVL (B.CONF_FLAG, 0) = '0' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "') WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery) OR UPPER (FIBER_CATEGORY) LIKE :SearchQuery  ORDER BY   FIBER_CODE) ) where rownum<='" + startOffset + "')";
            }

            string SortExpression = "  ORDER BY TRN_NUMB DESC ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetReceivingCount(string text)
    {
        try
        {
            string CommandText = "SELECT   * FROM   (  SELECT   * FROM   (SELECT  distinct a.TRN_NUMB, a.FIBER_CATEGORY, a.FIBER_CODE,(a.TRN_NUMB||'@'|| a.FIBER_CATEGORY||'@'|| a.FIBER_CODE||'@'||A.INWARD_TYPE||'@'||a.NOMINAL_COUNT) as combined FROM   FIBERQC_PARAMETER a, TX_FIBER_NEW_MASTER b WHERE A.BRANCH_CODE = B.BRANCH_CODE  AND A.COMP_CODE = B.COMP_CODE AND A.TRN_NUMB = B.TRN_NUMB AND A.FIBER_CATEGORY = B.FIBER_CATEGORY AND A.FIBER_CODE = B.FIBER_CODE AND A.INWARD_TYPE = B.INWARD_TYPE AND NVL (B.CONF_FLAG, 0) = '0' AND A.BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND A.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' ) WHERE   (UPPER (FIBER_CODE) LIKE :SearchQuery OR TRN_NUMB LIKE :SearchQuery) OR UPPER (FIBER_CATEGORY) LIKE :SearchQuery  ORDER BY   FIBER_CODE) ";
            string whereClause = string.Empty;

            string SortExpression = " ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "").Rows.Count;

        }
        catch
        {
            throw;
        }
    }

    protected void ddlTRNNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            string data = ddlTRNNumber.SelectedValue.Trim();
            string[] arr = data.Split('@');
            if (arr.Length > 0)
            {
                ddlyarncode.Items.Clear();
                txtTRNNUMBer.Text = arr[0].ToString();
                bindCategory("");
                ddlYarnCat.SelectedIndex = ddlYarnCat.Items.IndexOf(ddlYarnCat.Items.FindByText(arr[1].ToString()));
                DataTable dt = GetYarnData("", 0, arr[1].ToString());
                ddlyarncode.DataTextField = "FIBER_CODE";
                ddlyarncode.DataValueField = "FIBER_CAT";
                ddlyarncode.DataSource = dt;
                ddlyarncode.DataBind();
               
                foreach (ComboBoxItem item in ddlyarncode.Items)
                {
                    if (item.Text == arr[2].ToString())
                    {
                        ddlyarncode.SelectedIndex = ddlyarncode.Items.IndexOf(item);
                        break;
                    }
                }
                bindINWARDTYPE("");
                ddlInwardType.SelectedIndex = ddlInwardType.Items.IndexOf(ddlInwardType.Items.FindByText(arr[3].ToString()));
                txtNominalCount.Text = arr[4].ToString();
                double TRN_NUMB = 0;
                double.TryParse(txtTRNNUMBer.Text, out TRN_NUMB);
                txtYarnCode.Text = arr[2].ToString();
                SaitexDM.Common.DataModel.FIBERQC_PARAMETER oFIBERQC_PARAMETER = new SaitexDM.Common.DataModel.FIBERQC_PARAMETER();
                oFIBERQC_PARAMETER.FIBER_CATEGORY = ddlYarnCat.SelectedValue.Trim();
                oFIBERQC_PARAMETER.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oFIBERQC_PARAMETER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oFIBERQC_PARAMETER.FIBER_CODE = txtYarnCode.Text.Trim();
                oFIBERQC_PARAMETER.INWARD_TYPE = ddlInwardType.SelectedItem.Text;
                oFIBERQC_PARAMETER.TRN_NUMB = TRN_NUMB;
                dtDetailTBL = SaitexBL.Interface.Method.FIBERQC_PARAMETER.GetY_TRN_STANDARD_PARAMETER(oFIBERQC_PARAMETER);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                BindGridFromDataTable();
                CalculateTotal_Imperfection();
                ddlyarncode.Enabled = false;
                ddlYarnCat.Enabled = false;
                ddlInwardType.Enabled = false;
               
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fiber QC Standard  selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlUOM_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            bindUOM(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting UOM.\r\nSee error log for detail."));
        }
    }

    private void bindUOM(string SearchQuery)
    {
        try
        {
            DataTable dt = GET_MOM_DATA(SearchQuery, "UOM");
            ddlUOM.Items.Clear();
            ddlUOM.DataSource = dt;
            ddlUOM.DataTextField = "MST_CODE";
            ddlUOM.DataValueField = "MST_CODE";
            ddlUOM.DataBind();

        }
        catch
        {
            throw;
        }
    }


    protected void ddlStdType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlStdType.SelectedItem.Text == "SLUB LENGTH")
        {
            tdUom.Visible = true;
            tdheadUom.Visible = true;
        }
        else
        {
            tdUom.Visible = false;
            tdheadUom.Visible = false;
        }
    }
}

