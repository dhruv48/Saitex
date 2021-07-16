<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SalesOrderMachine.ascx.cs" Inherits="Module_OrderDevelopment_Controls_SalesOrderMachine" %>

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
    
    .c3
    {
        margin-left: 4px;
        width: 300px;
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
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td>
                    <table align="left">
                        <tr>
                            <td id="tdPrint" runat="server" visible="false" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" ></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr width="100%">
                <td align="center" class="TableHeader td">
                    <b class="titleheading">Sale Order For Production Machine</b>
                </td>
            </tr>
        </table>
        <fieldset>
            <table width="100%">
                <tr>
                    <td align="right" style="width: 12%;">
                        Branch :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlBranch" runat="server" CssClass="SmallFont" TabIndex="1"
                            Width="150px" Enabled="false">
                        </asp:DropDownList>
                    </td>
                    <td align="right" style="width: 12%;">
                        Party :
                    </td>
                    <td align="left" style="width: 12%;">
                        <cc2:ComboBox ID="txtPartyCode" runat="server"  EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME"  EnableVirtualScrolling="true"
                            Width="150px" MenuWidth="350px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
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
                    <td align="right" style="width: 12%;">
                        Quality :
                    </td>
                    <td align="left" style="width: 12%;">
                        <cc2:ComboBox ID="ddlArticle" runat="server"  CssClass="smallfont"
                            EnableLoadOnDemand="True" DataTextField="YARN_CODE" DataValueField="YARN_DESC"
                             MenuWidth="700px" EnableVirtualScrolling="true" OpenOnFocus="true"
                            TabIndex="11" Visible="true" Height="200px" Width="150px" OnLoadingItems="ddlArticle_LoadingItems">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Article Code</div>
                                     <div class="header c2">
                                    Display Name</div>
                                <div class="header c3">
                                   Technical Description</div>
                                    
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("YARN_CODE") %></div>
                                    <div class="item c2">
                                    <%# Eval("ASS_YARN_DESC")%></div>
                                <div class="item c3">
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
                   <td align="right" style="width: 12%;">
            Business Type:
            </td>
            <td align="left" style="width: 12%;">
            <asp:DropDownList runat="server" ID="ddlBusinessType"  Width="150px">
            </asp:DropDownList>
            </td> 
                </tr>
                <tr>
                    <td align="right" style="width: 12%;">
                        Shade Code :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtshadecode" runat="server" Width="150px" CssClass="SmallFont UpperCase" Visible="false" ></asp:TextBox>
                         <cc2:ComboBox ID="ddlshadecode" runat="server"  CssClass="smallfont"
                            EnableLoadOnDemand="True" DataTextField="SHADE_CODE" DataValueField="SHADE_FAMILY_CODE"
                             MenuWidth="250px" EnableVirtualScrolling="true" OpenOnFocus="true"
                            TabIndex="11" Visible="true" Height="200px" Width="150px" AutoPostBack="true"
                            OnLoadingItems="ddlShade_LoadingItems" onselectedindexchanged="ddlshadecode_SelectedIndexChanged1" 
                             >
                            <HeaderTemplate>
                                <div class="header c1">
                                    Shade Code</div>
                                     <div class="header c1">
                                    Shade Family</div>
                                
                                    
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("SHADE_CODE") %></div>
                                    <div class="item c1">
                                    <%# Eval("SHADE_FAMILY_CODE")%></div>
                                
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                    </td>
                    <td align="right" style="width: 12%;">
                        From Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="140px" CssClass="SmallFont"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 12%;">
                        To Date:
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="140px" CssClass="SmallFont"
                            AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>
                    </td>
                    <td align="right" style="width: 12%;">
                        Shade Group :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="SmallFont" TabIndex="8"
                            Width="150px" Visible="false">
                            <asp:ListItem Text="------ALL------" Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="UNCONFIRMED" Value="0"></asp:ListItem>
                            <asp:ListItem Text="CONFIRMED" Value="1"></asp:ListItem>
                            <asp:ListItem Text="CLOSED" Value="2"></asp:ListItem>
                        </asp:DropDownList>
                         <asp:DropDownList ID="ddlShadeGroup" runat="server" CssClass="SmallFont" TabIndex="8"
                            Width="150px" >
                            <asp:ListItem Text="------ALL------" Value="" Selected="True"></asp:ListItem>
                           <asp:ListItem Text="BROWN">BROWN</asp:ListItem>
                           <asp:ListItem Text="YELLOW">YELLOW</asp:ListItem>  
                           <asp:ListItem Text="GREY">GREY</asp:ListItem>
                           <asp:ListItem Text="RED">RED</asp:ListItem>
                           <asp:ListItem Text="BLUE">BLUE</asp:ListItem>
                           <asp:ListItem Text="RANI">RANI</asp:ListItem>
                           <asp:ListItem Text="ORANGE">ORANGE</asp:ListItem>
                           <asp:ListItem Text="BLACK">BLACK</asp:ListItem>
                           <asp:ListItem Text="WHITE">WHITE</asp:ListItem>
                           <asp:ListItem Text="WHITE">SKY BLUE</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 4%;">
                        
                    </td>
                </tr>
                 <tr>
                    <%--<td align="left"  colspan="4">
                      <asp:Panel ID="Panel1" runat="server" >
                            <asp:RadioButtonList ID="redForQuery" runat="server" Height="16px"  
                                RepeatDirection="Horizontal"  Font-Size="Small">
                                <asp:ListItem Selected="True" Value="Order" Text="Order Wise" >Sale Order Report Order Wise</asp:ListItem>
                                <asp:ListItem  Value="Party" Text="Party Wise">Sale Order Report Party Wise</asp:ListItem>
                                <asp:ListItem  Value="Yarn" Text="Yarn Wise">Sale Order Report Yarn Wise</asp:ListItem>
                                
                            </asp:RadioButtonList>
                        </asp:Panel>
                    </td>--%>
                     <td align="right" style="width: 12%;">
                        Order No :
                    </td>
                    <td align="left" style="width: 12%;">
                        <asp:TextBox ID="txtCustNo" runat="server" TabIndex="4" Width="150px" CssClass="SmallFont UpperCase" Visible="false"></asp:TextBox>
                         <cc2:ComboBox ID="cmdCustomer" runat="server"  CssClass="smallfont"
                            EnableLoadOnDemand="True" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                             MenuWidth="200px" 
                            EnableVirtualScrolling="true" OpenOnFocus="true" AutoPostBack="true"
                            TabIndex="11" Visible="true" Height="200px" Width="150px" 
                            OnLoadingItems="ddlORDER_LoadingItems" 
                            onselectedindexchanged="cmdCustomer_SelectedIndexChanged"  >
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order No</div>
                                </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("ORDER_NO") %></div>
                                    
                                
                                    
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                   
            <td align="right" style="width: 12%;">Machine No</td>
            <td align="left" style="width: 12%;">
                                    <cc2:ComboBox ID="ddlMachine" runat="server"  CssClass="SmallFont"
                                    EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="150px" OnLoadingItems="ddlMacCode_LoadingItems" 
                                    Width="150px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Mac Code</div>
                                       
                                       
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        
                                       
                                    </ItemTemplate>
                                   <%-- <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>--%>
                                </cc2:ComboBox>
            </td>
                   
                    
                    <td align="right" style="width: 12%;"> Shade Nature:</td>
                    <td align="left" style="width: 12%;">
                    <asp:DropDownList ID="ddlNatureShade" runat="server" AppendDataBoundItems="True"
                                         CssClass="SmallFont "   Width="150px">
                                          <asp:ListItem ></asp:ListItem>
                                         <asp:ListItem Value="Light">Light</asp:ListItem>
                                         <asp:ListItem Value="Medium">Medium</asp:ListItem>
                                           <asp:ListItem Value="Dark">Dark</asp:ListItem>
                                        </asp:DropDownList></td>
                    <td align="center" style="width: 4%;">
                     <asp:Button ID="btnShow" runat="Server" Text="Get Records" OnClick="btnShow_Click" />
                    </td>
                </tr>
            </table>
        </fieldset>
        <table width="100%">
            <tr>
                <td align="left" width="50%">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
                <td align="left" valign="top" width="50%" cssclass="Label">
                    <b>
                        <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                            <ProgressTemplate>
                                Loading...</ProgressTemplate>
                        </asp:UpdateProgress>
                    </b>
                </td>
            </tr>
        </table>
        <table width="100%">
            <tr>
                <td align="left" class="td" width="100%">
                    <div id="divPrint" runat="server">
                        <asp:GridView ID="grdCustomerRequest" runat="server" AutoGenerateColumns="False"
                            AllowPaging="True" PageSize="20" BorderStyle="Ridge" AllowSorting="True" CellPadding="3"
                            CssClass="smallfont" EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" 
                            PagerStyle-HorizontalAlign="Left" Width="100%" 
                            OnPageIndexChanging="grdCustomerRequest_PageIndexChanging" ShowFooter="true"  >
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                              
                              
                                 <asp:BoundField DataField="SHADE_CODE" HeaderText="Shade" />
                                 <asp:BoundField DataField="SHADE_FAMILY" HeaderText="Shade Family"  />
                                 <asp:BoundField DataField="SHADE_NATURE" HeaderText="Shade Nature" Visible="true" />
                                 <asp:BoundField DataField="SHADE_GROUP" HeaderText="Shade Group" Visible="true" />
                                 <asp:BoundField DataField="ASS_YARN_DESC" HeaderText="Quality Display Name" Visible="false"/>
                                 <asp:BoundField DataField="YARN_DESC" HeaderText="Quality Desc" />  
                                 <asp:BoundField DataField="YARN_CODE" HeaderText="Quality No"  Visible="false"/>
                                 <asp:BoundField DataField="ORDER_NO" HeaderText="Sale Order No" />
                                 <asp:TemplateField HeaderText="Order Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblORDER_DATE" runat="server" HtmlEncode="false" Text='<%# Bind("ORDER_DATE", "{0:dd-MM-yyyy}") %>'
                                            CssClass="Label smallfont"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Final Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinalDate" runat="server" HtmlEncode="false" Text='<%# Bind("REQ_DATE", "{0:dd-MM-yyyy}") %>'
                                            CssClass="Label smallfont"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Wrap="true" VerticalAlign="Top" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Vendor" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="LblVendor" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>                                                                
                                </asp:TemplateField>
                                      <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUOM" Text='<%#Eval("CR_UNIT") %>' 
                                            runat="server"></asp:Label>
                                    </ItemTemplate> 
                                    <FooterTemplate>
                                    <%--<asp:Label id="lblTotal" runat="server" Text="Total:" ></asp:Label>--%>
                                    </FooterTemplate>                                                               
                                </asp:TemplateField>    
                                   <asp:TemplateField HeaderText="Order Qty" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSoQty" Text='<%#Eval("QUANTITY") %>' 
                                            runat="server"></asp:Label>
                                    </ItemTemplate>       
                                     <%-- <FooterTemplate>
                                    <asp:Label id="lblTotalSoQty" runat="server"  ></asp:Label>
                                    </FooterTemplate>   --%>                                                        
                                </asp:TemplateField>        
                                
                                
                                <asp:BoundField DataField="MACHINE" HeaderText="Machine" Visible="true" />
                                <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="Machine capacity" Visible="true" />
                                 <asp:BoundField DataField="BATCH" HeaderText="Batches" Visible="true" />
                                 
                                <asp:BoundField DataField="CONF_TYPE" HeaderText="Status"  Visible="false"/>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </div>
                </td>
            </tr>
        </table>
    <%--</ContentTemplate>
</asp:UpdatePanel>--%>