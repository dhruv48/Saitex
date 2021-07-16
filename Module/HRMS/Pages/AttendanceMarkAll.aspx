<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="AttendanceMarkAll.aspx.cs" Inherits="Module_HRMS_Pages_AttendanceMarkAll" Title="Untitled Page" %>

<%@ Register src="../Controls/Attn_Mark_All.ascx" tagname="Attn_Mark_All" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:Attn_Mark_All ID="Attn_Mark_All1" runat="server" />
</asp:Content>

