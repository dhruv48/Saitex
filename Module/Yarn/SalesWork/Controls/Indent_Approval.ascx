<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Indent_Approval.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_Indent_Approval" %>
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
                    <td id="tdDelete" runat="server" align="left" width="48">
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
            <b class="titleheading">Yarn Indent Approval</b>
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
                    <asp:BoundField DataField="DEPT_NAME" HeaderText="Dept Code">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Center"  VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Indent Type">
                        <ItemTemplate>
                            <asp:Label ID="lblITEM_TYPE" runat="server" Text='<%# Bind("IND_TYPE") %>' ToolTip='<%# Bind("IND_TYPE") %>'
                                ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Width="40px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Indent No">
                        <ItemTemplate>
                            <asp:Label ID="lblInd_NUMB" runat="server" Text='<%# Bind("IND_NUMB") %>' ToolTip='<%# Bind("IND_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblItemCode" runat="server" Text='<%# Bind("YARN_CODE") %>' ToolTip='<%# Bind("IND_TYPE") %>'
                                Visible="false"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="REQD_DATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Required Date"
                        HtmlEncode="False">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="YARN_DESC" HeaderText="Yarn Description">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOT_NO" HeaderText="LOT No">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="GRADE" HeaderText="GRADE">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="HSN_CODE" HeaderText="HSN Code">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                       <asp:TemplateField HeaderText="Shade Family">
                         <ItemTemplate>
                             <asp:Label ID="lblShadeFamily" runat="server" Text='<%# Bind("SHADE_FAMILY") %>'></asp:Label>
                         </ItemTemplate>
                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Shade">
                         <ItemTemplate>
                             <asp:Label ID="lblShade" runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                         </ItemTemplate>                     
                         <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                             VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="currentStock" HeaderText="Current Stock">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="40px" />
                    </asp:BoundField>
                    <%-- <asp:TemplateField HeaderText="Requested Qty" >
                            <ItemTemplate>
                               
                            </ItemTemplate>
                            <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="50px" />
                            
                    </asp:TemplateField>--%>
                    <asp:BoundField DataField="RQST_QTY" HeaderText="Requested Qty">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                            Width="50px" />
                    </asp:BoundField>
                    <%-- <asp:BoundField DataField="APPR_QTY" HeaderText="Approved Qty">
                            <ItemStyle HorizontalAlign="Right" CssClass="labelNo smallfont" VerticalAlign="Top"
                                Width="50px" />
                        </asp:BoundField>--%>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:CheckBox ID="chkApproved" runat="server" />
                        </ItemTemplate>
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="txtApprovedQty" runat="server" CssClass="TextBoxNo SmallFont" Text='<%# Bind("RQST_QTY") %>'
                                Width="50px" MaxLength="7"></asp:TextBox>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtApprovedQty"
                                Display="dynamic" ErrorMessage="Pls enter b/w 0 to 9999999" MaximumValue="9999999"
                                MinimumValue="0" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                            <asp:TextBox ID="TextBox1" Text='<%# Bind("RQST_QTY") %>' runat="server" Visible="false"></asp:TextBox>
                        
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
