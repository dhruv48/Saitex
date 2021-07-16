<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HRMSPendingForApproval.ascx.cs" Inherits="Module_HRMS_Controls_HRMSPendingForApproval" %>
<table width="100%" align="left" class="tContentArial">
       <tr id="trLeave" runat="server" >
        <td align="left" width="100%">
            <asp:Label ID="LblLeaveApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="LnkLeaveApproval" runat="server" EnableTheming="True" PostBackUrl="~/Module/HRMS/Pages/Employee_Request_for_Leave.aspx" >Click 
            Here</asp:LinkButton>
        </td>
    </tr>
    <tr id="trOD" runat="server">
        <td align="left" width="100%">
            <asp:Label ID="LblODApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkODApproval" runat="server" EnableTheming="True"  PostBackUrl="~/Module/HRMS/Pages/ODApplovalForm.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr id="trLOAN" runat="server">
        <td align="left" width="100%">
            <asp:Label ID="LblLoanApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkLoanApproval" runat="server"  EnableTheming="True" PostBackUrl="~/Module/HRMS/Pages/EmpLoanRequest.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>   
</table><p>
   </p>
