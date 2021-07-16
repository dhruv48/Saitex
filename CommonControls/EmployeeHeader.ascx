<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeHeader.ascx.cs" Inherits="CommonControls_EmployeeHeader" %>
<table width="98%" align="center" height="83" border="0" cellpadding="0" cellspacing="0">
    <tr>
        <td rowspan="2" width="150px" height="83" align="right" valign="top"  bgcolor="#336799">
            <asp:Image ID="Image1" ImageUrl="~/CommonImages/logo.jpg" Width="147" Height="83"
                runat="server" />           
        </td>
        <td align="right" valign="bottom"  bgcolor="#336799">
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
                                    <img src="../CommonImages/home.jpg" width="21" height="21" />
                                </td>
                                <td align="right" valign="top">
                                    &nbsp;
                                </td>
                                <td align="right" valign="top">
                                    <img src="../CommonImages/contactus.jpg" width="23" height="19" />
                                </td>
                                <td align="right" valign="top">
                                    &nbsp;
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
                                    <strong>Welcome :
                                        <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                        ||<asp:LinkButton ID="lbtnLogOut" runat="server" Text="Logout" 
                                        CssClass="logout" onclick="lbtnLogOut_Click"></asp:LinkButton>
                                        </strong>
                                </td>                                
                                <td id="tdChangePwd" runat="server" visible="false"  align="right" class="logintext" valign="top" style="height: 38px">
                                      <strong>||<asp:LinkButton ID="LbtChangePassword" runat="server" Text="Change Password" 
                                        CssClass="logout" onclick="LbtChangePassword_Click" ></asp:LinkButton></strong>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>