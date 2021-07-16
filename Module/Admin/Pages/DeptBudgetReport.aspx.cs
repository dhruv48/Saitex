﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OracleClient;

public partial class Module_Admin_Pages_DeptBudgetReport : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {        

            DataTable dt = GetData();
            getReport(dt);
    }
    private void getReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load((Server.MapPath(@"\Saitex\Module\Admin\Reports\DeptBudget.rpt")));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;

    }
    private DataTable GetData()
    {
        try
        {

            SaitexDM.Common.DataModel.HR_DEPT_BUDGET oHR_DEPT_BUDGET = new SaitexDM.Common.DataModel.HR_DEPT_BUDGET();
            DataTable dt = SaitexBL.Interface.Method.HR_DEPT_BUDGET.GetReport();
            if (dt.Columns["COMP_NAME"] == null)
                dt.Columns.Add("COMP_NAME", typeof(string));

            if (dt.Columns["BRANCH_NAME"] == null)
                dt.Columns.Add("BRANCH_NAME", typeof(string));

            if (dt.Columns["USER_NAME"] == null)
                dt.Columns.Add("USER_NAME", typeof(string));
            if (dt.Columns["COMP_ADD"] == null)
                dt.Columns.Add("COMP_ADD", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dt.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dt.AcceptChanges();
            }

            return dt;
        }

        catch (OracleException ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }

        catch (Exception ex)
        {
            errorLog.ErrHandler.WriteError(ex.Message);
            return null;
        }
    }
}
