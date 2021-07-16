<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dyeing_Prod_approval.ascx.cs" Inherits="Module_Production_Controls_Dyeing_Prod_approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<table align="left" border="0" cellpadding="0" cellspacing="0" class="tContentArial" width ="100%">
    <tr>
        <td align="left" valign="top" class="td">
            <table align="left" cellpadding="0" cellspacing="0" >
                <tr>
                    <td id="tdUpdate" runat="server" align="left" width="48">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click1" ToolTip="Update" ValidationGroup="M1" Width="48"
                            OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }" />
                    </td>
                    <td id="tdDelete" runat="server" align="left" width="48" visible="false">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                            ToolTip="Delete" Width="48" OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }" />
                    </td>
                    <td id="tdFind" runat="server" align="left" visible="false" width="48">
                        <asp:ImageButton ID="imgbtnFindTop" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                            ToolTip="Find" Width="48" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" OnClientClick="if (!confirm('Are you sure to print the record ?')) { return false; }" />
                        
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" OnClientClick="if (!confirm('Are you sure to Exit ?')) { return false; }" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" colspan="3">
            <b class="titleheading">Dyeing Production Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" valign="top" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" class="td">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" class="td">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" class="td" Width ="100%">
            <asp:GridView ID="gvMaterialIndentApproval" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                BorderWidth="1px" Width ="99%" >
                <Columns>
                  
                    <asp:TemplateField HeaderText="Indent Type" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblITEM_TYPE" Visible="false" runat="server" Text='<%# Bind("TRN_TYPE") %>' ToolTip='<%# Bind("TRN_TYPE") %>'
                                ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TRN No">
                        <ItemTemplate>
                            <asp:Label ID="lblInd_NUMB" runat="server" Text='<%# Bind("TRN_NUMB") %>' ToolTip='<%# Bind("TRN_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("ARTICAL_CODE") %>' ToolTip='<%# Bind("TRN_TYPE") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CONF_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Required Date"
                        HtmlEncode="False">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ARTICAL_DESC" HeaderText="Quality Description">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="150px" />
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="Shade">
                         <ItemTemplate>
                             <asp:Label ID="lblgrylot" runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                         </ItemTemplate>                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="150px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Grey Lot No">
                         <ItemTemplate>
                             <asp:Label ID="lblShade" runat="server" Text='<%# Bind("GREY_LOT_NO") %>'></asp:Label>
                         </ItemTemplate>                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="150px" />
                    </asp:TemplateField>
                       <asp:TemplateField HeaderText="CR. Req. No">
                         <ItemTemplate>
                             <asp:Label ID="lblShadeFamily" runat="server" Text='<%# Bind("CUST_REQ_NO") %>'></asp:Label>
                         </ItemTemplate>
                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="150px" />
                    </asp:TemplateField>                    
                     <asp:TemplateField HeaderText="Party Name">
                         <ItemTemplate>
                             <asp:Label ID="lblParty" runat="server" Text='<%# Bind("PARTY_NAME") %>'></asp:Label>
                         </ItemTemplate>                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="150px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="UOM">
                         <ItemTemplate>
                             <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                         </ItemTemplate>                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="80px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="REJ_TRN_QTY" HeaderText="REJ QTY">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="40px" />
                    </asp:BoundField>
                  
                    <asp:BoundField DataField="MACHINE_CODE" HeaderText="Machine No">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                 <asp:BoundField DataField="TO_LOCATION" HeaderText="TO LOCATION ">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                <asp:BoundField DataField="FROM_LOCATION" HeaderText="FROM LOCATION ">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="TO_PROCESS" HeaderText="TO PROCESS ">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FROM_PROCESS" HeaderText=" FROM PROCESS ">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="PROCESS" HeaderText=" Packing Process ">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="txtApprovedQty" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                Width="50px" MaxLength="7" ReadOnly="true"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtApprovedQty"
                                Display="dynamic" ErrorMessage="Pls enter b/w 0 to 9999999" MaximumValue="9999999"
                                MinimumValue="0" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                            <asp:TextBox ID="TextBox1" Text='<%# Bind("TRN_QTY") %>' runat="server" Visible="false"></asp:TextBox>
                        
                        <cc1:FilteredTextBoxExtender ID="FiltertxtApprovedQty" runat="server"  TargetControlID="txtApprovedQty"   FilterType="Custom, Numbers" ValidChars="."/>
                        
                        
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="TextBox SmallFont TextBoxDisplay" Text='<%# Bind("CONF_DATE") %>'
                                Width="55px" Height="16px" ReadOnly="true"></asp:TextBox>
                           <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtConfirmDate">
                            </cc1:CalendarExtender>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                Text='<%# Bind("CONF_BY") %>' Width="70px" ReadOnly="true "></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="100px" MaxLength="20"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
                 <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                                ForeColor="White" Font-Bold="True" />
            </asp:GridView>
        </td>
    </tr>
</table>