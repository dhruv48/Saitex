<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dept_group_trn.ascx.cs"
    Inherits="Module_Admin_Controls_Dept_group_trn" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
            <tr>
                <td class="td" align="left">
                    <table cellpadding="0" cellspacing="0" class="tContentArial" border="0" align="left">
                        <tr>
                            <td id="tdUpdate" runat="server" width="48" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" Width="48" Height="41"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1"
                                    OnClientClick="if (!confirm('Are you sure to Update Record ?')) { return false; }">
                                </asp:ImageButton>
                            </td>
                            <td width="48" align="left">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you want to Clear ?')) { return false; }">
                                </asp:ImageButton>
                            </td>
                            <td width="48" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print Record ?')) { return false; }">
                                </asp:ImageButton>
                            </td>
                            <td width="48" align="left">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" Width="48" Height="41" OnClientClick="if (!confirm('Are you want to Exit ?')) { return false; }">
                                </asp:ImageButton>
                            </td>
                            <td width="48" align="left">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center">
                    <span class="titleheading">Department Group Integration</span>
                </td>
            </tr>
            <tr>
                <td class="td" align="left" valign="top">
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ValidationGroup="M1" ShowSummary="False" />
                    <strong></strong><strong></strong>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td align="right" valign="top">
                                *Select Department Group
                            </td>
                            <td>
                                <b>:</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlDeptGroup" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlDeptGroup_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Select Department
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:Panel ID="pnlDeptGroup" runat="server" ScrollBars="Auto" Height="400px">
                                    <asp:CheckBoxList ID="chklstDept" runat="server" DataTextField="DEPT_NAME" DataValueField="DEPT_CODE">
                                    </asp:CheckBoxList>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" align="center" valign="top">
                    <table class="tContentArial" cellspacing="0" cellpadding="0" width="100%" align="center"
                        border="0">
                        <tbody>
                            <tr>
                                <td align="left" valign="top">
                                    <br />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
