<%@ Control Language="C#" AutoEventWireup="true" CodeFile="STRateMst.ascx.cs" Inherits="Module_SewingThread_Controls_STRateMst" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        margin-left: 2px;
    }
    .c2
    {
        margin-left: 4px;
        width: 150px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="tContentArial">
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click"
                                    TabIndex="1" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click" 
                                    TabIndex="2"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" 
                                    ValidationGroup="M1" TabIndex="3">
                                </asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click" 
                                    TabIndex="4"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click" 
                                    TabIndex="5"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click" 
                                    TabIndex="6"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click" 
                                    TabIndex="7"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click" 
                                    TabIndex="8"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Sewing Thread Rate Master</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label></span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td valign="top" colspan="3">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ValidationGroup="M1" ShowSummary="False" />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                </strong>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="height: 23px">
                                *Article Code :
                            </td>
                            <td align="left" valign="top" style="height: 23px">
                                <asp:DropDownList ID="ddlArticleCode" runat="server" AppendDataBoundItems="true"
                                    Width="133px" DataTextField="YARN_CODE" DataValueField="YARN_CODE" TabIndex="9"
                                    CssClass="SmallFont" ValidationGroup="M1">
                                </asp:DropDownList>
                                <cc2:ComboBox ID="cmbArticleCode" runat="server" Width="133px" Height="200px" AutoPostBack="True"
                                    DataTextField="ARTICLE_CODE" DataValueField="SHADE_GROUP" EnableLoadOnDemand="True"
                                    EmptyText="Select Article" TabIndex="10" MenuWidth="850px" OnLoadingItems="cmbArticleCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbArticleCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            Article Code
                                        </div>
                                        <div class="header c2">
                                            Shade Group</div>
                                        <div class="header c2">
                                            Opening Rate
                                        </div>
                                        <div class="header c2">
                                            Transfer Price</div>
                                        <div class="header c2">
                                            Sale Price</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <%# Eval("ARTICLE_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("SHADE_GROUP")%></div>
                                        <div class="item c2">
                                            <%# Eval("OP_RATE")%></div>
                                        <div class="item c2">
                                            <%# Eval("TRANS_PRICE")%></div>
                                        <div class="item c2">
                                            <%# Eval("SALE_PRICE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top" style="height: 23px">
                                *Shade Group :
                            </td>
                            <td align="left" valign="top" style="height: 23px">
                                <asp:DropDownList CssClass="SmallFont" ID="ddlShadeGroup" runat="server" Width="133px"
                                    ValidationGroup="M1" TabIndex="11">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Opening Rate :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtOpeningRate" runat="server" CssClass="TextBoxNo" MaxLength="15"
                                    TabIndex="12" ValidationGroup="M1" Width="130px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtOpeningRate">
                                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Transfer Price :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtTransferPrice" runat="server" CssClass="TextBoxNo" MaxLength="15"
                                    TabIndex="13" ValidationGroup="M1" Width="130px"></asp:TextBox>
                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtTransferPrice">
                                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Sale Price :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSalePrice" runat="server" CssClass="TextBoxNo" MaxLength="15"
                                    TabIndex="14" ValidationGroup="M1" Width="130px"></asp:TextBox>
                                     <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                                    TargetControlID="txtSalePrice">
                                                </cc1:FilteredTextBoxExtender>
                                                    
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:RangeValidator ID="RVOpeningBalance" ControlToValidate="txtOpeningRate" runat="server"
            ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
            MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RangeValidator ID="RVTransferPrice" ControlToValidate="txtTransferPrice" runat="server"
            ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
            MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RangeValidator ID="RVSalePrice" ControlToValidate="txtSalePrice" runat="server"
            ErrorMessage="Only numeric value allowed" Display="None" MaximumValue="1000000"
            MinimumValue="0" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <asp:RequiredFieldValidator ID="RFOpeningRate" runat="server" ErrorMessage="Please enter Opening Rate"
            ControlToValidate="txtOpeningRate" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFTransferPrice" runat="server" ErrorMessage="Please enter Transfer Price"
            ControlToValidate="txtTransferPrice" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFSalePrice" runat="server" ErrorMessage="Please enter Sale Price"
            ControlToValidate="txtSalePrice" ValidationGroup="M1"></asp:RequiredFieldValidator>
    </ContentTemplate>
</asp:UpdatePanel>
