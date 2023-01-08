<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitReportLedger111.aspx.cs" Inherits="UI_UnitReportLedger" Title="Unit Ledger Report Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {         
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";            
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value =""; 
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
       
            return false;
        }
        else
        {
         return false;
        }
       
    }
    
    
     function fnResetAll()
    {
                                                       
            document.getElementById("<%=holderNameTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";            
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";                                         
            document.getElementById("<%=tdCIP.ClientID%>").innerHTML ="";            
             
             
             alert("No Data Found");
             return false;
      
    }
     function fnCheqInput()
        {
        
        
              //Input Text Checking
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                var digitCheck = /^\d+$/;
                if(!digitCheck.test(document.getElementById("<%=regNoTextBox.ClientID%>").value))
                {
                    document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                    alert("Please Enter Valid Registration Number");
                    return false;
                }
            }
            
    }
         
       
     
  
  function PopupLienDetails()
        {
            if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="")
            {
                document.getElementById("<%=regNoTextBox.ClientID%>").focus();
                alert("Please Enter Registration Number");
                return false;
                
            }
              if(document.getElementById("<%=TotalLienUnitHoldingTextBox.ClientID%>").value =="0"||document.getElementById("<%=TotalLienUnitHoldingTextBox.ClientID%>").value =="")
            {
                
                alert("No Lien Units to Show Details");
                return false;
                
            }
            
           if(document.getElementById("<%=regNoTextBox.ClientID%>").value !="")
            {
                 var fundCode=document.getElementById("<%=fundCodeTextBox.ClientID%>").value;
                 var reg=document.getElementById("<%=regNoTextBox.ClientID%>").value;
                 var Branch=document.getElementById("<%=branchCodeTextBox.ClientID%>").value;
                 var url='Popup/ViewLienDetails.aspx?reg='+reg+'&fund='+fundCode+'&branch='+Branch;
                 var ViewLienDetails= window.open(url,'ViewLienDetails');
                 ViewLienDetails.focus();
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
         font-size: 12px;
         font-weight: bold;
         color:Red; 
        
     }
      .style8
     {
         font-size:12px;
         color: green;
         font-weight: bold;
     }
      </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Ledger&nbsp; Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      <div id="dvUpdatePannel" runat="server">
      <asp:UpdatePanel ID="ledgerUpdatePannel" runat="server">
      <ContentTemplate>
              <div id="dvContent" runat="server" style="width:900px; padding-left:80px; padding-right:100px" >
              <div id="dvUpcontent" runat="server" style="width:733px;">
                  
                  <table width="450" align="left" cellpadding="0" cellspacing="0" border="0" >
            <colgroup width="120"></colgroup>
            
                <tr>
                    <td colspan="2" align="left">&nbsp;</td>
                   
                </tr>
                <tr>
                    <td align="left" >Registration No:</td>
                    <td align="left">
                        <asp:TextBox ID="fundCodeTextBox" runat="server" 
                            CssClass= "TextInputStyleSmall" Enabled="False" 
                            meta:resourcekey="fundCodeTextBoxResource1"></asp:TextBox>
                        &nbsp;
                        <asp:TextBox ID="branchCodeTextBox" runat="server" 
                            CssClass= "TextInputStyleSmall" Enabled="False" 
                            meta:resourcekey="branchCodeTextBoxResource1"></asp:TextBox>
                        &nbsp;
                                            <asp:TextBox ID="regNoTextBox" runat="server"  MaxLength="8"   
                            CssClass= "TextInputStyleSmall" TabIndex="1" AutoPostBack="True" 
                            ontextchanged="regNoTextBox_TextChanged" onkeypress= "fncInputNumericValuesOnly()"
                            meta:resourcekey="regNoTextBoxResource1" Width="95px"></asp:TextBox>
                                            <span class="star">*</span>&nbsp;<asp:Button 
                            ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            onclick="findButton_Click" AccessKey="f" 
                            onclientclick="return fnCheqInput();" TabIndex="2" 
                            meta:resourcekey="findButtonResource1" /></td>
         
                   
                </tr>
                 
                <tr>
                    <td align="left" >Name of Holder:</td>
                    <td align="left">
                        <asp:TextBox ID="holderNameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" 
                            meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                    </td>
                   
                       
                   
                   
                </tr>
                <tr>
                    <td align="left" >&nbsp;</td>
                    <td align="left">
                        <asp:TextBox ID="jHolderTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                            meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
                    </td>
                   
                    
                    
                </tr>
                <tr>
                   <td align="left" >Address1:</td>
                    <td align="left">
                        <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                            </td>
                  
                    </tr>
                <tr>
                    <td align="left" >Address2:</td>
                    <td align="left" >
                        <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6" 
                            meta:resourcekey="holderAddress2TextBoxResource1"></asp:TextBox>
                                            </td>
                    
                </tr>   
                <tr>
                          <td align="left">Telephone/Mobile:</td>
                          <td align="left">
                         <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="7" 
                                  meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                            </td>
                   
                 </tr>
                <tr>
                    <td align="left" class="style7">CIP:</td>
                    <td align="left" id="tdCIP" runat="server" class="style7" >
                        &nbsp;</td>
                        
                </tr>                  
                <tr>
                          <td align="left" >Nominee 1&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="Nominee1NameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                                </td>
                         
                </tr>     
                <tr>
                          <td align="left" >Nominee 2&#39;s Name:</td>
                    <td align="left">
                        <asp:TextBox ID="Nominee2NameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                                </td>
                         
                </tr>          
                <tr>
                    <td align="left">Total Liened Units:</td>
                    <td align="left" class="style8">
                        <asp:TextBox ID="TotalLienUnitHoldingTextBox" runat="server" 
                            CssClass="TextInputStyleSmall" Enabled="false" Font-Bold="True" 
                            ForeColor="#009933" Width="100px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="ShowLienDatailsButton" runat="server" AccessKey="l" 
                            CssClass="buttoncommon" EnableTheming="True" OnClientClick=" return PopupLienDetails();"                            
                            Text="Lien Details" Height="17px" Width="71px" />
                    </td>
                </tr>  
                
                    
                </table>
                 <div  id="dvImage" runat="server" 
                      
                      
                      style=" border-style: solid; border-color: inherit; border-width: 1px; width: 217px; width:340px; height: 254px;">
                        <table align="center" width="164"  cellpadding="0" cellspacing="0" 
                            style="height: 180px">
                        <tr style=" height:15px;">
                           <td align="center" ><span class="style6"  style="border:1px solid">Signature    and 
                               Photo    </span></td></tr>
                           <tr style=" height:102px;">
                            <td align="left">
                          <asp:Image ID="SignImage" runat="server" Height="236px" Width="340px" 
                                    meta:resourcekey="SignImageResource1"  />
                            </td>    
                        </tr>
                        
                       
                        </table>
                   </div>
             </div>
             
            <%--<table width="705" align="center" cellpadding="1" cellspacing="1" border="1">
                <tr>
                   <td align="center" class="style20" >Trans_Date</td>
                   <td align="center" class="style20">Trans_Type</td>
                   <td align="center" class="style20">Trans_No</td>
                   <td align="center" class="style21">Rate</td>
                   <td align="center" class="style21">Units Credit</td>
                   <td align="center" class="style19">Units Debit</td>
                   <td align="center" class="style14">Balance</td>
                </tr>
            </table>--%>
            <br />
            <br />
            <table align="center" width="700"  cellpadding="0" cellspacing="0" border="0">
                 <tr>
                 <td> 
                     <div id="dvLedger" runat="server" style="text-align: center; display: block; overflow: auto; width:700; height:300px;">

                       <asp:DataGrid ID="dgLedger" runat="server" AutoGenerateColumns="False"  
                             Width="700px" BorderColor="Black" CellPadding="1" CellSpacing="1" meta:resourcekey="dgLedgerResource1"
                            >
                           <SelectedItemStyle HorizontalAlign="Center"></SelectedItemStyle>
                            <ItemStyle CssClass="TableText"></ItemStyle>
                            <HeaderStyle CssClass="DataGridHeader"></HeaderStyle>
                            <AlternatingItemStyle CssClass="AlternatColor"></AlternatingItemStyle>
                            <Columns>
                            <asp:BoundColumn DataField="TRANS_DATE" HeaderText="Trans_Date">
                                <HeaderStyle Width="100px" />
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn DataField="TRANS_TYPE" HeaderText="Trans_Type">
                                <HeaderStyle Width="100px" />
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn  DataField="TRANS_NO" HeaderText="Trans_No">
                                <HeaderStyle Width="100px" />
                                </asp:BoundColumn>    
                            <asp:BoundColumn DataField="RATE" HeaderText="Rate">
                                <HeaderStyle Width="100px" />
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn DataField="UNIT_CREDIT" HeaderText="Units Credit">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                                </asp:BoundColumn>                                                                                              
                            <asp:BoundColumn  DataField="UNIT_DEBIT" HeaderText="Units Debit">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="#FF3300" />
                                </asp:BoundColumn> 
                            <asp:BoundColumn  DataField="BALANCE" HeaderText="Balance">
                                <HeaderStyle Width="100px" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" ForeColor="#00CC00" />
                                </asp:BoundColumn>                                 
                        </Columns>
                    </asp:DataGrid>              
                        </div>          
                   </td>
                 </tr>
            </table>
          
            
             
            </div>
      </ContentTemplate>
     <Triggers >
        <asp:AsyncPostBackTrigger ControlID="findButton" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="ShowLienDatailsButton" EventName="Click" />
        <asp:AsyncPostBackTrigger ControlID="regNoTextBox" EventName="TextChanged" />
     </Triggers>
      
     </asp:UpdatePanel>
   </div>
              <br />
              <table width="450" align="center" cellpadding="0" cellspacing="0">
             <tr>
                <td align="right">
                <asp:Button ID="PrintReportButton" runat="server" Text="Print Ledger" 
                        CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                         AccessKey="p" meta:resourcekey="PrintReportButtonResource1" onclick="PrintReportButton_Click" 
                       />&nbsp;
                </td>
                <td align="left">&nbsp;&nbsp;&nbsp;
                <asp:Button ID="ResetButton" runat="server" Text="Reset" 
                        CssClass="buttoncommon"  OnClientClick="return fnReset();" AccessKey="a" 
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
</asp:Content>

