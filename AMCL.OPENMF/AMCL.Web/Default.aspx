<%@Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" Title="ICB Asset Management Compnay Limited Login Page (Design and Developed by Sakhawat)" %>
<%@ Register Assembly="MSCaptcha" Namespace="MSCaptcha" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login Page</title>
    <script language="javascript" type="text/javascript"> 
    function fnValidation()
    {
         if(document.getElementById("<%=loginIDTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=loginIDTextBox.ClientID%>").focus();
            alert("Please Enter LoginID");
            return false;
            
        }
        if(document.getElementById("<%=loginPasswardTextBox.ClientID%>").value =="")
        {
            document.getElementById("<%=loginPasswardTextBox.ClientID%>").focus();
            alert("Please Enter Login Password");
            return false;
            
        }
    }
    
  
  </script>
<link rel="Stylesheet" type="text/css" href="CSS/amcl.css"/>
    <style type="text/css">
        .style3
        {
            font-size: small;
            font-family: "Courier New";
            font-weight: 700;
        }
        .style4
        {
            font-family: "Courier New", Courier, monospace;
            font-weight: bold;
        }
        .style5
        {
            font-family: "Courier New";
            font-size: small;
            color:Red;
        }
        
        .style6
        {
            text-align: center;
            color: #3399FF;
            font-weight: bold;
            font-family: "Times New Roman";
            font-size: x-large;
        }
        
        .style7
        {
            width: 161px;
        }
        
        .style8
        {
            font-size: x-small;
            color: #CC33FF;
        }
        
        .auto-style1 {
            font-size: x-large;
            color: #993399;
        }
        .auto-style2 {
            text-align: center;
        }
        
        .auto-style3 {
            BORDER-TOP: #CCCCCC 1px solid;
            BORDER-BOTTOM: #000000 1px solid;
            BORDER-LEFT: #CCCCCC 1px solid;
            BORDER-RIGHT: #000000 1px solid;
            COLOR: #FFFFFF;
            FONT-WEIGHT: bold;
            FONT-SIZE: 11px;
            BACKGROUND-COLOR: #547AC6;
        }
        .auto-style4 {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            border: 1px #1B68B8 solid;
            BACKGROUND-COLOR: #FFFFDD;
            COLOR: #000000;
            FONT-SIZE: 12px;
            padding-left: 2px;
        }
        
        .auto-style5 {
            font-size: large;
            text-decoration: underline;
        }
        
        .auto-style6 {
            font-size: large;
        }
        
        </style>
</head>
<body onkeydown="if (event.keyCode==13) {event.keyCode=9; return event.keyCode }">
    <form id="form1" runat="server" method="post" >
    <div>
       
    <table width="100%"  align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td align="center"> <strong><span class="auto-style1">IAMCL Unit Trading System</span></strong> </td>
    </tr>
    <tr>
        <td align="right"> 
             <table align="right" cellpadding="0" cellspacing="0" >    
                <tr>
                <td align="right" class="style3">
                 Login ID:
                </td>
                <td align="left" class="style3">
                <asp:TextBox ID="loginIDTextBox" runat="server" CssClass="auto-style4" TabIndex="1" Width="100px" ></asp:TextBox>
                </td>
                 <td class="style4" align="right">
                    <span class="style3">&nbsp;Password:</span>
                </td>
                <td colspan="2" align="left">
                <asp:TextBox ID="loginPasswardTextBox" runat="server" TextMode="Password" CssClass="auto-style4"  TabIndex="2" Width="93px"></asp:TextBox>
                </td>
                 <td align="center" class="auto-style2">
                    &nbsp;<asp:Button ID="loginButton" runat="server" Text="Login" 
                        CssClass="auto-style3" OnClientClick="return fnValidation();" 
                       TabIndex="3" Height="22px" Width="74px"/>
                
             </td>
                 <td align="center" colspan="3">
                &nbsp;&nbsp;&nbsp;
                </td>
            </tr>        
            </table>       
        </td>        
       </tr>
        <tr>
        <td align="right"> 
             <table align="right" cellpadding="0" cellspacing="0" >    
                <tr>
                 <td align="right" >
                    <asp:Label runat="server" ID="loginErrorLabel" Visible="false" Text="" class="style5"></asp:Label>
                 </td>
            </tr>        
            </table>       
        </td>        
       </tr>
        <tr>
            <td class="auto-style2">&nbsp;<span class="auto-style5"><strong> Latest Unit Price and NAV</strong></span></td>
        </tr>
        
        <tr>
            <td>
                  <table align="center">
                    <tr>
                        <td>
                         <div id="dvGridSurrender" runat="server"  >
                         
                                <asp:GridView ID="priceListListGridView" runat="server" AutoGenerateColumns="False" 
                                    BackColor="#DEBA84" 
                                    BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
                                    CellSpacing="2" CssClass="auto-style6" >
                                    <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                    <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="true" ForeColor="White" />
                                    <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <Columns>
                                                                                                                                
                                    <asp:BoundField DataField="FUND_NM" HeaderText="Name of The Fund" />
                                    <asp:BoundField DataField="REFIX_DATE" HeaderText="Refixation Date" />
                                    <asp:BoundField DataField="EFFECTIVE_DT" HeaderText="Effective Date" />
                                    <asp:BoundField DataField="REFIX_SL_PR" HeaderText="Sale Price" />
                                    <asp:BoundField DataField="REFIX_REP_PR" HeaderText="Surrender Price" />
                                    <asp:BoundField DataField="NAV_MP" HeaderText="NAV @ Market Price" />
                                    <asp:BoundField DataField="NAV_CP" HeaderText="NAV @ Cost Price" />
                               
                                </Columns>
                                </asp:GridView>
                            </div>                       
                        </td>
                    </tr>
                     <tr>
                         <td align="left" colspan="4">
                      &nbsp;</td>
                     </tr>
                </table>
            </td>
        </tr>
         <tr>
            <td>&nbsp; </td>
        </tr>
         <tr>
            <td>&nbsp; </td>
        </tr>
         <tr>
            <td>&nbsp; </td>
        </tr>
     <tr>
        <td class="auto-style2">  Design and developed by&nbsp; Md. Sakhawat Hossain&nbsp; Programmer, ICB AMCL</td>
    </tr>
   </table>
   
   <%-- <table align="center" cellpadding="0" cellspacing="0" >    
        <tr>
            <td align="right" class="style3">
             Login ID:
            </td>
            <td align="left" class="style3" colspan="2">
            <asp:TextBox ID="loginIDTextBox" runat="server" CssClass="textInputStyle" TabIndex="1" ></asp:TextBox>
            </td>
        </tr>
        
        <tr>
            <td class="style4" align="right">
                <span class="style3">Password:</span>
            </td>
            <td colspan="2" align="left">
            <asp:TextBox ID="loginPasswardTextBox" runat="server" TextMode="Password" CssClass="textInputStyle"  TabIndex="2"></asp:TextBox>
            </td>
        </tr>
        <tr>
         
        </tr>
         <tr>
         <td class="auto-style1">
         &nbsp;
         </td>
         <td align="center" class="auto-style2">
                <asp:Button ID="loginButton" runat="server" Text="Login" 
                    CssClass="buttoncommon" OnClientClick="return fnValidation();" 
                   TabIndex="3" Height="27px" Width="116px"/>
         </td>
         <td class="auto-style2">
         &nbsp;
         </td>

         </tr>
         <tr>
          
          </tr>
        </table>--%>
    </div>
    </form>
</body>
</html>
