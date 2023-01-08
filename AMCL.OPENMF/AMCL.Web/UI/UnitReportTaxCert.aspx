<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportTaxCert.aspx.cs" Inherits="UI_UnitReportTaxCert" Title=" Tax Certificate Report Form (Design and Developed by Sakhawat)" %>
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
	    document.getElementById("<%=regNoTextBox.ClientID%>").value="";
	    document.getElementById("<%=incomeTaxCertRadioButton.ClientID%>").checked=true;
	    document.getElementById("<%=incomeTaxFYDropDownList.ClientID%>").value="0";
	    document.getElementById("<%=fyPartDropDownList.ClientID%>").value="0";
	    document.getElementById("<%=InvestCertRadioButton.ClientID%>").checked=false;
	    document.getElementById("<%=investFYToTextBox.ClientID%>").value="";
	    document.getElementById("<%=investFYFromTextBox.ClientID%>").value="";
	    document.getElementById("<%=surrenderRadioButton.ClientID%>").checked=false;
	    document.getElementById("<%=surrenderDateDropDownList.ClientID%>").value="0";
	    document.getElementById("<%=solventRadioButton.ClientID%>").checked=false;
	    document.getElementById("<%=USDRateTextBox.ClientID%>").value="";
	    document.getElementById("<%=TTDateTextBox.ClientID%>").value ="";
	    document.getElementById("<%=RepRateTextBox.ClientID%>").value = "";
         document.getElementById("<%=balanceDateTextBox.ClientID%>").value ="";
	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	    if(document.getElementById("<%=regNoTextBox.ClientID%>").value=="")
	    {
	        alert("Please Enter Registration Number");
	        document.getElementById("<%=regNoTextBox.ClientID%>").focus();
	        return false;
	    }
	    
	    if(document.getElementById("<%=incomeTaxCertRadioButton.ClientID%>").checked==true)
	    {
	         if(document.getElementById("<%=incomeTaxFYDropDownList.ClientID%>").value=="")
	            {
	                alert("Please Enter Income Tax FY Number");
	                document.getElementById("<%=incomeTaxFYDropDownList.ClientID%>").focus();
	                return false;
	            }
	            if(document.getElementById("<%=fyPartDropDownList.ClientID%>").value=="")
	            {
	                alert("Please Enter Income Tax FY Part");
	                document.getElementById("<%=fyPartDropDownList.ClientID%>").focus();
	                return false;
	            }
	    }
	    else if(document.getElementById("<%=InvestCertRadioButton.ClientID%>").checked==true)
	    {
	        if(document.getElementById("<%=investFYFromTextBox.ClientID%>").value=="")
	            {
	                alert("Please Enter FY From Date");
	                document.getElementById("<%=investFYFromTextBox.ClientID%>").focus();
	                return false;
	            }
	            if(document.getElementById("<%=investFYToTextBox.ClientID%>").value=="")
	            {
	                alert("Please Enter FY To Date");
	                document.getElementById("<%=investFYToTextBox.ClientID%>").focus();
	                return false;
	            }

	           if(document.getElementById("<%=investFYToTextBox.ClientID%>").value !="")
                {
                    var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                    if(!checkDate.test(document.getElementById("<%=investFYToTextBox.ClientID%>").value))
                        {
                        document.getElementById("<%=investFYToTextBox.ClientID%>").focus();
                        alert("Plese Select Date From The Calender");
                         return false;
                        }
                 }      
                if(document.getElementById("<%=investFYFromTextBox.ClientID%>").value !="")
                {
                    var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                    if(!checkDate.test(document.getElementById("<%=investFYFromTextBox.ClientID%>").value))
                        {
                        document.getElementById("<%=investFYFromTextBox.ClientID%>").focus();
                        alert("Plese Select Date From The Calender");
                         return false;
                        }
                 }      
	         
	    }
	   else if(document.getElementById("<%=surrenderRadioButton.ClientID%>").checked==true)
	    {
	        if(document.getElementById("<%=surrenderDateDropDownList.ClientID%>").value=="")
	            {
	                alert("Please Enter Surrender Date");
	                document.getElementById("<%=surrenderDateDropDownList.ClientID%>").focus();
	                return false;
	            }	   
	                 	         
	    }
	   else if(document.getElementById("<%=solventRadioButton.ClientID%>").checked==true)
	    {
	         if(document.getElementById("<%=USDRateTextBox.ClientID%>").value=="")
	            {
	                alert("Please Enter USD Rate");
	                document.getElementById("<%=USDRateTextBox.ClientID%>").focus();
	                return false;
	            }
	            
	           if(document.getElementById("<%=USDRateTextBox.ClientID%>").value!="")
	           {
	             var checkValue=document.getElementById("<%=USDRateTextBox.ClientID%>").value;
	             if (checkValue == null || !checkValue.toString().match(/^[-]?\d*\.?\d*$/)) 
	                 {
	                    alert("Please Enter USD Rate in Numeric value Only");
                         return false;
                     }
	           }
	            if(document.getElementById("<%=RepRateTextBox.ClientID%>").value=="")
	            {
	                alert("Please Enter Repurchase Rate");
	                document.getElementById("<%=RepRateTextBox.ClientID%>").focus();
	                return false;
	            }
	            
	           if(document.getElementById("<%=RepRateTextBox.ClientID%>").value!="")
	           {
	             var checkValue=document.getElementById("<%=RepRateTextBox.ClientID%>").value;
	             if (checkValue == null || !checkValue.toString().match(/^[-]?\d*\.?\d*$/)) 
	                 {
	                    alert("Please Enter  Rate in Numeric value Only");
                         return false;
                     }
	           }
	            if(document.getElementById("<%=TTDateTextBox.ClientID%>").value=="")
	            {
	                alert("Please Enter USD TT Date");
	                document.getElementById("<%=TTDateTextBox.ClientID%>").focus();
	                return false;
	            }	 
	            if(document.getElementById("<%=TTDateTextBox.ClientID%>").value !="")
                {
                    var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                    if(!checkDate.test(document.getElementById("<%=TTDateTextBox.ClientID%>").value))
                        {
                        document.getElementById("<%=TTDateTextBox.ClientID%>").focus();
                        alert("Plese Select Date From The Calender");
                         return false;
                        }
	            }
             if(document.getElementById("<%=balanceDateTextBox.ClientID%>").value=="")
	            {
	                alert("Please Enter Balance Date");
	                document.getElementById("<%=balanceDateTextBox.ClientID%>").focus();
	                return false;
	            }	 
	            if(document.getElementById("<%=balanceDateTextBox.ClientID%>").value !="")
                {
                    var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                    if(!checkDate.test(document.getElementById("<%=balanceDateTextBox.ClientID%>").value))
                        {
                        document.getElementById("<%=balanceDateTextBox.ClientID%>").focus();
                        alert("Plese Select Date From The Calender");
                         return false;
                        }
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
        }
    .style7
    {
        font-size: small;
    }
        .style8
        {
            text-align: center;
            font-weight: bold;
            font-size: small;
            font-family: Verdana;
            height: 15px;
            width: 116px;
            border: 1px solid #A8ACAF;
        }
        .style9
        {
            text-align: center;
            font-weight: bold;
            font-size: large;
            font-family: Verdana;
            height: 15px;
            width: 106px;
            border: 1px solid #A8ACAF;
        }
        .style10
        {
            text-align: center;
            font-weight: bold;
            font-size: large;
            font-family: Verdana;
            height: 15px;
            width: 116px;
            border: 1px solid #A8ACAF;
        }
    </style>
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
<br />
<br />
 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Investment/ Income Tax Certifiacte Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
    <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>
<table align="center" cellpadding="0" cellspacing="0" style="width: 725px" >
<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>

    <tr   class="style5">
        <td align="left" class="style6"><b>
            <span class="style7">Registration No:</span></b></td>
        <td align="left"  class="style6" colspan="4">
            <asp:TextBox ID="fundCodeTextBox" runat="server" CssClass="TextInputStyleSmall" 
                Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass="TextInputStyleSmall" Enabled="False"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="regNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                MaxLength="8" onkeypress="fncInputNumericValuesOnly()" TabIndex="1" 
                Width="95px" ontextchanged="regNoTextBox_TextChanged" AutoPostBack="true"></asp:TextBox>
        <span class="star">*</span></td>
        
    </tr>
    <tr class="style5">
       <td align="left" class="style6" >
           <asp:RadioButton ID="incomeTaxCertRadioButton" 
                runat="server" GroupName="taxType" TabIndex="2" 
                Text="Income Tax Certificate" style="font-weight: 700; font-size: small;" 
               Checked="True" />
        </td>
        <td align="left" class="style6"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span 
                class="style7">FY :</span></b>
        </td>
        <td align="left" class="style9">
            <asp:DropDownList ID="incomeTaxFYDropDownList" runat="server" 
                    TabIndex="3"></asp:DropDownList></td>
         <td align="left" class="style10"><b>&nbsp;&nbsp;<span class="style7"> FYPart: </span> </b></td>
          <td align="left" class="style6">
              <asp:DropDownList ID="fyPartDropDownList" runat="server" 
                    TabIndex="4"></asp:DropDownList><span class="star">*</span></td>
      
    </tr>
    
    
    <tr class="style6">
        <td align="left"  class="style6">
           <asp:RadioButton ID="InvestCertRadioButton" 
                runat="server" GroupName="taxType" TabIndex="5" 
                Text="Investment Certificate" 
                style="font-weight: 700; font-size: small;" />
        </td>
        <td align="left"  class="style6"><b>&nbsp;&nbsp;&nbsp;<span class="style7">&nbsp;&nbsp;&nbsp;FY :</span></b></td>
        <td align="left" colspan="3"  class="style6">
            <asp:TextBox ID="investFYFromTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="6"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="investFYFromTextBox" />
            <asp:ImageButton ID="RegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
&nbsp;<span class="star">*</span>
            <asp:TextBox ID="investFYToTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="7"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender0" 
                runat="server" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton0" 
                TargetControlID="investFYToTextBox" />
            <asp:ImageButton ID="RegDateImageButton0" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
        &nbsp;<span class="star">*</span><asp:RadioButton ID="AllRadioButton" 
                runat="server" Checked="True" Font-Bold="True" GroupName="CIP" Text="All" />
            <asp:RadioButton ID="CIPRadioButton" runat="server" Font-Bold="True" 
                GroupName="CIP" Text="CIP" />
            <asp:RadioButton ID="NonCIPRadioButton" runat="server" Font-Bold="True" 
                GroupName="CIP" Text="NON CIP" />
        </td>
       
    </tr>
     <tr class="style6">
        <td align="left"  class="style6">
           <asp:RadioButton ID="surrenderRadioButton" 
                runat="server" GroupName="taxType" TabIndex="8" 
                Text="Surrender Certificate" style="font-weight: 700; font-size: small;" 
                 />
        </td>
        <td align="left"  class="style6"><b>&nbsp;&nbsp;&nbsp;<span class="style7">Date:</span></b></td>
        <td align="left"  class="style9">           
            <asp:DropDownList ID="surrenderDateDropDownList" runat="server" 
                    TabIndex="10"></asp:DropDownList>
            <span class="star">*</span></td>
         <td align="left"  class="style8">           
             as on Date:</td>
         <td align="left"  class="style6">           
             &nbsp;
             <asp:TextBox ID="surrenderFYToTextBox" runat="server" 
                 CssClass="textInputStyleDate" TabIndex="10"></asp:TextBox>
             <ajaxToolkit:CalendarExtender ID="surrenderFYToTextBox_CalendarExtender" 
                 runat="server" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton2" 
                 TargetControlID="surrenderFYToTextBox" />
             <asp:ImageButton ID="RegDateImageButton2" runat="server" 
                 AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                 TabIndex="10" />
             <span class="star">*</span></td>
        
       
    </tr>
     <%-- <tr class="style6">
        <td align="left"  class="style6">
           <asp:RadioButton ID="surrenderRadioButton" 
                runat="server" GroupName="taxType" TabIndex="8" 
                Text="Surrender Certificate" style="font-weight: 700; font-size: small;" 
                 />
        </td>
        <td align="left"  class="style6"><b>&nbsp;&nbsp;&nbsp;<span class="style7">Date:</span></b></td>
        <td align="left" colspan="3"  class="style6">
            <asp:TextBox ID="surDateFromTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="6"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="surDateFrom" 
                TargetControlID="surDateFromTextBox" />
            <asp:ImageButton ID="surDateFrom" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
&nbsp;<span class="star">*</span>
            <asp:TextBox ID="surDateToTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="7"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender2" 
                runat="server" Format="dd-MMM-yyyy" PopupButtonID="surDateTo" 
                TargetControlID="surDateToTextBox" />
            <asp:ImageButton ID="surDateTo" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
        &nbsp;<span class="star">*</span></td>
        
       
    </tr>--%>

     <tr class="style6">
        <td align="left"  class="style6">
           <asp:RadioButton ID="solventRadioButton" 
                runat="server" GroupName="taxType" TabIndex="11" 
                Text="Solvent Certificate" style="font-weight: 700; font-size: small;"  />
        </td>
          <td align="left"  class="style6"><b><span class="style7">US $ Rate:</span></b></td>
        <td align="left"   class="style6" colspan="3">           
            <asp:TextBox ID="USDRateTextBox" runat="server" CssClass="TextInputStyleSmall" 
                TabIndex="1" 
                Width="95px" ontextchanged="regNoTextBox_TextChanged"></asp:TextBox>
            <b> <span class="style7">&nbsp;<span class="star">*</span>Date
            </span>
            <asp:TextBox ID="TTDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="7"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="TTDateTextBox_CalendarExtender" 
                runat="server" Format="dd-MMM-yyyy" PopupButtonID="TTDatemageButton" 
                TargetControlID="TTDateTextBox" />
            <asp:ImageButton ID="TTDatemageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="10" />
            &nbsp;<span class="style7"><span class="star">*</span>Rep Rate&nbsp;
            <asp:TextBox ID="RepRateTextBox" runat="server" CssClass="TextInputStyleSmall"></asp:TextBox>
            <span class="star">*</span></span></b></td>
       
    </tr>
    <tr class="style6">
        <td align="left"  class="style6">
            &nbsp;</td>
        <td align="left"  class="style6"><b>&nbsp;&nbsp;&nbsp;</b></td>
        <td align="left"  class="style9">           
            &nbsp;</td>
         <td align="left"  class="style8">           
             Balance&nbsp; Date:</td>
         <td align="left"  class="style6">           
             &nbsp;
             <asp:TextBox ID="balanceDateTextBox" runat="server" 
                 CssClass="textInputStyleDate" TabIndex="10" AutoPostBack="True" OnTextChanged="balanceDateTextBox_TextChanged"></asp:TextBox>
             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" 
                 runat="server" Format="dd-MMM-yyyy" PopupButtonID="balanceImageButton2" 
                 TargetControlID="balanceDateTextBox" />
             <asp:ImageButton ID="balanceImageButton2" runat="server" 
                 AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                 TabIndex="10" />
             <span class="star">*</span></td>
        
       
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
</table>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="regNoTextBox" EventName="TextChanged" />
</Triggers>
</asp:UpdatePanel>
<table align="center" cellpadding="0" cellspacing="0" style="width: 338px">
     <tr>
        <td align="right">
        <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="p" onclick="PrintButton_Click" />&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        </td>
        <td>
        &nbsp;
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

