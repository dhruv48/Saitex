<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TaxMaster.ascx.cs" Inherits="Module_FA_Controls_TaxMaster" %>
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
        width: 150px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table align="left" class="tContentArial">
            <tr>
                <td align="center" class="td" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="New" ImageUrl="~/CommonImages/save.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
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
                    <asp:Label ID="lblFormHeading" CssClass="titleheading" runat="server" Text="Tax Master"></asp:Label>
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
                            <td align="right" valign="top">
                                Tax Code:
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtTaxCode" runat="server" CssClass="TextBox SmallFont UpperCase"
                                    Width="148px" TabIndex="2" Rows="2" MaxLength="15"></asp:TextBox>
                                <cc2:ComboBox ID="ddlTaxCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTaxCode_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                                    Height="200px" CssClass="SmallFont" EmptyText="Find Code" OnSelectedIndexChanged="ddlTaxCode_SelectedIndexChanged"
                                    MenuWidth="400px" Width="150px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Tax Code</div>
                                        <div class="header c2">
                                            Tax Description</div>
                                        <div class="header c3">
                                            Tax Group Code</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("TAX_CODE") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("TAX_DESC") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("TAX_GRP_CODE") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="None" runat="server"
                                    ErrorMessage="* Enter Master Code" ControlToValidate="txtTaxCode" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right">
                                Tax Group Code:
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlTaxGRPCode" runat="server" AppendDataBoundItems="true" DataTextField="MST_CODE"
                                    DataValueField="MST_CODE" CssClass="SmallFont" TabIndex="3" Width="140px">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RFTaxGRPCode" runat="server" ControlToValidate="ddlTaxGRPCode"
                                    Display="None" ErrorMessage="Please Select Tax Group" InitialValue="0" SetFocusOnError="True"
                                    ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Tax Description:
                            </td>
                            <td align="left" colspan="3">
                                <asp:TextBox ID="txtTaxdescription" runat="server" CssClass="TextBox SmallFont" TabIndex="3"
                                    MaxLength="200" Height="33px" Width="420px"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
  <%--  </ContentTemplate>
</asp:UpdatePanel>
--%>