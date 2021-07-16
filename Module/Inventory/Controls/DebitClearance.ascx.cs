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

public partial class Module_Inventory_Controls_DebitClearance : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    TX_DEBIT_MST oTX_DEBIT_MST;
    private static string strType = string.Empty;
    private static string strTypeName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = Session["LoginDetail"] as SaitexDM.Common.DataModel.UserLoginDetail;
            if (!IsPostBack)
            {
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
                {
                    strType = Request.QueryString["Type"].ToString().Trim();
                }
                InitiallisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitiallisePage()
    {
        try
        {
            tdUpdate.Visible = true;
            tdSave.Visible = false;

            if (strType == "D")
            {
                lblHeading.Text = "Material Debit Note Clearance";
                strTypeName = "DEBIT NOTE";
            }
            else
            {
                lblHeading.Text = "Material Credit Note Clearance";
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

            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetUnClearedDrCrNotes(oTX_DEBIT_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("BillID"))
                    dt.Columns.Add("BillID", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    dr["BillID"] = dr["NOTE_TYPE"].ToString() + "_" + dr["ADVICE_NO"].ToString() + "_" + dr["COMP_CODE"].ToString() + "_" + dr["BRANCH_CODE"].ToString() + "_" + dr["BILL_YEAR"].ToString();
                }

                grdDrCrNoteClearance.DataSource = dt;
                grdDrCrNoteClearance.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                if (strType == "D")
                {
                    CommonFuction.ShowMessage("No Debit Note found for Clearance..");
                }
                else
                {
                    CommonFuction.ShowMessage("No Credit Note found for Clearance..");
                }
                lblTotalRecord.Text = "0";
            }
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
            DataTable dtBillClearence = CreateDataTable();
            for (int i = 0; i < grdDrCrNoteClearance.Rows.Count; i++)
            {
                TextBox txtremarks = grdDrCrNoteClearance.Rows[i].FindControl("txtComment") as TextBox;
                Label lblCombinedid = grdDrCrNoteClearance.Rows[i].FindControl("lblCombinedid") as Label;
                CheckBox chk = grdDrCrNoteClearance.Rows[i].FindControl("chkConfirmed") as CheckBox;

                if (chk.Checked == true)
                {
                    string ID = lblCombinedid.ToolTip;
                    string[] IDs = ID.Split('_');
                    string BILLTYPE = IDs[0].ToString();
                    string BILL_NUMB = IDs[1].ToString();

                    DataRow dr = dtBillClearence.NewRow();
                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["NOTE_TYPE"] = BILLTYPE;
                    dr["ADVICE_NO"] = BILL_NUMB;
                    dr["PurClrUser"] = oUserLoginDetail.UserCode;
                    dr["PurClrDate"] = System.DateTime.Now.Date;
                    dr["Remarks"] = txtremarks.Text.Trim();
                    dtBillClearence.Rows.Add(dr);
                }
            }
            int iResult = SaitexBL.Interface.Method.TX_DEBIT_MST.Update_NoteClearForApproval(oUserLoginDetail.UserCode, dtBillClearence);
            if (iResult > 0)
            {
                lblMode.Text = "You are in Find Mode";
                if (strType == "D")
                {
                    CommonFuction.ShowMessage("Debit Note Cleared Successfully..");
                }
                else
                {
                    CommonFuction.ShowMessage("Credit Note Cleared Successfully..");
                }
                BindDrCrNoteGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Bill Clearence.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Print.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //Response.Redirect("./DebitNote.aspx?Type=D", false);
            if (strType == "D")
            {
                Response.Redirect("./DrCrClearance.aspx?Type=D", false);
            }
            else
            {
                Response.Redirect("./DrCrClearance.aspx?Type=C", false);
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
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(@"Problem in leaving page.\r\nSee error log for detail.");
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Getting Help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdDrCrNoteClearance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string str = string.Empty;
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCombinedID = (Label)e.Row.FindControl("lblCombinedid");
                string ID = lblCombinedID.ToolTip;
                string[] IDs = ID.Split('_');
                string BillType = IDs[0].ToString();
                string BillNumb = IDs[1].ToString();
                string Compcode = IDs[2].ToString();
                string BranchCode = IDs[3].ToString();
                int Year = int.Parse(IDs[4].ToString());
                DataTable dt = GetTransactionDetail(Compcode, BranchCode, BillType, BillNumb, Year);
                GridView grdTrndetails = (GridView)e.Row.FindControl("grdTrndetails");

                if (dt != null && dt.Rows.Count > 0)
                {
                    grdTrndetails.DataSource = dt;
                    grdTrndetails.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Row Data Bound.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetTransactionDetail(string Compcode, string BranchCode, string BillType, string BillNumb, int Year)
    {
        try
        {
            oTX_DEBIT_MST = new TX_DEBIT_MST();
            oTX_DEBIT_MST.COMP_CODE = Compcode;
            oTX_DEBIT_MST.BRANCH_CODE = BranchCode;
            oTX_DEBIT_MST.NOTE_TYPE = BillType;
            oTX_DEBIT_MST.ADVICE_NO = BillNumb;
            oTX_DEBIT_MST.BILL_YEAR = Year;
            DataTable dt = SaitexBL.Interface.Method.TX_DEBIT_MST.GetTrasactionByMst(oTX_DEBIT_MST);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable()
    {
        DataTable dtBillClearence = new DataTable();
        dtBillClearence.Columns.Add("YEAR", typeof(int));
        dtBillClearence.Columns.Add("COMP_CODE", typeof(string));
        dtBillClearence.Columns.Add("BRANCH_CODE", typeof(string));
        dtBillClearence.Columns.Add("NOTE_TYPE", typeof(string));
        dtBillClearence.Columns.Add("ADVICE_NO", typeof(string));
        dtBillClearence.Columns.Add("PurClrUser", typeof(string));
        dtBillClearence.Columns.Add("PurClrDate", typeof(DateTime));
        dtBillClearence.Columns.Add("Remarks", typeof(string));
        return dtBillClearence;
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
}
