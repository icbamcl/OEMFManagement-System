<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportIntimation.aspx.cs" Inherits="UI_UnitReportIntimation" Title=" Tax Intimation Letter Report Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
     

    
    function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
		}
	}
	
	function  fnReset()
	{
	   
	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	 
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
        }
    .style7
    {
        font-size: small;
    }
        .style11
        {
            border: solid 1px #A8ACAF;
            text-align: right;
        }
    </style>
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    
<br />
<br />
 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Print Dividend Intimation Letter Report&nbsp; Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
    <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>
<table align="center" cellpadding="0" cellspacing="0" style="width: 622px" >
<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>

    <tr   class="style5">
        <td class="style11"><b>
            <span class="style7">Fund Name :</span></b></td>
        <td align="left"  class="style6" colspan="4">
            &nbsp;<asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" TabIndex="1">
            </asp:DropDownList>
            <span class="star">*&nbsp;&nbsp;&nbsp; </span></td>
        
    </tr>
      <tr   class="style5">
        <td class="style11"><b>
            <span class="style7">Fiscal Year :</span></b></td>
        <td align="left"  class="style6" colspan="4">
            &nbsp;<asp:DropDownList ID="DividendFYDropDownList" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="DividendFYDropDownList_SelectedIndexChanged" 
                TabIndex="3" style="height: 22px">
            </asp:DropDownList>
            <span class="star">*</span></td>
        
    </tr>
      <tr   class="style5">
        <td class="style11"><b>
            <span class="style7"><b><span class="style7">Closing Date </span></b>:</span></b></td>
        <td align="left"  class="style6" colspan="4">
            &nbsp;<span class="star"><span><asp:DropDownList ID="ClosingDateDropDownList" 
                runat="server" TabIndex="4">
            </asp:DropDownList>
            </span>*</span></td>
        
    </tr>
    <tr   class="style5">
        <td class="style11"><b>
            <span class="style7"><b><span class="style7">Fy Part </span></b>:</span></b></td>
        <td align="left"  class="style6" colspan="4">
            &nbsp;<asp:DropDownList ID="fyPartDropDownList" runat="server" TabIndex="4">
            </asp:DropDownList>
            <span class="star">*</span></td>
        
    </tr>
    <tr   class="style5">
        <td class="style11"><b>
            <span class="style7"><b><span class="style7">Warrant Number </span></b>:</span></b></td>
        <td align="left"  class="style6" colspan="4">
            <asp:TextBox ID="fromWar_NoTextBox" runat="server" 
                CssClass="TextInputStyleSmall" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="5" Width="100px"></asp:TextBox>
            &nbsp;<b>TO
            <asp:TextBox ID="toWar_NoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="6" Width="100px"></asp:TextBox>
            </b></td>
        
    </tr>
 <tr   class="style5">
        <td class="style11"><b>
            <span class="style7"><b><span class="style7">BEFTN Issue Date </span></b>:</span></b></td>
        <td align="left"  class="style6" colspan="4">
           <asp:TextBox ID="BEFTNIssueDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="6" ></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                    TargetControlID="BEFTNIssueDateTextBox" PopupButtonID="chequeDateImageButton" 
                    Format="dd-MMM-yyyy" Enabled="True"/>
                <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" />
          
          <span class="star">*</span></td>
        
    </tr>
    
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
</table>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="fundNameDropDownList" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="DividendFYDropDownList" EventName="SelectedIndexChanged" />
</Triggers>
</asp:UpdatePanel>
<table align="center" cellpadding="0" cellspacing="0" style="width: 338px">
     <tr>
        <td align="right">
        <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="p" onclick="PrintButton_Click" />&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
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
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>

