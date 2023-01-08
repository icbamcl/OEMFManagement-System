<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitCertificateRecon.aspx.cs" Inherits="UI_UnitCertificateRecon" Title=" Unit Fund Certificate Reconsilation (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
            
	function  fnReset()
	{
	   	    
	     return false;
	}
	function fnCheqInput()
	{
	 
	     if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
                
            }   
             
                               	    
	}
	function fncInputNumericValuesOnly()
	{
		if(!(event.keyCode==48||event.keyCode==49||event.keyCode==50||event.keyCode==51||event.keyCode==52||event.keyCode==53||event.keyCode==54||event.keyCode==55||event.keyCode==56||event.keyCode==57))
		{
			alert("Please Enter Numaric Value Only");
			    event.returnValue=false;
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
                Unit Fund Certificate Reconsilation Form <span id="spanFundName" runat="server"></span>
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
        <td class="style9" style="font-size: small"  ><b>Fund Name :</b></td>
        
        <td align="left"   colspan="4" class="style12"> &nbsp;&nbsp;          
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1"></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
        <td align="left" class="style10" style="text-align: right"  >Branch Name :</td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2"></asp:DropDownList>
        </td>
       
    </tr>
   
    
    <tr >
        <td align="left" class="style10" style="text-align: right"  >Select Certificate :</td>
        
        <td align="left"   colspan="4"> 
            &nbsp; &nbsp;<asp:DropDownList ID="certNoDropDownList" runat="server" 
                CssClass="DropDownList"  TabIndex="3" 
                Width="75px">
                <asp:ListItem Value="0">--select--</asp:ListItem>
                <asp:ListItem Value="A">A</asp:ListItem>
                <asp:ListItem Value="B">B</asp:ListItem>
                <asp:ListItem Value="C">C</asp:ListItem>
                <asp:ListItem Value="D">D</asp:ListItem>
                <asp:ListItem Value="E">E</asp:ListItem>
                <asp:ListItem Value="F">F</asp:ListItem>
                <asp:ListItem Value="G">G</asp:ListItem>
                <asp:ListItem Value="H">H</asp:ListItem>
                <asp:ListItem Value="I">I</asp:ListItem>
                <asp:ListItem Value="J">J</asp:ListItem>
                <asp:ListItem Value="K">K</asp:ListItem>
            </asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
  
    <tr>
    <td align="left" class="style10" style="text-align: right"  >Certificate Book No. :</td>
        <td align="left" colspan="4">&nbsp;&nbsp;
            <asp:TextBox ID="fromBookNoTextBox" runat="server" 
                CssClass="TextInputStyleSmall" onkeypress="fncInputNumericValuesOnly()" 
                TabIndex="4" Width="100px"></asp:TextBox>
            &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="toBookNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="5" Width="100px"></asp:TextBox>
        </td>
    </tr>
     <tr>
    <td align="left" class="style10" style="text-align: right"  >Certificate  No. :</td>
        <td align="left" colspan="4">&nbsp;&nbsp;
            <asp:TextBox ID="fromCertNoTextBox" runat="server" 
                CssClass="TextInputStyleSmall" onkeypress="fncInputNumericValuesOnly()" 
                TabIndex="6" Width="100px"></asp:TextBox>
            &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="toCertNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="7" Width="100px" 
                EnableTheming="True"></asp:TextBox>
        </td>
    </tr>
    <tr>
    <td align="left" class="style10" style="text-align: right"  >Process Type. :</td>
        <td align="left" colspan="4">&nbsp;
            <asp:RadioButton ID="unUsedCertRadioButton" runat="server" AutoPostBack="True" 
                Checked="True" GroupName="processType" 
               TabIndex="9" 
                Text="Un-Used Certificate" />
            <asp:RadioButton ID="CertAllocationRadioButton" runat="server" 
                AutoPostBack="True" GroupName="processType" 
                 TabIndex="10" 
                Text="Certificate Allocation" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
            <asp:Button ID="processButton" runat="server" AccessKey="f" CssClass="buttonmid"                  
                onclientclick="return fnCheqInput();" TabIndex="10" Text="Run Process" 
                Width="128px" onclick="processButton_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
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

