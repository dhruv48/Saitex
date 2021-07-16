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


public partial class Module_OrderDevelopment_Controls_LabDipEntry : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                lblMode.Text = "Save";
                bindcmbPartyCode();
                bindcmbFabricCode();
                bindddlSecondaryLightSource();
                bindddlPrimaryLightSource();
                bindddlShadeType();
                bindcmbLabDipNo();
                cmbLabDipNo.Visible = false;
                tdUpdate.Visible = false;
                tdDelete.Visible = false;
                txtReceiveDate.Text = System.DateTime.Now.ToShortDateString();
                txtDueDate.Text = System.DateTime.Now.ToShortDateString();

                DateTime startdate = oUserLoginDetail.DT_STARTDATE;
                DateTime Enddate = CommonFuction.GetYearEndDate(startdate);
                string EndDate = Enddate.ToShortDateString();
                RangeValidator1.MinimumValue = startdate.ToShortDateString();
                RangeValidator1.MaximumValue = DateTime.Now.Date.ToShortDateString();
                RangeValidator2.MinimumValue = startdate.ToShortDateString();
                RangeValidator2.MaximumValue = EndDate.ToString();
            }
            getLabdipNo();

        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This function makes all the controls of this page Blank.
    /// </summary>
    private void BlankControls()
    {
        try
        {
            txtLabDipNo.Text = "";
            cmbLabDipNo.SelectedIndex = -1;
            txtFabricCode.Text = "";
            txtComment.Text = "";
            txtLabDipNo.Text = "";
            txtPartyDesignNo.Text = "";
            txtPartyRefDoc.Text = "";
            txtShade.Text = "";
            txtPartyDocdate.Text = "";
            ddlPrimaryLightSource.SelectedIndex = -1;
            ddlSecondaryLightSource.SelectedIndex = -1;
            ddlShadeType.SelectedIndex = -1;
            cmbFabricCode.SelectedIndex = -1;
            cmbPartyCode.SelectedIndex = -1;
            ddlLabdipStatus.SelectedIndex = 0;
            txtPartyCode.Text = "";
            txtFabricCode.Text = "";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This function binds the ComboBox for PartyCode.
    /// </summary>
    private void bindcmbPartyCode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectParty();
            cmbPartyCode.DataSource = dt;
            cmbPartyCode.DataValueField = "PRTY_CODE";
            cmbPartyCode.DataTextField = "PRTY_CODE";
            cmbPartyCode.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the ComboBox for FabricCode.
    /// </summary>

    private void bindcmbFabricCode()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectFabric();
            cmbFabricCode.DataSource = dt;
            cmbFabricCode.DataValueField = "FABR_CODE";
            cmbFabricCode.DataTextField = "FABR_CODE";
            cmbFabricCode.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the Dropdown for Lab Dip Shade.
    /// </summary>
    private void bindddlShadeType()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectShadeType();
            ddlShadeType.DataSource = dt;
            ddlShadeType.DataValueField = "MST_CODE";
            ddlShadeType.DataTextField = "MST_CODE";
            ddlShadeType.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the ComboBox for LabDip No.
    /// </summary>
    private void bindcmbLabDipNo()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectLabDipNo();
            cmbLabDipNo.DataSource = dt;
            cmbLabDipNo.DataValueField = "LAB_DIP_NO";
            cmbLabDipNo.DataTextField = "LAB_DIP_NO";
            cmbLabDipNo.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the Dropdown for LabDip PrimaryLightSource.
    /// </summary>
    private void bindddlPrimaryLightSource()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectLightSource();
            ddlPrimaryLightSource.DataSource = dt;
            ddlPrimaryLightSource.DataValueField = "MST_CODE";
            ddlPrimaryLightSource.DataTextField = "MST_CODE";
            ddlPrimaryLightSource.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the Dropdown for LabDip SecondaryLightSource.
    /// </summary>
    private void bindddlSecondaryLightSource()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectLightSource();
            ddlSecondaryLightSource.DataSource = dt;
            ddlSecondaryLightSource.DataValueField = "MST_CODE";
            ddlSecondaryLightSource.DataTextField = "MST_CODE";
            ddlSecondaryLightSource.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    /// <summary>
    /// This function binds the LabDipStatus Dropdown.
    /// </summary>
    private void bindddlLabDipStatus()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectLightSource();
            ddlLabdipStatus.DataSource = dt;
            ddlLabdipStatus.DataValueField = "MST_CODE";
            ddlLabdipStatus.DataTextField = "MST_CODE";
            ddlLabdipStatus.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    protected DataTable GetItemsPC(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE PRTY_CODE like :SearchQuery and PRTY_NAME like :SearchQuery and PRTY_ADD1 like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select distinct PRTY_CODE,PRTY_NAME,PRTY_ADD1 from TX_VENDOR_MST ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }

    protected DataTable GetItemsFC(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE FABR_CODE like :SearchQuery and FABR_CAT like :SearchQuery and FABR_DESC like :SearchQuery";
        string sortExpression = "";
        string commandText = "select distinct FABR_CODE,FABR_CAT,FABR_DESC from TX_FABRIC_MST";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;



    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE LAB_DIP_NO like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "order by LAB_DIP_NO";
        string commandText = "select distinct LAB_DIP_NO from OD_LAB_DIP_ENTRY ";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    /// <summary>
    /// This method validates Lab dip entry.
    /// </summary>
    /// <param name="msg">output message</param>
    /// <returns>true,false</returns>
    private bool ValidateLabDipRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;

            int count = 0;
            int msgCount = 1;
            if (cmbPartyCode.SelectedIndex > -1)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Party code. ";
                msgCount += 1;
            }

            if (cmbFabricCode.SelectedIndex > -1)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Fabric Code. ";
                msgCount += 1;
            }

            if (ddlPrimaryLightSource.SelectedIndex > -1)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Primary Light Source. ";
                msgCount += 1;
            }
            if (ddlSecondaryLightSource.SelectedIndex > -1)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select Secondary Light Source. ";
                msgCount += 1;
            }

            if (ddlLabdipStatus.SelectedIndex > -1)
                count = count + 1;
            else
            {
                msg = msg + msgCount.ToString() + ": Please select LabDipStatus. ";
                msgCount += 1;
            }
            if (count == 5)
                result = true;

            return result;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = "SELECT COUNT(*) FROM OD_LAB_DIP_ENTRY WHERE  LAB_DIP_NO like :SearchQuery And DEL_STATUS = '0'";
            return SaitexBL.Interface.Method.HR_LV_TRN.GetCountForLOV(CommandText, text + '%', "");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    /// <summary>
    /// This Function Saves the Record in the table OD_LAB_DIP_ENTRY.
    /// </summary>
    private void Insertdata()
    {
        try
        {
            int iRecordFound = 0;
            string msg = string.Empty;
            if (ValidateLabDipRow(out msg))
            {
                SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY oOD_LAB_DIP_ENTRY = new SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY();
                //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
                oOD_LAB_DIP_ENTRY.COMP_CODE = oUserLoginDetail.COMP_CODE;
                oOD_LAB_DIP_ENTRY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                oOD_LAB_DIP_ENTRY.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                oOD_LAB_DIP_ENTRY.LAB_DIP_NO = txtLabDipNo.Text.ToString();//Convert.ToInt32();
                oOD_LAB_DIP_ENTRY.RECEIVE_DATE = Convert.ToDateTime(txtReceiveDate.Text.ToString());
                oOD_LAB_DIP_ENTRY.DUE_DATE = Convert.ToDateTime(txtDueDate.Text.ToString());
                oOD_LAB_DIP_ENTRY.PRTY_CODE = cmbPartyCode.SelectedValue.Trim();
                oOD_LAB_DIP_ENTRY.FABR_CODE = cmbFabricCode.SelectedValue.Trim();
                oOD_LAB_DIP_ENTRY.SHADE = ddlShadeType.SelectedValue.Trim();
                oOD_LAB_DIP_ENTRY.PRTY_DSG_NO = txtPartyDesignNo.Text.ToString();
                oOD_LAB_DIP_ENTRY.PRTY_DOC_REF_NO = txtPartyRefDoc.Text.ToString();
                oOD_LAB_DIP_ENTRY.PRTY_DOC_DATE = Convert.ToDateTime(txtPartyDocdate.Text.ToString());
                oOD_LAB_DIP_ENTRY.PLS = ddlPrimaryLightSource.SelectedValue.Trim();
                oOD_LAB_DIP_ENTRY.SLS = ddlSecondaryLightSource.SelectedValue.Trim();
                oOD_LAB_DIP_ENTRY.REMARKS = txtComment.Text.Trim();
                oOD_LAB_DIP_ENTRY.LAB_DIP_STATUS = ddlLabdipStatus.SelectedItem.Text.Trim();
                oOD_LAB_DIP_ENTRY.TUSER = Session["urLoginId"].ToString().Trim();
                oOD_LAB_DIP_ENTRY.TDATE = System.DateTime.Now;
                int NewLABDIPNo = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetNewLABDIPNO(oOD_LAB_DIP_ENTRY);
                txtLabDipNo.Text = NewLABDIPNo.ToString();
                bool bResult = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.InsertODLABDIPENTRY(oOD_LAB_DIP_ENTRY, out iRecordFound);
                if (bResult)
                {
                    BlankControls();
                    getLabdipNo();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Saved');", true);
                }
            }

            else
            {
                Common.CommonFuction.ShowMessage(msg);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Insertdata();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This Function Updates the Record in the table OD_LAB_DIP_ENTRY.
    /// </summary>
    private void Updatedata()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY oOD_LAB_DIP_ENTRY = new SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY();
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
            oOD_LAB_DIP_ENTRY.LAB_DIP_NO = cmbLabDipNo.SelectedValue.Trim();//Convert.ToInt32();
            oOD_LAB_DIP_ENTRY.RECEIVE_DATE = Convert.ToDateTime(txtReceiveDate.Text.ToString());
            oOD_LAB_DIP_ENTRY.DUE_DATE = Convert.ToDateTime(txtDueDate.Text.ToString());
            oOD_LAB_DIP_ENTRY.PRTY_CODE = cmbPartyCode.SelectedValue.Trim();
            oOD_LAB_DIP_ENTRY.FABR_CODE = cmbFabricCode.SelectedValue.Trim();
            oOD_LAB_DIP_ENTRY.SHADE = ddlShadeType.SelectedValue.Trim();
            oOD_LAB_DIP_ENTRY.PRTY_DSG_NO = txtPartyDesignNo.Text.ToString();
            oOD_LAB_DIP_ENTRY.PRTY_DOC_REF_NO = txtPartyRefDoc.Text.ToString();
            oOD_LAB_DIP_ENTRY.PRTY_DOC_DATE = Convert.ToDateTime(txtPartyDocdate.Text.ToString());
            oOD_LAB_DIP_ENTRY.PLS = ddlPrimaryLightSource.SelectedValue.Trim();
            oOD_LAB_DIP_ENTRY.SLS = ddlSecondaryLightSource.SelectedValue.Trim();
            oOD_LAB_DIP_ENTRY.REMARKS = txtComment.Text.Trim();
            oOD_LAB_DIP_ENTRY.LAB_DIP_STATUS = ddlLabdipStatus.SelectedItem.Text.Trim();
            oOD_LAB_DIP_ENTRY.TUSER = Session["urLoginId"].ToString().Trim();
            oOD_LAB_DIP_ENTRY.TDATE = System.DateTime.Now;
            bool bResult = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.UpdateODLABDIPENTRY(oOD_LAB_DIP_ENTRY, out iRecordFound);
            if (bResult)
            {
                BlankControls();
                getLabdipNo();
                cmbLabDipNo.Visible = false;
                txtLabDipNo.Visible = true;
                tdDelete.Visible = false;
                tdUpdate.Visible = false;
                tdSave.Visible = true;
                lblMode.Text = "Save";
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
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Updatedata();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This Function Deletes the Record in the table OD_LAB_DIP_ENTRY.
    /// </summary>
    private void Deletedata()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY oOD_LAB_DIP_ENTRY = new SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY();
            //SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
            oOD_LAB_DIP_ENTRY.LAB_DIP_NO = txtLabDipNo.Text.ToString();//Convert.ToInt32();
            oOD_LAB_DIP_ENTRY.TUSER = Session["urLoginId"].ToString().Trim();
            oOD_LAB_DIP_ENTRY.TDATE = System.DateTime.Now;
            bool bResult = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.DeleteODLABDIPENTRY(oOD_LAB_DIP_ENTRY, out iRecordFound);
            if (bResult)
            {
                BlankControls();
                getLabdipNo();
                cmbLabDipNo.Visible = false;
                txtLabDipNo.Visible = true;
                tdDelete.Visible = false;
                tdUpdate.Visible = false;
                tdSave.Visible = true;
                lblMode.Text = "Save";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted Successfully');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Deleted');", true);
            }
        }
        catch (Exception ex)
        {
            throw ex;
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// This function generates new Lab Dip No and binds the neww Labdip No in the textbox on page load. 
    /// </summary>
    private void getLabdipNo()
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY oOD_LAB_DIP_ENTRY = new SaitexDM.Common.DataModel.OD_LAB_DIP_ENTRY();
            oOD_LAB_DIP_ENTRY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oOD_LAB_DIP_ENTRY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oOD_LAB_DIP_ENTRY.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
            int NewLABDIPNo = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetNewLABDIPNO(oOD_LAB_DIP_ENTRY);
            txtLabDipNo.Text = NewLABDIPNo.ToString();
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
            BlankControls();
            getLabdipNo();
            cmbLabDipNo.Visible = true;
            txtLabDipNo.Visible = false;
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            lblMode.Text = "Update";
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
            string URL = "../Reports/LabDipEntryReport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
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
            BlankControls();
            getLabdipNo();
            cmbLabDipNo.Visible = false;
            txtLabDipNo.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdSave.Visible = true;
            lblMode.Text = "Save";

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void cmbPartyCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItemsPC(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbPartyCode.Items.Clear();
            cmbPartyCode.DataSource = data;
            cmbPartyCode.DataBind();

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbFabricCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {

            DataTable data = new DataTable();
            data = GetItemsFC(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbFabricCode.Items.Clear();
            cmbFabricCode.DataSource = data;
            cmbFabricCode.DataBind();
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbPartyCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string pc = cmbPartyCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectPartyCode(pc);
            if (dt != null && dt.Rows.Count > 0)
            {

                txtPartyCode.Text = dt.Rows[0]["PRTY_NAME"].ToString() + ", " + dt.Rows[0]["PRTY_ADD1"].ToString();

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbFabricCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string fc = cmbFabricCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectFabricCode(fc);
            if (dt != null && dt.Rows.Count > 0)
            {

                txtFabricCode.Text = dt.Rows[0]["FABR_DESC"].ToString();

            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    ///In this function corresponding PartyAddress is extracted with the help of PartyCode.
    /// </summary>
    /// <param name="PCode">PRTY_CODE</param>
    private void partyAdd(string PCode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectPartyAdd(PCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtPartyCode.Text = dt.Rows[0]["PRTY_NAME"].ToString() + ", " + dt.Rows[0]["PRTY_ADD1"].ToString();


            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    /// <summary>
    /// In this function corresponding Fabric Description is extracted with the help of FabricCode.
    /// </summary>
    /// <param name="FCode">FabricCode</param>
    private void FDesc(string FCode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectFDesc(FCode);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFabricCode.Text = dt.Rows[0]["FDESC"].ToString();

            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbLabDipNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems(e.Text.ToUpper(), e.ItemsOffset, 10);
            cmbLabDipNo.Items.Clear();
            cmbLabDipNo.DataSource = data;
            cmbLabDipNo.DataBind();
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text.ToUpper());
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbLabDipNo_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            int LabdNo = Convert.ToInt32(cmbLabDipNo.SelectedValue.Trim());
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectLDipNo(LabdNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtLabDipNo.Text = dt.Rows[0]["LAB_DIP_NO"].ToString();
                txtPartyDesignNo.Text = dt.Rows[0]["PRTY_DSG_NO"].ToString();
                txtPartyRefDoc.Text = dt.Rows[0]["PRTY_DOC_REF_NO"].ToString();
                txtPartyDocdate.Text = dt.Rows[0]["PRTY_DOC_DATE"].ToString();
                txtReceiveDate.Text = dt.Rows[0]["RECEIVE_DATE"].ToString();
                txtShade.Text = dt.Rows[0]["SHADE"].ToString();
                txtDueDate.Text = dt.Rows[0]["DUE_DATE"].ToString();
                txtComment.Text = dt.Rows[0]["REMARKS"].ToString();
                cmbFabricCode.SelectedValue = dt.Rows[0]["FABR_CODE"].ToString();
                cmbPartyCode.SelectedValue = dt.Rows[0]["PRTY_CODE"].ToString();
                string Pcode = dt.Rows[0]["PRTY_CODE"].ToString();
                string Fcode = dt.Rows[0]["FABR_CODE"].ToString();
                partyAdd(Pcode);
                FDesc(Fcode);
                ddlShadeType.SelectedValue = dt.Rows[0]["SHADE_TYPE"].ToString();
                ddlPrimaryLightSource.SelectedValue = dt.Rows[0]["PLS"].ToString();
                ddlSecondaryLightSource.SelectedValue = dt.Rows[0]["SLS"].ToString();
                ddlLabdipStatus.SelectedValue = dt.Rows[0]["LAB_DIP_STATUS"].ToString();

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbPartyCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string pc = cmbPartyCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectPartyCode(pc);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtPartyCode.Text = dt.Rows[0]["PRTY_ADD1"].ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void cmbFabricCode_TextChanged(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string fc = cmbFabricCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.SelectFabricCode(fc);
            if (dt != null && dt.Rows.Count > 0)
            {
                txtFabricCode.Text = dt.Rows[0]["FDESC"].ToString();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void txtDueDate_TextChanged(object sender, EventArgs e)
    {

    }
    //protected void cmbPartyCode_TextChanged1(object sender, EventArgs e)
    //{

    //}

    protected DataTable GetItemsforPLS(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " where MST_NAME='LIGHT_SOURCE' and MST_CODE like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select MST_CODE from TX_MASTER_TRN";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }
    protected DataTable GetItemsforShadeType(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " where MST_NAME='SHADE_TYPE' and MST_CODE like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = "";
        string commandText = "select MST_CODE from TX_MASTER_TRN";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;
    }

    protected void ddlPrimaryLightSource_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItemsforPLS(e.Text.ToUpper(), e.ItemsOffset, 10);

        ddlPrimaryLightSource.Items.Clear();
        ddlPrimaryLightSource.DataSource = data;
        ddlPrimaryLightSource.DataBind();
    }
    protected void ddlSecondaryLightSource_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItemsforPLS(e.Text.ToUpper(), e.ItemsOffset, 10);

        ddlSecondaryLightSource.Items.Clear();
        ddlSecondaryLightSource.DataSource = data;
        ddlSecondaryLightSource.DataBind();
    }
    protected void ddlShadeType_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItemsforShadeType(e.Text.ToUpper(), e.ItemsOffset, 10);

        ddlShadeType.Items.Clear();
        ddlShadeType.DataSource = data;
        ddlShadeType.DataBind();
    }
}
