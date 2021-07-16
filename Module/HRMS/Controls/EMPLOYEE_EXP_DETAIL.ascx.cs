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
using Obout.ComboBox;

public partial class Module_HRMS_Controls_EMPLOYEE_EXP_DETAIL : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.EMPLOYEE_EXP_DETAIL oEMPLOYEE_EXP_DETAIL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                if (!Page.IsPostBack)
                {
                    InitialControls();
                    //BindEmployee();
                    //DataBind();
                    if (Request.QueryString["EMP_CODE"] != null && Request.QueryString["EMP_CODE"] != "") 
                    {
                        string EMP_Code = Request.QueryString["EMP_CODE"].ToString();
                        txtempcode.Text = EMP_Code;
                        //Employee_Code(EMP_Code);
                        DataBind(EMP_Code);
                    }
                    else
                    {

                    }
                }
            }
            else
            {
                Response.Redirect("~/Default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void InitialControls()
    {
        try
        {
            //ddlempcode.SelectedIndex = -1;
            txtempName.Text = string.Empty;
            txtcompname.Text = string.Empty;
            txtcomplevel.Text = string.Empty;
            txtreferby.Text = string.Empty;
            txtdesignation.Text = string.Empty;
            txtdepartment.Text = string.Empty;
            txtfrom_dt.Text = string.Empty;
            txtcomplocation.Text = string.Empty;
            txtTo_dt.Text = string.Empty;
            txtctc.Text = string.Empty;
            txtleavingreason.Text = string.Empty;
            EmpExpDataDind.Visible = true;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //private void BindEmployee()
    //{
    //    try
    //    {

    //        DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlempcode.Items.Clear();
    //            ddlempcode.DataValueField = "EMP_CODE";
    //            ddlempcode.DataTextField = "EMPLOYEENAME";
    //            ddlempcode.DataSource = dt;
    //            ddlempcode.DataBind();
    //        }
    //        ddlempcode.Items.Insert(0, new ListItem("---------SELECT--------", string.Empty));
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

    private void SavaData()
    {
        try
        {
            oEMPLOYEE_EXP_DETAIL = new SaitexDM.Common.DataModel.EMPLOYEE_EXP_DETAIL();

            
            oEMPLOYEE_EXP_DETAIL.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oEMPLOYEE_EXP_DETAIL.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oEMPLOYEE_EXP_DETAIL.EMP_CODE = CommonFuction.funFixQuotes(txtempcode.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.EMP_NAME = CommonFuction.funFixQuotes(txtempName.Text.ToUpper().Trim());
            //oEMPLOYEE_EXP_DETAIL.EMP_CODE = ddlempcode.SelectedValue.ToString().Trim();
            oEMPLOYEE_EXP_DETAIL.OLD_COMPANY = CommonFuction.funFixQuotes(txtcompname.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.COMPANY_LEVEL = CommonFuction.funFixQuotes(txtcomplevel.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.REFER_BY = CommonFuction.funFixQuotes(txtreferby.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.DESIGNATION = CommonFuction.funFixQuotes(txtdesignation.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.DEPARTMENT = CommonFuction.funFixQuotes(txtdepartment.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.C_LOCATION = CommonFuction.funFixQuotes(txtcomplocation.Text.ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.FROM_DATE = DateTime.Parse(txtfrom_dt.Text.Trim());
            oEMPLOYEE_EXP_DETAIL.T_DATE = DateTime.Parse(txtTo_dt.Text.Trim());
            oEMPLOYEE_EXP_DETAIL.LAST_CTC = int.Parse(txtctc.Text.ToString().ToUpper().Trim());
            oEMPLOYEE_EXP_DETAIL.LEAVING_REASON = CommonFuction.funFixQuotes(txtleavingreason.Text.ToUpper().Trim());

            bool bresult = SaitexBL.Interface.Method.EMPLOYEE_EXP_DETAIL.InsertDate(oEMPLOYEE_EXP_DETAIL);
            if (bresult)
            {
                InitialControls();
                CommonFuction.ShowMessage(" Saved Successfully");
            }
            else
            {
                CommonFuction.ShowMessage("proablem in Saving");
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }

    private void DataBind(string EMPcode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.EMPLOYEE_EXP_DETAIL.DataBind(EMPcode);

            if (dt != null && dt.Rows.Count > 0)
            {
                EmpExpDataDind.DataSource = dt;
                EmpExpDataDind.DataBind();
            }
            else
            {
                //EmpExpDataDind.DataSource = null;
                //EmpExpDataDind.DataBind();
                //Common.CommonFuction.ShowMessage("Data Not Available");
                //EmpExpDataDind.Visible = true;
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            SavaData();
            InitialControls();
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
            InitialControls();
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    //protected void ddlempcode_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}

    private void Employee_Code(string EMP_CODE)
    {
        try
        {
            DataTable DTable = SaitexBL.Interface.Method.EMPLOYEE_EXP_DETAIL.Employee_code(EMP_CODE);
            if (DTable != null && DTable.Rows.Count > 0)
            {
                txtempcode.Text = DTable.Rows[0]["EMP_CODE"].ToString();
                txtempName.Text = DTable.Rows[0]["EMPLOYEENAME"].ToString();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Data Available");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
