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
using CrystalDecisions.CrystalReports;
using CrystalDecisions.ReportSource;
using CrystalDecisions.CrystalReports.Engine;
using System.Data.OracleClient;

public partial class Module_Machine_Reports_MachineUtility : System.Web.UI.Page
{
    
    string MachineGroup, MachineSegment, MachineType, MachineSec;
    protected void Page_Load(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        if (Request.QueryString["Segment"] != null)
        {
            MachineSegment = Request.QueryString["Segment"].ToString();
            if (Request.QueryString["Segment"] != null && Request.QueryString["Section"] != null)
            {
                MachineSec = Request.QueryString["Section"].ToString();
                if (Request.QueryString["Segment"] != null && Request.QueryString["Section"] != null && Request.QueryString["Type"] != null)
                {
                    MachineType = Request.QueryString["Type"].ToString();
                    if (Request.QueryString["Segment"] != null && Request.QueryString["Section"] != null && Request.QueryString["Type"] != null && Request.QueryString["Group"] != null)
                    {
                        MachineGroup = Request.QueryString["Group"].ToString();
                        dt = GetGroupWiseData(MachineSegment, MachineSec, MachineType, MachineGroup);

                    }
                    else
                    {
                        dt = GetTypeWiseData(MachineSegment, MachineSec, MachineType);
                    }
                }
                else
                {
                    dt = GetSectionpWiseData(MachineSegment, MachineSec);
                }
            }
            else
            {
                dt = GetSegmentWiseData(MachineSegment);
            }

        }
        else
        {
            dt = GetDetailReport();
        }
        getReport(dt);

    }
    private void getReport(DataTable dt)
    {
        ReportDocument rdoc = new ReportDocument();
        rdoc.Load((Server.MapPath(@"MachineUtility.rpt")));
        rdoc.SetDataSource(dt);
        CrystalReportViewer1.ReportSource = rdoc;
    }
    private DataTable GetDetailReport()
    {
        try
        {

            DataTable dtGrp = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetUtilityDetailReport();
            if (dtGrp.Columns["COMP_NAME"] == null)
                dtGrp.Columns.Add("COMP_NAME", typeof(string));
            if (dtGrp.Columns["BRANCH_NAME"] == null)
                dtGrp.Columns.Add("BRANCH_NAME", typeof(string));
            if (dtGrp.Columns["USER_NAME"] == null)
                dtGrp.Columns.Add("USER_NAME", typeof(string));
            if (dtGrp.Columns["COMP_ADD"] == null)
                dtGrp.Columns.Add("COMP_ADD", typeof(string));
            if (dtGrp.Columns["DEVELOPER_COMP"] == null)
                dtGrp.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dtGrp.Columns["DEVELOPER_WEB"] == null)
                dtGrp.Columns.Add("DEVELOPER_WEB", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dtGrp.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dtGrp.AcceptChanges();
            }
            return dtGrp;
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
    private DataTable GetSegmentWiseData(string MachineSegment)
    {
        try
        {

            DataTable dtGrp = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetUtilitySegmentWiseReport(MachineSegment);
            if (dtGrp.Columns["COMP_NAME"] == null)
                dtGrp.Columns.Add("COMP_NAME", typeof(string));
            if (dtGrp.Columns["BRANCH_NAME"] == null)
                dtGrp.Columns.Add("BRANCH_NAME", typeof(string));
            if (dtGrp.Columns["USER_NAME"] == null)
                dtGrp.Columns.Add("USER_NAME", typeof(string));
            if (dtGrp.Columns["COMP_ADD"] == null)
                dtGrp.Columns.Add("COMP_ADD", typeof(string));
            if (dtGrp.Columns["DEVELOPER_COMP"] == null)
                dtGrp.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dtGrp.Columns["DEVELOPER_WEB"] == null)
                dtGrp.Columns.Add("DEVELOPER_WEB", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dtGrp.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dtGrp.AcceptChanges();
            }
            return dtGrp;
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
    private DataTable GetSectionpWiseData(string MachineSegment, string MachineSec)
    {
        try
        {

            DataTable dtGrp = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetUtilitySectionWiseReport(MachineSegment, MachineSec);
            if (dtGrp.Columns["COMP_NAME"] == null)
                dtGrp.Columns.Add("COMP_NAME", typeof(string));
            if (dtGrp.Columns["BRANCH_NAME"] == null)
                dtGrp.Columns.Add("BRANCH_NAME", typeof(string));
            if (dtGrp.Columns["USER_NAME"] == null)
                dtGrp.Columns.Add("USER_NAME", typeof(string));
            if (dtGrp.Columns["COMP_ADD"] == null)
                dtGrp.Columns.Add("COMP_ADD", typeof(string));
            if (dtGrp.Columns["DEVELOPER_COMP"] == null)
                dtGrp.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dtGrp.Columns["DEVELOPER_WEB"] == null)
                dtGrp.Columns.Add("DEVELOPER_WEB", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dtGrp.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dtGrp.AcceptChanges();
            }
            return dtGrp;
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
    private DataTable GetTypeWiseData(string MachineSegment, string MachineSec, string MachineType)
    {
        try
        {

            DataTable dtGrp = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetUtilityTypeWiseReport(MachineSegment, MachineSec, MachineType);
            if (dtGrp.Columns["COMP_NAME"] == null)
                dtGrp.Columns.Add("COMP_NAME", typeof(string));
            if (dtGrp.Columns["BRANCH_NAME"] == null)
                dtGrp.Columns.Add("BRANCH_NAME", typeof(string));
            if (dtGrp.Columns["USER_NAME"] == null)
                dtGrp.Columns.Add("USER_NAME", typeof(string));
            if (dtGrp.Columns["COMP_ADD"] == null)
                dtGrp.Columns.Add("COMP_ADD", typeof(string));
            if (dtGrp.Columns["DEVELOPER_COMP"] == null)
                dtGrp.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dtGrp.Columns["DEVELOPER_WEB"] == null)
                dtGrp.Columns.Add("DEVELOPER_WEB", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dtGrp.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dtGrp.AcceptChanges();
            }
            return dtGrp;
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
    private DataTable GetGroupWiseData(string MachineSegment, string MachineSec, string MachineType, string MachineGroup)
    {
        try
        {

            DataTable dtGrp = SaitexBL.Interface.Method.MC_MACHINE_MASTER.GetUtilityGroupWiseReport(MachineSegment, MachineSec, MachineType, MachineGroup);
            if (dtGrp.Columns["COMP_NAME"] == null)
                dtGrp.Columns.Add("COMP_NAME", typeof(string));
            if (dtGrp.Columns["BRANCH_NAME"] == null)
                dtGrp.Columns.Add("BRANCH_NAME", typeof(string));
            if (dtGrp.Columns["USER_NAME"] == null)
                dtGrp.Columns.Add("USER_NAME", typeof(string));
            if (dtGrp.Columns["COMP_ADD"] == null)
                dtGrp.Columns.Add("COMP_ADD", typeof(string));
            if (dtGrp.Columns["DEVELOPER_COMP"] == null)
                dtGrp.Columns.Add("DEVELOPER_COMP", typeof(string));
            if (dtGrp.Columns["DEVELOPER_WEB"] == null)
                dtGrp.Columns.Add("DEVELOPER_WEB", typeof(string));
            SaitexDM.Common.DataModel.UserLoginDetail oUserLoginDetail = (SaitexDM.Common.DataModel.UserLoginDetail)Session["LoginDetail"];
            foreach (DataRow dr in dtGrp.Rows)
            {
                dr["COMP_NAME"] = oUserLoginDetail.VC_COMPANYNAME;
                dr["BRANCH_NAME"] = oUserLoginDetail.VC_BRANCHNAME;
                dr["USER_NAME"] = oUserLoginDetail.Username;
                dr["COMP_ADD"] = oUserLoginDetail.COMP_ADD;
                dr["DEVELOPER_COMP"] = oUserLoginDetail.DEVELOPER_COMP;
                dr["DEVELOPER_WEB"] = oUserLoginDetail.DEVELOPER_WEB;
                dtGrp.AcceptChanges();
            }
            return dtGrp;
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
