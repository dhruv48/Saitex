<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MachineUtility.aspx.cs" Inherits="Module_Machine_Pages_MachineUtility" Title="Untitled Page" %>

<%@ Register src="../Controls/MachineUtility.ascx" tagname="MachineUtility" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MachineUtility ID="MachineUtility1" runat="server" />
</asp:Content>

