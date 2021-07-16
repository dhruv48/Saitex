<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Warping_issu_4Prod.aspx.cs"
    Inherits="Module_Production_Pages_Warping_issu_4Prod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td align="center" valign="top" colspan="6">
                    <strong>Issu Against PA (Wapring)</strong>
                </td>
            </tr>
            <tr>
                <td width="15%">
                    Branch:
                </td>
                <td width="15%">
                    <asp:TextBox ID="txtbranch" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                </td>
                <td width="15%">
                    Yarn Code:
                </td>
                <td width="15%">
                    <asp:TextBox ID="txtyrn_code" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                </td>
                <td width="15%">
                    Shade Code:
                </td>
                <td width="15%">
                    <asp:TextBox ID="txtshade_code" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td>
                    <asp:GridView ID="grd_issu_pa_warp" runat="server" AutoGenerateColumns="false" Width="100%"
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Trn Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbltrn_type" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                        runat="server" Text='<%# Bind("TRN_TYPE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trn Number">
                                <ItemTemplate>
                                    <asp:Label ID="lbltrn_numb" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                        runat="server" Text='<%# Bind("TRN_NUMB") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trn Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbltrn_qty" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                        runat="server" Text='<%# Bind("TRN_QTY") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lblissue_qty" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                        runat="server" Text='<%# Bind("ISS_QTY") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Basic Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblbasic_rate" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                        runat="server" Text='<%# Bind("BASIC_RATE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Final Rate">
                                <ItemTemplate>
                                    <asp:Label ID="lblbasic_rate" Width="80px" Font-Bold="true" CssClass="Label SmallFont"
                                        runat="server" Text='<%# Bind("FINAL_RATE") %>' AutoCompleteType="Disabled" ReadOnly="true"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="10%" />
                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
