<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjustAdviceWithBill.aspx.cs"
    Inherits="Module_FA_Pages_AdjustAdviceWithBill" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adjust Bill With Advices</title>
    <link href="~/StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />

    <script language="javascript" type="text/javascript">
    function GetRowValue(val,TextBoxId)
    {   
        window.opener.document.getElementById(TextBoxId).value=val;   
        window.opener.document.forms[0].submit();  
        window.close();
    }
    </script>

    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
        .header
        {
            margin-left: 2px;
        }
        .c1
        {
            width: 60px;
        }
        .c2
        {
            margin-left: 4px;
            width: 100px;
        }
        .c3
        {
            margin-left: 4px;
            width: 230px;
        }
        .c4
        {
            margin-left: 4px;
            width: 150px;
        }
    </style>
    <style type="text/css">
        .TextBoxDisplay
        {
            background-color: #aabbcc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:Panel ID="pnlReceiptAdjustment" runat="server" BackColor="#afcae4">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="1" class="tContentArial">
                        <tr>
                            <td align="center" valign="top" bgcolor="#ccccff">
                                <strong>Adjust Advice In Purchase Bill</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Label ID="lblLedgerPartyCode" runat="server" Font-Bold="True"></asp:Label>
                                <asp:Label ID="lblLedgerPartyName" runat="server" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Label ID="lblLedgerPartyCodeError" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:GridView ID="grdPaymentAdjustment" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select Advice No">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPurchaseVouchers" runat="server" Text='<%# Bind("ADV_NO") %>'
                                                    AutoPostBack="True" OnCheckedChanged="chkPurchaseVouchers_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advice Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdviceDate" runat="server" Text='<%# Bind("ADV_DATE", "{0:M-dd-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjusted Voucher No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVchrNo" runat="server" Text='<%# Bind("VCHR_NO") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Voucher Date" FooterText="Total Amount :">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVchrDT" runat="server" Text='<%# Bind("TRN_DATE", "{0:M-dd-yyyy}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advice Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("ADV_AMT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Button ID="btnAdjustAmount" runat="server" OnClick="btnAdjustAmount_Click" Text="Adjust Advice Amount" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>
    </div>
    </form>
</body>
</html>
