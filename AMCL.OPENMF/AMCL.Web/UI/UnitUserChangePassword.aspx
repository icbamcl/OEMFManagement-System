<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="UnitUserChangePassword.aspx.cs" Inherits="UI_UnitUserCreate" Title=" User Password Change Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript"> 
     function fnCheqeInput()
     {
      
          
            if(document.getElementById("<%=UserIDTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=UserIDTextBox.ClientID%>").focus();
                alert("Please Enter User ID");
                return false;
            }
         
            if(document.getElementById("<%=oldPasswordTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=oldPasswordTextBox.ClientID%>").focus();
                alert("Please Enter Old Password");
                return false;
            }
            if(document.getElementById("<%=newPasswordTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=newPasswordTextBox.ClientID%>").focus();
                alert("Please Enter New Password");
                return false;
            }
            if(document.getElementById("<%=newPasswordTextBox.ClientID%>").value !="")
            {
                var passLenght=document.getElementById("<%=newPasswordTextBox.ClientID%>").value;
                
                if(parseInt(passLenght.length)!="6")
                    {
                     document.getElementById("<%=newPasswordTextBox.ClientID%>").focus();
                     alert("Password must be 6 characters ");
                     return false;
                    }
            }
         
            if(document.getElementById("<%=confirmPasswordTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=confirmPasswordTextBox.ClientID%>").focus();
                alert("Please Enter Confirm New Password");
                return false;
            }
      
            if(document.getElementById("<%=newPasswordTextBox.ClientID%>").value !=document.getElementById("<%=confirmPasswordTextBox.ClientID%>").value)
            {
                 document.getElementById("<%=confirmPasswordTextBox.ClientID%>").focus();
                    alert("Confirm Password did not match ");
                    return false;
            }
        
         if(document.getElementById("<%=UserIDTextBox.ClientID%>").value !=""&&document.getElementById("<%=newPasswordTextBox.ClientID%>").value !="")
            {
            if(document.getElementById("<%=UserIDTextBox.ClientID%>").value ==document.getElementById("<%=newPasswordTextBox.ClientID%>").value)
               {
                document.getElementById("<%=UserIDTextBox.ClientID%>").focus();
                alert("User ID and password can not be same");
                return false;
               }
            }
         
         
     }
     
      function fnReset()
      {

        document.getElementById("<%=oldPasswordTextBox.ClientID%>").value ="";
        document.getElementById("<%=newPasswordTextBox.ClientID%>").value ="";
        document.getElementById("<%=confirmPasswordTextBox.ClientID%>").value ="";
        return false;
        
       }
       
     
  
   
    function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			event.returnValue=false;
		}
	}
    </script>
<br />
<br />
 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                User Password Change Form
            
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
<br />
<table width="540" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="160"></colgroup>


      
     <tr>
        <td align="left">User ID:</td>
        <td align="left">
        <asp:TextBox 
                ID="UserIDTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="5" MaxLength="10"></asp:TextBox>
            <span class="star">*</span></td>
    </tr>

     <tr>
        <td align="left">Enter Old Password:</td>
        <td align="left">
        <asp:TextBox 
                ID="oldPasswordTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="5" TextMode="Password" MaxLength="20"></asp:TextBox>
            <span class="star">*</span></td>
    </tr>
    <tr>
        <td align="left">Enter New Password:</td>
        <td align="left">
        <asp:TextBox 
                ID="newPasswordTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="5" TextMode="Password" MaxLength="20"></asp:TextBox>
            <span class="star">* ( password must be 6 characters )</span></td>
    </tr>
     <tr>
        <td align="left">Confirm New Password:</td>
        <td align="left">
        <asp:TextBox 
                ID="confirmPasswordTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="5" TextMode="Password" MaxLength="20"></asp:TextBox>
            <span class="star">*</span></td>
    </tr>
   

    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2"><asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" onclick="saveButton_Click" OnClientClick="return fnCheqeInput();"/>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;  <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" 
                  /></td>
    </tr>
</table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

