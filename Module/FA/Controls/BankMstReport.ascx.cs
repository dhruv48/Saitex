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
using System.IO;

public partial class Module_FA_Controls_BankMstReport : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                InitialisePage();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    private void InitialisePage()
    {
        try
        {
            lblMode.Text = "You are in Print Mode";
            BindBank();
        }
        catch
        {
            throw;
        }
    }

    private void BindBank()
    {
        try
        {
            cmbBankCode.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.FA_BANK_MST.GetBankMaster(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                cmbBankCode.DataTextField = "LGR_BANK_NAME";
                cmbBankCode.DataValueField = "LGR_BANK_CODE";
                cmbBankCode.DataSource = dt;
                cmbBankCode.DataBind();
            }
        }
        catch
        {
            throw;
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;
            myDataColumn = new DataColumn();
            myDataColumn.DataType = Type.GetType("System.String");
            myDataColumn.ColumnName = "LGR_BANK_CODE";
            myDataTable.Columns.Add(myDataColumn);
            DataRow row;
            row = myDataTable.NewRow();
            row["LGR_BANK_CODE"] = cmbBankCode.SelectedValue.ToString();

            if (cmbBankCode.SelectedValue.ToString() != null && cmbBankCode.SelectedValue.ToString() != string.Empty)
            {
                row["LGR_BANK_CODE"] = cmbBankCode.SelectedValue.ToString();
            }
            else
            {
                row["LGR_BANK_CODE"] = string.Empty;
            }
            myDataTable.Rows.Add(row);
            Session["bankreport"] = myDataTable;

            string URL = "../Reports/FA_Bank_Mst_Rpt.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Printing..\r\nSee error log for detail."));

        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./BankMstReport.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }
}