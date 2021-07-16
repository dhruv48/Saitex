<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_QC_Standard_Approval.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_QC_Standard_Approval" %>
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
            <b class="titleheading">Yarn QC Standard Master Approval</b>
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
                    <asp:TemplateField HeaderText="QC&nbsp;No." HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" Text='<%#Eval("TRN_NUMB") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Inward&nbsp;Type" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblINWARD_TYPE" runat="server" Text='<%#Eval("INWARD_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                   
                    <asp:TemplateField HeaderText="Yarn&nbsp;Category" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblYARN_CATEGORY" runat="server" Text='<%#Eval("YARN_CATEGORY") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Yarn&nbsp;Code" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblYARN_CODE" runat="server" Text='<%#Eval("YARN_CODE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn&nbsp;Description" ItemStyle-HorizontalAlign="Left"
                        HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                    <asp:TemplateField HeaderText="Std&nbsp;Type" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lblSTD_TYPE" runat="server" Text='<%#Eval("STD_TYPE") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
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
