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
using Common;
using errorLog;
using System.IO;


public partial class Module_HRMS_Controls_Telephonebill : System.Web.UI.UserControl
{
   private static  string EMP_CODE=string.Empty;
   private static string DEPT_CODE = string.Empty;
   private static string DESIG_CODE = string.Empty;
   private static string BRANCH_CODE = string.Empty;

   private static string FilterMonth = string.Empty;
   private static string FilterYear = string.Empty;

   private static decimal MobileLmt=0;
   SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Initial_Control();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
        }
    }
    private void Initial_Control()
    {
        try
        {
            tdUpdate.Visible = false;
            Bind_Year();
            Bind_Grid_Record();
            bindddldepartment();
            BindDesignation();
            Bind_BrachName();
            bindddlemployee();
        }
        catch
        {
            throw;
        }
    }
    private void Bind_Year()
    {
        try
        {
            for (int i = -15; i < 15; i++)
            {
                DDLOpenYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
            }
            DDLOpenYear.Items.Insert(0, new ListItem("---------Select---------", ""));
            DDLOpenYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
            DDLOpenMonth.SelectedValue = System.DateTime.Now.Month.ToString();
        }
        catch
        {
            throw;
        }
    }
    private void bindddlemployee()
    {
        try
        {

            ddlemployee.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlemployee.DataValueField = "EMP_CODE";
                ddlemployee.DataTextField = "EMPLOYEENAME";
                ddlemployee.DataSource = dt;
                ddlemployee.DataBind();

            }
            ddlemployee.Items.Insert(0, new ListItem("---------Select----------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void Bind_Grid_Record()
    {
        try
        {
            if (ddldepartment.SelectedValue.ToString() != null && ddldepartment.SelectedValue.ToString() != string.Empty)
            {
                DEPT_CODE = ddldepartment.SelectedValue.ToString();
            }
            else
            {
                DEPT_CODE = string.Empty;
            }
            if (DDLDesign.SelectedValue.ToString() != null && DDLDesign.SelectedValue.ToString() != string.Empty)
            {
                DESIG_CODE = DDLDesign.SelectedValue.ToString();
            }
            else
            {
                DESIG_CODE = string.Empty;
            }
            if (DDLBranch.SelectedValue.ToString() != null && DDLBranch.SelectedValue.ToString() != string.Empty)
            {
                BRANCH_CODE = DDLBranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH_CODE = string.Empty;
            }
            if (ddlemployee.SelectedValue.ToString() != null && ddlemployee.SelectedValue.ToString() != string.Empty)
            {
                EMP_CODE = ddlemployee.SelectedValue.ToString();
            }
            else
            {
                EMP_CODE = string.Empty;
            }
            if (DDLOpenYear.SelectedValue.ToString() != null && DDLOpenYear.SelectedValue.ToString() != string.Empty)
            {
                FilterYear = DDLOpenYear.SelectedValue.ToString();
            }
            else
            {
                FilterYear = System.DateTime.Now.Year.ToString();
            }

            if (DDLOpenMonth.SelectedValue.ToString() != null && DDLOpenMonth.SelectedValue.ToString() != string.Empty)
            {
                FilterMonth = DDLOpenMonth.SelectedValue.ToString();
            }
            else
            {
                FilterMonth = System.DateTime.Now.Month.ToString();
            }
            DataTable dt = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Load_Grid_Record(EMP_CODE, DEPT_CODE, DESIG_CODE, BRANCH_CODE,FilterYear,FilterMonth );
            GVTelephoneRecord.DataSource = dt;
            GVTelephoneRecord.DataBind();
        }
        catch
        {
            throw;
        }
    }
    private bool Insert_Record()
    {
        bool Res = false;
        try
        {
            SaitexDM.Common.DataModel.HR_TELEPHONE_MST TM = new SaitexDM.Common.DataModel.HR_TELEPHONE_MST();

            for (int i = 0; i < GVTelephoneRecord.Rows.Count; i++)
            {
                string sMonth = string.Empty;
                string sYear = string.Empty;
                decimal AdjustAmt = 0;
                
                TM.OFFICIAL_CALL_AMOUNT =decimal.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtofficailcall")).Text.ToString()));
                TM.PERSONAL_CALL_AMOUNT =decimal.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtprsncall")).Text.ToString()));
                string BillId = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txttelid")).Text.ToString());
                if (TM.OFFICIAL_CALL_AMOUNT != 0 || TM.PERSONAL_CALL_AMOUNT != 0 && BillId!=string.Empty )
                {
                    CheckBox thisCheckbox = ((CheckBox)GVTelephoneRecord.Rows[i].FindControl("ChkAdjust"));
                    AdjustAmt = decimal.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("TxtAdjustAmt")).Text.ToString()));
                    TM.BILL_AMOUNT = decimal.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("TxtBillAmt")).Text.ToString()));
                    TM.EMP_DEDUCTION_AMOUNT = decimal.Parse(CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtamtpayble")).Text.ToString()));
                    if (thisCheckbox.Checked)
                    {
                        TM.ADJUSTMENT_AMOUNT  = AdjustAmt;
                    }
                    else
                    {
                        TM.ADJUSTMENT_AMOUNT = 0;
                    }
                    if (BillId != string.Empty)
                    {
                        TM.BILL_NO = decimal.Parse(BillId.Trim().ToString());
                    }
                    else
                    {
                        TM.BILL_NO = 0;
                    }                   
                    TM.TELEPHONE_NO = decimal.Parse(CommonFuction.funFixQuotes(((Label)GVTelephoneRecord.Rows[i].FindControl("LblMobileID")).Text.ToString()));
                    TM.EMP_CODE = (((Label)GVTelephoneRecord.Rows[i].FindControl("lblempcode")).Text);
                    //TM.OFFICIAL_CALL_AMOUNT = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtofficailcall")).Text);
                    //TM.PERSONAL_CALL_AMOUNT = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtprsncall")).Text);
                   
                    TM.REMARKS = CommonFuction.funFixQuotes(((TextBox)GVTelephoneRecord.Rows[i].FindControl("txtremarks")).Text);
                    sMonth = DDLOpenMonth.SelectedValue.Trim().ToString();
                    sYear = DDLOpenYear.SelectedValue.Trim().ToString();
                    
                    if (sMonth != string.Empty && sMonth != "")
                    {
                        TM.BILL_MONTH = int.Parse(sMonth.ToString());
                    }
                    else
                    {
                        TM.BILL_MONTH = int.Parse(System.DateTime.Now.Month.ToString());
                    }
                    if (sYear != string.Empty && sYear != "" && sYear!="0")
                    {
                        TM.BILL_YEAR = int.Parse(sYear.ToString());
                    }
                    else
                    {
                        TM.BILL_YEAR = int.Parse(oUserLoginDetail.OPEN_YEAR.Trim().ToString());
                    }
                    TM.COMP_CODE = oUserLoginDetail.COMP_CODE.Trim().ToString();
                    TM.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();
                    TM.TUSER = oUserLoginDetail.UserCode.Trim().ToString();
                    bool bResult = SaitexBL.Interface.Method.HR_TELEPHONE_MST.Insert_Telephone_Month_Bill(TM);
                    if (bResult)
                    {
                        Res = true;
                    }
                    else
                    {
                        Res = false;
                        return Res;
                    }
                }
            }
            return Res;
        }
        catch
        {
            throw;
        }

    }
    private void bindddldepartment()
    {
        try
        {
            ddldepartment.Items.Clear();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDepartment();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldepartment.DataValueField = "DEPT_CODE";
                ddldepartment.DataTextField = "DEPT_NAME";
                ddldepartment.DataSource = dt;
                ddldepartment.DataBind();
            }
            ddldepartment.Items.Insert(0, new ListItem("---------Select----------", string.Empty));
        }
        catch
        {
            throw;
        }
    }
    private void BindDesignation()
    {
        try
        {
            //////////////////////////// Bind Branch Name//////////////////////////////////////
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getEmployeeDesignation();
            DDLDesign.DataSource = dt;
            DDLDesign.DataValueField = "DESIG_CODE";
            DDLDesign.DataTextField = "DESIG_NAME";
            DDLDesign.DataBind();
            DDLDesign.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    private void Bind_BrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);
            DDLBranch.DataSource = dt;
            DDLBranch.DataValueField = "BRANCH_CODE";
            DDLBranch.DataTextField = "BRANCH_NAME";
            DDLBranch.DataBind();
            DDLBranch.Items.Insert(0, new ListItem("------------SELECT------------", ""));
            dt.Dispose();
            dt = null;
        }
        catch
        {
            throw;
        }
    }
    protected void DDLDesign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    protected void DDLBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    protected void GVTelephoneRecord_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GVTelephoneRecord.PageIndex = e.NewPageIndex;
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Gridview Paging.\r\nplease see error log"));
        }
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    private void Clear_Control()
    {
        try
        {
            TrSerach1.Visible = false;
            TrSerach2.Visible = false;
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
                if (Insert_Record())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Save Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Problem in saving the record');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Saving.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                if (Insert_Record())
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Update Successfully');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Problem in updating the record');", true);
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            TrSerach1.Visible = true; 
            TrSerach2.Visible = true ;
            tdUpdate.Visible = false;
            tdSave.Visible = false;
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in finding record.\r\nSee error log for detail."));
        }

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Printing record.\r\nSee error log for detail."));
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
                Response.Redirect("/Saitex/Module/HRMS/Pages/EmployeeHomePage.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Help file open.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear_Control();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Record Clearing.\r\nSee error log for detail."));
        }
    }
    protected void ChkAdjust_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox thisCheckbox = (CheckBox)sender;
            GridViewRow grdRow = (GridViewRow)thisCheckbox.Parent.Parent;
            TextBox TxtAdjustAmt = (TextBox)grdRow.FindControl("TxtAdjustAmt");
            CheckBox  ChkAdjust = (CheckBox)grdRow.FindControl("ChkAdjust");
            if (ChkAdjust.Checked)
            {
                TxtAdjustAmt.Enabled = true;
                TxtAdjustAmt.AutoPostBack = true;
            }
            else
            {
                TxtAdjustAmt.Enabled = false ;
                TxtAdjustAmt.AutoPostBack = false ;
                TxtAdjustAmt.Text = "0";
            }
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }
    protected void txtofficailcall_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal TotalPay = 0;
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            TextBox Txtmoblimit = (TextBox)grdRow.FindControl("txtmoblimit");
            TextBox txtamtpayble = (TextBox)grdRow.FindControl("txtamtpayble");
            TextBox TxtAdjustAmt = (TextBox)grdRow.FindControl("TxtAdjustAmt");

            TextBox TxtBillAmt = (TextBox)grdRow.FindControl("TxtBillAmt");
            TextBox txtprsncall = (TextBox)grdRow.FindControl("txtprsncall"); 

            if(Txtmoblimit.Text.Trim().ToString()!=string.Empty)
            {
                MobileLmt=decimal.Parse(Txtmoblimit.Text.Trim().ToString());
            }
            else
            {
                MobileLmt=0;
            }
            decimal OfficeCall = decimal.Parse(thisTextBox.Text.Trim().ToString());
            decimal BillAmt=decimal.Parse(TxtBillAmt.Text.Trim().ToString());
            decimal PersonalAmt = 0;
            PersonalAmt=BillAmt-OfficeCall;
            txtprsncall.Text = PersonalAmt.ToString();
            decimal AMT=0;
            if (OfficeCall > MobileLmt)
            {
                 AMT = OfficeCall - MobileLmt;
            }
            else
            {
                AMT = 0;
            }
            TotalPay = AMT + PersonalAmt;
            txtamtpayble.Text = TotalPay.ToString();
            TxtAdjustAmt.Text = AMT.ToString();
            
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }
    protected void TxtAdjustAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            TextBox Txtmoblimit = (TextBox)grdRow.FindControl("txtmoblimit");
            TextBox txtofficailcall = (TextBox)grdRow.FindControl("txtofficailcall");
            TextBox txtamtpayble = (TextBox)grdRow.FindControl("txtamtpayble");

            TextBox txtprsncall = (TextBox)grdRow.FindControl("txtprsncall");
            MobileLmt = decimal.Parse(Txtmoblimit.Text.Trim().ToString());
            decimal PersonalAmt = 0;
            decimal AdjustAmt = 0;
            decimal AmtPay = 0;
            decimal AMT=0;
            decimal TotalPay = 0;
            PersonalAmt = decimal.Parse(txtprsncall.Text.Trim().ToString());
            decimal OfficeCall = decimal.Parse(txtofficailcall.Text.Trim().ToString());
            if (OfficeCall > MobileLmt)
            {
                AMT = OfficeCall - MobileLmt;
            }
            else
            {
                AMT = 0;
            }
            AmtPay = AMT;
            AdjustAmt = decimal.Parse(thisTextBox.Text.Trim().ToString());          
            if (AdjustAmt > AmtPay)
            {
                AMT=0;
            }
            else
            {
                AMT=AmtPay-AdjustAmt ;
            }
            TotalPay = AMT + PersonalAmt;
            txtamtpayble.Text =AMT.ToString();              

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }
    protected void txtprsncall_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal TotalPay = 0;
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            TextBox Txtmoblimit = (TextBox)grdRow.FindControl("txtmoblimit");
            TextBox txtamtpayble = (TextBox)grdRow.FindControl("txtamtpayble");
            TextBox TxtAdjustAmt = (TextBox)grdRow.FindControl("TxtAdjustAmt");
            TextBox TxtBillAmt = (TextBox)grdRow.FindControl("TxtBillAmt");
            TextBox TxtOfficailCall = (TextBox)grdRow.FindControl("txtofficailcall");

            if (Txtmoblimit.Text.Trim().ToString() != string.Empty)
            {
                MobileLmt = decimal.Parse(Txtmoblimit.Text.Trim().ToString());
            }
            else
            {
                MobileLmt = 0;
            }
            decimal OfficeCall = decimal.Parse(TxtOfficailCall.Text.Trim().ToString());
            decimal PersonalCall = decimal.Parse(thisTextBox.Text.Trim().ToString());
            decimal BillAmt = decimal.Parse(TxtBillAmt.Text.Trim().ToString());
           
            OfficeCall = BillAmt - PersonalCall;
            TxtOfficailCall.Text = OfficeCall.ToString();

            decimal AMT = 0;
            if (OfficeCall > MobileLmt)
            {
                AMT = OfficeCall - MobileLmt;
            }
            else
            {
                AMT = 0;
            }
            TotalPay = AMT + PersonalCall;
            txtamtpayble.Text = TotalPay.ToString();
            TxtAdjustAmt.Text = AMT.ToString();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }
    protected void TxtBillAmt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            decimal TotalPay = 0;
            TextBox thisTextBox = (TextBox)sender;
            GridViewRow grdRow = (GridViewRow)thisTextBox.Parent.Parent;
            TextBox Txtmoblimit = (TextBox)grdRow.FindControl("txtmoblimit");
            TextBox TxtOfficailCall = (TextBox)grdRow.FindControl("txtofficailcall");
            TextBox txtamtpayble = (TextBox)grdRow.FindControl("txtamtpayble");
            TextBox txtprsncall = (TextBox)grdRow.FindControl("txtprsncall");
            decimal PersonalCall = decimal.Parse(txtprsncall.Text.Trim().ToString());
            decimal BillAmt = decimal.Parse(thisTextBox.Text.Trim().ToString());
            if (Txtmoblimit.Text.Trim().ToString() != string.Empty)
            {
                MobileLmt = decimal.Parse(Txtmoblimit.Text.Trim().ToString());
            }
            else
            {
                MobileLmt = 0;
            }
            decimal  OfficealCall = BillAmt - PersonalCall;
            TxtOfficailCall.Text = OfficealCall.ToString();
            decimal AMT = 0;
            if (OfficealCall > MobileLmt)
            {
                AMT = OfficealCall - MobileLmt;
            }
            else
            {
                AMT = 0;
            }
            TotalPay = AMT + PersonalCall;
            TxtOfficailCall.Text = OfficealCall.ToString();
            txtamtpayble.Text = TotalPay.ToString();

        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Adjustment.\r\nSee eroor log"));
        }
    }

    protected void DDLOpenYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
    protected void DDLOpenMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            Bind_Grid_Record();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Record loading.\r\nplease see error log"));
        }
    }
}
