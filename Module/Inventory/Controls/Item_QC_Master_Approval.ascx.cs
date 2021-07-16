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

public partial class Module_Inventory_Controls_Item_QC_Master_Approval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!IsPostBack)
                {
                    lblMode.Text = "Find";
                    bindMaterialReceiptApproval();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }

    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("ITEM_CATEGORY", typeof(string));
        dtReceiptDetail.Columns.Add("ITEM_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("CONF_FLAG", typeof(string));
        //dtReceiptDetail.Columns.Add("CONF_DATE", typeof(DateTime));
        dtReceiptDetail.Columns.Add("CONF_BY", typeof(string));
        return dtReceiptDetail;
    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {


            DataTable dtReceiptDetail = CreateDataTable();
            int totalRows = gvMaterialReceiptApproval.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialReceiptApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblITEM_CATEGORY = (Label)thisGridViewRow.FindControl("lblITEM_CATEGORY");
                    Label lblITEM_CODE = (Label)thisGridViewRow.FindControl("lblITEM_CODE");
                    CheckBox chkApproved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    if (chkApproved.Checked == true)
                    {
                        DataRow dr = dtReceiptDetail.NewRow();
                        dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                        dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                        dr["ITEM_CATEGORY"] = lblITEM_CATEGORY.Text.Trim();
                        dr["ITEM_CODE"] = lblITEM_CODE.Text.Trim();
                        //dr["CONF_DATE"] = DateTime.Now.Date.ToString();
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;
                        dr["CONF_FLAG"] = "1";
                        dtReceiptDetail.Rows.Add(dr);
                        chkApproved.Checked = false;
                    }
                }
            }


            if (dtReceiptDetail != null && dtReceiptDetail.Rows.Count > 0)
            {
                bool iResult = SaitexBL.Interface.Method.TX_ITEM_MST.Update_ITEM_STANDARD_PARAMETERApproval(dtReceiptDetail);
                if (iResult)
                {
                    lblMode.Text = "Find";
                    CommonFuction.ShowMessage("QC Standard approved Successfully.");
                    bindMaterialReceiptApproval();
                }
            }
            else
            {
                CommonFuction.ShowMessage("Please Confirm Atleast one QC Standard Parameter!!!");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in QC Standard Confirm.\r\nSee error log for detail."));
        }
    }


    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/Item_QC_MasterReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=0,toolbar=0,menubar=0,location=100,scrollbars=1,resizable=1,width=800,height=500');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        bindMaterialReceiptApproval();
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void bindMaterialReceiptApproval()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemQCStandardApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                //if (!dt.Columns.Contains("CONF_DATE"))
                //    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                //if (!dt.Columns.Contains("CONF_BY"))
                //    dt.Columns.Add("CONF_BY", typeof(string));

                //foreach (DataRow dr in dt.Rows)
                //{
                //    string ConfBy = dr["CONF_BY"].ToString();
                //    if (ConfBy == "")
                //        dr["CONF_BY"] = oUserLoginDetail.Username;

                //    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                //}
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No QC Standard for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No QC Standard for approval");
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
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }
}
