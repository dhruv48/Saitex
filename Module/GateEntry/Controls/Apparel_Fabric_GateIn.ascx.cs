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
using DBLibrary;
using Common;
using errorLog;
using Obout.ComboBox;

public partial class Module_GateEntry_Controls_Apparel_Fabric_GateIn : System.Web.UI.UserControl
{
    private string _TRNTYPE;
    public string TRNTYPE
    {
        get { return _TRNTYPE; }
        set { _TRNTYPE = value; }
    }
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_Gate_MSTOLD oTXGateMST = new SaitexDM.Common.DataModel.TX_Gate_MSTOLD();

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;

        }
    }

    private void InitialisePage()
    {
        try
        {

            txtGateDate.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            lblMode.Text = "Save";
            txtCheckBy.Text = "";
            txtDocAmount.Text = string.Empty;
            txtDocDate.Text = string.Empty;
            txtDocNo.Text = string.Empty;
            txtDriverName.Text = string.Empty;
            txtGateRunningNo.Text = string.Empty;
            txtLorryno.Text = string.Empty;
            txtMaterialDetail.Text = string.Empty;
            txtPartyCode.SelectedIndex = -1;
            txtPartyName.Text = string.Empty;
            txtTransporterCode.SelectedIndex = -1;
            txtTransporterAddress.Text = string.Empty;
            lblPartyCode.Text = string.Empty;
            lblTransporterCode.Text = string.Empty;
            txtQty.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtSecurityIncharge.Text = string.Empty;
            ddlTranType.SelectedIndex = -1;
            ddlUOM.SelectedIndex = -1;

            tdSave.Visible = true;
            tdUpdate.Visible = false;
          
            bindUOM("UOM");
            bindGateTrnType("GATE_TRN_TYPE");
            cmbGatedetails.Visible = false;
            txtGateRunningNo.Visible = true;
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
            txtGateRunningNo.Text = SaitexBL.Interface.Method.TX_Gate_MST.GetNewGateNoOLDTABLE(oTXGateMST);
        }
        catch
        {
            throw;
        };
    }

    private void bindGateTrnType(string MST_NAME)
    {

        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
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

                if (string.Compare(TRNTYPE, "YARN PURCHASE IN", true) == 0)
                    lblHeading.Text = "Yarn Gate in Entry Form";
                else if (string.Compare(TRNTYPE, "MATERIAL IN", true) == 0)
                    lblHeading.Text = "Material Gate in Entry Form";
                else if (string.Compare(TRNTYPE, "FABRIC IN", true) == 0)
                    lblHeading.Text = "Fabric Gate in Entry Form";
                else if (string.Compare(TRNTYPE, "SEWING THREAD IN", true) == 0)
                    lblHeading.Text = "Sewing Thread Gate in Entry Form";
                else if (string.Compare(TRNTYPE, "APPAREL FABRIC IN", true) == 0)
                    lblHeading.Text = " Apparel Fabric Gate in Entry Form";
            }

        }
        catch
        {
            throw;
        }
    }

    private void bindUOM(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {


                ddlUOM.Items.Clear();
                ddlUOM.DataSource = dt;
                ddlUOM.DataTextField = "MST_CODE";
                ddlUOM.DataValueField = "MST_CODE";
                ddlUOM.DataBind();

                ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByValue("PCS"));
            }

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
            int iRecordFound = 0;

            oTXGateMST.GATE_NUMB = Convert.ToInt64(txtGateRunningNo.Text.Trim());
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.GATE_DATE = DateTime.Parse(txtGateDate.Text);

            oTXGateMST.ITEM_TYPE = ddlTranType.SelectedItem.ToString();
            oTXGateMST.DOC_NO = Common.CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTXGateMST.DOC_DATE = DateTime.Parse(txtDocDate.Text);
            double docamount = 0;
            double.TryParse(Common.CommonFuction.funFixQuotes(txtDocAmount.Text.Trim()), out docamount);
            oTXGateMST.DOC_AMOUNT = docamount;
            oTXGateMST.LORRY_NO = Common.CommonFuction.funFixQuotes(txtLorryno.Text.Trim());
            oTXGateMST.DRIVER = Common.CommonFuction.funFixQuotes(txtDriverName.Text.Trim());
            oTXGateMST.UOM = ddlUOM.SelectedItem.ToString();
            double Qty = 0;
            double.TryParse(Common.CommonFuction.funFixQuotes(txtQty.Text.Trim()), out Qty);
            oTXGateMST.QTY = Qty;

            oTXGateMST.SECURITY_ENCHARGE = Common.CommonFuction.funFixQuotes(txtSecurityIncharge.Text.Trim());
            oTXGateMST.CHECK_BY = Common.CommonFuction.funFixQuotes(txtCheckBy.Text.Trim());
            oTXGateMST.ENTER_BY = oUserLoginDetail.UserCode;
            oTXGateMST.MATERIAL_DTL = Common.CommonFuction.funFixQuotes(txtMaterialDetail.Text.Trim());
            oTXGateMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTXGateMST.TUSER = oUserLoginDetail.UserCode;
            oTXGateMST.STATUS = true;
            oTXGateMST.ISSUE_TYPE = "";

            oTXGateMST.PRTY_CODE = lblPartyCode.Text.Trim();
            oTXGateMST.PRTY_NAME = Common.CommonFuction.funFixQuotes(txtPartyName.Text.Trim());
            oTXGateMST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oTXGateMST.TRSP_NAME = txtTransporterAddress.Text.Trim();




            bool result = SaitexBL.Interface.Method.TX_Gate_MST.InsertGateIN(oTXGateMST, out iRecordFound);
            if (result)
            {
                string Resultmsg = "Gate Entry Saved Successfully" + "\\r\\n";
                Resultmsg += "Gate Entry No is:" + oTXGateMST.GATE_NUMB;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                InitialisePage();
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Gate Entry Already Exists ');", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Gate Entry Is Not Saved');", true);
            }

        }
        catch (Exception ex)
        {


            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;

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
            lblMode.Text = ex.ToString();
        }
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

    protected int GetPartyCount(string text)
    {
        try
        {

            string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
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
            txtPartyName.Text = txtPartyCode.SelectedValue.Trim();
            lblPartyCode.Text = txtPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Changing.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
        }
    }
    protected void txtTransporterCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLOVForTransporter(e.Text, e.ItemsOffset);

            txtTransporterCode.Items.Clear();

            txtTransporterCode.DataSource = data;
            txtTransporterCode.DataTextField = "PRTY_CODE";
            txtTransporterCode.DataValueField = "Address";
            txtTransporterCode.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetLOVForTransporter(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE,PRTY_GRP_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') and ROWNUM <= " + startOffset + ")";
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

    protected int GetTransporterCount(string text)
    {

        try
        {
            string CommandText = "SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1 FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)=upper('Transporter') ";
            string WhereClause = " ";
            string SortExpression = " ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void txtTransporterCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtTransporterAddress.Text = txtTransporterCode.SelectedValue;
            lblTransporterCode.Text = txtTransporterCode.SelectedText;
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in transporter selection.\r\nsee error log for detail"));
            lblMode.Text = ex.ToString();
        }
    }
    protected void cmbGatedetails_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {

        try
        {
            string CommandText = "SELECT   *  FROM   (SELECT   GATE_NUMB,  GATE_DATE,  PRTY_CODE,  PRTY_NAME,  ITEM_TYPE  FROM   tx_Gate_mst  WHERE DEL_STATUS = 0 AND ITEM_TYPE = '" + TRNTYPE + "' AND NVL (issue_numb, 0) = 0  AND COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE= '" + oUserLoginDetail.CH_BRANCHCODE + "'  AND GATE_TYPE ='IN' )a ";
            string WhereClause = "  where GATE_NUMB like :SearchQuery or GATE_DATE like :SearchQuery or PRTY_CODE like :SearchQuery or PRTY_NAME like :SearchQuery or ITEM_TYPE like :SearchQuery";
            string SortExpression = " order by GATE_NUMB desc";
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Details Loading.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
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
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.ITEM_TYPE = TRNTYPE;
            oTXGateMST.GATE_NUMB = Convert.ToInt64(cmbGatedetails.SelectedValue.ToString());

            DataTable dtGatedetails = SaitexBL.Interface.Method.TX_Gate_MST.GetGateIn(oTXGateMST);

            txtGateDate.Text = dtGatedetails.Rows[0]["GATE_DATE"].ToString();
            txtPartyCode.SelectedValue = dtGatedetails.Rows[0]["PRTY_CODE"].ToString();
            txtPartyName.Text = dtGatedetails.Rows[0]["PRTY_NAME"].ToString();
            txtTransporterCode.SelectedValue = dtGatedetails.Rows[0]["TRSP_CODE"].ToString();
            txtTransporterAddress.Text = dtGatedetails.Rows[0]["TRSP_NAME"].ToString();
            lblPartyCode.Text = dtGatedetails.Rows[0]["PRTY_CODE"].ToString().Trim();
            lblTransporterCode.Text = dtGatedetails.Rows[0]["TRSP_CODE"].ToString().Trim();
            txtDocNo.Text = dtGatedetails.Rows[0]["DOC_NO"].ToString();
            txtDocDate.Text = dtGatedetails.Rows[0]["DOC_DATE"].ToString();
            txtDocAmount.Text = dtGatedetails.Rows[0]["DOC_AMOUNT"].ToString();
            txtLorryno.Text = dtGatedetails.Rows[0]["LORRY_NO"].ToString();
            txtDriverName.Text = dtGatedetails.Rows[0]["DRIVER"].ToString();
            ddlUOM.SelectedIndex = ddlUOM.Items.IndexOf(ddlUOM.Items.FindByText(dtGatedetails.Rows[0]["UOM"].ToString()));
            txtQty.Text = dtGatedetails.Rows[0]["QTY"].ToString();
            txtSecurityIncharge.Text = dtGatedetails.Rows[0]["SECURITY_ENCHARGE"].ToString();
            txtCheckBy.Text = dtGatedetails.Rows[0]["CHECK_BY"].ToString();
            ddlTranType.SelectedIndex = ddlTranType.Items.IndexOf(ddlTranType.Items.FindByText(dtGatedetails.Rows[0]["ITEM_TYPE"].ToString()));
            txtMaterialDetail.Text = dtGatedetails.Rows[0]["MATERIAL_DTL"].ToString();
            txtRemarks.Text = dtGatedetails.Rows[0]["REMARKS"].ToString();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Details Changing.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }
    protected void ddlTranType_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Existing.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

            if (TRNTYPE.Equals("APPAREL FABRIC IN"))
            {
                Response.Redirect("~/Module/GateEntry/Pages/Apparel_Fabric_GateIn.aspx");

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;

            oTXGateMST.GATE_NUMB = Convert.ToInt64(cmbGatedetails.SelectedValue.ToString());
            oTXGateMST.GATE_TYPE = oTXGateMST.GATE_TYPE = "IN";
            oTXGateMST.GATE_DATE = DateTime.Parse(txtGateDate.Text);

            oTXGateMST.ITEM_TYPE = TRNTYPE.Trim();
            oTXGateMST.DOC_NO = Common.CommonFuction.funFixQuotes(txtDocNo.Text.Trim());
            oTXGateMST.DOC_DATE = DateTime.Parse(txtDocDate.Text);
            double docamount = 0;
            double.TryParse(Common.CommonFuction.funFixQuotes(txtDocAmount.Text.Trim()), out docamount);
            oTXGateMST.DOC_AMOUNT = docamount;
            oTXGateMST.LORRY_NO = Common.CommonFuction.funFixQuotes(txtLorryno.Text.Trim());
            oTXGateMST.DRIVER = Common.CommonFuction.funFixQuotes(txtDriverName.Text.Trim());
            oTXGateMST.UOM = ddlUOM.SelectedItem.ToString();
            double Qty = 0;
            double.TryParse(Common.CommonFuction.funFixQuotes(txtQty.Text.Trim()), out Qty);
            oTXGateMST.QTY = Qty;

            oTXGateMST.SECURITY_ENCHARGE = Common.CommonFuction.funFixQuotes(txtSecurityIncharge.Text.Trim());
            oTXGateMST.CHECK_BY = Common.CommonFuction.funFixQuotes(txtCheckBy.Text.Trim());
            oTXGateMST.ENTER_BY = oUserLoginDetail.UserCode;
            oTXGateMST.MATERIAL_DTL = Common.CommonFuction.funFixQuotes(txtMaterialDetail.Text.Trim());
            oTXGateMST.REMARKS = Common.CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            oTXGateMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTXGateMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTXGateMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTXGateMST.TUSER = oUserLoginDetail.UserCode;
            oTXGateMST.STATUS = true;
            oTXGateMST.ISSUE_TYPE = "";
            oTXGateMST.PRTY_CODE = lblPartyCode.Text.Trim();
            oTXGateMST.PRTY_NAME = Common.CommonFuction.funFixQuotes(txtPartyName.Text.Trim());
            oTXGateMST.TRSP_CODE = lblTransporterCode.Text.Trim();
            oTXGateMST.TRSP_NAME = txtTransporterAddress.Text.Trim();

            bool result = SaitexBL.Interface.Method.TX_Gate_MST.UpdateGateIN(oTXGateMST, out iRecordFound);
            if (result)
            {
                InitialisePage();
                string Resultmsg = "Gate Entry Updated Successfully" + "\\r\\n";
                Resultmsg += "Gate Entry No is:" + oTXGateMST.GATE_NUMB;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
            }
            else if (iRecordFound > 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Gate Entry Already Exists ');", true);

            }
            else
            {

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Updation Failed');", true);
            }

        }
        catch (Exception ex)
        {


            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
            lblMode.Text = "Update";
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            cmbGatedetails.Visible = true;
            cmbGatedetails.SelectedIndex = -1;
            txtGateRunningNo.Visible = false;
            txtGateDate.Text = "";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail"));
            lblMode.Text = ex.Message;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
}
