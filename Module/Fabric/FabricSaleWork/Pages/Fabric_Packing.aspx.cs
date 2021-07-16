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


public partial class Module_Fabric_FabricSaleWork_Pages_Fabric_Packing : System.Web.UI.Page
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

            SaitexDM.Common.DataModel.TX_PACKING_MST oTX_PACKING_MST = new SaitexDM.Common.DataModel.TX_PACKING_MST();

            oTX_PACKING_MST.ORDER_NO = CommonFuction.funFixQuotes(txtOrderNo1.Text);
            oTX_PACKING_MST.PI_NO = CommonFuction.funFixQuotes(TextBox1.Text);
            oTX_PACKING_MST.PARTY_CODE = CommonFuction.funFixQuotes(txtpartycode.Text);
            oTX_PACKING_MST.PRODUCT_NAME = CommonFuction.funFixQuotes(TxtPrdQty.Text);
            var ordDt = System.DateTime.Now.Date;
            DateTime.TryParse(TxtOrderdt.Text, out ordDt);
            oTX_PACKING_MST.ORDER_DATE = ordDt;
            var delDt = System.DateTime.Now.Date;
            DateTime.TryParse(Txtdelivrydt.Text, out ordDt);
            oTX_PACKING_MST.DEL_DATE = delDt;
            oTX_PACKING_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text);
            var ordQty = 0;
            int.TryParse(TxtOrdqty.Text, out ordQty);
            oTX_PACKING_MST.ORDER_QTY = ordQty;
            oTX_PACKING_MST.YEAR = oUserLoginDetail.OPEN_YEAR;
            oTX_PACKING_MST.TUSER = oUserLoginDetail.UserCode;
            oTX_PACKING_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_PACKING_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_PACKING_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oTX_PACKING_MST.PACKING_DATE = DateTime.Now.Date;
            if (ViewState["ARTICLE_CODE"] != null)
                oTX_PACKING_MST.ARTICLE_CODE = ViewState["ARTICLE_CODE"].ToString();

            if (ViewState["ARTICLE_NO"] != null)
                oTX_PACKING_MST.ARTICLE_NO = ViewState["ARTICLE_NO"].ToString();

            if (ViewState["SAMPLE_NO"] != null)
                oTX_PACKING_MST.SAMPLE_NO = ViewState["SAMPLE_NO"].ToString();

            if (ViewState["COLOR"] != null)
                oTX_PACKING_MST.COLOR = ViewState["COLOR"].ToString();
            
            if (ViewState["dtPackingDetailTBL"] != null)
                dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
            {
                bool result = SaitexBL.Interface.Method.TX_PACKING_MST.InsertPacking(oTX_PACKING_MST, out PROCESS_ID, dtPackingDetailTBL);
                if (result)
                {
                    InitialisePage();
                    string Msg = string.Empty;
                    Msg += @"\r\n" + PROCESS_ID + "";
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

        //    string PROCESS_ID = string.Empty;
        //    if (ViewState["dtPackingDetailTBL"] != null)
        //        dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];

        //    SaitexDM.Common.DataModel.TX_PACKING_MST oAPP_PACKING_MST = new SaitexDM.Common.DataModel.TX_PACKING_MST();

        //    oAPP_PACKING_MST.ORDER_NO = CommonFuction.funFixQuotes(txtOrderNo1.Text);
        //    oAPP_PACKING_MST.PI_NO = CommonFuction.funFixQuotes(TextBox1.Text);
        //    Int64 _PACKINGID = 0;
        //    if (ViewState["PACKING_ID"] != null)
        //    {
        //        Int64.TryParse(ViewState["PACKING_ID"].ToString(), out _PACKINGID);
        //    }

        //    oAPP_PACKING_MST.PACKING_ID = _PACKINGID;
        //    oAPP_PACKING_MST.PARTY_CODE = CommonFuction.funFixQuotes(txtpartycode.Text);
        //    oAPP_PACKING_MST.PRODUCT_NAME = CommonFuction.funFixQuotes(TxtPrdQty.Text);
        //    var ordDt = System.DateTime.Now.Date;
        //    DateTime.TryParse(TxtOrderdt.Text, out ordDt);
        //    oAPP_PACKING_MST.ORDER_DATE = ordDt;
        //    var delDt = System.DateTime.Now.Date;
        //    DateTime.TryParse(Txtdelivrydt.Text, out ordDt);
        //    oAPP_PACKING_MST.DEL_DATE = delDt;
        //    var ordQty = 0;
        //    int.TryParse(TxtOrdqty.Text, out ordQty);
        //    oAPP_PACKING_MST.ORDER_QTY = ordQty;
        //    oAPP_PACKING_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text);
        //    oAPP_PACKING_MST.YEAR = oUserLoginDetail.OPEN_YEAR;
        //    oAPP_PACKING_MST.TUSER = oUserLoginDetail.UserCode;
        //    oAPP_PACKING_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
        //    oAPP_PACKING_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;

        //    if (ViewState["dtPackingDetailTBL"] != null)
        //        dtPackingDetailTBL = (DataTable)ViewState["dtPackingDetailTBL"];
        //    {
        //        bool result = SaitexBL.Interface.Method.TX_PACKING_MST.UpdatePacking(oAPP_PACKING_MST, out PROCESS_ID, dtPackingDetailTBL, "UPDATE");
        //        if (result)
        //        {
        //            InitialisePage();

        //            string Msg = string.Empty;
        //            Msg += @"\r\n" + PROCESS_ID + "";
        //            //Msg += @"\r\n PROCESS_ID : " + PROCESS_ID + " saved successfully.";
        //            Common.CommonFuction.ShowMessage(Msg);



        //            if (ViewState["dtPackingDetailTBL"] != null)
        //                ViewState["dtPackingDetailTBL"] = null;
        //        }
        //        else
        //        {
        //            CommonFuction.ShowMessage("Data  Saving Failed");
        //        }

         //}
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

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Txtdelivrydt.Text = string.Empty;
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


            if (txtOrderNo1.Text != "" && txtItemQty.Text != "")
            {
                int UNIQUEID = 0;
                if (ViewState["UNIQUEID"] != null)
                    UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
                bool bb = SearchItemCodeInGrid(ddlPackingDetails.SelectedValue.Trim(), UNIQUEID);
                if (!bb)
                {
                    int Qty = 0;
                    int.TryParse(txtItemQty.Text.Trim(), out Qty);
                    if (Qty > 0)
                    {
                        if (UNIQUEID > 0)
                        {
                            var dv = new DataView(dtPackingDetailTBL);
                            dv.RowFilter = "UNIQUEID=" + UNIQUEID;
                            if (dv.Count > 0)
                            {
                                dv[0]["UNIQUEID"] = UNIQUEID;
                                dv[0]["ORDER_NO"] = txtOrderNo1.Text.Trim();
                                dv[0]["PACKING_TYPE"] = ddlPackingDetails.SelectedValue;
                                dv[0]["ITEM_QTY"] = Qty;
                                dv[0]["NO_OF_ITEM"] = txtNoOFItem.Text;
                                dv[0]["NO_OF_PACKING"] = txtNoOfPackingItem.Text;
                                dv[0]["WEIGHT_IN_KG"] = txtweightinkg.Text;

                                dv[0]["REMARKS"] = txtSubRemark.Text;
                                dv[0]["ROWSTATE"] = "INSERT";
                                dtPackingDetailTBL.AcceptChanges();
                                ViewState["UNIQUEID"] = null;

                            }
                        }
                        else
                        {
                            var dr = dtPackingDetailTBL.NewRow();
                            dr["UNIQUEID"] = dtPackingDetailTBL.Rows.Count + 1;
                            dr["ORDER_NO"] = txtOrderNo1.Text.Trim();
                            dr["PACKING_TYPE"] = ddlPackingDetails.SelectedValue;
                            dr["ITEM_QTY"] = Qty;
                            dr["NO_OF_ITEM"] = txtNoOFItem.Text;
                            dr["NO_OF_PACKING"] = txtNoOfPackingItem.Text;
                            dr["WEIGHT_IN_KG"] = txtweightinkg.Text;
                            dr["REMARKS"] = txtSubRemark.Text;
                            dr["ROWSTATE"] = "INSERT";
                            dtPackingDetailTBL.Rows.Add(dr);
                        }

                        ClearPackingDetails();
                        ddlPackingDetails.Enabled = true;
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
            dtPackingDetailTBL.Columns.Add("ORDER_NO", typeof(string));
            dtPackingDetailTBL.Columns.Add("PACKING_TYPE", typeof(string));
            dtPackingDetailTBL.Columns.Add("ITEM_QTY", typeof(int));
            dtPackingDetailTBL.Columns.Add("NO_OF_ITEM", typeof(int));
            dtPackingDetailTBL.Columns.Add("NO_OF_PACKING", typeof(int));
            dtPackingDetailTBL.Columns.Add("WEIGHT_IN_KG", typeof(int));

            dtPackingDetailTBL.Columns.Add("REMARKS", typeof(string));
            dtPackingDetailTBL.Columns.Add("ROWSTATE", typeof(string));
            dtPackingDetailTBL.Columns.Add("PCK_CODE", typeof(string));
            dtPackingDetailTBL.Columns.Add("PACKING_ID", typeof(string));
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
                    var txtCode = (Label)grdRow.FindControl("lblOrderDetail");
                    var lnkbtnEdit = (LinkButton)grdRow.FindControl("lnkbtnEdit");
                    int iUNIQUEID = int.Parse(lnkbtnEdit.CommandArgument.Trim());
                    if (txtCode.Text.Trim().Contains(PACKING_TYPE.Trim()) && UNIQUEID != iUNIQUEID)
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
        TxtOrdqty.Text = string.Empty;
        lblUOM.Text = string.Empty;
        Txtdelivrydt.Text = string.Empty;
        TextBox1.Text = string.Empty;
        txtOrderNo1.Text = string.Empty;
        ClearPackingDetails();
        ViewState["dtPackingDetailTBL"] = null;
        grdPIDetail.DataSource = null;
        grdPIDetail.DataBind();
    }

    protected void ClearPackingDetails()
    {
        ddlPackingDetails.SelectedIndex = 0;
        // txtOrderNo1.Text = string.Empty;
        txtNoOfPackingItem.Text = string.Empty;
        txtItemQty.Text = string.Empty;
        txtNoOFItem.Text = string.Empty;
        txtweightinkg.Text = string.Empty;
        txtSubRemark.Text = string.Empty;
        lblUOM.Text = string.Empty;
        // TextBox1.Text = string.Empty;
        ddlPackingDetails.Enabled = true;

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

                CommandText = " SELECT   *  FROM   (SELECT   *    FROM   (  SELECT   (   a.ORDER_NO || '@'||a.PI_NO || '@'|| a.ORDER_QTY|| '@'|| a.ORDER_DATE|| '@'|| a.STATUS|| '@'|| a.PRODUCT_TYPE|| '@'|| a.DEL_DATE|| '@'|| a.PRTY_CODE|| '@'|| a.PRTY_NAME|| '@'|| a.ARTICAL_CODE || '@'|| a.SHADE_CODE )ORDER_DATA, TRUNC (A.ORDER_DATE) AS ORDER_DATE,A.ORDER_NO, A.PI_NO ,A.ORDER_QTY,a.PRTY_CODE,a.PRTY_NAME,a.PRODUCT_TYPE,a.STATUS,a.DEL_DATE FROM   V_TX_FABRIC_PACKING_MST a WHERE   ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY   ORDER_NO)) WHERE   ORDER_NO NOT IN (SELECT   DISTINCT ORDER_NO   FROM   TX_FAB_PACK_MST AFT    WHERE   AFT.STATUS = '1') AND ROWNUM <= 15 ";

            }
            else if (string.Compare(lblMode.Text, "Update", true) != 1)
            {

                CommandText = "SELECT   *  FROM   (SELECT   *   FROM   (  SELECT   (   a.ORDER_NO || '@'||a.PI_NO || '@'  || a.ORDER_QTY   || '@'     || a.ORDER_DATE     || '@'    || a.STATUS      || '@'        || B.PRODUCT_NAME|| '@'  || b.DEL_DATE    || '@'|| B.PARTY_CODE  || '@'|| a.PRTY_NAME|| '@'|| a.FABRIC_CODE||'@' || B.PACKING_ID ||'@' || B.REMARKS)         ORDER_DATA,TRUNC (A.ORDER_DATE) AS ORDER_DATE,B.PACKING_ID,A.ORDER_NO, A.PI_NO ,A.ORDER_QTY,B.PARTY_CODE AS PRTY_CODE,a.PRTY_NAME,B.PRODUCT_NAME AS PRODUCT_TYPE,a.STATUS,b.DEL_DATE FROM   V_TX_FABRIC_PACKING_MST a,TX_FAB_PACK_MST B  WHERE    A.ORDER_NO=B.ORDER_NO AND (A.ORDER_NO LIKE :SearchQuery  OR A.ORDER_DATE LIKE :SearchQuery) ORDER BY   ORDER_NO)) WHERE   ORDER_NO  IN (SELECT   DISTINCT ORDER_NO   FROM   TX_FAB_PACK_MST AFT WHERE   AFT.STATUS = '1') AND ROWNUM <= 15";

            }
            
            if (startOffset != 0)
            {
                if (string.Compare(lblMode.Text, "Save", true) != 1)
                {

                    whereClause = " AND ORDER_NO NOT IN(SELECT ORDER_NO FROM (SELECT   * FROM   (   SELECT   (   a.ORDER_NO  || '@'|| a.ORDER_QTY|| '@'|| a.ORDER_DATE|| '@'|| a.STATUS|| '@'|| a.PRODUCT_TYPE|| '@'|| a.DEL_DATE|| '@'|| a.PRTY_CODE || '@'|| a.PRTY_NAME|| '@'|| a.ARTICAL_CODE || '@'|| a.SHADE_CODE )ORDER_DATA,TRUNC (A.ORDER_DATE) AS ORDER_DATE,A.ORDER_NO,A.ORDER_QTY,a.PRTY_CODE,a.PRTY_NAME,a.PRODUCT_TYPE, a.STATUS,a.DEL_DATE FROM   V_TX_FABRIC_PACKING_MST a WHERE   ORDER_NO LIKE :SearchQuery OR ORDER_DATE LIKE :SearchQuery ORDER BY   ORDER_NO) WHERE   ORDER_NO NOT IN (SELECT   DISTINCT ORDER_NO    FROM   TX_FAB_PACK_MST AFT  WHERE   AFT.STATUS = '1')      AND ROWNUM <= " + startOffset + ")";

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
            var OrderQty = string.Empty;
            var ProductType = string.Empty;
            var OrderNo = string.Empty;
            var PINo = string.Empty;
            var DeliveryDate = System.DateTime.Now;
            txtOrderNo1.Text = ddlOrderNo.SelectedText.ToString();
            var cString = ddlOrderNo.SelectedValue.Trim();
            char[] splitter = { '@' };
            string[] arrString = cString.Split(splitter);
            OrderNo = arrString[0].ToString();
            PINo = arrString[1].ToString();
            OrderQty = arrString[2].ToString();
            OrderDate = DateTime.Parse(arrString[3].ToString());
            ProductType = arrString[5].ToString();
            DeliveryDate = DateTime.Parse(arrString[6].ToString());
            txtpartycode.Text = arrString[7].ToString();
            txtpartyname.Text = arrString[8].ToString();
            TextBox1.Text = PINo;

            GetDataForOrderDetail(OrderQty, OrderDate, ProductType, DeliveryDate);



        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Order Entry Detail selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }

    }

    private void GetDataForOrderDetail(string OrderQty, DateTime OrderDate, string ProductType, DateTime DeliveryDate)
    {
        try
        {
            TxtOrdqty.Text = OrderQty;
            TxtOrderdt.Text = OrderDate.ToShortDateString();
            TxtPrdQty.Text = ProductType;
            Txtdelivrydt.Text = DeliveryDate.ToShortDateString();
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

                txtOrderNo1.Text = dv[0]["ORDER_NO"].ToString();
                ddlPackingDetails.SelectedValue = dv[0]["PACKING_TYPE"].ToString();
                ddlPackingDetails.Enabled = false;
                txtItemQty.Text = dv[0]["ITEM_QTY"].ToString();
                txtNoOFItem.Text = dv[0]["NO_OF_ITEM"].ToString();
                txtNoOfPackingItem.Text = dv[0]["NO_OF_PACKING"].ToString();
                txtweightinkg.Text = dv[0]["WEIGHT_IN_KG"].ToString();
                txtSubRemark.Text = dv[0]["REMARKS"].ToString();


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
                ddlPackingDetails.Enabled = true;
            }
        }
        catch
        {
            throw;
        }
    }

    protected void ddlPackingDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPackingDetails.SelectedIndex > 0)
        {
            lblNoOfPackingItem.Text = "No Of Packing " + ddlPackingDetails.SelectedValue;
        }
        else
        {
            lblNoOfPackingItem.Text = "No Of Packing Item";
        }


        if (!string.IsNullOrEmpty(ddlOrderNo.SelectedText) || !string.IsNullOrEmpty(ddlOrderNo1.SelectedText))
        {
            int UNIQUEID = 0;
            if (ViewState["UNIQUEID"] != null)
                UNIQUEID = int.Parse(ViewState["UNIQUEID"].ToString());
            bool bb = SearchItemCodeInGrid(ddlPackingDetails.SelectedValue.Trim(), UNIQUEID);
            if (!bb)
            {
                string CommandText = string.Empty;
                if (ViewState["dtPackingDetailTBL"] != null)
                {
                    var dtPackingDetails = (DataTable)ViewState["dtPackingDetailTBL"];
                    DataView dv = new DataView(dtPackingDetails);
                    if (ddlPackingDetails.SelectedValue == "Poly Bag")
                    {

                    }
                    //else if (ddlPackingDetails.SelectedValue == "Inner Carton")
                    //{

                    //    dv.RowFilter = "PACKING_TYPE='Poly Bag'";
                    //    if (dv.Count > 0)
                    //    {
                    //        txtItemQty.Text = dv[0]["NO_OF_PACKING"].ToString();
                    //        lblUOM.Text = dv[0]["PACKING_TYPE"].ToString();
                    //    }
                    //    else
                    //    {
                    //        CommonFuction.ShowMessage("Please choose Poly bag first.");
                    //        ddlPackingDetails.SelectedIndex = -1;

                    //    }
                    //}
                    else if (ddlPackingDetails.SelectedValue == "Outer Carton")
                    {

                        //dv.RowFilter = "PACKING_TYPE='Inner Carton'";
                        dv.RowFilter = "PACKING_TYPE='Poly Bag'";
                        if (dv.Count > 0)
                        {
                            txtItemQty.Text = dv[0]["NO_OF_PACKING"].ToString();
                            lblUOM.Text = dv[0]["PACKING_TYPE"].ToString();
                        }
                        else
                        {
                            CommonFuction.ShowMessage("Please choose inner carton first.");
                            ddlPackingDetails.SelectedIndex = -1;
                        }
                    }
                    else
                    {
                        lblNoOfPackingItem.Text = "Qty In Mtr.";
                    }
                }
                else
                {
                    if (ddlPackingDetails.SelectedValue == "Poly Bag")
                    {
                        CommandText = " SELECT ORDER_NO,ORD_QTY,UOM  FROM OD_CAPT_TRN_MAIN WHERE PRODUCT_TYPE='FABRIC' AND FINAL_ORDER_CONF_CLAG ='1' AND ORDER_NO='" + ddlOrderNo.SelectedText.Trim() + "'";
                    }
                    //if (ddlPackingDetails.SelectedValue == "Inner Carton")
                    //{
                    //    CommonFuction.ShowMessage("Please choose Poly bag first.");
                    //    ddlPackingDetails.SelectedIndex = -1;
                    //}
                    if (ddlPackingDetails.SelectedValue == "Outer Carton")
                    {
                        //CommonFuction.ShowMessage("Please choose inner carton first.");
                        CommonFuction.ShowMessage("Please choose Poly bag  first.");
                        ddlPackingDetails.SelectedIndex = -1;
                    }
                    if (!string.IsNullOrEmpty(CommandText))
                    {
                        var data = SaitexBL.Interface.Method.TX_FABRIC_MST.GetDataForLOV(CommandText, "", "", "", "", "");

                        if (data.Rows.Count > 0)
                        {
                            txtItemQty.Text = data.Rows[0]["ORD_QTY"].ToString();
                            lblUOM.Text = data.Rows[0]["UOM"].ToString();
                        }
                    }
                }
            }
            else
            {
                CommonFuction.ShowMessage("Selected packing type already exits.");
            }
        }
    }

    protected void txtNoOFItem_TextChanged(object sender, EventArgs e)
    {
        //double itemqty = 0;
        //double packitem = 0;
        //double numOfPackingItem = 0;
        //double.TryParse(txtItemQty.Text, out itemqty);
        //double.TryParse(txtNoOFItem.Text, out packitem);
        //double.TryParse(txtNoOfPackingItem.Text, out numOfPackingItem);
        //numOfPackingItem = itemqty / packitem;
        //string varchar = numOfPackingItem.ToString();
        //string[] checkdecimal = varchar.Split('.');
        //if (checkdecimal.Length > 1)
        //{
        //    if (double.Parse(checkdecimal[1].ToString()) > 0)
        //    {
        //        numOfPackingItem = double.Parse(checkdecimal[0].ToString()) + 1;
        //    }
        //}
        //else
        //{
        //    numOfPackingItem = double.Parse(checkdecimal[0].ToString());
        //}
        //txtNoOfPackingItem.ReadOnly = false;
        //txtNoOfPackingItem.Text = numOfPackingItem.ToString();
        //txtNoOfPackingItem.ReadOnly = true;

    }

    protected void grdPIDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < grdPIDetail.Rows.Count; i++)
        {
            if ((grdPIDetail.Rows.Count - 1) == i)
            {
                LinkButton btndelete = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnDel");
                LinkButton btnedit = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnEdit");
                btndelete.Visible = true;
                btnedit.Visible = true;

            }
            else
            {
                LinkButton btndelete = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnDel");
                LinkButton btnedit = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnEdit");
                btndelete.Visible = false;
                btnedit.Visible = false;
            }

            if (grdPIDetail.Rows.Count == 1)
            {
                LinkButton btndelete = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnDel");
                LinkButton btnedit = (LinkButton)grdPIDetail.Rows[i].FindControl("lnkbtnEdit");
                btndelete.Visible = false;
                btnedit.Visible = true;
            }


        }




    }

    protected void ddlOrderNo1_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            var data = GetLOVForOrderNo(e.Text.ToUpper(), e.ItemsOffset);
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

    protected void ddlOrderNo1_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        ClearPackingDetails();
        var OrderDate = System.DateTime.Now;
        var OrderQty = string.Empty;
        var ProductType = string.Empty;
        var OrderNo = string.Empty;
        var PINo = string.Empty;
        var DeliveryDate = System.DateTime.Now;
        txtOrderNo1.Text = ddlOrderNo1.SelectedText.ToString();
        var cString = ddlOrderNo1.SelectedValue.Trim();
        char[] splitter = { '@' };
        string[] arrString = cString.Split(splitter);
        OrderNo = arrString[0].ToString();
        PINo = arrString[1].ToString();
        OrderQty = arrString[2].ToString();
        OrderDate = DateTime.Parse(arrString[3].ToString());
        ProductType = arrString[4].ToString();
        DeliveryDate = DateTime.Parse(arrString[6].ToString());
        txtpartycode.Text = arrString[7].ToString();
        txtpartyname.Text = arrString[8].ToString();
        var PACKING_ID = arrString[10].ToString();
        ViewState["PACKING_ID"] = PACKING_ID;
        txtRemarks.Text = arrString[10].ToString();
        GetDataForOrderDetail(OrderQty, OrderDate, ProductType, DeliveryDate);
        TextBox1.Text = PINo;

        var dt = SaitexBL.Interface.Method.TX_PACKING_MST.getPackingTrnDetails(txtOrderNo1.Text, PACKING_ID);
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



