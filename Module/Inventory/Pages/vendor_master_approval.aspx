<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/CommonMaster/admin.master"
    CodeFile="vendor_master_approval.aspx.cs" Inherits="Module_Inventory_Pages_vendor_master_approval" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc3" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphBody">



    <script src="../../../javascript/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
        $("[id*=hchkApproved]").live("click", function() {
            var hchkApproved = $(this);
            var grid = $(this).closest("table");
            var chkApproved = $("[id*=chkApproved]", grid);
            $(chkApproved, grid).each(function() {
                if (hchkApproved.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                    chkApproved.addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");

                }
            });
        });
        $("[id*=chkApproved]").live("click", function() {
            var grid = $(this).closest("table");
            var hchkApproved = $("[id*=hchkApproved]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                hchkApproved.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkApproved]", grid).length == $("[id*=chkApproved]:checked", grid).length) {
                    hchkApproved.attr("checked", "checked");
                }
            }
        });

        $("[id*=hchkReject]").live("click", function() {
            var hchkReject = $(this);
            var grid = $(this).closest("table");
            var chkReject = $("[id*=chkReject]", grid);
            $(chkReject, grid).each(function() {
                if (hchkReject.is(":checked")) {
                    $(this).attr("checked", "checked");
                    $("td", $(this).closest("tr")).addClass("selected");
                    chkReject.addClass("selected");
                } else {
                    $(this).removeAttr("checked");
                    $("td", $(this).closest("tr")).removeClass("selected");

                }
            });
        });
        $("[id*=chkReject]").live("click", function() {
            var grid = $(this).closest("table");
            var hchkReject = $("[id*=hchkReject]", grid);
            if (!$(this).is(":checked")) {
                $("td", $(this).closest("tr")).removeClass("selected");
                hchkReject.removeAttr("checked");
            } else {
                $("td", $(this).closest("tr")).addClass("selected");
                if ($("[id*=chkReject]", grid).length == $("[id*=chkReject]:checked", grid).length) {
                    hchkReject.attr("checked", "checked");
                }
            }
        });
    </script>

    <%--<asp:UpdatePanel ID="UpdatePanel4531" runat="server">
        <ContentTemplate>--%>
            <table width="100%" class="tContentArial" align="left">
                <tr width="100%">
                    <td class="td">
                        <table>
                            <caption>
                                <tr>
                                    <td id="tdUpdate" runat="server" align="left">
                                        <asp:ImageButton ID="imgbtnUpdate" runat="server" Height="41" ImageUrl="~/CommonImages/edit1.jpg"
                                            OnClick="imgbtnUpdate_Click" ToolTip="Update" ValidationGroup="M1" Width="48" />
                                    </td>
                                    <td id="tdFind" runat="server" align="left" visible="false">
                                        <asp:ImageButton ID="imgbtnFindTop" runat="server" Height="41" ImageUrl="~/CommonImages/link_find.png"
                                            ToolTip="Find" Width="48" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnPrint" runat="server" Height="41" ImageUrl="~/CommonImages/link_print.png"
                                            OnClick="imgbtnPrint_Click" ToolTip="Print" Width="48" />
                                        &nbsp;
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnClear" runat="server" Height="41" ImageUrl="~/CommonImages/clear.jpg"
                                            ToolTip="Clear" Width="48" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnExit" runat="server" Height="41" ImageUrl="~/CommonImages/link_exit.png"
                                            OnClick="imgbtnExit_Click" ToolTip="Exit" Width="48" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgbtnHelp" runat="server" Height="41" ImageUrl="~/CommonImages/link_help.png"
                                            ToolTip="Help" Width="48" />
                                    </td>
                                </tr>
                            </caption>
                        </table>
                    </td>
                </tr>
                </tr>
            </table>
            <table width="100%" >
                <tr>
                    <td align="center" valign="top" class="tRowColorAdmin td">
                        <span class="titleheading">Party/Vendor Aproval List</span>
                    </td>
                </tr>
            </table>
            <table class="tContentArial">
                <tr>
                    <td align="right" width="25%">
                        Party&nbsp;Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtPartyName" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                        <cc3:AutoCompleteExtender ID="AutoCompletetxtPartyName" runat="server" ServiceMethod="AutoYarntxtPartyNameL"
                            ServicePath="~/MOM.asmx" MinimumPrefixLength="1" CompletionInterval="1" EnableCaching="false"
                            CompletionSetCount="1" TargetControlID="txtPartyName" FirstRowSelected="false">
                        </cc3:AutoCompleteExtender>
                    </td>
                    <td align="right" width="10%">
                        Party&nbsp;City:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlprtycity" runat="server" CssClass="tContentArial" Width="165px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Group&nbsp;Code:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlvendorcode" runat="server" CssClass="tContentArial" Width="160px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Category:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlcategory" runat="server" CssClass="tContentArial" Width="165px">
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Pin&nbsp;Code:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:TextBox ID="txtpincode" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="10%">
                        Status:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlstatus" runat="server" CssClass="tContentArial" Width="160px">
                         <asp:ListItem Selected="True" Value="">All</asp:ListItem>
                        <asp:ListItem Value="1">APPROVED</asp:ListItem>
                        <asp:ListItem Value="0">PENDING</asp:ListItem>
                         <asp:ListItem Value="3">REJECTED</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Credit&nbsp;Limit:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:TextBox ID="txtCredit" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                    </td>
                    <td align="right" width="10%">
                        Region:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:DropDownList ID="ddlregion" runat="server" CssClass="tContentArial" Width="160px">
                            <asp:ListItem Value="">--Select Region--</asp:ListItem>
                            <asp:ListItem>East</asp:ListItem>
                            <asp:ListItem>West</asp:ListItem>
                            <asp:ListItem>North</asp:ListItem>
                            <asp:ListItem>South</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        Mobile&nbsp;No:
                    </td>
                    <td class="tdLeft" width="5%">
                        <asp:TextBox ID="txtmobile" runat="server" CssClass="tContentArial" Width="160px"
                            Height="14px"></asp:TextBox>
                    </td>
                    <td align="right" width="10%">
                        Search:
                    </td>
                    <td align="left" valign="top" width="10%">
                        <asp:Button ID="btngetdata" runat="server" Text="Get Data" Height="22px" Width="85"
                           OnClick="btngetdata_Click"  CssClass="AButton" />
                    </td>
                </tr>
            </table>
            <table>
            <tr>
                <td align="center" class="td" width="100%">
                    <asp:Label ID="lblMessage" runat="server" CssClass="csslblMessage"></asp:Label>
                    <asp:Label ID="lblErrorMessage" runat="server" CssClass="UserError"></asp:Label>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <b>Total Record&nbsp;&nbsp;:&nbsp;&nbsp;<asp:Label ID="lblTotalRecord" runat="server"></asp:Label>
                    </b>
                </td>
            </tr>
            <tr>
                <td align="left" class="td" width="100%">
                    <asp:Panel ID="pnlShowHover" runat="server" ScrollBars="Auto" Width="1000px">
                        <asp:GridView ID="gvPartyList" runat="server" AllowPaging="true" AllowSorting="True"
                            AutoGenerateColumns="False" BorderStyle="Ridge" CellPadding="3" Font-Names="Arial"
                            Font-Size="X-Small" ForeColor="#333333" GridLines="Both" HeaderStyle-Font-Bold="true"
                            OnPageIndexChanging="gvPartyList_PageIndexChanging" PageSize="10">
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <RowStyle BackColor="#EFF3FB" />
                            <EmptyDataRowStyle Font-Bold="True" Font-Size="Medium" />
                            <Columns>
                                <asp:TemplateField HeaderText="Party/Vendor">
                                    <ItemTemplate>
                                        <asp:Label ID="lblParty" runat="server" Text='<%# Bind("PRTY_NAME") %>' ToolTip='<%# Bind("PRTY_CODE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>                                
                              
                               
                                                           
                                <asp:BoundField DataField="PRTY_GRP_CODE" HeaderText="Group" />
                                <asp:BoundField DataField="VENDOR_CAT_CODE" HeaderText="Category" />
                                <asp:BoundField DataField="PRTY_ADD1" HeaderText="Address1" />
                                <asp:BoundField DataField="PRTY_ADD2" HeaderText="Address2" />
                                <asp:BoundField DataField="PRTY_CITY" HeaderText="City" />
                                <asp:BoundField DataField="PRTY_STATE" HeaderText="State" />
                                <asp:BoundField DataField="PIN_CODE" HeaderText="Pin Code" />
                                <asp:BoundField DataField="COUNTRY" HeaderText="Country" />
                                <asp:BoundField DataField="PHONE" HeaderText="Phone" />
                                <asp:BoundField DataField="FAX" HeaderText="Adhar Card" />
                                <asp:BoundField DataField="EMAIL" HeaderText="Email" />
                                <asp:BoundField DataField="PRTY_PANNO" HeaderText="PAN No" />
                                <%--   <asp:ImageField DataImageUrlField="PAN_IMAGE_PATH"  HeaderText="PAN Image"  ItemStyle-Width="50px" ControlStyle-Width="100" ></asp:ImageField>--%>
                                <asp:BoundField DataField="PRTY_LSTNO" HeaderText="LST No" />
                                <%-- <asp:ImageField DataImageUrlField="LST_IMAGE_PATH"  HeaderText="LST Image"   ItemStyle-Width="50px" ControlStyle-Width="100" ></asp:ImageField>--%>
                                <asp:BoundField DataField="PRTY_LSTDT" HeaderText="LST Date" />
                                <asp:BoundField DataField="PRTY_CSTNO" HeaderText="CST No" />
                                <%-- <asp:ImageField DataImageUrlField="CST_IMAGE_PATH"  HeaderText="CST Image"  ItemStyle-Width="50px" ControlStyle-Width="100" ></asp:ImageField>--%>
                                <asp:BoundField DataField="PRTY_CSTDT" HeaderText="CST Date" />
                                <asp:BoundField DataField="PRTY_TINNO" HeaderText="TIN No" />
                                <asp:BoundField DataField="PRTY_TINDT" HeaderText="TIN Date" />
                                <asp:BoundField DataField="TIN_TYPE" HeaderText="TIN Type" />
                                <asp:BoundField DataField="MOB_NO" HeaderText="Mobile" />
                                <asp:BoundField DataField="WEBSITE" HeaderText="Website" />
                                <asp:BoundField DataField="PRODUCT" HeaderText="Product" />
                                <asp:BoundField DataField="STAX_TYPE" HeaderText="Sales Tax Type" />
                                <asp:BoundField DataField="INS_POLICY_NO" HeaderText="Insurance Policy" />
                                <asp:BoundField DataField="USER_NAME" HeaderText="Created By" />
                                <asp:BoundField DataField="TDATE" HeaderText="Created Date" />
                                <asp:BoundField DataField="SERVICE_TAX_NO" HeaderText="Service Tax No" />
                                <asp:BoundField DataField="BANK_NAME" HeaderText="Bank Name" />
                                <%--  <asp:BoundField DataField="BRANCH" HeaderText="Branch" />--%>
                                <asp:BoundField DataField="ACCOUNT_NO" HeaderText="Account No" />
                                <asp:BoundField DataField="NEFT_RTGS_CODE" HeaderText="NEFT/RTGS" />
                                
                                <%--<asp:BoundField DataField="ECC_NO" HeaderText="ECC No" />--%>
                                <asp:BoundField DataField="CR_LIMIT" HeaderText="Credit Limit" />
                                <asp:BoundField DataField="REGION" HeaderText="Region" />
                                <asp:BoundField DataField="REMARKS" HeaderText="Remarks" />
                                <asp:TemplateField HeaderText="Confirm">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="hchkApproved" runat="server" Text="Confirm" TextAlign="Left" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkApproved" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Reject">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="hchkReject" runat="server" Text="Reject" TextAlign="Left" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkReject" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Conf Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="TextBox SmallFont" Width="100px"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--     
                        <asp:BoundField DataField="PERSON1_NAME" HeaderText="Item Description" />                                    
<asp:BoundField DataField="PRTY_STATUS" HeaderText="Item Description" />
   <asp:BoundField DataField="PERSON2_NAME" HeaderText="Item Description" />
                                      <asp:BoundField DataField="PERSON1_DESIG" HeaderText="Item Description" />
                                       <asp:BoundField DataField="PERSON1_LL" HeaderText="Item Description" />
                                        <asp:BoundField DataField="PERSON1_MOB" HeaderText="Item Description" />
                                         <asp:BoundField DataField="PERSON1_EMAIL" HeaderText="Item Description" />                                         
                        <asp:BoundField DataField="PERSON2_DESIG" HeaderText="Item Description" />
                         <asp:BoundField DataField="PERSON2_LL" HeaderText="Item Description" />
                          <asp:BoundField DataField="PERSON2_MOB" HeaderText="Item Description" />
                           <asp:BoundField DataField="PERSON2_EMAIL" HeaderText="Item Description" />
                            <asp:BoundField DataField="BRANCH_CODE" HeaderText="Item Description" />--%>
                            </Columns>
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" HorizontalAlign="Center"
                                VerticalAlign="Middle" />
                        </asp:GridView>
                    </asp:Panel>
                    </table>
                </td>
            </tr>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
