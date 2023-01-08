<%@ Page Language="C#" MasterPageFile="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportRepurchaseVoucher.aspx.cs" Inherits="UI_UnitReportRepurchaseVoucher" Title=" Repurchase Voucher Report(Design and Developed by Sakhawat)" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
   function fnReset()
   {
        document.getElementById("<%=fromRegNoTextBox.ClientID%>").value ="";
        
        document.getElementById("<%=fromRepDateTextBox.ClientID%>").value ="";
        
        document.getElementById("<%=fromRepNoTextBox.ClientID%>").value ="";
      
   }
  
    function fnCheckInput()
    {
    
         
          if(document.getElementById("<%=fromRepNoTextBox.ClientID%>").value =="")
               {
                document.getElementById("<%=fromRepNoTextBox.ClientID%>").focus();
                alert("Please Enter Repurchase Number ");
                return false;
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
</script>



    <style type="text/css">
        .style7
        {
            height: 20px;
        }
    </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" EnableScriptGlobalization="true"
        EnableScriptLocalization="true" ID="ScriptManager1" />        
       <%-- <table align="left">
        <tr>
            <td class="FormTitle" align="center">
           Unit Repurchase Voucher Report Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>--%>
      <br />
      
   
      <table align="left" cellpadding="0" cellspacing="0" style=" width: 699px;">
      <colgroup width="200"></colgroup>
        <tr>
            <td align="center" colspan="2" class="FormTitle" >
                Unit Repurchase Voucher Report Form (<span id="spanFundName" runat="server"></span>)
            </td>
      </tr>
        <tr>
            <td align="left" colspan="2">
            &nbsp;
            </td>
      </tr>
      <tr>
        <td align="right">
         Fund Code:
        </td>
        <td align="left">
        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="80px" TabIndex="1"></asp:TextBox>
        </td>
      </tr>
      <tr>
        <td align="right">
         Branch Code:
        </td>
        <td align="left">
        <asp:TextBox ID="branchCodeTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Enabled="False" Width="80px" TabIndex="2"></asp:TextBox>
        </td>
      </tr>
       <tr>
        <td align="right">
            Repurchase No:</td>
        <td align="left" >
        <asp:TextBox ID="fromRepNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="3" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> &nbsp;</td>
      </tr>
       <tr>
        <td align="right">
         Registration No:</td>
        <td align="left" >
        <asp:TextBox ID="fromRegNoTextBox" runat="server" 
                CssClass= "TextInputStyleSmall" Width="100px" TabIndex="5" onkeypress= "fncInputNumericValuesOnly()"></asp:TextBox> 
            </td>
      </tr>
   <tr>
        <td align="right" class="style7">
            Repurchase&nbsp; Date:</td>
        <td align="left" class="style7">
        <asp:TextBox ID="fromRepDateTextBox" runat="server" CssClass="textInputStyleDate" 
                TabIndex="6"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="fromRegDatecalendarButtonExtender" 
                runat="server" TargetControlID="fromRepDateTextBox" 
                PopupButtonID="fromRegDateImageButton" Format="dd-MMM-yyyy"/>
            <asp:ImageButton ID="fromRegDateImageButton" runat="server" 
                AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                TabIndex="7" />
            </td>
      </tr>
   <tr>
       <td colspan="2"  align="center">
              
                 
             <b>Include Lien Amount</b> <b>:</b><asp:RadioButton ID="NoRadioButton" runat="server" 
                 Checked="True" EnableTheming="False" GroupName="Lien" Text="No" 
                 TabIndex="15" oncheckedchanged="NoRadioButton_CheckedChanged"  />
             &nbsp;<asp:RadioButton ID="YesRadioButton" runat="server" 
                 GroupName="Lien" Text="Yes" AutoPostBack="True" 
                  TabIndex="15" oncheckedchanged="YesRadioButton_CheckedChanged" />              
                 
         &nbsp; <b>Amount (in Tk.):
             <span class="star">
             <asp:TextBox ID="LienAmountTextBox" runat="server" 
                 CssClass="TextInputStyleSmall"  
                 Width="117px"></asp:TextBox>
             * </span>
                                            </b>              
                 
         </td>
      </tr>
    <tr>
      <td align="right"> Lien Institution:</td>
        <td align="left"> 
            <asp:DropDownList ID="LienbankNameDropDownList" runat="server" 
                AutoPostBack="true" Enabled="False" 
                 
                TabIndex="16" 
                onselectedindexchanged="LienbankNameDropDownList_SelectedIndexChanged">
            </asp:DropDownList>
            <span class="star">* </span>
         </td>
    </tr>
     <tr>
      <td align="right"> Lien Institution Branch:</td>
        <td align="left"> 
            <asp:DropDownList ID="LienbranchNameDropDownList" runat="server" 
                AutoPostBack="true" Enabled="False" 
                 
                TabIndex="16">
            </asp:DropDownList>
            <span class="star">* </span>
         </td>
    </tr>
    <tr>
             <td align="right"  >Surrender Letter Reference No:</td>
            <td align="left" >
                <asp:TextBox ID="LienReqRefTextBox" runat="server" 
                    CssClass="TextInputStyleLarge" MaxLength="55" TabIndex="7" Enabled="False"></asp:TextBox>
                <span class="star">* </span>
             </td>
             
        </tr>
     <tr>
            <td align="right" >Letter Reference Date:</td>
            <td align="left">
                <span class="star">
                <asp:TextBox ID="LienReqDateTextBox" runat="server" CssClass="textInputStyleDate" 
                    TabIndex="4" Enabled="False"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="LienReqDatecalendarButtonExtender" runat="server" 
                    TargetControlID="LienReqDateTextBox" PopupButtonID="LienReqDateImageButton" 
                    Format="dd-MMM-yyyy"/>
                <asp:ImageButton ID="LienReqDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" 
                    TabIndex="10" Enabled="False" />
                * </span></td>
            
           
        </tr>
        <tr>
       <td colspan="2"  align="center">
              
                 
             <b>Print</b> <b>:</b><asp:RadioButton ID="fundCopyRadioButton" runat="server" 
                 Checked="True" EnableTheming="False" GroupName="print" Text="Fund Copy" 
                 TabIndex="15" 
                 Font-Bold="True"  />
             &nbsp;<asp:RadioButton ID="accoountRadioButton" runat="server" 
                 GroupName="print" Text="Account Copy" AutoPostBack="True" 
                  TabIndex="15" 
                 Font-Bold="True" />              
                 
                    
                 
         </td>
      </tr>
        <tr>
            <td align="left" colspan="2">
            &nbsp;
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
      </tr>
      <tr>
      <td colspan="2">
       <table width="500" align="center" cellpadding="0" cellspacing="0">
     <tr>
        <td align="right">
        <asp:Button ID="ShowReportButton" runat="server" Text="Print Report" CssClass="buttoncommon"  OnClientClick="return fnCheckInput();"
                AccessKey="V" onclick="ShowReportButton_Click"/>&nbsp;
        </td>
        <td align="left">&nbsp;&nbsp;&nbsp;
        <asp:Button ID="regResetButton" runat="server" Text="Reset" 
                CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="r" />&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="regCloseButton" runat="server" Text="Close" 
                CssClass="buttoncommon"  onclick="regCloseButton_Click" 
                  />
        </td>
       
    </tr>
     
   </table>
   </td>
      </tr>
       </tr>
        <tr>
            <td align="left" colspan="2">
            &nbsp;
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
            </td>
      </tr>
      
</table>
     

    </asp:Content>

