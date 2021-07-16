<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Waste_Reciving.aspx.cs" Inherits="Module_Waste_Reports_Waste_Reciving" Title="Untitled Page" %>

<%@ Register src="../Controls/Waste_Reciving_Report.ascx" tagname="ReceiptPermRPT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ReceiptPermRPT ID="ReceiptPermRPT1" runat="server" />
</asp:Content>

