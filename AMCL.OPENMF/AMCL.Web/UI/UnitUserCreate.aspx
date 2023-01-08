<%@ Page Language="C#" MasterPageFile="~/UI/AMCLCommon.master" AutoEventWireup="true" CodeFile="UnitUserCreate.aspx.cs" Inherits="UI_UnitUserCreate" Title=" User Create Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script language="javascript" type="text/javascript"> 
     function fnCheqeInput()
     {
          
      
          if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
            }
            if(document.getElementById("<%=UserIDTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=UserIDTextBox.ClientID%>").focus();
                alert("Please Enter User ID");
                return false;
            }
            
            if(document.getElementById("<%=passwordTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=passwordTextBox.ClientID%>").focus();
                alert("Please Enter Password");
                return false;
            }
            if(document.getElementById("<%=confirmPasswordTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=confirmPasswordTextBox.ClientID%>").focus();
                alert("Please Enter Confirm Password");
                return false;
            }
            
            if(document.getElementById("<%=userNameTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=userNameTextBox.ClientID%>").focus();
                alert("Please Enter User Name");
                return false;
            }
            if(document.getElementById("<%=branchNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=branchNameDropDownList.ClientID%>").focus();
                alert("Please Select  Branch");
                return false;
            }
        var flag=0;
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
              if( document.forms[0].elements[Looper].checked==true)
              {
                flag=1;
              }
            }   
         }
         if (flag==0)
         {
             alert("Please check at least one menu ");
                return false;
         }
       if(document.getElementById("<%=passwordTextBox.ClientID%>").value !="")
        {
            var passLenght=document.getElementById("<%=passwordTextBox.ClientID%>").value;
            
            if(parseInt(passLenght.length)!="6")
                {
                 document.getElementById("<%=passwordTextBox.ClientID%>").focus();
                 alert("Password must be 6 characters ");
                 return false;
                }
        }
        if(document.getElementById("<%=passwordTextBox.ClientID%>").value !=document.getElementById("<%=confirmPasswordTextBox.ClientID%>").value)
        {
             document.getElementById("<%=confirmPasswordTextBox.ClientID%>").focus();
                alert("Confirm Password did not match ");
                return false;
        }
        if(document.getElementById("<%=UserIDTextBox.ClientID%>").value !=""&&document.getElementById("<%=passwordTextBox.ClientID%>").value !="")
            {
            if(document.getElementById("<%=UserIDTextBox.ClientID%>").value ==document.getElementById("<%=passwordTextBox.ClientID%>").value)
               {
                document.getElementById("<%=UserIDTextBox.ClientID%>").focus();
                alert("User ID and password can not be same");
                return false;
               }
            }
         
     }
     
      function fnReset()
      {
        document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0";
        document.getElementById("<%=branchNameDropDownList.ClientID%>").value ="0";
        document.getElementById("<%=UserIDTextBox.ClientID%>").value ="";
        document.getElementById("<%=passwordTextBox.ClientID%>").value ="";
        document.getElementById("<%=confirmPasswordTextBox.ClientID%>").value ="";
        document.getElementById("<%=userNameTextBox.ClientID%>").value ="";
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
                document.forms[0].elements[Looper].checked=false;
        }   }
        return false;
      } 
     
    function fnCheckAll()
    {
      
         
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
           
            if (strType=="checkbox")
            {
              if(document.forms[0].elements[Looper].id.indexOf("menuCheckBoxList")!=-1)
              {
                document.forms[0].elements[Looper].checked=true;
              }
        }   }
    }
    function fnUnCheckAll()
    {
        for (Looper=0; Looper < document.forms[0].length ; Looper++ )
        {
            var strType = document.forms[0].elements[Looper].type;
            if (strType=="checkbox")
            {
                
               if(document.forms[0].elements[Looper].id.indexOf("menuCheckBoxList")!=-1)
              {
                document.forms[0].elements[Looper].checked=false;
              }
        }   }
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
           &nbsp;User Create Form
            </td>           
            <td>
               
            </td>
        </tr> 
      </table>
<br />

<table width="500" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="120"></colgroup>

    <tr>
        <td align="left">Fund Name:</td>
        <td align="left">
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1" meta:resourcekey="fundNameDropDownListResource1"></asp:DropDownList><span class="star">*</span></td>
        
    </tr>    
     <tr>
        <td align="left">User ID:</td>
        <td align="left">
        <asp:TextBox 
                ID="UserIDTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="2" MaxLength="15" meta:resourcekey="UserIDTextBoxResource1"></asp:TextBox>
            <span class="star">*</span></td>
    </tr>
     <tr>
        <td align="left">Password:</td>
        <td align="left">
        <asp:TextBox 
                ID="passwordTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="3" TextMode="Password" MaxLength="20" 
                meta:resourcekey="passwordTextBoxResource1"></asp:TextBox>
            <span class="star">*( password must be 6 characters)</span></td>
    </tr>
     <tr>
        <td align="left">Confirm Password:</td>
        <td align="left">
        <asp:TextBox 
                ID="confirmPasswordTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="4" TextMode="Password" MaxLength="20" 
                meta:resourcekey="confirmPasswordTextBoxResource1"></asp:TextBox>
            <span class="star">*</span></td>
    </tr>
    <tr>
        <td align="left">User Name:</td>
        <td align="left">
        <asp:TextBox 
                ID="userNameTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="5" Width="255px" MaxLength="50" 
                meta:resourcekey="userNameTextBoxResource1"></asp:TextBox>
            <span class="star">*</span></td>
    </tr>
    <tr>
       <td align="left">User Branch Name:</td>
        <td align="left">
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="6" meta:resourcekey="branchNameDropDownListResource1"></asp:DropDownList><span class="star">*</span></td>
      
    </tr>
    <tr>
        <td align="center" colspan="2"><b>Lsit of Menue(s)</b></td>
    </tr>
    <tr><td align="left" >
        <br />
        <br />
        <br />
        Check Menues to be permitted</td><td align="left"> 
        <div  style="width:250px; height:180px; overflow:auto; border:solid 1px ; text-align:left" ID="dvMenue" runat="server">
            <asp:CheckBoxList ID="menuCheckBoxList" runat="server" 
                meta:resourcekey="menuCheckBoxListResource1" ></asp:CheckBoxList></div></td></tr>
    <tr><td>&nbsp;</td><td  align="left"><a href="#" onclick="fnCheckAll();">Check All</a>&nbsp;&nbsp;&nbsp;&nbsp;<a href="#" onclick="fnUnCheckAll();">Clear All</a></td></tr>            
    <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
      <tr><td align="left" >
        <br />
        <br />
        <br />
        Check Fund to be permitted</td><td align="left"> 
        <div  style="width:250px; height:100px; overflow:auto; border:solid 1px ; text-align:left" ID="DivFund" runat="server">
            <asp:CheckBoxList ID="fundCheckBoxList" runat="server" 
                meta:resourcekey="menuCheckBoxListResource1" ></asp:CheckBoxList></div></td>
      </tr>
        <tr><td align="left" >
        <br />
        <br />
        <br />
        Check Branch to be permitted</td><td align="left"> 
        <div  style="width:250px; height:180px; overflow:auto; border:solid 1px ; text-align:left" ID="DivBranch" runat="server">
            <asp:CheckBoxList ID="branchCheckBoxList" runat="server" 
                meta:resourcekey="menuCheckBoxListResource1" ></asp:CheckBoxList></div></td></tr>
                <tr>
        <td colspan="2">&nbsp;
        </td>
    </tr>
    
</table>
 
<table width="500" align="center" cellpadding="0" cellspacing="0" >
<tr>
        <td align="center" colspan="2"><asp:Button ID="saveButton" runat="server" Text="Save" 
                CssClass="buttoncommon" onclick="saveButton_Click" 
                OnClientClick="return fnCheqeInput();" meta:resourcekey="saveButtonResource1"/>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource1" />&nbsp;&nbsp;  
            <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" meta:resourcekey="regCloseButtonResource1" 
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

