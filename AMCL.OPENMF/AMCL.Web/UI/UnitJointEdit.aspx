<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitJointEdit.aspx.cs" Inherits="UI_UnitJointEdit" Title="Registration Edit Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
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
            document.getElementById("<%=TelphoneTextBox.ClientID%>").value ="";
            document.getElementById("<%=SexDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=DateofBirthTextBox.ClientID%>").value ="";
            document.getElementById("<%=MaritialStatusDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=ReligionDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=EducationDropDownList.ClientID%>").value ="0";            
            document.getElementById("<%=RemarksTextBox.ClientID%>").value =""; 
             
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
            
          
            if(document.getElementById("<%=NameTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=NameTextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder Name");
                return false;
                
            }
            
          if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="AMC/01")
           {            
               if(document.getElementById("<%=FMTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=FMTextBox.ClientID%>").focus();
                    alert("Please Enter Joint Holder's Father");
                    return false;
                    
                }
                if(document.getElementById("<%=MotherTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=MotherTextBox.ClientID%>").focus();
                    alert("Please Enter Joint Holder's Mother Name");
                    return false;
                    
                }
               if(document.getElementById("<%=OccupationDropDownList.ClientID%>").value =="0")
                {
                    document.getElementById("<%=OccupationDropDownList.ClientID%>").focus();
                    alert("Please Select Joint Holder's Service");
                    return false;
                    
                }    
                              
                      
             }
        
            
            if(document.getElementById("<%=NationalityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=NationalityTextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's  Nationality");
                return false;
                
            }
             if(document.getElementById("<%=NIDTextBox.ClientID%>").value =="" && document.getElementById("<%=TINTextBox.ClientID%>").value =="" && document.getElementById("<%=passportTextBox.ClientID%>").value =="" && document.getElementById("<%=birthCertNoTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=NIDTextBox.ClientID%>").focus();
                    alert("Please Enter NID or TIN or Passport Number or Birth Certificate Number");
                    return false;
                    
                }
            if(document.getElementById("<%=presentAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=presentAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's Present Address Line1");
                return false;
                
            }
            if(document.getElementById("<%=presentAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=presentAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's Present Address Line2");
                return false;
                
            }
           if(document.getElementById("<%=presentCityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=presentCityTextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's Present City");
                return false;
                
            }
            
            
            if(document.getElementById("<%=parmanAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=parmanAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's Parmanent Address Line1");
                return false;
                
            }
            if(document.getElementById("<%=parmanAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=parmanAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's Parmanent Address Line2");
                return false;
                
            }
           if(document.getElementById("<%=parmentCityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=parmentCityTextBox.ClientID%>").focus();
                alert("Please Enter Joint Holder's Parmanent City");
                return false;
                
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
        .style6
        {
            height: 20px;
        }
        </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="center">
        Unit Joint Holder Edit Form (<span id="spanFundName" runat="server"></span>)
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
            <table align="left" cellpadding="0" cellspacing="0" border="0" 
                style="width: 823px" >
          
                <tr>
                    <td align="left" style=" background-color: #CCCCFF; " id="tdReg" runat="server" 
                        class="style4"> &nbsp; 
                       <asp:HyperLink ID="regHolderHL" runat="server" 
                            NavigateUrl="~/UI/UnitRegEdit.aspx" Font-Bold="True">  Principal Holder Information</asp:HyperLink>
                    </td>
                    <td style=" background-color: #547DD3; "  id="tdJoint" runat="server" 
                        class="style5">
                  
                    <span class="style3">|</span><asp:HyperLink ID="HyperLink1" runat="server" 
                            NavigateUrl="~/UI/UnitJointEdit.aspx" Font-Bold="True" ForeColor="White">Joint Holder Information</asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px" id="tdNominee" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/UI/UnitNomineeEdit.aspx" Font-Bold="True">Nominee Information</asp:HyperLink>
                    </td >
                   <td style=" background-color: #CCCCFF; height:25px" id="td1" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink3" runat="server" 
                            NavigateUrl="~/UI/UnitSIPEdit.aspx" Font-Bold="True">SIP Information</asp:HyperLink>
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
                    meta:resourcekey="findButtonResource1" onclick="findButton_Click" 
                    onclientclick="return fnCheckRegNo();" TabIndex="2" Text="Find" />
                </span></span></td>
        <td align="right"><b>Name of The Peincipal Holder:</b></td>
        <td align="left">
          &nbsp;  <asp:Label ID="NameLabel" runat="server" Text="" 
                style="font-weight: 700; font-size: medium;"></asp:Label>
                                                    </td> 
       
    </tr>
    <tr>
        <td align="right" ><b>Registration Date:</b></td>
        <td align="left">
          &nbsp;  <asp:Label ID="DateLabel" runat="server" Text="" 
                style="font-weight: 700; font-size: medium;"></asp:Label>
                                                    </td>  
             
        <td align="right"><b>Registration Type :</b></td>
        <td align="left">
                &nbsp;  <asp:Label ID="TypeLabel" runat="server" Text="" 
                    style="font-weight: 700; font-size: medium;"></asp:Label>
                                                    </td>
    </tr>
     <tr>
        <td align="right" ><b>CIP:</b></td>
        <td align="left" >
            
           &nbsp; <asp:Label ID="CIPLabel" runat="server" Text="" 
                style="font-weight: 700; font-size: medium;"></asp:Label>
                                                    </td>   
                <td align="right"> <b>Is ID Account:</b>   </td>        
                
                 <td align="left"> &nbsp; <asp:Label ID="IDLabel" runat="server" Text="" 
                         style="font-weight: 700; font-size: medium;"></asp:Label>    </td>
             
       
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
            <legend style="font-weight: 700"> ::Joint Holder Information::</legend>
            <br />
             <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
      <tr>
        <td align="right" ><b>&nbsp;Joint Holder Name:</b></td>
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
                                </span></td>
    </tr>
   
      <tr>
        <td align="right"  ><b>Occupation:</b></td>
        <td align="left" colspan="3">
            <asp:DropDownList ID="OccupationDropDownList" runat="server" TabIndex="18" 
                meta:resourcekey="holderOccupationDropDownListResource1">
            </asp:DropDownList>
                                <span class="star">*</span></td>
        
       
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
                CssClass= "TextInputStyleLarge" MaxLength="120" TabIndex="23" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right"  ><b>TIN certificate No:</b></td>
        <td align="left">
            <asp:TextBox ID="TINTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="24" 
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
     <td  colspan="2"align="left"  >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <b>Present Address:</b></td>
     <td  colspan="2"align="center" >
         <asp:CheckBox ID="parmenentCheckBox" runat="server" Text="As Present Address" onclick="fnGetPresenAddress('parmenentCheckBox');"
             Font-Bold="False" style="font-weight: 700" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>&nbsp;&nbsp; Permanent Address</b></td>
        
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
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="30" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
     
    </tr>
     <tr>
      <td align="right" class="style6"  ><b>Address2:</b></td>
        <td align="left" class="style6">
            <asp:TextBox ID="presentAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="120" TabIndex="28" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                                    </td>
        <td align="right" class="style6"  ><b>Address2:</b></td>
        <td align="left" class="style6">
            <asp:TextBox ID="parmanAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="31" 
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
            
              <td align="right"><b>Mobile:</b></td>
              <td align="left">
             <asp:TextBox ID="TelphoneTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="34" 
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
            
              <td align="right"><b>Source of Fund:</b></td>
              <td align="left">
             <asp:TextBox ID="SourceFundTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="256" TabIndex="36" 
                      meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="right"><b>Web Address:</b></td>
        <td align="left">
             <asp:TextBox ID="webaddressTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="37" 
                      meta:resourcekey="holderEmailTextBoxResource1"></asp:TextBox>
                                    </td>
    </tr>
    <tr>
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
    </tr>
   
   
      <tr>
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
    </tr>
     
    <tr>
              <td align="right"><b>Remarks:</b></td>
              <td align="left" colspan="3">
             <asp:TextBox ID="RemarksTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="42" 
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
   
     
     
    </table>
    
   </div>
   
    <br />
    <table width="500" align="center" cellpadding="0" cellspacing="0">
    
     <tr>
        <td align="right">
        <asp:Button ID="regSaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="regSaveButton_Click" TabIndex="41" AccessKey="s" 
                meta:resourcekey="regSaveButtonResource1"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;<asp:Button ID="regDeleteButton" runat="server" 
                Text="Delete" CssClass="buttoncommon" OnClientClick="return  fnCheckRegNo();"
                TabIndex="41" AccessKey="s" 
                meta:resourcekey="regSaveButtonResource1" onclick="regDeleteButton_Click"/>&nbsp;
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

