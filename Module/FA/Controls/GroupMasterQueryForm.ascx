<%@ Control Language="C#" AutoEventWireup="true" CodeFile="GroupMasterQueryForm.ascx.cs"
    Inherits="Module_FA_Controls_GroupMasterQueryForm" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>

<script type="text/javascript">
    var isAppliedFilter = false;
       var printGridOnCallback = false;
		    var currentPageSize = 10;
    function applyFilter() {

        document.getElementById('apply').style.display = '';
        document.getElementById('remove').style.display = '';
        document.getElementById('hide').style.display = '';
        document.getElementById('show').style.display = 'none';

        grid1.filter()
        isAppliedFilter = true;
        return false;
    }
    function hideFilter() {

        if (isAppliedFilter == true) {
            document.getElementById('remove').style.display = '';
        } else {
            document.getElementById('remove').style.display = 'none';
        }
        document.getElementById('show').style.display = '';
        document.getElementById('hide').style.display = 'none';
        document.getElementById('apply').style.display = 'none';

        grid1.hideFilter()

        return false;
    }
    function showFilter() {

            if (isAppliedFilter == true) {
                document.getElementById('remove').style.display = '';
            } else {
                document.getElementById('remove').style.display = 'none';
            }
            document.getElementById('apply').style.display = '';
            document.getElementById('hide').style.display = '';
            document.getElementById('show').style.display = 'none';
            grid1.showFilter();

            return false;
    }

    function removeFilter() {

        document.getElementById('show').style.display = '';
        document.getElementById('apply').style.display = 'none';
        document.getElementById('hide').style.display = 'none';
        document.getElementById('remove').style.display = 'none';

        grid1.removeFilter();
        grid1.hideFilter();
        isAppliedFilter = false;
        return false;
    }
   
		    function printGrid(printAll) {
		        if (printAll) {
		            printGridOnCallback = true;
		            currentPageSize = grid1.getPageSize();
		            ob_grid1PageSizeSelector.value(-1);
		        } else {
		            grid1.print();
		        }

		        return false;
		    }

		    function grid1_Callback() {
		        if (printGridOnCallback) {
		            grid1.print();
		            printGridOnCallback = false;
		            ob_grid1PageSizeSelector.value(currentPageSize);
		        }
		    }
</script>

<asp:LinkButton ID="lbPage1" runat="server" PostBackUrl="~/Module/FA/Queries/LedgerMasterQuery.aspx"></asp:LinkButton><br />

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
                    <span class="titleheading">Group Master Query</span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    <cc2:Grid ID="Grid1" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                        PageSize="5" AutoGenerateColumns="False" Serialize="false" AllowPaging="true"
                        AllowPageSizeSelection="true" AllowMultiRecordSelection="False">
                        <Columns>
                            <cc2:Column DataField="GRP_CODE" Align="Left" HeaderText="Group Code" Width="100px">
                            </cc2:Column>
                            <cc2:Column DataField="GRP_NAME" Align="Left" HeaderText="Group Name" Width="150px">
                            </cc2:Column>
                            <cc2:Column DataField="PARENT_NAME" Align="Left" HeaderText="Parent Name" Width="130px">
                            </cc2:Column>
                            <cc2:Column DataField="GRP_DESC" Align="Left" HeaderText="Description" Width="150px">
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
