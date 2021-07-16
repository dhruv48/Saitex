<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="MachineGroup.aspx.cs" Inherits="Module_Machine_Pages_MachineGroup" Title="Untitled Page" %>

<%@ Register src="../Controls/Machine_Master.ascx" tagname="Machine_Master" tagprefix="uc1" %>

<%@ Register src="../Controls/MachineGroup.ascx" tagname="MachineGroup" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <uc2:MachineGroup ID="MachineGroup1" runat="server" />
    
</asp:Content>

