<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YarnReturnAgstPO.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_ReturnAgstPO" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
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
        width: 150px;
    }
    .c6
    {
        margin-left: 4px;
        width: 80px;
    }
   
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
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
            <b class="titleheading">Yarn Return Against PO</b>
        </td>
    </tr>
    <tr>
        <td class="style3" width="100%">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>
                &nbsp;Mode </span>
        </td>
    </tr>
    
    <tr>
        <td width="100%" class="td">
            <table width="100%">
              <tr>
                    <td  class="tdRight" width="15%">
                        <asp:Label ID="Label15" runat="server" Text="Challan Number : " CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%" >
                        <asp:TextBox ID="txtChallanNumber" runat="server" ValidationGroup="M1" Width="150px"
                            TabIndex="1" CssClass="TextBoxNo TextBoxDisplay SmallFont" AutoPostBack="True"
                            OnTextChanged="txtChallanNumber_TextChanged"></asp:TextBox>
                        <cc2:ComboBox ID="ddlTRNNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="ddlTRNNumber_LoadingItems" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                            OnSelectedIndexChanged="ddlTRNNumber_SelectedIndexChanged"  Width="150px" Height="200px"
                            MenuWidth="300px">
                            <HeaderTemplate>
                                <div class="header c1">
                                    MRN #</div>
                                <div class="header c2">
                                    MRN Date</div>
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
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td  class="tdRight" width="15%">
                        <asp:Label ID="Label16" runat="server" Text="Issue Date : " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td  class="tdLeft" width="15%">
                        <asp:TextBox ID="txtIssueDate" runat="server" TabIndex="2" ValidationGroup="M1"  Width="150px"
                            CssClass="TextBox TextBoxDisplay SmallFont"></asp:TextBox>
                    </td>
                      <td  width="15%" class="tdRight" width="15%">
                        <asp:Label ID="Label2" runat="server" Text="Department: " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td  class="tdLeft" width="15%">
                        <cc3:OboutDropDownList ID="txtDepartment" MenuWidth="200px" CssClass="SmallFont"
                            AppendDataBoundItems="true"  Width="150px" runat="server" TabIndex="1">
                        </cc3:OboutDropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label1" runat="server" Text="LR No :" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%" colspan="1">
                       <asp:TextBox ID="txtVehicleNo" runat="server" TabIndex="2"  Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                 
                        
                    </td>
                   
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label17" runat="server" CssClass="Label SmallFont" Text="Issue Shift : "></asp:Label>
                    </td>
                    <td class="tdLeft" width="15%" colspan="1">
                        <cc3:OboutDropDownList ID="ddlIssueShift"   Width="150px" runat="server"
                            TabIndex="3">
                        </cc3:OboutDropDownList>
                      
                    </td>
                    <td class="tdRight" width="15%">
                    <asp:Label ID="Label3" runat="server" Text="Location: " CssClass="Label SmallFont"></asp:Label>
                    </td>
                     <td class="tdLeft" width="15%">
                      <asp:DropDownList ID="ddlLocation" runat="server" CssClass="SmallFont" Font-Size="9"
                Width="150px" TabIndex="4">
            </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label7" runat="server" Text="Document Number :" CssClass="LabelNo SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDocNo" runat="server" TabIndex="5"  Width="150px" CssClass="TextBoxNo SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label8" runat="server" Text="Doc. Date" CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="15%">
                        <asp:TextBox ID="txtDocDate" runat="server" TabIndex="6"  Width="150px" CssClass="TextBox SmallFont"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="15%">
                        <asp:Label ID="Label4" runat="server" Text="Store: " CssClass="Label SmallFont"></asp:Label>
                    </td>
                    <td align="left" valign="top" width="25%">
                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="SmallFont" Font-Size="9" TabIndex="7"
                Width="150px">
            </asp:DropDownList>
                         </td>
                   
                </tr>
                 <tr>
                            <td class="tdRight" width="15%">
                                <asp:Label ID="Label5" runat="server" Text="Party :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="15%">
                                <cc2:ComboBox ID="txtPartyCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    OnLoadingItems="txtPartyCode_LoadingItems" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EmptyText="Select Vendor" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="400px" Height="200px">
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
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td class="tdRight" width="15%">
                                <asp:TextBox ID="txtPartyCode1" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                            <td class="tdLeft" colspan="1" style="width: 15%">
                                <asp:TextBox ID="txtPartyAddress" TabIndex="4" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="150px"></asp:TextBox>
                            </td>
                             <td class="tdRight" width="15%">
                                <asp:Label ID="Label14" runat="server" Text="Remarks :" CssClass="Label SmallFont"></asp:Label>
                            </td>
                            <td class="tdLeft" width="25%">
                                <asp:TextBox ID="txtRemarks" runat="server" Width="150px" TabIndex="8" CssClass="TextBox SmallFont"></asp:TextBox>
                            </td>
                        </tr>
                <tr>
                    <td align="right" valign="top" width="15%">
                        
                    </td>
                    <td align="left" colspan="5" valign="top" width="85%">
                        
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <%--<tr>
        <td width="100%" class="td">
            <table width="98%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td>
                        Yarn Code
                    </td>
                    <td>
                        Description
                    </td>
                    <td>
                        UOM
                    </td>
                    <td>
                        Adj Rec
                    </td>
                    <td>
                        Qty
                    </td>
                    <td>
                        Rate
                    </td>
                    <td>
                        Amount
                    </td>
                    <td>
                        Cost Code
                    </td>
                    <td>
                        Mac Code
                    </td>
                    <td>
                        Remarks
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                     
                        
                        <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="100px"
                                    EnableVirtualScrolling="true" OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            PO Numb</div>
                                        <div class="header c2">
                                            Item Code</div>
                                        <div class="header c4">
                                            Description</div>
                                        <div class="header c2 rAlign">
                                            Bal Apr Qty</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("PO_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("YARN_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("YARN_DESC") %>' />
                                        </div>
                                        <div class="item c2 rAlign">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("REMQTY") %>' />
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
                    <td>
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="120px" TabIndex="10"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="40px" TabIndex="11"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                            Text="adj. Rec." Width="55px" TabIndex="12" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            Width="50px" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True" TabIndex="13"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px" TabIndex="13"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="50px" TabIndex="14"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostCode" runat="server" CssClass="TextBox SmallFont" Width="50px" TabIndex="15"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMacCode" runat="server" CssClass="TextBox SmallFont" Width="50px" TabIndex="16"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="120px" TabIndex="17"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click" TabIndex="18"
                            Text="Save" />
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click" TabIndex="19"
                            Text="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>--%>
                <tr>
                <td width="100%" class="td">
                    <table width="98%">
                        <tr bgcolor="#336699" class="SmallFont titleheading">
                            <td>
                                Yarn Code
                            </td>
                            <td>
                                Adj Recpt
                            </td>
                            <td>
                                Qty
                            </td>
                            <td>
                                Rate
                            </td>
                            <td>
                                Amount
                            </td>
                            <td>
                               <%-- Cost Code--%>
                            </td>
                            <td>
                               <%-- Mac Code--%>
                            </td>
                            <td>
                                Remarks
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <cc2:ComboBox ID="txtICODE" runat="server" CssClass="SmallFont" EmptyText="select..."
                                    AutoPostBack="True" EnableLoadOnDemand="true" MenuWidth="580px" Width="100px"
                                    EnableVirtualScrolling="true" OnLoadingItems="cmbPOITEM_LoadingItems" OnSelectedIndexChanged="cmbPOITEM_SelectedIndexChanged"
                                    Height="200px">
                                    <HeaderTemplate>
                                        <div class="header c2">
                                            PO Numb</div>
                                        <div class="header c2">
                                            Yarn Code</div>
                                        <div class="header c4">
                                            Description</div>
                                             <div class="header c4">
                                            Shade Detals</div>
                                        <div class="header c2 rAlign">
                                            Bal Apr Qty</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Literal4" Text='<%# Eval("PO_NUMB") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("YARN_CODE") %>' />
                                        </div>
                                        <div class="item c4">
                                            <asp:Literal runat="server" ID="Container3" Text='<%# Eval("YARN_DESC") %>' />
                                        </div>
                                         <div class="item c4">
                                             <asp:Literal runat="server" ID="Literal6" Text='<%# Eval("SHADE_FAMILY") %>' />
                                             <asp:Literal runat="server" ID="Literal7" Text='<%# Eval("SHADE") %>' />
                                        </div>
                                        <div class="item c2 rAlign">
                                            <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("REMQTY") %>' />
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
                            <td>
                                <asp:Button ID="btnAdjRec" runat="server" CssClass="SmallFont" OnClick="btnAdjRec_Click"
                                    Text="Adj.Recpt." Width="65px" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="77px" OnTextChanged="txtQTY_TextChanged1" ReadOnly="True"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtBasicRate" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="75px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtAmount" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="73px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCostCode" Visible="false" CssClass="SmallFont" runat="server" AppendDataBoundItems="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <cc2:ComboBox ID="ddlMacCode" Visible="false" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    EmptyText="select..." EnableLoadOnDemand="true" EnableVirtualScrolling="true" TabIndex="11"
                                    Height="200px" MenuWidth="650px" OnLoadingItems="ddlMacCode_LoadingItems" OnSelectedIndexChanged="ddlMacCode_SelectedIndexChanged"
                                    Width="25%">
                                    <HeaderTemplate>
                                        <div class="header c3">
                                            Mac Code</div>
                                        <div class="header c2">
                                            Mac Group</div>
                                        <div class="header c3 ">
                                            Mac Segement</div>
                                        <div class="header c3">
                                            Mac Type</div>
                                        <div class="header c3 ">
                                            Mac Section</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c3">
                                            <asp:Literal ID="Container7" runat="server" Text='<%# Eval("MACHINE_CODE") %>' />
                                        </div>
                                        <div class="item c2">
                                            <asp:Literal ID="Container3" runat="server" Text='<%# Eval("MACHINE_GROUP") %>' />
                                        </div>
                                        <div class="item c3 ">
                                            <asp:Literal ID="Literal6" runat="server" Text='<%# Eval("MACHINE_SEGMENT") %>' />
                                        </div>
                                        <div class="item c3">
                                            <asp:Literal ID="Literal7" runat="server" Text='<%# Eval("MACHINE_TYPE") %>' />
                                        </div>
                                        <div class="item c3 ">
                                            <asp:Literal ID="Literal8" runat="server" Text='<%# Eval("MACHINE_SEC") %>' />
                                        </div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                                <asp:TextBox ID="txtMacCode" Width="50%" runat="server" CssClass="TextBox SmallFont" Visible="false"
                                    MaxLength="6" TabIndex="12">NA</asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="138px"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                                    Text="Save"  Width="60px" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <span lang="en-us">PO Numb:<asp:TextBox ID="txtPO_NUMB" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="true" Width="60px"></asp:TextBox>
                                    Yarn Code/ Desc:<asp:TextBox ID="txtItemCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="100px"></asp:TextBox>
                                    /<asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="150px"></asp:TextBox>
                                        &nbsp;
                                        Shade Details :
                                        <asp:TextBox ID="txtShadeFamily" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="100px"></asp:TextBox>/
                                        <asp:TextBox ID="txtShade" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="100px"></asp:TextBox>
                                    &nbsp; UOM:
                                    <asp:TextBox ID="txtUNIT" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                        ReadOnly="true" Width="60px"></asp:TextBox>
                                        
                                        
                                </span>
                                <%--<asp:TextBox ID="txtQtyUnit" Visible="TRUE" runat="server" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="1px"></asp:TextBox>
                                <asp:TextBox ID="txtQtyWeight" runat="server" Visible="TRUE" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                    Width="1px"></asp:TextBox>--%>
                                    
                                    <asp:HiddenField  ID="txtQtyUnit" runat="server" />
                                    <asp:HiddenField  ID="txtQtyWeight" runat="server" />
                                    
                                <asp:Label ID="lblPO_BRANCH" runat="server" Visible="false" ></asp:Label>
                                 <asp:Label ID="lblPO_TYPE" runat="server" Visible="false" ></asp:Label>
                                <asp:Label ID="lblPO_COMP" runat="server" Visible="false" ></asp:Label>
                                <asp:Label ID="lblPO_Year" runat="server" Visible="False"></asp:Label>
                            </td>
                            <td>
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
                <asp:GridView ID="grdMaterialItemIssue" runat="server" AutoGenerateColumns="False"
                    CssClass="SmallFont" Width="99%" ShowFooter="false" OnRowCommand="grdMaterialItemIssue_RowCommand">
                    <Columns>
                    <asp:TemplateField HeaderText="PO Numb">
                                    <ItemTemplate>
                                        <asp:Label ID="txtPONUMB" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PO_NUMB") %>'
                                            ReadOnly="True" ></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                        <asp:TemplateField HeaderText="Yarn Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_CODE") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("YARN_DESC") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit">
                            <ItemTemplate>
                                <asp:Label ID="txtUNIT" runat="server" CssClass="Label SmallFont" Text='<%# Bind("UOM") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_QTY") %>'
                                    ReadOnly="True" ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rate">
                            <ItemTemplate>
                                <asp:Label ID="txtRate" runat="server" ReadOnly="True" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("BASIC_RATE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                                <asp:Label ID="txtAmount" runat="server" ReadOnly="true" CssClass="LabelNo SmallFont"
                                    Text='<%# Bind("AMOUNT") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtCostCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("COST_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mac Code" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="txtMacCode" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("MAC_CODE") %>' ></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("REMARKS") %>' ></asp:Label>
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
                
            </asp:Panel>
        </td>
    </tr>
</table>
<cc1:CalendarExtender ID="ceIssueDate" runat="server" TargetControlID="txtIssueDate">
</cc1:CalendarExtender>
<asp:RangeValidator ID="rv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="Only numeric value allowed" MaximumValue="1000000"
    MinimumValue="1" Type="Integer" ValidationGroup="M1"></asp:RangeValidator>
<cc1:ValidatorCalloutExtender ID="vcrv1" runat="server" TargetControlID="rv1">
</cc1:ValidatorCalloutExtender>
<asp:RequiredFieldValidator ID="rfv1" runat="server" ControlToValidate="txtChallanNumber"
    Display="None" ErrorMessage="MRN number required" ValidationGroup="M1"></asp:RequiredFieldValidator>
<cc1:CalendarExtender ID="ceDoc" runat="server" TargetControlID="txtDocDate">
</cc1:CalendarExtender>
<%-- </ContentTemplate>
</asp:UpdatePanel>--%>