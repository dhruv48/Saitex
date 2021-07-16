<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FileUploading.aspx.cs" Inherits="Module_FileManagement_Pages_FileUploading" Title="Untitled Page" %>

<%@ Register src="../Controls/FileUpload.ascx" tagname="FileUpload" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FileUpload ID="FileUpload1" runat="server" />
</asp:Content>

