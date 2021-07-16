<%@ Page Language="C#" MasterPageFile="~/CommonMaster/admin.master" AutoEventWireup="true"
    CodeFile="Attendance.aspx.cs" Inherits="Module_HRMS_Pages_Attendance" Title="Attendance" %>

<%@ Register src="../Controls/Attendance.ascx" tagname="Attendance" tagprefix="uc1" %>

<asp:Content ID="Content1" runat="server" contentplaceholderid="cphBody">

                                  

                                  

    <uc1:Attendance ID="Attendance1" runat="server" />

                                  

                                  

</asp:Content>
