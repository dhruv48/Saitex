<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Lot_MovementYS.ascx.cs" 
Inherits="Module_OrderDevelopment_Controls_Lot_MovementYS" %>
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
        width: 120px;
    }
    .c2
    {
        margin-left: 4px;
        width: 80px;
    }
    .c3
    {
       
        width: 300px;
    }
    .FromCols
    {
        background-color: #E6E6E6;
    }
    .ToCols
    {
        background-color: #D9E5EA;
    }
</style>

<script type="text/javascript" language="javascript">
    function Calculation(val) {
        var Res;
        var FromLotQty = parseFloat(document.getElementById('ctl00_cphBody_Lot_MovementYS1_txtFromLotQty').value);
        Res = Math.round(parseFloat(document.getElementById('ctl00_cphBody_Lot_MovementYS1_TxtTONOU').value) * parseFloat(document.getElementById('ctl00_cphBody_Lot_MovementYS1_TxtToWOU').value)).toFixed(2);
        if (isNaN(Res)) {
            document.getElementById('ctl00_cphBody_Lot_MovementYS1_txtToLotQty').value = '';
        }
        else {
            if (FromLotQty >= Res) {
                document.getElementById('ctl00_cphBody_Lot_MovementYS1_txtToLotQty').value = Res;
            }
            else {
                alert("ToLotQty is greater then FromLotQty!");
                document.getElementById('ctl00_cphBody_Lot_MovementYS1_txtToLotQty').value = '';
                document.getElementById('ctl00_cphBody_Lot_MovementYS1_TxtToWOU').value = '';
            }
        }
    }   
</script>

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
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ToolTip="Help" ImageUrl="~/CommonImages/link_help.png">
                        </asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Lot Movement Form</b>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft" width="100%">
            <asp:ValidationSummary ID="VsMain" runat="server" ShowMessageBox="True" ValidationGroup="M1"
                ShowSummary="False" />
        </td>
    </tr>
    <tr>
        <td class="td tdLeft" width="100%">
            <span class="Mode">You are in
                <asp:Label ID="lblMode" runat="server"></asp:Label>Mode </span>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="25%">
                        Entry #
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtEntryNo" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="25%">
                        Entry Date
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtEntryDate" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="25%">
                        Lot Id No
                    </td>
                    <td class="tdLeft" width="25%">
                        <cc2:ComboBox ID="ddlLotIdNo" runat="server" EmptyText="select..." Height="200px"
                            AutoPostBack="True" DataTextField="LOT_NUMBER" DataValueField="LOT_NUMBER" EnableLoadOnDemand="true"
                            MenuWidth="850px" Width="57%" OnLoadingItems="ddlLotIdNo_LoadingItems" OnSelectedIndexChanged="ddlLotIdNo_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Lot #</div>
                                <div class="header c1">
                                    Department</div>
                                <div class="header c1">
                                    Order #</div>
                                <div class="header c3">
                                    Party</div>
                                <div class="header c1">
                                    Pr.Code</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("LOT_NUMBER") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("DEPT_CODE") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal3" Text='<%# Eval("ORDER_NO") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("PARTY") %>' />
                                </div>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("PROS_CODE") %>' />
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
                    <td class="tdRight" width="25%">
                        Order #
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtOrderNo" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="25%">
                        From Process
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtFrPros" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight" width="25%">
                        Order Article
                    </td>
                    <td class="tdLeft" width="25%">
                        <asp:TextBox ID="txtOrdArticle" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="25%">
                        Party Details
                    </td>
                    <td class="tdLeft" colspan="3" width="25%">
                        <asp:TextBox ID="txtPartyCode" Width="50px" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" runat="server"></asp:TextBox>
                        <asp:TextBox ID="txtPartyAddress" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server" Width="77%"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdLeft" style="font-size: 16pt; background-color: #336799; color: #ffffff;"
                        colspan="2" width="25%">
                        From Detail
                    </td>
                    <td class="tdLeft" style="font-size: 16pt; background-color: #336799; color: #ffffff;"
                        colspan="2" width="25%">
                        To Detail
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        Lot Qty.
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="txtFromLotQty" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        Lot Qty.
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:TextBox ID="txtToLotQty" CssClass="TextBoxNo TextBoxDisplay SmallFont" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        From Department
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="txtFromDepartment" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                        <asp:Label ID="LblFromDept" runat="server" Visible="false" Text=""></asp:Label>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        To Department
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:DropDownList ID="ddlToDepartment" runat="server" Width="132px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        From Batch No
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="txtFromBatchNo" CssClass="TextBox TextBoxDisplay SmallFont" MaxLength="15"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        To Batch No
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:TextBox ID="txtToBatchNo" CssClass="TextBox SmallFont" MaxLength="15" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        From Location
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="txtFromLocation" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        To Location
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:TextBox ID="txtToLocation" CssClass="TextBox SmallFont" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        From No Of Unit
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="TxtFromNOU" CssClass="TextBoxNo TextBoxDisplay SmallFont" onKeyPress="pricevalidate(this);"
                            ReadOnly="true" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        To No Of Unit
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:TextBox ID="TxtTONOU" CssClass="TextBoxNo SmallFont" onKeyPress="pricevalidate(this);"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        From UOM
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="TxtFromUOM" CssClass="TextBox TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        To UOM
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:DropDownList ID="DDLUOM" runat="server" Width="132px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight FromCols" width="25%">
                        From Weight Of Unit
                    </td>
                    <td class="tdLeft FromCols" width="25%">
                        <asp:TextBox ID="TxtFromWOU" CssClass="TextBoxNo TextBoxDisplay SmallFont" ReadOnly="true"
                            runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight ToCols" width="25%">
                        To Weight Of Unit
                    </td>
                    <td class="tdLeft ToCols" width="25%">
                        <asp:TextBox ID="TxtToWOU" CssClass="TextBoxNo SmallFont" onkeyup="javascript:Calculation(this.id)"
                            onKeyPress="pricevalidate(this);" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%">
                <tr>
                    <td class="tdRight" width="25%">
                        Remarks
                    </td>
                    <td class="tdLeft" width="75%">
                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="85%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight" width="25%">
                        Checked By
                    </td>
                    <td class="tdLeft" width="75%">
                        <asp:TextBox ID="txtCheckBy" runat="server" CssClass="TextBox SmallFont"></asp:TextBox>
                        <asp:Label ID="lblProsCode" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
