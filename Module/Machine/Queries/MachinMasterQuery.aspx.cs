using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Data;
using Common;
using errorLog;
public partial class Module_Machine_Queries_MachinMasterQuery : System.Web.UI.Page
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
            bindSection();
            bindType();
            bindGroup();
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
            ddlSegment.Items.Clear();
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
    private void bindGroup()
    {
        try
        {

            DataTable dt = new DataTable();
            ddlGroup.Items.Clear();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineGrp();
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
    private void bindSection()
    {
        try
        {

            DataTable dt = new DataTable();
            ddlSection.Items.Clear();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineSec();
            ddlSection.DataSource = dt;
            ddlSection.DataValueField = "MST_CODE";
            ddlSection.DataTextField = "MST_CODE";
            ddlSection.DataBind();
            ddlSection.Items.Insert(0, "Select");

        }

        catch (Exception ex)
        {
            throw ex;

        }
    }
    private void bindType()
    {
        try
        {

            DataTable dt = new DataTable();
            ddlType.Items.Clear();
            dt = SaitexBL.Interface.Method.MC_MACHINE_MASTER.SelectMachineType();
            ddlType.DataSource = dt;
            ddlType.DataValueField = "MST_CODE";
            ddlType.DataTextField = "MST_CODE";
            ddlType.DataBind();
            ddlType.Items.Insert(0,"Select");
        }

        catch (Exception ex)
        {
            throw ex;

        }
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
                Response.Redirect("~/Module/Admin/Pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        //string QueryString = "";
        //bool flag = false;
        //if (ddlGroup.SelectedIndex > 0)
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";

        //    QueryString = QueryString + "Group=" + ddlGroup.SelectedValue;
        //    flag = true;
        //}
        //if (ddlSection.SelectedIndex > 0)
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";

        //    QueryString = QueryString + "Section=" + ddlSection.SelectedValue;
        //    flag = true;
        //}
        //if (ddlType.SelectedIndex > 0)
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";

        //    QueryString = QueryString + "Type=" + ddlType.SelectedValue;
        //    flag = true;
        //}
        //if (ddlSegment.SelectedIndex > 0)
        //{
        //    if (flag)
        //        QueryString = QueryString + "&";
        //    else
        //        QueryString = QueryString + "?";

        //    QueryString = QueryString + "Segment=" + ddlSegment.SelectedValue;
        //    flag = true;
        //}
        //string URL = "../Reports/MachineSegment.aspx" + QueryString;
        //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=900,height=1000');", true);
    }
    private void GridProcesss()
    {
        string MachineSegment = string.Empty;
        string MachineSec = string.Empty;
        string MachineType = string.Empty;
        string MachineGroup = string.Empty;
        try
        {
            if (ddlSegment.SelectedValue.ToString() != null && ddlSegment.SelectedValue.ToString() != string.Empty && ddlSegment.SelectedValue.ToString() != "Select")
            {
                MachineSegment = ddlSegment.SelectedValue.ToString();
            }
            else
            {
                MachineSegment = string.Empty;
            }


            if (ddlSection.SelectedValue.ToString() != null && ddlSection.SelectedValue.ToString() != string.Empty && ddlSection.SelectedValue.ToString() != "Select")
            {
                MachineSec = ddlSection.SelectedValue.ToString();
            }
            else
            {
                MachineSec = string.Empty;
            }

            if (ddlType.SelectedValue.ToString() != null && ddlType.SelectedValue.ToString() != string.Empty && ddlType.SelectedValue.ToString() != "Select")
            {
                MachineType = ddlType.SelectedValue.ToString();
            }
            else
            {
                MachineType = string.Empty;
            }

            if (ddlGroup.SelectedValue.ToString() != null && ddlGroup.SelectedValue.ToString() != string.Empty && ddlGroup.SelectedValue.ToString() != "Select")
            {
                MachineGroup = ddlGroup.SelectedValue.ToString();
            }
            else
            {
                MachineGroup = string.Empty;
            }
            //DataTable DT = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetGroupWiseReport(MachineSegment, MachineSec, MachineType, MachineGroup);
            DataTable DT = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetMachinMaster(MachineSegment, MachineSec, MachineType, MachineGroup);
            if (DT != null && DT.Rows.Count > 0)
            {

                GridMachinMaster.DataSource = DT;
                GridMachinMaster.DataBind();
                lblTotalRecord.Text = DT.Rows.Count.ToString().Trim();
            
            }
            else
            {
                GridMachinMaster.DataSource = null;
                GridMachinMaster.DataBind();
                CommonFuction.ShowMessage("Data not Found");
            }
        }
        catch
        {
            throw;
        }
    } 
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            GridProcesss();
        }
        catch
        {
            throw;
        }
    }

    protected void GridMachinMaster_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow =
            new GridViewRow(0, 0, DataControlRowType.Header,
            DataControlRowState.Insert);  //creating new Header Type 

            TableCell HeaderCell = new TableCell(); //creating HeaderCell
            HeaderCell.Text = "MACHIN DETAIL";
            HeaderCell.ColumnSpan = 5;
            HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.
           

            HeaderCell = new TableCell(); //creating HeaderCell
            HeaderCell.Text = "MACHIN SPECIFICATION";
            HeaderCell.ColumnSpan = 7;
            HeaderGridRow.Cells.Add(HeaderCell);//Adding HeaderCell to header.

            HeaderCell = new TableCell();
            HeaderCell.Text = "UTILITY CONSUMPTION";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);
            GridMachinMaster.Controls[0].Controls.AddAt(0, HeaderGridRow);
                  

        }

    }
    protected void GridMachinMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridMachinMaster.PageIndex =  e.NewPageIndex;
        GridProcesss();

    }
}

