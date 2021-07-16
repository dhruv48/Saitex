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

public partial class Module_FA_Controls_BillReceive : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_BILL_MST oTX_BILL_MST, oTX_BILL_MST1;
    private static DataTable dtBillReceived, dtBillTrn;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Bill Received Successfully Done!');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
            if (!IsPostBack)
            {
                InitialisePage();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading..\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Update";
            tdUpdate.Visible = true;
            BindBillDTLGrid();
        }
        catch
        {
            throw;
        }
    }

    private void BindBillDTLGrid()
    {
        try
        {
            oTX_BILL_MST = new SaitexDM.Common.DataModel.TX_BILL_MST();

            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetBillForReceving(oTX_BILL_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("RECEIVED_DATE"))
                    dt.Columns.Add("RECEIVED_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("RECEIVED_BY"))
                    dt.Columns.Add("RECEIVED_BY", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ReceivedBy = dr["RECEIVED_BY"].ToString();
                    if (ReceivedBy == "")
                        dr["RECEIVED_BY"] = oUserLoginDetail.Username;

                    dr["RECEIVED_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                }
                grdBillReceive.DataSource = dt;
                grdBillReceive.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "Sorry Dear ! There is no Bill Receiving from Purchase Department..";
                grdBillReceive.DataSource = null;
                grdBillReceive.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("Sorry Dear ! There is no Bill Receiving from Purchase Department..");
            }
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void grdBillReceive_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdBillReceive.PageIndex = e.NewPageIndex;
            BindBillDTLGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("/Saitex/Module/FA/Pages/OnlineBillReceive.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bill Receiving..\r\nSee error log for detail."));
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
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            dtBillReceived = new DataTable();
            dtBillReceived.Columns.Add("COMP_CODE", typeof(string));
            dtBillReceived.Columns.Add("BRANCH_CODE", typeof(string));
            dtBillReceived.Columns.Add("BILL_YEAR", typeof(int));
            dtBillReceived.Columns.Add("BILL_TYPE", typeof(string));
            dtBillReceived.Columns.Add("BILL_NUMB", typeof(string));
            dtBillReceived.Columns.Add("RECEIVED_DATE", typeof(DateTime));
            dtBillReceived.Columns.Add("RECEIVED_BY", typeof(string));
            return dtBillReceived;
        }
        catch
        {
            throw;
        }
    }

    protected void grdBillReceive_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int iBillYear;
            string BillNumb = string.Empty;
            string strBillType = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBillYear = (Label)e.Row.FindControl("lblBillYear");
                iBillYear = Convert.ToInt32(lblBillYear.Text.Trim());
                Label lblBillType = (Label)e.Row.FindControl("lblBillType");
                strBillType = lblBillType.Text.Trim();
                Label lblBillNumb = (Label)e.Row.FindControl("lblBillNumb");
                BillNumb = lblBillNumb.Text.ToString();
                BindBillTrn(BillNumb, iBillYear, strBillType);
                GridView grdBillDTL = (GridView)e.Row.FindControl("grdBillDTL");
                if (dtBillTrn != null)
                {
                    grdBillDTL.DataSource = dtBillTrn;
                    grdBillDTL.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Row Bound Event.\r\nSee error log for detail."));
        }
    }

    private void BindBillTrn(string BillNumb, int iBillYear, string strBillType)
    {
        try
        {
            oTX_BILL_MST1 = new SaitexDM.Common.DataModel.TX_BILL_MST();
            if (dtBillTrn != null)
            {
                dtBillTrn = null;
            }
            oTX_BILL_MST1.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST1.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_BILL_MST1.BILL_YEAR = iBillYear;
            oTX_BILL_MST1.BILL_TYPE = strBillType;
            oTX_BILL_MST1.BILL_NUMB = BillNumb;

            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetBillTRNDetailByNumber(oTX_BILL_MST1);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        double dblPoint = 0;
                        int iTrnNo;

                        if (dtBillTrn == null)
                            CreateDataTableBillTrn();

                        DataRow dr = dtBillTrn.NewRow();
                        dr["TRN_TYPE"] = dv[iLoop]["TRN_TYPE"].ToString();
                        iTrnNo = 0;
                        int.TryParse(dv[iLoop]["TRN_NUMB"].ToString().Trim(), out iTrnNo);
                        dr["TRN_NUMB"] = iTrnNo;
                        dr["PRTY_CODE"] = dv[iLoop]["PRTY_CODE"].ToString().Trim();
                        dr["PRTY_NAME"] = dv[iLoop]["PRTY_NAME"].ToString().Trim();
                        dblPoint = 0;
                        double.TryParse(dv[iLoop]["QUALITY_POINT"].ToString().Trim(), out dblPoint);
                        dr["QUALITY_POINT"] = dblPoint;
                        dblPoint = 0;
                        double.TryParse(dv[iLoop]["DEL_POINT"].ToString().Trim(), out dblPoint);
                        dr["DEL_POINT"] = dblPoint;
                        dblPoint = 0;
                        double.TryParse(dv[iLoop]["PRICE_POINT"].ToString().Trim(), out dblPoint);
                        dr["PRICE_POINT"] = dblPoint;
                        dblPoint = 0;
                        double.TryParse(dv[iLoop]["SUPPORT_POINT"].ToString().Trim(), out dblPoint);
                        dr["SUPPORT_POINT"] = dblPoint;
                        dblPoint = 0;
                        double.TryParse(dv[iLoop]["TRN_AMT"].ToString().Trim(), out dblPoint);
                        dr["TRN_AMT"] = dblPoint;

                        dtBillTrn.Rows.Add(dr);
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void CreateDataTableBillTrn()
    {
        try
        {
            dtBillTrn = new DataTable();
            dtBillTrn.Columns.Add("TRN_TYPE", typeof(string));
            dtBillTrn.Columns.Add("TRN_NUMB", typeof(int));
            dtBillTrn.Columns.Add("PRTY_CODE", typeof(string));
            dtBillTrn.Columns.Add("PRTY_NAME", typeof(string));
            dtBillTrn.Columns.Add("QUALITY_POINT", typeof(double));
            dtBillTrn.Columns.Add("DEL_POINT", typeof(double));
            dtBillTrn.Columns.Add("PRICE_POINT", typeof(double));
            dtBillTrn.Columns.Add("SUPPORT_POINT", typeof(double));
            dtBillTrn.Columns.Add("TRN_AMT", typeof(double));
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

            dtBillReceived = CreateDataTable();
            int totalRows = grdBillReceive.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdBillReceive.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblBillYear = (Label)thisGridViewRow.FindControl("lblBillYear");
                    Label lblBillType = (Label)thisGridViewRow.FindControl("lblBillType");
                    Label lblBillNumb = (Label)thisGridViewRow.FindControl("lblBillNumb");
                    CheckBox chkReceived = (CheckBox)thisGridViewRow.FindControl("chkReceived");

                    if (chkReceived.Checked == true)
                    {
                        DataRow dr = dtBillReceived.NewRow();

                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["BILL_YEAR"] = Convert.ToInt32(lblBillYear.Text.Trim());
                        dr["BILL_TYPE"] = lblBillType.Text.Trim();
                        dr["BILL_NUMB"] = lblBillNumb.Text.Trim();
                        dr["RECEIVED_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                        dr["RECEIVED_BY"] = oUserLoginDetail.Username;
                        dtBillReceived.Rows.Add(dr);
                        chkReceived.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.TX_BILL_MST.UpdateBillReceiving(oUserLoginDetail.UserCode, dtBillReceived);
            if (iResult > 0)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./OnlineBillReceive.aspx?cId=U", false);
            }
            else
            {
                CommonFuction.ShowMessage("Problem in Bill Receiving..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
        }
    }
}