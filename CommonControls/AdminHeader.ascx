<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminHeader.ascx.cs" Inherits="CommonControls_AdminHeader" %>
<table width="98%" align="center" height="83" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td rowspan="2" width="150px" height="83" align="right" valign="top" bgcolor="#336799">
            <asp:ImageButton ID="Image1" ImageUrl="~/CommonImages/logo.jpg" Width="147" Height="83"
                runat="server" PostBackUrl="~/Module/Admin/Pages/Welcome.aspx" />
        </td>
        <td align="right" valign="bottom" bgcolor="#336799">
            <table width="98%" border="0" cellpadding="4" cellspacing="0">
                <tr>
                    <td rowspan="2" align="left" valign="top">
                        <span class="saitex">
                            <asp:Label ID="logCompany" runat="server"></asp:Label></span>
                    </td>
                    <td align="right" bgcolor="#336799" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" class="logout">
                                    Financial Year :
                                </td>
                                <td align="right" class="logout">
                                    <asp:Label ForeColor="white" ID="logFinYear" Visible="true" runat="server"></asp:Label>
                                </td>
                                <td align="right" class="logout">
                                    &nbsp;&nbsp;&nbsp;&nbsp; Branch :
                                </td>
                                <td align="right" class="logout">
                                    <asp:Label ForeColor="white" ID="logBranch" runat="server"></asp:Label>
                                </td>
                                <td align="right" class="logout">
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Label ForeColor="white" ID="LogInDate" Visible="false" runat="server"></asp:Label>
                                </td>
                                <td align="right" class="logout">
                                    Department :
                                </td>
                                <td align="right" class="logout">
                                    <asp:Label ForeColor="white" ID="logDepartment" runat="server"></asp:Label>
                                </td>
                                <td align="right" class="logout">
                                    <asp:Label ForeColor="white" ID="logInTime" Visible="false" runat="server"></asp:Label>&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td align="right" valign="top">
                                    <asp:Image ID="Image2" runat="server" ImageUrl="~/CommonImages/home.jpg" Width="21"
                                        Height="21" />
                                </td>
                                <td align="right" valign="top">
                                    &nbsp;
                                </td>
                                <td align="right" valign="top">
                                    <asp:Image ID="Image3" runat="server" ImageUrl="~/CommonImages/contactus.jpg" Width="21"
                                        Height="21" />
                                </td>
                                <td align="right" valign="top">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td align="right" colspan="2" class="logout">
                                    Current Opening Month :
                                </td>
                                <td align="right" class="logout">
                                    <asp:Label ForeColor="white" ID="LblOpeningMonth" Visible="true" runat="server"></asp:Label>
                                </td>
                                <td>
                                </td>
                                <td align="right" colspan="2" class="logout">
                                    &nbsp;&nbsp;Current Opening Year :
                                </td>
                                <td align="right" class="logout">
                                    <asp:Label ForeColor="white" ID="LblOpeningYear" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" class="logout" valign="top" style="height: 38px">
                                    <strong>Welcome
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                        |<asp:LinkButton ID="lbtnDashBoard" runat="server" Text="DashBoard" CssClass="logout"
                                            OnClick="lbtnDashBoard_Click"></asp:LinkButton>
                                        |
                                        <asp:LinkButton ID="lbtnChangeCOBRANCH" runat="server" Text="Change Branch/Department"
                                            CssClass="logout" OnClick="lbtnChangeCOBRANCH_Click"></asp:LinkButton>
                                        <%--<a href="../GetUserAuthorisation.aspx" class="logout"><span style="font-size: 8pt">
                                            Change Branch/Department</span></a>--%>
                                        |<asp:LinkButton ID="lbtnSearchPeople" runat="server" Text="Search People" CssClass="logout"
                                            OnClick="lbtnSearchPeople_Click"></asp:LinkButton>
                                        |
                                        <asp:LinkButton ID="lbtnLogOut" runat="server" Text="Logout" CssClass="logout" OnClick="lbtnLogOut_Click"></asp:LinkButton>
                                        <%--<a href="../Logout.aspx" class="logout"><b>Logout</b>
                                            </a>--%></strong>
                                </td>
                                <td align="right" class="logintext" valign="top" style="height: 38px">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
