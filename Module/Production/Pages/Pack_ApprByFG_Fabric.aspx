<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="Pack_ApprByFG_Fabric.aspx.cs" Inherits="Module_Production_Pages_Pack_ApprByFG_Fabric" %>

<%@ Register src="../Controls/PackApprByFG_Fabric.ascx" tagname="PackApprByFG_Fabric" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:PackApprByFG_Fabric ID="PackApprByFG_Fabric1" runat="server" />
</asp:Content>

