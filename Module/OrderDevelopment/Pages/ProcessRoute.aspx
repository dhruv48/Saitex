<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProcessRoute.aspx.cs" Inherits="Module_OrderDevelopment_Pages_ProcessRoute" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get Process Route</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
    
    function BindYRNSPIN_BOM(COST,TextBoxCOST)
    {          
//        window.opener.document.getElementById(TextBoxCOST).value=COST; 
        window.opener.document.forms[0].submit();      
        window.close();
    }
   
       function Close()
         {  
            opener.Reload();  
            self.close();  
         }  
    
     
    function Calculation(val)
    {                                                                
     document.getElementById('txtTotalBOM').value=(parseFloat(document.getElementById('txtSale').value)+parseFloat(document.getElementById('txtFreight').value)+parseFloat(document.getElementById('txtCommission').value)+parseFloat(document.getElementById('txtIncentives').value)+parseFloat(document.getElementById('txtExMill').value)+parseFloat(document.getElementById('txtBrokerage').value)).toFixed(3);
    }
     
    </script>

</head>
<body bgcolor="#afcae4" width="100%">
    <form id="form1" style="background-color: #afcae4" runat="server" width="100%">
    <div width="100%">
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top" width="100%">
                           <span class="titleheading">  <strong class="titleheading">Process Route</span></strong>
                        </td>
                    </tr>
                    <tr>
                        <td class="td tdLeft" width="100%">
                            <table width="98%">
                                <tr bgcolor="#006699">
                                    <td align="left" class="tdLeft SmallFont" valign="top" id="tdWarpHeads" runat="server"
                                        width="14%">
                                        <span class="titleheading"><b>Process Route</b></span>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td align="left" valign="top" width="14%" runat="server">
                                        <asp:DropDownList ID="ddlProcessRoot" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            CssClass="SmallFont" OnSelectedIndexChanged="ddlProcessRoot_SelectedIndexChanged"
                                            TabIndex="16" Width="98px">
                                            
                                        </asp:DropDownList>
                                    </td>
                              
                                    </tr>
                                <tr>
                                    <td align="left" valign="top" width="100%">
                                        <asp:GridView ID="gvProcessingStandardMaster" runat="server" 
                                            AllowSorting="True" AutoGenerateColumns="False" CssClass="smallfont" 
                                            TabIndex="30" Width="98%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Machine Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtMachineCode" runat="server" CssClass="Label SmallFont" 
                                                            Text='<%# Bind("MAC_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Process Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txcProcessCode" runat="server" CssClass="Label smallfont" 
                                                            ReadOnly="true" TabIndex="19" Text='<%# Bind("PROS_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Serial Number">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemakrs" runat="server" CssClass="LabelNo smallfont" 
                                                            ReadOnly="true" TabIndex="21" Text='<%# Bind("S_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Process Description">
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtProcessDescription" runat="server" 
                                                            CssClass="LabelNo smallfont" ReadOnly="true" TabIndex="21" 
                                                            Text='<%# Bind("PROS_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Test Code">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTestcode" runat="server" CssClass="LabelNo smallfont" 
                                                            ReadOnly="true" TabIndex="21" Text='<%# Bind("Test_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtRemakrs" runat="server" CssClass="LabelNo smallfont" 
                                                            ReadOnly="true" TabIndex="21" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle HorizontalAlign="Left" />
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheading" 
                                                Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top" width="100%">
                            <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
                                CssClass="SmallFont" Width="60px"/>
                            <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close"
                                ValidationGroup="M1" CssClass="SmallFont" onclientclick="Close();" Width="60px"/>
                            <br />
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="td" valign="top" width="100%">
                            <asp:CheckBox ID="CheckBox1" runat="server" CssClass="SmallFont" />
                            <asp:Button ID="btnupdateflag" runat="server" Text="Update Flag" 
                                CssClass="SmallFont" onclick="btnupdateflag_Click" Width="70px" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
