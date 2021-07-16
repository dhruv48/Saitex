<%@ Page Language="C#" Async="true"   MaintainScrollPositionOnPostback="true" MasterPageFile="~/CommonMaster/admin.master"
    AutoEventWireup="true" CodeFile="CustomerRequestApproval.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustomerRequestApproval"
     Title="sainath Texport Ltd......." %>

<%@ Register Src="../Controls/CustomerRequestApproval.ascx" TagName="CustomerRequestApproval"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:CustomerRequestApproval ID="CustomerRequestApproval1" runat="server" />
</asp:Content>
