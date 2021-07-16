<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true" CodeFile="LeaveMasterQuery.aspx.cs" Inherits="Module_HRMS_Queries_LeaveMasterQuery" Title="Untitled Page" %>

<%@ Register src="../Controls/LeaveMaster.ascx" tagname="LeaveMaster" tagprefix="uc1" %>

<%@ Register src="../Controls/LeaveMasterQuery.ascx" tagname="LeaveMasterQuery" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">

    

    <uc2:LeaveMasterQuery ID="LeaveMasterQuery1" runat="server" />

    

</asp:Content>


