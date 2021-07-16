<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxInt.ascx.cs" Inherits="Module_FA_Controls_TaxInt" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
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
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 180px;
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
                <td align="center" class="td TableHeader" width="100%">
                    <asp:Label ID="lblFormHeading" CssClass="titleheading" runat="server" Text="Tax Integration"></asp:Label>
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
                        <tr id="trTax" runat="server">
                            <td align="right" valign="top">
                                Select Integration :
                            </td>
                            <td align="left" colspan="3">
                                <cc2:ComboBox ID="ddlTaxIntegration" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="TAX_CODE" DataValueField="BRANCH_CODE" Height="200px" CssClass="SmallFont"
                                    EmptyText="Select Integration" MenuWidth="600px" Width="200px" TabIndex="1" OnLoadingItems="ddlTaxIntegration_LoadingItems"
                                    OnSelectedIndexChanged="ddlTaxIntegration_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Company Name</div>
                                        <div class="header c3">
                                            Branch Name</div>
                                        <div class="header c3">
                                            Tax Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("COMP_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("BRANCH_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("TAX_CODE") %>' /></div>
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
                            <td align="right" valign="top">
                                Select Company :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlCompany" runat="server" AppendDataBoundItems="true" DataTextField="COMP_NAME"
                                    DataValueField="COMP_CODE" CssClass="SmallFont" TabIndex="1" Width="200px" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFCompany" runat="server" ControlToValidate="ddlCompany"
                                    Display="None" ErrorMessage="Please Select Company" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Date :
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDate" runat="server" CssClass="TextBox" Width="200px" TabIndex="2"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Select Branch :
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="true" DataTextField="BRANCH_NAME"
                                    DataValueField="BRANCH_CODE" CssClass="SmallFont" TabIndex="3" Width="200px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFBranch" runat="server" ControlToValidate="ddlBranch"
                                    Display="None" ErrorMessage="Please Select Branch" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Select Tax :
                            </td>
                            <td align="left">
                                <cc2:ComboBox ID="ddlTax" runat="server" DataTextField="TAX_CODE" DataValueField="TAX_CODE"
                                    Height="200px" CssClass="SmallFont" EmptyText="Select Tax.." MenuWidth="400px"
                                    Width="200px" TabIndex="3" OnLoadingItems="ddlTax_LoadingItems">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Tax Code</div>
                                        <div class="header c3">
                                            Description</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("TAX_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("TAX_DESC") %>' /></div>
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
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
