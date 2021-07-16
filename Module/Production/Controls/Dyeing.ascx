<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Dyeing.ascx.cs" Inherits="Module_Production_Controls_Dyeing" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        margin-left: 4px;
    }

    .c1 {
        width: 120px;
    }

    .c2 {
        margin-left: 10px;
        width: 140px;
    }

    .c3 {
        margin-left: 4px;
        width: 120px;
    }

    .c4 {
        width: 190px;
    }

    .c5 {
        margin-left: 4px;
        width: 320px;
    }

    .c6 {
        margin-left: 4px;
        width: 150px;
    }

    .c7 {
        margin-left: 10px;
        width: 80px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table width="100%" align="left" class="tContentArial">
            <tr>
                <td align="left" valign="top" class="td" width="100%">
                    <table class="tContentArial">
                        <tr>

                             <td id="tdEdit" runat="server">
                                <asp:ImageButton ID="imgbtnExport" OnClick="imgbtnExport_Click" Visible="true" runat="server" ToolTip="Save"
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="YM"></asp:ImageButton>
                            </td>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" Visible="false" runat="server" ToolTip="Save"
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="YM"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" Visible="false" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" Visible="false" ToolTip="Find"
                                    ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                                    ImageUrl="~/CommonImages/clear.jpg"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                                    ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                                    ImageUrl="~/CommonImages/link_exit.png"></asp:ImageButton>
                            </td>
                            <td>
                                <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" Visible="false" runat="server" ToolTip="Help"
                                    ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center" class="TableHeader td" width="100%">
                    <b class="titleheading">
                        <asp:Label ID="lblFormHeading" runat="server" CssClass="SmallFont">Production Dyeing Entry Form</asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" width="100%" class="td">
                    <span class="Mode">You are in&nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                    </span>
                </td>
            </tr>

            <tr>
                <td class="td" align="left" width="100%">
                    <table align="left" width="100%">
                        <tr>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblBatchCode" runat="server" CssClass="SmallFont" Text="Prod Trn No"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtProductionNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" onkeyup="javascript:this.value = this.value.toUpperCase();"
                                    runat="server"></asp:TextBox>

                                <cc2:ComboBox ID="ddlProductionNo" runat="server" TabIndex="1" Width="100%" MenuWidth="800"
                                    AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                    Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlProductionNo_LoadingItems"
                                    OnSelectedIndexChanged="ddlProductionNo_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Batch Code
                                        </div>
                                        <div class="header c1">
                                            Batch Date
                                        </div>
                                        <div class="header c1">
                                            Pa No
                                        </div>
                                        <div class="header c2">
                                            Lot No
                                        </div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("BATCH_CODE")%>
                                        </div>
                                        <div class="item c1">
                                            <%# Eval("BATCH_DATE")%>
                                        </div>
                                        <div class="item c2">
                                            <%# Eval("PA_NO")%>
                                        </div>
                                        <div class="item c1">
                                            <%# Eval("LOT_NUMBER")%>
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblBatchDate" runat="server" Text="Pr Date"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtBatchDate" CssClass="TextBox SmallFont" Width="100%" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender ID="ceBatchDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtBatchDate">
                                </cc1:CalendarExtender>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="Label1" runat="server" Text="To Location"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:DropDownList ID="ddlToLocation" CssClass="TextBox  SmallFont" Width="100%"
                                    ReadOnly="true" runat="server">
                                    <asp:ListItem>GODOWN</asp:ListItem>
                                    <asp:ListItem>MCHINE ROOM</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                            <td class="tdRight" width="7%">
                                <asp:Label ID="Label2" runat="server" Text="From Location"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:DropDownList ID="ddlFromLocation" CssClass="TextBox  SmallFont" Width="100%"
                                    ReadOnly="true" runat="server">
                                    <asp:ListItem>MCHINE ROOM</asp:ListItem>
                                    <asp:ListItem>GODOWN</asp:ListItem>
                                </asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblPaNo" runat="server" Text="Select PA No"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <cc2:ComboBox ID="ddlPaNo" runat="server" TabIndex="1" Width="100%" MenuWidth="800"
                                    AutoPostBack="true" CssClass="SmallFont" EnableLoadOnDemand="true" OpenOnFocus="true"
                                    Height="200px" EnableVirtualScrolling="true" OnLoadingItems="ddlPaNo_LoadingItems"
                                    OnSelectedIndexChanged="ddlPaNo_SelectedIndexChanged" EmptyText="Select PA No">
                                    <HeaderTemplate>
                                        <div class="header c4">
                                            Party Name
                                        </div>
                                        <div class="header c7">
                                            Shade Code
                                        </div>
                                        <div class="header c1">
                                            Cust Req NO
                                        </div>
                                        <div class="header c1">
                                            Pa No
                                        </div>
                                        <div class="header c7">
                                            Job Card No
                                        </div>
                                        <div class="header c7">
                                            Lot Qty
                                        </div>

                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c4">
                                            <%# Eval("PRTY_NAME")%>
                                        </div>
                                        <div class="item c7">
                                            <%# Eval("SHADE_CODE")%>
                                        </div>
                                        <div class="item c1">
                                            <%# Eval("CUST_REQ_NO")%>
                                        </div>
                                        <div class="item c1">
                                            <%# Eval("PA_NO")%>
                                        </div>
                                        <div class="item c7">
                                            <%# Eval("BATCH_CODE")%>
                                        </div>
                                        <div class="item c7">
                                            <%# Eval("TRN_QTY")%>
                                        </div>


                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items<%# Container.ItemsCount> 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblPaNo1" runat="server" Text="PA No"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtPaNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="Label3" runat="server" Text="To Process"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:DropDownList ID="ddlToProcess" CssClass="TextBoxNo  SmallFont" Width="100%"
                                    ReadOnly="true" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="Label4" runat="server" Text="From Process"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:DropDownList ID="ddlFromProcess" CssClass="TextBox  SmallFont" Width="100%"
                                    ReadOnly="true" runat="server">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblJobCardNo" runat="server" Text="Job Card No"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtJobCardNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>

                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblCustReqNo" runat="server" Text="Cust Rq. No"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtCustReqNo" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblOrderQty" runat="server" Text="Batch Qty"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtOrderQty" CssClass="TextBoxNo TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblShade" runat="server" Text="Shade"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtShade" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblParty" runat="server" Text="Party"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtParty" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" colspan="2">
                                <asp:TextBox ID="txtPartDtl" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%">
                                <asp:Label ID="lblArticle" runat="server" Text="Article"></asp:Label>
                            </td>
                            <td class="tdLeft" width="7%">
                                <asp:TextBox ID="txtArticle" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="7%" colspan="2" style="width: 34%">
                                <asp:TextBox ID="txtArticleDesc" CssClass="TextBox TextBoxDisplay SmallFont" Width="100%"
                                    ReadOnly="true" runat="server"></asp:TextBox>
                            </td>
                        </tr>

                    </table>
                    <tr id="T1" runat="server">
                        <td align="left" class="td" valign="top" width="100%">
                            <table width="100%">
                                <tr bgcolor="#336699" class="SmallFont titleheading">
                                    <td class="HeaderRow">Posting/Date
                                    </td>
                                    <td class="HeaderRow">Job/ No
                                    </td>
                                    <td class="HeaderRow">Grey Lot
                                    </td>
                                    <td class="HeaderRow">Batch&nbsp;No
                                    </td>
                                    <td class="HeaderRow">Shade No
                                    </td>
                                    <td class="HeaderRow">Lot Qty&nbsp;
                                    </td>
                                    <td class="HeaderRow">Width&nbsp;
                                    </td>
                                    <td class="HeaderRow" id="tdheadUom" runat="server">Gross WTT
                                    </td>
                                    <td class="HeaderRow">Location
                                    </td>
                                    <td class="HeaderRow">GLM
                                    </td>
                                    <td class="HeaderRow">Remarks
                                    </td>
                                    <td class="HeaderRow"></td>
                                </tr>
                            </table>
                        </td>
                    </tr>

                    <tr>
                        <td align="left" class="td" width="100%">
                            <asp:GridView ID="gvProductionEntry" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                OnRowDataBound="gvProductionEntry_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Posting &nbsp;Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPostDate" DataFormatString="{0:MM-dd-yyyy hh:mm tt}" runat="server" Text='<%# Bind("BATCH_DATE") %>'></asp:Label>
                                            <%--<asp:Label ID="lblPostDate"  runat="server" Text='<%# Eval("TRN_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="100px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Job Card&nbsp;No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBatchNo" runat="server" ToolTip='<%# Bind("BATCH_CODE") %>' Text='<%# Bind("BATCH_CODE") %>'></asp:Label>
                                            <asp:Label ID="lblItemCode" runat="server" ToolTip='<%# Bind("ARTICLE_CODE") %>' Text='<%# Bind("ARTICLE_CODE") %>'
                                                Visible="false"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle CssClass="labelNo smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                            Width="90px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grey Lot &nbsp;">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGreyLotNo" runat="server" Text='<%# Bind("GREY_LOT_NO") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Shade No &nbsp;">
                                        <ItemTemplate>
                                            <asp:Label ID="lblShade" runat="server" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Lot&nbsp;Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTrnQty" runat="server" Visible="false" Width="60px" Text='<%# Bind("LOT_SIZE") %>'></asp:TextBox>
                                            <asp:Label ID="lblTrnQty" runat="server" Text='<%# Bind("LOT_SIZE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUOM" runat="server" Text='<%# Bind("UOM_OF_UNIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Machine&nbsp;">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMAchineCode" runat="server" Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Process &nbsp;">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProcess" runat="server" Text='<%# Bind("PROCESS") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Batch No &nbsp;">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBatchIssueNo" runat="server" Width="70px" CssClass="TextBox SmallFont"></asp:TextBox>
                                            <asp:RegularExpressionValidator Display="Dynamic" ControlToValidate="txtBatchIssueNo" ID="RegularExpressionValidator2"
                                                ValidationExpression="^[\s\S]{10,15}$" runat="server" ErrorMessage="Minimum 10 and Maximum 15 characters required.">

                                            </asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Cortoon No" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCortoonNo" runat="server" Width="70px" Text="0" CssClass="TextBox SmallFont"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtCortoonNo"
                                                Display="Dynamic" ErrorMessage="Please Enter No of Cops in Numeric &amp; Precision Should be 9 and Scale 2   "
                                                MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTexttxtCortoonNo" runat="server" TargetControlID="txtCortoonNo" FilterType="Custom, Numbers" ValidChars="." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Cops">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCops" runat="server" Width="70px" Text='<%# Bind("SPRINGS") %>' CssClass="TextBox SmallFont"></asp:TextBox>
                                            <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtCops"
                                                Display="Dynamic" ErrorMessage="Please Enter No of Cops in Numeric &amp; Precision Should be 9 and Scale 2   "
                                                MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTexttxttxtCops" runat="server" TargetControlID="txtCops" FilterType="Custom, Numbers" ValidChars="." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Net WT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Width="70px" AutoPostBack="True"
                                                OnTextChanged="txtQty_TextChanged" MaxLength="10" TabIndex="6"
                                                CssClass="TextBox  SmallFont" Text='<%# Bind("LOT_SIZE") %>'></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtQty"
                                                Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtQty"
                                                Display="Dynamic" ErrorMessage="Please Enter QTY in Numeric &amp; Precision Should be 9 and Scale 2   "
                                                MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FiltertxtQty" runat="server" TargetControlID="txtQty" FilterType="Custom, Numbers" ValidChars="." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText=" Rej Cops">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRejCops" runat="server" Width="70px" CssClass="TextBox  SmallFont"
                                                MaxLength="10" Text="0"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtQty"
                                                Display="Dynamic" ErrorMessage="Please Enter Tare Wt" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtRejCops"
                                                Display="Dynamic" ErrorMessage="Please Enter Tare.Wt. in Numeric &amp; Precision Should be 9 and Scale 2   "
                                                MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtRejCops" FilterType="Custom, Numbers" ValidChars="." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText=" Rej Net WT">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRejNetWt" Width="70px" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true" runat="server" MaxLength="10" Text="0"></asp:TextBox>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtQty"
                                                Display="Dynamic" ErrorMessage="Please Enter Gross WT" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtRejNetWt"
                                                Display="Dynamic" ErrorMessage="Please Enter Gross Wt in Numeric &amp; Precision Should be 9 and Scale 2"
                                                MaximumValue="999999999.9999" MinimumValue="0" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtRejNetWt" FilterType="Custom, Numbers" ValidChars="." />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Trolly No">
                                        <ItemTemplate>

                                            <asp:DropDownList ID="ddlRack_No" runat="server" CssClass="SmallFont " Width="100px">
                                            </asp:DropDownList>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirm">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkApproved" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" CssClass="label smallfont" VerticalAlign="Top"
                                            Width="50px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Confirm Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtConfirmDate" runat="server" Text='<%# Bind("CONF_DATE") %>' Width="55px"
                                                CssClass="TextBox SmallFont TextBoxDisplay" ReadOnly="true"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Confirm By">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtConfirmBy" runat="server" Text='<%# Bind("CONF_BY") %>' Width="70px"
                                                CssClass="TextBox SmallFont TextBoxDisplay" ReadOnly="true"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText=" Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemarks" runat="server" Width="100px" CssClass="TextBox SmallFont"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" />
                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                            </asp:GridView>
                        </td>
                    </tr>
                </td>
            </tr>

        </table>
    </ContentTemplate>
</asp:UpdatePanel>
