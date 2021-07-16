<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OtherDeduct.ascx.cs" Inherits="Module_HRMS_Controls_OtherDeduct" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>

<table align="left" width="100%" class="tContentArial">
            <tr>
                <td>
                    <table width="100%" class="tContentArial" cellspacing="0" cellpadding="0" align="left">
                        <tbody>
                            <tr>
                                <td align="left" class="td" >
                                    <table class="tContentArial" cellspacing="0" cellpadding="0" >
                                        <tbody>
                                            <tr>                                                                                           
                                                <td id="tdUpdate" align="left" width="48" runat="server">
                                                    <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ValidationGroup="M1"
                                                        ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48">
                                                    </asp:ImageButton>
                                                </td>                                               
                                                <td id="tdClear" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                        ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td id="tdPrint" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                        ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                                <td valign="top" align="center">
                            <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                ToolTip="Exit" Height="41" Width="48" onclick="imgbtnExit_Click"></asp:ImageButton>
                        </td>
                                                <td id="tdHelp" runat="server" align="left" width="48">
                                                    <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                        ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="TableHeader td" align="center" >
                                    <b class="titleheading">Employee Other Deduction</b>
                                </td>
                            </tr>
                            <tr>
                                <td class="td" valign="top" align="left"  >
                                    <span class="Mode">You are in
                                        <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" align="center"  >
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ValidationGroup="M1" />
                                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                    </strong>
                                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                    </strong>
                                </td>
                            </tr>
                            <tr>
                                <td class="td">
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: right">
                                                Employee Name
                                                <td><b>:</b></td>
                                                </td>
                                            <td>
                                                <cc1:ComboBox ID="cmbEmpCode" runat="server" Width="180px" Height="150px"   AutoPostBack="true" 
                                                TabIndex="0" DataTextField="F_NAME" DataValueField="EMP_CODE" 
                                                    onloadingitems="cmbEmpCode_LoadingItems" 
                                                    onselectedindexchanged="cmbEmpCode_SelectedIndexChanged" >
                                                <HeaderTemplate>
                                                    <div class="header c3">
                                                        Code
                                                    </div>
                                                    <div class="header c4">
                                                        Emp Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c3">
                                                        <%# Eval("EMP_CODE")%></div>
                                                    <div class="item c4">
                                                        <%# Eval("F_NAME")%></div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                    out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc1:ComboBox>
                                            </td>                                                                                       
                                        </tr>                                                                           
                                    </table>
                                </td>
                            </tr>
                             <tr>
                                            <td >
                                                <b><i>Deduction Details</i></b>
                                            </td>
                                        </tr> 
                <tr>
               <td   align="center">                   
                        <asp:GridView ID="GVDeduction" runat="server"  AutoGenerateColumns="False" Width="100%"  CssClass="tContentArial" HorizontalAlign="Center">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Deduction">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDeduction" runat="server" Text='<%# Eval("DEDUCTION")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblYear" runat="server" Text='<%# Eval("Year")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Month">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblMonth" runat="server" Text='<%# Eval("MONTH")%>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                       <asp:TextBox ID="TxtAmount"  Width="75px" Text='<%# Eval("AMOUNT")%>' runat="server" CssClass="gCtrTxt" onKeyPress="return checkNumeric(event)"></asp:TextBox>
                                                                    <br />
                                                                    <asp:RequiredFieldValidator ID="RFDefaultValue" ControlToValidate="TxtAmount"
                                                                        ErrorMessage="Pls Enter Value!" Display="Dynamic" runat="server" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                                                    <asp:RangeValidator ID="RvDefaultValue" ControlToValidate="TxtAmount" ErrorMessage="Value From 0-999999"
                                                                        runat="server" Display="Dynamic" MinimumValue="0" MaximumValue="999999" ValidationGroup="M1"
                                                                        Type="Double"></asp:RangeValidator>
                                                                    
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                            </Columns>
                                        </asp:GridView>                                      
                    </td>
               </tr>  
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>