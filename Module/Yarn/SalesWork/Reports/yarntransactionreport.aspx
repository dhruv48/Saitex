<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="yarntransactionreport.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_yarntransactionreport"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
    <%@ register assembly="obout_ComboBox" namespace="Obout.ComboBox" tagprefix="cc2" %>
    <style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;
        }
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 130px;
    }
    .c2
    {
        margin-left: 4px;
        width: 500px;
    }
    .c3
    {
        margin-left: 4px;
        width: 250px;
    }
    .c4
    {
        margin-left: 4px;
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 100px;
    }
    .style1
    {
        height: 207px;
    }
    .style3
    {
        border: .05em ridge #C1D3FB;
        height: 20px;
    }
</style>

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
        width: 200px;
    }
    .c2
    {
        margin-left: 4px;
        width: 300px;
    }
    .c3
    {
        width: 200px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 120px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 120px;
    }
</style>
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
        width: 150px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
        width: 150px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<%--<asp:UpdatePanel ID="uppnl" runat="server">--%>
    <ContentTemplate>
    <table align="left" class=" td tContentArial" width="945px">
        <tr>
            <td class="td" colspan="8">
                <table>
                    <tr>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                ToolTip="Clear" OnClick="imgbtnClear_Click1" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click1" />
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                ToolTip="Help" Width="48" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center" class="TableHeader td" colspan="8">
                <span class="titleheading"><strong>YARN TRANSACTION REPORT</strong></span>
            </td>
        </tr>
        <tr>
            <td align="right">
                Branch:
            </td>
            <td>
                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td align="right">
                Year:
            </td>
            <td>
                <%--<asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " 
                Font-Size="9" Width="160px">--%>
                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                    Width="160px" AutoPostBack="True" 
                    onselectedindexchanged="ddlYear_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td align="right">
                From Date:
            </td>
            <td>
                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                    OnTextChanged="TxtFromDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td align="right">
                To Date:
            </td>
            <td>
                <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                    OnTextChanged="TxtToDate_TextChanged" Width="150px" AutoPostBack="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                Trn Type:
            </td>
            <td>
                <asp:DropDownList ID="ddlTrnType" runat="server" DataTextField="TRN_TYPE" DataValueField="TRN_TYPE"
                    Width="160px" CssClass="gCtrTxt " Font-Size="8">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                Yarn Category:
            </td>
            <td class="tdLeft">
                <asp:DropDownList ID="ddlYarnCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px" DataTextField="YARN_CAT" DataValueField="YARN_CAT">
                </asp:DropDownList>
            </td>
            <td class="tdRight">
                Yarn Type:
            </td>
            <td class="tdLeft">
                <asp:DropDownList ID="ddlYarnType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                    Width="160px">
                </asp:DropDownList>
            </td>
            <td align="right">
                Party :
            </td>
            <td>
                <asp:DropDownList ID="ddlPartycode" runat="server" DataTextField="PRTY_CODE" DataValueField="PRTY_CODE"
                    Width="160px" CssClass="gCtrTxt" Font-Size="8">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right">
                Department:
            </td>
            <td>
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
                <td class="tdRight">
                    Yarn:
                </td>
                <td class="tdLeft">
                    
  <cc2:combobox ID="ddlYarn" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="YARN_CODE" DataValueField="YARN_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems" 
                                    EnableVirtualScrolling="true" OpenOnFocus="true" 
                TabIndex="9" Visible="true"
                                    Height="200px" EmptyText="---------All--------">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            YARN CODE</div>
                                        <div class="header c5">
                                            YARN DESCRIPTION</div>
                                        <div class="header c6">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("YARN_CODE") %></div>
                                        <div class="item c5">
                                            <%# Eval("YARN_DESC") %></div>
                                        <div class="item c6">
                                            <%# Eval("YARN_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:combobox>
         
                </td>
            </td>
            <td align="right">
                Location:
            </td>
            <td>
                <asp:DropDownList ID="ddllocation" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
                <td align="right">
                Store:
            </td>
            <td>
                <asp:DropDownList ID="ddlstore" runat="server" CssClass="gCtrTxt " Font-Size="8"
                    Width="160px">
                </asp:DropDownList>
            <td>
            </td>
            <td align="left" colspan="2">
                <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click1" />
            </td>
        </tr>
        <tr>
                             <td align="right">
            Yarn&nbsp;Shade:
        </td>
        <td>
           <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_NAME" EnableLoadOnDemand="True"
                                                MenuWidth="300" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="150px" OnLoadingItems="cmbShade_LoadingItems"
                                                >
                                                <HeaderTemplate>                                                  
                                                    <div class="header d2">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header d4">
                                                        Shade Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item d2">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item d4">
                                                        <%# Eval("SHADE_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox> 
        </td>
        <td align="right">
                                To Department:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlToDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Lot No:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtLotNo" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    Width="150px"  ></asp:TextBox>
                            </td>
        </tr>
        <tr>
            <td class="td tContentArial" colspan="8">
                <asp:Panel ID="pnlShowHover" runat="server" Height="350px" ScrollBars="Auto" Width="945px">
                    <asp:GridView ID="GridLedger" runat="server" AutoGenerateColumns="False" Width="200%"
                        CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-Wrap="true"
                        Font-Size="X-Small" AllowPaging="false" PageSize="14" AllowSorting="True">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="TRN_DATE" HeaderText="TRN DATE" DataFormatString="{0:dd-MM-yyyy}" />
                            <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                            <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUM" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="YARN_CODE" HeaderText="YARN CODE" />
                            <asp:BoundField DataField="YARN_DESC" HeaderText="YARN DESC" />
                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                            <asp:BoundField DataField="TRN_QTY" HeaderText="TRN QTY" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FINAL_RATE" HeaderText="FINAL RATE" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FINAL_AMOUNT" HeaderText="FINAL AMOUNT" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.00}">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="PRTY_CODE" HeaderText="PARTY CODE" />
                            <asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME" />
                            <asp:BoundField DataField="PO_NUMB" HeaderText="PO NUMBER" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BILL_NUMB" HeaderText="BILL NUMBER" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LORY_NUMB" HeaderText="LORY NUMBER" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
                            <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST CENTER CODE" />
                            <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                            <asp:BoundField DataField="PI_NO" HeaderText="PI NUMBER" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td colspan="8">
                <cc1:calendarextender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                    TargetControlID="TxtFromDate">
                </cc1:calendarextender>
                <cc1:calendarextender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                    TargetControlID="TxtToDate">
                </cc1:calendarextender>
            </td>
        </tr>
    </table>
    </ContentTemplate>
    <%--</asp:UpdatePanel>--%>
</asp:Content>
