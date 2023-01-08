<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitCertPrint.aspx.cs" Inherits="UI_UnitCertPrint" Title=" Unit Certificate Print Form (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
    
     
    function fnEnable(Action)
        {
       
            if(Action.indexOf("SaleNoRadioButton")!=-1)
            {   
                if( document.getElementById("<%=SaleNoRadioButton.ClientID%>").checked)
                {                   
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=RenNoTextBox.ClientID%>").value="";    
                    document.getElementById("<%=TrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=LineNoTextBox.ClientID%>").value="";     
                    document.getElementById("<%=RenNoTextBox.ClientID%>").disabled=true;    
                    document.getElementById("<%=TrNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=LineNoTextBox.ClientID%>").disabled=true;                                            
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {   
                    document.getElementById("<%=LineNoTextBox.ClientID%>").value="";     
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").disabled=true;    
                                                                             
                }
              }
              else if(Action.indexOf("RenNoRadioButton")!=-1)
              {
                if(document.getElementById("<%=RenNoRadioButton.ClientID%>").checked)
                {                   
                    document.getElementById("<%=RenNoTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").value="";    
                    document.getElementById("<%=TrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=LineNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").disabled=true;      
                    document.getElementById("<%=TrNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=LineNoTextBox.ClientID%>").disabled=true;                                           
                    document.getElementById("<%=RenNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {     
                    document.getElementById("<%=RenNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=RenNoTextBox.ClientID%>").disabled=true;   
                                                                              
                }
              }
              else if(Action.indexOf("TrNoRadioButton")!=-1)
              {
                 if(document.getElementById("<%=TrNoRadioButton.ClientID%>").checked)
                {                   
                    document.getElementById("<%=TrNoTextBox.ClientID%>").disabled=false; 
                    document.getElementById("<%=LineNoTextBox.ClientID%>").disabled=false;  
                    document.getElementById("<%=RenNoTextBox.ClientID%>").value="";   
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").value="";
                    document.getElementById("<%=RenNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").disabled=true;                                                  
                    document.getElementById("<%=TrNoTextBox.ClientID%>").focus();               
                             
                }
                else
                {   document.getElementById("<%=LineNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=TrNoTextBox.ClientID%>").value=""; 
                    document.getElementById("<%=TrNoTextBox.ClientID%>").disabled=true;  
                    document.getElementById("<%=LineNoTextBox.ClientID%>").disabled=true;                                                                   
                }
              }
         }
         
    function fnCheck()
    {
         if( document.getElementById("<%=SaleNoRadioButton.ClientID%>").checked)
            {                   
                if(document.getElementById("<%=SaleNoTextBox.ClientID%>").value=="")
                {
                    alert("Please Enter Sale Number");
                    document.getElementById("<%=SaleNoTextBox.ClientID%>").focus(); 
                    return false;
                }
                                                      
            }
          else if( document.getElementById("<%=RenNoRadioButton.ClientID%>").checked)
            {                   
                if(document.getElementById("<%=RenNoTextBox.ClientID%>").value=="")
                {
                    alert("Please Enter Renewal  Number");
                    document.getElementById("<%=RenNoTextBox.ClientID%>").focus(); 
                    return false;
                }
                                                      
            }
           else if( document.getElementById("<%=TrNoRadioButton.ClientID%>").checked)
            {                   
                if(document.getElementById("<%=TrNoTextBox.ClientID%>").value=="")
                {
                    alert("Please Enter Transfer Number Number");
                    document.getElementById("<%=TrNoTextBox.ClientID%>").focus(); 
                    return false;
                }
                if(document.getElementById("<%=LineNoTextBox.ClientID%>").value=="")
                {
                    alert("Please Enter Line Number");
                    document.getElementById("<%=LineNoTextBox.ClientID%>").focus(); 
                    return false;
                }
               if(document.getElementById("<%=LineNoTextBox.ClientID%>").value!="")
                {
                    var lineNumber=document.getElementById("<%=LineNoTextBox.ClientID%>").value;                                                      
                    if(parseInt(lineNumber)>7||parseInt(lineNumber)<1)
                    {
                        alert("Please Enter Line Number between 1 and 7 ");
                        document.getElementById("<%=LineNoTextBox.ClientID%>").focus(); 
                        return false;
                    }
                }
                                                      
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
	   
	    document.getElementById("<%=SaleNoRadioButton.ClientID%>").checked=true;
	    document.getElementById("<%=SaleNoTextBox.ClientID%>").value="";
	    document.getElementById("<%=RenNoRadioButton.ClientID%>").checked=false;
	    document.getElementById("<%=RenNoTextBox.ClientID%>").disabled=true;           
	    document.getElementById("<%=RenNoTextBox.ClientID%>").value="";
	    
	    document.getElementById("<%=RenNoRadioButton.ClientID%>").checked=false;
	    document.getElementById("<%=RenNoTextBox.ClientID%>").disabled=true;           
	    document.getElementById("<%=RenNoTextBox.ClientID%>").value="";
	    document.getElementById("<%=LineNoTextBox.ClientID%>").disabled=true;           
	    document.getElementById("<%=LineNoTextBox.ClientID%>").value="";
	    
	    document.getElementById("<%=regNoTextBox.ClientID%>").value="";
	    document.getElementById("<%=NoUnitsLabel.ClientID%>").innerHTML="";
	    document.getElementById("<%=HolderNameTextBox.ClientID%>").value="";
	    document.getElementById("<%=JHolderNameTextBox.ClientID%>").value="";
	    document.getElementById("<%=Address1TextBox.ClientID%>").value="";
	    document.getElementById("<%=Address2TextBox.ClientID%>").value="";
	    document.getElementById("<%=CityTextBox.ClientID%>").value="";
	    
	    
	     return false;
	}
	function fnCheqInput()
	{
	    
	}
    </script>
        
    <style type="text/css">
        .style4
        {
            width: 275px;
        }
        .style5
        {
            color: #009900;
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
                Unit Cetificate Print Form (<span id="spanFundName" runat="server"></span>)
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
<table align="center" cellpadding="0" cellspacing="0" style="width:1000px" > 
<colgroup width="120"></colgroup>
<colgroup width="430"></colgroup>
<colgroup width="120"></colgroup>
    <tr>
       <td align="left"  >
           <asp:RadioButton ID="SaleNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="1"  onclick="fnEnable('SaleNoRadioButton');"
                Text="Sale No:" style="font-weight: 700" Checked="True"/>
        </td>
        <td align="left"  colspan="3" >
                                    <asp:TextBox ID="SaleNoTextBox" runat="server"    
                    CssClass= "TextInputStyleSmall" TabIndex="2" 
                     onkeypress= "fncInputNumericValuesOnly()"
                     Width="95px" ></asp:TextBox>
        </td>
      
      </tr>
      <tr>
       <td align="left"  >
           <asp:RadioButton ID="RenNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="3" onclick="fnEnable('RenNoRadioButton');"
                Text="Renewal No:" style="font-weight: 700"  />
               
        </td>
        <td align="left"  colspan="3" >
                                    <asp:TextBox ID="RenNoTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" TabIndex="4" 
                     onkeypress= "fncInputNumericValuesOnly()"
                    Width="95px" Enabled="False" ></asp:TextBox>
        </td>
      
    </tr>
     <tr>
     <td align="left"  >
           <asp:RadioButton ID="TrNoRadioButton" 
                runat="server" GroupName="taxType" TabIndex="5" onclick="fnEnable('TrNoRadioButton');"
                Text="Transfer No :" style="font-weight: 700" />
        </td>
        <td align="left" class="style4">
                                    <asp:TextBox ID="TrNoTextBox" runat="server"  
                    CssClass= "TextInputStyleSmall" TabIndex="6" 
                    onkeypress= "fncInputNumericValuesOnly()"
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" Enabled="False"></asp:TextBox>
                                    <strong>
                                    <asp:RadioButton ID="narmalRadioButton" runat="server" Checked="True" GroupName="trType" Text="Normal" />
                                    <asp:RadioButton ID="deathRadioButton" runat="server" GroupName="trType" Text="Death" />
                                    </strong>
        </td>
       <td align="right"  >
           <b>Line No: 
             
        </b>&nbsp;
             
        </td>
        <td align="left">
                                    <asp:TextBox ID="LineNoTextBox" runat="server"  MaxLength="8"   
                    CssClass= "TextInputStyleSmall" TabIndex="7" 
                     onkeypress= "fncInputNumericValuesOnly()"
                     Width="95px" Enabled="False"></asp:TextBox>
        </td>
        
      
    </tr>
    <tr>
        <td align="center" colspan="4"> 
            <asp:Button 
                ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                OnClientClick="return fnCheck();" onclick="findButton_Click" 
                AccessKey="f" TabIndex="9"/> &nbsp;<span class="style5">(Alt+f)</span></td>
    </tr>
     <tr>
        <td align="center" colspan="4">
                &nbsp;</td>
    </tr>
    <tr>
        <td align="left"><b>Registration No:</b></td>
        <td align="left" class="style4"> <asp:TextBox ID="fundCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="branchCodeTextBox" runat="server" 
                    CssClass= "TextInputStyleSmall" Enabled="False" 
                    meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                &nbsp;
                <asp:TextBox ID="regNoTextBox" runat="server"   
                    CssClass= "TextInputStyleSmall"                    
                    meta:resourcekey="regNoTextBoxResource1" Width="95px" Enabled="False"></asp:TextBox></td>
                    <td align="right"  >
                        <b>&nbsp;No of Units: 
             
        </b> 
             
        </td>
        <td align="left">&nbsp;
                                
            <asp:Label ID="NoUnitsLabel" runat="server" Font-Bold="True" 
                ForeColor="#660066"></asp:Label>
                                
        </td>
        
    </tr>
    <tr>
        <td align="left"><b>Name of Holder:</b></td>
        <td align="left" colspan="3"> 
                                    <asp:TextBox ID="HolderNameTextBox" runat="server"   
                    CssClass= "TextInputStyleSmall" Width="444px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left"><b>Joint Holder:</b></td>
        <td align="left" colspan="3"> 
                                    <asp:TextBox ID="JHolderNameTextBox" runat="server"   
                    CssClass= "TextInputStyleSmall" Width="444px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left"><b>Address1 :</b></td>
        <td align="left" colspan="3"> 
                                    <asp:TextBox ID="Address1TextBox" runat="server"   
                    CssClass= "TextInputStyleSmall" Width="444px" Enabled="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td align="left"><b>Address2:</b></td>
        <td align="left" colspan="3"> 
                                    <asp:TextBox ID="Address2TextBox" runat="server"   
                    CssClass= "TextInputStyleSmall" Width="444px" Enabled="False"></asp:TextBox></td>
    </tr>
      <tr>
        <td align="left"><b>City:</b></td>
        <td align="left" colspan="3"> 
                                    <asp:TextBox ID="CityTextBox" runat="server"   
                    CssClass= "TextInputStyleSmall" Width="444px" Enabled="False"></asp:TextBox></td>
    </tr>
      <tr>
        <td align="left" colspan="4">&nbsp;</td>
      
    </tr>
</table>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
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

