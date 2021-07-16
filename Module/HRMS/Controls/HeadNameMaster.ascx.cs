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
using Common;
using errorLog;
using System.IO;
using DBLibrary;
public partial class Module_HRMS_Controls_HeadNameMaster : System.Web.UI.UserControl
{
    
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {

                lblMode.Text = "Save";
                bindGvHeadNameMaster('y');
                Grid1.AutoPostBackOnSelect = false;
                chkActive.Checked = true;
                tdUpdate.Visible = false;
                tdClear.Visible = false;
                tdDelete.Visible = false;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in page loading"));
        } 
    }
    private void bindGvHeadNameMaster(char chView)
    {

        try
        {
            SaitexDM.Common.DataModel.HR_HEAD_MST oHR_HEAD_MST = new SaitexDM.Common.DataModel.HR_HEAD_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_HEAD_MST.SelectHeadNameMaster(chView);
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }
        catch 
        {
            throw;
        }

    }
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Saving Record"));
        }
    }
    private void Insertdata()
    {
        try
        {                       
                int iRecordFound = 0;
                string strNewHeadId = string.Empty ;
                SaitexDM.Common.DataModel.HR_HEAD_MST oHR_HEAD_MST = new SaitexDM.Common.DataModel.HR_HEAD_MST();

                strNewHeadId = SaitexBL.Interface.Method.HR_HEAD_MST.GetNewHeadId();
                oHR_HEAD_MST.HEAD_ID = Convert.ToInt32(strNewHeadId);
                string sStr = txtHeadName.Text.ToUpper();
                txtHeadName.Text = sStr;
                oHR_HEAD_MST.HEAD_NAME = CommonFuction.funFixQuotes(txtHeadName.Text.Trim());
                oHR_HEAD_MST.STATUS = chkActive.Checked;
                oHR_HEAD_MST.TDATE = System.DateTime.Now;
                oHR_HEAD_MST.TUSER = Session["urLoginId"].ToString().Trim();
                 bool bResult = SaitexBL.Interface.Method.HR_HEAD_MST.InsertHeadNameMaster(oHR_HEAD_MST,out iRecordFound);
                if (bResult)
                {

                    txtHeadName.Text = string.Empty;
                    Grid1.AutoPostBackOnSelect = false;
                    bindGvHeadNameMaster('y');
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }           
          
        }
        catch 
        {
            throw;
        }
    
    }
    private void UpdateData()
    {
        try
        {

            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HR_HEAD_MST oHR_HEAD_MST = new SaitexDM.Common.DataModel.HR_HEAD_MST();

            oHR_HEAD_MST.HEAD_ID = Convert.ToInt32(ViewState["HEAD_ID"]);
            string sStr = txtHeadName.Text.ToUpper();
            txtHeadName.Text = sStr;
            oHR_HEAD_MST.HEAD_NAME = CommonFuction.funFixQuotes(txtHeadName.Text.Trim());
            oHR_HEAD_MST.STATUS = chkActive.Checked;
            oHR_HEAD_MST.TDATE = System.DateTime.Now;
            oHR_HEAD_MST.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_HEAD_MST.UpdateHeadNameMaster(oHR_HEAD_MST, out iRecordFound);
            if (bResult)
            {

                txtHeadName.Text = string.Empty;
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                Grid1.AutoPostBackOnSelect = false;
                lblMode.Text = "Save";
                bindGvHeadNameMaster('y');
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
            }

        }
        catch 
        {
            throw ;
        }
    }
    private void DeleteData()
    {
        try
        {

            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HR_HEAD_MST oHR_HEAD_MST = new SaitexDM.Common.DataModel.HR_HEAD_MST();
            oHR_HEAD_MST.HEAD_ID = Convert.ToInt32(ViewState["HEAD_ID"]);
            oHR_HEAD_MST.TDATE = System.DateTime.Now;
            oHR_HEAD_MST.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_HEAD_MST.DeleteHeadNameMaster(oHR_HEAD_MST, out iRecordFound);
            if (bResult)
            {

                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                txtHeadName.Text = string.Empty;
                Grid1.AutoPostBackOnSelect = false;
                lblMode.Text = "Save";
                bindGvHeadNameMaster('y');
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
            }

        }
        catch 
        {
            throw;
        }
    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = Grid1.SelectedRecords;
            lblMode.Text = "Update";
            tdClear.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            Hashtable ht = (Hashtable)ar[0];
            ViewState["HEAD_ID"] = ht["HEAD_ID"].ToString().Trim();
            txtHeadName.Text = ht["HEAD_NAME"].ToString().Trim();
         }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Selecting Record"));
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Exit Page"));
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            DeleteData();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Deleting Record"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
       try
        {

        UpdateData();
        }
       catch (Exception ex)
       {
           Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Updating Record"));
       }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        string URL = "HeadMasterReport.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Printing Record"));
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            lblMode.Text = "Update";
            Grid1.AutoPostBackOnSelect = true;
          }
       catch (Exception ex)
       {
           errorLog.ErrHandler.WriteError(ex.Message);
       }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        
        try
        {
        tdSave.Visible = true;
        tdUpdate.Visible = false;
        tdDelete.Visible = false;
        lblMode.Text = "Save";
        txtHeadName.Text = string.Empty;
           
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
}
