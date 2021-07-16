<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master"  AutoEventWireup="true" CodeFile="Waste_Receving.aspx.cs" Inherits="Module_Waste_Pages_Waste_Receving" %>

<%@ Register src="../Controls/Waste_MgtReceive.ascx" tagname="Waste_MgtReceive" tagprefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="Head1" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" runat="Server">
    <uc1:Waste_MgtReceive ID="Waste_MgtReceive1" runat="server" />
</asp:Content>