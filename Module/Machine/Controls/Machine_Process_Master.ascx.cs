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

public partial class Module_Machine_Controls_Machine_Process_Master : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.TX_MAC_PROC_MST oTX_MAC_PROC_MST = new SaitexDM.Common.DataModel.TX_MAC_PROC_MST();
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    private static DataTable dtStandardDetail = null;
    private static DataTable dtChemicalDetail = null;
    private string UserCode;
    private static string compcode = string.Empty;
    private static string branchcode = string.Empty;
    private static int year = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DateTime startdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Enddate = CommonFuction.GetYearEndDate(startdate);

            if (!IsPostBack)
            {
                UserCode = oUserLoginDetail.UserCode;
                compcode = oUserLoginDetail.COMP_CODE;
                branchcode = oUserLoginDetail.CH_BRANCHCODE;
                year = oUserLoginDetail.DT_STARTDATE.Year;
                ClearPage();
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            CreateChemicalDetailTable();
            CreateStandardDetailTable();
            lblMode.Text = "Save";
            imgbtnUpdate.Visible = false;
            lblprocessCodeSave.Visible = true;
            BindMachineCode();
            BindProcess("MACHINE_PROCESS_TYPE");
            Bind_FabricType();
            Bind_StandardParameter();
            Bind_BASIS();
            Bind_CHEMICAL_BASIS();
            Bind_ProductType();
            BindProcessCodeByProductType();
            Bind_UOM();
            Bind_ChemicalUOM();
            BindDepartment();
            GetDyeProcess();
            ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    private void BindProcessCodeByProductType()
    {
        try
        {
            oTX_MAC_PROC_MST.PRODUCT_TYPE = ddProducttype.SelectedItem.ToString();
            oTX_MAC_PROC_MST.COMP_CODE = compcode;
            oTX_MAC_PROC_MST.BRANCH_CODE = branchcode;
            oTX_MAC_PROC_MST.YEAR = year;
            string MachineGroup = string.Empty;
            if (ddlMachineCode.SelectedText.Trim() != "Find Machine Group")
            {
                MachineGroup = ddlMachineCode.SelectedText.Trim();
            }
        }
        catch
        {
            throw;
        }
    }
    private void GetDyeProcess()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("DYE_PROCESS", oUserLoginDetail.COMP_CODE);
            ddlDyeProcessCode.DataSource = dt;
            ddlDyeProcessCode.DataValueField = "MST_CODE";
            ddlDyeProcessCode.DataTextField = "MST_DESC";
            ddlDyeProcessCode.DataBind();
            ddlDyeProcessCode.Items.Insert(0, new ListItem("---Select---", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void Bind_ProductType()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("PRODUCT_TYPE", oUserLoginDetail.COMP_CODE);
            ddProducttype.DataSource = dt;
            ddProducttype.DataValueField = "MST_CODE";
            ddProducttype.DataTextField = "MST_CODE";
            ddProducttype.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }

    }

    private void BindMachineCode()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineGroup();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlMachineCode.Items.Clear();
                ddlMachineCode.DataSource = dt;
                ddlMachineCode.DataTextField = "MACHINE_GROUP";
                ddlMachineCode.DataValueField = "MACHINE_TYPE";
                ddlMachineCode.DataBind();
                // ddlMachineCode.Items.Insert(0, new ListItem("----Select----", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindProcess(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, compcode);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlProcess.Items.Clear();
                ddlProcess.DataSource = dt;
                ddlProcess.DataTextField = "MST_CODE";
                ddlProcess.DataValueField = "MST_CODE";
                ddlProcess.DataBind();
                ///ddlBland2.Items.Insert(0, new ListItem("------Select------", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    private void Bind_FabricType()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FAB_TYPE", oUserLoginDetail.COMP_CODE);
            ddlFabType.DataSource = dt;
            ddlFabType.DataValueField = "MST_CODE";
            ddlFabType.DataTextField = "MST_DESC";
            ddlFabType.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_UOM()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            ddlStandardunit.DataSource = dt;
            ddlStandardunit.DataValueField = "MST_CODE";
            ddlStandardunit.DataTextField = "MST_DESC";
            ddlStandardunit.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_ChemicalUOM()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("UOM", oUserLoginDetail.COMP_CODE);
            ddlChemicalunit.DataSource = dt;
            ddlChemicalunit.DataValueField = "MST_CODE";
            ddlChemicalunit.DataTextField = "MST_DESC";
            ddlChemicalunit.DataBind();
            ddlChemicalunit.SelectedIndex = ddlChemicalunit.Items.IndexOf(ddlChemicalunit.Items.FindByText("KG"));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_StandardParameter()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_STANDARD_PARAMETER", oUserLoginDetail.COMP_CODE);
            ddlParameter.DataSource = dt;
            ddlParameter.DataValueField = "MST_CODE";
            ddlParameter.DataTextField = "MST_DESC";
            ddlParameter.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_BASIS()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("MACHINE_BASIS", oUserLoginDetail.COMP_CODE);
            ddlStandardBasis.DataSource = dt;
            ddlStandardBasis.DataValueField = "MST_CODE";
            ddlStandardBasis.DataTextField = "MST_DESC";
            ddlStandardBasis.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }

    private void Bind_CHEMICAL_BASIS()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("DYE_BASIS", oUserLoginDetail.COMP_CODE);
            ddlChemicalBasis.DataSource = dt;
            ddlChemicalBasis.DataValueField = "MST_CODE";
            ddlChemicalBasis.DataTextField = "MST_DESC";
            ddlChemicalBasis.DataBind();
            //ddlFabType.Items.Insert(0, new ListItem("-------Select--------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }


    private void BindDepartment()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlDepartment.Items.Clear();
                ddlDepartment.DataSource = dt;
                ddlDepartment.DataTextField = "DEPT_NAME";
                ddlDepartment.DataValueField = "DEPT_CODE";
                ddlDepartment.DataBind();
                //ddlChemicalLCode.Items.Insert(0, new ListItem("----Select----", "0"));
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
                string msg = string.Empty;
                if (ValidateFormForSaving(out msg))
                {
                    int iRecordFound = 0;

                    oTX_MAC_PROC_MST.PROS_CODE = Common.CommonFuction.funFixScript(txtProcessCode.Text.Trim());
                    oTX_MAC_PROC_MST.PROS_DESC = Common.CommonFuction.funFixScript(txtDescription.Text.Trim());
                    oTX_MAC_PROC_MST.MAC_CODE = Common.CommonFuction.funFixScript(ddlMachineCode.SelectedText.ToString());
                    oTX_MAC_PROC_MST.MAIN_PROCESS = Common.CommonFuction.funFixScript(ddlProcess.SelectedItem.ToString());
                    oTX_MAC_PROC_MST.FABR_TYPE = Common.CommonFuction.funFixScript(ddlFabType.SelectedItem.ToString());
                    oTX_MAC_PROC_MST.DEPARTMENT = ddlDepartment.SelectedValue.Trim();// Common.CommonFuction.funFixScript(txtDepartment.Text.Trim());
                    oTX_MAC_PROC_MST.SPEED = Convert.ToInt32(Common.CommonFuction.funFixScript(txtSpeed.Text.Trim()));
                    oTX_MAC_PROC_MST.TEMP = Convert.ToInt32(Common.CommonFuction.funFixScript(txtTempReq.Text.Trim()));
                    oTX_MAC_PROC_MST.REMARKS = Common.CommonFuction.funFixScript(txtSpecificInstrction.Text.Trim());
                    oTX_MAC_PROC_MST.SETTIME = Convert.ToInt32(Common.CommonFuction.funFixScript(txtSetTime.Text.Trim()));

                    double SHRINKAGE = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtMaxShrikage.Text.Trim()), out SHRINKAGE);
                    oTX_MAC_PROC_MST.SHRINKAGE = SHRINKAGE;

                    double LONGATION = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtMaxLongation.Text.Trim()), out LONGATION);
                    oTX_MAC_PROC_MST.LONGATION = LONGATION;

                    double TARR_QTY = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtMachineTruffQuantity.Text.Trim()), out TARR_QTY);
                    oTX_MAC_PROC_MST.TARR_QTY = TARR_QTY;

                    double EXPR = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtExpr.Text.Trim()), out EXPR);
                    oTX_MAC_PROC_MST.EXPR = EXPR;

                    if (ddlDyesRecieptFlag.SelectedItem.ToString() == "YES")
                    {
                        oTX_MAC_PROC_MST.CLRFLG = "1";
                    }
                    else
                    {
                        oTX_MAC_PROC_MST.CLRFLG = "0";
                    }
                    if (ddlQualityControlFlag.SelectedItem.ToString() == "YES")
                    {
                        oTX_MAC_PROC_MST.QCFLG = "1";
                    }
                    else
                    {
                        oTX_MAC_PROC_MST.QCFLG = "0";
                    }
                    oTX_MAC_PROC_MST.MAC_GRUP_CODE = Common.CommonFuction.funFixScript(txtMAchineName.Text.Trim());


                    oTX_MAC_PROC_MST.YEAR = year;
                    oTX_MAC_PROC_MST.TUSER = oUserLoginDetail.UserCode;
                    oTX_MAC_PROC_MST.BRANCH_CODE = branchcode;
                    oTX_MAC_PROC_MST.COMP_CODE = compcode;
                    oTX_MAC_PROC_MST.STATUS = true;
                    oTX_MAC_PROC_MST.PRODUCT_TYPE = ddProducttype.SelectedItem.ToString();
                    bool resutl = SaitexBL.Interface.Method.TX_MAC_PROC_MST.InsertMachineProcessMaster(oTX_MAC_PROC_MST, out  iRecordFound, dtStandardDetail, dtChemicalDetail);
                    if (resutl)
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Master Saved Successfully');", true);
                        ClearPage();
                        InitialisePage();

                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Already Exists');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Master Saving Failed!!');", true);

                    }
                }
                else
                {
                    CommonFuction.ShowMessage(msg);
                }

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in saving data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                string msg = string.Empty;
                if (ValidateFormForSaving(out msg))
                {
                    int iRecordFound = 0;

                    oTX_MAC_PROC_MST.PROS_CODE = Common.CommonFuction.funFixScript(txtProcessCode.Text.Trim());
                    oTX_MAC_PROC_MST.PROS_DESC = Common.CommonFuction.funFixScript(txtDescription.Text.Trim());
                    oTX_MAC_PROC_MST.MAC_CODE = Common.CommonFuction.funFixScript(ddlMachineCode.SelectedText.ToString());
                    oTX_MAC_PROC_MST.MAIN_PROCESS = Common.CommonFuction.funFixScript(ddlProcess.SelectedItem.ToString());
                    oTX_MAC_PROC_MST.FABR_TYPE = Common.CommonFuction.funFixScript(ddlFabType.SelectedItem.ToString());
                    oTX_MAC_PROC_MST.DEPARTMENT = ddlDepartment.SelectedValue.Trim();// Common.CommonFuction.funFixScript(txtDepartment.Text.Trim());
                    oTX_MAC_PROC_MST.SPEED = Convert.ToInt32(Common.CommonFuction.funFixScript(txtSpeed.Text.Trim()));
                    oTX_MAC_PROC_MST.TEMP = Convert.ToInt32(Common.CommonFuction.funFixScript(txtTempReq.Text.Trim()));
                    oTX_MAC_PROC_MST.REMARKS = Common.CommonFuction.funFixScript(txtSpecificInstrction.Text.Trim());
                    oTX_MAC_PROC_MST.SETTIME = Convert.ToInt32(Common.CommonFuction.funFixScript(txtSetTime.Text.Trim()));

                    double SHRINKAGE = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtMaxShrikage.Text.Trim()), out SHRINKAGE);
                    oTX_MAC_PROC_MST.SHRINKAGE = SHRINKAGE;


                    double LONGATION = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtMaxLongation.Text.Trim()), out LONGATION);
                    oTX_MAC_PROC_MST.LONGATION = LONGATION;


                    double TARR_QTY = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtMachineTruffQuantity.Text.Trim()), out TARR_QTY);
                    oTX_MAC_PROC_MST.TARR_QTY = TARR_QTY;

                    double EXPR = 0;
                    double.TryParse(Common.CommonFuction.funFixScript(txtExpr.Text.Trim()), out EXPR);
                    oTX_MAC_PROC_MST.EXPR = EXPR;


                    if (ddlDyesRecieptFlag.SelectedItem.ToString() == "YES")
                    {
                        oTX_MAC_PROC_MST.CLRFLG = "1";
                    }
                    else
                    {
                        oTX_MAC_PROC_MST.CLRFLG = "0";
                    }
                    if (ddlQualityControlFlag.SelectedItem.ToString() == "YES")
                    {
                        oTX_MAC_PROC_MST.QCFLG = "1";
                    }
                    else
                    {
                        oTX_MAC_PROC_MST.QCFLG = "0";
                    }
                    oTX_MAC_PROC_MST.MAC_GRUP_CODE = Common.CommonFuction.funFixScript(txtMAchineName.Text.Trim());


                    oTX_MAC_PROC_MST.YEAR = year;
                    oTX_MAC_PROC_MST.TUSER = oUserLoginDetail.UserCode;
                    oTX_MAC_PROC_MST.BRANCH_CODE = branchcode;
                    oTX_MAC_PROC_MST.COMP_CODE = compcode;
                    oTX_MAC_PROC_MST.STATUS = true;
                    oTX_MAC_PROC_MST.PRODUCT_TYPE = ddProducttype.SelectedItem.ToString();



                    bool resutl = SaitexBL.Interface.Method.TX_MAC_PROC_MST.UpdateProcessMaster(oTX_MAC_PROC_MST, out  iRecordFound, dtStandardDetail, dtChemicalDetail);
                    if (resutl)
                    {

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Master Updated Successfully');", true);
                        ClearPage();
                        InitialisePage();

                    }
                    else if (iRecordFound > 0)
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Already Exists');", true);

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Process Master Updation Failed!!');", true);


                    }
                }
                else
                {
                    CommonFuction.ShowMessage(msg);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in updating data.\r\nSee error log for detail."));
        }
    }

    private bool ValidateFormForSaving(out string msg)
    {

        try
        {
            bool ReturnResult = false;
            msg = string.Empty;
            if (ddProducttype.SelectedItem.Text == "YARN DYEING")
            {
                int count = 0;
                int Totalcount = 0;


                Totalcount += 1;
                if (dtChemicalDetail != null && dtChemicalDetail.Rows.Count > 0)
                {
                    count += 1;
                }
                else
                {
                    msg += @"#. Please provide chemical detail.\r\n";
                }
                Totalcount += 1;
                if (dtStandardDetail != null && dtStandardDetail.Rows.Count > 0)
                {
                    count += 1;
                }
                else
                {
                    msg += @"#. Please provide Standard parameter detail.\r\n";
                }

                if (count == Totalcount)
                    ReturnResult = true;
                return ReturnResult;
            }
            else
            {
                ReturnResult = true;
                return ReturnResult;
            }
            // return ReturnResult;
        }
        catch
        {
            throw;
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            imgbtnSave.Visible = false;
            imgbtnUpdate.Visible = true;
            lblFindprocess.Visible = true;
            lblprocessCodeSave.Visible = false;
            txtProcessCode.Visible = false;
            lblMode.Text = "Update";
            ddlProcessCode.Visible = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in update mode.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        
        try
        {
            string URL = "../Reports/Machine_Proc_Master_Report.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
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
            ClearPage();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in refreshing page.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void txtItemCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtICode.Text = txtItemCode.SelectedText.ToString();
            txtItemDesc.Text = txtItemCode.SelectedValue.ToString();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in chemical selection.\r\nSee error log for detail."));
        }
    }

    protected void ddProducttype_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            BindProcessCodeByProductType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Product type selection.\r\nSee error log for detail."));
        }
    }

    #region For Manange Standard Datatable

    protected void btnStandardSave_Click(object sender, EventArgs e)
    {
        try
        {
            if (dtStandardDetail.Rows.Count < 15)
            {
                if (txtStandardQuantity.Text != "")
                {
                    int UniqueId = 0;
                    if (ViewState["UniqueId"] != null)
                    {
                        UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                    }
                    bool bb = SearchParameterInStandardGrid(ddlParameter.SelectedItem.ToString().Trim(), UniqueId);
                    if (!bb)
                    {


                        if (UniqueId > 0)
                        {
                            DataView dv = new DataView(dtStandardDetail);
                            dv.RowFilter = "UniqueId=" + UniqueId;
                            if (dv.Count > 0)
                            {
                                dv[0]["Parameter"] = ddlParameter.SelectedItem.ToString().Trim();
                                dv[0]["Quantity"] = double.Parse(txtStandardQuantity.Text.Trim());
                                dv[0]["Unit"] = ddlStandardunit.SelectedItem.ToString();
                                dv[0]["Basis"] = ddlStandardBasis.SelectedItem.ToString();
                                dtStandardDetail.AcceptChanges();
                            }
                        }
                        else
                        {

                            DataRow dr = dtStandardDetail.NewRow();
                            dr["UniqueId"] = dtStandardDetail.Rows.Count + 1;
                            dr["Parameter"] = ddlParameter.SelectedItem.ToString().Trim();
                            dr["Quantity"] = double.Parse(txtStandardQuantity.Text.Trim());
                            dr["Unit"] = ddlStandardunit.SelectedItem.ToString();
                            dr["Basis"] = ddlStandardBasis.SelectedItem.ToString();
                            dtStandardDetail.Rows.Add(dr);
                        }
                        RefreshStandardDetailRow();
                    }


                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Parameter Already Added.Please Select Another');", true);
                    }

                    BindStandardDetailGrid();
                }

                else
                {
                    CommonFuction.ShowMessage("Please Enter Standard Quantity");
                }

            }

            else
            {
                CommonFuction.ShowMessage("You have reached the limit of Standard. Only 15 Standard allowed in one Machine Process Master.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding standard parameter.\r\nSee error log for detail."));
        }
    }

    protected void btnStandardCancel_Click(object sender, EventArgs e)
    {

    }

    protected void gvStandard_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "StandardEdit")
            {
                FillStandardDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "StandardDelete")
            {

                DeleteStandardDetailRow(UniqueId);
                BindStandardDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in editing standard parameter.\r\nSee error log for detail."));
        }
    }

    private void CreateStandardDetailTable()
    {
        try
        {
            dtStandardDetail = new DataTable();
            dtStandardDetail.Columns.Add("UniqueId", typeof(int));
            dtStandardDetail.Columns.Add("Parameter", typeof(string));
            dtStandardDetail.Columns.Add("Quantity", typeof(double));
            dtStandardDetail.Columns.Add("Unit", typeof(string));
            dtStandardDetail.Columns.Add("Basis", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    private void FillStandardDetailByGrid(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(dtStandardDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                Bind_StandardParameter();
                Bind_UOM();
                Bind_BASIS();
                ddlParameter.SelectedValue = dv[0]["Parameter"].ToString();
                txtStandardQuantity.Text = dv[0]["Quantity"].ToString();
                ddlStandardunit.SelectedValue = dv[0]["Unit"].ToString();
                ddlStandardBasis.SelectedValue = dv[0]["Basis"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteStandardDetailRow(int UniqueId)
    {
        try
        {
            if (gvStandard.Rows.Count == 1)
            {
                dtStandardDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtStandardDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtStandardDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtStandardDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchParameterInStandardGrid(string Parameter, int UniqueId)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in gvStandard.Rows)
            {
                Label lblParameter = (Label)grdRow.FindControl("txtParameter");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                if (lblParameter.Text.Trim() == Parameter && UniqueId != iUniqueId)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }

    private void BindStandardDetailGrid()
    {
        try
        {
            gvStandard.DataSource = dtStandardDetail;
            gvStandard.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void RefreshStandardDetailRow()
    {
        try
        {
            ddlParameter.SelectedIndex = -1;
            txtStandardQuantity.Text = "";
            ddlStandardunit.SelectedIndex = -1;
            ddlStandardBasis.SelectedIndex = -1;
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void MapStandardDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtStandardDetail == null || dtStandardDetail.Rows.Count == 0)
                CreateStandardDetailTable();
            dtStandardDetail.Rows.Clear();

            int currentyear = year;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtStandardDetail.NewRow();
                    dr["UniqueId"] = dtStandardDetail.Rows.Count + 1;
                    dr["Parameter"] = drTemp["PARA_HEAD"];
                    dr["Quantity"] = drTemp["PARA_QTY"];
                    dr["Unit"] = drTemp["UOM"];
                    dr["Basis"] = drTemp["PARA_BASIS"];
                    dtStandardDetail.Rows.Add(dr);
                }
                dtTemp = null;

            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    #region For Manage Chemical Receipe Datatable

    protected void BtnChemicalSave_Click1(object sender, EventArgs e)
    {
        try
        {
            if (dtChemicalDetail.Rows.Count < 50)
            {
                //if (txtStandardQuantity.Text != "")
                //{
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                {
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                }
                bool bb = SearchParameterInChemicalGrid(txtItemCode.SelectedText.ToString().Trim(), UniqueId, ddlDyeProcessCode.SelectedValue, ddlChemicalBasis.SelectedValue, ddlChemicalunit.SelectedValue);
                if (!bb)
                {
                    if (UniqueId > 0)
                    {
                        DataView dv = new DataView(dtChemicalDetail);
                        dv.RowFilter = "UniqueId=" + UniqueId;
                        if (dv.Count > 0)
                        {
                            dv[0]["DyeProcess"] = ddlDyeProcessCode.SelectedItem.ToString();
                            //dv[0]["LCode"] = txtItemCode.SelectedValue.ToString().Trim();
                            dv[0]["LCode"] = txtICode.Text.Trim();
                            dv[0]["ItemDesc"] = txtItemDesc.Text.Trim();
                            dv[0]["Truff"] = double.Parse(txtChemicalTruff.Text.Trim());
                            dv[0]["Expr"] = double.Parse(txtChemicalExpr.Text.Trim());
                            dv[0]["Dansity"] = double.Parse(txtChemicalExpr.Text.Trim());
                            dv[0]["Quantity"] = double.Parse(txtChemicalQuantity.Text.Trim());
                            dv[0]["Unit"] = ddlChemicalunit.SelectedItem.ToString();
                            dv[0]["PARA_BASIS"] = ddlChemicalBasis.SelectedItem.ToString();
                            dv[0]["DYE_REMARKS"] = txtRemarks.Text.Trim().ToString();
                            dv[0]["TEMP"] = txtTemp.Text.Trim().ToString();
                            dv[0]["HOLD_TIME"] = txtHoldTime.Text.Trim().ToString();
                            dtChemicalDetail.AcceptChanges();
                        }
                    }
                    else
                    {

                        DataRow dr = dtChemicalDetail.NewRow();
                        dr["UniqueId"] = dtChemicalDetail.Rows.Count + 1;
                        dr["DyeProcess"] = ddlDyeProcessCode.SelectedItem.ToString();
                        // dr["LCode"] = txtItemCode.SelectedText.ToString();
                        dr["LCode"] = txtICode.Text.Trim();
                        dr["ItemDesc"] = txtItemDesc.Text.Trim();
                        dr["Truff"] = double.Parse(txtChemicalTruff.Text.Trim());
                        dr["Expr"] = double.Parse(txtChemicalExpr.Text.Trim());
                        dr["Dansity"] = double.Parse(txtChemicalExpr.Text.Trim());
                        dr["Quantity"] = double.Parse(txtChemicalQuantity.Text.Trim());
                        dr["Unit"] = ddlChemicalunit.SelectedItem.ToString();
                        dr["PARA_BASIS"] = ddlChemicalBasis.SelectedItem.ToString();
                        dr["DYE_REMARKS"] = txtRemarks.Text.Trim().ToString();
                        dr["TEMP"] = txtTemp.Text.Trim().ToString();
                        dr["HOLD_TIME"] = txtHoldTime.Text.Trim().ToString();
                        dtChemicalDetail.Rows.Add(dr);
                    }
                    RefreshChemicalDetailRow();
                }


                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "sf", "window.alert('Selected Lcode Already Added.Please Select Another');", true);
                }

                BindChemicalDetailGrid();
            }



            else
            {
                CommonFuction.ShowMessage("You have reached the limit of Chemical Reciepe. Only 15 Chemical Reciepe allowed in one Machine Process Master.");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in adding chemical.\r\nSee error log for detail."));
        }
    }

    protected void BtnChemicalCancel_Click(object sender, EventArgs e)
    {

    }

    protected void gvChemicalReciepe_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "ChemicalEdit")
            {
                FillChemicalDetailByGrid(UniqueId);
            }
            else if (e.CommandName == "ChemicalDelete")
            {

                DeleteChemicalDetailRow(UniqueId);
                BindChemicalDetailGrid();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in editing chemical.\r\nSee error log for detail."));
        }
    }

    private void CreateChemicalDetailTable()
    {
        try
        {
            dtChemicalDetail = new DataTable();
            dtChemicalDetail.Columns.Add("UniqueId", typeof(int));
            dtChemicalDetail.Columns.Add("DyeProcess", typeof(string));
            dtChemicalDetail.Columns.Add("LCode", typeof(string));
            dtChemicalDetail.Columns.Add("ItemDesc", typeof(string));
            dtChemicalDetail.Columns.Add("Truff", typeof(double));
            dtChemicalDetail.Columns.Add("Expr", typeof(double));
            dtChemicalDetail.Columns.Add("Dansity", typeof(double));
            dtChemicalDetail.Columns.Add("Quantity", typeof(double));
            dtChemicalDetail.Columns.Add("Unit", typeof(string));
            dtChemicalDetail.Columns.Add("PARA_BASIS", typeof(string));
            dtChemicalDetail.Columns.Add("DYE_REMARKS", typeof(string));
            dtChemicalDetail.Columns.Add("TEMP", typeof(string));
            dtChemicalDetail.Columns.Add("HOLD_TIME", typeof(string));

        }
        catch
        {
            throw;
        }
    }

    private void FillChemicalDetailByGrid(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(dtChemicalDetail);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {


                Bind_CHEMICAL_BASIS();
                Bind_ChemicalUOM();

                GetDyeProcess();
                ddlDyeProcessCode.SelectedValue = dv[0]["DyeProcess"].ToString();
                txtItemCode.SelectedText = dv[0]["LCode"].ToString();
                txtICode.Text = dv[0]["LCode"].ToString();
                txtItemDesc.Text = dv[0]["ItemDesc"].ToString();
                txtChemicalTruff.Text = dv[0]["Truff"].ToString();
                txtChemicalExpr.Text = dv[0]["Expr"].ToString();
                txtTemp.Text = dv[0]["TEMP"].ToString();
                txtHoldTime.Text = dv[0]["HOLD_TIME"].ToString();
                txtChemicalDansity.Text = dv[0]["Dansity"].ToString();
                txtChemicalQuantity.Text = dv[0]["Quantity"].ToString();
                ddlChemicalunit.SelectedValue = dv[0]["Unit"].ToString();
                ddlChemicalBasis.SelectedValue = dv[0]["PARA_BASIS"].ToString();
                txtRemarks.Text = dv[0]["DYE_REMARKS"].ToString();
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteChemicalDetailRow(int UniqueId)
    {
        try
        {
            if (gvChemicalReciepe.Rows.Count == 1)
            {
                dtChemicalDetail.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtChemicalDetail.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        dtChemicalDetail.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtChemicalDetail.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private bool SearchParameterInChemicalGrid(string LCode, int UniqueId, string DYE_PROCESS, string Basic, string UOM)
    {
        bool Result = false;
        try
        {
            foreach (GridViewRow grdRow in gvChemicalReciepe.Rows)
            {
                Label lblCode = (Label)grdRow.FindControl("txtLCode");
                LinkButton lnkDelete = (LinkButton)grdRow.FindControl("lnkDelete0");
                int iUniqueId = int.Parse(lnkDelete.CommandArgument.Trim());
                Label lblDProcessCode = (Label)grdRow.FindControl("lblDProcessCode");
                Label txtBasis = (Label)grdRow.FindControl("txtBasis");
                Label txtUnit = (Label)grdRow.FindControl("txtUnit");

                if (lblCode.Text.Trim() == LCode && iUniqueId != UniqueId && lblDProcessCode.Text.Trim() == DYE_PROCESS && txtBasis.Text.Trim() == Basic && txtUnit.Text.Trim() == UOM)
                    Result = true;
            }
            return Result;
        }
        catch
        {
            throw;
        }
    }
    private void BindChemicalDetailGrid()
    {
        try
        {
            gvChemicalReciepe.DataSource = dtChemicalDetail;
            gvChemicalReciepe.DataBind();
        }
        catch
        {
            throw;
        }
    }

    private void RefreshChemicalDetailRow()
    {
        try
        {
            ddlDyeProcessCode.SelectedIndex = -1;
            txtItemCode.SelectedIndex = -1;
            txtChemicalTruff.Text = "0";
            txtChemicalExpr.Text = "0";
            txtChemicalDansity.Text = "";
            txtTemp.Text = "";
            txtHoldTime.Text = "";
            txtChemicalQuantity.Text = "";
            ddlChemicalunit.SelectedIndex = -1;
            ddlChemicalBasis.SelectedIndex = -1;
            txtICode.Text = "";
            txtItemDesc.Text = "";
            txtRemarks.Text = "";
            ViewState["UniqueId"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void MapChemicalDataTable(DataTable dtTemp)
    {
        try
        {
            if (dtChemicalDetail == null || dtChemicalDetail.Rows.Count == 0)
                CreateChemicalDetailTable();
            //dtChemicalDetail.Rows.Clear();

            int currentyear = year;
            if (dtTemp != null && dtTemp.Rows.Count > 0)
            {
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    DataRow dr = dtChemicalDetail.NewRow();
                    dr["UniqueId"] = dtChemicalDetail.Rows.Count + 1;
                    dr["DyeProcess"] = drTemp["DYE_PROCESS"];
                    dr["LCode"] = drTemp["ITEM_CODE"];
                    dr["ItemDesc"] = drTemp["ITEM_DESC"];
                    dr["Truff"] = drTemp["TARR_QTY"];
                    dr["Expr"] = drTemp["Expr"];
                    dr["Dansity"] = drTemp["DANS"];
                    dr["Quantity"] = drTemp["QTY"];
                    dr["Unit"] = drTemp["UOM"];
                    dr["PARA_BASIS"] = drTemp["PARA_BASIS"];
                    dr["DYE_REMARKS"] = drTemp["DYE_REMARKS"];
                    dr["TEMP"] = drTemp["TEMP"];
                    dr["HOLD_TIME"] = drTemp["HOLD_TIME"];
                    dtChemicalDetail.Rows.Add(dr);

                }
                dtTemp = null;

            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void ddlProcessCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            string CommandText = "select PROS_CODE,PROS_DESC, PRODUCT_TYPE from TX_MAC_PROC_MST";
            string WhereClause = "  where PROS_CODE like :SearchQuery or PROS_DESC like :SearchQuery or PRODUCT_TYPE like :SearchQuery";
            string SortExpression = " order by PROS_CODE asc";
            string SearchQuery = e.Text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

            ddlProcessCode.Items.Clear();

            ddlProcessCode.DataSource = data;
            ddlProcessCode.DataBind();

            e.ItemsLoadedCount = data.Rows.Count;
            e.ItemsCount = data.Rows.Count;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Process code loading.\r\nSee error log for detail."));
        }
    }

    protected void ddlProcessCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            oTX_MAC_PROC_MST.BRANCH_CODE = branchcode;
            oTX_MAC_PROC_MST.COMP_CODE = compcode;
            oTX_MAC_PROC_MST.YEAR = year;
            oTX_MAC_PROC_MST.PROS_CODE = ddlProcessCode.SelectedText.ToString();
            oTX_MAC_PROC_MST.PRODUCT_TYPE = ddlProcessCode.SelectedValue.ToString();
            DataTable dtProcessMaster = SaitexBL.Interface.Method.TX_MAC_PROC_MST.GetProcessMaster(oTX_MAC_PROC_MST);
            if (dtProcessMaster != null && dtProcessMaster.Rows.Count > 0)
            {
                Bind_ProductType();
                txtProcessCode.Text = dtProcessMaster.Rows[0]["PROS_CODE"].ToString();
                ddProducttype.SelectedValue = dtProcessMaster.Rows[0]["PRODUCT_TYPE"].ToString();
                txtDescription.Text = dtProcessMaster.Rows[0]["PROS_DESC"].ToString();
                BindMachineCode();

                foreach (Obout.ComboBox.ComboBoxItem item in ddlMachineCode.Items)
                {
                    if (item.Text == dtProcessMaster.Rows[0]["MAC_CODE"].ToString())
                    {
                        ddlMachineCode.SelectedIndex = ddlMachineCode.Items.IndexOf(item);
                        break;
                    }
                }

                txtMAchineName.Text = dtProcessMaster.Rows[0]["MAC_GRUP_CODE"].ToString();
                ddlProcess.SelectedValue = dtProcessMaster.Rows[0]["MAIN_PROCESS"].ToString();
                Bind_FabricType();
                ddlFabType.SelectedValue = dtProcessMaster.Rows[0]["FABR_TYPE"].ToString();
                ddlDepartment.SelectedIndex = ddlDepartment.Items.IndexOf(ddlDepartment.Items.FindByValue(dtProcessMaster.Rows[0]["DEPARTMENT"].ToString()));

                ddlDyesRecieptFlag.SelectedValue = dtProcessMaster.Rows[0]["CLRFLG"].ToString();
                ddlQualityControlFlag.SelectedValue = dtProcessMaster.Rows[0]["CLRFLG"].ToString();
                ddlStatus.SelectedValue = dtProcessMaster.Rows[0]["STATUS"].ToString();
                txtMachineTruffQuantity.Text = dtProcessMaster.Rows[0]["TARR_QTY"].ToString();
                txtExpr.Text = dtProcessMaster.Rows[0]["EXPR"].ToString();
                txtSpeed.Text = dtProcessMaster.Rows[0]["SPEED"].ToString();
                txtTempReq.Text = dtProcessMaster.Rows[0]["TEMP"].ToString();
                txtSetTime.Text = dtProcessMaster.Rows[0]["SETTIME"].ToString();
                txtMaxShrikage.Text = dtProcessMaster.Rows[0]["SHRINKAGE"].ToString();
                txtMaxLongation.Text = dtProcessMaster.Rows[0]["LONGATION"].ToString();
                txtSpecificInstrction.Text = dtProcessMaster.Rows[0]["REMARKS"].ToString();
                DataTable dtStandard = SaitexBL.Interface.Method.TX_MAC_PROC_MST.GetStandardTransactionByProcessCode(oTX_MAC_PROC_MST);
                if (dtStandard != null && dtStandard.Rows.Count > 0)
                {
                    MapStandardDataTable(dtStandard);
                    BindStandardDetailGrid();

                    DataTable dtChemical = SaitexBL.Interface.Method.TX_MAC_PROC_MST.GetChemicalTransactionByProcessCode(oTX_MAC_PROC_MST);
                    if (dtChemical != null && dtChemical.Rows.Count > 0)
                    {
                        MapChemicalDataTable(dtChemical);
                        BindChemicalDetailGrid();


                    }

                }

            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Process code selection.\r\nSee error log for detail."));
        }

    }

    protected void ddlMachineCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            txtMAchineName.Text = ddlMachineCode.SelectedValue.ToString();
            BindProcessCodeByProductType();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Machine selection.\r\nSee error log for detail."));
        }
    }

    protected void ddlMachineCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindMachineCode();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Machine loading.\r\nSee error log for detail."));
        }
    }

    private void ClearPage()
    {
        try
        {
            ddlChemicalBasis.SelectedIndex = -1;
            ddlDyeProcessCode.SelectedIndex = -1;
            txtItemCode.SelectedIndex = -1;
            ddlChemicalunit.SelectedIndex = -1;
            ddlDepartment.SelectedIndex = -1;
            ddlDyesRecieptFlag.SelectedIndex = -1;
            ddlFabType.SelectedIndex = -1;
            ddlMachineCode.SelectedIndex = -1;
            ddlParameter.SelectedIndex = -1;
            ddlProcess.SelectedIndex = -1;
            ddlProcessCode.SelectedIndex = -1;
            ddlQualityControlFlag.SelectedIndex = -1;
            ddlStandardBasis.SelectedIndex = -1;

            ddlStandardunit.SelectedIndex = -1;
            ddlStatus.SelectedIndex = -1;
            ddProducttype.SelectedIndex = -1;

            txtChemicalDansity.Text = string.Empty;
            txtChemicalExpr.Text = string.Empty;
            txtTemp.Text = string.Empty;
            txtHoldTime.Text = string.Empty;
            txtChemicalQuantity.Text = string.Empty;
            txtChemicalTruff.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtExpr.Text = string.Empty;
            txtMAchineName.Text = string.Empty;
            txtMachineTruffQuantity.Text = string.Empty;
            txtMaxLongation.Text = string.Empty;
            txtMaxShrikage.Text = string.Empty;

            txtProcessCode.Text = string.Empty;
            txtSetTime.Text = string.Empty;
            txtSpecificInstrction.Text = string.Empty;
            txtSpeed.Text = string.Empty;
            txtStandardQuantity.Text = string.Empty;
            txtTempReq.Text = string.Empty;

            if (dtStandardDetail != null)
                dtStandardDetail.Rows.Clear();

            if (dtChemicalDetail != null)
                dtChemicalDetail.Rows.Clear();

            gvChemicalReciepe.DataSource = null;
            gvChemicalReciepe.DataBind();
            gvStandard.DataSource = null;
            gvStandard.DataBind();

            imgbtnSave.Visible = true;
            imgbtnUpdate.Visible = false;
            lblFindprocess.Visible = false;
            lblprocessCodeSave.Visible = true;
            txtProcessCode.Visible = true;
            lblMode.Text = "Save";
            ddlProcessCode.Visible = false;

        }
        catch
        {
            throw;
        }
    }
    protected void txtItemCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLItemCode(e.Text.ToUpper(), e.ItemsOffset);
            txtItemCode.Items.Clear();
            txtItemCode.DataSource = data;
            txtItemCode.DataTextField = "ITEM_CODE";
            txtItemCode.DataValueField = "ITEM_DESC";
            txtItemCode.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
    private DataTable GetLItemCode(string text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT * FROM (SELECT * FROM (SELECT i.ITEM_CODE,i.ITEM_TYPE, i.ITEM_DESC, i.ITEM_MAKE,i.OP_BAL_STOCK, i.OP_RATE, i.MIN_STOCK_LVL, i.REODR_QTY,i.REODR_LVL, i.MIN_PROCURE_DAYS, i.EXPIRY_DAYS, i.QC_REQUIRED, i.ITEM_STATUS, i.ITEM_REMARKS, i.UOM,i.ASOC_ITEM_CODE, i.DEPT_CODE, i.BRANCH_CODE, i.RACK_CODE, i.CAT_CODE,d.DEPT_NAME, b.BRANCH_NAME FROM TX_ITEM_MST i, CM_DEPT_MST d, CM_BRANCH_MST b WHERE i.BRANCH_CODE = b.BRANCH_CODE AND i.DEPT_CODE = d.DEPT_CODE AND I.CAT_CODE IN ('DYE', 'CHEMICALS', 'CHEMICAL') ORDER BY ITEM_CODE ) WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery)";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += "  AND ITEM_CODE not in(SELECT * FROM   (SELECT *  FROM (SELECT   i.ITEM_CODE,i.ITEM_TYPE, i.ITEM_DESC, i.ITEM_MAKE,i.OP_BAL_STOCK, i.OP_RATE, i.MIN_STOCK_LVL, i.REODR_QTY,i.REODR_LVL, i.MIN_PROCURE_DAYS, i.EXPIRY_DAYS, i.QC_REQUIRED, i.ITEM_STATUS, i.ITEM_REMARKS, i.UOM,i.ASOC_ITEM_CODE, i.DEPT_CODE, i.BRANCH_CODE, i.RACK_CODE, i.CAT_CODE,d.DEPT_NAME, b.BRANCH_NAME FROM TX_ITEM_MST i, CM_DEPT_MST d, CM_BRANCH_MST b WHERE i.BRANCH_CODE = b.BRANCH_CODE AND i.DEPT_CODE = d.DEPT_CODE AND I.CAT_CODE IN ('DYE', 'CHEMICALS', 'CHEMICAL') ORDER BY ITEM_CODE ) WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery) WHERE ROWNUM <= '" + startOffset + "')";
            }
            string SortExpression = " ORDER BY ITEM_CODE";
            string SearchQuery = "%" + text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOVMaterial(CommandText, whereClause, SortExpression, "", SearchQuery, "", "");
            return data;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected int GetItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT *  FROM (SELECT   i.ITEM_CODE,i.ITEM_TYPE, i.ITEM_DESC, i.ITEM_MAKE,i.OP_BAL_STOCK, i.OP_RATE, i.MIN_STOCK_LVL, i.REODR_QTY,i.REODR_LVL, i.MIN_PROCURE_DAYS, i.EXPIRY_DAYS, i.QC_REQUIRED, i.ITEM_STATUS, i.ITEM_REMARKS, i.UOM,i.ASOC_ITEM_CODE, i.DEPT_CODE, i.BRANCH_CODE, i.RACK_CODE, i.CAT_CODE,d.DEPT_NAME, b.BRANCH_NAME FROM TX_ITEM_MST i, CM_DEPT_MST d, CM_BRANCH_MST b WHERE i.BRANCH_CODE = b.BRANCH_CODE AND i.DEPT_CODE = d.DEPT_CODE AND I.CAT_CODE IN ('DYE', 'CHEMICALS', 'CHEMICAL') ORDER BY ITEM_CODE ) WHERE ITEM_CODE LIKE :SearchQuery OR ITEM_DESC LIKE :SearchQuery ORDER BY ITEM_CODE)";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ITEM_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
            return data.Rows.Count;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}

