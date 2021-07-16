<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Mobile_Master.aspx.cs" Inherits="Module_HRMS_Pages_Mobile_Master" Title="Untitled Page" %>

<%@ Register src="../Controls/Mobile_Master.ascx" tagname="Mobile_Master" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Mobile_Master ID="Mobile_Master1" runat="server" />
</asp:Content>

