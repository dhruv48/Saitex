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

public partial class Module_OrderDevelopment_Controls_OrderApproval4YarnSpinning : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private  string ORDER = string.Empty;
    private string ORDER_TYPE = string.Empty;
    private string PRTY_CODE = string.Empty;
    private string COMP_CODE = string.Empty;
    private string BRANCH_CODE = string.Empty;
    private string BUSINESS_TYPE = string.Empty;
    private string PRODUCT_TYPE = string.Empty;
    private string ORDER_CAT = string.Empty;
    private string ORDER_NO = string.Empty;
    private string Fdate = string.Empty;
    private string Tdate = string.Empty;
    private string PI_NO = string.Empty;

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
            lblMessage.Text = ex.ToString();
        }
    }
    public void Initial_Control()
    {
        lblTotalRecord.Text ="0";
        BindOrder();
        imgbtnClear.Visible = true;
        imgbtnExit.Visible = true;
       
        imgbtnPrint.Visible = true;
        ddlordertype.SelectedIndex = -1;
        ddlprtycode.SelectedIndex = -1;
        TxtFdate.Text = string.Empty;
        TxtTdate.Text = string.Empty;

    }
    private void ViewInGrid()
    {
        try
        {
           // bool Status = false;

            lblTotalRecord.Text = "0";

            if (txtOrderNo.Text.ToString() != null && txtOrderNo.Text.ToString() != string.Empty)
            {
                ORDER = txtOrderNo.Text.Trim().ToString();
            }
            else
            {
                ORDER = string.Empty;
            }

            if (ddlordertype.SelectedValue.ToString() != null && ddlordertype.SelectedValue.ToString() != string.Empty)
            {
                ORDER_TYPE = ddlordertype.SelectedValue.ToUpper().ToString();
            }
            else
            {
                ORDER_TYPE = string.Empty;
            }

            if (ddlprtycode.SelectedValue.ToString() != null && ddlprtycode.SelectedValue.ToString() != "SELECT")
            {
                PRTY_CODE = ddlprtycode.SelectedItem;
                
               
            }
            else
            {
                PRTY_CODE = string.Empty;
            }

            if (TxtFdate.Text.ToString() != null && TxtFdate.Text.ToString() != string.Empty)
            {
                Fdate = TxtFdate.Text.Trim().ToString();
            }
            else
            {
                Fdate = string.Empty;
            }

            if (TxtTdate.Text.ToString() != null && TxtTdate.Text.ToString() != string.Empty)
            {
                Tdate = TxtTdate.Text.Trim().ToString();
            }
            else
            {
                Tdate = string.Empty;
            }


            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.ViewInGrid(ORDER, ORDER_TYPE, PRTY_CODE, Fdate, Tdate, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, ddlStatus.SelectedItem.ToString(), "YARN SPINING");
            if (dt != null && dt.Rows.Count > 0)
            {
                OrderCapt_Grid.DataSource = dt;
                OrderCapt_Grid.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                OrderCapt_Grid.DataSource = null;
                OrderCapt_Grid.DataBind();
                Common.CommonFuction.ShowMessage("Data not found by selected item.");
                lblTotalRecord.Text = "0";
            }
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
    protected void btnview_Click(object sender, EventArgs e)
    {
        try
        {
            ViewInGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }
    private void GetOrderCapt_GridData()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.ViewInGrid(txtOrderNo.Text.Trim(), ddlordertype.SelectedValue.ToString(), ddlprtycode.SelectedItem.ToString(), TxtFdate.Text.Trim().ToString(), TxtTdate.Text.Trim().ToString(), oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.COMP_CODE, ddlStatus.SelectedItem.ToString(), "YARN SPINING");
            OrderCapt_Grid.DataSource = dt;
            OrderCapt_Grid.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString();
        }
        catch
        {
            throw;
        }
    }
    private DataTable createUpdateTable()
    {
        DataTable dtupdateflag = new DataTable();
        dtupdateflag.Columns.Add("COMP_CODE", typeof(string));
        dtupdateflag.Columns.Add("BRANCH_CODE", typeof(string));
        dtupdateflag.Columns.Add("BUSINESS_TYPE", typeof(string));
        dtupdateflag.Columns.Add("PRODUCT_TYPE", typeof(string));
        dtupdateflag.Columns.Add("ORDER_CAT", typeof(string));
        dtupdateflag.Columns.Add("ORDER_TYPE", typeof(string));
        dtupdateflag.Columns.Add("ORDER_NO", typeof(string));
        dtupdateflag.Columns.Add("PI_TYPE", typeof(string));
        dtupdateflag.Columns.Add("PI_NO", typeof(string));
        dtupdateflag.Columns.Add("ARTICAL_CODE", typeof(string));
        dtupdateflag.Columns.Add("SHADE_CODE", typeof(string));
        dtupdateflag.Columns.Add("status", typeof(bool));


        return dtupdateflag;
    }
    protected override void OnInit(EventArgs e)
    {
        ddlprtycode.AutoPostBack = false;
        base.OnInit(e);
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        DataTable dtupdateflag = createUpdateTable();

        foreach (GridViewRow gvr in OrderCapt_Grid.Rows)
        {
            Label lblCOMP_CODE = (Label)gvr.FindControl("lblCOMP_CODE");
            Label lblBRANCH_CODE = (Label)gvr.FindControl("lblBRANCH_CODE");
            Label lblbusinesstype = (Label)gvr.FindControl("lblbusinesstype");
            Label lblproducttype = (Label)gvr.FindControl("lblproducttype");
            Label lblordercat = (Label)gvr.FindControl("lblordercat");
            Label lblorderTYPE = (Label)gvr.FindControl("lblorderTYPE");
            Label lblorderno = (Label)gvr.FindControl("lblorderno");
            Label lblpitype = (Label)gvr.FindControl("lblpitype");
            Label lblpinov = (Label)gvr.FindControl("lblpinov");
            Label lblARTICAL_CODE = (Label)gvr.FindControl("lblARTICAL_CODE");
            Label lblSHADE_CODE = (Label)gvr.FindControl("lblSHADE_CODE");
            CheckBox chk = (CheckBox)gvr.FindControl("CheckBox1");


            DataRow row;

            row = dtupdateflag.NewRow();
            row["COMP_CODE"] = lblCOMP_CODE.Text;
            row["BRANCH_CODE"] = lblBRANCH_CODE.Text;
            row["BUSINESS_TYPE"] = lblbusinesstype.Text;
            row["PRODUCT_TYPE"] = lblproducttype.Text;
            row["ORDER_CAT"] = lblordercat.Text;
            row["ORDER_TYPE"] = lblorderTYPE.Text;
            row["ORDER_NO"] = lblorderno.Text;
            row["PI_TYPE"] = lblpitype.Text;
            row["PI_NO"] = lblpinov.Text;
            row["ARTICAL_CODE"] = lblARTICAL_CODE.Text;
            row["SHADE_CODE"] = lblSHADE_CODE.Text;
            if (chk.Checked)
            {
                row["status"] = true;
            }
            else
            {
                row["status"] = false;
            }

            dtupdateflag.Rows.Add(row);


            bool Result = SaitexDL.Interface.Method.OD_CAPTURE_MST.UpdateFinalOrderflag(dtupdateflag);
            if (Result)
            {
                string msg = string.Empty;
                msg += "Final Order Confirmation  Flag Updated";
                Common.CommonFuction.ShowMessage(msg);

            }
            else
            {
                Common.CommonFuction.ShowMessage("Final Order Confirmation flag Updating Failed");
            }
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnforward_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/OrderDevelopment/Pages/Approvedproduction.aspx", true);
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
            lblMessage.Text = ex.ToString();
        }
    }
    protected void OrderCapt_Grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName != "Page" && e.CommandName != "")
        {
            GridViewRow gvr = (GridViewRow)((Control)e.CommandSource).NamingContainer;

            Label lblCOMP_CODE = (Label)gvr.FindControl("lblCOMP_CODE");
            Label lblBRANCH_CODE = (Label)gvr.FindControl("lblBRANCH_CODE");
            Label lblbusinesstype = (Label)gvr.FindControl("lblbusinesstype");
            Label lblproducttype = (Label)gvr.FindControl("lblproducttype");
            Label lblordercat = (Label)gvr.FindControl("lblordercat");
            Label lblorderTYPE = (Label)gvr.FindControl("lblorderTYPE");
            Label lblorderno = (Label)gvr.FindControl("lblorderno");
            Label lblpitype = (Label)gvr.FindControl("lblpitype");
            Label lblpinov = (Label)gvr.FindControl("lblpinov");
            Label lblARTICAL_CODE = (Label)gvr.FindControl("lblARTICAL_CODE");
            Label lblSHADE_CODE = (Label)gvr.FindControl("lblSHADE_CODE");
            Label lblPROS_ROUTE_CODE = (Label)gvr.FindControl("lblPROS_ROUTE_CODE");
            Label lblBOM_FLAG = (Label)gvr.FindControl("lblBOM_FLAG");

            Label lblCOST_PRICE_FLAG = (Label)gvr.FindControl("lblCOST_PRICE_FLAG");
            Label lblPROCESS_ROUTE_FLAG = (Label)gvr.FindControl("lblPROCESS_ROUTE_FLAG");

            if (e.CommandName == "viewbom")
            {
                try
                {
                    string URL = "BOM.aspx";
                    URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblbusinesstype.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblproducttype.Text;
                    URL = URL + "&ORDER_CAT=" + lblordercat.Text;
                    URL = URL + "&ORDER_TYPE=" + lblorderTYPE.Text;
                    URL = URL + "&ORDER_NO=" + lblorderno.Text;
                    URL = URL + "&PI_TYPE=" + lblpitype.Text;
                    URL = URL + "&PI_NO=" + lblpinov.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                    URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;
                    URL = URL + "&BOM_FLAG=" + lblBOM_FLAG.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);


                }
                catch
                {

                }
            }
            else if (e.CommandName == "viewcost")
            {
                try
                {

                    string URL = "COST.aspx";
                    URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblbusinesstype.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblproducttype.Text;
                    URL = URL + "&ORDER_CAT=" + lblordercat.Text;
                    URL = URL + "&ORDER_TYPE=" + lblorderTYPE.Text;
                    URL = URL + "&ORDER_NO=" + lblorderno.Text;
                    URL = URL + "&PI_TYPE=" + lblpitype.Text;
                    URL = URL + "&PI_NO=" + lblpinov.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                    URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;
                    URL = URL + "&COST_PRICE_FLAG=" + lblCOST_PRICE_FLAG.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

                }
                catch
                {

                }
            }
            else if (e.CommandName == "viewprroot")
            {
                string URL = "ProcessRoute.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&BUSINESS_TYPE=" + lblbusinesstype.Text;
                URL = URL + "&PRODUCT_TYPE=" + lblproducttype.Text;
                URL = URL + "&ORDER_CAT=" + lblordercat.Text;
                URL = URL + "&ORDER_TYPE=" + lblorderTYPE.Text;
                URL = URL + "&ORDER_NO=" + lblorderno.Text;
                URL = URL + "&PI_TYPE=" + lblpitype.Text;
                URL = URL + "&PI_NO=" + lblpinov.Text;
                URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;
                URL = URL + "&PROS_ROUTE_CODE=" + lblPROS_ROUTE_CODE.Text;
                URL = URL + "&PROCESS_ROUTE_FLAG=" + lblPROCESS_ROUTE_FLAG.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            else if (e.CommandName == "Refresh")
            {
                string URL = "ProcessRoute.aspx";
                URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                URL = URL + "&BUSINESS_TYPE=" + lblbusinesstype.Text;
                URL = URL + "&PRODUCT_TYPE=" + lblproducttype.Text;
                URL = URL + "&ORDER_CAT=" + lblordercat.Text;
                URL = URL + "&ORDER_TYPE=" + lblorderTYPE.Text;
                URL = URL + "&ORDER_NO=" + lblorderno.Text;
                URL = URL + "&PI_TYPE=" + lblpitype.Text;
                URL = URL + "&PI_NO=" + lblpinov.Text;
                URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;
                URL = URL + "&PROS_ROUTE_CODE=" + lblPROS_ROUTE_CODE.Text;
                URL = URL + "&PROCESS_ROUTE_FLAG=" + lblPROCESS_ROUTE_FLAG.Text;

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

            }
            else if (e.CommandName == "viewCR")
            {
                try
                {
                    string URL = "ViewCROrderAdj.aspx";
                    URL = URL + "?COMP_CODE=" + lblCOMP_CODE.Text;
                    URL = URL + "&BRANCH_CODE=" + lblBRANCH_CODE.Text;
                    URL = URL + "&BUSINESS_TYPE=" + lblbusinesstype.Text;
                    URL = URL + "&PRODUCT_TYPE=" + lblproducttype.Text;
                    URL = URL + "&ORDER_CAT=" + lblordercat.Text;
                    URL = URL + "&ORDER_TYPE=" + lblorderTYPE.Text;
                    URL = URL + "&ORDER_NO=" + lblorderno.Text;
                    URL = URL + "&PI_TYPE=" + lblpitype.Text;
                    URL = URL + "&PI_NO=" + lblpinov.Text;
                    URL = URL + "&ARTICAL_CODE=" + lblARTICAL_CODE.Text;
                    URL = URL + "&SHADE_CODE=" + lblSHADE_CODE.Text;

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);

                }
                catch
                {

                }
            }
        }
    }
    protected void OrderCapt_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                Label lblPROCESS_ROUTE_FLAG = ((Label)e.Row.FindControl("lblPROCESS_ROUTE_FLAG"));
                Label lblBOM_FLAG = ((Label)e.Row.FindControl("lblBOM_FLAG"));
                Label lblCOST_PRICE_FLAG = ((Label)e.Row.FindControl("lblCOST_PRICE_FLAG"));

                LinkButton lnkbom = ((LinkButton)e.Row.FindControl("linkbom"));
                LinkButton lnkcost = ((LinkButton)e.Row.FindControl("Linkcost"));
                LinkButton Linkprocessroot = ((LinkButton)e.Row.FindControl("Linkprocessroot"));

                CheckBox chk = ((CheckBox)e.Row.FindControl("CheckBox1"));
                if (lblPROCESS_ROUTE_FLAG.Text.Equals("1"))
                {
                    Linkprocessroot.Text = "Approved";
                    Linkprocessroot.ForeColor = System.Drawing.Color.Green;

                }
                else if (lblPROCESS_ROUTE_FLAG.Text.Equals("0"))
                {
                    Linkprocessroot.Text = "Un-Approved";
                    Linkprocessroot.ForeColor = System.Drawing.Color.Red;
                }

                if (lblBOM_FLAG.Text.Equals("1"))
                {
                    lnkbom.Text = "Approved";
                    lnkbom.ForeColor = System.Drawing.Color.Green;

                }
                else if (lblBOM_FLAG.Text.Equals("0"))
                {
                    lnkbom.Text = "Un-Approved";
                    lnkbom.ForeColor = System.Drawing.Color.Red;
                }

                if (lblCOST_PRICE_FLAG.Text.Equals("1"))
                {
                    lnkcost.Text = "Approved";
                    lnkcost.ForeColor = System.Drawing.Color.Green;
                }
                else if (lblCOST_PRICE_FLAG.Text.Equals("0"))
                {
                    lnkcost.Text = "Un-Approved";
                    lnkcost.ForeColor = System.Drawing.Color.Red;

                }
                if (lblPROCESS_ROUTE_FLAG.Text.Equals("1") && (lblBOM_FLAG.Text.Equals("1") && (lblCOST_PRICE_FLAG.Text.Equals("1"))))
                {
                    chk.Enabled = true;
                    //chk.BorderColor   = System.Drawing.Color.Green;
                    chk.BackColor = System.Drawing.Color.Green;



                }
                else
                {

                    chk.Enabled = false;
                    //chk.BorderColor = System.Drawing.Color.Red;
                    chk.BackColor = System.Drawing.Color.Red;

                }


                Label lblFINAL_ORDER_CONF_CLAG = (Label)e.Row.FindControl("lblFINAL_ORDER_CONF_CLAG");
                if (lblFINAL_ORDER_CONF_CLAG.Text.Equals("1"))
                {
                    chk.Checked = true;

                }
                else if (lblFINAL_ORDER_CONF_CLAG.Text.Equals("0"))
                {
                    chk.Checked = false;

                }

            }

        }
        catch
        {
            throw;

        }

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            GetOrderCapt_GridData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }
    protected void OrderCapt_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            OrderCapt_Grid.PageIndex = e.NewPageIndex;
            ViewInGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Grid View Paging.\r\nSee error log for detail."));
            lblMessage.Text = ex.ToString();
        }
    }
}
