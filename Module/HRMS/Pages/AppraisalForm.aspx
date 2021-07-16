<%@ Page Language="C#" MasterPageFile="~/CommonMaster/EMPMaster.master" AutoEventWireup="true" CodeFile="AppraisalForm.aspx.cs" Inherits="Module_HRMS_Pages_AppraisalForm" Title="Appraisal Form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphBody" Runat="Server">
    
    <table border="1" cellpadding="1" cellspacing="1" style="vertical-align:top;" >
            <tr>
                    <td class="td" >Appraisal Form Executive</td>
                    <td><asp:ImageButton ID="ImgBtnAPE" ImageAlign="Middle" 
                            ImageUrl="~/CommonImages/link_print.png" runat="server" 
                            onclick="ImgBtnAPE_Click" OnClientClick="aspnetForm.target ='_blank';"/></td>
            </tr>
            <tr>
                <td class="td">Appraisal Form Executive-Confirmation</td>
                <td><asp:ImageButton ID="ImgBtnAFEC" ImageAlign="Middle" 
                        ImageUrl="~/CommonImages/link_print.png" runat="server" 
                        onclick="ImgBtnAFEC_Click" OnClientClick="aspnetForm.target ='_blank';"/></td>
            </tr>
            <tr>
                <td class="td">Appraisal Form Staff</td>
                <td><asp:ImageButton ID="ImgBtnAFS" ImageAlign="Middle" 
                        ImageUrl="~/CommonImages/link_print.png" runat="server" 
                        onclick="ImgBtnAFS_Click" OnClientClick="aspnetForm.target ='_blank';"/></td>
            </tr>
            <tr>
                <td class="td">Appraisal Form Executive Workers</td>
               <td><asp:ImageButton ID="ImgBtnAFEW" ImageAlign="Middle" 
                       ImageUrl="~/CommonImages/link_print.png" runat="server" 
                       onclick="ImgBtnAFEW_Click" OnClientClick="aspnetForm.target ='_blank';"/></td>
            </tr>          
   </table>
</asp:Content>

