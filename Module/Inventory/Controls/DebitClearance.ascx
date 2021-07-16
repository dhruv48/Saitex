<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DebitClearance.ascx.cs"
    Inherits="Module_Inventory_Controls_DebitClearance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 50px;
    }
    .c2
    {
        margin-left: 5px;
        width: 280px;
    }
    .c3
    {
        margin-left: 5px;
        width: 90px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    ToolTip="Save" ValidationGroup="M1" Width="48" Visible="False" />
                            </td>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFindTop_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
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
                    <b class="titleheading">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%" class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr id="trTotalRecord" runat="server">
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"
                        CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr id="trgrid" runat="server">
                <td align="left" class="td">
                    <asp:GridView ID="grdDrCrNoteClearance" CssClass="SmallFont" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="grdDrCrNoteClearance_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Note Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillType" runat="server" Text='<%# Bind("NOTE_TYPE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="ADVICE_NO" HeaderText="Advice #">
                                <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                    Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Advice Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("ADVICE_DT", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyCode" runat="server" HtmlEncode="false" Text='<%# Bind("PRTY_CODE") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="PRTY_NAME" HeaderText="Party Name">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:BoundField DataField="ADDRESS" HeaderText="Party Address">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Confirm">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkConfirmed" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advice Amount" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="txtBillDate" runat="server" ReadOnly="true" Text='<%# Bind("ADVICE_AMT") %>'
                                        CssClass="SmallFont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Note Comment">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtComment" runat="server" CssClass="TextBox  SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Show Details">
                                <ItemTemplate>
                                    <asp:Panel ID="pnlView" runat="server">
                                        <asp:LinkButton ID="lbtnView" runat="server" Text="View Detail" CssClass="Label"></asp:LinkButton>
                                        <asp:Label ID="lblCombinedid" runat="server" Text="Label" Visible="false" ToolTip='<%# Eval("BillID") %>'></asp:Label>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlShowHover" runat="server" Width="450px" BackColor="Beige" BorderWidth="2px"
                                        Height="140px" ScrollBars="Auto">
                                        <asp:GridView ID="grdTrndetails" runat="server" Width="450px" CssClass="SmallFont"
                                            AutoGenerateColumns="False" Height="140px">
                                            <Columns>
                                                <asp:BoundField DataField="YEAR" HeaderText="Year">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="NOTE_TYPE" HeaderText="Type">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ADVICE_NO" HeaderText="Advice No">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TDATE" HeaderText="Trn Date">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TRN_AMT" HeaderText="TRN AMT">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                            </Columns>
                                            <RowStyle CssClass="SmallFont" Width="98%" />
                                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                        </asp:GridView>
                                    </asp:Panel>
                                    <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="pnlView"
                                        PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
                                    </cc1:HoverMenuExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="SmallFont" Width="98%" />
                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                    </asp:GridView>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
