<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BankMasterQueryForm.ascx.cs"
    Inherits="Module_FA_Controls_BankMasterQueryForm" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Grid" Assembly="obout_Grid_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="cc2" %>
<%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
    <ContentTemplate>--%>
    <script language="javascript" type="text/javascript">

        function Calculation(val) {
            var name = val;

            document.getElementById('ctl00_cphBody_POCredit1_txtAdvanceAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtAdvance').value) * (parseFloat(document.getElementById('ctl00_cphBody_POCredit1_txtFinalTotal').value) / 100)).toFixed(3);
        }
        function SetFocus(ControlId) {
            document.getElementById(ControlId).focus();
        }
        function GetClick(ButtonId) {
            document.getElementById(ButtonId).click();
        }
   
</script>
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
                    <span class="titleheading">Bank Master Query </span>
                </td>
            </tr>
            <tr>
                <td class="td">
                    
                      <%--  <cc2:Grid ID="grdBank" runat="server" AllowAddingRecords="False" AllowFiltering="True"
                            PageSize="10" AutoGenerateColumns="False"  Serialize="false" AllowPaging="true"
                            AllowPageSizeSelection="true" AllowMultiRecordSelection="False" AutoPostBackOnSelect="True"
                            OnSelect="grdBank_Select">
                            <Columns>
                                <cc2:Column DataField="LGR_BANK_CODE" Align="Right" HeaderText="Code" Width="60px">
                                </cc2:Column>
                                <cc2:Column DataField="LGR_BANK_NAME" Align="Left" HeaderText="Bank Name" Width="130px">
                                </cc2:Column>
                                <cc2:Column DataField="LDGR_CODE" Align="Right" HeaderText="Code" Width="60px">
                                </cc2:Column>
                                <cc2:Column DataField="LDGR_NAME" Align="Left" HeaderText="Ledger Name" Width="120px">
                                </cc2:Column>
                                <cc2:Column DataField="BANK_BRANCH_CODE" Align="Right" HeaderText="Branch Code" Width="130px">
                                </cc2:Column>
                                <cc2:Column DataField="BANK_AC_NO" Align="Right" HeaderText="Bank A/c No" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="BANK_AC_TYPE" Align="Left" HeaderText="Bank A/c Type" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="BANK_BRANCH_ADD" Align="Left" HeaderText="Bank Branch Add."
                                    Width="130px">
                                </cc2:Column>
                                <cc2:Column DataField="OPENING_DATE" Align="Left" HeaderText="Opening Date" Width="120px">
                                </cc2:Column>
                                <cc2:Column DataField="RTGS_CODE" Align="Right" HeaderText="RTGS Code" Width="130px">
                                </cc2:Column>
                                <cc2:Column DataField="GRP_CODE" Align="Right" HeaderText="Group Code" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="GRP_NAME" Align="Left" HeaderText="Group Name" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="CHEQ_BOOK" Align="Right" HeaderText="Cheque Book" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="DEBIT_CARD" Align="Left" HeaderText="Debit Card" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="CREDIT_CARD" Align="Left" HeaderText="Credit Card" Width="130px">
                                </cc2:Column>
                                <cc2:Column DataField="INTERNET_BANKING" Align="Left" HeaderText="Internet Banking"
                                    Width="120px">
                                </cc2:Column>
                                <cc2:Column DataField="PHONE_BANKING" Align="Right" HeaderText="Phone Banking" Width="130px">
                                </cc2:Column>
                                <cc2:Column DataField="PASSBOOK" Align="Right" HeaderText="Passbook" Width="110px">
                                </cc2:Column>
                                <cc2:Column DataField="ONLINE_SHOPPING" Align="Left" HeaderText="Online Shoping"
                                    Width="110px">
                                </cc2:Column>
                            </Columns>
                            <PagingSettings PageSizeSelectorPosition="Bottom" ShowRecordsCount="true" Position="Bottom" />
                            <FilteringSettings InitialState="Hidden" FilterPosition="Top" FilterLinksPosition="Top" />
                        </cc2:Grid>--%>
                      <%--  <asp:Panel ID="panel1" runat="server" ScrollBars="Both" Height="250px" Width="800px">--%>
                    <asp:GridView ID="grdBank" ForeColor="#333333" runat="server"  AutoGenerateColumns="False"  
                                AllowPaging="True" PageSize="6" Font-Size="X-Small" OnPageIndexChanging="grdBank_PageIndexing" 
                                OnRowCommand="grdBank_RowCommand" Width="100%" OnRowDataBound="grdBank_RowBound">
                                <RowStyle BackColor="#EFF3FB" />
                                 <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Width="100%" />
                        <Columns>
                        <asp:TemplateField HeaderText="Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right">
                        <ItemTemplate>
                        <asp:Label Text='<%# Eval("LGR_BANK_CODE") %>' runat="server" ID="lblBankCode"></asp:Label>
                        </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                            <%--<asp:BoundField DataField="LGR_BANK_CODE" HeaderText="Code" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />--%>
                            <asp:BoundField DataField="LGR_BANK_NAME" HeaderText="Bank" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="LDGR_CODE" HeaderText="Code" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="LDGR_NAME" HeaderText="Ledger Name" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                              <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                              <asp:BoundField DataField="BANK_BRANCH_CODE" HeaderText="Branch Code" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                               <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                               <asp:BoundField DataField="BANK_AC_NO" HeaderText="Bank A/c No" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                               <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                               <asp:BoundField DataField="BANK_AC_TYPE" HeaderText="Bank A/c Type" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                               <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                               <asp:BoundField DataField="BANK_AC_NO" HeaderText="Bank A/c No" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" >
                               <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                               <asp:BoundField DataField="BANK_BRANCH_ADD" HeaderText="Bank Branch Add." 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                                <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                <asp:BoundField DataField="OPENING_DATE" HeaderText="Opening Date" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                                 <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                 <asp:BoundField DataField="RTGS_CODE" HeaderText="RTGS Code" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" >
                                  <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                                  <asp:BoundField DataField="GRP_CODE" HeaderText="Group Code" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" >
                                   <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                                   <asp:BoundField DataField="GRP_NAME" HeaderText="Group Name" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                                   <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                                   <asp:BoundField DataField="CHEQ_BOOK" HeaderText="Cheque Book" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="DEBIT_CARD" HeaderText="Debit Card" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="CREDIT_CARD" HeaderText="Credit Card" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="INTERNET_BANKING" HeaderText="INTERNET BANKING" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="PHONE_BANKING" HeaderText="Phone Banking" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="PASSBOOK" HeaderText="Passbook" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="right" >
                             <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundField>
                             <asp:BoundField DataField="ONLINE_SHOPPING" HeaderText="ONLINE SHOPPING" 
                                HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" >
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            </Columns>
                             <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                        VerticalAlign="Middle" />

                    </asp:GridView>
                  <%-- </asp:Panel>
 --%>               </td>
            </tr>
        </table>
   <%-- </ContentTemplate>
</asp:UpdatePanel>
--%>