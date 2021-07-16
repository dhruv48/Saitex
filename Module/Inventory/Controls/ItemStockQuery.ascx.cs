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
public partial class Module_Inventory_Controls_ItemStockQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    public static string YEAR = string.Empty;
    public static string ITEM_CODE = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
         
            if (!IsPostBack)
            {
                BindGrid();
                bindddlitemtype();
                bindddltem();
                bindddldept();
            
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
        }
    }
      private void BindGrid()
        {    
            try
            {
                string item = string.Empty;
                string itemtype = string.Empty;
                string department =string .Empty ;

                if (ddldepartment.SelectedValue.ToString() != null && ddldepartment.SelectedValue.ToString() != string.Empty)
                {
                    department = ddldepartment.SelectedValue.ToString();
                }
                else
                {
                    department = string.Empty;
                }
              
                if (ddlitem.SelectedValue.ToString() != null && ddlitem.SelectedValue.ToString() != string.Empty)
                {
                    item = ddlitem.SelectedValue.ToString();
                }
                else
                {
                    item = string.Empty;
                }
                if (ddlitemtype.SelectedValue.ToString() != null && ddlitemtype.SelectedValue.ToString() != string.Empty)
                {
                    itemtype = ddlitemtype.SelectedValue.ToString();
                }
                else
                {
                    itemtype = string.Empty;
                }

                DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemStockQuery(oUserLoginDetail.CH_BRANCHCODE, department, item, itemtype);
                if (dt != null && dt.Rows.Count > 0)
                {
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null ;
                    GridView1.DataBind();
                    Common.CommonFuction.ShowMessage("Data not found by selected item.");
                }
            }
            catch (Exception ex)
            {
                errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
                throw ex;
            }
        }
      protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
       {
           try
           {

               if (e.Row.RowType == DataControlRowType.DataRow)
               {
                   GridViewRow row = e.Row;
                   GridView grdvwitemissue = (GridView)row.FindControl("grdvwitemissue");
                   GridView grdvwitemreceive = (GridView)row.FindControl("grdvwitemreceive");
                   Label lblyear = (Label)e.Row.FindControl("lblYEAR");
                   Label lblitemcode = (Label)e.Row.FindControl("lblITEM_CODE");
                   YEAR = lblyear.Text;
                   ITEM_CODE = lblitemcode.Text;

            
                   DataTable dtitemissue = SaitexBL.Interface.Method.TX_ITEM_MST.Getytdissue(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, YEAR, ITEM_CODE);
                   if (dtitemissue != null && dtitemissue.Rows.Count > 0)
                   {
                       grdvwitemissue.DataSource = dtitemissue;
                       grdvwitemissue.DataBind();
                   }
                   DataTable dtitemreceive = SaitexBL.Interface.Method.TX_ITEM_MST.Getytdreceive(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, YEAR, ITEM_CODE);
                   if (dtitemreceive != null && dtitemreceive.Rows.Count > 0)
                   {
                       grdvwitemreceive.DataSource = dtitemreceive;
                       grdvwitemreceive.DataBind();
                   }
               }
           }
           catch (Exception ex)
           {
               errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
               throw ex;
           }
      }
      protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
      {
          try
          {
              ddldepartment.SelectedIndex = -1;
              ddlitem.SelectedIndex = -1;
              ddlitemtype.SelectedIndex = -1;
              BindGrid();
          }
          catch
          {
              throw;
          }

      }
      protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
      {
          GridView1.PageIndex = e.NewPageIndex;
          BindGrid();
      }
      private void bindddlitemtype()
      {
          
          try 
          {
          
              ddlitemtype.Items.Clear();
            
              DataTable dt = SaitexBL.Interface.Method.TX_ITEM_MST.GetItemType();
              if (dt != null && dt.Rows.Count > 0)
              {
                  ddlitemtype.DataTextField = "ITEM_TYPE";
                  ddlitemtype.DataValueField = "ITEM_TYPE";
                  ddlitemtype.DataSource = dt;
                  ddlitemtype.DataBind();

              }
              ddlitemtype.Items.Insert(0, new ListItem("SELECT", ""));
          }
          catch
          {
              throw;
          }

      }
      private void bindddltem()
      {
          try
          {
              ddlitem.Items.Clear();
              DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.getitemcode();
              if (dt != null && dt.Rows.Count > 0)
              {
                  ddlitem.DataTextField = "ITEM_DESC";
                  ddlitem.DataValueField = "ITEM_CODE";
                  ddlitem.DataSource = dt;
                  ddlitem.DataBind();

              }
              ddlitem.Items.Insert(0, new ListItem("SELECT", ""));
          }
          catch
          {
              throw;
          }
      }
      private void bindddldept()
      { 
          try
          {
              ddldepartment.Items.Clear();
              //DataTable dt = SaitexBL.Interface.Method.TX_ITEM_IR_MST.getitemcode();
              DataTable dt = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
              if (dt != null && dt.Rows.Count > 0)
              {
                  ddldepartment.DataTextField = "DEPT_NAME";
                  ddldepartment.DataValueField = "DEPT_CODE";
                  ddldepartment.DataSource = dt;
                  ddldepartment.DataBind();

              }
              ddldepartment.Items.Insert(0, new ListItem("SELECT", ""));
          }
          catch
          {
              throw;
          }
      }
      protected void btnview_Click(object sender, EventArgs e)
      {
          try
          {
              BindGrid();
          }
          catch (Exception ex)
          {
              CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in page loading.\r\nplease see error log"));
          }
      }
      protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
      {

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
              CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\n See error log for detail."));
          }
      }
}