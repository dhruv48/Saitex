<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchVendor.aspx.cs" Inherits="Inventory_SearchVendor" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Vendor</title>

    <script language="javascript" type="text/javascript">
    function BindPartyCode(code,PartyCodeId)
    {     
        window.opener.document.getElementById(PartyCodeId).value=code;
       
   window.opener.document.forms[0].submit();      
             window.close();
    } 
    </script>

    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
     
    
    </form>
</body>
</html>
