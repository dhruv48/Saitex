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
public partial class Module_Inventory_Controls_BillClearenceForm1 : System.Web.UI.UserControl
{
    private static string bill_type = string.Empty; 
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.TX_BILL_MST oTX_BILL_MST = new SaitexDM.Common.DataModel.TX_BILL_MST();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                lblMode.Text = "Find";
                bill_type = "MSP";
                bindBillClearence();
            } 
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }
    private void bindBillClearence()
    {
        try
        {
            grdBillClearence.DataSource = null;
            grdBillClearence.DataBind();
            oTX_BILL_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_BILL_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_BILL_MST.BILL_YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            oTX_BILL_MST.BILL_TYPE = bill_type;
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetUnClearBillNo(oTX_BILL_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (!dt.Columns.Contains("CONF_DATE"))
                    dt.Columns.Add("CONF_DATE", typeof(DateTime));
                if (!dt.Columns.Contains("CONF_BY"))
                    dt.Columns.Add("CONF_BY", typeof(string));
                if (!dt.Columns.Contains("BillID"))
                    dt.Columns.Add("BillID", typeof(string));

                foreach (DataRow dr in dt.Rows)
                {
                    string ConfBy = dr["CONF_BY"].ToString();
                    if (ConfBy == "")
                        dr["CONF_BY"] = oUserLoginDetail.UserCode;

                    dr["CONF_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                    string BILL_CLR_USER = dr["BILL_CLR_USER"].ToString();
                    if (BILL_CLR_USER == "")
                        dr["BILL_CLR_USER"] = oUserLoginDetail.UserCode;
                    dr["BILL_CLR_DATE"] = System.DateTime.Now.Date.ToShortDateString();
                    dr["BillID"] = dr["BILL_TYPE"].ToString() + "_" + dr["BILL_NUMB"].ToString() + "_" + dr["COMP_CODE"].ToString() + "_" + dr["BRANCH_CODE"].ToString() + "_" + dr["BILL_YEAR"].ToString();
                }

                grdBillClearence.DataSource = dt;
                grdBillClearence.DataBind();
            }
            else
            {

                CommonFuction.ShowMessage("No Bill Clearence for approval");
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
        //imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        //imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        //imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        //imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        //imgbtnExitf.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            DataTable dtBillClearence = CreateDataTable();

            for (int i = 0; i < grdBillClearence.Rows.Count; i++)
            {
                TextBox txtremarks = grdBillClearence.Rows[i].FindControl("txtComment") as TextBox;
                Label lblCombinedid = grdBillClearence.Rows[i].FindControl("lblCombinedid") as Label;
                CheckBox chk = grdBillClearence.Rows[i].FindControl("chkConfirmed") as CheckBox;   
               
              

                if (chk.Checked == true)
                {
                    //GridDataControlFieldCell celltxt = grdBillClearence.RowsInViewState[i].Cells[10] as GridDataControlFieldCell;
                    //TextBox textBox = celltxt.FindControl("txtID") as TextBox;

                    string ID = lblCombinedid.ToolTip;
                    string[] IDs = ID.Split('_');

                    string BILLTYPE = IDs[0].ToString();
                    string BILL_NUMB = IDs[1].ToString();

                    DataRow dr = dtBillClearence.NewRow();

                    dr["YEAR"] = oUserLoginDetail.DT_STARTDATE.Year;
                    dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                    dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                    dr["BILLTYPE"] = BILLTYPE;
                    dr["BILL_NUMB"] = BILL_NUMB;
                    dr["PurClrUser"] = oUserLoginDetail.UserCode;
                    dr["PurClrDate"] = System.DateTime.Now.Date;
                    dr["Remarks"] = txtremarks.Text.Trim();  
                    dtBillClearence.Rows.Add(dr);

                }
            }

            int iResult = SaitexBL.Interface.Method.TX_BILL_MST.Update_BillClearForApproval(oUserLoginDetail.UserCode, dtBillClearence);
            if (iResult > 0)
            {
                lblMode.Text = "Find";
                CommonFuction.ShowMessage("Bill Cleared Successfully.");
                //Response.Redirect("~/Module/Inventory/Pages/BillClearenceForm.aspx");  
                bindBillClearence(); 
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

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
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

    }
    protected void grdBillClearence_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string str = string.Empty;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCombinedID = (Label)e.Row.FindControl("lblCombinedid");

                string ID = lblCombinedID.ToolTip;
                string[] IDs = ID.Split('_');

                string BillType  = IDs[0].ToString();
                string  BillNumb = IDs[1].ToString();
                string Compcode  = IDs[2].ToString();
                string BranchCode= IDs[3].ToString();
                int Year = int.Parse(IDs[4].ToString());
                DataTable dt = GetTransactionDetail(Compcode, BranchCode, BillType, BillNumb, Year);       
                GridView grdTrndetails = (GridView)e.Row.FindControl("grdTrndetails");

                if (dt != null)
                {
                    grdTrndetails.DataSource = dt;
                    grdTrndetails.DataBind();
                }
                
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ff", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private DataTable GetTransactionDetail(string Compcode ,string BranchCode, string BillType, string  BillNumb, int Year)
    {
     
        oTX_BILL_MST.COMP_CODE = Compcode;
        oTX_BILL_MST.BRANCH_CODE = BranchCode;
        oTX_BILL_MST.BILL_TYPE = BillType;
        oTX_BILL_MST.BILL_NUMB = BillNumb.ToString();
        oTX_BILL_MST.BILL_YEAR = Year ;
        return SaitexBL.Interface.Method.TX_BILL_MST.GetTrasactionByMst(oTX_BILL_MST);
       
        
       

    
    
    }
    private DataTable CreateDataTable()
    {
        DataTable dtBillClearence = new DataTable();
        dtBillClearence.Columns.Add("YEAR", typeof(int));
        dtBillClearence.Columns.Add("COMP_CODE", typeof(string));
        dtBillClearence.Columns.Add("BRANCH_CODE", typeof(string));
        dtBillClearence.Columns.Add("BILLTYPE", typeof(string));
        dtBillClearence.Columns.Add("BILL_NUMB", typeof(string));
        
        dtBillClearence.Columns.Add("PurClrUser", typeof(string));
        dtBillClearence.Columns.Add("PurClrDate", typeof(DateTime));
        dtBillClearence.Columns.Add("Remarks", typeof(string));
        return dtBillClearence;
    }
}
