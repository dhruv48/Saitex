<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FiberPOClosing.ascx.cs" Inherits="Module_Fiber_Controls_FiberPOClosing" %>
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
                    <td id="tdDelete" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnDelete" runat="server" Width="48" Height="41" ToolTip="Delete"
                            ImageUrl="~/CommonImages/del6.png"></asp:ImageButton>
                    </td>
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
            <b class="titleheading">Poy Purchase Order Closing</b>
        </td>
    </tr>
    <tr>
        <td align="left" valign="top" width="100%" class="td">
            <span class="Mode">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode
            </span>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:GridView ID="grdPOMST" runat="server"  CallbackMode="false" Serialize="true" AllowAddingRecords="false"
                AutoGenerateColumns="false" AllowRecordSelection="false" AllowFiltering="True"
                AllowMultiRecordSelection="false" width="100%" >
             
                <Columns>
                   
                    <asp:BoundField DataField="COMP_CODE" HeaderText="COMPANY NAME" Visible ="false">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PO_TYPE" HeaderText="PO Type">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>
                   
                    <asp:BoundField DataField="PO_NUMB" HeaderText="PO # ">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>
                      <asp:BoundField DataField="PO_NATURE" HeaderText="PO Nature">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>
                    
                     <asp:BoundField DataField="PARTY_DATA" HeaderText="Party">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="50px" />
                    </asp:BoundField>
                    
                    <asp:BoundField DataField="DEL_DATE" DataFormatString="{0:dd/MM/yyyy}" 
                        HeaderText="Required Date" HtmlEncode="False">
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" 
                            VerticalAlign="Top" Width="80px" />
                    </asp:BoundField>
                    
                    <asp:TemplateField HeaderText="Close">
                            <ItemTemplate>
                                <asp:CheckBox ID="ChkID" runat="server" ToolTip ="Please Select for Closing" />
                            </ItemTemplate>
                            <ItemStyle CssClass="label smallfont" HorizontalAlign="Center" 
                                VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Confirm By">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmBy" runat="server" CssClass="TextBox SmallFont TextBoxDisplay" 
                                Text='<%# Bind("CONF_BY") %>' Width="70px" ReadOnly ="true "></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Confirm Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtConfirmDate" runat="server" CssClass="TextBox SmallFont " 
                                Text='<%# Bind("CONF_DATE") %>' Width="55px" Height="16px"></asp:TextBox>
                            <cc1:calendarextender id="CalendarExtender1" runat="server" 
                                targetcontrolid="txtConfirmDate">
                            </cc1:calendarextender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    
                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" 
                                Width="100px"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" Width="50px" />
                    </asp:TemplateField>
                    
                   </Columns>
             </asp:GridView>
        </td>
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

