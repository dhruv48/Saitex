<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ContractMaster.aspx.cs" Inherits="Module_FA_Pages_ContractMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/ContractMst.ascx" tagname="ContractMst" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ContractMst ID="ContractMst1" runat="server" />
</asp:Content>

