<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Master_Mst.ascx.cs" Inherits="Module_Inventory_Controls_Master_Mst" %>
<%@ Register assembly="obout_ComboBox" namespace="Obout.ComboBox" tagprefix="cc1" %>
<table cellpadding="0" cellspacing="0" border="0"  align="left" width ="100%" >
        <tr>
            <td align="left"  valign="top" >
                <br />
                <table cellpadding="0" cellspacing="0" border="0"  align="left" class="tContentArial tablebox" >
                    <tr>
                        <td align="center" colspan="3">
                            <table cellpadding="0" cellspacing="0" border="0" align="left" >
                                <tr>
                                     <td id="tdSave" runat="server">
                                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="New" ImageUrl="~/CommonImages/save.jpg"
                                            Width="48" Height="41"  ValidationGroup="M1" OnClick="imgbtnSave_Click" />
                                    </td>
                                   <td id="tdFind" runat="server" >
                                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                            Width="48" Height="41" OnClick="imgbtnFind_Click" ></asp:ImageButton>
                                    </td>
                                    <%--<td>
                            <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/save.jpg"
                                Width="40" Height="30" OnClick="imgbtnSave_Click"></asp:ImageButton>
                        </td>--%>
                                    <td id="tdUpdate" runat="server" >
                                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                            Width="48" Height="41"  ValidationGroup="M1" OnClick="imgbtnUpdate_Click"></asp:ImageButton>
                                    </td>
                                    <td id="tdDelete" runat="server" style="width: 6px">
                                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                            Width="48" Height="41"  OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')" OnClick="imgbtnDelete_Click">
                                        </asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                            Width="48" Height="41"  OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')" OnClick="imgbtnClear_Click">
                                        </asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                            Width="48" Height="41" OnClick="imgbtnPrint_Click" ></asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                            Width="48" Height="41" OnClick="imgbtnExit_Click" ></asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                            Width="48" Height="41" ></asp:ImageButton>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="TableHeader" colspan="3">
                            <span class="titleheading">Master</span>
                        </td>
                    </tr>
                      <tr>
                        <td align="left" colspan="2" valign="top">
                            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                            </span>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top" colspan="2">
                            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="30%">
                            Master Name :
                        </td>
                        <%--<td align="center" valign="top" width="2%">
                            <b>:</b></td>--%>
                        <td align="left" valign="top" width="68%" >
                            <asp:TextBox ID="txtMstName" runat="server" CssClass="gCtrTxt" Width="150px"  TabIndex="1"
                                MaxLength="10" OnTextChanged="txtMstName_TextChanged"></asp:TextBox>
                            <asp:TextBox ID="txtFind" runat="server" AutoPostBack="True" CssClass="gCtrTxt" MaxLength="10"
                                TabIndex="1" Visible="false" Width="150px" OnTextChanged="txtFind_TextChanged"></asp:TextBox>
                            <asp:LinkButton ID="lnkSearch" runat="server" OnClick="lnkSearch_Click">Find</asp:LinkButton>
                            <br />
                              <asp:RequiredFieldValidator ID="RequiredFieldValidator1" Display="dynamic" runat="server"
                                ErrorMessage="*Enter Master Name" ControlToValidate="txtMstName" ValidationGroup="M1"  TabIndex ="1"></asp:RequiredFieldValidator>
                               
                          
                           <%-- <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="1000"
                                MinimumPrefixLength="1" ServiceMethod="GetMasterName" ServicePath="../AutoComplete.asmx"
                                TargetControlID="txtFind" UseContextKey="false">
                            </cc1:AutoCompleteExtender>--%>
                          
                        </td>
                    </tr>
                    <tr>
                        <td align="right" valign="top" width="30%">
                            Description :
                        </td>
                       <%-- <td align="center" valign="top" width="2%">
                            <b>:</b></td>--%>
                        <td align="left" valign="top" width="68%">
                            <asp:TextBox ID="txtDesc" runat="server" CssClass="gCtrTxt" Width="150px" TabIndex="2" TextMode ="MultiLine"
                                Rows="2" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" valign="top" width="30%">
                            Max Length :
                        </td>
                        <%--<td align="center" valign="top" width="2%">
                            <b>:</b></td>--%>
                        <td align="left" valign="top" width="68%">
                            <asp:TextBox ID="txtMaxLen" runat="server" CssClass="gCtrTxt" Width="150px" TabIndex="3"
                                Rows="2" MaxLength="50"></asp:TextBox>
                             <asp:RangeValidator ID="RangeValidator3" ValidationGroup="M1" runat="server" ErrorMessage="Pls enter numeric value "
                                Display="dynamic" Type="Integer" ControlToValidate="txtMaxLen" MinimumValue="1"
                                MaximumValue="1000"></asp:RangeValidator></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>