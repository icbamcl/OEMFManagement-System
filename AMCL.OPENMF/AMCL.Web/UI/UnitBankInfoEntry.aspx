<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitBankInfoEntry.aspx.cs" Inherits="UI_UnitBankInfoEntry" Title="Registration Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
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
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderFMTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderMotherTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderOccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=holderNationalityTextBox.ClientID%>").value ="BANGLADESHI";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderCityTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderEmailTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderSexDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=holderDateofBirthTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderMaritialStatusDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=holderReligionDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=holderEducationDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=holderRemarksTextBox.ClientID%>").value =""; 
            
                        
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
           
         
             if(document.getElementById("<%=isCIPDropDownList.ClientID%>").value =="0")
             {
                 document.getElementById("<%=isCIPDropDownList.ClientID%>").focus();
                        alert("Please Select CIP");
                        return false;
             }
             
            if(document.getElementById("<%=holderNameTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderNameTextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder Name");
                return false;
                
            }
            
          if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="01")
           {            
               if(document.getElementById("<%=holderFMTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=holderFMTextBox.ClientID%>").focus();
                    alert("Please Enter Unit Holder's Father/Mother/Wife/Husband Name");
                    return false;
                    
                }
            }
           if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="01")
           {            
               if(document.getElementById("<%=holderOccupationDropDownList.ClientID%>").value =="0")
                {
                    document.getElementById("<%=holderOccupationDropDownList.ClientID%>").focus();
                    alert("Please Select Unit Holder's Service");
                    return false;
                    
                }
            }
         
            
            if(document.getElementById("<%=holderNationalityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderNationalityTextBox.ClientID%>").focus();
                alert("Please Unit Holder's Enter Nationality");
                return false;
                
            }
        
            if(document.getElementById("<%=holderAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Address Line1");
                return false;
                
            }
            if(document.getElementById("<%=holderAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Address Line2");
                return false;
                
            }
           if(document.getElementById("<%=holderAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Unit Holder's Address Line2");
                return false;
                
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
         
         }
       
        function fncInputNumericValuesOnly()
	    {
		    if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		    {
		        document.getElementById("<%=regNoTextBox.ClientID%>").focus();
		        alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		    }
	    }
	    
</script>



    <style type="text/css">
    
       
        .style1
        {
            font-size: small;
            font-weight: bold;
            color: #993333;
            text-decoration: underline;
        }
        .style2
        {
            color: #993333;
        }
    
       
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
      <div id="divContent" runat="server" style="border:solid 1px #AABBCC;width:980px; height:auto"  align="center">
 <table width="970px" align="center" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="150"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
 <tr>
        <td colspan="4" align="left">
            <table width="650px" align="left" cellpadding="0" cellspacing="0" border="0" >
          
                <tr>
                    <td align="left" style=" background-color: #CCCCFF; height:25px"  id="tdReg" runat="server"> &nbsp; 
                       <asp:HyperLink ID="regHolderHL" runat="server" 
                            NavigateUrl="~/UI/UnitRegEntry.aspx" Font-Bold="True" >  Principal Holder Information</asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px" id="tdJoint" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/UI/UnitJointEntry.aspx" Font-Bold="True">Joint Holder Information</asp:HyperLink></td>
                    <td style=" background-color: #CCCCFF; height:25px" id="tdNominee" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/UI/UnitNomineeEntry.aspx" Font-Bold="True">Nominee Information</asp:HyperLink>
                    </td >
                    <td  id="tdBank" style=" background-color: #547DD3; height:25px" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink3" runat="server" 
                            NavigateUrl="~/UI/UnitBankInfoEntry.aspx" Font-Bold="True" ForeColor="White">Bank Information </asp:HyperLink>
                    </td>
                </tr>
           </table>
        </td>
        
    </tr>
     <tr>
        <td colspan="4" align="left">
        &nbsp;
        </td>
        
   
        
    </tr>
    <tr>
        <td colspan="4" align="left"  
            style=" font-size: small;  text-decoration: underline " class="style2"><strong>::Unit Holder Registration Information::</strong></td>
        
    </tr>
     <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
   
    <tr>
        <td align="right" ><b>Registration No:</b></td>
        <td align="left">
            <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="fundCodeTextBoxResource1" Width="52px"></asp:TextBox>
            <b>/</b><asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="branchCodeTextBoxResource1" Width="52px"></asp:TextBox>
            <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="5"
                CssClass= "TextInputStyleSmall" TabIndex="1" Width="89px"              
                ontextchanged="regNoTextBox_TextChanged" onkeypress= "fncInputNumericValuesOnly()" 
               AutoPostBack="True" 
                meta:resourcekey="regNoTextBoxResource1"></asp:TextBox>
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
                Font-Bold="True" GroupName="regType" Text="Individual" />
            <asp:RadioButton ID="CompRadioButton" runat="server" Font-Bold="True" 
                GroupName="regType" Text="Company" />
            &nbsp;
            <asp:DropDownList ID="regTypeDropDownList" runat="server" 
                CssClass="DropDownList" TabIndex="4" 
                meta:resourcekey="regTypeDropDownListResource1">
            
            </asp:DropDownList><span class="star">*</span></td>  
             
        <td align="right"><b>Is CIP:</b></td>
        <td align="left">
                  <asp:DropDownList ID="isCIPDropDownList" runat="server" 
             CssClass="DropDownList" TabIndex="4" EnableViewState="False" 
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
                Font-Bold="True" GroupName="idType" Text="NO" />
             <asp:RadioButton ID="IDYesRadioButton" runat="server" Font-Bold="True" onclick="fnEnable('IDYesRadioButton');"
                GroupName="idType" Text="YES" /> </td>   
                <td align="left" colspan="2">                
                </td>
             
       
    </tr>
    <tr>
        <td align="left" colspan="4" >
        <div  id="dvIDInof" runat="server" style="visibility:hidden">
        <asp:UpdatePanel ID="IDBankNameUpdatePanel" runat="server">                                                               
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0">
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
                <tr>
                    <td align="right" >
                        <b>ID Account No:</b></td>
                    <td align="left">
                        <asp:TextBox ID="IDAccNoTextBox" runat="server" 
                    CssClass="textInputStyle" onkeypress= "CheckIDStatus()" MaxLength="16" 
                            TabIndex="5" Width="218px" ></asp:TextBox>  
                    </td>
                    <td align="right" >  <b>ID Institution Name:</b></td>
                    <td align="left">
                    <asp:DropDownList ID="IDbankNameDropDownList" runat="server"  
                         AutoPostBack="true" 
                         onselectedindexchanged="IDbankNameDropDownList_SelectedIndexChanged"  
                         TabIndex="5" ></asp:DropDownList>  
                                                </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>ID Institution Branch:</b></td>
                    <td align="left" colspan="3">
                     <asp:DropDownList ID="IDbranchNameDropDownList" runat="server" TabIndex="5"></asp:DropDownList>               
                                                </td>
                   
                </tr>
            </table>
         </ContentTemplate>
                  <Triggers>                   
                    <asp:AsyncPostBackTrigger ControlID="IDbankNameDropDownList"  EventName="SelectedIndexChanged"/>
                  </Triggers>
                  
              </asp:UpdatePanel>
         </div>
        
        </td>
                
    </tr>
     
    <tr>
        <td align="left"   colspan="4" class="style1" >::Principal Holder Information::</td>        
                
    </tr>
    <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
    <tr>
        <td align="right" ><b>Name of Holder:</b></td>
        <td align="left">
            <asp:TextBox ID="holderNameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="6" 
                meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
        
        <td align="right" >
            <b>Father/Husband&#39;s Name:</b></td>
       <td align="left">
            <asp:TextBox ID="holderFMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
    </tr>
    
     <tr>
         <td align="right" ><b>Mother&#39;s Name:</b></td>
        <td align="left">
            <asp:TextBox ID="holderMotherTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
            <span class="star">*</span></td>
          <td align="right" ><b>Occupation:</b></td>
        <td align="left">
            <asp:DropDownList ID="holderOccupationDropDownList" runat="server" TabIndex="8" 
                meta:resourcekey="holderOccupationDropDownListResource1">
            </asp:DropDownList>
            <span class="star">*</span></td>
    </tr>
   
    
    <tr>
       <td align="right" ><b>Address1:</b></td>
        <td align="left">
            <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="10" 
                meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
       <td align="right" ><b>Address2:</b></td>
        <td align="left">
            <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="11" ></asp:TextBox>
                                <span class="star">*</span></td>
     
    </tr>
     <tr>
       <td align="right" ><b>City:</b></td>
        <td align="left">
             <asp:TextBox ID="holderCityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="35" TabIndex="12" 
                      meta:resourcekey="holderCityTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
        <td align="right"  ><b>Nationality:</b></td>
        <td align="left">
            <asp:TextBox ID="holderNationalityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="9" meta:resourcekey="holderNationalityTextBoxResource1" 
               >BANGLADESHI</asp:TextBox>
                                <span class="star">*</span></td>
     
    </tr>
    <tr>
     <td align="right" ><b>Date of Birth:</b></td>
        <td align="left">
            <asp:TextBox ID="holderDateofBirthTextBox" runat="server" 
                CssClass="textInputStyleDate" TabIndex="15" 
                meta:resourcekey="holderDateofBirthTextBoxResource1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="hDofBCalendarExtender" runat="server" 
                TargetControlID="holderDateofBirthTextBox" PopupButtonID="hDofBImageButton" 
                Format="dd-MMM-yyyy" Enabled="True"/>
            <asp:ImageButton ID="hDofBImageButton" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="16" 
                meta:resourcekey="hDofBImageButtonResource1" />
                                <span class="star">*</span></td>  
        <td align="right">
            <asp:RadioButton ID="NIDRadioButton" runat="server" Checked="True" 
                Font-Bold="True" GroupName="attachment" Text="NID/" />
            <asp:RadioButton ID="passportRadioButton" runat="server" Font-Bold="True" 
                GroupName="attachment" Text="Passport/" />
            <br />
            <asp:RadioButton ID="birthRadioButton" runat="server" Font-Bold="True" 
                Text="Birth Cert No:"  />
        </td>
        <td align="left">
            <asp:TextBox ID="nomi1Address1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="31" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                </td>
    </tr>   
  
    <tr>
              <td align="right"><b>Telephone/Mobile:</b></td>
              <td align="left">
             <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13" 
                      meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="right"><b>E-Mail:</b></td>
        <td align="left">
             <asp:TextBox ID="holderEmailTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13" 
                      meta:resourcekey="holderEmailTextBoxResource1"></asp:TextBox>
                                    </td>
    </tr>
    
    <tr>
              <td align="right"><b>Sex:</b></td>
              <td align="left">
                  <asp:DropDownList ID="holderSexDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="14" 
                      meta:resourcekey="holderSexDropDownListResource1"> 
            <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource18">-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource19" >Male</asp:ListItem>
            <asp:ListItem Value="F" meta:resourcekey="ListItemResource20">Female</asp:ListItem>            
            </asp:DropDownList>
                                </td>
        <td align="right"><b>Maritial Status:</b></td>
        <td align="left">
                  <asp:DropDownList ID="holderMaritialStatusDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="17" 
                      meta:resourcekey="holderMaritialStatusDropDownListResource1">
             <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource21">-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource22">Married</asp:ListItem>
            <asp:ListItem Value="U" meta:resourcekey="ListItemResource23">Unmarried</asp:ListItem>            
                      <asp:ListItem Value="O" meta:resourcekey="ListItemResource24">Others</asp:ListItem>
            </asp:DropDownList>
                                </td>
    </tr>
    
   
      <tr>
              <td align="right"><b>Religion:</b></td>
              <td align="left">
                  <asp:DropDownList ID="holderReligionDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="18" 
                      meta:resourcekey="holderReligionDropDownListResource1">
             <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource25" >-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource26" >Muslim</asp:ListItem>
            <asp:ListItem Value="H" meta:resourcekey="ListItemResource27">Hindu</asp:ListItem>            
                      <asp:ListItem Value="C" meta:resourcekey="ListItemResource28">Christian</asp:ListItem>
                      <asp:ListItem Value="B" meta:resourcekey="ListItemResource29">Buddah</asp:ListItem>
                      <asp:ListItem Value="O" meta:resourcekey="ListItemResource30">Others</asp:ListItem>
            </asp:DropDownList>
                                </td>
        <td align="right"><b>Education Qua:</b></td>
        <td align="left">
                  <asp:DropDownList ID="holderEducationDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="19" 
                      meta:resourcekey="holderEducationDropDownListResource1">
            <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource31">-----</asp:ListItem>
            <asp:ListItem Value="Primary" meta:resourcekey="ListItemResource32">Primary</asp:ListItem>
            <asp:ListItem Value="S. S. C." meta:resourcekey="ListItemResource33">S. S. C.</asp:ListItem>            
                      <asp:ListItem meta:resourcekey="ListItemResource34">H. S. C.</asp:ListItem>
                      <asp:ListItem Value="GRADUATE" meta:resourcekey="ListItemResource35">Graduate</asp:ListItem>
                      <asp:ListItem Value="POST GRADUATE" meta:resourcekey="ListItemResource36">Post Graduate</asp:ListItem>
            </asp:DropDownList>
                                </td>
    </tr>
     
    <tr>
              <td align="right"><b>Remarks:</b></td>
              <td align="left" colspan="3">
             <asp:TextBox ID="holderRemarksTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="20" 
                      meta:resourcekey="holderRemarksTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
    </table>
    
   </div>
    <br />
    <table width="500" align="center" cellpadding="0" cellspacing="0">
    
     <tr>
        <td align="right">
        <asp:Button ID="regSaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="regSaveButton_Click" TabIndex="47" AccessKey="s" 
                meta:resourcekey="regSaveButtonResource1"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset"  OnClientClick="return fnReset();"
                CssClass="buttoncommon"   AccessKey="r" 
                meta:resourcekey="regResetButtonResource1" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" AccessKey="c" meta:resourcekey="regCloseButtonResource1" 
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

