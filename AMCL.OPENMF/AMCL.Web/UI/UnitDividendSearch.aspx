<%@ Page Language="C#"   MasterPageFile ="~/UI/UnitCommon.master" AutoEventWireup="true" CodeFile="UnitDividendSearch.aspx.cs" Inherits="UI_UnitDividendSearch" Title="Unit Dividend Search Form (Design and Developed by Sakhawat)" culture="auto" meta:resourcekey="PageResource1" uiculture="auto" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script language="javascript" type="text/javascript"> 
  
     function fnReset()
    {
        var Confrm=confirm("Are Sure To Resete");
        if(Confrm)
        {         
            document.getElementById("<%=holderNameTextBox.ClientID%>").value = "";
             document.getElementById("<%=holderBOTextBox.ClientID%>").value ="";    
            document.getElementById("<%=folioTextBox.ClientID%>").value ="";
            document.getElementById("<%=jHolderTextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress1TextBox.ClientID%>").value ="";
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";            
            document.getElementById("<%=holderTelphoneTextBox.ClientID%>").value =""; 
            document.getElementById("<%=holderAddress2TextBox.ClientID%>").value ="";
            document.getElementById("<%=Nominee1NameTextBox.ClientID%>").value =""; 
            document.getElementById("<%=Nominee2NameTextBox.ClientID%>").value ="";
            document.getElementById("<%=tdCIP.ClientID%>").innerHTML = ""; 
            document.getElementById("<%=tdTIN.ClientID%>").innerHTML = ""; 
             document.getElementById("<%=tdBEFTN.ClientID%>").innerHTML =""; 
            document.getElementById("<%=BankInfoTextBox.ClientID%>").value ="";
            document.getElementById("<%=RemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=TotalLienUnitHoldingTextBox.ClientID%>").value =""; 
       
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
            document.getElementById("<%=BankInfoTextBox.ClientID%>").value ="";
            document.getElementById("<%=RemarksTextBox.ClientID%>").value =""; 
            document.getElementById("<%=TotalLienUnitHoldingTextBox.ClientID%>").value =""; 
             
             alert("No Data Found");
             return false;
      
    }
     function fnCheqInput()
        {
        
        
              //Input Text Checking
          if(document.getElementById("<%=regNoTextBox.ClientID%>").value =="" && document.getElementById("<%=holderBOTextBox.ClientID%>").value ==""&& document.getElementById("<%=folioTextBox.ClientID%>").value =="")
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
                 var fundCode=document.getElementById("<%=fundCodeDDL.ClientID%>").value;
                 var reg=document.getElementById("<%=regNoTextBox.ClientID%>").value;
                 var Branch=document.getElementById("<%=branchCodeDDL.ClientID%>").value;
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
	 function CheckAllDataGridWarrantNo(checkVal)
     {
            if(document.getElementById("<%=dvLedger.ClientID%>"))
            {  
                
                var datagrid=document.getElementById("<%=dgLedger.ClientID%>")
                   
                var check = 0;                
                
                for( var rowCount = 0; rowCount < datagrid.rows.length; rowCount++)
                {
                  var tr = datagrid.rows[rowCount];
                  var td= tr.childNodes[0]; 
                  var item = td.firstChild; 
                  var strType=item.type;
                  if(strType=="checkbox")
                  {
                        item.checked = checkVal; 
                  }
                }
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
      .style9
     {
         text-align: center;
         text-decoration: underline;
         color: #6666FF;
         font-size: small;
     }
    
      .style11
     {}
     .style12
     {
         font-weight: bold;
         text-decoration: underline;
     }
      .style13
     {
         font-weight: bold;
         text-align: right;
     }
     .style14
     {
         text-align: left;
         font-weight: bold;
         font-size: medium;
         color: #CC0000;
         font-family: "Times New Roman";
     }
      .style15
     {
         text-align: left;
         font-size: medium;
         text-decoration: underline;
         color: #6666FF;
         font-family: Tahoma;
     }
     
      .style16
     {
         width: 52px;
         font-weight: bold;
     }
     .style17
     {
         font-size: 12px;
         font-weight: 700;
         color: Red;
         width: 42px;
     }
     .style18
     {
         width: 170px;
          font-size: 12px;
         font-weight: 700;
         color: Green;
     }
     
      .auto-style2 {
         overflow: auto;
         width: 1092px;
         height: 300px;
     }
     
      </style>



    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <ajaxToolkit:ToolkitScriptManager runat="Server" 
        EnableScriptGlobalization="True" ID="ScriptManager1" CombineScripts="True" />                
        <table align="center">
        <tr>
            <td class="FormTitle" align="center">
           Unit Holder Dividend Information&nbsp; Form (<span id="spanFundName" runat="server"></span>)
            </td>           
            <td>
                <br />
            </td>
        </tr> 
      </table>
      <br />
      
      
              <div id="dvContent" runat="server" style="width:1200px;  text-align:left;" >
              <table width="1200px" align="left" cellpadding="0" cellspacing="0" border="0" >
              <colgroup width="300"></colgroup>
              <colgroup width="150"></colgroup>
              <colgroup width="310"></colgroup>
               
                   <tr>
                    <td align="right" class="style9" ></td>
                    <td align="right">
                        <b>Registration No:</b></td>
                    <td align="left">
                        <asp:DropDownList ID="fundCodeDDL" runat="server"  ></asp:DropDownList>
                        <b>/</b> <asp:DropDownList ID="branchCodeDDL" runat="server"  ></asp:DropDownList>
                            
                        <b>/</b><asp:TextBox ID="regNoTextBox" runat="server"    
                            CssClass= "TextInputStyleSmall" TabIndex="1" 
                           onkeypress= "fncInputNumericValuesOnly()"
                            meta:resourcekey="regNoTextBoxResource1" Width="60px"></asp:TextBox>
                                            <span class="star">*<asp:Button 
                            ID="findButton" runat="server" Text="Find" CssClass="buttonmid" 
                            onclick="findButton_Click" AccessKey="f" 
                            onclientclick="return fnCheqInput();" TabIndex="2" 
                            meta:resourcekey="findButtonResource1" /></td>
                            <td>
                             
                             
                                &nbsp;
                            </td>
                           
                            
                </tr>
               
                <tr>
                    <td align="right" rowspan="12" >
                    <div  id="dvLockin" runat="server" style="visibility:hidden">
                        <table  width="252px" align="left" cellpadding="0" cellspacing="0" border="0">
                        <colgroup width="125"></colgroup>
                        <colgroup width="120"></colgroup>
                        <tr>
                                <td colspan="2" class="style15">
                                    <b style="text-align: center">Transaction Lock In Status</b></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    SALE LOCK: </td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="SaleLockLabel" runat="server" Text="NO"></asp:Label>
                                                        </td>
                            </tr>
                            <tr>
                                <td class="style13">
                                    REPURCHASE LOCK:</td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="RepLockLabel" runat="server" Text="NO"></asp:Label>
                                                        </td>
                            </tr>
                              <tr>
                                <td class="style13">
                                    TRANSFER LOCK:</td>
                                <td class="style14"> &nbsp;
            
                                    <asp:Label ID="TransferLockLabel" runat="server" Text="NO"></asp:Label>
                                  </td>
                            </tr>
                            <tr>
                                <td class="style13"> &nbsp;
                                    LIEN LOCK:</td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="LienLockLabel" runat="server" Text="NO"></asp:Label>
                                </td>
                            </tr>
                              <tr>
                                <td class="style13"> &nbsp;
                                    RENEWAL LOCK:</td>
                                <td class="style14">
            
                                    &nbsp;
            
                                    <asp:Label ID="RenLockLabel" runat="server" Text="NO"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style12" colspan="2" style="text-align: center">
                                    LOCK&nbsp; IN&nbsp; REMARKS</td>
                            </tr>
                             <tr>
                            <td class="style11" colspan="2">
                          <asp:TextBox ID="LockRemarksTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="63px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                            </tr>
                             <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                             <tr>
                                <td class="style11">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </div>
                    </td>
                    <td align="right">
                        <b>&nbsp;BO Number (if any):</b>

                    </td>
                    <td align="left">
                        <asp:TextBox ID="holderBOTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="16" TabIndex="3"  onkeypress= "fncInputNumericValuesOnly()"
                            meta:resourcekey="holderNameTextBoxResource1" Width="137px"></asp:TextBox>
                        <b>/Folio<asp:TextBox ID="folioTextBox" runat="server" 
                            CssClass="TextInputStyleSmall" MaxLength="10" 
                            onkeypress="fncInputNumericValuesOnly()" 
                             TabIndex="3" Width="90px"></asp:TextBox>
                        </b>
                    </td>
                   
                    <td rowspan="12" align="center">
                              <div  id="dvImage" runat="server"  
                                    style=" border-style: solid; border-color: inherit; border-width: 1px; width: 217px; width:415px; height: 254px;">
                                <table align="center" width="164"  cellpadding="0" cellspacing="0" 
                                    style="height: 180px">
                                <tr style=" height:15px;">
                                   <td align="center" ><span  style="border:1px solid">Signature    and 
                                       Photo    </span></td></tr>
                                   <tr style=" height:102px;">
                                    <td align="left">
                                   <asp:Image ID="SignImage" runat="server" Height="250px" Width="410px" 
                                            meta:resourcekey="SignImageResource1"  />
                                    </td>    
                                </tr>
                                                               
                                </table>
                             </div>
                            </td>
                </tr>
                 
                <tr>
                    <td align="right">
                        <b>Name of Holder:</b></td>
                    <td align="left">
                        <asp:TextBox ID="holderNameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="3" 
                            meta:resourcekey="holderNameTextBoxResource1"></asp:TextBox>
                    </td>
                   
                   
                </tr>
                 
                <tr>
                    <td align="right">
                        <b>Name of Joint Holder:</b></td>
                    <td align="left">
                        <asp:TextBox ID="jHolderTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="4" 
                            meta:resourcekey="jHolderTextBoxResource1"></asp:TextBox>
                    </td>
                   
                </tr>
                <tr>
                    <td align="right">
                        <b>Address1:</b></td>
                    <td align="left">
                        <asp:TextBox ID="holderAddress1TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                            </td>
                  
                    </tr>
                <tr>
                    <td align="right">
                        <b>Address2:</b></td>
                    <td align="left" >
                        <asp:TextBox ID="holderAddress2TextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="6" 
                            meta:resourcekey="holderAddress2TextBoxResource1"></asp:TextBox>
                                            </td>
                    
                </tr>   
                <tr>
                          <td align="right">
                              <b>Telephone/Mobile:</b></td>
                          <td align="left">
                         <asp:TextBox ID="holderTelphoneTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="20" TabIndex="7" 
                                  meta:resourcekey="holderTelphoneTextBoxResource1"></asp:TextBox>
                                            </td>
                   
                 </tr>
                <tr>
                    <td align="right" class="style7">
                        CIP:</td>
                    <td align="left">
                        <table>
                            <tr>
                                <td  id="tdCIP" runat="server" class="style17">&nbsp;</td>
                                 <td align="right" class="style16" >TIN:</td>
                                  <td align="left" class="style18" id="tdTIN" runat="server" >&nbsp;</td>
                                   <td align="right" class="style16" >BEFTN:</td>
                                  <td align="left" class="style18" id="tdBEFTN" runat="server" >&nbsp;</td>
                            </tr>
                        </table>
                     </td>
                        
                </tr>                  
                <tr>
                          <td align="right">
                              <b>Nominee 1&#39;s Name:</b></td>
                    <td align="left">
                        <asp:TextBox ID="Nominee1NameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1"></asp:TextBox>
                                                </td>
                         
                </tr>     
                <tr>
                          <td align="right">
                              <b>Nominee 2&#39;s Name:</b></td>
                    <td align="left">
                        <asp:TextBox ID="Nominee2NameTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" MaxLength="55" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1" ReadOnly="True"></asp:TextBox>
                                                </td>
                         
                </tr>    
                 <tr>
                          <td align="right">
                              <b>Bank Information:</b></td>
                    <td align="left">
                        <asp:TextBox ID="BankInfoTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                         
                </tr>  
                  <tr>
                          <td align="right">
                              <b>Remarks:</b></td>
                    <td align="left">
                        <asp:TextBox ID="RemarksTextBox" runat="server" 
                            CssClass= "TextInputStyleLarge" TabIndex="5" 
                            meta:resourcekey="holderAddress1TextBoxResource1" Font-Bold="False" 
                            Height="42px" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                         
                </tr>        
                <tr>
                    <td align="right">
                        <b>Total Liened Units:</b></td>
                    <td align="left" >
                        <asp:TextBox ID="TotalLienUnitHoldingTextBox" runat="server" 
                            CssClass="TextInputStyleSmall" Enabled="false" Font-Bold="True" 
                            ForeColor="#009933" Width="100px"></asp:TextBox>
                        &nbsp;
                        <asp:Button ID="ShowLienDatailsButton" runat="server" AccessKey="l" 
                            CssClass="buttoncommon" EnableTheming="True" OnClientClick=" return PopupLienDetails();"                            
                            Text="Lien Details" Height="17px" Width="71px" />
                    </td>
                   
                </tr>  
              
                <tr>
                    <td colspan="4">
                     <table>
                    <tr>
        <td align="right"  style="width:150px;">
            <b>Total Record Count:</b></td>    
         <td align="left" style="width:100px;">
            
             <asp:Label ID="totalRowCountLabel" runat="server" Font-Bold="True" 
                 ForeColor="#CC3300" Text="0"></asp:Label>
            
        </td>
         <td align="left">
             <b>&nbsp;Dividend Received By</b> :
             <asp:RadioButton ID="selfRadioButton" runat="server" GroupName="certDelivery"  Checked="true"
                 Text="SELF" Font-Bold="True" />
        </td>
         <td align="left">
          <asp:RadioButton ID="aouthorizeRadioButton" runat="server" GroupName="certDelivery" 
                 Text="Authorized Person" Font-Bold="True" />
             :</td>
         <td align="left">
            <asp:TextBox ID="authorizeTextBox" runat="server"   CssClass= "textInputStyle" 
                 Width="200px"  ></asp:TextBox>
        </td>
        <td align="left">
            <asp:Label ID="dateLabel" runat="server" Font-Bold="True" 
                 Text="Date: "></asp:Label>
        </td>
        <td align="left"> 
            <asp:TextBox ID="dateTextBox" runat="server"   CssClass= "textInputStyle" 
                 Width="100px"   ></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" 
                Enabled="True" Format="dd-MMM-yyyy" PopupButtonID="chequeDateImageButton" 
                TargetControlID="dateTextBox" />
                <asp:ImageButton ID="chequeDateImageButton" runat="server" 
                    AlternateText="Click Here" ImageUrl="~/Image/Calendar_scheduleHS.png" />   
            </td>    
    </tr>
                     </table>
                    </td>
                    
                </tr>
                <tr>
                    <td align="left" colspan="4">
                        <table align="center" border="0" cellpadding="0" cellspacing="0" 
                            style="width: 1203px">
                            <tr>
                                <td align="left" > 
                                     <div ID="dvLedger" runat="server" 
                                        style="text-align: left; display: block; padding-left:20px;" class="auto-style2">
                                       
                                        <asp:DataGrid ID="dgLedger" runat="server" AutoGenerateColumns="False" 
                                            BorderColor="Black" CellPadding="1" CellSpacing="1" 
                                            meta:resourcekey="dgLedgerResource1" Width="1091px">
                                            <SelectedItemStyle HorizontalAlign="Center" />
                                            <ItemStyle CssClass="TableText" />
                                            <HeaderStyle CssClass="DataGridHeader" />
                                            <AlternatingItemStyle CssClass="AlternatColor" />
                                            <Columns>
                                              <asp:TemplateColumn>
                                               <HeaderTemplate>
                                                     <input id="chkAllWarrant" type="checkbox" onclick="CheckAllDataGridWarrantNo(this.checked)"> 
                                                    </HeaderTemplate>
                                             <ItemTemplate> 
                                               <asp:CheckBox ID="leftCheckBox" runat="server"></asp:CheckBox> 
                                            </ItemTemplate>                    
                                             </asp:TemplateColumn>  
                                              <asp:BoundColumn DataField="ID" HeaderText="ID">
                                                   
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FY" HeaderText="FY">
                                                   
                                                </asp:BoundColumn>
                                               
                                                <asp:BoundColumn DataField="ISSUE_DT" HeaderText="Issue Date ">
                                                   
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="WAR_NO" HeaderText="Warrant No">
                                                   
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="BALANCE" HeaderText="No of Unit">
                                                   
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TOT_DIVI" HeaderText="Gross">
                                                    
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="DIDUCT" HeaderText="Tax Deduct">
                                                   
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FI_DIVI_QTY" HeaderText="Net Dividend">
                                                   
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                   <asp:BoundColumn DataField="CIP_QTY" HeaderText="CIP Units">
                                                    
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="IS_BEFTN" HeaderText="Dividend Type">
                                                    
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="WAR_STATUS" HeaderText="Dividend Present Status">
                                                    
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False" ForeColor="#FF3300"  />
                                                </asp:BoundColumn>
                                                
                                               <%-- <asp:BoundColumn DataField="WARR_RECPT_BY" HeaderText="Dividend Received By">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                
                                                  <asp:BoundColumn DataField="WAR_BK_PAY_DT" HeaderText="Paid Date">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>
                                                 <asp:BoundColumn DataField="BEFTN_RETURN_DT" HeaderText="BEFTN Return Date">
                                                    <HeaderStyle Width="100px" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                        Font-Strikeout="False" Font-Underline="False"  />
                                                </asp:BoundColumn>--%>
                                            </Columns>
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
              </table>
              </div>
            
    
 
             
              <table width="450" align="center" cellpadding="0" cellspacing="0">
             <tr>
                <td align="right">
                    &nbsp;
                <asp:Button ID="SaveButton" runat="server" Text="Save" CssClass="buttoncommon" OnClientClick="return fnCheqInput();"
                onclick="SaveButton_Click" AccessKey="s" 
                meta:resourcekey="SaveButtonResource1"/>
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

