<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IssueAgainstWorkOrder.ascx.cs"
    Inherits="Module_WorkOrder_Controls_IssueAgainstWorkOrder" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
      
    function Calculation(val)
    {                                                                
     document.getElementById('ctl00_cphBody_DepotSaleInvoice1_txtAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_DepotSaleInvoice1_txtFinalRate').value)*(parseFloat(document.getElementById('ctl00_cphBody_DepotSaleInvoice1_txtQTY').value))).toFixed(3) ;
   }
   
     
</script>

<style type="text/css">
    .style1
    {
        font-size: 8pt;
        font-weight: bold;
    }
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
        width: 60px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
        margin-left: 4px;
        width: 120px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 350px;
    }
    .c6
    {
        margin-left: 4px;
        width: 80px;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table class="tdMain" width="920px">
    <tr>
        <td width="100%" class="td">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                            ImageUrl="~/CommonImages/save.jpg" ValidationGroup="gg" Style="height: 41px">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png" Enabled="false"></asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
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
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Issue Against Work Order</b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                    ShowSummary="False" />
                <asp:Label ID="lblMode" runat="server"></asp:Label></span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr>
                    <td align="right" valign="top" width="15%">
                        <b>
                            <asp:Label ID="Label15" runat="server" Text="TRN No: " CssClass="LabelNo SmallFont"></asp:Label>
                        </b>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtTRNNUMBer" runat="server" ValidationGroup="M1" Width="98%" TabIndex="1"
                            CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true" Font-Bold="True"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="98%" Height="200px"
                            MenuWidth="500px" Font-Bold="True">
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c4">
                                    <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <HeaderTemplate>
                                <div class="header c1">
                                    MRN #</div>
                                <div class="header c2">
                                    MRN Date</div>
                                <div class="header c2">
                                    Party Code</div>
                                <div class="header c4">
                                    Party Name</div>
                            </HeaderTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <b>
                            <asp:Label ID="Label16" runat="server" Text="Issue Date : " CssClass="Label SmallFont"></asp:Label>
                        </b>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtMRNDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="80px"
                            CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true" Font-Bold="True"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <b>
                            <asp:Label ID="Label17" runat="server" Text="Issue Shift : " CssClass="Label SmallFont"></asp:Label>
                        </b>
                    </td>
                    <td align="left" valign="top" width="25%">
                        <asp:DropDownList ID="ddlReceiptShift" CssClass="SmallFont" Width="98%" runat="server" TabIndex="2"
                            Font-Bold="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label1" runat="server" Text="Jober Party :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <cc2:ComboBox ID="ddlJoberParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlJoberParty_LoadingItems" DataTextField="JOBER_PARTY" DataValueField="JOBER_NAME"
                            OnSelectedIndexChanged="ddlJoberParty_SelectedIndexChanged" Width="98%" MenuWidth="500px"
                            Height="200px">
                            <HeaderTemplate>
                            <div class="header c1">
                                   WORK NO</div>                                                                                                                                                            
                                <div class="header c1">
                                   JOBER PARTY</div>
                                <div class="header c5">
                                   JOBER NAME</div>
                                
                            </HeaderTemplate>
                            <ItemTemplate>
                            <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("WO_NUMB") %>' /></div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container7" Text='<%# Eval("JOBER_PARTY") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container8" Text='<%# Eval("JOBER_NAME") %>' /></div>
                                <%--<div class="item c4">
                                    <asp:Literal runat="server" ID="Container9" Text='<%# Eval("JOBER_ADDRESS") %>' /></div>--%>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td align="right" valign="top" colspan="4">
                        <asp:TextBox ID="txtJoberPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="11%" ReadOnly="true"></asp:TextBox>
                        <asp:TextBox ID="txtJoberPartyAddress" TabIndex="4" runat="server" Width="85%" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                
                 <tr>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label19" runat="server" CssClass="LabelNo SmallFont" Text="Party Code :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="17%">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="17%">
                                <asp:Label ID="Label23" runat="server" CssClass="LabelNo SmallFont" Text="Party Details :"></asp:Label>
                            </td>
                            <td class="tdLeft" colspan="1" style="width: 32%">
                                <asp:TextBox ID="txtPartyAddress" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="250px"></asp:TextBox>
                            </td>
                            
                            <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label2" runat="server" Text="Product Type :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtProductType" runat="server" TabIndex="14" Width="98%" CssClass="TextBoxNo  TextBoxDisplay SmallFont"
                           ReadOnly="true"  MaxLength="15"></asp:TextBox>
                    </td>
                            
                        </tr>
                <tr>
                             <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="3" width="85%">
                        <cc2:ComboBox ID="ddlTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlTransporterCode_SelectedIndexChanged"
                            Width="150px" EmptyText="Select transaporter" MenuWidth="600px" Height="200px" TabIndex="7">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtTransporterCode"  runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="70px" TabIndex="70"></asp:TextBox>
                        <asp:TextBox ID="txtTransporterAdd" TabIndex="75" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="250px"></asp:TextBox>
                    </td>
                            <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label3" runat="server" Text="Job Type :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtcategoryType" runat="server" TabIndex="14" Width="98%" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                           ReadOnly="true"  MaxLength="15"></asp:TextBox>
                    </td>
                            
                          
                             
                        </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label7" runat="server" Text="L.R. Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLRNo" runat="server" TabIndex="14" Width="98%" CssClass="TextBoxNo SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label8" runat="server" Text="L.R. Date :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    
                    </td>
                    
                      <td class="tdRight" width="17%">
                                <asp:Label ID="Label21" runat="server" CssClass="Label SmallFont" Text="Location :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                      <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="2"
                Width="150px">
            </asp:DropDownList></td>
            
                     
                  <%--  <td align="right" valign="top" width="15%">--%>
                        <cc1:CalendarExtender ID="celr" runat="server" TargetControlID="txtLRDate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    <%--</td>--%>
                    <td align="left" valign="top" width="25%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" colspan="3">
                        <asp:TextBox ID="txtRemarks" runat="server" Width="99%" TabIndex="21" CssClass="TextBox SmallFont"
                            MaxLength="200"></asp:TextBox>
                    </td>
                    
                      <td class="tdRight" width="17%">
                                <asp:Label ID="Label25" runat="server" CssClass="Label SmallFont" Text="Store :"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                         
            
            <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="3"
                Width="150px">
            </asp:DropDownList>
            </td>
                    
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td class="style1" width="22%">
                        WO Numb
                    </td>
                    <td class="style1" width="10%">
                        Adjustment
                    </td>
                     <td class="style1" width="12%" align="right">
                        Cartons
                    </td>
                    <td class="style1" width="12%" align="right">
                        Qty
                    </td>
                     <td class="style1" width="12%" align="right">
                        No Of Unit
                    </td>
                    <td class="style1" width="12%">
                        UOM
                    </td>
                    <td class="style1" width="12%" align="right">
                        Final Rate
                    </td>
                    <td class="style1" width="12%">
                        Remarks
                    </td>
                    <td class="style1" width="10%">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="SmallFont" width="22%">
                        <cc2:ComboBox ID="lblPOITEM" runat="server" CssClass="SmallFont" EmptyText="select..."
                            AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="680px" Width="99%" OnLoadingItems="lblPOITEM_LoadingItems"
                            OnSelectedIndexChanged="lblPOITEM_SelectedIndexChanged" Height="200px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    WO No.</div>
                                <div class="header c2">
                                    Article Code</div>
                                <div class="header c3">
                                    Description</div>
                                <div class="header c2">
                                    Shade Code</div>
                                <div class="header c2">
                                    Quantity</div>
                                <div class="header c2">
                                    Qty Issued.</div>
                                <div class="header c6">
                                    Qty. Rem.</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("WO_NUMB") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("BASE_ARTICLE_CODE") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("BASE_ARTICLE_DESC") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("BASE_SHADE_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("QTY") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("QTY_ISS") %>' /></div>
                                <div class="item c6">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("QTY_REM") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="SmallFont" width="10%">
                        <asp:Button ID="btnSubDetail" TabIndex="24" runat="server" Font-Size="8pt" Text="Adj. Rec."
                            Width="70px" OnClick="btnSubDetail_Click1" />
                    </td>
                    
                    <td class="SmallFont" width="12%">
                        <asp:TextBox ID="txtCarton" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"
                           ReadOnly="false" ></asp:TextBox>
                    </td>
                    <td class="SmallFont" width="12%">
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"
                            onkeyup="javascript:Calculation(this.id)"></asp:TextBox>
                    </td>
                    
                     <td width="8%">
                                <asp:TextBox ID="txtnoofunit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="100%" ReadOnly="True"></asp:TextBox>
                            </td>
                    
                    <td class="SmallFont" width="12%">
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%"></asp:TextBox>
                    </td>
                    <td class="SmallFont" width="12%">
                        <asp:TextBox ID="txtFinalRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="99%" onkeyup="javascript:Calculation(this.id)"></asp:TextBox>
                    </td>
                    <td class="SmallFont" width="22%">
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="99%"
                            MaxLength="200"></asp:TextBox>
                    </td>
                    <td class="SmallFont" width="10%">
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" Width="60px" />
                    </td>
                </tr>
                <tr>
                    <td class="SmallFont" colspan="7" width="90%">
                        WO Numb :<asp:TextBox ID="cmbPOITEM" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="80px" ReadOnly="true"></asp:TextBox>
                        &nbsp;Code/Desc :<asp:TextBox ID="txtICODE" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="70px" ReadOnly="true"></asp:TextBox>
                        :<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="190px"></asp:TextBox>
                        &nbsp;Rem Qty :<asp:TextBox ID="lblMaxQTY" runat="server" CssClass="TextBoxDisplay SmallFont TextBoxNo"
                            Width="60px" ReadOnly="True"></asp:TextBox>
                        &nbsp;Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            Width="100px" ReadOnly="true"></asp:TextBox>
                    </td>
                    <td class="SmallFont" width="10%">
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" Width="60px" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="grdMaterialItemReceipt" Width="99%" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" ShowFooter="false" OnRowCommand="grdMaterialItemReceipt_RowCommand"
                    OnRowDataBound="grdMaterialItemReceipt_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="WO #">
                            <ItemTemplate>
                                <asp:Label ID="txtPONum" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                    Width="40px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Article Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Shade">
                            <ItemTemplate>
                                <asp:Label ID="txtSHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date of Mfg." Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtDOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("DATE_OF_MFG", "{0:dd-MMM-yyyy}") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Cartons">
                            <ItemTemplate>
                                <asp:Label ID="txtCartons" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("CARTONS") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No Of Unit">
                            <ItemTemplate>
                                <asp:Label ID="lblnoofunit" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="F. Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtFinalRate" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("FINAL_RATE") %>'
                                    Width="50px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("AMOUNT") %>'
                                    Width="70px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                    Width="120px" ReadOnly="true"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>/
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
                <asp:Label ID="lblSO_BRANCH" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSO_TYPE" runat="server" Visible="false"></asp:Label>
                <asp:Label ID="lblSO_COMP" runat="server" Visible="false"></asp:Label>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:RequiredFieldValidator ControlToValidate="txtTRNNUMBer" ID="rfv1" runat="server"
    ValidationGroup="M1" ErrorMessage="MRN number required" Display="Dynamic" SetFocusOnError="True"></asp:RequiredFieldValidator>
</ContentTemplate>
</asp:UpdatePanel>