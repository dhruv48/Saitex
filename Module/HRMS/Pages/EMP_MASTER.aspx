﻿<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="EMP_MASTER.aspx.cs" Inherits="Module_HRMS_Pages_EMP_MASTER" Title="Untitled Page" %>

<%@ Register src="../Controls/EMP_MASTER.ascx" tagname="EMP_MASTER" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Head1" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphBody" Runat="Server">
    <uc1:EMP_MASTER ID="EMP_MASTER1" runat="server" />
</asp:Content>

