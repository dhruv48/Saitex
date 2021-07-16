<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Issue_Against_PA_Report.ascx.cs" Inherits="Module_Yarn_SalesWork_Queries_Issue_Against_PA_Report" %>

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
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ></asp:ImageButton>
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
                    <span class="titleheading"><strong>Issue Against PA Report</strong></span>
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
                    PI No:
                </td>
                <td>
                
                    <cc2:ComboBox ID="cmbCustomer" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="PI_NO" DataValueField="PI_NO" MenuWidth="250px" EnableVirtualScrolling="true" 
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="cmbCustomer_LoadingItems"
                        EmptyText="" TabIndex="5">
                        <HeaderTemplate>
                            <div class="header d1">
                                PI No</div>
                           
                             <div class="header c1">
                                Year</div>
                            </HeaderTemplate>
                        <ItemTemplate>
                            <div class="item d1">
                                <%# Eval("PI_NO")%></div>
                                <div class="item c1">
                                <%# Eval("YEAR")%></div>

                            
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                   
                </td>
                <td align="right">
                    Quality Code :
                </td>
                <td>
                    <cc2:ComboBox ID="txtYarn" runat="server" CssClass="smallfont" Width="161px" EnableLoadOnDemand="True"
                        DataTextField="YARN_CODE" DataValueField="ASS_YARN_DESC" MenuWidth="550px" EnableVirtualScrolling="true"
                        OpenOnFocus="true" Visible="true" Height="200px" OnLoadingItems="txtYCODE_LoadingItems"
                        EmptyText="" TabIndex="6">
                        <HeaderTemplate>
                            <div class="header c1">
                                Quality Code</div>
                            <div class="header c4">
                                Quality Description</div>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="header c1">
                                <%# Eval("YARN_CODE")%></div>
                            <div class="header c4">
                                <%# Eval("ASS_YARN_DESC")%></div>
                        </ItemTemplate>
                        <FooterTemplate>
                            Displaying items
                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                            <%# Container.ItemsCount %>.
                        </FooterTemplate>
                    </cc2:ComboBox>
                </td>
                <td align="right">
                    
                    
                    Base Lot No:
                    
                    
                </td>
                <td>
                    
                    
                    <cc2:ComboBox ID="cmbGreyLotNo" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="LOT_NO" DataValueField="LOT_NO" EmptyText="" Width="150px"
                                    MenuWidth="250px" Height="200px" TabIndex="7" OnLoadingItems="cmbGREY_LOT_NO_LoadingItems" OpenOnFocus="true"
                                     EnableVirtualScrolling="true" >
                                    <HeaderTemplate>
                                        <div class="header d3">
                                             LOT NO</div>
                                          
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item d3">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LOT_NO") %>' /></div>
                                           
                                    </ItemTemplate>
                                   
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                    
                    
                    
                </td>
                <td visible="false"> <%--Shade :--%></td>
             
               
                
               
            </tr>
            
            
            
            <tr>
              <td align="right">
                    
                    
                    Machine Code:
                    
                    
                </td>
                <td >
                    
                    
                    
                    
                    <cc2:ComboBox ID="cmbMachineCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="MAC_CODE" DataValueField="MAC_CODE" EmptyText="" Width="160px"
                                    MenuWidth="150px" Height="200px" TabIndex="9" OnLoadingItems="cmbMachineCode_LoadingItems"
                                     EnableVirtualScrolling="true" >
                                    <HeaderTemplate>
                                        <div class="header c1">
                                           Machine Code</div>
                                      </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MAC_CODE") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
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
                    <asp:Button ID="btnGetReport" runat="server" Text="Get Record" OnClick="btnGetReport_Click" CssClass="AButton"  TabIndex="10"/>
                </td>
                <td>
                </td>
                 
            </tr>
            <tr>
                <td class="td tContentArial" colspan="8" >
                    <%--<asp:Panel ID="pnlShowHover" runat="server" Width="100%" ScrollBars="Auto" Height="300px" >--%>
                       <asp:GridView ID="grdLabDipSubmission" runat="server" AutoGenerateColumns="False" AllowPaging="true"
                                     CssClass="SmallFont" Width="99%" TabIndex="21" BackColor="White" 
                               OnPageIndexChanging="Grid1_PageIndexChanging"  >
                               <%--onrowdatabound="grdLabDipSubmission_RowDataBound"--%>
                                  
                                  
                                  
                                    <Columns >
                                     
                                     <asp:TemplateField HeaderText="Issue slip No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueSlip" runat="server" Font-Size="X-Small" Text='<%# Bind("ISSUE_SLIP_NO") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Issue slip Date" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblIssueSlip" runat="server" Font-Size="X-Small" Text='<%# Bind("ISSUE_SLIP_DATE","{0:dd/MM/yyyy}") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Machine" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMachine" runat="server" Font-Size="X-Small" Text='<%# Bind("MAC_CODE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Yarn Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblYarnCode" runat="server" Font-Size="X-Small" Text='<%# Bind("YARN_CODE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Yarn Dis Quality" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblYARNDESC" runat="server" Font-Size="X-Small" Text='<%# Bind("ASS_YARN_DESC") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        
                                        <asp:TemplateField HeaderText="Shade Code" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblShadeCode" runat="server" Font-Size="X-Small" Text='<%# Bind("SHADE_CODE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        
                                         <asp:TemplateField HeaderText="Lot No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLotNo" runat="server" Font-Size="X-Small" Text='<%# Bind("LOT_NO") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Cheese" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCheese" runat="server" Font-Size="X-Small" Text='<%# Bind("NO_OF_CHEESE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Weight of Unit" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblWeightofUnit" runat="server" Font-Size="X-Small" Text='<%# Bind("WEIGHT_OF_UNIT") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Gross Weight" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGrossWt" runat="server" Font-Size="X-Small" Text='<%# Bind("GROSS_WT") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Tare Weight" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTareWt" runat="server" Font-Size="X-Small" Text='<%# Bind("TARE_WT") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Net Weight" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNetWt" runat="server" Font-Size="X-Small" Text='<%# Bind("TRN_QTY") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Paper Tube Weight" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPapertubeWeight" runat="server" Font-Size="X-Small" Text='<%# Bind("PAPER_TUBR_WEIGHT") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Paper Tube Size" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPaperTubeSize" runat="server" Font-Size="X-Small" Text='<%# Bind("PAPER_TUBR_SIZE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Location" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLOCATION" runat="server" Font-Size="X-Small" Text='<%# Bind("LOCATION") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                         <asp:TemplateField HeaderText="Store" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSTORE" runat="server" Font-Size="X-Small" Text='<%# Bind("STORE") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Trolly and Crates No" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTrolly" runat="server" Font-Size="X-Small" Text='<%# Bind("LORY_NUMB") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Tint" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRemarks" runat="server" Font-Size="X-Small" Text='<%# Bind("TINT") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:TemplateField HeaderText="Remarks" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRCOMMENT" runat="server" Font-Size="X-Small" Text='<%# Bind("REMARKS") %>' 
                                                    CssClass="LabelNo" ></asp:Label>
                                                    
                                            </ItemTemplate>
                                            
                                            <ItemStyle Width="100px" HorizontalAlign="Left" />
                                            
                                        </asp:TemplateField>
                                     
                                     
                                    <%--    <asp:TemplateField HeaderText="">
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