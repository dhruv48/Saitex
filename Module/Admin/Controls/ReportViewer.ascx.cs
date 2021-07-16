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

public partial class Module_Admin_Controls_ReportViewer : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                BindTableFromDatabase();
                BindViewFromDatabase();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    #region First Page of Wizard
    #region code for tables

    private void BindTableFromDatabase()
    {
        try
        {
            DataTable dtTable = SaitexBL.Interface.Method.ReportCreator.GetAllTableFromDatabase("SAI");

            lstTablesAll.DataSource = dtTable;
            lstTablesAll.DataTextField = "TABLE_NAME";
            lstTablesAll.DataValueField = "TABLE_NAME";
            lstTablesAll.DataBind();
            lstTablesAll.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSelectTable_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstTablesAll.SelectedIndex != -1)
            {
                lstTablesSelected.Items.Add(new ListItem(lstTablesAll.SelectedItem.Text.Trim(), lstTablesAll.SelectedValue.Trim()));
                lstTablesAll.Items.RemoveAt(lstTablesAll.SelectedIndex);
                lstTablesAll.SelectedIndex = -1;
                lstTablesSelected.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in table selection.\r\nSee error log for detail."));
        }
    }

    protected void btnUnselectTable_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstTablesSelected.SelectedIndex != -1)
            {
                lstTablesAll.Items.Add(new ListItem(lstTablesSelected.SelectedItem.Text.Trim(), lstTablesSelected.SelectedValue.Trim()));
                lstTablesSelected.Items.RemoveAt(lstTablesSelected.SelectedIndex);
                lstTablesSelected.SelectedIndex = -1;
                lstTablesAll.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in table de-selection.\r\nSee error log for detail."));
        }
    }

    #endregion

    #region code for Views

    private void BindViewFromDatabase()
    {
        try
        {
            DataTable dtViews = SaitexBL.Interface.Method.ReportCreator.GetAllViewsFromDatabase("SAI");

            lstViewsAll.DataSource = dtViews;
            lstViewsAll.DataTextField = "VIEW_NAME";
            lstViewsAll.DataValueField = "VIEW_NAME";
            lstViewsAll.DataBind();
            lstTablesAll.SelectedIndex = -1;
        }
        catch
        {
            throw;
        }
    }

    protected void btnSelectViews_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstViewsAll.SelectedIndex != -1)
            {
                lstViewsSelected.Items.Add(new ListItem(lstViewsAll.SelectedItem.Text.Trim(), lstViewsAll.SelectedValue.Trim()));
                lstViewsAll.Items.RemoveAt(lstViewsAll.SelectedIndex);
                lstViewsAll.SelectedIndex = -1;
                lstViewsSelected.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in views selection.\r\nSee error log for detail."));
        }
    }

    protected void btnUnselectViews_Click(object sender, EventArgs e)
    {
        try
        {
            if (lstViewsSelected.SelectedIndex != -1)
            {
                lstViewsAll.Items.Add(new ListItem(lstViewsSelected.SelectedItem.Text.Trim(), lstViewsSelected.SelectedValue.Trim()));
                lstViewsSelected.Items.RemoveAt(lstViewsSelected.SelectedIndex);
                lstViewsSelected.SelectedIndex = -1;
                lstViewsAll.SelectedIndex = -1;
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Views de-selection.\r\nSee error log for detail."));
        }
    }

    #endregion
    #endregion

    #region Second Page of Wizard

    private void CreateWizardSecondPage()
    {
        try
        {
            ArrayList arReport = new ArrayList();

            ddlSelectTableForCol.Items.Clear();
            ddlSelectTableForCol.Items.Add(new ListItem("Select", "SELECT"));
            if (lstTablesSelected.Items.Count > 0)
            {
                foreach (ListItem lstitem in lstTablesSelected.Items)
                {
                    arReport.Add(lstitem.Text.Trim());
                    // ddlSelectTableForCol.Items.Add(lstitem);
                }
            }

            if (lstViewsSelected.Items.Count > 0)
            {
                foreach (ListItem lstitem in lstViewsSelected.Items)
                {
                    arReport.Add(lstitem.Text.Trim());
                    // ddlSelectTableForCol.Items.Add(lstitem);
                }
            }

            ddlSelectTableForCol.DataSource = arReport;
            ddlSelectTableForCol.DataBind();

            ViewState["arReport"] = arReport;
        }
        catch
        {
            throw;
        }
    }

    protected void ddlSelectTableForCol_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            FillDataTableFromnDataList();

            if (ddlSelectTableForCol.SelectedValue != "SELECT")
            {
                DataTable dtCol = SaitexBL.Interface.Method.ReportCreator.GetAllcolFromDatabaseByTable("SAI", ddlSelectTableForCol.SelectedValue.Trim());

                if (dtCol != null && dtCol.Rows.Count > 0)
                {
                    lstSelectCols.DataSource = dtCol;
                    lstSelectCols.DataTextField = "COLUMN_NAME";
                    lstSelectCols.DataValueField = "TABLE_NAME";
                    lstSelectCols.DataBind();
                }

            }

            BindDataList();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Table selection."));
        }
    }

    private DataTable CreateReportTable()
    {
        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("TABLE_NAME", typeof(string));
        dtReport.Columns.Add("COLUMN_NAME", typeof(string));
        dtReport.Columns.Add("HEADER_TEXT", typeof(string));
        dtReport.Columns.Add("FORMAT_STRING", typeof(string));
        dtReport.Columns.Add("IS_REPORT", typeof(bool));
        dtReport.Columns.Add("IS_FILTER", typeof(bool));
        dtReport.Columns.Add("IS_SORT", typeof(bool));

        return dtReport;
    }

    protected void btnSelectCols_Click(object sender, EventArgs e)
    {
        try
        {
            FillDataTableFromnDataList();

            if (lstSelectCols.SelectedIndex != -1)
            {

                DataTable dtReportCreator = new DataTable();
                if (ViewState["dtReportCreator"] != null)
                {
                    dtReportCreator = (DataTable)ViewState["dtReportCreator"];
                }
                else
                {
                    dtReportCreator = CreateReportTable();
                }

                string TableName = lstSelectCols.SelectedValue.Trim();
                string ColName = lstSelectCols.SelectedItem.Text.Trim();

                DataView dvFind = new DataView(dtReportCreator);
                dvFind.RowFilter = "TABLE_NAME='" + TableName + "' AND COLUMN_NAME='" + ColName + "'";

                if (dvFind.Count == 0)
                {
                    DataRow dr = dtReportCreator.NewRow();
                    dr["TABLE_NAME"] = TableName;
                    dr["COLUMN_NAME"] = ColName;

                    dr["HEADER_TEXT"] = string.Empty;
                    dr["FORMAT_STRING"] = string.Empty;
                    dr["IS_REPORT"] = false;
                    dr["IS_FILTER"] = false;
                    dr["IS_SORT"] = false;

                    dtReportCreator.Rows.Add(dr);
                }
                ViewState["dtReportCreator"] = dtReportCreator;

                lstSelectCols.Items.RemoveAt(lstSelectCols.SelectedIndex);
                lstSelectCols.SelectedIndex = -1;

                BindDataList();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in columns selection.\r\nSee error log for detail."));
        }
    }

    protected void dlSelectedCols_ItemCommand(object source, DataListCommandEventArgs e)
    {
        try
        {
            FillDataTableFromnDataList();

            DataListItem itemrow = (DataListItem)(((LinkButton)e.CommandSource).NamingContainer);
            Label lbldlColName = (Label)itemrow.FindControl("lbldlColName");

            string TableName = e.CommandArgument.ToString();
            string ColumnName = lbldlColName.Text.Trim();

            DataTable dtReportCreator = new DataTable();
            if (ViewState["dtReportCreator"] != null)
            {
                dtReportCreator = (DataTable)ViewState["dtReportCreator"];
            }
            else
            {
                dtReportCreator = CreateReportTable();
            }

            foreach (DataRow dr in dtReportCreator.Rows)
            {
                string TabName = dr["TABLE_NAME"].ToString();
                string ColName = dr["COLUMN_NAME"].ToString();

                if (TabName == TableName && ColName == ColumnName)
                {
                    dtReportCreator.Rows.Remove(dr);
                    break;
                }
            }

            lstSelectCols.Items.Add(new ListItem(ColumnName, TableName));
            BindDataList();
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in columns selection.\r\nSee error log for detail."));
        }
    }

    private void BindDataList()
    {
        try
        {
            DataTable dtReportCreator = new DataTable();
            if (ViewState["dtReportCreator"] != null)
            {
                dtReportCreator = (DataTable)ViewState["dtReportCreator"];
            }
            else
            {
                dtReportCreator = CreateReportTable();
            }

            if (dlSelectedCols != null)
            {
                DataView dvReport = new DataView(dtReportCreator);

                dvReport.RowFilter = "TABLE_NAME='" + ddlSelectTableForCol.SelectedValue.Trim() + "'";

                dlSelectedCols.DataSource = dvReport;
                dlSelectedCols.DataBind();
            }

        }
        catch
        {
            throw;
        }
    }

    private void FillDataTableFromnDataList()
    {
        try
        {
            if (dlSelectedCols.Items.Count > 0)
            {
                DataTable dtReportCreator = new DataTable();
                if (ViewState["dtReportCreator"] != null)
                {
                    dtReportCreator = (DataTable)ViewState["dtReportCreator"];
                }
                else
                {
                    dtReportCreator = CreateReportTable();
                }
                foreach (DataListItem dlitem in dlSelectedCols.Items)
                {
                    Label lbldlColName = (Label)dlitem.FindControl("lbldlColName");
                    TextBox txtdlHeaderName = (TextBox)dlitem.FindControl("txtdlHeaderName");
                    TextBox txtdlFormatString = (TextBox)dlitem.FindControl("txtdlFormatString");
                    CheckBox chkdlIs_report = (CheckBox)dlitem.FindControl("chkdlIs_report");
                    CheckBox chkdlIs_Filter = (CheckBox)dlitem.FindControl("chkdlIs_Filter");
                    CheckBox chkdlIs_sort = (CheckBox)dlitem.FindControl("chkdlIs_sort");
                    LinkButton lbtndlRemove = (LinkButton)dlitem.FindControl("lbtndlRemove");

                    DataView dvReport = new DataView(dtReportCreator);

                    dvReport.RowFilter = "TABLE_NAME='" + lbtndlRemove.CommandArgument.Trim() + "' AND COLUMN_NAME='" + lbldlColName.Text.Trim() + "'";

                    if (dvReport.Count > 0)
                    {

                        dvReport[0]["TABLE_NAME"] = lbtndlRemove.CommandArgument.Trim();
                        dvReport[0]["COLUMN_NAME"] = lbldlColName.Text.Trim();

                        dvReport[0]["HEADER_TEXT"] = txtdlHeaderName.Text.Trim();
                        dvReport[0]["FORMAT_STRING"] = txtdlFormatString.Text.Trim();
                        dvReport[0]["IS_REPORT"] = chkdlIs_report.Checked;
                        dvReport[0]["IS_FILTER"] = chkdlIs_Filter.Checked;
                        dvReport[0]["IS_SORT"] = chkdlIs_sort.Checked;

                        dtReportCreator.AcceptChanges();
                    }
                }
                ViewState["dtReportCreator"] = dtReportCreator;
            }
        }
        catch
        {
            throw;
        }
    }

    #endregion

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in leaving page.\r\nSee error log for detail."));
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }

    protected void wzReportPrepare_NextButtonClick(object sender, WizardNavigationEventArgs e)
    {
        try
        {
            if (e.CurrentStepIndex == 0)
            {
                if (lstTablesSelected.Items.Count == 0 && lstViewsSelected.Items.Count == 0)
                {
                    Common.CommonFuction.ShowMessage("Please select Table / Views first to move further.");

                }
                else
                {
                    CreateWizardSecondPage();
                }

            }
            else if (e.CurrentStepIndex == 1)
            {
                FillDataTableFromnDataList();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting next step.\r\nSee error log for detail."));
        }
    }

    protected void wzReportPrepare_FinishButtonClick(object sender, WizardNavigationEventArgs e)
    {
        try
        {
            FillDataTableFromnDataList();

            DataTable dtReportCreator = new DataTable();
            if (ViewState["dtReportCreator"] != null)
            {
                Session["dtReportCreator"] = (DataTable)ViewState["dtReportCreator"];
            }
            else
            {
                Session["dtReportCreator"] = CreateReportTable();
            }
            Session["arReport"] = (ArrayList)ViewState["arReport"];

            Server.Transfer("ReportPreview.aspx", true);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in finishing wizard.\r\nSee error log for detail."));
        }
    }
}
