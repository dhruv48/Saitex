<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SALE_MACHINE_PRINT.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Reports_SALE_MACHINE_PRINT" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sale Machine </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
            AutoDataBind="true"  HasGotoPageButton="True" HasPrintButton="True" 
            PrintMode="Pdf" HasRefreshButton="True"   HasSearchButton="True" 
            HasToggleGroupTreeButton="True" HasPageNavigationButtons="True"  />
    </div>
    </form>
</body>
</html>
