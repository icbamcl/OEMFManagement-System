<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDividendReconPostReturnEntry.aspx.cs" Inherits="UI_UnitDividendReconPostReturnEntry" Title=" Unit Dividend Reconcile (Design and Developed by Jagonnath)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
        <style type="text/css">
        .style6
        {
                height: 18px;
                font-weight: 700;
            }
            .style7
            {
                width: 127px;
                font-weight: 700;
            }
            .huy
            {
            display:none;
            }
            .auto-style1 {
                font-family: Verdana, Arial, Helvetica, sans-serif;
                border: 1px #1B68B8 solid;
                BACKGROUND-COLOR: #FFFFDD;
                COLOR: #000000;
                FONT-SIZE: 12px;
                padding-left: 2px;
            }
            .auto-style2 {
                width: 847px;
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
                color: #FF3300;
            }
            </style>
    <script language="javascript" type="text/javascript"> 


  function fnReset()
    {
        var confm=confirm("Are Sure To Reset?");
        if(confm)
        {         
            document.getElementById("<%=warrantNosTextBox.ClientID%>").value ="";
            document.getElementById("<%=FundNameDropDownList.ClientID%>").value ="0";
            document.getElementById("<%=DividendFYDropDownList.ClientID%>").value ="0";
            
            //dvGridWarrantInfo.Visible = false;
            return false;
        }
        else
        {
            return true;
            
        }
    }
    
        function fnCheqInput()
        {
             if(document.getElementById("<%=warrantNosTextBox.ClientID%>").value =="")
                {   
          
                        document.getElementById("<%=warrantNosTextBox.ClientID%>").focus();
                        alert("Please Enter Warrnt Number (s) Number");
                        return false;
            
             }
             if(document.getElementById("<%=FundNameDropDownList.ClientID%>").value =="0")
                {   
          
                        document.getElementById("<%=FundNameDropDownList.ClientID%>").focus();
                        alert("Please Select Fund Name");
                        return false;
            
             }
            if(document.getElementById("<%=DividendFYDropDownList.ClientID%>").value =="0")
                {   
          
                        document.getElementById("<%=DividendFYDropDownList.ClientID%>").focus();
                        alert("Please Select FY");
                        return false;
            
                }
        }
    
    </script>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
    
    <br />
     <table align="center">
            <tr>
                <td class="FormTitle" align="center">
                                    &nbsp;Dividend Reconsiliation 
                                    Warrant Post Return Entry Form 
                </td>           
            </tr> 
          </table>
    <br />
<div id="dvContent" runat="server" style="width:1000px; padding-left:20px; padding-right:20px" >
    <div style=" float:left; width: 898px;" >
<table align="center" cellpadding="0" cellspacing="0" class="auto-style2">
<colgroup width="220"></colgroup>
    <tr>
        <td align="right"><b>&nbsp;Fund&nbsp;Fund Name:</b></td>
        <td align="left">
            <asp:DropDownList ID="FundNameDropDownList" runat="server"  AutoPostBack="True" OnSelectedIndexChanged="FundNameDropDownList_SelectedIndexChanged" 
                 ></asp:DropDownList>
            <span class="auto-style4">*</span></td>
    </tr>
    <tr>
        <td align="right" ><b>Fiscal Year:</b></td>
        <td align="left">               
            <asp:DropDownList ID="DividendFYDropDownList" runat="server"   
               
              TabIndex="1" OnSelectedIndexChanged="DividendFYDropDownList_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
                                    <span class="auto-style4">*</span></td>
    </tr>
         <tr >
       <td align="right" class="style9">
           <b>Closing Date : </b>
        </td>       
        <td align="left" class="style9">
            
            <asp:DropDownList ID="ClosingDateDropDownList" runat="server" TabIndex="4">
            </asp:DropDownList>
            </td>  
                  
    </tr>
    
    <tr>
        <td align="right" ><b>Warrant No(s)<br />
&nbsp;(1,2,3):</b></td>
        <td align="left" class="style6">               
            <asp:TextBox ID="warrantNosTextBox" runat="server"  
                    CssClass= "auto-style1" TabIndex="3" Width="602px" 
                  Height="80px" TextMode="MultiLine" ></asp:TextBox>&nbsp;<span class="auto-style4">*</span></td>
    </tr>
       
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr> 
    
    <tr>
        <td align="center" class="style7" >
            
        </td>
        <td align="left">
            &nbsp;<asp:Button 
                ID="saveButton" runat="server" CssClass="auto-style3" Text="Save for Return" OnClientClick="return fnCheqInput();" 
                onclick="saveButton_Click" Width="134px" Height="23px" />

            &nbsp;&nbsp;<asp:Button ID="resetButton" runat="server" CssClass="auto-style3" 
               Text="Reset" OnClick="resetButton_Click" Height="22px" />
&nbsp;
            <asp:Button ID="CloseButton" runat="server" CssClass="auto-style3" 
                 Text="Close" 
                onclick="CloseButton_Click" TabIndex="8" Height="23px" Width="49px" />
        </td>
    </tr>
    <tr>
            <td colspan="2">&nbsp;</td>
    </tr>
</table>
</div>
           
</div>
        
 
    <br />
    <br />               
</asp:Content>

