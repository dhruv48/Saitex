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
using Common;
using errorLog;
using System.IO;
using DBLibrary;


public partial class Module_HRMS_Controls_Mobile_Master : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable DTable;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {                
                //TxtTelephoneNo.Attributes.Add("readonly", "readonly");
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }
    private void Initial_Control()
    {
        try
        {
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            TxtArea.Text = string.Empty;           
            DDLServiceprovider.SelectedIndex = -1;
            TxtTelephoneNo.Text = string.Empty ;
            TxtPurchageDate.Text = System.DateTime.Now.Date.ToShortDateString();
            Load_Record_In_Grid();
        }
        catch 
        {
            throw;
        }
    }
    private void Load_Record_In_Grid()
    {
        try
        {
            DTable = new DataTable();
            DTable = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Load_Mobile_Record();
            GVTelephoneRecord.DataSource = DTable;
            GVTelephoneRecord.DataBind();

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
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Insert Sucesfully");
                Initial_Control();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in record inserting");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record Saving.\r\nSee error log for detail."));
        }
    }
    private bool Insert_Record()
    {
        bool Res = false;
        try
        {
            SaitexDM.Common.DataModel.HR_TELEPHONE_MST TM = new SaitexDM.Common.DataModel.HR_TELEPHONE_MST();
           
                if (TxtTelephoneNo.Text.Trim() != string.Empty)
                {
                    TM.TUSER = oUserLoginDetail.UserCode.Trim().ToString();
                    TM.PURCHAGE_DATE = DateTime.Parse(TxtPurchageDate.Text.Trim().ToString());
                    TM.TELEPHONE_NO = decimal .Parse(TxtTelephoneNo.Text.Trim().ToString());
                    TM.AREA = TxtArea.Text.Trim().ToString();
                    TM.SERVICE_PROVIDER = DDLServiceprovider.SelectedValue.Trim().ToString();
                    Res = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Insert_Into_HR_TELEPHONE_MASTER(TM);  
                }
            return Res;
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
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Update Sucesfully");
                Initial_Control();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in record updating");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Record.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //TxtTelephoneNo.Text = string.Empty;
            //TxtTelephoneNo.ReadOnly = false;
            //TxtTelephoneNo.Focus();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding Record.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Rercord Printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Control();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Control.\r\nSee error log for detail."));
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
                Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit.\\r\\nSee error log for detail."));
            }        
    }
    protected void GVTelephoneRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVTelephoneRecord.PageIndex = e.NewPageIndex;
            Load_Record_In_Grid();
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Grid View.\\r\\nSee error log for detail."));

        }
    }
    protected void GVTelephoneRecord_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            decimal UniqueId = decimal.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                FillDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "EmpDelete")
            {
                Delete_record_by_ID(UniqueId.ToString());
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting Record.\r\nSee error log for detail."));
        }

    }
    private void FillDetailByGrid(decimal  UniqueId)
    {
        try
        {
            DataView dv = new DataView(DTable);
            dv.RowFilter = "TELEPHONE_NO=" + UniqueId;
            if (dv.Count > 0)
            {
                DDLServiceprovider.SelectedValue = dv[0]["SERVICE_PROVIDER"].ToString();
                TxtArea.Text = dv[0]["AREA"].ToString();
                TxtTelephoneNo.Text = dv[0]["TELEPHONE_NO"].ToString();
                TxtPurchageDate.Text = dv[0]["PURCHAGE_DATE"].ToString();
                ViewState["TELEPHONE_NO"] = UniqueId;
            }
        }
        catch 
        {
            throw;
        }
    }
    private void Delete_record_by_ID( string TELEPHONE_NO)
    {
        try
        {
            bool Res = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Delete_Record_By_Id(TELEPHONE_NO);
            if (Res)
            {
                Common.CommonFuction.ShowMessage("Record delete sucessfully");
                Initial_Control();
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to delete,please try again");
            }
        }
        catch
        {
            throw;
        }
       
    }
    protected void TxtTelephoneNo_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtTelephoneNo.Text.Trim() != string.Empty && TxtTelephoneNo.Text.Trim().ToString() != "")
            {
                FillDetailByGrid(int.Parse(TxtTelephoneNo.Text.Trim().ToString()));
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please enter the telephone No");
            }
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding Record.\r\nSee error log for detail."));
        }
    }
   
}
