<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportPriceRefixing.aspx.cs" Inherits="UI_UnitReportPriceRefixing" Title=" Unit Price Refixation Report Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
     

    
    function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	
	function  fnReset()
	{
	   document.getElementById("<%=fundNameDropDownList.ClientID%>").value ="0";
	   document.getElementById("<%=fromPriceDateTextBox.ClientID%>").value ="";
	   document.getElementById("<%=toPriceDateTextBox.ClientID%>").value ="";
	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	     if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
                
            }
          
            if(document.getElementById("<%=fromPriceDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=fromPriceDateTextBox.ClientID%>").value))
                    {
                     document.getElementById("<%=fromPriceDateTextBox.ClientID%>").focus();
                     alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
             if(document.getElementById("<%=toPriceDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=toPriceDateTextBox.ClientID%>").value))
                    {
                     document.getElementById("<%=toPriceDateTextBox.ClientID%>").focus();
                     alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
           
          
	    
	}
 </script>
    <style type="text/css">
        .style5
        {
            height: 30px;
        }
        .style6
        {
         border:solid 1px #A8ACAF;
        text-align: left;
    }
        .style8
        {
            font-size: small;
            font-weight: bold;
            text-align: right;
        }
        .style9
        {
            text-align: right;
        }
        .style10
        {
            font-size: small;
            font-weight: bold;
        }
    </style>
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
<br />
<br />
 <table align="left" style="width: 537px">
        <tr>
            <td class="FormTitle" align="center">
               Price Refixing Report Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

    <br />
     <br />
      <br />
    
<table width="600" align="left" cellpadding="0" cellspacing="0" >
<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>

    
    
     <tr >
        <td class="style9" style="font-size: small"  ><b>Fund Name :</b></td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="2" 
               ></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
        <td align="left" class="style10" style="text-align: right"  >From Date:</td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:TextBox ID="fromPriceDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="2"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="PriceDateImageButton" 
                TargetControlID="fromPriceDateTextBox" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="fromPriceDateTextBox" />
            <asp:ImageButton ID="PriceDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="3" />
            </td>
       
    </tr>
    <tr class="style5">
       <td class="style8"  >
           To Date :
        </td>
        
        <td align="left" colspan="4">&nbsp;
            <asp:TextBox ID="toPriceDateTextBox" runat="server" 
                CssClass="textInputStyleDate" TabIndex="2"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toPriceDateTextBox_CalendarExtender" 
                runat="server" Format="dd-MMM-yyyy" PopupButtonID="PriceDateImageButton0" 
                TargetControlID="toPriceDateTextBox" />
            <ajaxToolkit:CalendarExtender ID="toPriceDateTextBox_CalendarExtender1" 
                runat="server" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="toPriceDateTextBox" />
            <asp:ImageButton ID="PriceDateImageButton0" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="3" />
        </td>
         
      
    </tr>
    
     
    
    
  
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
        <asp:Button ID="SaveButton" runat="server" Text="Print" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="p"  TabIndex="10" onclick="SaveButton_Click" />&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        </td>
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

