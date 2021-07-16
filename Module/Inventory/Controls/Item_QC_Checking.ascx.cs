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
using DBLibrary;
using Common;
using errorLog;
using System.Reflection;


public partial class Module_Inventory_Controls_Item_QC_Checking : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["LoginDetail"] != null)
            {
                bool NQC = false;
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                if (Request.QueryString["NQC"] != null)
                {
                    bool.TryParse(Request.QueryString["NQC"].ToString(), out NQC);
                }
                if (!IsPostBack)
                {
                    if (!NQC)
                    {
                        InitialisePage();
                    }
                    else
                    {
                        tdSave.Visible = true;
                        tdUpdate.Visible = false;
                        tdFind.Visible = true;
                        lblMode.Text = "Save";
                        AlreadyQC_Checked();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_Checking.aspx?NQC=true");
    }
    private void InitialisePage()
    {
        RemoveQuerystring();
        tdSave.Visible = true;
        tdUpdate.Visible = false;
        tdFind.Visible = true;
        lblMode.Text = "Save";
        bindMaterialReceiptApproval();
    }
    private DataTable CreateDataTable()
    {
        DataTable dtReceiptDetail = new DataTable();
        dtReceiptDetail.Columns.Add("COMP_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("BRANCH_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("TRN_NUMB", typeof(double));
        dtReceiptDetail.Columns.Add("QC_NUMB", typeof(double));
        dtReceiptDetail.Columns.Add("SR_NUMB", typeof(int));
        dtReceiptDetail.Columns.Add("YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("TRN_YEAR", typeof(int));
        dtReceiptDetail.Columns.Add("ITEM_CODE", typeof(string));
        dtReceiptDetail.Columns.Add("STD_TYPE", typeof(string));
        dtReceiptDetail.Columns.Add("QC_VALUE", typeof(double));
        dtReceiptDetail.Columns.Add("QC_RESULT", typeof(string));
        dtReceiptDetail.Columns.Add("QC_DONE_BY", typeof(string));
        dtReceiptDetail.Columns.Add("QC_REMARKS", typeof(string));
        dtReceiptDetail.Columns.Add("IsAdd", typeof(bool));

        return dtReceiptDetail;
    }

    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        tdSave.Visible = false;
        tdUpdate.Visible = true;
        lblMode.Text = "Update";
        RemoveQuerystring();
        EditQC_Checked();

        gvMaterialReceiptApproval.Columns[12].Visible = false;
        gvMaterialReceiptApproval.Columns[14].Visible = true;
    }

    private void RemoveQuerystring()
    {
        PropertyInfo isreadonly =
  typeof(System.Collections.Specialized.NameValueCollection).GetProperty(
  "IsReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);

        isreadonly.SetValue(this.Request.QueryString, false, null);

        this.Request.QueryString.Remove("NQC");
    }

    private void AlreadyQC_Checked()
    {

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetReceiptDataForQC_Approved_Or_NOT(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            gvMaterialReceiptApproval.DataSource = dt;
            gvMaterialReceiptApproval.DataBind();
            gvMaterialReceiptApproval.Columns[12].Visible = true;

            gvMaterialReceiptApproval.Columns[14].Visible = false;
        }
        else
        {
            gvMaterialReceiptApproval.DataSource = null;
            gvMaterialReceiptApproval.DataBind();
            lblTotalRecord.Text = "0";
            CommonFuction.ShowMessage("No MRNs QC is Checked");
        }
    }

    private void EditQC_Checked()
    {

        DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetReceiptDataForQC_NotApproved(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            gvMaterialReceiptApproval.DataSource = dt;
            gvMaterialReceiptApproval.DataBind();
            gvMaterialReceiptApproval.Columns[12].Visible = true;
            gvMaterialReceiptApproval.Columns[14].Visible = false;
        }
        else
        {
            gvMaterialReceiptApproval.DataSource = null;
            gvMaterialReceiptApproval.DataBind();
            lblTotalRecord.Text = "0";
            CommonFuction.ShowMessage("No MRNs QC is Checked");
        }
    }

    protected void imgbtnUpdate_Click1(object sender, ImageClickEventArgs e)
    {
        Save();
    }

    private void Save()
    {
        try
        {
            DataTable dtReceiptDetail = CreateDataTable();
            int totalRows = gvMaterialReceiptApproval.Rows.Count; int UNIQUE_ID = 0;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvMaterialReceiptApproval.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {

                    Label lblTRN_TYPE = (Label)thisGridViewRow.FindControl("lblTRN_TYPE");
                    Label lblITEM_CODE = (Label)thisGridViewRow.FindControl("lblITEM_CODE");
                    Label lblTRN_NUMB = (Label)thisGridViewRow.FindControl("lblTRN_NUMB");
                    Label lblSTD_TYPE = (Label)thisGridViewRow.FindControl("lblSTD_TYPE");
                    TextBox txtQCValue = (TextBox)thisGridViewRow.FindControl("txtQCValue");
                    TextBox txtRemarks = (TextBox)thisGridViewRow.FindControl("txtRemarks");
                    Label lblMAX_VALUE = (Label)thisGridViewRow.FindControl("lblMAX_VALUE");
                    Label lblMIN_VALUE = (Label)thisGridViewRow.FindControl("lblMIN_VALUE");
                    Label lblQC_NUMB = (Label)thisGridViewRow.FindControl("lblQC_NUMB");
                    Label lblTRN_YEAR = (Label)thisGridViewRow.FindControl("lblTRN_YEAR");
                    Label lblQC_Year = (Label)thisGridViewRow.FindControl("lblQC_Year");
                    CheckBox chkApproved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    CheckBox chkEdit = (CheckBox)thisGridViewRow.FindControl("chkEdit");
                    string QC_RESULT = "";
                    UNIQUE_ID++;
                    int QC_NUMB = 0, QC_Year = 0, TRN_YEAR = 0;
                    double TRN_NUMB = 0, QC_Value = 0, Max_Value = 0, Min_Value = 0;
                    double.TryParse(lblTRN_NUMB.Text, out TRN_NUMB);
                    double.TryParse(lblMAX_VALUE.Text, out Max_Value);
                    double.TryParse(lblMIN_VALUE.Text, out Min_Value);
                    
                    int.TryParse(lblQC_NUMB.Text, out QC_NUMB);
                    int.TryParse(lblTRN_YEAR.Text, out TRN_YEAR);
                    int.TryParse(lblQC_Year.Text, out QC_Year);

                    if (!string.IsNullOrEmpty(txtQCValue.Text))
                    {
                        if (double.TryParse(txtQCValue.Text, out QC_Value))
                        {
                            bool savedata = false;
                            // if (QC_NUMB != 0 && gvMaterialReceiptApproval.Columns[12].Visible == true && chkApproved.Checked == true)
                            if (gvMaterialReceiptApproval.Columns[12].Visible == true && chkApproved.Checked == true)
                            {
                                savedata = true;
                            }
                            else if (QC_NUMB == 0 && gvMaterialReceiptApproval.Columns[12].Visible == false)
                            {
                                savedata = true;
                            }
                            else if (QC_NUMB != 0 && gvMaterialReceiptApproval.Columns[12].Visible == false && tdUpdate.Visible == true && chkEdit.Checked == true)
                            {
                                savedata = true;
                            }
                            if (savedata)
                            {
                                if (QC_Value <= Max_Value && QC_Value >= Min_Value)
                                {
                                    QC_RESULT = "1";
                                }
                                else
                                {
                                    QC_RESULT = "0";

                                }
                                DataRow dr = dtReceiptDetail.NewRow();
                                dr["QC_NUMB"] = QC_NUMB;
                                dr["SR_NUMB"] = UNIQUE_ID;
                                dr["COMP_CODE"] = oUserLoginDetail.COMP_CODE;
                                dr["BRANCH_CODE"] = oUserLoginDetail.CH_BRANCHCODE;
                                dr["TRN_NUMB"] = TRN_NUMB;
                                dr["TRN_YEAR"] = TRN_YEAR;
                                dr["TRN_TYPE"] = lblTRN_TYPE.Text.Trim();
                                dr["YEAR"] = QC_Year == 0 ? oUserLoginDetail.DT_STARTDATE.Year : QC_Year;
                                dr["ITEM_CODE"] = lblITEM_CODE.Text.Trim();
                                dr["STD_TYPE"] = lblSTD_TYPE.Text.Trim();
                                dr["QC_VALUE"] = QC_Value;
                                dr["QC_RESULT"] = QC_RESULT;
                                dr["QC_DONE_BY"] = oUserLoginDetail.UserCode;
                                dr["QC_REMARKS"] = txtRemarks.Text;
                                dr["IsAdd"] = chkApproved != null ? chkApproved.Checked : false;
                                dtReceiptDetail.Rows.Add(dr);
                            }
                        }
                    }

                }
            }

            if (dtReceiptDetail != null && dtReceiptDetail.Rows.Count > 0)
            {
                bool is_save = false;
                if (tdSave.Visible)
                {
                    is_save = true;
                }
                bool iResult = SaitexBL.Interface.Method.TX_ITEM_IR_MST.InsertForQC_Checking(dtReceiptDetail, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.UserCode, is_save);
                if (iResult)
                {
                    InitialisePage();
                    CommonFuction.ShowMessage("QC Checking saved Successfully.");


                }
            }
            else
            {
                CommonFuction.ShowMessage("Please Enter QC Standard Value!!!");
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in QC Checking .\r\nSee error log for detail."));
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("~/Module/Inventory/Reports/Item_QC_CheckingReport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in printing.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

        InitialisePage();
        //Response.Redirect("~/Module/Inventory/Pages/Item_QC_Checking.aspx");
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
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
    private void bindMaterialReceiptApproval()
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.GetReceiptDataForQC(oUserLoginDetail.DT_STARTDATE.Year, oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvMaterialReceiptApproval.DataSource = dt;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                gvMaterialReceiptApproval.Columns[12].Visible = false;

            }
            else
            {
                lblTotalRecord.Text = "No MRN for approval";
                gvMaterialReceiptApproval.DataSource = null;
                gvMaterialReceiptApproval.DataBind();
                lblTotalRecord.Text = "0";
                CommonFuction.ShowMessage("No MRN for approval");
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
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }


    protected void txtQCValue_TextChanged(object sender, EventArgs e)
    {
        double QC_Value = 0, Max_Value = 0, Min_Value = 0;
        GridViewRow currentRow = ((GridViewRow)((TextBox)sender).NamingContainer);
        TextBox txtQCValue = (TextBox)currentRow.FindControl("txtQCValue");
        Label lblMAX_VALUE = (Label)currentRow.FindControl("lblMAX_VALUE");
        Label lblMIN_VALUE = (Label)currentRow.FindControl("lblMIN_VALUE");
        Image imgSuccess = (Image)currentRow.FindControl("imgSuccess");
        Image imgFail = (Image)currentRow.FindControl("imgFail");
        double.TryParse(lblMAX_VALUE.Text, out Max_Value);
        double.TryParse(lblMIN_VALUE.Text, out Min_Value);
        if (!string.IsNullOrEmpty(txtQCValue.Text))
        {
            if (double.TryParse(txtQCValue.Text, out QC_Value))
            {
                if (QC_Value <= Max_Value && QC_Value >= Min_Value)
                {
                    imgSuccess.Visible = true;
                    imgFail.Visible = false;
                }
                else
                {
                    imgSuccess.Visible = false;
                    imgFail.Visible = true;
                }
            }
            else
            {
                imgSuccess.Visible = false;
                imgFail.Visible = false;
            }
        }
        else
        {
            imgSuccess.Visible = false;
            imgFail.Visible = false;
        }

    }
    protected void gvMaterialReceiptApproval_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            double QC_Value = 0, Max_Value = 0, Min_Value = 0;
            TextBox txtQCValue = (TextBox)e.Row.FindControl("txtQCValue");
            Label lblMAX_VALUE = (Label)e.Row.FindControl("lblMAX_VALUE");
            Label lblMIN_VALUE = (Label)e.Row.FindControl("lblMIN_VALUE");
            Image imgSuccess = (Image)e.Row.FindControl("imgSuccess");
            Image imgFail = (Image)e.Row.FindControl("imgFail");
            Label lblQC_Approved_Result = (Label)e.Row.FindControl("lblQC_Approved_Result");
            Label lblQC_NUMB = (Label)e.Row.FindControl("lblQC_NUMB");
            TextBox txtRemarks = (TextBox)e.Row.FindControl("txtRemarks");


            if (lblQC_NUMB.Text == "0")
            {
                txtQCValue.Text = string.Empty;
            }

            double.TryParse(lblMAX_VALUE.Text, out Max_Value);
            double.TryParse(lblMIN_VALUE.Text, out Min_Value);
            double.TryParse(txtQCValue.Text, out QC_Value);
            if (!string.IsNullOrEmpty(txtQCValue.Text))
            {

                if (QC_Value <= Max_Value && QC_Value >= Min_Value)
                {
                    imgSuccess.Visible = true;
                    imgFail.Visible = false;
                }
                else
                {
                    imgSuccess.Visible = false;
                    imgFail.Visible = true;
                }
            }
            else
            {
                imgSuccess.Visible = false;
                imgFail.Visible = false;
            }
            if (tdUpdate.Visible)
            {
                txtQCValue.ReadOnly = true;
                txtRemarks.ReadOnly = true;
                gvMaterialReceiptApproval.Columns[14].Visible = true;
            }
            else
            {
                txtQCValue.ReadOnly = false;
                txtRemarks.ReadOnly = false;
                gvMaterialReceiptApproval.Columns[14].Visible = false;
            }
            if (lblQC_Approved_Result.Text.ToLower() == "pass")
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Green;
            }
            else if (lblQC_Approved_Result.Text.ToLower() == "fail")
            {
                e.Row.Cells[13].BackColor = System.Drawing.Color.Red;
            }



        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        Save();

    }
    protected void imgbtnList_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Inventory/Pages/Item_QC_CheckingList.aspx");
    }

    protected void chkEdit_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow currentRow = ((GridViewRow)((CheckBox)sender).NamingContainer);
        TextBox txtQCValue = (TextBox)currentRow.FindControl("txtQCValue");
        TextBox txtRemarks = (TextBox)currentRow.FindControl("txtRemarks");
        CheckBox chkEdit = (CheckBox)currentRow.FindControl("chkEdit");
        if (chkEdit.Checked)
        {
            txtQCValue.ReadOnly = false;
            txtRemarks.ReadOnly = false;
        }
        else
        {
            txtQCValue.ReadOnly = true;
            txtRemarks.ReadOnly = true;
        }

    }
}
