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
using System.Data.OracleClient;

using Common;
using errorLog;

public partial class Module_Fiber_Pages_make_Fiber_transaction_to_taxable_type : System.Web.UI.Page
{

    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string where_Query = string.Empty;
    string BRANCH = string.Empty;
    string QueryString = string.Empty;
    string TAXATIONTYPE = string.Empty;
    string PRTYNAME = string.Empty;
    string GRNNo = string.Empty;
    string FiberDesc = string.Empty;
    string Year = string.Empty;
    string DateFrom = string.Empty;
    string DateTo = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!Page.IsPostBack)
        {
            bindListForApproval();

        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string msg = string.Empty;

            DataTable dtDetail = CreateDataTable();
            int totalRows = gvPartyList.Rows.Count;
            for (int r = 0; r < totalRows; r++)
            {
                GridViewRow thisGridViewRow = gvPartyList.Rows[r];
                if (thisGridViewRow.RowType == DataControlRowType.DataRow)
                {
                    Label lblYear = (Label)thisGridViewRow.FindControl("lblYear");
                    Label lblCompany = (Label)thisGridViewRow.FindControl("lblCompany");
                    Label lblBranch = (Label)thisGridViewRow.FindControl("lblBranch");
                    Label lblMrn = (Label)thisGridViewRow.FindControl("lblMrn");
                    Label lblFiber = (Label)thisGridViewRow.FindControl("lblFiber");
                    CheckBox Approved = (CheckBox)thisGridViewRow.FindControl("chkApproved");
                    DropDownList ddlExcisableType = (DropDownList)thisGridViewRow.FindControl("ddlExcisableType");

                    if (Approved.Checked)
                    {
                        DataRow dr = dtDetail.NewRow();
                        dr["YEAR"] = int.Parse(lblYear.Text);
                        dr["COMP_CODE"] = lblCompany.ToolTip;
                        dr["BRANCH_CODE"] = lblBranch.ToolTip;
                        dr["TRN_NUMB"] = lblMrn.Text;
                        dr["TRN_TYPE"] = lblMrn.ToolTip;
                        dr["ITEM_CODE"] = lblFiber.ToolTip;
                       
                        dr["TAXATION_TYPE_DATE"] = DateTime.Now;
                        dr["TAXATION_TYPE_BY"] = oUserLoginDetail.Username;
                        dr["TAXATION_TYPE"] = ddlExcisableType.SelectedValue;
                        dtDetail.Rows.Add(dr);
                    }
                }
            }

            if (msg != string.Empty)
                CommonFuction.ShowMessage(msg);

            int iResult = SaitexBL.Interface.Method.TX_FIBER_IR_MST.Update_Reciving_For_TaxationType(dtDetail);
            if (iResult > 0)
            {
                CommonFuction.ShowMessage("Updated Successfully.");
                bindListForApproval();
            }

        }
        catch
        {
            throw;
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
                Response.Redirect("~/Module/Admin/pages/Welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Update this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");

    }


    private DataTable CreateDataTable()
    {
        try
        {
            DataTable dtDetail = new DataTable();
            dtDetail.Columns.Add("YEAR", typeof(int));
            dtDetail.Columns.Add("COMP_CODE", typeof(string));
            dtDetail.Columns.Add("BRANCH_CODE", typeof(string));
            dtDetail.Columns.Add("TRN_NUMB", typeof(int));
            dtDetail.Columns.Add("TRN_TYPE", typeof(string));
            dtDetail.Columns.Add("ITEM_CODE", typeof(string));
            dtDetail.Columns.Add("TAXATION_TYPE_BY", typeof(string));
            dtDetail.Columns.Add("TAXATION_TYPE_DATE", typeof(DateTime));
            dtDetail.Columns.Add("TAXATION_TYPE", typeof(string));
            return dtDetail;
        }
        catch
        {
            throw;
        }
    }



    private void bindListForApproval()
    {
        try
        {

            SaitexDM.Common.DataModel.TX_FIBER_IR_MST oTX_FIBER_IR_MST = new SaitexDM.Common.DataModel.TX_FIBER_IR_MST();
            oTX_FIBER_IR_MST.COMP_CODE = oUserLoginDetail.COMP_CODE;
            oTX_FIBER_IR_MST.BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            oTX_FIBER_IR_MST.YEAR = oUserLoginDetail.DT_STARTDATE.Year;

            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDataForTxationType1(oTX_FIBER_IR_MST);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 0)
                {
                    gvPartyList.DataSource = dt;
                    gvPartyList.DataBind();
                    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                }
                else
                {
                    lblTotalRecord.Text = "No Party for approval";
                    gvPartyList.DataSource = null;
                    gvPartyList.DataBind();
                    lblTotalRecord.Text = "0";
                }
            }
            else
            {
                lblTotalRecord.Text = "No Party for approval";
                gvPartyList.DataSource = null;
                gvPartyList.DataBind();
                lblTotalRecord.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }


    protected void gvPartyList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindListForApproval();

            gvPartyList.PageIndex = e.NewPageIndex;
            gvPartyList.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

   

    protected void FilterGrid_Click1(object sender, EventArgs e)
    {
        try
        {
            SearchbyKeywords();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    public void SearchbyKeywords()
    {




        string FiberDesc = txtfiberdesc.Text.Trim();
        string PRTYNAME = txtpartyname.Text.Trim();
        string GRNNo = txtyGRNNo.Text.Trim();
        string BRANCH = txtbranch.Text.Trim();
        string DateFrom = txtdatefrom.Text.Trim();
        string DateTo = txtdateto.Text.Trim();
        string TAXATIONTYPE = ddlExcisableType1.SelectedValue;



        DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDataForReportTaxation1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, txtbranch.Text, ddlExcisableType1.SelectedValue, txtfiberdesc.Text, txtyGRNNo.Text, txtpartyname.Text, txtdatefrom.Text, txtdateto.Text);
        if (dt.Rows.Count > 0)
        {

            gvPartyList.DataSource = dt;
            gvPartyList.DataBind();
            lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            gvPartyList.Visible = true;

        }
        AutofillSearchContent(PRTYNAME, GRNNo, FiberDesc, TAXATIONTYPE, BRANCH, DateFrom, DateTo);



    }

    private void AutofillSearchContent(string PRTYNAME, string GRNNo, string FiberDesc, string TAXATIONTYPE, string BRANCH, string DateFrom, string DateTo)
    {
        try
        {

            ////((TextBox)gvPartyList.HeaderRow.FindControl("txtyarndesc")).Text = YarnDesc;
            FiberDesc = txtfiberdesc.Text.Trim();
            PRTYNAME = txtpartyname.Text.Trim();
            GRNNo = txtyGRNNo.Text.Trim();
            BRANCH = txtbranch.Text.Trim();
            DateFrom = txtdatefrom.Text.Trim();
            DateTo = txtdateto.Text.Trim();


        }
        catch
        {
            throw;
        }

    }
    protected void imgbtnPrint_Click1(object sender, ImageClickEventArgs e)
    {

        try
        {

            string TAXATIONTYPE = ddlExcisableType1.Text.Trim();
            string FiberDesc = txtfiberdesc.Text.Trim();
            string PRTYNAME = txtpartyname.Text.Trim();
            string GRNNo = txtyGRNNo.Text.Trim();
            string BRANCH = txtbranch.Text.Trim();
            string DateFrom = txtdatefrom.Text.Trim();
            string DateTo = txtdateto.Text.Trim();

            string URL = "../../Fiber/Reports/MakeFiberTaxation.aspx?TAXATION_TYPE=" + TAXATIONTYPE + "&BRANCH_NAME=" + BRANCH + "&FiberDesc=" + FiberDesc + "&PRTY_NAME=" + PRTYNAME + "&TRN_NUMB=" + GRNNo + "&TRN_DATE1=" + DateFrom + "&TRN_DATE2=" + DateTo;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=800,height=600');", true);

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Open Print Page.\r\nSee error log for detail."));
        }
    }




    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

        string TAXATIONTYPE = ddlExcisableType1.Text.Trim();
        string FiberDesc = txtfiberdesc.Text.Trim();
        string PRTYNAME = txtpartyname.Text.Trim();
        string GRNNo = txtyGRNNo.Text.Trim();
        string BRANCH = txtbranch.Text.Trim();
        string DateFrom = txtdatefrom.Text.Trim();
        string DateTo = txtdateto.Text.Trim();


        string strFilename = "Taxation_Fiber_Conformation_List_" + DateTime.Now.ToString() + ".xls";
        string TAXATION_TYPE = ddlExcisableType1.SelectedValue.Trim();
        DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.GetDataForReportTaxation1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year, txtbranch.Text, ddlExcisableType1.SelectedValue, txtfiberdesc.Text, txtyGRNNo.Text, txtpartyname.Text, txtdatefrom.Text, txtdateto.Text);
        dt.TableName = "MakeYarnTaxation1";
        UploadDataTableToExcel(dt, strFilename);
    }


    protected void UploadDataTableToExcel(DataTable dt, string filename)
    {
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (DataColumn dtcol in dt.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dt.Rows)
        {
            tab = "";
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }
    protected void btngetdata_Click(object sender, EventArgs e)
    {
        try
        {
            SearchbyKeywords();
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        bindListForApproval();

    }
}
