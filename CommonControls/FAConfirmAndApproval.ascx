<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FAConfirmAndApproval.ascx.cs"
    Inherits="CommonControls_FAConfirmAndApproval" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="center" width="100%">
            <asp:Label ID="LabelHeading" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#663300">Finance Pending Confirmation And Approvals</asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblWelcomeMsg" runat="server" Font-Bold="True" Font-Size="Medium"
                ForeColor="#663300"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="Center" width="100%">
            <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblVoucherConfirmation" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkbtnVoucherConfirmation" runat="server" EnableTheming="True"
                Visible="false" PostBackUrl="~/Module/FA/Pages/VoucherConfirmation.aspx">Click 
            Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblVoucherApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkbtnVoucherApproval" runat="server" Visible="false" PostBackUrl="~/Module/FA/Pages/VoucherApproval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblAdviceConfirmation" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkbtnAdviceConfirmation" runat="server" Visible="false" PostBackUrl="~/Module/FA/Pages/AdvancedAdviceConfirmation.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblAdviceApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkbtnAdviceApproval" runat="server" Visible="false" PostBackUrl="~/Module/FA/Pages/AdvancedAdviceApproval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
</table>
