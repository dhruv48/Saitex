<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SalesReport.aspx.cs" ClientTarget="uplevel" Inherits="Module_OrderDevelopment_CustomerRequest_Reports_SalesReport" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Sale Contract Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true"  HasGotoPageButton="True" HasPrintButton="True" 
            PrintMode="ActiveX" HasRefreshButton="True"   HasSearchButton="True" 
            HasToggleGroupTreeButton="True" HasPageNavigationButtons="True"  />
    </div>
   <%-- <div> <input type="button" id="btnPrint" value="Print" onclick="Print()" />  </div>--%>
    </form>
</body>
</html>
