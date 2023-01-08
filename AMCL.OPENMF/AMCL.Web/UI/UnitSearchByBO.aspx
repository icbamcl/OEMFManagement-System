<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSearchByBO.aspx.cs" Inherits="UI_UnitSearchByBO" Title="Search By BO (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
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
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderCityTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderEmailTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderSexDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=holderDateofBirthTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderMaritialStatusDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=holderReligionDropDownList.ClientID%>").value ="0";   
            
            document.getElementById("<%=AllotTextBox.ClientID%>").value ="";
            document.getElementById("<%=BOTextBox.ClientID%>").value ="";
            document.getElementById("<%=folioTextBox.ClientID%>").value ="";
            document.getElementById("<%=BalaceNoTextBox.ClientID%>").value ="";
            document.getElementById("<%=BOTextBox.ClientID%>").focus();  
            document.getElementById("<%=tdImage.ClientID%>").innerHTML=""; 
                
                 
            return false;
       
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



    <style type="text/css">
        .style7
        {
            font-size: large;
            font-weight: bold;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           &nbsp;Registration Search By BO/Folio/Allot&nbsp; Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      
          
            <table align="center" cellpadding="0" cellspacing="0" border="0" 
        style="width: 987px" >
            <colgroup width="120"></colgroup>
            <colgroup width="320"></colgroup>
            <colgroup width="140"></colgroup>
                <tr>
                    <td colspan="2" align="left"><strong>Unit Holder Registration Information</strong></td>
                    <td colspan="2" align="left">&nbsp;</td>
                </tr>    
                <tr>
                    <td align="left" >Registration No:</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                            CssClass= "TextInputStyleSmall" Enabled="False"></asp:TextBox>
                        &nbsp;
                        <asp:TextBox ID="branchCodeTextBox" runat="server" 
                            CssClass= "TextInputStyleSmall" Enabled="False"></asp:TextBox>
                        &nbsp;
                                            <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="5"
                            CssClass= "TextInputStyleSmall" TabIndex="1" Width="60px" 
                           
                            onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
                                            <span class="star">*</span><b>/BO
                        <asp:TextBox ID="BOTextBox" runat="server" CssClass="TextInputStyleLarge" 
                            MaxLength="16" TabIndex="1" Width="170px"></asp:TextBox>
                        &nbsp;/Allot
                        <asp:TextBox ID="AllotTextBox" runat="server" AutoPostBack="True" 
                            CssClass="TextInputStyleSmall" MaxLength="10" 
                            onkeypress="fncInputNumericValuesOnly()" 
                            TabIndex="1" Width="70px"></asp:TextBox>
                        &nbsp;/Folio<asp:TextBox ID="folioTextBox" runat="server" AutoPostBack="True" 
                            CssClass="TextInputStyleSmall" MaxLength="6" 
                            onkeypress="fncInputNumericValuesOnly()" 
                             TabIndex="1" Width="90px"></asp:TextBox>
                        <asp:Button ID="findButton" runat="server" AccessKey="f" CssClass="buttonmid" 
                            onclick="findButton_Click" OnClientClick="return fnCheckRegNo();" TabIndex="1" 
                            Text="Find" />
                        &nbsp;&nbsp;<asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                            Width="70px" />
                        </b></td>
                   
                </tr>
                <tr>
                    <td align="left" >Rgistration Date:</td>
                    <td align="left" colspan="3">
                        <asp:TextBox ID="regDateTextBox" runat="server" CssClass="textInputStyleDate" 
                            TabIndex="2"></asp:TextBox>
                        <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" TargetControlID="RegDateTextBox" PopupButtonID="RegDateImageButton" Format="dd-MMM-yyyy"/>
                        <asp:ImageButton ID="RegDateImageButton" runat="server" 
                            AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                            TabIndex="3" />
                        <span class="star">*</span></td>  
                         
                     
                </tr>
                <tr>
                    <td align="left" >Registration Type:</td>
                    <td align="left">
                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="RegDateTextBox" PopupButtonID="RegDateImageButton" Format="dd-MMM-yyyy"/>
                        <asp:DropDownList ID="regTypeDropDownList" runat="server" 
                            CssClass="DropDownList" TabIndex="4">
                        
                        </asp:DropDownList><span class="star">*</span></td>  
                 <td align="center" colspan="2" rowspan="12" id="tdImage" runat="server">
                     <asp:Image ID="SignImage" runat="server" Height="236px" 
                         meta:resourcekey="SignImageResource1" Width="340px" />
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
                            
                </tr>
                <tr>
                    <td align="left" >Is ID Account</td>
                    <td align="left"> 
                        <asp:DropDownList ID="isIDDropDownList" runat="server" onchange=" fnClearID(this.value)"
                            CssClass="DropDownList" TabIndex="5" AutoPostBack="True" 
                           >
                        <asp:ListItem Value="N" Selected="True">No</asp:ListItem>
                        <asp:ListItem Value="Y">Yes</asp:ListItem>            
                        </asp:DropDownList><span class="star">*</span>&nbsp;&nbsp;ID A/C No:<asp:TextBox 
                            ID="IDAccNoTextBox" runat="server" CssClass="textInputStyle" onkeypress= "CheckIDStatus()"
                            TabIndex="5" MaxLength="6"></asp:TextBox>
                                            </td>
                </tr>
                 <tr>
                 <td align="left">ID Institution:</td>
                 <td align="left">
                        <asp:DropDownList ID="IDbankNameDropDownList" runat="server" TabIndex="5" 
                            ></asp:DropDownList>
                                            </td>
                </tr>
                <tr>
                    <td align="left" >ID Institution Branch</td>    
                    <td align="left">
                         <asp:DropDownList ID="IDbranchNameDropDownList" runat="server" 
                                TabIndex="5" 
                                  meta:resourcekey="branchNameDropDownListResource1" ></asp:DropDownList>
                                            </td>    
                </tr>
                <tr>
                    <td align="left"   colspan="2"><b>Principal Holder Information</b></td>        
                   
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
                    
                </tr>
                <tr>
                    <td align="left" >Fa/Hus&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="holderFMTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7"></asp:TextBox>
                                            <span class="star">*</span></td>
                </tr>
                 <tr>
                     <td align="left" >Mother&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="holderMotherTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="125" TabIndex="7" 
                            meta:resourcekey="holderFMTextBoxResource1"></asp:TextBox>
                        <span class="star">*</span></td>
                </tr>
                 <tr>
                     <td align="left" >Occupation:</td>
                    <td align="left">
                        <asp:DropDownList ID="holderOccupationDropDownList" runat="server" TabIndex="8">
                        </asp:DropDownList>
                        <span class="star">*</span></td>
                </tr>
                 <tr>
                   <td align="left" >Nationality:</td>
                    <td align="left">
                        <asp:TextBox ID="holderNationalityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="9"></asp:TextBox>
                                            <span class="star">*</span></td>
                 
                </tr>
                <tr>
                   <td align="left" >Address1:</td>
                    <td align="left">
                        <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="10"></asp:TextBox>
                                            <span class="star">*</span></td>
                 
                </tr>
                <tr>
                    <td align="left">Address2:</td>
                    <td align="left">
                        <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="11"></asp:TextBox>
                                            <span class="star">*</span></td>
                     <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>   
                <tr>
                          <td align="left">City:</td>
                          <td align="left">
                         <asp:TextBox ID="holderCityTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="35" TabIndex="12"></asp:TextBox>
                                            </td>
                    <td align="left" class="style7" style="text-align: right">Balance:</td>
                    <td align="left">
                        <asp:TextBox ID="BalaceNoTextBox" runat="server" CssClass="textInputStyle" 
                            Font-Bold="True" Font-Size="Medium" ForeColor="#FF6600" MaxLength="6" 
                            onkeypress="CheckIDStatus()" TabIndex="5" Width="223px"></asp:TextBox>
                          </td>
                </tr>
                <tr>
                          <td align="left">Telephone/Mobile:</td>
                          <td align="left">
                         <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13"></asp:TextBox>
                                            </td>
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                <tr>
                          <td align="left">E-Mail:</td>
                          <td align="left">
                         <asp:TextBox ID="holderEmailTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="13"></asp:TextBox>
                                            </td>
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
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
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
                  <tr>
                          <td align="left">National ID:</td>
                          <td align="left">
                        <asp:TextBox ID="holderNationalIDTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="15" 
                            meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                                            </td>
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
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
                         
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
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
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
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
                    <td align="left">&nbsp;</td>
                    <td align="left">
                        &nbsp;</td>
                </tr>
               <%--  <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>--%>
                </table>
           
            
     </div>
    <br />
    <table width="500" align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="right">
            &nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
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

