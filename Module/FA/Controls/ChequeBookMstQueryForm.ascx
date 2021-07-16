<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ChequeBookMstQueryForm.ascx.cs"
    Inherits="Module_FA_Controls_ChequeBookMstQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc1" %>
<asp:LinkButton ID="lbPage1" runat="server" PostBackUrl="~/Module/FA/Queries/BankMasterQueryForm.aspx"></asp:LinkButton><br />

<script type="text/javascript">
document.attachEvent('onkeyup', KeysShortcut);
function KeysShortcut ()
{    
if (event.keyCode == 49)    
{      
document.getElementById('<%= lbPage1.ClientID %>').click();   
}
}

</script>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td class="td">
                    <table>
                        <tr>
                            <td id="tdPrint" runat="server">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                            </td>
                            <td id="tdHelp" runat="server">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                    OnClick="imgbtnHelp_Click" ToolTip="Help" Width="48" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                    OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="TableHeader">
                <td align="center" valign="top" class="td">
                    <span class="titleheading">Cheque Book Query</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <cc2:Grid ID="grdChequeBook" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False">
                        <Columns>
                            <cc2:Column DataField="LGR_BANK_CODE" Align="Right" HeaderText="Bank Code" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="LGR_BANK_NAME" Align="Left" HeaderText="Bank Name" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="BANK_AC_NO" Align="Left" HeaderText="Account No" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="CHEQUEBOOK_NO" Align="Right" HeaderText="Cheque Book No" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="START_LEAF" Align="Right" HeaderText="Start Leaf" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="END_LEAF" Align="Left" HeaderText="End Leaf" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="ISSUED_ON" Align="Left" HeaderText="Issued Date" DataFormatString="{0:dd-MMM-yyyy}"
                                Width="110px">
                            </cc2:Column>
                        </Columns>
                        <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                        <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                    </cc2:Grid>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
