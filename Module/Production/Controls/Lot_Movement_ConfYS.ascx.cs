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

public partial class Module_Production_Controls_Lot_Movement_ConfYS : System.Web.UI.UserControl
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
            DataTable dtLotConf = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetLot_Mov_for_Confirmation1(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE);
            grdLOT_MOV_CONF.DataSource = dtLotConf;
            grdLOT_MOV_CONF.DataBind();
            lblTotalRecord.Text = grdLOT_MOV_CONF.Rows.Count.ToString();
        }
        catch (Exception)
        {
            
            throw;
        }
    }
    protected void grdLOT_MOV_CONF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Label lblToDept = (Label)e.Row.FindControl("lblToDept");
            //    CheckBox chkConf = (CheckBox)e.Row.FindControl("chkConf");

            //    if (lblToDept.Text.Trim().Equals(oUserLoginDetail.VC_DEPARTMENTCODE.Trim()))
            //    {
            //        e.Row.BackColor = System.Drawing.Color.LightGreen;
            //        chkConf.Enabled = true;
            //        chkConf.Checked = false;
            //    }
            //    else
            //    {
            //        e.Row.BackColor = System.Drawing.Color.Red;
            //        chkConf.Enabled = false;
            //        chkConf.Checked = false;
            //    }
            //}
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
                foreach (GridViewRow GVR in grdLOT_MOV_CONF.Rows)
                {
                    CheckBox chkConf = (CheckBox)GVR.FindControl("chkConf");
                    CheckBox chkReject = (CheckBox)GVR.FindControl("chkReject");
                    if (chkConf.Checked == true || chkReject.Checked == true)
                    {
                        Label lblDeptCode = (Label)GVR.FindControl("lblDeptCode");
                        Label lblEntryNo = (Label)GVR.FindControl("lblEntryNo");
                        Label lblEntryDate = (Label)GVR.FindControl("lblEntryDate");
                        Label lblLotNo = (Label)GVR.FindControl("lblLotNo");
                        Label lblEntryType = (Label)GVR.FindControl("lblEntryType");

                        DataRow dr = dtLotConf.NewRow();
                        dr["DEPT_CODE"] = lblDeptCode.Text.Trim();
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
            int iResult = SaitexBL.Interface.Method.OD_CAPTURE_MST.CONF_Lot_Mov1(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.VC_DEPARTMENTCODE, oUserLoginDetail.UserCode, dtLotConf, out msg);
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
            dtLotConf.Columns.Add("DEPT_CODE", typeof(string));
            dtLotConf.Columns.Add("ENTRY_TYPE", typeof(string));
            dtLotConf.Columns.Add("ENTRY_NO", typeof(string));
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
        InitialisePage();
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

            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in leaving page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
}
