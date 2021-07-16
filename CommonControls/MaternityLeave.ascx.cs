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
public partial class CommonControls_MaternityLeave : System.Web.UI.UserControl
{
    OracleConnection ocon = null;
    OracleCommand ocmd = null;
   
    OracleParameter oparam = null;
    OracleDataAdapter dap = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            chkActive.Checked = true;
            imgbtnClear.Visible = false;
            imgbtnUpdate.Visible = false;
            bindGVMaternityLeave('y');
            lblMode.Text = "Save";
                       
        }
       

    }
    private void bindGVMaternityLeave(char chView)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();
            string strSQL = "select CH_LEAVEDAY,CH_YEAR,CH_FORWARDED,CH_PERIODTYPE,CH_PERIODVALUE,IN_MATERNITYLEAVEMASTERID from tblMaternityLeaveMaster where CH_DELETESTATUS='0'";
            if (chView == 'Y')
            {
                strSQL = strSQL + "order by IN_MATERNITYLEAVEMASTERID asc ";
            }
            else
            {
                strSQL = strSQL + "order by DT_UPDATED desc";
            }
            ocmd = new OracleCommand(strSQL, ocon);
            dap = new OracleDataAdapter(ocmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            gvMaternityLeavemaster.DataSource = ds;
            gvMaternityLeavemaster.DataBind();
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
        txtLeaveDays.Text = "";
        txtPeriod.Text = "";
        txtYear.Text = "";
        radCarrying_forward_leave.SelectedIndex = 0;
        radLeaveApplicable.SelectedIndex = 0;

    }

    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {

        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();

            string strDup = "select CH_YEAR from tblMaternityLeaveMaster where ltrim(rtrim(CH_YEAR))=:CH_YEAR  order by in_MaternityLeaveMasterId asc";
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
                strMaxId = "select NVL(max(in_MaternityLeaveMasterId),0) + 1 in_MaternityLeaveMasterId from tblMaternityLeaveMaster";
                ocmd = new OracleCommand(strMaxId, ocon);
                strMaxId = ocmd.ExecuteScalar().ToString();
                ocmd.Dispose();

                string strSQL = "insert into tblMaternityLeaveMaster values(:CH_LEAVEDAY,:CH_YEAR,:CH_FORWARDED,:CH_PERIODTYPE,:CH_PERIODVALUE,:CH_STATUS,:CH_DELETESTATUS,:DT_CREATED,:DT_UPDATED,:in_MaternityLeaveMasterId)";
                ocmd = new OracleCommand(strSQL, ocon);

                oparam = new OracleParameter(":CH_LEAVEDAY", OracleType.Char, 3);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = CommonFuction.funFixQuotes(txtLeaveDays.Text.Trim());
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":CH_YEAR", OracleType.Char, 4);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":CH_FORWARDED", OracleType.Char, 3);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = radCarrying_forward_leave.SelectedItem.Text.Trim();
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":CH_PERIODTYPE", OracleType.Char, 2);
                oparam.Value = radLeaveApplicable.SelectedItem.Text.Trim();
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);


                if (radLeaveApplicable.SelectedValue == "PE")
                {
                    oparam = new OracleParameter(":CH_PERIODVALUE", OracleType.Char, 2);
                    oparam.Value = txtPeriod.Text.Trim();
                    oparam.Direction = ParameterDirection.Input;
                    ocmd.Parameters.Add(oparam);
                }
                else
                {
                    oparam = new OracleParameter(":CH_PERIODVALUE", OracleType.Char, 2);
                    oparam.Value = "";
                    oparam.Direction = ParameterDirection.Input;
                    ocmd.Parameters.Add(oparam);
                }

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

                oparam = new OracleParameter(":in_MaternityLeaveMasterId", OracleType.Int32);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = Convert.ToInt32(strMaxId);
                ocmd.Parameters.Add(oparam);

                int iRecordEffected = ocmd.ExecuteNonQuery();
                if (iRecordEffected > 0)
                {
                    Session["saveStatus"] = 1;
                    blankcontrols();
                    bindGVMaternityLeave('y');
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
    protected void gvMaternityLeavemaster_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (this.Page.HasControls())
            {
                foreach (Control c in this.Page.Controls)
                {
                    SetTabContainerIndex(c, "Tabs");
                }
            }
            lblMode.Text = "Find";
            imgbtnUpdate.Visible = true;
            imgbtnClear.Visible = true;
            imgbtnSave.Visible = false;
            txtYear.Enabled = false;
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();
            string strSQL = "";
            strSQL = "select * from tblMaternityLeaveMaster where IN_MATERNITYLEAVEMASTERID='" + e.CommandArgument + "' ";
            ocmd = new OracleCommand(strSQL, ocon);
            OracleDataReader odr = ocmd.ExecuteReader();
            if (odr.Read() == true)
            {
                ViewState["IN_MATERNITYLEAVEMASTERID"] = odr["IN_MATERNITYLEAVEMASTERID"].ToString();
                txtLeaveDays.Text = odr["CH_LEAVEDAY"].ToString();
                txtPeriod.Text = odr["CH_PERIODVALUE"].ToString();
                txtYear.Text = odr["CH_YEAR"].ToString();
                radCarrying_forward_leave.SelectedIndex = radCarrying_forward_leave.Items.IndexOf(radCarrying_forward_leave.Items.FindByText(odr["CH_FORWARDED"].ToString().Trim()));
                radLeaveApplicable.SelectedIndex = radLeaveApplicable.Items.IndexOf(radLeaveApplicable.Items.FindByText(odr["CH_PERIODTYPE"].ToString().Trim()));

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
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();

            string strSQL = "";
            strSQL = "Update tblMaternityLeaveMaster set CH_LEAVEDAY=:CH_LEAVEDAY,CH_FORWARDED=:CH_FORWARDED,CH_PERIODTYPE=:CH_PERIODTYPE,CH_PERIODVALUE=:CH_PERIODVALUE,CH_STATUS=:CH_STATUS,DT_UPDATED=:DT_UPDATED where IN_MATERNITYLEAVEMASTERID=:IN_MATERNITYLEAVEMASTERID and CH_YEAR=:CH_YEAR";
            ocmd = new OracleCommand(strSQL, ocon);


            oparam = new OracleParameter(":CH_LEAVEDAY", OracleType.Char, 3);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtLeaveDays.Text.Trim());
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":CH_YEAR", OracleType.Char, 4);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":CH_FORWARDED", OracleType.Char, 3);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = radCarrying_forward_leave.SelectedItem.Text.Trim();
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":CH_PERIODTYPE", OracleType.Char, 2);
            oparam.Value = radLeaveApplicable.SelectedItem.Text.Trim();
            oparam.Direction = ParameterDirection.Input;
            ocmd.Parameters.Add(oparam);

            if (radLeaveApplicable.SelectedValue == "PE")
            {
                oparam = new OracleParameter(":CH_PERIODVALUE", OracleType.Char, 2);
                oparam.Value = txtPeriod.Text.Trim();
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);
            }
            else
            {
                oparam = new OracleParameter(":CH_PERIODVALUE", OracleType.Char, 2);
                oparam.Value = "";
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);
            }

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

            oparam = new OracleParameter(":in_MaternityLeaveMasterId", OracleType.Int32);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = ViewState["IN_MATERNITYLEAVEMASTERID"];
            ocmd.Parameters.Add(oparam);

            int iRecordEffected = ocmd.ExecuteNonQuery();

            if (iRecordEffected == 1)
            {

                Session["saveStatus"] = 1;
                blankcontrols();
                bindGVMaternityLeave('y');
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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        txtYear.Text = "";
        txtLeaveDays.Text = "";
        radLeaveApplicable.SelectedIndex = 0;
        txtPeriod.Text = "";
        imgbtnClear.Visible = false;
        imgbtnSave.Visible = true;
        imgbtnUpdate.Visible = false;
        lblMode.Text = "Save";
        
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

    private void SetTabContainerIndex(Control ctrl, string TabContainerId)
    {
        if (ctrl.HasControls())
        {
            foreach (Control c in ctrl.Controls)
            {
                if (c.FindControl(TabContainerId) != null)
                {
                    AjaxControlToolkit.TabContainer tabs = (AjaxControlToolkit.TabContainer)c.FindControl(TabContainerId);
                    tabs.ActiveTabIndex = 2;
                    break;
                }
                else
                {
                    SetTabContainerIndex(c, TabContainerId);
                }
            }
        }
    }
    protected void imgbtnExit_Click1(object sender, ImageClickEventArgs e)
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "MaternityLeave_OPT.aspx";
      //  ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "javascript:popuponclick('" + URL + "')", true);
    }
    protected void gvMaternityLeavemaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMessage.Text = "";
        lblErrorMessage.Text = "";
        gvMaternityLeavemaster.PageIndex = e.NewPageIndex;
        bindGVMaternityLeave('y');
    }
}



