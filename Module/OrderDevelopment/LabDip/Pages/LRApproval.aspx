<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LRApproval.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_LRApproval" Title="Untitled Page" %>
<%@ Register src="../Controls/LRApproval.ascx" tagname="LRApproval" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:LRApproval ID="LRApproval1" runat="server" />
</asp:Content>

