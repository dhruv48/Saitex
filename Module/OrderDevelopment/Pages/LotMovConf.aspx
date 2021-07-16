<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LotMovConf.aspx.cs" Inherits="Module_OrderDevelopment_Pages_LotMovConf" Title="Untitled Page" %>

<%@ Register src="../Controls/Lot_Movemement_Conf.ascx" tagname="Lot_Movemement_Conf" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Lot_Movemement_Conf ID="Lot_Movemement_Conf1" runat="server" />
</asp:Content>

