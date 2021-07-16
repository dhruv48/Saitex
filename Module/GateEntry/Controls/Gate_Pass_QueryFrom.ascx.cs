using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using System.Data;

public partial class Module_GateEntry_Controls_Gate_Pass_QueryFrom : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            if (!IsPostBack)
            {
                pageInstial();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnUpdate_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Updating the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Deleting the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnFindTop_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Finding the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            Response.Redirect("./Gate_Pass_QueryFrom.aspx", false);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Clearing the data.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
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
                Response.Redirect("~/Admin/Pages/welcome.aspx", false);
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in Exit.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected void imgbtnHelp_Click(object sender, ImageClickEventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(errorLog.ErrHandler.LogError(ex, @"Problem in help.\r\nSee error log for detail."));
            lblMode.Text = ex.ToString();
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        imgbtnUpdate.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Save this record ? ')");
        imgbtnClear.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Clear this record ? ')");
        imgbtnPrint.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Print this record ? ')");
        imgbtnExit.Attributes.Add("OnClick", "return window.confirm('Are you sure you want to Exit ')");
    }
    protected void pageInstial()
    {
        try
        {
            Branch_bind();
            GridBind();
            Year_bind();
            LotNoBind();
            LooryNoBind();
            InvoiceNoBind();
            PartyNameBind();
            TrspNameBind();
            PartyNameBind();
            GateNoBind();
            YranCodeBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GridBind()
    {
        
            string BRANCH = string.Empty;
            string LOTNO = string.Empty;
            string YARNCODE = string.Empty;
            string VECHILNO = string.Empty;
            string TRSPNAME = string.Empty;
            string FROMDATE= string.Empty;
            string TODATE = string.Empty;
            string YEAR = string.Empty;
            string GATENO = string.Empty;
            string PRTY = string.Empty;
            string invoice = string.Empty;

            try
            {
                if (ddlparty.SelectedValue.ToString() != null && ddlparty.SelectedValue.ToString() != string.Empty && ddlparty.SelectedIndex > 0)
                {
                    PRTY = ddlparty.SelectedValue.ToString();
                }
                else
                {
                    PRTY = string.Empty;
                }
                if (ddlinvoice.SelectedValue.ToString() != null && ddlinvoice.SelectedValue.ToString() != string.Empty && ddlinvoice.SelectedIndex > 0)
                {
                    invoice = ddlinvoice.SelectedValue.ToString();
                }
                else
                {
                    invoice = string.Empty;
                }
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty && ddlbranch.SelectedIndex > 0)
            {
                BRANCH = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty && ddllotno.SelectedIndex > 0)
            {
                LOTNO = ddllotno.SelectedValue.ToString();
            }
            else
            {
                LOTNO = string.Empty;
            }
            if (ddlyarn.SelectedValue.ToString() != null && ddlyarn.SelectedValue.ToString() != string.Empty && ddlyarn.SelectedIndex > 0)
            {
                YARNCODE = ddlyarn.SelectedValue.ToString();
            }
            else
            {
                YARNCODE = string.Empty;
            }
            if (ddlvechilno.SelectedValue.ToString() != null && ddlvechilno.SelectedValue.ToString() != string.Empty && ddlvechilno.SelectedIndex > 0)
            {
                VECHILNO = ddlvechilno.SelectedValue.ToString();
            }
            else
            {
                VECHILNO = string.Empty;
            }
            if (ddltrspname.SelectedValue.ToString() != null && ddltrspname.SelectedValue.ToString() != string.Empty && ddltrspname.SelectedIndex > 0)
            {
                TRSPNAME = ddltrspname.SelectedValue.ToString();
            }
            else
            {
                TRSPNAME = string.Empty;
            }
            if (ddlgateno.SelectedValue.ToString() != null && ddlgateno.SelectedValue.ToString() != string.Empty && ddlgateno.SelectedIndex > 0)
            {
                GATENO = ddlgateno.SelectedValue.ToString();
            }
            else
            {
                GATENO = string.Empty;
            }
            if (txtFROMDATE.Text.Trim().ToString() != null && txtFROMDATE.Text.Trim().ToString() != string.Empty && txtTODATE.Text.Trim().ToString() != null && txtTODATE.Text.Trim().ToString() != string.Empty)
            {
                FROMDATE = txtFROMDATE.Text.Trim().ToString();
                TODATE = txtTODATE.Text.Trim().ToString();
            }
            else
            {
                FROMDATE = string.Empty;
                TODATE = string.Empty;
            }
            if (ddlyear.SelectedValue.ToString() != null && ddlyear.SelectedValue.ToString() != string.Empty && ddlyear.SelectedIndex > 0)
            {
                YEAR = ddlgateno.SelectedValue.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetGatePass(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE, BRANCH, LOTNO, YARNCODE, VECHILNO, TRSPNAME, FROMDATE, TODATE, YEAR, GATENO, invoice, PRTY);
            if (dt != null && dt.Rows.Count > 0)
            {
                grGatePass.DataSource = dt;
                grGatePass.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

                grGatePass.Visible = true;
            }
            else
            {
                grGatePass.DataSource = null;
                grGatePass.DataBind();
                lblTotalRecord.Text = "0";
                grGatePass.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }




        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void Branch_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(oUserLoginDetail.COMP_CODE);

            ddlbranch.Items.Clear();
            DataView dv = new DataView(dt);
            ddlbranch.DataSource = dv;
            ddlbranch.DataValueField = "BRANCH_CODE";
            ddlbranch.DataTextField = "BRANCH_NAME";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void Year_bind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getYear(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlyear.Items.Clear();
            DataView dv = new DataView(dt);
            ddlyear.DataSource = dv;
            ddlyear.DataValueField ="YEAR";
            ddlyear.DataTextField = "YEAR";
            ddlyear.DataBind();
            ddlyear.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    protected void LotNoBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getLotNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddllotno.Items.Clear();
            DataView dv = new DataView(dt);
            ddllotno.DataSource = dv;
            ddllotno.DataValueField ="LOT_NO";
            ddllotno.DataTextField = "LOT_NO";
            ddllotno.DataBind();
            ddllotno.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void InvoiceNoBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getInvoiceNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlinvoice.Items.Clear();
            DataView dv = new DataView(dt);
            ddlinvoice.DataSource = dv;
            ddlinvoice.DataValueField = "INVOICE_NUMB";
            ddlinvoice.DataTextField = "INVOICE_NUMB";
            ddlinvoice.DataBind();
            ddlinvoice.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void LooryNoBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getLorryNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlvechilno.Items.Clear();
            DataView dv = new DataView(dt);
            ddlvechilno.DataSource = dv;
            ddlvechilno.DataValueField ="VEHICLE_NO";
            ddlvechilno.DataTextField = "VEHICLE_NO";
            ddlvechilno.DataBind();
            ddlvechilno.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void TrspNameBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getTrspName(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddltrspname.Items.Clear();
            DataView dv = new DataView(dt);
            ddltrspname.DataSource = dv;
            ddltrspname.DataValueField = "TRSP_CODE";
            ddltrspname.DataTextField = "TRSP_NAME";
            ddltrspname.DataBind();
            ddltrspname.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void PartyNameBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getPartyName(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlparty.Items.Clear();
            DataView dv = new DataView(dt);
            ddlparty.DataSource = dv;
            ddlparty.DataValueField = "PRTY_CODE";
            ddlparty.DataTextField = "PRTY_NAME";
            ddlparty.DataBind();
            ddlparty.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void GateNoBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getGateNo(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlgateno.Items.Clear();
            DataView dv = new DataView(dt);
            ddlgateno.DataSource = dv;
            ddlgateno.DataValueField ="GATE_NUMB";
            ddlgateno.DataTextField = "GATE_NUMB";
            ddlgateno.DataBind();
            ddlgateno.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btn_search(object sender, EventArgs e)
    {
            string BRANCH = string.Empty;
            string LOTNO = string.Empty;
            string YARNCODE = string.Empty;
            string VECHILNO = string.Empty;
            string TRSPNAME = string.Empty;
            string FROMDATE= string.Empty;
            string TODATE = string.Empty;
            string YEAR = string.Empty;
            string GATENO = string.Empty;
            string PRTY = string.Empty;
            string invoice = string.Empty;

            try
            {
                if (ddlparty.SelectedValue.ToString() != null && ddlparty.SelectedValue.ToString() != string.Empty && ddlparty.SelectedIndex > 0)
                {
                    PRTY = ddlparty.SelectedValue.ToString();
                }
                else
                {
                    PRTY = string.Empty;
                }
                if (ddlinvoice.SelectedValue.ToString() != null && ddlinvoice.SelectedValue.ToString() != string.Empty && ddlinvoice.SelectedIndex > 0)
                {
                    invoice = ddlinvoice.SelectedValue.ToString();
                }
                else
                {
                    invoice = string.Empty;
                }
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty && ddlbranch.SelectedIndex > 0)
            {
                BRANCH = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty && ddllotno.SelectedIndex > 0)
            {
                LOTNO = ddllotno.SelectedValue.ToString();
            }
            else
            {
                LOTNO = string.Empty;
            }
            if (ddlyarn.SelectedValue.ToString() != null && ddlyarn.SelectedValue.ToString() != string.Empty && ddlyarn.SelectedIndex > 0)
            {
                YARNCODE = ddlyarn.SelectedValue.ToString();
            }
            else
            {
                YARNCODE = string.Empty;
            }
            if (ddlvechilno.SelectedValue.ToString() != null && ddlvechilno.SelectedValue.ToString() != string.Empty && ddlvechilno.SelectedIndex > 0)
            {
                VECHILNO = ddlvechilno.SelectedValue.ToString();
            }
            else
            {
                VECHILNO = string.Empty;
            }
            if (ddltrspname.SelectedValue.ToString() != null && ddltrspname.SelectedValue.ToString() != string.Empty && ddltrspname.SelectedIndex > 0)
            {
                TRSPNAME = ddltrspname.SelectedValue.ToString();
            }
            else
            {
                TRSPNAME = string.Empty;
            }
            if (ddlgateno.SelectedValue.ToString() != null && ddlgateno.SelectedValue.ToString() != string.Empty && ddlgateno.SelectedIndex > 0)
            {
                GATENO = ddlgateno.SelectedValue.ToString();
            }
            else
            {
                GATENO = string.Empty;
            }
            if (txtFROMDATE.Text.Trim().ToString() != null && txtFROMDATE.Text.Trim().ToString() != string.Empty && txtTODATE.Text.Trim().ToString() != null && txtTODATE.Text.Trim().ToString() != string.Empty)
            {
                FROMDATE = txtFROMDATE.Text.Trim().ToString();
                TODATE = txtTODATE.Text.Trim().ToString();
            }
            else
            {
                FROMDATE = string.Empty;
                TODATE = string.Empty;
            }
            if (ddlyear.SelectedValue.ToString() != null && ddlyear.SelectedValue.ToString() != string.Empty && ddlyear.SelectedIndex > 0)
            {
                YEAR = ddlyear.SelectedValue.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetGatePass(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE, BRANCH, LOTNO, YARNCODE, VECHILNO, TRSPNAME, FROMDATE, TODATE, YEAR, GATENO, invoice, PRTY);
            if (dt != null && dt.Rows.Count > 0)
            {
                grGatePass.DataSource = dt;
                grGatePass.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();

                grGatePass.Visible = true;
            }
            else
            {
                grGatePass.DataSource = null;
                grGatePass.DataBind();
                lblTotalRecord.Text = "0";
                grGatePass.Visible = false;
                CommonFuction.ShowMessage("No data found..");
            }




        }
        catch (Exception ex)
        {
            throw ex;
        }
    }




    protected DataTable btn_search()
    {
        string BRANCH = string.Empty;
        string LOTNO = string.Empty;
        string YARNCODE = string.Empty;
        string VECHILNO = string.Empty;
        string TRSPNAME = string.Empty;
        string FROMDATE = string.Empty;
        string TODATE = string.Empty;
        string YEAR = string.Empty;
        string GATENO = string.Empty;
        string PRTY = string.Empty;
        string invoice = string.Empty;

        try
        {
            if (ddlparty.SelectedValue.ToString() != null && ddlparty.SelectedValue.ToString() != string.Empty && ddlparty.SelectedIndex > 0)
            {
                PRTY = ddlparty.SelectedValue.ToString();
            }
            else
            {
                PRTY = string.Empty;
            }
            if (ddlinvoice.SelectedValue.ToString() != null && ddlinvoice.SelectedValue.ToString() != string.Empty && ddlinvoice.SelectedIndex > 0)
            {
                invoice = ddlinvoice.SelectedValue.ToString();
            }
            else
            {
                invoice = string.Empty;
            }
            if (ddlbranch.SelectedValue.ToString() != null && ddlbranch.SelectedValue.ToString() != string.Empty && ddlbranch.SelectedIndex > 0)
            {
                BRANCH = ddlbranch.SelectedValue.ToString();
            }
            else
            {
                BRANCH = string.Empty;
            }
            if (ddllotno.SelectedValue.ToString() != null && ddllotno.SelectedValue.ToString() != string.Empty && ddllotno.SelectedIndex > 0)
            {
                LOTNO = ddllotno.SelectedValue.ToString();
            }
            else
            {
                LOTNO = string.Empty;
            }
            if (ddlyarn.SelectedValue.ToString() != null && ddlyarn.SelectedValue.ToString() != string.Empty && ddlyarn.SelectedIndex > 0)
            {
                YARNCODE = ddlyarn.SelectedValue.ToString();
            }
            else
            {
                YARNCODE = string.Empty;
            }
            if (ddlvechilno.SelectedValue.ToString() != null && ddlvechilno.SelectedValue.ToString() != string.Empty && ddlvechilno.SelectedIndex > 0)
            {
                VECHILNO = ddlvechilno.SelectedValue.ToString();
            }
            else
            {
                VECHILNO = string.Empty;
            }
            if (ddltrspname.SelectedValue.ToString() != null && ddltrspname.SelectedValue.ToString() != string.Empty && ddltrspname.SelectedIndex > 0)
            {
                TRSPNAME = ddltrspname.SelectedValue.ToString();
            }
            else
            {
                TRSPNAME = string.Empty;
            }
            if (ddlgateno.SelectedValue.ToString() != null && ddlgateno.SelectedValue.ToString() != string.Empty && ddlgateno.SelectedIndex > 0)
            {
                GATENO = ddlgateno.SelectedValue.ToString();
            }
            else
            {
                GATENO = string.Empty;
            }
            if (txtFROMDATE.Text.Trim().ToString() != null && txtFROMDATE.Text.Trim().ToString() != string.Empty && txtTODATE.Text.Trim().ToString() != null && txtTODATE.Text.Trim().ToString() != string.Empty)
            {
                FROMDATE = txtFROMDATE.Text.Trim().ToString();
                TODATE = txtTODATE.Text.Trim().ToString();
            }
            else
            {
                FROMDATE = string.Empty;
                TODATE = string.Empty;
            }
            if (ddlyear.SelectedValue.ToString() != null && ddlyear.SelectedValue.ToString() != string.Empty && ddlyear.SelectedIndex > 0)
            {
                YEAR = ddlyear.SelectedValue.ToString();
            }
            else
            {
                YEAR = string.Empty;
            }
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.GetGatePass(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE, BRANCH, LOTNO, YARNCODE, VECHILNO, TRSPNAME, FROMDATE, TODATE, YEAR, GATENO, invoice, PRTY);
            
                return dt;
            

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void YranCodeBind()
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_Gate_MST.getYarnCode(oUserLoginDetail.COMP_CODE, oUserLoginDetail.BRANCH_PRTYCODE);

            ddlyarn.Items.Clear();
            DataView dv = new DataView(dt);
            ddlyarn.DataSource = dv;
            ddlyarn.DataValueField = "YARN_CODE";
            ddlyarn.DataTextField = "YARN_CODE";
            ddlyarn.DataBind();
            ddlyarn.Items.Insert(0, new ListItem("--------ALL---------", ""));

            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }








    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
            string strFilename = "Gate_Pass_List_" + DateTime.Now.ToShortDateString() + ".xls";
            ExporttoExcel(btn_search(), strFilename, "Gate Pass List");

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
