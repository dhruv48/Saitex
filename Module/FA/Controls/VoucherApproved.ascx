<%@ Control Language="C#" AutoEventWireup="true" CodeFile="VoucherApproved.ascx.cs"
    Inherits="Module_FA_Controls_VoucherApproved" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table aign="left" class="tContentArial" width="100%">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
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
                    <b class="titleheading">Voucher Approval</b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%" class="td">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr id="trVoucher" runat="server" align="center">
                <td class="td">
                    Select Voucher :
                    <asp:DropDownList ID="cmbVoucher" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                        Width="150px" DataTextField="VCHR_DTL" DataValueField="VCHR_NO" TabIndex="2"
                        CssClass="SmallFont" OnSelectedIndexChanged="cmbVoucher_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="trDate" runat="server">
                <td class="td" align="center">
                    <table style="width: 600px">
                        <tr>
                            <td align="right">
                                Start Date :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtstartdate" runat="server" Width="80px"></asp:TextBox>
                            </td>
                            <td align="right">
                                End Date :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtenddate" runat="server" Width="80px"></asp:TextBox>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnSearch" Text="Search" runat="server" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="trTotalRecord" runat="server">
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"
                        CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr id="trgrid" runat="server">
                <td align="left" class="td" width="100%">
                    <asp:GridView ID="grdVoucherApproval" CssClass="SmallFont" runat="server" AllowSorting="True"
                        AutoGenerateColumns="False" OnRowDataBound="grdVoucherApproval_RowDataBound"
                        AllowPaging="True" OnPageIndexChanging="grdVoucherApproval_PageIndexChanging" width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="Voucher Code" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoucherCode" runat="server" Text='<%# Bind("VCHR_CODE") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="VCHR_NAME" HeaderText="Voucher Type">
                                <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                    Width="70px" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Voucher Number">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Bind("VCHR_NO") %>' CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblVoucherDate" runat="server" Text='<%# Bind("JOURNAL_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass="Label smallfont"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DESCRIPTION" HeaderText="Description">
                                <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Approved">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkApproved" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved Date">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtApprovedDate" runat="server" ReadOnly="true" Text='<%# Bind("APPR_DATE", "{0:dd-MM-yyyy}") %>'
                                        CssClass=" TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved By">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtApprovedBy" runat="server" ReadOnly="true" Text='<%# Bind("APPR_BY") %>'
                                        ToolTip='<%# Bind("APPR_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Show Details">
                                <ItemTemplate>
                                    <asp:Panel ID="pnlView" runat="server">
                                        <asp:LinkButton ID="lbtnView" runat="server" Text="View Detail" CssClass="Label"></asp:LinkButton>
                                    </asp:Panel>
                                    <asp:Panel ID="pnlShowHover" runat="server" BackColor="Beige" BorderWidth="2px"
                                        >
                                        <asp:GridView ID="grdJourenaldetails" runat="server" Width="530px" CssClass="SmallFont"
                                            AutoGenerateColumns="False" Height="160px">
                                            <Columns>
                                                <asp:BoundField DataField="VCHR_NO" HeaderText="Code">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="ENTRY_TYPE" HeaderText="Type">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="LDGR_NAME" HeaderText="Ledger Name">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DR_AMOUNT" HeaderText="Dr Amt"  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign = "Right"  FooterStyle-HorizontalAlign = "Right" FooterStyle-Font-Bold = "true">
                                                    <ItemStyle  Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CR_AMOUNT" HeaderText="Cr Amt"  HeaderStyle-HorizontalAlign ="Right" ItemStyle-HorizontalAlign = "Right"  FooterStyle-HorizontalAlign = "Right" FooterStyle-Font-Bold = "true">
                                                    <ItemStyle  Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOC_NO" HeaderText="Doc No" HeaderStyle-HorizontalAlign ="Center">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DOC_DT" HeaderText="Doc Date" HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-HorizontalAlign ="Center">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DESCRIPTION" HeaderText="Narration" HeaderStyle-HorizontalAlign ="Center">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CONF_BY" HeaderText="Confirm By" HeaderStyle-HorizontalAlign ="Center">
                                                    <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CONF_DATE" HeaderText="Confirm Date" HtmlEncode="false"
                                                    DataFormatString="{0:dd-MM-yyyy}" HeaderStyle-HorizontalAlign ="Center">
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
        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtstartdate" PopupPosition="TopLeft"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtenddate"
            Format="dd/MM/yyyy" PopupPosition="TopLeft">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtstartdate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtenddate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
