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

public partial class Module_Sewing_Thread_Controls_SWIndent : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

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
            lblMode.Text = ex.ToString();
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
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTNAME;
                txtPreparedBy.Text = oUserLoginDetail.Username;
                txtIndentDate.Text = DateTime.Now.ToShortDateString();
                txtRequiredBefore.Text = DateTime.Now.ToShortDateString();

            }
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

    private void CreateIndentDetailTable()
    {
        try
        {
            dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("UniqueId", typeof(int));
            dtIndentDetail.Columns.Add("IndentDetailNumber", typeof(int));
            dtIndentDetail.Columns.Add("IndentNumber", typeof(string));
            dtIndentDetail.Columns.Add("YARN_CODE", typeof(string));
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

                DeleteIndentDetailRow(UniqueId);
                BindIndentDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                txtItemCode.SelectedText = (dv[0]["YARN_CODE"].ToString());
                txtItemCodeLabel.Text = dv[0]["YARN_CODE"].ToString();
                txtItemDesc.Text = dv[0]["YARN_DESC"].ToString();
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
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }
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
                Label txtItemCode1 = (Label)grdRow.FindControl("txtItemCode");
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in entering request quantity.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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

    private int GetdataByIndentNumber(string IndentNumber)
    {
        int iRecordFound = 0;
        try
        {
            DataTable dts = SaitexBL.Interface.Method.YRN_INT_MST.SelectByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN", int.Parse(IndentNumber), oUserLoginDetail.VC_DEPARTMENTCODE,oUserLoginDetail.DT_STARTDATE.Year );

            if (dts != null && dts.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtCommentComment.Text = dts.Rows[0]["CONF_COMMENT"].ToString().Trim();
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
                    ActivateUpdateMode();
                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }
        catch (OracleException ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            return iRecordFound;
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
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
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["YARN_DESC"] = drTemp["YARN_DESC"];
                    dr["currentStock"] = drTemp["currentStock"];
                    dr["MIN_STOCK_LVL"] = drTemp["MIN_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["RQST_QTY"] = drTemp["RQST_QTY"];
                    dr["VC_UNITNAME"] = drTemp["UNIT_NAME"];
                    dr["Amount"] = drTemp["iValue"];
                    dr["DPT_REMARK"] = drTemp["DPT_REMARK"];
                    dr["Min_Procure_days"] = drTemp["MIN_PROCURE_DAYS"];

                    FinalTotal = FinalTotal + double.Parse(drTemp["iValue"].ToString());
                    dtIndentDetail.Rows.Add(dr);

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

    private DataTable GetItemIndentTrasaction(string strIndentNum)
    {
        try
        {
            DataTable dtTemp = SaitexBL.Interface.Method.YRN_INT_MST.Select_TransactionByIndentNumber(oUserLoginDetail.DT_STARTDATE.Year, int.Parse(strIndentNum), oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, "GEN");

            return dtTemp;
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

            string URL = "~/Module/Yarn/SalesWork/Reports/YarnPrintIndent1.aspx";
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=300,height=200');", true);
            Response.Redirect(URL);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
        //imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (ViewState["dtIndentDetail"] != null)
                    dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
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
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting indent.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
        }
    }

    protected void SaveIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IND_MST oYRN_IND_MST = new SaitexDM.Common.DataModel.YRN_IND_MST();
            oYRN_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IND_MST.IND_TYPE = "GEN";
            oYRN_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oYRN_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oYRN_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oYRN_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oYRN_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oYRN_IND_MST.STATUS = true;
            oYRN_IND_MST.TUSER = oUserLoginDetail.UserCode;
            int Ind_numb = 0;

            bool Result = SaitexBL.Interface.Method.YRN_INT_MST.Insert(oYRN_IND_MST, dtIndentDetail, out Ind_numb);
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

    protected void UpdateIndentData()
    {
        try
        {
            SaitexDM.Common.DataModel.YRN_IND_MST oYRN_IND_MST = new SaitexDM.Common.DataModel.YRN_IND_MST();
            oYRN_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oYRN_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oYRN_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oYRN_IND_MST.IND_TYPE = "GEN";
            oYRN_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oYRN_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oYRN_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oYRN_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oYRN_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtCommentComment.Text.Trim());
            oYRN_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oYRN_IND_MST.STATUS = true;
            oYRN_IND_MST.TUSER = oUserLoginDetail.UserCode;

            bool Result = SaitexBL.Interface.Method.YRN_INT_MST.Update(oYRN_IND_MST, dtIndentDetail);
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

    protected void Button1_Click(object sender, EventArgs e)
    {

    }

    private void RefreshDetailRow()
    {
        try
        {
            txtItemCode.SelectedIndex = -1;
            txtItemCodeLabel.Text = string.Empty;
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
                    {
                        Temp = NewDate;
                    }
                }
                txtRequiredBefore.Text = Temp.ToShortDateString();
            }
            else
            {
                // txtRequiredBefore.Text = DateTime.Now.ToShortDateString();
            }
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
            if (dtIndentDetail.Rows.Count < 15)
            {
                if (txtItemCodeLabel.Text != "" && txtRequestQty.Text != "" && txtOpeningRate.Text != "")
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
                                    dv[0]["YARN_CODE"] = txtItemCodeLabel.Text.Trim();
                                    dv[0]["YARN_DESC"] = txtItemDesc.Text.Trim();
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
                                dr["YARN_CODE"] = txtItemCode.SelectedValue.Trim();
                                dr["YARN_DESC"] = txtItemDesc.Text.Trim();
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
                else if (txtItemCode.SelectedValue == "SELECT")
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
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
            data = GetIndents(e.Text.ToUpper(), e.ItemsOffset, 10);

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
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlIndentNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
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
                //      ActivateUpdateMode();
            }
            else
            {
                txtIndentNumber.Text = IndentNo;

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading indent data for updation.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetIndents(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = "  where Ind_numb like:searchQuery";
            string sortExpression = " order by ind_numb desc, ind_date desc";
            string commandText = "select * from (Select a.IND_NUMB, TO_CHAR (a.IND_DATE, 'DD/MM/YYYY') AS IND_DATE from YRN_IND_MST a Where A.COMP_CODE=:COMP_CODE AND A.BRANCH_CODE=:BRANCH_CODE AND A.IND_TYPE=:IND_TYPE and a.dept_code='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' ) asd";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IND_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN", commandText, whereClause, sortExpression, "", text + "%");

            return dt;
        }
        catch
        {
            throw;
        }
    }

    private void ActivateUpdateMode()
    {
        try
        {
            ddlIndentNumber.Visible = true;

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


            txtIndentNumber.Visible = true;
        }
        catch
        {
            throw;
        }
    }

    protected void Item_LOV_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                txtItemCode.Items.Clear();

                txtItemCode.DataSource = data;
                txtItemCode.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_TYPE = 'SEWING THREAD' AND ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_TYPE = 'SEWING THREAD' AND ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE  YARN_TYPE = 'SEWING THREAD' ";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }

    protected void Item_LOV_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {

            string Description = "";
            string UOM = "";
            double CurrentStock = 0;
            double MinStockLevel = 0;
            double OpeningRate = 0;
            double Min_Procure_days = 0;
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());

            txtItemCodeLabel.Text = string.Empty;
            txtItemCodeLabel.Text = txtItemCode.SelectedValue.ToString().Trim();
            if (!SearchItemCodeInGrid(txtItemCode.SelectedValue.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByItemCode(CommonFuction.funFixQuotes(txtItemCode.SelectedValue.Trim()), out Description, out UOM, out CurrentStock, out MinStockLevel, out OpeningRate, out Min_Procure_days);

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
                    txtItemCode.Focus();
                }
            }
            else
            {
                CommonFuction.ShowMessage("This Yarn already included");
                txtItemCode.SelectedValue = "SELECT";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            ActivateSaveMode();
            lblMode.Text = "Save";

            FinalTotal = 0;
            txtCommentComment.Text = "";
            txtDepartment.Text = "";
            txtIndentDate.Text = "";
            txtIndentNumber.Text = "";
            txtPreparedBy.Text = "";
            txtRequiredBefore.Text = "";

            txtIndentNumber.Text = SaitexBL.Interface.Method.YRN_INT_MST.GetNewIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, "GEN",oUserLoginDetail.DT_STARTDATE.Year);
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
        }
        catch
        {
            throw;
        }
    }


}
