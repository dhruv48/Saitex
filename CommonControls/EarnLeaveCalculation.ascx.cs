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
using DBLibrary;
using Common;
using errorLog;
using System.IO;

public partial class CommonControls_EarnLeaveCalculation : System.Web.UI.UserControl
{
    csSaitex obj = null;
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader  dr = null;
    
    DataSet ds = null;
    DataSet ds2 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            getEmployeeid();
        }
        if (Convert.ToInt16(Session["saveStatus"]) == 1)
        {
            if (Request.QueryString["cId"].ToString().Trim() == "S")
            {
                lblMessage.Text = "Record Saved successfully";
            }
        }

    }
    private void getEmployeeid()
    {
        try
        {
            string stremployeeid = "";
            stremployeeid = " SELECT CH_EMPLOYEEMASTERID,VC_CARDNUMBER,CH_GENDER FROM TBLEMPLOYEEMASTER order by CH_EMPLOYEEMASTERID";
            obj = new csSaitex();
            ds2 = obj.getDataSet(stremployeeid, CommandType.Text);
            foreach (DataRow dr1 in ds2.Tables[0].Rows)
            {
                AssignedSelectiveLeave(dr1["VC_CARDNUMBER"].ToString().Trim(), dr1["ch_EmployeeMasterId"].ToString().Trim());
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

    private void AssignedSelectiveLeave(string CardNumber, string EmployeeId)
    {
        try
        {
            string forward = "";
            string GetLeave = "";
            GetLeave = " SELECT  CH_EMPLOYEEMASTERID,VC_CURRENTYEAR";
            GetLeave = GetLeave + " IN_LEAVEMASTERID,case VC_LEAVEID when '1' then 'Casual' when '2' then 'Earn' when '3' then 'Maternity' when '4' then 'Partanity' when '5' then 'Sick' when '6' then 'Without Pay'  when '7' then 'Compensatory' else 'Not-Define' end vc_LeaveName,";
            GetLeave = GetLeave + " case VC_LEAVEID when '1' then 'CL' when '2' then 'EL' when '3' then 'ML' when '4' then 'PL' when '5' then 'SL' when '6' then 'LWP'  when '7' then 'CM' else 'Not-Define' end vc_LeaveCode,";
            GetLeave = GetLeave + " VC_LEAVEDAYS,DT_CREATED,DT_UPDATED ";
            GetLeave = GetLeave + " FROM TBLEMPLOYEELEAVE where ltrim(rtrim(CH_EMPLOYEEMASTERID))='" + EmployeeId.Trim() + "'";

            obj = new csSaitex();
            ds = obj.getDataSet(GetLeave, CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    forward = "";
                    if (rw["vc_LeaveCode"].ToString().Trim() == "CL")
                    {
                        forward = CalsualLeave();
                    }
                    if (rw["vc_LeaveCode"].ToString().Trim() == "EL")
                    {
                        calculateEarnLeave(CardNumber.ToString().Trim(), EmployeeId.ToString().Trim(), Convert.ToDateTime(rw["dt_Created"].ToString().Trim()));

                        forward = "";
                    }
                    if (rw["vc_LeaveCode"].ToString().Trim() == "SL")
                    {
                        forward = SickLeave();
                    }
                    if (rw["vc_LeaveCode"].ToString().Trim() == "ML")
                    {
                        forward = MaternityLeave();
                    }
                    if (rw["vc_LeaveCode"].ToString().Trim() == "PL")
                    {
                        forward = PaternityLeave();
                    }
                    if (rw["vc_LeaveCode"].ToString().Trim() == "CM")
                    {
                        forward = CompensatoryLeave();
                    }
                    if (rw["vc_LeaveCode"].ToString().Trim() == "LWP")
                    {
                        forward = "";
                    }
                    if (forward.ToString().Trim() != "")
                    {
                        SaveRecord_AssignLeaveMaster(rw["vc_CurrentYear"].ToString().Trim(), "", EmployeeId, "", rw["vc_LeaveCode"].ToString().Trim(), rw["vc_LeaveDays"].ToString().Trim(), System.DateTime.Now.ToString().Trim(), System.DateTime.Now.ToString().Trim(), forward.ToString().Trim(), CardNumber);
                    }
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
            if (ds != null)
            {
                ds = null;
            }
        }
    }
    private string CalsualLeave()
    {
        string forward = "";
        try
        {
            string strLeaveMaster = "SELECT IN_CASUALLEAVEMASTERID,ch_Year,vc_LeaveDay,ch_Forwared,ch_Status,dt_Created,dt_Updated FROM TBLCASUALLEAVEMASTER where ltrim(rtrim(ch_Year))='" + System.DateTime.Now.Year + "'";
            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);          
            if (dr.Read() == true)
            {
                forward = dr["ch_Forwared"].ToString().Trim();
            }
            if (dr != null)
            {
                dr.Close();
                dr.Dispose();
                dr = null;
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
        return forward;
    }
    private string SickLeave()
    {
        string forward = "";
        try
        {
            string strLeaveMaster = "SELECT in_SickLeaveMasterId,ch_Year,vc_LeaveDay,ch_PeriodType,ch_PeriodValue,ch_Forwared,ch_Status,dt_Created,dt_Updated FROM tblSickLeaveMaster where ltrim(rtrim(ch_Year))='" + System.DateTime.Now.Year + "'";
        

            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["ch_Forwared"].ToString().Trim();
            }
            if (dr != null)
            {
                dr.Close();
                dr.Dispose();
                dr = null;
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
        return forward;
    }
    private string MaternityLeave()
    {
        string forward = "";
        try
        {

            string strLeaveMaster = "SELECT in_MaternityLeaveMasterId,ch_Year,vc_LeaveDay,ch_PeriodType,ch_PeriodValue,ch_Forwared,ch_Status,dt_Created,dt_Updated FROM tblMaternityLeaveMaster where ltrim(rtrim(ch_Year))='" + System.DateTime.Now.Year + "'";
           

            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["ch_Forwared"].ToString().Trim();

            }
            if (dr != null)
            {
                dr.Close();
                dr.Dispose();
                dr = null;
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
        return forward;
    }
    private string PaternityLeave()
    {
        string forward = "";
        try
        {
            

            string strLeaveMaster = "SELECT in_PaternityLeaveMasterId,ch_Year,vc_LeaveDay,ch_PeriodType,ch_PeriodValue,ch_Forwared,ch_Status,dt_Created,dt_Updated FROM tblPaternityLeaveMaster where ltrim(rtrim(ch_Year))='" + System.DateTime.Now.Year + "'";
            //LeaveCode = "PL";


            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["ch_Forwared"].ToString().Trim();

            }
            if (dr != null)
            {
                dr.Close();
                dr.Dispose();
                dr = null;
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
        return forward;
    }
    private string CompensatoryLeave()
    {
        string forward = "";
        try
        {
          
            string strLeaveMaster = "SELECT in_CompensatoryleaveMasterId,ch_Year,vc_LeaveDay,ch_Forwared,ch_Status,dt_Created,dt_Updated FROM tblCompensatoryleaveMaster where ltrim(rtrim(ch_Year))='" + System.DateTime.Now.Year + "'";
           // LeaveCode = "CM";
            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["ch_Forwared"].ToString().Trim();
            }
            if (dr != null)
            {
                dr.Close();
                dr.Dispose();
                dr = null;
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
        return forward;
    }
    private void calculateEarnLeave(string CardNumber, string EmployeeId, DateTime dt_start)
    {
        try
        {
            
            string strLeaveMaster = "SELECT IN_LEAVEMASTERID,CH_LEAVETYPE,VC_LEAVENAME,VC_LEAVEDAYS, ACTIVEYEAR FROM tblLeaveMaster where ltrim(rtrim(CH_LEAVETYPE))='E' and ltrim(rtrim(ACTIVEYEAR))='" + System.DateTime.Now.Year + "'";
            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {

                string strEarnLeave = "";

                strEarnLeave = " SELECT count(vc_CardNumber) total,year(vc_AttanceDate) Year1";
                strEarnLeave = strEarnLeave + " FROM TBLATTENDANCEMASTER ";
                strEarnLeave = strEarnLeave + " where ltrim(rtrim(vc_CardNumber))='" + CardNumber.Trim() + "' and convert(varchar(50),vc_AttanceDate,101)>='" + dt_start.ToString("MM/dd/yyyy").Trim() + "'  and ltrim(rtrim(year(vc_AttanceDate)))='" + dr["ActiveYear"].ToString().Trim() + "' group by year(vc_AttanceDate)";
                obj = new csSaitex ();
                OracleDataReader  dr1 = obj.getDataReader(strEarnLeave, CommandType.Text);
                if (dr1.Read() == true)
                {
                    long days1 = getPerviousYearDay(dr1["Year1"].ToString().Trim(), EmployeeId, "EL");
                    long days = (Convert.ToInt64(dr1["total"].ToString().Trim()) + days1) / Convert.ToInt64(dr["vc_LeaveDays"].ToString().Trim());
                    {
                        SaveRecord_AssignLeaveMaster(dr1["Year1"].ToString().Trim(), "", EmployeeId, "", "EL", days.ToString().Trim(), System.DateTime.Now.ToString().Trim(), System.DateTime.Now.ToString().Trim(), "Yes", CardNumber);
                    }

                }
                dr1.Close();
                dr1.Dispose();
                dr1 = null;
            }
            if (dr != null)
            {
                dr.Close();
                dr.Dispose();
                dr = null;
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

    private void SaveRecord(string ch_EmployeeMasterId, int bi_TotalDay, string dt_Created, string dt_Update)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            /////duplicacy check
            string strDup = "";
            int iRecordFound = 0;
            strDup = "SELECT ch_EarnLeaveCalculation,ch_EmployeeMasterId,bi_TotalDay,dt_Created,dt_Updated FROM tblEarnLeaveCalculation where ltrim(rtrim(ch_EmployeeMasterId))=:ch_EmployeeMasterId)  ";

            cmd = new OracleCommand(strDup, con);

            param = new OracleParameter(":ch_EmployeeMasterId", OracleType.Number);
            param.Value = ch_EmployeeMasterId.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            strDup = Convert.ToString(cmd.ExecuteOracleScalar());

            if (strDup != "")
            {
                iRecordFound = 1;
                lblMessage.Text = "This record already saved! Pls enter another";
                cmd.Dispose();
            }

            ////
            if (iRecordFound == 0)
            {
                string strMaxId = "";
                strMaxId = "select nvl(max(ch_EarnLeaveCalculation),0)+1  ch_EarnLeaveCalculation from tblEarnLeaveCalculation";
                obj = new csSaitex();
                strMaxId = obj.executeScalar(strMaxId, CommandType.Text);


                string strSQL = "";
                strSQL = "insert into tblEarnLeaveCalculation(CH_EARNLEAVECALCULATION,CH_EMPLOYEEMASTERID,BI_TOTALDAY,DT_CREATED,DT_UPDATED)";
                strSQL = strSQL + " values(:CH_EARNLEAVECALCULATION,:CH_EMPLOYEEMASTERID,:BI_TOTALDAY,:DT_CREATED,:DT_UPDATED)";
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;

                param = new OracleParameter(":CH_EARNLEAVECALCULATION", OracleType.Number);
                param.Direction = ParameterDirection.Input;
                param.Value = Convert.ToInt32(strMaxId);
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_EmployeeMasterId", OracleType.Number);
                param.Direction = ParameterDirection.Input;
                param.Value = ch_EmployeeMasterId.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":bi_TotalDay", OracleType.Number);
                param.Direction = ParameterDirection.Input;
                param.Value = bi_TotalDay;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":dt_Created", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = dt_Created.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":dt_Updated", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = dt_Update.Trim();
                cmd.Parameters.Add(param);

                int iRecordEffected = cmd.ExecuteNonQuery();

            }
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
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
    private void UpdateRecord(string ch_EarnLeaveCalculation, string ch_EmployeeMasterId, int bi_TotalDay, string dt_Update)
    {
        try
        {
            int iRecordFound = 0;
            if (iRecordFound == 0)
            {

                con = new OracleConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                con.Open();

                string strSQL = "";
                strSQL = "UPDATE tblEarnLeaveCalculation SET ch_EmployeeMasterId=:ch_EmployeeMasterId, bi_TotalDay=:bi_TotalDay,dt_Updated=:dt_Updated 	WHERE ltrim(rtrim(ch_EarnLeaveCalculation))=:ch_EarnLeaveCalculation ";

                cmd = new OracleCommand(strSQL, con);

                param = new OracleParameter(":ch_EarnLeaveCalculation", OracleType.Number);
                param.Value = ch_EarnLeaveCalculation.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_EmployeeMasterId", OracleType.Number);
                param.Value = ch_EmployeeMasterId.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":bi_TotalDay", OracleType.Number);
                param.Value = bi_TotalDay;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":dt_Updated", OracleType.DateTime);
                param.Value = dt_Update.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                int iRecordEffected = cmd.ExecuteNonQuery();

                if (iRecordEffected == 1)
                {

                }
            }
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
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
    private void SaveRecord_AssignLeaveMaster(string ch_Year, string ch_Month, string ch_EmployeeMasterId, string in_LeaveMasterId, string ch_LeaveType, string vc_Total_LeaveDay_InMonth, string dt_Created, string dt_Updated, string Forwared, string CardNumber)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
            int iRecordFound = 0;
            string checkYearChange = "SELECT ch_LeaveAssignedMasterId,ch_Year,ch_Month,ch_EmployeeMasterId,in_LeaveMasterId,ch_LeaveType,vc_Total_LeaveDay_InMonth,dt_Created,dt_Updated,ch_LeaveAvial,ch_CarryForwaredLeave,ch_DayLeftPreviousYear,ch_DayLeftPrevoisYear_Status FROM tblLeaveAssignedMaster where ltrim(rtrim(ch_Year))='" + ch_Year.Trim() + "' and ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(ch_LeaveType))='" + ch_LeaveType.Trim() + "' ";
            obj = new csSaitex();
            dr = obj.getDataReader(checkYearChange, CommandType.Text);
            if (dr.Read() == true)
            {
                iRecordFound = 0;
            }
            else
            {
                iRecordFound = 1;
                if (Forwared.Trim() == "Yes")
                {
                    getPerviousYearLeave(Convert.ToString(Convert.ToInt64(ch_Year) - 1), ch_Month, ch_EmployeeMasterId, in_LeaveMasterId, ch_LeaveType, vc_Total_LeaveDay_InMonth, dt_Created, dt_Updated, Forwared, CardNumber);
                }
                if (Forwared.Trim() == "No")
                {
                    iRecordFound = 0;
                }
            }
            dr.Close();
            dr.Dispose();
            dr = null;
            /////duplicacy check/////
            string strDup = "";
           
            strDup = "select ch_Year,ch_EmployeeMasterId,ch_LeaveType from tblLeaveAssignedMaster where ltrim(rtrim(ch_Year))=:ch_Year  and  ltrim(rtrim(ch_EmployeeMasterId))=:ch_EmployeeMasterId  and  ltrim(rtrim(ch_LeaveType))=:ch_LeaveType";

            cmd = new OracleCommand(strDup, con);

            param = new OracleParameter(":ch_Year", OracleType.Char, 4);
            param.Direction = ParameterDirection.Input;
            param.Value = ch_Year.Trim();
            cmd.Parameters.Add(param);

            param = new OracleParameter(":ch_EmployeeMasterId", OracleType.Number);
            param.Direction = ParameterDirection.Input;
            param.Value = ch_EmployeeMasterId.Trim();
            cmd.Parameters.Add(param);

            param = new OracleParameter(":ch_LeaveType", OracleType.Char, 3);
            param.Direction = ParameterDirection.Input;
            param.Value = ch_LeaveType.Trim();
            cmd.Parameters.Add(param);

            strDup = Convert.ToString(cmd.ExecuteOracleScalar());

            if (strDup != "")
            {
                iRecordFound = 1;
                lblMessage.Text = "This record already saved! Pls enter another";
                cmd.Dispose();
            }
            ///

            if (iRecordFound == 0)
            {
                string strMaxId = "";


                strMaxId = "select nvl(max(ch_LeaveAssignedMasterId),0)+1  ch_LeaveAssignedMasterId from tblLeaveAssignedMaster ";
                obj = new csSaitex();
                strMaxId = obj.executeScalar(strMaxId, CommandType.Text);

                string strSQL = "";
                strSQL = "insert into tblLeaveAssignedMaster (CH_LEAVEASSIGNEDMASTERID,CH_YEAR,CH_MONTH,CH_EMPLOYEEMASTERID,IN_LEAVEMASTERID,CH_LEAVETYPE,VC_TOTAL_LEAVEDAY_INMONTH,DT_CREATED,DT_UPDATED,CH_LEAVEAVIAL,CH_CARRYFORWAREDLEAVE,CH_DAYLEFTPREVIOUSYEAR,CH_DAYLEFTPREVOISYEAR_STATUS)";
                strSQL = strSQL + " values(:CH_LEAVEASSIGNEDMASTERID,:CH_YEAR,:CH_MONTH,:CH_EMPLOYEEMASTERID,:IN_LEAVEMASTERID,:CH_LEAVETYPE,:VC_TOTAL_LEAVEDAY_INMONTH,:DT_CREATED,:DT_UPDATED,:CH_LEAVEAVIAL,:CH_CARRYFORWAREDLEAVE,:CH_DAYLEFTPREVIOUSYEAR,:CH_DAYLEFTPREVOISYEAR_STATUS)";
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;

                cmd = new OracleCommand(strSQL, con);


                param = new OracleParameter(":CH_LEAVEASSIGNEDMASTERID", OracleType.Char, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = Convert.ToInt32(strMaxId);
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_Year", OracleType.Char, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = ch_Year.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_Month", OracleType.Char, 2);
                param.Direction = ParameterDirection.Input;
                param.Value = ch_Month.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_EmployeeMasterId", OracleType.Number);
                param.Direction = ParameterDirection.Input;
                param.Value = ch_EmployeeMasterId.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":in_LeaveMasterId", OracleType.Number);
                param.Direction = ParameterDirection.Input;
                param.Value = in_LeaveMasterId;
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_LeaveType", OracleType.Char, 3);
                param.Direction = ParameterDirection.Input;
                param.Value = ch_LeaveType.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":vc_Total_LeaveDay_InMonth", OracleType.VarChar, 3);
                param.Direction = ParameterDirection.Input;
                param.Value = vc_Total_LeaveDay_InMonth.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":dt_Created", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = dt_Created.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":dt_Updated", OracleType.DateTime);
                param.Direction = ParameterDirection.Input;
                param.Value = dt_Updated.Trim();
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_LeaveAvial", OracleType.Char, 3);
                param.Direction = ParameterDirection.Input;
                param.Value = "0";
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_CarryForwaredLeave", OracleType.Char, 4);
                param.Direction = ParameterDirection.Input;
                param.Value = "0";
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_DayLeftPreviousYear", OracleType.Char, 3);
                param.Direction = ParameterDirection.Input;
                param.Value = "0";
                cmd.Parameters.Add(param);

                param = new OracleParameter(":ch_DayLeftPrevoisYear_Status", OracleType.Char, 1);
                param.Direction = ParameterDirection.Input;
                param.Value = "2";
                cmd.Parameters.Add(param);

                int iRecordEffected = cmd.ExecuteNonQuery();

            }
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
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
    private void getPerviousYearLeave(string ch_Year, string ch_Month, string ch_EmployeeMasterId, string in_LeaveMasterId, string ch_LeaveType, string vc_Total_LeaveDay_InMonth, string dt_Created, string dt_Updated, string Forwared, string CardNumber)
    {
        try
        {
            long days = 0;
            string checkYearChange = "SELECT ch_LeaveAssignedMasterId,ch_Year,ch_Month,ch_EmployeeMasterId,in_LeaveMasterId,ch_LeaveType,vc_Total_LeaveDay_InMonth,dt_Created,dt_Updated,ch_LeaveAvial,ch_CarryForwaredLeave,ch_DayLeftPreviousYear,ch_DayLeftPrevoisYear_Status FROM tblLeaveAssignedMaster where ltrim(rtrim(ch_Year))='" + ch_Year.Trim() + "' and ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(ch_LeaveType))='" + ch_LeaveType.Trim() + "' ";
            obj = new csSaitex();
            dr = obj.getDataReader(checkYearChange, CommandType.Text);
            if (dr.Read() == true)
            {
                if (ch_LeaveType.Trim() != "EL")
                {
                    LeaveRecord_AddCarryForward(Convert.ToString(Convert.ToInt64(ch_Year) + 1), "0", ch_EmployeeMasterId, in_LeaveMasterId, ch_LeaveType, Convert.ToString(Convert.ToUInt64(dr["vc_Total_LeaveDay_InMonth"].ToString().Trim()) - Convert.ToUInt64(dr["ch_LeaveAvial"].ToString().Trim()) + Convert.ToUInt64(vc_Total_LeaveDay_InMonth)), dt_Created, dt_Updated, Convert.ToString(Convert.ToUInt64(dr["vc_Total_LeaveDay_InMonth"].ToString().Trim()) - Convert.ToUInt64(dr["ch_LeaveAvial"].ToString().Trim())), "0", "1");
                }
                else
                {
                    string strEarnLeave = "";

                    strEarnLeave = " SELECT count(vc_CardNumber) total,year(vc_AttanceDate) Year1";
                    strEarnLeave = strEarnLeave + " FROM tblAttandanceMasterDeatil ";
                    strEarnLeave = strEarnLeave + " where ltrim(rtrim(vc_CardNumber))='" + CardNumber.Trim() + "' and ltrim(rtrim(year(vc_AttanceDate)))='" + dr["ActiveYear"].ToString().Trim() + "' group by year(vc_AttanceDate)";
                    obj = new csSaitex();
                    OracleDataReader  dr1 = obj.getDataReader(strEarnLeave, CommandType.Text);
                    if (dr1.Read() == true)
                    {
                        days = Convert.ToInt32(dr1["total"].ToString().Trim()) % Convert.ToInt32(dr["vc_LeaveDays"].ToString().Trim());

                    }
                    dr1.Close();
                    dr1.Dispose();
                    dr1 = null;


                    LeaveRecord_AddCarryForward(Convert.ToString(Convert.ToInt64(ch_Year) + 1), "0", ch_EmployeeMasterId, in_LeaveMasterId, ch_LeaveType, Convert.ToString(Convert.ToUInt64(dr["vc_Total_LeaveDay_InMonth"].ToString().Trim()) - Convert.ToUInt64(dr["ch_LeaveAvial"].ToString().Trim()) + Convert.ToUInt64(vc_Total_LeaveDay_InMonth)), dt_Created, dt_Updated, Convert.ToString(Convert.ToUInt64(dr["vc_Total_LeaveDay_InMonth"].ToString().Trim()) - Convert.ToUInt64(dr["ch_LeaveAvial"].ToString().Trim())), days.ToString().Trim(), "1");

                }

            }
            else
            {
                LeaveRecord_AddCarryForward(Convert.ToString(Convert.ToInt64(ch_Year) + 1), ch_Month, ch_EmployeeMasterId, in_LeaveMasterId, ch_LeaveType, vc_Total_LeaveDay_InMonth, dt_Created, dt_Updated, "0", "0", "2");

            }
        }
        catch (OracleException  ex)
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
    private long getPerviousYearDay(string ch_Year, string ch_EmployeeMasterId, string ch_LeaveType)
    {
        long days = 0;
        try
        {

            string checkYearChange = "SELECT ch_LeaveAssignedMasterId,ch_Year,ch_Month,ch_EmployeeMasterId,in_LeaveMasterId,ch_LeaveType,vc_Total_LeaveDay_InMonth,dt_Created,dt_Updated,ch_LeaveAvial,ch_CarryForwaredLeave,ch_DayLeftPreviousYear,ch_DayLeftPrevoisYear_Status FROM tblLeaveAssignedMaster where ltrim(rtrim(ch_Year))='" + ch_Year.Trim() + "' and ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(ch_LeaveType))='" + ch_LeaveType.Trim() + "' ";
            obj = new csSaitex();
            OracleDataReader  dr = obj.getDataReader(checkYearChange, CommandType.Text);
            if (dr.Read() == true)
            {
                days = Convert.ToInt64(dr["ch_DayLeftPreviousYear"].ToString().Trim());
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
        return days;
    }
    private void LeaveRecord_AddCarryForward(string ch_Year, string ch_Month, string ch_EmployeeMasterId, string in_LeaveMasterId, string ch_LeaveType, string vc_Total_LeaveDay_InMonth, string dt_Created, string dt_Updated, string ch_CarryForwaredLeave, string ch_DayLeftPreviousYear, string ch_DayLeftPrevoisYear_Status)
    {
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        /////duplicacy check/////
        string strDup = "";
        int iRecordFound = 0;
        strDup = "select * from tblLeaveAssignedMaster where ltrim(rtrim(ch_Year))=:ch_Year  and  ltrim(rtrim(ch_EmployeeMasterId))=:ch_EmployeeMasterId  and  ltrim(rtrim(ch_LeaveType))=:ch_LeaveType)";

        cmd = new OracleCommand(strDup, con);

        param = new OracleParameter(":ch_Year", OracleType.Char, 4);
        param.Direction = ParameterDirection.Input;
        param.Value = ch_Year.Trim();
        cmd.Parameters.Add(param);

        param = new OracleParameter(":ch_EmployeeMasterId", OracleType.Number);
        param.Direction = ParameterDirection.Input;
        param.Value = ch_EmployeeMasterId.Trim();
        cmd.Parameters.Add(param);

        param = new OracleParameter(":ch_LeaveType", OracleType.Char, 3);
        param.Direction = ParameterDirection.Input;
        param.Value = ch_LeaveType.Trim();
        cmd.Parameters.Add(param);

        strDup = Convert.ToString(cmd.ExecuteOracleScalar());

        if (strDup != "")
        {
            iRecordFound = 1;
            lblMessage.Text = "This record already saved! Pls enter another";
            cmd.Dispose();
        }
        ///

        if (iRecordFound == 0)
        {
            string strMaxId = "";
            strMaxId = "select nvl(max(ch_LeaveAssignedMasterId),0)+1  ch_LeaveAssignedMasterId from tblLeaveAssignedMaster";
            obj = new csSaitex();
            strMaxId = obj.executeScalar(strMaxId, CommandType.Text);

            string strSQL = "";
            strSQL = "insert into tblLeaveAssignedMaster(CH_LEAVEASSIGNEDMASTERID,CH_YEAR,CH_MONTH,CH_EMPLOYEEMASTERID,IN_LEAVEMASTERID,CH_LEAVETYPE,VC_TOTAL_LEAVEDAY_INMONTH,DT_CREATED,DT_UPDATED,CH_LEAVEAVIAL,CH_CARRYFORWAREDLEAVE,CH_DAYLEFTPREVIOUSYEAR,CH_DAYLEFTPREVOISYEAR_STATUS)";
            strSQL = strSQL + " values(:CH_LEAVEASSIGNEDMASTERID,:CH_YEAR,:CH_MONTH,:CH_EMPLOYEEMASTERID,:IN_LEAVEMASTERID,:CH_LEAVETYPE,:VC_TOTAL_LEAVEDAY_INMONTH,:DT_CREATED,:DT_UPDATED,:CH_LEAVEAVIAL,:CH_CARRYFORWAREDLEAVE,:CH_DAYLEFTPREVIOUSYEAR,:CH_DAYLEFTPREVOISYEAR_STATUS)";
            cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;

            param = new OracleParameter(":CH_LEAVEASSIGNEDMASTERID", OracleType.Number);
            param.Direction = ParameterDirection.Input;
            param.Value = Convert.ToInt32(strMaxId);
            cmd.Parameters.Add(param);


            param = new OracleParameter("@ch_Year", OracleType.Char, 4);
            param.Value = Convert.ToString(Convert.ToInt64(ch_Year));
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_Month", OracleType.Char, 2);
            param.Value = ch_Month.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_EmployeeMasterId", OracleType.Number);
            param.Value = ch_EmployeeMasterId.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@in_LeaveMasterId", OracleType.Number);
            param.Value = in_LeaveMasterId;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_LeaveType", OracleType.Char, 3);
            param.Value = ch_LeaveType.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@vc_Total_LeaveDay_InMonth", OracleType.VarChar, 3);
            param.Value = vc_Total_LeaveDay_InMonth.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@dt_Created", OracleType.DateTime);
            param.Value = dt_Created.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@dt_Updated", OracleType.DateTime);
            param.Value = dt_Updated.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_LeaveAvial", OracleType.Char, 3);
            param.Value = "0";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_CarryForwaredLeave", OracleType.Char, 4);
            param.Value = ch_CarryForwaredLeave.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_DayLeftPreviousYear", OracleType.Char, 3);
            param.Value = ch_DayLeftPreviousYear.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("@ch_DayLeftPrevoisYear_Status", OracleType.Char, 1);
            param.Value = ch_DayLeftPrevoisYear_Status.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            int iRecordEffected = cmd.ExecuteNonQuery();
        }



    }
}
