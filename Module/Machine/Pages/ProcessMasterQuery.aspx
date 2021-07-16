<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="ProcessMasterQuery.aspx.cs" Inherits="Module_Machine_Pages_ProcessMasterQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/ProcessMasterQuery.ascx" tagname="ProcessMasterQuery" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:ProcessMasterQuery ID="ProcessMasterQuery1" runat="server" />
</asp:Content>

