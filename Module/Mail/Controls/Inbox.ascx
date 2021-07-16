<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Inbox.ascx.cs" Inherits="Module_Mail_Controls_Inbox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc2" %>
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

        //document.getElementById('ChkSelector').checked = false;

        // disable the record selection feature by clicking on the records
        var sRecordsIds = grdInboxList.getRecordsIds();
        var arrRecordsIds = sRecordsIds.split(",");
        for (var i = 0; i < arrRecordsIds.length; i++) {
            var oRecord = document.getElementById(arrRecordsIds[i]);
            oRecord.onmousedown = function(e) { stopEventPropagation(e); };
            oRecord.onclick = function(e) { stopEventPropagation(e); };
        }

        // populate the previously checked checkboxes
        var arrPageSelectedRecords = grdInboxList.PageSelectedRecords;
        for (var i = 0; i < arrPageSelectedRecords.length; i++) {
            var oCheckbox = document.getElementById("chk_grid_" + arrPageSelectedRecords[i].RECEIVE_MAIL_ID);
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
        <td class="td tdLeft" width="100%">
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <cc3:Grid runat="server" ID="grdInboxList" CallbackMode="false" AutoPostBackOnSelect="true"
                Serialize="true" AllowAddingRecords="false" AutoGenerateColumns="false" AllowRecordSelection="true"
                AllowFiltering="True" Width="98%" AllowMultiRecordSelection="false" OnSelect="grdInboxList_Select">
                <Columns>
                    <cc3:Column DataField="RECEIVE_MAIL_ID" Visible="false">
                    </cc3:Column>
                    <cc3:Column DataField="FROM_EMAIL_ADD" HeaderText="From" Width="150" Visible="true"
                        Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="EMAIL_ADD" HeaderText="To" Width="150" Visible="true" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="SUBJECT" HeaderText="Subject" Width="150" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="BODY" HeaderText="Body" Width="150" >
                    </cc3:Column>
                    <cc3:Column DataField="HAS_ATTACH" HeaderText="Attachment" Width="80" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="READ_STATUS" HeaderText="Status" Width="80" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="TDATE" HeaderText="Date" Width="90" DataFormatString="{0:MM/dd/yyyy}"
                        Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="TO_EMAIL_ADD" Visible="false" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="CC_EMAIL_ADD" Visible="false" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="BCC_EMAIL_ADD" Visible="false" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="ATTACH_IDS" Visible="false" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="FOLDER_NAME" Visible="false" Wrap="true">
                    </cc3:Column>
                </Columns>
                <FilteringSettings FilterPosition="Top" />
            </cc3:Grid>
        </td>
    </tr>
</table>

<script type="text/javascript">
    function setChkboxValue() {
        if (grdInboxList.SelectedRecords.length <= 0) {
            return
        }

        for (var i = 0; i < grdInboxList.SelectedRecords.length; i++) {
            var record = grdInboxList.SelectedRecords[i];
            var chk = document.getElementById("chk_grid_" + record.RECEIVE_MAIL_ID);
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

