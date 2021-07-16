<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Loose_Packed_Yarn_Reciept.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Loose_Packed_Yarn_Reciept" %>
<%@ Register src="../Controls/PackedYarnReciept.ascx" tagname="ReceiptCredit" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceiptCredit ID="ReceiptCredit1" runat="server" />
</asp:Content>