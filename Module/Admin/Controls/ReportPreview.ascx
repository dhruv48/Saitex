<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ReportPreview.ascx.cs"
    Inherits="Module_Admin_Controls_ReportPreview" %>
<asp:Panel ID="pnlReportPreview" runat="server">
    <table>
        <tr>
            <td id="tdReportPreviewFilter" runat="server">
                <asp:Label ID="lblSelectQuery" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblWhereQuery" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSortQuery" runat="server" Visible="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="grdReportPreview" runat="server" AutoGenerateColumns="False" Width="98%"
                    CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-Wrap="true"
                    Font-Size="X-Small" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="grdReportPreview_PageIndexChanging"
                    PageSize="20">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                    </Columns>
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Panel>
