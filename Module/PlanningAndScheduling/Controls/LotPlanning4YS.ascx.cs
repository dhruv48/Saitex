using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Module_PlanningAndScheduling_Controls_LotPlanning4YS : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
   
    public  string COMP_CODE = string.Empty;
    public  string BRANCH_CODE = string.Empty;
    public  string PRODUCT_TYPE = string.Empty;
    public  string ORDER_CAT = string.Empty;
    public  string ORDER_TYPE = string.Empty;
    public  string ORDER_NO = string.Empty;
    public  string PA_NO = string.Empty;
    private string STATUS = string.Empty;
    public  string ARTICAL_CODE = string.Empty;
    public  string SHADE_CODE = string.Empty;
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
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (Request.QueryString["PRODUCT_TYPE"] != null && Request.QueryString["PRODUCT_TYPE"].ToString().Equals(string.Empty) == false)
            {
                PRODUCT_TYPE = Request.QueryString["PRODUCT_TYPE"].ToString();
            } if (ddlProductType.SelectedValue.ToString() != null || ddlProductType.SelectedValue.ToString() != string.Empty)
            {
                PRODUCT_TYPE = ddlProductType.SelectedValue.ToString();
            }
            else
            {
                PRODUCT_TYPE = string.Empty;
            }
            if (ddlOrderCategory.SelectedValue.ToString() != null || ddlOrderCategory.SelectedValue.ToString() != string.Empty)
            {
                ORDER_CAT = ddlOrderCategory.SelectedValue.ToString();
            }
            else
            {
                ORDER_CAT = string.Empty;
            }
            if (ddlordertype.SelectedValue.ToString() != null || ddlordertype.SelectedValue.ToString() != string.Empty)
            {
                ORDER_TYPE = ddlordertype.SelectedValue.ToString();

            }
            else
            {
                ORDER_TYPE = string.Empty;
            }
            if (ddlStatus.SelectedValue.ToString() != null || ddlStatus.SelectedValue.ToString() != string.Empty)
            {
                STATUS = ddlStatus.SelectedValue.ToString();
            }
            else
            {
                STATUS = string.Empty;
            }
            if (!IsPostBack)
            {
                

                InitialisePage();
                ViewInGrid();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }

    }
    protected void ViewInGrid()
    {
        try
        {
           
           
            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.ViewDataInGrid(PRODUCT_TYPE,ORDER_CAT,ORDER_TYPE,oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE,STATUS);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("LOT_CNF_DATE"))
                {
                    dt.Columns.Add("LOT_CNF_DATE", typeof(DateTime));
                }
                if (!dt.Columns.Contains("LOT_CNF_BY"))
                {
                    dt.Columns.Add("LOT_CNF_BY", typeof(string));
                }
                foreach (DataRow dr in dt.Rows)
                {
                    string cnf_by = dr["LOT_CNF_BY"].ToString();
                    if (cnf_by == "")
                        dr["LOT_CNF_BY"] = oUserLoginDetail.Username;
                    dr["LOT_CNF_DATE"] = System.DateTime.Now.Date.ToShortDateString();

                }
                grdLotPlanning.DataSource = dt;
                grdLotPlanning.DataBind();
                lblTotalRecords.Text = dt.Rows.Count.ToString();

            }
            else
            {
                grdLotPlanning.DataSource = null;
                grdLotPlanning.DataBind();
                Common.CommonFuction.ShowMessage("Data Not Present");
                lblTotalRecords.Text = "0";
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }
 

    }
    public void InitialisePage()
    {
        try
        {
            lblTotalRecords.Text = "0";
            BindOrder();
            BindProductType();
            ddlOrderCategory.Items.Insert(0,new ListItem("---Select-----",""));
            grdLotPlanning.DataSource = null;
            grdLotPlanning.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }
    }
    private void BindProductType()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            ddlProductType.Items.Clear();
            DataTable dtProductionType = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);

            ddlProductType.DataSource = dtProductionType;
            ddlProductType.DataTextField = "MST_DESC";
            ddlProductType.DataValueField = "MST_CODE";
            ddlProductType.DataBind();

            ddlProductType.SelectedIndex = ddlProductType.Items.IndexOf(ddlProductType.Items.FindByValue(PRODUCT_TYPE));
            ddlProductType.Text = PRODUCT_TYPE;
            //ddlProductType.Enabled = false;
        }
        catch
        {
            throw;
        }
    }
    protected void BindOrder()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("ORDER_TYPE", oUserLoginDetail.COMP_CODE);
            ddlordertype.Items.Clear();
            ddlordertype.DataSource = dt;
            ddlordertype.DataValueField = "MST_DESC";
            ddlordertype.DataTextField = "MST_CODE";
            ddlordertype.DataBind();
            ddlordertype.Items.Insert(0, new ListItem("----Select---", ""));
            dt.Dispose();
            dt = null;
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
            DataTable dtUpdate = createLotUpdateTable();
            foreach (GridViewRow gvr in grdLotPlanning.Rows)
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
                Row = dtUpdate.NewRow();
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
                dtUpdate.Rows.Add(Row);
                bool Result = SaitexDL.Interface.Method.OD_CAPT_MST.UpdateFinalLotFlag(dtUpdate);
                if (Result)
                {
                    string msg = string.Empty;
                    msg += "Final Confirmation Flag Updated";
                    Common.CommonFuction.ShowMessage(msg);
                }
                else
                {
                    
                        Common.CommonFuction.ShowMessage("Final Lot Confirmation flag Updating Failed");
                    
                }

            }

            


        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialisePage();
            ViewInGrid();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void grdLotPlanning_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdLotPlanning.PageIndex = e.NewPageIndex;
            ViewInGrid();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }
    }
    protected void grdLotPlanning_RowDataBound(object sender, GridViewRowEventArgs e)
    
    {
        try
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblLot_Flag = ((Label)e.Row.FindControl("lblLot_Flag"));
                LinkButton linkLotDetail = ((LinkButton)e.Row.FindControl("linkLotDetail"));
                CheckBox chk=((CheckBox)e.Row.FindControl("CheckBox1"));
                if (lblLot_Flag.Text.Equals("1"))
                {
                    linkLotDetail.ForeColor = System.Drawing.Color.Green;
                    chk.Enabled = true;
                    chk.BackColor = System.Drawing.Color.Green;
                }
                else if (lblLot_Flag.Text.Equals("0"))
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
                else if(lbl_Final_Flag.Text.Equals("0"))
                {
                    chk.Checked=false;
                }

            }
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }

    }
    protected void grdLotPlanning_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName != "Page" && e.CommandName != "")
            {
                GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

                Label lblComp_Code = (Label)gvr.FindControl("lblComp_Code");
                Label lblBranch_Code=(Label)gvr.FindControl("lbl_Branch_Code");
                Label lblBusiness_Type = (Label)gvr.FindControl("lblBusiness_Type");
                Label lblProduct_Type = (Label)gvr.FindControl("lblProduct_type");
                Label lblOrder_Type = (Label)gvr.FindControl("lblOrder_Type");
                Label lblOrder_Cat = (Label)gvr.FindControl("lblOrder_Cat");
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
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
                else if (e.CommandName == "LotDetail")
                {

                    try
                    {
                        string URL = "LotDetail.aspx";
                        URL = URL + "?COMP_CODE=" + lblComp_Code.Text;
                        URL = URL + "&BRANCH_CODE=" + lblBranch_Code.Text;
                        URL = URL + "&BUSINESS_TYPE="+ lblBusiness_Type.Text;
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
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                }

            }

             
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
            LblError.Text = ex.ToString();
        }
    }
}
