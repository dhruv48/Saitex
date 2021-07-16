using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using Common;
using System.Web.UI.WebControls;

public partial class Module_Fiber_Controls_PO_Approval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                BindFiberPoAppGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    /// <about this method>
    /// made by abhishek 28-06-2012
    /// This Methos Is Used To Bind the main Grid for approval
    /// the data layer and business layer for this method is TX_FIBER_PO_CREDIT
    /// </summary>
    private void BindFiberPoAppGrid()
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_NEW_PO_CREDIT.GetDataForApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.Username;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                grd_fib_po_app.DataSource = dt;
                grd_fib_po_app.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No PO for approval";
                grd_fib_po_app.DataSource = null;
                grd_fib_po_app.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No PO for approval");
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private DataTable CreateDataTable()
    {
        DataTable dtPODetail = new DataTable();
        //dtPODetail.Columns.Add("YEAR", typeof(int));
        dtPODetail.Columns.Add("COMP_CODE", typeof(string));
        dtPODetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtPODetail.Columns.Add("PO_TYPE", typeof(string));
        dtPODetail.Columns.Add("PO_NUMB", typeof(int));
        dtPODetail.Columns.Add("PO_NATURE", typeof(string));
        dtPODetail.Columns.Add("PARTY_DATA", typeof(string));
        dtPODetail.Columns.Add("DEL_DATE", typeof(DateTime));
        dtPODetail.Columns.Add("CONF_FLAG", typeof(string));
        dtPODetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtPODetail.Columns.Add("CONF_BY", typeof(string));
        return dtPODetail;
    }
    
    protected void grd_fib_po_app_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            BindFiberPoAppGrid();

            grd_fib_po_app.PageIndex = e.NewPageIndex;
            grd_fib_po_app.DataBind();
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
            string msg = string.Empty;

            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            DataTable dtPODetail = CreateDataTable();
            int totalRows = grd_fib_po_app.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grd_fib_po_app.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblPO_NUMB = (Label)thisGridViewRow.FindControl("lblPO_NUMB");
                    Label lblPO_type = (Label)thisGridViewRow.FindControl("lblPO_type");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    //TextBox ConfirmDate = (TextBox)thisGridViewRow.FindControl("txtConfirmDate");
                    //TextBox ConfirmBy = (TextBox)thisGridViewRow.FindControl("txtConfirmBy");

                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtPODetail.NewRow();

                        //dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["PO_TYPE"] = lblPO_type.Text.Trim();
                        dr["PO_NUMB"] = int.Parse(lblPO_NUMB.Text.Trim());
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "1";
                        dtPODetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.TX_FIBER_NEW_PO_CREDIT.Update_POForApproval(oUserLoginDetail.UserCode, dtPODetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("PO approved Successfully.");
                BindFiberPoAppGrid();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Confirm.\r\nSee error log for detail."));
        }
    }
    
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Pages/PO_Approval.aspx", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    
    protected void grd_fib_po_app_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    /// <about this method>
    /// made by abhishek 28-06-2012
    /// This Methos Is Used To Bind the nested Grid for approval
    /// that will show the TRN Data for specif Po Number
    /// the data layer and business layer for this method is TX_FIBER_PO_CREDIT
    /// </summary>
    protected void grd_fib_po_app_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridView grd_fib_po_app = (GridView)e.Row.FindControl("grd_fib_po_app");
                int po_no = int.Parse(e.Row.Cells[1].Text);
                string po_type = e.Row.Cells[2].Text;
                

                //Label lblPO_NUMB = (Label)grdRow.FindControl("lblPO_NUMB");
                //Label lblPO_type = (Label)grdRow.FindControl("lblPO_type");

                SaitexDM.Common.DataModel.TX_FIBER_NEW_PO_CREDIT oTX_FIBER_PO_CREDIT = new SaitexDM.Common.DataModel.TX_FIBER_NEW_PO_CREDIT();
                oTX_FIBER_PO_CREDIT.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oTX_FIBER_PO_CREDIT.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FIBER_PO_CREDIT.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FIBER_PO_CREDIT.PO_NUMB = int.Parse(po_no.ToString());
                oTX_FIBER_PO_CREDIT.PO_TYPE = po_type;

                DataTable dt = SaitexBL.Interface.Method.TX_FIBER_NEW_PO_CREDIT.GetChildGrdData(oTX_FIBER_PO_CREDIT);

                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView grdchild = (GridView)e.Row.FindControl("grdchild");
                    //GridView grdchild = (GridView)grdchild.FindControl("grdchild");
                    grdchild.DataSource = dt;
                    grdchild.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
        }
    }
}
