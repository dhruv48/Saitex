<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Master_Mst_Rpt.aspx.cs" Inherits="Inventory_Master_Mst_Rpt" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Master report</title>
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body>
 <form id="form1" runat="server">
     
                         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
            PrintMode="pdf"  HasPrintButton="true" HasExportButton="true"
            DisplayToolbar="true"  />
                      
    </form> 
 
</body>
</html>