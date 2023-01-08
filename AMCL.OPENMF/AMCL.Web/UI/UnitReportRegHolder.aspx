<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportRegHolder.aspx.cs" Inherits="UI_UnitReportRegHolder" Title=" Unit Holder Registration Report (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
   function fnReset()
   {
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=toRegNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=fromRegDateTextBox.ClientID%>").value ="";
        document.getElementById("<%=toRegDateTextBox.ClientID%>").value ="";
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
	    
	   if(textObject.id.indexOf("fromRegNoTextBox")!=-1)
	    {
	        document.getElementById("<%=toRegNoTextBox.ClientID%>").value =document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ;
	        document.getElementById("<%=toRegNoTextBox.ClientID%>").focus();
	    }
	     else  if(textObject.id.indexOf("fromRegDateImageButton")!=-1)
	    {
	        document.getElementById("<%=toRegDateTextBox.ClientID%>").value =document.getElementById("<%=fromRegDateTextBox.ClientID%>").value ;
	        document.getElementById("<%=toRegDateTextBox.ClientID%>").focus();
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
           Unit Holder Registration Statement Report Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <table width="500" align="center" cellpadding="0" cellspacing="0">
      <colgroup width="120"></colgroup>
      <tr>
        <td align="left">
         Fund Code:
        </td>
        <td align="left">
        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="80px" TabIndex="1"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td align="left">
         Branch Code:
        </td>
        <td align="left">
        <asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="80px" TabIndex="2"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <td align="left">
         Registration No:</td>
        <td align="left" >
        <asp:TextBox ID="fromRegNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText(this);"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRegNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="4" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td align="left">
         Registration Date:</td>
        <td align="left">
        <asp:TextBox ID="fromRegDateTextBox" runat="server" CssClass="textInputStyleDate" onBlur="Javascript:fnOnChangeText(this);"
                TabIndex="5"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" runat="server" TargetControlID="fromRegDateTextBox" PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" onBlur="Javascript:fnOnChangeText(this);"
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="3" />
            <b><span style="font-weight:bold; height:100px;">&nbsp;&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRegDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="7"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="toRegDateTextBox" 
                PopupButtonID="toRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="toRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="8" />
          
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
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" 
                  />
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

