<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitBookCloserBalanceProcess.aspx.cs" Inherits="UI_UnitBookCloserBalanceProcess" Title=" Unit Fund Book closer balance process (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
            
	
	function fnCheqInput()
	{

    
                  
	            if (document.getElementById("<%=asOnDateTextBox.ClientID%>").value =="")
	            {
                      document.getElementById("<%=asOnDateTextBox.ClientID%>").focus();
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
        .auto-style3 {
            color: #006600;
        }
        .auto-style4 {
            height: 13px;
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
                Unit Fund Book Closer Balance Process Form <span id="spanFundName" runat="server"></span>
                            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
  
    
<table width="700" align="center" cellpadding="0" cellspacing="0" >


    
     <tr >
        <td  style="font-size: small" align="right"  ><b>Fund Name :</b></td>
        
        <td align="left"  >           
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="1"></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
       <td  style="font-size: small"   align="right"><b>Branch Name :</b></td>
        
        <td align="left"   >           
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="2"></asp:DropDownList>
            </td>
       
    </tr>
   
    
    <tr >
         <td  style="font-size: small"   align="right"><b>Book Closer Date :</b></td>
        
        <td align="left"  > 
            <asp:TextBox ID="asOnDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="3"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="asOnDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="4" />
            <b><span style="font-weight:bold; height:100px;"><span class="star">*</span></span></b></td>
       
    </tr>
  
    <tr>
        <td align="center" colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2" class="auto-style4"><strong>
            <asp:Label ID="ProcssResultLabel" runat="server" CssClass="auto-style3" Text="Process Result"></asp:Label>
            </strong></td>
    </tr>
     <tr>
        <td align="center" colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <asp:Button ID="startProcessButton" runat="server"  CssClass="auto-style2"                
                onclientclick="return fnCheqInput();"  Text="Start Process" onclick="startProcessButton_Click"
                Width="171px" Height="24px"  />
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

