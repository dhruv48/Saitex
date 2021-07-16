<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AuthoriseLocationAndStore.ascx.cs" Inherits="Module_Admin_Controls_AuthoriseLocationAndStore" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .csslblMessage
    {
        color: #000000;
    }
    .style2
    {
        height: 228px;
    }
    .td
    {
        text-align: left;
    }
    .titleheading
    {
        text-align: center;
    }
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
    <ContentTemplate>--%>
        <table cellpadding="0" cellspacing="0" class="td" bgcolor="#AFCAE4" class="tContentArial">
            <tr>
                <td>
                    <table align="left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server" ValidationGroup="FA"
                                    ToolTip="Save" ImageUrl="~/CommonImages/save.jpg" OnClientClick="if (!confirm('Are you want to Save ?')) { return false; }"
                                    OnClick="imgbtnSave_Click" />
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    OnClientClick="if (!confirm('Are you want to print ?')) { return false; }" />
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClientClick="if (!confirm('Are you want to Clear ?')) { return false; }" />
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                    OnClick="imgbtnExit_Click" />
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png">
                                </asp:ImageButton>
                            </td>
                            <td valign="top" align="center">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader" colspan="4" class="td">
                    <b class="titleheading">Assign Company to User</b>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" colspan="2" class="td">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="right" valign="top" class="tContentArial" width="50%">
                    *User Name <b>:</b>
                </td>
                <td align="left" valign="top" class="tContentArial" width="50%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txtUserName" runat="server" AutoCompleteType="Disabled" Width="100px"
                                AutoPostBack="True" MaxLength="10" OnTextChanged="txtUserName_TextChanged1" CssClass="TextBox"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="dynamic" runat="server"
                                ErrorMessage="*" ControlToValidate="txtUserName" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            <cc2:ComboBox ID="ddlUserMaster" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                DataTextField="USER_CODE" DataValueField="USER_CODE" EmptyText="Find User" EnableLoadOnDemand="true"
                                Height="200px" MenuWidth="450px" OnLoadingItems="ddlUserMaster_LoadingItems"
                                OnSelectedIndexChanged="ddlUserMaster_SelectedIndexChanged" TabIndex="1" Width="100px">
                                <HeaderTemplate>
                                    <div class="header c1">
                                        User Code</div>
                                    <div class="header c2">
                                        User Name</div>
                                    <div class="header c3">
                                        Login Id</div>
                                    <div class="header c1">
                                        User Type</div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <asp:Literal ID="Container7" runat="server" Text='<%# Eval("USER_CODE") %>' />
                                    </div>
                                    <div class="item c2">
                                        <asp:Literal ID="Container8" runat="server" Text='<%# Eval("USER_NAME") %>' />
                                    </div>
                                    <div class="item c3">
                                        <asp:Literal ID="Container9" runat="server" Text='<%# Eval("USER_LOG_ID") %>' />
                                    </div>
                                    <div class="item c1">
                                        <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("USER_TYPE") %>' />
                                    </div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Displaying items - out of .
                                </FooterTemplate>
                            </cc2:ComboBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td tContentArial">
                    Select Company and Branch
                </td>
                <td align="left" valign="top" class=" td tContentArial">
                    Select Department
                </td>
            </tr>
            <tr>
                <td align="left" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TreeView ID="trvCompanyBranch" runat="server" ShowCheckBoxes="All" AutoGenerateDataBindings="False">
                            </asp:TreeView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUserName" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td align="left" valign="top">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TreeView ID="trvDepartment" runat="server" ShowCheckBoxes="All" AutoGenerateDataBindings="False">
                            </asp:TreeView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="txtUserName" EventName="TextChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
        </table>
   <%-- </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="imgbtnSave" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
--%>