<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchTrn.aspx.cs" Inherits="Inventory_SearchTrn" %>

<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <script language="javascript" type="text/javascript">
    function BindTrnMst(code,MstCodeId)
    {          
        window.opener.document.getElementById(MstCodeId).value=code;       
       window.opener.document.forms[0].submit();      
         window.close();
    } 
    </script>
   
     <style type="text/css">
      
        .item
        {
            position: relative !important;
            display:-moz-inline-stack;
            display:inline-block;
            zoom:1;
            *display:inline;
            overflow: hidden;
            white-space: nowrap;
        }
        
        .header
        {
            margin-left: 2px;
        }
   
        .c1
        {
            width: 120px;
        }
        
        .c2
        {
            margin-left: 4px;
            width: 180px;
        }
        
        .c3
        {
            margin-left: 4px;
            width: 300px;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
        <asp:PlaceHolder runat="server" ID="ComboBox1Container" />
    
            &nbsp;</div>
    </form>
</body>
</html>
