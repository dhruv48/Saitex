<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Sw_Transactional_Approval.aspx.cs" Inherits="Module_SewingThread_Page_Sw_Transactional_Approval" %>

<%@ Register src="../Controls/SW_TransactionalApproval.ascx" tagname="SW_TransactionalApproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SW_TransactionalApproval ID="SW_TransactionalApproval1" runat="server" />
</asp:Content>

