<%@ Page Title="" Language="C#" Async="true" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Pack_ApprByFG.aspx.cs" Inherits="Module_Production_Pages_Pack_ApprByFG" %>

<%@ Register src="../Controls/PackApprByFG.ascx" tagname="PackApprByFG" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PackApprByFG ID="PackApprByFG1" runat="server" />
</asp:Content>

