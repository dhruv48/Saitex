<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricLedgerQuery1.ascx.cs" Inherits="Module_Fabric_FabricSaleWork_Controls_FabricLedgerQuery1" %>

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
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="td" colspan="8">
            <table>
                <tr>
                    <%--<td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                    </td>--%>
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
            <asp:UpdatePanel ID="UpdatePanel1114" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td align="center" class="TableHeader td" colspan="8">
                                <span class="titleheading"><strong>FABRIC LEDGER </strong></span>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Branch:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged">
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
                            <td class="tdRight">
                                Fabric:
                            </td>
                            <td class="tdLeft">
                                <cc2:ComboBox ID="ddlFabric" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="FABR_CODE" DataValueField="FABR_CODE" EnableLoadOnDemand="true"
                                    MenuWidth="660" OnLoadingItems="Item_LOV_LoadingItems" 
                                    EnableVirtualScrolling="true" OpenOnFocus="true" 
                TabIndex="9" Visible="true"
                                    Height="200px" EmptyText="---------All--------">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            FABRIC CODE</div>
                                        <div class="header c5">
                                            FABRIC DESCRIPTION</div>
                                        <div class="header c6">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("FABR_CODE") %></div>
                                        <div class="item c5">
                                            <%# Eval("FABR_DESC") %></div>
                                        <div class="item c6">
                                            <%# Eval("FABR_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <%--<td class="tdRight">
                                Fabric Category:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlYarnCate" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px" DataTextField="YARN_CAT" DataValueField="YARN_CAT">
                                </asp:DropDownList>
                            </td>--%>
                            <td class="tdRight">
                                Fabric Type:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlFabricType" runat="server" CssClass="gCtrTxt " Font-Size="9"
                                    Width="160px">
                                </asp:DropDownList>
                            </td>
                            <td align="center" colspan="2">
                                <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click1" />
                            </td>
                        </tr>
                        <tr>
                            <td class="TdBackVir" colspan="8">
                                <b>Total Records : &nbsp;&nbsp;</b><asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                              <b>
                                    <asp:UpdateProgress ID="UpdateProgress1114" runat="server">
                                        <ProgressTemplate>
                                            Loading...</ProgressTemplate>
                                    </asp:UpdateProgress>
                                </b>
                            </td>
                            <%--<td align="right" valign="top" cssclass="Label">
                              
                            </td>--%>
                        </tr>
                    </table>
                    <table width="100%">
                        <tr>
                            <td class="td tContentArial">
                                <asp:Panel ID="pnlShowHover" runat="server" Height="350px" ScrollBars="Horizontal"
                                    Width="945px">
                                    <asp:GridView ID="GridLedger" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="3" Font-Size="X-Small" 
                                        HeaderStyle-Wrap="true" OnPageIndexChanging="GridLedger_PageIndexChanging"
                                        PageSize="14" Width="250%" BackColor="White" BorderColor="#999999" 
                                        BorderStyle="None" BorderWidth="1px">
                                       <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />


                                        <Columns>
                                            <asp:BoundField DataField="BRANCH_NAME" HeaderText="BRANCH NAME" />
                                            <asp:TemplateField HeaderText="FABRIC CODE" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFabricCode" Text='<%#Eval("FABR_CODE") %>' ToolTip='<%#Eval("FABR_DESC") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
             
                                    <HeaderStyle HorizontalAlign="Left" />
             
                                </asp:TemplateField>
                                           <%-- <asp:BoundField DataField="FABR_CODE" HeaderText="FABRIC CODE" />
                                            <asp:BoundField DataField="FABR_DESC" HeaderText="FABRIC DESCRIPTION" />--%>
                                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                                            <asp:BoundField DataField="USER_NAME" HeaderText="USER NAME" />
                                            <asp:BoundField DataField="TRN_TYPE" HeaderText="TRANSACTION TYPE" />
                                            <asp:BoundField DataField="TRN_DATE" DataFormatString="{0:dd-MM-yyyy}" HeaderText="TRANSACTION DATE" />
                                            <asp:BoundField DataField="TRN_QTY" HeaderStyle-HorizontalAlign="Right" HeaderText="TRANSCTION QTY"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="ISSUE_QTY" HeaderText="ISSUE QTY" />
                                            <asp:BoundField DataField="RECEIVE_QTY" HeaderStyle-HorizontalAlign="Right" HeaderText="RECEIVE QTY"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FINAL_RATE" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Right"
                                                HeaderText="FINAL RATE" ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="DEPT_NAME" HeaderText="DEPARTMENT" />
                                            <asp:BoundField DataField="PRTY_CH_NUMB" HeaderStyle-HorizontalAlign="Right" HeaderText="PARTY CHALAN NUMBER"
                                                ItemStyle-HorizontalAlign="Right">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:BoundField>
                                            <%--<asp:BoundField DataField="PRTY_NAME" HeaderText="PARTY NAME" />--%>
                                            
                                            <asp:TemplateField HeaderText="PARTY NAME" HeaderStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFabricCode" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("DESCRIPTION") %>' runat="server"></asp:Label>
                                    </ItemTemplate>
             
                                    <HeaderStyle HorizontalAlign="Left" />
             
                                </asp:TemplateField>
                                           <%-- <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" />--%>
                                            <asp:BoundField DataField="COMP_ADD" HeaderText="COMPANY ADDRESS" />
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
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtFromDate">
                    </cc1:CalendarExtender>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" PopupPosition="TopLeft"
                        TargetControlID="TxtToDate">
                    </cc1:CalendarExtender>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
