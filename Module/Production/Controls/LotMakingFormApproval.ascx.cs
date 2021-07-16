using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using errorLog;
using Common;


public partial class Module_Production_Controls_LotMakingFormApproval : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if(!IsPostBack)
            {
                lblMode.Text = "Approval";
                BindLotMakingApproval();
                BindLotNo();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nsee error log for detail"));
         
        }
    }


    private void BindLotMakingApproval()
    {
        try
        {
           
            DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.GetLotMakingDataForApproval();
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
               
                }
                FormView1.DataSource = dt;
                FormView1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }
            else
            {
                lblTotalRecord.Text = "No Lot for approval";
                FormView1.DataSource = null;
                FormView1.DataBind();
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
        }
    }


    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {

        

        try
        {
           
            DataTable dtDetails = CreateDataTable();
           
                Label lblLotNo = (Label)FormView1.Row.FindControl("lblLotNo");
                CheckBox Approved = (CheckBox)FormView1.Row.FindControl("chkApproved");


                if (Approved.Checked)
                {
                    DataRow dr = dtDetails.NewRow();
                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["LOT_NO"] = lblLotNo.Text;
                    if (Approved.Checked)
                        dr["CONF_FLAG"] = "1";
                    else
                        dr["CONF_FLAG"] = "0";
                    dr["CONF_BY"] = oUserLoginDetail.Username;
                    dtDetails.Rows.Add(dr);
                }
                else
                {
                    CommonFuction.ShowMessage("Checkbox is not checked to approve Lot No - "+lblLotNo.Text );
                
                }       

           
            int iResult = SaitexBL.Interface.Method.YRN_LOT_MAKING.Update_Lot_Making_Approval(dtDetails);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Lot No- "+ lblLotNo.Text+ " Approved Successfully.");
                BindLotMakingApproval();
            }
            else
            {
                CommonFuction.ShowMessage("Lot No- " + lblLotNo.Text + " Not Approved.");
            }

        }
        catch
        {
            throw;
        }
       
    }
    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtLotDetail = new DataTable();
            dtLotDetail.Columns.Add("YEAR", typeof(int));
            dtLotDetail.Columns.Add("COMP_CODE", typeof(string));
            dtLotDetail.Columns.Add("BRANCH_CODE", typeof(string));
            dtLotDetail.Columns.Add("LOT_NO", typeof(string));
           // dtLotDetail.Columns.Add("CONF_DATE", typeof(DateTime));
            dtLotDetail.Columns.Add("CONF_BY", typeof(string));  
            dtLotDetail.Columns.Add("CONF_FLAG", typeof(string));
            return dtLotDetail;
        }
        catch
        {
            throw;
        }
    }


    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string Query_String = string.Empty;
        try
        {
            string QueryString = "";
            bool flag = false;

            if (ddlLotNo.SelectedValue.Trim() != "")
            {
                if (!flag)
                    QueryString = QueryString + "?";
                else
                    QueryString = QueryString + "&";
                QueryString = QueryString + "LOT_NO=" + ddlLotNo.SelectedValue.Trim();
                flag = true;
            }

            string URL = "../Report/LotMakingApprovalReport.aspx" + QueryString;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Open Print Page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        lblMode.Text = "Approval";
        BindLotMakingApproval();
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void DetailsView1_PageIndexChanging(object sender, DetailsViewPageEventArgs e)
    {

    }

    protected void FormView1_PageIndexChanging(object sender, FormViewPageEventArgs e)
    {
        FormView1.PageIndex = e.NewPageIndex;
        BindLotMakingApproval();
    }


    private void BindLotNo()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_LOT_MAKING.getLotNo();
            ddlLotNo.Items.Clear();
            DataView dv = new DataView(dt);
            ddlLotNo.DataSource = dv;
            ddlLotNo.DataValueField = "LOT_NO";
            ddlLotNo.DataTextField = "LOT_NO";
            ddlLotNo.DataBind();
            ddlLotNo.Items.Insert(0, new ListItem("-------Select-------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
   
}
