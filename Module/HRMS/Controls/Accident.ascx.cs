using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_Accident : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                Clear();
                BindCmbEmp();
                MaxProid();
                griddata(""); 
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void BindCmbEmp()
    {
        try
        {
            cmbEmpCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.Getempcode();
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbEmpCode.DataValueField = "EMP_CODE";
                cmbEmpCode.DataTextField = "EMPLOYEENAME";
                cmbEmpCode.DataSource = dt;
                cmbEmpCode.DataBind();

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void MaxProid()
    {
        try
        {
            string x = string.Empty;
            int y = 0;
        
            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.GetMaxProid();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (dv.Count > 0)
                {
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        x = dv[iLoop]["MAX_ID"].ToString();
                        y = int.Parse(x);
                        y = y + 1;
                        txtaccident_id.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void cmbEmpCode_SelectedIndexChanged(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        try
        {
            if (cmbEmpCode.SelectedIndex != -1)
            {
                GetData(cmbEmpCode.SelectedValue.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void GetData(string EMP_CODE)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.Data(EMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
               
                cmbEmpCode.SelectedValue = dt.Rows[0]["EMP_CODE"].ToString().Trim();
                txtdesig.Text = dt.Rows[0]["DESIG_NAME"].ToString().Trim();
                txtdept.Text = dt.Rows[0]["DEPT_NAME"].ToString().Trim();
                txtesi_no.Text = dt.Rows[0]["ESI"].ToString().Trim();
                txtsex.Text = dt.Rows[0]["GENDER"].ToString().Trim();
               // txtage.Text = dt.Rows[0]["DOB"].ToString().Trim();
                DateTime BirthDate  = DateTime.Parse(dt.Rows[0]["DOB"].ToString().Trim());
                // get the difference in years
               
                //int years = DateTime.Now.Year - BirthDate.Year;
                int years = DateTime.Now.Year - BirthDate.Year;
                // subtract another year if we're before the
                // birth day in the current year
                if (DateTime.Now.Month < BirthDate.Month ||
                    (DateTime.Now.Month == BirthDate.Month &&
                    DateTime.Now.Day < BirthDate.Day))
                    years--;
                txtage.Text = years.ToString();
                txtshift.Text = dt.Rows[0]["SFT_NAME"].ToString().Trim();  
            }
        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnNew_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
              Insert();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Inserting Record.\r\nSee error log for detail."));
        }
    }
    private void Insert()
    {
        SaitexDM.Common.DataModel.HR_ACCIDENT_MST oHR_ACCIDENT_MST = new SaitexDM.Common.DataModel.HR_ACCIDENT_MST();
        int iRecordFound = 0;
        bool bResult = false;
        try
        {
            oHR_ACCIDENT_MST.ACCIDENT_ID = Common.CommonFuction.funFixQuotes(txtaccident_id.Text.Trim());
            oHR_ACCIDENT_MST.EMP_CODE = cmbEmpCode.SelectedValue.ToString();
            oHR_ACCIDENT_MST.ACCIDENT_DATE = DateTime.Parse(txtdate.Text.ToString());
            oHR_ACCIDENT_MST.ACCIDENT_TIME = Common.CommonFuction.funFixQuotes(txttime.Text.Trim());
            oHR_ACCIDENT_MST.ACCIDENT_PLACE = Common.CommonFuction.funFixQuotes(txtplace.Text.Trim());
            oHR_ACCIDENT_MST.ACCIDENT_REASON = Common.CommonFuction.funFixQuotes(txtreason.Text.Trim());
            bResult = SaitexBL.Interface.Method.HR_ACCIDENT_MST.Insert(oHR_ACCIDENT_MST, out iRecordFound);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Recored Save Successfully .')", true);
                griddata("");  
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Recored Not Save .')", true);
            }
        }
        catch
        {
            throw;
        }
    }  
    private void Update()
    {
        SaitexDM.Common.DataModel.HR_ACCIDENT_MST oHR_ACCIDENT_MST = new SaitexDM.Common.DataModel.HR_ACCIDENT_MST();
        int iRecordFound = 0;
        bool bResult = false;
        try
        {
            oHR_ACCIDENT_MST.ACCIDENT_ID = Common.CommonFuction.funFixQuotes(txtaccident_id.Text .Trim());
            oHR_ACCIDENT_MST.EMP_CODE = cmbEmpCode.SelectedValue.ToString();
            oHR_ACCIDENT_MST.ACCIDENT_DATE = DateTime.Parse (txtdate.Text.ToString());
            oHR_ACCIDENT_MST.ACCIDENT_TIME = Common.CommonFuction.funFixQuotes(txttime .Text .Trim ());
            oHR_ACCIDENT_MST.ACCIDENT_PLACE = Common.CommonFuction.funFixQuotes(txtplace .Text .Trim());
            oHR_ACCIDENT_MST.ACCIDENT_REASON = Common.CommonFuction.funFixQuotes(txtreason .Text .Trim());
            bResult = SaitexBL.Interface.Method.HR_ACCIDENT_MST.Update(oHR_ACCIDENT_MST, out iRecordFound);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page ,Page .GetType(),"25","window.alert('Recored Update Successfully .')",true );
                griddata("");  
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Recored Not Update .')", true);
            }
        }
        catch
        {
            throw;
        } 
    }
    private void Delete()
    {
        SaitexDM.Common.DataModel.HR_ACCIDENT_MST oHR_ACCIDENT_MST = new SaitexDM.Common.DataModel.HR_ACCIDENT_MST();
    
        try
        {
            oHR_ACCIDENT_MST.ACCIDENT_ID = Common.CommonFuction.funFixQuotes(txtaccident_id .Text .Trim ());
            oHR_ACCIDENT_MST.EMP_CODE = cmbEmpCode.SelectedValue.ToString();
            bool bResult = SaitexBL.Interface.Method.HR_ACCIDENT_MST.Delete(oHR_ACCIDENT_MST);
            if (bResult)
            {
                griddata("");           
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Delete Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Not Delete');", true);

            }
             
        }
        catch
        {
            throw;
        }
    }
    private void Clear()
    {
        try
        {
            ddlfind.Visible = false;
            txtaccident_id.Enabled = false;
            MaxProid();
            lblMode.Text = "Save";
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            txtage.Text = string.Empty;
            txtdate.Text = string.Empty;
            txtdept.Text = string.Empty;
            txtdesig.Text = string.Empty;
            txtesi_no.Text = string.Empty;
            txtplace.Text = string.Empty;
            txtreason.Text = string.Empty;
            txtsex.Text = string.Empty;
            txtshift.Text = string.Empty;
            txttime.Text = string.Empty;
            cmbEmpCode.SelectedIndex = -1;
            txtdesig.Enabled = false;
            txtdept.Enabled = false;
            txtesi_no.Enabled = false;
            txtage.Enabled = false;
            txtsex.Enabled = false;
            txtshift.Enabled = false;
        }
        catch
        {
            throw;
        }
       
    }
    protected void grdaccident_SelectedIndexChanged(object sender, EventArgs e)
    {

    } 
    protected void GridAccident_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                GridRowdata(Convert.ToInt32(e.CommandArgument));
                ViewState["RecordEdit"] = e.CommandArgument.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in GridView RowCommand.\r\nSee error log for detail."));
        }    


        
        
    }
    private void Gridbind(int ACCIDENT_ID)
    {
        try
        {
          
            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.grdbind();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (ACCIDENT_ID != 0)
                {
                    dv.RowFilter = " ACCIDENT_ID='" + ACCIDENT_ID + "'";
                }
            

                if (dv.Count > 0)
                {
                    tdUpdate.Visible = true;
                    tdDelete.Visible = true;
                    tdSave.Visible = false;
                    cmbEmpCode.Enabled = false;
                    lblMode.Text = "Update";
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        cmbEmpCode.SelectedValue = dv[iLoop]["EMP_CODE"].ToString().Trim();
                        GetData(cmbEmpCode.SelectedValue.ToString().Trim());
                        txtdept.Text  = dv[iLoop]["DEPT_NAME"].ToString().Trim();
                        txtdesig.Text  = dv[iLoop]["DESIG_NAME"].ToString().Trim();
                         txtdate.Text  = dv[iLoop]["ACCIDENT_DATE"].ToString().Trim();
                        txttime.Text  = dv[iLoop]["ACCIDENT_TIME"].ToString().Trim();
                        txtplace.Text = dv[iLoop]["ACCIDENT_PLACE"].ToString().Trim();
                       txtreason .Text  = dv[iLoop]["ACCIDENT_REASON"].ToString().Trim();
                      

                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void GridAccident_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    private void griddata(string EMP_CODE)
    {
        try
        {   
               

            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.grdbind();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                if (EMP_CODE != "")
                {
                    dv.RowFilter = " EMP_CODE='" + EMP_CODE + "'";
                }
                if (dv.Count > 0)
                {
                   

                        GridAccident.DataSource = dv;
                        GridAccident.DataBind();
                    
                }
            }
        }
        catch
        {
            throw;
        }
        
    }
    private void GridRowdata(int ACCIDENT_ID)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.grdbind();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = " ACCIDENT_ID='" + ACCIDENT_ID + "'";
                if (dv.Count > 0)
                {
                    tdUpdate.Visible = true;
                    tdDelete.Visible = true;
                    tdSave.Visible = false;
                    lblMode.Text = "Update";
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        txtaccident_id.Text = dv[iLoop]["ACCIDENT_ID"].ToString().Trim(); 
                        cmbEmpCode.SelectedValue = dv[iLoop]["EMP_CODE"].ToString().Trim();
                        GetData(cmbEmpCode.SelectedValue.ToString().Trim());
                        txtdate.Text = dv[iLoop]["ACCIDENT_DATE"].ToString().Trim();
                        txttime.Text = dv[iLoop]["ACCIDENT_TIME"].ToString().Trim();
                        txtplace.Text = dv[iLoop]["ACCIDENT_PLACE"].ToString().Trim();
                        txtreason.Text = dv[iLoop]["ACCIDENT_REASON"].ToString().Trim();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Update();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Updating Record.\r\nSee error log for detail."));
        }          
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Delete();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Deleting Record.\r\nSee error log for detail."));
        }    
        
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Clear Record.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void Imgbtnfind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bindddlfind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding Record.\r\nSee error log for detail."));
        }
    }
    private void bindddlfind()
    {
        try
        {
            cmbEmpCode.Visible = false;
            ddlfind.Visible = true;
            ddlfind.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.HR_ACCIDENT_MST.Getfind();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlfind.DataValueField = "EMP_CODE";
                ddlfind.DataTextField = "EMPLOYEENAME";
                ddlfind.DataSource = dt;
                ddlfind.DataBind();
            }
            ddlfind.Items.Insert(0, new ListItem("---------Find----------", ""));
        }
        catch
        {
            throw;
        }
    }


    protected void ddlfind_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            griddata(ddlfind.SelectedValue.Trim().ToString());
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Finding.\r\nSee error log for detail."));
        }    

    }
}
