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

public partial class Module_Fiber_Controls_FIBER_MST : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading"));
        }
    }
    private void Initial_Control()
    {
        try
        {
            Clear_Control();
            Bind_DropDown("FIBER_TYPE", DDLFiberType);
        }
        catch
        {
            throw;
        }
    }
    private void Clear_Control()
    {
        try
        {
            DDLFiberCode.Visible = false;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            txtFiberCode.Visible = true;
            txtFiberCode .Text = string.Empty;
            txtFiberDescription.Text = string.Empty;
            txtMaximumStock.Text = string.Empty;
            txtMimimumStock.Text = string.Empty;
            txtMinimumProcureDays.Text = string.Empty;
            txtOpeningBalanceStock.Text = string.Empty;
            txtOpeningRate.Text = string.Empty;
            txtRecorderLevel.Text = string.Empty;
            txtRecorderQuantity.Text = string.Empty;
            DDLFiberType.SelectedIndex = -1;
            lblMode.Text = "Save";
        }
        catch
        {
            throw;
        }
    }
    public void Bind_DropDown(string MST_NAME,DropDownList DDL)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME , oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                DDL.Items.Clear();
                DDL.DataSource = dt;
                DDL.DataTextField = "MST_DESC";
                DDL.DataValueField = "MST_CODE";
                DDL.DataBind();
                DDL.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }
    }
    public void Bind_Fiber_Record()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.Load_Fiber_Record(string.Empty , oUserLoginDetail.COMP_CODE,oUserLoginDetail.CH_BRANCHCODE );
            if (dt != null && dt.Rows.Count > 0)
            {

                DDLFiberCode.Items.Clear();
                DDLFiberCode.DataSource = dt;
                DDLFiberCode.DataTextField = "FIBER";
                DDLFiberCode.DataValueField = "FIBER_CODE";
                DDLFiberCode.DataBind();
                DDLFiberCode.Items.Insert(0, new ListItem("------Select------", "0"));

            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Can_Insert())
                {
                    if (Insert_Record())
                    {
                        Common.CommonFuction.ShowMessage("Record Insert Sucessfully");
                        Clear_Control();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Problem in inserting data please try again");
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Inserting data"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            if (Can_Insert())
            {
                if (Insert_Record())
                {
                    Common.CommonFuction.ShowMessage("Record Update Sucessfully");
                    Bind_Fiber_Record();
                    Clear_Control();
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Problem in updating data please try again");
                }
            }
        }

    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        //try
        //{
        //    if (txtFiberCode.Text.Trim().ToString() != string.Empty)
        //    {
        //        if (Delete_Record(txtFiberCode.Text.Trim().ToString()))
        //        {
        //            Common.CommonFuction.ShowMessage("Record Delete Sucessfully");
        //            Clear_Control();
        //        }
        //        else
        //        {
        //            Common.CommonFuction.ShowMessage("Record Delete Fail");
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Deleting Record"));
        //}
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdPrint.Visible = true;
            tdDelete.Visible = true;
            DDLFiberCode.Visible = true;
            txtFiberCode.Visible = false;
            Bind_Fiber_Record();
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in finding record"));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control ();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Clearing record"));
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private bool Can_Insert()
    {
        bool Res = false;  
        try
        {
            if (txtFiberCode.Text.Trim() == string.Empty)
            {
                txtFiberCode.Focus();
                Common.CommonFuction.ShowMessage("Please enter Fiber code");                
                return false;
            }
            else
            {
                Res = true;
            }
            if (txtFiberDescription.Text.Trim() == string.Empty)
            {
                txtFiberDescription.Focus();
                Common.CommonFuction.ShowMessage("Please enter Fiber description");
                return false;
            }
            else
            {
                Res = true;
            }
            if (DDLFiberType.SelectedIndex == 0)
            {
                DDLFiberType.Focus();
                Common.CommonFuction.ShowMessage("Please select Fiber type");
                return false;
            }
            else
            {
                Res = true;
            }
            if (txtOpeningBalanceStock.Text.Trim() == string.Empty)
            {
                txtOpeningBalanceStock.Focus();
                Common.CommonFuction.ShowMessage("Please enter Opening stock");
                return false;
            }
            else
            {
                Res = true;
            }
            if (txtMimimumStock.Text.Trim() == string.Empty)
            {
                txtMimimumStock.Focus();
                Common.CommonFuction.ShowMessage("Please enter minimum stock");
                return false;
            }
            else
            {
                Res = true;
            }
            if (txtOpeningRate.Text.Trim() == string.Empty)
            {
                txtOpeningRate.Focus();
                Common.CommonFuction.ShowMessage("Please enter Opening rate");
                return false;
            }
            else
            {
                Res = true;
            }
            if (txtMinimumProcureDays.Text.Trim() == string.Empty)
            {
                txtMinimumProcureDays.Focus();
                Common.CommonFuction.ShowMessage("Please enter Procure days");
                return false;
            }
            else
            {
                Res = true;
            }
            return Res;
        }
        catch
        {
            throw;
        }
    }
    private bool Insert_Record()
    {
        SaitexDM.Common.DataModel.TX_FIBER_MST FM = new SaitexDM.Common.DataModel.TX_FIBER_MST();
        bool result = false;
        try
        {
            FM.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.ToString();
            FM.COMP_CODE = oUserLoginDetail.COMP_CODE.ToString();
            FM.FIBER_CODE = Common.CommonFuction.funFixQuotes(txtFiberCode.Text.Trim()).ToString();
            FM.FIBER_DESC = Common.CommonFuction.funFixQuotes(txtFiberDescription.Text.Trim()).ToString();
            FM.FIBER_TYPE = Common.CommonFuction.funFixQuotes(DDLFiberType.SelectedValue.ToString());
            FM.MIN_STOCK = double.Parse(txtMimimumStock.Text.Trim().ToString());
            FM.MIN_PROCURE_DAYS = double.Parse(txtMinimumProcureDays.Text.Trim().ToString());
            FM.REORDER_LEVEL = double.Parse(txtRecorderLevel.Text.Trim().ToString());
            FM.REORDER_QUANTITY = double.Parse(txtRecorderQuantity.Text.Trim().ToString());
            FM.MAX_STOCK = double.Parse(txtMaximumStock.Text.Trim().ToString());
            FM.FIBER_RATE = double.Parse(txtOpeningRate.Text.Trim().ToString());
            FM.OPEN_BAL = double.Parse(txtOpeningBalanceStock.Text.Trim().ToString());
            FM.TUSER = oUserLoginDetail.UserCode.ToString();
            bool rES = SaitexBL.Interface.Method.TX_FIBER_MST.iNSERT_rECORD(FM);
            if (rES)
            {
                result = true;
            }
            else
            {
                result = false;
            }
            return result;
        }
        catch(Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.ToString());
            return false;
        }
    }
    //private bool Delete_Record(string Fiber_Code)
    //{
    //    //try
    //    //{
    //    //  bool REs=  SaitexBL.Interface.Method.TX_FIBER_MST.Delete_Record(Fiber_Code);
    //    //  if (REs)
    //    //  {
    //    //      return true;
    //    //  }
    //    //  else
    //    //  {
    //    //      return false;
    //    //  }
    //    //}
    //    //catch
    //    //{
    //    //    throw;
    //    //}
    //}
    protected void DDLFiberCode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DDLFiberCode.SelectedIndex!=0)
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_MST.Load_Fiber_Record(DDLFiberCode.SelectedValue.Trim().ToString(), oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFiberCode.Text = dt.Rows[0]["FIBER_CODE"].ToString();
                txtFiberDescription.Text = dt.Rows[0]["FIBER_DESC"].ToString();
                txtMaximumStock.Text = dt.Rows[0]["MAX_STOCK"].ToString();
                txtMimimumStock.Text = dt.Rows[0]["MIN_STOCK"].ToString();
                txtMinimumProcureDays.Text = dt.Rows[0]["MIN_PROCURE_DAYS"].ToString();
                txtOpeningBalanceStock.Text = dt.Rows[0]["OPEN_BAL"].ToString();
                txtOpeningRate.Text = dt.Rows[0]["FIBER_RATE"].ToString();
                txtRecorderLevel.Text = dt.Rows[0]["REORDER_LEVEL"].ToString();
                txtRecorderQuantity.Text = dt.Rows[0]["REORDER_QUANTITY"].ToString();
                DDLFiberType.SelectedValue = dt.Rows[0]["FIBER_TYPE"].ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Record Found In "+DDLFiberCode.SelectedValue.Trim().ToString() +" fiber code");
            }
        }
    }
}
