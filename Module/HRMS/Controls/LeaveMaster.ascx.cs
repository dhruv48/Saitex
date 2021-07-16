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
using System.Web.UI.WebControls.Adapters;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_LeaveMaster : System.Web.UI.UserControl
{
    string strTUser = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["urLoginId"] != null)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            strTUser = oUserLoginDetail.LOGINDETAILID;
            if (!IsPostBack)
            {
            try
                  {
                lblMode.Text = "Save";
                bindDDLLeaveMaster();
                bindGvLeaveMaster();
                ddlLeave.Visible = false;
                chkActive.Checked = true;
                tdUpdate.Visible = false;
                tdFind.Visible = true;
                tdClear.Visible = true;
                tdDelete.Visible = false;
                Grid1.AutoPostBackOnSelect = false;

                  }

            catch (Exception ex)
            {
                throw ex;

            }


            }
        }
        
    }
    private void bindDDLLeaveMaster()
    {
        try
        {
            //SaitexDM.Common.DataModel.HR_BANK_MST oHR_BANK_MST = new SaitexDM.Common.DataModel.HR_BANK_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_MST.SelectLeaveMaster();
            ddlLeave.DataValueField = "LV_ID";
            ddlLeave.DataTextField = "LV_NAME";
            ddlLeave.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    private void bindGvLeaveMaster()
    {

        try
        {
            SaitexDM.Common.DataModel.HR_LV_MST oHR_LV_MST = new SaitexDM.Common.DataModel.HR_LV_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_LV_MST.SelectLeaveMaster();
            Grid1.DataSource = dt;
            Grid1.DataBind();
           
        }


        catch (Exception ex)
        {
            throw ex;
        } 

    }
    private void Insertdata()
    {
       
        try
        {
            if (txtLeaveName.Text != "")
            {
                int iRecordFound = 0;
                int iPriority=0;
                string strNewLeaveId = "";
                SaitexDM.Common.DataModel.HR_LV_MST oHR_LV_MST = new SaitexDM.Common.DataModel.HR_LV_MST();
                strNewLeaveId = SaitexBL.Interface.Method.HR_LV_MST.GetNewLeaveId();
                oHR_LV_MST.LV_ID = Convert.ToInt32(strNewLeaveId);
                string sStr = txtLeaveName.Text.ToUpper();
                txtLeaveName.Text = sStr;
                oHR_LV_MST.LV_NAME = CommonFuction.funFixQuotes(txtLeaveName.Text.Trim());
                oHR_LV_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_LV_MST.LV_PRIORITY = CommonFuction.funFixQuotes(txtPriority.Text.Trim());
                oHR_LV_MST.STATUS = chkActive.Checked;
                oHR_LV_MST.TDATE = System.DateTime.Now;
                oHR_LV_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_LV_MST.InsertLeaveMaster(oHR_LV_MST, out iRecordFound,out iPriority);
                if (bResult)
                {

                    txtLeaveName.Text = "";
                    txtRemarks.Text = "";
                    txtPriority.Text = "";
                    bindGvLeaveMaster();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
                }
                else if(iPriority >0)
                {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Priority Exists :Pls Enter Another Priority');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
            }
          
        }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;

        }

    }
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        try
        {
            ArrayList ar = Grid1.SelectedRecords;

            // lblMessage.Text = "";
            lblMode.Text = "Find";
            tdDelete.Visible = true;
            tdClear.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            Hashtable ht = (Hashtable)ar[0];
            ViewState["LV_ID"] = ht["LV_ID"].ToString().Trim();
            txtLeaveName.Text = ht["LV_NAME"].ToString().Trim();
            txtPriority.Text = ht["LV_PRIORITY"].ToString().Trim();
            txtRemarks.Text = ht["REMARKS"].ToString().Trim();
        }
        catch (Exception ex)
        {
            throw ex;
        }
              
    }
    private void BlankControls()
    {
        try
        {
        txtLeaveName.Text = "";
        txtPriority.Text = "";
        txtRemarks.Text = "";
          }
        catch (Exception ex)
        {
            throw ex;
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
            throw ex;
        }
     }
    private void UpdateData()
    {
        int iRecordFound = 0;
        int iPriority = 0;
        try
        {

            if (txtLeaveName.Text != null)
            {
                SaitexDM.Common.DataModel.HR_LV_MST oHR_LV_MST = new SaitexDM.Common.DataModel.HR_LV_MST();
                oHR_LV_MST.LV_ID = Convert.ToInt32(ViewState["LV_ID"]);
                string sStr = txtLeaveName.Text.ToUpper();
                txtLeaveName.Text = sStr;
                oHR_LV_MST.LV_NAME = CommonFuction.funFixQuotes(txtLeaveName.Text.Trim());
                oHR_LV_MST.REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                oHR_LV_MST.LV_PRIORITY = CommonFuction.funFixQuotes(txtPriority.Text.Trim());
                oHR_LV_MST.STATUS = chkActive.Checked;
                oHR_LV_MST.TDATE = System.DateTime.Now;
                oHR_LV_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_LV_MST.UpdateLeaveMaster(oHR_LV_MST, out iRecordFound,out iPriority);
                if (bResult)
                {
                    BlankControls();
                    tdUpdate.Visible = false;
                    tdDelete.Visible = false;
                    tdSave.Visible = true;
                    ddlLeave.Visible = false;
                    Grid1.AutoPostBackOnSelect = false;
                    bindGvLeaveMaster();
                    lblMode.Text = "Save";
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);
                }
                else if (iRecordFound > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Exists :Pls Enter Another Record');", true);
                }
                else if (iPriority > 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Priority Exists :Pls Enter Another Priority');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
            }
        }

        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnDelete_Click2(object sender, ImageClickEventArgs e)
    {
        try
        {
      DeleteData();
            }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void DeleteData()
    {
        int iRecordFound = 0;
        try
        {

            SaitexDM.Common.DataModel.HR_LV_MST oHR_LV_MST = new SaitexDM.Common.DataModel.HR_LV_MST();
            oHR_LV_MST.LV_ID = (Convert.ToInt32(ViewState["LV_ID"]));
            oHR_LV_MST.TDATE = System.DateTime.Now;
            oHR_LV_MST.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_LV_MST.DeleteLeaveMaster(oHR_LV_MST, out iRecordFound);
            if (bResult)
            {
                BlankControls();
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                tdSave.Visible = true;
                ddlLeave.Visible = false;
                Grid1.AutoPostBackOnSelect = false;
                bindGvLeaveMaster();
                lblMode.Text = "Save";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);

            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
          try
        {
        BlankControls();
        lblMode.Text = "Save";
        bindGvLeaveMaster();
        ddlLeave.SelectedIndex = -1;
        Grid1.AutoPostBackOnSelect = false;
        chkActive.Checked = true;
        ddlLeave.Visible = false;
        tdUpdate.Visible = false;
        tdDelete.Visible = false;
        tdSave.Visible = true;
        }
          catch (Exception ex)
          {
              throw ex;
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        string URL = "LeaveMasterReport.aspx"; 
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE LV_NAME like :SearchQuery And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY LV_ID";
        string commandText = "SELECT distinct LV_ID,LV_NAME,REMARKS,LV_PRIORITY,DEL_STATUS  FROM HR_LV_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected int GetItemsCount(string text)
    {

        string CommandText = "SELECT COUNT(*) FROM HR_LV_MST WHERE  LV_NAME like :SearchQuery And DEL_STATUS = '0'";
        return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");


    }
    protected void ddlLeave_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);

        ddlLeave.Items.Clear();
        ddlLeave.DataSource = data;
        ddlLeave.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
          }
          catch (Exception ex)
          {
              throw ex;
          }

    }
    protected void ddlLeave_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.HR_LV_MST oHR_LV_MST = new SaitexDM.Common.DataModel.HR_LV_MST();
            int LV_ID =Convert.ToInt32(ddlLeave.SelectedValue.Trim());
            DataTable dt = SaitexBL.Interface.Method.HR_LV_MST.SelectLeave(LV_ID);
            if (dt != null && dt.Rows.Count > 0)
            {
                ViewState["LV_ID"]=dt.Rows[0]["LV_ID"].ToString();
                txtLeaveName.Text = dt.Rows[0]["LV_NAME"].ToString();
                txtPriority.Text = dt.Rows[0]["LV_PRIORITY"].ToString();
                txtRemarks.Text = dt.Rows[0]["REMARKS"].ToString();
                tdSave.Visible = false;
                tdUpdate.Visible = true;
                tdDelete.Visible = true;
                lblMode.Text = "Update";

            }
            else
            {
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                lblMode.Text = "Save";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        lblMode.Text = "Update";
        ddlLeave.Visible = true;
        tdDelete.Visible = true;
        tdUpdate.Visible = true;
        tdSave.Visible = false;
        ddlLeave.SelectedIndex = -1;
        Grid1.AutoPostBackOnSelect = true;
           }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}



