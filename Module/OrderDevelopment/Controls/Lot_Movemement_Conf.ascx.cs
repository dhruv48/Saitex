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

public partial class Module_OrderDevelopment_Controls_Lot_Movemement_Conf : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            DataTable dtLotConf = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetLot_Mov_for_Confirmation(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE);

            grdLOT_MOV_CONF.DataSource = dtLotConf;
            grdLOT_MOV_CONF.DataBind();

            lblTotalRecord.Text = dtLotConf.Rows.Count.ToString();
        }
        catch
        {
            throw;
        }
    }

    protected void grdLOT_MOV_CONF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblFromDept = (Label)e.Row.FindControl("lblFromDept");
                CheckBox chkConf = (CheckBox)e.Row.FindControl("chkConf");

                if (lblFromDept.Text.Trim().Equals(oUserLoginDetail.VC_DEPARTMENTCODE.Trim()))
                {
                    e.Row.BackColor = System.Drawing.Color.Green;
                    chkConf.Enabled = false;
                    chkConf.Checked = false;
                }
                else
                {
                    e.Row.BackColor = System.Drawing.Color.Red;
                    chkConf.Enabled = true;
                    chkConf.Checked = false;
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in Data Loading.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dtLotConf = CreateDataTable();
            if (grdLOT_MOV_CONF.Rows.Count > 0)
            {
                foreach (GridViewRow grdRow in grdLOT_MOV_CONF.Rows)
                {
                    CheckBox chkConf = (CheckBox)grdRow.FindControl("chkConf");
                    CheckBox chkReject = (CheckBox)grdRow.FindControl("chkReject");

                    if (chkConf.Checked == true || chkReject.Checked == true)
                    {
                        Label lblEntryNo = (Label)grdRow.FindControl("lblEntryNo");
                        Label lblEntryDate = (Label)grdRow.FindControl("lblEntryDate");
                        Label lblLotNo = (Label)grdRow.FindControl("lblLotNo");
                        Label lblEntryType = (Label)grdRow.FindControl("lblEntryType");

                        DataRow dr = dtLotConf.NewRow();
                        dr["ENTRY_TYPE"] = lblEntryType.Text.Trim();
                        dr["ENTRY_NO"] = int.Parse(lblEntryNo.Text.Trim());
                        dr["ENTRY_DATE"] = DateTime.Parse(lblEntryDate.Text.Trim());
                        dr["LOT_NUMBER"] = lblLotNo.Text.Trim();

                        if (chkConf.Checked)
                        {
                            dr["CONFIRMED"] = "1";
                        }
                        else if (chkReject.Checked)
                        {
                            dr["CONFIRMED"] = "2";
                        }

                        dtLotConf.Rows.Add(dr);
                    }
                }
            }
            string msg = string.Empty;
            int iResult = SaitexBL.Interface.Method.OD_CAPTURE_MST.CONF_Lot_Mov(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE, oUserLoginDetail.UserCode, dtLotConf, out msg); ;

            CommonFuction.ShowMessage(msg);
            InitialisePage();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in data saving.\r\nSee error log for detail."));
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtLotConf = new DataTable();
            dtLotConf.Columns.Add("ENTRY_TYPE", typeof(string));
            dtLotConf.Columns.Add("ENTRY_NO", typeof(int));
            dtLotConf.Columns.Add("ENTRY_DATE", typeof(DateTime));
            dtLotConf.Columns.Add("LOT_NUMBER", typeof(string));
            dtLotConf.Columns.Add("CONFIRMED", typeof(string));
            return dtLotConf;
        }
        catch
        {
            throw;
        }
    }

    protected void chkConf_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow grdRow = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
        CheckBox chkConf = (CheckBox)grdRow.FindControl("chkConf");
        CheckBox chkReject = (CheckBox)grdRow.FindControl("chkReject");

        if (chkConf.Checked == true && chkReject.Checked == true)
        {
            CommonFuction.ShowMessage("Please uncheck reject checkbox to confirm that lot movement.");
        }
    }

    protected void chkReject_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow grdRow = (GridViewRow)((DataControlFieldCell)((CheckBox)sender).Parent).Parent;
        CheckBox chkConf = (CheckBox)grdRow.FindControl("chkConf");
        CheckBox chkReject = (CheckBox)grdRow.FindControl("chkReject");

        if (chkConf.Checked == true && chkReject.Checked == true)
        {
            CommonFuction.ShowMessage("Please uncheck confirm checkbox to reject that lot movement.");
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

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}
