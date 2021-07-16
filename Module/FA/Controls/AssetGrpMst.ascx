<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AssetGrpMst.ascx.cs" Inherits="Module_FA_Controls_AssetGrpMst" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 225px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
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
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
        <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial tablebox">
            <tr>
                <td class="td" colspan="3">
                    <table cellpadding="0" cellspacing="0" border="1" align="left" class="tContentArial ">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                                    ValidationGroup="M1" ImageUrl="~/CommonImages/save.jpg" OnClick="imgbtnSave_Click"
                                    TabIndex="8" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Width="48" Height="41" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png" OnClick="imgbtnFind_Click"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" Width="48" Height="41" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" OnClick="imgbtnUpdate_Click" ValidationGroup="M1">
                                </asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" Width="48" Height="41" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png" OnClick="imgbtnDelete_Click"></asp:ImageButton>
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center" colspan="3">
                    <b class="titleheading">Assets Group Master</b>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table cellpadding="0" cellspacing="0" border="0" align="left" class="tContentArial">
                        <tr>
                            <td align="left" colspan="5" valign="top">
                                <span class="Mode">You are in
                                    <asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode </span>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center" colspan="5">
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ValidationGroup="M1" />
                                <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                                </strong>
                                <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                                </strong>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *Asset Group Code :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtGroupCode" runat="server" CssClass="TextBox TextBoxNo TextBoxDisplay UpperCase"
                                    ValidationGroup="M1" MaxLength="15" Width="175px" ReadOnly="true" TabIndex="1"></asp:TextBox>
                                <cc1:ComboBox ID="cmbGroupCode" runat="server" Width="180px" Height="200px" AutoPostBack="True"
                                    EnableLoadOnDemand="True" EmptyText="Select Asset Group" DataTextField="ASSET_GRP_CODE"
                                    DataValueField="ASSET_GRP_CODE" TabIndex="2" MenuWidth="550px" OnLoadingItems="cmbGroupCode_LoadingItems"
                                    OnSelectedIndexChanged="cmbGroupCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code
                                        </div>
                                        <div class="header c2">
                                            Group Name</div>
                                        <div class="header c3">
                                            From Date</div>
                                        <div class="header c3">
                                            To Date</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("ASSET_GRP_CODE")%></div>
                                        <div class="item c2">
                                            <%# Eval("ASSET_GRP_NAME")%></div>
                                        <div class="item c3">
                                            <%# Eval("FROM_DT")%></div>
                                        <div class="item c3">
                                            <%# Eval("TO_DT")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc1:ComboBox>
                            </td>
                            <td>
                                *Group Name :
                            </td>
                            <td>
                                <asp:TextBox ID="txtGroupName" runat="server" CssClass="TextBox UpperCase" ValidationGroup="M1"
                                    MaxLength="15" Width="175px" TabIndex="3"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *From Date :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtFromDT" runat="server" ValidationGroup="M1" Width="175px" CssClass="TextBox"
                                    TabIndex="4"></asp:TextBox>
                            </td>
                            <td align="left" valign="top">
                                *To Date :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtToDate" runat="server" ValidationGroup="M1" Width="175px" CssClass="TextBox"
                                    TabIndex="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                *WDV Rate :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtWDVRate" runat="server" CssClass="TextBoxNo TextBox" ValidationGroup="M1"
                                    Width="175px" TabIndex="6" MaxLength="50"></asp:TextBox>%
                                <asp:RangeValidator ID="RVWDVRate" runat="server" ValidationGroup="M1" Display="Dynamic"
                                    ControlToValidate="txtWDVRate" MinimumValue="0" MaximumValue="99.99" ErrorMessage="Rate Value Should not exceeds 100% Or Please Enter Numeric"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                            </td>
                            <td align="left" valign="top">
                                *SLM Rate :
                            </td>
                            <td align="left" valign="top">
                                <asp:TextBox ID="txtSLMRate" runat="server" CssClass="TextBoxNo TextBox" Width="175px"
                                    TabIndex="7"></asp:TextBox>%
                                <asp:RangeValidator ID="RVSLMRate" runat="server" ValidationGroup="M1" Display="Dynamic"
                                    ControlToValidate="txtSLMRate" MinimumValue="0" MaximumValue="99.99" ErrorMessage="Rate Value Should not exceeds 100% Or Please Enter Numeric"
                                    Type="Double" SetFocusOnError="true"></asp:RangeValidator>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" valign="top">
                                Description :
                            </td>
                            <td align="left" valign="top" colspan="5">
                                <asp:TextBox ID="txtDescription" runat="server" Width="552px" CssClass="TextBox"
                                    TabIndex="7" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top" colspan="5">
                                <table>
                                 <tr>
            <td>
              <table width="100%">
                        <asp:GridView ID="grdAssetGroup" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" CssClass="smallfont"
                            EmptyDataText="No Record Found" Font-Size="X-Small" ForeColor="#333333" OnPageIndexChanging="grdAssetGroup_PageIndexChanging"
                            PagerStyle-HorizontalAlign="Left" PageSize="20" OnSelect="grdAssetGroup_Select" Width="100%">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:BoundField DataField="ASSET_GRP_CODE" HeaderText="Code" />
                                <asp:BoundField DataField="ASSET_GRP_NAME" HeaderText="Group Name" />
                                <asp:BoundField DataField="FROM_DT" HeaderText="From Date" />
                                <asp:BoundField DataField="TO_DT" HeaderText="To Date" />
                                <asp:BoundField DataField="WDV_DEPR_RATE" HeaderText="WDV Rate" />
                                <asp:BoundField DataField="SLM_DEPR_RATE" HeaderText="SLM Rate" />
                                 <asp:BoundField DataField="DESCRIPTION" HeaderText="Description" />
                                 </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </table> 
                   
                    </td>
              
            </tr>
                                   <%-- <tr>
                                        <td style="margin-left: 80px">
                                            <cc2:Grid ID="grdAssetGroup" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                                                PageSize="5" AutoGenerateColumns="False" OnSelect="grdAssetGroup_Select">
                                                <Columns>
                                                    <cc2:Column DataField="ASSET_GRP_CODE" Align="Left" HeaderText="Code" Width="70px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="ASSET_GRP_NAME" Align="Left" HeaderText="Group Name" Width="150px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="FROM_DT" Align="Left" HeaderText="From Date" DataFormatString="{0:dd/MM/yyyy}"
                                                        Width="97px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="TO_DT" Align="Left" HeaderText="To Date" DataFormatString="{0:dd/MM/yyyy}"
                                                        Width="90px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="WDV_DEPR_RATE" Align="Left" HeaderText="WDV Rate" Width="93px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="SLM_DEPR_RATE" Align="Left" HeaderText="SLM Rate" Width="90px">
                                                    </cc2:Column>
                                                    <cc2:Column DataField="DESCRIPTION" Align="Left" HeaderText="Description" Width="200px">
                                                    </cc2:Column>
                                                </Columns>
                                            </cc2:Grid>
                                        </td>
                                    </tr>--%>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:RequiredFieldValidator ID="RFGroupName" runat="server" ValidationGroup="M1"
            Display="None" ErrorMessage="Please.. enter Group Name" ControlToValidate="txtGroupName"
            SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFFromDT" runat="server" ValidationGroup="M1" Display="None"
            ErrorMessage="Please.. enter From Date" ControlToValidate="txtFromDT" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFToDT" runat="server" ValidationGroup="M1" Display="None"
            ErrorMessage="Please.. enter To Date" ControlToValidate="txtToDate" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFWDVRate" runat="server" ValidationGroup="M1" Display="None"
            ErrorMessage="Please.. enter WDV Rate" ControlToValidate="txtWDVRate" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RFSLMRate" runat="server" ValidationGroup="M1" Display="None"
            ErrorMessage="Please.. enter SLM Rate" ControlToValidate="txtSLMRate" SetFocusOnError="True"></asp:RequiredFieldValidator>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtFromDT" OnClientDateSelectionChanged="checkDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
            OnClientDateSelectionChanged="checkDate" PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFromDT" PromptCharacter="_">
        </cc4:MaskedEditExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtToDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    <%--</ContentTemplate>
</asp:UpdatePanel>
--%>