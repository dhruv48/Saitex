<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Machine_Proc_Master_Report.aspx.cs"
    Inherits="Module_Machine_Reports_Machine_Proc_Master_Report" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="width:100%;">
        <table width="100%">
        
            <tr>
            <td align ="right" width="55%">ProcessCode</td>
                <td align="left" valign="top" width="20%">
                    <asp:DropDownList ID="ddProducttype" runat="server" Width="200px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddProducttype_SelectedIndexChanged" CssClass="SmallFont" color="blue">
                    </asp:DropDownList>
                </td>
                <td Width="100%" align="left">
                    <asp:Button ID="btnselect" runat="server" Width="20%" Text="find" CssClass="SmallFont"
                        OnClick="btnselect_Click"  Visible ="false"/>
                </td>
            </tr>
           
        </table>
        
       
                    <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true"
                        HasPrintButton="true" PrintMode="pdf" HasRefreshButton="true" EnableDatabaseLogonPrompt="False" />
           
    </div>
    </form>
</body>
</html>
