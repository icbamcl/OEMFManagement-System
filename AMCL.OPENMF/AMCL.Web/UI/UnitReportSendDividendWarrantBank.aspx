<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportSendDividendWarrantBank.aspx.cs" Inherits="UI_UnitReportSendDividendWarrantBank" Title=" Unit Report Form for Sending Dividend Warrant to Bank (Design and Developed by Sakhawat)" %>
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
	 
	     if(document.getElementById("<%=fundNameDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=fundNameDropDownList.ClientID%>").focus();
                alert("Please Select Fund Name");
                return false;
                
            }
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
          
	    
	}
 </script>
   <style type="text/css">
       
        .style6
        {
         border:solid 1px #A8ACAF;
        text-align: left;
    }
        .style8
        {
            font-size: small;
            font-weight: bold;
            text-align: right;
        }
      
        .style10
        {
            font-size: small;
            font-weight: bold;
        }
       </style>
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    

 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Sending Dividend Warrant To Bank Report Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
<br />
    <br />
  <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  
<table width="600" align="center" cellpadding="0" cellspacing="0" >

   <%-- <tr   class="style5">
        <td align="right" ><b style="font-size: small; text-align: right;">&nbsp;&nbsp;Dividend Category :</b></td>
        <td align="left"   colspan="4">
            &nbsp;
            <asp:RadioButton ID="NormalRadioButton" runat="server" Checked="True" 
                GroupName="DividendCategory" style="font-size: small; font-weight: 700;" 
                Text="Normal" TabIndex="1" /> &nbsp;&nbsp;
            <asp:RadioButton ID="IDAccountRadioButton" runat="server" Font-Bold="True" 
                style="font-size: small" Text="ID Account"  GroupName="DividendCategory" 
                TabIndex="1" />
        </td>
        
    </tr>--%>
     <%--<tr   class="style5">
        <td c><b style="font-size: small; text-align: right;">&nbsp;&nbsp;Dividend Type :</b></td>
        <td align="left"   colspan="4">
            &nbsp;
            <asp:CheckBox ID="DuplicateCheckBox" runat="server"  Text="Duplicate" 
                style="font-weight: 700; font-size: small" TabIndex="1"/>
            &nbsp;&nbsp;
            
        </td>
        
    </tr>--%>
     <tr >
        <td style="font-size: small" align="right" ><b>Fund Name :</b></td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="2" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged"></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
        <td align="left" class="style10" style="text-align: right"  >Branch Name :</td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="branchNameDropDownList" runat="server" 
                    TabIndex="3"></asp:DropDownList>
        </td>
       
    </tr>
    <tr class="style5">
       <td class="style8"  >
           Fiscal Year :
        </td>
        
        <td align="left" colspan="4">&nbsp;
            <asp:DropDownList ID="DividendFYDropDownList" runat="server" 
                    TabIndex="4" AutoPostBack="True" 
                onselectedindexchanged="DividendFYDropDownList_SelectedIndexChanged"></asp:DropDownList>
            <span class="star">*</span></td>
         
      
    </tr>
     <tr class="style5">
       <td class="style8"  >
           Closing Date :
        </td>
        
        <td align="left" colspan="4">&nbsp;
            <asp:DropDownList ID="ClosingDateDropDownList" runat="server" 
                    TabIndex="5"></asp:DropDownList><span class="star">*</span></td>
        
      
    </tr>
     <tr class="style5">
       <td class="style8"  >
          Warrant No :
        </td>
        
        <td align="left" colspan="4">&nbsp;
        <asp:TextBox ID="War_NoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="221px" TabIndex="6" 
                ></asp:TextBox> &nbsp;</td>
        
      
    </tr>
     <tr class="style5">
       <td class="style8"  >
         Registration No :
        </td>
        
        <td align="left" colspan="4">&nbsp;
        <asp:TextBox ID="RegNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="221px" TabIndex="9" 
                 Height="17px"></asp:TextBox> &nbsp;</td>
        
      
    </tr>
    <%--<tr class="style5">
       <td class="style12"  >
           Enter Separator:
        </td>
        
        <td align="left" colspan="4" class="style13">&nbsp;
                
        <asp:TextBox ID="SeparatorTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="5" 
                onkeypress= "fncInputNumericValuesOnly()">~</asp:TextBox> &nbsp;<span 
                class="style11">[</span><span class="style14">Recommandation Value</span><span 
                class="style11"> </span><span class="style15">~</span><span class="style11">]</span></td>
        
      
    </tr>--%>
    
    <tr >
        <td style="font-size: small" align="right" ><b>Select Bank  Name :</b></td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="bankNameDropDownList" runat="server" 
                    TabIndex="2" AutoPostBack="True" 
                onselectedindexchanged="bankNameDropDownList_SelectedIndexChanged"></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
        <td align="left" class="style10" style="text-align: right"  >Branch Name :</td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="bankbranchNameDropDownList" runat="server"   TabIndex="3"></asp:DropDownList>
        </td>
       
    </tr>
      <tr>
                              <td align="left" class="style10" style="text-align: right"  >
                                  Name of The Signatory  :</td>
                              <td align="left">
                                  &nbsp;&nbsp;&nbsp;
                                  <asp:DropDownList ID="SignatoryDropDownList" runat="server">
                                  </asp:DropDownList>
                                  <span class="star">* </span></td>
                          </tr>
                            <tr>
                              <td align="left" class="style10" style="text-align: right"  >
                                 Designation of The Signatory  :</td>
                              <td align="left">
                                  &nbsp;&nbsp;&nbsp;
                                  <asp:DropDownList ID="DesignationDropDownList" runat="server">
                                  <asp:ListItem Value="0" Selected="True">Chief Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="1" >Deputy Chief Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="2" >Senior Principal Officer</asp:ListItem>
                                  <asp:ListItem Value="3" >Senior Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="4" >System Analyst</asp:ListItem>
                                  <asp:ListItem Value="5" >Principal Officer</asp:ListItem>
                                  <asp:ListItem Value="6" >Executive Officer</asp:ListItem>
                                  <asp:ListItem Value="7" >Programmer</asp:ListItem>
                                  </asp:DropDownList>
                                  <span class="star">*</span></td>
                          </tr>
  
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
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
        <asp:Button ID="PrintButton" runat="server" Text="Print" 
                CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="p" onclick="PrintButton_Click" TabIndex="10" />&nbsp;
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

