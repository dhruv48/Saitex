<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ViewUploadedFile.aspx.cs" Inherits="Module_FileManagement_Pages_ViewUploadedFile" Title="Untitled Page" %>

<%@ Register src="../Controls/ViewUploaded.ascx" tagname="ViewUploaded" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ViewUploaded ID="ViewUploaded1" runat="server" />
</asp:Content>

