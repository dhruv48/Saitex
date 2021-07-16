<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjustPurchaseVoucher.aspx.cs"
    Inherits="Module_FA_Pages_AdjustPurchaseVoucher" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adjust Purchase Voucher</title>
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
        .TextBoxDisplay
        {
            background-color: #aabbcc;
        }
    </style>
</head>
<body bgcolor="#afcae4">
    <form id="form1" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:Panel ID="pnlReceiptAdjustment" runat="server" BackColor="#afcae4">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <table border="1" class="tContentArial">
                        </td>
                        <tr>
                            <td align="center" valign="top" bgcolor="#ccccff">
                                <strong>Adjust Purchase Voucher Payment</strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ValidationGroup="M1" />
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
                                        <asp:TemplateField HeaderText="Select Voucher" FooterText="Total Amount :">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPurchaseVouchers" runat="server" Text='<%# Bind("VCHR_NO") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkPurchaseVouchers_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblActualAmt" runat="server" Text='<%# Bind("ACT_AMT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalActAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTDSAmt" runat="server" Text='<%# Bind("TDS_AMT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalTDSAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payable Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("REM_AMT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtTotalAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Adjusted Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdjAmount" runat="server" Text='<%# Bind("ADJ_AMT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalAdjAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pending Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPendingAmount" runat="server" Text='<%# Bind("PEND_AMT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalPendingAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" FooterText="Total Amount :">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Text="0" Width="70px" AutoPostBack="True"
                                                    MaxLength="10" OnTextChanged="txtAmount_TextChanged" ReadOnly="true" CssClass="TextBoxNo"></asp:TextBox>
                                                <asp:RangeValidator ID="RVAmount" runat="server" ValidationGroup="M1" Display="Dynamic"
                                                    ControlToValidate="txtAmount" MinimumValue="0" MaximumValue="9999999" ErrorMessage="Enter only number"
                                                    Type="Integer" SetFocusOnError="true"></asp:RangeValidator>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtPayAmt" CssClass="TextBoxNo" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="top">
                                <asp:Button ID="btnAdjustAmount" runat="server" OnClick="btnAdjustAmount_Click" Text="Adjust Purchase Amount" />
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
