<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportAfterClosingStatement.aspx.cs" Inherits="UI_UnitReportAfterClosingStatement" Title=" Report After Closing Statement(Design and Developed by Sakhawat) " %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
    
     
 function fnOnChangeText(textObject)
	{
	    
	    if(textObject.id.indexOf("fromBalanceTextBox")!=-1)
	    {
	        document.getElementById("<%=toBalanceTextBox.ClientID%>").value =document.getElementById("<%=fromBalanceTextBox.ClientID%>").value ;
	        document.getElementById("<%=toBalanceTextBox.ClientID%>").focus();
	    }
	    else  if(textObject.id.indexOf("fromRegNoTextBox")!=-1)
	    {
	        document.getElementById("<%=toRegNoTextBox.ClientID%>").value =document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ;
	        document.getElementById("<%=toRegNoTextBox.ClientID%>").focus();
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
//            if(document.getElementById("<%=branchNameDropDownList.ClientID%>").value =="0")
//            {
//                document.getElementById("<%=branchNameDropDownList.ClientID%>").focus();
//                alert("Please Select Branch Name");
//                return false;
//                
//            }
           if(document.getElementById("<%=DividendFYDropDownList.ClientID%>").value =="--Select FY--")
            {               
              
                document.getElementById("<%=DividendFYDropDownList.ClientID%>").focus();
                alert("Please Select Fiscal Year");
                return false;
               
            }
            if(document.getElementById("<%=ClosingDateDropDownList.ClientID%>").value =="")
            {                            
                document.getElementById("<%=ClosingDateDropDownList.ClientID%>").focus();
                alert("Please Select Closing Date");
                return false;
            }
            
//           if(document.getElementById("<%=IDRadioButton.ClientID%>").checked==true)
//            {
//                
//                 if(document.getElementById("<%=IDbankNameDropDownList.ClientID%>").value =="0")
//                 {
//                        document.getElementById("<%=IDbankNameDropDownList.ClientID%>").focus();
//                        alert("Please Select ID Institution");
//                        return false;
//                  }
//                 if(document.getElementById("<%=IDbranchNameDropDownList.ClientID%>").value =="0")
//                 {
//                        document.getElementById("<%=IDbranchNameDropDownList.ClientID%>").focus();
//                        alert("Please Select ID Institution Branch");
//                        return false;
//                  }
//             }
	    
	}
 </script>
    
    
    <style type="text/css">
        .style7
        {
            color: #FF3300;
        }
        .style8
        {
            height: 22px;
        }
        .style9
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
                Ledger Statement After Closing Print Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
  <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  
<table width="1000" align="center" cellpadding="0" cellspacing="0" >
<colgroup width="130"></colgroup>
<colgroup width="320"></colgroup>
<colgroup width="140"></colgroup>


  
    
     <tr >
        <td align="right" class="style9" ><b>Fund Name :</b></td>
        
        <td align="left" class="style9"> 
            
            <asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" 
                TabIndex="1">
            </asp:DropDownList>      
            <span class="style7">*</span></td>
             <td colspan="2"  align="left" class="style9">
                 <b>Gender</b><b> :</b>
                 <asp:RadioButton ID="AllGenderRadioButton" runat="server" Checked="True" 
                     GroupName="gender" Text="All" TabIndex="13" />
                 <asp:RadioButton ID="MaleRadioButton" runat="server" 
                     GroupName="gender" Text="Male" TabIndex="13" />
                 <asp:RadioButton ID="FemaleRadioButton" runat="server" GroupName="gender" 
                     Text="Female" TabIndex="13" />
                 
                 &nbsp;</td>
        
      
       
    </tr>
     <tr >
        <td align="right" class="style8" ><b>Branch Name :</b></td>
        
        <td align="left" class="style8" > 
            <asp:DropDownList 
                ID="branchNameDropDownList" runat="server" TabIndex="2">
            </asp:DropDownList>
            </span>
            </td>
      <td colspan="2" align="left"   >      
                 <b>Warrant Type:</b><asp:RadioButton ID="AllWarrantTypeRadioButton" 
               runat="server" Checked="True" GroupName="WarrantType" Text="All" />
           &nbsp;
           <asp:RadioButton ID="BEFTNRadioButton" runat="server" GroupName="WarrantType" 
               TabIndex="14" Text="BEFTN" />
           <asp:RadioButton ID="NONBEFTNRadioButton" runat="server" GroupName="WarrantType" 
               TabIndex="14" Text="NON-BEFTN" />
                 </td>
    </tr>
    <tr >
       <td align="right">
           <b>Fiscal Year : </b>
        </td>
        
        <td align="left">            
            </span>
            <asp:DropDownList ID="DividendFYDropDownList" runat="server" 
                AutoPostBack="True" 
                onselectedindexchanged="DividendFYDropDownList_SelectedIndexChanged" 
                TabIndex="3">
            </asp:DropDownList>
            <span class="style7">*</span></td>
             <td colspan="2"  align="left">
                 <b>Re-Investment Plan</b><b>:</b><asp:RadioButton ID="AllCIPRadioButton" 
                     runat="server" GroupName="cipPlan" Text="All" Checked="True" 
                     TabIndex="14" />
&nbsp;<asp:RadioButton ID="NonCIPRadioButton" runat="server" GroupName="cipPlan" 
                     Text="Non-CIP" TabIndex="14" />
                 <asp:RadioButton ID="CIPRadioButton" runat="server" GroupName="cipPlan" 
                     Text="CIP" TabIndex="14" />
                 
         </td>
         
      
    </tr>
     <tr >
       <td align="right" class="style9">
           <b>Closing Date : </b>
        </td>
        
        <td align="left" class="style9">
            <span >
            <asp:DropDownList ID="ClosingDateDropDownList" runat="server" TabIndex="4">
            </asp:DropDownList>
            </span><span class="style7">*</span></td>
         <td colspan="2"  align="left" class="style9">
              
                 
             &nbsp;</td>
         
      
    </tr>
     <tr>
       <td align="right">
           <b>Unit Balance Range: </b>
        </td>
        
        <td align="left">
           
            <asp:TextBox ID="fromBalanceTextBox" runat="server" onBlur="Javascript:fnOnChangeText(this);"
                CssClass="TextInputStyleSmall" onkeypress="fncInputNumericValuesOnly()" 
                TabIndex="5" Width="100px"></asp:TextBox>
            &nbsp; To&nbsp;
            <asp:TextBox ID="toBalanceTextBox" runat="server" CssClass="TextInputStyleSmall" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="6" Width="100px"></asp:TextBox>
           </td>
           
       <td colspan="2"  align="left" class="style9">
              
                 
             <b>Investor Type:</b><asp:RadioButton ID="AllIDRadioButton" runat="server" 
                 Checked="True" EnableTheming="False" GroupName="investorType" Text="All" 
                 TabIndex="15" oncheckedchanged="AllIDRadioButton_CheckedChanged" />
&nbsp;<asp:RadioButton ID="NonIDRadioButton" runat="server" 
                 GroupName="investorType" Text="Non-ID" AutoPostBack="True" 
                 oncheckedchanged="NonIDRadioButton_CheckedChanged" TabIndex="15" />
             <asp:RadioButton ID="IDRadioButton" runat="server" GroupName="investorType" 
                 Text="ID Account" AutoPostBack="True" 
                 oncheckedchanged="IDRadioButton_CheckedChanged" TabIndex="15" />
              
                 
         </td>
      
    </tr>
     <tr >
       <td align="right">
           <b>Registration No: </b>
        </td>
        
        <td align="left">
           
            <asp:TextBox ID="fromRegNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText(this);"
                CssClass="TextInputStyleSmall" onkeypress="fncInputNumericValuesOnly()" 
                TabIndex="7" Width="100px"></asp:TextBox>
            &nbsp; To&nbsp;
            <asp:TextBox ID="toRegNoTextBox" runat="server" CssClass="TextInputStyleSmall" 
                onkeypress="fncInputNumericValuesOnly()" TabIndex="8" Width="100px"></asp:TextBox>
           </td>
           
        <td align="right"> <b>ID Institution:</b></td>
        <td align="left"> 
            <asp:DropDownList ID="IDbankNameDropDownList" runat="server" 
                AutoPostBack="true" Enabled="False" 
                onselectedindexchanged="IDbankNameDropDownList_SelectedIndexChanged" 
                TabIndex="16">
            </asp:DropDownList>
         </td>
      
    </tr>
    <tr>
       <td align="right">
           <b>Unit Holder Name: </b>
        </td>
        
        <td align="left"> 
            </span>
            <asp:TextBox ID="holderNameTextBox" runat="server" 
                CssClass="TextInputStyleLarge" MaxLength="155" 
                meta:resourcekey="holderNameTextBoxResource1" TabIndex="9"></asp:TextBox>
            
           </td>
        
         <td align="right"> <b>ID Institution Branch:</b></td>
        <td align="left"> 
            <asp:DropDownList ID="IDbranchNameDropDownList" runat="server" Enabled="False" 
                TabIndex="16">
            </asp:DropDownList>
         </td>
      
    </tr>
    
    
  
    <tr>
        <td align="right"><b>Occupation:</b></td>
        <td align="left">
            <asp:DropDownList ID="holderOccupationDropDownList" runat="server" 
                meta:resourcekey="holderOccupationDropDownListResource1" TabIndex="10">
            </asp:DropDownList>
        </td>
        <td align="left">&nbsp;</td>
        <td align="left">&nbsp;</td>
        
        
        
    </tr>
    <tr>
        <td align="center" colspan="4" class="style12">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="4">&nbsp;</td>
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
                 AccessKey="p" onclick="PrintButton_Click" TabIndex="17" />&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" TabIndex="18" />&nbsp;&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" 
                meta:resourcekey="CloseButtonResource1" TabIndex="19" 
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

