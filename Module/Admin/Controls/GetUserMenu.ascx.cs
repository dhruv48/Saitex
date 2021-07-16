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
using System.Linq;
using System.Collections.Generic;

public partial class Admin_UserControls_GetUserMenu : System.Web.UI.UserControl
{
    private bool _IsChange;
    public bool IsMenuChanged
    {
        get { return _IsChange; }
        set { _IsChange = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        GetData();
        GetCollapseId();
    }
    private void GetData()
    {
        try
        {
            if (Session["urLoginId"] != null)
            {
                SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

                DataTable dtUserMenu;
                if (oUserLoginDetail.UserType == "EE")
                {
                    tblHideEmpMenu.Visible = true;
                   
                }
                else
                {
                    tblHideEmpMenu.Visible = false;
                }

                if (Cache["dtUserMenu"] != null)
                {
                    dtUserMenu = (DataTable)Cache["dtUserMenu"];
                }
                else if (oUserLoginDetail.UserType == "SA")
                {
                    dtUserMenu = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.GetMenuDetailForSuperAdmin(oUserLoginDetail.UserCode);
                }
                else
                {
                    dtUserMenu = SaitexBL.Interface.Method.CM_CHILD_MDL_MST.GetMenuDetailByUserCode(oUserLoginDetail.UserCode);
                }

                if (dtUserMenu != null && dtUserMenu.Rows.Count > 0)
                {
                    CreateMenu(dtUserMenu);
                    //Cache["dtUserMenu"] = dtUserMenu;
                }
            }
            else
            {
                //Response.Redirect("~/Default.aspx");
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected void imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        //  imgbtnExpand.Visible = true;
        imgbtn.Visible = false;
    }
    protected void imgbtnExpand_Click(object sender, ImageClickEventArgs e)
    {
        //  imgbtnExpand.Visible = false;
        imgbtn.Visible = true;
    }

    private DataTable CreateTempDataTable()
    {
        DataTable dttemp = new DataTable();
        dttemp.Columns.Add("Id");
        return dttemp;
    }
    private void CreateMenu(DataTable dtUserMenu)
    {
        DataView dvModule = new DataView(dtUserMenu);
        DataTable distinctValues = dvModule.ToTable(true, "MDL_ID", "MDL_NAME", "POSTED_LENGTHMODULE");

        Table tblMain = CreateTable();

        if (distinctValues.Rows.Count > 0)
        {
            for (int iLoop = 0; iLoop < distinctValues.Rows.Count; iLoop++)
            {
                int ModuleId = Convert.ToInt32(distinctValues.Rows[iLoop]["MDL_ID"].ToString());

                ImageButton imgbtn = new ImageButton();
                imgbtn.ID = "imgbtn" + iLoop;
                imgbtn.ToolTip = distinctValues.Rows[iLoop]["MDL_NAME"].ToString();
                imgbtn.ImageUrl = "~/Module/Admin/ShowImage.aspx?MDL_ID=" + ModuleId.ToString() + "&ilen=" + distinctValues.Rows[iLoop]["POSTED_LENGTHMODULE"].ToString();
                //    imgbtn.ImageUrl = "~/" + dvModule[iLoop]["IMAGEURL"].ToString();
                imgbtn.CommandArgument = ModuleId.ToString();

                imgbtn.Height = Unit.Pixel(20);
                imgbtn.Width = Unit.Pixel(20);
                imgbtn.Visible = true;

                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtn" + iLoop;
                lbtn.ToolTip = distinctValues.Rows[iLoop]["MDL_NAME"].ToString();
                lbtn.Text = "&nbsp;&nbsp;" + distinctValues.Rows[iLoop]["MDL_NAME"].ToString();
                lbtn.CommandArgument = ModuleId.ToString();
                lbtn.Height = Unit.Pixel(15);
                lbtn.Font.Bold = false;
                lbtn.Font.Size = FontUnit.Point(9);
                lbtn.Visible = true;
                lbtn.CssClass = "LeftMenu_Main_Image";

                #region Module menu panel creation
                TableCell tcModule = CreateCell();
                tcModule.HorizontalAlign = HorizontalAlign.Left;
                tcModule.Controls.Add(imgbtn);
                tcModule.Controls.Add(lbtn);

                TableRow trModule = CreateRow();
                trModule.Controls.Add(tcModule);

                TableRow trLineBar = CreateLinebarRow();
                Table tblModule = CreateTable();
                tblModule.Controls.Add(trLineBar);
                tblModule.Controls.Add(trModule);
                Panel pnlModule = CreatePanel("Module" + iLoop);
                pnlModule.Controls.Add(tblModule);
                #endregion

                #region Child Module menu panel creation
                Table tblChildModule = CreateChildMenu(dtUserMenu, Convert.ToInt32(distinctValues.Rows[iLoop]["MDL_ID"].ToString()));
                Panel pnlChildModule = CreatePanel("ChildModule" + iLoop);
                pnlChildModule.Controls.Add(tblChildModule);
                #endregion

                #region assign Collapsible control to panels
                AjaxControlToolkit.CollapsiblePanelExtender oCollapsible = CreateCollapsibleControl(ModuleId, pnlModule.ID, pnlChildModule.ID);
                #endregion

                #region adding Menu item to main table
                TableRow trMainModule = CreateRow();
                TableCell tcMainModule = CreateCell();
                tcMainModule.Controls.Add(pnlModule);
                CollapsiblePanel.Controls.Add(oCollapsible);
                trMainModule.Controls.Add(tcMainModule);

                Table tblMainLink = CreateTable();
                tblMainLink.Controls.Add(trMainModule);

                TableCell tcMainLink = CreateCell();
                tcMainLink.Controls.Add(tblMainLink);
                tcMainLink.Controls.Add(pnlChildModule);
                TableRow trMainLink = CreateRow();
                trMainLink.Controls.Add(tcMainLink);
                tblMain.Controls.Add(trMainLink);
                #endregion

            }
        }
        MenuPanel.Controls.Add(tblMain);
    }
    private Table CreateChildMenu(DataTable dtUserMenu, int ModuleId)
    {
        Table tblChildMain = CreateTable();
        DataView dvChildModule = new DataView(dtUserMenu);
        dvChildModule.RowFilter = "MDL_ID=" + ModuleId;

        DataTable distinctChildVal = dvChildModule.ToTable(true, "MDL_ID", "CHILD_MDL_ID", "CHILD_MDL_NAME", "POSTED_LENGTHCHILDMODULE");

        if (distinctChildVal.Rows.Count > 0)
        {
            for (int iLoop = 0; iLoop < distinctChildVal.Rows.Count; iLoop++)
            {
                int ChildModuleId = Convert.ToInt32(distinctChildVal.Rows[iLoop]["CHILD_MDL_ID"].ToString());

                ImageButton imgbtn = new ImageButton();
                imgbtn.ID = "imgbtnChild" + ModuleId + iLoop;
                imgbtn.ToolTip = distinctChildVal.Rows[iLoop]["CHILD_MDL_NAME"].ToString();
                imgbtn.ImageUrl = "~/Module/Admin/ShowImage.aspx?CHILD_MDL_ID=" + ChildModuleId.ToString() + "&ilen=" + distinctChildVal.Rows[iLoop]["POSTED_LENGTHCHILDMODULE"].ToString();
                //  imgbtn.ImageUrl = "~/" + dvChildModule[iLoop]["IMAGEURL"].ToString();
                imgbtn.CommandArgument = ChildModuleId.ToString();
                imgbtn.PostBackUrl = "~/Module/Admin/Pages/Welcome.aspx?ModuleId=" + ModuleId + "&ChildModuleId=" + ChildModuleId.ToString();
                imgbtn.Height = Unit.Pixel(12);
                imgbtn.Width = Unit.Pixel(12);
                imgbtn.Visible = true;

                LinkButton lbtn = new LinkButton();
                lbtn.ID = "lbtnChild" + ModuleId + iLoop;
                lbtn.ToolTip = distinctChildVal.Rows[iLoop]["CHILD_MDL_NAME"].ToString();
                lbtn.Text = distinctChildVal.Rows[iLoop]["CHILD_MDL_NAME"].ToString();
                lbtn.PostBackUrl = "~/Module/Admin/Pages/Welcome.aspx?ModuleId=" + ModuleId + "&ChildModuleId=" + ChildModuleId.ToString();
                lbtn.CommandArgument = ChildModuleId.ToString();
                lbtn.Height = Unit.Pixel(20);
                lbtn.Font.Size = FontUnit.Point(8);
                lbtn.Font.Bold = false;
                lbtn.Font.Name = "ARIAL";
                lbtn.Visible = true;
                lbtn.CssClass = "LeftMenu_Child_Image";

                #region Child Module menu panel creation
                TableCell tcChildModule1 = CreateCell();
                tcChildModule1.HorizontalAlign = HorizontalAlign.Right;
                tcChildModule1.Width = Unit.Pixel(22);
                tcChildModule1.Controls.Add(imgbtn);

                TableCell tcChildModule2 = CreateCell();
                tcChildModule2.Controls.Add(lbtn);

                TableRow trChildModule = CreateRow();
                trChildModule.Controls.Add(tcChildModule1);
                trChildModule.Controls.Add(tcChildModule2);

                //TableRow trLineBar = CreateLinebarRow();
                tblChildMain.Controls.Add(trChildModule);
                // tblChildMain.Controls.Add(trLineBar);
                #endregion

            }
        }
        return tblChildMain;
    }
    private AjaxControlToolkit.CollapsiblePanelExtender CreateCollapsibleControl(int CollapsibleId, string ModulePanelId, string ChildModulePanelId)
    {
        AjaxControlToolkit.CollapsiblePanelExtender oCollapse = new AjaxControlToolkit.CollapsiblePanelExtender();
        oCollapse.ID = "Collapse" + CollapsibleId;
        oCollapse.CollapseControlID = ModulePanelId;
        oCollapse.Collapsed = true;
        oCollapse.ExpandControlID = ModulePanelId;
        oCollapse.ExpandDirection = AjaxControlToolkit.CollapsiblePanelExpandDirection.Vertical;
        oCollapse.ScrollContents = false;
        oCollapse.SuppressPostBack = true;
        oCollapse.TargetControlID = ChildModulePanelId;

        return oCollapse;
    }
    private Panel CreatePanel(string PanelId)
    {
        Panel pnl = new Panel();
        pnl.ID = "pnl" + PanelId;
        pnl.Visible = true;
        return pnl;
    }
    private Table CreateTable()
    {
        Table tbl = new Table();
        tbl.Visible = true;
        tbl.CellPadding = 1;
        tbl.CellSpacing = 1;
        return tbl;
    }
    private TableRow CreateRow()
    {
        TableRow tr = new TableRow();
        tr.Visible = true;
        tr.Height = Unit.Pixel(22);
        return tr;
    }
    private TableCell CreateCell()
    {
        TableCell tc = new TableCell();
        tc.Height = Unit.Pixel(22);
        tc.HorizontalAlign = HorizontalAlign.Left;
        tc.VerticalAlign = VerticalAlign.Top;
        return tc;
    }
    private TableRow CreateLinebarRow()
    {
        TableCell tcLineBar = CreateCell();
        Image imgLineBar = new Image();
        imgLineBar.Width = Unit.Percentage(100);
        imgLineBar.ImageUrl = "~/CommonImages/linebar.jpg";
        tcLineBar.ColumnSpan = 2;
        tcLineBar.Height = Unit.Pixel(2);
        tcLineBar.Controls.Add(imgLineBar);
        TableRow trLineBar = CreateRow();
        trLineBar.Height = Unit.Pixel(2);
        trLineBar.Controls.Add(tcLineBar);
        return trLineBar;
    }
    private void GetCollapseId()
    {
        if (Session["Collapse"] != null)
        {
            AjaxControlToolkit.CollapsiblePanelExtender oCollapse = (AjaxControlToolkit.CollapsiblePanelExtender)Session["Collapse"];
            oCollapse.Collapsed = false;
        }
    }
}
