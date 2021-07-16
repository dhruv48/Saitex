using System;
using System.Collections;
using System.Collections.Generic;
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
using System.IO;
using System.Globalization;
using WCFMain;


public partial class Module_Fabric_FabricSaleWork_Controls_Waste_Management : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private string PRODUCT_TYPE = string.Empty;
    private string ITEM_DESC = string.Empty;
    private string ISS_QTY = string.Empty;
    private static double FinalTotal;
    private DataTable dtOrderST;
    private string strContext = string.Empty;
    string url = string.Empty;
  

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

          
            if (!IsPostBack)
            {
                strContext = oUserLoginDetail.COMP_CODE + "@" + oUserLoginDetail.CH_BRANCHCODE;
              
                InitialiseData();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialiseData()
    {
        try
        {
            txtCrLocation.Text = oUserLoginDetail.VC_BRANCHNAME;
           
           
         
            bindOrderType();
            FinalTotal = 0;
          
           
            ActivateSaveMode();
            cmbOrderNo.Visible = false;
            txtOrderNo.Visible = true;
            ClearMainData();
            BindOrderNo();
            EnablePrimaryFields();
            BlankSTControls();

            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            dtOrderST = null;
            ViewState["dtOrderST"] = dtOrderST;

            bindSTGrid();
           
        }
        catch
        {
            throw;
        }
    }
    
    protected void btncncelpack_Click(object sender, EventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                //trOther.Visible = false;
                // mpepacking.Show();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel Button..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void ActivateSaveMode()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
        }
        catch
        {
            throw;
        }
    }

   

    

    

   

    private void bindOrderType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string sType = dr["MST_CODE"].ToString();
                    if (string.Compare(sType, "Development", true) == 0)
                    {
                        dt.Rows.Remove(dr);
                        dt.AcceptChanges();
                        break;
                    }
                }
            }
            ddlOrderType.DataSource = dt;
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataBind();
            ddlOrderType.Items.Insert(0, "Select");
        }
        catch
        {
            throw;
        }
    }

    private void ClearMainData()
    {
        try
        {
            txtDate.Text = System.DateTime.Now.ToShortDateString();
           
            ddlOrderType.SelectedIndex = -1;
         
            txtOrderNo.Text = string.Empty;
            txtAddress.Text = string.Empty;
            cmbPartyCode.SelectedIndex = -1;
            txtPartyCode.Text = "";

            txtMstRemarks.Text = string.Empty;
          
        }
        catch
        {
            throw;
        }
    }

   

   

    private void BindOrderNo()
    {
        try
        {
            string CRLocationPrefix = string.Empty;
            string CRType = string.Empty;
   
            string msg = string.Empty;
            FinalTotal = 0;
            CRLocationPrefix = oUserLoginDetail.SEQ_PREFIX.ToString();
            CRType = ddlOrderType.SelectedItem.ToString();

            string WMNo = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.GetNewWMNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, CRLocationPrefix, CRType);
            txtOrderNo.Text = WMNo;
        }
        catch
        {
            throw;
        }
    }

    void PartyCodeLOV1_OnTextChanged(string Val, string Text)
    {
        try
        {
            txtAddress.Text = Val;
            txtPartyCode.Text = Text;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Code Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchPiCodeInGrid(string pi_no, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (GridSpinningThread.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in GridSpinningThread.Rows)
                {
                    Label Txtpino = (Label)grdRow.FindControl("Txtpino");
                    Label txtArticleNo = (Label)grdRow.FindControl("txtArticleNo");
                    Label txtfabrdesc = (Label)grdRow.FindControl("txtfabrdesc");
                    Label TxtTrnNo = (Label)grdRow.FindControl("TxtTrnNo");

                    Label txtShadeCode = (Label)grdRow.FindControl("txtShadeCode");
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (Txtpino.Text.Trim() == pi_no && UNIQUE_ID != iUNIQUE_ID)
                    {
                        Result = true;
                    }
                }
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSTSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            if (dtOrderST == null)
            {
                CreateSTDataTable();
            }

            string msg = string.Empty;
            if (ValidateSTRow(out msg))
            {
                if (dtOrderST.Rows.Count < 15)
                {
                    int UniqueId = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                        UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());

                    string ArticalCode = txtItemCodeLabel.Text.ToString();
                    string pi_no = Txtpino.Text.ToString();
                    bool bb = SearchPiCodeInGrid(pi_no, UniqueId);
                    if (!bb)
                    {
                        double issueqty = 0;
                        double.TryParse(txtNoofUnit.Text.Trim(), out issueqty);
                    
                        double p_qty = 0;
                        double.TryParse(txtTotalCost.Text.Trim(), out p_qty);
                        double WASTE_QTY = 0;
                        double.TryParse(txtsalerate.Text.Trim(), out WASTE_QTY);
                        if (issueqty < 1)
                        {
                            Common.CommonFuction.ShowMessage(@"Issue Quantity can not be zero or empty.");
                            return;
                        }
                        if (WASTE_QTY < 1)
                        {
                            Common.CommonFuction.ShowMessage(@"Waste Qty can not be zero or empty.");
                            return;
                        }
                        if (string.IsNullOrEmpty(txtReqDate.Text))
                        {
                            Common.CommonFuction.ShowMessage(@"Please select Required Date.");
                            return;
                        }
                        double TRN_NUMB = 0;
                         //string[] arrString = cmbArticleNo.SelectedValue.Split('@');
                        double.TryParse(TxtTrnNo.Text.Trim(), out TRN_NUMB);
                        // TRN_NUMB = double.Parse(ViewState["TRN_NUMB"].ToString());
                    
                       
                        
                         //string PI_NO = string.Empty;
                         //PI_NO = arrString[2].ToString();
                         
                        
                         //string TRN_TYPE = string.Empty;
                         //TRN_TYPE = arrString[6].ToString();

                         //if (string.IsNullOrEmpty(arrString[6].ToString()))
                         //{
                         //    TRN_TYPE = ViewState["TRN_TYPE"].ToString();
                         //}
                         //else
                         //{
                         //    TRN_TYPE = arrString[6].ToString();
                         //}

                       
                       
                        if (issueqty > 0)
                        {
                            if (UniqueId > 0)
                            {
                                DataView dv = new DataView(dtOrderST);
                                dv.RowFilter = "UNIQUE_ID=" + UniqueId;
                                if (dv.Count > 0)
                                {
                                   
                                    dv[0]["ITEM_CODE"] = txtItemCodeLabel.Text.Trim();
                                    dv[0]["ITEM_DESC"] = TxtItemDesc.Text.ToString();
                                    dv[0]["SHADE_CODE"] = TxtShadeCode.Text.ToString();
                                    dv[0]["ISS_QTY"] = issueqty;
                                    dv[0]["WASTE_QTY"] = WASTE_QTY;
                                    dv[0]["P_QTY"] = p_qty;
                                    dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                    dv[0]["TRN_NUMB"] = TRN_NUMB;
                                    dv[0]["PI_NO"] = Txtpino.Text.Trim();
                                    dv[0]["TRN_TYPE"] = TxttrnType.Text.Trim();
                                    dv[0]["REQ_DATE"] = DateTime.Parse(txtReqDate.Text);
                                   
                                }
                                dtOrderST.AcceptChanges();
                            }
                            else
                            {
                                DataRow dr = dtOrderST.NewRow();
                                dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                                dr["ITEM_CODE"] = txtItemCodeLabel.Text.Trim();
                                dr["ITEM_DESC"] = TxtItemDesc.Text.ToString();
                                dr["SHADE_CODE"] = TxtShadeCode.Text.ToString();
                                dr["ISS_QTY"] = issueqty;
                                dr["WASTE_QTY"] = WASTE_QTY;
                                dr["P_QTY"] = p_qty;
                                dr["REMARKS"] = txtRemarks.Text.Trim();
                                dr["TRN_NUMB"] = TRN_NUMB;
                                dr["PI_NO"] = Txtpino.Text.Trim();
                                dr["TRN_TYPE"] = TxttrnType.Text.Trim();
                                dr["REQ_DATE"] = DateTime.Parse(txtReqDate.Text);
                               
                                dtOrderST.Rows.Add(dr);
                            }
                            BlankSTControls();
                        }
                       
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage(@"Please Select Diffrent Item Code ");
                    }
                }
              
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
            ViewState["dtOrderST"] = dtOrderST;
            bindSTGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Save Button.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool ValidateSTRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;
            int count = 0;
            int countAll = 0;
            int msgCount = 1;

            countAll += 1;
            if (txtItemCodeLabel.Text != string.Empty)
                count += 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Article No. ";

            }

           

            countAll += 1;
            double dd = 0;
            double.TryParse(txtNoofUnit.Text.Trim(), out dd);
            if (dd > 0)
            {
                count += 1;
            }
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Quantity/Validy Quantity. ";
                txtNoofUnit.Text = string.Empty;
            }

          

            if (countAll == count)
            {
                result = true;
            }
            return result;
        }
        catch
        {
            throw;
        }
    }

    protected void txtNoofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtNoofUnit;
            if (thisTextBox.Text != "")
            {
                double IssueQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out IssueQTY))
                {

                    double wasteQty = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtsalerate.Text.Trim()), out wasteQty))
                    {
                        double Total = ((double.Parse(IssueQTY.ToString()) - (wasteQty)));
                        txtTotalCost.Text = Total.ToString();
                     

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
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void CreateSTDataTable()
    {
        try
        {
            dtOrderST = new DataTable();
            dtOrderST.Columns.Add("UNIQUE_ID", typeof(int));
            dtOrderST.Columns.Add("ITEM_CODE", typeof(string));
            dtOrderST.Columns.Add("ITEM_DESC", typeof(string));
            dtOrderST.Columns.Add("SHADE_CODE", typeof(string));
            dtOrderST.Columns.Add("ISS_QTY", typeof(double));
            dtOrderST.Columns.Add("WASTE_QTY", typeof(double));
            dtOrderST.Columns.Add("P_QTY", typeof(double));
            dtOrderST.Columns.Add("REQ_DATE", typeof(DateTime));
            dtOrderST.Columns.Add("REMARKS", typeof(string));
            dtOrderST.Columns.Add("TRN_TYPE", typeof(string));
            dtOrderST.Columns.Add("PI_NO", typeof(string));
            dtOrderST.Columns.Add("TRN_NUMB", typeof(double));
         
        }
        catch
        {
            throw;
        }
    }

    private void BlankSTControls()
    {
        try
        {
            cmbArticleNo.SelectedIndex = -1;
            cmbArticleNo.Enabled = true;
            txtItemCodeLabel.Text = string.Empty;
          
            TxtItemDesc.Text = string.Empty;
            txtTotalCost.Text = string.Empty;
            txtsalerate.Text = string.Empty;
            TxtShadeCode.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtNoofUnit.Text = string.Empty;
            txtReqDate.Text = string.Empty;
        
            ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSTCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BlankSTControls();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Cancel.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void GridSpinningThread_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditSTItem(UniqueId);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteSTItem(UniqueId);
                bindSTGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void EditSTItem(int UniqueId)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            DataView dv = new DataView(dtOrderST);
            dv.RowFilter = "UNIQUE_ID=" + UniqueId;
            if (dv.Count > 0)
            {

                cmbArticleNo.Enabled = false;
                txtItemCodeLabel.Text = dv[0]["ITEM_CODE"].ToString();
               
                TxtItemDesc.Text = dv[0]["ITEM_DESC"].ToString();
                TxtShadeCode.Text = dv[0]["SHADE_CODE"].ToString();
                txtNoofUnit.Text = dv[0]["ISS_QTY"].ToString();
                txtsalerate.Text = dv[0]["WASTE_QTY"].ToString();
             
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                ViewState["PI_NO"] = dv[0]["PI_NO"].ToString().Trim();
                ViewState["TRN_TYPE"] = dv[0]["TRN_TYPE"].ToString().Trim();
                ViewState["TRN_NUMB"] = dv[0]["TRN_NUMB"].ToString().Trim();   
             
                txtReqDate.Text = dv[0]["REQ_DATE"].ToString();
                txtTotalCost.Text = dv[0]["P_QTY"].ToString();
                ViewState["UNIQUE_ID"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteSTItem(int UniqueId)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            if (dtOrderST.Rows.Count == 1)
            {
                dtOrderST.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtOrderST.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtOrderST.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtOrderST.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
                ViewState["dtOrderST"] = dtOrderST;
            }
            BlankSTControls();
        }
        catch
        {
            throw;
        }
    }
  
   
   
 
    private void bindSTGrid()
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            GridSpinningThread.DataSource = dtOrderST;
            GridSpinningThread.DataBind();
          
            foreach (GridViewRow row in GridSpinningThread.Rows)
            {
                Label txtTotalCost = (Label)row.FindControl("txtTotalCost");
               
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void Insertdata()
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            bool bResult = false;
            string msg = string.Empty;
            if (ValidateSTMasterRow(out msg))
            {
                if (dtOrderST != null && dtOrderST.Rows.Count > 0)
                {
                    SaitexDM.Common.DataModel.TX_WASTE_MGT_MST oTX_WASTE_MGT_MST = new SaitexDM.Common.DataModel.TX_WASTE_MGT_MST();
                    oTX_WASTE_MGT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oTX_WASTE_MGT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oTX_WASTE_MGT_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oTX_WASTE_MGT_MST.WM_LOCATION = txtCrLocation.Text.Trim();
                    oTX_WASTE_MGT_MST.WM_DATE = Convert.ToDateTime(txtDate.Text.Trim());

                    oTX_WASTE_MGT_MST.PRODUCT_TYPE = ddlOrderType.SelectedValue.Trim();
                    oTX_WASTE_MGT_MST.WM_NO = txtOrderNo.Text.Trim();


                    oTX_WASTE_MGT_MST.PRTY_CODE = txtPartyCode.Text.Trim();
                    oTX_WASTE_MGT_MST.PRTY_NAME = txtAddress.Text.ToString();
                    
                    oTX_WASTE_MGT_MST.TUSER = oUserLoginDetail.UserCode;
                    oTX_WASTE_MGT_MST.REMARKS = txtMstRemarks.Text.Trim();


                    bResult = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.Insert(oTX_WASTE_MGT_MST, dtOrderST);

                    if (bResult == true)
                    {
                        string Resultmsg = "Waste Management Saved Successfully" + "\\r\\n";
                        Resultmsg += "Waste Mgt No is:" + oTX_WASTE_MGT_MST.WM_NO;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                        InitialiseData();
                        if (dtOrderST != null)
                        {
                            dtOrderST.Rows.Clear();
                        }
                        bindSTGrid();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved :Enter Fabric Detail Atleast');", true);
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch
        {
            throw;
        }
    }

    private bool ValidateSTMasterRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;

            int count = 0;
            int msgCount = 1;
            

            if (ddlOrderType.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Product Type. ";
                msgCount += 1;
            }
            
            if (txtPartyCode.Text != "")
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Customer Name. ";
                msgCount += 1;
            }
            if (txtCrLocation.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Location. ";
                msgCount += 1;
            }
            if (txtAddress.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Address. ";
                msgCount += 1;
            }
            if (txtOrderNo.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please Create WM No. ";
                msgCount += 1;
            }
            if (count == 5)
                result = true;

            return result;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbOrderNo.SelectedIndex = -1;
            cmbOrderNo.Visible = true;
            txtOrderNo.Visible = false;
            lblMode.Text = "You are in Update Mode";
            tdUpdate.Visible = true;
            tdSave.Visible = false;

            DiablePrimaryFields();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetCRItems(e.Text.ToUpper(), e.ItemsOffset);
      
            if (data != null && data.Rows.Count > 0)
            {
                cmbOrderNo.Items.Clear();
                cmbOrderNo.DataSource = data;
                cmbOrderNo.DataBind();
            }
          
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
          
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Number Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetCRItems(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT WM_NO, PRODUCT_TYPE, WM_LOCATION,WM_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| PRODUCT_TYPE|| '@'|| WM_NO|| '@'|| WM_DATE|| '@'|| PRTY_CODE|| '@'|| PRTY_NAME|| '@'|| REMARKS|| '@'|| WM_LOCATION ) AS Combined FROM TX_WASTE_MGT_MST WHERE DEL_STATUS = '0' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE WM_NO LIKE :SearchQuery  ORDER BY WM_NO) www WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND Combined NOT IN (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT WM_NO, PRODUCT_TYPE, WM_LOCATION,WM_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| PRODUCT_TYPE|| '@'|| WM_NO|| '@'|| WM_DATE|| '@'|| PRTY_CODE|| '@'|| PRTY_NAME|| '@'|| REMARKS|| '@'|| WM_LOCATION ) AS Combined FROM TX_WASTE_MGT_MST WHERE DEL_STATUS = '0' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE WM_NO LIKE :SearchQuery  ORDER BY WM_NO) www WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY WM_NO";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetCRItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT WM_NO, PRODUCT_TYPE, WM_LOCATION,WM_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| PRODUCT_TYPE|| '@'|| WM_NO|| '@'|| WM_DATE|| '@'|| PRTY_CODE|| '@'|| PRTY_NAME|| '@'|| REMARKS|| '@'|| WM_LOCATION ) AS Combined FROM TX_WASTE_MGT_MST WHERE DEL_STATUS = '0'  AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE WM_NO LIKE :SearchQuery  ORDER BY WM_NO) www  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY WM_NO ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    private void BindControls(DataTable dt)
    {
        try
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                txtOrderNo.Text = dt.Rows[0]["WM_NO"].ToString();
                txtDate.Text = dt.Rows[0]["WM_DATE"].ToString();
                //   ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
                cmbPartyCode.SelectedValue = dt.Rows[0]["PRTY_CODE"].ToString();
                txtPartyCode.Text = dt.Rows[0]["PRTY_CODE"].ToString();
                txtMstRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                txtAddress.Text = dt.Rows[0]["PRTY_NAME"].ToString().Trim();
                ddlOrderType.SelectedValue = dt.Rows[0]["PRODUCT_TYPE"].ToString();
              
                //   ddlOrderCategory.SelectedValue = dt.Rows[0]["ORDER_CAT"].ToString();
             
               
      
            }
        }
        catch
        {
            throw;
        }
    }

    private void MapTableST(DataTable dtSpinningThread)
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            if (dtOrderST == null || dtOrderST.Rows.Count == 0)
            {
                CreateSTDataTable();
            }
            dtOrderST.Rows.Clear();
            FinalTotal = 0;
            if (dtSpinningThread != null && dtSpinningThread.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSpinningThread.Rows)
                {
                    DataRow drST = dtOrderST.NewRow();
                    drST["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                    drST["ITEM_CODE"] = dr["ITEM_CODE"];
                    drST["ITEM_DESC"] = dr["ITEM_DESC"];
                    drST["SHADE_CODE"] = dr["SHADE_CODE"];
                    drST["ISS_QTY"] = dr["ISS_QTY"];
                    drST["WASTE_QTY"] = dr["WASTE_QTY"];
                    drST["P_QTY"] = dr["P_QTY"];
                    drST["REQ_DATE"] = dr["REQ_DATE"];
                    drST["REMARKS"] = dr["REMARKS"];

                    drST["TRN_TYPE"] = dr["TRN_TYPE"];
                    drST["PI_NO"] = dr["PI_NO"];
                    drST["TRN_NUMB"] = dr["TRN_NUMB"];
                 
                    dtOrderST.Rows.Add(drST);
                }
                dtSpinningThread = null;
                ViewState["dtOrderST"] = dtOrderST;
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindSTTranasaction()
    {
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            GridSpinningThread.DataSource = dtOrderST;
            GridSpinningThread.DataBind();
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Updatedata();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void Updatedata()
    {
        bool bResult = false;
        try
        {
            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            if (dtOrderST != null && dtOrderST.Rows.Count > 0)
            {
                SaitexDM.Common.DataModel.TX_WASTE_MGT_MST oTX_WASTE_MGT_MST = new SaitexDM.Common.DataModel.TX_WASTE_MGT_MST();
                oTX_WASTE_MGT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_WASTE_MGT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_WASTE_MGT_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_WASTE_MGT_MST.WM_LOCATION = txtCrLocation.Text.Trim();
                oTX_WASTE_MGT_MST.WM_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                oTX_WASTE_MGT_MST.PRODUCT_TYPE = ddlOrderType.SelectedValue.Trim();
                oTX_WASTE_MGT_MST.WM_NO = txtOrderNo.Text.Trim();
                oTX_WASTE_MGT_MST.PRTY_CODE = txtPartyCode.Text.Trim();
                oTX_WASTE_MGT_MST.PRTY_NAME = txtAddress.Text.ToString();
                oTX_WASTE_MGT_MST.TUSER = oUserLoginDetail.UserCode;
                oTX_WASTE_MGT_MST.REMARKS = txtMstRemarks.Text.Trim();


                bResult = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.Insert(oTX_WASTE_MGT_MST, dtOrderST);

                 if (bResult)
                {
                    string Resultmsg = "Waste Management Updated Successfully" + "\\r\\n";
                    Resultmsg += "Waste Mgt No is:" + oTX_WASTE_MGT_MST.WM_NO;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                    InitialiseData();
                    //dtOrderST.Rows.Clear();
                    bindSTGrid();

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnDelete_Click1(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialiseData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    //protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    //{
    //    var name = string.Empty;
    //    //if (PRODUCT_TYPE == "NULL")
    //    //{
    //    //    name = "FABRIC_";
    //    //}
    //    //else
    //    //{
    //    //    name = "YARN SPINNING_";
    //    //}
    //    string strFilename = name + "WasteMgtDetails_" + DateTime.Now.ToString() + ".xls";
    //    var data = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.SelectWasteForExcels(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString());
    //    UploadDataTableToExcel(data, strFilename);




    //}
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        url = "../Reports/Waste_mgt.aspx?COMP_CODE=" + oUserLoginDetail.COMP_CODE + "&&BRANCH_CODE=" + oUserLoginDetail.CH_BRANCHCODE;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    }
    protected void UploadDataTableToExcel(DataTable dtEmp, string filename)
    {
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach ( DataColumn dtcol in dtEmp.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dtEmp.Rows)
        {
            tab = "";
            for (int j = 0; j < dtEmp.Columns.Count; j++)
            {
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    private void EnablePrimaryFields()
    {
        try
        {
            ddlOrderType.Enabled = true;
         
        }
        catch
        {
            throw;
        }
    }

    private void DiablePrimaryFields()
    {
        try
        {
            ddlOrderType.Enabled = false;
            
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindOrderNo();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bisiness Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbOrderNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dtMst = new DataTable();
            DataTable dtST = new DataTable();
            string cString = cmbOrderNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            string COMP_CODE = arrString[0].ToString();
            string BRANCH_CODE = arrString[1].ToString();
            string YEAR = arrString[2].ToString();
            string PRODUCT_TYPE = arrString[3].ToString();
            string WM_NO = arrString[4].ToString();
            string WM_DATE = arrString[5].ToString();
            string PRTY_CODE = arrString[6].ToString();
            string PRTY_NAME = arrString[7].ToString();
            string REMARKS = arrString[8].ToString();
            string WM_LOCATION = arrString[9].ToString();
            SaitexDM.Common.DataModel.TX_WASTE_MGT_MST oTX_WASTE_MGT_MST = new SaitexDM.Common.DataModel.TX_WASTE_MGT_MST();
            oTX_WASTE_MGT_MST.COMP_CODE = COMP_CODE;
            oTX_WASTE_MGT_MST.BRANCH_CODE = BRANCH_CODE;
            oTX_WASTE_MGT_MST.YEAR = int.Parse(YEAR);
            oTX_WASTE_MGT_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oTX_WASTE_MGT_MST.WM_NO = WM_NO;
            DateTime wmdate = DateTime.Now;
            DateTime.TryParse(WM_DATE, out wmdate);
            oTX_WASTE_MGT_MST.WM_DATE = wmdate;
            
            oTX_WASTE_MGT_MST.PRTY_CODE = PRTY_CODE;
            oTX_WASTE_MGT_MST.PRTY_NAME = PRTY_NAME;
            oTX_WASTE_MGT_MST.REMARKS = REMARKS;
            oTX_WASTE_MGT_MST.WM_LOCATION = WM_LOCATION;



             dtMst = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.SelectWasteMgtMst(oTX_WASTE_MGT_MST);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);

                dtST = SaitexBL.Interface.Method.TX_WASTE_MGT_MST.SelectWasteMgtTrn(oTX_WASTE_MGT_MST);
                if (dtST != null && dtST.Rows.Count > 0)
                {
                    MapTableST(dtST);
                    BindSTTranasaction();
                }
            }
       }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Type Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbArticleNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
   
            if (data != null && data.Rows.Count > 0)
            {
                cmbArticleNo.Items.Clear();
                cmbArticleNo.DataSource = data;
                cmbArticleNo.DataBind();
            }
   
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
  
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        DataTable data = null;
        try
        {
            if (ddlOrderType.SelectedItem.ToString() == "FABRIC")
            {
                string CommandText = " SELECT * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE, A.PI_NO ,A.TRN_NUMB,A.FABR_CODE AS ITEM_CODE,B.FABR_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB  || '@' || A.PI_NO || '@' || A.FABR_CODE|| '@' ||B.FABR_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  TX_FABRIC_IR_TRN A, TX_FABRIC_MST B WHERE A.FABR_CODE = B.FABR_CODE AND TRN_TYPE = 'IFS01' ) WHERE ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery)ORDER BY   ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
                string whereClause = string.Empty;

                if (startOffset != 0)
                {
                    whereClause += " AND FABR_CODE NOT IN(SELECT   * FROM  ( SELECT   A.TRN_TYPE,A.PI_NO , A.TRN_NUMB,A.FABR_CODE AS ITEM_CODE,B.FABR_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.PI_NO || '@' || A.FABR_CODE|| '@' ||B.FABR_DESC|| '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  TX_FABRIC_IR_TRN A, TX_FABRIC_MST B WHERE A.FABR_CODE = B.FABR_CODE AND TRN_TYPE = 'IFS01' ) WHERE  ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) ORDER BY   ITEM_CODE) asd WHERE    ROWNUM <= " + startOffset + ")";
                }
                string SortExpression = " ORDER BY  ITEM_CODE";
                string SearchQuery = text.ToUpper() + "%";
                 data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
          
            }
            else if (ddlOrderType.SelectedItem.ToString() == "YARN SPINING")
            {
                string CommandText = " SELECT   * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE,A.PI_NO , A.TRN_NUMB,A.YARN_CODE AS ITEM_CODE, B.YARN_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB  || '@' || A.PI_NO || '@' || A.YARN_CODE|| '@' ||B.YARN_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  YRN_IR_TRN A, YRN_MST B WHERE A.YARN_CODE = B.YARN_CODE AND TRN_TYPE = 'IYS01' ) WHERE ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND  (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) ORDER BY   ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
                string whereClause = string.Empty;

                if (startOffset != 0)
                {
                    whereClause += " AND YARN_CODE NOT IN(SELECT   * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE,A.PI_NO, A.TRN_NUMB, A.YARN_CODE AS ITEM_CODE,B.YARN_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.PI_NO || '@' || A.YARN_CODE|| '@' ||B.YARN_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  YRN_IR_TRN A, YRN_MST B WHERE A.YARN_CODE = B.YARN_CODE AND TRN_TYPE = 'IYS01' ) WHERE  ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) ORDER BY   ITEM_CODE) asd WHERE   ROWNUM <= 15) asd WHERE    ROWNUM <= " + startOffset + ")";
                }
                string SortExpression = " ORDER BY ITEM_CODE";
                string SearchQuery = text.ToUpper() + "%";
                 data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
          
              
            }
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetItemsCount(string text)
    {
        DataTable data = null;
        try
        {
            if (ddlOrderType.SelectedItem.ToString() == "FABRIC")
            {
                string CommandText = "SELECT * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE, A.TRN_NUMB,A.FABR_CODE AS ITEM_CODE,B.FABR_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.FABR_CODE|| '@' ||B.FABR_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  TX_FABRIC_IR_TRN A, TX_FABRIC_MST B WHERE A.FABR_CODE = B.FABR_CODE AND TRN_TYPE = 'IFS01' ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd  ";
                string WhereClause = " ";
                string SortExpression = " ORDER BY ITEM_CODE ";
                string SearchQuery = text.ToUpper() + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
               
            }
            else if (ddlOrderType.SelectedItem.ToString() == "YARN SPINING")
            {
                string CommandText = "SELECT   * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE, A.TRN_NUMB,A.YARN_CODE AS ITEM_CODE, B.YARN_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.YARN_CODE || '@' ||B.YARN_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  YRN_IR_TRN A, YRN_MST B WHERE A.YARN_CODE = B.YARN_CODE AND TRN_TYPE = 'IYS01' ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd  ";
                string WhereClause = " ";
                string SortExpression = " ORDER BY ITEM_CODE ";
                string SearchQuery = text.ToUpper() + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
                
            }
            
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

        try
        {
            string ArticleNo = cmbArticleNo.SelectedText.ToString();
            string shade_code = string.Empty;
            string TRN_NUMB = string.Empty;
            string PI_NO = string.Empty;
            string TRN_TYPE = string.Empty;
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            TxtTrnNo.Text = arrString[0].ToString();
            TRN_NUMB = TxtTrnNo.Text.Trim();
            Txtpino.Text = arrString[1].ToString();
            PI_NO = Txtpino.Text.Trim();
           
            TxtItemDesc.Text = arrString[3].ToString();
            ITEM_DESC = TxtItemDesc.Text.Trim();
            txtItemCodeLabel.Text = ArticleNo;
            txtNoofUnit.Text = arrString[4].ToString();
            ISS_QTY = txtNoofUnit.Text.Trim();
            TxtShadeCode.Text = arrString[5].ToString();
            shade_code = TxtShadeCode.Text.Trim();
            TxttrnType.Text = arrString[6].ToString();
            TRN_TYPE = TxttrnType.Text.Trim();

            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

  
   

    protected void txtsalerate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtNoofUnit;
            if (thisTextBox.Text != "")
            {
                double IssueQTY = 0;
                if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out IssueQTY))
                {

                    double wasteQty = 0f;
                    if (double.TryParse(CommonFuction.funFixQuotes(txtsalerate.Text.Trim()), out wasteQty))
                    {
                        double Total = ( (double.Parse(IssueQTY.ToString())- (wasteQty)));
                        txtTotalCost.Text = Total.ToString();
                

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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in No Of Unit TextChanged.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtWeightofUnit_TextChanged(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Weight Of Unit TextChanged.\r\nSee error log for detail."));
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

   

    private DataTable GetPartyData(string Text, int startOffset)
    {
            try
            {
                string CommandText = string.Empty;
                string whereClause = string.Empty;
                  CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
                   whereClause = string.Empty;
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

            string CommandText = string.Empty;
            string WhereClause = string.Empty;
            string SortExpression = string.Empty;
            string SearchQuery = string.Empty;
            DataTable data = null;
           
            
            
                CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
                WhereClause = " ";
                SortExpression = " ";
                SearchQuery = text.ToUpper() + "%";
                data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            
            return data.Rows.Count;
        }

        protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
        {
            try
            {
                DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

                cmbPartyCode.Items.Clear();
           

                cmbPartyCode.DataSource = data;
                cmbPartyCode.DataBind();

                e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
                e.ItemsCount = GetPartyCount(e.Text);
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
                lblMode.Text = ex.ToString();
            }
    }

    protected void cmbPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtAddress.Text = cmbPartyCode.SelectedValue.Trim();
            txtPartyCode.Text = cmbPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    
}