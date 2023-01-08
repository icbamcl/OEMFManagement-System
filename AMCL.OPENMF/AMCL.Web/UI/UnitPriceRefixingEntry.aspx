<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitPriceRefixingEntry.aspx.cs" Inherits="UI_UnitPriceRefixingEntry" Title=" Unit Price Refixation Form (Design and Developed by Sakhawat)" %>
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
           if(document.getElementById("<%=PriceDateTextBox.ClientID%>").value =="")
            {               
              
                document.getElementById("<%=PriceDateTextBox.ClientID%>").focus();
                alert("Please Enter Price Refixation Date");
                return false;
               
            }
            if(document.getElementById("<%=PriceDateTextBox.ClientID%>").value !="")
            {
                var checkDate=/^([012]?\d|3[01])-([Jj][Aa][Nn]|[Ff][Ee][bB]|[Mm][Aa][Rr]|[Aa][Pp][Rr]|[Mm][Aa][Yy]|[Jj][Uu][Nn]|[Jj][Uu][Ll]|[aA][Uu][gG]|[Ss][eE][pP]|[Oo][Cc][Tt]|[Nn][Oo][Vv]|[Dd][Ee][Cc])-(19|20)\d\d$/;
                if(!checkDate.test(document.getElementById("<%=PriceDateTextBox.ClientID%>").value))
                    {
                     document.getElementById("<%=PriceDateTextBox.ClientID%>").focus();
                     alert("Plese Select Date From The Calender");
                     return false;
                    }
             }
            if(document.getElementById("<%=SalePriceTextBox.ClientID%>").value =="")
            {                            
                document.getElementById("<%=SalePriceTextBox.ClientID%>").focus();
                alert("Please Enter Sale Price");
                return false;
            }
            if(document.getElementById("<%=RepPriceTextBox.ClientID%>").value =="")
            {                            
                document.getElementById("<%=RepPriceTextBox.ClientID%>").focus();
                alert("Please Enter Repurchase Price");
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
        .style8
        {
            font-size: small;
            font-weight: bold;
            text-align: right;
        }
        .style9
        {
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
    
<br />
<br />
 <table align="left" style="width: 537px">
        <tr>
            <td class="FormTitle" align="center">
               Price Refixing Entry Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>

    <br />
     <br />
      <br />
    <asp:UpdatePanel ID="holderInfoUpdatePanel" runat="server">
    <ContentTemplate>  
<table width="600" align="left" cellpadding="0" cellspacing="0" >
<colgroup width="190"></colgroup>
<colgroup width="75"></colgroup>
<colgroup width="100"></colgroup>
<colgroup width="75"></colgroup>

    
    
     <tr >
        <td class="style9" style="font-size: small"  ><b>Fund Name :</b></td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:DropDownList ID="fundNameDropDownList" runat="server" 
                    TabIndex="2" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" ></asp:DropDownList>
            <span class="star">*</span></td>
       
    </tr>
     <tr >
        <td align="left" class="style10" style="text-align: right"  >Price Refix Date:</td>
        
        <td align="left"   colspan="4"> &nbsp;&nbsp;          
            <asp:TextBox ID="PriceDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="2"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="RegDatecalendarButtonExtender" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="PriceDateImageButton" 
                TargetControlID="PriceDateTextBox" />
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                Format="dd-MMM-yyyy" PopupButtonID="RegDateImageButton" 
                TargetControlID="PriceDateTextBox" />
            <asp:ImageButton ID="PriceDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="3" />
            <span class="star">*</span></td>
       
    </tr>
    <tr class="style5">
       <td class="style8"  >
           Sale Price :
        </td>
        
        <td align="left" colspan="4">&nbsp;
            <asp:TextBox ID="SalePriceTextBox" runat="server" 
                CssClass="TextInputStyleSmall"  AutoPostBack="True" 
                TabIndex="6" Width="100px" ></asp:TextBox>
            <span class="star">*</span></td>
         
      
    </tr>
     <tr class="style5">
       <td class="style8"  >
           Repurchase Price :
        </td>
        
        <td align="left" colspan="4">&nbsp;
            <asp:TextBox ID="RepPriceTextBox" runat="server" 
                CssClass="TextInputStyleSmall" 
                TabIndex="6" Width="100px"></asp:TextBox>
            <span class="star">*</span></td>
        
      
    </tr>
     
    
    
  
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="5">
        <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                 AccessKey="p"  TabIndex="10" onclick="SaveButton_Click" />&nbsp;&nbsp;
        <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" 
                meta:resourcekey="ResetButtonResource2" />&nbsp;&nbsp;
        <asp:Button ID="CloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="CloseButton_Click" AccessKey="c" meta:resourcekey="CloseButtonResource1" 
                  />
        </td>
    </tr>
    
</table>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="fundNameDropDownList" EventName="SelectedIndexChanged" />
    <%--<asp:AsyncPostBackTrigger ControlID="SalePriceTextBox" EventName="TextChanged" />--%>
    
</Triggers>
</asp:UpdatePanel>


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

