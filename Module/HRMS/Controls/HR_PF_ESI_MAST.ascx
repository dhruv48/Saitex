<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HR_PF_ESI_MAST.ascx.cs"
    Inherits="Module_HRMS_Controls_HR_PF_ESI_MAST" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript">
    function filterNonNumeric(field) {
var result = new String();
var numbers = "0123456789.";
var chars = field.value.split(""); // create array 
for (i = 0; i < chars.length; i++) {
if (numbers.indexOf(chars[i]) != -1) result += chars[i];
}
if (field.value != result) field.value = result;
}
                  
                        
</script>

<style type="text/css">
    fieldset
    {
        width: 98%;
        -moz-border-radius: 10px;
        position: relative;
    }
</style>
<%--<asp:UpdatePanel ID="UpdatePanel" runat="server">
    <ContentTemplate>--%>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtFromDate"
    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
    OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
    ErrorTooltipEnabled="True">
</cc1:MaskedEditExtender>
<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtTodate"
    Mask="99/99/9999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus"
    OnInvalidCssClass="MaskedEditError" MaskType="Date" InputDirection="LeftToRight"
    ErrorTooltipEnabled="True">
</cc1:MaskedEditExtender>
<table class="tContentArial" width="100%" align="left">
    <tr>
        <td valign="top" class="td" align="left">
            <table cellspacing="0" cellpadding="0" width="15%" align="left">
                <tr>
                    <td id="tdSave" valign="top" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnSave" TabIndex="9" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            ToolTip="Save" Height="41" Width="48" ValidationGroup="M1" OnClick="imgbtnSave_Click">
                        </asp:ImageButton>
                    </td>
                    <td id="tdUpdate" valign="top" visible="false" align="center" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" TabIndex="9" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" Height="41" Width="48" ValidationGroup="M1" 
                            onclick="imgbtnUpdate_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            ToolTip="Print" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            ToolTip="Clear" Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server" class="cl">
                        <asp:ImageButton ID="imgbtnExit" runat="server" ToolTip="Exit" ImageUrl="~/CommonImages/link_exit.png"
                            Width="48" Height="41" TabIndex="11" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                    <td valign="top" align="center">
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            ToolTip="Help" Height="41" Width="48"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader" align="center" width="100%">
            <b class="titleheading">PROVIDENT FUND / EMPLOYEE STATE INSURANCE DETAILS</b>
        </td>
    </tr>
    <tr>
        <td class="td" valign="top" align="left" width="100%">
            <span style="color: #ff0000">You are in &nbsp;<asp:Label ID="lblMode" runat="server"></asp:Label>&nbsp;Mode</span>
        </td>
    </tr>
    <tr>
        <td>
            <fieldset>
                <legend>PF/ESI Record</legend>
                <table class="td tContent" width="100%">
                    <tr>
                        <td class="tdRight">
                            Branch:
                        </td>
                        <td class="tdLeft">
                            <asp:DropDownList ID="DDLBranch" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="tdRight">
                            Subh Head:
                        </td>
                        <td class="tdLeft">
                            <asp:DropDownList ID="DDLSubhHead" Width="130px" CssClass="SmallFont TextBox UpperCase"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                        <td class="tdRight">
                            Frome Date:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtFromDate" CssClass="SmallFont TextBox" MaxLength="10" Width="130px"
                                runat="server"></asp:TextBox>
                        </td>
                        <td class="tdRight">
                            To Date:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtTodate" CssClass="SmallFont TextBox" MaxLength="10" Width="130px"
                                runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdRight">
                            EMP Contr:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtEmpContr" CssClass="SmallFont textboxno" onkeyup="pricevalidate(this);"
                                Width="130px" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdRight">
                            EMPR Contr:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtEmployerContr" CssClass="SmallFont textboxno" onkeyup="pricevalidate(this);"
                                Width="130px" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdRight">
                            Basic Sub Head Lmt:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtBasicSubhHeadLmt" CssClass="SmallFont textboxno" onkeyup="pricevalidate(this);"
                                Width="130px" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdRight">
                            DLI:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtDLI" CssClass="SmallFont textboxno" onkeyup="pricevalidate(this);"
                                Width="130px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdRight">
                            DLI Admin Charges:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtDLIAdminCharges" CssClass="SmallFont textboxno" onkeyup="pricevalidate(this);"
                                Width="130px" runat="server"></asp:TextBox>
                        </td>
                        <td class="tdRight">
                            Sub Head Lmt:
                        </td>
                        <td class="tdLeft">
                            <asp:TextBox ID="TxtSubhHeadLmt" CssClass="SmallFont textboxno" onkeyup="pricevalidate(this);"
                                Width="130px" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="7">
                            <fieldset>
                                <legend>DEPD Sub Head Detail</legend>
                                <asp:CheckBoxList ID="ChkCBLDEPDSubHead" RepeatColumns="6" RepeatDirection="Horizontal"
                                    runat="server">
                                </asp:CheckBoxList>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td align="center" valign="top" class="tRowColorAdmin">
            <span class="H3Heading">P.F. Detail</span>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <fieldset>
                <legend>PF/ESI Record:</legend>
                <asp:Panel ID="gridPanel" runat="server" ScrollBars="Vertical" Width="100%" Height="200px">
                    <asp:GridView ID="GVPFESIRecord" runat="server" AutoGenerateColumns="False" AllowSorting="True"
                        Font-Size="X-Small" CellPadding="3" GridLines="None" Width="100%" ForeColor="#333333"
                        CssClass="smallfont" EmptyDataText="No Record Found" OnRowCommand="GVPFESIRecord_RowCommand">
                        <FooterStyle Width="100%" BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <RowStyle BackColor="#EFF3FB" />
                        <EmptyDataRowStyle Font-Bold="True" Wrap="False" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl No." ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="top">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>                                
                            </asp:TemplateField>
                           
                                   
                            <asp:TemplateField HeaderText="BRANCH" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblBranch" runat="server" Text='<%# Eval("BRANCH_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SUBH HEAD" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblSubhHeadName" runat="server" Text='<%# Eval("SUBH_NAME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FROM DATE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblFromDate" runat="server" Text='<%# Bind("FROM_DATE","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TO DATE" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblTODate" runat="server" Text='<%# Bind("TO_DATE","{0:d}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BASIC SUBH LMT." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblBASE_SUBH_HEAD_LMT" runat="server" Text='<%# Eval("BASE_SUBH_HEAD_LMT") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMP.CONTR" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblEMP_CONTR" runat="server" Text='<%# Eval("EMP_CONTR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="EMPR.CONTR." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblEMPLR_CONTR" runat="server" Text='<%# Eval("EMPLR_CONTR") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DLI" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblDLI" runat="server" Text='<%# Eval("DLI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DLI ADMIN CHR." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblDLI_ADMIN_CHRG" runat="server" Text='<%# Eval("DLI_ADMIN_CHRG") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SUBH HEAD LMT." HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center"
                                ItemStyle-Width="50px">
                                <ItemTemplate>
                                    <asp:Label ID="LblSUBH_HEAD_LMT" runat="server" Text='<%# Eval("SUBH_HEAD_LMT") %>'></asp:Label>
                                     <asp:Label ID="LblBranchCode" Visible="false"  runat="server" Text='<%# Eval("BRANCH_CODE") %>'></asp:Label>
                                      <asp:Label ID="LblComp_Code" Visible="false"  runat="server" Text='<%# Eval("COMP_CODE") %>'></asp:Label>
                                    <asp:Label ID="LblSubhHeadId" Visible="false" runat="server" Text='<%# Eval("SUBH_HEAD_ID") %>'></asp:Label>                                
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemStyle HorizontalAlign="Center" Width="80px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" CommandName="EmpEdit" TabIndex="12"
                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>'></asp:LinkButton>&nbsp;/&nbsp;
                                    <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete" CommandName="EmpDelete"
                                        CommandArgument='<%# DataBinder.Eval(Container, "RowIndex") %>' TabIndex="12"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" BackColor="#507CD1"
                            ForeColor="White" Font-Bold="True" />
                    </asp:GridView>
                </asp:Panel>
            </fieldset>
        </td>
    </tr>
</table>
<%--  </ContentTemplate>
</asp:UpdatePanel>--%>