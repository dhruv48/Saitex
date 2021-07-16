<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Statement_Of_Account.ascx.cs"
    Inherits="Module_FA_Controls_Statement_Of_Account" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%--<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>--%>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<script type="text/javascript">
        function onPopulateControls(sender, record) {
            record.PARENT_ID = sender.ForeignKeys.ACCOUNT_ID.Value;

            return record;
        }

        function onBeforeClientDelete(sender, record) {
            return confirm('Are you sure you want to delete this Account?');
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
        width: 200px;
    }
    .c3
    {
        margin-left: 4px;
        width: 200px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table width="98%">
    <tr>
        <td class="td" width="100%">
            <table>
                <tr>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr class="TableHeader">
        <td class="td" align="center" valign="top" width="100%">
            <span class="titleheading">Statement Of Account</span>
        </td>
    </tr>
    <tr>
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td class="tdRight">
                        <asp:Label ID="lblStartMonth" runat="server" CssClass="Label" Text="Date From :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartingDate" runat="server" CssClass="TextBox" Width="100px"
                            TabIndex="1"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Label ID="lblEndMonth" runat="server" CssClass="Label" Text="Date To :"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndingDate" runat="server" CssClass="TextBox" Width="100px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr id="trSelectVoucherType" runat="server">
                    <td class="TableHeader SmallFont tdLeft" colspan="4">
                        <span class="titleheading"><i>select group.... </i></span>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <cc2:ComboBox ID="ddlGroupCode" EmptyText="select Group" runat="server" Width="190px"
                            Height="250px" DataTextField="GRP_NAME" DataValueField="GRP_CODE" EnableLoadOnDemand="True"
                            MenuWidth="600px" OnLoadingItems="ddlGroupCode_LoadingItems" TabIndex="5">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code
                                </div>
                                <div class="header c2">
                                    Group Name</div>
                                <div class="header c3">
                                    Parent Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("GRP_CODE")%></div>
                                <div class="item c2">
                                    <%# Eval("GRP_NAME")%></div>
                                <div class="item c3">
                                    <%# Eval("PARENT_NAME")%></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnGetLedger" runat="server" OnClick="btnGetLedger_Click" Text="Get Ledger Book">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="ShowLedger" runat="server">
        <td class="td" width="100%">
            <table width="100%">
                <tr>
                    <td align="center" valign="top" width="100%">
                        <asp:Label ID="lblGroupName" runat="server"></asp:Label>
                    </td>
                </tr>
                         <table width="100%">
                        <asp:GridView ID="grdSOA" runat="server" AllowPaging="True" AllowSorting="True" OnDataSourceNeeded="grdSOA_DataSourceNeeded"
                            AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="grdSOA_PageIndexChanging"
                            PagerStyle-HorizontalAlign="Left" PageSize="20" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="ACCOUNT_ID" HeaderText="ACCOUNT_ID" />
                                <asp:BoundField DataField="PARENT_ID" HeaderText="PARENT_ID" />
                                <asp:BoundField DataField="ACCOUNT_NAME" HeaderText="Particulars" />
                                <asp:BoundField DataField="DR_OP_AMOUNT" HeaderText="Opening Bal" />
                                <asp:BoundField DataField="CR_OP_AMOUNT" HeaderText="Opening Bal" />
                                <asp:BoundField DataField="DR_TOTAL" HeaderText="Dr" />
                                <asp:BoundField DataField="CR_TOTAL" HeaderText="Cr" />
                                <asp:BoundField DataField="CR_AMOUNT" HeaderText="Closing Bal. Dr" />
                                <asp:BoundField DataField="DR_AMOUNT" HeaderText="Closing Bal. Cr" />
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </table>
                                        
                                  </table>
               <%-- <tr>
                    <td class="tdLeft" width="100%">
                        <cc3:Grid ID="grdSOA" runat="server" AutoGenerateColumns="False" AllowAddingRecords="false"
                            Serialize="false" OnDataSourceNeeded="grdSOA_DataSourceNeeded" Width="98%" AllowPaging="false"
                            PageSize="-1" AutoPostBackOnSelect="True" AllowPageSizeSelection="false" OnSelect="grdSOA_Select"
                            ShowFooter="false" ShowColumnsFooter="false" ShowTotalNumberOfPages="false" AllowColumnReordering="false"
                            AllowSorting="false" OnRebind="grdSOA_Rebind">
                            <Columns>
                                <cc3:Column DataField="ACCOUNT_ID" Visible="false" Width="1%" />
                                <cc3:Column DataField="PARENT_ID" Visible="false" Width="1%" />
                                <cc3:Column DataField="ACCOUNT_NAME" HeaderText="Particulars" Width="20%" Wrap="true" />
                                <cc3:Column DataField="DR_OP_AMOUNT" ItemStyle-HorizontalAlign="Right" Width="14%"
                                    HeaderText="Opening Bal. Dr" Wrap="true" />
                                <cc3:Column DataField="CR_OP_AMOUNT" ItemStyle-HorizontalAlign="Right" Width="14%"
                                    HeaderText="Opening Bal. Cr" Wrap="true" />
                                <cc3:Column DataField="DR_TOTAL" ItemStyle-HorizontalAlign="Right" Width="11%" HeaderText="Dr"
                                    Wrap="true" />
                                <cc3:Column DataField="CR_TOTAL" ItemStyle-HorizontalAlign="Right" HeaderText="Cr"
                                    Width="11%" Wrap="true" />
                                <cc3:Column DataField="CR_AMOUNT" ItemStyle-HorizontalAlign="Right" Width="14%" HeaderText="Closing Bal. Dr"
                                    Wrap="true" />
                                <cc3:Column DataField="DR_AMOUNT" ItemStyle-HorizontalAlign="Right" Width="14%" HeaderText="Closing Bal. Cr"
                                    Wrap="true" />
                            </Columns>
                            <MasterDetailSettings LoadingMode="OnLoad" ShowEmptyDetails="false" />
                            <ScrollingSettings ScrollHeight="350" />
                        </cc3:Grid>
                    </td>
                </tr>--%>
            </table>
        </td>
    </tr>
</table>
<cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtStartingDate" PopupPosition="TopLeft"
    OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
</cc4:CalendarExtender>
<cc4:CalendarExtender ID="ce2" runat="server" TargetControlID="txtEndingDate" PopupPosition="TopLeft"
    OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
</cc4:CalendarExtender>
<asp:RangeValidator ID="rvStartDate" runat="server" ControlToValidate="txtStartingDate"
    Display="Dynamic" ErrorMessage="Hi Dear, Pls.. enter valid date of this Financial Year"
    Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
<asp:RangeValidator ID="rvEndDate" runat="server" ControlToValidate="txtEndingDate"
    Display="Dynamic" ErrorMessage="Hi Dear, Pls.. enter valid date of this Financial Year"
    Type="Date" ValidationGroup="M1" SetFocusOnError="True"></asp:RangeValidator>
<cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
    MaskType="Date" TargetControlID="txtStartingDate" PromptCharacter="_">
</cc4:MaskedEditExtender>
<cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
    MaskType="Date" TargetControlID="txtEndingDate" PromptCharacter="_">
</cc4:MaskedEditExtender>
<%--</ContentTemplate> </asp:UpdatePanel>--%>