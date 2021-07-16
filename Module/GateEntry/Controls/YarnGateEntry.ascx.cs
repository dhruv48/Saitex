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
public partial class Module_GateEntry_Controls_YarnGateEntry : System.Web.UI.UserControl
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
                Response.Redirect("/Saitex/Default.aspx", false);

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
            bindGateTrnType("GATE_TRN_TYPE");
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
            grdMaterialGateIn.DataSource = null;
            grdMaterialGateIn.DataBind();
            txtGateRunningNo.Text = SaitexBL.Interface.Method.TX_Gate_MST.GetNewGateNo(oTXGateMST);
            CMBPO.SelectedIndex = -1; 
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
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE  );
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
    
    private void BindGateDetails()
    {

        try
        {
            string e = string.Empty; 
            string CommandText = "select * from (select GATE_NUMB,GATE_DATE,PRTY_CODE,PRTY_NAME,ITEM_TYPE  from tx_Gate_mst where ITEM_TYPE='" + TRNTYPE + "')a  ";
            string WhereClause = "  where GATE_NUMB like :SearchQuery or GATE_DATE like :SearchQuery or PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or ITEM_TYPE like :SearchQuery";
            string SortExpression = " order by GATE_NUMB desc";
            string SearchQuery = e.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            cmbGatedetails.Items.Clear();
            cmbGatedetails.DataTextField = "GATE_NUMB";
            cmbGatedetails.DataValueField = "GATE_NUMB"; 
            cmbGatedetails.DataSource = data;
            cmbGatedetails.DataBind();
            cmbGatedetails.Items.Insert(0, new ListItem("-------- Select Gate Details -------", "0"));

           
        }
        catch
        { 
        
        
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
                bindPO();
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
            if (txtPartyCode.SelectedItem != "Select")
            {

                string whereClause = " where PM.CONF_FLAG='1' and (nvl(PT.ORD_QTY,0)-nvl(PT.GATE_QTY,0)) > 0 and pt.YARN_CODE = i.YARN_CODE and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE='" + txtPartyCode.SelectedValue + "' and pt.PO_NUMB like :SearchQuery and pt.YARN_CODE like :SearchQuery";
                string sortExpression = " ORDER BY PO_NUMB";
                string commandText = "SELECT distinct PM.PO_NUMB,PM.PO_TYPE,(PM.PO_TYPE ||'@'|| PM.PO_DATE)as Combined FROM YRN_PU_TRN pt,YRN_MST i ,YRN_PU_MST pm ";

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
       
    private void bindPO()
    {
        try
        {
            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
            DataTable dtpo = GetPOData("");
            if (dtpo != null && dtpo.Rows.Count > 0)
            {

                CMBPO.Items.Clear();
                CMBPO.DataTextField = "PO_NUMB";
                CMBPO.DataValueField = "Combined";
                CMBPO.DataSource = dtpo;
                CMBPO.DataBind();
                CMBPO.Items.Insert(0, new ListItem("--------  Approved Po -------", "0"));

            }
            else
            {
                CMBPO.Items.Clear();
                CMBPO.Items.Insert(0, new ListItem("-------- NoApprovedPoExistforSelectedParty -------", "0"));

            
            }

        }
        catch
        { 

        }
    
    }
        
    protected DataTable GetItems(string text)
    {
        try
        {
            string whereClause = " WHERE YARN_CODE like :SearchQuery or YARN_TYPE like :SearchQuery or YARN_DESC like :SearchQuery";
            string sortExpression = " ORDER BY YARN_CODE";

            string commandText = "SELECT * FROM YRN_MST";

            string sPO = "";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);

            return dt;
        }
        catch
        {
            throw;
        }
    }
    
    #region YarnGateINTrnTable()
    private void CreateDataTable()
    {
        try
        {
            dtDetailTBL = new DataTable();
            dtDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtDetailTBL.Columns.Add("PO_NUMB", typeof(int));
            dtDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtDetailTBL.Columns.Add("YARN_DESC", typeof(string));
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
                            dv[0]["YARN_CODE"] = txtPartyCode1.SelectedValue.ToString().Trim();
                            dv[0]["YARN_DESC"] = txtDescription.Text.Trim();
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
                        dr["YARN_CODE"] = txtPartyCode1.SelectedValue.ToString().Trim();
                        dr["YARN_DESC"] = txtDescription.Text.Trim();
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
                txtPartyCode1.SelectedValue = dv[0]["YARN_CODE"].ToString();
                txtDescription.Text = dv[0]["YARN_DESC"].ToString();
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Material GridView Row Command.\r\nSee error log for detail."));

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

            int currentyear = oUserLoginDetail.DT_STARTDATE.Year;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    
                        DataRow dr = dtDetailTBL.NewRow();
                        dr["UniqueId"] = dtDetailTBL.Rows.Count + 1;
                        //dr["PO_NUMB"] = drTemp["PO_NUMB"];
                        dr["YARN_CODE"] = drTemp["YARN_CODE"];
                        dr["YARN_DESC"] = drTemp["YARN_DESC"];
                        dr["UOM"] = drTemp["UOM"];
                        if (drTemp["ORD_QTY"].ToString() != string.Empty)
                        {
                            dr["ORD_QTY"] = drTemp["QTY_REM"];
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
                            dr["RecivedQuanitity"] = drTemp["QTY_REM"];
                        }
                        //dr["Year"] = drTemp["Year"];
                        dr["REMARKS"] = drTemp["COMMENTS"];


                        dtDetailTBL.Rows.Add(dr);

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
            txtGateDate.Text = ""; BindGateDetails();
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
                    oTXGateMST.PRTY_CODE = Common.CommonFuction.funFixQuotes(txtPartyCode.SelectedValue.Trim());
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
                    oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;;
                    oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oTXGateMST.TUSER = oUserLoginDetail.UserCode;   
                    oTXGateMST.STATUS = true;
                    oTXGateMST.ISSUE_TYPE = "";

                    if (CheckBox1.Checked == true)
                    {
                        oTXGateMST.PO_NUMB = int.Parse(CMBPO.SelectedItem.ToString() );

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
                    bool result = SaitexBL.Interface.Method.TX_Gate_MST.UpdateYARNGateIN(oTXGateMST, out iRecordFound, dtDetailTBL);
                    if (result)
                    {
                        InitialisePage();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('YARN Gate Entry Updated Successfully');", true);
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('YARN Gate Entry Already Exists ');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('YARN Gate Entry Is Not Updated');", true);
                    }
                }
                else
                { 
                
                }

            }
            else
            { 
            
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
                    oTXGateMST.PRTY_CODE = Common.CommonFuction.funFixQuotes(txtPartyCode.SelectedValue.Trim());
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
                    oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;;
                    oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oTXGateMST.TUSER = oUserLoginDetail.UserCode;  
                    oTXGateMST.STATUS = true;
                    oTXGateMST.ISSUE_TYPE = "";


                    if (CheckBox1.Checked == true)
                    {
                        oTXGateMST.PO_NUMB = int.Parse(CMBPO.SelectedItem.ToString() );

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


                    bool result = SaitexBL.Interface.Method.TX_Gate_MST.InsertYARNGateIN(oTXGateMST, out iRecordFound, dtDetailTBL);
                    if (result)
                    {
                        InitialisePage();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('YARN Gate Entry Saved Successfully');", true);
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('YARN Gate Entry Already Exists ');", true);

                    }
                    else
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('YARN Gate Entry Is Not Saved');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Create Yarn Details');", true);
             
                
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Please Select Party');", true);

            
            }

        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }


    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {


            //string YarnDesc = txtyarndesc.Text.Trim();
            string Query_String = string.Empty;


            string URL = "../../GateEntry/Reports/YarnGateInReport.aspx?Query_String =" + Query_String;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
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

    protected override void OnInit(EventArgs e)
    {

        txtPartyCode.AutoPostBack = true;
        txtPartyCode.OnTextChanged += new CommonControls_PartyCodeLOV.RefreshDataGridView(txtPartyCode_OnTextChanged);
        
        txtPartyCode1.AutoPostBack = true;
        txtPartyCode1.OnTextChanged += new CommonControls_LOV_YarnCodeLov.RefreshDataGridView(txtPartyCode1_OnTextChanged);


    }
    
    void txtPartyCode_OnTextChanged(string Value, string Text)
    {
        try
        {
            txtPartyName.Text = txtPartyCode.SelectedItem;
            bindPO();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party selection.\r\nSee error log for detail."));
        }
    }
    
    void txtPartyCode1_OnTextChanged(string Value, string Text)
    {
        try
        {
            string text = "";
            string whereClause = " WHERE YARN_CODE like :SearchQuery or YARN_TYPE like :SearchQuery or YARN_DESC like :SearchQuery";
            string sortExpression = " ORDER BY YARN_CODE";

            string commandText = "Select * from (SELECT   FT.*, YARN_OP_BAL.OP_RATE  FROM    YRN_MST ft, YARN_OP_BAL WHERE       FT.YARN_CODE = YARN_OP_BAL.YARN_CODE)tab";

            string sPO = "";

            DataTable dtItemDescription = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            if (dtItemDescription != null && dtItemDescription.Rows.Count > 0)
            {
                DataView dv = new DataView(dtItemDescription);
                dv.RowFilter = "YARN_CODE='" + txtPartyCode1.SelectedValue.ToString()  + "'";
                if (dv != null && dv.Count > 0)
                {
                    txtDescription.Text = dv[0]["YARN_DESC"].ToString();
                    txtUOm.Text = dv[0]["UOM"].ToString();
                    txtBasicRate.Text = dv[0]["OP_RATE"].ToString();

                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Item Code Selection.\r\nSee error log for detail."));

        }
    }
    
    protected void cmbGatedetails_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            cmbGatedetails.Visible = true;

            oTXGateMST.GATE_NUMB = Convert.ToInt64(cmbGatedetails.SelectedItem.ToString());
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
            DataTable dtGatedetails = SaitexBL.Interface.Method.TX_Gate_MST.GetGateInMaster(oTXGateMST);
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
                    // CMBPO.SelectedItem  = dtGatedetails.Rows[0]["PO_NUMB"].ToString();

                    CMBPO.SelectedValue = dtGatedetails.Rows[0]["PO_TYPE"].ToString() + "@" + dtGatedetails.Rows[0]["PO_DATE"].ToString();

                    ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByText(dtGatedetails.Rows[0]["ITEM_TYPE"].ToString()));
                    txtRemarks.Text = dtGatedetails.Rows[0]["REMARKS"].ToString();
                    DataTable dtGateTRNdetails = SaitexBL.Interface.Method.TX_Gate_MST.GetYARNGateInTrn(oTXGateMST);
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

    protected void CMBPO_SelectedIndexChanged1(object sender, EventArgs e)
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
            int Po = int.Parse(CMBPO.SelectedItem.ToString());
            string text = "";

            string whereClause = " where PM.CONF_FLAG='1' and (nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0)) > 0 and pt.YARN_CODE = i.YARN_CODE and PT.PO_TYPE='" + PO_Type + "' and pm.PO_NUMB=PT.PO_NUMB and PM.PRTY_CODE='" + txtPartyCode.SelectedValue.Trim() + "' and pt.PO_NUMB like :SearchQuery and pt.YARN_CODE like :SearchQuery";
            string sortExpression = " ORDER BY PO_NUMB";
            string commandText = "SELECT distinct (PT.PO_TYPE||'@'||PT.PO_NUMB||'@'||PT.YARN_CODE) po_Item_trn,PM.PO_TYPE, pt.PO_NUMB, pt.YARN_CODE, pt.ORD_QTY, pt.UOM, pm.CURRENCY_CODE, pm.CONVERSION_RATE, pt.DEL_DATE,PM.PRTY_CODE,pt.COMMENTS, pt.BASIC_RATE, pt.FINAL_RATE, pt.PRC_TYPE, i.YARN_DESC,nvl(PT.QTY_RCPT,0) QTY_RCPT,nvl(PT.ORD_QTY,0)-nvl(PT.QTY_RCPT,0) as QTY_REM FROM YRN_PU_TRN pt, YRN_MST i, YRN_PU_MST pm ";

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(commandText, whereClause, sortExpression, "", text + "%", "");
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "PO_NUMB=" + Po + " AND PO_TYPE='" + PO_Type + "'AND PRTY_CODE='" + txtPartyCode.SelectedValue.ToString() + "'";
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

}

