<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AssetDTL.ascx.cs" Inherits="Module_FA_Controls_AssetDTL" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
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
        margin-left: 4px;
        width: 220px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 80px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td class="td" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="1" align="left" class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1">
                                </asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center" colspan="3">
                    <b class="titleheading">Assets Acquisition Details</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                        <tr>
                            <td align="left" colspan="6" valign="top">
                                <span class="Mode">You are in
                                    <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="6">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="false" ValidationGroup="M1" />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Asset Code :
                            </td>
                            <td align="left" valign="top">
                                <cc1:ComboBox ID="cmbAssetCode" runat="server" Width="133px" Height="200px" AutoPostBack="True"
                                    EnableLoadOnDemand="True" EmptyText="Select Asset Code" DataTextField="ASSET_CODE"
                                    DataValueField="ASSET_CODE" TabIndex="1" MenuWidth="550px" OnLoadingItems="cmbAssetCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbAssetCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Code</div>
                                        <div class="header c2">
                                            Asset Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <%# Eval("ASSET_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("ASSET_DTL")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                            </td>
                            <td align="right" valign="top">
                                *Asset Detail Type :
                            </td>
                            <td align="left" valign="top" colspan="3">
                                <asp:DropDownList ID="cmbDetailType" runat="server" AppendDataBoundItems="true" DataTextField="MST_CODE"
                                    DataValueField="MST_CODE" CssClass="SmallFont" TabIndex="2" Width="133px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFDetailType" runat="server" ControlToValidate="cmbAssetGroup"
                                    Display="None" ErrorMessage="Please Select Detail Type" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 10px">
                            </td>
                        </tr>
                        <tr>
                            <td class="td" colspan="6" runat="server" id="trMaster">
                                <table>
                                    <tr>
                                        <td class="SmallFont tdLeft" colspan="6">
                                            <span class="TableHeader titleheading"><i><font face="verdana" color="yellow">asset
                                                acquisition details....</font></i></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:GridView ID="grdAssetsDTL" CssClass="SmallFont" runat="server" AllowSorting="True"
                                                AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grdAssetsDTL_PageIndexChanging"
                                                OnRowDataBound="grdAssetsDTL_RowDataBound" PageSize="5" TabIndex="3">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Asset Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssetCode" runat="server" Text='<%# Bind("ASSET_CODE") %>' CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Asset Tran ID" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssetTranID" runat="server" Text='<%# Bind("ASSET_TRN_ID") %>'
                                                                CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ASSET_DTL" HeaderText="Asset Detail">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DETAIL_TYPE" HeaderText="Detail Type">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Asset Group Code" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAssetGrpCode" runat="server" Text='<%# Bind("ASSET_GRP_CODE") %>'
                                                                CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="ASSET_GRP_NAME" HeaderText="Asset Group Name">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Purchase Voucher">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Bind("VCHR_NO") %>' CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVoucherDate" runat="server" HtmlEncode="false" Text='<%# Bind("JOURNAL_DATE", "{0:dd-MM-yyyy}") %>'
                                                                CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="VOUCHER_AMT" HeaderText="PV Amount">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="TDS Voucher">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTDSVoucherNo" runat="server" Text='<%# Bind("TDS_VCHR_NO") %>'
                                                                CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDS Voucher Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTDSDate" runat="server" HtmlEncode="false" Text='<%# Bind("JOURNAL_DATE", "{0:dd-MM-yyyy}") %>'
                                                                CssClass="Label smallfont"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="TDS_AMT" HeaderText="TDS Amount">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ASSET_LOCATION_NAME" HeaderText="Branch">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ASSET_DEPT_NAME" HeaderText="Department">
                                                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" VerticalAlign="Top"
                                                            Width="70px" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField HeaderText="Asset Details">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlAssetView" runat="server">
                                                                <asp:LinkButton ID="lbtnAssetView" runat="server" Text="Show Details" CssClass="Label"></asp:LinkButton>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlAssetShowHover" runat="server" Width="600px" BackColor="Beige"
                                                                BorderWidth="2px" Height="90px" ScrollBars="Auto">
                                                                <asp:GridView ID="grdAssetTranDetail" runat="server" Width="600px" CssClass="SmallFont"
                                                                    AutoGenerateColumns="False" Height="90px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="SAL_DEPR_AMT" HeaderText="Sale Depr Amt" HeaderStyle-Width="200px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SAL_IT_AMT" HeaderText="Sale IT Amt" HeaderStyle-Width="200px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="SAL_COMP_ACT_AMT" HeaderText="Sale Comp Act Amt" HeaderStyle-Width="200px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DT_OF_MANUFACT" HeaderText="Manufacturing Date" HeaderStyle-Width="200px"
                                                                            HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DT_OF_PUR" HeaderText="Purchase Date" HeaderStyle-Width="200px"
                                                                            HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DT_OF_INSTAL" HeaderText="Installation Date" HeaderStyle-Width="200px"
                                                                            HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DT_OF_PUT_IN_USE" HeaderText="Put In Use Date" HeaderStyle-Width="200px"
                                                                            HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <cc4:HoverMenuExtender ID="HoverMenuExtender1" runat="server" TargetControlID="pnlAssetView"
                                                                PopupControlID="pnlAssetShowHover" PopupPosition="Left" PopDelay="10">
                                                            </cc4:HoverMenuExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="TDS Details">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlTDSView" runat="server">
                                                                <asp:LinkButton ID="lbtnTDSView" runat="server" Text="Show TDS" CssClass="Label"></asp:LinkButton>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlTDSShowHover" runat="server" Width="650px" BackColor="Beige" BorderWidth="2px"
                                                                Height="110px" ScrollBars="Auto">
                                                                <asp:GridView ID="grdTDSJourenaldetails" runat="server" Width="650px" CssClass="SmallFont"
                                                                    AutoGenerateColumns="False" Height="110px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="VCHR_NO" HeaderText="Code" HeaderStyle-Width="150px" HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ENTRY_TYPE" HeaderText="Type" HeaderStyle-Width="120px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="LDGR_NAME" HeaderText="Ledger Name" HeaderStyle-Width="700px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DR_AMOUNT" HeaderText="Dr Amt" HeaderStyle-Width="300px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="CR_AMOUNT" HeaderText="Cr Amt" HeaderStyle-Width="300px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DOC_NO" HeaderText="Doc No" HeaderStyle-Width="300px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DOC_DT" HeaderText="Doc Date" HeaderStyle-Width="150px"
                                                                            HeaderStyle-Height="1px" HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Narration" HeaderStyle-Width="1000px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <cc4:HoverMenuExtender ID="HoverMenuExtender2" runat="server" TargetControlID="pnlTDSView"
                                                                PopupControlID="pnlTDSShowHover" PopupPosition="Left" PopDelay="10">
                                                            </cc4:HoverMenuExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher Details">
                                                        <ItemTemplate>
                                                            <asp:Panel ID="pnlView" runat="server">
                                                                <asp:LinkButton ID="lbtnView" runat="server" Text="Show PV" CssClass="Label"></asp:LinkButton>
                                                            </asp:Panel>
                                                            <asp:Panel ID="pnlShowHover" runat="server" Width="650px" BackColor="Beige" BorderWidth="2px"
                                                                Height="110px" ScrollBars="Auto">
                                                                <asp:GridView ID="grdJourenaldetails" runat="server" Width="650px" CssClass="SmallFont"
                                                                    AutoGenerateColumns="False" Height="110px">
                                                                    <Columns>
                                                                        <asp:BoundField DataField="VCHR_NO" HeaderText="Code" HeaderStyle-Width="150px" HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ENTRY_TYPE" HeaderText="Type" HeaderStyle-Width="120px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="LDGR_NAME" HeaderText="Ledger Name" HeaderStyle-Width="700px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DR_AMOUNT" HeaderText="Dr Amt" HeaderStyle-Width="300px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="CR_AMOUNT" HeaderText="Cr Amt" HeaderStyle-Width="300px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DOC_NO" HeaderText="Doc No" HeaderStyle-Width="300px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DOC_DT" HeaderText="Doc Date" HeaderStyle-Width="150px"
                                                                            HeaderStyle-Height="1px" HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="DESCRIPTION" HeaderText="Narration" HeaderStyle-Width="1000px"
                                                                            HeaderStyle-Height="1px">
                                                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                                        </asp:BoundField>
                                                                    </Columns>
                                                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                                                </asp:GridView>
                                                            </asp:Panel>
                                                            <cc4:HoverMenuExtender ID="HoverMenuExtender3" runat="server" TargetControlID="pnlView"
                                                                PopupControlID="pnlShowHover" PopupPosition="Left" PopDelay="10">
                                                            </cc4:HoverMenuExtender>
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
                            </td>
                        </tr>
                        <tr id="trSpace" runat="server">
                            <td colspan="6" style="height: 8px">
                            </td>
                        </tr>
                        <tr>
                            <td class="SmallFont tdLeft" colspan="6">
                                <span class="TableHeader titleheading"><i><font face="verdana" color="yellow">asset
                                    attachment details....</font></i></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Sale Depr Amount :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSalDeprAmt" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="15" TabIndex="4"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Sale IT Amount :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSalDeprITAmt" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="15" TabIndex="5"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Sale Comp Act Amount :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSaleCompActAmt" runat="server" CssClass="TextBoxNo" ValidationGroup="M1"
                                    MaxLength="15" TabIndex="6"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Date Of Put In Use :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtDTOfPutInUse" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    TabIndex="7"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                *Date Of Purchase :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtDTOfPurchase" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    TabIndex="8"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                *Date Of Manufacturing :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtDTOfManufacturing" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    TabIndex="9"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Date Of Installation :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtDTOfInstall" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    TabIndex="10"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Asset Description :
                            </td>
                            <td align="left" valign="top" colspan="3">
                                <asp:TextBox ID="txtAssetDescription" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="200" TabIndex="11" Width="420px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Branch :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="cmbLocation" runat="server" AppendDataBoundItems="true" DataTextField="BRANCH_NAME"
                                    DataValueField="BRANCH_CODE" CssClass="SmallFont" TabIndex="12" Width="133px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFLocation" runat="server" ControlToValidate="cmbLocation"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                *Department :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="cmbDepartment" runat="server" AppendDataBoundItems="true" DataTextField="DEPT_NAME"
                                    DataValueField="DEPT_CODE" CssClass="SmallFont" TabIndex="13" Width="133px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFDepartment" runat="server" ControlToValidate="cmbDepartment"
                                    Display="None" ErrorMessage="Please Select Department" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                *Asset Group :
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="cmbAssetGroup" runat="server" AppendDataBoundItems="true" DataTextField="ASSET_GRP_CODE"
                                    DataValueField="ASSET_GRP_CODE" CssClass="SmallFont" TabIndex="14" Width="133px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFAssetGroup" runat="server" ControlToValidate="cmbAssetGroup"
                                    Display="None" ErrorMessage="Please Select Asset Group" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" style="height: 8px">
                            </td>
                        </tr>
                        <tr>
                            <td class="SmallFont tdLeft" colspan="6">
                                <span class="TableHeader titleheading"><i><font face="verdana" color="yellow">main details....</font></i></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Voucher Type :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="ddlVoucherType" runat="server" CssClass="TextBox TextBoxDisplay"
                                    ValidationGroup="M1" TabIndex="15"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                *Voucher Number:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtVoucherNo" runat="server" CssClass="TextBox TextBoxDisplay" TabIndex="16"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                *Date :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtJournalDate" runat="server" CssClass="TextBox" TabIndex="17"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Description :
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="TextBox" ValidationGroup="M1"
                                    MaxLength="200" TabIndex="18" Width="614px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="SmallFont tdLeft" colspan="6">
                                <span class="TableHeader titleheading"><i><font face="verdana" color="yellow">transaction
                                    details....</font></fontcolor></i></span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" colspan="6">
                                <table>
                                    <tr>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr bgcolor="#336699" class="titleheading">
                                                    <td colspan="2">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Particulars
                                                    </td>
                                                    <td>
                                                        Debit
                                                    </td>
                                                    <td>
                                                        Credit
                                                    </td>
                                                    <td>
                                                        Doc No
                                                    </td>
                                                    <td>
                                                        Doc Date
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:DropDownList ID="ddlEntry_Type" runat="server" Width="50px" TabIndex="19" CssClass="SmallFont"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlEntry_Type_SelectedIndexChanged">
                                                            <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                            <asp:ListItem Value="Cr">Cr</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlLedgerCode" runat="server" AppendDataBoundItems="true" Width="190px"
                                                            DataTextField="LDGR_NAME" DataValueField="LDGR_CODE" TabIndex="20" CssClass="SmallFont"
                                                            AutoPostBack="true" OnSelectedIndexChanged="ddlLedgerCode_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDebitAmount" runat="server" Width="80px" TabIndex="21" CssClass="TextBoxNo"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCreditAmount" runat="server" Width="80px" TabIndex="22" CssClass="TextBoxNo"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDocNo" runat="server" Width="80px" TabIndex="23" CssClass="TextBox UpperCase"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDocDT" runat="server" Width="90px" TabIndex="24" CssClass="TextBox"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnSaveDetail" runat="server" Text="Save" TabIndex="25" OnClick="btnSaveDetail_Click">
                                                        </asp:Button>
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="26" OnClick="btnCancel_Click">
                                                        </asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="tdRight">
                                                        Narration :
                                                    </td>
                                                    <td class="tdLeft" colspan="6">
                                                        <asp:TextBox ID="txtTranDescription" runat="server" CssClass="TextBox" Width="620px"
                                                            MaxLength="200" TabIndex="27"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdJourenaldetails" runat="server" AutoGenerateColumns="False"
                                                ShowFooter="True" CellSpacing="0" Width="730px" TabIndex="28" OnRowCommand="grdJourenaldetails_RowCommand1">
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
                                                    <asp:TemplateField HeaderText="Doc No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("DOC_NO") %>' CssClass="LabelNo"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Doc Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label6" runat="server" Text='<%# Bind("DOC_DT", "{0:M-dd-yyyy}") %>'
                                                                CssClass="LabelNo"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Narration">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text='<%# Bind("DESC") %>' CssClass="LabelNo"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnEdit" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                                CommandName="EditTRN" Text="Edit" />
                                                            <asp:Button ID="btnDelete" runat="server" CommandArgument='<%# bind("UNIQUE_ID") %>'
                                                                CommandName="DeleteTRN" Text="Delete" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <RowStyle VerticalAlign="Top" />
                                                <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr id="trTDS" runat="server">
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblTDSLedgerCode" runat="server" Visible="false" CssClass="LabelNo"></asp:Label>
                                                        <asp:Label ID="lblTDSText" runat="server" Visible="false" CssClass="LabelNo" Text="Tax Applicable On : "></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblTDSLedgerName" runat="server" CssClass="LabelNo"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBox ID="chkTDS" runat="server" Text="Deduct TDS" Font-Bold="true" AutoPostBack="true"
                                                            CssClass="LabelNo" TabIndex="29" OnCheckedChanged="chkTDS_CheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <cc1:ComboBox ID="ddlContractCode" runat="server" EnableLoadOnDemand="true" DataTextField="CONTRACT_CODE"
                                                            DataValueField="CONTRACT_CODE" Height="200px" CssClass="SmallFont" EmptyText="Code"
                                                            MenuWidth="650px" Width="70px" TabIndex="30" OnLoadingItems="ddlContractCode_LoadingItems">
                                                            <HeaderTemplate>
                                                                <div class="header c1">
                                                                    Code</div>
                                                                <div class="header c2">
                                                                    Description</div>
                                                                <div class="header c5">
                                                                    Section</div>
                                                                <div class="header c3">
                                                                    Start Date</div>
                                                                <div class="header c3">
                                                                    End Date</div>
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <div class="item c1">
                                                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CONTRACT_CODE") %>' /></div>
                                                                <div class="item c2">
                                                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("CONTRACT_DESC") %>' /></div>
                                                                <div class="item c5">
                                                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("SECTION") %>' /></div>
                                                                <div class="item c3">
                                                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("START_DATE", "{0:M-dd-yyyy}") %>' /></div>
                                                                <div class="item c3">
                                                                    <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("END_DATE", "{0:M-dd-yyyy}") %>' /></div>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                Displaying items
                                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                                out of
                                                                <%# Container.ItemsCount %>.
                                                            </FooterTemplate>
                                                        </cc1:ComboBox>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnTDS" runat="server" Text="Save TDS" TabIndex="31" OnClick="btnTDS_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="trTDSGrid" runat="server">
                                        <td>
                                            <asp:GridView ID="grdTDS" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                                CellSpacing="0" Width="548px" TabIndex="32">
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
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:RangeValidator ID="RVSalDeprAmt" runat="server" ValidationGroup="M1" Display="None"
            ControlToValidate="txtSalDeprAmt" MinimumValue="0" MaximumValue="999999999" ErrorMessage="Please Enter Numeric !"
            Type="Double" SetFocusOnError="true"></asp:RangeValidator>
        <asp:RangeValidator ID="RVSalDeprITAmt" runat="server" ValidationGroup="M1" Display="None"
            ControlToValidate="txtSalDeprITAmt" MinimumValue="0" MaximumValue="999999999"
            ErrorMessage="Please Enter Numeric !" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
        <asp:RangeValidator ID="RVSaleCompActAmt" runat="server" ValidationGroup="M1" Display="None"
            ControlToValidate="txtSaleCompActAmt" MinimumValue="0" MaximumValue="999999999"
            ErrorMessage="Please Enter Numeric !" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
        <asp:RangeValidator ID="RVDebitAmount" runat="server" ValidationGroup="M1" Display="None"
            ControlToValidate="txtDebitAmount" MinimumValue="0" MaximumValue="999999999"
            ErrorMessage="Please Enter Numeric !" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RFSalDeprAmt" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Sale Depreciation Amount" ControlToValidate="txtSalDeprAmt"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFSaleDeprITAmt" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Sale Depreciation IT Amount" ControlToValidate="txtSalDeprITAmt"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFSaleCompActAmt" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Sale Company Act Amount" ControlToValidate="txtSaleCompActAmt"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFDTOfManufacturing" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Date Of Manufacturing" ControlToValidate="txtDTOfManufacturing"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFDTOfPurchase" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Date Of Purchase" ControlToValidate="txtDTOfPurchase"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFDTOfInstall" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Date Of Installation" ControlToValidate="txtDTOfInstall"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFDTOfPutInUse" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. Enter Put In Use Date" ControlToValidate="txtDTOfPutInUse"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RVCreditAmount" runat="server" ValidationGroup="M1" Display="None"
            ControlToValidate="txtCreditAmount" MinimumValue="0" MaximumValue="999999999"
            ErrorMessage="Please Enter Numeric !" Type="Double" SetFocusOnError="true"></asp:RangeValidator>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtDTOfManufacturing"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDTOfManufacturing" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDTOfPurchase"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDTOfPurchase" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDTOfInstall"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDTOfInstall" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtDTOfPutInUse"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtDocDT"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDTOfPutInUse" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtJournalDate"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtJournalDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDocDT" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
