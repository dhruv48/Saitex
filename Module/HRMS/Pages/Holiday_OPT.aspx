<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Holiday_OPT.aspx.cs" Inherits="Module_HRMS_Pages_Holiday_OPT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Holiday Master Report</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
</head>
<body bgcolor="#FFFFFF" topmargin="0" leftmargin="0" width="100%">
    <form id="form1" runat="server">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <div>
            <asp:Panel ID="Panel1" runat="server" BackColor="#336799" Height="600px" Width="600px">
                 <table width="600" align="left" class="tContentArial">
                    <tr>
                        <td align="center" width="600">
                            <table width="600" align="left" class="tContentArial">
                                <tr>
                                    <td align="center" colspan="6" class="TableHeader">
                                        <span class="titleheading">Holiday Master Report</span>
                                        
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        Year</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtYear" runat="server" Width="100px" CssClass="gCtrTxt"></asp:TextBox>
                                        
                                    </td>
                                    
                                    <td align="right">
                                        Date</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDate" runat="server" Width="100px" CssClass="gCtrTxt"></asp:TextBox>
                                        
                                    </td>
                                    
                                </tr>
                                
                                <tr>
                                    <td align="center" colspan="6">
                                        <br /><br />
                                        <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click1" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
