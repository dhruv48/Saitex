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
public partial class Module_Inventory_Controls_jurnalmaterial : System.Web.UI.UserControl
{
   
    DateTime FromDate;
    DateTime ToDate;
    string  from = string.Empty ;
    string  to = string.Empty ;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    SaitexDM.Common.DataModel.TX_ITEM_IR_MST oTX_ITEM_IR_MST;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if(!IsPostBack)
        {
            bindddltrntype();
            bindddlprty();
            bindgridrecevingmaterial();
        }
    }
    private void bindgridrecevingmaterial()
    {
        try
        {
            string PRTY_CODE = string.Empty;
            string TRN_TYPE = string.Empty;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            if (ddlpartycode.SelectedValue.ToString() != null && ddlpartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlpartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }


            if (ddltrn.SelectedValue.ToString() != null && ddltrn.SelectedValue.ToString() != string.Empty)
            {
                TRN_TYPE = ddltrn.SelectedValue.ToString();
            }
            else
            {
                TRN_TYPE = string.Empty;
            }
            if (txtformdate.Text.Trim() != string.Empty && txtTodate.Text.Trim().ToString() != string.Empty)
            {
                FromDate = DateTime.Parse(txtformdate.Text.Trim().ToString());
                ToDate = DateTime.Parse(txtTodate.Text.Trim().ToString());
                if (ToDate <= Edate && Sdate <= FromDate)
                {
                    if (FromDate < ToDate)
                    {
                        from = txtformdate.Text.Trim().ToString();
                        to = txtTodate.Text.Trim().ToString();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Sir ! From Date Should be less then To Date");

                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Sir ! To Date Should be in Financial Year");
                }
            
            }
            else
            {
                if (txtformdate.Text.Trim() != string.Empty)
                {
                    FromDate = DateTime.Parse(txtformdate.Text.Trim().ToString());
                    if (FromDate >= Sdate && Edate >= FromDate)
                    {
                        from = txtformdate.Text.Trim().ToString();
                                         
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Sir ! From Date  Should be in Financial Year");
                      
                    }
                }
                else
                {

                    from = Sdate.ToShortDateString().ToString();
                }

                if (txtTodate.Text.Trim().ToString() != string.Empty)
                {
                    ToDate = DateTime.Parse(txtTodate.Text.Trim().ToString());

                    if (ToDate >= Sdate || Edate >= ToDate)
                    {
                       
                       to = txtTodate.Text.Trim().ToString();
                        
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Sir !To Date Should be in Financial Year");
                    }

                }
                else
                {
                    to = Edate.ToShortDateString().ToString();

                }
            }
            
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.getrecevingmaterial(PRTY_CODE, TRN_TYPE, from, to);
            ViewState["grid2data"] = dt;
            if (dt != null && dt.Rows.Count > 0)
            {
                Gridrecevingmaterial.DataSource = dt;
                Gridrecevingmaterial.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                Gridrecevingmaterial.DataSource = dt;
                Gridrecevingmaterial.DataBind();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
        }
        catch
        {
            throw;
        }
    } 
    protected void Gridrecevingmaterial_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {

            string stritemtrn = string.Empty;
            string stritemtrntype = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lbltrnnumb = (Label)e.Row.FindControl("lbltrnnumber");
                stritemtrn = lbltrnnumb.Text.Trim();
                Label lbltrntype = (Label)e.Row.FindControl("lbltrntype");

                stritemtrntype = lbltrntype.Text.Trim();
                
                
                GridView Grid2 = (GridView)e.Row.FindControl("Grid2");

                oTX_ITEM_IR_MST = new SaitexDM.Common.DataModel.TX_ITEM_IR_MST();
                //oTX_ITEM_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                //oTX_ITEM_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_ITEM_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_ITEM_IR_MST.TRN_NUMB = int.Parse(stritemtrn);
                oTX_ITEM_IR_MST.TRN_TYPE = stritemtrntype.ToString();
                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetItemTrn(oTX_ITEM_IR_MST);

                if (dt!= null)
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "TRN_NUMB='" + stritemtrn + "'";
                    if (dv.Count > 0)
                    {
                        Grid2.DataSource = dv;
                        Grid2.DataBind();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Bound Event.\r\nSee error log for detail."));
        }
    }
    private void bindddlprty()
    {

        try
        {

            ddlpartycode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlpartycode.DataTextField = "PRTY_NAME";
                ddlpartycode.DataValueField = "PRTY_CODE";
                ddlpartycode.DataSource = dt;
                ddlpartycode.DataBind();

            }
           
            ddlpartycode.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }
    }
    private void bindddltrntype()
    {
         
        try 
        {

            ddltrn.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddltrn.DataTextField = "TRN_DESC";
                ddltrn.DataValueField = "TRN_TYPE";
                ddltrn.DataSource = dt;
                ddltrn.DataBind();

            }
            ddltrn.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }
    }  
    protected void ddlpartycode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindgridrecevingmaterial();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }
    protected void txtformdate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            bindgridrecevingmaterial();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    protected void txtTodate_TextChanged(object sender, EventArgs e)
    {
        try
        {   

            bindgridrecevingmaterial();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }
    protected void ddltrn_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            bindgridrecevingmaterial();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
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
    protected void Gridrecevingmaterial_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Gridrecevingmaterial.PageIndex = e.NewPageIndex;
        bindgridrecevingmaterial();
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
        DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);
        from = Sdate.ToShortDateString().ToString();
        to = Edate.ToShortDateString().ToString();
        
        string  FROMDATE = string .Empty ;
        string  TODATE = string.Empty ;
        string PRTY_CODE = string.Empty;
        string TRN_TYPE = string.Empty;

        try
        {

            DataTable myDataTable = new DataTable();
            //DataColumn myDataColumn;

            myDataTable.Columns.Add("FROMDATE", typeof(string));
            myDataTable.Columns.Add("TODATE", typeof(string));
            myDataTable.Columns.Add("PRTY_CODE", typeof(string));
            myDataTable.Columns.Add("TRN_TYPE", typeof(string));
            DataRow row;
            row = myDataTable.NewRow();

            if (txtformdate.Text.Trim().ToString() == string.Empty)
            {
                row["FROMDATE"] = Sdate.ToShortDateString().ToString();
            }
            else
            {
                row["FROMDATE"] = txtformdate.Text.Trim().ToString();
            }

            if (txtTodate.Text.Trim().ToString() == string.Empty)
            {
                row["TODATE"] = Edate.ToShortDateString().ToString();
            }
            else
            {
                row["TODATE"] = txtTodate.Text.Trim().ToString();
            }
            
            row["PRTY_CODE"] = ddlpartycode.SelectedValue.ToString();
            row["TRN_TYPE"] = ddltrn.SelectedValue.ToString();

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;
            //Response.Redirect("~/Module/Inventory/Reports/MaterialIssueQueryReport.aspx", false);
            string URL = "../Reports/MaterialIssueQueryReport.aspx";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }                        
    }
}

