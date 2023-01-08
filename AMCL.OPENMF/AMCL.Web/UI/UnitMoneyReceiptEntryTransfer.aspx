<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitMoneyReceiptEntryTransfer.aspx.cs" Inherits="UI_UnitMoneyReceiptEntryTransfer" Title=" Unit Money Receipt Entry Form (Design and Developed by Sakhawat)" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
    
        function  fnCheqInput1()
        {
           
            if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number First Holder ");
                return false;
                
            }
        }
    
        function fnCheqInput2() {

            if (document.getElementById("<%=RegNoTextBox_I.ClientID%>").value == "") {
                document.getElementById("<%=RegNoTextBox_I.ClientID%>").focus();
                alert("Please Enter Registration Number Second Holder");
                return false;

            }
              if (document.getElementById("<%=RegNoTextBox_I.ClientID%>").value == document.getElementById("<%=regNoTextBox.ClientID%>").value) {
                document.getElementById("<%=RegNoTextBox_I.ClientID%>").focus();
                alert("First Holder and  Second Holder can not be same");
                return false;

            }
        }
    
        function fncInputNumericValuesOnly()
        {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57))
            {
                alert("Please Enter Numaric Value Only");
                event.returnValue = false;
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
              Unit Holder Transfer Unit Receipt Entry Form (<span id="spanFundName" runat="server"></span>)
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
                    <td style=" background-color: #547DD3; height:25px" id="tdTR" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="TRHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryTransfer.aspx" Font-Bold="True" ForeColor="White">Transfer</asp:HyperLink>
                    </td >
                  <td style=" background-color: #CCCCFF; height:25px" id="tdRen" runat="server">
                        <span class="style3">|</span><asp:HyperLink ID="RNHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEntryRenewal.aspx" Font-Bold="True">Renewal</asp:HyperLink>
                    </td >
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
        <td align="right" ><b>Registration No :</b></td>
        <td align="left">
            <asp:DropDownList ID="fundCodeDDL" runat="server"  Width="80px" AutoPostBack="True" OnSelectedIndexChanged="fundCodeDDL_SelectedIndexChanged"  ></asp:DropDownList>
           <b>/</b> <asp:DropDownList ID="branchCodeDDL" runat="server" Width="80px" Enabled="False" ></asp:DropDownList>
            <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                <span class="star">*<asp:Button 
                            ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            onclick="findButton_Click" AccessKey="f" 
                            onclientclick="return fnCheqInput1();"
                            /></span></td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="120"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong> Receipt No:</strong></td>
                        <td > <asp:TextBox ID="ReceiptNoTextBox" runat="server"  CssClass= "TextInputStyleSmall"   
                                     Width="100px" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date: </b></td>
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
        <td align="right" ><b>BO :</b></td>
        <td align="left">
                        <asp:TextBox ID="holderBOTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="16" TabIndex="3"  onkeypress= "fncInputNumericValuesOnly()"
                           Width="137px"></asp:TextBox>
                        </td>  
             
        <td align="right"><b>Name&nbsp; of Holder:</b></td>
        <td align="left">
                  <span class="star">
            <asp:TextBox ID="NameTextBox" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" 
               ></asp:TextBox>
                                *</span></td>
    </tr>
    <tr>
        <td align="right" ><b>Address:</b></td>
        <td align="left" >
            
                                    <span class="star">
            
                        <asp:TextBox ID="addressTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine"></asp:TextBox>
                                    *</span></td>   
               <td align="right" ></td>
        <td align="left" >
            
                        &nbsp;</td>  
             
       
    </tr>
  <tr>
        <td align="right" ><b>Sale, Transfer, Renewal No(s)&nbsp; :</b></td>
        <td align="left">
            
                        <asp:TextBox ID="SaleTRRNNoTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                           ></asp:TextBox>
                                    </td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="120"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp; Unit Quantity:</strong></td>
                        <td > <asp:TextBox ID="QtyTextBox" runat="server"  CssClass= "TextInputStyleSmall"   
                                     Width="100px" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><strong>&nbsp;Delivery Date:</strong><asp:TextBox ID="DeliveryDateTextBox" runat="server" CssClass="textInputStyleDate"></asp:TextBox>
                             <ajaxToolkit:CalendarExtender ID="CalendarExtender4" runat="server" Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" TargetControlID="DeliveryDateTextBox" />
                             <asp:ImageButton ID="ImageButton3" runat="server" AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" />
                             <span class="star">*</span></td>
                        <td align="left">
                                                    &nbsp;</td>
                     </tr>
                      </table>           
                </td>
      
       
    </tr>

         <tr>
        <td align="right" ><b>Registration No(if Any) :</b></td>
        <td align="left">
            <asp:DropDownList ID="FundCodeDDL_I" runat="server"  Width="80px" ></asp:DropDownList>
           <b>/</b> <asp:DropDownList ID="BranchCodeDDL_I" runat="server" Width="80px" Enabled="False" ></asp:DropDownList>
            <b>/</b><asp:TextBox ID="RegNoTextBox_I" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                <span class="star">*<asp:Button 
                            ID="findButton_I" runat="server" Text="Find" CssClass="buttonmid" OnClick="findButton_I_Click" 
                         onclientclick="return fnCheqInput2();"
                            
                            /></span></td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp;</strong></td>
                        <td > &nbsp;</td>
                         <td align="right"><b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </b></td>
                        <td align="left">
                            &nbsp;</td>
                     </tr>
                      </table>             
                </td>
      
       
    </tr>
             <tr>
        <td align="right" ><b>BO :</b></td>
        <td align="left">
                        <asp:TextBox ID="HolderBOTextBox_I" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="16" TabIndex="3"  onkeypress= "fncInputNumericValuesOnly()"
                           Width="137px"></asp:TextBox>
                        </td>  
             
        <td align="right"><b>Name&nbsp; of Holder:</b></td>
        <td align="left">
                  <span class="star">
            <asp:TextBox ID="NameTextBox_I" runat="server" 
                CssClass= "TextInputStyleLarge" MaxLength="155" 
               ></asp:TextBox>
                                *</span></td>
    </tr>      
                     <tr>
        <td align="right" ><b>Address:</b></td>
        <td align="left" >
            
                                    <span class="star">
            
                        <asp:TextBox ID="addressTextBox_I" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine"></asp:TextBox>
                                    *</span></td>   
               <td align="right" >&nbsp;</td>
        <td align="left" >
            
                        &nbsp;</td>  
             
       
    </tr>
            </table>
        </fieldset>
        </td>
        
   
        
    </tr>  
            <tr>
                <td colspan="4">&nbsp;</td>        
            </tr>
               <tr align="center">
                <td colspan="4" align="center">       <asp:Button ID="PrintSaveButton" runat="server" AccessKey="p" 
                                        CssClass="buttoncommon" OnClientClick="return fnCheqInput();" 
                                        Text="Save and Print" OnClick="PrintSaveButton_Click" />


                                    &nbsp;&nbsp;&nbsp;
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
