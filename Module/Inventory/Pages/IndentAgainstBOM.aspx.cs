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
using Obout.ComboBox;
using Obout.Interface;

public partial class Module_Inventory_Pages_IndentAgainstBOM : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private  string IND_TYPE = "BOM";
    private DataTable dtIndentDetail = null;
    private static double FinalTotal;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);
            ActivateSaveMode();
            lblMode.Text = "Save";
            FinalTotal = 0;
            txtCommentComment.Text = "";
            txtDepartment.Text = "";
            txtIndentDate.Text = "";
            txtIndentNumber.Text = "";
            txtPreparedBy.Text = "";
            txtRequiredBefore.Text = "";

            txtIndentNumber.Text = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetNewIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE); //FindMaxIndentNumber();

            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();

            dtIndentDetail.Rows.Clear();
            BindInitialData();

            BindIndentDetailGrid();

            //DataTable data = new DataTable();
            //data = GetItems("");
            //txtItemCode.DataSource = data;
            //txtItemCode.DataTextField = "ITEM_CODE";
            //txtItemCode.DataValueField = "ITEM_CODE";
            //txtItemCode.DataBind();

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;

            txtIndentNumber.ReadOnly = true;
            txtIndentNumber.AutoPostBack = false;
            ddlIndentNumber.Visible = false;
            txtIndentNumber.Visible = true;

            Session["dtBOMIndent"] = null;
            ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
            ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
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
            DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dtDepartment != null && dtDepartment.Rows.Count > 0)
            {
                ddl.DataSource = dtDepartment;
                ddl.DataTextField = "DEPT_NAME";
                ddl.DataValueField = "DEPT_NAME";
                ddl.DataBind();
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    private void CreateIndentDetailTable()
    {
        try
        {
            dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("UniqueId", typeof(int));
            dtIndentDetail.Columns.Add("IndentDetailNumber", typeof(int));
            dtIndentDetail.Columns.Add("IndentNumber", typeof(string));
            dtIndentDetail.Columns.Add("ITEM_CODE", typeof(string));
            dtIndentDetail.Columns.Add("ITEM_DESC", typeof(string));
            dtIndentDetail.Columns.Add("currentStock", typeof(double));
            dtIndentDetail.Columns.Add("MIN_STOCK_LVL", typeof(double));
            dtIndentDetail.Columns.Add("Min_Procure_days", typeof(double));
            dtIndentDetail.Columns.Add("OP_RATE", typeof(double));
            dtIndentDetail.Columns.Add("RQST_QTY", typeof(double));
            dtIndentDetail.Columns.Add("VC_UNITNAME", typeof(string));
            dtIndentDetail.Columns.Add("Amount", typeof(double));
            dtIndentDetail.Columns.Add("DPT_REMARK", typeof(string));
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

    private void BindInitialData()
    {
        try
        {
            txtIndentDate.Text = DateTime.Now.Date.ToShortDateString();
            txtRequiredBefore.Text = DateTime.Now.Date.ToShortDateString();

            txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
            txtPreparedBy.Text = oUserLoginDetail.Username;
            txtIndentDate.Text = DateTime.Now.ToShortDateString();
            txtRequiredBefore.Text = DateTime.Now.ToShortDateString();

        }
        catch
        {
            throw;
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
        catch
        {
            throw;
        }
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

    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            ComboBox thisTextBox = (ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetItems(e.Text);
            thisTextBox.DataSource = data;
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item loading.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetItems(string text)
    {
        try
        {
            string whereClause = " WHERE ITEM_CODE like :SearchQuery or ITEM_TYPE like :SearchQuery or ITEM_DESC like :SearchQuery";
            string sortExpression = " ORDER BY ITEM_CODE";
            string commandText = "SELECT * FROM ( select * from TX_ITEM_MST where rownum<=25 ) asd ";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void Item_LOV_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            ComboBox thisTextBox = (ComboBox)txtItemCode;

            string Description = "";
            string UOM = "";
            double CurrentStock = 0;
            double MinStockLevel = 0;
            double OpeningRate = 0;
            double Min_Procure_days = 0;
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchItemCodeInGrid(thisTextBox.SelectedValue.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.SelectedValue.Trim()), out Description, out UOM, out CurrentStock, out MinStockLevel, out OpeningRate, out Min_Procure_days);

                if (iRecordFound > 0)
                {
                    txtOpeningRate.Text = OpeningRate.ToString();
                    txtItemDesc.Text = Description;
                    txtCurrentStock.Text = CurrentStock.ToString();
                    txtMinStockLevel.Text = MinStockLevel.ToString();
                    txtUnitName.Text = UOM;
                    txtRequestQty.Focus();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
        }
    }

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdIndentDetail.Rows)
            {
                Label txtItemCode1 = (Label)grdRow.FindControl("txtItemCode");
                OboutButton lnkDelete = (OboutButton)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtItemCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private int GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out double CurrentStock, out double MinStockLevel, out double OpeningRate, out double Min_Procure_days)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "";
        CurrentStock = 0;
        MinStockLevel = 0;
        OpeningRate = 0;
        Min_Procure_days = 0;
        try
        {
            DataTable dts = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemDetailByItemCode(ItemCode, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.CH_BRANCHCODE,"","");
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["ITEM_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();
                double.TryParse(dts.Rows[0]["currentStock"].ToString().Trim(), out CurrentStock);
                double.TryParse(dts.Rows[0]["MIN_STOCK_LVL"].ToString().Trim(), out MinStockLevel);
                double.TryParse(dts.Rows[0]["OP_RATE"].ToString().Trim(), out OpeningRate);
                double.TryParse(dts.Rows[0]["MIN_PROCURE_DAYS"].ToString().Trim(), out Min_Procure_days);
                iRecordFound = dts.Rows.Count;
            }
            return iRecordFound;
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
            txtItemCode.SelectedIndex = -1;
            txtItemDesc.Text = "";
            txtMinStockLevel.Text = "";
            txtOpeningRate.Text = "";
            txtRemark.Text = "";
            txtRequestQty.Text = "";
            txtAmount.Text = "";
            txtCurrentStock.Text = "";
            txtUnitName.Text = "";

            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void btnAdjBOM_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtItemCode.SelectedValue != "" || txtItemCode.SelectedIndex != -1)
            {
                string URL = "AdjBOM_INDENT.aspx";
                URL = URL + "?ItemCodeId=" + txtItemCode.SelectedValue.Trim();
                URL = URL + "&TextBoxId=" + txtRequestQty.ClientID;

                URL = URL + "&IND_NUMB=" + txtIndentNumber.Text.Trim();
                IND_TYPE = "BOM";
                URL = URL + "&IND_TYPE=" + IND_TYPE;

                txtRequestQty.ReadOnly = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=650,height=380,left=200,top=300');", true);
            }
            else
            {
                CommonFuction.ShowMessage("Please select Item to adjust Indent");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting adjustment.\r\nSee error log for detail."));
        }

    }

    protected void txtRequestQty_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtRequestQty;

            thisTextBox.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in entering quantity .\r\nSee error log for detail."));
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
            if (dtIndentDetail.Rows.Count < 15)
            {
                if (txtItemCode.SelectedText != "" && txtRequestQty.Text != "" && txtOpeningRate.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    bool bb = SearchItemCodeInGrid(txtItemCode.SelectedValue.Trim(), UniqueId);
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
                                    dv[0]["ITEM_CODE"] = txtItemCode.SelectedValue.Trim();
                                    dv[0]["ITEM_DESC"] = txtItemDesc.Text.Trim();
                                    dv[0]["currentStock"] = double.Parse(txtCurrentStock.Text.Trim());
                                    dv[0]["MIN_STOCK_LVL"] = double.Parse(txtMinStockLevel.Text.Trim());
                                    dv[0]["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                    dv[0]["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                    dv[0]["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["DPT_REMARK"] = txtRemark.Text.Trim();
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
                                dr["ITEM_CODE"] = txtItemCode.SelectedValue.Trim();
                                dr["ITEM_DESC"] = txtItemDesc.Text.Trim();
                                dr["currentStock"] = double.Parse(txtCurrentStock.Text.Trim());
                                dr["MIN_STOCK_LVL"] = double.Parse(txtMinStockLevel.Text.Trim());
                                dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                dr["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                dr["Min_Procure_days"] = double.Parse(lblMin_Procure_days.Text.Trim());
                                dr["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                dr["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dr["DPT_REMARK"] = txtRemark.Text.Trim();
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
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                    }
                }
                else if (txtItemCode.SelectedText == "")
                {
                    CommonFuction.ShowMessage("Item Code Required");
                }
                else if (txtRequestQty.Text == "")
                {
                    CommonFuction.ShowMessage("Quantity can not be zero");
                }

                ViewState["dtIndentDetail"] = dtIndentDetail;

                BindIndentDetailGrid();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Indent.");
            }
            getReqDate();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving item detail row.\r\nSee error log for detail."));
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing item detail row.\r\nSee error log for detail."));
        }
    }

    protected void grdIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
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
                txtItemCode.SelectedValue = dv[0]["ITEM_CODE"].ToString();
                txtItemDesc.Text = dv[0]["ITEM_DESC"].ToString();
                txtCurrentStock.Text = dv[0]["currentStock"].ToString();
                txtMinStockLevel.Text = dv[0]["MIN_STOCK_LVL"].ToString();
                txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                txtRequestQty.Text = dv[0]["RQST_QTY"].ToString();
                txtUnitName.Text = dv[0]["VC_UNITNAME"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtRemark.Text = dv[0]["DPT_REMARK"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
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
        catch
        {
            throw;
        }
    }

    private bool FillDataTableByGrid()
    {
        try
        {
            bool result = true;
            if (grdIndentDetail.Rows.Count > 0)
            {
                if (ViewState["dtIndentDetail"] != null)
                    dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

                dtIndentDetail.Rows.Clear();
                FinalTotal = 0;
                foreach (GridViewRow grdRow in grdIndentDetail.Rows)
                {
                    ComboBox txtItemCode = (ComboBox)grdRow.FindControl("txtItemCode");
                    TextBox txtRequestQty = (TextBox)grdRow.FindControl("txtRequestQty");
                    Label txtOpeningRate = (Label)grdRow.FindControl("txtOpeningRate");
                    Label txtAmount = (Label)grdRow.FindControl("txtAmount");
                    Label txtItemDesc = (Label)grdRow.FindControl("txtItemDesc");
                    Label txtCurrentStock = (Label)grdRow.FindControl("txtCurrentStock");
                    Label txtMinStockLevel = (Label)grdRow.FindControl("txtMinStockLevel");
                    Label txtUnitName = (Label)grdRow.FindControl("txtUnitName");
                    TextBox txtRemark = (TextBox)grdRow.FindControl("txtRemark");
                    TextBox txtIndentDetailNumber = (TextBox)grdRow.FindControl("txtIndentDetailNumber");
                    if (txtItemCode.SelectedText != "" && txtRequestQty.Text != "" && txtOpeningRate.Text != "" && txtAmount.Text != "")
                    {
                        double Qty = 0;
                        double.TryParse(txtRequestQty.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            DataRow dr = dtIndentDetail.NewRow();
                            dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                            dr["IndentDetailNumber"] = int.Parse(txtIndentDetailNumber.Text.Trim());
                            dr["IndentNumber"] = txtIndentNumber.Text;
                            dr["ITEM_CODE"] = txtItemCode.SelectedValue.Trim();
                            dr["ITEM_DESC"] = txtItemDesc.Text.Trim();
                            dr["currentStock"] = double.Parse(txtCurrentStock.Text.Trim());
                            dr["MIN_STOCK_LVL"] = double.Parse(txtMinStockLevel.Text.Trim());
                            dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                            dr["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                            dr["VC_UNITNAME"] = txtUnitName.Text.Trim();
                            dr["Amount"] = double.Parse(txtAmount.Text.Trim());
                            dr["DPT_REMARK"] = txtRemark.Text.Trim();
                            FinalTotal = FinalTotal + double.Parse(txtAmount.Text.Trim());
                            dtIndentDetail.Rows.Add(dr);
                        }
                        else
                        {
                            result = false;
                        }
                    }
                    ViewState["dtIndentDetail"] = dtIndentDetail;
                }
            }
            return result;
        }
        catch
        {
            throw;
        }
    }
    //code modified by Deepak Kumar Das on 08/04/2012----------//
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (grdIndentDetail.Rows.Count==0)
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
        }
    }

    protected void SaveIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_IND_MST oTX_ITEM_IND_MST = new SaitexDM.Common.DataModel.TX_ITEM_IND_MST();
            oTX_ITEM_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IND_MST.IND_TYPE = IND_TYPE;
            oTX_ITEM_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oTX_ITEM_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oTX_ITEM_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oTX_ITEM_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oTX_ITEM_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oTX_ITEM_IND_MST.STATUS = true;
            oTX_ITEM_IND_MST.TUSER = oUserLoginDetail.UserCode;

            oTX_ITEM_IND_MST.LOCATION = ddlLocation.SelectedItem.ToString();
            oTX_ITEM_IND_MST.STORE = ddlStore.SelectedItem.ToString();
            int Ind_numb = 0;

            DataTable dtBOMIndent = (DataTable)Session["dtBOMIndent"];

            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

            bool Result = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Insert(oTX_ITEM_IND_MST, dtIndentDetail, out Ind_numb, dtBOMIndent);
            if (Result)
            {
                InitialisePage();
                string msg = "Indent Number " + Ind_numb + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent saving Failed');", true);
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
            txtIndentNumber.Text = "";
            lblMode.Text = "Update";

            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = false;

            RefreshDetailRow();

            ddlIndentNumber.Visible = true;
            txtIndentNumber.Visible = false;
            ActivateUpdateMode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in activating update mode.\r\nSee error log for detail."));
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

    protected void ddlIndentNumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();

            DataTable data = new DataTable();
            data = GetIndents(e.Text.ToUpper());

            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "IND_NUMB";
            thisTextBox.DataValueField = "IND_NUMB";
            thisTextBox.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading indent for updation.\r\nSee error log for detail."));
        }
    }

    protected DataTable GetIndents(string text)
    {
        try
        {
            string whereClause = "  where Ind_numb like:searchQuery";
            string sortExpression = " order by ind_numb desc, ind_date desc";
            string commandText = "select * from (Select a.IND_NUMB, a.IND_DATE from TX_ITEM_IND_MST a Where A.COMP_CODE=:COMP_CODE AND A.BRANCH_CODE=:BRANCH_CODE AND A.IND_TYPE=:IND_TYPE and a.YEAR=" + oUserLoginDetail.DT_STARTDATE.Year + " and a.dept_code='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, commandText, whereClause, sortExpression, "", text + "%");

            return dt;
        }
        catch
        {
            throw;
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
                txtIndentNumber.Focus();
                txtIndentNumber.Text = "";
            }
            else
            {
                txtIndentNumber.Text = IndentNo;

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading indent data for updation.\r\nSee error log for detail."));
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
        }
    }

    private int GetdataByIndentNumber(string IndentNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dts = SaitexBL.Interface.Method.TX_ITEM_IND_MST.SelectByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(IndentNumber), oUserLoginDetail.VC_DEPARTMENTCODE);

            if (dts != null && dts.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtCommentComment.Text = dts.Rows[0]["CONF_COMMENT"].ToString().Trim();
                txtIndentDate.Text = DateTime.Parse(dts.Rows[0]["IND_DATE"].ToString().Trim()).ToShortDateString();
                txtPreparedBy.Text = dts.Rows[0]["PREP_BY"].ToString().Trim();
                txtRequiredBefore.Text = DateTime.Parse(dts.Rows[0]["REQD_DATE"].ToString().Trim()).ToShortDateString();
                txtDepartment.Text = dts.Rows[0]["DEPT_NAME"].ToString().Trim();
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByText(dts.Rows[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByText(dts.Rows[0]["STORE"].ToString()));
            }

            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetItemIndentTrasaction(IndentNumber.Trim());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    DataTable dtBOMIndent = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetAdjBOMByIND(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(IndentNumber));

                    Session["dtBOMIndent"] = dtBOMIndent;

                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Indent already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtIndentNumber.ReadOnly = false;
                    txtIndentNumber.AutoPostBack = true;
                    txtIndentNumber.Text = "";
                    txtIndentNumber.Focus();

                    lblMode.Text = "Update";

                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                    tdDelete.Visible = false;

                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            return iRecordFound;
        }
    }

    private DataTable GetItemIndentTrasaction(string strIndentNum)
    {
        try
        {
            DataTable dtTemp = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Select_TransactionByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(strIndentNum));

            return dtTemp;
        }
        catch
        {
            throw;
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
                    if (drTemp["YEAR"].ToString().Equals(oUserLoginDetail.DT_STARTDATE.Year.ToString()))
                    {
                        DataRow dr = dtIndentDetail.NewRow();
                        dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                        dr["IndentDetailNumber"] = 0;
                        dr["IndentNumber"] = drTemp["IND_NUMB"];
                        dr["ITEM_CODE"] = drTemp["ITEM_CODE"];
                        dr["ITEM_DESC"] = drTemp["ITEM_DESC"];
                        dr["currentStock"] = drTemp["currentStock"];
                        dr["MIN_STOCK_LVL"] = drTemp["MIN_STOCK_LVL"];
                        dr["OP_RATE"] = drTemp["OP_RATE"];
                        dr["RQST_QTY"] = drTemp["RQST_QTY"];
                        dr["VC_UNITNAME"] = drTemp["UNIT_NAME"];
                        dr["Amount"] = drTemp["iValue"];
                        dr["DPT_REMARK"] = drTemp["DPT_REMARK"];
                        FinalTotal = FinalTotal + double.Parse(drTemp["iValue"].ToString());
                        dtIndentDetail.Rows.Add(dr);
                    }
                }
                dtTemp = null;

                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating page.\r\nSee error log for detail."));
        }
    }

    protected void UpdateIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_ITEM_IND_MST oTX_ITEM_IND_MST = new SaitexDM.Common.DataModel.TX_ITEM_IND_MST();
            oTX_ITEM_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_ITEM_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_ITEM_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_ITEM_IND_MST.IND_TYPE = IND_TYPE;
            oTX_ITEM_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oTX_ITEM_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oTX_ITEM_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_ITEM_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oTX_ITEM_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oTX_ITEM_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oTX_ITEM_IND_MST.STATUS = true;
            oTX_ITEM_IND_MST.TUSER = oUserLoginDetail.UserCode;

            oTX_ITEM_IND_MST.LOCATION = ddlLocation.SelectedItem.ToString();
            oTX_ITEM_IND_MST.STORE = ddlStore.SelectedItem.ToString();
            DataTable dtBOMIndent = (DataTable)Session["dtBOMIndent"];

            if (ViewState["dtIndentDetail"] != null)
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];

            bool Result = SaitexBL.Interface.Method.TX_ITEM_IND_MST.Update(oTX_ITEM_IND_MST, dtIndentDetail, dtBOMIndent);
            if (Result)
            {
                InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updation Failed');", true);
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "PrintIndent.aspx";
            Response.Redirect(URL);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page refresh.\r\nSee error log for detail."));
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

    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting indent.\r\nSee error log for detail."));
        }
    }

}
