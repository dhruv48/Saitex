<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ShadeFamilyMaster.ascx.cs"
    Inherits="Module_OrderDevelopment_Controls_ShadeFamilyMaster" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table align="left" cellpadding="0" cellspacing="0" class="tContentArial">
            <tr>
                <td align="right" class="td" style="text-align: left" valign="top">
                    <table cellpadding="0" cellspacing="0" class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnSave_Click" ToolTip="Save" ValidationGroup="ss" 
                                    TabIndex="1" />
                            </td>
                            <td id="tdFind" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgfind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgfind_Click" ToolTip="Find" ValidationGroup="M1" Width="48" 
                                    TabIndex="2" />
                            </td>
                            <td id="tdUpdate" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="ss" Width="48"
                                    
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to Update this Form')" 
                                    TabIndex="3" />
                            </td>
                            <td align="left" style="height: 46px" width="48">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" 
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to Clear this Form')" 
                                    TabIndex="4" />
                            </td>
                            <td align="left" style="width: 42px; height: 46px">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="43px" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48px" TabIndex="5" />
                            </td>
                            <td width="48">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" 
                                    OnClientClick="javascript:return window.confirm('Are you sure you want to Exit from this Form')" 
                                    TabIndex="6" />
                            </td>
                            <td style="width: 48px">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" TabIndex="7" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td">
                    <b class="tRowColorAdmin">Shade Family Master</b>
                </td>
            </tr>
            <tr>
                <td align="tdLeft" class="td" style="height: 16px" valign="top">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server">
                        </asp:Label>
                        <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                            ShowSummary="False" ValidationGroup="ss" />
                    </span>
                </td>
            </tr>
            <tr>
                <td align="center" valign="top">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td align="right">
                                *Product Type :
                            </td>
                            <td align="left">
                                <asp:DropDownList CssClass="SmallFont" ID="ddlProductType" runat="server" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged" Width="120px" 
                                    ValidationGroup="ss" TabIndex="8">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <%--*Shade Group :--%>*Shade Range :
                            </td>
                            <td align="left">
                                <asp:DropDownList CssClass="SmallFont" ID="ddlShadeGroup" runat="server" Width="120px"
                                    ValidationGroup="ss" TabIndex="9">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                              <%--  *Shade Family Code--%> *Shade Code :
                            </td>
                            <td align="left">
                                <asp:TextBox CssClass=" SmallFont" ID="txtShadeFamilycode" runat="server" AutoPostBack="True"
                                    OnTextChanged="txtShadeFamilycode_TextChanged" Width="120px" 
                                    MaxLength="25" TabIndex="10"></asp:TextBox>
                                <asp:DropDownList CssClass="SmallFont" ID="ddlShadeCodes" runat="server" AutoPostBack="True"
                                    Width="120px" Visible="false" 
                                    OnSelectedIndexChanged="ddlShadeCodes_SelectedIndexChanged" TabIndex="11">
                                </asp:DropDownList>
                                <asp:Label ID="lblmsg" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Remarks :
                            </td>
                            <td align="left" colspan="5">
                                <asp:TextBox CssClass="SmallFont" Width="100%" ID="txtRemarks" runat="server" 
                                    MaxLength="50" TabIndex="12"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="td" width="100%">
                    <table>
                        <tr>
                            <td>
                                <asp:GridView ID="gvShadefamily" runat="server" AllowPaging="True" AllowSorting="True"
                                    AutoGenerateColumns="False" Width="635px" OnPageIndexChanging="gvShadefamily_PageIndexChanging"
                                    OnRowCommand="gvShadefamily_RowCommand" OnSelectedIndexChanged="gvShadefamily_SelectedIndexChanged"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="PRODUCT_TYPE" HeaderText="Product Type" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHADE_GROUP" HeaderText="Shade Range" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="SHADE_FAMILY_CODE" HeaderText="Shade  Code" ItemStyle-HorizontalAlign="center"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REMARKS" HeaderText="Remakrs" ItemStyle-HorizontalAlign="left"
                                            ItemStyle-VerticalAlign="top">
                                            <ItemStyle HorizontalAlign="Left" VerticalAlign="Top"></ItemStyle>
                                        </asp:BoundField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#336799" ForeColor="White" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <br />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please Select Product Type"
            ControlToValidate="ddlProductType" Display="None" InitialValue="0" ValidationGroup="ss"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtShadeFamilycode"
            Display="None" ErrorMessage="Please Enter Shade Family Code" ValidationGroup="ss"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Please Select Shade Group"
            ControlToValidate="ddlShadeGroup" Display="None" InitialValue="0" ValidationGroup="ss"></asp:RequiredFieldValidator>
    </ContentTemplate>
</asp:UpdatePanel>
