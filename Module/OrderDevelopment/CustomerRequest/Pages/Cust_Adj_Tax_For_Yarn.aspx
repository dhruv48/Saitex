

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Cust_Adj_Tax_For_Yarn.aspx.cs" Inherits="Module_OrderDevelopment_CustomerRequest_Pages_Cust_Adj_Tax_For_Yarn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get discount and taxes</title>
     <link rel="stylesheet" type="text/css" href="../../../../StyleSheet/CommonStyle.css" />
  <%--<link rel="stylesheet" type="text/css" href="../../../StyleSheet/ModalPopup.css" />--%>

    <script language="javascript" type="text/javascript">
    function BindRate(Amount,TextBoxId,DiscountAmount,TextBoxDiscount,Fright,TextBoxFright,NetRate,TextBoxNetRate,SGST,TextBoxSGST,CGST,TextBoxCGST,IGST,TextBoxIGST)
    {          
        textObj = window.opener.document.getElementById(TextBoxId);
        textObj.disabled =false;
        textObj.value=Amount;  
        textObj.ReadOnly =true;
        
        textObj1 = window.opener.document.getElementById(TextBoxDiscount);
        textObj1.disabled =false;
        textObj1.value=DiscountAmount;  
        textObj1.ReadOnly =true;
        
        textObj2 = window.opener.document.getElementById(TextBoxFright);
        textObj2.disabled =false;
        textObj2.value=Fright;  
        textObj2.ReadOnly =true;
        
         textObj3 = window.opener.document.getElementById(TextBoxNetRate);
         textObj3.disabled =false;
         textObj3.value=NetRate;  
         textObj3.ReadOnly =true;
         
         textObj4 = window.opener.document.getElementById(TextBoxSGST);
         textObj4.disabled =false;
         textObj4.value=SGST;  
         textObj4.ReadOnly =true;
         
         textObj5 = window.opener.document.getElementById(TextBoxCGST);
         textObj5.disabled =false;
         textObj5.value=CGST;  
         textObj5.ReadOnly =true;
         
         textObj6 = window.opener.document.getElementById(TextBoxIGST);
         textObj6.disabled =false;
         textObj6.value=IGST;  
         textObj6.ReadOnly =true;
        
        
        window.opener.document.forms[0].submit();      
        window.close();
    } 

    function DisableTextObj(TextBoxId,TextBoxDiscount,TextBoxFright,TextBoxNetRate,TextBoxSGST,TextBoxCGST,TextBoxIGST)
    {               
        textObj = window.opener.document.getElementById(TextBoxId);       
        textObj.disabled =true;    
        textObj.ReadOnly =true;   
        
         textObj1 = window.opener.document.getElementById(TextBoxDiscount);       
        textObj1.disabled =true;    
        textObj1.ReadOnly =true;
        
         textObj2 = window.opener.document.getElementById(TextBoxFright);       
        textObj2.disabled =true;    
        textObj2.ReadOnly =true; 
         
        textObj3 = window.opener.document.getElementById(TextBoxNetRate);       
        textObj3.disabled =true;    
        textObj3.ReadOnly =true;    
        
        textObj4 = window.opener.document.getElementById(TextBoxSGST);       
        textObj4.disabled =true;    
        textObj4.ReadOnly =true;  
        
        textObj5 = window.opener.document.getElementById(TextBoxCGST);       
        textObj5.disabled =true;    
        textObj5.ReadOnly =true;
        
        textObj6 = window.opener.document.getElementById(TextBoxIGST);       
        textObj6.disabled =true;    
        textObj6.ReadOnly =true;
          
    }
    </script>

</head>
<body bgcolor="#afcae4">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table>
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading">Get Rate Component for
                                <asp:Label ID="Label1" runat="server"></asp:Label></strong>
                            <asp:Label ID="lblshade" ForeColor="White" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <asp:Label ID="lblErrormsg" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft">
                            <table>
                                <tr>
                                    <td class="TableHeader">
                                        <span class="titleheading">Select Component</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Select Base</span>
                                    </td>
                                    <td class="TableHeader">
                                        <span class="titleheading">Rate(%)</span>
                                    </td>
                                    <td class="TableHeader">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlRateComponent" runat="server" AppendDataBoundItems="True"
                                            Width="120px">
                                            <asp:ListItem>Select</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBaseComponent" runat="server" AppendDataBoundItems="True"
                                            Width="120px">
                                            <asp:ListItem Value="Basic Rate">Basic Rate</asp:ListItem>
                                            <asp:ListItem Value="Final Rate">Final Rate</asp:ListItem>
                                             <asp:ListItem Value="Flat Amount">Flat Amount</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtRate" runat="server" CssClass="SmallFont TextBox" Width="50px" AutoPostBack="true"  ontextchanged="txtRate_TextChanged"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSaveRateCompo" runat="server" OnClick="btnSaveRateCompo_Click"
                                            Text="Save"></asp:Button>
                                        <asp:Button ID="btnCancelRateCompo" runat="server" OnClick="btnCancelRateCompo_Click"
                                            Text="Cancel"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            Basic Rate :
                            <asp:Label ID="lblBasicRate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Final
                            Amount :
                            <asp:TextBox ID="txtFinalAmount" runat="server" CssClass="SmallFont TextBox" Enabled="False"
                                ValidationGroup="M1" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" ScrollBars="Vertical"
                                Height="150px">
                                <asp:GridView ID="grdRate" runat="server" AutoGenerateColumns="False" 
                                    OnRowCommand="grdRate_RowCommand" OnRowDataBound="grdRate_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblS_NO" runat="server" CssClass="SmallFont Label" 
                                                    Text='<%# Bind("Uniqueid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Component Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComponentName" runat="server" CssClass="SmallFont Label" 
                                                    Text='<%# Bind("COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Component Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaseComponentName" runat="server" CssClass="SmallFont Label" 
                                                    Text='<%# Bind("BASE_COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate (%)">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="txtRate" runat="server" AutoPostBack="True" 
                                                    CssClass="SmallFont Label" MaxLength="3" Text='<%# Bind("Rate") %>' 
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="txtAmount" runat="server" AutoPostBack="True" 
                                                    CssClass="SmallFont Label" MaxLength="3" Text='<%# Bind("Amount") %>' 
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Bind("UniqueId") %>' CommandName="editRate" Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" 
                                                    CommandArgument='<%# Bind("UniqueId") %>' CommandName="delRate" Text="Remove"></asp:LinkButton>
                                                <asp:Label ID="lblIS_PO" runat="server" CssClass="SmallFont Label" Text="ispo" 
                                                    Visible="false"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdRate" EventName="RowCommand" />
                <asp:AsyncPostBackTrigger ControlID="btnCancelRateCompo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSaveRateCompo" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>

