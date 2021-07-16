<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RM_PLANNING.ascx.cs" Inherits="Module_Prod_plan_Controls_RM_PLANNING" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

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
        width: 80px;
    }
    .c2
    {
        margin-left: 2px;
        width: 150px;
    }
    .c3
    {
        margin-left: 2px;
        width: 300px;
    }
    .c4
    {
        margin-left: 2px;
        width: 40px;
    }
    .c5
    {
        margin-left: 2px;
        width: 60px;
    }
    .c6
    {
        margin-left: 2px;
        width: 60px;
    }
    .c7
    {
        margin-left: 2px;
        width: 100px;
    }
</style>
<script type="text/javascript">

    function openPopup() {

        window.open("../../../Module/Production/Pages/Raw_Finish_Availablilty.aspx", "_blank", "WIDTH=1200,HEIGHT=625,scrollbars=yes, menubar=no,resizable=yes,directories=no,location=no");

    }
    //<![CDATA[    
    //
</script>
<%--<asp:UpdatePanel id="UpdatePanel1" runat="server"><ContentTemplate>--%>
  <table class="tdMain" width="100%">
            <tr>
                <td class="td" width="100%">
                    <table class="tContentArial">
                        <tr>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1" Visible="false" ></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" Visible="false"></asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" Visible="false" ></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" ></asp:ImageButton>
                            </td>
                            <td> 
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" Visible="false" ></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" ></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" Visible="false"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td> 
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">RM PLANNING
                        <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont"></asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="TRN" />
                    </b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">You are in
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                        Mode</span>
                </td>
            </tr>
            <tr >
                <td width="100%" class="td">
                    <table width="100%" style="font-weight: bold">
                                         
                                               
                        <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label1" runat="server" Text="Business Type :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                           <td class="tdLeft" width="12%">
                                <asp:DropDownList ID="ddlBusinessType" runat="server"  CssClass="SmallFont BoldFont"
                                    Width="98%" TabIndex="1">
                                   
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label2" runat="server" Text="Party Name :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                          <td align="left" style="width: 12%;">
                        <cc2:ComboBox ID="txtPartyCode1" runat="server"  EnableLoadOnDemand="true"
                            DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" EmptyText="Select Party" EnableVirtualScrolling="true"
                            Width="98%" MenuWidth="350px" Height="200px" OnLoadingItems="txtPartyCode_LoadingItems">
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
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label3" runat="server" Text="Quality Name :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                             <td align="left" style="width: 12%;">
                        <cc2:ComboBox ID="ddlArticle" runat="server"  CssClass="smallfont"
                            EnableLoadOnDemand="True" DataTextField="YARN_CODE" DataValueField="YARN_DESC"
                            EmptyText="Select Article" MenuWidth="400px" EnableVirtualScrolling="true" OpenOnFocus="true"
                            TabIndex="11" Visible="true" Height="200px" Width="98%" OnLoadingItems="ddlArticle_LoadingItems">
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
                        </cc2:ComboBox>
                    </td>
                           
                        </tr>
                        
                          <tr>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label4" runat="server" Text="Shade No:" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                                
                              <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" CssClass="smallfont"
                                                DataTextField="SHADE_FAMILY_NAME" DataValueField="SHADE_CODE" EnableLoadOnDemand="True"
                                                MenuWidth="300px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="16"
                                                Height="200px" Visible="true" Width="150px" OnLoadingItems="cmbShade_LoadingItems"
                                                >
                                                <HeaderTemplate>                                                  
                                                    <div class="header c1">
                                                        Shade Family Name</div>                                                  
                                                    <div class="header c2">
                                                        Shade Code</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>                                                   
                                                    <div class="item c1">
                                                        <%# Eval("SHADE_FAMILY_NAME")%></div>                                                    
                                                    <div class="item c2">
                                                        <%# Eval("SHADE_CODE")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox> 
        
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label5" runat="server" Text="From Date :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="12%">
                             
                        <asp:TextBox ID="txtCRFrom" runat="server" TabIndex="6" Width="98%" CssClass="SmallFont"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce1" runat="server" TargetControlID="txtCRFrom" PopupPosition="TopLeft"
                            Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                   
                                    
                            </td>
                            <td class="tdRight" width="12%">
                                <asp:Label ID="Label6" runat="server" Text="To Date :" CssClass="SmallFont BoldFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="16%">
                                <asp:TextBox ID="txtCRTo" runat="server" TabIndex="7" Width="98%" CssClass="SmallFont"
                            AutoPostBack="true" OnTextChanged="txtCRTo_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="ce2" runat="server" TargetControlID="txtCRTo" Format="dd/MM/yyyy"
                            PopupPosition="TopLeft">
                        </cc1:CalendarExtender>
                            </td>
                           
                        </tr>
                        <tr>
                          <td class="tdRight" width="12%">
                             <asp:Button ID="btnShow" runat="Server" Text="Get Records" OnClick="btnShow_Click" />
                             </td>
                        </tr>
                                                  
                    </table>
                </td>
            </tr>
            
                       
             <tr>
        <td align="left" class="td" width="100%">
            <%--<asp:Panel ID="grdpanel" runat="server" ScrollBars="Horizontal">--%>
            <asp:GridView ID="grdPlaningData" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                Width="100%" AllowPaging="True" PagerStyle-CssClass="pager" PageSize="10" OnPageIndexChanging="grdPlaningData_PageIndexChanging"
                OnRowDataBound="grdPlaningData_RowDataBound1" >
                <RowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText=" Company" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblcompcode" runat="server" Text='<%#Eval("COMP_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Branch" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblbranchcode" runat="server" Text='<%#Eval("BRANCH_CODE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                                
                    <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblyear" runat="server" Text='<%#Eval("YEAR") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Order No" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ORDER_NO") %>'
                                Width="80px" ReadOnly="true" ToolTip='<%# Bind("ORDER_NO") %>'>
                            </asp:Label>
                          
                        </ItemTemplate>
                       
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Order Type">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblBUSINESS_TYPE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("BUSINESS_TYPE") %>'
                                Width="80px" ReadOnly="true">
                            </asp:Label>
                           
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Party Code" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txtPartyCode1" runat="server" Text='<%#Eval("PRTY_CODE") %>' ToolTip='<%#Eval("PRTY_CODE") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Party Name" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="txtPartyDetail1" runat="server" Text='<%#Eval("PRTY_NAME") %>' ToolTip='<%#Eval("PRTY_CODE") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="LAB_DIP_NO" Visible="false">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblLabDip" runat="server" CssClass="Label SmallFont" Text='<%# Bind("LAB_DIP_NO") %>'
                                Width="50px" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="Shade Family">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblShadeFamily" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_FAMILY_CODE") %>'
                                Width="50px" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Shade No">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblShadeCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                Width="50px" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Quality Code">
                        <ItemStyle HorizontalAlign="Center" />
                        <ItemTemplate>
                            <asp:Label ID="lblQualityCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ASS_YARN_CODE") %>'
                                Width="50px" ReadOnly="true">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Quality Name" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblQulityProduct" runat="server" Text='<%#Eval("YARN_DESC") %>' ToolTip='<%#Eval("YARN_CODE") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                    
                    
                    
                    
                     <asp:TemplateField HeaderText="Base Quality Name" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblBaseQuality" runat="server" Font-Bold="true" Text='<%#Eval("QUALITY_YARN_DESC") %>' ToolTip='<%#Eval("ARTICLE_CODE") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                    
                    <asp:TemplateField HeaderText="Quality Name" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblQulityCode" runat="server" Text='<%#Eval("ARTICLE_CODE") %>' ToolTip='<%#Eval("ARTICLE_CODE") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                   
                    
                    <asp:TemplateField HeaderText="Order Qty" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblreqqty" runat="server" Text='<%#Eval("QUANTITY") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Unit-1" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblyarnstock" runat="server" Text='<%#Eval("YARN_STOCK_QTY") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderText="Unit-3" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblyarnstockUnit3" runat="server" Text='<%#Eval("YARN_STOCK_QTY1") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                    
                    
                     <asp:TemplateField HeaderText="Total Stock" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblTotalyarnstock" runat="server" Text='<%#Eval("YARN_STOCK") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                    
                     <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                    
                   
                   <asp:TemplateField HeaderText="Order Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblSaleOrderDate" runat="server" Text='<%#Eval("SALE_ORDER_DATE") %>' />
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                   
                   
                    
                    <asp:TemplateField HeaderText="Expected Dispatch Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblDeldate" runat="server" Text='<%# Bind("DEL_DATE","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                  <%--  <asp:TemplateField HeaderText="Yarn Avl.Date" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:Label ID="lblmindays" runat="server" Text='<%# Bind("DEL_DAYS","{0:d}") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>--%>
                    <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" Visible="false">
                        <ItemTemplate>
                            <asp:TextBox ID="txtremarks" runat="server"></asp:TextBox>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                  
                  
                  <asp:TemplateField HeaderText="">
                  
                      <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewPOTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlPOTRN" runat="server"  BackColor="Beige" BorderWidth="2px">
                                <asp:GridView ID="grdPOTRN" runat="server" AutoGenerateColumns="False" Width="700px"
                                    CssClass="SmallFont" >
                                    <Columns>
                                    
                                    
                                     
                                        
                                        <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn DESC">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LOT_NO" HeaderText="Grey Lot No">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STOCK" HeaderText="Stock">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="LOCATION" HeaderText="Location">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="STORE" HeaderText="Store">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        
                                        
                                        <asp:BoundField DataField="PO_NUMB" HeaderText="Po No">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="PO_ORDER_QTY" HeaderText="Po Order Qty">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="PO_RECIVE_QTY" HeaderText="Recive Qty">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="BALANCE" HeaderText="Balance Qty">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        
                                        
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc4:HoverMenuExtender ID="hmePOTRN" runat="server" PopupPosition="Left" PopupControlID="pnlPOTRN"
                                TargetControlID="lbtnViewPOTRN">
                            </cc4:HoverMenuExtender>
                        </ItemTemplate>
                   
                   
                   </asp:TemplateField>
                   
                   
                </Columns>
                <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
            </asp:GridView>
            
            <%-- </asp:Panel>--%>
        </td>
    </tr>
            
            
            
        </table>
       
      
      
      
        
        
       <%-- </ContentTemplate></asp:UpdatePanel>--%>