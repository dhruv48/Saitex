<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PackingSummarydetail_Report.aspx.cs" Inherits="Module_Production_Report_PackingSummarydetail_Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <CR:CrystalReportViewer ID="CrystalReportViewer1" HasExportButton="True" PrintMode="Pdf" SeparatePages="True" HasPrintButton="True" DisplayToolbar="True" HasRefreshButton="True" runat="server" AutoDataBind="true" />
    </div>
    </form>
</body>
</html>
