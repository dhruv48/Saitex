<%@ Page Language="C#" MasterPageFile="~/CommonMaster/UserMaster.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" Title="Login Page" %>
<%@ Register assembly="obout_Interface" namespace="Obout.Interface" tagprefix="cc2" %>
<asp:Content ContentPlaceHolderID="cphBody" runat="server">
    <br />
    <br />
    <br />
    <br />
    <table width="374" border="0" cellpadding="2" cellspacing="0" bgcolor="#3871A9">
        <tr>
        
            <td height="19" align="center" colspan="3" bgcolor="#AFCAE4">
                <span class="wel" ><strong>Welcome&nbsp; !&nbsp; Textiles Application Management System </strong></span>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="3" bgcolor="#AFCAE4">
                <br />
            </td>
        </tr>
        <tr>
            <td height="20" colspan="3" align="center" bgcolor="#214263" class="copy">
                <strong class="logintext">Login Here </strong>
            </td>
        </tr>
        <tr>
            <td class="formtext">
                &nbsp;
            </td>
            <td class="formtext">
                &nbsp;
            </td>
            <td align="left" class="copy">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center" valign="top">
                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessageLogin"></asp:Label>
                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
            </td>
        </tr>
        <tr>
            <td width="41" class="formtext">
                &nbsp;
            </td>
            <td width="59" align="left" class="formtext">
                Login ID :
            </td>
            <td width="260" align="left" class="copy">
                <cc2:oboutTextBox ID="txtLoginName" runat="server" Width="150px" CssClass="gCtrTxt" MaxLength="50"
                    TabIndex="1"></cc2:oboutTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLoginName"
                    Display="Dynamic" ErrorMessage="Pls Enter Login Id !" BackColor="White"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="formtext">
                &nbsp;
            </td>
            <td class="formtext">
                &nbsp;
            </td>
            <td align="left" class="copy">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="formtext">
                &nbsp;
            </td>
            <td align="left" class="formtext">
                Password:
            </td>
            <td align="left" class="copy">
                <cc2:oboutTextBox ID="txtPassword" TextMode="Password" runat="server" Width="150px" CssClass="gCtrTxt"
                    MaxLength="10" TabIndex="2"></cc2:oboutTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPassword"
                    Display="Dynamic" ErrorMessage="Pls Enter Password !" BackColor="White"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="formtext">
                &nbsp;
            </td>
            <td colspan="2" align="left" class="formtext">
                <%--<span class="forgotpassword">Forgot Password</span>--%>
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <asp:ImageButton ID="imgButtonLogin" runat="server" ImageUrl="./CommonImages/buttonlogin.jpg"
                    OnClick="imgButtonLogin_Click" TabIndex="3" />
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>
