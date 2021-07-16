<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SearchPO.aspx.cs" Inherits="Inventory_SearchPO" %>

<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Search Purchase Order</title>

    <script language="javascript" type="text/javascript">
    function BindPO(code,TextBoxId,txtICODE,txtDESC,txtUNIT,txtQTY,txtBasicRate,txtFinalRate,txtAmount,ITEM_CODE,ITEM_DESC,UOM,QTY_REM,BASIC_RATE,FINAL_RATE)
    {          
        window.opener.document.getElementById(TextBoxId).value=code;    
        window.opener.document.getElementById(txtICODE).value=ITEM_CODE;          
        window.opener.document.getElementById(txtDESC).value=ITEM_DESC;  
        window.opener.document.getElementById(txtUNIT).value=UOM;  
        window.opener.document.getElementById(txtQTY).value=(Math.round(parseFloat(QTY_REM))).toFixed(2);  
        window.opener.document.getElementById(txtBasicRate).value=(Math.round(parseFloat(BASIC_RATE))).toFixed(2); 
        window.opener.document.getElementById(txtFinalRate).value=(Math.round(parseFloat(FINAL_RATE))).toFixed(2);     
        window.opener.document.getElementById(txtAmount).value=(Math.round(parseFloat(QTY_REM)*(parseFloat(FINAL_RATE)))).toFixed(2);
        window.opener.document.forms[0].submit();
         window.close();
    } 
    function BindPOOnly(code,TextBoxId,txtICODE,ITEM_CODE)
    {          
        window.opener.document.getElementById(TextBoxId).value=code;    
        window.opener.document.getElementById(txtICODE).value=ITEM_CODE;          
        window.opener.document.forms[0].submit();
        window.close();
    } 
    function BindPOOnlyTT(code,TextBoxId,PO_REF)
    {          
        window.opener.document.getElementById(TextBoxId).value=code; 
        window.opener.document.getElementById(PO_REF).value=code;            
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
            width: 80px;
        }
        
        .c2
        {
            margin-left: 4px;
            width: 80px;
        }
        
        .c3
        {
            margin-left: 4px;
            width: 120px;
        }
        
        .c4
        {
            margin-left: 4px;
            width: 120px;
        }
        
        .c5
        {
            margin-left: 4px;
            width: 120px;
        }
        
        .c6
        {
            margin-left: 4px;
            width: 120px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
            <asp:PlaceHolder runat="server" ID="ComboBox1Container" />
        </div>
    </form>
</body>
</html>
