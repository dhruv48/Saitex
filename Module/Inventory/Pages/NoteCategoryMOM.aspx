<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="NoteCategoryMOM.aspx.cs" Inherits="Module_Inventory_Pages_NoteCategoryMOM" %>

<%@ Register src="../../../CommonControls/Transaction_Of_Master.ascx" tagname="Transaction_Of_Master" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Transaction_Of_Master ID="Transaction_Of_Master1" runat="server" />
</asp:Content>
