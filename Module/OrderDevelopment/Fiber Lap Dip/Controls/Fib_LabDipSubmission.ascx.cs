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
using errorLog;
using Common;
using DBLibrary;
using System.IO;
using System.Data.Linq;
using Obout.ComboBox;

public partial class Module_OrderDevelopment_Fiber_Lap_Dip_Controls_Fib_LabDipSubmission : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.FIBER_OD_LAB_DIP_ENTRY oFIBER_OD_LAB_DIP_ENTRY;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY oFIBER_OD_SHADE_FAMILY;
    private static string PRODUCT_TYPE = "FIBER_DYEING";
    private string strContext = string.Empty;
    private DataTable dtLRGenerate = null;
    private DataTable dtDye = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
            //if (Session["Total1"] != null)
            //    txtDepth.Text = Session["Total1"].ToString();

            if (Convert.ToInt16(Session["saveStatus"]) == 1)
            {
                if (Request.QueryString["cId"].ToString().Trim() == "S")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "U")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated successfully');", true);
                }
                if (Request.QueryString["cId"].ToString().Trim() == "D")
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Deleted successfully');", true);
                }
                Session["saveStatus"] = 0;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Page.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Save Mode";
            tdSave.Visible = true;
            tdDelete.Visible = false;
            tdUpdate.Visible = false;
            tdFind.Visible = false;
            trQuality.Visible = false;
            trQuality1.Visible = false;
            txtSubmissionDate.Text = System.DateTime.Now.ToShortDateString();

            if (ViewState["dtLRGenerate"] != null)
                dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

            if (dtLRGenerate == null)
                CreateLRGenerateTable();
            dtLRGenerate.Rows.Clear();

            if (Session["dtDye"] != null)
                Session["dtDye"] = null;
            if (Session["Total1"] != null)
                Session["Total1"] = null;
            ddlOption.Enabled = true;
            ddlShadeFamily.Enabled = true;
            ddlShade.Enabled = true;
            ddlLRNo.Enabled = true;
            // ddlDepth.Enabled = true;

            //BindOrderNo();
            BindOption();
            BindShade();
            // BindDepth();
        }
        catch
        {
            throw;
        }
    }

    private void CreateLRGenerateTable()
    {
        try
        {
            dtLRGenerate = new DataTable();
            dtLRGenerate.Columns.Add("UNIQUE_ID", typeof(int));
            dtLRGenerate.Columns.Add("LR_OPTION", typeof(string));
            dtLRGenerate.Columns.Add("ORDER_NO", typeof(string));
            dtLRGenerate.Columns.Add("ORDER_DATE", typeof(string));
            dtLRGenerate.Columns.Add("BRANCH_CODE", typeof(string));
            dtLRGenerate.Columns.Add("PRTY_CODE", typeof(string));
            dtLRGenerate.Columns.Add("LAB_DIP_NO", typeof(string));
            dtLRGenerate.Columns.Add("ARTICAL_NO", typeof(string));
            dtLRGenerate.Columns.Add("ARTICAL_DESC", typeof(string));
            dtLRGenerate.Columns.Add("GREY_LOT_NO", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_FAMILY_CODE_OLD", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_FAMILY_NAME", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_CODE_OLD", typeof(string));
            dtLRGenerate.Columns.Add("SHADE_NAME_OLD", typeof(string));
            dtLRGenerate.Columns.Add("COUNT", typeof(string));
            dtLRGenerate.Columns.Add("PLY", typeof(double));
            dtLRGenerate.Columns.Add("QUALITY", typeof(string));
            dtLRGenerate.Columns.Add("SUBMISSION_DATE", typeof(string));
            dtLRGenerate.Columns.Add("TOTAL_RECIPE_COSE", typeof(double));
            dtLRGenerate.Columns.Add("DEPTH", typeof(string));
            dtLRGenerate.Columns.Add("DEPTH_NAME", typeof(string));
            dtLRGenerate.Columns.Add("REMARKS", typeof(string));
            dtLRGenerate.Columns.Add("ORDER_REF_NO", typeof(string));
        }
        catch
        {
            throw;
        }
    }

    //private void BindOrderNo()
    //{
    //    try
    //    {
    //        DataTable dtOrder = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetLabDipOrderForSubmission(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
    //        if (dtOrder != null && dtOrder.Rows.Count > 0)
    //        {
    //            ddlOrderNo.DataSource = dtOrder;
    //            ddlOrderNo.DataBind();
    //            ddlOrderNo.Items.Insert(0, new ListItem("----- Select Order No -----", "0"));
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    private void BindOption()
    {
        try
        {
            ArrayList _Alphabet = new ArrayList();

            _Alphabet = new ArrayList();
            for (int i = 65; i < 91; i++)
            {
                _Alphabet.Add(Convert.ToChar(i));
            }
            ddlOption.DataSource = _Alphabet;
            ddlOption.DataBind();
            ddlOption.Items.Insert(0, new ListItem("---", "0"));
        }
        catch
        {
           throw;
        }
    }

    private void BindShade()
    {
        try
        {
            oFIBER_OD_SHADE_FAMILY = new SaitexDM.Common.DataModel.FIBER_OD_SHADE_FAMILY();
            oFIBER_OD_SHADE_FAMILY.COMP_CODE = oUserLoginDetail.COMP_CODE;
            DataTable dt = SaitexBL.Interface.Method.FIBER_OD_SHADE_FAMILY.GetShadeFamilyCodeALL(oFIBER_OD_SHADE_FAMILY);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeFamily.Items.Clear();
                ddlShadeFamily.DataSource = dt;
                ddlShadeFamily.DataBind();
                //ddlShadeFamily.Items.Insert(0, new ListItem("--Select Shade--", "select"));
                ddlShadeFamily.Items.Insert(0, new ListItem("NONE", "NONE"));
            }
        }
        catch
        {
            throw;
        }
    }

    //private void BindDepth()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("FABRIC_DEPTH", oUserLoginDetail.COMP_CODE);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlDepth.DataSource = dt;
    //            ddlDepth.DataBind();
    //            ddlDepth.Items.Insert(0, new ListItem("--Select Depth--", "select"));
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}

    protected void btnSaveDetail_Click(object sender, EventArgs e)
    {
        try
        {
           // ddlShadeFamily.SelectedValue = "NONE";
            if (ddlOrderNo.SelectedIndex != -1)
            {
                if (ddlLRNo.SelectedIndex != 0)
                {
                    if (ddlOption.SelectedIndex != 0)
                    {
                        if (ddlShadeFamily.SelectedValue != "select")
                        {
                            if (ddlShade.Text != string.Empty)
                            {
                                //if (ddlDepth.SelectedValue != "select")
                                if (txtDepth.Text != null)
                                {
                                    int UNIQUE_ID = 0;

                                    if (ViewState["UNIQUE_ID"] != null)
                                        UNIQUE_ID = int.Parse(ViewState["UNIQUE_ID"].ToString());

                                    if (ViewState["dtLRGenerate"] != null)
                                        dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

                                    if (dtLRGenerate == null)
                                        CreateLRGenerateTable();

                                    if (UNIQUE_ID > 0)
                                    {
                                        DataView dvEdit = new DataView(dtLRGenerate);
                                        dvEdit.RowFilter = "UNIQUE_ID=" + UNIQUE_ID;
                                        if (dvEdit.Count > 0)
                                        {
                                            if (txtRecipeCost.Text != "")
                                            {
                                                dvEdit[0]["TOTAL_RECIPE_COSE"] = double.Parse(txtRecipeCost.Text.Trim());
                                            }
                                            else
                                            {
                                                dvEdit[0]["TOTAL_RECIPE_COSE"] = 0;
                                            }
                                            dvEdit[0]["DEPTH"] = txtDepth.Text.ToString();
                                            //dvEdit[0]["DEPTH"] = ddlDepth.SelectedValue.ToString().Trim();
                                            dvEdit[0]["DEPTH_NAME"] = txtDepth.Text.ToString();
                                            dvEdit[0]["SUBMISSION_DATE"] = txtSubmissionDate.Text.Trim();
                                            dvEdit[0]["REMARKS"] = txtRemarks.Text.ToString();
                                            dvEdit[0]["ORDER_REF_NO"] = txtOrderRefNo.Text.ToString();
                                            dvEdit[0]["SHADE_FAMILY_NAME"] = ddlShadeFamily.SelectedValue.Trim();
                                            dvEdit[0]["SHADE_CODE_OLD"] = ddlShade.Text.Trim().ToUpper();
                                            dvEdit[0]["SHADE_NAME_OLD"] = ddlShade.Text.Trim().ToUpper();
                                            dvEdit[0]["SHADE_FAMILY_CODE_OLD"] = ddlShadeFamily.SelectedValue.ToString().Trim();
                                            dtLRGenerate.AcceptChanges();
                                        }
                                    }
                                    else
                                    {
                                        DataRow dr = dtLRGenerate.NewRow();
                                        dr["UNIQUE_ID"] = dtLRGenerate.Rows.Count + 1;
                                        dr["LR_OPTION"] = ddlOption.SelectedItem.ToString().Trim();
                                        dr["ORDER_NO"] = ddlOrderNo.SelectedValue.ToString().Trim();
                                        dr["ORDER_DATE"] = txtOrderDate.Text.Trim();
                                        dr["BRANCH_CODE"] = txtBranchCode.Text.Trim();
                                        dr["PRTY_CODE"] = txtCustomerCode.Text.Trim();
                                        dr["LAB_DIP_NO"] = ddlLRNo.SelectedItem.Text.Trim();
                                        dr["ARTICAL_NO"] = txtArticalNo.Text.Trim();
                                        dr["ARTICAL_DESC"] = txtQualityDesc.Text.Trim();
                                        dr["GREY_LOT_NO"] = txtLotNo.SelectedValue.ToString().Trim();
                                        dr["SHADE_FAMILY_CODE_OLD"] = ddlShadeFamily.SelectedValue.ToString().Trim();
                                        dr["SHADE_FAMILY_NAME"] = ddlShadeFamily.SelectedItem.Text.Trim();
                                        dr["SHADE_FAMILY_NAME"] = ddlShadeFamily.SelectedValue.ToString().Trim();
                                        dr["SHADE_CODE_OLD"] = ddlShade.Text.Trim().ToUpper();
                                        dr["SHADE_NAME_OLD"] = ddlShade.Text.Trim().ToUpper();                                      
                                        //  dr["COUNT"] = txtCount.Text.Trim();

                                        //if (txtPly.Text != "")
                                        //{
                                        //    dr["PLY"] = double.Parse(txtPly.Text.Trim());
                                        //}
                                        //else
                                        // {
                                        dr["PLY"] = 0;
                                        // }

                                        dr["QUALITY"] = txtQuality.Text.Trim();
                                        dr["SUBMISSION_DATE"] = txtSubmissionDate.Text.Trim();

                                        if (txtRecipeCost.Text != "")
                                        {
                                            dr["TOTAL_RECIPE_COSE"] = double.Parse(txtRecipeCost.Text.Trim());
                                        }
                                        else
                                        {
                                            dr["TOTAL_RECIPE_COSE"] = 0;
                                        }
                                        dr["DEPTH"] = txtDepth.Text.ToString();
                                        //dr["DEPTH"] = ddlDepth.SelectedValue.ToString().Trim();
                                        dr["DEPTH_NAME"] = txtDepth.Text.ToString();
                                        dr["REMARKS"] = txtRemarks.Text.ToString();
                                        dr["ORDER_REF_NO"] = txtOrderRefNo.Text.ToString();

                                        dtLRGenerate.Rows.Add(dr);
                                    }
                                    BindLRGrid();
                                    ddlLRNo.SelectedIndex = 0;
                                    ddlOption.SelectedIndex = 0;
                                    ddlShade.Text = string.Empty;
                                    //ddlShadeFamily.SelectedValue = "select";
                                    ddlShadeFamily.SelectedValue = "NONE";
                                    ClearControls();
                                    ddlShade.Enabled = true;
                                    ddlShadeFamily.Enabled = true;
                                    ddlLRNo.Enabled = true;
                                    ddlOption.Enabled = true;
                                    txtOrderRefNo.Text = string.Empty;

                                    //if (ddlShade.Items.Count > 0)
                                    //    ddlShade.Items.Clear();
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please provide Depth..");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please select Shade..");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please select Shade Family..");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please select Option..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select LR Number..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Order Number..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transaction Saving..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindLRGrid()
    {
        try
        {
            grdLabDipSubmission.DataSource = dtLRGenerate;
            ViewState["dtLRGenerate"] = dtLRGenerate;
            grdLabDipSubmission.DataBind();

            if (ViewState["UNIQUE_ID"] != null)
                ViewState["UNIQUE_ID"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void ClearControls()
    {
        try
        {
            ddlOption.SelectedIndex = 0;
            // ddlDepth.SelectedValue = "select";
            txtDepth.Text = string.Empty;
            //ddlShadeFamily.SelectedValue = "select";
            ddlShadeFamily.SelectedValue = "NONE";
            ViewState["UNIQUE_ID"] = null;
            txtArticalNo.Text = string.Empty;
            txtQualityDesc.Text = string.Empty;
            // txtCount.Text = string.Empty;
            //  txtPly.Text = string.Empty;
            txtDepth.Text = string.Empty;
            txtQuality.Text = string.Empty;
            txtRecipeCost.Text = string.Empty;
            txtRemarks.Text = string.Empty;
        }
        catch
        {
            throw;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ClearControls();
            ddlLRNo.Enabled = true;
            ddlShade.Enabled = true;
            ddlShadeFamily.Enabled = true;
            ddlOption.Enabled = true;
            ddlLRNo.SelectedIndex = 0;
            ddlShade.Text = string.Empty;
            ddlShadeFamily.SelectedValue = "NONE";
            //ddlShadeFamily.SelectedValue = "select";
            ddlOption.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Transaction Cancelling..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InsertData()
    {
        try
        {
            int bResult = 0;
            oFIBER_OD_LAB_DIP_ENTRY = new SaitexDM.Common.DataModel.FIBER_OD_LAB_DIP_ENTRY();

            if (ViewState["dtLRGenerate"] != null)
                dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

            if (Session["dtDye"] != null)
                dtDye = (DataTable)Session["dtDye"];

            if (ViewState["dtLRGenerate"] != null && dtLRGenerate.Rows.Count > 0)
            {
                if (Session["dtDye"] != null && dtDye.Rows.Count > 0)
                {
                    oFIBER_OD_LAB_DIP_ENTRY.COMP_CODE = oUserLoginDetail.COMP_CODE;
                    oFIBER_OD_LAB_DIP_ENTRY.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                    oFIBER_OD_LAB_DIP_ENTRY.YEAR = oUserLoginDetail.DT_STARTDATE.Year;
                    oFIBER_OD_LAB_DIP_ENTRY.TUSER = oUserLoginDetail.UserCode;

                    bResult = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.Insert_LRSubmission(oFIBER_OD_LAB_DIP_ENTRY, dtLRGenerate, dtDye, oUserLoginDetail.COMP_CODE, PRODUCT_TYPE, oUserLoginDetail.UserCode);
                    if (bResult > 0)
                    {
                        Session["saveStatus"] = 1;
                        Response.Redirect("./Fiber_LapDipSubmission.aspx?cId=S", false);
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Details Saving failed..");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Make Dye Name and Dose Percent first, according to LR Number and Option..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Create Transaction detail first, according to LR Number and Option..");
            }
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
            //UpdateData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the Data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Common.CommonFuction.ShowMessage("Sorry Dear ! No Deletion allowed..");
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deletion.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "You are in Update Mode";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./LabDipSubmission.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../../LabDip/Reports/Customer_Request_For_Yarn.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void grdLabDipSubmission_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int Unique_id = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EditTRN")
            {
                EditTRNRow(Unique_id);
                ddlLRNo.Enabled = false;
                ddlOption.Enabled = false;
                ddlShade.Enabled = false;
               // ddlShadeFamily.Enabled = false;
            }
            else if (e.CommandName == "DeleteTRN")
            {
                DeleteTRNRow(Unique_id);
                BindLRGrid();
                ddlLRNo.SelectedIndex = 0;
                ddlOption.SelectedIndex = 0;
                ddlShade.Text = string.Empty;
                ddlShadeFamily.SelectedValue = "NONE";
                //ddlShadeFamily.SelectedValue = "select";
                ClearControls();
                ddlOption.Enabled = true;
                ddlShade.Enabled = true;
                ddlShadeFamily.Enabled = true;
                ddlLRNo.Enabled = true;
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Grid Row Command.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void EditTRNRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtLRGenerate"] != null)
                dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

            DataView dv = new DataView(dtLRGenerate);
            dv.RowFilter = "UNIQUE_ID=" + UNIQUEID;
            if (dv.Count > 0)
            {
                ddlLRNo.SelectedIndex = ddlLRNo.Items.IndexOf(ddlLRNo.Items.FindByText(dv[0]["LAB_DIP_NO"].ToString()));
                ddlOption.SelectedValue = dv[0]["LR_OPTION"].ToString();
                txtArticalNo.Text = dv[0]["ARTICAL_NO"].ToString();
                txtQualityDesc.Text = dv[0]["ARTICAL_DESC"].ToString();
                txtLotNo.SelectedValue = dv[0]["GREY_LOT_NO"].ToString();
                ddlShadeFamily.SelectedIndex = ddlShadeFamily.Items.IndexOf(ddlShadeFamily.Items.FindByValue(dv[0]["SHADE_FAMILY_CODE_OLD"].ToString()));
                try
                {
                    BindShadeName(dv[0]["SHADE_FAMILY_CODE"].ToString());
                }
                catch { }
                //ddlShade.SelectedIndex = ddlShade.Items.IndexOf(ddlShade.Items.FindByValue(dv[0]["SHADE_CODE_OLD"].ToString()));
                ddlShade.Text = dv[0]["SHADE_CODE_OLD"].ToString();
                txtQuality.Text = dv[0]["QUALITY"].ToString();
                // txtCount.Text = dv[0]["COUNT"].ToString();
                //  txtPly.Text = dv[0]["PLY"].ToString();
                txtSubmissionDate.Text = dv[0]["SUBMISSION_DATE"].ToString();
                txtRecipeCost.Text = dv[0]["TOTAL_RECIPE_COSE"].ToString();
                txtDepth.Text = dv[0]["DEPTH"].ToString();
                txtRemarks.Text = dv[0]["REMARKS"].ToString();
                // ddlDepth.SelectedIndex = ddlDepth.Items.IndexOf(ddlDepth.Items.FindByValue(dv[0]["DEPTH"].ToString()));
                ViewState["UNIQUE_ID"] = UNIQUEID;
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteTRNRow(int UNIQUEID)
    {
        try
        {
            if (ViewState["dtLRGenerate"] != null)
                dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

            if (dtLRGenerate.Rows.Count == 1)
            {
                dtLRGenerate.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in dtLRGenerate.Rows)
                {
                    int iUNIQUEID = int.Parse(dr["UNIQUE_ID"].ToString());
                    if (iUNIQUEID == UNIQUEID)
                    {
                        dtLRGenerate.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in dtLRGenerate.Rows)
                {
                    iCount = iCount + 1;
                    dr["UNIQUE_ID"] = iCount;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }

    protected void ddlOrderNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            ddlLRNo.Enabled = true;
            ddlOption.Enabled = true;
            ddlShade.Enabled = true;
            ddlShadeFamily.Enabled = true;
            ClearControls();

            if (ViewState["dtLRGenerate"] != null)
                ViewState["dtLRGenerate"] = null;

            if (Session["dtDye"] != null)
                Session["dtDye"] = null;

            grdLabDipSubmission.DataSource = null;
            grdLabDipSubmission.DataBind();

            ddlLRNo.Items.Clear();
            DataTable dtOrder = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetLabDipDTLByORDERForSubmission(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim());
            if (dtOrder != null && dtOrder.Rows.Count > 0)
            {
                for (int iLoop = 0; iLoop < dtOrder.Rows.Count; iLoop++)
                {
                    txtOrderDate.Text = dtOrder.Rows[iLoop]["ORDER_DATE"].ToString();
                    txtBranchCode.Text = dtOrder.Rows[iLoop]["BRANCH_CODE"].ToString();
                    txtBranch.Text = dtOrder.Rows[iLoop]["BRANCH_NAME"].ToString();
                    txtCustomerCode.Text = dtOrder.Rows[iLoop]["PRTY_CODE"].ToString();
                    txtCustomerName.Text = dtOrder.Rows[iLoop]["PRTY_NAME"].ToString() + " " + dtOrder.Rows[iLoop]["PRTY_ADD1"].ToString();
                }
                ddlLRNo.DataSource = dtOrder;
                ddlLRNo.DataTextField = "LAB_DIP_NO";
                ddlLRNo.DataValueField = "LAB_DATA";
                ddlLRNo.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("There is no LR for submission, Select different Customer Request..");
            }
            ddlLRNo.Items.Insert(0, new ListItem("--Selct LR No--", "0"));
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Order Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlLRNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            string combstr = ddlLRNo.SelectedValue.Trim();
            string[] splitstr = combstr.Split('@');

            string LR_NO = splitstr[0].ToString();
            string ARTICAL_NO = splitstr[1].ToString();
            string QualityDesc = splitstr[2].ToString();
            string strShadeFamily = splitstr[3].ToString();
            ddlShadeFamily.SelectedIndex = ddlShadeFamily.Items.IndexOf(ddlShadeFamily.Items.FindByValue(strShadeFamily));
            BindShadeName(ddlShadeFamily.SelectedValue.ToString());
            string QUALITY = splitstr[4].ToString();
            string COUNT = string.Empty;
            COUNT = splitstr[5].ToString();
            double PLY = 0;
            double.TryParse(splitstr[6].ToString(), out PLY);
            string strShadeCode = splitstr[7].ToString();            
            txtOrderRefNo.Text = splitstr[8].ToString();
            ddlShade.Text = splitstr[8].ToString(); //strShadeCode;
            string DEPTH = string.Empty;
            
            double TOTAL_RECIPE_COSE = 0;

            
            txtArticalNo.Text = ARTICAL_NO;
            txtQualityDesc.Text = QualityDesc;
            txtQuality.Text = QUALITY;
            // txtCount.Text = COUNT;
            //  txtPly.Text = PLY.ToString();

            if (ddlOrderNo.SelectedIndex != -1)
            {
                if (ddlLRNo.SelectedIndex != 0)
                {
                    // Code to search LR in database
                    if (ViewState["dtLRGenerate"] != null)
                        dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

                    if (dtLRGenerate == null)
                        CreateLRGenerateTable();

                    DataTable dtMst = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetST_LabDip_MstByOrderAndLRNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.Trim().ToString(), LR_NO);
                    if (dtMst != null && dtMst.Rows.Count > 0)
                    {
                        grdLabDipSubmission.DataSource = null;
                        grdLabDipSubmission.DataBind();

                        for (int iLoop = 0; iLoop < dtMst.Rows.Count; iLoop++)
                        {
                            string LR_OPTION = dtMst.Rows[iLoop]["LR_OPTION"].ToString();
                            string SHADE_FAMILY_CODE_OLD = dtMst.Rows[iLoop]["SHADE_FAMILY_CODE_OLD"].ToString();
                            //ddlShadeFamily.SelectedIndex = ddlShadeFamily.Items.IndexOf(ddlShadeFamily.Items.FindByValue(dtMst.Rows[iLoop]["SHADE_FAMILY_CODE"].ToString()));
                            string SHADE_FAMILY_NAME = dtMst.Rows[iLoop]["SHADE_FAMILY_NAME"].ToString();
                            string SHADE_CODE_OLD = dtMst.Rows[iLoop]["SHADE_CODE_OLD"].ToString();
                            //BindShadeName(ddlShadeFamily.SelectedValue.ToString());
                            //ddlShade.SelectedIndex = ddlShade.Items.IndexOf(ddlShade.Items.FindByValue(dtMst.Rows[iLoop]["SHADE_CODE"].ToString()));
                            string SHADE_NAME_OLD = dtMst.Rows[iLoop]["SHADE_NAME_OLD"].ToString();
                            string SUBMISSION_DATE = dtMst.Rows[iLoop]["SUBMISSION_DATE"].ToString();
                            DEPTH = dtMst.Rows[iLoop]["DEPTH"].ToString();
                            string DEPTH_NAME = dtMst.Rows[iLoop]["DEPTH_NAME"].ToString();
                            string REMARKS = dtMst.Rows[iLoop]["REMARKS"].ToString();
                            string GREY_LOT_NO = dtMst.Rows[iLoop]["GREY_LOT_NO"].ToString();
                            string ORDER_REF_NO = dtMst.Rows[iLoop]["ORDER_REF_NO"].ToString();
                            double.TryParse(dtMst.Rows[iLoop]["TOTAL_RECIPE_COSE"].ToString(), out TOTAL_RECIPE_COSE);

                            if (dtLRGenerate != null && dtLRGenerate.Rows.Count > 0)
                            {
                                DataView dv = new DataView(dtLRGenerate);
                                dv.RowFilter = "LAB_DIP_NO = '" + LR_NO + "' AND LR_OPTION = '" + LR_OPTION + "'";
                                if (dv.Count > 0)
                                {

                                }
                                else
                                {
                                    DataRow dr = dtLRGenerate.NewRow();
                                    dr["UNIQUE_ID"] = dtLRGenerate.Rows.Count + 1;
                                    dr["LR_OPTION"] = LR_OPTION;
                                    dr["ORDER_NO"] = ddlOrderNo.SelectedValue.ToString().Trim();
                                    dr["ORDER_DATE"] = txtOrderDate.Text.Trim();
                                    dr["BRANCH_CODE"] = txtBranchCode.Text.Trim();
                                    dr["PRTY_CODE"] = txtCustomerCode.Text.Trim();
                                    dr["LAB_DIP_NO"] = ddlLRNo.SelectedItem.Text.Trim();
                                    dr["SHADE_FAMILY_CODE_OLD"] = SHADE_FAMILY_CODE_OLD;
                                    dr["SHADE_FAMILY_NAME"] = SHADE_FAMILY_NAME;
                                    dr["SHADE_CODE_OLD"] = SHADE_CODE_OLD;
                                    dr["SHADE_NAME_OLD"] = SHADE_NAME_OLD;
                                    dr["ARTICAL_NO"] = ARTICAL_NO;
                                    dr["ARTICAL_DESC"] = QualityDesc;
                                    dr["COUNT"] = COUNT;
                                    dr["PLY"] = PLY;
                                    dr["QUALITY"] = QUALITY;
                                    dr["SUBMISSION_DATE"] = SUBMISSION_DATE;
                                    dr["TOTAL_RECIPE_COSE"] = TOTAL_RECIPE_COSE;
                                    dr["DEPTH"] = DEPTH;
                                    dr["DEPTH_NAME"] = DEPTH_NAME;
                                    dr["REMARKS"] = REMARKS;
                                    dr["GREY_LOT_NO"] = GREY_LOT_NO;
                                    dr["ORDER_REF_NO"] = ORDER_REF_NO;
                                    dtLRGenerate.Rows.Add(dr);
                                }
                            }
                            else
                            {
                                DataRow dr = dtLRGenerate.NewRow();
                                dr["UNIQUE_ID"] = dtLRGenerate.Rows.Count + 1;
                                dr["LR_OPTION"] = LR_OPTION;
                                dr["ORDER_NO"] = ddlOrderNo.SelectedValue.ToString().Trim();
                                dr["ORDER_DATE"] = txtOrderDate.Text.Trim();
                                dr["BRANCH_CODE"] = txtBranchCode.Text.Trim();
                                dr["PRTY_CODE"] = txtCustomerCode.Text.Trim();
                                dr["LAB_DIP_NO"] = ddlLRNo.SelectedItem.Text.Trim();
                                dr["SHADE_FAMILY_CODE_OLD"] = SHADE_FAMILY_CODE_OLD;
                                dr["SHADE_FAMILY_NAME"] = SHADE_FAMILY_NAME;
                                dr["SHADE_CODE_OLD"] = SHADE_CODE_OLD;
                                dr["SHADE_NAME_OLD"] = SHADE_NAME_OLD;
                                dr["ARTICAL_NO"] = ARTICAL_NO;
                                dr["ARTICAL_DESC"] = QualityDesc;
                                dr["COUNT"] = COUNT;
                                dr["PLY"] = PLY;
                                dr["QUALITY"] = QUALITY;
                                dr["SUBMISSION_DATE"] = SUBMISSION_DATE;
                                dr["TOTAL_RECIPE_COSE"] = TOTAL_RECIPE_COSE;
                                dr["DEPTH"] = DEPTH;
                                dr["DEPTH_NAME"] = DEPTH_NAME;
                                dr["GREY_LOT_NO"] = GREY_LOT_NO;
                                dr["REMARKS"] = txtRemarks.Text.ToString();
                                dr["ORDER_REF_NO"] = txtOrderRefNo.Text.ToString();
                                dtLRGenerate.Rows.Add(dr);
                            }
                        }
                        MapTranDataTable(LR_NO);
                        BindLRGrid();
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select LR Number..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Order Number..");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of LR Number..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void MapTranDataTable(string LR_NO)
    {
        try
        {
            string DYE_NAME = string.Empty;
            string LR_OPTION = string.Empty;
            string DYE_DTL = string.Empty;
            string GREY_LOT_NO = string.Empty;
            double DOSE = 0, RATE = 0, RECIPE_COST = 0;
            string UOM = string.Empty;
            DataTable dtTrn = SaitexBL.Interface.Method.FIBER_OD_LAB_DIP_ENTRY.GetST_LabDip_TrnByOrderAndLRNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, ddlOrderNo.SelectedValue.ToString().Trim(), LR_NO);
            if (dtTrn != null && dtTrn.Rows.Count > 0)
            {
                DataView dvTrn = new DataView(dtTrn);
                if (dvTrn.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dvTrn.Count; iLoop++)
                    {
                        LR_OPTION = dvTrn[iLoop]["LR_OPTION"].ToString();
                        GREY_LOT_NO = dvTrn[iLoop]["GREY_LOT_NO"].ToString();
                        DYE_NAME = dvTrn[iLoop]["DYE_NAME"].ToString();
                        RATE = double.Parse(dvTrn[iLoop]["RATE"].ToString());
                        UOM = dvTrn[iLoop]["UOM"].ToString();
                        DOSE = double.Parse(dvTrn[iLoop]["DOSE"].ToString());
                        RECIPE_COST = double.Parse(dvTrn[iLoop]["RECIPE_COST"].ToString());
                        DYE_DTL = dvTrn[iLoop]["DYE_DTL"].ToString();
                        InsertDyeTrn(LR_NO, LR_OPTION, GREY_LOT_NO, DYE_NAME, RATE,UOM, DOSE, RECIPE_COST, DYE_DTL);
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void InsertDyeTrn(string LR_NO, string LR_OPTION, string GREY_LOT_NO, string DYE_NAME, double RATE,string UOM, double DOSE, double RECIPE_COST, string DYE_DTL)
    {
        try
        {
            if (Session["dtDye"] != null)
            {
                dtDye = (DataTable)Session["dtDye"];
            }
            else
            {
                dtDye = CreateDataTable();
            }

            if (dtDye != null)
            {
                DataView dv = new DataView(dtDye);
                dv.RowFilter = "LAB_DIP_NO = '" + LR_NO + "' AND LR_OPTION = '" + LR_OPTION + "' AND DYE_NAME = '" + DYE_NAME + "'";
                if (dv.Count > 0)
                {

                }
                else
                {
                    DataRow dr = dtDye.NewRow();
                    dr["UNIQUE_ID"] = dtDye.Rows.Count + 1;
                    dr["LAB_DIP_NO"] = LR_NO;
                    dr["LR_OPTION"] = LR_OPTION;
                    dr["GREY_LOT_NO"] = GREY_LOT_NO;
                    dr["DYE_NAME"] = DYE_NAME;
                    dr["ITEM_DESC"] = DYE_DTL;
                    dr["RATE"] = RATE;
                    dr["UOM"] = UOM;
                    dr["DOSE"] = DOSE;
                    dr["RECIPE_COST"] = RECIPE_COST;
                    dtDye.Rows.Add(dr);
                }
            }

            Session["dtDye"] = dtDye;
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtDye = new DataTable();
            dtDye.Columns.Add("UNIQUE_ID", typeof(int));
            dtDye.Columns.Add("LAB_DIP_NO", typeof(string));
            dtDye.Columns.Add("LR_OPTION", typeof(string));
            dtDye.Columns.Add("GREY_LOT_NO", typeof(string));
            dtDye.Columns.Add("DYE_NAME", typeof(string));
            dtDye.Columns.Add("ITEM_DESC", typeof(string));
            dtDye.Columns.Add("RATE", typeof(double));
            dtDye.Columns.Add("UOM", typeof(string));
            dtDye.Columns.Add("DOSE", typeof(double));
            dtDye.Columns.Add("RECIPE_COST", typeof(double));
            return dtDye;
        }
        catch
        {
            throw;
        }
    }

    protected void btnDyeName_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlLRNo.SelectedIndex != 0)
            {
                if (ddlOption.SelectedIndex != 0)
                {
                    txtRecipeCost.ReadOnly = false;
                    txtDepth.ReadOnly = false;
                    string URL = "Fiber_DyePopUp.aspx";
                    URL = URL + "?LRNo=" + ddlLRNo.SelectedItem.Text.Trim();
                    URL = URL + "&Option=" + ddlOption.SelectedItem.Text.Trim();
                    URL = URL + "&GREY_LOT_NO=" + txtLotNo.SelectedValue.ToString();
                    URL = URL + "&TextBoxId=" + txtRecipeCost.ClientID;
                    URL = URL + "&TextBoxId1=" + txtDepth.ClientID;
                    //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=850,height=400');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please select Option..");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select LR Number..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Dye Name Button Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlLRNo.SelectedIndex > 0)
            {
                if (ViewState["dtLRGenerate"] != null)
                    dtLRGenerate = (DataTable)ViewState["dtLRGenerate"];

                if (dtLRGenerate != null && dtLRGenerate.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtLRGenerate);
                    dv.RowFilter = "LAB_DIP_NO = '" + ddlLRNo.SelectedItem.Text.Trim() + "' AND LR_OPTION = '" + ddlOption.SelectedValue.ToString() + "'";
                    if (dv.Count > 0)
                    {
                        Common.CommonFuction.ShowMessage("Sorry! dear you can not add same Option with same LR Number, you have to take different Option..");
                        ddlOption.SelectedIndex = 0;
                    }
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please select LR Number first..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Option DropDown..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtRecipeCost_TextChanged(object sender, EventArgs e)
    {
        try
        {
            txtRecipeCost.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TextBox Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void txtDepth_TextChanged(object sender, EventArgs e)
    {
        try
        {

            txtDepth.ReadOnly = true;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in TextBox Changed Event..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void ddlShadeFamily_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlShadeFamily.SelectedValue != "select")
            {
                //ddlShade.Text = string.Empty;
                BindShadeName(ddlShadeFamily.SelectedValue.ToString());
            }
            else
            {
                Common.CommonFuction.ShowMessage("Please Select Shade..");
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Event Of Shade DropDown..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void BindShadeName(string SHADE_FAMILY_CODE)
    {
        try
        {
            strContext = oUserLoginDetail.COMP_CODE + "@" + SHADE_FAMILY_CODE + "@" + PRODUCT_TYPE;
            aceShade.ContextKey = strContext;

            //ddlShade.Items.Clear();
            //DataTable dtShadeName = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeTrnByComAndMstCode(oUserLoginDetail.COMP_CODE, SHADE_FAMILY_CODE);
            //if (dtShadeName != null && dtShadeName.Rows.Count > 0)
            //{
            //    ddlShade.DataSource = dtShadeName;
            //    ddlShade.DataBind();
            //    ddlShade.Items.Insert(0, new ListItem("--Shade Name--", "select"));
            //}
        }
        catch
        {
            throw;
        }
    }

    protected void ddlOrderNo_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);
            if (data != null && data.Rows.Count > 0)
            {
                ddlOrderNo.Items.Clear();
                ddlOrderNo.DataSource = data;
                ddlOrderNo.DataBind();
            }
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Article  loading.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();

        }
    }

    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = string.Empty;
            string whereClause = string.Empty;
            CommandText = " SELECT *  FROM  (SELECT * FROM (SELECT   DISTINCT (ORDER_NO) AS ORDER_NO,BRANCH_CODE,BRANCH_NAME,CR_BUSINESS_TYPE,CR_PRODUCT_TYPE FROM   V_TX_OD_LAB_DIP_ENTRY V WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE  and TUSER='" + oUserLoginDetail.UserCode + "' and LAB_DIP_NO NOT IN (SELECT LAB_DIP_ENTRY FROM V_TX_ST_LABDIP_SUB_MST B WHERE NVL(IS_APPROVED, 0) = '1' AND COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE  AND b.ORDER_NO = v.ORDER_NO) ORDER BY ORDER_NO ) asd WHERE    ORDER_NO LIKE :SearchQuery OR CR_PRODUCT_TYPE LIKE :SearchQuery) bd where ROWNUM <= 200 ";
            if (startOffset != 0)
            {
                whereClause += " AND ORDER_NO NOT IN (SELECT * FROM (SELECT   DISTINCT (ORDER_NO) AS ORDER_NO,BRANCH_CODE,BRANCH_NAME,CR_BUSINESS_TYPE,CR_PRODUCT_TYPE FROM   V_TX_OD_LAB_DIP_ENTRY V WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE and TUSER='" + oUserLoginDetail.UserCode + "' and LAB_DIP_NO NOT IN (SELECT LAB_DIP_ENTRY FROM V_TX_ST_LABDIP_SUB_MST B WHERE NVL(IS_APPROVED, 0) = '1' AND COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE  AND b.ORDER_NO = v.ORDER_NO) ORDER BY ORDER_NO ) asd WHERE    ORDER_NO LIKE :SearchQuery OR CR_PRODUCT_TYPE LIKE :SearchQuery) bd where ROWNUM  <= " + startOffset + ")";
            }
            string SortExpression = " ORDER BY ORDER_NO ASC";
           // string SearchQuery = text.ToUpper() + "%";
            string SearchQuery = "%" + text + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data;
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
            string CommandText = string.Empty;
            CommandText = " SELECT *  FROM  (SELECT * FROM (SELECT   DISTINCT (ORDER_NO) AS ORDER_NO,BRANCH_CODE,BRANCH_NAME,CR_BUSINESS_TYPE,CR_PRODUCT_TYPE FROM   V_TX_OD_LAB_DIP_ENTRY V WHERE COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE and LAB_DIP_NO NOT IN (SELECT LAB_DIP_ENTRY FROM V_TX_ST_LABDIP_SUB_MST B WHERE NVL(IS_APPROVED, 0) = '1' AND COMP_CODE = :COMP_CODE AND BRANCH_CODE = :BRANCH_CODE AND b.ORDER_NO = v.ORDER_NO) ORDER BY ORDER_NO ) asd WHERE    ORDER_NO LIKE :SearchQuery OR CR_PRODUCT_TYPE LIKE :SearchQuery)";
            string WhereClause = " ";
            string SortExpression = " ORDER BY ORDER_NO ASC";
            string SearchQuery = "%" + text + "%";
            DataTable data = SaitexBL.Interface.Method.OD_LAB_DIP_ENTRY.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            return data.Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void txtLotNo_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetLotData(e.Text.ToUpper(), e.ItemsOffset);
            txtLotNo.Items.Clear();
            txtLotNo.DataSource = data;
            txtLotNo.DataBind();
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetLotCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Merge No in party selection.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private DataTable GetLotData(string Text, int startOffset)
    {
        try
        {
            //string CommandText = "SELECT * FROM (SELECT   M.MST_CODE, M.MST_DESC  FROM   TX_MASTER_TRN M, YRN_BASE_ARTICLE_TRN T   WHERE   M.del_status = '0'  AND M.OTHER_INFO = T.ARTICLE_CODE       AND TRIM (RTRIM (M.MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))   AND T.YARN_CODE LIKE '%" + txtArticalNo.Text + "%'      AND (UPPER (M.MST_CODE) LIKE :SearchQuery              OR UPPER (M.MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1 AND  FINISHED_DENIER like  '%" + txtCustomerCode.Text + "%'               AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery)   )   WHERE   ROWNUM <= 15";
            string CommandText = "SELECT   * FROM   (SELECT   M.LOT_NO, T.FIBER_CODE  FROM   TX_FIBER_NEW_IR_TRN_SUB M, TX_FIBER_NEW_MASTER T WHERE   M.FIBER_CODE = T.FIBER_CODE AND T.FIBER_CODE LIKE :SearchQuery AND (UPPER (M.FIBER_CODE) LIKE :SearchQuery OR UPPER (T.FIBER_DESC) LIKE :SearchQuery))WHERE   ROWNUM <= 15";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                //whereClause += " AND MST_CODE NOT IN ( SELECT * FROM (SELECT   M.MST_CODE, M.MST_DESC  FROM   TX_MASTER_TRN M, YRN_BASE_ARTICLE_TRN T WHERE   M.del_status = '0'    AND M.OTHER_INFO = T.ARTICLE_CODE      AND TRIM (RTRIM (M.MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))      AND T.YARN_CODE LIKE '%" + txtArticalNo.Text + "%'   AND (UPPER (M.MST_CODE) LIKE :SearchQuery              OR UPPER (M.MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1 AND  FINISHED_DENIER like '%" + txtCustomerCode.Text + "%'      AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery )   ) WHERE  ROWNUM <= " + startOffset + ")";
                whereClause += "AND FIBER_CODE  ( SELECT * FROM(SELECT   * FROM   (SELECT   M.LOT_NO, T.FIBER_CODE  FROM   TX_FIBER_NEW_IR_TRN_SUB M, TX_FIBER_NEW_MASTER T WHERE   M.FIBER_CODE = T.FIBER_CODE AND T.FIBER_CODE LIKE :SearchQuery AND (UPPER (M.FIBER_CODE) LIKE :SearchQuery OR UPPER (T.FIBER_DESC) LIKE :SearchQuery))WHERE   ROWNUM <= " + startOffset + ")";
            }
            string SortExpression = " order by FIBER_CODE";
            string SearchQuery = Text + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery.ToUpper(), "");
        }
        catch
        {
            throw;
        }
    }
    protected int GetLotCount(string text)
    {

        //string CommandText = " SELECT   M.MST_CODE, M.MST_DESC  FROM   TX_MASTER_TRN M, YRN_BASE_ARTICLE_TRN T WHERE   M.del_status = '0' AND M.OTHER_INFO = T.ARTICLE_CODE AND T.YARN_CODE LIKE '%" + txtArticalNo.Text + "%'        AND TRIM (RTRIM (M.MST_NAME)) = LTRIM (RTRIM ('" + MST_NAME + "'))   AND T.YARN_CODE LIKE '%" + txtArticalNo.Text + "%'      AND (UPPER (M.MST_CODE) LIKE :SearchQuery              OR UPPER (M.MST_DESC) LIKE :SearchQuery)    UNION   SELECT   DISTINCT   LOT_NO MST_CODE,  MERGE_NO MST_DESC   FROM   V_YRN_LOT_MAKING      WHERE  CONF_FLAG=1 AND   FINISHED_DENIER like '%" + txtCustomerCode.Text + "%'               AND (   UPPER (LOT_NO) LIKE :SearchQuery OR UPPER (MERGE_NO) LIKE :SearchQuery)    ";
        string CommandText = "SELECT   * FROM   (SELECT   M.LOT_NO, T.FIBER_CODE  FROM   TX_FIBER_NEW_IR_TRN_SUB M, TX_FIBER_NEW_MASTER T WHERE   M.FIBER_CODE = T.FIBER_CODE AND T.FIBER_CODE LIKE :SearchQuery AND (UPPER (M.FIBER_CODE) LIKE :SearchQuery OR UPPER (T.FIBER_DESC) LIKE :SearchQuery))WHERE   ROWNUM <= 15";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;

    }

    protected void grdLabDipSubmission_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                GridViewRow grdRow = e.Row;
                if (Session["dtDye"] != null)
                {
                    dtDye = (DataTable)Session["dtDye"];
                }

                Label lrno = (Label)grdRow.FindControl("lblLRNO");
                Label lroption = (Label)grdRow.FindControl("lblOption");
                if (dtDye != null && dtDye.Rows.Count > 0)
                {
                    DataView dv = new DataView(dtDye);
                    dv.RowFilter= "LAB_DIP_NO='" + lrno.Text + "' and LR_OPTION='" + lroption.Text + "' ";
                    GridView grdPOTRN = (GridView)grdRow.FindControl("grdPOTRN");
                    grdPOTRN.DataSource = dv;
                    grdPOTRN.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting Po TRN Data.\r\nSee error log for detail."));
            lblErrorMessage.Text = ex.ToString();
        }
    }
}
