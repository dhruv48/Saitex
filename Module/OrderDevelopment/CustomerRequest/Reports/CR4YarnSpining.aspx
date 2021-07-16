<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CR4YarnSpining.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Reports_CR4YarnSpining" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
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
        width: 200px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
<table class="tContentArial">
<tr>
<td>
<table align="left">
                <tbody>
                    <tr>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                        </td>
                        <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                        </td>
                    </tr>
                </tbody>
            </table>
            </td>
            </tr>
            <tr>
           
             <td class="TableHeader" class="td" align="center" width="100%">
            <b class="titleheading">Print Sales Contract Report</b>
           </td>
            </tr>
            <tr>
            <td>
          <fieldset>
            <table width="100%">
                <tr>
                    <td align="right" style="width: 12%;">
                        Branch :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" TabIndex="1"
                            Width="150px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Party :
                    </td>
                    <td align="left" style="width: 12%;">
                        <cc2:combobox ID="txtPartyCode" runat="server"  EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" 
                            EmptyText="Select Party" EnableVirtualScrolling="true"
                            Width="150px" MenuWidth="350px" Height="200px" 
                            OnLoadingItems="txtPartyCode_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:combobox>
                    </td>
                    <td align="right" style="width: 12%;">
                        Article :
                    </td>
                    <td align="left" style="width: 12%;">
                        <cc2:combobox ID="ddlArticle" runat="server"  CssClass="smallfont"
                            EnableLoadOnDemand="True" DataTextField="YARN_CODE" DataValueField="YARN_DESC"
                            EmptyText="Select Article" MenuWidth="400px" EnableVirtualScrolling="true" OpenOnFocus="true"
                            TabIndex="11" Visible="true" Height="200px" Width="150px" 
                            OnLoadingItems="ddlArticle_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Article Code</div>
                                <div class="header c2">
                                    Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("YARN_CODE") %></div>
                                <div class="item c2">
                                    <%# Eval("YARN_DESC") %></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:combobox>
                    </td>
                   <td align="right" style="width: 12%;">
            Product&nbsp;Type:
            </td>
            <td align="left" style="width: 12%;">
            <asp:DropDownList runat="server" ID="ddlProductType"  Width="150px">
            </asp:DropDownList>
            </td> 
                </tr>
                <tr>
                    <td align="right" style="width: 12%;">
                        Shade&nbsp;Code :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtshadecode" runat="server" Width="150px" CssClass="SmallFont UpperCase" Text="WHITE" ></asp:TextBox>
                    </td>
                    <td align="right" style="width: 12%;">
                        From&nbsp;Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="150px" CssClass="SmallFont"></asp:TextBox>
                        <cc1:calendarextender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:calendarextender>
                    </td>
                    <td align="right" style="width: 12%;">
                        To&nbsp;Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="150px" CssClass="SmallFont"
                            AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
                        <cc1:calendarextender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:calendarextender>
                    </td>
                    <td align="right" style="width: 12%;">
                        Status :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont" TabIndex="8"
                            Width="150px">
                            <asp:ListItem Text="------ALL------" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="UNCONFIRMED" Value="0"></asp:ListItem>
                            <asp:ListItem Text="CONFIRMED" Value="1"></asp:ListItem>
                            <asp:ListItem Text="CLOSED" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 4%;">
                        
                    </td>
                </tr>
                 <tr>
                   
                     <td align="right" style="width: 12%;">
                        SO No :
                    </td>
                     
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCustNo" runat="server" TabIndex="4" Width="150px" CssClass="SmallFont UpperCase"></asp:TextBox>
                    </td>
                   <td align="left" >
                      Agent
                    </td>
            <td>
            
             <cc2:ComboBox ID="cmbAgent" runat="server"  EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" EmptyText="Select Agent" EnableVirtualScrolling="true"
                            Width="150px" MenuWidth="350px" Height="200px" OnLoadingItems="cmbAgent_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
            </td>
                   
                    <td align="center" style="width: 4%;">
                    
                    </td>
                    <td colspan="4"></td>
                </tr>
            </table>
        </fieldset>
            </td>
            </tr>
  </table>
</asp:Content>

