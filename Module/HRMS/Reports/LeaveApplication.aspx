<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeaveApplication.aspx.cs" Inherits="Module_HRMS_Reports_LeaveApplication" %>
<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Leave Application</title>
</head>
<body>
    <form id="form1" runat="server">
      <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
              HasPrintButton="true"
                HasExportButton="true" PrintMode="pdf"
            AutoDataBind="true" />
    </form>
</body>
</html>
