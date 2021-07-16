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
using DBLibrary;
using Common;
using errorLog;

public partial class Module_HRMS_Controls_Employee_Family : System.Web.UI.UserControl
{
    private static DataTable DTEmpTable = null;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (!Page.IsPostBack)
                {
                    InitialisePage();
                    if (Request.QueryString["EMP_CODE"] != null && Request.QueryString["EMP_CODE"] != "")
                    {
                        string EMP_Code = Request.QueryString["EMP_CODE"].ToString();
                        Employee_Name(EMP_Code);
                        getEmployeeFamilyData(EMP_Code);
                    }
                    else
                    {
                        lblMode.Text = "Save";
                    }
                }
            }
            else
            {
                Response.Redirect("/Saitex/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Page Loading.\r\nSee error log for detail."));
        }
    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            RefreshDetailRow();
            Bind_Relation("RELATION_TYPE");
            if (DTEmpTable == null || DTEmpTable.Rows.Count == 0)
                CreateEmpFamilyDetailTable();
            DTEmpTable.Rows.Clear();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
        }
        catch
        {
            throw;
        }
    }

    private void CreateEmpFamilyDetailTable()
    {
        DTEmpTable = new DataTable();
        DTEmpTable.Columns.Add("UniqueId", typeof(int));
        DTEmpTable.Columns.Add("EMP_DEPEND_ID", typeof(int));
        DTEmpTable.Columns.Add("EMP_CODE", typeof(string));
        DTEmpTable.Columns.Add("I_F_NAME", typeof(string));
        DTEmpTable.Columns.Add("I_L_NAME", typeof(string));
        DTEmpTable.Columns.Add("I_DOB", typeof(string));
        DTEmpTable.Columns.Add("I_SEX", typeof(string));
        DTEmpTable.Columns.Add("Age", typeof(string));
        DTEmpTable.Columns.Add("I_RELATION", typeof(string));

    }
    private void Employee_Name(string EMP_CODE)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Name(EMP_CODE);
            LblEmpCode.Text = DTable.Rows[0]["EMP_CODE"].ToString();
            LblEmpName.Text = DTable.Rows[0]["EMPLOYEENAME"].ToString();

        }
        catch
        {
            throw;
        }

    }
    private void getEmployeeFamilyData(string iEMPMasterCode)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_FAMILY_IND.GetFamilyDetail(iEMPMasterCode);
            foreach (DataRow Drow in DTable.Rows)
            {
                tdUpdate.Visible = true;
                lblMode.Text = "Update";
                tdSave.Visible = false;
                DataRow dr;
                dr = DTEmpTable.NewRow();
                dr["UniqueId"] = DTEmpTable.Rows.Count + 1;
                dr["EMP_DEPEND_ID"] = Drow["EMP_DEPEND_ID"];
                dr["EMP_CODE"] = Drow["EMP_CODE"].ToString();
                dr["I_F_NAME"] = Drow["I_F_NAME"].ToString();
                dr["I_L_NAME"] = Drow["I_L_NAME"].ToString();
                dr["I_DOB"] = string.Format("{0:MM/dd/yyyy}", DateTime.Parse(Drow["I_DOB"].ToString()));
                dr["I_SEX"] = Drow["I_SEX"].ToString();
                dr["Age"] = Drow["Age"].ToString();
                dr["I_RELATION"] = Drow["I_RELATION"].ToString();
                DTEmpTable.Rows.Add(dr);
            }
            grdEmpFamilyDetail.DataSource = DTEmpTable;
            grdEmpFamilyDetail.DataBind();
        }
        catch
        {
            throw;
        }

    }
    private void BindDetailGrid()
    {
        try
        {
            grdEmpFamilyDetail.DataSource = DTEmpTable;
            grdEmpFamilyDetail.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    private void RefreshDetailRow()
    {
        txtdob.Text = "";
        txtfname.Text = "";
        txtfname.Focus();
        txtlname.Text = "";
        txtAge.Text = "";
        DDLRelation.SelectedIndex = 0;
        DDLSEX.SelectedIndex = -1;
        ViewState["UniqueId"] = null;
    }
    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            int UID = 0;
            if (ViewState["UniqueId"] != null)
            {
                UID = int.Parse(ViewState["UniqueId"].ToString());
            }
            if (Can_Bind_FamilyDetail(UID))
            {
                if (UID > 0)
                {
                    DataView dv = new DataView(DTEmpTable);
                    dv.RowFilter = "UniqueId=" + UID;
                    if (dv.Count > 0)
                    {
                        dv[0]["EMP_CODE"] = LblEmpCode.Text.Trim().ToString();
                        dv[0]["I_F_NAME"] = txtfname.Text.Trim().ToString();
                        dv[0]["I_L_NAME"] = txtlname.Text.Trim().ToString();
                        dv[0]["I_DOB"] = txtdob.Text.Trim().ToString();
                        dv[0]["Age"] = txtAge.Text.Trim().ToString();
                        dv[0]["I_SEX"] = DDLSEX.SelectedValue.Trim();
                        dv[0]["I_RELATION"] = DDLRelation.SelectedValue.ToString().ToUpper();
                        DTEmpTable.AcceptChanges();
                    }
                }
                else
                {
                    DataRow dr;
                    dr = DTEmpTable.NewRow();
                    dr["UniqueId"] = DTEmpTable.Rows.Count + 1;
                    dr["EMP_CODE"] = LblEmpCode.Text.Trim().ToString();
                    dr["I_F_NAME"] = txtfname.Text.Trim().ToString();
                    dr["I_L_NAME"] = txtlname.Text.Trim().ToString();
                    dr["I_DOB"] = txtdob.Text.Trim().ToString();
                    dr["Age"] = txtAge.Text.Trim().ToString();
                    dr["I_SEX"] = DDLSEX.SelectedValue.Trim();
                    dr["I_RELATION"] = DDLRelation.SelectedValue.ToString().ToUpper();
                    DTEmpTable.Rows.Add(dr);
                }
                RefreshDetailRow();
            }
            BindDetailGrid();
            ViewState.Clear();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }
    private bool Can_Bind_FamilyDetail(int uid)
    {
        bool Res = true;
        try
        {
            if (txtfname.Text != "" && DDLSEX.SelectedValue != "" && DDLRelation.SelectedValue.ToString() != "")
            {
                if (DTEmpTable.Rows.Count > 0)
                {
                    foreach (DataRow Drow in DTEmpTable.Rows)
                    {
                        if (uid != int.Parse(Drow["UniqueId"].ToString()))
                        {
                            if (txtfname.Text.Trim().ToUpper() == Drow["I_F_NAME"].ToString().ToUpper())
                            {
                                Common.CommonFuction.ShowMessage("Duplicate Name!Please change Name");
                                Res = false;
                            }
                            if (DDLRelation.SelectedValue.ToString().ToUpper() == "FATHER" && DDLRelation.SelectedValue.ToString().ToUpper() == Drow["I_RELATION"].ToString().ToUpper())
                            {
                                Common.CommonFuction.ShowMessage("Duplicate Father relation!Please change relation");
                                Res = false;
                            }
                        }
                    }
                }
                else
                {
                    Res = true;
                }
            }
            else
            {
                Res = false;
            }
            return Res;

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
            return false;
        }
    }
    public void Bind_Relation(string MST_NAME)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DDLRelation.Items.Clear();
                DDLRelation.DataSource = dt;
                DDLRelation.DataTextField = "MST_CODE";
                DDLRelation.DataValueField = "MST_CODE";
                DDLRelation.DataBind();
            }

        }
        catch
        {
            throw;
        }
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            RefreshDetailRow();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clear the Record"));
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (DTEmpTable.Rows.Count > 0)
                {
                    if (Save_Family_Detail())
                    {
                        Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Unable to save!try agin");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No Items selected. Please enter item detail");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving Record"));
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (DTEmpTable.Rows.Count > 0)
                {
                    if (Save_Family_Detail())
                    {
                        Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Unable to save!try agin");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("No Items selected. Please enter item detail");
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating Record"));
            throw;
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        RefreshDetailRow();
    }
    protected void grdEmpFamilyDetail_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UID = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                FillDetailByGrid(UID);
            }
            else if (e.CommandName == "EmpDelete")
            {
                DeleteFamilyDetailRow(UID);
                BindDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void FillDetailByGrid(int UID)
    {
        try
        {
            DataView dv = new DataView(DTEmpTable);
            dv.RowFilter = "UniqueId=" + UID;
            if (dv.Count > 0)
            {
                DDLSEX.SelectedValue = dv[0]["I_SEX"].ToString();
                txtfname.Text = dv[0]["I_F_NAME"].ToString();
                txtlname.Text = dv[0]["I_L_NAME"].ToString();
                DDLRelation.SelectedValue = dv[0]["I_RELATION"].ToString().ToUpper();
                txtdob.Text = dv[0]["I_DOB"].ToString();
                txtAge.Text = dv[0]["Age"].ToString();
                ViewState["UniqueId"] = UID;
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private void DeleteFamilyDetailRow(int UID)
    {
        try
        {
            if (grdEmpFamilyDetail.Rows.Count == 1)
            {
                DTEmpTable.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in DTEmpTable.Rows)
                {
                    int iUID = int.Parse(dr["UniqueId"].ToString());
                    if (iUID == UID)
                    {
                        DTEmpTable.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in DTEmpTable.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private bool FillDataTableByGrid()
    {
        try
        {
            bool result = true;
            if (grdEmpFamilyDetail.Rows.Count > 0)
            {
                DTEmpTable.Rows.Clear();
                foreach (GridViewRow grdRow in grdEmpFamilyDetail.Rows)
                {
                    Label LblFirstname = (Label)grdRow.FindControl("LblFirstname");
                    Label LblLastName = (Label)grdRow.FindControl("LblLastName");
                    Label LblDateOfBirth = (Label)grdRow.FindControl("LblDateOfBirth");
                    Label LblAge = (Label)grdRow.FindControl("LblAge");
                    Label LblSex = (Label)grdRow.FindControl("LblSex");
                    Label LblRelationGrid = (Label)grdRow.FindControl("LblRelationGrid");

                    if (LblFirstname.Text != "" && LblLastName.Text != "" && LblDateOfBirth.Text != "" && LblSex.Text != "" && LblRelationGrid.Text != "")
                    {
                        DataRow dr = DTEmpTable.NewRow();
                        dr["UniqueId"] = DTEmpTable.Rows.Count + 1;
                        dr["I_F_NAME"] = LblFirstname.Text;
                        dr["I_L_NAME"] = LblLastName.Text.Trim();
                        dr["I_DOB"] = DateTime.Parse(LblDateOfBirth.Text.Trim());
                        dr["Age"] = decimal.Parse(LblAge.Text.Trim());
                        dr["I_SEX"] = LblSex.Text.Trim();
                        dr["I_RELATION"] = LblRelationGrid.Text.Trim();
                        DTEmpTable.Rows.Add(dr);
                    }
                }
            }
            return result;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }
    private bool Save_Family_Detail()
    {
        try
        {
            bool Res = SaitexBL.Interface.Method.HR_EMP_FAMILY_IND.Save_Family_Detail(DTEmpTable);
            return Res;
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw;
        }
    }

}
