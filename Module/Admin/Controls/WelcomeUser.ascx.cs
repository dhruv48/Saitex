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

public partial class Admin_UserControls_WelcomeUser : System.Web.UI.UserControl
{
    private static string RedirectURL;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            RedirectURL = "";
            GetData();
            if (Session["ActiveTabIndex"] != null)
                tcWelcome.ActiveTabIndex = int.Parse(Session["ActiveTabIndex"].ToString());
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    private void RedirectPage()
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
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void GetData()
    {
        tcWelcome.Visible = false;
        if (Session["urLoginId"] != null)
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            SaitexDM.Common.DataModel.UserAccessRight oUserAccessRight = new SaitexDM.Common.DataModel.UserAccessRight();
            oUserAccessRight.UserCode = Session["urLoginId"].ToString();
            DataTable dtUserMenu;
            if (oUserLoginDetail.UserType == "SA")
            {
                dtUserMenu = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightForSuperAdmin(oUserAccessRight);
            }
            else
            {
                dtUserMenu = SaitexBL.Interface.Method.UserNavigationRight.GetUserNavigationRightByUserCode(oUserAccessRight);
            }
            if (dtUserMenu != null && dtUserMenu.Rows.Count > 0)
            {
                int iModuleId = 0;
                int iChildModuleId = 0;
                string RowFilter = "";
                bool bRURL = false;
                RedirectURL = "~/Module/Admin/Pages/Welcome.aspx";
                DataTable dtNavigation = SaitexBL.Interface.Method.UserNavigationRight.GetNavigationMaster();
                if (Request.QueryString["ModuleId"] != null && Request.QueryString["ModuleId"].ToString().Trim() != "")
                {
                    iModuleId = Convert.ToInt32(Request.QueryString["ModuleId"].ToString().Trim());
                    RowFilter = RowFilter + "MDL_ID=" + iModuleId;
                    if (bRURL)
                    {
                        RedirectURL = RedirectURL + "&";
                    }
                    else
                    {
                        bRURL = true;
                        RedirectURL = RedirectURL + "?";
                    }
                    RedirectURL = RedirectURL + "ModuleId=" + iModuleId;

                    UserControl GetUserMenu1 = (UserControl)this.Page.Master.FindControl("GetUserMenu1");
                    if (GetUserMenu1 != null)
                    {
                        AjaxControlToolkit.CollapsiblePanelExtender oCollapse = (AjaxControlToolkit.CollapsiblePanelExtender)GetUserMenu1.FindControl("Collapse" + iModuleId);
                        oCollapse.Collapsed = false;
                        Session["Collapse"] = oCollapse;
                    }

                }
                if (Request.QueryString["ChildModuleId"] != null && Request.QueryString["ChildModuleId"].ToString().Trim() != "")
                {
                    iChildModuleId = Convert.ToInt32(Request.QueryString["ChildModuleId"].ToString().Trim());
                    if (RowFilter != "")
                    {
                        RowFilter = RowFilter + " and ";
                    }
                    if (bRURL)
                    {
                        RedirectURL = RedirectURL + "&";
                    }
                    else
                    {
                        bRURL = true;
                        RedirectURL = RedirectURL + "?";
                    }
                    RedirectURL = RedirectURL + "ChildModuleId=" + iChildModuleId;
                    RowFilter = RowFilter + "CHILD_MDL_ID=" + iChildModuleId;
                }
                if (bRURL)
                {
                    Session["RedirectURL"] = RedirectURL;
                    divDashBoard.Visible = false;
                    divNavLink.Visible = true;
                }
                else
                {
                    divDashBoard.Visible = true;
                    divNavLink.Visible = false;
                    Session["RedirectURL"] = null;
                }
                DataView dvNav = new DataView(dtUserMenu);
                if (RowFilter != "")
                    dvNav.RowFilter = RowFilter;

                if (dvNav.Count > 0)
                {
                    Hashtable ht1 = new Hashtable();
                    Hashtable ht2 = new Hashtable();
                    Hashtable ht3 = new Hashtable();
                    Hashtable ht4 = new Hashtable();
                    Hashtable ht5 = new Hashtable();

                    for (int iLoop = 0; iLoop < dvNav.Count; iLoop++)
                    {
                        string NavigationName = dvNav[iLoop]["NAV_NAME"].ToString();
                        int NavigationId = Convert.ToInt32(dvNav[iLoop]["NAV_ID"].ToString());

                        DataView dvNavlnk = new DataView(dtNavigation);
                        dvNavlnk.RowFilter = "MDL_ID=" + iModuleId + " and CHILD_MDL_ID=" + iChildModuleId + " and NAV_ID=" + NavigationId;
                        if (dvNavlnk.Count > 0)
                        {
                            ImageButton imgbtn = new ImageButton();
                            imgbtn.ID = "imgbtn" + NavigationId;
                            imgbtn.ToolTip = NavigationName;

                            imgbtn.ImageUrl = "~/Module/Admin/ShowImage.aspx?NAV_ID=" + NavigationId + "&ilen=" + dvNavlnk[0]["POSTED_LENGTH"].ToString();
                            //  imgbtn.ImageUrl = "~/" + dvNavlnk[0]["IMAGEURL"].ToString();
                            imgbtn.CommandArgument = dvNavlnk[0]["Nav_URL"].ToString();
                            imgbtn.PostBackUrl = dvNavlnk[0]["Nav_URL"].ToString();
                            imgbtn.ToolTip = "Quick Id :" + dvNavlnk[0]["NAV_ID"].ToString();
                            imgbtn.Height = Unit.Pixel(25);
                            imgbtn.Width = Unit.Pixel(25);
                            imgbtn.Visible = true;

                            HyperLink hpl = new HyperLink();
                            hpl.ID = "hpl" + NavigationId;
                            hpl.ToolTip = "Quick Id :" + dvNavlnk[0]["NAV_ID"].ToString();
                            hpl.Text = NavigationName;
                            hpl.NavigateUrl = dvNavlnk[0]["Nav_URL"].ToString();
                            hpl.Visible = true;
                            hpl.Font.Bold = false;
                            hpl.Font.Size = FontUnit.Point(9);
                            hpl.Visible = true;
                            hpl.CssClass = "WelcomeItem_Image";

                            Table tblNew = CreateTable();
                            TableRow trnew = CreateRow();
                            TableRow trnews = CreateRow();
                            TableCell tc1 = new TableCell();
                            //tc1.Width = Unit.Pixel(27);
                            tc1.Height = Unit.Pixel(27);
                            tc1.VerticalAlign = VerticalAlign.Top;
                            tc1.HorizontalAlign = HorizontalAlign.Center;
                            tc1.Controls.Add(imgbtn);
                            TableCell tc2 = new TableCell();
                            tc2.Width = Unit.Pixel(80);
                            tc2.VerticalAlign = VerticalAlign.Top;
                            tc2.HorizontalAlign = HorizontalAlign.Center;
                            tc2.Controls.Add(hpl);
                            trnew.VerticalAlign = VerticalAlign.Top;
                            trnew.HorizontalAlign = HorizontalAlign.Center;
                            trnews.VerticalAlign = VerticalAlign.Top;
                            trnews.HorizontalAlign = HorizontalAlign.Center;
                            trnew.Controls.Add(tc1);
                            trnews.Controls.Add(tc2);

                            tblNew.HorizontalAlign = HorizontalAlign.Center;
                            tblNew.Controls.Add(trnew);
                            tblNew.Controls.Add(trnews);

                            string TabId = dvNavlnk[0]["TAB_ID"].ToString();
                            if (TabId == "1")
                            {
                                ht1.Add(ht1.Count + 1, tblNew);
                            }
                            else if (TabId == "2")
                            {
                                ht2.Add(ht2.Count + 1, tblNew);
                            }
                            else if (TabId == "3")
                            {
                                ht3.Add(ht3.Count + 1, tblNew);
                            }
                            else if (TabId == "4")
                            {
                                ht4.Add(ht4.Count + 1, tblNew);
                            }
                            else if (TabId == "5")
                            {
                                ht5.Add(ht5.Count + 1, tblNew);
                            }
                        }
                    }
                    if (ht1.Count > 0)
                    {
                        GenerateNavigation(ht1, Panel1);
                    }
                    if (ht2.Count > 0)
                    {
                        GenerateNavigation(ht2, Panel2);
                    }
                    if (ht3.Count > 0)
                    {
                        GenerateNavigation(ht3, Panel3);
                    }
                    if (ht4.Count > 0)
                    {
                        GenerateNavigation(ht4, Panel4);
                    }
                    if (ht5.Count > 0)
                    {
                        GenerateNavigation(ht5, Panel5);
                    }
                }
            }
        }
        else
        {
            Session.Abandon();
            Response.Redirect("~/Default.aspx", false);

        }
    }
    private void GenerateNavigation(Hashtable ht, Panel pnl)
    {
        Table tbl = CreateTable();

        // adding blank row to table
        TableRow trnn = CreateRow();
        TableCell tcc = new TableCell();
        tcc.Height = Unit.Pixel(20);
        trnn.Controls.Add(tcc);
        tbl.Controls.Add(trnn);

        TableRow tr = CreateRow();

        int iColClount = 0;
        for (int iLoop = 0; iLoop < ht.Count; iLoop++)
        {
            iColClount = iColClount + 1;
            if (iColClount == 1)
            {
                tr = CreateRow();
                // adding blank cell to row
                TableCell tc = new TableCell();
                tc.Width = Unit.Pixel(10);
                tr.Controls.Add(tc);
            }
            for (int jLoop = 0; jLoop < ht.Count; jLoop++)
            {
                if (iLoop == jLoop)
                {
                    TableCell tc = new TableCell();
                    tc.Controls.Add(ht[iLoop + 1] as Table);
                    tr.Controls.Add(tc);

                    // adding blank cell to row
                    tc = new TableCell();
                    tc.Width = Unit.Pixel(10);
                    tr.Controls.Add(tc);
                }
            }
            if (iColClount == 1)
            {
                tbl.Controls.Add(tr);

                // adding blank row to table
                TableRow trn = CreateRow();
                TableCell tc = new TableCell();
                tc.Height = Unit.Pixel(20);
                trn.Controls.Add(tc);
                tbl.Controls.Add(trn);
            }
            if (iColClount == 6)
                iColClount = 0;
        }
        pnl.Controls.Add(tbl);
        tcWelcome.Visible = true;
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
    protected void tcWelcome_ActiveTabChanged(object sender, EventArgs e)
    {
        Session["ActiveTabIndex"] = tcWelcome.ActiveTabIndex;
    }
}
