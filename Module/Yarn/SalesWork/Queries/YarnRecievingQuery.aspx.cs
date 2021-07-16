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
public partial class Module_Yarn_SalesWork_Queries_YarnRecievingQuery : System.Web.UI.Page
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
            txtTrnFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            txttoTrndate.Text = Common.CommonFuction.GetYearEndDate(DateTime.Parse(txtTrnFromDate.Text)).ToShortDateString();
            BindDepartment(ddlstore);
            BindDropDown(ddllocation);
            cmbShade.SelectedIndex = -1;
            BindTrnType();
            ViewState["GridRecord"] = null;
        }
        catch
        {
            throw;
        }
    }

    private void BindTrnType()
    {
        try
        {
            ddlTrnType.Items.Clear();

            DataTable dt = SaitexBL.Interface.Method.YRN_MST.getTransType();
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView DV = dt.DefaultView;
                DV.RowFilter = "TRN_TYPE LIKE '%R%'";
                ddlTrnType.DataTextField = "TRN_TDESC";
                ddlTrnType.DataValueField = "TRN_TYPE";
                ddlTrnType.DataSource = DV;// dt;
                ddlTrnType.DataBind();
            }
            ddlTrnType.Items.Insert(0, new ListItem("-----------All---------------", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindGridYarnRecieving()
    {
        try
        {
            string YarnCode;
            string PartyCode;
            int TrnfromNo = 0;
            int TrntoNo = 0;
            string LOCATION;
            string STORE;
            string YARN_SHADE_FAMILY = string.Empty;
            string YARN_SHADE = string.Empty;
            string TRN_TYPE = string.Empty;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);


            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                YARN_SHADE = cmbShade.SelectedValue.Trim().ToString();
            }
            else
            {
                YARN_SHADE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedText))
            {
                YARN_SHADE_FAMILY = cmbShade.SelectedText;
            }
            else
            {
                YARN_SHADE_FAMILY = string.Empty;
            }
            if (ddlYarn.SelectedIndex > -1)
            {
                YarnCode = ddlYarn.SelectedValue.ToString();
            }
            else
            {
                YarnCode = string.Empty;
            }



            if (ddlParty.SelectedIndex > 0)
            {
                PartyCode = ddlParty.SelectedValue.ToString();
            }
            else
            {
                PartyCode = string.Empty;
            }
            if (ddllocation.SelectedIndex > 0)
            {
                LOCATION = ddllocation.SelectedValue.ToString();
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (ddlstore.SelectedIndex > 0)
            {
                STORE = ddlstore.SelectedValue.ToString();
            }
            else
            {
                STORE = string.Empty;
            }
            if (ddlTrnType.SelectedItem.Value != null && ddlTrnType.SelectedItem.Value != string.Empty)
            {
                TRN_TYPE = ddlTrnType.SelectedItem.Value;

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


            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.YrnRecievingForQueryForm(PartyCode, YarnCode, from, to, TrnfromNo, TrntoNo,LOCATION,STORE,YARN_SHADE_FAMILY,YARN_SHADE,TRN_TYPE);
            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                ViewState["GridRecord"] = dt;
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
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string YarnCode;
            string PartyCode;
            int TrnfromNo = 0;
            int TrntoNo = 0;
            string LOCATION;
            string STORE;
            string YARN_SHADE_FAMILY = string.Empty;
            string YARN_SHADE = string.Empty;
            string TRN_TYPE = string.Empty;
            DateTime Sdate = oUserLoginDetail.DT_STARTDATE;
            DateTime Edate = Common.CommonFuction.GetYearEndDate(Sdate);


            if (!string.IsNullOrEmpty(cmbShade.SelectedValue))
            {
                YARN_SHADE = cmbShade.SelectedValue.Trim().ToString();
            }
            else
            {
                YARN_SHADE = string.Empty;
            }
            if (!string.IsNullOrEmpty(cmbShade.SelectedText))
            {
                YARN_SHADE_FAMILY = cmbShade.SelectedText;
            }
            else
            {
                YARN_SHADE_FAMILY = string.Empty;
            }
            if (ddlYarn.SelectedIndex > -1)
            {
                YarnCode = ddlYarn.SelectedValue.ToString();
            }
            else
            {
                YarnCode = string.Empty;
            }



            if (ddlParty.SelectedIndex > 0)
            {
                PartyCode = ddlParty.SelectedValue.ToString();
            }
            else
            {
                PartyCode = string.Empty;
            }
            if (ddllocation.SelectedIndex > 0)
            {
                LOCATION = ddllocation.SelectedValue.ToString();
            }
            else
            {
                LOCATION = string.Empty;
            }
            if (ddlstore.SelectedIndex > 0)
            {
                STORE = ddlstore.SelectedValue.ToString();
            }
            else
            {
                STORE = string.Empty;
            }
            if (ddlTrnType.SelectedItem.Value != null && ddlTrnType.SelectedItem.Value != string.Empty)
            {
                TRN_TYPE = ddlTrnType.SelectedItem.Value;

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
            string URL = "../Reports/YarnRecevingQueryReport.aspx?";
            URL += "Party_Code=" + PartyCode;
            URL += "&YARN_CODE=" + YarnCode;
            URL += "&STORE=" + STORE;
            URL += "&Comp_Code=" + oUserLoginDetail.COMP_CODE;
            URL += "&Branch_Code=" + oUserLoginDetail.CH_BRANCHCODE;
            URL += "&Sdate=" + from;
            URL += "&Edate=" + to;
            URL += "&FromTrnNumb=" + TrnfromNo;
            URL += "&ToTrnNumb=" + TrntoNo;
            URL += "&YARN_SHADE_FAMILY=" + YARN_SHADE_FAMILY;
            URL += "&YARN_SHADE=" + YARN_SHADE;
            URL += "&TRN_TYPE=" + TRN_TYPE;
            URL += "&LOCATION=" + LOCATION;
            if (ddlTrnType.SelectedIndex != 0)
            {
                string tst = ddlTrnType.SelectedItem.Text;
                string[] word = tst.Split('-');

                URL += "&TRN_DESC=" + word[1].ToString();//ddlTrnType.SelectedItem.Text.Trim()
            }
            else
            {
                URL += "&TRN_DESC=Purchase Inward Report";
            }
            Response.Redirect(URL);
        }
        catch (Exception exx) { };
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
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarn();
            DataView Dv = new DataView(dt);
            ddlYarn.DataSource = Dv;
            ddlYarn.DataValueField = "YARN_CODE";
            ddlYarn.DataTextField = "YARN_CODE";
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
        string CommandText = " SELECT   *  FROM   (  SELECT   YARN_CODE, YARN_DESC, YARN_TYPE FROM   YRN_MST WHERE      YARN_CODE LIKE :SearchQuery OR YARN_TYPE LIKE :SearchQuery OR YARN_DESC LIKE :SearchQuery ORDER BY   YARN_CODE) asd WHERE YARN_TYPE <> 'SEWING THREAD'";
        string WhereClause = " ";
        string SortExpression = " ORDER BY YARN_CODE ";
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
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Yarn loading.\r\nSee error log for detail."));
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
                Label lblYrnType = (Label)e.Row.FindControl("lblyarn");
                YrnType = lblYrnType.ToolTip.Trim();
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
        catch(Exception ex)
        {
            throw ex;
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
    private void BindDropDown(DropDownList ddllocation)
    {
        try
        {
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_LOCATION", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {


                ddllocation.DataSource = dt;
                ddllocation.DataValueField = "MST_DESC";
                ddllocation.DataTextField = "MST_DESC";
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                ddllocation.DataSource = null;
                ddllocation.DataBind();
                ddllocation.Items.Insert(0, new ListItem(oUserLoginDetail.VC_BRANCHNAME, oUserLoginDetail.VC_BRANCHNAME));

            }
            ddllocation.SelectedIndex = ddllocation.Items.IndexOf(ddllocation.Items.FindByText(oUserLoginDetail.VC_BRANCHNAME));
        }
        catch
        {
            throw;
        }
    }
    private void BindDepartment(DropDownList ddl)
    {
        try
        {
            ddl.Items.Clear();
            DataTable dt = SaitexDL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME("WAREHOUSE_STORE", oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddl.DataSource = dt;
                ddl.DataTextField = "MST_DESC";
                ddl.DataValueField = "MST_DESC";
                ddl.DataBind();
                ddl.Items.Insert(0, new ListItem("--Select--", ""));
            }
            else
            {
                DataTable dtDepartment = SaitexBL.Interface.Method.CM_DEPT_MST.Select();
                if (dtDepartment != null && dtDepartment.Rows.Count > 0)
                {
                    ddl.DataSource = dtDepartment;
                    ddl.DataTextField = "DEPT_NAME";
                    ddl.DataValueField = "DEPT_NAME";
                    ddl.DataBind();
                }
            }
            ddl.SelectedIndex = ddl.Items.IndexOf(ddl.Items.FindByText(oUserLoginDetail.VC_DEPARTMENTNAME));
        }
        catch
        {
            throw;
        }
    }

    protected void cmbShade_LoadingItems(object sender, Obout.ComboBox.ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetShadeItems(e.Text.ToUpper(), e.ItemsOffset);
            // Looping through the items and adding them to the "Items" collection of the ComboBox
            if (data != null && data.Rows.Count > 0)
            {
                cmbShade.Items.Clear();
                cmbShade.DataSource = data;
                cmbShade.DataBind();
            }
            // Calculating the numbr of items loaded so far in the ComboBox
            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            // Getting the total number of items that start with the typed text
            e.ItemsCount = GetShadeItemsCount(e.Text);
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Shade Loading.\r\nSee error log for detail."));

        }
    }

    protected DataTable GetShadeItems(string text, int startOffset)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_NAME ||'@' ||T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= 15 ";
            string whereClause = string.Empty;

            if (startOffset != 0)
            {
                whereClause += " AND combined NOT IN (SELECT Combined FROM (SELECT * FROM (SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, (M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE  || '@' || T.SHADE_NAME) AS Combined FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) WHERE ROWNUM <= " + startOffset + ") ";
            }
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

        }
        catch
        {
            throw;
        }
    }

    protected int GetShadeItemsCount(string text)
    {
        try
        {
            string CommandText = " SELECT * FROM (SELECT * FROM ( SELECT M.SHADE_FAMILY_CODE, M.SHADE_FAMILY_NAME, T.SHADE_CODE, T.SHADE_NAME, ( M.SHADE_FAMILY_CODE || '@' || M.SHADE_FAMILY_NAME || '@' || T.SHADE_CODE || '@' || T.SHADE_NAME) AS Combined  FROM OD_SHADE_FAMILY_MST M, OD_SHADE_FAMILY_TRN T WHERE M.COMP_CODE = '" + oUserLoginDetail.COMP_CODE + "' AND M.PRODUCT_TYPE = 'YARN' AND M.SHADE_FAMILY_CODE = T.SHADE_FAMILY_CODE ORDER BY SHADE_FAMILY_CODE) ASD WHERE SHADE_FAMILY_CODE LIKE :SearchQuery OR SHADE_FAMILY_NAME LIKE :SearchQuery OR SHADE_CODE LIKE :SearchQuery OR SHADE_NAME LIKE :SearchQuery) ";
            string WhereClause = " ";
            string SortExpression = " ORDER BY SHADE_FAMILY_CODE ";
            string SearchQuery = text.ToUpper() + "%";
            return SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "").Rows.Count;
        }
        catch
        {
            throw;
        }
    }

    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Yarn_Receving_" + DateTime.Now.ToShortDateString() + ".xls";
       DateTime Sdate = DateTime.Parse(txtTrnFromDate.Text);
       DateTime Edate = DateTime.Parse(txttoTrndate.Text);

       DataTable data = (DataTable)ViewState["GridRecord"];

        ExporttoExcel(data, strFilename, "Yarn_Receving_Query");
    }



    private void ExporttoExcel(DataTable table, string name, string title)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=" + name);

        HttpContext.Current.Response.Charset = "utf-8";
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.GetEncoding("windows-1250");
        //sets font
        HttpContext.Current.Response.Write("<font style='font-size:10.0pt; font-family:Calibri;'>");
        HttpContext.Current.Response.Write("<BR><BR><BR>");
        //sets the table border, cell spacing, border color, font of the text, background, foreground, font height
        HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
          "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
          "style='font-size:10.0pt; font-family:Calibri; background:white;'>");
        //am getting my grid's column headers
        int columnscount = table.Columns.Count;
        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:14.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(oUserLoginDetail.VC_COMPANYNAME);
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD style='font-size:12.0pt;' align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write(" " + title + " ");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");

        HttpContext.Current.Response.Write("<TR>");
        HttpContext.Current.Response.Write("<TD  align='center' colspan=" + columnscount + ">");
        HttpContext.Current.Response.Write("<B>");
        HttpContext.Current.Response.Write("DATED:" + DateTime.Now.ToString() + "");
        HttpContext.Current.Response.Write("</B>");
        HttpContext.Current.Response.Write("</TD>");
        HttpContext.Current.Response.Write("</TR>");


        HttpContext.Current.Response.Write("<TR>");

        foreach (DataColumn dtcol in table.Columns)
        {
            HttpContext.Current.Response.Write("<Td>");
            HttpContext.Current.Response.Write("<B>");
            HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
            HttpContext.Current.Response.Write("</B>");
            HttpContext.Current.Response.Write("</Td>");

        }

        //HttpContext.Current.Response.Write("<Td align='center' valing='top'>");
        //HttpContext.Current.Response.Write("<B>");
        //HttpContext.Current.Response.Write("Recipe Details");
        //HttpContext.Current.Response.Write("</B>");
        //HttpContext.Current.Response.Write("</Td>");

        //HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                HttpContext.Current.Response.Write(row[i].ToString());
                //******************************************//
                //      if (i == 15)
                //      {

                //          HttpContext.Current.Response.Write("<Td align=left valign=Top>");

                //          DataView dvBASEQUALITY = new DataView(dtBASEQUALITY);
                //          dvBASEQUALITY.RowFilter = "CUSTOMER_REQ_NO='" + row["CUSTOMER_REQ_NO"].ToString() + "' and LAB_DIP_NO='" + row["LAB_DIP_NO"].ToString() + "' AND LR_OPTION='" + row["LR_OPTION"].ToString() + "'";

                //          if (dvBASEQUALITY.Count > 0)
                //          {
                //              HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                //"borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                //"style='font-size:10.0pt; font-family:Calibri; background:white;'>");
                //              HttpContext.Current.Response.Write("<TR>");

                //              foreach (DataColumn dtcol in dtBASEQUALITY.Columns)
                //              {
                //                  HttpContext.Current.Response.Write("<Td bgcolor=silver>");
                //                  HttpContext.Current.Response.Write("<B>");
                //                  HttpContext.Current.Response.Write(dtcol.ColumnName.Replace("_", " "));
                //                  HttpContext.Current.Response.Write("</B>");
                //                  HttpContext.Current.Response.Write("</Td>");

                //              }
                //              HttpContext.Current.Response.Write("</TR>");
                //              for (int j = 0; j < dvBASEQUALITY.Count; j++)
                //              {
                //                  HttpContext.Current.Response.Write("<Tr>");
                //                  for (int i1 = 0; i1 < dvBASEQUALITY.Table.Columns.Count; i1++)
                //                  {
                //                      HttpContext.Current.Response.Write("<Td >");
                //                      HttpContext.Current.Response.Write(dvBASEQUALITY[j][i1].ToString());
                //                      HttpContext.Current.Response.Write("</Td>");

                //                  }
                //                  HttpContext.Current.Response.Write("</Tr>");
                //              }
                //              HttpContext.Current.Response.Write("</Table>");
                //          }
                //          HttpContext.Current.Response.Write("</Td>");

                //      }



                //***********************************************//   

                HttpContext.Current.Response.Write("</Td>");

            }

            HttpContext.Current.Response.Write("</TR>");


        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
        //  HttpContext.Current.ApplicationInstance.CompleteRequest();
    }
}
