<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitRegEditVR1.aspx.cs" Inherits="UI_UnitReg" Title="Registration Edit Form(Design and Developed by Sakhawat)" %>
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
            document.getElementById("<%=isCIPDropDownList.ClientID%>").value =="0"
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderFMTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderMotherTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderOccupationDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=holderNationalityTextBox.ClientID%>").value ="";
            
             document.getElementById("<%=holderTINTextBox.ClientID%>").value ="";
            
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
            document.getElementById("<%=bftnYesRadioButton.ClientID%>").checked=false;         
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
                    alert("Please Enter Valid Registration Number");
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
                alert("Please Enter Nationality");
                return false;
                
            }
             if(document.getElementById("<%=holderAddress1TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderAddress1TextBox.ClientID%>").focus();
                alert("Please Enter Address Line1");
                return false;
                
            }
             if(document.getElementById("<%=holderAddress2TextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=holderAddress2TextBox.ClientID%>").focus();
                alert("Please Enter Address Line2");
                return false;
                
            }
            
            if(document.getElementById("<%=isBankDropDownList.ClientID%>").value =="Y")
            {
                 if(document.getElementById("<%=bankAccTextBox.ClientID%>").value =="")
                 {
                        document.getElementById("<%=bankAccTextBox.ClientID%>").focus();
                        alert("Please Enter Bank Account Number");
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
                 if(document.getElementById("<%=bankAddressTextBox.ClientID%>").value =="")
                 {
                        document.getElementById("<%=bankAddressTextBox.ClientID%>").focus();
                        alert("Please Enter Address");
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
            }
            
            if(document.getElementById("<%=nomi2MotherNameTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2FMTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2OccupationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=nomi2NationalityTextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2Address1TextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2Address2TextBox.ClientID%>").value !=""||document.getElementById("<%=nomi2RelationDropDownList.ClientID%>").value !="0"||document.getElementById("<%=nomi2PtcTextBox.ClientID%>").value !="")
            {
                if(document.getElementById("<%=nomi2NameTextBox.ClientID%>").value =="")
                {
                    document.getElementById("<%=nomi2NameTextBox.ClientID%>").focus();
                     alert("Please Enter Nominee2's Name ");
                      return false;
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
             
             
            
//           
            
        }
        
        function PopupImage()
        {
            if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                 var fundCode=document.getElementById("<%=fundCodeTextBox.ClientID%>").value;
                 var reg=document.getElementById("<%=regNoTextBox.ClientID%>").value;
                 var Branch=document.getElementById("<%=branchCodeTextBox.ClientID%>").value;
                 var url='Popup/ViewImage.aspx?reg='+reg+'&fund='+fundCode+'&branch='+Branch;
                 var ViewImage= window.open(url,'ViewImage','left=200,top=200,width=1000,height=600');
                 ViewImage.focus();
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
		            {alert("Please Enter Numaric Value Only");
			            event.returnValue=false;
			           
		            }
             }
          
        } 
        
       function fnClearID(value)
        {
            if(value=='N')
            {
                document.getElementById("<%=IDAccNoTextBox.ClientID%>").value="";
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



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Registration Edit Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <div id="dvUpdatePanel" runat="server" width="1000" align="center">
      <%--  <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">--%>
        <%-- <ContentTemplate>--%>
            <table width="1000" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="120"></colgroup>
            <colgroup width="320"></colgroup>
            <colgroup width="140"></colgroup>
                <tr>
                    <td colspan="2" align="left"><strong>Unit Holder Registration Information</strong></td>
                    <td colspan="2" align="left"><strong>Joint Holder Information</strong></td>
                </tr>    
                <tr>
                    <td align="left" >Registration No:</td>
                    <td align="left">
                        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                            CssClass= "TextInputStyleSmall" Enabled="False"></asp:TextBox>
                        &nbsp;
                        <asp:TextBox ID="branchCodeTextBox" runat="server" 
                            CssClass= "TextInputStyleSmall" Enabled="False"></asp:TextBox>
                        &nbsp;
                                            <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="5"
                            CssClass= "TextInputStyleSmall" TabIndex="1" Width="90px" 
                             AutoPostBack="True" 
                            ontextchanged="regNoTextBox_TextChanged" 
                            onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                            <span class="star">*</span>&nbsp;<asp:Button 
                            ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            OnClientClick="return fnCheckRegNo();" onclick="findButton_Click" 
                            AccessKey="f" TabIndex="1"/></td>
                    <td align="left">Joint Holder&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="jHolderNameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="21"></asp:TextBox>
                                            </td> 
                   
                </tr>
                <tr>
                    <td align="left" >Rgistration Date:</td>
                    <td align="left">
                        <asp:TextBox ID="regDateTextBox" runat="server" CssClass="textInputStyleDate" 
                            TabIndex="2"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" TargetControlID="RegDateTextBox" PopupButtonID="RegDateImageButton" Format="dd-MMM-yyyy"/>
                        <asp:ImageButton ID="RegDateImageButton" runat="server" 
                            AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                            TabIndex="3" />
                        <span class="star">*</span></td>  
                         
                    <td align="left">Fa/Hus&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="jHolderFMTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="22"></asp:TextBox>
                                            </td>
                </tr>
                <tr>
                    <td align="left" >Registration Type:</td>
                    <td align="left">
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="RegDateTextBox" PopupButtonID="RegDateImageButton" Format="dd-MMM-yyyy"/>
                        <asp:DropDownList ID="regTypeDropDownList" runat="server" 
                            CssClass="DropDownList" TabIndex="4">
                        
                        </asp:DropDownList><span class="star">*</span></td>  
                         
                    <td align="left">Mother&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="jHolderMotherTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="22"></asp:TextBox>
                                            </td>
                </tr>
                <tr>
                    <td align="left" ><b>Is CIP:</b></td>
                    <td align="left">
                              <asp:DropDownList ID="isCIPDropDownList" runat="server" 
                         CssClass="DropDownList" TabIndex="6"> 
                        <asp:ListItem Value="0" Selected="True">---</asp:ListItem>           
                        <asp:ListItem Value="N">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>            
                        </asp:DropDownList></td>
                      <td align="left" >Occupation:</td>
                    <td align="left">
                        <asp:DropDownList ID="jHolderOccupationDropDownList" runat="server" 
                            TabIndex="23">
                        </asp:DropDownList>
                     </td>
                            
                </tr>
                <tr>
                    <td align="left" >Is ID Account</td>
                    <td align="left"> 
                        <asp:DropDownList ID="isIDDropDownList" runat="server" onchange=" fnClearID(this.value)"
                            CssClass="DropDownList" TabIndex="5" AutoPostBack="True" 
                            onselectedindexchanged="isIDDropDownList_SelectedIndexChanged"  >
                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>            
                        </asp:DropDownList><span class="star">*</span>&nbsp;&nbsp;ID A/C No:<asp:TextBox 
                            ID="IDAccNoTextBox" runat="server" CssClass="textInputStyle" onkeypress= "CheckIDStatus()"
                            TabIndex="5" MaxLength="6"></asp:TextBox>
                                            </td>
                     <td align="left" >Nationality:</td>
                    <td align="left">
                        <asp:TextBox ID="jHolderNantionalityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="24"></asp:TextBox>
                                            </td>
                </tr>
                 <tr>
                 <td align="left">ID Institution:</td>
                 <td align="left">
                        <asp:DropDownList ID="IDbankNameDropDownList" runat="server"                 
                            AutoPostBack="True" TabIndex="5" 
                            onselectedindexchanged="IDbankNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
                                            </td>
                  <td align="left" >Address1:</td>
                  <td align="left">
                        <asp:TextBox ID="jHolderAddress1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="25"></asp:TextBox>
                   </td>
                </tr>
                <tr>
                    <td align="left" >ID Institution Branch</td>    
                    <td align="left">
                         <asp:DropDownList ID="IDbranchNameDropDownList" runat="server" 
                                TabIndex="5" 
                                  meta:resourcekey="branchNameDropDownListResource1" ></asp:DropDownList>
                                            </td>    
                    <td align="left" >Address2:</td>
                    <td align="left">
                        <asp:TextBox ID="jHolderAddress2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="26"></asp:TextBox>
                                            </td>
                </tr>
                <tr>
                    <td align="left"   colspan="2"><b>Principal Holder Information</b></td>        
                    <td align="left" colspan="2" >
                        <strong>Nominee Information</strong></td>
                   
                </tr>
               <%-- <tr>
                    <td colspan="2" align="left">&nbsp;</td>
                    <td colspan="2" align="left">&nbsp;</td>
                </tr>--%>
                <tr>
                    <td align="left" >Name of Holder:</td>
                    <td align="left">
                        <asp:TextBox ID="holderNameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="6"></asp:TextBox>
                                            <span class="star">*</span></td>
                    
                    <td align="left" >
                        Nominee Control No:</td>
                        <td align="left" >
                                            <asp:TextBox ID="NomiControlNoTextBox" runat="server"  MaxLength="10"
                            CssClass= "TextInputStyleSmall" TabIndex="27" Width="89px"              
                             onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                            </td>
                   
                </tr>
                <tr>
                    <td align="left" >Fa/Hus&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="holderFMTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7"></asp:TextBox>
                                            <span class="star">*</span></td>
                    <td align="left"> Nominee1&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi1NameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="27"></asp:TextBox>
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
                        <asp:DropDownList ID="holderOccupationDropDownList" runat="server" TabIndex="8">
                        </asp:DropDownList>
                        <span class="star">*</span></td>
                      <td align="left" >Mother&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi1MotherNameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="28"></asp:TextBox>
                     </td>
                </tr>
                 <tr>
                   <td align="left" >Nationality:</td>
                    <td align="left">
                        <asp:TextBox ID="holderNationalityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="9"></asp:TextBox>
                                            <span class="star">*</span></td>
                    <td align="left" >Occupation:</td>
                    <td align="left">
                        <asp:DropDownList ID="nomi1OccupationDropDownList" runat="server" TabIndex="29">
                        </asp:DropDownList>
                                            </td>
                 
                </tr>
                <tr>
                   <td align="left" >Address1:</td>
                    <td align="left">
                        <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="10"></asp:TextBox>
                                            <span class="star">*</span></td>
                   <td align="left" >Nationality:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi1NationalityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="30"></asp:TextBox>
                   </td>
                 
                </tr>
                <tr>
                    <td align="left">Address2:</td>
                    <td align="left">
                        <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="11"></asp:TextBox>
                                            <span class="star">*</span></td>
                     <td align="left">Address1:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi1Address1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="31"></asp:TextBox>
                 
                                            </td>
                </tr>   
                <tr>
                          <td align="left">City:</td>
                          <td align="left">
                         <asp:TextBox ID="holderCityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="35" TabIndex="12"></asp:TextBox>
                                            </td>
                    <td align="left">Address2:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi1Address2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="32"></asp:TextBox>
                                            </td>
                </tr>
                <tr>
                          <td align="left">Telephone/Mobile:</td>
                          <td align="left">
                         <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13"></asp:TextBox>
                                            </td>
                    <td align="left">Relation:</td>
                    <td align="left">
                        <asp:DropDownList ID="nomi1RelationDropDownList" runat="server" 
                            CssClass="DropDownList" TabIndex="33">
                            <asp:ListItem Value="0" Selected="True">----</asp:ListItem>
                        <asp:ListItem Value="FATHER">Father</asp:ListItem>
                        <asp:ListItem Value="MOTHER">Mother</asp:ListItem>
                        <asp:ListItem Value="WIFE">Wife</asp:ListItem>
                        <asp:ListItem Value="SON">Son</asp:ListItem>
                        <asp:ListItem Value="DAUGHTER">Daughter</asp:ListItem>
                        <asp:ListItem Value="COUSINE">Cousine</asp:ListItem>
                        <asp:ListItem Value="FRIEND">Friend</asp:ListItem>
                        <asp:ListItem Value="HUSBAND">Husband</asp:ListItem>
                        <asp:ListItem Value="SISTER">Sister</asp:ListItem>
                        <asp:ListItem Value="BROTHER">Brother</asp:ListItem>
                        <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                        </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;Percentage:<asp:TextBox ID="nomi1PtcTextBox" 
                            runat="server" CssClass="textInputStyle" MaxLength="6" TabIndex="33" 
                            Width="120px"></asp:TextBox>
                                            </td>
                </tr>
                <tr>
                          <td align="left">E-Mail:</td>
                          <td align="left">
                         <asp:TextBox ID="holderEmailTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="30" TabIndex="13"></asp:TextBox>
                                            </td>
                    <td align="left">Nominee2&#39;s Name</td>
                    <td align="left">
                        <asp:TextBox ID="nomi2NameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="34"></asp:TextBox>
                          </td>
                </tr>
                <tr>
                          <td align="left">Sex:</td>
                          <td align="left">
                              <asp:DropDownList ID="holderSexDropDownList" runat="server" 
                                  CssClass="DropDownList" TabIndex="14"> 
                        <asp:ListItem Value="0" Selected="True">-----</asp:ListItem>
                        <asp:ListItem Value="M" >Male</asp:ListItem>
                        <asp:ListItem Value="F">Female</asp:ListItem>            
                        </asp:DropDownList>
                                            </td>
                    <td align="left">Fa/Mo/Hus&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi2FMTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="35"></asp:TextBox>
                                            </td>
                </tr>
                  <tr>
                          <td align="left">TIN:</td>
                          <td align="left">
                        <asp:TextBox ID="holderTINTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="12" TabIndex="15" 
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
                            CssClass="textInputStyleDate" TabIndex="15"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="hDofBCalendarExtender" runat="server" TargetControlID="holderDateofBirthTextBox" PopupButtonID="hDofBImageButton" Format="dd-MMM-yyyy"/>
                        <asp:ImageButton ID="hDofBImageButton" runat="server" AlternateText="Click Here" 
                            ImageUrl="~/Image/Calendar_scheduleHS.png" TabIndex="16" />
                        </td>  
                         
                    <td align="left">Occupation:</td>
                    <td align="left">
                        <asp:DropDownList ID="nomi2OccupationDropDownList" runat="server" TabIndex="36">
                        </asp:DropDownList>
                                            </td>
                </tr>
                  <tr>
                          <td align="left">Maritial Status:</td>
                          <td align="left">
                              <asp:DropDownList ID="holderMaritialStatusDropDownList" runat="server" 
                                  CssClass="DropDownList" TabIndex="17">
                         <asp:ListItem Value="0" Selected="True">-----</asp:ListItem>
                        <asp:ListItem Value="M">Maried</asp:ListItem>
                        <asp:ListItem Value="U">Unmarried</asp:ListItem>            
                                  <asp:ListItem Value="O">Others</asp:ListItem>
                        </asp:DropDownList>
                                            </td>
                    <td align="left">Nationality:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi2NationalityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="37"></asp:TextBox>
                                            </td>
                </tr>
                  <tr>
                          <td align="left">Religion:</td>
                          <td align="left">
                              <asp:DropDownList ID="holderReligionDropDownList" runat="server" 
                                  CssClass="DropDownList" TabIndex="18">
                         <asp:ListItem Value="0" Selected="True" >-----</asp:ListItem>
                        <asp:ListItem Value="M" >Muslim</asp:ListItem>
                        <asp:ListItem Value="H">Hindu</asp:ListItem>            
                                  <asp:ListItem Value="C">Christian</asp:ListItem>
                                  <asp:ListItem Value="B">Buddah</asp:ListItem>
                                  <asp:ListItem Value="O">Others</asp:ListItem>
                        </asp:DropDownList>
                                            </td>
                    <td align="left">Address1:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi2Address1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="38"></asp:TextBox>
                                            </td>
                </tr>
                  <tr>
                          <td align="left">Education Qua:</td>
                          <td align="left">
                              <asp:DropDownList ID="holderEducationDropDownList" runat="server" 
                                  CssClass="DropDownList" TabIndex="19">
                        <asp:ListItem Value="0" Selected="True">-----</asp:ListItem>
                        <asp:ListItem Value="Primary">Primary</asp:ListItem>
                        <asp:ListItem Value="S. S. C.">S. S. C.</asp:ListItem>            
                                  <asp:ListItem>H. S. C.</asp:ListItem>
                                  <asp:ListItem Value="GRADUATE">Graduate</asp:ListItem>
                                  <asp:ListItem Value="POST GRADUATE">Post Graduate</asp:ListItem>
                        </asp:DropDownList>
                                            </td>
                    <td align="left">Address2:</td>
                    <td align="left">
                        <asp:TextBox ID="nomi2Address2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="39"></asp:TextBox>
                                            </td>
                </tr>
                <tr>
                          <td align="left">Remarks:</td>
                          <td align="left">
                         <asp:TextBox ID="holderRemarksTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="255" TabIndex="20"></asp:TextBox>
                                            </td>
                    <td align="left">Relation:</td>
                    <td align="left">
                        <asp:DropDownList ID="nomi2RelationDropDownList" runat="server" 
                            CssClass="DropDownList" TabIndex="40">
                         <asp:ListItem Value="0" Selected="True">-----</asp:ListItem>
                        <asp:ListItem Value="FATHER">Father</asp:ListItem>
                        <asp:ListItem Value="MOTHER">Mother</asp:ListItem>
                        <asp:ListItem Value="WIFE">Wife</asp:ListItem>
                        <asp:ListItem Value="SON">Son</asp:ListItem>
                        <asp:ListItem Value="DAUGHTER">Daughter</asp:ListItem>
                         <asp:ListItem Value="COUSINE">Cousine</asp:ListItem>
                         <asp:ListItem Value="FRIEND">Friend</asp:ListItem>
                         <asp:ListItem Value="HUSBAND">Husband</asp:ListItem>
                        <asp:ListItem Value="SISTER">Sister</asp:ListItem>
                        <asp:ListItem Value="BROTHER">Brother</asp:ListItem>
                              <asp:ListItem Value="OTHERS">OTHERS</asp:ListItem>
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;Percentage:<asp:TextBox ID="nomi2PtcTextBox" 
                            runat="server" CssClass="textInputStyle" MaxLength="6" TabIndex="40" 
                            Width="120px"></asp:TextBox>
                                            </td>
                </tr>
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
                                  CssClass="DropDownList" TabIndex="39" 
                                  onselectedindexchanged="isBankDropDownList_SelectedIndexChanged" 
                                  AutoPostBack="True">
                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>            
                        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; A/C No:&nbsp;
                        <asp:TextBox ID="bankAccTextBox" runat="server" CssClass= "textInputStyle" 
                                  TabIndex="40" Enabled="False"></asp:TextBox>
                                            </td>
                    <td align="left">Bank Name:</td>
                    <td align="left">
                        <asp:DropDownList ID="bankNameDropDownList" runat="server"                 
                            AutoPostBack="True" 
                            onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" 
                            Enabled="False" TabIndex="41"></asp:DropDownList>
                                            </td>
                </tr>
                <tr>
                          <td align="left">Branch Name:</td>
                          <td align="left">
                        <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                                TabIndex="42" Enabled="False" AutoPostBack="True" 
                                  onselectedindexchanged="branchNameDropDownList_SelectedIndexChanged"></asp:DropDownList>
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
                              <asp:RadioButton ID="bftnYesRadioButton" runat="server" Font-Bold="True" 
                                  GroupName="bftn" Text="Yes" />
                              <asp:RadioButton ID="bftnNoRadioButton" runat="server" Checked="True" 
                                  Font-Bold="True" GroupName="bftn" Text="No" />
                          </td>
                </tr>
                
                <tr>
                 <td align="left" colspan="4">&nbsp;</td>               
                </tr>
               </table>
          <%--  </ContentTemplate>--%>
          <%--  <Triggers>
                <asp:AsyncPostBackTrigger ControlID="regNoTextBox" EventName="TextChanged" />
                <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="isIDDropDownList" EventName="SelectedIndexChanged" />    
                <asp:AsyncPostBackTrigger ControlID="IDbankNameDropDownList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="isBankDropDownList" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="bankNameDropDownList" EventName="SelectedIndexChanged" />                            
            </Triggers>--%>
      <%--  </asp:UpdatePanel>--%>
     </div>
    <br />
    <table width="500" align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="right">
        <asp:Button ID="regSaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="regSaveButton_Click" AccessKey="s"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="viewImageButton" runat="server" Text="View Image" 
                CssClass="buttoncommon" OnClientClick=" return PopupImage();" AccessKey="v"/>&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" 
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

