<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportRepurchase.aspx.cs" Inherits="UI_UnitReportRepurchase" Title=" Sale Statement Report (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
   function fnReset()
   {
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=toRegNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=fromRepDateTextBox.ClientID%>").value ="";
        document.getElementById("<%=toRepDateTextBox.ClientID%>").value ="";
        document.getElementById("<%=fromRepNoTextBox.ClientID%>").value ="";
        document.getElementById("<%=toRepNoTextBox.ClientID%>").value ="";
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
         document.getElementById("<%=toRepNoTextBox.ClientID%>").value=document.getElementById("<%=fromRepNoTextBox.ClientID%>").value;                                                                           
          document.getElementById("<%=toRepNoTextBox.ClientID%>").focus();
     }
     function fnOnChangeText2()
     {                                
         document.getElementById("<%=toRegNoTextBox.ClientID%>").value=document.getElementById("<%=fromRegNoTextBox.ClientID%>").value;  
          document.getElementById("<%=toRegNoTextBox.ClientID%>").focus();                                                                         
     }
      function fnOnChangeText3()
     {                                
         document.getElementById("<%=toRepDateTextBox.ClientID%>").value=document.getElementById("<%=fromRepDateTextBox.ClientID%>").value;                                                                           
         document.getElementById("<%=toRepDateTextBox.ClientID%>").focus();    
     }
</script>



    <style type="text/css">
        .style7
        {
            height: 20px;
        }
        .auto-style2 {
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
           Unit Repurchase Statement Report Form (<span id="spanFundName" runat="server"></span>)
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
        <td align="left" class="style7">
            Repurchase No:</td>
        <td align="left" class="style7" >
        <asp:TextBox ID="fromRepNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText1();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRepNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="4" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <td align="left">
         Registration No:</td>
        <td align="left" >
        <asp:TextBox ID="fromRegNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText2();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="5" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRegNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="6" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td align="left">
            Repurchase&nbsp; Date:</td>
        <td align="left">
        <asp:TextBox ID="fromRepDateTextBox" runat="server" CssClass="textInputStyleDate" onBlur="Javascript:fnOnChangeText3();"
                TabIndex="6"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromRepDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="7" />
            <b><span style="font-weight:bold; height:100px;">&nbsp;&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRepDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="8"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="toRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="toRepDateTextBox" 
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
        <asp:Button ID="ShowReportButton" runat="server" Text="Details Repoert" CssClass="auto-style2"
                AccessKey="V" onclick="ShowReportButton_Click" Width="118px"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="SummaryReportButton" runat="server" Text="Summary Repoert" CssClass="auto-style2"
                AccessKey="0" Width="117px" OnClick="SummaryReportButton_Click"/>&nbsp; &nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
            <%--<asp:Button ID="ExportReportButton" runat="server" Text="Export File" CssClass="buttoncommon"
                 onclick="ExportReportButton_Click" />--%>&nbsp;&nbsp;&nbsp;
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

