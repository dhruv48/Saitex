<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LabDipQueryForm.ascx.cs"
    Inherits="Module_OrderDevelopment_LabDip_Controls_LabDipQueryForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc4" %>

<script type="text/javascript" language="javascript">
    // Added By Rajesh for Printing Directly from Form (05 Jan 2012)
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        if (prtContent != null) {
            var WinPrint = window.open('', '', 'center=1,width=800,height=600,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            //WinPrint.close();
            //prtContent.innerHTML = strOldOne;
        }
    }    
</script>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>
        <table class="td tContentArial" width="100%">
            <tr>
                <td align="left" class="td" colspan="4" width="100%">
                    <table align="left">
                        <tr>
                            <td id="tdPrint" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                    ToolTip="Print" Width="48" OnClick="imgbtnPrint_Click" />
                            </td>
                            <td id="tdClear" runat="server">
                                <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                    OnClick="imgbtnClear_Click" ToolTip="Clear" Width="48" />
                            </td>
                            <td id="tdExit" runat="server">
                                <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                                    Width="48" Height="41" OnClick="imgbtnExit_Click"></asp:ImageButton>
                            </td>
                            <td id="tdHelp" runat="server" align="left" width="48">
                                <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                                    ToolTip="Help" Height="41" Width="48" OnClick="imgbtnHelp_Click"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="TableHeader td" align="center" colspan="4" width="100%">
                    <span class="titleheading">Query form for Lab Entry</span>
                </td>
            </tr>
            <tr>
                <td align="left" valign="top" class="td" colspan="4" width="100%">
                    <span class="Mode">
                        <asp:Label ID="lblMode" runat="server"></asp:Label>
                    </span>
                </td>
            </tr>
            <tr>
                <td class="tdLeft" width="25%">
                    <asp:LinkButton ID="btnShowCR" runat="server" Text="Customer request pending for Lab Dip"
                        OnClick="btnShowCR_Click" />
                </td>
                <td class="tdLeft" width="25%">
                    <asp:LinkButton ID="btnShowLDNA" runat="server" Text="LabDip Pending for submission and approval"
                        OnClick="btnShowLDNA_Click" />
                </td>
                <td class="tdLeft" width="25%">
                    <asp:LinkButton ID="btnShowLDA" runat="server" Text="Approved Lab Dip" OnClick="btnShowLDA_Click" />
                </td>
                <td class="tdLeft" width="25%">
                    <asp:LinkButton ID="btnRejected" runat="server" Text="Rejected Lab Dip" OnClick="btnRejected_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Button ID="btnPrint" runat="Server" Text="Print Records" />
                </td>
            </tr>
            <tr>
                <td colspan="4" class="td" width="100%">
                    <asp:Panel ID="pnlShowHover" runat="server" BackColor="Beige" BorderWidth="2px" Height="440px"
                        ScrollBars="Auto" Width="100%">
                        <div id="divPrint">
                            <asp:GridView ID="grdLabDip" runat="server" AutoGenerateColumns="False" Width="98%"
                                CellPadding="4" ForeColor="#333333" GridLines="None" HeaderStyle-Wrap="true"
                                Font-Size="X-Small">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                </Columns>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>
