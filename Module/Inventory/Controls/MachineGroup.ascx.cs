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

public partial class Module_Machine_Controls_MachineGroup : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["urLoginId"] != null)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            //  strTUser = oUserLoginDetail.LOGINDETAILID;
            if (!IsPostBack)
            {
                InitialisePage();
          
            }
        }
    }
    private void InitialisePage()
    {
        lblMode.Text = "Save";
        cmbMachineGroup.Visible = false;
        tdUpdate.Visible = false;
        tdFind.Visible = true;
        tdClear.Visible = true;
        tdDelete.Visible = false;
        ddlMachineType.SelectedIndex = -1;
        ddlSection.SelectedIndex = -1;
        ddlSegment.SelectedIndex = -1;
        bindDDLMachineType();
        bindDDLMachineSegment();
        bindDDLMachineSection();
        bindCMBMachineGroup();
        txtMachineGroup.Text = "";
       
    }
    
    
    private void bindCMBMachineGroup()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineGrp();
            cmbMachineGroup.DataSource = dt;
            cmbMachineGroup.DataValueField = "MACHINE_GROUP";
            cmbMachineGroup.DataTextField = "MACHINE_GROUP";
            cmbMachineGroup.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    
    private void bindDDLMachineType()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineType();
            ddlMachineType.DataSource = dt;
            ddlMachineType.DataValueField = "MST_CODE";
            ddlMachineType.DataTextField = "MST_CODE";
            ddlMachineType.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    private void bindDDLMachineSection()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineSec();
            ddlSection.DataSource = dt;
            ddlSection.DataValueField = "MST_CODE";
            ddlSection.DataTextField = "MST_CODE";
            ddlSection.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    private void bindDDLMachineSegment()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineSegment();
            ddlSegment.DataSource = dt;
            ddlSegment.DataValueField = "MST_CODE";
            ddlSegment.DataTextField = "MST_CODE";
            ddlSegment.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }
    }
   
    protected DataTable GetItemsMG(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE t.MST_NAME='MACHINE_GROUP' and t.MST_NAME=m.MST_NAME and t.MST_CODE like :SearchQuery and t.DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct t.MST_CODE from TX_MASTER_TRN t,TX_MASTER_MST m";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected DataTable GetItemsMSG(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE t.MST_NAME='MACHINE_SEGMENT' and t.MST_NAME=m.MST_NAME and t.MST_CODE like :SearchQuery and t.DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct t.MST_CODE from TX_MASTER_TRN t,TX_MASTER_MST m ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected DataTable GetItemsMS(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE t.MST_NAME='MACHINE_SEC' and t.MST_NAME=m.MST_NAME and t.MST_CODE like :SearchQuery and t.DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct t.MST_CODE from TX_MASTER_TRN t,TX_MASTER_MST m ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected DataTable GetItemsMT(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE t.MST_NAME='MACHINE_TYPE' and t.MST_NAME=m.MST_NAME and t.MST_CODE like :SearchQuery and t.DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct t.MST_CODE from TX_MASTER_TRN t,TX_MASTER_MST m ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
  
       private void Insertdata()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.MC_MACHINE_GRP oMC_MACHINE_GRP = new SaitexDM.Common.DataModel.MC_MACHINE_GRP();
            //oMC_MACHINE_GRP.MACHINE_GRP_ID =Convert.ToInt32(txtMachineGrpId.Text.Trim());
            oMC_MACHINE_GRP.MACHINE_GROUP = (txtMachineGroup.Text.Trim());
            oMC_MACHINE_GRP.MACHINE_SEC = (ddlSection.SelectedValue.Trim());
            oMC_MACHINE_GRP.MACHINE_TYPE = (ddlSegment.SelectedValue.Trim());
            oMC_MACHINE_GRP.MACHINE_SEGMENT = (ddlMachineType.SelectedValue.Trim());
            oMC_MACHINE_GRP.TUSER = Session["urLoginId"].ToString().Trim();
            //oMC_MACHINE_MASTER.TDATE = System.DateTime.Now;
            bool bResult = SaitexBL.Interface.Method.MC_MACHINE_MASTER.InsertMachineGroup(oMC_MACHINE_GRP, out iRecordFound);
            if (bResult)
            {
                InitialisePage();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
       private void Updatedata()
       {
           try
           {
               int iRecordFound = 0;
               SaitexDM.Common.DataModel.MC_MACHINE_GRP oMC_MACHINE_GRP = new SaitexDM.Common.DataModel.MC_MACHINE_GRP();
               // strNewLeaveId = SaitexBL.Interface.Method.HR_LV_MST.GetNewLeaveId();
               //string strNewMachineCode = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetNewMachineCode(mg);
               oMC_MACHINE_GRP.MACHINE_GROUP = (txtMachineGroup.Text.Trim());
               oMC_MACHINE_GRP.MACHINE_SEC = (ddlSection.SelectedValue.Trim());
               oMC_MACHINE_GRP.MACHINE_TYPE = (ddlSegment.SelectedValue.Trim());
               oMC_MACHINE_GRP.MACHINE_SEGMENT = (ddlMachineType.SelectedValue.Trim());
               oMC_MACHINE_GRP.TUSER = Session["urLoginId"].ToString().Trim();
               //oMC_MACHINE_MASTER.TDATE = System.DateTime.Now;
               bool bResult = SaitexBL.Interface.Method.MC_MACHINE_MASTER.UpdateMachineGroup(oMC_MACHINE_GRP, out iRecordFound);
               if (bResult)
               {
                   InitialisePage();
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);

               }
               else
               {
                   ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Updated');", true);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }
       protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
       {
           Insertdata();
       }
       protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
       {
           Updatedata();
       }

       protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
       {

       }
       protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
       {
           tdSave.Visible = false;
           tdUpdate.Visible = true;
           tdDelete.Visible = true;
           lblMode.Text = "Update";
           cmbMachineGroup.Visible = true;
          
       }
       protected DataTable GetItems(string text, int startOffset, int numberOfItems)
       {

           string whereClause = " WHERE MACHINE_GROUP like :SearchQuery and DEL_STATUS='0'";
           string sortExpression = "";
           string commandText = "select distinct MACHINE_GROUP from MC_MACHINE_GRP";
           string sPO = "";
           DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
           return dt;

       }
       protected void cmbMachineGroup_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
       {
           DataTable data = new DataTable();
           data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);

           cmbMachineGroup.Items.Clear();
           cmbMachineGroup.DataSource = data;
           cmbMachineGroup.DataBind();

       }
       protected void cmbMachineGroup_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
       {
           DataTable dt = new DataTable();
           string MachineGroupID = txtMachineGroup.Text.Trim();
           dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineGrouponID(MachineGroupID);
           if (dt != null && dt.Rows.Count > 0)
           {
               txtMachineGroup.Text = dt.Rows[0]["MACHINE_GROUP"].ToString();
               ddlMachineType.SelectedValue = dt.Rows[0]["MACHINE_TYPE"].ToString();
               ddlSection.SelectedValue = dt.Rows[0]["MACHINE_SEC"].ToString();
               ddlSegment.SelectedValue = dt.Rows[0]["MACHINE_SEGMENT"].ToString();
              

           }
       }
       protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
       {
           tdDelete.Visible = false;
           tdSave.Visible = true;
           tdUpdate.Visible = false;
           tdDelete.Visible = false;
           lblMode.Text = "Save";
           cmbMachineGroup.Visible = false;
           ddlMachineType.SelectedIndex = -1;
           ddlSection.SelectedIndex = -1;
           ddlSegment.SelectedIndex = -1;
           txtMachineGroup.Text = "";
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
       protected void ddlSegment_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
       {
           DataTable data = new DataTable();
           data = GetItemsMSG(e.Text.ToUpper(), e.ItemsOffset, 10);
           ddlSegment.Items.Clear();
           ddlSegment.DataSource = data;
           ddlSegment.DataBind();

       }
       protected void ddlSegment_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
       {

       }
       protected void ddlSection_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
       {
           DataTable data = new DataTable();
           data = GetItemsMS(e.Text.ToUpper(), e.ItemsOffset, 10);
           ddlSection.Items.Clear();
           ddlSection.DataSource = data;
           ddlSection.DataBind();

       }
       protected void ddlSection_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
       {

       }
       protected void ddlMachineType_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
       {
           DataTable data = new DataTable();
           data = GetItemsMT(e.Text.ToUpper(), e.ItemsOffset, 10);
           ddlMachineType.Items.Clear();
           ddlMachineType.DataSource = data;
           ddlMachineType.DataBind();

       }
       protected void ddlMachineType_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
       {

       }
       protected void ddlMachineGroup_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
       {
           DataTable data = new DataTable();
           data = GetItemsMG(e.Text.ToUpper(), e.ItemsOffset, 10);
          
       }
     
       protected void txtMachineGroup_TextChanged(object sender, EventArgs e)
       {
         
       }
}


    

