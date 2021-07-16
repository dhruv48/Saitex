<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Dyeing_Pending_Print_Report.aspx.cs" Inherits="Module_Production_Report_Dyeing_Pending_Print_Report" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
     <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        PrintMode="pdf" SeparatePages="True" HasPrintButton="true" HasExportButton="true"
        DisplayToolbar="true" HasRefreshButton="true"  />
    </div>
    </form>
</body>
</html>
