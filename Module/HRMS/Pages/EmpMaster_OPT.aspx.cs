using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using Obout.ComboBox;
using DBLibrary;
using errorLog;
using System.IO;
using Common;
public partial class HRMS_EmpMaster_OPT : System.Web.UI.Page
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    //OracleDataReader dr = null;
    //OracleParameter param = null;
    //csSaitex obj = null;
    OracleDataAdapter da = null;
    DataSet ds = null;
    protected void Page_Load(object sender, EventArgs e)
    {

    }
 
    protected void btnGetReport_Click1(object sender, EventArgs e)
    {
        string QueryString = "";
        bool flag = false;
        //if (txtCompanyCode.Text.Trim() != "")
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";
        //    QueryString = QueryString + "comCode=" + txtCompanyCode.Text;
        //    flag = true;
        //}

        //if (txtCompName.Text.Trim() != "")
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";
        //    QueryString = QueryString + "comName=" + txtCompName.Text.Trim();
        //    flag = true;
        //}


        if (ddlBranchName.SelectedValue.Trim() != "")
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "bncCode=" + ddlBranchName.SelectedValue.Trim();
            flag = true;
        }

        //if (txtBranchName.Text.Trim()!="")
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";
        //    QueryString = QueryString + "bncName=" + txtBranchName.Text.Trim();
        //    flag = true;
        //}

        //if (txtDepartmentCode.Text.Trim()!="")
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";
        //    QueryString = QueryString + "decCode=" + ddlDepartment.SelectedValue.Trim();
        //    flag = true;
        //}

        if (ddlDepartment.SelectedValue.Trim() != "")
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";
            QueryString = QueryString + "decName=" + ddlDepartment.SelectedValue.Trim();
            flag = true;
        }

        string URL = "../Reports/EmployeeMasterReport.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','top=2,left=2,toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=1000,height=800');", true);
    }

    protected void ddlBranchName_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
       
        
        // Getting the items
        DataTable data = GetItems(e.Text.ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);

        ddlBranchName.DataSource = data;
        ddlBranchName.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
    }

    // Gets all the countries that start with the typed text, taking paging into account
    //protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    protected DataTable GetItems(string strBranchName, int startOffset, int numberOfItems)
    {

        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string whereClause = " WHERE  BRANCH_NAME LIKE :BRANCH_NAME";
            string sortExpression = " ORDER BY BRANCH_NAME asc";

            string commandText = "SELECT  *  FROM CM_BRANCH_MST";
            commandText += whereClause;
            //if (startOffset != 0)
            //{
            //    commandText += " AND HLD_ID NOT IN (SELECT TOP " + startOffset + " HLD_ID FROM HR_HLD_MST";
            //    commandText += whereClause + sortExpression + ")";
            //}

            commandText += sortExpression;
            cmd = new OracleCommand(commandText, con);
            cmd.Parameters.Add(":BRANCH_NAME", OracleType.VarChar).Value = strBranchName + '%';

            da = new OracleDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            return dt;

        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = "";
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = "";
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        finally
        {
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

            if (da != null)
            {
                da = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }

    }

    // Gets the total number of items that start with the typed text
    protected int GetItemsCount(string text)
    {
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        cmd = new OracleCommand("SELECT COUNT(*) FROM CM_BRANCH_MST WHERE BRANCH_NAME LIKE :BRANCH_NAME", con);
        cmd.Parameters.Add(":BRANCH_NAME", OracleType.VarChar).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }



    protected void ddlDepartment_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        

        // Getting the items
        DataTable data = GetItemsDepartment(e.Text.ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);

        ddlDepartment.DataSource = data;
        ddlDepartment.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCountDepartment(e.Text);
    }

    // Gets all the countries that start with the typed text, taking paging into account
    //protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    protected DataTable GetItemsDepartment(string strDepartment, int startOffset, int numberOfItems)
    {

        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            string whereClause = " WHERE  DEPT_NAME LIKE :DEPT_NAME";
            string sortExpression = " ORDER BY DEPT_NAME asc";

            string commandText = "SELECT * FROM CM_DEPT_MST";
            commandText += whereClause;
            //if (startOffset != 0)
            //{
            //    commandText += " AND HLD_ID NOT IN (SELECT TOP " + startOffset + " HLD_ID FROM HR_HLD_MST";
            //    commandText += whereClause + sortExpression + ")";
            //}

            commandText += sortExpression;
            cmd = new OracleCommand(commandText, con);
            cmd.Parameters.Add(":DEPT_NAME", OracleType.VarChar).Value = strDepartment + '%';

            da = new OracleDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt = ds.Tables[0];
            return dt;

        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = "";
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = "";
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        finally
        {
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

            if (da != null)
            {
                da = null;
            }
            if (ds != null)
            {
                ds.Dispose();
                ds = null;
            }

        }

    }

    // Gets the total number of items that start with the typed text
    protected int GetItemsCountDepartment(string text)
    {
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        cmd = new OracleCommand("SELECT COUNT(*) FROM CM_DEPT_MST WHERE DEPT_NAME LIKE :DEPT_NAME", con);
        cmd.Parameters.Add(":DEPT_NAME", OracleType.VarChar).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

}
