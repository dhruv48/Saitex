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
using System.Drawing;
using Obout.ComboBox;

public partial class Module_Inventory_Pages_Item_Opening_Form : System.Web.UI.Page
{
    DataTable dtItemDetail;
    DataTable dtTRN_SUB;
    string msg = string.Empty;
    string Errormsg = string.Empty;
    string ArticleCode = string.Empty;
    string newShortcode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_ITEM_MST oTX_ITEM_MST = new SaitexDM.Common.DataModel.TX_ITEM_MST();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {          
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                RefreshControls();
                Initialize();

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));

        }
    }
    
    protected void ddlItemcode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = GetItemData(e.Text.ToUpper(), e.ItemsOffset);
        ddlitemcode.Items.Clear();
        ddlitemcode.DataSource = data;
        ddlitemcode.DataBind();
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        e.ItemsCount = GetItemCount(e.Text);
    }
    private DataTable GetItemData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "select ITEM_CODE,CAT_CODE,ITEM_TYPE,ITEM_DESC,UOM,ITEM_CODE||'@'||  CAT_CODE||'@'|| ITEM_TYPE||'@'||    ITEM_DESC||'@'||       UOM     AS Combined  from TX_ITEM_MST Where   (UPPER(ITEM_CODE) like :SearchQuery  or  UPPER(ITEM_DESC) like :SearchQuery)   AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND ITEM_CODE NOT IN ( select ITEM_CODE  from TX_ITEM_MST Where   (UPPER(ITEM_CODE) like :SearchQuery  or  UPPER(ITEM_DESC) like :SearchQuery)   AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by ITEM_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemCount(string text)
    {

        string CommandText = " select ITEM_CODE  from TX_ITEM_MST Where   (UPPER(ITEM_CODE) like :SearchQuery  or  UPPER(ITEM_DESC) like :SearchQuery)   ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }
    protected void ddlitemcode_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        
        try
        {
            txtItemCode.Text = ddlitemcode.SelectedText;
            string[] arr = ddlitemcode.SelectedValue.Split('@');
            if (arr.Length > 0)
            {
                txtItemDescription.Text = arr[3].ToString();
                hdnUOM.Value = arr[4].ToString();
            }

            ddlitemcode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;

                  DataTable dtTRN_SUB = SaitexBL.Interface.Method.TX_ITEM_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtItemCode.Text, "OPB01");
                  if (dtTRN_SUB.Rows.Count > 0)
                  {
                      ViewState["dtTRN_Sub"] = dtTRN_SUB;
                      MapTrnDataTable();

                      DataTable dtTemp = SaitexBL.Interface.Method.TX_ITEM_MST.Select_Item_LIST(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, txtItemCode.Text);
                      if (dtTemp != null && dtTemp.Rows.Count > 0)
                      {
                          MapItemDataTable(dtTemp);
                      }
                      BindItemDetailGrid();
                  }
               


           
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

        }

    }
    protected void txtPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
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
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME ) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void txtPartyCode_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {
            txtPartyName.Value = txtPartyCode.SelectedValue.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void lbtnsavedetailColor_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtItemDetail"] != null)
                dtItemDetail = (DataTable)ViewState["dtItemDetail"];
            if (dtItemDetail == null || dtItemDetail.Rows.Count == 0)

                CreateItemDetailTable();

            if (!string.IsNullOrEmpty(txtPartyName.Value) && !string.IsNullOrEmpty(ddlLocation.SelectedValue) && !string.IsNullOrEmpty(ddlStore.SelectedValue))
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchItemCodeInGrid(UniqueId);
                if (!bb)
                {
                    double rate = 0;
                    double.TryParse(txtOpenRate.Text.Trim(), out rate);
                    double opBal = 0;
                    double.TryParse(txtOpeningBal.Text.Trim(), out opBal);

                    double no_of_unit = 0;
                    double wt_of_unit = 0;                    
                    double.TryParse(txtNoOfUnit.Value, out no_of_unit);
                    double.TryParse(txtWeightOfUnit.Value, out wt_of_unit);

                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtItemDetail);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["ITEM_CODE"] = txtItemCode.Text.Trim();
                            dv[0]["BILL_NUMB"] = txtBillNo.Text.Trim();
                            dv[0]["BILL_DATE"] = txtBillDate.Text;                         
                            dv[0]["OP_BAL_STOCK"] = opBal;
                            dv[0]["OP_RATE"] = rate;
                           
                            dv[0]["LOCATION"] = ddlLocation.SelectedValue.ToString();
                            dv[0]["STORE"] = ddlStore.SelectedValue.ToString();
                            dv[0]["ROW_STATE"] = "UPDATE";
                            dv[0]["NO_OF_UNIT"] = no_of_unit;
                            dv[0]["WEIGHT_OF_UNIT"] = wt_of_unit;                           
                            dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                            dv[0]["PRTY_NAME"] = txtPartyName.Value;
                            dv[0]["UOM"] = hdnUOM.Value;
                            dtItemDetail.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtItemDetail.NewRow();
                        dr["UniqueId"] = dtItemDetail.Rows.Count + 1;
                        dr["ITEM_CODE"] = txtItemCode.Text.Trim();
                        dr["BILL_NUMB"] = txtBillNo.Text.Trim();
                        dr["BILL_DATE"] = txtBillDate.Text;         
                        dr["OP_BAL_STOCK"] = opBal;
                        dr["OP_RATE"] = rate;
                       
                        dr["LOCATION"] = ddlLocation.SelectedValue.ToString();
                        dr["STORE"] = ddlStore.SelectedValue.ToString();
                        dr["ROW_STATE"] = "INSERT";            
                        dr["NO_OF_UNIT"] = no_of_unit;
                        dr["WEIGHT_OF_UNIT"] = wt_of_unit;
                        dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                        dr["PRTY_NAME"] = txtPartyName.Value;
                        dr["UOM"] = hdnUOM.Value;
                        dtItemDetail.Rows.Add(dr);
                    }
                    RefreshDetailRowItem();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Party Code/Bill No Should Be Diffrent');", true);
                }
            }
            

            ViewState["dtItemDetail"] = dtItemDetail;
            BindItemDetailGrid();
        }


        catch (Exception ex)
        {
            throw ex;
        }


    }



    public void RefreshDetailRowItem()
    {
        txtOpenRate.Text = string.Empty;
        txtOpeningBal.Text = string.Empty;
        txtMaxStock.Text = "";
        txtMinStock.Text = "";
        txtBillNo.Text = string.Empty;
        txtBillDate.Text = string.Empty;
        ViewState["UniqueId"] = null;  
        txtNoOfUnit.Value = string.Empty;
        txtWeightOfUnit.Value = string.Empty;
        txtPartyCode.SelectedIndex = -1;
        txtPartyName.Value = string.Empty;
       
    }
    private void BindItemDetailGrid()
    {
        try
        {
            if (ViewState["dtItemDetail"] != null)
            {
                dtItemDetail = (DataTable)ViewState["dtItemDetail"];
                DataView dv = new DataView(dtItemDetail);
                dv.RowFilter = "ROW_STATE <> 'DELETE'";
                grdItemDetail.DataSource = dv;
                grdItemDetail.DataBind();
            }
            else
            {
                grdItemDetail.DataSource = null;
                grdItemDetail.DataBind();
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private bool SearchItemCodeInGrid(int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in grdItemDetail.Rows)
            {
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                Label lblBillNo = (Label)grdRow.FindControl("txtBillNo");               
                Label lblStore = (Label)grdRow.FindControl("txtstore");
                Label lblParty = (Label)grdRow.FindControl("txtParty");              
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (UniqueId != iUniqueId && txtBillNo.Text == lblBillNo.Text.Trim()  && ddlStore.SelectedValue == lblStore.Text.Trim() && lblParty.ToolTip == txtPartyCode.SelectedText)
                {
                    Result = true;
                }
            }



            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        RefreshDetailRowItem();
    }
    protected void grdItemDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "ROWEdit")
            {

                FillDetailByGridColor(UniqueId);
            }
            else if (e.CommandName == "ROWDelete")
            {
                DeleteItemDetailRow(UniqueId);
                BindItemDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void DeleteItemDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtItemDetail"] != null)
                dtItemDetail = (DataTable)ViewState["dtItemDetail"];
            if (grdItemDetail.Rows.Count == 1)
            {
                //dtItemDetail.Rows.Clear();
                dtItemDetail.Rows[0].SetField("ROW_STATE", "DELETE");
            }
            else
            {
                foreach (DataRow dr in dtItemDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        //dtItemDetail.Rows.Remove(dr);
                        dr.SetField("ROW_STATE", "DELETE");
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtItemDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
            ViewState["dtItemDetail"] = dtItemDetail;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void FillDetailByGridColor(int UniqueId)
    {
        try
        {
            if (ViewState["dtItemDetail"] != null)
                dtItemDetail = (DataTable)ViewState["dtItemDetail"];
            DataView dv = new DataView(dtItemDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
               
                txtBillNo.Text = dv[0]["BILL_NUMB"].ToString();
                txtBillDate.Text = dv[0]["BILL_DATE"].ToString();               
                txtOpenRate.Text = dv[0]["OP_RATE"].ToString();
                txtOpeningBal.Text = dv[0]["OP_BAL_STOCK"].ToString();
                txtMinStock.Text = dv[0]["MIN_STOCK"].ToString();
                txtMaxStock.Text = dv[0]["MAX_STOCK"].ToString();
               
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dv[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dv[0]["STORE"].ToString()));
                ViewState["UniqueId"] = UniqueId;             
            
                txtNoOfUnit.Value = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightOfUnit.Value = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtPartyName.Value = dv[0]["PRTY_NAME"].ToString();
                string CommandText3 = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker')) ";
                string WhereClause3 = "  and  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                string SortExpression3 = " order by PRTY_CODE asc";
                string SearchQuery3 = "%";
                DataTable data3 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText3, WhereClause3, SortExpression3, "", SearchQuery3, "");
                txtPartyCode.DataSource = data3;
                txtPartyCode.DataTextField = "PRTY_CODE";
                txtPartyCode.DataValueField = "Address";
                txtPartyCode.DataBind();
                foreach (ComboBoxItem item in txtPartyCode.Items)
                {
                    if (item.Text == dv[0]["PRTY_CODE"].ToString())
                    {
                        txtPartyCode.SelectedIndex = txtPartyCode.Items.IndexOf(item);
                        break;
                    }
                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void grdItemDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                DateTime TDATE = DateTime.Now;
                if (ViewState["TDATE"] != null)
                    TDATE = (DateTime)ViewState["TDATE"];
                Button delButton = ((Button)e.Row.FindControl("lnkDelete"));
                Button lnkEdit = ((Button)e.Row.FindControl("lnkEdit"));             
                


                if (!oUserLoginDetail.UserType.Equals("SA"))
                {
                    if (TDATE.Date.Equals(DateTime.Now.Date))
                    {
                        delButton.Visible = true;
                        lnkEdit.Visible = true;
                        imgbtnUpdate.Enabled = true;
                    }
                    else
                    {
                        delButton.Visible = false;
                        lnkEdit.Visible = false;
                        imgbtnUpdate.Enabled = false;
                    }

                }
                else
                {

                    delButton.Visible = true;
                    lnkEdit.Visible = true;
                    imgbtnUpdate.Enabled = true;
                }

                LinkButton lnkunige = (LinkButton)e.Row.FindControl("lnkunige");
                int UNIQUE_ID = int.Parse(lnkunige.CommandArgument);
                if (dtItemDetail != null && dtItemDetail.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtItemDetail);

                    dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string ITEM_CODE = dv[0]["ITEM_CODE"].ToString();
                        string BILL_NUMB = dv[0]["BILL_NUMB"].ToString();
                        string BILL_DATE = dv[0]["BILL_DATE"].ToString();
                        string STORE = dv[0]["STORE"].ToString();
                        string LOCATION = dv[0]["LOCATION"].ToString();
                        string PRTY_CODE = dv[0]["PRTY_CODE"].ToString();
                      
                        if (Session["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];
                            DataView dvTRN_Sub = new DataView(dtTRN_Sub);
                            dvTRN_Sub.RowFilter = "ITEM_CODE='" + ITEM_CODE + "' AND BILL_NUMB='" + BILL_NUMB + "'  AND STORE='" + STORE + "'  AND LOCATION='" + LOCATION + "' AND PRTY_CODE='" + PRTY_CODE + "' ";
                            if (dvTRN_Sub.Count > 0)
                            {
                                GridView grdItemSubTrn = e.Row.FindControl("grdItemSubTrn") as GridView;
                                grdItemSubTrn.DataSource = dvTRN_Sub;
                                grdItemSubTrn.DataBind();
                            }

                        }

                    }

                }



            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Databound..\r\nSee error log for detail."));

        }
    }
    protected void btnSubTransaction_Click(object sender, EventArgs e)
    {
        try
        {
            bool Result = false;
            if (!string.IsNullOrEmpty(txtPartyCode.SelectedText) && !string.IsNullOrEmpty(txtItemCode.Text) && !string.IsNullOrEmpty(txtItemDescription.Text) && !string.IsNullOrEmpty(txtBillNo.Text) && !string.IsNullOrEmpty(txtBillDate.Text))
            {
                txtOpeningBal.ReadOnly = false;
                string URL = "ITEM_OP_BAL_LOT_DETAILS.aspx";
                URL = URL + "?ITEM_CODE=" + txtItemCode.Text;
                URL = URL + "&BILL_NUMB=" + HttpUtility.UrlEncode(txtBillNo.Text);
                URL = URL + "&BILL_DATE=" + HttpUtility.UrlEncode(txtBillDate.Text);               
                URL = URL + "&OP_BAL=" + txtOpeningBal.Text;
                URL = URL + "&UOM=" + hdnUOM.Value;
                URL = URL + "&STORE=" + ddlStore.SelectedValue;
                URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;               
                URL = URL + "&txtQTY=" + txtOpeningBal.ClientID;               
                URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
                URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
                URL = URL + "&PI_NO=NA";
                URL = URL + "&PRTY_CODE=" + txtPartyCode.SelectedText;
             
                              
            

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Party/Shade/Location/LotNO/Grade.");
            }
        }
        catch
        {
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (CheckValidation("Update"))
            {
                return;
            }
            if (Page.IsValid)
            {
                int iRecordFound = 0;
                oTX_ITEM_MST.ITEM_CODE = txtItemCode.Text.Trim();                
                oTX_ITEM_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_ITEM_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_ITEM_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_ITEM_MST.ARTICLE_CODE = txtItemCode.Text.Trim();
                oTX_ITEM_MST.LOCATION = oUserLoginDetail.VC_BRANCHNAME;
                oTX_ITEM_MST.STORE = oUserLoginDetail.VC_DEPARTMENTNAME;
                oTX_ITEM_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oTX_ITEM_MST.TUSER = oUserLoginDetail.UserCode;              
                oTX_ITEM_MST.UOM = hdnUOM.Value ;
                if (ViewState["dtItemDetail"] != null)
                    dtItemDetail = (DataTable)ViewState["dtItemDetail"];
                if (Session["dtTRN_SUB"] != null)
                    dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                if (Errormsg == string.Empty)
                {                   
                    bool resultTrn = false;
                    string[] columnArr = new String[] { "PRTY_CODE", "ITEM_CODE", "PRTY_NAME", "LOCATION", "STORE","BILL_NUMB","BILL_DATE" };
                    DataView mdvDetailTBL = new DataView(dtItemDetail);
                    DataTable mdtDetailTBL = mdvDetailTBL.ToTable(true, columnArr);

                    for (int i = 0; i < mdtDetailTBL.Rows.Count; i++)
                    {
                        oTX_ITEM_MST.PRTY_CODE = mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString();
                        oTX_ITEM_MST.PRTY_NAME = mdtDetailTBL.Rows[i]["PRTY_NAME"].ToString();
                        oTX_ITEM_MST.LOCATION = mdtDetailTBL.Rows[i]["LOCATION"].ToString();
                        oTX_ITEM_MST.STORE = mdtDetailTBL.Rows[i]["STORE"].ToString();
                        oTX_ITEM_MST.BILL_NUMB = mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString();
                        oTX_ITEM_MST.BILL_DATE = Convert.ToDateTime(mdtDetailTBL.Rows[i]["BILL_DATE"].ToString());
                        SaitexDM.Common.DataModel.TX_ITEM_IR_MST OTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
                        OTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                        OTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        OTX_ITEM_IR_MST.TRN_TYPE = "OPB01";
                        OTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        OTX_ITEM_IR_MST.PRTY_CODE = mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString();
                        OTX_ITEM_IR_MST.BILL_NUMB = mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString();
                        OTX_ITEM_IR_MST.BILL_DATE = Convert.ToDateTime(mdtDetailTBL.Rows[i]["BILL_DATE"].ToString());

                        int TRN = 0;
                        int.TryParse(SaitexDL.Interface.Method.TX_ITEM_IR_MST.GetMRNNumber(OTX_ITEM_IR_MST, mdtDetailTBL.Rows[i]["ITEM_CODE"].ToString()), out TRN);

                        if (TRN > 0)
                        {
                            oTX_ITEM_MST.TRN_NUMB = TRN;
                        }
                        else
                        {
                            oTX_ITEM_MST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetNewTRNNumber(OTX_ITEM_IR_MST).ToString());
                        }

                        DataView dvItemDetail = new DataView(dtItemDetail);
                        dvItemDetail.RowFilter = "ITEM_CODE='" + mdtDetailTBL.Rows[i]["ITEM_CODE"].ToString() + "' AND PRTY_CODE='" + mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "' AND STORE='" + mdtDetailTBL.Rows[i]["STORE"].ToString() + "'  AND LOCATION='" + mdtDetailTBL.Rows[i]["LOCATION"].ToString() + "' AND BILL_NUMB='" + mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString() + "'";
                        DataView dvTRN_SUB = new DataView(dtTRN_SUB);
                        dvTRN_SUB.RowFilter = "ITEM_CODE='" + mdtDetailTBL.Rows[i]["ITEM_CODE"].ToString() + "' AND PRTY_CODE='" + mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "' AND STORE='" + mdtDetailTBL.Rows[i]["STORE"].ToString() + "'  AND LOCATION='" + mdtDetailTBL.Rows[i]["LOCATION"].ToString() + "' AND BILL_NUMB='" + mdtDetailTBL.Rows[i]["BILL_NUMB"].ToString() + "'";
                        resultTrn = SaitexDL.Interface.Method.TX_ITEM_MST.InsertItemReceive(oTX_ITEM_MST, dvTRN_SUB.ToTable(), dvItemDetail.ToTable());
                    }

                    if (resultTrn)
                    {
                        string Resultmsg = "Item Master Opening Updated Successfully" + "\\r\\n" + "Item Code is:" + oTX_ITEM_MST.ITEM_CODE;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                        RefreshControls();
                        Initialize();
                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Item Already Exists');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Item Master Opening Updated Successfully!!');", true);
                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Item Master Updation Failed!!');", true);
                    Common.CommonFuction.ShowMessage(Errormsg);

                }


            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating Item Details.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string Query_String = string.Empty;
        try
        {
            //string URL = "../../../Yarn/SalesWork/Reports/YarnOpeningBalanceReport.aspx?Query_String =" + Query_String;
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


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
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Save";
            Response.Redirect("~/Module/Inventory/Pages/Item_Opening_Form.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clearing Form Data.\r\nSee error log for detail."));

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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {

            ddlitemcode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            txtItemCode.Text = "";
            txtItemCode.Visible = false;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));

        }
    }

    private void RefreshControls()
    {
        try
        {
            txtItemDescription.Text = string.Empty;
            ddlitemcode.SelectedIndex = -1;
            ViewState["dtItemDetail"] = null;
            Session["dtTRN_SUB"] = null;
            grdItemDetail.DataSource = null;
            grdItemDetail.DataBind();
        }
        catch
        {
            throw;
        }

    }
    private void Initialize()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);
            ddlitemcode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            txtItemCode.Text = "";
            txtItemCode.Visible = false;


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
            ddl.Items.Clear();
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
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
                ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByValue("GENERAL"));
                
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
           // ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    private void MapTrnDataTable()
    {
        try
        {
            DataTable dtTRN_SUB = null;
            if (ViewState["dtTRN_Sub"] != null)
                dtTRN_SUB = (DataTable)ViewState["dtTRN_Sub"];
            if (!dtTRN_SUB.Columns.Contains("UNIQUE_ID"))
                dtTRN_SUB.Columns.Add("UNIQUE_ID", typeof(int));
            for (int iLoop = 0; iLoop < dtTRN_SUB.Rows.Count; iLoop++)
            {
                dtTRN_SUB.Rows[iLoop]["UNIQUE_ID"] = iLoop + 1;
            }
            Session["dtTRN_SUB"] = dtTRN_SUB;
        }
        catch
        {
            throw;
        }
    }
    private void MapItemDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtItemDetail"] != null)
                dtItemDetail = (DataTable)ViewState["dtItemDetail"];
            if (dtItemDetail == null || dtItemDetail.Rows.Count == 0)
                CreateItemDetailTable();
            dtItemDetail.Rows.Clear();
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtItemDetail.NewRow();
                    dr["UniqueId"] = dtItemDetail.Rows.Count + 1;
                    dr["ITEM_CODE"] = drTemp["ITEM_CODE"];
                    dr["BILL_NUMB"] = drTemp["BILL_NUMB"];
                    dr["BILL_DATE"] = drTemp["BILL_DATE"];                  
                    dr["STORE"] = drTemp["STORE"];
                    dr["LOCATION"] = drTemp["LOCATION"];
                    dr["OP_BAL_STOCK"] = drTemp["OP_BAL_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];                    
                    dr["OLD_STORE"] = drTemp["OLD_STORE"];
                    dr["OLD_LOCATION"] = drTemp["OLD_LOCATION"]; 
                    dr["NO_OF_UNIT"] = drTemp["NO_OF_UNIT"];
                    dr["WEIGHT_OF_UNIT"] = drTemp["WEIGHT_OF_UNIT"];
                    dr["TRN_NUMB"] = drTemp["TRN_NUMB"];
                    dr["PRTY_CODE"] = drTemp["PRTY_CODE"];
                    dr["PRTY_NAME"] = drTemp["PRTY_NAME"];
                    dr["UOM"] = drTemp["UOM"];
                    dr["ROW_STATE"] = "NO STATE";
                    dtItemDetail.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtItemDetail"] = dtItemDetail;
            }
        }
        catch
        {
            throw;
        }
    }
    private void CreateItemDetailTable()
    {
        dtItemDetail = new DataTable();
        dtItemDetail.Columns.Add("UniqueId", typeof(int));
        dtItemDetail.Columns.Add("ITEM_CODE", typeof(string));
        dtItemDetail.Columns.Add("BILL_NUMB", typeof(string));
        dtItemDetail.Columns.Add("BILL_DATE", typeof(string));        
        dtItemDetail.Columns.Add("OP_BAL_STOCK", typeof(double));
        dtItemDetail.Columns.Add("OP_RATE", typeof(string));
        dtItemDetail.Columns.Add("MIN_STOCK", typeof(double));
        dtItemDetail.Columns.Add("MAX_STOCK", typeof(double));
        dtItemDetail.Columns.Add("LOCATION", typeof(string));
        dtItemDetail.Columns.Add("STORE", typeof(string));
        dtItemDetail.Columns.Add("OLD_LOCATION", typeof(string));
        dtItemDetail.Columns.Add("OLD_STORE", typeof(string));
        dtItemDetail.Columns.Add("ROW_STATE", typeof(string));  
        dtItemDetail.Columns.Add("NO_OF_UNIT", typeof(double));
        dtItemDetail.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dtItemDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtItemDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtItemDetail.Columns.Add("PRTY_CODE", typeof(string));
        dtItemDetail.Columns.Add("PRTY_NAME", typeof(string));
        dtItemDetail.Columns.Add("UOM", typeof(string));
    }

    protected bool CheckValidation(string task)
    {
        bool result = false;

        if (string.IsNullOrEmpty(txtItemDescription.Text))
        {
            CommonFuction.ShowMessage("Please enter Item description.");
            result = true;
        }
        return result;

    }
}
