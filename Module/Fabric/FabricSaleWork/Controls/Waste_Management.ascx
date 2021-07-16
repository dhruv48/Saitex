<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Waste_Management.ascx.cs" Inherits="Module_Fabric_FabricSaleWork_Controls_Waste_Management" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Src="~/CommonControls/LOV/PartyCodeLOV.ascx" TagName="PartyCodeLOV"
    TagPrefix="uc1" %>
<%@ Register Src="../../../../CommonControls/LOV/ApproveLRLOV.ascx" TagName="ApproveLRLOV"
    TagPrefix="uc2" %>
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
        width: 100px;
    }
    .c2
    {
        margin-left: 1px;
        width: 100px;
    }
    .c3
    {   margin-left: 1px;
        width: 100px;
    }
    .c4
    {
    	 margin-left: 1px;
        width: 300px;
    }
    .c5
    {  
    	 width: 100px;
    }
    .d1
    {
        width: 180px;
    }
    .d2
    {
        margin-left: 4px;
        width: 180px;
    }
    .d3
    {
        margin-left: 4px;
        width: 180px;
    }
    .d4
    {
        margin-left: 4px;
        width: 180px;
    }
</style>
<style type="text/css">
    .AutoExtender
    {
        font-family: Verdana, Helvetica, sans-serif;
        font-size: .8em;
        font-weight: normal;
        border: solid 1px #006699;
        line-height: 20px;
        padding: 10px;
        background-color: White;
        margin-left: 10px;
    }
    .AutoExtenderList
    {
        border-bottom: dotted 1px #006699;
        cursor: pointer;
        color: Maroon;
    }
    .AutoExtenderHighlight
    {
        color: White;
        background-color: #006699;
        cursor: pointer;
    }
    #divwidth
    {
        width: 200px !important;
    }
    #divwidth div
    {
        width: 200px !important;
    }
    .SmallFont
    {
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" class="td" valign="top" width="100%">
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            OnClick="imgbtnSave_Click1" ToolTip="Save" ValidationGroup="CR" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            OnClick="imgbtnUpdate_Click1" ToolTip="Update" ValidationGroup="CR" />
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Height="41" ImageUrl="~/CommonImages/del6.png"
                            OnClick="imgbtnDelete_Click1" OnClientClick="javascript:return window.confirm('Are you sure you want to delete this record')"
                            ToolTip="Delete" ValidationGroup="M1" Width="48" />
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            OnClick="imgbtnFind_Click1" ToolTip="Find" />
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            OnClick="imgbtnPrint_Click" ToolTip="Print" />
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            OnClick="imgbtnClear_Click1" ToolTip="Clear" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            OnClick="imgbtnExit_Click1" ToolTip="Exit" />
                    </td>
                    <td style="font-style: italic">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <span class="titleheading"><b>WASTE MANAGEMENT</b></span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" valign="top" width="100%">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
                <asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="ss" />
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" ValidationGroup="CR" />
            </span>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <table width="100%" class="tdLeft">
                <tr>
                    <td align="right" width="17%">
                        WM Location:
                    </td>
                    <td align="left" width="17%">
                        <asp:TextBox ID="txtCrLocation" runat="server" ReadOnly="true" TabIndex="1" Width="120px"
                            CssClass="SmallFont TextBox UpperCase TextBoxDisplay" ValidationGroup="M1"></asp:TextBox>
                    </td>
                    <td align="right" width="17%">
                        Product Type:
                    </td>
                    <td align="left" width="17%">
                        <asp:DropDownList Width="120px" TabIndex="2" CssClass="SmallFont TextBox UpperCase"
                            ID="ddlOrderType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlOrderType_SelectedIndexChanged"
                            AppendDataBoundItems="True">
                        </asp:DropDownList>
                        <br />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="DDLOrderType"
                            Display="None" ErrorMessage="Please Select Order Type " InitialValue="0" ValidationGroup="CR"></asp:RequiredFieldValidator>
                    </td>
                    <td align="right" width="17%">
                        WM No :
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox ID="txtOrderNo" TabIndex="3" runat="server" Width="120px" CssClass="TextBox TextBoxDisplay"
                            ValidationGroup="M1" ReadOnly="True"></asp:TextBox>
                        <cc2:ComboBox ID="cmbOrderNo" TabIndex="4" runat="server" AutoPostBack="True" CssClass="smallfont"
                            DataTextField="WM_NO" DataValueField="Combined" EnableLoadOnDemand="True"
                            MenuWidth="800" OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged"
                            EnableVirtualScrolling="true" OpenOnFocus="true" Width="120px" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    WM No</div>
                                <div class="header c1">
                                    WM Location</div>
                                <div class="header c1">
                                    Product Type</div>
                                <div class="header c1">
                                    WM date</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <%# Eval("WM_NO")%></div>
                                <div class="item c1">
                                    <%# Eval("WM_LOCATION")%></div>
                                <div class="item c1">
                                    <%# Eval("PRODUCT_TYPE")%></div>
                                <div class="item c1">
                                    <%# Eval("WM_DATE")%></div>
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
                    <td align="right" width="17%">
                        WM Date :
                    </td>
                    <td align="left" width="17%">
                        <asp:TextBox ID="txtDate" runat="server" TabIndex="5" Width="120px" MaxLength="10"
                            CssClass="SmallFont TextBoxDisplay UpperCase" ValidationGroup="M1"></asp:TextBox>
                    </td>
                   
                   
               
                </tr>
                <tr>
                    <td align="right" class="" width="17%">
                        Customer Name :
                    </td>
                    <td align="left" width="17%">
                        <cc2:ComboBox ID="cmbPartyCode" runat="server" AutoPostBack="True" DataTextField="PRTY_CODE"
                            DataValueField="Address" EmptyText="Select Party" EnableLoadOnDemand="true" Height="200px"
                            MenuWidth="500px" OnLoadingItems="cmbPartyCode_LoadingItems" OnSelectedIndexChanged="cmbPartyCode_SelectedIndexChanged"
                            TabIndex="8" Width="100px" EnableVirtualScrolling="true">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    NAME</div>
                                <div class="header d2">
                                    Address</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("PRTY_CODE") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item d2">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("Address") %>' />
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
                    <td align="left" colspan="4" class="tdLeft" width="66%">
                        <asp:TextBox ID="txtPartyCode" TabIndex="7" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                            ReadOnly="true" ValidationGroup="M1" Width="24%"></asp:TextBox>
                        <asp:TextBox ID="txtAddress" runat="server" CssClass="SmallFont TextBox TextBoxDisplay"
                            ReadOnly="true" TabIndex="8" ValidationGroup="M1" Width="74%"></asp:TextBox>
                    </td>
                </tr>
               
                    
                <tr>
                    <td align="right" width="17%">
                        &nbsp;Remarks:
                    </td>
                    <td align="left" width="83%" colspan="5">
                        &nbsp;<asp:TextBox ID="txtMstRemarks" runat="server" TabIndex="14" CssClass="SmallFont TextBox UpperCase"
                            ValidationGroup="M1" Width="99.50%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%">
            <table width="100%">
                <tr>
                    <td class="td" width="100%">
                        <table width="100%">
                            <tr bgcolor="#006699">
                                <td width="10%">
                                    <span class="titleheading"><b> Item Code </b></span>
                                </td>
                                <td width="18%">
                                    <span class="titleheading"><b>item Desc</b></span>
                                </td>
                                <td width="10%">
                                    <span class="titleheading"><b>Shade Code</b></span>
                                </td>
                                <td  width="8%">
                                    <span class="titleheading"><b>Issue Qty</b></span>
                                </td>
                                <td width="10%">
                                    <span class="titleheading"><b>waste qty</b></span>
                                </td>
                               <%--  <td width="10%" align="center">
                                   <span class="titleheading"><b>Tax/Disc.</b></span>
                                </td>
                               <td width="10%" align="center">
                                 <span class="titleheading"><b>Final&nbsp;Rate</b></span>
                                        </td>--%>
                                <td class="tdRight" width="8%">
                                    <span class="titleheading"><b>Total Cost</b></span>
                                </td>
                               <%-- <td class="tdRight" width="10%">
                                    <span class="titleheading"><b>End Use</b></span>
                                </td>--%>
                                <td  width="8%">
                                
                                    <span class="titleheading"><b>Req Date</b></span>
                                </td>
                                <td  width="15%">
                                    <span class="titleheading"><b>Remarks</b></span>
                                </td>
                                <td width="18%">
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="top" width="26%">
                                    <cc2:ComboBox ID="cmbArticleNo" runat="server" AutoPostBack="True" CssClass="smallfont"
                                        DataTextField="ITEM_CODE" DataValueField="Combined" EnableLoadOnDemand="True"
                                        MenuWidth="800" OnLoadingItems="cmbArticleNo_LoadingItems" OnSelectedIndexChanged="cmbArticleNo_SelectedIndexChanged"
                                        EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="15" Visible="true"
                                        Width="25%" Height="200px">
                                        <HeaderTemplate>
                                            <div class="header c1">
                                                TRN_NUMB</div>
                                            <div class="header c2">
                                                PI_NO</div>
                                            <div class="header c3">
                                                ITEM_CODE</div>
                                            <div class="header c4">
                                                ITEM_DESC</div>
                                            <div class="header c5">
                                                ISS_QTY</div>
                                           <div class="header c3">
                                                SHADE_CODE</div>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <div class="item c1">
                                                <%# Eval("TRN_NUMB")%></div>
                                            <div class="item c2">
                                                <%# Eval("PI_NO")%></div>
                                            <div class="item c3">
                                                <%# Eval("ITEM_CODE")%></div>
                                           <div class="item c4">
                                                <%# Eval("ITEM_DESC")%></div>
                                          <div class="item c5">
                                                <%# Eval("TRN_QTY")%></div>
                                          <div class="item c1">
                                                <%# Eval("SHADE_CODE")%></div>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            Displaying items
                                            <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                            out of
                                            <%# Container.ItemsCount %>.
                                        </FooterTemplate>
                                    </cc2:ComboBox>
                                    <asp:TextBox ID="txtItemCodeLabel" runat="server" Width="50%" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td valign =top>
                                <asp:TextBox ID="TxtItemDesc" runat="server" Width="90%" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" width="8%">
                                    <asp:TextBox ID="TxtShadeCode" runat="server" Width="90%" CssClass="TextBox TextBoxDisplay SmallFont"
                                        Font-Bold="False" ReadOnly="True"></asp:TextBox>
                                </td>
                                 <%--<td align="left" valign="top" width="10%">
                                    <uc2:ApproveLRLOV ID="txtShadeCode" Width="100%" TabIndex="18" runat="server" />
                                </td>--%>
                                <td align="left" valign="top" width="8%">
                                    <asp:TextBox ID="txtNoofUnit" runat="server" CssClass="TextBoxNo SmallFont TextBoxDisplay" Width="95%"
                                        AutoPostBack="True" TabIndex="19" OnTextChanged="txtNoofUnit_TextChanged"></asp:TextBox>
                                </td>
                                 <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtsalerate" runat="server" CssClass="SmallFont TextBoxNo" Width="95%"
                                        AutoPostBack="True" TabIndex="19" OnTextChanged="txtsalerate_TextChanged"></asp:TextBox>
                                </td>
                               <%-- <td align="left" valign="top" width="10%">
                                
                                 <asp:Button ID="btnDisc" runat="server" Text="Disc/Taxes" OnClick="btnDisc_Click"
                                    CssClass="SmallFont " Width="100%" />
                                        <%--<td align="left" valign="top" width="8%">
                                 </td>
                                   <td align="left" valign="top" width="10%">
                                <asp:TextBox ID="txtfinal" runat="server" CssClass="SmallFont TextboxNo" OnTextChanged="txtfinal_TextChanged"
                                    Width="100%"></asp:TextBox>
                            </td>--%>
                                
                                 <td align="left" valign="top" width="8%">
                                 
                                        <asp:TextBox ID="txtTotalCost" runat="server" ReadOnly="true" CssClass="TextBoxNo SmallFont TextBoxDisplay"
                            Width="80px"></asp:TextBox>
                                </td>
                               <%-- <td align="left" valign="top" width="10%">
                                    <asp:DropDownList CssClass="SmallFont TextBox UpperCase" TabIndex="21" ID="txtEndUse"
                                        runat="server" AppendDataBoundItems="true" Width="100%">
                                    </asp:DropDownList>
                                </td>--%>
                                <td align="left" valign="top" width="8%">
                                    <asp:TextBox ID="txtReqDate" runat="server" TabIndex="11" Width="100px" CssClass="SmallFont TextBox UpperCase"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfvod0" runat="server" ControlToValidate="txtReqDate"
                                        Display="Dynamic" ErrorMessage="Enter Req Date" Font-Bold="False" ValidationGroup="ss"></asp:RequiredFieldValidator>
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:TextBox ID="txtRemarks" runat="server" TabIndex="22" CssClass="SmallFont TextBox"
                                        Width="95%"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" width="10%">
                                    <asp:Button ID="btnSTSave" runat="server" CssClass="SmallFont" TabIndex="23" OnClick="btnSTSave_Click"
                                        Text="Save" ValidationGroup="ss" Width="60px" />
                                    <asp:Button ID="btnSTCancel" runat="server" CssClass="SmallFont" TabIndex="24" OnClick="btnSTCancel_Click"
                                        Text="Cancel" Width="60px" />
                                </td>
                            </tr>
                            <tr>
                              <td align="left" valign="top" width="5%" runat="server" visible = "False" >
                                    <asp:TextBox ID="Txtpino" runat="server" TabIndex="12" CssClass="SmallFont TextBox"
                                        Width="95%"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" width="5%" runat="server" visible = "False" >
                                    <asp:TextBox ID="TxttrnType" runat="server" TabIndex="12" CssClass="SmallFont TextBox"
                                        Width="95%"></asp:TextBox>
                                </td>
                                <td align="left" valign="top" width="5%" runat="server" visible = "False" >
                                    <asp:TextBox ID="TxtTrnNo" runat="server" TabIndex="12" CssClass="SmallFont TextBox"
                                        Width="95%"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td width="100%">
                        <table width="100%">
                            <tr id="tr4" runat="server">
                                <td id="Td4" runat="server" align="left" class="td" width="100%">
                                    <asp:GridView ID="GridSpinningThread" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        CssClass="SmallFont" Font-Bold="False" OnRowCommand="GridSpinningThread_RowCommand"
                                        Width="100%">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtArticleNo" runat="server" Font-Bold="true" Text='<%# Bind("ITEM_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Desc.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtfabrdesc" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_DESC") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Pi No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="Txtpino" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PI_NO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Trn No.">
                                                <ItemTemplate>
                                                    <asp:Label ID="TxtTrnNo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NUMB") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Trn Type.">
                                                <ItemTemplate>
                                                    <asp:Label ID="TxttrnType" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_TYPE") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ShadeCode.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtShadeCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Issue Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtQuantity" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                        Text='<%# Bind("ISS_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Waste_QTY.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtsalerate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("WASTE_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                          
                                            
                                            <asp:TemplateField HeaderText="P_QTY.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtTotalCost" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("P_QTY") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Req Date.">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtReqDate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("REQ_DATE" , "{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtRemarks" runat="server" CssClass="LabelNo SmallFont" Font-Bold="true"
                                                        Text='<%# Bind("REMARKS") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkEdit" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                        CommandName="EditItem" Text="Edit" />
                                                    <asp:LinkButton ID="lnkDelete" runat="server" CommandArgument='<%# Bind("UNIQUE_ID") %>'
                                                        CommandName="DelItem" Text="Delete" />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheadingGrid" />
                                        <RowStyle CssClass="SmallFont" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<asp:Label ID="lblUNIT_WT" runat="server" Visible="false" Text=""></asp:Label>
<asp:Label ID="lblNETBOX_WT" runat="server" Visible="false" Text=""></asp:Label>
<asp:Label ID="lblNETCART_WT" runat="server" Visible="false" Text=""></asp:Label>
<cc3:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
    MaskType="Date" PromptCharacter="_" TargetControlID="txtReqDate">
</cc3:MaskedEditExtender>
<cc3:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtReqDate">
</cc3:CalendarExtender>
 <%--<cc3:CalendarExtender ID="ceDoc" runat="server" TargetControlID="TextBox1" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>--%>
       <%-- <cc3:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="TextBox1" PromptCharacter="_">
        </cc3:MaskedEditExtender>
        <%--going<asp:ValidationSummary ID="ValidationSummary2" runat="server" ShowMessageBox="True"
            ShowSummary="false" ValidationGroup="M1" />
        <cc3:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtLCDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc3:CalendarExtender>
        <cc3:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLCDate" PromptCharacter="_">
        </cc3:MaskedEditExtender>--%>
</ContentTemplate>
</asp:UpdatePanel>