<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Work_order_entry.ascx.cs"
    Inherits="Module_WorkOrder_Controls_Work_order_entry" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
        
    function Calculation(val)
    {
        var name=val;
                   
        document.getElementById('ctl00_cphBody_POCredit1_txtAdvanceAmount').value=(parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtAdvance').value)*(parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtFinalTotal').value)/100)).toFixed(3) ;
     }           
    function SetFocus(ControlId)
    {   
        document.getElementById(ControlId).focus();       
    }
    function GetClick(ButtonId)
    {
        document.getElementById(ButtonId).click();
    }
   
</script>

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
        width: 150px;
    }
    .c4
    {
        margin-left: 4px;
        width: 200px;
    }
    .c5
    {
        margin-left: 4px;
        width: 500px;
    }
    .ralign
    {
        text-align: right;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
<table class="tContentArial SmallFont" width="800px">
    <tr>
        <td class="td tdLeft" width="100%">
            <table align="left">
                <tr>
                    <td>
                        <asp:ImageButton ID="imgbtnSave" OnClick="imgbtnSave_Click" runat="server" ToolTip="save"
                            ImageUrl="~/CommonImages/save.jpg" Width="48" Height="41" TabIndex="21" ValidationGroup="M1"
                            ></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ToolTip="Update"
                            ImageUrl="~/CommonImages/edit1.jpg" Width="48" Height="41" ValidationGroup="M1"
                            TabIndex="22"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnFind" OnClick="imgbtnFind_Click" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png" Width="48" Height="41" TabIndex="23">
                        </asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" OnClick="imgbtnPrint_Click" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" Width="48" Height="41" TabIndex="24">
                        </asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" OnClick="imgbtnClear_Click" runat="server" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" Width="48" Height="41" TabIndex="25"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" OnClick="imgbtnExit_Click" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" Width="48" Height="41" TabIndex="26">
                        </asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" OnClick="imgbtnHelp_Click" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" Width="48" Height="41" TabIndex="27 ">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center" width="100%">
            <span class="titleheading"><b>Job Work Order Entry</b></span>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
    <tr>
        <td width="100%" class="td SmallFont">
            <table width="100%">
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label2" runat="server" CssClass="LabelNo" Text="Work Order Type :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:DropDownList ID="ddlWOType" runat="server" Width="150px" CssClass="TextBox SmallFont"
                            Font-Bold="true" TabIndex="1">
                            <asp:ListItem Value="WUM">Main Order</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label29" runat="server" CssClass="LabelNo" Text="Product Type :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:DropDownList ID="ddlProductType" runat="server" CssClass="SmallFont TextBox"
                            Width="150px" TabIndex="2" AutoPostBack="True" Font-Bold="True" OnSelectedIndexChanged="ddlProductType_SelectedIndexChanged">
                             <asp:ListItem Value="YARN">YARN</asp:ListItem>
                             <asp:ListItem Value="SEWING THREAD">SEWING THREAD</asp:ListItem>
                               <asp:ListItem Value="ITEM">ITEM</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:Label ID="Label30" runat="server" CssClass="LabelNo" Text="Job Work category"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="25%">
                        <asp:DropDownList ID="ddlJobWorkCat" runat="server" CssClass="SmallFont TextBox"
                            Width="150px" AppendDataBoundItems="true" Font-Bold="True" TabIndex="3">
                        <%--    <asp:ListItem Value="COPS">PAPER TUBE</asp:ListItem>
                              <asp:ListItem Value="CARTON">CARTON</asp:ListItem>--%>
                           
                            <asp:ListItem Value="TWISTING">TWISTING</asp:ListItem>
                             <asp:ListItem Value="TEXTURISING">TEXTURISING</asp:ListItem>  
                              <asp:ListItem Value="YARN DYEING">YARN DYEING</asp:ListItem>    
                                <asp:ListItem Value="FINISHING">FINISHING</asp:ListItem>   
                             <asp:ListItem Value="%">GENERAL</asp:ListItem>
                             
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label3" runat="server" CssClass="LabelNo" Text="Work Order Numb :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:TextBox ID="txtWONumber" TabIndex="200" Font-Bold="true" runat="server" Width="150px"
                            CssClass="TextBoxNo SmallFont TextBoxDisplay" MaxLength="10" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvon" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Order number required" ControlToValidate="txtWONumber"></asp:RequiredFieldValidator>
                        <cc2:ComboBox ID="ddlWONumber" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="WO_NUMB" DataValueField="WO_NUMB" EmptyText="Find Work Order"
                            EnableLoadOnDemand="true" Height="200px" MenuWidth="450px" OnLoadingItems="ddlWONumber_LoadingItems"
                            EnableVirtualScrolling="true" OnSelectedIndexChanged="ddlWONumber_SelectedIndexChanged"
                            TabIndex="109" Width="150px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order Number</div>
                                <div class="header c2">
                                    Order Date</div>
                                <div class="header c4">
                                    Party Name</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container7" runat="server" Text='<%# Eval("WO_NUMB") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container8" runat="server" Text='<%# Eval("WO_DATE","{0:dd/MM/yyyy}") %>' />
                                </div>
                                <div class="item c4">
                                    <asp:Literal ID="Container9" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
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
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label4" runat="server" CssClass="LabelNo" Text="Work Order Date :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:TextBox ID="txtWODate" TabIndex="4" Font-Bold="true" runat="server" Width="150px"
                            CssClass="TextBox SmallFont" MaxLength="25"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvod" runat="server" ValidationGroup="M1" Font-Bold="False"
                            Display="Dynamic" ErrorMessage="*Order Date Required" ControlToValidate="txtWODate"></asp:RequiredFieldValidator>
                        <cc1:CalendarExtender ID="CE1" runat="server" TargetControlID="txtWODate" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                    </td>
                    <td valign="top" align="left" width="15%">
                        <asp:Label ID="Label8" runat="server" CssClass="LabelNo" Text="Delivery Branch :"></asp:Label>
                    </td>
                    <td valign="top" align="left" width="25%">
                        <asp:DropDownList ID="ddlDelAdd" runat="server" CssClass="SmallFont TextBox" Width="150px"
                            AppendDataBoundItems="true" Font-Bold="True" TabIndex="5">
                        </asp:DropDownList>
                    </td>
                
                </tr>
                
                
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label12" runat="server" CssClass="LabelNo" Text="Jober Party:"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
                        <cc2:ComboBox ID="ddlJoberParty" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlJoberParty_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="ddlJoberParty_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="600px" Height="200px" TabIndex="6">
                            <HeaderTemplate>
                                <div class="header c1">
                                   Jober Code</div>
                                <div class="header c5">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container10" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container11" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtJoberPartyCode" TabIndex="400" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="80px"></asp:TextBox>
                        <asp:TextBox ID="txtJoberPartyAddress" TabIndex="401" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="550px"></asp:TextBox>
                    </td>
                </tr>
                
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Job Party / Self Job:"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
                        <cc2:ComboBox ID="ddlPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                            EmptyText="Select Vendor" OnSelectedIndexChanged="ddlPartyCode_SelectedIndexChanged"
                            EnableVirtualScrolling="true" Width="150px" MenuWidth="600px" Height="200px" TabIndex="6">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c5">
                                    NAME</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container10" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                <div class="item c5">
                                    <asp:Literal runat="server" ID="Container11" Text='<%# Eval("PRTY_NAME") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                        <asp:TextBox ID="txtPartyCode" TabIndex="400" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="80px"></asp:TextBox>
                        <asp:TextBox ID="txtPartyAddress" TabIndex="401" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="550px"></asp:TextBox>
                    </td>
                </tr>
                
                
                
                
                <tr id="TRANSPOTER" runat="server" style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
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
                            ReadOnly="True" Width="80px" TabIndex="70"></asp:TextBox>
                        <asp:TextBox ID="txtTransporterAdd" TabIndex="75" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="True" Width="550px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label6" runat="server" CssClass="LabelNo" Text="Payment Term :"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
                        <asp:TextBox ID="txtPayTerm" TabIndex="8" runat="server" Width="97%" CssClass="SmallFont gCtrTxt"
                            MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label14" runat="server" CssClass="LabelNo" Text="Remarks :"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
                        <asp:TextBox ID="txtRemarks" TabIndex="9" runat="server" Width="97%" CssClass="SmallFont gCtrTxt"
                            MaxLength="200" TextMode="SingleLine"></asp:TextBox>
                    </td>
                </tr>
                <tr style="font-weight: bold">
                    <td valign="top" align="right" width="15%">
                        <asp:Label ID="Label16" runat="server" CssClass="LabelNo" Visible="false" Text="Instructions :"></asp:Label>
                    </td>
                    <td valign="top" align="left" colspan="5" width="85%">
                        <asp:TextBox ID="txtInstructions" runat="server" CssClass="SmallFont gCtrTxt" Visible="false" MaxLength="200"
                            TabIndex="10" TextMode="SingleLine" Width="97%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left" class="td SmallFont" valign="top" width="100%">
            <table width="100%" class="SmallFont">
                <tr bgcolor="#336699">
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label18" runat="server" CssClass="Label titleheading" Text="Quality Code"></asp:Label>
                    </td>
                    <td class="tdLefts SmallFont">
                        <asp:Label ID="Label7" runat="server" CssClass="Label titleheading" Text="Shade Code"></asp:Label>
                    </td>
                     <td class="tdLefts SmallFont">
                        <asp:Label ID="Label13" runat="server" CssClass="Label titleheading" Text="Lot No"></asp:Label>
                    </td>
                     <td class="tdLefts SmallFont">
                        <asp:Label ID="Label15" runat="server" CssClass="Label titleheading" Text="Grade"></asp:Label>
                    </td>
                    
                    <td class="tdLefts SmallFont">
                        <asp:Label ID="Label31" runat="server" CssClass="LabelNo titleheading" Text="UOM"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label20" runat="server" CssClass="LabelNo titleheading" Text="Quantity"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label11" runat="server" CssClass="LabelNo titleheading" Text="Unit"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label23" runat="server" CssClass="LabelNo titleheading" Text="Job Rate"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                        <asp:Label ID="Label24" runat="server" CssClass="LabelNo titleheading" Text="Dis./Tax"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label25" runat="server" CssClass="LabelNo titleheading" Text="Final Rate"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label10" runat="server" CssClass="LabelNo titleheading" Text="Supply Quality"></asp:Label>
                    </td>
                    <td class="tdRight SmallFont">
                        <asp:Label ID="Label28" runat="server" CssClass="LabelNo titleheading" Text="Shrinkage"></asp:Label>
                    </td>
                     <td class="tdRight SmallFont">
                        <asp:Label ID="Label9" runat="server" CssClass="LabelNo titleheading" Text="Remarks"></asp:Label>
                    </td>
                    <td class="tdLeft SmallFont">
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft">
                        <cc2:ComboBox ID="ddlArticleCode" runat="server" AutoPostBack="True" CssClass="SmallFont"
                            DataTextField="ARTICLE_CODE" DataValueField="Combined" EmptyText="Find Article"
                            EnableLoadOnDemand="true" EnableVirtualScrolling="true" Height="200px" MenuWidth="550px"
                            OnLoadingItems="ddlArticleCode_LoadingItems" OnSelectedIndexChanged="ddlArticleCode_SelectedIndexChanged"
                            TabIndex="11" Width="120px">
                            <HeaderTemplate>
                                <div class="header c1">
                                   Quality Code</div>
                                <div class="header c5">
                                   Quality Description</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container4" runat="server" Text='<%# Eval("ARTICLE_CODE") %>' />
                                </div>
                                <div class="item c5">
                                    <asp:Literal ID="Container5" runat="server" Text='<%# Eval("YARN_DESC") %>' />
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
                    <td class="tdLeft">
                        <asp:DropDownList ID="ddlshadeCode" runat="server" Width="100px" TabIndex="12" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlshadeCode_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    
                     <td>
                      <cc2:ComboBox ID="txtLotNo" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtLotNo_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Lot No" 
                            EnableVirtualScrolling="true" Width="100px" MenuWidth="300px" Height="200px"  TabIndex="28">
                            <HeaderTemplate>
                                <div class="header c2">
                                    Lot No</div>
                                <div class="header c2">
                                    Desc</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("MST_DESC") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    
                    <td>
                        <cc2:ComboBox ID="txtGrade" runat="server"  EnableLoadOnDemand="true"
                            OnLoadingItems="txtGrade_LoadingItems" DataTextField="MST_CODE" DataValueField="MST_CODE"
                            EmptyText="Grade" 
                            EnableVirtualScrolling="true" Width="60px" MenuWidth="100px" Height="200px"  TabIndex="29">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Grade</div>
                               
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("MST_CODE") %>' />
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
                    
                    <td class="tdLeft">
                        <asp:TextBox ID="txtUnit" TabIndex="220" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" ReadOnly="true" Width="40px" Text='<%# Bind("UOM") %>'></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:TextBox ID="txtWOQty" TabIndex="13" CssClass="TextBoxNo SmallFont" runat="server"
                            Width="80px" Text='<%# Bind("ORD_QTY") %>' ValidationGroup="trn"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNoOfUnite" runat="server" CssClass="TextBoxNo  SmallFont" Width="50px" TabIndex="14"></asp:TextBox>
                    </td>
                    
                    <td class="tdRight">
                        <asp:TextBox ID="txtBasicRate" TabIndex="15" runat="server" Width="80px" Text='<%# Bind("BASIC_RATE") %>'
                            ValidationGroup="trn" CssClass="TextBoxNo SmallFont" AutoPostBack="true"
                            ontextchanged="txtBasicRate_TextChanged"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnDiscountTaxes" TabIndex="16" runat="server" Font-Size="8pt" Text="Disc/Taxes"
                            Width="70px" OnClick="btnDiscountTaxes_Click" CssClass="SmallFont" />
                    </td>
                    <td class="tdRight">
                        <asp:TextBox ID="txtFinalRate" TabIndex="160" ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE") %>'
                            Width="80px" CssClass="TextBoxNo SmallFont TextBoxDisplay"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        <asp:Button ID="btnBOM" Text="Supply Quality" runat="server" OnClick="btnBOM_Click" TabIndex="17"
                            CssClass="SmallFont" Width="80px"></asp:Button>
                    </td>
                    <td class="tdRight">
                        <asp:TextBox ID="txtShrinkage" TabIndex="18" runat="server" Width="70px" Text='<%# Bind("DEL_DATE") %>'
                            CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                      <asp:TextBox ID="txtTRNRemarks" TabIndex="19" CssClass="TextBox SmallFont "
                            runat="server" Width="150px" MaxLength="200"></asp:TextBox>
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnSaveDetail" Text="Save" runat="server" OnClick="btnSaveDetail_Click"
                            ValidationGroup="trn" Width="60px" TabIndex="20" CssClass="SmallFont"></asp:Button>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="tdLeft" colspan="13">
                    Code/Dispaly&nbsp;Quality&nbsp;:<asp:TextBox ID="txtYarn_CodeParty"  ReadOnly="false"
                            CssClass="TextBox SmallFont " runat="server" Text='<%# Bind("ASS_YARN_CODE") %>'
                            Width="50px"></asp:TextBox>
                                                <asp:TextBox ID="txtParyItemDesc" runat="server" Width="200px" CssClass="TextBox  SmallFont"
                                                Font-Bold="False"  TabIndex="48" Text='<%# Bind("ASS_YARN_DESC") %>'></asp:TextBox>&nbsp;
                        Quality Code/description :<asp:TextBox ID="txtArticleCode" TabIndex="221" ReadOnly="true"
                            CssClass="TextBox SmallFont TextBoxDisplay" runat="server" Text='<%# Bind("ARTICLE_CODE") %>'
                            Width="50px"></asp:TextBox>
                        <asp:TextBox ID="txtArticleDescription" TabIndex="240" ReadOnly="true" CssClass="TextBox SmallFont TextBoxDisplay"
                            runat="server" Text='<%# Bind("YARN_DESC") %>' Width="350px"></asp:TextBox>
                      
                    </td>
                    <td class="tdLeft">
                        <asp:Button ID="btnCancelDetail" Text="Cancel" runat="server" OnClick="btnCancelDetail_Click"
                            Width="60px" TabIndex="20" CssClass="SmallFont"></asp:Button>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="trMaterialPOCreditTRN" runat="server">
        <td width="100%" class="td SmallFont">
            <asp:GridView ID="grdWOTRN" TabIndex="25" runat="server" OnRowCommand="grdWOTRN_RowCommand"
                CssClass="SmallFont" AutoGenerateColumns="False" AllowSorting="True" AllowPaging="false"
                OnRowDataBound="grdWOTRN_RowDataBound" width="100%">
                <RowStyle CssClass="SmallFont" Width="98%" />
                <Columns>
                <asp:TemplateField HeaderText="Party Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="txtPartyCode" TabIndex="17" Font-Bold="true" CssClass="Label SmallFont"
                                runat="server" Text='<%# Bind("ASS_YARN_CODE") %>' AutoCompleteType="Disabled"
                                ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quality Code">
                        <ItemTemplate>
                            <asp:Label ID="txtArticleCode" TabIndex="17" Font-Bold="true" CssClass="Label SmallFont"
                                runat="server" Text='<%# Bind("ARTICLE_CODE") %>' AutoCompleteType="Disabled"
                                ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Party Quality">
                        <ItemTemplate>
                            <asp:Label ID="txtPartyDescription" TabIndex="19" ReadOnly="true" runat="server" CssClass="Label SmallFont"
                                Text='<%# Bind("ASS_YARN_DESC") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" Width="12%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quality Description">
                        <ItemTemplate>
                            <asp:Label ID="txtItemDescription" TabIndex="19" ReadOnly="true" runat="server" CssClass="Label SmallFont"
                                Text='<%# Bind("ARTICLE_DESC") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" Width="12%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="shade Code">
                        <ItemTemplate>
                            <asp:Label ID="txtSHADE_CODE" TabIndex="19" ReadOnly="true" runat="server" CssClass="Label SmallFont"
                                Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" Width="7%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="Lot No">
                        <ItemTemplate>
                            <asp:Label ID="txtLotNo" TabIndex="19" ReadOnly="true" runat="server" CssClass="Label SmallFont"
                                Text='<%# Bind("LOT_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" Width="7%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="GRADE">
                        <ItemTemplate>
                            <asp:Label ID="txtGRADE" TabIndex="19" ReadOnly="true" runat="server" CssClass="Label SmallFont"
                                Text='<%# Bind("SHADE_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" Width="7%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Quantity">
                        <ItemTemplate>
                            <asp:Label ID="txtOrderQty" TabIndex="21" CssClass="LabelNo SmallFont" runat="server"
                                Text='<%# Bind("QTY") %>' Font-Bold="true" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="No Of Unit">
                        <ItemTemplate>
                            <asp:Label ID="txtNoOfUnit" TabIndex="21" CssClass="LabelNo SmallFont" runat="server"
                                Text='<%# Bind("NO_OF_UNIT") %>' Font-Bold="true" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>
                            <asp:Label ID="txtUnit" TabIndex="22" CssClass="Label SmallFont" runat="server" ReadOnly="true"
                                Text='<%# Bind("UOM") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="7%" Wrap="true" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Basic Rate">
                        <ItemTemplate>
                            <asp:Label ID="txtBaseRate" TabIndex="23" runat="server" Text='<%# Bind("BASIC_RATE","{0:0.0000}") %>'
                                CssClass="LabelNo SmallFont" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dis/ Taxes" >
                        <ItemTemplate  >
                            <asp:LinkButton ID="lbtnDistaxes" TabIndex="28" runat="server" Text="Dis/Taxes" CssClass="SmallFont"></asp:LinkButton>
                            <asp:Panel ID="pnlDisTaxes" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                              >
                                <asp:GridView ID="grdDisTaxes" runat="server" BorderColor="#C5E7F1" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Compo Sr No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCOMPO_SL" runat="server" CssClass="Label" Text='<%# Bind("COMPO_SL") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Component Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCOMPO_CODE" runat="server" CssClass="Label" Text='<%# Bind("COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Component Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCOMPO_TYPE" runat="server" CssClass="Label" Text='<%# Bind("COMPO_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Component Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBASE_COMPO_CODE" runat="server" CssClass="Label" Text='<%# Bind("BASE_COMPO_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRATE" runat="server" CssClass="Label" Text='<%# Bind("RATE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                                <asp:Button ID="btnCancelDisTaxes" runat="server" Text="Close This" CssClass="SmallFont" Width="100px"/>
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="mpedisTaxes" runat="server" PopupControlID="pnlDisTaxes"
                                TargetControlID="lbtnDistaxes" BackgroundCssClass="modalBackground" CancelControlID="btnCancelDisTaxes"
                                DropShadow="true">
                            </cc1:ModalPopupExtender>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Final Rate">
                        <ItemTemplate>
                            <asp:Label ID="txtFinalRate" TabIndex="25" ReadOnly="true" runat="server" Text='<%# Bind("FINAL_RATE","{0:0.0000}") %>'
                                Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount">
                        <ItemTemplate>
                            <asp:Label ID="txtAmount" TabIndex="26" ReadOnly="true" runat="server" Text='<%# Bind("AMOUNT","{0:0.0000}") %>'
                                Font-Bold="true" CssClass="LabelNo SmallFont"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Supply Quality">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnIssueBOM" TabIndex="28" runat="server" Text="Supply Quality" CssClass="SmallFont"></asp:LinkButton>
                            <asp:Panel ID="pnlIssueBOM" runat="server" BackColor="#C5E7F1" BorderColor="BlueViolet"
                                 >
                                <asp:GridView ID="grdIssueBOM" runat="server" BorderColor="#C5E7F1" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Base Article Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBASE_ARTICLE_TYPE" runat="server" CssClass="Label" Text='<%# Bind("BASE_ARTICLE_TYPE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Article Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBASE_ARTICLE_CODE" runat="server" CssClass="Label" Text='<%# Bind("BASE_ARTICLE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Base Article Desc">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBASE_ARTICLE_DESC" runat="server" CssClass="Label" Text='<%# Bind("BASE_ARTICLE_DESC") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Base Shade Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBASE_SHADE_CODE" runat="server" CssClass="Label" Text='<%# Bind("BASE_SHADE_CODE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUOM" runat="server" CssClass="Label" Text='<%# Bind("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQTY" runat="server" CssClass="Label" Text='<%# Bind("QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="shrinkage">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSHRINKAGE" runat="server" CssClass="Label" Text='<%# Bind("SHRINKAGE") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" />
                                            <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                </asp:GridView>
                                <asp:Button ID="btnCancelIssueBOM" runat="server" Text="Close This" CssClass="SmallFont" Width="100px" />
                            </asp:Panel>
                            <cc1:ModalPopupExtender ID="mpeIssueBOM" runat="server" PopupControlID="pnlIssueBOM"
                                TargetControlID="lbtnIssueBOM" BackgroundCssClass="modalBackground" CancelControlID="btnCancelIssueBOM"
                                DropShadow="true">
                            </cc1:ModalPopupExtender>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="7%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Shrinkage">
                        <ItemTemplate>
                            <asp:Label ID="txtShrinkage" TabIndex="28" runat="server" Text='<%# Bind("SHRINKAGE") %>'
                                CssClass="LabelNo SmallFont" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Right" Width="8%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Right" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="txtTrnRemarks" TabIndex="28" runat="server" Text='<%# Bind("REMARKS") %>'
                                CssClass="Label SmallFont" ReadOnly="true"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" Wrap="true" HorizontalAlign="Left" Width="12%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" TabIndex="29" runat="server" Text="Edit" CommandName="WOTrnEdit"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                            /
                            <asp:LinkButton ID="lnkDelete" TabIndex="29" runat="server" Text="Del" CommandName="WOTrnDelete"
                                CommandArgument='<%# Eval("UniqueId") %>'></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle VerticalAlign="Top" HorizontalAlign="Left" Width="7%" />
                        <HeaderStyle VerticalAlign="Top" HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
                <RowStyle CssClass="SmallFont" />
                <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
            </asp:GridView>
        </td>
    </tr>
</table>
</ContentTemplate></asp:UpdatePanel>
