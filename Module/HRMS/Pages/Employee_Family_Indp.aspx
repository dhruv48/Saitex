<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Employee_Family_Indp.aspx.cs" Inherits="Module_HRMS_Pages_Employee_Family_Indp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<%@ Register src="../Controls/Employee_Family.ascx" tagname="Employee_Family" tagprefix="uc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Family Dependent</title>
     <link rel="stylesheet" type="text/css" href="~/StyleSheet/CommonStyle.css" />
    <link rel="stylesheet" type="text/css" href="~/StyleSheet/cssdesign.css" />
     <script language="javascript" type="text/javascript">
    function checkNumeric(evt) {
    evt = (evt) ? evt : window.event
    var charCode = (evt.which) ? evt.which : evt.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57))
    {      
        return false
    }   
         return true
    }

    </script>
</head>
<body>
    <form id="form1" runat="server">  
     <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager> 
     <uc1:Employee_Family ID="Employee_Family1" runat="server" />   
    </form>
</body>
</html>
