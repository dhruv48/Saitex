<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="CRForYarnSpningQuery.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Queries_CRForYarnSpningQuery" %>

<%@ Register src="../Controls/CR4YarnSpningQuery.ascx" tagname="CR4YarnSpningQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CR4YarnSpningQuery ID="CR4YarnSpningQuery1" runat="server" />
</asp:Content>

