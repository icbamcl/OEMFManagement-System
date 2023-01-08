<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRegEntry.aspx.cs" Inherits="UI_UnitRegEntry" Title="Registration Entry Form (Design and Developed by Sakhawat)"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
   function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
            document.getElementById("<%=regNoTextBox.ClientID%>").value ="";          
            document.getElementById("<%=regDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=regTypeDropDownList.ClientID%>").value ="N";
           
            document.getElementById("<%=IDAccNoTextBox.ClientID%>").value ="";            
            document.getElementById("<%=IDbranchNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=IDbankNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=isCIPDropDownList.ClientID%>").value ="0"
            document.getElementById("<%=NameTextBox.ClientID%>").value ="";
            document.getElementById("<%=FMTextBox.ClientID%>").value ="";
            document.getElementById("<%=MotherTextBox.ClientID%>").value ="";
            document.getElementById("<%=spouceTextBox.ClientID%>").value ="";
            document.getElementById("<%=OccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=NationalityTextBox.ClientID%>").value ="BANGLADESHI";
            
            document.getElementById("<%=NIDTextBox.ClientID%>").value ="";
            document.getElementById("<%=TINTextBox.ClientID%>").value ="";
            document.getElementById("<%=passportTextBox.ClientID%>").value ="";
            document.getElementById("<%=birthCertNoTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=presentAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=presentAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=presentCityTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=parmanAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=parmanAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=parmentCityTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=SourceFundTextBox.ClientID%>").value ="";
            document.getElementById("<%=webaddressTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=EmailTextBox.ClientID%>").value ="";
            document.getElementById("<%=MobileTextBox.ClientID%>").value = "";
            document.getElementById("<%=TelphoneTextBox.ClientID%>").value ="";
            document.getElementById("<%=SexDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=DateofBirthTextBox.ClientID%>").value ="";
            document.getElementById("<%=MaritialStatusDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=ReligionDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=EducationDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=RemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=dvIDInof.ClientID%>").style.visibility = "hidden";
            document.getElementById("<%=DivBank.ClientID%>").style.visibility = "hidden";
            document.getElementById("<%=divBEFTN.ClientID%>").style.visibility = "hidden";   
             
            document.getElementById("<%=parmenentCheckBox.ClientID%>").checked=true;
          
            document.getElementById("<%=IDNoRadioButton.ClientID%>").checked=true;      
            document.getElementById("<%=IDYesRadioButton.ClientID%>").checked=false; 
            
            document.getElementById("<%=BankNORadioButton.ClientID%>").checked=true;      
            document.getElementById("<%=BankYesRadioButton.ClientID%>").checked=false; 
            
            document.getElementById("<%=bftnNoRadioButton.ClientID%>").checked=true;      
            document.getElementById("<%=bftnYesRadioButton.ClientID%>").checked=false; 
            
            document.getElementById("<%=IndividualButton.ClientID%>").checked=true;      
            document.getElementById("<%=CompRadioButton.ClientID%>").checked=false; 
            
            
             
            return false;
        }
        else
        {
            return true;
            
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
            
           if(document.getElementById("<%=regDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regDateTextBox.ClientID%>").focus();
                alert("Please Enter Registration Date");
                return false;
                
            }
            if(document.getElementById("<%=regDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=regDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=regDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
           
              if(document.getElementById("<%=EmailTextBox.ClientID%>").value !="")
                {
                  var emailCheck = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
                  if (!emailCheck.test(document.getElementById("<%=EmailTextBox.ClientID%>").value))
                    {
                        document.getElementById("<%=EmailTextBox.ClientID%>").focus();
                        alert("Please Enter Valid Email ID");
                        return false;
                    }
                }


         
             if(document.getElementById("<%=isCIPDropDownList.ClientID%>").value =="0")
             {
                 document.getElementById("<%=isCIPDropDownList.ClientID%>").focus();
                        alert("Please Select CIP");
                        return false;
             }
              if( document.getElementById("<%=IDYesRadioButton.ClientID%>").checked)                
                {       
                         
                         if(document.getElementById("<%=IDAccNoTextBox.ClientID%>").value =="")
                         {
                                document.getElementById("<%=IDAccNoTextBox.ClientID%>").focus();
                                alert("Please Enter ID Account Number");
                                return false;
                          }
                         if(document.getElementById("<%=IDbankNameDropDownList.ClientID%>").value =="0")
                         {
                                document.getElementById("<%=IDbankNameDropDownList.ClientID%>").focus();
                                alert("Please Select ID Institution");
                                return false;
                          }
                         if(document.getElementById("<%=IDbranchNameDropDownList.ClientID%>").value =="0")
                         {
                                document.getElementById("<%=IDbranchNameDropDownList.ClientID%>").focus();
                                alert("Please Select ID Institution Branch");
                                return false;
                          }
                      
                      
                }
             
            if(document.getElementById("<%=NameTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=NameTextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder Name");
                return false;
                
            }
            
          if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="AMC/01")
           {            
               if(document.getElementById("<%=FMTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=FMTextBox.ClientID%>").focus();
                    alert("Please Enter Unit Holder's Father");
                    return false;
                    
                }
                if(document.getElementById("<%=MotherTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=MotherTextBox.ClientID%>").focus();
                    alert("Please Enter Unit Holder's Mother Name");
                    return false;
                    
                }
               if(document.getElementById("<%=OccupationDropDownList.ClientID%>").value =="0")
                {
                    document.getElementById("<%=OccupationDropDownList.ClientID%>").focus();
                    alert("Please Select Unit Holder's Service");
                    return false;
                    
                }    
                              
                      
             }        
            
            if(document.getElementById("<%=NationalityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=NationalityTextBox.ClientID%>").focus();
                alert("Please Unit Holder's Enter Nationality");
                return false;
                
            }
         
            if(document.getElementById("<%=presentAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=presentAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Present Address Line1");
                return false;
                
            }
            if(document.getElementById("<%=presentAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=presentAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Present Address Line2");
                return false;
                
            }
           if(document.getElementById("<%=presentCityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=presentCityTextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Present City");
                return false;
                
            }
            
            
            if(document.getElementById("<%=parmanAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=parmanAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Parmanent Address Line1");
                return false;
                
            }
            if(document.getElementById("<%=parmanAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=parmanAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Parmanent Address Line2");
                return false;
                
            }
           if(document.getElementById("<%=parmentCityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=parmentCityTextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Parmanent City");
                return false;
                
            }
            
            if(document.getElementById("<%=MobileTextBox.ClientID%>").value !="")
            {
                var len = parseInt(document.getElementById("<%=MobileTextBox.ClientID%>").value.length);
                         
                if (len != 11)
                {
                    document.getElementById("<%=MobileTextBox.ClientID%>").focus();
                    alert("Please Enter 11 Digits Mobile Number Only");
                    return false;
                }
            }
          
                
           if( document.getElementById("<%=BankYesRadioButton.ClientID%>").checked)                
                {                                                               
                  
                      
                    if(document.getElementById("<%=bankAccTextBox.ClientID%>").value =="")
                     {
                            document.getElementById("<%=bankAccTextBox.ClientID%>").focus();
                            alert("Please Enter Unit Holder's Bank Account Number");
                            return false;
                      }
                     if(document.getElementById("<%=bankAccTextBox.ClientID%>").value !="")
                     {
                         var len = parseInt(document.getElementById("<%=bankAccTextBox.ClientID%>").value.length);
                         
                         if (len != 13)
                         {
                             document.getElementById("<%=bankAccTextBox.ClientID%>").focus();
                             alert("Please Enter 13 Digits Bank Account Number Only");
                             return false;
                         }
                      }
                     if(document.getElementById("<%=bankNameDropDownList.ClientID%>").value =="0")
                     {
                            document.getElementById("<%=bankNameDropDownList.ClientID%>").focus();
                            alert("Please Select Unit Holder's Bank Name");
                            return false;
                     }
                     if(document.getElementById("<%=branchNameDropDownList.ClientID%>").value =="0")
                     {
                            document.getElementById("<%=branchNameDropDownList.ClientID%>").focus();
                            alert("Please Select Unit Holder's Bank Branch Name");
                            return false;
                      }     
                                      
                                                                                                   
                }
        }        
       function fnEnable(Action)
        {
            if(Action.indexOf("IDNoRadioButton")!=-1)
            {   
                if( document.getElementById("<%=IDNoRadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=dvIDInof.ClientID%>").style.visibility == "visible")
                      {
                    
                        document.getElementById("<%=dvIDInof.ClientID%>").style.visibility = "hidden";
                      }
                    
                }
                
             }
           if(Action.indexOf("IDYesRadioButton")!=-1)
            {   
                if( document.getElementById("<%=IDYesRadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=dvIDInof.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=dvIDInof.ClientID%>").style.visibility = "visible";
                      }
                  
                      
                }
                
             }
             
           if(Action.indexOf("BankNORadioButton")!=-1)
            {   
                if( document.getElementById("<%=BankNORadioButton.ClientID%>").checked)                
                {       
                         
                               
                      if(document.getElementById("<%=divBEFTN.ClientID%>").style.visibility == "visible")
                      {
                    
                        document.getElementById("<%=divBEFTN.ClientID%>").style.visibility = "hidden";
                      }
                      if(document.getElementById("<%=DivBank.ClientID%>").style.visibility == "visible")
                      {
                    
                        document.getElementById("<%=DivBank.ClientID%>").style.visibility = "hidden";
                      }
                    
                }
                
             }
          if(Action.indexOf("BankYesRadioButton")!=-1)
            {   
                if( document.getElementById("<%=BankYesRadioButton.ClientID%>").checked)                
                {       
                                                        
                      if(document.getElementById("<%=divBEFTN.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=divBEFTN.ClientID%>").style.visibility = "visible";
                      }
                      if(document.getElementById("<%=DivBank.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=DivBank.ClientID%>").style.visibility = "visible";
                      }
                  
                      
                }
                
             }
         
         }
    function   fnGetPresenAddress(Action)
        {
             if( document.getElementById("<%=parmenentCheckBox.ClientID%>").checked)                
                    {       
                                                            
                       
                        document.getElementById("<%=parmanAddress1TextBox.ClientID%>").value =document.getElementById("<%=presentAddress1TextBox.ClientID%>").value;
                        document.getElementById("<%=parmanAddress2TextBox.ClientID%>").value =document.getElementById("<%=presentAddress2TextBox.ClientID%>").value;
                        document.getElementById("<%=parmentCityTextBox.ClientID%>").value = document.getElementById("<%=presentCityTextBox.ClientID%>").value;
                                
                    }
                    
                else
                {
                     document.getElementById("<%=parmanAddress1TextBox.ClientID%>").value ="";
                     document.getElementById("<%=parmanAddress2TextBox.ClientID%>").value ="";
                     document.getElementById("<%=parmentCityTextBox.ClientID%>").value ="";
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
	    
	    function fnCheckRouterNo()
	        {
	             if(document.getElementById("<%=routingNoTextBox.ClientID%>").value =="")
                         {
                                document.getElementById("<%=routingNoTextBox.ClientID%>").focus();
                                alert("Please Enter Routing Number");
                                return false;
                          }
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
            height: 12px;
            font-weight: bold;
            text-decoration: underline;
        }
        .style5
        {
            height: 12px;
            font-weight: bold;
        }
        .style6
        {
            text-decoration: underline;
        }
        </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="center">
        Unit Holder Registration Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      
 <div id="divContent" runat="server" style="width:980px; height:auto"  align="center">
 <table width="970px" align="center" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="150"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
 <tr>
        <td colspan="4" align="left">
            <table width="850px" align="left" cellpadding="0" cellspacing="0" border="0" >
          
                <tr>
                    <td align="left" style=" background-color: #547DD3; height:25px" id="tdReg" runat="server"> &nbsp; 
                       <asp:HyperLink ID="regHolderHL" runat="server" 
                            NavigateUrl="~/UI/UnitRegEntry.aspx" Font-Bold="True" ForeColor="White">  Principal Holder Information</asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px " id="tdJoint" runat="server">
                  
                    <span class="style3">|</span><asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/UI/UnitJointEntry.aspx" Font-Bold="True">Joint Holder Information</asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px" id="tdNominee" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/UI/UnitNomineeEntry.aspx" Font-Bold="True">Nominee Information</asp:HyperLink>
                    </td >
                   </td>
                    <td style=" background-color: #CCCCFF; height:25px" id="td1" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink3" runat="server" 
                            NavigateUrl="~/UI/UnitSIPEntry.aspx" Font-Bold="True">SIP Information</asp:HyperLink>
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
                                <span class="star">*</span></td>
        <td align="right"><b>Registration Date:</b></td>
        <td align="left">
            <asp:TextBox ID="regDateTextBox" runat="server" CssClass="textInputStyleDate" 
                meta:resourcekey="regDateTextBoxResource1" TabIndex="2"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="RegDateTextBox" />
            <asp:ImageButton ID="RegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                meta:resourcekey="RegDateImageButtonResource1" TabIndex="3" />
            <span class="star">*</span></td> 
       
    </tr>
    <tr>
        <td align="right" ><b>Registration Type:</b></td>
        <td align="left">
            <asp:RadioButton ID="IndividualButton" runat="server" Checked="True" 
                Font-Bold="True" GroupName="regType" Text="Individual" TabIndex="3" />
            <asp:RadioButton ID="CompRadioButton" runat="server" Font-Bold="True" 
                GroupName="regType" Text="Company" TabIndex="4" />
            &nbsp;
            <asp:DropDownList ID="regTypeDropDownList" runat="server" 
                CssClass="DropDownList" TabIndex="5" 
                meta:resourcekey="regTypeDropDownListResource1">
            
            </asp:DropDownList><span class="star">*</span></td>  
             
        <td align="right"><b>Is CIP:</b></td>
        <td align="left">
                  <asp:DropDownList ID="isCIPDropDownList" runat="server" 
             CssClass="DropDownList" TabIndex="6" EnableViewState="False" 
                      meta:resourcekey="isCIPDropDownListResource1">
            <asp:ListItem Value="0" Selected="True">---</asp:ListItem>
            <asp:ListItem Value="N" meta:resourcekey="ListItemResource6">No</asp:ListItem>
            <asp:ListItem Value="Y" meta:resourcekey="ListItemResource7">Yes</asp:ListItem>            
            </asp:DropDownList><span class="star">*</span></td>
    </tr>
     <tr>
        <td align="right" ><b>Is ID Account:</b></td>
        <td align="left" >
            
         <asp:RadioButton ID="IDNoRadioButton" runat="server" Checked="True" onclick="fnEnable('IDNoRadioButton');"
                Font-Bold="True" GroupName="idType" Text="NO" TabIndex="7" />
             <asp:RadioButton ID="IDYesRadioButton" runat="server" Font-Bold="True" onclick="fnEnable('IDYesRadioButton');"
                GroupName="idType" Text="YES" TabIndex="8" /> </td>   
                <td align="left" colspan="2">                
                </td>
             
       
    </tr>
    <tr>
        <td align="left" colspan="4" >
        <div  id="dvIDInof" runat="server" style="visibility:hidden">
            <%-- <asp:UpdatePanel ID="IDBankNameUpdatePanel" runat="server">                                                               
        <ContentTemplate>--%>
            <table cellpadding="0" cellspacing="0">
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
                <tr>
                    <td align="right" >
                        <b>ID Account No:</b></td>
                    <td align="left">
                        <asp:TextBox ID="IDAccNoTextBox" runat="server" 
                    CssClass="textInputStyle" onkeypress= "fncInputNumericValuesOnly()" MaxLength="16" 
                            TabIndex="10" Width="218px" ></asp:TextBox>  
                    </td>
                    <td align="right" >  <b>ID Institution Name:</b></td>
                    <td align="left">
                    <asp:DropDownList ID="IDbankNameDropDownList" runat="server"  
                         AutoPostBack="true" 
                         onselectedindexchanged="IDbankNameDropDownList_SelectedIndexChanged"  
                         TabIndex="11" ></asp:DropDownList>  
                                                </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>ID Institution Branch:</b></td>
                    <td align="left" colspan="3">
                     <asp:DropDownList ID="IDbranchNameDropDownList" runat="server" TabIndex="12"></asp:DropDownList>               
                                                </td>
                   
                </tr>
            </table>
            <%-- </ContentTemplate>
                  <Triggers>                   
                    <asp:AsyncPostBackTrigger ControlID="IDbankNameDropDownList"  EventName="SelectedIndexChanged"/>
                  </Triggers>
                  
              </asp:UpdatePanel>--%>
         </div>
        
        </td>
                
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
            <legend style="font-weight: 700"> ::Principal Holder Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
      <tr>
        <td align="right" ><b>Name of Holder:</b></td>
        <td align="left">
            <asp:TextBox ID="NameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="13" 
                meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
        
        <td align="right" >
            <b>Father&#39;s Name:</b></td>
       <td align="left">
            <asp:TextBox ID="FMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="15" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
    </tr>
    
     <tr>
         <td align="right" ><b>Mother&#39;s Name:</b></td>
        <td align="left">
            <asp:TextBox ID="MotherTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="16" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
            <span class="star">*</span></td>
          <td align="right" ><b>Spouse Name:</b></td>
        <td align="left">
            <span class="star">
            <asp:TextBox ID="spouceTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="17" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                                *</span></td>
    </tr>
   
      <tr>
        <td align="right"  ><b>Occupation:</b></td>
        <td align="left">
            <asp:DropDownList ID="OccupationDropDownList" runat="server" TabIndex="18" 
                meta:resourcekey="holderOccupationDropDownListResource1">
            </asp:DropDownList>
                                <span class="star">*</span></td>
         <td align="right" ><b>BO Number (if any):</b></td>
        <td align="left">
            <asp:TextBox ID="holderBONumberTextBox" runat="server"  onkeypress= "fncInputNumericValuesOnly()"
                CssClass= "TextInputStyleLarge" MaxLength="16" TabIndex="18"
               ></asp:TextBox>
                                                    </td>
       
    </tr>
    <tr>
       <td align="right" ><b>Nationality:</b></td>
        <td align="left">
            <asp:TextBox ID="NationalityTextBox" runat="server" Text="BANGLADESHI"
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="21" meta:resourcekey="holderNationalityTextBoxResource1" 
               ></asp:TextBox>
                                <span class="star">*</span></td>
       <td align="right" ><b>Date of Birth:</b></td>
        <td align="left">
            <asp:TextBox ID="DateofBirthTextBox" runat="server" 
                CssClass="textInputStyleDate" TabIndex="22" 
                meta:resourcekey="holderDateofBirthTextBoxResource1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="hDofBCalendarExtender" runat="server" 
                TargetControlID="DateofBirthTextBox" PopupButtonID="hDofBImageButton" 
                Format="dd-MMM-yyyy" Enabled="True"/>
            <asp:ImageButton ID="hDofBImageButton" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="22" 
                meta:resourcekey="hDofBImageButtonResource1" />
                                                    </td>
     
    </tr>
     <tr>
       <td align="right" ><b>National ID No:</b></td>
        <td align="left">
            <asp:TextBox ID="NIDTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="23" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right"  ><b>TIN certificate No:</b></td>
        <td align="left">
            <asp:TextBox ID="TINTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="12" TabIndex="24" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
     
    </tr>
     <tr>
      <td align="right"  ><b>Passport No:</b></td>
        <td align="left">
            <asp:TextBox ID="passportTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="26" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right"  ><b>Birth certificate No:</b></td>
        <td align="left">
            <asp:TextBox ID="birthCertNoTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="26" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
     
    </tr>
    <tr>
     <td  colspan="2"align="center" class="style4" >Present Address</td>
     <td  colspan="2"align="center" class="style5" >
         <asp:CheckBox ID="parmenentCheckBox" runat="server" Text="As Present Address" onclick="fnGetPresenAddress('parmenentCheckBox');"
             Font-Bold="False" />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <span class="style6">Permanent Address</span></td>
        
    </tr>   
   <tr>
      <td align="right"  ><b>Address1:</b></td>
        <td align="left">
            <asp:TextBox ID="presentAddress1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="120" TabIndex="27" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right"  ><b>Address1:</b></td>
        <td align="left">
            <asp:TextBox ID="parmanAddress1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="120" TabIndex="30" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
     
    </tr>
     <tr>
      <td align="right"  ><b>Address2:</b></td>
        <td align="left">
            <asp:TextBox ID="presentAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="120" TabIndex="28" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right"  ><b>Address2:</b></td>
        <td align="left">
            <asp:TextBox ID="parmanAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="120" TabIndex="31" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
     
    </tr>
     <tr>
      <td align="right"  ><b>City:</b></td>
        <td align="left">
            <asp:TextBox ID="presentCityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="29" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right"  ><b>City:</b></td>
        <td align="left">
            <asp:TextBox ID="parmentCityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="32" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
     
    </tr>
     <tr>
     <td  colspan="2"align="center" class="style4" ></td>
     <td  colspan="2"align="center" class="style5" ></td>
        
    </tr>
    <tr>
            
              <td align="right"><b>Mobile Number:</b></td>
              <td align="left">
             <asp:TextBox ID="MobileTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="11" TabIndex="34" 
                      meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="right"><b>Telephone:</b></td>
        <td align="left">
             <asp:TextBox ID="TelphoneTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="32" TabIndex="34" 
                      meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                    </td>
    </tr>
     <tr>
            
              <td align="right"><b>Source of Fund:</b></td>
              <td align="left">
             <asp:TextBox ID="SourceFundTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="36" 
                      meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="right"><b>E-Mail:</b></td>
        <td align="left">
             <asp:TextBox ID="EmailTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="56" TabIndex="35" 
                      meta:resourcekey="holderEmailTextBoxResource1"></asp:TextBox>
                                    </td>
    </tr>
    <tr>
              <td align="right"><b>Web Address:</b></td>
              <td align="left">
             <asp:TextBox ID="webaddressTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="37" 
                      meta:resourcekey="holderEmailTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="right"><b>Sex:</b></td>
        <td align="left">
                  <asp:DropDownList ID="SexDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="38" 
                      meta:resourcekey="holderSexDropDownListResource1"> 
            <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource18">-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource19" >Male</asp:ListItem>
            <asp:ListItem Value="F" meta:resourcekey="ListItemResource20">Female</asp:ListItem>            
            </asp:DropDownList>
                                </td>
    </tr>
   
   
      <tr>
              <td align="right"><b>Maritial Status:</b></td>
              <td align="left">
                  <asp:DropDownList ID="MaritialStatusDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="39" 
                      meta:resourcekey="holderMaritialStatusDropDownListResource1">
             <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource21">-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource22">Married</asp:ListItem>
            <asp:ListItem Value="U" meta:resourcekey="ListItemResource23">Unmarried</asp:ListItem>            
                      <asp:ListItem Value="O" meta:resourcekey="ListItemResource24">Others</asp:ListItem>
            </asp:DropDownList>
                                </td>
        <td align="right"><b>Religion:</b></td>
        <td align="left">
                  <asp:DropDownList ID="ReligionDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="40" 
                      meta:resourcekey="holderReligionDropDownListResource1">
             <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource25" >-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource26" >Muslim</asp:ListItem>
            <asp:ListItem Value="H" meta:resourcekey="ListItemResource27">Hindu</asp:ListItem>            
                      <asp:ListItem Value="C" meta:resourcekey="ListItemResource28">Christian</asp:ListItem>
                      <asp:ListItem Value="B" meta:resourcekey="ListItemResource29">Buddah</asp:ListItem>
                      <asp:ListItem Value="O" meta:resourcekey="ListItemResource30">Others</asp:ListItem>
            </asp:DropDownList>
                                </td>
    </tr>
     
    <tr>
         <td align="right"><b>Education Qua:</b></td>
        <td align="left">
                  <asp:DropDownList ID="EducationDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="41" 
                      meta:resourcekey="holderEducationDropDownListResource1">
            <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource31">-----</asp:ListItem>
            <asp:ListItem Value="Primary" meta:resourcekey="ListItemResource32">Primary</asp:ListItem>
            <asp:ListItem Value="S. S. C." meta:resourcekey="ListItemResource33">S. S. C.</asp:ListItem>            
                      <asp:ListItem meta:resourcekey="ListItemResource34">H. S. C.</asp:ListItem>
                      <asp:ListItem Value="GRADUATE" meta:resourcekey="ListItemResource35">Graduate</asp:ListItem>
                      <asp:ListItem Value="POST GRADUATE" meta:resourcekey="ListItemResource36">Post Graduate</asp:ListItem>
            </asp:DropDownList>
                                </td>
              <td align="right"><b>Remarks:</b></td>
              <td align="left">
             <asp:TextBox ID="RemarksTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="128" TabIndex="42" 
                      meta:resourcekey="holderRemarksTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
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
            <legend style="font-weight: 700"> ::Principal Holder&#39;s Bank Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
             <tr>
        <td align="right" ><b>Is Bank Information:</b></td>
        <td align="left" >
            
         <asp:RadioButton ID="BankNORadioButton" runat="server" Checked="True" onclick="fnEnable('BankNORadioButton');"
                Font-Bold="True" GroupName="bankType" Text="NO" TabIndex="43" />
             <asp:RadioButton ID="BankYesRadioButton" runat="server" Font-Bold="True" onclick="fnEnable('BankYesRadioButton');"
                GroupName="bankType" Text="YES" TabIndex="4" /> </td>   
                <td align="left" colspan="2">   
                <div id="divBEFTN" runat="server"  style="visibility:hidden">         
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <b>Is BEFTN:&nbsp; </b></td>
                            <td>
                                  <asp:RadioButton ID="bftnYesRadioButton" runat="server" Font-Bold="True" Checked="True" 
                                  GroupName="bftn" Text="YES" TabIndex="33" />
                              <asp:RadioButton ID="bftnNoRadioButton" runat="server" 
                                  Font-Bold="True" GroupName="bftn" Text="NO" TabIndex="34" />
                        </td>
                        </tr>
                        
                    </table>
                    </div> 
                </td>
             
       
    </tr>
     <tr>
        <td align="left" colspan="4" >
        <div  id="DivBank" runat="server"  style="visibility:visible">
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">                                                               
        <ContentTemplate>--%>
            <table cellpadding="0" cellspacing="0" width="100%">
            <colgroup width="100"></colgroup>
            <colgroup width="350"></colgroup>
            <colgroup width="120"></colgroup>
                <tr>
                    <td align="right" >
                        <b>Account No:</b></td>
                    <td align="left">
                        <asp:TextBox ID="bankAccTextBox" runat="server" 
                    CssClass="textInputStyle" onkeypress= "fncInputNumericValuesOnly()" MaxLength="13" 
                            TabIndex="35" Width="218px" ></asp:TextBox>  
                    </td>
                    <td align="right" >  <b>Routing Number:</b></td>
                    <td align="left">
                        <asp:TextBox ID="routingNoTextBox" runat="server" CssClass="textInputStyle" 
                            MaxLength="16"  onkeypress= "fncInputNumericValuesOnly()" TabIndex="6" 
                            Width="218px"></asp:TextBox>
                        <asp:Button   ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            OnClientClick="return fnCheckRouterNo();" onclick="findButton_Click" 
                            AccessKey="f" TabIndex="47"/>
                    </td>
                </tr>
                 <tr>
                    <td align="right" >
                        <b>&nbsp;Bank Name:</b></td>
                    <td align="left">
                        <asp:DropDownList ID="bankNameDropDownList" runat="server" AutoPostBack="True" 
                             meta:resourcekey="bankNameDropDownListResource1" 
                            onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" 
                            TabIndex="48">
                        </asp:DropDownList>
                     </td>
                    <td align="right" >  <b>Bank Branch:</b></td>
                    <td align="left">
                        <asp:DropDownList ID="branchNameDropDownList" runat="server" TabIndex="49"  AutoPostBack="True" 
                            onselectedindexchanged="branchNameDropDownList_SelectedIndexChanged">
                        </asp:DropDownList>
                                                </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Address:</b></td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="bankAddressTextBox" runat="server" 
                            CssClass="TextInputStyleLarge" Enabled="False" Height="44px" TabIndex="50" 
                            TextMode="MultiLine" Width="398px"></asp:TextBox>
                                                </td>
                   
                </tr>
            </table>
            <%-- </ContentTemplate>
                  <Triggers>                   
                    <asp:AsyncPostBackTrigger ControlID="bankNameDropDownList"  EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="branchNameDropDownList"  EventName="SelectedIndexChanged"/>
                  </Triggers>
                  
              </asp:UpdatePanel>--%>
         </div>
        
        </td>
                
    </tr>
            </table>
            </fieldset>
        </td>
                   
    </tr>
     
     <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
    </table>
    
   </div>
   
 
    <table width="500" align="center" cellpadding="0" cellspacing="0">
    
     <tr>
        <td align="right">
        <asp:Button ID="regSaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="regSaveButton_Click" TabIndex="51" AccessKey="s" 
                meta:resourcekey="regSaveButtonResource1"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset"  OnClientClick="return fnReset();"
                CssClass="buttoncommon"   AccessKey="r" 
                meta:resourcekey="regResetButtonResource1" TabIndex="52" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" 
                meta:resourcekey="regCloseButtonResource1" TabIndex="53" 
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

