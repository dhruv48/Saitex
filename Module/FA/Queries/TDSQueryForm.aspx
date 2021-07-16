<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TDSQueryForm.aspx.cs" Inherits="Module_FA_Queries_TDSQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/TDSQuery.ascx" tagname="TDSQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TDSQuery ID="TDSQuery1" runat="server" />
</asp:Content>

