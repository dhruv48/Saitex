<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdjustAdvanceAdvice.aspx.cs"
    Inherits="Module_FA_Pages_AdjustAdvanceAdvice" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Adjust Advanced Advice</title>
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
<body bgcolor="#afcae4">
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
                                <strong>Adjust Advanced Advice</strong>
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
                                        <asp:TemplateField HeaderText="Select Advice">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPurchaseVouchers" runat="server" Text='<%# Bind("ADV_NO") %>'
                                                    AutoPostBack="true" OnCheckedChanged="chkPurchaseVouchers_CheckedChanged" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Advice Date" FooterText="Total Amount :">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdviceDate" runat="server" Text='<%# Bind("ADV_DATE", "{0:M-dd-yyyy}") %>'></asp:Label>
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
                                        <asp:TemplateField HeaderText="Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" Text="0" Width="70px" AutoPostBack="True"
                                                    ReadOnly="true" CssClass="TextBoxNo"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="txtPayAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trTDS" runat="server">
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:CheckBox ID="chkTDS" runat="server" Text="Deduct TDS" Font-Bold="true" AutoPostBack="true"
                                                OnCheckedChanged="chkTDS_CheckedChanged" CssClass="LabelNo" />
                                        </td>
                                        <td>
                                            <cc2:ComboBox ID="ddlContractCode" runat="server" EnableLoadOnDemand="true" DataTextField="CONTRACT_CODE"
                                                DataValueField="CONTRACT_CODE" Height="200px" CssClass="SmallFont" EmptyText="Select Contract Code"
                                                MenuWidth="650px" Width="80px" OnLoadingItems="ddlContractCode_LoadingItems">
                                                <HeaderTemplate>
                                                    <div class="header c2">
                                                        Contract Code</div>
                                                    <div class="header c3">
                                                        Description</div>
                                                    <div class="header c1">
                                                        Section</div>
                                                    <div class="header c2">
                                                        Start Date</div>
                                                    <div class="header c2">
                                                        End Date</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c2">
                                                        <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CONTRACT_CODE") %>' /></div>
                                                    <div class="item c3">
                                                        <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("CONTRACT_DESC") %>' /></div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("SECTION") %>' /></div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("START_DATE", "{0:M-dd-yyyy}") %>' /></div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("END_DATE", "{0:M-dd-yyyy}") %>' /></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnTDS" runat="server" Text="Save TDS" OnClick="btnTDS_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trTDSGrid" runat="server">
                            <td>
                                <asp:GridView ID="grdTDS" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    CellSpacing="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Particulars">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" CssClass="LabelNo" runat="server" Text='<%# Bind("ENTRY_TYPE") %>'></asp:Label>
                                                <asp:Label ID="Label2" CssClass="LabelNo" runat="server" Text='<%# Bind("LEDGER_NAME") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="200px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Debit Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblDr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label3" runat="server" Text='<%# Bind("DR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Credit Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblCr_Amount_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label4" runat="server" Text='<%# Bind("CR_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Tax In (%)">
                                            <FooterTemplate>
                                                <asp:Label ID="lblTax_ftr" runat="server" CssClass="LabelNo"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label5" runat="server" Text='<%# Bind("TAX_PERCENT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr id="trFinalAmt" runat="server">
                            <td>
                                <asp:GridView ID="grdFinalAmt" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                    CellSpacing="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sum Of Adviced Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAdvAmount" runat="server" Text='<%# Bind("ADV_AMOUNT") %>' CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TDS Deducted Amount" FooterText="Total Payable Amount :">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTDSAmount" runat="server" Text='<%# Bind("TDS_AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Payable Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPayAmount" runat="server" Text='<%# Bind("PAY_AMOUNT") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblTotalPayAmt" runat="server" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                    <FooterStyle CssClass="titleheading" BackColor="#336699" />
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
