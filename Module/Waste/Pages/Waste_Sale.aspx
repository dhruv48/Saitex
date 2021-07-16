<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Waste_Sale.aspx.cs" Inherits="Module_Waste_Pages_Waste_Sale" %>

<%@ Register src="../Controls/Waste_Sale.ascx" tagname="Waste_Sale1" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Waste_Sale1 ID="Waste_Sale1" runat="server" />
</asp:Content>