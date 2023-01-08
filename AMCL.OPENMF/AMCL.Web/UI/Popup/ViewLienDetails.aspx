<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ViewLienDetails.aspx.cs" Inherits="UI_ViewLienDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Show Lien Details (Design and Developed by Sakhawat)</title>
    <link rel="Stylesheet" type="text/css" href="../../CSS/amcl.css" />
    <style type="text/css">
        .style2
        {
            width: 77px;
        }
        .style3
        {
            width: 380px;
        }
        .style4
        {
            width: 115px;
        }
        .style5
        {
            font-size: small;
            font-weight: bold;
            color: #FF3300;
            text-decoration: underline;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server" style="height:auto; width:1000px;">
    <div  style="text-align: Left; float:left;  height:auto; width: 1000px;">
      <table align="left" width="805">
         <tr>
             <td align="left" class="style2">Name:</td> 
             <td align="left" id="tdHolderName" runat="server" class="style3"></td>
              <td align="left" class="style4" >Registration No:</td> 
             <td align="left" id="tdReg" runat="server"></td>
         </tr>
         <tr>
             <td class="style2">Fund Name:</td> 
             <td align="left" id="tdFund" runat="server" class="style3"></td>
             <td align="left" class="style4">Total Lien Units:</td> 
              <td align="left" id="tdBranch" runat="server"></td>
         </tr>         
         <tr>
              
             <td align="left" colspan="4">&nbsp;&nbsp;</td>
        </tr> 
      
         <tr>
             <td align="center" colspan="4" class="style5">Unit Holder Lien Details</td> 
             
        </tr> 
        <tr>              
             <td align="left" colspan="4">&nbsp;&nbsp;</td>
        </tr> 
         <tr>              
           <td align="left" id="tdLeinDetails"  runat="server" colspan="4"></td>
        </tr> 
         <tr>              
             <td align="left" colspan="4">&nbsp;&nbsp;</td>
        </tr>
        <tr>              
             <td align="left" colspan="4">&nbsp;&nbsp;</td>
        </tr>
         <tr>              
             <td align="center" colspan="4"><asp:Button ID="Button1" runat="server" Text="Close" 
                CssClass="buttoncommon" OnClientClick="window.close();" AccessKey="c" /></td>
        </tr>
        </table>                  
       
    
    
    </div>
    
    </form>
</body>
</html>
