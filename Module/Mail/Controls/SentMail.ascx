<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SentMail.ascx.cs" Inherits="Module_Mail_Controls_SentMail" %>
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
<table width="100%" align="left" class="tContentArial">
    <tr>
        <td class="td tdLeft" width="100%">
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <cc3:Grid runat="server" ID="grdSendList" CallbackMode="false" AutoPostBackOnSelect="true"
                Serialize="true" AllowAddingRecords="false" AutoGenerateColumns="false" AllowRecordSelection="true"
                AllowFiltering="True" Width="98%" AllowMultiRecordSelection="false" OnSelect="grdSendList_Select">
                <Columns>
                    <cc3:Column DataField="RECEIVE_MAIL_ID" Visible="false">
                    </cc3:Column>
                    <cc3:Column DataField="EMAIL_ADD" HeaderText="From" Width="150" Visible="true" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="TO_EMAIL_ADD" HeaderText="To" Width="150" Visible="true" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="SUBJECT" HeaderText="Subject" Width="150" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="BODY" HeaderText="Body" Width="150">
                    </cc3:Column>
                    <cc3:Column DataField="HAS_ATTACH" HeaderText="Attachment" Width="80" Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="TDATE" HeaderText="Date" Width="90" DataFormatString="{0:MM/dd/yyyy}"
                        Wrap="true">
                    </cc3:Column>
                    <cc3:Column DataField="READ_STATUS" Visible="false" Wrap="true">
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
