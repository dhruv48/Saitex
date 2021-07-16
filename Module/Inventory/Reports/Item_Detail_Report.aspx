<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Item_Detail_Report.aspx.cs"
    Inherits="Module_Inventory_Reports_Item_Detail_Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    </div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" PrintMode="pdf"
        SeparatePages="True" HasPrintButton="true" HasExportButton="true" DisplayToolbar="true"
        HasRefreshButton="true"  AutoDataBind="true" />
    </form>
     <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</body>
</html>
