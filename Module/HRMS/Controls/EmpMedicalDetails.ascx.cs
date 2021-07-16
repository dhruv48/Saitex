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
using System.Data.OracleClient;

using Common;
using errorLog;
using System.IO;
using DBLibrary;
public partial class Module_HRMS_Controls_EmpMedicalDetails : System.Web.UI.UserControl
{
    public static string controlName;
    string strTUser = "";
    public static string strCompanyCode = string.Empty ;
    public static string strBranchCode = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["urLoginId"] != null)
            {                
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strTUser = oUserLoginDetail.UserCode;
                strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
                strBranchCode = oUserLoginDetail.CH_BRANCHCODE.Trim().ToString();

                if (!IsPostBack)
                {
                    imgbtnUpdate.Visible = false;
                    if (Request.QueryString["EMP_CODE"]!= null && Request.QueryString["EMP_CODE"]!= "")
                    {
                       
                        string EMP_Code = Request.QueryString["EMP_CODE"].ToString();
                        ddlfind.SelectedValue = EMP_Code;
                        Bind_Control(EMP_Code); 
                    }
                    else
                    {
                        lblMode.Text = "Save";
                    }
                    bindEmpCode();
                }
                if (chkphydis.Checked == true)
                {
                    RBPhysicalDis.Enabled = true;
                }
                else
                {
                    RBPhysicalDis.Enabled = false;
                }               
            }
            else
            {
                Response.Redirect("/Saitex/default.aspx", false);
            }
        }
        catch
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Error in Page Loading');", true);

        }
    }
    
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        Update();

    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Clear_Field();
    }
    private void Clear_Field()
    {
        txtadd.Text = string.Empty;
        Txtbmark.Text = string.Empty;
        txtcity.Text = string.Empty;
        TxtContry.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtesi.Text = string.Empty;
        Txtfax.Text = string.Empty;
        Txthelth.Text = string.Empty;
        Txthgt.Text = string.Empty;
        Txtmob.Text = string.Empty;
        txtmr.Text = string.Empty;
        Txtphydis.Text = string.Empty;
        txtpin.Text = string.Empty;
        txtstate.Text = string.Empty;
        TxtTel.Text = string.Empty;
        txtwgt.Text = string.Empty;
        lblMode.Text = "Save";
        ddlbldgrp.SelectedIndex = 0;
        ddlfind.SelectedIndex = 0;
       
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/EmpMedDtl_mst.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
    }  
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Help Msg');", true);
    }
    
    private void SaveGroup()
    {
        try
        {
            if (ddlfind.SelectedValue != "" && ddlfind.SelectedText != "Find")
            {
                int iRecordFound = 0;

                SaitexDM.Common.DataModel.HR_EMP_MED_DTL oHR_EMP_MED_DTL = new SaitexDM.Common.DataModel.HR_EMP_MED_DTL();
                SaitexDM.Common.DataModel.UserLoginDetail dtLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                string ecode = "";
                if (ddlfind.SelectedIndex != 0)

                    ecode = Common.CommonFuction.funFixQuotes(ddlfind.SelectedValue.ToString().Trim());
                oHR_EMP_MED_DTL.EMP_CODE = ecode;

                string bcode = "";
                if (ddlbldgrp.SelectedIndex != 0)
                    bcode = Common.CommonFuction.funFixQuotes(ddlbldgrp.Text.Trim());
                oHR_EMP_MED_DTL.EMP_BLD_GRP = bcode;

                oHR_EMP_MED_DTL.EMP_HIGHT = Common.CommonFuction.funFixQuotes(Txthgt.Text.Trim());
                oHR_EMP_MED_DTL.EMP_WEIGHT = Common.CommonFuction.funFixQuotes(txtwgt.Text.Trim());
                oHR_EMP_MED_DTL.EMP_B_MARKS = Common.CommonFuction.funFixQuotes(Txtbmark.Text.Trim());
                oHR_EMP_MED_DTL.ESI = Common.CommonFuction.funFixQuotes(txtesi.Text.Trim());
                oHR_EMP_MED_DTL.DIS = "";
                if (chkphydis.Checked == true)
                {
                    oHR_EMP_MED_DTL.CHK_DIS = true;                    
                    oHR_EMP_MED_DTL.DIS = RBPhysicalDis.SelectedIndex.ToString();
                }
                else
                {
                    oHR_EMP_MED_DTL.CHK_DIS = false;
                }
                oHR_EMP_MED_DTL.DIS_REMARKS = Common.CommonFuction.funFixQuotes(Txtphydis.Text.Trim());
                oHR_EMP_MED_DTL.H_REMARKS = Common.CommonFuction.funFixQuotes(Txthelth.Text.Trim());
                oHR_EMP_MED_DTL.EMER_NAME = Common.CommonFuction.funFixQuotes(txtmr.Text.Trim());
                oHR_EMP_MED_DTL.EMER_ADD = Common.CommonFuction.funFixQuotes(txtadd.Text.Trim());
                oHR_EMP_MED_DTL.EMER_CITY = Common.CommonFuction.funFixQuotes(txtcity.Text.ToUpper().Trim());
                oHR_EMP_MED_DTL.EMER_STATE = Common.CommonFuction.funFixQuotes(txtstate.Text.ToUpper().Trim());
                oHR_EMP_MED_DTL.EMER_PIN = Common.CommonFuction.funFixQuotes(txtpin.Text.Trim());
                oHR_EMP_MED_DTL.EMER_COUNTRY = Common.CommonFuction.funFixQuotes(TxtContry.Text.ToUpper().Trim());
                oHR_EMP_MED_DTL.EMER_M_NO = Common.CommonFuction.funFixQuotes(Txtmob.Text.Trim());
                oHR_EMP_MED_DTL.EMER_TEL_NO = Common.CommonFuction.funFixQuotes(TxtTel.Text.Trim());
                oHR_EMP_MED_DTL.EMER_FAX = Common.CommonFuction.funFixQuotes(Txtfax.Text.Trim());
                oHR_EMP_MED_DTL.EMER_EMAIL = Common.CommonFuction.funFixQuotes(txtEmail.Text.Trim());
                oHR_EMP_MED_DTL.TUSER = Common.CommonFuction.funFixQuotes(strTUser);
                oHR_EMP_MED_DTL.STATUS = chkActive.Checked;

                bool bResult = SaitexBL.Interface.Method.HR_EMP_MED_DTL.InsertEmpMedDTL(oHR_EMP_MED_DTL, out iRecordFound);

                if (bResult)
                {
                    Session["saveStatus"] = 1;
                    Common.CommonFuction.ShowMessage("Record Save Sucessfully");
                }
                else
                {

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save. Select Emp Code');", true);

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message.ToString());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);
        }

    }
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        SaveGroup();

    }
    protected void chkphydis_CheckedChanged(object sender, EventArgs e)
    {
        if (chkphydis.Checked == true)
        {
            RBPhysicalDis.Enabled = true;
        }
        else
        {
            RBPhysicalDis.Enabled = false;
        }
    }
    private void Update()
    {
        try
        {
            if (ddlfind.SelectedValue != "" && ddlfind.SelectedText != "Find")
            {
                int iRecordFound = 0;

                SaitexDM.Common.DataModel.HR_EMP_MED_DTL oHR_EMP_MED_DTL = new SaitexDM.Common.DataModel.HR_EMP_MED_DTL();
                SaitexDM.Common.DataModel.UserLoginDetail dtLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                string ecode = "";
                if (ddlfind.SelectedIndex != 0)

                    ecode = Common.CommonFuction.funFixQuotes(ddlfind.SelectedValue.ToString().Trim());
                oHR_EMP_MED_DTL.EMP_CODE = ecode;

                string bcode = "";
                if (ddlbldgrp.SelectedIndex != 0)
                    bcode = Common.CommonFuction.funFixQuotes(ddlbldgrp.Text.Trim());
                oHR_EMP_MED_DTL.EMP_BLD_GRP = bcode;

                oHR_EMP_MED_DTL.EMP_HIGHT = Common.CommonFuction.funFixQuotes(Txthgt.Text.Trim());
                oHR_EMP_MED_DTL.EMP_WEIGHT = Common.CommonFuction.funFixQuotes(txtwgt.Text.Trim());
                oHR_EMP_MED_DTL.EMP_B_MARKS = Common.CommonFuction.funFixQuotes(Txtbmark.Text.Trim());
                oHR_EMP_MED_DTL.ESI = Common.CommonFuction.funFixQuotes(txtesi.Text.Trim());
                oHR_EMP_MED_DTL.DIS = "";
                if (chkphydis.Checked == true)
                {
                    oHR_EMP_MED_DTL.CHK_DIS = true;
                    oHR_EMP_MED_DTL.DIS = RBPhysicalDis.SelectedIndex.ToString();
                }
                else
                {
                    oHR_EMP_MED_DTL.CHK_DIS = false;
                }
                oHR_EMP_MED_DTL.DIS_REMARKS = Common.CommonFuction.funFixQuotes(Txtphydis.Text.Trim());
                oHR_EMP_MED_DTL.H_REMARKS = Common.CommonFuction.funFixQuotes(Txthelth.Text.Trim());
                oHR_EMP_MED_DTL.EMER_NAME = Common.CommonFuction.funFixQuotes(txtmr.Text.Trim());
                oHR_EMP_MED_DTL.EMER_ADD = Common.CommonFuction.funFixQuotes(txtadd.Text.Trim());
                oHR_EMP_MED_DTL.EMER_CITY = Common.CommonFuction.funFixQuotes(txtcity.Text.Trim());
                oHR_EMP_MED_DTL.EMER_STATE = Common.CommonFuction.funFixQuotes(txtstate.Text.Trim());
                oHR_EMP_MED_DTL.EMER_PIN = Common.CommonFuction.funFixQuotes(txtpin.Text.Trim());
                oHR_EMP_MED_DTL.EMER_COUNTRY = Common.CommonFuction.funFixQuotes(TxtContry.Text.Trim());
                oHR_EMP_MED_DTL.EMER_M_NO = Common.CommonFuction.funFixQuotes(Txtmob.Text.Trim());
                oHR_EMP_MED_DTL.EMER_TEL_NO = Common.CommonFuction.funFixQuotes(TxtTel.Text.Trim());
                oHR_EMP_MED_DTL.EMER_FAX = Common.CommonFuction.funFixQuotes(Txtfax.Text.Trim());
                oHR_EMP_MED_DTL.EMER_EMAIL = Common.CommonFuction.funFixQuotes(txtEmail.Text.Trim());
                oHR_EMP_MED_DTL.TUSER = Common.CommonFuction.funFixQuotes(strTUser);
                oHR_EMP_MED_DTL.STATUS = chkActive.Checked;

                bool bResult = SaitexBL.Interface.Method.HR_EMP_MED_DTL.UpdateEmpMedDTL(oHR_EMP_MED_DTL, out iRecordFound);
                if (bResult)
                {
                    Common.CommonFuction.ShowMessage("Record Update Sucessfully");
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Duplicate Record.');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Update. ProvideEMP_CODE');", true);

            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message.ToString());
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('" + ex.Message + "');", true);
        }




    }
    protected void ddlempcode_SelectedIndexChanged(object sender, EventArgs e)
    {
        Bind_Control(ddlfind.SelectedValue.ToString().Trim());
    }
    private void Bind_Control(string EMP_CODE)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_EMP_MED_DTL.FindEmpCode();          
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dr = new DataView(dt);
                dr.RowFilter = "EMP_CODE='" + EMP_CODE  + "'";
                if (dr.Count > 0)
                {
                    imgbtnUpdate.Visible = true;
                    imgbtnNew.Visible = false;
                    lblMode.Text = "Update";               
                      ddlbldgrp.SelectedIndex = ddlbldgrp.Items.IndexOf(ddlbldgrp.Items.FindByValue(dr[0]["EMP_BLD_GRP"].ToString()));
                        char ch;
                        ch = char.Parse(dr[0]["CHK_DIS"].ToString());
                        if (ch == '1')
                        {
                            chkphydis.Checked = true;
                            RBPhysicalDis.Enabled = true;
                        }
                        else
                        {
                            chkphydis.Checked = false;
                            RBPhysicalDis.Enabled = false;
                        }
                        int ss = 0;
                        if (int.TryParse(dr[0]["DIS"].ToString(), out ss))
                        RBPhysicalDis.SelectedIndex = int.Parse(dr[0]["DIS"].ToString());
                        Txthgt.Text = dr[0]["EMP_HIGHT"].ToString().Trim();
                        txtwgt.Text = dr[0]["EMP_WEIGHT"].ToString().Trim();
                        Txtbmark.Text = dr[0]["EMP_B_MARKS"].ToString().Trim();
                        txtesi.Text = dr[0]["ESI"].ToString().Trim();
                        Txthelth.Text = dr[0]["H_REMARKS"].ToString().Trim();
                        txtmr.Text = dr[0]["EMER_NAME"].ToString().Trim();
                        txtadd.Text = dr[0]["EMER_ADD"].ToString().Trim();
                        txtcity.Text = dr[0]["EMER_CITY"].ToString().Trim();
                        txtstate.Text = dr[0]["EMER_STATE"].ToString().Trim();
                        txtpin.Text = dr[0]["EMER_PIN"].ToString().Trim();
                        TxtContry.Text = dr[0]["EMER_COUNTRY"].ToString().Trim();
                        Txtmob.Text = dr[0]["EMER_M_NO"].ToString().Trim();
                        TxtTel.Text = dr[0]["EMER_TEL_NO"].ToString().Trim();
                        Txtfax.Text = dr[0]["EMER_FAX"].ToString().Trim();
                        txtEmail.Text = dr[0]["EMER_EMAIL"].ToString().Trim();
                        Txtphydis.Text = dr[0]["DIS_REMARKS"].ToString().Trim();

                        if (dr[0]["STATUS"].ToString() == "1")
                            chkActive.Checked = true;
                        else
                            chkActive.Checked = false;                                   
                }
                else
                {                   
                    imgbtnNew.Visible = true;
                    imgbtnUpdate.Visible = false;
                    blank();                  

                }
            }

        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message.ToString());
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
        }
    }
    private void blank()
    {
        chkActive.Checked = false;
        ddlbldgrp.SelectedIndex = -1;
        chkphydis.Checked = false;
        RBPhysicalDis.SelectedIndex = -1;
        Txthgt.Text = "";
        txtwgt.Text = "";
        Txtbmark.Text = "";
        txtesi.Text = "";
        Txtphydis.Text = "";
        Txthelth.Text = "";
        txtmr.Text = "";
        txtadd.Text = "";
        txtcity.Text = "";
        txtstate.Text = "";
        txtpin.Text = "";
        TxtContry.Text = "";
        Txtmob.Text = "";
        TxtTel.Text = "";
        Txtfax.Text = "";
        txtEmail.Text = "";

    }

    protected void ddlfind_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        DataTable data = new DataTable();
        data = GetItems(e.Text, e.ItemsOffset, 10);
        ddlfind.DataSource = data;
        ddlfind.DataBind();
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        e.ItemsCount = GetItemsCount(e.Text);
    }
    protected void ddlfind_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {

            SaitexDM.Common.DataModel.HR_EMP_MED_DTL oHR_EMP_MED_DTL = new SaitexDM.Common.DataModel.HR_EMP_MED_DTL();
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.HR_EMP_MED_DTL.FindEmpCode();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dr = new DataView(dt);
                dr.RowFilter = "EMP_CODE='" + ddlfind.SelectedValue.ToString().Trim() + "'";
                if (dr.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dr.Count; iLoop++)
                    {

                        ddlbldgrp.SelectedIndex = ddlbldgrp.Items.IndexOf(ddlbldgrp.Items.FindByValue(dr[0]["EMP_BLD_GRP"].ToString()));
                        char ch;
                        ch = char.Parse(dr[0]["CHK_DIS"].ToString());
                        if (ch == '1')
                        {
                            chkphydis.Checked = true;
                            RBPhysicalDis.Enabled = true;
                        }
                        else
                        {
                            chkphydis.Checked = false;
                            RBPhysicalDis.Enabled = false;
                        }



                        int ss = 0;
                        if (int.TryParse(dr[0]["DIS"].ToString(), out ss))
                            RBPhysicalDis.SelectedIndex = int.Parse(dr[0]["DIS"].ToString());
                        Txthgt.Text = dr[0]["EMP_HIGHT"].ToString().Trim();
                        txtwgt.Text = dr[0]["EMP_WEIGHT"].ToString().Trim();
                        Txtbmark.Text = dr[0]["EMP_B_MARKS"].ToString().Trim();
                        txtesi.Text = dr[0]["ESI"].ToString().Trim();
                        Txthelth.Text = dr[0]["H_REMARKS"].ToString().Trim();
                        txtmr.Text = dr[0]["EMER_NAME"].ToString().Trim();
                        txtadd.Text = dr[0]["EMER_ADD"].ToString().Trim();
                        txtcity.Text = dr[0]["EMER_CITY"].ToString().Trim();
                        txtstate.Text = dr[0]["EMER_STATE"].ToString().Trim();
                        txtpin.Text = dr[0]["EMER_PIN"].ToString().Trim();
                        TxtContry.Text = dr[0]["EMER_COUNTRY"].ToString().Trim();
                        Txtmob.Text = dr[0]["EMER_M_NO"].ToString().Trim();
                        TxtTel.Text = dr[0]["EMER_TEL_NO"].ToString().Trim();
                        Txtfax.Text = dr[0]["EMER_FAX"].ToString().Trim();
                        txtEmail.Text = dr[0]["EMER_EMAIL"].ToString().Trim();
                        Txtphydis.Text = dr[0]["DIS_REMARKS"].ToString().Trim();

                        if (dr[0]["STATUS"].ToString() == "1")
                            chkActive.Checked = true;
                        else
                            chkActive.Checked = false;

                    }

                    imgbtnUpdate.Visible = true;
                    imgbtnNew.Visible = false;
                    lblMode.Text = "Update";
                }
                else
                {

                    imgbtnNew.Visible = true;
                    imgbtnUpdate.Visible = false;
                    blank();

                }
            }

        }
        catch
        {

        }
    }
    protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    {

        string whereClause = " WHERE EMP_CODE like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0' or F_NAME like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0'";
        string sortExpression = " ORDER BY EMP_CODE";
        string commandText = "SELECT * FROM  hr_emp_mst";
        string sPO = "";
        DataTable dt = SaitexBL.Interface.Method.HR_EMP_MED_DTL.GetDataForLOV(commandText, whereClause, sortExpression, "", text + '%', strCompanyCode,strBranchCode , sPO);
        return dt;
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = "SELECT COUNT(*) FROM hr_emp_mst WHERE EMP_CODE like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0' or F_NAME like :SearchQuery AND COMP_CODE=:COMP_CODE AND BRANCH_CODE=:BRANCH_CODE And DEL_STATUS = '0'";
        return SaitexBL.Interface.Method.HR_EMP_MED_DTL.GetCountForLOV(CommandText, text + '%',strCompanyCode,strBranchCode, "");
    }
    private void bindEmpCode()
    {
        try
        {
            DataTable data = new DataTable();
            data = GetItems ("", 0, 10);
            ddlfind .DataSource = data;
            ddlfind.DataBind();
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }
    }

    
}
