﻿using System;
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
public partial class Module_Yarn_SalesWork_Controls_YrnIssue : System.Web.UI.UserControl
{
    SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail;

    string yr = string.Empty;    
    string catcode = string.Empty;
   
    private static DateTime Sdate;
    private static DateTime Edate;

    string sdate1 = string.Empty;
    string edate1 = string.Empty;
       
    protected void Page_Load(object sender, EventArgs e)
    {
        oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
        
        if (!IsPostBack)
        {            
            Maxtrnno();
               
            Bind_Yarn_ddlitem();
            Bind_Year_ddlYear();
            Initial_Control();
            bindgridyrnissue();      
        } 
    }
    private void Initial_Control()
    {
        try
        {

          Sdate = oUserLoginDetail.DT_STARTDATE;
          if (DateTime.IsLeapYear(Sdate.AddDays(364).Year))
          {
              Edate = Sdate.AddDays(365);
          }
          else
          {
              Edate = Sdate.AddDays(364);
          }                  
                      
           TxtIndentDate.SelectedDate = Sdate.Date;
           TxtIndentDate1.SelectedDate = Edate.Date;
            
           ddlYear.SelectedIndex = ddlYear.Items.IndexOf(ddlYear.Items.FindByText(oUserLoginDetail.DT_STARTDATE.Year.ToString()));
           
        }
        catch
        {
            throw;
        }
    }
    private void bindFromToDate()
    {
        try
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
                        TxtIndentDate.SelectedDate = DateTime.Parse(dv[iLoop]["START_DATE"].ToString()).Date; 
                        TxtIndentDate1.SelectedDate = DateTime.Parse(dv[iLoop]["END_DATE"].ToString()).Date;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void bindgridyrnissue()
    {
        try
        {
            string trn;
            string item;
            int from;
            int to;
            DateTime StDate;
            DateTime EnDate;

            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                trn = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                trn = string.Empty;
            }
            if (ddlitem.SelectedValue.ToString() != null && ddlitem.SelectedValue.ToString() != string.Empty)
            {
                item = ddlitem.SelectedValue.ToString();
            }
            else
            {
                item = string.Empty;
            }

            if (Txtfromno.Text.ToString() != null && Txtfromno.Text.ToString() != string.Empty)
            {
                from = int.Parse(Txtfromno.Text.ToString());
            }
            else
            {
                from = 1;
            }

            if (txttono.Text.ToString() != null && txttono.Text.ToString() != string.Empty)
            {
                to = int.Parse(txttono.Text.ToString());
            }
            else
            {
                to = 5;
            }
            // ===========================================//          
            
            if (TxtIndentDate.SelectedDate.ToString() != string.Empty && TxtIndentDate1.SelectedDate.ToString().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtIndentDate.SelectedDate.ToString());
                EnDate = DateTime.Parse(TxtIndentDate1.SelectedDate.ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }
           
            //===========================================//

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_Issue(trn, item, from, to, StDate, EnDate);
            
            //DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.YrnIssue(trn, item, from, to);
            
            if (dt != null && dt.Rows.Count > 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            }


            else
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
                lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
                CommonFuction.ShowMessage("Data not Found by selected Item .");
           
            }
             }
        catch
        {
            throw;
        }
    }
    private void bindddltemtype()
    {
        try
        {

            ddlitem.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.GetYarnMaster();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlitem.DataTextField = "YARN_DESC";
                ddlitem.DataValueField = "YARN_CODE";
                ddlitem.DataSource = dt;
                ddlitem.DataBind();

            }
            ddlitem.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }

   //==========================================================
   //===== FILL  DROP DOWN LIST                             ===
   //=====================  SCRIPT START                    ===

    private void Bind_Yarn_ddlitem()
    {
        try
        {

            ddlitem.Items.Clear();
            DataTable dt = SaitexBL.Interface.Method.YRN_MST.Get_YarnMasterCodeDesc();
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlitem.DataTextField = "YARN_DESC";
                ddlitem.DataValueField = "YARN_CODE";
                ddlitem.DataSource = dt;
                ddlitem.DataBind();

            }
            ddlitem.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }


    private void Bind_Year_ddlYear()
    {
        try
        {

            ddlYear.Items.Clear();
            string BRANCH_CODE = oUserLoginDetail.CH_BRANCHCODE;
            DataTable dt = SaitexBL.Interface.Method.CM_FIN_YEAR_MST.Get_YearCodeYear(BRANCH_CODE);
            if (dt != null && dt.Rows.Count > 0)
            {
                ddlYear.DataTextField = "YEAR";
                ddlYear.DataValueField = "FIN_YEAR_CODE";
                ddlYear.DataSource = dt;
                ddlYear.DataBind();

            }
            ddlYear.Items.Insert(0, new ListItem("SELECT", ""));
        }
        catch
        {
            throw;
        }
    }
  //=====================  END  START                        ====
  //=============================================================
  //=============================================================  

    protected void Txtfromno_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindgridyrnissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }
    protected void txttono_TextChanged(object sender, EventArgs e)
    {
        try
        {
            bindgridyrnissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }


    }
    protected void ddlitem_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            bindgridyrnissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }
    }
    protected void DDLTrnType_SelectedIndexChanged1(object sender, EventArgs e)
    {
        try
        {
            bindgridyrnissue();
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Select.\r\nSee error log for detail."));
        }

    }
    private void Maxtrnno()
    {
        try
        {
            int I = 1;
           Txtfromno.Text  = I.ToString();
            string x = "";
            int y = 0;
            string trn;
            DataTable dt;
            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                trn = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                trn = string.Empty;
            }
            dt = SaitexBL.Interface.Method.YRN_IR_MST.GetMaxtTRN(trn);
            if (trn == "I")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            x = dv[iLoop]["ISS_TRN_NUMB"].ToString();
                            y = int.Parse(x);

                            txttono.Text = y.ToString();
                        }
                    }
                }

            }

            if (trn == "R")
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataView dv = new DataView(dt);
                    if (dv.Count > 0)
                    {
                        for (int iLoop = 0; iLoop < dv.Count; iLoop++)
                        {
                            x = dv[iLoop]["TRN_NUMB"].ToString();
                            y = int.Parse(x);
                            txttono.Text = y.ToString();
                        }
                    }
                }
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
        {

            string trn = string.Empty;
            string item = string.Empty;
            string fromto = string.Empty;
            string toto = string.Empty;

            DataTable myDataTable = new DataTable();
            DataColumn myDataColumn;

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "trn";
            myDataTable.Columns.Add(myDataColumn);



            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "item";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "fromto";
            myDataTable.Columns.Add(myDataColumn);

            myDataColumn = new DataColumn();
            myDataColumn.DataType = System.Type.GetType("System.String");
            myDataColumn.ColumnName = "toto";
            myDataTable.Columns.Add(myDataColumn);

            DataRow row;

            row = myDataTable.NewRow();
            row["trn"] = DDLTrnType.SelectedValue.ToString();
            row["item"] = ddlitem.SelectedValue.ToString();
            row["fromto"] = Txtfromno.Text;
            row["toto"] = txttono.Text;

            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                row["trn"] = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                row["trn"] = string.Empty;
            }


            if (ddlitem.SelectedValue.ToString() != null && ddlitem.SelectedValue.ToString() != string.Empty)
            {
                row["item"] = ddlitem.SelectedValue.ToString();
            }
            else
            {
                row["item"] = string.Empty;
            }

            if (Txtfromno.Text.ToString() != null && Txtfromno.Text.ToString() != string.Empty)
            {
                row["fromto"] = int.Parse(Txtfromno.Text.ToString());

            }
            else
            {
                row["fromto"] = 1;


            }

            if (txttono.Text.ToString() != null && txttono.Text.ToString() != string.Empty)
            {
                row["toto"] = int.Parse(txttono.Text.ToString());

            }
            else
            {
                row["toto"] = 5;
            }

            myDataTable.Rows.Add(row);
            Session["Proceereport"] = myDataTable;
           // Response.Redirect("~/Module/Yarn/SalesWork/Reports/yrnissuereport.aspx", false);

            string URL = "../Reports/yrnissuereport.aspx";
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "PrintWindow", "window.open('" + URL + "','_blank','status=1,toolbar=0,menubar=0,location=20,scrollbars=1,resizable=1,width=850,height=600');", true);
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"\r\nProblem in getting print.\r\nSee error log for detail."));
        }         
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        bindgridyrnissue();

        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();
        
    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void txtDate1_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty)
            {
                DateTime StartDate = DateTime.Parse(txtDate1.Text.Trim().ToString());

                if (StartDate >= Sdate && StartDate <= Edate)
                {

                }
                else
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate1.Text = oUserLoginDetail.DT_STARTDATE.ToShortDateString();

                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in Start Date.\r\nSee error log for detail."));
        }
    }
    protected void txtDate2_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtDate1.Text.Trim() != string.Empty && txtDate2.Text.Trim().ToString() != string.Empty)
            {
                DateTime EndDate = DateTime.Parse(txtDate2.Text.Trim().ToString());
                if (EndDate > Edate || EndDate < Sdate)
                {
                    Common.CommonFuction.ShowMessage("Please Select Date within Financial Year");
                    txtDate2.Text = Edate.ToShortDateString().ToString();
                }
            }
        }
        catch (Exception ex)
        {
            CommonFuction.ShowMessage(ErrHandler.LogError(ex, @"Problem in End Date.\r\nSee error log for detail."));
        }
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {   
        bindFromToDate();
    }

    protected void btnGetReport_Click(object sender, EventArgs e)
    {
        bindgridyrnissue();
    }


    protected void imgbtnExport_Click(object sender, ImageClickEventArgs e)
    {
        string strFilename = "  Yarn_Issue_Query_" + DateTime.Now.ToString() + ".xls";
        Common.CommonFuction.ExporttoExcel(bindgridyrnissue1(), strFilename, "  Yarn Issue Query", oUserLoginDetail.VC_COMPANYNAME);

    }


    private DataTable bindgridyrnissue1()
    {
        try
        {
            string trn;
            string item;
            int from;
            int to;
            DateTime StDate;
            DateTime EnDate;

            if (DDLTrnType.SelectedValue.ToString() != null && DDLTrnType.SelectedValue.ToString() != string.Empty)
            {
                trn = DDLTrnType.SelectedValue.ToString();
            }
            else
            {
                trn = string.Empty;
            }
            if (ddlitem.SelectedValue.ToString() != null && ddlitem.SelectedValue.ToString() != string.Empty)
            {
                item = ddlitem.SelectedValue.ToString();
            }
            else
            {
                item = string.Empty;
            }

            if (Txtfromno.Text.ToString() != null && Txtfromno.Text.ToString() != string.Empty)
            {
                from = int.Parse(Txtfromno.Text.ToString());
            }
            else
            {
                from = 1;
            }

            if (txttono.Text.ToString() != null && txttono.Text.ToString() != string.Empty)
            {
                to = int.Parse(txttono.Text.ToString());
            }
            else
            {
                to = 5;
            }
            // ===========================================//          

            if (TxtIndentDate.SelectedDate.ToString() != string.Empty && TxtIndentDate1.SelectedDate.ToString().ToString() != string.Empty)
            {
                StDate = DateTime.Parse(TxtIndentDate.SelectedDate.ToString());
                EnDate = DateTime.Parse(TxtIndentDate1.SelectedDate.ToString());
            }
            else
            {
                StDate = Sdate;
                EnDate = Edate;
            }

            //===========================================//

            DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.Yrn_IssueExcel(trn, item, from, to, StDate, EnDate);


            return dt;
            //DataTable dt = SaitexBL.Interface.Method.YRN_IR_MST.YrnIssue(trn, item, from, to);

            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //}


            //else
            //{
            //    GridView1.DataSource = dt;
            //    GridView1.DataBind();
            //    lblTotalRecord.Text = dt.Rows.Count.ToString().Trim();
            //    CommonFuction.ShowMessage("Data not Found by selected Item .");

            //}
        }
        catch
        {
            throw;
        }
    }
    
}
