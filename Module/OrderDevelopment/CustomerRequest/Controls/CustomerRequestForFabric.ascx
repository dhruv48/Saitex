<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerRequestForFabric.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_CustomerRequestForFabric" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

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
        margin-left: 8px;
        width: 100px;
    }
    .c3
    {
        margin-left: 8px;
        width: 100px;
    }
     .c4
    {
        margin-left: 8px;
        width: 100px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
    .style1
    {
        width: 44px;
    }
    .style2
    {
        height: 42px;
    }
    </style>
 <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
<table align="left" class="tContentArial">
<tr>
<td>
    <tr>
        <td valign="top" align="left" class="td">
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                             ValidationGroup="M1" onclick="imgbtnSave_Click" TabIndex="22" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                             ValidationGroup="M1" onclick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" 
                            OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')" ></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" 
                            ImageUrl="~/CommonImages/link_find.png" onclick="imgbtnFind_Click" TabIndex="23"
                            ></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" 
                            ImageUrl="~/CommonImages/link_print.png" onclick="imgbtnPrint_Click" TabIndex="24"
                            ></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" 
                            ImageUrl="~/CommonImages/clear.jpg" onclick="imgbtnClear_Click" TabIndex="25"
                            ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" 
                            ImageUrl="~/CommonImages/link_exit.png" onclick="imgbtnExit_Click" TabIndex="26"
                            ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" 
                            ImageUrl="~/CommonImages/link_help.png" TabIndex="27"
                            ></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td">
            <span class="titleheading"><b>Customer Request For Fabric</b></span>
        </td>
    </tr>
    <tr>
        <td class="td" align="left" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="M1" />
            </span>
        </td>
    </tr> 
<tr> 
<td>
<table class="td">
    <tr>
               <td width="25%" align="right" class="tdRight">
                        Order No&nbsp;
                    </td>                  
                 <td width="15%" align="left">
               <asp:TextBox ID="txtOrderNo" runat="server" CssClass="TextBox TextBoxDisplay" 
                         ValidationGroup="M1" TabIndex="1"></asp:TextBox>
                 <cc2:ComboBox ID="cmbOrderNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            DataTextField="ORDER_NO" DataValueField="ORDER_NO" Width="150px" MenuWidth="550px"
                            Height="200px" CssClass="SmallFont" TabIndex="1" 
                         EmptyText="Find Order No" onloadingitems="cmbOrderNo_LoadingItems" 
                         onselectedindexchanged="cmbOrderNo_SelectedIndexChanged" >                             
                                                          
                            <HeaderTemplate>
                                <div class="header c1">
                                     Order No</div>
                                      <div class="header c1">
                                     Business Type</div>
                                      <div class="header c1">
                                     Order Type</div>
                                     <div class="header c1">
                                     Order Cat</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("ORDER_NO") %>' /></div>
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("BUSINESS_TYPE") %>' /></div>
                                    <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("ORDER_TYPE") %>' /></div>
                                  <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("ORDER_CAT") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                      </cc2:ComboBox>
                  </td>
                    <td width="15%" align="right" class="tdRight">
                        Business Type
                       </td>
                     
                    <td width="15%" align="left" class="tdLeft">
                           <cc1:OboutDropDownList ID="DDLBusinessType" runat="server" Width="150px" TabIndex="2" ></cc1:OboutDropDownList>
                 <td width="15%" align="right" class="tdRight">
                     Order Cat
                  </td>
                    <td width="15%" align="left" class="tdLeft">
                      <cc1:OboutDropDownList ID="DDLOrderCat" runat="server" Width="150px" TabIndex="3"  ></cc1:OboutDropDownList>                     
                        </td>
                </tr>
    <tr>
                    <td width="25%" align="right" class="tdRight">
                        Order Type
                    </td>                   
                 <td width="15%" align="left">
                  <cc1:OboutDropDownList ID="DDLOrderType" runat="server" Width="150px" TabIndex="4"   ></cc1:OboutDropDownList>                  
                  </td>                
                 <td width="15%" align="right" class="tdRight">
                                       Order Date
                  </td>
                    <td width="15%" align="left" class="tdLeft">
                       <asp:TextBox ID="txtDate" runat="server" ValidationGroup="M1" TabIndex="5"></asp:TextBox>
                        <cc3:CalendarExtender ID="cetTindt" TargetControlID="txtDate" runat="server">
                        </cc3:CalendarExtender>
                    </td>
                </tr>
    <tr>
              <td width="25%" align="right" class="tdRight">
                 Customer Name :
                </td>                
                 <td width="15%" align="left">
                    <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                             DataTextField="PRTY_CODE" DataValueField="PRTY_CODE"
                            EmptyText="Select Party Code" 
                            Width="150px" MenuWidth="500px" Height="200px" TabIndex="6" 
                         onloadingitems="cmbPartyCode_LoadingItems" 
                         onselectedindexchanged="cmbPartyCode_SelectedIndexChanged" 
                         ontextchanged="cmbPartyCode_TextChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    NAME</div>
                                <div class="header c3">
                                    Address</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PRTY_ADD1") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        
                  </td>
                 <td width="15%" align="right" class="tdRight">
                                       Customer Reff No.&nbsp;
                   </td>
                  
                <td width="15%" align="left">
                <asp:TextBox ID="txtCustomerReffNo" runat="server" ValidationGroup="M1" 
                        TabIndex="7"></asp:TextBox>
                </td>                   
                 <td width="15%" align="left">
               
                  </td>
                 <td width="15%" align="left">  
                         
                 </td>
                </tr>
    <tr>
          <td width="25%" align="right" class="style2" >
                Customer Address&nbsp; r Address&nbsp;
                </td>                
                 <td width="100%" align="left" colspan="5"  CssClass="TextBox TextBoxDisplay" 
                            class="style2"> 
                   <asp:TextBox ID="txtAddress" runat="server" ValidationGroup="M1" Width="99%" 
                         ReadOnly="true" TabIndex="8"></asp:TextBox>
                  </td>
               
                </tr>
      <tr>
               <td width="25%" align="right">
                   Delivery Mode      Delivery Mode
                    </td>                
                 <td width="15%" align="left">
                     <cc1:OboutDropDownList ID="cmbDeliveryMode" runat="server" Width="75px" 
                                        ControlsToDisable="" ControlsToEnable="" DisablingValues="" EnablingValues="" 
                                        FolderStyle="" MenuWidth="75px" TabIndex="9" ></cc1:OboutDropDownList>
                   
                  </td>
                    <td width="15%" align="right" class="tdRight">
                        Agent&nbsp;             Agent&nbsp;
                       </td>
                 
                    <td width="15%" align="left">
                     <asp:TextBox ID="txtAgent" runat="server" ValidationGroup="M1" Width="99%" 
                            TabIndex="10" ></asp:TextBox>      
                    </td>                   
                 <td width="15%" align="right">
                Direct Billing :
                  </td>
                    <td width="15%" align="left">
                   <asp:TextBox ID="txtDirectBilling" runat="server" ValidationGroup="M1" Width="99%" 
                            TabIndex="11" ></asp:TextBox>
                    </td>
                </tr>
 
 
 </table>
 </td>
 </tr>
 
 <tr>
 <td >
 <table class="tContentArial td" width="100%">
             <tr>
         
                                      
   <td>
   <table width="100%">
 
                            <tr bgcolor="#006699">
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Design No.</b></span>
                                </td>
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Collection</b></span>
                                </td>
                                <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>Colour</b></span>
                                </td>
                                <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>Matching Reff</b></span>
                                </td>
                                <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>Quantity</b></span>
                                </td>
                                <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>Roll Size</b></span>
                                </td>
                                 <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>End Use</b></span>
                                </td>
                                <td class="tdRight SmallFont">
                                    <span class="titleheading"><b>Remarks</b></span>
                                </td>
                                 <td class="tdLeft SmallFont">
                                     &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                              <cc2:ComboBox ID="cmbDesignNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="True"
                            DataTextField="DESIGN_NO" DataValueField="DESIGN_NO" Width="100px" MenuWidth="550px"
                            Height="200px" CssClass="SmallFont" TabIndex="12" 
                         EmptyText="Find Design No" onloadingitems="cmbDesignNo_LoadingItems" 
                                        onselectedindexchanged="cmbDesignNo_SelectedIndexChanged">
                           <HeaderTemplate>
                                <div class="header c1">
                                     Design No</div>
                                      <div class="header c1">
                                     No Of Colors</div>
                                      <div class="header c1">
                                     Collection Name</div>
                                      <div class="header c1">
                                     End Use</div>
                                   
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("DESIGN_NO") %>' /></div>
                                    <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("NO_COLOR") %>' /></div>
                                    <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("COLLECTION_NAME") %>' /></div>
                                    <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("END_USE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                      </cc2:ComboBox>
                                    &nbsp;</td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtCollection" runat="server" 
                                        Width="100px" TabIndex="13"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                <cc1:OboutDropDownList ID="ddlColour" runat="server" Width="75px" 
                                        ControlsToDisable="" ControlsToEnable="" DisablingValues="" EnablingValues="" 
                                        FolderStyle="" MenuWidth="75px" TabIndex="14" ></cc1:OboutDropDownList>
                                               
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtMatchingReff" runat="server"
                                        Width="60px" TabIndex="15"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtQuantity" runat="server" 
                                        Width="60px" TabIndex="16"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRollSize" runat="server" 
                                         Width="70px" TabIndex="17"></asp:TextBox>
                                </td>
                                  </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtEndUse" runat="server" 
                                        Width="60px" TabIndex="18"></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="txtRemarks" runat="server" 
                                         Width="70px" TabIndex="19"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" style="width:150px" class="style1">
                                    <asp:Button ID="btnFabricSave"  runat="server" Text="Save" 
                                        onclick="btnFabricSave_Click" TabIndex="20"/> 
                                    <asp:Button ID="btnFabricCancel"  runat="server" Text="Cancel" 
                                        onclick="btnFabricCancel_Click" TabIndex="21"/></td>
                            </tr>
                        </table>

 </td>
 </tr>  
 <tr>
 <td>
 <table width="100%"><tr id="tr2" runat="server">
                    <td id="Td2" class="td" align="left" width="100%" runat="server">
                       <%-- <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" Width="100%">--%>
                                    <asp:GridView ID="GridFabric" runat="server" CssClass="SmallFont" Font-Bold="False"
                                        ShowFooter="True" 
                                        AutoGenerateColumns="False" AllowSorting="True" Width="98%" 
                                        onrowcommand="GridFabric_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Design No">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtDesignNo" Font-Bold="true" runat="server" Text='<%# Bind("DESIGN_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Collection">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtCollection" runat="server" Text='<%# Bind("COLLECTION_NAME") %>' CssClass="Label SmallFont"></asp:Label>
                                                    <%--<asp:TextBox ID="txtIndentDetailNumber" runat="server" Text='<%# Bind("IndentDetailNumber") %>'--%>
                                                     
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="21%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Colour">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtColour" runat="server" Text='<%# Bind("COLOR") %>'
                                                        CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Matching Reff">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtMatchingReff" runat="server" Text='<%# Bind("MATCHING_REFF") %>'
                                                        CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQuantity" runat="server" Text='<%# Bind("QUANTITY") %>' CssClass="LabelNo SmallFont"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="5%"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Roll Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRollSize" runat="server" Font-Bold="true" Text='<%# Bind("ROLL_SIZE") %>'
                                                        CssClass="LabelNo SmallFont" AutoPostBack="True" OnTextChanged="txtRequestQty_TextChanged"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                           </asp:TemplateField>
                                              <asp:TemplateField HeaderText="End Use">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtEndUse" runat="server" Font-Bold="true" Text='<%# Bind("END_USE") %>'
                                                        CssClass="LabelNo SmallFont" AutoPostBack="True" OnTextChanged="txtRequestQty_TextChanged"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                           </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRemarks" runat="server" Font-Bold="true" Text='<%# Bind("REMARKS") %>'
                                                        CssClass="LabelNo SmallFont" AutoPostBack="True" OnTextChanged="txtRequestQty_TextChanged"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="8%"></ItemStyle>
                                           </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                <asp:LinkButton ID ="lnkEdit" runat="server"  text="Edit" CommandName="EditItem" CommandArgument='<%# Bind("UNIQUE_ID") %>'/>
                                                <asp:LinkButton ID ="lnkDelete" runat="server"  text="Delete" CommandName="DelItem" CommandArgument='<%# Bind("UNIQUE_ID") %>'/>
                                                  
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                        <RowStyle CssClass="SmallFont" />
                                    </asp:GridView>
                            <%--   </asp:Panel>--%>
                    
                    </td>
                </tr>
                </table>
                 </td>
                </tr>
            </table>
 </td>
 </tr>
 </table>
<%--</ContentTemplate>
</asp:UpdatePanel>--%>