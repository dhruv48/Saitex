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
using Common;
using errorLog;

public partial class Module_Fiber_Queries_FiberReceivingQuery : System.Web.UI.Page
{
    DateTime FromDate;
    DateTime ToDate;
    string from = string.Empty;
    string to = string.Empty;
    string lblComp = string.Empty;
    string lblBranch = string.Empty;
    string TrnNum = string.Empty;
    string PINum = string.Empty;
    string YrnType = string.Empty;
    string ShadeCode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Clear();
                BindGridYarnRecieving();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));

        }
    }
    private void Clear()
    {
        try
        {
            ddlYarn.SelectedIndex = -1;
            BindPartyCodeFromYarnRecieving();
            txtFromTrnNo.Text = string.Empty;
            txttoTrnNo.Text = string.Empty;
            txtTrnFromDate.Text = string.Empty;
            txttoTrndate.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }
    private void BindGridYarnRecieving()
    {
        try
        {
            string FiberCode;
            string PartyCode;
            int TrnfromNo = 0;
            int TrntoNo = 0;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);


            if (ddlYarn.SelectedIndex > -1)
            {
                FiberCode = ddlYarn.SelectedValue.ToString();
            }
            else
            {
                FiberCode = string.Empty;
            }


            if (ddlParty.SelectedIndex > 0)
            {
                PartyCode = ddlParty.SelectedValue.ToString();
            }
            else
            {
                PartyCode = string.Empty;
            }

            if (txtFromTrnNo.Text.ToString() != null && txtFromTrnNo.Text.ToString() != string.Empty)
            {
                TrnfromNo = int.Parse(txtFromTrnNo.Text.ToString());

            }
            else
            {
                //Trnfrom = ;

            }

            if (txttoTrnNo.Text.ToString() != null && txttoTrnNo.Text.ToString() != string.Empty)
            {
                TrntoNo = int.Parse(txttoTrnNo.Text.ToString());

            }
            else
            {
                //Trnto = 5;
            }

            if (txtTrnFromDate.Text.Trim() != string.Empty && txttoTrndate.Text.Trim().ToString() != string.Empty)
            {
                FromDate = DateTime.Parse(txtTrnFromDate.Text.Trim().ToString());
                ToDate = DateTime.Parse(txttoTrndate.Text.Trim().ToString());
                if (ToDate <= Edate && Sdate <= FromDate)
                {
                    if (FromDate < ToDate)
                    {
                        from = txtTrnFromDate.Text.Trim().ToString();
                        to = txttoTrndate.Text.Trim().ToString();
                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Sir ! From Date Should be less then To Date");

                    }
                }
                else
                {
                    Common.CommonFuction.ShowMessage("Sir ! To Date Should be in Financial Year");
                }

            }
            else
            {
                if (txtTrnFromDate.Text.Trim() != string.Empty)
                {
                    FromDate = DateTime.Parse(txtTrnFromDate.Text.Trim().ToString());
                    if (FromDate >= Sdate && Edate >= FromDate)
                    {
                        from = txtTrnFromDate.Text.Trim().ToString();

                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Sir ! From Date  Should be in Financial Year");

                    }
                }
                else
                {

                    from = Sdate.ToShortDateString().ToString();
                }

                if (txtTrnFromDate.Text.Trim().ToString() != string.Empty)
                {
                    ToDate = DateTime.Parse(txtTrnFromDate.Text.Trim().ToString());

                    if (ToDate >= Sdate || Edate >= ToDate)
                    {

                        to = txtTrnFromDate.Text.Trim().ToString();

                    }
                    else
                    {
                        Common.CommonFuction.ShowMessage("Sir !To Date Should be in Financial Year");
                    }

                }
                else
                {
                    to = Edate.ToShortDateString().ToString();

                }
            }


            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.FiberRecievingForQueryForm(PartyCode, FiberCode, from, to, TrnfromNo, TrntoNo);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item.");
                Clear();
            }
        }
        catch
        {
            throw;
        }
    }
    private void BindPartyCodeFromYarnRecieving()
    {
        try
        {


            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.PartyforFiberRecievingQueryForm();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlParty.Items.Clear();
                ddlParty.DataTextField = "PRTY_NAME";
                ddlParty.DataValueField = "PRTY_CODE";
                ddlParty.DataSource = dt;
                ddlParty.DataBind();
                ddlParty.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            else
            {
                ddlParty.Items.Clear();
                ddlParty.Items.Insert(0, new ListItem("SELECT", "0"));
            }
        }
        catch
        {
            throw;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {

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
    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlYarn_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FIBER_MST.GetFiber();
            DataView Dv = new DataView(dt);
            ddlYarn.DataSource = Dv;
            ddlYarn.DataValueField = "FIBER_CODE";
            ddlYarn.DataTextField = "FIBER_CODE";
            ddlYarn.DataBind();
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
        }
    }
    protected DataTable GetItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT FIBER_CODE, FIBER_DESC, FIBER_CAT FROM TX_FIBER_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND FIBER_code NOT IN (SELECT FIBER_CODE FROM (SELECT FIBER_CODE, FIBER_DESC FROM TX_FIBER_MASTER WHERE FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY FIBER_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY FIBER_CODE";
            string SearchQuery = text.ToUpper() + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }
    protected int GetItemsCount(string text)
    {
        string CommandText = " SELECT   *  FROM   (  SELECT   FIBER_CODE, FIBER_DESC, FIBER_CAT FROM  TX_FIBER_MASTER WHERE      FIBER_CODE LIKE :SearchQuery OR FIBER_CAT LIKE :SearchQuery OR FIBER_DESC LIKE :SearchQuery ORDER BY   FIBER_CODE) asd WHERE FIBER_CAT <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY FIBER_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                ddlYarn.Items.Clear();

                ddlYarn.DataSource = data;
                ddlYarn.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Fiber loading.\r\nSee error log for detail."));
            //lblMode.Text = ex.ToString();

        }
    }
    protected void btnGetRecord_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridYarnRecieving();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select Yarn Code.\r\nSee error log for detail."));
        }
    }
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Clear();
        }
        catch
        {
            throw;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridYarnRecieving();
        }
        catch
        {
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCompCode = (Label)e.Row.FindControl("lblCompCode1");
                lblComp = lblCompCode.Text.Trim();

                Label lblBranchCode = (Label)e.Row.FindControl("lblBranch");
                lblBranch = lblBranchCode.ToolTip.Trim();

                Label LblTrnNum = (Label)e.Row.FindControl("lblTrnnumb");
                TrnNum = LblTrnNum.Text.Trim();

                Label lblpino = (Label)e.Row.FindControl("lblTrnpino");
                PINum = lblpino.Text.Trim();
                Label lblFiberType = (Label)e.Row.FindControl("lblfIBER");
                YrnType = lblFiberType.ToolTip;
                //Label LblShadeCode = (Label)e.Row.FindControl("lblShadeCode");
                //ShadeCode = LblShadeCode.Text.Trim();

                DataTable dtc = BindSupTranGrid(lblComp, lblBranch, TrnNum, PINum, YrnType);
                GridView grdBOM = (GridView)e.Row.FindControl("grdBOM");
                if (grdBOM != null)
                {
                    grdBOM.DataSource = dtc;
                    grdBOM.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private DataTable BindSupTranGrid(string lblComp, string lblBranch, string TrnNum, string PINum, string YrnType)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FIBER_IR_MST.BindSubTranGrid(lblComp, lblBranch, TrnNum, PINum, YrnType);
            return dt;
        }
        catch
        {
            throw;
        }
    }
}
