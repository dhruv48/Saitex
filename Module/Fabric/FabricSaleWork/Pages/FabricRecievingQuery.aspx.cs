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
public partial class Module_Fabric_FabricSaleWork_Pages_FabricRecievingQuery : System.Web.UI.Page
{
    DataTable dt = null;
    DateTime FromDate;
    DateTime ToDate;
    string from = string.Empty;
    string to = string.Empty;
    string lblComp = string.Empty;
    string lblBranch = string.Empty;
    string TrnNum = string.Empty;
    string PINum = string.Empty;
    string FbrType = string.Empty;
    string ShadeCode = string.Empty;
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                Clear1();
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in loading Page.\r\nSee error log for detail."));

        }

    }
    private void Clear1()
    {
        try
        {
           // ddlFabric.SelectedIndex = -1;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            BindFabricForRecieving();
            BindPartyCodeFromFabricRecieving();
            txtfrmdate.Text = oUserLoginDetail.DT_STARTDATE.ToString();  
                //string.Empty;
            txtfromtrnno.Text = string.Empty;
            txttodate.Text = Common.CommonFuction.GetYearEndDate(Sdate).ToString();
//string.Empty;
            txttotrnno.Text = string.Empty;
         }
        catch
        {
            throw;
        }
    }
    private void BindPartyCodeFromFabricRecieving()
    {
        try
        {


            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.PartyforYrnRecievingQueryForm();
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
    private void BindFabricForRecieving()
    {
        try
        {


           
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView Dv = new DataView(dt);
                ddlFabric.DataSource = Dv;
                ddlFabric.DataValueField = "FABR_CODE";
                ddlFabric.DataTextField = "FABR_CODE";
                ddlFabric.DataBind();
                ddlFabric.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            else
            {
                ddlFabric.Items.Clear();
                ddlFabric.Items.Insert(0, new ListItem("SELECT", "0"));
            }
            
        }
        catch
        {
            throw;
        }
        finally
        {
            dt.Dispose();
            dt = null;

        }
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {


    }
    //protected void ddlFabric1_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {


    //        DataTable dt = null;
    //        dt = new DataTable();
    //        dt = SaitexBL.Interface.Method.TX_FABRIC_MST.GetFabric();
               
    //        DataView Dv = new DataView(dt);
    //        ddlFabric.DataSource = Dv;
    //        ddlFabric.DataValueField = "FABR_CODE";
    //        ddlFabric.DataTextField = "FABR_CODE";
    //        ddlFabric.DataBind();
    //        dt.Dispose();
    //        dt = null;
    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting data for material detail.\r\nSee error log for detail."));
    //    }
    //}
  

    //protected void Fabric_Lov_Items(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    //{
    //    try
    //    {
    //        DataTable data = GetItems(e.Text.ToUpper(), e.ItemsOffset);

    //        // Looping through the items and adding them to the "Items" collection of the ComboBox
    //        if (data != null && data.Rows.Count > 0)
    //        {
    //            ddlFabric.Items.Clear();

    //            ddlFabric.DataSource = data;
    //            ddlFabric.DataBind();
    //        }

    //        // Calculating the numbr of items loaded so far in the ComboBox
    //        e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;

    //        // Getting the total number of items that start with the typed text
    //        e.ItemsCount = GetItemsCount(e.Text);
    //    }
    //    catch (Exception ex)
    //    {
    //        CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
    //        //lblMode.Text = ex.ToString();

    //    }
    //}
    //protected DataTable GetItems(string text, int startOffset)
    //{
    //    try
    //    {
    //        string CommandText = " SELECT * FROM (SELECT FABR_CODE, FABR_DESC, FABR_TYPE FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY FABR_CODE) asd WHERE   ROWNUM <= 15 ";
    //        string whereClause = string.Empty;

    //        if (startOffset != 0)
    //        {
    //            whereClause += " AND FABR_code NOT IN (SELECT FABR_CODE FROM (SELECT FABR_CODE, FABR_DESC FROM TX_FABRIC_MST WHERE FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY FABR_CODE) asd WHERE ROWNUM <= " + startOffset + ") ";
    //        }

    //        string SortExpression = " ORDER BY FABR_CODE";
    //        string SearchQuery = text.ToUpper() + "%";
    //        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

    //        return data;
    //    }
    //    catch
    //    {
    //        throw;
    //    }
    //}
    //protected int GetItemsCount(string text)
    //{
    //    string CommandText = " SELECT   *  FROM   (  SELECT   FABR_CODE, FABR_DESC, FABR_TYPE FROM   TX_FABRIC_MST WHERE      FABR_CODE LIKE :SearchQuery OR FABR_TYPE LIKE :SearchQuery OR FABR_DESC LIKE :SearchQuery ORDER BY   FABR_CODE) asd ";
    //    string WhereClause = " ";
    //    string SortExpression = " ORDER BY FABR_CODE ";
    //    string SearchQuery = text.ToUpper() + "%";
    //    DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");
    //    return data.Rows.Count;
    //}

    protected void btnGetRecord_Click(object sender, EventArgs e)
    {
        try
        {
            BindGridFabricRecieving();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select Yarn Code.\r\nSee error log for detail."));
        }
    }
    private void BindGridFabricRecieving()
    {
        try
        {
            string FabricCode;
            string PartyCode;
            int TrnfromNo = 0;
            int TrntoNo = 0;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);


            if (ddlFabric.SelectedIndex > 0)
            {
                FabricCode = ddlFabric.SelectedValue.ToString();
            }
            else
            {
                FabricCode = string.Empty;
            }


            if (ddlParty.SelectedIndex > 0)
            {
                PartyCode = ddlParty.SelectedValue.ToString();
            }
            else
            {
                PartyCode = string.Empty;
            }

            if (txtfromtrnno.Text.ToString()!=null && txtfromtrnno.Text.ToString()!=string.Empty)
                 
            {
                TrnfromNo = int.Parse(txtfromtrnno.Text.ToString());

            }
            else
            {
                //Trnfrom = ;

            }

            if (txttotrnno.Text.ToString() != null && txttotrnno.Text.ToString() != string.Empty)
            {
                TrntoNo = int.Parse(txttotrnno.Text.ToString());

            }
            else
            {
                //Trnto = 5;
            }

            if (txtfrmdate.Text.Trim() != string.Empty && txttodate.Text.Trim().ToString() != string.Empty)
            {
                FromDate = DateTime.Parse(txtfrmdate.Text.Trim().ToString());
                ToDate = DateTime.Parse(txttodate.Text.Trim().ToString());
                if (ToDate <= Edate && Sdate <= FromDate)
                {
                    if (FromDate < ToDate)
                    {
                        from = txtfrmdate.Text.Trim().ToString();
                        to = txttodate.Text.Trim().ToString();
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
                if (txtfrmdate.Text.Trim() != string.Empty)
                {
                    FromDate = DateTime.Parse(txtfrmdate.Text.Trim().ToString());
                    if (FromDate >= Sdate && Edate >= FromDate)
                    {
                        from = txtfrmdate.Text.Trim().ToString();

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

                if (txtfrmdate.Text.Trim().ToString() != string.Empty)
                {
                    ToDate = DateTime.Parse(txtfrmdate.Text.Trim().ToString());

                    if (ToDate >= Sdate || Edate >= ToDate)
                    {

                        to = txtfrmdate.Text.Trim().ToString();

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


            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.FabricRecievingForQueryForm(PartyCode, FabricCode, from, to, TrnfromNo, TrntoNo);
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
                Clear1();
                //Clear1();
            }
        }
        catch
        {
            throw;
        }
    }


    protected void Clear_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void Exit_Click(object sender, ImageClickEventArgs e)
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
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GridView1.PageIndex = e.NewPageIndex;
            BindGridFabricRecieving();
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
                lblBranch = lblBranchCode.Text.Trim();

                Label LblTrnNum = (Label)e.Row.FindControl("lblTrnnumb");
                TrnNum = LblTrnNum.Text.Trim();

                Label lblpino = (Label)e.Row.FindControl("lblTrnpino");
                PINum = lblpino.Text.Trim();
                Label lblfbrType = (Label)e.Row.FindControl("lblfabric");
                FbrType = lblfbrType.Text.Trim();
                //Label LblShadeCode = (Label)e.Row.FindControl("lblShadeCode");
                //ShadeCode = LblShadeCode.Text.Trim();

                DataTable dtc = BindSupTranGrid(lblComp, lblBranch, TrnNum, PINum, FbrType);
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
    private DataTable BindSupTranGrid(string lblComp, string lblBranch, string TrnNum, string PINum, string FbrType)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_FABRIC_IR_MST.BindSubTranGrid(lblComp, lblBranch, TrnNum, PINum, FbrType);
               
            return dt;
        }
        catch
        {
            throw;
        }
    }

}
