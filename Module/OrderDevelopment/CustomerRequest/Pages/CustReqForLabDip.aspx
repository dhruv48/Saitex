<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="CustReqForLabDip.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_CustReqForLabDip"
    Title="Untitled Page" %>
<%@ Register src="../Controls/CustReqForLabDip.ascx" tagname="CustReqForLabDip" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:CustReqForLabDip ID="CustReqForLabDip1" runat="server" />
</asp:Content>
