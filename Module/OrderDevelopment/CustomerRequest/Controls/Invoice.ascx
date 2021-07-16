<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Invoice.ascx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Controls_Invoice" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 80px;
    }
    .c4
    {
        margin-left: 4px;
        width: 300px;
    }
    
    .c5
    {
        margin-left: 4px;
        width: 390px;
    }
    .d1
    {
        width: 150px;
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
                    <span class="titleheading"><strong>Invoice Query Form</strong></span>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Invoice Type
                </td>
                <td>
                    <asp:DropDownList ID="ddlInvoiceType"  runat="server" AutoPostBack="true" 
                                    CssClass="gCtrTxt" Width="150px" TabIndex="1" >
                                </asp:DropDownList>
                </td>
                <td class="tdRight">
                    Year:
                </td>
                <td>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="True" CssClass="gCtrTxt"
                        Font-Size="8"  Width="150px" TabIndex="2">
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
                    Invoice No:
                </td>
                <td>
                
                    <cc2:ComboBox ID="cmbInvoiceNo" runat="server" CssClass="smallfont" Width="150px" EnableLoadOnDemand="True"
                        DataTextField="INVOICE_NUMB" DataValueField="YEAR" MenuWidth="200px" EnableVirtualScrolling="true" 
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="cmbInvoiceNo_LoadingItems"
                        EmptyText="" TabIndex="5">
                        <HeaderTemplate>
                            <div class="header c1">
                                Invoice No</div>
                           
                             <div class="header c1">
                                Year</div>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("INVOICE_NUMB")%></div>
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
                    Challan No :
                </td>
                <td>
                 
                    <cc2:ComboBox ID="cmbChallanNo" runat="server" CssClass="smallfont" Width="150px" EnableLoadOnDemand="True"
                        DataTextField="CHALLAN_NO" DataValueField="YEAR" MenuWidth="200px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtChallanNo_LoadingItems"
                        EmptyText="" TabIndex="6">
                        <HeaderTemplate>
                            <div class="header c1">
                                Challan No</div>
                            <div class="header c1">
                                Year</div>
                          
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("CHALLAN_NO")%></div>
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
                       <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="150px" EnableLoadOnDemand="True"
                        DataTextField="YARN_CODE" DataValueField="YARN_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                        EmptyText="" TabIndex="6" >
                        <HeaderTemplate>
                            <div class="header c1">
                                Quality Code</div>
                            <div class="header c5">
                                Quality Description</div>
                          
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item c1">
                                <%# Eval("YARN_CODE")%></div>
                            <div class="item c5">
                                <%# Eval("YARN_DESC")%></div>
                            
                           
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                            out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                    
                </td>
                <td align="right"> Shade:</td>
                <td ><cc2:ComboBox ID="cmbShade" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="SHADE_CODE" DataValueField="SHADE_FAMILY_CODE" EmptyText="" Width="150px"
                                    MenuWidth="350px" Height="200px" TabIndex="8" OnLoadingItems="cmbShade_LoadingItems"
                                     EnableVirtualScrolling="true" Visible="true">
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
                    Party Name:
                </td>
                <td >
                    <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="PRTY_NAME" EmptyText="" Width="150px"
                                    MenuWidth="350px" Height="200px" TabIndex="9" OnLoadingItems="cmbPartyCode_LoadingItems"
                                     EnableVirtualScrolling="true">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                           Party Code</div>
                                        <div class="header d2">
                                          Party Name</div>
                                      
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item d2">
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
                    <asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="300px" >
                       <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                     CssClass="SmallFont" Width="99%" TabIndex="21" BackColor="White"  OnPageIndexChanging="Grid1_PageIndexChanging" onrowdatabound="grdLabDipSubmission_RowDataBound" >
                                  
                                  
                                  
                                    <Columns >
                                     
                                     
                                      <asp:TemplateField HeaderText="Invoice Type" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblInvoiceType" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("BUSINESS_TYPE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Invoice No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblInvoiceNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_NUMB") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Invoice Date" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblInvoiceDate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_DT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Challan No" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblChallanNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("CHALLAN_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Buyer's PO No." >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblPoNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("PO_NUMB") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Party Name" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblPartyName" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("PRTY_NAME") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText=" Vehicle NO." >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblVehicleNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_VEHICLE_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText=" LR NO." >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblLRNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_LR_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText=" Yarn Code" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblYarnCode" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("ARTICLE_CODE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText=" Yarn Description" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblYarnDesc" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("ARTICLE_DESC") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Lot NO." >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblLotNo" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_LOT_NO") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="No of Packages" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblNoOfPackages" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_NO_OF_UNIT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Total Qty(In Kgs)" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblTotalQty" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_QTY") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Rate" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblRate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_RATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Amount" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblAmount" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("INVOICE_AMOUNT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Freight" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblFreight" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("FREIGHT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Other Charges" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblOtherC" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("ED") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="CGST"  Visible="false">
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCgst" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_TAX_TYPE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CGST Rate" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCgstRate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_TAX_RATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="CGST Amount" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblCGSTAmount" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_TAX_AMOUNT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="SGST" Visible="false" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblSgst" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_SGST_TYPE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="SGST Rate" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblSgstRate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_SGST_RATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="SGST Amount" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblSGSTAmount" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_SGST_AMOUNT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="IGST Rate" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblIGSTRate" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_IGST_RATE") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="IGST AMOUNT" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblIGSTAmount" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    Text='<%# Bind("SALE_IGST_AMOUNT") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Grand Total" >
                                            <ItemTemplate  >
                                                <asp:Label ID="lblGrandAmount" CssClass="LabelNo"  Font-Size="Smaller" runat="server" 
                                                    ></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        
                                        <%--<asp:TemplateField HeaderText="">
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
                    </asp:TemplateField>--%>
                                      
                                    </Columns>
                                     
                                     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                                      <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <RowStyle VerticalAlign="Top" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont titleheading" BackColor="#336699" />
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  />
                                </asp:GridView>
                    </asp:Panel>
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