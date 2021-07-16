<%@ Control Language="C#" AutoEventWireup="true" CodeFile="approved_prod1.ascx.cs" Inherits="Module_OrderDevelopment_Controls_approved_prod1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function FillCrUnit(val) {
        //        alert(val);
        var aapunit = document.getElementById(val).value;
        // alert(aapunit);
        if (aapunit == "") {
            var mySplitResult = val.substr(0, 64);
            //alert(mySplitResult);
            var merger = mySplitResult + 'lblRequestedNoOfUnit';
            // alert(merger);
            var a = document.getElementById(merger).innerHTML;
            //alert(a);
            document.getElementById(val).value = a;
        }
    }
</script>

<style type="text/css">
    .HideControl
    {
        visibility: hidden;
    }                                        
    .pager span
    {
        color: #009900;
        font-weight: bold;
        font-size: 16pt;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left" visible ="false" runat="server">
                       <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" onclick="imgbtnAddNew_Click" ></asp:ImageButton></td>
                    <td id="tdDelete" runat="server" align="left" visible="false">
                        &nbsp;</td>
                    <td id="tdFind" runat="server" visible="false" align="left">
                        &nbsp;</td>
                    <td visible="false">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
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
            <b class="titleheading"><asp:Label ID="lblHeader" runat="server" Text="Approval Form For Issue Against PA"></asp:Label> </b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
          
            <asp:GridView ID="gvLogistic" runat="server" AutoGenerateColumns="False"
                CssClass="SmallFont" Width="100%" OnRowDataBound="gvLogistic_RowDataBound"
                 AllowPaging="True"
                PagerStyle-CssClass="pager" 
                PageSize="12" onpageindexchanging="gvLogistic_PageIndexChanging">
                <RowStyle BackColor="White" />
                <Columns>    
                              
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Logistic Code">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td colspan="2" width="100%">
                                       Order No.
                                    </td>
                                </tr>
                                <tr>
                                    <td width="80%">
                                        <asp:TextBox ID="txtOrderNo" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnGetLogistic" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblOrderNo" CssClass=" SmallFont" runat="server" Text='<%# Eval("ORDER_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass=" label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField> 
                   
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Approved Date">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                      Approved Date
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnApprovedDate" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:TextBox ID="txtApprovedDate" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                   <%-- <cc1:CalendarExtender ID="CE12" Format="dd/MM/yyyy" runat="server" TargetControlID="txtLogisticDate"></cc1:CalendarExtender>
                                     <cc1:  ID="MaskedEditExtender2" runat="server" Mask="99/99/9999" MaskType="Date" PromptCharacter="_" TargetControlID="txtLogisticDate"></cc1:MaskedEditExtender>
--%>
                                    
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblApprovedDate" runat="server" CssClass="label smallfont" Text='<%# Bind("ORDER_DATE", "{0:dd/MM/yyyy}") %>'></asp:Label>
                        
                       
                        
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Name">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Party  </td>
                                    <td>
                                        <asp:ImageButton ID="btnPartyName" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtPartyName" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                           
                            <asp:LinkButton ID="lnkPartyName" Font-Size="Smaller" runat="server" Text='<%# Bind("PRTY_NAME") %>'></asp:LinkButton>
                            <asp:Panel ID="pnlParty2" runat="server" BackColor="Red">
                                <asp:Label ID="lblPartyAddress" Font-Size="Smaller" runat="server" Text='<%# Bind("PRTY_ADD1") %>'></asp:Label>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmeParty2" runat="server" PopupPosition="Top" TargetControlID="lnkPartyName"
                                PopupControlID="pnlParty2" PopDelay="50">
                            </cc1:HoverMenuExtender>
                      
                       
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Party Code" Visible="false">
                        
                        <ItemTemplate>
                            <asp:Label ID="lblPartyCode" runat="server" Text='<%# Bind("PRTY_Code") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"   Wrap="true" />
                    </asp:TemplateField>
                    
                    
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PA NO">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Pa No.
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnpano" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtpano" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblpano" runat="server" Text='<%# Bind("PI_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"   Wrap="true" />
                    </asp:TemplateField>
                    
                   <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="ARTICLE Name">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        Article  </td>
                                    <td>
                                        <asp:ImageButton ID="btnARTICLECODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtARTICLECODE" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                           
                            <asp:LinkButton ID="lnkARTICLECODE" Font-Size="Smaller" runat="server" Text='<%# Bind("ARTICAL_CODE") %>'></asp:LinkButton>
                            <asp:Panel ID="pnlParty3" runat="server" BackColor="Red">
                                <asp:Label ID="lblARTICLEDESC" Font-Size="Smaller" runat="server" Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmeParty3" runat="server" PopupPosition="Top" TargetControlID="lnkARTICLECODE"
                                PopupControlID="pnlParty3" PopDelay="50">
                            </cc1:HoverMenuExtender>
                      
                       
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Article Code" Visible="false">
                        
                        <ItemTemplate>
                            <asp:Label ID="lblARTICLECODE" runat="server" Text='<%# Bind("ARTICAL_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"   Wrap="true" />
                    </asp:TemplateField>
                    
                    
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Shade Code">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td width="80%">
                                       Shade Code
                                    </td>
                                    <td width="20%">
                                        <asp:ImageButton ID="btnGrey" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" width="100%">
                                        <asp:TextBox ID="txtGrey" Width="98%" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>                        
                            <asp:Label ID="lblGrey" runat="server" CssClass="label smallfont" Text='<%# Bind("SHADE_CODE") %>' ToolTip='<%# Bind("SHADE_CODE") %>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Approx Qty">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                      Ord.&nbsp;Qty
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnQty" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtQty" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                          
                                    <cc1:FilteredTextBoxExtender ID="FiltertxtRate1" runat="server"  TargetControlID="txtQty"   FilterType="Custom, Numbers" ValidChars="."/>

                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                           
                               <asp:Label ID="lblQty" Font-Size="Smaller" runat="server" Text='<%# Bind("ORD_QTY") %>'></asp:Label>&nbsp; <asp:Label ID="lblUOM" Font-Size="Smaller" runat="server" Text='<%# Bind("UOM") %>'></asp:Label>
                           
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>   
                     <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="PA NO">
                        <HeaderTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        PA NO.
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnpano" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                            Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtpano" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblpano" runat="server" Text='<%# Bind("PA_NO") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"   Wrap="true" />
                    </asp:TemplateField>
                    --%>
                   <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnViewTRN" runat="server" Text="View Details"></asp:LinkButton>
                            <asp:Panel ID="pnlTRN" runat="server" Width="470px" BackColor="Beige" BorderWidth="2px"
                                Height="140px" ScrollBars="Auto">
                                <asp:GridView ID="grdTRN" runat="server" AutoGenerateColumns="False" Width="450px"
                                    CssClass="SmallFont" Height="140px">
                                    <Columns>
                                        <asp:BoundField DataField="BASE_ARTICAL_CODE" HeaderText="Fiber Code">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FIBER_DESC" HeaderText="Fiber Description">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="REQ_QTY" HeaderText="Appr. Req. Qty.">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM1" HeaderText="UOM" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="Req. Qty" HeaderStyle-HorizontalAlign="Right" runat="server"  Visible="false" ItemStyle-HorizontalAlign="Right">
                                 <ItemTemplate>
                             
                                    <asp:Label ID="lblREQ_QTY" runat="server" Text='<%# Bind("REQ_QTY") %>' 
                                Visible="false"></asp:Label>
                               </ItemTemplate>
                               <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                             </asp:TemplateField>
                                        <asp:BoundField DataField="REMQTY" HeaderText="Stock Qty" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="UOM1" HeaderText="UOM" HeaderStyle-HorizontalAlign="Right"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="labelNo smallfont" VerticalAlign="Top" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="REMQTY" HeaderStyle-HorizontalAlign="Right" runat="server"  Visible="false" ItemStyle-HorizontalAlign="Right">
                                 <ItemTemplate>
                             
                                    <asp:Label ID="lblREMQTY" runat="server" Text='<%# Bind("REMQTY") %>' 
                                Visible="false"></asp:Label>
                               </ItemTemplate>
                               <ItemStyle CssClass="labelNo smallfont" Wrap="true" HorizontalAlign="Right" VerticalAlign="Top" />
                             </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="SmallFont" Width="98%" />
                                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="BlueViolet" />
                                </asp:GridView>
                            </asp:Panel>
                            <cc1:HoverMenuExtender ID="hmeTRN" runat="server" PopupPosition="Left" PopupControlID="pnlTRN"
                                TargetControlID="lbtnViewTRN">
                            </cc1:HoverMenuExtender>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" Wrap="true" VerticalAlign="Top" />
                    </asp:TemplateField>
                    
                   
                     <asp:TemplateField HeaderText="Issue">
                        <ItemTemplate>
                       
                       <asp:Button ID="BtnISSUE" runat="server" OnClick="BtnISSUE_Click" Text="ISSUE"  runat="server" AutoPostBack="True"
                                            ValidationGroup="BA" CssClass="SmallFont" Width="40px" />
                        </ItemTemplate>
                        <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField> 
                    
                    <%-- <asp:TemplateField HeaderText="Close" >
                        <ItemTemplate>
                            <asp:CheckBox ID="chkClosed"  runat="server" AutoPostBack="True" OnCheckedChanged="ChkClosed_CheckedChanged" />
                        </ItemTemplate>
                        <ItemStyle CssClass="label Smallfont" HorizontalAlign="Center" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    --%>
               
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Comp Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblCompCode" runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    
                   <%--  <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="year" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblYear" runat="server" Text='<%# Eval("YEAR") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>
                    --%>
                     <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Branch Code" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="lblBranchCode" CssClass=" SmallFont" runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle CssClass="label Smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                            Wrap="true" />
                    </asp:TemplateField>  
                   
                   
                </Columns>
                <PagerStyle BackColor="#336799" ForeColor="White" HorizontalAlign="Left" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#336799" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
            </asp:GridView>
           
        </td>
    </tr>
</table>
    </ContentTemplate>
</asp:UpdatePanel>