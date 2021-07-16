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
public partial class Module_SewingThread_Queries_STRecievingQuery : System.Web.UI.Page
{
    DateTime FromDate;
    DateTime ToDate;
    string from = string.Empty;
    string to = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    string lblComp = string.Empty;
    string lblBranch = string.Empty;
    string TrnNum = string.Empty;
    string PINum = string.Empty;
    string YrnType = string.Empty;
    string ShadeCode = string.Empty;
   
    //----------------------
    //private DataTable dtRate1 = null;
    //private DataTable dtdRateComponent = null;
    //private double FinalAmount = 0;
    //private double StartFinalAmount = 0;
    //private string TextBoxId = "";
    //private string YARN_CODE = "";
    //private string SHADE_CODE = "";
    //------------------------
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Clear();
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
            ddlSewingThread.SelectedIndex = -1;
            txtfromTrnNo.Text = string.Empty;
            txttoTrndate.Text = string.Empty;
            //txtTrnFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
           // txttoTrndate.Text = System.DateTime.Now.ToShortDateString();
           
            BindPartyCodeFromYarnRecieving();
            txtfromTrnNo.Text = string.Empty;
            txttoTrnNo.Text = string.Empty;

        }
        catch
        {
            throw;
        }
    }
    protected void ddlSewingThread_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {


            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetSewingThread();
            DataView Dv = new DataView(dt);
            ddlSewingThread.DataSource = Dv;
            ddlSewingThread.DataValueField = "YARN_CODE";
            ddlSewingThread.DataTextField = "YARN_CODE";
            ddlSewingThread.DataBind();
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
            string CommandText = " SELECT * FROM (SELECT YARN_CODE, YARN_DESC, YARN_TYPE FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE   ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND YARN_code NOT IN (SELECT YARN_CODE FROM (SELECT YARN_CODE, YARN_DESC FROM YRN_MST WHERE YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY YARN_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
            }

            string SortExpression = " ORDER BY YARN_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE = 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
        return data.Rows.Count;
    }
    protected void BindPartyCodeFromYarnRecieving()
    {
        try
        {


            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.PartyforYrnRecievingQueryForm();
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlparty.Items.Clear();
                ddlparty.DataTextField = "PRTY_NAME";
                ddlparty.DataValueField = "PRTY_CODE";
                ddlparty.DataSource = dt;
                ddlparty.DataBind();
                ddlparty.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            else
            {
                ddlparty.Items.Clear();
                ddlparty.Items.Insert(0, new ListItem("SELECT", "0"));
            }
        }
        catch
        {
            throw;
        }
    }

    protected void txttoTrndate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtTrnFromDate.Text == null || txtTrnFromDate.Text == string.Empty)
            {
                CommonFuction.ShowMessage("Please enter From CR Date first..");
            }
            else
            {
                if (DateTime.Parse(txtTrnFromDate.Text) > DateTime.Parse(txtTrnFromDate.Text))
                {
                    CommonFuction.ShowMessage("Please From TRN Date should not be greater than To TRN Date..");
                }
                else
                {
                    //BindCRGrid();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sewing Thread Recieving To Date TextBox.\r\nSee error log for detail."));
            // lblMode.Text = ex.ToString();
        }
    }
    private void BindGridYarnRecieving()
    {
        try
        {
            string YarnCode = string.Empty;
            string PartyCode = string.Empty;
            int TrnfromNo = 0;
            int TrntoNo = 0;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            if (ddlSewingThread.SelectedValue != null && ddlSewingThread.SelectedValue != string.Empty && ddlSewingThread.SelectedIndex > 0)
            {
                YarnCode = ddlSewingThread.SelectedValue.ToString();
            }
            else
            {
                YarnCode = string.Empty;
            }
            if (ddlparty.SelectedValue != null && ddlparty.SelectedValue != string.Empty && ddlparty.SelectedIndex > 0)
            {
                PartyCode = ddlparty.SelectedValue.ToString();
            }
            else
            {
                PartyCode = string.Empty;
            }
            if (txtfromTrnNo.Text != null && txtfromTrnNo.Text != string.Empty)
            {
                TrnfromNo = int.Parse(txtfromTrnNo.Text.ToString());
            }
            else
            {
               // TrnfromNo = 0;
            }
            if (txttoTrnNo.Text != null && txttoTrnNo.Text != string.Empty)
            {
                TrntoNo = int.Parse(txttoTrnNo.Text.ToString());

            }
            else
            {
               // TrntoNo = 0;
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
                      //  to = Edate.ToShortDateString().ToString();

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

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.STRecievingForQueryForm(YarnCode, PartyCode, TrnfromNo, TrntoNo, from, to);
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
    protected void imgbtnClear_Click1(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./STRecievingQuery.aspx", false);
        }
        catch(Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
           
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
            throw;
        }
    }
    protected void Item_LOV_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {

                ddlSewingThread.Items.Clear();

                ddlSewingThread.DataSource = data;
                ddlSewingThread.DataBind();
            }

            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Sewing Thread loading.\r\nSee error log for detail."));
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
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select Sewing Thread Code.\r\nSee error log for detail."));
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
                lblBranch = lblBranchCode.Text.Trim();

                Label LblTrnNum = (Label)e.Row.FindControl("lblTrnnumb");
                TrnNum = LblTrnNum.Text.Trim();

                Label lblpino = (Label)e.Row.FindControl("lblTrnpino");
                PINum = lblpino.Text.Trim();
                Label lblYrnType = (Label)e.Row.FindControl("lblyarn");
                YrnType = lblYrnType.Text.Trim();
                Label LblShadeCode = (Label)e.Row.FindControl("lblShadeCode");
                
                ShadeCode = LblShadeCode.Text.Trim();
                DataTable dtc = BindSupTranGrid(lblComp, lblBranch, TrnNum, PINum, YrnType);
                GridView grdBOM = (GridView)e.Row.FindControl("grdBOM");
                if (grdBOM != null)
                {
                    grdBOM.DataSource = dtc;
                    grdBOM.DataBind();
                }
                DataTable dtd = BindTranDis(lblComp, lblBranch, TrnNum, YrnType, ShadeCode);
                GridView grdTrnDis = (GridView)e.Row.FindControl("grdTrnDis");
                if (grdTrnDis != null)
                {
                    grdTrnDis.DataSource = dtd;
                    grdTrnDis.DataBind();
                }
            }

        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        Label lblCompCode = (Label)e.Row.FindControl("lblCompCode1");
        //        lblComp = lblCompCode.Text.Trim();

        //        Label lblBranchCode = (Label)e.Row.FindControl("lblBranch");
        //        lblBranch = lblBranchCode.Text.Trim();

        //        Label LblTrnNum = (Label)e.Row.FindControl("lblTrnnumb");
        //        TrnNum = LblTrnNum.Text.Trim();

        //        //    ////Label lblpino = (Label)e.Row.FindControl("lblTrnpino");
        //        //    ////PINum = lblpino.Text.Trim();
        //        Label lblYrnType = (Label)e.Row.FindControl("lblyarn");
        //        YrnType = lblYrnType.Text.Trim();
        //        Label LblShadeCode = (Label)e.Row.FindControl("lblShadeCode");
        //        ShadeCode = LblShadeCode.Text.Trim();
        //        DataTable dtd = BindTranDis(lblComp, lblBranch, TrnNum, YrnType, ShadeCode);
        //        GridView grdTrnDis = (GridView)e.Row.FindControl("grdTrnDis");
        //        if (grdTrnDis != null)
        //        {
        //            grdTrnDis.DataSource = dtd;
        //            grdTrnDis.DataBind();
        //        }
        //    }

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private DataTable BindTranDis(string lblComp, string lblBranch, string TrnNum, string YrnType, string ShadeCode)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.BindTranDis(lblComp, lblBranch, TrnNum, YrnType, ShadeCode);
            return dt;
        }
        catch
        {
            throw;
        }
    }
    

    private DataTable BindSupTranGrid(string lblComp, string lblBranch, string TrnNum, string PINum, string YrnType)
    {
        try

        {
            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.BindSubTranGrid(lblComp, lblBranch, TrnNum, PINum, YrnType);
            return dt;
        }
        catch
        {
            throw;
        }
    }

    
}
