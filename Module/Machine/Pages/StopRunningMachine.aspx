<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="StopRunningMachine.aspx.cs" Inherits="Module_Machine_Pages_StopRunningMachine" Title="Untitled Page" %>

<%@ Register src="~/Module/Machine/Controls/StopRunningMachine.ascx" tagname="StopRunningMachine" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:StopRunningMachine ID="StopRunningMachine1" runat="server" />
</asp:Content>

