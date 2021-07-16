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


public partial class Module_Inventory_Controls_VendorMasterQuery : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];

        if (!IsPostBack)
        {
            //BindParty();
            BindPrtyCity();
            Bindvendorcode();
            Bindvendorcategory();
            try
            {
                bindVendorMaster();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    private void BindPrtyCity()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorCity();
            ddlprtycity.Items.Clear();
            ddlprtycity.DataSource = dt;
            ddlprtycity.DataValueField = "PRTY_CITY";
            ddlprtycity.DataTextField = "PRTY_CITY";
            ddlprtycity.DataBind();
            ddlprtycity.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Bindvendorcode()
    {
        try
        {
            
            ddlvendorcode.Items.Clear();
            DataTable dt = GET_MOM_DATA("", "PRTY_GRP_CODE");
            ddlvendorcode.DataSource = dt;
            ddlvendorcode.DataValueField = "PRTY_GRP_CODE";
            ddlvendorcode.DataTextField = "PRTY_GRP_CODE";
            ddlvendorcode.DataBind();
            ddlvendorcode.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void Bindvendorcategory()
    {
        try
        {
          

            ddlcategory.Items.Clear();
            DataTable dt = GET_VEND_DATA("", "VENDOR_CAT_CODE");
            ddlcategory.DataSource = dt;
            ddlcategory.DataValueField = "VENDOR_CAT_CODE";
            ddlcategory.DataTextField = "VENDOR_CAT_CODE";
            ddlcategory.DataBind();
            ddlcategory.Items.Insert(0, new ListItem("--------SELECT-------", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private DataTable GET_VEND_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT VENDOR_CAT_CODE from TX_VENDOR_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'  ";
            string WhereClause = " and VENDOR_CAT_CODE like :SearchQuery";
            string SortExpression = " order by VENDOR_CAT_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }

    protected void imgbtnAddNew_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/Module/Yarn/SalesWork/Pages/YarnMaster.aspx", false);
    }  


 



    private DataTable GET_CR_LIMIT_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT CR_LIMIT from TX_VENDOR_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'  ";
            string WhereClause = " and CR_LIMIT like :SearchQuery";
            string SortExpression = " order by CR_LIMIT asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch (Exception EX)
        {
            throw EX;
        }
    }
  

    private DataTable bindVendorMaster()
    {        
        try
        {
          
            var  dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst2(txtPartyName.Text.Trim(), ddlprtycity.SelectedValue.ToString(), ddlvendorcode.SelectedValue, ddlcategory.SelectedValue, txtmobile.Text.Trim(), ddlstatus.SelectedValue, txtCredit.Text.Trim(), ddlregion.SelectedValue.ToString(), txtpincode.Text.Trim());
            if (dt.Rows.Count > 0)
            {
                Grid12.DataSource = dt;
                Grid12.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                Grid12.Visible = true;
            }
            else
            {
                Grid12.DataSource = dt;
                Grid12.DataBind();
                lblTotalRecord.Text = "0";
                Common.CommonFuction.ShowMessage("Record Not Available By Selected Parameter ");
            }
            return dt;

        }

        catch (Exception ex)
        {
            throw ex;
        }

    }

    private DataTable GET_MOM_DATA(string Text, string PRTY_GRP_CODE)
    {
        try
        {
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            string CommandText = "select DISTINCT PRTY_GRP_CODE from TX_VENDOR_MST where Del_Status=0 and  comp_code='" + oUserLoginDetail.COMP_CODE + "'  ";
            string WhereClause = " and PRTY_GRP_CODE like :SearchQuery";
            string SortExpression = " order by PRTY_GRP_CODE asc";
            string SearchQuery = Text + "%";
            DataTable dt = SaitexBL.Interface.Method.TX_VENDOR_MST.GetDataForLOV(CommandText, WhereClause, SortExpression, "", SearchQuery, PRTY_GRP_CODE, oUserLoginDetail.COMP_CODE);
            return dt;
        }
        catch(Exception EX)
        {
            throw EX;
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
       
        try
        {  string URL = "../Reports/VendorMasterRpt.aspx?PRTY_CODE=" + txtPartyName.Text.Trim() + "&PRTY_CITY=" + ddlprtycity.SelectedValue + "&VENDOR_CODE=" + ddlvendorcode.SelectedValue + "&VENDOR_CAT=" + ddlcategory.SelectedValue + "&MOBILENO=" +  txtmobile.Text + "&STATUS=" + ddlstatus.SelectedValue + "&CREDITLIMIT=" + txtCredit.Text + "&REGION=" +  ddlregion.SelectedValue + "&PINCODE" + txtpincode.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btngetdata_Click(object sender, EventArgs e)
    {
        try
        {
            bindVendorMaster();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void Grid12_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            bindVendorMaster();

            Grid12.PageIndex = e.NewPageIndex;
            Grid12.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "Vendor_Master_List_" + DateTime.Now.ToString() + ".xls";
        ExporttoExcel(bindVendorMaster(), strFilename, "Party/Vendor/Transporter/Broker/Spinner Master List");
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
        HttpContext.Current.Response.Write("</TR>");
        foreach (DataRow row in table.Rows)
        {//write in new row
            HttpContext.Current.Response.Write("<TR>");
            for (int i = 0; i < table.Columns.Count; i++)
            {
                HttpContext.Current.Response.Write("<Td>");
                HttpContext.Current.Response.Write(row[i].ToString());
                HttpContext.Current.Response.Write("</Td>");
            }

            HttpContext.Current.Response.Write("</TR>");
        }
        HttpContext.Current.Response.Write("</Table>");
        HttpContext.Current.Response.Write("</font>");
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }





}
