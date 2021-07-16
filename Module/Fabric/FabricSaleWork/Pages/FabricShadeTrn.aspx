<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FabricShadeTrn.aspx.cs" Inherits="Module_Fabric_FabricSaleWork_Pages_FabricShadeTrn" %>

<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Get Fabric Shade Trn</title>
    <link rel="stylesheet" type="text/css" href="../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">
        function BindRate(Amount, TextBoxId) {
            window.opener.document.getElementById(TextBoxId).value = Amount;
            window.opener.document.forms[0].submit();
            window.close();
        } 
    </script>

    <style type="text/css">
        .item
        {
            position: relative !important;
            display: -moz-inline-stack;
            display: inline-block;
            zoom: 1; *display:inline;overflow:hidden;white-space:nowrap;}
        .header
        {
            margin-left: 2px;
        }
        .c1
        {
            width: 100px;
        }
        .c2
        {
            margin-left: 4px;
            width: 200px;
        }
    </style>
</head>
<body bgcolor="#afcae4" class="tContentArial" >
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="pnlTRNDetail" runat="server" CssClass="pnlTrn">
                    <table class="tContentArial td" width="900px" >
                        <tr>
                            <td colspan="10" align="center" valign="top">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="10" valign="top">
                                Yarn
                                <asp:Label ID="lbltrnWarpweft" runat="server"></asp:Label>
                                &nbsp;Detail for Shade :
                                <asp:Label ID="lblSstrnHead" runat="server"></asp:Label>
                            </td>
                        </tr>
                       
                        <tr  >
                            <td bgcolor="#006699"  align ="left" width = "10%">
                            <asp:Label ID="Label1" runat="server" Text="Yarn Detail" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "10%">
                                
                                  <asp:Label ID="Label2" runat="server" Text="Yarn Shade" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "12%">
                              
                                  <asp:Label ID="Label3" runat="server" Text="  Shade RGB" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "9%">
                              
                                  <asp:Label ID="Label4" runat="server" Text="Yarn Count " ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "8%">
                                
                                  <asp:Label ID="Label5" runat="server" Text="Yarn STD" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "8%" >
                                
                                  <asp:Label ID="Label6" runat="server" Text="Req Qty" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "8%">
                                
                                  <asp:Label ID="Label7" runat="server" Text="Shrinkage" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "8%" >
                                
                                  <asp:Label ID="Label8" runat="server" Text="Wastage" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699" width = "8%">
                                
                                  <asp:Label ID="Label9" runat="server" Text="Rejection" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                            <td bgcolor="#006699">
                               
                                  <asp:Label ID="Label10" runat="server" Text="Qty." ForeColor="White" CssClass = "tContentArial"></asp:Label>
                            </td>
                        </tr>
                        <tr >
                            <td valign="top">
                                <cc2:ComboBox ID="txtShTrnCode" runat="server" AutoPostBack="True" CssClass="smallfont"
                                    DataTextField="YARN_CODE" DataValueField="YARN_DATA" EnableLoadOnDemand="true"
                                    MenuWidth="660" EnableVirtualScrolling="true" OpenOnFocus="true" TabIndex="9"
                                    Visible="true" Height="200px" OnLoadingItems="txtShTrnCode_LoadingItems" 
                                    OnSelectedIndexChanged="txtShTrnCode_SelectedIndexChanged" Width="100px">
                                    <HeaderTemplate>
                                        <div class="header c1">
                                            YARN CODE</div>
                                        <div class="header c2">
                                            YARN DESCRIPTION</div>
                                        <div class="header c1">
                                            TYPE</div>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="item c1">
                                            <%# Eval("YARN_CODE") %></div>
                                        <div class="item c2">
                                            <%# Eval("YARN_DESC") %></div>
                                        <div class="item c1">
                                            <%# Eval("YARN_TYPE")%></div>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        Displaying items
                                        <%# Container.ItemsCount > 0 ? "1" : "0" %>-<%# Container.ItemsLoadedCount %>
                                        out of
                                        <%# Container.ItemsCount %>.
                                    </FooterTemplate>
                                </cc2:ComboBox>
                            </td>
                            <td valign="top">
                                <asp:DropDownList ID="ddlTrnShade" runat="server" class="SmallFont" AppendDataBoundItems="True"
                                    OnSelectedIndexChanged="ddlTrnShade_SelectedIndexChanged" Width="100px">
                                </asp:DropDownList>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txtTrnShadeRGB" runat="server" AutoPostBack="True" CssClass="SmallFont"
                                    Width="60px" OnTextChanged="txtTrnShadeRGB_TextChanged"></asp:TextBox>
                                <asp:TextBox ID="txtTrnRGBColor" runat="server" CssClass="SmallFont" ReadOnly="True"
                                    Width="20px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnyarncount" runat="server" CssClass="SmallFont" Width="60px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnyarnstd" runat="server" CssClass="SmallFont" Width="60px"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnReqQty" runat="server" CssClass="SmallFont" Width="60px" 
                                    ontextchanged="txttrnReqQty_TextChanged"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnsrink" runat="server" CssClass="SmallFont" Width="50px" 
                                    ontextchanged="txttrnsrink_TextChanged"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnWastage" runat="server" CssClass="SmallFont" 
                                    Width="50px" ontextchanged="txttrnWastage_TextChanged"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnRejection" runat="server" CssClass="SmallFont" 
                                    Width="50px" ontextchanged="txttrnRejection_TextChanged"></asp:TextBox>
                            </td>
                            <td valign="top">
                                <asp:TextBox ID="txttrnQty" runat="server" CssClass="SmallFont" Width="50px"></asp:TextBox>
                                &nbsp;
                            </td>
                        </tr>
                        <tr class ="td">
                         <td bgcolor="#006699" class ="td">
                               
                                 <asp:Label ID="Label11" runat="server" Text=" Yarn Code" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                                </td>
                                <td>
                                <asp:TextBox ID="txttrnYarnCode" runat="server" CssClass="TextBox TextBoxDisplay SmallFont"
                                    Font-Bold="False" ReadOnly="True" TabIndex="10" Width="80px" 
                                        ></asp:TextBox>
                                    
                               </td>
                               <td bgcolor="#006699" class ="td">
                              
                                 <asp:Label ID="Label12" runat="server" Text="Yarn Desc" ForeColor="White" CssClass = "tContentArial"></asp:Label>
                                </td><td colspan ="2">
                                <asp:TextBox ID="txttrnyarnDesc" runat="server" CssClass="TextBox SmallFont TextBoxDisplay"
                                    ReadOnly="true" Width="150px"></asp:TextBox>
                           </td>
                            <td colspan = "5" align ="right" style="text-align: left" >
                                <asp:Button ID="btntrnSave" runat="server" OnClick="btntrnSave_Click" 
                                    Text="Save" Width="50px" />
                                <asp:Button ID="btntrncancel" runat="server" Text="Cancel" OnClick="btntrncancel_Click"
                                    Width="50px" />
                                <asp:Button ID="btnClose0" runat="server" OnClick="btnClose_Click1" 
                                    Text="Close" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="10">
                                <asp:GridView ID="grdshadetrn" runat="server" AllowSorting="True"  
                                    AutoGenerateColumns="False" CssClass="tContentArial"  
                                    HeaderStyle-CssClass ="tContentArial"  Font-Bold="False" 
                                    OnRowCommand="grdshadetrn_RowCommand"  Width = "100%"
                                    OnRowDataBound="grdshadetrn_RowDataBound" ShowFooter="True" 
                                    BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" 
                                    CellPadding="3" Font-Size="Small">
                                    <Columns>
                                        <asp:BoundField DataField="SEQUENCE_NO" HeaderText="Sequence" />
                                        <asp:BoundField DataField="YARN_CODE" HeaderText="Yarn Code" />
                                        
                                        <asp:BoundField DataField="YARN_SHADE_CODE" HeaderText="Shade Code" />
                                        <asp:TemplateField HeaderText="Shade RGB">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtShadeRGBColorTrn" runat="server" Width="20px" Text="" ReadOnly="true"></asp:TextBox>
                                                <asp:LinkButton ID="lbtnShadeRGBTrn" ToolTip='<%# Bind("YARN_SHADE_RGB") %>' runat="server"
                                                    Text='<%# Bind("YARN_SHADE_RGB") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="COUNT" HeaderText="Count" />
                                        <asp:BoundField DataField="YARN_STD" HeaderText="Yanr STD." />
                                      <%--  <asp:BoundField DataField="REQ_QTY" HeaderText="Req Qty" />--%>
                                       
                                        <asp:TemplateField HeaderText="Req Qty">
                                            <ItemTemplate>
                                              <asp:Label ID = "REQ_QTY" runat ="server"   Text='<%# Bind("REQ_QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate >
                                             <asp:Label ID="reqlblTotal" runat="server" Text="Label" ></asp:Label>
                                             
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="SHRINKAGE" HeaderText="Shrinkage" />
                                        <asp:BoundField DataField="WASTAGE" HeaderText="Wastage" />
                                        <asp:BoundField DataField="REJECTION" HeaderText="Rejection" />
                                    <%--  <asp:BoundField DataField="QTY" HeaderText="Qty." />--%>
                                          
                                        <asp:TemplateField HeaderText="QTY">
                                            <ItemTemplate>
                                              <asp:Label ID = "QTY" runat ="server"   Text='<%# Bind("QTY") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate >
                                             <asp:Label ID="lblTotal" runat="server" Text="Label" ></asp:Label>
                                             
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                        
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Button ID="lnktrnEdit" Text="Edit" runat="server" CommandName="trnEdit" CommandArgument='<%# Eval("SEQUENCE_NO") %>'>
                                                </asp:Button><asp:Button ID="lnktrnDelete" runat="server" Text="Delete" CommandName="trnDelete"
                                                    CommandArgument='<%# Eval("SEQUENCE_NO") %>'></asp:Button>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <RowStyle CssClass="RowStyle SmallFont" ForeColor="#000066" />
                                    <SelectedRowStyle CssClass="SelectedRowStyle" BackColor="#669999" 
                                        Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" />
                                    <FooterStyle BackColor="#0099FF" ForeColor="#000066" />
                                    <PagerStyle CssClass="PagerStyle" BackColor="White" ForeColor="#000066" 
                                        HorizontalAlign="Left" />
                                    <HeaderStyle CssClass="HeaderStyle SmallFont" BackColor="#006699" 
                                        Font-Bold="False" ForeColor="White" Font-Size="Small" />
                                </asp:GridView>
                                
                                 <asp:Label ID="lbl" runat="server" Text="Label" Visible="False"></asp:Label>
                                <asp:HiddenField ID="hf1" runat="server" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
