<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitNomineeEdit.aspx.cs" Inherits="UI_UnitNomineeEdit" Title="Registration Edit Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(confirm)
        {
            
            document.getElementById("<%=nomiNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=nomiFMTextBox.ClientID%>").value ="";
            document.getElementById("<%=nomiMotherTextBox.ClientID%>").value ="";
            document.getElementById("<%=nomiOccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=nomiNationalityTextBox.ClientID%>").value ="BANGLADESHI";
            document.getElementById("<%=nomiAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=nomiAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=nomiCityTextBox.ClientID%>").value ="";
                   
            document.getElementById("<%=nomiRemarksTextBox.ClientID%>").value =""; 
           
            
                 
             
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
            
          
            if(document.getElementById("<%=nomiNameTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=nomiNameTextBox.ClientID%>").focus();
                alert("Please Enter Unit Nmominee Name");
                return false;
                
            }
            
          if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="AMC/01")
           {            
               if(document.getElementById("<%=nomiFMTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=nomiFMTextBox.ClientID%>").focus();
                    alert("Please Enter Unit Nominee's Father/Wife/Husband Name");
                    return false;
                    
                }
                if(document.getElementById("<%=nomiMotherTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=nomiMotherTextBox.ClientID%>").focus();
                    alert("Please Enter Unit Nominee's Mother Name");
                    return false;
                    
                }
               if(document.getElementById("<%=nomiOccupationDropDownList.ClientID%>").value =="0")
                {
                    document.getElementById("<%=nomiOccupationDropDownList.ClientID%>").focus();
                    alert("Please Select Unit Nominee's Service");
                    return false;
                    
                }    
                              
                      
             }
        
            
            if(document.getElementById("<%=nomiNationalityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=nomiNationalityTextBox.ClientID%>").focus();
                alert("Please Unit Nominee's Enter Nationality");
                return false;
                
            }
        
            if(document.getElementById("<%=nomiAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=nomiAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Unit Nominee's Address Line1");
                return false;
                
            }
            if(document.getElementById("<%=nomiAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=nomiAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Unit Nominee's Address Line2");
                return false;
                
            }
           if(document.getElementById("<%=nomiCityTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=nomiCityTextBox.ClientID%>").focus();
                alert("Please Enter Unit Nominee's City");
                return false;
                
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
	   if(Action.indexOf("noMinorRadioButton")!=-1)
            {   
                if( document.getElementById("<%=noMinorRadioButton.ClientID%>").checked)                
                {       
                                                        
                    
                      if(document.getElementById("<%=DivGardian.ClientID%>").style.visibility == "visible")
                      {
                    
                        document.getElementById("<%=DivGardian.ClientID%>").style.visibility = "hidden";
                      }
                    
                }
                
             }
          if(Action.indexOf("yesMinorRadioButton")!=-1)
            {   
                if( document.getElementById("<%=yesMinorRadioButton.ClientID%>").checked)                
                {       
                                                                             
                      if(document.getElementById("<%=DivGardian.ClientID%>").style.visibility == "hidden")
                      {
                        
                         document.getElementById("<%=DivGardian.ClientID%>").style.visibility = "visible";
                      }
                  
                      
                }
                
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
        </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center" runat="server" id="tableFundName">
        <tr>
            <td class="FormTitle" align="center">
        Unit Holder Nominee Edit Form (<span id="spanFundName" runat="server"></span>)
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
                style="width: 823px" >
          
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
                    <td style=" background-color: #547DD3; "  id="tdNominee" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="HyperLink2" runat="server" 
                            NavigateUrl="~/UI/UnitNomineeEdit.aspx" Font-Bold="True" ForeColor="White">Nominee Information</asp:HyperLink>
                    </td >
                    <td align="left" style=" background-color: #CCCCFF; " id="td1" runat="server" 
                        class="style4"> <span class="style3">|</span> <asp:HyperLink ID="HyperLink3" runat="server" 
                            NavigateUrl="~/UI/UnitSIPEdit.aspx" Font-Bold="True"> SIP Information  </asp:HyperLink>
                    </td>
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
            <legend style="font-weight: 700"> ::Nominee Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
              <tr>
              <td align="right" ><b>&nbsp;Nominee Number:</b></td>
        <td align="left">
        
            <asp:DropDownList ID="NomiNumberDropDownList" runat="server" 
                CssClass="DropDownList" 
                onselectedindexchanged="NomiNumberDropDownList_SelectedIndexChanged" 
                AutoPostBack="True" >
                                  
            </asp:DropDownList>
                                                   
                                            
                                                   
                                                    </td>
        <td align="right" ><b>&nbsp;Control Number:</b></td>
        <td align="left">
            <asp:TextBox ID="controlNumberTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="3" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                                                    </td>
        
    </tr>
             <tr>
        <td align="right" ><b>Nominee Type:</b></td>
        <td align="left">
            <asp:DropDownList ID="TypeDropDownList" runat="server" 
                CssClass="DropDownList" TabIndex="4" 
                meta:resourcekey="nomi1RelationDropDownListResource1">
             <asp:ListItem Value="N" Selected="True">NOMINEE</asp:ListItem>
            <asp:ListItem Value="S">SUCCESSOR</asp:ListItem>
           
          
            </asp:DropDownList>
                                <span class="star">*</span></td>
        
        <td align="right" >
            <b>Nominee Number:</b></td>
       <td align="left">
            <asp:TextBox ID="nomiNumberTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="10" TabIndex="5" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
    </tr>
              <tr>
        <td align="right" ><b>Name of the Nominee:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiNameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="13"> </asp:TextBox>
                                <span class="star">*</span></td>
        
        <td align="right" >
            <b>Father/Husband&#39;s Name:</b></td>
       <td align="left">
            <asp:TextBox ID="nomiFMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="15" >
               </asp:TextBox>
                                <span class="star">*</span></td>
    </tr>
    
     <tr>
         <td align="right" ><b>Mother&#39;s Name:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiMotherTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="16" >
              </asp:TextBox>
            <span class="star">*</span></td>
          <td align="right" ><b>Occupation:</b></td>
        <td align="left">
            <asp:DropDownList ID="nomiOccupationDropDownList" runat="server" TabIndex="17" >
            </asp:DropDownList>
            <span class="star">*</span></td>
    </tr>
   
    
    <tr>
       <td align="right" ><b>Address1:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiAddress1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="18" 
              ></asp:TextBox>
                                <span class="star">*</span></td>
       <td align="right" ><b>Address2:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="19" ></asp:TextBox>
                                <span class="star">*</span></td>
     
    </tr>
     <tr>
       <td align="right" ><b>City:</b></td>
        <td align="left">
             <asp:TextBox ID="nomiCityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="35" TabIndex="20" 
                     ></asp:TextBox>
                                <span class="star">*</span></td>
        <td align="right"  ><b>Nationality:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiNationalityTextBox" runat="server" Text="BANGLADESHI"
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="21" 
               ></asp:TextBox>
                                <span class="star">*</span></td>
     
    </tr>
    <tr>
     <td align="right" ><b>Date of Birth:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiDateofBirthTextBox" runat="server" 
                CssClass="textInputStyleDate" TabIndex="22" 
               ></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="hDofBCalendarExtender" runat="server" 
                TargetControlID="nomiDateofBirthTextBox" PopupButtonID="hDofBImageButton" 
                Format="dd-MMM-yyyy" Enabled="True"/>
            <asp:ImageButton ID="hDofBImageButton" runat="server" AlternateText="Click Here" 
                ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="22" 
                meta:resourcekey="hDofBImageButtonResource1" />
                                    </td>  
        <td align="right">
            <b>Age:</b></td>
        <td align="left">
            <asp:TextBox ID="nomiAgeTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="23" 
               ></asp:TextBox>
     
                                                    </td>
    </tr>   
  
   
      <tr>
              <td align="right"><b>Relation with Holder:</b></td>
              <td align="left">
            <asp:DropDownList ID="RelationDropDownList" runat="server" 
                CssClass="DropDownList" TabIndex="33" 
                meta:resourcekey="nomi1RelationDropDownListResource1">
             <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource8">----</asp:ListItem>
            <asp:ListItem Value="FATHER" meta:resourcekey="ListItemResource9">Father</asp:ListItem>
            <asp:ListItem Value="MOTHER" meta:resourcekey="ListItemResource10">Mother</asp:ListItem>
            <asp:ListItem Value="WIFE" meta:resourcekey="ListItemResource11">Wife</asp:ListItem>
            <asp:ListItem Value="HUSBAND" meta:resourcekey="ListItemResource12">Husband</asp:ListItem>
            <asp:ListItem Value="SON" meta:resourcekey="ListItemResource13">Son</asp:ListItem>
            <asp:ListItem Value="DAUGHTER" meta:resourcekey="ListItemResource14">Daughter</asp:ListItem>
            <asp:ListItem Value="COUSINE" meta:resourcekey="ListItemResource15">Cousine</asp:ListItem>
            <asp:ListItem Value="FRIEND" meta:resourcekey="ListItemResource16">Friend</asp:ListItem>
            
            <asp:ListItem Value="SISTER">Sister</asp:ListItem>
            <asp:ListItem Value="BROTHER">Brother</asp:ListItem>
            <asp:ListItem Value="OTHERS" meta:resourcekey="ListItemResource17">OTHERS</asp:ListItem>
            </asp:DropDownList>
                                </td>
        <td align="right"><b>Percentage(%):</b></td>
        <td align="left">
            <asp:TextBox ID="percentageTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="23" 
               ></asp:TextBox>
     
                                <span class="star">*</span></td>
    </tr>
     
    <tr>
              <td align="right"><b>Remarks:</b></td>
              <td align="left" colspan="3">
             <asp:TextBox ID="nomiRemarksTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="30" 
                     ></asp:TextBox>
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
            <legend style="font-weight: 700"> ::Guardian Details (If Nominee is Minor::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
        <tr>
                    <td align="right">
                        <b>Nominee is Minor:</b></td>
                    <td align="left">
                         <asp:RadioButton ID="noMinorRadioButton" runat="server" Checked="True" onclick="fnEnable('noMinorRadioButton');"
                Font-Bold="True" GroupName="nomiType" Text="NO" TabIndex="31" />
             <asp:RadioButton ID="yesMinorRadioButton" runat="server" Font-Bold="True" onclick="fnEnable('yesMinorRadioButton');"
                GroupName="nomiType" Text="YES" TabIndex="32" /></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                   
      </tr>     
     <tr>
        <td align="left" colspan="4" >
        <div  id="DivGardian" runat="server"  style="visibility:hidden">
                                                                     
      
            <table cellpadding="0" cellspacing="0" width="100%">
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
           
                <tr>
                    <td align="right" >
                        <b>Guardian Name:</b></td>
                    <td align="left">
                        <asp:TextBox ID="gardianNameTextBox" runat="server" 
                            CssClass="TextInputStyleLarge" MaxLength="155" 
                            meta:resourcekey="holderNameTextBoxResource1" TabIndex="31"></asp:TextBox>
                        <span class="star">*</span></td>
                    <td align="right" >  <b>Relation with Nominee:</b></td>
                    <td align="left">
                        <asp:DropDownList ID="gardianRelationDropDownList" runat="server" 
                            CssClass="DropDownList" 
                            TabIndex="33">
                            <asp:ListItem  Selected="True" Value="0">----</asp:ListItem>
                            <asp:ListItem Value="FATHER">Father</asp:ListItem>
                            <asp:ListItem  Value="MOTHER">Mother</asp:ListItem>
                            <asp:ListItem  Value="WIFE">Wife</asp:ListItem>
                            <asp:ListItem  Value="HUSBAND">Husband</asp:ListItem>
                            <asp:ListItem Value="SON">Son</asp:ListItem>
                            <asp:ListItem  Value="DAUGHTER">Daughter</asp:ListItem>
                            <asp:ListItem Value="COUSINE">Cousine</asp:ListItem>
                            <asp:ListItem  Value="FRIEND">Friend</asp:ListItem>
                            <asp:ListItem Value="SISTER">Sister</asp:ListItem>
                            <asp:ListItem Value="BROTHER">Brother</asp:ListItem>
                            <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        <b>Date of Birth:</b></td>
                    <td align="left">
                       <asp:TextBox ID="gardianBirthDateTextBox" runat="server" 
                        CssClass="textInputStyleDate" TabIndex="34"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                        TargetControlID="gardianBirthDateTextBox" PopupButtonID="gDofBImageButton" 
                        Format="dd-MMM-yyyy" Enabled="True"/>
                    <asp:ImageButton ID="gDofBImageButton" runat="server" AlternateText="Click Here" 
                        ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="35" />
                       
                     </td>
                    <td align="right" >  <b>Age:</b></td>
                    <td align="left">
                        <asp:TextBox ID="gardianAgeTextBox" runat="server" CssClass="TextInputStyleLarge" 
                            MaxLength="20" meta:resourcekey="nomi1Address1TextBoxResource1" 
                            TabIndex="35"></asp:TextBox>
                                                </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Address:</b></td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="gardianAddressTextBox" runat="server" 
                            CssClass="TextInputStyleLarge" Height="44px" TabIndex="40" 
                            TextMode="MultiLine" Width="398px"></asp:TextBox>
                                                </td>
                   
                </tr>
            </table>
        
                 
                  
           
         </div>
        
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
        <asp:Button ID="regSaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="regSaveButton_Click" TabIndex="41" AccessKey="s" 
                meta:resourcekey="regSaveButtonResource1"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;<asp:Button ID="regDeleteButton" runat="server" 
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

