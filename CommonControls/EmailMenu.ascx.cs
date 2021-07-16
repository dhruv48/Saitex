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
using SaitexBL.Interface.Method.Mail;

public partial class CommonControls_EmailMenu : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                GetFolderName();
            }
        }
        catch 
        {
            throw;
        }
    }
    protected void lbtnMailExit_Click(object sender, EventArgs e)
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

    private void GetFolderName()
    {
        try
        {
            Table tbl = CreateTable();

            SaitexDM.Common.DataModel.Mail.MAIL_FOLDER oMAIL_FOLDER = new SaitexDM.Common.DataModel.Mail.MAIL_FOLDER();
            oMAIL_FOLDER.USER_CODE = oUserLoginDetail.UserCode;
            DataTable dtFolderList = MAIL_FOLDER.GetFolderNameByUser(oMAIL_FOLDER);
            if (dtFolderList != null && dtFolderList.Rows.Count > 0)
            {
                for (int iLoop = 0; iLoop < dtFolderList.Rows.Count; iLoop++)
                {
                    #region code to create new link button for folder
                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtn" + (iLoop + 1);
                    lbtn.Text = "&nbsp;&nbsp;&nbsp;&nbsp;" + dtFolderList.Rows[iLoop]["FOLDER_NAME"].ToString();
                    lbtn.PostBackUrl = "~/Module/Mail/Pages/ReadMail.aspx?FOLDER_NAME=" + dtFolderList.Rows[iLoop]["FOLDER_NAME"].ToString();
                    lbtn.Font.Name = "Arial";
                    lbtn.Font.Size = FontUnit.Point(8);
                    lbtn.ForeColor = System.Drawing.Color.FromArgb(52, 103, 153);
                    lbtn.CommandArgument = dtFolderList.Rows[iLoop]["FOLDER_NAME"].ToString();
                    lbtn.Visible = true;
                    lbtn.ToolTip = dtFolderList.Rows[iLoop]["FOLDER_NAME"].ToString();
                    #endregion
                    TableCell tc = CreateCell();
                    tc.Controls.Add(lbtn);

                    TableRow tr = CreateRow();
                    tr.Controls.Add(tc);

                    TableRow trLineBar = CreateLinebarRow();
                    tbl.Controls.Add(trLineBar);
                    tbl.Controls.Add(tr);
                }
            }
            else
            {
                Label lbtn = new Label();
                lbtn.ID = "lbl001";
                lbtn.Text = "&nbsp;&nbsp;&nbsp;&nbsp;No Folder Exists";
                lbtn.Font.Name = "Arial";
                lbtn.Font.Size = FontUnit.Point(8);
                lbtn.ForeColor = System.Drawing.Color.FromArgb(52, 103, 153);
                lbtn.Visible = true;
                lbtn.ToolTip = "No Folder Exists";

                TableCell tc = CreateCell();
                tc.Controls.Add(lbtn);

                TableRow tr = CreateRow();
                tr.Controls.Add(tc);

                TableRow trLineBar = CreateLinebarRow();
                tbl.Controls.Add(trLineBar);
                tbl.Controls.Add(tr);
            }
            TableRow trLineBar1 = CreateLinebarRow();
            tbl.Controls.Add(trLineBar1);

            pnlFolderList.Controls.Add(tbl);
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
        tbl.CellPadding = 2;
        tbl.CellSpacing = 2;
        return tbl;
    }
    private TableRow CreateRow()
    {
        TableRow tr = new TableRow();
        tr.Visible = true;
        tr.Height = Unit.Pixel(13);
        return tr;
    }
    private TableCell CreateCell()
    {
        TableCell tc = new TableCell();
        tc.Height = Unit.Pixel(13);
        tc.HorizontalAlign = HorizontalAlign.Left;
        tc.VerticalAlign = VerticalAlign.Top;
        return tc;
    }
    private TableCell CreateCellDOT()
    {
        TableCell tc = new TableCell();
        tc.Height = Unit.Pixel(13);
        tc.HorizontalAlign = HorizontalAlign.Left;
        tc.VerticalAlign = VerticalAlign.Top;
        tc.Width = Unit.Pixel(10);
        tc.Font.Bold = true;
        tc.Font.Size = FontUnit.Large;
        tc.Text = "";
        return tc;
    }
    private TableRow CreateLinebarRow()
    {
        TableCell tcLineBar = CreateCell();
        Image imgLineBar = new Image();
        imgLineBar.Width = Unit.Percentage(100);
        imgLineBar.ImageUrl = "~/CommonImages/linebar.jpg";
        tcLineBar.ColumnSpan = 2;
        tcLineBar.Height = Unit.Pixel(1);
        tcLineBar.Controls.Add(imgLineBar);
        TableRow trLineBar = CreateRow();
        trLineBar.Height = Unit.Pixel(1);
        trLineBar.Controls.Add(tcLineBar);
        return trLineBar;
    }
}
