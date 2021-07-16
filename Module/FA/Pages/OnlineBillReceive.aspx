<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="OnlineBillReceive.aspx.cs" Inherits="Module_FA_Pages_OnlineBillReceive"
    Title="Untitled Page" %>

<%@ Register src="../Controls/BillReceive.ascx" tagname="BillReceive" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:BillReceive ID="BillReceive1" runat="server" />
</asp:Content>
