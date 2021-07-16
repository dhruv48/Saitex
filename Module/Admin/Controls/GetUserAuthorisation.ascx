<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GetUserAuthorisation.ascx.cs"
    Inherits="Admin_UserControls_GetUserAuthorisation" %>
    <asp:ScriptManager ID="script1" runat="server"></asp:ScriptManager>
<table border="0" cellpadding="2" cellspacing="0" bgcolor="#3871A9" width="65%">
    <%--<tr>
        <td height="19" align="center" colspan="3" bgcolor="#AFCAE4">
            <span class="wel"><strong>Welcome ! </strong></span>
        </td>
    </tr>--%>
    <tr>
        <td align="center" colspan="3" bgcolor="#AFCAE4">
            <br />
        </td>
    </tr>
    <tr>
        <td height="20" colspan="3" align="center" bgcolor="#214263" class="copy">
            <strong class="logintext">Welcome
                <asp:Label ID="lblUserWelcome" runat="server"></asp:Label></strong>
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
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong><span
                style="color: #0000ff; text-decoration: underline"> </span></strong>
        </td>
    </tr>
    <tr>
        <td class="formtext">
            &nbsp;
        </td>
        <td align="right" class="formtext tdRight">
            Company :
        </td>
        <td align="left" class="copy">
            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged"
                AppendDataBoundItems="True" Width="200px" CssClass="tContentArial">
                <asp:ListItem>Select Company</asp:ListItem>
            </asp:DropDownList>
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
        <td class="formtext tdRight">
            &nbsp;
        </td>
        <td class="formtext tdRight">
            Branch :
        </td>
        <td align="left" class="copy">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlBranch" runat="server" AppendDataBoundItems="True" Width="200px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" CssClass="tContentArial">
                        <asp:ListItem>Select Branch</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class=" tdRight">
            &nbsp;
        </td>
        <td class=" tdRight">
            &nbsp;
        </td>
        <td align="left" class="copy">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="formtext tdRight">
        </td>
        <td class="formtext tdRight">
            Financial Year :
        </td>
        <td align="left" class="copy">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddlFinYear" runat="server" AppendDataBoundItems="True" Width="200px"
                        CssClass="tContentArial">
                        <asp:ListItem>Select Financial Year</asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBranch" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td class="tdRight">
            &nbsp;
        </td>
        <td class="tdRight">
            &nbsp;
        </td>
        <td align="left" class="copy">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="formtext tdRight">
        </td>
        <td class="formtext tdRight">
            Department :
        </td>
        <td align="left" class="copy">
            <asp:DropDownList ID="ddlDepartment" runat="server" AppendDataBoundItems="True" Width="200px"
                CssClass="tContentArial">
                <asp:ListItem>Select Department</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="formtext tdRight" style="height: 13px">
        </td>
        <td align="left" class="formtext" style="height: 13px">
        </td>
        <td align="left" class="copy" style="height: 13px">
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center">
            &nbsp;<asp:ImageButton ID="imgbtnGetAccess" runat="server" ImageUrl="~/CommonImages/conect.jpg"
                OnClick="imgbtnGetAccess_Click" />
            <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/exit.jpg"
                OnClick="imgbtnExit_Click" Style="text-align: center" />
        </td>
    </tr>
    <tr>
        <td colspan="3" align="center" class="logout" valign="top">
        </td>
    </tr>
</table>
