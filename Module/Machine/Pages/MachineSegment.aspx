<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MachineSegment.aspx.cs" Inherits="Module_Machine_Pages_MachineSegment" Title="Untitled Page" %>

<%@ Register src="../Controls/MachineSegmentOPT.ascx" tagname="MachineSegmentOPT" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:MachineSegmentOPT ID="MachineSegmentOPT1" runat="server" />
</asp:Content>

