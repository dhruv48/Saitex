<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AdvancedAdviceApproval.aspx.cs" Inherits="Module_FA_Pages_AdvancedAdviceApproval" Title="Untitled Page" %>

<%@ Register src="../Controls/AdvancedAdviceApproved.ascx" tagname="AdvancedAdviceApproved" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:AdvancedAdviceApproved ID="AdvancedAdviceApproved1" runat="server" />
</asp:Content>

