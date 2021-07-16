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
using errorLog;
using Common;
using DBLibrary;

public partial class Module_HRMS_Pages_GridEmployeeDisplaySalary : System.Web.UI.Page
{
    csSaitex obj = null;
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader dr = null;
    DataSet ds = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["EmpCode"] != null)
        {
            
            
        if (!IsPostBack)
        {
            tdGridView.Visible = false;
            tdTotal.Visible = false;
            fillYear();
           bindgvSalaryDisplay();
            
        }
        }
        else
        {
            Response.Redirect("/Saitex/Module/HRMS/Default.aspx", false);
        }

    }
    private void bindgvSalaryDisplay()
    {
        try
        {
            string strBindCompany = "SELECT ltrim(rtrim(SAL_SLIP_MST_ID)) SAL_SLIP_MST_ID,slp.EMP_CODE,SAL_YEAR,";
            //--DATENAME(month, (SAL_YEAR+'-'+SAL_MONTH+'-01')) AS vc_Month1,
            strBindCompany = strBindCompany + " SAL_MONTH,SAL_WORKING_DAY,HLD,PAID_DAY,NET_SAL,EPF,ERN_AMT, LOAN_AMT,DEDCT_AMT,CASUAL_LV,SICK_LV,EARN_LV, MATERNITY_LV,PATERNITY_LV,COMPENSATORY_LV,LV_WITHOUT_PAY FROM HR_SAL_SLIP_MST slp,HR_EMP_MST em where slp.EMP_CODE=em.EMP_CODE and ltrim(rtrim(slp.DEL_STATUS))='0' and ltrim(rtrim(em.DEL_STATUS))='0' and ltrim(rtrim(slp.EMP_CODE))='" + Session["EmpCode"].ToString().Trim() + "' and ltrim(rtrim(LOCK_LV))='Lock'";

            if (ddlMonth.SelectedValue.Trim() != "")
            {
                strBindCompany = strBindCompany + " and ltrim(rtrim(slp.SAL_MONTH))='" + ddlMonth.SelectedValue.Trim() + "'";
            }
            if (ddlYear.SelectedValue.Trim() != "")
            {
                strBindCompany = strBindCompany + " and ltrim(rtrim(slp.SAL_YEAR))='" + ddlYear.SelectedValue.Trim() + "'";
            }
            strBindCompany = strBindCompany + " order by to_number(SAL_YEAR) desc,to_number(SAL_MONTH) asc";
            obj = new csSaitex();
            ds = obj.getDataSet(strBindCompany, CommandType.Text);
            gvSalaryDisplay.DataSource = ds;
            gvSalaryDisplay.DataBind();
            lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString().Trim();

            if (ddlMonth.SelectedValue.Trim() != "" && ddlYear.SelectedValue.Trim() != "")
            {
                if (ds.Tables[0].Rows.Count < 1)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('Salry for selected month and year not generated.');", true);
                    tdGridView.Visible = false;
                    tdTotal.Visible = false;
                }
                else if (ds.Tables[0].Rows.Count>0)
                {
                    tdGridView.Visible = true;
                    tdTotal.Visible = true;
                }

            }
            
        
        }
        catch (OracleException ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }
        }
    }
  
  
    private void fillYear()
    {
        for (int i = -15; i < 15; i++)
        {
            ddlYear.Items.Add(new ListItem(Convert.ToString((System.DateTime.Now.Year + i)), Convert.ToString((System.DateTime.Now.Year + i))));
        }
        ddlYear.Items.Insert(0, new ListItem("---Select---", ""));
        ddlYear.SelectedValue = System.DateTime.Now.Year.ToString().Trim();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        tdGridView.Visible = true;
        bindgvSalaryDisplay();
    }
    protected void gvSalaryDisplay_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "salaryPrint")
        {
                Response.Redirect("./PrintSalarySlip.aspx?SalaryId=" + e.CommandArgument.ToString().Trim(), false);
        }
    }
    protected void btnLock_Click(object sender, EventArgs e)
    {
        try
        {
            OracleConnection
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "P_SAL_SLIP_LOCKING_UPDATE";
            cmd.CommandType = CommandType.StoredProcedure;

            param = new OracleParameter("P_SAL_MONTH", OracleType.VarChar, 3);
            param.Value = ddlMonth.SelectedValue.ToString().Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_SAL_YEAR", OracleType.VarChar, 5);
            param.Value = ddlYear.SelectedValue.ToString().Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_LOCK_LV", OracleType.VarChar, 5);
            param.Value = "Lock";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            cmd.ExecuteNonQuery();
        }
        catch (OracleException ex)
        {


            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {

            ErrHandler.WriteError(ex.Message);
        }

        finally
        {
            if (obj != null)
            {
                obj = null;
            }

            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

            if (cmd != null)
            {
                cmd.Dispose();
                cmd = null;
            }
            if (param != null)
            {
                param = null;
            }
        }
    }

    private int CheckingLockingStatus(string TableId)
    {
        int result = 0;
        try
        {
            string strLockSataus = "SELECT ltrim(rtrim(SAL_SLIP_MST_ID)) ,LOCK_LV FROM HR_SAL_SLIP_MST where ltrim(rtrim(SAL_SLIP_MST_ID))='" + TableId.Trim() + "' and LOCK_LV='Lock' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            dr = obj.getDataReader(strLockSataus, CommandType.Text);
            if (dr.Read() == true)
            {
                result = 1;
            }
            dr.Close();
            dr.Dispose();
            dr = null;
        }
        catch (OracleException ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }
        finally
        {
            if (obj != null)
            {
                obj = null;
            }

        }
        return result;
    }
}
