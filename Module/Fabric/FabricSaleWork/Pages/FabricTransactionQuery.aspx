<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FabricTransactionQuery.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricTransactionQuery" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

  <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
        }
        .header
        {
            margin-left: 4px;
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
        .d1
        {
            width: 150px;
        }
        .d2
        {
            margin-left: 4px;
            width: 350px;
        }
        .d3
        {
            width: 80px;
        }
    </style>
<table align="left" class=" tContentArial" width="100%">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click" />
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
            <table width="100%">
                <tr>
                    <td align="center" class="TableHeader td" colspan="13">
                        <span class="titleheading"><strong>Fabric Transaction Query</strong></span>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1113" runat="server">
                <ContentTemplate>
                    <table width ="100%">
                        <tr>
                            <td align="right">
                                Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                Year:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="gCtrTxt " Font-Size="9" 
                                    Width="160px" AutoPostBack="True" 
                                    onselectedindexchanged="ddlYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                From Date:
                            </td>
                           
                            <td colspan="2">
                                <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    Width="150px" OnTextChanged="TxtFromDate_TextChanged" AutoPostBack="True"></asp:TextBox>
                            </td>
                            <td align="right">
                                To Date:
                            </td>
                            <td>
                                <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                                    Width="150px" OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"></asp:TextBox>
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
                                Fabric Type:
                            </td>
                            
                            <td class="tdLeft" colspan="1">
                                <asp:DropDownList ID="ddlFabrType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                                 <%--< align="right">--%>
                                 <td align="right">  
                                 Party :
                                 </td>
                               
                             <td>
                                <cc2:combobox ID="ddlPartycode" runat="server" CssClass="smallfont"
                                    DataTextField="PRTY_CODE" DataValueField="PRTY_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="ddlPartycode_LoadingItems" 
                                    EnableVirtualScrolling="true" OpenOnFocus="true" 
                TabIndex="9" Visible="true"
                                    Height="200px" EmptyText="---------All--------">
                                   
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("PRTY_CODE")%></div>
                                        <div class="item c5">
                                            <%# Eval(" PRTY_NAME")%></div>
                                        </ItemTemplate>
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            PARTY CODE</div>
                                        <div class="header c5">
                                            PARTY NAME</div>
                                     
                                    </HeaderTemplate>
                                     <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:combobox>
                            </td>
                               </td>
                            <tr>
                            <td align="right">
                                Department:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="gCtrTxt " Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                Fabr:
                            </td>
                            <td class="tdLeft">
                                 <cc2:combobox ID="ddlFabr" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="FABR_CODE" DataValueField="FABR_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems1" 
                                    EnableVirtualScrolling="true" OpenOnFocus="true" 
                TabIndex="9" Visible="true"
                                    Height="200px" EmptyText="---------All--------">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            FABR CODE</div>
                                        <div class="header c5">
                                            FABR DESCRIPTION</div>
                                        <div class="header c6">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("FABR_CODE")%></div>
                                        <div class="item c5">
                                            <%# Eval(" FABR_DESC")%></div>
                                        <div class="item c6">
                                            <%# Eval("FABR_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:combobox>
                            </td>
                            <td>
                            </td>
                           
                            <td>
                                                            <asp:Button ID="btnGetReport" runat="server" Text="Get Data" 
                                    OnClick="btnGetReport_Click1" />
                            
                            </td>
                            <td align="center">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TdBackVir" colspan="9">
                                <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                          
                                <b>
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>  
                            </td>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="td tContentArial">
                                <asp:Panel ID="pnl110" runat="server" ScrollBars="Both" Height="300px" Width="100%">
                                    <asp:GridView ID="grid_trn_Query" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="3" BorderStyle="Ridge" CssClass="smallfont"
                                        Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" PageSize="12"
                                        Width="100%" OnPageIndexChanging="grid_trn_Query_PageIndexChanging">
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                                        <Columns>
                                            <asp:BoundField DataField="TRN_DATE" HeaderText="TRN DATE" DataFormatString="{0:dd-MM-yyyy}" />
                                            <asp:BoundField DataField="TRN_TYPE" HeaderText="TRN TYPE" />
                                            <asp:BoundField DataField="TRN_NUMB" HeaderText="TRN NUM" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FABR_CODE" HeaderText="FABR CODE" />
                                            <asp:BoundField DataField="FABR_DESC" HeaderText="FABR DESC" />
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
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                            VerticalAlign="Middle" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                                    TargetControlID="TxtFromDate">
                                </cc1:CalendarExtender>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                                    TargetControlID="TxtToDate">
                                </cc1:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

</asp:Content>

