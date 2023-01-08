<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRegEntryVR1.aspx.cs" Inherits="UI_UnitRegEntry" Title="Registration Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
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
            document.getElementById("<%=isIDDropDownList.ClientID%>").value ="N";
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
            
            document.getElementById("<%=jHolderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderFMTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderMotherTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderOccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=jHolderNantionalityTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderAddress2TextBox.ClientID%>").value ="";
            
            document.getElementById("<%=NomiControlNoTextBox.ClientID%>").value =""; 
            document.getElementById("<%=nomi1NameTextBox.ClientID%>").value ="";   
            document.getElementById("<%=nomi1FMTextBox.ClientID%>").value =""; 
            document.getElementById("<%=nomi1MotherNameTextBox.ClientID%>").value ="";                 
            document.getElementById("<%=nomi1OccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=nomi1NationalityTextBox.ClientID%>").value ="";
            document.getElementById("<%=nomi1Address1TextBox.ClientID%>").value ="";
            document.getElementById("<%=nomi1Address2TextBox.ClientID%>").value ="";
            document.getElementById("<%=nomi1RelationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=nomi1PtcTextBox.ClientID%>").value ="";        
            
            document.getElementById("<%=nomi2NameTextBox.ClientID%>").value ="";    
            document.getElementById("<%=nomi2FMTextBox.ClientID%>").value ="";  
            document.getElementById("<%=nomi2MotherNameTextBox.ClientID%>").value ="";      
            document.getElementById("<%=nomi2OccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=nomi2NationalityTextBox.ClientID%>").value ="";
            document.getElementById("<%=nomi2Address1TextBox.ClientID%>").value ="";
            document.getElementById("<%=nomi2Address2TextBox.ClientID%>").value ="";
            document.getElementById("<%=nomi2RelationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=nomi2PtcTextBox.ClientID%>").value ="";  
            
            document.getElementById("<%=isBankDropDownList.ClientID%>").value ="N";            
            document.getElementById("<%=bankAccTextBox.ClientID%>").value ="";            
            document.getElementById("<%=bankNameDropDownList.ClientID%>").value ="0";     
            document.getElementById("<%=branchNameDropDownList.ClientID%>").value ="0";     
            document.getElementById("<%=bankAddressTextBox.ClientID%>").value ="";     
            document.getElementById("<%=isCIPDropDownList.ClientID%>").value ="N"; 
            document.getElementById("<%=bftnNoRadioButton.ClientID%>").checked=true;  
            document.getElementById("<%=bfnYesRadioButton.ClientID%>").checked=false;              
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
            if(document.getElementById("<%=isIDDropDownList.ClientID%>").value =="Y")
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
             if(document.getElementById("<%=isIDDropDownList.ClientID%>").value =="N")
            {
                 if(document.getElementById("<%=IDAccNoTextBox.ClientID%>").value !="")
                 {
                        document.getElementById("<%=IDAccNoTextBox.ClientID%>").focus();
                        alert("Please Select ID Account YES or Clear ID Account Number");
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
            
          if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="AMC/01")
           {            
               if(document.getElementById("<%=holderFMTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=holderFMTextBox.ClientID%>").focus();
                    alert("Please Enter Unit Holder's Father/Mother/Wife/Husband Name");
                    return false;
                    
                }
            }
           if(document.getElementById("<%=branchCodeTextBox.ClientID%>").value =="AMC/01")
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

            if(document.getElementById("<%=isBankDropDownList.ClientID%>").value =="Y")
            {
                 if(document.getElementById("<%=bankAccTextBox.ClientID%>").value =="")
                 {
                        document.getElementById("<%=bankAccTextBox.ClientID%>").focus();
                        alert("Please Enter Unit Holder's Bank Account Number");
                        return false;
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
            if(document.getElementById("<%=fundCodeTextBox.ClientID%>").value=='IAMPH')
            {
                 if(document.getElementById("<%=jHolderNameTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderMotherTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderFMTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderOccupationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=jHolderNantionalityTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderAddress1TextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderAddress2TextBox.ClientID%>").value !="")
                    {
                       
                            alert("In Pension Fund Joint Holder Information are not allowed");
                            return false;
                        
                    }
            }
            if(document.getElementById("<%=jHolderMotherTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderFMTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderOccupationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=jHolderNantionalityTextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderAddress1TextBox.ClientID%>").value !=""||document.getElementById("<%=jHolderAddress2TextBox.ClientID%>").value !="")
            {
                if(document.getElementById("<%=jHolderNameTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=jHolderNameTextBox.ClientID%>").focus();
                    alert("Please Enter Joint Holder Name");
                    return false;
                }
            }
           
            
            if(document.getElementById("<%=nomi1MotherNameTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi1FMTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi1OccupationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=nomi1NationalityTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi1Address1TextBox.ClientID%>").value !=""||document.getElementById("<%=nomi1Address2TextBox.ClientID%>").value !=""||document.getElementById("<%=nomi1RelationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=nomi1PtcTextBox.ClientID%>").value !="")
            {
                if(document.getElementById("<%=nomi1NameTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=nomi1NameTextBox.ClientID%>").focus();
                     alert("Please Enter Nominee1's Name ");
                      return false;
                }
                if(document.getElementById("<%=fundCodeTextBox.ClientID%>").value =="BDF")
                {
                    if(document.getElementById("<%=NomiControlNoTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%=NomiControlNoTextBox.ClientID%>").focus();
                         alert("Please Enter Nominee Control Number ");
                          return false;
                    }
                }
            }
            
            if(document.getElementById("<%=nomi2MotherNameTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2FMTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2OccupationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=nomi2NationalityTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2Address1TextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2Address2TextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2RelationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=nomi2PtcTextBox.ClientID%>").value !="")
            {
                if(document.getElementById("<%=nomi2NameTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=nomi2NameTextBox.ClientID%>").focus();
                     alert("Please Nominee2's Name ");
                      return false;
                }
                if(document.getElementById("<%=fundCodeTextBox.ClientID%>").value =="BDF")
                {
                    if(document.getElementById("<%=NomiControlNoTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%=NomiControlNoTextBox.ClientID%>").focus();
                         alert("Please Enter Nominee Control Number ");
                          return false;
                    }
                }
            }
            
             if(document.getElementById("<%=nomi1NameTextBox.ClientID%>").value !="")
             {
                
               if(document.getElementById("<%=nomi1PtcTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=nomi1PtcTextBox.ClientID%>").focus();               
                alert("Please Nominee1's Percentage ");
                return false;
               }
               if(document.getElementById("<%=nomi2NameTextBox.ClientID%>").value =="")
                   {
                    var nomi1=document.getElementById("<%=nomi1PtcTextBox.ClientID%>").value;
                    nomi1=parseFloat(nomi1.toString());
                    if(nomi1!=100)
                    {
                       document.getElementById("<%=nomi1PtcTextBox.ClientID%>").focus();
                       alert("Please Enter Valid Nominee1's Percentage ");
                       return false;
                    }
                   }
               
             }
             
             if(document.getElementById("<%=nomi2NameTextBox.ClientID%>").value !="")
             {
              if(document.getElementById("<%=nomi1NameTextBox.ClientID%>").value =="")
                 {
                       document.getElementById("<%=nomi1NameTextBox.ClientID%>").focus();
                       alert("Please Enter  Nominee1's Name First ");
                       return false;
                 }
              
               if(document.getElementById("<%=nomi2PtcTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=nomi2PtcTextBox.ClientID%>").focus();
                alert("Please Nominee2's Percentage ");
                return false;
               }
               
               if(document.getElementById("<%=nomi2PtcTextBox.ClientID%>").value !="")
               {
                var nomi1=document.getElementById("<%=nomi1PtcTextBox.ClientID%>").value;
                nomi1=parseFloat(nomi1.toString());
                var nomi2=document.getElementById("<%=nomi2PtcTextBox.ClientID%>").value;
                nomi2=parseFloat(nomi2.toString());             
                ptc=nomi1+nomi2;
                    if(ptc!=100)
                    {
                        document.getElementById("<%=nomi2PtcTextBox.ClientID%>").focus();
                        alert("Please Enter Valid Nominee2's Percentage ");
                        return false;
                    }
               }
             }
           
            
        }
        function fnClearID(value)
        {
            if(value=='N')
            {
                document.getElementById("<%=IDAccNoTextBox.ClientID%>").value="";
                document.getElementById("<%=IDbranchNameDropDownList.ClientID%>").value ="0";
                document.getElementById("<%=IDbankNameDropDownList.ClientID%>").value ="0";
            }
            else
            {
                
            }
        }
        function CheckIDStatus()
        {
           if(document.getElementById("<%=isIDDropDownList.ClientID%>").value=='N')
             {
                 event.returnValue=false;
                 alert("Plese Select Is ID Account Yes ");
                 document.getElementById("<%=isIDDropDownList.ClientID%>").focus();
                 
             }
             else
             {
                  if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		            {
		            document.getElementById("<%=IDAccNoTextBox.ClientID%>").focus();
		           alert("Please Enter Numaric Value Only");
			            event.returnValue=false;
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
        .style7
        {
            height: 22px;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />        
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Registration Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      <br />
 <table width="1000" align="center" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="120"></colgroup>
<colgroup width="320"></colgroup>
<colgroup width="140"></colgroup>
    <tr>
        <td colspan="2" align="left"><strong>Unit Holder Registration Information</strong></td>
        <td colspan="2" align="left"><strong>Joint Holder Information</strong></td>
    </tr>
    <%--<tr>
        <td colspan="4" align="left">&nbsp;</td>
    </tr>--%>
    <tr>
        <td align="left" >Registration No:</td>
        <td align="left">
            <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
            &nbsp;
            <asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" 
                meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
            &nbsp;
                                <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="5"
                CssClass= "TextInputStyleSmall" TabIndex="1" Width="89px"              
                ontextchanged="regNoTextBox_TextChanged" onkeypress= "fncInputNumericValuesOnly()" 
               AutoPostBack="True" 
                meta:resourcekey="regNoTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
        <td align="left">Joint Holder&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="jHolderNameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="21" 
                meta:resourcekey="jHolderNameTextBoxResource1"></asp:TextBox>
                                </td> 
       
    </tr>
    <tr>
        <td align="left" >Rgistration Date:</td>
        <td align="left">
            <asp:TextBox ID="regDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="2" meta:resourcekey="regDateTextBoxResource1"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                TargetControlID="RegDateTextBox" PopupButtonID="RegDateImageButton" 
                Format="dd-MMM-yyyy" Enabled="True"/>
            <asp:ImageButton ID="RegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="3" meta:resourcekey="RegDateImageButtonResource1" 
                />
            <span class="star">*</span></td>  
             
        <td align="left">Father/Hus&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="jHolderFMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="22" 
                meta:resourcekey="jHolderFMTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
     <tr>
        <td align="left" >Registration Type:</td>
        <td align="left">
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                TargetControlID="RegDateTextBox" PopupButtonID="RegDateImageButton" 
                Format="dd-MMM-yyyy" Enabled="True"/>
            <asp:DropDownList ID="regTypeDropDownList" runat="server" 
                CssClass="DropDownList" TabIndex="4" 
                meta:resourcekey="regTypeDropDownListResource1">
            
            </asp:DropDownList><span class="star">*</span></td>  
             
        <td align="left">Mother&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="jHolderMotherTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="128" TabIndex="22" 
                meta:resourcekey="jHolderFMTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
    <tr>
        <td align="left" >Is CIP:</td>
        <td align="left">
                  <asp:DropDownList ID="isCIPDropDownList" runat="server" 
             CssClass="DropDownList" TabIndex="4" EnableViewState="False" 
                      meta:resourcekey="isCIPDropDownListResource1">
            <asp:ListItem Value="0" Selected="True">---</asp:ListItem>
            <asp:ListItem Value="N" meta:resourcekey="ListItemResource6">No</asp:ListItem>
            <asp:ListItem Value="Y" meta:resourcekey="ListItemResource7">Yes</asp:ListItem>            
            </asp:DropDownList><span class="star">*</span></td>
          <td align="left" >Occupation:</td>
        <td align="left">
            <asp:DropDownList ID="jHolderOccupationDropDownList" runat="server" 
                TabIndex="23" meta:resourcekey="jHolderOccupationDropDownListResource1">
            </asp:DropDownList>
         </td>
                
    </tr>
    <tr>
        <td align="left" >Is ID Account:</td>
        <td align="left" rowspan="3"> 
        <div>
            <asp:UpdatePanel ID="IDBankNameUpdatePanel" runat="server">                                                               
                 <ContentTemplate>
                 <asp:DropDownList ID="isIDDropDownList" runat="server" onchange=" fnClearID(this.value)"
                    CssClass="DropDownList" AutoPostBack="true" TabIndex="5"                                       
                    onselectedindexchanged="isIDDropDownList_SelectedIndexChanged">
                <asp:ListItem Value="N" Selected="True" meta:resourcekey="ListItemResource4">No</asp:ListItem>
                <asp:ListItem Value="Y" meta:resourcekey="ListItemResource5">Yes</asp:ListItem>            
                </asp:DropDownList><span class="star">*</span>&nbsp;
                    A/C No:<asp:TextBox ID="IDAccNoTextBox" runat="server" 
                    CssClass="textInputStyle" onkeypress= "CheckIDStatus()" MaxLength="6" 
                    meta:resourcekey="IDAccNoTextBoxResource1" TabIndex="5" ></asp:TextBox>  
                    <asp:DropDownList ID="IDbankNameDropDownList" runat="server"  
                         AutoPostBack="true" 
                         onselectedindexchanged="IDbankNameDropDownList_SelectedIndexChanged"  
                         TabIndex="5" ></asp:DropDownList>  
                     <asp:DropDownList ID="IDbranchNameDropDownList" runat="server" TabIndex="5"></asp:DropDownList>               
                 </ContentTemplate>
                  <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="isIDDropDownList"  EventName="SelectedIndexChanged"/>
                    <asp:AsyncPostBackTrigger ControlID="IDbankNameDropDownList"  EventName="SelectedIndexChanged"/>
                  </Triggers>
                  
              </asp:UpdatePanel>
            
         </div>                    
        </td>
         <td align="left" >Nationality:</td>
        <td align="left">
            <asp:TextBox ID="jHolderNantionalityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="24" 
                meta:resourcekey="jHolderNantionalityTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
     <tr>
        <td align="left" class="style7">ID Institution:</td>
         <td align="left" class="style7" >Address1:</td>
        <td align="left" class="style7">
            <asp:TextBox ID="jHolderAddress1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="25" 
                meta:resourcekey="jHolderAddress1TextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
      <tr>
        <td align="left">ID Institution Branch</td>
         <td align="left" >Address2:</td>
        <td align="left">
            <asp:TextBox ID="jHolderAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="26" 
                meta:resourcekey="jHolderAddress2TextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
    <tr>
        <td align="left"   colspan="2"><b>Principal Holder Information</b></td>        
        <td align="left" colspan="2"> <strong>Nominee Information</strong></td>
        
    </tr>
   <%-- <tr>
        <td colspan="2" align="left">&nbsp;</td>
        <td colspan="2" align="left">&nbsp;</td>
    </tr>--%>
    <tr>
        <td align="left" >Name of Holder:</td>
        <td align="left">
            <asp:TextBox ID="holderNameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="6" 
                meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
        
        <td align="left" >
            Nominee Control No:</td>
       <td align="left">
                                <asp:TextBox ID="NomiControlNoTextBox" runat="server"  MaxLength="10"
                CssClass= "TextInputStyleSmall" TabIndex="27" Width="89px"              
                 onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                </td>
    </tr>
    <tr>
        <td align="left" >Father/Hus&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="holderFMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
        <td align="left"> Nominee1&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="nomi1NameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="27" 
                meta:resourcekey="nomi1NameTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
     <tr>
         <td align="left" >Mother&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="holderMotherTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7" 
                meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
            <span class="star">*</span></td>
          <td align="left" >Father/Hus&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="nomi1FMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="28" 
                meta:resourcekey="nomi1FMTextBoxResource1"></asp:TextBox>
         </td>
    </tr>
     <tr>
         <td align="left" >Occupation:</td>
        <td align="left">
            <asp:DropDownList ID="holderOccupationDropDownList" runat="server" TabIndex="8" 
                meta:resourcekey="holderOccupationDropDownListResource1">
            </asp:DropDownList>
            <span class="star">*</span></td>
          <td align="left" >Mother&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="nomi1MotherNameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="28" 
                meta:resourcekey="nomi1FMTextBoxResource1"></asp:TextBox>
         </td>
    </tr>
     <tr>
       <td align="left" >Nationality:</td>
        <td align="left">
            <asp:TextBox ID="holderNationalityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="9" meta:resourcekey="holderNationalityTextBoxResource1" 
               >BANGLADESHI</asp:TextBox>
                                <span class="star">*</span></td>
        <td align="left" >Occupation:</td>
        <td align="left">
            <asp:DropDownList ID="nomi1OccupationDropDownList" runat="server" TabIndex="29" 
                meta:resourcekey="nomi1OccupationDropDownListResource1">
            </asp:DropDownList>
                                </td>
     
    </tr>
    <tr>
       <td align="left" >Address1:</td>
        <td align="left">
            <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="10" 
                meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
       <td align="left" >Nationality:</td>
        <td align="left">
            <asp:TextBox ID="nomi1NationalityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="30" 
                meta:resourcekey="nomi1NationalityTextBoxResource1"></asp:TextBox></td>
     
    </tr>
    <tr>
        <td align="left">Address2:</td>
        <td align="left">
            <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="11" 
                meta:resourcekey="holderAddress2TextBoxResource1"></asp:TextBox>
                                <span class="star">*</span></td>
         <td align="left">Address1:</td>
        <td align="left">
            <asp:TextBox ID="nomi1Address1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="31" 
                meta:resourcekey="nomi1Address1TextBoxResource1"></asp:TextBox>
     
                                </td>
    </tr>   
    <tr>
              <td align="left">City:</td>
              <td align="left">
             <asp:TextBox ID="holderCityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="35" TabIndex="12" 
                      meta:resourcekey="holderCityTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="left">Address2:</td>
        <td align="left">
            <asp:TextBox ID="nomi1Address2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="32" 
                meta:resourcekey="nomi1Address2TextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
    <tr>
              <td align="left">Telephone/Mobile:</td>
              <td align="left">
             <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13" 
                      meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="left">Relation:</td>
        <td align="left">
            <asp:DropDownList ID="nomi1RelationDropDownList" runat="server" 
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
                                &nbsp;&nbsp;&nbsp;Percentage:<asp:TextBox 
                ID="nomi1PtcTextBox" runat="server" CssClass="textInputStyle" 
                TabIndex="33" MaxLength="6" Width="120px" 
                meta:resourcekey="nomi1PtcTextBoxResource1"></asp:TextBox></td>
    </tr>
    <tr>
              <td align="left">E-Mail:</td>
              <td align="left">
             <asp:TextBox ID="holderEmailTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13" 
                      meta:resourcekey="holderEmailTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="left">Nominee2&#39;s Name</td>
        <td align="left">
            <asp:TextBox ID="nomi2NameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="34" 
                meta:resourcekey="nomi2NameTextBoxResource1"></asp:TextBox>
              </td>
    </tr>
    <tr>
              <td align="left">Sex:</td>
              <td align="left">
                  <asp:DropDownList ID="holderSexDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="14" 
                      meta:resourcekey="holderSexDropDownListResource1"> 
            <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource18">-----</asp:ListItem>
            <asp:ListItem Value="M" meta:resourcekey="ListItemResource19" >Male</asp:ListItem>
            <asp:ListItem Value="F" meta:resourcekey="ListItemResource20">Female</asp:ListItem>            
            </asp:DropDownList>
                                </td>
        <td align="left">Father/Hus&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="nomi2FMTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="35" 
                meta:resourcekey="nomi2FMTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
     <tr>
              <td align="left">National ID:</td>
              <td align="left">
            <asp:TextBox ID="holderNationalIDTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="15" 
                meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="left">Mother&#39;s Name:</td>
        <td align="left">
            <asp:TextBox ID="nomi2MotherNameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="35" 
                meta:resourcekey="nomi2FMTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
       <tr>
        <td align="left" >Date of Birth</td>
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
            </td>  
             
        <td align="left">Occupation:</td>
        <td align="left">
            <asp:DropDownList ID="nomi2OccupationDropDownList" runat="server" TabIndex="36" 
                meta:resourcekey="nomi2OccupationDropDownListResource1">
            </asp:DropDownList>
                                </td>
    </tr>
      <tr>
              <td align="left">Maritial Status:</td>
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
        <td align="left">Nationality:</td>
        <td align="left">
            <asp:TextBox ID="nomi2NationalityTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="37" 
                meta:resourcekey="nomi2NationalityTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
      <tr>
              <td align="left">Religion:</td>
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
        <td align="left">Address1:</td>
        <td align="left">
            <asp:TextBox ID="nomi2Address1TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="38" 
                meta:resourcekey="nomi2Address1TextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
      <tr>
              <td align="left">Education Qua:</td>
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
        <td align="left">Address2:</td>
        <td align="left">
            <asp:TextBox ID="nomi2Address2TextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="39" 
                meta:resourcekey="nomi2Address2TextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
    <tr>
              <td align="left">Remarks:</td>
              <td align="left">
             <asp:TextBox ID="holderRemarksTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="20" 
                      meta:resourcekey="holderRemarksTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="left">Relation:</td>
        <td align="left">
            <asp:DropDownList ID="nomi2RelationDropDownList" runat="server" 
                CssClass="DropDownList" TabIndex="40" 
                meta:resourcekey="nomi2RelationDropDownListResource1">
             <asp:ListItem Value="0" Selected="True" meta:resourcekey="ListItemResource37">-----</asp:ListItem>
            <asp:ListItem Value="FATHER" meta:resourcekey="ListItemResource38">Father</asp:ListItem>
            <asp:ListItem Value="MOTHER" meta:resourcekey="ListItemResource39">Mother</asp:ListItem>
            <asp:ListItem Value="WIFE" meta:resourcekey="ListItemResource40">Wife</asp:ListItem>
             <asp:ListItem Value="HUSBAND" meta:resourcekey="ListItemResource41">Husband</asp:ListItem>
             <asp:ListItem Value="SON" meta:resourcekey="ListItemResource42">Son</asp:ListItem>
              <asp:ListItem Value="DAUGHTER" meta:resourcekey="ListItemResource43">Daughter</asp:ListItem>
              <asp:ListItem Value="COUSINE" meta:resourcekey="ListItemResource44">Cousine</asp:ListItem>
             <asp:ListItem Value="FRIEND" meta:resourcekey="ListItemResource45">Friend</asp:ListItem>
              
            <asp:ListItem Value="SISTER">Sister</asp:ListItem>
            <asp:ListItem Value="BROTHER">Brother</asp:ListItem>
                  <asp:ListItem Value="OTHERS" meta:resourcekey="ListItemResource46">OTHERS</asp:ListItem>
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;Percentage:<asp:TextBox ID="nomi2PtcTextBox" 
                runat="server" CssClass="textInputStyle"  MaxLength="6" 
                TabIndex="40" Width="120px" meta:resourcekey="nomi2PtcTextBoxResource1"></asp:TextBox>
                                </td>
    </tr>
    </table>
    <div id="dvBankNameUpdate" runat="server" width="1000" align="center">
    <asp:UpdatePanel ID="bankUpdatePanel" runat="server">
    <ContentTemplate>
  <table width="1000" align="center" cellpadding="0" cellspacing="0" border="0" >
  <colgroup width="120"></colgroup>
<colgroup width="320"></colgroup>
<colgroup width="140"></colgroup>
   <%--  <tr>
        <td colspan="4">&nbsp;</td>
    </tr>--%>
    <tr>
      <td colspan="4" align="center" ><b>Special Instructions</b></td>
    </tr>
     <tr>
              <td align="left">Is Bank Information:</td>
              <td align="left">
                  <asp:DropDownList ID="isBankDropDownList" runat="server" 
                      CssClass="DropDownList" TabIndex="41" 
                      onselectedindexchanged="isBankDropDownList_SelectedIndexChanged" 
                      AutoPostBack="True" meta:resourcekey="isBankDropDownListResource1">
            <asp:ListItem Value="N" Selected="True" meta:resourcekey="ListItemResource47">No</asp:ListItem>
            <asp:ListItem Value="Y" meta:resourcekey="ListItemResource48">Yes</asp:ListItem>            
            </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A/C No:&nbsp;
            <asp:TextBox ID="bankAccTextBox" runat="server" CssClass= "textInputStyle" 
                      TabIndex="42" Enabled="False" meta:resourcekey="bankAccTextBoxResource1"></asp:TextBox>
                                </td>
        <td align="left">Bank Name:</td>
        <td align="left">
            <asp:DropDownList ID="bankNameDropDownList" runat="server"                 
                AutoPostBack="True" 
                onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" 
                Enabled="False" TabIndex="43" 
                meta:resourcekey="bankNameDropDownListResource1"></asp:DropDownList>
                                </td>
    </tr>
    <tr>
                          <td align="left">Branch Name:</td>
                          <td align="left">
                        <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                                TabIndex="42" Enabled="False" 
                                  onselectedindexchanged="branchNameDropDownList_SelectedIndexChanged" 
                                  AutoPostBack="True"></asp:DropDownList>
                                            </td>
                    <td align="left" rowspan="2">Address:</td>
                    <td align="left" rowspan="2">
                        <asp:TextBox ID="bankAddressTextBox" runat="server" 
                            CssClass="TextInputStyleLarge" Enabled="False" Height="44px" 
                             TabIndex="45" 
                            TextMode="MultiLine" Width="291px"></asp:TextBox>
                          </td>
                </tr>
                  <tr>
                          <td align="left">Is BEFTN</td>
                          <td align="left">
                              <asp:RadioButton ID="bfnYesRadioButton" runat="server" Font-Bold="True" 
                                  GroupName="bftn" Text="Yes" />
                              <asp:RadioButton ID="bftnNoRadioButton" runat="server" Checked="True" 
                                  Font-Bold="True" GroupName="bftn" Text="No" />
                          </td>
                </tr>
  
    </table>
    </ContentTemplate>
       <Triggers>
       
        <asp:AsyncPostBackTrigger ControlID="isBankDropDownList" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="bankNameDropDownList" EventName="SelectedIndexChanged" />
        <asp:AsyncPostBackTrigger ControlID="branchNameDropDownList" EventName="SelectedIndexChanged" />
       </Triggers>
    </asp:UpdatePanel>
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
</asp:Content>

