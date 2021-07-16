using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class Module_OrderDevelopment_Controls_Recipe_Entry : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    
    private static DataTable dtRecipeDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            getRecipeNo();
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Save";

                bindddlSecondaryLightSource();
                bindddlPrimaryLightSource();
                bindcmbRecipeNo();
                bindcmbLabDipNo();
                bindddlBasis();
                bindddlItemCode();
                cmbRecipeNumber.Visible = false;
                txtRecipeNumber.Visible = true;
                tdUpdate.Visible = false;
                tdFind.Visible = true;
                tdDelete.Visible = false;
                txtRecipeDate.Text = System.DateTime.Now.ToShortDateString();
                if (dtRecipeDetail == null)
                    CreateDataTable();
                dtRecipeDetail.Rows.Clear();

            }

            DateTime startdate = oUserLoginDetail.DT_STARTDATE;
            //RangeValidator1.MinimumValue = startdate.ToShortDateString();
            //RangeValidator1.MaximumValue = DateTime.Now.Date.ToShortDateString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This function Generates the new Recipe No and binds that in the textbox on page load.
    /// </summary>
    private void getRecipeNo()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            //oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
            SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST oOD_RECIPE_ENTRY_MST = new SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST();
            oOD_RECIPE_ENTRY_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_RECIPE_ENTRY_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_RECIPE_ENTRY_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            int NewRecipeNo = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.GetNewRECNO(oOD_RECIPE_ENTRY_MST);
            txtRecipeNumber.Text = NewRecipeNo.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function binds the ComboBox for LabDip No.
    /// </summary>
    private void bindcmbLabDipNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectLabDipNo();
            cmbLabdipNo.DataSource = dt;
            cmbLabdipNo.DataValueField = "LAB_DIP_NO";
            cmbLabdipNo.DataTextField = "LAB_DIP_NO";
            cmbLabdipNo.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the Dropdown for ItemCode.
    /// </summary>
    private void bindddlItemCode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectItemCode();
            cmbItemCode.DataSource = dt;
            cmbItemCode.DataValueField = "ITEM_CODE";
            cmbItemCode.DataTextField = "ITEM_CODE";
            cmbItemCode.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the ComboBox for Basis.
    /// </summary>
    private void bindddlBasis()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectBasis();
            ddlBasis.DataSource = dt;
            ddlBasis.DataValueField = "MST_CODE";
            ddlBasis.DataTextField = "MST_CODE";
            ddlBasis.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the Dropdown for PrimaryLightSource.
    /// </summary>
    private void bindddlPrimaryLightSource()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectPLS();
            ddlPLS.DataSource = dt;
            ddlPLS.DataValueField = "MST_CODE";
            ddlPLS.DataTextField = "MST_CODE";
            ddlPLS.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the Dropdown for SecondaryLightSource.
    /// </summary>
    private void bindddlSecondaryLightSource()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectSLS();
            ddlSLS.DataSource = dt;
            ddlSLS.DataValueField = "MST_CODE";
            ddlSLS.DataTextField = "MST_CODE";
            ddlSLS.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function makes all the controls of this page blank.
    /// </summary>
    private void BlankControl()
    {
        try
        {
            txtRecipeNumber.Text = "";
            txtRemarks.Text = "";
            txtValueKg.Text = "";
            txtQtyKg.Text = "";
            txtProcessCode.Text = "";
            txtDesc.Text = "";
            txtCurrRate.Text = "";
            txtFabricExpr.Text = "";
            cmbRecipeNumber.SelectedIndex = -1;
            cmbLabdipNo.SelectedIndex = -1;
            cmbItemCode.SelectedIndex = -1;
            ddlProcessCode.SelectedIndex = 0;
            txtComment.Text = "";
            txtQty.Text = "";
            txtLabdipNo.Text = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlProcessCode.SelectedValue != null && cmbLabdipNo.SelectedValue != null)
            {
                Insertdata();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved : Select Process Code/LabDipNo');", true);
            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtRecipeNumber.Visible = false;
            cmbRecipeNumber.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (ddlProcessCode.SelectedValue != null && cmbLabdipNo.SelectedValue != null)
            {
                Updatedata();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved : Select Process Code/LabDipNo');", true);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Pages/RecipeEntryOPT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            BlankControl();
            getRecipeNo();
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            lblMode.Text = "Save";
            ddlProcessCode.SelectedIndex = 0;
            cmbLabdipNo.SelectedIndex = -1;
            cmbRecipeNumber.Visible = false;
            txtRecipeNumber.Visible = true;
            dtRecipeDetail.Rows.Clear();
            bindGrid();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
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
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE RECIPE_NO like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "order by RECIPE_NO";
        string commandText = "select distinct RECIPE_NO from OD_RECIPE_ENTRY_MST ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM OD_RECIPE_ENTRY_MST WHERE  LAB_DIP_NO like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function saves the record in OD_RECIPE_ENTRY_MST  and OD_RECIPE_ENTRY_TRN table.
    /// </summary>
    private void Insertdata()
    {
        try
        {
            string msg = string.Empty;
            if (ValidateRecipeMasterRow(out msg))
            {
                if (dtRecipeDetail != null && dtRecipeDetail.Rows.Count > 0)
                {
                    SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST oOD_RECIPE_ENTRY_MST = new SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST();
                    //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
                    oOD_RECIPE_ENTRY_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oOD_RECIPE_ENTRY_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oOD_RECIPE_ENTRY_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oOD_RECIPE_ENTRY_MST.RECIPE_NO = Convert.ToInt32(txtRecipeNumber.Text.Trim());
                    oOD_RECIPE_ENTRY_MST.RECIPE_DATE = Convert.ToDateTime(txtRecipeDate.Text.Trim());
                    oOD_RECIPE_ENTRY_MST.PROCESS_CODE = ddlProcessCode.SelectedItem.Text.Trim();
                    oOD_RECIPE_ENTRY_MST.FABR_EXPR = txtFabricExpr.Text.Trim();
                    oOD_RECIPE_ENTRY_MST.LAB_DIP_NO = Convert.ToInt32(cmbLabdipNo.SelectedValue.Trim());
                    oOD_RECIPE_ENTRY_MST.PLS = ddlPLS.SelectedValue.Trim();
                    oOD_RECIPE_ENTRY_MST.SLS = ddlSLS.SelectedValue.Trim();
                    oOD_RECIPE_ENTRY_MST.REMARKS = txtComment.Text.Trim();
                    // oOD_RECIPE_ENTRY_MST.STATUS = chkActive.Checked;
                    oOD_RECIPE_ENTRY_MST.TUSER = Session["urLoginId"].ToString().Trim();
                    int NewRecipeNo = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.GetNewRECNO(oOD_RECIPE_ENTRY_MST);
                    txtRecipeNumber.Text = NewRecipeNo.ToString();
                    bool bResult = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.InsertRECENTRYMST(oOD_RECIPE_ENTRY_MST, dtRecipeDetail);
                    if (bResult)
                    {
                        BlankControl();
                        tdUpdate.Visible = false;
                        tdSave.Visible = true;
                        lblMode.Text = "Save";
                        cmbRecipeNumber.Visible = false;
                        txtRecipeNumber.Visible = true;
                        dtRecipeDetail.Rows.Clear();
                        bindGrid();
                        getRecipeNo();
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                    }

                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved :Enter atleast 1 Recipe Colour Details');", true);
                }
            }
            else
               
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        //else
        //{
        //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved :Select Process Code/LabDip No');", true);
        //}


        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function updates the record in OD_RECIPE_ENTRY_MST and OD_RECIPE_ENTRY_TRN tables.
    /// </summary>
    private void Updatedata()
    {
        try
        {
            if (dtRecipeDetail != null && dtRecipeDetail.Rows.Count > 0)
            {
                SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST oOD_RECIPE_ENTRY_MST = new SaitexDM.Common.DataModel.OD_RECIPE_ENTRY_MST();
                //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
                oOD_RECIPE_ENTRY_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_RECIPE_ENTRY_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_RECIPE_ENTRY_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oOD_RECIPE_ENTRY_MST.RECIPE_NO = Convert.ToInt32(cmbRecipeNumber.SelectedValue.Trim());
                oOD_RECIPE_ENTRY_MST.RECIPE_DATE = Convert.ToDateTime(txtRecipeDate.Text.Trim());
                oOD_RECIPE_ENTRY_MST.PROCESS_CODE = ddlProcessCode.SelectedValue.Trim();
                oOD_RECIPE_ENTRY_MST.FABR_EXPR = txtFabricExpr.Text.Trim();
                oOD_RECIPE_ENTRY_MST.LAB_DIP_NO = Convert.ToInt32(cmbLabdipNo.SelectedValue.Trim());
                oOD_RECIPE_ENTRY_MST.PLS = ddlPLS.SelectedValue.Trim();
                oOD_RECIPE_ENTRY_MST.SLS = ddlSLS.SelectedValue.Trim();
                oOD_RECIPE_ENTRY_MST.REMARKS = txtComment.Text.Trim();
                // oOD_RECIPE_ENTRY_MST.STATUS = chkActive.Checked;
                oOD_RECIPE_ENTRY_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.UpdateRECENTRYMST(oOD_RECIPE_ENTRY_MST, dtRecipeDetail);
                if (bResult)
                {
                    BlankControl();
                    tdUpdate.Visible = false;
                    tdSave.Visible = true;
                    lblMode.Text = "Save";
                    cmbRecipeNumber.Visible = false;
                    txtRecipeNumber.Visible = true;
                    dtRecipeDetail.Rows.Clear();
                    bindGrid();
                    getRecipeNo();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);

                }

                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function Creates the Data Table for Recipe Transaction Entry.
    /// </summary>
    private void CreateDataTable()
    {
        try
        {
            dtRecipeDetail = new DataTable();
            dtRecipeDetail.Columns.Add("UNIQUE_ID", typeof(int));
            dtRecipeDetail.Columns.Add("RECIPE_NO", typeof(int));
            dtRecipeDetail.Columns.Add("ITEM_CODE", typeof(string));
            dtRecipeDetail.Columns.Add("ITEM_DESC", typeof(string));
            dtRecipeDetail.Columns.Add("BASIS", typeof(string));
            dtRecipeDetail.Columns.Add("QTY", typeof(double));
            dtRecipeDetail.Columns.Add("QTY_KG", typeof(double));
            dtRecipeDetail.Columns.Add("VALUE_KG", typeof(double));
            dtRecipeDetail.Columns.Add("REMARKS", typeof(string));

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function Inserts the values in the datatable of Recipe transaction.
    /// </summary>
    private void InsertRow()
    {
        try
        {

            if (dtRecipeDetail == null)
            {
                CreateDataTable();
            }

            DataRow dr = dtRecipeDetail.NewRow();
            if (dtRecipeDetail.Rows.Count >= 0)
            {
                dr["UNIQUE_ID"] = dtRecipeDetail.Rows.Count + 1;
                ViewState["UNIQUE_ID"] = dr["UNIQUE_ID"].ToString();
                dtRecipeDetail.Rows.Add(dr);

            }

            bindGrid();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    /// <summary>
    /// This function binds the gridview of Recipe transaction.
    /// </summary>
    private void bindGrid()
    {
        try
        {
            if (dtRecipeDetail == null)
                InsertRow();
            GridView1.DataSource = dtRecipeDetail;
            GridView1.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function extracts the Corresponding LabDip shade of the given Labdip No.
    /// </summary>
    /// <param name="LabdipNo">LabDip No</param>
    private void Shade(int LabdipNo)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectShade(LabdipNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtLabdipNo.Text = dt.Rows[0]["SHADE"].ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function extracts The corresponding Itemcode description for the given ItemCode.
    /// </summary>
    /// <param name="ItemCode"></param>
    private void Desc(string ItemCode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectItemCodeDesc(ItemCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtCurrRate.Text = dt.Rows[0]["LAST_PO_RATE"].ToString();
                txtDesc.Text = dt.Rows[0]["ITEM_DESC"].ToString();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function makes edit of the entries in the Recipe Colour Details Gridview.
    /// </summary>
    /// <param name="RecipeNo"></param>
    private void EditItem(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(dtRecipeDetail);
            dv.RowFilter = "UNIQUE_ID=" + UniqueId;
            if (dv.Count > 0)
            {
                cmbItemCode.SelectedValue = dv[0]["ITEM_CODE"].ToString();
                ddlBasis.SelectedValue = dv[0]["BASIS"].ToString();
                txtQty.Text = dv[0]["QTY"].ToString();
                txtQtyKg.Text = dv[0]["QTY_KG"].ToString();
                txtValueKg.Text = dv[0]["VALUE_KG"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                //txtDesc.Text=dv[0][""].ToString();
                //txtCurrRate.Text = dv[0][""].ToString();
                ViewState["UNIQUE_ID"] = UniqueId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function makes deletes the row in the Recipe Colour Details Gridview.
    /// </summary>
    /// <param name="RecipeNo"></param>
    private void deleteItem(int UniqueId)
    {
        try
        {
            if (dtRecipeDetail.Rows.Count == 1)
            {
                dtRecipeDetail.Rows.Clear();
                InsertRow();
            }
            else
            {
                foreach (DataRow dr in dtRecipeDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtRecipeDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtRecipeDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;

        }
    }
    /// <summary>
    /// This function finds the recipe transaction details of the given Recipe no.
    /// </summary>
    private void RecipeTran()
    {
        try
        {
            if (dtRecipeDetail == null)
            {
                CreateDataTable();
            }
            DataRow dr = dtRecipeDetail.NewRow();
            int RecipeNo = Convert.ToInt32(cmbRecipeNumber.SelectedValue.Trim());
            dtRecipeDetail = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.FindRecipeTran(RecipeNo);
            GridView1.DataSource = dtRecipeDetail;
            GridView1.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string msg = string.Empty;
            if (ValidateDetailRow(out msg))
            {
                //if (ddlItemCode.SelectedIndex > 0)
                //{
                if (dtRecipeDetail == null)
                {
                    CreateDataTable();
                }
                int UniqueId = 0;
                if (ViewState["UNIQUE_ID"] != null)
                    UniqueId = int.Parse(ViewState["UNIQUE_ID"].ToString());
                if (UniqueId > 0 && dtRecipeDetail.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtRecipeDetail);
                    dv.RowFilter = "UNIQUE_ID=" + UniqueId;
                    if (dv.Count > 0)
                    {
                        dv[0]["RECIPE_NO"] = cmbRecipeNumber.SelectedValue.Trim();
                        dv[0]["ITEM_CODE"] = cmbItemCode.SelectedValue.Trim();
                        dv[0]["ITEM_DESC"] = txtDesc.Text.Trim();
                        dv[0]["BASIS"] = ddlBasis.SelectedValue.Trim();
                        double qty = 0;
                        double.TryParse(txtQty.Text.Trim(), out qty);
                        dv[0]["QTY"] = txtQty.Text.Trim();
                        dv[0]["QTY_KG"] = txtQtyKg.Text.Trim();
                        dv[0]["VALUE_KG"] = txtValueKg.Text.Trim();
                        dv[0]["REMARKS"] = txtRemarks.Text.Trim();
                    }

                    dtRecipeDetail.AcceptChanges();
                }
                else
                {
                    DataRow dr = dtRecipeDetail.NewRow();
                    dr["UNIQUE_ID"] = dtRecipeDetail.Rows.Count + 1;
                    cmbRecipeNumber.SelectedValue = txtRecipeNumber.Text.Trim();
                    dr["RECIPE_NO"] = cmbRecipeNumber.SelectedValue.Trim();
                    dr["ITEM_CODE"] = cmbItemCode.SelectedValue.Trim();
                    dr["BASIS"] = ddlBasis.SelectedValue.Trim();
                    dr["ITEM_DESC"] = txtDesc.Text.Trim();
                    dr["QTY"] = txtQty.Text.Trim();
                    dr["QTY_KG"] = txtQtyKg.Text.Trim();
                    dr["VALUE_KG"] = txtValueKg.Text.Trim();
                    dr["REMARKS"] = txtRemarks.Text.Trim();
                    dtRecipeDetail.Rows.Add(dr);
                }

                GridView1.DataSource = dtRecipeDetail;
                GridView1.DataBind();
                cmbItemCode.SelectedIndex = -1;
                ddlBasis.SelectedIndex = -1;
                txtDesc.Text = "";
                txtQty.Text = "";
                txtCurrRate.Text = "";
                txtQtyKg.Text = "";
                txtValueKg.Text = "";
                txtRemarks.Text = "";
            }
            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved:Select ItemCode');", true);
        //    }
        //}


        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This method Validates RecipeMasterRow
    /// </summary>
    /// <param name="msg">Output Message</param>
    /// <returns>true,false</returns>
    private bool ValidateRecipeMasterRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;

            int count = 0;
            int msgCount = 1;
            if (ddlProcessCode.SelectedIndex > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Process code. ";
                msgCount += 1;
            }

            if (cmbLabdipNo.SelectedIndex >= 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select LabDipNo. ";
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
    /// <summary>
    /// This method Validates RecipeColorDetails
    /// </summary>
    /// <param name="msg">Output Message</param>
    /// <returns>true,false</returns>
    private bool ValidateDetailRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;


            int count = 0;
            int msgCount = 1;
            if (cmbItemCode.SelectedIndex >= 0 || cmbItemCode.SelectedValue !=null)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Item. ";
                msgCount += 1;
            }

            double dd = 0;
            double.TryParse(txtQty.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Enter valid quantity. ";
                msgCount += 1;
                txtQty.Text = string.Empty;
            }

            dd = 0;
            double.TryParse(txtQtyKg.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Enter valid quantity/KG. ";
                msgCount += 1;
                txtQtyKg.Text = string.Empty;
            }

            dd = 0;
            double.TryParse(txtValueKg.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Enter valid value/KG. ";
                msgCount += 1;
                txtValueKg.Text = string.Empty;
            }
            if (count == 4)
                result = true;

            return result;
        }
        catch
        {
            throw;
        }
    }
    /// <summary>
    /// This function binds the combobox for recipeNo.
    /// </summary>
    private void bindcmbRecipeNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.SelectRecipeNo();
            cmbRecipeNumber.DataSource = dt;
            cmbRecipeNumber.DataValueField = "RECIPE_NO";
            cmbRecipeNumber.DataTextField = "RECIPE_NO";
            cmbRecipeNumber.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditItem")
            {
                EditItem(UniqueId);
                string ItemCode = cmbItemCode.SelectedValue.Trim();
                Desc(ItemCode);
            }
            else if (e.CommandName == "DelItem")
            {
                deleteItem(UniqueId);
                bindGrid();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbRecipeNumber_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);

            cmbRecipeNumber.Items.Clear();
            cmbRecipeNumber.DataSource = data;
            cmbRecipeNumber.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
  
    protected void cmbRecipeNumber_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dtMst = new DataTable();
            DataTable dtTran = new DataTable();
            int RecipeNo = Convert.ToInt32(cmbRecipeNumber.SelectedValue.Trim());
            txtRecipeNumber.Text = RecipeNo.ToString();
            dtMst = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.FindRecipeMst(RecipeNo);
            if (dtMst != null && dtMst.Rows.Count > 0)
            {
                dtTran = SaitexBL.Interface.Method.OD_RECIPE_ENTRY_MST.FindRecipeTran(RecipeNo);
                if (dtTran != null && dtTran.Rows.Count > 0)
                {
                    MapTable(dtTran);
                    BindRecipeTranasaction();
                    BindControls(dtMst);
                }
            }


        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }

    }
    /// <summary>
    /// This Method Maps Datatable for RecipeDetail with Recipe Transaction Table.
    /// </summary>
    /// <param name="dtTran">Datatable for Recipe Transaction Table</param>
    private void MapTable(DataTable dtTran)
    {
        try
        {
            if (dtRecipeDetail == null || dtRecipeDetail.Rows.Count == 0)
            {
                CreateDataTable();
            }
            dtRecipeDetail.Rows.Clear();
            if (dtTran != null && dtTran.Rows.Count > 0)
            {
                foreach (DataRow dr in dtTran.Rows)
                {
                    DataRow drRecipe = dtRecipeDetail.NewRow();
                    drRecipe["UNIQUE_ID"] = dtRecipeDetail.Rows.Count + 1;
                    drRecipe["RECIPE_NO"] = dr["RECIPE_NO"];
                    drRecipe["ITEM_CODE"] = dr["ITEM_CODE"];
                    drRecipe["BASIS"] = dr["BASIS"];
                    drRecipe["ITEM_DESC"] = dr["ITEM_DESC"];
                    drRecipe["QTY"] = dr["QTY"];
                    drRecipe["VALUE_KG"] = dr["VALUE_KG"];
                    drRecipe["QTY_KG"] = dr["QTY_KG"];
                    drRecipe["REMARKS"] = dr["REMARKS"];
                    dtRecipeDetail.Rows.Add(drRecipe);

                }
                dtTran = null;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This Method Binds the contols of Recipe Master Details.
    /// </summary>
    /// <param name="dt">Datatable</param>
    private void BindControls(DataTable dt)
    {
        if (dt != null && dt.Rows.Count > 0)
        {
            txtRecipeNumber.Text = dt.Rows[0]["RECIPE_NO"].ToString();
            txtFabricExpr.Text = dt.Rows[0]["FABR_EXPR"].ToString();
            ddlPLS.SelectedValue = dt.Rows[0]["PLS"].ToString();
            ddlSLS.SelectedValue = dt.Rows[0]["SLS"].ToString();
            txtProcessCode.Text = dt.Rows[0]["PROCESS_CODE"].ToString();
            txtComment.Text = dt.Rows[0]["REMARKS"].ToString();
            ddlProcessCode.SelectedValue = dt.Rows[0]["PROCESS_CODE"].ToString();
            cmbLabdipNo.SelectedValue = dt.Rows[0]["LAB_DIP_NO"].ToString();
            txtRecipeDate.Text = dt.Rows[0]["RECIPE_DATE"].ToString();
            int Labdipno = Convert.ToInt32(dt.Rows[0]["LAB_DIP_NO"].ToString());
            Shade(Labdipno);

        }
    }
    /// <summary>
    /// This Method Binds the grid for Recipe Transaction
    /// </summary>
    private void BindRecipeTranasaction()
    {
        GridView1.DataSource = dtRecipeDetail;
        GridView1.DataBind();


    }
    /// <summary>
    /// This Methods binds the controls for RecipeTranasaction.
    /// </summary>
    private void bindrecipeTran()
    {
        try
        {
            ddlBasis.SelectedValue = dtRecipeDetail.Rows[0]["BASIS"].ToString();
            //txtDesc.Text = dtRecipeDetail.Rows[0]["RECIPE_DATE"].ToString();
            txtQty.Text = dtRecipeDetail.Rows[0]["QTY"].ToString();
            txtValueKg.Text = dtRecipeDetail.Rows[0]["VALUE_KG"].ToString();
            txtQtyKg.Text = dtRecipeDetail.Rows[0]["QTY_KG"].ToString();
            txtRemarks.Text = dtRecipeDetail.Rows[0]["REMARKS"].ToString();
            cmbItemCode.SelectedValue = dtRecipeDetail.Rows[0]["ITEM_CODE"].ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void ddlItemCode_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        string ItemCode = cmbItemCode.SelectedValue.Trim();
    //        Desc(ItemCode);
    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void cmbLabdipNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            int LabdipNo = Convert.ToInt32(cmbLabdipNo.SelectedValue.Trim());
            Shade(LabdipNo);

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }
    protected void ddlProcessCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string P1 = ddlProcessCode.SelectedItem.Text.Trim();
            txtProcessCode.Text = P1.ToString();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);

        }
    }
    //protected void ddlItemCode_TextChanged(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        string ItemCode = cmbItemCode.SelectedValue.Trim();
    //        Desc(ItemCode);
    //    }
    //    catch (Exception ex)
    //    {
    //        errorLog.ErrHandler.WriteError(ex.Message);
    //    }
    //}
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected DataTable GetItemsforITEM(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE ITEM_CODE like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct ITEM_CODE,ITEM_TYPE,ITEM_DESC from TX_ITEM_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
   
    protected int GetItemsCountforITEM(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM TX_ITEM_MST WHERE ITEM_CODE like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
  
   
   
    protected DataTable GetItemsforPLS(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " where MST_NAME='LIGHT_SOURCE' and MST_CODE like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select MST_CODE from TX_MASTER_TRN";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
    protected DataTable GetItemsforBasis(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " where MST_NAME='BASIS' and MST_CODE like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select MST_CODE from TX_MASTER_TRN";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
    protected void cmbItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsforITEM(e.Text.ToUpper(), e.ItemsOffset, 10);

            cmbItemCode.Items.Clear();
            cmbItemCode.DataSource = data;
            cmbItemCode.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCountforITEM(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string ItemCode = cmbItemCode.SelectedValue.Trim();
            Desc(ItemCode);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbItemCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string ItemCode = cmbItemCode.SelectedValue.Trim();
            Desc(ItemCode);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            BlankColorDetails();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This Method Make the controls of Recipe Colour Detail Blank
    /// </summary>
    private void BlankColorDetails()
    {
        try
        {
            
            txtRemarks.Text = "";
            txtValueKg.Text = "";
            txtQtyKg.Text = "";
            txtDesc.Text = "";
            txtCurrRate.Text = "";
            cmbItemCode.SelectedIndex = -1;
            ddlBasis.SelectedIndex = -1;
            txtQty.Text = "";
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    protected void ddlPLS_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsforPLS(e.Text.ToUpper(), e.ItemsOffset, 10);
            ddlPLS.Items.Clear();
            ddlPLS.DataSource = data;
            ddlPLS.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void ddlSLS_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsforPLS(e.Text.ToUpper(), e.ItemsOffset, 10);

            ddlSLS.Items.Clear();
            ddlSLS.DataSource = data;
            ddlSLS.DataBind();

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void ddlBasis_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsforBasis(e.Text.ToUpper(), e.ItemsOffset, 10);
            ddlBasis.Items.Clear();
            ddlBasis.DataSource = data;
            ddlBasis.DataBind();
           
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}

