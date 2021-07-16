<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnLineDebitReceive.ascx.cs"
    Inherits="Module_FA_Controls_OnLineDebitReceive" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
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
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;:&nbsp;<asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td">
                    <asp:GridView ID="grdDrCrNoteClearance" CssClass="SmallFont" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdDrCrNoteClearance_PageIndexChanging"
                        OnRowDataBound="grdDrCrNoteClearance_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Advice Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillEntryDT" runat="server" Text='<%# Bind("ADVICE_DT", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Note Type">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillType" runat="server" Text='<%# Bind("NOTE_TYPE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advice No">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillNumb" runat="server" Text='<%# Bind("ADVICE_NO") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyCode" runat="server" Text='<%# Bind("PRTY_CODE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Party Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblPartyName" runat="server" Text='<%# Bind("PRTY_NAME") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bill Year" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillYear" runat="server" Text='<%# Bind("BILL_YEAR") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server" Text='<%# Bind("ADDRESS") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Advice Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillAmt" runat="server" Text='<%# Bind("ADVICE_AMT") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Clearance Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblBillClearDT" runat="server" Text='<%# Bind("BILL_CLR_PUR_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkReceived" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Receiving Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReceivingDate" runat="server" ReadOnly="true" Text='<%# Bind("RECEIVED_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass=" TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received By">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtConfirmBy" runat="server" ReadOnly="true" Text='<%# Bind("RECEIVED_BY") %>'
                                        ToolTip='<%# Bind("RECEIVED_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Show Details">
                                <ItemTemplate>
                                    <asp:Panel ID="pnlView" runat="server">
                                        <asp:LinkButton ID="lbtnView" runat="server" Text="View Detail" CssClass="Label"></asp:LinkButton>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlShowHover" runat="server" Width="400px" BackColor="Beige" BorderWidth="2px"
                                        Height="100px" ScrollBars="Auto">
                                        <asp:GridView ID="grdBillDTL" runat="server" Width="400px" CssClass="SmallFont" AutoGenerateColumns="False"
                                            Height="100px">
                                            <Columns>
                                                <asp:BoundField DataField="TRN_TYPE" HeaderText="Tran Type">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TRN_NUMB" HeaderText="Tran Number">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRTY_CODE" HeaderText="Party Code" Visible="false">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRTY_NAME" HeaderText="Party Name">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="QUALITY_POINT" HeaderText="Quality Point">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DEL_POINT" HeaderText="Delivery Point">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="PRICE_POINT" HeaderText="Price Point">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="SUPPORT_POINT" HeaderText="Support Point">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="TRN_AMT" HeaderText="Tran Amount">
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
