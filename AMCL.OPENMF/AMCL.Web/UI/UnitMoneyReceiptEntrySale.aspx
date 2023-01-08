<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitMoneyReceiptEntrySale.aspx.cs" Inherits="UI_UnitMoneyReceiptEntrySale" Title=" Unit Money Receipt Entry Form (Design and Developed by Sakhawat)" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
    
        function fnCheqInput()
        {
            if(document.getElementById("<%=ReceiptNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=ReceiptNoTextBox.ClientID%>").focus();
                alert("Please Enter Receipt Number");
                return false;
                
            }
            
            if(document.getElementById("<%=ReceiptDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=ReceiptDateTextBox.ClientID%>").focus();
                alert("Please Enter Receipt Date ");
                return false;
                
            }
            if(document.getElementById("<%=NameTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=NameTextBox.ClientID%>").focus();
                alert("Please Enter Holder Name ");
                return false;
                
            }
            if(document.getElementById("<%=addressTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=addressTextBox.ClientID%>").focus();
                alert("Please Enter Holder Address ");
                return false;
                
            }
           if(document.getElementById("<%=QtyTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=QtyTextBox.ClientID%>").focus();
                alert("Please Enter Unit Quantity ");
                return false;
                
           }
           if(document.getElementById("<%= RateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%= RateTextBox.ClientID%>").focus();
                alert("Please Enter Rate ");
                return false;
                
           }
            if(document.getElementById("<%= sellingAgentCodeTextBox.ClientID%>").value =="0")
            {
                document.getElementById("<%= sellingAgentCodeTextBox.ClientID%>").focus();
                alert("Selling agent code can not be 0 ");
                return false;
                
            }

            

            if (document.getElementById("<%=  ChqRadioButton.ClientID%>").checked ==true || document.getElementById("<%=  BothRadioButton.ClientID%>").checked ==true)
            {
                  if(document.getElementById("<%= CHQDDPONOTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%= CHQDDPONOTextBox.ClientID%>").focus();
                        alert("Please Enter Cheque Number ");
                        return false;
                
                    }
                if(document.getElementById("<%= chequeDateTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%= chequeDateTextBox.ClientID%>").focus();
                        alert("Please Enter Cheque Date ");
                        return false;
                
                     }
                if(document.getElementById("<%= RoutingNoTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%= RoutingNoTextBox.ClientID%>").focus();
                        alert("Please Enter Routing Number ");
                        return false;
                
                     }
                if(document.getElementById("<%= BankInfoTextBox.ClientID%>").value =="")
                    {
                        document.getElementById("<%= BankInfoTextBox.ClientID%>").focus();
                        alert("Please Enter Bank Branch Information ");
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
    
     .auto-style1 {
         font-size: xx-small;
     }
     .auto-style2 {
         font-family: Verdana, Arial, Helvetica, sans-serif;
         border: 1px #666666 solid;
         BACKGROUND-COLOR: #FFFFFF;
         COLOR: #465360;
         FONT-SIZE: xx-small;
         padding-left: 2px;
         height: 17px;
     }
    
     .auto-style3 {
         font-family: Verdana, Arial, Helvetica, sans-serif;
         border: 1px #1B68B8 solid;
         BACKGROUND-COLOR: #FFFFDD;
         COLOR: #FF6699;
         FONT-SIZE: 12px;
         padding-left: 2px;
     }
    
     .auto-style4 {
         height: 25px;
     }
    
     .auto-style5 {
         width: 101px;
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
     }
    
     </style>
    


    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
              Unit Holder Sale Money Receipt Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      <br />
      <div id="divContent" runat="server" style="width:1000px; height:auto"  align="center">
         <table width="1000px" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="350"></colgroup>
            <colgroup width="200"></colgroup>
            <tr>
        <td colspan="4" align="left" class="auto-style4">
             <table width="160px" align="left" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="70"></colgroup>
            <colgroup width="90"></colgroup>
                 <%-- <colgroup width="70"></colgroup>
            <colgroup width="70"></colgroup>--%>
                <tr>
                    <td align="left" style=" background-color: #547DD3; height:25px" id="tdSale" runat="server"> &nbsp; 
                       <asp:HyperLink ID="SaleHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntrySale.aspx" Font-Bold="True" ForeColor="White">  Unit Sale </asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px " id="tdRep" runat="server">
                  
                    <span class="style3">|</span><asp:HyperLink ID="RepHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryRepurchase.aspx" Font-Bold="True" >Repurchase</asp:HyperLink>
                    </td>
                    <%-- <td style=" background-color: #CCCCFF; height:25px" id="tdTR" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="TRHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryTransfer.aspx" Font-Bold="True">Transfer</asp:HyperLink>
                    </td >
                  <td style=" background-color: #CCCCFF; height:25px" id="tdRen" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="RNHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryRenewal.aspx" Font-Bold="True">Renewal</asp:HyperLink>
                    </td >--%>
                </tr>
           </table>
        </td>
        
    </tr>
            <tr>
        <td colspan="4" align="left">
        <fieldset>
            <legend style="font-weight: 1000"> ::Money Receipt Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="170"></colgroup>
            <colgroup width="350"></colgroup>
            <colgroup width="150"></colgroup>
             <tr>
        <td align="right" ><b>Registration No (if any):</b></td>
        <td align="left">
            <asp:DropDownList ID="fundCodeDDL" runat="server"  Width="80px" OnSelectedIndexChanged="fundCodeDDL_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
           <b>/</b> <asp:DropDownList ID="branchCodeDDL" runat="server" Width="80px" Enabled="False" ></asp:DropDownList>
            <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" TabIndex="1" ></asp:TextBox>
                                <span class="star">*<asp:Button 
                            ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            onclick="findButton_Click" AccessKey="f" 
                            TabIndex="2"
                            /></span></td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp; Receipt No:</strong></td>
                        <td > <asp:TextBox ID="ReceiptNoTextBox" runat="server"  CssClass= "TextInputStyleSmall"   
                                     Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date: </b></td>
                        <td align="left">
                            <asp:TextBox ID="ReceiptDateTextBox" runat="server" CssClass="textInputStyleDate" TabIndex="4" 
                                ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" 
                                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="ImageButton2" 
                                TargetControlID="ReceiptDateTextBox" />
                            <asp:ImageButton ID="ImageButton2" runat="server" 
                                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                               />
                            <span class="star">*</span></td>
                     </tr>
                      </table>             
                </td>
      
       
    </tr>
    <tr>
        <td align="right" ><b>BO :</b></td>
        <td align="left">
                        <asp:TextBox ID="holderBOTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="16" TabIndex="1"  onkeypress= "fncInputNumericValuesOnly()"
                           Width="137px"></asp:TextBox>
                        &nbsp;</td>  
             
        <td align="right"><b>Name&nbsp; of Holder:</b></td>
        <td align="left">
                  <span class="star">
            <asp:TextBox ID="NameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" TabIndex="5" 
               ></asp:TextBox>
                                *</span></td>
    </tr>
    <tr>
        <td align="right" ><b>Address:</b></td>
        <td align="left" >
            
                        <asp:TextBox ID="addressTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine" TabIndex="6"></asp:TextBox>
                                    <span class="star">*</span></td>   
                <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp; Unit Quantity:</strong></td>
                        <td > <asp:TextBox ID="QtyTextBox" runat="server"  CssClass= "TextInputStyleSmall"   onkeypress= "fncInputNumericValuesOnly()"
                                     Width="100px" TabIndex="7" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><strong>&nbsp;&nbsp;&nbsp;&nbsp; Unit Rate:</strong></td>
                        <td align="left">
                                                    <asp:TextBox ID="RateTextBox" runat="server"   
                                    CssClass= "TextInputStyleSmall" 
                                   
                                   Width="95px" TabIndex="8" Enabled="False" 
                                      ></asp:TextBox>
                                                    <span class="star">*</span></td>
                     </tr>
                      </table>             
                </td>
             
       
    </tr>
  <tr>
                       <td align="right""><strong>&nbsp;Payment Type:</strong></td>
                        <td align="left">
                            <table>
                            <tr>
                                <td  align="center"> 
                                    <strong> 
                                    <asp:RadioButton ID="ChqRadioButton" runat="server" GroupName="payType" Text="CHQ" TabIndex="9" CssClass="auto-style1" Checked="True" />
                                    </strong>
                                </td>
                                <td  align="center">
                                     <strong>
                                     <asp:DropDownList ID="ChequeTypeDropDownList" runat="server" CssClass="auto-style2"  Height="27px" TabIndex="9">
                                    <asp:ListItem Value="CHQ">CHQ</asp:ListItem>
                                    <asp:ListItem Value="DD">DD</asp:ListItem>
                                    <asp:ListItem Value="PO">PO</asp:ListItem>
                                     </asp:DropDownList>
                                     </strong>
                                </td>
                                 <td  align="center">
                                     <strong>
                                   <asp:RadioButton ID="CashRadioButton" runat="server"  GroupName="payType" Text="CASH"  TabIndex="10" CssClass="auto-style1" />                                    
                                     </strong>                                    
                                </td>
                                 <td  align="center">
                                     <strong>
                                    <asp:RadioButton ID="BothRadioButton" runat="server" GroupName="payType" Text="BOTH"  TabIndex="11" CssClass="auto-style1" />                                                                        
                                     </strong>                                                                        
                                </td>
                                 <td  align="center">
                                     <strong>
                                    <asp:RadioButton ID="MultiRadioButton" runat="server" Text="MULTIPLE"  GroupName="payType"  CssClass="auto-style1" TabIndex="12" />                                                                      
                                     </strong>                                                                      
                                </td>
                            </tr>
                            </table>
                         </td>
                             <td align="right"><strong>Cheque/DD/PO No:</strong>  </td>
                            
                             <td align="left">
                                 <table>
                                 <tr>
                                    <td align="left" class="auto-style5" >
                                        <asp:TextBox ID="CHQDDPONOTextBox" runat="server" Width="95px"    CssClass= "TextInputStyleSmall" onkeypress= "fncInputNumericValuesOnly()" TabIndex="13" ></asp:TextBox>                                                                                 
                                    </td>
                                    <td align="right">
                                        <strong> Cheque Date:</strong>
                                    </td>
                                     <td align="left">
                                        <asp:TextBox ID="chequeDateTextBox" runat="server" CssClass="textInputStyleDate" TabIndex="14" ></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                        TargetControlID="chequeDateTextBox" PopupButtonID="chequeDateImageButton" 
                                        Format="dd-MMM-yyyy" Enabled="True"/>
                                        <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                                        AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                                        TabIndex="10" />
                                        <span class="star">* </span>
                                   </td>
                                </tr>
                             </table>
                             </td>
                       

                    </tr>

                <tr>
                        <td align="right"><strong>&nbsp;Routing No:</strong></td>
                         <td align="left" >
                                        <asp:TextBox ID="RoutingNoTextBox" runat="server" Width="95px"  MaxLength="10"   CssClass= "TextInputStyleSmall" onkeypress= "fncInputNumericValuesOnly()" TabIndex="15" ></asp:TextBox>                                                                                 
                                    &nbsp;<span class="star">* </span><asp:Button 
                    ID="findRoutingButton" runat="server" Text="Find" CssClass="buttonmid" 
                                 
                    TabIndex  ="16" OnClick="findRoutingButton_Click" />                                                                                 
                                    </td>
                     <td align="right">
                                        <strong>Bank Info:</strong>
                                    </td>
                                     <td align="left">
                                        <asp:TextBox ID="BankInfoTextBox" runat="server" Width="280px"   Height="36px"  CssClass= "auto-style3"  onkeypress= "fncInputNumericValuesOnly()" TextMode="MultiLine" TabIndex="17" ></asp:TextBox>
                                        <span class="star">* </span>
                                   </td>
                      
                     </tr>
                    <tr>
                        <td align="right"><strong>Cash Amount:</strong></td>
                        <td align="left">
                               <asp:TextBox ID="CashAmountTextBox" runat="server"  CssClass="TextInputStyleLarge" MaxLength="55" TabIndex="18"></asp:TextBox>
                         </td>
                 
                        <td align="right"><strong>Multiple Payment:</strong></td>
                        <td align="left">
                           <asp:TextBox ID="MultiplePayTypeTextBox" runat="server"   CssClass="TextInputStyleLarge" MaxLength="256" Width="280px" TabIndex="19"></asp:TextBox>
                        </td>
                     </tr>
                <tr>
                        <td align="right"><strong>Selling Agent&#39;s Name:</strong></td>
                        <td align="left">
                               <asp:DropDownList ID="agentNameDDL" runat="server" AutoPostBack="True" OnSelectedIndexChanged="agentNameDDL_SelectedIndexChanged"  ></asp:DropDownList>
                         </td>
                 
                        <td align="right"><strong>Selling Agent&#39;s Code:</strong></td>
                        <td align="left">
                           <asp:TextBox ID="sellingAgentCodeTextBox" runat="server"   CssClass="TextInputStyleLarge" MaxLength="256" Width="150px" TabIndex="19"></asp:TextBox>
                                <span class="star">*<asp:Button 
                            ID="agentCodefindButton" runat="server" Text="Find" CssClass="buttonmid" 
                            
                            TabIndex="0" OnClick="agentCodefindButton_Click"
                            /></span>
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
                <td align="center" colspan="4">       <asp:Button ID="PrintSaveButton" runat="server" AccessKey="p" 
                                        CssClass="auto-style6" OnClientClick="return fnCheqInput();" 
                                        Text="Save and Print" TabIndex="20" Width="110px" OnClick="PrintSaveButton_Click" />


                                    &nbsp;&nbsp;&nbsp;
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="CloseButton" runat="server" AccessKey="c" 
                                        CssClass="buttoncommon" meta:resourcekey="CloseButtonResource1" 
                                        onclick="CloseButton_Click" Text="Close" TabIndex="22" />


                                    </td>        
            </tr>
            
         
    </table>
    
       </div>
      
     
    <br />
  </asp:Content> 
