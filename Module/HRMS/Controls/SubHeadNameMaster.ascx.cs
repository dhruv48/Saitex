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

public partial class Module_HRMS_Controls_SubHeadNameMaster : System.Web.UI.UserControl
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                bindGvSubHeadNameMaster();
                bindComboHeadName();
                bindddlSubHeadName();
                Initial_Record();
            }
            catch (Exception ex)
            {
                Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
            }
        }

    }
    private void Initial_Record()
    {
        try
        {
            lblMode.Text = "Save";
            Grid1.AutoPostBackOnSelect = false;
            tdUpdate.Visible = false;
            tdClear.Visible = true;
            tdFind.Visible = true;
            tdDelete.Visible = false;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void bindGvSubHeadNameMaster()
    {

        try
        {

            SaitexDM.Common.DataModel.HR_SUBH_MST oHR_SUBH_MST = new SaitexDM.Common.DataModel.HR_SUBH_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SUBH_MST.Load_Grid_Record();
            Grid1.DataSource = dt;
            Grid1.DataBind();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void bindComboHeadName()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_SUBH_MST oHR_SUBH_MST = new SaitexDM.Common.DataModel.HR_SUBH_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SUBH_MST.SelectHeadName();
            cmbHead.DataSource = dt;
            cmbHead.DataValueField = "HEAD_ID";
            cmbHead.DataTextField = "HEAD_NAME";
            cmbHead.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void bindddlSubHeadName()
    {
        try
        {
            SaitexDM.Common.DataModel.HR_SUBH_MST oHR_SUBH_MST = new SaitexDM.Common.DataModel.HR_SUBH_MST();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_SUBH_MST.SelectSubHeadNameMaster();
            cmbHead.DataSource = dt;
            cmbHead.DataValueField = "HEAD_ID";
            cmbHead.DataTextField = "HEAD_NAME";
            cmbHead.DataBind();
            cmbHead.Items.Insert(0, new ListItem("-------------Select---------------------------", ""));
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void BlankControls()
    {
        try
        {
            txtSubHeadName.Text = "";
            txtSlipFieldName.Text = "";
            cmbHead.SelectedIndex = -1;
            radSalaryType.SelectedIndex = 0;
            radSubHeadCategory.SelectedIndex = 0;
            radSubHeadType.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    protected void imgbntSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Data"));
        }
    }
    private void Insertdata()
    {
        try
        {
            if (cmbHead.SelectedValue.Trim() != null && txtSubHeadName.Text != null)
            {

                SaitexDM.Common.DataModel.HR_SUBH_MST oHR_SUBH_MST = new SaitexDM.Common.DataModel.HR_SUBH_MST();
                if (txtSubh_Head_Id.Text.Trim() != "")
                {
                    oHR_SUBH_MST.SUBH_ID = Convert.ToInt32(txtSubh_Head_Id.Text.Trim().ToString());
                }
                else
                {
                    oHR_SUBH_MST.SUBH_ID = 0;
                }
                oHR_SUBH_MST.HEAD_ID = Convert.ToInt32(cmbHead.SelectedValue.Trim());
                string sStr = txtSubHeadName.Text.ToUpper();
                txtSubHeadName.Text = sStr;
                oHR_SUBH_MST.SUBH_NAME = CommonFuction.funFixQuotes(txtSubHeadName.Text.Trim());
                oHR_SUBH_MST.SUBH_CAT = char.Parse(radSubHeadCategory.SelectedValue.Trim());
                if (radSubHeadCategory.SelectedValue.Trim() == "S")
                {
                    oHR_SUBH_MST.SUBH_SAL_TYPE = radSalaryType.SelectedValue.ToString().Trim();
                }
                else if (radSubHeadCategory.SelectedValue.Trim() == "A")
                {
                    oHR_SUBH_MST.SUBH_SAL_TYPE = radSalaryType.SelectedValue.ToString().Trim();
                }
                else if (radSubHeadCategory.SelectedValue.Trim() == "D")
                {
                    oHR_SUBH_MST.SUBH_SAL_TYPE = radSalaryDeduction.SelectedValue.ToString().Trim();
                }
                else if (radSubHeadCategory.SelectedValue.Trim() == "P")
                {
                    oHR_SUBH_MST.SUBH_SAL_TYPE = radSalaryDeduction.SelectedValue.ToString().Trim();
                }
                oHR_SUBH_MST.SUBH_TYPE = char.Parse(radSubHeadType.SelectedValue.Trim());
                oHR_SUBH_MST.SUBH_SLIP_FLD_NAME = CommonFuction.funFixQuotes(txtSlipFieldName.Text.Trim());

                oHR_SUBH_MST.PAY_MODE = DDLPayMode.SelectedValue.Trim().ToString();
                if (TxtRoundIn.Text.Trim().ToString() != "" && TxtRoundIn.Text.Trim().ToString() != null)
                {
                    oHR_SUBH_MST.ROUND_IN = decimal.Parse(TxtRoundIn.Text.Trim().ToString());
                }
                oHR_SUBH_MST.ROUND_STYLE = DDLRoundStyle.SelectedValue.Trim().ToString();
                oHR_SUBH_MST.CAL_ON_DAYS = DDLCalculateOnLOP.SelectedValue.Trim().ToString();
                oHR_SUBH_MST.ACCOUNT_CODE = TxtAccountNo.Text.Trim().ToString();

                oHR_SUBH_MST.TUSER = Session["urLoginId"].ToString().Trim();
                bool bResult = SaitexBL.Interface.Method.HR_SUBH_MST.InsertSubHeadMaster(oHR_SUBH_MST);

                if (bResult)
                {
                    BlankControls();
                    bindGvSubHeadNameMaster();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void Deletedata()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HR_SUBH_MST oHR_SUBH_MST = new SaitexDM.Common.DataModel.HR_SUBH_MST();
            oHR_SUBH_MST.SUBH_ID = Convert.ToInt32(ViewState["SUBH_ID"]);
            oHR_SUBH_MST.TDATE = System.DateTime.Now;
            oHR_SUBH_MST.TUSER = Session["urLoginId"].ToString().Trim();
            bool bResult = SaitexBL.Interface.Method.HR_SUBH_MST.DeleteSubHeadMaster(oHR_SUBH_MST, out iRecordFound);
            if (bResult)
            {
                BlankControls();
                tdSave.Visible = true;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                lblMode.Text = "Save";
                Grid1.AutoPostBackOnSelect = false;
                bindGvSubHeadNameMaster();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);
            }
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
            lblMode.Text = "Find";
            tdClear.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            Hashtable ht = (Hashtable)ar[0];
            ViewState["SUBH_ID"] = ht["SUBH_ID"].ToString().Trim();
            txtSubh_Head_Id.Text = ht["SUBH_ID"].ToString().Trim();
            cmbHead.SelectedValue = ht["HEAD_ID"].ToString().Trim();
            txtSubHeadName.Text = ht["SUBH_NAME"].ToString().Trim();
            radSubHeadCategory.SelectedValue = ht["SUBH_CAT"].ToString().Trim();

            DDLPayMode.SelectedIndex = DDLPayMode.Items.IndexOf(DDLPayMode.Items.FindByValue(ht["PAY_MODE"].ToString()));
            DDLRoundStyle.SelectedIndex = DDLRoundStyle.Items.IndexOf(DDLRoundStyle.Items.FindByValue(ht["ROUND_STYLE"].ToString()));
            DDLCalculateOnLOP.SelectedIndex = DDLCalculateOnLOP.Items.IndexOf(DDLCalculateOnLOP.Items.FindByValue(ht["CAL_ON_DAYS"].ToString()));
            TxtAccountNo.Text = ht["ACCOUNT_CODE"].ToString().Trim();
            TxtRoundIn.Text = ht["ROUND_IN"].ToString().Trim();

            if (ht["SUBH_CAT"].ToString().Trim() == "S")
            {
                radSalaryDeduction.Visible = false;
                radSalaryType.Visible = true;
                radSalaryType.SelectedValue = ht["SUBH_SAL_TYPE"].ToString().Trim();
            }
            else if (ht["SUBH_CAT"].ToString().Trim() == "D")
            {
                radSalaryDeduction.Visible = true;
                radSalaryType.Visible = false;
                radSalaryDeduction.SelectedValue = ht["SUBH_SAL_TYPE"].ToString().Trim();
            }
            radSubHeadType.SelectedValue = ht["SUBH_TYPE"].ToString().Trim();
            txtSlipFieldName.Text = ht["SUBH_SLIP_FLD_NAME"].ToString().Trim();
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
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Deletedata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting Record"));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Record"));
        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE HEAD_ID like :SearchQuery And DEL_STATUS = '0' or HEAD_NAME like :SearchQuery And DEL_STATUS = '0'";
            string sortExpression = " ORDER BY HEAD_ID";
            string commandText = "SELECT * FROM HR_HEAD_MST";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM HR_HEAD_MST WHERE HEAD_ID like :SearchQuery And DEL_STATUS = '0' or HEAD_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            string URL = "SubHeadMasterReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in  Printing {0}\n Message: " + ex.Message);
        }
    }
    protected void ddlSubHead_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {

    }
    protected DataTable GetSItems(string text, int startOffset, int numberOfItems)
    {
        try
        {
            string whereClause = " WHERE s.SUBH_ID like :SearchQuery or s.SUBH_NAME like :SearchQuery And s.DEL_STATUS = '0'";
            string sortExpression = " ORDER BY s.HEAD_ID";
            string commandText = "select distinct s.SUBH_ID,s.HEAD_ID,s.SUBH_NAME,s.SUBH_CAT,s.SUBH_SAL_TYPE, s.SUBH_SLIP_FLD_NAME,s.SUBH_TYPE,h.HEAD_NAME  from HR_SUBH_MST s,HR_HEAD_MST h";
            string sPO = "";
            DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
            return dt;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    protected int GetSItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM HR_SUBH_MST WHERE HEAD_ID like :SearchQuery And DEL_STATUS = '0' or SUBH_NAME like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }

    }
    protected void ddlSubHead_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetSItems(e.Text, e.ItemsOffset, 10);
            cmbHead.Items.Clear();
            cmbHead.DataSource = data;
            cmbHead.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetSItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
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
            radSubHeadType.SelectedIndex = -1;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding Data"));
        }

    }
    protected void radSubHeadCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (radSubHeadCategory.SelectedValue.ToString().Trim() == "S")
            {
                radSalaryType.SelectedValue = "B";
                radSalaryType.Visible = true;
            }
            else if (radSubHeadCategory.SelectedValue.ToString().Trim() == "D")
            {
                radSalaryDeduction.SelectedValue = "P";
                radSalaryType.Visible = false;
                radSalaryDeduction.Visible = true;
            }
            else
            {
                radSalaryType.SelectedValue = null;
                radSalaryType.Visible = false;
                radSalaryDeduction.Visible = false;
                radSalaryDeduction.SelectedValue = null;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Initial_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear Record"));
        }
    }
}

