<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/CommonMaster/admin.master" CodeFile="PO_Auth.aspx.cs" Inherits="Module_Fiber_Pages_PO_Auth" %>


<%@ Register src="../Controls/PO_Approval.ascx" tagname="PO_AUTH" tagprefix="uc1" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PO_AUTH ID="PO_AUTH1" runat="server" />
</asp:Content>
