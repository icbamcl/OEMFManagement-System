<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSIPReportStatementAfterSaleIssue.aspx.cs" Inherits="UI_UnitSIPReportStatementAfterSaleIssue" Title=" SIP Statement Report(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script runat="server">

    
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
   function fnReset()
   {
        document.getElementById("<%=fromSIPNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=toSIPNoTextBox.ClientID%>").value ="";
       
        
        return false;
   }
  
 function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	function fnOnChangeText1()
     {                                
         document.getElementById("<%=toSIPNoTextBox.ClientID%>").value=document.getElementById("<%=fromSIPNoTextBox.ClientID%>").value;                                                                           
          document.getElementById("<%=toSIPNoTextBox.ClientID%>").focus();
     }
    
    
     
</script>



    <style type="text/css">
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            height: 26px;
            text-align: right;
        }
        .auto-style4 {
            text-align: right;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
      
      <br />
      <table align="left" cellpadding="0" cellspacing="0" 
        style="width: 664px; height: 193px;">
      <colgroup width="120"></colgroup>
      <tr>
            <td class="FormTitle" align="center"  colspan="3">
                Unit SIP Statement After Sale Issue Report Form (<span id="spanFundName" runat="server"></span>)
            </td>           
           
        </tr> 
      <tr>
        <td align="left" colspan="2">
         &nbsp;&nbsp;
        </td>
        
      </tr>
      <tr>
        <td class="auto-style4">
            <strong>Fund Code:
        </strong>
        </td>
        <td align="left" colspan="2">
                        <asp:DropDownList ID="fundCodeDDL" runat="server"  ></asp:DropDownList>
        
            <span class="star">
                        *</span></td>
      </tr>
         
       <tr>
        <td class="auto-style4">
            <strong>SIP No:</strong></td>
        <td align="left"  >
        <asp:TextBox ID="fromSIPNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText1();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toSIPNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="4" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> 
                
                
        </td>
            <td>
           &nbsp;<%-- <table>
                    <tr>
                         <td align="left" class="style11" >Sale Type:</td>
            <td align="left" >
             <asp:DropDownList ID="saleTypeDropDownList" runat="server" 
                    CssClass="DropDownList" TabIndex="12" >
                 <asp:ListItem Value="0" Text=""> </asp:ListItem>
                <asp:ListItem Value="SL">SALE</asp:ListItem>
                <asp:ListItem Value="CIP" >CIP</asp:ListItem>
                 <asp:ListItem Value="SIP" >SIP</asp:ListItem>
                </asp:DropDownList>&nbsp;</td>
                    </tr>
             </table>--%></td>
      </tr>
          <%-- <tr>
        <td align="left">
         Registration No:</td>
        <td align="left" colspan="2">
        <asp:TextBox ID="fromRegNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText2();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="13" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRegNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="14" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
        </td>
      </tr>--%>
      <%--<tr>
        <td class="auto-style4">
            <strong>SIP Date:</strong></td>
        <td align="left" colspan="2">
        <asp:TextBox ID="fromSIPDateTextBox" runat="server" CssClass="textInputStyleDate" onBlur="Javascript:fnOnChangeText3();"
                TabIndex="15"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromSIPDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="7" />
            <b><span style="font-weight:bold; height:100px;">&nbsp;&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toSIPDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="16"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="toSIPDateTextBox" 
                PopupButtonID="toRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="toRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="9" />
          
        </td>
      </tr>--%>
            <tr>
        <td class="auto-style3">
            <strong>Report Type:
        </strong>
        </td>
        <td align="left" colspan="2" class="auto-style2">
            <asp:RadioButton ID="formSIPRadioButton" runat="server" Font-Bold="True" GroupName="sipHolder" Text="Form to Issue Units by SIP" Checked="True" />
                                                   
                                            
                                                   
            <asp:RadioButton ID="stateSIPRadioButton" runat="server" Font-Bold="True" GroupName="sipHolder" Text=" SIP Statement" />
                </td>
      </tr>
      <tr>
        <td align="left" colspan="2">
         &nbsp;&nbsp;
        </td>
        
      </tr>
      <tr>
        <td align="right">
        <asp:Button ID="ShowReportButton" runat="server" Text="Print Repoert" CssClass="buttoncommon" OnClick="ShowReportButton_Click"
                 />&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" 
                  />
        &nbsp;&nbsp;
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
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

