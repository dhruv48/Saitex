<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmpLeaveDetails.ascx.cs"  Inherits="Module_HRMS_Controls_EmpLeaveDetails" %>
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
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate> 
      <table align="left" class="tContentArial" width="100%">
            <tr>
                <td class="td">
                    <table class="tContentArial" cellspacing="0" cellpadding="0" border="0" style="width: 10%;height: 39px">
                        <tr> 
                            <td id="tdUpdate" width="48" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1">
                                </asp:ImageButton>
                            </td>
                             <td id="tdFind" runat="server">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                        Width="48" Height="41" onclick="imgbtnFind_Click" ></asp:ImageButton>
                                </td>
                            <td id="Td3" colspan="2" width="48" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    Width="48" Height="41" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td> 
                            </tr>                       
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table cellpadding="0" cellspacing="0" border="0" width="100%" align="left" class="tContentArial">
                        <tr>
                            <td colspan="3" valign="top" class="TableHeader td" align="center">
                                <span class="titleheading">Employee Leave Master Detail</span>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" valign="top" style="height: 42px">
                                <br />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError" 
                                    Visible="False"></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="left" valign="top">
                                <table cellpadding="0" cellspacing="0" border="1" width="100%" align="left" class="tContentArial">
                                    <tr><td colspan="5" valign="top" class="TableHeader td" align="center"><span class="titleheading">Employee Details</span></td></tr>
                                    <tr>
                                        <td style="width:20%; text-align:right;"><b>Employee Code :</b></td>
                                        <td style="width:25%; text-align:left;"><asp:Label ID="lblEmployeeCode" runat="server"></asp:Label></td>
                                        <td></td>
                                        <td style="width:20%; text-align:right;"><b>Employee Name :</b></td>
                                        <td style="width:25%; text-align:left;"><asp:Label ID="lblEmployeeName" runat="server"></asp:Label></td>                                                                                                               
                                    </tr>
                                   <tr>
                                        <td style="width:20%; text-align:right;"><b>Department :</b></td>
                                        <td style="width:25%; text-align:left;"><asp:Label ID="LblDept" runat="server"></asp:Label></td>
                                        <td></td>
                                        <td style="width:20%; text-align:right;"><b>Designation :</b></td>
                                        <td style="width:25%; text-align:left;"><asp:Label ID="LblDesig" runat="server"></asp:Label></td>                                                                                                               
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr><td></td></tr>
                        <tr>
                            <td colspan="3" valign="top" class="TableHeader td" align="center">
                                <span class="titleheading">Yearly Leave Details</span>
                            </td>
                        </tr>
                         <tr>
                            <td colspan="2" valign="top"  align="center">Previous Year:
                                <asp:Label ID="LblPreYear" Font-Bold="true" runat="server" Text="Label"></asp:Label>
                            </td>
                            <td  valign="top"  align="center">Current Year:
                                <asp:DropDownList ID="DDLYear" runat ="server" AutoPostBack="true"  Width="100px" runat="server" 
                                    Enabled="False" onselectedindexchanged="DDLYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" align="center" style="height: 161px">
                            <asp:GridView ID="grdLeave" runat="server" AutoGenerateColumns="false" CssClass="tContentArial"
                                    HorizontalAlign="Left" ShowFooter="false" Width="100%"> 
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="LEAVE TYPE">                                           
                                            <ItemTemplate>                                                
                                                            <asp:Label ID="lblLeaveName" runat="server" Text='<%# Eval("LV_NAME")%>'></asp:Label>
                                                            <asp:Label ID="lblLeaveId" runat="server" Text='<%# Eval("LV_ID")%>' Visible="false" Width="25px"></asp:Label>
                                                            <asp:Label ID="lBLMst_ID" runat="server" Text='<%# Eval("MST_ID")%>' Visible="false" Width="5px"></asp:Label>                                                      
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="OPENING">  
                                        <ItemStyle HorizontalAlign="Center" Font-Bold="true" />                               
                                            <ItemTemplate>                                            
                                                <asp:Label ID="LblOpening" runat="server" Text='<%# Eval("OPENING")%>'></asp:Label>  
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CONSUMED"> 
                                         <ItemStyle HorizontalAlign="Center" Font-Bold="true" /> 
                                            <ItemTemplate >
                                                 <asp:Label ID="LblConsumed" runat="server" Text='<%# Eval("CONSUMED")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CLOSING">
                                           <ItemStyle HorizontalAlign="Center" Font-Bold="true" /> 
                                            <ItemTemplate >
                                                <asp:Label ID="LblClosing" runat="server" Text='<%# Eval("CLOSING")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="CARRY FORWARD">
                                           <ItemStyle HorizontalAlign="Center" Font-Bold="true" /> 
                                            <ItemTemplate >
                                                  <asp:Label ID="LblCarry" runat="server" Text='<%# Eval("FORWORD")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="CURRENT YEAR OPENING">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  />
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtDefaultValue" runat="server" AutoPostBack="true"  CssClass="LabelNo SmallFont" 
                                                    onkeyup="pricevalidate(this);"  Text='<%# Eval("LV_DAYS")%>' 
                                                    Width="75px" ontextchanged="txtDefaultValue_TextChanged"></asp:TextBox>
                                               <br /> <asp:RequiredFieldValidator ID="RFDefaultValue" runat="server" ControlToValidate="txtDefaultValue"  Display="Dynamic" ErrorMessage="Pls Enter Days!" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                <asp:RangeValidator ID="RvDefaultValue" runat="server" ControlToValidate="txtDefaultValue" Display="Dynamic" ErrorMessage="Value From 0-365" MaximumValue="365" MinimumValue="0" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="TOTAL DAYS">
                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Top"  />
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtCurrYear" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont" onKeyPress="return checkNumeric(event)" Text='<%# Eval("LV_DAYS")%>' Width="75px"></asp:TextBox>                                             
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:GridView>                                
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" width="100%" valign="top">
                </td>
            </tr>
        </table>
</ContentTemplate>
</asp:UpdatePanel>