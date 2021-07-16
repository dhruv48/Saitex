<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RateComponent.ascx.cs"
    Inherits="Module_Inventory_Controls_RateComponent" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc3" %>
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
        width: 100px;
    }
    .c2
    {
        margin-left: 4px;
        width: 60px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
<table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
    <tr>
        <td class="td tdLeft">
            <table cellpadding="0" cellspacing="0" border="0" align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" style="width: 6px">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td>
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
        <td class="td tdCenter TableHeader">
            <span class="titleheading">Rate Component</span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <table>
                <tr>
                    <td align="right" valign="top">
                        Code :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtCompoCode" runat="server" MaxLength="25" TabIndex="1" CssClass="UpperCase TextBox"
                            Width="128px"></asp:TextBox>
                        <cc3:ComboBox ID="cmbFind" runat="server" AutoPostBack="True" DataTextField="COMPO_CODE"
                            EmptyText="Search Component" EnableLoadOnDemand="True" OnLoadingItems="cmbFind_LoadingItems"
                            OnSelectedIndexChanged="cmbFind_SelectedIndexChanged" Width="150px" MenuWidth="350px"
                            Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    Sequence</div>
                                <div class="header c3">
                                    Type</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("COMPO_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("COMPO_SL") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("COMPO_TYPE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc3:ComboBox>
                        <asp:RequiredFieldValidator ID="rfvCode" runat="server" ControlToValidate="txtCompoCode"
                            Display="Dynamic" ErrorMessage="Code Required" SetFocusOnError="True" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Sequence :
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtCompoSL" runat="server" MaxLength="2" TabIndex="3" CssClass="TextBoxNo SmallFont"
                            Width="128px"></asp:TextBox>
                        <asp:RangeValidator ID="rvSL" runat="server" ControlToValidate="txtCompoSL" Display="Dynamic"
                            ErrorMessage="Numeric field required." MaximumValue="99" MinimumValue="1" SetFocusOnError="True"
                            Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Type :
                    </td>
                    <td align="left" valign="top">
                        <cc1:OboutDropDownList ID="ddlCompoType" runat="server" TabIndex="4" CssClass="SmallFont"
                            Width="130px">
                            <asp:ListItem Value="A">Addition</asp:ListItem>
                            <asp:ListItem Value="D">Deduction</asp:ListItem>
                        </cc1:OboutDropDownList>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="tdLeft ">
            <table class="td" width="100%">
                <tr>
                    <td>
                        <cc2:Grid ID="grdRateCompo" runat="server" AllowAddingRecords="False" AllowRecordSelection="true"
                            AllowMultiRecordSelection="false" AutoGenerateColumns="False" FolderStyle="~/StyleSheet/black_glass"
                            OnSelect="grdRateCompo_Select" OnRebind="grdRateCompo_Rebind">
                            <Columns>
                                <cc2:Column DataField="COMPO_CODE" HeaderText="Code" Width="100px">
                                </cc2:Column>
                                <cc2:Column DataField="COMPO_SL" HeaderText="Sequence" Width="100px">
                                </cc2:Column>
                                <cc2:Column DataField="COMPO_TYPE" HeaderText="Type" Width="120px">
                                </cc2:Column>
                            </Columns>
                        </cc2:Grid>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--   </ContentTemplate>
</asp:UpdatePanel>
--%>