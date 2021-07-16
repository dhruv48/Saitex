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
using Common;
using errorLog;

public partial class CommonControls_CompansatoryLeave : System.Web.UI.UserControl
{
    OracleConnection ocon = null;
    OracleCommand ocmd = null;
    OracleParameter oparam = null;
    OracleDataAdapter dap = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblMode.Text = "Save";
            chkActive.Checked = true;
            imgbtnClear.Visible = false;
            imgbtnUpdate.Visible = false;
            bindGVCompensatoryLeave('y');
        }
       
    }
    private void bindGVCompensatoryLeave(char chView)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();
            string strSQL = "select CH_LEAVEDAY,CH_YEAR,CH_FORWARDED,IN_COMPENSATORYLEAVEMASTERID from tblCompensatoryLeaveMaster where CH_DELETESTATUS='0'";
            if (chView == 'Y')
            {
                strSQL = strSQL + "order by IN_COMPENSATORYLEAVEMASTERID asc ";
            }
            else
            {
                strSQL = strSQL + "order by DT_UPDATED desc";
            }
            ocmd = new OracleCommand(strSQL, ocon);
            dap = new OracleDataAdapter(ocmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            gvCompensatoryLeavemaster.DataSource = ds;
            gvCompensatoryLeavemaster.DataBind();
            lblTotalRecord.Text = ds.Tables[0].Rows.Count.ToString().Trim();
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
            if (ocon != null)
            {
                ocon.Close();
                ocon.Dispose();
                ocon = null;
            }

            if (ocmd != null)
            {
                ocmd.Dispose();
                ocmd = null;
            }

            if (dap != null)
            {
                dap.Dispose();
                dap = null;
            }
        }
    }
    private void blankcontrols()
    {
        txtYear.Text = "";
        txtLeaveDays.Text = "";
        radCarrying_forward_leave.SelectedIndex = 0;
        
    }
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();

            string strDup = "select CH_YEAR from tblCompensatoryLeaveMaster where ltrim(rtrim(CH_YEAR))=:CH_YEAR  order by in_CompensatoryleaveMasterId asc";
            int iRecordFound = 0;

            ocmd = new OracleCommand(strDup, ocon);

            oparam = new OracleParameter(":CH_YEAR", OracleType.Char, 4);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
            ocmd.Parameters.Add(oparam);

            strDup = Convert.ToString(ocmd.ExecuteOracleScalar());

            if (strDup != "")
            {
                iRecordFound = 1;
                lblMessage.Text = "This record already saved! Pls enter another record.";
                ocmd.Dispose();
            }

            if (iRecordFound == 0)
            {
                ocon = new OracleConnection();
                ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
                ocon.Open();

                string strMaxId = "";
                strMaxId = "select NVL(max(in_CompensatoryleaveMasterId),0) + 1 in_CompensatoryleaveMasterId from tblCompensatoryLeaveMaster";
                ocmd = new OracleCommand(strMaxId, ocon);
                strMaxId = ocmd.ExecuteScalar().ToString();
                ocmd.Dispose();

                string strSQL = "insert into tblCompensatoryLeaveMaster values(:ch_LeaveDay,:ch_Year,:ch_Forwarded,:CH_STATUS,:CH_DELETESTATUS,:DT_CREATED,:DT_UPDATED,:in_CompensatoryleaveMasterId)";

                ocmd = new OracleCommand(strSQL, ocon);

                oparam = new OracleParameter(":ch_LeaveDay", OracleType.Char, 3);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = CommonFuction.funFixQuotes(txtLeaveDays.Text.Trim());
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":ch_Year", OracleType.Char, 4);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":CH_FORWARDED", OracleType.Char, 3);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = radCarrying_forward_leave.SelectedItem.Text.Trim();
                ocmd.Parameters.Add(oparam);
                if (chkActive.Checked == true)
                {
                    oparam = new OracleParameter(":CH_STATUS", OracleType.Char, 1);
                    oparam.Value = '1';
                    oparam.Direction = ParameterDirection.Input;
                    ocmd.Parameters.Add(oparam);
                }
                else
                {
                    oparam = new OracleParameter(":CH_STATUS", OracleType.Char, 1);
                    oparam.Value = '0';
                    oparam.Direction = ParameterDirection.Input;
                    ocmd.Parameters.Add(oparam);
                }

                oparam = new OracleParameter(":CH_DELETESTATUS", OracleType.Char, 1);
                oparam.Value = '0';
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":DT_CREATED", OracleType.DateTime);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = System.DateTime.Now;
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":DT_UPDATED", OracleType.DateTime);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = System.DateTime.Now;
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":in_CompensatoryleaveMasterId", OracleType.Int32);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = Convert.ToInt32(strMaxId);
                ocmd.Parameters.Add(oparam);


                int iRecordEffected = ocmd.ExecuteNonQuery();

                if (iRecordEffected > 0)
                {
                    Session["saveStatus"] = 1;
                    blankcontrols();
                    bindGVCompensatoryLeave('y');
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);

                }
            }

        }


        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

            finally
            {
                if (ocon != null)
                {
                    ocon.Close();
                    ocon.Dispose();
                    ocon = null;
                }

                if (ocmd != null)
                {
                    ocmd.Dispose();
                    ocmd = null;
                }
                if (oparam != null)
                {
                    oparam = null;
                }
            }
        }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();

            string strSQL = "";
            strSQL = "Update tblCompensatoryLeaveMaster set CH_LEAVEDAY=:CH_LEAVEDAY,CH_FORWARDED=:CH_FORWARDED,CH_STATUS=:CH_STATUS,DT_UPDATED=:DT_UPDATED where IN_COMPENSATORYLEAVEMASTERID=:IN_COMPENSATORYLEAVEMASTERID and CH_YEAR=:CH_YEAR";

            ocmd = new OracleCommand(strSQL, ocon);

            oparam = new OracleParameter(":ch_LeaveDay", OracleType.Char, 3);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtLeaveDays.Text.Trim());
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":ch_Year", OracleType.Char, 4);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":CH_FORWARDED", OracleType.Char, 3);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = radCarrying_forward_leave.SelectedItem.Text.Trim();
            ocmd.Parameters.Add(oparam);
            if (chkActive.Checked == true)
            {
                oparam = new OracleParameter(":CH_STATUS", OracleType.Char, 1);
                oparam.Value = '1';
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);
            }
            else
            {
                oparam = new OracleParameter(":CH_STATUS", OracleType.Char, 1);
                oparam.Value = '0';
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);
            }

            oparam = new OracleParameter(":DT_UPDATED", OracleType.DateTime);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = System.DateTime.Now;
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":in_CompensatoryleaveMasterId", OracleType.Int32);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = ViewState["IN_COMPENSATORYLEAVEMASTERID"];
            ocmd.Parameters.Add(oparam);


            int iRecordEffected = ocmd.ExecuteNonQuery();

            if (iRecordEffected > 0)
            {
                Session["saveStatus"] = 1;
                blankcontrols();
                bindGVCompensatoryLeave('y');
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Updated Successfully');", true);

            }
        }




        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

        finally
        {
            if (ocon != null)
            {
                ocon.Close();
                ocon.Dispose();
                ocon = null;
            }

            if (ocmd != null)
            {
                ocmd.Dispose();
                ocmd = null;
            }
            if (oparam != null)
            {
                oparam = null;
            }
        }
    }
   
  
 private void SetTabContainerIndex(Control ctrl, string TabContainerId)
    {
        if (ctrl.HasControls())
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c.FindControl(TabContainerId) != null)
                {
                    AjaxControlToolkit.TabContainer tabs = (AjaxControlToolkit.TabContainer)c.FindControl(TabContainerId);
                    tabs.ActiveTabIndex = 5;
                    break;
                }
                else
                {
                    SetTabContainerIndex(c, TabContainerId);
                }
            }
        }
    }
    protected void gvCompensatoryLeavemaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (this.Page.HasControls())
        {
            foreach (Control c in this.Page.Controls)
            {
                SetTabContainerIndex(c, "Tabs");
            }
        }
        try
        {
            txtYear.Enabled = false;
            lblMode.Text = "Find";
            lblMessage.Text = "";
            imgbtnUpdate.Visible = true;
            imgbtnClear.Visible = true;
            imgbtnSave.Visible = false;

            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();
            string strSQL = "";
            strSQL = "select * from tblCompensatoryLeaveMaster where IN_COMPENSATORYLEAVEMASTERID='" + e.CommandArgument + "' ";
            ocmd = new OracleCommand(strSQL, ocon);
            OracleDataReader odr = ocmd.ExecuteReader();
            if (odr.Read() == true)
            {
                ViewState["IN_COMPENSATORYLEAVEMASTERID"] = odr["IN_COMPENSATORYLEAVEMASTERID"].ToString();
                txtLeaveDays.Text = odr["CH_LEAVEDAY"].ToString();
                txtYear.Text = odr["CH_YEAR"].ToString();
                radCarrying_forward_leave.SelectedIndex = radCarrying_forward_leave.Items.IndexOf(radCarrying_forward_leave.Items.FindByText(odr["CH_FORWARDED"].ToString().Trim()));
            }
        }

        catch (OracleException ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

        catch (Exception ex)
        {
            lblMessage.Text = "";
            lblErrorMessage.Text = ex.Message;
        }

        finally
        {
            if (ocon != null)
            {
                ocon.Close();
                ocon.Dispose();
                ocon = null;
            }

            if (ocmd != null)
            {
                ocmd.Dispose();
                ocmd = null;
            }

        }
    }
    protected void gvCompensatoryLeavemaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMessage.Text = "";
        lblErrorMessage.Text = "";
        gvCompensatoryLeavemaster.PageIndex = e.NewPageIndex;
        bindGVCompensatoryLeave('y');
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        lblMode.Text = "Save";
        txtLeaveDays.Text = "";
        txtYear.Text = "";
        radCarrying_forward_leave.SelectedIndex = 0;
        imgbtnClear.Visible = false;
        imgbtnUpdate.Visible = false;
        imgbtnSave.Visible = true;
       
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string URL = "CompensatoryLeave_OPT.aspx";
       ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
      //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
    
    
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    }



