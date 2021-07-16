<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISEReport.aspx.cs" Inherits="Module_Production_Report_MACHINE_ISSUE_CONSUMPTION_DETAILS_DATE_WISEReport" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    
    </div>
    <CR:CrystalReportViewer ID="COPSreport" runat="server" PrintMode="pdf" SeparatePages="True" HasPrintButton="true" HasExportButton="true"
            DisplayToolbar="true" HasRefreshButton="true" 
        AutoDataBind="true" />
    </form>
</body>
</html>