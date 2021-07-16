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

public partial class Module_Inventory_Controls_BillQueryForm : System.Web.UI.UserControl
{
    private static string BILL_NUMB = string.Empty;
    private static string PRTY_CODE = string.Empty;
    private static string TRN_TYPE = string.Empty;
    private static string FIN_VOUCH_NO = string.Empty;
    private static string YEAR = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                GridBillData();
                bindddlBillNo();
                bindddlPartycode();
                bindddlTrnType();
                bindddlFinVouchNo();
               
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
    private void bindddlBillNo()
    {
        try
        {
            ddlbillNO.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetBillNo();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlbillNO.DataTextField = "BILL_NUMB";
                ddlbillNO.DataValueField = "BILL_NUMB";
                ddlbillNO.DataSource = dt;            
                ddlbillNO.DataBind();
               
            }
          
            ddlbillNO.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }
       
    }
    private void bindddlPartycode()
    {
        try
        {
            ddlPartycode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlPartycode.DataTextField = "PRTY_NAME";
                ddlPartycode.DataValueField = "PRTY_CODE";
                ddlPartycode.DataSource = dt;
                ddlPartycode.DataBind();
            }
           
            ddlPartycode.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }

    }
    private void bindddlTrnType()
    {
        try
        {
            ddlTrnType.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetTrnType();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlTrnType.DataTextField = "TRN_TYPE";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = dt;
                ddlTrnType.DataBind();
            }
           
            ddlTrnType.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }

    }
    private void bindddlFinVouchNo()
    {
        try
        {
            ddlFinVouchNo.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.GetFinVouchNo();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlFinVouchNo.DataTextField = "FIN_VOUCH_NO";
                ddlFinVouchNo.DataValueField = "FIN_VOUCH_NO";
                ddlFinVouchNo.DataSource = dt;
                ddlFinVouchNo.DataBind();
            }
          //  ddlFinVouchNo.Items.Insert(0, new ListItem("SELECT", ""));
            ddlFinVouchNo.Items.Insert(0, new ListItem("---------------All---------------", ""));
        }
        catch
        {
            throw;
        }

    }

    private void bindyear()
    {

        try
        {

           
            DataTable dt = SaitexBL.Interface.Method.TX_BILL_MST.bindyearWithoutBranch();
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "YEAR";
                ddlYear.DataBind();
                ddlYear.Items.Insert(0, new ListItem("----All----", ""));

                dt.Dispose();
                dt = null;

            }
            else
            {               
                bindyear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
              
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }


    private void GridBillData()
    {
        try
        {
            if (ddlbillNO.SelectedValue.ToString() != null && ddlbillNO.SelectedValue.ToString() != string.Empty)
            {
                BILL_NUMB = ddlbillNO.SelectedValue.ToString();
            }
            else
            {
                BILL_NUMB = string.Empty;
            }


            if (ddlPartycode.SelectedValue.ToString() != null && ddlPartycode.SelectedValue.ToString() != string.Empty)
            {
                PRTY_CODE = ddlPartycode.SelectedValue.ToString();
            }
            else
            {
                PRTY_CODE = string.Empty;
            }
            if (ddlTrnType.SelectedValue.ToString() != null && ddlTrnType.SelectedValue.ToString() != string.Empty)
            {
                TRN_TYPE = ddlTrnType.SelectedValue.ToString();
            }
            else
            {
                TRN_TYPE = string.Empty;
            }
            if (ddlFinVouchNo.SelectedValue.ToString() != null && ddlFinVouchNo.SelectedValue.ToString() != string.Empty)
            {
                FIN_VOUCH_NO = ddlFinVouchNo.SelectedValue.ToString();
            }
            else
            {
                FIN_VOUCH_NO = string.Empty;
            }


            if (ddlYear.SelectedValue.ToString() != null && ddlYear.SelectedValue.ToString() != string.Empty)
            {
                YEAR = ddlYear.SelectedValue.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }

            DataTable DT = SaitexBL.Interface.Method.TX_BILL_MST.GetBilldata(BILL_NUMB, PRTY_CODE, TRN_TYPE, FIN_VOUCH_NO, YEAR);
            if (DT != null && DT.Rows.Count > 0)
            {
                GridProductEntry.DataSource = DT;
                GridProductEntry.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
            else
            {
                GridProductEntry.DataSource = null;
                GridProductEntry.DataBind();

                CommonFuction.ShowMessage("Data not Found by selected Item .");
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            }
        }
        catch
        {
            throw;
        }

    }
    protected void ddlbillNO_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridBillData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
    protected void ddlPartycode_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridBillData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void ddlTrnType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridBillData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }


    }
    protected void ddlFinVouchNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            GridBillData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
         try
        {

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "BILL_NUMB";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "PRTY_CODE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "TRN_TYPE";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "FIN_VOUCH_NO";
            myDataTable.Columns.Add(myDataColumn);



            DataRow row;
            row = myDataTable.NewRow();
            row["BILL_NUMB"] = ddlbillNO.SelectedValue.ToString();
            row["PRTY_CODE"] = ddlPartycode.SelectedValue.ToString();
            row["TRN_TYPE"] = ddlTrnType.SelectedValue.ToString();
            row["FIN_VOUCH_NO"] = ddlFinVouchNo.SelectedValue.ToString();

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;
            //Response.Redirect("~/Module/Inventory/Reports/Billqueryreport.aspx", false);
            string URL = "../Reports/Billqueryreport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
         }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    }
    protected void GridProductEntry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridProductEntry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
        GridProductEntry.PageIndex = e.NewPageIndex;
        GridBillData();
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            

                GridBillData();
            
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
}

