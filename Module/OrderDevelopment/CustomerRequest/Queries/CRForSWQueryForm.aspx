<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRForSWQueryForm.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Queries_CRForSWQueryForm" %>

<%@ Register src="../Controls/CRQueryForm.ascx" tagname="CRQueryForm" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CRQueryForm ID="CRQueryForm1" runat="server" />
</asp:Content>

