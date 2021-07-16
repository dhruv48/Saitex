<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FIBER_MST.ascx.cs" Inherits="Module_Fiber_Controls_FIBER_MST" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
  <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
            <table width="100%">
                <tr>
                    <td valign="top">
                        <table align="left" class="tContentArial" width="100%">
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <table>
                                        <tr>
                                            <td id="tdSave" runat="server">
                                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                                    OnClick="imgbtnSave_Click" OnClientClick="if (!confirm('Are you sure to Save the record ?')) { return false; }"
                                                    ToolTip="Save" ValidationGroup="YM" TabIndex="51" />
                                            </td>
                                            <td id="tdUpdate" runat="server">
                                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                                    OnClick="imgbtnUpdate_Click" OnClientClick="if (!confirm('Are you sure to Update the record ?')) { return false; }"
                                                    ToolTip="Update" ValidationGroup="M1" />
                                            </td>
                                            <td id="tdDelete" runat="server">
                                                <asp:ImageButton ID="imgbtnDelete" runat="server"  ImageUrl="~/CommonImages/del6.png"
                                                    OnClick="imgbtnDelete_Click" OnClientClick="if (!confirm('Are you sure to Delete the record ?')) { return false; }"
                                                    ToolTip="Delete" />
                                            </td>
                                            <td id="tdFind" runat="server">
                                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                                    OnClick="imgbtnFind_Click" OnClientClick="if (!confirm('Are you sure to Find the record ?')) { return false; }"
                                                    ToolTip="Find" TabIndex="52" />
                                            </td>
                                            <td id="tdPrint" runat="server">
                                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                                    OnClick="imgbtnPrint_Click" OnClientClick="if (!confirm('Are you sure to Print the record ?')) { return false; }"
                                                    ToolTip="Print" TabIndex="53" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                                    OnClick="imgbtnClear_Click" OnClientClick="if (!confirm('Are you sure to Clear the record ?')) { return false; }"
                                                    ToolTip="Clear" TabIndex="54" style="height: 41px" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                                    OnClick="imgbtnExit_Click" OnClientClick="if (!confirm('Are you sure to Exit From This Form ?')) { return false; }"
                                                    ToolTip="Exit" TabIndex="55" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                                    ToolTip="Help" TabIndex="56" onclick="imgbtnHelp_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" class="TableHeader td">
                                    <span class="titleheading"><b>Fiber Master</b></span>
                                </td>
                            </tr>
                            <tr>
                                <td align="left" class="td" valign="top">
                                    <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                                        &nbsp;Mode </span>
                                </td>
                            </tr>                                                      
                            <tr>
                                <td class="td" width="100%">
                                    <table width="100%">
                                     <tr>
                                            <td class="tdRight" width="20%">
                                                *Fiber Code
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:TextBox ID="txtFiberCode" runat="server" CssClass="SmallFont TextBoxNo"  Width="125px"  TabIndex="1"></asp:TextBox>
                                                <asp:DropDownList ID="DDLFiberCode"  Width="127px" runat="server" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="DDLFiberCode_SelectedIndexChanged" TabIndex="2">
                                                </asp:DropDownList>                                                
                                            </td>                                            
                                            <td class="tdRight" width="20%">
                                                *Fiber Type
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:DropDownList ID="DDLFiberType" runat="server" AppendDataBoundItems="True" CssClass="SmallFont"
                                                    TabIndex="3" Width="127px">
                                                </asp:DropDownList>
                                            </td>
                                            <td  width="20%"></td>
                                            <td width="15%"></td>
                                        </tr>                                       
                                        <tr>
                                            <td class="tdRight" width="20%">
                                                *Fiber Description</td>
                                            <td class="tdLeft" width="15%" colspan="5">
                                                <asp:TextBox ID="txtFiberDescription" runat="server" CssClass="SmallFont TextBox"
                                                    MaxLength="50" TabIndex="3" Width="99%"></asp:TextBox>
                                            </td>                                            
                                        </tr>
                                        <tr>
                                            <td class="td" colspan="6" ></td>
                                        </tr>
                                        <tr>
                                            <td class="tdRight" width="20%">
                                                *Opening Stock
                                            </td>
                                            <td class="tdLeft" width="15%">
                                                <asp:TextBox ID="txtOpeningBalanceStock" runat="server" CssClass="SmallFont TextBoxNo" onKeyPress="pricevalidate(this);" MaxLength="10" TabIndex="27" Width="125px"></asp:TextBox>                                                
                                            </td>
                                            <td class="tdRight" width="20%">
                                                *Minimum Stock
                                            </td>
                                            <td class="tdleft" width="15%">
                                                <asp:TextBox ID="txtMimimumStock" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10" onKeyPress="pricevalidate(this);" TabIndex="28" Width="125px"></asp:TextBox>
                                            </td>
                                            <td class="tdRight" width="20%">
                                                *Minimum Procure Days
                                            </td>
                                            <td class="tdleft" width="15%">
                                                <asp:TextBox ID="txtMinimumProcureDays" runat="server" CssClass="SmallFont TextBoxNo" onKeyPress="pricevalidate(this);"  MaxLength="12" TabIndex="29" Width="125px"></asp:TextBox>                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="tdRight" >
                                                *Opening Rate
                                            </td>
                                            <td class="tdLeft" >
                                                <asp:TextBox ID="txtOpeningRate" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="16" onKeyPress="pricevalidate(this);"  TabIndex="30" Width="125px"></asp:TextBox>
                                            </td>
                                            <td class="tdRight" >
                                                Reorder Level
                                            </td>
                                            <td class="tdLeft" >
                                                <asp:TextBox ID="txtRecorderLevel" runat="server" CssClass="SmallFont TextBoxNo" onKeyPress="pricevalidate(this);"   MaxLength="10" TabIndex="31" Width="125px"></asp:TextBox>
                                            </td>
                                            <td class="tdRight" >
                                                Reorder Quantity
                                            </td>
                                            <td class="tdLeft" >
                                                <asp:TextBox ID="txtRecorderQuantity" runat="server" CssClass="SmallFont TextBoxNo" onKeyPress="pricevalidate(this);"  MaxLength="10" TabIndex="32" width="125"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>                                            
                                            <td class="tdRight" >
                                                Maximum Stock
                                            </td>
                                            <td class="tdLeft"  colspan="2">
                                                <asp:TextBox ID="txtMaximumStock" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="16" onKeyPress="pricevalidate(this);"    TabIndex="34" Width="125px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>                           
                            
                        </table>
                    </td>
                </tr>
            </table>           
    <%--    </ContentTemplate>
    </asp:UpdatePanel>--%>