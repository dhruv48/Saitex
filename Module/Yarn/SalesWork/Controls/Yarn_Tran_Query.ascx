<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Yarn_Tran_Query.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Yarn_Tran_Query" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; overflow:hidden;white-space:nowrap;
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

<table align="left" class="  tContentArial" width="945px">
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
            <table width="945px">
                <tr>
                    <td align="center" class="TableHeader td" colspan="13">
                        <span class="titleheading"><strong>Yarn Transaction Query</strong></span>
                    </td>
                </tr>
            </table>
            <asp:UpdatePanel ID="UpdatePanel1113" runat="server">
                <ContentTemplate>
                    <table width ="945px">
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
                            
                            <td class="tdLeft" colspan="2">
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
                            </td>
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
                            
                                                 <td class="tdRight">
            Yarn&nbsp;Shade:
        </td>
        <td td class="tdLeft" colspan="2">
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
                                Location:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddllocation" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                           
                            
                        </tr>
                        <tr>
                        <td align="right">
                                Store:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlstore" runat="server" AutoPostBack="True" CssClass="gCtrTxt" Font-Size="8"
                                    Width="160px">
                                </asp:DropDownList>
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
                        <td>
                                                            <asp:Button ID="btnGetReport" runat="server" Text="Get Data" 
                                    OnClick="btnGetReport_Click1" CssClass="AButton"/>
                            
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
                    <table width="945px">
                        <tr>
                            <td class="td tContentArial">
                                <asp:Panel ID="pnl110" runat="server" ScrollBars="Both" Height="300px" Width="945px">
                                    <asp:GridView ID="grid_trn_Query" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="3" ShowFooter="true"  BorderStyle="Ridge" CssClass="smallfont"
                                        Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" PageSize="12"
                                        Width="200%" OnPageIndexChanging="grid_trn_Query_PageIndexChanging" 
                                        onrowdatabound="grid_trn_Query_RowDataBound" >
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
                                            <asp:BoundField DataField="YARN_CODE" HeaderText="YARN CODE" />
                                            <asp:BoundField DataField="YARN_DESC" HeaderText="YARN DESC" />
                                            <asp:BoundField DataField="LOCATION" HeaderText="LOCATION" />
                                            <asp:BoundField DataField="STORE" HeaderText="STORE" />
                                            <asp:BoundField DataField="SHADE_FAMILY" HeaderText="SHADE FAMILY" />
                                            <asp:BoundField DataField="SHADE_CODE" HeaderText="SHADE CODE" />
                                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                           <%--<asp:BoundField DataField="TRN_QTY" HeaderText="TRN QTY" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />                                                
                                            </asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="TRN QTY">                                            
                                           <ItemTemplate>
                                            <asp:Label ID="lblTrnQty" runat="server" ToolTip='<%# Bind("TRN_QTY") %>'  Text='<%#Bind("TRN_QTY")%>' ></asp:Label>
                                             </ItemTemplate>
                                             <FooterTemplate>
                                        <asp:Label ID="lblTotalQty" runat="server" Text="Total&nbsp;Qty=" Font-Bold="true" >
                                        </asp:Label>&nbsp;<asp:Label ID="lblTotal" runat="server" Font-Bold="true"></asp:Label>                                     
                                       </FooterTemplate>
                                          
                                        </asp:TemplateField>
                                                                                     
                                            
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
                                            <asp:BoundField DataField="NO_OF_UNIT" HeaderText="NO OF COPS" HeaderStyle-HorizontalAlign="Right"
                                                ItemStyle-HorizontalAlign="Right">
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
                                            <asp:BoundField DataField="TO_DEPT_NAME" HeaderText="TO DEPARTMENT" />
                                            <asp:BoundField DataField="COST_CENTER_CODE" HeaderText="COST CENTER CODE" />
                                            <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                            <asp:BoundField DataField="PI_NO" HeaderText="TEX.LOT NO." HeaderStyle-HorizontalAlign="Right"
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
