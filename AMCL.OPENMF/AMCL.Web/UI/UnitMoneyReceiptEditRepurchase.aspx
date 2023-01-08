<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitMoneyReceiptEditRepurchase.aspx.cs" Inherits="UI_UnitMoneyReceiptEditRepurchase" Title=" Unit Money Receipt Edit Form (Design and Developed by Sakhawat)" culture="auto" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
    function fnCheqInput()
    {
         if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number ");
                return false;
                
           }
            if(document.getElementById("<%=moneyReceipDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=moneyReceipDropDownList.ClientID%>").focus();
                alert("Please Select Receipt Number");
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
          if(document.getElementById("<%= SaleTRRNNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%= SaleTRRNNoTextBox.ClientID%>").focus();
                alert("Please Enter Sale /Transfer/Renewal Numbers ");
                return false;
                
          }
         if(document.getElementById("<%= PayDateTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%= PayDateTextBox.ClientID%>").focus();
                alert("Please Enter Payment Date ");
                return false;
                
           }
          
    }


        function  fnCheqFind()
        {
           
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number ");
                return false;
                
           }
          
          
        }
        function fncInputNumericValuesOnly()
        {
            if (!(event.keyCode == 48 || event.keyCode == 49 || event.keyCode == 50 || event.keyCode == 51 || event.keyCode == 52 || event.keyCode == 53 || event.keyCode == 54 || event.keyCode == 55 || event.keyCode == 56 || event.keyCode == 57)) {
                alert("Please Enter Numaric Value Only");
                event.returnValue = false;
            }
        }
    
        function fnConfirmDelete()
        {
            if(document.getElementById("<%=moneyReceipDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=moneyReceipDropDownList.ClientID%>").focus();
                alert("Please Select Receipt Number");
                return false;
                
            }

            var isConfirm = confirm("Are you sure to DELETE this sale record");
            if (isConfirm) {
                return true;
            }
            else {
                return false;
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
    
     .auto-style5 {
         text-align: center;
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
              Unit Holder Repurchase Unit Receipt Entry Form (<span id="spanFundName" runat="server"></span>)
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
            <%--<colgroup width="70"></colgroup>
            <colgroup width="70"></colgroup>--%>
                <tr>
                    <td align="left" style=" background-color: #CCCCFF; height:25px" id="tdSale" runat="server"> &nbsp; 
                       <asp:HyperLink ID="SaleHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptSaleEdit.aspx" Font-Bold="True" >  Unit Sale </asp:HyperLink>
                    </td>
                    <td style=" background-color: #547DD3; height:25px " id="tdRep" runat="server">
                  
                    <span class="style3">|</span><asp:HyperLink ID="RepHL" runat="server" 
                            NavigateUrl="~/UI/UnitMoneyReceiptEditRepurchase.aspx" Font-Bold="True" ForeColor="White">Repurchase</asp:HyperLink>
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
        <td align="right" ><b>Registration No :</b></td>
        <td align="left">
            <asp:DropDownList ID="fundCodeDDL" runat="server"  Width="80px" AutoPostBack="True" OnSelectedIndexChanged="fundCodeDDL_SelectedIndexChanged" ></asp:DropDownList>
           <b>/</b> <asp:DropDownList ID="branchCodeDDL" runat="server" Width="80px" Enabled="False" ></asp:DropDownList>
            <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                </td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp;&nbsp; Receipt No:</strong></td>
                        <td > 
                                                    <span class="star"> 
            <b><asp:DropDownList ID="moneyReceipDropDownList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="moneyReceipDropDownList_SelectedIndexChanged" >
            </asp:DropDownList>
            </b>
                                                    *</span></td>
                         <td align="right"><b>&nbsp;&nbsp;&nbsp;&nbsp; Date: </b></td>
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
            
                        <asp:TextBox ID="addressTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine"></asp:TextBox>
                                    <span class="star">*</span></td>   
                <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="150"></colgroup>
                  <colgroup width="120"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp; Unit Quantity:</strong></td>
                        <td > <asp:TextBox ID="QtyTextBox" runat="server"  CssClass= "TextInputStyleSmall"   
                                     Width="100px" ></asp:TextBox>
                                                    <span class="star">*</span></td>
                         <td align="right"><strong>&nbsp;Unit&nbsp; Rate:</strong></td>
                        <td align="left">
                                                    <asp:TextBox ID="RateTextBox" runat="server"   
                                    CssClass= "TextInputStyleSmall" 
                                    Width="95px" 
                                      ></asp:TextBox>
                                                    <span class="star">*</span></td>
                     </tr>
                      </table>             
                </td>
             
       
    </tr>
  <tr>
        <td align="right" ><b>Sale, Transfer, Renewal No(s)&nbsp; :</b></td>
        <td align="left">
            
                        <asp:TextBox ID="SaleTRRNNoTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" 
                             Font-Bold="False" ></asp:TextBox>
                                    <span class="star">*</span></td>
                 <td align="left" colspan="2">   
                 <table  align="left" cellpadding="0" cellspacing="0" border="0" >
                  <colgroup width="120"></colgroup>
                  <colgroup width="150"></colgroup>
                        <tr>
                        <td align="right"><strong>&nbsp;&nbsp;Payment Type:</strong></td>
                        <td > 
                            <asp:RadioButton ID="EFTRadioButton" runat="server" Checked="True" Font-Bold="True" GroupName="PayType" Text="BEFTN" />
 <asp:RadioButton ID="CHQRadioButton" runat="server" Font-Bold="True" GroupName="PayType" Text="CHEQUE" />
                            </td>
                         <td align="right"><b>Pay Date: </b></td>
                        <td align="left">
                            <asp:TextBox ID="PayDateTextBox" runat="server" CssClass="textInputStyleDate" 
                                ></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="ImageButton3" 
                                TargetControlID="PayDateTextBox" />
                            <asp:ImageButton ID="ImageButton3" runat="server" 
                                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                               />
                            <span class="star">*</span></td>
                     </tr>
                      </table>             
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
                <td colspan="4" class="auto-style5">       <asp:Button ID="PrintSaveButton" runat="server" AccessKey="p" 
                                        CssClass="buttoncommon" OnClientClick="return fnCheqInput();" 
                                        Text="Save and Print" OnClick="PrintSaveButton_Click" />


                                    &nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="DeleteButton" runat="server" AccessKey="p" 
                                        CssClass="auto-style6" OnClientClick="return fnConfirmDelete();" 
                                        Text="Delete" TabIndex="20" Width="110px" OnClick="DeleteButton_Click"  />


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
