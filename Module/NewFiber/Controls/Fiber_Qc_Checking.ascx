<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Qc_Checking.ascx.cs" Inherits="Module_NewFiber_Controls_Fiber_Qc_Checking" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
        width: 80px;
    }
    .c2
    {
        margin-left: 4px;
        width: 100px;
    }
    .c3
    {
        margin-left: 4px;
        width: 100px;
    }
    .d1
    {
        width: 150px;
    }
    .d2
    {
        margin-left: 4px;
        width: 350px;
    }
    .d3
    {
        width: 80px;
    }
    .HeaderRow
    {
        font-size: 8pt;
        font-weight: bold;
    }
    .Smallfont
    {
        font-size: 8pt;
    }
</style>
<table width="95%">
    <tr>
        <td valign="top">
            <table align="left" class="tContentArial" width="100%">
                <tr>
                    <td valign="top" align="left" class="td">
                        <table>
                            <tr>
                                <td id="td1" runat="server" align="left">
                                    <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                                        ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                                </td>
                                <td id="tdSave" runat="server">
                                    <asp:ImageButton ID="imgbtnSave" runat="server" ToolTip="Save" ImageUrl="~/CommonImages/save.jpg"
                                        OnClick="imgbtnSave_Click" TabIndex="10" />
                                </td>
                                <td id="tdUpdate" runat="server">
                                    <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                                        OnClick="imgbtnUpdate_Click" TabIndex="10"></asp:ImageButton>
                                </td>
                                <td id="tdFind" runat="server">
                                    <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                                        OnClick="imgbtnFind_Click" TabIndex="11"></asp:ImageButton>
                                </td>
                                <td id="tdPrint" runat="server">
                                    <asp:ImageButton ID="imgbtnPrint" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/link_print.png"
                                        OnClick="imgbtnPrint_Click" TabIndex="12"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnList" runat="server" ToolTip="Print" ImageUrl="~/CommonImages/list.jpg"
                                        OnClick="imgbtnList_Click" TabIndex="13"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnClear" runat="server" ToolTip="Clear" ImageUrl="~/CommonImages/clear.jpg"
                                        OnClick="imgbtnClear_Click" TabIndex="14"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                        OnClick="imgbtnExit_Click" TabIndex="15"></asp:ImageButton>
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png"
                                        OnClick="imgbtnHelp_Click"></asp:ImageButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="TableHeader td" width="100%">
                        <span class="titleheading"><b>Fiber QC Checking</b></span>
                    </td>
                </tr>
                <tr>
                    <td class="td" align="left" valign="top" width="100%">
                        <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                ShowSummary="False" ValidationGroup="M1" />
                        </span>
                    </td>
                </tr>
                <tr>
                    <td width="90%" class="td" align="center">
                        <fieldset>
                            <legend style="font-size: small">Search Section:</legend>
                            <table width="90%">
                                <tr>
                                    <td width="17%" class="tdRight">
                                        <asp:Label ID="Label15" runat="server" Text="MRN No : " CssClass="LabelNo SmallFont"></asp:Label>
                                    </td>
                                    <td width="20%" class="tdLeft">
                                        <b>
                                            <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                                OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="combined"
                                                EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"
                                                Width="180px" Height="200px" MenuWidth="400px" TabIndex="1" EmptyText="Select MRN">
                                                <HeaderTemplate>
                                                    <div class="header c1">
                                                        MRN #</div>
                                                    <div class="header c1">s
                                                        Year</div>
                                                    <div class="header c1">s
                                                        Prty Name</div>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                                    </div>
                                                    <div class="item c1">
                                                        <asp:Literal runat="server" ID="Container5" Text='<%# Eval("YEAR") %>' />
                                                    </div>
                                                     <div class="item c2">
                                                        <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PRTY_NAME") %>' />
                                                    </div>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    Displaying items
                                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                    <%# Container.ItemsCount %>.
                                                </FooterTemplate>
                                            </cc2:ComboBox>
                                            <asp:HiddenField ID="hdTRN_TYPE" runat="server" />
                                            <asp:HiddenField ID="hdTRN_NUMB" runat="server" />
                                            <asp:HiddenField ID="hdYEAR" runat="server" />
                                        </b>
                                    </td>
                                    <td class="tdRight" width="17%">
                                        <asp:Label ID="Label18" runat="server" CssClass="LabelNo SmallFont" Text=" Fiber Code:"></asp:Label>
                                    </td>
                                    <td class="tdLeft" width="10%">
                                        <cc2:ComboBox ID="ddlyarncode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                            DataTextField="FIBER_CODE" DataValueField="FIBER_DESC" EmptyText="Find Yarn Code"
                                            EnableLoadOnDemand="true" EnableVirtualScrolling="true" Height="200px" MenuWidth="500"
                                            OnLoadingItems="ddlyarncode_LoadingItems" OnSelectedIndexChanged="ddlyarncode_SelectedIndexChanged"
                                            TabIndex="2" Width="180px">
                                            <HeaderTemplate>
                                                <div class="header c1">
                                                    Code</div>
                                                <div class="header c2">
                                                    Description</div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <div class="item c1">
                                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("FIBER_CODE") %>' /></div>
                                                <div class="item c2">
                                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("FIBER_DESC") %>' /></div>
                                               
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                Displaying items
                                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                                <%# Container.ItemsCount %>.
                                            </FooterTemplate>
                                        </cc2:ComboBox>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td width="100%" class="td">
                        <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                            <asp:GridView ID="gvMaterialReceiptApproval" Width="99%" runat="server" AutoGenerateColumns="False"
                                CssClass="SmallFont" ShowFooter="false" OnRowDataBound="gvMaterialReceiptApproval_RowDataBound">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr&nbsp;No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRN&nbsp;Year" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTRN_Year" runat="server" Text='<%#Eval("TRN_YEAR") %>'></asp:Label>
                                            <asp:Label ID="lblTRN_TYPE" runat="server" Text='<%#Eval("TRN_TYPE") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblQC_Year" runat="server" Text='<%#Eval("YEAR") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblQC_NUMB" runat="server" Text='<%#Eval("QC_NUMB") %>' Visible="false"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="MRN&nbsp;No" HeaderStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTRN_NUMB" runat="server" Text='<%#Eval("TRN_NUMB") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="TRN_DATE" HeaderText="MRN&nbsp;Date" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}">
                                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PARTY_DATA" HeaderText="Party" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemStyle Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Fiber&nbsp;Code" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblFiber_Code" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FIBER_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fiber&nbsp;Desc" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblYarnDesc" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FIBER_DESC") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fiber&nbsp;Count" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <%--<asp:Label ID="lblYarnCount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Y_COUNT") %>'></asp:Label>--%>
                                            <asp:Label ID="lblYarnCount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("INWARD_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Std&nbsp;Type" HeaderStyle-HorizontalAlign="Left"
                                        ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSTD_TYPE" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("STD_TYPE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Max&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMaxValue" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MAX_VALUE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Min&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:Label ID="lblMinValue" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("MIN_VALUE") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="QC&nbsp;Value" HeaderStyle-HorizontalAlign="Right"
                                        ItemStyle-HorizontalAlign="Right">
                                        <ItemTemplate>
                                            <asp:UpdatePanel ID="updatepanel11" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="lblQC_VALUE" runat="server" CssClass="TextBoxNo SmallFont" Width="90%"
                                                        AutoPostBack="true" Text='<%# Bind("QC_VALUE") %>' OnTextChanged="lblQC_VALUE_TextChanged"
                                                        TabIndex="2"></asp:TextBox>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="lblQC_VALUE"
                                                ValidationExpression="(^-?0\.[0-9]*[1-9]+[0-9]*$)|(^-?[1-9]+[0-9]*((\.[0-9]*[1-9]+[0-9]*$)|(\.[0-9]+)))|(^-?[1-9]+[0-9]*$)|(^0$){1}"
                                                ErrorMessage="Invalid No" >
                                            </asp:RegularExpressionValidator>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" />
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblREMARKS" runat="server" Width="97%" MaxLength="200" CssClass="TextBox SmallFont"
                                                Text='<%# Bind("QC_REMARKS") %>' TabIndex="3"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="20%" />
                                        <HeaderStyle Width="20%" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Result" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Image ID="imgSuccess" runat="server" ImageUrl="~/CommonImages/green.png" Height="20px"
                                                Width="20px" AlternateText="Pass" ToolTip="Pass" />
                                            <asp:Image ID="imgFail" runat="server" ImageUrl="~/CommonImages/red.png" Height="22px"
                                                Width="22px" AlternateText="Fail" ToolTip="Fail" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Add&nbsp;New">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkApproved" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Edit">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkEdit" runat="server" OnCheckedChanged="chkEdit_CheckedChanged"
                                                AutoPostBack="true" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Wrap="true" CssClass="label smallfont" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="SmallFont" />
                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                            </asp:GridView>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
