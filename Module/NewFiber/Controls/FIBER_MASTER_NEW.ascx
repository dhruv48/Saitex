<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FIBER_MASTER_NEW.ascx.cs" Inherits="Module_Fiber_Controls_FIBER_MASTER_NEW" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc11" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<style type="text/css">
    .item {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1;
        *display: inline;
        overflow: hidden;
        white-space: nowrap;
    }

    .header {
        margin-left: 2px;
    }

    .c1 {
        width: 80px;
    }

    .c2 {
        margin-left: 4px;
        width: 150px;
    }

    .c3 {
        margin-left: 4px;
        width: 250px;
    }

    .c4 {
        margin-left: 4px;
        width: 150px;
    }

    .c5 {
        margin-left: 4px;
        width: 100px;
    }

    .TextBoxNo {
        height: 22px;
    }
</style>


<asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>
        <table width="100%" style="border: 0px; border-style: inset;">
            <tr>
                <td align="left" class="td" valign="top">
                    <table class="tContentArial">
                        <tr>
                            <td>
                                <asp:Label ID="lblMode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>

                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                                    ToolTip="Save" ValidationGroup="YM" TabIndex="21"
                                    OnClick="imgbtnSave_Click" Style="width: 48px" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    ToolTip="Update" ValidationGroup="M1" OnClick="imgbtnUpdate_Click" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" ImageUrl="~/CommonImages/del6.png"
                                    ToolTip="Delete" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                    ToolTip="Find" TabIndex="22" OnClick="imgbtnFind_Click" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" TabIndex="53" OnClick="imgbtnPrint_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                                    ToolTip="Clear" TabIndex="54" Style="height: 41px"
                                    OnClick="imgbtnClear_Click" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                                    ToolTip="Exit" TabIndex="55" OnClick="imgbtnExit_Click" />
                            </td>

                            <td>
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" TabIndex="56" />
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnList" runat="server" ImageUrl="~/CommonImages/list.jpg"
                                    ToolTip="Clear" TabIndex="54" Style="height: 41px"
                                    OnClick="imgbtnList_Click" />
                            </td>
                        </tr>
                    </table>
                    <table width="100%" class="tContentArial">
                        <tr>
                            <td align="center" class="TableHeader td" valign="top">
                                <span class="titleheading"><b>Fiber Master</b></span>
                            </td>
                        </tr>

                        <tr>
                            <td class="td" align="left" width="50%">
                                <table class="tContentArial">
                                    <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               <ContentTemplate>--%>
                                    <tr>
                                        <td align="right" valign="top">*Fiber Code:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtFiberCode" runat="server" CssClass="SmallFont TextBox UpperCase" Width="125px"
                                                TabIndex="1" AutoPostBack="True" OnTextChanged="txtFiberCode_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV1" runat="server"
                                                ControlToValidate="txtFiberCode" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <%-- <asp:DropDownList ID="DDLFiberCode" Width="130px" 
                            CssClass="SmallFont TextBox UpperCase" runat="server" 
                            AutoPostBack="True" 
                            onselectedindexchanged="DDLFiberCode_SelectedIndexChanged">
                        </asp:DropDownList>--%>
                                            <cc2:ComboBox ID="DDLFiberCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                                DataTextField="Combined" DataValueField="FIBER_CODE" EmptyText="Find Fiber Code" EnableLoadOnDemand="true"
                                                Height="200px" MenuWidth="400" OnLoadingItems="DDLFiberCode_LoadingItems" OnSelectedIndexChanged="DDLFiberCode_SelectedIndexChanged"
                                                TabIndex="2" Width="150px" Visible="False">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        FIBER CODE
                                                    </div>

                                                    <div class="header c2">
                                                        FIBER Description
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FIBER_CODE") %>' />
                                                    </div>
                                                    <div class="item c2">
                                                        <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FIBER_DESC") %>' />
                                                    </div>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                                        out of
                                                        <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>

                                        </td>
                                        <td align="right" valign="top">Fiber&nbsp;Category:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlfibercat" Width="130px"
                                                CssClass="SmallFont TextBox UpperCase" TabIndex="2" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlfibercat_SelectedIndexChanged">
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="ddlfibercat" runat="server" CssClass="SmallFont TextBoxNo" Width="125px"></asp:TextBox>--%>
                                        </td>
                                        <td align="right" valign="top">Sub Fiber Cat:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlsubfiber_cat" Width="130px" CssClass="SmallFont TextBox UpperCase" runat="server" TabIndex="3">
                                            </asp:DropDownList>

                                        </td>

                                    </tr>

                                    <tr>
                                        <td align="right" valign="top">Fiber&nbsp;Description:
                                        </td>
                                        <td align="left" valign="top" colspan="1">
                                            <asp:TextBox ID="txtdescription" runat="server" TabIndex="2" CssClass="SmallFont TextBox UpperCase" MaxLength="75" Width="290px"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">Uom1:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddluom1" Width="130px" CssClass="SmallFont TextBox UpperCase" TabIndex="5" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" valign="top">Uom2:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddluom2" Width="130px" CssClass="SmallFont TextBox UpperCase" TabIndex="5" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td align="right" valign="top">Fiber&nbsp;Length:<%-- Length Type:--%>
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlLengthType" Width="130px" CssClass="SmallFont TextBox UpperCase" TabIndex="6" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="right" valign="top">Length Value:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtlengthvalue" runat="server" CssClass="SmallFont TextBox UpperCase" TabIndex="7" Width="125px" MaxLength="20"></asp:TextBox>
                                        </td>


                                        <td align="right" valign="top" visible="false">Fiber Lusture:
                                        </td>
                                        <td align="left" valign="top" visible="false">
                                            <asp:DropDownList ID="ddlfiberlusture" Width="130px" CssClass="SmallFont TextBox UpperCase" TabIndex="8" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">Fiber Denier:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtFiberDenier" runat="server"
                                                CssClass="SmallFont TextBox UpperCase" TabIndex="9" Width="125px"
                                                OnTextChanged="txtFiberDenier_TextChanged" MaxLength="50"></asp:TextBox>
                                        </td>
                                        <td align="right" valign="top">Fancy Effect:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:DropDownList ID="ddlFancyEffect" Width="130px" CssClass="SmallFont TextBox UpperCase" TabIndex="10" runat="server">
                                            </asp:DropDownList>
                                        </td>




                                        <td align="right" valign="top" visible="false">Fiber Tenacity:
                                        </td>
                                        <td align="left" valign="top" visible="false">
                                            <asp:TextBox ID="txtTanacity" runat="server" CssClass="SmallFont TextBox UpperCase" TabIndex="11" Width="125px" MaxLength="200"></asp:TextBox>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top"></td>
                                        <td align="left" valign="top"></td>

                                    </tr>




                                    <%--Commented By Nishant Rai at 26-07-2013--%>

                                    <%--                <tr>
                    <td align="right" valign="top" visible="false">
                        FinNess Fiber:
                    </td>
                    <td align="left" valign="top" visible="false">
                        <asp:TextBox ID="txtfinness" runat="server" CssClass="SmallFont TextBox UpperCase"  TabIndex="7" Width="125px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        Moisture:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtmosture" runat="server" CssClass="SmallFont TextBox UpperCase"  TabIndex="8" Width="125px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        EndUse:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtenduse" runat="server" CssClass="SmallFont TextBox UpperCase"  TabIndex="9" Width="125px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Fiber Appearance:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtfiberappearance" runat="server"  TabIndex="10" CssClass="SmallFont TextBox UpperCase"
                            Width="125px"></asp:TextBox>
                    </td>
                    <td align="right" valign="top">
                        BioLogic Property:
                    </td>
                    <td align="left" valign="top">
                        <asp:TextBox ID="txtbio_property" runat="server" CssClass="SmallFont TextBox UpperCase"  TabIndex="11" Width="125px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Fiber Properties:
                    </td>
                    <td align="left" valign="top" colspan="6">
                        <asp:TextBox ID="txtproperties" runat="server"  TabIndex="12" CssClass="SmallFont TextBox UpperCase" MaxLength="100" Width="675px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        Description:
                    </td>
                    <td align="left" valign="top" colspan="6">
                       
                    </td>
                </tr>--%>

                                    <tr>
                                        <td colspan="6"></td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">*Opening Stock:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtOpeningBalanceStock" runat="server" CssClass="SmallFont TextBoxNo"
                                                MaxLength="7" TabIndex="12" Width="125px"
                                                OnTextChanged="txtOpeningBalanceStock_TextChanged"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV2" runat="server" ControlToValidate="txtOpeningBalanceStock"
                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <cc11:FilteredTextBoxExtender ID="f1" runat="server"
                                                TargetControlID="txtOpeningBalanceStock"
                                                FilterType="Custom, Numbers" ValidChars="." />

                                        </td>
                                        <td align="right" valign="top">*Minimum Stock:
                                        </td>
                                        <td class="tdleft" width="15%">
                                            <asp:TextBox ID="txtMimimumStock" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                TabIndex="13" Width="125px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV3" runat="server" ControlToValidate="txtMimimumStock"
                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <cc11:FilteredTextBoxExtender ID="f2" runat="server"
                                                TargetControlID="txtMimimumStock"
                                                FilterType="Custom, Numbers" ValidChars="." />
                                        </td>
                                        <td align="right" valign="top">&nbsp;*Min Procure Days:
                                        </td>

                                        <td class="tdleft" width="15%">
                                            <asp:TextBox ID="txtMinimumProcureDays" runat="server" CssClass="SmallFont TextBoxNo"
                                                MaxLength="12" TabIndex="14" Width="125px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV5" runat="server" ControlToValidate="txtMinimumProcureDays"
                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <cc11:FilteredTextBoxExtender ID="f3" runat="server"
                                                TargetControlID="txtMinimumProcureDays"
                                                FilterType="Custom, Numbers" ValidChars="." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" valign="top">*Opening Rate:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtOpeningRate" runat="server" CssClass="SmallFont TextBoxNo" MaxLength="10"
                                                TabIndex="15" Width="125px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV6" runat="server" ControlToValidate="txtOpeningRate"
                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <cc11:FilteredTextBoxExtender ID="f4" runat="server"
                                                TargetControlID="txtOpeningRate"
                                                FilterType="Custom, Numbers" ValidChars="." />
                                        </td>
                                        <td align="right" valign="top">Reorder Level:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRecorderLevel" runat="server" CssClass="SmallFont TextBoxNo"
                                                MaxLength="10" TabIndex="16" Width="125px"></asp:TextBox>

                                            <cc11:FilteredTextBoxExtender ID="FilteredTextBoxtxtRecorderLevel" runat="server"
                                                TargetControlID="txtRecorderLevel"
                                                FilterType="Custom, Numbers" ValidChars="." />
                                        </td>
                                        <td align="right" valign="top">*Reorder Quantity
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtRecorderQuantity" runat="server" CssClass="SmallFont TextBoxNo"
                                                MaxLength="10" TabIndex="17" Width="125"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV7" runat="server" ControlToValidate="txtRecorderQuantity"
                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <cc11:FilteredTextBoxExtender ID="F5" runat="server"
                                                TargetControlID="txtRecorderQuantity"
                                                FilterType="Custom, Numbers" ValidChars="." />
                                        </td>
                                    </tr>

                                    <tr>
                                        <td align="right" valign="top">*Maximum Stock:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="txtMaximumStock" runat="server" TabIndex="18" CssClass="SmallFont TextBoxNo" MaxLength="12"
                                                Width="125px"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RFV8" runat="server" ControlToValidate="txtMaximumStock"
                                                Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <cc11:FilteredTextBoxExtender ID="F6" runat="server"
                                                TargetControlID="txtMaximumStock"
                                                FilterType="Custom, Numbers" ValidChars="." />

                                        </td>
                                        <td align="right" valign="top">Fiber Supplier:
                                        </td>
                                        <td align="left" valign="top">
                                            <cc2:ComboBox ID="txtPartyCodecmb" runat="server" AutoPostBack="True"
                                                EnableLoadOnDemand="true" TabIndex="19"
                                                OnLoadingItems="txtPartyCodecmb_LoadingItems"
                                                EmptyText="N/A"
                                                EnableVirtualScrolling="true" Width="150px" MenuWidth="400px"
                                                Height="200px" OnTextChanged="txtPartyCodecmb_TextChanged">


                                                <%--DataTextField="PRTY_NAME" DataValueField="PRTY_CODE"   --%>
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        Code
                                                    </div>
                                                    <div class="header c3">
                                                        NAME
                                                    </div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' />
                                                    </div>
                                                    <div class="item c3">
                                                        <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>
                                                </FooterTemplate>

                                            </cc2:ComboBox>

                                        </td>
                                        <td align="right" valign="top">Remarks:
                                        </td>
                                        <td align="left" valign="top">
                                            <asp:TextBox ID="Txtremark" runat="server" CssClass="SmallFont TextBox UpperCase" TabIndex="20" Width="125px" MaxLength="1000"></asp:TextBox>
                                        </td>

                                        <%--<asp:TextBox ID="txtFiberSupplier" runat="server"  TabIndex="22" CssClass="SmallFont TextBoxNo" 
                            Width="125px"></asp:TextBox>--%>
                                        <%--<asp:TextBox ID="txtpartycode" runat="server"  ></asp:TextBox>--%>
                            </td>
                        </tr>
                        <tr>

                            <td align="right" valign="top">KG/Bail:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="Txtuomperbail" runat="server" CssClass="SmallFont TextBoxNo" TabIndex="20" Width="125px" MaxLength="1000"></asp:TextBox>
                            </td>

                            <td align="right" valign="top">Polyster:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtpolyster" runat="server" CssClass="SmallFont TextBoxNo" TabIndex="20" Width="125px" MaxLength="1000"></asp:TextBox>
                            </td>

                            <td align="right" valign="top">Viscos:
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtviscose" runat="server" CssClass="SmallFont TextBoxNo" TabIndex="20" Width="125px" MaxLength="1000"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">QC&nbsp;Required	
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:RadioButtonList ID="rad_qc_req" runat="server" CssClass="SmallFont" RepeatColumns="4"
                                    Width="100px" RepeatDirection="Horizontal" TabIndex="18" Height="11px">
                                    <asp:ListItem Value="yes">Yes</asp:ListItem>
                                    <asp:ListItem Value="No">No</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>

                        <%--                <tr>
                    <td align="right">
                        *Maximum Stock:
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtMaximumStock" runat="server"  TabIndex="20" CssClass="SmallFont TextBoxNo" MaxLength="16"
                            Width="125px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RFV8" runat="server" ControlToValidate="txtMaximumStock"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                    </td>
                    
               
                
             
                    <td align="right">
                        Fiber Supplier:
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="TextBox1" runat="server"  TabIndex="20" CssClass="SmallFont TextBoxNo" MaxLength="16"
                            Width="125px"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMaximumStock"
                                                    Display="Dynamic" ErrorMessage="*" SetFocusOnError="True"
                                                    ValidationGroup="YM"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                    &nbsp;
                    </td>
                    
                </tr>--%>
                        <%-- </ContentTemplate>
                 </asp:UpdatePanel>--%>
                    </table>
                </td>
            </tr>

            <tr>
                <td align="left" class="td SmallFont" valign="top" width="100%">
                    <table width="80%">
                        <tr bgcolor="#006699">
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Lot No</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Grade</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Qty</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>UOM</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>No of Bale</b></span>
                            </td>

                            <td align="left" class="tdLeft SmallFont" valign="top">
                                <span class="titleheading"><b>Weight. of Unit</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top" runat="server" visible="false">
                                <span class="titleheading"><b>Date of Manufacturing</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top" runat="server" visible="false">
                                <span class="titleheading"><b>Material Status</b></span>
                            </td>
                            <td align="left" class="tdLeft SmallFont" valign="top">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtLotNo" Width="95%" class="SmallFont uppercase" runat="server" MaxLength="14"></asp:TextBox>
                                <br />
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtGrade" class="SmallFont uppercase" Width="95%" runat="server" MaxLength="14"></asp:TextBox>
                                <br />
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtQty" Width="95%" class="SmallFont" runat="server" AutoPostBack="True"
                                    OnTextChanged="txtQty_TextChanged" MaxLength="9"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQty"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>

                                <cc11:FilteredTextBoxExtender ID="FiltertxtQty" runat="server"
                                    TargetControlID="txtQty"
                                    FilterType="Custom, Numbers" ValidChars="." />
                            </td>
                            <td align="left" valign="top">
                                <asp:DropDownList ID="ddlUOM" class="SmallFont" runat="server" Width="95%">
                                </asp:DropDownList>
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtNoofUnit" runat="server" class="SmallFont" Width="95%" AutoPostBack="True"
                                    OnTextChanged="txtNoofUnit_TextChanged" MaxLength="5"></asp:TextBox>
                                <%--  <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtNoofUnit"
                                            Display="Dynamic" ErrorMessage="Please Enter No of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                                <cc11:FilteredTextBoxExtender ID="FilteredTextBoxtxtNoofUnit" runat="server"
                                    TargetControlID="txtNoofUnit"
                                    FilterType="Custom, Numbers" ValidChars="." />

                            </td>

                            <td align="left" valign="top">
                                <asp:TextBox ID="txtWeightofUnit" class="SmallFont" runat="server" Width="95%" Enabled="False"></asp:TextBox>
                                <%-- <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtWeightofUnit"
                                            Display="None" ErrorMessage="Please Enter Weight of Unit in Numeric &amp; Precision Should be 9 and Scale 2   "
                                            MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>--%>
                            </td>
                            <td align="left" valign="top" runat="server" visible="false">
                                <asp:TextBox ID="txtDofMfd" class="SmallFont" Width="95%" runat="server"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDofMfd"
                                            Display="Dynamic" ErrorMessage="Please Enter Date Of Manufacutring" SetFocusOnError="True"
                                            ValidationGroup="YM"></asp:RequiredFieldValidator>--%>
                                <cc11:CalendarExtender ID="CalendarExtender2" runat="server" PopupPosition="TopLeft"
                                    TargetControlID="txtDofMfd">
                                </cc11:CalendarExtender>
                                <cc11:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                                    MaskType="Date" PromptCharacter="_" TargetControlID="txtDofMfd">
                                </cc11:MaskedEditExtender>

                                <br />
                            </td>
                            <td align="left" valign="top" runat="server" visible="false">
                                <asp:DropDownList ID="ddlMaterialStatus" class="SmallFont" runat="server" Width="95%">
                                    <%--<asp:ListItem Value="0">------Select---------</asp:ListItem>--%>
                                    <%--<asp:ListItem>UnCheck</asp:ListItem>
                                            <asp:ListItem>Extracted</asp:ListItem>
                                            <asp:ListItem>Rejected</asp:ListItem>--%>

                                    <asp:ListItem>Approved</asp:ListItem>
                                    <asp:ListItem>Rejected</asp:ListItem>
                                    <asp:ListItem>Hold</asp:ListItem>
                                </asp:DropDownList>
                                <br />
                            </td>
                            <td align="left" valign="top">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="BtnBOMSave" class="SmallFont" runat="server" OnClick="BtnBOMSave_Click"
                                                Text="Save" ValidationGroup="YM" Width="70px" CausesValidation="false" />

                                        </td>

                                        <td>
                                            <asp:Button ID="BtnBOMCancel" runat="server" class="SmallFont" OnClick="BtnBOMCancel_Click" CausesValidation="false"
                                                Text="Cancel" Width="70px" />
                                        </td>
                                    </tr>


                                </table>
                            </td>
                        </tr>

                    </table>
                </td>
            </tr>
            <tr>
                <td align="left" class="td SmallFont" valign="top" width="60%">
                    <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                        Font-Bold="False" OnRowCommand="grdSub_trnArticleDetail_RowCommand" ShowFooter="True"
                        Width="80%">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px">
                                <ItemTemplate>
                                    <asp:Label ID="txtSubTrnUNIQUE_ID" runat="server" Text='<%# Bind("UNIQUE_ID") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lot No">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lbtlotno" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Grade">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBOMUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("GRADE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="QTY">
                                <ItemTemplate>
                                    <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblUom" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No of Bale">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblNoUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Weight of Unit">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblWeightofUnit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of Mfd" Visible="false">
                                <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="lblBOMValueQty" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DATE_OF_MFG","{0:dd/MM/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Material Status" Visible="false">
                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                <ItemTemplate>
                                    <asp:Label ID="txtBOMArticleCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Button ID="lnkBOMEdit" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>' Width="70px"
                                        CommandName="BOMEdit" TabIndex="12" Text="Edit" />
                                    <asp:Button ID="lnkBOMDelete" class="SmallFont" runat="server" CommandArgument='<%# Eval("UNIQUE_ID") %>' Width="70px"
                                        CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this BOM Detail?');"
                                        TabIndex="12" Text="Delete" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle CssClass="RowStyle " />
                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" />
                        <PagerStyle CssClass="PagerStyle" />
                        <HeaderStyle BackColor="#336699" CssClass="SmallFont" ForeColor="White" />
                    </asp:GridView>
                </td>
            </tr>

            <tr>
                <td>

                    <%--    <asp:Panel ID="pnlBOM" runat="server" BackColor="#C5E7F1" BorderColor="Desktop" BorderStyle="Solid"
                                    BorderWidth="5px" HorizontalAlign="Left">
                                    <asp:GridView ID="grdBOM" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("TRN_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No Of Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE14" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("NO_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UOM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE15" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Weight Of Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdW_SIDE20" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("WEIGHT_OF_UNIT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Material Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdARTICLE_CODE" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("MATERIAL_STATUS") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grade">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdUOM" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("GRADE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lot No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdBASIS" runat="server" CssClass="SmallFont Label" 
                                                        Text='<%# Bind("LOT_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date of Manufacturing">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrdVALUE_QTY" runat="server" CssClass="SmallFont LabelNo" 
                                                        Text='<%# Bind("DATE_OF_MFG") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                </asp:Panel>
                                <cc11:hovermenuextender ID="hmeBOM" runat="server" 
                        PopupControlID="pnlBOM" TargetControlID="lnkshow"
                                    PopupPosition="Left"></cc11:hovermenuextender>--%>
                </td>
            </tr>

        </table>



        </td>
    </tr> 
</table>
</br></br></br></br></br></br>
    </ContentTemplate>
</asp:UpdatePanel>
