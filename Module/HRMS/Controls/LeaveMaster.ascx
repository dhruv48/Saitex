<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LeaveMaster.ascx.cs" Inherits="Module_HRMS_Controls_LeaveMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
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
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
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
                <td id="tdDelete" runat="server">
                    <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                        Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                        TabIndex="6" OnClick="imgbtnDelete_Click2"></asp:ImageButton>
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
            <span class="titleheading">Leave Master</span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                ShowSummary="False" ValidationGroup="M1" />
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
                            Leave Name
                        </td>
                        <td align="center" valign="top">
                            <b>:</b>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtLeaveName" runat="server" Width="180px" CssClass="UpperCase"
                                ValidationGroup="M1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLeaveName"
                                Display="Dynamic" ErrorMessage="Pls.Enter Leave Name" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <td align="left" valign="top">
                                <cc1:ComboBox ID="ddlLeave" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlLeave_LoadingItems" DataTextField="LV_NAME" DataValueField="LV_ID"
                                    Width="150px" MenuWidth="350px" Height="200px" CssClass="SmallFont" TabIndex="1"
                                    EmptyText="Find Leave" OnSelectedIndexChanged="ddlLeave_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Leave Code</div>
                                        <div class="header c2">
                                            Leave Name</div>
                                        <div class="header c3">
                                            Priority</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("LV_ID") %>' /></div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("LV_NAME") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("LV_PRIORITY") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                            </td>
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
                            <asp:TextBox ID="txtRemarks" runat="server" Width="180px" CssClass="gCtrTxt" ValidationGroup="M1"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <td align="left" valign="top">
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top">
                            Priority
                        </td>
                        <td align="center" valign="top">
                            <b>:</b>
                        </td>
                        <td align="left" valign="top">
                            <asp:TextBox ID="txtPriority" runat="server" Width="50px" ValidationGroup="M1"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPriority"
                                Display="Dynamic" ErrorMessage="Field Can't be Empty" ValidationGroup="M1"></asp:RequiredFieldValidator>
                        </td>
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
                            <td align="left" valign="top">
                            </td>
                        </td>
                    </tr>
                </td>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <td align="left">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" 
                        PageSize="5" AutoGenerateColumns="False" OnSelect="Grid1_Select">
                        <Columns>
                            <cc2:Column DataField="LV_ID" HeaderText="LeaveId" Visible="false" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="LV_NAME" HeaderText="Leave Name" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="REMARKS" HeaderText="Remarks" Width="160px">
                            </cc2:Column>
                            <cc2:Column DataField="LV_PRIORITY" HeaderText="Priority" Width="100px">
                            </cc2:Column>
                        </Columns>
                    </cc2:Grid>
                </td>
            </table>
        </td>
    </tr>
</table>
</ContentTemplate>
</asp:UpdatePanel>
