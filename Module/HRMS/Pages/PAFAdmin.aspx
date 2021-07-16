<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="PAFAdmin.aspx.cs" Inherits="Module_HRMS_Pages_PAFAdmin" %>

<%@ Register Src="../Controls/PAFAdmin.ascx" TagName="PAFAdmin" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:PAFAdmin ID="PAF1" runat="server" />
</asp:Content>
