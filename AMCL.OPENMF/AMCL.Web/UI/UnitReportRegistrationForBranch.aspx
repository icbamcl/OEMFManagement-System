<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportRegistrationForBranch.aspx.cs" Inherits="UI_UnitReportRegistrationForBranch" Title=" Report After Closing Statement(Design and Developed by Sakhawat) " %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
     

	function fnCheqInput()
	{
	 
	     if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
                
            }
//           
	}
 </script>
    
    
    <style type="text/css">
        .style7
        {
            color: #FF3300;
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
                Registration Statement for Branch Print Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
  <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  
<table align="center" cellpadding="0" cellspacing="0" style="width: 561px" >
<colgroup width="130"></colgroup>
<colgroup width="320"></colgroup>



  
    
     <tr >
        <td align="right" class="style9" ><b>Fund Name :</b></td>
        
        <td align="left" class="style9"> 
            
            <asp:DropDownList ID="fundNameDropDownList" runat="server"  
               
                TabIndex="1">
            </asp:DropDownList>      
            <span class="style7">*</span></td>
        
      
       
    </tr>
     <tr >
        <td align="right" class="style8" ><b>Branch Name :</b></td>
        
        <td align="left" class="style8" > 
            <asp:DropDownList 
                ID="branchNameDropDownList" runat="server" TabIndex="2">
            </asp:DropDownList>
            </span>
            </td>
    </tr>
    <tr>
        <td align="center" colspan="2">&nbsp;</td>
    </tr>
</table>
</ContentTemplate>
<Triggers>
    
</Triggers>
</asp:UpdatePanel>
<table align="center" cellpadding="0" cellspacing="0" style="width: 338px">
     <tr>
        <td align="right" style="text-align: center">
        <asp:Button ID="PrintButton" runat="server" Text="Print" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="p" onclick="PrintButton_Click" TabIndex="17" />&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
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

