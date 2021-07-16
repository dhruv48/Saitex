<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Employee_Advance_Request.ascx.cs"
    Inherits="Module_HRMS_Controls_Employee_Advance_Request" %>
<link href="../../../StyleSheet/abhishek.css" rel="stylesheet" type="text/css" />
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
                    <td align="center" valign="middle" class="hd">
                        Employee Advance Request
                    </td>
                </tr>
                <tr>
                    <td align="left" valign="top" class="hd">
                        Employee Detail
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Advance Application No.
                    </td>
                    <td align="left" valign="top" width="75%" colspan="3" class="cl">
                        <asp:TextBox ID="TextBox1" runat="server" Width="130px" TabIndex="1"></asp:TextBox>
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
                        <asp:TextBox ID="TextBox2" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        Employee Name
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox3" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Department
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox4" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Designation
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox5" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Position
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox6" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        *Branch
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox7" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Grade
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox8" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        Level
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox9" runat="server" Width="130px" TabIndex="2"></asp:TextBox>
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
                    <td align="left" valign="top" colspan="4" class="hd">
                        Advance Detail
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        Advance Apply Date
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox10" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        Ammount Requested
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox11" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        HOD Approval Date
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox12" runat="server"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        App. Ammount By HOD
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox13" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="25%" class="cl">
                        HR Approval Date
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox14" runat="server" Width="130px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        App. Ammount By HR
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox15" runat="server" Width="130px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="25%" colspan="2" class="cl">
                    </td>
                    <td align="right" valign="top" width="25%" class="cl">
                        No Of Installments
                    </td>
                    <td align="left" valign="top" width="25%" class="cl">
                        <asp:TextBox ID="TextBox16" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td align="left" valign="top" class="hd">
                        Deduction Details
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" class="cl">
                        <asp:GridView ID="GridView1" runat="server" Width="720px" Height="30px" AutoGenerateColumns="False"
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
                                <asp:BoundField DataField="" HeaderText="Ammount to be Deducted">
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
                    <td align="left" valign="top" class="hd">
                        Advance Taken Detail
                    </td>
                </tr>
                <tr>
                    <td align="center" valign="middle" class="cl">
                        <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" Width="720px" Height="30px">
                        <Columns>
                            <asp:BoundField DataField="" HeaderText="Date" />
                            <asp:BoundField DataField="" HeaderText="Ammount" />
                            <asp:BoundField DataField="" HeaderText="No. Of Installment" />
                            <asp:BoundField DataField="" HeaderText="Deduction" />
                            <asp:BoundField DataField="" HeaderText="Balanced Ammount" />
                        </Columns>
                        </asp:GridView> 
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
