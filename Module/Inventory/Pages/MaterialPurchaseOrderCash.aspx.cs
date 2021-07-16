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

public partial class Inventory_MaterialPurchaseOrderCash : System.Web.UI.Page
{


//    private static DataTable dtMaterialPOCredit = null;
//    private static bool UpdateMode = false;


   
    protected void Page_Load(object sender, EventArgs e)
    {
        //lblErrorMessage.Text = "";
        //lblMessage.Text = "";
        //if (!IsPostBack)
        //{
        //    UserCode = Session["urLoginId"].ToString();

        //    InitialisePage();
        //}

        //if (Convert.ToInt16(Session["saveStatus"]) == 1)
        //{
        //    if (Request.QueryString["cId"].ToString().Trim() == "S")
        //    {
        //        lblMessage.Text = "Record Saved successfully";
        //    }
        //    if (Request.QueryString["cId"].ToString().Trim() == "U")
        //    {
        //        lblMessage.Text = "Record Updated successfully";
        //    }

        //    if (Request.QueryString["cId"].ToString().Trim() == "D")
        //    {
        //        lblMessage.Text = "Record Deleted successfully";
        //    }
        //    Session["saveStatus"] = 0;
        //}
        //txtFinalTotal.Text = FinalTotal.ToString();
    }
   /*
    private void InitialisePage()
    {
        txtOrderNumber.Enabled = true;
        txtOrderDate.Text = System.DateTime.Now.Date.ToShortDateString();
        UpdateMode = false;
        getPOMaxId();
        txtFinalTotal.Text = "0";
        txtPartyAddress.Text = "";
        txtPartycode.Text = "";
        txtRemarks.Text = "";
        txtTransporterCode.Text = "";
        txtTransporterName.Text = "";
        
        if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
            Create5BlankMaterialPOCredit();
        dtMaterialPOCredit.Rows.Clear();
        Create5BlankMaterialPOCredit();
        BindMaterialPOCredittoGrid();
    }
    private void getPOMaxId()
    {

        try
        {
            /////////////////////////////////////////////////  Getting Max id of Material PO Order Credit///////////////////////////////////////

            string strMaxId = "";
            strMaxId = "select nvl(max(PO_NUMB),0) + 1 PO_NUMB from TX_ITEM_PU_MST";
            obj = new csSaitex();
            txtOrderNumber.Text = obj.executeScalar(strMaxId, CommandType.Text);
            obj = null;

        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);

        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
    private void CreateMaterialPODetailTable()
    {
        dtMaterialPOCredit = new DataTable();
        dtMaterialPOCredit.Columns.Add("UniqueId", typeof(int));
        dtMaterialPOCredit.Columns.Add("MaterialPODetailsNumber", typeof(int));
        dtMaterialPOCredit.Columns.Add("PO_NUMB", typeof(string));
        dtMaterialPOCredit.Columns.Add("ITEM_CODE", typeof(string));
        dtMaterialPOCredit.Columns.Add("ITEM_DESC", typeof(string));
        dtMaterialPOCredit.Columns.Add("ORD_QTY", typeof(int));
        dtMaterialPOCredit.Columns.Add("VC_UNITNAME", typeof(string));
        dtMaterialPOCredit.Columns.Add("BASIC_RATE", typeof(float));
        dtMaterialPOCredit.Columns.Add("FINAL_RATE", typeof(float));
        dtMaterialPOCredit.Columns.Add("Amount", typeof(float));
        dtMaterialPOCredit.Columns.Add("CURRENCY", typeof(string));
        dtMaterialPOCredit.Columns.Add("DEL_DATE", typeof(DateTime));
        dtMaterialPOCredit.Columns.Add("Comments", typeof(string));
    }
    private void Create5BlankMaterialPOCredit()
    {
        if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
            CreateMaterialPODetailTable();

        int RowCount = 4;

        if (dtMaterialPOCredit.Rows.Count > 1)
            RowCount = 1;

        for (int iLoop = 0; iLoop < RowCount; iLoop++)
        {
            DataRow dr = dtMaterialPOCredit.NewRow();
            dr["UniqueId"] = dtMaterialPOCredit.Rows.Count + 1;
            dr["MaterialPODetailsNumber"] = 0;
            dr["ORD_QTY"] = 0;
            dr["BASIC_RATE"] = 0f;
            dr["Amount"] = 0f;
            dr["FINAL_RATE"] = 0f;
            dtMaterialPOCredit.Rows.Add(dr);
        }
    }
    private void BindMaterialPOCredittoGrid()
    {
        try
        {
            if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
                Create5BlankMaterialPOCredit();

            gvMaterialPOTRN.DataSource = dtMaterialPOCredit;
            gvMaterialPOTRN.DataBind();

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void lnkAddMore_Click(object sender, EventArgs e)
    {
        try
        {
            FillDataTableByGrid();
            Create5BlankMaterialPOCredit();
            BindMaterialPOCredittoGrid();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void FillDataTableByGrid()
    {
        try
        {
            if (gvMaterialPOTRN.Rows.Count > 0)
            {
                dtMaterialPOCredit.Rows.Clear();
                FinalTotal = 0;
                foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
                {
                    TextBox txtItemCode = (TextBox)grdRow.FindControl("txtItemCode");
                    TextBox txtIndentDetailNumber = (TextBox)grdRow.FindControl("txtIndentDetailNumber");
                    TextBox txtItemDescription = (TextBox)grdRow.FindControl("txtItemDescription");
                    TextBox txtOrderQty = (TextBox)grdRow.FindControl("txtOrderQty");
                    TextBox txtUnit = (TextBox)grdRow.FindControl("txtUnit");
                    TextBox txtBaseRate = (TextBox)grdRow.FindControl("txtBaseRate");
                    TextBox txtFinalRate = (TextBox)grdRow.FindControl("txtFinalRate");
                    TextBox txtAmount = (TextBox)grdRow.FindControl("txtAmount");
                    TextBox txtCurrency = (TextBox)grdRow.FindControl("txtCurrency");
                    TextBox txtTrnDeliveryDate = (TextBox)grdRow.FindControl("txtTrnDeliveryDate");
                    TextBox txttrnComments = (TextBox)grdRow.FindControl("txttrnComments");

                    if (txtItemCode.Text.Trim() != "")
                    {
                        DataRow dr = dtMaterialPOCredit.NewRow();
                        dr["UniqueId"] = dtMaterialPOCredit.Rows.Count + 1;
                        dr["MaterialPODetailsNumber"] = int.Parse(txtIndentDetailNumber.Text.Trim());
                        dr["PO_NUMB"] = txtOrderNumber.Text;
                        dr["ITEM_CODE"] = txtItemCode.Text.Trim();
                        dr["ITEM_DESC"] = txtItemDescription.Text.Trim();
                        dr["ORD_QTY"] = int.Parse(txtOrderQty.Text.Trim());
                        dr["VC_UNITNAME"] = txtUnit.Text.Trim();
                        dr["BASIC_RATE"] = float.Parse(txtBaseRate.Text.Trim());
                        dr["FINAL_RATE"] = float.Parse(txtFinalRate.Text.Trim());
                        dr["Amount"] = float.Parse(txtAmount.Text.Trim());
                        dr["CURRENCY"] = txtCurrency.Text.Trim();
                        DateTime Deldate = DateTime.Now.Date;
                        DateTime.TryParse(txtTrnDeliveryDate.Text.Trim(), out Deldate);
                        dr["DEL_DATE"] = Deldate;
                     //   dr["Comments"] = CommonFuction.funFixQuotes(txttrnComments.Text.Trim());
                        dr["Comments"] = "";
                        dtMaterialPOCredit.Rows.Add(dr);
                        FinalTotal = FinalTotal + float.Parse(txtAmount.Text.Trim());
                    }
                }
                //TextBox txtFooterAmount = (TextBox)gvMaterialPOTRN.FooterRow.FindControl("txtFooterAmount");
                //txtFooterAmount.Text = FinalTotal.ToString();
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void gvMaterialPOTRN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "FindItemCode")
            {
                string URL = "SearchItem.aspx";
                GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                TextBox txtItemCode = (TextBox)row.FindControl("txtItemCode");
                URL = URL + "?ItemCodeId=" + txtItemCode.ClientID;
                URL = URL + "&ForPONumb=" + true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=600');", true);
            }
            else if (e.CommandName == "AdjustIndent")
            {
                string URL = "POIndentAdjustment.aspx";
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                TextBox txtOrderQty = (TextBox)row.FindControl("txtOrderQty");
                TextBox txtItemCode = (TextBox)row.FindControl("txtItemCode");
                URL = URL + "?ItemCodeId=" + txtItemCode.Text.Trim();
                URL = URL + "&TextBoxId=" + txtOrderQty.ClientID;
                if (UpdateMode)
                    URL = URL + "&PONum=" + txtOrderNumber.Text.Trim();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=600');", true);
            }
            else if (e.CommandName == "AddDisTax")
            {
                string URL = "GetPODisTex.aspx";
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                TextBox txtOrderQty = (TextBox)row.FindControl("txtOrderQty");
                TextBox txtBaseRate = (TextBox)row.FindControl("txtBaseRate");
                TextBox txtFinalRate = (TextBox)row.FindControl("txtFinalRate");
                URL = URL + "?FinalAmount=" + txtBaseRate.Text.Trim();
                URL = URL + "&TextBoxId=" + txtFinalRate.ClientID;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=600');", true);
            }
            else if (e.CommandName == "POMateialCreditDelete")
            {
                FillDataTableByGrid();
                deletePOMaterialCreditRow(UniqueId);
                BindMaterialPOCredittoGrid();
            }
            CalculateFinalTotal();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void deletePOMaterialCreditRow(int UniqueId)
    {
        try
        {
            if (gvMaterialPOTRN.Rows.Count == 1)
            {
                dtMaterialPOCredit.Rows.Clear();
                Create5BlankMaterialPOCredit();
            }
            else
            {
                foreach (DataRow dr in dtMaterialPOCredit.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtMaterialPOCredit.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtMaterialPOCredit.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtItemCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            string Description = "";
            string UOM = "";
            float OpeningRate = 0;

            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            ImageButton imgbtnFindItem = (ImageButton)grdRow.FindControl("imgbtnFindItem");
            int UniqueId = int.Parse(imgbtnFindItem.CommandArgument.Trim());
            if (!SearchItemCodeInGrid(thisTextBox.Text.Trim(), UniqueId))
            {
                GetItemDetailByItemCode(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out Description, out UOM, out OpeningRate);

                TextBox txtBaseRate = (TextBox)grdRow.FindControl("txtBaseRate");
                TextBox txtItemDescription = (TextBox)grdRow.FindControl("txtItemDescription");
                TextBox txtUnit = (TextBox)grdRow.FindControl("txtUnit");

                txtBaseRate.Text = OpeningRate.ToString();
                txtItemDescription.Text = Description;
                txtUnit.Text = UOM;
                CalculateAmount(grdRow);
                CalculateFinalTotal();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('This Item already included');", true);
                thisTextBox.Text = "";
            }

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void GetItemDetailByItemCode(string ItemCode, out string Description, out string UOM, out float OpeningRate)
    {
        Description = "";
        UOM = "";
        OpeningRate = 0;

        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select a.ITEM_CODE, (nvl(b.OPBAL_STOCK,0) + nvl(b.YTD_RCPT,0) - nvl(b.YTD_ISSU,0))  currentStock,a.MIN_STOCK_LVL,nvl(b.LAST_PO_RATE,0) LAST_PO_RATE,a.ITEM_DESC,c.MST_CODE as VC_UNITNAME,c.MST_CODE as UOM from  TX_ITEM_MST a, TX_ITEM_OPBAL b,tx_master_trn c where ltrim(rtrim(a.ITEM_CODE))=ltrim(rtrim(b.ITEM_CODE)) and ltrim(rtrim(c.MST_NAME))='UOM' and ltrim(rtrim(c.MST_CODE))=ltrim(rtrim(a.UOM)) and ltrim(rtrim(a.ITEM_CODE))=:ITEM_CODE";

            cmd = new OracleCommand(strSQL, con);

            param = new OracleParameter(":ITEM_CODE", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = ItemCode;
            cmd.Parameters.Add(param);

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                Description = dr["ITEM_DESC"].ToString().Trim();
                UOM = dr["UOM"].ToString().Trim();
                OpeningRate = float.Parse(dr["LAST_PO_RATE"].ToString().Trim());
            }
        }
        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }
            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            if (param != null)
            {
                param = null;
            }
        }
    }

    protected void txtOrderQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            if (thisTextBox.Text != "")
            {
                int RequestQTY = 0;
                if (int.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {
                    GridViewRow grdRow = ((GridViewRow)(thisTextBox.NamingContainer));
                    TextBox txtAmount = (TextBox)grdRow.FindControl("txtAmount");
                    txtAmount.Text = CalculateAmount(grdRow).ToString();
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
            CalculateFinalTotal();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtBaseRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            if (thisTextBox.Text != "")
            {
                float RequestQTY = 0;
                if (float.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {
                    GridViewRow grdRow = ((GridViewRow)(thisTextBox.NamingContainer));
                    TextBox txtAmount = (TextBox)grdRow.FindControl("txtAmount");
                    txtAmount.Text = CalculateAmount(grdRow).ToString();
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
            CalculateFinalTotal();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }

    }
    protected void txtFinalRate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            if (thisTextBox.Text != "")
            {
                float RequestQTY = 0;
                if (float.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out RequestQTY))
                {
                    GridViewRow grdRow = ((GridViewRow)(thisTextBox.NamingContainer));
                    TextBox txtAmount = (TextBox)grdRow.FindControl("txtAmount");
                    txtAmount.Text = CalculateAmount(grdRow).ToString();
                }
                else
                {
                    thisTextBox.Text = "0";
                }
            }
            CalculateFinalTotal();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private float CalculateAmount(GridViewRow grdRow)
    {
        try
        {
            TextBox txtOrderQty = (TextBox)grdRow.FindControl("txtOrderQty");
            TextBox txtBaseRate = (TextBox)grdRow.FindControl("txtBaseRate");
            TextBox txtFinalRate = (TextBox)grdRow.FindControl("txtFinalRate");
            int OrderQty = 0;
            float BaseRate = 0f;
            float FinalRate = 0f;
            float Amount = 0f;
            int.TryParse(CommonFuction.funFixQuotes(txtOrderQty.Text.Trim()), out OrderQty);
            float.TryParse(CommonFuction.funFixQuotes(txtBaseRate.Text.Trim()), out BaseRate);
            txtFinalRate.Text = txtBaseRate.Text;
            float.TryParse(CommonFuction.funFixQuotes(txtFinalRate.Text.Trim()), out FinalRate);

            Amount = OrderQty * FinalRate;
            return Amount;
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
            return 0f;
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            FillDataTableByGrid();
            if (UpdateMode)
            {
                UpdateMaterialPOCreditMST();
            }
            else
            {
                saveMaterialPOOrderCreditMST();
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void saveMaterialPOOrderCreditMST()
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strDup = "";
            int iRecordFound = 0;
            strDup = "select PO_NUMB from TX_ITEM_PU_MST where ltrim(rtrim(PO_NUMB))=:PO_NUMB";

            cmd = new OracleCommand(strDup, con);

            param = new OracleParameter(":PO_NUMB", OracleType.Number);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim());
            cmd.Parameters.Add(param);

            strDup = Convert.ToString(cmd.ExecuteScalar());

            if (strDup != "")
            {
                iRecordFound = 1;
            }
            cmd.Dispose();

            if (iRecordFound == 0)
            {
                string strSQL = "";
                strSQL = "insert into TX_ITEM_PU_MST (CH_CASH,COMP_CODE,BRANCH_CODE,PO_TYPE,PO_NUMB,PO_DATE,PRTY_CODE,CONF_FLAG,COMMENTS,TRSP_CODE,TUSER,TDATE)";
                strSQL = strSQL + " values(:CH_CASH,:COMP_CODE,:BRANCH_CODE,:PO_TYPE,:PO_NUMB,:PO_DATE,:PRTY_CODE,:CONF_FLAG,:COMMENTS,:TRSP_CODE,:TUSER,:TDATE)";
                //16
                cmd = new OracleCommand(strSQL, con);

                param = new OracleParameter(":CH_CASH", OracleType.Char, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = '1';
                cmd.Parameters.Add(param);

                param = new OracleParameter(":COMP_CODE", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = Session["COMPCODE"].ToString().Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":BRANCH_CODE", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = Session["BranchCode"].ToString().Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":PO_TYPE", OracleType.VarChar, 3);
                param.Direction = ParameterDirection.Input;
                param.Value = "PUC";
                cmd.Parameters.Add(param);

                param = new OracleParameter(":PO_NUMB", OracleType.Number);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim());
                cmd.Parameters.Add(param);

                param = new OracleParameter(":PO_DATE", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                DateTime podate = DateTime.Now.Date;
                DateTime.TryParse(txtOrderDate.Text.Trim(), out podate);
                param.Value = podate;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":PRTY_CODE", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtPartycode.Text.Trim());
                cmd.Parameters.Add(param);

                param = new OracleParameter(":CONF_FLAG", OracleType.VarChar, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = radConfirm.SelectedValue.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":COMMENTS", OracleType.VarChar, 500);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                cmd.Parameters.Add(param);

                param = new OracleParameter(":TRSP_CODE", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
                cmd.Parameters.Add(param);

                param = new OracleParameter(":TUSER", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = UserCode;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":TDATE", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = System.DateTime.Now;
                cmd.Parameters.Add(param);

                int iRecordEffected = cmd.ExecuteNonQuery();
                if (iRecordEffected > 0)
                {
                    saveMaterialPOCreditTRN(int.Parse(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim())));
                    InitialisePage();
                    lblMessage.Text = "Purchase Order saved successfully";
                }
            }
            else
            {
                lblMessage.Text = "Record exists with This Order number";
            }
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }

            if (obj != null)
            {
                obj = null;
            }

        }
    }
    private void saveMaterialPOCreditTRN(int PO_NUMB)
    {
        try
        {
            DeletePOTrasaction(PO_NUMB.ToString());

            
            /////////////////////////////////////
            if (dtMaterialPOCredit != null && dtMaterialPOCredit.Rows.Count > 0)
            {
                foreach (DataRow dr in dtMaterialPOCredit.Rows)
                {
                    con = new OracleConnection();
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                    con.Open();

                    string strMaxId = "";
                    strMaxId = "select nvl(max(in_ITEM_PU_TRN),0 ) +1 in_ITEM_PU_TRN  from TX_ITEM_PU_TRN";
                    obj = new csSaitex();
                    strMaxId = obj.executeScalar(strMaxId, CommandType.Text);

                    obj = null;

                    string strSQL = "";
                    strSQL = "insert into TX_ITEM_PU_TRN (in_ITEM_PU_TRN,COMP_CODE,BRANCH_CODE,PO_TYPE,PO_NUMB,ITEM_CODE,ORD_QTY,UOM,IN_CURRENCYCODE,DEL_DATE,COMMENTS,BASIC_RATE,FINAL_RATE,TUSER,TDATE)";
                    strSQL = strSQL + "values(:in_ITEM_PU_TRN,:COMP_CODE,:BRANCH_CODE,:PO_TYPE,:PO_NUMB,:ITEM_CODE,:ORD_QTY,:UOM,:CURRENCY,:DEL_DATE,:COMMENTS,:BASIC_RATE,:FINAL_RATE,:TUSER,:TDATE)";

                    cmd = new OracleCommand(strSQL, con);

                    param = new OracleParameter(":in_ITEM_PU_TRN", OracleType.Number);
                    param.Direction = ParameterDirection.Input;
                    param.Value = Convert.ToInt32(strMaxId);
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":COMP_CODE", OracleType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = Session["COMPCODE"].ToString().Trim();
                    //param.Value = "C00001";
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":BRANCH_CODE", OracleType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = Session["BranchCode"].ToString().Trim();
                    // param.Value = "B0001";
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":PO_TYPE", OracleType.VarChar, 3);
                    param.Direction = ParameterDirection.Input;
                    param.Value = "PUC";
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":PO_NUMB", OracleType.Number);
                    param.Direction = ParameterDirection.Input;
                    param.Value = PO_NUMB;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":ITEM_CODE", OracleType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = dr["ITEM_CODE"].ToString();
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":ORD_QTY", OracleType.Double);
                    param.Direction = ParameterDirection.Input;
                    param.Value = dr["ORD_QTY"].ToString();
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":UOM", OracleType.VarChar, 3);
                    param.Direction = ParameterDirection.Input;
                    param.Value = dr["VC_UNITNAME"].ToString();
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":CURRENCY", OracleType.Number);
                    param.Direction = ParameterDirection.Input;
                    param.Value = 2;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":DEL_DATE", OracleType.DateTime);
                    param.Direction = ParameterDirection.Input;
                    param.Value = dr["DEL_DATE"].ToString();
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":COMMENTS", OracleType.VarChar, 200);
                    param.Direction = ParameterDirection.Input;
                    param.Value = "";
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":BASIC_RATE", OracleType.Double);
                    param.Direction = ParameterDirection.Input;
                    param.Value = dr["BASIC_RATE"].ToString();
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":FINAL_RATE", OracleType.Double);
                    param.Direction = ParameterDirection.Input;
                    param.Value = dr["FINAL_RATE"].ToString();
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":TUSER", OracleType.VarChar, 10);
                    param.Direction = ParameterDirection.Input;
                    param.Value = UserCode;
                    cmd.Parameters.Add(param);

                    param = new OracleParameter(":TDATE", OracleType.DateTime);
                    param.Direction = ParameterDirection.Input;
                    param.Value = System.DateTime.Now;
                    cmd.Parameters.Add(param);

                    int iRecordEffected = cmd.ExecuteNonQuery();

                    if (iRecordEffected == 1)
                    {
                        AdjustIndentItems(dr["ITEM_CODE"].ToString());
                    }
                }
            }
        }
        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }
        }
    }
    private void UpdateMaterialPOCreditMST()
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = " update TX_ITEM_PU_MST set PO_TYPE=:PO_TYPE,PO_DATE=:PO_DATE,PRTY_CODE=:PRTY_CODE,CONF_FLAG=:CONF_FLAG,COMMENTS=:COMMENTS,TRSP_CODE=:TRSP_CODE,TUSER=:TUSER,TDATE=:TDATE where ltrim(rtrim(PO_NUMB))=:PO_NUMB";

            cmd = new OracleCommand(strSQL, con);

            param = new OracleParameter(":PO_NUMB", OracleType.Number);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":PO_TYPE", OracleType.VarChar, 3);
            param.Direction = ParameterDirection.Input;
            param.Value = "PUC";
            cmd.Parameters.Add(param);

            param = new OracleParameter(":PO_DATE", OracleType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = txtOrderDate.Text.Trim();
            cmd.Parameters.Add(param);

            param = new OracleParameter(":PRTY_CODE", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtPartycode.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":CONF_FLAG", OracleType.VarChar, 1);
            param.Direction = ParameterDirection.Input;
            param.Value = radConfirm.SelectedValue.Trim();
            cmd.Parameters.Add(param);

            param = new OracleParameter(":COMMENTS", OracleType.VarChar, 500);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":TRSP_CODE", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtTransporterCode.Text.Trim());
            cmd.Parameters.Add(param);

            param = new OracleParameter(":TDATE", OracleType.DateTime);
            param.Direction = ParameterDirection.Input;
            param.Value = System.DateTime.Now;
            cmd.Parameters.Add(param);

            param = new OracleParameter(":TUSER", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = UserCode;
            cmd.Parameters.Add(param);

            int iRecordEffected = cmd.ExecuteNonQuery();

            if (iRecordEffected == 1)
            {
                saveMaterialPOCreditTRN(int.Parse(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim())));
                InitialisePage();
                lblMessage.Text = "Purchase Order updated successfully";
            }
        }
        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }

            if (obj != null)
            {
                obj = null;
            }

        }
    }
    private void DeletePOMasterData()
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "delete from tx_item_pu_mst WHERE ltrim(rtrim(PO_NUMB))= :PO_NUMB";
            cmd = new OracleCommand(strSQL, con);


            param = new OracleParameter(":PO_NUMB", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim());
            cmd.Parameters.Add(param);

            int iRecordEffected = cmd.ExecuteNonQuery();

            if (iRecordEffected == 1)
            {
                DeletePOTrasaction(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim()));
                InitialisePage();
                lblMessage.Text = "Purchase order deleted successfully";
            }
        }
        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }

            if (obj != null)
            {
                obj = null;
            }

        }
    }
    private void DeletePOTrasaction(string PO_NUMB)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strDup = "";

            strDup = "delete from TX_ITEM_PU_TRN where ltrim(rtrim(PO_NUMB))=:PO_NUMB";
            cmd = new OracleCommand(strDup, con);

            param = new OracleParameter(":PO_NUMB", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = PO_NUMB;
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();

            cmd.Dispose();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }
        }
    }
    private void AdjustIndentItems(string ItemCode)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
            DataTable dtItemIndent = (DataTable)Session["dtItemIndent"];
            DataView dvItemIndent = new DataView(dtItemIndent);
            dvItemIndent.RowFilter = "ItemCode='" + ItemCode + "'";
            if (dvItemIndent.Count > 0)
            {
                for (int iLoop = 0; iLoop < dvItemIndent.Count; iLoop++)
                {
                    string IndentNumber = dvItemIndent[iLoop]["IndentNumber"].ToString();
                    string Indent_Type = dvItemIndent[iLoop]["Indent_Type"].ToString();
                    int Adjustqty = int.Parse(dvItemIndent[iLoop]["AdjustQTY"].ToString());
                    int APPR_QTY = int.Parse(dvItemIndent[iLoop]["APPR_QTY"].ToString());
                    string UserCode = Session["urLoginId"].ToString();

                    cmd = new OracleCommand();
                    cmd.Connection = con;
                    cmd.CommandText = "sp_Item_indent_PO_Adjust";
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("CompCode", Session["COMPCODE"].ToString().Trim());
                    cmd.Parameters.AddWithValue("BranchCode", Session["BranchCode"].ToString().Trim());
                    cmd.Parameters.AddWithValue("POTYPE", "PUC");
                    cmd.Parameters.AddWithValue("poNumb", int.Parse(txtOrderNumber.Text.Trim()));
                    cmd.Parameters.AddWithValue("ItemCode", ItemCode);
                    cmd.Parameters.AddWithValue("IndType", Indent_Type);
                    cmd.Parameters.AddWithValue("IndNumb", IndentNumber);
                    cmd.Parameters.AddWithValue("AdjQty", Adjustqty);
                    cmd.Parameters.AddWithValue("TU", UserCode);
                    cmd.Parameters.AddWithValue("TD", System.DateTime.Now.Date);

                    int IRecordEffected = cmd.ExecuteNonQuery();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }
            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (UpdateMode)
            {
                if (txtOrderNumber.Text != null)
                {
                    DeletePOMasterData();
                }

            }
            else
            {
                lblErrorMessage.Text = "Please enter existing Order number";
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitialisePage();
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void txtOrderNumber_TextChanged1(object sender, EventArgs e)
    {
        try
        {
            if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
                CreateMaterialPODetailTable();
            dtMaterialPOCredit.Rows.Clear();
            FinalTotal = 0;
            Create5BlankMaterialPOCredit();
            int iRecordFound = GetdataByOrderNumber(CommonFuction.funFixQuotes(txtOrderNumber.Text.Trim()));
            BindMaterialPOCredittoGrid();
            if (iRecordFound > 0)
                UpdateMode = true;
            else
            {
                InitialisePage();
                UpdateMode = false;
            }
        }
        catch (Exception Ex)
        {
            lblErrorMessage.Text = Ex.Message;
            errorLog.ErrHandler.WriteError(Ex.Message);
        }
    }
    private int GetdataByOrderNumber(string poNumber)
    {
        int iRecordFound = 0;
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
            string strSQL = "";


            strSQL = "Select * from TX_ITEM_PU_MST Where ltrim(rtrim(PO_NUMB))=:PO_NUMB and PO_TYPE='PUC'";
            cmd = new OracleCommand(strSQL, con);

            param = new OracleParameter(":PO_NUMB", OracleType.VarChar, 10);
            param.Direction = ParameterDirection.Input;
            param.Value = poNumber;
            cmd.Parameters.Add(param);

            dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                iRecordFound = 1;
                txtOrderDate.Text = dr["PO_DATE"].ToString().Trim();
                txtPartycode.Text = dr["PRTY_CODE"].ToString().Trim();
                txtRemarks.Text = dr["COMMENTS"].ToString().Trim();
                txtTransporterCode.Text = dr["TRSP_CODE"].ToString().Trim();
              //  ddlOrderType.SelectedIndex = ddlOrderType.Items.IndexOf(ddlOrderType.Items.FindByValue(dr["PO_TYPE"].ToString().Trim()));
                radConfirm.SelectedIndex = radConfirm.Items.IndexOf(radConfirm.Items.FindByValue(dr["CONF_FLAG"].ToString().Trim()));

                /////////////////// to fill party address
                strSQL = "";
                strSQL = "select PRTY_ADD1 from tx_vendor_mst where ltrim(rtrim(PRTY_CODE))='" + txtPartycode.Text.Trim() + "' order by PRTY_CODE asc";
                obj = new csSaitex();
                strSQL = obj.executeScalar(strSQL, CommandType.Text);
                txtPartyAddress.Text = strSQL.Trim();
                obj = null;


                ///////////////////// to fill transporter address
                strSQL = "";
                strSQL = "select PRTY_ADD1 from tx_vendor_mst where ltrim(rtrim(PRTY_CODE))='" + txtTransporterCode.Text.Trim() + "' order by PRTY_CODE asc";
                obj = new csSaitex();
                strSQL = obj.executeScalar(strSQL, CommandType.Text);
                txtTransporterName.Text = strSQL;
                obj = null;
            }

            dr.Close();
            dr.Dispose();
            dr = null;

            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetTRNdataByOrderNumber('Y', poNumber);
                MapDataTable(dtTemp);
            }
            return iRecordFound;
        }
        catch (OracleException ex)
        {
            lblMessage.Text = ex.Message;
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }

        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            return iRecordFound;
        }

        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
        }

    }
    private DataTable GetTRNdataByOrderNumber(char ch_View, string PONum)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";

            strSQL = "SELECT pt.IN_ITEM_PU_TRN, pt.PO_NUMB, pt.ITEM_CODE, pt.ORD_QTY, pt.UOM, pt.IN_CURRENCYCODE, pt.CONV_RATE, pt.DEL_DATE, pt.COMMENTS, pt.BASIC_RATE, pt.FINAL_RATE, pt.PRC_TYPE, i.ITEM_DESC FROM TX_ITEM_PU_TRN pt,TX_ITEM_MST i where pt.ITEM_CODE = i.ITEM_CODE and ltrim(rtrim(pt.PO_NUMB))=:PO_NUMB";
            //  
            if (ch_View == 'Y')
            {
                strSQL = strSQL + " order by pt.PO_NUMB desc";
            }
            else
            {
                strSQL = strSQL + " order by pt.PO_NUMB desc";
            }

            cmd = new OracleCommand(strSQL, con);

            if (PONum != "")
            {
                param = new OracleParameter(":PO_NUMB", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = PONum;
                cmd.Parameters.Add(param);

            }
            else
            {
                param = new OracleParameter(":PO_NUMB", OracleType.VarChar, 10);
                param.Direction = ParameterDirection.Input;
                param.Value = CommonFuction.funFixQuotes(PONum.Trim());
                //param.Value = "2";
                cmd.Parameters.Add(param);
            }

            OracleDataAdapter da = new OracleDataAdapter(cmd);
            DataTable dtTemp = new DataTable();
            da.Fill(dtTemp);
            return dtTemp;
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }

    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtMaterialPOCredit == null || dtMaterialPOCredit.Rows.Count == 0)
                CreateMaterialPODetailTable();
            dtMaterialPOCredit.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtMaterialPOCredit.NewRow();
                    float Amount = 0f;
                    Amount = (float.Parse(drTemp["FINAL_RATE"].ToString().Trim())) * (int.Parse(drTemp["ORD_QTY"].ToString().Trim()));
                    FinalTotal = FinalTotal + Amount;
                    dr["UniqueId"] = dtMaterialPOCredit.Rows.Count + 1;
                    dr["MaterialPODetailsNumber"] = drTemp["in_ITEM_PU_TRN"];
                    dr["PO_NUMB"] = drTemp["PO_NUMB"].ToString().Trim();
                    dr["ITEM_CODE"] = drTemp["ITEM_CODE"].ToString().Trim();
                    dr["ITEM_DESC"] = drTemp["ITEM_DESC"].ToString().Trim();
                    dr["ORD_QTY"] = drTemp["ORD_QTY"].ToString().Trim();
                    dr["VC_UNITNAME"] = drTemp["UOM"].ToString().Trim();
                    dr["BASIC_RATE"] = drTemp["BASIC_RATE"].ToString().Trim();
                    dr["FINAL_RATE"] = drTemp["FINAL_RATE"].ToString().Trim();
                    dr["Amount"] = Amount;
                    dr["CURRENCY"] = drTemp["IN_CURRENCYCODE"].ToString().Trim();
                    dr["DEL_DATE"] = drTemp["DEL_DATE"].ToString().Trim();
                    dr["Comments"] = drTemp["Comments"].ToString().Trim();
                    dtMaterialPOCredit.Rows.Add(dr);
                }
                dtTemp = null;
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    
    protected void txtPartycode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string strSQL = "";
            strSQL = "select PRTY_NAME || ' ,' ||PRTY_ADD1 from tx_vendor_mst where ltrim(rtrim(PRTY_CODE))='" + txtPartycode.Text.Trim() + "' order by PRTY_CODE asc";
            obj = new csSaitex();
            strSQL = obj.executeScalar(strSQL, CommandType.Text);
            txtPartyAddress.Text = strSQL.Trim();
            obj = null;
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }

        }
    }
    protected void txtTransporterCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string strSQL = "";
            strSQL = "select PRTY_NAME || ' ,' ||PRTY_ADD1 from tx_vendor_mst where ltrim(rtrim(PRTY_CODE))='" + txtTransporterCode.Text.Trim() + "' order by PRTY_CODE asc";
            obj = new csSaitex();
            strSQL = obj.executeScalar(strSQL, CommandType.Text);
            txtTransporterName.Text = strSQL;
            obj = null;
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }

        }
    }
    private bool SearchItemCodeInGrid(string ItemCode, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
            {
                TextBox txtItemCode1 = (TextBox)grdRow.FindControl("txtItemCode");
                ImageButton imgbtnFindItem = (ImageButton)grdRow.FindControl("imgbtnFindItem");
                int iUniqueId = int.Parse(imgbtnFindItem.CommandArgument.Trim());
                if (txtItemCode1.Text.Trim() == ItemCode && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
            return Result;
        }
    }
    
    protected void lbtnFindVendor_Click(object sender, EventArgs e)
    {
        GetVendorCodeAndAddress(true);
    }
    protected void lbtnFindTransporter_Click(object sender, EventArgs e)
    {
        GetVendorCodeAndAddress(false);
    }
    private void GetVendorCodeAndAddress(bool IsParty)
    {
        string URL = "SearchVendor.aspx";
        if (IsParty)
        {
            URL = URL + "?PartyCodeId=" + txtPartycode.ClientID;
        }
        else
        {
            URL = URL + "?PartyCodeId=" + txtTransporterCode.ClientID;
        }
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + URL + "','_blank','status=1,toolbar=no,menubar=no,location=no,directories=no,scrollbars=yes,resizable=yes,width=500,height=300');", true);
    }
    private float GetAdvanceAmount(float AdvancePercent)
    {
        try
        {
            float totaladvance = (FinalTotal * AdvancePercent) / 100;
            return totaladvance;
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    private void SetFinalTotal()
    {

    }

    protected void CalculateFinalTotal()
    {
        try
        {
            FinalTotal = 0;
            if (gvMaterialPOTRN.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in gvMaterialPOTRN.Rows)
                {
                    TextBox txtItemCode = (TextBox)grdRow.FindControl("txtItemCode");
                    TextBox txtAmount = (TextBox)grdRow.FindControl("txtAmount");

                    if (txtItemCode.Text.Trim() != "")
                    {
                        FinalTotal = FinalTotal + float.Parse(txtAmount.Text.Trim());
                    }
                }
            }
            txtFinalTotal.Text = FinalTotal.ToString();
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void btnAdjustIndent_Click(object sender, EventArgs e)
    {
        try
        {
            int totalRows = gvMaterialPOTRN.Rows.Count;
            //for (int r = 0; r < totalRows; r++)
            //{
                GridViewRow thisGridViewRow = gvMaterialPOTRN.Rows[0];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtItemCode = (TextBox)thisGridViewRow.FindControl("txtItemCode");
                }
            //}

            if (txtOrderNumber.Text != "")
            {
                string URL = "POIndentAdjustment.aspx?ItemCode=" + txtOrderNumber.Text;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "newWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=350,height=500');", true);
            }
            else
            {
                lblErrorMessage.Text = "Please provise Item code for indent adjustment";
            }
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnDiscountTaxes_Click(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        try
        { 
            TextBox thisTextBox = (TextBox)sender;
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string strSQL = "";
            strSQL = "select IN_CURRENCYCODE from TX_CURRENCYMASTER where ltrim(rtrim(VC_CURRENCYNAME))=:VC_CURRENCYNAME";

            cmd = new OracleCommand(strSQL, con);

            param = new OracleParameter(":VC_CURRENCYNAME", OracleType.VarChar, 100);
            param.Direction = ParameterDirection.Input;
            param.Value = CommonFuction.funFixQuotes(thisTextBox.Text.Trim());
            cmd.Parameters.Add(param);

            strSQL = Convert.ToString(cmd.ExecuteScalar());

            thisTextBox.Text = strSQL.Trim();

            cmd.Dispose();
        }

        catch (OracleException ex)
        {
            lblMessage.Text = ex.Message;
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }

            if (param != null)
            {
                param = null;
            }
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    } 
    */
}
