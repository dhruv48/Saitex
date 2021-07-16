<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Telephone_Allotment.ascx.cs" Inherits="Module_HRMS_Controls_Telephone_Allotment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<table class="td tContent">
<tr>
        <td class="tdRight" colspan = "4">
            <table class="tdLeft">
                            <tr>
                                <td id="tdSave" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" TabIndex="9" OnClick="imgbtnSave_Click" runat="server"
                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>
                                <td id="tdUpdate" valign="top" align="center" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" OnClick="imgbtnUpdate_Click" runat="server"
                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48" ValidationGroup="M1">
                                    </asp:ImageButton>
                                </td>                               
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnFind" TabIndex="9"  runat="server"
                                        ImageUrl="~/CommonImages/link_find.png" ToolTip="Find" Height="41" Width="48">
                                    </asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                        ToolTip="Clear" Height="41" Width="48" onclick="imgbtnClear_Click"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                        ToolTip="Exit" Height="41" Width="48"></asp:ImageButton>
                                </td>
                                <td valign="top" align="center">
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" colspan = "4">
            <span class="titleheading">Employee Telephone Allotment</span>
            </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td" colspan="4">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>  
   
<tr><td colspan="4" class = "td">Telephone Record :-</td></tr>
<tr>
                    <td align="left" class="td" valign="top" width="100%">
                        <table width="100%">
                            <tr bgcolor="#006699">
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Employee</b></span>
                                </td> 
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Telephone</b></span>
                                </td>
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Account No</b></span>
                                </td> 
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Teriff Plan</b></span>
                                </td> 
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Mobile Limit</b></span>
                                </td> 
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Allotment Date</b></span>
                                </td> 
                                <td class="tdLeft SmallFont">
                                    <span class="titleheading"><b>Remarks</b></span>
                                </td>
                                <td style="width:15%"> </td> 
                            </tr>
                            <tr>
                                <td align="left" valign="top">
                                        <asp:DropDownList ID="DDLEmployee" CssClass="SmallFont TextBox UpperCase" 
                                            Width="175px" runat="server"   AutoPostBack="True" 
                                            onselectedindexchanged="DDLEmployee_SelectedIndexChanged">  </asp:DropDownList>
                                </td> 
                                <td align="right" valign="top">
                                     <asp:DropDownList ID="DDLMobile" CssClass="TextBox SmallFont" DataTextField="TELEPHONE_NO" DataValueField="TELEPHONE_NO" Width="100px" runat="server"></asp:DropDownList>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtAccountNo" runat="server" CssClass="TextBox SmallFont" Width="100px" ></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtTeriffPlane" runat="server" CssClass="TextBox SmallFont" Width="100px" ></asp:TextBox>
                                </td>
                                <td align="left" valign="top">
                                    <asp:TextBox ID="TxtMobileLimit" runat="server" onKeyPress="pricevalidate(this);" CssClass="SmallFont TextBoxNo" Width="100px" ></asp:TextBox>
                                </td>
                                 <td align="left" valign="top">
                                    <asp:TextBox ID="TxtAllotmentDate" runat="server" CssClass="TextBox SmallFont" Width="100px" ></asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" Format="dd/MM/yyyy"  TargetControlID="TxtAllotmentDate" runat="server"></cc1:CalendarExtender>
                                </td>
                                 <td align="left" valign="top">
                                    <asp:TextBox ID="TxtREmarks" runat="server" CssClass="TextBox SmallFont" Width="170px" ></asp:TextBox>
                                </td>
                                <td align="left" valign="top" style="width:15%">
                                       <asp:LinkButton ID="lbtnsavedetail" runat="server" TabIndex="7" 
                                            onclick="lbtnsavedetail_Click">Save</asp:LinkButton>&nbsp;/&nbsp;<asp:LinkButton ID="lbtnCancel" runat="server" TabIndex="8" 
                                            onclick="lbtnCancel_Click">Cancel</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>  
<tr>
<td colspan="4">
    <asp:GridView ID="GridViewTelephoneTRN" runat="server" AutoGenerateColumns="False" 
        AllowPaging="True" AllowSorting="True" Font-Size="X-Small" PageSize="15" 
        CellPadding="3"   GridLines="None" Width="100%" ForeColor="#333333" 
        CssClass = "smallfont" 
        onrowcommand="GridViewTelephoneTRN_RowCommand"      >       
        <FooterStyle Width="100%" BackColor="#507CD1"  ForeColor="White" Font-Bold="True" />
        <RowStyle BackColor="#EFF3FB" />
        <Columns>
            <asp:TemplateField HeaderText="S.No." >
                   <ItemTemplate>
                       <%#Container.DataItemIndex+1 %>
                   </ItemTemplate>
                   <ItemStyle  HorizontalAlign="Center" Width="3%" />
                   <HeaderStyle  HorizontalAlign="Center" />
        </asp:TemplateField>
            <asp:TemplateField HeaderText="EMP_CODE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="50px">
             <ItemTemplate>
                    <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                </ItemTemplate>               
            </asp:TemplateField>
            <asp:TemplateField HeaderText="EMPLOYEE" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"> <ItemTemplate>
                    <asp:Label ID="LblEmpName" runat="server" Text='<%# Eval("EMPLOYEENAME") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="DEPARTMENT" ItemStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"> <ItemTemplate>
                    <asp:Label ID="LblDepartment" runat="server" Text='<%# Eval("DEPARTMENT") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="DESIGNATION" ItemStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="120px"> <ItemTemplate>
                    <asp:Label ID="LblDesignation" runat="server" Text='<%# Eval("DESIGINATION") %>'></asp:Label>
                </ItemTemplate></asp:TemplateField> 
            <asp:TemplateField HeaderText="MOBILE_NO" ItemStyle-Width="80px" HeaderStyle-Width="80px">
             <ItemTemplate>
                <asp:Label ID="LblTelephoneNo" runat="server" Text='<%# Eval("TELEPHONE_NO") %>'></asp:Label>
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="TERIFF_PLAN">
             <ItemTemplate>
                        <asp:Label ID="LblTariffPlan" runat="server" Text='<%# Eval("TERIFF_PLAN") %>'></asp:Label>    
              </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="MOBILE LIMIT">
             <ItemTemplate>
                       <asp:Label ID="LblMobileLimit" runat="server" Text='<%# Eval("TELEPHONE_LIMIT") %>'></asp:Label>    
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ACCOUNT NO">
             <ItemTemplate>
                     <asp:Label ID="LblAccountNo" runat="server" Text='<%# Eval("ACCOUNT_NO") %>'></asp:Label>                    
              </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ALLOTEMENT DATE">
             <ItemTemplate>
                     <asp:Label ID="LblAllotedDate" runat="server" Text='<%# Bind("ALLOT_DATE","{0:d}") %>'></asp:Label>  
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="REMARKS">
                <ItemTemplate>
                      <asp:Label ID="LblRemarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>  
                </ItemTemplate>
            </asp:TemplateField>            
             <asp:TemplateField HeaderText="Delete">
                                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                                     <ItemTemplate>
                                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"  TabIndex="12" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                                            <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete" TabIndex="12" OnClientClick="javascript: return confirm('Are you sure you want to delete this record?')" CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                                                     </ItemTemplate>
                                          </asp:TemplateField>  
        </Columns>
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <HeaderStyle 
            HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
            ForeColor="White" Font-Bold="True" /> 
    </asp:GridView>
    
    </td>
  </tr>

</table>
