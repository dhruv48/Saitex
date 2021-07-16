<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SubHeadNameMaster.ascx.cs"
    Inherits="Module_HRMS_Controls_SubHeadNameMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial" width="100%">
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server" align="left">
                                <asp:ImageButton ID="imgbntSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                    Width="48" Height="41px" ValidationGroup="M1" TabIndex="4" OnClick="imgbntSave_Click">
                                </asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                    Width="48" Height="41" ValidationGroup="M1" TabIndex="5" OnClick="imgbtnUpdate_Click">
                                </asp:ImageButton>
                            </td>
                            <td id="tdFind" runat="server" valign="top">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                    Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                                    Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                                    TabIndex="6" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                    Width="48" Height="41" OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')"
                                    TabIndex="8" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                    Width="48" Height="41" TabIndex="9" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" TabIndex="10" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server" align="left">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                    Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top" class="tRowColorAdmin td">
                    <span class="titleheading">Sub Head Master</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                        ShowSummary="False" ValidationGroup="M1" />
                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td class="td" align="left">
                    <table width="75%">
                        <tr>
                            <td align="right" valign="top">
                                *Head Name
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="cmbHead" runat="server" Width="150px" CssClass="TextBox SmallFont UpperCase"
                                    Height="20px" TabIndex="1">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cmbHead"
                                    ErrorMessage="Select Head" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                *Sub Head Name
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSubHeadName" runat="server" Width="150px" ValidationGroup="M1"
                                    CssClass="TextBox SmallFont  UpperCase"></asp:TextBox>
                                &nbsp;&nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtSubHeadName" ErrorMessage="Enter SubHead" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                            <td align="right" valign="top">
                                Salary Slip Field Name
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSlipFieldName" CssClass="TextBox SmallFont UpperCase" runat="server"
                                    Width="150px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtSlipFieldName"
                                    ErrorMessage="Enter SubHead" ValidationGroup="M1"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr id="trSubHeadCategory" runat="server">
                            <td align="right" valign="top">
                                *Sub Head Catagory
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:RadioButtonList ID="radSubHeadCategory" RepeatColumns="2" SelectedValue='<%# Bind("SUBH_CAT") %>'
                                    runat="server" RepeatLayout="Flow" CssClass="TextBox SmallFont" RepeatDirection="Horizontal"
                                    AutoPostBack="True" OnSelectedIndexChanged="radSubHeadCategory_SelectedIndexChanged">
                                    <asp:ListItem Text="Salary" Selected="True" Value="S"></asp:ListItem>
                                    <asp:ListItem Text="Allowances" Value="A"></asp:ListItem>
                                    <asp:ListItem Text="Deduction" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="Perquisites" Value="P"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right" valign="top">
                                Salary Type
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:RadioButtonList ID="radSalaryType" Visible="true" SelectedValue='<%# Bind("SUBH_SAL_TYPE") %>'
                                    CssClass="TextBox SmallFont" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Basic" Selected="True" Value="B"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:RadioButtonList ID="radSalaryDeduction" Visible="false" SelectedValue='<%# Bind("SUBH_SAL_TYPE") %>'
                                    CssClass="TextBox SmallFont" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="PF" Value="P"></asp:ListItem>
                                    <asp:ListItem Text="Loans" Value="L"></asp:ListItem>
                                    <asp:ListItem Text="Other" Value="O"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td align="right" valign="top">
                                Sub Head Type
                            </td>
                            <td align="center" valign="top">
                                <b>:</b>
                            </td>
                            <td align="left" valign="top">
                                <asp:RadioButtonList ID="radSubHeadType" Visible="true" SelectedValue='<%# Bind("SUBH_TYPE") %>'
                                    CssClass="TextBox SmallFont" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="Rupees(Rs.)" Selected="True" Value="R"></asp:ListItem>
                                    <asp:ListItem Text="% of Basic" Value="P"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Pay Mode
                            </td>
                            <td align="center" valign="top">
                                <strong>:</strong>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="DDLPayMode" Width="150px" CssClass="TextBox SmallFont" runat="server">
                                    <asp:ListItem Value="M">MONTHLY</asp:ListItem>
                                    <asp:ListItem Value="Y">YEARLY</asp:ListItem>
                                    <asp:ListItem Value="O">ONCE</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top">
                                Round In
                            </td>
                            <td align="center" valign="top">
                                <strong>:</strong>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TxtRoundIn" Width="150px" CssClass="TextBoxNo SmallFont" onkeyup="pricevalidate(this);"
                                    runat="server"></asp:TextBox>
                            </td>
                            <td align="right" valign="top">
                                Round Style
                            </td>
                            <td align="center" valign="top">
                                <strong>:</strong>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="DDLRoundStyle" Width="150px" CssClass="TextBox SmallFont" runat="server">
                                    <asp:ListItem Value="R">ROUND</asp:ListItem>
                                    <asp:ListItem Value="U">UPPER</asp:ListItem>
                                    <asp:ListItem Value="L">LOWER</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Calculate on Days
                            </td>
                            <td align="center" valign="top">
                                <strong>:</strong>
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="DDLCalculateOnLOP" Width="150px" CssClass="TextBox SmallFont"
                                    runat="server">
                                    <asp:ListItem Value="Y">YES</asp:ListItem>
                                    <asp:ListItem Value="N">NO</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right" valign="top">
                                Account Code
                            </td>
                            <td align="center" valign="top">
                                <strong>:</strong>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="TxtAccountNo" Width="150px" CssClass="TextBoxNo SmallFont" runat="server"></asp:TextBox>
                            </td>
                            <td align="left" colspan="3" valign="top">
                                <asp:TextBox ID="txtSubh_Head_Id" Text="" Width="1" Visible="false" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" align="left">
                    <cc2:Grid ID="Grid1" runat="server" Width="90%" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="50" AutoGenerateColumns="False" OnSelect="Grid1_Select" AutoPostBackOnSelect="True">
                        <Columns>
                            <cc2:Column DataField="HEAD_NAME" HeaderText="HeadName" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_NAME" HeaderText="Sub Head Name" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="Headcat" HeaderText="Sub Head Catagory" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="subHeadCat" HeaderText="Salary Type" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="RupeesPercent" HeaderText="Sub Head Type" ItemStyle-Width="5%"
                                HeaderStyle-Width="5%">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_SLIP_FLD_NAME" HeaderText="Slip Field Name" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="PAYMODE" HeaderText="Pay Mode" ItemStyle-Width="10%" HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="CALONDAYS" HeaderText="Calculate On Days" ItemStyle-Width="10%"
                                HeaderStyle-Width="10%">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_ID" HeaderText="SubHeadId" Visible="false">
                            </cc2:Column>
                            <cc2:Column DataField="HEAD_ID" HeaderText="HeadName" Visible="false">
                            </cc2:Column>
                            <cc2:Column DataField="PAY_MODE" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="ROUND_IN" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="ROUND_STYLE" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="ACCOUNT_CODE" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="CAL_ON_DAYS" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_CAT" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_SAL_TYPE" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                            <cc2:Column DataField="SUBH_TYPE" Visible="false" ItemStyle-Width="1%">
                            </cc2:Column>
                        </Columns>
                    </cc2:Grid>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
