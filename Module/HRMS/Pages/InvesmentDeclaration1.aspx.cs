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
using DBLibrary;
using Common;
using errorLog;

public partial class Module_HRMS_Pages_InvesmentDeclaration1 : System.Web.UI.Page
{
    SaitexDM.Common.DataModel.HR_EMP_INVESMENT_DECLARATION HID = new SaitexDM.Common.DataModel.HR_EMP_INVESMENT_DECLARATION();
    private static DataTable DTEmpHouseRent = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            InitialisePage();
            Fill_EmpData_InForm();            
        }

    }
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    }
    private void Fill_EmpData_InForm()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_MST.Employee_Info_ByCode(Session["EmpCode"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtEmpName.Text = DT.Rows[0]["EMPLOYEENAME"].ToString();
                TxtEmpCode.Text = DT.Rows[0]["EMP_CODE"].ToString();
                TxtDesig.Text = DT.Rows[0]["DESIG_NAME"].ToString();
                TxtDept.Text = DT.Rows[0]["DEPT_NAME"].ToString();
                TxtAddress.Text = DT.Rows[0]["PADDRESS"].ToString();
                TxtPanCard.Text = DT.Rows[0]["PAN_NO"].ToString();
                TxtContactNo.Text = DT.Rows[0]["PERM_TEL_NO"].ToString();
                TxtEmail.Text = DT.Rows[0]["EMAIL_ID"].ToString();
                getEMP_HouseRent_Data(DT.Rows[0]["EMP_CODE"].ToString());
                Fill_Inv_Record();
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(ex.Message);
        }
    }

    private void Fill_Inv_Record()
    {
        try
        {
            DataTable DT = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION .GetInvestmentRecord(Session["EmpCode"].ToString());
            if (DT.Rows.Count > 0)
            {
                TxtPurDeductID.Text = DT.Rows[0]["HR_PUR_DEDUCT_ID"].ToString();
                TxtPropertyName.Text = DT.Rows[0]["PADDRESS"].ToString();
                TxtSOR .Text = DT.Rows[0]["SOR"].ToString();
                TxtDOL.Text = DT.Rows[0]["DOL"].ToString();
                TxtPOL.Text = DT.Rows[0]["POL"].ToString();
                TxtCopdate.Text = DT.Rows[0]["COP_DATE"].ToString();
                TxtDateOfProcess.Text = DT.Rows[0]["DATE_OF_PROCESS"].ToString();
                TxtPostAmount.Text = DT.Rows[0]["TOTAL_AMOUNT_POST"].ToString();
                TxtPreAmount.Text = DT.Rows[0]["TOTAL_AMOUNT_PRE"].ToString();

                txtSourceOfIncomeId.Text = DT.Rows[0]["SOURCE_OF_INCOME_ID"].ToString();
                TxtPreSalary3.Text = DT.Rows[0]["PRE_SALARY"].ToString();
                TxtHouseIncome3.Text = DT.Rows[0]["HOUSE_PROP_INCOME"].ToString();
                TxtBankInterest3.Text = DT.Rows[0]["BANK_INTEREST"].ToString();
                TxtOtherInterest3.Text = DT.Rows[0]["OTHER_INTEREST"].ToString();
                TxtOtherIncome3.Text = DT.Rows[0]["OTHER_INCOME"].ToString();

                TxtClaimDeductId.Text = DT.Rows[0]["HR_CLAIM_ID"].ToString();
                TxtLIP.Text = DT.Rows[0]["LIP"].ToString();
                TxtPPF.Text = DT.Rows[0]["PPF"].ToString();
                TxtULIP.Text = DT.Rows[0]["ULIP"].ToString();
                TxtNSC.Text = DT.Rows[0]["NSC"].ToString();
                TxtHouseLoan.Text = DT.Rows[0]["HOUSE_LOAN"].ToString();
                TxtPPP.Text = DT.Rows[0]["PPP"].ToString();
                TxtMF.Text = DT.Rows[0]["MF"].ToString();
                TxtITSI.Text = DT.Rows[0]["ITSI"].ToString();
                TxtNFD.Text = DT.Rows[0]["NFD"].ToString();
                TxtOtherFund.Text = DT.Rows[0]["OTHER_FUND"].ToString();
                TxtFCName.Text = DT.Rows[0]["FCHILD_NAME"].ToString();
                txtFCSchool.Text = DT.Rows[0]["FCHILD_SCHOOL"].ToString();
                TxtFCClass.Text = DT.Rows[0]["FCHILD_CLASS"].ToString();
                TxtFCTutionFee.Text = DT.Rows[0]["FCHILD_FEE"].ToString();
                TxtSCName.Text = DT.Rows[0]["SCHILD_NAME"].ToString();
                TxtSCSchool.Text = DT.Rows[0]["SCHILD_SCHOOL"].ToString();
                TxtSCClass.Text = DT.Rows[0]["SCHILD_CLASS"].ToString();
                TxtSCTutionFee.Text = DT.Rows[0]["SCHILD_FEE"].ToString();
                Txt1to2.Text = DT.Rows[0]["N1TO2"].ToString();
                Txt2to2.Text = DT.Rows[0]["N2TO2"].ToString();
                Txt2to3.Text = DT.Rows[0]["N2TO3"].ToString();
                Txt3to3.Text = DT.Rows[0]["N3TO3"].ToString();
                Txt3to4.Text = DT.Rows[0]["N3TO4"].ToString();
                Txt4to5.Text = DT.Rows[0]["N4TO5"].ToString();
                Txt5to6.Text = DT.Rows[0]["N5TO6"].ToString();
                Txt6to7.Text = DT.Rows[0]["N6TO7"].ToString();
                Txt7to8.Text = DT.Rows[0]["N7TO8"].ToString();
                Txt8To9.Text = DT.Rows[0]["N8TO9"].ToString();

                TxtOtherDeId.Text = DT.Rows[0]["OTHER_DE_ID"].ToString();
                TxtMedicalInsu.Text = DT.Rows[0]["MED_INSURANCE"].ToString();
                TxtMedicalTreat.Text = DT.Rows[0]["MED_TREAT"].ToString();
                TxtInterestOnLoan.Text = DT.Rows[0]["INTREST_ON_LOAN"].ToString();
                TxtDPP.Text = DT.Rows[0]["DPP"].ToString();
                txtOtherDeduct.Text = DT.Rows[0]["OTHER_DEDUCT"].ToString();
               
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            CommonFuction.ShowMessage(ex.Message);
        }
    }
  //------------------------------House Rent----------------------------------------
    private void InitialisePage()
    {
        try
        {
            RefreshDetailRow();
            //-----------------------For Employee Qualification Record-----------------------
            if (DTEmpHouseRent == null || DTEmpHouseRent.Rows.Count == 0)
                CreateEmpHouseRentTable();
            DTEmpHouseRent.Rows.Clear();           
            tdSave.Visible = true;         
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
        }
    }
    private void CreateEmpHouseRentTable()
    {
        DTEmpHouseRent = new DataTable();
        DTEmpHouseRent.Columns.Add("UniqueId", typeof(int));
        DTEmpHouseRent.Columns.Add("HR_HOUSE_RENT_ID", typeof(int));
        DTEmpHouseRent.Columns.Add("EMP_CODE", typeof(string));
        DTEmpHouseRent.Columns.Add("HR_PERIOD", typeof(string));
        DTEmpHouseRent.Columns.Add("HRHOUSE_ADD", typeof(string));
        DTEmpHouseRent.Columns.Add("HRRENTPAID", typeof(string));       

    }
    private void RefreshDetailRow()
    {
        TxtPeriodSec1.Text = "";
            TxtAddSec1.Text="";
            TxtRentSec1.Text = "";
        ViewState["UniqueId"] = null;
    }
    
    protected void lbtnsavedetail_Click(object sender, EventArgs e)
    {
        try
        {
            if (TxtPeriodSec1.Text != "" && TxtAddSec1.Text != "" && TxtRentSec1.Text != "")
            {
                int UniqueId = 0;
                if (ViewState["UniqueId"] != null)
                    UniqueId = int.Parse(ViewState["UniqueId"].ToString());
                DataRow dr;
                dr = DTEmpHouseRent.NewRow();             
                dr["UniqueId"] = DTEmpHouseRent.Rows.Count + 1;
                dr["EMP_CODE"] = Session["EmpCode"].ToString();
                dr["HR_PERIOD"] = TxtPeriodSec1.Text.ToString();
                dr["HRHOUSE_ADD"] = TxtAddSec1.Text.ToString();
                dr["HRRENTPAID"] = TxtRentSec1.Text.ToString();
                DTEmpHouseRent.Rows.Add(dr);
                RefreshDetailRow();
            }
            BindDetailGrid();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindDetailGrid()
    {
        try
        {
            GvHouserent.DataSource = DTEmpHouseRent;
            GvHouserent.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        RefreshDetailRow();
    }
    private void DeleteDetailRow(int UniqueId)
    {
        try
        {
            if (GvHouserent.Rows.Count == 1)
            {
                DTEmpHouseRent.Rows.Clear();
            }
            else
            {
                foreach (DataRow dr in DTEmpHouseRent.Rows)
                {
                    int iUniqueId = int.Parse(dr["UniqueId"].ToString());
                    if (iUniqueId == UniqueId)
                    {
                        DTEmpHouseRent.Rows.Remove(dr);
                        break;
                    }
                }
                int iCount = 0;
                foreach (DataRow dr in DTEmpHouseRent.Rows)
                {
                    iCount = iCount + 1;
                    dr["UniqueId"] = iCount;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GvHouserent_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int UniqueId = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName == "EmpEdit")
            {
                FillDetailByGrid (UniqueId);
            }
            else if (e.CommandName == "EmpDelete")
            {
                DeleteDetailRow(UniqueId);
                BindDetailGrid();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + ex.Message + "');", true);
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void FillDetailByGrid(int UniqueId)
    {
        try
        {
            DataView dv = new DataView(DTEmpHouseRent);
            dv.RowFilter = "UniqueId=" + UniqueId;
            if (dv.Count > 0)
            {
                 TxtPeriodSec1.Text = dv[0]["HR_PERIOD"].ToString();
                TxtAddSec1.Text = dv[0]["HRHOUSE_ADD"].ToString();
                TxtRentSec1.Text = dv[0]["HRRENTPAID"].ToString();
              
                ViewState["UniqueId"] = UniqueId;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getEMP_HouseRent_Data(string iEMPMasterCode)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.GetHouseRentDetail (iEMPMasterCode);
            foreach (DataRow Drow in DTable.Rows)
            {
                DataRow dr;
                dr = DTEmpHouseRent.NewRow();
                dr["UniqueId"] = DTEmpHouseRent.Rows.Count + 1;
                dr["HR_HOUSE_RENT_ID"] = Drow["HR_HOUSE_RENT_ID"];
                dr["EMP_CODE"] = Drow["EMP_CODE"].ToString();
                dr["HR_PERIOD"] = Drow["HR_PERIOD"].ToString();
                dr["HRHOUSE_ADD"] = Drow["HRHOUSE_ADD"].ToString();
                dr["HRRENTPAID"] = Drow["HRRENTPAID"].ToString();               
                DTEmpHouseRent.Rows.Add(dr);
            }
            GvHouserent.DataSource = DTEmpHouseRent;
            GvHouserent.DataBind();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }

    }
    //------------------------------House Rent----------------------------------------

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Save_Record())
            {
                Common.CommonFuction.ShowMessage("Record save sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Unable to save!please try agin");
            }
        }
        catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    private bool Save_Record()
    {
        try
        {
            HID.EMP_CODE = Session["EmpCode"].ToString();
            //------------------------Section 2--------------------------------
            HID.HR_PUR_DEDUCT_ID =Int32.Parse(TxtPurDeductID.Text.ToString());
            HID.PADDRESS = TxtPropertyName.Text.ToString();
            HID.SOR = TxtSOR.Text.ToString();
            if (TxtDOL.Text.ToString() != "")
            {
                HID.DOL = DateTime.Parse(TxtDOL.Text.ToString());
            }
            else
            {
                HID.DOL = DateTime.Now;
            }
            if (TxtCopdate.Text.ToString() != "")
            {
                HID.COP_DATE = DateTime.Parse(TxtCopdate.Text.ToString());
            }
            else
            {
                HID.COP_DATE = DateTime.Now;
            }
            if (TxtDateOfProcess.Text.ToString() != "")
            {
                HID.DATE_OF_PROCESS = DateTime.Parse(TxtDateOfProcess.Text.ToString());
            }
            else
            {
                HID.DATE_OF_PROCESS = DateTime.Now;
            } 
            HID.POL = TxtPOL.Text.ToString();        
           
            HID.TOTAL_AMOUNT_POST = Int32.Parse( TxtPostAmount.Text.ToString());
            HID.TOTAL_AMOUNT_PRE = Int32.Parse( TxtPreAmount.Text.ToString());
            //------------------------Section 2--------------------------------

            //------------------------Section 3--------------------------------
            HID.SOURCE_OF_INCOME_ID = Int32.Parse( txtSourceOfIncomeId.Text.ToString());
            HID.PRE_SALARY = Int32.Parse( TxtPreSalary3.Text.ToString());
            HID.HOUSE_PROP_INCOME = Int32.Parse( TxtHouseIncome3.Text.ToString());
            HID.BANK_INTEREST = Int32.Parse( TxtBankInterest3.Text.ToString());
            HID.OTHER_INTEREST = Int32.Parse( TxtOtherInterest3.Text.ToString());
            HID.OTHER_INCOME = TxtOtherIncome3.Text.ToString();
            //------------------------Section 3--------------------------------

            //------------------------Section 4--------------------------------
            HID.HR_CLAIM_ID = Int32.Parse(TxtClaimDeductId.Text.ToString());
            HID.LIP = Int32.Parse( TxtLIP.Text.ToString());
            HID.PPF = Int32.Parse(TxtPPF.Text.ToString());
            HID.ULIP = Int32.Parse(TxtULIP.Text.ToString());
            HID.NSC = Int32.Parse(TxtNSC.Text.ToString());
            HID.HOUSE_LOAN = Int32.Parse(TxtHouseLoan.Text.ToString());
            HID.PPP = Int32.Parse(TxtPPP.Text.ToString());
            HID.MF = Int32.Parse(TxtMF.Text.ToString());

            HID.FCHILD_NAME = TxtFCName.Text.ToString();
            HID.FCHILD_SCHOOL = txtFCSchool.Text.ToString();
            HID.FCHILD_CLASS = TxtFCClass.Text.ToString();
            HID.FCHILD_FEE = Int32.Parse(TxtFCTutionFee.Text.ToString());

            HID.SCHILD_NAME = TxtSCName.Text.ToString();
            HID.SCHILD_SCHOOL = TxtSCSchool.Text.ToString();
            HID.SCHILD_CLASS = TxtSCClass.Text.ToString();
            HID.SCHILD_FEE = Int32.Parse(TxtSCTutionFee.Text.ToString());

            HID.ITSI = Int32.Parse(TxtITSI.Text.ToString());
            HID.NFD = Int32.Parse(TxtNFD.Text.ToString());
            HID.OTHER_FUND = TxtOtherFund.Text.ToString();

            HID.N1TO2 = Int32.Parse(Txt1to2.Text.ToString());
            HID.N2TO2 = Int32.Parse(Txt2to2.Text.ToString());
            HID.N2TO3 = Int32.Parse(Txt2to3.Text.ToString());
            HID.N3TO3 = Int32.Parse(Txt3to3.Text.ToString());
            HID.N3TO4 = Int32.Parse(Txt3to4.Text.ToString());
            HID.N4TO5 = Int32.Parse(Txt4to5.Text.ToString());
            HID.N5TO6 = Int32.Parse(Txt5to6.Text.ToString());
            HID.N6TO7 = Int32.Parse(Txt6to7.Text.ToString());
            HID.N7TO8 = Int32.Parse(Txt7to8.Text.ToString());
            HID.N8TO9 = Int32.Parse(Txt8To9.Text.ToString());
            //------------------------Section 4--------------------------------

            //------------------------Section 5--------------------------------
            HID.OTHER_DE_ID = Int32.Parse( TxtOtherDeId.Text.ToString());
            HID.MED_INSURANCE = Int32.Parse( TxtMedicalInsu.Text.ToString());
            HID.MED_TREAT = Int32.Parse( TxtMedicalTreat.Text.ToString());
            HID.INTREST_ON_LOAN = Int32.Parse( TxtInterestOnLoan.Text.ToString());
            HID.DPP = Int32.Parse( TxtDPP.Text.ToString());
            HID.OTHER_DEDUCT = txtOtherDeduct.Text.ToString();
            //------------------------Section 5--------------------------------

            bool rES = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.Save_Investment_Record(DTEmpHouseRent, HID);
            //bool rES1 = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.Save_Inv_House_Rent_Record(DTEmpHouseRent);
            //bool rES2 = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.Save_Inv_Purch_Deduct_Record(HID);
            //bool rES3 = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.Save_Inv_Source_Of_Income_Record(HID);
            //bool rES5 = SaitexBL.Interface.Method.HR_EMP_INVESMENT_DECLARATION.Save_Inv_Other_Deduct_Record(HID);
            if (rES) { return true; } else { return false; }

        }
          catch(Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            return false;
        }
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "";
        URL = "InvestmentRpt.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("./InvesmentDeclaration1.aspx", false);
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
}

