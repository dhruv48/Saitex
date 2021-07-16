<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DashBoard.ascx.cs" Inherits="CommonControls_DashBoard" %>
<%@ Register Src="AprovalPendings.ascx" TagName="AprovalPendings" TagPrefix="uc1" %>

<%@ Register Src="FAConfirmAndApproval.ascx" TagName="FAConfirmAndApproval" TagPrefix="uc3" %>
<%@ Register src="../Module/HRMS/Controls/HRMSPendingForApproval.ascx" tagname="HRMSPendingForApproval" tagprefix="uc2" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td class="td" align="center" width="100%">
            <asp:Label ID="LabelHeading" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#663300">DashBoard</asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td" align="center" width="100%">
            <asp:Label ID="lblWelcomeMsg" runat="server" Font-Bold="True" Font-Size="Medium"
                ForeColor="#663300"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="td" align="center" width="100%">
            <asp:Label ID="lblNoMail" runat="server"></asp:Label>
            <asp:DataList ID="dlmailDetail" Width="100%" runat="server">
                <ItemTemplate>
                    <table width="100%">
                        <tr>
                            <td colspan="2" class="tdLeft" width="100%">
                                <asp:Label ID="lblEmailAddress" runat="server" Text='<%# Eval("EMAIL_ADD") %>' Font-Bold="True"
                                    Font-Italic="False" Font-Names="Arial" Font-Size="Medium" ForeColor="#FF9933"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdLeft" width="10%">
                                <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                            </td>
                            <td class="tdLeft" width="90%">
                                You have Total
                                <asp:Label ID="lblInboxTotalDetail" runat="server" Text='<%# Eval("TOTAL_MAILS") %>'></asp:Label>
                                Mails. out of which
                                <asp:Label ID="lblInboxUnReadDetail" runat="server" Text='<%# Eval("UNREAD_MAILS") %>'
                                    Font-Bold="True"></asp:Label>
                                unread
                                <asp:LinkButton ID="lbtnShowInbox" PostBackUrl="~/Module/Mail/Pages/Inbox.aspx" runat="server">Show 
                                Inbox</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </td>
    </tr>
    <tr>
        <td class="td" align="center" width="100%">
            <uc1:AprovalPendings ID="AprovalPendings1" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="td" align="center" width="100%">
            <uc3:FAConfirmAndApproval ID="FAConfirmAndApproval1" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="td" align="center" width="100%">
            
        
            <uc2:HRMSPendingForApproval ID="HRMSPendingForApproval1" runat="server" />
        </td>
    </tr>
</table>
