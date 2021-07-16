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

public partial class Module_Admin_Controls_ReportPreview : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dtReportCreator = new DataTable();
        if (Session["dtReportCreator"] != null)
        {
            dtReportCreator = (DataTable)Session["dtReportCreator"];
            Session["dtReportCreator"] = null;
        }
        else
        {
            dtReportCreator = CreateReportTable();
        }
        ArrayList arReport = new ArrayList();

        if (Session["arReport"] != null)
        {
            arReport = (ArrayList)Session["arReport"];
            Session["arReport"] = null;
        }

        ViewState["arReport"] = arReport;

        ViewState["dtReportCreator"] = dtReportCreator;

        CreateReport();
    }

    protected override void OnInit(EventArgs e)
    {

        base.OnInit(e);
    }

    protected override void OnPreRender(EventArgs e)
    {

        base.OnPreRender(e);
    }

    #region CreateReport

    private void CreateReport()
    {
        try
        {
            grdReportPreview.Columns.Clear();
            DataTable dtReportCreator = new DataTable();
            if (ViewState["dtReportCreator"] != null)
            {
                dtReportCreator = (DataTable)ViewState["dtReportCreator"];
            }
            else
            {
                dtReportCreator = CreateReportTable();
            }

            ArrayList arReport = (ArrayList)ViewState["arReport"];
            if (arReport.Count > 0)
            {
                for (int i = 0; i < arReport.Count; i++)
                {
                    DataView dvReport = new DataView(dtReportCreator);
                    dvReport.RowFilter = "TABLE_NAME='" + arReport[i].ToString() + "'";

                    string SelectQuery = "Select ";
                    bool IsSecondSelect = false;
                    bool IsSecondOrderBy = false;
                    string SortString = "";
                    string whereClause = " where ";
                    if (dvReport.Count > 0)
                    {
                        Table tblFilter = CreateTable();
                        TableRow trFilterHead = CreateRow();
                        TableRow trFilterContent = CreateRow();
                        int iCount = 0;

                        for (int j = 0; j < dvReport.Count; j++)
                        {
                            string ColName = dvReport[j]["COLUMN_NAME"].ToString();
                            string DispName = dvReport[j]["HEADER_TEXT"].ToString();
                            string FormatString = dvReport[j]["FORMAT_STRING"].ToString();

                            if (bool.Parse(dvReport[j]["IS_REPORT"].ToString()) == true)
                            {
                                if (IsSecondSelect)
                                {
                                    SelectQuery += ", ";
                                }
                                else
                                {
                                    whereClause += " " + ColName + " like :SearchQuery";
                                }

                                if (DispName == string.Empty)
                                    DispName = ColName;

                                grdReportPreview.Columns.Add(CreateBoundField(ColName, DispName, false, FormatString, true, false));

                                SelectQuery += ColName + "";
                                IsSecondSelect = true;
                            }

                            if (bool.Parse(dvReport[j]["IS_SORT"].ToString()) == true)
                            {
                                if (!IsSecondOrderBy)
                                {
                                    SortString += " order by ";
                                }
                                else
                                {
                                    SortString += ", ";
                                }
                                SortString += ColName + " ASC";
                                IsSecondOrderBy = true;
                            }

                            if (bool.Parse(dvReport[j]["IS_FILTER"].ToString()) == true)
                            {
                                iCount += 1;
                                Label lblHead = new Label();
                                lblHead.ToolTip = ColName;
                                lblHead.ID = "lbl" + iCount;
                                lblHead.Text = "Enter " + DispName;
                                lblHead.Font.Bold = true;

                                TableCell tcHead = new TableCell();
                                tcHead.VerticalAlign = VerticalAlign.Top;
                                tcHead.HorizontalAlign = HorizontalAlign.Left;
                                tcHead.Controls.Add(lblHead);
                                trFilterHead.Controls.Add(tcHead);

                                TextBox txtContent = new TextBox();
                                txtContent.ToolTip = ColName;
                                txtContent.ID = "txt" + iCount;
                                txtContent.Width = Unit.Pixel(150);

                                TableCell tcContent = new TableCell();
                                tcContent.VerticalAlign = VerticalAlign.Top;
                                tcContent.HorizontalAlign = HorizontalAlign.Left;
                                tcContent.Controls.Add(txtContent);
                                trFilterContent.Controls.Add(tcContent);
                            }

                        }


                        TableCell tcHead1 = new TableCell();
                        tcHead1.VerticalAlign = VerticalAlign.Top;
                        tcHead1.HorizontalAlign = HorizontalAlign.Left;
                        trFilterHead.Controls.Add(tcHead1);

                        TableCell tcContent1 = new TableCell();
                        tcContent1.VerticalAlign = VerticalAlign.Top;
                        tcContent1.HorizontalAlign = HorizontalAlign.Left;
                        Button btnFilter = new Button();
                        btnFilter.ID = "btnFilter";
                        btnFilter.Text = "Search";
                        btnFilter.Attributes.Add("runat", "server");
                        tcContent1.Controls.Add(btnFilter);
                        btnFilter.Click += new EventHandler(btnFilter_Click);
                        trFilterContent.Controls.Add(tcContent1);

                        tblFilter.Controls.Add(trFilterHead);
                        tblFilter.Controls.Add(trFilterContent);
                        tdReportPreviewFilter.Controls.Add(tblFilter);
                    }
                    SelectQuery += " From " + arReport[i].ToString();

                    lblSelectQuery.Text = SelectQuery;
                    lblWhereQuery.Text = whereClause;
                    lblSortQuery.Text = SortString;
                    BindGrid();
                }
            }

        }
        catch
        {
            throw;
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

    protected void btnFilter_Click(object sender, EventArgs e)
    {
        try
        {
            int TotalControls = tdReportPreviewFilter.Controls.Count;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in searching.\r\nSee error log for detail."));

        }
    }

    private void BindGrid()
    {
        try
        {

            string SearchQuery = "%";
            DataTable dtReportPreview = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(lblSelectQuery.Text, lblWhereQuery.Text, lblSortQuery.Text, "", SearchQuery, "");

            if (dtReportPreview != null && dtReportPreview.Rows.Count > 0)
            {
                grdReportPreview.DataSource = dtReportPreview;
                grdReportPreview.DataBind();
            }
            else
            {
                Common.CommonFuction.ShowMessage("No Data found.");
            }
        }
        catch
        {
            throw;
        }

    }

    private BoundField CreateBoundField(string DataField, string HeaderText, bool HTMLEncode, string DataFormatString, bool Visible, bool isNumeric)
    {
        try
        {
            BoundField boundField = new BoundField();
            boundField.DataField = DataField;
            boundField.HeaderText = HeaderText;
            boundField.HtmlEncode = HTMLEncode;

            TableItemStyle tt = new TableItemStyle();
            if (isNumeric)
                boundField.HeaderStyle.HorizontalAlign = HorizontalAlign.Right;
            else
                boundField.HeaderStyle.HorizontalAlign = HorizontalAlign.Left;

            boundField.Visible = Visible;
            if (DataFormatString != "")
                boundField.DataFormatString = DataFormatString;
            return boundField;
        }
        catch
        {
            throw;
        }
    }

    private Table CreateTable()
    {
        Table tbl = new Table();
        tbl.Visible = true;
        tbl.CellPadding = 1;
        tbl.CellSpacing = 1;
        //tbl.BorderWidth = 1;
        return tbl;
    }

    private TableRow CreateRow()
    {
        TableRow tr = new TableRow();
        tr.Visible = true;
        return tr;
    }

    #endregion

    protected void grdReportPreview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CreateReport();

        BindGrid();
        grdReportPreview.PageIndex = e.NewPageIndex;
        grdReportPreview.Visible = true;
        pnlReportPreview.Visible = true;
    }
}
