<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Indent_Approval.ascx.cs" Inherits="Module_Fiber_Controls_Fiber_Indent_Approval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table align="left" border="0" cellpadding="0" cellspacing="0"  class="tContentArial">
   
    <tr>
        <td align="left" valign="top" class="td" width="100%">
                <table align="left">
                    <tr>
                        <td id="tdUpdate" runat="server" align="left">
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                        </td>
                        <td id="tdDelete" runat="server" align="left">
                            <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                                ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                        </td>
                        <td id="tdFind" runat="server" visible="false" align="left">
                            <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                                ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader" colspan="3">
            <b class="titleheading">Poy Indent Approval</b>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" valign="top">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
            &nbsp;Mode </span>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
            </b>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3">
            <asp:GridView ID="gvMaterialIndentApproval" runat="server" AllowSorting="True" 
                AutoGenerateColumns="False" BorderWidth="1px">
                <Columns>                  
                    <asp:TemplateField HeaderText="Indent No">
                        <ItemTemplate>
                             <asp:Label ID="lblInd_NUMB" runat="server" Text='<%# Bind("IND_NUMB") %>' 
                                ToolTip='<%# Bind("IND_NUMB") %>'></asp:Label>
                            <asp:Label ID="lblFabricCode" runat="server" ToolTip='<%# Bind("IND_TYPE") %>' 
                             Text='<%# Bind("FABR_CODE") %>' Visible="false"></asp:Label>
                             
                        </ItemTemplate>
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="REQD_DATE" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Required Date" HtmlEncode="False">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FDESC" HeaderText="Poy Description">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="currentStock" HeaderText="Current Stock">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="40px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RQST_QTY" HeaderText="Requested Qty">
                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>                       
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:CheckBox ID="chkApproved" runat="server" />
                            </ItemTemplate>
                            <ItemStyle CssClass="label smallfont" HorizontalAlign="Center" 
                                VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Approved Qty">
                        <ItemTemplate>
                           <asp:TextBox ID="txtApprovedQty" runat="server" Width="50px" CssClass="TextBoxNo SmallFont"
                                    Text='<%# Bind("RQST_QTY") %>' MaxLength="9"></asp:TextBox>
                                <asp:Label ID="lblApprovedQty" runat="server" Visible="false" CssClass="TextBoxNo SmallFont"
                                    Text='<%# Bind("RQST_QTY") %>'></asp:Label>
                            <asp:RangeValidator ID="RangeValidator1" runat="server" 
                                ControlToValidate="txtApprovedQty" Display="dynamic" 
                                ErrorMessage="Pls enter b/w 0 to 100000" MaximumValue="100000" MinimumValue="0" 
                                Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                                
                                
                                  <cc1:FilteredTextBoxExtender ID="FiltertxtApprovedQty" runat="server"
                                                       TargetControlID="txtApprovedQty"         
                                                       FilterType="Custom, Numbers"
                                                            />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="TextBox SmallFont TextBoxDisplay" 
                                Text='<%# Bind("CONF_DATE") %>' Width="50px" ReadOnly="true"></asp:TextBox>
                           <%-- <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                targetcontrolid="txtConfirmDate">
                            </cc1:CalendarExtender>--%>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" CssClass="TextBox SmallFont TextBoxDisplay" 
                                Text='<%# Bind("CONF_BY") %>' Width="70px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" 
                                Width="100px" MaxLength="50"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
                <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
            </asp:GridView>
        </td>
    </tr>
</table>
