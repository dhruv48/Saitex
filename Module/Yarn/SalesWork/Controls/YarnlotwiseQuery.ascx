<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YarnlotwiseQuery.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_YarnlotwiseQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
    function CallPrint(strid) {
        var prtContent = document.getElementById(strid);
        if (prtContent != null) {
            var WinPrint = window.open('', '', 'center=1,width=800,height=600,toolbar=0,scrollbars=1,status=0');
            WinPrint.document.write(prtContent.innerHTML);
            //WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
        }
    }    
</script>

<table align="left" class="tContentArial" width="100%">
    <tr>
        <td align="left" valign="top" class="td" width="100%">
            <table align="left">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        &nbsp;<td id="tdFind" runat="server" visible="false" align="left">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnPrint" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/link_print.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="ImageButton1" runat="server" Width="48" Height="41" ToolTip="Print"
                                ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click1"></asp:ImageButton>&nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                                ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:ImageButton ID="imgbtnExit" runat="server" Width="48" Height="41" ToolTip="Exit"
                                ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                        </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Lot Wise Yarn Stock Query </b>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            <asp:Button ID="printGrid" runat="server" Text="Print Grid" Width="85px" Height="22px"
                CssClass="AButton" OnClick="printGrid_Click"   Visible="false"/>
                <asp:DropDownList ID="ddlTRANTYPE" runat="server"   Width="150px" 
                Height="17px" CssClass="SmallFont" AutoPostBack="True" 
                onselectedindexchanged="ddlTRANTYPE_SelectedIndexChanged" ></asp:DropDownList>
            <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label></b>
            <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
            <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
              
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="400px" Width="100%">
                <div id="divPrint">
                    <asp:GridView ID="gvStock" runat="server" AutoGenerateColumns="false" CssClass="SmallFont"
                        Width="100%" OnPageIndexChanging="gvStock_PageIndexChanging1" 
                        OnPreRender="gvStock_PreRender1" ShowFooter="true">
                       
                        <Columns>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Yarn Code" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Yarn&nbsp;Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtYarnCode" Width="65px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtYarnCode" runat="server" ServiceMethod="AutoYarntxtYarnCodeL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtYarnCode" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnTrnDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblYarnDesc" runat="server" ToolTip='<%# Bind("YARN_CODE") %>' Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Yarn  Desc">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Yarn&nbsp;Desc
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtYarnDesc" Width="170px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtYarnDesc" runat="server" ServiceMethod="AutoYarntxtYarnDescL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtYarnDesc" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnYarnDesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTrnDesc" runat="server" ToolTip='<%# Bind("YARN_DESC") %>' Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SHADE FAMILY" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                Shade&nbsp;Family
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnBalBale" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:TextBox ID="txtSHADEFAMILY" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtSHADEFAMILY" runat="server" ServiceMethod="AutoYarntxtSHADEFAMILYL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtSHADEFAMILY" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBalBale" runat="server" Text='<%# Eval("SHADE_FAMILY") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="SHADE CODE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Shade&nbsp;Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtSHADECODE" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtSHADECODE" runat="server" ServiceMethod="AutoYarntxtSHADECODEL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtSHADECODE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnIssueBale" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIssueBale" runat="server" Text='<%# Eval("SHADE_CODE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Lot No">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Lot&nbsp;No
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtLotNo" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtLotNo" runat="server" ServiceMethod="AutoYarntxtLotNoL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtLotNo" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnLotNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLotNo" runat="server" Text='<%# Eval("LOT_NO") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Batch No">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Batch&nbsp;No
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtBatchNo" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtBatchNo" runat="server" ServiceMethod="AutoYarntxtBatchNoL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtLotNo" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnBatchNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblDyedBatchNo" runat="server" Text='<%# Eval("DYED_BATCH") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Grade">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Grade
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtgrade" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtgrade" runat="server" ServiceMethod="AutoYarntxtgradeL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtgrade" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btngrade" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGrade" runat="server" Text='<%# Eval("GRADE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Gross Wt">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Gross Wt
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtGrossWt" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <%--   <cc1:AutoCompleteExtender ID="AutoCompletetxtWEIGHTOFUNIT" runat="server" ServiceMethod="AutoYarntxtWEIGHTOFUNITL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtWEIGHTOFUNIT" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>--%>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnGrossWt" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblGrossWt" runat="server" Text='<%# Eval("GROSS_WT") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                             <asp:Label ID="lblTotalGrossWt" runat="server" CssClass="Label SmallFont"  Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Tare Wt">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Tare Wt
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtTareWt" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <%-- <cc1:AutoCompleteExtender ID="AutoCompletetxtWEIGHTOFUNIT" runat="server" ServiceMethod="AutoYarntxtWEIGHTOFUNITL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtWEIGHTOFUNIT" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>--%>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnTareWt" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTareWt" runat="server" Text='<%# Eval("TARE_WT") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                             <asp:Label ID="lblTotalTareWt" runat="server" CssClass="Label SmallFont" Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Net Wt">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Net Wt
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtNetWt" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                               
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnNetWt" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNetWt" runat="server" Text='<%# Eval("NET_WT") %>'></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                             <asp:Label ID="lblTotalNetWt" runat="server" CssClass="Label SmallFont"  Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="NO OF UNIT">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Cops
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtNOOFUNIT" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtNOOFUNIT" runat="server" ServiceMethod="AutoYarntxtNOOFUNITL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtNOOFUNIT" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnpalletno" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBale" runat="server" Text='<%# Eval("COPS") %>'></asp:Label>
                                </ItemTemplate>
                                  <FooterTemplate>
                                             <asp:Label ID="lblTotalBale" runat="server" CssClass="Label SmallFont"  Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" WEIGHT OF UNIT">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Avg&nbsp;Wt
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtWEIGHTOFUNIT" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtWEIGHTOFUNIT" runat="server" ServiceMethod="AutoYarntxtWEIGHTOFUNITL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtWEIGHTOFUNIT" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnTotalBale" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblAwgWt" runat="server" Text='<%# Eval("AVG_WT") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText=" Cartons">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Carton&nbsp;No
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtCartonNo" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <%--  <cc1:AutoCompleteExtender ID="AutoCompletetxtWEIGHTOFUNIT" runat="server" ServiceMethod="AutoYarntxtWEIGHTOFUNITL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtWEIGHTOFUNIT" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>--%>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnCartonNo" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCartonNo" runat="server" Text='<%# Eval("CARTON_NO") %>'></asp:Label>
                                </ItemTemplate>
                                 <FooterTemplate>
                                             <asp:Label ID="lblTotalCartonNo" runat="server" CssClass="Label SmallFont"  Font-Bold="true"></asp:Label>                                           
                                            </FooterTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>  
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Barcode" Visible="false">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Barcode
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtBarcode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <%-- <cc1:AutoCompleteExtender ID="AutoCompletetxtWEIGHTOFUNIT" runat="server" ServiceMethod="AutoYarntxtWEIGHTOFUNITL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtWEIGHTOFUNIT" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>--%>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnBarcode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBarcode" runat="server" Text='<%# Eval("BARCODE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="LOCATION">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                Location
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnQuantity" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:TextBox ID="txtLOCATION" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtLOCATION" runat="server" ServiceMethod="AutoYarntxtLOCATION"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtLOCATION" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLocation" runat="server" Text='<%# Eval("LOCATION") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="Store">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td width="80%">
                                                Store
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnStore" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" width="100%">
                                                <asp:TextBox ID="txtStore" Width="45px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtSTORE" runat="server" ServiceMethod="AutoYarntxtSTORE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtLOCATION" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStore" runat="server" Text='<%# Eval("STORE") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText="Po Numb">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                Po/Job Card Num
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtPoNumb" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtPoNumb" runat="server" ServiceMethod="AutoYarntxtPoNumbL"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtPoNumb" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnFiberCat" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFiberCat" runat="server" ToolTip='<%# Eval("PO_NUMB") %>' Text='<%# Eval("PO_NUMB") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Left" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderStyle-HorizontalAlign="Right" HeaderText=" Rgb">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2" width="100%">
                                                RGB
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtRGB" Width="35px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtRGB" runat="server" ServiceMethod="AutoYarntxtRGB"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtRGB" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnWeightofUnit" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblWeightofUnit" runat="server" Text='<%# Eval("RGB") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle CssClass="label smallfont" HorizontalAlign="Right" VerticalAlign="Top"
                                    Wrap="true" />
                            </asp:TemplateField>
                        
                        </Columns>
                        <PagerStyle BackColor="#336799"  HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" />
                        <HeaderStyle BackColor="#336799" Font-Bold="True"  />
                        <EditRowStyle BackColor="#2461BF" />
                        <FooterStyle ForeColor="Black" Font-Bold="true" />
                    </asp:GridView>
                </div>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td align="left" class="td" width="100%">
            &nbsp;
        </td>
    </tr>
</table>
