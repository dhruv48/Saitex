<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Issue_Againts_PA_Trolly_No.aspx.cs" Inherits="Module_Yarn_SalesWork_Pages_Issue_Againts_PA_Trolly_No" %>


<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="obout_Interface" Namespace="Obout.Interface" TagPrefix="cc3" %>
<%@ Register Assembly="obout_ComboBox" Namespace="Obout.ComboBox" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
 <link rel="stylesheet" type="text/css" href="../../../../StyleSheet/CommonStyle.css" />

    <script language="javascript" type="text/javascript">

        function BindYRNQTY() {
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
            margin-left: 4px;
        }
        .c1
        {
            width: 80px;
        }
        .c2
        {
            margin-left: 4px;
            width: 300px;
        }
        .c3
        {
            width: 200px;
        }
        .c4
        {
            margin-left: 4px;
            width: 300px;
        }
        .c5
        {
            width: 200px;
        }
        .d1
        {
            width: 80px;
        }
        .d2
        {
            margin-left: 4px;
            width: 180px;
        }
        .d3
        {
            margin-left: 4px;
            width: 180px;
        }
        .d4
        {
            margin-left: 4px;
            width: 180px;
        }
    </style>
</head>
<body bgcolor="#afcae4" class="tContentArial">
    <form id="form1" style="background-color: #afcae4" runat="server">
    <div>
        <cc1:ToolkitScriptManager ID="ToolkitScriptManager2" runat="server">
        </cc1:ToolkitScriptManager>
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
            ShowSummary="False" ValidationGroup="YM" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <table width="100%" class="tContentArial td">
                    <tr>
                        <td align="center" class="td TableHeader" valign="top">
                            <strong class="titleheading"> Quality:
                                <asp:Label ID="lblYarnCode" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Desc
                                <asp:Label ID="lblYarnDesc" runat="server"></asp:Label>
                                &nbsp;&nbsp;                                
                                 Shade :
                                <asp:Label ID="lblShade" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Shade Family :
                                <asp:Label ID="lblShadeFamily" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Trn No :
                                <asp:Label ID="lbTrnNo" runat="server"></asp:Label>
                                &nbsp;&nbsp;
                                Trn Type:
                                <asp:Label ID="lblTrnType" runat="server"></asp:Label>
                                 &nbsp;&nbsp;
                                PA No.:
                                <asp:Label ID="lblPaNo" runat="server"></asp:Label>
                                
                            
                            </strong>
                        </td>
                    </tr>
                    <tr class="SmallFont">
                        <td class="td tdLeft">
                            <table width="98%" align="center">
                                <tr bgcolor="#006699">
                                   
                                   
                                    <td align="left" valign="top" width="10%">
                                        <span class="titleheading"><b>Trolly/Cartes No.</b></span>
                                    </td>
                                     <td align="left" valign="top" width="10%">
                                        <span class="titleheading"><b>Weight</b></span>
                                    </td>
                                   
                                </tr>
                                <tr>
                               
                                
                                   
                                    <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtTrollyNo" CssClass="SmallFont TextBoxNo" Width="100px"  AutoPostBack="true"
                                            runat="server"
                                          ></asp:TextBox>
                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtBatch"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtBatch"
                                            Display="None" ErrorMessage="Maximum 10 Batch can be Schedule for a day" 
                                            MaximumValue="9" MinimumValue="0" Type="Integer" ValidationGroup="YM"></asp:RangeValidator>--%>
                                    </td>
                                    
                                     <td align="left" valign="top" width="10%">
                                        <asp:TextBox ID="txtWeight" CssClass="SmallFont TextBoxNo" Width="100px" runat="server" ReadOnly="false"
                                          ></asp:TextBox>
                                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtWeight"
                                            Display="Dynamic" ErrorMessage="Please Enter QTY" SetFocusOnError="True" ValidationGroup="YM"></asp:RequiredFieldValidator>
                                           <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtWeight"
                                            Display="None" ErrorMessage="Only Numeric value" 
                                            MaximumValue="99999.9999" MinimumValue="00000.0000" Type="Double" ValidationGroup="YM"></asp:RangeValidator>
                                        
                                    </td>
                                    
                                    <td align="left" valign="top">
                                        <asp:Button ID="BtnBOMSave" runat="server" OnClick="BtnBOMSave_Click" Text="Add"
                                            ValidationGroup="YM" />
                                        <asp:Button ID="BtnBOMCancel" runat="server" OnClick="BtnBOMCancel_Click" Text="Cancel" />
                                    </td>
                                </tr>
                                
                                <tr>
                                    <td align="left" colspan="3" valign="top">
                                        <asp:GridView ID="grdsub_trn" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Bold="False"  ShowFooter="false" OnRowCommand="grdSub_trnArticleDetail_RowCommand"
                                            Width="98%">
                                            <Columns>
                                            
                                             
                                            
                                                <asp:TemplateField HeaderText="Sr&nbsp;No." ItemStyle-VerticalAlign="top" ItemStyle-Width="25px"
                                                    HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtSubTrnUNIQUE_ID" CssClass="SmallFont LabelNo" runat="server" Text='<%# Bind("SR_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                    <ItemStyle VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="TRN No" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTRN_NO" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TRN_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                 <asp:TemplateField HeaderText="PA No" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPANo" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("PA_NO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                
                                                <asp:TemplateField HeaderText="Yarn Code" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYarncode" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_CODE") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                <asp:TemplateField HeaderText="Yarn Desc" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblYarnDesc" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("YARN_DESC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                               
                                                <asp:TemplateField HeaderText="Trolly" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTrolly" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TROLLY") %>'></asp:Label>
                                                    </ItemTemplate>
                                                 
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Trolly Weight" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtTrollyWeight" runat="server" CssClass="LabelNo SmallFont" Text='<%# Bind("TROLLY_WEIGHT") %>'></asp:Label>
                                                    </ItemTemplate>
                                                 
                                                </asp:TemplateField>
                                               
                                                
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkBOMDelete" runat="server" CommandArgument='<%# Eval("SR_NO") %>'
                                                            CommandName="BOMDelete" OnClientClick="return confirm('Are you Sure want to delete this  Detail?');"
                                                            TabIndex="12" Text="Delete" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <RowStyle CssClass="RowStyle " />
                                            <SelectedRowStyle CssClass="SelectedRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" />
                                            <PagerStyle CssClass="PagerStyle" />
                                            <HeaderStyle BackColor="#336699" CssClass="SmallFont" ForeColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="td" valign="top">
                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit"
                                ValidationGroup="M1" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>