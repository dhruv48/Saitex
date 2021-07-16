<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" ValidateRequest="false" EnableEventValidation="false" CodeFile="BankMasterQueryForm.aspx.cs" Inherits="Module_FA_Queries_BankMasterQueryForm" Title="Untitled Page" %>

<%@ Register src="../Controls/BankMasterQueryForm.ascx" tagname="BankMasterQueryForm" tagprefix="uc1" %>

<%@ Register src="../Controls/FABankMaster.ascx" tagname="FABankMaster" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cphBody"  Visible="true" Runat="Server">
    <uc1:BankMasterQueryForm ID="BankMasterQueryForm1" runat="server" />
    </asp:Content>

