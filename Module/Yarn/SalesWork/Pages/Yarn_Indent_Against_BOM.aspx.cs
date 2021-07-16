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


public partial class Module_Yarn_SalesWork_Pages_Yarn_Indent_Against_BOM : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string IND_TYPE = string.Empty;
    private DataTable dtIndentDetail = null;
    private double FinalTotal=0;

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
            IND_TYPE = "BOM";
            ViewState["IND_TYPE"] = IND_TYPE;
            ActivateSaveMode();
            lblMode.Text = "Save";

            FinalTotal = 0;
            ViewState["FinalTotal"] = FinalTotal;
            txtCommentComment.Text = "";
            txtDepartment.Text = "";
            txtIndentDate.Text = "";
            txtIndentNumber.Text = "";
            txtPreparedBy.Text = "";
            txtRequiredBefore.Text = "";
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            txtIndentNumber.Text = SaitexBL.Interface.Method.YRN_INT_MST.GetNewIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE,oUserLoginDetail.DT_STARTDATE.Year); //FindMaxIndentNumber();

            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();

            dtIndentDetail.Rows.Clear();
            BindInitialData();

            BindIndentDetailGrid();

            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;

            txtIndentNumber.ReadOnly = true;
            txtIndentNumber.AutoPostBack = false;
            ddlIndentNumber.Visible = false;
            txtIndentNumber.Visible = true;

            Session["dtBOMIndent"] = null;
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
            dtIndentDetail.Columns.Add("YARN_CODE", typeof(string));
            dtIndentDetail.Columns.Add("SHADE_CODE", typeof(string));
            dtIndentDetail.Columns.Add("YARN_DESC", typeof(string));
            dtIndentDetail.Columns.Add("currentStock", typeof(double));
            dtIndentDetail.Columns.Add("MIN_STOCK_LVL", typeof(double));
            dtIndentDetail.Columns.Add("Min_Procure_days", typeof(double));
            dtIndentDetail.Columns.Add("OP_RATE", typeof(double));
            dtIndentDetail.Columns.Add("RQST_QTY", typeof(double));
            dtIndentDetail.Columns.Add("VC_UNITNAME", typeof(string));
            dtIndentDetail.Columns.Add("Amount", typeof(double));
            dtIndentDetail.Columns.Add("DPT_REMARK", typeof(string));
            ViewState["dtIndentDetail"] = dtIndentDetail;

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
            double FinalTotal1 = 0;
            //if (ViewState["FinalTotal"] != null)
            //{
            //    FinalTotal = (double)ViewState["FinalTotal"];
            //}
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            grdIndentDetail.DataSource = dtIndentDetail;
            grdIndentDetail.DataBind();


            foreach (GridViewRow row in grdIndentDetail.Rows)
            {
                Label lblAmount = (Label)row.FindControl("lblAmount");
                FinalTotal1 = FinalTotal1 + double.Parse(lblAmount.Text.Trim());
            }
            if (grdIndentDetail.Rows.Count > 0)
            {
                Label lblFooterAmount = (Label)grdIndentDetail.FooterRow.FindControl("lblFooterAmount");
                lblFooterAmount.Text = FinalTotal1.ToString();
            }
            getReqDate();
            AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
            lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal1);
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
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            if (dtIndentDetail != null && dtIndentDetail.Rows.Count > 0)
            {
                DateTime Temp = System.DateTime.Now.Date;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    Int64 Min_Procure_Days = 0;
                    Int64.TryParse(dr["Min_Procure_days"].ToString(), out Min_Procure_Days);
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

    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdIndentDetail.Rows)
            {
                Label txtYarnCode1 = (Label)grdRow.FindControl("txtItemCode");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtYarnCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId)
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
            DataTable dts = SaitexBL.Interface.Method.YRN_INT_MST.GetItemDetailByItemCode(oUserLoginDetail.DT_STARTDATE.Year, ItemCode, oUserLoginDetail.CH_BRANCHCODE, "", "", "", "");
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["YARN_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();
                double.TryParse(dts.Rows[0]["currentStock"].ToString().Trim(), out CurrentStock);
                double.TryParse(dts.Rows[0]["MIN_STOCK"].ToString().Trim(), out MinStockLevel);
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
            ddlYarnCode.SelectedIndex = -1;
            txtYarnCode.Text = "";
            txtYarnDesc.Text = "";
            txtMinStockLevel0.Text = "";
            txtOpeningRate.Text = "";
            txtRemark.Text = "";
            txtRequestQty.Text = "";
            txtAmount.Text = "";
            txtCurrentStock0.Text = "";
            txtUnitName.Text = "";

            ddlYarnCode.Enabled = true;
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
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            if (txtYarnCode.Text.ToString() != "")
            {
                string URL = "AdjBOM_INDENT.aspx";
                URL = URL + "?ItemCodeId=" + txtYarnCode.Text.Trim();
                URL = URL + "&TextBoxId=" + txtRequestQty.ClientID;

                URL = URL + "&IND_NUMB=" + txtIndentNumber.Text.Trim();
                IND_TYPE = "BOM";
                URL = URL + "&IND_TYPE=" + IND_TYPE;

                txtRequestQty.ReadOnly = false;
                txtRequestQty.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=900,height=450,left=200,top=300');", true);
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
            //if (ViewState["FinalTotal"] != null)
            //{
            //    FinalTotal = (double)ViewState["FinalTotal"];
            //}
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            if (dtIndentDetail.Rows.Count < 15)
            {
                if (txtYarnCode.Text.ToString() != "" && txtRequestQty.Text != "" && txtOpeningRate.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    bool bb = SearchItemCodeInGrid(txtYarnCode.Text.Trim(), UniqueId);
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
                                    dv[0]["YARN_CODE"] = txtYarnCode.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtYarnDesc.Text.Trim();
                                    dv[0]["currentStock"] = double.Parse(txtCurrentStock0.Text.Trim());
                                    dv[0]["MIN_STOCK_LVL"] = double.Parse(txtMinStockLevel0.Text.Trim());
                                    dv[0]["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                    dv[0]["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                    dv[0]["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["DPT_REMARK"] = txtRemark.Text.Trim();
                                    dv[0]["SHADE_CODE"] = txtShadeCode.Text.ToString();
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
                                dr["YARN_CODE"] = txtYarnCode.Text.Trim();
                                dr["YARN_DESC"] = txtYarnDesc.Text.Trim();
                                dr["currentStock"] = double.Parse(txtCurrentStock0.Text.Trim());
                                dr["MIN_STOCK_LVL"] = double.Parse(txtMinStockLevel0.Text.Trim());
                                dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                dr["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                dr["Min_Procure_days"] = double.Parse(lblMin_Procure_days.Text.Trim());
                                dr["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                dr["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dr["DPT_REMARK"] = txtRemark.Text.Trim();
                                dr["SHADE_CODE"] = txtShadeCode.Text.ToString();
                                FinalTotal = FinalTotal + double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dtIndentDetail.Rows.Add(dr);
                                lblMin_Procure_days.Text = "";
                            }

                            ViewState["FinalTotal"] = FinalTotal;
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
                else if (txtYarnCode.Text.ToString() == "")
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
            //RefreshDetailRow();
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
        }
    }

    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            DataView dv = new DataView(dtIndentDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                txtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                //txtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                txtYarnDesc.Text = dv[0]["YARN_DESC"].ToString();
                txtCurrentStock0.Text = dv[0]["currentStock"].ToString();
               txtMinStockLevel0.Text = dv[0]["MIN_STOCK_LVL"].ToString();
                txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                txtRequestQty.Text = dv[0]["RQST_QTY"].ToString();
                txtUnitName.Text = dv[0]["VC_UNITNAME"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtRemark.Text = dv[0]["DPT_REMARK"].ToString();
                txtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();

                ddlYarnCode.Enabled = false;
                ddlYarnCode.SelectedText=(dv[0]["YARN_CODE"].ToString());
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
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
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
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
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
            if (ViewState["FinalTotal"] != null)
            {
                FinalTotal = (double)ViewState["FinalTotal"];
            }

            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            bool result = true;
            if (grdIndentDetail.Rows.Count > 0)
            {
                dtIndentDetail.Rows.Clear();

                foreach (GridViewRow grdRow in grdIndentDetail.Rows)
                {
                    DropDownList txtYarnCode = (DropDownList)grdRow.FindControl("txtYarnCode");
                    TextBox txtRequestQty = (TextBox)grdRow.FindControl("txtRequestQty");
                    Label txtOpeningRate = (Label)grdRow.FindControl("txtOpeningRate");
                    Label txtAmount = (Label)grdRow.FindControl("txtAmount");
                    Label txtYarnDesc = (Label)grdRow.FindControl("txtYarnDesc");
                    Label txtCurrentStock = (Label)grdRow.FindControl("txtCurrentStock");
                    Label txtMinStockLevel = (Label)grdRow.FindControl("txtMinStockLevel");
                    Label txtUnitName = (Label)grdRow.FindControl("txtUnitName");
                    TextBox txtRemark = (TextBox)grdRow.FindControl("txtRemark");
                    TextBox txtIndentDetailNumber = (TextBox)grdRow.FindControl("txtIndentDetailNumber");
                    if (txtYarnCode.SelectedItem.ToString() != "SELECT" && txtRequestQty.Text != "" && txtOpeningRate.Text != "" && txtAmount.Text != "")
                    {
                        double Qty = 0;
                        double.TryParse(txtRequestQty.Text.Trim(), out Qty);
                        if (Qty > 0)
                        {
                            DataRow dr = dtIndentDetail.NewRow();
                            dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                            dr["IndentDetailNumber"] = int.Parse(txtIndentDetailNumber.Text.Trim());
                            dr["IndentNumber"] = txtIndentNumber.Text;
                            dr["YARN_CODE"] = txtYarnCode.Text.Trim();
                            dr["SHADE_CODE"] = txtShadeCode.Text.Trim();
                            dr["YARN_DESC"] = txtYarnDesc.Text.Trim();
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
                }
                ViewState["FinalTotal"] = FinalTotal;
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
            return result;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }

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
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));
        }
    }

    protected void SaveIndentData()
    {
        try
        {
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            SaitexDM.Common.DataModel.YRN_IND_MST oYRN_IND_MST = new SaitexDM.Common.DataModel.YRN_IND_MST();
            oYRN_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IND_MST.IND_TYPE = IND_TYPE;
            oYRN_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oYRN_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oYRN_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oYRN_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oYRN_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oYRN_IND_MST.STATUS = true;
            oYRN_IND_MST.TUSER = oUserLoginDetail.UserCode;

            int Ind_numb = 0;

            DataTable dtBOMIndent = (DataTable)Session["dtBOMIndent"];

            bool Result = SaitexBL.Interface.Method.YRN_INT_MST.Insert(oYRN_IND_MST, dtIndentDetail, out Ind_numb, dtBOMIndent);
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
            //txtIndentNumber.Text = "";
            lblMode.Text = "Update";

            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = false;

            RefreshDetailRow();

            ddlIndentNumber.Visible = true;
            txtIndentNumber.Visible = true;
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

            ddlIndentNumber.SelectedIndex = -1;
            ddlIndentNumber.Items.Clear();
            txtIndentNumber.Visible = false;
            BindIndentNumber();
        }
        catch
        {
            throw;
        }
    }

    private void BindIndentNumber()
    {
        try
        {

            DataTable data = new DataTable();
            data = GetIndents(string.Empty);

            ddlIndentNumber.DataSource = data;
            ddlIndentNumber.DataTextField = "IND_NUMB";
            ddlIndentNumber.DataValueField = "IND_NUMB";
            ddlIndentNumber.DataBind();
            ddlIndentNumber.Items.Insert(0, new ListItem("------Select------", "0"));
            //txtIndentNumber.Text = "2";
        }
        catch
        {

        }

    }

    protected DataTable GetIndents(string text)
    {
        try
        {
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            string whereClause = "  where Ind_numb like:searchQuery";
            string sortExpression = " order by ind_numb desc, ind_date desc";
            string commandText = "select * from (Select a.IND_NUMB, a.IND_DATE from YRN_IND_MST a Where A.COMP_CODE=:COMP_CODE AND A.BRANCH_CODE=:BRANCH_CODE AND A.IND_TYPE=:IND_TYPE and a.dept_code='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, commandText, whereClause, sortExpression, "", text + "%");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected void txtIndentNumber_TextChanged(object sender, EventArgs e)
    {

    }

    private int GetdataByIndentNumber(string IndentNumber)
    {
        int iRecordFound = 0;
        try
        {
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            DataTable dts = SaitexBL.Interface.Method.YRN_INT_MST.SelectByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(IndentNumber), oUserLoginDetail.VC_DEPARTMENTCODE,oUserLoginDetail.DT_STARTDATE.Year);

            if (dts != null && dts.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtCommentComment.Text = dts.Rows[0]["CONF_COMMENT"].ToString().Trim();
                txtIndentDate.Text = DateTime.Parse(dts.Rows[0]["IND_DATE"].ToString().Trim()).ToShortDateString();
                txtPreparedBy.Text = dts.Rows[0]["PREP_BY"].ToString().Trim();
                txtRequiredBefore.Text = DateTime.Parse(dts.Rows[0]["REQD_DATE"].ToString().Trim()).ToShortDateString();
                txtDepartment.Text = dts.Rows[0]["DEPT_NAME"].ToString().Trim();
                txtIndentNumber.Text = IndentNumber;
            }

            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetItemIndentTrasaction(IndentNumber.Trim());
                if (dtTemp != null && dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    DataTable dtBOMIndent = SaitexBL.Interface.Method.YRN_INT_MST.GetAdjBOMByIND(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(IndentNumber));

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
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            DataTable dtTemp = SaitexBL.Interface.Method.YRN_INT_MST.Select_TransactionByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, int.Parse(strIndentNum), oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, IND_TYPE);

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
            if (ViewState["FinalTotal"] != null)
            {
                FinalTotal = (double)ViewState["FinalTotal"];
            }
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }

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
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["SHADE_CODE"] = drTemp["SHADE_CODE"];
                    dr["YARN_DESC"] = drTemp["YARN_DESC"];
                    dr["currentStock"] = drTemp["currentStock"];
                    dr["MIN_STOCK_LVL"] = drTemp["MIN_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["RQST_QTY"] = drTemp["RQST_QTY"];
                    dr["VC_UNITNAME"] = drTemp["UNIT_NAME"];
                    dr["Amount"] = drTemp["iValue"];
                    dr["DPT_REMARK"] = drTemp["DPT_REMARK"];
                    FinalTotal = FinalTotal + double.Parse(drTemp["iValue"].ToString());
                    dtIndentDetail.Rows.Add(dr);

                }
                ViewState["FinalTotal"] = FinalTotal;
                ViewState["dtIndentDetail"] = dtIndentDetail;
                dtTemp = null;

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
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }

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
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            SaitexDM.Common.DataModel.YRN_IND_MST oYRN_INT_MST = new SaitexDM.Common.DataModel.YRN_IND_MST();
            oYRN_INT_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_INT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_INT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_INT_MST.IND_TYPE = IND_TYPE;
            oYRN_INT_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(ddlIndentNumber.SelectedValue));
            oYRN_INT_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oYRN_INT_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_INT_MST.PREP_BY = oUserLoginDetail.UserCode;
            oYRN_INT_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oYRN_INT_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oYRN_INT_MST.STATUS = true;
            oYRN_INT_MST.TUSER = oUserLoginDetail.UserCode;

            DataTable dtBOMIndent = (DataTable)Session["dtBOMIndent"];

            bool Result = SaitexBL.Interface.Method.YRN_INT_MST.Update(oYRN_INT_MST, dtIndentDetail, dtBOMIndent);
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

    protected void ddlIndentNumber_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["FinalTotal"] != null)
            {
                FinalTotal = (double)ViewState["FinalTotal"];
            }
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }

            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
            {
                CreateIndentDetailTable();
            }
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByIndentNumber(CommonFuction.funFixQuotes(ddlIndentNumber.SelectedValue));
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

    protected void ddlYarnCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
 {
        try
        {
            DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);

            ddlYarnCode.Items.Clear();

            ddlYarnCode.DataSource = data;
            //ddlYarnCode.DataTextField = "BASE_ARTICAL_CODE";
            //ddlYarnCode.DataValueField = "BASE_SHADE_CODE";
            ddlYarnCode.DataTextField = "ARTICAL_CODE";
            ddlYarnCode.DataValueField = "SHADE_CODE";
            ddlYarnCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetYarnCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetYarnData(string Text, int startOffset)
   {
        try
        {
            //string CommandText = "SELECT * FROM (SELECT * FROM (SELECT T.PRTY_CODE, T.PI_NO, T.BASE_ARTICAL_CODE, T.BASE_ARTICAL_DESC, T.BASE_SHADE_CODE, (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) AS rem_qty, ( t.PRTY_CODE|| '@'|| T.PI_NO|| '@'|| T.BASE_ARTICAL_CODE|| '@'|| T.BASE_ARTICAL_DESC|| '@'|| T.BASE_SHADE_CODE) AS YARN_DATA FROM V_OD_CAPT_TRN_BOM T,YRN_MST Y WHERE Y.YARN_CODE=T.BASE_ARTICAL_CODE AND T.comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND T.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) > 0 AND T.BOM_FLAG = '1' AND T.FINAL_ORDER_CONF_CLAG = '1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR BASE_ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery ORDER BY BASE_ARTICAL_CODE) WHERE ROWNUM <= 15 ";
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT T.PRTY_CODE, T.PI_NO, T.ARTICAL_CODE, T.ARTICAL_DESC, T.SHADE_CODE, (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) AS rem_qty, ( t.PRTY_CODE|| '@'|| T.PI_NO|| '@'|| T.ARTICAL_CODE|| '@'|| T.ARTICAL_DESC|| '@'|| T.BASE_SHADE_CODE) AS YARN_DATA FROM V_OD_CAPT_TRN_BOM T,YRN_MST Y WHERE Y.YARN_CODE=T.ARTICAL_CODE AND T.comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND T.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) > 0 AND T.BOM_FLAG = '1' AND T.FINAL_ORDER_CONF_CLAG = '1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery ORDER BY ARTICAL_CODE) WHERE ROWNUM <= 15 ";
            
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                //whereClause += " AND YARN_DATA NOT IN(SELECT YARN_DATA FROM (SELECT * FROM (SELECT PRTY_CODE,PI_NO,BASE_ARTICAL_CODE,BASE_ARTICAL_DESC,BASE_SHADE_CODE,(NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) AS rem_qty,( PRTY_CODE || '@' || PI_NO || '@' || BASE_ARTICAL_CODE || '@' || BASE_ARTICAL_DESC || '@' || BASE_SHADE_CODE) AS YARN_DATA FROM V_OD_CAPT_TRN_BOM WHERE comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) > 0 AND BOM_FLAG = '1' AND FINAL_ORDER_CONF_CLAG ='1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR BASE_ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery ORDER BY BASE_ARTICAL_CODE) WHERE ROWNUM <= '" + startOffset + "')";
                whereClause += " AND YARN_DATA NOT IN(SELECT YARN_DATA FROM (SELECT * FROM (SELECT PRTY_CODE,PI_NO,ARTICAL_CODE,ARTICAL_DESC,SHADE_CODE,(NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) AS rem_qty,( PRTY_CODE || '@' || PI_NO || '@' || ARTICAL_CODE || '@' || ARTICAL_DESC || '@' || SHADE_CODE) AS YARN_DATA FROM V_OD_CAPT_TRN_BOM WHERE comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) > 0 AND BOM_FLAG = '1' AND FINAL_ORDER_CONF_CLAG ='1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery ORDER BY ARTICAL_CODE) WHERE ROWNUM <= '" + startOffset + "')";
           
            }

            //string SortExpression = " order by BASE_ARTICAL_CODE";
            string SortExpression = " order by ARTICAL_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        //string CommandText = " SELECT * FROM (SELECT * FROM (SELECT PRTY_CODE, PI_NO, BASE_ARTICAL_CODE, BASE_ARTICAL_DESC, BASE_SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) AS rem_qty, ( PRTY_CODE|| '@'|| PI_NO|| '@'|| BASE_ARTICAL_CODE|| '@'|| BASE_ARTICAL_DESC|| '@'|| BASE_SHADE_CODE) AS YARN_DATA FROM V_OD_CAPT_TRN_BOM WHERE comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) > 0 AND BOM_FLAG = '1' AND FINAL_ORDER_CONF_CLAG = '1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR BASE_ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery ORDER BY BASE_ARTICAL_CODE)  ";
        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT PRTY_CODE, PI_NO, ARTICAL_CODE, ARTICAL_DESC, SHADE_CODE, (NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) AS rem_qty, ( PRTY_CODE|| '@'|| PI_NO|| '@'|| ARTICAL_CODE|| '@'|| ARTICAL_DESC|| '@'|| SHADE_CODE) AS YARN_DATA FROM V_OD_CAPT_TRN_BOM WHERE comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (REQ_QTY, 0) - NVL (adj_qty, 0)) > 0 AND BOM_FLAG = '1' AND FINAL_ORDER_CONF_CLAG = '1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR ARTICAL_CODE LIKE :SearchQuery OR ARTICAL_DESC LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery ORDER BY ARTICAL_CODE)  ";
        
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void ddlYarnCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtYarnCode.Text = ddlYarnCode.SelectedText.Trim();
            txtShadeCode.Text = ddlYarnCode.SelectedValue.Trim();

            string Description = "";
            string UOM = "";
            double CurrentStock = 0;
            double MinStockLevel = 0;
            double OpeningRate = 0;
            double Min_Procure_days = 0;
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchItemCodeInGrid(txtYarnCode.Text.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(txtYarnCode.Text.Trim()), out Description, out UOM, out CurrentStock, out MinStockLevel, out OpeningRate, out Min_Procure_days);

                if (iRecordFound > 0)
                {
                    txtOpeningRate.Text = OpeningRate.ToString();
                    txtYarnDesc.Text = Description;
                    txtCurrentStock0.Text = CurrentStock.ToString();
                    txtMinStockLevel0.Text = MinStockLevel.ToString();
                    txtUnitName.Text = UOM;
                    txtRequestQty.Focus();
                    lblMin_Procure_days.Text = Min_Procure_days.ToString();
                }
                else
                {
                    RefreshDetailRow();
                }
            }
            else
            {
                CommonFuction.ShowMessage("This Item already included");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
        }
    }

    protected void grdIndentDetail_RowEditing(object sender, GridViewEditEventArgs e)
    {
       // grdIndentDetail.EditIndex = e.NewEditIndex;

    }
   
   
}
