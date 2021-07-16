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
using errorLog;
using Common;
using Obout.ComboBox;

public partial class Module_Fiber_Controls_Fiber_Pallet_Return : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    DataTable  dtDetailTBL = null ;
    private string  TRN_TYPE = "IPT01"; 
    
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
        ActivateSaveMode();
        txtchallandate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
        BindNewChallanNum();
        ddlChallanNumber.Visible = false;
        txtPartyCode.SelectedIndex = -1;
        txtTransporterCode.SelectedIndex = -1;
        txtPartyCode1.Text = string.Empty;
        txtPartyAddress.Text = string.Empty;
        txtTransporterCode1.Text = string.Empty;
        Session["dtPalletReturn"] = null;
        ViewState["dtDetailTBL"] = null;
        BindGridFromDataTable();
        RefreshDetailRow();

    }

    protected void ddlchallannumber_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            thisTextBox.SelectedIndex = 0;
            thisTextBox.Items.Clear();
            DataTable data = new DataTable();
            data =GetIssueData(e.Text.ToUpper(), e.ItemsOffset, 10);       
            thisTextBox.DataSource = data;
            thisTextBox.DataTextField = "ISSUE_TRN_NUMB";
            thisTextBox.DataValueField= "ISSUE_TRN_NUMB";
            thisTextBox.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = data.Rows.Count; 
        }

        catch (Exception ex)

        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
          
        }
    }

    protected DataTable GetIssueData(string text, int startOffset, int numberOfItems)
    {

        try
        {




            string whereClause = " AND A.PRTY_CODE=V.PRTY_CODE  AND A.ISSUE_COMP_CODE='" + oUserLoginDetail.COMP_CODE + "' AND A.ISSUE_BRANCH_CODE='" + oUserLoginDetail.CH_BRANCHCODE + "' AND A.ISSUE_YEAR=" + oUserLoginDetail.DT_STARTDATE.Year + "  AND (A.ISSUE_TRN_NUMB LIKE :SearchQuery  OR  V.PRTY_NAME LIKE :SearchQuery OR  A.ISSUE_TRN_TYPE LIKE :SearchQuery OR TO_CHAR(A.ISSUE_TRN_DATE,'dd/MM/yyy') LIKE :SearchQuery) ";
            string sortExpression = "  ORDER BY A.ISSUE_TRN_NUMB ASC";
            string commandText = " Select A.ISSUE_TRN_NUMB,A.ISSUE_TRN_TYPE,A.ISSUE_TRN_DATE,V.PRTY_NAME from TX_PALLET_RETURN_MST a, TX_VENDOR_MST V where 1=1 ";
            string SearchQuery = "%" + text + "%";
            return SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetDataForPalletReturn(commandText, whereClause, sortExpression, "", SearchQuery, "");           
        }
        catch
        {
            throw;
        }

    }

    protected int GetItemsCountreturn(string text)
    {


        string CommandText = "SELECT  A.ISSUE_TRN_NUMB AS TRN_NUMB,A.ISSUE_TRN_TYPE AS TRN_TYPE FROM   TX_PALLET_RETURN_MST A";
        string WhereClause = "";
        string SortExpression = "";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetDataForPalletReturn(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        
    }
   
    private void BindNewChallanNum()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_PALLET_RETURN_MST oTX_PALLET_RETURN_MST = new SaitexDM.Common.DataModel.TX_PALLET_RETURN_MST();
            oTX_PALLET_RETURN_MST.ISSUE_TRN_TYPE = TRN_TYPE;
            oTX_PALLET_RETURN_MST.ISSUE_COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_PALLET_RETURN_MST.ISSUE_BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_PALLET_RETURN_MST.ISSUE_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            txtChallanNumber.Text = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetNewChallaNumber(oTX_PALLET_RETURN_MST).ToString();

        }
        catch
        {

            throw;
        }
    }    

    protected void ddlchallanNumber_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
           
            int PRTY_CH_NUMB = 0;
            int.TryParse(ddlChallanNumber.SelectedValue, out  PRTY_CH_NUMB);
            int iRecordFound = GetdataByChallaNumber(PRTY_CH_NUMB);
            BindGridFromDataTable();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selection of Challan Number.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }
    
    }    

    protected void cmbPalletRec_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable dat = BindpalletreturnCombo(e.Text.ToUpper(), e.ItemsOffset);
            if (dat != null && dat.Rows.Count > 0)
            {
                cmbPalletRec.Items.Clear();
                cmbPalletRec.DataSource = dat;
                cmbPalletRec.DataTextField = "TRN_NUMB";
                cmbPalletRec.DataValueField = "TRN_DATA";
                cmbPalletRec.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + dat.Rows.Count;           
            e.ItemsCount = GetItemsCountreturn(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in item selection.\r\nSee error log for detail."));
             lblMode.Text = ex.ToString();
        }
    }

    protected void cmbPalletRec_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {        
        var Desc = cmbPalletRec.SelectedValue.ToString();
        string[] words = Desc.Split('@');
        txtmrnno.Text = words[0].ToString();
        txtmencode.Text = words[1].ToString();
        txtpalletcode.Text = words[2].ToString();
        txtMergeNo.Text = words[3].ToString();         
    }

    private DataTable BindpalletreturnCombo(string text, int startOffset)
    {
        try
        {
            string whereClause = " ";
            string sortExpression = " ORDER BY TRN_NUMB";
            string commandText = " SELECT   asd.TRN_NUMB,  asd.TRN_TYPE,  asd.PALLET_CODE,  asd.MERGE_NO,  TRN_DATA  FROM   ( SELECT DISTINCT  (A.TRN_NUMB || '@' || A.TRN_TYPE || '@' || A.PALLET_CODE|| '@' || A.LOT_NO)       TRN_DATA,   A.TRN_NUMB, A.TRN_TYPE,    A.PALLET_CODE,     A.LOT_NO AS MERGE_NO         FROM   TX_FIBER_IR_TRN_SUB A WHERE NVL(A.RET_PALLET,0)=0 AND NVL(A.NO_OF_UNIT,0)=NVL(A.ISS_NO_OF_UNIT,0) AND SUBSTR(TRN_TYPE,1,1) IN ('R','O') ) asd WHERE (upper(TRN_NUMB) like :SearchQuery or upper(PALLET_CODE) like :SearchQuery OR upper(MERGE_NO) like :SearchQuery OR upper(TRN_TYPE) LIKE  :SearchQuery) AND ROWNUM<=15";
            if (startOffset != 0)
            {
                whereClause += " WHERE   TRN_DATA NOT IN  (SELECT   TRN_DATA FROM   ( SELECT   asd.TRN_NUMB,  asd.TRN_TYPE,  asd.PALLET_CODE,  asd.MERGE_NO,  TRN_DATA  FROM   ( SELECT DISTINCT  (A.TRN_NUMB || '@' || A.TRN_TYPE || '@' || A.PALLET_CODE|| '@' || A.LOT_NO)       TRN_DATA,   A.TRN_NUMB, A.TRN_TYPE,    A.PALLET_CODE,     A.LOT_NO AS MERGE_NO         FROM   TX_FIBER_IR_TRN_SUB A WHERE NVL(A.RET_PALLET,0)=0 AND NVL(A.NO_OF_UNIT,0)=NVL(A.ISS_NO_OF_UNIT,0) AND SUBSTR(TRN_TYPE,1,1) IN ('R','O') ) asd WHERE (upper(TRN_NUMB) like :SearchQuery or upper(PALLET_CODE) like :SearchQuery OR upper(MERGE_NO) like :SearchQuery OR upper(TRN_TYPE) LIKE  :SearchQuery) dss WHERE  ROWNUM <= " + startOffset + " )";
            }
            return SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetDataForPalletReturn(commandText, whereClause, sortExpression, "", '%' + text.ToUpper() + '%', "");
        
        }
        catch
        {
            throw;
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

    protected void btnSubDetail_Click(object sender, EventArgs e)
    {

        try
        {
            
            if (txtmrnno.Text.ToString() != "")
            {
                string URL = "";
                URL = "/Saitex/Module/Fiber/Pages/Fiber_adjustment_pallet_return.aspx";
                URL = URL + "?TRN_NUMB=" + txtmrnno.Text.Trim();
                URL = URL + "&TRN_TYPE=" + txtmencode.Text.Trim();
                URL = URL + "&MERGE_NO=" + txtMergeNo.Text.Trim();
                URL = URL + "&PALLET_CODE=" + txtpalletcode.Text.Trim();
                URL = URL + "&RETURN_TRN_NUMB=" + txtChallanNumber.Text;
                URL = URL + "&RETURN_TRN_TYPE=" + TRN_TYPE;
                URL = URL + "&txtid=" + txtQTY.ClientID;              
                
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=450,height=450,left=350,top=100');", true);

            }
            else
            {
                CommonFuction.ShowMessage("Please select to adjust pallets to return");
            }


        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting adjustment.\r\nSee error log for detail."));
        }
    
    }
    
    protected void txtPartyCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);
            txtPartyCode.Items.Clear();
            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }
    }
   
    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression,"", SearchQuery,"");

        return data.Rows.Count;
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    
    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyAddress.Text = txtPartyCode.SelectedValue.Trim();
            txtPartyCode1.Text = txtPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();
        }
    }
   
    protected void txtTransporterCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            DataTable data = GetTransporterData(e.Text.ToUpper(), e.ItemsOffset);
            txtTransporterCode.Items.Clear();
            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetTransporterCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected int GetTransporterCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = "%" + text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    private DataTable GetTransporterData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = "%" + Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
   
    protected void txtTransporterCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {


        try
        {
            txtTransporterName.Text = txtTransporterCode.SelectedValue;
            txtTransporterCode1.Text = txtTransporterCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    private DataTable createTrnTable()
    {
        DataTable dtDetailTBL = new DataTable();
        dtDetailTBL.Columns.Add("UNIQUE_ID", typeof(int));
        dtDetailTBL.Columns.Add("COMP_CODE", typeof(string));
        dtDetailTBL.Columns.Add("BRANCH_CODE", typeof(string));
        dtDetailTBL.Columns.Add("YEAR", typeof(int));        
        dtDetailTBL.Columns.Add("TRN_NUMB", typeof(int));
        dtDetailTBL.Columns.Add("TRN_TYPE", typeof(string));
        dtDetailTBL.Columns.Add("MERGE_NO", typeof(string));
        dtDetailTBL.Columns.Add("PALLET_CODE", typeof(string));
        dtDetailTBL.Columns.Add("NO_OF_PALLET", typeof(double));        
        dtDetailTBL.Columns.Add("CHALLAN_NUMB", typeof(int));
        dtDetailTBL.Columns.Add("CHALLAN_TYPE", typeof(string));
        dtDetailTBL.Columns.Add("CHALLAN_DATE", typeof(DateTime));
        dtDetailTBL.Columns.Add("REMARKS", typeof(string));        
        return dtDetailTBL;
    }
    
    protected void Savebtn_Click(object sender, EventArgs e)
    {
        try
        {

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            else 
             dtDetailTBL= createTrnTable();
            if (dtDetailTBL.Rows.Count < 15)
            {
                if (txtmrnno.Text != "" && txtpalletcode.Text != "" && txtMergeNo.Text != "")
                {
                    int UNIQUE_ID = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());
                    bool bb = SearchItemCodeInGrid(UNIQUE_ID);
                    if (!bb)
                    {
                        double Qty = 0;
                        int _challan_no = 0;
                        int _trn_numb = 0;
                        double.TryParse(txtQTY.Text.Trim(), out Qty);
                        int.TryParse(txtChallanNumber.Text, out _challan_no);
                        int.TryParse(txtmrnno.Text.Trim(),out _trn_numb );

                        if (Qty > 0)
                        {
                            if (UNIQUE_ID > 0)
                            {
                                DataView dv = new DataView(dtDetailTBL);
                                dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                                if (dv.Count > 0)
                                {
                                    dv[0]["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                    dv[0]["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE ;
                                    dv[0]["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                    dv[0]["TRN_NUMB"] = _trn_numb;
                                    dv[0]["TRN_TYPE"] = txtmencode.Text.Trim();
                                    dv[0]["MERGE_NO"] = txtMergeNo.Text.Trim();
                                    dv[0]["PALLET_CODE"] = txtpalletcode.Text.Trim();
                                    dv[0]["NO_OF_PALLET"] = Qty;
                                    dv[0]["CHALLAN_NUMB"] = _challan_no;
                                    dv[0]["CHALLAN_TYPE"] = TRN_TYPE;
                                    dv[0]["CHALLAN_DATE"] = DateTime.Now.Date;
                                    dv[0]["REMARKS"] = "";                                     
                                    dtDetailTBL.AcceptChanges();
                                }
                            }
                            else
                            {
                                DataRow dr = dtDetailTBL.NewRow();
                                dr["UNIQUE_ID"] = dtDetailTBL.Rows.Count + 1;
                                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                                dr["TRN_NUMB"] = _trn_numb;
                                dr["TRN_TYPE"] = txtmencode.Text.Trim();
                                dr["MERGE_NO"] = txtMergeNo.Text.Trim();
                                dr["PALLET_CODE"] = txtpalletcode.Text.Trim();
                                dr["NO_OF_PALLET"] = Qty;
                                dr["CHALLAN_NUMB"] = _challan_no;
                                dr["CHALLAN_TYPE"] = TRN_TYPE;
                                dr["CHALLAN_DATE"] = DateTime.Now.Date;
                                dr["REMARKS"] = "";                                     
                                dtDetailTBL.Rows.Add(dr);
                            }
                            ViewState["dtDetailTBL"] = dtDetailTBL;
                            RefreshDetailRow();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid poy code, it is already in use.');", true);
                    }
                }
                BindGridFromDataTable();
            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Purchase Order.");
            }  
                
        }
        catch(Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem adding pallet detail data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
        

    }

    private bool SearchItemCodeInGrid( int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdpallletrtnsave.Rows)
            {
                Label lbltrnno = (Label)grdRow.FindControl("lblmrnno");
                Label lbltrntype = (Label)grdRow.FindControl("lblmrntype");
                Label lblpalletcode = (Label)grdRow.FindControl("lblpalletcode");
                Label lblmergeno = (Label)grdRow.FindControl("lblmergeno");                
                LinkButton lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                int iUniqueId = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                if (lbltrnno.Text.Trim().ToUpper() == txtmrnno.Text.Trim().ToUpper() && lbltrntype.Text.Trim().ToUpper() == txtmencode.Text.Trim().ToUpper() && lblpalletcode.Text.Trim().ToUpper()==txtpalletcode.Text.Trim().ToUpper() && lblmergeno.Text.Trim().ToUpper()==txtMergeNo.Text.Trim().ToUpper() && UniqueId != iUniqueId)
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
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
    }

    protected void RefreshDetailRow()
    {
        ViewState["UNIQUE_ID"] = null;
        txtmrnno.Text = string.Empty;
        txtmencode.Text = string.Empty;
        txtMergeNo.Text = string.Empty;
        txtpalletcode.Text = string.Empty;
        cmbPalletRec.SelectedIndex = -1;
        txtQTY.Text = string.Empty;
    
    }
    
    protected void grdpallletrtnsave_RowCommand(object sender, GridViewCommandEventArgs e)
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
            lblMode.Text = ex.ToString();
        }

    }
    
    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            else dtDetailTBL = createTrnTable();
            grdpallletrtnsave.DataSource = dtDetailTBL;
            grdpallletrtnsave.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void EditItemReceiptRow(int UNIQUE_ID)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
            if (dv.Count > 0)
            {
                ViewState["UNIQUE_ID"] = UNIQUE_ID;
                txtmrnno.Text = dv[0]["TRN_NUMB"].ToString();
                txtmencode.Text = dv[0]["TRN_TYPE"].ToString();
                txtpalletcode.Text = dv[0]["PALLET_CODE"].ToString();
                txtMergeNo.Text = dv[0]["MERGE_NO"].ToString();
                txtQTY.Text = dv[0]["NO_OF_PALLET"].ToString(); 
            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteItemIssueRow(int UniqueId)
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
                    int iUniqueId = int.Parse(dr["UNIQUE_ID"].ToString());
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
                    dr["UNIQUE_ID"] = iCount;
                }
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        bool result = true;
        msg = string.Empty;
        if (string.IsNullOrEmpty(txtChallanNumber.Text))
        {
            msg = "please enter challan number.";
            result = false;
        }
        if (string.IsNullOrEmpty(txtPartyCode1.Text))
        {
            msg = "please select party";
            result = false;
        }

        if (grdpallletrtnsave.Rows.Count < 1)
        {
            msg = "please add pallate details.";
            result = false;
        
        }        

            return result;
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                savePalletReturn();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
               
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }

    }

    private void savePalletReturn()
    {
        try
        {

            SaitexDM.Common.DataModel.TX_PALLET_RETURN_MST oTX_PALLET_RETURN_MST = new SaitexDM.Common.DataModel.TX_PALLET_RETURN_MST();
            oTX_PALLET_RETURN_MST.ISSUE_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_PALLET_RETURN_MST.ISSUE_COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_PALLET_RETURN_MST.ISSUE_BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_PALLET_RETURN_MST.ISSUE_TRN_TYPE = TRN_TYPE;
            int chnumb = 0;
            int.TryParse(txtChallanNumber.Text, out chnumb);
            oTX_PALLET_RETURN_MST.ISSUE_TRN_NUMB = chnumb;
            oTX_PALLET_RETURN_MST.ISSUE_TRN_DATE = DateTime.Now;
            oTX_PALLET_RETURN_MST.PRTY_CH_NUMB = txtChallanNumber.Text;
            oTX_PALLET_RETURN_MST.PRTY_CH_DATE = DateTime.Now;
            oTX_PALLET_RETURN_MST.PRTY_CODE = txtPartyCode1.Text;
            oTX_PALLET_RETURN_MST.TRSP_CODE = txtTransporterCode1.Text;
            oTX_PALLET_RETURN_MST.RCOMMENT = string.Empty;
            oTX_PALLET_RETURN_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_PALLET_RETURN_MST.SHIFT = string.Empty;
            oTX_PALLET_RETURN_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_PALLET_RETURN_MST.TDATE = DateTime.Now;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            DataTable dtPalletReturn = (DataTable)Session["dtPalletReturn"];
            int TRN_NUMB = 0;
            bool Result = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.Insert(oTX_PALLET_RETURN_MST, out TRN_NUMB, dtDetailTBL, dtPalletReturn);
            if (Result)
            {
                InitialisePage();
                CommonFuction.ShowMessage(@"Issue Number : " + TRN_NUMB + " saved successfully.");

            }
            else
            {
                CommonFuction.ShowMessage("data  Saving Failed");
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdatePalletReturn();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void UpdatePalletReturn()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_PALLET_RETURN_MST oTX_PALLET_RETURN_MST = new SaitexDM.Common.DataModel.TX_PALLET_RETURN_MST();
            oTX_PALLET_RETURN_MST.ISSUE_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_PALLET_RETURN_MST.ISSUE_COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_PALLET_RETURN_MST.ISSUE_BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_PALLET_RETURN_MST.ISSUE_TRN_TYPE = TRN_TYPE;
            int chnumb = 0;
            int.TryParse(txtChallanNumber.Text, out chnumb);
            oTX_PALLET_RETURN_MST.ISSUE_TRN_NUMB = chnumb;
            oTX_PALLET_RETURN_MST.ISSUE_TRN_DATE = DateTime.Now;
            oTX_PALLET_RETURN_MST.PRTY_CH_NUMB = txtChallanNumber.Text;
            oTX_PALLET_RETURN_MST.PRTY_CH_DATE = DateTime.Now;
            oTX_PALLET_RETURN_MST.PRTY_CODE = txtPartyCode1.Text;
            oTX_PALLET_RETURN_MST.TRSP_CODE = txtTransporterCode1.Text;
            oTX_PALLET_RETURN_MST.RCOMMENT = string.Empty;
            oTX_PALLET_RETURN_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_PALLET_RETURN_MST.SHIFT = string.Empty;
            oTX_PALLET_RETURN_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_PALLET_RETURN_MST.TDATE = DateTime.Now;

            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            DataTable dtPalletReturn = (DataTable)Session["dtPalletReturn"]; 
            bool result = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.Update(oTX_PALLET_RETURN_MST, dtDetailTBL, dtPalletReturn);
            if (result)
            {
                InitialisePage();
                CommonFuction.ShowMessage("Data updated Successfully");
            }
            else
            {
                CommonFuction.ShowMessage("data updation Failed");
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
            ActivateUpdateMode();
           
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updation mode.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void ActivateSaveMode()
    {
        try
        {
            txtChallanNumber.Text = string.Empty;
            txtChallanNumber.Visible = true;
            ddlChallanNumber.SelectedIndex = -1;  
            ddlChallanNumber.Visible = false;
            lblMode.Text = "You are in Save Mode";      
            tdUpdate.Visible = false;           
            tdSave.Visible = true;
           
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
            txtChallanNumber.Text = string.Empty;
            txtChallanNumber.Visible = false;
            ddlChallanNumber.SelectedIndex = -1; 
            ddlChallanNumber.Visible = true;
            RefreshDetailRow();
            lblMode.Text = "You are in Update Mode";           
            tdUpdate.Visible = true;
            tdSave.Visible = false;

        }
        catch
        {
            throw;
        }
    }

    private int GetdataByChallaNumber(int ChallanNumber)
    {
        int iRecordFound = 0;
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];

            DataTable dt = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetPalletReturnDataByTRN_NUMB(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtChallanNumber.Text = dt.Rows[0]["ISSUE_TRN_NUMB"].ToString().Trim();
                txtPartyCode1.Text = dt.Rows[0]["PRTY_CODE"].ToString().Trim();
                txtPartyAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                txtTransporterCode1.Text = dt.Rows[0]["TRSP_CODE"].ToString().Trim();
                txtTransporterName.Text = dt.Rows[0]["TRSP_NAME"].ToString().Trim();   
              
            }
            if (iRecordFound == 1)
            {
               
                dtDetailTBL = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetPalletReturnTrnDataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, ChallanNumber, TRN_TYPE);
                ViewState["dtDetailTBL"] = dtDetailTBL;
                MapDataTable();

                if (dtDetailTBL.Rows.Count > 0)
                {
                    Session["dtPalletReturn"] = SaitexBL.Interface.Method.TX_PALLET_RETURN_MST.GetAdjpalletReturnByTrnNumb(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, TRN_TYPE, txtChallanNumber.Text,  oUserLoginDetail.DT_STARTDATE.Year);
     
                }
                
            }
            else
            {
                string msg = "Dear " + oUserLoginDetail.Username + " !! Pallet Return has already approved. Modification not allowed.";
                Common.CommonFuction.ShowMessage(msg);
                InitialisePage(); 
                ActivateUpdateMode();
                RefreshDetailRow();
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }

    private void MapDataTable()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
           
            if (!dtDetailTBL.Columns.Contains("UNIQUE_ID"))
                dtDetailTBL.Columns.Add("UNIQUE_ID", typeof(int));
            
            if (!dtDetailTBL.Columns.Contains("CHALLAN_NUMB"))
                dtDetailTBL.Columns.Add("CHALLAN_NUMB", typeof(string));
            
            if (!dtDetailTBL.Columns.Contains("CHALLAN_TYPE"))
                dtDetailTBL.Columns.Add("CHALLAN_TYPE", typeof(string));

            if (!dtDetailTBL.Columns.Contains("CHALLAN_DATE"))
                dtDetailTBL.Columns.Add("CHALLAN_DATE", typeof(DateTime));

            for (int iLoop = 0; iLoop < dtDetailTBL.Rows.Count; iLoop++)
            {
                dtDetailTBL.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
                dtDetailTBL.Rows[iLoop]["CHALLAN_NUMB"] = dtDetailTBL.Rows[iLoop]["ISSUE_TRN_NUMB"];
                dtDetailTBL.Rows[iLoop]["CHALLAN_TYPE"] = dtDetailTBL.Rows[iLoop]["ISSUE_TRN_TYPE"];
                dtDetailTBL.Rows[iLoop]["CHALLAN_DATE"] =DateTime.Now;
            }
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Reports/Fiber_PalletReturn_Findby.aspx?TRN_TYPE=IPT01");

    }
    protected void imgbtnaddlist_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Queries/Fiber_Search_Pallet_Return.aspx", false);
    }
}
