<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="BankUpdateProcess.aspx.cs" Inherits="UI_BankUpdateProcess" Title=" Bank Information Update(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
            
	function  fnReset()
	{
	   	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	     if(document.getElementById("<%=bankNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=bankNameDropDownList.ClientID%>").focus();
                alert("Please Select Bank Name");
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
        .style12
        {
            height: 24px;
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
                Bank information update Form <span id="spanFundName" runat="server"></span>
                            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
  
    
<table width="700" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>

    
     <tr >
        <td class="style9" style="font-size: small"  ><b>&nbsp;Bank Name :</b></td>
        
        <td align="left"   colspan="4" class="style12"> &nbsp;&nbsp;          
            <asp:DropDownList ID="bankNameDropDownList" runat="server" 
                    TabIndex="1"></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     
        
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
            <asp:Button ID="Button3" runat="server" onclick="Button3_Click" 
                Text="Migrate Dividend " />
            <asp:Button ID="processButton" runat="server" AccessKey="f" CssClass="buttonmid"                  
                onclientclick="return fnCheqInput();" TabIndex="10" Text="Run Process" 
                Width="128px" onclick="processButton_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                ID="Button1" runat="server" onclick="Button1_Click" Text="SSLIST" />
        &nbsp;<asp:Button ID="Button2" runat="server" onclick="Button2_Click" 
                Text="ImageRename" />
            <asp:Button ID="Button4" runat="server" OnClick="Button4_Click" Text="DpwnloadExecl" />
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
    <br />
</asp:Content>

