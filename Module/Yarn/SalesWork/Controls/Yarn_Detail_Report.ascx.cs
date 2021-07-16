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
using errorLog;
using Obout.ComboBox;
using Obout.Interface;
using Common;
public partial class Module_Yarn_SalesWork_Controls_Yarn_Detail_Report : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;
    SaitexDM.Common.DataModel.OD_SHADE_FAMILY oOD_SHADE_FAMILY = new SaitexDM.Common.DataModel.OD_SHADE_FAMILY();
    private static string Start_Year = string.Empty;
    private static string End_Year = string.Empty;
    string branch = string.Empty;
    private static DateTime Sdate;
    private static DateTime Edate;
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        try
        {
            if (!IsPostBack)
            {
                InitialControl();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }
    private void InitialControl()
    {
        try
        {
            ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
            Sdate = oUserLoginDetail.DT_STARTDATE;
            Edate = Common.CommonFuction.GetYearEndDate(Sdate);
            //TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
            TxtToDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy"); //Common.CommonFuction.GetYearEndDate(Sdate).ToShortDateString();
            getBrachName();
            BindYear();
            ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
            //BindYarnCat();
            //BindYarnType();
            bindCategory("YARN_CAT");
            bindYarnType("YARN_TYPE");
            BindParty();
            bindYarnSHADE("YARN_SHADE");
            BindDepartment(ddlstore);
            BindDropDown(ddllocation);
            
           
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void getBrachName()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            string strCompanyCode = oUserLoginDetail.COMP_CODE.Trim().ToString();
            dt = SaitexBL.Interface.Method.EmployeeMaster.getBranchName(strCompanyCode);
            DataView Dv = new DataView(dt);
            ddlBranch.DataSource = Dv;
            ddlBranch.DataValueField = "BRANCH_CODE";
            ddlBranch.DataTextField = "BRANCH_NAME";
            ddlBranch.DataBind();
            //ddlBranch.Items.Insert(0, new ListItem("---------------All---------------", ""));
            dt.Dispose();
            dt = null;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "ss", "window.alert('" + ex.Message + "');", true);
            ErrHandler.WriteError(ex.Message);
        }
    }
    private void BindYarnCat()
    {
        try
        {
            DataTable dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnCate();
            ddlyarncat.Items.Clear();
            ddlyarncat.DataSource = dt;
            ddlyarncat.DataValueField = "YARN_CAT";
            ddlyarncat.DataTextField = "YARN_CAT";
            ddlyarncat.DataBind();
            ddlyarncat.Items.Insert(0, new ListItem("All", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void BindYarnType()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.YRN_MST.GetYarnType();
            ddlyarntype.Items.Clear();
            ddlyarntype.DataSource = dt;
            ddlyarntype.DataValueField = "YARN_TYPE";
            ddlyarntype.DataTextField = "YARN_TYPE";
            ddlyarntype.DataBind();
            ddlyarntype.Items.Insert(0, new ListItem("All", string.Empty));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public void bindCategory(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {

                ddlyarncat.Items.Clear();
                ddlyarncat.DataSource = dt;
                ddlyarncat.DataTextField = "MST_DESC";
                ddlyarncat.DataValueField = "MST_DESC";
                ddlyarncat.DataBind();
                ddlyarncat.Items.Insert(0, new ListItem("All", string.Empty));
                dt.Dispose();
                dt = null;

            }
        }
        catch
        {
            throw;
        }


    }

    public void bindYarnType(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAMEForYarnType(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                DataView dv = new DataView(dt);
                // dv.RowFilter = "MST_DESC='" + TYPE + "'";
                if (dv != null && dv.Count > 0)
                {
                    ddlyarntype.Items.Clear();
                    ddlyarntype.DataSource = dv;
                    ddlyarntype.DataTextField = "MST_CODE";
                    ddlyarntype.DataValueField = "MST_CODE";
                    ddlyarntype.DataBind();
                    ddlyarntype.Items.Insert(0, new ListItem("All", string.Empty));
                    dt.Dispose();
                    dt = null;
                }
                else
                {
                    ddlyarntype.Items.Clear();
                    ddlyarntype.Items.Insert(0, new ListItem("------NoItems------", "0"));

                }
            }
        }
        catch
        {
            throw;
        }
    }


    private void BindParty()
    {
        try
        {
            DataTable dt = null;
            dt = new DataTable();
            dt = SaitexBL.Interface.Method.TX_VENDOR_MST.SelectVendorMst();
            ddlpartycode.Items.Clear();
            ddlpartycode.DataSource = dt;
            ddlpartycode.DataValueField = "PRTY_CODE";
            ddlpartycode.DataTextField = "PRTY_NAME";
            ddlpartycode.DataBind();
            ddlpartycode.Items.Insert(0, new ListItem("All", ""));
            dt.Dispose();
            dt = null;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
   
    //private void BindShadeCode()
    //{
    //    try
    //    {
    //        DataTable dt = SaitexBL.Interface.Method.OD_SHADE_FAMILY.GetShadeCodeALL(oOD_SHADE_FAMILY);
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            ddlShadeCode.Items.Clear();
    //            ddlShadeCode.DataSource = dt;
    //            ddlShadeCode.DataTextField = "SHADE_CODE";
    //            ddlShadeCode.DataValueField = "SHADE_CODE";
    //            ddlShadeCode.DataBind();
    //            ddlShadeCode.Items.Insert(0, new ListItem("All", string.Empty));

    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        throw ex;
    //    }
    //}

  private void bindYarnSHADE(string MST_NAME)
    {
        try
        {
            DataTable dt = SaitexBL.Interface.Method.TX_MASTER_TRN.SelectByMST_NAME(MST_NAME, oUserLoginDetail.COMP_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlShadeCode.Items.Clear();
                ddlShadeCode.DataSource = dt;
                ddlShadeCode.DataTextField = "MST_CODE";
                ddlShadeCode.DataValueField = "MST_CODE";
                ddlShadeCode.DataBind();
                ddlShadeCode.Items.Insert(0, new ListItem("All", string.Empty));
                //ddlShadeCode.SelectedValue = "GREIGE";
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void imgbtnPrint_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            string chk = string.Empty;
            if (redForQuery.SelectedValue == "red")
            {
                chk = "0";
            }
            else if (redForQuery.SelectedValue == "green")
            {
                chk = "1";
            }
            else if (redForQuery.SelectedValue == "blue")
            {
                chk = "2";
            }
            else if (redForQuery.SelectedValue == "yellow")
            {
                chk = "3";
            }
            else if (redForQuery.SelectedValue == "sky")
            {
                chk = "4";
            }
            else
            {
                chk = "5";
            }

            string URL = "../Reports/YarnDetailreport.aspx?BRANCH_CODE=" + ddlBranch.SelectedValue.ToString() + "&YARN_CAT=" + ddlyarncat.SelectedValue.ToString() + "&YARN_TYPE=" + ddlyarntype.SelectedValue.ToString() + "&LOCATION=" + ddllocation.SelectedValue.ToString() + "&STORE=" + ddlstore.SelectedValue.ToString() + "&PRTY_CODE=" + ddlpartycode.SelectedValue.ToString() + "&SHADE_CODE=" + ddlShadeCode.SelectedValue.ToString() + "&BRANCH=" + ddlBranch.SelectedItem.Text + "&YARNCAT=" + ddlyarncat.SelectedItem.Text + "&YARNTYPE=" + ddlyarntype.SelectedItem.Text + "&PARTY=" + ddlpartycode.SelectedItem.Text + "&SHADCODE=" + ddlShadeCode.SelectedItem.Text + "&chk=" + chk + "&YARN_CODE=" + TxtYarnCode.Text.ToString() + "&StDate=" + TxtFromDate.Text.Trim().ToString() + "&EnDate=" + TxtToDate.Text.ToString()+"&TRN="+ddl_TRN_Type.SelectedValue;
            
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }
      
    }
    protected void imgbtnClear_Click(object sender, ImageClickEventArgs e)
    {
        try
        {
            InitialControl();
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
            throw ex;
        }
    }
    protected void TxtToDate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (TxtFromDate.Text.Trim() != string.Empty && TxtToDate.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(TxtToDate.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtToDate.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
        }
    }
    private void bindFromToDate()
    {
        string FIN_YEAR_CODE = ddlYear.SelectedValue.ToString();
        DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.FinYear(FIN_YEAR_CODE);
        if (dt != null && dt.Rows.Count > 0)
        {
            DataView dv = new DataView(dt);
            dv.RowFilter = "FIN_YEAR_CODE='" + ddlYear.SelectedValue.ToString() + "'";
            if (dv.Count > 0)
            {
                for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                {


                    TxtFromDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["START_DATE"].ToString()));
                    TxtToDate.Text = string.Format("{0:dd/MM/yyyy}", DateTime.Parse(dv[iLoop]["END_DATE"].ToString()));
                }
            }
        }
    }
    private void BindYear()
    {
        try
        {

            string branch = ddlBranch.SelectedValue.ToString();
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.bindyear(branch);
            if (dt.Rows.Count > 0)
            {
                ddlYear.Items.Clear();
                ddlYear.DataSource = dt;
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataBind();
                //ddlYear.Items.Insert(0, new ListItem("---------------All---------------", ""));
                dt.Dispose();
                dt = null;
            }
            else
            {
                string brnch = ddlBranch.SelectedItem.Text.Trim();
                CommonFuction.ShowMessage(" No financial Year associated with " + brnch + " branch ");
                getBrachName();
                ddlBranch.SelectedValue = oUserLoginDetail.CH_BRANCHCODE;
                BindYear();
                ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
                bindFromToDate();
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            bindFromToDate();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void TxtFromDate_TextChanged(object sender, EventArgs e)
    {
        try
        {

            if (TxtFromDate.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(TxtFromDate.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    TxtFromDate.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();
                }
            }
        }
        catch (Exception ex)
        {
            errorLog.ErrHandler.LogError(ex, @"Problem in " + ex.TargetSite.Name + "{0}\n Message: " + ex.Message);
            throw ex;
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

    protected void imgBtnExportExcel_Click(object sender, ImageClickEventArgs e)
    {
       
        string strFilename = string.Empty;
        string strTitle = string.Empty;
        DataTable dt = null;
        string TRN_TYPE = string.Empty;
        DateTime FROMDATE = DateTime.Now;
        DateTime TODATE = DateTime.Now;
        DateTime.TryParse(TxtFromDate.Text, out FROMDATE);
        DateTime.TryParse(TxtToDate.Text, out TODATE);
        if (ddl_TRN_Type.SelectedValue.Equals("R"))
        {
            TRN_TYPE = "'RYS21','RYS06','RYS04','RYS03','RYS02','RYS01','RYJ01'";
        }
        else if (ddl_TRN_Type.SelectedValue.Equals("P"))
        {
            TRN_TYPE = "'RYS31','RYS32','RYS33','RYS34','RYS35','RYS36'";
        }
        else if (ddl_TRN_Type.SelectedValue.Equals("O"))
        {
            TRN_TYPE = "'OPB01','OJB01'";
        }
        if (redForQuery.SelectedValue == "red")
        {
            strFilename = "LOT_WISE_YARN_STOCK_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "LOT WISE YARN STOCK DETAILS (" + ddl_TRN_Type.SelectedItem.ToString() + ")";
           
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetLotWiseYarnDetail(ddlBranch.SelectedValue, ddlyarncat.SelectedValue, ddlyarntype.SelectedValue, ddlpartycode.SelectedValue, ddlShadeCode.SelectedValue, TxtYarnCode.Text, FROMDATE, TODATE , ddllocation.SelectedValue, ddlstore.SelectedValue, TRN_TYPE);

        }
        else if (redForQuery.SelectedValue == "green" || redForQuery.SelectedValue == "blue")
        {
            strFilename = "CARTON_WISE_YARN_STOCK_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "CARTON WISE YARN STOCK DETAILS (" + ddl_TRN_Type.SelectedItem.ToString() + ")";
           
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetCartonWiseYarnDetail(ddlBranch.SelectedValue, ddlyarncat.SelectedValue, ddlyarntype.SelectedValue, ddlpartycode.SelectedValue, ddlShadeCode.SelectedValue, TxtYarnCode.Text, FROMDATE, TODATE, ddllocation.SelectedValue, ddlstore.SelectedValue, TRN_TYPE);

        }        
        else if (redForQuery.SelectedValue == "yellow")
        {
            strFilename = "PARTY_WISE_YARN_STOCK_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "PARTY WISE YARN STOCK DETAILS (" + ddl_TRN_Type.SelectedItem.ToString() + ")";
          
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetPartyWiseYarnDetail(ddlBranch.SelectedValue, ddlyarncat.SelectedValue, ddlyarntype.SelectedValue, ddlpartycode.SelectedValue, ddlShadeCode.SelectedValue, TxtYarnCode.Text, FROMDATE, TODATE, ddllocation.SelectedValue, ddlstore.SelectedValue, TRN_TYPE);

        }
        else if (redForQuery.SelectedValue == "sky")
        {
            strFilename = "CHALLAN_WISE_YARN_STOCK_DETAILS_" + DateTime.Now.ToString() + ".xls";
            strTitle = "CHALLAN WISE YARN STOCK DETAILS (" + ddl_TRN_Type.SelectedItem.ToString() + ")";
          
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetYarnDetail(ddlBranch.SelectedValue, ddlyarncat.SelectedValue, ddlyarntype.SelectedValue, ddlpartycode.SelectedValue, ddlShadeCode.SelectedValue, TxtYarnCode.Text, FROMDATE, TODATE, ddllocation.SelectedValue, ddlstore.SelectedValue, TRN_TYPE);

        }

        ExporttoExcel(dt, strFilename, strTitle);

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
