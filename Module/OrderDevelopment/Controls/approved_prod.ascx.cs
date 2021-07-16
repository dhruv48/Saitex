using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using Common;
using errorLog;
using System.IO;
using System.Globalization;
using WCFMain;
using System.Data;
public partial class Module_OrderDevelopment_Controls_approved_prod : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
   
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        if (!IsPostBack)
        {
          
            bindviewgrd();
           
        }

    }

    protected void gvLogistic_PreRender(object sender, EventArgs e)
    {
        gvLogistic.UseAccessibleHeader = true;
        gvLogistic.HeaderRow.TableSection = TableRowSection.TableHeader;
    }

    protected void gvLogistic_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var grdRow = e.Row;
                var orderno = ((Label)e.Row.FindControl("lblOrderNo"));
                var approveddate = ((Label)e.Row.FindControl("lblApprovedDate"));
                var party = ((LinkButton)e.Row.FindControl("lnkPartyName"));
                var partyadd = ((Label)e.Row.FindControl("lblPartyAddress"));
                var PARTYCODE = ((Label)e.Row.FindControl("lblPartyCode"));
                var pano = ((Label)e.Row.FindControl("lblpano"));
                var lblARTICLEDESC = ((Label)e.Row.FindControl("lblARTICLEDESC"));
                var article_code = ((Label)e.Row.FindControl("lblARTICLECODE"));
                var lnkARTICLECODE = (LinkButton)e.Row.FindControl("lnkARTICLECODE");
                var shadecode = ((Label)e.Row.FindControl("lblGrey"));
                var ordqty = ((Label)e.Row.FindControl("lblQty"));
                var lblUOM = (Label)e.Row.FindControl("lblUOM");
                var fiber = ((Label)e.Row.FindControl("lblFIBERDESC"));
                var fibercode = ((Label)e.Row.FindControl("lblFIBERCODE"));
              //  var lnkbtnFIBERCODE = (LinkButton)e.Row.FindControl("lnkbtnFIBERCODE");
                var apprvqtyfiber = ((Label)e.Row.FindControl("lblWeight"));
                var uom = ((Label)e.Row.FindControl("lblWeightUom"));
                var stockqty = ((Label)e.Row.FindControl("lblWeight1"));
                var uom1 = ((Label)e.Row.FindControl("lblWeightUom1"));


                var issuebtn = ((Button)e.Row.FindControl("BtnISSUE"));





                double _apprvqtyfiber = 0;
                double _stockqty = 0;
                double.TryParse(apprvqtyfiber.Text, out _apprvqtyfiber);
                double.TryParse(stockqty.Text, out _stockqty);
                if (_stockqty < _apprvqtyfiber)
                {
                    issuebtn.Enabled = false;
                    //Common.CommonFuction.ShowMessage("stock qty is less then approve qty");
                }



            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in getting load of Approved Production Data.\r\nSee error log for detail."));
        }

    }

  
    //protected void ChkClosed_CheckedChanged(object sender, EventArgs e)
    //{
    //    try
    //    {

    //    //    var chk = ((CheckBox)(sender));
    //    //    var gv1 = ((GridViewRow)(chk.NamingContainer));


    //    //    var dtDetail = CreateDataTable();
    //    //    var chkconfirm = gv1.FindControl("chkApproved") as CheckBox;
    //    //    var chkclosed = gv1.FindControl("ChkClosed") as CheckBox;
    //    //    custapp ocustapp = new custapp();

    //    //    Label lblCompCode = (Label)gv1.FindControl("lblCompCode");
    //    //    Label lblBRANCH_CODE = (Label)gv1.FindControl("lblBranchCode");
    //    //    Label lblYEAR = (Label)gv1.FindControl("lblYear");

    //    //    Label lblOrderNo = (Label)gv1.FindControl("lblOrderNo");
    //    //    Label lblApprovedDate = (Label)gv1.FindControl("lblApprovedDate");
    //    //    Label lblPartyCode = (Label)gv1.FindControl("lblPartyCode");
    //    //    Label lblPartyAddress = (Label)gv1.FindControl("lblPartyAddress");
    //    //    Label lblpano = (Label)gv1.FindControl("lblpano");
    //    //    Label lblARTICLEDESC = (Label)gv1.FindControl("lblARTICLECODE");
    //    //    Label lblARTICLECODE = (Label)gv1.FindControl("lblARTICLECODE");
    //    //    LinkButton lnkARTICLECODE = (LinkButton)gv1.FindControl("lnkARTICLECODE");

    //    //    Label lblGrey = (Label)gv1.FindControl("lblGrey");
    //    //    Label lblQty = (Label)gv1.FindControl("lblQty");
    //    //    Label lblUOM = (Label)gv1.FindControl("lblUOM");
    //    //    Label lblFIBERDESC = (Label)gv1.FindControl("lblFIBERDESC");
    //    //    Label lblFIBERCODE = (Label)gv1.FindControl("lblFIBERCODE");
    //    //    LinkButton lnkbtnFIBERCODE = (LinkButton)gv1.FindControl("lnkbtnFIBERCODE");
    //    //    Label lblWeight = (Label)gv1.FindControl("lblWeight");
    //    //    Label lblWeightUom = (Label)gv1.FindControl("lblWeightUom");
    //    //    Label lblWeight1 = (Label)gv1.FindControl("lblWeight1");
    //    //    Label lblWeightUom1 = (Label)gv1.FindControl("lblWeightUom1");
          

    //    //     DataRow dr = dtDetail.NewRow();
    //    //     dr["ORDER_NO"] = lblOrderNo.Text;
    //    //     dr["TDATE"] = DateTime.Parse(lblApprovedDate.Text);
    //    //    dr["PRTY_CODE"] = lblPartyCode.Text;
    //    //    dr["PRTY_NAME"] = lblPartyAddress.Text;
    //    //   // dr["PARTY_ADDRESS"] = lblPartyAddress.Text;
    //    //  //  dr["PARTY_UNIT"] = lblPartyUnit.Text;
    //    //    //dr["PICKUP_DATE"] = DateTime.Parse(lblPickUpDate.Text);
    //    //    dr["PA_NO"] = lblpano.Text;
    //    //    dr["ARTICLE_CODE"] = lblARTICLECODE.Text;
    //    //    dr["ARTICLE_NAME"] = lblARTICLEDESC.Text;
    //    //    dr["GREY"] = lblGrey.Text;

    //    //    dr["UOM"] = lblUOM.Text;
    //    //    dr["QTY"] = double.Parse(lblQty.Text);
    //    //    dr["FIBER_CODE"] = lblFIBERCODE.Text;
    //    //    dr["FIBER_DESC"] = lblFIBERDESC.Text;
    //    //    dr["UOM"] = lblUOM.Text;
    //    //    dr["APPROVED_QTY"] = double.Parse(lblQty.Text);
         
          
    //    //    dr["ROWSTATE"] = "1";
    //    //    dr["TDATE"] = DateTime.Now.Date.ToString();
    //    //    dr["YEAR"] = lblYEAR.Text;
    //    //    dr["COMP_CODE"] = lblCompCode.Text;
    //    //    dr["BRANCH_CODE"] = lblBRANCH_CODE.Text;
       
    //    //    dtDetail.Rows.Add(dr);

    //    //    int result = SaitexDL.Interface.Method.LOGISTICS_MST.closeLogistic(oUserLoginDetail.UserCode, dtDetail);
    //    //    if (result > 0)
    //    //    {
    //    //        Common.CommonFuction.ShowMessage("Selected Logistic is closed Successfully.");
    //    //    }

    //    //    SearchbyKeywords();
           


    //    }
    //    catch (Exception ex)
    //    {
    //        Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Confirming.\r\nSee error log for detail."));
    //    }

    //}

    protected void BtnISSUE_Click(object sender, EventArgs e)
    {
        var chk = ((Button)(sender));
        var gv1 = ((GridViewRow)(chk.NamingContainer));
        var lblpano = (Label)gv1.FindControl("lblpano");
        var lblFIBERCODE = (Label)gv1.FindControl("lblFIBERCODE");

        Response.Redirect("~/Module/Fiber/Pages/FiberIssueAgnstPA.aspx?PI_NO=" + lblpano.Text.Trim() + "&FIBER_CODE=" + lblFIBERCODE.Text.Trim(), true);

    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("REGISTER_CALL.aspx", false);

    }

    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        var name = string.Empty;
        string strFilename = name + "ApprovedDetails_" + DateTime.Now.ToString() + ".xls";

        var ORDER_NO = ((TextBox)gvLogistic.HeaderRow.FindControl("txtOrderNo")).Text;
        var ORDER_DATE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtApprovedDate")).Text;
        var PRTY_NAME = ((TextBox)gvLogistic.HeaderRow.FindControl("txtPartyName")).Text;
        var PI_NO = ((TextBox)gvLogistic.HeaderRow.FindControl("txtpano")).Text;
        var ARTICAL_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtARTICLECODE")).Text;
        var SHADE_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtGrey")).Text;
        var ORD_QTY = ((TextBox)gvLogistic.HeaderRow.FindControl("txtQty")).Text;
       // var FIBER_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("TXTFIBERDESC")).Text;


       // var REQ_QTY = ((TextBox)gvLogistic.HeaderRow.FindControl("txtWEIGHT")).Text;
       // var REMQTY = ((TextBox)gvLogistic.HeaderRow.FindControl("txtWEIGHT1")).Text;

        var data = SaitexBL.Interface.Method.OD_CAPT_MST.GetAPPROVEDDetailsAuto1(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), "YARN SPINING", ORDER_NO, ORDER_DATE, PRTY_NAME, PI_NO, ARTICAL_CODE, SHADE_CODE, ORD_QTY);
        UploadDataTableToExcel(data, strFilename);

    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Logistics/pages/LIST_OF_REGISTER_CALLS.aspx", false);

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

    protected void FilterGrid_Click(object sender, ImageClickEventArgs e)
    {
        SearchbyKeywords();
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
        imgbtnAddNew.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Register New Call ')");

    }

    private void bindviewgrd()
    {
        try
        {
        

            DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.viewgrd(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), "YARN SPINING");
            if (dt != null && dt.Rows.Count > 0)
            {
                gvLogistic.DataSource = dt;
                gvLogistic.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                gvLogistic.DataSource = null;
                gvLogistic.DataBind();
                Common.CommonFuction.ShowMessage("No approved Details Available");
                lblTotalRecord.Text = "0";
            }
        }
        catch
        {
            throw;
        }
    }

    private DataTable CreateDataTable()
    {
        var dtLogistic = new DataTable();
        dtLogistic.Columns.Add("YEAR", typeof(int));
        dtLogistic.Columns.Add("COMP_CODE", typeof(string));
        dtLogistic.Columns.Add("BRANCH_CODE", typeof(string));
        dtLogistic.Columns.Add("LOGISTIC_ID", typeof(string));
        dtLogistic.Columns.Add("LOGISTIC_DATE", typeof(DateTime));
        dtLogistic.Columns.Add("PARTY_CODE", typeof(string));
        dtLogistic.Columns.Add("PARTY_NAME", typeof(string));
        dtLogistic.Columns.Add("PARTY_ADDRESS", typeof(string));
        dtLogistic.Columns.Add("PARTY_UNIT", typeof(string));
        dtLogistic.Columns.Add("PICKUP_DATE", typeof(DateTime));
        dtLogistic.Columns.Add("GREY", typeof(string));
        dtLogistic.Columns.Add("GREY_DESC", typeof(string));
        dtLogistic.Columns.Add("UOM", typeof(string));
        dtLogistic.Columns.Add("QTY", typeof(double));
        dtLogistic.Columns.Add("REC_QT", typeof(double));
        dtLogistic.Columns.Add("VEHICLE_NO", typeof(string));
        dtLogistic.Columns.Add("ZONE", typeof(string));
        dtLogistic.Columns.Add("ISRESHEDULED", typeof(string));
        dtLogistic.Columns.Add("RESHEDULED_BY", typeof(string));
        dtLogistic.Columns.Add("RESHEDULED_DATE", typeof(DateTime));
        dtLogistic.Columns.Add("ISCLOSED", typeof(string));
        dtLogistic.Columns.Add("CLOSED_BY", typeof(string));
        dtLogistic.Columns.Add("CLOSED_DATE", typeof(DateTime));
        dtLogistic.Columns.Add("CONF_DATE", typeof(DateTime));
        dtLogistic.Columns.Add("CONF_FLAG", typeof(string));
        dtLogistic.Columns.Add("TDATE", typeof(string));
        dtLogistic.Columns.Add("TUSER", typeof(string));
        dtLogistic.Columns.Add("REMARKS", typeof(string));
        dtLogistic.Columns.Add("ROWSTATE", typeof(string));
        dtLogistic.Columns.Add("CARRY_TYPE", typeof(string));
        return dtLogistic;
    }

    private DataTable CreatedtForGrid()
    {
        try
        {
            var dtLogistic = new DataTable();
            dtLogistic.Columns.Add("YEAR", typeof(int));
            dtLogistic.Columns.Add("COMP_CODE", typeof(string));
            dtLogistic.Columns.Add("BRANCH_CODE", typeof(string));
            dtLogistic.Columns.Add("LOGISTIC_ID", typeof(string));
            dtLogistic.Columns.Add("LOGISTIC_DATE", typeof(DateTime));
            dtLogistic.Columns.Add("PARTY_CODE", typeof(string));
            dtLogistic.Columns.Add("PARTY_NAME", typeof(string));
            dtLogistic.Columns.Add("PARTY_ADDRESS", typeof(string));
            dtLogistic.Columns.Add("PARTY_UNIT", typeof(string));
            dtLogistic.Columns.Add("PICKUP_DATE", typeof(DateTime));
            dtLogistic.Columns.Add("GREY", typeof(string));
            dtLogistic.Columns.Add("GREY_DESC", typeof(string));
            dtLogistic.Columns.Add("UOM", typeof(string));
            dtLogistic.Columns.Add("QTY", typeof(double));
            dtLogistic.Columns.Add("REC_QT", typeof(double));
            dtLogistic.Columns.Add("VEHICLE_NO", typeof(string));
            dtLogistic.Columns.Add("ZONE", typeof(string));
            dtLogistic.Columns.Add("ISRESHEDULED", typeof(string));
            dtLogistic.Columns.Add("RESHEDULED_BY", typeof(string));
            dtLogistic.Columns.Add("RESHEDULED_DATE", typeof(DateTime));
            dtLogistic.Columns.Add("ISCLOSED", typeof(string));
            dtLogistic.Columns.Add("CLOSED_BY", typeof(string));
            dtLogistic.Columns.Add("CLOSED_DATE", typeof(DateTime));
            dtLogistic.Columns.Add("CONF_DATE", typeof(DateTime));
            dtLogistic.Columns.Add("CONF_FLAG", typeof(string));
            dtLogistic.Columns.Add("TDATE", typeof(string));
            dtLogistic.Columns.Add("TUSER", typeof(string));
            dtLogistic.Columns.Add("REMARKS", typeof(string));
            dtLogistic.Columns.Add("ROWSTATE", typeof(string));
            dtLogistic.Columns.Add("CARRY_TYPE", typeof(string));
            return dtLogistic;
        }
        catch
        {
            throw;
        }
    }

  
    private void SearchbyKeywords()
    {
        try
        {
          
              
            var ORDER_NO = ((TextBox)gvLogistic.HeaderRow.FindControl("txtOrderNo")).Text;
            var ORDER_DATE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtApprovedDate")).Text;
            var PRTY_NAME = ((TextBox)gvLogistic.HeaderRow.FindControl("txtPartyName")).Text;
            var PI_NO = ((TextBox)gvLogistic.HeaderRow.FindControl("txtpano")).Text;
            var ARTICAL_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtARTICLECODE")).Text;
            var SHADE_CODE = ((TextBox)gvLogistic.HeaderRow.FindControl("txtGrey")).Text;
            var ORD_QTY = ((TextBox)gvLogistic.HeaderRow.FindControl("txtQty")).Text;
            //var FIBER_CODE= ((TextBox)gvLogistic.HeaderRow.FindControl("TXTFIBERDESC")).Text;
            

            //var REQ_QTY = ((TextBox)gvLogistic.HeaderRow.FindControl("txtWEIGHT")).Text;
            //var REMQTY = ((TextBox)gvLogistic.HeaderRow.FindControl("txtWEIGHT1")).Text;
      
            var o = new AutoComplete();

              DataTable dt = SaitexBL.Interface.Method.OD_CAPT_MST.GetAPPROVEDDetailsAuto(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE, oUserLoginDetail.DT_STARTDATE.Year.ToString(), "YARN SPINING",ORDER_NO,ORDER_DATE,PRTY_NAME,PI_NO,ARTICAL_CODE,SHADE_CODE,ORD_QTY);
            if (dt != null && dt.Rows.Count > 0)
            {
                gvLogistic.DataSource = dt;
                gvLogistic.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString();
            }
            else
            {
                gvLogistic.DataSource = null;
                gvLogistic.DataBind();
                Common.CommonFuction.ShowMessage("No approved Details Available");
                lblTotalRecord.Text = "0";
                AutofillSearchContent(ORDER_NO,ORDER_DATE,PRTY_NAME,PI_NO,ARTICAL_CODE,SHADE_CODE,ORD_QTY);
            }
            AutofillSearchContent(ORDER_NO, ORDER_DATE, PRTY_NAME, PI_NO, ARTICAL_CODE, SHADE_CODE, ORD_QTY);
        }
             
    
       catch
       {
          throw;
        }
    }

    private void AutofillSearchContent(string ORDER_NO,string ORDER_DATE,string PRTY_NAME,string PI_NO,string ARTICAL_CODE,string SHADE_CODE,string ORD_QTY)
    {

        try
        {
            var tORDER_NO = (TextBox)gvLogistic.HeaderRow.FindControl("txtOrderNo");
            var tORDER_DATE = (TextBox)gvLogistic.HeaderRow.FindControl("txtApprovedDate");
            var tPRTY_NAME = (TextBox)gvLogistic.HeaderRow.FindControl("txtPartyName");
            var tPI_NO = (TextBox)gvLogistic.HeaderRow.FindControl("txtpano");
            var tARTICAL_CODE = (TextBox)gvLogistic.HeaderRow.FindControl("txtARTICLECODE");
            var tSHADE_CODE = (TextBox)gvLogistic.HeaderRow.FindControl("txtGrey");
            var tORD_QTY = (TextBox)gvLogistic.HeaderRow.FindControl("txtQty");
            //var tFIBER_CODE = (TextBox)gvLogistic.HeaderRow.FindControl("TXTFIBERDESC");
            //var tREQ_QTY = (TextBox)gvLogistic.HeaderRow.FindControl("txtWEIGHT");
            //var tREMQTY = (TextBox)gvLogistic.HeaderRow.FindControl("txtWEIGHT1");
         

            tORDER_NO.Text = ORDER_NO;
            tORDER_DATE.Text = ORDER_DATE;
            tPRTY_NAME.Text = PRTY_NAME;
            tPI_NO.Text = PI_NO;
            tARTICAL_CODE.Text = ARTICAL_CODE;
            tSHADE_CODE.Text = SHADE_CODE;
            tORD_QTY.Text = ORD_QTY;
            //tFIBER_CODE.Text = FIBER_CODE;
            //tREQ_QTY.Text = REQ_QTY;
            //tREMQTY.Text = REMQTY;
           

        }
        catch
        {
            throw;
        }

    }

    protected void gvLogistic_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvLogistic.PageIndex = e.NewPageIndex;
        bindviewgrd();
    }

    private bool CheckQty(string Qty, string RecQty)
    {
        double qty = double.Parse(Qty.ToString());
        double recqty = double.Parse(RecQty.ToString());
        bool result = false;
        if (recqty.Equals(0))
        {
            result = false;

        }
        else
        {
            result = true;
        }



        return result;
    }

  
    protected void UploadDataTableToExcel(DataTable dtEmp, string filename)
    {
        string attachment = "attachment; filename=" + filename;
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/vnd.ms-excel";
        string tab = string.Empty;
        foreach (DataColumn dtcol in dtEmp.Columns)
        {
            Response.Write(tab + dtcol.ColumnName);
            tab = "\t";
        }
        Response.Write("\n");
        foreach (DataRow dr in dtEmp.Rows)
        {
            tab = "";
            for (int j = 0; j < dtEmp.Columns.Count; j++)
            {
                Response.Write(tab + Convert.ToString(dr[j]));
                tab = "\t";
            }
            Response.Write("\n");
        }
        Response.End();
    }

  

}
