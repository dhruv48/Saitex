<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="yrnwipstockreport.aspx.cs" Inherits="Module_Production_Pages_yrnwipstockreport" Title="Untitled Page" %>

<%@ Register src="../Controls/YRN_WIP_STOCK_REPORT.ascx" tagname="YRN_WIP_STOCK_REPORT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:YRN_WIP_STOCK_REPORT ID="YRN_WIP_STOCK_REPORT1" runat="server" />
</asp:Content>

