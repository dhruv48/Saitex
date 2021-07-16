<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrderMachinePlanning.ascx.cs"
    Inherits="Module_PlanningAndScheduling_Controls_OrderMachinePlanning" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<link href="../../../StyleSheet/CommonStyle.css" rel="stylesheet" type="text/css" />
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
        width: 150px;
    }
    .c2
    {
        margin-left: 2px;
        width: 150px;
    }
    .c3
    {
        margin-left: 2px;
        width: 200px;
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

<script type="text/javascript">
    function SetWriteAble() {
        document.getElementById('ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty').readOnly = false;
    }

    function CompareQty() {
        var ord1 = document.getElementById('ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty');
        var ord2 = document.getElementById('ctl00_cphBody_OMP_grdOrderDetails_ctl02_lblordqty');
        var plannedQty = parseInt(ord1.value);
        var ordQty = parseInt(ord2.value);


        if (isNaN(plannedQty)) {
            var promptQty = prompt("Please enter Planned Qantity");
            var perQty = parseInt(promptQty);
            if (perQty != null && perQty != "" && perQty != 0) {
                if (perQty > ordQty) {
                    var r = confirm("Planned Qty is greater then Order Qty Do you want same as order quantity.\nPress a button");
                    if (r == true) {
                        var q = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty");
                        q.innerHTML = ordQty;
                        q.value = ordQty;
                        CalculateRemainingQty(q.value, ordQty)

                    }
                    else {

                        var q = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty");
                        q.innerHTML = 0;
                        q.value = 0;
                        CalculateRemainingQty(q.value, ordQty)

                    }
                }
                else {
                    var q = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty");
                    q.innerHTML = perQty;
                    q.value = perQty;
                    CalculateRemainingQty(q.value, ordQty)
                }

            }
            else {
                var q = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty");
                q.innerHTML = 0;
                q.value = 0;
                CalculateRemainingQty(q.value, ordQty)
            }
        }
        if (plannedQty > ordQty) {

            var r = confirm("Planned Qty is greater then Order Qty Do you want same as order quantity.\nPress a button");
            if (r == true) {
                var q = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty");
                q.innerHTML = ordQty;
                q.value = ordQty;
                CalculateRemainingQty(q.value, ordQty)

            }
            else {
                var q = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtPlannedQty");
                q.innerHTML = 0;
                q.value = 0;
                CalculateRemainingQty(q.value, ordQty)
            }

        }
        else {
            alert(plannedQty);
            alert(ordQty);
            ord1.innerHTML = plannedQty;
            ord1.value = plannedQty;
            CalculateRemainingQty(plannedQty, ordQty)
        }
    }


    function CalculateRemainingQty(ord1, ord2) {
        var remainingQtyControl = document.getElementById("ctl00_cphBody_OMP_grdOrderDetails_ctl02_txtBalQty");
        var plannedQty = parseInt(ord1);

        var ordQty = parseInt(ord2);

        var remQty = ordQty - plannedQty;

        remainingQtyControl.innerHTML = remQty;
        remainingQtyControl.value = remQty;


    }

    function divexpandcollapse(divname) {
        var div = document.getElementById(divname);
        var img = document.getElementById('img' + divname);

        if (div.style.display == "none") {
            div.style.display = "inline";
            img.src = "../../../APP_IMAGES/minus.gif";
        } else {
            div.style.display = "none";
            img.src = "../../../APP_IMAGES/plus.gif";

        }
    }

   
    
    
</script>

<%--
<asp:UpdatePanel ID="upPanel" runat="server">
<ContentTemplate>
--%>
<table class="td tContentArial" width="100%">
    <tr>
        <td>
            <table>
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" ValidationGroup="CR" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="td1" runat="server">
                        <asp:ImageButton ID="imgPlanned" runat="server" ImageUrl="~/CommonImages/planned.jpg"
                            ToolTip="Planned Order" OnClick="imgPlanned_Click" />
                    </td>
                    <td id="td2" runat="server">
                        <asp:ImageButton ID="imgUnplanned" runat="server" ImageUrl="~/CommonImages/unplanned.jpg"
                            ToolTip="Unplanned Order" OnClick="imgUnplanned_Click" />
                    </td>
                    <td id="td3" runat="server">
                        <asp:ImageButton ID="imgRemaining" runat="server" ImageUrl="~/CommonImages/remain.jpg"
                            ToolTip="Remaining Order" OnClick="imgRemaining_Click" />
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" OnClick="imgbtnClear_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" OnClick="imgbtnExit_Click" />
                    </td>
                    <td style="font-style: italic">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" OnClick="imgbtnHelp_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <%--<asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                                    ToolTip="Update" ValidationGroup="CR" onclick="imgbtnUpdate_Click" />--%>
                    </td>
                    <%--<td id="tdDelete" runat="server">
                                <asp:ImageButton ID="imgbtnList" runat="server" Height="41" ImageUrl="~/CommonImages/del6-.png"                                   
                                    ToolTip="List" ValidationGroup="M1" Width="48" 
                                    onclick="imgbtnDelete_Click" />
                            </td>--%>
                    <td id="tdFind" runat="server">
                        <%--<asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                                     ToolTip="Find" onclick="imgbtnFind_Click" />--%>
                    </td>
                    <td id="tdPrint" runat="server">
                        <%-- <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                                     ToolTip="Print" onclick="imgbtnPrint_Click" />--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader" colspan="3">
            <table width="100%">
                <tr>
                    <td align="center" style="background-color: #336799; color: white;">
                        <b class="titleheading">Order&nbsp;Scheduling&nbsp;For&nbsp;<asp:Label ID="lblorderplanning"
                            runat="server" Text="Dyeing"></asp:Label></b>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="left">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server" Text="Save"></asp:Label>&nbsp;Mode</span>
        </td>
    </tr>
    <tr>
        <td align="center">
            <asp:Label ID="LblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="LblError" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%" class="td">
                <tr>
                    <td class="td" width="25%" align="center">
                        Order&nbsp;Number&nbsp;:
                    </td>
                    <td class="td" width="35%" align="center">
                        Party&nbsp;Name&nbsp;:
                    </td>
                    <td width="20%" class="td" align="center">
                        Order&nbsp;Date&nbsp;:
                    </td>
                    <td width="20%" class="td" align="center">
                        Delevery&nbsp;Date&nbsp;:
                    </td>
                </tr>
                <tr>
                    <td class="tdCenter" width="15%" align="center">
                        <cc2:ComboBox ID="cmbOrderNo" runat="server" AutoPostBack="True" DataTextField="ORDER_NO"
                            DataValueField="ORDER_NO" EmptyText="Select&nbsp;Order&nbsp;No" EnableLoadOnDemand="true"
                            Height="200px" MenuWidth="800px" TabIndex="1" Width="100%" EnableVirtualScrolling="true"
                            OnLoadingItems="cmbOrderNo_LoadingItems" OnSelectedIndexChanged="cmbOrderNo_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Order&nbsp;No.</div>
                                <div class="header c2">
                                    Party&nbsp;Name</div>
                                <div class="header c3">
                                    Order&nbsp;Date</div>
                                     <div class="header c3">
                                    Article Desc</div>
                                    
                                    
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal ID="Container1" runat="server" Text='<%# Eval("ORDER_NO ") %>' />
                                </div>
                                <div class="item c2">
                                    <asp:Literal ID="Container2" runat="server" Text='<%# Eval("PRTY_NAME") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Container3" runat="server" Text='<%# Eval("ORDER_DATE", "{0:dd/MM/yyyy}") %>' />
                                </div>
                                <div class="item c3">
                                    <asp:Literal ID="Literal1" runat="server" Text='<%# Eval("ARTICAL_DESC") %>' />
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
                    <td class="tdLeft" valign="top">
                        <%--<asp:TextBox ID="txtPartyCode" runat="server"  Width="100%" CssClass="SmallFont TextBox TextBoxDisplay"  ReadOnly="true" ></asp:TextBox>--%>
                        <%--<asp:TextBox ID="txtPartyAddress" runat="server"  Width="100%" CssClass="SmallFont TextBox TextBoxDisplay"  ReadOnly="true" ></asp:TextBox>--%>
                        <asp:TextBox ID="txtPartyName" runat="server" Width="100%" CssClass="SmallFont TextBox TextBoxDisplay"
                            ReadOnly="true"></asp:TextBox>
                        <br />
                    </td>
                    <td class="tdLeft" valign="top">
                        <asp:TextBox ID="txtOrderDate" runat="server" ReadOnly="true" Width="100%" CssClass="SmallFont TextBox TextBoxDisplay"></asp:TextBox>
                        <br />
                    </td>
                    <td class="tdLeft" valign="top">
                        <asp:TextBox ID="txtDeleveryDate" runat="server" ReadOnly="true" Width="100%" CssClass="SmallFont TextBox TextBoxDisplay"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr id="trOrderDetails" runat="server">
                    <td align="center" class="TableHeader" colspan="4">
                        <b class="titleheading">Order&nbsp;No&nbsp;:-&nbsp;<asp:Label ID="lblorderno" runat="server"
                            Text=""></asp:Label>&nbsp;Details</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="grdOrderDetails" runat="server" AllowPaging="True" AllowSorting="True"
                            AutoGenerateColumns="False" CellPadding="4" Font-Size="10px" ForeColor="#333333"
                            GridLines="None" Width="100%">
                            <FooterStyle BackColor="#CCCCCC" />
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:TemplateField HeaderText="PA NO" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpino" runat="server" Text='<%# Eval("PI_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORDER PROCESS ROOT" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprderpro" runat="server" Text='<%# Eval("PROS_ROUTE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ARTICAL CODE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblarticlecode" runat="server" Text='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ARTICAL DESCRIPTION" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblarticleDesc" runat="server" Text='<%# Eval("ARTICAL_DESC") %>'
                                            ToolTip='<%# Eval("ARTICAL_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SHADE CODE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_CODE" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SHADE NAME" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSHADE_NAME" runat="server" Text='<%# Eval("SHADE_NAME") %>' ToolTip='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORD QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblordqty" runat="server" Text='<%# Bind("ORD_QTY") %>' ReadOnly="true"
                                            Width="75px"></asp:TextBox>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BALANCE QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBalQty" runat="server" Text='<%# Bind("REMAINING_QTY")%>' Width="75px"
                                            MaxLength="6" ReadOnly="true"></asp:TextBox>
                                        <cc3:FilteredTextBoxExtender ID="FilterBalQty" runat="server" TargetControlID="txtBalQty"
                                            FilterType="Custom, Numbers" ValidChars="." />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PLANNED QTY" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlannedQty" runat="server" Text='<%# Bind("PLANNED_QTY") %>'
                                            Width="75px" OnTextChanged="txtPlannedQty_TextChanged" AutoPostBack="true" MaxLength="6"></asp:TextBox>
                                        <%--readonly="true"  ondblclick="SetWriteAble()"   onchange="CompareQty()"--%>
                                        <cc3:FilteredTextBoxExtender ID="FiltertxtRate" runat="server" TargetControlID="txtPlannedQty"
                                            FilterType="Custom, Numbers" ValidChars="." />
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="REMARKS" ItemStyle-HorizontalAlign="Left" ItemStyle-Width="20px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblremarks" runat="server" Text='<%# Eval("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="20px" />
                                    <ItemStyle HorizontalAlign="Left" Width="20px"></ItemStyle>
                                </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="ORDER NO" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorderno" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORDER DATE" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblorderdt" runat="server" Text='<%# Eval("ORDER_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ORDER DELEVERY DATE" ItemStyle-HorizontalAlign="Left"
                                    Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDELDT" runat="server" Text='<%# Eval("DEL_DATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRTY NAME" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprtyname" runat="server" Text='<%# Eval("PRTY_CODE") %>' ToolTip='<%# Eval("PRTY_NAME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Year" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>' ></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Width="120px" />
                                </asp:TemplateField>
                            </Columns>
                            <%--      <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />--%>
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr id="trProcessRootDetails" runat="server">
                    <td align="center" class="TableHeader" colspan="4">
                        <b class="titleheading">Process&nbsp;Root&nbsp;:-&nbsp;<asp:Label ID="lblprocessroot"
                            runat="server" Text=""></asp:Label>&nbsp;Details&nbsp;For&nbsp;Order&nbsp;No.&nbsp;:-&nbsp;<asp:Label
                                ID="lblorderno1" runat="server" Text=""></asp:Label></b>
                    </td>
                </tr>
                <tr>
                    <td colspan="5" width="100%">
                        <asp:GridView ID="gridProcessRoot" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            Width="100%" OnRowCommand="gridProcessRoot_RowCommand" DataKeyNames="MAC_CODE"
                            OnRowDataBound="gridProcessRoot_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                        <a href="JavaScript:divexpandcollapse('div<%# Eval("MAC_CODE") %>');">
                                            <img id="imgdiv<%# Eval("MAC_CODE") %>" width="9px" border="0" src="../../../APP_IMAGES/plus.gif" /></a>
                                        <asp:Label ID="txtMachineCode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MAC_CODE") %>'
                                            Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="MAC_CODE" HeaderText="MACHINE GROUP" HeaderStyle-HorizontalAlign="Left">
                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Process Code">
                                    <ItemTemplate>
                                        <asp:Label ID="txcProcessCode" runat="server" CssClass="Label smallfont" TabIndex="19"
                                            Text='<%# Bind("PROS_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Serial Number">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="txtSNO" runat="server" CssClass="LabelNo smallfont" TabIndex="21"
                                            Text='<%# Bind("S_NO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Description">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <ItemTemplate>
                                        <asp:Label ID="txtProcessDescription" runat="server" CssClass="LabelNo smallfont"
                                            TabIndex="21" Text='<%# Bind("PROS_DESC") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Test Code">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="txtTestcode" runat="server" CssClass="LabelNo smallfont" TabIndex="21"
                                            Text='<%# Bind("Test_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <asp:Label ID="txtRemakrs" runat="server" CssClass="LabelNo smallfont" TabIndex="21"
                                            Text='<%# Bind("Remarks") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField ItemStyle-HorizontalAlign="Center"  ItemStyle-VerticalAlign="Middle" >
                                        <ItemTemplate>                                           
                                            <asp:LinkButton ID="linkbom" runat="server" CssClass="Label SmallFont"
                                                Text="View Machine Schedule" ToolTip="View Machine Schedule" CommandName="MachinSchedule" CommandArgument='<%# Eval("MAC_CODE") %>'>
                                            </asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <tr>
                                            <td colspan="100%">
                                                <div id="div<%# Eval("MAC_CODE") %>" style="display: none; position: relative; left: 15px;
                                                    overflow: auto" width="100%">
                                                    <asp:GridView ID="gvChildGrid" runat="server" AutoGenerateColumns="false" BorderStyle="Double"
                                                        BorderColor="#df5015" GridLines="None" Width="95%" DataKeyNames="MACHINE_CODE"
                                                        OnRowCommand="gvChildGrid_RowCommand">
                                                        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                                        <RowStyle BackColor="#E1E1E1" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                        <HeaderStyle BackColor="#df5015" Font-Bold="true" ForeColor="White" />
                                                        <Columns>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%">
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkMachine" runat="server" />
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="MACHINE_CODE" HeaderText="MACHINE CODE" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            <asp:BoundField DataField="MACHINE_CAPACITY" HeaderText="MACHINE CAPACITY" HeaderStyle-HorizontalAlign="Left">
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:BoundField>
                                                            
                                                      <%--<asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="3%" HeaderText="DIA">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblDia" runat="server" Text='<%# Bind("DIA") %>' Width="50px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="3%" />
                                                            </asp:TemplateField>                    
                                                            
                                                      <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" HeaderText="GAUGE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="lblGauge" runat="server" Text='<%# Bind("GAUGE") %>' Width="50px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            --%>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Left" ItemStyle-Width="5%" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblMachineCode" runat="server" Text='<%# Bind("MACHINE_CODE") %>'></asp:Label>
                                                                    <asp:Label ID="lblMachineCapacity" runat="server" Text='<%# Bind("MACHINE_CAPACITY") %>'></asp:Label>
                                                                    <asp:Label ID="lblMachineGroup" runat="server" Text='<%# Bind("MACHINE_GROUP") %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="5%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="START DATE" ItemStyle-HorizontalAlign="Left" Visible="true"
                                                                ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtFrom" runat="server" Width="50%" Text='<%# Bind("SCHEDULED_DATE_FROM") %>'></asp:TextBox>
                                                                    <cc3:CalendarExtender ID="txtFromCalender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtFrom">
                                                                    </cc3:CalendarExtender>
                                                                    <cc3:MaskedEditExtender ID="txtFromMask" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                        PromptCharacter="_" TargetControlID="txtFrom">
                                                                    </cc3:MaskedEditExtender>
                                                                    <cc1:TimeSelector ID="startTime" runat="server" SelectedTimeFormat="TwentyFour" AllowSecondEditing="true"
                                                                        Width="40%" Hour='<%# Bind("START_HOUR") %>' Minute='<%# Bind("START_MINUT") %>' >
                                                                    </cc1:TimeSelector>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="END DATE" ItemStyle-HorizontalAlign="Left" Visible="true"
                                                                ItemStyle-Width="30%">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtTo" runat="server" Width="50%" Text='<%# Bind("SCHEDULED_DATE_TO") %>'></asp:TextBox>
                                                                    <cc3:CalendarExtender ID="txtToCalender" Format="dd/MM/yyyy" runat="server" TargetControlID="txtTo">
                                                                    </cc3:CalendarExtender>
                                                                    <cc3:MaskedEditExtender ID="txtToMask" runat="server" Mask="99/99/9999" MaskType="Date"
                                                                        PromptCharacter="_" TargetControlID="txtTo">
                                                                    </cc3:MaskedEditExtender>
                                                                    <cc1:TimeSelector ID="endTime" runat="server" AllowSecondEditing="true" SelectedTimeFormat="TwentyFour"
                                                                        Width="40%" Hour='<%# Bind("END_HOUR") %>' Minute='<%# Bind("END_MINUT") %>'>
                                                                    </cc1:TimeSelector>
                                                                    <asp:RequiredFieldValidator ID="rfvod" runat="server" ControlToValidate="txtTo" Display="Dynamic"
                                                                        ErrorMessage="*" Font-Bold="False"></asp:RequiredFieldValidator>
                                                                </ItemTemplate>
                                                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="linkbom" runat="server" CssClass="Label SmallFont" Text="View&nbsp;Schedule"
                                                                        ToolTip="View Machine Schedule" CommandName="MachinSchedule" CommandArgument='<%# Eval("MACHINE_CODE") %>'>
                                                                    </asp:LinkButton>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="true" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Left" />
                            <HeaderStyle BackColor="#336699" CssClass="SmallFont titleheading" Font-Bold="True"
                                ForeColor="White" HorizontalAlign="Center" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <%--<asp:TextBox ID="date" runat="server" ReadOnly="true"  AutoPostBack="true" ></asp:TextBox><img src="../../../calender.png" />
<asp:Label ID="lbldate" runat="server" Text=""></asp:Label>
<asp:TextBox ID="date1" runat="server" ReadOnly="true" AutoPostBack="true" ></asp:TextBox><img src="../../../calender.png" />
<asp:Label ID="lbldate1" runat="server" Text=""></asp:Label>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
<%--
</ContentTemplate>
</asp:UpdatePanel>--%>