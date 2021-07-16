<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TDS.ascx.cs" Inherits="Module_FA_Controls_TDS" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
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
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        margin-left: 4px;
        width: 250px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table align="left" class="tContentArial">
            <tr>
                <td align="center" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="New" ImageUrl="~/CommonImages/save.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                    Width="48" Height="41" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    Width="48" Height="41" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    Width="48" Height="41" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="td TableHeader" width="100%">
                    <asp:Label ID="lblFormHeading" CssClass="titleheading" runat="server" Text="TDS Party Integration"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                </td>
            </tr>
            <tr>
                <td class="td" align="center">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td align="right">
                                Party Code:
                            </td>
                            <td align="left">
                                <cc2:ComboBox ID="ddlAccountCode" runat="server" EnableLoadOnDemand="true" DataTextField="LDGR_NAME"
                                    DataValueField="LDGR_CODE" Height="200px" CssClass="SmallFont" EmptyText="Select Ledger"
                                    MenuWidth="400px" Width="200px" OnLoadingItems="ddlAccountCode_LoadingItems"
                                    AutoPostBack="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c4">
                                            Ledger Name</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LDGR_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("LDGR_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Contract Code:
                            </td>
                            <td align="left">
                                <cc2:ComboBox ID="ddlContractCode" runat="server" EnableLoadOnDemand="true" DataTextField="CONTRACT_CODE"
                                    DataValueField="CONTRACT_CODE" Height="200px" CssClass="SmallFont" EmptyText="Select Contract Code"
                                    MenuWidth="600px" Width="200px" OnLoadingItems="ddlContractCode_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Contract Code</div>
                                        <div class="header c3">
                                            Description</div>
                                        <div class="header c1">
                                            Section</div>
                                        <div class="header c1">
                                            Start Date</div>
                                        <div class="header c1">
                                            End Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CONTRACT_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("CONTRACT_DESC") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("SECTION") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("START_DATE", "{0:dd-MM-yyyy}") %>' /></div>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("END_DATE", "{0:dd-MM-yyyy}") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                                <cc3:Grid ID="grdTDS" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                    PageSize="5" AutoGenerateColumns="False" OnSelect="grdTDS_Select">
                                    <Columns>
                                        <cc3:Column DataField="LDGR_CODE" Align="Left" HeaderText="Code" Width="70px" Visible="false">
                                        </cc3:Column>
                                        <cc3:Column DataField="LDGR_NAME" Align="Left" HeaderText="Ledger Name" Width="240px">
                                        </cc3:Column>
                                        <cc3:Column DataField="CONTRACT_CODE" Align="Center" HeaderText="Contract Code" Width="120px">
                                        </cc3:Column>
                                        <cc3:Column DataField="TDATE" Align="Left" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}"
                                            Width="120px">
                                        </cc3:Column>
                                    </Columns>
                                </cc3:Grid>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
