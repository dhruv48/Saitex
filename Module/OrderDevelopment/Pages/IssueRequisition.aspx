<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="IssueRequisition.aspx.cs" Inherits="Module_OrderDevelopment_Pages_IssueRequisition" %>

<%@ Register src="../Controls/IssueRequisitionl.ascx" tagname="IssueRequisitionl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:IssueRequisitionl ID="IssueRequisitionl1" runat="server" />
</asp:Content>

