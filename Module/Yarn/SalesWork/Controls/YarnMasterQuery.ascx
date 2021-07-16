<%@ Control Language="C#" AutoEventWireup="true" CodeFile="YarnMasterQuery.ascx.cs"
    Inherits="Module_Yarn_SalesWork_Controls_YarnMasterQuery" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    .style1
    {
        height: 55px;
    }
</style>

<style type="text/css">
 #GridScroll{
	width:1100px;
	overflow:scroll;
	height:360px;
 }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel1"  runat="server">
    <ContentTemplate>--%>
<table align="left" class=" td tContentArial" width="100%">
    <tr>
        <td class="style1" width="99%">
            <table class=" td tContentArial">
                <tr>
                    <td id="tdUpdate" runat="server" align="left">
                        <asp:ImageButton ID="imgbtnAddNew" runat="server" Width="48" Height="41" ToolTip="Add New"
                            ImageUrl="~/CommonImages/addnew.png" OnClick="imgbtnAddNew_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" OnClick="imgbtnPrint_Click1" />
                    </td>
                    <td>
                        <asp:ImageButton ID="ImageButton1" runat="server" Width="48" Height="41" ToolTip="Print"
                            ImageUrl="~/CommonImages/export.png" OnClick="imgbtnPrint_Click"></asp:ImageButton>&nbsp;
                    </td>
                       <td> <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Width="48" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                            ToolTip="Exit" Width="48" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Width="48" OnClick="imgbtnHelp_Click" />
                    </td>
                </tr>
            </table>
            <tr>
                <td align="center" class="TableHeader td">
                    <span class="titleheading"><strong>
                    
                    
                    Yarn Master List</strong> </span>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <b>
                        <asp:Label ID="Label1" runat="server" Text="Total Record : " CssClass="Label"></asp:Label>
                        <asp:Label ID="lblTotalRecord" runat="server" CssClass="Label"></asp:Label></b>
                </td>
            </tr>
            <tr>
                <td align="center">
                <div id="GridScroll">
                    <asp:GridView ID="grdFiberMasterQuery" runat="server" AutoGenerateColumns="False"
                        Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="2" BorderStyle="Ridge" PageSize="13"
                        CssClass="smallfont" Font-Size="X-Small" ForeColor="#333333" PagerStyle-HorizontalAlign="Left" HeaderStyle-Wrap="true"
                        OnPageIndexChanging="grUserMasterQuery_PageIndexChanging" 
                         OnRowDataBound="RowDataBound"
                        onselectedindexchanged="grdFiberMasterQuery_SelectedIndexChanged1">
                        
                        
                      <%--  <asp:GridView ID="GridLedger" runat="server" AllowPaging="True" AllowSorting="True"
                                        AutoGenerateColumns="False" CellPadding="2" Font-Size="X-Small" 
                                        HeaderStyle-Wrap="true" OnPageIndexChanging="GridLedger_PageIndexChanging"
                                           OnRowDataBound="RowDataBound"
                                        
                                         PageSize="14" Width="250%" BackColor="White" BorderColor="#999999" 
                                        BorderStyle="None" BorderWidth="1px">--%>
                        
                        
                        
                        
                        
                        
                        
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                        <Columns>
                            <asp:TemplateField HeaderText="yarncode">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                               Yarn Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtyarncode" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtyarncode" runat="server" ServiceMethod="AutoYarnyarncode"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtyarncode" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnyarncode" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFabCode" runat="server" Text='<%#Eval("YARN_CODE") %>' ToolTip='<%#Eval("YARN_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                                <asp:TemplateField HeaderText="denier">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                Yarn Desc
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtYarnDesc" Width="150px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtYarnDesc" runat="server" ServiceMethod="AutoYarntxtYarnDesc"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtYarnDesc" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btndenier" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTechnical" runat="server" Text='<%#Eval("TECHNICAL_DESC") %>' ToolTip='<%#Eval("TECHNICAL_DESC") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="yarncat">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                CATEGORY
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtyarncat" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtyarncat" runat="server" ServiceMethod="AutoYarnyarncat"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtyarncat" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnyarncat" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCategory" runat="server" Text='<%#Eval("CATEGORY") %>' ToolTip='<%#Eval("CATEGORY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="yarn process">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                LUSTER
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtColour" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtColour" runat="server" ServiceMethod="AutoYarntxtColour"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtColour" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnyarnprocess" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblLuster" runat="server" Text='<%#Eval("LUSTER") %>' ToolTip='<%#Eval("LUSTER") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="yarndesc">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                PROCESS
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtPly" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtPly" runat="server" ServiceMethod="AutoYarntxtPly"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtPly" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnyarndesc" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblProcess" runat="server" Text='<%#Eval("PROCESS") %>' ToolTip='<%#Eval("PROCESS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="color">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               Yarn Type
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtyarntype" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtyarntype" runat="server" ServiceMethod="AutoYarnyarntype"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtyarntype" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btncolor" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblyarnType" runat="server" Text='<%#Eval("YARN_TYPE") %>' ToolTip='<%#Eval("YARN_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText=" MaxStock">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                TWIS TYPE
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtMaxStock" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtMaxStock" runat="server" ServiceMethod="AutoYarntxtMaxStock"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtMaxStock" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnYarnshade" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblTwistType" runat="server" Text='<%#Eval("TWIST_TYPE") %>' ToolTip='<%#Eval("TWIST_TYPE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            <asp:TemplateField HeaderText="Fancy Effect">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                DENIER/COUNT
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtFancyEffect" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtFancyEffect" runat="server" ServiceMethod="AutoYarntxtFancyEffect"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtFancyEffect" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnYarnblend" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblCount" runat="server" Text='<%#Eval("DENIER_COUNT") %>' ToolTip='<%#Eval("DENIER_COUNT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="stock">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                FILAMENT
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtBlending" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtBlending" runat="server" ServiceMethod="AutoYarntxtBlending"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtBlending" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnstock" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblFilament" runat="server" Text='<%#Eval("FILAMENT") %>' ToolTip='<%#Eval("FILAMENT") %>'></asp:Label>
                                </ItemTemplate>
                               
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="STORE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               BASE PLY
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtSTORE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtSTORE" runat="server" ServiceMethod="AutoYarnSTORE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtRGB" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnSTORE1" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblSTORE" runat="server" Text='<%#Eval("BASE_PLY") %>' ToolTip='<%#Eval("BASE_PLY") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                             <asp:TemplateField HeaderText="RGB">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               BASE DIRECTION
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtRGB" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtRGB" runat="server" ServiceMethod="AutoYarnRGB"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtRGB" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnRGB1" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblRGB" runat="server" Text='<%#Eval("BASE_DIRECTION") %>' ToolTip='<%#Eval("BASE_DIRECTION") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="LOCATION">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               MELANGE
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtLOCATION" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtLOCATION" runat="server" ServiceMethod="AutoYarnLOCATION"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtLOCATION" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnLOCATION1" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblLOCATION" runat="server" Text='<%#Eval("MELANGE") %>' ToolTip='<%#Eval("MELANGE") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            <asp:TemplateField HeaderText="YARNSHADE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                              PRIMARY TPM
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtYARNSHADE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtYARNSHADE" runat="server" ServiceMethod="AutoYarnYARNSHADE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtYARNSHADE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnYARNSHADE1" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblYARNSHADE" runat="server" Text='<%#Eval("PRIMARY_TPM") %>' ToolTip='<%#Eval("PRIMARY_TPM") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            <asp:TemplateField HeaderText="ENDUSE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               PRIMARY TD
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtENDUSE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtENDUSE" runat="server" ServiceMethod="AutoYarnENDUSE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtENDUSE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnENDUSE1" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblENDUSE" runat="server" Text='<%#Eval("PRIMARY_TD") %>' ToolTip='<%#Eval("PRIMARY_TD") %>'></asp:Label>
                                </ItemTemplate>
                              </asp:TemplateField>
                            
                            <asp:TemplateField Visible=false HeaderText="TARIFFHEADING">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               TWIST PLY
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtTARIFFHEADING" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtTARIFFHEADING" runat="server" ServiceMethod="AutoYarnTARIFFHEADING"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtTARIFFHEADING" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnTARIFFHEADING" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblTARIFFHEADING" runat="server" Text='<%#Eval("TWIST_PLY") %>' ToolTip='<%#Eval("TWIST_PLY") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible=false HeaderText="SALESITCHSCODE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               SECONDARY TPM
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtSALESITCHSCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtSALESITCHSCODE" runat="server" ServiceMethod="AutoYarnSALESITCHSCODE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtSALESITCHSCODE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnSALESITCHSCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblSALESITCHSCODE" runat="server" Text='<%#Eval("SECONDARY_TPM") %>' ToolTip='<%#Eval("SECONDARY_TPM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField Visible=false HeaderText="CUSTOMITCHSCODE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                              SECONDARY TD
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtCUSTOMITCHSCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtCUSTOMITCHSCODE" runat="server" ServiceMethod="AutoYarnCUSTOMITCHSCODE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtCUSTOMITCHSCODE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnCUSTOMITCHSCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblCUSTOMITCHSCODE" runat="server" Text='<%#Eval("SECONDARY_TD") %>' ToolTip='<%#Eval("SECONDARY_TD") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            
                            
                            
                               <asp:TemplateField HeaderText="denier">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                HSN Code
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtHSNCODE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtHSNCODE" runat="server" ServiceMethod="AutoYarntxtHSNCODE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtHSNCODE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnHSNCODE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblHsnCode" runat="server" Text='<%#Eval("HSN_CODE") %>' ToolTip='<%#Eval("HSN_CODE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                           
                            <asp:TemplateField HeaderText="rgb">
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td colspan="2">
                                                UOM
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtUOM" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtUOM" runat="server" ServiceMethod="AutoYarntxtUOM"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtUOM" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnrgb" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblUom" runat="server" Text='<%#Eval("UOM") %>' ToolTip='<%#Eval("UOM") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                       <asp:TemplateField HeaderText="ISEXCISABLE">
                                <HeaderTemplate>
                                    <table width="100%">
                                        <tr>
                                            <td colspan="2">
                                               QC_REQUIRED
                                            </td>
                                        </tr>
                                        <tr>
                                            <td width="80%">
                                                <asp:TextBox ID="txtISEXCISABLE" Width="50px" Height="10px" runat="server" Font-Size="X-Small"></asp:TextBox>
                                                <cc1:AutoCompleteExtender ID="AutoCompletetxtISEXCISABLE" runat="server" ServiceMethod="AutoYarnISEXCISABLE"
                                                    ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                                                    CompletionSetCount="1" TargetControlID="txtISEXCISABLE" FirstRowSelected="false">
                                                </cc1:AutoCompleteExtender>
                                            </td>
                                            <td width="20%">
                                                <asp:ImageButton ID="btnISEXCISABLE" CssClass="SmallFont" runat="server" ImageUrl="~/CommonImages/Icons/search.png"
                                                    Text="Go" OnClick="FilterGrid_Click1" Height="16px" Width="16px" />
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                   <asp:Label ID="lblISEXCISABLE" runat="server" Text='<%#Eval("QC_REQUIRED") %>' ToolTip='<%#Eval("QC_REQUIRED") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Base Quality &nbsp;Detail........." ItemStyle-HorizontalAlign="Right">
                                           <ItemTemplate>
                                          <asp:GridView ID="grdBaseQuality" runat="server" AutoGenerateColumns="False"  Width="450px">
                                             <Columns>
                                                      
                                                  <asp:TemplateField HeaderText="Category&nbsp;" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProductCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PRODUCT_TYPE") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="YARN&nbsp;CODE" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYarnCode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Quality  &nbsp;Description" ItemStyle-HorizontalAlign="Right" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYarnDesc"  runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("QUALITY_YARN_DESC") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="UOM" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUOM" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("UOM") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText=" SHADE" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShade" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_SHADE") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Percentage" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPercentage" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Percentage") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                
                                        </Columns>  
                                        <HeaderStyle  Font-Bold="True"  HorizontalAlign="Center"     VerticalAlign="Top" />                                   
                                    </asp:GridView>
                                 <%--<asp:Label  ID="lblPTotal" runat="server" ReadOnly="true" Text='<%# Bind("YARN_CODE") %>'   Font-Bold="true"   ></asp:Label>--%>
                                           </ItemTemplate>
                                       
                                          </asp:TemplateField>
                                          
                                          
                                           <asp:TemplateField HeaderText="Display Yarn &nbsp;Quality" ItemStyle-HorizontalAlign="left">
                                           <ItemTemplate>
                                          <asp:GridView ID="grdDisplayQuality" runat="server" AutoGenerateColumns="False"  >
                                             <Columns>
                                                  <asp:TemplateField HeaderText="Yarn&nbsp;Code" ItemStyle-HorizontalAlign="left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYarnCode1" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Display Quality" ItemStyle-HorizontalAlign="Right"  >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDisplayQuality"  Width="250px" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("Display_Quality") %>'></asp:Label>
                                                    </ItemTemplate>                                                                                                    
                                                </asp:TemplateField>
                                        </Columns>  
                                        <HeaderStyle  Font-Bold="True"  HorizontalAlign="Center"     VerticalAlign="Top" />                                   
                                    </asp:GridView>
                                      <%--<asp:Label  ID="lblYARNCODE" runat="server" ReadOnly="true" Text='<%# Bind("YARN_CODE") %>'   Font-Bold="true"   ></asp:Label>--%>
                                           </ItemTemplate>
                                          </asp:TemplateField>
                            
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                            VerticalAlign="Middle" />
                    </asp:GridView>
                    </div>
                </td>
            </tr>
        </td>
    </tr>
</table>

  <%-- </ContentTemplate>
</asp:UpdatePanel>--%>
<style type="text/css">
{
    width:700px;
    overflow:scroll;
}

</style>