<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="HeadNameMaster.aspx.cs" Inherits="Module_HRMS_Pages_HeadNameMaster" Title="Head Name Master" %>
   

<%@ Register src="../Controls/HeadNameMaster.ascx" tagname="HeadNameMaster" tagprefix="uc2" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">

                                  

    <uc2:HeadNameMaster ID="HeadNameMaster1" runat="server" />

                                  

</asp:Content>