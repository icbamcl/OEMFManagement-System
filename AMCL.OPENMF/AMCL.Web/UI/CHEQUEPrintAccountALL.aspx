<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="CHEQUEPrintAccountALL.aspx.cs" Inherits="UI_CHEQUEPrintAccountALL" Title=" Cheque Print Account ALL (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
  
 
 
    function fnCheqInput() {


        //Input Text Checking
        if (document.getElementById("<%=NameTextBox.ClientID%>").value == "") {
            document.getElementById("<%=NameTextBox.ClientID%>").focus();
            alert("Please Enter Pay To Name");
            return false;

        }
         if (document.getElementById("<%=AmountTextBox.ClientID%>").value == "") {
            document.getElementById("<%=AmountTextBox.ClientID%>").focus();
            alert("Please Enter Amount");
            return false;

         }
         if (document.getElementById("<%=chqDateTextBox.ClientID%>").value == "") {
            document.getElementById("<%=chqDateTextBox.ClientID%>").focus();
            alert("Please Enter Date");
            return false;

         }
         if(document.getElementById("<%=chqDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=chqDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=chqDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
    }
            			               
  
</script>
 


    <style type="text/css">
        .style1
        {
            height: 13px;
        }
        .style2
        {
            height: 20px;
        }
        .auto-style1 {
            height: 20px;
            text-align: right;
        }
        </style>
 


    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
        <table align="left"  cellpadding="0" cellspacing="0" border="0" >
        <tr>
            <td class="FormTitle" align="center">
           &nbsp;Cheque Print Form&nbsp;
            </td>           
            
        </tr> 
        <tr>
             <td>
                
     <table width="1100" align="left" cellpadding="0" cellspacing="0" border="0" >
      
        
        <tr>
            <td align="left" colspan="4" class="style1">
            </td>
      </tr>
   
        <tr>
        <td   align="right"><strong>Pay To Name :</strong></td>
        <td align="left">
            <strong>
         <asp:TextBox ID="NameTextBox" runat="server" CssClass="TextInputStyleLarge" Font-Bold="True" >ICB Securities Trading Company Ltd.</asp:TextBox>
            </strong>
            <span class="star">*</span></td>
         <td   align="right"><strong>Pay To Account No(if any):</strong></td>
        <td align="left">
         <asp:TextBox ID="accNoTextBox" runat="server" CssClass="TextInputStyleLarge" ></asp:TextBox>
          </td>
        </tr>
          <tr>
        <td   align="right"><strong>&nbsp;Amount :</strong></td>
        <td align="left">
         <asp:TextBox ID="AmountTextBox" runat="server" CssClass="TextInputStyleLarge" ></asp:TextBox>
            <span class="star">*</span></td>
         <td   align="right"><strong>Date:</strong></td>
        <td align="left">
         <asp:TextBox ID="chqDateTextBox" runat="server" CssClass="TextInputStyleLarge" ></asp:TextBox>
           <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="chqDateTextBox" PopupButtonID="chequeDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
             <asp:ImageButton ID="chequeDateImageButton" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png"  />
            <span class="star">*</span></td>
        </tr>
           <tr>
            <td align="left" colspan="4" class="style1">
            </td>
      </tr>
           <tr>
            <td align="left" colspan="4" class="style1">
            </td>
      </tr>
          <tr>
        
        
             
        <td align="center" class="style2" colspan="4" >         
        <asp:Button ID="ChecqueVoucherButton" runat="server" Text="Print Cheque" CssClass="buttoncommon"  OnClientClick="return fnCheqInput();"
               Width="150px" Height="24px" OnClick="ChecqueVoucherButton_Click"/></td>
        
       
      </tr>
        <tr>
            <td align="left" colspan="5">
                &nbsp;</td>
      </tr>
      
                                
     
</table>
        </td>
        </tr>
       
      </table>

    
</asp:Content>

