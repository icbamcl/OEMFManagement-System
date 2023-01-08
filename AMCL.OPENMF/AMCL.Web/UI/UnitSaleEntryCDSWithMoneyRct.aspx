<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSaleEntryCDSWithMoneyRct.aspx.cs" Inherits="UI_UnitSaleEntryCDSWithMoneyRct" Title=" Unit Sale Entry Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {
            document.getElementById("<%=saleDateTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleNumberTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleRateTextBox.ClientID%>").value ="";          
            document.getElementById("<%=saleRemarksTextBox.ClientID%>").value =""; 
                    
       
            
         
               
            return false;
        }
        else
        {
         return false;
        }
       
    }
    
    
     function fnResetAll()
    {
       
              
            document.getElementById("<%=saleDateTextBox.ClientID%>").value ="";
            
            
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=saleNumberTextBox.ClientID%>").value ="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value ="";          
            document.getElementById("<%=saleRemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=saleDateTextBox.ClientID%>").value="";
            document.getElementById("<%=saleRateTextBox.ClientID%>").value="";
            document.getElementById("<%=unitQtyTextBox.ClientID%>").value ="";
         
             
             
             alert("Invalid Registration Number");
             return false;
      
    }
     function fnCheqInput()
        {
        
        
              //Input Text Checking
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
            
           if(document.getElementById("<%=saleDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleDateTextBox.ClientID%>").focus();
                alert("Please Enter Sale Date");
                return false;
                
            }
            if(document.getElementById("<%=saleDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=saleDateTextBox.ClientID%>").value))
                    {
                    document.getElementById("<%=saleDateTextBox.ClientID%>").focus();
                    alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
            if(document.getElementById("<%=saleNumberTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleNumberTextBox.ClientID%>").focus();
                alert("Please Enter Sale Number");
                return false;
                
            }
            
          
            if(document.getElementById("<%=saleRateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=saleRateTextBox.ClientID%>").focus();
                alert("Please Enter Sale Rate");
                return false;
                
            }
            if(document.getElementById("<%=unitQtyTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=unitQtyTextBox.ClientID%>").focus();
                alert("Please Enter Unit Quantity");
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
            }
       
    
       function  fnFindChek()//check Find Button validation
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
      .style6
        {
            font-size: smaller;
            font-weight: bold;
            color: #009933;
        }
     .style19
     {
         font-size: large;
         font-weight: bold;
         color: #75C8FF;
     }
     .style20
     {
         color: #FF3300;
     }
     .auto-style1 {
         font-family: Verdana, Arial, Helvetica, sans-serif;
         border: 1px #1B68B8 solid;
         BACKGROUND-COLOR: #FFFFDD;
         COLOR: #000000;
         FONT-SIZE: x-small;
         padding-left: 2px;
         font-weight: 700;
     }
     .auto-style2 {
         height: 18px;
     }
     .auto-style3 {
         text-align: left;
     }
     .auto-style4 {
         height: 18px;
         text-align: right;
     }
     .auto-style5 {
         font-weight: bold;
         text-align: right;
     }
     .auto-style6 {
         text-align: right;
     }
     </style>



    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
              Unit Holder Sale Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      <br />
      <div id="dvContent" runat="server">
   <%--  <asp:UpdatePanel ID="HolderInfoUpdatePanel" runat="server">--%>
     <%-- <ContentTemplate>--%>
    <table width="1100" align="center" cellpadding="0" cellspacing="0" border="0" >
    <colgroup width="120"></colgroup>
    <colgroup width="320"></colgroup>
    <colgroup width="160"></colgroup>
    <colgroup width="160"></colgroup>
    <tr>
        <td class="auto-style5">Sale No:</td>
        <td align="left">
            <asp:TextBox ID="saleNumberTextBox" runat="server" 
                CssClass="auto-style1" MaxLength="6" 
                meta:resourcekey="saleNumberTextBoxResource1" 
                onkeypress="fncInputNumericValuesOnly()" Height="17px" Width="76px"></asp:TextBox>
            <span class="star">*</span></td>
        <td colspan="2" align="center"><span class="style6"  
                           style="border:1px solid">Signature    and 
                       Photo</span></td>
    </tr>
     <tr>
        <td class="auto-style6">Money Receipt No:</td>
        <td align="left">
            <asp:DropDownList ID="moneyReceipDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="moneyReceipDropDownList_SelectedIndexChanged" >
            </asp:DropDownList>
            </td>
        <td colspan="2" rowspan="10" align="center">
            <asp:Image ID="SignImage" runat="server" Height="202px" 
                meta:resourcekey="SignImageResource1" Width="286px" />
         </td>
    </tr>
     <tr>
        <td class="auto-style6">Registration No:</td>
        <td align="left"> 
        
                    <asp:TextBox ID="fundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="branchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                                    <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"   
                    CssClass= "TextInputStyleSmall" TabIndex="1" AutoPostBack="True" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" 
                        ontextchanged="regNoTextBox_TextChanged"></asp:TextBox>
                                    <span class="star">*</span>&nbsp;<asp:Button 
                    ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                    onclick="findButton_Click" AccessKey="f" 
                    onclientclick="return fnFindChek();" TabIndex="2" 
                    meta:resourcekey="findButtonResource1" /></td>
    </tr>
     <tr>
       <td class="auto-style6">Payment Type:</td>
        <td align="left"><table>
            <tr>
                <td  align="center">
                     <asp:RadioButton ID="ChqRadioButton" runat="server" 
                    Checked="True" GroupName="payType" Text="CHQ" 
                   TabIndex="2" Enabled="False" />
                </td>
                 <td  align="center">
                 <asp:DropDownList ID="ChequeTypeDropDownList" runat="server" 
                    CssClass="DropDownList" 
                    meta:resourcekey="saleTypeDropDownListResource1" Height="27px" Enabled="False">
                <asp:ListItem Value="CHQ" Selected="True">CHQ</asp:ListItem>
                <asp:ListItem Value="DD">DD</asp:ListItem>
                <asp:ListItem Value="PO">PO</asp:ListItem>
                </asp:DropDownList>
                </td>
                 <td  align="center">
                   <asp:RadioButton ID="CashRadioButton" runat="server" AutoPostBack="True" 
                    GroupName="payType" Text="CASH" 
                    TabIndex="3" Enabled="False" />
                </td>
                 <td  align="center">
                  <asp:RadioButton ID="BothRadioButton" runat="server" AutoPostBack="True" 
                    GroupName="payType" Text="BOTH" 
                   TabIndex="4" Enabled="False" />
                </td>
                 <td  align="center">
                 <asp:RadioButton ID="MultiRadioButton" runat="server" Text="MULTIPLE" 
                    GroupName="payType" AutoPostBack="True" 
                    TabIndex="5" Enabled="False" />
                </td>
            </tr>
            </table></td>
    </tr>
     <tr>
      <td class="auto-style6">Cheque/DD/PO No:</td>
        <td align="left">
           <table>
            <tr>
                <td class="style18">
                <asp:TextBox ID="CHQDDNoRemarksTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" 
                    meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="6" Width="148px" Enabled="False"></asp:TextBox>
                </td>
                <td>
                 Date:
                </td>
                   <td align="left" class="style11">
                <asp:TextBox ID="chequeDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="6" meta:resourcekey="saleDateTextBoxResource1" Enabled="False"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="chequeDateTextBox" PopupButtonID="chequeDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
                <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" Enabled="False" />
                </td>
            </tr>
            </table>
                                </td>
     </tr>
     <tr>
        <td class="auto-style6">Bank Name:</td>
        <td align="left">
            <asp:DropDownList ID="bankNameDropDownList" runat="server" 
                meta:resourcekey="bankNameDropDownListResource1" 
                onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged" TabIndex="7" Enabled="False">
            </asp:DropDownList>
                                </td>
    </tr>
     <tr>
      <td class="auto-style6">Branch Name:</td>
       <td align="left">
           <asp:DropDownList ID="branchNameDropDownList" runat="server" 
               meta:resourcekey="branchNameDropDownListResource1" TabIndex="8" Enabled="False">
           </asp:DropDownList>
                                </td>
    </tr>
     <tr>
        <td class="auto-style6">Cash Amount:</td>
       <td align="left">
           <asp:TextBox ID="CashAmountTextBox" runat="server" 
               CssClass="TextInputStyleLarge" MaxLength="55" 
               meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="9" Enabled="False"></asp:TextBox>
                                </td>
    </tr>
     <tr>
        <td class="auto-style6">Multiple Payment:</td>
       <td align="left">
           <asp:TextBox ID="MultiplePayTypeTextBox" runat="server" 
               CssClass="TextInputStyleLarge" MaxLength="256" 
               meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="10" Width="280px" Enabled="False"></asp:TextBox>
                                </td>
     </tr>
   <tr>
        <td class="auto-style4">Sale Date:</td>
       <td align="left" class="auto-style2">
           <asp:TextBox ID="saleDateTextBox" runat="server" CssClass="textInputStyleDate" 
               meta:resourcekey="saleDateTextBoxResource1" TabIndex="11" Enabled="False"></asp:TextBox>
           <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
               Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
               TargetControlID="saleDateTextBox" />
           <asp:ImageButton ID="RegDateImageButton" runat="server" 
               AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
               meta:resourcekey="RegDateImageButtonResource1" TabIndex="10" Enabled="False" />
           <span class="star">* </span></td>
    </tr>
     <tr>
       <td class="auto-style6">Sale Type:</td>
       <td align="left">
           <asp:DropDownList ID="saleTypeDropDownList" runat="server" 
               CssClass="DropDownList" meta:resourcekey="saleTypeDropDownListResource1" 
               TabIndex="12" Enabled="False">
               <asp:ListItem meta:resourcekey="ListItemResource1" Selected="True" Value="SL">SALE</asp:ListItem>
               <asp:ListItem meta:resourcekey="ListItemResource2" Value="CIP">CIP</asp:ListItem>
           </asp:DropDownList>
           <span class="star">*</span></td>
    </tr>
      <tr>
        <td class="auto-style6">Sale Rate:</td>
        <td style="text-align: left">
            <asp:TextBox ID="saleRateTextBox" runat="server" CssClass="textInputStyleDate" 
                MaxLength="6" meta:resourcekey="saleRateTextBoxResource1" TabIndex="13" Enabled="False"></asp:TextBox>
            <span class="star">*</span></td>
        <td style="text-align: right"><span class="style20"><b style="text-align: right">CIP 
            :&nbsp; </b></span></td>
        <td style="text-align: left; color:Red" id="tdCIP" runat="server"></td>
    </tr>
     <tr>
        <td class="auto-style6">Unit Quantity:</td>
        <td style="text-align: left"><asp:TextBox ID="unitQtyTextBox" runat="server"  MaxLength="10"
                    CssClass= "textInputStyleDate" TabIndex="14" 
                    meta:resourcekey="unitQtyTextBoxResource1" 
                    onkeypress= "fncInputNumericValuesOnly()" Enabled="False"></asp:TextBox>
                                    <span class="star">*</span></td>
        <td align="right">Name of Holder :</td>
        <td align="left">                <asp:TextBox ID="holderNameTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" 
                    meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td class="auto-style6">Remarks:</td>
        <td style="text-align: left">
            <asp:TextBox ID="saleRemarksTextBox" runat="server" 
                CssClass="TextInputStyleLarge" MaxLength="55" 
                meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="15" Width="278px" Enabled="False"></asp:TextBox>
         </td>
        <td align="right" >Name of Joint Holder :</td>
        <td align="left">
                <asp:TextBox ID="jHolderTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td class="auto-style6">Selling Agent Code:</td>
        <td class="auto-style3">
            <asp:TextBox ID="sellingAgentCodeTextBox" runat="server" 
                CssClass="TextInputStyleLarge" MaxLength="55" 
                meta:resourcekey="saleRemarksTextBoxResource1" TabIndex="15" Width="278px" Enabled="False"></asp:TextBox>
         </td>
         <td align="right" >Address1 :</td>
           <td align="left">
                <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
     <tr>
        <td colspan="2" align="center">
                &nbsp;</td>
        <td align="right" >Address2 :</td>
         <td align="left">
                <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
     </tr>
     <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
         <td align="right" >Telephone/Mobile :</td>
         <td align="left">
                <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                    CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                    meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
            </td>
    </tr>
 
      <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
   
    </table>
   <%-- </ContentTemplate>--%>
       <%-- <Triggers>       
            <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="regNoTextBox" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="addListButton" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>--%>
     </div>
     <table width="1100" align="center" cellpadding="0" cellspacing="0" border="0" >
      <tr>
        <td colspan="5" align="center">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="SaveButton_Click" AccessKey="s" 
                meta:resourcekey="SaveButtonResource1"/>&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="a" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        </td>
    </tr>
     </table>
    <br />
  </asp:Content> 
