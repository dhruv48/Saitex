<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="OnLineSaleBillReceive.aspx.cs" Inherits="Module_FA_Pages_OnLineSaleBillReceive" Title="Untitled Page" %>

<%@ Register src="../Controls/SaleBillReceive.ascx" tagname="SaleBillReceive" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:SaleBillReceive ID="SaleBillReceive1" runat="server" />
</asp:Content>

