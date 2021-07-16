<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WO_Issue_Approval.ascx.cs" Inherits="Module_WorkOrder_Controls_WO_Issue_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" class="td" valign="top" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click1" 
                            ToolTip="Update" ValidationGroup="M1" Width="48" />
                    </td>
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/del6.png" ToolTip="Delete" Width="48" />
                    </td>
                    <td id="tdFind" runat="server" align="left" visible="false">
                        <asp:ImageButton ID="imgbtnFindTop" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Width="48" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                            ToolTip="Print" Width="48" />
                        &nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" 
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" ToolTip="Clear" 
                            Width="48" />
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
            <b class="titleheading">WOrk Order Issue Approval</b>
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
            <asp:GridView ID="gvMaterialReceiptApproval" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" CssClass="SmallFont"  Width="100%"  
                AllowPaging="true" PageSize="20"
                onrowdatabound="gvMaterialReceiptApproval_RowDataBound" 
                onpageindexchanging="gvMaterialReceiptApproval_PageIndexChanging">
                <Columns>
                 <%--   <asp:BoundField DataField="YEAR" HeaderText="YEAR">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:BoundField>--%>
                      <asp:TemplateField HeaderText="TRN&nbsp;Date">
                        <ItemTemplate>
                            <asp:Label ID="lbltrndate" runat="server"   Text='<%# Bind("TRN_DATE","{0:dd/MM/yyyy}") %>' ToolTip='<%# Bind("YEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="TRN_DESC" HeaderText="MRN&nbsp;Type">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="MRN&nbsp;No">
                        <ItemTemplate>
                            <asp:Label ID="lblTRN_NUMB" runat="server" Text='<%# Bind("TRN_NUMB") %>' 
                                ToolTip='<%# Bind("TRN_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%# Bind("TRN_TYPE") %>' 
                                ToolTip='<%# Bind("YEAR") %>' Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PARTY_DATA" HeaderText="Party">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:BoundField>
                    
                       
                       <asp:TemplateField HeaderText="Challan&nbsp;Date">
                        <ItemTemplate>
                            <asp:Label ID="lblprtychdate" runat="server"   Text='<%# Bind("PRTY_CH_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="PRTY_CH_NUMB" HeaderText="Party&nbsp;Challan&nbsp;No">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:BoundField>
                     <asp:BoundField DataField="BILL_NUMB" HeaderText="Invoice&nbsp;No" HeaderStyle-HorizontalAlign="Right"
                        ItemStyle-HorizontalAlign="Right">
                        <ItemStyle HorizontalAlign="Right" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GATE_NUMB" HeaderText="Gate&nbsp;Numb">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Center" 
                            VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm&nbsp;Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" 
                                CssClass=" TextBox TextBoxDisplay SmallFont" ReadOnly="true" 
                                Text='<%# Bind("CONF_DATE") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm&nbsp;By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" 
                                CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true" 
                                Text='<%# Bind("CONF_BY") %>' ToolTip='<%# Bind("CONF_BY") %>'></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Wrap="true" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                    <asp:LinkButton ID="lbtnViewDetails" runat="server" Text="ViewDetails"></asp:LinkButton>
                    <asp:Panel ID="pnlTRN" runat="server" Width="470px" BackColor="Beige" BorderWidth="2px"
                                Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="grdTRN" runat="server" AutoGenerateColumns="False" Width="450px"
                                    CssClass="SmallFont" Height="140px">
                                <Columns>
                                 <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn&nbsp;Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn&nbsp;Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="HSN_CODE" HeaderText="HSN&nbsp;Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="SHADE_FAMILY" HeaderText="Shade&nbsp;Family">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="SHADE" HeaderText="Shade">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                      
                                        <asp:BoundField DataField="TRN_QTY" HeaderText="Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                          <asp:BoundField DataField="UOM" HeaderText="UOM">
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
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                                </asp:Panel>
                                <cc1:HoverMenuExtender ID="hmeTRN" runat="server" PopupPosition="Left" PopupControlID="pnlTRN"
                                TargetControlID="lbtnViewDetails">
                            </cc1:HoverMenuExtender>
                    </ItemTemplate>
                    </asp:TemplateField>
                    
                      
                </Columns>
                <RowStyle CssClass="SmallFont" Width="98%" />
                <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                
                 
            </asp:GridView>
        </td>
    </tr>
</table>