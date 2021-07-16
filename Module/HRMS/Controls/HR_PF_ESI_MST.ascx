<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_PF_ESI_MST.ascx.cs"
    Inherits="Module_HRMS_Controls_HR_PF_ESI_MST" %>
<link href="../../../StyleSheet/abhishek.css" rel="stylesheet" type="text/css" />
<table class="tContentArial" width="100%">
    <tr>
        <td>
            <table class="tdLeft">
                <tr>
                    <td id="tdSave" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px" ValidationGroup="M1" TabIndex="5" OnClick="imgbtnSave_Click">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" TabIndex="6" OnClick="imgbtnUpdate_Click">
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" TabIndex="7" >
                        </asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" valign="top" class="cl">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" TabIndex="8"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" TabIndex="9" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="10"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="11" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="12" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
       </tr>
        <tr>
        <td class="TableHeader td" align="center" colspan = "4">
            <span class="titleheading">PF ESI Master</span>
            </td>
     </tr>
          <tr>
            <td  colspan="4">                    
            <table class="tdLeft" width="100%">
            <tr>
                    <td align="right" valign="top" class="cl" width="15%">
                        Choose
                    </td>
                    <td align="left" valign="top" class="cl"  width="15%">
                        <asp:DropDownList ID="DDLTrnType" runat="server" Width="100px" 
                            AppendDataBoundItems="True">
                           <asp:ListItem Value="0">----Select----</asp:ListItem>
                            <asp:ListItem>PF</asp:ListItem>
                            <asp:ListItem>ESI</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TxtTrnNO" Visible="false" Text="New"  runat="server"></asp:TextBox>
                    </td>
                     <td align="right" valign="top" class="cl" width="15%">
                        Percentage
                    </td>
                    <td align="left" valign="top" colspan="4"  class="cl" width="15%">
                        <asp:TextBox ID="TxtTrnValue" runat="server" Width="75px"></asp:TextBox>(%)
                    </td>
                </tr>               
                <tr>
                    <td align="right" valign="top" class="cl" width="15%">
                        Cut Of Basic
                    </td>
                    <td align="left" valign="top" class="cl" width="15%">
                        <asp:TextBox ID="TxtCOB" runat="server" Width="151px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" class="cl" width="15%">
                        Location
                    </td>
                    <td align="left" valign="top" class="cl" width="15%">
                        <asp:DropDownList ID="DDLBranch" runat="server" Width="150px" CssClass="SmallFont TextBox UpperCase"></asp:DropDownList>
                    </td>
                </tr>
                </table>
            <table width="100%">
                <tr>
                    <td align="left" class="TdBackVir">PF ESI Record
                    </td>
                </tr> 
                <tr>            
                        <td >
                    <asp:GridView ID="GridViewESIPF" runat="server" AutoGenerateColumns="False" AllowPaging="True"
                        AllowSorting="True" Font-Size="9px" GridLines="None" CellPadding="4" ForeColor="#333333"
                        Width="100%" onrowcommand="GridViewESIPF_RowCommand">
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Year"  >
                                <ItemTemplate>
                                    <asp:Label ID="lblyear" runat="server" Text='<%# Eval("Year") %>'></asp:Label>
                                </ItemTemplate>                                
                                <ItemStyle HorizontalAlign="Center" Width="3%" ></ItemStyle>
                            </asp:TemplateField>                           
                            
                            <asp:TemplateField HeaderText="Branch"  ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"   ItemStyle-Width="15%">
                                 <ItemTemplate>
                                    <asp:Label ID="lblbranchcode" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                </ItemTemplate> 
                            </asp:TemplateField>                          
                            
                            <asp:TemplateField HeaderText="Company" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="15%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcompcode" runat="server" Text='<%# Eval("COMP_NAME") %>'></asp:Label>
                                </ItemTemplate>                               
                            </asp:TemplateField>                           
                            
                            <asp:TemplateField HeaderText="Trn Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left"    ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lblType" runat="server" Text='<%# Eval("TRN_TYPE") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>                          
                            
                            <asp:TemplateField HeaderText="Percentage"  ItemStyle-HorizontalAlign="Center"    ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblpercentage" runat="server" Text='<%# Eval("TRN_VALUE") %>'></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>                           
                            
                            <asp:TemplateField HeaderText="Cut of Basic" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="8%">
                                <ItemTemplate>
                                    <asp:Label ID="lblcb" runat="server" Text='<%# Eval("CB") %>' ></asp:Label>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField HeaderText="Location" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Left" ItemStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:Label ID="lbllocation" runat="server" Text='<%# Eval("LOC") %>' ></asp:Label>
                                     <asp:Label ID="LblLocID" Visible="false"  runat="server" Text='<%# Eval("Location") %>' ></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Delete">
                            <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                 <ItemTemplate>
                                        <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit"  TabIndex="12" CommandArgument='<%# Eval("TRN_NO") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                        <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete" TabIndex="12" OnClientClick="javascript: return confirm('Are you sure you want to delete this record?')" CommandArgument='<%# Eval("TRN_NO") %>'></asp:LinkButton>
                                 </ItemTemplate>
                      </asp:TemplateField> 
                        </Columns>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1" 
                                        ForeColor="White" />   
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </td>
                </tr>   
            </table>
        </td>
    </tr>
</table>
