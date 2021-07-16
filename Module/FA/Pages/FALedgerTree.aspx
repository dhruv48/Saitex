<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FALedgerTree.aspx.cs" Inherits="Module_FA_Pages_FALedgerTree" %>

<%@ Register src="../Controls/LedgerTree.ascx" tagname="LedgerTree" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ledger Master</title>
    <link href="~/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
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
                                    <span style="font-size: 13pt" class="titleheading"><strong>Ledger Master</strong>
                                    </span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                
                                    <uc1:LedgerTree ID="LedgerTree1" runat="server" />
                                
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
