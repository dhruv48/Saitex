<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master"  CodeFile="CRApprovalForWaste.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CRApprovalForWaste" %>

<%@ Register src="../../Controls/CRApprovalforWaste.ascx" tagname="CRApprovalforWaste1" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:CRApprovalforWaste1 ID="CRApprovalforWaste1" runat="server" />
</asp:Content>

