<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Packing_Mst.ascx.cs" Inherits="Module_Inventory_Controls_Packing_Mst" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Src="../../../CommonControls/LOV/ItemLOV_Packing.ascx" TagName="ItemLOV_Packing"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript">   
    function Calculation(val)
    { 
      var Res;
      Res =Math.round(parseFloat(document.getElementById('ctl00_cphBody_Packing_Mst1_TxtPackWeight').value)-parseFloat(document.getElementById('ctl00_cphBody_Packing_Mst1_TxtItemNo').value)* parseFloat(document.getElementById('ctl00_cphBody_Packing_Mst1_TxtItemWeight').value)).toFixed(2);
      if(isNaN(Res))
       {  
         document.getElementById('ctl00_cphBody_Packing_Mst1_TxtPackingEmpty').value=''; 
       }
       else
       {
        document.getElementById('ctl00_cphBody_Packing_Mst1_TxtPackingEmpty').value=Res;
       } 
   }   
</script>
 <asp:UpdatePanel ID="uppnl" runat="server">
    <ContentTemplate>
<table align="left" class="tContentArial">
    <tr>
        <td class="td">
            <table align="left">
                <tr>
                    <td id="tdSave" runat="server">
                        <asp:ImageButton ID="imgbtnSave" runat="server" Width="48" Height="41" ToolTip="Save"
                            ValidationGroup="s" ImageUrl="~/CommonImages/save.jpg" TabIndex="10" OnClick="imgbtnSave_Click" />
                    </td>
                    <td id="tdUpdate" runat="server">
                        <asp:ImageButton ID="imgbtnUpdate" OnClick="imgbtnUpdate_Click" runat="server" ValidationGroup="M1"
                            ImageUrl="~/CommonImages/edit1.jpg" ToolTip="Update" Height="41" Width="48">
                        </asp:ImageButton>
                    </td>
                    <td id="tdFind" runat="server">
                        <asp:ImageButton ID="imgbtnFind" runat="server" ToolTip="Find" ImageUrl="~/CommonImages/link_find.png"
                            Width="48" Height="41" TabIndex="7" OnClick="imgbtnFind_Click"></asp:ImageButton>
                    </td>
                    <td id="tdDelete" runat="server">
                        <asp:ImageButton ID="imgbtnDelete" OnClick="imgbtnDelete_Click" runat="server" ImageUrl="~/CommonImages/del6.png"
                            OnClientClick="if (!confirm('Are you want to Delete ?')) { return false; }" ToolTip="Delete"
                            Height="41" Width="48"></asp:ImageButton>
                    </td>
                    <td id="tdClear" runat="server">
                        <asp:ImageButton ID="imgbtnClear" runat="server" Width="48" Height="41" ToolTip="Clear"
                            ImageUrl="~/CommonImages/clear.jpg" OnClick="imgbtnClear_Click"></asp:ImageButton>
                    </td>
                    <td id="tdPrint" runat="server">
                        <asp:ImageButton ID="imgbtnPrint" Width="48" Height="41" runat="server" ToolTip="Print"
                            ImageUrl="~/CommonImages/link_print.png"></asp:ImageButton>
                    </td>
                    <td id="tdHelp" runat="server">
                        <asp:ImageButton ID="imgbtnHelp" Width="48" Height="41" runat="server" ToolTip="Help"
                            ImageUrl="~/CommonImages/link_help.png"></asp:ImageButton>
                    </td>
                    <td id="tdExit" runat="server">
                        <asp:ImageButton ID="imgbtnExit" Width="48" Height="41" runat="server" ToolTip="Exit"
                            ImageUrl="~/CommonImages/link_exit.png" OnClick="imgbtnExit_Click"></asp:ImageButton>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TableHeader td" align="center">
            <b class="titleheading">Packing Master</b>
        </td>
    </tr>
    <tr>
        <td>
            <table class="td">
                <tr>
                    <td class="tdRight">
                        Paking Code:
                    </td>
                    <td>
                        <asp:TextBox ID="TxtPackCode" runat="server" Width="60px" CssClass="TextBoxNo"></asp:TextBox>
                        <cc2:ComboBox ID="DDLPackCode" runat="server" AutoPostBack="True" EnableLoadOnDemand="true"
                            OnLoadingItems="DDLPackCode_LoadingItems" DataTextField="PCK_DESC" DataValueField="PCK_CODE"
                            Width="150px" MenuWidth="350px" Height="200px" CssClass="SmallFont" TabIndex="1"
                            EmptyText="Find Item" OnSelectedIndexChanged="DDLPackCode_SelectedIndexChanged">
                            <HeaderTemplate>
                                <div class="header c1">
                                    Code</div>
                                <div class="header c2">
                                    DESCRIPTION</div>
                                <div class="header c3">
                                    TYPE</div>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div class="item c1">
                                    <asp:Literal runat="server" ID="Container1" Text='<%# Eval("PCK_CODE") %>' /></div>
                                <div class="item c2">
                                    <asp:Literal runat="server" ID="Container2" Text='<%# Eval("PCK_DESC") %>' /></div>
                                <div class="item c3">
                                    <asp:Literal runat="server" ID="Container3" Text='<%# Eval("PACK_TYPE") %>' /></div>
                            </ItemTemplate>
                            <FooterTemplate>
                                Displaying items
                                <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                out of
                                <%# Container.ItemsCount %>.
                            </FooterTemplate>
                        </cc2:ComboBox>
                    </td>
                    <td class="tdRight">
                        Yarn Pack UOM :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLYarnPackUOM" runat="server" CssClass="tContentArial" Width="162px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Packing Type :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLPackingType" runat="server" CssClass="tContentArial" Width="162px"
                            AutoPostBack="True" OnSelectedIndexChanged="DDLPackingType_SelectedIndexChanged">
                            <asp:ListItem Value="0">----------------Select----------</asp:ListItem>
                            <asp:ListItem>Self</asp:ListItem>
                            <asp:ListItem>Binded</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Paking Desc.:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtPackDesc" Width="162px" runat="server"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        Unit Name:
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLUnit" runat="server" Width="162px" CssClass="tContentArial">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        Packing Weight:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtPackWeight" runat="server" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"
                            Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Base Packing Code :
                    </td>
                    <td class="tdLeft">
                        <asp:DropDownList ID="DDLBasepack" runat="server" CssClass="tContentArial" Width="162px">
                        </asp:DropDownList>
                    </td>
                    <td class="tdRight">
                        No Of Item.:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtItemNo" runat="server" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"
                            Width="160px"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        Each Item Length:
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtItemLength" runat="server" CssClass="TextBoxNo" onkeyup="pricevalidate(this);"
                            Width="160px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdRight">
                        Each Item Weight :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtItemWeight" runat="server" CssClass="TextBoxNo" onkeyup="javascript:Calculation(this.id)"
                            onKeyPress="pricevalidate(this);" Width="160px"></asp:TextBox>
                    </td>
                    <td class="tdRight">
                        Packing Empty Weight :
                    </td>
                    <td class="tdLeft">
                        <asp:TextBox ID="TxtPackingEmpty" runat="server" CssClass="TextBoxNo" Width="160px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <table width="100%%">
                <tr bgcolor="#336699" class="SmallFont titleheading">
                    <td>
                        Item Code
                    </td>
                    <td>
                        Description
                    </td>
                    <td>
                        Qty
                    </td>
                    <td>
                        Remarks
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc1:ItemLOV_Packing ID="cmbPOITEM"  runat="server" />                        
                    </td>
                    <td>
                        <asp:TextBox ID="txtDESC" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                            ReadOnly="true" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtQTY" runat="server" CssClass="TextBoxNo SmallFont" onkeyup="pricevalidate(this);"
                            Width="100px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtDetRemarks" runat="server" CssClass="TextBox SmallFont" Width="200px"></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnsaveTRNDetail" runat="server" CssClass="SmallFont" OnClick="btnsaveTRNDetail_Click"
                            Text="Save" />
                        <asp:Button ID="btnTRNCancel" runat="server" CssClass="SmallFont" OnClick="btnTRNCancel_Click"
                            Text="Cancel" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td width="100%" class="td">
            <asp:Panel ID="pnlGrid" runat="server" Width="100%">
                <asp:GridView ID="GridViewItemTrn" runat="server" AutoGenerateColumns="False" CssClass="SmallFont"
                    Width="99%" ShowFooter="false" OnRowCommand="GridViewItemTrn_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="txtICODE" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_CODE") %>'
                                    ReadOnly="True" Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                <asp:Label ID="txtDESC" runat="server" CssClass="Label SmallFont" Text='<%# Bind("ITEM_DESC") %>'
                                    ReadOnly="True" Width="200px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity">
                            <ItemStyle HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:Label ID="txtQTY" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("ITEM_QTY") %>'
                                    ReadOnly="True" Width="60px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="txtDetRemarks" runat="server" ReadOnly="True" CssClass="Label SmallFont"
                                    Text='<%# Bind("REMARKS") %>' Width="120px"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="EditItem" Text="Edit"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                                /
                                <asp:LinkButton ID="lnkbtnDel" runat="server" CommandName="DelItem" Text="Delete"
                                    CommandArgument='<%# Bind("UNIQUEID") %>'></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="SmallFont" />
                    <HeaderStyle CssClass="SmallFont titleheadingGrid" BackColor="#336699" />
                </asp:GridView>
            </asp:Panel>
        </td>
    </tr>
</table>
 </ContentTemplate>
    </asp:UpdatePanel>