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
using System.IO;

public partial class Module_PlanningAndScheduling_Controls_RawMaterialPlan : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string COMP_CODE = string.Empty;
    public static string BRANCH_CODE = string.Empty;
    public static string PRODUCT_TYPE = string.Empty;
    public static string ORDER_CAT = string.Empty;
    public static string ORDER_TYPE = string.Empty;
    public static string ORDER_NO = string.Empty;
    public static string PA_NO = string.Empty;
    public static string ARTICAL_CODE = string.Empty;
    public static string SHADE_CODE = string.Empty;


    public string PRODUCTTYPE
    {
        get
        {
            return PRODUCT_TYPE;
        }
        set
        {
            PRODUCT_TYPE = value;
        }
    }


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initial_Control();
                ViewInGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    private void ViewInGrid()
    {
        try
        {
            lblTotalRecord.Text = "0";

            if (ddlProductType.SelectedValue.ToString() != null && ddlProductType.SelectedValue.ToString() != string.Empty)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }

            if (ddlOrderCat.SelectedValue.ToString() != null && ddlOrderCat.SelectedValue.ToString() != string.Empty)
            {
                ORDER_CAT = ddlOrderCat.SelectedValue.ToString();
            }
            else
            {
                ORDER_CAT = string.Empty;
            }

            if (ddlOrderType.SelectedValue.ToString() != null && ddlOrderType.SelectedValue.ToString() != string.Empty)
            {
                ORDER_TYPE = ddlOrderType.SelectedValue.ToString();
            }
            else
            {
                ORDER_TYPE = string.Empty;
            }

            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.GetDataForGrid(PRODUCT_TYPE, ORDER_CAT, ORDER_TYPE, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("LOT_CNF_DATE"))
                    dt.Columns.Add("LOT_CNF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("LOT_CNF_BY"))
                    dt.Columns.Add("LOT_CNF_BY", typeof(string));
                foreach (DataRow dr in dt.Rows)
                {
                    string cnf_by = dr["LOT_CNF_BY"].ToString();
                    if (cnf_by == "")
                        dr["LOT_CNF_BY"] = oUserLoginDetail.Username;
                    dr["LOT_CNF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                grdRawMaterialPlan.DataSource = dt;
                grdRawMaterialPlan.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                grdRawMaterialPlan.DataSource = null;
                Common.CommonFuction.ShowMessage("Data Not Found");
                Initial_Control();
                lblTotalRecord.Text = "0";
            }
        }
        catch (Exception)
        {

            throw;
        }
    }

    private void Initial_Control()
    {
        try
        {
            lblMode.Text = "Update";
            lblTotalRecord.Text = "0";
            BindProductType();
            BindOrderType();
            ddlOrderCat.Items.Insert(0, new ListItem("-----SELECT-----", ""));
            grdRawMaterialPlan.DataSource = null;
            grdRawMaterialPlan.DataBind();
        }
        catch
        {

            throw;
        }
    }

    private void BindOrderType()
    {
        try
        {
            ddlOrderType.Items.Clear();
            DataTable dtOrderType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);
            ddlOrderType.DataSource = dtOrderType;
            ddlOrderType.DataTextField = "MST_DESC";
            ddlOrderType.DataValueField = "MST_CODE";
            ddlOrderType.DataBind();
            ddlOrderType.Items.Insert(0, new ListItem("-------SELECT------", ""));
            dtOrderType.Dispose();
            dtOrderType = null;
        }
        catch
        {

            throw;
        }
    }

    private void BindProductType()
    {
        try
        {
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();

            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
            ddlProductType.Text = PRODUCT_TYPE;
            ddlProductType.Enabled = false;
          
            dtProductionType.Dispose();
            dtProductionType = null;
        }
        catch
        {

            throw;
        }
    }


    protected void btnShow_Click(object sender, EventArgs e)
    {
        try
        {
            ViewInGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Control();
            ViewInGrid();
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clearing the Data..\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
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

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }

    protected void grdRawMaterialPlan_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdRawMaterialPlan.PageIndex = e.NewPageIndex;
            ViewInGrid();
        }
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Grid View Paging.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }


    protected void grdRawMaterialPlan_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblLot_Flag = ((Label)e.Row.FindControl("lblLot_Flag"));
            LinkButton linkLotDetail = ((LinkButton)e.Row.FindControl("linkLotDetail"));
            CheckBox chk = ((CheckBox)e.Row.FindControl("CheckBox1"));
            if (lblLot_Flag.Text.Equals("1"))
            {
                linkLotDetail.ForeColor = System.Drawing.Color.Green;
                chk.Enabled = true;
                chk.BackColor = System.Drawing.Color.Green;
            }
            else if(lblLot_Flag.Text.Equals("0"))
            {
                linkLotDetail.ForeColor = System.Drawing.Color.Red;
                chk.Enabled = false;
                chk.BackColor = System.Drawing.Color.Red;
            }

            Label lbl_Final_Flag = (Label)e.Row.FindControl("lbl_Final_Lot_Flag");

            if (lbl_Final_Flag.Text.Equals("1"))
            {
                chk.Checked = true;
            }
            else if (lbl_Final_Flag.Text.Equals("0"))
            {
                chk.Checked = false;
            }
        }
    }

    protected void grdRawMaterialPlan_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void grdRawMaterialPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Page" && e.CommandName != "")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            Label lblComp_Code = (Label)gvr.FindControl("lblComp_Code");
            Label lblBranch_Code = (Label)gvr.FindControl("lbl_Branch_Code");
            Label lblBusiness_Type = (Label)gvr.FindControl("lblBusiness_Type");
            Label lblProduct_Type = (Label)gvr.FindControl("lblProduct_type");
            Label lblOrder_Cat = (Label)gvr.FindControl("lblOrder_Cat");
            Label lblOrder_Type = (Label)gvr.FindControl("lblOrder_Type");
            Label lblOrder_No = (Label)gvr.FindControl("lblOrder_No");
            Label lblPi_Type = (Label)gvr.FindControl("lblPi_Type");
            Label lblPA_No = (Label)gvr.FindControl("lblPA_NO");
            Label lblArtical_Code = (Label)gvr.FindControl("lblArtical_Code");
            Label lblShade_Code = (Label)gvr.FindControl("lblShade_Code");
            Label lblBom_Flag = (Label)gvr.FindControl("lblBom_Flag");
            Label lblLot_Flag = (Label)gvr.FindControl("lblLot_Flag");
            Label lblOrd_Qty = (Label)gvr.FindControl("lblOrder_QTY");
            Label lblLot_id = (Label)gvr.FindControl("lblLot_id");

            if (e.CommandName == "ViewBOM")
            {
                try
                {
                    string URL = "BOM.aspx";
                    URL = URL + "?COMP_CODE=" + lblComp_Code.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBranch_Code.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblBusiness_Type.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblProduct_Type.Text;
                    URL = URL + "&ORDER_CAT=" + lblOrder_Cat.Text;
                    URL = URL + "&ORDER_TYPE=" + lblOrder_Type.Text;
                    URL = URL + "&ORDER_NO=" + lblOrder_No.Text;
                    URL = URL + "&PI_TYPE=" + lblPi_Type.Text;
                    URL = URL + "&PI_NO=" + lblPA_No.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblArtical_Code.Text;
                    URL = URL + "&SHADE_CODE=" + lblShade_Code.Text;
                    URL = URL + "&BOM_FLAG=" + lblBom_Flag.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
                catch
                {

                    throw;
                }


            }
            else if (e.CommandName == "LotDetail")
            {
                try
                {
                    string URL = "LotDetail.aspx";
                    URL = URL + "?COMP_CODE=" + lblComp_Code.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBranch_Code.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblBusiness_Type.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblProduct_Type.Text;
                    URL = URL + "&ORDER_CAT=" + lblOrder_Cat.Text;
                    URL = URL + "&ORDER_TYPE=" + lblOrder_Type.Text;
                    URL = URL + "&ORDER_NO=" + lblOrder_No.Text;
                    URL = URL + "&PI_TYPE=" + lblPi_Type.Text;
                    URL = URL + "&PI_NO=" + lblPA_No.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblArtical_Code.Text;
                    URL = URL + "&SHADE_CODE=" + lblShade_Code.Text;
                    URL = URL + "&ORD_QTY=" + lblOrd_Qty.Text;
                    URL = URL + "&LOT_FLAG=" + lblLot_Flag.Text;
                    URL = URL + "&LOT_ID=" + lblLot_id.Text;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

                }
                catch 
                {

                    throw;
                }
            }
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtUpdateLotFlag = createLotUpdateTable();
        foreach (GridViewRow gvr in grdRawMaterialPlan.Rows)
        {
            Label lbl_Comp_Code = (Label)gvr.FindControl("lblComp_Code");
            Label lbl_Branch_Code = (Label)gvr.FindControl("lbl_Branch_Code");
            Label lbl_Business_Type = (Label)gvr.FindControl("lblBusiness_Type");
            Label lbl_Prod_Type = (Label)gvr.FindControl("lblProduct_type");
            Label lbl_Order_Cat = (Label)gvr.FindControl("lblOrder_Cat");
            Label lbl_Order_Type = (Label)gvr.FindControl("lblOrder_Type");
            Label lbl_Order_No = (Label)gvr.FindControl("lblOrder_No");
            Label lbl_Pi_Type = (Label)gvr.FindControl("lblPi_Type");
            Label lbl_Pi_No = (Label)gvr.FindControl("lblPA_NO");
            Label lbl_Artical_Code = (Label)gvr.FindControl("lblArtical_Code");
            Label lbl_Shade_Code = (Label)gvr.FindControl("lblShade_Code");
            Label lbl_Lot_id = (Label)gvr.FindControl("lblLot_id");
            TextBox txt_lot_cnf_date = (TextBox)gvr.FindControl("txtLotDate");
            TextBox txt_lot_cnf_by = (TextBox)gvr.FindControl("txtCofBy");
            CheckBox chk = (CheckBox)gvr.FindControl("CheckBox1");

         
            DataRow Row;
            Row = dtUpdateLotFlag.NewRow();
            Row["COMP_CODE"] = lbl_Comp_Code.Text;
            Row["BRANCH_CODE"] = lbl_Branch_Code.Text;
            Row["BUSINESS_TYPE"] = lbl_Business_Type.Text;
            Row["PRODUCT_TYPE"] = lbl_Prod_Type.Text;
            Row["ORDER_CAT"] = lbl_Order_Cat.Text;
            Row["ORDER_TYPE"] = lbl_Order_Type.Text;
            Row["ORDER_NO"] = lbl_Order_No.Text;
            Row["PI_TYPE"] = lbl_Pi_Type.Text;
            Row["PI_NO"] = lbl_Pi_No.Text;
            Row["ARTICAL_CODE"] = lbl_Artical_Code.Text;
            Row["SHADE_CODE"] = lbl_Shade_Code.Text;
            Row["LOT_CNF_DATE"] = txt_lot_cnf_date.Text;
            Row["LOT_CNF_BY"] = txt_lot_cnf_by.Text;
            Row["LOT_ID"] = lbl_Lot_id.Text;

            if (chk.Checked)
            {
                Row["status"] = true;
            }
            else
            {
                Row["status"] = false;
            }

            dtUpdateLotFlag.Rows.Add(Row);

            bool Result = SaitexDL.Interface.Method.OD_CAPT_MST.UpdateFinalLotFlag(dtUpdateLotFlag);
            if (Result)
            {
                string msg = string.Empty;
                msg += "Final Lot Confirmation  Flag Updated";
                Common.CommonFuction.ShowMessage(msg);
            }
            else
            {
                {
                    Common.CommonFuction.ShowMessage("Final Lot Confirmation flag Updating Failed");
                }
            }
        }


    }

    private DataTable createLotUpdateTable()
    {
        DataTable dtUpdateLotFlag = new DataTable();
        dtUpdateLotFlag.Columns.Add("COMP_CODE", typeof(string));
        dtUpdateLotFlag.Columns.Add("BRANCH_CODE", typeof(string));
        dtUpdateLotFlag.Columns.Add("BUSINESS_TYPE", typeof(string));
        dtUpdateLotFlag.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtUpdateLotFlag.Columns.Add("ORDER_CAT", typeof(string));
        dtUpdateLotFlag.Columns.Add("ORDER_TYPE", typeof(string));
        dtUpdateLotFlag.Columns.Add("ORDER_NO", typeof(string));
        dtUpdateLotFlag.Columns.Add("PI_TYPE", typeof(string));
        dtUpdateLotFlag.Columns.Add("PI_NO", typeof(string));
        dtUpdateLotFlag.Columns.Add("ARTICAL_CODE", typeof(string));
        dtUpdateLotFlag.Columns.Add("SHADE_CODE", typeof(string));
        dtUpdateLotFlag.Columns.Add("LOT_ID", typeof(string));
        dtUpdateLotFlag.Columns.Add("LOT_CNF_DATE", typeof(DateTime));
        dtUpdateLotFlag.Columns.Add("LOT_CNF_BY", typeof(string));
        dtUpdateLotFlag.Columns.Add("status", typeof(bool));

        return dtUpdateLotFlag;
    }
}