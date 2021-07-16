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


public partial class CommonControls_GetEmpMenu : System.Web.UI.UserControl
{
    private bool _IsChange;
    public bool IsMenuChanged
    {
        get { return _IsChange; }
        set { _IsChange = value; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

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
        Table tblMain = CreateTable();

        if (dvModule.Count > 0)
        {
            DataTable dtTemp = CreateTempDataTable();
            for (int iLoop = 0; iLoop < dvModule.Count; iLoop++)
            {
                bool IsDuplicate = false;
                int ModuleId = Convert.ToInt32(dvModule[iLoop]["MDL_ID"].ToString());
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    int iModuleId = Convert.ToInt32(drTemp["Id"].ToString());
                    if (iModuleId == ModuleId)
                        IsDuplicate = true;
                }
                if (!IsDuplicate)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp["Id"] = ModuleId.ToString();
                    dtTemp.Rows.Add(drTemp);

                    ImageButton imgbtn = new ImageButton();
                    imgbtn.ID = "imgbtn" + iLoop;
                    imgbtn.ToolTip = dvModule[iLoop]["MDL_NAME"].ToString();
                    imgbtn.ImageUrl = "~/Module/Admin/ShowImage.aspx?AddModuleImageId=" + ModuleId.ToString() + "&ilen=" + dvModule[iLoop]["POSTED_LENGTHMODULE"].ToString();
                    imgbtn.CommandArgument = ModuleId.ToString();

                    imgbtn.Height = Unit.Pixel(20);
                    imgbtn.Width = Unit.Pixel(20);
                    imgbtn.Visible = true;

                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtn" + iLoop;
                    lbtn.ToolTip = dvModule[iLoop]["MDL_NAME"].ToString();
                    lbtn.Text = "&nbsp;&nbsp;" + dvModule[iLoop]["MDL_NAME"].ToString();
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
                    Table tblChildModule = CreateChildMenu(dtUserMenu, Convert.ToInt32(dvModule[iLoop]["MDL_ID"].ToString()));
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
            dtTemp.Clear();
            dtTemp.Dispose();
        }
        MenuPanel.Controls.Add(tblMain);
    }
    private Table CreateChildMenu(DataTable dtUserMenu, int ModuleId)
    {
        Table tblChildMain = CreateTable();
        DataView dvChildModule = new DataView(dtUserMenu);
        dvChildModule.RowFilter = "MDL_ID=" + ModuleId;
        if (dvChildModule.Count > 0)
        {
            DataTable dtTemp = CreateTempDataTable();
            for (int iLoop = 0; iLoop < dvChildModule.Count; iLoop++)
            {
                bool IsDuplicate = false;
                int ChildModuleId = Convert.ToInt32(dvChildModule[iLoop]["CHILD_MDL_ID"].ToString());
                foreach (DataRow drTemp in dtTemp.Rows)
                {
                    int iChildModuleId = Convert.ToInt32(drTemp["Id"].ToString());
                    if (iChildModuleId == ChildModuleId)
                        IsDuplicate = true;
                }
                if (!IsDuplicate)
                {
                    DataRow drTemp = dtTemp.NewRow();
                    drTemp["Id"] = ChildModuleId.ToString();
                    dtTemp.Rows.Add(drTemp);

                    ImageButton imgbtn = new ImageButton();
                    imgbtn.ID = "imgbtnChild" + ModuleId + iLoop;
                    imgbtn.ToolTip = dvChildModule[iLoop]["CHILD_MDL_NAME"].ToString();
                    imgbtn.ImageUrl = "~/Module/Admin/ShowImage.aspx?AddChildModuleImageId=" + ChildModuleId.ToString() + "&ilen=" + dvChildModule[iLoop]["POSTED_LENGTHCHILDMODULE"].ToString();
                    imgbtn.CommandArgument = ChildModuleId.ToString();
                    imgbtn.PostBackUrl = "~/Module/Admin/Pages/Welcome.aspx?ModuleId=" + ModuleId + "&ChildModuleId=" + ChildModuleId.ToString();
                    imgbtn.Height = Unit.Pixel(12);
                    imgbtn.Width = Unit.Pixel(12);
                    imgbtn.Visible = true;

                    LinkButton lbtn = new LinkButton();
                    lbtn.ID = "lbtnChild" + ModuleId + iLoop;
                    lbtn.ToolTip = dvChildModule[iLoop]["CHILD_MDL_NAME"].ToString();
                    lbtn.Text = dvChildModule[iLoop]["CHILD_MDL_NAME"].ToString();
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
            dtTemp.Clear();
            dtTemp.Dispose();
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
