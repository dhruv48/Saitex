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
public partial class CommonControls_SpecialLeave : System.Web.UI.UserControl
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
            bindGVSpecialLeave('y');
        }

    }
    private void bindGVSpecialLeave(char chView)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();
            string strSQL = "select CH_LEAVEDAY,CH_YEAR,CH_FORWARDED,IN_SPECIALLEAVEMASTERID from tblSpecialLeaveMaster where CH_DELETESTATUS='0'";
            if (chView == 'Y')
            {
                strSQL = strSQL + "order by IN_SPECIALLEAVEMASTERID asc ";
            }
            else
            {
                strSQL = strSQL + "order by DT_UPDATED desc";
            }
            ocmd = new OracleCommand(strSQL, ocon);
            dap = new OracleDataAdapter(ocmd);
            DataSet ds = new DataSet();
            dap.Fill(ds);
            gvSpecialLeaveMaster.DataSource = ds;
            gvSpecialLeaveMaster.DataBind();
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
        txtNoOfDays.Text = "";
        txtYear.Text = "";
        radLeaveSelection.SelectedIndex = 0;

    }
    protected void imgbtnSave_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            ocon = new OracleConnection();
            ocon.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            ocon.Open();

            string strDup = "select CH_YEAR from tblspecialLeaveMaster where ltrim(rtrim(CH_YEAR))=:CH_YEAR  order by IN_SPECIALLEAVEMASTERID asc";
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
                strMaxId = "select NVL(max(in_SpecialLeaveMasterId),0) + 1 in_SpecialLeaveMasterId from tblspecialLeaveMaster";
                ocmd = new OracleCommand(strMaxId, ocon);
                strMaxId = ocmd.ExecuteScalar().ToString();
                ocmd.Dispose();
                string strSQL = "insert into tblspecialLeaveMaster(CH_LEAVEDAY,CH_YEAR,CH_FORWARDED,CH_STATUS,DT_CREATED,DT_UPDATED,CH_DELETESTATUS,in_SpecialLeaveMasterId)values(:ch_LeaveDay,:ch_Year,:ch_Forwarded,:CH_STATUS,:DT_CREATED,:DT_UPDATED,:CH_DELETESTATUS,:in_SpecialLeaveMasterId)";

                ocmd = new OracleCommand(strSQL, ocon);

                oparam = new OracleParameter(":ch_LeaveDay", OracleType.Char, 3);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = CommonFuction.funFixQuotes(txtNoOfDays.Text.Trim());
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":ch_Year", OracleType.Char, 4);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":ch_Forwarded", OracleType.Char, 3);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = radLeaveSelection.SelectedItem.Text.Trim();
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


                oparam = new OracleParameter(":DT_CREATED", OracleType.DateTime);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = System.DateTime.Now;
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":DT_UPDATED", OracleType.DateTime);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = System.DateTime.Now;
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":CH_DELETESTATUS", OracleType.Char, 1);
                oparam.Value = '0';
                oparam.Direction = ParameterDirection.Input;
                ocmd.Parameters.Add(oparam);

                oparam = new OracleParameter(":in_SpecialLeaveMasterId", OracleType.Int32);
                oparam.Direction = ParameterDirection.Input;
                oparam.Value = Convert.ToInt32(strMaxId);
                ocmd.Parameters.Add(oparam);


                int iRecordEffected = ocmd.ExecuteNonQuery();

                if (iRecordEffected > 0)
                {
                    Session["saveStatus"] = 1;
                    blankcontrols();
                    bindGVSpecialLeave('y');
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

            string strSQL = "update tblSpecialLeaveMaster set CH_LEAVEDAY=:CH_LEAVEDAY,CH_FORWARDED=:CH_FORWARDED,CH_STATUS=:CH_STATUS,DT_UPDATED=:DT_UPDATED where IN_SPECIALLEAVEMASTERID=:IN_SPECIALLEAVEMASTERID and CH_YEAR=:CH_YEAR";
            ocmd = new OracleCommand(strSQL, ocon);

            oparam = new OracleParameter(":CH_LEAVEDAY", OracleType.Char, 3);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtNoOfDays.Text.Trim());
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":CH_YEAR", OracleType.Char, 4);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = CommonFuction.funFixQuotes(txtYear.Text.Trim());
            ocmd.Parameters.Add(oparam);

            oparam = new OracleParameter(":CH_FORWARDED", OracleType.Char, 3);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = radLeaveSelection.SelectedItem.Text.Trim();
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

            oparam = new OracleParameter(":IN_SPECIALLEAVEMASTERID", OracleType.Int32);
            oparam.Direction = ParameterDirection.Input;
            oparam.Value = ViewState["IN_SPECIALLEAVEMASTERID"];
            ocmd.Parameters.Add(oparam);

            int iRecordEffected = ocmd.ExecuteNonQuery();
            if (iRecordEffected > 0)
            {
                Session["saveStatus"] = 1;
                blankcontrols();
                bindGVSpecialLeave('y');
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
                    tabs.ActiveTabIndex = 7;
                    break;
                }
                else
                {
                    SetTabContainerIndex(c, TabContainerId);
                }
            }
        }
    }


    protected void gvSpecialLeaveMaster_RowCommand(object sender, GridViewCommandEventArgs e)
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
            strSQL = "select * from tblSpecialLeaveMaster where IN_SPECIALLEAVEMASTERID='" + e.CommandArgument + "' ";
            ocmd = new OracleCommand(strSQL, ocon);
            OracleDataReader odr = ocmd.ExecuteReader();
            if (odr.Read() == true)
            {
                ViewState["IN_SPECIALLEAVEMASTERID"] = odr["IN_SPECIALLEAVEMASTERID"].ToString();
                txtNoOfDays.Text = odr["CH_LEAVEDAY"].ToString();
                txtYear.Text = odr["CH_YEAR"].ToString();
                radLeaveSelection.SelectedIndex = radLeaveSelection.Items.IndexOf(radLeaveSelection.Items.FindByText(odr["CH_FORWARDED"].ToString().Trim()));

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
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {

        lblMode.Text = "Save";
        chkActive.Checked = true;
        imgbtnClear.Visible = false;
        imgbtnUpdate.Visible = false;
        blankcontrols();

    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "SpecialLeave_OPT.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
    }
    protected void gvSpecialLeaveMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        lblMessage.Text = "";
        lblErrorMessage.Text = "";
        gvSpecialLeaveMaster.PageIndex = e.NewPageIndex;
        bindGVSpecialLeave('y');
    }
}
