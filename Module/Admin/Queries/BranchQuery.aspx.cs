using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Module_Admin_Queries_BranchQuery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        List<DynamicGrid.DataTableProperty> list = new List<DynamicGrid.DataTableProperty>();
        list.Add(new DynamicGrid.DataTableProperty("Comp Name", "COMP_NAME", false, true, false, HorizontalAlign.Left, Unit.Pixel(50), "COMP_NAME"));
        list.Add(new DynamicGrid.DataTableProperty("Branch Name", "BRANCH_NAME", false, true, false, HorizontalAlign.Left, Unit.Pixel(50), "COMP_NAME"));
        list.Add(new DynamicGrid.DataTableProperty("Address", "BRANCH_ADD", false, true, false, HorizontalAlign.Left, Unit.Pixel(50), "COMP_NAME"));

        QueryGridView1.CreateGridDynamic(list);
        QueryGridView1.BindGridView("select COMP_NAME, BRANCH_NAME, BRANCH_ADD from V_CM_BRANCH_MST");
    }
}
