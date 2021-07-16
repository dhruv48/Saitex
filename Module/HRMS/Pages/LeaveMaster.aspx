<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="LeaveMaster.aspx.cs" Inherits="Module_HRMS_Pages_LeaveMaster" Title="Leave Master" %>
<%@ Register src="../Controls/LeaveMaster.ascx" tagname="LeaveMaster" tagprefix="uc1" %>
<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">

                                    

    <uc1:LeaveMaster ID="LeaveMaster1" runat="server" />

                                    

</asp:Content>
