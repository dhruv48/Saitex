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


public partial class Module_HRMS_Controls_EmpMSTQuery : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Grid2.Visible = false;
            Grid3.Visible = false;
            Grid4.Visible = false;
            Grid5.Visible = false;
            Grid6.Visible = false;
            lblEmpCompInfo.Visible = false;
            lblEmpFamInd.Visible = false;
            lblEmpMed.Visible = false;
            lblEmpQual.Visible = false;
            lblEmpLang.Visible = false;
            bindGvEmployeeMaster();
        }
        
    }
    private void bindGvEmployeeMaster()
    {

        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmployeeMst();
            Grid1.DataSource = dt;
            Grid1.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpQual(string EmpCode)
    {
       try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpQual(EmpCode);
            Grid4.DataSource = dt;
            Grid4.DataBind();
         }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpLang(string EmpCode)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpLang(EmpCode);
            Grid6.DataSource = dt;
            Grid6.DataBind();
        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpFamInd(string EmpCode)
    {

        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpFamilyInd(EmpCode);
            Grid5.DataSource = dt;
            Grid5.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpMedDTL(string EmpCode)
    {

        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpMedDTL(EmpCode);
            Grid2.DataSource = dt;
            Grid2.DataBind();

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void bindGvEmpCompInfo(string EmpCode)
    {

        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.EmployeeMaster.EmpCompInfo(EmpCode);
            Grid3.DataSource = dt;
            Grid3.DataBind();

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
    protected void Grid1_Select(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        ArrayList ar = Grid1.SelectedRecords;
        Hashtable ht = (Hashtable)ar[0];
        ViewState["EMP_CODE"] = ht["EMP_CODE"].ToString().Trim();
        string EmpCode = ViewState["EMP_CODE"].ToString();
        lblEmpQual.Visible = true;
        bindGvEmpQual(EmpCode);
        lblEmpLang.Visible = true;
        bindGvEmpLang(EmpCode);
        lblEmpFamInd.Visible = true;
        bindGvEmpFamInd(EmpCode);
        lblEmpMed.Visible = true;
        bindGvEmpMedDTL(EmpCode);
        lblEmpCompInfo.Visible = true;
        bindGvEmpCompInfo(EmpCode);
        Grid2.Visible = true;
        Grid3.Visible = true;
        Grid4.Visible = true;
        Grid5.Visible = true;
        Grid6.Visible = true;
    }
}
