<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AprovalPendings.ascx.cs" Inherits="CommonControls_AprovalPendings" %>
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="center"  width="100%">
            <asp:Label ID="LabelHeading" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="#663300">Inventory Pending Approvals</asp:Label>
        </td>
    </tr>
    <tr>
        <td  align="left" width="100%">
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
            <asp:Label ID="lblIndentApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkIndentApproval" runat="server" EnableTheming="True"  Visible="false" 
                PostBackUrl="~/Module/Inventory/Pages/MaterialIndentApproval.aspx" >Click 
            Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblIndentPendingForPO" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkIndentPendingForPO" runat="server" EnableTheming="True"  Visible="false" 
                PostBackUrl="~/Module/Inventory/Pages/IndentPendingForPO.aspx" >Click 
            Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblPoApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkPoApproval" runat="server" Visible ="false"  PostBackUrl="~/Module/Inventory/Pages/POApproval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblTransactional" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkTransactional" runat="server" Visible="false" 
                PostBackUrl="~/Module/Inventory/Pages/ReceiptApproval.aspx?TRN_TYPE='RMS01', 'RMS02', 'RMS03', 'RMS04', 'RMS05','RMS11', 'RMS06', 'RMS30', 'GRM01','IMS05'">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblIssueApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkIssueApproval" runat="server" Visible="false" 
                PostBackUrl="~/Module/Inventory/Pages/ReceiptApproval.aspx?TRN_TYPE='IMS06','IMS07','IMS01','IMS02','IMS03','IMS04','IMS11','GIM01'">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblYarnIndent" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkYarnIndent" runat="server" Visible="false" 
                PostBackUrl="~/Module/Yarn/SalesWork/Pages/Yarn_Indent_Approval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
     <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblApprovedIndentPendingForPO" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkApprovedIndentPendingForPO" runat="server" Visible="false" 
                >Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblYarnPOIndent" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkYarnPOIndent" runat="server" Visible="false" 
                PostBackUrl="~/Module/Yarn/SalesWork/Pages/Po_Approval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblYarnTransactional" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkYarnTransactional" runat="server" Visible="false" PostBackUrl="~/Module/Yarn/SalesWork/Pages/Receipt_Approval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblFabricIndentApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkFabricIndentApproval" runat="server" Visible="false" 
                PostBackUrl="~/Module/Fabric/FabricSaleWork/Pages/Fabric_Indent_Approval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblFabricPOApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkFabricPOApproval" runat="server" Visible="false" 
                PostBackUrl="~/Module/Fabric/FabricSaleWork/Pages/FabricPOApproval.aspx">Click 
            Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblFabricTransactionApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkFabricTransactionApproval" runat="server" Visible="false"
                
                PostBackUrl="~/Module/Fabric/FabricSaleWork/Pages/FabricReceiptApproval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
     <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblFiberIndentApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkFiberIndentApproval" runat="server" Visible="false" 
                PostBackUrl="~/Module/Fiber/Pages/Fiber_IndentApproval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
     <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblFiberPOApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkFiberPOApproval" runat="server" Visible="false" 
                PostBackUrl="~/Module/Fiber/Pages/PO_Approval.aspx">Click 
            Here</asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td align="left" width="100%">
            <asp:Label ID="lblFiberTransactionApproval" runat="server" Text=""></asp:Label>
            <asp:LinkButton ID="lnkFiberTransactionApproval" runat="server" Visible="false"
                
                PostBackUrl="~/Module/Fiber/Pages/Fiber_TranApproval.aspx">Click Here</asp:LinkButton>
        </td>
    </tr>
</table>
