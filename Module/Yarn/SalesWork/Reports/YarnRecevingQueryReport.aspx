<%@ Page Language="C#" AutoEventWireup="true" CodeFile="YarnRecevingQueryReport.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_YarnRecevingQueryReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Yarn Receving Report</title>
</head>
<body>
    <form id="form1" runat="server">
   
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
        PrintMode="pdf" SeparatePages="True" HasPrintButton="true" HasExportButton="true"
        DisplayToolbar="true" HasRefreshButton="true"  />
   
    </form>
</body>
</html>
