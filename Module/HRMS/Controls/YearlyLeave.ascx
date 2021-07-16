<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YearlyLeave.ascx.cs"
    Inherits="Module_HRMS_Controls_YearlyLeave" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
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
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 90px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
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
</style>
<table class="tContentArial">
    <tr>
        <td class="td">
            <table>
                <td id="tdSave" runat="server">
                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/save.jpg"
                        Width="61px" Height="40px" ValidationGroup="M1" OnClick="imgbtnSave_Click1">
                    </asp:ImageButton>
                </td>
                <td id="tdUpdate" runat="server">
                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                        Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                </td>
                <td id="tdFind" runat="server" valign="top">
                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                        Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                </td>
                <td id="tdClear" runat="server">
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
                        Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                </td>
                <td>
                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                        Width="48" Height="41"></asp:ImageButton>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Yearly Leave</span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <%--<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />--%>
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <td>
                    <tr>
                        <td align="right" valign="top">
                            Leave
                        </td>
                        <td align="center" valign="top">
                            <b>:</b>
                        </td>
                        <td align="left" valign="top">
                            <cc1:ComboBox ID="cmbLeaveTran" runat="server" EnableLoadOnDemand="true" DataTextField="LV_NAME"
                                DataValueField="LV_ID" Width="150px" MenuWidth="250px" Height="170px" CssClass="SmallFont"
                                TabIndex="1" EmptyText="Select Leave" OnLoadingItems="cmbLeaveTran_LoadingItems">
                                <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <%# Eval("LV_ID")%></div>
                                    <div class="item c2">
                                        <%# Eval("LV_NAME")%></div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="header c1">
                                        Leave Code
                                    </div>
                                    <div class="header c2">
                                        Leave Name</div>
                                </HeaderTemplate>
                            </cc1:ComboBox>                            
                            <cc1:ComboBox ID="ddlLeave" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                DataTextField="LV_NAME" DataValueField="LV_ID" Width="150px" MenuWidth="250px"
                                Height="170px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Leave" OnLoadingItems="ddlLeave_LoadingItems"
                                OnSelectedIndexChanged="ddlLeave_SelectedIndexChanged">
                                <FooterTemplate>
                                    Displaying items
                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                    out of
                                    <%# Container.ItemsCount %>.
                                </FooterTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <%# Eval("LV_ID")%></div>
                                    <div class="item c2">
                                        <%# Eval("LV_NAME")%></div>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <div class="header c1">
                                        Leave Code
                                    </div>
                                    <div class="header c2">
                                        Leave Name</div>
                                </HeaderTemplate>
                            </cc1:ComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="cmbLeaveTran"
                                Display="Dynamic" ErrorMessage="Select Leave" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" valign="top">
                            &nbsp;
                        </td>
                        <tr>
                            <td align="right" valign="top">
                                Year
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtYear" runat="server" CssClass="gCtrTxt" Width="80" MaxLength="4"
                                    ValidationGroup="M1"></asp:TextBox>&nbsp;
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtYear"
                                    Display="Dynamic" ErrorMessage="Enter Digit Only" Font-Bold="False" ValidationExpression="^[0-9]{4}$"
                                    ValidationGroup="M1"></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtYear"
                                    ErrorMessage="Year Can't be Previous Year" MaximumValue="2100" MinimumValue="2010"
                                    Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                            </td>
                            <td align="right" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                No of days
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtLeaveDays" runat="server" CssClass="gCtrTxt" MaxLength="3" Width="50px"
                                    ValidationGroup="M1"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLeaveDays"
                                    Display="Dynamic" ErrorMessage="Field can't be empty !" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtLeaveDays"
                                    Display="Dynamic" ErrorMessage="Pls enter digit only !" Font-Bold="False" ValidationExpression="^\s*[0-9]+\s*$"
                                    ValidationGroup="M1"></asp:RegularExpressionValidator>
                                <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtLeaveDays"
                                    Display="Dynamic" ErrorMessage="Value Must be between  0-365" MaximumValue="365"
                                    MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
                            </td>
                            <td align="right" valign="top">
                                <td align="left" valign="top">
                                </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Maximum Leave Applicable
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:RadioButtonList ID="radLeaveApplicable" runat="server" RepeatColumns="2" RepeatDirection="Horizontal"
                                    RepeatLayout="flow" OnSelectedIndexChanged="radLeaveApplicable_SelectedIndexChanged">
                                    <asp:ListItem Text="Period" Value="PE"></asp:ListItem>
                                    <asp:ListItem Text="No Period" Value="NP"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="txtPeriod" Width="50" MaxLength="2" CssClass="gCtrTxt" runat="server"></asp:TextBox>
                                <asp:Label ID="lblYear" runat="server" Text="Year"></asp:Label>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtPeriod"
                                    ValidationGroup="M1" Display="Dynamic" ErrorMessage="Pls enter digit only !"
                                    Font-Bold="False" ValidationExpression="^\s*[0-9]+\s*$">
                                </asp:RegularExpressionValidator>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                <span class="mw-headline">Carrying forward leave</span>
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:RadioButtonList ID="radCarrying_forward_leave" RepeatColumns="2" RepeatDirection="Horizontal"
                                    RepeatLayout="Flow" runat="server">
                                    <asp:ListItem Text="Yes" Value="Yes"></asp:ListItem>
                                    <asp:ListItem Text="No" Value="No" Selected="True"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Remarks
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="150px" CssClass="gCtrTxt" ValidationGroup="M1"
                                    TextMode="MultiLine"></asp:TextBox>
                            </td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtRemarks"
                                Display="Dynamic" ErrorMessage="Field Can't be Empty" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Status
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:CheckBox ID="chkActive" runat="server" TabIndex="2" />
                            </td>
                            <td align="left" valign="top">
                            </td>
                        </tr>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td">
            <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select" AutoPostBackOnSelect="True">
                <Columns>
                    <cc2:Column DataField="LV_ID" HeaderText="LeaveId" Visible="false" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_NAME" HeaderText="Leave Name" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_DAY" HeaderText="No of Days" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="YEAR" HeaderText="Year" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="REMARKS" HeaderText="Remarks" Width="150px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_FRWD" HeaderText="Carring Forward" Width="70px">
                    </cc2:Column>
                    <cc2:Column DataField="LV_PRD_TYPE" HeaderText="Period Type" Visible="false" Width="100px">
                    </cc2:Column>
                </Columns>
            </cc2:Grid>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%" class="td">
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
        </td>
    </tr>
</table>
