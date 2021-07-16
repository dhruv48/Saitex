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


public partial class Module_Waste_Controls_Waste_MgtReceive : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    private string PRODUCT_TYPE = string.Empty;
    private string ITEM_DESC = string.Empty;
    private string ISS_QTY = string.Empty;
    private static double FinalTotal;
    private DataTable dtOrderST;
    private string strContext = string.Empty;
    string url = string.Empty;
    private static string TRN_TYPE = string.Empty;

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
          
            TRN_TYPE = "RWS01";
            ActivateSaveMode();
            cmbOrderNo.Visible = false;
            txtOrderNo.Visible = true;
            ClearMainData();
            BindOrderNo();
            bindProcess();
            EnablePrimaryFields();
            BlankSTControls();

            if (ViewState["dtOrderST"] != null)
            {
                dtOrderST = (DataTable)ViewState["dtOrderST"];
            }

            dtOrderST = null;
            ViewState["dtOrderST"] = dtOrderST;

            bindSTGrid();
            BindNewMRNNum();
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


    private void bindProcess()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PROCESS_WASTE", oUserLoginDetail.COMP_CODE);
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
            ddlProcess.DataSource = dt;
            ddlProcess.DataValueField = "MST_CODE";
            ddlProcess.DataTextField = "MST_DESC";
            ddlProcess.DataBind();
            ddlProcess.Items.Insert(0, "Select");
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
           // txtAddress.Text = string.Empty;
           // cmbPartyCode.SelectedIndex = -1;
            //txtPartyCode.Text = "";

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

            string WMNo = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetNewWMNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, CRLocationPrefix, CRType);
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
           // txtAddress.Text = Val;
           // txtPartyCode.Text = Text;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Party Code Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private bool SearchPiCodeInGrid(string ArticalCode, int UNIQUE_ID)
    {
        bool Result = false;
        try
        {
            if (GridSpinningThread.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in GridSpinningThread.Rows)
                {
                  
                    Label txtArticleNo = (Label)grdRow.FindControl("txtArticleNo");
                    Label txtfabrdesc = (Label)grdRow.FindControl("txtfabrdesc");
                 
                 
                    LinkButton lnkEdit = (LinkButton)grdRow.FindControl("lnkEdit");
                    int iUNIQUE_ID = int.Parse(lnkEdit.CommandArgument.Trim());
                    if (txtArticleNo.Text.Trim() == ArticalCode && UNIQUE_ID != iUNIQUE_ID)
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
                if (dtOrderST.Rows.Count < 5)
                {
                    int UniqueId = 0;
                    if (ViewState["UNIQUE_ID"] != null)
                        UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());

                    string ArticalCode = txtItemCodeLabel.Text.ToString();
                   // string pi_no = Txtpino.Text.ToString();
                    bool bb = SearchPiCodeInGrid(ArticalCode, UniqueId);
                    if (!bb)
                    {
                       
                        double WASTE_QTY = 0;
                        double.TryParse(txtWasteQty.Text.Trim(), out WASTE_QTY);
                     
                        if (WASTE_QTY < 1)
                        {
                            Common.CommonFuction.ShowMessage(@"Waste Qty can not be zero or empty.");
                            return;
                        }
                      
                        //double TRN_NUMB = 0;
                        //string[] arrString = cmbArticleNo.SelectedValue.Split('@');
                        //double.TryParse(TxtTrnNo.Text.Trim(), out TRN_NUMB);
                        //TRN_NUMB = double.Parse(ViewState["TRN_NUMB"].ToString());



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



                        if (WASTE_QTY > 0)
                        {
                            if (UniqueId > 0)
                            {
                                DataView dv = new DataView(dtOrderST);
                                dv.RowFilter = "UNIQUE_ID=" + UniqueId;
                                if (dv.Count > 0)
                                {

                                    dv[0]["ITEM_CODE"] = txtItemCodeLabel.Text.Trim();
                                    dv[0]["ITEM_DESC"] = TxtItemDesc.Text.ToString();
                                    dv[0]["CAT_CODE"] = Txtcatcode.Text.ToString();
                                  
                                    dv[0]["WASTE_QTY"] = WASTE_QTY;
                                    dv[0]["UOM"] = txtUom.Text.ToString();
                                    dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                                  

                                }
                                dtOrderST.AcceptChanges();
                            }
                            else
                            {
                                DataRow dr = dtOrderST.NewRow();
                                dr["UNIQUE_ID"] = dtOrderST.Rows.Count + 1;
                                dr["ITEM_CODE"] = txtItemCodeLabel.Text.Trim();
                                dr["ITEM_DESC"] = TxtItemDesc.Text.ToString();
                                dr["CAT_CODE"] = Txtcatcode.Text.ToString();
                            
                                dr["WASTE_QTY"] = WASTE_QTY;
                                dr["UOM"] = txtUom.Text.ToString();
                                dr["REMARKS"] = txtRemarks.Text.Trim();
                               
                             

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



            //countAll += 1;
            //double dd = 0;
            //double.TryParse(txtUom.Text.Trim(), out dd);
            //if (dd > 0)
            //{
            //    count += 1;
            //}
            //else
            //{
            //    msg = msg + msgCount.ToString() + ": Please Select uom. ";
            //    txtUom.Text = string.Empty;
            //}



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
        //try
        //{
        //    TextBox thisTextBox = (TextBox)txtNoofUnit;
        //    if (thisTextBox.Text != "")
        //    {
        //        double IssueQTY = 0;
        //        if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out IssueQTY))
        //        {

        //            double wasteQty = 0f;
        //            if (double.TryParse(CommonFuction.funFixQuotes(txtsalerate.Text.Trim()), out wasteQty))
        //            {
        //                double Total = ((double.Parse(IssueQTY.ToString()) - (wasteQty)));
        //                txtTotalCost.Text = Total.ToString();


        //            }
        //        }
        //        else
        //        {
        //            thisTextBox.Text = "0";
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
        //    errorLog.ErrHandler.WriteError(ex.Message);
        //}
    }
    private void CreateSTDataTable()
    {
        try
        {
            dtOrderST = new DataTable();
            dtOrderST.Columns.Add("UNIQUE_ID", typeof(int));
            dtOrderST.Columns.Add("ITEM_CODE", typeof(string));
            dtOrderST.Columns.Add("ITEM_DESC", typeof(string));
            dtOrderST.Columns.Add("CAT_CODE", typeof(string));
            dtOrderST.Columns.Add("UOM", typeof(string));
            dtOrderST.Columns.Add("WASTE_QTY", typeof(double));
          
            dtOrderST.Columns.Add("REMARKS", typeof(string));
         

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
             Txtcatcode.Text = string.Empty;
             txtWasteQty.Text = string.Empty;
             txtUom.Text = string.Empty;
           txtRemarks.Text = string.Empty;
         
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

                cmbArticleNo.Enabled = true;
                txtItemCodeLabel.Text = dv[0]["ITEM_CODE"].ToString();
                TxtItemDesc.Text = dv[0]["ITEM_DESC"].ToString();
                Txtcatcode.Text = dv[0]["CAT_CODE"].ToString();
                txtUom.Text = dv[0]["UOM"].ToString();
             
                txtWasteQty.Text = dv[0]["WASTE_QTY"].ToString();

                txtRemarks.Text = dv[0]["REMARKS"].ToString();
             ;

             
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
                //Label txtTotalCost = (Label)row.FindControl("txtTotalCost");

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
                    SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();
                    oTX_WASTE_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oTX_WASTE_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oTX_WASTE_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oTX_WASTE_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                    oTX_WASTE_IR_MST.WR_LOCATION = txtCrLocation.Text.Trim();
                    oTX_WASTE_IR_MST.WR_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                    oTX_WASTE_IR_MST.PROCESS = ddlProcess.SelectedValue.Trim();
                    oTX_WASTE_IR_MST.PRODUCT_TYPE = ddlOrderType.SelectedValue.Trim();
                   // int WR_NUMB = 0;
                   // int.TryParse(txtOrderNo.Text, out WR_NUMB);
                    oTX_WASTE_IR_MST.WR_NO = txtOrderNo.Text.Trim();
                    oTX_WASTE_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);



                    oTX_WASTE_IR_MST.TUSER = oUserLoginDetail.UserCode;
                    oTX_WASTE_IR_MST.REMARKS = txtMstRemarks.Text.Trim();


                    bool result = SaitexBL.Interface.Method.TX_WASTE_IR_MST.InsertRecevingCredit(oTX_WASTE_IR_MST, dtOrderST);


                    if (result == true)
                    {
                        string Resultmsg = "Waste Received Saved Successfully" + "\\r\\n";
                        Resultmsg += "Waste Mgt No is:" + oTX_WASTE_IR_MST.WR_NO;
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

          
            if (txtOrderNo.Text != string.Empty)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please Create WM No. ";
                msgCount += 1;
            }
            if (count == 2)
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
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT WR_NO, PRODUCT_TYPE, WR_LOCATION,TRN_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| PRODUCT_TYPE|| '@'|| WR_NO|| '@'|| TRN_DATE|| '@'|| PRTY_CODE|| '@'|| PRTY_NAME|| '@'|| RCOMMENT|| '@'|| WR_LOCATION || '@'|| TRN_NUMB) AS Combined FROM TX_WASTE_IR_MST WHERE DEL_STATUS = '0' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE WR_NO LIKE :SearchQuery  ORDER BY WR_NO) www WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND Combined NOT IN (SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT WR_NO, PRODUCT_TYPE, WR_LOCATION,WM_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| PRODUCT_TYPE|| '@'|| WR_NO|| '@'|| TRN_DATE|| '@'|| PRTY_CODE|| '@'|| PRTY_NAME|| '@'|| RCOMMENT|| '@'|| WR_LOCATION || '@'|| TRN_NUMB) AS Combined FROM TX_WASTE_IR_MST WHERE DEL_STATUS = '0' AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE WR_NO LIKE :SearchQuery  ORDER BY WR_NO) www WHERE ROWNUM <= '" + startOffset + "') ";
            }

            string SortExpression = " ORDER BY WR_NO";
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
            string CommandText = " SELECT * FROM (SELECT * FROM (SELECT * FROM (SELECT DISTINCT WR_NO, PRODUCT_TYPE, WR_LOCATION,TRN_DATE, ( COMP_CODE|| '@'|| BRANCH_CODE|| '@'|| YEAR|| '@'|| PRODUCT_TYPE|| '@'|| WR_NO|| '@'|| TRN_DATE|| '@'|| PRTY_CODE|| '@'|| PRTY_NAME|| '@'|| RCOMMENT|| '@'|| WR_LOCATION ) AS Combined FROM TX_WASTE_IR_MST WHERE DEL_STATUS = '0'  AND comp_code = '" + oUserLoginDetail.COMP_CODE + "'AND BRANCH_CODE = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND YEAR = '" + oUserLoginDetail.DT_STARTDATE.Year + "') asd) WHERE WR_NO LIKE :SearchQuery  ORDER BY WR_NO) www  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY WR_NO ";
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
                txtOrderNo.Text = dt.Rows[0]["WR_NO"].ToString();
                txtDate.Text = dt.Rows[0]["TRN_DATE"].ToString();
               
                txtCrLocation.Text = dt.Rows[0]["WR_LOCATION"].ToString();
               
                txtMstRemarks.Text = dt.Rows[0]["RCOMMENT"].ToString();
                
                ddlOrderType.SelectedValue = dt.Rows[0]["PRODUCT_TYPE"].ToString();
                ddlProcess.SelectedValue = dt.Rows[0]["PROCESS"].ToString();
                lblTrnNo.Text= dt.Rows[0]["TRN_NUMB"].ToString();
            



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

                    drST["CAT_CODE"] = dr["CAT_CODE"];
                    drST["WASTE_QTY"] = dr["WASTE_QTY"];
                    drST["UOM"] = dr["UOM"];
                   
                    drST["REMARKS"] = dr["REMARKS"];

                   
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


            string msg = string.Empty;
            if (ValidateSTMasterRow(out msg))
            {
                if (dtOrderST != null && dtOrderST.Rows.Count > 0)
                {
                    SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();
                    oTX_WASTE_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oTX_WASTE_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oTX_WASTE_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oTX_WASTE_IR_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                    oTX_WASTE_IR_MST.WR_LOCATION = txtCrLocation.Text.Trim();
                    oTX_WASTE_IR_MST.WR_DATE = Convert.ToDateTime(txtDate.Text.Trim());
                    oTX_WASTE_IR_MST.PROCESS = ddlProcess.SelectedValue.Trim();
                    oTX_WASTE_IR_MST.PRODUCT_TYPE = ddlOrderType.SelectedValue.Trim();
                    // int WR_NUMB = 0;
                    // int.TryParse(txtOrderNo.Text, out WR_NUMB);
                    oTX_WASTE_IR_MST.WR_NO = txtOrderNo.Text.Trim();
                    oTX_WASTE_IR_MST.TRN_TYPE = CommonFuction.funFixQuotes(TRN_TYPE);
                    oTX_WASTE_IR_MST.TRN_NUMB = int.Parse(lblTrnNo.Text);


                    oTX_WASTE_IR_MST.TUSER = oUserLoginDetail.UserCode;
                    oTX_WASTE_IR_MST.REMARKS = txtMstRemarks.Text.Trim();
                    oTX_WASTE_IR_MST.TRN_DATE = DateTime.Parse(txtDate.Text);

                    bool result = SaitexBL.Interface.Method.TX_WASTE_IR_MST.UpdateRecevingCredit(oTX_WASTE_IR_MST, dtOrderST);

                    if (result)
                    {
                        string Resultmsg = "Waste Management Updated Successfully" + "\\r\\n";
                        Resultmsg += "Waste Mgt No is:" + oTX_WASTE_IR_MST.WR_NO;
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                        InitialiseData();
                       // dtOrderST.Rows.Clear();
                        bindSTGrid();

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                    }
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
        url = "../Report/Waste_Reciving1.aspx?TRN_TYPE=RWS01";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + url + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
    }
    protected void UploadDataTableToExcel(DataTable dtEmp, string filename)
    {
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (DataColumn dtcol in dtEmp.Columns)
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
            string WR_NO = arrString[4].ToString();
            string TRN_DATE = arrString[5].ToString();
            string PRTY_CODE = arrString[6].ToString();
            string PRTY_NAME = arrString[7].ToString();
            string RCOMMENT = arrString[8].ToString();
            string WR_LOCATION = arrString[9].ToString();
            string TRN_NUMB = arrString[10].ToString();
            SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();
            oTX_WASTE_IR_MST.COMP_CODE = COMP_CODE;
            oTX_WASTE_IR_MST.BRANCH_CODE = BRANCH_CODE;
            oTX_WASTE_IR_MST.YEAR = int.Parse(YEAR);
            oTX_WASTE_IR_MST.PRODUCT_TYPE = PRODUCT_TYPE;
            oTX_WASTE_IR_MST.WR_NO = WR_NO;
            DateTime wmdate = DateTime.Now;
            DateTime.TryParse(TRN_DATE, out wmdate);
            oTX_WASTE_IR_MST.TRN_DATE = wmdate;

            oTX_WASTE_IR_MST.PRTY_CODE = PRTY_CODE;
            oTX_WASTE_IR_MST.PRTY_NAME = PRTY_NAME;
            oTX_WASTE_IR_MST.REMARKS = RCOMMENT;
            oTX_WASTE_IR_MST.WR_LOCATION = WR_LOCATION;



            dtMst = SaitexBL.Interface.Method.TX_WASTE_IR_MST.SelectWasteMgtMst(oTX_WASTE_IR_MST);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                BindControls(dtMst);

                dtST = SaitexBL.Interface.Method.TX_WASTE_IR_MST.SelectWasteMgtTrn(oTX_WASTE_IR_MST);
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

    //protected void cmbArticleNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            cmbArticleNo.Items.Clear();
    //            cmbArticleNo.DataSource = data;
    //            cmbArticleNo.DataBind();
    //        }

    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

    //        e.ItemsCount = GetItemsCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article loading.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}

    //protected DataTable GetItems(string text, int startOffset)
    //{
    //    DataTable data = null;
    //    try
    //    {
    //        if (ddlOrderType.SelectedItem.ToString() == "FABRIC")
    //        {
    //            string CommandText = " SELECT * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE, A.PI_NO ,A.TRN_NUMB,A.FABR_CODE AS ITEM_CODE,B.FABR_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB  || '@' || A.PI_NO || '@' || A.FABR_CODE|| '@' ||B.FABR_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  TX_FABRIC_IR_TRN A, TX_FABRIC_MST B WHERE A.FABR_CODE = B.FABR_CODE AND TRN_TYPE = 'IFS01' ) WHERE ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery)ORDER BY   ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
    //            string whereClause = string.Empty;

    //            if (startOffset != 0)
    //            {
    //                whereClause += " AND FABR_CODE NOT IN(SELECT   * FROM  ( SELECT   A.TRN_TYPE,A.PI_NO , A.TRN_NUMB,A.FABR_CODE AS ITEM_CODE,B.FABR_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.PI_NO || '@' || A.FABR_CODE|| '@' ||B.FABR_DESC|| '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  TX_FABRIC_IR_TRN A, TX_FABRIC_MST B WHERE A.FABR_CODE = B.FABR_CODE AND TRN_TYPE = 'IFS01' ) WHERE  ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) ORDER BY   ITEM_CODE) asd WHERE    ROWNUM <= " + startOffset + ")";
    //            }
    //            string SortExpression = " ORDER BY  ITEM_CODE";
    //            string SearchQuery = text.ToUpper() + "%";
    //            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        }
    //        else if (ddlOrderType.SelectedItem.ToString() == "YARN SPINING")
    //        {
    //            string CommandText = " SELECT   * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE,A.PI_NO , A.TRN_NUMB,A.YARN_CODE AS ITEM_CODE, B.YARN_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB  || '@' || A.PI_NO || '@' || A.YARN_CODE|| '@' ||B.YARN_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  YRN_IR_TRN A, YRN_MST B WHERE A.YARN_CODE = B.YARN_CODE AND TRN_TYPE = 'IYS01' ) WHERE ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND  (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) ORDER BY   ITEM_CODE) asd WHERE   ROWNUM <= 15 ";
    //            string whereClause = string.Empty;

    //            if (startOffset != 0)
    //            {
    //                whereClause += " AND YARN_CODE NOT IN(SELECT   * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE,A.PI_NO, A.TRN_NUMB, A.YARN_CODE AS ITEM_CODE,B.YARN_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.PI_NO || '@' || A.YARN_CODE|| '@' ||B.YARN_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  YRN_IR_TRN A, YRN_MST B WHERE A.YARN_CODE = B.YARN_CODE AND TRN_TYPE = 'IYS01' ) WHERE  ( TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE) NOT IN (SELECT TRN_TYPE,TRN_NUMB,PI_NO,ITEM_CODE FROM TX_WASTE_MGT_TRN WHERE STATUS='1' )AND (ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) ORDER BY   ITEM_CODE) asd WHERE   ROWNUM <= 15) asd WHERE    ROWNUM <= " + startOffset + ")";
    //            }
    //            string SortExpression = " ORDER BY ITEM_CODE";
    //            string SearchQuery = text.ToUpper() + "%";
    //            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");


    //        }
    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    //protected int GetItemsCount(string text)
    //{
    //    DataTable data = null;
    //    try
    //    {
    //        if (ddlOrderType.SelectedItem.ToString() == "FABRIC")
    //        {
    //            string CommandText = "SELECT * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE, A.TRN_NUMB,A.FABR_CODE AS ITEM_CODE,B.FABR_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.FABR_CODE|| '@' ||B.FABR_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  TX_FABRIC_IR_TRN A, TX_FABRIC_MST B WHERE A.FABR_CODE = B.FABR_CODE AND TRN_TYPE = 'IFS01' ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd  ";
    //            string WhereClause = " ";
    //            string SortExpression = " ORDER BY ITEM_CODE ";
    //            string SearchQuery = text.ToUpper() + "%";
    //            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //        }
    //        else if (ddlOrderType.SelectedItem.ToString() == "YARN SPINING")
    //        {
    //            string CommandText = "SELECT   * FROM  (SELECT   * FROM  ( SELECT   A.TRN_TYPE, A.TRN_NUMB,A.YARN_CODE AS ITEM_CODE, B.YARN_DESC AS ITEM_DESC, A.TRN_QTY,A.SHADE_CODE, (A.TRN_NUMB   || '@' || A.YARN_CODE || '@' ||B.YARN_DESC || '@' ||A.TRN_QTY|| '@' ||A.SHADE_CODE || '@' ||A.TRN_TYPE) AS Combined FROM  YRN_IR_TRN A, YRN_MST B WHERE A.YARN_CODE = B.YARN_CODE AND TRN_TYPE = 'IYS01' ) WHERE  ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY   ITEM_CODE) asd  ";
    //            string WhereClause = " ";
    //            string SortExpression = " ORDER BY ITEM_CODE ";
    //            string SearchQuery = text.ToUpper() + "%";
    //            data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

    //        }

    //        return data.Rows.Count;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}



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
        try
        {
            string CommandText = " SELECT   *  FROM   (  SELECT   ITEM_CODE,   ITEM_DESC,ITEM_TYPE,UOM,CAT_CODE,(   '' || '@'|| ITEM_CODE|| '@'|| ''|| '@'|| ITEM_DESC|| '@'|| UOM || '@' || CAT_CODE|| '@' || ''|| '@'|| UOM)  AS Combined FROM   TX_WASTE_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR CAT_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery    ORDER BY   ITEM_CODE) asd ";
    
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND ITEM_CODE NOT IN (SELECT ITEM_CODE FROM (   SELECT   ITEM_CODE,   ITEM_DESC,ITEM_TYPE,UOM,CAT_CODE,(   '' || '@'|| ITEM_CODE|| '@'|| ''|| '@'|| ITEM_DESC|| '@'|| UOM || '@' || CAT_CODE|| '@' || ''|| '@'|| UOM)  AS Combined FROM   TX_WASTE_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR CAT_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery    ORDER BY   ITEM_CODE  ) asd   AND ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }
    private int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT   *  FROM   (  SELECT   ITEM_CODE,   ITEM_DESC,ITEM_TYPE,UOM,CAT_CODE,(   '' || '@'|| ITEM_CODE|| '@'|| ''|| '@'|| ITEM_DESC|| '@'|| UOM || '@' || CAT_CODE|| '@' || ''|| '@'|| UOM)  AS Combined FROM   TX_WASTE_MST      WHERE      ITEM_CODE LIKE :SearchQuery          OR CAT_CODE LIKE :SearchQuery         OR ITEM_DESC LIKE :SearchQuery    ORDER BY   ITEM_CODE) asd  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ITEM_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
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
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            txtItemCodeLabel.Text = arrString[0].ToString();
            TxtItemDesc.Text = arrString[3].ToString();
            txtItemCodeLabel.Text = ArticleNo;
           // txtEndUse.SelectedIndex = txtEndUse.Items.IndexOf(txtEndUse.Items.FindByValue(arrString[2].ToString()));
            Txtcatcode.Text = arrString[5].ToString();
            txtUom.Text = arrString[4].ToString();
            //lblNETCART_WT.Text = arrString[6].ToString();
            //DDLCaseBox.SelectedIndex = DDLCaseBox.Items.IndexOf(DDLCaseBox.Items.FindByValue(arrString[7].ToString()));

            BindDetailByCaseSelection();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindDetailByCaseSelection()
    {
        try
        {
            string ArticleCode = txtItemCodeLabel.Text.Trim();
            SaitexDM.Common.DataModel.TX_WASTE_MASTER oTX_WASTE_MASTER = new SaitexDM.Common.DataModel.TX_WASTE_MASTER();
            oTX_WASTE_MASTER.ITEM_CODE = CommonFuction.funFixQuotes(ArticleCode);
            DataTable dt = SaitexBL.Interface.Method.TX_WASTE_MASTER.GetTX_WASTE_MASTERByItemCode(oTX_WASTE_MASTER);

            if (dt != null && dt.Rows.Count > 0)
            {
                lblUNIT_WT.Text = dt.Rows[0]["UOM"].ToString();
                lblNETBOX_WT.Text = dt.Rows[0]["UOM"].ToString();
                lblNETCART_WT.Text = dt.Rows[0]["UOM"].ToString();
            }
        }
        catch
        {

            throw;
        }
    }
    //protected void cmbArticleNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{

    //    try
    //    {
    //        string ArticleNo = cmbArticleNo.SelectedText.ToString();
    //        string shade_code = string.Empty;
    //        string TRN_NUMB = string.Empty;
    //        string PI_NO = string.Empty;
    //        string TRN_TYPE = string.Empty;
    //        string[] arrString = cmbArticleNo.SelectedValue.Split('@');
    //        TxtTrnNo.Text = arrString[0].ToString();
    //        TRN_NUMB = TxtTrnNo.Text.Trim();
    //        Txtpino.Text = arrString[1].ToString();
    //        PI_NO = Txtpino.Text.Trim();

    //        TxtItemDesc.Text = arrString[3].ToString();
    //        ITEM_DESC = TxtItemDesc.Text.Trim();
    //        txtItemCodeLabel.Text = ArticleNo;
    //        txtNoofUnit.Text = arrString[4].ToString();
    //        ISS_QTY = txtNoofUnit.Text.Trim();
    //        TxtShadeCode.Text = arrString[5].ToString();
    //        shade_code = TxtShadeCode.Text.Trim();
    //        TxttrnType.Text = arrString[6].ToString();
    //        TRN_TYPE = TxttrnType.Text.Trim();


    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
    //        lblMode.Text = ex.ToString();
    //    }
    //}




    protected void txtsalerate_TextChanged(object sender, EventArgs e)
    {
        //try
        //{
            //TextBox thisTextBox = (TextBox)txtNoofUnit;
            //if (thisTextBox.Text != "")
            //{
            //    double IssueQTY = 0;
            //    if (double.TryParse(CommonFuction.funFixQuotes(thisTextBox.Text.Trim()), out IssueQTY))
            //    {

            //        double wasteQty = 0f;
            //        if (double.TryParse(CommonFuction.funFixQuotes(txtsalerate.Text.Trim()), out wasteQty))
            //        {
            //            double Total = ((double.Parse(IssueQTY.ToString()) - (wasteQty)));
            //            txtTotalCost.Text = Total.ToString();


        //            }
        //        }
        //        else
        //        {
        //            thisTextBox.Text = "0";
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in No Of Unit TextChanged.\r\nSee error log for detail."));
        //    lblMode.Text = ex.ToString();
        //}
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

           // cmbPartyCode.Items.Clear();


          //  cmbPartyCode.DataSource = data;
          //  cmbPartyCode.DataBind();

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
           // txtAddress.Text = cmbPartyCode.SelectedValue.Trim();
           // txtPartyCode.Text = cmbPartyCode.SelectedText.Trim();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private void BindNewMRNNum()
    {
        try
        {
            SaitexDM.Common.DataModel.TX_WASTE_IR_MST oTX_WASTE_IR_MST = new SaitexDM.Common.DataModel.TX_WASTE_IR_MST();
            oTX_WASTE_IR_MST.TRN_TYPE = TRN_TYPE;
            oTX_WASTE_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_WASTE_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_WASTE_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            lblTrnNo.Text = SaitexBL.Interface.Method.TX_WASTE_IR_MST.GetNewTRNNumber(oTX_WASTE_IR_MST).ToString();
        }
        catch
        {
            throw;
        }
    }

}