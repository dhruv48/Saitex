<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Mobile_Allotment.aspx.cs" Inherits="Module_HRMS_Pages_Mobile_Allotment" Title="Untitled Page" %>
<%@ Register src="../Controls/Telephone_Allotment.ascx" tagname="Telephone_Allotment" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">

    <uc1:Telephone_Allotment ID="Telephone_Allotment1" runat="server" />

</asp:Content>

