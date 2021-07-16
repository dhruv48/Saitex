<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Wast_Receiving_Approval.aspx.cs" Inherits="Module_Waste_Pages_Wast_Receiving_Approval" %>
<%@ Register src="../Controls/Waste_Rec_Approval.ascx" tagname="Waste_Rec_Approval" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Waste_Rec_Approval ID="Waste_Rec_Approval1" runat="server" />
</asp:Content>