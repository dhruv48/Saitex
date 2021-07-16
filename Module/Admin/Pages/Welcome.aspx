<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Welcome.aspx.cs" Inherits="Admin_Welcome" Title="Welcome" %>

<%@ Register Src="~/Module/Admin/Controls/WelcomeUser.ascx" TagName="WelcomeUser"
    TagPrefix="uc1" %>
<asp:Content ID="Head11" runat="server" ContentPlaceHolderID="Head1">
  
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <table bgcolor="white">
        <tr>
            <td align="left" bgcolor="white" valign="top">
                <uc1:WelcomeUser ID="WelcomeUser1" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>
