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

public partial class Module_Fiber_Controls_FiberIndentBom : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private string IND_TYPE = string.Empty;
    private DataTable dtIndentDetail = null;
    private double FinalTotal = 0;
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }
    public void InitialisePage()
    {
        try
        {
            IND_TYPE = "BOM";
            Session["IND_TYPE"] = IND_TYPE;
            ActivateSaveMode();
            lblMode.Text = "Save";
            FinalTotal = 0;
            ViewState["FinalTotal"] = FinalTotal;
            txtComments.Text = "";
            txtDepartment.Text = "";
            txtIndentDate.Text = "";
            txtIndentNumber.Text = "";
            txtPreparedBy.Text = "";
            txtRequiredBefore.Text = "";
            if (Session["IND_TYPE"] != null)
            {
                IND_TYPE = (string)Session["IND_TYPE"];
            }
            txtIndentNumber.Text = SaitexBL.Interface.Method.FIBER_IND_MST.GetNewIndentNumber1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE);
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
             CreateIndentDetailTable();

            dtIndentDetail.Rows.Clear();
            BindInitialData();
            BindIndentDetailGrid();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            txtIndentNumber.ReadOnly = true;
            ddlIndentNumber.Visible = false;
            txtIndentNumber.Visible = true;
            Session["dtBOMIndent"] = null;
            
        
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindIndentDetailGrid()
    {
        try
        {
            double FinalTotal1 = 0.0;
            if(ViewState["dtIndentDetail"]!=null)
            {
                grdIndentDetail.DataSource = dtIndentDetail;
                grdIndentDetail.DataBind();
            }
            foreach (GridViewRow Row in grdIndentDetail.Rows)
            {
                Label lblAmount = (Label)Row.FindControl("lblAmount");
                FinalTotal1 += double.Parse(lblAmount.Text);
            }
            if (grdIndentDetail.Rows.Count > 0)
            {
                Label lblFooterAmount = (Label)grdIndentDetail.FooterRow.FindControl("lblFooterAmount");
                lblFooterAmount.Text = FinalTotal1.ToString();
            }
            getReqDate();
            AmountToWords.RupeesToWord oRupeesToWord = new AmountToWords.RupeesToWord();
            lblAmountInWords.Text = oRupeesToWord.changeNumericToWords(FinalTotal1);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindInitialData()
    {
        try
        {
            txtIndentDate.Text = DateTime.Now.Date.ToShortDateString();
            txtRequiredBefore.Text = DateTime.Now.Date.ToShortDateString();
            txtDepartment.Text = oUserLoginDetail.VC_DEPARTMENTCODE;
            txtPreparedBy.Text = oUserLoginDetail.Username;

        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void ActivateSaveMode()
    {
        try
        {
            ddlIndentNumber.SelectedIndex = -1;
            ddlIndentNumber.Visible = false;
            txtIndentNumber.Visible = true;
            ddlIndentNumber.Items.Clear();

            

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void CreateIndentDetailTable()
    {
        try
        {
            dtIndentDetail = new DataTable();
            dtIndentDetail.Columns.Add("UniqueId", typeof(int));
            dtIndentDetail.Columns.Add("IndentDetailNumber", typeof(int));
            dtIndentDetail.Columns.Add("IndentNumber", typeof(string));
            dtIndentDetail.Columns.Add("FIBER_CODE", typeof(string));
            dtIndentDetail.Columns.Add("REQ_DATE",typeof(DateTime));
            dtIndentDetail.Columns.Add("FIBER_DESC",typeof(string));
            dtIndentDetail.Columns.Add("PI_NO", typeof(string));
            dtIndentDetail.Columns.Add("currentStock", typeof(double));
            dtIndentDetail.Columns.Add("MIN_STOCK_LVL", typeof(double));
            dtIndentDetail.Columns.Add("Min_Procure_days", typeof(double));
            dtIndentDetail.Columns.Add("OP_RATE", typeof(double));
            dtIndentDetail.Columns.Add("RQST_QTY",typeof(double));
            dtIndentDetail.Columns.Add("VC_UNITNAME", typeof(string));
            dtIndentDetail.Columns.Add("Amount", typeof(double));
            dtIndentDetail.Columns.Add("DPT_REMARK", typeof(string));
            ViewState["dtIndentDetail"] = dtIndentDetail;


        }
        catch (Exception exe)
        {
            throw exe;
        }
    }
    private void getReqDate()
    {
        try
        {
            if(ViewState["dtIndentDetail"]!=null)
            {
         dtIndentDetail=(DataTable)ViewState["dtIndentDetail"];
            }
            if (dtIndentDetail != null || dtIndentDetail.Rows.Count > 0)
            {
                DateTime Temp = System.DateTime.Now;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    double Min_Procure_Days = 0;
                    double.TryParse(dr["Min_Procure_days"].ToString(), out Min_Procure_Days);
                    DateTime IndentDate = DateTime.Parse(txtIndentDate.Text.Trim());
                    DateTime NewDate = IndentDate.AddDays(Min_Procure_Days);
                    int Val = Temp.CompareTo(NewDate);
                    if (Val == -1)
                    {
                        Temp = NewDate;
                    }


                }
                txtRequiredBefore.Text = Temp.ToShortDateString();

            }
            else
            {
                txtRequiredBefore.Text = DateTime.Now.ToShortDateString();
            }

           

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnCancel_Click1(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in clearing item detail row.\r\nSee error log for detail."));
        }
    }
    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = ViewState["dtIndentDetail"] as DataTable;
            }
            if (dtIndentDetail.Rows.Count < 15)
            {
                if (txtFiberCode.Text != "" || txtOpeningRate.Text != "" || txtRequestQty.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    bool bb = SearchItemCodeInGrid(txtFiberCode.Text, UniqueId);
                    if (!bb)
                    {
                        double Qty = 0;
                        double.TryParse(txtRequestQty.Text, out Qty);
                        if (Qty > 0)
                        {
                            if (UniqueId > 0)
                            {
                                DataView dv = new DataView(dtIndentDetail);
                                dv.RowFilter = "UniqueId=" + UniqueId;
                                if (dv.Count > 0)
                                {
                                    dv[0]["IndentNumber"] = txtIndentNumber.Text;
                                    dv[0]["FIBER_CODE"] = txtFiberCode.Text;
                                    dv[0]["FIBER_DESC"] = txtFiberDesc.Text;
                                    dv[0]["PI_NO"] = lblPINO.Text;
                                    dv[0]["currentStock"] = double.Parse(txtCurrent.Text.Trim());
                                    dv[0]["MIN_STOCK_LVL"] = double.Parse(txtMinStock.Text.Trim());
                                    dv[0]["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                    dv[0]["REQ_DATE"] = txtReqDate.Text;
                                    dv[0]["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["VC_UNITNAME"] = txtUnitName.Text;
                                    dv[0]["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                    dv[0]["DPT_REMARK"] = txtRemark.Text.Trim();
                                    FinalTotal = FinalTotal + double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                    dtIndentDetail.AcceptChanges();
                                }

                            }
                            else
                            {
                                DataRow dr = dtIndentDetail.NewRow();
                                dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                                dr["IndentNumber"] = txtIndentNumber.Text.Trim();
                                dr["IndentDetailNumber"] = 0;
                                dr["FIBER_CODE"] = txtFiberCode.Text;
                                dr["FIBER_DESC"] = txtFiberDesc.Text;
                                dr["PI_NO"] = lblPINO.Text;
                                dr["currentStock"] = double.Parse(txtCurrent.Text.Trim());
                                dr["MIN_STOCK_LVL"] = double.Parse(txtMinStock.Text.Trim());
                                dr["OP_RATE"] = double.Parse(txtOpeningRate.Text.Trim());
                                dr["REQ_DATE"] = txtReqDate.Text;
                                dr["RQST_QTY"] = double.Parse(txtRequestQty.Text.Trim());
                                dr["Min_Procure_days"] = double.Parse(lblMin_Procure_days.Text.Trim());
                                dr["VC_UNITNAME"] = txtUnitName.Text.Trim();
                                dr["Amount"] = double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dr["DPT_REMARK"] = txtRemark.Text.Trim();
                                FinalTotal = FinalTotal + double.Parse(txtOpeningRate.Text.Trim()) * double.Parse(txtRequestQty.Text.Trim());
                                dtIndentDetail.Rows.Add(dr);
                                lblMin_Procure_days.Text = "";

                            }
                            ViewState["FinalTotal"] = FinalTotal;
                            RefreshDetailRow();
                        }
                        else
                        {

                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Quantity can not be zero');", true);

                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('enter valid item code');", true);
                    }
                }
                else if (txtFiberCode.Text.ToString() == "")
                {
                    CommonFuction.ShowMessage("Fiber Code Required");
                }
                else if (txtRequestQty.Text == "")
                {
                    CommonFuction.ShowMessage("Quantity can not be zero");
                }
                else if (txtReqDate.Text == "")
                {
                    CommonFuction.ShowMessage("Select required date");
                }
                ViewState["dtIndentDetail"] = dtIndentDetail;
                BindIndentDetailGrid();


            }
            else
            {
                CommonFuction.ShowMessage("You have reached the limit of items. Only 15 items allowed in one Indent.");
            }
            getReqDate();
        }
        catch (Exception ex)
        {     
            if (txtReqDate.Text == "")
                {
                    CommonFuction.ShowMessage("Select required date");
                }
          
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving item detail row.\r\nSee error log for detail."));
        }

    }
    protected void btnAdjBom_CLick(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = ViewState["IND_TYPE"].ToString();
            }
            if (txtFiberCode.Text.ToString() != "")
            {
                string URL = "";
                URL = "/Saitex/Module/Fiber/Pages/Adj_INDENT_BOM.aspx";
                URL = URL + "?ItemCodeId=" + txtFiberCode.Text.Trim();
                URL = URL + "&TextBoxId=" + txtRequestQty.ClientID;
                URL = URL + "&IND_NUMB=" + txtIndentNumber.Text.Trim();
                IND_TYPE = "BOM";
                URL = URL + "&IND_TYPE=" + IND_TYPE;
                URL = URL + "&PI_NO=" + lblPINO.Text;
                txtRequestQty.ReadOnly = false;
                txtRequestQty.Focus();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=no,menubar=no,width=900,height=450,left=200,top=300');", true);

            }
            else
            {
                CommonFuction.ShowMessage("Please select Fiber to adjust Indent");
            }
          

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting adjustment.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }

            if (Page.IsValid)
            {
                if (dtIndentDetail.Rows.Count > 0)
                {
                    SaveIndentData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving page.\r\nSee error log for detail."));
        }
    }
    protected void SaveIndentData()
    {
        try
        {
            if (Session["IND_TYPE"] != null)
            {
                IND_TYPE = (string)Session["IND_TYPE"];
            }
            SaitexDM.Common.DataModel.FIBER_IND_MST oFIBER_IND_MST = new SaitexDM.Common.DataModel.FIBER_IND_MST();
            oFIBER_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFIBER_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFIBER_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFIBER_IND_MST.IND_TYPE = IND_TYPE;
            oFIBER_IND_MST.IND_NUMB=int.Parse(CommonFuction.funFixQuotes(txtIndentNumber.Text.Trim()));
            oFIBER_IND_MST.IND_DATE=DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oFIBER_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oFIBER_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oFIBER_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtComments.Text);
            oFIBER_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oFIBER_IND_MST.STATUS = true;
            oFIBER_IND_MST.TUSER = oUserLoginDetail.UserCode;
            int IND_NUMB = 0;
            DataTable dtBOMIndent = (DataTable)Session["dtBOMIndent"];
            bool result = SaitexBL.Interface.Method.FIBER_IND_MST.Insert(oFIBER_IND_MST, dtIndentDetail, out IND_NUMB, dtBOMIndent);
            if (result)
            {
                InitialisePage();
                string msg = "Indent Number " + IND_NUMB + " Successfully Saved.";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('" + msg + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent saving Failed');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
       
            try
            {
                if (ViewState["dtIndentDetail"] != null)
                {
                    dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
                }

                if (dtIndentDetail.Rows.Count > 0)
                {
                    UpdateIndentData();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ds", "window.alert('No Items selected. Please enter item detail');", true);
                }
            }
            catch (Exception ex)
            {
                CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating page.\r\nSee error log for detail."));
            }
       
    }
    protected void UpdateIndentData()
    {
        try
        {
            if (Session["IND_TYPE"] != null)
            {
                IND_TYPE = Session["IND_TYPE"].ToString();
            }
            SaitexDM.Common.DataModel.FIBER_IND_MST oFIBER_IND_MST = new SaitexDM.Common.DataModel.FIBER_IND_MST();
            oFIBER_IND_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oFIBER_IND_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oFIBER_IND_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFIBER_IND_MST.IND_TYPE = IND_TYPE;
            oFIBER_IND_MST.IND_NUMB = int.Parse(CommonFuction.funFixQuotes(ddlIndentNumber.SelectedValue));
            oFIBER_IND_MST.IND_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtIndentDate.Text.Trim()));
            oFIBER_IND_MST.DEPT_CODE = oUserLoginDetail.VC_DEPARTMENTCODE;
            oFIBER_IND_MST.PREP_BY = oUserLoginDetail.UserCode;
            oFIBER_IND_MST.CONF_COMMENT = CommonFuction.funFixQuotes(txtComments.Text.Trim());

            oFIBER_IND_MST.REQD_DATE = DateTime.Parse(CommonFuction.funFixQuotes(txtRequiredBefore.Text.Trim()));
            oFIBER_IND_MST.STATUS = true;
            oFIBER_IND_MST.TUSER = oUserLoginDetail.UserCode;

            DataTable dtBOMIndent = (DataTable)Session["dtBOMIndent"];
            bool Result = SaitexBL.Interface.Method.FIBER_IND_MST.Update(oFIBER_IND_MST, dtIndentDetail, dtBOMIndent);
            if (Result)
            {
                InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updated successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "alertmsg", "window.alert('Indent updation Failed');", true);
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            imgbtnSave.Visible = false;
            //imgbtnUpdate.Visible = true;
            imgbtnUpdate.Visible = true;
            ddlIndentNumber.Visible = true;
            txtIndentNumber.Visible = true;
            RefreshDetailRow();
            ActivateUpdateMode();
        }
        catch (Exception exe)
        {
            throw exe;
        }

    }
    protected void txtIndentNumber_TextChanged(object sender, EventArgs e)
    {
 
    }
    protected void ActivateUpdateMode()
    {
        try
        {
            imgbtnUpdate.Visible = true;
            ddlIndentNumber.Enabled = true;
            ddlIndentNumber.SelectedIndex = -1;
            ddlIndentNumber.Items.Clear();
            txtIndentNumber.Visible = false;
            BindIndentNumber();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void BindIndentNumber()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = GetIndents();
            ddlIndentNumber.DataSource = dt;
            ddlIndentNumber.DataTextField = "IND_NUMB";
            ddlIndentNumber.DataValueField = "IND_NUMB";
            ddlIndentNumber.DataBind();
            ddlIndentNumber.Items.Insert(0, new ListItem("---------SELECT--------", "0"));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected DataTable GetIndents()
    {
        try
        {
            string text = string.Empty;
            if (Session["IND_TYPE"] != null)
            {
                IND_TYPE = Session["IND_TYPE"].ToString();
            }
            string commandText = "select * from (Select a.IND_NUMB, a.IND_DATE from TX_FIBER_IND_MST a Where A.COMP_CODE=:COMP_CODE AND A.BRANCH_CODE=:BRANCH_CODE AND A.IND_TYPE=:IND_TYPE and a.dept_code='" + oUserLoginDetail.VC_DEPARTMENTCODE + "' ) asd";
            string whereClause = " where Ind_numb like : searchQuery";
            string sortExpression = " order by ind_numb desc, ind_date desc";
            DataTable dt = SaitexBL.Interface.Method.YRN_INT_MST.GetDataForLOV(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, commandText, whereClause, sortExpression, "", text + "%");

            return dt;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void ddlFiberCode_LoadingItems(object sender,Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable dt = GetFiberData(e.Text.ToUpper(), e.ItemsOffset);
            ddlFiberCode.Items.Clear();
            ddlFiberCode.DataSource = dt;
            ddlFiberCode.DataTextField = "BASE_ARTICAL_CODE";
            ddlFiberCode.DataValueField = "FIBER_DATA";// "BASE_SHADE_CODE";
            ddlFiberCode.DataBind();
            e.ItemsLoadedCount=e.ItemsOffset+dt.Rows.Count;
            e.ItemsCount = GetFiberCount(e.Text.ToUpper());

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected int GetFiberCount(string text)
    {
        string CommandText = " SELECT * FROM (SELECT * FROM (SELECT T.PRTY_CODE, T.PI_NO, T.BASE_ARTICAL_CODE, T.BASE_ARTICAL_DESC, T.BASE_SHADE_CODE, (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) AS rem_qty, ( T.PRTY_CODE|| '@'|| T.PI_NO|| '@'|| T.BASE_ARTICAL_CODE|| '@'|| T.BASE_ARTICAL_DESC|| '@'|| T.BASE_SHADE_CODE) AS FIBER_DATA FROM V_OD_CAPT_TRN_BOM T,TX_FIBER_MASTER F WHERE F.FIBER_CODE=T.BASE_ARTICAL_CODE AND T.comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND T.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) > 0 AND T.BOM_FLAG = '1' AND T.FINAL_ORDER_CONF_CLAG = '1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR BASE_ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery ORDER BY BASE_ARTICAL_CODE)  ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }
    private DataTable GetFiberData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT T.PRTY_CODE, T.PI_NO, T.BASE_ARTICAL_CODE, T.BASE_ARTICAL_DESC, T.BASE_SHADE_CODE, (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) AS rem_qty, ( t.PRTY_CODE|| '@'|| T.PI_NO|| '@'|| T.BASE_ARTICAL_CODE|| '@'|| T.BASE_ARTICAL_DESC|| '@'|| T.BASE_SHADE_CODE) AS FIBER_DATA FROM V_OD_CAPT_TRN_BOM T,TX_FIBER_MASTER F WHERE F.FIBER_CODE=T.BASE_ARTICAL_CODE AND T.comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND T.branch_code = '" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) > 0 AND T.BOM_FLAG = '1' AND T.FINAL_ORDER_CONF_CLAG = '1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR BASE_ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery ORDER BY BASE_ARTICAL_CODE) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND FIBER_DATA NOT IN(SELECT FIBER_DATA FROM (SELECT * FROM (SELECT T.PRTY_CODE,T.PI_NO,T.BASE_ARTICAL_CODE,T.BASE_ARTICAL_DESC,T.BASE_SHADE_CODE,(NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) AS rem_qty,( T.PRTY_CODE || '@' || T.PI_NO || '@' || T.BASE_ARTICAL_CODE || '@' || T.BASE_ARTICAL_DESC || '@' || T.BASE_SHADE_CODE) AS FIBER_DATA FROM V_OD_CAPT_TRN_BOM T,TX_FIBER_MASTER F WHERE F.FIBER_CODE=T.BASE_ARTICAL_CODE AND T.comp_code = '" + oUserLoginDetail.COMP_CODE + "' AND T.branch_code ='" + oUserLoginDetail.CH_BRANCHCODE + "' AND (NVL (T.REQ_QTY, 0) - NVL (T.adj_qty, 0)) > 0 AND T.BOM_FLAG = '1' AND T.FINAL_ORDER_CONF_CLAG ='1') WHERE PRTY_CODE LIKE :SearchQuery OR PI_NO LIKE :SearchQuery OR BASE_ARTICAL_CODE LIKE :SearchQuery OR BASE_ARTICAL_DESC LIKE :SearchQuery OR BASE_SHADE_CODE LIKE :SearchQuery ORDER BY BASE_ARTICAL_CODE) WHERE ROWNUM <= '" + startOffset + "')";
            }

            string SortExpression = " order by BASE_ARTICAL_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected void RefreshDetailRow()
    {
        try
        {
            ddlFiberCode.SelectedIndex = -1;
            txtFiberCode.Text = "";
            txtFiberDesc.Text = "";
            txtMinStock.Text = "";
            txtOpeningRate.Text = "";
            //txtShadeCode.Text = "";
            txtRemark.Text = "";
            txtCurrent.Text = "";
            txtAmount.Text = "";
            txtUnitName.Text = "";
            txtRequestQty.Text = "";
            txtReqDate.Text = "";
            ddlFiberCode.Enabled = false;
            ViewState["UniqueId"] = null;
        }
        catch (Exception ex)
        {
            throw ex;
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
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page refresh.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }
    protected void ddlIndentNumber_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            if (ViewState["FinalTotal"] != null)
            {
                FinalTotal = (double)ViewState["FinalTotal"];
            }
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
            {
                CreateIndentDetailTable();
            }
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;
            int iRecordFound = GetdataByIndentNumber(CommonFuction.funFixQuotes(ddlIndentNumber.SelectedValue));
            BindIndentDetailGrid();
            if (iRecordFound == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('Invalid Indent Number');", true);
                //InitialisePage();
                txtIndentNumber.Focus();
                txtIndentNumber.Text = "";

            }

        }
        catch (Exception ex)
        {
 
        }
    }
    private int GetdataByIndentNumber(string IndentNumber)
    {
        int iRecordFound = 0;
        try
        {
            if (Session["IND_TYPE"] != null)
            {
                IND_TYPE = Session["IND_TYPE"].ToString();
            }
            DataTable dt = SaitexDL.Interface.Method.FIBER_IND_MST.SelectByIndentNumber(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(IndentNumber), oUserLoginDetail.VC_DEPARTMENTCODE);
            if (dt != null || dt.Rows.Count > 0)
            {
                iRecordFound = 1;
                txtComments.Text = dt.Rows[0]["CONF_COMMENT"].ToString().Trim();
                txtIndentDate.Text = dt.Rows[0]["IND_DATE"].ToString().Trim();
                txtPreparedBy.Text = dt.Rows[0]["PREP_BY"].ToString().Trim();
                txtRequiredBefore.Text = DateTime.Parse(dt.Rows[0]["REQD_DATE"].ToString().Trim()).ToShortDateString();
                txtDepartment.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
                txtIndentNumber.Text = IndentNumber;
            }
            if (iRecordFound == 1)
            {
                DataTable dtTemp = GetItemTransaction(IndentNumber.Trim());
                if (dtTemp != null || dtTemp.Rows.Count > 0)
                {
                    MapDataTable(dtTemp);
                    DataTable dtBOMIndent = SaitexBL.Interface.Method.FIBER_IND_MST.GetAdjBOMByIND(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, IND_TYPE, int.Parse(IndentNumber));

                    Session["dtBOMIndent"] = dtBOMIndent;
                }
                else
                {
                    string msg = "Dear " + oUserLoginDetail.Username + " !! Indent already approved. Modification not allowed.";
                    Common.CommonFuction.ShowMessage(msg);

                    InitialisePage();

                    txtIndentNumber.ReadOnly = false;
                    txtIndentNumber.AutoPostBack = true;
                    txtIndentNumber.Text = "";
                    txtIndentNumber.Focus();

                    lblMode.Text = "Update";

                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                    //tdDelete.Visible = false;

                    RefreshDetailRow();
                }
            }
            return iRecordFound;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for updation.\r\nSee error log for detail."));
            return iRecordFound;
        }

    }
    private void MapDataTable(DataTable dtTemp)
    {
        try
        {
            if (ViewState["FinalTotal"] != null)
            {
                FinalTotal = (double)ViewState["FinalTotal"];
            }
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }

            if (dtIndentDetail == null || dtIndentDetail.Rows.Count == 0)
                CreateIndentDetailTable();
            dtIndentDetail.Rows.Clear();
            FinalTotal = 0;

            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {

                    DataRow dr = dtIndentDetail.NewRow();
                    dr["UniqueId"] = dtIndentDetail.Rows.Count + 1;
                    dr["IndentDetailNumber"] = 0;
                    dr["IndentNumber"] = drTemp["IND_NUMB"];
                    dr["FIBER_CODE"] = drTemp["FIBER_CODE"];
                    dr["REQ_DATE"] = drTemp["REQ_DATE"];
                    dr["FIBER_DESC"] = drTemp["FIBER_DESC"];
                    dr["PI_NO"] = drTemp["PI_NO"];
                    dr["currentStock"] = drTemp["currentStock"];
                    dr["MIN_STOCK_LVL"] = drTemp["MIN_STOCK"];
                    dr["OP_RATE"] = drTemp["OP_RATE"];
                    dr["RQST_QTY"] = drTemp["RQST_QTY"];
                    dr["VC_UNITNAME"] = drTemp["UNIT_NAME"];
                    dr["Amount"] = drTemp["iValue"];
                    dr["DPT_REMARK"] = drTemp["DPT_REMARK"];
                    FinalTotal = FinalTotal + double.Parse(drTemp["iValue"].ToString());
                    dtIndentDetail.Rows.Add(dr);

                }
                ViewState["FinalTotal"] = FinalTotal;
                ViewState["dtIndentDetail"] = dtIndentDetail;
                dtTemp = null;

            }
        }
        catch
        {
            throw;
        }
    }
    private DataTable GetItemTransaction(string IndentNumber)
    {
        try
        {
            if (ViewState["IND_TYPE"] != null)
            {
                IND_TYPE = (string)ViewState["IND_TYPE"];
            }

            DataTable dtTemp = SaitexBL.Interface.Method.FIBER_IND_MST.Select_TransactionByIndentNumber1(oUserLoginDetail.DT_STARTDATE.Year, int.Parse(IndentNumber), oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, IND_TYPE);

            return dtTemp;
        }
        catch
        {
            throw;
        }
 
    }
    protected void ddlFiberCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtFiberCode.Text = ddlFiberCode.SelectedText.ToString();
            var Desc= ddlFiberCode.SelectedValue.ToString();
            string[] words = Desc.Split('@');

            txtFiberDesc.Text = words[4].ToString();
            lblPINO.Text = words[1].ToString();
            string Description = "";
            string UOM = "";
            double CurrentStock = 0;
            double MinStockLevel = 0;
            double OpeningRate = 0;
            double Min_Procure_days = 0;
            int UniqueId = 0;
            if (ViewState["UniqueId"] != null)
                UniqueId = int.Parse(ViewState["UniqueId"].ToString());
            if (!SearchItemCodeInGrid(txtFiberCode.Text.Trim(), UniqueId))
            {
                int iRecordFound = GetItemDetailByFiberCode(CommonFuction.funFixQuotes(txtFiberCode.Text.Trim()), out Description, out UOM, out CurrentStock, out MinStockLevel, out OpeningRate, out Min_Procure_days);
                if (iRecordFound > 0)
                {
                    txtOpeningRate.Text = OpeningRate.ToString();
                    txtFiberDesc.Text = Description;
                    txtCurrent.Text = CurrentStock.ToString();
                    txtMinStock.Text = MinStockLevel.ToString();
                    txtUnitName.Text = UOM;
                    txtRequestQty.Focus();
                    lblMin_Procure_days.Text = Min_Procure_days.ToString();

                }
                else
                {
                    RefreshDetailRow();
                }
            }
            else
            {
                CommonFuction.ShowMessage("This Item already included");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private int GetItemDetailByFiberCode(string FiberCode, out string Description, out string UOM, out double CurrentStock, out double MinStockLevel, out double OpeningRate, out double Min_Procure_days)
    {
        int iRecordFound = 0;
        Description = "";
        UOM = "";
        CurrentStock = 0;
        MinStockLevel = 0;
        OpeningRate = 0;
        Min_Procure_days = 0;
        try
        {
            DataTable dts = SaitexBL.Interface.Method.FIBER_IND_MST.GetItemDetailByFiberCode(oUserLoginDetail.DT_STARTDATE.Year, FiberCode, oUserLoginDetail.CH_BRANCHCODE);
            if (dts != null && dts.Rows.Count > 0)
            {
                Description = dts.Rows[0]["FIBER_DESC"].ToString().Trim();
                UOM = dts.Rows[0]["UNIT_NAME"].ToString().Trim();
                double.TryParse(dts.Rows[0]["currentStock"].ToString().Trim(), out CurrentStock);
                double.TryParse(dts.Rows[0]["MIN_STOCK"].ToString().Trim(), out MinStockLevel);
                double.TryParse(dts.Rows[0]["OP_RATE"].ToString().Trim(), out OpeningRate);
                double.TryParse(dts.Rows[0]["MIN_PROCURE_DAYS"].ToString().Trim(), out Min_Procure_days);
                iRecordFound = dts.Rows.Count;
            }
            return iRecordFound;
        }
        catch
        {
            throw;
        }
    }
    protected bool SearchItemCodeInGrid(string FIBER_CODE,int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdrow in grdIndentDetail.Rows)
            {
                //Label txtFiberCode = (Label)grdrow.FindControl("");
                Label txtFiberCode = (Label)grdrow.FindControl("txtItemCode");
                LinkButton lnkDelete = (LinkButton)grdrow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (txtFiberCode.Text.Trim() == FIBER_CODE && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void txtRequestQty_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)txtRequestQty;
            double openRate = 0;
            double reqQty = 0;
            double.TryParse(txtOpeningRate.Text,out openRate);
            double.TryParse(txtRequestQty.Text,out reqQty);
            txtAmount.Text=(openRate * reqQty).ToString();
            thisTextBox.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in entering quantity .\r\nSee error log for detail."));
        }
    }
    protected void grdIndentDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "indentEdit")
            {
                FillDataFromGrid(UniqueId);
            }
            else
                if (e.CommandName == "indentDelete")
                {
                    DeleteIndentDetailRow(UniqueId);
                    BindIndentDetailGrid();
                }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
  
        }

    }
    private void FillDataFromGrid(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            DataView dv = new DataView(dtIndentDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                txtFiberCode.Text = dv[0]["FIBER_CODE"].ToString();
                txtFiberDesc.Text = dv[0]["FIBER_DESC"].ToString();
                txtCurrent.Text = dv[0]["currentStock"].ToString();
                txtMinStock.Text = dv[0]["MIN_STOCK_LVL"].ToString();

                lblPINO.Text = dv[0]["PI_NO"].ToString();
                txtOpeningRate.Text = dv[0]["OP_RATE"].ToString();
                txtRequestQty.Text = dv[0]["RQST_QTY"].ToString();
                txtUnitName.Text = dv[0]["VC_UNITNAME"].ToString();
                txtAmount.Text = dv[0]["Amount"].ToString();
                txtRemark.Text = dv[0]["DPT_REMARK"].ToString();
                txtReqDate.Text = dv[0]["REQ_DATE"].ToString();
                ddlFiberCode.Enabled = false;
                ddlFiberCode.SelectedText = (dv[0]["FIBER_CODE"].ToString());
               
                ViewState["UniqueId"] = UniqueId;
            }


        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
  
        }
    }
    private void DeleteIndentDetailRow(int UniqueId)
    {
        try
        {
            if (ViewState["dtIndentDetail"] != null)
            {
                dtIndentDetail = (DataTable)ViewState["dtIndentDetail"];
            }
            if (grdIndentDetail.Rows.Count == 1)
            {
                dtIndentDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtIndentDetail.Rows.Remove(dr) ;
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtIndentDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
                ViewState["dtIndentDetail"] = dtIndentDetail;
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in row editing/ deletion.\r\nSee error log for detail."));
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

}
