<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="PositionMaster.aspx.cs" Inherits="Module_HRMS_Pages_PositionMaster" Title="Untitled Page" %>

<%@ Register src="../Controls/PositionMaster.ascx" tagname="PositionMaster" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PositionMaster ID="PositionMaster1" runat="server" />
</asp:Content>

