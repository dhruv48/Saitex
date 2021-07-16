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
using Obout.ComboBox;
public partial class HRMS_Holiday : System.Web.UI.Page
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataAdapter da = null;
    DataSet ds = null;
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnSave.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnDelete.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit')");
        imgbtnHelp.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Help')");
    } 
    
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
        
            txtYear.Text = System.DateTime.Now.Year.ToString();
            chkActive.Checked = true;
            tdUpdate.Visible=false;
            tdDelete.Visible = false;
            lblMode.Text = "Save";
            ddlHoliday.Visible = false;
        }
        if (Convert.ToInt16(Session["saveStatus"]) == 1)
        {
            if (Request.QueryString["cId"].ToString().Trim() == "S")
            {
                lblMessage.Text = "Record Saved successfully";

            }
            else if (Request.QueryString["cId"].ToString().Trim() == "U")
            {
                lblMessage.Text = "Record Updated successfully";
            }
            else if (Request.QueryString["cId"].ToString().Trim() == "D")
            {
                lblMessage.Text = "Record Deleted successfully";
            }

            Session["saveStatus"] = 0;
        }
    }
    private void BlanckControls()
    {
        txtHoildayDate.Text = "";
        txtHoildayName.Text = "";
        txtYear.Text = "";
        radLeaveSelection.SelectedIndex = 0;
    }

    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        InsertHolidayMasterData();
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpadateHolidayMasterData();
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.HolidayMaster oHM = new SaitexDM.Common.DataModel.HolidayMaster();
            oHM.HLD_ID = Convert.ToInt32(ViewState["iHolidayId"]);
            bool bResult = SaitexBL.Interface.Method.HolidayMaster.DeleteHolidayMaster(oHM);
           
            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("/Saitex/Module/HRMS/Pages/Holiday.aspx?cId=D", false);
            }
        }
        catch (OracleException ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);

        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlHoliday.Visible = true;
            tblMainTable.Visible = false;
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            lblMode.Text = "Find";
            lblMessage.Text = "";
            lblErrorMessage.Text = "";

        }

        catch (Exception ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Server.Transfer("Holiday.aspx");
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "Holiday_OPT.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=300');", true);
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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
   
    private void InsertHolidayMasterData()
    {
        if (Page.IsValid)
        {

            try
            {
                //bool bResult;
                SaitexDM.Common.DataModel.HolidayMaster oHM = new SaitexDM.Common.DataModel.HolidayMaster();
                int iRecordFound = 0;
                if (txtYear.Text.Trim() != "" && radLeaveSelection.SelectedValue.Trim() != "" && txtHoildayName.Text.Trim() != "" && txtHoildayDate.Text.Trim() != "")
                {

                    int iRecordEffected = 0;

                    
                    //oHM.HLD_ID = 1;
                    oHM.HLD_NAME = CommonFuction.funFixQuotes(txtHoildayName.Text.Trim());
                    oHM.YEAR = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                    oHM.HLD_DATE = Convert.ToDateTime(txtHoildayDate.Text.Trim());
                    oHM.OPT_LV = CommonFuction.funFixQuotes(radLeaveSelection.SelectedValue.Trim());
                    if (chkActive.Checked == true)
                    {
                        oHM.STATUS = "1";
                    }
                    else
                    {
                        oHM.STATUS = "0";
                    }
                   
                    oHM.TUSER = Session["urLoginId"].ToString().Trim();

                }
                else
                {
                    lblMessage.Text = "Pls enter mandatory fielde";
                }


                bool bResult = SaitexBL.Interface.Method.HolidayMaster.InsertHoliday(oHM, out iRecordFound);

                if (bResult)
                {
                   
                    Session["saveStatus"] = 1;

                    Response.Redirect("/Saitex/Module/HRMS/Pages/Holiday.aspx?cId=S", false);
                }
                else if (iRecordFound > 0)
                {
                    lblMessage.Text = "This record is already saved Please save another Record";
                }


                else
                {
                    lblMessage.Text = "Pls enter all mandatory field";
                }
            
            }
            

            catch (OracleException ex)
            {
                //lblMessage.Text = "";
                //lblErrorMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
                ErrHandler.WriteError(ex.Message);
            }

            catch (Exception ex)
            {
                //lblMessage.Text = "";
                //lblErrorMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
                ErrHandler.WriteError(ex.Message);
            }

            finally
            {


            }
        }
    
    }
    private void UpadateHolidayMasterData()
    {
        if (Page.IsValid)
        {

            try
            {
                if (txtYear.Text.Trim() != "" && radLeaveSelection.SelectedValue.Trim() != "" && txtHoildayName.Text.Trim() != "" && txtHoildayDate.Text.Trim() != "")
                {

                    int iRecordEffected = 0;
                    int iRecordFound = 0;
                    SaitexDM.Common.DataModel.HolidayMaster oHM = new SaitexDM.Common.DataModel.HolidayMaster();
                    oHM.HLD_ID = Convert.ToInt32(ViewState["iHolidayId"]);
                    oHM.HLD_NAME = CommonFuction.funFixQuotes(txtHoildayName.Text.Trim());
                    oHM.YEAR = CommonFuction.funFixQuotes(txtYear.Text.Trim());
                    oHM.HLD_DATE = Convert.ToDateTime(txtHoildayDate.Text.Trim());
                    oHM.OPT_LV = CommonFuction.funFixQuotes(radLeaveSelection.SelectedValue.Trim());
                    if (chkActive.Checked == true)
                    {
                        oHM.STATUS = "1";
                    }
                    else
                    {
                        oHM.STATUS = "0";
                    }
                    //oHM.DEL_STATUS=0;

                    //oHM.TDATE = System.DateTime.Now;
                    oHM.TUSER = Session["urLoginId"].ToString().Trim();
                    bool bResult = SaitexBL.Interface.Method.HolidayMaster.UpdateHolidayMaster(oHM, out iRecordFound);
                    
                    if (bResult)
                    {

                        Session["saveStatus"] = 1;

                        Response.Redirect("/Saitex/Module/HRMS/Pages/Holiday.aspx?cId=U", false);
                    }
                    else if (iRecordFound > 0)
                    {
                        lblMessage.Text = "This record is already saved Please save another Record";
                    }
                }
                else
                {
                    lblMessage.Text = "Pls enter mandatory fielde";
                }
            }

            catch (OracleException ex)
            {
                //lblMessage.Text = "";
                //lblErrorMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
                ErrHandler.WriteError(ex.Message);
            }

            catch (Exception ex)
            {
                //lblMessage.Text = "";
                //lblErrorMessage.Text = "";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
                ErrHandler.WriteError(ex.Message);
            }

            finally
            {
                //if (oHM != null)
                //{
                //    oHM = null;
                //}

            }
        }
    }
    protected void ddlHoliday_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        
        // Getting the items
        DataTable data = GetItems(e.Text.ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);

        ddlHoliday.DataSource = data;
        ddlHoliday.DataBind();

        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);
       
    }

    // Gets all the countries that start with the typed text, taking paging into account
    //protected DataTable GetItems(string text, int startOffset, int numberOfItems)
    protected DataTable GetItems(string strHolidayName, int startOffset, int numberOfItems)
    {
        
        try
        {
            con = new OracleConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
            con.Open();
       
            string whereClause = " WHERE HLD_NAME LIKE :HLD_NAME";
            string sortExpression = " ORDER BY HLD_NAME asc";

            string commandText = "SELECT  * FROM HR_HLD_MST";
            commandText += whereClause;
            //if (startOffset != 0)
            //{
            //    commandText += " AND HLD_ID NOT IN (SELECT TOP " + startOffset + " HLD_ID FROM HR_HLD_MST";
            //    commandText += whereClause + sortExpression + ")";
            //}

            commandText += sortExpression;
            cmd = new OracleCommand(commandText, con);
            cmd.Parameters.Add(":HLD_NAME", OracleType.VarChar).Value = strHolidayName + '%';
            
            da = new OracleDataAdapter(cmd);
            ds = new DataSet();
            da.Fill(ds);
            DataTable dt = new DataTable();
            dt= ds.Tables[0];
            return dt;
            
        }

        catch (OracleException ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }

        catch (Exception ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = "";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
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

        cmd = new OracleCommand("SELECT COUNT(*) FROM HR_HLD_MST WHERE HLD_NAME LIKE :HLD_NAME", con);
        cmd.Parameters.Add(":HLD_NAME", OracleType.VarChar).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

    protected void ddlHoliday_SelectedIndexChanged(object sender, ComboBoxItemEventArgs e)
    {
        getHolidayMasterData(Convert.ToInt32(ddlHoliday.SelectedValue.Trim()));

        ViewState["iHolidayId"] = ddlHoliday.SelectedValue.Trim();
    }
    
    
    private void getHolidayMasterData(int iHolidayMasterId)
    {
        try
        {
            
            DataTable dt = new DataTable();
            dt= SaitexBL.Interface.Method.HolidayMaster.GetHolidayMaster(iHolidayMasterId);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
                //txtYear.Text = dt.Rows[i]["YEAR"].ToString().Trim();
                txtYear.Text = dt.Rows[0]["YEAR"].ToString().Trim();
                txtHoildayName.Text = dt.Rows[0]["HLD_NAME"].ToString().Trim();
                txtHoildayDate.Text = dt.Rows[0]["HLD_DATE"].ToString().Trim();
                radLeaveSelection.SelectedValue = dt.Rows[0]["OPT_LV"].ToString().Trim();
                if (dt.Rows[0]["STATUS"].ToString().Trim() == "1")
                {
                    chkActive.Checked = true;
                }
                else
                {
                    chkActive.Checked = false;
                }
            //}
                ddlHoliday.Visible = false;
                tblMainTable.Visible = true;
        }

        catch (OracleException ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }

        catch (Exception ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }

        finally
        { 
        
        }
    
    
    
    }

    
}
