<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MRNDisTaxAdjMST_FIBER.aspx.cs" Inherits="Module_Fiber_Pages_MRNDisTaxAdjMST_FIBER" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get discount and taxes</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />
  

    <script language="javascript" type="text/javascript">
    function BindRate(Amount,TextBoxId)
    {          
        textObj = window.opener.document.getElementById(TextBoxId);
       
        textObj.disabled =false;
        textObj.value=Amount;  
        textObj.ReadOnly =true;
        
        window.opener.document.forms[0].submit();      
        window.close();
    } 

    function DisableTextObj(TextBoxId)
    {               
        textObj = window.opener.document.getElementById(TextBoxId);       
        textObj.disabled =true;    
        textObj.ReadOnly =true;        
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
                <table class="td" >
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                        </td>
                    </tr>
                            <caption>
                                <strong class="titleheading">Get Rate Component for
                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                </strong>
                                </td>
                                </tr>
                                <tr>
                                    <td class="td tdLeft">
                                        <asp:Label ID="lblErrormsg" runat="server" Font-Bold="False" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TableHeader">
                                        <table >
                                            <caption>
                                                <span class="titleheading">Select&nbsp;Component</span>
                                            </caption>
                                        </table>
                                    </td>
                                   
                                        <td class="TableHeader">
                                            <span class="titleheading">Select&nbsp;Base</span>
                                        </td>
                                        <td class="TableHeader">
                                            <asp:Label ID="lblRateAmount" runat="server" CssClass="titleheading" 
                                                Text="Rate(%)"></asp:Label>
                                            <%--<span class="titleheading">Rate(%)</span>--%>
                                        </td>
                                        <td class="TableHeader">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList ID="ddlRateComponent" runat="server" 
                                                AppendDataBoundItems="True" Width="120px">
                                                <asp:ListItem>Select</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlBaseComponent" runat="server" 
                                                AppendDataBoundItems="True" AutoPostBack="true" 
                                                onselectedindexchanged="ddlBaseComponent_SelectedIndexChanged" Width="120px">
                                                <asp:ListItem Value="Basic Rate">Basic Rate</asp:ListItem>
                                                <asp:ListItem Value="Final Rate">Final Rate</asp:ListItem>
                                                <asp:ListItem Value="Flat Amount">Flat Amount</asp:ListItem>
                                            </asp:DropDownList>
                                            <%-- AppendDataBoundItems="True"--%>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRate" runat="server" AutoPostBack="true" 
                                                CssClass="SmallFont TextBox" MaxLength="7" ontextchanged="txtRate_TextChanged" 
                                                Width="50px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FiltertxtRate" runat="server" 
                                                FilterType="Custom, Numbers" TargetControlID="txtRate" ValidChars="." />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSaveRateCompo" runat="server" 
                                                OnClick="btnSaveRateCompo_Click" Text="Save" />
                                            <asp:Button ID="btnCancelRateCompo" runat="server" 
                                                OnClick="btnCancelRateCompo_Click" Text="Cancel" />
                                        </td>
                                    </tr>
                                </tr>
                            </caption>
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
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Final&nbsp;Amount&nbsp;:
                            <asp:TextBox ID="txtFinalAmount" runat="server" CssClass="SmallFont TextBox" Enabled="False"
                                ValidationGroup="M1" Width="60px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top">
                            <asp:Panel ID="pnlIndentAdjustment" runat="server" BackColor="#afcae4" ScrollBars="Vertical"
                                Height="150px">
                                <asp:GridView ID="grdRate" runat="server" AutoGenerateColumns="False" 
                                    OnRowCommand="grdRate_RowCommand" onrowdatabound="grdRate_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S. No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblS_NO" runat="server" CssClass="SmallFont Label" Text='<%# Bind("Uniqueid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Component Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblComponentName" runat="server" CssClass="SmallFont Label" Text='<%# Bind("COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Component Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBaseComponentName" runat="server" CssClass="SmallFont Label" Text='<%# Bind("BASE_COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate (%)">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="txtRate" runat="server" CssClass="SmallFont Label" AutoPostBack="True"
                                                    MaxLength="3" Text='<%# Bind("Rate") %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemStyle />
                                            <ItemTemplate>
                                                <asp:Label ID="txtAmount" runat="server" CssClass="SmallFont Label" AutoPostBack="True"
                                                    MaxLength="3" Text='<%# Bind("Amount") %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnEdit" runat="server" CausesValidation="false" CommandName="editRate"
                                                    CommandArgument='<%# Bind("UniqueId") %>' Text="Edit"></asp:LinkButton>
                                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="false" CommandName="delRate"
                                                    CommandArgument='<%# Bind("UniqueId") %>' Text="Remove"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td class="td" align="center" valign="top" width="100%">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
