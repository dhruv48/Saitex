<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="FIBER_MST_NEW.aspx.cs" Inherits="Module_Fiber_Pages_FIBER_MST_NEW"  %>

<%@ Register src="../Controls/FIBER_MASTER_NEW.ascx" tagname="FIBER_MASTER_NEW" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:FIBER_MASTER_NEW ID="FIBER_MASTER_NEW1" runat="server" />
</asp:Content>

