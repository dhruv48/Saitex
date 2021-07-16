using System;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.OracleClient;
using System.Data;

public partial class Index : System.Web.UI.Page
{
    OracleCommand cmd;
    private static int Shift_ID;
    private static string CARD_NO = string.Empty;
    private static string Sift_Min_Work_Hours = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                TdLogin.Visible = true;
                TdWelcome.Visible = false;
                LblDate.Text = String.Format("{0:D}", DateTime.Today);
                TxtEmpCode.Focus();
                Session.Abandon();
            }
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString());
        }
    }
    protected bool ValidateUser(string LoginId, string Password)
    {
        OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        bool res = false;
        try
        {
            StringBuilder Lsql = new StringBuilder();
            Lsql.Append("SELECT U.USER_CODE,U.USER_NAME,U.USER_TYPE,D.DEPT_NAME,ltrim(rtrim(E.CARD_NO)) AS CARD_NO,S.* FROM CM_USER_MST U");
            Lsql.Append(" LEFT JOIN HR_EMP_MST E ON E.EMP_CODE=U.USER_CODE");
            Lsql.Append(" LEFT OUTER JOIN HR_SFT_MST S ON S.SFT_ID=E.SFT_ID");
            Lsql.Append(" LEFT OUTER JOIN CM_DEPT_MST D ON D.DEPT_CODE=E.DEPT_CODE");
            Lsql.Append(" WHERE ltrim(rtrim(U.USER_LOG_ID))=:USER_LOG_ID ");
            Lsql.Append(" AND ltrim(rtrim(U.USER_PASS))=:USER_PASS AND ltrim(rtrim(U.STATUS))='1'");
            cmd = new OracleCommand(Lsql.ToString(), con);
            cmd.Parameters.AddWithValue(":USER_LOG_ID", LoginId);
            cmd.Parameters.AddWithValue(":USER_PASS", Password);
            cmd.CommandType = CommandType.Text;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            OracleDataReader odr = cmd.ExecuteReader();
            DataTable DTable = new DataTable();
            DTable.Load(odr);
            if (DTable.Rows.Count > 0)
            {
                LblCode.Text = DTable.Rows[0]["USER_CODE"].ToString();
                CARD_NO = DTable.Rows[0]["CARD_NO"].ToString();
                Shift_ID = int.Parse(DTable.Rows[0]["SFT_ID"].ToString());
                Sift_Min_Work_Hours = String.Format("{0:T}", DateTime.Parse(DTable.Rows[0]["SFT_MIN_WRK_HOUR"].ToString()));
                LblUserName.Text = DTable.Rows[0]["USER_NAME"].ToString();
                LblOutTime.Text = DTable.Rows[0]["SFT_OUT_TIME"].ToString();
                LblShift.Text = DTable.Rows[0]["SFT_NAME"].ToString();
                LblStartTime.Text = DTable.Rows[0]["SFT_IN_TIME"].ToString();
                LblDept.Text = DTable.Rows[0]["DEPT_NAME"].ToString();
                LblTime.Text = String.Format("{0:T}", DateTime.Now.ToString("HH:mm"));
                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }

        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString());
            return res;
        }
        finally
        {
            if (con != null)
            {
                con.Close();
                con.Dispose();
                con = null;
            }

        }
    }
    public static void ShowMessage(string Message)
    {

        Page page = HttpContext.Current.CurrentHandler as Page;
        ScriptManager.RegisterStartupScript(page, page.GetType(), "alertmsg", "window.alert('" + Message + "');", true);
    }
    protected bool Insert_Punch()
    {
        OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        bool res = false;
        decimal RESULT = 0;
        try
        {
            cmd = new OracleCommand("P_EMP_PUNCH_INFO", con);

            cmd.Parameters.AddWithValue("P_EMP_CODE", LblCode.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("P_ATTN_DATE", System.DateTime.Now);
            cmd.Parameters.AddWithValue("P_IN_TIME", LblTime.Text.ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            RESULT = cmd.ExecuteNonQuery();
            if (RESULT > 0)
            {

                if (Insert_Attendance())
                { res = true; }
                else { res = false; }
            }
            else
            {
                res = false;
            }
            return res;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString());
            return res;
        }
    }
    protected bool Insert_Attendance()
    {
        OracleConnection con = new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["csTextile"].ToString());
        bool res = false;
        decimal RESULT = 0;
        string OT = string.Empty;
        string Early = string.Empty;
        try
        {
            cmd = new OracleCommand("P_HR_ATTN_TRN_INSERT_BY_User", con);
            cmd.Parameters.AddWithValue("p_SFT_ID", Shift_ID);
            cmd.Parameters.AddWithValue("P_EMP_CODE", LblCode.Text.Trim().ToString());
            cmd.Parameters.AddWithValue("p_CARD_NO", CARD_NO);
            cmd.Parameters.AddWithValue("P_ATTN_DATE", System.DateTime.Now.Date);
            cmd.Parameters.AddWithValue("p_IN_TIME", System.DateTime.Now);
            cmd.Parameters.AddWithValue("p_OUT_TIME", System.DateTime.Now);
            cmd.Parameters.AddWithValue("p_ENTRY_TYPE", "0");
            cmd.Parameters.AddWithValue("p_TUSER", LblCode.Text.Trim().ToString());
            cmd.CommandType = CommandType.StoredProcedure;
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            RESULT = cmd.ExecuteNonQuery();
            if (RESULT > 0)
            {

                res = true;
            }
            else
            {
                res = false;
            }
            return res;
        }
        catch (Exception ex)
        {
            ShowMessage(ex.Message.ToString());
            return res;
        }
    }
    protected void TxtPassword_TextChanged(object sender, EventArgs e)
    {
        if (ValidateUser(TxtEmpCode.Text.Trim().ToString(), TxtPassword.Text.Trim().ToString()))
        {
            if (Insert_Punch())
            {
                TdWelcome.Visible = true;
                Clear_Control();
            }
            else
            {
                ShowMessage("Please try again");
                Clear_Control();
            }
        }
        else
        {
            ShowMessage("UserId/Password  is Wrong!Please try again");
            Clear_Control();
            TdLogin.Visible = true;
            TdWelcome.Visible = false;
        }
    }
    protected void Clear_Control()
    {

        try
        {
            TxtEmpCode.Text = "";
            TxtPassword.Text = "";
            TxtEmpCode.Focus();
        }
        catch
        {
            throw;
        }
    }
}