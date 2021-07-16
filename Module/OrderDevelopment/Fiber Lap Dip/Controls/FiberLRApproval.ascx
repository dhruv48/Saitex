<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberLRApproval.ascx.cs" Inherits="Module_OrderDevelopment_LabDip_Controls_FiberLRApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>
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
            .c1
            {
                width: 100px;
            }
            .c2
            {
                
                width: 120px;
            }
            .c3
            {
                
                width: 80px;
            }
            .c4
            {
                margin-left: 4px;
                width: 150px;
            }
            .c5
            {
                margin-left: 4px;
                width: 100px;
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
                                <asp:ImageButton ID="imgbtnNew" runat="server" Height="41" ImageUrl="~/CommonImages/save.jpg"
                                    OnClick="imgbtnNew_Click" ToolTip="Save" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                    OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48" />
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                                    OnClick="imgbtnDelete_Click" ToolTip="Delete" Width="48" />
                            </td>
                            <td id="tdFind" runat="server">
                                <asp:ImageButton ID="imgbtnFind" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                    OnClick="imgbtnFind_Click" ToolTip="Find" Width="48" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Fiber LR Approval</span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft td">
                    <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                        ShowSummary="false" ValidationGroup="M1" />
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label><strong>
                    </strong>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label><strong>
                    </strong>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td class="tdRight">
                                Customer Req. No. :
                            </td>
                            <td class="tdLeft">
                                <cc2:ComboBox ID="ddlOrderNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    EnableLoadOnDemand="True" DataTextField="ORDER_NO" DataValueField="ORDER_NO"
                                    Width="130px" MenuWidth="450" OnLoadingItems="ddlOrderNo_LoadingItems" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11" Visible="true"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            BRANCH</div>
                                        <div class="header c2">
                                            CR.NO</div>
                                        <div class="header c3">
                                            P. TYPE</div>
                                        <div class="header c3">
                                            B. TYPE
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("BRANCH_NAME")%></div>
                                        <div class="item c2">
                                            <%# Eval("ORDER_NO")%></div>
                                        <div class="item c3">
                                            <%# Eval("CR_PRODUCT_TYPE")%></div>
                                        <div class="item c3">
                                            <%# Eval("CR_BUSINESS_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <%--  <asp:DropDownList ID="ddlOrderNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Width="130px" DataTextField="ORDER_NO" DataValueField="ORDER_NO" TabIndex="1"
                                    CssClass="SmallFont" OnSelectedIndexChanged="ddlOrderNo_SelectedIndexChanged">
                                </asp:DropDownList>--%>
                            </td>
                            <td class="tdRight">
                                Customer Req. Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="TextBox TextBoxDisplay" Width="130px"
                                    ReadOnly="true" TabIndex="2"></asp:TextBox>
                                      </td>
                            <td class="tdLeft">
                            Artical No
                             </td>
                            <td class="tdLeft">
                            <asp:TextBox ID="txtOrderRefNo" runat="server" CssClass="TextBox TextBoxDisplay" Width="130px"
                                    ReadOnly="true" TabIndex="2"></asp:TextBox>                                    
                            </td>
                            <td class="tdRight">
                                Branch:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtBranch" runat="server" CssClass="TextBox TextBoxDisplay" Width="130px"
                                    TabIndex="3" ReadOnly="True"></asp:TextBox>
                                <asp:TextBox ID="txtBranchCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="1px" TabIndex="4" ReadOnly="True" Visible="false"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Approval Date :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="TextBox" Width="75px"
                                    TabIndex="5"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                Lab Dip No.:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlLRNo" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                                    Width="130px" DataTextField="LAB_DIP_NO" DataValueField="LAB_DIP_NO" TabIndex="1"
                                    CssClass="SmallFont" OnSelectedIndexChanged="ddlLRNo_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                Base Lot No:
                            </td>
                            <td class="tdLeft">
                                 <asp:TextBox ID="txtGreyLotNo" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="130px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Approved By :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtApprovedBy" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="130px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Remarks :
                            </td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox" Width="325px" TabIndex="4"></asp:TextBox>
                            </td>
                        </tr>
                         <tr>
                             <td class="tdRight">
                                Shade Category :
                            </td>
                            <td class="tdLeft" >
                                 <asp:DropDownList ID="ddlShadeCat" runat="server" AppendDataBoundItems="True"
                                         CssClass="SmallFont "   Width="130px">
                                         <asp:ListItem Value="Normal">Normal</asp:ListItem>
                                           <asp:ListItem Value="Sensitive">Sensitive</asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                             <td class="tdRight">
                                Nature Shade :
                            </td>
                            <td class="tdLeft" >
                                <asp:DropDownList ID="ddlNatureShade" runat="server" AppendDataBoundItems="True"
                                         CssClass="SmallFont "   Width="130px">
                                         <asp:ListItem Value="Light">Light</asp:ListItem>
                                         <asp:ListItem Value="Medium" Selected="True">Medium</asp:ListItem>
                                           <asp:ListItem Value="Dark">Dark</asp:ListItem>
                                        </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                               Quality Code:
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtArticalCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="130px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight">
                                Quality Desc:
                            </td>
                            <td class="tdLeft" colspan="3">
                                <asp:TextBox ID="txtArticalDesc" runat="server" CssClass="TextBox TextBoxDisplay" Width="325px" TabIndex="4"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="tdRight">
                                F. Shade Family:
                            </td>
                            <td class="tdLeft">
                                <asp:DropDownList ID="ddlShadeFamily" runat="server" Width="130px" TabIndex="13"
                                                AutoPostBack="True" CssClass="SmallFont" 
                                                DataValueField="SHADE_FAMILY_CODE" DataTextField="SHADE_FAMILY_CODE"
                                                OnSelectedIndexChanged="ddlShadeFamily_SelectedIndexChanged" 
                                              >
                                            </asp:DropDownList>
                            </td>
                            <td class="tdRight">
                                F. Shade No :
                            </td>
                            <td class="tdLeft">
                               <asp:TextBox ID="txtShadeCodeAuto" runat="server" Width="99%"
                                        ReadOnly="false" onkeyup="javascript:this.value = this.value.toUpperCase();"
                            CssClass="TextBox SmallFont " ></asp:TextBox>
                            </td>
                           <td class="tdRight">
                                Shade Code :
                            </td>
                            <td class="tdLeft">
                                <asp:TextBox ID="txtShadeCode" runat="server" CssClass="TextBox TextBoxDisplay"
                                    Width="130px" TabIndex="4" ReadOnly="True"></asp:TextBox>
                            </td>
                        </tr>
                        
                        <tr>
                            <td class="tdRight">
                                <i>Choose Option :</i>
                            </td>
                            <td colspan="7">
                                <asp:Panel ID="pnlOptions" runat="server" ScrollBars="Auto" Width="800px" Height="100px">
                                    <asp:RadioButtonList ID="rdoLstOption" runat="server" RepeatColumns="1" RepeatDirection="Vertical"
                                        CssClass="TextBox" AutoPostBack="true" OnSelectedIndexChanged="rdoLstOption_SelectedIndexChanged"
                                        Font-Bold="true" ForeColor="#cc0099" Font-Italic="true">
                                    </asp:RadioButtonList>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight">
                                <%--<i>Dye Details :</i>--%>
                            </td>
                            <td colspan="7">
                                <asp:Panel ID="Panel1" runat="server" ScrollBars="Auto" Width="800px" Height="130px">
                                    <asp:GridView ID="grdDyeName" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BorderWidth="1px" CssClass="SmallFont" Font-Bold="False" ShowFooter="false" Width="98%"
                                        TabIndex="9">
                                        <Columns>
                                            <asp:TemplateField HeaderText="LR Number">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblLRNo" runat="server" Text='<%# Bind("LAB_DIP_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Option">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOption" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("LR_OPTION") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dye Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDyeName" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DYE_NAME") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dye Detail">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDyeDTL" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DYE_DTL") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("RATE") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Dose %">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDose" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("DOSE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="txtTotalAmt" runat="server" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cost">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCost" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("RECIPE_COST") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCostFooter" runat="server" Width="100px"></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="RowStyle SmallFont" />
                                        <SelectedRowStyle CssClass="SelectedRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" />
                                        <PagerStyle CssClass="PagerStyle" />
                                        <HeaderStyle BackColor="#336699" CssClass="HeaderStyle SmallFont" ForeColor="White" />
                                    </asp:GridView>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <cc4:CalendarExtender ID="ce1" runat="server" TargetControlID="txtApprovalDate" PopupPosition="TopRight"
            OnClientDateSelectionChanged="checkDate" Format="dd/MM/yyyy">
        </cc4:CalendarExtender>
        <cc4:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtApprovalDate" PromptCharacter="_">
        </cc4:MaskedEditExtender>
    </ContentTemplate>
</asp:UpdatePanel>
