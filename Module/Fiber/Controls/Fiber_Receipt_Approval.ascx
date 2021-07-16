<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Receipt_Approval.ascx.cs" Inherits="Module_Fiber_Controls_Fiber_Receipt_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table align ="left" class = "tContentArial" width = 100%>
<tr>
<td align="left" class ="td" width = 100% valign = "top">
<table align ="left">
<tr>
<td id = "tdUpdate" align="left" runat="server">


    <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" 
        ImageUrl="~/CommonImages/edit1.jpg" onclick="imgbtnUpdate_Click" 
        ToolTip="Update" ValidationGroup="M1" Width="48px"/>
       </td>
       <td id ="tdDelete" align="left" runat="server">
       
           <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41px" 
               ImageUrl="~/CommonImages/del6.png" onclick="imgbtnDelete_Click" 
               ToolTip="Delete" ValidationGroup="M1" Width="48px" />
               </td>
               <td id="tdFind" align="left" runat="server" visible="false">
               
               
                   <asp:ImageButton ID="imgbtnFindTop" runat="server" Height="41px" 
                       ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" ValidationGroup="M1" 
                       Width="48px" />
               </td>
               <td>
               <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                            ToolTip="Print" Width="48" />
               </td>
               <td>
               <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" 
                            ToolTip="Exit" Width="48" />
               </td>
               <td>
               <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click" 
                            ToolTip="Help" Width="48" />
               </td>
               
</tr>
</table>
</td>
</tr>
 <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Poy Receipt Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" valign="top" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
            &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" class="td" width="100%">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
   <tr>
  <td align="left" class="td" width="100%">
            <asp:GridView ID="gvMaterialReceiptApproval" CssClass="SmallFont" runat="server"
                AllowSorting="True" AutoGenerateColumns="False" 
                onrowdatabound="gvMaterialReceiptApproval_RowDataBound" Width="95%" 
                AllowPaging="true" PageSize="20" 
                onpageindexchanging="gvMaterialReceiptApproval_PageIndexChanging" >
                <Columns>
                    <asp:BoundField DataField="YEAR" HeaderText="YEAR" HeaderStyle-HorizontalAlign="Right" Visible="false"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Left" CssClass="labelNo smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                      <asp:TemplateField HeaderText="TRN&nbsp;Date" >
                        <ItemTemplate>
                            <asp:Label  ID="lblTrnDate" runat="server" ReadOnly="true" Text='<%# Bind("TRN_DATE","{0:dd/MM/yyyy}") %>' ToolTip='<%# Bind("YEAR") %>'
                                ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRN_DESC" HeaderText="MRN&nbsp;Type">
                        <ItemStyle HorizontalAlign="Left" CssClass="label smallfont" Wrap="true" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="MRN&nbsp;No" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" CssClass="LabelNo" ToolTip='<%# Bind("TRN_NUMB") %>'
                                Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>' ToolTip='<%# Bind("YEAR") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PARTY_DATA" HeaderText="Party">
                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    
                       
                       <asp:TemplateField HeaderText="Challan&nbsp;Date">
                        <ItemTemplate>
                            <asp:Label ID="lblprtychdate" runat="server"   Text='<%# Bind("PRTY_CH_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    
                    <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="Party&nbsp;Challan&nbsp;No" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                     <asp:BoundField DataField="BILL_NUMB" HeaderText="Invoice&nbsp;No" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="GATE_NUMB" HeaderText="Gate&nbsp;Numb" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" ReadOnly="true" Text='<%# Bind("CONF_DATE") %>'
                                CssClass=" TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" ReadOnly="true" Text='<%# Bind("CONF_BY") %>'
                                ToolTip='<%# Bind("CONF_BY") %>' CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlTRN" runat="server"  BackColor="#C5E7F1" BorderWidth="2px"
                                 ScrollBars="Auto">
                                <asp:GridView ID="grdTRN" runat="server" AutoGenerateColumns="False" 
                                    CssClass="SmallFont" >
                                    <Columns>
                                        <asp:BoundField DataField="FIBER_CODE" HeaderText="Poy&nbsp;Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FIBER_DESC" HeaderText="Poy&nbsp;Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM" HeaderText="UOM">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="UOM1" HeaderText="UOM2" Visible="false">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="UOM_BAIL" HeaderText="kg/Bail" Visible="false">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="TRN_QTY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FINAL_RATE" HeaderText="Final&nbsp;Rate" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="AMOUNT" HeaderText="Value" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARKS" HeaderText="Comments">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                    </Columns>
                                   <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />                                   
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmeTRN" runat="server" PopupPosition="Left" PopupControlID="pnlTRN"
                                TargetControlID="lbtnViewTRN">
                            </cc1:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnPalletViewTRN" runat="server" Text="Pallet Details"></asp:LinkButton>
                                <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False" ShowFooter="true">
                                        <Columns>
                                      
                                                       <asp:TemplateField HeaderText="LOT&nbsp;NO">                                                   
                                                    <ItemTemplate>
                                                       <asp:Label ID="lblLotNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                    </ItemTemplate>   
                                                     <FooterTemplate>
                                                     <asp:Label ID="lblFLotNo" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true" Text="Total:"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                  
                                                </asp:TemplateField>                                                  
                                                     <asp:BoundField DataField="PALLET_CODE" HeaderText="PALLET CODE" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                    </asp:BoundField>
                                                   
                                                      <asp:TemplateField HeaderText="PALLET NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPalletNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PALLET_NO") %>'></asp:Label>
                                                    </ItemTemplate>       
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFPalletNo" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                              
                                                </asp:TemplateField>
                                                  <asp:TemplateField HeaderText="TRN&nbsp;QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFQTY" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                   
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="COPS">                                                   
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                    </ItemTemplate> 
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFNoUnit" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                  
                                                </asp:TemplateField>
                                                    <asp:BoundField DataField="UOM_OF_UNIT" HeaderText="UOM" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="WEIGHT_OF_UNIT" HeaderText="WEIGHT OF UNIT" HeaderStyle-Font-Size="8">
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                                       </asp:BoundField>
                                                     
                                                        <asp:TemplateField HeaderText="ISSUE&nbsp;QTY">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIssQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ISS_QTY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                     <asp:Label ID="lblFIssQTY" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"></asp:Label>
                                                   
                                                    </FooterTemplate>                                                   
                                                </asp:TemplateField>
                                                     
                                            
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmeBOM" runat="server" PopupControlID="pnlBOM" TargetControlID="lbtnPalletViewTRN"
                                    PopupPosition="Left">
                                </cc1:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>

   </tr>
</table>
