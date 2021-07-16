<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="YarnItemStockAgeing.aspx.cs" Inherits="Module_Yarn_SalesWork_Reports_YarnItemStockAgeing" %>

<%@ Register src="../Controls/YarnStockAgeing_Opt.ascx" tagname="YarnStockAgeing_Opt" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YarnStockAgeing_Opt ID="YarnStockAgeing_Opt1" runat="server" />
</asp:Content>

