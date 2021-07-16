<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeAdvanceHRApproval.ascx.cs"
    Inherits="Module_HRMS_Controls_EmployeeAdvanceHRApproval" %>
<link href="../../../StyleSheet/abhishek.css" rel="stylesheet" type="text/css" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table id="tblempadvrq" runat="server" width="100%">
    <tr>
        <td>
            <table>
                <tr>
                    <td id="tdSave" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px" ValidationGroup="M1" TabIndex="4"></asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" TabIndex="5"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" TabIndex="6"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server" valign="top" class="cl">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" TabIndex="7"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" TabIndex="8"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="9"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="10"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                <td  class="TableHeader td" align="center" >
                  <span  class="titleheading">Employee Advance Request(HR Approval)</span>
                 </td>                    
                </tr>
                <tr>
                    <td align="left" valign="top" class="TdBackVir">
                        Employee Detail
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top">
                             <asp:GridView ID="GridViewAdvanceTakenDetail" runat="server" AllowPaging="True" 
                                 AllowSorting="True" Font-Size="X-Small" PageSize="15" 
                                    CellPadding="3"   GridLines="Both" Width="100%" ForeColor="#333333"  
                                 CssClass = "smallfont" AutoGenerateColumns="False" Visible = "true" 
                                 onpageindexchanging="GridViewAdvanceTakenDetail_PageIndexChanging" 
                                 onrowcommand="GridViewAdvanceTakenDetail_RowCommand" 
                                 onrowdatabound="GridViewAdvanceTakenDetail_RowDataBound" >
                               <Columns>  
                                 <asp:TemplateField HeaderText="S.No." ItemStyle-VerticalAlign="top">
                                              <ItemTemplate><%#Container.DataItemIndex+1 %>
                                              </ItemTemplate>
                                              <ItemStyle VerticalAlign="Top" HorizontalAlign="Center" Width="3%" />
                                              <HeaderStyle VerticalAlign="Top" HorizontalAlign="Center" />
                                         </asp:TemplateField>  
                                <asp:BoundField DataField="APPL_NO" HeaderText="Application No." ItemStyle-Width="5%" > <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" /> </asp:BoundField> 
                                <asp:BoundField DataField="APPLY_DATE" HeaderText="Apply Date" DataFormatString ="{0:dd-MM-yyyy}" ItemStyle-Width="8%"> <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" /> </asp:BoundField>
                                     
                                <asp:TemplateField HeaderText="EMP_CODE"  ItemStyle-HorizontalAlign="Center"  ItemStyle-Width="8%">
                                 <ItemTemplate>
                                        <asp:Label ID="lblempcode" runat="server" Text='<%# Eval("EMP_CODE") %>'></asp:Label>
                                    </ItemTemplate>               
                                </asp:TemplateField>
                                <asp:BoundField DataField="EMPLOYEENAME" HeaderText="Name" ItemStyle-Width="12%"> <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
                                 <asp:BoundField DataField="APPLY_AMOUNT" HeaderText="Amount Requird" ItemStyle-Width="8%"> <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="right" /> </asp:BoundField> 
                                <asp:BoundField DataField="PURPOSE" HeaderText="Purpose" ItemStyle-Width="12%"><HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>   
                                  <asp:BoundField DataField="APPROVED_STATUS" HeaderText="Status" HtmlEncode="false"  ItemStyle-Width="10%">  <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" /> </asp:BoundField>
                                  <asp:TemplateField HeaderText="Select">
                                   <ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
                                             <ItemTemplate>
                                                    <asp:LinkButton ID="LnkDetail" runat="server" Text="Show Details" CommandName="Select"  TabIndex="12" CommandArgument='<%# Eval("APPL_NO") %>'></asp:LinkButton>
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
            <div id="DivDetail" runat="server" visible="false">
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Advance Application No.
                    </td>
                    <td align="left" valign="top" width="75%" colspan="3" class="cl">
                        <asp:TextBox ID="TxtAppNo" runat="server" Width="130px" TabIndex="1"></asp:TextBox>
                    </td>
                </tr>
                </table>
                <table width="100%">
                    <tr>
                        <td></td>
                    </tr>
                </table>
                <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Employee Code
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtEmpCode" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        Employee Name
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtEmpName" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Department
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtDepartment" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Designation
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtDesignation" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Position
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtPosition" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Branch
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtBranch" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Grade
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtGrade" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        Level
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtEmpLevel" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td colspan="4" class="TdBackVir">
                        Advance Detail
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Advance Apply Date
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtApplydate" Width="130px" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        Amount Requested
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtAmountRequest" Width="130px" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        HOD Approval Date
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtHODApprovDate" Width="130px" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        App. Amount By HOD
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtAmountApproveByHOD" Width="130px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        HR Approval Date
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtHRApprovalDate" runat="server" Width="130px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        App. Amount By HR
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtAmountApproveByHR" runat="server" Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" colspan="2" class="cl">
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        No Of Installments
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TxtNoOfInstalMent" Width="130px" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td  valign="top" class="TdBackVir">
                        Deduction Details
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" class="cl">
                        <asp:GridView ID="GridViewDeductinDetail" runat="server" Width="720px" Height="30px" AutoGenerateColumns="False"
                            Style="margin-top: 0px">
                            <Columns>
                                <asp:BoundField DataField="" HeaderText="Installment No.">
                                    <ItemStyle BorderWidth="1px" Font-Bold="True" Font-Italic="False" HorizontalAlign="Center"
                                        VerticalAlign="Top" Width="75px" Wrap="True" />
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="Month">
                                    <ItemStyle BorderWidth="1px" HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="Year">
                                    <ItemStyle BorderWidth="1px" HorizontalAlign="Center" VerticalAlign="Middle" Width="65px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="Amount to be Deducted">
                                    <ItemStyle BorderWidth="1px" HorizontalAlign="Right" VerticalAlign="Top" Width="75px" />
                                </asp:BoundField>
                                <asp:BoundField DataField="" HeaderText="Remark">
                                    <ItemStyle BorderWidth="1px" HorizontalAlign="Right" VerticalAlign="Top" Width="150px" />
                                </asp:BoundField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left" valign="top" class="TdBackVir">
                        Advance Taken Detail
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" class="cl">
                        <asp:GridView ID="GridViewAdvanceTaken" runat="server" AutoGenerateColumns="False" Width="720px" Height="30px">
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="Date" />
                            <asp:BoundField DataField="" HeaderText="Amount" />
                            <asp:BoundField DataField="" HeaderText="No. Of Installment" />
                            <asp:BoundField DataField="" HeaderText="Deduction" />
                            <asp:BoundField DataField="" HeaderText="Balanced Amount" />
                        </Columns>
                        </asp:GridView> 
                    </td>
                </tr>
            </table>
            </div>
        </td>
    </tr>
</table>
 </ContentTemplate>
</asp:UpdatePanel>
