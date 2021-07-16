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
using errorLog;
using Common;
using DBLibrary;
using System.IO;
using Obout.ComboBox;
public partial class Module_HRMS_Controls_PromotionIncrement : System.Web.UI.UserControl
{

    SaitexDM.Common.DataModel.hr_promotionIncrement_mst ohr_promotionIncrement_mst;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = new SaitexDM.Common.DataModel.UserLoginDetail();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                bindddldesignation();
                bindddlempcode();
                Clear();
                MaxProid();
              
            }

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void bindddlempcode()
    {
        try
        {

            ddlemployee.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getempcode();

            if (dt != null && dt.Rows.Count > 0)
            {
                ddlemployee.DataValueField = "EMP_CODE";
                ddlemployee.DataTextField = "EMPLOYEENAME";
                ddlemployee.DataSource = dt;
                ddlemployee.DataBind();

            }
           
            ddlemployee.Items.Insert(0,new ListItem("---------Select----------", "")); 
        
        }
        catch
        {
            throw;
        }
    }
    private void MaxProid()
    {
        try
        {
            string x = string.Empty;
            int y = 0;

            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.GetMaxProid();
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
                        txtproid.Text = y.ToString();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    private void save()
    {
        try
        {
            int iRecordFound = 0;
            ohr_promotionIncrement_mst = new SaitexDM.Common.DataModel.hr_promotionIncrement_mst();
            ohr_promotionIncrement_mst.EMP_CODE = ddlemployee.SelectedValue.Trim();
            ohr_promotionIncrement_mst.Promotion_id = Common.CommonFuction.funFixQuotes(txtproid.Text.Trim());
            ohr_promotionIncrement_mst.PROMOTION_DESIG = ddldesig.SelectedValue.Trim();
            ohr_promotionIncrement_mst.Join_GrossSalary = Common.CommonFuction.funFixQuotes(txtjoinsalary.Text.Trim());
            ohr_promotionIncrement_mst.Current_GrossSalary = Common.CommonFuction.funFixQuotes(txtcurrentsalary.Text.Trim());
            ohr_promotionIncrement_mst.Last_performance = Common.CommonFuction.funFixQuotes(txtlastyearperform.Text.Trim());
            ohr_promotionIncrement_mst.Current_Performance = Common.CommonFuction.funFixQuotes(txtcurrentyearperform.Text.Trim());
            ohr_promotionIncrement_mst.Last_Rating = Common.CommonFuction.funFixQuotes(txtlastrating.Text.Trim());
            ohr_promotionIncrement_mst.Current_Rating = Common.CommonFuction.funFixQuotes(txtcurrentrating.Text.Trim());
            ohr_promotionIncrement_mst.Recommendation = Common.CommonFuction.funFixQuotes(txtrecomdation.Text.Trim());
            ohr_promotionIncrement_mst.Remarks = Common.CommonFuction.funFixQuotes(txtremarks.Text.Trim());

            bool bResult = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.InsertPromotionMaster(ohr_promotionIncrement_mst, out iRecordFound);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Save');", true);
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Save');", true);
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnSave_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            if (Page.IsValid)
            {
                save();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
    private void Clear()
    {
        try
        {
            GridEmp.Visible = false;
            ddlfind.Visible = false;
            ddldesig.SelectedIndex = 0;
            ddlemployee.Visible = true;
            ddlemployee.Enabled = true;
            txtproid.Enabled = false;
            ddlemployee.SelectedIndex = 0;
            MaxProid();
            tdSave.Visible = true;
            tdUpdate.Visible = false;
            tdDelete.Visible = false;
            lblMode.Text = "Save";
            txtjoinsalary.Text = string.Empty;
            txtcurrentsalary.Text = string.Empty;
            txtcurrentyearperform.Text = string.Empty;
            txtlastyearperform.Text = string.Empty;
            txtlastrating.Text = string.Empty;
            txtcurrentrating.Text = string.Empty;
            txtrecomdation.Text = string.Empty;
            txtremarks.Text = string.Empty;
            TxtDesignation.Text = string.Empty;
          
        }
        catch
        {
            throw;
        }

    }
    //private void find()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.GetGridData();

    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            DataView dv = new DataView(dt);
    //            TxtDesignation.Text = dt.Rows[0]["DESIG_NAME"].ToString();
    //            dv.RowFilter = "EMP_CODE='" + ddlemployee.SelectedValue.ToString().Trim() + "'";
    //            if (dv.Count > 0)
    //            {
    //                GridEmp.DataSource = dv;
    //                GridEmp.DataBind();
    //                tdUpdate.Visible = false;
    //                tdDelete.Visible = false;
    //                tdSave.Visible = true;

    //            }
    //            else
    //            {
    //                bindGrdEmp();
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    private void Update()
    {

        try
        {
            int iRecordFound = 0;
            ohr_promotionIncrement_mst = new SaitexDM.Common.DataModel.hr_promotionIncrement_mst();
            ohr_promotionIncrement_mst.EMP_CODE = ddlemployee.SelectedValue.Trim();
            ohr_promotionIncrement_mst.Promotion_id = Common.CommonFuction.funFixQuotes(txtproid.Text.Trim());
            ohr_promotionIncrement_mst.PROMOTION_DESIG = ddldesig.SelectedValue.Trim();
            ohr_promotionIncrement_mst.Join_GrossSalary = Common.CommonFuction.funFixQuotes(txtjoinsalary.Text.Trim());
            ohr_promotionIncrement_mst.Current_GrossSalary = Common.CommonFuction.funFixQuotes(txtcurrentsalary.Text.Trim());
            ohr_promotionIncrement_mst.Last_performance = Common.CommonFuction.funFixQuotes(txtlastyearperform.Text.Trim());
            ohr_promotionIncrement_mst.Current_Performance = Common.CommonFuction.funFixQuotes(txtcurrentyearperform.Text.Trim());
            ohr_promotionIncrement_mst.Last_Rating = Common.CommonFuction.funFixQuotes(txtlastrating.Text.Trim());
            ohr_promotionIncrement_mst.Current_Rating = Common.CommonFuction.funFixQuotes(txtcurrentrating.Text.Trim());
            ohr_promotionIncrement_mst.Recommendation = Common.CommonFuction.funFixQuotes(txtrecomdation.Text.Trim());
            ohr_promotionIncrement_mst.Remarks = Common.CommonFuction.funFixQuotes(txtremarks.Text.Trim());

            bool bResult = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.UpdatePromotionMaster(ohr_promotionIncrement_mst, out iRecordFound);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert(' Record Update');", true);
                Clear();
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Not Update');", true);
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void DeletePromotionMaster()
    {
        try
        {

            ohr_promotionIncrement_mst = new SaitexDM.Common.DataModel.hr_promotionIncrement_mst();
            ohr_promotionIncrement_mst.EMP_CODE = ddlemployee.SelectedValue.Trim();
            ohr_promotionIncrement_mst.Promotion_id = Common.CommonFuction.funFixQuotes(txtproid.Text.Trim());

            bool bResult = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.DeletePromotionMaster(ohr_promotionIncrement_mst);
            if (bResult)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('Record Delete Successfully');", true);
                Clear();

            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "25", "window.alert('No such record exits.! Pls enter valid Emp Code.');", true);
            }

        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

        try
        {
            DeletePromotionMaster();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
    private void bindGrdEmp()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.GetGridData();
            GridEmp.DataSource = dt;
            GridEmp.DataBind();
        }
        catch
        {
            throw;
        }
    }
    protected void GridEmp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridEmp.PageIndex = e.NewPageIndex;
            bindGrdEmp();

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in GridView Paging..\r\nSee error log for detail."));
        }
    }
    private void GridEmployee(int promotion_id)
    {
        try
        {

            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.GetGridData();

            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = " PROMOTION_ID='" + promotion_id + "'";
                //dv.RowFilter = " PROMOTION_ID='" + promotion_id ;

                if (dv.Count > 0)
                {
                    tdUpdate.Visible = true;
                    tdDelete.Visible = true;
                    tdSave.Visible = false;
                    ddlemployee.Enabled = false;
                    lblMode.Text = "Update";
                    for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                    {
                        TxtDesignation.Text = dv[iLoop]["DESIG_NAME"].ToString().Trim();
                        ddlemployee.SelectedValue = dv[iLoop]["EMP_CODE"].ToString().Trim();
                        txtproid.Text = dv[iLoop]["Promotion_id"].ToString().Trim();
                        ddldesig.SelectedValue = dv[iLoop]["PROMOTION_DESIG"].ToString().Trim();
                        txtjoinsalary.Text = dv[iLoop]["Join_GrossSalary"].ToString().Trim();
                        txtcurrentsalary.Text = dv[iLoop]["Current_GrossSalary"].ToString().Trim();
                        txtlastyearperform.Text = dv[iLoop]["Last_performance"].ToString().Trim();
                        txtcurrentyearperform.Text = dv[iLoop]["Current_Performance"].ToString().Trim();
                        txtlastrating.Text = dv[iLoop]["Last_Rating"].ToString().Trim();
                        txtcurrentrating.Text = dv[iLoop]["Current_Rating"].ToString().Trim();
                        txtrecomdation.Text = dv[iLoop]["Recommendation"].ToString().Trim();
                        txtremarks.Text = dv[iLoop]["Remarks"].ToString().Trim();
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void GridEmp_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "RecordEdit")
            {
                GridEmployee(Convert.ToInt32(e.CommandArgument));
                ViewState["RecordEdit"] = e.CommandArgument.ToString().Trim();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }



    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "../Pages/Promotion.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=800,height=400');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }

    }
    private void bindddldesignation()
    {
        try
        {
            ddldesig.SelectedIndex = 0;
            ddldesig.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.GetDesigcode();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddldesig.DataValueField = "DESIG_CODE";
                ddldesig.DataTextField = "DESIG_NAME";
                ddldesig.DataSource = dt;
                ddldesig.DataBind();

            }
            ddldesig.Items.Insert(0, new ListItem("---------Select----------", ""));
          
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    //protected void cmbEmpCode_SelectedIndexChanged1(object sender, ComboBoxItemEventArgs e)
    //{
    //    try
    //    {
    //        if (ddlemployee.SelectedIndex != 0)
    //        {
    //            find1(ddlemployee.SelectedValue.Trim().ToString());

    //            GetDesigName(ddlemployee.SelectedValue.ToString().Trim());
    //        }          
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
    //    }       
    //}
    private void find1(string EMP_CODE)
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.find(EMP_CODE);

            if (dt != null && dt.Rows.Count > 0)
            {
                TxtDesignation.Text = dt.Rows[0]["DESIG_NAME"].ToString().Trim();
                GridEmp.DataSource = dt;
                GridEmp.DataBind();
                GridEmp.Visible = true;
              
            }
            else
            {

               // bindGrdEmp();
            }

        }
        catch
        {
            throw;

        }

    }
    private void GetDesigName(string EMP_CODE)
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.getdesigname(EMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                TxtDesignation.Text = dt.Rows[0]["DESIG_NAME"].ToString().Trim();

            }


        }
        catch
        {
            throw;

        }

    }
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
        }

        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    private void bindddlfind()
    {
        try
        {
            ddlemployee.Visible = false;
            ddlfind.Visible = true;
            ddlfind.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.hr_PromotionIncrement_mst.Getfind();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlfind.DataValueField = "EMP_CODE";
                ddlfind.DataTextField = "EMPLOYEENAME";
                ddlfind.DataSource = dt;
                ddlfind.DataBind();
            }
            ddlfind.Items.Insert(0, new ListItem("---------Find----------",""));
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
            if (ddlfind.SelectedIndex != 0)
            {
                find1(ddlfind.SelectedValue.Trim().ToString());
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlemployee.SelectedIndex != 0)
            {
                GetDesigName(ddlemployee.SelectedValue.ToString().Trim());
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    protected void Imgbtnfind_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            bindddlfind();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Selecting Employeee.\r\nSee error log for detail."));
        }
    }
    
}

     

      

