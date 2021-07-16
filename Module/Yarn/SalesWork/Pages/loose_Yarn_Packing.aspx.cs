using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DBLibrary;
using errorLog;
using Obout.ComboBox;
using System.Data;
using System.Collections;



public partial class Module_Yarn_SalesWork_Pages_loose_Yarn_Packing : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    DataTable dtPackingDetailTBL = null;
    //ViewState["dtPackingDetailTBL"];

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!Page.IsPostBack)
            {
                InitialisePage();
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
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];


            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                savePackingDetails();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void savePackingDetails()
    {
        try
        {

            string PROCESS_ID = string.Empty;
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];

            SaitexDM.Common.DataModel.TX_LOOSE_PACKING_MST oTX_LOOSE_PACKING_MST = new SaitexDM.Common.DataModel.TX_LOOSE_PACKING_MST();

            oTX_LOOSE_PACKING_MST.ORDER_NO = CommonFuction.funFixQuotes(txtOrderNo1.Text);
            oTX_LOOSE_PACKING_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtpartycode.Text);
            oTX_LOOSE_PACKING_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtpartyname.Text);
            oTX_LOOSE_PACKING_MST.PRODUCT_NAME = CommonFuction.funFixQuotes(TxtPrdQty.Text);
            var ordDt = System.DateTime.Now.Date;
            DateTime.TryParse(TxtOrderdt.Text, out ordDt);
            oTX_LOOSE_PACKING_MST.ORDER_DATE = ordDt;
         
            oTX_LOOSE_PACKING_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text);
           


            oTX_LOOSE_PACKING_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_LOOSE_PACKING_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_LOOSE_PACKING_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_LOOSE_PACKING_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_LOOSE_PACKING_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
         

           
           
         

            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
            {
                bool result = SaitexBL.Interface.Method.TX_LOOSE_PACKING_MST.InsertPacking(oTX_LOOSE_PACKING_MST, out PROCESS_ID, dtPackingDetailTBL);
                if (result)
                {
                    InitialisePage();
                    string Msg = string.Empty;
                    Msg =  PROCESS_ID + " saved successfully.";
                    //Msg += @"\r\n PROCESS_ID : " + PROCESS_ID + " saved successfully.";
                    if (ViewState["dtPackingDetailTBL"] != null)
                        ViewState["dtPackingDetailTBL"] = null;
                    Common.CommonFuction.ShowMessage(Msg);
                }
                else
                {
                    CommonFuction.ShowMessage("Data  Saving Failed");
                }

            }
        }
        catch
        {
            throw;
        }
    }
    private void Bindlotid()
    {
        try
        {
            txtlotid.Enabled = true;
            string yType = string.Empty;
            string msg = string.Empty;
            string COUNT = string.Empty;
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            COUNT = arrString[2].ToString();
            yType = arrString[4].ToString();
            string yDesc = arrString[4].ToString();
            string PREFIX = SaitexBL.Interface.Method.TX_LOOSE_PACKING_MST.GetPrefixCode(yType, yDesc);
            string lotid = SaitexBL.Interface.Method.TX_LOOSE_PACKING_MST.GetlOTID(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, COUNT.Trim(), PREFIX.ToUpper());
            txtlotid.Text = lotid;
            txtlotid.Enabled = false;

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
            string msg = string.Empty;
            if (ValidateFormForSavingOrUpdating(out msg))
            {
                UpdateMaterialReceipt();
            }
            else
            {
                CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Data Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void UpdateMaterialReceipt()
    {
        try
        {
            string PROCESS_ID = string.Empty;
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];

            SaitexDM.Common.DataModel.TX_LOOSE_PACKING_MST oTX_LOOSE_PACKING_MST = new SaitexDM.Common.DataModel.TX_LOOSE_PACKING_MST();

            oTX_LOOSE_PACKING_MST.ORDER_NO = CommonFuction.funFixQuotes(txtOrderNo1.Text);
            oTX_LOOSE_PACKING_MST.PRTY_CODE = CommonFuction.funFixQuotes(txtpartycode.Text);
            oTX_LOOSE_PACKING_MST.PRTY_NAME = CommonFuction.funFixQuotes(txtpartyname.Text);
            oTX_LOOSE_PACKING_MST.PRODUCT_NAME = CommonFuction.funFixQuotes(TxtPrdQty.Text);
            Int64 _PACKINGID = 0;
            if (ViewState["PACKING_ID"] != null)
            {
                Int64.TryParse(ViewState["PACKING_ID"].ToString(), out _PACKINGID);
            }

            oTX_LOOSE_PACKING_MST.PACKING_ID = _PACKINGID;
            var ordDt = System.DateTime.Now.Date;
            DateTime.TryParse(TxtOrderdt.Text, out ordDt);
            oTX_LOOSE_PACKING_MST.ORDER_DATE = ordDt;
           // var delDt = System.DateTime.Now.Date;
           // DateTime.TryParse(Txtdelivrydt.Text, out delDt);
           // oTX_LOOSE_PACKING_MST.DELIVERY_DATE = delDt;
            oTX_LOOSE_PACKING_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text);
           // double ordQty = 0;
           // double.TryParse(TxtOrdqty.Text, out ordQty);
           // oTX_LOOSE_PACKING_MST.ORDER_QTY = ordQty;


            oTX_LOOSE_PACKING_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_LOOSE_PACKING_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_LOOSE_PACKING_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_LOOSE_PACKING_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_LOOSE_PACKING_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            //oTX_LOOSE_PACKING_MST.PI_NO = CommonFuction.funFixQuotes(TextBox1.Text);





            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
            {
                bool result = SaitexBL.Interface.Method.TX_LOOSE_PACKING_MST.Update(oTX_LOOSE_PACKING_MST, out PROCESS_ID, dtPackingDetailTBL);
                if (result)
                {
                    InitialisePage();
                    string Msg = string.Empty;
                    Msg = PROCESS_ID + " Updated successfully.";
                    //Msg += @"\r\n PROCESS_ID : " + PROCESS_ID + " saved successfully.";




                    if (ViewState["dtPackingDetailTBL"] != null)
                        ViewState["dtPackingDetailTBL"] = null;
                    Common.CommonFuction.ShowMessage(Msg);
                }
                else
                {
                    CommonFuction.ShowMessage("Data  Saving Failed");
                }

            }
        }
        catch
        {
            throw;
        }
    }

    private bool ValidateFormForSavingOrUpdating(out string msg)
    {
        try
        {
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];

            bool ReturnResult = false;

            int count = 0;
            msg = string.Empty;




            if (txtOrderNo1.Text != "")
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please select OrderNo. Entry Details first.\r\n";
            }

            if (dtPackingDetailTBL != null && dtPackingDetailTBL.Rows.Count > 0)
            {
                count += 1;
            }
            else
            {
                msg += @"#. Please Enter atleast one Packing detail for Order NO.\r\n";
            }

            if (count == 2)
                ReturnResult = true;

            return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        ddlOrderNo.Visible = false;
        ddlOrderNo1.Visible = true;
        ddlOrderNo1.SelectedIndex = -1;

        lblMode.Text = "Update";
        imgbtnSave.Visible = false;
        imgbtnUpdate.Visible = true;


    }
    protected void imgbtnforward_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/Yarn_Packing.aspx", true);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
      
        ddlOrderNo.Visible = true;
        ddlOrderNo1.Visible = false;
        lblMode.Text = "Save";
        imgbtnSave.Visible = true;
        imgbtnUpdate.Visible = false;
        ClearPage();
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void btnSavePackingDetails_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];

            if (dtPackingDetailTBL == null)
                CreateDataTable();


            if (txtOrderNo1.Text != "" && txtConeWT.Text != "")
            {
                int UNIQUEID = 0;
                bool bb = true;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                if (UNIQUEID > 0)
                { 
                    bb = false; 
                }
                else
                {
                    bb = SearchItemCodeInGrid(cmbArticleNo.SelectedValue.Trim(), UNIQUEID);
                }

                if (!bb)
                {
                    double Qty = 0;
                    double.TryParse(txtConeWT.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UNIQUEID > 0)
                        {
                            var dv = new DataView(dtPackingDetailTBL);
                            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                            if (dv.Count > 0)
                            {
                                dv[0]["UNIQUEID"] = UNIQUEID;
                                dv[0]["COUNT"] = txtCount.Text.Trim();
                                dv[0]["BLEND_PER"] = txtBlend.Text.Trim();
                                dv[0]["LOT_NO"] = txtlotid.Text.Trim();
                                dv[0]["CONE_TIP"] = txtConetip.Text;
                                dv[0]["CH"] = ddlStatus.SelectedValue;
                                dv[0]["CONE_WT"] = txtConeWT.Text;
                                dv[0]["PI_NO"] = TxtPiNO.Text;
                                dv[0]["LOT_QTY"] = TxtBalQty.Text;
                                dv[0]["NO_OF_CONES"] = txtNoofCones.Text;
                                dv[0]["WEIGHT_IN_KG"] = txtweightinkg.Text;
                                dv[0]["REMARKS"] = txtSubRemark.Text;
                                dv[0]["YARN_CODE"] = TxtYarnCode.Text;
                                dv[0]["YARN_DESC"] = TxtYarnDesc.Text;
                                dtPackingDetailTBL.AcceptChanges();
                                ViewState["UNIQUEID"] = null;

                            }
                        }
                        else
                        {
                            var dr = dtPackingDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtPackingDetailTBL.Rows.Count + 1;
                            dr["COUNT"] = txtCount.Text.Trim();
                            dr["BLEND_PER"] = txtBlend.Text.Trim();
                            dr["LOT_NO"] = txtlotid.Text.Trim();
                            dr["CONE_TIP"] = txtConetip.Text;
                            dr["CH"] = ddlStatus.SelectedValue;
                            dr["CONE_WT"] = txtConeWT.Text;
                            dr["PI_NO"] = TxtPiNO.Text;
                            dr["LOT_QTY"] = TxtBalQty.Text;
                            dr["NO_OF_CONES"] = txtNoofCones.Text;
                            dr["WEIGHT_IN_KG"] = txtweightinkg.Text;
                            dr["REMARKS"] = txtSubRemark.Text;
                            dr["YARN_CODE"] = TxtYarnCode.Text;
                            dr["YARN_DESC"] = TxtYarnDesc.Text;
                            dtPackingDetailTBL.Rows.Add(dr);
                        }

                        ClearPackingDetails();
                        //cmbArticleNo.Enabled = true;
                    }


                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero . ');", true);
                    }
              
                }
               else
               {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('select valid Packing type');", true);
                }
            }
            else
            {
                //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Please select sub details button first');", true);
            }

            ViewState["dtPackingDetailTBL"] = dtPackingDetailTBL;
            BindGridFromDataTable();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding transaction data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    private void CreateDataTable()
    {
        try
        {
            dtPackingDetailTBL = new DataTable();
            dtPackingDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            dtPackingDetailTBL.Columns.Add("YARN_CODE", typeof(string));
            dtPackingDetailTBL.Columns.Add("YARN_DESC", typeof(string));
            dtPackingDetailTBL.Columns.Add("BLEND_PER", typeof(string));
            dtPackingDetailTBL.Columns.Add("COUNT", typeof(double));
            dtPackingDetailTBL.Columns.Add("LOT_NO", typeof(double));
            dtPackingDetailTBL.Columns.Add("CONE_TIP", typeof(string));
            dtPackingDetailTBL.Columns.Add("PI_NO", typeof(string));
            dtPackingDetailTBL.Columns.Add("LOT_QTY", typeof(double));

            dtPackingDetailTBL.Columns.Add("CONE_WT", typeof(double));
            dtPackingDetailTBL.Columns.Add("CH", typeof(string));
            dtPackingDetailTBL.Columns.Add("NO_OF_CONES", typeof(double));
            dtPackingDetailTBL.Columns.Add("WEIGHT_IN_KG", typeof(double));
            dtPackingDetailTBL.Columns.Add("REMARKS", typeof(string));
            ViewState["dtPackingDetailTBL"] = dtPackingDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    private bool SearchItemCodeInGrid(string PACKING_TYPE, int UNIQUEID)
    {
        bool Result = false;

        try
        {
            if (grdPIDetail.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdPIDetail.Rows)
                {
                    var TxtYarnCode = (Label)grdRow.FindControl("lblYarnCode");
                    var lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (TxtYarnCode.Text.Trim().Contains(PACKING_TYPE.Trim()) && UNIQUEID != iUNIQUEID)
                    {
                        Result = true;
                        break;

                    }
                    else
                    {
                        Result = false;
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

    private void BindGridFromDataTable()
    {
        try
        {
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
            if (dtPackingDetailTBL == null || dtPackingDetailTBL.Rows.Count < 1)
            {
                // InsertBlankRowInTable();
                grdPIDetail.DataSource = null;
                grdPIDetail.DataBind();
            }
            else
            {
                grdPIDetail.DataSource = dtPackingDetailTBL;
                grdPIDetail.DataBind();
            }
        }
        catch
        {
            throw;
     }
    }

    protected void btnCancelPackingDetails_Click(object sender, EventArgs e)
    {
        ClearPackingDetails();
    }

    protected void ClearPage()
    {
        ddlOrderNo.SelectedIndex = -1;
        txtpartycode.Text = string.Empty;
        txtpartyname.Text = string.Empty;
        TxtOrderdt.Text = string.Empty;
        TxtPrdQty.Text = string.Empty;
        txtRemarks.Text = string.Empty;
       
      
        txtOrderNo1.Text = string.Empty;
        ClearPackingDetails();
        ViewState["dtPackingDetailTBL"] = null;
        grdPIDetail.DataSource = null;
        grdPIDetail.DataBind();
    }

    protected void ClearPackingDetails()
    {
          ddlStatus.SelectedIndex = 0;
          txtNoofCones.Text = string.Empty;
          txtweightinkg.Text = string.Empty;
          txtConeWT.Text = string.Empty;
          txtConetip.Text = string.Empty;
          txtlotid.Text = string.Empty;
          txtSubRemark.Text = string.Empty;

          TxtPiNO.Text = string.Empty;
          TxtBalQty.Text = string.Empty;
          txtBlend.Text = string.Empty;
          ddlStatus.Enabled = true;
          TxtYarnCode.Text = string.Empty;
          TxtYarnDesc.Text = string.Empty;
          txtCount.Text = string.Empty;
          cmbArticleNo.SelectedIndex = -1;

    }

    protected void InitialisePage()
    {
        ddlOrderNo1.Visible = false;
        ddlOrderNo.Visible = true;
        lblMode.Text = "Save";
        imgbtnSave.Visible = true;
        imgbtnUpdate.Visible = false;
        if (ViewState["dtPackingDetailTBL"] != null)
            ViewState["dtPackingDetailTBL"] = null;
        ViewState["PACKING_ID"] = null;
        ClearPage();
        ddlOrderNo.SelectedIndex = -1;

    }

    protected void ddlOrderNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = GetLOVForOrderNo(e.Text.ToUpper(), e.ItemsOffset);
            ddlOrderNo.Items.Clear();

            ddlOrderNo.DataSource = data;
            ddlOrderNo.DataTextField = "ORDER_NO";
            ddlOrderNo.DataValueField = "ORDER_DATA";
            ddlOrderNo.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    private DataTable GetLOVForOrderNo(string text, int startOffset)
    {
        try
        {
            DataTable data = null;
            var CommandText = string.Empty;
            var whereClause = string.Empty;

            if (string.Compare(lblMode.Text, "Save", true) != 1)
            {
                CommandText = " SELECT   *  FROM   (SELECT  distinct *    FROM   (  SELECT   (   a.ORDER_NO || '@'|| a.ORDER_DATE|| '@'|| a.PRODUCT_TYPE|| '@'|| a.PRTY_CODE|| '@'|| a.PRTY_NAME )ORDER_DATA, TRUNC (A.ORDER_DATE) AS ORDER_DATE,A.ORDER_NO ,a.PRTY_CODE,a.PRTY_NAME,a.PRODUCT_TYPE AS PRODUCT_NAME FROM   V_YARN_DETAIL_MST a WHERE   ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY   ORDER_NO)) WHERE   ORDER_NO  IN (SELECT   DISTINCT YDM.ORDER_NO          FROM              TX_FIBER_WIP_STOCK WP,    V_YARN_DETAIL_MST YDM       WHERE        YDM.PI_NO= WP.ORDER_NO AND NVL (YDM.ORDER_QTY, 0) - NVL (WP.ADJ_LOT_QTY, 0) > 0) AND ROWNUM <= 25";
            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {
                CommandText = "SELECT   *  FROM   (SELECT  distinct *    FROM   (  SELECT   (   a.ORDER_NO || '@'|| a.ORDER_DATE|| '@'|| a.PRODUCT_TYPE|| '@'|| a.PRTY_CODE|| '@'|| a.PRTY_NAME )ORDER_DATA, TRUNC (A.ORDER_DATE) AS ORDER_DATE,A.ORDER_NO ,a.PRTY_CODE,a.PRTY_NAME,a.PRODUCT_TYPE AS PRODUCT_NAME FROM   V_YARN_DETAIL_MST a WHERE   ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY   ORDER_NO)) WHERE   ORDER_NO  IN (SELECT   DISTINCT YDM.ORDER_NO          FROM              TX_FIBER_WIP_STOCK WP,    V_YARN_DETAIL_MST YDM       WHERE        YDM.PI_NO= WP.ORDER_NO AND NVL (YDM.ORDER_QTY, 0) - NVL (WP.ADJ_LOT_QTY, 0) > 0) AND ROWNUM <= 25";
            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {

                    whereClause = " AND ORDER_NO NOT IN(SELECT  distinct *  FROM   (SELECT   *  FROM   (SELECT   *    FROM   (  SELECT   (   a.ORDER_NO || '@'|| a.ORDER_DATE|| '@'|| a.PRODUCT_TYPE|| '@'|| a.PRTY_CODE|| '@'|| a.PRTY_NAME )ORDER_DATA, TRUNC (A.ORDER_DATE) AS ORDER_DATE,A.ORDER_NO ,a.PRTY_CODE,a.PRTY_NAME,a.PRODUCT_TYPE AS PRODUCT_NAME FROM   V_YARN_DETAIL_MST a WHERE   ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY   ORDER_NO)) WHERE   ORDER_NO  IN (SELECT   DISTINCT YDM.ORDER_NO          FROM              TX_FIBER_WIP_STOCK WP,    V_YARN_DETAIL_MST YDM       WHERE        YDM.PI_NO= WP.ORDER_NO AND NVL (YDM.ORDER_QTY, 0) - NVL (WP.ADJ_LOT_QTY, 0) > 0)     AND ROWNUM <= " + startOffset + ")";

                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {
                    //whereClause = " AND ORDER_NO NOT IN(SELECT ORDER_NO FROM (SELECT * FROM (SELECT ( a.ORDER_DATE || '@' || a.FABRIC_BOM_FLAG || '@' || a.PRODUCT_TYPE || '@' || a.ORD_QTY || '@'||a.DEL_DATE|| '@'||a.PRTY_CODE|| '@'|| a.PRTY_NAME) ORDER_DATA, trunc(A.ORDER_DATE) as ORDER_DATE,A.ORDER_NO,A.PRODUCT_TYPE,a.PRTY_CODE,a.PRTY_NAME,a.FABRIC_BOM_FLAG,a.ORD_QTY,a.DEL_DATE FROM V_APP_CUTTING_ORD_MST a  WHERE ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY ORDER_NO) WHERE ROWNUM <= " + startOffset + ")";
                }
            }

            var SortExpression = " ORDER BY ORDER_NO ";
            var SearchQuery = text + "%";
            data = SaitexBL.Interface.Method.TX_FABRIC_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderNo_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        try
        {

            ClearPackingDetails();
            var OrderDate = System.DateTime.Now;
          
            var ProductName = string.Empty;
            var OrderNo = string.Empty;
           
            var Packing_id = string.Empty;
            var Remarks = string.Empty;
           
            txtOrderNo1.Text = ddlOrderNo.SelectedText.ToString();
            var cString = ddlOrderNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            OrderNo = arrString[0].ToString();
           
            OrderDate = DateTime.Parse(arrString[1].ToString());
            ProductName = arrString[2].ToString();
           
            txtpartycode.Text = arrString[3].ToString();
            txtpartyname.Text = arrString[4].ToString();
           

            GetDataForOrderDetail( OrderDate, ProductName);



        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }

    }

    private void GetDataForOrderDetail( DateTime OrderDate, string ProductType)
    {
        try
        {
          
            TxtOrderdt.Text = OrderDate.ToShortDateString();
            TxtPrdQty.Text = ProductType;
           
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
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];

            if (!dtPackingDetailTBL.Columns.Contains("UNIQUEID"))
                dtPackingDetailTBL.Columns.Add("UNIQUEID", typeof(int));
            if (!dtPackingDetailTBL.Columns.Contains("ROWSTATE"))
                dtPackingDetailTBL.Columns.Add("ROWSTATE", typeof(string));
            if (!dtPackingDetailTBL.Columns.Contains("PACKING_ID"))
                dtPackingDetailTBL.Columns.Add("PACKING_ID", typeof(string));



            for (int iLoop = 0; iLoop < dtPackingDetailTBL.Rows.Count; iLoop++)
            {
                dtPackingDetailTBL.Rows[iLoop]["UNIQUEID"] = iLoop + 1;
                dtPackingDetailTBL.Rows[iLoop]["ROWSTATE"] = "UPDATE";

            }
            ViewState["dtPackingDetailTBL"] = dtPackingDetailTBL;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Reports/looseyarnpack1.aspx", false);
    }
    protected void grdPIDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UNIQUEID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditDetail")
            {
                EditPackingDetailsRow(UNIQUEID);
            }
            else if (e.CommandName == "DelDetail")
            {
                deleteEditPackingDetailsRowRow(UNIQUEID);

                BindGridFromDataTable();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Editing/ Deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    private void EditPackingDetailsRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
            var dv = new DataView(dtPackingDetailTBL);
            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                txtCount.Text = dv[0]["COUNT"].ToString();
                txtBlend.Text = dv[0]["BLEND_PER"].ToString();
                txtlotid.Text = dv[0]["LOT_NO"].ToString();
                txtConetip.Text = dv[0]["CONE_TIP"].ToString();
                ddlStatus.SelectedValue = dv[0]["CH"].ToString();
               // ddlStatus.Enabled = false;
                txtConeWT.Text = dv[0]["CONE_WT"].ToString();
                txtNoofCones.Text = dv[0]["NO_OF_CONES"].ToString();
                TxtBalQty.Text = dv[0]["LOT_QTY"].ToString();
                TxtPiNO.Text = dv[0]["PI_NO"].ToString();
                txtweightinkg.Text = dv[0]["WEIGHT_IN_KG"].ToString();
                txtSubRemark.Text = dv[0]["REMARKS"].ToString();
                TxtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                TxtYarnDesc.Text = dv[0]["YARN_DESC"].ToString();
                ViewState["UNIQUEID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void deleteEditPackingDetailsRowRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
            if (dtPackingDetailTBL.Rows.Count == 1)
            {
                dtPackingDetailTBL.Rows.Clear();

            }
            else
            {
                foreach (DataRow dr in dtPackingDetailTBL.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUEID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtPackingDetailTBL.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtPackingDetailTBL.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUEID"] = iCount;
                }
                ViewState["dtPackingDetailTBL"] = dtPackingDetailTBL;
                //ddlPackingDetails.Enabled = true;
            }
        }
        catch
        {
            throw;
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
            {
                string CommandText = " SELECT   *    FROM   (  SELECT   DISTINCT *   FROM   (SELECT   S.ARTICAL_CODE AS YARN_CODE,       A.YARN_DESC,       A.Y_COUNT, (SELECT   DISTINCT BLEND    FROM   (      SELECT   YARN_CODE,     SUBSTR (SYS_CONNECT_BY_PATH (BLEND, '/'), 2)     BLEND  FROM   (SELECT   DISTINCT YARN_CODE,BLEND,COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt, ROW_NUMBER () OVER (PARTITION BY YARN_CODE ORDER BY BLEND)seq  FROM   (SELECT   DISTINCT YARN_CODE, BLEND FROM   YRN_BLAND_TRN)           ) WHERE   seq = cnt  START WITH   seq = 1  CONNECT BY   PRIOR seq + 1 = seq AND PRIOR YARN_CODE = YARN_CODE  ) WHERE   YARN_CODE = C.ARTICAL_CODE   )    BLEND,(SELECT   DISTINCT BLEND_PER       FROM   (     SELECT   YARN_CODE,   SUBSTR (SYS_CONNECT_BY_PATH (BLEND_PER, '/'), 2)     BLEND_PER  FROM   (SELECT    YARN_CODE,  BLEND_PER,   COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt,  ROW_NUMBER ()  OVER (PARTITION BY YARN_CODE   ORDER BY BLEND_PER) seq     FROM   (SELECT   YARN_CODE, BLEND_PER     FROM   YRN_BLAND_TRN)     )   WHERE   seq = cnt   START WITH   seq = 1       CONNECT BY   PRIOR seq + 1 = seq  AND PRIOR YARN_CODE = YARN_CODE )  WHERE   YARN_CODE = C.ARTICAL_CODE     )          BLEND_PER,    A.YARN_CAT,   C.PI_NO,  (NVL (C.ORDER_QTY, 0) - NVL (D.ADJ_LOT_QTY, 0)) ORDER_QTY, (   S.ARTICAL_CODE || '@' || A.YARN_DESC  || '@'  || A.Y_COUNT  || '@'|| (SELECT   DISTINCT BLEND_PER       FROM   (     SELECT   YARN_CODE,   SUBSTR (SYS_CONNECT_BY_PATH (BLEND_PER, '/'), 2)     BLEND_PER  FROM   (SELECT    YARN_CODE,  BLEND_PER,   COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt,  ROW_NUMBER ()  OVER (PARTITION BY YARN_CODE   ORDER BY BLEND_PER) seq     FROM   (SELECT   YARN_CODE, BLEND_PER     FROM   YRN_BLAND_TRN)     )   WHERE   seq = cnt   START WITH   seq = 1       CONNECT BY   PRIOR seq + 1 = seq  AND PRIOR YARN_CODE = YARN_CODE )  WHERE   YARN_CODE = C.ARTICAL_CODE     )   || '@'   || A.YARN_CAT|| '@' || C.PI_NO      || '@'  || (NVL (C.ORDER_QTY, 0) - NVL (D.ADJ_LOT_QTY, 0)))   AS Combined FROM   YRN_MST A, V_YARN_DETAIL_MST C, TX_FIBER_WIP_STOCK D, OD_CAPT_TRN_MAIN S WHERE    C.ARTICAL_CODE = S.ARTICAL_CODE   AND A.YARN_CODE = C.ARTICAL_CODE AND D.ORDER_NO = C.PI_NO  AND C.PI_NO = S.PI_NO AND NVL(C.ORDER_QTY,0)-NVL(D.ADJ_LOT_QTY,0)>0 AND C.ORDER_NO='" + txtOrderNo1.Text.Trim() + "' )    WHERE   (YARN_CODE LIKE :SearchQuery  OR YARN_DESC LIKE :SearchQuery)          ORDER BY   YARN_CODE) asd   WHERE   ROWNUM <= 15";
                string whereClause = string.Empty;
                if (startOffset != 0)
                {
                    whereClause += " AND YARN_CODE NOT IN(SELECT   * FROM   (  SELECT   *  FROM   (    SELECT   DISTINCT *   FROM   (SELECT   S.ARTICAL_CODE AS YARN_CODE,       A.YARN_DESC,       A.Y_COUNT, (SELECT   DISTINCT BLEND    FROM   (      SELECT   YARN_CODE,     SUBSTR (SYS_CONNECT_BY_PATH (BLEND, '/'), 2)     BLEND  FROM   (SELECT   DISTINCT YARN_CODE,BLEND,COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt, ROW_NUMBER () OVER (PARTITION BY YARN_CODE ORDER BY BLEND)seq  FROM   (SELECT   DISTINCT YARN_CODE, BLEND FROM   YRN_BLAND_TRN)           ) WHERE   seq = cnt  START WITH   seq = 1  CONNECT BY   PRIOR seq + 1 = seq AND PRIOR YARN_CODE = YARN_CODE  ) WHERE   YARN_CODE = C.ARTICAL_CODE   )    BLEND,(SELECT   DISTINCT BLEND_PER       FROM   (     SELECT   YARN_CODE,   SUBSTR (SYS_CONNECT_BY_PATH (BLEND_PER, '/'), 2)     BLEND_PER  FROM   (SELECT    YARN_CODE,  BLEND_PER,   COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt,  ROW_NUMBER ()  OVER (PARTITION BY YARN_CODE   ORDER BY BLEND_PER) seq     FROM   (SELECT   YARN_CODE, BLEND_PER     FROM   YRN_BLAND_TRN)     )   WHERE   seq = cnt   START WITH   seq = 1       CONNECT BY   PRIOR seq + 1 = seq  AND PRIOR YARN_CODE = YARN_CODE )  WHERE   YARN_CODE = C.ARTICAL_CODE     )          BLEND_PER,    A.YARN_CAT,   C.PI_NO, (NVL (C.ORDER_QTY, 0) - NVL (D.ADJ_LOT_QTY, 0)) ORDER_QTY, (   S.ARTICAL_CODE || '@' || A.YARN_DESC  || '@'  || A.Y_COUNT  ||'@'||  (SELECT   DISTINCT BLEND_PER       FROM   (     SELECT   YARN_CODE,   SUBSTR (SYS_CONNECT_BY_PATH (BLEND_PER, '/'), 2)     BLEND_PER  FROM   (SELECT    YARN_CODE,  BLEND_PER,   COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt,  ROW_NUMBER ()  OVER (PARTITION BY YARN_CODE   ORDER BY BLEND_PER) seq     FROM   (SELECT   YARN_CODE, BLEND_PER     FROM   YRN_BLAND_TRN)     )   WHERE   seq = cnt   START WITH   seq = 1       CONNECT BY   PRIOR seq + 1 = seq  AND PRIOR YARN_CODE = YARN_CODE )  WHERE   YARN_CODE = C.ARTICAL_CODE     )  || '@'   || A.YARN_CAT|| '@' || C.PI_NO      || '@'  ||(NVL (C.ORDER_QTY, 0) - NVL (D.ADJ_LOT_QTY, 0)))   AS Combined FROM   YRN_MST A, V_YARN_DETAIL_MST C, TX_FIBER_WIP_STOCK D, OD_CAPT_TRN_MAIN S WHERE    C.ARTICAL_CODE = S.ARTICAL_CODE   AND A.YARN_CODE = C.ARTICAL_CODE AND D.ORDER_NO = C.PI_NO  AND C.PI_NO = S.PI_NO  AND NVL(C.ORDER_QTY,0)-NVL(D.ADJ_LOT_QTY,0)>0 AND C.ORDER_NO='" + txtOrderNo1.Text.Trim() + "')    WHERE   (YARN_CODE LIKE :SearchQuery  OR YARN_DESC LIKE :SearchQuery) asd WHERE   ROWNUM <= " + startOffset + "))";
                }
                string SortExpression = " ORDER BY  YARN_CODE";
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
            {
                string CommandText = "SELECT   *    FROM   (    SELECT   DISTINCT *   FROM   (SELECT   S.ARTICAL_CODE AS YARN_CODE,       A.YARN_DESC,       A.Y_COUNT, (SELECT   DISTINCT BLEND    FROM   (      SELECT   YARN_CODE,     SUBSTR (SYS_CONNECT_BY_PATH (BLEND, '/'), 2)     BLEND  FROM   (SELECT   DISTINCT YARN_CODE,BLEND,COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt, ROW_NUMBER () OVER (PARTITION BY YARN_CODE ORDER BY BLEND)seq  FROM   (SELECT   DISTINCT YARN_CODE, BLEND FROM   YRN_BLAND_TRN)           ) WHERE   seq = cnt  START WITH   seq = 1  CONNECT BY   PRIOR seq + 1 = seq AND PRIOR YARN_CODE = YARN_CODE  ) WHERE   YARN_CODE = C.ARTICAL_CODE   )    BLEND,(SELECT   DISTINCT BLEND_PER       FROM   (     SELECT   YARN_CODE,   SUBSTR (SYS_CONNECT_BY_PATH (BLEND_PER, '/'), 2)     BLEND_PER  FROM   (SELECT    YARN_CODE,  BLEND_PER,   COUNT ( * ) OVER (PARTITION BY YARN_CODE) cnt,  ROW_NUMBER ()  OVER (PARTITION BY YARN_CODE   ORDER BY BLEND_PER) seq     FROM   (SELECT   YARN_CODE, BLEND_PER     FROM   YRN_BLAND_TRN)     )   WHERE   seq = cnt   START WITH   seq = 1       CONNECT BY   PRIOR seq + 1 = seq  AND PRIOR YARN_CODE = YARN_CODE )  WHERE   YARN_CODE = C.ARTICAL_CODE     )          BLEND_PER,    A.YARN_CAT,   C.PI_NO,  C.ORDER_QTY, (   S.ARTICAL_CODE || '@' || A.YARN_DESC  || '@'  || A.Y_COUNT  || '@'   || A.YARN_CAT|| '@' || C.PI_NO      || '@'  || C.ORDER_QTY)   AS Combined FROM   YRN_MST A, V_YARN_DETAIL_MST C, TX_FIBER_WIP_STOCK D, OD_CAPT_TRN_MAIN S WHERE    C.ARTICAL_CODE = S.ARTICAL_CODE   AND A.YARN_CODE = C.ARTICAL_CODE AND D.ORDER_NO = C.PI_NO  AND C.PI_NO = S.PI_NO)    WHERE   (YARN_CODE LIKE :SearchQuery  OR YARN_DESC LIKE :SearchQuery)          ORDER BY   YARN_CODE) asd     ";
                string WhereClause = " ";
                string SortExpression = " ORDER BY YARN_CODE ";
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
            string Yarn_code = cmbArticleNo.SelectedText.ToString();
            string Yarn_desc = string.Empty;
            string Count = string.Empty;
            string pi_no = string.Empty;
            string Bal_Qty = string.Empty;
            string Blend = string.Empty;
            string Blend_per = string.Empty;
            string[] arrString = cmbArticleNo.SelectedValue.Split('@');
            TxtYarnCode.Text = arrString[0].ToString();
            Yarn_code = TxtYarnCode.Text.Trim();
            TxtYarnDesc.Text = arrString[1].ToString();
            Yarn_desc = TxtYarnDesc.Text.Trim();
            txtCount.Text = arrString[2].ToString();
            Count = txtCount.Text.Trim();
            TxtYarnCode.Text = Yarn_code;
            double blnd_prc = 0;
            double.TryParse(txtBlend.Text.Trim(), out blnd_prc);
            txtBlend.Text = arrString[3].ToString();
            Blend_per = txtBlend.Text.Trim();
            TxtPiNO.Text = arrString[5].ToString();
            pi_no = TxtPiNO.Text.Trim();
            double bal_qty = 0;
            double.TryParse(TxtBalQty.Text.Trim(), out bal_qty);
            TxtBalQty.Text = arrString[6].ToString();
            Bal_Qty = TxtBalQty.Text.Trim();
            Bindlotid();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article No Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

  
    //protected void ddlPackingDetails_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlPackingDetails.SelectedIndex > 0)
    //    {
    //        lblNoOfPackingItem.Text = "No Of Packing " + ddlPackingDetails.SelectedValue;
    //    }
    //    else
    //    {
    //        lblNoOfPackingItem.Text = "No Of Packing Item";
    //    }


    //    if (!string.IsNullOrEmpty(ddlOrderNo.SelectedText) || !string.IsNullOrEmpty(ddlOrderNo1.SelectedText))
    //    {
    //        int UNIQUEID = 0;
    //        if (ViewState["UNIQUEID"] != null)
    //            UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
    //        bool bb = SearchItemCodeInGrid(ddlPackingDetails.SelectedValue.Trim(), UNIQUEID);
    //        if (!bb)
    //        {
    //            string CommandText = string.Empty;
    //            if (ViewState["dtPackingDetailTBL"] != null)
    //            {
    //                var dtPackingDetails = (DataTable)ViewState["dtPackingDetailTBL"];
    //                DataView dv = new DataView(dtPackingDetails);
    //                if (ddlPackingDetails.SelectedValue == "Poly Bag")
    //                {

    //                }
    //                //else if (ddlPackingDetails.SelectedValue == "Inner Carton")
    //                //{

    //                //    dv.RowFilter = "PACKING_TYPE='Poly Bag'";
    //                //    if (dv.Count > 0)
    //                //    {
    //                //        txtItemQty.Text = dv[0]["NO_OF_PACKING"].ToString();
    //                //        lblUOM.Text = dv[0]["PACKING_TYPE"].ToString();
    //                //    }
    //                //    else
    //                //    {
    //                //        CommonFuction.ShowMessage("Please choose Poly bag first.");
    //                //        ddlPackingDetails.SelectedIndex = -1;

    //                //    }
    //                //}
    //                else if (ddlPackingDetails.SelectedValue == "Outer Carton")
    //                {

    //                    //dv.RowFilter = "PACKING_TYPE='Inner Carton'";
    //                    dv.RowFilter = "PACKING_TYPE='Poly Bag'";
    //                    if (dv.Count > 0)
    //                    {
    //                        txtItemQty.Text = dv[0]["NO_OF_PACKING"].ToString();
    //                        lblUOM.Text = dv[0]["PACKING_TYPE"].ToString();
    //                    }
    //                    else
    //                    {
    //                        CommonFuction.ShowMessage("Please choose inner carton first.");
    //                        ddlPackingDetails.SelectedIndex = -1;
    //                    }
    //                }
    //                else
    //                {
    //                    lblNoOfPackingItem.Text = "Qty In Mtr.";
    //                }
    //            }
    //            else
    //            {
    //                if (ddlPackingDetails.SelectedValue == "Poly Bag")
    //                {
    //                    CommandText = " SELECT ORDER_NO,ORD_QTY,UOM  FROM OD_CAPT_TRN_MAIN WHERE PRODUCT_TYPE='FABRIC' AND FINAL_ORDER_CONF_CLAG ='1' AND ORDER_NO='" + ddlOrderNo.SelectedText.Trim() + "'";
    //                }
    //                //if (ddlPackingDetails.SelectedValue == "Inner Carton")
    //                //{
    //                //    CommonFuction.ShowMessage("Please choose Poly bag first.");
    //                //    ddlPackingDetails.SelectedIndex = -1;
    //                //}
    //                if (ddlPackingDetails.SelectedValue == "Outer Carton")
    //                {
    //                    //CommonFuction.ShowMessage("Please choose inner carton first.");
    //                    CommonFuction.ShowMessage("Please choose Poly bag  first.");
    //                    ddlPackingDetails.SelectedIndex = -1;
    //                }
    //                if (!string.IsNullOrEmpty(CommandText))
    //                {
    //                    var data = SaitexBL.Interface.Method.TX_FABRIC_MST.GetDataForLOV(CommandText, "", "", "", "", "");

    //                    if (data.Rows.Count > 0)
    //                    {
    //                        txtItemQty.Text = data.Rows[0]["ORD_QTY"].ToString();
    //                        lblUOM.Text = data.Rows[0]["UOM"].ToString();
    //                    }
    //                }
    //            }
    //        }
    //        else
    //        {
    //            CommonFuction.ShowMessage("Selected packing type already exits.");
    //        }
    //    }
    //}

    protected void txtNoOFItem_TextChanged(object sender, EventArgs e)
    {
        double conewt = 0;
        double noofcone = 0;
        double wtinkg = 0;
        double balqty = 0;
        double.TryParse(txtConeWT.Text, out conewt);
        double.TryParse(txtNoofCones.Text, out noofcone);
        double.TryParse(txtweightinkg.Text, out wtinkg);
        double.TryParse(TxtBalQty.Text, out balqty);
        if (string.IsNullOrEmpty(txtNoofCones.Text))
        {
         txtNoofCones.Text=Math.Round(balqty/conewt,2).ToString();
         txtweightinkg.Text = TxtBalQty.Text;

        }
        else
        {

            wtinkg = conewt * noofcone;
            string varchar = wtinkg.ToString();
            string[] checkdecimal = varchar.Split('.');
            if (checkdecimal.Length > 1)
            {
                if (double.Parse(checkdecimal[1].ToString()) > 0)
                {
                    wtinkg = double.Parse(checkdecimal[0].ToString()) + 1;
                }
            }
            else
            {
                wtinkg = double.Parse(checkdecimal[0].ToString());
            }
            txtweightinkg.ReadOnly = false;
            txtweightinkg.Text = wtinkg.ToString();
            txtweightinkg.ReadOnly = true;
        }

    }

    protected void txtweightinkg_TextChanged(object sender, EventArgs e)
    {
        double conewt = 0;
        double noofcone = 0;
        double wtinkg = 0;
        double.TryParse(txtConeWT.Text, out conewt);
        double.TryParse(txtNoofCones.Text, out noofcone);
        double.TryParse(txtweightinkg.Text, out wtinkg);
        wtinkg = conewt * noofcone;
        string varchar = wtinkg.ToString();
        string[] checkdecimal = varchar.Split('.');
        if (checkdecimal.Length > 1)
        {
            if (double.Parse(checkdecimal[1].ToString()) > 0)
            {
                wtinkg = double.Parse(checkdecimal[0].ToString()) + 1;
            }
        }
        else
        {
            wtinkg = double.Parse(checkdecimal[0].ToString());
        }
        txtweightinkg.ReadOnly = false;
        txtweightinkg.Text = wtinkg.ToString();
        txtweightinkg.ReadOnly = true;

    }
    protected void txtNoofCones_TextChanged(object sender, EventArgs e)
    {
        double conewt = 0;
        double noofcone = 0;
        double wtinkg = 0;
        double.TryParse(txtConeWT.Text, out conewt);
        double.TryParse(txtNoofCones.Text, out noofcone);
        double.TryParse(txtweightinkg.Text, out wtinkg);
        wtinkg = conewt * noofcone;
        string varchar = wtinkg.ToString();
        string[] checkdecimal = varchar.Split('.');
        if (checkdecimal.Length > 1)
        {
            if (double.Parse(checkdecimal[1].ToString()) > 0)
            {
                wtinkg = double.Parse(checkdecimal[0].ToString()) + 1;
            }
        }
        else
        {
            wtinkg = double.Parse(checkdecimal[0].ToString());
        }
        txtweightinkg.ReadOnly = false;
        txtweightinkg.Text = wtinkg.ToString();
        txtweightinkg.ReadOnly = true;

    }

    protected void grdPIDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //for (int i = 0; i < grdPIDetail.Rows.Count; i++)
        //{
        //    if ((grdPIDetail.Rows.Count - 1) == i)
        //    {
        //        LinkButton btndelete = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnDel");
        //        LinkButton btnedit = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnEdit");
        //        btndelete.Visible = true;
        //        btnedit.Visible = true;

        //    }
        //    else
        //    {
        //        LinkButton btndelete = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnDel");
        //        LinkButton btnedit = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnEdit");
        //        btndelete.Visible = false;
        //        btnedit.Visible = false;
        //    }

        //    if (grdPIDetail.Rows.Count == 1)
        //    {
        //        LinkButton btndelete = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnDel");
        //        LinkButton btnedit = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnEdit");
        //        btndelete.Visible = false;
        //        btnedit.Visible = true;
        //    }


        //}




    }

    protected void ddlOrderNo1_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = GetLOVForOrderNo1(e.Text.ToUpper(), e.ItemsOffset);
            ddlOrderNo1.Items.Clear();

            ddlOrderNo1.DataSource = data;
            ddlOrderNo1.DataTextField = "ORDER_NO";
            ddlOrderNo1.DataValueField = "ORDER_DATA";
            ddlOrderNo1.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gate Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }

    }
    private DataTable GetLOVForOrderNo1(string text, int startOffset)
    {
        try
        {
            DataTable data = null;
            var CommandText = string.Empty;
            var whereClause = string.Empty;

            if (string.Compare(lblMode.Text, "Save", true) != 1)
            {

                CommandText = " SELECT   * FROM   (SELECT   * FROM   (  SELECT   (   a.ORDER_NO  || '@' || a.ORDER_DATE || '@' || a.PRODUCT_NAME || '@' || a.PRTY_CODE || '@' || a.PRTY_NAME || '@' || a.PACKING_ID || '@'  || a.REMARKS)   ORDER_DATA,  TRUNC (A.ORDER_DATE) AS ORDER_DATE, A.ORDER_NO, a.PRTY_CODE,   a.PRTY_NAME,   a.PRODUCT_NAME, a.STATUS, a.REMARKS,  a.PACKING_ID  FROM   TX_LOOSE_YARN_PACK a  WHERE   ORDER_NO LIKE :SearchQuery  OR ORDER_DATE LIKE :SearchQuery  ORDER BY   ORDER_NO)) WHERE   ORDER_NO NOT IN (SELECT   DISTINCT ORDER_NO   FROM   TX_LOOSE_YARN_PACK AFT   WHERE   AFT.DEL_STATUS = '1')  AND ROWNUM <= 15";

            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {

                CommandText = "SELECT   * FROM   (SELECT  distinct * FROM   (  SELECT   (   a.ORDER_NO  || '@' || a.ORDER_DATE || '@' || a.PRODUCT_NAME || '@' || a.PRTY_CODE || '@' || a.PRTY_NAME || '@' || a.PACKING_ID || '@'  || a.REMARKS)   ORDER_DATA,  TRUNC (A.ORDER_DATE) AS ORDER_DATE, A.ORDER_NO, a.PRTY_CODE,   a.PRTY_NAME,   a.PRODUCT_NAME, a.STATUS, a.REMARKS,  a.PACKING_ID  FROM   TX_LOOSE_YARN_PACK a  WHERE   ORDER_NO LIKE :SearchQuery  OR ORDER_DATE LIKE :SearchQuery  ORDER BY   ORDER_NO)) WHERE   ORDER_NO  IN (SELECT   DISTINCT AFT.ORDER_NO   FROM   TX_LOOSE_YARN_PACK AFT,TX_LOOSE_YARN_PACK_TRN AT     WHERE   AFT.STATUS = '1' AND AFT.ORDER_NO=AT.ORDER_NO  AND AT.ISS_QTY IS  NULL)  AND ROWNUM <= 15";

            }

            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {

                    whereClause = " AND ORDER_NO NOT IN(SELECT   * FROM   (SELECT   * FROM   (  SELECT   (   a.ORDER_NO  || '@' || a.ORDER_DATE || '@' || a.PRODUCT_NAME  || '@' || a.PRTY_CODE || '@' || a.PRTY_NAME || '@' || a.PACKING_ID || '@'  || a.REMARKS)   ORDER_DATA,  TRUNC (A.ORDER_DATE) AS ORDER_DATE, A.ORDER_NO, a.PRTY_CODE,   a.PRTY_NAME,   a.PRODUCT_NAME, a.STATUS, a.REMARKS,  a.PACKING_ID FROM   TX_LOOSE_YARN_PACK a  WHERE   ORDER_NO LIKE :SearchQuery  OR ORDER_DATE LIKE :SearchQuery  ORDER BY   ORDER_NO)) WHERE   ORDER_NO NOT IN (SELECT   DISTINCT ORDER_NO   FROM   TX_LOOSE_YARN_PACK AFT   WHERE   AFT.DEL_STATUS = '1')    AND ROWNUM <= " + startOffset + ")";

                }
                else if (string.Compare(lblMode.Text, "Update", true) != 1)
                {

                    //whereClause = " AND ORDER_NO NOT IN(SELECT ORDER_NO FROM (SELECT * FROM (SELECT ( a.ORDER_DATE || '@' || a.FABRIC_BOM_FLAG || '@' || a.PRODUCT_TYPE || '@' || a.ORD_QTY || '@'||a.DEL_DATE|| '@'||a.PRTY_CODE|| '@'|| a.PRTY_NAME) ORDER_DATA, trunc(A.ORDER_DATE) as ORDER_DATE,A.ORDER_NO,A.PRODUCT_TYPE,a.PRTY_CODE,a.PRTY_NAME,a.FABRIC_BOM_FLAG,a.ORD_QTY,a.DEL_DATE FROM V_APP_CUTTING_ORD_MST a  WHERE ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY ORDER_NO) WHERE ROWNUM <= " + startOffset + ")";

                }
            }

            var SortExpression = " ORDER BY ORDER_NO ";
            var SearchQuery = text + "%";
            data = SaitexBL.Interface.Method.TX_FABRIC_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderNo1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        ClearPackingDetails();
        var OrderDate = System.DateTime.Now;
       
        var ProductName = string.Empty;
        var OrderNo = string.Empty;
      
        txtOrderNo1.Text = ddlOrderNo1.SelectedText.ToString();
        var cString = ddlOrderNo1.SelectedValue.Trim();
        char[] splitter = { '@' };
        string[] arrString = cString.Split(splitter);
        OrderNo = arrString[0].ToString();
     
        OrderDate = DateTime.Parse(arrString[1].ToString());
        ProductName = arrString[2].ToString();
      
        txtpartycode.Text = arrString[3].ToString();
        txtpartyname.Text = arrString[4].ToString();
        var PACKING_ID = arrString[5].ToString();
        ViewState["PACKING_ID"] = PACKING_ID;
        txtRemarks.Text = arrString[6].ToString();
        GetDataForOrderDetail( OrderDate, ProductName);
     

        var dt = SaitexBL.Interface.Method.TX_LOOSE_PACKING_MST.getPackingTrnDetails(txtOrderNo1.Text, PACKING_ID);
        if (dt != null && dt.Rows.Count > 0)
        {
            ViewState["dtPackingDetailTBL"] = dt;
            MapDataTable();
            BindGridFromDataTable();
        }


    }
    protected void txtNoOfPackingItem_TextChanged(object sender, EventArgs e)
    {

    }
}




