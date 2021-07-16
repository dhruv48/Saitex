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
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Inventory_Controls_Packing_Mst : System.Web.UI.UserControl
{
    public static string strCompanyCode = string.Empty;
    public static string strBranchCode = string.Empty;
    public static int OpenYear;
    private static DataTable dtDetailTBL = null;
    private static SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["usrNames"] != null)
            {
                TxtPackingEmpty.Attributes.Add("readonly", "readonly");
                txtDESC.Attributes.Add("readonly", "readonly");
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
                strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                OpenYear = oUserLoginDetail.DT_STARTDATE.Year;
                if (!IsPostBack)
                {
                    Initial_Control();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading"));
        }
    }

    protected override void OnInit(EventArgs e)
    {
        cmbPOITEM.OnTextChanged += new CommonControls_LOV_ItemLOV_Packing.RefreshDataGridView(cmbPOITEM_OnTextChanged);
    }

    void cmbPOITEM_OnTextChanged(string Value, string Text)
    {
        try
        {
            string cString = cmbPOITEM.SelectedValue.Trim();
            GetDataForDetail(cString);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data.\r\nSee error log for detail."));
        }
    }


    private void Initial_Control()
    {
        try
        {
            CreateDataTable();
            Clear_Control();
            Bind_Control(DDLUnit, "UOM");
            Bind_Control(DDLYarnPackUOM, "PACK_UOM");
            Load_Base_Packing_Record();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Clear_Control()
    {
        try
        {
            //Max_Pack_Code();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            DDLPackCode.Visible = false;
            TxtPackCode.Visible = true;
            TxtItemLength.Text = string.Empty;
            TxtItemNo.Text = string.Empty;
            TxtItemWeight.Text = string.Empty;
            TxtPackDesc.Text = string.Empty;
            TxtPackingEmpty.Text = string.Empty;
            TxtPackWeight.Text = string.Empty;
            DDLBasepack.SelectedIndex = -1;
            DDLPackCode.SelectedIndex = -1;
            DDLUnit.SelectedIndex = -1;
            DDLPackingType.SelectedIndex = -1;
            DDLYarnPackUOM.SelectedIndex = -1;
            TxtPackCode.Text = string.Empty;
            dtDetailTBL.Rows.Clear();
            BindGridFromDataTable();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Max_Pack_Code()
    {
        try
        {
            string PackCode = SaitexBL.Interface.Method.TX_PACKING_MST.Get_Max_Pack_Code(strCompanyCode.Trim().ToString());
            TxtPackCode.Text = PackCode.ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Bind_Control(DropDownList DDL, string MST_NAME)
    {
        try
        {
            DDL.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, strCompanyCode);
            DDL.DataSource = dt;
            DDL.DataTextField = "MST_DESC";
            DDL.DataValueField = "MST_CODE";
            DDL.DataBind();
            DDL.Items.Insert(0, new ListItem("--------------Select--------------"));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Load_Packing_Record()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_PACKING_MST.Load_Packing_Code(strCompanyCode, strBranchCode);
            DDLPackCode.DataSource = dt;
            DDLPackCode.DataTextField = "PACKING";
            DDLPackCode.DataValueField = "PCK_CODE";
            DDLPackCode.DataBind();
            //DDLPackCode.Items.Insert(0, new ListItem("--------------Select--------------"));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void Load_Base_Packing_Record()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_PACKING_MST.Load_Packing_Code(strCompanyCode, strBranchCode);
            DDLBasepack.DataSource = dt;
            DDLBasepack.DataTextField = "PCK_CODE";
            DDLBasepack.DataValueField = "PCK_CODE";
            DDLBasepack.DataBind();
            DDLBasepack.Items.Insert(0, new ListItem("--------------Select--------------"));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Save_Record())
            {
                Clear_Control();
                Common.CommonFuction.ShowMessage("Record save sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("unable to save!please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in saving the record"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Save_Record())
            {
                Clear_Control();
                Common.CommonFuction.ShowMessage("Record Update sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("unable to Update!please try again");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in saving the record"));
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in clearing the control"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit.\\r\\nSee error log for detail."));
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            DDLPackCode.Visible = true;
            TxtPackCode.Visible = false;


        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Finding Records.\\r\\nSee error log for detail."));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (TxtPackCode.Text.Trim().ToString() != string.Empty)
            {
                bool Res = SaitexBL.Interface.Method.TX_PACKING_MST.Delete_Record(TxtPackCode.Text.Trim().ToString());
                if (Res)
                {
                    Clear_Control();
                    Common.CommonFuction.ShowMessage("Record Delete Sucessfully");
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Unable to Delete");
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Record Loading"));
        }
    }
    private bool Save_Record()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_PACKING_MST PM = new SaitexDM.Common.DataModel.TX_PACKING_MST();
            if (DDLBasepack.SelectedIndex != 0)
            {
                PM.BASE_PCK_CODE = DDLBasepack.SelectedValue.Trim().ToString();
            }
            else
            {
                PM.BASE_PCK_CODE = "SELF";
            }
            if (DDLYarnPackUOM.SelectedIndex != 0)
            {
                PM.PACK_UOM = DDLYarnPackUOM.SelectedValue.Trim().ToString();
            }
            else
            {
                PM.PACK_UOM = string.Empty;
            }
            if (DDLPackingType.SelectedIndex != 0)
            {
                PM.PACK_TYPE = DDLPackingType.SelectedValue.Trim().ToString();
            }
            else
            {
                PM.PACK_TYPE = string.Empty;
            }
            PM.BRANCH_CODE = strBranchCode.ToString();
            PM.COMP_CODE = strCompanyCode.ToString();
            if (TxtItemLength.Text.Trim().ToString() != string.Empty)
            {
                PM.LENGHT_ITEM_EACH = decimal.Parse(TxtItemLength.Text.Trim().ToString());
            }
            if (TxtItemNo.Text.Trim().ToString() != string.Empty)
            {
                PM.NO_ITEM = decimal.Parse(TxtItemNo.Text.Trim().ToString());
            }
            PM.PCK_CODE = TxtPackCode.Text.Trim().ToString();
            PM.PCK_DESC = TxtPackDesc.Text.Trim().ToString();
            if (TxtPackingEmpty.Text.Trim().ToString() != string.Empty)
            {
                PM.PCK_EMPTY_WEIGHT = decimal.Parse(TxtPackingEmpty.Text.Trim().ToString());
            }
            if (TxtPackWeight.Text.Trim().ToString() != string.Empty)
            {
                PM.PCK_WEIGHT = decimal.Parse(TxtPackWeight.Text.Trim());
            }
            if (DDLUnit.SelectedIndex != 0)
            {
                PM.UNIT_NAME = DDLUnit.SelectedValue.Trim().ToString();
            }
            else
            {
                PM.UNIT_NAME = string.Empty;
            }
            if (TxtItemWeight.Text.Trim().ToString() != string.Empty)
            {
                PM.WEIGHT_ITEM_EACH = decimal.Parse(TxtItemWeight.Text.Trim().ToString());
            }
            PM.TUSER = Session["urLoginId"].ToString().Trim();
            bool Res = SaitexBL.Interface.Method.TX_PACKING_MST.Insert_Record(PM, dtDetailTBL, oUserLoginDetail.DT_STARTDATE.Year);
            return Res;
        }
        catch
        {
            throw;
        }
    }
    protected void DDLPackCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_PACKING_MST.Load_Packing_Code(strCompanyCode, strBranchCode);
            DDLPackCode.Items.Clear();
            DDLPackCode.DataSource = dt;
            DDLPackCode.DataBind();
            e.ItemsLoadedCount = dt.Rows.Count;
            e.ItemsCount = dt.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
        }
    }
    protected void DDLPackCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            if (DDLPackCode.SelectedValue.Trim().ToString() != "0")
            {
                Load_Records(DDLPackCode.SelectedValue.Trim().ToString());
                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Record Loading"));
        }
    }
    private void Load_Records(string Packing_Code)
    {
        int iRecordFound = 0;
        try
        {
            DataTable DTable = new DataTable();
            DTable = SaitexBL.Interface.Method.TX_PACKING_MST.Load_Record(Packing_Code);
            if (DTable.Rows.Count > 0)
            {
                iRecordFound = 1;
                TxtPackCode.Text = DTable.Rows[0]["PCK_CODE"].ToString().Trim();
                if (DTable.Rows[0]["LENGHT_ITEM_EACH"].ToString().Trim() != "0")
                {
                    TxtItemLength.Text = DTable.Rows[0]["LENGHT_ITEM_EACH"].ToString().Trim();
                }
                if (DTable.Rows[0]["NO_ITEM"].ToString().Trim() != "0")
                {
                    TxtItemNo.Text = DTable.Rows[0]["NO_ITEM"].ToString().Trim();
                }
                if (DTable.Rows[0]["WEIGHT_ITEM_EACH"].ToString().Trim() != "0")
                {
                    TxtItemWeight.Text = DTable.Rows[0]["WEIGHT_ITEM_EACH"].ToString().Trim();
                }
                if (DTable.Rows[0]["PCK_DESC"].ToString().Trim() != "0")
                {
                    TxtPackDesc.Text = DTable.Rows[0]["PCK_DESC"].ToString().Trim();
                }
                if (DTable.Rows[0]["PCK_EMPTY_WEIGHT"].ToString().Trim() != "0")
                {
                    TxtPackingEmpty.Text = DTable.Rows[0]["PCK_EMPTY_WEIGHT"].ToString().Trim();
                }
                if (DTable.Rows[0]["PCK_WEIGHT"].ToString().Trim() != "0")
                {
                    TxtPackWeight.Text = DTable.Rows[0]["PCK_WEIGHT"].ToString().Trim();
                }
                if (DTable.Rows[0]["BASE_PCK_CODE"].ToString().Trim() != "SELF")
                {
                    DDLBasepack.SelectedValue = DTable.Rows[0]["BASE_PCK_CODE"].ToString().Trim();
                }
                if (DTable.Rows[0]["UNIT_NAME"].ToString().Trim() != string.Empty)
                {
                    DDLUnit.SelectedValue = DTable.Rows[0]["UNIT_NAME"].ToString().Trim();
                }
                if (DTable.Rows[0]["PACK_UOM"].ToString().Trim() != string.Empty)
                {
                    DDLYarnPackUOM.SelectedValue = DTable.Rows[0]["PACK_UOM"].ToString().Trim();
                }
                if (DTable.Rows[0]["PACK_TYPE"].ToString().Trim() != string.Empty)
                {
                    DDLPackingType.SelectedValue = DTable.Rows[0]["PACK_TYPE"].ToString().Trim();
                }
            }
            if (iRecordFound == 1)
            {
                dtDetailTBL.Rows.Clear();
                dtDetailTBL = SaitexBL.Interface.Method.TX_PACKING_MST.GetPacking_TrnRecord(oUserLoginDetail.DT_STARTDATE.Year, strCompanyCode, strBranchCode, Packing_Code);
                MapDataTable();

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void MapDataTable()
    {
        try
        {
            if (!dtDetailTBL.Columns.Contains("UNIQUEID"))
                dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void DDLPackingType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (DDLPackingType.SelectedValue != "0")
            {
                if (DDLPackingType.SelectedValue == "Self")
                {
                    DDLBasepack.Enabled = false;
                }
                else
                {
                    DDLBasepack.Enabled = true;
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("PCK_CODE", typeof(string ));
            dtDetailTBL.Columns.Add("ITEM_CODE", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_DESC", typeof(string));
            dtDetailTBL.Columns.Add("ITEM_QTY", typeof(double));
            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("COMP_CODE", typeof(string));
            dtDetailTBL.Columns.Add("BRANCH_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YEAR", typeof(string));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void BindGridFromDataTable()
    {
        try
        {
            GridViewItemTrn.DataSource = dtDetailTBL;
            GridViewItemTrn.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void deleteItemIssueRow(int UniqueId)
    {
        try
        {
            if (dtDetailTBL.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in GridViewItemTrn.Rows)
            {
                Label LblItemCode = (Label)grdRow.FindControl("txtICODE");
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                if (LblItemCode.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void btnsaveTRNDetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (dtDetailTBL == null)
                CreateDataTable();
            if (TxtPackCode.Text.Trim().ToString() != string.Empty)
            {
                if (dtDetailTBL.Rows.Count < 15)
                {
                    if (cmbPOITEM.SelectedItem != "" && txtQTY.Text != "")
                    {
                        int UNIQUEID = 0;
                        if (ViewState["UNIQUEID"] != null)
                            UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                        bool bb = SearchItemCodeInGrid(cmbPOITEM.SelectedValue.Trim(), UNIQUEID);
                        if (!bb)
                        {
                            double Qty = 0;
                            double.TryParse(txtQTY.Text.Trim(), out Qty);
                            if (Qty > 0)
                            {
                                if (UNIQUEID > 0)
                                {
                                    DataView dv = new DataView(dtDetailTBL);
                                    dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                                    if (dv.Count > 0)
                                    {
                                        dv[0]["PCK_CODE"] = TxtPackCode.Text.Trim();
                                        dv[0]["YEAR"] = OpenYear;
                                        dv[0]["COMP_CODE"] = strCompanyCode;
                                        dv[0]["BRANCH_CODE"] = strBranchCode;
                                        dv[0]["ITEM_CODE"] = cmbPOITEM.SelectedValue.Trim();
                                        dv[0]["ITEM_DESC"] = txtDESC.Text.Trim();
                                        dv[0]["ITEM_QTY"] = double.Parse(txtQTY.Text.Trim());
                                        dv[0]["REMARKS"] = txtDetRemarks.Text.Trim();
                                        dtDetailTBL.AcceptChanges();
                                    }
                                }
                                else
                                {
                                    DataRow dr = dtDetailTBL.NewRow();
                                    dr["UNIQUEID"] = dtDetailTBL.Rows.Count + 1;
                                    dr["PCK_CODE"] = TxtPackCode.Text.Trim();
                                    dr["YEAR"] = OpenYear;
                                    dr["COMP_CODE"] = strCompanyCode;
                                    dr["BRANCH_CODE"] = strBranchCode;
                                    dr["ITEM_CODE"] = cmbPOITEM.SelectedValue.Trim();
                                    dr["ITEM_DESC"] = txtDESC.Text.Trim();
                                    dr["ITEM_QTY"] = double.Parse(txtQTY.Text.Trim());
                                    dr["REMARKS"] = txtDetRemarks.Text.Trim();
                                    dtDetailTBL.Rows.Add(dr);
                                }
                                RefreshDetailRow();
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PM", "window.alert('Quantity can not be zero');", true);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PM", "window.alert('enter valid item code');", true);
                        }
                    }
                    GridViewItemTrn.DataSource = dtDetailTBL;
                    GridViewItemTrn.DataBind();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PM", "window.alert('You have reached the limit of items. Only 15 items allowed in one Packing Master.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PM", "window.alert('Packing Code can't insert null,Please enter Packing Code');", true);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding Material detail data.\r\nSee error log for detail."));
        }
    }
    private void RefreshDetailRow()
    {
        try
        {
            cmbPOITEM.SelectedIndex = -1;
            txtDESC.Text = string.Empty;
            txtQTY.Text = string.Empty;
            txtDetRemarks.Text = string.Empty;
            ViewState["UNIQUEID"] = null;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void btnTRNCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refresh detail information.\r\nSee error log for detail."));
        }
    }

    private DataTable RemoveAlreadyAddedRow(DataTable dt)
    {
        try
        {
            if (dtDetailTBL != null && dtDetailTBL.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dtDetailTBL.Rows)
                {
                    dt = RemoveRowFromItemSelectionList(dr1, dt);
                }
            }
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private DataTable RemoveRowFromItemSelectionList(DataRow dr, DataTable dt)
    {
        try
        {
            foreach (DataRow dr1 in dt.Rows)
            {
                if (dr1["ITEM_CODE"].ToString() == dr["ITEM_CODE"].ToString())
                {
                    dt.Rows.Remove(dr1);
                    break;
                }
            }
            dt.AcceptChanges();
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }

    private void GetDataForDetail(string Item_Code)
    {
        try
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            if (!SearchItemCodeInGrid(Item_Code, UNIQUEID))
            {
                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemDetailByItemCode(Item_Code, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.CH_BRANCHCODE,"","");

                if (dt != null && dt.Rows.Count > 0)
                {
                    cmbPOITEM.SetIndexByValue(dt.Rows[0]["ITEM_CODE"].ToString().Trim());
                    txtDESC.Text = dt.Rows[0]["ITEM_DESC"].ToString().Trim();
                    txtQTY.Text = "0";
                }
            }
            else
            {
                RefreshDetailRow();
                CommonFuction.ShowMessage("Item Already Included");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void EditItemReceiptRow(int UNIQUEID)
    {
        try
        {
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                ViewState["UNIQUEID"] = UNIQUEID;

                cmbPOITEM.SetIndexByValue(dv[0]["ITEM_CODE"].ToString());

                txtDESC.Text = dv[0]["ITEM_DESC"].ToString();
                txtQTY.Text = dv[0]["ITEM_QTY"].ToString();
                txtDetRemarks.Text = dv[0]["REMARKS"].ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    protected void GridViewItemTrn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItemReceiptRow(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItemIssueRow(UniqueId);
                BindGridFromDataTable();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Editing/ deletion of Material detail.\r\nSee error log for detail."));
        }
    }


}
