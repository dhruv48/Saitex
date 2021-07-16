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

public partial class Module_Inventory_Pages_Test : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Item_LOV1.PONumb = 0;
        Item_LOV1.ForPONumb = false;
        Item_LOV1.Width = Unit.Pixel(500);
        Item_LOV1.Height = Unit.Pixel(350);
        Item_LOV1.SelectedIndexChanged += new Module_Inventory_Controls_Item_LOV.RefreshDataGridView(Item_LOV1_SelectedIndexChanged);
        
    }

    void Item_LOV1_SelectedIndexChanged(Module_Inventory_Controls_Item_LOV.Item_LOV_EventArgs e)
    {
        e.SelectedText.ToString();
        e.SelectedValue.ToString();
        Label1.Text = e.SelectedText.ToString() + "  " + e.SelectedValue.ToString();
    }

}
