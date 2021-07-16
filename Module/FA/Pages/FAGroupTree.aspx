<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FAGroupTree.aspx.cs" Inherits="Module_FA_Pages_FAGroupTree" %>
<%@ Register src="../Controls/GroupTree.ascx" tagname="GroupTree" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<link href="~/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
<head runat="server">
    <title>Group Master</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" BackColor="#afcae4" runat="server">
            <table align="left" class="tContentArial">
                <tr>
                    <td align="center" class="td">
                        <table align="left" class="tContentArial">
                            <tr>
                                <td align="center" class="TableHeader">
                                    <span style="font-size: 13pt" class="titleheading"><strong>Group Master</strong>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                
                                    <uc1:GroupTree ID="GroupTree1" runat="server" />
                                
                                </td>
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
