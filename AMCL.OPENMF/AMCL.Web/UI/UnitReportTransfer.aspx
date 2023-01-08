<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportTransfer.aspx.cs" Inherits="UI_UnitReportTransfer" Title=" Transfer Statement Report (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
   function fnReset()
   {
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=toRegNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=fromTrDateTextBox.ClientID%>").value ="";
        document.getElementById("<%=toTrDateTextBox.ClientID%>").value ="";
        document.getElementById("<%=fromTrNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=toTrNoTextBox.ClientID%>").value ="";
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
	
	function fnOnChangeText(textObject)
	{
	    
	    if(textObject.id.indexOf("fromTrNoTextBox")!=-1)
	    {
	        document.getElementById("<%=toTrNoTextBox.ClientID%>").value =document.getElementById("<%=fromTrNoTextBox.ClientID%>").value ;
	        document.getElementById("<%=toTrNoTextBox.ClientID%>").focus();
	    }
	    else  if(textObject.id.indexOf("fromRegNoTextBox")!=-1)
	    {
	        document.getElementById("<%=toRegNoTextBox.ClientID%>").value =document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ;
	        document.getElementById("<%=toRegNoTextBox.ClientID%>").focus();
	    }
	     else  if(textObject.id.indexOf("fromRegDateImageButton")!=-1)
	    {
	        document.getElementById("<%=toTrDateTextBox.ClientID%>").value =document.getElementById("<%=fromTrDateTextBox.ClientID%>").value ;
	        document.getElementById("<%=toTrDateTextBox.ClientID%>").focus();
	    }
	}
</script>



    <style type="text/css">
        .auto-style2 {
            text-align: right;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Transfer Statement Report Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <table width="600" align="center" cellpadding="0" cellspacing="0">
      <colgroup width="200"></colgroup>
      <tr>
        <td class="auto-style2">
            <strong>Fund Code/ Branch Code :</strong></td>
        <td align="left">
        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="80px" TabIndex="1"></asp:TextBox>
            /<asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="80px" TabIndex="2"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="auto-style2">
       
            <strong>Report Type :</strong></td>
        <td align="left">
            <strong><asp:RadioButton ID="TransferRadioButton" 
               runat="server" Checked="True" GroupName="TrType" Text="Transfer" />
            </strong>&nbsp;
           <strong>
           <asp:RadioButton ID="TrDeathRadioButton" runat="server" GroupName="TrType" 
               TabIndex="14" Text="Death Transmission" />
            </strong></td>
      </tr>
       <tr>
        <td class="auto-style2">
            <strong>Transfer/Transmission No :</strong></td>
        <td align="left" >
        <asp:TextBox ID="fromTrNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText(this);"
                CssClass= "TextInputStyleSmall" Width="100px"  onkeypress= "fncInputNumericValuesOnly()" TabIndex="3"></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp;To</span></b>&nbsp;
                <asp:TextBox ID="toTrNoTextBox" runat="server"  onkeypress= "fncInputNumericValuesOnly()"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="4"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <td class="auto-style2">
            <strong>Registration No :</strong></td>
        <td align="left" >
        <asp:TextBox ID="fromRegNoTextBox" runat="server"  onkeypress= "fncInputNumericValuesOnly()" onBlur="Javascript:fnOnChangeText(this);"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="5"></asp:TextBox> &nbsp; <b><span style="font-weight:bold; height:100px;">To</span></b>&nbsp;
                <asp:TextBox ID="toRegNoTextBox" runat="server"  onkeypress= "fncInputNumericValuesOnly()"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="6"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td class="auto-style2">
            <strong>Transfer/Transmission Date :</strong></td>
        <td align="left">
        <asp:TextBox ID="fromTrDateTextBox" runat="server" CssClass="textInputStyleDate" onBlur="Javascript:fnOnChangeText(this);"
                TabIndex="6"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromTrDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" onBlur="Javascript:fnOnChangeText(this);"
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="7" />
            <b><span style="font-weight:bold; height:100px;">&nbsp;To </span></b>&nbsp;<asp:TextBox ID="toTrDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="8"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="toTrDateTextBox" 
                PopupButtonID="toRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="toRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="9" />
          
        </td>
      </tr>
      </table>
      
     
    <br />
    <br />
    <table width="500" align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="right">
        <asp:Button ID="ShowReportButton" runat="server" Text="View Repoert" CssClass="buttoncommon"
                AccessKey="V" onclick="ShowReportButton_Click"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;<asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" 
                  />
        &nbsp;
            <asp:Button ID="ExportReportButton" runat="server" Text="Export File" CssClass="buttoncommon"
                AccessKey="E" onclick="ExportReportButton_Click" />
        </td>
        <td>
        &nbsp;
            <br />
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
</asp:Content>

