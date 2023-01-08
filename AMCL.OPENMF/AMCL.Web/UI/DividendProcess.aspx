<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="DividendProcess.aspx.cs" Inherits="UI_DividendProcess" Title=" Dividend Process (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

<script language="javascript" type="text/javascript"> 
   
     

   
	    

 </script>
    
    
    
       
    
    
    <style type="text/css">
        .auto-style2 {
            height: 24px;
        }
    </style>
    
    
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />               
    

 <table align="center">
        <tr>
            <td class="FormTitle" align="center">
                Dividend Process Form</td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
 <table width="1100px" align="center" cellpadding="0" cellspacing="0" border="0" >
<colgroup width="150"></colgroup>
<colgroup width="310"></colgroup>
<colgroup width="200"></colgroup>
 
     <tr>
        <td colspan="4" align="left">
        <fieldset>
            <legend style="font-weight: 700"> ::Dividend Parameter ::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
             <tr>
        <td align="right" ><b>Fund Name:</b></td>
        <td align="left">
           <asp:DropDownList ID="fundNameDropDownList" runat="server" AutoPostBack="True" 
                onselectedindexchanged="fundNameDropDownList_SelectedIndexChanged" >
            </asp:DropDownList>
            <span class="star">*</span></td>
        <td align="right"><b>Fiscal Year:</b></td>
        <td align="left">
          &nbsp;<asp:DropDownList ID="DividendFYDropDownList" runat="server" AutoPostBack="True" onselectedindexchanged="DividendFYDropDownList_SelectedIndexChanged" style="height: 22px" TabIndex="3">
            </asp:DropDownList>
            <span class="star">*</span></td> 
       
    </tr>
    <tr>
        <td align="right" class="auto-style2" ><b>Closing Date:</b></td>
        <td align="left" class="auto-style2">
            <asp:DropDownList ID="ClosingDateDropDownList" runat="server" TabIndex="4" >
            </asp:DropDownList>
            <span class="star">*</span></td>  
             
        <td align="right" class="auto-style2"><b>FY Part :</b></td>
        <td align="left" class="auto-style2">
                &nbsp;<asp:DropDownList ID="fyPartDropDownList" runat="server" TabIndex="4">
                </asp:DropDownList>
                <span class="star">*</span></td>
    </tr>
     <tr>
        <td align="right" class="auto-style4" >&nbsp;</td>
        <td align="left" class="auto-style4" >
            
            &nbsp;</td>   
                <td align="right" class="auto-style4"> &nbsp;</td>        
                
                 <td align="left" class="auto-style4"> &nbsp;</td>
             
       
    </tr>
   
 </table>
        </fieldset>
        </td>
        
   
        
    </tr>
   
   
    <tr>
        <td colspan="4">&nbsp;</td>
        
    </tr>
    <tr>
        <td colspan="4">
          <fieldset>
            <legend style="font-weight: 700"> ::Dividend Process Type (NON ID)::</legend>
            <br />
            <table width="100%" align="center" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="150"></colgroup>
            <colgroup width="300"></colgroup>
            <colgroup width="200"></colgroup>
              <tr>
      
        
        <td align="right" >
            <b>&nbsp;CIP Issue Process:</b></td>
       <td align="left">
        <asp:Button ID="CIPButton" runat="server" Text="Start CIP Issue" CssClass="auto-style5" OnClick="CIPButton_Click" Width="186px" 
                  /></td>
                   <td align="right" >
            <b>CIP Issue Status</b></td>
        <td align="left">
            &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Text="CIPIssueStatuesLabel"></asp:Label>
            &nbsp;</td></tr>
            
   
    
   
               
     
            </table>
            </fieldset>
        </td>
        
    </tr>
  <tr>
        <td colspan="4">&nbsp;</td>
        
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
</asp:Content>

