<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Machine_Group_Report.aspx.cs" Inherits="Module_Machine_Reports_Machine_Group_Report" %>

<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
        AutoDataBind="true"   HasPrintButton="true" PrintMode="pdf" HasRefreshButton="true" EnableDatabaseLogonPrompt="False" />
    </form>
</body>
</html>
