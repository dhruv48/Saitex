<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HiringRequest.ascx.cs" Inherits="Module_HRMS_Controls_HiringRequest" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
<table align="left" width="100%" class="tContentArial">
    <tr>
        <td>
            <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                     <tbody>
                            <tr>
                                <td align="left" class="td" colspan="3">
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" >
                                        <tbody>
                                            <tr>
                                                <td id="tdSave" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnSave"  runat="server" ValidationGroup="M1" 
                                                        ImageUrl="~/CommonImages/save.jpg" ToolTip="Save" 
                                                        onclick="imgbtnSave_Click" ></asp:ImageButton>
                                                </td>                                              
                                                <td id="tdUpdate" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ValidationGroup="M1" 
                                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" 
                                                        onclick="imgbtnUpdate_Click"> </asp:ImageButton>
                                                </td>
                                                 <td id="tdFind" runat="server" valign="top">
                                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41" TabIndex="7" ></asp:ImageButton>
                                                </td>
                                                 <td id="tdDelete" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnDelete"  runat="server" 
                                                         ImageUrl="~/CommonImages/del6.png"  ToolTip="Delete" 
                                                         onclick="imgbtnDelete_Click" ></asp:ImageButton>
                                                </td>
                                                <td id="tdClear" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnClear"  runat="server" 
                                                        ImageUrl="~/CommonImages/clear.jpg" ToolTip="Clear" 
                                                        onclick="imgbtnClear_Click" ></asp:ImageButton>
                                                </td>
                                                <td id="tdPrint" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnPrint"  runat="server" ImageUrl="~/CommonImages/link_print.png" ToolTip="Print" ></asp:ImageButton>
                                                </td>
                                                 <td id="tdExit" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnExit"  runat="server" ToolTip="Exit" 
                                                         ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" 
                                                         OnClientClick="javascript:return window.confirm('Are you sure you want to Exit')" 
                                                         onclick="imgbtnExit_Click">
                                                    </asp:ImageButton>
                                                </td>
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp"  runat="server" ImageUrl="~/CommonImages/link_help.png" ToolTip="Help" ></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableHeader td" align="center" colspan="3">
                                    <b class="titleheading">Employee Hiring Request</b></td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left" colspan="3">
                                    <span class="Mode">You are in
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center" colspan="3">
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ValidationGroup="M1" />                                    
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <table width="100%">                                        
                                        <tr>
                                            <td colspan="9">
                                                <b><i>Hiring Request</i></b>
                                            </td>
                                        </tr>
                                        <tr id="trFind" runat="server" >
                                          <td colspan="2"> <asp:TextBox ID="TxtHireID" Text="NEW" Visible="false"  runat="server" 
                                                    Width="0px"></asp:TextBox></td>
                                            <td>                                                
                                            </td>
                                            <td style="width:150px;"></td>                                           
                                        </tr>
                                        <tr>
                                         <td>
                                                Department</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td><asp:TextBox ID="txtDept" Width="150px" runat="server"></asp:TextBox></td>
                                            <td>
                                                Location</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLLocation" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Position</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="DDLPosition" runat="server" Width="150px">
                                                </asp:DropDownList>
                                            </td>
                                           
                                        </tr>
                                        <tr>                                           
                                            <td>
                                                Total vacancies.</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TxtTotalVac" runat="server" onKeyPress="return checkNumeric(event)" MaxLength="4" CssClass="gCtrTxt" Width="150px"></asp:TextBox>
                                            </td>
                                            <td>Priority</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLPriority" runat="server" Width="150px">
                                                    <asp:ListItem Value="0">----------SELECT-----------</asp:ListItem>
                                                    <asp:ListItem Value="H">High</asp:ListItem>
                                                    <asp:ListItem Value="N">Normal</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                             <td>Vacancies Type</td>
                                            <td>
                                                <b>:</b>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="DDLVacType" runat="server" Width="150px">
                                                    <asp:ListItem Value="0">---------SELECT-----------</asp:ListItem>
                                                    <asp:ListItem Value="I">Internal</asp:ListItem>
                                                    <asp:ListItem Value="E">External</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>  
                                        <tr>
                                            <td>Remarks</td>
                                            <td><b>:</b></td>
                                            <td><asp:TextBox ID="TxtRemarks" TextMode="MultiLine" Width="150px" runat="server"></asp:TextBox></td>
                                        </tr>  
                                        
                                        <tr><td colspan="9"></td></tr> 
                                        <tr><td colspan="9">
                                            <asp:GridView ID="GrdViewHiring" Width="100%" CssClass="SmallFont" 
                                                runat="server"  AutoGenerateColumns="False" 
                                                onrowcommand="GrdViewHiring_RowCommand" >
                                                <Columns>
                                                    <asp:BoundField DataField="HIR_RQ_ID" HeaderText="ID" />
                                                    <asp:BoundField DataField="DEPARTMENT" HeaderText="DEPARTMENT" />
                                                    <asp:BoundField DataField="POSITION_NAME" HeaderText="POSITION" />
                                                    <asp:BoundField DataField="TOTAL_VAC" ItemStyle-HorizontalAlign="Right"  HeaderText="VACANCIES" />
                                                    <asp:BoundField DataField="PRIORITY" HeaderText="PRIORITY" />
                                                    <asp:BoundField DataField="VACTYPE" HeaderText="VAC.TYPE" />
                                                    <asp:BoundField DataField="POSTEDBY" HeaderText="POSTEDBY" />
                                                    <asp:BoundField DataField="POSTEDON" HeaderText="POSTED ON" />
                                                    <asp:BoundField DataField="REMARKS" HeaderText="REMARKS" />
                                                    <asp:BoundField DataField="STATUS" HtmlEncode="false"  HeaderText="STATUS" />
                                                     <asp:TemplateField HeaderText="Delete" ItemStyle-VerticalAlign="top">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="ImgDelete" runat="server" CommandArgument='<%# Eval("HIR_RQ_ID") %>'
                                                            CommandName="ImageDelete" CssClass="ContolStyle" Text="Delete" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this?')" /></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Edit" ItemStyle-VerticalAlign="top">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# Eval("HIR_RQ_ID") %>' CommandName="ImageEdit" CssClass="ContolStyle" Text="Edit"></asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Top"></ItemStyle>
                                                    </asp:TemplateField>                                                    
                                                </Columns>
                                                 <HeaderStyle CssClass="HeaderStyle GrdHeader" BackColor="#336699" />
                                            </asp:GridView>
                                        </td></tr>                                                               
                                    
                                    </table>
                                </td>
                            </tr>                            
                     </tbody>
             </table>
        </td>
    </tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>
