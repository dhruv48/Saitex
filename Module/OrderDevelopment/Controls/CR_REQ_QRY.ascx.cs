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
using Obout.ComboBox;

public partial class Module_OrderDevelopment_Controls_CR_REQ_QRY : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

            if (!IsPostBack)
            {
                GetAllOrderCaptureApproval();
                InitiliseControl();
            }
        }
        catch (Exception ex)
        {
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in page loading.\r\nSee error log for detail."));
        }
    }

    private void GetAllOrderCaptureApproval()
    {
        try
        {
            
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.GetAllOrderCaptureApproval(oUserLoginDetail.COMP_CODE, oUserLoginDetail.CH_BRANCHCODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                grd_cr_req_qry.DataSource = dt;
                grd_cr_req_qry.DataBind();
                grd_cr_req_qry.Visible = true;
            }
            else
            {
                grd_cr_req_qry.DataSource = null;
                grd_cr_req_qry.DataBind();
                grd_cr_req_qry.Visible = false;
                Common.CommonFuction.ShowMessage("Record Not Available...");
            }


        }
        catch
        {
            throw;
        }
    }

    private void InitiliseControl()
    {
        grd_cr_req_qry.Visible = true;
        imgbtnClear.Visible = true;
        imgbtnExit.Visible = true;
    }
    
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        InitiliseControl();
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
            Common.CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit..\r\nSee error log for detail."));
           
        }
    }
    
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    
    protected void grd_cr_req_qry_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            GetAllOrderCaptureApproval();
            grd_cr_req_qry.PageIndex = e.NewPageIndex;
            grd_cr_req_qry.DataBind();  
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void txtPartyCodecmb_LoadingItems(object sender, ComboBoxLoadingItemsEventArgs e)
    {
        try
        {
            DataTable data = GetPartyData(e.Text.ToUpper(), e.ItemsOffset);

            txtPartyCodecmb.Items.Clear();

            txtPartyCodecmb.DataSource = data;
            txtPartyCodecmb.DataBind();

            e.ItemsLoadedCount = e.ItemsOffset + data.Rows.Count;
            e.ItemsCount = GetPartyCount(e.Text);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in party selection.\r\nSee error log for detail."));
         
        }
    }

    private DataTable GetPartyData(string Text, int startOffset)
    {
        try
        {
            string CommandText = "SELECT PRTY_CODE, PRTY_NAME, (PRTY_NAME || PRTY_ADD1) Address,PRTY_GRP_CODE FROM (SELECT PRTY_CODE, PRTY_NAME, PRTY_ADD1,PRTY_GRP_CODE FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') and ROWNUM <= 15 ";
            string whereClause = string.Empty;
            if (startOffset != 0)
            {
                whereClause += " AND PRTY_CODE NOT IN (SELECT PRTY_CODE FROM (SELECT PRTY_CODE,PRTY_GRP_CODE FROM TX_VENDOR_MST  WHERE PRTY_CODE LIKE :SearchQuery  OR PRTY_NAME LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<> upper('Transporter') and ROWNUM <= " + startOffset + ")";
            }

            string SortExpression = " order by PRTY_CODE";
            string SearchQuery = Text + "%";
            DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, whereClause, SortExpression, "", SearchQuery, "");

            return data;
        }
        catch
        {
            throw;
        }
    }

    protected int GetPartyCount(string text)
    {

        string CommandText = " SELECT PRTY_CODE,PRTY_GRP_CODE, PRTY_NAME, PRTY_ADD1, (PRTY_NAME || PRTY_ADD1) Address FROM (SELECT * FROM TX_VENDOR_MST WHERE PRTY_CODE LIKE :SearchQuery OR PRTY_NAME LIKE :SearchQuery OR PRTY_ADD1 LIKE :SearchQuery ORDER BY PRTY_CODE ASC) asd WHERE upper(PRTY_GRP_CODE)<>upper('Transporter') ";
        string WhereClause = " ";
        string SortExpression = " ";
        string SearchQuery = text.ToUpper() + "%";
        DataTable data = SaitexBL.Interface.Method.TX_ITEM_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, "");

        return data.Rows.Count;
    }

    protected void grd_cr_req_qry2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void grd_cr_req_qry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            int index = int.Parse(e.CommandArgument.ToString());
            GridViewRow row = grd_cr_req_qry.Rows[index];

            Label lblCOMP_CODE = (Label)row.FindControl("lblCOMP_CODE");
            Label lblBRANCH_CODE = (Label)row.FindControl("lblBRANCH_CODE");
            Label lblORDER_NO = (Label)row.FindControl("lblORDER_NO");
            Label lblPI_NO = (Label)row.FindControl("lblPI_NO");
            Label lblPRTY_CODE = (Label)row.FindControl("lblPRTY_CODE");

            string comp = lblCOMP_CODE.Text;
            string branch = lblBRANCH_CODE.Text;
            string order_no = lblORDER_NO.Text;
            string pi_no = lblPI_NO.Text;
            string prty_code = lblPRTY_CODE.Text;

            Bind2ndGrd(comp, branch, order_no, pi_no, prty_code);

        }
        catch (Exception ex)
        {
            throw ex;
        }
  
    }
    
    private void Bind2ndGrd(string comp, string branch, string order_no, string pi_no, string prty_code)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.OD_CAPTURE_MST.BindGridByGrid(comp, branch, order_no, pi_no, prty_code);
            if (dt != null && dt.Rows.Count > 0)
            {
                grd_cr_req_qry2.DataSource = dt;
                grd_cr_req_qry2.DataBind();
                grd_cr_req_qry2.Visible = true;
            }
            else
            {
                grd_cr_req_qry2.DataSource = null;
                grd_cr_req_qry2.DataBind();
                grd_cr_req_qry2.Visible = false;
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void grd_cr_req_qry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // e.Row.Cells[0].Style.Add(HtmlTextWriterStyle.Visibility, "hidden");


                e.Row.Attributes["onclick"] = "__doPostBack('" + grd_cr_req_qry.UniqueID + "','Select$" + e.Row.RowIndex + "');";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    
    protected void grd_cr_req_qry2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblCOMP_CODE = (Label)e.Row.FindControl("lblCOMP_CODE");
                Label lblBRANCH_CODE = (Label)e.Row.FindControl("lblBRANCH_CODE");
                Label lblSHADE_CODE = (Label)e.Row.FindControl("lblSHADE_CODE");
                Label lblBUSINESS_TYPE = (Label)e.Row.FindControl("lblBUSINESS_TYPE");
                Label lblPRODUCT_TYPE = (Label)e.Row.FindControl("lblPRODUCT_TYPE");
                Label lblORDER_CAT = (Label)e.Row.FindControl("lblORDER_CAT");
                Label lblORDER_TYPE = (Label)e.Row.FindControl("lblORDER_TYPE");
                Label lblORDER_NO = (Label)e.Row.FindControl("lblORDER_NO");
                Label lblPI_TYPE = (Label)e.Row.FindControl("lblPI_TYPE");
                Label lblPI_NO = (Label)e.Row.FindControl("lblPI_NO");
                Label lblARTICAL_CODE = (Label)e.Row.FindControl("lblARTICAL_CODE");

                string comp = lblCOMP_CODE.Text;
                string branch = lblBRANCH_CODE.Text;
                string shade = lblSHADE_CODE.Text;
                string business_type = lblBUSINESS_TYPE.Text;
                string product_type = lblPRODUCT_TYPE.Text;
                string order_cat = lblORDER_CAT.Text;
                string order_type = lblORDER_TYPE.Text;
                string order_no = lblORDER_NO.Text;
                string pi_type = lblPI_TYPE.Text;
                string pi_no = lblPI_NO.Text;
                string article_code = lblARTICAL_CODE.Text;

                DataSet ds = BindePoSupGrid(comp, branch, shade, business_type, product_type, order_cat, order_type, order_no, pi_type, pi_no, article_code);

                GridView grd_cr = (GridView)e.Row.FindControl("grd_cr");
                if (ds != null && ds.Tables.Count > 0)
                {
                    grd_cr.DataSource = ds.Tables[0];
                    grd_cr.DataBind();
                }
                GridView grd_pack = (GridView)e.Row.FindControl("grd_pack");
                if (ds != null && ds.Tables.Count > 0)
                {
                    grd_pack.DataSource = ds.Tables[1];
                    grd_pack.DataBind();
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected DataSet BindePoSupGrid(string comp, string branch, string shade, string business_type, string product_type, string order_cat, string order_type, string order_no, string pi_type, string pi_no, string article_code)
    {
        try
        {
            DataSet ds = SaitexDL.Interface.Method.OD_CAPTURE_MST.GetAllNestedDetailForOCQ(comp, branch, shade, business_type, product_type, order_cat, order_type, order_no, pi_type, pi_no, article_code);
            return ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}
