<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ISSUE_AGAINST_PA_REPT.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_ISSUE_AGAINST_PA_REPT" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ISSU AGAINST PA REPORT</title>
</head>
<body>
    <form id="form1" runat="server">
   
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        PrintMode="pdf" SeparatePages="True" HasPrintButton="true" HasExportButton="true"
        DisplayToolbar="true" HasRefreshButton="true"  />
   
    </form>
</body>
</html>
