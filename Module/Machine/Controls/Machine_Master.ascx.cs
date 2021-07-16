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


public partial class Module_Machine_Controls_Machine_Master : System.Web.UI.UserControl
{
    int count;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }
    private void InitialisePage()
    {
        try
        {
            Blankcontrols();
           
            BindUOM(ddlUOMAir);
            BindUOM(ddlUOMMachineCapacity);
            BindUOM(ddlUOMCount);
            BindUOM(ddlUOMNoOfHeads);
            BindUOM(ddlUOMManpower);
            BindUOM(ddlUOMMaxPackage);
            BindUOM(ddlUOMNoOfPackages);
            BindUOM(ddlUOMPower);
            BindUOM(ddlUOMSoftwater);
            BindUOM(ddlUOMNoOfSpindles);
            BindUOM(ddlUOMSteam);
            BindUOM(ddlUOMMachineSpeed);
            BindUOM(ddlUomCoal);
            
            
            lblMode.Text = "Save";            
            //txtMachineCode.Visible = false;
            cmbMachineCode.Visible = false;
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdFind.Visible = true;
            tdDelete.Visible = false;
            bindDDLMachineGroup();
            bindCMBMachineCode();
            txtNos.Visible = true;
            dvMachineDetail.Visible = false;
            lblNoOfMachine.Text = "No Of Machines :";

            ddlUOMAir.SelectedIndex = -1;
            ddlUOMCount.SelectedIndex = -1;
            ddlUOMMachineCapacity.SelectedIndex = -1;
            ddlUOMMachineSpeed.SelectedIndex = -1;
            ddlUOMManpower.SelectedIndex = -1;
            ddlUOMMaxPackage.SelectedIndex = -1;
            ddlUOMNoOfHeads.SelectedIndex = -1;
            ddlUOMNoOfPackages.SelectedIndex = -1;
            ddlUOMNoOfSpindles.SelectedIndex = -1;
            ddlUOMPower.SelectedIndex = -1;
            ddlUOMSoftwater.SelectedIndex = -1;
            ddlUOMSteam.SelectedIndex = -1;

            //txtMachineCode.Visible = true;
            //chkActive.Checked = true

        }
        catch
        {
            throw;
        }
    }

   
    
    


    private void BindUOM(DropDownList ddl)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectUOM();
            ddl.DataSource = dt;
            ddl.DataValueField = "MST_CODE";
            ddl.DataTextField = "MST_CODE";
            ddl.DataBind();
            ddl.Items.Insert(0, "UOM");
        }
        catch (Exception ex)
        {
            throw ex;

        }
    
    }
   
    
    
    private void bindDDLMachineGroup()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineGroup();
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
    private void bindCMBMachineCode()
    {
        try
        {

            DataTable dt = new DataTable();
            //string MACHINECODE = cmbMachineCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineCode();
            cmbMachineCode.Items.Clear();
            cmbMachineCode.DataSource = dt;
            cmbMachineCode.DataValueField = "MACHINE_CODE";
            cmbMachineCode.DataTextField = "MACHINE_CODE";
            cmbMachineCode.DataBind();

        }
        catch (Exception ex)
        {
            throw ex;

        }

    }
    protected DataTable GetItemsMG(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE MACHINE_GROUP like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = " order by MACHINE_GROUP ";
        string commandText = "select distinct * from MC_MACHINE_GRP";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    protected DataTable GetItemsMC(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE MACHINE_CODE like :SearchQuery and DEL_STATUS='0'";
        string sortExpression = " order by MACHINE_CODE desc";
        string commandText = "select distinct MACHINE_CODE from MC_MACHINE_MASTER";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_LV_TRN.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', sPO);
        return dt;

    }
    private void Insertdata()
    {
        try
        {
            int iRecordFound = 0;
            string alphaTex = "ABCDEFGHIJKLMNOPQRSTUVQXYZ";
            string alphaTwist = "ULABCDEFGHIJKMNOPQRSTVQXYZ";
            int _no_of_sides=0;
            int.TryParse(txtNoOfHeads.Text,out _no_of_sides);
            if (_no_of_sides <= 0)
            {
                _no_of_sides = 1;
            }

            string msg = string.Empty;

            SaitexDM.Common.DataModel.MC_MACHINE_MASTER oMC_MACHINE_MASTER = new SaitexDM.Common.DataModel.MC_MACHINE_MASTER();
            oMC_MACHINE_MASTER.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oMC_MACHINE_MASTER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;   
            if (ValidateDetaiRow(out msg))
            {
                msg = string.Empty;
                if (cmbMachineGroup.SelectedIndex >= 0)
                {
                    for (count = 1; count <= Convert.ToInt32(txtNos.Text); count++)
                    {
                        string mg = cmbMachineGroup.SelectedValue.Trim();
                        string strNewMachineGroupCode = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetNewMachineCode(mg,_no_of_sides);
                        for (int i = 0; i < _no_of_sides; i++)
                        {
                           
                                                 
                           
                            if (lblSection.Text.Trim() == "TEXTURISING")
                            {
                                oMC_MACHINE_MASTER.MACHINE_CODE = strNewMachineGroupCode.ToString() +"-"+ alphaTex[i].ToString();
                            }
                           //else if (lblSection.Text.Trim() == "TWISTING")
                           // {
                           //     oMC_MACHINE_MASTER.MACHINE_CODE = strNewMachineGroupCode.ToString() + "-" + alphaTwist[i].ToString();
                           // }
                            else 
                            {
                                oMC_MACHINE_MASTER.MACHINE_CODE = strNewMachineGroupCode.ToString();
                            }
                            int NoofPackages = 0;
                            int.TryParse(txtNoOfPackage.Text.Trim(), out NoofPackages);
                            oMC_MACHINE_MASTER.NO_OF_PACKAGES = NoofPackages / _no_of_sides;
                            oMC_MACHINE_MASTER.UOM_NO_OF_PACKAGES = ddlUOMNoOfPackages.SelectedValue.Trim();
                            oMC_MACHINE_MASTER.MACHINE_GROUP = (cmbMachineGroup.SelectedValue.Trim());
                            int MachineCapacity = 0;
                            int.TryParse(txtMachineCapacity.Text.Trim(), out MachineCapacity);
                            oMC_MACHINE_MASTER.MACHINE_CAPACITY = MachineCapacity / _no_of_sides;
                            oMC_MACHINE_MASTER.UOM_CAPACITY = ddlUOMMachineCapacity.SelectedValue.Trim();
                            oMC_MACHINE_MASTER.MACHINE_MAKE = (txtMachineMake.Text.Trim());
                            int MachineSpeed = 0;
                            int.TryParse(txtMachineSpeed.Text.Trim(), out MachineSpeed);
                            oMC_MACHINE_MASTER.MACHINE_SPEED = MachineSpeed;
                            oMC_MACHINE_MASTER.UOM_SPEED = ddlUOMMachineSpeed.SelectedValue.Trim();
                            int NoOfSpindles = 0;
                            int.TryParse(txtNoOfSpindles.Text.Trim(), out NoOfSpindles);
                            oMC_MACHINE_MASTER.NO_OF_SPINDLES = NoOfSpindles / _no_of_sides;
                            oMC_MACHINE_MASTER.UOM_NO_OF_SPINDLES = ddlUOMNoOfSpindles.SelectedValue.Trim();
                            int ManpowerDay = 0;
                            int.TryParse(txtManpowerDay.Text.Trim(), out ManpowerDay);
                            oMC_MACHINE_MASTER.MANPOWER = ManpowerDay / _no_of_sides;
                            oMC_MACHINE_MASTER.UOM_MANPOWER = ddlUOMManpower.SelectedValue.Trim();
                            int NoOfHeads = 0;
                            int.TryParse(txtNoOfHeads.Text.Trim(), out NoOfHeads);
                            oMC_MACHINE_MASTER.NO_OF_HEADS = NoOfHeads;
                            oMC_MACHINE_MASTER.UOM_NO_OF_HEADS = ddlUOMNoOfHeads.SelectedValue.Trim();
                            int Steam = 0;
                            int.TryParse(txtSteam.Text.Trim(), out Steam);
                            oMC_MACHINE_MASTER.STEAM = Steam;
                            oMC_MACHINE_MASTER.UOM_STEAM = ddlUOMSteam.SelectedValue.Trim();
                            int SoftWater = 0;
                            int.TryParse(txtSoftWater.Text.Trim(), out SoftWater);
                            oMC_MACHINE_MASTER.SOFTWATER = SoftWater;
                            oMC_MACHINE_MASTER.UOM_SOFTWATER = ddlUOMSoftwater.SelectedValue.Trim();
                            int Air = 0;
                            int.TryParse(txtAir.Text.Trim(), out Air);
                            oMC_MACHINE_MASTER.AIR = Air;
                            oMC_MACHINE_MASTER.UOM_AIR = ddlUOMAir.SelectedValue.Trim();
                            int Power = 0;
                            int.TryParse(txtPower.Text.Trim(), out Power);
                            oMC_MACHINE_MASTER.MACHINE_POWER = Power;
                            oMC_MACHINE_MASTER.UOM_MACHINE_POWER = ddlUOMPower.SelectedValue.Trim();
                            int YOM = 0;
                            int.TryParse(txtYOM.Text.Trim(), out YOM);
                            oMC_MACHINE_MASTER.YOM = YOM;
                            int CountRatio = 0;
                            int.TryParse(txtCount.Text.Trim(), out CountRatio);
                            oMC_MACHINE_MASTER.COUNT_PROD_RATIO = CountRatio;
                            oMC_MACHINE_MASTER.UOM_COUNT_PROD_RATIO = ddlUOMCount.SelectedValue.Trim();
                            int MaximumPackage = 0;
                            int.TryParse(txtMaximumPackage.Text.Trim(), out MaximumPackage);
                            oMC_MACHINE_MASTER.MAX_PACKAGE = MaximumPackage;
                            oMC_MACHINE_MASTER.UOM_MAX_PACKAGE = ddlUOMMaxPackage.SelectedValue.Trim();
                            oMC_MACHINE_MASTER.NO_OF_MACHINES = Convert.ToInt32(txtNos.Text.Trim());
                            oMC_MACHINE_MASTER.TUSER = Session["urLoginId"].ToString().Trim();
                            oMC_MACHINE_MASTER.TDATE = System.DateTime.Now;
                            oMC_MACHINE_MASTER.SUPPLIER = txtSupplier.Text.Trim();
                            int coal = 0;
                            int.TryParse(txtCoal.Text.Trim(), out coal);
                            oMC_MACHINE_MASTER.COAL = coal;
                            oMC_MACHINE_MASTER.UOM_COAL = ddlUomCoal.SelectedValue.Trim();


                            bool bResult = SaitexBL.Interface.Method.MC_MACHINE_MASTER.InsertMachineMaster(oMC_MACHINE_MASTER, out iRecordFound);
                            if (bResult)
                            {
                                //msg += @"\r\n Machine Code " + strNewMachineGroupCode + " saved successfully";
                                msg += @"\r\n Machine Code " + oMC_MACHINE_MASTER.MACHINE_CODE + " saved successfully";
                            }

                            else
                            {
                                //msg += @"\r\n Machine Code " + strNewMachineGroupCode + " saving failed";
                                msg += @"\r\n Machine Code " + oMC_MACHINE_MASTER.MACHINE_CODE + " saving failed";
                            }
                        }
                    }
                    cmbMachineGroup.Items.Clear();
                    bindDDLMachineGroup();
                    cmbMachineGroup.SelectedIndex = -1;
                    InitialisePage();
                    Common.CommonFuction.ShowMessage(msg);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Select Machine Group');", true);
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
    private void Updatedata()
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.MC_MACHINE_MASTER oMC_MACHINE_MASTER = new SaitexDM.Common.DataModel.MC_MACHINE_MASTER();
            // strNewLeaveId = SaitexBL.Interface.Method.HR_LV_MST.GetNewLeaveId();
            oMC_MACHINE_MASTER.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oMC_MACHINE_MASTER.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oMC_MACHINE_MASTER.MACHINE_CODE = cmbMachineCode.SelectedValue.Trim();
            int NoofPackages = 0;
            int.TryParse(txtNoOfPackage.Text.Trim(), out NoofPackages);
            oMC_MACHINE_MASTER.NO_OF_PACKAGES = NoofPackages;
            oMC_MACHINE_MASTER.UOM_NO_OF_PACKAGES = ddlUOMNoOfPackages.SelectedValue.Trim();
            oMC_MACHINE_MASTER.MACHINE_GROUP = (cmbMachineGroup.SelectedValue.Trim());
            int MachineCapacity = 0;
            int.TryParse(txtMachineCapacity.Text.Trim(), out MachineCapacity);
            oMC_MACHINE_MASTER.MACHINE_CAPACITY = MachineCapacity;
            oMC_MACHINE_MASTER.UOM_CAPACITY = ddlUOMMachineCapacity.SelectedValue.Trim();
            oMC_MACHINE_MASTER.MACHINE_MAKE = (txtMachineMake.Text.Trim());
            int MachineSpeed = 0;
            int.TryParse(txtMachineSpeed.Text.Trim(), out MachineSpeed);
            oMC_MACHINE_MASTER.MACHINE_SPEED = MachineSpeed;
            oMC_MACHINE_MASTER.UOM_SPEED = ddlUOMMachineSpeed.SelectedValue.Trim();
            int NoOfSpindles = 0;
            int.TryParse(txtNoOfSpindles.Text.Trim(), out NoOfSpindles);
            oMC_MACHINE_MASTER.NO_OF_SPINDLES = NoOfSpindles;
            oMC_MACHINE_MASTER.UOM_NO_OF_SPINDLES = ddlUOMNoOfSpindles.SelectedValue.Trim();
            int ManpowerDay = 0;
            int.TryParse(txtManpowerDay.Text.Trim(), out ManpowerDay);
            oMC_MACHINE_MASTER.MANPOWER = ManpowerDay;
            oMC_MACHINE_MASTER.UOM_MANPOWER = ddlUOMManpower.SelectedValue.Trim();
            int NoOfHeads = 0;
            int.TryParse(txtNoOfHeads.Text.Trim(), out NoOfHeads);
            oMC_MACHINE_MASTER.NO_OF_HEADS = NoOfHeads;
            oMC_MACHINE_MASTER.UOM_NO_OF_HEADS = ddlUOMNoOfHeads.SelectedValue.Trim();
            int Steam = 0;
            int.TryParse(txtSteam.Text.Trim(), out Steam);
            oMC_MACHINE_MASTER.STEAM = Steam;
            oMC_MACHINE_MASTER.UOM_STEAM = ddlUOMSteam.SelectedValue.Trim();
            int SoftWater = 0;
            int.TryParse(txtSoftWater.Text.Trim(), out SoftWater);
            oMC_MACHINE_MASTER.SOFTWATER = SoftWater;
            oMC_MACHINE_MASTER.UOM_SOFTWATER = ddlUOMSoftwater.SelectedValue.Trim();
            int Air = 0;
            int.TryParse(txtAir.Text.Trim(), out Air);
            oMC_MACHINE_MASTER.AIR = Air;
            oMC_MACHINE_MASTER.UOM_AIR = ddlUOMAir.SelectedValue.Trim();
            int Power = 0;
            int.TryParse(txtPower.Text.Trim(), out Power);
            oMC_MACHINE_MASTER.MACHINE_POWER = Power;
            oMC_MACHINE_MASTER.UOM_MACHINE_POWER = ddlUOMPower.SelectedValue.Trim();

            int YOM = 0;
            int.TryParse(txtYOM.Text.Trim(), out YOM);
            oMC_MACHINE_MASTER.YOM = YOM;
            int CountRatio = 0;
            int.TryParse(txtCount.Text.Trim(), out CountRatio);
            oMC_MACHINE_MASTER.COUNT_PROD_RATIO = CountRatio;

            oMC_MACHINE_MASTER.UOM_COUNT_PROD_RATIO = ddlUOMCount.SelectedValue.Trim();
            int MaximumPackage = 0;
            int.TryParse(txtMaximumPackage.Text.Trim(), out MaximumPackage);
            oMC_MACHINE_MASTER.MAX_PACKAGE = MaximumPackage;
            oMC_MACHINE_MASTER.UOM_MAX_PACKAGE = ddlUOMMaxPackage.SelectedValue.Trim();
            //oMC_MACHINE_MASTER.STATUS = chkActive.Checked;
            oMC_MACHINE_MASTER.TUSER = Session["urLoginId"].ToString().Trim();
            oMC_MACHINE_MASTER.TDATE = System.DateTime.Now;
            oMC_MACHINE_MASTER.SUPPLIER = txtSupplier.Text.Trim();
            int coal = 0;
            int.TryParse(txtCoal.Text.Trim(), out coal);
            oMC_MACHINE_MASTER.COAL = coal;
            oMC_MACHINE_MASTER.UOM_COAL = ddlUomCoal.SelectedValue.Trim();
            bool bResult = SaitexBL.Interface.Method.MC_MACHINE_MASTER.UpdateMachineMaster(oMC_MACHINE_MASTER, out iRecordFound);
            if (bResult)
            {
                cmbMachineGroup.Items.Clear();
                bindDDLMachineGroup();
                cmbMachineGroup.SelectedIndex = -1;
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
    private void Deletedata()
    {
        try
        {

            int iRecordFound = 0;
            SaitexDM.Common.DataModel.MC_MACHINE_MASTER oMC_MACHINE_MASTER = new SaitexDM.Common.DataModel.MC_MACHINE_MASTER();
            // strNewLeaveId = SaitexBL.Interface.Method.HR_LV_MST.GetNewLeaveId();
            oMC_MACHINE_MASTER.MACHINE_CODE = (cmbMachineCode.SelectedValue.Trim());
            //oMC_MACHINE_MASTER.STATUS = chkActive.Checked;
            oMC_MACHINE_MASTER.TUSER = Session["urLoginId"].ToString().Trim();
            //oMC_MACHINE_MASTER.TDATE = System.DateTime.Now;
            bool bResult = SaitexBL.Interface.Method.MC_MACHINE_MASTER.DeleteMachineMaster(oMC_MACHINE_MASTER, out iRecordFound);
            if (bResult)
            {
                InitialisePage();
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
    private void Blankcontrols()
    {
        cmbMachineGroup.SelectedIndex = -1;
        cmbMachineCode.SelectedIndex = -1;
        txtAir.Text = "";
        txtCount.Text = "";
        txtMachineCapacity.Text = "";
        txtNos.Text = "";
        // txtMachineCode.Text = "";
        txtMachineSpeed.Text = "";
        txtManpowerDay.Text = "";
        txtMaximumPackage.Text = "";
        txtNoOfHeads.Text = "";
        txtNoOfPackage.Text = "";
        txtNoOfSpindles.Text = "";
        txtPower.Text = "";
        txtSoftWater.Text = "";
        txtSteam.Text = "";
        txtYOM.Text = "";
        txtMachineMake.Text = "";
        ddlUOMMachineCapacity.SelectedIndex = -1;
        ddlUOMMachineSpeed.SelectedIndex = -1;
        ddlUOMManpower.SelectedIndex = -1;
        ddlUOMMaxPackage.SelectedIndex = -1;
        ddlUOMNoOfHeads.SelectedIndex = -1;
        ddlUOMNoOfPackages.SelectedIndex = -1;
        ddlUOMNoOfSpindles.SelectedIndex = -1;
        ddlUOMCount.SelectedIndex = -1;
        ddlUOMAir.SelectedIndex = -1;
        ddlUOMPower.SelectedIndex = -1;
        ddlUOMSteam.SelectedIndex = -1;
        ddlUOMSoftwater.SelectedIndex = -1;
        ddlUomCoal.SelectedIndex = -1;
        txtSupplier.Text = string.Empty;
        txtCoal.Text = string.Empty;

    }
    private bool ValidateDetaiRow(out string msg)
    {
        try
        {
            msg = string.Empty;
            bool result = false;

            int count = 0;

            int dd = 0;
            int.TryParse(txtNos.Text.Trim(), out dd);
            if (dd > 0)
                count = count + 1;
            else
            {
                msg = msg + "Enter Numeric value for No of Machines. ";
                txtNos.Text = string.Empty;
            }
            if (count == 1)
                result = true;

            return result;
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
            Insertdata();
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
            throw ex;
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            cmbMachineCode.Visible = true;
            txtNos.Visible = false;
            //txtMachineCode.Visible = false;
            tdDelete.Visible = true;
            tdUpdate.Visible = true;
            tdSave.Visible = false;
            lblMode.Text = "Update";
            lblNoOfMachine.Text = "Search Machine Code";
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Pages/MachineSegment.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            cmbMachineCode.Visible = false;
            txtNos.Visible = true;
            Blankcontrols();
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void cmbMachineGroup_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItemsMG(e.Text.ToUpper(), e.ItemsOffset, 10);

        cmbMachineGroup.Items.Clear();
        cmbMachineGroup.DataSource = data;
        cmbMachineGroup.DataBind();

    }

    protected void cmbMachineCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItemsMC(e.Text.ToUpper(), e.ItemsOffset, 10);

        cmbMachineCode.Items.Clear();
        cmbMachineCode.DataSource = data;
        cmbMachineCode.DataBind();
    }
    protected void cmbMachineCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string MachineCode = cmbMachineCode.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineDetail(MachineCode);
            if (dt != null && dt.Rows.Count > 0)
            {

                cmbMachineGroup.SelectedValue = dt.Rows[0]["MACHINE_GROUP"].ToString();
                if (dt.Rows[0]["AIR"] != null && dt.Rows[0]["AIR"].ToString() != "")
                {
                    txtAir.Text = dt.Rows[0]["AIR"].ToString();
                    if (dt.Rows[0]["UOM_AIR"].ToString() != null && dt.Rows[0]["UOM_AIR"].ToString() != string.Empty)
                    {
                        ddlUOMAir.SelectedValue = dt.Rows[0]["UOM_AIR"].ToString();
                    }
                }
                if (dt.Rows[0]["COUNT_PROD_RATIO"] != null && dt.Rows[0]["COUNT_PROD_RATIO"].ToString() != "")
                {
                    txtCount.Text = dt.Rows[0]["COUNT_PROD_RATIO"].ToString();
                    if (dt.Rows[0]["UOM_COUNT_PROD_RATIO"].ToString() != null && dt.Rows[0]["UOM_COUNT_PROD_RATIO"].ToString() != string.Empty)
                    {
                        ddlUOMCount.SelectedValue = dt.Rows[0]["UOM_COUNT_PROD_RATIO"].ToString();
                    }
                }
                if (dt.Rows[0]["MACHINE_CAPACITY"] != null && dt.Rows[0]["MACHINE_CAPACITY"].ToString() != "")
                {
                    txtMachineCapacity.Text = dt.Rows[0]["MACHINE_CAPACITY"].ToString();
                    if (dt.Rows[0]["UOM_CAPACITY"].ToString() != null && dt.Rows[0]["UOM_CAPACITY"].ToString() != string.Empty)
                    {
                        ddlUOMMachineCapacity.SelectedValue = dt.Rows[0]["UOM_CAPACITY"].ToString();
                    }
                }
                if (dt.Rows[0]["MACHINE_SPEED"] != null && dt.Rows[0]["MACHINE_SPEED"].ToString() != "")
                {
                    txtMachineSpeed.Text = dt.Rows[0]["MACHINE_SPEED"].ToString();
                    if (dt.Rows[0]["UOM_SPEED"].ToString() != null && dt.Rows[0]["UOM_SPEED"].ToString() != string.Empty)
                    {
                        ddlUOMMachineSpeed.SelectedValue = dt.Rows[0]["UOM_SPEED"].ToString();
                    }
                }
                if (dt.Rows[0]["MANPOWER"] != null && dt.Rows[0]["MANPOWER"].ToString() != "")
                {
                    txtManpowerDay.Text = dt.Rows[0]["MANPOWER"].ToString();
                    if (dt.Rows[0]["UOM_MANPOWER"].ToString() != null && dt.Rows[0]["UOM_MANPOWER"].ToString() != string.Empty)
                    {
                        ddlUOMManpower.SelectedValue = dt.Rows[0]["UOM_MANPOWER"].ToString();
                    }
                }
                if (dt.Rows[0]["MAX_PACKAGE"] != null && dt.Rows[0]["MAX_PACKAGE"].ToString() != "")
                {
                    txtMaximumPackage.Text = dt.Rows[0]["MAX_PACKAGE"].ToString();
                    if (dt.Rows[0]["UOM_MAX_PACKAGE"].ToString() != null && dt.Rows[0]["UOM_MAX_PACKAGE"].ToString() != string.Empty)
                    {
                        ddlUOMMaxPackage.SelectedValue = dt.Rows[0]["UOM_MAX_PACKAGE"].ToString();
                    }
                }
                if (dt.Rows[0]["NO_OF_HEADS"] != null && dt.Rows[0]["NO_OF_HEADS"].ToString() != "")
                {
                    txtNoOfHeads.Text = dt.Rows[0]["NO_OF_HEADS"].ToString();
                    if (dt.Rows[0]["UOM_NO_OF_HEADS"].ToString() != null && dt.Rows[0]["UOM_NO_OF_HEADS"].ToString() != string.Empty)
                    {
                        ddlUOMNoOfHeads.SelectedValue = dt.Rows[0]["UOM_NO_OF_HEADS"].ToString();
                    }
                }
                if (dt.Rows[0]["NO_OF_PACKAGES"] != null && dt.Rows[0]["NO_OF_PACKAGES"].ToString() != "")
                {
                    txtNoOfPackage.Text = dt.Rows[0]["NO_OF_PACKAGES"].ToString();
                    if (dt.Rows[0]["UOM_NO_OF_PACKAGES"].ToString() != null && dt.Rows[0]["UOM_NO_OF_PACKAGES"].ToString() != string.Empty)
                    {
                        ddlUOMNoOfPackages.SelectedValue = dt.Rows[0]["UOM_NO_OF_PACKAGES"].ToString();
                    }
                }
                if (dt.Rows[0]["NO_OF_SPINDLES"] != null && dt.Rows[0]["NO_OF_SPINDLES"].ToString() != "")
                {
                    txtNoOfSpindles.Text = dt.Rows[0]["NO_OF_SPINDLES"].ToString();
                    if (dt.Rows[0]["UOM_NO_OF_SPINDLES"].ToString() != null && dt.Rows[0]["UOM_NO_OF_SPINDLES"].ToString() != string.Empty)
                    {
                        ddlUOMNoOfSpindles.SelectedValue = dt.Rows[0]["UOM_NO_OF_SPINDLES"].ToString();
                    }
                }
                if (dt.Rows[0]["MACHINE_POWER"] != null && dt.Rows[0]["MACHINE_POWER"].ToString() != "")
                {
                    txtPower.Text = dt.Rows[0]["MACHINE_POWER"].ToString();
                    if (dt.Rows[0]["UOM_MACHINE_POWER"].ToString() != null && dt.Rows[0]["UOM_MACHINE_POWER"].ToString() != string.Empty)
                    {
                        ddlUOMManpower.SelectedValue = dt.Rows[0]["UOM_MACHINE_POWER"].ToString();
                    }
                }
                if (dt.Rows[0]["SOFTWATER"] != null && dt.Rows[0]["SOFTWATER"].ToString() != "")
                {
                    txtSoftWater.Text = dt.Rows[0]["SOFTWATER"].ToString();
                    if (dt.Rows[0]["UOM_SOFTWATER"].ToString() != null && dt.Rows[0]["UOM_SOFTWATER"].ToString() != string.Empty)
                    {
                        ddlUOMSoftwater.SelectedValue = dt.Rows[0]["UOM_SOFTWATER"].ToString();
                    }
                }
                if (dt.Rows[0]["STEAM"] != null && dt.Rows[0]["STEAM"].ToString() != "")
                {
                    txtSteam.Text = dt.Rows[0]["STEAM"].ToString();
                    if (dt.Rows[0]["UOM_STEAM"].ToString() != null && dt.Rows[0]["UOM_STEAM"].ToString() != string.Empty)
                    {
                        ddlUOMPower.SelectedValue = dt.Rows[0]["UOM_STEAM"].ToString();
                    }
                }

                txtYOM.Text = dt.Rows[0]["YOM"].ToString();
                txtMachineMake.Text = dt.Rows[0]["MACHINE_MAKE"].ToString();
                txtSupplier.Text = dt.Rows[0]["SUPPLIER"].ToString();
                if (dt.Rows[0]["COAL"] != null && dt.Rows[0]["COAL"].ToString() != "")
                {
                    txtCoal.Text = dt.Rows[0]["COAL"].ToString();
                    if (dt.Rows[0]["COAL_UOM"].ToString() != null && dt.Rows[0]["COAL_UOM"].ToString() != string.Empty)
                    {
                        ddlUomCoal.SelectedValue = dt.Rows[0]["COAL_UOM"].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void ddlUOM1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtNos_TextChanged(object sender, EventArgs e)
    {

    }
    protected void cmbMachineGroup_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            string MachineGroup = cmbMachineGroup.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineDetailonGroup(MachineGroup);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblSegment.Text = dt.Rows[0]["MACHINE_SEGMENT"].ToString();
                lblType.Text = dt.Rows[0]["MACHINE_TYPE"].ToString();
                lblSection.Text = dt.Rows[0]["MACHINE_SEC"].ToString();
                dvMachineDetail.Visible = true;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
}
