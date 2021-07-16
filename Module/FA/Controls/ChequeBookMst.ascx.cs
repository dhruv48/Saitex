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

using errorLog;
using Common;
using DBLibrary;
using System.IO;
using Obout.ComboBox;

public partial class Module_FA_Controls_ChequeBookMst : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.FA_CHEQUEBOOK_MST oFA_CHEQUEBOOK_MST;
    bool chStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                InitialisePage();
            }
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading page.\r\nSee error log for detail."));
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "Save";
            lblErrorMessage.Text = "";
            lblMessage.Text = "";
            txtChequeBookCode.ReadOnly = true;
            cmbChequeBookCode.Visible = false;
            //grdChequeBook.AutoPostBackOnSelect = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            chk_Status.Checked = true;

            MaxChequeCode();
            BindGrid();
            BindBank();
            BindChequeBook();
        }
        catch
        {
            throw;
        }
    }

    //protected void grdChequeBook_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    //{
    //    try
    //    {
    //        string strChequeBookCode = "";

    //        cmbChequeBookCode.Visible = false;
    //        txtChequeBookCode.Visible = true;
    //        txtChequeBookCode.ReadOnly = true;

    //        ArrayList ar = grdChequeBook.SelectedRecords;

    //        lblMessage.Text = "";
    //        tdClear.Visible = true;
    //        tdDelete.Visible = true;
    //        tdUpdate.Visible = true;
    //        tdSave.Visible = false;

    //        Hashtable ht = (Hashtable)ar[0];

    //        strChequeBookCode = ht["CHEQUEBOOK_CODE"].ToString().Trim();
    //        BindData(strChequeBookCode);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in selecting data from Grid..\r\nSee error log for detail."));
    //    }
    //}

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InsertData();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Saving the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnFind_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            txtChequeBookCode.Visible = false;
            cmbChequeBookCode.Visible = true;
            //grdChequeBook.AutoPostBackOnSelect = true;
            cmbBankCode.Enabled = false;
            cmbChequeBookCode.Visible = true;
            cmbChequeBookCode.Enabled = true;
            txtChequeBookCode.Visible = false;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            lblMode.Text = "Update";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finding the data.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            //DeleteData();
            Common.CommonFuction.ShowMessage("Sorry! dear you can't delete any Cheque Book Entry");
            lblMessage.Text = "Sorry! dear you can't delete any Cheque Book Entry";
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in deleting the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./ChequeBookMaster.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Reports/ChequeBookMst_Rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in printing the data.\r\nSee error log for detail."));
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
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Help/ChequeBookMasterHelp.htm";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Helping.\r\nSee error log for detail."));
        }
    }

    private void InsertData()
    {
        try
        {
            string strACNo = "";
            if (cmbBankCode.SelectedIndex != -1)
            {
                if (txtChequeBookNo.Text != "")
                {
                    if (txtStartLeaf.Text != "")
                    {
                        if (txtNoOfCheque.Text != "")
                        {
                            if (txtEndLeaf.Text != "")
                            {
                                if (txtIssuedOn.Text != "")
                                {
                                    oFA_CHEQUEBOOK_MST = new SaitexDM.Common.DataModel.FA_CHEQUEBOOK_MST();
                                    oFA_CHEQUEBOOK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                    oFA_CHEQUEBOOK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
                                    oFA_CHEQUEBOOK_MST.LGR_BANK_CODE = cmbBankCode.SelectedValue.ToString().Trim();

                                    DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        DataView dv = new DataView(dt);
                                        dv.RowFilter = "LGR_BANK_CODE='" + cmbBankCode.SelectedValue.ToString().Trim() + "'";
                                        if (dv.Count > 0)
                                        {
                                            for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                            {
                                                strACNo = dv[iLoop]["BANK_AC_NO"].ToString();
                                            }
                                        }
                                    }

                                    oFA_CHEQUEBOOK_MST.BANK_AC_NO = strACNo;
                                    oFA_CHEQUEBOOK_MST.CHEQUEBOOK_CODE = txtChequeBookCode.Text.ToUpper().Trim();
                                    oFA_CHEQUEBOOK_MST.CHEQUEBOOK_NO = txtChequeBookNo.Text.ToUpper().Trim();
                                    oFA_CHEQUEBOOK_MST.START_LEAF = int.Parse(txtStartLeaf.Text.ToUpper().Trim());
                                    oFA_CHEQUEBOOK_MST.NO_OF_CHEQUE = int.Parse(txtNoOfCheque.Text.ToUpper().Trim());
                                    oFA_CHEQUEBOOK_MST.END_LEAF = int.Parse((int.Parse(txtStartLeaf.Text) + int.Parse(txtNoOfCheque.Text)).ToString());
                                    oFA_CHEQUEBOOK_MST.ISSUED_ON = DateTime.Parse(txtIssuedOn.Text.Trim());
                                    oFA_CHEQUEBOOK_MST.TUSER = oUserLoginDetail.UserCode;

                                    if (chk_Status.Checked == true)
                                    {
                                        chStatus = true;
                                    }
                                    else
                                    {
                                        chStatus = false;
                                    }
                                    oFA_CHEQUEBOOK_MST.STATUS = chStatus;
                                    oFA_CHEQUEBOOK_MST.DEL_STATUS = false;

                                    int iRecordFound = 0;

                                    bool bResult = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.InsertChequeBookMaster(oFA_CHEQUEBOOK_MST, out iRecordFound);

                                    if (bResult)
                                    {
                                        Session["saveStatus"] = 1;
                                        Response.Redirect("./ChequeBookMaster.aspx?cId=S", false);
                                    }
                                    else if (iRecordFound > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('This Record is already saved.. Please enter another.');", true);
                                    }
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please provide Issued Date...");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please provide End Leaf Number...");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please enter Number Of Cheques...");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please enter Start Leaf Number...");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please enter Cheque Book Number...");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Bank...");
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
            bool chStatus;
            string strACNo = "";

            if (cmbBankCode.SelectedIndex != -1)
            {
                if (txtChequeBookNo.Text != "")
                {
                    if (txtStartLeaf.Text != "")
                    {
                        if (txtNoOfCheque.Text != "")
                        {
                            if (txtEndLeaf.Text != "")
                            {
                                if (txtIssuedOn.Text != "")
                                {
                                    oFA_CHEQUEBOOK_MST = new SaitexDM.Common.DataModel.FA_CHEQUEBOOK_MST();

                                    if (chk_Status.Checked == true)
                                    {
                                        chStatus = true;
                                    }
                                    else
                                    {
                                        chStatus = false;
                                    }
                                    oFA_CHEQUEBOOK_MST.STATUS = chStatus;

                                    DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
                                    if (dt != null && dt.Rows.Count > 0)
                                    {
                                        DataView dv = new DataView(dt);
                                        dv.RowFilter = "LGR_BANK_CODE='" + cmbBankCode.SelectedValue.ToString().Trim() + "'";
                                        if (dv.Count > 0)
                                        {
                                            for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                                            {
                                                strACNo = dv[iLoop]["BANK_AC_NO"].ToString();
                                            }
                                        }
                                    }

                                    oFA_CHEQUEBOOK_MST.BANK_AC_NO = strACNo;
                                    oFA_CHEQUEBOOK_MST.LGR_BANK_CODE = cmbBankCode.SelectedValue.ToString().Trim();
                                    oFA_CHEQUEBOOK_MST.CHEQUEBOOK_CODE = txtChequeBookCode.Text.ToUpper().Trim();
                                    oFA_CHEQUEBOOK_MST.CHEQUEBOOK_NO = txtChequeBookNo.Text.ToUpper().Trim();
                                    oFA_CHEQUEBOOK_MST.START_LEAF = int.Parse(txtStartLeaf.Text.ToUpper().Trim());
                                    oFA_CHEQUEBOOK_MST.NO_OF_CHEQUE = int.Parse(txtNoOfCheque.Text.ToUpper().Trim());
                                    oFA_CHEQUEBOOK_MST.END_LEAF = int.Parse((int.Parse(txtStartLeaf.Text) + int.Parse(txtNoOfCheque.Text)).ToString());
                                    oFA_CHEQUEBOOK_MST.ISSUED_ON = DateTime.Parse(txtIssuedOn.Text.Trim());
                                    oFA_CHEQUEBOOK_MST.DEL_STATUS = false;
                                    oFA_CHEQUEBOOK_MST.TUSER = oUserLoginDetail.UserCode;
                                    oFA_CHEQUEBOOK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
                                    oFA_CHEQUEBOOK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

                                    int iRecordFound = 0;
                                    bool bResult = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.UpdateChequeBookMaster(oFA_CHEQUEBOOK_MST, out iRecordFound);

                                    if (bResult)
                                    {
                                        Session["saveStatus"] = 1;
                                        Response.Redirect("./ChequeBookMaster.aspx?cId=U", false);
                                    }
                                    else if (iRecordFound > 0)
                                    {
                                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record already exists..');", true);
                                    }
                                }
                                else
                                {
                                    Common.CommonFuction.ShowMessage("Dear! Please provide Issued Date...");
                                }
                            }
                            else
                            {
                                Common.CommonFuction.ShowMessage("Dear! Please provide End Leaf Number...");
                            }
                        }
                        else
                        {
                            Common.CommonFuction.ShowMessage("Dear! Please enter Number Of Cheques...");
                        }
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Dear! Please enter Start Leaf Number...");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Dear! Please enter Cheque Book Number...");
                }
            }
            else
            {
                Common.CommonFuction.ShowMessage("Dear! Please select Bank...");
            }
        }
        catch
        {
            throw;
        }
    }

    private void DeleteData()
    {
        try
        {
            oFA_CHEQUEBOOK_MST = new SaitexDM.Common.DataModel.FA_CHEQUEBOOK_MST();

            oFA_CHEQUEBOOK_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oFA_CHEQUEBOOK_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;

            if (txtChequeBookCode.Visible == true)
            {
                oFA_CHEQUEBOOK_MST.CHEQUEBOOK_CODE = CommonFuction.funFixQuotes(txtChequeBookCode.Text.ToString().Trim());
            }
            else
            {
                oFA_CHEQUEBOOK_MST.CHEQUEBOOK_CODE = CommonFuction.funFixQuotes(cmbChequeBookCode.SelectedValue.ToString().Trim());
            }

            bool bResult = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.DeleteChequeBookMaster(oFA_CHEQUEBOOK_MST);

            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("./ChequeBookMaster.aspx?cId=D", false);
            }
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Category Code.');", true);
        }
        catch
        {
            throw;
        }
    }

    private void MaxChequeCode()
    {
        try
        {
            string x = "";
            int y = 0;
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.MaxChequeCode();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["MAX_ID"].ToString();
                        y = int.Parse(x) + 1;
                        txtChequeBookCode.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindData(string strChequeBookCode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMasterWithoutStatus();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "CHEQUEBOOK_CODE='" + strChequeBookCode + "'";
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        //cmbBankCode.SelectedValue = dv[iLoop]["LGR_BANK_CODE"].ToString();
                        setBank(dv[iLoop]["LGR_BANK_CODE"].ToString());
                        txtChequeBookCode.Text = dv[iLoop]["CHEQUEBOOK_CODE"].ToString();
                        cmbChequeBookCode.SelectedText = dv[iLoop]["CHEQUEBOOK_CODE"].ToString();
                        txtChequeBookNo.Text = dv[iLoop]["CHEQUEBOOK_NO"].ToString();
                        txtStartLeaf.Text = dv[iLoop]["START_LEAF"].ToString();
                        txtNoOfCheque.Text = dv[iLoop]["NO_OF_CHEQUE"].ToString();
                        txtEndLeaf.Text = dv[iLoop]["END_LEAF"].ToString();
                        txtIssuedOn.Text = dv[iLoop]["ISSUED_ON"].ToString().Trim();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindGrid()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMasterWithoutStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                grdChequeBook.DataSource = dt;
                grdChequeBook.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    private void BindBank()
    {
        try
        {
            cmbBankCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbBankCode.DataTextField = "LGR_BANK_NAME";
                cmbBankCode.DataValueField = "LGR_BANK_CODE";
                cmbBankCode.DataSource = dt;
                cmbBankCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    public void setBank(string ledgerBankCode)
    {

        DataTable data = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        cmbBankCode.DataSource = data;
        cmbBankCode.DataTextField = "LGR_BANK_NAME";
        cmbBankCode.DataValueField = "LGR_BANK_CODE";
        cmbBankCode.DataBind();
        foreach (ComboBoxItem dl in cmbBankCode.Items)
        {
            if (dl.Value == ledgerBankCode)
            {
                cmbBankCode.SelectedIndex = cmbBankCode.Items.IndexOf(dl);
                break;
            }
        }
    }   


    private void BindChequeBook()
    {
        try
        {
            cmbChequeBookCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_CHEQUEBOOK_MST.GetChequeBookMasterWithoutStatus();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbChequeBookCode.DataValueField = "CHEQUEBOOK_CODE";
                cmbChequeBookCode.DataTextField = "CHEQUEBOOK_CODE";
                cmbChequeBookCode.DataSource = dt;
                cmbChequeBookCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void cmbBankCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Bank Code..\r\nSee error log for detail."));
        }
    }

    protected void cmbBankCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindBank();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Bank Code.\r\nSee error log for detail."));
        }
    }

    protected void cmbChequeBookCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            string strChequeBookCode = "";
            strChequeBookCode = cmbChequeBookCode.SelectedValue.ToString().Trim();
            BindData(strChequeBookCode);
            txtChequeBookCode.Visible = true;
            txtChequeBookCode.ReadOnly = true;
            cmbChequeBookCode.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Selecting Cheque Book Code.\r\nSee error log for detail."));
        }
    }

    protected void cmbChequeBookCode_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            BindChequeBook();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Loading Cheque Book Code.\r\nSee error log for detail."));
        }
    }

    protected void txtNoOfCheque_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtStartLeaf.Text != "")
            {
                txtEndLeaf.Text = (int.Parse(txtStartLeaf.Text) + int.Parse(txtNoOfCheque.Text) - 1).ToString();
            }
            else
            {
                lblMessage.Text = "Enter Starting Leaf First.";
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Text Changed of Number of Cheque.\r\nSee error log for detail."));
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
    protected void grdChequeBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            //BindData();
            grdChequeBook.PageIndex = e.NewPageIndex;
            grdChequeBook.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}