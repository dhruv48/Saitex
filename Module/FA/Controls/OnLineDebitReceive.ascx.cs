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

using SaitexDM.Common.DataModel;
using Common;

public partial class Module_FA_Controls_OnLineDebitReceive : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    TX_DEBIT_MST oTX_DEBIT_MST;
    private static string strType = string.Empty;
    private static string strTypeName = string.Empty;

    private static DataTable dtBillReceived, dtBillTrn;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
                {
                    strType = Request.QueryString["Type"].ToString().Trim();
                }
                InitialisePage();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page loading..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Update Mode";
            tdUpdate.Visible = true;

            if (strType == "D")
            {
                lblHeading.Text = "On Line Debit Note Receiving";
                strTypeName = "DEBIT NOTE";
            }
            else
            {
                lblHeading.Text = "On Line Credit Note Receiving";
                strTypeName = "CREDIT NOTE";
            }
            BindDrCrNoteGrid();
        }
        catch
        {
            throw;
        }
    }

    private void BindDrCrNoteGrid()
    {
        try
        {
            grdDrCrNoteClearance.DataSource = null;
            grdDrCrNoteClearance.DataBind();

            oTX_DEBIT_MST = new TX_DEBIT_MST();
            oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_DEBIT_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_DEBIT_MST.NOTE_TYPE = strTypeName;

            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetDrCrForOnLineReceving(oTX_DEBIT_MST);
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

                grdDrCrNoteClearance.DataSource = dt;
                grdDrCrNoteClearance.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                if (strType == "D")
                {
                    CommonFuction.ShowMessage("Sorry Dear ! There is no Debit Note found for Receiving..");
                }
                else
                {
                    CommonFuction.ShowMessage("Sorry Dear ! There is no Credit Note found for Receiving..");
                }
                lblTotalRecord.Text = "0";
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

    protected void grdDrCrNoteClearance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdDrCrNoteClearance.PageIndex = e.NewPageIndex;
            BindDrCrNoteGrid();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (strType == "D")
            {
                Response.Redirect("./ReceiveDrCrOnline.aspx?Type=D", false);
            }
            else
            {
                Response.Redirect("./ReceiveDrCrOnline.aspx?Type=C", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Refreshing Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
            lblMode.Text = ex.ToString();
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
            dtBillReceived.Columns.Add("NOTE_TYPE", typeof(string));
            dtBillReceived.Columns.Add("ADVICE_NO", typeof(string));
            dtBillReceived.Columns.Add("RECEIVED_DATE", typeof(DateTime));
            dtBillReceived.Columns.Add("RECEIVED_BY", typeof(string));
            return dtBillReceived;
        }
        catch
        {
            throw;
        }
    }

    protected void grdDrCrNoteClearance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            int iBillYear;
            string strAdviceNo = string.Empty;
            string strBillType = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBillYear = (Label)e.Row.FindControl("lblBillYear");
                iBillYear = Convert.ToInt32(lblBillYear.Text.Trim());
                Label lblBillType = (Label)e.Row.FindControl("lblBillType");
                strBillType = lblBillType.Text.Trim();
                Label lblBillNumb = (Label)e.Row.FindControl("lblBillNumb");
                strAdviceNo = lblBillNumb.Text.Trim();
                BindBillTrn(strAdviceNo, iBillYear, strBillType);
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
            lblMode.Text = ex.ToString();
        }
    }

    private void BindBillTrn(string strAdviceNo, int iBillYear, string strBillType)
    {
        try
        {
            oTX_DEBIT_MST = new TX_DEBIT_MST();
            if (dtBillTrn != null)
            {
                dtBillTrn = null;
            }
            oTX_DEBIT_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_DEBIT_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_DEBIT_MST.BILL_YEAR = iBillYear;
            oTX_DEBIT_MST.NOTE_TYPE = strBillType;
            oTX_DEBIT_MST.ADVICE_NO = strAdviceNo;
            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetTrasactionByMst(oTX_DEBIT_MST);
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
            dtBillReceived = CreateDataTable();
            int totalRows = grdDrCrNoteClearance.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = grdDrCrNoteClearance.Rows[r];
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
                        dr["NOTE_TYPE"] = lblBillType.Text.Trim();
                        dr["ADVICE_NO"] = lblBillNumb.Text.Trim();
                        dr["RECEIVED_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                        dr["RECEIVED_BY"] = oUserLoginDetail.Username;
                        dtBillReceived.Rows.Add(dr);
                        chkReceived.Checked = false;
                    }
                }
            }

            int iResult = SaitexBL.Interface.Method.TX_DEBIT_MST.UpdateDrCrReceiving(oUserLoginDetail.UserCode, dtBillReceived);
            if (iResult > 0)
            {
                lblMode.Text = "You are in Update Mode";
                if (strType == "D")
                {
                    CommonFuction.ShowMessage("Debit Note Received Successfully..");
                }
                else
                {
                    CommonFuction.ShowMessage("Credit Note Received Successfully..");
                }
                BindDrCrNoteGrid();
            }
            else
            {
                CommonFuction.ShowMessage("Problem in Bill Receiving..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}