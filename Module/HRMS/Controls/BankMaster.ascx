<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankMaster.ascx.cs" Inherits="Module_HRMS_Controls_BankMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
    .header
    {
        margin-left: 4px;
    }
    .c1
    {
        width: 50px;
    }
    .c2
    {
        margin-left: 4px;
        width: 90px;
    }
    .c3
    {
        margin-left: 4px;
        width: 80px;
    }
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
    a:hover
    {
        color: Teal;
    }
    .ob_gMCont_DT
    {
        overflow: hidden;
    }
    .ob_gMCont
    {
        position: relative;
    }
</style>
<table runat="server" id="tblBankMaster"  align="left" class="tContentArial">
    <tr>
        <td id="Td1" align="left"  runat="server" class="td">
            <table cellpadding="0" cellspacing="0" border="0">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                            Width="55px" Height="40px" ValidationGroup="M1" TabIndex="4" OnClick="imgbntSave_Click">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" TabIndex="5" OnClick="imgbtnUpdate_Click">
                        </asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" ToolTip="Delete" ImageUrl="~/CommonImages/del6.png"
                            Width="48" Height="41" ValidationGroup="M1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                            TabIndex="6" OnClick="imgbtnDelete_Click2"></asp:ImageButton>
                    </td>
                     <td id="tdFind" runat="server" valign="top">
                            <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                Width="48" Height="41" TabIndex="7" onclick="imgbtnFind_Click" ></asp:ImageButton>
                     </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                            Width="48" Height="41" OnClientClick="javascript:return window.confirm('Are you sure you want to clear this record')"
                            TabIndex="8" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                            Width="48" Height="41" TabIndex="9" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="10" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                            Width="48" Height="41" TabIndex="11"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3" valign="top" class="tRowColorAdmin td">
            <span class="titleheading">Bank Master</span>
        </td>
    </tr>
       <tr>
        <td align="left" colspan="3" valign="top" class="td">
         <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" 
                ShowSummary="False" ValidationGroup="M1" />
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td class="td">
            <table>
                <tr>
                    <td align="right"  valign="top">
                        *Bank Code
                    </td>
                    <td align="center"  valign="top">
                        <b>:</b>
                    </td>
                    <td align="left"  valign="top">
                        <asp:TextBox ID="txtBankCode" runat="server" Width="150px" MaxLength="10" TabIndex="1"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtBankCode" Display="Dynamic"
                        ErrorMessage="Pls. Enter Bank Code" ValidationGroup="M1"></asp:RequiredFieldValidator>
                                              
                     <td align="left"  valign="top">   
                            <cc2:ComboBox ID="ddlBankCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                    OnLoadingItems="ddlBankCode_LoadingItems" DataTextField="BANK_NAME" DataValueField="BANK_CODE"
                                                    Width="150px" MenuWidth="350px" Height="200px" CssClass="SmallFont" TabIndex="1"
                                                    EmptyText="Find Bank" OnSelectedIndexChanged="ddlBankCode_SelectedIndexChanged">
                                                    <HeaderTemplate>
                                                        <div class="header c1">
                                                          Bank Code</div>
                                                        <div class="header c2">
                                                          Bank Name</div>
                                                        </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="item c1">
                                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("BANK_CODE") %>' /></div>
                                                        <div class="item c2">
                                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("BANK_NAME") %>' /></div>
                                                      </ItemTemplate>
                                                    <FooterTemplate>
                                                        Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                    </FooterTemplate>
                                                </cc2:ComboBox>                  
                                               </td> 
                       
                    </td>
                </tr>
                <tr>
                    <td align="right"  valign="top">
                        *Bank Name
                    </td>
                    <td align="center"  valign="top">
                        <b>:</b>
                    </td>
                    <td align="left"  valign="top">
                        <asp:TextBox ID="txtBankName" runat="server" Width="150px" ValidationGroup="M1" TabIndex="2"
                            CssClass="UpperCase"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBankName" Display="Dynamic"
                            ErrorMessage="Pls. Enter Bank Name" ValidationGroup="M1"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left"  valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="right"  valign="top">
                        Remarks
                    </td>
                    <td align="center"  valign="top">
                        <b>:</b>
                    </td>
                    <td align="left"  valign="top">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="150px" ValidationGroup="M1" TextMode="MultiLine"
                            TabIndex="3"></asp:TextBox>
                    </td>
                    <td align="left" valign="top">
                    </td>
                </tr>
                <tr>
                    <td align="right"  width="30%">
                        Status
                    </td>
                    <td align="center"  width="2%">
                        <stron
                    </td>
                    <td align="left" valign="top" >
                        <asp:CheckBox ID="chkActive" runat="server" TabIndex="4" />
                    </td>
                     <td align="left" valign="top" >
                     </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" class="td">
            <cc2:Grid ID="Grid1" runat="server" Width="100%" AllowAddingRecords="False"  PageSize="10" AutoGenerateColumns="False" OnSelect="Grid1_Select">
                <Columns>
                    <cc2:Column DataField="BANK_CODE" HeaderText="BANK CODE" Width="100px">
                    </cc2:Column>
                    <cc2:Column DataField="BANK_NAME" HeaderText="BANK NAME" Width="200px">
                    </cc2:Column>
                    <cc2:Column DataField="BANK_REMARKS" HeaderText="Remarks" Width="250px">
                    </cc2:Column>
                </Columns>
            </cc2:Grid>
            
        </td>
    </tr>
</table>
