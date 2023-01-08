<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitSIPReportIndividualInstalment.aspx.cs" Inherits="UI_UnitSIPReportIndividualInstalment" Title=" SIP Statement Report(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<script runat="server">

    
</script>

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
 function fnCheckRegNo()
     {                                
         if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
     }
        function fnCheckInput()
     {                                
         if(document.getElementById("<%=sipNumberDropDownList.ClientID%>").value =="0")
            {
                document.getElementById("<%=sipNumberDropDownList.ClientID%>").focus();
                alert("Please SIP Number");
                return false;
                
            }
     }  
      
     
</script>



    <style type="text/css">
        .auto-style2 {
            height: 26px;
        }
        .auto-style3 {
            height: 26px;
            text-align: right;
        }
        .auto-style4 {
            text-align: right;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
      
      <br />
      <table align="left" cellpadding="0" cellspacing="0" 
        style="width: 664px; height: 193px;">
      <colgroup width="120"></colgroup>
      <tr>
            <td class="FormTitle" align="center"  colspan="3">
                Unit SIP Individual Installment Statement Report Form (<span id="spanFundName" runat="server"></span>)
            </td>           
           
        </tr> 
      <tr>
        <td align="left" colspan="2">
         &nbsp;&nbsp;
        </td>
        
      </tr>
      <tr>
        <td class="auto-style4">
            <strong>Fund Code:
        </strong>
        </td>
        <td align="left" colspan="2">
                        <asp:DropDownList ID="fundCodeDDL" runat="server"  ></asp:DropDownList>
        </td>
      </tr>
       <tr>
        <td class="auto-style4">
            <strong>Branch Code:
        </strong>
        </td>
        <td align="left" colspan="2">
            <asp:DropDownList ID="branchCodeDDL" runat="server"  ></asp:DropDownList>
                            
        </td>
      </tr>
      <tr>
        <td class="auto-style4">
            <strong>Registration No:</strong></td>
        <td align="left" colspan="2">
            <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"
                CssClass= "TextInputStyleSmall" TabIndex="1" Width="89px"              
                onkeypress= "fncInputNumericValuesOnly()" ></asp:TextBox>
                                <span class="star"><b><span style="font-weight:bold; height:100px;">*</span></b><asp:Button ID="findButton" 
                    runat="server" AccessKey="f" CssClass="buttonmid" 
                    onclick="findButton_Click" 
                    onclientclick="return fnCheckRegNo();" TabIndex="2" Text="Find" />
                </span>
        </td>
      </tr> 
       <tr>
        <td class="auto-style4">
            <strong>SIP No:</strong></td>
        <td align="left"  >
            <b><span style="font-weight:bold; height:100px;">
        
            <asp:DropDownList ID="sipNumberDropDownList" runat="server" 
                CssClass="DropDownList"       
                >
                                  
            </asp:DropDownList>
                                                   
                                            
                                                   
                                <span class="star">*</span></span></b></td>
            <td>
           &nbsp;<%-- <table>
                    <tr>
                         <td align="left" class="style11" >Sale Type:</td>
            <td align="left" >
             <asp:DropDownList ID="saleTypeDropDownList" runat="server" 
                    CssClass="DropDownList" TabIndex="12" >
                 <asp:ListItem Value="0" Text=""> </asp:ListItem>
                <asp:ListItem Value="SL">SALE</asp:ListItem>
                <asp:ListItem Value="CIP" >CIP</asp:ListItem>
                 <asp:ListItem Value="SIP" >SIP</asp:ListItem>
                </asp:DropDownList>&nbsp;</td>
                    </tr>
             </table>--%></td>
      </tr>
          <%-- <tr>
        <td align="left">
         Registration No:</td>
        <td align="left" colspan="2">
        <asp:TextBox ID="fromRegNoTextBox" runat="server" onBlur="Javascript:fnOnChangeText2();"
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="13" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;<b><span style="font-weight:bold; height:100px;">&nbsp; To</span></b>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="toRegNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="14" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox>
        </td>
      </tr>--%>
     
            <tr>
        <td class="auto-style3">
            <strong>Report Type:
        </strong>
        </td>
        <td align="left" colspan="2" class="auto-style2">
            <asp:RadioButton ID="allRadioButton" runat="server" Font-Bold="True" GroupName="sipHolder" Text="All" Checked="True" />
                                                   
                                            
                                                   
            <asp:RadioButton ID="paidRadioButton" runat="server" Font-Bold="True" GroupName="sipHolder" Text=" Paid" />
                                                   
                                            
                                                   
            <asp:RadioButton ID="unpaidRadioButton" runat="server" Font-Bold="True" GroupName="sipHolder" Text=" Unpaid" />
                </td>
      </tr>
      <tr>
        <td align="left" colspan="2">
         &nbsp;&nbsp;
        </td>
        
      </tr>
      <tr>
        <td align="right">
        <asp:Button ID="ShowReportButton" runat="server" Text="Print Repoert" CssClass="buttoncommon" OnClick="ShowReportButton_Click"
             onclientclick="return fnCheckInput();"     />&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" 
                  />
        &nbsp;&nbsp;
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
</asp:Content>

