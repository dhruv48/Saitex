<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DynamicReport.aspx.cs" Inherits="Module_Admin_Reports_DynamicReport" %>

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
        <asp:TextBox ID="TextBox1" runat="server" Height="125px" TextMode="MultiLine" Width="600px"></asp:TextBox>
        <br />
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <br />
        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            PrintMode="pdf" SeparatePages="True" HasPrintButton="true" HasExportButton="true"
            DisplayToolbar="true" HasRefreshButton="true"  />
    </div>
    </form>
</body>
</html>
