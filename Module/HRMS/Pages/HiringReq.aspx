<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="HiringReq.aspx.cs" Inherits="Module_HRMS_Pages_HiringReq" Title="Untitled Page" %>

<%@ Register src="../Controls/HiringRequest.ascx" tagname="HiringRequest" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:HiringRequest ID="HiringRequest1" runat="server" />
</asp:Content>

