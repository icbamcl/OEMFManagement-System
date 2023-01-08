﻿<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSIPEdit.aspx.cs" Inherits="UI_UnitSIPEdit" Title="Registration Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  function fnConfirmDelete()
  {
         if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
            if(document.getElementById("<%=sipNumberDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=sipNumberDropDownList.ClientID%>").focus();
                alert("Please Select Receipt Number");
                return false;
                
            }

            var isConfirm = confirm("Are you sure to DELETE this sale record");
            if (isConfirm) {
                return true;
            }
            else {
                return false;
            }
        }
    
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
            document.getElementById("<%=sipDateTextBox.ClientID%>").value = "";    
            document.getElementById("<%=sipNumberDropDownList.ClientID%>").value = "0";
            document.getElementById("<%=sipAmountTextBox.ClientID%>").value ="";
            document.getElementById("<%=autoRenewalDropDownList.ClientID%>").value = "N";
            document.getElementById("<%=payFrequencyTypeDropDownList.ClientID%>").value ="1";
            document.getElementById("<%=monthlySIPDateDropDownList.ClientID%>").value = "5";
            document.getElementById("<%=sipStartDateTextBox.ClientID%>").value = "";
            document.getElementById("<%=sipEndDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=sipDurationTextBox.ClientID%>").value = ""; 

             document.getElementById("<%=accTypeDropDownList.ClientID%>").value = "3"; 
            document.getElementById("<%=bankNameDropDownList.ClientID%>").value = "0"; 
            document.getElementById("<%=branchNameDropDownList.ClientID%>").value = "0";
            document.getElementById("<%=routingNoTextBox.ClientID%>").value = "";
            document.getElementById("<%=bankAccTextBox.ClientID%>").value ="";
           
            return false;
        }
        else
        {
            return true;
            
        }
    }
    
     function fnCheckRouterNo()
	        {
	             if(document.getElementById("<%=routingNoTextBox.ClientID%>").value =="")
                         {
                                document.getElementById("<%=routingNoTextBox.ClientID%>").focus();
                                alert("Please Enter Routing Number");
                                return false;
                          }
	        }
	        
    
       function fnCheqInput()
        {                
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Numaric value for Registration Number");
                    return false;
                }
            }                                                                                            
        
         if (document.getElementById("<%=sipNumberDropDownList.ClientID%>").value == "0")
            {
                document.getElementById("<%=sipNumberDropDownList.ClientID%>").focus();
                alert("Please Select SIP Number");
                return false;
                
            }
           if (document.getElementById("<%=sipDateTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=sipDateTextBox.ClientID%>").focus(); alert("Please Enter SIP Date "); return false;                                               
            }

         if (document.getElementById("<%=sipAmountTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=sipAmountTextBox.ClientID%>").focus(); alert("Please Enter Amount "); return false;                                               
            }
         if (document.getElementById("<%=sipStartDateTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=sipStartDateTextBox.ClientID%>").focus(); alert("Please  Enter EFT Debit Start Date "); return false;                                               
         }
         if (document.getElementById("<%=sipEndDateTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=sipEndDateTextBox.ClientID%>").focus(); alert("Please  Enter EFT Debit End Date "); return false;                                               
         }
         if (document.getElementById("<%=sipDurationTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=sipDurationTextBox.ClientID%>").focus(); alert("Please  Enter SIP Duration In Month "); return false;                                               
         }
         if (document.getElementById("<%=bankAccTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=bankAccTextBox.ClientID%>").focus(); alert("Please  Enter Bank Account Number "); return false;                                               
         }
         if (document.getElementById("<%=routingNoTextBox.ClientID%>").value == "")
            {
             document.getElementById("<%=routingNoTextBox.ClientID%>").focus(); alert("Please  Enter Routing Number "); return false;                                               
            }
        
        
        }        
    
          
        function fnCheckRegNo()
        {
            if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
            if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Registration Number");
                    return false;
                }
            }
        }
        function fncInputNumericValuesOnly()
	    {
		    if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		    {		        
		        alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		    }
	    }
	 
  function fnEnable(Action)
    {   
	  
             
  }
                
	    
</script>



    <style type="text/css">
    
       
        .style3
        {
            font-size: large;
        }
        A.TextLink:hover
        {
	        COLOR: #556677; 
	        font-weight:bold; 
	        font-size:large;
	        text-decoration:underline;
        	
        }
       
        .fontStyle
        {
            color: #FFFFFF;
            font-weight: bold;
        }
        tr .menuBarBottomSelected td a:hover
        {
        	background-color:#547DD3;
        }
       .menuBarBottomSelected
       {
       	         background-color: #666666;  
       }
        .style4
        {
            height: 25px;
            width: 196px;
        }
        .style5
        {
            height: 25px;
            width: 172px;
        }
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #666666 solid;
            BACKGROUND-COLOR: #FFFFFF;
            COLOR: #465360;
            FONT-SIZE: 11px;
            padding-left: 2px;
            height: 17px;
            font-weight: bold;
        }
        .auto-style6 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
            WIDTH: 40px;
        }
        </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="center">
        Unit Holder SIP Edit Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      
      <div id="divContent" runat="server" style="width:1100px; height:auto"  align="center">
 <table width="1100px" align="center" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="150"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
 <tr>
        <td colspan="4" align="left">
            <table align="left" cellpadding="0" cellspacing="0" border="0" 
                style="width: 723px" >
          
                <tr>
                    <td align="left" style=" background-color: #CCCCFF; " id="tdReg" runat="server" 
                        class="style4"> &nbsp; 
                       <asp:HyperLink ID="regHolderHL" runat="server" 
                            NavigateUrl="~/UI/UnitRegEdit.aspx" Font-Bold="True">  Principal Holder Information</asp:HyperLink>
                    </td>
                    <td style="background-color: #CCCCFF;" id="tdJoint" runat="server" 
                        class="style5">
                  
                    <span class="style3">|</span><asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/UI/UnitJointEdit.aspx" Font-Bold="True" >Joint Holder Information</asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; "  id="tdNominee" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink3" runat="server" 
                            NavigateUrl="~/UI/UnitNomineeEdit.aspx" Font-Bold="True" >Nominee Information</asp:HyperLink>
                    </td >
                    <td style=" background-color: #547DD3; "  id="tdSIP" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/UI/UnitSIPEdit.aspx" Font-Bold="True" ForeColor="White">SIP Information</asp:HyperLink>
                    </td >
                   
                </tr>
           </table>
        </td>
        
    </tr>
     <tr>
        <td colspan="4" align="left">
        <fieldset>
            <legend style="font-weight: 700"> ::Unit Holder Registration Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
             <tr>
        <td align="right" ><b>Registration No:</b></td>
        <td align="left">
            <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="fundCodeTextBoxResource1" Width="52px"></asp:TextBox>
            <b>/</b><asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="branchCodeTextBoxResource1" Width="52px"></asp:TextBox>
            <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" TabIndex="1" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                <span class="star">*<span class="star" ><asp:Button ID="findButton" 
                    runat="server" AccessKey="f" CssClass="buttonmid" 
                    onclick="findButton_Click" 
                    onclientclick="return fnCheckRegNo();" TabIndex="2" Text="Find" />
                </span></span></td>
        <td align="right"><b>Name of The Peincipal Holder:</b></td>
        <td align="left">
          &nbsp;  <asp:Label ID="NameLabel" runat="server" Text="" 
                style="font-weight: 700; font-size: small;"></asp:Label>
                                                    </td> 
       
    </tr>
    <tr>
        <td align="right" ><b>Registration Date:</b></td>
        <td align="left">
          &nbsp;  <asp:Label ID="DateLabel" runat="server" Text="" 
                style="font-weight: 700; font-size: small;"></asp:Label>
                                                    </td>  
             
        <td align="right"><b>Registration Type :</b></td>
        <td align="left">
                &nbsp;  <asp:Label ID="TypeLabel" runat="server" Text="" 
                    style="font-weight: 700; font-size: small;"></asp:Label>
                                                    </td>
    </tr>
     <tr>
        <td align="right" ><b>CIP:</b></td>
        <td align="left" >
            
           &nbsp; <asp:Label ID="CIPLabel" runat="server" Text="" 
                style="font-weight: 700; font-size: small;"></asp:Label>
                                                    </td>   
                <td align="right"> <b>Is ID Account:</b>   </td>        
                
                 <td align="left"> &nbsp; <asp:Label ID="IDLabel" runat="server" Text="" 
                         style="font-weight: 700; font-size: small;"></asp:Label>    </td>
             
       
    </tr>
   
 </table>
        </fieldset>
        </td>
        
   
        
    </tr>
   
   
    <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
    <tr>
        <td colspan="4">
          <fieldset>
            <legend style="font-weight: 700"> ::SIP Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
             
             <tr>
      
        
        <td align="right" >
            <b> Select SIP Number:</b></td>
       <td align="left">
        
            <asp:DropDownList ID="sipNumberDropDownList" runat="server" 
                CssClass="DropDownList"       
                AutoPostBack="True" OnSelectedIndexChanged="sipNumberDropDownList_SelectedIndexChanged" >
                                  
            </asp:DropDownList>
                                                   
                                            
                                                   
                                <span class="star">*</span></td>
                   <td align="right" >&nbsp;</td>
        <td align="left">
            &nbsp;</tr>
                 <tr>
      
        
        <td align="right" >
            <b>&nbsp;SIP Application Date:</b></td>
       <td align="left">
            <asp:TextBox ID="sipDateTextBox" runat="server" CssClass="textInputStyleDate" TabIndex="8"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="sipDateTextBox_CalendarExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton1" 
                TargetControlID="sipDateTextBox" />
            <asp:ImageButton ID="RegDateImageButton1" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
               />           
            <span class="star">*</span></td>
                   <td align="right" ><b>Installment Amount:</b></td>
        <td align="left">
            <asp:TextBox ID="sipAmountTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="12" TabIndex="3"></asp:TextBox>
                 
                                <span class="star">*</span></tr>
              <tr>
      
                      
                   <td align="right" ><b>&nbsp;Auto Renewal Option:</b></td>
        <td align="left">
            <strong>
            <asp:DropDownList ID="autoRenewalDropDownList" runat="server" 
                CssClass="auto-style3" TabIndex="5" >
               
             <asp:ListItem Value="N" Selected="True">NO</asp:ListItem>
            <asp:ListItem Value="Y">YES</asp:ListItem>                     
            </asp:DropDownList> </strong> <span class="star">*</span></td>
                      
                   <td align="right" ><b>Payment Frequency:</b></td>
        <td align="left">
            <strong>
            <asp:DropDownList ID="payFrequencyTypeDropDownList" runat="server" 
                CssClass="auto-style3" TabIndex="6" >
               
             <asp:ListItem Value="1" Selected="True">Monthly</asp:ListItem>
            <asp:ListItem Value="3">Quarterly</asp:ListItem>                     
            </asp:DropDownList> </strong> <span class="star">*</span></td>
    </tr>
    
     <tr>
         <td align="right" ><b>&nbsp;Monthly SIP Date:</b></td>
        <td align="left">
            <strong>
            <asp:DropDownList ID="monthlySIPDateDropDownList" runat="server" 
                CssClass="auto-style3" TabIndex="7" >
               
             <asp:ListItem Value="10" Selected="True">10th</asp:ListItem>
            <asp:ListItem Value="5">5th</asp:ListItem>                     
            </asp:DropDownList> </strong>
            <span class="star">*</span></td>
          <td align="right" ><b>EFT Debit Starts On Date:</b></td>
        <td align="left">
            <asp:TextBox ID="sipStartDateTextBox" runat="server" CssClass="textInputStyleDate" TabIndex="8" ></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="sipStartDateTextBox" />
            <asp:ImageButton ID="RegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                meta:resourcekey="RegDateImageButtonResource1" TabIndex="9" />           
            <span class="star">*</span></td>
    </tr>
   
    
    <tr>
       <td align="right" class="auto-style2" ><b>SIP Duration in Month:</b></td>
        <td align="left" class="auto-style2">
            <asp:TextBox ID="sipDurationTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="5" Font-Bold="True" AutoPostBack="True" OnTextChanged="sipDurationTextBox_TextChanged" 
              ></asp:TextBox>
                                <span class="star">*</span></td>
       <td align="right" class="auto-style2" ><b>EFT Debit Ends On Date:</b></td>
        <td align="left" class="auto-style2">
            <asp:TextBox ID="sipEndDateTextBox" runat="server" CssClass="textInputStyleDate" TabIndex="8"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender0" runat="server" Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton0" TargetControlID="sipEndDateTextBox" />
            <asp:ImageButton ID="RegDateImageButton0" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png"  />
                                <span class="star">*</span></td>
     
    </tr>
                <%-- <tr>
                    <td align="right">
                        <b>Step-up Option:</b></td>
                    <td align="left">
                         <asp:RadioButton ID="stepupNORadioButton" runat="server" Checked="True" onclick="fnEnable('noMinorRadioButton');"
                Font-Bold="True" GroupName="stepupType" Text="NO" TabIndex="31" />
             <asp:RadioButton ID="stepupYESRadioButton" runat="server" Font-Bold="True" onclick="fnEnable('yesMinorRadioButton');"
                GroupName="stepType" Text="YES" TabIndex="32" /></td>
                <td class="auto-style4"><b>Step-up Annual Amount:</b></td>
                <td class="auto-style5">
            <asp:TextBox ID="stepupAmountTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="12"></asp:TextBox>
                 
                                </td>
                   
      </tr> --%>
     
            </table>
            </fieldset>
        </td>
        
    </tr>
  <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
    <tr>
        <td colspan="4" align="left">
         <fieldset>
            <legend style="font-weight: 700"> ::SIP Bank Details::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
                 <tr>
                    <td align="right" >
                        <b>Account Type:</b></td>
                    <td align="left">
            <strong>
            <asp:DropDownList ID="accTypeDropDownList" runat="server" 
                CssClass="auto-style3" TabIndex="6" >
               
             <asp:ListItem Value="1">Current</asp:ListItem>
            <asp:ListItem Value="2" Selected="True">Savings</asp:ListItem>                     
            </asp:DropDownList> </strong> <span class="star">*</span></td>
                    <td align="right" >  &nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
              <tr>
                    <td align="right" >
                        <b>Account No:</b></td>
                    <td align="left">
                        <asp:TextBox ID="bankAccTextBox" runat="server" 
                    CssClass="textInputStyle" onkeypress= "fncInputNumericValuesOnly()" MaxLength="15" 
                            TabIndex="35" Width="218px" ></asp:TextBox>  
                                <span class="star">*</span></td>
                    <td align="right" >  <b>Routing Number:</b></td>
                    <td align="left">
                        <asp:TextBox ID="routingNoTextBox" runat="server" CssClass="textInputStyle" 
                            MaxLength="16"  onkeypress= "fncInputNumericValuesOnly()" TabIndex="6" 
                            Width="218px"></asp:TextBox>
                        <asp:Button   ID="findRoutingNoButton" runat="server" Text="Find" CssClass="auto-style6" 
                            OnClientClick="return fnCheckRouterNo();" 
                            AccessKey="f" OnClick="findRoutingNoButton_Click"/>
                                <span class="star">*</span></td>
                </tr>
              <tr>
                    <td align="right" >
                        <b>&nbsp;Bank Name:</b></td>
                    <td align="left">
                        <asp:DropDownList ID="bankNameDropDownList" runat="server" AutoPostBack="True" 
                           
                            TabIndex="48" Enabled="False">
                        </asp:DropDownList>
                     </td>
                    <td align="right" >  <b>Bank Branch:</b></td>
                    <td align="left">
                        <asp:DropDownList ID="branchNameDropDownList" runat="server" TabIndex="49"  AutoPostBack="True" 
                            Enabled="False">
                        </asp:DropDownList>
                                                </td>
                </tr>
                </table>
       </fieldset>
        </td>
      </tr>
     
      
  
     
     
    </table>
    
   </div>
   
    <br />
    <table width="500" align="center" cellpadding="0" cellspacing="0">
    
     <tr>
        <td align="right">
        <asp:Button ID="regSaveButton" runat="server" Text="Save and Print" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="regSaveButton_Click" TabIndex="41" AccessKey="s" 
                meta:resourcekey="regSaveButtonResource1"/>&nbsp;
        </td>
        <td align="left"><asp:Button ID="regDeleteButton" runat="server" 
                Text="Delete" CssClass="buttoncommon" OnClientClick="return fnConfirmDelete();"
                TabIndex="41" OnClick="regDeleteButton_Click"/>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset"  OnClientClick="return fnReset();"
                CssClass="buttoncommon"   AccessKey="r" 
                meta:resourcekey="regResetButtonResource1" TabIndex="42" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" 
                meta:resourcekey="regCloseButtonResource1" TabIndex="43" 
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
</asp:Content>

