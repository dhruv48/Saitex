<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Customer_Request_Lab_Dip_Query_Form.ascx.cs" Inherits="Module_OrderDevelopment_LabDip_Controls_Customer_Request_Lab_Dip_Query_Form" %>
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
        width: 150px;
    }
    .c3
    {
        margin-left: 4px;
        width: 150px;
    }
    .c4
    {
         margin-left: 4px;
        width: 300px;
    }
    .c5
    {
         margin-left: 4px;
        width: 376px;
    }
    .c6
    {
        margin-left: 4px;
        width: 100px;
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
        width: 100px;
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
   
    .d2
    {
        margin-left: 4px;
        width: 200px;
    }
    .d3
    {
        width: 100px;
    }
    .d4
    {
          margin-left: 4px;
        width: 400px;
    }
</style>
 
 <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
        <table align="left" class=" td tContentArial" width="100%">
            <tr>
                <td class="td" colspan="8">
                    <table>
                        <tr>
                            <td>
                                <%--<asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" Enabled="false"></asp:ImageButton>--%>
                            </td>
                             <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" TabIndex="7" ></asp:ImageButton>&nbsp;</td> 
                    
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" TabIndex="8"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" TabIndex="9" />
                            </td>
                            <td>
                                <%--<asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Width="48" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" colspan="8">
                    <span class="titleheading"><strong>Customer Request Lab Dip Query Form</strong></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Order Type:
                </td>
                <td>
                    <asp:DropDownList ID="ddlBusinessType"  runat="server" AutoPostBack="true" 
                                    CssClass="gCtrTxt" Width="150px" TabIndex="1" 
                        onselectedindexchanged="ddlBusinessType_SelectedIndexChanged">
                        <asp:ListItem Text=""  > </asp:ListItem>
                                <asp:ListItem  Value="SW" > Sale Work</asp:ListItem>
                                <asp:ListItem  Value="JW" > Job Work</asp:ListItem>
                                <asp:ListItem  Value="ES" > Export Sales</asp:ListItem>
                                </asp:DropDownList>
                                
                </td>
                <td class="tdRight">
                    Year:
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                        Font-Size="8"  Width="160px" TabIndex="2">
                    </asp:DropDownList>
                </td>
               
            </tr>
            <tr>
                <td align="right">
                    Customer Request No:
                </td>
                <td>
                    <%--<asp:DropDownList ID="ddlCustomerRequestNo" runat="server" AutoPostBack="true"  CssClass="gCtrTxt " 
                        Width="150px" TabIndex="3" >
                    </asp:DropDownList>--%>
                    
                    
                     <cc2:ComboBox ID="ddlCustomerRequestNo" runat="server" CssClass="smallfont" Width="150px" EnableLoadOnDemand="True"
                         MenuWidth="300px" EnableVirtualScrolling="true" 
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtCustomerRequestItems"
                        EmptyText="" TabIndex="3">
                        <HeaderTemplate>
                            <div class="header d1">
                                Customer Request No</div>
                            <%--<div class="header d2">
                                YARN DESCRIPTION</div>--%>
                                <div class="header c1">
                                Year</div>
                          
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item d1">
                                <%# Eval("ORDER_NO")%></div>
                                <div class="item c1">
                                <%# Eval("YEAR")%></div>
                           <%-- <div class="item d2">
                                <%# Eval("YARN_DESC") %></div>--%>
                           
                        </ItemTemplate>
                      <%--  <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>--%>
                    </cc2:ComboBox>
                    
                    
                    
                </td>
                <td align="right">
                    Quality Code :
                </td>
                <td>
                    <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="YARN_CODE" DataValueField="YARN_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                        EmptyText="" TabIndex="4">
                        <HeaderTemplate>
                            <div class="header d3">
                                QUALITY CODE</div>
                            <div class="header d2">
                                QUALITY DESCRIPTION</div>
                          
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item d3">
                                <%# Eval("YARN_CODE")%></div>
                            <div class="item d2">
                                <%# Eval("YARN_DESC") %></div>
                           
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
                    
                    
                    Customer Name:
                    
                    
                </td>
                <td>
                    
                    
                    
                    
                    <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" EmptyText="" Width="150px"
                                    MenuWidth="350px" Height="200px" TabIndex="5" OnLoadingItems="cmbPartyCode_LoadingItems"
                                     EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header d1">
                                            NAME</div>
                                       <%-- <div class="header d2">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item d1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                      <%--  <div class="item d2">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PRTY_ADD1") %>' /></div>--%>
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
                <td style="font-size:large;">
                    <b>Total Records : 
                    <asp:Label ID="lblTotalRecord" runat="server" ></asp:Label></b>
                </td>
                <td align="right" colspan="3">
                    <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                        <ProgressTemplate>
                            <h3>
                                Loading...
                            </h3>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
                <td align="right">
                    <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click" CssClass="AButton"  TabIndex="6"/>
                </td>
                <td colspan="6">
                </td>
                 
            </tr>
            <tr>
                <td class="td tContentArial" colspan="8">
                    <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="300px" >
                        <asp:GridView ID="GridLedger" runat="server" AutoGenerateColumns="False" HeaderStyle-Font-Bold="true"
                            AllowPaging="true" PageSize="14" CellPadding="3" ForeColor="#333333" GridLines="Both"
                            BorderStyle="Ridge" Font-Names="Arial" Font-Size="X-Small"  Width="150%"  OnPageIndexChanging="Grid1_PageIndexChanging"  >
                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                            <asp:BoundField DataField="YEAR" HeaderText="YEAR" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ORDER_NO" HeaderText="CUST. REQ. No." ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="ORDER_DATE" HeaderText="CUST. REQ. DATE." ItemStyle-HorizontalAlign="Center"/>
                            <asp:BoundField DataField="BUSINESS_TYPE" HeaderText="ORDER TYPE" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="PRTY_NAME" HeaderText="CUSTOMER NAME"  />
                            <asp:BoundField DataField="ORDER_REFF_NO" HeaderText="ARTICLE NO" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="YARN_CODE" HeaderText="QUALITY CODE"  ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField DataField="YARN_DESC" HeaderText="QUALITY DESCRIPTION" HeaderStyle-HorizontalAlign="Right"
                                    ItemStyle-HorizontalAlign="Right" />
                                <asp:BoundField DataField="SHADE_CODE" HeaderText="PARTY DESCRIPTION" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="LIGHT_SOURCE" HeaderText="1ST LIGHT SOURCE" ItemStyle-HorizontalAlign="Center" /> 
                                <asp:BoundField DataField="LIGHT_SOURCE1" HeaderText="2ND LIGHT SOURCE " ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="NO_OF_OPTIONS" HeaderText="OPTIONS" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                <asp:BoundField DataField="AGENT" HeaderText="AGENT" />
                                <asp:BoundField DataField="DIRECT_BILLING" HeaderText="DIRECT BILLING" />
                                <asp:BoundField DataField="TREMARKS" HeaderText="YARN REMARKS" />
                              
                                
                            </Columns>
                           <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                   
                </td>
            </tr>
        </table>
       </ContentTemplate><Triggers>
       <asp:PostBackTrigger ControlID="imgBtnExportExcel" />
       </Triggers>
        </asp:UpdatePanel>