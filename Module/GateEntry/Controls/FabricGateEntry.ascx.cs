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
public partial class Module_GateEntry_Controls_FabricGateEntry : System.Web.UI.UserControl
{
    
    private string _TRNTYPE;
    private DataTable dtDetailTBL = null;
    public string TRNTYPE
    {
        get { return _TRNTYPE; }
        set { _TRNTYPE = value; }
    }
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_Gate_MST oTXGateMST = new SaitexDM.Common.DataModel.TX_Gate_MST();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
              
                if (!IsPostBack)
                {

                    InitialisePage();

                }
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));

        }
    }
    private void InitialisePage()
    {
        try
        {
            cmbGatedetails.Visible = false;
            txtGateRunningNo.Visible = true; 
            CreateDataTable();
            txtGateDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lblMode.Text = "Save";
            txtCheckBy.Text = "";
            txtDocAmount.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtDriverName.Text = string.Empty;

            CheckBox1.Checked = false;
            tdSelectPO.Visible = false;
            tdSelectPODrop.Visible = false;

            txtGateRunningNo.Text = string.Empty;
            txtLorryno.Text = string.Empty; txtPartyCode.SelectedIndex = -1;
            txtPartyName.Text = string.Empty;

            txtRemarks.Text = string.Empty;
            txtSecurityIncharge.Text = string.Empty;
            ddlTranType.SelectedIndex = -1;


            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            //bindUOM("UOM");
            bindGateTrnType("GATE_TRN_TYPE");

            //if (dtitemdetail == null || dtitemdetail.Rows.Count > 0)
            //{
            //    CreateIndentDetailTable();
            //}
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;;
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
            grdMaterialGateIn.DataSource = null;
            grdMaterialGateIn.DataBind();
            txtGateRunningNo.Text = SaitexBL.Interface.Method.TX_Gate_MST.GetNewGateNo(oTXGateMST);
        }
        catch
        {
            throw;
        }
    }
    public void bindGateTrnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE );
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "MST_CODE='" + TRNTYPE.Trim() + "'";

                ddlTranType.Items.Clear();
                ddlTranType.DataSource = dv;
                ddlTranType.DataTextField = "MST_CODE";
                ddlTranType.DataValueField = "MST_CODE";
                ddlTranType.DataBind();
                ddlTranType.Items.Insert(0, new ListItem("------Select------", "0"));
                ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByText(TRNTYPE.Trim()));

                lblHeading.Text = TRNTYPE.Trim();
            }

        }
        catch
        {
            throw;
        }


    }

    protected void ddlPartyCode_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select PRTY_CODE,PRTY_NAME,PRTY_ADD1,(PRTY_NAME || PRTY_ADD1) Address from (select * from TX_VENDOR_MST where Del_Status=0) asd";
            string WhereClause = "  where PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or PRTY_ADD1 like :SearchQuery";
            string SortExpression = " order by PRTY_CODE asc";
            string SearchQuery = e.Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            txtPartyCode.Items.Clear();

            txtPartyCode.DataSource = data;
            txtPartyCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Party Code Loading.\r\nSee error log for detail."));

        }
    }
    protected void txtPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
            DataTable dtpo = GetPOData("");
            if (dtpo != null && dtpo.Rows.Count > 0)
            {

                CMBPO.Items.Clear();
                CMBPO.DataSource = dtpo;
                CMBPO.DataBind();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Party Code Selection.\r\nSee error log for detail."));

        }
    }
    protected void cmbGatedetails_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select * from (select GATE_NUMB,GATE_DATE,PRTY_CODE,PRTY_NAME,ITEM_TYPE  from tx_Gate_mst where ITEM_TYPE='" + TRNTYPE + "')a  ";
            string WhereClause = "  where GATE_NUMB like :SearchQuery or GATE_DATE like :SearchQuery or PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or ITEM_TYPE like :SearchQuery";
            string SortExpression = " order by GATE_NUMB asc";
            string SearchQuery = e.Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            cmbGatedetails.Items.Clear();

            cmbGatedetails.DataSource = data;
            cmbGatedetails.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Gate Detail Loading on find.\r\nSee error log for detail."));
        }

    }
    protected void cmbGatedetails_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            cmbGatedetails.Visible = true;

            oTXGateMST.GATE_NUMB = Convert.ToInt64(cmbGatedetails.SelectedText.ToString());
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;;
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
            DataTable dtGatedetails = SaitexBL.Interface.Method.TX_Gate_MST.GetGateInMaster (oTXGateMST);
            if (dtGatedetails != null && dtGatedetails.Rows.Count > 0)
            {
                if (dtGatedetails != null && dtGatedetails.Rows.Count > 0)
                {
                    txtGateRunningNo.Text = dtGatedetails.Rows[0]["GATE_NUMB"].ToString();
                    txtGateDate.Text = dtGatedetails.Rows[0]["GATE_DATE"].ToString();
                    txtPartyCode.SelectedValue = dtGatedetails.Rows[0]["PRTY_CODE"].ToString();
                    txtPartyName.Text = dtGatedetails.Rows[0]["PRTY_NAME"].ToString();
                    txtDocNo.Text = dtGatedetails.Rows[0]["DOC_NO"].ToString();
                    txtDocDate.Text = dtGatedetails.Rows[0]["DOC_DATE"].ToString();
                    txtDocAmount.Text = dtGatedetails.Rows[0]["DOC_AMOUNT"].ToString();
                    txtLorryno.Text = dtGatedetails.Rows[0]["LORRY_NO"].ToString();
                    txtDriverName.Text = dtGatedetails.Rows[0]["DRIVER"].ToString();

                    txtSecurityIncharge.Text = dtGatedetails.Rows[0]["SECURITY_ENCHARGE"].ToString();
                    txtCheckBy.Text = dtGatedetails.Rows[0]["CHECK_BY"].ToString();

                    CMBPO.SelectedText = dtGatedetails.Rows[0]["PO_NUMB"].ToString();

                    CMBPO.SelectedValue = dtGatedetails.Rows[0]["PO_TYPE"].ToString() + "@" + dtGatedetails.Rows[0]["PO_DATE"].ToString();
                    ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByText(dtGatedetails.Rows[0]["ITEM_TYPE"].ToString()));
                    txtRemarks.Text = dtGatedetails.Rows[0]["REMARKS"].ToString();
                    DataTable dtGateTRNdetails = SaitexBL.Interface.Method.TX_Gate_MST.GetFABRICGateInTrn(oTXGateMST);
                    if (dtGateTRNdetails != null && dtGateTRNdetails.Rows.Count > 0)
                    {
                        MapDetailDataTable(dtGateTRNdetails);
                        BindMaterialGrid();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Gate Details Selection.\r\nSee error log for detail."));

        }

    }
    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            if (CheckBox1.Checked)
            {
                tdSelectPO.Visible = true;
                tdSelectPODrop.Visible = true;
            }
            else
            {
                tdSelectPO.Visible = false;
                tdSelectPODrop.Visible = false;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in CheckBox Changed.\r\nSee error log for detail."));

        }
    }
    protected DataTable GetPOData(string text)
    {
        try
        {
            DataTable dt = new DataTable();
            if (txtPartyCode.SelectedText != "")
            {

                string whereClause = " where PM.CONF_FLAG='1' and (nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0)) > 0 and pt.FABR_CODE = i.FABR_CODE and PT.PO_TYPE='PUM' and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' and pt.PO_NUMB like :SearchQuery and pt.FABR_CODE like :SearchQuery";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = "SELECT   DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.FABR_CODE) po_Fabric_trn,(pt.PO_TYPE ||'@'|| PM.PO_DATE)as Combined,pt.PO_TYPE, pt.PO_NUMB, pt.FABR_CODE,i.FABR_BASE, pt.ORD_QTY, pt.UOM, pm.CURRENCY_CODE,         pm.CONVERSION_RATE,         pt.DEL_DATE,         PM.PRTY_CODE,         pt.COMMENTS,         pt.BASIC_RATE,         pt.FINAL_RATE,         pt.PRC_TYPE,PM.YEAR,         NVL (PT.QTY_RCPT, 0) QTY_RCPT,         NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM  FROM   TX_FABRIC_PU_TRN pt, TX_FABRIC_MST i, tx_fabric_pu_mst pm";

                dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
                ///ViewState["PODATA"] = dt; 
            }
            else
            {
                CommonFuction.ShowMessage("Please select Party First");
            }


            return dt;
        }
        catch
        {
            throw;
        }
    }
    protected void CMBPO_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
            DataTable dtpo = GetPOData("");
            if (dtpo != null && dtpo.Rows.Count > 0)
            {

                CMBPO.Items.Clear();
                CMBPO.DataSource = dtpo;
                CMBPO.DataBind();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in PO Loading.\r\nSee error log for detail."));

        }


    }
    protected void CMBPO_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            string cString = CMBPO.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string PO_Type = arrString[0].ToString();
            int Po = int.Parse(CMBPO.SelectedText.ToString());
            string text = "";

            string whereClause = " where PM.CONF_FLAG='1' and (nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0)) > 0 and pt.FABR_CODE = i.FABR_CODE and PT.PO_TYPE='"+PO_Type+"' and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE='" + txtPartyCode.SelectedText.Trim() + "' and pt.PO_NUMB like :SearchQuery and pt.FABR_CODE like :SearchQuery";
            string sortExpression = " ORDER BY PO_NUMB";
            string commandText = "SELECT   DISTINCT (PT.PO_TYPE || '@' || PT.PO_NUMB || '@' || PT.FABR_CODE) po_Fabric_trn, pt.PO_NUMB, pt.FABR_CODE, i.FABR_BASE,pt.ORD_QTY, pt.UOM, pm.CURRENCY_CODE,   pt.PO_TYPE,      pm.CONVERSION_RATE,         pt.DEL_DATE,         PM.PRTY_CODE,         pt.COMMENTS,         pt.BASIC_RATE,         pt.FINAL_RATE,         pt.PRC_TYPE,PM.YEAR,         NVL (PT.QTY_RCPT, 0) QTY_RCPT,         NVL (PT.ORD_QTY, 0) - NVL (PT.QTY_RCPT, 0) AS QTY_REM  FROM   TX_FABRIC_PU_TRN pt, TX_FABRIC_MST i, tx_fabric_pu_mst pm";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "PO_NUMB=" + Po + " AND PO_TYPE='" + PO_Type + "'AND PRTY_CODE='" + txtPartyCode.SelectedText.ToString() + "'";
                if (dv != null && dv.Count > 0)
                {
                    DataTable dttemp = dv.ToTable();
                    MapDetailDataTable(dttemp);
                    grdMaterialGateIn.DataSource = dtDetailTBL;
                    grdMaterialGateIn.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem PO Selection .\r\nSee error log for detail."));

        }

    }
    protected DataTable GetItems(string text)
    {
        try
        {
            string whereClause = " WHERE FABR_CODE like :SearchQuery or FABR_TYPE like :SearchQuery or FABR_BASE like :SearchQuery";
            string sortExpression = " ORDER BY FABR_CODE";

            string commandText = "SELECT   *  FROM   (SELECT   FT.*, TX_FABRIC_OPSTAT.OP_RATE  FROM   TX_FABRIC_MST ft, TX_FABRIC_OPSTAT           WHERE   FT.FABR_CODE = TX_FABRIC_OPSTAT.FABR_CODE) tab";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }
    #region FabricGateINTrnTable()
    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            //dtDetailTBL.Columns.Add("TRNNUMB", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("FABR_CODE", typeof(string));
            dtDetailTBL.Columns.Add("FABR_BASE", typeof(string));
            dtDetailTBL.Columns.Add("UOM", typeof(string));

            dtDetailTBL.Columns.Add("ORD_QTY", typeof(double));

            dtDetailTBL.Columns.Add("BASIC_RATE", typeof(double));
            dtDetailTBL.Columns.Add("FINAL_RATE", typeof(double));
            dtDetailTBL.Columns.Add("RecivedQuanitity", typeof(double));

            dtDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtDetailTBL.Columns.Add("Year", typeof(int));
            ViewState["dtDetailTBL"] = dtDetailTBL;
        }
        catch
        {
            throw;
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (dtDetailTBL.Rows.Count < 15)
            {

                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                {
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                }
                bool bb = SearchParameterInMaterialGrid(txtPartyCode1.SelectedValue.ToString().Trim(), UniqueId);
                if (!bb)
                {


                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtDetailTBL);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["FABR_CODE"] = txtPartyCode1.SelectedValue.ToString().Trim();
                            dv[0]["FABR_BASE"] = txtDescription.Text.Trim();
                            dv[0]["UOM"] = txtUOm.Text.Trim();
                            double Quantity = 0f;
                            double.TryParse(txtQuantity.Text.Trim(), out Quantity);
                            dv[0]["ORD_QTY"] = Quantity;
                            dv[0]["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                            dv[0]["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim()) * double.Parse(txtRecievedQuantity.Text.Trim());
                            dv[0]["RecivedQuanitity"] = double.Parse(txtRecievedQuantity.Text.Trim());
                            dv[0]["REMARKS"] = txtDetailsRemarks.Text.Trim();
                            dtDetailTBL.AcceptChanges();
                        }
                    }
                    else
                    {

                        DataRow dr = dtDetailTBL.NewRow();
                        dr["UniqueId"] = dtDetailTBL.Rows.Count + 1;
                        dr["FABR_CODE"] = txtPartyCode1.SelectedValue.ToString().Trim();
                        dr["FABR_BASE"] = txtDescription.Text.Trim();
                        dr["UOM"] = txtUOm.Text.Trim();
                        double Quantity = 0f;
                        double.TryParse(txtQuantity.Text.Trim(), out Quantity);
                        dr["ORD_QTY"] = Quantity;
                        dr["BASIC_RATE"] = double.Parse(txtBasicRate.Text.Trim());
                        dr["FINAL_RATE"] = double.Parse(txtBasicRate.Text.Trim()) * double.Parse(txtRecievedQuantity.Text.Trim());
                        dr["RecivedQuanitity"] = double.Parse(txtRecievedQuantity.Text.Trim());
                        dr["REMARKS"] = txtDetailsRemarks.Text.Trim();
                        dtDetailTBL.Rows.Add(dr);
                    }
                    RefreshMaterialDetailRow();
                }


                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected ItemCode Already Added.Please Select Another');", true);
                }
                ViewState["dtDetailTBL"] = dtDetailTBL;
                BindMaterialGrid();




            }

            else
            {
                CommonFuction.ShowMessage("You have reached the limit Items. Only 15 Items allowed in one Gate IN.");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving Detail Row.\r\nSee error log for detail."));

        }

    }
    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshMaterialDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Cancel Detail Row.\r\nSee error log for detail."));

        }

    }

    private void FillMaterialTrnGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            DataView dv = new DataView(dtDetailTBL);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                BindItemsCombo();
                txtPartyCode1.SelectedValue = dv[0]["FABR_CODE"].ToString();
                txtDescription.Text = dv[0]["FABR_BASE"].ToString();
                txtUOm.Text = dv[0]["UOM"].ToString();
                txtQuantity.Text = dv[0]["ORD_QTY"].ToString();
                txtBasicRate.Text = dv[0]["BASIC_RATE"].ToString();
                txtFinalRate.Text = dv[0]["FINAL_RATE"].ToString();
                txtRecievedQuantity.Text = dv[0]["RecivedQuanitity"].ToString();
                txtDetailsRemarks.Text = dv[0]["REMARKS"].ToString();

                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;

        }
    }
    private void DeleteMaterialRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }
            if (grdMaterialGateIn.Rows.Count == 1)
            {
                dtDetailTBL.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtDetailTBL.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
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
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtDetailTBL"] = dtDetailTBL;
            }
        }
        catch
        {
            throw;
        }
    }
    private bool SearchParameterInMaterialGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdMaterialGateIn.Rows)
            {
                Label txtICODE = (Label)grdRow.FindControl("txtICODE");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkbtnDel");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtICODE.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }
    private void BindMaterialGrid()
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }


            grdMaterialGateIn.DataSource = dtDetailTBL;
            grdMaterialGateIn.DataBind();


        }
        catch
        {
            throw;
        }
    }
    protected void grdMaterialGateIn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {


            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                FillMaterialTrnGrid(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {

                DeleteMaterialRow(UniqueId);
                BindMaterialGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in  GridView Row Command.\r\nSee error log for detail."));

        }

    }

    private void RefreshMaterialDetailRow()
    {
        try
        {
            txtPartyCode1.SelectedIndex = -1;
            txtDescription.Text = "";
            txtUOm.Text = "";
            txtBasicRate.Text = "";
            txtFinalRate.Text = "";
            txtQuantity.Text = "";
            txtRecievedQuantity.Text = "";
            txtDetailsRemarks.Text = "";
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;

        }

    }
    private void MapDetailDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (dtDetailTBL == null || dtDetailTBL.Rows.Count == 0)
                CreateDataTable();
            dtDetailTBL.Rows.Clear();

            int currentyear = oUserLoginDetail.DT_STARTDATE.Year;;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    if (currentyear == int.Parse(drTemp["Year"].ToString()))
                    {
                        DataRow dr = dtDetailTBL.NewRow();
                        dr["UniqueId"] = dtDetailTBL.Rows.Count + 1;
                        dr["FABR_CODE"] = drTemp["FABR_CODE"];
                        dr["FABR_BASE"] = drTemp["FABR_BASE"];
                        dr["UOM"] = drTemp["UOM"];
                        if (drTemp["ORD_QTY"].ToString() != string.Empty)
                        {
                            dr["ORD_QTY"] = drTemp["ORD_QTY"];
                        }
                        else
                        {
                            dr["ORD_QTY"] = 0f;

                        }
                        dr["BASIC_RATE"] = drTemp["BASIC_RATE"];
                        dr["FINAL_RATE"] = drTemp["FINAL_RATE"];
                        if (CheckBox1.Checked != true)
                        {
                            dr["RecivedQuanitity"] = drTemp["GATE_QTY"];
                        }
                        else
                        {
                            dr["RecivedQuanitity"] = drTemp["ORD_QTY"];
                        }
                        dr["Year"] = drTemp["Year"];
                        dr["REMARKS"] = drTemp["COMMENTS"];


                        dtDetailTBL.Rows.Add(dr);
                    }
                }
                ViewState["dtDetailTBL"] = dtDetailTBL;
                dtTemp = null;

            }
        }
        catch
        {
            throw;
        }
    }
    #endregion
    protected void txtPartyCode1_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            // Obout.ComboBox.ComboBox thisTextBox = (Obout.ComboBox.ComboBox)sender;
            BindItemsCombo();
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Fabric Code Loading.\r\nSee error log for detail."));
        }

    }
    protected void txtPartyCode1_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string text = "";
            string whereClause = " WHERE FABR_CODE like :SearchQuery or FABR_TYPE like :SearchQuery or FABR_BASE like :SearchQuery";
            string sortExpression = " ORDER BY FABR_CODE";

            string commandText = "SELECT   * FROM   (SELECT   FT.*, TX_FABRIC_OPSTAT.OP_RATE            FROM   TX_FABRIC_MST ft, TX_FABRIC_OPSTAT           WHERE   FT.FABR_CODE = TX_FABRIC_OPSTAT.FABR_CODE) tab";

            string sPO = "";

            DataTable dtItemDescription = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            if (dtItemDescription != null && dtItemDescription.Rows.Count > 0)
            {
                DataView dv = new DataView(dtItemDescription);
                dv.RowFilter = "FABR_CODE='" + txtPartyCode1.SelectedText.Trim() + "'";
                if (dv != null && dv.Count > 0)
                {
                    txtDescription.Text = dv[0]["FABR_BASE"].ToString();
                    txtUOm.Text = dv[0]["UOM"].ToString();
                    txtBasicRate.Text = dv[0]["OP_RATE"].ToString();

                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Fabric Code Selection.\r\nSee error log for detail."));

        }
    }
    protected void BindItemsCombo()
    {
        try
        {
            txtPartyCode1.SelectedIndex = 0;
            txtPartyCode1.Items.Clear();

            DataTable data = new DataTable();
            data = GetItems("");
            txtPartyCode1.DataSource = data;
            txtPartyCode1.DataBind();
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
            tdDelete.Visible = true;
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            cmbGatedetails.Visible = true;
            txtGateRunningNo.Visible = false;
            txtGateDate.Text = "";
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
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (txtPartyCode.SelectedIndex != -1)
            {
                if (dtDetailTBL.Rows.Count > 0)
                {
                    int iRecordFound = 0;

                    oTXGateMST.GATE_NUMB = Convert.ToInt64(txtGateRunningNo.Text.Trim());
                    oTXGateMST.GATE_TYPE = "IN";
                    oTXGateMST.GATE_DATE = DateTime.Parse(txtGateDate.Text.Trim());
                    oTXGateMST.PRTY_CODE = Common.CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
                    oTXGateMST.PRTY_NAME = Common.CommonFuction.funFixQuotes(txtPartyName.Text.Trim());
                    oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
                    oTXGateMST.DOC_NO = Common.CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
                    oTXGateMST.DOC_DATE = DateTime.Parse(Common.CommonFuction.funFixQuotes(txtDocDate.Text.Trim()));
                    double DOC_AMOUNT = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtDocAmount.Text.Trim()), out DOC_AMOUNT);
                    oTXGateMST.DOC_AMOUNT = DOC_AMOUNT;
                    oTXGateMST.LORRY_NO = Common.CommonFuction.funFixQuotes(txtLorryno.Text.Trim());
                    oTXGateMST.DRIVER = Common.CommonFuction.funFixQuotes(txtDriverName.Text.Trim());
                    oTXGateMST.SECURITY_ENCHARGE = Common.CommonFuction.funFixQuotes(txtSecurityIncharge.Text.Trim());
                    oTXGateMST.CHECK_BY = Common.CommonFuction.funFixQuotes(txtCheckBy.Text.Trim());
                    oTXGateMST.ENTER_BY = oUserLoginDetail.UserCode;

                    oTXGateMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                    oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
                    oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;;
                    oTXGateMST.TUSER = oUserLoginDetail.UserCode;
                    oTXGateMST.STATUS = true;
                    oTXGateMST.ISSUE_TYPE = "";

                    if (CheckBox1.Checked == true)
                    {
                        oTXGateMST.PO_NUMB = int.Parse(CMBPO.SelectedText.Trim());

                        string cString = CMBPO.SelectedValue.Trim();
                        char[] splitter = { '@' };
                        string[] arrString = cString.Split(splitter);

                        oTXGateMST.PO_TYPE = arrString[0].ToString();
                        oTXGateMST.PO_DATE = DateTime.Parse(arrString[1].ToString());


                    }
                    else
                    {
                        oTXGateMST.PO_TYPE = string.Empty;

                    }
                    bool result = SaitexBL.Interface.Method.TX_Gate_MST.UpdateFABRICGateIN(oTXGateMST, out iRecordFound, dtDetailTBL);
                    if (result)
                    {
                        InitialisePage();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Gate Entry Updated Successfully');", true);
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Gate Entry Already Exists ');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Gate Entry Is Not Updated');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Create  Fabric Details');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Select Party');", true);


            }

        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updation.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtDetailTBL"] != null)
            {
                dtDetailTBL = (DataTable)ViewState["dtDetailTBL"];
            }

            if (txtPartyCode.SelectedIndex != -1)
            {
                if (dtDetailTBL.Rows.Count > 0)
                {
                    int iRecordFound = 0;

                    oTXGateMST.GATE_NUMB = Convert.ToInt64(txtGateRunningNo.Text.Trim());
                    oTXGateMST.GATE_TYPE = "IN";
                    oTXGateMST.GATE_DATE = DateTime.Parse(txtGateDate.Text.Trim());
                    oTXGateMST.PRTY_CODE = Common.CommonFuction.funFixQuotes(txtPartyCode.SelectedText.Trim());
                    oTXGateMST.PRTY_NAME = Common.CommonFuction.funFixQuotes(txtPartyName.Text.Trim());
                    oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
                    oTXGateMST.DOC_NO = Common.CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
                    oTXGateMST.DOC_DATE = DateTime.Parse(Common.CommonFuction.funFixQuotes(txtDocDate.Text.Trim()));
                    double DOC_AMOUNT = 0;
                    double.TryParse(Common.CommonFuction.funFixQuotes(txtDocAmount.Text.Trim()), out DOC_AMOUNT);
                    oTXGateMST.DOC_AMOUNT = DOC_AMOUNT;
                    oTXGateMST.LORRY_NO = Common.CommonFuction.funFixQuotes(txtLorryno.Text.Trim());
                    oTXGateMST.DRIVER = Common.CommonFuction.funFixQuotes(txtDriverName.Text.Trim());
                    oTXGateMST.SECURITY_ENCHARGE = Common.CommonFuction.funFixQuotes(txtSecurityIncharge.Text.Trim());
                    oTXGateMST.CHECK_BY = Common.CommonFuction.funFixQuotes(txtCheckBy.Text.Trim());
                    oTXGateMST.ENTER_BY = oUserLoginDetail.UserCode;

                    oTXGateMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                    oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
                    oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;;
                    oTXGateMST.TUSER = oUserLoginDetail.UserCode;
                    oTXGateMST.STATUS = true;
                    oTXGateMST.ISSUE_TYPE = "";

                    if (CheckBox1.Checked == true)
                    {
                        oTXGateMST.PO_NUMB = int.Parse(CMBPO.SelectedText.Trim());

                        string cString = CMBPO.SelectedValue.Trim();
                        char[] splitter = { '@' };
                        string[] arrString = cString.Split(splitter);

                        oTXGateMST.PO_TYPE = arrString[0].ToString();
                        oTXGateMST.PO_DATE = DateTime.Parse(arrString[1].ToString());


                    }
                    else
                    {
                        oTXGateMST.PO_TYPE = string.Empty;

                    }


                    bool result = SaitexBL.Interface.Method.TX_Gate_MST.InsertFABRICGateIN(oTXGateMST, out iRecordFound, dtDetailTBL);
                    if (result)
                    {
                        InitialisePage();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Gate Entry Saved Successfully');", true);
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Gate Entry Already Exists ');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Fabric Gate Entry Is Not Saved');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Create Fabric Items Details');", true);

                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Select Party Details ');", true);

            
            }
            
          
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }


    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clearing Form Detail.\r\nSee error log for detail."));

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {

    }
}
