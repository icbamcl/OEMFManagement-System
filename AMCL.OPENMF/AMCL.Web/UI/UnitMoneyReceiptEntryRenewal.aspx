<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitMoneyReceiptEntryRenewal.aspx.cs" Inherits="UI_UnitMoneyReceiptEntryRenewal" Title=" Unit Money Receipt Entry Form (Design and Developed by Sakhawat)" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
    
    
    
            
      
       
  
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
    
     </style>
    


    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
              Unit Holder Renewal Money Receipt Entry Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            
        </tr> 
      </table>
      <br />
      <div id="divContent" runat="server" style="width:980px; height:auto"  align="center">
         <table width="970px" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="310"></colgroup>
            <colgroup width="200"></colgroup>
            <tr>
        <td colspan="4" align="left" class="auto-style4">
            <table width="300px" align="left" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="70"></colgroup>
            <colgroup width="90"></colgroup>
            <colgroup width="70"></colgroup>
            <colgroup width="70"></colgroup>
                <tr>
                    <td align="left" style=" background-color: #CCCCFF; height:25px" id="tdSale" runat="server"> &nbsp; 
                       <asp:HyperLink ID="SaleHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntrySale.aspx" Font-Bold="True" >  Unit Sale </asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px " id="tdRep" runat="server">
                  
                    <span class="style3">|</span><asp:HyperLink ID="RepHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryRepurchase.aspx" Font-Bold="True" >Repurchase</asp:HyperLink>
                    </td>
                    <td style=" background-color: #CCCCFF; height:25px" id="tdTR" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="TRHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryTransfer.aspx" Font-Bold="True">Transfer</asp:HyperLink>
                    </td >
                  <td style=" background-color: #547DD3; height:25px" id="tdRen" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="RNHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryRenewal.aspx" Font-Bold="True" ForeColor="White">Renewal</asp:HyperLink>
                    </td >
                </tr>
           </table>
        </td>
        
    </tr>
            <tr>
        <td colspan="4" align="left">
        <fieldset>
            <legend style="font-weight: 700"> ::Money Receipt Information::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="170"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="150"></colgroup>
             <tr>
        <td align="right" ><b>Registration No :</b></td>
        <td align="left">
            <asp:DropDownList ID="fundCodeDDL" runat="server"  Width="60px" ></asp:DropDownList>
           <b>/</b> <asp:DropDownList ID="branchCodeDDL" runat="server" Width="60px" ></asp:DropDownList>
            <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                <span class="star">*<asp:Button 
                            ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            onclick="findButton_Click" AccessKey="f" 
                            onclientclick="return fnCheqInput();"
                            /></span></td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp; Receipt No:</strong></td>
                        <td > <asp:TextBox ID="ReceiptNoTextBox" runat="server"  CssClass= "TextInputStyleSmall"   
                                     Width="100px" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date: </b></td>
                        <td align="left">
                            <asp:TextBox ID="ReceiptDateTextBox" runat="server" CssClass="textInputStyleDate" 
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
       
        <td align="right"><b>Name&nbsp; of Holder:</b></td>
        <td align="left">
                  <span class="star">
            <asp:TextBox ID="NameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" 
               ></asp:TextBox>
                                *</span></td>
       <td align="right" ><b>Address:</b></td>
        <td align="left" >
            
                        <asp:TextBox ID="TextBox1" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine"></asp:TextBox>
                                    <span class="star">*</span></td> 
             
    </tr>
    <tr>
        <td align="right" ><b>Renewal Type:</b></td>
        <td align="left" >
            
                        <asp:DropDownList ID="renewalTypeDropDownList" runat="server" CssClass="DropDownList" Font-Bold="True">
                            <asp:ListItem Selected="True" Value="S">Sub-Division</asp:ListItem>
                            <asp:ListItem  Value="C">Consolidation</asp:ListItem>
                            <asp:ListItem  Value="L">Lost</asp:ListItem>
                        </asp:DropDownList>
        </td>   
                <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp; Unit Quantity:</strong></td>
                        <td > <asp:TextBox ID="QtyTextBox" runat="server"  CssClass= "TextInputStyleSmall"   
                                     Width="100px" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><strong>&nbsp; Renwal Fee:</strong></td>
                        <td align="left">
                                                    <asp:TextBox ID="RenwalFeeTextBox" runat="server"   
                                    CssClass= "TextInputStyleSmall" 
                                    onkeypress= "fncInputNumericValuesOnly()"
                                   Width="95px" 
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
                                    <asp:RadioButton ID="ChqRadioButton" runat="server" AutoPostBack="True"  Checked="True" GroupName="payType" Text="CHQ" oncheckedchanged="ChqRadioButton_CheckedChanged" TabIndex="2" CssClass="auto-style1" />
                                    </strong>
                                </td>
                                <td  align="center">
                                     <strong>
                                     <asp:DropDownList ID="ChequeTypeDropDownList" runat="server" CssClass="auto-style2"  Height="27px">
                                    <asp:ListItem Value="CHQ" Selected="True">CHQ</asp:ListItem>
                                    <asp:ListItem Value="DD">DD</asp:ListItem>
                                    <asp:ListItem Value="PO">PO</asp:ListItem>
                                     </asp:DropDownList>
                                     </strong>
                                </td>
                                 <td  align="center">
                                     <strong>
                                   <asp:RadioButton ID="CashRadioButton" runat="server" AutoPostBack="True" GroupName="payType" Text="CASH" oncheckedchanged="CashRadioButton_CheckedChanged" TabIndex="3" CssClass="auto-style1" />                                    
                                     </strong>                                    
                                </td>
                                 <td  align="center">
                                     <strong>
                                    <asp:RadioButton ID="BothRadioButton" runat="server" AutoPostBack="True" GroupName="payType" Text="BOTH" oncheckedchanged="BothRadioButton_CheckedChanged" TabIndex="4" CssClass="auto-style1" />                                                                        
                                     </strong>                                                                        
                                </td>
                                 <td  align="center">
                                     <strong>
                                    <asp:RadioButton ID="MultiRadioButton" runat="server" Text="MULTIPLE"  GroupName="payType" AutoPostBack="True"  CssClass="auto-style1" />                                                                      
                                     </strong>                                                                      
                                </td>
                            </tr>
                            </table>
                             <td align="right"><strong>Cheque/DD/PO No:</strong>

                             </td>
                             <td align="left">
                             <table>
                                <tr>
                                    <td align="left" >
                                        <asp:TextBox ID="CHQDDPONOTextBox" runat="server" Width="95px"  MaxLength="15"   CssClass= "TextInputStyleSmall" onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>                                                                                 
                                    </td>
                                    <td align="right">
                                        &nbsp;<strong>&nbsp; Cheque Date:</strong>
                                    </td>
                                     <td align="left">
                                        <asp:TextBox ID="chequeDateTextBox" runat="server" CssClass="textInputStyleDate" TabIndex="6" ></asp:TextBox>
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
                        </td>

                    </tr>

                <tr>
                        <td align="right"><strong>&nbsp;Routing No:</strong></td>
                         <td align="left" >
                                        <asp:TextBox ID="RoutingNoTextBox" runat="server" Width="95px"  MaxLength="10"   CssClass= "TextInputStyleSmall" onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>                                                                                 
                                    &nbsp;<span class="star">* </span><asp:Button 
                    ID="findRoutingButton" runat="server" Text="Find" CssClass="buttonmid" 
                    onclick="findButton_Click" AccessKey="0" 
                    onclientclick="return fnFindChek();" 
                    meta:resourcekey="findButtonResource1" />                                                                                 
                                    </td>
                     <td align="right">
                                        <strong>Bank Info:</strong>
                                    </td>
                                     <td align="left">
                                        <asp:TextBox ID="BankInfoTextBox" runat="server" Width="280px"   Height="36px"  CssClass= "auto-style3"  onkeypress= "fncInputNumericValuesOnly()" TextMode="MultiLine" ></asp:TextBox>
                                        <span class="star">* </span>
                                   </td>
                      
                     </tr>
                    <tr>
                        <td align="right"><strong>Cash Amount:</strong></td>
                        <td align="left">
                               <asp:TextBox ID="CashAmountTextBox" runat="server"  CssClass="TextInputStyleLarge" MaxLength="55"></asp:TextBox>
                         </td>
                 
                        <td align="right"><strong>Multiple Payment:</strong></td>
                        <td align="left">
                           <asp:TextBox ID="MultiplePayTypeTextBox" runat="server"   CssClass="TextInputStyleLarge" MaxLength="256" Width="280px"></asp:TextBox>
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
                <td colspan="4">       <asp:Button ID="PrintSaveButton" runat="server" AccessKey="p" 
                                        CssClass="buttoncommon" OnClientClick="return fnCheqInput();" 
                                        Text="Save and Print" />


                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="ResetButton" runat="server" AccessKey="a" 
                                        CssClass="buttoncommon" meta:resourcekey="ResetButtonResource2" 
                                        OnClientClick="return fnReset();" Text="Reset" />
                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="CloseButton" runat="server" AccessKey="c" 
                                        CssClass="buttoncommon" meta:resourcekey="CloseButtonResource1" 
                                        onclick="CloseButton_Click" Text="Close" />


                                    </td>        
            </tr>
            
         
    </table>
    
       </div>
      
     
    <br />
  </asp:Content> 
