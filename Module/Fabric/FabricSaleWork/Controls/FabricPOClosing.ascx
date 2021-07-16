<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FabricPOClosing.ascx.cs" Inherits="Module_Inventory_Controls_FabricPOClosing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc3" %>
<style type="text/css">
    .tdText
    {
        font: 11px Verdana;
        color: #333333;
    }
    .option2
    {
        font: 11px Verdana;
        color: #0033cc;
        padding-left: 4px;
        padding-right: 4px;
    }
    a
    {
        font: 11px Verdana;
        color: #315686;
        text-decoration: underline;
    }
</style>

<script type="text/javascript">
 
    function stopEventPropagation(e) {
        if (!e) { e = window.event; }
        if (!e) { return false; }
        e.cancelBubble = true;
        if (e.stopPropagation) { e.stopPropagation(); }
    }

    function assignEventsToCheckboxes() {

        document.getElementById('ChkSelector').checked = false;

        // disable the record selection feature by clicking on the records
        var sRecordsIds = grdPOMST.getRecordsIds();
        var arrRecordsIds = sRecordsIds.split(",");
        for (var i = 0; i < arrRecordsIds.length; i++) {
            var oRecord = document.getElementById(arrRecordsIds[i]);
            oRecord.onmousedown = function(e) { stopEventPropagation(e); };
            oRecord.onclick = function(e) { stopEventPropagation(e); };
        }

        // populate the previously checked checkboxes
        var arrPageSelectedRecords = grdPOMST.PageSelectedRecords;
        for (var i = 0; i < arrPageSelectedRecords.length; i++) {
            var oCheckbox = document.getElementById("chk_grid_" + arrPageSelectedRecords[i].ProductID);
            oCheckbox.checked = true;
        }

        // enable the record selection feature by selecting the checkboxes
        var arrCheckboxes = document.getElementsByTagName("INPUT");
        for (var i = 0; i < arrCheckboxes.length; i++) {
            if (arrCheckboxes[i].type == "checkbox" && arrCheckboxes[i].id.indexOf("chk_grid_") == 0) {
                arrCheckboxes[i].onmousedown = function(e) { stopEventPropagation(e); };
                arrCheckboxes[i].onclick = function(e) { SelectDeselect(this); stopEventPropagation(e); };
            }
        }
    }

    function toggleSelection(checkbox) {
        var arrCheckboxes = document.getElementsByTagName("INPUT");
        for (var i = 0; i < arrCheckboxes.length; i++) {
            if (arrCheckboxes[i].type == "checkbox" && arrCheckboxes[i].id.indexOf("chk_grid_") == 0) {
                if (arrCheckboxes[i].checked != checkbox.checked) {
                    arrCheckboxes[i].checked = checkbox.checked;
                    SelectDeselect(arrCheckboxes[i]);
                }
            }
        }
    }

</script>

<table width="100%" align="left" class="tContentArial">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ToolTip="Update" ImageUrl="~/CommonImages/edit1.jpg"
                            Width="48" Height="41" ValidationGroup="M1" OnClick="imgbtnUpdate_Click1"></asp:ImageButton>
                    </td>
                    <%-- <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>--%>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        <asp:ImageButton ID="imgbtnFindTop" Width="48" Height="41" runat="server" ToolTip="Find"
                            ImageUrl="~/CommonImages/link_find.png"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Width="48" Height="41" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Fabric Purchase Order Closing</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            <cc3:Grid runat="server" ID="grdPOMST" CallbackMode="false" Serialize="true" AllowAddingRecords="false"
                AutoGenerateColumns="false" AllowRecordSelection="false" AllowFiltering="True"
                AllowMultiRecordSelection="false" OnRowDataBound="grdPOMST_RowDataBound">
                <ClientSideEvents OnClientCallback="assignEventsToCheckboxes" />
                <Columns>
                    <cc3:Column DataField="COMP_CODE" HeaderText="COMPANY NAME" Visible="false" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="BRANCH_CODE" HeaderText="BRANCH NAME" Visible="false" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="PO_TYPE" HeaderText="PO Type" Width="70" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="PO_NUMB" HeaderText="PO #" Width="60" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="PO_NATURE" HeaderText="PO Nature" Width="90" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="PARTY_DATA" HeaderText="Party" Width="200" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="DEL_DATE" HeaderText="Del. Date" Width="90" DataFormatString="{0:MM/dd/yyyy}"
                        Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="ProductID" HeaderText="Close" Width="70" Wrap="true">
                        <TemplateSettings TemplateId="GetCheckBoxTemplate1" />
                    </cc3:Column>
                    <cc3:Column DataField="CONF_BY" HeaderText="Conf. By" Width="110" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="CONF_DATE" HeaderText="Conf. Date" Width="90" DataFormatString="{0:MM/dd/yyyy}"
                        Wrap="true">
                    </cc3:Column>
                    <cc3:Column HeaderText="Comments" Width="150" Wrap="true">
                        <TemplateSettings TemplateId="GetTextBoxTemplate1" />
                    </cc3:Column>
                 <%--   <cc3:Column HeaderText="View Details" Wrap="true">
                        <TemplateSettings TemplateId="ViewPODTLTemplate1" />
                    </cc3:Column>--%>
                </Columns>
                <Templates>
                    <cc3:GridTemplate runat="server" ID="GetCheckBoxTemplate1">
                        <Template>
                            <asp:CheckBox runat="server" ID="ChkID" ToolTip="<%# Container.Value %>" />
                        </Template>
                    </cc3:GridTemplate>
                    <cc3:GridTemplate runat="server" ID="GetTextBoxTemplate1">
                        <Template>
                            <asp:TextBox runat="server" ID="txtID" Width="120px" CssClass="TextBox SmallFont" />
                        </Template>
                    </cc3:GridTemplate>
                    
                </Templates>
                <FilteringSettings FilterPosition="Top" />
            </cc3:Grid>
            </span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            &nbsp;</td>
    </tr>
</table>

<script type="text/javascript">
    function setChkboxValue() {
        if (grdPOMST.SelectedRecords.length <= 0) {
            return
        }

        for (var i = 0; i < grdPOMST.SelectedRecords.length; i++) {
            var record = grdPOMST.SelectedRecords[i];
            var chk = document.getElementById("chk_grid_" + record.ProductID);
            chk.checked = true;
        }
    }
    function init() {
        assignEventsToCheckboxes();
        setChkboxValue();
    }

    var oldonload = window.onload;
    if (typeof window.onload != 'function') {
        window.onload = init;
    }
    else {
        window.onload = function() {
            if (oldonload) {
                oldonload();
            }
            init();

        }
    }

</script>
