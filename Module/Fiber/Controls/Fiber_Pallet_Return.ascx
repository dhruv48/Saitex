<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Fiber_Pallet_Return.ascx.cs"
    Inherits="Module_Fiber_Controls_Fiber_Pallet_Return" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">

    function Calculation(val) {
        document.getElementById('ctl00_cphBody_ReceiptCredit1_txtAmount').value = (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtFinalRate').value) * (parseFloat(document.getElementById('ctl00_cphBody_ReceiptCredit1_txtQTY').value))).toFixed(4);
    }
</script>

<style type="text/css">
    .item
    {
        position: relative !important;
        display: -moz-inline-stack;
        display: inline-block;
        zoom: 1; 
        *display:inline;
        overflow:hidden;
        white-space:nowrap;
    }
    
    .header
    {
        margin-left: 2px;
    }
    .c1
    {
        width: 100px;
    }
    .c3
    {
        width: 350px;
        margin-left: 2px;        
    }
    .c2
    {
        margin-left: 2px;
        width: 150px;
    }
    .c4
    {
        margin-left: 35%;
        width: 80px;
    }
    .c5
    {
        margin-left: 4px;
        width: 340px;
    }
    .c6
    {
        margin-left: 4px;
        width: 150px;
    }
   
</style>
<table class="tdMain tContentArial" width="900px">
    <tr>
        <td class="td" width="100%">
            <table class="tContentArial">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" ImageUrl="~/CommonImages/save.jpg"
                            TabIndex="17" Style="height: 41px" ToolTip="Save" ValidationGroup="gg" 
                            onclick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/CommonImages/edit1.jpg"
                            ToolTip="Update" ValidationGroup="M1" onclick="imgbtnUpdate_Click" />
                    </td>
                    
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ImageUrl="~/CommonImages/link_find.png"
                            TabIndex="18" ToolTip="Find" onclick="imgbtnFind_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnClear" runat="server" ImageUrl="~/CommonImages/clear.jpg"
                            TabIndex="19" ToolTip="Clear" onclick="imgbtnClear_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnPrint" runat="server" ImageUrl="~/CommonImages/link_print.png"
                            TabIndex="20" ToolTip="Print" onclick="imgbtnPrint_Click" />
                    </td>
                     <td id="tdaddlist" runat="server">
                        <asp:ImageButton ID="imgbtnaddlist" runat="server" ImageUrl="~/CommonImages/list.jpg"
                            ToolTip="Poy Master List" onclick="imgbtnaddlist_Click"  />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnExit" runat="server" ImageUrl="~/CommonImages/link_exit.png"
                            TabIndex="21" ToolTip="Exit" OnClick="imgbtnExit_Click" />
                    </td>
                    <td>
                        <asp:ImageButton ID="imgbtnHelp" runat="server" ImageUrl="~/CommonImages/link_help.png"
                            TabIndex="22" ToolTip="Help" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="td tdLeft">
            <span class="Mode">
                <asp:Label ID="lblMode" runat="server"></asp:Label>
            </span>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                ShowSummary="False" />
        </td>
    </tr>
    <tr>
        <td align="center" class="TableHeader td" width="100%">
            <b class="titleheading">Pallet Return </b>
        </td>
    </tr>
    <tr>
        <td class="td SmallFont" width="100%">
            <table width="100%">
                <tr>
                   
                    <td valign="top" align="right" width="17%">
                            <asp:Label ID="Label2" runat="server" CssClass="Label SmallFont" Text="Challan Number : "></asp:Label>
                        </td>
                    <td class="td tdLeft" valign="top" width="17%">
                            <asp:TextBox ID="txtChallanNumber" TabIndex="1" runat="server" Width="150px" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                Font-Bold="True" ></asp:TextBox>
                            <cc2:ComboBox ID="ddlChallanNumber" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                DataTextField="ISSUE_TRN_NUMB" DataValueField="ISSUE_TRN_NUMB" Width="150px" MenuWidth="500px"
                                Height="200px" CssClass="SmallFont" TabIndex="1" EmptyText="Find Challan Number"
                                OnLoadingItems="ddlchallannumber_LoadingItems" OnSelectedIndexChanged="ddlchallanNumber_SelectedIndexChanged">
                                <HeaderTemplate>
                                    <div class="header c1">
                                        Challan Number</div>
                                    <div class="header c2">
                                        Challan Date</div>                                        
                                    <div class="header c2">
                                        Party </div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <asp:Literal runat="server" ID="Container1" Text='<%# Eval("ISSUE_TRN_NUMB") %>' /></div>
                                    <div class="item c2">
                                        <asp:Literal runat="server" ID="Literal1" Text='<%# Eval("ISSUE_TRN_DATE") %>' /></div>
                                         <div class="item c2">
                                        <asp:Literal runat="server" ID="Literal2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Displaying items
                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                    out of
                                    <%# Container.ItemsCount %>.
                                </FooterTemplate>
                            </cc2:ComboBox>
                        </td>
                 
                    <td align="right" valign="top" width="17%">
                        <asp:Label ID="Label16" runat="server" CssClass="Label SmallFont" Text="Challan Date : "></asp:Label>
                    </td>
                    <td align="left" valign="top" width="17%">
                        <asp:TextBox ID="txtchallandate" runat="server" TabIndex="3" Width="150px" CssClass="TextBox SmallFont"
                            MaxLength="15"></asp:TextBox>
                    </td>
                </tr>
               
               
                        <tr>
                            <td valign="top" align="right" width="17%">
                                <asp:Label ID="Label1" runat="server" CssClass="LabelNo" Text="Party Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="17%">
                                <cc2:ComboBox ID="txtPartyCode" TabIndex="4" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                                    DataTextField="PRTY_CODE" DataValueField="Address" EmptyText="Select Vendor"
                                    EnableVirtualScrolling="true" Width="150px" MenuWidth="500px" Height="200px"
                                    OnLoadingItems="txtPartyCode_LoadingItems" OnSelectedIndexChanged="txtPartyCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c3">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="65%" colspan="4">
                                <asp:TextBox ID="txtPartyCode1" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="25%"></asp:TextBox>
                                <asp:TextBox ID="txtPartyAddress" TabIndex="5" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="70%"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="right" width="16%" class="style5">
                                <asp:Label ID="Label5" runat="server" CssClass="LabelNo" Text="Transporter Code :"></asp:Label>
                            </td>
                            <td valign="top" align="left" width="15%" class="style5">
                                <cc2:ComboBox ID="txtTransporterCode" TabIndex="6" runat="server" AutoPostBack="True"
                                    EnableLoadOnDemand="true" DataTextField="PRTY_CODE" DataValueField="Address"
                                    EnableVirtualScrolling="true" Width="150px" EmptyText="Select transporter" MenuWidth="500px"
                                    Height="200px" OnLoadingItems="txtTransporterCode_LoadingItems" OnSelectedIndexChanged="txtTransporterCode_SelectedIndexChanged">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            Code</div>
                                        <div class="header c3">
                                            NAME</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PRTY_CODE") %>' /></div>
                                        <div class="item c3">
                                            <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PRTY_NAME") %>' /></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top" align="left" width="69%" colspan="4" class="style5">
                                <asp:TextBox ID="txtTransporterCode1" TabIndex="7" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="25%"></asp:TextBox>
                                <asp:TextBox ID="txtTransporterName" TabIndex="7" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    ReadOnly="True" Width="70%"></asp:TextBox>
                            </td>
                        </tr>
                   
                <tr>
                <td>
                &nbsp;
                </td>
                </tr>
                    <tr bgcolor="#336699" class="SmallFont titleheading">
                        <td  width="12%">
                        MRN/MERGE/PALLET                
                            </td>
                            <td  width="12%">
                                MRN&nbsp;NO
                            </td>
                            <td  width="12%">
                                MRN&nbsp;TYPE
                            </td>
                            <td  width="12%">
                                MERGE&nbsp;NO
                            </td>
                            <td  width="12%">
                                PALLET&nbsp;CODE
                            </td>
                        
                        <td  width="12%">
                           PALLET&nbsp;ADJUST
                        </td>
                        <td  width="12%">
                           NO&nbsp;OF&nbsp;PALLET
                        </td>
                        <td  width="12%">                            
                            
                        </td>
                    </tr>
                    <tr>
                        <td  width="12%">
                            <cc2:ComboBox ID="cmbPalletRec" runat="server" AutoPostBack="True" CssClass="smallfont"
                                EnableLoadOnDemand="True" DataTextField="TRN_NUMB" DataValueField="TRN_NUMB"
                                MenuWidth="500px" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="11" Width="100PX"
                                Visible="true" Height="200px" OnLoadingItems="cmbPalletRec_LoadingItems" OnSelectedIndexChanged="cmbPalletRec_SelectedIndexChanged">
                                <HeaderTemplate>
                                    <div class="header c1">
                                        MRN NO</div>
                                    <div class="header c1">
                                        MRN TYPE</div>
                                        <div class="header c1">
                                             MERGE NO</div>
                                    <div class="header c1">
                                        PALLET CODE</div>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="item c1">
                                        <%# Eval("TRN_NUMB")%></div>
                                    <div class="item c1">
                                        <%# Eval("TRN_TYPE")%></div>
                                        <div class="item c1">
                                        <%# Eval("MERGE_NO")%></div>
                                    <div class="item c1">
                                        <%# Eval("PALLET_CODE")%></div>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Displaying items
                                    <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                    out of
                                    <%# Container.ItemsCount %>.
                                </FooterTemplate>
                            </cc2:ComboBox>
                        </td>
                        <td  width="12%">
                            <asp:TextBox ID="txtmrnno" runat="server" TabIndex="13" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td  width="12%">
                            <asp:TextBox ID="txtmencode" runat="server" TabIndex="13" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td  width="12%">
                            <asp:TextBox ID="txtMergeNo" runat="server" TabIndex="13" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td  width="12%">
                            <asp:TextBox ID="txtpalletcode" runat="server" TabIndex="13" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td  width="12%">
                            <asp:Button ID="btnSubDetail" runat="server" Font-Size="8pt" TabIndex="12" Text="Adj"
                                Width="95%" OnClick="btnSubDetail_Click" />
                        </td>
                        <td  width="12%">
                            <asp:TextBox ID="txtQTY" runat="server" TabIndex="13" CssClass="TextBoxNo TextBoxDisplay SmallFont"
                                Width="95%"></asp:TextBox>
                        </td>
                        <td  width="12%">
                            <asp:Button ID="Savebtn" runat="server" Font-Size="8pt" TabIndex="12" Text="Save"
                                Width="60PX" onclick="Savebtn_Click"  />
                        </td>
                        </tr>
                        <tr>
                            <td width="100%" class="td" colspan="8">
                               <%-- <asp:Panel ID="pnlfiberpartyreturnGrid" runat="server" Width="100%">--%>
                                    <asp:GridView ID="grdpallletrtnsave" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                                        Width="99%" ShowFooter="false" onrowcommand="grdpallletrtnsave_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="MRN NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmrnno" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TRN_NUMB") %>'
                                                        ReadOnly="True" Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRN TYPE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmrntype" runat="server" CssClass="Label SmallFont" Text='<%# Bind("TRN_TYPE") %>'
                                                        ReadOnly="True" Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MERGE NO">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblmergeno" runat="server" CssClass="Label SmallFont" Text='<%# Bind("MERGE_NO") %>'
                                                        ReadOnly="True" Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PALLET CODE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpalletcode" runat="server" CssClass="Label SmallFont" Text='<%# Bind("PALLET_CODE") %>'
                                                        ReadOnly="True" Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NO OF PALLET">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpalletno" runat="server" CssClass="Label SmallFont" Text='<%# Bind("NO_OF_PALLET") %>'
                                                        ReadOnly="True" Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="REMARKS" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblremarks" runat="server" CssClass="Label SmallFont" Text='<%# Bind("REMARKS") %>'
                                                        ReadOnly="True" Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                               <ItemTemplate>
                                                  <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                                         CommandArgument='<%# Bind("UNIQUE_ID") %>'></asp:LinkButton>
                                                          /
                                                  <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                                       CommandArgument='<%# Bind("UNIQUE_ID") %>'></asp:LinkButton>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                        </Columns>
                                        <RowStyle CssClass="SmallFont" />
                                        <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                                    </asp:GridView>
                                <%--</asp:Panel>--%>
                            </td>
                        </tr>
               
              
            </table>
           
        </td>
        </tr>
        </table>
<cc1:CalendarExtender ID="ceIssueDate" runat="server" Format="dd/MM/yyyy" TargetControlID="txtchallandate">
</cc1:CalendarExtender>
