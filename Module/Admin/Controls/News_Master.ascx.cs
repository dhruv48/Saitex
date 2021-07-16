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
public partial class Module_Admin_Controls_News_Master : System.Web.UI.UserControl
{
    int iRecordFound = 0;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.CM_NEWS_MST oCM_NEWS_MST = new SaitexDM.Common.DataModel.CM_NEWS_MST();    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["LoginDetail"] != null)
        {

             oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
           // strTUser = oUserLoginDetail.UserCode;
            {
                if (!IsPostBack)
                {
                    Intilagepage();
                }
            }
        }

    }
    private void Intilagepage()
    {
        try
           {  
                    lblMode.Text = "Save"; 
                    GenrateRunningNo();
                    BindNewsGrid();
                     
                }
               
        catch (Exception ex)
        {
            throw;
        }

    }
    private void GenrateRunningNo()
    {
        try
        {
            int NewId  = SaitexBL.Interface.Method.CM_NEWS_MST.GenrateShortCodeDuplicasyCheck();
            txtNewsId.Text = (NewId + 1).ToString() ;
  
        }
        catch (Exception ex)

        {
            throw; 
        }
    
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        oCM_NEWS_MST.NEWS_HEAD = Common.CommonFuction.funFixScript(txtNewsHeading.Text.Trim());
        oCM_NEWS_MST.NEWS_DESC = txtNewsBody.Content.Trim();
        if (chkIshot.Checked)
        {
            oCM_NEWS_MST.IS_HOT = true;  
        }
        else
        {
            oCM_NEWS_MST.IS_HOT = false;
        }
        if (chk_Status.Checked)
        {
            oCM_NEWS_MST.IS_ACTIVE = true;
        }
        else
        {
            oCM_NEWS_MST.IS_ACTIVE = false;
        }
        oCM_NEWS_MST.TUSER = oUserLoginDetail.Username;   
        bool Result = SaitexBL.Interface.Method.CM_NEWS_MST.InsertNewsMaster(oCM_NEWS_MST, out iRecordFound); 
        if (Result)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' News Saved Successfully');", true);
            Intilagepage();

            RefreshConrols();
        }
        else if (iRecordFound > 0)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('This News Already Exist');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Some Problem in News Adding ');", true);
        }


    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        oCM_NEWS_MST.NEWS_ID = int.Parse(ViewState["editId"].ToString()); 
        oCM_NEWS_MST.NEWS_HEAD = Common.CommonFuction.funFixScript(txtNewsHeading.Text.Trim());
        oCM_NEWS_MST.NEWS_DESC = txtNewsBody.Content.Trim();
        if (chkIshot.Checked)
        {
            oCM_NEWS_MST.IS_HOT = true;
        }
        else
        {
            oCM_NEWS_MST.IS_HOT = false;
        }
        if (chk_Status.Checked)
        {
            oCM_NEWS_MST.IS_ACTIVE = true;
        }
        else
        {
            oCM_NEWS_MST.IS_ACTIVE = false;
        }
        oCM_NEWS_MST.TUSER = oUserLoginDetail.UserCode;   
        bool Result = SaitexBL.Interface.Method.CM_NEWS_MST.UpdateNewsMaster(oCM_NEWS_MST, out iRecordFound);
        if (Result)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' News Updated Successfully');", true);
            Intilagepage();
            RefreshConrols();
        }
        else if (iRecordFound > 0)
        {

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('This News Already Exist');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert(' Some Problem in News Adding ');", true);
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Intilagepage();
        RefreshConrols(); 
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgbtnExit_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Session["RedirectURL"] != null)
            {
                string test = Session["RedirectURL"].ToString();
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    private void BindNewsGrid()
    {
       DataTable dtNews = SaitexBL.Interface.Method.CM_NEWS_MST.GetNewMAster();
       if (dtNews != null && dtNews.Rows.Count > 0)
       {

           grdNews.DataSource = dtNews;
           grdNews.DataBind();
           ViewState["dtNewsMaster"] = dtNews;
           lblTotalRecord.Text = dtNews.Rows.Count.ToString() ;
       
       }
    
    
    }
    protected void grdNews_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdNews_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "ImageEdit")
        {
           
            ViewState["editId"] = e.CommandArgument.ToString().Trim();
            getAddModuleData(Convert.ToInt32(e.CommandArgument));

        }
        if (e.CommandName == "ImageDelete")
        {

            deleteNewsData(Convert.ToInt32(e.CommandArgument));
        }
    }
    private void getAddModuleData(int iAddNEWSId)
    {
        try
        {
            if (ViewState["dtNewsMaster"] != null)
            {
                DataTable dtNewsMaster = (DataTable)ViewState["dtNewsMaster"];
                DataView dv = new DataView(dtNewsMaster);
                dv.RowFilter = "NEWS_ID=" + iAddNEWSId;
                if (dv != null && dv.Count > 0)
                {
                    txtNewsId.Text = iAddNEWSId.ToString(); 
                    txtNewsHeading.Text = dv[0]["NEWS_HEAD"].ToString().Trim();
                    //txtNewsDescription.Text = dv[0]["NEWS_DESC"].ToString().Trim();
                    txtNewsBody.Content = dv[0]["NEWS_DESC"].ToString().Trim();
                    if (dv[0]["IS_ACTIVE"].ToString().Trim() == "1")
                    {
                        chk_Status.Checked = true;
                    }
                    if (dv[0]["IS_HOT"].ToString().Trim() == "1")
                    {
                        chkIshot.Checked = true;
                    }


                    lblMode.Text = "Update";
                    tdSave.Visible = false;
                    tdUpdate.Visible = true;
                }
            }


        }
        catch (Exception ex)
        {
            throw ex;
        }


    }
    private void deleteNewsData(int NewsId)
    {
        oCM_NEWS_MST.NEWS_ID = NewsId;
        oCM_NEWS_MST.TUSER = "strTUser";
        bool result = SaitexBL.Interface.Method.CM_NEWS_MST.DeleteNewsMaster(oCM_NEWS_MST);  
        if (result)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('News Deleted Successfully');", true);
            Intilagepage();
            RefreshConrols();
        }


    }
    private void RefreshConrols()
    {
        try
        {
            txtNewsHeading.Text = "";
            //txtNewsDescription.Text = "";
            chk_Status.Checked = false;
            chkIshot.Checked = false; 



        }
        catch (Exception ex)
        {
            throw;
        
        }
    
    }
    protected void grdNews_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblActive = (Label)e.Row.FindControl("lblActive");
                Label lblHot = (Label)e.Row.FindControl("lblHot");
                if (lblActive.Text == "1")
                {
                    lblActive.Text = "Active";
                }
                else
                {
                    lblActive.Text = "De-Active";
                }
                if (lblHot.Text == "1")
                {
                    lblHot.Text = "<B>HotNews</B>";
                    lblHot.ForeColor = System.Drawing.Color.Red;     
                }
                else
                {
                    lblHot.Text = "Normal News";
                }
            }
     }

}
