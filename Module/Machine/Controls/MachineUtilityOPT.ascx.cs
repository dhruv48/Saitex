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

public partial class Module_Machine_Controls_MachineUtilityOPT : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string MachineGroup;
    string MachineSegment;
    string MachineType;
    string MachineSec;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (Request.QueryString["IND_NUMB"] != null)
            ////{
            //    int IndentNumber = 0;
            //    IndentNumber = int.Parse(Request.QueryString["IND_NUMB"].ToString());
            //    txtFrom.Text = IndentNumber.ToString();
            //    txtTo.Text = IndentNumber.ToString();
            InitialisePage();
            //}
            //else
            //{
            //    GetLastIndentNo();
            //}

        }
    }
    private void InitialisePage()
    {
        try
        {
            bindSegment();


        }

        catch (Exception ex)
        {
            throw ex;

        }

    }
    private void bindSegment()
    {
        try
        {

            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectSegment();
            ddlSegment.DataSource = dt;
            ddlSegment.DataValueField = "MACHINE_SEGMENT";
            ddlSegment.DataTextField = "MACHINE_SEGMENT";
            ddlSegment.DataBind();
            ddlSegment.Items.Insert(0, "Select");

        }

        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void bindGroup(string MachineSegment, string MachineSec, string MachineType)
    {
        try
        {

            DataTable dt = new DataTable();
            MachineSec = ddlSection.SelectedValue.Trim();
            MachineSegment = ddlSegment.SelectedValue.Trim();
            MachineType = ddlType.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectGroup(MachineSegment, MachineSec, MachineType);
            ddlGroup.DataSource = dt;
            ddlGroup.DataValueField = "MACHINE_GROUP";
            ddlGroup.DataTextField = "MACHINE_GROUP";
            ddlGroup.DataBind();
            ddlGroup.Items.Insert(0, "Select");

        }

        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void bindSection(string MachineSegment)
    {
        try
        {

            DataTable dt = new DataTable();
            MachineSegment = ddlSegment.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectSection(MachineSegment);
            ddlSection.DataSource = dt;
            ddlSection.DataValueField = "MACHINE_SEC";
            ddlSection.DataTextField = "MACHINE_SEC";
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, "Select");

        }

        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void bindType(string MachineSegment, string MachineSec)
    {
        try
        {

            DataTable dt = new DataTable();
            MachineSec = ddlSection.SelectedValue.Trim();
            MachineSegment = ddlSegment.SelectedValue.Trim();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectType(MachineSegment, MachineSec);
            ddlType.DataSource = dt;
            ddlType.DataValueField = "MACHINE_TYPE";
            ddlType.DataTextField = "MACHINE_TYPE";
            ddlType.DataBind();
            ddlType.Items.Insert(0, "Select");
        }

        catch (Exception ex)
        {
            throw ex;

        }
    }

    //protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    //{
    //    string QueryString = "";
    //    bool flag = false;
    //    if (txtFrom.Text != "")
    //    {
    //        if (flag)
    //            QueryString = QueryString + "&";
    //        else
    //            QueryString = QueryString + "?";

    //        QueryString = QueryString + "From=" + txtFrom.Text;
    //        flag = true;
    //    }
    //    if (txtTo.Text != "")
    //    {
    //        if (flag)
    //            QueryString = QueryString + "&";
    //        else
    //            QueryString = QueryString + "?";

    //        QueryString = QueryString + "To=" + txtTo.Text;
    //        flag = true;
    //    }
    //    string URL = "../Reports/FabricIndentReport.aspx" + QueryString;
    //    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);

    //}
    //protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    //{
    //    GetLastIndentNo();
    //}
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
                Response.Redirect("~/Module/Admin/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        string QueryString = "";
        bool flag = false;
        if (ddlGroup.SelectedIndex > 0)
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";

            QueryString = QueryString + "Group=" + ddlGroup.SelectedValue;
            flag = true;
        }
        if (ddlSection.SelectedIndex > 0)
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";

            QueryString = QueryString + "Section=" + ddlSection.SelectedValue;
            flag = true;
        }
        if (ddlType.SelectedIndex > 0)
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";

            QueryString = QueryString + "Type=" + ddlType.SelectedValue;
            flag = true;
        }
        if (ddlSegment.SelectedIndex > 0)
        {
            if (flag)
                QueryString = QueryString + "&";
            else
                QueryString = QueryString + "?";

            QueryString = QueryString + "Segment=" + ddlSegment.SelectedValue;
            flag = true;
        }
        string URL = "../Reports/MachineUtility.aspx" + QueryString;
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);
    }
    //protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    //{
    //    string QueryString = "";
    //    bool flag = false;
    //    if (ddlGroup.SelectedValue != "")
    //    {
    //        if (flag)
    //            QueryString = QueryString + "&";
    //        else
    //            QueryString = QueryString + "?";

    //        QueryString = QueryString + "Group=" + ddlGroup.SelectedValue;
    //        flag = true;

    //        if (ddlSection.SelectedValue != "")
    //        {
    //            if (flag)
    //                QueryString = QueryString + "&";
    //            else
    //                QueryString = QueryString + "?";

    //            QueryString = QueryString + "Section=" + ddlSection.SelectedValue;
    //            flag = true;

    //            if (ddlType.SelectedValue != "")
    //            {
    //                if (flag)
    //                    QueryString = QueryString + "&";
    //                else
    //                    QueryString = QueryString + "?";

    //                QueryString = QueryString + "Type=" + ddlType.SelectedValue;
    //                flag = true;

    //                if (ddlSegment.SelectedValue != "")
    //                {
    //                    if (flag)
    //                        QueryString = QueryString + "&";
    //                    else
    //                        QueryString = QueryString + "?";

    //                    QueryString = QueryString + "Segment=" + ddlSegment.SelectedValue;
    //                    flag = true;
    //                }
    //                else
    //                {
    //                    string URL = "../Reports/MachineSegment.aspx" + QueryString;
    //                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);
    //                }
    //            }
    //            else
    //            {
    //                string URL = "../Reports/MachineSegment.aspx" + QueryString;
    //                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);
    //            }
    //        }
    //        else
    //        {
    //            string URL = "../Reports/MachineSegment.aspx" + QueryString;
    //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);
    //        }
    //    }
    //    else
    //    {
    //        string URL = "../Reports/MachineSegment.aspx" + QueryString;
    //        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);
    //    }
    //}
    protected void ddlSegment_SelectedIndexChanged(object sender, EventArgs e)
    {

        bindSection(MachineSegment);

    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindType(MachineSegment, MachineSec);
    }
    protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
    {
        bindGroup(MachineSegment, MachineSec, MachineType);

    }
    protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
