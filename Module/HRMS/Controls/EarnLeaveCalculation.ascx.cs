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
using DBLibrary;
using Common;
using errorLog;

public partial class Module_HRMS_Controls_EarnLeaveCalculation : System.Web.UI.UserControl
{
    csSaitex obj = null;
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader dr = null;    
    DataSet ds = null;
    DataSet ds2 = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["EMP_CODE"] != null && Request.QueryString["GradeID"] != null)
            {
                string EMP_CODE = Request.QueryString["EMP_CODE"].ToString();
                getEmployeeid(EMP_CODE );
            }
        }
    }
    private void getEmployeeid(string EMP_CODE)
    {
        try
        {
            string stremployeeid = "";
            stremployeeid = "SELECT EMP_CODE,CARD_NO,GENDER FROM hr_emp_mst where ltrim(rtrim(del_status))='0' AND ltrim(rtrim(EMP_CODE))='"+EMP_CODE +"' order by EMP_ID asc";
            obj = new csSaitex();
            ds2 = obj.getDataSet(stremployeeid, CommandType.Text);
            foreach (DataRow dr1 in ds2.Tables[0].Rows)
            {
                AssignedSelectiveLeave(dr1["EMP_CODE"].ToString().Trim(), dr1["EMP_CODE"].ToString().Trim());
            }          
        
        }      
        
        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }       

    }
    private void AssignedSelectiveLeave(string CardNumber, string EMP_CODE)
    {
        try
        {
            string forward = "";
            string GetLeave = "";
            GetLeave = "SELECT LV_MST_ID, CUR_YEAR,EMP_CODE,LV_ID,case LV_ID when '1' then 'Casual' when '2' then 'Earn' when '3' then 'Sick' when '4' then 'Partanity' when '5' then 'Maternity' when '6' then 'Compensatory' when '7' then 'Leave Without Pay' else 'Not-Define' end vc_LeaveName, LV_DAYS,TDATE, to_char(TDATE,'dd') dd,to_char(TDATE,'mm') mm,to_char(TDATE,'yyyy') yyyy FROM HR_EMP_LV where ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            ds = obj.getDataSet(GetLeave, CommandType.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow rw in ds.Tables[0].Rows)
                {
                    forward = "";
                    if (rw["LV_ID"].ToString().Trim() == "1")
                    {
                        forward = CalsualLeave();
                    }
                    if (rw["LV_ID"].ToString().Trim() == "2")
                    {
                        calculateEarnLeave(CardNumber.ToString().Trim(), EMP_CODE.Trim(), Convert.ToInt16(rw["dd"].ToString().Trim()), Convert.ToInt16(rw["mm"].ToString().Trim()), Convert.ToInt16(rw["yyyy"].ToString().Trim()));
                        forward = "";
                    }
                    if (rw["LV_ID"].ToString().Trim() == "3")
                    {
                        forward = SickLeave();
                    }
                    if (rw["LV_ID"].ToString().Trim() == "4")
                    {
                        forward = PaternityLeave();
                    }
                    
                    if (rw["LV_ID"].ToString().Trim() == "5")
                    {
                        forward = MaternityLeave();
                    }
                    
                    if (rw["LV_ID"].ToString().Trim() == "6")
                    {
                        forward = CompensatoryLeave();
                    }
                    if (rw["LV_ID"].ToString().Trim() == "7")
                    {
                        forward = "";
                    }
                    if (forward.ToString().Trim() != "")
                    {
                        SaveRecord_AssignLeaveMaster(rw["CUR_YEAR"].ToString().Trim(), "", EMP_CODE, "", rw["LV_ID"].ToString().Trim(), rw["LV_DAYS"].ToString().Trim(), forward.ToString().Trim(), EMP_CODE.Trim());
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
            string strLeaveMaster = "SELECT LV_ID,YEAR,LV_DAY,LV_FRWD,STATUS,TDATE FROM hr_lv_trn where ltrim(rtrim(YEAR))='" + System.DateTime.Now.Year + "' and ltrim(rtrim(LV_ID))='1' and ltrim(rtrim(DEL_STATUS))='0'";
            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["LV_FRWD"].ToString().Trim();
            }
            
            dr.Close();
            dr.Dispose();
            dr = null;
            obj = null;
           
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
        return forward;
    }
    private string PaternityLeave()
    {
        string forward = "";
        try
        {
            string LeaveCode = "";

            string strLeaveMaster = "SELECT LV_ID,YEAR,LV_DAY,LV_FRWD,STATUS,TDATE FROM hr_lv_trn where ltrim(rtrim(YEAR))='" + System.DateTime.Now.Year + "' and ltrim(rtrim(LV_ID))='4' and ltrim(rtrim(DEL_STATUS))='0'";
            LeaveCode = "4";

            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["LV_FRWD"].ToString().Trim();

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
        return forward;
    }
    private string MaternityLeave()
    {
        string forward = "";
        try
        {

            string strLeaveMaster = "SELECT LV_ID,YEAR,LV_DAY,LV_FRWD,STATUS,TDATE FROM hr_lv_trn where ltrim(rtrim(YEAR))='" + System.DateTime.Now.Year + "' and ltrim(rtrim(LV_ID))='5' and ltrim(rtrim(DEL_STATUS))='0'";
            string LeaveCode = "5";

            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["LV_FRWD"].ToString().Trim();

            }
            
            dr.Close();
            dr.Dispose();
            dr = null;
            obj = null;
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
        return forward;
    }
    private string SickLeave()
    {
        string forward = "";
        try
        {
            string LeaveCode = "";
            string strLeaveMaster = "SELECT LV_ID,YEAR,LV_DAY,LV_FRWD,STATUS,TDATE FROM hr_lv_trn where ltrim(rtrim(YEAR))='" + System.DateTime.Now.Year + "' and ltrim(rtrim(LV_ID))='3' and ltrim(rtrim(DEL_STATUS))='0'";
            LeaveCode = "3";

            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["LV_FRWD"].ToString().Trim();
            }

            obj = null;
            dr.Close();
            dr.Dispose();
            dr = null;
            
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
        return forward;
    }
    private string CompensatoryLeave()
    {
        string forward = "";
        try
        {
            string LeaveCode = "";
            string strLeaveMaster = "SELECT LV_ID,YEAR,LV_DAY,LV_FRWD,STATUS,TDATE FROM hr_lv_trn where ltrim(rtrim(YEAR))='" + System.DateTime.Now.Year + "' and ltrim(rtrim(LV_ID))='6' and ltrim(rtrim(DEL_STATUS))='0'";
            LeaveCode = "6";
            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {
                forward = dr["LV_FRWD"].ToString().Trim();
            }

            obj = null;
            dr.Close();
            dr.Dispose();
            dr = null;
           
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
        return forward;
    }


    private void calculateEarnLeave(string CardNumber, string EMP_CODE, int dd, int mm, int yyyy)
    {
        try
        {
            int day = 0;
            string strLeaveMaster = "SELECT LV_ID,LV_DAY, YEAR FROM hr_Lv_trn where ltrim(rtrim(LV_ID))='2' and ltrim(rtrim(YEAR))='" + System.DateTime.Now.Year + "'";
            obj = new csSaitex();
            dr = obj.getDataReader(strLeaveMaster, CommandType.Text);
            if (dr.Read() == true)
            {

                string strEarnLeave = "";
                                
                strEarnLeave = "SELECT count(EMP_CODE) total,to_char(ATTN_DATE,'yyyy') Year1 FROM hr_attn_trn where ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and to_char(ATTN_DATE,'dd')>='" + dd + "' and to_char(ATTN_DATE,'mm')>='" + mm + "' and to_char(ATTN_DATE,'yyyy')>='" + yyyy + "' group by to_char(ATTN_DATE,'yyyy')";               
                obj = new csSaitex();
                OracleDataReader dr1 = obj.getDataReader(strEarnLeave, CommandType.Text);
                if (dr1.Read() == true)
                {
                    long days1 = getPerviousYearDay(dr1["Year1"].ToString().Trim(), EMP_CODE, "2");
                    long days = (Convert.ToInt64(dr1["total"].ToString().Trim()) + days1) / Convert.ToInt64(dr["LV_DAY"].ToString().Trim());
                    {
                        SaveRecord_AssignLeaveMaster(dr1["Year1"].ToString().Trim(), "", EMP_CODE.Trim(), "2", "2", days.ToString().Trim(), "Yes", EMP_CODE.Trim());
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
    # region No calling 
        private void SaveRecord(string EMP_CODE, int bi_TotalDay, string dt_Created, string dt_Update)
        {
        try
        {
            int iRecordFound = 0;
            if (iRecordFound == 0)
            {

                con = new OracleConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                con.Open();

                
                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "P_HR_EARN_LV_CAL_INSERT";
                cmd.CommandType = CommandType.StoredProcedure;

                param = new OracleParameter("@ch_EmployeeMasterId", OracleType.Char, 10);
                param.Value = EMP_CODE.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("@bi_TotalDay", OracleType.Number);
                param.Value = bi_TotalDay;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("@dt_Created", OracleType.DateTime);
                param.Value = dt_Created.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                param = new OracleParameter("@dt_Updated", OracleType.DateTime);
                param.Value = dt_Update.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                int iRecordEffected = cmd.ExecuteNonQuery();

                if (iRecordEffected == 1)
                {
                    // Session["saveStatus"] = 1;
                    // Response.Redirect("./BranchDetails.aspx?cId=" + 'S', false);
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

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "sp_ud_UpdateEarnLeaveCalculation";
                cmd.CommandType = CommandType.StoredProcedure;

                param = new OracleParameter("@ch_EarnLeaveCalculation", OracleType.Char, 12);
                param.Value = ch_EarnLeaveCalculation.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("@ch_EmployeeMasterId", OracleType.Char, 10);
                param.Value = ch_EmployeeMasterId.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("@bi_TotalDay", OracleType.Number);
                param.Value = bi_TotalDay;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("@dt_Updated", OracleType.DateTime);
                param.Value = dt_Update.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);


                int iRecordEffected = cmd.ExecuteNonQuery();

                if (iRecordEffected == 1)
                {
                    // Session["saveStatus"] = 1;
                    // Response.Redirect("./BranchDetails.aspx?cId=" + 'S', false);
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
    #endregion


        private void SaveRecord_AssignLeaveMaster(string LV_YEAR, string LV_MONTH, string EMP_CODE, string LV_TYPE, string LV_ID, string LV_DAYS_IN_MONTH, string Forwared, string CardNo)
        {
        try
        {
            int iRecordFound = 0;
            string checkYearChange = "SELECT LV_ASS_ID,LV_YEAR,LV_MONTH,EMP_CODE,LV_ID,LV_TYPE, LV_DAYS_IN_MONTH,TDATE,LV_AVL,CARRY_FRWD_LV,DAY_LEFT_PREV_YEAR, DAY_LEFT_PREV_YEAR_STATUS,TDATE FROM hr_lv_ass_mst where ltrim(rtrim(LV_YEAR))='" + LV_YEAR.Trim() + "' and ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(LV_ID))='" + LV_ID.Trim() + "'";
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
                    getPerviousYearLeave(Convert.ToString(Convert.ToInt64(LV_YEAR.Trim()) - 1), LV_MONTH.Trim(), EMP_CODE.Trim(), LV_ID.Trim(), LV_TYPE.Trim(), LV_DAYS_IN_MONTH.Trim(), Forwared.Trim(), EMP_CODE.Trim());
                }
                if (Forwared.Trim() == "No")
                {
                    iRecordFound = 0;
                }
            }
            dr.Close();
            dr.Dispose();
            dr = null;
            obj = null;
            if (iRecordFound == 0)
            {
                con = new OracleConnection();
                con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                con.Open();

                cmd = new OracleCommand();
                cmd.Connection = con;
                cmd.CommandText = "P_HR_LV_ASS_MST_INSERT";
                cmd.CommandType = CommandType.StoredProcedure;

                param = new OracleParameter("P_LV_YEAR", OracleType.Char, 4);
                param.Value = LV_YEAR.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_LV_MONTH", OracleType.Char, 2);
                param.Value = LV_MONTH.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_EMP_CODE", OracleType.Char, 10);
                param.Value = EMP_CODE.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_LV_ID", OracleType.Char, 3);
                param.Value = LV_ID.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_LV_TYPE", OracleType.Char, 3);
                param.Value = LV_ID.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_LV_DAYS_IN_MONTH", OracleType.VarChar, 3);
                param.Value = LV_DAYS_IN_MONTH.Trim();
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_LV_AVL", OracleType.Char, 3);
                param.Value = "0";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_CARRY_FRWD_LV", OracleType.Char, 4);
                param.Value = "0";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_DAY_LEFT_PREV_YEAR", OracleType.Char, 3);
                param.Value = "0";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_DAY_LEFT_PREV_YEAR_STATUS", OracleType.Char, 1);
                param.Value = "2";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_STATUS", OracleType.Char, 1);
                param.Value = "1";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
                
                param = new OracleParameter("P_DEL_STATUS", OracleType.Char, 1);
                param.Value = "0";
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);
                
                param = new OracleParameter("P_TDATE", OracleType.DateTime);
                param.Value = System.DateTime.Now;
                param.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(param);

                param = new OracleParameter("P_TUSER", OracleType.VarChar, 15);
                param.Value = "SA0001";
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
        private void getPerviousYearLeave(string LV_YEAR, string LV_MONTH, string EMP_CODE, string LV_ID, string LV_TYPE, string LV_DAYS_IN_MONTH, string CARRY_FRWD_LV, string CardNo)
    {
        try
        {
            long days = 0;
            string checkYearChange = "SELECT LV_ASS_ID,LV_YEAR,LV_MONTH,EMP_CODE,LV_ID,LV_TYPE, LV_DAYS_IN_MONTH,TDATE,LV_AVL,CARRY_FRWD_LV,DAY_LEFT_PREV_YEAR, DAY_LEFT_PREV_YEAR_STATUS FROM hr_lv_ass_mst where ltrim(rtrim(LV_YEAR))='" + LV_YEAR.Trim() + "' and ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(LV_TYPE))='" + LV_TYPE.Trim() + "'";
            obj = new csSaitex();
            dr = obj.getDataReader(checkYearChange, CommandType.Text);
            if (dr.Read() == true)
            {
                if (LV_TYPE.Trim() != "2")
                {
                    LeaveRecord_AddCarryForward(Convert.ToString(Convert.ToInt64(LV_YEAR) + 1), "0", EMP_CODE, LV_ID, LV_TYPE, Convert.ToString(Convert.ToInt64(dr["LV_DAYS_IN_MONTH"].ToString().Trim()) - Convert.ToInt64(dr["LV_AVL"].ToString().Trim()) + Convert.ToInt64(LV_DAYS_IN_MONTH)), Convert.ToString(Convert.ToInt64(dr["LV_DAYS_IN_MONTH"].ToString().Trim()) - Convert.ToInt64(dr["LV_AVL"].ToString().Trim())), "0", "1");
                }
                else
                {
                    string strEarnLeave = "";
                   
                    strEarnLeave = "SELECT count(EMP_CODE) total,to_char(ATTN_DATE,'yyyy') Year1 FROM  hr_Attn_trn where ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(year(ATTN_DATE)))='" + dr["YEAR"].ToString().Trim() + "' group by to_char(ATTN_DATE,'yyyy')";
                    obj = new csSaitex();
                    OracleDataReader dr1 = obj.getDataReader(strEarnLeave, CommandType.Text);
                    if (dr1.Read() == true)
                    {
                        days = Convert.ToInt32(dr1["total"].ToString().Trim()) % Convert.ToInt32(dr["LV_DAY"].ToString().Trim());

                    }
                    dr1.Close();
                    dr1.Dispose();
                    dr1 = null;


                    LeaveRecord_AddCarryForward(Convert.ToString(Convert.ToInt64(LV_YEAR) + 1), "0", EMP_CODE, LV_ID, LV_TYPE, Convert.ToString(Convert.ToInt64(dr["LV_DAYS_IN_MONTH"].ToString().Trim()) - Convert.ToInt64(dr["LV_AVL"].ToString().Trim()) + Convert.ToInt64(LV_DAYS_IN_MONTH)), Convert.ToString(Convert.ToInt64(dr["LV_DAYS_IN_MONTH"].ToString().Trim()) - Convert.ToInt64(dr["LV_AVL"].ToString().Trim())), days.ToString().Trim(), "1");

                }

            }
            else
            {
                LeaveRecord_AddCarryForward(Convert.ToString(Convert.ToInt64(LV_YEAR.Trim()) + 1), LV_MONTH.Trim(), EMP_CODE, LV_ID, LV_TYPE, LV_DAYS_IN_MONTH, "0", "0", "2");

            }
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
        }
    }
    private void LeaveRecord_AddCarryForward(string LV_YEAR, string LV_MONTH, string EMP_CODE, string LV_ID, string LV_TYPE, string LV_DAYS_IN_MONTH, string CARRY_FRWD_LV, string DAY_LEFT_PREV_YEAR, string DAY_LEFT_PREV_YEAR_STATUS)
    {
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();

            cmd = new OracleCommand();
            cmd.Connection = con;
            cmd.CommandText = "P_HR_LV_ASS_MST_INSERT";
            cmd.CommandType = CommandType.StoredProcedure;

            param = new OracleParameter("P_LV_YEAR", OracleType.Char, 4);
            param.Value = LV_YEAR.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_LV_MONTH", OracleType.Char, 2);
            param.Value = LV_MONTH.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_EMP_CODE", OracleType.Char, 10);
            param.Value = EMP_CODE.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_LV_ID", OracleType.Char, 3);
            param.Value = LV_ID.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_LV_TYPE", OracleType.Char, 3);
            param.Value = LV_ID.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_LV_DAYS_IN_MONTH", OracleType.VarChar, 3);
            param.Value = LV_DAYS_IN_MONTH.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_LV_AVL", OracleType.Char, 3);
            param.Value = "0";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_CARRY_FRWD_LV", OracleType.Char, 4);
            param.Value = CARRY_FRWD_LV.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_DAY_LEFT_PREV_YEAR", OracleType.Char, 3);
            param.Value = DAY_LEFT_PREV_YEAR.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_DAY_LEFT_PREV_YEAR_STATUS", OracleType.Char, 1);
            param.Value = DAY_LEFT_PREV_YEAR_STATUS.Trim();
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_STATUS", OracleType.Char, 1);
            param.Value = "1";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_DEL_STATUS", OracleType.Char, 1);
            param.Value = "0";
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_TDATE", OracleType.DateTime);
            param.Value = System.DateTime.Now;
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            param = new OracleParameter("P_TUSER", OracleType.VarChar,15);
            param.Value = "SA0001"; 
            param.Direction = ParameterDirection.Input;
            cmd.Parameters.Add(param);

            int iRecordEffected = cmd.ExecuteNonQuery();
        }
        catch (OracleException ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }
    private long getPerviousYearDay(string LV_YEAR, string EMP_CODE, string LV_ID)
    {
        long days = 0;
        try
        {

            //string checkYearChange = "SELECT ch_LeaveAssignedMasterId,ch_Year,ch_Month,ch_EmployeeMasterId,in_LeaveMasterId,ch_LeaveType,vc_Total_LeaveDay_InMonth,dt_Created,dt_Updated,ch_LeaveAvial,ch_CarryForwaredLeave,ch_DayLeftPreviousYear,ch_DayLeftPrevoisYear_Status FROM tblLeaveAssignedMaster where ltrim(rtrim(ch_Year))='" + ch_Year.Trim() + "' and ltrim(rtrim(ch_EmployeeMasterId))='" + ch_EmployeeMasterId.Trim() + "' and ltrim(rtrim(ch_LeaveType))='" + ch_LeaveType.Trim() + "' ";
            string checkYearChange = "SELECT LV_ASS_ID,LV_YEAR,LV_MONTH,EMP_CODE,LV_ID,LV_TYPE,LV_DAYS_IN_MONTH,TDATE,LV_AVL,CARRY_FRWD_LV,DAY_LEFT_PREV_YEAR,DAY_LEFT_PREV_YEAR_STATUS FROM hr_lv_ass_mst where ltrim(rtrim(LV_YEAR))='" + LV_YEAR.Trim() + "' and ltrim(rtrim(EMP_CODE))='" + EMP_CODE.Trim() + "' and ltrim(rtrim(LV_ID))='" + LV_ID.Trim() + "'";
            obj = new csSaitex();
            OracleDataReader dr = obj.getDataReader(checkYearChange, CommandType.Text);
            if (dr.Read() == true)
            {
                days = Convert.ToInt64(dr["DAY_LEFT_PREV_YEAR"].ToString().Trim());
            }
            dr.Close();
            dr.Dispose();
            dr = null;
            obj = null;
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
        }
        return days;
    }
}
