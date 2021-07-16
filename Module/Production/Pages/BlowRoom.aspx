<%@ Page Title="" Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="BlowRoom.aspx.cs" Inherits="Module_Production_Pages_BlowRoom" %>

<%@ Register src="../Controls/BlowRoom.ascx" tagname="BlowRoom" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <p>
        <uc1:BlowRoom ID="BlowRoom1" runat="server" />
    </p>
</asp:Content>

