<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MaternityLeave.ascx.cs"
    Inherits="CommonControls_MaternityLeave" %>
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td>
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="61px" Height="40px" OnClick="imgbtnSave_Click1"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')"
                            OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" OnClick="imgbtnExit_Click1"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <table border="0" cellpadding="3" cellspacing="0" width="400" class="tContentArial">
                <tr>
                    <td align="center" colspan="8" valign="top" class="tRowColorAdmin">
                        <span class="H3Heading">Maternity Leave Master</span>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left" colspan="3" valign="top">
                        <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="200" valign="top">
                        Year</td>
                    <td align="center" width="2%" valign="top">
                        <b>:</b>
                    </td>
                    <td align="left" width="195" valign="top">
                        <asp:TextBox ID="txtYear" runat="server" CssClass="gCtrTxt" Width="80" MaxLength="4"></asp:TextBox>&nbsp;
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtYear"
                            Display="Dynamic" ErrorMessage="Enter Digit Only" Font-Bold="False" ValidationExpression="^[0-9]{4}$"
                            ValidationGroup="Maternity"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtYear"
                            ErrorMessage="Year Can't be Previous Year" MaximumValue="2100" MinimumValue="2010"
                            Type="Integer" ValidationGroup="Maternity"></asp:RangeValidator></td>
                </tr>
                <tr>
                    <td align="right" width="200" valign="top">
                        No of days</td>
                    <td align="center" width="2%" valign="top">
                        <b>:</b>
                    </td>
                    <td align="left" width="195" valign="top">
                        <asp:TextBox ID="txtLeaveDays" runat="server" CssClass="gCtrTxt" MaxLength="3" Width="50px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLeaveDays"
                            Display="Dynamic" ErrorMessage="Field can't be empty !" ValidationGroup="Maternity"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLeaveDays"
                            Display="Dynamic" ErrorMessage="Pls enter digit only !" Font-Bold="False" ValidationExpression="^\s*[0-9]+\s*$"
                            ValidationGroup="Maternity"></asp:RegularExpressionValidator>
                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtLeaveDays"
                            Display="Dynamic" ErrorMessage="Value Must be between  0-365" MaximumValue="365"
                            MinimumValue="0" Type="Integer" ValidationGroup="Maternity"></asp:RangeValidator></td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="200">
                        Maximum Leave Applicable</td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="195">
                        <asp:RadioButtonList ID="radLeaveApplicable" AutoPostBack="true" runat="server" RepeatColumns="2"
                            RepeatDirection="Horizontal" RepeatLayout="flow">
                            <asp:ListItem Text="Period" Value="PE" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="No Period" Value="NP"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:TextBox ID="txtPeriod" Width="50" MaxLength="2" CssClass="gCtrTxt" runat="server"></asp:TextBox>
                        <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtPeriod"
                            Display="Dynamic" ErrorMessage="Field can't be empty !" ValidationGroup="Maternity"></asp:RequiredFieldValidator><asp:RegularExpressionValidator
                                ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPeriod"
                                Display="Dynamic" ErrorMessage="Pls enter digit only !" Font-Bold="False" ValidationExpression="^\s*[0-9]+\s*$">
                            </asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="200">
                        <span class="mw-headline">Carrying forward leave</span></td>
                    <td align="center" valign="top" width="2%">
                        <b>:</b>
                    </td>
                    <td align="left" valign="top" width="195">
                        <asp:RadioButtonList ID="radCarrying_forward_leave" RepeatColumns="2" RepeatDirection="Horizontal"
                            RepeatLayout="Flow" runat="server">
                            <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                            <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td width="200" align="right" valign="top">
                        Status</td>
                    <td width="2%" align="center" valign="top">
                        <b>:</b></td>
                    <td width="195" align="left" valign="top">
                        <asp:CheckBox ID="chkActive" runat="server" TabIndex="2" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
     <tr>
                    <td colspan="6" align="left" width="100%">
                        <br />
                        <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                        </b>
                    </td>
                </tr>
    <tr>
        <td align="left" width="100%" colspan="3">
        <br />
            <asp:GridView ID="gvMaternityLeavemaster" runat="server" PagerSettings-Mode="Numeric" PageSize="10"
                PagerSettings-Position="Bottom" PagerStyle-HorizontalAlign="Left" AutoGenerateColumns="False"
                AllowPaging="True" Width="100%" CssClass="tContentArial" OnRowCommand="gvMaternityLeavemaster_RowCommand" OnPageIndexChanging="gvMaternityLeavemaster_PageIndexChanging">
                <HeaderStyle HorizontalAlign="center"/>
                <Columns>
                    <asp:BoundField HeaderText="IN_MATERNITYLEAVEMASTERID" DataField="IN_MATERNITYLEAVEMASTERID"
                        Visible="false">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="YEAR" DataField="CH_YEAR">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="NO OF DAYS" DataField="CH_LEAVEDAY">
                                <ItemStyle HorizontalAlign="center" />
                                   </asp:BoundField>
                                <asp:BoundField HeaderText="CARRING FORWARDED" DataField="CH_FORWARDED">
                                    <ItemStyle HorizontalAlign="center" />
                                </asp:BoundField>
                   
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CausesValidation="false" CommandArgument='<%# Eval("IN_MATERNITYLEAVEMASTERID") %>'
                                Text="Edit"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Left" />
            </asp:GridView>
        </td>
    </tr>
</table>
