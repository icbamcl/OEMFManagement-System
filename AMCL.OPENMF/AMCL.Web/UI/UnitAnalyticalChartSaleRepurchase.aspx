<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitAnalyticalChartSaleRepurchase.aspx.cs" Inherits="UI_UnitAnalyticalChartSaleRepurchase" Title=" Unit Fund Analytica Chart Sale Repurchase (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
            
	
	function fnCheqInput()
	{

    
                  
	            if (document.getElementById("<%=TransactionDatefromTextBox.ClientID%>").value =="")
	            {
                      document.getElementById("<%=TransactionDatefromTextBox.ClientID%>").focus();
                        alert("Please Enter Book Closer Date");
                        return false;
	            }
	 	    
	        var isConfirm = confirm("Are you sure to Start Balance Process");
	        if (!isConfirm)
	        {                 
	            return false;
	        }
	       
	  
           	    
	}
	
	
	
 </script>
    <style type="text/css">
        .style5
        {
            height: 30px;
        }
        .style6
        {
         border:solid 1px #A8ACAF;
        text-align: left;
    }
        .style9
        {
            text-align: right;
            height: 24px;
        }
        .style10
        {
            font-size: small;
            font-weight: bold;
        }
        .style11
        {
            color: #660033;
        }
        .style12
        {
            height: 24px;
        }
        .auto-style1 {
            text-align: right;
        }
        .auto-style4 {
            height: 13px;
        }
        .auto-style5 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            COLOR: #000000;
            FONT-SIZE: 11px;
        }
        .auto-style6 {
            height: 20px;
        }
        .auto-style3 {
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
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
<table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Sale Repurchase Chart Report Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
 <table width="1100px" align="center" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="150"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
 
     <tr>
        <td colspan="4" align="left">
        <fieldset>
            <legend style="font-weight: 700"> ::Report Parameter ::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
             <tr>
        <td align="right" ><b>Fund Name:</b></td>
        <td align="left">
           <asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" >
            </asp:DropDownList>
                 </td>
        <td align="right"><b>Branch Name:</b></td>
        <td align="left">
          &nbsp;<asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2"></asp:DropDownList>
                 </td> 
       
    </tr>
    <tr>
       <td align="right" class="auto-style6"><b>Transaction Date:</b></td>
        <td align="left" class="auto-style6">
            <asp:TextBox ID="TransactionDatefromTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="TransactionDatefromTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="4" />
            <span style="font-weight:bold; height:100px;"><b><span class="star">&nbsp; <span class="auto-style5">TO</span><span class="star"> 
            <asp:TextBox ID="transactonDatetoTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="transactonDatetoTextBox_CalendarExtender" 
                runat="server" TargetControlID="transactonDatetoTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton0" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="4" />
            </span></span></b></td>  
             
        <td align="right" class="auto-style6"><b>Transaction Type:</b></td>
        <td align="left"" class="auto-style6">
                &nbsp;<strong><asp:RadioButton ID="saleRadioButton" runat="server" Text="Sale" Checked="True" GroupName="tranType" />
                <asp:RadioButton ID="repRadioButton" runat="server" Text="Repurchase" GroupName="tranType" />
                <asp:RadioButton ID="bothRadioButton" runat="server" Text="Both" GroupName="tranType" />
                </strong>&nbsp;</td>
    </tr>
     <tr>
        <td align="right" class="auto-style4" ><b>Transaction No: </b></td>
        <td align="left" class="auto-style4" >
            
        <asp:TextBox ID="fromTranNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText1();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> <span style="font-weight:bold; height:100px;"><b><span class="star"> <span class="auto-style5">&nbsp;TO</span></span></b></span>
                <asp:TextBox ID="totranNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="4" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> 
                
                
         </td>   
                <td align="right" class="auto-style4"> <b>&nbsp;Rate:</b></td>        
                
                 <td align="left" class="auto-style4"> 
        <asp:TextBox ID="fromRateTextBox" runat="server" onBlur="Javascript:fnOnChangeText1();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<span style="font-weight:bold; height:100px;"><b><span class="star"><span class="auto-style5">TO</span></span></b></span>
                <asp:TextBox ID="toRateNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="4" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> 
                
                
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
    <td align="center" colspan="6">

        <asp:Button ID="findButton" runat="server" Text="Find Data" CssClass="auto-style3" 
                  Width="140px" Height="22px"  OnClientClick="return fnCheqInput();" OnClick="findButton_Click"
                />
        &nbsp;
        &nbsp;
        
    </td>
</tr>   
    <tr>
        <td colspan="4">
          <fieldset>
            <legend style="font-weight: 700"> ::Report Type ::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
              <tr>
      
        
        <td align="right" >
            &nbsp;</td>
       <td align="left">
           &nbsp;</td>
                   <td align="right" >
                       &nbsp;</td>
        <td align="left">
            &nbsp;&nbsp;</td></tr>
            
   
    
   
               
     
            </table>
            </fieldset>
        </td>
        
    </tr>
  <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
  
      <tr>
            <td align="center" colspan="6">
            
                <table align="left">
                    <tr>
                        <td align="center">
                         <div id="dvGrid" runat="server"  >
                         
                                <asp:GridView ID="SaleRepTrListGridView" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                                         
                                <asp:BoundField DataField="REG_BK" HeaderText="Fund Code" />
                                <asp:BoundField DataField="SL_UNIT" HeaderText=" Sale Unit" />
                                <asp:BoundField DataField="SL_AMOUNT" HeaderText=" Sale Amount" />  
                                <asp:BoundField DataField="REP_UNIT" HeaderText=" Repurchase UNIT" />        
                                <asp:BoundField DataField="REP_AMOUNT" HeaderText=" Repurchase Amount" />   
                                <asp:BoundField DataField="NET_UNIT" HeaderText=" Net Unit" /> 
                                <asp:BoundField DataField="NET_AMOUNT" HeaderText=" Net Amount" />                                                                                                                                                                                                                                                     
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                   
                </table>
            
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

