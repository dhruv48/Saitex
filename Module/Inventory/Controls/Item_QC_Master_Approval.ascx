<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Item_QC_Master_Approval.ascx.cs"
    Inherits="Module_Inventory_Controls_Item_QC_Master_Approval" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Item QC Standard Master Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="center" width="100%" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                AllowSorting="True" AutoGenerateColumns="False" Width="95%">
                <Columns>
                    <asp:BoundField DataField="TRN_NUMB" HeaderText="Sr.&nbsp;No." ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:TemplateField HeaderText="Item&nbsp;Category" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblITEM_CATEGORY" runat="server" Text='<%#Eval("ITEM_CATEGORY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Item&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblITEM_CODE" runat="server" Text='<%#Eval("ITEM_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:BoundField DataField="STD_VALUE" HeaderText="Std&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:BoundField DataField="STD_TYPE" HeaderText="Std&nbsp;Type" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:BoundField DataField="TOLERANCE" HeaderText="Tolerance" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:BoundField DataField="TOLERANCE_TYPE" HeaderText="Tolerance&nbsp;Type" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:BoundField DataField="TOLERANCE_RANGE" HeaderText="Tolerance&nbsp;Range" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:BoundField DataField="MAX_VALUE" HeaderText="Max&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:BoundField DataField="MIN_VALUE" HeaderText="Min&nbsp;Value" ItemStyle-HorizontalAlign="Right"
                        HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    <asp:BoundField DataField="UOM" HeaderText="UOM" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:BoundField DataField="REMARKS" HeaderText="Remarks" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                  
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
