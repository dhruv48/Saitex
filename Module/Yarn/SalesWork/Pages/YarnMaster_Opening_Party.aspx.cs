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


public partial class Module_Yarn_SalesWork_Pages_YarnMaster_Opening_Party : System.Web.UI.Page
{
    DataTable dtColorDetail;
    DataTable dtTRN_SUB;
    string msg = string.Empty;
    string Errormsg = string.Empty;
    string ArticleCode = string.Empty;
    string newShortcode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.YRN_MST oYRNMST = new SaitexDM.Common.DataModel.YRN_MST();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            //Page.MaintainScrollPositionOnPostBack = true;
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

    private void Initialize()
    {
        try
        {
            BindDepartment(ddlStore);
            BindDropDown(ddlLocation);
            ddlyarncode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            txtYarnCode.Text = "";
            txtYarnCode.Visible = false;


        }
        catch
        {
            throw;
        }
    }

    private void RefreshControls()
    {
        try
        {
            txtYarnDescription.Text = string.Empty;
            ddlyarncode.SelectedIndex = -1;
            cmbJober.SelectedIndex = -1;
            ViewState["dtColorDetail"] = null;
            Session["dtTRN_SUB"] = null;
            grdColorDetail.DataSource = null;
            grdColorDetail.DataBind();
        }
        catch
        {
            throw;
        }

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Save";
            Response.Redirect("~/Module/Yarn/SalesWork/Pages/YarnMaster_Opening.aspx", false);
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
                oYRNMST.YARN_CODE = txtYarnCode.Text.Trim();
                oYRNMST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oYRNMST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oYRNMST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oYRNMST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
                oYRNMST.TUSER = oUserLoginDetail.UserCode;
                oYRNMST.ARTICLE_CODE = txtYarnCode.Text.Trim();
                oYRNMST.LOCATION = oUserLoginDetail.VC_BRANCHNAME;
                oYRNMST.STORE = oUserLoginDetail.VC_DEPARTMENTNAME;
                oYRNMST.UOM = "KG";
                if (ViewState["dtColorDetail"] != null)
                    dtColorDetail = (DataTable)ViewState["dtColorDetail"];
                if (Session["dtTRN_SUB"] != null)
                    dtTRN_SUB = (DataTable)Session["dtTRN_SUB"];
                if (Errormsg == string.Empty)
                {
                    bool C = SaitexDL.Interface.Method.YRN_MST.InsertYarnColor(oYRNMST, dtColorDetail);
                    bool resultTrn = false;

                    string[] columnArr = new String[] { "PRTY_CODE", "YARN_CODE", "PRTY_NAME","LOCATION","STORE"};                
                    DataView mdvDetailTBL = new DataView(dtColorDetail);
                    DataTable mdtDetailTBL = mdvDetailTBL.ToTable(true, columnArr);

                    for (int i = 0; i < mdtDetailTBL.Rows.Count; i++)
                    {
                        oYRNMST.PRTY_CODE = mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString();
                        oYRNMST.PRTY_NAME = mdtDetailTBL.Rows[i]["PRTY_NAME"].ToString();
                        oYRNMST.LOCATION = mdtDetailTBL.Rows[i]["LOCATION"].ToString();
                        oYRNMST.STORE = mdtDetailTBL.Rows[i]["STORE"].ToString();
                       
                        SaitexDM.Common.DataModel.YRN_IR_MST OYRN_IR_MST = new SaitexDM.Common.DataModel.YRN_IR_MST();
                        OYRN_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE ;
                        OYRN_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                        OYRN_IR_MST.TRN_TYPE = "OJB01";
                        OYRN_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                        OYRN_IR_MST.PRTY_CODE = mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString();
                        
                       
                        int TRN = 0;
                        int.TryParse(SaitexDL.Interface.Method.YRN_IR_MST.GetMRNNumber(OYRN_IR_MST, mdtDetailTBL.Rows[i]["YARN_CODE"].ToString()),out TRN );

                        if (TRN > 0)
                        {
                            oYRNMST.TRN_NUMB = TRN;
                        }
                        else
                        {
                            oYRNMST.TRN_NUMB = int.Parse(SaitexBL.Interface.Method.YRN_IR_MST.GetNewTRNNumber(OYRN_IR_MST).ToString());
                        }
                        oYRNMST.TRN_TYPE = "OJB01";
                        DataView dvColorDetail = new DataView(dtColorDetail);
                        dvColorDetail.RowFilter = "YARN_CODE='" + mdtDetailTBL.Rows[i]["YARN_CODE"].ToString() + "' AND PRTY_CODE='" + mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "' AND STORE='" + mdtDetailTBL.Rows[i]["STORE"].ToString() + "'  AND LOCATION='" + mdtDetailTBL.Rows[i]["LOCATION"].ToString() + "' ";
                        DataView dvTRN_SUB = new DataView(dtTRN_SUB);
                        dvTRN_SUB.RowFilter = "YARN_CODE='" + mdtDetailTBL.Rows[i]["YARN_CODE"].ToString() + "' AND PRTY_CODE='" + mdtDetailTBL.Rows[i]["PRTY_CODE"].ToString() + "' AND STORE='" + mdtDetailTBL.Rows[i]["STORE"].ToString() + "'  AND LOCATION='" + mdtDetailTBL.Rows[i]["LOCATION"].ToString() + "' ";
                       resultTrn = SaitexDL.Interface.Method.YRN_MST.InsertYarnReceive(oYRNMST, dvTRN_SUB.ToTable(), dvColorDetail.ToTable());
                    }

                    if (resultTrn)
                    {
                        string Resultmsg = "Yarn Master Updated Successfully" + "\\r\\n" + "Article Code is:" + oYRNMST.YARN_CODE;                       
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + Resultmsg + "');", true);
                        RefreshControls();
                        Initialize();


                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Already Exists');", true);

                    }
                    else 
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Master Updated Successfully!!');", true);
                    }
                   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Yarn Master Updation Failed!!');", true);
                    Common.CommonFuction.ShowMessage(Errormsg);

                }


            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating Yarn Details.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {

            ddlyarncode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = false;
            txtYarnCode.Text = "";
            txtYarnCode.Visible = false;

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));

        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string Query_String = string.Empty;
        try
        {
            string URL = "../../../Yarn/SalesWork/Reports/YarnOpeningBalanceReport.aspx?Query_String =" + Query_String;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);


        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }




    }



    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }  

    protected void ddlyarncode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = GetYarnData(e.Text.ToUpper(), e.ItemsOffset);
        ddlyarncode.Items.Clear();
        ddlyarncode.DataSource = data;
        ddlyarncode.DataBind();
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        e.ItemsCount = GetYarnCount(e.Text);
    }
   
    private DataTable GetYarnData(string Text, int startOffset)
    {
        try
        {
            //string CommandText = "select YARN_CODE,YARN_CAT,YARN_DESC  from YRN_MST Where   (UPPER(YARN_CODE) like :SearchQuery  or  UPPER(YARN_DESC) like :SearchQuery)   AND ROWNUM <= 15   ";

            string CommandText = "SELECT   M.YARN_CODE,  M.YARN_CAT, M.YARN_DESC,A.ASS_YARN_DESC  FROM   YRN_MST M, YRN_ASSOCATED_MST A WHERE  M.YARN_CODE = A.YARN_CODE  AND (   UPPER (M.YARN_CODE) LIKE :SearchQuery  OR UPPER (M.YARN_DESC) LIKE :SearchQuery  OR UPPER (A.ASS_YARN_DESC) LIKE :SearchQuery)  AND ROWNUM <= 15  ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND YARN_CODE NOT IN ( select M.YARN_CODE  from YRN_MST M, YRN_ASSOCATED_MST A  Where   M.YARN_CODE = A.YARN_CODE   AND  (UPPER(YARN_CODE) like :SearchQuery  or  UPPER(YARN_DESC) like :SearchQuery)     AND  ROWNUM <= " + startOffset + " )";
            }
            string SortExpression = " order by YARN_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
        }
        catch
        {
            throw;
        }
    }

    protected int GetYarnCount(string text)
    {

        string CommandText = " select YARN_CODE  from YRN_MST Where    (UPPER(YARN_CODE) like :SearchQuery  or  UPPER(YARN_DESC) like :SearchQuery)  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void ddlyarncode_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {

            ddlyarncode.Visible = true;
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetYarnMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                string text = ddlyarncode.SelectedValue.ToString();
                DataView dv = new DataView(dt);
                dv.RowFilter = "YARN_CODE='" + text + "'";
                if (dv != null && dv.Count > 0)
                {

                    txtYarnCode.Text = dv[0]["YARN_CODE"].ToString();
                    txtYarnDescription.Text = dv[0]["YARN_DESC"].ToString().Trim().ToUpper();
                    DataTable dtTRN_SUB = SaitexBL.Interface.Method.YRN_MST.GetSUBTRN_DataByTRN_NUMB(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, txtYarnCode.Text, "OJB01");
                    ViewState["dtTRN_Sub"] = dtTRN_SUB;
                    MapTrnDataTable();

                    DataTable dtTemp = SaitexBL.Interface.Method.YRN_MST.Select_Yarn_Color_LIST(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, txtYarnCode.Text, "OJB01");
                    if (dtTemp != null && dtTemp.Rows.Count > 0)
                    {
                        MapColorDataTable(dtTemp);
                    }
                    BindColorDetailGrid();

                }


            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Yarn Code Selection.\r\nSee error log for detail."));

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

    private void MapColorDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            if (dtColorDetail == null || dtColorDetail.Rows.Count == 0)
                CreateColorDetailTable();
            dtColorDetail.Rows.Clear();
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtColorDetail.NewRow();
                    dr["UniqueId"] = dtColorDetail.Rows.Count + 1;
                    dr["YARN_CODE"] = drTemp["YARN_CODE"];
                    dr["SHADE_FAMILY"] = drTemp["SHADE_FAMILY"];
                    dr["SHADE"] = drTemp["SHADE"];
                    dr["RGB"] = drTemp["RGB"];
                    dr["STORE"] = drTemp["STORE"];
                    dr["LOCATION"] = drTemp["LOCATION"];
                    dr["OP_BAL_STOCK"] = drTemp["OP_BAL_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["MIN_STOCK"] = drTemp["MIN_STOCK"];
                    dr["MAX_STOCK"] = drTemp["MAX_STOCK"];
                    dr["OLD_STORE"] = drTemp["OLD_STORE"];
                    dr["OLD_LOCATION"] = drTemp["OLD_LOCATION"];

                    dr["LOT_NO"] = drTemp["LOT_NO"];
                    dr["GRADE"] = drTemp["GRADE"];
                    dr["GROSS_WT"] = drTemp["GROSS_WT"];
                    dr["TARE_WT"] = drTemp["TARE_WT"];
                    dr["CARTONS"] = drTemp["CARTONS"];
                    dr["DYED_BATCH"] = drTemp["DYED_BATCH"];
                    dr["TRN_NUMB"] = drTemp["TRN_NUMB"];
                    dr["PRTY_CODE"] = drTemp["PRTY_CODE"];
                    dr["PRTY_NAME"] = drTemp["PRTY_NAME"];
                    dr["NO_OF_PALLET"] = drTemp["NO_OF_PALLET"];
                    dr["ROW_STATE"] = "NO STATE";
                    dr["JOBER"] = drTemp["JOBER"];
                    dr["JOBER_NAME"] = drTemp["JOBER_NAME"];
                    dtColorDetail.Rows.Add(dr);
                }
                dtTemp = null;
                ViewState["dtColorDetail"] = dtColorDetail;
            }
        }
        catch
        {
            throw;
        }
    }

    protected bool CheckValidation(string task)
    {
        bool result = false;

        if (string.IsNullOrEmpty(txtYarnDescription.Text))
        {
            CommonFuction.ShowMessage("Please enter yarn description.");
            result = true;
        }
        return result;

    }

    /************************************* For opening balance color wise *****************************************/

    protected void btnLotDetails_Click(object sender, EventArgs e)
    {
        try
        {
            bool Result = false;
            string Shade_Family = txtShadeFamily.Text.Trim();
            string Shade = txtShade.Text.Trim();

            if (ViewState["UniqueId"] == null)
            {
                foreach (GridViewRow grdRow in grdColorDetail.Rows)
                {
                    Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                    Label lblShadeFamily = (Label)grdRow.FindControl("txtShadeFamily");
                    Label lblShade = (Label)grdRow.FindControl("txtShade");
                    Label lblStore = (Label)grdRow.FindControl("txtstore");
                    Label lblLocation = (Label)grdRow.FindControl("txtlocation");
                    ////if (Shade_Family == lblShadeFamily.Text.Trim() && Shade == lblShade.Text.Trim())
                    ////{
                    ////    Result = true;
                    ////}
                }
            }


            if (!string.IsNullOrEmpty(txtShade.Text) && !string.IsNullOrEmpty(txtPartyCode.SelectedValue) && !string.IsNullOrEmpty(ddlStore.SelectedValue) && !string.IsNullOrEmpty((txtLotNo.SelectedValue).Trim()) && !string.IsNullOrEmpty(txtGrade.SelectedValue))
            {
            txtOpeningBal.ReadOnly = false;
            string URL = "YARN_OP_BAL_LOT_DETAILS.aspx";
            URL = URL + "?YARN_CODE=" + txtYarnCode.Text;
            URL = URL + "&SHADE_FAMILY=" + HttpUtility.UrlEncode(txtShadeFamily.Text.Trim());
            URL = URL + "&SHADE=" + HttpUtility.UrlEncode(txtShade.Text.Trim());
            URL = URL + "&RGB=" + txtRGB.Text.Trim();
            URL = URL + "&OP_BAL=" + txtOpeningBal.Text;
            URL = URL + "&UOM=" + "KG";
            URL = URL + "&STORE=" + ddlStore.SelectedValue;
            URL = URL + "&LOCATION=" + ddlLocation.SelectedValue;
            //URL = URL + "&txtOpBal=" + txtOpeningBal.ClientID;
            URL = URL + "&txtQTY=" + txtOpeningBal.ClientID;
            URL = URL + "&LOT_NO=" + (txtLotNo.SelectedValue).Trim();
            URL = URL + "&GRADE=" + txtGrade.SelectedValue;
            URL = URL + "&GROSS_WT=" + txtGrossWt.ClientID;
            URL = URL + "&TARE_WT=" + txtTareWt.ClientID;
            URL = URL + "&DYED_BATCH=" + txtDyedBatchNo.ClientID;
            URL = URL + "&CARTONS=" + txtCartoons.ClientID;
            URL = URL + "&txtNoOfUnit=" + txtNoOfUnit.ClientID;
            URL = URL + "&txtWeightOfUnit=" + txtWeightOfUnit.ClientID;
            URL = URL + "&PI_NO=NA";
            URL = URL + "&PRTY_CODE=" + txtPartyCode.SelectedText;
            URL = URL + "&txtNoOfPallet=" + txtNoOfPallet.ClientID;
                
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

    private void CreateColorDetailTable()
    {
        dtColorDetail = new DataTable();
        dtColorDetail.Columns.Add("UniqueId", typeof(int));
        dtColorDetail.Columns.Add("YARN_CODE", typeof(string));
        dtColorDetail.Columns.Add("SHADE_FAMILY", typeof(string));
        dtColorDetail.Columns.Add("SHADE", typeof(string));
        dtColorDetail.Columns.Add("RGB", typeof(string));
        dtColorDetail.Columns.Add("OP_BAL_STOCK", typeof(double));
        dtColorDetail.Columns.Add("OP_RATE", typeof(string));
        dtColorDetail.Columns.Add("MIN_STOCK", typeof(double));
        dtColorDetail.Columns.Add("MAX_STOCK", typeof(double));
        dtColorDetail.Columns.Add("LOCATION", typeof(string));
        dtColorDetail.Columns.Add("STORE", typeof(string));
        dtColorDetail.Columns.Add("OLD_LOCATION", typeof(string));
        dtColorDetail.Columns.Add("OLD_STORE", typeof(string));
        dtColorDetail.Columns.Add("ROW_STATE", typeof(string));
        dtColorDetail.Columns.Add("DYED_BATCH", typeof(double));

        dtColorDetail.Columns.Add("LOT_NO", typeof(string));
        dtColorDetail.Columns.Add("GRADE", typeof(string));
        dtColorDetail.Columns.Add("GROSS_WT", typeof(double));
        dtColorDetail.Columns.Add("TARE_WT", typeof(double));
        dtColorDetail.Columns.Add("CARTONS", typeof(double));
        dtColorDetail.Columns.Add("NO_OF_UNIT", typeof(double));
        dtColorDetail.Columns.Add("WEIGHT_OF_UNIT", typeof(double));
        dtColorDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtColorDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtColorDetail.Columns.Add("PRTY_CODE", typeof(string));
        dtColorDetail.Columns.Add("PRTY_NAME", typeof(string));
        dtColorDetail.Columns.Add("NO_OF_PALLET", typeof(string));
        dtColorDetail.Columns.Add("JOBER", typeof(string));
        dtColorDetail.Columns.Add("JOBER_NAME", typeof(string));
    }

    protected void lbtnsavedetailColor_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            if (dtColorDetail == null || dtColorDetail.Rows.Count == 0)

                CreateColorDetailTable();

            if (!string.IsNullOrEmpty(txtPartyName.Value) && !string.IsNullOrEmpty(txtShade.Text.Trim()))
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                bool bb = SearchShadeCodeInGrid(UniqueId);
                if (!bb)
                {
                    double rate = 0;
                    double.TryParse(txtOpenRate.Text.Trim(), out rate);
                    double opBal = 0;
                    double.TryParse(txtOpeningBal.Text.Trim(), out opBal);
                    double minStock = 0;
                    double.TryParse(txtMinStock.Text.Trim(), out minStock);
                    double maxStock = 0;
                    double.TryParse(txtMaxStock.Text.Trim(), out maxStock);

                    double grossWt = 0;
                    double tareWt = 0;
                    double cartons = 0;
                    double DYED_BATCH = 0;
                    double no_of_unit = 0;
                    double wt_of_unit = 0;
                    double.TryParse(txtGrossWt.Value.Trim(), out grossWt);
                    double.TryParse(txtTareWt.Value.Trim(), out tareWt);
                    double.TryParse(txtCartoons.Value.Trim(), out cartons);
                    double.TryParse(txtNoOfUnit.Value, out no_of_unit);
                    double.TryParse(txtDyedBatchNo.Value.Trim(), out DYED_BATCH);
                    double.TryParse(txtWeightOfUnit.Value, out wt_of_unit);

                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtColorDetail);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["YARN_CODE"] = txtYarnCode.Text.Trim();
                            dv[0]["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                            dv[0]["SHADE"] = txtShade.Text.Trim();
                            dv[0]["RGB"] = txtRGB.Text.Trim();
                            dv[0]["OP_BAL_STOCK"] = opBal;
                            dv[0]["OP_RATE"] = rate;
                            dv[0]["MIN_STOCK"] = minStock;
                            dv[0]["MAX_STOCK"] = maxStock;
                            dv[0]["LOCATION"] = ddlLocation.SelectedValue.ToString();
                            dv[0]["STORE"] = ddlStore.SelectedValue.ToString();
                            dv[0]["ROW_STATE"] = "UPDATE";
                            dv[0]["CARTONS"] = cartons;
                            dv[0]["DYED_BATCH"] = DYED_BATCH;
                            dv[0]["TARE_WT"] = tareWt;
                            dv[0]["GROSS_WT"] = grossWt;
                            dv[0]["NO_OF_UNIT"] = no_of_unit;
                            dv[0]["WEIGHT_OF_UNIT"] = wt_of_unit;
                            dv[0]["LOT_NO"] = txtLotNo.SelectedValue;
                            dv[0]["GRADE"] = txtGrade.SelectedValue;
                            dv[0]["PRTY_CODE"] = txtPartyCode.SelectedText;
                            dv[0]["PRTY_NAME"] = txtPartyName.Value;
                            dv[0]["NO_OF_PALLET"] = txtNoOfPallet.Value;
                            dv[0]["JOBER"] = cmbJober.SelectedText.ToString();
                            dv[0]["JOBER_NAME"] = cmbJober.SelectedValue.ToString();
                            dtColorDetail.AcceptChanges();
                        }
                    }
                    else
                    {
                        DataRow dr = dtColorDetail.NewRow();
                        dr["UniqueId"] = dtColorDetail.Rows.Count + 1;
                        dr["YARN_CODE"] = txtYarnCode.Text.Trim();
                        dr["SHADE_FAMILY"] = txtShadeFamily.Text.Trim();
                        dr["SHADE"] = txtShade.Text.Trim();
                        dr["RGB"] = txtRGB.Text.Trim();
                        dr["OP_BAL_STOCK"] = opBal;
                        dr["OP_RATE"] = rate;
                        dr["MIN_STOCK"] = minStock;
                        dr["MAX_STOCK"] = maxStock;
                        dr["LOCATION"] = ddlLocation.SelectedValue.ToString();
                        dr["STORE"] = ddlStore.SelectedValue.ToString();
                        dr["ROW_STATE"] = "INSERT";
                        dr["CARTONS"] = cartons;
                        dr["DYED_BATCH"] = DYED_BATCH;
                        dr["TARE_WT"] = tareWt;
                        dr["GROSS_WT"] = grossWt;
                        dr["NO_OF_UNIT"] = no_of_unit;
                        dr["WEIGHT_OF_UNIT"] = wt_of_unit;
                        dr["LOT_NO"] = txtLotNo.SelectedValue;
                        dr["GRADE"] = txtGrade.SelectedValue;
                        dr["PRTY_CODE"] = txtPartyCode.SelectedText;
                        dr["PRTY_NAME"] = txtPartyName.Value;
                        dr["NO_OF_PALLET"] = txtNoOfPallet.Value;
                        dr["JOBER"] = cmbJober.SelectedText.ToString();
                        dr["JOBER_NAME"] = cmbJober.SelectedValue.ToString();
                        dtColorDetail.Rows.Add(dr);
                    }
                    RefreshDetailRowColor();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Shade Code Should Be Diffrent');", true);
                }
            }
            else if (string.IsNullOrEmpty(txtShade.Text.Trim()))
            {
                CommonFuction.ShowMessage("Party/Color Required.");
            }

            ViewState["dtColorDetail"] = dtColorDetail;
            BindColorDetailGrid();

        }


        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        RefreshDetailRowColor();
    }

    private void BindColorDetailGrid()
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
            {
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
                DataView dv = new DataView(dtColorDetail);
                dv.RowFilter = "ROW_STATE <> 'DELETE'";
                grdColorDetail.DataSource = dv;
                grdColorDetail.DataBind();
            }
            else
            {
                grdColorDetail.DataSource = null;
                grdColorDetail.DataBind();
            }
            CalculateAllData();


        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private bool SearchShadeCodeInGrid(int UniqueId)
    {
        bool Result = false;
        try
        {
            string Shade_Family = txtShadeFamily.Text.Trim();
            string Shade = txtShade.Text.Trim();
            string Store = ddlStore.SelectedValue;
            string PARTY = txtPartyCode.SelectedText;
            string LOT_NO = txtLotNo.SelectedValue;
            string GRADE = txtGrade.SelectedValue;
            foreach (GridViewRow grdRow in grdColorDetail.Rows)
            {
                Button lnkDelete = (Button)grdRow.FindControl("lnkDelete");
                Label lblShadeFamily = (Label)grdRow.FindControl("txtShadeFamily");
                Label lblShade = (Label)grdRow.FindControl("txtShade");
                Label lblStore = (Label)grdRow.FindControl("txtstore");
                Label lblParty = (Label)grdRow.FindControl("txtParty");
                Label lblLotNO = (Label)grdRow.FindControl("txtLotNo");
                Label lblGrade = (Label)grdRow.FindControl("txtGrade");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (UniqueId != iUniqueId && Shade_Family == lblShadeFamily.Text.Trim() && Shade == lblShade.Text.Trim() && Store == lblStore.Text.Trim() && lblParty.ToolTip == PARTY && lblLotNO.Text == LOT_NO && lblGrade.Text == GRADE)
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

    public void RefreshDetailRowColor()
    {
        txtOpenRate.Text = string.Empty;
        txtOpeningBal.Text = string.Empty;
        txtMaxStock.Text = "";
        txtMinStock.Text = "";
        cmbShade.SelectedIndex = -1;
        txtShade.Text = string.Empty;
        txtShadeFamily.Text = string.Empty;
        ViewState["UniqueId"] = null;
        txtRGB.Text = string.Empty;
        txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
        txtCartoons.Value = string.Empty;
        txtDyedBatchNo.Value = string.Empty;
        txtGrossWt.Value = string.Empty;
        txtLotNo.SelectedIndex = -1;
        txtGrade.SelectedIndex = -1;
        txtTareWt.Value = string.Empty;
        txtNoOfUnit.Value = string.Empty;
        txtWeightOfUnit.Value = string.Empty;
        txtPartyCode.SelectedIndex = -1;
        txtPartyName.Value = string.Empty;
        txtNoOfPallet.Value = string.Empty;
    }

    protected void grdColorDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "colorEdit")
            {

                FillDetailByGridColor(UniqueId);
            }
            else if (e.CommandName == "colorDelete")
            {
                DeleteColorDetailRow(UniqueId);
                BindColorDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void grdColorDetail_RowDataBound(object sender, GridViewRowEventArgs e)
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

                Label txtRGB = ((Label)e.Row.FindControl("txtRGB"));
                TextBox txtRGBColor = ((TextBox)e.Row.FindControl("txtRGBColor"));
                if (!string.IsNullOrEmpty(txtRGB.Text))
                {
                    txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
                }


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
                if (dtColorDetail != null && dtColorDetail.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtColorDetail);

                    dv.RowFilter = "UNIQUEID=" + UNIQUE_ID;
                    if (dv.Count > 0)
                    {
                        string YARN_CODE = dv[0]["YARN_CODE"].ToString();
                        string SHADE_FAMILY = dv[0]["SHADE_FAMILY"].ToString();
                        string SHADE = dv[0]["SHADE"].ToString();
                        string STORE = dv[0]["STORE"].ToString();
                        string LOCATION = dv[0]["LOCATION"].ToString();
                        string PRTY_CODE = dv[0]["PRTY_CODE"].ToString();
                        string LOT_NO = dv[0]["LOT_NO"].ToString();
                        string GRADE = dv[0]["GRADE"].ToString();
                        if (Session["dtTRN_SUB"] != null)
                        {
                            DataTable dtTRN_Sub = (DataTable)Session["dtTRN_SUB"];
                            DataView dvYRNDRecieve_trn = new DataView(dtTRN_Sub);
                            dvYRNDRecieve_trn.RowFilter = "YARN_CODE='" + YARN_CODE + "' AND SHADE='" + SHADE + "' AND SHADE_FAMILY='" + SHADE_FAMILY + "' AND STORE='" + STORE + "'  AND LOCATION='" + LOCATION + "' AND PRTY_CODE='" + PRTY_CODE + "'  AND LOT_NO='" + LOT_NO + "' AND GRADE='" + GRADE + "'";
                            if (dvYRNDRecieve_trn.Count > 0)
                            {
                                GridView grdBOM = e.Row.FindControl("grdBOM") as GridView;
                                grdBOM.DataSource = dvYRNDRecieve_trn;
                                grdBOM.DataBind();
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

    private void DeleteColorDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            if (grdColorDetail.Rows.Count == 1)
            {
                //dtColorDetail.Rows.Clear();
                dtColorDetail.Rows[0].SetField("ROW_STATE", "DELETE");
            }
            else
            {
                foreach (DataRow dr in dtColorDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        //dtColorDetail.Rows.Remove(dr);
                        dr.SetField("ROW_STATE", "DELETE");
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtColorDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
            ViewState["dtColorDetail"] = dtColorDetail;
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
            if (ViewState["dtColorDetail"] != null)
                dtColorDetail = (DataTable)ViewState["dtColorDetail"];
            DataView dv = new DataView(dtColorDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                setShadeFamilyCombo(dv[0]["SHADE_FAMILY"].ToString().Trim(), dv[0]["SHADE"].ToString());
                txtShadeFamily.Text = dv[0]["SHADE_FAMILY"].ToString();
                txtShade.Text = dv[0]["SHADE"].ToString();
                txtRGB.Text = dv[0]["RGB"].ToString();
                txtOpenRate.Text = dv[0]["OP_RATE"].ToString();
                txtOpeningBal.Text = dv[0]["OP_BAL_STOCK"].ToString();
                txtMinStock.Text = dv[0]["MIN_STOCK"].ToString();
                txtMaxStock.Text = dv[0]["MAX_STOCK"].ToString();
                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dv[0]["LOCATION"].ToString()));
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dv[0]["STORE"].ToString()));
                ViewState["UniqueId"] = UniqueId;
                txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
                txtCartoons.Value = dv[0]["CARTONS"].ToString();
                txtGrossWt.Value = dv[0]["GROSS_WT"].ToString();
                txtDyedBatchNo.Value = dv[0]["DYED_BATCH"].ToString();
                //txtLotNo.Text = dv[0]["LOT_NO"].ToString();
                //txtGrade.Text = dv[0]["GRADE"].ToString();
                txtTareWt.Value = dv[0]["TARE_WT"].ToString();
                txtNoOfUnit.Value = dv[0]["NO_OF_UNIT"].ToString();
                txtWeightOfUnit.Value = dv[0]["WEIGHT_OF_UNIT"].ToString();
                txtPartyName.Value = dv[0]["PRTY_NAME"].ToString();
                txtNoOfPallet.Value=dv[0]["NO_OF_PALLET"].ToString();

                string CommandText = "SELECT * FROM (SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GREY_LOT_NO'))     AND OTHER_INFO LIKE '%" + txtYarnCode.Text + "%'      AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  LOT_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE   FINISHED_DENIER like '%" + ddlyarncode.SelectedValue + "%'               AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery)   )  ";
                string SortExpression = " order by MST_CODE asc";
                DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, "", SortExpression, "", "%", "");
                txtLotNo.DataSource = data;
                txtLotNo.DataTextField = "MST_CODE";
                txtLotNo.DataValueField = "MST_CODE";
                txtLotNo.DataBind();
                foreach (ComboBoxItem item in txtLotNo.Items)
                {
                    if (item.Text == dv[0]["LOT_NO"].ToString())
                    {
                        txtLotNo.SelectedIndex = txtLotNo.Items.IndexOf(item);
                        break;
                    }
                }

                string CommandText2 = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('GRADE'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  ";
                string SortExpression2 = " order by MST_CODE asc";
                DataTable data2 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText2, "", SortExpression2, "", "%", "");
                txtGrade.DataSource = data2;
                txtGrade.DataTextField = "MST_CODE";
                txtGrade.DataValueField = "MST_CODE";
                txtGrade.DataBind();
                foreach (ComboBoxItem item in txtGrade.Items)
                {
                    if (item.Text == dv[0]["GRADE"].ToString())
                    {
                        txtGrade.SelectedIndex = txtGrade.Items.IndexOf(item);
                        break;
                    }
                }





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





                string CommandText4 = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE)  IN       (UPPER ('YARN SUPPLIER'), UPPER ('JOBER')) ";
                string WhereClause4 = "  and  (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
                string SortExpression4 = " order by PRTY_CODE asc";
                string SearchQuery4 = "%";
                DataTable data4 = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText4, WhereClause4, SortExpression4, "", SearchQuery4, "");
                cmbJober.DataSource = data4;
                cmbJober.DataTextField = "PRTY_CODE";
                cmbJober.DataValueField = "Address";
                cmbJober.DataBind();
                foreach (ComboBoxItem item in cmbJober.Items)
                {
                    if (item.Text == dv[0]["JOBER"].ToString())
                    {
                        cmbJober.SelectedIndex = cmbJober.Items.IndexOf(item);
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

    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);

            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void cmbShade_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbShade.SelectedIndex > -1)
            {
                txtShadeFamily.Text = cmbShade.SelectedText;
                string[] arrName = cmbShade.SelectedValue.Split('@'); 
                txtShade.Text = arrName[1].ToString();//cmbShade.SelectedValue;
                txtRGB.Text = arrName[2].ToString();
                txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select Shade");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT   M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME,   T.SHADE_CODE,        T.SHADE_NAME,   (   M.SHADE_FAMILY_NAME     || '@'|| T.SHADE_NAME  || '@'|| T.RGB)    AS Combined FROM   OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T      WHERE   M.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'         AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE        AND    M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE          AND    (M.SHADE_FAMILY_CODE LIKE :SearchQuery         OR M.SHADE_FAMILY_NAME LIKE :SearchQuery         OR T.SHADE_CODE LIKE :SearchQuery              OR T.SHADE_NAME LIKE :SearchQuery)   AND ROWNUM <= 15";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                //whereClause += " AND Combined NOT IN (SELECT    (   M.SHADE_FAMILY_NAME     || '@'|| T.SHADE_NAME  || '@'|| T.RGB)    AS Combined FROM   OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T      WHERE   M.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'         AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE        AND    M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE          AND    (M.SHADE_FAMILY_CODE LIKE :SearchQuery         OR M.SHADE_FAMILY_NAME LIKE :SearchQuery         OR T.SHADE_CODE LIKE :SearchQuery              OR T.SHADE_NAME LIKE :SearchQuery)   AND ROWNUM <= " + startOffset + ") ";


                whereClause += " AND T.SHADE_CODE NOT IN (SELECT  T.SHADE_CODE   FROM   OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T      WHERE   M.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'         AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE        AND    M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE          AND    (M.SHADE_FAMILY_CODE LIKE :SearchQuery         OR M.SHADE_FAMILY_NAME LIKE :SearchQuery         OR T.SHADE_CODE LIKE :SearchQuery              OR T.SHADE_NAME LIKE :SearchQuery)   AND ROWNUM <= " + startOffset + ") ";



            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");
            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetShadeItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT   M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME,   T.SHADE_CODE,        T.SHADE_NAME,   (   M.SHADE_FAMILY_NAME     || '@'|| T.SHADE_NAME  || '@'|| T.RGB)    AS Combined FROM   OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T      WHERE   M.COMP_CODE ='" + oUserLoginDetail.COMP_CODE + "'         AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE        AND    M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE          AND    (M.SHADE_FAMILY_CODE LIKE :SearchQuery         OR M.SHADE_FAMILY_NAME LIKE :SearchQuery         OR T.SHADE_CODE LIKE :SearchQuery              OR T.SHADE_NAME LIKE :SearchQuery)  ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void txtRGB_TextChanged(object sender, EventArgs e)
    {
        txtRGBColor.BackColor = getRGBColor(txtRGB.Text);
        ddlLocation.Focus();
    }

    public Color getRGBColor(string argb)
    {
        int r = 0;
        int g = 0;
        int b = 0;
        Color RGB = Color.White;
        try
        {

            if (!string.IsNullOrEmpty(argb))
            {
                string[] argbstring = argb.Split(',');
                if (argbstring.Length > 2)
                {
                    int.TryParse(argbstring[0].ToString(), out r);
                    int.TryParse(argbstring[1].ToString(), out g);
                    int.TryParse(argbstring[2].ToString(), out b);

                    if (r > 255 || g > 255 || b > 255 || r < 0 || g < 0 || b < 0)
                    {
                        Common.CommonFuction.ShowMessage("R G B values are being less then 0 or greater then 255.");
                    }
                    else
                    {
                        RGB = Color.FromArgb(r, g, b);
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("make space between R G B values.");
                    RGB = Color.FromArgb(255, 255, 255);
                }

            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem is getting Shade color.\r\nSee error log for detail."));
        }
        return RGB;

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
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    public void setShadeFamilyCombo(string shade_family, string shade)
    {

        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV("SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery)", "", "", "", "%", "");
        cmbShade.DataSource = data;
        cmbShade.DataTextField = "SHADE_FAMILY_NAME";
        cmbShade.DataValueField = "SHADE_NAME";
        cmbShade.DataBind();
        foreach (ComboBoxItem dl in cmbShade.Items)
        {
            if (dl.Text == shade_family && dl.Value == shade)
            {
                cmbShade.SelectedIndex = cmbShade.Items.IndexOf(dl);
                break;
            }
        }
    }

    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Queries/YarnlotwiseQuery.aspx", false);
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
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME ) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker')) AND PRTY_CODE not in('SELF')   AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('Transporter'), UPPER ('Spinner'), UPPER ('Broker')) AND PRTY_CODE not in('SELF')   AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
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






    protected void txtJoberCode_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetJoberData(e.Text.ToUpper(), e.ItemsOffset);
            cmbJober.Items.Clear();
            cmbJober.DataSource = data;
            cmbJober.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetJoberCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetJoberData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT   PRTY_CODE,PRTY_NAME,  PRTY_GRP_CODE, VENDOR_CAT_CODE,(PRTY_NAME ) Address   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE)  IN       (UPPER ('JOBER'), UPPER ('YARN SUPPLIER'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND ROWNUM <= 15   ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN ( SELECT   PRTY_CODE  FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('JOBER'), UPPER ('YARN SUPPLIER'))   AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery)    AND  ROWNUM <= " + startOffset + " )";
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

    protected int GetJoberCount(string text)
    {

        string CommandText = " SELECT   PRTY_CODE   FROM   TX_VENDOR_MST WHERE   NVL (CONF_FLAG, 0) = 1 AND PRTY_GRP_CODE IN ('47','48','18')  AND UPPER (VENDOR_CAT_CODE) NOT IN       (UPPER ('JOBER'), UPPER ('YARN SUPPLIER'))    AND (UPPER (RTRIM (LTRIM (PRTY_CODE))) LIKE :SearchQuery     OR UPPER (RTRIM (LTRIM (PRTY_NAME))) LIKE   :SearchQuery) ";
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

    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLotData(e.Text.ToUpper(), e.ItemsOffset, "GREY_LOT_NO");
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLotCount(e.Text, "GREY_LOT_NO");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void txtGrade_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetMOMData(e.Text.ToUpper(), e.ItemsOffset, "GRADE");
            txtGrade.Items.Clear();
            txtGrade.DataSource = data;
            txtGrade.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetMOMCount(e.Text, "GRADE");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grade selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetMOMData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )  AND ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT   MST_CODE,MST_DESC  FROM   TX_MASTER_TRN WHERE       del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND ( UPPER (MST_CODE) LIKE  :SearchQuery OR UPPER(MST_DESC) LIKE :SearchQuery )     AND  ROWNUM <= " + startOffset + " )";
                }
            string SortExpression = " order by MST_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetMOMCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))         AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)     ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }
    private DataTable GetLotData(string Text, int startOffset, string MST_NAME)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))   AND OTHER_INFO LIKE '%" + txtYarnCode.Text + "%'      AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1 AND  FINISHED_DENIER like  '%" + ddlyarncode.SelectedValue + "%'               AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery)   )   WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND MST_CODE NOT IN ( SELECT * FROM (SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0'         AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))      AND OTHER_INFO LIKE '%" + txtYarnCode.Text + "%'   AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1 AND  FINISHED_DENIER like '%" + ddlyarncode.SelectedValue + "%'      AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery )   ) WHERE  ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by MST_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetLotCount(string text, string MST_NAME)
    {

        string CommandText = " SELECT   MST_CODE, MST_DESC  FROM   TX_MASTER_TRN WHERE   del_status = '0' AND OTHER_INFO LIKE '%" + txtYarnCode.Text + "%'        AND TRIM (RTRIM (MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))   AND OTHER_INFO LIKE '%" + txtYarnCode.Text + "%'      AND (UPPER (MST_CODE) LIKE :SearchQuery              OR UPPER (MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1 AND   FINISHED_DENIER like '%" + ddlyarncode.SelectedValue + "%'               AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery)    ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void CalculateAllData()
    {
        if (grdColorDetail.Rows.Count > 0)
        {
            double totalOpeningStock = 0;
            double totalDyedBatchNo = 0;
            double totalGrossWt = 0;
            double totalTareWt = 0;
            double totalCortoons = 0;

            for (int i = 0; i < grdColorDetail.Rows.Count; i++)
            {
                double TareWt = 0;
                double OpeningStock = 0;
                double GrossWt = 0;
                double Cortoons = 0;
                    double DyedBatchNo = 0;


                Label txtOpeningStock = grdColorDetail.Rows[i].FindControl("txtOpeningStock") as Label;
                Label txtDyedBatchNo = grdColorDetail.Rows[i].FindControl("txtDyedBatchNo") as Label;
                Label txtCortoons = grdColorDetail.Rows[i].FindControl("txtCortoons") as Label;
                Label txtGrossWt = grdColorDetail.Rows[i].FindControl("txtGrossWt") as Label;
                Label txtTareWt = grdColorDetail.Rows[i].FindControl("txtTareWt") as Label;

                double.TryParse(txtOpeningStock.Text, out OpeningStock);
                double.TryParse(txtDyedBatchNo.Text, out DyedBatchNo);
                double.TryParse(txtCortoons.Text, out Cortoons);
                double.TryParse(txtGrossWt.Text, out GrossWt);
                double.TryParse(txtTareWt.Text, out TareWt);
                totalOpeningStock = totalOpeningStock + OpeningStock;
                totalCortoons = totalCortoons + Cortoons;
                totalGrossWt = totalGrossWt + GrossWt;
                totalTareWt = totalTareWt + TareWt;
            }
            ((Label)grdColorDetail.FooterRow.FindControl("lblTotalOpeningStock")).Text = totalOpeningStock.ToString();
            ((Label)grdColorDetail.FooterRow.FindControl("lblDyedBatchNo")).Text = totalDyedBatchNo.ToString();
            ((Label)grdColorDetail.FooterRow.FindControl("lblTotalCortoons")).Text = totalCortoons.ToString();
            ((Label)grdColorDetail.FooterRow.FindControl("lblTotalGrossWt")).Text = totalGrossWt.ToString();
            ((Label)grdColorDetail.FooterRow.FindControl("lblTotalTareWt")).Text = totalTareWt.ToString();


        }
    }


}

