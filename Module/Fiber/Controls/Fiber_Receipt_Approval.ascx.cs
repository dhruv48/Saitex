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

public partial class Module_Fiber_Controls_Fiber_Receipt_Approval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
               
                lblMode.Text = "Find";
                bindMaterialReceiptApproval();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
            
        }
    }

    private void bindMaterialReceiptApproval()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetReceiptDataForApproval(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
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
                    {
                        dr["CONF_BY"] = oUserLoginDetail.Username;
                    }
                    dr["CONF_DATE"] = System.DateTime.Now.ToShortDateString();
                }
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

            }
            else
            {
                lblTotalRecord.Text = "No MRN for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No MRN for approval");
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
            string msg = string.Empty;
            DataTable dtReceiptDetail = CreateDataTable();
            int TotalRows = gvMaterialReceiptApproval.Rows.Count;

            for (int r = 0; r < TotalRows; r++)
            { 
            GridViewRow thisGridViewRow = gvMaterialReceiptApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblTRN_NUMB = (Label)thisGridViewRow.FindControl("lblTRN_NUMB");
                    Label lblTRN_type = (Label)thisGridViewRow.FindControl("lblTRN_type");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                   
                    if (Approved.Checked == true)
                    {
                        DataRow dr = dtReceiptDetail.NewRow();

                        dr["YEAR"] = int.Parse(lblTRN_type.ToolTip.Trim());
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["TRN_TYPE"] = lblTRN_type.Text.Trim();
                        dr["TRN_NUMB"] = int.Parse(lblTRN_NUMB.Text.Trim());
                        dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "1";
                        dtReceiptDetail.Rows.Add(dr);
                        Approved.Checked = false;
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Update_ReceiptForApproval(oUserLoginDetail.UserCode, dtReceiptDetail);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("Transaction approved Successfully.");
                bindMaterialReceiptApproval();
            }
            }
      
        catch (Exception ex)
        {

            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in MRN Confirm.\r\nSee error log for detail."));

        }
    }

    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_NUMB", typeof(int));
        dtReceiptDetail.Columns.Add("PARTY_DATA", typeof(string));
        dtReceiptDetail.Columns.Add("PRTY_CH_NUMB", typeof(string));
        dtReceiptDetail.Columns.Add("GATE_NUMB", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_FLAG", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtReceiptDetail.Columns.Add("CONF_BY", typeof(string));
        return dtReceiptDetail;
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
       // imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }




    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;


                Label lblTRN_NUMB = (Label)grdRow.FindControl("lblTRN_NUMB");
                Label lblTRN_TYPE = (Label)grdRow.FindControl("lblTRN_TYPE");

                oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
                oTX_FIBER_IR_MST.YEAR = int.Parse(lblTRN_TYPE.ToolTip.Trim());
                oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oTX_FIBER_IR_MST.TRN_NUMB = int.Parse(lblTRN_NUMB.Text.Trim());
                oTX_FIBER_IR_MST.TRN_TYPE = lblTRN_TYPE.Text.Trim();

                DataTable dtTRN = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetTRN_DataByTRN_NUMB(int.Parse(lblTRN_TYPE.ToolTip.Trim()), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, int.Parse(lblTRN_NUMB.Text.Trim()), lblTRN_TYPE.Text.Trim());
                if (dtTRN != null && dtTRN.Rows.Count > 0)
                {
                    GridView grdTRN = (GridView)grdRow.FindControl("grdTRN");
                    grdTRN.DataSource = dtTRN;
                    grdTRN.DataBind();
                    dtTRN.Dispose();

                }
                DataTable dtc = BindSupTranGrid(oTX_FIBER_IR_MST.COMP_CODE, oTX_FIBER_IR_MST.BRANCH_CODE, oTX_FIBER_IR_MST.TRN_NUMB.ToString(),  oTX_FIBER_IR_MST.TRN_TYPE);
                GridView grdBOM = (GridView)e.Row.FindControl("grdBOM");
                if (dtc != null && dtc.Rows.Count > 0)
                {
                    if (grdBOM != null)
                    {
                        grdBOM.DataSource = dtc;
                        grdBOM.DataBind();
                        CalculateAllData(grdBOM);
                        dtc.Dispose();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            
        }
    }
    protected void CalculateAllData(GridView grdsub_trn)
    {
        if (grdsub_trn.Rows.Count > 0)
        {
            double totalcartonno = 0;
            double totalNoUnit = 0;
            double totalQTY = 0;
            double totalIssQTY = 0;

            for (int i = 0; i < grdsub_trn.Rows.Count; i++)
            {

                double NoUnit = 0;
                double QTY = 0;
                double IssQTY = 0;

                Label lblNoUnit = grdsub_trn.Rows[i].FindControl("lblNoUnit") as Label;
                Label lblQTY = grdsub_trn.Rows[i].FindControl("lblQTY") as Label;
                Label lblIssQTY = grdsub_trn.Rows[i].FindControl("lblIssQTY") as Label;
                double.TryParse(lblNoUnit.Text, out NoUnit);
                double.TryParse(lblQTY.Text, out QTY);
                double.TryParse(lblIssQTY.Text, out IssQTY);
                totalcartonno = totalcartonno + 1;
                totalNoUnit = totalNoUnit + NoUnit;
                totalQTY = totalQTY + QTY;
                totalIssQTY = totalIssQTY + IssQTY;

            }

            ((Label)grdsub_trn.FooterRow.FindControl("lblFPalletNo")).Text = Math.Round(totalcartonno, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFNoUnit")).Text = Math.Round(totalNoUnit, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFQTY")).Text = Math.Round(totalQTY, 3).ToString();
            ((Label)grdsub_trn.FooterRow.FindControl("lblFIssQTY")).Text = Math.Round(totalIssQTY, 3).ToString();

        }
    }
    private DataTable BindSupTranGrid(string lblComp, string lblBranch, string TrnNum, string Type)
    {
        try
        {
           return SaitexBL.Interface.Method.TX_FIBER_IR_MST.BindSubTranGrid(lblComp, lblBranch, TrnNum,  Type);
            
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void gvMaterialReceiptApproval_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvMaterialReceiptApproval.PageIndex = e.NewPageIndex;
        bindMaterialReceiptApproval();
    }
}
