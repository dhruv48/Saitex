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

using Common;
using errorLog;
using System.IO;
using DBLibrary;

public partial class Module_HRMS_Controls_SalaryDetails : System.Web.UI.UserControl
{
    string strTUser = string.Empty;
    private static string StrGrade = string.Empty;

    private static int iSubHeadId = 0;
    private static string strAmount = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
                strTUser = oUserLoginDetail.LOGINDETAILID;
                if (!IsPostBack)
                {
                    bindGradeMaster();
                }
            }
            else
            {
                Response.Redirect("/Saitex/default.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Loading"));
        }
    }
    private void bindGradeMaster()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetGradeMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlGrade.DataSource = dt;
                ddlGrade.DataTextField = "MST_DESC";
                ddlGrade.DataValueField = "MST_CODE";
                ddlGrade.DataBind();
                ddlGrade.Items.Insert(0, "------------Select Grade------------");
            }
        }
        catch
        {
            throw;
        }
    }
    protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            StrGrade = ddlGrade.SelectedValue.Trim().ToString();
            if (StrGrade != "0")
            {
                bindSalaryHeadMaster();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Salary Binding"));
        }
    }
    private void bindSalaryHeadMaster()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetHeadMaster();

            if (dt != null && dt.Rows.Count > 0)
            {
                gvSalaryGrade.DataSource = dt;
                gvSalaryGrade.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }
    protected void gvSalaryGrade_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label HeadId = (Label)e.Row.FindControl("lblHeadId");
                GridView gvSubHead = (GridView)e.Row.FindControl("gvSubHead");
                DataTable dt = SaitexBL.Interface.Method.HR_SAL_GRD.GetSubHeadMaster(StrGrade);
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    dv.RowFilter = "HEAD_ID='" + Convert.ToInt16(HeadId.Text.Trim()) + "'";
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            gvSubHead.DataSource = dv;
                            gvSubHead.DataBind();
                        }
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }
    protected void gvSubHead_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ((TextBox)e.Row.FindControl("txtDefaultValue")).Attributes.Add("onkeyup", "javascript:pricevalidate(this);");
            }
        }
        catch (Exception ex)
        {
            ErrHandler.WriteError(ex.Message);
        }
    }

    private bool Insert_Record()
    {
        SaitexDM.Common.DataModel.HR_SAL_GRD oHR_SAL_GRD = new SaitexDM.Common.DataModel.HR_SAL_GRD();
        bool bResult = false;
        try
        {
            if (Page.IsValid)
            {
                foreach (GridViewRow rw in gvSalaryGrade.Rows)
                {
                    Label lblHeadId = (Label)rw.FindControl("lblHeadId");
                    GridView gvSubHead = (GridView)rw.FindControl("gvSubHead");
                    foreach (GridViewRow rw1 in gvSubHead.Rows)
                    {
                        Label lblSubHeadId = (Label)rw1.FindControl("lblSubHeadId");
                        TextBox txtDefaultValue = (TextBox)rw1.FindControl("txtDefaultValue");
                        int IN_HEADID, IN_SUBHEADNAMEMASTER;
                        float ft_Amount = 0;
                        IN_HEADID = Convert.ToInt32(lblHeadId.Text.Trim());
                        IN_SUBHEADNAMEMASTER = Convert.ToInt32(lblSubHeadId.Text.Trim());
                        ft_Amount = float.Parse(txtDefaultValue.Text.Trim());
                        oHR_SAL_GRD.GRADE_ID = ddlGrade.SelectedValue.ToString().Trim();
                        oHR_SAL_GRD.HEAD_ID = IN_HEADID;
                        oHR_SAL_GRD.SUBH_ID = IN_SUBHEADNAMEMASTER;
                        oHR_SAL_GRD.SAL_GRD_AMT = ft_Amount;
                        oHR_SAL_GRD.TUSER = strTUser;
                        bResult = SaitexBL.Interface.Method.HR_SAL_GRD.InsertSalaryGrade(oHR_SAL_GRD);

                    }
                }
            }
            return bResult;
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
            if (Insert_Record())
            {
                Common.CommonFuction.ShowMessage("Record Update Sucessfully");
            }
            else
            {
                Common.CommonFuction.ShowMessage("Problem in Record Update");
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Updating Data"));
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            iSubHeadId = 0;
            Server.Transfer("./SalaryDetails.aspx");
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Clear the Records"));
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string URL = "SalaryDetails_OPT.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','toolbar=no,location=no,directories=no,status=no,scrollbars=yes,resizable=yes,menubar=no,width=600,height=600');", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Salary Printing"));
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
                Response.Redirect("~/Admin/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, "Problem in Page Exit"));
        }
    }
}