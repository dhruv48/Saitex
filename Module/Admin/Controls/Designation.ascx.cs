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
using System.Web.UI.WebControls.Adapters;
using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Admin_UserControls_Designation : System.Web.UI.UserControl
{
    OracleConnection con = null;
    OracleCommand cmd = null;
    OracleParameter param = null;
    OracleDataReader dr = null;
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
            ddlDesig.Visible = false;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            tdFind.Visible = true;
            lblMode.Text = "Save";
            bindDDLDesignation();
        }

    }

    private void bindDDLDesignation()
    {
        DataTable dt = SaitexBL.Interface.Method.CM_DESIG_MST.Select();
        ddlDesignation.DataTextField = "DESIG_NAME";
        ddlDesignation.DataValueField = "DESIG_CODE";
        ddlDesignation.DataSource = dt;
        ddlDesignation.DataBind();
        ddlDesignation.Items.Insert(0, new ListItem("----------Select---------", ""));       

    }

    private void InsertData()
    {
        try
        {
            if (txtDesigCode.Text != "")
            {
                if (txtDesigName.Text != "")
                {
                    if (ddlDesignation.SelectedIndex > 0)

                    {
                        if (txtRemarks.Text != "")
                        {
                            int iRecordFound = 0;
                            SaitexDM.Common.DataModel.CM_DESIG_MST oDesn_Mst = new SaitexDM.Common.DataModel.CM_DESIG_MST();
                            oDesn_Mst.DESIG_CODE = CommonFuction.funFixQuotes(txtDesigCode.Text.Trim());
                            oDesn_Mst.DESIG_NAME = CommonFuction.funFixQuotes(txtDesigName.Text.Trim());
                            oDesn_Mst.DESIG_REMARKS = CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                            oDesn_Mst.SR_DESIG_CODE = CommonFuction.funFixQuotes(ddlDesignation.SelectedValue.Trim());
                            oDesn_Mst.TUSER = Session["urLoginId"].ToString().Trim();
                            bool bResult = SaitexBL.Interface.Method.CM_DESIG_MST.Insert_CM_DESIG_MST(oDesn_Mst, out iRecordFound);
                            if (bResult)
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Saved Successfully');", true);
                            }
                            else if (iRecordFound == 1)
                            {

                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Already Exist');", true);

                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Problem in Insertion');", true);

                            }
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Enter Remarks');", true);

                        }

                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Select Senior Designation');", true);

                    }
                    }

                    else
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Enter Designation Name .');", true);

                    }

                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Please Enter Designation Code .');", true);
                }
            
        }
        catch (Exception oEx)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "Error", "window.alert('" + oEx.Message + "');", true);

        }

    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        InsertData();
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("/Saitex/Module/Admin/Pages/DesignationMaster.aspx", false);
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
                Response.Redirect("~/GetUserAuthorisation.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnFind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            ddlDesig .Visible = true;           
            tdSave.Visible = false;
            tdUpdate.Visible = true;
            tdDelete.Visible = true;
            tdFind.Visible = false;
            lblMode.Text = "Find";
        }

        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        UpadateDesigMasterData();
    }
    private void UpadateDesigMasterData()
    {
        if (Page.IsValid)
        {

            try
            {
                if (txtDesigName.Text.Trim() != "" && ddlDesignation .SelectedValue.Trim() != "" && txtRemarks.Text.Trim() != "" )
                {

                    int iRecordEffected = 0;
                    int iRecordFound = 0;
                    SaitexDM.Common.DataModel.CM_DESIG_MST oCMDESIG = new SaitexDM.Common.DataModel.CM_DESIG_MST();
                    oCMDESIG.DESIG_CODE = ViewState["iDESIG_CODE"].ToString();

                    oCMDESIG.DESIG_NAME=CommonFuction.funFixQuotes(txtDesigName .Text.Trim());
                        oCMDESIG.DESIG_REMARKS=CommonFuction.funFixQuotes(txtRemarks.Text.Trim());
                        oCMDESIG.SR_DESIG_CODE=CommonFuction.funFixQuotes(ddlDesignation .SelectedValue.Trim());
            
                    bool bResult = SaitexBL.Interface.Method.CM_DESIG_MST.UpdateDesignMaster(oCMDESIG, out iRecordFound);

                    if (bResult)
                    {

                        Session["saveStatus"] = 1;
                        Response.Redirect("/Saitex/Module/HRMS/Pages/DesignationMaster.aspx?cId=U", false);
                    }
                    else if (iRecordFound > 0)
                    {
                        Common.CommonFuction.ShowMessage("This record is already saved Please save another Record");
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Some Problem in Updation");
                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Pls enter mandatory field");
                }
            }

            catch (OracleException ex)
            {
                Common.CommonFuction.ShowMessage(ex.Message.ToString());
                ErrHandler.WriteError(ex.Message);
            }

            catch (Exception ex)
            {
               Common.CommonFuction.ShowMessage(ex.Message.ToString());
                ErrHandler.WriteError(ex.Message);
            }

            finally
            {
                
            }
        }
    }
    protected void ddlDesig_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        // Getting the items
        DataTable data = GetItems(e.Text.ToString().Trim(), Convert.ToInt32(e.ItemsOffset), 10);
        ddlDesig .DataSource = data;
        ddlDesig .DataBind();
        // Calculating the numbr of items loaded so far in the ComboBox
        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
        // Getting the total number of items that start with the typed text
        e.ItemsCount = GetItemsCount(e.Text);

    } 
    protected DataTable GetItems(string strDesigName, int startOffset, int numberOfItems)
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DESIG_MST.GetItems(strDesigName);
            return dt;
        }      
        catch (Exception ex)
        {            
            ErrHandler.WriteError(ex.Message);
            throw ex;
        }  
    }
    protected int GetItemsCount(string text)
    {
        con = new OracleConnection();
        con.ConnectionString = ConfigurationManager.ConnectionStrings["csTextile"].ConnectionString;
        con.Open();

        cmd = new OracleCommand("SELECT COUNT(*) FROM CM_DESIG_MST WHERE DESIG_NAME LIKE :DESIG_NAME", con);
        cmd.Parameters.Add(":DESIG_NAME", OracleType.VarChar).Value = text + '%';

        return int.Parse(cmd.ExecuteScalar().ToString());
    }

   private void getDesigMasterData(string  iDesigMasterCode)
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.CM_DESIG_MST.GetDesigMaster(iDesigMasterCode);
            txtDesigCode.Text = dt.Rows[0]["DESIG_CODE"].ToString().Trim();
            txtDesigName.Text = dt.Rows[0]["DESIG_NAME"].ToString().Trim();            
            txtRemarks.Text = dt.Rows[0]["DESIG_REMARKS"].ToString().Trim();
            ddlDesignation .SelectedValue = dt.Rows[0]["SR_DESIG_CODE"].ToString().Trim();         
          
            ddlDesig .Visible = false;            
            tblDesgMainTable.Visible = true;
        }      
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(ex.Message.ToString());
            ErrHandler.WriteError(ex.Message);
        }    

    }
    protected void ddlDesig_SelectedIndexChanged1(object sender, Obout.ComboBox.ComboBoxItemEventArgs e)
    {
        getDesigMasterData(Convert.ToString(ddlDesig .SelectedValue.Trim()));
        ViewState["iDESIG_CODE"] = ddlDesig.SelectedValue.Trim();
    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string URL = "../Reports/DESIGNATION_MASTER.aspx";
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
  
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            int iRecordFound = 0;
            SaitexDM.Common.DataModel.CM_DESIG_MST oCMDE= new SaitexDM.Common.DataModel.CM_DESIG_MST();
            oCMDE.DESIG_CODE = ViewState["iDESIG_CODE"].ToString();
            bool bResult = SaitexBL.Interface.Method.CM_DESIG_MST.DeleteDesigMaster(oCMDE);
            if (bResult)
            {
                Session["saveStatus"] = 1;
                Response.Redirect("~/Module/ADMIN/Pages/DesignationMaster.aspx?cId=D", false);
            }
        }
        catch (OracleException ex)
        {
            //lblMessage.Text = "";
            //lblErrorMessage.Text = ex.Message;
            ErrHandler.WriteError(ex.Message);

        }
    }
}
