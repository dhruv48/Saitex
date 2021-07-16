<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InvoiceToParty.ascx.cs" Inherits="Module_Yarn_SalesWork_Controls_InvoiceToParty" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; 
        *display:inline;overflow:hidden;white-space:nowrap;
     }
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
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 150px;
    }
    
    .c6
    {
        margin-left: 4px;
        width: 300px;
    }
    .SmallFont
    {
        width: 42px;
    }
    .panelclass
    {
        border-color: Red;
        border-style: solid;
        border-width: 2;
        background-color: Gray;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table class="tdMain" width="100%">
            <tr>
                <td class="td" width="100%">
                    <table class="tContentArial">
                        <tr>
                            <td id="tdSave" runat="server">
                                <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="Save"
                                    ImageUrl="~/CommonImages/save.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdUpdate" runat="server">
                                <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                                    ImageUrl="~/CommonImages/edit1.jpg" ValidationGroup="M1"></asp:ImageButton>
                            </td>
                            <td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ToolTip="Delete"
                                    ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
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
                    <b class="titleheading">Delevery Order Challan (Yarn)</b>
                </td>
            </tr>
            <tr>
                <td class="td tdLeft" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr>
                           <td class="tdRight" width="15%">
                                <%--<asp:Label ID="Label8" runat="server" Text="Buyer's PO Date :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>--%>
                                      <asp:Label ID="Label8" runat="server" Text="Challan Type:" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                          
                               <asp:DropDownList ID="ddlTrnType" runat="server" CssClass="SmallFont"   
                                    Width="150px" AutoPostBack="true" 
                                    onselectedindexchanged="ddlTrnType_SelectedIndexChanged" >
                                <asp:ListItem Selected="True" Text="Sale Work" Value="IYS26"></asp:ListItem>
                                <asp:ListItem  Text="Job Work"  Value="IYS27"></asp:ListItem>
                                <asp:ListItem  Text="From Stock"  Value="IYS29"></asp:ListItem>
                               
                               </asp:DropDownList>  
                                <asp:TextBox ID="txtDocDate" runat="server" T Width="150px"
                                    CssClass="TextBox TextBoxDisplay SmallFont" Visible="false"></asp:TextBox>
                               </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label15" runat="server" Text="Challan No : " CssClass="LabelNo SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="99%"
                                    TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                                    OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>                                    
                                <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                    OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged" Width="99%" Height="200px" EmptyText="Select Challan No"
                                    MenuWidth="500px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Challan No #</div>
                                        <div class="header c2">
                                            Challan Date</div>
                                        <div class="header c4">
                                            Department</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container4" Text='<%# Eval("TRN_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container6" Text='<%# Eval("TRN_DATE","{0:dd/MM/yyyy}") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Literal5" Text='<%# Eval("DEPT_NAME") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                         <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                         <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>                                
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label16" runat="server" Text="Challan Date : " CssClass="Label tdRight SmallFont"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1" Width="150px"
                                    CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                            </td>
                           
                        </tr>
                        <tr>
                     
                            <td class="tdRight" width="15%">                            
                           
                            
                            <asp:Label ID="Label7" runat="server" Text="Doc/Form No :"
                                CssClass="LabelNo SmallFont" Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">                         
                        <asp:TextBox ID="txtInvoiceNo" runat="server" TabIndex="14" Width="99%"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label12" runat="server" Text="Buyer's PO No :"
                                CssClass="LabelNo SmallFont" Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtBuyerorder" runat="server" TabIndex="14" Width="99%"
                            CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="15%">
                                <asp:Label ID="Label3" runat="server" CssClass="Label SmallFont " Text="Shift : "></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:DropDownList ID="ddlIssueShift"  runat="server" TabIndex="2" Width="150px">
                                </asp:DropDownList>
                            </td>
                            <td class="tdRight" width="15%">
                                &nbsp;
                            </td>
                            <td class="tdLeft" width="25%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Party Detail :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="CmBParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="CmBParty_LoadingItems" DataTextField="PRTY_CODE" DataValueField="XYZ"
                                    OnSelectedIndexChanged="CmBParty_SelectedIndexChanged" Width="99%" Height="200px"
                                    MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Party">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c6">
                                            NAME</div>
                                       <%-- <div class="header c4">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c6">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                       <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                         <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                         <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" colspan="2">
                                <asp:TextBox ID="lblPartyCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="10%"></asp:TextBox>
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                             <td class="tdRight" >
                                <asp:Label ID="Label13" runat="server" Text="Location :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" >
                               <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="2"
                Width="150px" Enabled="false" >
            </asp:DropDownList>  
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="txtTransporterCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtTransporterCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged" Width="99%"
                                    Height="200px" MenuWidth="550px" EnableVirtualScrolling="true" EmptyText="Select Transporter">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                       <%-- <div class="header c4">
                                            Address</div>--%>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                       <%-- <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("Address") %>' /></div>--%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                         <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                         <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="70%" colspan="2">
                                <asp:TextBox ID="lblTransporterCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="10%"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterAddress" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                             <td class="tdRight" >
                                <asp:Label ID="Label17" runat="server" Text="Store :" CssClass="LabelNo SmallFont"></asp:Label>
                            </td>
                            <td align="left" valign="top" >
                               <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="2"
                Width="150px">
            </asp:DropDownList>  
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="15%">
                                <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Consignee Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%">
                                <cc2:ComboBox ID="txtConsigneeCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtConsigneeCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Consignee" OnSelectedIndexChanged="txtConsigneeCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="99%" MenuWidth="800px" Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c4">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                         <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                         <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="70%" colspan="2">
                                <asp:TextBox ID="lblConsigneeCode" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="10%"></asp:TextBox>
                                <asp:TextBox ID="txtConsigneeAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" TextMode="SingleLine" Width="85%"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label9" runat="server" Text="Truck No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="16" Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                              
                            </td>
                        </tr>
                        <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label5" runat="server" Text="LR No :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRNumber" runat="server" TabIndex="15" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label6" runat="server" Text="LR Date :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtLRDate" runat="server" TabIndex="15" Width="99%" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label14" runat="server" Text="Eway Bill No.:" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="150px" TabIndex="21" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="trOther" runat="server" visible="false">
                            <td  colspan="8" align ="center" ><%-- class="tdleft"--%>
                                <asp:Panel ID="pnlOtherDTL" runat="server" BackColor="LightSteelBlue" BorderStyle="Ridge" 
                                    BorderWidth="5px">
                                    <table>
                                        <tr id="RDA_NO" runat="server"  >
                                            <td align="right">
                                                other Reference(s) D/A NO. :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInsurancePolicyNo" runat="server" CssClass="TextBox SmallFont"
                                                    TabIndex="27" Width="80px"></asp:TextBox>
                                            </td>
                                             <td align="right">
                                                 other Reference(s) Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtLCDate" runat="server" CssClass="TextBox SmallFont" TabIndex="27"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                buyer(other than consignee) :
                                            </td>
                                            <td align="left">
                                               <asp:TextBox ID="txtbuyer" runat="server" CssClass="TextBox SmallFont"
                                                    TabIndex="27" Width="80px"></asp:TextBox>
                                            </td>
                                           
                                          <%--  <td align="right" runat ="server" visible ="false">
                                                Sale Against :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleAgainst" runat="server" visible ="false" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="28" Width="80px"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                           <%-- <td align="right"runat ="server" visible ="false">
                                                Excise Notification No :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseNo" runat="server" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="29" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise Notification Date :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseDate" runat="server" CssClass="TextBox SmallFont" TabIndex="30"
                                                    Width="80px"></asp:TextBox>
                                            </td>--%>
                                             <td align="right">
                                                  Date & Time of preparation :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtdateprp" runat="server" CssClass="TextBox SmallFont" TabIndex="27"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                             <td align="right">
                                                 Date & Time removal :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtdateremvl" runat="server" CssClass="TextBox SmallFont" TabIndex="27"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Freight Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtFreight" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="18" OnTextChanged="txtFreight_TextChanged" TabIndex="31" ValidationGroup="M1"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                           <%-- <td align="right"runat ="server" visible ="false">
                                                Insurance Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtInsurance" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="18" OnTextChanged="txtInsurance_TextChanged" TabIndex="32" ValidationGroup="M1"
                                                    Width="80px"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                          <%--  <td align="right"runat ="server" visible ="false">
                                                Excise On Base Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseOnBaseRate" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="12" OnTextChanged="txtExciseOnBaseRate_TextChanged" TabIndex="36"
                                                    ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise On CESS Rate 1:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseOnCESSRate1" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="12" OnTextChanged="txtExciseOnCESSRate1_TextChanged" TabIndex="37"
                                                    ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise On CESS Rate 2:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseOnCESSRate2" runat="server" AutoPostBack="True" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="12" OnTextChanged="txtExciseOnCESSRate2_TextChanged" TabIndex="38"
                                                    ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise Total Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseTotalRate" runat="server" CssClass="TextBoxNo SmallFont"
                                                    MaxLength="12" ReadOnly="True" TabIndex="39" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise Base Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseBaseAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="40" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise CESS Amount 1 :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseCESSAmt1" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="41" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise CESS Amount 2 :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseCESSAmt2" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="42" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Excise Total Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtExciseTotalAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="43" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>--%>
                                        </tr>
                                        <tr>
                                          <%--  <td align="right" runat ="server" visible ="false">
                                                Sale TAX Type:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSaleTax" runat="server" CssClass="SmallFont" TabIndex="44"
                                                    Width="80">
                                                    <asp:ListItem Selected="True" Text="Select" Value="Select"></asp:ListItem>
                                                    <asp:ListItem Text="VAT" Value="VAT"></asp:ListItem>
                                                    <asp:ListItem Text="CST" Value="CST"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Sale TAX Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleTAXRate" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="12"
                                                    OnTextChanged="txtSaleTAXRate_TextChanged" TabIndex="45" ValidationGroup="M1"
                                                    Width="80px" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                            <td align="right" runat ="server" visible ="false">
                                                Sale TAX Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleTAXAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="46" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td> --%>
                                            <td align="right">
                                                Goods Dispatch From:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtgoodsdisptch" runat="server" CssClass="TextBox SmallFont" MaxLength="25"
                                                    TabIndex="47" Width="80px"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Final Destination:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtDestination" runat="server" CssClass="TextBox SmallFont" MaxLength="25"
                                                    TabIndex="47" Width="80px"></asp:TextBox>
                                            </td>
                                             <td id="Td1" align="right" runat ="server" >
                                                Other Charges:
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtED" runat="server" AutoPostBack="True" OnTextChanged="txtED_TextChanged" CssClass="TextBox SmallFont" MaxLength="50"
                                                    TabIndex="28" Width="80px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        
                                        <tr>
                                            <td align="right">
                                                Sale TAX Type:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSaleTax" runat="server" CssClass="SmallFont" TabIndex="44"
                                                    Width="80">
                                                   <%-- <asp:ListItem Selected="True" Text="Select" Value="Select"></asp:ListItem>--%>
                                                    <asp:ListItem Text="CGST" Value="CGST"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Sale TAX Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleTAXRate" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="12"
                                                    OnTextChanged="txtSaleTAXRate_TextChanged" TabIndex="45" ValidationGroup="M1"
                                                    Width="80px" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Sale TAX Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleTAXAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="46" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                           
                                        </tr>
                                        
                                        
                                        
                                        <tr>
                                            <td align="right">
                                                Sale TAX Type:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlSSaleTax" runat="server" CssClass="SmallFont" TabIndex="44"
                                                    Width="80">
                                                   <%-- <asp:ListItem Selected="True" Text="Select" Value="Select"></asp:ListItem>--%>
                                                    <asp:ListItem Text="SGST" Value="SGST"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Sale TAX Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleSGSTTAXRate" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="12"
                                                    OnTextChanged="txtSaleSGSTTAXRate_TextChanged" TabIndex="45" ValidationGroup="M1"
                                                    Width="80px" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Sale TAX Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleSGSTTAXAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="46" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                           
                                        </tr>
                                        
                                        
                                        <tr>
                                            <td align="right">
                                                Sale TAX Type:
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlISaleTax" runat="server" CssClass="SmallFont" TabIndex="44"
                                                    Width="80">
                                                   <%-- <asp:ListItem Selected="True" Text="Select" Value="Select"></asp:ListItem>--%>
                                                    <asp:ListItem Text="IGST" Value="IGST"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                Sale TAX Rate :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleIGSTTAXRate" runat="server" CssClass="TextBoxNo SmallFont" MaxLength="12"
                                                    OnTextChanged="txtSaleIGSTTAXRate_TextChanged" TabIndex="45" ValidationGroup="M1"
                                                    Width="80px" AutoPostBack="True"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                Sale TAX Amount :
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="txtSaleIGSTTAXAmt" runat="server" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                                    MaxLength="18" ReadOnly="True" TabIndex="46" ValidationGroup="M1" Width="80px"></asp:TextBox>
                                            </td>
                                           
                                        </tr>
                                        
                                        
                                        <tr>
                                            <td align="center" colspan="6">
                                                <asp:Button ID="btncncelpack" runat="server" OnClick="btncncelpack_Click" TabIndex="48"
                                                    Text="Hide Others" ValidationGroup="M1" Width="100px" />
                                                <asp:RangeValidator ID="RVFreight" runat="server" ControlToValidate="txtFreight"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Freight" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                              <%--  <asp:RangeValidator ID="RVInsurance" runat="server" ControlToValidate="txtInsurance"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Insurance" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVSaleTAXRate" runat="server" ControlToValidate="txtSaleTAXRate"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Sale TAX Rate" MaximumValue="100"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVSaleTAXAmt" runat="server" ControlToValidate="txtSaleTAXAmt"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Sale TAX Amount" MaximumValue="9999999"
                                                    MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseOnBaseRate" runat="server" ControlToValidate="txtExciseOnBaseRate"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise On Base Rate"
                                                    MaximumValue="100" MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseOnCESSRate1" runat="server" ControlToValidate="txtExciseOnCESSRate1"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise On CESS Rate One"
                                                    MaximumValue="100" MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseOnCESSRate2" runat="server" ControlToValidate="txtExciseOnCESSRate2"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise On CESS Rate Two"
                                                    MaximumValue="100" MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseTotalRate" runat="server" ControlToValidate="txtExciseTotalRate"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise Total Rate"
                                                    MaximumValue="100" MinimumValue="0" SetFocusOnError="true" Type="Double" ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseBaseAmt" runat="server" ControlToValidate="txtExciseBaseAmt"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise Base Amount"
                                                    MaximumValue="9999999" MinimumValue="0" SetFocusOnError="true" Type="Double"
                                                    ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseCESSAmt1" runat="server" ControlToValidate="txtExciseCESSAmt1"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise CESS Amount One"
                                                    MaximumValue="9999999" MinimumValue="0" SetFocusOnError="true" Type="Double"
                                                    ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseCESSAmt2" runat="server" ControlToValidate="txtExciseCESSAmt2"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise CESS Amount Two"
                                                    MaximumValue="9999999" MinimumValue="0" SetFocusOnError="true" Type="Double"
                                                    ValidationGroup="M1"></asp:RangeValidator>
                                                <asp:RangeValidator ID="RVExciseTotalAmt" runat="server" ControlToValidate="txtExciseTotalAmt"
                                                    Display="None" ErrorMessage="Please Enter Numeric Only, in Excise Total Amount"
                                                    MaximumValue="9999999" MinimumValue="0" SetFocusOnError="true" Type="Double"
                                                    ValidationGroup="M1"></asp:RangeValidator>--%>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr runat="server" id="trSubTotal">
                            <td class="tdRight" width="15%" >
                                <asp:Label ID="Label10" runat="server" Text="Sub Total :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtSubTotal" runat="server" Width="120px" TabIndex="21" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label11" runat="server" Text="Grand Total :" CssClass="Label SmallFont tdRight"
                                    Width="100%"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <asp:TextBox ID="txtFinalTotal" runat="server" Width="120px" TabIndex="21" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                            </td>
                            <td class="tdRight" width="15%">
                              <asp:Label ID="Label18" runat="server" Text="Net Weight:" CssClass="Label SmallFont tdRight"
                                    Width="50%"></asp:Label>
                                <asp:TextBox ID="txtNetWeight" runat="server" Width="70px" TabIndex="21" CssClass="TextBoxDisplay TextBoxNo SmallFont"
                                    ReadOnly="True"></asp:TextBox>
                                    
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:Button ID="btnOther" runat="server" TabIndex="17" Text="Show Other" OnClick="btnOther_Click" CssClass="SmallFont"
                                    Width="80px" />
                            </td>
                        </tr>
                      
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <table width="100%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td width="15%">
                                SO No
                            </td>
                            <td width="10%">
                                Packed Detail
                            </td>
                            <td align="right" width="8%">
                                Total Cones
                            </td>
                            <td align="right" width="8%">
                                Uom
                            </td>
                            <td align="right" width="8%">
                                Avg Weight
                            </td>
                            <td align="right" width="8%">
                                Net Weight
                            </td>
                            <td align="right" width="10%" runat="server" id="thBasic" >
                                Basic Rate
                            </td>
                            <td align="right" width="10%" runat="server" id="thTax" visible="false">
                                Disc/Tax
                            </td>
                            <td align="right" width="10%" runat="server" id="thFinal"  visible="false">
                                Final rate
                            </td>
                            <td align="right" width="10%" runat="server" id="thAmount">
                                Amount
                            </td>
                            <td width="15%">
                                Remarks
                            </td>
                            <td width="8%">
                            </td>
                        </tr>
                        <tr>
                            <td width="15%">
                                <cc2:ComboBox ID="DDLPiNo" runat="server" CssClass="SmallFont" AutoPostBack="True"
                                    EnableLoadOnDemand="true" MenuWidth="750px" Width="99%" OnLoadingItems="DDLPiNo_LoadingItems"
                                    OnSelectedIndexChanged="DDLPiNo_SelectedIndexChanged" Height="200px" EmptyText="Select Sales Order">
                                    <HeaderTemplate>
                                        <div class="header c5">
                                            SO No</div>
                                        <div class="header c2"> 
                                            Article Code</div>
                                        <div class="header c2">
                                            Shade</div>
                                        <div class="header c5">
                                            Party</div>
                                        <div class="header c2">
                                            Apr.Unit</div>
                                        <div class="header c2">
                                            Bal.Unit</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c5">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("CUSTNO") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ARTICLE_NO") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal8" Text='<%# Eval("SHADE_CODE") %>' />
                                        </div>
                                        <div class="item c5">
                                            <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("PRTY_DATA") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("APP_NO_OF_UNIT") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("REM_INVOICE_NO_OF_UNIT") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                         <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>out of
                                         <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td width="10%">
                                <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Text="Cartoons" Width="75px" />
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtQtyUnit" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtQtyUom" runat="server" CssClass="TextBox TextBoxDisplay SmallFont "
                                    ReadOnly="True" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtQtyWeight" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%">
                                <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="99%" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td width="10%" runat="server" id="trBasic" >
                                <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo SmallFont" Width="99%"
                                    AutoPostBack="True" OnTextChanged="txtBasicRate_TextChanged"></asp:TextBox>
                            </td>
                            <td width="10%" runat="server" id="trTax"  visible="false">
                                <asp:Button ID="btnDisc" runat="server" Text="Disc/Taxes" OnClick="btnDisc_Click"
                                    CssClass="SmallFont " Width="100%" />
                            </td>
                            <td width="10%" id="trFinal" runat="server" visible="false" >
                                <asp:TextBox ID="txtfinal" runat="server" CssClass="SmallFont TextboxNo" OnTextChanged="txtfinal_TextChanged" AutoPostBack="true"
                                    Width="100%"></asp:TextBox>
                            </td>
                            <td width="10%" runat="server" id="trAmount" >
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="99%"></asp:TextBox>
                            </td>
                            <td width="15%">
                                <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="99%"></asp:TextBox>
                            </td>
                            <td width="8%" align="center">
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save" Width="70px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9" width="92%">
                                SO No.:<asp:TextBox ID="txtPA_NO" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="80px"></asp:TextBox>
                                Article Code/Desc:<asp:TextBox ID="txtArticleCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px"></asp:TextBox>
                                <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                                    &nbsp;Shade family:<asp:TextBox ID="txtShadeFamily" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px"></asp:TextBox>
                                &nbsp;Shade:<asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="100px"></asp:TextBox>
                                UOM:
                                <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                                Max Qty:<asp:TextBox ID="txtMaxQty" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                                    &nbsp;
                                    <asp:HiddenField ID="txtGrossWt" runat="server" /><asp:HiddenField ID="txtTareWt" runat="server" /><asp:HiddenField ID="txtCartons" runat="server" />
                                    <asp:HiddenField ID="hdnLotNo" runat="server" />
                                    <asp:HiddenField ID="hdnGrade" runat="server" />
                            </td>
                            <td width="8%" align="center">
                                <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click" Width="70px"
                                    Text="Cancel" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td width="100%" class="td">
                    <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                        <asp:GridView ID="grdMaterialItemIssue" runat="server" AutoGenerateColumns="False"
                            CssClass="SmallFont" Width="99%" ShowFooter="false" OnRowCommand="grdMaterialItemIssue_RowCommand"
                            OnRowDataBound="grdMaterialItemIssue_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SO No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPICode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PI_NO") %>'
                                            ReadOnly="True" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Yarn Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                            ReadOnly="True" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shade">
                                    <ItemTemplate>
                                        <asp:Label ID="txtSHADE_CODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("SHADE_CODE") %>'
                                            ReadOnly="True" Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Cones">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyUOM" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Avg Wt">
                                    <ItemTemplate>
                                        <asp:Label ID="txtQtyWeight" runat="server" CssClass="Label SmallFont" Text='<%# Bind("WEIGHT_OF_UNIT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Net Weight">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'></asp:LinkButton>
                                        <asp:Panel ID="pnlrecadj" runat="server" CssClass="panelclass">
                                            <asp:GridView ID="grdRecAdj" runat="server" AutoGenerateColumns="False" CssClass="SmallFont">
                                                <Columns>
                                                    <%--<asp:BoundField HeaderText="P. Type" DataField="TRN_TYPE" />
                                                    <asp:BoundField HeaderText="Packing&nbsp;Numb" DataField="TRN_NUMB" />
                                                    <asp:BoundField HeaderText="Comp" DataField="PO_COMP" />
                                                    <asp:BoundField HeaderText="P.Branch" DataField="PO_BRANCH" />
                                                    <asp:BoundField HeaderText="P.Type" DataField="PO_TYPE" />
                                                    <asp:BoundField HeaderText="Rec PA No" DataField="PI_NO" />
                                                    <asp:BoundField HeaderText="Production&nbsp;Numb" DataField="PO_NUMB" />--%>
                                                    <asp:BoundField HeaderText="Artiicle" DataField="YARN_CODE" />
                                                    <asp:BoundField HeaderText="Shade" DataField="SHADE_CODE" />                                                    
                                                    <asp:BoundField HeaderText="Lot&nbsp;No" DataField="LOT_NO" />
                                                    <asp:BoundField HeaderText="Batch&nbsp;No" DataField="DYED_BATCH" />
                                                    <asp:BoundField HeaderText="Grade" DataField="GRADE" />
                                                    <asp:BoundField HeaderText="Carton&nbsp;No" DataField="CARTON_NO" />
                                                    <asp:BoundField HeaderText="Issued&nbsp;Qty" DataField="ISSUE_QTY" />
                                                </Columns>
                                                <RowStyle CssClass="SmallFont" />
                                                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                            </asp:GridView>
                                        </asp:Panel>
                                        <cc1:HoverMenuExtender ID="hvrmnrecadj" runat="server" PopDelay="500" PopupControlID="pnlrecadj"
                                            TargetControlID="txtQTY" PopupPosition="Left">
                                        </cc1:HoverMenuExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Gross Wt">
                                    <ItemTemplate>
                                        <asp:Label ID="txtGrossWt" runat="server" CssClass="Label SmallFont" Text='<%# Bind("GROSS_WT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Tare Wt">
                                    <ItemTemplate>
                                        <asp:Label ID="txtTareWt" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TARE_WT") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="Cartons">
                                    <ItemTemplate>
                                        <asp:Label ID="txtCartons" runat="server" CssClass="Label SmallFont" Text='<%# Bind("CARTONS") %>'
                                            ReadOnly="True" Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Basic Rate" >
                                    <ItemTemplate>
                                        <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("BASIC_RATE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Final Rate" Visible="false" >
                                    <ItemTemplate>
                                        <asp:Label ID="txtfRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("FINAL_RATE") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount" >
                                    <ItemTemplate>
                                        <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                            Text='<%# Bind("AMOUNT") %>' Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                            Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                            CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                                        /
                                        <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                            CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle CssClass="SmallFont" />
                            <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                        </asp:GridView>
                        <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_TYPE" runat="server" Visible="false"></asp:Label>
                       <asp:Label ID="lblPO_YEAR" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="lblPO_COMP" runat="server" Visible="false"></asp:Label>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <cc1:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate"
            Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender5" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtIssueDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        <asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
            MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
        <cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
        </cc1:ValidatorCalloutExtender>
        <asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
            Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
        <cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtDocDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
      <cc1:CalendarExtender ID="CE3" runat="server" TargetControlID="txtdateprp" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtender6" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtdateprp" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
           <cc1:CalendarExtender ID="CE4" runat="server" TargetControlID="txtdateremvl" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtender7" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtdateremvl" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="false" ValidationGroup="M1" />
            
        <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtLCDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtender4" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLCDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>  
          
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLRDate"
            PopupPosition="TopLeft" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtLRDate" PromptCharacter="_">
        </cc1:MaskedEditExtender>
        
    </ContentTemplate>
</asp:UpdatePanel>