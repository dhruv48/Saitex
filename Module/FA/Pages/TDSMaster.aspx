<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="TDSMaster.aspx.cs" Inherits="Module_FA_Pages_TDSMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/TDS.ascx" tagname="TDS" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:TDS ID="TDS1" runat="server" />
</asp:Content>

