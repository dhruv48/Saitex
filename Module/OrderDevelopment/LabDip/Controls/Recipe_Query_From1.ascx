<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Recipe_Query_From1.ascx.cs" Inherits="Module_OrderDevelopment_LabDip_Controls_Recipe_Query_From1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
         width: 81px;
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
        width: 81px;
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
                            <td >
                                <%--<asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" Enabled="false"></asp:ImageButton>--%>
                            </td>
                             <td>  
                              <asp:ImageButton ID="imgBtnExportExcel" runat="server" Width="48" Height="41" ToolTip="Excel Report"
                            ImageUrl="~/CommonImages/export.png" onclick="imgBtnExportExcel_Click" TabIndex="11" ></asp:ImageButton>&nbsp;</td> 
                    
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" TabIndex="12"></asp:ImageButton>
                            </td>
                            <td align="center" valign="top" >
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" TabIndex="13" />
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
                    <span class="titleheading"><strong>Lab Dip Recipe Query Form</strong></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Order Type
                </td>
                <td>
                    <asp:DropDownList ID="ddlBusinessType"  runat="server" AutoPostBack="true" 
                                    CssClass="gCtrTxt" Width="160px" TabIndex="1" >
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
                <td class="tdRight">
            From date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtFromDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                OnTextChanged="TxtFromDate_TextChanged" Width="140px" AutoPostBack="True" TabIndex="3"></asp:TextBox>
        </td>
        <td class="tdRight">
            To Date:
        </td>
        <td class="tdLeft">
            <asp:TextBox ID="TxtToDate" runat="server" CssClass="SmallFont TextBox UpperCase"
                 Width="140px" TabIndex="4" ></asp:TextBox><%--OnTextChanged="TxtToDate_TextChanged" AutoPostBack="True"--%>
        </td>
               
            </tr>
            <tr>
                <td align="right">
                    Customer Request No:
                </td>
                <td>
                
                    <cc2:ComboBox ID="cmbCustomer" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="ORDER_No" DataValueField="ORDER_No" MenuWidth="250px" EnableVirtualScrolling="true" 
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="cmbCustomer_LoadingItems"
                        EmptyText="" TabIndex="5">
                        <HeaderTemplate>
                            <div class="header d1">
                                Customer Request No</div>
                           
                             <div class="header c1">
                                Year</div>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item d1">
                                <%# Eval("ORDER_NO")%></div>
                                <div class="item c1">
                                <%# Eval("YEAR")%></div>

                            
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
                    Quality Code :
                </td>
                <td>
                    <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="ARTICAL_NO" DataValueField="ARTICAL_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                        EmptyText="" TabIndex="6">
                        <HeaderTemplate>
                            <div class="header c1">
                                Quality Code</div>
                            <div class="header c5">
                                Quality Description</div>
                          <%-- <div class="header c1">
                                Cust. Req. No</div>--%>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("ARTICAL_NO")%></div>
                            <div class="item c5">
                                <%# Eval("ARTICAL_DESC")%></div>
                                <%--<div class="item c1">
                                <%# Eval("ORDER_NO")%></div>--%>
                           
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
                    
                    
                    Base Lot No:
                    
                    
                </td>
                <td>
                    
                    
                    <cc2:ComboBox ID="cmbGreyLotNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="GREY_LOT_NO" DataValueField="GREY_LOT_NO" EmptyText="" Width="150px"
                                    MenuWidth="350px" Height="200px" TabIndex="7" OnLoadingItems="cmbGREY_LOT_NO_LoadingItems" OpenOnFocus="true"
                                     EnableVirtualScrolling="true" >
                                    <HeaderTemplate>
                                        <div class="header d3">
                                            Base LOT NO</div>
                                             <div class="header d1">
                                            Base Supplier Name</div>
                                           <%-- <div class="header d3">
                                            Cust.Req. No</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item d3">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("GREY_LOT_NO") %>' /></div>
                                           <div class="item d2">
                                            <asp:Literal runat="server" ID="LBSupplierName" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                             <%--<div class="item d3">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ORDER_NO") %>' /></div>--%>
                                    </ItemTemplate>
                                   
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                    
                    
                    
                </td>
                <td visible="false"> <%--Shade :--%></td>
                <td visible="false">
                
                 <cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="SHADE_CODE" DataValueField="SHADE_FAMILY_CODE" EmptyText="" Width="150px"
                                    MenuWidth="350px" Height="200px" TabIndex="8" OnLoadingItems="cmbShade_LoadingItems"
                                     EnableVirtualScrolling="true" Visible="false">
                                    <HeaderTemplate>
                                        <div class="header d1">
                                            Shade Family</div>
                                        <div class="header d1">
                                            Shade Code</div>
                                        
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item d1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("SHADE_FAMILY_CODE") %>' /></div>
                                        <div class="item d1">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("SHADE_CODE") %>' /></div>
                                      
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
              <td align="right">
                    
                    
                    Customer Name:
                    
                    
                </td>
                <td >
                    
                    
                    
                    
                    <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="PRTY_ADD1" EmptyText="" Width="160px"
                                    MenuWidth="350px" Height="200px" TabIndex="9" OnLoadingItems="cmbPartyCode_LoadingItems"
                                     EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header d2">
                                            NAME</div>
                                       <%-- <div class="header d2">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item d2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                       <%-- <div class="item d2">
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
            <td></td>
                <td style="font-size:large;">
                    <b>Total Records : 
                    <asp:Label ID="lblTotalRecord" runat="server" ></asp:Label></b>
                </td>
               <%-- <td align="right" colspan="3">
                    <asp:UpdateProgress ID="UpdateProgress431" runat="server">
                        <ProgressTemplate>
                            <h3>
                                Loading...
                            </h3>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>--%>
                <td colspan="5" align="right">
                    <asp:Button ID="btnGetReport" runat="server" Text="Get Report" OnClick="btnGetReport_Click" CssClass="AButton"  TabIndex="10"/>
                </td>
                <td colspan="6">
                </td>
                 
            </tr>
            <tr>
                <td class="td tContentArial" colspan="8" >
                    <%--<asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="300px" >--%>
                       <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                     CssClass="SmallFont" Width="99%" TabIndex="21" BackColor="White" onrowdatabound="grdLabDipSubmission_RowDataBound"
                               OnPageIndexChanging="Grid1_PageIndexChanging"  >
                                  
                                  
                                  
                                    <Columns >
                                     
                                     
                                      <asp:TemplateField HeaderText="Customer Request No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("ORDER_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Customer Request Date" Visible="false" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCDate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("ORDER_DATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Submission Date" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblSubmission" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SUBMISSION_DATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Customer Name" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblPartyName" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("PRTY_NAME") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Lab Dip No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblLRNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("LAB_DIP_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Option" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblOption" runat="server" Font-Size="X-Small" Text='<%# Bind("LR_OPTION") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="8px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quality Code" Visible="false"
                                          >
                                            <ItemTemplate>
                                                <asp:Label ID="lblarticalNo" runat="server" Font-Size="X-Small" Text='<%# Bind("YARN_CODE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quality Description" 
                                          >
                                            <ItemTemplate>
                                                <asp:Label ID="lblQualityDesc" runat="server" Font-Size="X-Small" Text='<%# Bind("YARN_DESC") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                         <asp:BoundField DataField="ORDER_REF_NO" HeaderText="Article No" ItemStyle-Width="40px" />
                                        <asp:TemplateField HeaderText="S.F.Code" Visible="true" >
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeFamilyCode" runat="server" Font-Size="X-Small" Text='<%# Bind("SHADE_FAMILY_CODE") %>'
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="Party Description" Visible="true" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeCode" runat="server" Font-Size="X-Small" Text='<%# Bind("PARTY_DESCRIPTION") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" HorizontalAlign="Center"  />
                                        </asp:TemplateField>
                                     
                                          <asp:TemplateField HeaderText="Base Lot No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGreyLot" runat="server" Font-Size="X-Small" Text='<%# Bind("GREY_LOT_NO") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Lot Supplier">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaseLotSupplier" runat="server" Font-Size="X-Small" Text='<%# Bind("BASE_LOT_SUPPLIER") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Colour Cost" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblRecipeCost" runat="server" Font-Size="X-Small" Text='<%# Bind("TOTAL_RECIPE_COSE") %>'
                                                    CssClass="LabelNo"  ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="30px" HorizontalAlign="Left"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Depth Code" Visible="true" 
                                            >
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepthCode" runat="server" Font-Size="X-Small" Text='<%# Bind("DEPTH") %>' 
                                                    CssClass="LabelNo"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px"  HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                       
                                        <asp:TemplateField HeaderText="Remarks" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Font-Size="X-Small" Text='<%# Bind("REMARKS") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewPOTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlPOTRN" runat="server"  BackColor="Beige" BorderWidth="2px">
                                <asp:GridView ID="grdPOTRN" runat="server" AutoGenerateColumns="False" Width="700px"
                                    CssClass="SmallFont" >
                                    <Columns>
                                    
                                     <asp:BoundField DataField="ORDER_NO" HeaderText="Customer Request No" >
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    <asp:BoundField DataField="LR_OPTION" HeaderText="LR&nbsp;Option">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DYE_NAME" HeaderText="Dyes&nbsp;Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="ITEM_DESC" HeaderText="Item&nbsp;Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" Width="250px"/>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RATE" HeaderText="Dyes Rate" DataFormatString="{0:0.000}">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="DOSE" HeaderText="Dose Gm/Kg&nbsp;%" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.000}">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="RECIPE_COST" HeaderText="Dyes&nbsp;Cost" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right" DataFormatString="{0:0.000}">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="LabelNo SmallFont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                   
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet"  />
                                </asp:GridView>
                            </asp:Panel>
                            <cc4:HoverMenuExtender ID="hmePOTRN" runat="server" PopupPosition="Left" PopupControlID="pnlPOTRN"
                                TargetControlID="lbtnViewPOTRN">
                            </cc4:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" Width="20px"/>
                    </asp:TemplateField>
                                      
                                    </Columns>
                                     
                                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                                </asp:GridView>
                   <%-- </asp:Panel>--%>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                   
                </td>
            </tr>
             <tr>
        <td colspan="8">
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
       <Triggers>
       <asp:PostBackTrigger ControlID="imgBtnExportExcel" />
       </Triggers>
        </asp:UpdatePanel>