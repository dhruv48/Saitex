using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Common;
using errorLog;

public partial class Module_Machine_Controls_Processing_Standard_Master : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.TX_MAC_PROC_MST oTX_MAC_PROC_MST = new SaitexDM.Common.DataModel.TX_MAC_PROC_MST();
    SaitexDM.Common.DataModel.TX_PRO_STN_MST oTX_PRO_STN_MST = new SaitexDM.Common.DataModel.TX_PRO_STN_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtProcessingStandardTrn = null;
    private string UserCode;
    private static string compcode = string.Empty;
    private static string branchcode = string.Empty;
    private static int year = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DateTime startdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Enddate = CommonFuction.GetYearEndDate(startdate);

            if (!IsPostBack)
            {
                dtProcessingStandardTrn = null;
                UserCode = Session["urLoginId"].ToString();
                compcode = oUserLoginDetail.COMP_CODE;
                branchcode = oUserLoginDetail.CH_BRANCHCODE;
                year = oUserLoginDetail.DT_STARTDATE.Year;
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            //Bind_PrintStyle();
            //Bind_Ground();
            ddlProcessRouteCode.Visible = false;
            txtProcessRouteCode.Visible = true;
            BindProcess("MACHINE_PROCESS_TYPE");
            CreateProcessingStandardTrnTable();
            lblMode.Text = "You are in Save Mode";
            imgbtnUpdate.Visible = false;
            imgbtnSave.Visible = true;
            BlankControls();
        }
        catch
        {
            throw;
        }
    }

    //private void BindProcessCode()
    //{
    //    try
    //    {
    //        oTX_MAC_PROC_MST.BRANCH_CODE = branchcode;
    //        oTX_MAC_PROC_MST.COMP_CODE = compcode;
    //        oTX_MAC_PROC_MST.YEAR = year;

    //        DataTable dt = SaitexBL.Interface.Method.TX_MAC_PROC_MST.GetProcessMaster(oTX_MAC_PROC_MST);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlProcessCode.Items.Clear();
    //            ddlProcessCode.DataSource = dt;
    //            ddlProcessCode.DataTextField = "PROS_CODE";
    //            ddlProcessCode.DataValueField = "PROS_CODE";
    //            ddlProcessCode.DataBind();
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    private void BindProcess(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, compcode);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlProcess.Items.Clear();
                ddlProcess.DataSource = dt;
                ddlProcess.DataTextField = "MST_CODE";
                ddlProcess.DataValueField = "MST_CODE";
                ddlProcess.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    //private void Bind_Ground()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_GROUND", oUserLoginDetail.COMP_CODE);
    //        ddlGround.DataSource = dt;
    //        ddlGround.DataValueField = "MST_CODE";
    //        ddlGround.DataTextField = "MST_DESC";
    //        ddlGround.DataBind();
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    private void BlankControls()
    {
        try
        {
            txtDescription.Text = string.Empty;
            txtMachineCode.Text = string.Empty;
            txtProcessDescription.Text = string.Empty;
            txtProcessRouteCode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtTestCode.Text = string.Empty;
            //ddlGround.SelectedIndex = -1;
            //ddlPrintStyle.SelectedIndex = -1;
            ddlProcess.SelectedIndex = -1;
            ddlProcessCode.Text = string.Empty;
            ddlProcessRouteCode.SelectedIndex = -1;
            RefreshProcessingStandardDetailRow();
            dtProcessingStandardTrn = null;
            BindProcessingStandardDetailGrid();
        }
        catch
        {
            throw;
        }
    }

    //private void Bind_PrintStyle()
    //{
    //    try
    //    {
    //        DataTable dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_PRINT_STYLE", oUserLoginDetail.COMP_CODE);
    //        ddlPrintStyle.DataSource = dt;
    //        ddlPrintStyle.DataValueField = "MST_CODE";
    //        ddlPrintStyle.DataTextField = "MST_DESC";
    //        ddlPrintStyle.DataBind();
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (dtProcessingStandardTrn.Rows.Count > 0)
                {
                    int iRecordFound = 0;
                    oTX_PRO_STN_MST.PROS_ROUTE_CODE = Common.CommonFuction.funFixScript(txtProcessRouteCode.Text);
                    oTX_PRO_STN_MST.PROS_DESC = Common.CommonFuction.funFixScript(txtDescription.Text);
                    oTX_PRO_STN_MST.PROCESS = ddlProcess.SelectedItem.Text.Trim();
                    oTX_PRO_STN_MST.GROUND = "NA"; // ddlGround.SelectedItem.Text.Trim();
                    oTX_PRO_STN_MST.PRINT_STYLE = "NA";  //ddlPrintStyle.SelectedItem.Text.Trim();
                    oTX_PRO_STN_MST.COMP_CODE = compcode;
                    oTX_PRO_STN_MST.BRANCH_CODE = branchcode;
                    oTX_PRO_STN_MST.YEAR = year;
                    oTX_PRO_STN_MST.TUSER = oUserLoginDetail.UserCode;
                    bool resutl = SaitexBL.Interface.Method.TX_PRO_STN_MST.InsertProcessingStandardMaster(oTX_PRO_STN_MST, out  iRecordFound, dtProcessingStandardTrn);
                    if (resutl)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Processing Standard Master Saved Successfully');", true);
                        InitialisePage();
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Processing Standard Already Exists');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Processing Standard Master Saving Failed!!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Processing Standard selected. Please enter Processing Standard detail');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (dtProcessingStandardTrn.Rows.Count > 0)
                {
                    int iRecordFound = 0;
                    oTX_PRO_STN_MST.PROS_ROUTE_CODE = ddlProcessRouteCode.SelectedValue.ToString().Trim();
                    oTX_PRO_STN_MST.PROS_DESC = Common.CommonFuction.funFixScript(txtDescription.Text);
                    oTX_PRO_STN_MST.PROCESS = ddlProcess.SelectedItem.Text.Trim();
                    oTX_PRO_STN_MST.GROUND = "NA";  //ddlGround.SelectedItem.Text.Trim();
                    oTX_PRO_STN_MST.PRINT_STYLE = "NA";  //ddlPrintStyle.SelectedItem.Text.Trim();
                    oTX_PRO_STN_MST.COMP_CODE = compcode;
                    oTX_PRO_STN_MST.BRANCH_CODE = branchcode;
                    oTX_PRO_STN_MST.YEAR = year;
                    oTX_PRO_STN_MST.TUSER = oUserLoginDetail.UserCode;

                    bool resutl = SaitexBL.Interface.Method.TX_PRO_STN_MST.UpdateProcessingMaster(oTX_PRO_STN_MST, out  iRecordFound, dtProcessingStandardTrn);
                    if (resutl)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Master Updated Successfully');", true);
                        InitialisePage();
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Already Exists');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Master Updation Failed!!');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Processing Standard selected. Please enter Processing Standard detail');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Update.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlProcessRouteCode.Visible = true;
            txtProcessRouteCode.Visible = false;
            lblMode.Text = "You are in Update Mode";
            imgbtnUpdate.Visible = true;
            imgbtnSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Find.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
         try
        {
            string URL = "../Reports/Machine_Proute_MstRpt.aspx";
           ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
             

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing.rnSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./Processing_Standard_Master.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear.\r\nSee error log for detail."));
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbProcessCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            //string CommandText = " select PROS_CODE,PROS_DESC, PRODUCT_TYPE, MAC_CODE,DEPARTMENT from TX_MAC_PROC_MST";
            //string WhereClause = "  where PROS_CODE like :SearchQuery or PROS_DESC like :SearchQuery OR MAC_CODE LIKE :SearchQuery OR DEPARTMENT LIKE :SearchQuery or PRODUCT_TYPE like :SearchQuery";
            //string SortExpression = " order by PROS_CODE asc";
            //string SearchQuery = e.Text.ToUpper() + "%";
            //DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            //ddlProcessCode.Items.Clear();
            //ddlProcessCode.DataSource = data;
            //ddlProcessCode.DataBind();
            //e.ItemsLoadedCount = data.Rows.Count;
            //e.ItemsCount = data.Rows.Count;

            DataTable data = GetProcessData(e.Text, e.ItemsOffset);
            cmbProcessCode.Items.Clear();
            cmbProcessCode.DataSource = data;
            cmbProcessCode.DataTextField = "PROS_CODE";
            cmbProcessCode.DataValueField = "PRODUCT_TYPE";
            cmbProcessCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetProcessCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Process Code Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetProcessData(string text, int startOffset)
    {
        try
        {
            DataTable dt = new DataTable();
            //string CommandText = " SELECT * FROM (SELECT * FROM (SELECT DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE|| '@'|| PT.YEAR)po_Item_trn, pt.PO_NUMB, pt.year, pt.ITEM_CODE, pt.ORD_QTY, PM.PRTY_CODE, pt.BASIC_RATE, pt.FINAL_RATE, i.ITEM_DESC, NVL (PT.QTY_RCPT, 0) QTY_RCPT, NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt, TX_ITEM_MST i, tx_item_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND pt.year = pm.year AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY po_Item_trn) WHERE ROWNUM <= 15 ";
            string CommandText = " SELECT * FROM (SELECT PROS_CODE, PROS_DESC, PRODUCT_TYPE, MAC_CODE, DEPARTMENT FROM TX_MAC_PROC_MST WHERE PROS_CODE LIKE :SearchQuery OR PROS_DESC LIKE :SearchQuery OR PRODUCT_TYPE LIKE :SearchQuery OR MAC_CODE LIKE :SearchQuery OR DEPARTMENT LIKE :SearchQuery ORDER BY PROS_CODE) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                //whereClause += "   AND po_Item_trn NOT IN(SELECT po_Item_trn FROM (SELECT * FROM (SELECT DISTINCT( PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.ITEM_CODE|| '@'|| PT.YEAR) po_Item_trn,pt.PO_NUMB, pt.year,pt.ITEM_CODE,pt.ORD_QTY,PM.PRTY_CODE,pt.BASIC_RATE,pt.FINAL_RATE,i.ITEM_DESC,NVL (PT.QTY_RCPT, 0) QTY_RCPT,NVL (PT.ORD_QTY, 0)- NVL (PT.QTY_RCPT, 0) AS QTY_REM FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i,tx_item_pu_mst pm WHERE  pt.comp_code = pm.comp_code AND pt.branch_code = pm.branch_code AND pt.po_type = pm.po_type AND pt.po_numb = pm.po_numb AND pt.year = pm.year AND PM.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND PM.DELV_BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND PM.CONF_FLAG = '1' AND (NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0)) > 0 AND pt.ITEM_CODE = i.ITEM_CODE AND pm.PO_NUMB = PT.PO_NUMB and pm.PRTY_CODE = '" + lblPartyCode.Text + "') where PO_NUMB LIKE :SearchQuery or ITEM_CODE LIKE :SearchQuery ORDER BY po_Item_trn) WHERE ROWNUM <= " + startOffset + ")";
                whereClause += " AND PROS_CODE NOT IN (SELECT PROS_CODE FROM (SELECT PROS_CODE, PROS_DESC, PRODUCT_TYPE, MAC_CODE, DEPARTMENT FROM TX_MAC_PROC_MST WHERE PROS_CODE LIKE :SearchQuery OR PROS_DESC LIKE :SearchQuery OR PRODUCT_TYPE LIKE :SearchQuery OR MAC_CODE LIKE :SearchQuery OR DEPARTMENT LIKE :SearchQuery ORDER BY PROS_CODE) WHERE ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " ORDER BY PROS_CODE";
            string SearchQuery = text + "%";
            dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return dt;
        }
        catch
        {
            throw;
        }
    }

    protected int GetProcessCount(string text)
    {
        try
        {
            DataTable data = new DataTable();
            string CommandText = " SELECT * FROM (SELECT PROS_CODE, PROS_DESC, PRODUCT_TYPE, MAC_CODE, DEPARTMENT FROM TX_MAC_PROC_MST WHERE PROS_CODE LIKE :SearchQuery OR PROS_DESC LIKE :SearchQuery OR PRODUCT_TYPE LIKE :SearchQuery OR MAC_CODE LIKE :SearchQuery OR DEPARTMENT LIKE :SearchQuery ORDER BY PROS_CODE) ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbProcessCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            ddlProcessCode.Text = cmbProcessCode.SelectedText.ToString().Trim();
            oTX_MAC_PROC_MST.BRANCH_CODE = branchcode;
            oTX_MAC_PROC_MST.COMP_CODE = compcode;
            oTX_MAC_PROC_MST.YEAR = year;
            oTX_MAC_PROC_MST.PROS_CODE = cmbProcessCode.SelectedText.ToString();
            oTX_MAC_PROC_MST.PRODUCT_TYPE = cmbProcessCode.SelectedValue.ToString();
            DataTable dtProcessMaster = SaitexBL.Interface.Method.TX_MAC_PROC_MST.GetProcessMaster(oTX_MAC_PROC_MST);
            if (dtProcessMaster != null && dtProcessMaster.Rows.Count > 0)
            {
                txtMachineCode.Text = dtProcessMaster.Rows[0]["MAC_CODE"].ToString();
                txtProcessDescription.Text = dtProcessMaster.Rows[0]["PROS_DESC"].ToString();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Process Code Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    #region ProcessingStandardTrnTable()

    private void CreateProcessingStandardTrnTable()
    {
        try
        {
            dtProcessingStandardTrn = new DataTable();
            dtProcessingStandardTrn.Columns.Add("UniqueId", typeof(int));
            dtProcessingStandardTrn.Columns.Add("MCode", typeof(string));
            dtProcessingStandardTrn.Columns.Add("PCode", typeof(string));
            dtProcessingStandardTrn.Columns.Add("Description", typeof(string));
            dtProcessingStandardTrn.Columns.Add("TestCode", typeof(string));
            dtProcessingStandardTrn.Columns.Add("Remarks", typeof(string));
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
            if (dtProcessingStandardTrn == null)
                CreateProcessingStandardTrnTable();

            if (dtProcessingStandardTrn.Rows.Count < 15)
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                {
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                }
                bool bb = SearchParameterInProcessingStandardGrid(ddlProcessCode.Text.Trim(), UniqueId);
                if (!bb)
                {
                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtProcessingStandardTrn);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["PCode"] = ddlProcessCode.Text.Trim();
                            dv[0]["MCode"] = txtMachineCode.Text.Trim();
                            dv[0]["Description"] = txtProcessDescription.Text.Trim();
                            dv[0]["TestCode"] = txtTestCode.Text.Trim();
                            dv[0]["Remarks"] = txtRemarks.Text.Trim();
                            dtProcessingStandardTrn.AcceptChanges();
                        }
                    }
                    else
                    {

                        DataRow dr = dtProcessingStandardTrn.NewRow();
                        dr["UniqueId"] = dtProcessingStandardTrn.Rows.Count + 1;
                        dr["PCode"] = ddlProcessCode.Text.Trim();
                        dr["MCode"] = txtMachineCode.Text.Trim();
                        dr["Description"] = txtProcessDescription.Text.Trim();
                        dr["TestCode"] = txtTestCode.Text.Trim();
                        dr["Remarks"] = txtRemarks.Text.Trim();
                        dtProcessingStandardTrn.Rows.Add(dr);
                    }
                    RefreshProcessingStandardDetailRow();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Parameter Already Added.Please Select Another');", true);
                }
                BindProcessingStandardDetailGrid();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of Standard. Only 15 Standard allowed in one Machine Process Master.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void gvProcessingStandardMaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "ProcessingStandardEdit")
            {
                FillProcessingStandardTrnGrid(UniqueId);
            }
            else if (e.CommandName == "ProcessingStandardDelete")
            {

                DeleteProcessingStandardRow(UniqueId);
                BindProcessingStandardDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid RowCommand Event.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void FillProcessingStandardTrnGrid(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(dtProcessingStandardTrn);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                ddlProcessCode.Text = dv[0]["PCode"].ToString();
                txtMachineCode.Text = dv[0]["MCode"].ToString();
                txtProcessDescription.Text = dv[0]["Description"].ToString();
                txtTestCode.Text = dv[0]["TestCode"].ToString();
                txtRemarks.Text = dv[0]["Remarks"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteProcessingStandardRow(int UniqueId)
    {
        try
        {
            if (gvProcessingStandardMaster.Rows.Count == 1)
            {
                dtProcessingStandardTrn.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtProcessingStandardTrn.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtProcessingStandardTrn.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtProcessingStandardTrn.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchParameterInProcessingStandardGrid(string ProcessCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in gvProcessingStandardMaster.Rows)
            {
                Label lblProcessCode = (Label)grdRow.FindControl("txcProcessCode");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (lblProcessCode.Text.Trim() == ProcessCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private void BindProcessingStandardDetailGrid()
    {
        try
        {
            gvProcessingStandardMaster.DataSource = dtProcessingStandardTrn;
            gvProcessingStandardMaster.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void RefreshProcessingStandardDetailRow()
    {
        try
        {
            ddlProcessCode.Text = string.Empty;
            txtMachineCode.Text = string.Empty;
            txtProcessDescription.Text = string.Empty;
            txtTestCode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void MapProcessingStandardDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtProcessingStandardTrn == null || dtProcessingStandardTrn.Rows.Count == 0)
                CreateProcessingStandardTrnTable();

            dtProcessingStandardTrn.Rows.Clear();
            int currentyear = year;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    if (currentyear == int.Parse(drTemp["YEAR"].ToString()))
                    {
                        DataRow dr = dtProcessingStandardTrn.NewRow();
                        dr["UniqueId"] = dtProcessingStandardTrn.Rows.Count + 1;
                        dr["MCode"] = drTemp["MAC_CODE"];
                        dr["PCode"] = drTemp["PROS_CODE"];
                        dr["Description"] = drTemp["PROS_DESC"];
                        dr["TestCode"] = drTemp["TEST_CODE"];
                        dr["Remarks"] = drTemp["REMARKS"];

                        dtProcessingStandardTrn.Rows.Add(dr);
                    }
                }
                dtTemp = null;
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void ddlProcessRouteCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select PROS_ROUTE_CODE,PROS_DESC from TX_PRO_STN_MST ";
            string WhereClause = "  where PROS_ROUTE_CODE like :SearchQuery or PROS_DESC like :SearchQuery ";
            string SortExpression = " order by PROS_ROUTE_CODE asc";
            string SearchQuery = e.Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlProcessRouteCode.Items.Clear();
            ddlProcessRouteCode.DataSource = data;
            ddlProcessRouteCode.DataBind();
            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Process Route Code Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlProcessRouteCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            oTX_PRO_STN_MST.BRANCH_CODE = branchcode;
            oTX_PRO_STN_MST.COMP_CODE = compcode;
            oTX_PRO_STN_MST.YEAR = year;
            oTX_PRO_STN_MST.PROS_ROUTE_CODE = ddlProcessRouteCode.SelectedText.ToString();

            DataTable dtProcessingMaster = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessinRoutegMst(oTX_PRO_STN_MST);
            if (dtProcessingMaster != null && dtProcessingMaster.Rows.Count > 0)
            {
                txtProcessRouteCode.Text = dtProcessingMaster.Rows[0]["PROS_ROUTE_CODE"].ToString();
                txtDescription.Text = dtProcessingMaster.Rows[0]["PROS_DESC"].ToString();
                ddlProcess.SelectedValue = dtProcessingMaster.Rows[0]["PROCESS"].ToString();
                //ddlGround.SelectedValue = dtProcessingMaster.Rows[0]["GROUND"].ToString();
                //ddlPrintStyle.SelectedValue = dtProcessingMaster.Rows[0]["PRINT_STYLE"].ToString();
                DataTable dtProcessingTrn = SaitexBL.Interface.Method.TX_PRO_STN_MST.GetProcessingStandarProcessRouteCode(oTX_PRO_STN_MST);
                if (dtProcessingTrn != null && dtProcessingTrn.Rows.Count > 0)
                {
                    MapProcessingStandardDataTable(dtProcessingTrn);
                    BindProcessingStandardDetailGrid();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Process Route Code Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}