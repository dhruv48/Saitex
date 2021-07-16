<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="WasteMasterQuery.aspx.cs" Inherits="Module_Waste_Queries_WasteMasterQuery"
    Title="Untitled Page" %>

<%@ Register Src="../Controls/WasteMasterQry.ascx" TagName="ItemMasterQry" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:ItemMasterQry ID="ItemMasterQuery1" runat="server" />
</asp:Content>
