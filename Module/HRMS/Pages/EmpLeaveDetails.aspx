<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmpLeaveDetails.aspx.cs" Inherits="Module_HRMS_Pages_EmpLeaveDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="../Controls/EmpLeaveDetails.ascx" TagName="EmpLeaveDetails" TagPrefix="uc1" %>
<%@ Register src="../Controls/EarnLeaveCalculation.ascx" tagname="EarnLeaveCalculation" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Employee Leave Detail</title>
      <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/cssdesign.css" />  
</head>
<body>
    <form id="form1" runat="server">
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager> 
    <table cellpadding="0" cellspacing="0" border="0" width="100%" align="center">
        <tr>
            <td align="left" width="100%" valign="top">
                <uc1:EmpLeaveDetails ID="EmpLeaveDetails1" runat="server" />
            </td>
        </tr>        
    </table>
    </form>
</body>
</html>
