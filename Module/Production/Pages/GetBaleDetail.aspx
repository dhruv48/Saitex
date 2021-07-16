<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GetBaleDetail.aspx.cs" Inherits="Module_Production_Pages_GetBaleDetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get Bale Details</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Get Bale Details </strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <asp:Label ID="lblErrormsg" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <table>
                                <tr>
                                    <td class="TableHeader">
                                        <span class="titleheading">Bale Code</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Bale Qty</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Bale Width</span>
                                    </td>
                                    <td class="TableHeader">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtBaleCode" runat="server" CssClass="SmallFont TextBoxNo" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBaleQty" runat="server" CssClass="SmallFont TextBoxNo" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBaleWidth" CssClass="SmallFont TextBoxNo" runat="server" Width="60px"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSaveBaleDetail" CssClass="SmallFont " runat="server" OnClick="btnSaveBaleDetail_Click"
                                            Text="Save"></asp:Button>
                                        <asp:Button ID="btnCancelBaleDetail" CssClass="SmallFont " runat="server" OnClick="btnCancelBaleDetail_Click"
                                            Text="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="left" valign="top">
                            <asp:Panel ID="pnlBaleDetails" runat="server" BackColor="#afcae4" ScrollBars="Vertical"
                                Height="150px">
                                <asp:GridView ID="grdBaleDetails" runat="server" AutoGenerateColumns="False" OnRowCommand="grdBaleDetails_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Bale Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaleCode" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BALE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bale Qty.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaleQty" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BALE_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bale Width">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaleWidth" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BALE_WIDTH") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" CommandName="editBale"
                                                    CommandArgument='<%# Bind("BALE_ID") %>' Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="delBale"
                                                    CommandArgument='<%# Bind("BALE_ID") %>' Text="Remove"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdBaleDetails" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelBaleDetail" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSaveBaleDetail" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
