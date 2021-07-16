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
using DBLibrary;
using Common;
using errorLog;
using Obout.Grid;

public partial class Module_Fiber_Controls_FiberPOClosing : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bindMaterialPOApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }

    private DataTable CreateDataTable()
    {
        DataTable dtPODetail = new DataTable();
        dtPODetail.Columns.Add("YEAR", typeof(int));
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
        dtPODetail.Columns.Add("REMARKS", typeof(string));
        return dtPODetail;
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtPODetail = CreateDataTable();

            for (int i = 0; i < grdPOMST.Rows.Count; i++)
            {
                //GridDataControlFieldCell cell = grdPOMST.Rows[i].Cells[7] as GridDataControlFieldCell;
                //CheckBox chk = cell.FindControl("ChkID") as CheckBox;
                CheckBox chk = (CheckBox)grdPOMST.Rows[i].FindControl("ChkID");

                if (chk.Checked == true)
                {
                    //GridDataControlFieldCell celltxt = grdPOMST.Rows[i].Cells[10] as GridDataControlFieldCell;
                    //TextBox textBox = celltxt.FindControl("txtID") as TextBox;
                    TextBox textBox = (TextBox)grdPOMST.Rows[i].FindControl("txtRemarks");

                    //string ID = chk.ToolTip;
                    //string[] IDs = ID.Split('_');

                    string PO_TYPE = grdPOMST.Rows[i].Cells[1].Text.ToString();
                    int PO_NUMB = int.Parse(grdPOMST.Rows[i].Cells[2].Text.ToString());

                    DataRow dr = dtPODetail.NewRow();

                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["PO_TYPE"] = PO_TYPE;
                    dr["PO_NUMB"] = PO_NUMB;
                    dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                    dr["CONF_BY"] = oUserLoginDetail.UserCode;
                    dr["CONF_FLAG"] = "3";
                    dr["REMARKS"] = textBox.Text;
                    dtPODetail.Rows.Add(dr);

                }
            }

            int iResult = SaitexBL.Interface.Method.TX_FIBER_PU_MST.Update_POForApprovalForClosing(oUserLoginDetail.UserCode, dtPODetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("PO Closed Successfully.");
                bindMaterialPOApproval();
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in PO Confirm.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/FIBER_POCLOSINGRPT.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=600');", true);
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Fiber/Pages/FiberPOClosing.aspx", false);
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

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    private void bindMaterialPOApproval()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_PU_MST.GetPODataForClosing(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));
                if (!dt.Columns.Contains("ProductID"))
                    dt.Columns.Add("ProductID", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();

                    dr["ProductID"] = dr["PO_TYPE"].ToString() + "_" + dr["PO_NUMB"].ToString();
                }

                grdPOMST.DataSource = dt;
                grdPOMST.DataBind();
            }
            else
            {

                CommonFuction.ShowMessage("No PO for approval");
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
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }

    protected void grdPOMST_RowDataBound(object sender, GridRowEventArgs e)
    {
    //    if (e.Row.RowType == GridRowType.DataRow && grdPOMST.RowsInViewState.Count > 0)
    //    {

    //        //#region To bind Detail Grid
    //        //GridDataControlFieldCell cellDTL = e.Row.Cells[11] as GridDataControlFieldCell;
    //        //Grid grdPODetail = cellDTL.FindControl("grdPODetail") as Grid;
    //        //HiddenField hfPO_NUMB = cellDTL.FindControl("hfPO_NUMB") as HiddenField;
    //        //HiddenField hfPO_TYPE = cellDTL.FindControl("hfPO_TYPE") as HiddenField;
    //        //BindDetailGrid(grdPODetail, hfPO_TYPE.Value.Trim(), int.Parse(hfPO_NUMB.Value.Trim()));
    //        //#endregion


    //        GridDataControlFieldCell cell = e.Row.Cells[7] as GridDataControlFieldCell;
    //        CheckBox chk = cell.FindControl("ChkID") as CheckBox;

    //        GridDataControlFieldCell celltxt = e.Row.Cells[10] as GridDataControlFieldCell;
    //        TextBox textBox = celltxt.FindControl("txtID") as TextBox;

    //        if (grdPOMST.RowsInViewState.Count < (e.Row.RowIndex + 1))
    //        {
    //        }
    //        else
    //        {
    //            GridDataControlFieldCell cellInViewState = grdPOMST.RowsInViewState[e.Row.RowIndex].Cells[7] as GridDataControlFieldCell;
    //            CheckBox chkInViewState = cellInViewState.FindControl("ChkID") as CheckBox;

    //            GridDataControlFieldCell cellInViewStatetxt = grdPOMST.RowsInViewState[e.Row.RowIndex].Cells[10] as GridDataControlFieldCell;
    //            TextBox textBoxInViewState = cellInViewStatetxt.FindControl("txtID") as TextBox;

    //            if (cell.Value == chkInViewState.ToolTip)
    //            {
    //                chk.Checked = chkInViewState.Checked;
    //                textBox.Text = textBoxInViewState.Text;
    //            }
    //        }
    //    }
    }
    private void BindDetailGrid(Grid grdDTL, string PO_TYPE, int PO_NUMB)
    {
        try
        {
            SaitexDM.Common.DataModel.TX_FIBER_PU_MST oTX_FIBER_PU_MST = new SaitexDM.Common.DataModel.TX_FIBER_PU_MST();

            oTX_FIBER_PU_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_FIBER_PU_MST.PO_NUMB = PO_NUMB;
            oTX_FIBER_PU_MST.PO_TYPE = PO_TYPE;
            oTX_FIBER_PU_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FIBER_PU_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            DataTable dtPODetail = SaitexBL.Interface.Method.TX_FIBER_PU_MST.Select_TransactionByPONumber(oTX_FIBER_PU_MST);

            if (dtPODetail != null)
            {
                grdDTL.DataSource = dtPODetail;
                grdDTL.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnFindTop_Click1(object sender, ImageClickEventArgs e)
    {

    }
}
