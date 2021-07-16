<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FiberLRApproval.aspx.cs" Inherits="Module_OrderDevelopment_LabDip_Pages_FiberLRApproval" %>
<%@ Register src="../Controls/FiberLRApproval.ascx" tagname="FiberLRApproval" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
     <uc1:FiberLRApproval ID="FiberLRApproval" runat="server" />
</asp:Content>

